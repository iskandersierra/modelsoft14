using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ModelSoft.Framework.Collections;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework
{
    [DebuggerStepThrough]
    public static class GeneralExtensions
    {

        #region [ object: AsString ]
        public static string AsString(this object obj)
        {
            return obj.AsString(Resources.NullString);
        }
        public static string AsString(this object obj, string nullString)
        {
            if (obj == null) return nullString;
            return obj.ToString();
        }
        #endregion

        #region [ object: IsNull, EvaluateOr, IfNull ]

        public static bool IsNull(this object obj)
        {
            return ReferenceEquals(obj, null);
        }

        public static T IfNull<T>(this T obj, T defaultValue)
            where T : class
        {
            if (ReferenceEquals(obj, null))
                return defaultValue;
            return obj;
        }

        public static T IfNull<T>(this T obj, Func<T> defaultValue)
            where T : class
        {
            if (defaultValue == null) throw new ArgumentNullException("defaultValue");
            if (ReferenceEquals(obj, null))
                return defaultValue();
            return obj;
        }

        public static V EvaluateOr<T, V>(this T obj, Func<T, V> evaluator)
            where T : class
        {
            if (evaluator == null) throw new ArgumentNullException("evaluator");
            if (ReferenceEquals(obj, null))
                return default(V);
            return evaluator(obj);
        }

        public static V EvaluateOr<T, V>(this T obj, Func<T, V> evaluator, V defaultValue)
            where T : class
        {
            if (evaluator == null) throw new ArgumentNullException("evaluator");
            if (ReferenceEquals(obj, null))
                return defaultValue;
            return evaluator(obj);
        }

        public static V EvaluateOr<T, V>(this T obj, Func<T, V> evaluator, Func<V> defaultValue)
            where T : class
        {
            if (evaluator == null) throw new ArgumentNullException("evaluator");
            if (defaultValue == null) throw new ArgumentNullException("defaultValue");
            if (ReferenceEquals(obj, null))
                return defaultValue();
            return evaluator(obj);
        }

        #endregion

        #region [ object, Delegate: IsSerializable ]
        public static bool IsSerializable(this Delegate @delegate)
        {
            return @delegate == null || @delegate.Method.IsStatic || (@delegate.Target != null && @delegate.Target.IsSerializable());
        }

        public static bool IsSerializable(this Object obj)
        {
            if (obj is Delegate) return ((Delegate) obj).IsSerializable();
            return obj == null || obj.GetType().IsSerializable;
        }
        #endregion

        #region [ object, Action, Func: Protected Execution ]
        public static T GetProtected<T>(this T failValue, Func<T> func, Action<Exception> failAction = null)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                if (failAction != null)
                    failAction(ex);
                return failValue;
            }
        }
        public static bool DoProtected(this Action action, Action<Exception> failAction = null)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                if (failAction != null)
                    failAction(ex);
                return false;
            }
        }
        public static bool TryProtected(this Action action, IEnumerable<RetryPattern> retryPatterns, Action<IEnumerable<Tuple<RetryPattern, Exception>>> failAction = null)
        {
            var exceptions = new List<Tuple<RetryPattern, Exception>>();
            var retryEnumerator = retryPatterns.GetEnumerator();
            var retryPattern = RetryPattern.SkipDelay;
            var keepTrying = true;
            while (keepTrying)
            {
                try
                {
                    action();
                    return true;
                }
                catch (Exception ex)
                {
                    exceptions.Add(Tuple.Create(retryPattern, ex));
                    if (retryEnumerator.MoveNext())
                    {
                        retryPattern = retryEnumerator.Current ?? RetryPattern.SkipDelay;
                        if (retryPattern == RetryPattern.Stop)
                        {
                            retryPattern = null;
                            keepTrying = false;
                        }
                        else if (retryPattern.RetryDelay != RetryPattern.SkipDelay)
                        {
                            Thread.Sleep(retryPattern.RetryDelay);
                        }
                    }
                    else
                    {
                        retryPattern = null;
                        keepTrying = false;
                    }
                }
            }
            if (failAction != null)
                failAction(exceptions);
            return false;
        }

        public static T InsteadOf<T>(this T defaultValue, Func<T> func)
        {
            try
            {
                return func();
            }
            catch
            {
                return defaultValue;
            }
        }
        public static T InsteadOf<T>(this Func<T> defaultValueFunc, Func<T> func)
        {
            try
            {
                return func();
            }
            catch
            {
                return defaultValueFunc();
            }
        }
        public static bool IfExists<T>(this T target, Action<T> action)
        {
            if (ReferenceEquals(target, null)) return false;
            action(target);
            return true;
        }

        public static TResult If<TResult, T>(T value, Func<T, bool> predicate, Func<T, TResult> ifTrue, Func<T, TResult> ifFalse)
        {
            return predicate(value) ? ifTrue(value) : ifFalse(value);
        }

        public static T ThrowIfNull<T>(this T value) where T : class
        {
            return ThrowIfDefault(value, null);
        }
        public static T ThrowIfDefault<T>(this T value)
        {
            return ThrowIfDefault(value, default(T));
        }
        public static T ThrowIfDefault<T>(this T value, T defaultValue)
        {
            if (Equals(value, defaultValue)) throw new NullReferenceException();
            return value;
        }

        public static void DoItFirstTime(this Action action, ref bool alreadyDone)
        {
            if (!alreadyDone)
            {
                action();
                alreadyDone = true;
            }
        }
        public static void DoItExclusive(this Action action, ref bool doing)
        {
            if (!doing)
            {
                doing = true;
                try
                {
                    action();
                }
                finally
                {
                    doing = false;
                }
            }
        }
        #endregion

        #region [ ToChildrenComparer ]
        public static IEqualityComparer<TChildren> ToChildrenComparer<TBase, TChildren>(this IEqualityComparer<TBase> baseEqualityComparer) where TChildren : TBase
        {
            return new EqualityComparerDownCaster<TBase, TChildren>(baseEqualityComparer);
        }
        #endregion

        #region [ Random: NextDouble, NextSingle, NextDecimal ]
        public static double NextDouble(this Random rnd, double min, double max)
        {
            return rnd.NextDouble() * (max - min) + min;
        }

        public static float NextSingle(this Random rnd)
        {
            return (float)rnd.NextDouble();
        }
        public static float NextSingle(this Random rnd, float min, float max)
        {
            return rnd.NextSingle() * (max - min) + min;
        }

        public static decimal NextDecimal(this Random rnd)
        {
            return (decimal)rnd.NextDouble();
        }
        public static decimal NextDecimal(this Random rnd, decimal min, decimal max)
        {
            return rnd.NextDecimal() * (max - min) + min;
        }
        #endregion

        #region [ Diagnostics ]
        #region [ MeasureTime ]
        public static TimeSpan MeasureTime(this Action action, int times = 1)
        {
            action.RequireNotNull("action");

            // take a shortcut here
            if (times <= 0) return TimeSpan.Zero;
            // some diversion tactics to trick the JIT
            action();
            // use a high accuracy time span measuring device
            var watch = new Stopwatch();
            watch.Start();
            // run the action, whatever it is, times times
            for (int i = 0; i < times; i++)
                action();
            watch.Stop();
            // return the average time taken to run the action
            return TimeSpan.FromMilliseconds(watch.Elapsed.TotalMilliseconds / times);
        }
        public static TimeSpan MeasureTime(this Stopwatch watch, Action action, int times = 1)
        {
            watch.RequireNotNull("watch");
            action.RequireNotNull("action");

            if (times <= 0) return TimeSpan.Zero;
            watch.Reset();
            watch.Start();
            for (int i = 0; i < times; i++)
                action();
            watch.Stop();
            return TimeSpan.FromMilliseconds(watch.Elapsed.TotalMilliseconds / times);
        }
        #endregion

        #region [ Console Tests ]
        public static void StartTest(this Stopwatch watch, string message, ConsoleColor? foreColor = null, ConsoleColor? backColor = null)
        {
            var oldForeColor = Console.ForegroundColor;
            var oldBackColor = Console.BackgroundColor;
            if (foreColor.HasValue)
                Console.ForegroundColor = foreColor.Value;
            if (backColor.HasValue)
                Console.BackgroundColor = backColor.Value;

            Console.Write(message);

            if (foreColor.HasValue)
                Console.ForegroundColor = oldForeColor;
            if (backColor.HasValue)
                Console.BackgroundColor = oldBackColor;

            watch.Restart();
        }
        public static void EndTest(this Stopwatch watch, string message, ConsoleColor? foreColor = null, ConsoleColor? backColor = null)
        {
            watch.Stop();

            var oldForeColor = Console.ForegroundColor;
            var oldBackColor = Console.BackgroundColor;
            if (foreColor.HasValue)
                Console.ForegroundColor = foreColor.Value;
            if (backColor.HasValue)
                Console.BackgroundColor = backColor.Value;

            Console.Write(message);
            Console.WriteLine(" ({0})", watch.Elapsed);

            if (foreColor.HasValue)
                Console.ForegroundColor = oldForeColor;
            if (backColor.HasValue)
                Console.BackgroundColor = oldBackColor;
        }
        public static void StartComplexTest(this Stopwatch watch, string message, ConsoleColor? foreColor = null, ConsoleColor? backColor = null)
        {
            var oldForeColor = Console.ForegroundColor;
            var oldBackColor = Console.BackgroundColor;
            if (foreColor.HasValue)
                Console.ForegroundColor = foreColor.Value;
            if (backColor.HasValue)
                Console.BackgroundColor = backColor.Value;

            Console.WriteLine(message);

            if (foreColor.HasValue)
                Console.ForegroundColor = oldForeColor;
            if (backColor.HasValue)
                Console.BackgroundColor = oldBackColor;

            watch.Restart();
        }
        public static void EndComplexTest(this Stopwatch watch, string message, ConsoleColor? foreColor = null, ConsoleColor? backColor = null)
        {
            watch.EndTest(message, foreColor, backColor);
        }

        public static void StartTest(this Stopwatch watch, TextWriter writer, string message)
        {
            writer.Write(message);
            watch.Restart();
        }
        public static void EndTest(this Stopwatch watch, TextWriter writer, string message)
        {
            watch.Stop();
            writer.Write(message);
            writer.WriteLine(" ({0})", watch.Elapsed);
        }
        #endregion
        #endregion

        #region [ Swap ]
        public static void Swap<T>(ref T value1, ref T value2)
        {
            T temp = value1;
            value1 = value2;
            value2 = temp;
        }
        public static bool Swap<T>(this bool condition, ref T value1, ref T value2)
        {
            if (condition) Swap(ref value1, ref value2);
            return condition;
        }
        public static bool Swap<T>(this Func<bool> condition, ref T value1, ref T value2)
        {
            return condition().Swap(ref value1, ref value2);
        }

        public static void Swap<T>(this IList<T> list, int pos1, int pos2)
        {
            T temp = list[pos1];
            list[pos1] = list[pos2];
            list[pos2] = temp;
        }
        public static bool Swap<T>(this IList<T> list, bool condition, int pos1, int pos2)
        {
            if (condition) list.Swap(pos1, pos2);
            return condition;
        }
        public static bool Swap<T>(this IList<T> list, Func<bool> condition, int pos1, int pos2)
        {
            return list.Swap(condition(), pos1, pos2);
        }
        #endregion

        #region [ numerics: InRange & ToRange ]
        public static bool InRange(this int value, int? min, int? max)
        {
            Require.MinMaxValid(min, max, "min", "max");

            if (min.HasValue && min.Value > value) return false;
            if (max.HasValue && max.Value < value) return false;
            return true;
        }
        public static bool InRange(this double value, double? min, double? max)
        {
            Require.MinMaxValid(min, max, "min", "max");

            if (min.HasValue && min.Value > value) return false;
            if (max.HasValue && max.Value < value) return false;
            return true;
        }
        public static bool InRange<T>(this T value, T? min, T? max)
          where T : struct, IComparable<T>
        {
            Require.MinMaxValid(min, max, "min", "max");

            if (min.HasValue && min.Value.CompareTo(value) > 0) return false;
            if (max.HasValue && max.Value.CompareTo(value) < 0) return false;
            return true;
        }
        public static bool InRange<T>(this T value, T min, T max)
          where T : class, IComparable<T>
        {
            value.RequireNotNull("value");
            Require.MinMaxValid(min, max, "min", "max");

            if (min != null && min.CompareTo(value) > 0) return false;
            if (max != null && max.CompareTo(value) < 0) return false;
            return true;
        }
        public static bool InRange<T>(this T value, T? min, T? max, IComparer<T> comparer)
          where T : struct
        {
            Require.MinMaxValidWithComparer(min, max, "min", "max", comparer: comparer);

            if (comparer == null) comparer = Comparer<T>.Default;
            if (min.HasValue && comparer.Compare(min.Value, value) > 0) return false;
            if (max.HasValue && comparer.Compare(max.Value, value) < 0) return false;
            return true;
        }
        public static bool InRange<T>(this T value, T min, T max, IComparer<T> comparer)
          where T : class
        {
            value.RequireNotNull("value");
            Require.MinMaxValidWithComparer(min, max, "min", "max", comparer: comparer);

            if (comparer == null) comparer = Comparer<T>.Default;
            if (min != null && comparer.Compare(min, value) > 0) return false;
            if (max != null && comparer.Compare(max, value) < 0) return false;
            return true;
        }

        public static int ToRange(this int value, int? min, int? max)
        {
            Require.MinMaxValid(min, max, "min", "max");

            if (min.HasValue && min.Value > value) return min.Value;
            if (max.HasValue && max.Value < value) return max.Value;
            return value;
        }
        public static double ToRange(this double value, double? min, double? max)
        {
            Require.MinMaxValid(min, max, "min", "max");

            if (min.HasValue && min.Value > value) return min.Value;
            if (max.HasValue && max.Value < value) return max.Value;
            return value;
        }
        public static T ToRange<T>(this T value, T? min, T? max)
          where T : struct, IComparable<T>
        {
            Require.MinMaxValid(min, max, "min", "max");

            if (min.HasValue && min.Value.CompareTo(value) > 0) return min.Value;
            if (max.HasValue && max.Value.CompareTo(value) < 0) return max.Value;
            return value;
        }
        public static T ToRange<T>(this T value, T min, T max)
          where T : class, IComparable<T>
        {
            value.RequireNotNull("value");

            Require.MinMaxValid(min, max, "min", "max");

            if (min != null && min.CompareTo(value) > 0) return min;
            if (max != null && max.CompareTo(value) < 0) return max;
            return value;
        }
        public static T ToRange<T>(this T value, T? min, T? max, IComparer<T> comparer)
          where T : struct
        {
            Require.MinMaxValidWithComparer(min, max, "min", "max", comparer: comparer);

            if (comparer == null) comparer = Comparer<T>.Default;
            if (min.HasValue && comparer.Compare(min.Value, value) > 0) return min.Value;
            if (max.HasValue && comparer.Compare(max.Value, value) < 0) return max.Value;
            return value;
        }
        public static T ToRange<T>(this T value, T min, T max, IComparer<T> comparer)
          where T : class
        {
            value.RequireNotNull("value");
            Require.MinMaxValidWithComparer(min, max, "min", "max", comparer: comparer);

            if (comparer == null) comparer = Comparer<T>.Default;
            if (min != null && comparer.Compare(min, value) > 0) return min;
            if (max != null && comparer.Compare(max, value) < 0) return max;
            return value;
        }
        #endregion

        #region [ To/From/Clone Binary ]
        public static bool IsCloneableType(this Type type)
        {
            bool isValue;
            return type.IsCloneableType(out isValue);
        }
        public static bool IsCloneableType(this Type type, out bool isValue)
        {
            isValue = false;

            if (typeof(ICloneable).IsAssignableFrom(type))
            {
                return true;
            }
            else if (type.IsValueType)
            {
                isValue = true;
                return true;
            }
            else
                return false;
        }

        public static TObject CloneSerializable<TObject>(this TObject serializableObject)
        {
            var serialized = serializableObject.ToBinarySerializable();
            var clone = serialized.FromBinarySerializable();
            return (TObject)clone;
        }
        public static byte[] ToBinarySerializable(this object serializableObject)
        {
            if (serializableObject == null) return null;
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, serializableObject);
                return memoryStream.ToArray();
            }
        }
        public static object FromBinarySerializable(this byte[] serializedObject)
        {
            if (serializedObject == null) return null;
            using (var memoryStream = new MemoryStream(serializedObject))
            {
                var formatter = new BinaryFormatter();
                var result = formatter.Deserialize(memoryStream);
                return result;
            }
        }
        #endregion

        #region [ Clone and Merge Dictionaries ]
        public static IDictionary<TKey, TValue> Clone<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            dict.RequireNotNull("dict");

            var result = new Dictionary<TKey, TValue>(dict.Count, dict is Dictionary<TKey, TValue> ? ((Dictionary<TKey, TValue>)dict).Comparer : null);
            foreach (var pair in dict)
                result[pair.Key] = pair.Value;
            return result;
        }
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> dict, IDictionary<TKey, TValue> other, bool overrideKeys = true)
        {
            dict.RequireNotNull("dict");
            other.RequireNotNull("other");

            foreach (var pair in other)
                if (overrideKeys || !dict.ContainsKey(pair.Key))
                    dict[pair.Key] = pair.Value;
            return dict;
        }
        public static IDictionary<TKey, TValue> MergeCopy<TKey, TValue>(this IDictionary<TKey, TValue> dict, IDictionary<TKey, TValue> other, bool overrideKeys = true)
        {
            dict.RequireNotNull("dict");
            other.RequireNotNull("other");

            var result = new Dictionary<TKey, TValue>(dict.Count + other.Count, dict is Dictionary<TKey, TValue> ? ((Dictionary<TKey, TValue>)dict).Comparer : null);
            foreach (var pair in dict)
                result[pair.Key] = pair.Value;
            foreach (var pair in other)
                if (overrideKeys || !result.ContainsKey(pair.Key))
                    result[pair.Key] = pair.Value;
            return result;
        }
        #endregion

        #region [ CreateOperationMatrix ]
        public static S[,] CreateOperationMatrix<T1, T2, S>(this T1[] rows, T2[] cols, Func<T1, T2, S> operation)
        {
            rows.RequireNotNull("rows");
            cols.RequireNotNull("cols");
            operation.RequireNotNull("operation");

            S[,] result = new S[rows.Length, cols.Length];
            for (int i = 0; i < rows.Length; i++)
                for (int j = 0; j < cols.Length; j++)
                    result[i, j] = operation(rows[i], cols[j]);
            return result;
        }
        #endregion

        #region [ SortedSearch ]
        /// <summary>
        /// Returns a tuple containning (position, found)
        /// If found == true, then the value was found at given position 
        /// If found == false, then the value should have been found at given position, but it wasn't
        /// endIndex is excluded from the search
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static Tup<int, bool> SortedSearch<T>(this IList<T> list, T value, IComparer<T> comparer = null, int startIndex = 0, int endIndex = -1)
        {
            Contract.Requires(list != null);
            Contract.Requires(startIndex >= 0);
            Contract.Requires(endIndex == 0 || (endIndex <= list.Count && endIndex >= startIndex));

            if (comparer == null) comparer = Comparer<T>.Default;
            int ini = startIndex;
            int fin = endIndex < 0 ? list.Count : endIndex;
            while (ini < fin)
            {
                int mid = (ini + fin) / 2;
                int comp = comparer.Compare(value, list[mid]);
                if (comp == 0)
                    return Tup.Create(mid, true);
                if (comp < 0)
                    fin = mid;
                else
                    ini = mid + 1;
            }
            return Tup.Create(fin, false);
        }
        #endregion

        #region [ Comparer, Comparable, IEqualityComparer and IEquatable extensions ]

        /// <summary>
        /// Determine if the left operand is greater than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &gt; right</returns>
        public static bool GreaterThan<T>(this T left, T right)
          where T : IComparable<T>
        {
            if (ReferenceEquals(left, null))
                return false;

            return left.CompareTo(right) > 0;
        }
        /// <summary>
        /// Determine if the left operand is less than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &lt; right </returns>
        public static bool LessThan<T>(this T left, T right)
          where T : IComparable<T>
        {
            if (ReferenceEquals(left, null))
                return (ReferenceEquals(right, null)) ? false : true;

            return left.CompareTo(right) < 0;
        }
        /// <summary>
        /// Determines if the left operand is greater than or equal
        /// than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &gt;= right</returns>
        public static bool GreaterThanOrEqual<T>(this T left, T right)
          where T : IComparable<T>
        {
            if (ReferenceEquals(left, null))
                return (ReferenceEquals(right, null)) ? true : false;

            return left.CompareTo(right) >= 0;
        }
        /// <summary>
        /// Determines if the left operand is less than or
        /// equal to the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if the left &lt;= right</returns>
        public static bool LessThanOrEqual<T>(this T left, T right)
          where T : IComparable<T>
        {
            if (ReferenceEquals(left, null))
                return true;
            return left.CompareTo(right) <= 0;
        }
        public static T Min<T>(this T left, T right)
          where T : IComparable<T>
        {
            return left.LessThanOrEqual(right) ? left : right;
        }
        public static T Min<T>(this T first, params T[] rest)
          where T : IComparable<T>
        {
            T min = first;
            for (int i = 0; i < rest.Length; i++)
                min = min.Min(rest[i]);
            return min;
        }
        public static int MinPos<T>(this T first, params T[] rest)
          where T : IComparable<T>
        {
            T min = first;
            int pos = 0;
            for (int i = 0; i < rest.Length; i++)
                if (min.GreaterThan(rest[i]))
                {
                    min = rest[i];
                    pos = i + 1;
                }
            return pos;
        }
        public static T Max<T>(this T left, T right)
          where T : IComparable<T>
        {
            return left.GreaterThanOrEqual(right) ? left : right;
        }
        public static T Max<T>(this T first, params T[] rest)
          where T : IComparable<T>
        {
            T max = first;
            for (int i = 0; i < rest.Length; i++)
                max = max.Max(rest[i]);
            return max;
        }
        public static int MaxPos<T>(this T first, params T[] rest)
          where T : IComparable<T>
        {
            T max = first;
            int pos = 0;
            for (int i = 0; i < rest.Length; i++)
                if (max.LessThan(rest[i]))
                {
                    max = rest[i];
                    pos = i + 1;
                }
            return pos;
        }

        /// <summary>
        /// Determine if the left operand is greater than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &gt; right</returns>
        public static bool EqualsTo<T>(this IComparer<T> comparer, T left, T right)
        {
            if (ReferenceEquals(left, right))
                return true;

            return (comparer ?? Comparer<T>.Default).Compare(left, right) == 0;
        }
        /// <summary>
        /// Determine if the left operand is less than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &lt; right </returns>
        public static bool NotEqualsTo<T>(this IComparer<T> comparer, T left, T right)
        {
            return !comparer.EqualsTo(left, right);
        }
        /// <summary>
        /// Determine if the left operand is greater than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &gt; right</returns>
        public static bool GreaterThan<T>(this IComparer<T> comparer, T left, T right)
        {
            if (ReferenceEquals(left, null))
                return false;

            return (comparer ?? Comparer<T>.Default).Compare(left, right) > 0;
        }
        /// <summary>
        /// Determine if the left operand is less than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &lt; right </returns>
        public static bool LessThan<T>(this IComparer<T> comparer, T left, T right)
        {
            if (ReferenceEquals(left, null))
                return (ReferenceEquals(right, null)) ? false : true;

            return comparer.Compare(left, right) < 0;
        }
        /// <summary>
        /// Determines if the left operand is greater than or equal
        /// than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &gt;= right</returns>
        public static bool GreaterThanOrEqual<T>(this IComparer<T> comparer, T left, T right)
        {
            if (ReferenceEquals(left, null))
                return (ReferenceEquals(right, null)) ? true : false;

            return comparer.Compare(left, right) >= 0;
        }
        /// <summary>
        /// Determines if the left operand is less than or
        /// equal to the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if the left &lt;= right</returns>
        public static bool LessThanOrEqual<T>(this IComparer<T> comparer, T left, T right)
        {
            if (ReferenceEquals(left, null))
                return true;
            return comparer.Compare(left, right) <= 0;
        }
        public static T Min<T>(this IComparer<T> comparer, T left, T right)
        {
            return comparer.LessThanOrEqual(left, right) ? left : right;
        }
        public static T Min<T>(this IComparer<T> comparer, T first, params T[] rest)
        {
            T min = first;
            for (int i = 0; i < rest.Length; i++)
                min = comparer.Min(min, rest[i]);
            return min;
        }
        public static int MinPos<T>(this IComparer<T> comparer, T first, params T[] rest)
        {
            T min = first;
            int pos = 0;
            for (int i = 0; i < rest.Length; i++)
                if (comparer.GreaterThan(min, rest[i]))
                {
                    min = rest[i];
                    pos = i + 1;
                }
            return pos;
        }
        public static T Max<T>(this IComparer<T> comparer, T left, T right)
        {
            return comparer.GreaterThanOrEqual(left, right) ? left : right;
        }
        public static T Max<T>(this IComparer<T> comparer, T first, params T[] rest)
        {
            T max = first;
            for (int i = 0; i < rest.Length; i++)
                max = comparer.Max(max, rest[i]);
            return max;
        }
        public static int MaxPos<T>(this IComparer<T> comparer, T first, params T[] rest)
        {
            T max = first;
            int pos = 0;
            for (int i = 0; i < rest.Length; i++)
                if (comparer.LessThan(max, rest[i]))
                {
                    max = rest[i];
                    pos = i + 1;
                }
            return pos;
        }

        /// <summary>
        /// Determine if the left operand is greater than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &gt; right</returns>
        public static bool EqualsTo<T>(this T left, T right, IComparer<T> comparer)
        {
            return comparer.EqualsTo(left, right);
        }
        /// <summary>
        /// Determine if the left operand is less than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &lt; right </returns>
        public static bool NotEqualsTo<T>(this T left, T right, IComparer<T> comparer)
        {
            return comparer.NotEqualsTo(left, right);
        }
        /// <summary>
        /// Determine if the left operand is greater than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &gt; right</returns>
        public static bool GreaterThan<T>(this T left, T right, IComparer<T> comparer)
        {
            return comparer.GreaterThan(left, right);
        }
        /// <summary>
        /// Determine if the left operand is less than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &lt; right </returns>
        public static bool LessThan<T>(this T left, T right, IComparer<T> comparer)
        {
            return comparer.LessThan(left, right);
        }
        /// <summary>
        /// Determines if the left operand is greater than or equal
        /// than the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if left &gt;= right</returns>
        public static bool GreaterThanOrEqual<T>(this T left, T right, IComparer<T> comparer)
        {
            return comparer.GreaterThanOrEqual(left, right);
        }
        /// <summary>
        /// Determines if the left operand is less than or
        /// equal to the right
        /// </summary>
        /// <typeparam name="T">the type being compared</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if the left &lt;= right</returns>
        public static bool LessThanOrEqual<T>(this T left, T right, IComparer<T> comparer)
        {
            return comparer.LessThanOrEqual(left, right);
        }
        public static T Min<T>(this T left, T right, IComparer<T> comparer)
        {
            return comparer.LessThanOrEqual(left, right) ? left : right;
        }
        public static T Max<T>(this T left, T right, IComparer<T> comparer)
        {
            return comparer.GreaterThanOrEqual(left, right) ? left : right;
        }


        public static bool AreEquals<T>(this T value1, T value2, IEqualityComparer<T> comparer = null)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;
            return comparer.Equals(value1, value2);
        }
        public static bool AreEqualsArray<T>(this T[] array1, T[] array2, IEqualityComparer<T> comparer = null)
        {
            if (array1 == null)
                return array2 == null;
            if (array2 == null)
                return false;
            if (array1.Length != array2.Length)
                return false;

            comparer = comparer ?? EqualityComparer<T>.Default;
            var length = array1.Length;

            for (int i = 0; i < length; i++)
            {
                if (!comparer.Equals(array1[i], array2[i]))
                    return false;
            }
            return true;
        }
        public static bool AreEqualsNullable<T>(this T? item1, T? item2, IEqualityComparer<T> comparer = null)
          where T : struct
        {
            return Nullable.Equals(item1, item2);
        }

        public static IComparer<T> AsComparer<T>(this Comparison<T> comparison)
        {
            comparison.RequireNotNull("comparison");
            return new ComparisonComparer<T>(comparison);
        }

        public static IComparer AsComparer<T>(this IComparer<T> comparer)
        {
            comparer.RequireNotNull("comparer");
            if (comparer is IComparer) return (IComparer) comparer;
            return new ComparerOfTToComparerAdapter<T>(comparer);
        }
        public static IComparer<T> AsComparer<T>(this IComparer comparer)
        {
            comparer.RequireNotNull("comparer");
            if (comparer is IComparer<T>) return (IComparer<T>)comparer;
            return new ComparerToComparerOfTAdapter<T>(comparer);
        }

        class ComparerOfTToComparerAdapter<T> : 
            IComparer
        {
            private readonly IComparer<T> _comparer;

            public ComparerOfTToComparerAdapter(IComparer<T> comparer)
            {
                comparer.RequireNotNull("comparer");
                _comparer = comparer;
            }

            public int Compare(object x, object y)
            {
                return _comparer.Compare((T) x, (T) y);
            }
        }
        class ComparerToComparerOfTAdapter<T> : 
            IComparer<T>
        {
            private readonly IComparer _comparer;

            public ComparerToComparerOfTAdapter(IComparer comparer)
            {
                comparer.RequireNotNull("comparer");
                _comparer = comparer;
            }

            public int Compare(T x, T y)
            {
                return _comparer.Compare(x, y);
            }
        }

        #endregion

        #region [ GetDefaultValue ]
        public static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
                return Activator.CreateInstance(t);
            return null;
        }
        #endregion

        #region [ Reflection ]
        public static object GetPropertyValue(this object target, string propertyName, params object[] args)
        {
            target.RequireNotNull("target");
            propertyName.RequireNotWhitespace("propertyName");

            var type = target.GetType();
            var result = type.InvokeMember(propertyName, BindingFlags.GetProperty, null, target, args);
            return result;
        }
        public static void SetPropertyValue(this object target, string propertyName, object value, params object[] args)
        {
            target.RequireNotNull("target");
            propertyName.RequireNotWhitespace("propertyName");

            var type = target.GetType();
            type.InvokeMember(propertyName, BindingFlags.SetProperty, null, target, args.GetAsEmptyIfNull().Concat(value.Singleton()).ToArray());
        }
        public static object CallMethod(this object target, string methodName, params object[] args)
        {
            target.RequireNotNull("target");
            methodName.RequireNotWhitespace("methodName");

            var type = target.GetType();
            var result = type.InvokeMember(methodName, BindingFlags.InvokeMethod, null, target, args);
            return result;
        }
        #endregion

        #region [ SaveValue ]
        public static IDisposable SaveValue<T>(this T value, Func<T> valueGetter, Action<T> valueSetter)
        {
            return new ValueSaver<T>(value, valueGetter, valueSetter);
        }

        private class ValueSaver<T> : IDisposable
        {
            T oldValue;
            Action<T> valueSetter;

            public ValueSaver(T newValue, Func<T> valueGetter, Action<T> valueSetter)
            {
                Contract.Requires(valueGetter != null);
                Contract.Requires(valueSetter != null);

                oldValue = valueGetter();
                valueSetter(newValue);
            }

            void IDisposable.Dispose()
            {
                if (valueSetter != null)
                {
                    valueSetter(oldValue);
                    valueSetter = null;
                    GC.SuppressFinalize(this);
                }
            }
        }
        #endregion

        #region [ SetAndRestore ]
        public static IDisposable SetAndRestore<T>(this T currentValue, T newValue, Action<T> restoreFunc)
        {
            restoreFunc.RequireNotNull("restoreFunc");

            return new SetAndRestoreImpl<T>(currentValue, newValue, restoreFunc);
        }

        private class SetAndRestoreImpl<T> : IDisposable
        {
            private T oldValue;
            private Action<T> restoreFunc;

            public SetAndRestoreImpl(T currentValue, T newValue, Action<T> restoreFunc)
            {
                this.oldValue = currentValue;
                this.restoreFunc = restoreFunc;

                restoreFunc(newValue);
            }

            public void Dispose()
            {
                if (restoreFunc == null) return;
                restoreFunc(oldValue);
                restoreFunc = null;
                GC.SuppressFinalize(this);
            }
        }
        #endregion

        #region [ IServiceProvider ]
        public static TService GetService<TService>(this IServiceProvider serviceProvider, bool throwIfNotFound = false)
          where TService : class
        {
            serviceProvider.RequireNotNull("serviceProvider");

            var service = serviceProvider.GetService(typeof(TService)) as TService;

            if (service == null && throwIfNotFound)
                throw new NotSupportedException("Service not available: " + typeof(TService).FullName);

            return service;
        }

        #endregion

        #region [ IDictionary: GetValueOr, GetValueOrSet ]

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return GetValueOr(dictionary, key, null);
        }
        public static TValue GetValueOr<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            return GetValueOr(dictionary, key, () => defaultValue);
        }
        public static TValue GetValueOr<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueFunc)
        {
            if (dictionary == null)
                return defaultValueFunc.EvalOr();
            TValue result;
            if (dictionary.TryGetValue(key, out result))
                return result;
            result = defaultValueFunc.EvalOr();
            return result;
        }

        public static TValue GetValueOrSetDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return GetValueOrSet(dictionary, key, null);
        }
        public static TValue GetValueOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            return GetValueOrSet(dictionary, key, () => defaultValue);
        }
        public static TValue GetValueOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueFunc)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
        
            TValue result;
            if (dictionary.TryGetValue(key, out result))
                return result;
            result = defaultValueFunc.EvalOr();
            dictionary[key] = result;
            return result;
        }

        #endregion

        #region [ Uri ]

        public static Uri WithPathDelimiter(this Uri uri)
        {
            if (uri == null) throw new ArgumentNullException("uri");

            var path = uri.GetLeftPart(UriPartial.Path);
            if (!path.EndsWith("/"))
            {
                path = path + "/";

                path += uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped) +
                        uri.GetComponents(UriComponents.Fragment, UriFormat.UriEscaped);

                return new Uri(path, uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
            }

            return uri;
        }
        public static Uri WithoutPathDelimiter(this Uri uri)
        {
            if (uri == null) throw new ArgumentNullException("uri");

            var path = uri.GetLeftPart(UriPartial.Path);
            if (path.EndsWith("/"))
            {
                path = path.Substring(0, path.Length - 1);

                path += uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped) +
                        uri.GetComponents(UriComponents.Fragment, UriFormat.UriEscaped);

                return new Uri(path, uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
            }

            return uri;
        }

        #endregion

    }

    public class RetryPattern : IEquatable<RetryPattern>
    {
        public static readonly RetryPattern SkipDelay = new RetryPattern(TimeSpan.Zero, false);
        public static readonly RetryPattern Stop = new RetryPattern(TimeSpan.Zero, true);

        public RetryPattern() :
            this(TimeSpan.Zero, false)
        {
        }

        public RetryPattern(TimeSpan delay, bool stopTrying = false)
        {
            if (delay.TotalMilliseconds < 0) delay = TimeSpan.Zero;
            RetryDelay = delay;
            StopTrying = stopTrying;
        }

        public TimeSpan RetryDelay { get; private set; }
        public bool StopTrying { get; private set; }

        public static implicit operator RetryPattern(TimeSpan delay)
        {
            return new RetryPattern(delay);
        }

        public bool Equals(RetryPattern other)
        {
            return other != null &&
              RetryDelay.Equals(other.RetryDelay) &&
              StopTrying.Equals(other.StopTrying);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RetryPattern);
        }

        public override int GetHashCode()
        {
            return RetryDelay.GetHashCode() ^ StopTrying.GetHashCode();
        }

        public static bool operator ==(RetryPattern p1, RetryPattern p2)
        {
            return ReferenceEquals(p1, null) && ReferenceEquals(p2, null) ||
              !ReferenceEquals(p1, null) && !ReferenceEquals(p2, null) && p1.Equals(p2);
        }

        public static bool operator !=(RetryPattern p1, RetryPattern p2)
        {
            return !(p1 == p2);
        }

        public override string ToString()
        {
            if (this == SkipDelay) return "Skip delay";
            if (this == Stop) return "Stop trying";
            return string.Format("Delay {0} and retry", RetryDelay);
        }

    }
}
