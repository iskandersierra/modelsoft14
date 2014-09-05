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
  [ContentProperty("Properties")]
  public class ComplexType :
    DataType,
    IComplexType
  {
    public bool IsAbstract { get; set; }

    [RelationshipType(ERelationshipType.Association)]
    public IModelElementCollection<IComplexType> BaseTypes
    {
      get { return _BaseTypes ?? (_BaseTypes = new ModelElementCollection<IComplexType>(this, false)); }
    }
    private IModelElementCollection<IComplexType> _BaseTypes;

    [RelationshipType(ERelationshipType.Content)]
    public IModelElementCollection<IProperty> Properties
    {
      get { return _Properties ?? (_Properties = new ModelElementCollection<IProperty>(this, true)); }
    }
    private IModelElementCollection<IProperty> _Properties;

    [IsComputed]
    [IsHiddenProperty]
    [RelationshipType(ERelationshipType.Association)]
    public IEnumerable<IARequiredNamedElement> NamedChildren
    {
      get { return this.GetNamedChildrenEx(); }
    }
  }
}
