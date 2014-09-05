using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ModelSoft.Modeling.Definitions.Common.ImplPOCOS
{
  [ContentProperty("ModelUri")]
  public class ImportedModel :
    ModelElement,
    IImportedModel
  {
    internal ImportedModel()
    {
    }

    public override string ModelElementTypeName
    {
      get { return "ImportedModel"; }
    }

    public string Alias { get; set; }

    public Uri ModelUri { get; set; }

    public SerializedModelFormat Format { get; set; }

    [IsComputed]
    [RelationshipType(ERelationshipType.Association)]
    public IModel Model { get; internal set; }
  }
}
