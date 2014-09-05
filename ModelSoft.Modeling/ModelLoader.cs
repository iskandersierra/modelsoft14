using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ModelSoft.Framework;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling
{
  public static class ModelLoader
  {
    public static IModel DeserializeFile(this IModelDeserializer deserializer, string fileName, ModelLoaderContext loadContext = null)
    {
      deserializer.RequireNotNull("deserializer");
      fileName.RequireNotEmpty("fileName");

      var fullPath = Path.GetFullPath(fileName);
      var sourceUri = new Uri("file:///" + fullPath);
      var model = deserializer.Deserialize(sourceUri, loadContext);
      return model;
    }

    public static IModel Deserialize(Uri sourceUri, SerializedModelFormat format = SerializedModelFormat.Xaml, ModelLoaderContext loadContext = null)
    {
      sourceUri.RequireNotNull("sourceUri");

      if (loadContext == null)
        loadContext = new ModelLoaderContext();

      IModelDeserializer deserializer;

      switch (format)
      {
        case SerializedModelFormat.Xaml:
          deserializer = new XamlModelDeserializer();
          break;
        default:
          throw new NotSupportedException("Imported model format not supported: " + format);
      }

      var model = deserializer.Deserialize(sourceUri, loadContext);

      return model;
    }

    public static IModel DeserializeFile(string fileName, SerializedModelFormat format = SerializedModelFormat.Xaml, ModelLoaderContext loadContext = null)
    {
      fileName.RequireNotEmpty("fileName");

      var fullPath = Path.GetFullPath(fileName);
      var sourceUri = new Uri("file:///" + fullPath);
      var model = Deserialize(sourceUri, format, loadContext);
      return model;
    }
  }
}
