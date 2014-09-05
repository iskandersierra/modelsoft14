using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling.Definitions.Common.ImplPOCOS
{
  [DebuggerDisplay("{ModelElementTypeName}")]
  public abstract class ModelElement :
    IModelElement
  {
    internal ModelElement _ParentElement;
    internal IModelElementCollection _ParentCollection;
    internal Action _RemoveFromParent;

    [IsComputed]
    [RelationshipType(ERelationshipType.Container)]
    public IModel RootModel
    {
      get 
      {
        return this.GetAncestorsEx().Last() as IModel;
      }
    }

    [IsComputed]
    [RelationshipType(ERelationshipType.Container)]
    public IModelElement ParentElement
    {
      get { return _ParentElement; }
    }

    [IsComputed]
    [IsHiddenProperty]
    public abstract string ModelElementTypeName { get; }

    [IsComputed]
    [IsHiddenProperty]
    [RelationshipType(ERelationshipType.Container)]
    public IEnumerable<IModelElement> Ancestors
    {
      get { return this.GetAncestorsEx(); }
    }

    [IsComputed]
    [IsHiddenProperty]
    [RelationshipType(ERelationshipType.Association)]
    public IEnumerable<IModelElement> Children
    {
      get { return this.GetChildrenEx(); }
    }

    [IsComputed]
    [IsHiddenProperty]
    [RelationshipType(ERelationshipType.Association)]
    public IEnumerable<IModelElement> DescendantsAndSelf
    {
      get { return this.GetDescendantsAndSelfEx(); }
    }

    [IsComputed]
    [IsHiddenProperty]
    public IEnumerable<ModelElementPropertyValue> ValueProperties
    {
      get { return this.GetPropertiesEx(ERelationshipType.Value); }
    }

    [IsComputed]
    [IsHiddenProperty]
    public IEnumerable<ModelElementPropertyValue> AssociationProperties
    {
      get { return this.GetPropertiesEx(ERelationshipType.Association); }
    }

    [IsComputed]
    [IsHiddenProperty]
    public IEnumerable<ModelElementPropertyValue> ContentProperties
    {
      get { return this.GetPropertiesEx(ERelationshipType.Content); }
    }

    [IsComputed]
    [IsHiddenProperty]
    public IEnumerable<ModelElementPropertyValue> ContainerProperties
    {
      get { return this.GetPropertiesEx(ERelationshipType.Container); }
    }

    [IsComputed]
    [IsHiddenProperty]
    public IEnumerable<ModelElementPropertyValue> AllProperties
    {
      get { return this.GetPropertiesEx(); }
    }

    public IEnumerable<IModelElement> FindByName(string aReferenceName)
    {
      return this.GetFindByNameEx(aReferenceName);
    }

    public sealed override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public sealed override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
