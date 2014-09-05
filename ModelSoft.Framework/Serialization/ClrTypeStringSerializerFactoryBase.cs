using System;
using ModelSoft.Framework.Annotations;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.Serialization
{
    public abstract class ClrTypeStringSerializerFactoryBase :
        IClrTypeStringSerializerFactory
    {
        private readonly IClrTypeStringSerializerFactory _fallbackFactory;

        protected ClrTypeStringSerializerFactoryBase()
        {
        }

        protected ClrTypeStringSerializerFactoryBase([NotNull] IClrTypeStringSerializerFactory fallbackFactory)
        {
            if (fallbackFactory == null) throw new ArgumentNullException("fallbackFactory");
            _fallbackFactory = fallbackFactory;
        }

        public virtual IClrTypeStringSerializer GetSerializer([NotNull] Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            if (_fallbackFactory != null)
                return _fallbackFactory.GetSerializer(type);

            throw new ArgumentException(string.Format(Resources.SerializerForTypeNotFound, type.FullName));
        }

        public IClrTypeStringSerializer GetSerializer<TValue>()
        {
            return GetSerializer(typeof(TValue));
        }
    }
}