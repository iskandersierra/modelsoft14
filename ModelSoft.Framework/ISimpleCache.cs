using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework
{
  public interface ISimpleCache
  {
    void Clear();
    void Clear(object key);

    object Get(object key);
    object Get(object key, Func<object, object> valueFactory);

    void Update(object key, object newValue);
  }

  public interface ISimpleCache<TKey, TValue> :
    ISimpleCache
  {
    void Clear(TKey key);

    TValue Get(TKey key);
    TValue Get(TKey key, Func<TKey, TValue> valueFactory);

    void Update(TKey key, TValue newValue);
  }

  public static class SimpleCacheExtensions
  {
    public static object Get(this ISimpleCache cache, object key, object defaultValue)
    {
      cache.RequireNotNull("cache");

      return cache.Get(key, _ => defaultValue);
    }

  }
}
