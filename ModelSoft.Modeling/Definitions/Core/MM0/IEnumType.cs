using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling.Definitions.Core.MM0
{
  public interface IEnumType :
    IDataType,
    IANamespace
  {
    [RelationshipType(ERelationshipType.Association)]
    IPrimitiveType BaseType { get; set; }

    [RelationshipType(ERelationshipType.Content)]
    IModelElementCollection<IEnumLiteral> Literals { get; }
  }
}
