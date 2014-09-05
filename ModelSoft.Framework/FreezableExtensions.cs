using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework
{
  public static class FreezableExtensions
  {
    public static TFreezable AsFrozen<TFreezable>(this TFreezable freezable)
      where TFreezable : class, IFreezable
    {
      freezable.RequireNotNull("freezable");

      freezable.Freeze();

      return freezable;
    }
  }
}
