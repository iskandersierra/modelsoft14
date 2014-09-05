using System;
using System.Linq;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework.ObjectManagement;

namespace ModelSoft.Framework.Tests.ObjectManagement
{
  [TestClass]
  public class ObjectManagerTests
  {
    [TestMethod]
    public void ObjectManagerCreateNullClass()
    {
      var hostMoq = new Mock<IObjectManagerHost>();

      var om = new ObjectManager(hostMoq.Object);

      Assert.AreSame(hostMoq.Object, om.Host);
      Assert.IsNull(om.MetaClass);
      Assert.IsNull(om.ParentObject);
      Assert.IsNull(om.ContainerCollection);  
    }

    [TestMethod]
    //[ExpectedException(typeof(ArgumentNullException))]
    public void ObjectManagerCreateNullHost()
    {
      var mycls = new CustomClassDescription("ClassA", "ClassA").AsFrozen();

      var om = new ObjectManager(null, mycls);

      Assert.IsNull(om.Host);
      Assert.AreSame(mycls, om.MetaClass);
      Assert.IsNull(om.ParentObject);
      Assert.IsNull(om.ContainerCollection);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidFreezingOperationException))]
    public void ObjectManagerCreateNotFrozenClass()
    {
      var mycls = new CustomClassDescription("ClassA", "ClassA");

      var om = new ObjectManager(new Mock<IObjectManagerHost>().Object, mycls);
    }

    [TestMethod]
    public void ObjectManagerCreateNoPropertiesOrBaseClasses()
    {
      var hostMoq = new Mock<IObjectManagerHost>();
      var mycls = new CustomClassDescription("ClassA", "ClassA").AsFrozen();

      var om = new ObjectManager(hostMoq.Object, mycls);

      Assert.AreSame(hostMoq.Object, om.Host);
      Assert.AreSame(mycls, om.MetaClass);
      Assert.IsNull(om.ParentObject);
      Assert.IsNull(om.ContainerCollection);
    }

    [TestMethod]
    public void ObjectManagerCreateTwoProperties()
    {
      var hostMoq = new Mock<IObjectManagerHost>();
      var mycls = new CustomClassDescription("ClassA", "ClassA",
        new CustomPropertyDescription("Prop1", "Prop1", CustomCLRTypes.String),
        new CustomPropertyDescription("Prop2", "Prop2", CustomCLRTypes.Int32)
      ).AsFrozen();
      var otherProperty = new CustomPropertyDescription("Prop1", "Prop1", CustomCLRTypes.String).AsFrozen();

      var om = new ObjectManager(hostMoq.Object, mycls);

      Assert.AreSame(hostMoq.Object, om.Host);
      Assert.AreSame(mycls, om.MetaClass);
      Assert.IsNull(om.ParentObject);
      Assert.IsNull(om.ContainerCollection);

      Assert.AreEqual(2, om.Fields.Count());
      Assert.IsTrue(om.HasField(mycls.Properties[0]));
      Assert.IsTrue(om.HasField(mycls.Properties[1]));
      Assert.IsFalse(om.HasField(otherProperty));
      Assert.AreSame(mycls.Properties[0], om.Field(mycls.Properties[0]).Property);
      Assert.AreSame(mycls.Properties[1], om.Field(mycls.Properties[1]).Property);
    }

    [TestMethod]
    public void ObjectManagerCreateSimpleStringProperty()
    {
      var hostMoq = new Mock<IObjectManagerHost>();
      CustomPropertyDescription prop1;
      var mycls = new CustomClassDescription("ClassA", "ClassA",
        prop1 = new CustomPropertyDescription("Prop1", "Prop1", CustomCLRTypes.String)
      ).AsFrozen();

      var om = new ObjectManager(hostMoq.Object, mycls);

      var field = om.Field(prop1);

      Assert.IsFalse(field.IsSet);
      Assert.AreEqual(CustomCLRTypes.String.DefaultValue, field.Value);

      field.Value = "Hello World";

      Assert.IsTrue(field.IsSet);
      Assert.AreEqual("Hello World", field.Value);

      field.Unset();

      Assert.IsFalse(field.IsSet);
      Assert.AreEqual(CustomCLRTypes.String.DefaultValue, field.Value);
    }

    [TestMethod]
    public void ObjectManagerCreateSimpleStringPropertyWithDefault()
    {
      var hostMoq = new Mock<IObjectManagerHost>();
      CustomPropertyDescription prop1;
      var mycls = new CustomClassDescription("ClassA", "ClassA",
        prop1 = new CustomPropertyDescription("Prop1", "Prop1", CustomCLRTypes.String, "Hello")
      ).AsFrozen();

      var om = new ObjectManager(hostMoq.Object, mycls);

      var field = om.Field(prop1);

      Assert.IsFalse(field.IsSet);
      Assert.AreEqual("Hello", field.Value);

      field.Value = "Hello World";

      Assert.IsTrue(field.IsSet);
      Assert.AreEqual("Hello World", field.Value);

      field.Unset();

      Assert.IsFalse(field.IsSet);
      Assert.AreEqual("Hello", field.Value);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ObjectManagerCreateSimpleStringPropertyWrongValue()
    {
      var hostMoq = new Mock<IObjectManagerHost>();
      CustomPropertyDescription prop1;
      var mycls = new CustomClassDescription("ClassA", "ClassA",
        prop1 = new CustomPropertyDescription("Prop1", "Prop1", CustomCLRTypes.String)
      ).AsFrozen();

      var om = new ObjectManager(hostMoq.Object, mycls);

      var field = om.Field(prop1);

      field.Value = 123;
    }

    [TestMethod]
    public void ObjectManagerCreateSimpleInt32Property()
    {
      var hostMoq = new Mock<IObjectManagerHost>();
      CustomPropertyDescription prop1;
      var mycls = new CustomClassDescription("ClassA", "ClassA",
        prop1 = new CustomPropertyDescription("Prop1", "Prop1", CustomCLRTypes.Int32)
      ).AsFrozen();

      var om = new ObjectManager(hostMoq.Object, mycls);

      var field = om.Field(prop1);

      Assert.IsFalse(field.IsSet);
      Assert.AreEqual(CustomCLRTypes.Int32.DefaultValue, field.Value);

      field.Value = 6543;

      Assert.IsTrue(field.IsSet);
      Assert.AreEqual(6543, field.Value);

      field.Unset();

      Assert.IsFalse(field.IsSet);
      Assert.AreEqual(CustomCLRTypes.Int32.DefaultValue, field.Value);
    }
  }
}
