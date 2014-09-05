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
  [RuntimeNameProperty("Name")]
  public abstract class MetaModelDefinition :
    MM0ModelElement,
    IMetaModelDefinition
  {
    public string Name { get; set; }

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
