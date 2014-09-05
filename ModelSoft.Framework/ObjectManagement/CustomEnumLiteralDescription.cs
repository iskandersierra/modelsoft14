using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  public class CustomEnumLiteralDescription : 
    CustomNamedDescription,
    IEnumLiteralDescription,
    IDescriptionWithParent
  {
    protected CustomEnumLiteralDescription()
    {
    }

    public CustomEnumLiteralDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public IEnumTypeDescription DeclaringEnum
    {
      get { return ((IDescriptionWithParent)this).Parent as IEnumTypeDescription; }
    }

    private object baseValue;
    public object BaseValue
    {
      get { return baseValue; }
      set 
      {
        CheckModifyFrozen();
        baseValue = value; 
      }
    }

    protected override void OnFreeze()
    {
      if (BaseValue == null)
        throw new InvalidFreezingOperationException(FormattedResources.ExMsg_PropertyCannotBeNull("BaseValue"));

      base.OnFreeze();
    }

    object IDescriptionWithParent.Parent { get; set; }

    object IDescriptionWithParent.ParentCollection { get; set; }
  }
}
