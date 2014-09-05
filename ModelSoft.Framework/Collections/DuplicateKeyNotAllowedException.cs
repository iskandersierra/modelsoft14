using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework.Collections
{
  [Serializable]
  public class DuplicateKeyNotAllowedException : 
    Exception
  {
    public DuplicateKeyNotAllowedException()
    {
    }
    public DuplicateKeyNotAllowedException(string message)
      : base(message)
    {
    }
    public DuplicateKeyNotAllowedException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
