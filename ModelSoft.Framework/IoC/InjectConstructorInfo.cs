using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.IoC
{
  public class InjectConstructorInfo : InjectMemberInfo
  {
    public InjectConstructorInfo()
    {
    }

    public InjectConstructorInfo(params object[] parameterValues)
    {
      this.ParameterValues = parameterValues;
    }

    public object[] ParameterValues { get; set; }
  }
}
