using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework;

namespace ModelSoft.Framework.ObjectModel
{
  public sealed class PropertyDescriptionCoerceContext :
    PropertyDescriptionContext
  {
    public PropertyDescriptionCoerceContext(object self, IPropertyDescription propertyDescription)
      : base(self, propertyDescription)
    {
      CoercedValue = null;
      CoercedValueIsSet = false;
    }

    public object Value { get; private set; }

    public object CoercedValue { get; private set; }

    public bool CoercedValueIsSet { get; private set; }

    public void SetCoercedValue(object value)
    {
        this.CoercedValue = value;
        if (value == PropertyDescriptionValues.DoNothing)
        this.CoercedValueIsSet = false;
      else
        this.CoercedValueIsSet = true;
    }
  }
}
