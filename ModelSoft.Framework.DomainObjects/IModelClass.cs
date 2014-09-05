using System;
using System.Globalization;
using ModelSoft.Framework.Collections;

namespace ModelSoft.Framework.DomainObjects
{
    public interface IModelClass
    {
        string Name { get; }
        Type ClrType { get; }

        IModelClass BaseClass { get; }

        IIndexedList<string, IModelProperty> Properties { get; }

        string DisplayName { get; }
        string GetDisplayName(CultureInfo culture);
        string Description { get; }
        string GetDescription(CultureInfo culture = null);
        string Category { get; }
        string GetCategory(CultureInfo culture = null);
    }
}