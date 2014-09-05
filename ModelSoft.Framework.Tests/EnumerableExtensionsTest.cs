using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelSoft.Framework.Tests
{
    
    
    /// <summary>
    ///This is a test class for EnumerableExtensionsTest and is intended
    ///to contain all EnumerableExtensionsTest Unit Tests
    ///</summary>
  [TestClass()]
  public class EnumerableExtensionsTest
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

    #region [ Singleton ]
    [TestMethod()]
    public void EnumerableExtensionsTestSingletonItem()
    {
      var expected = new[] { "item" };
      var result = "item".Singleton();
      Assert.IsTrue(result.SameSequenceAs(expected));
    }

    [TestMethod()]
    public void EnumerableExtensionsTestSingletonNull()
    {
      var expected = new string[] { null };
      string item = null;
      var result = item.Singleton();
      Assert.IsTrue(result.SameSequenceAs(expected));
    }

    [TestMethod()]
    public void EnumerableExtensionsTestSingletonNonNullItem()
    {
      var expected = new[] { "item" };
      var result = "item".SingletonNonNull();
      Assert.IsTrue(result.SameSequenceAs(expected));
    }

    [TestMethod()]
    public void EnumerableExtensionsTestSingletonNonNullNull()
    {
      var expected = new string[] { };
      string item = null;
      var result = item.SingletonNonNull();
      Assert.IsTrue(result.SameSequenceAs(expected));
    }
    #endregion [ Singleton ]

    #region [ Distinct ]
    [TestMethod]
    public void EnumerableExtensionsTestHasDuplicatesStringNull()
    {
      string[] arr = null;
      Assert.IsFalse(arr.HasDuplicates());
    }

    [TestMethod]
    public void EnumerableExtensionsTestHasDuplicatesStringNoSelectorNoDuplicates()
    {
      var arr = new string[] { "take", "no", "duplicates", "please", "TAKE" };
      Assert.IsFalse(arr.HasDuplicates());
      Assert.IsTrue(arr.HasDuplicates(StringComparer.InvariantCultureIgnoreCase));
    }

    [TestMethod]
    public void EnumerableExtensionsTestHasDuplicatesStringSelectorNoDuplicates()
    {
      var arr = new string[] { "take", "no", "duplicates", "please", "TAKE" };
      Assert.IsFalse(arr.HasDuplicates(s => s[0]));
    }

    [TestMethod]
    public void EnumerableExtensionsTestHasDuplicatesStringNoSelectorDuplicates()
    {
      var arr = new string[] { "take", "no", "duplicates", "no", "please", "TAKE", "please" };
      Assert.IsTrue(arr.HasDuplicates());
      Assert.IsTrue(arr.HasDuplicates(StringComparer.InvariantCultureIgnoreCase));
    }

    [TestMethod]
    public void EnumerableExtensionsTestHasDuplicatesStringSelectorDuplicates()
    {
      var arr = new string[] { "take", "no", "duplicates", "no", "please", "TAKE", "please" };
      Assert.IsTrue(arr.HasDuplicates(s => s[0]));
    }

    [TestMethod]
    public void EnumerableExtensionsTestGetDuplicatesStringNull()
    {
      string[] arr = null;
      Assert.IsTrue(arr.GetDuplicates().SameSequenceAs(Enumerable.Empty<string>()));
    }

    [TestMethod]
    public void EnumerableExtensionsTestGetDuplicatesStringNoSelectorNoDuplicates()
    {
      var arr = new string[] { "take", "no", "duplicates", "please", "TAKE" };
      Assert.IsTrue(arr.GetDuplicates().SameSequenceAs(Enumerable.Empty<string>()));
      Assert.IsTrue(arr.GetDuplicates(StringComparer.InvariantCultureIgnoreCase).SameSequenceAs(new string[] { "TAKE" }));
    }

    [TestMethod]
    public void EnumerableExtensionsTestGetDuplicatesStringSelectorNoDuplicates()
    {
      var arr = new string[] { "take", "no", "duplicates", "please", "TAKE" };
      Assert.IsTrue(arr.GetDuplicates(s => s[0]).SameSequenceAs(new string[0]));
    }

    [TestMethod]
    public void EnumerableExtensionsTestGetDuplicatesStringNoSelectorDuplicates()
    {
      var arr = new string[] { "take", "no", "duplicates", "no", "please", "TAKE", "please" };
      Assert.IsTrue(arr.GetDuplicates().SameSequenceAs(new string[] { "no", "please" }));
      Assert.IsTrue(arr.GetDuplicates(StringComparer.InvariantCultureIgnoreCase).SameSequenceAs(new string[] { "no", "TAKE", "please" }));
    }

    [TestMethod]
    public void EnumerableExtensionsTestGetDuplicatesStringSelectorDuplicates()
    {
      var arr = new string[] { "take", "no", "duplicates", "no", "please", "TAKE", "please" };
      Assert.IsTrue(arr.GetDuplicates(s => s[0]).SameSequenceAs(new string[] { "no", "please" }));
    }
    #endregion [ Distinct ]

    #region [ Graph Searching ]

    #region [ FindShortestPath ]

    [TestMethod]
    public void EnumerableExtensionsTestFindShortestPath_Test()
    {
      var AdyMatrix = new string[,]
      {
        { null, "0-1",  null,  null,  null,  null },
        { null,  null, "1-2", "1-3",  null,  null },
        { null, "2-1",  null,  null,  null,  null },
        { null,  null, "3-2",  null, "3-4",  null },
        { null,  null, "4-2",  null,  null, "4-5" },
        { null,  null, "5-2",  null,  null,  null },
      };

      var emptyPath = Enumerable.Empty<Tup<int, string, int>>();
      var paths = new IEnumerable<Tup<int, string, int>>[,]
      {
        // From 0
        { emptyPath, FSPPath(0,1), FSPPath(0,1,2), FSPPath(0,1,3), FSPPath(0,1,3,4), FSPPath(0,1,3,4,5), },
        // From 1
        { null, emptyPath, FSPPath(1,2), FSPPath(1,3), FSPPath(1,3,4), FSPPath(1,3,4,5), },
        // From 2
        { null, FSPPath(2,1), emptyPath, FSPPath(2,1,3), FSPPath(2,1,3,4), FSPPath(2,1,3,4,5), },
        // From 3
        { null, FSPPath(3,2,1), FSPPath(3,2), emptyPath, FSPPath(3,4), FSPPath(3,4,5), },
        // From 4
        { null, FSPPath(4,2,1), FSPPath(4,2), FSPPath(4,2,1,3), emptyPath, FSPPath(4,5), },
        // From 5
        { null, FSPPath(5,2,1), FSPPath(5,2), FSPPath(5,2,1,3), FSPPath(5,2,1,3,4), emptyPath, },
      };

      Func<int, IEnumerable<Tup<int, string, int>>> getArcs = n =>
        {
          return 0
            .ToCount(AdyMatrix.GetLength(1))
            .Where(m => AdyMatrix[n, m] != null)
            .Select(m => Tup.Create(n, AdyMatrix[n, m], m));
        };

      for (int from = 0; from < AdyMatrix.GetLength(0); from++)
      {
        for (int to = 0; to < AdyMatrix.GetLength(1); to++)
        {
          var fromNode = from;
          var toNode = to;
          Predicate<int> foundCondition = n => n == toNode;
          var foundPath = fromNode.FindShortestPath(getArcs, foundCondition);
          Assert.IsTrue(
            paths[fromNode, toNode] == null && foundPath == null ||
            paths[fromNode, toNode] != null && foundPath != null && paths[fromNode, toNode].SameSequenceAs(foundPath.Select(s => Tup.Create(s.From, s.Arc, s.To))), 
            string.Format(
              "Path from {0} to {1} differ: {2} != {3}", 
              fromNode, 
              toNode, 
              paths[fromNode, toNode].ToStringList(" ==> ", t => t.Item3.ToString(), opening: fromNode.ToString(), empty: fromNode.ToString()),
              foundPath.ToStringList(" ==> ", t => t.To.ToString(), opening: fromNode.ToString(), empty: fromNode.ToString())
            )
          );
        }
      }
    }

    private static IEnumerable<Tup<int, string, int>> FSPPath(params int[] nodes)
    {
      for (int i = 0; i < nodes.Length - 1; i++)
      {
        yield return Tup.Create(nodes[i], string.Format("{0}-{1}", nodes[i], nodes[i + 1]), nodes[i + 1]);
      }
    }

    #endregion

    #endregion

    #region [ RemoveWhere ]

    [TestMethod]
    public void EnumerableExtensionsTestRemoveWhereEmptyList()
    {
      var list = new List<string>();
      var removed = list.RemoveWhere(s => true);
      Assert.IsNotNull(removed);
      Assert.IsTrue(list.IsEmpty(), "List should be empty");
      Assert.IsTrue(removed.IsEmpty(), "OnRemoved should be empty");
    }

    [TestMethod]
    public void EnumerableExtensionsTestRemoveWhereEmptyArray()
    {
      var list = new string[0];
      var removed = list.RemoveWhere(s => true);
      Assert.IsNotNull(removed);
      Assert.IsTrue(list.IsEmpty(), "List should be empty");
      Assert.IsTrue(removed.IsEmpty(), "OnRemoved should be empty");
    }

    [TestMethod]
    public void EnumerableExtensionsTestRemoveWhereAllList()
    {
      var list = new List<string>{"a", "b", "c"};
      var removed = list.RemoveWhere(s => true);
      Assert.IsNotNull(removed);
      Assert.IsTrue(list.IsEmpty(), "List should be empty");
      Assert.IsTrue(removed.SameSequenceAs(Seq.Build("a", "b", "c")), "OnRemoved should be empty");
    }

    [TestMethod]
    public void EnumerableExtensionsTestRemoveWhereAllArray()
    {
      var list = new string[] { "a", "b", "c" };
      var removed = list.RemoveWhere(s => true);
      Assert.IsNotNull(removed);
      Assert.IsTrue(list.SameSequenceAs(new string[3]), "List should be empty");
      Assert.IsTrue(removed.SameSequenceAs(Seq.Build("a", "b", "c")), "OnRemoved should be empty");
    }

    [TestMethod]
    public void EnumerableExtensionsTestRemoveWhereSomeList()
    {
      var list = new List<string> { "a", "aaa", "aab", "b", "c", "ccc", "ddd", "d" };
      var removed = list.RemoveWhere(s => s.Length == 1);
      Assert.IsNotNull(removed);
      Assert.IsTrue(list.SameSequenceAs(new string[] { "aaa", "aab", "ccc", "ddd" }), "List got: " + list.ToStringList());
      Assert.IsTrue(removed.SameSequenceAs(new string[] { "a", "b", "c", "d" }), "OnRemoved got: " + removed.ToStringList());
    }

    [TestMethod]
    public void EnumerableExtensionsTestRemoveWhereSomeArray()
    {
      var list = new string[] { "a", "aaa", "aab", "b", "c", "ccc", "ddd", "d" };
      var removed = list.RemoveWhere(s => s.Length == 1);
      Assert.IsNotNull(removed);
      Assert.IsTrue(list.SameSequenceAs(new string[] { "aaa", "aab", "ccc", "ddd", null, null, null, null }), "List got: " + list.ToStringList());
      Assert.IsTrue(removed.SameSequenceAs(new string[] { "a", "b", "c", "d" }), "OnRemoved got: " + removed.ToStringList());
    }

    [TestMethod]
    public void EnumerableExtensionsTestRemoveWhereNoneList()
    {
      var list = new List<string> { "a", "aaa", "aab", "b", "c", "ccc", "ddd", "d" };
      var removed = list.RemoveWhere(s => s.Length == 1, 0);
      Assert.IsNotNull(removed);
      Assert.IsTrue(list.SameSequenceAs(new string[] { "a", "aaa", "aab", "b", "c", "ccc", "ddd", "d" }), "List got: " + list.ToStringList());
      Assert.IsTrue(removed.IsEmpty(), "OnRemoved got: " + removed.ToStringList());
    }

    [TestMethod]
    public void EnumerableExtensionsTestRemoveWhereNoneArray()
    {
      var list = new string[] { "a", "aaa", "aab", "b", "c", "ccc", "ddd", "d" };
      var removed = list.RemoveWhere(s => s.Length == 1, 0);
      Assert.IsNotNull(removed);
      Assert.IsTrue(list.SameSequenceAs(new string[] { "a", "aaa", "aab", "b", "c", "ccc", "ddd", "d" }), "List got: " + list.ToStringList());
      Assert.IsTrue(removed.IsEmpty(), "OnRemoved got: " + removed.ToStringList());
    }

    [TestMethod]
    public void EnumerableExtensionsTestRemoveWhere2List()
    {
      var list = new List<string> { "a", "aaa", "aab", "b", "c", "ccc", "ddd", "d" };
      var removed = list.RemoveWhere(s => s.Length == 1, 2);
      Assert.IsNotNull(removed);
      Assert.IsTrue(list.SameSequenceAs(new string[] { "aaa", "aab", "c", "ccc", "ddd", "d" }), "List got: " + list.ToStringList());
      Assert.IsTrue(removed.SameSequenceAs(new string[] { "a", "b" }), "OnRemoved got: " + removed.ToStringList());
    }

    [TestMethod]
    public void EnumerableExtensionsTestRemoveWhere2Array()
    {
      var list = new string[] { "a", "aaa", "aab", "b", "c", "ccc", "ddd", "d" };
      var removed = list.RemoveWhere(s => s.Length == 1, 2);
      Assert.IsNotNull(removed);
      Assert.IsTrue(list.SameSequenceAs(new string[] { "aaa", "aab", "c", "ccc", "ddd", "d", null, null }), "List got: " + list.ToStringList());
      Assert.IsTrue(removed.SameSequenceAs(new string[] { "a", "b" }), "OnRemoved got: " + removed.ToStringList());
    }

    [TestMethod]
    public void EnumerableExtensionsTestRemoveWhere2Collection()
    {
      var list = new LinkedList<string>(new string[] { "a", "aaa", "aab", "b", "c", "ccc", "ddd", "d" });
      var removed = list.RemoveWhere(s => s.Length == 1, 2);
      Assert.IsNotNull(removed);
      Assert.IsTrue(list.SameSequenceAs(new string[] { "aaa", "aab", "c", "ccc", "ddd", "d" }), "List got: " + list.ToStringList());
      Assert.IsTrue(removed.SameSequenceAs(new string[] { "a", "b" }), "OnRemoved got: " + removed.ToStringList());
    }

    #endregion

  }
}
