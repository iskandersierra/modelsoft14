using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  public sealed class PropertyDescriptionChangeValueContext :
    PropertyDescriptionValueChangeContext
  {
    public PropertyDescriptionChangeValueContext(object self, IPropertyDescription propertyDescription, object oldValue, object newValue)
      : base(self, propertyDescription, oldValue, newValue)
    {
    }
  }
}
