using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  public interface IClassDescription : 
    IFreezable
  {
    IClassDescriptionRegistry Registry { get; }

    IClassDescription BaseClass { get; }

    string Name { get; }

    string ResourceName { get; }

    string FriendlyName { get; }

    IEnumerable<IPropertyDescription> Properties { get; }

    IObjectData CreateInstance(Type type, IObjectDataHost host);

    void RegisterProperty(IPropertyDescription propertyDescription);

    bool TryRegisterProperty(IPropertyDescription propertyDescription);
  }
}
