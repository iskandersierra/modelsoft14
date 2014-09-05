 

namespace ModelSoft.Framework
{
  using global::System;
  using global::System.ComponentModel;
  using global::System.Collections.Generic;
  using global::System.Collections;

  /// <summary>
  /// An immutable 1-tuple class.
  /// </summary>
  /// <typeparam name="T1">The type of the 1th element.</typeparam>
  /// <remarks>
  /// This class will be deprecated when VS2010 is released. At 
  /// that time, System.Tuple will be the preferred solution.
  /// </remarks>
  [Serializable]
  public sealed class Tup<T1> : IEquatable<Tup<T1>>, IEnumerable, ICloneable
  {
    private readonly T1 item1;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tup&lt;T1&gt;"/> class.
    /// </summary>
    /// <param name="item1">The 1th element.</param>
    public Tup(T1 item1)
    {
      this.item1 = item1;
    }

    /// <summary>
    /// Gets the 1th element of the tuple.
    /// </summary>
    /// <value>The 1th element of the tuple.</value>
    public T1 Item1
    {
      get { return item1; }
    }


    /// <summary>
    /// Selects a value from this tuple using a function. Allows you to 
    /// assign descriptive names to the tuple elements to increase
    /// readability.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="selector">The selector.</param>
    /// <returns></returns>
    public TResult Select<TResult>(Func<T1, TResult> selector)
    {
      selector.RequireNotNull("selector");

      return selector(Item1);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj"/> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
	  var otherTup = obj as Tup<T1>;
      return otherTup != null && Equals(otherTup);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Tup<T1> other)
    {
	  if (other == null) return false;
      if (!EqualityComparer<T1>.Default.Equals(item1, other.item1))
        return false;

      return true;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
      int result = EqualityComparer<T1>.Default.GetHashCode(item1);
      unchecked {
      }
      return result;
    }

