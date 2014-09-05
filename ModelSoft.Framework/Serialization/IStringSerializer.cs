using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Serialization
{
  public interface IStringSerializer
  {
    string SerializeToString(object value);
    object DeserializeFromString(string serializedValue, Type targetType);

    IEnumerable<Type> SerializableTypes { get; }
  }
}
