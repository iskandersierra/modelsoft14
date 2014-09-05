using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling.Definitions.Core.MM0.ImplPOCOS
{
  [DebuggerDisplay("{ModelElementTypeName} \"{Name}\"")]
  public abstract class ComplexTypeMember :
    MM0ModelElement,
    IComplexTypeMember
  {
    public ComplexTypeMember()
    {
      Multiplicity = Multiplicity.Single;
    }

    public string Name { get; set; }

    public bool IsAbstract { get; set; }

    public Multiplicity Multiplicity { get; set; }

    [RelationshipType(ERelationshipType.Container)]
    public IComplexType DeclaringType { get { return ParentElement as IComplexType; } }

    [RelationshipType(ERelationshipType.Association)]
    public IDataType MemberType { get; set; }


    public string FullName
    {
      get { throw new NotImplementedException(); }
    }
  }
}
