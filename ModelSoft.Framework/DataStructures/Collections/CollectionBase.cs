﻿//******************************
// Written by Peter Golde
// Copyright (c) 2004-2007, Wintellect
//
// Use and restribution of this code is subject to the license agreement 
// contained in the file "License.txt" accompanying this file.
//******************************

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ModelSoft.Framework.DataStructures.Collections
{
  /// <summary>
  /// CollectionBase is a base class that can be used to more easily implement the
  /// generic ICollection&lt;T&gt; and non-generic ICollection interfaces.
  /// </summary>
  /// <remarks>
  /// <para>To use CollectionBase as a base class, the derived class must override
  /// the Count, GetEnumerator, Add, Clear, and Remove methods. </para>
  /// <para>ICollection&lt;T&gt;.Contains need not be implemented by the
  /// derived class, but it should be strongly considered, because the CollectionBase implementation
  /// may not be very efficient.</para>
  /// </remarks>
  /// <typeparam name="T">The item type of the collection.</typeparam>
  [Serializable]
  [DebuggerDisplay("{DebuggerDisplayString()}")]
  public abstract class CollectionBase<T> : ICollection<T>, ICollection
  {

    /// <summary>
    /// Shows the string representation of the collection. The string representation contains
    /// a list of the items in the collection. Contained collections (except string) are expanded
    /// recursively.
    /// </summary>
    /// <returns>The string representation of the collection.</returns>
    public override string ToString()
    {
      return this.ToStringList();
    }

    #region ICollection<T> Members

    /// <summary>
    /// Must be overridden to allow adding items to this collection.
    /// </summary>
    /// <remarks><p>This method is not abstract, although derived classes should always
    /// override it. It is not abstract because some derived classes may wish to reimplement
    /// Add with a different return type (typically bool). In C#, this can be accomplished
    /// with code like the following:</p>
    /// <code>
    ///     public class MyCollection&lt;T&gt;: CollectionBase&lt;T&gt;, ICollection&lt;T&gt;
    ///     {
    ///         public new bool Add(T item) {
    ///             /* Add the item */
    ///         }
    ///  
    ///         void ICollection&lt;T&gt;.Add(T item) {
    ///             Add(item);
    ///         }
    ///     }
    /// </code>
    /// </remarks>
    /// <param name="item">Item to be added to the collection.</param>
    /// <exception cref="NotImplementedException">Always throws this exception to indicated
    /// that the method must be overridden or re-implemented in the derived class.</exception>
    public virtual void Add(T item)
    {
      throw new NotImplementedException();
    }


    /// <summary>
    /// Must be overridden to allow clearing this collection.
    /// </summary>
    public abstract void Clear();

    /// <summary>
    /// Must be overridden to allow removing items from this collection.
    /// </summary>
    /// <returns>True if <paramref name="item"/> existed in the collection and
    /// was removed. False if <paramref name="item"/> did not exist in the collection.</returns>
    public abstract bool Remove(T item);

    /// <summary>
    /// Determines if the collection contains a particular item. This default implementation
    /// iterates all of the items in the collection via GetEnumerator, testing each item
    /// against <paramref name="item"/> using IComparable&lt;T&gt;.Equals or
    /// Object.Equals.
    /// </summary>
    /// <remarks>You should strongly consider overriding this method to provide
    /// a more efficient implementation, or if the default equality comparison
    /// is inappropriate.</remarks>
    /// <param name="item">The item to check for in the collection.</param>
    /// <returns>True if the collection contains <paramref name="item"/>, false otherwise.</returns>
    public virtual bool Contains(T item)
    {
      IEqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
      foreach (T i in this)
      {
        if (equalityComparer.Equals(i, item))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Copies all the items in the collection into an array. Implemented by
    /// using the enumerator returned from GetEnumerator to get all the items
    /// and copy them to the provided array.
    /// </summary>
    /// <param name="array">Array to copy to.</param>
    /// <param name="arrayIndex">Starting index in <paramref name="array"/> to copy to.</param>
    public virtual void CopyTo(T[] array, int arrayIndex)
    {
      int count = this.Count;

      if (count == 0)
        return;

      array.RequireNotNull("array");
      count.RequireMinValue(1, "count"); // Count is abstract so check it 
      arrayIndex.RequireInRange(0, array.Length - count, "arrayIndex");

      int index = arrayIndex, i = 0;
      foreach (T item in this)
      {
        if (i >= count)
          break;

        array[index] = item;
        ++index;
        ++i;
      }
    }

    /// <summary>
    /// Creates an array of the correct size, and copies all the items in the 
    /// collection into the array, by calling CopyTo.
    /// </summary>
    /// <returns>An array containing all the elements in the collection, in order.</returns>
    public virtual T[] ToArray()
    {
      int count = this.Count;

      T[] array = new T[count];
      CopyTo(array, 0);
      return array;
    }

    /// <summary>
    /// Must be overridden to provide the number of items in the collection.
    /// </summary>
    /// <value>The number of items in the collection.</value>
    public abstract int Count { get; }

    /// <summary>
    /// Indicates whether the collection is read-only. Always returns false.
    /// </summary>
    /// <value>Always returns false.</value>
    bool ICollection<T>.IsReadOnly
    {
      get { return false; }
    }

    /// <summary>
    /// Provides a read-only view of this collection. The returned ICollection&lt;T&gt; provides
    /// a view of the collection that prevents modifications to the collection. Use the method to provide
    /// access to the collection without allowing changes. Since the returned object is just a view,
    /// changes to the collection will be reflected in the view.
    /// </summary>
    /// <returns>An ICollection&lt;T&gt; that provides read-only access to the collection.</returns>
    public virtual ICollection<T> AsReadOnly()
    {
      return this.AsReadOnly();
    }

    #endregion

    #region IEnumerable<T> Members

    /// <summary>
    /// Must be overridden to enumerate all the members of the collection.
    /// </summary>
    /// <returns>A generic IEnumerator&lt;T&gt; that can be used
    /// to enumerate all the items in the collection.</returns>
    public abstract IEnumerator<T> GetEnumerator();

    #endregion

    #region ICollection Members

    /// <summary>
    /// Copies all the items in the collection into an array. Implemented by
    /// using the enumerator returned from GetEnumerator to get all the items
    /// and copy them to the provided array.
    /// </summary>
    /// <param name="array">Array to copy to.</param>
    /// <param name="index">Starting index in <paramref name="array"/> to copy to.</param>
    void ICollection.CopyTo(Array array, int index)
    {
      int count = this.Count;

      if (count == 0)
        return;

      array.RequireNotNull("array");
      count.RequireMinValue(1, "count"); // Count is abstract so check it 
      index.RequireInRange(0, array.Length - count, "index");

      int i = 0;
      //TODO: Look into this
      foreach (object o in (ICollection)this)
      {
        if (i >= count)
          break;

        array.SetValue(o, index);
        ++index;
        ++i;
      }
    }

    /// <summary>
    /// Indicates whether the collection is synchronized.
    /// </summary>
    /// <value>Always returns false, indicating that the collection is not synchronized.</value>
    bool ICollection.IsSynchronized
    {
      get { return false; }
    }

    /// <summary>
    /// Indicates the synchronization object for this collection.
    /// </summary>
    /// <value>Always returns this.</value>
    object ICollection.SyncRoot
    {
      get { return this; }
    }

    #endregion

    #region IEnumerable Members

    /// <summary>
    /// Provides an IEnumerator that can be used to iterate all the members of the
    /// collection. This implementation uses the IEnumerator&lt;T&gt; that was overridden
    /// by the derived classes to enumerate the members of the collection.
    /// </summary>
    /// <returns>An IEnumerator that can be used to iterate the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      foreach (T item in this)
      {
        yield return item;
      }
    }

    #endregion

    /// <summary>
    /// Display the contents of the collection in the debugger. This is intentionally private, it is called
    /// only from the debugger due to the presence of the DebuggerDisplay attribute. It is similar
    /// format to ToString(), but is limited to 250-300 characters or so, so as not to overload the debugger.
    /// </summary>
    /// <returns>The string representation of the items in the collection, similar in format to ToString().</returns>
    internal string DebuggerDisplayString()
    {
      const int MAXLENGTH = 250;

      System.Text.StringBuilder builder = new System.Text.StringBuilder();

      builder.Append('{');

      // Call ToString on each item and put it in.
      bool firstItem = true;
      foreach (T item in this)
      {
        if (builder.Length >= MAXLENGTH)
        {
          builder.Append(",...");
          break;
        }

        if (!firstItem)
          builder.Append(',');

        if (item == null)
          builder.Append("null");
        else
          builder.Append(item.ToString());

        firstItem = false;
      }

      builder.Append('}');
      return builder.ToString();
    }

  }
}