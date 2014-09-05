using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  public interface IFieldData
  {
    object Value { get; set; }

    void Reset();

    bool IsSet { get; }

    IPropertyDescription PropertyDescriptor { get; }
  }
}
