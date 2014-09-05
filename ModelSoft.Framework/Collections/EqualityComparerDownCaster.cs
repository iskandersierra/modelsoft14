using System;
using System.Collections;
using System.Collections.Generic;

namespace ModelSoft.Framework.Collections
{
  public class EqualityComparerDownCaster<TBase, TChildren> : IEqualityComparer<TChildren>
    where TChildren : TBase
  {
    public IEqualityComparer<TBase> Inner { get; private set; }

    public EqualityComparerDownCaster(IEqualityComparer<TBase> inner)
    {
      inner.RequireNotNull("inner");

      Inner = inner;
    }

    #region Implementation of IEqualityComparer<TChildren>

    public bool Equals(TChildren x, TChildren y)
    {
      return Inner.Equals(x, y);
    }

    public int GetHashCode(TChildren obj)
    {
      return Inner.GetHashCode(obj);
    }

    #endregion
  }

  public class EqualityComparerDownCaster<TChildren> : IEqualityComparer<TChildren>
  {
    public IEqualityComparer Inner { get; private set; }

    public EqualityComparerDownCaster(IEqualityComparer inner)
    {
      inner.RequireNotNull("inner");

      Inner = inner;
    }

    public bool Equals(TChildren x, TChildren y)
    {
      return Inner.Equals(x, y);
    }

    public int GetHashCode(TChildren obj)
    {
      return Inner.GetHashCode(obj);
    }
  }
}
