using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework;
using ModelSoft.Framework.Collections;

namespace ModelSoft.Modeling.Definitions.Common.ImplPOCOS
{
  public class ModelElementCollection<T> :
    CustomList<T>,
    IModelElementCollection<T>
    where T : class, IModelElement
  {
    ModelElement _Owner;
    bool _IsContainer;

    public ModelElementCollection(ModelElement _Owner, bool _IsContainer)
      : base()
    {
      _Owner.RequireNotNull("_Owner");
      this._Owner = _Owner;
      this._IsContainer = _IsContainer;
    }

    public IModelElement Owner { get { return _Owner; } }

    protected override void Check(T item)
    {
      base.Check(item);

      if (ModelLoaderContext.Current != null && !_IsContainer)
      {
        // If it is an association collection then allow insertion of null values that will be later properly set
        if (item != null && !(item is ModelElement)) 
          throw new InvalidCastException("item must be of type ModelElement");
      }
      else
      {
        if (item == null) 
          throw new ArgumentNullException("item");

        if (!(item is ModelElement)) 
          throw new InvalidCastException("item must be of type ModelElement");
      }
    }

    protected override bool OnBeforeEnter(T item, int pos)
    {
      if (_IsContainer)
      {
        var elem = (ModelElement)(IModelElement)item;
        // It item has as its container this collection, then it cannot be added again.
        if (elem._ParentCollection == this) return false;

        // if item is an ancestor of this collection's owner, then it cannot be added
        if (_Owner.GetAncestorsEx().Contains(elem)) return false;

        if (elem._ParentElement != null || elem._ParentCollection != null)
        {
          if (elem._RemoveFromParent != null)
          {
            elem._RemoveFromParent();
            elem._RemoveFromParent = null;
          }
          elem._ParentCollection = null;
          elem._ParentElement = null;
        }
      }
      return base.OnBeforeEnter(item, pos);
    }
    protected override void OnAfterEnter(T item, int pos)
    {
      if (_IsContainer)
      {
        var elem = (ModelElement)(IModelElement)item;
        elem._ParentElement = _Owner;
        elem._ParentCollection = this;
        elem._RemoveFromParent = () => this.Remove(item);
      }
      base.OnAfterEnter(item, pos);
    }
    protected override void OnAfterLeave(T item, int pos)
    {
      if (_IsContainer)
      {
        var elem = (ModelElement)(IModelElement)item;
        elem._ParentElement = null;
        elem._ParentCollection = null;
        elem._RemoveFromParent = null;
      }
      base.OnAfterLeave(item, pos);
    }
  }
}
