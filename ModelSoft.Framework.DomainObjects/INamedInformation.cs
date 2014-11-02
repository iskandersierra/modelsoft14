using System.Globalization;

namespace ModelSoft.Framework.DomainObjects
{
    public interface INamedInformation : ICommonInformation
    {
        string Name { get; }
        string DisplayName { get; }
        string Description { get; }
        string Category { get; }

        string GetDisplayName(CultureInfo culture);
        string GetDescription(CultureInfo culture);
        string GetCategory(CultureInfo culture);
    }
}