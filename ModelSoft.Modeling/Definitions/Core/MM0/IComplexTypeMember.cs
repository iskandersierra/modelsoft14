using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling.Definitions.Core.MM0
{
  public interface IComplexTypeMember :
    IMM0ModelElement,
    IANamedElement
  {
    [RelationshipType(ERelationshipType.Container)]
    IComplexType DeclaringType { get; }

    [RelationshipType(ERelationshipType.Association)]
    IDataType MemberType { get; set; }

    Multiplicity Multiplicity { get; set; }

    bool IsAbstract { get; set; }
  }
}
