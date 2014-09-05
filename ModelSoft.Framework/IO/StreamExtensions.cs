using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ModelSoft.Framework.IO
{
  public static class StreamExtensions
  {
    public static void CopyTo(this Stream stream, Stream target, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (stream == null) throw new ArgumentNullException("stream");
      if (!stream.CanRead) throw new ObjectDisposedException("stream", "Stream has been disposed");
      if (target == null) throw new ArgumentNullException("target");
      if (!target.CanWrite) throw new ObjectDisposedException("target", "Stream has been disposed");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      target.CopyFromInternal(stream, maxAmount, bufferLength);
    }
    public static void CopyTo(this Stream stream, string targetFileName, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (stream == null) throw new ArgumentNullException("stream");
      if (!stream.CanRead) throw new ObjectDisposedException("stream", "Stream has been disposed");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      using (var target = File.OpenWrite(targetFileName))
      {
        target.CopyFromInternal(stream, maxAmount, bufferLength);
      }
    }
    public static void CopyTo(this Stream stream, Uri targetUri, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (stream == null) throw new ArgumentNullException("stream");
      if (!stream.CanRead) throw new ObjectDisposedException("stream", "Stream has been disposed");
      if (targetUri == null) throw new ArgumentNullException("targetUri");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      var client = new WebClient();
      using (var target = client.OpenWrite(targetUri))
      {
        target.CopyFromInternal(stream, maxAmount, bufferLength);
      }
    }

    public static void CopyFrom(this Stream stream, Stream source, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (stream == null) throw new ArgumentNullException("stream");
      if (!stream.CanWrite) throw new ObjectDisposedException("stream", "Stream has been disposed");
      if (source == null) throw new ArgumentNullException("source");
      if (!source.CanRead) throw new ObjectDisposedException("source", "Stream has been disposed");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      stream.CopyFromInternal(source, maxAmount, bufferLength);
    }
    public static void CopyFrom(this Stream stream, string sourceFileName, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (stream == null) throw new ArgumentNullException("stream");
      if (!stream.CanWrite) throw new ObjectDisposedException("stream", "Stream has been disposed");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      using (var source = File.OpenRead(sourceFileName))
      {
        stream.CopyFromInternal(source, maxAmount, bufferLength);
      }
    }
    public static void CopyFrom(this Stream stream, Uri sourceUri, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (stream == null) throw new ArgumentNullException("stream");
      if (!stream.CanWrite) throw new ObjectDisposedException("stream", "Stream has been disposed");
      if (sourceUri == null) throw new ArgumentNullException("sourceUri");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      var client = new WebClient();
      using (var source = client.OpenRead(sourceUri))
      {
        stream.CopyFromInternal(source, maxAmount, bufferLength);
      }
    }
    private static void CopyFromInternal(this Stream stream, Stream source, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      var amount = 0;
      var buffer = new byte[Math.Min(maxAmount, bufferLength)];

      while (amount < maxAmount)
      {
        int read = source.Read(buffer, 0, Math.Min(maxAmount - amount, bufferLength));
        if (read <= 0) break;
        stream.Write(buffer, 0, read);
        amount += read;
      }
      stream.Flush();
    }

    public static void CopyTo(this TextReader reader, TextWriter target, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (reader == null) throw new ArgumentNullException("reader");
      if (target == null) throw new ArgumentNullException("target");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      target.CopyFromInternal(reader, maxAmount, bufferLength);
    }
    public static void CopyTo(this TextReader reader, string targetFileName, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (reader == null) throw new ArgumentNullException("reader");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      using (var target = new StreamWriter(File.OpenWrite(targetFileName)))
      {
        target.CopyFromInternal(reader, maxAmount, bufferLength);
      }
    }
    public static void CopyTo(this TextReader reader, Uri targetUri, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (reader == null) throw new ArgumentNullException("reader");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      var client = new WebClient();
      using (var target = new StreamWriter(client.OpenWrite(targetUri)))
      {
        target.CopyFromInternal(reader, maxAmount, bufferLength);
      }
    }

    public static void CopyFrom(this TextWriter writer, TextReader source, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (writer == null) throw new ArgumentNullException("writer");
      if (source == null) throw new ArgumentNullException("source");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      writer.CopyFromInternal(source, maxAmount, bufferLength);
    }
    public static void CopyFrom(this TextWriter writer, string sourceFileName, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (writer == null) throw new ArgumentNullException("writer");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      using (var source = File.OpenText(sourceFileName))
      {
        writer.CopyFromInternal(source, maxAmount, bufferLength);
      }
    }
    public static void CopyFrom(this TextWriter writer, Uri sourceUri, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (writer == null) throw new ArgumentNullException("writer");
      if (sourceUri == null) throw new ArgumentNullException("sourceUri");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return;

      var client = new WebClient();
      using (var source = new StreamReader(client.OpenRead(sourceUri)))
      {
        writer.CopyFromInternal(source, maxAmount, bufferLength);
      }
    }
    private static void CopyFromInternal(this TextWriter writer, TextReader source, int maxAmount, int bufferLength)
    {
      var amount = 0;
      var buffer = new char[Math.Min(maxAmount, bufferLength)];

      while (amount < maxAmount)
      {
        int read = source.Read(buffer, 0, Math.Min(maxAmount - amount, bufferLength));
        if (read <= 0) break;
        writer.Write(buffer, 0, read);
        amount += read;
      }
      writer.Flush();
    }

    public static byte[] ToArray(this Stream stream, int maxAmount = int.MaxValue, int bufferLength = 0x4000)
    {
      if (stream == null) throw new ArgumentNullException("stream");
      if (!stream.CanRead) throw new ObjectDisposedException("stream", "Stream has been disposed");
      if (maxAmount < 0) throw new ArgumentOutOfRangeException("maxAmount");
      if (bufferLength <= 0) throw new ArgumentNullException("bufferLength");
      if (maxAmount == 0) return new byte[0];
      using (var memory = new MemoryStream())
      {
        CopyTo(stream, memory, maxAmount, bufferLength);
        return memory.ToArray();
      }
    }
  }
}
