using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling.Definitions.Common
{
  public interface IModel : 
    IModelElement,
    IANamespace,
    IWithNamespace
  {
    Uri BaseUri { get; }

    [RelationshipType(ERelationshipType.Content)]
    IModelElementCollection<IImportedModel> ImportedModels { get; }
  }
}
