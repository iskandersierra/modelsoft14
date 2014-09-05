using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework;
using ModelSoft.Framework.ObjectModel;

namespace ModelSoft.Framework.Tests.ObjectModel
{
  [TestClass]
  public class NetTypeClassDescriptionRegistryTests
  {
    [TestMethod]
    public void NetTypeClassDescriptionRegistry_Create()
    {
      var registry = new DefaultNetTypeClassDescriptionRegistryImpl();

      Assert.IsNotNull(registry);
      Assert.IsNotNull(registry.RegisteredClasses);
      Assert.AreEqual(1, registry.RegisteredClasses.Count());
      Assert.IsNotNull(registry.lastCreatedSetup);
      Assert.IsNotNull(registry.defaultClassHierarchyBase);
      Assert.AreEqual(typeof(object), registry.defaultClassHierarchyBase);
      Assert.AreEqual("Object", registry.lastCreatedSetup.Name);
      Assert.IsNotNull(registry.ClassHierarchyBase);

      Assert.AreSame(registry, registry.ClassHierarchyBase.Registry);
      Assert.AreEqual("Object", registry.ClassHierarchyBase.Name);
      Assert.IsNull(registry.ClassHierarchyBase.ResourceName);
      Assert.AreEqual("Object", registry.ClassHierarchyBase.FriendlyName);
      Assert.IsNull(registry.ClassHierarchyBase.BaseClass);

      var objClass = registry.GetClassDescription(CommonTypes.TypeOfObject);
      Assert.AreSame(registry.ClassHierarchyBase, objClass);
      IClassDescription stringClass;
      var found = registry.TryGetClassDescription(CommonTypes.TypeOfString, out stringClass);
      Assert.IsFalse(found);

      Assert.IsFalse(registry.IsFrozen);
      Assert.IsFalse(objClass.IsFrozen);
      registry.Freeze();
      Assert.IsTrue(registry.IsFrozen);
      Assert.IsTrue(objClass.IsFrozen);

      objClass = registry.GetClassDescription(CommonTypes.TypeOfObject);
      Assert.AreSame(registry.ClassHierarchyBase, objClass);
      found = registry.TryGetClassDescription(CommonTypes.TypeOfString, out stringClass);
      Assert.IsFalse(found);

    }

    [TestMethod]
    public void NetTypeClassDescriptionRegistry_Register()
    {
      var registry = new DefaultNetTypeClassDescriptionRegistryImpl();

      var stringClass = registry.RegisterClass(CommonTypes.TypeOfString);
      this.CheckClassInfo(stringClass, "String", null, "String", registry.ClassHierarchyBase);

      var class1 = registry.RegisterClass(typeof(NTCDR_Class1));
      this.CheckClassInfo(class1, "NTCDR_Class1", null, "NTCDR_Class1", registry.ClassHierarchyBase);

      var class11 = registry.RegisterClass(typeof(NTCDR_Class11));
      this.CheckClassInfo(class11, "NTCDR_Class11", null, "NTCDR_Class11", class1);

      Assert.AreEqual(4, registry.RegisteredClasses.Count());
    }

    private void CheckClassInfo(IClassDescription classDesc, string name, string resourceName, string friendlyName, IClassDescription baseClass)
    {
      Assert.IsNotNull(classDesc);
      Assert.AreEqual(name, classDesc.Name);
      Assert.AreEqual(resourceName, classDesc.ResourceName);
      Assert.AreEqual(friendlyName, classDesc.FriendlyName);
      Assert.AreEqual(baseClass, classDesc.BaseClass);
    }
  }

  internal class NTCDR_Class1 : Object
  {

  }

  internal class NTCDR_Class11 : NTCDR_Class1
  {

  }

  internal class DefaultNetTypeClassDescriptionRegistryImpl :
    NetTypeClassDescriptionRegistry
  {
    internal Type defaultClassHierarchyBase;
    internal ClassDescriptionSetup lastCreatedSetup;

    protected override ClassDescriptionSetup CreateSetup(Type type)
    {
      lastCreatedSetup = base.CreateSetup(type);
      return lastCreatedSetup;
    }

    protected override Type GetNetTypeClassHierarchyBase(out ClassDescriptionSetup setup)
    {
      defaultClassHierarchyBase = base.GetNetTypeClassHierarchyBase(out setup);
      return defaultClassHierarchyBase;
    }
  }
}
