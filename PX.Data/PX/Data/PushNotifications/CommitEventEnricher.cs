// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.CommitEventEnricher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Globalization;

#nullable disable
namespace PX.Data.PushNotifications;

internal class CommitEventEnricher : ICommitEventEnricher
{
  internal const string PXPerformanceInfoStartTime = "PXPerformanceInfoStartTime";

  public void Enrich(IQueueEvent commitEvent)
  {
    commitEvent.AdditionalInfo["PXPerformanceInfoStartTime"] = (object) PXPerformanceMonitor.CurrentSample?.StartTime.ToString((IFormatProvider) CultureInfo.InvariantCulture);
  }
}
