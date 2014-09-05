using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  public abstract class CustomNamedDescription : 
    CustomIdentifiedDescription, 
    INamedDescription
  {
    protected CustomNamedDescription()
    {
    }

    public CustomNamedDescription(string _Identifier, string _Name)
      : base(_Identifier)
    {
      this.Name = _Name;
    }

    private string name;
    public string Name 
    {
      get
      {
        return name;
      }
      set
      {
        CheckModifyFrozen();
        name = value;
      }
    }

    protected override void OnFreeze()
    {
      if (Name.IsWS())
        throw new InvalidFreezingOperationException(FormattedResources.ExMsg_PropertyCannotBeWhitespace("Name"));
      base.OnFreeze();
    }

    public override string ToString()
    {
      return Name ?? base.ToString();
    }
  }
}
