using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Collections
{
  public class SimpleConcurrentCache<TKey, TValue> :
    ISimpleCache<TKey, TValue>
  {
    private ConcurrentDictionary<TKey, TValue> internalCache;
    private Func<TKey, TValue> valueFactory;

    public SimpleConcurrentCache(Func<TKey, TValue> valueFactory, IEqualityComparer<TKey> comparer = null)
    {
      valueFactory.RequireNotNull("valueFactory");

      this.internalCache = new ConcurrentDictionary<TKey, TValue>(comparer ?? EqualityComparer<TKey>.Default);
      this.valueFactory = valueFactory;
    }

    public void Clear(TKey key)
    {
      TValue value;
      internalCache.TryRemove(key, out value);
    }

    public TValue Get(TKey key)
    {
      return GetInternal(key, valueFactory);
    }

    public TValue Get(TKey key, Func<TKey, TValue> valueFactory)
    {
      valueFactory.RequireNotNull("valueFactory");

      return GetInternal(key, valueFactory);
    }

    public void Update(TKey key, TValue newValue)
    {
      internalCache[key] = newValue;
    }

    public void Clear()
    {
      internalCache.Clear();
    }

    private TValue GetInternal(TKey key, Func<TKey, TValue> valueFactory)
    {
      return internalCache.GetOrAdd(key, valueFactory);
    }

    void ISimpleCache.Clear(object key)
    {
      Clear((TKey)key);
    }

    object ISimpleCache.Get(object key)
    {
      return this.Get((TKey)key);
    }

    object ISimpleCache.Get(object key, Func<object, object> valueFactory)
    {
      return this.Get((TKey)key, k => (TValue)valueFactory(key));
    }

    void ISimpleCache.Update(object key, object newValue)
    {
      this.Update((TKey)key, (TValue)newValue);
    }
  }
}
