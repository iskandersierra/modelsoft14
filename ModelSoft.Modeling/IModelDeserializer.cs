using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling
{
  public interface IModelDeserializer
  {
    IModel Deserialize(Uri sourceUri, ModelLoaderContext loadContext = null);
  }
}
