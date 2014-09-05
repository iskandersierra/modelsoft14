using System;
using System.IO;

namespace ModelSoft.Framework
{
  public class TemporaryFile : IDisposable
  {
    public TemporaryFile()
    {
      FileName = Path.GetTempFileName();
      IsDisposed = false;
    }

    public TemporaryFile(string fileName)
    {
      FileName = fileName;
      IsDisposed = !File.Exists(FileName);
    }

    public string FileName { get; private set; }

    public bool IsDisposed { get; private set; }

    public Stream OpenRead()
    {
      return File.OpenRead(FileName);
    }

    public Stream OpenWrite()
    {
      return File.Create(FileName);
    }

    public void Dispose()
    {
      if (!IsDisposed && File.Exists(FileName))
      {
        File.Delete(FileName);
        IsDisposed = true;
      }
    }
  }
}
