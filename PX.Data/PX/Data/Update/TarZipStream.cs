// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.TarZipStream
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;

#nullable disable
namespace PX.Data.Update;

internal class TarZipStream : Stream
{
  private Stream _stream;
  private int _length;

  public TarZipStream(Stream stream) => this._stream = stream;

  protected override void Dispose(bool disposing) => this._stream.Dispose();

  public override bool CanRead => true;

  public override bool CanSeek => true;

  public override bool CanWrite => true;

  public override long Length => (long) this._length;

  public override long Position
  {
    get => (long) this._length;
    set => throw new NotImplementedException();
  }

  public override void Flush() => this._stream.Flush();

  public override void SetLength(long value) => throw new NotImplementedException();

  public override long Seek(long offset, SeekOrigin origin) => offset;

  public override int Read(byte[] buffer, int offset, int count)
  {
    return this._stream.Read(buffer, offset, count);
  }

  public override void Write(byte[] buffer, int offset, int count)
  {
    this._stream.Write(buffer, offset, count);
  }
}
