using System;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework;
using ModelSoft.Framework.ObjectManagement;

namespace ModelSoft.Framework.Tests.ObjectManagement
{
  [TestClass]
  public class CustomCLRPrimitiveTypeDescriptionTests
  {
    private static object InvalidValue = new object();

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionBoolean()
    {
      var conformantValues = new Boolean[] { false, true, };
      var nonConformantValues = new object[] { null, new object(), "", Guid.Empty, 345, 3.14, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.Boolean,
        identifier: "Boolean",
        name: "Boolean",
        defaultValue: false,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: null
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionByte()
    {
      var conformantValues = new Byte[] { (Byte)0, (Byte)1, (Byte)120, (Byte)255, };
      var incrementedValues = new object[] { (Byte)1, (Byte)2, (Byte)121, InvalidValue, };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345, 3.14, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.Byte,
        identifier: "Byte",
        name: "Byte",
        defaultValue: (Byte)0,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: incrementedValues
      );

    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionSByte()
    {
      var conformantValues = new SByte[] { (SByte)(-120), (SByte)0, (SByte)1, (SByte)127, };
      var incrementedValues = new object[] { (SByte)(-119), (SByte)1, (SByte)2, InvalidValue, };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345, 3.14, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.SByte,
        identifier: "SByte",
        name: "SByte",
        defaultValue: (SByte)0,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: incrementedValues.Cast<object>().ToArray()
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionChar()
    {
      var conformantValues = new Char[] { (Char)0, '0', 'A', 'a', '\u1234', };
      var incrementedValues = new object[] { (Char)1, '1', 'B', 'b', '\u1235', };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345, 3.14, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.Char,
        identifier: "Char",
        name: "Char",
        defaultValue: (Char)0,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: incrementedValues.Cast<object>().ToArray()
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionString()
    {
      var conformantValues = new String[] { "", "0", "Hello World", "Low", "a", };
      var nonConformantValues = new object[] { null, true, new object(), 'r', Guid.Empty, 345, 3.14, };
      var valuesToString = conformantValues.Select(e => e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => e).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.String,
        identifier: "String",
        name: "String",
        defaultValue: String.Empty,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: null
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionDouble()
    {
      var conformantValues = new Double[] { (-120e123), -45, (-120e-123), 0, (120e-123), 1, Math.PI, (120e123), };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345, 3.14f, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.Double,
        identifier: "Double",
        name: "Double",
        defaultValue: 0.0,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: null
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionSingle()
    {
      var conformantValues = new Single[] { (-120e13f), -45f, (-120e-13f), 0f, (120e-13f), 1f, (Single)Math.PI, (120e13f), };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345, 3.14, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.Single,
        identifier: "Single",
        name: "Single",
        defaultValue: 0.0f,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: null
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionDecimal()
    {
      var conformantValues = new Decimal[] { (-120e13M), -45M, (-120e-13M), 0M, (120e-13M), 1M, (Decimal)Math.PI, (120e13M), };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345, 3.14, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.Decimal,
        identifier: "Decimal",
        name: "Decimal",
        defaultValue: 0.0M,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: null
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionInt16()
    {
      var conformantValues = new Int16[] { Int16.MinValue, -45, -1, 0, 1, 45, Int16.MaxValue, };
      var incrementedValues = new object[] { (Int16)(conformantValues[0] + 1), (Int16)(conformantValues[1] + 1), (Int16)(conformantValues[2] + 1), (Int16)(conformantValues[3] + 1), (Int16)(conformantValues[4] + 1), (Int16)(conformantValues[5] + 1), InvalidValue };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345, 3.14f, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.Int16,
        identifier: "Int16",
        name: "Int16",
        defaultValue: (Int16)0,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: incrementedValues
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionInt32()
    {
      var conformantValues = new Int32[] { Int32.MinValue, -45, -1, 0, 1, 45, Int32.MaxValue, };
      var incrementedValues = new object[] { (Int32)(conformantValues[0] + 1), (Int32)(conformantValues[1] + 1), (Int32)(conformantValues[2] + 1), (Int32)(conformantValues[3] + 1), (Int32)(conformantValues[4] + 1), (Int32)(conformantValues[5] + 1), InvalidValue };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345L, 3.14f, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.Int32,
        identifier: "Int32",
        name: "Int32",
        defaultValue: (Int32)0,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: incrementedValues
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionInt64()
    {
      var conformantValues = new Int64[] { Int64.MinValue, -45, -1, 0, 1, 45, Int64.MaxValue, };
      var incrementedValues = new object[] { (Int64)(conformantValues[0] + 1), (Int64)(conformantValues[1] + 1), (Int64)(conformantValues[2] + 1), (Int64)(conformantValues[3] + 1), (Int64)(conformantValues[4] + 1), (Int64)(conformantValues[5] + 1), InvalidValue };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345, 3.14f, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.Int64,
        identifier: "Int64",
        name: "Int64",
        defaultValue: (Int64)0,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: incrementedValues
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionUInt16()
    {
      var conformantValues = new UInt16[] { UInt16.MinValue, 1, 2, 45, 450, 4500, UInt16.MaxValue, };
      var incrementedValues = new object[] { (UInt16)(conformantValues[0] + 1), (UInt16)(conformantValues[1] + 1), (UInt16)(conformantValues[2] + 1), (UInt16)(conformantValues[3] + 1), (UInt16)(conformantValues[4] + 1), (UInt16)(conformantValues[5] + 1), InvalidValue };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345, 3.14f, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.UInt16,
        identifier: "UInt16",
        name: "UInt16",
        defaultValue: (UInt16)0,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: incrementedValues
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionUInt32()
    {
      var conformantValues = new UInt32[] { UInt32.MinValue, 1, 2, 45, 450, 4500, UInt32.MaxValue, };
      var incrementedValues = new object[] { (UInt32)(conformantValues[0] + 1), (UInt32)(conformantValues[1] + 1), (UInt32)(conformantValues[2] + 1), (UInt32)(conformantValues[3] + 1), (UInt32)(conformantValues[4] + 1), (UInt32)(conformantValues[5] + 1), InvalidValue };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345, 3.14f, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.UInt32,
        identifier: "UInt32",
        name: "UInt32",
        defaultValue: (UInt32)0,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: incrementedValues
      );
    }

    [TestMethod]
    public void CustomCLRPrimitiveTypeDescriptionUInt64()
    {
      var conformantValues = new UInt64[] { UInt64.MinValue, 1, 2, 45, 450, 4500, UInt64.MaxValue, };
      var incrementedValues = new object[] { (UInt64)(conformantValues[0] + 1), (UInt64)(conformantValues[1] + 1), (UInt64)(conformantValues[2] + 1), (UInt64)(conformantValues[3] + 1), (UInt64)(conformantValues[4] + 1), (UInt64)(conformantValues[5] + 1), InvalidValue };
      var nonConformantValues = new object[] { null, true, new object(), "", Guid.Empty, 345, 3.14f, };
      var valuesToString = conformantValues.Select(e => "" + e).ToArray();
      var valuesSerializedAsString = conformantValues.Select(e => XmlConvert.ToString(e)).ToArray();
      var valuesSerializedAsBinary = conformantValues.Select(e => BinaryConvert.ToBinary(e)).ToArray();

      TestCLRType(CustomCLRTypes.UInt64,
        identifier: "UInt64",
        name: "UInt64",
        defaultValue: (UInt64)0,
        supportsCompare: true,
        conformantValues: conformantValues.Cast<object>().ToArray(),
        nonConformantValues: nonConformantValues,
        valuesToString: valuesToString,
        valuesSerializedAsString: valuesSerializedAsString,
        valuesSerializedAsBinary: valuesSerializedAsBinary,
        incrementedValues: incrementedValues
      );
    }

    private void TestCLRType<TNetType>(
      CustomPrimitiveTypeDescription type,
      string identifier, string name,
      TNetType defaultValue,
      bool supportsCompare,
      object[] conformantValues,
      object[] nonConformantValues,
      string[] valuesToString = null,
      string[] valuesSerializedAsString = null,
      byte[][] valuesSerializedAsBinary = null,
      object[] incrementedValues = null
    )
    {
      // Basic
      Assert.IsNotNull(type, "{0} primitive type cannot be null", name);
      Assert.AreEqual(identifier, type.Identifier);
      Assert.AreEqual(name, type.Name);

      // DefaultValue
      Assert.AreEqual(defaultValue, type.DefaultValue);
      
      // IsConformant(value)
      foreach (var item in conformantValues)
        Assert.IsTrue(type.IsConformant(item), "{0} must be conformant with type {1}", item, name);
      foreach (var item in nonConformantValues)
        Assert.IsFalse(type.IsConformant(item), "{0} must not be conformant with type {1}", item, name);
      
      // ToString(value)
      if (valuesToString != null)
        foreach (var pair in conformantValues.Enumerate(valuesToString))
        {
          Assert.AreEqual(pair.Item2, type.ToString(pair.Item1));
        }

      // AreEquals
      for (int i = 0; i < conformantValues.Length; i++)
        for (int j = 0; j < conformantValues.Length; j++)
          if (i == j)
            Assert.IsTrue(type.AreEquals(conformantValues[i], conformantValues[j]), "Values {0} and {1} must be considered as equals", conformantValues[i], conformantValues[j]);
          else
            Assert.IsFalse(type.AreEquals(conformantValues[i], conformantValues[j]), "Values {0} and {1} must be considered as different", conformantValues[i], conformantValues[j]);

      // Compare
      if (supportsCompare)
      {
        Assert.IsTrue(type.SupportsCompare, "{0} must support value comparison", name);
        for (int i = 0; i < conformantValues.Length; i++)
          for (int j = 0; j < conformantValues.Length; j++)
            Assert.IsTrue(Math.Sign(type.Compare(conformantValues[i], conformantValues[j])) == Math.Sign(i - j), 
              "Values {0} and {1} must be considered as {2}", 
              conformantValues[i], conformantValues[j],
              Math.Sign(i - j) == 0 ? "equals" : (Math.Sign(i - j) < 0 ? "ordered" : "disordered"));
      }
      else
      {
        Assert.IsFalse(type.SupportsCompare, "{0} cannot support value comparison", name);
      }

      // Serialize/Deserialize to String
      if (valuesSerializedAsString != null)
      {
        Assert.IsTrue(type.SupportsStringSerialization, "{0} must support string serialization/deserialization", name);
        foreach (var pair in conformantValues.Enumerate(valuesSerializedAsString))
        {
          Assert.AreEqual(pair.Item2, type.SerializeValueToString(pair.Item1));
          Assert.AreEqual(pair.Item1, type.DeserializeValueFromString(pair.Item2));
        }
      }
      else
      {
        Assert.IsFalse(type.SupportsStringSerialization, "{0} cannot support string serialization/deserialization", name);
      }

      // Serialize/Deserialize to Binary
      if (valuesSerializedAsBinary != null)
      {
        Assert.IsTrue(type.SupportsBinarySerialization, "{0} must support binary serialization/deserialization", name);
        foreach (var pair in conformantValues.Enumerate(valuesSerializedAsBinary))
        {
          Assert.IsTrue(pair.Item2.SameSequenceAs(type.SerializeValueToBinary(pair.Item1)));
          Assert.AreEqual(pair.Item1, type.DeserializeValueFromBinary(pair.Item2));
        }
      }
      else
      {
        Assert.IsFalse(type.SupportsBinarySerialization, "{0} cannot support binary serialization/deserialization", name);
      }

      // Increment/Decrement
      if (incrementedValues != null)
      {
        Assert.IsTrue(type.SupportsIncrementDecrement, "{0} must support increment/decrement", name);
        foreach (var pair in conformantValues.Enumerate(incrementedValues).Where(t => t.Item2 != InvalidValue))
        {
          Assert.AreEqual(pair.Item2, type.Increment(pair.Item1));
          Assert.AreEqual(pair.Item1, type.Decrement(pair.Item2));
        }
      }
      else
      {
        Assert.IsFalse(type.SupportsIncrementDecrement, "{0} cannot support increment/decrement", name);
      }
    }
  }
}
