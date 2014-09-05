using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Collections
{
  public class InverseComparer<T> : IComparer<T>
  {
    private IComparer<T> comparer;
    
    public InverseComparer() : this(null)
    {
    }

    public InverseComparer(IComparer<T> comparer)
    {
      if (comparer == null) comparer = Comparer<T>.Default;
      this.comparer = comparer;
    }

    public int Compare(T x, T y)
    {
      return comparer.Compare(y, x);
    }
  }
}
