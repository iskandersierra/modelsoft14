using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework;
using ModelSoft.Framework.ObjectManagement;

namespace ModelSoft.Framework.Tests.ObjectManagement
{
  [TestClass]
  public class CustomClassDescriptionTests
  {
    [TestMethod]
    public void CustomClassDescriptionCreate1()
    {
      var mycls = new CustomClassDescription()
      {
        Identifier = "MyClassId",
        Name = "MyClassName",
      };

      mycls.Freeze();

      Assert.AreEqual("MyClassId", mycls.Identifier);
      Assert.AreEqual("MyClassName", mycls.Name);
      Assert.IsNotNull(mycls.BaseClasses);
      Assert.AreEqual(0, mycls.BaseClasses.Count);
      Assert.IsNotNull(mycls.Properties);
      Assert.AreEqual(0, mycls.Properties.Count);
    }

    [TestMethod]
    public void CustomClassDescriptionCreate2()
    {
      var mycls = new CustomClassDescription()
        {
          Identifier = "MyClassId",
          Name = "MyClassName",
        };
      mycls.Properties.Add(new CustomPropertyDescription("MyProp1Id", "MyProp1Name", CustomCLRTypes.String));
      mycls.Properties.Add(new CustomPropertyDescription("MyProp2Id", "MyProp2Name", CustomCLRTypes.Int32));

      mycls.Freeze();

      Assert.AreEqual("MyClassId", mycls.Identifier);
      Assert.AreEqual("MyClassName", mycls.Name);
      Assert.IsNotNull(mycls.BaseClasses);
      Assert.AreEqual(0, mycls.BaseClasses.Count);
      Assert.IsNotNull(mycls.Properties);
      Assert.AreEqual(2, mycls.Properties.Count);
      Assert.AreEqual("MyProp1Name", mycls.Properties[0].Name);
      Assert.AreEqual("MyProp2Name", mycls.Properties[1].Name);
      Assert.IsTrue(mycls.Properties[0].IsFrozen, "mycls.Properties[0].IsFrozen");
      Assert.IsTrue(mycls.Properties[1].IsFrozen, "mycls.Properties[1].IsFrozen");
    }

    [TestMethod]
    public void CustomClassDescriptionCreate3()
    {
      var mycls = new CustomClassDescription("MyClassId", "MyClassName",
        new CustomPropertyDescription("MyProp1Id", "MyProp1Name", CustomCLRTypes.String), 
        new CustomPropertyDescription("MyProp2Id", "MyProp2Name", CustomCLRTypes.Int32)
      );

      mycls.Freeze();

      Assert.AreEqual("MyClassId", mycls.Identifier);
      Assert.AreEqual("MyClassName", mycls.Name);
      Assert.IsNotNull(mycls.BaseClasses);
      Assert.AreEqual(0, mycls.BaseClasses.Count);
      Assert.IsNotNull(mycls.Properties);
      Assert.AreEqual(2, mycls.Properties.Count);
      Assert.AreEqual("MyProp1Name", mycls.Properties[0].Name);
      Assert.AreEqual("MyProp2Name", mycls.Properties[1].Name);
      Assert.IsTrue(mycls.Properties[0].IsFrozen, "mycls.Properties[0].IsFrozen");
      Assert.IsTrue(mycls.Properties[1].IsFrozen, "mycls.Properties[1].IsFrozen");
      Assert.AreEqual(mycls, mycls.Properties[0].DeclaringClass);
      Assert.AreEqual(mycls, mycls.Properties[1].DeclaringClass);
    }

