using System.Collections.Generic;

namespace ModelSoft.Framework.Collections
{
  public interface IItemDispatcher<T> : ICollection<T>
  {
    void Push(T item);
    T Pop();
    T Top { get; }
  }
}
