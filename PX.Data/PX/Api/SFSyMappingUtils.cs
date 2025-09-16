// Decompiled with JetBrains decompiler
// Type: PX.Api.SFSyMappingUtils
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Translation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

#nullable disable
namespace PX.Api;

[Obsolete("This class will be removed in 2018R1")]
[PXInternalUseOnly]
public static class SFSyMappingUtils
{
  public static int ImportPreparedData(
    SYProcess graph,
    SYMapping mapping,
    SYImportOperation operation,
    PXSYTable preparedData,
    CancellationToken token)
  {
    return SyMappingUtils.ImportPreparedData(graph, mapping, operation, preparedData, StringComparer.Ordinal, token);
  }

  public static int ImportPreparedData(
    SYProcess graph,
    SYMapping mapping,
    SYImportOperation operation,
    PXSYTable preparedData)
  {
    return SFSyMappingUtils.ImportPreparedData(graph, mapping, operation, preparedData, CancellationToken.None);
  }

  public static PXSYTable ExportPreparedData(
    SYProcess graph,
    SYMapping mapping,
    bool breakOnError,
    bool newApi,
    CancellationToken token)
  {
    return SyMappingUtils.ExportPreparedData(graph, mapping, breakOnError, newApi, token);
  }

  public static PXSYTable ExportPreparedData(
    SYProcess graph,
    SYMapping mapping,
    bool breakOnError,
    bool newApi)
  {
    return SFSyMappingUtils.ExportPreparedData(graph, mapping, breakOnError, newApi, CancellationToken.None);
  }

  public static Dictionary<string, string> ReadPreparedDataKeys(
    PXGraph sourceGraph,
    string gridView,
    SYData row)
  {
    return SyMappingUtils.ReadPreparedDataKeys(sourceGraph, gridView, row);
  }

  public static int WritePreparedDataToProvider(
    SYProcess graph,
    SYMapping mapping,
    SYImportOperation operation,
    PXSYTable preparedData)
  {
    return SyMappingUtils.WritePreparedDataToProvider(graph, mapping, operation, preparedData);
  }

  public static IPXSYProvider GetProvider(PXGraph graph, SYMapping mapping, PXSYParameter[] input)
  {
    return SyMappingUtils.GetProvider(graph, mapping, input);
  }

  public static void LoadProviderFilters(
    SYProcess graph,
    SYMapping mapping,
    List<SYProviderField> sources,
    List<PXSYFilterRow> filters)
  {
    SyMappingUtils.LoadProviderFilters(graph, mapping, (IList<SYProviderField>) sources, filters);
  }

  public static SYImportOperation GetPrepareAndProcessOperation(Guid? mappingID)
  {
    return SyMappingUtils.GetPrepareAndProcessOperation(mappingID);
  }

  public static PXSYSyncTypes GetMappingSyncType(Guid mappingID, string unknownSyncTypeMessage)
  {
    SYMapping syMapping = (SYMapping) PXSelectBase<SYMapping, PXSelect<SYMapping, Where<SYMapping.mappingID, Equal<Required<SYMapping.mappingID>>>>.Config>.Select(new PXGraph(), (object) mappingID);
    try
    {
      return SyMappingUtils.ConvertSyncType(syMapping.SyncType);
    }
    catch (PXException ex)
    {
      throw new PXException(string.Format(unknownSyncTypeMessage, (object) syMapping.Name));
    }
  }

  public sealed class ProcessMappingScope : IDisposable
  {
    private CultureInfo prev;
    private CultureInfo prevUI;

    public ProcessMappingScope(SYProcess graph, SYMapping mapping)
    {
      this.prev = Thread.CurrentThread.CurrentCulture;
      this.prevUI = Thread.CurrentThread.CurrentUICulture;
      SyMappingUtils.ProcessMappingInit(graph, mapping);
    }

    public void Dispose()
    {
      Thread.CurrentThread.CurrentCulture = this.prev;
      Thread.CurrentThread.CurrentUICulture = this.prevUI;
      PXContext.SetSlot<PXDictionaryManager>((PXDictionaryManager) null);
      PXLocalizer.Localize("Explicit", typeof (InfoMessages).FullName);
    }
  }

  public class SFSyProviderInstance
  {
    public static object Provider
    {
      get => SyProviderInstance.Provider;
      set => SyProviderInstance.Provider = value;
    }
  }
}
