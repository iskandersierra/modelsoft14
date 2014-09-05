using System;
using ModelSoft.Framework;
using ModelSoft.Framework.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelSoft.Framework.Collections.Tests
{
  [TestClass]
  public class TrieNodeTests
  {
    [TestMethod]
    public void TrieNodeTests_Constructor()
    {
      var node = new TrieNode<char, string>();
      Assert.IsNotNull(node);
      Assert.IsNull(node.Value);
      Assert.IsFalse(node.IsActive);
      Assert.AreEqual(0, node.ActiveCount);
      Assert.IsNull(node.Derived);
    }

    [TestMethod]
    public void TrieNodeTests_GetActiveCount()
    {
      TrieNode<char, string> node = null;

      Assert.AreEqual(0, node.GetActiveCount());

      node = new TrieNode<char, string>();

      Assert.AreEqual(0, node.GetActiveCount());

      node.ActiveCount = 1234;

      Assert.AreEqual(1234, node.GetActiveCount());
    }

    [TestMethod]
    public void TrieNodeTests_GetIsActive()
    {
      TrieNode<char, string> node = null;

      Assert.AreEqual(false, node.GetIsActive());

      node = new TrieNode<char, string>();

      Assert.AreEqual(false, node.GetIsActive());

      node.IsActive = true;

      Assert.AreEqual(true, node.GetIsActive());
    }

    [TestMethod]
    public void TrieNodeTests_GetSetDerived()
    {
      char symbol = 'G';
      TrieNode<char, string> node = null;
      TrieNode<char, string> childNode = new TrieNode<char, string>();

      Assert.IsNull(node.GetDerived(symbol, null));

      node = new TrieNode<char, string>();

      Assert.IsNull(node.GetDerived(symbol, null));

      node.SetDerived(symbol, childNode, null);

      Assert.AreSame(childNode, node.GetDerived(symbol, null));
    }

    [TestMethod]
    public void TrieNodeTests_SetEmptyKey()
    {
      TrieNode<char, string> node = null;
      var key = "";
      var value = "a value";

      node = node.Set(key.GetEnumerator(), value, null, true);

      Assert.IsNotNull(node);
      Assert.AreEqual(value, node.Value);
      Assert.IsTrue(node.IsActive);
      Assert.AreEqual(1, node.ActiveCount);
      Assert.IsNull(node.Derived);
    }

    [TestMethod]
    public void TrieNodeTests_AddEmptyKey()
    {
      TrieNode<char, string> node = null;
      var key = "";
      var value = "a value";

      node = node.Set(key.GetEnumerator(), value, null, false);

      Assert.IsNotNull(node);
      Assert.AreEqual(value, node.Value);
      Assert.IsTrue(node.IsActive);
      Assert.AreEqual(1, node.ActiveCount);
      Assert.IsNull(node.Derived);
    }

    [TestMethod]
    public void TrieNodeTests_SetDuplicateEmptyKey()
    {
      TrieNode<char, string> node = null;
      var key = "";
      var value = "a value";

      node = node.Set(key.GetEnumerator(), value, null, true);
      var node2 = node.Set(key.GetEnumerator(), value, null, true);

      Assert.IsNotNull(node);
      Assert.AreSame(node, node2);
      Assert.AreEqual(value, node.Value);
      Assert.IsTrue(node.IsActive);
      Assert.AreEqual(1, node.ActiveCount);
      Assert.IsNull(node.Derived);
    }

    [TestMethod]
    [ExpectedException(typeof(DuplicateKeyNotAllowedException))]
    public void TrieNodeTests_AddDuplicateEmptyKey()
    {
      TrieNode<char, string> node = null;
      var key = "";
      var value = "a value";

      node = node.Set(key.GetEnumerator(), value, null, false);
      var node2 = node.Set(key.GetEnumerator(), value, null, false);

    }

    [TestMethod]
    public void TrieNodeTests_SetKey()
    {
      TrieNode<char, string> node = null;
      var h = "h";
      var hi = "hi";
      var him = "him";
      var her = "her";

      node = node.Set(h.GetEnumerator(), h, null, true);
      AssertNodeState(node, null, false, 1);
      var hNode = node.GetDerived('h', null);
      AssertNodeState(hNode, h, true, 1);

      node = node.Set(h.GetEnumerator(), h, null, true);
      AssertNodeState(node, null, false, 1);
      hNode = node.GetDerived('h', null);
      AssertNodeState(hNode, h, true, 1);

      node = node.Set(hi.GetEnumerator(), hi, null, true);
      node = node.Set(him.GetEnumerator(), him, null, true);
      node = node.Set(her.GetEnumerator(), her, null, true);
      AssertNodeState(node, null, false, 4);
      hNode = node.GetDerived('h', null);
      var hiNode = hNode.GetDerived('i', null);
      var himNode = hiNode.GetDerived('m', null);
      var heNode = hNode.GetDerived('e', null);
      var herNode = heNode.GetDerived('r', null);
      AssertNodeState(hNode, h, true, 4);
      AssertNodeState(hiNode, hi, true, 2);
      AssertNodeState(himNode, him, true, 1);
      AssertNodeState(heNode, null, false, 1);
      AssertNodeState(herNode, her, true, 1);
    }

    [TestMethod]
    public void TrieNodeTests_RemoveKey()
    {
      TrieNode<char, string> node = null;
      var h = "h";
      var hi = "hi";
      var him = "him";
      var her = "her";

      node = node.Set(h.GetEnumerator(), h, null, true);
      node = node.Set(hi.GetEnumerator(), hi, null, true);
      node = node.Set(him.GetEnumerator(), him, null, true);
      node = node.Set(her.GetEnumerator(), her, null, true);

      node = node.Remove(her.GetEnumerator(), null);

      var hNode = node.GetDerived('h', null);
      var hiNode = hNode.GetDerived('i', null);
      var himNode = hiNode.GetDerived('m', null);
      var heNode = hNode.GetDerived('e', null);
      var herNode = heNode.GetDerived('r', null);

      AssertNodeState(node, null, false, 3);
      AssertNodeState(hNode, h, true, 3);
      AssertNodeState(hiNode, hi, true, 2);
      AssertNodeState(himNode, him, true, 1);
      Assert.IsNull(heNode);
      Assert.IsNull(herNode);

      node = node.Remove(hi.GetEnumerator(), null);

      hNode = node.GetDerived('h', null);
      hiNode = hNode.GetDerived('i', null);
      himNode = hiNode.GetDerived('m', null);
      heNode = hNode.GetDerived('e', null);
      herNode = heNode.GetDerived('r', null);

      AssertNodeState(node, null, false, 2);
      AssertNodeState(hNode, h, true, 2);
      AssertNodeState(hiNode, null, false, 1);
      AssertNodeState(himNode, him, true, 1);
      Assert.IsNull(heNode);
      Assert.IsNull(herNode);
    }

    [TestMethod]
    public void TrieNodeTests_Find()
    {
      TrieNode<char, string> node = null;
      var h = "h";
      var hi = "hi";
      var him = "him";
      var her = "her";

      node = node.Set(h.GetEnumerator(), h, null, true);
      node = node.Set(hi.GetEnumerator(), hi, null, true);
      node = node.Set(him.GetEnumerator(), him, null, true);
      node = node.Set(her.GetEnumerator(), her, null, true);
      var hNode = node.GetDerived('h', null);
      var hiNode = hNode.GetDerived('i', null);
      var himNode = hiNode.GetDerived('m', null);
      var heNode = hNode.GetDerived('e', null);
      var herNode = heNode.GetDerived('r', null);

      var emptyFind = node.Find("".GetEnumerator(), null);
      Assert.AreSame(node, emptyFind);

      var hFind = node.Find(h.GetEnumerator(), null);
      Assert.AreSame(hNode, hFind);

      var hiFind = node.Find(hi.GetEnumerator(), null);
      Assert.AreSame(hiNode, hiFind);

      var himFind = node.Find(him.GetEnumerator(), null);
      Assert.AreSame(himNode, himFind);

      var herFind = node.Find(her.GetEnumerator(), null);
      Assert.AreSame(herNode, herFind);

      var heroFind = node.Find("hero".GetEnumerator(), null);
      Assert.IsNull(heroFind);

      var niceFind = node.Find("nice".GetEnumerator(), null);
      Assert.IsNull(niceFind);
    }

    [TestMethod]
    public void TrieNodeTests_Track()
    {
      TrieNode<char, string> node = null;
      var h = "h";
      var hi = "hi";
      var him = "him";
      var her = "her";

      var emptySeq = new TrieNode<char, string>[0];
      bool found;

      var nullTrack = node.Track("some".GetEnumerator(), null, out found);
      Assert.IsFalse(found);
      Assert.IsTrue(emptySeq.SameSequenceAs(nullTrack));

      node = node.Set(h.GetEnumerator(), h, null, true);
      node = node.Set(hi.GetEnumerator(), hi, null, true);
      node = node.Set(him.GetEnumerator(), him, null, true);
      node = node.Set(her.GetEnumerator(), her, null, true);
      var hNode = node.GetDerived('h', null);
      var hiNode = hNode.GetDerived('i', null);
      var himNode = hiNode.GetDerived('m', null);
      var heNode = hNode.GetDerived('e', null);
      var herNode = heNode.GetDerived('r', null);

      var emptyTrack = node.Track("".GetEnumerator(), null, out found);
      Assert.IsTrue(found);
      Assert.IsTrue(Seq.Build(node).SameSequenceAs(emptyTrack));

      var hTrack = node.Track(h.GetEnumerator(), null, out found);
      Assert.IsTrue(found);
      Assert.IsTrue(Seq.Build(node, hNode).SameSequenceAs(hTrack));

      var hiTrack = node.Track(hi.GetEnumerator(), null, out found);
      Assert.IsTrue(found);
      Assert.IsTrue(Seq.Build(node, hNode, hiNode).SameSequenceAs(hiTrack));

      var himTrack = node.Track(him.GetEnumerator(), null, out found);
      Assert.IsTrue(found);
      Assert.IsTrue(Seq.Build(node, hNode, hiNode, himNode).SameSequenceAs(himTrack));

      var herTrack = node.Track(her.GetEnumerator(), null, out found);
      Assert.IsTrue(found);
      Assert.IsTrue(Seq.Build(node, hNode, heNode, herNode).SameSequenceAs(herTrack));

      var heroTrack = node.Track("hero".GetEnumerator(), null, out found);
      Assert.IsFalse(found);
      Assert.IsTrue(Seq.Build(node, hNode, heNode, herNode).SameSequenceAs(heroTrack));

      var niceTrack = node.Track("nice".GetEnumerator(), null, out found);
      Assert.IsFalse(found);
      Assert.IsTrue(Seq.Build(node).SameSequenceAs(niceTrack));
    }

    private void AssertNodeState(TrieNode<char, string> node, string value, bool isActive, int activeCount)
    {
      Assert.IsNotNull(node);
      
      if (value == null)
        Assert.IsNull(node.Value);
      else
        Assert.AreEqual(value, node.Value);
      
      if (isActive)
        Assert.IsTrue(node.GetIsActive());
      else
        Assert.IsFalse(node.GetIsActive());
      
      Assert.AreEqual(activeCount, node.GetActiveCount());
    }
  }
}
