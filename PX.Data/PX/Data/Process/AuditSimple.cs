// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.AuditSimple
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.Process;

public class AuditSimple
{
  protected PXGraph Graph;
  protected PXCache Cache;
  private Dictionary<string, List<string>> cacheExtensionsByTableName = new Dictionary<string, List<string>>();
  protected Dictionary<System.Type, object> currents = new Dictionary<System.Type, object>();
  protected Dictionary<string, object> lastValues = new Dictionary<string, object>();
  protected readonly bool ShowAllRecords;
  protected readonly string ScreenId;
  private const int AuditRowsLimit = 1000;

  protected AuditSimple()
  {
  }

  public AuditSimple(PXGraph graph, string dataView, bool showAllRecords, string screenId)
  {
    this.Graph = graph;
    this.Cache = graph.Views[dataView].Cache;
    this.ShowAllRecords = showAllRecords;
    this.ScreenId = screenId;
    this.FillCacheExtensionInfo();
  }

  private void FillCacheExtensionInfo()
  {
    if (this.Graph == null)
      return;
    foreach (KeyValuePair<System.Type, PXCache> cach in (Dictionary<System.Type, PXCache>) this.Graph.Caches)
    {
      List<System.Type> extensionTables = cach.Value.GetExtensionTables();
      if (extensionTables != null && !this.cacheExtensionsByTableName.ContainsKey(cach.Key.Name))
        this.cacheExtensionsByTableName.Add(cach.Key.Name, extensionTables.Select<System.Type, string>((Func<System.Type, string>) (extTable => extTable.Name)).ToList<string>());
    }
  }

  protected virtual bool CheckCondition()
  {
    return this.ShowAllRecords || this.Cache.Current != null && this.Cache.GetStatus(this.Cache.Current) != PXEntryStatus.Inserted;
  }

