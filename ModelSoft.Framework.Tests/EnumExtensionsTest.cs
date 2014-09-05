using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelSoft.Framework.Tests
{
    
    
    /// <summary>
    ///This is a test class for EnumExtensionsTest and is intended
    ///to contain all EnumExtensionsTest Unit Tests
    ///</summary>
  [TestClass()]
  public class EnumExtensionsTest
  {

    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
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

    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion


    [TestMethod()]
    public void ParseTest_DayOfWeek_Ok()
    {
      DayOfWeek expectedValue = DayOfWeek.Saturday;
      DayOfWeek result = EnumExtensions.Parse<DayOfWeek>("Saturday");
      Assert.AreEqual(expectedValue, result, "Wrong value parsing enum");
    }

    [TestMethod()]
    [ExpectedException(typeof(ArgumentException))]
    public void ParseTest_DayOfWeek_Wrong()
    {
      DayOfWeek result = EnumExtensions.Parse<DayOfWeek>("wrong value");
      Assert.Fail();
    }

    [TestMethod()]
    public void TryParseTest_DayOfWeek_Ok()
    {
      DayOfWeek expectedValue = DayOfWeek.Saturday;
      DayOfWeek result;
      var ok = EnumExtensions.TryParse<DayOfWeek>("Saturday", out result);
      Assert.AreEqual(true, ok, "Wrong value parsing enum");
      Assert.AreEqual(expectedValue, result, "Wrong value parsing enum");
    }

    [TestMethod()]
    public void TryParseTest_DayOfWeek_Wrong()
    {
      DayOfWeek expectedValue = default(DayOfWeek);
      DayOfWeek result;
      var ok = EnumExtensions.TryParse<DayOfWeek>("wrong value", out result);
      Assert.AreEqual(false, ok, "Wrong value parsing enum");
      Assert.AreEqual(expectedValue, result, "Wrong value parsing enum");
    }
  }
}
