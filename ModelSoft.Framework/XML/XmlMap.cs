using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Xml.Linq;
using ModelSoft.Framework;
using ModelSoft.Framework.Collections;

namespace ModelSoft.Framework.XML
{
  public class XmlMap<K, T> : IValueMap<K, T>, IPersistentStructure
  {
    private Dictionary<K, T> cache;
    private string sourceFile;
    private readonly IEqualityComparer<K> keyComparer;
    private bool autoSave = false;
    private bool modified = false;
    private static readonly string PairXName = "Pair";
    private static readonly string KeyTypeXName = "KeyType";
    private static readonly string StringQualifiedName = typeof(string).AssemblyQualifiedName;
    private static readonly string ValueTypeXName = "ValueType";
    private static readonly string KeyXName = "Key";
    private static readonly string ValueXName = "Value";
    private static readonly string XmlMapXName = "XmlMap";

    public XmlMap(string sourceFile, IEqualityComparer<K> keyComparer = null)
    {
      if (sourceFile == null) throw new ArgumentNullException("sourceFile");
      this.sourceFile = sourceFile;
      this.keyComparer = keyComparer ?? EqualityComparer<K>.Default;
      cache = new Dictionary<K, T>(keyComparer);
      Load();
    }

    public bool AutoSave
    {
      get { return autoSave; }
      set
      {
        if (autoSave == value) return;
        autoSave = value;
        if (autoSave)
          Save();
      }
    }

    public string SourceFile
    {
      get { return sourceFile; }
    }

    public bool Modified
    {
      get { return modified; }
    }

    public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
    {
      return cache.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    void ICollection<KeyValuePair<K, T>>.Add(KeyValuePair<K, T> item)
    {
      Add(item.Key, item.Value);
    }

    public void Clear()
    {
      if (cache.Count == 0) return;
      cache.Clear();
      modified = true;
      DoAutoSave();
    }

    bool ICollection<KeyValuePair<K, T>>.Contains(KeyValuePair<K, T> item)
    {
      return ((IDictionary<K, T>)cache).Contains(item);
    }

    public void CopyTo(KeyValuePair<K, T>[] array, int arrayIndex)
    {
      cache.CopyTo(array, arrayIndex);
    }

    bool ICollection<KeyValuePair<K, T>>.Remove(KeyValuePair<K, T> item)
    {
      if (((IDictionary<K, T>)cache).Remove(item))
      {
        modified = true;
        DoAutoSave();
        return true;
      }
      return false;
    }

    public int Count
    {
      get { return cache.Count; }
    }

    public bool IsReadOnly
    {
      get { return false; }
    }

    public bool ContainsKey(K key)
    {
      return cache.ContainsKey(key);
    }

    public void Add(K key, T value)
    {
      cache.Add(key, value);
      modified = true;
      DoAutoSave();
    }

    public bool Remove(K key)
    {
      if (cache.Remove(key))
      {
        modified = true;
        DoAutoSave();
        return true;
      }
      return false;
    }

    public bool TryGetValue(K key, out T value)
    {
      return cache.TryGetValue(key, out value);
    }

    public T this[K key]
    {
      get { return cache[key]; }
      set
      {
        cache[key] = value;
        modified = true;
        DoAutoSave();
      }
    }

    public T this[K key, T defaultValue]
    {
      get { return GetOr<T>(key, defaultValue); }
    }

    public T this[K key, Func<T> defaultValueFunc]
    {
      get { return GetOr<T>(key, defaultValueFunc); }
    }

    public T Get(K key)
    {
      return cache[key];
    }

    public TChild GetOr<TChild>(K key, TChild defaultValue) where TChild : T
    {
      T value;
      if (TryGetValue(key, out value) && value is TChild)
        return (TChild)value;
      this[key] = defaultValue;
      return defaultValue;
    }

    public TChild GetOr<TChild>(K key, Func<TChild> defaultValueFunc) where TChild : T
    {
      T value;
      if (TryGetValue(key, out value) && value is TChild)
        return (TChild)value;
      var defaultValue = defaultValueFunc();
      this[key] = defaultValue;
      return defaultValue;
    }

    bool IValueMap.ContainsKey(object key)
    {
      return this.ContainsKey((K)key);
    }

    object IValueMap.Get(object key)
    {
      return this.Get((K)key);
    }

    object IValueMap.GetOr(object key, object defaultValue)
    {
      return this.GetOr<T>((K)key, (T)defaultValue);
    }

    object IValueMap.GetOr(object key, Func<object> defaultValueFunc)
    {
      return this.GetOr<T>((K)key, () => (T)defaultValueFunc());
    }

    public ICollection<K> Keys
    {
      get
      {
        return cache.Keys;
      }
    }

    public ICollection<T> Values
    {
      get { return cache.Values; }
    }

    public void Dispose()
    {
      if (sourceFile != null)
      {
        if (modified)
          Save();
        cache = null;
        sourceFile = null;
      }
    }

    public void Load()
    {
      if (sourceFile == null)
        throw new ObjectDisposedException("XmlMap already disposed");
      cache.Clear();
      if (File.Exists(sourceFile))
      {
        var simpleValueSerializerContext = new SimpleValueSerializerContext();
        var doc = XDocument.Load(sourceFile);
        foreach (var pairElement in doc.Root.Elements(PairXName))
        {
          var keyType = Type.GetType(pairElement.Attribute(KeyTypeXName).GetValueOr(StringQualifiedName));
          var valueType = Type.GetType(pairElement.Attribute(ValueTypeXName).GetValueOr(StringQualifiedName));
          var keyValue = pairElement.Element(KeyXName).Value;
          var valueValue = pairElement.Element(ValueXName).Value;
          var key = (K)ValueSerializer.GetSerializerFor(keyType).ConvertFromString(keyValue, simpleValueSerializerContext);
          var value = (T)ValueSerializer.GetSerializerFor(valueType).ConvertFromString(valueValue, simpleValueSerializerContext);

          cache.Add(key, value);
        }
      }
      modified = false;
    }

    public void Save()
    {
      var simpleValueSerializerContext = new SimpleValueSerializerContext();
      var doc = new XDocument(
        new XElement(XmlMapXName,
          cache.Select(p =>
            new XElement(PairXName,
              (p.Key is string ? null : new XAttribute(KeyTypeXName, p.Key.GetType().AssemblyQualifiedName)).SingletonNonNull<object>()
              .Concat((p.Value is string ? null : new XAttribute(ValueTypeXName, p.Value.GetType().AssemblyQualifiedName)).SingletonNonNull<object>())
              .Concat(new XElement(KeyXName, ValueSerializer.GetSerializerFor(p.Key.GetType()).ConvertToString(p.Key, simpleValueSerializerContext)).Singleton<object>())
              .Concat(new XElement(ValueXName, ValueSerializer.GetSerializerFor(p.Value.GetType()).ConvertToString(p.Value, simpleValueSerializerContext)).Singleton<object>())
              )
          )
          )
        );
      doc.Save(sourceFile);
      modified = false;
    }

    private void DoAutoSave()
    {
      if (modified && autoSave)
        Save();
    }
  }
}
