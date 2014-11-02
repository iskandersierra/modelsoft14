using System;
using System.CodeDom.Compiler;
using System.Globalization;
using ModelSoft.Framework.DomainObjects.Properties;

namespace ModelSoft.Framework.DomainObjects
{
    public abstract class NamedInformation : 
        CommonInformation, 
        INamedInformation
    {
        private readonly Func<CultureInfo, string> _displayNameFunc;
        private readonly Func<CultureInfo, string> _descriptionFunc;
        private readonly Func<CultureInfo, string> _categoryFunc;

        protected NamedInformation(
            IModelElementInformationProvider informationProvider,
            string name,
            Func<CultureInfo, string> displayNameFunc = null,
            Func<CultureInfo, string> descriptionFunc = null,
            Func<CultureInfo, string> categoryFunc = null)
            : base(informationProvider)
        {
            name.ValidateCSharpIdentifier();
            if (displayNameFunc == null)
                displayNameFunc = DefaultDisplayName;
            if (descriptionFunc == null)
                descriptionFunc = DefaultDescription;
            if (categoryFunc == null)
                categoryFunc = DefaultCategory;

            Name = name;
            _displayNameFunc = displayNameFunc;
            _descriptionFunc = descriptionFunc;
            _categoryFunc = categoryFunc;
        }

        public string Name { get; private set; }
        public string DisplayName
        {
            get
            {
                return _displayNameFunc(null);
            }
        }
        public string Description
        {
            get
            {
                return _descriptionFunc(null);
            }
        }
        public string Category
        {
            get
            {
                return _categoryFunc(null);
            }
        }

        public string GetDisplayName(CultureInfo culture)
        {
            return _displayNameFunc(culture);
        }

        public string GetDescription(CultureInfo culture)
        {
            return _descriptionFunc(culture);
        }

        public string GetCategory(CultureInfo culture)
        {
            return _categoryFunc(culture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>E.g.: PersonalAgenda (Personal agenda)</returns>
        public override string ToString()
        {
            var displayName = DisplayName;
            if (displayName == Name) return Name;
            return string.Format("{0} ({1})", displayName, Name);
        }

        public string Formatted
        {
            get
            {
                return this.Format();
            }
        }

        /// <summary>
        /// Prints the following attributes where required:
        /// [DisplayName(@"[display name in current culture]")]
        /// [Category(@"[category in current culture]")]
        /// [Description(@"[description in current culture]")]
        /// </summary>
        /// <param name="writer">An indented text writer</param>
        public override void Format(IndentedTextWriter writer)
        {
            // Add DisplayName, Description and Category as a <summary> and a [Category(...)]
            var displayName = DisplayName;
            if (displayName.IsNotWS() && displayName != Name)
                writer.WriteLine(@"[DisplayName(@""{0}"")]", displayName.AsVerbatim());

            var category = Category;
            if (category.IsNotWS())
                writer.WriteLine(@"[Category(@""{0}"")]", category.AsVerbatim());

            var description = Description;
            if (description.IsNotWS())
                writer.WriteLine(@"[Description(@""{0}"")]", description.AsVerbatim());
        }

        private string DefaultDisplayName(CultureInfo culture)
        {
            return Name;
        }
        private static string DefaultDescription(CultureInfo culture)
        {
            return string.Empty;
        }
        private static string DefaultCategory(CultureInfo culture)
        {
            return Resources.ResourceManager.GetString("DefaultCategory", culture);
        }
    }
}