using System;

namespace ModelSoft.Framework.DomainObjects
{
    public interface ITypeInformation : INamedInformation
    {
        string GlobalIdentifier { get; }
        //Type ClrType { get; }

        bool IsInstance(object instance);
        void CheckInstance(object instance);
        object GetDefaultValue();

        void SetAssembly(IAssemblyInformation assembly);
    }
}