using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ModelSoft.Framework
{
  public class ObjectEqualityComparer<T> : 
    IEqualityComparer<T>
    where T : class
  {

    private static ObjectEqualityComparer<T> _default;
    public static ObjectEqualityComparer<T> Default { get { return _default ?? (_default = new ObjectEqualityComparer<T>()); } }

    private ObjectEqualityComparer()
    {
    }

    public bool Equals(T x, T y)
    {
      return object.ReferenceEquals(x, y);
    }

    public int GetHashCode(T obj)
    {
      return RuntimeHelpers.GetHashCode(obj);
    }
  }
}
