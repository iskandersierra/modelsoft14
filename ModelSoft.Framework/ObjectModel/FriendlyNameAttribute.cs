using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
  public class FriendlyNameAttribute : Attribute
  {
    public FriendlyNameAttribute(string name)
    {
      this.Name = name;
      this.IsLocalizable = false;
    }

    public FriendlyNameAttribute(string name, bool isLocalizable)
    {
      this.Name = name;
      this.IsLocalizable = isLocalizable;
    }

    public string Name { get; set; }

    public bool IsLocalizable { get; set; }
  }
}
