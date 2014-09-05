using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling.Definitions.Core.MM0
{
  public interface IComplexType :
    IDataType,
    IANamespace
  {
    bool IsAbstract { get; set; }

    [RelationshipType(ERelationshipType.Association)]
    IModelElementCollection<IComplexType> BaseTypes { get; }

    [RelationshipType(ERelationshipType.Content)]
    IModelElementCollection<IProperty> Properties { get; }
  }
}
