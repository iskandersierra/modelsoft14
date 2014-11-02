using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using ModelSoft.Framework.DomainObjects.Properties;

namespace ModelSoft.Framework.DomainObjects
{
    public sealed class ClassInformation :
        TypeInformation,
        IClassInformation
    {
        private readonly Dictionary<string, IPropertyInformation> _properties;

        internal ClassInformation(
            IModelElementInformationProvider informationProvider,
            Type clrInterfaceType,
            Type clrClassType,
            string globalIdentifier,
            string name,
            IPropertyInformation[] properties,
            IClassInformation[] baseClasses = null,
            Func<CultureInfo, string> displayNameFunc = null,
            Func<CultureInfo, string> descriptionFunc = null,
            Func<CultureInfo, string> categoryFunc = null)
            : base(informationProvider, clrInterfaceType, globalIdentifier, name, displayNameFunc, descriptionFunc, categoryFunc)
        {
            if (clrClassType == null)
                throw new ArgumentNullException("clrClassType");
            if (!clrClassType.IsClass)
                throw new ArgumentException(string.Format(Resources.TypeIsNotAClass, clrClassType), "clrClassType");
            if (!clrInterfaceType.IsInterface)
                throw new ArgumentException(string.Format("Type {0} is not an interface", clrInterfaceType), "clrInterfaceType");

            ClrClassType = clrClassType;
            IsAbstract = clrClassType.IsAbstract;
            if (baseClasses != null)
            {
                if (baseClasses.Any(e => e == null))
                    throw new ArgumentNullException("baseClasses");

                var duplicateBaseClasses = baseClasses.GetDuplicates().ToArray();
                if (duplicateBaseClasses.Any())
                    throw new ArgumentException(string.Format(Resources.DuplicateBaseClassesAreNotAllowed, this, baseClasses.GetDuplicates().ToStringList()), "baseClasses");

                //var invalidBaseClasses = baseClasses.Where(c => !c.ClrType.IsAssignableFrom(clrInterfaceType)).ToArray();
                //if (invalidBaseClasses.Any())
                //    throw new ArgumentException(string.Format(Resources.InvalidBaseClasses, this, invalidBaseClasses.GetDuplicates().ToStringList()), "baseClasses");

                BaseClasses = new ReadOnlyCollection<IClassInformation>(baseClasses.ToList());
            }
            else
            {
                BaseClasses = new ReadOnlyCollection<IClassInformation>(new IClassInformation[0]);
            }

            if (properties != null)
            {
                if (properties.Any(e => e == null))
                    throw new ArgumentNullException("properties");

                if (properties.Any(e => e.Class != null))
                    throw new ArgumentException(string.Format("Properties cannot belog to different classes on ClrClassType {0}: {1}", this, properties.Where(p => p.Class != null).ToStringList()), "properties");

                var duplicateProperties = properties.GetDuplicates(e => e.Name).ToArray();
                if (duplicateProperties.Any())
                    throw new ArgumentException(string.Format(Resources.DuplicatePropertyNamesAreNotAllowed, this, duplicateProperties.ToStringList()));

                properties.ForEach(p => p.SetClass(this));
                _properties = properties.ToDictionary(e => e.Name);
                Properties = new ReadOnlyCollection<IPropertyInformation>(properties.ToList());
            }
            else
            {
                _properties = new Dictionary<string, IPropertyInformation>();
                Properties = new ReadOnlyCollection<IPropertyInformation>(new IPropertyInformation[0]);
            }
        }

        public IReadOnlyCollection<IClassInformation> BaseClasses { get; private set; }
        private Type ClrClassType { get; set; }
        public bool IsAbstract { get; private set; }

        public IReadOnlyCollection<IPropertyInformation> Properties { get; private set; }

        public bool TryGetProperty(string name, out IPropertyInformation propertyInfo)
        {
            return _properties.TryGetValue(name, out propertyInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>E.g.: abstract class PersonalAgenda // Personal agenda</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (IsAbstract) sb.Append("abstract ");
            sb.Append("class ")
                .Append(Name);
            if (DisplayName != Name)
                sb.Append(" // ")
                    .Append(DisplayName);
            return sb.ToString();
        }

        public override void Format(IndentedTextWriter writer)
        {
            base.Format(writer); // DisplayName, Description & Category

            writer.WriteLine(@"[GlobalIdentifier(@""{0}"")]", GlobalIdentifier.AsVerbatim());

            if (IsAbstract) writer.Write("abstract ");
            writer.Write("ClrClassType ");
            writer.Write(Name);
            if (BaseClasses.Any())
            {
                writer.WriteLine(" : ");
                writer.Indent++;
                writer.Write(BaseClasses.ToStringList(", ", c => c.Name));
                writer.Indent--;
            }
            else
            {
                writer.WriteLine();
            }

            writer.WriteLine("{");
            if (Properties.Any())
            {
                writer.Indent++;
                foreach (var property in Properties)
                {
                    writer.WriteLine();
                    property.Format(writer);
                }
                writer.WriteLine();
                writer.Indent--;
            }
            writer.WriteLine("}");
        }
    }
}