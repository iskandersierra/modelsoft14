using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  public interface IPrimitiveTypeDescription : 
    IDataTypeDescription
  {
    bool SupportsStringSerialization { get; }

    bool SupportsBinarySerialization { get; }

    bool SupportsIncrementDecrement { get; }

    bool SupportsCompare { get; }

    string SerializeValueToString(object value);

    object DeserializeValueFromString(string serializedValue);

    byte[] SerializeValueToBinary(object value);

    object DeserializeValueFromBinary(byte[] serializedValue);

    object Increment(object value);

    object Decrement(object value);

    bool AreEquals(object value1, object value2);

    int GetHashCode(object value);

    string ToString(object value);

    int Compare(object value1, object value2);
  }
}
