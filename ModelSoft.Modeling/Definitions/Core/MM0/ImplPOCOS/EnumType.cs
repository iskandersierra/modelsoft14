using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using ModelSoft.Modeling.Definitions.Common;
using ModelSoft.Modeling.Definitions.Common.ImplPOCOS;

namespace ModelSoft.Modeling.Definitions.Core.MM0.ImplPOCOS
{
  [ContentProperty("Literals")]
  public class EnumType :
    DataType,
    IEnumType
  {
    [RelationshipType(ERelationshipType.Association)]
    public IPrimitiveType BaseType { get; set; }

    [RelationshipType(ERelationshipType.Content)]
    public IModelElementCollection<IEnumLiteral> Literals
    {
      get { return _Literals ?? (_Literals = new ModelElementCollection<IEnumLiteral>(this, true)); }
    }
    private ModelElementCollection<IEnumLiteral> _Literals;

    [IsComputed]
    [IsHiddenProperty]
    [RelationshipType(ERelationshipType.Association)]
    public IEnumerable<IARequiredNamedElement> NamedChildren
    {
      get { return this.GetNamedChildrenEx(); }
    }
  }
}
