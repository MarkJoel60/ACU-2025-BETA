// Decompiled with JetBrains decompiler
// Type: PX.Data.Archiving.ArchiveInfoHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Archiving.DAC;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.Archiving;

internal class ArchiveInfoHelper : IPrefetchable, IPXCompanyDependent
{
  private readonly ConcurrentDictionary<string, (BqlCommand CountPerDatePerYear, BqlCommand Earliest, BqlCommand Records)> Commands = new ConcurrentDictionary<string, (BqlCommand, BqlCommand, BqlCommand)>();
  private static readonly ConcurrentDictionary<string, ArchiveInfoHelper.TableInfo> Types = new ConcurrentDictionary<string, ArchiveInfoHelper.TableInfo>();
  private readonly ConcurrentDictionary<(int Year, string Table), ImmutableDictionary<System.DateTime, int>> CountPerDatePerYear = new ConcurrentDictionary<(int, string), ImmutableDictionary<System.DateTime, int>>();
  private readonly ConcurrentStack<ArchivalPolicy> Policies = new ConcurrentStack<ArchivalPolicy>();
  private System.DateTime? EarliestArchiveDate;
  private readonly object dateSyncObject = new object();

  internal void ClearData()
  {
    ArchivalPolicy archivalPolicy = this.Policies.FirstOrDefault<ArchivalPolicy>();
    if (archivalPolicy == null)
      return;
    PXDatabase.Update<ArchivalPolicy>((PXDataFieldParam) new PXDataFieldAssign("TypeName", (object) archivalPolicy.TypeName), (PXDataFieldParam) new PXDataFieldRestrict("TypeName", (object) archivalPolicy.TypeName));
  }

  void IPrefetchable.Prefetch()
  {
    foreach (ArchivalPolicy archivalPolicy in (IEnumerable<ArchivalPolicy>) PXDatabase.Select<ArchivalPolicy>())
      this.Policies.Push(archivalPolicy);
    lock (this.dateSyncObject)
      this.EarliestArchiveDate = new System.DateTime?();
  }

  public static ArchiveInfoHelper Instance
  {
    get
    {
      return PXDatabase.GetSlotWithContextCache<ArchiveInfoHelper>(typeof (ArchiveInfoHelper).FullName, typeof (PXGraph.FeaturesSet), typeof (ArchivalPolicy));
    }
  }

  public IEnumerable<ArchivalPolicy> GetPolicies()
  {
    return (IEnumerable<ArchivalPolicy>) this.Policies.Select<ArchivalPolicy, ArchivalPolicy>((Func<ArchivalPolicy, ArchivalPolicy>) (p => PropertyTransfer.Transfer<ArchivalPolicy, ArchivalPolicy>(p, new ArchivalPolicy()))).ToArray<ArchivalPolicy>();
  }

  public ArchivalPolicy GetPolicyFor(System.Type table) => this.GetPolicyFor(table.FullName);

  public ArchivalPolicy GetPolicyFor(string dacFullName)
  {
    return this.Policies.Where<ArchivalPolicy>((Func<ArchivalPolicy, bool>) (p => p.TypeName == dacFullName)).Select<ArchivalPolicy, ArchivalPolicy>((Func<ArchivalPolicy, ArchivalPolicy>) (p => PropertyTransfer.Transfer<ArchivalPolicy, ArchivalPolicy>(p, new ArchivalPolicy()))).FirstOrDefault<ArchivalPolicy>();
  }

  public string GetCacheName(PXGraph graph, string dacFullName)
  {
    return ArchiveInfoHelper.GetType(graph, dacFullName).CacheName;
  }

  public int GetAllCount(PXGraph graph, System.DateTime date)
  {
    return this.Policies.Sum<ArchivalPolicy>((Func<ArchivalPolicy, int>) (d => this.GetCount(graph, date, d.TypeName)));
  }

