using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.Collections
{
  public class AVLTree<TKey, TValue> : 
    IPriorityDispatcher<KeyValuePair<TKey, TValue>>
  {
    private AVLNode<TKey, TValue> root;
    private int count;
    private bool allowRepetitions;
    private IComparer<TKey> comparer;

    public AVLTree(IComparer<TKey> comparer, bool allowRepetitions)
    {
      this.allowRepetitions = allowRepetitions;
      this.comparer = comparer;
    }

    public AVLTree(IComparer<TKey> comparer) : this(comparer, true) { }

    public AVLTree() : this(Comparer<TKey>.Default, true) { }

    public bool Add(TKey key, TValue value)
    {
      bool inserted = false;
      root = AVLNode<TKey, TValue>.InsertLast(root, new AVLNode<TKey, TValue>(key, value), comparer, allowRepetitions, delegate { inserted = true; });
      if (inserted) count++;
      return inserted;
    }

    public bool Remove(TKey key)
    {
      bool removed = false;
      root = AVLNode<TKey, TValue>.RemoveFirst(root, key, comparer, delegate { removed = true; });
      if (removed) count--;
      return removed;
    }

    public bool ContainsKey(TKey key)
    {
      AVLNode<TKey, TValue> node = AVLNode<TKey, TValue>.Search(root, key, comparer);
      return node != null;
    }

    public TValue this[TKey key]
    {
      get
      {
        AVLNode<TKey, TValue> node = AVLNode<TKey, TValue>.Search(root, key, comparer);
        Require.Condition(node != null, "key", FormattedResources.ArgException_KeyNotFound("key", key.AsString()));
        return (TValue)node.Value;
      }
      set
      {
        AVLNode<TKey, TValue> node = AVLNode<TKey, TValue>.Search(root, key, comparer);
        if (node == null)
          Add(key, value);
        else
          node.Value = value;
      }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      value = default(TValue);
      AVLNode<TKey, TValue> node = AVLNode<TKey, TValue>.Search(root, key, comparer);
      if (node == null) return false;
      value = (TValue)node.Value;
      return true;
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
      Add(item.Key, item.Value);
    }

    public void Clear()
    {
      root = null;
      count = 0;
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      foreach (KeyValuePair<TKey, TValue> pair in GetEnumerable(item.Key))
      {
        if (Equals(item.Value, pair.Value)) return true;
      }
      return false;
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      foreach (KeyValuePair<TKey, TValue> pair in this)
      {
        array[arrayIndex++] = pair;
      }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      bool removed = false;
      root = AVLNode<TKey, TValue>.RemovePair(root, item.Key, item.Value, comparer, delegate { removed = true; });
      if (removed) count--;
      return removed;
    }

    public int Count
    {
      get { return count; }
    }

    public bool IsReadOnly
    {
      get { return false; }
    }

    public IComparer<TKey> Comparer
    {
      get { return comparer; }
    }

    public bool AllowRepetitions
    {
      get { return allowRepetitions; }
    }

    public KeyValuePair<TKey, TValue> Min
    {
      get
      {
        AVLNode<TKey, TValue> minNode = AVLNode<TKey, TValue>.SearchMin(root);
        if (minNode == null) throw new InvalidOperationException("Empty tree");
        return new KeyValuePair<TKey, TValue>((TKey)minNode.Key, (TValue)minNode.Value);
      }
    }

    public KeyValuePair<TKey, TValue> Max
    {
      get
      {
        AVLNode<TKey, TValue> maxNode = AVLNode<TKey, TValue>.SearchMax(root);
        if (maxNode == null) throw new InvalidOperationException("Empty tree");
        return new KeyValuePair<TKey, TValue>((TKey)maxNode.Key, (TValue)maxNode.Value);
      }
    }

    public IEnumerable<TKey> Keys
    {
      get
      {
        foreach (AVLNode<TKey, TValue> node in AVLNode<TKey, TValue>.Enumerate(root))
          yield return (TKey)node.Key;
      }
    }

    public IEnumerable<TValue> Values
    {
      get
      {
        foreach (AVLNode<TKey, TValue> node in AVLNode<TKey, TValue>.Enumerate(root))
          yield return (TValue)node.Value;
      }
    }

    public KeyValuePair<TKey, TValue> Search(TKey subKey, IComparer<TKey> subKeyComparer)
    {
      AVLNode<TKey, TValue> node = AVLNode<TKey, TValue>.Search(root, subKey, subKeyComparer);
      Require.Condition(node != null, "subKey", FormattedResources.ArgException_KeyNotFound("subKey", subKey.AsString()));
      return new KeyValuePair<TKey, TValue>();
    }

    public bool TrySearch(TKey subKey, IComparer<TKey> subKeyComparer, out KeyValuePair<TKey, TValue> pair)
    {
      AVLNode<TKey, TValue> node = AVLNode<TKey, TValue>.Search(root, subKey, subKeyComparer);
      if (node != null)
      {
        pair = new KeyValuePair<TKey, TValue>((TKey)node.Key, (TValue)node.Value);
        return true;
      }
      pair = new KeyValuePair<TKey, TValue>();
      return false;
    }

    public void ChangePriority(Func<KeyValuePair<TKey, TValue>, bool> acceptChangeCallback,
                               ChangePriorityCallback<KeyValuePair<TKey, TValue>> changeCallback)
    {
      ChangePriorityInternal(this, acceptChangeCallback, changeCallback);
    }

    public void ChangePriority(TKey key, Func<KeyValuePair<TKey, TValue>, bool> acceptChangeCallback,
                               ChangePriorityCallback<KeyValuePair<TKey, TValue>> changeCallback)
    {
      ChangePriorityInternal(GetEnumerable(key), acceptChangeCallback, changeCallback);
    }

    void IPriorityDispatcher<KeyValuePair<TKey, TValue>>.ChangePriority<T>(T key, Func<KeyValuePair<TKey, TValue>, bool> acceptChangeCallback, ChangePriorityCallback<KeyValuePair<TKey, TValue>> changeCallback)
    {
      //Contract.Requires(typeof(T) == typeof(TKey));

      ChangePriorityInternal(GetEnumerable((TKey)(object)key), acceptChangeCallback, changeCallback);
    }

    private void ChangePriorityInternal(IEnumerable<KeyValuePair<TKey, TValue>> enumerable,
                                Func<KeyValuePair<TKey, TValue>, bool> acceptChangeCallback,
                                ChangePriorityCallback<KeyValuePair<TKey, TValue>> changeCallback)
    {
      if (changeCallback == null) return;
      Queue<KeyValuePair<TKey, TValue>> queueRemove = new Queue<KeyValuePair<TKey, TValue>>();
      Queue<KeyValuePair<TKey, TValue>> queueInsert = new Queue<KeyValuePair<TKey, TValue>>();
      foreach (KeyValuePair<TKey, TValue> pair in enumerable)
      {
        if (acceptChangeCallback != null && !acceptChangeCallback(pair)) continue;
        KeyValuePair<TKey, TValue> newPair = pair;
        ContinueChangePriorityAction action = changeCallback(ref newPair);
        if ((action & ContinueChangePriorityAction.Skip) != ContinueChangePriorityAction.Skip)
        {
          queueRemove.Enqueue(pair);
          queueInsert.Enqueue(newPair);
        }
        if ((action & ContinueChangePriorityAction.Stop) == ContinueChangePriorityAction.Stop)
          break;
      }
      while (queueRemove.Count > 0)
        Remove(queueRemove.Dequeue());
      while (queueInsert.Count > 0)
        Add(queueInsert.Dequeue());
    }

    public void Push(KeyValuePair<TKey, TValue> item)
    {
      Add(item.Key, item.Value);
    }

    public KeyValuePair<TKey, TValue> Pop()
    {
      KeyValuePair<TKey, TValue> min = Min;
      Remove(min.Key);
      return min;
    }

    public KeyValuePair<TKey, TValue> Top
    {
      get { return Min; }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return AVLNode<TKey, TValue>.Enumerate(root).Select(node => new KeyValuePair<TKey, TValue>((TKey)node.Key, (TValue)node.Value)).GetEnumerator();
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> GetEnumerable(TKey key)
    {
      return AVLNode<TKey, TValue>.Enumerate(root, key, comparer).Select(node => new KeyValuePair<TKey, TValue>((TKey)node.Key, (TValue)node.Value));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public override string ToString()
    {
      return root == null ? "<empty>" : root.ToFullString;
    }
  }
}
