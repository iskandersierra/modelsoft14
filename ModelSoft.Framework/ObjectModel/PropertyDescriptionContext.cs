using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework;

namespace ModelSoft.Framework.ObjectModel
{
  public abstract class PropertyDescriptionContext
  {
    public PropertyDescriptionContext(object self, IPropertyDescription propertyDescription)
    {
      self.RequireNotNull("self");
      propertyDescription.RequireNotNull("propertyDescription");

      Self = self;
      PropertyDescription = propertyDescription;
    }

    public object Self { get; private set; }

    public IPropertyDescription PropertyDescription { get; private set; }
  }
}
