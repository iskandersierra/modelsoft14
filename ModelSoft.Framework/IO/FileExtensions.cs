using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework.IO
{
  public enum CopyFileCondition
  {
    CopyAlways,
    CopyIfNewer,
    DontCopy,
  }

  public static class FileExtensions
  {
    public static void BackupFile(string fileName, bool justLastContent = true)
    {
      if (!File.Exists(fileName))
        return;
      string backupFileName = fileName + ".bak";
      if (File.Exists(backupFileName))
      {
        if (justLastContent)
          File.Delete(backupFileName);
        else
        {
          for (int i = 0; ; i++)
          {
            string backupBackupFileName = backupFileName + "." + string.Format("{0:000}", i);
            if (!File.Exists(backupBackupFileName))
            {
              File.Move(backupFileName, backupBackupFileName);
              break;
            }
          }
        }
      }
      Contract.Assume(!File.Exists(backupFileName));
      File.Copy(fileName, backupFileName);
      Contract.Assume(File.Exists(backupFileName));
    }

    public static string PathCombine(this string startPath, params string[] paths)
    {
      if (startPath == null) throw new ArgumentNullException("startPath");
      if (paths == null) throw new ArgumentNullException("paths");
      if (paths.Any(p => p == null)) throw new ArgumentNullException("paths");

      return paths.Aggregate(startPath, Path.Combine);
    }

    public static string SetTrailingBackslash(this string path, bool setTrailingBackslash = true)
    {
      if (setTrailingBackslash)
      {
        if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
          return path + Path.DirectorySeparatorChar;
      }
      else
      {
        if (path.EndsWith(Path.DirectorySeparatorChar.ToString()))
          return path.Substring(0, path.Length - 1);
      }
      return path;
    }

    public static bool CopyFile(this string sourceFileName, string targetFileName, CopyFileCondition copyCondition = CopyFileCondition.CopyAlways, bool backupTargetFile = false, bool throwOnError = false, bool createFolder = true)
    {
      if (sourceFileName == null) throw new ArgumentNullException("sourceFileName");
      if (targetFileName == null) throw new ArgumentNullException("targetFileName");
      string errorMessage = null;
      bool result = false;
      var source = new FileInfo(sourceFileName);
      if (!source.Exists)
        errorMessage = "Source file does not exists";
      else
      {
        var directoryName = Path.GetDirectoryName(Path.GetFullPath(targetFileName));
        if (!Directory.Exists(directoryName))
          if (createFolder)
            Directory.CreateDirectory(directoryName);
          else
            errorMessage = "Could not create target folder";

        if (errorMessage == null)
          switch (copyCondition)
          {
            case CopyFileCondition.CopyAlways:
              if (backupTargetFile)
                BackupFile(targetFileName);
              File.Copy(sourceFileName, targetFileName);
              result = true;
              break;
            case CopyFileCondition.CopyIfNewer:
              var target = new FileInfo(targetFileName);
              if (target.LastWriteTime < source.LastWriteTime)
              {
                if (backupTargetFile)
                  BackupFile(targetFileName);
                File.Copy(sourceFileName, targetFileName);
              }
              result = true;
              break;
            case CopyFileCondition.DontCopy:
              result = true;
              break;
            default:
              errorMessage = "Unknown copy file condition";
              break;
          }
      }
      if (throwOnError && errorMessage != null)
        throw new InvalidOperationException(errorMessage);
      return result;
    }

    public static bool CopyDirectory(this string sourceFolder, string targetFolder, CopyFileCondition copyCondition = CopyFileCondition.CopyAlways, bool backupTargetFile = false, bool throwOnError = false)
    {
      if (sourceFolder == null) throw new ArgumentNullException("sourceFolder");
      if (targetFolder == null) throw new ArgumentNullException("targetFolder");
      string errorMessage = null;
      bool result = false;
      var source = new DirectoryInfo(sourceFolder);
      if (!source.Exists)
        errorMessage = "Source folder does not exists";
      else
      {
        if (!Directory.Exists(targetFolder))
          Directory.CreateDirectory(targetFolder);
        foreach (var fileInfo in source.GetFiles())
          CopyFile(fileInfo.FullName, Path.Combine(targetFolder, Path.GetFileName(fileInfo.Name)), copyCondition,
                   backupTargetFile, throwOnError);
        foreach (var directoryInfo in source.GetDirectories())
          CopyDirectory(directoryInfo.FullName, Path.Combine(targetFolder, Path.GetFileName(directoryInfo.Name)),
                        copyCondition, backupTargetFile, throwOnError);
      }
      if (throwOnError && errorMessage != null)
        throw new InvalidOperationException(errorMessage);
      return result;
    }
  }
}
