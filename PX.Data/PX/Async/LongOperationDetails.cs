// Decompiled with JetBrains decompiler
// Type: PX.Async.LongOperationDetails
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable enable
namespace PX.Async;

public sealed class LongOperationDetails
{
  internal LongOperationDetails(
    PXLongRunStatus status,
    TimeSpan duration,
    Exception? message,
    bool isRedirected)
  {
    this.Status = status;
    this.Duration = duration;
    this.Message = message;
    this.IsRedirected = isRedirected;
  }

  /// <summary>The status of the long-running operation.</summary>
  public PXLongRunStatus Status { get; }

  /// <summary>The duration of the long-running operation.</summary>
  public TimeSpan Duration { get; }

  /// <summary>The status message of the long-running operation.</summary>
  public Exception? Message { get; }

  /// <summary> An indicator of whether the status message is of the <see cref="T:PX.Data.PXBaseRedirectException" /> exception type.</summary>
  public bool IsRedirected { get; }

  public void Deconstruct(
    out PXLongRunStatus status,
    out TimeSpan duration,
    out Exception? message,
    out bool isRedirected)
  {
    status = this.Status;
    duration = this.Duration;
    message = this.Message;
    isRedirected = this.IsRedirected;
  }
}
