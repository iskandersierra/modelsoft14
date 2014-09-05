using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  public interface INamedDescription : 
    IIdentifiedDescription
  {
    string Name { get; }
  }
}
