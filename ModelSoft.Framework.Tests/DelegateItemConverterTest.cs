using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelSoft.Framework.Tests
{
    /// <summary>
    ///This is a test class for DelegateItemConverterTest and is intended
    ///to contain all DelegateItemConverterTest Unit Tests
    ///</summary>
  [TestClass()]
  public class DelegateItemConverterTest
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
    public void ConstructorTest()
    {
      var converter = new DelegateItemConverter<string, int>(str => int.Parse(str));
    }

    [TestMethod()]
    public void ConstructorTest1()
    {
      var converter = new DelegateItemConverter<string, int>(delegate(string str, out int result) { return int.TryParse(str, out result); });
    }

    [TestMethod()]
    public void ConvertTest_Ok()
    {
      var converter = new DelegateItemConverter<string, int>(delegate(string s, out int i) { return int.TryParse(s, out i); });
      var expected = 12345;
      var result = converter.Convert("12345");
      Assert.AreEqual(expected, result);
    }

    [TestMethod()]
    [ExpectedException(typeof(FormatException))]
    public void ConvertTest_Wrong()
    {
      var converter = new DelegateItemConverter<string, int>(delegate(string s, out int i) { return int.TryParse(s, out i); });
      var result = converter.Convert("abcde");
      Assert.Fail();
    }

    [TestMethod()]
    public void ConvertTest2_Ok()
    {
      var converter = new DelegateItemConverter<string, int>(str => int.Parse(str));
      var expected = 12345;
      var result = converter.Convert("12345");
      Assert.AreEqual(expected, result);
    }

    [TestMethod()]
    [ExpectedException(typeof(FormatException))]
    public void ConvertTest2_Wrong()
    {
      var converter = new DelegateItemConverter<string, int>(str => int.Parse(str));
      var result = converter.Convert("abcde");
      Assert.Fail();
    }

    [TestMethod()]
    public void TryConvertTest_Ok()
    {
      var converter = new DelegateItemConverter<string, int>(delegate(string s, out int i) { return int.TryParse(s, out i); });
      var expected = 12345;
      int result;
      var ok = converter.TryConvert("12345", out result);
      Assert.AreEqual(true, ok);
      Assert.AreEqual(expected, result);
    }

    [TestMethod()]
    public void TryConvertTest_Wrong()
    {
      var converter = new DelegateItemConverter<string, int>(delegate(string s, out int i) { return int.TryParse(s, out i); });
      var expected = default(int);
      int result;
      var ok = converter.TryConvert("abcde", out result);
      Assert.AreEqual(false, ok);
      Assert.AreEqual(expected, result);
    }

    [TestMethod()]
    public void TryConvertTest2_Ok()
    {
      var converter = new DelegateItemConverter<string, int>(str => int.Parse(str));
      var expected = 12345;
      int result;
      var ok = converter.TryConvert("12345", out result);
      Assert.AreEqual(true, ok);
      Assert.AreEqual(expected, result);
    }

    [TestMethod()]
    public void TryConvertTest2_Wrong()
    {
      var converter = new DelegateItemConverter<string, int>(str => int.Parse(str));
      var expected = default(int);
      int result;
      var ok = converter.TryConvert("abcde", out result);
      Assert.AreEqual(false, ok);
      Assert.AreEqual(expected, result);
    }
  }
}
