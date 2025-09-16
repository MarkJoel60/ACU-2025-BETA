// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUTCTimeZoneScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

public sealed class PXUTCTimeZoneScope : IDisposable
{
  private readonly PXTimeZoneInfo prevTimeZone;

  public PXUTCTimeZoneScope()
  {
    this.prevTimeZone = LocaleInfo.GetTimeZone();
    LocaleInfo.SetTimeZone(PXTimeZoneInfo.FindSystemTimeZoneById("GMTE0000U"));
  }

  void IDisposable.Dispose() => LocaleInfo.SetTimeZone(this.prevTimeZone);
}
