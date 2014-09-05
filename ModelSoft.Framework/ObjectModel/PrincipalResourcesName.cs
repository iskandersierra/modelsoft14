using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
  public class PrincipalResourcesName : Attribute
  {
    public PrincipalResourcesName(string resourcesName)
    {
      this.ResourcesName = resourcesName;
      this.AssemblyName = null;
    }

    public PrincipalResourcesName(string resourcesName, string assemblyName)
    {
      this.ResourcesName = resourcesName;
      this.AssemblyName = assemblyName;
    }

    public string AssemblyName { get; set; }

    public string ResourcesName { get; set; }
  }
}
