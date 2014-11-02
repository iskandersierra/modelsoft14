using System.Collections.Generic;

namespace ModelSoft.Framework.DomainObjects
{
    public interface IEnumTypeInformation : ISimpleTypeInformation
    {
        bool IsFlags { get; }
        IReadOnlyCollection<IEnumLiteralInformation> Literals { get; }
        EnumTypeRepresentation Representation { get; }

        IEnumLiteralInformation FindMember(object instance);
        IEnumLiteralInformation[] FindFlagsAndGroupMembers(object instance);
        IEnumLiteralInformation[] DeserializeToLiterals(string serializedInstance);

        object Union(object instance1, object instance2);
        object Intersection(object instance1, object instance2);
        object Difference(object instance1, object instance2);
        bool ContainsFlag(object instance, object flag);
        bool IsEmpty(object instance);
    }
}