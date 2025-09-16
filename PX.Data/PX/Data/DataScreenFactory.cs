// Decompiled with JetBrains decompiler
// Type: PX.Data.DataScreenFactory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class DataScreenFactory : IDataScreenFactory
{
  public DataScreenBase CreateDataScreen(string screenID)
  {
    return this.CreateDataScreen(screenID, (PXGraph) null);
  }

  public DataScreenBase CreateDataScreen(string screenID, PXGraph graph)
  {
    PXSiteMapNode pxSiteMapNode = !string.IsNullOrEmpty(screenID) ? PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID) : throw new ArgumentNullException(nameof (screenID));
    if (pxSiteMapNode == null)
      throw new PXScreenNotFoundException(screenID);
    if (graph != null && !string.IsNullOrEmpty(pxSiteMapNode.GraphType) && !string.Equals(pxSiteMapNode.GraphType, CustomizedTypeManager.GetTypeNotCustomized(graph).FullName))
      throw new InvalidOperationException($"Graph type mismatch for ScreenID = '{screenID}'. Expected: {pxSiteMapNode.GraphType}, passed: {graph.GetType().FullName}");
    return Str.Contains(pxSiteMapNode.Url, "~/GenericInquiry/GenericInquiry.aspx", StringComparison.OrdinalIgnoreCase) || Str.Contains(pxSiteMapNode.Url, "~/Scripts/Screens/GenericInquiry.html", StringComparison.OrdinalIgnoreCase) ? (DataScreenBase) new GIDataScreen(screenID, graph) : (DataScreenBase) new DataScreen(screenID, graph);
  }
}
