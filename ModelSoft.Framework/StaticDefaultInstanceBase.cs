using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework
{
  public class StaticDefaultInstanceBase<T>
    where T : class
  {
    private static object _lock = new object();
    private static T _Default;
    private static Func<T> _InstanceCreator;

    public static T Default
    {
      get
      {
        EnsureDefaultComposer();
        return _Default;
      }
    }

    public static bool IsSetUp
    {
      get
      {
        return _InstanceCreator != null;
      }
    }

    public static void SetUp(T _Instance)
    {
      _Instance.RequireNotNull("_Instance");
      SetUp(() => _Instance);

    }
    public static void SetUp(Func<T> _CreateInstance)
    {
      _CreateInstance.RequireNotNull("_CreateInstance");
      Require.Condition(_InstanceCreator == null, "_CreateInstance", Resources.ExMsg_DefaultInstanceAlreadySetUp);
      lock (_lock)
      {
        Require.Condition(_InstanceCreator == null, "_CreateInstance", Resources.ExMsg_DefaultInstanceAlreadySetUp);
        _InstanceCreator = _CreateInstance;
      }
    }

    private static void EnsureDefaultComposer()
    {
      _InstanceCreator.RequireNotNull("_InstanceCreator");
      if (_Default == null)
      {
        lock (_lock)
        {
          if (_Default == null)
          {
            _Default = _InstanceCreator();
            _Default.RequireNotNull("_Default", Resources.ExMsg_InvalidSetUp);
          }
        }
      }
    }

  }
}
