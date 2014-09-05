using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  public abstract class PropertyDescriptionValueChangeContext :
    PropertyDescriptionContext
  {
    public PropertyDescriptionValueChangeContext(object self, IPropertyDescription propertyDescription, object oldValue, object newValue)
      : base(self, propertyDescription)
    {
      OldValue = oldValue;
      NewValue = newValue;
    }

    public object OldValue { get; private set; }

    public object NewValue { get; private set; }
  }
}
