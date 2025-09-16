// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Services.SchedulerSession
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;

#nullable disable
namespace PX.Objects.FS.Services;

public class SchedulerSession : PXSessionState
{
  internal PXSessionState.Indexer<SchedulerDatesFilter> PeriodInfos = new PXSessionState.Indexer<SchedulerDatesFilter>();
}
