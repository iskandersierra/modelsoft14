using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.Collections
{
  public class TrieNode<TSymbol, TValue>
  {
    public TValue Value;
    public bool IsActive;
    public int ActiveCount;
    public IDictionary<TSymbol, TrieNode<TSymbol, TValue>> Derived;

    public TrieNode(TValue value = default(TValue), bool isActive = false)
    {
      this.Value = value;
      this.IsActive = isActive;
      this.ActiveCount = isActive ? 1 : 0;
    }
  }

  public static class TrieNode
  {
    public static int GetActiveCount<TSymbol, TValue>(this TrieNode<TSymbol, TValue> node)
    {
      return node == null ? 0 : node.ActiveCount;
    }
    public static bool GetIsActive<TSymbol, TValue>(this TrieNode<TSymbol, TValue> node)
    {
      return node == null ? false : node.IsActive;
    }
    public static TrieNode<TSymbol, TValue> GetDerived<TSymbol, TValue>(this TrieNode<TSymbol, TValue> node, TSymbol symbol, IEqualityComparer<TSymbol> comparer)
    {
      if (node == null) return null;
      if (node.Derived == null) return null;
      TrieNode<TSymbol, TValue> result;
      if (node.Derived.TryGetValue(symbol, out result))
        return result;
      return null;
    }
    public static void SetDerived<TSymbol, TValue>(this TrieNode<TSymbol, TValue> node, TSymbol symbol, TrieNode<TSymbol, TValue> childNode, IEqualityComparer<TSymbol> comparer)
    {
      node.RequireNotNull("node");

      if (childNode == null)
      {
        node.RemoveDerived(symbol, comparer);
      }
      else
      {
        if (node.Derived == null)
          node.Derived = new Dictionary<TSymbol, TrieNode<TSymbol, TValue>>(10, comparer);
        node.Derived[symbol] = childNode;
      }
    }
    public static void RemoveDerived<TSymbol, TValue>(this TrieNode<TSymbol, TValue> node, TSymbol symbol, IEqualityComparer<TSymbol> comparer)
    {
      node.RequireNotNull("node");

      if (node.Derived == null) return;
      node.Derived.Remove(symbol);
      if (node.Derived.Count == 0)
        node.Derived = null;
    }

    public static TrieNode<TSymbol, TValue> Set<TSymbol, TValue>(
      this TrieNode<TSymbol, TValue> node, 
      IEnumerator<TSymbol> key, TValue value, 
      IEqualityComparer<TSymbol> comparer, 
      bool allowOverride)
    {
      if (node == null)
        node = new TrieNode<TSymbol, TValue>(isActive: false);

      if (key.MoveNext())
      {
        var symbol = key.Current;
        var childNode = node.GetDerived(symbol, comparer);
        var childActiveCount = childNode.GetActiveCount();
        childNode = childNode.Set(key, value, comparer, allowOverride);
        node.SetDerived(symbol, childNode, comparer);
        node.ActiveCount += childNode.GetActiveCount() - childActiveCount;
      }
      else
      {
        if (node.IsActive && !allowOverride)
          throw new DuplicateKeyNotAllowedException(Resources.ExMsg_TrieNode_DuplicatedKeyNotAllowed);
        if (!node.IsActive)
          node.ActiveCount ++;
        node.IsActive = true;
        node.Value = value;
      }

      return node;
    }
    public static TrieNode<TSymbol, TValue> Remove<TSymbol, TValue>(
      this TrieNode<TSymbol, TValue> node, 
      IEnumerator<TSymbol> key, 
      IEqualityComparer<TSymbol> comparer)
    {
      if (node == null) return null;

      if (key.MoveNext())
      {
        var symbol = key.Current;
        var childNode = node.GetDerived(symbol, comparer);
        var childActiveCount = childNode.GetActiveCount();
        childNode = childNode.Remove(key, comparer);
        node.SetDerived(symbol, childNode, comparer);
        node.ActiveCount += childNode.GetActiveCount() - childActiveCount;
        if (node.ActiveCount == 0)
          return null;
        return node;
      }
      else
      {
        if (!node.GetIsActive()) return node;
        node.IsActive = false;
        node.ActiveCount--;
        node.Value = default(TValue);
        if (node.Derived == null)
          return null;
        return node;
      }
    }
    public static IEnumerable<TrieNode<TSymbol, TValue>> Track<TSymbol, TValue>(
      this TrieNode<TSymbol, TValue> node, 
      IEnumerator<TSymbol> key, 
      IEqualityComparer<TSymbol> comparer,
      out bool found)
    {
      var result = new List<TrieNode<TSymbol, TValue>>();
      found = false;
      while (node != null)
      {
        result.Add(node);
        if (!key.MoveNext())
        {
          found = true;
          break;
        }
        node = node.GetDerived(key.Current, comparer);
      }
      return result;
    }
    public static TrieNode<TSymbol, TValue> Find<TSymbol, TValue>(
      this TrieNode<TSymbol, TValue> node, 
      IEnumerator<TSymbol> key, 
      IEqualityComparer<TSymbol> comparer)
    {
      while (node != null && key.MoveNext())
        node = node.GetDerived(key.Current, comparer);
      return node;
    }
  }
}
