using System;
using System.Globalization;
using ModelSoft.Framework.DomainObjects.Properties;

namespace ModelSoft.Framework.DomainObjects
{
    public abstract class TypeInformation : 
        NamedInformation,
        ITypeInformation
    {
        private readonly string _globalIdentifier;
        private readonly Type _clrType;

        protected TypeInformation(
            IModelElementInformationProvider informationProvider,
            Type clrType, 
            string globalIdentifier, 
            string name,
            Func<CultureInfo, string> displayNameFunc = null, 
            Func<CultureInfo, string> descriptionFunc = null, 
            Func<CultureInfo, string> categoryFunc = null
            ) 
            : base(informationProvider, name, displayNameFunc, descriptionFunc, categoryFunc)
        {
            if (clrType == null)
                throw new ArgumentNullException("clrType");
            if (globalIdentifier == null)
                throw new ArgumentNullException("globalIdentifier");
            if (!Uri.IsWellFormedUriString(globalIdentifier, UriKind.Absolute))
                throw new ArgumentException(string.Format(Resources.InvalidClassGlobalIdentifierFormat, globalIdentifier), "globalIdentifier");

            _globalIdentifier = globalIdentifier;
            _clrType = clrType;
        }

        protected Type ClrType
        {
            get { return _clrType; }
        }

        public string GlobalIdentifier
        {
            get { return _globalIdentifier; }
        }

        //public Type ClrType { get; private set; }
        public virtual bool IsInstance(object instance)
        {
            return instance != null && ClrType.IsInstanceOfType(instance);
        }

        public void CheckInstance(object instance)
        {
            if (!IsInstance(instance))
                throw new ArgumentException(string.Format(Resources.CannotConvertInstanceToType, instance, this));
        }

        public virtual object GetDefaultValue()
        {
            if (ClrType.IsValueType)
                return Activator.CreateInstance(ClrType);
            else
                return null;
        }

        public void SetAssembly(IAssemblyInformation assembly)
        {
            throw new NotImplementedException();
        }
    }
}