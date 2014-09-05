using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework
{
  public interface IFreezable
  {
    bool IsFrozen { get; set; }

    void Freeze();
  }
}
