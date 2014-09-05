using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling.Definitions.Common
{
  public interface IWithNamespace :
    IModelElement
  {
    string Namespace { get; set; }
  }
}
