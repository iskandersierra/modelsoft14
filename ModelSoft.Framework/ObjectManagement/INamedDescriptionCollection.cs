using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  public interface INamedDescriptionCollection<T> :
    IDescriptionCollection<T>
    where T : INamedDescription
  {
    bool ContainsName(string name);

    T this[string name] { get; }
  }
}
