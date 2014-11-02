using System;
using System.Globalization;
using ModelSoft.Framework.DomainObjects.Properties;

namespace ModelSoft.Framework.DomainObjects
{
    public abstract class SimpleTypeInformation :
        TypeInformation,
        ISimpleTypeInformation
    {
        protected SimpleTypeInformation(
            IModelElementInformationProvider informationProvider, 
            Type clrType, 
            string globalIdentifier, 
            string name, 
            Func<CultureInfo, string> displayNameFunc = null, 
            Func<CultureInfo, string> descriptionFunc = null, 
            Func<CultureInfo, string> categoryFunc = null) 
            : base(informationProvider, clrType, globalIdentifier, name, displayNameFunc, descriptionFunc, categoryFunc)
        {
        }

        public virtual string SerializeToString(object instance)
        {
            CheckInstance(instance);
            return null;
        }

        public virtual object DeserializeFromString(string serializedInstance)
        {
            throw new FormatException(string.Format("Cannot parse \"{0}\" to type {1}", serializedInstance, this));
        }
    }
}