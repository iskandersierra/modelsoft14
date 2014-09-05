using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework.Collections
{
  public class ReadOnlyDictionary<TKey, TValue> 
    : IDictionary<TKey, TValue>
  {
    private IDictionary<TKey, TValue> innerDictionary;

    public ReadOnlyDictionary(IDictionary<TKey, TValue> innerDictionary)
    {
      innerDictionary.RequireNotNull("innerDictionary");

      this.innerDictionary = innerDictionary;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return innerDictionary.GetEnumerator();
    }

    public bool ContainsKey(TKey key)
    {
      return innerDictionary.ContainsKey(key);
    }

    void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
    {
      throw new NotSupportedException();
    }

    bool IDictionary<TKey, TValue>.Remove(TKey key)
    {
      throw new NotSupportedException();
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      return innerDictionary.TryGetValue(key, out value);
    }

    public TValue this[TKey key]
    {
      get { return innerDictionary[key]; }
      set { throw new NotSupportedException(); }
    }

    private ICollection<TKey> keys;
    public ICollection<TKey> Keys
    {
      get { return keys ?? (keys = new ReadOnlyList<TKey>(innerDictionary.Keys)); }
    }

    private ICollection<TValue> values;
    public ICollection<TValue> Values
    {
      get { return values ?? (values = new ReadOnlyList<TValue>(innerDictionary.Values)); }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
      throw new NotSupportedException();
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Clear()
    {
      throw new NotSupportedException();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      return innerDictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      innerDictionary.CopyTo(array, arrayIndex);
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
      throw new NotSupportedException();
    }

    public int Count
    {
      get { return innerDictionary.Count; }
    }

    public bool IsReadOnly
    {
      get { return true; }
    }
  }
}
