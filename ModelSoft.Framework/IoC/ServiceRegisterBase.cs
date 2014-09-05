using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.IoC
{
  public abstract class ServiceRegisterBase :
    IServiceRegister
  {
    public void RegisterServices(IServiceComposer composer)
    {
      composer.RequireNotNull("composer");
      
      RegisterServicesInternal(composer);
    }

    protected virtual void RegisterServicesInternal(IServiceComposer composer)
    {
    }
  }
}
