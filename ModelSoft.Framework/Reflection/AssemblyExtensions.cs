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
    #region [ GetPath / GetFolder ]
    public static string GetEntryAssemblyPath()
    {
      var result = Assembly.GetEntryAssembly().GetPath();
      return result;
    }
    public static string GetEntryAssemblyFolder()
    {
      var result = Assembly.GetEntryAssembly().GetFolder();
      return result;
    }

    public static string GetExecutingAssemblyPath()
    {
      var result = Assembly.GetExecutingAssembly().GetPath();
      return result;
    }
    public static string GetExecutingAssemblyFolder()
    {
      var result = Assembly.GetExecutingAssembly().GetFolder();
      return result;
    }

    public static string GetAssemblyPath(this Type type)
    {
      type.RequireNotNull("type");

      var result = type.Assembly.GetPath();
      return result;
    }
    public static string GetAssemblyFolder(this Type type)
    {
      type.RequireNotNull("type");

      var result = type.Assembly.GetFolder();
      return result;
    }

    public static string GetPath(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");

      var codeBase = assembly.CodeBase;
      var localPath = new Uri(codeBase).LocalPath;
      return localPath;
    }
    public static string GetFolder(this Assembly assembly)
    {
      assembly.RequireNotNull("assembly");

      var path = assembly.GetPath();
      var result = Path.GetDirectoryName(path);
      return result;
    }
    #endregion
  }
}
