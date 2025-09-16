// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeedImportException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BankFeed.Common;
using PX.Common;
using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.CA;

public class BankFeedImportException : PXException
{
  public BankFeedImportException.ExceptionReason Reason { get; private set; }

  public DateTime ErrorTime { get; private set; }

  public BankFeedImportException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public BankFeedImportException(string message)
    : base(message)
  {
    this.ErrorTime = PXTimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, LocaleInfo.GetTimeZone());
  }

  public BankFeedImportException(string message, BankFeedException ex)
    : base(message, (Exception) ex)
  {
    if (ex.Reason == 1)
      this.Reason = BankFeedImportException.ExceptionReason.LoginFailed;
    this.ErrorTime = PXTimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, LocaleInfo.GetTimeZone());
  }

  public BankFeedImportException(string message, BankFeedImportException.ExceptionReason reason)
    : base(message)
  {
    if (reason == BankFeedImportException.ExceptionReason.LoginFailed)
      this.Reason = BankFeedImportException.ExceptionReason.LoginFailed;
    this.ErrorTime = PXTimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, LocaleInfo.GetTimeZone());
  }

  public enum ExceptionReason
  {
    Error,
    LoginFailed,
  }
}
