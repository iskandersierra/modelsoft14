using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  public class ClassDescriptionSetup
  {
    public string Name { get; set; }

    public string ResourceName { get; set; }

    public string FriendlyName { get; set; }

    public Func<string> FriendlyNameCallback { get; set; }

    public Func<string, string> ByNameFriendlyNameCallback { get; set; }

    internal Func<string, string> GetFriendlyNameFunc()
    {
      if (ByNameFriendlyNameCallback != null)
      {
        if (ResourceName.IsWS())
          throw new ArgumentNullException("ResourceName", @"Resource name cannot be empty if by name callback is provided.");
        return ByNameFriendlyNameCallback;
      }
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
      throw new InvalidOperationException(@"Class description setup is missconfigured: Name is missing.");
    }
  }
}
