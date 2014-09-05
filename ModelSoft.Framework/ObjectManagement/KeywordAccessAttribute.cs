using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
  public class KeywordAccessAttribute : Attribute
  {
    public KeywordAccessAttribute(string keyword)
    {
      this.Keyword = keyword;
    }
    public string Keyword { get; set; }
  }
}
