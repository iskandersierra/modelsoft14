using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework;

namespace ModelSoft.Framework.ObjectModel
{
  public sealed class PropertyDescriptionComputeContext :
    PropertyDescriptionContext
  {
    public PropertyDescriptionComputeContext(object self, IPropertyDescription propertyDescription)
      : base(self, propertyDescription)
    {
      ComputedValue = null;
      ComputedValueIsSet = false;
    }

    public object ComputedValue { get; private set; }

    public bool ComputedValueIsSet { get; private set; }

    public void SetComputedValue(object value)
    {
      this.ComputedValue = value;
      if (value == PropertyDescriptionValues.DoNothing)
        this.ComputedValueIsSet = false;
      else
        this.ComputedValueIsSet = true;
    }
  }
}
