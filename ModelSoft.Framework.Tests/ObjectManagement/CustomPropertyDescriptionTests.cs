using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework.ObjectManagement;

namespace ModelSoft.Framework.Tests.ObjectManagement
{
  [TestClass]
  public class CustomPropertyDescriptionTests
  {
    [TestMethod]
    public void CustomPropertyDescriptionCreate1()
    {
      var property = new CustomPropertyDescription()
        {
          Identifier = "MyPropertyId",
          Name = "MyPropertyName",
          DataType = CustomCLRTypes.String
        };

      Assert.AreEqual("MyPropertyId", property.Identifier);
      Assert.AreEqual("MyPropertyName", property.Name);
      Assert.IsNull(property.DeclaringClass, "DeclaringClass");
      Assert.AreEqual(CustomCLRTypes.String, property.DataType);
      Assert.AreEqual(PropertyKind.Default, property.Kind);
      Assert.AreEqual(PropertyMultiplicity.Single, property.Multiplicity);
      Assert.IsFalse(property.HasDefaultValue, "HasDefaultValue");
      Assert.IsFalse(property.HasComputedValue, "HasComputedValue");

      property.Freeze();

      Assert.IsTrue(property.IsFrozen, "property.IsFrozen");
    }

    [TestMethod]
    public void CustomPropertyDescriptionCreate2()
    {
      var property = new CustomPropertyDescription(
        "MyPropertyId",
        "MyPropertyName",
        CustomCLRTypes.Double)
        {
          Kind = PropertyKind.Content,
          Multiplicity = PropertyMultiplicity.Multiple,
        };

      Assert.AreEqual("MyPropertyId", property.Identifier);
      Assert.AreEqual("MyPropertyName", property.Name);
      Assert.IsNull(property.DeclaringClass, "DeclaringClass");
      Assert.AreEqual(CustomCLRTypes.Double, property.DataType);
      Assert.AreEqual(PropertyKind.Content, property.Kind);
      Assert.AreEqual(PropertyMultiplicity.Multiple, property.Multiplicity);
      Assert.IsFalse(property.HasDefaultValue, "HasDefaultValue");
      Assert.IsFalse(property.HasComputedValue, "HasComputedValue");

      property.Freeze();

      Assert.IsTrue(property.IsFrozen, "property.IsFrozen");
    }

    [TestMethod]
    public void CustomPropertyDescriptionCreateWithDefault()
    {
      var property = new CustomPropertyDescription(
        "MyPropertyId",
        "MyPropertyName",
        CustomCLRTypes.Double,
        3.14)
      {
        Kind = PropertyKind.Content,
        Multiplicity = PropertyMultiplicity.Multiple,
      };

      Assert.AreEqual("MyPropertyId", property.Identifier);
      Assert.AreEqual("MyPropertyName", property.Name);
      Assert.IsNull(property.DeclaringClass, "DeclaringClass");
      Assert.AreEqual(CustomCLRTypes.Double, property.DataType);
      Assert.AreEqual(PropertyKind.Content, property.Kind);
      Assert.AreEqual(PropertyMultiplicity.Multiple, property.Multiplicity);
      Assert.IsTrue(property.HasDefaultValue, "HasDefaultValue");
      Assert.AreEqual(3.14, property.DefaultValue);
      Assert.IsFalse(property.HasComputedValue, "HasComputedValue");

      property.Freeze();

      Assert.IsTrue(property.IsFrozen, "property.IsFrozen");

      Assert.AreEqual("MyPropertyId", property.Identifier);
      Assert.AreEqual("MyPropertyName", property.Name);
      Assert.IsNull(property.DeclaringClass, "DeclaringClass");
      Assert.AreEqual(CustomCLRTypes.Double, property.DataType);
      Assert.AreEqual(PropertyKind.Content, property.Kind);
      Assert.AreEqual(PropertyMultiplicity.Multiple, property.Multiplicity);
      Assert.IsTrue(property.HasDefaultValue, "HasDefaultValue");
      Assert.AreEqual(3.14, property.DefaultValue);
      Assert.IsFalse(property.HasComputedValue, "HasComputedValue");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidFreezingOperationException))]
    public void CustomPropertyDescriptionCreateWithWrongDefault()
    {
      var property = new CustomPropertyDescription(
        "MyPropertyId",
        "MyPropertyName",
        CustomCLRTypes.Double,
        "Wrong value")
      {
        Kind = PropertyKind.Content,
        Multiplicity = PropertyMultiplicity.Multiple,
      };

      Assert.IsTrue(property.HasDefaultValue, "HasDefaultValue");

      property.Freeze();

      Assert.IsFalse(property.IsFrozen, "property.IsFrozen");
    }

    [TestMethod]
    public void CustomPropertyDescriptionCreateWithComputed()
    {
      var property = new CustomPropertyDescription(
        "MyPropertyId",
        "MyPropertyName",
        CustomCLRTypes.Double,
        om => 12345.6789)
      {
        Kind = PropertyKind.Content,
        Multiplicity = PropertyMultiplicity.Multiple,
      };

      var manager = new ObjectManager(new Mock<IObjectManagerHost>().Object);

      Assert.AreEqual("MyPropertyId", property.Identifier);
      Assert.AreEqual("MyPropertyName", property.Name);
      Assert.IsNull(property.DeclaringClass, "DeclaringClass");
      Assert.AreEqual(CustomCLRTypes.Double, property.DataType);
      Assert.AreEqual(PropertyKind.Content, property.Kind);
      Assert.AreEqual(PropertyMultiplicity.Multiple, property.Multiplicity);
      Assert.IsFalse(property.HasDefaultValue, "HasDefaultValue");
      Assert.IsTrue(property.HasComputedValue, "HasComputedValue");
      Assert.AreEqual(12345.6789, property.ComputeValue(manager));

      property.Freeze();

      Assert.IsTrue(property.IsFrozen, "property.IsFrozen");

      Assert.AreEqual("MyPropertyId", property.Identifier);
      Assert.AreEqual("MyPropertyName", property.Name);
      Assert.IsNull(property.DeclaringClass, "DeclaringClass");
      Assert.AreEqual(CustomCLRTypes.Double, property.DataType);
      Assert.AreEqual(PropertyKind.Content, property.Kind);
      Assert.AreEqual(PropertyMultiplicity.Multiple, property.Multiplicity);
      Assert.IsFalse(property.HasDefaultValue, "HasDefaultValue");
      Assert.IsTrue(property.HasComputedValue, "HasComputedValue");
      Assert.AreEqual(12345.6789, property.ComputeValue(manager));
    }
  }
}
