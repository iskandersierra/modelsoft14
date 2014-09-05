using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework.Collections
{
  public class ItemsEventArgs<T> : EventArgs
  {
    public ItemsEventArgs(IEnumerable<T> item)
    {
      Item = item;
    }

    public IEnumerable<T> Item { get; private set; }
  }
}
