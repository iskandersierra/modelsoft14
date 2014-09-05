using System;
using System.Globalization;

namespace ModelSoft.Framework.DomainObjects
{
    public interface IModelProperty
    {
        string Name { get; }
        Type PropertyClrType { get; }
        bool HasDefaultValue { get; }
        object DefaultValue { get; }
        bool HasComputedValue { get; }
        object ComputeValue(ComputePropertyValueContext context);
        bool IsReadOnly { get; }
    
        string DisplayName { get; }
        string GetDisplayName(CultureInfo culture = null);
        string Description { get; }
        string GetDescription(CultureInfo culture = null);
        string Category { get; }
        string GetCategory(CultureInfo culture = null);
    }

    public interface IModelProperty<out T> : IModelProperty
    {
        new T DefaultValue { get; }
    }
}