using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework.Collections
{
  public class CancelableItemsEventArgs<T> : ItemsEventArgs<T>
  {
    public CancelableItemsEventArgs(IEnumerable<T> item)
      : base(item)
    {
    }

    public bool Canceled { get; private set; }

    public void Cancel()
    {
      Canceled = true;
    }
  }
}
