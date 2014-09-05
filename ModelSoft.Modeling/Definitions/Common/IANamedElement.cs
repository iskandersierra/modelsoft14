using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling.Definitions.Common
{
  public interface IANamedElement :
    IModelElement
  {
    string Name { get; set; }

    [IsComputed]
    string FullName { get; }
  }
}
