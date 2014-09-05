using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelSoft.Framework.Tests
{
  [TestClass()]
  public class SetAndRestoreTests
  {
    private TestContext testContextInstance;

    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    [TestMethod()]
    public void SetAndRestoreTests_SimpleTest()
    {
      var variable = 123;
      using (variable.SetAndRestore(456, v => variable = v))
      {
        Assert.AreEqual(variable, 456);
      }
      Assert.AreEqual(variable, 123);
    }
  }
}
