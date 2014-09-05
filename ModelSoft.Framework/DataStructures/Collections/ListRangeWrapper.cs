﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.DataStructures.Collections
{
  /// <summary>
  /// The class that is used to implement IList&lt;T&gt; to view a sub-range
  /// of a list. The object stores a wrapped list, and a start/count indicating
  /// a sub-range of the list. Insertion/deletions through the sub-range view
  /// cause the count to change also; insertions and deletions directly on
  /// the wrapped list do not.
  /// </summary>
  [Serializable]
  internal class ListRangeWrapper<T> : ListBase<T>, ICollection<T>
  {
    private readonly IList<T> wrappedList;
    private readonly int start;
    private int count;

    /// <summary>
    /// Create a sub-range view object on the indicate part 
    /// of the list.
    /// </summary>
    /// <param name="wrappedList">List to wrap.</param>
    /// <param name="start">The start index of the view in the wrapped list.</param>
    /// <param name="count">The number of items in the view.</param>
    public ListRangeWrapper(IList<T> wrappedList, int start, int count)
    {
      this.wrappedList = wrappedList;
      this.start = start;
      this.count = count;
    }

    public override int Count
    {
      get
      {
        return Math.Min(count, wrappedList.Count - start);
      }
    }

    public override void Clear()
    {
      if (wrappedList.Count - start < count)
        count = wrappedList.Count - start;

      while (count > 0)
      {
        wrappedList.RemoveAt(start + count - 1);
        --count;
      }
    }

    public override void Insert(int index, T item)
    {
      if (index < 0 || index > count)
        throw new ArgumentOutOfRangeException("index");

      wrappedList.Insert(start + index, item);
      ++count;
    }

    public override void RemoveAt(int index)
    {
      if (index < 0 || index >= count)
        throw new ArgumentOutOfRangeException("index");

      wrappedList.RemoveAt(start + index);
      --count;
    }

    public override bool Remove(T item)
    {
      if (wrappedList.IsReadOnly)
        throw new NotSupportedException(@"Cannot modify range");
      else
        return base.Remove(item);
    }

    public override T this[int index]
    {
      get
      {
        if (index < 0 || index >= count)
          throw new ArgumentOutOfRangeException("index");

        return wrappedList[start + index];
      }
      set
      {
        if (index < 0 || index >= count)
          throw new ArgumentOutOfRangeException("index");

        wrappedList[start + index] = value;
      }
    }

    bool ICollection<T>.IsReadOnly
    {
      get
      {
        return wrappedList.IsReadOnly;
      }
    }
  }
}
