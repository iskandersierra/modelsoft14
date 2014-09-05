using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  public static class CustomCLRTypes
  {
    public static readonly CustomBoolPrimitiveTypeDescription           Boolean        = new CustomBoolPrimitiveTypeDescription          ("Boolean",        "Boolean")       .AsFrozen();
    public static readonly CustomBytePrimitiveTypeDescription           Byte           = new CustomBytePrimitiveTypeDescription          ("Byte",           "Byte")          .AsFrozen();
    public static readonly CustomSBytePrimitiveTypeDescription          SByte          = new CustomSBytePrimitiveTypeDescription         ("SByte",          "SByte")         .AsFrozen();
    public static readonly CustomCharPrimitiveTypeDescription           Char           = new CustomCharPrimitiveTypeDescription          ("Char",           "Char")          .AsFrozen();
    public static readonly CustomDoublePrimitiveTypeDescription         Double         = new CustomDoublePrimitiveTypeDescription        ("Double",         "Double")        .AsFrozen();
    public static readonly CustomDecimalPrimitiveTypeDescription        Decimal        = new CustomDecimalPrimitiveTypeDescription       ("Decimal",        "Decimal")       .AsFrozen();
    public static readonly CustomInt16PrimitiveTypeDescription          Int16          = new CustomInt16PrimitiveTypeDescription         ("Int16",          "Int16")         .AsFrozen();
    public static readonly CustomUInt16PrimitiveTypeDescription         UInt16         = new CustomUInt16PrimitiveTypeDescription        ("UInt16",         "UInt16")        .AsFrozen();
    public static readonly CustomInt32PrimitiveTypeDescription          Int32          = new CustomInt32PrimitiveTypeDescription         ("Int32",          "Int32")         .AsFrozen();
    public static readonly CustomUInt32PrimitiveTypeDescription         UInt32         = new CustomUInt32PrimitiveTypeDescription        ("UInt32",         "UInt32")        .AsFrozen();
    public static readonly CustomInt64PrimitiveTypeDescription          Int64          = new CustomInt64PrimitiveTypeDescription         ("Int64",          "Int64")         .AsFrozen();
    public static readonly CustomUInt64PrimitiveTypeDescription         UInt64         = new CustomUInt64PrimitiveTypeDescription        ("UInt64",         "UInt64")        .AsFrozen();
    public static readonly CustomSinglePrimitiveTypeDescription         Single         = new CustomSinglePrimitiveTypeDescription        ("Single",         "Single")        .AsFrozen();
    public static readonly CustomStringPrimitiveTypeDescription         String         = new CustomStringPrimitiveTypeDescription        ("String",         "String")        .AsFrozen();
    public static readonly CustomDateTimePrimitiveTypeDescription       DateTime       = new CustomDateTimePrimitiveTypeDescription      ("DateTime",       "DateTime")      .AsFrozen();
    public static readonly CustomDateTimeOffsetPrimitiveTypeDescription DateTimeOffset = new CustomDateTimeOffsetPrimitiveTypeDescription("DateTimeOffset", "DateTimeOffset").AsFrozen();
    public static readonly CustomTimeSpanPrimitiveTypeDescription       TimeSpan       = new CustomTimeSpanPrimitiveTypeDescription      ("TimeSpan",       "TimeSpan")      .AsFrozen();
    public static readonly CustomGuidPrimitiveTypeDescription           Guid           = new CustomGuidPrimitiveTypeDescription          ("Guid",           "Guid")          .AsFrozen();
  }

  public class CustomCLRPrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomCLRPrimitiveTypeDescription()
    {
    }

    public CustomCLRPrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    private object _DefaultValue;
    public override object DefaultValue
    {
      get { return _DefaultValue; }
    }
    public object TheDefaultValue
    {
      get { return DefaultValue; }
      set
      {
        CheckModifyFrozen();
        this._DefaultValue = value;
      }
    }

    private Func<object, string> stringSerializer;
    public Func<object, string> StringSerializer
    {
      get { return stringSerializer; }
      set
      {
        CheckModifyFrozen();
        stringSerializer = value;
      }
    }

    private Func<string, object> stringDeserializer;
    public Func<string, object> StringDeserializer
    {
      get { return stringDeserializer; }
      set
      {
        CheckModifyFrozen();
        stringDeserializer = value;
      }
    }

    private Func<object, byte[]> binarySerializer;
    public Func<object, byte[]> BinarySerializer
    {
      get { return binarySerializer; }
      set 
      {
        CheckModifyFrozen();
        binarySerializer = value; 
      }
    }

    private Func<byte[], object> binaryDeserializer;
    public Func<byte[], object> BinaryDeserializer
    {
      get { return binaryDeserializer; }
      set 
      {
        CheckModifyFrozen();
        binaryDeserializer = value; 
      }
    }

    private Func<object, object> incrementer;
    public Func<object, object> Incrementer
    {
      get { return incrementer; }
      set 
      {
        CheckModifyFrozen();
        incrementer = value; 
      }
    }

    private Func<object, object> decrementer;
    public Func<object, object> Decrementer
    {
      get { return decrementer; }
      set 
      {
        CheckModifyFrozen();
        decrementer = value; 
      }
    }
    
    private Func<object,bool> isConformantEvaluator;
    public Func<object,bool> IsConformantEvaluator
    {
      get { return isConformantEvaluator; }
      set 
      {
        CheckModifyFrozen();
        isConformantEvaluator = value; 
      }
    }

    private Func<object, object, bool> areEqualsEvaluator;
    public Func<object, object, bool> AreEqualsEvaluator
    {
      get { return areEqualsEvaluator; }
      set 
      {
        CheckModifyFrozen();
        areEqualsEvaluator = value; 
      }
    }

    private Func<object, int> getHashCodeEvaluator;
    public Func<object, int> GetHashCodeEvaluator
    {
      get { return getHashCodeEvaluator; }
      set 
      {
        CheckModifyFrozen();
        getHashCodeEvaluator = value; 
      }
    }

    private Func<object, object, int> compareEvaluator;
    public Func<object, object, int> CompareEvaluator
    {
      get { return compareEvaluator; }
      set 
      {
        CheckModifyFrozen();
        compareEvaluator = value; 
      }
    }


    public override bool SupportsStringSerialization
    {
      get { return stringSerializer != null && stringDeserializer != null; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return binarySerializer != null && binaryDeserializer != null; }
    }

    public override bool SupportsIncrementDecrement
    {
      get { return incrementer != null && decrementer != null; }
    }

    public override string SerializeValueToString(object value)
    {
      if (!SupportsStringSerialization) return base.SerializeValueToString(value);
      return stringSerializer(value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      if (!SupportsStringSerialization) return base.DeserializeValueFromString(serializedValue);
      return stringDeserializer(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      if (!SupportsBinarySerialization) return base.SerializeValueToBinary(value);
      return binarySerializer(value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      if (!SupportsBinarySerialization) return DeserializeValueFromBinary(serializedValue);
      return binaryDeserializer(serializedValue);
    }

    public override object Increment(object value)
    {
      if (!SupportsIncrementDecrement) return base.Increment(value);
      return incrementer(value);
    }

    public override object Decrement(object value)
    {
      if (!SupportsIncrementDecrement) return base.Decrement(value);
      return decrementer(value);
    }

    public override bool IsConformant(object value)
    {
      if (isConformantEvaluator == null) return false;
      if (value == null)
        return false;
      return isConformantEvaluator(value);
    }

    public override bool AreEquals(object value1, object value2)
    {
      if (areEqualsEvaluator == null)
        return base.AreEquals(value1, value2);
      return areEqualsEvaluator(value1, value2);
    }

    public override int GetHashCode(object value)
    {
      if (getHashCodeEvaluator == null)
        return base.GetHashCode(value);
      return getHashCodeEvaluator(value);
    }

    public override bool SupportsCompare { get { return compareEvaluator != null; } }

    public override int Compare(object value1, object value2)
    {
      if (compareEvaluator == null)
        return base.Compare(value1, value2);
      return compareEvaluator(value1, value2);
    }

    protected override void OnFreeze()
    {
      if (isConformantEvaluator == null)
        throw new InvalidFreezingOperationException("IsConformantEvaluator is required to freeze the type");
      if ((stringSerializer == null) != (stringDeserializer == null))
        throw new InvalidFreezingOperationException("String serializer and deserializer functions must be both set or unset");
      if ((binarySerializer == null) != (binaryDeserializer == null))
        throw new InvalidFreezingOperationException("Binary serializer and deserializer functions must be both set or unset");
      if ((incrementer == null) != (decrementer == null))
        throw new InvalidFreezingOperationException("Incrementer and decrementer functions must be both set or unset");

      base.OnFreeze();
    }
  }

  public class CustomBoolPrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomBoolPrimitiveTypeDescription()
    {
    }

    public CustomBoolPrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(bool); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((bool)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToBoolean(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((bool)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToBoolean(serializedValue);
    }

    public override bool IsConformant(object value)
    {
      return value is Boolean;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (Boolean)value1 == (Boolean)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((Boolean)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((Boolean)value1).CompareTo((Boolean)value2);
    }
  }

  public class CustomBytePrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomBytePrimitiveTypeDescription()
    {
    }

    public CustomBytePrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(byte); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((byte)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToByte(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((byte)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToByte(serializedValue);
    }

    public override bool SupportsIncrementDecrement
    {
      get { return true; }
    }

    public override object Increment(object value)
    {
      var val = (byte)value;
      if (val >= byte.MaxValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_IncrementOutOfRange(ToString(val), this.ToString()));
      val++;
      return val;
    }

    public override object Decrement(object value)
    {
      var val = (byte)value;
      if (val <= byte.MinValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_DecrementOutOfRange(ToString(val), this.ToString()));
      val--;
      return val;
    }

    public override bool IsConformant(object value)
    {
      return value is Byte;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (Byte)value1 == (Byte)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((Byte)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((Byte)value1).CompareTo((Byte)value2);
    }
  }

  public class CustomSBytePrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomSBytePrimitiveTypeDescription()
    {
    }

    public CustomSBytePrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(sbyte); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((sbyte)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToSByte(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((sbyte)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToSByte(serializedValue);
    }

    public override bool SupportsIncrementDecrement
    {
      get { return true; }
    }

    public override object Increment(object value)
    {
      var val = (sbyte)value;
      if (val >= sbyte.MaxValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_IncrementOutOfRange(ToString(val), this.ToString()));
      val++;
      return val;
    }

    public override object Decrement(object value)
    {
      var val = (sbyte)value;
      if (val <= sbyte.MinValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_DecrementOutOfRange(ToString(val), this.ToString()));
      val--;
      return val;
    }

    public override bool IsConformant(object value)
    {
      return value is SByte;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (SByte)value1 == (SByte)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((SByte)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((SByte)value1).CompareTo((SByte)value2);
    }
  }

  public class CustomCharPrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomCharPrimitiveTypeDescription()
    {
    }

    public CustomCharPrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(char); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((char)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToChar(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((char)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToChar(serializedValue);
    }

    public override bool SupportsIncrementDecrement
    {
      get { return true; }
    }

    public override object Increment(object value)
    {
      var val = (char)value;
      if (val >= char.MaxValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_IncrementOutOfRange(ToString(val), this.ToString()));
      val++;
      return val;
    }

    public override object Decrement(object value)
    {
      var val = (char)value;
      if (val <= char.MinValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_DecrementOutOfRange(ToString(val), this.ToString()));
      val--;
      return val;
    }

    public override bool IsConformant(object value)
    {
      return value is Char;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (Char)value1 == (Char)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((Char)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((Char)value1).CompareTo((Char)value2);
    }
  }

  public class CustomDoublePrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomDoublePrimitiveTypeDescription()
    {
    }

    public CustomDoublePrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(double); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((double)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToDouble(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((double)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToDouble(serializedValue);
    }

    public override bool IsConformant(object value)
    {
      return value is Double;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (Double)value1 == (Double)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((Double)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((Double)value1).CompareTo((Double)value2);
    }
  }

  public class CustomDecimalPrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomDecimalPrimitiveTypeDescription()
    {
    }

    public CustomDecimalPrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(decimal); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((decimal)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToDecimal(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((decimal)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToDecimal(serializedValue);
    }

    public override bool IsConformant(object value)
    {
      return value is Decimal;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (Decimal)value1 == (Decimal)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((Decimal)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((Decimal)value1).CompareTo((Decimal)value2);
    }
  }

  public class CustomInt16PrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomInt16PrimitiveTypeDescription()
    {
    }

    public CustomInt16PrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(Int16); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((Int16)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToInt16(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((Int16)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToInt16(serializedValue);
    }

    public override bool SupportsIncrementDecrement
    {
      get { return true; }
    }

    public override object Increment(object value)
    {
      var val = (Int16)value;
      if (val >= Int16.MaxValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_IncrementOutOfRange(ToString(val), this.ToString()));
      val++;
      return val;
    }

    public override object Decrement(object value)
    {
      var val = (Int16)value;
      if (val <= Int16.MinValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_DecrementOutOfRange(ToString(val), this.ToString()));
      val--;
      return val;
    }

    public override bool IsConformant(object value)
    {
      return value is Int16;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (Int16)value1 == (Int16)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((Int16)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((Int16)value1).CompareTo((Int16)value2);
    }
  }

  public class CustomInt32PrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomInt32PrimitiveTypeDescription()
    {
    }

    public CustomInt32PrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(Int32); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((Int32)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToInt32(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((Int32)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToInt32(serializedValue);
    }

    public override bool SupportsIncrementDecrement
    {
      get { return true; }
    }

    public override object Increment(object value)
    {
      var val = (Int32)value;
      if (val >= Int32.MaxValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_IncrementOutOfRange(ToString(val), this.ToString()));
      val++;
      return val;
    }

    public override object Decrement(object value)
    {
      var val = (Int32)value;
      if (val <= Int32.MinValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_DecrementOutOfRange(ToString(val), this.ToString()));
      val--;
      return val;
    }

    public override bool IsConformant(object value)
    {
      return value is Int32;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (Int32)value1 == (Int32)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((Int32)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((Int32)value1).CompareTo((Int32)value2);
    }
  }

  public class CustomInt64PrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomInt64PrimitiveTypeDescription()
    {
    }

    public CustomInt64PrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(Int64); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((Int64)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToInt64(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((Int64)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToInt64(serializedValue);
    }

    public override bool SupportsIncrementDecrement
    {
      get { return true; }
    }

    public override object Increment(object value)
    {
      var val = (Int64)value;
      if (val >= Int64.MaxValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_IncrementOutOfRange(ToString(val), this.ToString()));
      val++;
      return val;
    }

    public override object Decrement(object value)
    {
      var val = (Int64)value;
      if (val <= Int64.MinValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_DecrementOutOfRange(ToString(val), this.ToString()));
      val--;
      return val;
    }

    public override bool IsConformant(object value)
    {
      return value is Int64;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (Int64)value1 == (Int64)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((Int64)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((Int64)value1).CompareTo((Int64)value2);
    }
  }

  public class CustomUInt64PrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomUInt64PrimitiveTypeDescription()
    {
    }

    public CustomUInt64PrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(UInt64); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((UInt64)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToUInt64(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((UInt64)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToUInt64(serializedValue);
    }

    public override bool SupportsIncrementDecrement
    {
      get { return true; }
    }

    public override object Increment(object value)
    {
      var val = (UInt64)value;
      if (val >= UInt64.MaxValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_IncrementOutOfRange(ToString(val), this.ToString()));
      val++;
      return val;
    }

    public override object Decrement(object value)
    {
      var val = (UInt64)value;
      if (val <= UInt64.MinValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_DecrementOutOfRange(ToString(val), this.ToString()));
      val--;
      return val;
    }

    public override bool IsConformant(object value)
    {
      return value is UInt64;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (UInt64)value1 == (UInt64)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((UInt64)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((UInt64)value1).CompareTo((UInt64)value2);
    }
  }

  public class CustomUInt16PrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomUInt16PrimitiveTypeDescription()
    {
    }

    public CustomUInt16PrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(UInt16); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((UInt16)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToUInt16(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((UInt16)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToUInt16(serializedValue);
    }

    public override bool SupportsIncrementDecrement
    {
      get { return true; }
    }

    public override object Increment(object value)
    {
      var val = (UInt16)value;
      if (val >= UInt16.MaxValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_IncrementOutOfRange(ToString(val), this.ToString()));
      val++;
      return val;
    }

    public override object Decrement(object value)
    {
      var val = (UInt16)value;
      if (val <= UInt16.MinValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_DecrementOutOfRange(ToString(val), this.ToString()));
      val--;
      return val;
    }

    public override bool IsConformant(object value)
    {
      return value is UInt16;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (UInt16)value1 == (UInt16)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((UInt16)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((UInt16)value1).CompareTo((UInt16)value2);
    }
  }

  public class CustomUInt32PrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomUInt32PrimitiveTypeDescription()
    {
    }

    public CustomUInt32PrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(UInt32); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((UInt32)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToUInt32(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((UInt32)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToUInt32(serializedValue);
    }

    public override bool SupportsIncrementDecrement
    {
      get { return true; }
    }

    public override object Increment(object value)
    {
      var val = (UInt32)value;
      if (val >= UInt32.MaxValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_IncrementOutOfRange(ToString(val), this.ToString()));
      val++;
      return val;
    }

    public override object Decrement(object value)
    {
      var val = (UInt32)value;
      if (val <= UInt32.MinValue)
        throw new ArgumentOutOfRangeException("value", FormattedResources.ExMsg_DecrementOutOfRange(ToString(val), this.ToString()));
      val--;
      return val;
    }

    public override bool IsConformant(object value)
    {
      return value is UInt32;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (UInt32)value1 == (UInt32)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((UInt32)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((UInt32)value1).CompareTo((UInt32)value2);
    }
  }

  public class CustomSinglePrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomSinglePrimitiveTypeDescription()
    {
    }

    public CustomSinglePrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(Single); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((Single)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToSingle(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((Single)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToSingle(serializedValue);
    }

    public override bool IsConformant(object value)
    {
      return value is Single;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (Single)value1 == (Single)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((Single)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((Single)value1).CompareTo((Single)value2);
    }
  }

  public class CustomStringPrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomStringPrimitiveTypeDescription()
    {
    }

    public CustomStringPrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return String.Empty; }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return (String)value;
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return serializedValue;
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((String)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToString(serializedValue);
    }

    public override bool IsConformant(object value)
    {
      return value is String;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (String)value1 == (String)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((String)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return StringComparer.Ordinal.Compare((String)value1, (String)value2);
    }
  }

  public class CustomDateTimePrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomDateTimePrimitiveTypeDescription()
    {
    }

    public CustomDateTimePrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(DateTime); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.RoundtripKind);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToDateTime(serializedValue, XmlDateTimeSerializationMode.RoundtripKind);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((DateTime)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToDateTime(serializedValue);
    }

    public override bool IsConformant(object value)
    {
      return value is DateTime;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (DateTime)value1 == (DateTime)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((DateTime)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((DateTime)value1).CompareTo((DateTime)value2);
    }
  }

  public class CustomDateTimeOffsetPrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomDateTimeOffsetPrimitiveTypeDescription()
    {
    }

    public CustomDateTimeOffsetPrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(DateTimeOffset); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((DateTimeOffset)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToDateTimeOffset(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((DateTimeOffset)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToDateTimeOffset(serializedValue);
    }

    public override bool IsConformant(object value)
    {
      return value is DateTimeOffset;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (DateTimeOffset)value1 == (DateTimeOffset)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((DateTimeOffset)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((DateTimeOffset)value1).CompareTo((DateTimeOffset)value2);
    }
  }

  public class CustomTimeSpanPrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomTimeSpanPrimitiveTypeDescription()
    {
    }

    public CustomTimeSpanPrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return default(TimeSpan); }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((TimeSpan)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToTimeSpan(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((TimeSpan)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToTimeSpan(serializedValue);
    }

    public override bool IsConformant(object value)
    {
      return value is TimeSpan;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (TimeSpan)value1 == (TimeSpan)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((TimeSpan)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((TimeSpan)value1).CompareTo((TimeSpan)value2);
    }
  }

  public class CustomGuidPrimitiveTypeDescription :
    CustomPrimitiveTypeDescription
  {
    public CustomGuidPrimitiveTypeDescription()
    {
    }

    public CustomGuidPrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public override object DefaultValue
    {
      get { return Guid.Empty; }
    }

    public override bool SupportsStringSerialization
    {
      get { return true; }
    }

    public override bool SupportsBinarySerialization
    {
      get { return true; }
    }

    public override string SerializeValueToString(object value)
    {
      return XmlConvert.ToString((Guid)value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return XmlConvert.ToGuid(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      return BinaryConvert.ToBinary((Guid)value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      serializedValue.RequireNotNull("serializedValue");
      return BinaryConvert.ToGuid(serializedValue);
    }

    public override bool IsConformant(object value)
    {
      return value is Guid;
    }

    public override bool AreEquals(object value1, object value2)
    {
      return (Guid)value1 == (Guid)value2;
    }

    public override int GetHashCode(object value)
    {
      return ((Guid)value).GetHashCode();
    }

    public override bool SupportsCompare { get { return true; } }

    public override int Compare(object value1, object value2)
    {
      return ((Guid)value1).CompareTo((Guid)value2);
    }
  }
}
