// Decompiled with JetBrains decompiler
// Type: PX.Data.AccessInfoExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

public static class AccessInfoExtension
{
  public static System.DateTime? BusinessDateWithTime(this AccessInfo _)
  {
    System.DateTime? businessDate = PXContext.GetBusinessDate();
    if (!businessDate.HasValue)
      return new System.DateTime?(PXTimeZoneInfo.Today.Date + PXTimeZoneInfo.Now.TimeOfDay);
    if (!(businessDate.Value.TimeOfDay == TimeSpan.Zero))
      return businessDate;
    System.DateTime? nullable = businessDate;
    TimeSpan timeOfDay = PXTimeZoneInfo.Now.TimeOfDay;
    return !nullable.HasValue ? new System.DateTime?() : new System.DateTime?(nullable.GetValueOrDefault() + timeOfDay);
  }
}
