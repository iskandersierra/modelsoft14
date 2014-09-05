using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ModelSoft.Framework.Serialization
{
    public class XmlConvertStringSerializer :
      IStringSerializer
    {
        private static XmlConvertStringSerializer _Default;
        public static XmlConvertStringSerializer Default
        {
            get
            {
                if (_Default == null)
                {
                    _Default = new XmlConvertStringSerializer();
                }
                return _Default;
            }
        }

        public string SerializeToString(object value)
        {
            if (value == null) return null;

            if (value is bool) return XmlConvert.ToString((bool)value);
            if (value is string) return (string)value;
            if (value is int) return XmlConvert.ToString((int)value);
            if (value is Int16) return XmlConvert.ToString((Int16)value);
            if (value is Byte) return XmlConvert.ToString((Byte)value);
            if (value is SByte) return XmlConvert.ToString((SByte)value);
            if (value is Char) return XmlConvert.ToString((Char)value);

            if (value is DateTime) return XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.RoundtripKind);
            if (value is DateTimeOffset) return XmlConvert.ToString((DateTimeOffset)value);
            if (value is TimeSpan) return XmlConvert.ToString((TimeSpan)value);

            if (value is Single) return XmlConvert.ToString((Single)value);
            if (value is Double) return XmlConvert.ToString((Double)value);
            if (value is Decimal) return XmlConvert.ToString((Decimal)value);

            if (value is Guid) return XmlConvert.ToString((Guid)value);

            if (value is Int64) return XmlConvert.ToString((Int64)value);
            if (value is UInt32) return XmlConvert.ToString((UInt32)value);
            if (value is UInt16) return XmlConvert.ToString((UInt16)value);
            if (value is UInt64) return XmlConvert.ToString((UInt64)value);

            throw new ArgumentOutOfRangeException("value", "Value type not supported: " + value.GetType().ToString());
        }

        public object DeserializeFromString(string serializedValue, Type targetType)
        {
            targetType.RequireNotNull("targetType");

            if (serializedValue == null)
                if (targetType.IsValueType)
                    throw new ArgumentNullException("Cannot convert null to a value type: " + targetType.ToString());
                else
                    return null;

            if (targetType == CommonTypes.TypeOfBoolean) return XmlConvert.ToBoolean(serializedValue);
            if (targetType == CommonTypes.TypeOfString) return serializedValue;
            if (targetType == CommonTypes.TypeOfInt32) return XmlConvert.ToInt32(serializedValue);
            if (targetType == CommonTypes.TypeOfInt16) return XmlConvert.ToInt16(serializedValue);
            if (targetType == CommonTypes.TypeOfByte) return XmlConvert.ToByte(serializedValue);
            if (targetType == CommonTypes.TypeOfSByte) return XmlConvert.ToSByte(serializedValue);
            if (targetType == CommonTypes.TypeOfChar) return XmlConvert.ToChar(serializedValue);

            if (targetType == CommonTypes.TypeOfDateTime) return XmlConvert.ToDateTime(serializedValue, XmlDateTimeSerializationMode.RoundtripKind);
            if (targetType == CommonTypes.TypeOfDateTimeOffset) return XmlConvert.ToDateTimeOffset(serializedValue);
            if (targetType == CommonTypes.TypeOfTimeSpan) return XmlConvert.ToTimeSpan(serializedValue);

            if (targetType == CommonTypes.TypeOfSingle) return XmlConvert.ToSingle(serializedValue);
            if (targetType == CommonTypes.TypeOfDouble) return XmlConvert.ToDouble(serializedValue);
            if (targetType == CommonTypes.TypeOfDecimal) return XmlConvert.ToDecimal(serializedValue);

            if (targetType == CommonTypes.TypeOfGuid) return XmlConvert.ToGuid(serializedValue);

            if (targetType == CommonTypes.TypeOfInt64) return XmlConvert.ToInt64(serializedValue);
            if (targetType == CommonTypes.TypeOfUInt16) return XmlConvert.ToUInt16(serializedValue);
            if (targetType == CommonTypes.TypeOfUInt32) return XmlConvert.ToUInt32(serializedValue);
            if (targetType == CommonTypes.TypeOfUInt64) return XmlConvert.ToUInt64(serializedValue);

            throw new ArgumentOutOfRangeException("value", "Value type not supported: " + targetType.ToString());
        }

        public IEnumerable<Type> SerializableTypes
        {
            get
            {
                return _SerializableTypes.AsEnumerable();
            }
        }

        // Sorted by estimated frequency of use
        private static readonly Type[] _SerializableTypes = new[]
        {
            CommonTypes.TypeOfBoolean,
            CommonTypes.TypeOfString,
            CommonTypes.TypeOfInt32,
            CommonTypes.TypeOfInt16,
            CommonTypes.TypeOfByte,
            CommonTypes.TypeOfSByte,
            CommonTypes.TypeOfChar,
            
            CommonTypes.TypeOfDateTime,
            CommonTypes.TypeOfDateTimeOffset,
            CommonTypes.TypeOfTimeSpan,
            
            CommonTypes.TypeOfSingle,
            CommonTypes.TypeOfDouble,
            CommonTypes.TypeOfDecimal,
            
            CommonTypes.TypeOfGuid,
            
            CommonTypes.TypeOfInt64,
            CommonTypes.TypeOfUInt16,
            CommonTypes.TypeOfUInt32,
            CommonTypes.TypeOfUInt64,
        };
    }
}