    [TestMethod]
    public void CustomClassDescriptionCreate4()
    {
      var mycls = new CustomClassDescription("MyClassId", "MyClassName",
        new CustomPropertyDescription("MyProp1Id", "MyProp1Name", CustomCLRTypes.String),
        new CustomPropertyDescription("MyProp2Id", "MyProp2Name", CustomCLRTypes.Int32)
      );

      var mycls2 = new CustomClassDescription("MyClass2Id", "MyClass2Name",
        new CustomClassDescription[] { mycls },
        new CustomPropertyDescription("MyProp3Id", "MyProp3Name", CustomCLRTypes.Boolean),
        new CustomPropertyDescription("MyProp4Id", "MyProp4Name", CustomCLRTypes.Double),
        new CustomPropertyDescription("MyProp5Id", "MyProp5Name", CustomCLRTypes.Single)
      );

      mycls2.Freeze();

      Assert.IsTrue(mycls.IsFrozen);
      Assert.AreEqual("MyClassId", mycls.Identifier);
      Assert.AreEqual("MyClassName", mycls.Name);
      Assert.IsNotNull(mycls.BaseClasses);
      Assert.AreEqual(0, mycls.BaseClasses.Count);
      Assert.IsNotNull(mycls.Properties);
      Assert.AreEqual(2, mycls.Properties.Count);
      Assert.AreEqual("MyProp1Name", mycls.Properties[0].Name);
      Assert.AreEqual("MyProp2Name", mycls.Properties[1].Name);
      Assert.IsTrue(mycls.Properties[0].IsFrozen, "mycls.Properties[0].IsFrozen");
      Assert.IsTrue(mycls.Properties[1].IsFrozen, "mycls.Properties[1].IsFrozen");

      Assert.IsTrue(mycls2.IsFrozen);
      Assert.AreEqual("MyClass2Id", mycls2.Identifier);
      Assert.AreEqual("MyClass2Name", mycls2.Name);
      Assert.IsNotNull(mycls2.BaseClasses);
      Assert.AreEqual(1, mycls2.BaseClasses.Count);
      Assert.AreEqual(mycls, mycls2.BaseClasses[0]);
      Assert.IsNotNull(mycls2.Properties);
      Assert.AreEqual(3, mycls2.Properties.Count);
      Assert.AreEqual("MyProp3Name", mycls2.Properties[0].Name);
      Assert.AreEqual("MyProp4Name", mycls2.Properties[1].Name);
      Assert.AreEqual("MyProp5Name", mycls2.Properties[2].Name);
      Assert.IsTrue(mycls2.Properties[0].IsFrozen, "mycls2.Properties[0].IsFrozen");
      Assert.IsTrue(mycls2.Properties[1].IsFrozen, "mycls2.Properties[1].IsFrozen");
      Assert.IsTrue(mycls2.Properties[2].IsFrozen, "mycls2.Properties[1].IsFrozen");
      var expectedAllClasses = Seq.Build(mycls, mycls2);
      Assert.IsTrue(mycls2.GetAllClasses().SameSequenceAs(expectedAllClasses), "All base classes should give {{{0}}} but gave {{{1}}}", expectedAllClasses.ToStringList(), mycls2.GetAllClasses().ToStringList());
    }

