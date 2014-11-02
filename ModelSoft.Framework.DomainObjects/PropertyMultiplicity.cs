using System;
using System.ComponentModel;

namespace ModelSoft.Framework.DomainObjects
{
    [Flags]
    public enum PropertyMultiplicity
    {
        Single = FlagSingle,
        Collection = FlagMultiple | FlagUnordered | FlagUnidentified,
        IdentifiedCollection = Collection | FlagIdentified,
        List = FlagMultiple | FlagOrdered | FlagUnidentified,
        IdentifiedList = List | FlagIdentified,

        [EditorBrowsable(EditorBrowsableState.Never)] FlagSingle = 0,
        [EditorBrowsable(EditorBrowsableState.Never)] FlagMultiple = 1,
        [EditorBrowsable(EditorBrowsableState.Never)] MaskMultiplicity = FlagSingle | FlagMultiple,

        [EditorBrowsable(EditorBrowsableState.Never)] FlagUnordered = 0,
        [EditorBrowsable(EditorBrowsableState.Never)] FlagOrdered = 2,
        [EditorBrowsable(EditorBrowsableState.Never)] MaskOrdering = FlagUnordered | FlagOrdered,

        [EditorBrowsable(EditorBrowsableState.Never)] FlagUnidentified = 0,
        [EditorBrowsable(EditorBrowsableState.Never)] FlagIdentified = 4,
        [EditorBrowsable(EditorBrowsableState.Never)] MaskIdentification = FlagUnidentified | FlagIdentified,
    }

    public static class PropertyMultiplicityExtensions
    {
        public static bool IsFlagSingle(this PropertyMultiplicity value)
        {
            return (value & PropertyMultiplicity.MaskMultiplicity) == PropertyMultiplicity.FlagSingle;
        }
        public static bool IsFlagMultiple(this PropertyMultiplicity value)
        {
            return (value & PropertyMultiplicity.MaskMultiplicity) == PropertyMultiplicity.FlagMultiple;
        }

        public static bool IsFlagUnordered(this PropertyMultiplicity value)
        {
            return (value & PropertyMultiplicity.MaskOrdering) == PropertyMultiplicity.FlagUnordered;
        }
        public static bool IsFlagOrdered(this PropertyMultiplicity value)
        {
            return (value & PropertyMultiplicity.MaskOrdering) == PropertyMultiplicity.FlagOrdered;
        }

        public static bool IsFlagUnidentified(this PropertyMultiplicity value)
        {
            return (value & PropertyMultiplicity.MaskIdentification) == PropertyMultiplicity.FlagUnidentified;
        }
        public static bool IsFlagIdentified(this PropertyMultiplicity value)
        {
            return (value & PropertyMultiplicity.MaskIdentification) == PropertyMultiplicity.FlagIdentified;
        }
    }
}