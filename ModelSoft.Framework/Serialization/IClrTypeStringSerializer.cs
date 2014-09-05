using System;

namespace ModelSoft.Framework.Serialization
{
    public interface IClrTypeStringSerializer
    {
        Type ClrType { get; }

        string SerializeValue(object value);
        object DeserializeValue(string serializedValue);
    }
}