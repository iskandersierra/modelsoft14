using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling.Definitions.Common
{
  public interface IANamespace :
    IARequiredNamedElement
  {
    [RelationshipType(ERelationshipType.Association)]
    IEnumerable<IARequiredNamedElement> NamedChildren { get; }
  }
}
