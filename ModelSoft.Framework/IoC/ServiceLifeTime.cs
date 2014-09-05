using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.IoC
{
  public enum ServiceLifeTime
  {
    Singleton,
    Transient,
    PerThread,
    PerResolve,
    Disposable,
  }
}
