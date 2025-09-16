// Decompiled with JetBrains decompiler
// Type: PX.SM.CurrentUserSiteMapMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public class CurrentUserSiteMapMaint : PXGraph<CurrentUserSiteMapMaint>
{
  public PXCancel<SiteMapCurrentUserEntry> Cancel;
  [PXFilterable(new System.Type[] {})]
  public PXSelectReadonly3<SiteMapCurrentUserEntry, OrderBy<Asc<SiteMapCurrentUserEntry.screenID>>> SiteMap;

  protected IEnumerable siteMap()
  {
    List<SiteMapCurrentUserEntry> list = new List<SiteMapCurrentUserEntry>();
    int index = 0;
    this.EnumSiteMap(PXSiteMap.RootNode, list, ref index);
    return (IEnumerable) list;
  }

  private void EnumSiteMap(PXSiteMapNode node, List<SiteMapCurrentUserEntry> list, ref int index)
  {
    ++index;
    if (node.IsAccessibleToUser() && !PXList.Provider.IsList(node.ScreenID))
      list.Add(this.CreateSiteMapEntry(node, index));
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
      this.EnumSiteMap(node1, list, ref index);
  }

  private SiteMapCurrentUserEntry CreateSiteMapEntry(PXSiteMapNode node, int index)
  {
    return new SiteMapCurrentUserEntry()
    {
      ScreenID = node.ScreenID,
      Title = node.Title,
      Url = node.Url,
      OrderIndex = new int?(index),
      AccessRights = new int?((int) PXSiteMap.Provider.GetAccessRights(node.ScreenID, node))
    };
  }
}
