using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  public enum PropertyKind
  {
    Default, // for values and association properties
    Container, // for parent properties
    Content, // for children properties
  }
}
