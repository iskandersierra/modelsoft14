using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSoft.Framework.Collections
{
  public class AVLNode<TKey, TValue>
  {
    public TKey Key;
    public TValue Value;
    public AVLNode<TKey, TValue> Left, Right;
    public int Height = 1;

    public AVLNode(TKey key = default(TKey), TValue value = default(TValue), AVLNode<TKey, TValue> left = null, AVLNode<TKey, TValue> right = null)
    {
      Key = key;
      Value = value;
      Left = left;
      Right = right;
    }

    public static AVLNode<TKey, TValue> Search(AVLNode<TKey, TValue> root, TKey key, IComparer<TKey> comparer)
    {
      if (root == null) return null;
      int comp = comparer.Compare(key, root.Key);
      if (comp == 0) return root;
      if (comp < 0) return Search(root.Left, key, comparer);
      return Search(root.Right, key, comparer);
    }

    public static AVLNode<TKey, TValue> SearchFirst(AVLNode<TKey, TValue> root, TKey key, IComparer<TKey> comparer)
    {
      if (root == null) return null;
      int comp = comparer.Compare(key, root.Key);
      if (comp == 0)
      {
        AVLNode<TKey, TValue> res = SearchFirst(root.Left, key, comparer);
        if (res != null) return res;
        return root;
      }
      if (comp < 0) return SearchFirst(root.Left, key, comparer);
      return SearchFirst(root.Right, key, comparer);
    }

    public static AVLNode<TKey, TValue> SearchLast(AVLNode<TKey, TValue> root, TKey key, IComparer<TKey> comparer)
    {
      if (root == null) return null;
      int comp = comparer.Compare(key, root.Key);
      if (comp == 0)
      {
        AVLNode<TKey, TValue> res = SearchLast(root.Right, key, comparer);
        if (res != null) return res;
        return root;
      }
      if (comp < 0) return SearchLast(root.Left, key, comparer);
      return SearchLast(root.Right, key, comparer);
    }

    public static AVLNode<TKey, TValue> InsertFirst(AVLNode<TKey, TValue> root, AVLNode<TKey, TValue> newNode, IComparer<TKey> comparer, bool allowRepetition, Action<AVLNode<TKey, TValue>> callbackInserted)
    {
      newNode.RequireNotNull("newNode");
      if (root == null)
      {
        if (callbackInserted != null)
          callbackInserted(newNode);
        newNode.Height = 1;
        return newNode;
      }
      int comp = comparer.Compare(newNode.Key, root.Key);
      if (comp == 0 && !allowRepetition) return root;
      if (comp <= 0)
        root.Left = InsertFirst(root.Left, newNode, comparer, allowRepetition, callbackInserted);
      else
        root.Right = InsertFirst(root.Right, newNode, comparer, allowRepetition, callbackInserted);
      return Balance(root);
    }

    public static AVLNode<TKey, TValue> InsertLast(AVLNode<TKey, TValue> root, AVLNode<TKey, TValue> newNode, IComparer<TKey> comparer, bool allowRepetition, Action<AVLNode<TKey, TValue>> callbackInserted)
    {
      newNode.RequireNotNull("newNode");
      if (root == null)
      {
        if (callbackInserted != null)
          callbackInserted(newNode);
        newNode.Height = 1;
        return newNode;
      }
      int comp = comparer.Compare(newNode.Key, root.Key);
      if (comp == 0 && !allowRepetition) return root;
      if (comp < 0)
        root.Left = InsertLast(root.Left, newNode, comparer, allowRepetition, callbackInserted);
      else
        root.Right = InsertLast(root.Right, newNode, comparer, allowRepetition, callbackInserted);
      return Balance(root);
    }

    public static AVLNode<TKey, TValue> RemoveFirst(AVLNode<TKey, TValue> root, TKey key, IComparer<TKey> comparer, Action<AVLNode<TKey, TValue>> callbackRemoved)
    {
      if (root == null) return null;
      int comp = comparer.Compare(key, root.Key);
      if (comp == 0)
      {
        AVLNode<TKey, TValue> removed = null;
        root.Left = RemoveFirst(root.Left, key, comparer, delegate(AVLNode<TKey, TValue> node)
        {
          removed = node;
          if (callbackRemoved != null)
            callbackRemoved(root);
        });
        if (removed != null) return root;
        if (callbackRemoved != null)
          callbackRemoved(root);
        if (root.Left == null && root.Right == null)
          return null;
        if (root.Left == null) return root.Right;
        if (root.Right == null) return root.Left;
        AVLNode<TKey, TValue> newRoot;
        root.Right = RemoveMin(root.Right, out newRoot);
        newRoot.Left = root.Left;
        newRoot.Right = root.Right;
        root = newRoot;
      }
      else if (comp < 0)
        root.Left = RemoveFirst(root.Left, key, comparer, callbackRemoved);
      else
        root.Right = RemoveFirst(root.Right, key, comparer, callbackRemoved);
      return Balance(root);
    }

    public static AVLNode<TKey, TValue> RemoveLast(AVLNode<TKey, TValue> root, TKey key, IComparer<TKey> comparer, Action<AVLNode<TKey, TValue>> callbackRemoved)
    {
      if (root == null) return null;
      int comp = comparer.Compare(key, root.Key);
      if (comp == 0)
      {
        AVLNode<TKey, TValue> removed = null;
        root.Right = RemoveFirst(root.Right, key, comparer, delegate(AVLNode<TKey, TValue> node)
        {
          removed = node;
          if (callbackRemoved != null)
            callbackRemoved(root);
        });
        if (removed != null) return root;
        if (callbackRemoved != null)
          callbackRemoved(root);
        if (root.Left == null && root.Right == null)
          return null;
        if (root.Left == null) return root.Right;
        if (root.Right == null) return root.Left;
        AVLNode<TKey, TValue> newRoot;
        root.Right = RemoveMin(root.Right, out newRoot);
        newRoot.Left = root.Left;
        newRoot.Right = root.Right;
        root = newRoot;
      }
      else if (comp < 0)
        root.Left = RemoveFirst(root.Left, key, comparer, callbackRemoved);
      else
        root.Right = RemoveFirst(root.Right, key, comparer, callbackRemoved);
      return Balance(root);
    }

    public static AVLNode<TKey, TValue> RemovePair(AVLNode<TKey, TValue> root, TKey key, TValue value, IComparer<TKey> comparer, Action<AVLNode<TKey, TValue>> callbackRemoved)
    {
      if (root == null) return null;
      int comp = comparer.Compare(key, root.Key);
      if (comp == 0)
      {
        AVLNode<TKey, TValue> removed = null;
        root.Left = RemovePair(root.Left, key, value, comparer, delegate(AVLNode<TKey, TValue> node)
        {
          removed = node;
          if (callbackRemoved != null)
            callbackRemoved(root);
        });
        if (removed != null) return root;
        if (Equals(value, root.Value))
        {
          if (callbackRemoved != null)
            callbackRemoved(root);
          if (root.Left == null && root.Right == null)
            return null;
          if (root.Left == null) return root.Right;
          if (root.Right == null) return root.Left;
          AVLNode<TKey, TValue> newRoot;
          root.Right = RemoveMin(root.Right, out newRoot);
          newRoot.Left = root.Left;
          newRoot.Right = root.Right;
          root = newRoot;
        }
        else
        {
          root.Right = RemovePair(root.Right, key, value, comparer, delegate(AVLNode<TKey, TValue> node)
          {
            removed = node;
            if (callbackRemoved != null)
              callbackRemoved(root);
          });
          if (removed != null) return root;
        }
      }
      else if (comp < 0)
        root.Left = RemovePair(root.Left, key, value, comparer, callbackRemoved);
      else
        root.Right = RemovePair(root.Right, key, value, comparer, callbackRemoved);
      return Balance(root);
    }

    public static AVLNode<TKey, TValue> SearchMin(AVLNode<TKey, TValue> root)
    {
      if (root == null) return null;
      if (root.Left == null) return root;
      return SearchMin(root.Left);
    }

    public static AVLNode<TKey, TValue> SearchMax(AVLNode<TKey, TValue> root)
    {
      if (root == null) return null;
      if (root.Right == null) return root;
      return SearchMax(root.Right);
    }

    private static AVLNode<TKey, TValue> RemoveMin(AVLNode<TKey, TValue> root, out AVLNode<TKey, TValue> removed)
    {
      if (root.Left == null)
      {
        removed = root;
        return root.Right;
      }
      root.Left = RemoveMin(root.Left, out removed);
      return Balance(root);
    }

    public static AVLNode<TKey, TValue> Balance(AVLNode<TKey, TValue> root)
    {
      if (root == null) return null;
      int left = GetHeight(root.Left);
      int right = GetHeight(root.Right);
      root.Height = System.Math.Max(left, right) + 1;
      if (System.Math.Abs(left - right) <= 1) return root;
      if (left < right)
      {
        int rightright = GetHeight(root.Right.Right);
        if (rightright > left)
        {
          AVLNode<TKey, TValue> B = root.Right;
          root.Right = B.Left;
          B.Left = root;
          root.Height = CalculateHeight(root);
          B.Height = CalculateHeight(B);
          return B;
        }
        else
        {
          AVLNode<TKey, TValue> B = root.Right;
          AVLNode<TKey, TValue> C = B.Left;
          root.Right = C.Left;
          B.Left = C.Right;
          C.Left = root;
          C.Right = B;
          root.Height = CalculateHeight(root);
          B.Height = CalculateHeight(B);
          C.Height = CalculateHeight(C);
          return C;
        }
      }
      else
      {
        int leftleft = GetHeight(root.Left.Left);
        if (leftleft > right)
        {
          AVLNode<TKey, TValue> B = root.Left;
          root.Left = B.Right;
          B.Right = root;
          root.Height = CalculateHeight(root);
          B.Height = CalculateHeight(B);
          return B;
        }
        else
        {
          AVLNode<TKey, TValue> B = root.Left;
          AVLNode<TKey, TValue> C = B.Right;
          root.Left = C.Right;
          B.Right = C.Left;
          C.Right = root;
          C.Left = B;
          root.Height = CalculateHeight(root);
          B.Height = CalculateHeight(B);
          C.Height = CalculateHeight(C);
          return C;
        }
      }
    }

    public static IEnumerable<AVLNode<TKey, TValue>> Enumerate(AVLNode<TKey, TValue> root)
    {
      if (root == null) yield break;
      foreach (AVLNode<TKey, TValue> node in Enumerate(root.Left))
        yield return node;
      yield return root;
      foreach (AVLNode<TKey, TValue> node in Enumerate(root.Right))
        yield return node;
    }

    public static IEnumerable<AVLNode<TKey, TValue>> Enumerate(AVLNode<TKey, TValue> root, TKey key, IComparer<TKey> comparer)
    {
      if (root == null) yield break;
      int comp = comparer.Compare(key, root.Key);
      if (comp <= 0)
        foreach (AVLNode<TKey, TValue> node in Enumerate(root.Left, key, comparer))
          yield return node;
      if (comp == 0)
        yield return root;
      if (comp >= 0)
        foreach (AVLNode<TKey, TValue> node in Enumerate(root.Right, key, comparer))
          yield return node;
    }

    private static int GetHeight(AVLNode<TKey, TValue> node)
    {
      return node == null ? 0 : node.Height;
    }

    private static int CalculateHeight(AVLNode<TKey, TValue> node)
    {
      return System.Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }

    public string ToFullString
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        FillString(sb, "");
        return sb.ToString();
      }
    }

    public void FillString(StringBuilder builder, string indent)
    {
      builder.Append(indent).Append(string.Format("<{0}, {1} ({2}) : H={3}>\r\n", Key, Value, (Left == null ? (Right == null ? "O" : "R") : (Right == null ? "L" : "LR")), Height));
      if (Left != null) Left.FillString(builder, indent + "    ");
      if (Right != null) Right.FillString(builder, indent + "    ");
    }

    public override string ToString()
    {
      string s = string.Format("<{0}, {1}>", Key, Value);
      if (Left != null) s += string.Format(" L:({0})", Left.Key);
      if (Right != null) s += string.Format(" R:({0})", Right.Key);
      s += " H:" + Height;
      return s;
    }
  }
}
