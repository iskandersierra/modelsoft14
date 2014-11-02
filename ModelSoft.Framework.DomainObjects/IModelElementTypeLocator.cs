using System;
using System.Collections.Generic;

namespace ModelSoft.Framework.DomainObjects
{
    public interface IModelElementTypeLocator
    {
        IEnumerable<Type> FindTypesByGlobalIdentifier(string globalIdentifier);
    }
}