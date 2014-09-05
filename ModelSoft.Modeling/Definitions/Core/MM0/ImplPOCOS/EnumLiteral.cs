using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling.Definitions.Core.MM0.ImplPOCOS
{
  [DebuggerDisplay("{ModelElementTypeName} \"{Name}\"")]
  //[RuntimeNameProperty("Name")]
  public class EnumLiteral :
    MM0ModelElement,
    IEnumLiteral
  {
    [RelationshipType(ERelationshipType.Container)]
    public IEnumType DeclaringEnumType { get { return ParentElement as IEnumType; } }

    public string Name { get; set; }

    public int Value { get; set; }

    [IsComputed]
    [RelationshipType(ERelationshipType.Container)]
    public IANamespace ParentNamespace
    {
      get { return this.GetParentNamespaceEx(); }
    }

    [IsComputed]
    public string FullName
    {
      get { return this.GetFullNameEx(); }
    }
  }
}
