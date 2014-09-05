using System;

namespace ModelSoft.Framework.Serialization
{
    public interface IClrTypeStringSerializerFactory
    {
        IClrTypeStringSerializer GetSerializer(Type type);
        IClrTypeStringSerializer GetSerializer<TValue>();
    }
}
