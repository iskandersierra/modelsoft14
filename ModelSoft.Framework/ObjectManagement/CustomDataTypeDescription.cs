using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  public abstract class CustomDataTypeDescription : 
    CustomNamedDescription,
    IDataTypeDescription
  {
    public CustomDataTypeDescription()
    {
    }

    public CustomDataTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public abstract object DefaultValue { get; }

    public abstract bool IsConformant(object value);
  }
}
