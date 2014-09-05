 

namespace ModelSoft.Framework
{
	using global::System;
	using global::System.ComponentModel;
	using global::System.Collections.Generic;
  
  public enum StopsWith { Shorter, Longer, First, Never }
  
  public static class Tup
  {
  
    /// <summary>
    /// Creates an immutable 1-Tuple.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th element.</typeparam>
    /// <param name="item1">The 1th element.</param>
    /// <returns>A new immutable 1-Tuple.</returns>
    public static Tup<T1> Create<T1>(T1 item1)
    {
        return new Tup<T1>(item1);
    }

    public static Tup<T1> CreateTuple<T, T1>(this T obj, Func<T, T1> func1)
    {
      return Create(func1(obj));
    }
    
    public static IEnumerable<Tup<T1>> Enumerate<T1>(this Tup<IEnumerable<T1>> enumerables)
    {
      return Enumerate(enumerables, true);
    }

    public static IEnumerable<Tup<T1>> Enumerate<T1>(this Tup<IEnumerable<T1>> enumerables, bool stopWithShorter)
    {
      return Enumerate(enumerables.Item1, stopWithShorter);
    }

    public static IEnumerable<Tup<T1>> Enumerate<T1>(this IEnumerable<T1> item1)
    {
      return Enumerate(item1, false);
    }

    public static IEnumerable<Tup<T1>> Enumerate<T1>(this IEnumerable<T1> item1, bool stopWithShorter)
    {
      return Enumerate(item1, stopWithShorter ? StopsWith.Shorter : StopsWith.Longer);
    }

    public static IEnumerable<Tup<T1>> Enumerate<T1>(this IEnumerable<T1> item1, StopsWith stopsWith)
    {
      var item1Enum = item1.GetEnumerator();
      var item1Finished = false;

      while (true)
      {
        int finishedCounter = 1;

        if (!item1Finished && !item1Enum.MoveNext())
          item1Finished = true;
        if (item1Finished) finishedCounter--;

        switch (stopsWith)
        {
          case StopsWith.Shorter:
            if (finishedCounter < 1)
              yield break;
            break;
          case StopsWith.Longer:
            if (finishedCounter == 0)
              yield break;
            break;
          case StopsWith.First:
            if (item1Finished)
              yield break;
            break;
        }

        //if (stopWithShorter && finishedCounter < 1 || !stopWithShorter && finishedCounter == 0)
        //  yield break;
        var tuple = Tup.Create(
          item1Finished ? default(T1) : item1Enum.Current );
        yield return tuple;
      }
    }
    
    /// <summary>
    /// Creates an immutable 2-Tuple.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th element.</typeparam>
    /// <typeparam name="T2">The type of the 2th element.</typeparam>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <returns>A new immutable 2-Tuple.</returns>
    public static Tup<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
    {
        return new Tup<T1, T2>(item1, item2);
    }

    public static Tup<T1, T2> CreateTuple<T, T1, T2>(this T obj, Func<T, T1> func1, Func<T, T2> func2)
    {
      return Create(func1(obj), func2(obj));
    }
    
    public static IEnumerable<Tup<T1, T2>> Enumerate<T1, T2>(this Tup<IEnumerable<T1>, IEnumerable<T2>> enumerables)
    {
      return Enumerate(enumerables, true);
    }

    public static IEnumerable<Tup<T1, T2>> Enumerate<T1, T2>(this Tup<IEnumerable<T1>, IEnumerable<T2>> enumerables, bool stopWithShorter)
    {
      return Enumerate(enumerables.Item1, enumerables.Item2, stopWithShorter);
    }

    public static IEnumerable<Tup<T1, T2>> Enumerate<T1, T2>(this IEnumerable<T1> item1, IEnumerable<T2> item2)
    {
      return Enumerate(item1, item2, false);
    }

    public static IEnumerable<Tup<T1, T2>> Enumerate<T1, T2>(this IEnumerable<T1> item1, IEnumerable<T2> item2, bool stopWithShorter)
    {
      return Enumerate(item1, item2, stopWithShorter ? StopsWith.Shorter : StopsWith.Longer);
    }

    public static IEnumerable<Tup<T1, T2>> Enumerate<T1, T2>(this IEnumerable<T1> item1, IEnumerable<T2> item2, StopsWith stopsWith)
    {
      var item1Enum = item1.GetEnumerator();
      var item1Finished = false;
      var item2Enum = item2.GetEnumerator();
      var item2Finished = false;

      while (true)
      {
        int finishedCounter = 2;

        if (!item1Finished && !item1Enum.MoveNext())
          item1Finished = true;
        if (item1Finished) finishedCounter--;

        if (!item2Finished && !item2Enum.MoveNext())
          item2Finished = true;
        if (item2Finished) finishedCounter--;

        switch (stopsWith)
        {
          case StopsWith.Shorter:
            if (finishedCounter < 2)
              yield break;
            break;
          case StopsWith.Longer:
            if (finishedCounter == 0)
              yield break;
            break;
          case StopsWith.First:
            if (item1Finished)
              yield break;
            break;
        }

        //if (stopWithShorter && finishedCounter < 2 || !stopWithShorter && finishedCounter == 0)
        //  yield break;
        var tuple = Tup.Create(
          item1Finished ? default(T1) : item1Enum.Current, 
          item2Finished ? default(T2) : item2Enum.Current );
        yield return tuple;
      }
    }
    
