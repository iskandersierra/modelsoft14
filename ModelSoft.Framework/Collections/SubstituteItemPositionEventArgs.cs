using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework.Collections
{
  public class SubstituteItemPositionEventArgs<T, TPos> : 
    ItemPositionEventArgs<T, TPos>
  {
    public SubstituteItemPositionEventArgs(T item, TPos pos, T oldItem)
      : base(item, pos)
    {
      this.OldItem = oldItem;
    }

    public T OldItem { get; private set; }
  }
}
