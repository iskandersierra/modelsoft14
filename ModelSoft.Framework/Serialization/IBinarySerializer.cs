using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Serialization
{
  public interface IBinarySerializer
  {
    byte[] SerializeToBinary(object value);
    object DeserializeFromString(byte[] serializedValue, Type targetType);

    IEnumerable<Type> SerializableTypes { get; }
  }
}
