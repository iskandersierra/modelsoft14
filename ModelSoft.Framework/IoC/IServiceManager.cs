using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.IoC
{
  public interface IServiceManager :
    IServiceProvider
  {
    #region [ GetAllInstances ]
    IEnumerable<TService> GetAllInstances<TService>();

    IEnumerable<object> GetAllInstances(Type serviceType);
    #endregion

    #region [ GetInstance ]
    TService GetInstance<TService>(string key = null);

    object GetInstance(Type serviceType, string key = null);
    #endregion
  }
}
