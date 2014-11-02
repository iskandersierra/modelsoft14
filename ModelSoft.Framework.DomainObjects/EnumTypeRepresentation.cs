using System;
using System.ComponentModel;

namespace ModelSoft.Framework.DomainObjects
{
    [Flags]
    public enum EnumTypeRepresentation
    {
        UnsignedByte    = GroupMemberOneByte,
        SignedByte      = GroupMemberOneByte | FlagSigned,
        UnsignedShort   = GroupMemberTwoBytes,
        SignedShort     = GroupMemberTwoBytes | FlagSigned,
        UnsignedInteger = GroupMemberFourBytes,
        SignedInteger   = GroupMemberFourBytes | FlagSigned,
        UnsignedLong    = GroupMemberEightBytes,
        SignedLong      = GroupMemberEightBytes | FlagSigned,
        
        [EditorBrowsable(EditorBrowsableState.Never)] GroupMemberOneByte    = 0,
        [EditorBrowsable(EditorBrowsableState.Never)] GroupMemberTwoBytes   = 1,
        [EditorBrowsable(EditorBrowsableState.Never)] GroupMemberFourBytes  = 2,
        [EditorBrowsable(EditorBrowsableState.Never)] GroupMemberEightBytes = 3,
        [EditorBrowsable(EditorBrowsableState.Never)] GroupSize             = 3,

        [EditorBrowsable(EditorBrowsableState.Never)] FlagSigned = 4,
    }

    public static class EnumTypeRepresentationExtensions
    {
        public static bool IsFlagSigned(this EnumTypeRepresentation value)
        {
            return (value & EnumTypeRepresentation.FlagSigned) == EnumTypeRepresentation.FlagSigned;
        }

        public static bool IsGroupMemberOneByte(this EnumTypeRepresentation value)
        {
            return (value & EnumTypeRepresentation.GroupSize) == EnumTypeRepresentation.GroupMemberOneByte;
        }
        public static bool IsGroupMemberTwoBytes(this EnumTypeRepresentation value)
        {
            return (value & EnumTypeRepresentation.GroupSize) == EnumTypeRepresentation.GroupMemberTwoBytes;
        }
        public static bool IsGroupMemberFourBytes(this EnumTypeRepresentation value)
        {
            return (value & EnumTypeRepresentation.GroupSize) == EnumTypeRepresentation.GroupMemberFourBytes;
        }
        public static bool IsGroupMemberEightBytes(this EnumTypeRepresentation value)
        {
            return (value & EnumTypeRepresentation.GroupSize) == EnumTypeRepresentation.GroupMemberEightBytes;
        }
    }

}