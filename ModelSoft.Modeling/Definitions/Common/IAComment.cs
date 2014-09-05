using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling.Definitions.Common
{
  public interface IAComment :
    IModelElement
  {

    [RelationshipType(ERelationshipType.Container)]
    IACommented CommentedElement { get; }

    string Text { get; set; }

    string AuthorName { get; set; }

    DateTime? LastModification { get; set; }

    string Format { get; set; } // Plain Text (default), RTF, HTML, etc. (convention)
  }
}
