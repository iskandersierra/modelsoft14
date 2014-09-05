using System;
using ModelSoft.Framework.Annotations;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.Framework.Serialization
{
    public class AttributedClrTypeStringSerializerFactory :
        ClrTypeStringSerializerFactoryBase
    {
        public AttributedClrTypeStringSerializerFactory()
        {
        }

        public AttributedClrTypeStringSerializerFactory(
            [NotNull] IClrTypeStringSerializerFactory fallbackFactory) 
            : base(fallbackFactory)
        {
        }

        public override IClrTypeStringSerializer GetSerializer([NotNull] Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            var serializerAttr = type.GetAttribute<ClrTypeStringSerializerAttribute>();
            if (serializerAttr != null)
            {
                return (IClrTypeStringSerializer) Activator.CreateInstance(serializerAttr.SerializerType);
            }

            return base.GetSerializer(type);
        }
    }
}