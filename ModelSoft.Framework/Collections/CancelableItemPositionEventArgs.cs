using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework.Collections
{
  public class CancelableItemPositionEventArgs<T, TPos> : ItemPositionEventArgs<T, TPos>
  {
    public CancelableItemPositionEventArgs(T item, TPos pos)
      : base(item, pos)
    {
    }

    public bool Canceled { get; private set; }

    public void Cancel()
    {
      Canceled = true;
    }
  }
}
