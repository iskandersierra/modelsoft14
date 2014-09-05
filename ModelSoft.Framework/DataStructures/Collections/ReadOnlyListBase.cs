using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.DataStructures.Collections
{
  /// <summary>
  /// ReadOnlyListBase is an abstract class that can be used as a base class for a read-only collection that needs 
  /// to implement the generic IList&lt;T&gt; and non-generic IList collections. The derived class needs
  /// to override the Count property and the get part of the indexer. The implementation
  /// of all the other methods in IList&lt;T&gt; and IList are handled by ListBase.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  [Serializable]
  public abstract class ReadOnlyListBase<T> : 
    ReadOnlyCollectionBase<T>, 
    IList<T>, 
    IList
  {
    /// <summary>
    /// Throws an NotSupportedException stating that this collection cannot be modified.
    /// </summary>
    private void MethodModifiesCollection()
    {
      throw new NotSupportedException(@"Cannot modify read-only list");
    }

    /// <summary>
    /// The property must be overridden by the derived class to return the number of 
    /// items in the list.
    /// </summary>
    /// <value>The number of items in the list.</value>
    public abstract override int Count { get; }

    /// <summary>
    /// The get part of the indexer must be overridden by the derived class to get 
    /// values of the list at a particular index.
    /// </summary>
    /// <param name="index">The index in the list to get or set an item at. The
    /// first item in the list has index 0, and the last has index Count-1.</param>
    /// <returns>The item at the given index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is
    /// less than zero or greater than or equal to Count.</exception>
    public virtual T this[int index]
    {
      get
      {
        throw new NotImplementedException(@"Must implement list indexer get");
      }

      set
      {
        MethodModifiesCollection();
      }
    }

    /// <summary>
    /// Enumerates all of the items in the list, in order. The item at index 0
    /// is enumerated first, then the item at index 1, and so on.
    /// </summary>
    /// <returns>An IEnumerator&lt;T&gt; that enumerates all the
    /// items in the list.</returns>
    public override IEnumerator<T> GetEnumerator()
    {
      int count = Count;
      for (int i = 0; i < count; ++i)
      {
        yield return this[i];
      }
    }

    /// <summary>
    /// Determines if the list contains any item that compares equal to <paramref name="item"/>.
    /// The implementation simply checks whether IndexOf(item) returns a non-negative value.
    /// </summary>
    /// <remarks>Equality in the list is determined by the default sense of
    /// equality for T. If T implements IComparable&lt;T&gt;, the
    /// Equals method of that interface is used to determine equality. Otherwise, 
    /// Object.Equals is used to determine equality.</remarks>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the list contains an item that compares equal to <paramref name="item"/>.</returns>
    public override bool Contains(T item)
    {
      return (IndexOf(item) >= 0);
    }

    /// <summary>
    /// Copies all the items in the list, in order, to <paramref name="array"/>,
    /// starting at index 0.
    /// </summary>
    /// <param name="array">The array to copy to. This array must have a size
    /// that is greater than or equal to Count.</param>
    public virtual void CopyTo(T[] array)
    {
      this.CopyTo(array, 0);
    }

    /// <summary>
    /// Copies a range of elements from the list to <paramref name="array"/>,
    /// starting at <paramref name="arrayIndex"/>.
    /// </summary>
    /// <param name="index">The starting index in the source list of the range to copy.</param>
    /// <param name="array">The array to copy to. This array must have a size
    /// that is greater than or equal to Count + arrayIndex.</param>
    /// <param name="arrayIndex">The starting index in <paramref name="array"/>
    /// to copy to.</param>
    /// <param name="count">The number of items to copy.</param>
    public virtual void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
      this.AsRange(index, count).CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Finds the index of the first item in the list that is equal to <paramref name="item"/>. 
    /// </summary>
    /// <remarks>The default implementation of equality for type T is used in the search. This is the
    /// equality defined by IComparable&lt;T&gt; or object.Equals.</remarks>
    /// <param name="item">The item to search fror.</param>
    /// <returns>The index of the first item in the list that that is equal to <paramref name="item"/>.  If no item is equal
    /// to <paramref name="item"/>, -1 is returned.</returns>
    public virtual int IndexOf(T item)
    {
      return EnumerableExtensions.IndexOf(this, item, EqualityComparer<T>.Default);
    }

    /// <summary>
    /// Inserts a new item at the given index. This implementation throws a NotSupportedException
    /// indicating that the list is read-only.
    /// </summary>
    /// <param name="index">The index in the list to insert the item at. After the
    /// insertion, the inserted item is located at this index. The
    /// first item in the list has index 0.</param>
    /// <param name="item">The item to insert at the given index.</param>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    void IList<T>.Insert(int index, T item)
    {
      MethodModifiesCollection();
    }

    /// <summary>
    /// Removes the item at the given index.  This implementation throws a NotSupportedException
    /// indicating that the list is read-only.
    /// </summary>
    /// <param name="index">The index in the list to remove the item at. The
    /// first item in the list has index 0.</param>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    void IList<T>.RemoveAt(int index)
    {
      MethodModifiesCollection();
    }

    /// <summary>
    /// Adds an item to the end of the list. This implementation throws a NotSupportedException
    /// indicating that the list is read-only.
    /// </summary>
    /// <param name="value">The item to add to the list.</param>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    int IList.Add(object value)
    {
      MethodModifiesCollection();
      return -1;
    }

    /// <summary>
    /// Removes all the items from the list, resulting in an empty list. This implementation throws a NotSupportedException
    /// indicating that the list is read-only.
    /// </summary>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    void IList.Clear()
    {
      MethodModifiesCollection();
    }

    /// <summary>
    /// Determines if the list contains any item that compares equal to <paramref name="value"/>.
    /// </summary>
    /// <remarks>Equality in the list is determined by the default sense of
    /// equality for T. If T implements IComparable&lt;T&gt;, the
    /// Equals method of that interface is used to determine equality. Otherwise, 
    /// Object.Equals is used to determine equality.</remarks>
    /// <param name="value">The item to search for.</param>
    bool IList.Contains(object value)
    {
      if (value is T || value == null)
        return Contains((T)value);
      else
        return false;
    }

    /// <summary>
    /// Find the first occurrence of an item equal to <paramref name="value"/>
    /// in the list, and returns the index of that item.
    /// </summary>
    /// <remarks>Equality in the list is determined by the default sense of
    /// equality for T. If T implements IComparable&lt;T&gt;, the
    /// Equals method of that interface is used to determine equality. Otherwise, 
    /// Object.Equals is used to determine equality.</remarks>
    /// <param name="value">The item to search for.</param>
    /// <returns>The index of <paramref name="value"/>, or -1 if no item in the 
    /// list compares equal to <paramref name="value"/>.</returns>
    int IList.IndexOf(object value)
    {
      if (value is T || value == null)
        return IndexOf((T)value);
      else
        return -1;
    }

    /// <summary>
    /// Insert a new item at the given index. This implementation throws a NotSupportedException
    /// indicating that the list is read-only.
    /// </summary>
    /// <param name="index">The index in the list to insert the item at. After the
    /// insertion, the inserted item is located at this index. The
    /// first item in the list has index 0.</param>
    /// <param name="value">The item to insert at the given index.</param>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    void IList.Insert(int index, object value)
    {
      MethodModifiesCollection();
    }

    /// <summary>
    /// Returns whether the list is a fixed size. This implementation always returns true.
    /// </summary>
    /// <value>Alway true, indicating that the list is fixed size.</value>
    bool IList.IsFixedSize
    {
      get { return true; }
    }

    /// <summary>
    /// Returns whether the list is read only. This implementation always returns true.
    /// </summary>
    /// <value>Alway true, indicating that the list is read-only.</value>
    bool IList.IsReadOnly
    {
      get { return true; }
    }

    /// <summary>
    /// Searches the list for the first item that compares equal to <paramref name="value"/>.
    /// If one is found, it is removed. Otherwise, the list is unchanged.  This implementation throws a NotSupportedException
    /// indicating that the list is read-only.
    /// </summary>
    /// <remarks>Equality in the list is determined by the default sense of
    /// equality for T. If T implements IComparable&lt;T&gt;, the
    /// Equals method of that interface is used to determine equality. Otherwise, 
    /// Object.Equals is used to determine equality.</remarks>
    /// <param name="value">The item to remove from the list.</param>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    void IList.Remove(object value)
    {
      MethodModifiesCollection();
    }

    /// <summary>
    /// Removes the item at the given index. This implementation throws a NotSupportedException
    /// indicating that the list is read-only.
    /// </summary>
    /// <param name="index">The index in the list to remove the item at. The
    /// first item in the list has index 0.</param>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    void IList.RemoveAt(int index)
    {
      MethodModifiesCollection();
    }

    /// <summary>
    /// Gets or sets the value at a particular index in the list.
    /// </summary>
    /// <param name="index">The index in the list to get or set an item at. The
    /// first item in the list has index 0, and the last has index Count-1.</param>
    /// <value>The item at the given index.</value>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is
    /// less than zero or greater than or equal to Count.</exception>
    /// <exception cref="ArgumentException"><paramref name="value"/> cannot be converted to T.</exception>
    /// <exception cref="NotSupportedException">Always thrown from the setter, indicating that the list
    /// is read-only.</exception>
    object IList.this[int index]
    {
      get
      {
        return this[index];
      }

      set
      {
        MethodModifiesCollection();
      }
    }
  }
}