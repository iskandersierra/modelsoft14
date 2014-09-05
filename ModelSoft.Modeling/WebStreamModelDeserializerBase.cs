using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling
{
  public abstract class WebStreamModelDeserializerBase :
    ModelDeserializerBase
  {
    protected sealed override IModel DeserializeOverride(Uri sourceUri, ModelLoaderContext loadContext)
    {
      using (var webClient = new WebClient())
      {
        var data = webClient.DownloadData(sourceUri);
        using (var memStream = new MemoryStream(data))
        {
          var model = DeserializeOverride(sourceUri, memStream, loadContext);
          return model;
        }
      }
    }

    protected abstract IModel DeserializeOverride(Uri sourceUri, Stream stream, ModelLoaderContext loadContext);
  }
}
