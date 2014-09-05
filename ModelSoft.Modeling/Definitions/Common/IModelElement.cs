using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling.Definitions.Common
{
  public interface IModelElement
  {
    [IsComputed]
    [IsHiddenProperty]
    string ModelElementTypeName { get; }

    [IsComputed]
    [RelationshipType(ERelationshipType.Container)]
    IModel RootModel { get; }

    [IsComputed]
    [RelationshipType(ERelationshipType.Container)]
    IModelElement ParentElement { get; }

    [IsComputed]
    [IsHiddenProperty]
    IEnumerable<IModelElement> Ancestors { get; }

    [IsComputed]
    [IsHiddenProperty]
    IEnumerable<IModelElement> Children { get; }

    [IsComputed]
    [IsHiddenProperty]
    IEnumerable<IModelElement> DescendantsAndSelf { get; }

    [IsComputed]
    [IsHiddenProperty]
    IEnumerable<ModelElementPropertyValue> ValueProperties { get; }
    [IsComputed]
    [IsHiddenProperty]
    IEnumerable<ModelElementPropertyValue> AssociationProperties { get; }
    [IsComputed]
    [IsHiddenProperty]
    IEnumerable<ModelElementPropertyValue> ContentProperties { get; }
    [IsComputed]
    [IsHiddenProperty]
    IEnumerable<ModelElementPropertyValue> ContainerProperties { get; }
    [IsComputed]
    [IsHiddenProperty]
    IEnumerable<ModelElementPropertyValue> AllProperties { get; }

    IEnumerable<IModelElement> FindByName(string aReferenceName);
  }
}