  public int GetCount(PXGraph graph, System.DateTime date, string dacFullName)
  {
    return this.SelectDocCount((date, dacFullName), graph);
  }

  private int SelectDocCount((System.DateTime Date, string Table) key, PXGraph graph)
  {
    if (this.Policies.Where<ArchivalPolicy>((Func<ArchivalPolicy, bool>) (d => d.TypeName == key.Table)).FirstOrDefault<ArchivalPolicy>() != null)
    {
      System.DateTime? businessDate = graph.Accessinfo.BusinessDate;
      if (businessDate.HasValue)
      {
        System.DateTime date = key.Date;
        businessDate = graph.Accessinfo.BusinessDate;
        System.DateTime dateTime = businessDate.Value;
        if (!(date > dateTime))
        {
          ImmutableDictionary<System.DateTime, int> countPerDatePerYear = this.GetCountPerDatePerYear(graph, key.Table, key.Date.Year);
          return countPerDatePerYear != null && countPerDatePerYear.ContainsKey(key.Date) ? countPerDatePerYear[key.Date] : 0;
        }
      }
    }
    return 0;
  }

  public IEnumerable<IBqlTable> SelectRecords(PXGraph graph, string dacFullName, System.DateTime date)
  {
    ArchivalPolicy archivalPolicy = this.Policies.Where<ArchivalPolicy>((Func<ArchivalPolicy, bool>) (d => d.TypeName == dacFullName)).FirstOrDefault<ArchivalPolicy>();
    BqlCommand records = this.Commands.GetOrAdd<PXGraph>(dacFullName, new Func<string, PXGraph, (BqlCommand, BqlCommand, BqlCommand)>(this.CreateCommands), graph).Records;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return graph.TypedViews.GetView(records, true).SelectMulti((object) date.AddDays((double) -archivalPolicy.DelayInDays)).Select<object, IBqlTable>(ArchiveInfoHelper.\u003C\u003EO.\u003C0\u003E__UnwrapMain ?? (ArchiveInfoHelper.\u003C\u003EO.\u003C0\u003E__UnwrapMain = new Func<object, IBqlTable>(PXResult.UnwrapMain)));
  }

  public ImmutableDictionary<System.DateTime, int> GetCountPerDatePerYear(
    PXGraph graph,
    string dacFullName,
    int year)
  {
    return this.CountPerDatePerYear.GetOrAdd<PXGraph>((year, dacFullName), new Func<(int, string), PXGraph, ImmutableDictionary<System.DateTime, int>>(this.SelectCountPerDatePerYear), graph);
  }

  private ImmutableDictionary<System.DateTime, int> SelectCountPerDatePerYear(
    (int Year, string Table) key,
    PXGraph graph)
  {
    ArchivalPolicy policy = this.Policies.Where<ArchivalPolicy>((Func<ArchivalPolicy, bool>) (d => d.TypeName == key.Table)).FirstOrDefault<ArchivalPolicy>();
    BqlCommand countPerDatePerYear = this.Commands.GetOrAdd<PXGraph>(key.Table, new Func<string, PXGraph, (BqlCommand, BqlCommand, BqlCommand)>(this.CreateCommands), graph).CountPerDatePerYear;
    PXView view = graph.TypedViews.GetView(countPerDatePerYear, true);
    System.DateTime dateTime1 = new System.DateTime(key.Year, 1, 1).AddDays((double) -policy.DelayInDays);
    System.DateTime dateTime2 = new System.DateTime(key.Year + 1, 1, 1).AddDays((double) -policy.DelayInDays);
    ArchiveInfoHelper.TableInfo tableInfo = ArchiveInfoHelper.GetType(graph, policy.TypeName);
    PXCache cache = view.Cache;
    using (new PXFieldScope(view, tableInfo.SelectedFields))
      return ImmutableDictionary.ToImmutableDictionary<PXResult, System.DateTime, int>(view.SelectMulti((object) dateTime1, (object) dateTime2).Cast<PXResult>(), (Func<PXResult, System.DateTime>) (r => ((System.DateTime) cache.GetValue((object) PXResult.UnwrapMain((object) r), tableInfo.GotReadyToArchiveField.Name)).AddDays((double) policy.DelayInDays)), (Func<PXResult, int>) (r => r.RowCount.GetValueOrDefault()));
  }

