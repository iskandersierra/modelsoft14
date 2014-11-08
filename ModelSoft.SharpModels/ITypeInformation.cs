using System.Collections.Generic;

namespace ModelSoft.SharpModels
{
    public interface ITypeInformation
    {
        bool IsInstance(IModelElement instance);

        IModelElement CreateInstance();

        string Url { get; }

        IEnumerable<IModelProperty> Properties { get; }

        IModelProperty GetProperty(string propertyName, bool throwIfNotFound = true);
        
        bool TryGetProperty(string propertyName, out IModelProperty property);
    }
}