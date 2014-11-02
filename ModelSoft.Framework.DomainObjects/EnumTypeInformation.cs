using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using ModelSoft.Framework.DomainObjects.Properties;

namespace ModelSoft.Framework.DomainObjects
{
    public class EnumTypeInformation :
        SimpleTypeInformation,
        IEnumTypeInformation
    {
        public const string EnumMemberSeparator = "|";

        private readonly bool _isFlags;
        private readonly IReadOnlyCollection<IEnumLiteralInformation> _literals;
        private readonly EnumTypeRepresentation _representation;

        public EnumTypeInformation(
            IModelElementInformationProvider informationProvider,
            Type clrType,
            string globalIdentifier,
            string name,
            bool isFlags,
            EnumTypeRepresentation representation,
            IEnumLiteralInformation[] literals,
            Func<CultureInfo, string> displayNameFunc = null,
            Func<CultureInfo, string> descriptionFunc = null,
            Func<CultureInfo, string> categoryFunc = null)
            : base(informationProvider, clrType, globalIdentifier, name, displayNameFunc, descriptionFunc, categoryFunc)
        {
            if (literals == null) throw new ArgumentNullException("literals");

            _isFlags = isFlags;
            _representation = representation;

            // Check no null literal
            if (literals.Any(e => e == null))
                throw new ArgumentNullException("literals");
            // Check no duplicate literal names
            if (literals.AreDistinct(e => e.Name))
                throw new ArgumentException(string.Format("Enum {0} cannot contain duplicated literal names: {1}", this.Name, literals.GetDuplicates(e => e.Name).ToStringList(", ", e => e.Name)), "literals");

            if (!isFlags)
            {
                // Check only member literals allowed
                var nonMemberLiterals = literals.Where(e => e.Role != EnumLiteralRole.Member).ToArray();
                if (nonMemberLiterals.Any())
                {
                    throw new ArgumentException(
                        string.Format(
                            Resources.EnumCannotContainNonMemberLiterals,
                            this.Name,
                            nonMemberLiterals.ToStringList(", ", e => e.Name)),
                        "literals");
                }
            }
            else
            {

            }

            _literals = new ReadOnlyCollection<IEnumLiteralInformation>(literals);
            literals.ForEach(literal => literal.SetEnumType(this));
        }

        public override string SerializeToString(object instance)
        {
            CheckInstance(instance);


            // find member corresponding to instance
            var member = FindMember(instance);
            if (member != null)
                return member.Name;

            if (IsFlags)
            {
                var flags = FindFlagsAndGroupMembers(instance);
                var result = flags.ToStringList(" " + EnumMemberSeparator + " ", m => m.Name);
                return result;
            }

            return base.SerializeToString(instance);
        }

        private static readonly Regex LiteralRegex = new Regex(@"^\s*(?<word>" + StringExtensions.PartCSharpIdentifierPattern + @")\s*$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private static readonly Regex FlagsRegex = new Regex(@"^\s*(?<word>" + StringExtensions.PartCSharpIdentifierPattern + @")(\s*\" + EnumMemberSeparator + @"\s*(?<word>" + StringExtensions.PartCSharpIdentifierPattern + @"))*\s*$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        public override object DeserializeFromString(string serializedInstance)
        {
            // A string of the form "Member" or "Flag | GroupMember | ..." is expected
            var literals = DeserializeToLiterals(serializedInstance);
            if (literals.Length == 0)
                return literals[0].Instance;
            var union = literals.Skip(1).Select(e => e.Instance).Aggregate(literals[0].Instance, UnionInternal);
            return union;
        }

        public IEnumLiteralInformation[] DeserializeToLiterals(string serializedInstance)
        {
            if (serializedInstance == null) throw new ArgumentNullException("serializedInstance");
            // A string of the form "Member" or "Flag | GroupMember | ..." is expected
            if (!IsFlags)
            {
                // only a member is allowed
                var match = LiteralRegex.Match(serializedInstance);
                if (match.Success)
                {
                    var literalName = match.Groups["word"].Value;
                    return new[] { FindMember(literalName, true) };
                }
                throw new FormatException(string.Format(Resources.NoLiteralFoundMatchingGivenValue, serializedInstance,
                    Name));
            }
            else
            {
                // One member or various flags and groupmembers are allowed
                var match = FlagsRegex.Match(serializedInstance);
                if (match.Success)
                {
                    var literalNames = match.Groups["word"].Captures.Cast<Capture>().Select(c => c.Value).ToArray();
                    if (literalNames.Length == 1)
                        return new[] { FindMember(literalNames[0], true) };
                    var literals = literalNames.Select(e => FindMember(e, false)).ToArray();
                    return literals;
                }
                else
                    throw new FormatException(string.Format(Resources.NoLiteralFoundMatchingGivenValue, serializedInstance, Name));
            }
        }

        public object Union(object instance1, object instance2)
        {
            if (!IsFlags) throw new NotSupportedException(string.Format(Resources.NonFlagEnumDoNotSupportOperations, Name));
            CheckInstance(instance1);
            CheckInstance(instance2);
            return UnionInternal(instance1, instance1);
        }

        private object UnionInternal(object instance1, object instance2)
        {
            switch (Representation)
            {
                case EnumTypeRepresentation.UnsignedByte:
                    {
                        var val1 = Convert.ToByte(instance1);
                        var val2 = Convert.ToByte(instance2);
                        var union = val1 | val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedByte:
                    {
                        var val1 = Convert.ToSByte(instance1);
                        var val2 = Convert.ToSByte(instance2);
                        var union = val1 | val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.UnsignedShort:
                    {
                        var val1 = Convert.ToUInt16(instance1);
                        var val2 = Convert.ToUInt16(instance2);
                        var union = val1 | val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedShort:
                    {
                        var val1 = Convert.ToInt16(instance1);
                        var val2 = Convert.ToInt16(instance2);
                        var union = val1 | val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.UnsignedInteger:
                    {
                        var val1 = Convert.ToUInt32(instance1);
                        var val2 = Convert.ToUInt32(instance2);
                        var union = val1 | val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedInteger:
                    {
                        var val1 = Convert.ToInt32(instance1);
                        var val2 = Convert.ToInt32(instance2);
                        var union = val1 | val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.UnsignedLong:
                    {
                        var val1 = Convert.ToUInt64(instance1);
                        var val2 = Convert.ToUInt64(instance2);
                        var union = val1 | val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedLong:
                    {
                        var val1 = Convert.ToInt64(instance1);
                        var val2 = Convert.ToInt64(instance2);
                        var union = val1 | val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object Intersection(object instance1, object instance2)
        {
            if (!IsFlags) throw new NotSupportedException(string.Format(Resources.NonFlagEnumDoNotSupportOperations, Name));
            CheckInstance(instance1);
            CheckInstance(instance2);
            return IntersectionInternal(instance1, instance1);
        }

        private object IntersectionInternal(object instance1, object instance2)
        {
            switch (Representation)
            {
                case EnumTypeRepresentation.UnsignedByte:
                    {
                        var val1 = Convert.ToByte(instance1);
                        var val2 = Convert.ToByte(instance2);
                        var union = val1 & val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedByte:
                    {
                        var val1 = Convert.ToSByte(instance1);
                        var val2 = Convert.ToSByte(instance2);
                        var union = val1 & val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.UnsignedShort:
                    {
                        var val1 = Convert.ToUInt16(instance1);
                        var val2 = Convert.ToUInt16(instance2);
                        var union = val1 & val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedShort:
                    {
                        var val1 = Convert.ToInt16(instance1);
                        var val2 = Convert.ToInt16(instance2);
                        var union = val1 & val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.UnsignedInteger:
                    {
                        var val1 = Convert.ToUInt32(instance1);
                        var val2 = Convert.ToUInt32(instance2);
                        var union = val1 & val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedInteger:
                    {
                        var val1 = Convert.ToInt32(instance1);
                        var val2 = Convert.ToInt32(instance2);
                        var union = val1 & val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.UnsignedLong:
                    {
                        var val1 = Convert.ToUInt64(instance1);
                        var val2 = Convert.ToUInt64(instance2);
                        var union = val1 & val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedLong:
                    {
                        var val1 = Convert.ToInt64(instance1);
                        var val2 = Convert.ToInt64(instance2);
                        var union = val1 & val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object Difference(object instance1, object instance2)
        {
            if (!IsFlags) throw new NotSupportedException(string.Format(Resources.NonFlagEnumDoNotSupportOperations, Name));
            CheckInstance(instance1);
            CheckInstance(instance2);
            return DifferenceInternal(instance1, instance1);
        }
        private object DifferenceInternal(object instance1, object instance2)
        {
            switch (Representation)
            {
                case EnumTypeRepresentation.UnsignedByte:
                    {
                        var val1 = Convert.ToByte(instance1);
                        var val2 = Convert.ToByte(instance2);
                        var union = val1 & ~val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedByte:
                    {
                        var val1 = Convert.ToSByte(instance1);
                        var val2 = Convert.ToSByte(instance2);
                        var union = val1 & ~val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.UnsignedShort:
                    {
                        var val1 = Convert.ToUInt16(instance1);
                        var val2 = Convert.ToUInt16(instance2);
                        var union = val1 & ~val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedShort:
                    {
                        var val1 = Convert.ToInt16(instance1);
                        var val2 = Convert.ToInt16(instance2);
                        var union = val1 & ~val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.UnsignedInteger:
                    {
                        var val1 = Convert.ToUInt32(instance1);
                        var val2 = Convert.ToUInt32(instance2);
                        var union = val1 & ~val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedInteger:
                    {
                        var val1 = Convert.ToInt32(instance1);
                        var val2 = Convert.ToInt32(instance2);
                        var union = val1 & ~val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.UnsignedLong:
                    {
                        var val1 = Convert.ToUInt64(instance1);
                        var val2 = Convert.ToUInt64(instance2);
                        var union = val1 & ~val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                case EnumTypeRepresentation.SignedLong:
                    {
                        var val1 = Convert.ToInt64(instance1);
                        var val2 = Convert.ToInt64(instance2);
                        var union = val1 & ~val2;
                        var result = Convert.ChangeType(union, ClrType);
                        return result;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool ContainsFlag(object instance, object flag)
        {
            if (!IsFlags) throw new NotSupportedException(string.Format(Resources.NonFlagEnumDoNotSupportOperations, Name));
            CheckInstance(instance);
            CheckInstance(flag);
            return ContainsFlagInternal(instance, flag);
        }
        private bool ContainsFlagInternal(object instance, object flag)
        {
            switch (Representation)
            {
                case EnumTypeRepresentation.UnsignedByte:
                    {
                        var val1 = Convert.ToByte(instance);
                        var val2 = Convert.ToByte(flag);
                        var contains = (val1 & val2) == val2;
                        return contains;
                    }
                case EnumTypeRepresentation.SignedByte:
                    {
                        var val1 = Convert.ToSByte(instance);
                        var val2 = Convert.ToSByte(flag);
                        var contains = (val1 & val2) == val2;
                        return contains;
                    }
                case EnumTypeRepresentation.UnsignedShort:
                    {
                        var val1 = Convert.ToUInt16(instance);
                        var val2 = Convert.ToUInt16(flag);
                        var contains = (val1 & val2) == val2;
                        return contains;
                    }
                case EnumTypeRepresentation.SignedShort:
                    {
                        var val1 = Convert.ToInt16(instance);
                        var val2 = Convert.ToInt16(flag);
                        var contains = (val1 & val2) == val2;
                        return contains;
                    }
                case EnumTypeRepresentation.UnsignedInteger:
                    {
                        var val1 = Convert.ToUInt32(instance);
                        var val2 = Convert.ToUInt32(flag);
                        var contains = (val1 & val2) == val2;
                        return contains;
                    }
                case EnumTypeRepresentation.SignedInteger:
                    {
                        var val1 = Convert.ToInt32(instance);
                        var val2 = Convert.ToInt32(flag);
                        var contains = (val1 & val2) == val2;
                        return contains;
                    }
                case EnumTypeRepresentation.UnsignedLong:
                    {
                        var val1 = Convert.ToUInt64(instance);
                        var val2 = Convert.ToUInt64(flag);
                        var contains = (val1 & val2) == val2;
                        return contains;
                    }
                case EnumTypeRepresentation.SignedLong:
                    {
                        var val1 = Convert.ToInt64(instance);
                        var val2 = Convert.ToInt64(flag);
                        var contains = (val1 & val2) == val2;
                        return contains;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool IsEmpty(object instance)
        {
            if (!IsFlags) throw new NotSupportedException(string.Format(Resources.NonFlagEnumDoNotSupportOperations, Name));
            CheckInstance(instance);
            CheckInstance(instance);
            return IsEmptyInternal(instance);
        }
        private bool IsEmptyInternal(object instance)
        {
            switch (Representation)
            {
                case EnumTypeRepresentation.UnsignedByte:
                    {
                        var val1 = Convert.ToByte(instance);
                        return val1 == 0;
                    }
                case EnumTypeRepresentation.SignedByte:
                    {
                        var val1 = Convert.ToSByte(instance);
                        return val1 == 0;
                    }
                case EnumTypeRepresentation.UnsignedShort:
                    {
                        var val1 = Convert.ToUInt16(instance);
                        return val1 == 0;
                    }
                case EnumTypeRepresentation.SignedShort:
                    {
                        var val1 = Convert.ToInt16(instance);
                        return val1 == 0;
                    }
                case EnumTypeRepresentation.UnsignedInteger:
                    {
                        var val1 = Convert.ToUInt32(instance);
                        return val1 == 0;
                    }
                case EnumTypeRepresentation.SignedInteger:
                    {
                        var val1 = Convert.ToInt32(instance);
                        return val1 == 0;
                    }
                case EnumTypeRepresentation.UnsignedLong:
                    {
                        var val1 = Convert.ToUInt64(instance);
                        return val1 == 0;
                    }
                case EnumTypeRepresentation.SignedLong:
                    {
                        var val1 = Convert.ToInt64(instance);
                        return val1 == 0;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumLiteralInformation FindMember(string literalName, bool member)
        {
            var matchingLiterals =
                Literals.Where(l => StringComparer.OrdinalIgnoreCase.Equals(l.Name, literalName) &&
                    (member && l.Role == EnumLiteralRole.Member || !member && (l.Role == EnumLiteralRole.Flag || l.Role == EnumLiteralRole.GroupMember)))
                    .ToArray();
            if (matchingLiterals.Length == 0)
                throw new FormatException(string.Format(Resources.NoLiteralFoundMatchingGivenValue, literalName, Name));
            if (matchingLiterals.Length == 1)
                return matchingLiterals[0];
            var exactMatch = matchingLiterals.FirstOrDefault(l => StringComparer.Ordinal.Equals(l.Name, literalName));
            if (exactMatch != null)
                return exactMatch;
            throw new FormatException(string.Format(Resources.AmbiguousMatchesFoundForGivenValue,
                literalName, Name, matchingLiterals.ToStringList(", ", e => e.Name)));
        }

        public IEnumLiteralInformation FindMember(object instance)
        {
            return _literals.FirstOrDefault(e => Equals(e.Instance, instance));

        }

        public IEnumLiteralInformation[] FindFlagsAndGroupMembers(object instance)
        {
            if (!IsFlags)
                throw new InvalidOperationException(string.Format(Resources.CannotFindFlagsForNonFlagEnum, this, instance));
            var result = new List<IEnumLiteralInformation>();
            throw new NotImplementedException();
        }

        public bool IsFlags
        {
            get { return _isFlags; }
        }

        public IReadOnlyCollection<IEnumLiteralInformation> Literals
        {
            get { return _literals; }
        }

        public EnumTypeRepresentation Representation
        {
            get { return _representation; }
        }
    }
}