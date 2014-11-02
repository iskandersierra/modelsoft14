using System;
using System.Globalization;
using ModelSoft.Framework.DomainObjects.Properties;

namespace ModelSoft.Framework.DomainObjects
{
    public class PrimitiveTypeInformation :
        SimpleTypeInformation,
        IPrimitiveTypeInformation
    {
        private readonly Func<object, string> _serializeFunc;
        private readonly Func<string, object> _deserializeFunc;

        public PrimitiveTypeInformation(
            IModelElementInformationProvider informationProvider, 
            Type clrType, 
            string globalIdentifier, 
            string name, 
            Func<object, string> serializeFunc,
            Func<string, object> deserializeFunc,
            Func<CultureInfo, string> displayNameFunc = null, 
            Func<CultureInfo, string> descriptionFunc = null, 
            Func<CultureInfo, string> categoryFunc = null) : 
                base(informationProvider, clrType, globalIdentifier, name, displayNameFunc, descriptionFunc, categoryFunc)
        {
            if (serializeFunc == null) throw new ArgumentNullException("serializeFunc");
            if (deserializeFunc == null) throw new ArgumentNullException("deserializeFunc");

            _serializeFunc = serializeFunc;
            _deserializeFunc = deserializeFunc;
        }

        public override string SerializeToString(object instance)
        {
            if (!IsInstance(instance))
                throw new InvalidCastException(string.Format(Resources.CannotConvertToType, this));
            return _serializeFunc(instance);
        }

        public override object DeserializeFromString(string serializedInstance)
        {
            return _serializeFunc(serializedInstance);
        }

        public override string ToString()
        {
            return string.Format("primitive {0}", ClrType.Name);
        }
    }
}