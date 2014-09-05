using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework.Collections
{
  public class CancelableSubstituteItemPositionEventArgs<T, TPos> : SubstituteItemPositionEventArgs<T, TPos>
  {
    public CancelableSubstituteItemPositionEventArgs(T item, TPos pos, T oldItem)
      : base(item, pos, oldItem)
    {
    }

    public bool Canceled { get; private set; }

    public void Cancel()
    {
      Canceled = true;
    }
  }
}
