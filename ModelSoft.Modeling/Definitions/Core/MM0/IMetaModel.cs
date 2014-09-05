using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling.Definitions.Core.MM0
{
  public interface IMetaModel : 
    IMM0ModelElement,
    IARequiredNamedElement,
    IModel
  {
    [RelationshipType(ERelationshipType.Content)]
    IModelElementCollection<IMetaModelDefinition> Definitions { get; }
  }
}
