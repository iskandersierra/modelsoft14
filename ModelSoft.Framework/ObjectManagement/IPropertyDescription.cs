using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  public interface IPropertyDescription : INamedDescription
  {
    IClassDescription DeclaringClass { get; }

    IDataTypeDescription DataType { get; }

    PropertyKind Kind { get; }

    PropertyMultiplicity Multiplicity { get; }

    bool HasDefaultValue { get; }

    object DefaultValue { get; }

    bool HasComputedValue { get; }

    object ComputeValue(ObjectManager instance);
  }
}
