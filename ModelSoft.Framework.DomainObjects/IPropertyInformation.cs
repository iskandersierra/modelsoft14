using System.Reflection;

namespace ModelSoft.Framework.DomainObjects
{
    public interface IPropertyInformation : INamedInformation
    {
        IClassInformation Class { get; }
        //PropertyInfo ClrProperty { get; }
        ITypeInformation PropertyType { get; }
        PropertyRole Role { get; }
        string OppositePropertyName { get; }
        bool IsReadOnly { get; }
        bool IsComputed { get; }
        PropertyMultiplicity Multiplicity { get; }

        void SetClass(IClassInformation aClass);

    }
}