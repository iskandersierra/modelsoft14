using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling.Definitions.Common
{
  public interface IModelElementCollection :
    IList
  {
    IModelElement Owner { get; }
  }

  public interface IModelElementCollection<T> :
    IModelElementCollection,
    IList<T>
    where T : class, IModelElement
  {
  
  }
}
