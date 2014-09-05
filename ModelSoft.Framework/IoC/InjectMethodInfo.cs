using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.IoC
{
  public class InjectMethodInfo : InjectMemberInfo
  {
    public InjectMethodInfo()
    {
    }

    public InjectMethodInfo(string methodName, params object[] parameterValues)
    {
      this.MethodName = methodName;
      this.ParameterValues = parameterValues;
    }

    public string MethodName { get; set; }
    public object[] ParameterValues { get; set; }
  }
}
