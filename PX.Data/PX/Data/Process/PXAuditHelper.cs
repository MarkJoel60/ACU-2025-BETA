// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.PXAuditHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.Model.Entities;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.Process;

public static class PXAuditHelper
{
  public static readonly string[] FIELDS;
  public static readonly char SEPARATOR = PXDatabase.Provider.SqlDialect.WildcardFieldSeparatorChar;

  [Obsolete("Use IPrincipal.IsInRole(IOptions<AuditOptions>.Value.AuditorRole). Current IPrincipal may be got using IPXIdentityAccessor.Identity?.User")]
  public static bool IsUserAuditor
  {
    get
    {
      string role = ConfigurationManager.AppSettings["FieldLevelAuditRole"];
      if (string.IsNullOrEmpty(role))
        role = "Field-Level Audit";
      return PXContext.PXIdentity.User.IsInRole(role);
    }
  }

  static PXAuditHelper()
  {
    PXAuditHelper.FIELDS = ((IEnumerable<PropertyInfo>) typeof (AUAuditPanelInfo).GetProperties(BindingFlags.Instance | BindingFlags.Public)).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (p => p.Name)).ToArray<string>();
  }

  public static string CollectAudit(PXGraph graph, string primaryView)
  {
    if (HttpContext.Current == null)
      return (string) null;
    string dataView = primaryView;
    bool showAllRecords = false;
    if (graph is IPXAuditSource pxAuditSource)
    {
      dataView = pxAuditSource.GetMainView();
      showAllRecords = dataView != primaryView;
    }
    string screenId = PXAuditHelper.GetScreenID();
    AuditInfo auditInfo = new AuditSimple(graph, dataView, showAllRecords, screenId).CollectAudit();
    if (auditInfo == null)
      return (string) null;
    string key = Guid.NewGuid().ToString();
    PXContext.SessionTyped<PXSessionStatePXData>().AuditInfo[key] = auditInfo;
    return key;
  }

  [Obsolete]
  public static string GetKeysRestrinction(PXCache cache) => throw new NotImplementedException();

  public static IList<string> GetKeysRestrinction(PXCache cache, string screenId)
  {
    PXGraph graph = cache.Graph;
    (Dictionary<string, (System.Type, string)> Tables, IEnumerable<string> TableDbKeys) tablesFromCache = AuditMaintDataLoader.GetTablesFromCache(cache);
    Dictionary<string, (System.Type, string)> tablesDictionary = tablesFromCache.Tables;
    (System.Type, string)[] array = tablesFromCache.TableDbKeys.Select<string, (System.Type, string)>((Func<string, (System.Type, string)>) (dbType => tablesDictionary[dbType])).ToArray<(System.Type, string)>();
    Dictionary<System.Type, TableHeader> tableHeaders = ((IEnumerable<(System.Type, string)>) array).Select<(System.Type, string), (System.Type, TableHeader)>((Func<(System.Type, string), (System.Type, TableHeader)>) (t => (t.Item1, PXDatabase.Provider.GetTableStructure(t.Item2)))).ToDictionary<(System.Type, TableHeader), System.Type, TableHeader>((Func<(System.Type, TableHeader), System.Type>) (c => c.Item1), (Func<(System.Type, TableHeader), TableHeader>) (c => c.Item2));
    Dictionary<System.Type, string[]> keys = ((IEnumerable<(System.Type, string)>) array).Select<(System.Type, string), (System.Type, string[])>((Func<(System.Type, string), (System.Type, string[])>) (dac => (dac.Item1, PXAuditHelper.GetKeys(graph.Caches[dac.Item1], screenId)))).ToDictionary<(System.Type, string[]), System.Type, string[]>((Func<(System.Type, string[]), System.Type>) (c => c.Item1), (Func<(System.Type, string[]), string[]>) (c => c.Item2));
    Dictionary<System.Type, IEnumerable<string>> dictionary = ((IEnumerable<(System.Type, string)>) array).Select<(System.Type, string), (System.Type, IEnumerable<string>)>((Func<(System.Type, string), (System.Type, IEnumerable<string>)>) (dac => (dac.Item1, ((IEnumerable<string>) keys[dac.Item1]).Where<string>((Func<string, bool>) (k => tableHeaders[dac.Item1].getColumnByName(k) != null))))).ToDictionary<(System.Type, IEnumerable<string>), System.Type, IEnumerable<string>>((Func<(System.Type, IEnumerable<string>), System.Type>) (c => c.Item1), (Func<(System.Type, IEnumerable<string>), IEnumerable<string>>) (c => c.Item2));
    object current = cache.Current;
    List<string> keysRestrinction = new List<string>();
    foreach (KeyValuePair<System.Type, string[]> keyValuePair in keys)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < keyValuePair.Value.Length; ++index)
      {
        string fieldName = keyValuePair.Value[index];
        if (dictionary[keyValuePair.Key].Contains<string>(fieldName))
        {
          stringBuilder.Append(cache.ValueToString(fieldName, cache.GetValue(current, fieldName)));
          if (index < keyValuePair.Value.Length - 1)
            stringBuilder.Append(PXAuditHelper.SEPARATOR);
        }
      }
      keysRestrinction.Add(stringBuilder.ToString());
    }
    return (IList<string>) keysRestrinction;
  }

  internal static string[] GetKeys(PXCache cache, string screenId)
  {
    string[] keys = PXDatabase.GetSlot<AuditSetup>(typeof (AuditSetup).FullName, typeof (AUAuditSetup), typeof (AUAuditTable), typeof (AUAuditField)).GetKeyNames(BqlCommand.GetTableName(cache.GetItemType()), screenId);
    if (keys.Length == 0)
    {
      PXTrace.WriteError($"Keys for cache {cache} were not found in AuditSetup.");
      keys = cache.Keys.ToArray();
    }
    return keys;
  }

  private static System.DateTime ConvertServerToUTCTime(System.DateTime date)
  {
    System.DateTime dtLocal;
    System.DateTime dtUtc;
    PXDatabase.SelectDate(out dtLocal, out dtUtc);
    long num = dtUtc.Ticks - dtLocal.Ticks;
    return date.AddTicks(num);
  }

  public static AUAuditPanelInfo CollectInfo(PXGraph graph, string primaryView)
  {
    string key = primaryView;
    if (graph is IPXAuditSource pxAuditSource)
      key = pxAuditSource.GetMainView();
    return PXAuditHelper.CollectInfo(graph, graph.Views[key].Cache);
  }

  public static AUAuditPanelInfo CollectInfo(PXGraph graph, PXCache cache)
  {
    if (cache.Current == null || cache.GetStatus(cache.Current) == PXEntryStatus.Inserted)
      return (AUAuditPanelInfo) null;
    object data = cache.GetOriginal(cache.Current) ?? cache.Current;
    bool flag = false;
    AUAuditPanelInfo target = new AUAuditPanelInfo();
    foreach (string str in PXAuditHelper.FIELDS)
    {
      if (cache.Fields.Contains(str) && cache.GetStateExt(data, str) is PXFieldState stateExt && stateExt.Value != null)
      {
        typeof (AUAuditPanelInfo).InvokeMember(str, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, (Binder) null, (object) target, new object[1]
        {
          stateExt.Value
        });
        flag = true;
      }
    }
    return !flag ? (AUAuditPanelInfo) null : target;
  }

  public static bool IsInfoAvailable(PXGraph graph, string primaryView)
  {
    if (graph == null || string.IsNullOrEmpty(primaryView) || !graph.Views.ContainsKey(primaryView))
      return false;
    PXCache cache = graph.Views[primaryView].Cache;
    foreach (string str in PXAuditHelper.FIELDS)
    {
      if (cache.Fields.Contains(str))
        return true;
    }
    return false;
  }

  public static string GetScreenID()
  {
    string screenId = PXContext.GetScreenID()?.Replace(".", "");
    if (!string.IsNullOrEmpty(screenId))
      screenId = PXAuditHelper.GetAuditedScreenIDs(screenId).FirstOrDefault<string>() ?? screenId;
    return screenId;
  }

  internal static IEnumerable<string> GetAuditedScreenIDs(string screenId)
  {
    PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenId);
    if (screenIdUnsecure != null && PXAuditHelper.IsGenericScreen(screenIdUnsecure))
      return EnumerableExtensions.AsSingleEnumerable<string>(screenId);
    string graphType = screenIdUnsecure?.GraphType;
    return !string.IsNullOrEmpty(graphType) ? PXSiteMap.Provider.FindSiteMapNodesByGraphTypeUnsecure(graphType).Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (sm => !string.IsNullOrEmpty(sm.ScreenID) && sm.GraphType == graphType)).OrderBy<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (sm => sm.ScreenID)).Select<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (sm =>
    {
      string url1 = sm.Url;
      if ((url1 != null ? (url1.StartsWith("~/Pages/SM/SM206036.aspx", StringComparison.OrdinalIgnoreCase) ? 1 : 0) : 0) != 0)
        return "SM206036";
      string url2 = sm.Url;
      return (url2 != null ? (url2.StartsWith("~/Pages/SM/SM207036.aspx", StringComparison.OrdinalIgnoreCase) ? 1 : 0) : 0) != 0 ? "SM207036" : sm.ScreenID;
    })).Distinct<string>() : Enumerable.Empty<string>();
  }

  /// <summary>
  /// Returns true if the screen is generic and works with different entities
  /// </summary>
  private static bool IsGenericScreen(PXSiteMapNode node) => PXSiteMap.IsGenericInquiry(node?.Url);
}
