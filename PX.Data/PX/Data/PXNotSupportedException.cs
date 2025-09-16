// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNotSupportedException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// Derived from PXException. The exception that is thrown when an invoked method is not supported, or when there is an attempt to read, seek, or write to a stream that does not support the invoked functionality.
/// </summary>
[Serializable]
public class PXNotSupportedException : PXException
{
  public PXNotSupportedException() => this.HResult = -2146233067;

  public PXNotSupportedException(string message)
    : base(message)
  {
    this.HResult = -2146233067;
  }

  public PXNotSupportedException(string format, params object[] args)
    : base(format, args)
  {
    this.HResult = -2146233067;
  }

  public PXNotSupportedException(string message, Exception innerException)
    : base(message, innerException)
  {
    this.HResult = -2146233067;
  }

  public PXNotSupportedException(Exception innerException, string format, params object[] args)
    : base(innerException, format, args)
  {
    this.HResult = -2146233067;
  }

  public PXNotSupportedException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2146233067;
  }
}
