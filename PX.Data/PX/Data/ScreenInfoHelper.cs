// Decompiled with JetBrains decompiler
// Type: PX.Data.ScreenInfoHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.Description;
using PX.Data.Reports;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

internal static class ScreenInfoHelper
{
  private static Dictionary<string, PXSiteMap.ScreenInfo> ScreenInfo
  {
    get
    {
      return PXContext.GetSlot<Dictionary<string, PXSiteMap.ScreenInfo>>("GIScreenInfoListAttribute$ScreenInfo") ?? PXContext.SetSlot<Dictionary<string, PXSiteMap.ScreenInfo>>("GIScreenInfoListAttribute$ScreenInfo", new Dictionary<string, PXSiteMap.ScreenInfo>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase));
    }
    set
    {
      PXContext.SetSlot<Dictionary<string, PXSiteMap.ScreenInfo>>("GIScreenInfoListAttribute$ScreenInfo", value);
    }
  }

  internal static PXSiteMapNode GetSiteMapNode(PXCache cache, string screenIdFieldName)
  {
    string rawScreenId = ScreenInfoHelper.GetRawScreenID(cache, screenIdFieldName);
    return rawScreenId != null ? PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(rawScreenId) : (PXSiteMapNode) null;
  }

  internal static string GetRawScreenID(PXCache cache, string screenIdFieldName)
  {
    return cache.Current != null ? (string) cache.GetValue(cache.Current, screenIdFieldName) : (string) null;
  }

  internal static string GetScreenID(PXCache cache, string screenIdFieldName)
  {
    return ScreenInfoHelper.GetSiteMapNode(cache, screenIdFieldName)?.ScreenID;
  }

  private static PXSiteMap.ScreenInfo GetScreenInfo(string screenId)
  {
    if (!string.IsNullOrEmpty(screenId))
    {
      if (ScreenInfoHelper.ScreenInfo.ContainsKey(screenId))
        return ScreenInfoHelper.ScreenInfo[screenId];
      PXSiteMap.ScreenInfo screenInfo = ScreenUtils.ScreenInfo.TryGet(screenId);
      if (screenInfo != null)
      {
        ScreenInfoHelper.ScreenInfo[screenId] = screenInfo;
        return screenInfo;
      }
    }
    return (PXSiteMap.ScreenInfo) null;
  }

  internal static PXSiteMap.ScreenInfo GetScreenInfo(PXCache cache, string screenIdFieldName)
  {
    return ScreenInfoHelper.GetScreenInfo(ScreenInfoHelper.GetScreenID(cache, screenIdFieldName));
  }

  private static ScreenMetadata GetScreenMetadata(PXSiteMap.ScreenInfo screenInfo)
  {
    if (screenInfo == null)
      return (ScreenMetadata) null;
    PXViewDescription primaryView;
    if ((screenInfo.PrimaryView == null || screenInfo.GraphName.OrdinalIgnoreCaseEquals(typeof (ReportMaint).FullName)) && screenInfo.Containers.TryGetValue("Parameters", out primaryView))
      return new ScreenMetadata(ScreenType.Report, screenInfo, primaryView);
    if (screenInfo.PrimaryView == null)
      return new ScreenMetadata(screenInfo);
    PXViewDescription pxViewDescription;
    int num = !screenInfo.Containers.TryGetValue(screenInfo.PrimaryView, out pxViewDescription) ? 0 : (pxViewDescription != null ? 1 : 0);
    bool flag1 = num == 0;
    if (num != 0)
    {
      bool flag2 = ((IEnumerable<PX.Data.Description.FieldInfo>) pxViewDescription.AllFields).Any<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (f => f.IsKey));
      flag1 = !pxViewDescription.IsGrid && !flag2;
      if (flag2)
        return new ScreenMetadata(pxViewDescription.IsGrid ? ScreenType.Inquiry : ScreenType.EntryScreen, screenInfo, pxViewDescription);
    }
    else
      pxViewDescription = new PXViewDescription(screenInfo.PrimaryView);
    if (!flag1)
      return new ScreenMetadata(screenInfo);
    PXViewDescription dataView = screenInfo.Containers.Values.FirstOrDefault<PXViewDescription>((Func<PXViewDescription, bool>) (c => c.IsGrid));
    return dataView == null ? new ScreenMetadata(ScreenType.Dashboard, screenInfo, pxViewDescription, pxViewDescription) : new ScreenMetadata(ScreenType.InquiryWithFilter, screenInfo, pxViewDescription, dataView);
  }

  internal static ScreenMetadata GetScreenMetadata(string screenID)
  {
    return ScreenInfoHelper.GetScreenMetadata(ScreenInfoHelper.GetScreenInfo(screenID));
  }

  internal static ScreenMetadata GetScreenMetadata(PXCache cache, string screenIdFieldName)
  {
    return ScreenInfoHelper.GetScreenMetadata(ScreenInfoHelper.GetScreenInfo(cache, screenIdFieldName));
  }
}
