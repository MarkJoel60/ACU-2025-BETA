// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeEvent
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Diagnostics.Tracing;

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeEvent : PXExchangeItemBase
{
  public EventLevel Level;
  public string Message;
  public Exception Error;
  public string[] Details;
  public System.DateTime Date;

  public PXExchangeEvent(string mailbox, string message, Exception error = null)
    : this(mailbox, EventLevel.Error, message, error)
  {
  }

  public PXExchangeEvent(
    string mailbox,
    EventLevel level,
    string message,
    Exception error,
    string[] details = null)
    : base(mailbox)
  {
    this.Level = level;
    this.Error = error;
    this.Message = message;
    this.Details = details;
    this.Date = System.DateTime.UtcNow;
  }
}
