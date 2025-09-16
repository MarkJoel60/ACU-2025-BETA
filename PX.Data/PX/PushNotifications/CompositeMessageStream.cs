// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.CompositeMessageStream
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;

#nullable disable
namespace PX.PushNotifications;

internal class CompositeMessageStream : Stream
{
  private readonly List<Message> _innerMessages = new List<Message>(4);
  private const int MaxBlockCapacity = 4000000;
  internal const string Split = "SPLIT";
  internal const string Finalsplit = "FINALSPLIT";
  private long _length;
  private readonly string _correlationId;
  private readonly TimeSpan _timeToBeReceived;

  private long _currentStreamId => this.Position / 4000000L;

  private Stream currentStream
  {
    get
    {
      while ((long) this._innerMessages.Count <= this._currentStreamId)
        this._innerMessages.Add(new Message()
        {
          Label = "SPLIT",
          CorrelationId = this._correlationId,
          TimeToBeReceived = this._timeToBeReceived,
          BodyStream = (Stream) new MemoryStream()
        });
      return this._innerMessages[(int) this._currentStreamId].BodyStream;
    }
  }

  private int currentOffset => (int) this.currentStream.Position;

  public CompositeMessageStream(List<Message> messages)
  {
    this._innerMessages = messages;
    this._correlationId = messages[0].CorrelationId;
    this._length = this._innerMessages.Select<Message, long>((Func<Message, long>) (c => c.BodyStream.Length)).Sum();
  }

  public CompositeMessageStream(Message message)
  {
    this._correlationId = message.CorrelationId;
    this._timeToBeReceived = message.TimeToBeReceived;
    this._innerMessages.Add(message);
  }

  public override void Flush()
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._innerMessages.Count > 1)
      {
        string label = this._innerMessages[this._innerMessages.Count - 1].Label;
        this._innerMessages[this._innerMessages.Count - 1].Label = label == "SPLIT" ? "FINALSPLIT" : label;
      }
      else
        this._innerMessages.Add(new Message((object) "", (IMessageFormatter) new BinaryMessageFormatter())
        {
          Label = "FINALSPLIT",
          CorrelationId = this._correlationId,
          TimeToBeReceived = this._timeToBeReceived,
          BodyStream = (Stream) new MemoryStream()
        });
    }
    base.Dispose(disposing);
  }

  public override long Seek(long offset, SeekOrigin origin)
  {
    switch (origin)
    {
      case SeekOrigin.Begin:
        this.Position = offset;
        break;
      case SeekOrigin.Current:
        this.Position += offset;
        break;
      case SeekOrigin.End:
        this.Position = this.Length - offset;
        break;
    }
    return this.Position;
  }

  public override void SetLength(long value) => this._length = value;

  public override int Read(byte[] buffer, int offset, int count)
  {
    int num1 = count;
    if (num1 < 0)
      throw new ArgumentOutOfRangeException(nameof (count), (object) num1, "The number of bytes to copy cannot be negative.");
    long num2 = this._length - this.Position;
    if ((long) num1 > num2)
      num1 = (int) num2;
    if (buffer == null)
      throw new ArgumentNullException(nameof (buffer), "The buffer cannot be null.");
    if (offset < 0)
      throw new ArgumentOutOfRangeException(nameof (offset), (object) offset, "The destination offset cannot be negative.");
    int num3 = 0;
    do
    {
      int count1 = System.Math.Min(num1, 4000000 - this.currentOffset);
      this.currentStream.Read(buffer, offset, count1);
      num1 -= count1;
      offset += count1;
      num3 += count1;
      this.Position += (long) count1;
    }
    while (num1 > 0);
    return num3;
  }

  public override void Write(byte[] buffer, int offset, int count)
  {
    long position = this.Position;
    try
    {
      do
      {
        int count1 = System.Math.Min(count, 4000000 - this.currentOffset);
        this.EnsureCapacity(this.Position + (long) count1);
        this.currentStream.Write(buffer, offset, count1);
        count -= count1;
        offset += count1;
        this.Position += (long) count1;
      }
      while (count > 0);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      this.Position = position;
      throw;
    }
  }

  public override bool CanRead => true;

  public override bool CanSeek => true;

  public override bool CanWrite => true;

  public override long Length => this._length;

  public override long Position { get; set; }

  private void EnsureCapacity(long intended_length)
  {
    if (intended_length <= this._length)
      return;
    this._length = intended_length;
  }

  public IEnumerable<Message> GetMessages()
  {
    return (IEnumerable<Message>) this._innerMessages.Where<Message>((Func<Message, bool>) (c => c.BodyStream.Length > 0L || c.Label == "FINALSPLIT")).ToArray<Message>();
  }
}
