using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework.Serialization;

namespace ModelSoft.Framework.Tests
{
  [TestClass]
  public class XmlConvertStringSerializerTests
  {
    [TestMethod]
    public void XmlConvertStringSerializerTests_Null()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = (object)null;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(string));

      Assert.AreEqual(null, serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_True()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = true;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(bool));

      Assert.AreEqual("true", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_False()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = false;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(bool));

      Assert.AreEqual("false", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_String()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = "test string";
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(string));

      Assert.AreEqual("test string", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_Int32()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = 12345;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(int));

      Assert.AreEqual("12345", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_Int16()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = (short)12345;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(short));

      Assert.AreEqual("12345", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_Byte()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = (Byte)12;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Byte));

      Assert.AreEqual("12", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_SByte()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = (SByte)12;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(SByte));

      Assert.AreEqual("12", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_Char()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = 'ñ';
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Char));

      Assert.AreEqual("ñ", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_DateTime()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = new DateTime(1978, 11, 1, 11, 30, 12, 345);
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(DateTime));

      Assert.AreEqual("1978-11-01T11:30:12.345", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_DateTimeOffset()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = new DateTimeOffset(1978, 11, 1, 11, 30, 12, 345, TimeSpan.FromHours(-5));
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(DateTimeOffset));

      Assert.AreEqual("1978-11-01T11:30:12.345-05:00", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_TimeSpan()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = TimeSpan.FromMilliseconds(1234567);
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(TimeSpan));

      Assert.AreEqual("PT20M34.567S", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_Single()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = 3.14159274f;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(float));

      Assert.AreEqual("3.14159274", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_Double()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = 3.1415926535897931;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(double));

      Assert.AreEqual("3.1415926535897931", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_Decimal()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = (decimal)3.14159265358979;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Decimal));

      Assert.AreEqual("3.14159265358979", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_Guid()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = new Guid("{86cdcf73-3eaa-48f9-8674-7c5a02609c28}");
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Guid));

      Assert.AreEqual("86cdcf73-3eaa-48f9-8674-7c5a02609c28", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_Int64()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = 1234567890123456L;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(Int64));

      Assert.AreEqual("1234567890123456", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_UInt16()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = (UInt16)12345;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(UInt16));

      Assert.AreEqual("12345", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_UInt32()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = (UInt32)123456789;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(UInt32));

      Assert.AreEqual("123456789", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_UInt64()
    {
      var serializer = new XmlConvertStringSerializer();

      var value = (UInt64)123456789012345L;
      var serializedValue = serializer.SerializeToString(value);
      var deserializedValue = serializer.DeserializeFromString(serializedValue, typeof(UInt64));

      Assert.AreEqual("123456789012345", serializedValue);
      Assert.AreEqual(value, deserializedValue);
    }

    [TestMethod]
    public void XmlConvertStringSerializerTests_AllTypes()
    {
      var serializer = XmlConvertStringSerializer.Default;
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
