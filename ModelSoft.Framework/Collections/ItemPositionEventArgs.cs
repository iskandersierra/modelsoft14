using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework.Collections
{
  public class ItemPositionEventArgs<T, TPos> : EventArgs
  {
    public ItemPositionEventArgs(T item, TPos pos)
    {
      this.Item = item;
      this.Pos = pos;
    }

    public T Item { get; private set; }

    public TPos Pos { get; private set; }
  }
}
