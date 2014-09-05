using System;

namespace ModelSoft.Framework.Collections
{
  [Flags]
  public enum ContinueChangePriorityAction
  {
    Continue = 0,
    Stop = 1,
    Skip = 2,
    SkipAndStop = 3
  }

  public delegate ContinueChangePriorityAction ChangePriorityCallback<T>(ref T item);

  public interface IPriorityDispatcher<T> : IItemDispatcher<T>
  {
    void ChangePriority(Func<T, bool> acceptChangeCallback, ChangePriorityCallback<T> changeCallback);
    void ChangePriority<TKey>(TKey key, Func<T, bool> acceptChangeCallback, ChangePriorityCallback<T> changeCallback);
  }
}
