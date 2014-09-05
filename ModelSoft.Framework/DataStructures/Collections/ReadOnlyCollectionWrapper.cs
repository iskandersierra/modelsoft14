using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.DataStructures.Collections
{
  /// <summary>
  /// The read-only ICollection&lt;T&gt; implementation that is used by the ReadOnly method.
  /// Methods that modify the collection throw a NotSupportedException, methods that don't
  /// modify are fowarded through to the wrapped collection.
  /// </summary>
  [Serializable]
  internal class ReadOnlyCollectionWrapper<T> :
    ICollection<T>
  {
    private readonly ICollection<T> wrappedCollection;  // The collection we are wrapping (never null).

    /// <summary>
    /// Create a ReadOnlyCollection wrapped around the given collection.
    /// </summary>
    /// <param name="wrappedCollection">Collection to wrap.</param>
    public ReadOnlyCollectionWrapper(ICollection<T> wrappedCollection)
    {
      this.wrappedCollection = wrappedCollection;
    }

    /// <summary>
    /// Throws an NotSupportedException stating that this collection cannot be modified.
    /// </summary>
    private static void MethodModifiesCollection()
    {
      throw new NotSupportedException(@"Cannot modify read-only collection");
    }


    public IEnumerator<T> GetEnumerator()
    {
      return wrappedCollection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable)wrappedCollection).GetEnumerator();
    }

    public bool Contains(T item)
    {
      return wrappedCollection.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      wrappedCollection.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get { return wrappedCollection.Count; }
    }

    public bool IsReadOnly
    {
      get { return true; }
    }

    public void Add(T item)
    {
      MethodModifiesCollection();
    }

    public void Clear()
    {
      MethodModifiesCollection();
    }

    public bool Remove(T item)
    {
      MethodModifiesCollection(); return false;
    }
  }
}
