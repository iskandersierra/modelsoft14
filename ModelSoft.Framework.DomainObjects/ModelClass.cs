using System;
using System.Globalization;
using ModelSoft.Framework.Annotations;
using ModelSoft.Framework.Collections;

namespace ModelSoft.Framework.DomainObjects
{
    public class ModelClass : IModelClass
    {
        private string _name;

        private readonly Type _clrType;
        private readonly Func<CultureInfo, string> _displayName;
        private readonly Func<CultureInfo, string> _description;
        private readonly Func<CultureInfo, string> _category;
        private IIndexedList<string, IModelProperty> _properties;
        private readonly IModelClass _baseClass;

        internal ModelClass([NotNull] Type clrType, 
            Func<CultureInfo, string> displayName,
            IModelClass baseClass,
            Func<CultureInfo, string> description,
            Func<CultureInfo, string> category)
        {
            if (clrType == null) throw new ArgumentNullException("clrType");
            _clrType = clrType;
            _name = clrType.Name;
            _displayName = displayName ?? (c => _name);
            _description = description ?? (c => "");
            _category = category ?? (c => "");
            _baseClass = baseClass;
            _properties = new IndexedList<string, IModelProperty>(p => p.Name);
        }

        public string Name
        {
            get { return _name; }
        }

        public string DisplayName
        {
            get { return _displayName(null); }
        }

        public string GetDisplayName(CultureInfo culture)
        {
            return _displayName(culture);
        }

        public string Description
        {
            get { return _description(null); }
        }

        public string GetDescription(CultureInfo culture)
        {
            return _description(culture);
        }

        public string Category
        {
            get { return _category(null); }
        }

        public string GetCategory(CultureInfo culture)
        {
            return _category(culture);
        }

        public Type ClrType
        {
            get { return _clrType; }
        }

        public IModelClass BaseClass
        {
            get { return _baseClass; }
        }

        public IIndexedList<string, IModelProperty> Properties
        {
            get { return _properties; }
        }

        public override string ToString()
        {
            return string.Format(@"{0}{1} ""{2}""", Name, BaseClass != null ? " : " + BaseClass.Name : "", DisplayName);
        }
    }
}