// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSessionStatePXData
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Common;
using PX.Data.Search;
using PX.SM;
using PX.Translation;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public class PXSessionStatePXData : PXSessionState
{
  [PXInternalUseOnly]
  public PXSessionState.Indexer<PX.Data.Process.AuditInfo> AuditInfo = new PXSessionState.Indexer<PX.Data.Process.AuditInfo>();
  public PXSessionState.Indexer<PX.SM.FileInfo> FileInfo = new PXSessionState.Indexer<PX.SM.FileInfo>();
  internal PXSessionState.Indexer<PXTrace.Event> PXTraceEvent = new PXSessionState.Indexer<PXTrace.Event>();
  internal PXSessionState.Indexer<Dictionary<string, LocalizationRecord>> LocalizationGridParent = new PXSessionState.Indexer<Dictionary<string, LocalizationRecord>>();
  internal PXSessionState.Indexer<Dictionary<string, LocalizationRecordObsolete>> LocalizationGridParentObsolete = new PXSessionState.Indexer<Dictionary<string, LocalizationRecordObsolete>>();
  internal PXSessionState.Indexer<List<WikiPage>> selectedArticles = new PXSessionState.Indexer<List<WikiPage>>();
  internal PXSessionState.Indexer<Content> ScreenGateSchema = new PXSessionState.Indexer<Content>();
  internal PXSessionState.Indexer<List<Command>> SubmitFieldCommands = new PXSessionState.Indexer<List<Command>>();
  internal PXSessionState.Indexer<CacheResult> SearchCacheResult = new PXSessionState.Indexer<CacheResult>();
  internal PXSessionState.Indexer<IPXResultset> DrilldownReport = new PXSessionState.Indexer<IPXResultset>();
  internal PXSessionState.Indexer<GenericResult> GIEditCurrent = new PXSessionState.Indexer<GenericResult>();
  internal PXSessionState.PrefixedIndexer<GenericResult> GITotalCurrent = new PXSessionState.PrefixedIndexer<GenericResult>("Total");

  internal PXReusableGraphInfo ReusableGraphInfo
  {
    get => (PXReusableGraphInfo) PXSessionState.GetValue(nameof (ReusableGraphInfo));
    set => PXSessionState.SetValue(nameof (ReusableGraphInfo), (object) value);
  }
}
