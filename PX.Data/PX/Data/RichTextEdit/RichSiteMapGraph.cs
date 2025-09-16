// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.RichSiteMapGraph
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Data.RichTextEdit;

/// <exclude />
[Serializable]
public class RichSiteMapGraph : PXGraph<RichSiteMapGraph>
{
  public PXSelect<SiteMapEntry> AllScreens;

  public IEnumerable allscreens()
  {
    return (IEnumerable) PXSiteMap.Provider.Definitions.Nodes.Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (node => !string.IsNullOrEmpty(node.ScreenID))).Select<PXSiteMapNode, SiteMapEntry>((Func<PXSiteMapNode, SiteMapEntry>) (node => new SiteMapEntry(node)));
  }
}