  public System.DateTime? GetEarliestArchiveDate(PXGraph graph)
  {
    lock (this.dateSyncObject)
      return this.EarliestArchiveDate ?? (this.EarliestArchiveDate = this.GetEarliestArchiveDates(graph).OrderBy<System.DateTime?, System.DateTime?>((Func<System.DateTime?, System.DateTime?>) (t => t)).FirstOrDefault<System.DateTime?>());
  }

  private IEnumerable<System.DateTime?> GetEarliestArchiveDates(PXGraph graph)
  {
    ArchiveInfoHelper archiveInfoHelper = this;
    foreach (ArchivalPolicy policy in archiveInfoHelper.Policies)
    {
      ArchiveInfoHelper.TableInfo type = ArchiveInfoHelper.GetType(graph, policy.TypeName);
      PXView view = graph.TypedViews.GetView(archiveInfoHelper.Commands.GetOrAdd<PXGraph>(policy.TypeName, new Func<string, PXGraph, (BqlCommand, BqlCommand, BqlCommand)>(archiveInfoHelper.CreateCommands), graph).Earliest, true);
      using (new PXFieldScope(view, type.SelectedFields))
      {
        object data = view.SelectSingle();
        if (data != null && graph.Caches[type.Table].GetValue(data, type.GotReadyToArchiveField.Name) is System.DateTime dateTime)
          yield return new System.DateTime?(dateTime.AddDays((double) policy.DelayInDays));
      }
    }
  }

  public IEnumerable<System.DateTime> GetArchivableDates(
    PXGraph graph,
    System.DateTime? startDate = null,
    System.DateTime? endDate = null)
  {
    System.DateTime? earliestArchiveDate = this.GetEarliestArchiveDate(graph);
    if (earliestArchiveDate.HasValue)
    {
      System.DateTime valueOrDefault1 = earliestArchiveDate.GetValueOrDefault();
      System.DateTime? businessDate = graph.Accessinfo.BusinessDate;
      if (businessDate.HasValue)
      {
        System.DateTime valueOrDefault2 = businessDate.GetValueOrDefault();
        startDate = new System.DateTime?(!startDate.HasValue ? valueOrDefault1 : Tools.Max<System.DateTime>(valueOrDefault1, startDate.Value));
        endDate = new System.DateTime?(!endDate.HasValue ? valueOrDefault2 : Tools.Min<System.DateTime>(valueOrDefault2, endDate.Value));
        for (System.DateTime currentDate = startDate.Value; currentDate <= endDate.Value; currentDate = currentDate.AddDays(1.0))
        {
          if (this.GetAllCount(graph, currentDate) != 0)
            yield return currentDate;
        }
      }
    }
  }

  private (BqlCommand CountPerDatePerYear, BqlCommand Earliest, BqlCommand Records) CreateCommands(
    string dacFullName,
    PXGraph graph)
  {
    ArchiveInfoHelper.TableInfo type = ArchiveInfoHelper.GetType(graph, dacFullName);
    return (BqlCommand.CreateInstance(typeof (Select4<,,>), type.Table, typeof (Where<,,>), type.GotReadyToArchiveField, typeof (IsNotNull), typeof (And<,>), type.GotReadyToArchiveField, typeof (Between<,>), typeof (Required<>), type.GotReadyToArchiveField, typeof (Required<>), type.GotReadyToArchiveField, typeof (Aggregate<>), typeof (GroupBy<,>), type.GotReadyToArchiveField, typeof (Count)), BqlCommand.CreateInstance(typeof (Select<,,>), type.Table, typeof (Where<,>), type.GotReadyToArchiveField, typeof (IsNotNull), typeof (OrderBy<>), typeof (Asc<>), type.GotReadyToArchiveField), BqlCommand.CreateInstance(typeof (Select<,>), type.Table, typeof (Where<,>), type.GotReadyToArchiveField, typeof (Equal<>), typeof (Required<>), type.GotReadyToArchiveField));
  }

