using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Serialization
{
  public class SimpleBinarySerializer :
    IBinarySerializer
  {
    public byte[] SerializeToBinary(object value)
    {
      if (value == null) return null;

      if (value is bool) return BinaryConvert.ToBinary((bool)value);
      if (value is string) return BinaryConvert.ToBinary((string)value);
      if (value is int) return BinaryConvert.ToBinary((int)value);
      if (value is Int16) return BinaryConvert.ToBinary((Int16)value);
      if (value is Byte) return BinaryConvert.ToBinary((Byte)value);
      if (value is SByte) return BinaryConvert.ToBinary((SByte)value);
      if (value is Char) return BinaryConvert.ToBinary((Char)value);

      if (value is DateTime) return BinaryConvert.ToBinary((DateTime)value);
      if (value is DateTimeOffset) return BinaryConvert.ToBinary((DateTimeOffset)value);
      if (value is TimeSpan) return BinaryConvert.ToBinary((TimeSpan)value);

      if (value is Single) return BinaryConvert.ToBinary((Single)value);
      if (value is Double) return BinaryConvert.ToBinary((Double)value);
      if (value is Decimal) return BinaryConvert.ToBinary((Decimal)value);

      if (value is Guid) return BinaryConvert.ToBinary((Guid)value);

      if (value is Int64) return BinaryConvert.ToBinary((Int64)value);
      if (value is UInt32) return BinaryConvert.ToBinary((UInt32)value);
      if (value is UInt16) return BinaryConvert.ToBinary((UInt16)value);
      if (value is UInt64) return BinaryConvert.ToBinary((UInt64)value);

      throw new ArgumentOutOfRangeException("value", "Value type not supported: " + value.GetType().ToString());
    }

    public object DeserializeFromString(byte[] serializedValue, Type targetType)
    {
      targetType.RequireNotNull("targetType");

      if (serializedValue == null)
        if (targetType.IsValueType)
          throw new ArgumentNullException("Cannot convert null to a value type: " + targetType.ToString());
        else
          return null;

      if (targetType == CommonTypes.TypeOfBoolean) return BinaryConvert.ToBoolean(serializedValue);
      if (targetType == CommonTypes.TypeOfString) return BinaryConvert.ToString(serializedValue);
      if (targetType == CommonTypes.TypeOfInt32) return BinaryConvert.ToInt32(serializedValue);
      if (targetType == CommonTypes.TypeOfInt16) return BinaryConvert.ToInt16(serializedValue);
      if (targetType == CommonTypes.TypeOfByte) return BinaryConvert.ToByte(serializedValue);
      if (targetType == CommonTypes.TypeOfSByte) return BinaryConvert.ToSByte(serializedValue);
      if (targetType == CommonTypes.TypeOfChar) return BinaryConvert.ToChar(serializedValue);

      if (targetType == CommonTypes.TypeOfDateTime) return BinaryConvert.ToDateTime(serializedValue);
      if (targetType == CommonTypes.TypeOfDateTimeOffset) return BinaryConvert.ToDateTimeOffset(serializedValue);
      if (targetType == CommonTypes.TypeOfTimeSpan) return BinaryConvert.ToTimeSpan(serializedValue);

      if (targetType == CommonTypes.TypeOfSingle) return BinaryConvert.ToSingle(serializedValue);
      if (targetType == CommonTypes.TypeOfDouble) return BinaryConvert.ToDouble(serializedValue);
      if (targetType == CommonTypes.TypeOfDecimal) return BinaryConvert.ToDecimal(serializedValue);

      if (targetType == CommonTypes.TypeOfGuid) return BinaryConvert.ToGuid(serializedValue);

      if (targetType == CommonTypes.TypeOfInt64) return BinaryConvert.ToInt64(serializedValue);
      if (targetType == CommonTypes.TypeOfUInt16) return BinaryConvert.ToUInt16(serializedValue);
      if (targetType == CommonTypes.TypeOfUInt32) return BinaryConvert.ToUInt32(serializedValue);
      if (targetType == CommonTypes.TypeOfUInt64) return BinaryConvert.ToUInt64(serializedValue);

      throw new ArgumentOutOfRangeException("value", "Value type not supported: " + targetType.ToString());
    }

    public IEnumerable<Type> SerializableTypes
    {
      get
      {
        // Sorted by estimated frequency of use
        yield return CommonTypes.TypeOfBoolean;
        yield return CommonTypes.TypeOfString;
        yield return CommonTypes.TypeOfInt32;
        yield return CommonTypes.TypeOfInt16;
        yield return CommonTypes.TypeOfByte;
        yield return CommonTypes.TypeOfSByte;
        yield return CommonTypes.TypeOfChar;

        yield return CommonTypes.TypeOfDateTime;
        yield return CommonTypes.TypeOfDateTimeOffset;
        yield return CommonTypes.TypeOfTimeSpan;

        yield return CommonTypes.TypeOfSingle;
        yield return CommonTypes.TypeOfDouble;
        yield return CommonTypes.TypeOfDecimal;

        yield return CommonTypes.TypeOfGuid;

        yield return CommonTypes.TypeOfInt64;
        yield return CommonTypes.TypeOfUInt16;
        yield return CommonTypes.TypeOfUInt32;
        yield return CommonTypes.TypeOfUInt64;
      }
    }
  }
}
