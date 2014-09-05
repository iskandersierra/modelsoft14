using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  public interface IObjectManagerCollection
  {
  }

  public interface IObjectManagerCollection<T> :
    IObjectManagerCollection,
    IList<T>
    where T : IObjectManagerContainer
  {
  }
}