  public AuditInfo CollectAudit()
  {
    if (!this.CheckCondition())
      return (AuditInfo) null;
    AuditInfo info = new AuditInfo(this.GetTableName(this.Cache.GetItemType()));
    info.Panel = PXAuditHelper.CollectInfo(this.Graph, this.Cache);
    info.ScreenId = PXContext.GetScreenID()?.Replace(".", "");
    if (!this.ShowAllRecords)
      this.FillKeys(info, this.Cache);
    AuditBatch source = (AuditBatch) null;
    AuditEntry entry = (AuditEntry) null;
    PXCache cache = (PXCache) null;
    bool flag = false;
    (IEnumerable<AuditHistory> auditHistories, bool RecordsLimitReached) = this.GetRecords();
    info.ChangesLimitReached = RecordsLimitReached;
    foreach (AuditHistory auditHistory in auditHistories.Reverse<AuditHistory>())
    {
      AuditHistory record = auditHistory;
      if (source != null)
      {
        long batch = source.Batch;
        long? batchId = record.BatchID;
        long valueOrDefault = batchId.GetValueOrDefault();
        if (batch == valueOrDefault & batchId.HasValue)
          goto label_9;
      }
      entry = (AuditEntry) null;
      source = info.Add();
      source.Batch = record.BatchID.Value;
      source.Screens = PXAuditHelper.GetAuditedScreenIDs(record.ScreenID).ToArray<string>();
      source.Username = this.GetValueExt(this.Graph.Caches[typeof (AuditHistory)], (object) record, typeof (AuditHistory.userID).Name).NewValue as string;
      source.Date = this.GetValueExt(this.Graph.Caches[typeof (AuditHistory)], (object) record, typeof (AuditHistory.changeDate).Name).NewValue as System.DateTime?;
label_9:
      if (entry == null || entry.Table != record.TableName || entry.Key != record.CombinedKey)
      {
        string key = record.TableName + record.CombinedKey;
        entry = source.FirstOrDefault<AuditEntry>((Func<AuditEntry, bool>) (e => e.Key == key));
        if (entry == null)
        {
          entry = source.Add();
          entry.Key = key;
          entry.Table = this.GetTableName(record.TableName);
          entry.Operation = this.GetOperation(record.Operation);
          entry.OperationTitle = (string) this.GetValueExt(this.Graph.Caches[typeof (AuditHistory)], (object) record, typeof (AuditHistory.operation).Name).NewValue;
        }
        cache = this.Graph.Caches[record.TableName];
        if (cache == null && this.cacheExtensionsByTableName != null)
        {
          string key1 = this.cacheExtensionsByTableName.Where<KeyValuePair<string, List<string>>>((Func<KeyValuePair<string, List<string>>, bool>) (tableInfo => tableInfo.Value.Contains(record.TableName))).Select<KeyValuePair<string, List<string>>, string>((Func<KeyValuePair<string, List<string>>, string>) (tableInfo => tableInfo.Key)).FirstOrDefault<string>();
          if (!string.IsNullOrEmpty(key1))
            cache = this.Graph.Caches[key1];
        }
        if (cache == null)
        {
          System.Type key2 = this.Graph.Caches.Select<KeyValuePair<System.Type, PXCache>, System.Type>((Func<KeyValuePair<System.Type, PXCache>, System.Type>) (pair => pair.Value.BqlTable)).Where<System.Type>((Func<System.Type, bool>) (bqlTable => bqlTable.Name.Equals(record.TableName, StringComparison.Ordinal))).FirstOrDefault<System.Type>();
          if (key2 != (System.Type) null)
            cache = this.Graph.Caches[key2];
        }
        if (cache != null && !this.currents.ContainsKey(cache.GetItemType()))
        {
          this.currents[cache.GetItemType()] = cache.CreateInstance();
          if (cache._KeyValueAttributeNames != null)
            cache.SetSlot<object[]>(this.currents[cache.GetItemType()], cache._KeyValueAttributeSlotPosition, new object[cache._KeyValueAttributeNames.Count], true);
        }
      }
      if (cache != null && (entry.Operation != AuditOperation.Update || !string.IsNullOrEmpty(record.ModifiedFields) || flag))
      {
        cache.GetItemType();
        try
        {
          cache._SelectingForAuditExplore = true;
          this.FillKeys(entry, cache, this.currents[cache.GetItemType()], record.CombinedKey);
          flag = this.FillFields(entry, cache, this.currents[cache.GetItemType()], record.ModifiedFields);
        }
        finally
        {
          cache._SelectingForAuditExplore = false;
        }
      }
    }
    if ((info == null || source == null || entry == null) && info.Panel == null)
      return (AuditInfo) null;
    for (int index1 = info.Count - 1; index1 >= 0; --index1)
    {
      AuditBatch auditBatch = info[index1];
      for (int index2 = auditBatch.Count - 1; index2 >= 0; --index2)
      {
        AuditEntry auditEntry = auditBatch[index2];
        if (auditEntry.Count == 0)
          auditBatch.RemoveAt(index2);
        else if (auditEntry.Values.All<AuditValue>((Func<AuditValue, bool>) (v => object.Equals(v.NewValue, v.OldValue))))
          auditBatch.RemoveAt(index2);
      }
      if (auditBatch.Count <= 0)
        info.RemoveAt(index1);
    }
    if (info.Count <= 0 && info.Panel == null)
      return (AuditInfo) null;
    info.Reverse();
    return info;
  }

  protected virtual void FillKeys(AuditInfo info, PXCache cache)
  {
    string[] keys = PXAuditHelper.GetKeys(cache, this.ScreenId);
    object current = cache.Current;
    if (current == null)
      return;
    for (int index = 0; index < keys.Length; ++index)
      info.Keys.Add(this.GetValueExt(cache, current, keys[index]));
  }

  protected virtual void FillKeys(AuditEntry entry, PXCache cache, object row, string fields)
  {
    PXCache cache1 = cache;
    string[] screens = entry.Batch.Screens;
    string screenId = screens != null ? ((IEnumerable<string>) screens).FirstOrDefault<string>() : (string) null;
    string[] keys = PXAuditHelper.GetKeys(cache1, screenId);
    string[] strArray = fields.Split(PXAuditHelper.SEPARATOR);
    if (strArray.Length != keys.Length)
      return;
    for (int index = 0; index < strArray.Length; ++index)
    {
      string fieldName = keys[index];
      string val = strArray[index];
      object obj = string.IsNullOrWhiteSpace(val) ? (object) null : cache.ValueFromString(fieldName, val);
      cache.SetValue(row, fieldName, obj);
    }
    for (int index = 0; index < keys.Length; ++index)
    {
      string str = keys[index];
      AuditValue valueExt = this.GetValueExt(cache, row, str);
      valueExt.OldValue = this.GetOldValue(entry.Key, str, valueExt.NewValue, true);
      entry[str] = valueExt;
    }
  }

