using System;
using System.Collections.Generic;

namespace ModelSoft.Framework.DomainObjects
{
    public interface IClassInformation : ITypeInformation
    {
        //Type ClrClassType { get; }
        bool IsAbstract { get; }
        IReadOnlyCollection<IClassInformation> BaseClasses { get; }
        IReadOnlyCollection<IPropertyInformation> Properties { get; }
        bool TryGetProperty(string propertyName, out IPropertyInformation propertyInfo);
    }
}