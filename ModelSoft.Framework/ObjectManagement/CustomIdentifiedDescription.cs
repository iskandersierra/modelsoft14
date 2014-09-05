using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  public abstract class CustomIdentifiedDescription : 
    FreezableBase, 
    IIdentifiedDescription
  {
    public CustomIdentifiedDescription()
    {
    }

    public CustomIdentifiedDescription(string _Identifier)
    {
      this.Identifier = _Identifier;
    }

    private string identifier;
    public string Identifier
    {
      get { return identifier; }
      set 
      {
        CheckModifyFrozen();
        identifier = value; 
      }
    }

    protected override void OnFreeze()
    {
      if (Identifier.IsWS())
        throw new InvalidFreezingOperationException(FormattedResources.ExMsg_PropertyCannotBeWhitespace("Identifier"));
      base.OnFreeze();
    }

    public override string ToString()
    {
      return Identifier ?? "a " + this.GetType().Name;
    }
  }
}
