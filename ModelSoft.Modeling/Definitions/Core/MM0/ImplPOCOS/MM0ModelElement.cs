using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;
using ModelSoft.Modeling.Definitions.Common.ImplPOCOS;

namespace ModelSoft.Modeling.Definitions.Core.MM0.ImplPOCOS
{
  public abstract class MM0ModelElement :
    ModelElement,
    IMM0ModelElement
  {
    [RelationshipType(ERelationshipType.Container)]
    public IMetaModel MetaModel
    {
      get { return this.RootModel as IMetaModel; }
    }

    [RelationshipType(ERelationshipType.Content)]
    public IModelElementCollection<IAComment> Comments 
    {
      get { return _Comments ?? (_Comments = new ModelElementCollection<IAComment>(this, true)); }
    }
    private ModelElementCollection<IAComment> _Comments;

    [IsComputed]
    [IsHiddenProperty]
    public override string ModelElementTypeName
    {
      get { return GetType().Name; }
    }
  }
}
