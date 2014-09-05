using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  public sealed class PropertyDescriptionChangingValueContext :
    PropertyDescriptionValueChangeContext
  {
    public PropertyDescriptionChangingValueContext(object self, IPropertyDescription propertyDescription, object oldValue, object newValue, bool allowChange = false)
      : base(self, propertyDescription, oldValue, newValue)
    {
      AllowChange = allowChange;
    }

    public bool AllowChange { get; set; }
  }
}
