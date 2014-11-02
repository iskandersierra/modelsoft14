using System;
using System.Globalization;

namespace ModelSoft.Framework.DomainObjects
{
    public class EnumLiteralInformation :
        NamedInformation,
        IEnumLiteralInformation
    {
        ///TODO:
        private EnumLiteralRole _role;
        private object _instance;

        public EnumLiteralInformation(
            IModelElementInformationProvider informationProvider,
            string name,
            object instance,

            Func<CultureInfo, string> displayNameFunc = null,
            Func<CultureInfo, string> descriptionFunc = null,
            Func<CultureInfo, string> categoryFunc = null)
            : base(informationProvider, name, displayNameFunc, descriptionFunc, categoryFunc)
        {
        }

        public IEnumTypeInformation EnumType { get; private set; }

        public object Instance
        {
            get { return _instance; }
        }

        public EnumLiteralRole Role
        {
            get { return _role; }
        }

        public byte ByteInstance { get; private set; }
        public sbyte SByteInstance { get; private set; }
        public short Int16Instance { get; private set; }
        public ushort UInt16Instance { get; private set; }
        public int Int32Instance { get; private set; }
        public uint UInt32Instance { get; private set; }
        public long Int64Instance { get; private set; }
        public ulong UInt64Instance { get; private set; }

        public void SetEnumType(IEnumTypeInformation aEnumType)
        {
            throw new NotImplementedException();
        }
    }
}