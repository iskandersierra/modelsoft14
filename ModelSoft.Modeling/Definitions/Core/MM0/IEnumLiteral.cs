using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling.Definitions.Core.MM0
{
  public interface IEnumLiteral :
    IMM0ModelElement,
    IARequiredNamedElement
  {
    [RelationshipType(ERelationshipType.Container)]
    IEnumType DeclaringEnumType { get; }

    int Value { get; }
  }
}
