using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Collections
{
  public class ComparisonComparer<T> :
    IComparer<T>
  {
    Comparison<T> comparison;

    public ComparisonComparer(Comparison<T> comparison)
    {
      comparison.RequireNotNull("comparison");

      this.comparison = comparison;
    }

    public int Compare(T x, T y)
    {
      return comparison(x, y);
    }
  }
}
