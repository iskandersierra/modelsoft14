using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.Reflection
{
  public static class AllAssemblies
  {
    private const bool DefaultSkipOnError = true;

    #region [ mscorlib info ]

    private static Assembly _mscorlib;
    public static Assembly mscorlib { get { return _mscorlib ?? (_mscorlib = CommonTypes.TypeOfObject.Assembly); } }

    /// <summary>
    /// 4.0.0.0
    /// </summary>
    private static string _MSCorLibVersion;
    public static string MSCorLibVersion { get { return _MSCorLibVersion ?? (_MSCorLibVersion = mscorlib.GetVersion()); } }
    /// <summary>
    /// Microsoft Corporation
    /// </summary>
    private static string _MSCorLibCompany;
    public static string MSCorLibCompany { get { return _MSCorLibCompany ?? (_MSCorLibCompany = mscorlib.GetCompany()); } }
    /// <summary>
    /// © Microsoft Corporation.  All rights reserved.
    /// </summary>
    private static string _MSCorLibCopyright;
    public static string MSCorLibCopyright { get { return _MSCorLibCopyright ?? (_MSCorLibCopyright = mscorlib.GetCopyright()); } }
    /// <summary>
    /// mscorlib.dll
    /// </summary>
    private static string _MSCorLibDefaultAlias;
    public static string MSCorLibDefaultAlias { get { return _MSCorLibDefaultAlias ?? (_MSCorLibDefaultAlias = mscorlib.GetDefaultAlias()); } }
    /// <summary>
    /// mscorlib.dll
    /// </summary>
    private static string _MSCorLibDescription;
    public static string MSCorLibDescription { get { return _MSCorLibDescription ?? (_MSCorLibDescription = mscorlib.GetDescription()); } }
    /// <summary>
    /// 4.0.30319.18034
    /// </summary>
    private static string _MSCorLibFileVersion;
    public static string MSCorLibFileVersion { get { return _MSCorLibFileVersion ?? (_MSCorLibFileVersion = mscorlib.GetFileVersion()); } }
    /// <summary>
    /// Microsoft® .NET Framework
    /// </summary>
    private static string _MSCorLibProduct;
    public static string MSCorLibProduct { get { return _MSCorLibProduct ?? (_MSCorLibProduct = mscorlib.GetProduct()); } }
    /// <summary>
    /// mscorlib.dll
    /// </summary>
    private static string _MSCorLibTitle;
    public static string MSCorLibTitle { get { return _MSCorLibTitle ?? (_MSCorLibTitle = mscorlib.GetTitle()); } }
    #endregion

    #region [ GetLoadedAssemblies ]
    public static IEnumerable<Assembly> GetLoadedAssemblies(bool skipOnError = DefaultSkipOnError)
    {
      return AppDomain.CurrentDomain.GetLoadedAssemblies(skipOnError);
    }
    public static IEnumerable<Assembly> GetLoadedAssemblies(this AppDomain appDomain, bool skipOnError = DefaultSkipOnError)
    {
      appDomain.RequireNotNull("appDomain");

      IEnumerable<Assembly> result;
      try
      {
        result = appDomain.GetAssemblies();
      }
      catch (Exception)
      {
        if (skipOnError)
          return new Assembly[0];
        throw;
      }
      return result;
    }
    #endregion

    #region [ GetAssemblyNames ]
    public static IEnumerable<string> GetAssemblyNamesInBasePath(bool skipOnError = DefaultSkipOnError)
    {
      try
      {
        return GetAssemblyNames(AppDomain.CurrentDomain.BaseDirectory, skipOnError);
      }
      catch (Exception)
      {
        if (skipOnError)
          return CommonTypes.EmptyArrayOfString;
        throw;
      }

    }
    public static IEnumerable<string> GetAssemblyNames(string path, bool skipOnError = DefaultSkipOnError)
    {
      path.RequireNotEmpty(path);

      IEnumerable<string> result;
      try
      {
        result = Directory.EnumerateFiles(path, "*.dll")
          .Concat(Directory.EnumerateFiles(path, "*.exe"));
      }
      catch (Exception ex)
      {
        if (skipOnError &&
          (ex is DirectoryNotFoundException ||
          ex is IOException ||
          ex is SecurityException ||
          ex is UnauthorizedAccessException))
          return CommonTypes.EmptyArrayOfString;
        throw;
      }
      return result;
    }
    #endregion

    #region [ GetAssemblies ]
    public static IEnumerable<Assembly> GetAssemblies(string path, bool skipOnError = DefaultSkipOnError)
    {
      try
      {
        var result =
          from name in GetAssemblyNames(path, skipOnError)
          select LoadAssembly(Path.GetFileNameWithoutExtension(name), skipOnError) into a
          where a != null
          select a;
#if TRACE
        result = result.ToList();
#endif
        return result;
      }
      catch (Exception)
      {
        if (skipOnError)
          return new Assembly[0];
        throw;
      }
    }

    public static IEnumerable<Assembly> GetAssembliesInBasePath(bool skipOnError = DefaultSkipOnError)
    {
      try
      {
        return GetAssemblies(AppDomain.CurrentDomain.BaseDirectory);
      }
      catch (Exception)
      {
        if (skipOnError)
          return new Assembly[0];
        throw;
      }
    }
    #endregion

    #region [ LoadAssembly ]
    public static Assembly LoadAssembly(string assemblyName, bool skipOnError = DefaultSkipOnError)
    {
      assemblyName.RequireNotEmpty("assemblyName");

      try
      {
        return Assembly.Load(assemblyName);
      }
      catch (Exception ex)
      {
        if (skipOnError && 
          (ex is FileNotFoundException || 
          ex is FileLoadException))
          return null;
        throw;
      }
    }
    #endregion

    public static IEnumerable<Assembly> SkipSystemAndDynamic(this IEnumerable<Assembly> assemblies)
    {
      if (assemblies == null) return Enumerable.Empty<Assembly>();

      return assemblies.Where(a => !a.IsSystemAssembly() && !a.IsDynamic);
    }

    public static bool IsSystemAssembly(this Assembly assembly)
    {
      return 
        assembly.IsProduct(MSCorLibProduct) ||
        assembly.IsCompany(MSCorLibCompany);
    }

    public static bool IsCompany(this Assembly assembly, string company)
    {
      assembly.RequireNotNull("assembly");
      company.RequireNotEmpty("company");

      return StringComparer.OrdinalIgnoreCase.Compare(assembly.GetCompany(), company) == 0;
    }
    public static bool IsProduct(this Assembly assembly, string product)
    {
      assembly.RequireNotNull("assembly");
      product.RequireNotEmpty("product");

      return StringComparer.OrdinalIgnoreCase.Compare(assembly.GetProduct(), product) == 0;
    }
  }
}
