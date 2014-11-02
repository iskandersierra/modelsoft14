using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace ModelSoft.Framework.DomainObjects
{
    public sealed class PropertyInformation : 
        NamedInformation, 
        IPropertyInformation
    {
        private ITypeInformation _propertyType;
        private IClassInformation _class;
        private readonly PropertyInfo _clrProperty;
        private readonly PropertyRole _role;
        private readonly string _oppositePropertyName;
        private readonly bool _isReadOnly;
        private readonly bool _isComputed;
        private readonly PropertyMultiplicity _multiplicity;

        internal PropertyInformation(
            IModelElementInformationProvider informationProvider,
            PropertyInfo clrProperty, 
            string name, 
            PropertyMultiplicity multiplicity, 
            bool isReadOnly, 
            bool isComputed, 
            PropertyRole role,
            string oppositePropertyName = null, 
            Func<CultureInfo, string> displayNameFunc = null, 
            Func<CultureInfo, string> descriptionFunc = null, 
            Func<CultureInfo, string> categoryFunc = null) 
            : base(informationProvider, name, displayNameFunc, descriptionFunc, categoryFunc)
        {
            _multiplicity = multiplicity;
            _isReadOnly = isReadOnly;
            _isComputed = isComputed;
            _oppositePropertyName = oppositePropertyName;
            _role = role;
            _clrProperty = clrProperty;
        }

        public IClassInformation Class
        {
            get { return _class; }
        }

        public ITypeInformation PropertyType
        {
            get
            {
                if (_propertyType == null)
                {
                    _propertyType = InformationProvider.GetTypeInformation(_clrProperty.PropertyType);
                }
                return _propertyType;
            }
        }

        public PropertyRole Role
        {
            get { return _role; }
        }

        public string OppositePropertyName
        {
            get { return _oppositePropertyName; }
        }

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        public bool IsComputed
        {
            get { return _isComputed; }
        }

        public PropertyMultiplicity Multiplicity
        {
            get { return _multiplicity; }
        }

        void IPropertyInformation.SetClass(IClassInformation aClass)
        {
            if (aClass == null) 
                throw new ArgumentNullException("aClass");
            if (_class != null)
                throw new InvalidOperationException(string.Format("Cannot change {0} class {1}", this, _class));
            _class = aClass;
        }

        /// <summary>
        /// E.g.: property Class.Property: Type // Display name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("property ");
            
            if (Class != null) 
                sb.Append(Class.Name)
                    .Append(".");

            sb.Append(Name);
            if (_propertyType != null)
                sb.Append(" : ").Append(_propertyType.Name);

            if (DisplayName != Name)
                sb.Append(" // ")
                    .Append(DisplayName);

            return sb.ToString();
        }

        /// <summary>
        /// property [Name] : [Type|ClrClassType]
        /// </summary>
        /// <param name="writer"></param>
        public override void Format(IndentedTextWriter writer)
        {
            base.Format(writer); ///TODO: do it
            writer.Write("property {0}", Name);
        }
    }
}