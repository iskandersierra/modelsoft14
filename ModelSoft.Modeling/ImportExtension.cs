using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using ModelSoft.Framework;
using ModelSoft.Modeling.Definitions.Common;
using ModelSoft.Modeling.Definitions.Common.ImplPOCOS;

namespace ModelSoft.Modeling
{
  [ContentProperty("ModelUri")]
  public class ImportExtension : MarkupExtension
  {
    public ImportExtension()
    {
    }

    public ImportExtension(Uri modelUri)
    {
      this.ModelUri = modelUri;
    }

    [ConstructorArgument("modelUri")]
    public Uri ModelUri { get; set; }

    public string Alias { get; set; }

    public SerializedModelFormat Format { get; set; }

    private Uri BaseUri { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      var uriContext = serviceProvider.GetService<IUriContext>(true);
      this.BaseUri = uriContext.BaseUri;

      var result = new ImportedModel
      {
        ModelUri = this.ModelUri,
        Alias = this.Alias,
        Format = this.Format,
      };

      ModelLoaderContext.Current.RegisterBeforeLoadAction(ctx =>
        {
          Uri uri;
          if (ModelUri.IsAbsoluteUri)
            uri = ModelUri;
          else
            uri = new Uri(this.BaseUri, this.ModelUri);

          var importedModel = ModelLoader.Deserialize(uri, Format, ctx);

          result.Model = importedModel;
        });

      return result;
    }
  }
}
