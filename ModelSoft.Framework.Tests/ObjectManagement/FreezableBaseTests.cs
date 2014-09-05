using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelSoft.Framework.ObjectManagement.Tests
{
  [TestClass]
  public class FreezableBaseTests
  {
    [TestMethod]
    public void FreezableBaseTests_InitNotFrozen()
    {
      var obj = new FreezableImpl();

      Assert.IsFalse(obj.IsFrozen);
      Assert.AreEqual(0, obj.OnFreezeCallCount, "OnFreezeCallCount == 0");
      obj.CallCheckModifyFrozen();

      obj.IsFrozen = false;

      Assert.IsFalse(obj.IsFrozen);
      Assert.AreEqual(0, obj.OnFreezeCallCount, "OnFreezeCallCount == 0");
      obj.CallCheckModifyFrozen();
    }

    [TestMethod]
    public void FreezableBaseTests_Freeze()
    {
      var obj = new FreezableImpl();

      obj.Freeze();

      Assert.IsTrue(obj.IsFrozen);
      Assert.AreEqual(1, obj.OnFreezeCallCount, "OnFreezeCallCount == 1");

      obj.Freeze();

      Assert.IsTrue(obj.IsFrozen);
      Assert.AreEqual(1, obj.OnFreezeCallCount, "OnFreezeCallCount == 1");
    }

    [TestMethod]
    public void FreezableBaseTests_SetIsFrozen()
    {
      var obj = new FreezableImpl();

      obj.IsFrozen = true;

      Assert.IsTrue(obj.IsFrozen);
      Assert.AreEqual(1, obj.OnFreezeCallCount, "OnFreezeCallCount == 1");

      obj.IsFrozen = true;

      Assert.IsTrue(obj.IsFrozen);
      Assert.AreEqual(1, obj.OnFreezeCallCount, "OnFreezeCallCount == 1");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidFreezingOperationException))]
    public void FreezableBaseTests_SetIsNotFrozen()
    {
      var obj = new FreezableImpl();

      obj.IsFrozen = true;
      obj.IsFrozen = false;
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidFreezingOperationException))]
    public void FreezableBaseTests_FrozenCheckFail()
    {
      var obj = new FreezableImpl();

      obj.IsFrozen = true;

      Assert.IsTrue(obj.IsFrozen);
      Assert.AreEqual(1, obj.OnFreezeCallCount, "OnFreezeCallCount == 1");
      obj.CallCheckModifyFrozen();
    }

    class FreezableImpl : FreezableBase
    {
      public int OnFreezeCallCount = 0;

      public void CallCheckModifyFrozen(){
        base.CheckModifyFrozen();
      }

      protected override void OnFreeze()
      {
        OnFreezeCallCount++;
        base.OnFreeze();
      }
    }
  }
}
