using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace ModelSoft.Framework.Reflection
{
  public static partial class AssemblyExtensions
  {
    public static string GetCompany(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");
      var attr = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
      if (attr != null)
        return attr.Company;
      return default(string);
    }
    public static string GetCopyright(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");
      var attr = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
      if (attr != null)
        return attr.Copyright;
      return default(string);
    }
    public static string GetCulture(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");
      var attr = assembly.GetCustomAttribute<AssemblyCultureAttribute>();
      if (attr != null)
        return attr.Culture;
      return default(string);
    }
    public static string GetDefaultAlias(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");
      var attr = assembly.GetCustomAttribute<AssemblyDefaultAliasAttribute>();
      if (attr != null)
        return attr.DefaultAlias;
      return default(string);
    }
    public static string GetDescription(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");
      var attr = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
      if (attr != null)
        return attr.Description;
      return default(string);
    }
    public static string GetFileVersion(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");
      var attr = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
      if (attr != null)
        return attr.Version;
      return default(string);
    }
    public static string GetProduct(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");
      var attr = assembly.GetCustomAttribute<AssemblyProductAttribute>();
      if (attr != null)
        return attr.Product;
      return default(string);
    }
    public static string GetTitle(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");
      var attr = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
      if (attr != null)
        return attr.Title;
      return default(string);
    }
    public static string GetTrademark(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");
      var attr = assembly.GetCustomAttribute<AssemblyTrademarkAttribute>();
      if (attr != null)
        return attr.Trademark;
      return default(string);
    }
    public static string GetVersion(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");
      var attr = assembly.GetCustomAttribute<AssemblyVersionAttribute>();
      if (attr != null)
        return attr.Version;
      return default(string);
    }
  }
}
