using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.IoC
{
  public interface IServiceComposer :
    IServiceManager
  {
    #region [ BuildUp ]
    object BuildUp(object existingObject);
    #endregion

    #region [ IsRegistered ]
    bool IsRegistered(Type type, string key = null);

    bool IsRegistered<T>(string key = null);
    #endregion

    #region [ RegisterInstance ]
    IServiceComposer RegisterInstance(Type type, object instance, string key = null, ServiceLifeTime lifetime = ServiceLifeTime.Singleton);

    IServiceComposer RegisterInstance<TInterface>(TInterface instance, string key = null, ServiceLifeTime lifetime = ServiceLifeTime.Singleton);
    #endregion

    #region [ Register Type ]
    IServiceComposer RegisterType(Type serviceType, string key = null, ServiceLifeTime lifetime = ServiceLifeTime.Singleton, params InjectMemberInfo[] injectMembers);
    IServiceComposer RegisterType(Type interfaceType, Type serviceType, string key = null, ServiceLifeTime lifetime = ServiceLifeTime.Singleton, params InjectMemberInfo[] injectMembers);

    IServiceComposer RegisterType<TService>(string key = null, ServiceLifeTime lifetime = ServiceLifeTime.Singleton, params InjectMemberInfo[] injectMembers);
    IServiceComposer RegisterType<TInterface, TService>(string key = null, ServiceLifeTime lifetime = ServiceLifeTime.Singleton, params InjectMemberInfo[] injectMembers) 
      where TService : TInterface;
    #endregion
  }
}
