using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework.Serialization;

namespace ModelSoft.Framework.Tests
{
  [TestClass]
  public class SimpleBinarySerializerTests
  {
    [TestMethod]
    public void SimpleBinarySerializerTests_Null()
    {
      var serializer = new SimpleBinarySerializer();

      var value = (object)null;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(string));

      Assert.AreEqual(null, serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_True()
    {
      var serializer = new SimpleBinarySerializer();

      var value = true;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(bool));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_False()
    {
      var serializer = new SimpleBinarySerializer();

      var value = false;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(bool));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_String()
    {
      var serializer = new SimpleBinarySerializer();

      var value = "test string";
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(string));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_Int32()
    {
      var serializer = new SimpleBinarySerializer();

      var value = 123;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(int));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_Int16()
    {
      var serializer = new SimpleBinarySerializer();

      var value = (Int16)12345;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Int16));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_Byte()
    {
      var serializer = new SimpleBinarySerializer();

      var value = (Byte)123;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Byte));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_SByte()
    {
      var serializer = new SimpleBinarySerializer();

      var value = (SByte)123;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(SByte));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_Char()
    {
      var serializer = new SimpleBinarySerializer();

      var value = 'ñ';
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Char));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_DateTime()
    {
      var serializer = new SimpleBinarySerializer();

      var value = new DateTime(1978, 11, 1, 11, 30, 12, 345);
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(DateTime));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_DateTimeOffset()
    {
      var serializer = new SimpleBinarySerializer();

      var value = new DateTimeOffset(1978, 11, 1, 11, 30, 12, TimeSpan.FromHours(-5));
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(DateTimeOffset));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_TimeSpan()
    {
      var serializer = new SimpleBinarySerializer();

      var value = TimeSpan.FromMilliseconds(1234567);
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(TimeSpan));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_Single()
    {
      var serializer = new SimpleBinarySerializer();

      var value = 3.1415926f;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Single));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_Double()
    {
      var serializer = new SimpleBinarySerializer();

      var value = 3.14159265358;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Double));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_Decimal()
    {
      var serializer = new SimpleBinarySerializer();

      var value = (Decimal)3.14159265358979323;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Decimal));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_Guid()
    {
      var serializer = new SimpleBinarySerializer();

      var value = Guid.NewGuid();
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Guid));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_Int64()
    {
      var serializer = new SimpleBinarySerializer();

      var value = 12345678901234567L;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Int64));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_UInt32()
    {
      var serializer = new SimpleBinarySerializer();

      var value = (UInt32)123456789;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(UInt32));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_UInt16()
    {
      var serializer = new SimpleBinarySerializer();

      var value = (UInt16)12345;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(UInt16));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_UInt64()
    {
      var serializer = new SimpleBinarySerializer();

      var value = (UInt64)123456789012345L;
      var serializedValue = serializer.SerializeToBinary(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(UInt64));

      Assert.AreNotEqual(null, serializedValue);
      Assert.AreEqual(BinaryConvert.ToBinarySize(value), serializedValue.Length);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void SimpleBinarySerializerTests_AllTypes()
    {
      var serializer = new SimpleBinarySerializer();
      var types = serializer.SerializableTypes.ToList();

      Assert.IsTrue(types.Contains(CommonTypes.TypeOfBoolean));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfString));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfInt32));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfInt16));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfByte));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfSByte));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfChar));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfDateTime));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfDateTimeOffset));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfTimeSpan));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfSingle));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfDouble));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfDecimal));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfGuid));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfInt64));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfUInt16));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfUInt32));
      Assert.IsTrue(types.Contains(CommonTypes.TypeOfUInt64));
    }
  }
}
