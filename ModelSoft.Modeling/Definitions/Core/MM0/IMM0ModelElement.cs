using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling.Definitions.Core.MM0
{
  public interface IMM0ModelElement :
    IModelElement,
    IACommented
  {
    [RelationshipType(ERelationshipType.Container)]
    IMetaModel MetaModel { get; }
  }
}
