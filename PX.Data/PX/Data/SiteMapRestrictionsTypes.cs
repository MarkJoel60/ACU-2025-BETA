// Decompiled with JetBrains decompiler
// Type: PX.Data.SiteMapRestrictionsTypes
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

public static class SiteMapRestrictionsTypes
{
  public static bool IsDashboardInquiry(PX.SM.SiteMap sitemap)
  {
    if (sitemap.Graphtype != null)
    {
      System.Type type = PXBuildManager.GetType(sitemap.Graphtype, false);
      if (type != (System.Type) null && Attribute.GetCustomAttribute((MemberInfo) type, typeof (DashboardTypeAttribute), true) is DashboardTypeAttribute customAttribute && (customAttribute.Types.Length == 0 || customAttribute.Types.Length > 1 || customAttribute.Types.Length == 1 && customAttribute.Types[0] != 1))
        return true;
    }
    return false;
  }

  public static bool IsProcessing(PX.SM.SiteMap siteMap)
  {
    if (siteMap.Graphtype == null)
      return false;
    System.Type type = PXBuildManager.GetType(siteMap.Graphtype, false);
    return !(type == (System.Type) null) && ((IEnumerable<System.Reflection.FieldInfo>) type.GetFields()).Any<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (f => f.FieldType.IsInstanceOfGenericType(typeof (PXProcessing<>))));
  }

  public static bool IsReport(PX.SM.SiteMap sitemap)
  {
    string url = sitemap.Url;
    if (string.IsNullOrEmpty(url))
      return false;
    return PXSiteMap.IsReport(url) || PXSiteMap.IsARmReport(url);
  }

  public static bool IsWiki(PX.SM.SiteMap sitemap)
  {
    string url = sitemap.Url;
    return !string.IsNullOrEmpty(url) && url.ToLowerInvariant().Contains("wiki.aspx");
  }
}
