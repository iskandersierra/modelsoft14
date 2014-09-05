using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using ModelSoft.Modeling.Definitions.Common;
using ModelSoft.Modeling.Definitions.Common.ImplPOCOS;

namespace ModelSoft.Modeling.Definitions.Core.MM0.ImplPOCOS
{
  [ContentProperty("Definitions")]
  [DebuggerDisplay("{ModelElementTypeName} \"{Name}\"")]
  public class MetaModel :
    MM0ModelElement,
    IMetaModel,
    IModelImpl
  {
    public string Name { get; set; }

    public string Namespace { get; set; }

    [IsComputed]
    public string FullName
    {
      get { return this.GetFullNameEx(); }
    }

    [IsComputed]
    [RelationshipType(ERelationshipType.Value)]
    public Uri BaseUri { get; internal set; }

    [IsComputed]
    [RelationshipType(ERelationshipType.Container)]
    public IANamespace ParentNamespace
    {
      get { return this.GetParentNamespaceEx(); }
    }

    [RelationshipType(ERelationshipType.Content)]
    public IModelElementCollection<IMetaModelDefinition> Definitions
    {
      get { return _Definitions ?? (_Definitions = new ModelElementCollection<IMetaModelDefinition>(this, true)); }
    }
    private ModelElementCollection<IMetaModelDefinition> _Definitions;

    [IsComputed]
    [IsHiddenProperty]
    [RelationshipType(ERelationshipType.Association)]
    public IEnumerable<IARequiredNamedElement> NamedChildren
    {
      get { return this.GetNamedChildrenEx(); }
    }

    [RelationshipType(ERelationshipType.Content)]
    public IModelElementCollection<IImportedModel> ImportedModels
    {
      get { return _ImportedModels ?? (_ImportedModels = new ModelElementCollection<IImportedModel>(this, true)); }
    }
    private ModelElementCollection<IImportedModel> _ImportedModels;

    void IModelImpl.SetBaseUri(Uri baseUri)
    {
      this.BaseUri = baseUri;
    }
  }
}
