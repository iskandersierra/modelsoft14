using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;
using ModelSoft.Modeling.Definitions.Core.Expressions;

namespace ModelSoft.Modeling.Definitions.Core.MM0
{
  public interface IProperty :
    IComplexTypeMember,
    IARequiredNamedElement
  {
    RelationshipType RelationshipType { get; set; }
    
    bool IsRequired { get; set; }

    bool IsReadOnly { get; set; }

    [RelationshipType(ERelationshipType.Association)]
    IProperty Opposite { get; set; }

    ITypedExpression ComputedValue { get; set; }
  }
}
