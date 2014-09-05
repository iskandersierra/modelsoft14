using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  public interface IEnumLiteralDescription :
    INamedDescription
  {
    IEnumTypeDescription DeclaringEnum { get; }

    object BaseValue { get; }
  }
}
