using System;
using System.Collections.Generic;
using System.Linq;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework.Serialization
{
    public class StringSerializerToClrTypeStringSerializerAdapterFactory :
        ClrTypeStringSerializerFactoryBase
    {
        private IStringSerializer _serializer;
        private Dictionary<Type, IClrTypeStringSerializer> _clrSerializers;

        public StringSerializerToClrTypeStringSerializerAdapterFactory(
            [NotNull] IStringSerializer serializer)
        {
            Initialize(serializer);
        }

        public StringSerializerToClrTypeStringSerializerAdapterFactory(
            [NotNull] IStringSerializer serializer, 
            [NotNull] IClrTypeStringSerializerFactory fallbackFactory) : base(fallbackFactory)
        {
            Initialize(serializer);
        }

        public override IClrTypeStringSerializer GetSerializer(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            IClrTypeStringSerializer serializer;
            if (!_clrSerializers.TryGetValue(type, out serializer))
            {
                serializer = CreateSerializer(type);
                if (serializer != null)
                {
                    _clrSerializers.Add(type, serializer);
                }
            }

            return serializer ?? base.GetSerializer(type);
        }

        private IClrTypeStringSerializer CreateSerializer(Type type)
        {
            if (_serializer.SerializableTypes.Contains(type))
            {
                return new InnerSerializer(type, _serializer);
            }
            return null;
        }

        private class InnerSerializer : IClrTypeStringSerializer
        {
            private readonly IStringSerializer _serializer;

            public InnerSerializer(Type type, IStringSerializer serializer)
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

        private void Initialize(IStringSerializer serializer)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            _serializer = serializer;
            _clrSerializers = new Dictionary<Type, IClrTypeStringSerializer>();
        }
    }
}