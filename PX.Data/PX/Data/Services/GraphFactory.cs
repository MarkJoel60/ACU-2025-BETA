// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.GraphFactory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.Services.Interfaces;
using PX.Metadata;
using System.Threading;

#nullable disable
namespace PX.Data.Services;

internal class GraphFactory : IGraphFactory
{
  public PXGraph Create(string screenId)
  {
    System.Type graphType = SyImportProcessor.GetGraphType(PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenId).GraphType);
    PXGraph pxGraph = (PXGraph) null;
    if (graphType != (System.Type) null)
      pxGraph = !(graphType == typeof (PXGenericInqGrph)) || string.IsNullOrEmpty(screenId) ? PXGraph.CreateInstance(graphType) : (PXGraph) PXGenericInqGrph.CreateInstance(screenId);
    if (pxGraph == null)
      throw new PXException("A graph cannot be created.");
    pxGraph.Culture = Thread.CurrentThread.CurrentCulture;
    pxGraph.UnattendedMode = false;
    return pxGraph;
  }

  public PXSiteMap.ScreenInfo GetScreenInfo(string screenId)
  {
    return ScreenUtils.ScreenInfo.Get(screenId);
  }
}
