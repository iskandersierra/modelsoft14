using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling
{
  public abstract class ModelDeserializerBase : 
    IModelDeserializer
  {
    public IModel Deserialize(Uri sourceUri, ModelLoaderContext loadContext = null)
    {
      sourceUri.RequireNotNull("sourceUri");
      
      if (loadContext == null)
        loadContext = new ModelLoaderContext();
      else
      {
        IModel loadedModel;
        if (loadContext.TryGetLoadedModel(sourceUri, out loadedModel))
          return loadedModel;
      }

      using (loadContext.Open())
      {
        var model = DeserializeOverride(sourceUri, loadContext);
        loadContext.RegisterModel(sourceUri, model);
        return model;
      }
    }

    protected abstract IModel DeserializeOverride(Uri sourceUri, ModelLoaderContext loadContext);
  }
}