    /// <summary>
    /// Creates an immutable 3-Tuple.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th element.</typeparam>
    /// <typeparam name="T2">The type of the 2th element.</typeparam>
    /// <typeparam name="T3">The type of the 3th element.</typeparam>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <returns>A new immutable 3-Tuple.</returns>
    public static Tup<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
    {
        return new Tup<T1, T2, T3>(item1, item2, item3);
    }

    public static Tup<T1, T2, T3> CreateTuple<T, T1, T2, T3>(this T obj, Func<T, T1> func1, Func<T, T2> func2, Func<T, T3> func3)
    {
      return Create(func1(obj), func2(obj), func3(obj));
    }
    
    public static IEnumerable<Tup<T1, T2, T3>> Enumerate<T1, T2, T3>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> enumerables)
    {
      return Enumerate(enumerables, true);
    }

    public static IEnumerable<Tup<T1, T2, T3>> Enumerate<T1, T2, T3>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> enumerables, bool stopWithShorter)
    {
      return Enumerate(enumerables.Item1, enumerables.Item2, enumerables.Item3, stopWithShorter);
    }

    public static IEnumerable<Tup<T1, T2, T3>> Enumerate<T1, T2, T3>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3)
    {
      return Enumerate(item1, item2, item3, false);
    }

    public static IEnumerable<Tup<T1, T2, T3>> Enumerate<T1, T2, T3>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, bool stopWithShorter)
    {
      return Enumerate(item1, item2, item3, stopWithShorter ? StopsWith.Shorter : StopsWith.Longer);
    }

    public static IEnumerable<Tup<T1, T2, T3>> Enumerate<T1, T2, T3>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, StopsWith stopsWith)
    {
      var item1Enum = item1.GetEnumerator();
      var item1Finished = false;
      var item2Enum = item2.GetEnumerator();
      var item2Finished = false;
      var item3Enum = item3.GetEnumerator();
      var item3Finished = false;

      while (true)
      {
        int finishedCounter = 3;

        if (!item1Finished && !item1Enum.MoveNext())
          item1Finished = true;
        if (item1Finished) finishedCounter--;

        if (!item2Finished && !item2Enum.MoveNext())
          item2Finished = true;
        if (item2Finished) finishedCounter--;

        if (!item3Finished && !item3Enum.MoveNext())
          item3Finished = true;
        if (item3Finished) finishedCounter--;

        switch (stopsWith)
        {
          case StopsWith.Shorter:
            if (finishedCounter < 3)
              yield break;
            break;
          case StopsWith.Longer:
            if (finishedCounter == 0)
              yield break;
            break;
          case StopsWith.First:
            if (item1Finished)
              yield break;
            break;
        }

        //if (stopWithShorter && finishedCounter < 3 || !stopWithShorter && finishedCounter == 0)
        //  yield break;
        var tuple = Tup.Create(
          item1Finished ? default(T1) : item1Enum.Current, 
          item2Finished ? default(T2) : item2Enum.Current, 
          item3Finished ? default(T3) : item3Enum.Current );
        yield return tuple;
      }
    }
    
    /// <summary>
    /// Creates an immutable 4-Tuple.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th element.</typeparam>
    /// <typeparam name="T2">The type of the 2th element.</typeparam>
    /// <typeparam name="T3">The type of the 3th element.</typeparam>
    /// <typeparam name="T4">The type of the 4th element.</typeparam>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <returns>A new immutable 4-Tuple.</returns>
    public static Tup<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
    {
        return new Tup<T1, T2, T3, T4>(item1, item2, item3, item4);
    }

    public static Tup<T1, T2, T3, T4> CreateTuple<T, T1, T2, T3, T4>(this T obj, Func<T, T1> func1, Func<T, T2> func2, Func<T, T3> func3, Func<T, T4> func4)
    {
      return Create(func1(obj), func2(obj), func3(obj), func4(obj));
    }
    
    public static IEnumerable<Tup<T1, T2, T3, T4>> Enumerate<T1, T2, T3, T4>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> enumerables)
    {
      return Enumerate(enumerables, true);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4>> Enumerate<T1, T2, T3, T4>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> enumerables, bool stopWithShorter)
    {
      return Enumerate(enumerables.Item1, enumerables.Item2, enumerables.Item3, enumerables.Item4, stopWithShorter);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4>> Enumerate<T1, T2, T3, T4>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4)
    {
      return Enumerate(item1, item2, item3, item4, false);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4>> Enumerate<T1, T2, T3, T4>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, bool stopWithShorter)
    {
      return Enumerate(item1, item2, item3, item4, stopWithShorter ? StopsWith.Shorter : StopsWith.Longer);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4>> Enumerate<T1, T2, T3, T4>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, StopsWith stopsWith)
    {
      var item1Enum = item1.GetEnumerator();
      var item1Finished = false;
      var item2Enum = item2.GetEnumerator();
      var item2Finished = false;
      var item3Enum = item3.GetEnumerator();
      var item3Finished = false;
      var item4Enum = item4.GetEnumerator();
      var item4Finished = false;

      while (true)
      {
        int finishedCounter = 4;

        if (!item1Finished && !item1Enum.MoveNext())
          item1Finished = true;
        if (item1Finished) finishedCounter--;

        if (!item2Finished && !item2Enum.MoveNext())
          item2Finished = true;
        if (item2Finished) finishedCounter--;

        if (!item3Finished && !item3Enum.MoveNext())
          item3Finished = true;
        if (item3Finished) finishedCounter--;

        if (!item4Finished && !item4Enum.MoveNext())
          item4Finished = true;
        if (item4Finished) finishedCounter--;

        switch (stopsWith)
        {
          case StopsWith.Shorter:
            if (finishedCounter < 4)
              yield break;
            break;
          case StopsWith.Longer:
            if (finishedCounter == 0)
              yield break;
            break;
          case StopsWith.First:
            if (item1Finished)
              yield break;
            break;
        }

        //if (stopWithShorter && finishedCounter < 4 || !stopWithShorter && finishedCounter == 0)
        //  yield break;
        var tuple = Tup.Create(
          item1Finished ? default(T1) : item1Enum.Current, 
          item2Finished ? default(T2) : item2Enum.Current, 
          item3Finished ? default(T3) : item3Enum.Current, 
          item4Finished ? default(T4) : item4Enum.Current );
        yield return tuple;
      }
    }
    
    /// <summary>
    /// Creates an immutable 5-Tuple.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th element.</typeparam>
    /// <typeparam name="T2">The type of the 2th element.</typeparam>
    /// <typeparam name="T3">The type of the 3th element.</typeparam>
    /// <typeparam name="T4">The type of the 4th element.</typeparam>
    /// <typeparam name="T5">The type of the 5th element.</typeparam>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <param name="item5">The 5th element.</param>
    /// <returns>A new immutable 5-Tuple.</returns>
    public static Tup<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
        return new Tup<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
    }

    public static Tup<T1, T2, T3, T4, T5> CreateTuple<T, T1, T2, T3, T4, T5>(this T obj, Func<T, T1> func1, Func<T, T2> func2, Func<T, T3> func3, Func<T, T4> func4, Func<T, T5> func5)
    {
      return Create(func1(obj), func2(obj), func3(obj), func4(obj), func5(obj));
    }
    
    public static IEnumerable<Tup<T1, T2, T3, T4, T5>> Enumerate<T1, T2, T3, T4, T5>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> enumerables)
    {
      return Enumerate(enumerables, true);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5>> Enumerate<T1, T2, T3, T4, T5>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> enumerables, bool stopWithShorter)
    {
      return Enumerate(enumerables.Item1, enumerables.Item2, enumerables.Item3, enumerables.Item4, enumerables.Item5, stopWithShorter);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5>> Enumerate<T1, T2, T3, T4, T5>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5)
    {
      return Enumerate(item1, item2, item3, item4, item5, false);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5>> Enumerate<T1, T2, T3, T4, T5>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, bool stopWithShorter)
    {
      return Enumerate(item1, item2, item3, item4, item5, stopWithShorter ? StopsWith.Shorter : StopsWith.Longer);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5>> Enumerate<T1, T2, T3, T4, T5>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, StopsWith stopsWith)
    {
      var item1Enum = item1.GetEnumerator();
      var item1Finished = false;
      var item2Enum = item2.GetEnumerator();
      var item2Finished = false;
      var item3Enum = item3.GetEnumerator();
      var item3Finished = false;
      var item4Enum = item4.GetEnumerator();
      var item4Finished = false;
      var item5Enum = item5.GetEnumerator();
      var item5Finished = false;

      while (true)
      {
        int finishedCounter = 5;

        if (!item1Finished && !item1Enum.MoveNext())
          item1Finished = true;
        if (item1Finished) finishedCounter--;

        if (!item2Finished && !item2Enum.MoveNext())
          item2Finished = true;
        if (item2Finished) finishedCounter--;

        if (!item3Finished && !item3Enum.MoveNext())
          item3Finished = true;
        if (item3Finished) finishedCounter--;

        if (!item4Finished && !item4Enum.MoveNext())
          item4Finished = true;
        if (item4Finished) finishedCounter--;

        if (!item5Finished && !item5Enum.MoveNext())
          item5Finished = true;
        if (item5Finished) finishedCounter--;

        switch (stopsWith)
        {
          case StopsWith.Shorter:
            if (finishedCounter < 5)
              yield break;
            break;
          case StopsWith.Longer:
            if (finishedCounter == 0)
              yield break;
            break;
          case StopsWith.First:
            if (item1Finished)
              yield break;
            break;
        }

        //if (stopWithShorter && finishedCounter < 5 || !stopWithShorter && finishedCounter == 0)
        //  yield break;
        var tuple = Tup.Create(
          item1Finished ? default(T1) : item1Enum.Current, 
          item2Finished ? default(T2) : item2Enum.Current, 
          item3Finished ? default(T3) : item3Enum.Current, 
          item4Finished ? default(T4) : item4Enum.Current, 
          item5Finished ? default(T5) : item5Enum.Current );
        yield return tuple;
      }
    }
    
    /// <summary>
    /// Creates an immutable 6-Tuple.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th element.</typeparam>
    /// <typeparam name="T2">The type of the 2th element.</typeparam>
    /// <typeparam name="T3">The type of the 3th element.</typeparam>
    /// <typeparam name="T4">The type of the 4th element.</typeparam>
    /// <typeparam name="T5">The type of the 5th element.</typeparam>
    /// <typeparam name="T6">The type of the 6th element.</typeparam>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <param name="item5">The 5th element.</param>
    /// <param name="item6">The 6th element.</param>
    /// <returns>A new immutable 6-Tuple.</returns>
    public static Tup<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
    {
        return new Tup<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
    }

    public static Tup<T1, T2, T3, T4, T5, T6> CreateTuple<T, T1, T2, T3, T4, T5, T6>(this T obj, Func<T, T1> func1, Func<T, T2> func2, Func<T, T3> func3, Func<T, T4> func4, Func<T, T5> func5, Func<T, T6> func6)
    {
      return Create(func1(obj), func2(obj), func3(obj), func4(obj), func5(obj), func6(obj));
    }
    
    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6>> Enumerate<T1, T2, T3, T4, T5, T6>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> enumerables)
    {
      return Enumerate(enumerables, true);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6>> Enumerate<T1, T2, T3, T4, T5, T6>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> enumerables, bool stopWithShorter)
    {
      return Enumerate(enumerables.Item1, enumerables.Item2, enumerables.Item3, enumerables.Item4, enumerables.Item5, enumerables.Item6, stopWithShorter);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6>> Enumerate<T1, T2, T3, T4, T5, T6>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6)
    {
      return Enumerate(item1, item2, item3, item4, item5, item6, false);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6>> Enumerate<T1, T2, T3, T4, T5, T6>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, bool stopWithShorter)
    {
      return Enumerate(item1, item2, item3, item4, item5, item6, stopWithShorter ? StopsWith.Shorter : StopsWith.Longer);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6>> Enumerate<T1, T2, T3, T4, T5, T6>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, StopsWith stopsWith)
    {
      var item1Enum = item1.GetEnumerator();
      var item1Finished = false;
      var item2Enum = item2.GetEnumerator();
      var item2Finished = false;
      var item3Enum = item3.GetEnumerator();
      var item3Finished = false;
      var item4Enum = item4.GetEnumerator();
      var item4Finished = false;
      var item5Enum = item5.GetEnumerator();
      var item5Finished = false;
      var item6Enum = item6.GetEnumerator();
      var item6Finished = false;

      while (true)
      {
        int finishedCounter = 6;

        if (!item1Finished && !item1Enum.MoveNext())
          item1Finished = true;
        if (item1Finished) finishedCounter--;

        if (!item2Finished && !item2Enum.MoveNext())
          item2Finished = true;
        if (item2Finished) finishedCounter--;

        if (!item3Finished && !item3Enum.MoveNext())
          item3Finished = true;
        if (item3Finished) finishedCounter--;

        if (!item4Finished && !item4Enum.MoveNext())
          item4Finished = true;
        if (item4Finished) finishedCounter--;

        if (!item5Finished && !item5Enum.MoveNext())
          item5Finished = true;
        if (item5Finished) finishedCounter--;

        if (!item6Finished && !item6Enum.MoveNext())
          item6Finished = true;
        if (item6Finished) finishedCounter--;

        switch (stopsWith)
        {
          case StopsWith.Shorter:
            if (finishedCounter < 6)
              yield break;
            break;
          case StopsWith.Longer:
            if (finishedCounter == 0)
              yield break;
            break;
          case StopsWith.First:
            if (item1Finished)
              yield break;
            break;
        }

        //if (stopWithShorter && finishedCounter < 6 || !stopWithShorter && finishedCounter == 0)
        //  yield break;
        var tuple = Tup.Create(
          item1Finished ? default(T1) : item1Enum.Current, 
          item2Finished ? default(T2) : item2Enum.Current, 
          item3Finished ? default(T3) : item3Enum.Current, 
          item4Finished ? default(T4) : item4Enum.Current, 
          item5Finished ? default(T5) : item5Enum.Current, 
          item6Finished ? default(T6) : item6Enum.Current );
        yield return tuple;
      }
    }
    
    /// <summary>
    /// Creates an immutable 7-Tuple.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th element.</typeparam>
    /// <typeparam name="T2">The type of the 2th element.</typeparam>
    /// <typeparam name="T3">The type of the 3th element.</typeparam>
    /// <typeparam name="T4">The type of the 4th element.</typeparam>
    /// <typeparam name="T5">The type of the 5th element.</typeparam>
    /// <typeparam name="T6">The type of the 6th element.</typeparam>
    /// <typeparam name="T7">The type of the 7th element.</typeparam>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <param name="item5">The 5th element.</param>
    /// <param name="item6">The 6th element.</param>
    /// <param name="item7">The 7th element.</param>
    /// <returns>A new immutable 7-Tuple.</returns>
    public static Tup<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
    {
        return new Tup<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
    }

    public static Tup<T1, T2, T3, T4, T5, T6, T7> CreateTuple<T, T1, T2, T3, T4, T5, T6, T7>(this T obj, Func<T, T1> func1, Func<T, T2> func2, Func<T, T3> func3, Func<T, T4> func4, Func<T, T5> func5, Func<T, T6> func6, Func<T, T7> func7)
    {
      return Create(func1(obj), func2(obj), func3(obj), func4(obj), func5(obj), func6(obj), func7(obj));
    }
    
    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7>> Enumerate<T1, T2, T3, T4, T5, T6, T7>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>> enumerables)
    {
      return Enumerate(enumerables, true);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7>> Enumerate<T1, T2, T3, T4, T5, T6, T7>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>> enumerables, bool stopWithShorter)
    {
      return Enumerate(enumerables.Item1, enumerables.Item2, enumerables.Item3, enumerables.Item4, enumerables.Item5, enumerables.Item6, enumerables.Item7, stopWithShorter);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7>> Enumerate<T1, T2, T3, T4, T5, T6, T7>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7)
    {
      return Enumerate(item1, item2, item3, item4, item5, item6, item7, false);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7>> Enumerate<T1, T2, T3, T4, T5, T6, T7>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7, bool stopWithShorter)
    {
      return Enumerate(item1, item2, item3, item4, item5, item6, item7, stopWithShorter ? StopsWith.Shorter : StopsWith.Longer);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7>> Enumerate<T1, T2, T3, T4, T5, T6, T7>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7, StopsWith stopsWith)
    {
      var item1Enum = item1.GetEnumerator();
      var item1Finished = false;
      var item2Enum = item2.GetEnumerator();
      var item2Finished = false;
      var item3Enum = item3.GetEnumerator();
      var item3Finished = false;
      var item4Enum = item4.GetEnumerator();
      var item4Finished = false;
      var item5Enum = item5.GetEnumerator();
      var item5Finished = false;
      var item6Enum = item6.GetEnumerator();
      var item6Finished = false;
      var item7Enum = item7.GetEnumerator();
      var item7Finished = false;

      while (true)
      {
        int finishedCounter = 7;

        if (!item1Finished && !item1Enum.MoveNext())
          item1Finished = true;
        if (item1Finished) finishedCounter--;

        if (!item2Finished && !item2Enum.MoveNext())
          item2Finished = true;
        if (item2Finished) finishedCounter--;

        if (!item3Finished && !item3Enum.MoveNext())
          item3Finished = true;
        if (item3Finished) finishedCounter--;

        if (!item4Finished && !item4Enum.MoveNext())
          item4Finished = true;
        if (item4Finished) finishedCounter--;

        if (!item5Finished && !item5Enum.MoveNext())
          item5Finished = true;
        if (item5Finished) finishedCounter--;

        if (!item6Finished && !item6Enum.MoveNext())
          item6Finished = true;
        if (item6Finished) finishedCounter--;

        if (!item7Finished && !item7Enum.MoveNext())
          item7Finished = true;
        if (item7Finished) finishedCounter--;

        switch (stopsWith)
        {
          case StopsWith.Shorter:
            if (finishedCounter < 7)
              yield break;
            break;
          case StopsWith.Longer:
            if (finishedCounter == 0)
              yield break;
            break;
          case StopsWith.First:
            if (item1Finished)
              yield break;
            break;
        }

        //if (stopWithShorter && finishedCounter < 7 || !stopWithShorter && finishedCounter == 0)
        //  yield break;
        var tuple = Tup.Create(
          item1Finished ? default(T1) : item1Enum.Current, 
          item2Finished ? default(T2) : item2Enum.Current, 
          item3Finished ? default(T3) : item3Enum.Current, 
          item4Finished ? default(T4) : item4Enum.Current, 
          item5Finished ? default(T5) : item5Enum.Current, 
          item6Finished ? default(T6) : item6Enum.Current, 
          item7Finished ? default(T7) : item7Enum.Current );
        yield return tuple;
      }
    }
    
    /// <summary>
    /// Creates an immutable 8-Tuple.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th element.</typeparam>
    /// <typeparam name="T2">The type of the 2th element.</typeparam>
    /// <typeparam name="T3">The type of the 3th element.</typeparam>
    /// <typeparam name="T4">The type of the 4th element.</typeparam>
    /// <typeparam name="T5">The type of the 5th element.</typeparam>
    /// <typeparam name="T6">The type of the 6th element.</typeparam>
    /// <typeparam name="T7">The type of the 7th element.</typeparam>
    /// <typeparam name="T8">The type of the 8th element.</typeparam>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <param name="item5">The 5th element.</param>
    /// <param name="item6">The 6th element.</param>
    /// <param name="item7">The 7th element.</param>
    /// <param name="item8">The 8th element.</param>
    /// <returns>A new immutable 8-Tuple.</returns>
    public static Tup<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
    {
        return new Tup<T1, T2, T3, T4, T5, T6, T7, T8>(item1, item2, item3, item4, item5, item6, item7, item8);
    }

    public static Tup<T1, T2, T3, T4, T5, T6, T7, T8> CreateTuple<T, T1, T2, T3, T4, T5, T6, T7, T8>(this T obj, Func<T, T1> func1, Func<T, T2> func2, Func<T, T3> func3, Func<T, T4> func4, Func<T, T5> func5, Func<T, T6> func6, Func<T, T7> func7, Func<T, T8> func8)
    {
      return Create(func1(obj), func2(obj), func3(obj), func4(obj), func5(obj), func6(obj), func7(obj), func8(obj));
    }
    
    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, IEnumerable<T8>> enumerables)
    {
      return Enumerate(enumerables, true);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, IEnumerable<T8>> enumerables, bool stopWithShorter)
    {
      return Enumerate(enumerables.Item1, enumerables.Item2, enumerables.Item3, enumerables.Item4, enumerables.Item5, enumerables.Item6, enumerables.Item7, enumerables.Item8, stopWithShorter);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7, IEnumerable<T8> item8)
    {
      return Enumerate(item1, item2, item3, item4, item5, item6, item7, item8, false);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7, IEnumerable<T8> item8, bool stopWithShorter)
    {
      return Enumerate(item1, item2, item3, item4, item5, item6, item7, item8, stopWithShorter ? StopsWith.Shorter : StopsWith.Longer);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7, IEnumerable<T8> item8, StopsWith stopsWith)
    {
      var item1Enum = item1.GetEnumerator();
      var item1Finished = false;
      var item2Enum = item2.GetEnumerator();
      var item2Finished = false;
      var item3Enum = item3.GetEnumerator();
      var item3Finished = false;
      var item4Enum = item4.GetEnumerator();
      var item4Finished = false;
      var item5Enum = item5.GetEnumerator();
      var item5Finished = false;
      var item6Enum = item6.GetEnumerator();
      var item6Finished = false;
      var item7Enum = item7.GetEnumerator();
      var item7Finished = false;
      var item8Enum = item8.GetEnumerator();
      var item8Finished = false;

      while (true)
      {
        int finishedCounter = 8;

        if (!item1Finished && !item1Enum.MoveNext())
          item1Finished = true;
        if (item1Finished) finishedCounter--;

        if (!item2Finished && !item2Enum.MoveNext())
          item2Finished = true;
        if (item2Finished) finishedCounter--;

        if (!item3Finished && !item3Enum.MoveNext())
          item3Finished = true;
        if (item3Finished) finishedCounter--;

        if (!item4Finished && !item4Enum.MoveNext())
          item4Finished = true;
        if (item4Finished) finishedCounter--;

        if (!item5Finished && !item5Enum.MoveNext())
          item5Finished = true;
        if (item5Finished) finishedCounter--;

        if (!item6Finished && !item6Enum.MoveNext())
          item6Finished = true;
        if (item6Finished) finishedCounter--;

        if (!item7Finished && !item7Enum.MoveNext())
          item7Finished = true;
        if (item7Finished) finishedCounter--;

        if (!item8Finished && !item8Enum.MoveNext())
          item8Finished = true;
        if (item8Finished) finishedCounter--;

        switch (stopsWith)
        {
          case StopsWith.Shorter:
            if (finishedCounter < 8)
              yield break;
            break;
          case StopsWith.Longer:
            if (finishedCounter == 0)
              yield break;
            break;
          case StopsWith.First:
            if (item1Finished)
              yield break;
            break;
        }

        //if (stopWithShorter && finishedCounter < 8 || !stopWithShorter && finishedCounter == 0)
        //  yield break;
        var tuple = Tup.Create(
          item1Finished ? default(T1) : item1Enum.Current, 
          item2Finished ? default(T2) : item2Enum.Current, 
          item3Finished ? default(T3) : item3Enum.Current, 
          item4Finished ? default(T4) : item4Enum.Current, 
          item5Finished ? default(T5) : item5Enum.Current, 
          item6Finished ? default(T6) : item6Enum.Current, 
          item7Finished ? default(T7) : item7Enum.Current, 
          item8Finished ? default(T8) : item8Enum.Current );
        yield return tuple;
      }
    }
    
    /// <summary>
    /// Creates an immutable 9-Tuple.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th element.</typeparam>
    /// <typeparam name="T2">The type of the 2th element.</typeparam>
    /// <typeparam name="T3">The type of the 3th element.</typeparam>
    /// <typeparam name="T4">The type of the 4th element.</typeparam>
    /// <typeparam name="T5">The type of the 5th element.</typeparam>
    /// <typeparam name="T6">The type of the 6th element.</typeparam>
    /// <typeparam name="T7">The type of the 7th element.</typeparam>
    /// <typeparam name="T8">The type of the 8th element.</typeparam>
    /// <typeparam name="T9">The type of the 9th element.</typeparam>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <param name="item5">The 5th element.</param>
    /// <param name="item6">The 6th element.</param>
    /// <param name="item7">The 7th element.</param>
    /// <param name="item8">The 8th element.</param>
    /// <param name="item9">The 9th element.</param>
    /// <returns>A new immutable 9-Tuple.</returns>
    public static Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
    {
        return new Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item1, item2, item3, item4, item5, item6, item7, item8, item9);
    }

    public static Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9> CreateTuple<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T obj, Func<T, T1> func1, Func<T, T2> func2, Func<T, T3> func3, Func<T, T4> func4, Func<T, T5> func5, Func<T, T6> func6, Func<T, T7> func7, Func<T, T8> func8, Func<T, T9> func9)
    {
      return Create(func1(obj), func2(obj), func3(obj), func4(obj), func5(obj), func6(obj), func7(obj), func8(obj), func9(obj));
    }
    
    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, IEnumerable<T8>, IEnumerable<T9>> enumerables)
    {
      return Enumerate(enumerables, true);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, IEnumerable<T8>, IEnumerable<T9>> enumerables, bool stopWithShorter)
    {
      return Enumerate(enumerables.Item1, enumerables.Item2, enumerables.Item3, enumerables.Item4, enumerables.Item5, enumerables.Item6, enumerables.Item7, enumerables.Item8, enumerables.Item9, stopWithShorter);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7, IEnumerable<T8> item8, IEnumerable<T9> item9)
    {
      return Enumerate(item1, item2, item3, item4, item5, item6, item7, item8, item9, false);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7, IEnumerable<T8> item8, IEnumerable<T9> item9, bool stopWithShorter)
    {
      return Enumerate(item1, item2, item3, item4, item5, item6, item7, item8, item9, stopWithShorter ? StopsWith.Shorter : StopsWith.Longer);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7, IEnumerable<T8> item8, IEnumerable<T9> item9, StopsWith stopsWith)
    {
      var item1Enum = item1.GetEnumerator();
      var item1Finished = false;
      var item2Enum = item2.GetEnumerator();
      var item2Finished = false;
      var item3Enum = item3.GetEnumerator();
      var item3Finished = false;
      var item4Enum = item4.GetEnumerator();
      var item4Finished = false;
      var item5Enum = item5.GetEnumerator();
      var item5Finished = false;
      var item6Enum = item6.GetEnumerator();
      var item6Finished = false;
      var item7Enum = item7.GetEnumerator();
      var item7Finished = false;
      var item8Enum = item8.GetEnumerator();
      var item8Finished = false;
      var item9Enum = item9.GetEnumerator();
      var item9Finished = false;

      while (true)
      {
        int finishedCounter = 9;

        if (!item1Finished && !item1Enum.MoveNext())
          item1Finished = true;
        if (item1Finished) finishedCounter--;

        if (!item2Finished && !item2Enum.MoveNext())
          item2Finished = true;
        if (item2Finished) finishedCounter--;

        if (!item3Finished && !item3Enum.MoveNext())
          item3Finished = true;
        if (item3Finished) finishedCounter--;

        if (!item4Finished && !item4Enum.MoveNext())
          item4Finished = true;
        if (item4Finished) finishedCounter--;

        if (!item5Finished && !item5Enum.MoveNext())
          item5Finished = true;
        if (item5Finished) finishedCounter--;

        if (!item6Finished && !item6Enum.MoveNext())
          item6Finished = true;
        if (item6Finished) finishedCounter--;

        if (!item7Finished && !item7Enum.MoveNext())
          item7Finished = true;
        if (item7Finished) finishedCounter--;

        if (!item8Finished && !item8Enum.MoveNext())
          item8Finished = true;
        if (item8Finished) finishedCounter--;

        if (!item9Finished && !item9Enum.MoveNext())
          item9Finished = true;
        if (item9Finished) finishedCounter--;

        switch (stopsWith)
        {
          case StopsWith.Shorter:
            if (finishedCounter < 9)
              yield break;
            break;
          case StopsWith.Longer:
            if (finishedCounter == 0)
              yield break;
            break;
          case StopsWith.First:
            if (item1Finished)
              yield break;
            break;
        }

        //if (stopWithShorter && finishedCounter < 9 || !stopWithShorter && finishedCounter == 0)
        //  yield break;
        var tuple = Tup.Create(
          item1Finished ? default(T1) : item1Enum.Current, 
          item2Finished ? default(T2) : item2Enum.Current, 
          item3Finished ? default(T3) : item3Enum.Current, 
          item4Finished ? default(T4) : item4Enum.Current, 
          item5Finished ? default(T5) : item5Enum.Current, 
          item6Finished ? default(T6) : item6Enum.Current, 
          item7Finished ? default(T7) : item7Enum.Current, 
          item8Finished ? default(T8) : item8Enum.Current, 
          item9Finished ? default(T9) : item9Enum.Current );
        yield return tuple;
      }
    }
    
    /// <summary>
    /// Creates an immutable 10-Tuple.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th element.</typeparam>
    /// <typeparam name="T2">The type of the 2th element.</typeparam>
    /// <typeparam name="T3">The type of the 3th element.</typeparam>
    /// <typeparam name="T4">The type of the 4th element.</typeparam>
    /// <typeparam name="T5">The type of the 5th element.</typeparam>
    /// <typeparam name="T6">The type of the 6th element.</typeparam>
    /// <typeparam name="T7">The type of the 7th element.</typeparam>
    /// <typeparam name="T8">The type of the 8th element.</typeparam>
    /// <typeparam name="T9">The type of the 9th element.</typeparam>
    /// <typeparam name="T10">The type of the 10th element.</typeparam>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <param name="item5">The 5th element.</param>
    /// <param name="item6">The 6th element.</param>
    /// <param name="item7">The 7th element.</param>
    /// <param name="item8">The 8th element.</param>
    /// <param name="item9">The 9th element.</param>
    /// <param name="item10">The 10th element.</param>
    /// <returns>A new immutable 10-Tuple.</returns>
    public static Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10)
    {
        return new Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10);
    }

    public static Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> CreateTuple<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T obj, Func<T, T1> func1, Func<T, T2> func2, Func<T, T3> func3, Func<T, T4> func4, Func<T, T5> func5, Func<T, T6> func6, Func<T, T7> func7, Func<T, T8> func8, Func<T, T9> func9, Func<T, T10> func10)
    {
      return Create(func1(obj), func2(obj), func3(obj), func4(obj), func5(obj), func6(obj), func7(obj), func8(obj), func9(obj), func10(obj));
    }
    
    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>> enumerables)
    {
      return Enumerate(enumerables, true);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Tup<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>> enumerables, bool stopWithShorter)
    {
      return Enumerate(enumerables.Item1, enumerables.Item2, enumerables.Item3, enumerables.Item4, enumerables.Item5, enumerables.Item6, enumerables.Item7, enumerables.Item8, enumerables.Item9, enumerables.Item10, stopWithShorter);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7, IEnumerable<T8> item8, IEnumerable<T9> item9, IEnumerable<T10> item10)
    {
      return Enumerate(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, false);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7, IEnumerable<T8> item8, IEnumerable<T9> item9, IEnumerable<T10> item10, bool stopWithShorter)
    {
      return Enumerate(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, stopWithShorter ? StopsWith.Shorter : StopsWith.Longer);
    }

    public static IEnumerable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> Enumerate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IEnumerable<T1> item1, IEnumerable<T2> item2, IEnumerable<T3> item3, IEnumerable<T4> item4, IEnumerable<T5> item5, IEnumerable<T6> item6, IEnumerable<T7> item7, IEnumerable<T8> item8, IEnumerable<T9> item9, IEnumerable<T10> item10, StopsWith stopsWith)
    {
      var item1Enum = item1.GetEnumerator();
      var item1Finished = false;
      var item2Enum = item2.GetEnumerator();
      var item2Finished = false;
      var item3Enum = item3.GetEnumerator();
      var item3Finished = false;
      var item4Enum = item4.GetEnumerator();
      var item4Finished = false;
      var item5Enum = item5.GetEnumerator();
      var item5Finished = false;
      var item6Enum = item6.GetEnumerator();
      var item6Finished = false;
      var item7Enum = item7.GetEnumerator();
      var item7Finished = false;
      var item8Enum = item8.GetEnumerator();
      var item8Finished = false;
      var item9Enum = item9.GetEnumerator();
      var item9Finished = false;
      var item10Enum = item10.GetEnumerator();
      var item10Finished = false;

      while (true)
      {
        int finishedCounter = 10;

        if (!item1Finished && !item1Enum.MoveNext())
          item1Finished = true;
        if (item1Finished) finishedCounter--;

        if (!item2Finished && !item2Enum.MoveNext())
          item2Finished = true;
        if (item2Finished) finishedCounter--;

        if (!item3Finished && !item3Enum.MoveNext())
          item3Finished = true;
        if (item3Finished) finishedCounter--;

        if (!item4Finished && !item4Enum.MoveNext())
          item4Finished = true;
        if (item4Finished) finishedCounter--;

        if (!item5Finished && !item5Enum.MoveNext())
          item5Finished = true;
        if (item5Finished) finishedCounter--;

        if (!item6Finished && !item6Enum.MoveNext())
          item6Finished = true;
        if (item6Finished) finishedCounter--;

        if (!item7Finished && !item7Enum.MoveNext())
          item7Finished = true;
        if (item7Finished) finishedCounter--;

        if (!item8Finished && !item8Enum.MoveNext())
          item8Finished = true;
        if (item8Finished) finishedCounter--;

        if (!item9Finished && !item9Enum.MoveNext())
          item9Finished = true;
        if (item9Finished) finishedCounter--;

        if (!item10Finished && !item10Enum.MoveNext())
          item10Finished = true;
        if (item10Finished) finishedCounter--;

        switch (stopsWith)
        {
          case StopsWith.Shorter:
            if (finishedCounter < 10)
              yield break;
            break;
          case StopsWith.Longer:
            if (finishedCounter == 0)
              yield break;
            break;
          case StopsWith.First:
            if (item1Finished)
              yield break;
            break;
        }

        //if (stopWithShorter && finishedCounter < 10 || !stopWithShorter && finishedCounter == 0)
        //  yield break;
        var tuple = Tup.Create(
          item1Finished ? default(T1) : item1Enum.Current, 
          item2Finished ? default(T2) : item2Enum.Current, 
          item3Finished ? default(T3) : item3Enum.Current, 
          item4Finished ? default(T4) : item4Enum.Current, 
          item5Finished ? default(T5) : item5Enum.Current, 
          item6Finished ? default(T6) : item6Enum.Current, 
          item7Finished ? default(T7) : item7Enum.Current, 
          item8Finished ? default(T8) : item8Enum.Current, 
          item9Finished ? default(T9) : item9Enum.Current, 
          item10Finished ? default(T10) : item10Enum.Current );
        yield return tuple;
      }
    }
    
  }
}

