using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.IoC
{
  public interface IServiceRegister
  {
    void RegisterServices(IServiceComposer composer);
  }
}
