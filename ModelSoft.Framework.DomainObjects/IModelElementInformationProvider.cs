using System;

namespace ModelSoft.Framework.DomainObjects
{
    public interface IModelElementInformationProvider
    {
        TypeInformation GetTypeInformation(Type type);
        TypeInformation GetTypeInformation(string globalIdentifier);
    }
}