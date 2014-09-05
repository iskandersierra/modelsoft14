using System;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework.Serialization
{
    public static class SerializationExtensions
    {
        public static IClrTypeStringSerializer AsClrSerializer<T>([NotNull] this IStringSerializer serializer)
        {
            return serializer.AsClrSerializer(typeof (T));
        }
        public static IClrTypeStringSerializer AsClrSerializer([NotNull] this IStringSerializer serializer, [NotNull] Type type)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (type == null) throw new ArgumentNullException("type");

            return new InnerClrSerializer(type, serializer);
        }

        private class InnerClrSerializer : IClrTypeStringSerializer
        {
            private readonly IStringSerializer _serializer;

            public InnerClrSerializer(Type type, IStringSerializer serializer)
            {
                _serializer = serializer;
                ClrType = type;
            }

            public Type ClrType { get; private set; }

            public string SerializeValue(object value)
            {
                return _serializer.SerializeToString(value);
            }

            public object DeserializeValue(string serializedValue)
            {
                return _serializer.DeserializeFromString(serializedValue, ClrType);
            }
        }

    }
}