  public static ArchiveInfoHelper.TableInfo GetType(PXGraph graph, string dacFullName)
  {
    return ArchiveInfoHelper.Types.GetOrAdd<PXGraph>(dacFullName, (Func<string, PXGraph, ArchiveInfoHelper.TableInfo>) ((s, pxGraph) => ArchiveInfoHelper.GetType(s, pxGraph)), graph);
  }

  private static ArchiveInfoHelper.TableInfo GetType(string dacFullName, PXGraph graph)
  {
    System.Type type = PXBuildManager.GetType(dacFullName, true);
    PXCache cach = graph.Caches[type];
    string cacheName = cach.DisplayName ?? type.Name;
    string dateFieldOf = PXDBGotReadyForArchiveAttribute.GetDateFieldOf(type);
    System.Type bqlField = cach.GetBqlField(dateFieldOf);
    System.Type graphType;
    PXPrimaryGraphAttribute.FindPrimaryGraph(cach, out graphType);
    string listScreenID = graphType.With<System.Type, string>(new Func<System.Type, string>(((PXSiteMapProviderExtensions) PXSiteMap.Provider).GetScreenIdByGraphType)).With<string, string>(new Func<string, string>(PXList.Provider.GetListID));
    return new ArchiveInfoHelper.TableInfo(type, cacheName, (IEnumerable<System.Type>) cach.BqlKeys, bqlField, graphType, listScreenID);
  }

  public bool IsReadyToBeArchived(PXCache cache, object row, out System.DateTime? readyDate)
  {
    PXEventSubscriberAttribute subscriberAttribute = (PXEventSubscriberAttribute) cache.GetAttributes((string) null).OfType<PXDBGotReadyForArchiveAttribute>().FirstOrDefault<PXDBGotReadyForArchiveAttribute>();
    if (subscriberAttribute != null)
    {
      if (cache.GetValue(row, subscriberAttribute.FieldName) is System.DateTime dateTime)
      {
        ArchivalPolicy policyFor = this.GetPolicyFor(cache.GetItemType());
        readyDate = policyFor == null ? new System.DateTime?(dateTime) : new System.DateTime?(dateTime.AddDays((double) policyFor.DelayInDays));
        System.DateTime? nullable = readyDate;
        System.DateTime? businessDate = cache.Graph.Accessinfo.BusinessDate;
        return nullable.HasValue & businessDate.HasValue && nullable.GetValueOrDefault() <= businessDate.GetValueOrDefault();
      }
      readyDate = new System.DateTime?();
      return false;
    }
    readyDate = new System.DateTime?();
    return true;
  }

  internal class TableInfo
  {
    public System.Type Table { get; private set; }

    public string CacheName { get; private set; }

    public System.Type GotReadyToArchiveField { get; private set; }

    public System.Type PrimaryGraph { get; private set; }

    public string ListScreenID { get; private set; }

    public System.Type[] SelectedFields { get; private set; }

    public TableInfo(
      System.Type table,
      string cacheName,
      IEnumerable<System.Type> keys,
      System.Type gotReadyToArchiveField,
      System.Type primaryGraph,
      string listScreenID)
    {
      this.Table = table;
      this.CacheName = cacheName;
      this.SelectedFields = keys.Append<System.Type>(gotReadyToArchiveField).ToArray<System.Type>();
      this.GotReadyToArchiveField = gotReadyToArchiveField;
      this.PrimaryGraph = primaryGraph;
      this.ListScreenID = listScreenID;
    }
  }
}