  protected virtual bool FillFields(AuditEntry entry, PXCache cache, object row, string fields)
  {
    bool flag = false;
    string[] strArray = fields.Split(PXAuditHelper.SEPARATOR);
    if (strArray.Length % 2 != 0)
      return flag;
    PXCache cache1 = cache;
    string[] screens = entry.Batch.Screens;
    string screenId = screens != null ? ((IEnumerable<string>) screens).FirstOrDefault<string>() : (string) null;
    HashSet<string> stringSet = new HashSet<string>((IEnumerable<string>) PXAuditHelper.GetKeys(cache1, screenId), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    int num = 0;
    for (int index = 0; index < strArray.Length; index += 2)
    {
      if (stringSet.Contains(strArray[index]))
      {
        num = index;
        flag = true;
      }
    }
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    for (int index = num; index < strArray.Length; index += 2)
    {
      string str = strArray[index];
      string val = strArray[index + 1];
      object obj1 = string.IsNullOrWhiteSpace(val) ? (object) null : cache.ValueFromString(str, val);
      object obj2 = cache.GetValue(row, str);
      object obj3;
      if (!cache.IsKvExtAttribute(str))
      {
        cache.SetValue(row, str, obj1);
        obj3 = cache.GetValue(row, str);
      }
      else
      {
        cache.SetValueExt(row, str, obj1);
        obj3 = obj1;
      }
      object obj4 = obj3;
      if (obj2 == obj4 || obj3 == null && obj1 != null)
        dictionary[str] = obj1;
    }
    for (int index = num; index < strArray.Length; index += 2)
    {
      string str = strArray[index];
      AuditValue valueExt = this.GetValueExt(cache, row, str, dictionary.ContainsKey(str) ? dictionary[str] : (object) null);
      bool isKey = stringSet.Contains(str);
      valueExt.OldValue = this.GetOldValue(entry.Key, str, valueExt.NewValue, isKey);
      entry[str] = valueExt;
    }
    return flag;
  }

  protected virtual string GetTableName(string tablename)
  {
    foreach (KeyValuePair<System.Type, PXCache> cach in (Dictionary<System.Type, PXCache>) this.Graph.Caches)
    {
      if (cach.Key.Name == tablename)
        return this.GetTableName(cach.Key);
    }
    return tablename;
  }

  protected virtual string GetTableName(System.Type type)
  {
    string name;
    if (type.IsDefined(typeof (PXCacheNameAttribute), true))
      name = ((PXNameAttribute) type.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0]).GetName();
    else if (type.IsDefined(typeof (PXProjectionAttribute), true))
      name = PXCache.GetBqlTable(BqlCommand.CreateInstance(((PXProjectionAttribute) type.GetCustomAttributes(typeof (PXProjectionAttribute), true)[0]).Select).GetTables()[0]).Name;
    else
      name = type.Name;
    return name;
  }

  protected virtual AuditOperation GetOperation(string code)
  {
    return PXAuditOperationsListAttribute.GetOperation(code);
  }

