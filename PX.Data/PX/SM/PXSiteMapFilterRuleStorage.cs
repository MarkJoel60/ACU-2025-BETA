// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSiteMapFilterRuleStorage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

/// <exclude />
public static class PXSiteMapFilterRuleStorage
{
  private static List<Guid> _wikiList;

  private static List<Guid> WikiList
  {
    get
    {
      if (PXSiteMapFilterRuleStorage._wikiList == null)
      {
        PXSiteMapFilterRuleStorage._wikiList = new List<Guid>();
        foreach (PXResult pxResult in new PXSelect<WikiDescriptor>(new PXGraph()).Select())
        {
          if (pxResult[typeof (WikiDescriptor)] is WikiDescriptor wikiDescriptor)
          {
            Guid? pageId = wikiDescriptor.PageID;
            if (pageId.HasValue)
            {
              List<Guid> wikiList = PXSiteMapFilterRuleStorage._wikiList;
              pageId = wikiDescriptor.PageID;
              Guid guid = pageId.Value;
              wikiList.Add(guid);
            }
          }
        }
      }
      return PXSiteMapFilterRuleStorage._wikiList;
    }
  }

  public static bool IsNotAccessible(PXSiteMapNode node)
  {
    return PXSiteMap.Provider.FindSiteMapNodeFromKey(node.NodeID) == null;
  }

  public static bool IsEmptyFolder(PXSiteMapNode node)
  {
    bool flag = string.IsNullOrEmpty(node.Url) || node.Url.ToLowerInvariant().Contains("~/Frames/Default.aspx".ToLowerInvariant());
    return (!flag || !node.HasChildNodes()) && flag;
  }

  public static bool IsWiki(PXSiteMapNode node)
  {
    return PXSiteMapFilterRuleStorage.WikiList.IndexOf(node.NodeID) != -1;
  }

  public static bool CantBeAutomated(PXSiteMapNode node)
  {
    bool flag1 = PXSiteMap.Provider.HasChildNodesThatCanBeAutomated(node);
    if (string.IsNullOrEmpty(node.Url))
      return !(node.HasChildNodes() & flag1);
    bool flag2 = node.Url.StartsWith("~");
    bool flag3 = node.Url.Contains("wiki.aspx");
    bool flag4 = node.Url.Contains("reportlauncher.aspx") || node.Url.Contains("rmlauncher.aspx");
    bool flag5 = !string.IsNullOrEmpty(node.Url) && !string.IsNullOrEmpty(node.Title) && !string.IsNullOrEmpty(node.ScreenID) && !string.IsNullOrEmpty(node.GraphType);
    bool flag6 = (!node.HasChildNodes() | flag5) & flag2 && !flag3 && !flag4;
    return (!node.HasChildNodes() || !flag1) && !flag6;
  }

  public static bool IsDashboard(PXSiteMapNode node) => PXSiteMap.IsDashboard(node.Url);

  public static bool IsGenericInquiry(PXSiteMapNode node) => PXSiteMap.IsGenericInquiry(node.Url);

  public static bool IsReport(PXSiteMapNode node) => PXSiteMap.IsReport(node.Url);

  public static bool HasCopyPaste(PXSiteMapNode node)
  {
    bool flag = false;
    if (node != null && !string.IsNullOrEmpty(node.GraphType))
    {
      System.Type type = GraphHelper.GetType(node.GraphType);
      if (type != (System.Type) null)
        flag = ((IEnumerable<System.Reflection.FieldInfo>) type.GetFields()).Any<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (field => field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof (PXCopyPasteAction<>)));
    }
    return flag;
  }
}
