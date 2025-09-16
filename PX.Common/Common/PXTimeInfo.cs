// Decompiled with JetBrains decompiler
// Type: PX.Common.PXTimeInfo
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;

#nullable disable
namespace PX.Common;

public sealed class PXTimeInfo
{
  private TimeSpan \u0002;
  private PXTimeZoneInfo \u000E;
  public static readonly PXTimeInfo Empty = new PXTimeInfo(DateTime.UtcNow, PXTimeZoneInfo.Invariant);

  public PXTimeInfo(PXTimeInfo source)
  {
    this.\u0002 = source.\u0002;
    this.\u000E = source.\u000E;
  }

  public PXTimeInfo(DateTime utcCurrent)
  {
    this.\u0002 = utcCurrent - DateTime.UtcNow;
    this.\u000E = LocaleInfo.GetTimeZone();
  }

  public PXTimeInfo(DateTime utcCurrent, PXTimeZoneInfo info)
  {
    this.\u0002 = utcCurrent - DateTime.UtcNow;
    this.\u000E = info;
  }

  public DateTime Now => DateTime.Now.AddTicks(this.\u0002.Ticks);

  public DateTime UtcNow => DateTime.UtcNow.AddTicks(this.\u0002.Ticks);

  public PXTimeZoneInfo Info => this.\u000E;

  public static string GetLongTime()
  {
    return string.Format("{0:dd MMM yyyy HH:mm tt} +0:00 GMT", (object) DateTime.UtcNow);
  }

  public static string GetDate() => $"{DateTime.UtcNow:dd-MMM-yy}";

  public static string GetShortTime()
  {
    return string.Format("{0:HH:mm tt} +0 GMT", (object) DateTime.UtcNow);
  }

  public bool Equals(PXTimeInfo other)
  {
    if (other == null)
      return false;
    return this == other || other.\u0002.Equals(this.\u0002);
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return !(obj.GetType() != typeof (PXTimeInfo)) && this.Equals((PXTimeInfo) obj);
  }

  public override int GetHashCode() => this.\u0002.GetHashCode();
}
