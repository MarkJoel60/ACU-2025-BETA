// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Storage.WrappedStream
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;

#nullable disable
namespace PX.Data.Update.Storage;

internal class WrappedStream : Stream
{
  private readonly Stream _baseStream;
  private bool _canRead;
  private bool _canSeek;
  private bool _canWrite;
  private readonly bool _closeBaseStream;
  private bool _isDisposed;
  private readonly System.Action _onClosed;

  internal WrappedStream(Stream baseStream, System.Action onClosed)
    : this(baseStream, true, true, true, onClosed)
  {
  }

  internal WrappedStream(
    Stream baseStream,
    bool canRead,
    bool canWrite,
    bool canSeek,
    System.Action onClosed)
    : this(baseStream, canRead, canWrite, canSeek, false, onClosed)
  {
  }

  internal WrappedStream(
    Stream baseStream,
    bool canRead,
    bool canWrite,
    bool canSeek,
    bool closeBaseStream,
    System.Action onClosed)
  {
    this._baseStream = baseStream;
    this._onClosed = onClosed;
    this._canRead = canRead;
    this._canSeek = canSeek;
    this._canWrite = canWrite;
    this._isDisposed = false;
    this._closeBaseStream = closeBaseStream;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && !this._isDisposed)
    {
      if (this._onClosed != null)
        this._onClosed();
      if (this._closeBaseStream)
        this._baseStream.Dispose();
      this._canRead = false;
      this._canWrite = false;
      this._canSeek = false;
      this._isDisposed = true;
    }
    base.Dispose(disposing);
  }

  public override void Flush()
  {
    this.ThrowIfDisposed();
    this.ThrowIfCantWrite();
    this._baseStream.Flush();
  }

  public override int Read(byte[] buffer, int offset, int count)
  {
    this.ThrowIfDisposed();
    this.ThrowIfCantRead();
    return this._baseStream.Read(buffer, offset, count);
  }

  public override long Seek(long offset, SeekOrigin origin)
  {
    this.ThrowIfDisposed();
    this.ThrowIfCantSeek();
    return this._baseStream.Seek(offset, origin);
  }

  public override void SetLength(long value)
  {
    this.ThrowIfDisposed();
    this.ThrowIfCantSeek();
    this.ThrowIfCantWrite();
    this._baseStream.SetLength(value);
  }

  private void ThrowIfCantRead()
  {
    if (!this.CanWrite)
      throw new NotSupportedException("WritingNotSupported");
  }

  private void ThrowIfCantSeek()
  {
    if (!this.CanSeek)
      throw new NotSupportedException("SeekingNotSupported");
  }

  private void ThrowIfCantWrite()
  {
    if (!this.CanWrite)
      throw new NotSupportedException("WritingNotSupported");
  }

  private void ThrowIfDisposed()
  {
    if (this._isDisposed)
      throw new ObjectDisposedException(this.GetType().Name, "HiddenStreamName");
  }

  public override void Write(byte[] buffer, int offset, int count)
  {
    this.ThrowIfDisposed();
    this.ThrowIfCantWrite();
    this._baseStream.Write(buffer, offset, count);
  }

  public override bool CanRead => this._canRead && this._baseStream.CanRead;

  public override bool CanSeek => this._canSeek && this._baseStream.CanSeek;

  public override bool CanWrite => this._canWrite && this._baseStream.CanWrite;

  public override long Length
  {
    get
    {
      this.ThrowIfDisposed();
      return this._baseStream.Length;
    }
  }

  public override long Position
  {
    get
    {
      this.ThrowIfDisposed();
      return this._baseStream.Position;
    }
    set
    {
      this.ThrowIfDisposed();
      this.ThrowIfCantSeek();
      this._baseStream.Position = value;
    }
  }
}
