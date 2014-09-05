using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling.Definitions.Common
{
  public interface IImportedModel :
    IModelElement
  {
    string Alias { get; set; }

    Uri ModelUri { get; set; }

    SerializedModelFormat Format { get; set; }

    IModel Model { get; }
  }
}
