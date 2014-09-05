using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using System.Xml;
using ModelSoft.Framework;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling
{
  public class XamlModelDeserializer :
    WebStreamModelDeserializerBase

  {
    protected override IModel DeserializeOverride(Uri sourceUri, Stream stream, ModelLoaderContext loadContext)
    {
      using (var xmlReader = new XmlTextReader(sourceUri.ToString(), stream))
      {
        var obj = XamlServices.Load(xmlReader);
        if (!(obj is IModel))
          throw new InvalidOperationException("Deserialized object is not a model: " + (obj == null ? "<null>" : obj.GetType().FullName));
        return obj as IModel;
      }
    }
  }
}
