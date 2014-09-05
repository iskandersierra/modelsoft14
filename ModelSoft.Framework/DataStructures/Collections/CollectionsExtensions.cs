using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.DataStructures.Collections
{
  public static class CollectionsExtensions
  {
    public static ICollection<T> AsReadOnly<T>(this ICollection<T> list)
    {
      if (list == null) return null;

      return new ReadOnlyCollectionWrapper<T>(list);
    }
    public static IList<T> AsReadOnly<T>(this IList<T> list)
    {
      if (list == null) return null;

      return new ReadOnlyListWrapper<T>(list);
    }
    public static IDictionary<K, T> AsReadOnly<K, T>(this IDictionary<K, T> dictionary)
    {
      if (dictionary == null) return null;

      return new ReadOnlyDictionaryWrapper<K, T>(dictionary);
    }

    public static IList<T> AsRange<T>(this IList<T> list, int startIndex = -1, int count = -1)
    {
      if (list == null) return null;

      return new ListRangeWrapper<T>(list, startIndex, count);
    }

    public static IList<T> AsRange<T>(this T[] array, int startIndex = -1, int count = -1)
    {
      if (array == null) return null;

      return new ArrayRangeWrapper<T>(array, startIndex, count);
    }
  }
}
