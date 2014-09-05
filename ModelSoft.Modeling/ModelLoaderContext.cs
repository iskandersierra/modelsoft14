using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModelSoft.Framework;
using ModelSoft.Modeling.Definitions.Common;
using ModelSoft.Modeling.Definitions.Common.ImplPOCOS;

namespace ModelSoft.Modeling
{
  public sealed class ModelLoaderContext
  {
    private int _OpenCounter;
    private int? _ThreadId;
    private Dictionary<Uri, IModel> _LoadedModels;
    private List<Action<ModelLoaderContext>> _AfterLoadActions;
    private List<Action<ModelLoaderContext>> _BeforeLoadActions;
    private bool _Monitoring;

    [ThreadStatic]
    private static ModelLoaderContext _Current;
    public static ModelLoaderContext Current { get { return _Current; } }

    public ModelLoaderContext()
    {
      _Monitoring = false;
      _OpenCounter = 0;
      _ThreadId = 0;
      _LoadedModels = new Dictionary<Uri, IModel>();
      _AfterLoadActions = new List<Action<ModelLoaderContext>>();
      _BeforeLoadActions = new List<Action<ModelLoaderContext>>();
    }

    public IDisposable Open()
    {
      return new GenericMonitor(MonitorEnterActions, MonitorExitActions, true);
    }

    public IEnumerable<Uri> LoadedModelUris
    {
      get
      {
        return _LoadedModels.Keys.AsEnumerable();
      }
    }

    public IEnumerable<IModel> LoadedModels
    {
      get
      {
        return _LoadedModels.Values.AsEnumerable();
      }
    }

    public IEnumerable<KeyValuePair<Uri, IModel>> LoadedUrisAndModels
    {
      get
      {
        return _LoadedModels.AsEnumerable();
      }
    }

    public bool TryGetLoadedModel(Uri sourceUri, out IModel loadedModel)
    {
      sourceUri.RequireNotNull("sourceUri");

      return _LoadedModels.TryGetValue(sourceUri, out loadedModel);
    }
    
    public void RegisterModel(Uri sourceUri, IModel model)
    {
      sourceUri.RequireNotNull("sourceUri");
      model.RequireNotNull("model");

      if (!(model is IModelImpl))
        throw new NotSupportedException("Model implementations must implement interface IModelImpl");

      if (_LoadedModels.ContainsKey(sourceUri))
        throw new ArgumentException("Cannot load the same model twice in the same context.");

      _LoadedModels.Add(sourceUri, model);
      ((IModelImpl)model).SetBaseUri(sourceUri);
    }

    public void RegisterBeforeLoadAction(Action<ModelLoaderContext> action)
    {
      action.RequireNotNull("action");

      if (_OpenCounter == 0) throw new InvalidOperationException("Cannot register new actions while the context is not open");
      if (_Monitoring) throw new InvalidOperationException("Cannot register new actions during monitoring");

      _BeforeLoadActions.Add(action);
    }

    public void RegisterAfterLoadAction(Action<ModelLoaderContext> action)
    {
      action.RequireNotNull("action");

      if (_OpenCounter == 0) throw new InvalidOperationException("Cannot register new actions while the context is not open");
      if (_Monitoring) throw new InvalidOperationException("Cannot register new actions during monitoring");

      _AfterLoadActions.Add(action);
    }

    private void MonitorEnterActions()
    {
      if (_Monitoring) throw new InvalidOperationException();
      using (_Monitoring.SetAndRestore(true, v => _Monitoring = v))
      {
        if (_OpenCounter == 0)
        {
          if (_Current != null)
            throw new InvalidOperationException("There cannot be more than one opened context in the same thread.");
          _OpenCounter = 1;
          _ThreadId = GetCurrentThreadId();
          _Current = this;
        }
        else
        {
          CheckThread();
          _OpenCounter++;
        }
      }
    }

    private void MonitorExitActions()
    {
      var beforeActions = _BeforeLoadActions.ToList();
      _BeforeLoadActions.Clear();
      foreach (var action in beforeActions)
      {
        action(this);
      }

      if (_Monitoring) throw new InvalidOperationException();
      using (_Monitoring.SetAndRestore(true, v => _Monitoring = v))
      {
        if (_OpenCounter > 1)
        {
          CheckThread();
          _OpenCounter--;
        }
        else
        {
          var actions = _AfterLoadActions.ToList();
          _AfterLoadActions.Clear();
          foreach (var action in actions)
          {
            action(this);
          }
          _OpenCounter = 0;
          _ThreadId = null;
          _Current = null;
        }
      }
    }

    private void CheckThread()
    {
      if (_ThreadId.HasValue && GetCurrentThreadId() != _ThreadId.Value)
        throw new InvalidOperationException("This context is open and cannot be used by a different thread from which it was opened.");
    }

    private static int GetCurrentThreadId()
    {
      return Thread.CurrentThread.ManagedThreadId;
    }
  }

}
