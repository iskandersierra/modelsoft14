using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  internal interface IDescriptionWithParent
  {
    object Parent { get; set; }
    object ParentCollection { get; set; }
  }
}
