// Decompiled with JetBrains decompiler
// Type: PX.Data.Handlers.PXEntityOpener
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.Description;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.Handlers;

/// <summary>
/// Performs navigation to specific screens and records from URLs containing screen IDs
/// and key fields.
/// </summary>
public static class PXEntityOpener
{
  public static void Open(
    string screenId,
    bool redirect,
    params KeyValuePair<string, string>[] pars)
  {
    if (pars == null || string.IsNullOrEmpty(screenId))
      return;
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId);
    PXSiteMap.ScreenInfo screenInfo = ScreenUtils.ScreenInfo.Get(screenId);
    if (mapNodeByScreenId == null || screenInfo == null)
      return;
    System.Type type = PXBuildManager.GetType(screenInfo.GraphName, false);
    if (type == (System.Type) null)
      return;
    PXGraph instance = PXGraph.CreateInstance(type);
    if (instance == null)
      return;
    PXView view = instance.Views[screenInfo.PrimaryView];
    if (view == null || !screenInfo.Containers.ContainsKey(screenInfo.PrimaryView))
      return;
    PXViewDescription container = screenInfo.Containers[screenInfo.PrimaryView];
    List<KeyValuePair<string, string>> keyValuePairList = new List<KeyValuePair<string, string>>();
    List<KeyValuePair<string, string>> source = new List<KeyValuePair<string, string>>();
    foreach (KeyValuePair<string, string> par in pars)
    {
      KeyValuePair<string, string> param = par;
      if (((IEnumerable<PX.Data.Description.FieldInfo>) container.Fields).Any<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (fi => fi.FieldName == param.Key)))
        keyValuePairList.Add(param);
      else
        source.Add(param);
    }
    if (((IEnumerable<KeyValuePair<string, string>>) pars).Count<KeyValuePair<string, string>>() > 0)
      PXEntityOpener.SelectCurrent(view, container, keyValuePairList.ToArray());
    instance.Unload();
    if (!redirect)
      return;
    string str = source.Count <= 0 ? string.Empty : "?" + string.Join("&", source.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (p => $"{p.Key}={p.Value}")).ToArray<string>());
    PXContext.Session.SetRedirectGraphType(mapNodeByScreenId.Url.ToRelativeUrl(), instance);
    HttpContext.Current.Response.Redirect(mapNodeByScreenId.Url + str);
  }

  private static void SelectCurrent(
    PXView primaryView,
    PXViewDescription primaryViewInfo,
    params KeyValuePair<string, string>[] pars)
  {
    List<object> objectList1 = new List<object>();
    List<object> objectList2 = new List<object>();
    List<string> stringList = new List<string>();
    List<bool> boolList = new List<bool>();
    int startRow = 0;
    int totalRows = 0;
    foreach (KeyValuePair<string, string> par in pars)
    {
      KeyValuePair<string, string> pair = par;
      if (!primaryView.Cache.Keys.Contains(pair.Key))
        return;
      PXFieldState stateExt = primaryView.Cache.GetStateExt((object) null, pair.Key) as PXFieldState;
      object obj = Convert.ChangeType((object) pair.Value, stateExt.DataType);
      objectList1.Add(obj);
      stringList.Add(pair.Key);
      boolList.Add(false);
      if (((IEnumerable<ParsInfo>) primaryViewInfo.Parameters).Any<ParsInfo>((Func<ParsInfo, bool>) (p => p.Name == pair.Key)))
        objectList2.Add(obj);
    }
    List<object> objectList3 = primaryView.Select((object[]) null, objectList2.ToArray(), objectList1.ToArray(), stringList.ToArray(), boolList.ToArray(), (PXFilterRow[]) null, ref startRow, 1, ref totalRows);
    if (objectList3 == null || objectList3.Count == 0)
      return;
    object obj1 = objectList3[0] is PXResult ? ((PXResult) objectList3[0])[primaryView.Cache.GetItemType()] : objectList3[0];
    if (obj1 == null)
      return;
    primaryView.Cache.Current = obj1;
    primaryView.Graph?.EnsureIfArchived(primaryView);
  }
}
