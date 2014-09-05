using System;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework.Serialization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ClrTypeStringSerializerAttribute : Attribute
    {
        public ClrTypeStringSerializerAttribute([NotNull] Type serializerType)
        {
            if (serializerType == null) throw new ArgumentNullException("serializerType");
            if (!typeof(IClrTypeStringSerializer).IsAssignableFrom(serializerType))
                throw new ArgumentException("serializerType must be an implementation of IClrTypeStringSerializer");
            if (!serializerType.IsClass || serializerType.IsAbstract)
                throw new ArgumentException("serializerType must be a concrete class");
            if (serializerType.GetConstructor(Type.EmptyTypes) == null)
                throw new ArgumentException("serializerType must have a parameterless public constructor");
            SerializerType = serializerType;
        }

        public Type SerializerType { get; private set; }
    }
}