using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ModelSoft.Framework.Tests
{
    
    
    /// <summary>
    ///This is a test class for GeneralExtensionsTest and is intended
    ///to contain all GeneralExtensionsTest Unit Tests
    ///</summary>
  [TestClass()]
  public class GeneralExtensionsTest
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
    public void DoProtectedPositiveTest()
    {
      Exception exception = null;
      bool flag = false;
      Action action = () => { flag = true; /* Succeeding action */ };
      Action<Exception> failAction = ex => { exception = ex; };

      var result = action.DoProtected(failAction);

      Assert.IsTrue(flag, "action done");
      Assert.IsTrue(result, "result is true");
      Assert.IsNull(exception, "no exception thrown");
    }
    [TestMethod()]
    public void DoProtectedNegativeTest()
    {
      Exception exception = null;
      bool flag = false;
      Action action = () => { flag = true; throw new ArgumentException(); /* Failing action */ };
      Action<Exception> failAction = ex => { exception = ex; };

      var result = action.DoProtected(failAction);

      Assert.IsTrue(flag, "action done");
      Assert.IsFalse(result, "result is false");
      Assert.IsInstanceOfType(exception, typeof(ArgumentException), "ApplicationException exception expected");
    }

    [TestMethod()]
    public void TryProtectedPositiveTest()
    {
      int countException = 0;
      int countAction = 0;
      int seqCount = 0;
      var delays = new RetryPattern[] { RetryPattern.SkipDelay, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(250), RetryPattern.Stop, TimeSpan.FromMilliseconds(125) };
      Action action = () => { countAction++; /* Succeeding action */ };
      var seq = delays.Select(d => Tuple.Create<Action, RetryPattern>(() => seqCount++, d)); 
      var retryPatterns = seq.Select(t => { t.Item1(); return t.Item2; });
      Action<IEnumerable<Tuple<RetryPattern, Exception>>> failAction = ex => { countException = ex.Count(); };

      var result = action.TryProtected(retryPatterns, failAction);

      Assert.AreEqual(1, countAction, "action done once");
      Assert.IsTrue(result, "result is true");
      Assert.AreEqual(0, seqCount, "no retry pattern requested");
      Assert.AreEqual(0, countException, "no exception thrown");
    }
    [TestMethod()]
    public void TryProtectedNegativeTest()
    {
      var exceptions = new List<Tuple<RetryPattern, Exception>>();
      DateTime now = DateTime.Now;
      var delays = new RetryPattern[] { RetryPattern.SkipDelay, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(250), RetryPattern.Stop, TimeSpan.FromMilliseconds(125) };
      int countAction = 0;
      int seqCount = 0;
      Action action = () => { countAction++; throw new ArgumentException(); /* Failing action */ };
      var seq = delays.Select(d => Tuple.Create<Action, RetryPattern>(() => seqCount++, d)); 
      var retryPatterns = seq.Select(t => { t.Item1(); return t.Item2; });
      Action<IEnumerable<Tuple<RetryPattern, Exception>>> failAction = p => { exceptions.AddRange(p); };

      var result = action.TryProtected(retryPatterns, failAction);

      Assert.AreEqual(4, countAction, "action done once");
      Assert.IsFalse(result, "result is false");
      Assert.AreEqual(4, seqCount, "4 retry pattern requested");
      Assert.AreEqual(4, exceptions.Count, "no exception thrown");
    }

  }
}
