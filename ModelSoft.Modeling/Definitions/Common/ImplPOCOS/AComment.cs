using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling.Definitions.Common.ImplPOCOS
{
  public class AComment :
    ModelElement,
    IAComment
  {
    public IACommented CommentedElement
    {
      get { return ParentElement as IACommented; }
    }

    public string Text { get; set; }

    public string AuthorName { get; set; }

    public DateTime? LastModification { get; set; }

    public string Format { get; set; }

    public override string ModelElementTypeName
    {
      get { return "AComment"; }
    }
  }
}
