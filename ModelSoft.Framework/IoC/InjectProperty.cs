using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.IoC
{
  public class InjectPropertyInfo : InjectMemberInfo
  {
    public InjectPropertyInfo()
    {
    }

    public InjectPropertyInfo(string propertyName)
      : this(propertyName, null)
    {
    }

    public InjectPropertyInfo(string propertyName, object propertyValue)
    {
      this.PropertyName = propertyName;
      this.PropertyValue = propertyValue;
    }

    public string PropertyName { get; set; }
    public object PropertyValue { get; set; }
  }
}
