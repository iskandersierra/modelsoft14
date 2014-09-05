using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Collections
{
  public interface IValueMap : IEnumerable
  {
    bool ContainsKey(object key);

    object Get(object key);

    object GetOr(object key, object defaultValue);

    object GetOr(object key, Func<object> defaultValueFunc);
  }

  public interface IValueMap<K, T> : IValueMap, IDictionary<K, T>
  {
    //bool ContainsKey(K key);

    T Get(K key);

    TChild GetOr<TChild>(K key, TChild defaultValue) where TChild : T;

    TChild GetOr<TChild>(K key, Func<TChild> defaultValueFunc) where TChild : T;

    //T this[K key] { get; }

    T this[K key, T defaultValue] { get; }

    T this[K key, Func<T> defaultValueFunc] { get; }
  }
}
