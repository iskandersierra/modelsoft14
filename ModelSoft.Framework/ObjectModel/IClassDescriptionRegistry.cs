using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  public interface IClassDescriptionRegistry
  {
    IEnumerable<IClassDescription> RegisteredClasses { get; }
  }
}
