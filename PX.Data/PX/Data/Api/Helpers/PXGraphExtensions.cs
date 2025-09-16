// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Helpers.PXGraphExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.Api.Services;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace PX.Data.Api.Helpers;

public static class PXGraphExtensions
{
  public static void InitViewDataCache(this PXGraph graph, string screenId)
  {
    if (!(graph is PXGenericInqGrph pxGenericInqGrph))
      return;
    ViewDataService.Instance.SetCache(screenId, "DesignID", pxGenericInqGrph.Design.DesignID.ToString());
    if (!graph.Views.ContainsKey("Filter"))
      return;
    ViewDataService.Instance.ClearFilter(screenId);
    object current = graph.Views["Filter"].Cache.Current;
    if (current == null || !(current is GenericFilter genericFilter))
      return;
    foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) genericFilter.Values)
    {
      ViewDataService.Instance.SetFilterCache(screenId, keyValuePair.Key, keyValuePair.Value?.ToString());
      object obj = PXFieldState.UnwrapValue(graph.Views["Filter"].Cache.GetValueExt(current, keyValuePair.Key));
      if (obj == null)
        ViewDataService.Instance.SetInitFilterCache(screenId, keyValuePair.Key, (string) null);
      else if (obj is System.DateTime dateTime)
        ViewDataService.Instance.SetInitFilterCache(screenId, keyValuePair.Key, dateTime.ToString((IFormatProvider) CultureInfo.InvariantCulture.DateTimeFormat));
      else
        ViewDataService.Instance.SetInitFilterCache(screenId, keyValuePair.Key, obj.ToString());
    }
  }

  public static Dictionary<string, string> GetAdditionalKeys(this PXGraph graph, string screenId)
  {
    return ViewDataService.Instance.GetFilterKeys(screenId) ?? new Dictionary<string, string>();
  }

  public static bool IsImportExportGraph(this string graphType)
  {
    return graphType.OrdinalContains("SYImportMaint", "SYExportMaint", "SYProviderMaint", "SYImportProcess", "SYExportProcess");
  }
}
