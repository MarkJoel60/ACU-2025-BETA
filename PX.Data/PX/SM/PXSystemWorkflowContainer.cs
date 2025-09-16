// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSystemWorkflowContainer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.WorkflowAPI;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

[PXInternalUseOnly]
public class PXSystemWorkflowContainer
{
  public readonly Dictionary<System.Type, Exception> InitializationExceptions = new Dictionary<System.Type, Exception>();
  private readonly ConcurrentDictionary<System.Type, IList> _items = new ConcurrentDictionary<System.Type, IList>();
  internal readonly Dictionary<string, WorkflowConditionEvaluator> SystemConditions = new Dictionary<string, WorkflowConditionEvaluator>();
  internal readonly Dictionary<string, List<AUScreenFieldState>> SystemFieldStates = new Dictionary<string, List<AUScreenFieldState>>();
  public readonly HashSet<IBqlTable> UniqueItems = new HashSet<IBqlTable>((IEqualityComparer<IBqlTable>) new PXCaseInsensitiveDacComparer<IBqlTable>());
  internal PXGraph Graph = new PXGraph();

  public List<T> GetItems<T>() where T : IBqlTable => this.GetList<T>(true);

  internal List<T> GetItemsForScreen<T>(string screenID) where T : IScreenItem
  {
    return this.GetItems<T>().Where<T>((Func<T, bool>) (item => string.Equals(item.ScreenID, screenID, StringComparison.OrdinalIgnoreCase))).ToList<T>();
  }

  private List<T> GetList<T>(bool create)
  {
    IList list;
    if (!this._items.TryGetValue(typeof (T), out list))
    {
      list = (IList) new List<T>();
      if (create)
        this._items.TryAdd(typeof (T), list);
    }
    return (List<T>) list;
  }

  internal List<T> GetListForScreen<T>(bool create, string screenID) where T : IScreenItem
  {
    return this.GetList<T>(create).Where<T>((Func<T, bool>) (item => string.Equals(item.ScreenID, screenID, StringComparison.OrdinalIgnoreCase))).ToList<T>();
  }

  private void AddUniqueItem(IBqlTable row)
  {
    if (!this.UniqueItems.Add(row) && this.Graph != null)
    {
      PXCache cache = this.Graph.Caches[row.GetType()];
      throw new ArgumentException($"Duplicate item of type {row.GetType().Name} with keys {string.Join<object>(";", cache.Keys.Select<string, object>((Func<string, object>) (k => cache.GetValue((object) row, k))))} ");
    }
  }

  public void Insert<T>(T row) where T : IBqlTable
  {
    this.AddUniqueItem((IBqlTable) row);
    this.GetItems<T>().Add(row);
  }

  public void InsertRange<T>(IList<T> rows) where T : IBqlTable
  {
    foreach (T row in (IEnumerable<T>) rows)
      this.AddUniqueItem((IBqlTable) row);
    this.GetItems<T>().AddRange((IEnumerable<T>) rows);
  }

  internal WorkflowConditionEvaluator RegisterCondition(
    Readonly.Condition condition,
    IBqlTable record,
    string property = null)
  {
    WorkflowConditionEvaluator conditionEvaluator = new WorkflowConditionEvaluator(condition)
    {
      Name = condition.Name,
      DisplayName = condition.Name,
      Record = record
    };
    this.SystemConditions.Add($"{((AUScreenConditionState) record).ScreenID}_{conditionEvaluator.Name}", conditionEvaluator);
    return conditionEvaluator;
  }

  internal void RegisterFieldState(AUScreenFieldState fieldState)
  {
    List<AUScreenFieldState> screenFieldStateList;
    if (!this.SystemFieldStates.TryGetValue(fieldState.TableName, out screenFieldStateList))
    {
      screenFieldStateList = new List<AUScreenFieldState>();
      this.SystemFieldStates.Add(fieldState.TableName, screenFieldStateList);
    }
    screenFieldStateList.Add(fieldState);
  }

  internal void ClearServiceGraphs()
  {
    this.Graph = new PXGraph();
    ((PXCaseInsensitiveDacComparer<IBqlTable>) this.UniqueItems.Comparer).Graph = new PXGraph();
  }
}
