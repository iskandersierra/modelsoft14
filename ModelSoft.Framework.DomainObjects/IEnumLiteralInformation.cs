namespace ModelSoft.Framework.DomainObjects
{

    public enum EnumLiteralRole
    {
        Member,
        Flag,
        Mask,
        GroupMember,
        Group,
    }
    public interface IEnumLiteralInformation : INamedInformation
    {
        IEnumTypeInformation EnumType { get; }
        object Instance { get; }
        EnumLiteralRole Role { get; }

        byte ByteInstance { get; }
        sbyte SByteInstance { get; }
        short Int16Instance { get; }
        ushort UInt16Instance { get; }
        int Int32Instance { get; }
        uint UInt32Instance { get; }
        long Int64Instance { get; }
        ulong UInt64Instance { get; }

        void SetEnumType(IEnumTypeInformation aEnumType);
    }
}