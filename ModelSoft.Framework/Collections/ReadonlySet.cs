using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework.Collections
{
  public class ReadOnlySet<T> : ISet<T>
  {
    private IEnumerable<T> innerEnumerable;
    private ICollection<T> innerCollection;
    private ISet<T> innerSet;

    public ReadOnlySet(IEnumerable<T> innerEnumerable)
    {
      innerEnumerable.RequireNotNull("innerEnumerable");

      this.innerEnumerable = innerEnumerable;
      innerCollection = innerEnumerable as ICollection<T>;
      innerSet = innerEnumerable as ISet<T>;
    }

    public ReadOnlySet(ICollection<T> innerCollection)
      : this((IEnumerable<T>)innerCollection)
    {
    }

    public ReadOnlySet(ISet<T> innerSet)
      : this((IEnumerable<T>)innerSet)
    {
    }

    public IEnumerator<T> GetEnumerator()
    {
        return innerEnumerable.GetEnumerator();
    }

    void ICollection<T>.Add(T item)
    {
      throw new NotSupportedException();
    }

    void ISet<T>.UnionWith(IEnumerable<T> other)
    {
      throw new NotSupportedException();
    }

    void ISet<T>.IntersectWith(IEnumerable<T> other)
    {
      throw new NotSupportedException();
    }

    void ISet<T>.ExceptWith(IEnumerable<T> other)
    {
      throw new NotSupportedException();
    }

    void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
    {
      throw new NotSupportedException();
    }

    public bool IsSubsetOf(IEnumerable<T> other)
    {
      if (innerSet != null)
        return innerSet.IsSubsetOf(other);
      if (other is HashSet<T> || other is SortedSet<T>)
        return ((ISet<T>) other).IsSupersetOf(innerEnumerable);
      if (other is ReadOnlySet<T> && ((ReadOnlySet<T>)other).innerSet != null)
        return ((ReadOnlySet<T>)other).innerSet.IsSupersetOf(innerEnumerable);
      HashSet<T> otherFastSet = null;
      foreach (var t in innerEnumerable)
      {
        if (otherFastSet == null) otherFastSet = new HashSet<T>(other);
        if (!otherFastSet.Contains(t))
          return false;
      }
      return true;
    }

    public bool IsSupersetOf(IEnumerable<T> other)
    {
      if (innerSet != null)
        return innerSet.IsSupersetOf(other);
      if (other is HashSet<T> || other is SortedSet<T>)
        return ((ISet<T>)other).IsSubsetOf(innerEnumerable);
      if (other is ReadOnlySet<T> && ((ReadOnlySet<T>)other).innerSet != null)
        return ((ReadOnlySet<T>)other).innerSet.IsSubsetOf(innerEnumerable);
      HashSet<T> thisFastSet = null;
      foreach (var t in other)
      {
        if (thisFastSet == null) thisFastSet = new HashSet<T>(innerEnumerable);
        if (!thisFastSet.Contains(t))
          return false;
      }
      return true;
    }

    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
      if (innerSet != null)
        return innerSet.IsProperSupersetOf(other);
      if (other is HashSet<T> || other is SortedSet<T>)
        return ((ISet<T>)other).IsProperSubsetOf(innerEnumerable);
      if (other is ReadOnlySet<T> && ((ReadOnlySet<T>)other).innerSet != null)
        return ((ReadOnlySet<T>)other).innerSet.IsProperSubsetOf(innerEnumerable);
      var thisFastSet = new HashSet<T>(innerEnumerable);
      int otherCount = 0;
      foreach (var t in other)
      {
        otherCount++;
        if (!thisFastSet.Contains(t))
          return false;
      }
      return otherCount < thisFastSet.Count;
    }

    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
      if (innerSet != null)
        return innerSet.IsProperSubsetOf(other);
      if (other is HashSet<T> || other is SortedSet<T>)
        return ((ISet<T>)other).IsProperSupersetOf(innerEnumerable);
      if (other is ReadOnlySet<T> && ((ReadOnlySet<T>)other).innerSet != null)
        return ((ReadOnlySet<T>)other).innerSet.IsProperSupersetOf(innerEnumerable);
      var otherFastSet = new HashSet<T>(other);
      int thisCount = 0;
      foreach (var t in this)
      {
        thisCount++;
        if (!otherFastSet.Contains(t))
          return false;
      }
      return thisCount < otherFastSet.Count;
    }

    public bool Overlaps(IEnumerable<T> other)
    {
      if (innerSet != null)
        return innerSet.Overlaps(other);
      if (other is HashSet<T> || other is SortedSet<T>)
        return ((ISet<T>)other).Overlaps(innerEnumerable);
      if (other is ReadOnlySet<T> && ((ReadOnlySet<T>)other).innerSet != null)
        return ((ReadOnlySet<T>)other).innerSet.Overlaps(innerEnumerable);
      HashSet<T> otherFastSet = null;
      foreach (var t in this)
      {
        if (otherFastSet == null) otherFastSet = new HashSet<T>(other);
        if (otherFastSet.Contains(t))
          return true;
      }
      return false;
    }

    public bool SetEquals(IEnumerable<T> other)
    {
      if (innerSet != null)
        return innerSet.SetEquals(other);
      if (other is HashSet<T> || other is SortedSet<T>)
        return ((ISet<T>)other).SetEquals(innerEnumerable);
      if (other is ReadOnlySet<T> && ((ReadOnlySet<T>)other).innerSet != null)
        return ((ReadOnlySet<T>)other).innerSet.SetEquals(innerEnumerable);
      return innerEnumerable.SameSetAs(other);
    }

    bool ISet<T>.Add(T item)
    {
      throw new NotSupportedException();
    }

    void ICollection<T>.Clear()
    {
      throw new NotSupportedException();
    }

    public bool Contains(T item)
    {
      if (innerCollection != null)
        return innerCollection.Contains(item);
      return innerEnumerable.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      if (innerCollection != null)
        innerCollection.CopyTo(array, arrayIndex);
      else
        innerEnumerable.CopyTo(array, arrayIndex);
    }

    bool ICollection<T>.Remove(T item)
    {
      throw new NotSupportedException();
    }

    public int Count
    {
      get
      {
        if (innerCollection != null)
          return innerCollection.Count;
        return innerEnumerable.Count();
      }
    }

    public bool IsReadOnly
    {
      get { return true; }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
