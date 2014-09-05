using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  public interface IDataTypeDescription : 
    INamedDescription
  {
    object DefaultValue { get; }

    bool IsConformant(object value);
  }
}
