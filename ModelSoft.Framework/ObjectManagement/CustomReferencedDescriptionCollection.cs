using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Collections;

namespace ModelSoft.Framework.ObjectManagement
{
  class CustomReferencedDescriptionCollection<TItem, TItemImpl, TOwner> :
    CustomList<TItem>,
    IDescriptionCollection<TItem>
    where TItem : class, IMetaDescription
    where TItemImpl : class, TItem
    where TOwner : class
  {
    protected readonly TOwner Owner;

    public CustomReferencedDescriptionCollection(TOwner _Owner, string _CollectionName)
      : base(CustomListEvents.All, false)
    {
      _Owner.RequireNotNull("_Owner");
      _CollectionName.RequireNotWhitespace("_CollectionName");

      this.Owner = _Owner;
      this.CollectionName = _CollectionName;
    }

    public string CollectionName { get; private set; }

    protected override bool OnBeforeClear(IEnumerable<TItem> items)
    {
      CheckModifyFrozen();
      if (items.OfType<IFreezable>().Any(e => e.IsFrozen))
        throw new InvalidOperationException("Cannot remove an already frozen item");
      return base.OnBeforeClear(items);
    }

    protected override bool OnBeforeEnter(TItem item, int pos)
    {
      CheckModifyFrozen();
      if (item is IFreezable && ((IFreezable)item).IsFrozen)
        throw new InvalidOperationException("Cannot add an already frozen item");
      return base.OnBeforeEnter(item, pos);
    }

    protected override bool OnBeforeLeave(TItem item, int pos)
    {
      CheckModifyFrozen();
      if (item is IFreezable && ((IFreezable)item).IsFrozen)
        throw new InvalidOperationException("Cannot remove an already frozen item");
      return base.OnBeforeLeave(item, pos);
    }

    protected override void OnAfterLeave(TItem item, int pos)
    {
      base.OnAfterLeave(item, pos);
    }

    protected override void Check(TItem item)
    {
      base.Check(item);
      if (item == null)
        throw new ArgumentNullException("item");
      if (!(item is TItemImpl))
        throw new ArgumentException("item must be a " + typeof(TItemImpl));
    }
    #region [ IFreezable ]
    bool isFrozen;
    public bool IsFrozen
    {
      get { return isFrozen; }
      set
      {
        if (isFrozen == value) return;
        if (isFrozen && !value)
          throw new InvalidFreezingOperationException("A frozen object cannot be defrosted");
        Freeze();
      }
    }

    public void Freeze()
    {
      if (!isFrozen)
      {
        OnFreeze();
        this.isFrozen = true;
      }
    }

    protected virtual void OnFreeze()
    {
      foreach (var item in this)
        item.Freeze();
    }

    protected void CheckModifyFrozen()
    {
      if (isFrozen)
        throw new InvalidFreezingOperationException("Cannot modify a frozen object");
    }
    #endregion
  }
}
