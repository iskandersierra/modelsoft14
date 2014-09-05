using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling.Definitions.Core.Expressions
{
  public interface ICodeSnippet :
    IModelElement
  {
    string CodeLanguage { get; set; }

    string Code { get; set; }
  }
}
