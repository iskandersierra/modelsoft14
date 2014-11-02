using System;
using System.Collections.Generic;

namespace ModelSoft.Framework.DomainObjects
{
    public interface IModelElementRegistry : IModelElementInformationProvider
    {
        void RegisterTypes(IEnumerable<Type> typesToRegister);
        IModelElementTypeLocator Source { get; set; }
    }
}