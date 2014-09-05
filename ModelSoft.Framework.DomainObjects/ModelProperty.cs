using System;
using System.Globalization;
using ModelSoft.Framework.Annotations;
using ModelSoft.Framework.DomainObjects.Properties;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.Framework.DomainObjects
{
    public abstract class ModelProperty : IModelProperty
    {
        private readonly string _name;
        private readonly Type _propertyClrType;
        private readonly Func<CultureInfo, string> _displayName;
        private readonly Func<CultureInfo, string> _description;
        private readonly Func<CultureInfo, string> _category;
        private readonly object _defaultValue;
        private readonly bool _hasDefaultValue;
        private readonly bool _isReadOnly;
        private readonly Func<ComputePropertyValueContext, string> _computeValue;
        private readonly bool _hasComputedValue;

        protected ModelProperty(
            [NotNull]   string name,
            [NotNull]   Type propertyClrType,
            [CanBeNull] Func<CultureInfo, string> displayName,
            [CanBeNull] Func<CultureInfo, string> description,
            [CanBeNull] Func<CultureInfo, string> category,
            [CanBeNull] object defaultValue,
            [CanBeNull] Func<ComputePropertyValueContext, string> computeValue,
                        bool isReadOnly)
        {
            name.ValidateCSharpIdentifier();
            if (propertyClrType == null) throw new ArgumentNullException("propertyClrType");

            _name = name;
            _propertyClrType = propertyClrType;
            _displayName = displayName ?? (c => _name);
            _description = description ?? (c => String.Empty);
            _category = category ?? (c => String.Empty);
            _defaultValue = defaultValue;
            _hasDefaultValue = _defaultValue != null;
            _isReadOnly = isReadOnly;
            _computeValue = computeValue;
            _hasComputedValue = computeValue != null;
        }

        protected ModelProperty(
            [NotNull]   string name,
            [NotNull]   Type propertyClrType,
            [NotNull]   string displayName,
            [CanBeNull] string description,
            [CanBeNull] string category,
            [CanBeNull] object defaultValue,
            [CanBeNull] Func<ComputePropertyValueContext, string> computeValue,
                        bool isReadOnly)
            : this(name, propertyClrType, 
                c => displayName, c => description, c => category, 
                defaultValue, computeValue, isReadOnly)
        {
            if (displayName.IsWS()) throw new ArgumentNullException("displayName");
        }

        public string Name
        {
            get { return _name; }
        }

        public Type PropertyClrType
        {
            get { return _propertyClrType; }
        }

        public bool HasDefaultValue
        {
            get { return _hasDefaultValue; }
        }

        object IModelProperty.DefaultValue
        {
            get { return GetUntypedDefaultValue(); }
        }

        protected object GetUntypedDefaultValue()
        {
            if (!HasDefaultValue)
                throw new InvalidOperationException(string.Format(Resources.PropertyHasNoDefaultValue, DisplayName));
            return _defaultValue;
        }

        public bool HasComputedValue
        {
            get { return _hasComputedValue; }
        }

        public object ComputeValue(ComputePropertyValueContext context)
        {
            if (!HasComputedValue)
                throw new InvalidOperationException(string.Format(Resources.PropertyHasNoComputedValue, DisplayName));
            return _computeValue(context);
        }

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        public string DisplayName
        {
            get { return _displayName(null); }
        }

        public string GetDisplayName(CultureInfo culture = null)
        {
            return _displayName(culture);
        }

        public string Description
        {
            get { return _description(null); }
        }

        public string GetDescription(CultureInfo culture = null)
        {
            return _description(culture);
        }

        public string Category
        {
            get { return _category(null); }
        }

        public string GetCategory(CultureInfo culture = null)
        {
            return _category(culture);
        }

        public override string ToString()
        {
            return string.Format(@"{0}: {1}{2} ""{3}""", Name, PropertyClrType.GetPlainName(), HasDefaultValue ? " = " + ((IModelProperty)this).DefaultValue : "", DisplayName);
        }
    }

    public class ModelProperty<T> : ModelProperty, IModelProperty<T>
    {
        public ModelProperty(
            [NotNull] string name, 
            [NotNull] Type propertyClrType, 
            [CanBeNull] Func<CultureInfo, string> displayName, 
            [CanBeNull] Func<CultureInfo, string> description, 
            [CanBeNull] Func<CultureInfo, string> category, 
            [CanBeNull] object defaultValue, 
            [CanBeNull] Func<ComputePropertyValueContext, string> computeValue, 
            bool isReadOnly) 
            : base(name, propertyClrType, displayName, description, category, defaultValue, computeValue, isReadOnly)
        {
        }

        public ModelProperty(
            [NotNull] string name, 
            [NotNull] Type propertyClrType, 
            [NotNull] string displayName, 
            [NotNull] string description, 
            [NotNull] string category, 
            [CanBeNull] object defaultValue, 
            [CanBeNull] Func<ComputePropertyValueContext, string> computeValue, 
            bool isReadOnly) 
            : base(name, propertyClrType, displayName, description, category, defaultValue, computeValue, isReadOnly)
        {
        }

        public T DefaultValue {
            get
            {
                return (T) GetUntypedDefaultValue();
            }
        }
    }
}