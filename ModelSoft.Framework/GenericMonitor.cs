using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework
{
  public class GenericMonitor : IDisposable
  {
    private Action EnterAction;
    private Action<bool> ExitAction;
    private int enterCount;

    public GenericMonitor(Action enterAction, Action exitAction, bool enter = true)
      : this(enterAction, exit => { if (exit) exitAction(); }, enter)
    {
    }

    public GenericMonitor(Action enterAction, Action<bool> exitAction, bool enter = true)
    {
      enterAction.RequireNotNull("enterAction");
      exitAction.RequireNotNull("exitAction");

      this.EnterAction = enterAction;
      this.ExitAction = exitAction;

      if (enter)
        Enter();
    }

    public void Enter()
    {
      lock (this)
      {
        enterCount++;
        EnterAction();
      }
    }

    public void Dispose()
    {
      lock (this)
      {
        if (enterCount <= 0)
          throw new InvalidOperationException("Calling dispose before an enter operation");
        ExitAction(enterCount == 1);
        enterCount--;
      }
      GC.SuppressFinalize(this);
    }
  }
}
