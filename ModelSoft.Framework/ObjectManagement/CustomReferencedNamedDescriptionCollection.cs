using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  class CustomReferencedNamedDescriptionCollection<TItem, TItemImpl, TOwner> :
    CustomReferencedDescriptionCollection<TItem, TItemImpl, TOwner>,
    INamedDescriptionCollection<TItem>
    where TItem : class, INamedDescription
    where TItemImpl : class, TItem
    where TOwner : class
  {

    Dictionary<string, TItem> dictionary;

    public CustomReferencedNamedDescriptionCollection(TOwner _Owner, string _CollectionName)
      : base(_Owner, _CollectionName)
    {
    }

    public bool ContainsName(string name)
    {
        if (!IsFrozen)
          throw new InvalidFreezingOperationException(FormattedResources.ExMsg_ObjectMustBeFrozen("ContainsName"));
        name.RequireNotNull("name");
        return dictionary.ContainsKey(name);
    }

    public TItem this[string name]
    {
      get
      {
        if (!IsFrozen)
          throw new InvalidFreezingOperationException(FormattedResources.ExMsg_ObjectMustBeFrozen("this[string]"));
        name.RequireNotNull("name");
        return dictionary[name];
      }
    }

    protected override void OnFreeze()
    {
      this.CheckDuplicatedIdentifiersInCollection(CollectionName);
      this.CheckDuplicatedNamesInCollection(CollectionName);

      dictionary = this.ToDictionary(e => e.Name);

      base.OnFreeze();
    }
  }
}