  protected virtual (IEnumerable<AuditHistory> Records, bool RecordsLimitReached) GetRecords()
  {
    PXSelectBase<AuditHistory, PXSelect<AuditHistory, Where<AuditHistory.batchID, Equal<Required<AuditHistory.batchID>>>>.Config>.Clear(this.Graph);
    this.Graph.Caches[typeof (AuditHistory)].Clear();
    HashSet<string> source1 = new HashSet<string>();
    (Dictionary<string, (System.Type, string)> Tables, IEnumerable<string> TableDbKeys) tables = AuditMaintDataLoader.GetTablesFromCache(this.Cache);
    EnumerableExtensions.AddRange<string>((ISet<string>) source1, tables.TableDbKeys.Select<string, string>((Func<string, string>) (c => tables.Tables[c].Item2)));
    IQueryable<AuditHistory> source2 = PXDatabase.Select<AuditHistory>();
    string[] primaryViewTables = source1.ToArray<string>();
    IList<string> combKeys = (IList<string>) null;
    IQueryable<AuditHistory> source3;
    if (this.ShowAllRecords)
    {
      source3 = source2.Where<AuditHistory>((Expression<Func<AuditHistory, bool>>) (h => primaryViewTables.Contains<string>(h.TableName)));
    }
    else
    {
      combKeys = PXAuditHelper.GetKeysRestrinction(this.Cache, this.ScreenId);
      source3 = source2.Where<AuditHistory>((Expression<Func<AuditHistory, bool>>) (h => primaryViewTables.Contains<string>(h.TableName) && combKeys.Contains(h.CombinedKey)));
    }
    long?[] array1 = source3.GroupBy<AuditHistory, long?>((Expression<Func<AuditHistory, long?>>) (h => h.BatchID)).OrderByDescending<IGrouping<long?, AuditHistory>, long?>((Expression<Func<IGrouping<long?, AuditHistory>, long?>>) (h => h.Key)).Select<IGrouping<long?, AuditHistory>, long?>((Expression<Func<IGrouping<long?, AuditHistory>, long?>>) (g => g.Key)).Take<long?>(1000).ToArray<long?>();
    List<AuditHistory> auditHistoryList = new List<AuditHistory>();
    foreach (long? nullable in array1)
    {
      long? batch = nullable;
      if (batch.HasValue)
      {
        IQueryable<AuditHistory> source4 = this.Graph.Select<AuditHistory>();
        if (!this.ShowAllRecords)
          source4 = source4.Where<AuditHistory>((Expression<Func<AuditHistory, bool>>) (h => h.BatchID == (long?) batch.Value && (primaryViewTables.Contains<string>(h.TableName) && combKeys.Contains(h.CombinedKey) || !primaryViewTables.Contains<string>(h.TableName))));
        AuditHistory[] array2 = source4.ToArray<AuditHistory>();
        auditHistoryList.AddRange((IEnumerable<AuditHistory>) array2);
      }
    }
    return ((IEnumerable<AuditHistory>) auditHistoryList, array1.Length == 1000);
  }

  protected virtual AuditValue GetValueExt(PXCache cache, object row, string field)
  {
    return this.GetValueExt(cache, row, field, (object) null);
  }

  protected virtual AuditValue GetValueExt(PXCache cache, object row, string field, object value)
  {
    object returnValue = value;
    if (returnValue != null)
      cache.RaiseFieldSelecting(field, row, ref returnValue, true);
    else
      returnValue = cache.GetStateExt(row, field);
    if (!(returnValue is PXFieldState pxFieldState))
      return new AuditValue(field, returnValue);
    if (pxFieldState is PXStringState)
    {
      PXStringState pxStringState = pxFieldState as PXStringState;
      if (pxStringState.Value is string && pxStringState.AllowedValues != null && pxStringState.AllowedLabels != null)
      {
        int index = Array.IndexOf<string>(pxStringState.AllowedValues, pxStringState.Value as string);
        return index < 0 ? new AuditValue(pxStringState.DisplayName, pxStringState.Value) : new AuditValue(pxStringState.DisplayName, (object) pxStringState.AllowedLabels[index]);
      }
    }
    else
    {
      if (pxFieldState is PXDateState)
        return new AuditValue(pxFieldState.DisplayName, pxFieldState.Value)
        {
          Format = (pxFieldState as PXDateState).InputMask
        };
      if (!string.IsNullOrWhiteSpace(pxFieldState.ViewName))
      {
        string fieldName = field + "_description";
        if (cache.Fields.Contains(fieldName))
          return new AuditValue(pxFieldState.DisplayName, cache.GetValueExt(row, fieldName));
        if (!string.IsNullOrWhiteSpace(pxFieldState.DescriptionName))
          return new AuditValue(pxFieldState.DisplayName, cache.GetValueExt(row, pxFieldState.DescriptionName));
      }
    }
    return new AuditValue(pxFieldState.DisplayName, pxFieldState.Value);
  }

  protected virtual object GetOldValue(string table, string field, object value, bool isKey)
  {
    object oldValue = (object) null;
    string key = table + field;
    if (!this.lastValues.TryGetValue(key, out oldValue) || !isKey)
      this.lastValues[key] = value;
    return oldValue;
  }
}
