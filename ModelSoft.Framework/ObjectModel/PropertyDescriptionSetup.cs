using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  public class PropertyDescriptionSetup
  {
    public string Name { get; set; }

    public string ResourceName { get; set; }

    public string FriendlyName { get; set; }

    public Func<string> FriendlyNameCallback { get; set; }

    public Func<string, string> ByNameFriendlyNameCallback { get; set; }

    internal Func<string, string> GetFriendlyNameFunc()
    {
      if (ByNameFriendlyNameCallback != null)
        return ByNameFriendlyNameCallback;
      if (FriendlyNameCallback != null)
      {
        var callback = FriendlyNameCallback;
        return _ => callback();
      }
      if (!FriendlyName.IsWS() || !Name.IsWS())
      {
        var friendlyName = FriendlyName.IsWS() ? Name : FriendlyName;
        return _ => friendlyName;
      }
      throw new InvalidOperationException(@"Property description setup is missconfigured: Name is missing.");
    }

    private object _DefaultValue;
    public object DefaultValue
    {
      get { return _DefaultValue; }
      set 
      { 
        _DefaultValue = value;
        HasDefaultValue = (value != PropertyDescriptionValues.DoNothing);
      }
    }

    public bool HasDefaultValue { get; private set; }

    public Action<PropertyDescriptionComputeContext> ComputeValue { get; set; }

    private Func<object, object> _SimpleComputeValue;
    public Func<object, object> SimpleComputeValue
    {
      get { return _SimpleComputeValue; }
      set 
      { 
        _SimpleComputeValue = value;
        
        if (value == null)
          ComputeValue = null;
        else
          ComputeValue = ctx =>
            {
              var result = value(ctx.Self);
              ctx.SetComputedValue(result);
            };
      }
    }

    public Action<PropertyDescriptionCoerceContext> CoerceValue { get; set; }

    private Func<object, object, object> _SimpleCoerceValue;
    public Func<object, object, object> SimpleCoerceValue
    {
      get { return _SimpleCoerceValue; }
      set 
      { 
        _SimpleCoerceValue = value;

        if (value == null)
          CoerceValue = null;
        else
          CoerceValue = ctx =>
            {
              var result = value(ctx.Self, ctx.Value);
              ctx.SetCoercedValue(result);
            };
      }
    }

    public Action<PropertyDescriptionChangeValueContext> ChangeValue { get; set; }

    public Action<PropertyDescriptionChangingValueContext> ChangingValue { get; set; }


  }
}
