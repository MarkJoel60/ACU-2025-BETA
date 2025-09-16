// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Services.ImportService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Metadata;

#nullable disable
namespace PX.Data.Api.Services;

[PXInternalUseOnly]
public class ImportService : IImportService
{
  private readonly IScreenInfoProvider _screenInfoProvider;

  public ImportService(IScreenInfoProvider screenInfoProvider)
  {
    this._screenInfoProvider = screenInfoProvider;
  }

  public PXGraph CreateGraphForMobile(string screenId)
  {
    return this.CreateGraphForMobile(this._screenInfoProvider.GetWithInvariantLocale(screenId).GraphName, screenId);
  }

  public PXGraph CreateGraphForMobile(string graphName, string screenId)
  {
    return SyImportProcessor.CreateGraph(graphName, screenId);
  }

  public PXGraph CreateGraphForMobile(string graphName, string screenId, string prefix)
  {
    if (graphName == null)
      graphName = this._screenInfoProvider.GetWithInvariantLocale(screenId).GraphName;
    return SyImportProcessor.CreateGraphWithPrefix(graphName, prefix, screenId);
  }
}
