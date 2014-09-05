using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework
{
  [Serializable]
  public class InvalidFreezingOperationException : 
    InvalidOperationException
  {
    public InvalidFreezingOperationException()
      : base(Resources.ExMsg_InvalidFreezingOperationException)
    {
    }
    public InvalidFreezingOperationException(string message)
      : base(message)
    {

    }
    protected InvalidFreezingOperationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {

    }
    public InvalidFreezingOperationException(string message, Exception innerException)
      : base(message, innerException)
    {

    }
  }
}
