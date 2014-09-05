using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Collections
{
  public class Trie<TKey, TSymbol, TValue> :
    ITrie<TKey, TSymbol, TValue>
  {
    IEqualityComparer<TSymbol> comparer;
    Func<TKey, IEnumerable<TSymbol>> unfoldKey;
    Func<IEnumerable<TSymbol>, TKey> foldKey;
    TrieNode<TSymbol, TValue> root;

    public Trie(Func<TKey, IEnumerable<TSymbol>> unfoldKey, Func<IEnumerable<TSymbol>, TKey> foldKey = null, IEqualityComparer<TSymbol> comparer = null)
    {
      unfoldKey.RequireNotNull("unfoldKey");

      this.unfoldKey = unfoldKey;
      this.foldKey = foldKey;
      this.comparer = comparer;
      root = null;
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> Track(TKey key, out bool found, bool activeOnly = true)
    {
      if (foldKey == null)
        throw new NotSupportedException();
      var unfoldedKey = unfoldKey(key).ToList();
      var track = root.Track(unfoldedKey.GetEnumerator(), comparer, out found).ToList();
      var result = track.Enumerate(unfoldedKey, StopsWith.Shorter).ToList();
      if (activeOnly)
        result.Where(t => t.Item1.GetIsActive()).ToList();

      return null;
    }

    IEnumerable<KeyValuePair<object, object>> ITrie.Track(object key, out bool found, bool activeOnly)
    {
      return Track((TKey)key, out found, activeOnly)
        .Select(p => KeyValuePair.Create((object)p.Key, (object)p.Value));
    }

    public void Add(object key, object value)
    {
      throw new NotImplementedException();
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public bool Contains(object key)
    {
      throw new NotImplementedException();
    }

    public System.Collections.IDictionaryEnumerator GetEnumerator()
    {
      throw new NotImplementedException();
    }

    public bool IsFixedSize
    {
      get { throw new NotImplementedException(); }
    }

    public bool IsReadOnly
    {
      get { throw new NotImplementedException(); }
    }

    public System.Collections.ICollection Keys
    {
      get { throw new NotImplementedException(); }
    }

    public void Remove(object key)
    {
      throw new NotImplementedException();
    }

    public System.Collections.ICollection Values
    {
      get { throw new NotImplementedException(); }
    }

    public object this[object key]
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public void CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get { throw new NotImplementedException(); }
    }

    public bool IsSynchronized
    {
      get { throw new NotImplementedException(); }
    }

    public object SyncRoot
    {
      get { throw new NotImplementedException(); }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    public void Add(TKey key, TValue value)
    {
      throw new NotImplementedException();
    }

    public bool ContainsKey(TKey key)
    {
      throw new NotImplementedException();
    }

    ICollection<TKey> IDictionary<TKey, TValue>.Keys
    {
      get { throw new NotImplementedException(); }
    }

    public bool Remove(TKey key)
    {
      throw new NotImplementedException();
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      throw new NotImplementedException();
    }

    ICollection<TValue> IDictionary<TKey, TValue>.Values
    {
      get { throw new NotImplementedException(); }
    }

    public TValue this[TKey key]
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
      throw new NotImplementedException();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      throw new NotImplementedException();
    }

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
}