    ///<summary>
    /// Returns an enumerator that iterates through the tuple elements.
    ///</summary>
    ///<returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the tuple elements.
    ///</returns>
    public IEnumerator GetEnumerator()
    {
      yield return item1;
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(Tup<T1> left, Tup<T1> right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(Tup<T1> left, Tup<T1> right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the tuple
    /// </summary>
    /// <returns>a string representation of the tuple</returns>
    public override string ToString()
    {
      return string.Format("<{0}>", item1);
    }

    object ICloneable.Clone()
    {
      return Clone();
    }

    public Tup<T1> Clone()
    {
      return Tup.Create(item1);
    }

    public static implicit operator System.Tuple<T1>(Tup<T1> t)
    {
      return System.Tuple.Create(t.item1);
    }

    public static implicit operator Tup<T1>(System.Tuple<T1> t)
    {
      return Tup.Create(t.Item1);
    }
  }

  /// <summary>
  /// An immutable 2-tuple class.
  /// </summary>
  /// <typeparam name="T1">The type of the 1th element.</typeparam>
  /// <typeparam name="T2">The type of the 2th element.</typeparam>
  /// <remarks>
  /// This class will be deprecated when VS2010 is released. At 
  /// that time, System.Tuple will be the preferred solution.
  /// </remarks>
  [Serializable]
  public sealed class Tup<T1, T2> : IEquatable<Tup<T1, T2>>, IEnumerable, ICloneable
  {
    private readonly T1 item1;
    private readonly T2 item2;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tup&lt;T1, T2&gt;"/> class.
    /// </summary>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    public Tup(T1 item1, T2 item2)
    {
      this.item1 = item1;
      this.item2 = item2;
    }

    /// <summary>
    /// Gets the 1th element of the tuple.
    /// </summary>
    /// <value>The 1th element of the tuple.</value>
    public T1 Item1
    {
      get { return item1; }
    }

    /// <summary>
    /// Gets the 2th element of the tuple.
    /// </summary>
    /// <value>The 2th element of the tuple.</value>
    public T2 Item2
    {
      get { return item2; }
    }


    /// <summary>
    /// Selects a value from this tuple using a function. Allows you to 
    /// assign descriptive names to the tuple elements to increase
    /// readability.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="selector">The selector.</param>
    /// <returns></returns>
    public TResult Select<TResult>(Func<T1, T2, TResult> selector)
    {
      selector.RequireNotNull("selector");

      return selector(Item1, Item2);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj"/> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
	  var otherTup = obj as Tup<T1, T2>;
      return otherTup != null && Equals(otherTup);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Tup<T1, T2> other)
    {
	  if (other == null) return false;
      if (!EqualityComparer<T1>.Default.Equals(item1, other.item1))
        return false;
      if (!EqualityComparer<T2>.Default.Equals(item2, other.item2))
        return false;

      return true;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
      int result = EqualityComparer<T1>.Default.GetHashCode(item1);
      unchecked {
        result = 397*result^EqualityComparer<T2>.Default.GetHashCode(item2);
      }
      return result;
    }

    ///<summary>
    /// Returns an enumerator that iterates through the tuple elements.
    ///</summary>
    ///<returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the tuple elements.
    ///</returns>
    public IEnumerator GetEnumerator()
    {
      yield return item1;
      yield return item2;
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(Tup<T1, T2> left, Tup<T1, T2> right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(Tup<T1, T2> left, Tup<T1, T2> right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the tuple
    /// </summary>
    /// <returns>a string representation of the tuple</returns>
    public override string ToString()
    {
      return string.Format("<{0}, {1}>", item1, item2);
    }

    object ICloneable.Clone()
    {
      return Clone();
    }

    public Tup<T1, T2> Clone()
    {
      return Tup.Create(item1, item2);
    }

    public static implicit operator System.Tuple<T1, T2>(Tup<T1, T2> t)
    {
      return System.Tuple.Create(t.item1, t.item2);
    }

    public static implicit operator Tup<T1, T2>(System.Tuple<T1, T2> t)
    {
      return Tup.Create(t.Item1, t.Item2);
    }
  }

  /// <summary>
  /// An immutable 3-tuple class.
  /// </summary>
  /// <typeparam name="T1">The type of the 1th element.</typeparam>
  /// <typeparam name="T2">The type of the 2th element.</typeparam>
  /// <typeparam name="T3">The type of the 3th element.</typeparam>
  /// <remarks>
  /// This class will be deprecated when VS2010 is released. At 
  /// that time, System.Tuple will be the preferred solution.
  /// </remarks>
  [Serializable]
  public sealed class Tup<T1, T2, T3> : IEquatable<Tup<T1, T2, T3>>, IEnumerable, ICloneable
  {
    private readonly T1 item1;
    private readonly T2 item2;
    private readonly T3 item3;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tup&lt;T1, T2, T3&gt;"/> class.
    /// </summary>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    public Tup(T1 item1, T2 item2, T3 item3)
    {
      this.item1 = item1;
      this.item2 = item2;
      this.item3 = item3;
    }

    /// <summary>
    /// Gets the 1th element of the tuple.
    /// </summary>
    /// <value>The 1th element of the tuple.</value>
    public T1 Item1
    {
      get { return item1; }
    }

    /// <summary>
    /// Gets the 2th element of the tuple.
    /// </summary>
    /// <value>The 2th element of the tuple.</value>
    public T2 Item2
    {
      get { return item2; }
    }

    /// <summary>
    /// Gets the 3th element of the tuple.
    /// </summary>
    /// <value>The 3th element of the tuple.</value>
    public T3 Item3
    {
      get { return item3; }
    }


    /// <summary>
    /// Selects a value from this tuple using a function. Allows you to 
    /// assign descriptive names to the tuple elements to increase
    /// readability.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="selector">The selector.</param>
    /// <returns></returns>
    public TResult Select<TResult>(Func<T1, T2, T3, TResult> selector)
    {
      selector.RequireNotNull("selector");

      return selector(Item1, Item2, Item3);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj"/> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
	  var otherTup = obj as Tup<T1, T2, T3>;
      return otherTup != null && Equals(otherTup);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Tup<T1, T2, T3> other)
    {
	  if (other == null) return false;
      if (!EqualityComparer<T1>.Default.Equals(item1, other.item1))
        return false;
      if (!EqualityComparer<T2>.Default.Equals(item2, other.item2))
        return false;
      if (!EqualityComparer<T3>.Default.Equals(item3, other.item3))
        return false;

      return true;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
      int result = EqualityComparer<T1>.Default.GetHashCode(item1);
      unchecked {
        result = 397*result^EqualityComparer<T2>.Default.GetHashCode(item2);
        result = 397*result^EqualityComparer<T3>.Default.GetHashCode(item3);
      }
      return result;
    }

    ///<summary>
    /// Returns an enumerator that iterates through the tuple elements.
    ///</summary>
    ///<returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the tuple elements.
    ///</returns>
    public IEnumerator GetEnumerator()
    {
      yield return item1;
      yield return item2;
      yield return item3;
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(Tup<T1, T2, T3> left, Tup<T1, T2, T3> right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(Tup<T1, T2, T3> left, Tup<T1, T2, T3> right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the tuple
    /// </summary>
    /// <returns>a string representation of the tuple</returns>
    public override string ToString()
    {
      return string.Format("<{0}, {1}, {2}>", item1, item2, item3);
    }

    object ICloneable.Clone()
    {
      return Clone();
    }

    public Tup<T1, T2, T3> Clone()
    {
      return Tup.Create(item1, item2, item3);
    }

    public static implicit operator System.Tuple<T1, T2, T3>(Tup<T1, T2, T3> t)
    {
      return System.Tuple.Create(t.item1, t.item2, t.item3);
    }

    public static implicit operator Tup<T1, T2, T3>(System.Tuple<T1, T2, T3> t)
    {
      return Tup.Create(t.Item1, t.Item2, t.Item3);
    }
  }

  /// <summary>
  /// An immutable 4-tuple class.
  /// </summary>
  /// <typeparam name="T1">The type of the 1th element.</typeparam>
  /// <typeparam name="T2">The type of the 2th element.</typeparam>
  /// <typeparam name="T3">The type of the 3th element.</typeparam>
  /// <typeparam name="T4">The type of the 4th element.</typeparam>
  /// <remarks>
  /// This class will be deprecated when VS2010 is released. At 
  /// that time, System.Tuple will be the preferred solution.
  /// </remarks>
  [Serializable]
  public sealed class Tup<T1, T2, T3, T4> : IEquatable<Tup<T1, T2, T3, T4>>, IEnumerable, ICloneable
  {
    private readonly T1 item1;
    private readonly T2 item2;
    private readonly T3 item3;
    private readonly T4 item4;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tup&lt;T1, T2, T3, T4&gt;"/> class.
    /// </summary>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    public Tup(T1 item1, T2 item2, T3 item3, T4 item4)
    {
      this.item1 = item1;
      this.item2 = item2;
      this.item3 = item3;
      this.item4 = item4;
    }

    /// <summary>
    /// Gets the 1th element of the tuple.
    /// </summary>
    /// <value>The 1th element of the tuple.</value>
    public T1 Item1
    {
      get { return item1; }
    }

    /// <summary>
    /// Gets the 2th element of the tuple.
    /// </summary>
    /// <value>The 2th element of the tuple.</value>
    public T2 Item2
    {
      get { return item2; }
    }

    /// <summary>
    /// Gets the 3th element of the tuple.
    /// </summary>
    /// <value>The 3th element of the tuple.</value>
    public T3 Item3
    {
      get { return item3; }
    }

    /// <summary>
    /// Gets the 4th element of the tuple.
    /// </summary>
    /// <value>The 4th element of the tuple.</value>
    public T4 Item4
    {
      get { return item4; }
    }


    /// <summary>
    /// Selects a value from this tuple using a function. Allows you to 
    /// assign descriptive names to the tuple elements to increase
    /// readability.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="selector">The selector.</param>
    /// <returns></returns>
    public TResult Select<TResult>(Func<T1, T2, T3, T4, TResult> selector)
    {
      selector.RequireNotNull("selector");

      return selector(Item1, Item2, Item3, Item4);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj"/> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
	  var otherTup = obj as Tup<T1, T2, T3, T4>;
      return otherTup != null && Equals(otherTup);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Tup<T1, T2, T3, T4> other)
    {
	  if (other == null) return false;
      if (!EqualityComparer<T1>.Default.Equals(item1, other.item1))
        return false;
      if (!EqualityComparer<T2>.Default.Equals(item2, other.item2))
        return false;
      if (!EqualityComparer<T3>.Default.Equals(item3, other.item3))
        return false;
      if (!EqualityComparer<T4>.Default.Equals(item4, other.item4))
        return false;

      return true;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
      int result = EqualityComparer<T1>.Default.GetHashCode(item1);
      unchecked {
        result = 397*result^EqualityComparer<T2>.Default.GetHashCode(item2);
        result = 397*result^EqualityComparer<T3>.Default.GetHashCode(item3);
        result = 397*result^EqualityComparer<T4>.Default.GetHashCode(item4);
      }
      return result;
    }

    ///<summary>
    /// Returns an enumerator that iterates through the tuple elements.
    ///</summary>
    ///<returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the tuple elements.
    ///</returns>
    public IEnumerator GetEnumerator()
    {
      yield return item1;
      yield return item2;
      yield return item3;
      yield return item4;
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(Tup<T1, T2, T3, T4> left, Tup<T1, T2, T3, T4> right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(Tup<T1, T2, T3, T4> left, Tup<T1, T2, T3, T4> right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the tuple
    /// </summary>
    /// <returns>a string representation of the tuple</returns>
    public override string ToString()
    {
      return string.Format("<{0}, {1}, {2}, {3}>", item1, item2, item3, item4);
    }

    object ICloneable.Clone()
    {
      return Clone();
    }

    public Tup<T1, T2, T3, T4> Clone()
    {
      return Tup.Create(item1, item2, item3, item4);
    }

    public static implicit operator System.Tuple<T1, T2, T3, T4>(Tup<T1, T2, T3, T4> t)
    {
      return System.Tuple.Create(t.item1, t.item2, t.item3, t.item4);
    }

    public static implicit operator Tup<T1, T2, T3, T4>(System.Tuple<T1, T2, T3, T4> t)
    {
      return Tup.Create(t.Item1, t.Item2, t.Item3, t.Item4);
    }
  }

  /// <summary>
  /// An immutable 5-tuple class.
  /// </summary>
  /// <typeparam name="T1">The type of the 1th element.</typeparam>
  /// <typeparam name="T2">The type of the 2th element.</typeparam>
  /// <typeparam name="T3">The type of the 3th element.</typeparam>
  /// <typeparam name="T4">The type of the 4th element.</typeparam>
  /// <typeparam name="T5">The type of the 5th element.</typeparam>
  /// <remarks>
  /// This class will be deprecated when VS2010 is released. At 
  /// that time, System.Tuple will be the preferred solution.
  /// </remarks>
  [Serializable]
  public sealed class Tup<T1, T2, T3, T4, T5> : IEquatable<Tup<T1, T2, T3, T4, T5>>, IEnumerable, ICloneable
  {
    private readonly T1 item1;
    private readonly T2 item2;
    private readonly T3 item3;
    private readonly T4 item4;
    private readonly T5 item5;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tup&lt;T1, T2, T3, T4, T5&gt;"/> class.
    /// </summary>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <param name="item5">The 5th element.</param>
    public Tup(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
      this.item1 = item1;
      this.item2 = item2;
      this.item3 = item3;
      this.item4 = item4;
      this.item5 = item5;
    }

    /// <summary>
    /// Gets the 1th element of the tuple.
    /// </summary>
    /// <value>The 1th element of the tuple.</value>
    public T1 Item1
    {
      get { return item1; }
    }

    /// <summary>
    /// Gets the 2th element of the tuple.
    /// </summary>
    /// <value>The 2th element of the tuple.</value>
    public T2 Item2
    {
      get { return item2; }
    }

    /// <summary>
    /// Gets the 3th element of the tuple.
    /// </summary>
    /// <value>The 3th element of the tuple.</value>
    public T3 Item3
    {
      get { return item3; }
    }

    /// <summary>
    /// Gets the 4th element of the tuple.
    /// </summary>
    /// <value>The 4th element of the tuple.</value>
    public T4 Item4
    {
      get { return item4; }
    }

    /// <summary>
    /// Gets the 5th element of the tuple.
    /// </summary>
    /// <value>The 5th element of the tuple.</value>
    public T5 Item5
    {
      get { return item5; }
    }


    /// <summary>
    /// Selects a value from this tuple using a function. Allows you to 
    /// assign descriptive names to the tuple elements to increase
    /// readability.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="selector">The selector.</param>
    /// <returns></returns>
    public TResult Select<TResult>(Func<T1, T2, T3, T4, T5, TResult> selector)
    {
      selector.RequireNotNull("selector");

      return selector(Item1, Item2, Item3, Item4, Item5);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj"/> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
	  var otherTup = obj as Tup<T1, T2, T3, T4, T5>;
      return otherTup != null && Equals(otherTup);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Tup<T1, T2, T3, T4, T5> other)
    {
	  if (other == null) return false;
      if (!EqualityComparer<T1>.Default.Equals(item1, other.item1))
        return false;
      if (!EqualityComparer<T2>.Default.Equals(item2, other.item2))
        return false;
      if (!EqualityComparer<T3>.Default.Equals(item3, other.item3))
        return false;
      if (!EqualityComparer<T4>.Default.Equals(item4, other.item4))
        return false;
      if (!EqualityComparer<T5>.Default.Equals(item5, other.item5))
        return false;

      return true;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
      int result = EqualityComparer<T1>.Default.GetHashCode(item1);
      unchecked {
        result = 397*result^EqualityComparer<T2>.Default.GetHashCode(item2);
        result = 397*result^EqualityComparer<T3>.Default.GetHashCode(item3);
        result = 397*result^EqualityComparer<T4>.Default.GetHashCode(item4);
        result = 397*result^EqualityComparer<T5>.Default.GetHashCode(item5);
      }
      return result;
    }

    ///<summary>
    /// Returns an enumerator that iterates through the tuple elements.
    ///</summary>
    ///<returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the tuple elements.
    ///</returns>
    public IEnumerator GetEnumerator()
    {
      yield return item1;
      yield return item2;
      yield return item3;
      yield return item4;
      yield return item5;
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(Tup<T1, T2, T3, T4, T5> left, Tup<T1, T2, T3, T4, T5> right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(Tup<T1, T2, T3, T4, T5> left, Tup<T1, T2, T3, T4, T5> right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the tuple
    /// </summary>
    /// <returns>a string representation of the tuple</returns>
    public override string ToString()
    {
      return string.Format("<{0}, {1}, {2}, {3}, {4}>", item1, item2, item3, item4, item5);
    }

    object ICloneable.Clone()
    {
      return Clone();
    }

    public Tup<T1, T2, T3, T4, T5> Clone()
    {
      return Tup.Create(item1, item2, item3, item4, item5);
    }

    public static implicit operator System.Tuple<T1, T2, T3, T4, T5>(Tup<T1, T2, T3, T4, T5> t)
    {
      return System.Tuple.Create(t.item1, t.item2, t.item3, t.item4, t.item5);
    }

    public static implicit operator Tup<T1, T2, T3, T4, T5>(System.Tuple<T1, T2, T3, T4, T5> t)
    {
      return Tup.Create(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5);
    }
  }

  /// <summary>
  /// An immutable 6-tuple class.
  /// </summary>
  /// <typeparam name="T1">The type of the 1th element.</typeparam>
  /// <typeparam name="T2">The type of the 2th element.</typeparam>
  /// <typeparam name="T3">The type of the 3th element.</typeparam>
  /// <typeparam name="T4">The type of the 4th element.</typeparam>
  /// <typeparam name="T5">The type of the 5th element.</typeparam>
  /// <typeparam name="T6">The type of the 6th element.</typeparam>
  /// <remarks>
  /// This class will be deprecated when VS2010 is released. At 
  /// that time, System.Tuple will be the preferred solution.
  /// </remarks>
  [Serializable]
  public sealed class Tup<T1, T2, T3, T4, T5, T6> : IEquatable<Tup<T1, T2, T3, T4, T5, T6>>, IEnumerable, ICloneable
  {
    private readonly T1 item1;
    private readonly T2 item2;
    private readonly T3 item3;
    private readonly T4 item4;
    private readonly T5 item5;
    private readonly T6 item6;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tup&lt;T1, T2, T3, T4, T5, T6&gt;"/> class.
    /// </summary>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <param name="item5">The 5th element.</param>
    /// <param name="item6">The 6th element.</param>
    public Tup(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
    {
      this.item1 = item1;
      this.item2 = item2;
      this.item3 = item3;
      this.item4 = item4;
      this.item5 = item5;
      this.item6 = item6;
    }

    /// <summary>
    /// Gets the 1th element of the tuple.
    /// </summary>
    /// <value>The 1th element of the tuple.</value>
    public T1 Item1
    {
      get { return item1; }
    }

    /// <summary>
    /// Gets the 2th element of the tuple.
    /// </summary>
    /// <value>The 2th element of the tuple.</value>
    public T2 Item2
    {
      get { return item2; }
    }

    /// <summary>
    /// Gets the 3th element of the tuple.
    /// </summary>
    /// <value>The 3th element of the tuple.</value>
    public T3 Item3
    {
      get { return item3; }
    }

    /// <summary>
    /// Gets the 4th element of the tuple.
    /// </summary>
    /// <value>The 4th element of the tuple.</value>
    public T4 Item4
    {
      get { return item4; }
    }

    /// <summary>
    /// Gets the 5th element of the tuple.
    /// </summary>
    /// <value>The 5th element of the tuple.</value>
    public T5 Item5
    {
      get { return item5; }
    }

    /// <summary>
    /// Gets the 6th element of the tuple.
    /// </summary>
    /// <value>The 6th element of the tuple.</value>
    public T6 Item6
    {
      get { return item6; }
    }


    /// <summary>
    /// Selects a value from this tuple using a function. Allows you to 
    /// assign descriptive names to the tuple elements to increase
    /// readability.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="selector">The selector.</param>
    /// <returns></returns>
    public TResult Select<TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> selector)
    {
      selector.RequireNotNull("selector");

      return selector(Item1, Item2, Item3, Item4, Item5, Item6);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj"/> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
	  var otherTup = obj as Tup<T1, T2, T3, T4, T5, T6>;
      return otherTup != null && Equals(otherTup);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Tup<T1, T2, T3, T4, T5, T6> other)
    {
	  if (other == null) return false;
      if (!EqualityComparer<T1>.Default.Equals(item1, other.item1))
        return false;
      if (!EqualityComparer<T2>.Default.Equals(item2, other.item2))
        return false;
      if (!EqualityComparer<T3>.Default.Equals(item3, other.item3))
        return false;
      if (!EqualityComparer<T4>.Default.Equals(item4, other.item4))
        return false;
      if (!EqualityComparer<T5>.Default.Equals(item5, other.item5))
        return false;
      if (!EqualityComparer<T6>.Default.Equals(item6, other.item6))
        return false;

      return true;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
      int result = EqualityComparer<T1>.Default.GetHashCode(item1);
      unchecked {
        result = 397*result^EqualityComparer<T2>.Default.GetHashCode(item2);
        result = 397*result^EqualityComparer<T3>.Default.GetHashCode(item3);
        result = 397*result^EqualityComparer<T4>.Default.GetHashCode(item4);
        result = 397*result^EqualityComparer<T5>.Default.GetHashCode(item5);
        result = 397*result^EqualityComparer<T6>.Default.GetHashCode(item6);
      }
      return result;
    }

    ///<summary>
    /// Returns an enumerator that iterates through the tuple elements.
    ///</summary>
    ///<returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the tuple elements.
    ///</returns>
    public IEnumerator GetEnumerator()
    {
      yield return item1;
      yield return item2;
      yield return item3;
      yield return item4;
      yield return item5;
      yield return item6;
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(Tup<T1, T2, T3, T4, T5, T6> left, Tup<T1, T2, T3, T4, T5, T6> right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(Tup<T1, T2, T3, T4, T5, T6> left, Tup<T1, T2, T3, T4, T5, T6> right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the tuple
    /// </summary>
    /// <returns>a string representation of the tuple</returns>
    public override string ToString()
    {
      return string.Format("<{0}, {1}, {2}, {3}, {4}, {5}>", item1, item2, item3, item4, item5, item6);
    }

    object ICloneable.Clone()
    {
      return Clone();
    }

    public Tup<T1, T2, T3, T4, T5, T6> Clone()
    {
      return Tup.Create(item1, item2, item3, item4, item5, item6);
    }

    public static implicit operator System.Tuple<T1, T2, T3, T4, T5, T6>(Tup<T1, T2, T3, T4, T5, T6> t)
    {
      return System.Tuple.Create(t.item1, t.item2, t.item3, t.item4, t.item5, t.item6);
    }

    public static implicit operator Tup<T1, T2, T3, T4, T5, T6>(System.Tuple<T1, T2, T3, T4, T5, T6> t)
    {
      return Tup.Create(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6);
    }
  }

  /// <summary>
  /// An immutable 7-tuple class.
  /// </summary>
  /// <typeparam name="T1">The type of the 1th element.</typeparam>
  /// <typeparam name="T2">The type of the 2th element.</typeparam>
  /// <typeparam name="T3">The type of the 3th element.</typeparam>
  /// <typeparam name="T4">The type of the 4th element.</typeparam>
  /// <typeparam name="T5">The type of the 5th element.</typeparam>
  /// <typeparam name="T6">The type of the 6th element.</typeparam>
  /// <typeparam name="T7">The type of the 7th element.</typeparam>
  /// <remarks>
  /// This class will be deprecated when VS2010 is released. At 
  /// that time, System.Tuple will be the preferred solution.
  /// </remarks>
  [Serializable]
  public sealed class Tup<T1, T2, T3, T4, T5, T6, T7> : IEquatable<Tup<T1, T2, T3, T4, T5, T6, T7>>, IEnumerable, ICloneable
  {
    private readonly T1 item1;
    private readonly T2 item2;
    private readonly T3 item3;
    private readonly T4 item4;
    private readonly T5 item5;
    private readonly T6 item6;
    private readonly T7 item7;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tup&lt;T1, T2, T3, T4, T5, T6, T7&gt;"/> class.
    /// </summary>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <param name="item5">The 5th element.</param>
    /// <param name="item6">The 6th element.</param>
    /// <param name="item7">The 7th element.</param>
    public Tup(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
    {
      this.item1 = item1;
      this.item2 = item2;
      this.item3 = item3;
      this.item4 = item4;
      this.item5 = item5;
      this.item6 = item6;
      this.item7 = item7;
    }

    /// <summary>
    /// Gets the 1th element of the tuple.
    /// </summary>
    /// <value>The 1th element of the tuple.</value>
    public T1 Item1
    {
      get { return item1; }
    }

    /// <summary>
    /// Gets the 2th element of the tuple.
    /// </summary>
    /// <value>The 2th element of the tuple.</value>
    public T2 Item2
    {
      get { return item2; }
    }

    /// <summary>
    /// Gets the 3th element of the tuple.
    /// </summary>
    /// <value>The 3th element of the tuple.</value>
    public T3 Item3
    {
      get { return item3; }
    }

    /// <summary>
    /// Gets the 4th element of the tuple.
    /// </summary>
    /// <value>The 4th element of the tuple.</value>
    public T4 Item4
    {
      get { return item4; }
    }

    /// <summary>
    /// Gets the 5th element of the tuple.
    /// </summary>
    /// <value>The 5th element of the tuple.</value>
    public T5 Item5
    {
      get { return item5; }
    }

    /// <summary>
    /// Gets the 6th element of the tuple.
    /// </summary>
    /// <value>The 6th element of the tuple.</value>
    public T6 Item6
    {
      get { return item6; }
    }

    /// <summary>
    /// Gets the 7th element of the tuple.
    /// </summary>
    /// <value>The 7th element of the tuple.</value>
    public T7 Item7
    {
      get { return item7; }
    }


    /// <summary>
    /// Selects a value from this tuple using a function. Allows you to 
    /// assign descriptive names to the tuple elements to increase
    /// readability.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="selector">The selector.</param>
    /// <returns></returns>
    public TResult Select<TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector)
    {
      selector.RequireNotNull("selector");

      return selector(Item1, Item2, Item3, Item4, Item5, Item6, Item7);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj"/> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
	  var otherTup = obj as Tup<T1, T2, T3, T4, T5, T6, T7>;
      return otherTup != null && Equals(otherTup);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Tup<T1, T2, T3, T4, T5, T6, T7> other)
    {
	  if (other == null) return false;
      if (!EqualityComparer<T1>.Default.Equals(item1, other.item1))
        return false;
      if (!EqualityComparer<T2>.Default.Equals(item2, other.item2))
        return false;
      if (!EqualityComparer<T3>.Default.Equals(item3, other.item3))
        return false;
      if (!EqualityComparer<T4>.Default.Equals(item4, other.item4))
        return false;
      if (!EqualityComparer<T5>.Default.Equals(item5, other.item5))
        return false;
      if (!EqualityComparer<T6>.Default.Equals(item6, other.item6))
        return false;
      if (!EqualityComparer<T7>.Default.Equals(item7, other.item7))
        return false;

      return true;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
      int result = EqualityComparer<T1>.Default.GetHashCode(item1);
      unchecked {
        result = 397*result^EqualityComparer<T2>.Default.GetHashCode(item2);
        result = 397*result^EqualityComparer<T3>.Default.GetHashCode(item3);
        result = 397*result^EqualityComparer<T4>.Default.GetHashCode(item4);
        result = 397*result^EqualityComparer<T5>.Default.GetHashCode(item5);
        result = 397*result^EqualityComparer<T6>.Default.GetHashCode(item6);
        result = 397*result^EqualityComparer<T7>.Default.GetHashCode(item7);
      }
      return result;
    }

    ///<summary>
    /// Returns an enumerator that iterates through the tuple elements.
    ///</summary>
    ///<returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the tuple elements.
    ///</returns>
    public IEnumerator GetEnumerator()
    {
      yield return item1;
      yield return item2;
      yield return item3;
      yield return item4;
      yield return item5;
      yield return item6;
      yield return item7;
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(Tup<T1, T2, T3, T4, T5, T6, T7> left, Tup<T1, T2, T3, T4, T5, T6, T7> right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(Tup<T1, T2, T3, T4, T5, T6, T7> left, Tup<T1, T2, T3, T4, T5, T6, T7> right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the tuple
    /// </summary>
    /// <returns>a string representation of the tuple</returns>
    public override string ToString()
    {
      return string.Format("<{0}, {1}, {2}, {3}, {4}, {5}, {6}>", item1, item2, item3, item4, item5, item6, item7);
    }

    object ICloneable.Clone()
    {
      return Clone();
    }

    public Tup<T1, T2, T3, T4, T5, T6, T7> Clone()
    {
      return Tup.Create(item1, item2, item3, item4, item5, item6, item7);
    }

    public static implicit operator System.Tuple<T1, T2, T3, T4, T5, T6, T7>(Tup<T1, T2, T3, T4, T5, T6, T7> t)
    {
      return System.Tuple.Create(t.item1, t.item2, t.item3, t.item4, t.item5, t.item6, t.item7);
    }

    public static implicit operator Tup<T1, T2, T3, T4, T5, T6, T7>(System.Tuple<T1, T2, T3, T4, T5, T6, T7> t)
    {
      return Tup.Create(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6, t.Item7);
    }
  }

  /// <summary>
  /// An immutable 8-tuple class.
  /// </summary>
  /// <typeparam name="T1">The type of the 1th element.</typeparam>
  /// <typeparam name="T2">The type of the 2th element.</typeparam>
  /// <typeparam name="T3">The type of the 3th element.</typeparam>
  /// <typeparam name="T4">The type of the 4th element.</typeparam>
  /// <typeparam name="T5">The type of the 5th element.</typeparam>
  /// <typeparam name="T6">The type of the 6th element.</typeparam>
  /// <typeparam name="T7">The type of the 7th element.</typeparam>
  /// <typeparam name="T8">The type of the 8th element.</typeparam>
  /// <remarks>
  /// This class will be deprecated when VS2010 is released. At 
  /// that time, System.Tuple will be the preferred solution.
  /// </remarks>
  [Serializable]
  public sealed class Tup<T1, T2, T3, T4, T5, T6, T7, T8> : IEquatable<Tup<T1, T2, T3, T4, T5, T6, T7, T8>>, IEnumerable, ICloneable
  {
    private readonly T1 item1;
    private readonly T2 item2;
    private readonly T3 item3;
    private readonly T4 item4;
    private readonly T5 item5;
    private readonly T6 item6;
    private readonly T7 item7;
    private readonly T8 item8;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tup&lt;T1, T2, T3, T4, T5, T6, T7, T8&gt;"/> class.
    /// </summary>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <param name="item5">The 5th element.</param>
    /// <param name="item6">The 6th element.</param>
    /// <param name="item7">The 7th element.</param>
    /// <param name="item8">The 8th element.</param>
    public Tup(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
    {
      this.item1 = item1;
      this.item2 = item2;
      this.item3 = item3;
      this.item4 = item4;
      this.item5 = item5;
      this.item6 = item6;
      this.item7 = item7;
      this.item8 = item8;
    }

    /// <summary>
    /// Gets the 1th element of the tuple.
    /// </summary>
    /// <value>The 1th element of the tuple.</value>
    public T1 Item1
    {
      get { return item1; }
    }

    /// <summary>
    /// Gets the 2th element of the tuple.
    /// </summary>
    /// <value>The 2th element of the tuple.</value>
    public T2 Item2
    {
      get { return item2; }
    }

    /// <summary>
    /// Gets the 3th element of the tuple.
    /// </summary>
    /// <value>The 3th element of the tuple.</value>
    public T3 Item3
    {
      get { return item3; }
    }

    /// <summary>
    /// Gets the 4th element of the tuple.
    /// </summary>
    /// <value>The 4th element of the tuple.</value>
    public T4 Item4
    {
      get { return item4; }
    }

    /// <summary>
    /// Gets the 5th element of the tuple.
    /// </summary>
    /// <value>The 5th element of the tuple.</value>
    public T5 Item5
    {
      get { return item5; }
    }

    /// <summary>
    /// Gets the 6th element of the tuple.
    /// </summary>
    /// <value>The 6th element of the tuple.</value>
    public T6 Item6
    {
      get { return item6; }
    }

    /// <summary>
    /// Gets the 7th element of the tuple.
    /// </summary>
    /// <value>The 7th element of the tuple.</value>
    public T7 Item7
    {
      get { return item7; }
    }

    /// <summary>
    /// Gets the 8th element of the tuple.
    /// </summary>
    /// <value>The 8th element of the tuple.</value>
    public T8 Item8
    {
      get { return item8; }
    }


    /// <summary>
    /// Selects a value from this tuple using a function. Allows you to 
    /// assign descriptive names to the tuple elements to increase
    /// readability.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="selector">The selector.</param>
    /// <returns></returns>
    public TResult Select<TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector)
    {
      selector.RequireNotNull("selector");

      return selector(Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj"/> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
	  var otherTup = obj as Tup<T1, T2, T3, T4, T5, T6, T7, T8>;
      return otherTup != null && Equals(otherTup);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Tup<T1, T2, T3, T4, T5, T6, T7, T8> other)
    {
	  if (other == null) return false;
      if (!EqualityComparer<T1>.Default.Equals(item1, other.item1))
        return false;
      if (!EqualityComparer<T2>.Default.Equals(item2, other.item2))
        return false;
      if (!EqualityComparer<T3>.Default.Equals(item3, other.item3))
        return false;
      if (!EqualityComparer<T4>.Default.Equals(item4, other.item4))
        return false;
      if (!EqualityComparer<T5>.Default.Equals(item5, other.item5))
        return false;
      if (!EqualityComparer<T6>.Default.Equals(item6, other.item6))
        return false;
      if (!EqualityComparer<T7>.Default.Equals(item7, other.item7))
        return false;
      if (!EqualityComparer<T8>.Default.Equals(item8, other.item8))
        return false;

      return true;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
      int result = EqualityComparer<T1>.Default.GetHashCode(item1);
      unchecked {
        result = 397*result^EqualityComparer<T2>.Default.GetHashCode(item2);
        result = 397*result^EqualityComparer<T3>.Default.GetHashCode(item3);
        result = 397*result^EqualityComparer<T4>.Default.GetHashCode(item4);
        result = 397*result^EqualityComparer<T5>.Default.GetHashCode(item5);
        result = 397*result^EqualityComparer<T6>.Default.GetHashCode(item6);
        result = 397*result^EqualityComparer<T7>.Default.GetHashCode(item7);
        result = 397*result^EqualityComparer<T8>.Default.GetHashCode(item8);
      }
      return result;
    }

    ///<summary>
    /// Returns an enumerator that iterates through the tuple elements.
    ///</summary>
    ///<returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the tuple elements.
    ///</returns>
    public IEnumerator GetEnumerator()
    {
      yield return item1;
      yield return item2;
      yield return item3;
      yield return item4;
      yield return item5;
      yield return item6;
      yield return item7;
      yield return item8;
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(Tup<T1, T2, T3, T4, T5, T6, T7, T8> left, Tup<T1, T2, T3, T4, T5, T6, T7, T8> right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(Tup<T1, T2, T3, T4, T5, T6, T7, T8> left, Tup<T1, T2, T3, T4, T5, T6, T7, T8> right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the tuple
    /// </summary>
    /// <returns>a string representation of the tuple</returns>
    public override string ToString()
    {
      return string.Format("<{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}>", item1, item2, item3, item4, item5, item6, item7, item8);
    }

    object ICloneable.Clone()
    {
      return Clone();
    }

    public Tup<T1, T2, T3, T4, T5, T6, T7, T8> Clone()
    {
      return Tup.Create(item1, item2, item3, item4, item5, item6, item7, item8);
    }
  }

  /// <summary>
  /// An immutable 9-tuple class.
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
  /// <remarks>
  /// This class will be deprecated when VS2010 is released. At 
  /// that time, System.Tuple will be the preferred solution.
  /// </remarks>
  [Serializable]
  public sealed class Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IEquatable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9>>, IEnumerable, ICloneable
  {
    private readonly T1 item1;
    private readonly T2 item2;
    private readonly T3 item3;
    private readonly T4 item4;
    private readonly T5 item5;
    private readonly T6 item6;
    private readonly T7 item7;
    private readonly T8 item8;
    private readonly T9 item9;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tup&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9&gt;"/> class.
    /// </summary>
    /// <param name="item1">The 1th element.</param>
    /// <param name="item2">The 2th element.</param>
    /// <param name="item3">The 3th element.</param>
    /// <param name="item4">The 4th element.</param>
    /// <param name="item5">The 5th element.</param>
    /// <param name="item6">The 6th element.</param>
    /// <param name="item7">The 7th element.</param>
    /// <param name="item8">The 8th element.</param>
    /// <param name="item9">The 9th element.</param>
    public Tup(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
    {
      this.item1 = item1;
      this.item2 = item2;
      this.item3 = item3;
      this.item4 = item4;
      this.item5 = item5;
      this.item6 = item6;
      this.item7 = item7;
      this.item8 = item8;
      this.item9 = item9;
    }

    /// <summary>
    /// Gets the 1th element of the tuple.
    /// </summary>
    /// <value>The 1th element of the tuple.</value>
    public T1 Item1
    {
      get { return item1; }
    }

    /// <summary>
    /// Gets the 2th element of the tuple.
    /// </summary>
    /// <value>The 2th element of the tuple.</value>
    public T2 Item2
    {
      get { return item2; }
    }

    /// <summary>
    /// Gets the 3th element of the tuple.
    /// </summary>
    /// <value>The 3th element of the tuple.</value>
    public T3 Item3
    {
      get { return item3; }
    }

    /// <summary>
    /// Gets the 4th element of the tuple.
    /// </summary>
    /// <value>The 4th element of the tuple.</value>
    public T4 Item4
    {
      get { return item4; }
    }

    /// <summary>
    /// Gets the 5th element of the tuple.
    /// </summary>
    /// <value>The 5th element of the tuple.</value>
    public T5 Item5
    {
      get { return item5; }
    }

    /// <summary>
    /// Gets the 6th element of the tuple.
    /// </summary>
    /// <value>The 6th element of the tuple.</value>
    public T6 Item6
    {
      get { return item6; }
    }

    /// <summary>
    /// Gets the 7th element of the tuple.
    /// </summary>
    /// <value>The 7th element of the tuple.</value>
    public T7 Item7
    {
      get { return item7; }
    }

    /// <summary>
    /// Gets the 8th element of the tuple.
    /// </summary>
    /// <value>The 8th element of the tuple.</value>
    public T8 Item8
    {
      get { return item8; }
    }

    /// <summary>
    /// Gets the 9th element of the tuple.
    /// </summary>
    /// <value>The 9th element of the tuple.</value>
    public T9 Item9
    {
      get { return item9; }
    }


    /// <summary>
    /// Selects a value from this tuple using a function. Allows you to 
    /// assign descriptive names to the tuple elements to increase
    /// readability.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="selector">The selector.</param>
    /// <returns></returns>
    public TResult Select<TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> selector)
    {
      selector.RequireNotNull("selector");

      return selector(Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj"/> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
	  var otherTup = obj as Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9>;
      return otherTup != null && Equals(otherTup);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9> other)
    {
	  if (other == null) return false;
      if (!EqualityComparer<T1>.Default.Equals(item1, other.item1))
        return false;
      if (!EqualityComparer<T2>.Default.Equals(item2, other.item2))
        return false;
      if (!EqualityComparer<T3>.Default.Equals(item3, other.item3))
        return false;
      if (!EqualityComparer<T4>.Default.Equals(item4, other.item4))
        return false;
      if (!EqualityComparer<T5>.Default.Equals(item5, other.item5))
        return false;
      if (!EqualityComparer<T6>.Default.Equals(item6, other.item6))
        return false;
      if (!EqualityComparer<T7>.Default.Equals(item7, other.item7))
        return false;
      if (!EqualityComparer<T8>.Default.Equals(item8, other.item8))
        return false;
      if (!EqualityComparer<T9>.Default.Equals(item9, other.item9))
        return false;

      return true;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
      int result = EqualityComparer<T1>.Default.GetHashCode(item1);
      unchecked {
        result = 397*result^EqualityComparer<T2>.Default.GetHashCode(item2);
        result = 397*result^EqualityComparer<T3>.Default.GetHashCode(item3);
        result = 397*result^EqualityComparer<T4>.Default.GetHashCode(item4);
        result = 397*result^EqualityComparer<T5>.Default.GetHashCode(item5);
        result = 397*result^EqualityComparer<T6>.Default.GetHashCode(item6);
        result = 397*result^EqualityComparer<T7>.Default.GetHashCode(item7);
        result = 397*result^EqualityComparer<T8>.Default.GetHashCode(item8);
        result = 397*result^EqualityComparer<T9>.Default.GetHashCode(item9);
      }
      return result;
    }

    ///<summary>
    /// Returns an enumerator that iterates through the tuple elements.
    ///</summary>
    ///<returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the tuple elements.
    ///</returns>
    public IEnumerator GetEnumerator()
    {
      yield return item1;
      yield return item2;
      yield return item3;
      yield return item4;
      yield return item5;
      yield return item6;
      yield return item7;
      yield return item8;
      yield return item9;
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9> left, Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9> right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9> left, Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9> right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the tuple
    /// </summary>
    /// <returns>a string representation of the tuple</returns>
    public override string ToString()
    {
      return string.Format("<{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}>", item1, item2, item3, item4, item5, item6, item7, item8, item9);
    }

    object ICloneable.Clone()
    {
      return Clone();
    }

    public Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9> Clone()
    {
      return Tup.Create(item1, item2, item3, item4, item5, item6, item7, item8, item9);
    }
  }

  /// <summary>
  /// An immutable 10-tuple class.
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
  /// <remarks>
  /// This class will be deprecated when VS2010 is released. At 
  /// that time, System.Tuple will be the preferred solution.
  /// </remarks>
  [Serializable]
  public sealed class Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IEquatable<Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>, IEnumerable, ICloneable
  {
    private readonly T1 item1;
    private readonly T2 item2;
    private readonly T3 item3;
    private readonly T4 item4;
    private readonly T5 item5;
    private readonly T6 item6;
    private readonly T7 item7;
    private readonly T8 item8;
    private readonly T9 item9;
    private readonly T10 item10;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tup&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10&gt;"/> class.
    /// </summary>
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
    public Tup(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10)
    {
      this.item1 = item1;
      this.item2 = item2;
      this.item3 = item3;
      this.item4 = item4;
      this.item5 = item5;
      this.item6 = item6;
      this.item7 = item7;
      this.item8 = item8;
      this.item9 = item9;
      this.item10 = item10;
    }

    /// <summary>
    /// Gets the 1th element of the tuple.
    /// </summary>
    /// <value>The 1th element of the tuple.</value>
    public T1 Item1
    {
      get { return item1; }
    }

    /// <summary>
    /// Gets the 2th element of the tuple.
    /// </summary>
    /// <value>The 2th element of the tuple.</value>
    public T2 Item2
    {
      get { return item2; }
    }

    /// <summary>
    /// Gets the 3th element of the tuple.
    /// </summary>
    /// <value>The 3th element of the tuple.</value>
    public T3 Item3
    {
      get { return item3; }
    }

    /// <summary>
    /// Gets the 4th element of the tuple.
    /// </summary>
    /// <value>The 4th element of the tuple.</value>
    public T4 Item4
    {
      get { return item4; }
    }

    /// <summary>
    /// Gets the 5th element of the tuple.
    /// </summary>
    /// <value>The 5th element of the tuple.</value>
    public T5 Item5
    {
      get { return item5; }
    }

    /// <summary>
    /// Gets the 6th element of the tuple.
    /// </summary>
    /// <value>The 6th element of the tuple.</value>
    public T6 Item6
    {
      get { return item6; }
    }

    /// <summary>
    /// Gets the 7th element of the tuple.
    /// </summary>
    /// <value>The 7th element of the tuple.</value>
    public T7 Item7
    {
      get { return item7; }
    }

    /// <summary>
    /// Gets the 8th element of the tuple.
    /// </summary>
    /// <value>The 8th element of the tuple.</value>
    public T8 Item8
    {
      get { return item8; }
    }

    /// <summary>
    /// Gets the 9th element of the tuple.
    /// </summary>
    /// <value>The 9th element of the tuple.</value>
    public T9 Item9
    {
      get { return item9; }
    }

    /// <summary>
    /// Gets the 10th element of the tuple.
    /// </summary>
    /// <value>The 10th element of the tuple.</value>
    public T10 Item10
    {
      get { return item10; }
    }


    /// <summary>
    /// Selects a value from this tuple using a function. Allows you to 
    /// assign descriptive names to the tuple elements to increase
    /// readability.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="selector">The selector.</param>
    /// <returns></returns>
    public TResult Select<TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> selector)
    {
      selector.RequireNotNull("selector");

      return selector(Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj"/> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
	  var otherTup = obj as Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>;
      return otherTup != null && Equals(otherTup);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> other)
    {
	  if (other == null) return false;
      if (!EqualityComparer<T1>.Default.Equals(item1, other.item1))
        return false;
      if (!EqualityComparer<T2>.Default.Equals(item2, other.item2))
        return false;
      if (!EqualityComparer<T3>.Default.Equals(item3, other.item3))
        return false;
      if (!EqualityComparer<T4>.Default.Equals(item4, other.item4))
        return false;
      if (!EqualityComparer<T5>.Default.Equals(item5, other.item5))
        return false;
      if (!EqualityComparer<T6>.Default.Equals(item6, other.item6))
        return false;
      if (!EqualityComparer<T7>.Default.Equals(item7, other.item7))
        return false;
      if (!EqualityComparer<T8>.Default.Equals(item8, other.item8))
        return false;
      if (!EqualityComparer<T9>.Default.Equals(item9, other.item9))
        return false;
      if (!EqualityComparer<T10>.Default.Equals(item10, other.item10))
        return false;

      return true;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
      int result = EqualityComparer<T1>.Default.GetHashCode(item1);
      unchecked {
        result = 397*result^EqualityComparer<T2>.Default.GetHashCode(item2);
        result = 397*result^EqualityComparer<T3>.Default.GetHashCode(item3);
        result = 397*result^EqualityComparer<T4>.Default.GetHashCode(item4);
        result = 397*result^EqualityComparer<T5>.Default.GetHashCode(item5);
        result = 397*result^EqualityComparer<T6>.Default.GetHashCode(item6);
        result = 397*result^EqualityComparer<T7>.Default.GetHashCode(item7);
        result = 397*result^EqualityComparer<T8>.Default.GetHashCode(item8);
        result = 397*result^EqualityComparer<T9>.Default.GetHashCode(item9);
        result = 397*result^EqualityComparer<T10>.Default.GetHashCode(item10);
      }
      return result;
    }

    ///<summary>
    /// Returns an enumerator that iterates through the tuple elements.
    ///</summary>
    ///<returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the tuple elements.
    ///</returns>
    public IEnumerator GetEnumerator()
    {
      yield return item1;
      yield return item2;
      yield return item3;
      yield return item4;
      yield return item5;
      yield return item6;
      yield return item7;
      yield return item8;
      yield return item9;
      yield return item10;
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> left, Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> left, Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the tuple
    /// </summary>
    /// <returns>a string representation of the tuple</returns>
    public override string ToString()
    {
      return string.Format("<{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}>", item1, item2, item3, item4, item5, item6, item7, item8, item9, item10);
    }

    object ICloneable.Clone()
    {
      return Clone();
    }

    public Tup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Clone()
    {
      return Tup.Create(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10);
    }
  }

}
