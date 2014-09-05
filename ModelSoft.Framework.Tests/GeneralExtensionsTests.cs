using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework;

namespace ModelSoft.Framework.Tests
{
  [TestClass]
  public class GeneralExtensionsTests
  {
    [TestMethod]
    public void GeneralExtensionsTests_Delegate_IsSerializable()
    {
      var objSer = new GeneralExtensionsTests_SerializableClass();
      var objNonSer = new GeneralExtensionsTests_NonSerializableClass();

      EventHandler serEH = objSer.PropertyChanged;
      EventHandler nonSerEH = objNonSer.PropertyChanged;
      EventHandler staticEH = objSer.PropertyChanged;

      Assert.IsTrue(serEH.IsSerializable(), "serEH.IsSerializable()");
      Assert.IsFalse(nonSerEH.IsSerializable(), "nonSerEH.IsSerializable()");
      Assert.IsTrue(staticEH.IsSerializable(), "staticEH.IsSerializable()");
    }
  }

  [Serializable]
  public class GeneralExtensionsTests_SerializableClass
  {
    public static int CallCounter = 0;

    public GeneralExtensionsTests_SerializableClass()
    {
    }

    public void PropertyChanged(object sender, EventArgs args)
    {
      CallCounter++;
    }
  }

  public class GeneralExtensionsTests_NonSerializableClass
  {
    public static int CallCounter = 0;
    public static int StaticCallCounter = 0;

    public GeneralExtensionsTests_NonSerializableClass()
    {
    }

    public void PropertyChanged(object sender, EventArgs args)
    {
      CallCounter++;
    }

    public static void StaticPropertyChanged(object sender, EventArgs args)
    {
      StaticCallCounter++;
    }
  }

}