    [TestMethod]
    public void CustomClassDescriptionCreate5()
    {
      var myclsA = new CustomClassDescription("A", "A");
      var myclsB = new CustomClassDescription("B", "B");
      var myclsC = new CustomClassDescription("C", "C");
      var myclsD = new CustomClassDescription("D", "D");
      var myclsE = new CustomClassDescription("E", "E");
      var myclsF = new CustomClassDescription("F", "F");
      var myclsG = new CustomClassDescription("G", "G");
      myclsD.BaseClasses.AddRange(myclsA, myclsB);
      myclsE.BaseClasses.AddRange(myclsB, myclsC);
      myclsF.BaseClasses.AddRange(myclsA);
      myclsG.BaseClasses.AddRange(myclsD, myclsE, myclsF, myclsC);

      myclsG.Freeze();

      Assert.IsTrue(myclsG.GetAllClasses().All(e => e.IsFrozen), "All classes must be frozen");

      var expectedAllClasses = Seq.Build(myclsA, myclsB, myclsD, myclsC, myclsE, myclsF, myclsG);
      Assert.IsTrue(myclsG.GetAllClasses().SameSequenceAs(expectedAllClasses), "All base classes should give {{{0}}} but gave {{{1}}}", expectedAllClasses.ToStringList(), myclsG.GetAllClasses().ToStringList());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidFreezingOperationException))]
    public void CustomClassDescriptionCreateDuplicatedPropertyId()
    {
      var mycls = new CustomClassDescription()
      {
        Identifier = "MyClassId",
        Name = "MyClassName",
      };
      mycls.Properties.Add(new CustomPropertyDescription("MyProp1Id", "MyProp1Name", CustomCLRTypes.String));
      mycls.Properties.Add(new CustomPropertyDescription("MyProp1Id", "MyProp2Name", CustomCLRTypes.Int32));

      mycls.Freeze();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidFreezingOperationException))]
    public void CustomClassDescriptionCreateDuplicatedPropertyName()
    {
      var mycls = new CustomClassDescription()
      {
        Identifier = "MyClassId",
        Name = "MyClassName",
      };
      mycls.Properties.Add(new CustomPropertyDescription("MyProp1Id", "MyProp1Name", CustomCLRTypes.String));
      mycls.Properties.Add(new CustomPropertyDescription("MyProp2Id", "MyProp1Name", CustomCLRTypes.Int32));

      mycls.Freeze();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidFreezingOperationException))]
    public void CustomClassDescriptionCreateDuplicatedBaseClass()
    {
      var mycls = new CustomClassDescription()
      {
        Identifier = "MyClassId",
        Name = "MyClassName",
      };
      var mycls2 = new CustomClassDescription()
      {
        Identifier = "MyClass2Id",
        Name = "MyClass2Name",
      };
      mycls2.BaseClasses.Add(mycls);
      mycls2.BaseClasses.Add(mycls);

      mycls2.Freeze();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidFreezingOperationException))]
    public void CustomClassDescriptionCreateSelfBaseClass()
    {
      var mycls = new CustomClassDescription()
      {
        Identifier = "MyClassId",
        Name = "MyClassName",
      };
      mycls.Properties.Add(new CustomPropertyDescription("MyProp1Id", "MyProp1Name", CustomCLRTypes.String));
      mycls.Properties.Add(new CustomPropertyDescription("MyProp2Id", "MyProp2Name", CustomCLRTypes.Int32));

      mycls.BaseClasses.Add(mycls);

      mycls.Freeze();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CustomClassDescriptionCreateNullBaseClass()
    {
      var mycls = new CustomClassDescription()
      {
        Identifier = "MyClassId",
        Name = "MyClassName",
      };
      mycls.Properties.Add(new CustomPropertyDescription("MyProp1Id", "MyProp1Name", CustomCLRTypes.String));
      mycls.Properties.Add(new CustomPropertyDescription("MyProp2Id", "MyProp2Name", CustomCLRTypes.Int32));

      mycls.BaseClasses.Add(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CustomClassDescriptionCreateNullProperty()
    {
      var mycls = new CustomClassDescription()
      {
        Identifier = "MyClassId",
        Name = "MyClassName",
      };
      mycls.Properties.Add(new CustomPropertyDescription("MyProp1Id", "MyProp1Name", CustomCLRTypes.String));
      mycls.Properties.Add(null);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidFreezingOperationException))]
    public void CustomClassDescriptionCreateBaseClassCycle()
    {
      var mycls = new CustomClassDescription(
        "MyClassId", "MyClassName");

      var mycls2 = new CustomClassDescription(
        "MyClass2Id", "MyClass2Name",
        new CustomClassDescription[] { mycls });
      mycls.BaseClasses.Add(mycls2);

      mycls2.Freeze();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidFreezingOperationException))]
    public void CustomClassDescriptionCreateBaseClassCycle2()
    {
      var mycls = new CustomClassDescription(
        "MyClassId", "MyClassName");

      var mycls2 = new CustomClassDescription(
        "MyClass2Id", "MyClass2Name",
        new CustomClassDescription[] { mycls });
      mycls.BaseClasses.Add(mycls2);

      var mycls3 = new CustomClassDescription(
        "MyClass3Id", "MyClass3Name",
        new CustomClassDescription[] { mycls2 });

      mycls3.Freeze();
    }
  }
}
