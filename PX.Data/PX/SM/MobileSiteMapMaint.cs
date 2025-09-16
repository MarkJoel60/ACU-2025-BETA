// Decompiled with JetBrains decompiler
// Type: PX.SM.MobileSiteMapMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public class MobileSiteMapMaint : PXGraph<MobileSiteMapMaint>
{
  public PXSelect<MobileSiteMapEntry> SiteMap;

  protected IEnumerable siteMap()
  {
    List<MobileSiteMapEntry> accumulator = new List<MobileSiteMapEntry>();
    this.Walk(PXSiteMap.RootNode, accumulator, 0);
    return (IEnumerable) accumulator;
  }

  private void Walk(PXSiteMapNode node, List<MobileSiteMapEntry> accumulator, int currentIndent)
  {
    accumulator.Add(this.Map(node, currentIndent));
    if (!node.HasChildNodes())
      return;
    IList<PXSiteMapNode> childNodes = node.ChildNodes;
    if (childNodes.Count == 1 && string.IsNullOrEmpty(childNodes[0].GraphType))
    {
      if (!childNodes[0].HasChildNodes())
        return;
      childNodes = childNodes[0].ChildNodes;
    }
    foreach (PXSiteMapNode node1 in (IEnumerable<PXSiteMapNode>) childNodes)
      this.Walk(node1, accumulator, currentIndent + 1);
  }

  private MobileSiteMapEntry Map(PXSiteMapNode node, int currentIndent)
  {
    return new MobileSiteMapEntry()
    {
      ScreenID = node.ScreenID,
      Title = node.Title,
      Url = node.Url,
      Indent = new int?(currentIndent)
    };
  }
}
