using System;

namespace ModelSoft.Framework.Serialization
{
    public abstract class ClrTypeStringSerializerBase<T> :
        IClrTypeStringSerializer
    {
        public Type ClrType {
            get
            {
                return typeof (T);
            }
        }

        public string SerializeValue(object value)
        {
            return DoSerializeValue((T) value);
        }

        public object DeserializeValue(string serializedValue)
        {
            return DoDeserializeValue(serializedValue);
        }

        protected abstract string DoSerializeValue(T value);
        protected abstract T DoDeserializeValue(string serializedValue);
    }
}