using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  public interface IDescriptionCollection<T> :
    IList<T>,
    IFreezable
    where T : IMetaDescription
  {
    void AddRange(IEnumerable<T> collection);
    
    void AddRange(params T[] collection);
  }
}
