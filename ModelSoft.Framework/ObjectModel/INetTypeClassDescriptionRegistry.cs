using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  public interface INetTypeClassDescriptionRegistry :
    IClassDescriptionRegistry,
    IFreezable
  {
    IClassDescription ClassHierarchyBase { get; }

    IClassDescription GetClassDescription(Type type);

    bool TryGetClassDescription(Type type, out IClassDescription registeredClass);

    IClassDescription RegisterClass(Type type, ClassDescriptionSetup setup = null);

    bool TryRegisterClass(Type type, out IClassDescription registeredClass, ClassDescriptionSetup setup = null);
  }
}
