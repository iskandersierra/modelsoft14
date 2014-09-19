using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using ModelSoft.Framework.Collections;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework
{
    //[DebuggerStepThrough]
    public static class EnumerableExtensions
    {
        #region [ Ex ]
        #region [ AggregateEx ]
        public static object AggregateEx(this IEnumerable source, Func<object, object, object> func)
        {
            source.RequireNotNull("source");
            func.RequireNotNull("func");

            object result;
            IEnumerator enumerator = source.GetEnumerator();
            try
            {
                Require.Condition(enumerator.MoveNext(), "source", Resources.ExMsg_EnumerableExtensions_EmptyCollection);

                object item = enumerator.Current;
                while (enumerator.MoveNext())
                    item = func(item, enumerator.Current);

                result = item;
            }
            finally
            {
                if (enumerator is IDisposable)
                    ((IDisposable)enumerator).Dispose();
            }
            return result;
        }
        public static object AggregateEx(this IEnumerable source, object seed, Func<object, object, object> funcAggAccumAndValue)
        {
            source.RequireNotNull("source");
            funcAggAccumAndValue.RequireNotNull("funcAggAccumAndValue");

            object accum = seed;
            foreach (object item in source)
                accum = funcAggAccumAndValue(accum, item);

            return accum;
        }
        public static object AggregateEx(this IEnumerable source, object seed, Func<object, object, object> funcAggAccumAndValue, Func<object, object> resultSelector)
        {
            source.RequireNotNull("source");
            funcAggAccumAndValue.RequireNotNull("funcAggAccumAndValue");
            resultSelector.RequireNotNull("resultSelector");

            object accum = seed;
            foreach (object item in source)
                accum = funcAggAccumAndValue(accum, item);

            object result = resultSelector(accum);
            return result;
        }
        #endregion

        #region [ AllEx/AnyEx ]
        public static bool AllEx(this IEnumerable source, Func<object, bool> predicate)
        {
            source.RequireNotNull("source");
            predicate.RequireNotNull("predicate");

            foreach (object item in source)
                if (!predicate(item))
                    return false;
            return true;
        }
        public static bool AnyEx(this IEnumerable source)
        {
            source.RequireNotNull("source");

            IEnumerator enumerator = source.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                    return true;
            }
            finally
            {
                if (enumerator is IDisposable)
                    ((IDisposable)enumerator).Dispose();
            }
            return false;
        }
        public static bool AnyEx(this IEnumerable source, Func<object, bool> predicate)
        {
            source.RequireNotNull("source");
            predicate.RequireNotNull("predicate");

            foreach (object current in source)
                if (predicate(current))
                    return true;
            return false;
        }
        #endregion

        #region [ AverageEx -- TODO ]
        #endregion

        #region [ ConcatEx ]
        public static IEnumerable ConcatEx(this IEnumerable first, IEnumerable second)
        {
            first.RequireNotNull("first");
            second.RequireNotNull("second");

            foreach (var item in first)
                yield return item;
            foreach (var item in second)
                yield return item;
        }
        #endregion

        #region [ ContainsEx ]
        public static bool ContainsEx(this IEnumerable source, object value)
        {
            source.RequireNotNull("source");

            return source.ContainsEx(value, null);
        }
        public static bool ContainsEx(this IEnumerable source, object value, Func<object, object, bool> funcEquals)
        {
            source.RequireNotNull("source");
            if (funcEquals == null)
                funcEquals = (o1, o2) => object.Equals(o1, o2);

            foreach (object current in source)
                if (funcEquals(current, value))
                    return true;
            return false;
        }

        #endregion

        #region [ CopyToEx ]
        public static void CopyToEx(this IEnumerable e, object[] array, int arrayIndex)
        {
            e.TakeEx(array.Length - arrayIndex).ForEachEx(t => array[arrayIndex++] = t);
        }
        public static void CopyToEx(this IEnumerable e, Array array, int arrayIndex)
        {
            e.TakeEx(array.Length - arrayIndex).ForEachEx(t => array.SetValue(t, arrayIndex++));
        }
        #endregion

        #region [ Do/ForEach ]

        [DebuggerStepThrough]
        public static IEnumerable DoEx(this IEnumerable e, Action<object> action)
        {
            action.RequireNotNull("action");

            if (e == null) yield break;
            foreach (var t in e)
            {
                action(t);
                yield return t;
            }
        }
        [DebuggerStepThrough]
        public static IEnumerable DoEx(this IEnumerable e, Action<object, int> action)
        {
            action.RequireNotNull("action");

            if (e == null) yield break;
            int pos = 0;
            foreach (var t in e)
            {
                action(t, pos);
                yield return t;
                pos++;
            }
        }
        [DebuggerStepThrough]
        public static IEnumerable ForEachEx(this IEnumerable e, Action<object> action)
        {
            if (e == null) return null;
            foreach (var t in e.DoEx(action)) { }
            return e;
        }
        [DebuggerStepThrough]
        public static IEnumerable ForEachEx(this IEnumerable e, Action<object, int> action)
        {
            if (e == null) return null;
            foreach (var t in e.DoEx(action)) { }
            return e;
        }

        #endregion

        #region [ OfTypeEx/Exact ]
        public static IEnumerable OfTypeEx(this IEnumerable enumerable, Type type)
        {
            enumerable.RequireNotNull("enumerable");
            type.RequireNotNull("type");

            return enumerable.WhereEx(e => type.IsInstanceOfType(e));
        }
        public static IEnumerable OfTypeExactEx(this IEnumerable enumerable, Type type)
        {
            enumerable.RequireNotNull("enumerable");
            type.RequireNotNull("type");

            return enumerable.WhereEx(e => e != null && type == e.GetType());
        }
        #endregion

        #region [ TakeEx/SkipEx ]
        public static IEnumerable SkipIterator(this IEnumerable source, int count)
        {
            var enumerator = source.GetEnumerator();
            while (count > 0 && enumerator.MoveNext())
                --count;
            if (count <= 0)
                while (enumerator.MoveNext())
                    yield return enumerator.Current;
        }

        public static IEnumerable TakeEx(this IEnumerable e, int count)
        {
            if (count > 0)
            {
                foreach (object source1 in e)
                {
                    yield return source1;
                    if (--count == 0)
                        break;
                }
            }
        }
        #endregion [ TakeEx/SkipEx ]

        #region [ WhereEx ]
        public static IEnumerable WhereEx(this IEnumerable source, Func<object, bool> predicate)
        {
            source.RequireNotNull("source");
            predicate.RequireNotNull("predicate");

            foreach (var item in source)
                if (predicate(item))
                    yield return item;
        }
        #endregion

        #endregion

        #region [ OfType/Exact ]
        public static IEnumerable<T> OfType<T>(this IEnumerable<T> enumerable, Type type)
        {
            enumerable.RequireNotNull("enumerable");
            type.RequireNotNull("type");

            return enumerable.Where<T>(e => type.IsInstanceOfType(e));
        }
        public static IEnumerable<T> OfTypeExact<T>(this IEnumerable<T> enumerable, Type type)
        {
            enumerable.RequireNotNull("enumerable");
            type.RequireNotNull("type");

            return enumerable.Where<T>(e => e != null && type == e.GetType());
        }
        #endregion

        #region [ Singleton ]

        public static IEnumerable<T> Singleton<T>(this T obj)
        {
            yield return obj;
        }

        public static IEnumerable<T> SingletonNonNull<T>(this T obj)
          where T : class
        {
            if (obj != null)
                yield return obj;
        }

        #endregion

        #region [ AsEnumerable ]

        /// <summary>
        /// Cast the object as IEnumerable of T
        /// </summary>
        public static IEnumerable<T> AsEnumerable<T>(this object obj)
        {
            return obj as IEnumerable<T>;
        }

        /// <summary>
        /// Cast the object as IEnumerable of T, but returns an empty enumerator if the object is not IEnumerable of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<T> AsEnumerableNonNull<T>(this object obj)
        {
            var enumerable = obj as IEnumerable<T>;
            if (enumerable != null)
            {
                foreach (var item in enumerable)
                    yield return item;
            }
        }

        #endregion

        #region [ EnumerateRange ]

        public static IEnumerable<T> EnumerateRange<T>(this IList<T> list, int fromIndex = -1, int toIndex = -1)
        {
            if (list == null) yield break;
            if (fromIndex == -1) fromIndex = 0;
            if (toIndex == -1) toIndex = list.Count - 1;
            fromIndex.RequireInRange(0, list.Count - 1, "fromIndex");
            toIndex.RequireInRange(fromIndex, list.Count - 1, "toIndex");

            for (int i = fromIndex; i <= toIndex; i++)
            {
                yield return list[i];
            }
        }

        public static IEnumerable<T> EnumerateRange<T>(this T[] array, int fromIndex = -1, int toIndex = -1)
        {
            if (array == null) yield break;
            if (fromIndex == -1) fromIndex = 0;
            if (toIndex == -1) toIndex = array.Length - 1;
            fromIndex.RequireInRange(0, array.Length - 1, "fromIndex");
            toIndex.RequireInRange(fromIndex, array.Length - 1, "toIndex");

            for (int i = fromIndex; i <= toIndex; i++)
            {
                yield return array[i];
            }
        }

        public static IEnumerable EnumerateListRange(this IList array, int fromIndex = -1, int toIndex = -1)
        {
            if (array == null) yield break;
            if (fromIndex == -1) fromIndex = 0;
            if (toIndex == -1) toIndex = array.Count - 1;
            fromIndex.RequireInRange(0, array.Count - 1, "fromIndex");
            toIndex.RequireInRange(fromIndex, array.Count - 1, "toIndex");

            for (int i = fromIndex; i <= toIndex; i++)
            {
                yield return array[i];
            }
        }

        #endregion

        #region [ IsEmpty / NotNull / IsNotEmpty / GetAsEmptyIfNull / NotNull ]

        public static bool IsEmpty<T>(this IEnumerable<T> e)
        {
            return e == null || !e.Any();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> e, Func<T, bool> predicate)
        {
            predicate.RequireNotNull("predicate");

            return e == null || !e.Any(predicate);
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> e)
        {
            return IsNotEmpty(e, t => true);
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> e, Func<T, bool> predicate)
        {
            predicate.RequireNotNull("predicate");

            return e != null && e.Any(predicate);
        }

        public static IEnumerable<T> GetAsEmptyIfNull<T>(this IEnumerable<T> e)
        {
            return e ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> e)
          where T : class
        {
            return e.Where(x => x != null);
        }

        public static bool IsEmpty(this IEnumerable e)
        {
            if (e == null) return true;
            foreach (var v in e)
                return false;
            return true;
        }

        public static bool IsNotEmpty(this IEnumerable e)
        {
            return !e.IsEmpty();
        }

        public static IEnumerable GetAsEmptyIfNull(this IEnumerable e)
        {
            return e ?? Enumerable.Empty<object>();
        }

        public static IEnumerable NotNull(this IEnumerable e)
        {
            return e.Cast<object>().Where(x => x != null);
        }

        #endregion

        #region [ Concat ]

        [DebuggerStepThrough]
        public static IEnumerable Concat(this IEnumerable itemsAndCollections)
        {
            foreach (var o in itemsAndCollections)
            {
                if (o is IEnumerable && !(o is string))
                    foreach (var item in (IEnumerable)o)
                        yield return item;
                else
                    yield return o;
            }
        }
        [DebuggerStepThrough]
        public static IEnumerable Concat(params object[] itemsAndCollections)
        {
            return Concat((IEnumerable)itemsAndCollections);
        }
        [DebuggerStepThrough]
        public static IEnumerable ConcatNonNull(this IEnumerable itemsAndCollections)
        {
            foreach (var o in itemsAndCollections)
            {
                if (o == null) continue;
                if (o is IEnumerable && !(o is string))
                    foreach (var item in (IEnumerable)o)
                    {
                        if (item != null)
                            yield return item;
                    }
                else
                    yield return o;
            }
        }
        [DebuggerStepThrough]
        public static IEnumerable ConcatNonNull(params object[] itemsAndCollections)
        {
            return ConcatNonNull((IEnumerable)itemsAndCollections);
        }
        [DebuggerStepThrough]
        public static IEnumerable<T> Concat<T>(this IEnumerable<IEnumerable<T>> e)
        {
            foreach (var sub in e)
                foreach (var t in sub)
                    yield return t;
        }

        #endregion

        #region [ FirstOr / LastOr / SingleOr ]

        public static T FirstOr<T>(this IEnumerable<T> e, Predicate<T> condition, T defaultValue)
        {
            T value;
            if (e.TryGetFirst(condition, out value)) return value;
            return defaultValue;
        }
        public static T FirstOr<T>(this IEnumerable<T> e, T defaultValue)
        {
            return e.FirstOr(t => true, defaultValue);
        }
        public static T FirstOr<T>(this IEnumerable<T> e, Predicate<T> condition, Func<T> defaultValue)
        {
            T value;
            if (e.TryGetFirst(condition, out value)) return value;
            return defaultValue();
        }
        public static T FirstOr<T>(this IEnumerable<T> e, Func<T> defaultValue)
        {
            return e.FirstOr(t => true, defaultValue);
        }
        public static T FirstOr<T>(this IEnumerable<T> e, Func<T, int, bool> condition, T defaultValue)
        {
            T value;
            if (e.TryGetFirst(condition, out value)) return value;
            return defaultValue;
        }
        public static T FirstOr<T>(this IEnumerable<T> e, Func<T, int, bool> condition, Func<T> defaultValue)
        {
            T value;
            if (e.TryGetFirst(condition, out value)) return value;
            return defaultValue();
        }

        public static T LastOr<T>(this IEnumerable<T> e, Predicate<T> condition, T defaultValue)
        {
            T value;
            if (e.TryGetLast(condition, out value)) return value;
            return defaultValue;
        }
        public static T LastOr<T>(this IEnumerable<T> e, T defaultValue)
        {
            return e.LastOr(t => true, defaultValue);
        }
        public static T LastOr<T>(this IEnumerable<T> e, Predicate<T> condition, Func<T> defaultValue)
        {
            T value;
            if (e.TryGetLast(condition, out value)) return value;
            return defaultValue();
        }
        public static T LastOr<T>(this IEnumerable<T> e, Func<T> defaultValue)
        {
            return e.LastOr(t => true, defaultValue);
        }
        public static T LastOr<T>(this IEnumerable<T> e, Func<T, int, bool> condition, T defaultValue)
        {
            T value;
            if (e.TryGetLast(condition, out value)) return value;
            return defaultValue;
        }
        public static T LastOr<T>(this IEnumerable<T> e, Func<T, int, bool> condition, Func<T> defaultValue)
        {
            T value;
            if (e.TryGetLast(condition, out value)) return value;
            return defaultValue();
        }

        public static T SingleOr<T>(this IEnumerable<T> e, Predicate<T> condition, T defaultValue)
        {
            T value;
            if (e.TryGetSingle(condition, out value)) return value;
            return defaultValue;
        }
        public static T SingleOr<T>(this IEnumerable<T> e, T defaultValue)
        {
            return e.SingleOr(t => true, defaultValue);
        }
        public static T SingleOr<T>(this IEnumerable<T> e, Predicate<T> condition, Func<T> defaultValue)
        {
            T value;
            if (e.TryGetSingle(condition, out value)) return value;
            return defaultValue();
        }
        public static T SingleOr<T>(this IEnumerable<T> e, Func<T> defaultValue)
        {
            return e.SingleOr(t => true, defaultValue);
        }
        public static T SingleOr<T>(this IEnumerable<T> e, Func<T, int, bool> condition, T defaultValue)
        {
            T value;
            if (e.TryGetSingle(condition, out value)) return value;
            return defaultValue;
        }
        public static T SingleOr<T>(this IEnumerable<T> e, Func<T, int, bool> condition, Func<T> defaultValue)
        {
            T value;
            if (e.TryGetSingle(condition, out value)) return value;
            return defaultValue();
        }

        public static bool TryGetFirst<T>(this IEnumerable<T> e, Predicate<T> condition, out T value)
        {
            foreach (var t in e)
            {
                if (condition(t))
                {
                    value = t;
                    return true;
                }
            }
            value = default(T);
            return false;
        }
        public static bool TryGetFirst<T>(this IEnumerable<T> e, Func<T, int, bool> condition, out T value)
        {
            int pos = 0;
            foreach (var t in e)
            {
                if (condition(t, pos))
                {
                    value = t;
                    return true;
                }
                pos++;
            }
            value = default(T);
            return false;
        }
        public static bool TryGetLast<T>(this IEnumerable<T> e, Predicate<T> condition, out T value)
        {
            bool found = false;
            T foundT = default(T);
            foreach (var t in e)
            {
                if (condition(t))
                {
                    foundT = t;
                    found = true;
                }
            }
            value = foundT;
            return found;
        }
        public static bool TryGetLast<T>(this IEnumerable<T> e, Func<T, int, bool> condition, out T value)
        {
            bool found = false;
            T foundT = default(T);
            int pos = 0;
            foreach (var t in e)
            {
                if (condition(t, pos))
                {
                    foundT = t;
                    found = true;
                }
                pos++;
            }
            value = foundT;
            return found;
        }
        public static bool TryGetSingle<T>(this IEnumerable<T> e, Predicate<T> condition, out T value)
        {
            bool found = false;
            T foundT = default(T);
            foreach (var t in e)
            {
                if (condition(t))
                {
                    if (found)
                    {
                        value = default(T);
                        return false;
                    }
                    foundT = t;
                    found = true;
                }
            }
            value = foundT;
            return found;
        }
        public static bool TryGetSingle<T>(this IEnumerable<T> e, Func<T, int, bool> condition, out T value)
        {
            bool found = false;
            T foundT = default(T);
            int pos = 0;
            foreach (var t in e)
            {
                if (condition(t, pos))
                {
                    if (found)
                    {
                        value = default(T);
                        return false;
                    }
                    foundT = t;
                    found = true;
                }
                pos++;
            }
            value = foundT;
            return found;
        }

        public static T First<T>(IEnumerable<T> e, Func<T, int, bool> predicate)
        {
            T value;
            Require.Condition(TryGetFirst(e, predicate, out value), "e", Resources.ExMsg_EnumerableExtensions_NoMatchingElementException);
            return value;
        }
        public static T FirstOrDefault<T>(IEnumerable<T> e, Func<T, int, bool> predicate)
        {
            return e.FirstOr(predicate, default(T));
        }
        public static T Single<T>(IEnumerable<T> e, Func<T, int, bool> predicate)
        {
            T value;
            Require.Condition(TryGetSingle(e, predicate, out value), "e", Resources.ExMsg_EnumerableExtensions_NoMatchingElementException);
            return value;
        }
        public static T SingleOrDefault<T>(IEnumerable<T> e, Func<T, int, bool> predicate)
        {
            return e.SingleOr(predicate, default(T));
        }
        public static T Last<T>(IEnumerable<T> e, Func<T, int, bool> predicate)
        {
            T value;
            Require.Condition(TryGetLast(e, predicate, out value), "e", Resources.ExMsg_EnumerableExtensions_NoMatchingElementException);
            return value;
        }
        public static T LastOrDefault<T>(IEnumerable<T> e, Func<T, int, bool> predicate)
        {
            return e.LastOr(predicate, default(T));
        }
        public static int IndexOf<T>(this IEnumerable<T> e, Func<T, int, bool> predicate, int skipCount = 0)
        {
            predicate.RequireNotNull("predicate");
            skipCount.RequireMinValue(0, "skipCount");

            if (e == null) return -1;

            int found = 0;
            int pos = 0;
            foreach (var t in e)
            {
                if (predicate(t, pos))
                {
                    found++;
                    if (found > skipCount) return pos;
                }
                pos++;
            }
            return -1;
        }
        public static int LastIndexOf<T>(this IEnumerable<T> e, Func<T, int, bool> predicate, int skipCount = 0)
        {
            predicate.RequireNotNull("predicate");
            skipCount.RequireMinValue(0, "skipCount");

            if (e == null) return -1;

            var pos = e.Reverse().IndexOf(predicate, skipCount);
            if (pos == -1) return -1;
            return e.Count() - pos - 1;
        }

        #endregion

        #region [ SameSetAs / SameSequenceAs ]

        public static bool SameSetAs<T>(this IEnumerable<T> e, IEnumerable<T> other)
        {
            return e.SameSetAs(other, EqualityComparer<T>.Default);
        }
        public static bool SameSetAs<T>(this IEnumerable<T> e, IEnumerable<T> other, IEqualityComparer<T> comparer)
        {
            var a1 = e.ToArray();
            var a2 = other.ToArray();
            int length = a1.Length;
            if (length != a2.Length) return false;
            if (length <= 8)
                for (int i = 0; i < length; i++)
                {
                    bool found = false;
                    for (int j = 0; j < length; j++)
                        if (comparer.Equals(a1[i], a2[j]))
                        {
                            found = true;
                            break;
                        }
                    if (!found) return false;
                }
            else
            {
                var set = new HashSet<T>(a1, comparer);
                for (int j = 0; j < length; j++)
                    if (!set.Contains(a2[j])) return false;
            }
            return true;
        }

        public static bool SameSequenceAs<T>(this IEnumerable<T> e, IEnumerable<T> other)
        {
            return e.SameSequenceAs(other, EqualityComparer<T>.Default);
        }
        public static bool SameSequenceAs<T>(this IEnumerable<T> e, IEnumerable<T> other, IEqualityComparer<T> comparer)
        {
            if (e is ICollection && other is ICollection && ((ICollection)e).Count != ((ICollection)other).Count)
                return false;

            var seq1 = e.GetEnumerator();
            var seq2 = other.GetEnumerator();

            while (true)
            {
                var m1 = seq1.MoveNext();
                var m2 = seq2.MoveNext();
                if (m1 != m2)
                    return false;
                if (!m1) break;
                if (!comparer.Equals(seq1.Current, seq2.Current))
                    return false;
            }
            return true;
        }

        #endregion

        #region [ Distinct ]

        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc)
        {
            return Distinct(e, keyFunc, EqualityComparer<TKey>.Default);
        }
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IEqualityComparer<TKey> comparer)
        {
            var set = new HashSet<TKey>(comparer);
            foreach (var source in e)
            {
                var key = keyFunc(source);
                if (set.Contains(key)) continue;
                yield return source;
                set.Add(key);
            }
        }
        public static bool AreDistinct<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc)
        {
            return e.AreDistinct(keyFunc, EqualityComparer<TKey>.Default);
        }
        public static bool AreDistinct<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IEqualityComparer<TKey> comparer)
        {
            keyFunc.RequireNotNull("keyFunc");
            comparer.RequireNotNull("comparer");

            var set = new HashSet<TKey>(comparer);
            foreach (var source in e)
            {
                var key = keyFunc(source);
                if (set.Contains(key)) return false;
                set.Add(key);
            }
            return true;
        }

        public static bool AreDistinct<TSource>(this IEnumerable<TSource> e)
        {
            return AreDistinct(e, EqualityComparer<TSource>.Default);
        }

        public static bool AreDistinct<TSource>(this IEnumerable<TSource> e, IEqualityComparer<TSource> comparer)
        {
            comparer.RequireNotNull("comparer");

            var set = new HashSet<TSource>(comparer);
            foreach (var source in e)
            {
                if (set.Contains(source)) return false;
                set.Add(source);
            }
            return true;
        }

        public static bool HasDuplicates<T, TKey>(this IEnumerable<T> e, Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            return e.GetDuplicates(keySelector, comparer).Any();
        }
        public static bool HasDuplicates<T>(this IEnumerable<T> e, IEqualityComparer<T> comparer = null)
        {
            return e.GetDuplicates(comparer).Any();
        }
        public static IEnumerable<T> GetDuplicates<T, TKey>(this IEnumerable<T> e, Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            keySelector.RequireNotNull("keySelector");

            if (e == null) yield break;
            var set = new HashSet<TKey>(comparer ?? EqualityComparer<TKey>.Default);
            foreach (var item in e)
            {
                var key = keySelector(item);
                if (set.Contains(key))
                    yield return item;
                set.Add(key);
            }
        }
        public static IEnumerable<T> GetDuplicates<T>(this IEnumerable<T> e, IEqualityComparer<T> comparer = null)
        {
            return e.GetDuplicates(i => i, comparer);
        }

        public static IEnumerable<T> WhileNonDuplicated<T, TKey>(this IEnumerable<T> e, Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            keySelector.RequireNotNull("keySelector");

            if (e == null) yield break;
            var set = new HashSet<TKey>(comparer ?? EqualityComparer<TKey>.Default);
            foreach (var item in e)
            {
                var key = keySelector(item);
                if (set.Contains(key))
                    yield break;
                yield return item;
                set.Add(key);
            }
        }
        public static IEnumerable<T> WhileNonDuplicated<T>(this IEnumerable<T> e, IEqualityComparer<T> comparer = null)
        {
            return e.WhileNonDuplicated(a => a, comparer);
        }

        #endregion

        #region [ Min / Max ]

        #region [ MinItem / MinItemOr / MinItemOrDefault ]
        public static TSource MinItemOrDefault<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc)
        {
            return e.MinItemOr(keyFunc, default(TSource));
        }
        public static TSource MinItemOrDefault<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer)
        {
            return e.MinItemOr(keyFunc, comparer, default(TSource));
        }

        public static TSource MinItemOr<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, TSource defaultValue)
        {
            return e.MinItemOr(keyFunc, Comparer<TKey>.Default, defaultValue);
        }
        public static TSource MinItemOr<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, TSource defaultValue)
        {
            TSource value;
            if (e.MinItemInternal(keyFunc, comparer, out value))
                return value;
            return defaultValue;
        }

        public static TSource MinItem<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc)
        {
            return e.MinItem(keyFunc, true);
        }
        public static TSource MinItem<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer)
        {
            return e.MinItem(keyFunc, comparer, true);
        }

        public static TSource MinItem<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, bool throwIfEmpty)
        {
            return e.MinItem(keyFunc, Comparer<TKey>.Default, throwIfEmpty);
        }
        public static TSource MinItem<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, bool throwIfEmpty)
        {
            TSource value;
            Require.Condition(
              e.MinItemInternal(keyFunc, comparer, out value) || !throwIfEmpty,
              "e",
              Resources.ExMsg_EnumerableExtensions_EmptyCollectionNoMinException
              );
            return value;
        }
        private static bool MinItemInternal<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, out TSource value)
        {
            TSource minItem = default(TSource);
            TKey minKey = default(TKey);
            bool firstTime = true;
            foreach (var source in e)
            {
                var key = keyFunc(source);
                if (firstTime || comparer.Compare(key, minKey) < 0)
                {
                    minKey = key;
                    minItem = source;
                }
                firstTime = false;
            }
            value = minItem;
            if (firstTime)
                return false;
            return true;
        }
        #endregion

        #region [ Min / MinOr / MinOrDefault ]
        public static TKey MinOrDefault<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc)
        {
            return e.MinOr(keyFunc, default(TKey));
        }
        public static TKey MinOrDefault<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer)
        {
            return e.MinOr(keyFunc, comparer, default(TKey));
        }

        public static TKey MinOr<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, TKey defaultValue)
        {
            return e.MinOr(keyFunc, Comparer<TKey>.Default, defaultValue);
        }
        public static TKey MinOr<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, TKey defaultValue)
        {
            TKey value;
            if (e.MinInternal(keyFunc, comparer, out value))
                return value;
            return defaultValue;
        }

        public static TKey Min<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc)
        {
            return e.Min(keyFunc, true);
        }
        public static TKey Min<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer)
        {
            return e.Min(keyFunc, comparer, true);
        }

        public static TKey Min<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, bool throwIfEmpty)
        {
            return e.Min(keyFunc, Comparer<TKey>.Default, throwIfEmpty);
        }
        public static TKey Min<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, bool throwIfEmpty)
        {
            TKey value;
            Require.Condition(
              e.MinInternal(keyFunc, comparer, out value) || !throwIfEmpty,
              "e",
              Resources.ExMsg_EnumerableExtensions_EmptyCollectionNoMinException
              );
            return value;
        }
        private static bool MinInternal<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, out TKey key)
        {
            TSource minItem;
            if (MinItemInternal(e, keyFunc, comparer, out minItem))
            {
                key = keyFunc(minItem);
                return true;
            }
            key = default(TKey);
            return false;
        }
        #endregion

        #region [ MaxItem / MaxItemOr / MaxItemOrDefault ]
        public static TSource MaxItemOrDefault<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc)
        {
            return e.MaxItemOr(keyFunc, default(TSource));
        }
        public static TSource MaxItemOrDefault<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer)
        {
            return e.MaxItemOr(keyFunc, comparer, default(TSource));
        }

        public static TSource MaxItemOr<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, TSource defaultValue)
        {
            return e.MaxItemOr(keyFunc, Comparer<TKey>.Default, defaultValue);
        }
        public static TSource MaxItemOr<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, TSource defaultValue)
        {
            TSource value;
            if (e.MaxItemInternal(keyFunc, comparer, out value))
                return value;
            return defaultValue;
        }

        public static TSource MaxItem<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc)
        {
            return e.MaxItem(keyFunc, true);
        }
        public static TSource MaxItem<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer)
        {
            return e.MaxItem(keyFunc, comparer, true);
        }

        public static TSource MaxItem<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, bool throwIfEmpty)
        {
            return e.MaxItem(keyFunc, Comparer<TKey>.Default, throwIfEmpty);
        }
        public static TSource MaxItem<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, bool throwIfEmpty)
        {
            return e.MinItem(keyFunc, new InverseComparer<TKey>(comparer), throwIfEmpty);
        }
        private static bool MaxItemInternal<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, out TSource value)
        {
            return e.MinItemInternal(keyFunc, new InverseComparer<TKey>(comparer), out value);
        }
        #endregion

        #region [ Max / MaxOr / MaxOrDefault ]
        public static TKey MaxOrDefault<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc)
        {
            return e.MaxOr(keyFunc, default(TKey));
        }
        public static TKey MaxOrDefault<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer)
        {
            return e.MaxOr(keyFunc, comparer, default(TKey));
        }

        public static TKey MaxOr<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, TKey defaultValue)
        {
            return e.MaxOr(keyFunc, Comparer<TKey>.Default, defaultValue);
        }
        public static TKey MaxOr<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, TKey defaultValue)
        {
            TKey value;
            if (e.MaxInternal(keyFunc, comparer, out value))
                return value;
            return defaultValue;
        }

        public static TKey Max<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc)
        {
            return e.Max(keyFunc, true);
        }
        public static TKey Max<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer)
        {
            return e.Max(keyFunc, comparer, true);
        }

        public static TKey Max<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, bool throwIfEmpty)
        {
            return e.Max(keyFunc, Comparer<TKey>.Default, throwIfEmpty);
        }
        public static TKey Max<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, bool throwIfEmpty)
        {
            return e.Min(keyFunc, new InverseComparer<TKey>(comparer), throwIfEmpty);
        }
        private static bool MaxInternal<TSource, TKey>(this IEnumerable<TSource> e, Func<TSource, TKey> keyFunc, IComparer<TKey> comparer, out TKey value)
        {
            return e.MinInternal(keyFunc, new InverseComparer<TKey>(comparer), out value);
        }
        #endregion

        #endregion

        #region [ WithIndex ]
        public static IEnumerable<Tup<T, int>> Indexed<T>(this IEnumerable<T> e)
        {
            return e.Select((t, i) => Tup.Create(t, i));
        }
        #endregion

        #region [ Unfold ]

        public static IEnumerable<T> Unfold<T>(this T t, Func<T, T> unfoldOneLevel, Predicate<T> stopCondition)
        {
            unfoldOneLevel.RequireNotNull("unfoldOneLevel");
            stopCondition.RequireNotNull("stopCondition");

            while (!stopCondition(t))
            {
                yield return t;
                t = unfoldOneLevel(t);
            }
        }
        public static IEnumerable<T> Unfold<T>(this T t, Func<T, T> unfoldOneLevel, T stopCondition, IEqualityComparer<T> comparer)
        {
            return t.Unfold(unfoldOneLevel, obj => comparer.Equals(stopCondition, obj));
        }
        public static IEnumerable<T> Unfold<T>(this T t, Func<T, T> unfoldOneLevel, T stopCondition)
        {
            return t.Unfold(unfoldOneLevel, stopCondition, EqualityComparer<T>.Default);
        }
        public static IEnumerable<T> Unfold<T>(this T t, Func<T, T> unfoldOneLevel)
        {
            return t.Unfold(unfoldOneLevel, default(T));
        }

        #endregion

        #region [ At / IndexOf / LastIndexOf ]

        public static T At<T>(this IEnumerable<T> e, int index)
        {
            e.RequireNotNull("e");

            index.RequireMinValue(0, "index");

            var c = 0;
            foreach (var t in e)
                if (c++ == index)
                    return t;

            index.RequireMaxValue(c - 1, "index");
            throw new NotSupportedException();
        }

        public static int IndexOf<T>(this IEnumerable<T> e, T value, IEqualityComparer<T> comparer = null, int skipCount = 0)
        {
            e.RequireNotNull("e");

            if (comparer == null) comparer = EqualityComparer<T>.Default;
            return e.IndexOf(t => comparer.Equals(value, t));
        }
        public static int IndexOf<T>(this IEnumerable<T> e, Func<T, bool> predicate, int skipCount = 0)
        {
            predicate.RequireNotNull("predicate");
            skipCount.RequireMinValue(0, "skipCount");

            if (e == null) return -1;

            return e.IndexOf((t, _) => predicate(t));
        }

        public static int LastIndexOf<T>(this IEnumerable<T> e, T value, IEqualityComparer<T> comparer = null, int skipCount = 0)
        {
            e.RequireNotNull("e");

            if (comparer == null) comparer = EqualityComparer<T>.Default;
            return e.LastIndexOf(t => comparer.Equals(value, t));
        }
        public static int LastIndexOf<T>(this IEnumerable<T> e, Func<T, bool> predicate, int skipCount = 0)
        {
            predicate.RequireNotNull("predicate");
            skipCount.RequireMinValue(0, "skipCount");

            if (e == null) return -1;

            return e.LastIndexOf((t, _) => predicate(t));
        }
        #endregion

        #region [ Graph Searching ]
        public static bool IsForest<T>(this T root, Func<T, IEnumerable<T>> getRelated)
        {
            return root.Singleton().IsForest(getRelated);
        }
        public static bool IsForest<T>(this T root, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer)
        {
            return root.Singleton().IsForest(getRelated, comparer);
        }
        public static bool IsForest<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated)
        {
            return roots.IsForest(getRelated, EqualityComparer<T>.Default);
        }
        public static bool IsForest<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer)
        {
            return roots.DepthFirstSearchAll(getRelated, comparer, true).Where(r => r.IsArc).All(r => !r.IsTargetNodeVisited);
        }

        public static bool CyclesBack<T>(this T root, Func<T, IEnumerable<T>> getRelated)
        {
            return root.CyclesBack(getRelated, EqualityComparer<T>.Default);
        }
        public static bool CyclesBack<T>(this T root, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer)
        {
            return root.DepthFirstSearchAll(getRelated, comparer, true).
              Where(r => r.IsArc).
              Any(r => comparer.Equals(r.TargetNode, root));
        }
        public static bool CyclesBack<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated)
        {
            return roots.CyclesBack(getRelated, EqualityComparer<T>.Default);
        }
        public static bool CyclesBack<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer)
        {
            var rootsSet = new HashSet<T>(roots, comparer);
            return roots.DepthFirstSearchAll(getRelated, comparer, true).
              Where(r => r.IsArc).
              Any(r => rootsSet.Contains(r.TargetNode));
        }

        public static IEnumerable<T> BreadthFirstSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated)
        {
            return root.Singleton().BreadthFirstSearch(getRelated);
        }
        public static IEnumerable<T> BreadthFirstSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated, bool includeRoot)
        {
            return root.Singleton().BreadthFirstSearch(getRelated, includeRoot);
        }
        public static IEnumerable<T> BreadthFirstSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer)
        {
            return root.Singleton().BreadthFirstSearch(getRelated, comparer);
        }
        public static IEnumerable<T> BreadthFirstSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoot)
        {
            return root.Singleton().BreadthFirstSearch(getRelated, comparer, includeRoot);
        }
        public static IEnumerable<T> BreadthFirstSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated)
        {
            return BreadthFirstSearch<T>(roots, getRelated, true);
        }
        public static IEnumerable<T> BreadthFirstSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, bool includeRoots)
        {
            return BreadthFirstSearch<T>(roots, getRelated, EqualityComparer<T>.Default, includeRoots);
        }
        public static IEnumerable<T> BreadthFirstSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer)
        {
            return BreadthFirstSearch<T>(roots, getRelated, comparer, true);
        }
        public static IEnumerable<T> BreadthFirstSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoots)
        {
            return roots.BreadthFirstSearchAll(getRelated, comparer, includeRoots).
              Where(res => res.IsNode).
              Select(res => res.Node);
        }
        public static IEnumerable<SearchingResult<T>> BreadthFirstSearchAll<T>(this T root, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoot)
        {
            return root.Singleton().BreadthFirstSearchAll(getRelated, comparer, includeRoot);
        }
        public static IEnumerable<SearchingResult<T>> BreadthFirstSearchAll<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoots)
        {
            getRelated.RequireNotNull("getRelated");

            if (roots == null) yield break;
            if (comparer == null) comparer = EqualityComparer<T>.Default;
            var queue = new Queue<T>();
            var hashSet = new HashSet<T>(comparer);
            foreach (var t in roots)
            {
                if (hashSet.Contains(t)) continue;
                var result = new SearchingResult<T>(t, hashSet.Contains);
                if (includeRoots)
                    yield return result;
                if (!result.PruneSearch)
                    queue.Enqueue(t);
                hashSet.Add(t);
            }
            while (queue.Count > 0)
            {
                var t = queue.Dequeue();
                var related = getRelated(t);
                if (related != null)
                    foreach (var t1 in related)
                    {
                        bool isVisited = hashSet.Contains(t1);
                        yield return new SearchingResult<T>(t, t1, isVisited, hashSet.Contains);
                        if (isVisited) continue;
                        var result = new SearchingResult<T>(t1, hashSet.Contains);
                        yield return result;
                        if (!result.PruneSearch)
                            queue.Enqueue(t1);
                        hashSet.Add(t1);
                    }
            }
        }

        public static IEnumerable<T> DepthFirstSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated)
        {
            return root.Singleton().DepthFirstSearch(getRelated);
        }
        public static IEnumerable<T> DepthFirstSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated, bool includeRoot)
        {
            return root.Singleton().DepthFirstSearch(getRelated, includeRoot);
        }
        public static IEnumerable<T> DepthFirstSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer)
        {
            return root.Singleton().DepthFirstSearch(getRelated, comparer);
        }
        public static IEnumerable<T> DepthFirstSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoot)
        {
            return root.Singleton().DepthFirstSearch(getRelated, comparer, includeRoot);
        }
        public static IEnumerable<T> DepthFirstSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated)
        {
            return DepthFirstSearch<T>(roots, getRelated, true);
        }
        public static IEnumerable<T> DepthFirstSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, bool includeRoots)
        {
            return DepthFirstSearch(roots, getRelated, EqualityComparer<T>.Default, includeRoots);
        }
        public static IEnumerable<T> DepthFirstSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer)
        {
            return DepthFirstSearch<T>(roots, getRelated, comparer, true);
        }
        public static IEnumerable<T> DepthFirstSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoots)
        {
            return roots.DepthFirstSearchAll(getRelated, comparer, includeRoots).
              Where(res => res.IsNode).
              Select(res => res.Node);
        }
        public static IEnumerable<SearchingResult<T>> DepthFirstSearchAll<T>(this T root, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoots)
        {
            return root.Singleton().DepthFirstSearchAll(getRelated, comparer, includeRoots);
        }
        public static IEnumerable<SearchingResult<T>> DepthFirstSearchAll<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoots)
        {
            getRelated.RequireNotNull("getRelated");

            if (roots == null) return Enumerable.Empty<SearchingResult<T>>();
            if (comparer == null) comparer = EqualityComparer<T>.Default;
            var hashSet = new HashSet<T>(comparer);
            return DepthFirstSearch(roots, getRelated, hashSet, includeRoots);
        }
        private static IEnumerable<SearchingResult<T>> DepthFirstSearch<T>(IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, ICollection<T> hashSet, bool includeRoots)
        {
            return roots.SelectMany(root => DepthFirstSearch(root, getRelated, hashSet, includeRoots));
        }
        private static IEnumerable<SearchingResult<T>> DepthFirstSearch<T>(T root, Func<T, IEnumerable<T>> getRelated, ICollection<T> hashSet, bool includeRoots)
        {
            return DepthSearch(root, getRelated, hashSet, includeRoots, true);
        }
        private static IEnumerable<SearchingResult<T>> DepthSearch<T>(T root, Func<T, IEnumerable<T>> getRelated, ICollection<T> hashSet, bool includeRoots, bool visitRootFirst)
        {
            //if (hashSet.Contains(root)) yield break;
            var result = new SearchingResult<T>(root, hashSet.Contains);
            if (visitRootFirst && includeRoots)
            {
                yield return result;
                if (result.PruneSearch)
                    yield break;
            }
            hashSet.Add(root);
            var related = getRelated(root);
            if (related != null)
                foreach (var t in related)
                {
                    bool isVisited = hashSet.Contains(t);
                    yield return new SearchingResult<T>(root, t, isVisited, hashSet.Contains);
                    if (!isVisited)
                        foreach (var res in DepthSearch(t, getRelated, hashSet, true, visitRootFirst))
                            yield return res;
                }
            if (!visitRootFirst && includeRoots)
                yield return result;
        }

        public static IEnumerable<T> DepthLastSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated)
        {
            return root.Singleton().DepthLastSearch(getRelated);
        }
        public static IEnumerable<T> DepthLastSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated, bool includeRoot)
        {
            return root.Singleton().DepthLastSearch(getRelated, includeRoot);
        }
        public static IEnumerable<T> DepthLastSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer)
        {
            return root.Singleton().DepthLastSearch(getRelated, comparer);
        }
        public static IEnumerable<T> DepthLastSearch<T>(this T root, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoot)
        {
            return root.Singleton().DepthLastSearch(getRelated, comparer, includeRoot);
        }
        public static IEnumerable<T> DepthLastSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated)
        {
            return DepthLastSearch<T>(roots, getRelated, true);
        }
        public static IEnumerable<T> DepthLastSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, bool includeRoots)
        {
            return DepthLastSearch(roots, getRelated, EqualityComparer<T>.Default, includeRoots);
        }
        public static IEnumerable<T> DepthLastSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer)
        {
            return DepthLastSearch<T>(roots, getRelated, comparer, true);
        }
        public static IEnumerable<T> DepthLastSearch<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoots)
        {
            return DepthLastSearchAll(roots, getRelated, comparer, includeRoots).Where(r => r.IsNode).Select(r => r.Node);
        }
        public static IEnumerable<SearchingResult<T>> DepthLastSearchAll<T>(this T root, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoots)
        {
            return root.Singleton().DepthLastSearchAll(getRelated, comparer, includeRoots);
        }
        public static IEnumerable<SearchingResult<T>> DepthLastSearchAll<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, IEqualityComparer<T> comparer, bool includeRoots)
        {
            getRelated.RequireNotNull("getRelated");

            if (roots == null) return Enumerable.Empty<SearchingResult<T>>();
            if (comparer == null) comparer = EqualityComparer<T>.Default;
            var hashSet = new HashSet<T>(comparer);
            return DepthLastSearch(roots, getRelated, hashSet, includeRoots);
        }
        private static IEnumerable<SearchingResult<T>> DepthLastSearch<T>(IEnumerable<T> roots, Func<T, IEnumerable<T>> getRelated, ICollection<T> hashSet, bool includeRoots)
        {
            return roots.SelectMany(root => DepthSearch(root, getRelated, hashSet, includeRoots, false));
        }

        public static IEnumerable<T> TopologicalOrder<T>(this IEnumerable<T> elements, Func<T, IEnumerable<T>> getDependencies, Func<T, IEnumerable<T>> getDependants, IEqualityComparer<T> comparer)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;
            var set = new HashSet<T>(comparer);
            var list = new List<T>();
            foreach (var elem in elements)
                foreach (var e in elem.DepthLastSearch(getDependants, comparer).Where(x => !set.Contains(x)))
                {
                    set.Add(e);
                    list.Add(e);
                }

            set.Clear();
            foreach (var elem in list)
                foreach (var e in elem.DepthLastSearch(getDependencies, comparer).Where(x => !set.Contains(x)))
                {
                    set.Add(e);
                    yield return e;
                }

        }
        public static IEnumerable<T> TopologicalOrder<T>(this IEnumerable<T> elements, Func<T, IEnumerable<T>> getDependencies, Func<T, IEnumerable<T>> getDependants)
        {
            return elements.TopologicalOrder(getDependencies, getDependants, EqualityComparer<T>.Default);
        }
        public static IEnumerable<T> TopologicalOrder<T>(this IEnumerable<T> elements, Func<T, T, bool> firstDependsOnSecondFunc, IEqualityComparer<T> comparer)
        {
            return elements.TopologicalOrder(
              elem => elements.Where(e => firstDependsOnSecondFunc(elem, e)),
              elem => elements.Where(e => firstDependsOnSecondFunc(e, elem)),
              comparer
              );
        }
        public static IEnumerable<T> TopologicalOrder<T>(this IEnumerable<T> elements, Func<T, T, bool> firstDependsOnSecondFunc)
        {
            return elements.TopologicalOrder(firstDependsOnSecondFunc, EqualityComparer<T>.Default);
        }

        private static Regex treeRegex = new Regex(@"(?'LParen'\()|(?'RParen'\))|(?'Str'[^\(\)\s,]+)",
                                                   RegexOptions.Compiled | RegexOptions.Singleline);
        private enum ForestTokenType { LParen, RParen, Value }
        public static IEnumerable<T> ParseForest<T>(string forestString, Func<string, IEnumerable<T>, T> generator)
        {
            var matches = treeRegex.Matches(forestString).Cast<Match>();
            var tokens = matches.Select(match
                                        => match.Groups["LParen"].Success
                                             ? Tup.Create(ForestTokenType.LParen, match.Value)
                                             : (match.Groups["RParen"].Success
                                                  ? Tup.Create(ForestTokenType.RParen, match.Value)
                                                  : Tup.Create(ForestTokenType.Value, match.Value)
                                               )
              ).ToList();
            return TryParseForest(tokens, false, generator);
        }

        private static IEnumerable<T> TryParseForest<T>(IEnumerable<Tup<ForestTokenType, string>> tokens, bool lParenMatched, Func<string, IEnumerable<T>, T> generator)
        {
            var enumerator = tokens.GetEnumerator();
            while (true)
            {
                T tree;
                if (TryParseForest(enumerator, false, generator, out tree))
                    yield return tree;
                else break;
            }
        }
        private static bool TryParseForest<T>(IEnumerator<Tup<ForestTokenType, string>> tokens, bool lParenMatched, Func<string, IEnumerable<T>, T> generator, out T tree)
        {
            tree = default(T);
            if (!lParenMatched)
            {
                if (!tokens.MoveNext())
                    return false; // unexpected EOL
                if (tokens.Current.Item1 == ForestTokenType.LParen)
                    lParenMatched = true;
                else
                    if (tokens.Current.Item1 == ForestTokenType.Value)
                    {
                        var childValue = tokens.Current.Item2;
                        tree = generator(childValue, new T[0]);
                        return true;
                    }
                    else
                        return false; // Unexpected RParen
            }
            //if (!lParenMatched && (!tokens.MoveNext() || tokens.Current.First != ForestTokenType.LParen)) 
            //  return false; // LParen expected
            if (!tokens.MoveNext() || tokens.Current.Item1 != ForestTokenType.Value)
                return false; // value expected
            var rootValue = tokens.Current.Item2;
            var children = new List<T>();
            while (tokens.MoveNext())
            {
                bool breakLoop = false;
                switch (tokens.Current.Item1)
                {
                    case ForestTokenType.LParen:
                        {
                            T child;
                            if (!TryParseForest(tokens, true, generator, out child))
                                return false;
                            children.Add(child);
                        }
                        break;
                    case ForestTokenType.RParen:
                        breakLoop = true;
                        break;
                    case ForestTokenType.Value:
                        {
                            var childValue = tokens.Current.Item2;
                            T child = generator(childValue, new T[0]);
                            children.Add(child);
                        }
                        break;
                }
                if (breakLoop) break;
            }
            tree = generator(rootValue, children);
            return true;
        }

        public static IEnumerable<PathStep<T, TArc>> FindShortestPath<T, TArc>(this T root, Func<T, IEnumerable<Tup<T, TArc, T>>> getArcs, Predicate<T> foundCondition, IEqualityComparer<T> comparer = null)
        {
            return root.Singleton().FindShortestPath(getArcs, foundCondition, comparer);
        }
        public static IEnumerable<PathStep<T, TArc>> FindShortestPath<T, TArc>(this IEnumerable<T> roots, Func<T, IEnumerable<Tup<T, TArc, T>>> getArcs, Predicate<T> foundCondition, IEqualityComparer<T> comparer = null)
        {
            getArcs.RequireNotNull("getArcs");
            foundCondition.RequireNotNull("foundCondition");

            if (roots == null) return null;
            if (comparer == null) comparer = EqualityComparer<T>.Default;
            var queue = new Queue<FSPNode<T, TArc>>();
            var hashSet = new HashSet<T>(comparer);
            foreach (var t in roots)
            {
                if (hashSet.Contains(t)) continue;
                queue.Enqueue(new FSPNode<T, TArc>(t));
                hashSet.Add(t);
            }
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (foundCondition(node.NodeValue))
                    return node.BuildPath().ToArray();
                var arcs = getArcs(node.NodeValue);
                if (arcs != null)
                    foreach (var arc in arcs)
                    {
                        bool isVisited = hashSet.Contains(arc.Item3);
                        if (isVisited) continue;
                        queue.Enqueue(new FSPNode<T, TArc>(arc.Item3, new FSPArc<T, TArc>(arc.Item2, node)));
                        hashSet.Add(arc.Item3);
                    }
            }
            return null;
        }

        public static IEnumerable<PathStep<T, TArc>> FindShortestPath<T, TArc>(this T root, Func<T, IEnumerable<Tup<T, TArc, T>>> getArcs, Func<T, double> getNodeCost, Func<TArc, double> getArcCost, Func<T, double> getEstimatedRemainingCost, Predicate<T> foundCondition, IEqualityComparer<T> comparer = null)
        {
            return root.Singleton().FindShortestPath(getArcs, getNodeCost, getArcCost, getEstimatedRemainingCost, foundCondition, comparer);
        }
        public static IEnumerable<PathStep<T, TArc>> FindShortestPath<T, TArc>(this IEnumerable<T> roots, Func<T, IEnumerable<Tup<T, TArc, T>>> getArcs, Func<T, double> getNodeCost, Func<TArc, double> getArcCost, Func<T, double> getEstimatedRemainingCost, Predicate<T> foundCondition, IEqualityComparer<T> comparer = null)
        {
            getArcs.RequireNotNull("getArcs");
            foundCondition.RequireNotNull("foundCondition");

            if (roots == null) return null;
            if (comparer == null) comparer = EqualityComparer<T>.Default;
            if (getNodeCost == null) getNodeCost = _ => 0.0;
            if (getArcCost == null) getArcCost = _ => 1.0;
            if (getEstimatedRemainingCost == null) getEstimatedRemainingCost = _ => 0.0;
            var queue = new AVLTree<double, FSPNode<T, TArc>>(Comparer<double>.Default, false);
            var hashSet = new HashSet<T>(comparer);
            foreach (var t in roots)
            {
                if (hashSet.Contains(t)) continue;
                var nodeCost = getNodeCost(t);
                var estimatedRemainingCost = getEstimatedRemainingCost(t);
                var node = new FSPNode<T, TArc>(t) { NodeCost = nodeCost, AccumCost = nodeCost, EstimatedRemainingCost = estimatedRemainingCost };
                if (foundCondition(node.NodeValue))
                    return node.BuildPath().ToArray();
                queue.Push(KeyValuePair.Create(node.AccumCost + node.EstimatedRemainingCost, node));
                hashSet.Add(t);
            }
            while (queue.Count > 0)
            {
                var pair = queue.Pop();
                var node = pair.Value;
                var arcs = getArcs(node.NodeValue);
                if (arcs != null)
                    foreach (var tup in arcs)
                    {
                        var arc = tup.Item2;
                        var target = tup.Item3;
                        bool isVisited = hashSet.Contains(target);
                        var arcCost = getArcCost(arc);
                        var nodeCost = getNodeCost(target);
                        var estimCost = getEstimatedRemainingCost(target);
                        var totalCost = node.AccumCost + arcCost + nodeCost + estimCost;
                        if (isVisited)
                        {
                            queue.ChangePriority(
                              p => comparer.Equals(p.Value.NodeValue, node.NodeValue),
                              (ref KeyValuePair<double, FSPNode<T, TArc>> p) =>
                              {
                                  p = KeyValuePair.Create(totalCost, p.Value);
                                  return ContinueChangePriorityAction.Continue;
                              });
                        }
                        var newArc = new FSPArc<T, TArc>(arc, node) { ArcCost = arcCost, AccumCost = node.AccumCost + arcCost };
                        var newNode = new FSPNode<T, TArc>(target, newArc) { NodeCost = nodeCost, AccumCost = newArc.AccumCost + nodeCost, EstimatedRemainingCost = estimCost };
                        if (foundCondition(newNode.NodeValue))
                            return newNode.BuildPath().ToArray();
                        queue.Push(KeyValuePair.Create(newNode.AccumCost + newNode.EstimatedRemainingCost, newNode));
                        hashSet.Add(target);
                    }
            }
            return null;
        }

        public class SearchingResult<T>
        {
            public SearchingResult(T node, Func<T, bool> isVisited)
            {
                Node = node;
                IsVisited = isVisited;
                IsNode = true;
            }

            public SearchingResult(T node, T targetNode, bool isTargetNodeVisited, Func<T, bool> isVisited)
            {
                Node = node;
                TargetNode = targetNode;
                IsNode = false;
                IsVisited = isVisited;
                IsTargetNodeVisited = isTargetNodeVisited;
            }

            public Func<T, bool> IsVisited { get; private set; }

            public bool IsNode { get; private set; }
            public bool IsArc { get { return !IsNode; } }

            public T Node { get; private set; }
            public T TargetNode { get; private set; }

            public bool IsTargetNodeVisited { get; private set; }

            public bool PruneSearch { get; set; }
        }
        public class PathStep<T, TArc>
        {
            public PathStep(T from, TArc arc, T to)
            {
                From = from;
                Arc = arc;
                To = to;
            }

            public T From { get; private set; }

            public TArc Arc { get; private set; }

            public T To { get; private set; }
        }

        class FSPNode<T, TArc>
        {
            public FSPNode(T value, FSPArc<T, TArc> fromArc = null)
            {
                NodeValue = value;
                FromArc = fromArc;
            }
            public T NodeValue;
            public FSPArc<T, TArc> FromArc;
            public double NodeCost;
            public double AccumCost;
            public double EstimatedRemainingCost;

            public IEnumerable<PathStep<T, TArc>> BuildPath()
            {
                if (FromArc == null || FromArc.FromNode == null)
                    return Enumerable.Empty<PathStep<T, TArc>>();
                return Seq.Build(FromArc.FromNode.BuildPath(), new PathStep<T, TArc>(FromArc.FromNode.NodeValue, FromArc.ArcValue, NodeValue));
            }
        }
        class FSPArc<T, TArc>
        {
            public FSPArc(TArc value, FSPNode<T, TArc> fromNode = null)
            {
                ArcValue = value;
                FromNode = fromNode;
            }
            public TArc ArcValue;
            public FSPNode<T, TArc> FromNode;
            public double ArcCost;
            public double AccumCost;
        }

        #endregion

        #region [ Do/ForEach ]

        [DebuggerStepThrough]
        public static IEnumerable<T> Do<T>(this IEnumerable<T> e, Action<T> action)
        {
            action.RequireNotNull("action");

            if (e == null) yield break;
            foreach (var t in e)
            {
                action(t);
                yield return t;
            }
        }
        [DebuggerStepThrough]
        public static IEnumerable<T> Do<T>(this IEnumerable<T> e, Action<T, int> action)
        {
            action.RequireNotNull("action");

            if (e == null) yield break;
            int pos = 0;
            foreach (var t in e)
            {
                action(t, pos);
                yield return t;
                pos++;
            }
        }
        [DebuggerStepThrough]
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> e, Action<T> action)
        {
            if (e == null) return null;
            foreach (var t in e.Do(action)) { }
            return e;
        }
        [DebuggerStepThrough]
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> e, Action<T, int> action)
        {
            if (e == null) return null;
            foreach (var t in e.Do(action)) { }
            return e;
        }

        #endregion

        #region [ Indexed Methods ]

        public static bool Any<T>(IEnumerable<T> e, Func<T, int, bool> predicate)
        {
            e.RequireNotNull("e");
            predicate.RequireNotNull("predicate");

            int pos = 0;
            foreach (var t in e)
            {
                if (predicate(t, pos))
                    return true;
                pos++;
            }
            return false;
        }
        public static bool All<T>(IEnumerable<T> e, Func<T, int, bool> predicate)
        {
            e.RequireNotNull("e");
            predicate.RequireNotNull("predicate");

            int pos = 0;
            foreach (var t in e)
            {
                if (!predicate(t, pos))
                    return false;
                pos++;
            }
            return true;
        }
        #endregion

        #region [ ToStringList ]

        public static string ToString(this IEnumerable<char> e)
        {
            if (e == null) return "";
            return new string(e.ToArray());
        }
        public static string ToStringList<T>(this IEnumerable<T> e, string separator, Func<T, string> toStringFunc)
        {
            if (e == null) return "";
            return e.ToStringList(separator, toStringFunc, Resources.EnumerableExtensions_Periods, int.MaxValue);
        }
        public static string ToStringList<T>(this IEnumerable<T> e, string separator, Func<T, string> toStringFunc, string etcetera, int maxElementCount)
        {
            return e.ToStringList(separator, toStringFunc, etcetera, maxElementCount, "", "", null);
        }
        public static string ToStringList<T>(this IEnumerable<T> e, string separator)
        {
            if (e == null) return "";
            return ToStringList(e, separator, t => "" + t);
        }
        public static string ToStringList<T>(this IEnumerable<T> e, Func<T, string> toStringFunc)
        {
            if (e == null) return "";
            var separator = Thread.CurrentThread.CurrentUICulture.TextInfo.ListSeparator + " ";
            return ToStringList(e, separator, toStringFunc);
        }
        public static string ToStringList<T>(this IEnumerable<T> e)
        {
            if (e == null) return "";
            var separator = Thread.CurrentThread.CurrentUICulture.TextInfo.ListSeparator + " ";
            return ToStringList(e, separator, t => "" + t);
        }
        //public static string ToStringList<T>(this IEnumerable<T> e, string separator, Func<T, string> toStringFunc, string etcetera, int maxElementCount, string opening, string closing, string empty)
        //{
        //  if (maxElementCount < 0) maxElementCount = int.MaxValue;
        //  var sb = new StringBuilder();
        //  if (e != null)
        //    foreach (var t in e)
        //    {
        //      sb.AppendIfNotEmpty(separator);
        //      if (maxElementCount-- <= 0)
        //      {
        //        sb.Append(etcetera);
        //        break;
        //      }
        //      sb.Append(toStringFunc(t));
        //    }
        //  if (sb.Length == 0 && empty != null)
        //    return empty;
        //  return opening + sb.Append(closing);
        //}
        public static string ToStringList<T>(this IEnumerable<T> e, string separator = null, Func<T, string> toStringFunc = null, string etcetera = "...", int maxElementCount = -1, string opening = "", string closing = "", string empty = "")
        {
            if (separator == null)
                separator = Thread.CurrentThread.CurrentUICulture.TextInfo.ListSeparator + " ";
            if (toStringFunc == null)
                toStringFunc = t => "" + t;

            if (maxElementCount < 0) maxElementCount = int.MaxValue;
            var sb = new StringBuilder();
            if (e != null)
                foreach (var t in e)
                {
                    sb.AppendIfNotEmpty(separator);
                    if (maxElementCount-- <= 0)
                    {
                        sb.Append(etcetera);
                        break;
                    }
                    sb.Append(toStringFunc(t));
                }
            if (sb.Length == 0 && empty != null)
                return empty;
            return opening + sb + closing;
        }

        #endregion

        #region [ ToEnumerable ]

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> e)
        {
            while (e.MoveNext())
                yield return e.Current;
        }
        public static IEnumerable ToEnumerable(this IEnumerator e)
        {
            while (e.MoveNext())
                yield return e.Current;
        }

        #endregion

        #region [ Merge ]

        public static IEnumerable Merge(this IEnumerable enumerable1, IEnumerable enumerable2, IComparer comparer, bool keepEqualsAsOne = false)
        {
            enumerable1.RequireNotNull("enumerable1");
            enumerable2.RequireNotNull("enumerable2");

            if (comparer == null) comparer = Comparer<object>.Default;
            IEnumerator e1 = enumerable1.GetEnumerator();
            IEnumerator e2 = enumerable1.GetEnumerator();
            bool m1 = e1.MoveNext();
            bool m2 = e2.MoveNext();
            while (m1 || m2)
            {
                if (!m1)
                {
                    yield return e2.Current;
                    m2 = e2.MoveNext();
                }
                else if (!m2)
                {
                    yield return e1.Current;
                    m1 = e1.MoveNext();
                }
                else
                {
                    int comp = comparer.Compare(e1.Current, e2.Current);
                    if (comp < 0)
                    {
                        yield return e1.Current;
                        m1 = e1.MoveNext();
                    }
                    else if (comp > 0)
                    {
                        yield return e2.Current;
                        m2 = e2.MoveNext();
                    }
                    else
                    {
                        if (!keepEqualsAsOne)
                            yield return e1.Current;
                        m1 = e1.MoveNext();
                    }
                }
            }
        }
        public static IEnumerable Merge(this IEnumerable enumerable1, IEnumerable enumerable2, bool keepEqualsAsOne = false)
        {
            enumerable1.RequireNotNull("enumerable1");
            enumerable2.RequireNotNull("enumerable2");

            return Merge(enumerable1, enumerable2, Comparer.Default, keepEqualsAsOne);
        }
        public static IEnumerable<T> Merge<T>(this IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, Comparison<T> comparison, bool keepEqualsAsOne = false)
        {
            enumerable1.RequireNotNull("enumerable1");
            enumerable2.RequireNotNull("enumerable2");
            comparison.RequireNotNull("comparison");

            IEnumerator<T> e1 = enumerable1.GetEnumerator();
            IEnumerator<T> e2 = enumerable2.GetEnumerator();
            bool m1 = e1.MoveNext();
            bool m2 = e2.MoveNext();
            while (m1 || m2)
            {
                if (!m1)
                {
                    yield return e2.Current;
                    m2 = e2.MoveNext();
                }
                else if (!m2)
                {
                    yield return e1.Current;
                    m1 = e1.MoveNext();
                }
                else
                {
                    int comp = comparison(e1.Current, e2.Current);
                    if (comp < 0)
                    {
                        yield return e1.Current;
                        m1 = e1.MoveNext();
                    }
                    else if (comp > 0)
                    {
                        yield return e2.Current;
                        m2 = e2.MoveNext();
                    }
                    else
                    {
                        if (!keepEqualsAsOne)
                            yield return e1.Current;
                        m1 = e1.MoveNext();
                    }
                }
            }
        }
        public static IEnumerable<T> Merge<T>(this IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, IComparer<T> comparer, bool keepEqualsAsOne = false)
        {
            enumerable1.RequireNotNull("enumerable1");
            enumerable2.RequireNotNull("enumerable2");

            if (comparer == null) comparer = Comparer<T>.Default;
            return Merge(enumerable1, enumerable2, (x, y) => comparer.Compare(x, y), keepEqualsAsOne);
        }
        public static IEnumerable<T> Merge<T>(this IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, bool keepEqualsAsOne = false)
        {
            enumerable1.RequireNotNull("enumerable1");
            enumerable2.RequireNotNull("enumerable2");

            return Merge(enumerable1, enumerable2, Comparer<T>.Default, keepEqualsAsOne);
        }

        #endregion

        #region [ Numeric Iterators ]
        public static IEnumerable<int> To(this int start, int end)
        {
            return start.To(end, 1);
        }
        public static IEnumerable<int> To(this int start, int end, int by)
        {
            if (by == 0) by = 1;
            if (by < 0) by = -by;
            if (start <= end)
                for (int i = start; i <= end; i += by)
                    yield return i;
            else
                for (int i = start; i >= end; i -= by)
                    yield return i;
        }
        public static IEnumerable<long> To(this long start, long end)
        {
            return start.To(end, 1);
        }
        public static IEnumerable<long> To(this long start, long end, long by)
        {
            if (by == 0) by = 1;
            if (by < 0) by = -by;
            if (start <= end)
                for (long i = start; i <= end; i += by)
                    yield return i;
            else
                for (long i = start; i >= end; i -= by)
                    yield return i;
        }
        public static IEnumerable<int> ToCount(this int start, int count)
        {
            for (int i = 0; i < count; i++)
                yield return start + i;
        }
        public static IEnumerable<long> ToCount(this long start, long count)
        {
            for (long i = 0; i < count; i++)
                yield return start + i;
        }
        #endregion

        #region [ SetOperations ]

        public static IEnumerable<T> LeftDifference<T>(this IEnumerable<T> e, IEnumerable<T> e2)
        {
            return LeftDifference(e, e2, t => t);
        }
        public static IEnumerable<T> LeftDifference<T>(this IEnumerable<T> e, IEnumerable<T> e2, IEqualityComparer<T> comparer)
        {
            return LeftDifference(e, e2, t => t, comparer);
        }
        public static IEnumerable<T> LeftDifference<T, K>(this IEnumerable<T> e, IEnumerable<T> e2, Func<T, K> getKeyFunc)
        {
            return LeftDifference(e, e2, getKeyFunc, EqualityComparer<K>.Default);
        }
        public static IEnumerable<T> LeftDifference<T, K>(this IEnumerable<T> e, IEnumerable<T> e2, Func<T, K> getKeyFunc, IEqualityComparer<K> keyComparer)
        {
            keyComparer.RequireNotNull("keyComparer");

            var dict = new HashSet<K>(keyComparer);
            e2.ForEach(t => dict.Add(getKeyFunc(t)));
            return e.Where(t => !dict.Contains(getKeyFunc(t)));
        }
        #endregion

        #region [ SubArraying ]

        public static IEnumerable<T> Subelements<T>(this IList<T> list, IEnumerable<int> indices)
        {
            list.RequireNotNull("list");
            indices.RequireNotNull("indices");

            return indices.Select(i => list[i]);
        }
        public static IEnumerable<T> Subelements<T>(this IList<T> list, int start, int count)
        {
            list.RequireNotNull("list");
            count.RequireMinValue(0, "count");
            start.RequireInRange(0, list.Count - count, "start");

            return list.Subelements(start.ToCount(count));
        }
        public static IEnumerable<T> Subelements<T>(this T[] array, IEnumerable<int> indices)
        {
            array.RequireNotNull("array");
            indices.RequireNotNull("indices");

            return indices.Select(i => array[i]);
        }
        public static IEnumerable<T> Subelements<T>(this T[] array, int start, int count)
        {
            array.RequireNotNull("array");
            count.RequireMinValue(0, "count");
            start.RequireInRange(0, array.Length - count, "start");

            return array.Subelements(start.ToCount(count));
        }

        public static void Split<T>(IEnumerable<T> collection, Predicate<T> predicate, out IEnumerable<T> compliant, out IEnumerable<T> uncompliant)
        {
            collection.RequireNotNull("collection");
            predicate.RequireNotNull("predicate");

            var compliantList = new List<T>();
            var uncompliantList = new List<T>();
            foreach (var elem in collection)
                if (predicate(elem))
                    compliantList.Add(elem);
                else
                    uncompliantList.Add(elem);
            compliant = compliantList;
            uncompliant = uncompliantList;
        }

        #endregion

        #region [ CopyTo ]
        public static void CopyTo<T>(this IEnumerable<T> e, T[] array, int arrayIndex)
        {
            e.Take(array.Length - arrayIndex).ForEach(t => array[arrayIndex++] = t);
        }
        #endregion

        #region [ OrderBy ]
        public static IOrderedEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, Comparison<TSource> comparison)
        {
            var comparer = new ComparisonComparer<TSource>(comparison);
            return source.OrderBy(e => e, comparer);
        }

        public static IOrderedEnumerable<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> source, Comparison<TSource> comparison)
        {
            var comparer = new ComparisonComparer<TSource>(comparison);
            return source.OrderByDescending(e => e, comparer);
        }
        #endregion

        #region [ RemoveWhere ]
        public static IEnumerable<T> RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> predicate, int maxCount = int.MaxValue)
        {
            collection.RequireNotNull("collection");
            predicate.RequireNotNull("predicate");
            maxCount.RequireMinValue(0, "maxCount");

            return RemoveWhere<T>(collection, (t, _) => predicate(t), maxCount);
        }
        public static IEnumerable<T> RemoveWhere<T>(this ICollection<T> collection, Func<T, int, bool> predicate, int maxCount = int.MaxValue)
        {
            collection.RequireNotNull("collection");
            predicate.RequireNotNull("predicate");
            maxCount.RequireMinValue(0, "maxCount");
            if (maxCount == 0)
                return Enumerable.Empty<T>();

            var removed = new List<T>();

            var list = collection as IList<T>;
            if (list != null)
            {
                var indices = new List<int>();
                for (int i = 0; i < list.Count; i++)
                {
                    var value = list[i];
                    if (predicate(value, i))
                    {
                        indices.Add(i);
                        if (indices.Count >= maxCount)
                            break;
                    }
                }
                if (indices.Count > 0)
                {
                    for (int i = 0; i < indices.Count; i++)
                    {
                        removed.Add(list[indices[i]]);
                        var start = indices[i] + 1;
                        var end = (i == indices.Count - 1 ? list.Count : indices[i + 1]) - 1;
                        for (int j = start; j <= end; j++)
                        {
                            list[j - (i + 1)] = list[j];
                        }
                    }

                    if (list is IList && ((IList)list).IsFixedSize)
                    {
                        for (int i = list.Count - indices.Count; i < list.Count; i++)
                        {
                            list[i] = default(T);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < indices.Count; i++)
                        {
                            list.RemoveAt(list.Count - 1);
                        }
                    }
                }
            }
            else
            {
                foreach (var item in collection.Indexed())
                    if (predicate(item.Item1, item.Item2))
                    {
                        removed.Add(item.Item1);
                        if (removed.Count >= maxCount)
                            break;
                    }

                foreach (T item in removed)
                    collection.Remove(item);
            }
            return removed;
        }

        #endregion

        #region [ FillRange ]
        /// <summary>
        /// Replaces each item in a part of a list with a given value.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The list to modify.</param>
        /// <param name="start">The index at which to start filling. The first index in the list has index 0.</param>
        /// <param name="count">The number of items to fill.</param>
        /// <param name="value">The value to fill with.</param>
        /// <exception cref="ArgumentException"><paramref name="list"/> is a read-only list.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/> or <paramref name="count"/> is negative, or 
        /// <paramref name="start"/> + <paramref name="count"/> is greater than <paramref name="list"/>.Count.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is null.</exception>
        public static void FillRange<T>(this IList<T> list, int start, int count, T value)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (list.IsReadOnly)
                throw new ArgumentException(@"List is read-only", "list");

            if (count == 0)
                return;
            if (start < 0 || start >= list.Count)
                throw new ArgumentOutOfRangeException("start");
            if (count < 0 || count > list.Count || start > list.Count - count)
                throw new ArgumentOutOfRangeException("count");

            for (int i = start; i < count + start; ++i)
            {
                list[i] = value;
            }
        }
        #endregion
    }
}
