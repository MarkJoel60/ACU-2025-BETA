// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFilterDetailView
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data;

public sealed class PXFilterDetailView : PXView
{
  private readonly System.Type[] _AutoFill;
  private readonly string _ViewName;
  private static readonly Regex _selectorNestedFieldsRegex = new Regex("[A-Za-z0-9]+_[A-Za-z0-9]+_[A-Za-z0-9]+", RegexOptions.Compiled);

  public PXFilterDetailView(PXGraph graph, string viewName, params System.Type[] autoFill)
    : base(graph, false, (BqlCommand) new PX.Data.Select<FilterRow, Where<FilterRow.filterID, Equal<Required<FilterRow.filterID>>>>())
  {
    this._AutoFill = autoFill;
    this._ViewName = viewName;
    this.AttachEventHandlers();
  }

  public override List<object> Select(
    object[] currents,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    List<object> objectList = base.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    if (this._AutoFill.Length != 0)
      this.ClearCache(parameters[0]);
    if (objectList != null && objectList.Count > 0)
    {
      System.Type[] itemTypes = this._Graph.Views[this._ViewName].GetItemTypes();
      for (int index1 = 0; index1 < this._AutoFill.Length && index1 < itemTypes.Length; ++index1)
      {
        if (this._AutoFill[index1] != (System.Type) null)
        {
          PXCache cach = this._Graph.Caches[this._AutoFill[index1]];
          object current = cach.Current;
          if (current != null)
          {
            string str = itemTypes[index1].Name + "__";
            for (int index2 = 0; index2 < objectList.Count; ++index2)
            {
              FilterRow data = (FilterRow) objectList[index2];
              if (data.DataField != null && (index1 == 0 && !data.DataField.Contains("__") || index1 > 0 && data.DataField.StartsWith(str)))
              {
                byte? condition = data.Condition;
                if (condition.HasValue)
                {
                  condition = data.Condition;
                  PXCondition pxCondition = (PXCondition) condition.Value;
                  object valueExt1 = this.Cache.GetValueExt<FilterRow.valueSt>((object) data);
                  if (valueExt1 is PXFieldState)
                    valueExt1 = ((PXFieldState) valueExt1).Value;
                  if (pxCondition != PXCondition.BETWEEN && pxCondition != PXCondition.ISNULL && pxCondition != PXCondition.ISNOTNULL && (valueExt1 == null || valueExt1 is string && string.IsNullOrEmpty((string) valueExt1)))
                  {
                    object valueExt2 = cach.GetValueExt(current, index1 == 0 ? data.DataField : data.DataField.Substring(str.Length));
                    if (valueExt2 is PXFieldState)
                      valueExt2 = ((PXFieldState) valueExt2).Value;
                    this.Cache.SetValueExt<FilterRow.valueSt>((object) data, valueExt2);
                  }
                }
              }
            }
          }
        }
      }
    }
    return objectList;
  }

  public static PXCache TargetCache(PXGraph graph, Guid? filterID, ref string dataField)
  {
    string viewName = PXFilterDetailView.GetViewName(graph, filterID);
    return !string.IsNullOrEmpty(viewName) && graph.Views.ContainsKey(viewName) ? PXFilterDetailView.CalculateTargetCache(graph.Views[viewName], ref dataField) : (PXCache) null;
  }

  private void AttachEventHandlers()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.Cache.RowInserting += PXFilterDetailView.\u003C\u003EO.\u003C0\u003E__CacheOnRowInserting ?? (PXFilterDetailView.\u003C\u003EO.\u003C0\u003E__CacheOnRowInserting = new PXRowInserting(PXFilterDetailView.CacheOnRowInserting));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.Cache.RowInserted += PXFilterDetailView.\u003C\u003EO.\u003C1\u003E__CacheOnRowInserted ?? (PXFilterDetailView.\u003C\u003EO.\u003C1\u003E__CacheOnRowInserted = new PXRowInserted(PXFilterDetailView.CacheOnRowInserted));
    this.Cache.RowUpdating += new PXRowUpdating(this.CacheOnRowUpdating);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.Cache.RowUpdated += PXFilterDetailView.\u003C\u003EO.\u003C2\u003E__CacheOnRowUpdated ?? (PXFilterDetailView.\u003C\u003EO.\u003C2\u003E__CacheOnRowUpdated = new PXRowUpdated(PXFilterDetailView.CacheOnRowUpdated));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.Cache.RowDeleted += PXFilterDetailView.\u003C\u003EO.\u003C3\u003E__CacheOnRowDeleted ?? (PXFilterDetailView.\u003C\u003EO.\u003C3\u003E__CacheOnRowDeleted = new PXRowDeleted(PXFilterDetailView.CacheOnRowDeleted));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.Cache.RowPersisting += PXFilterDetailView.\u003C\u003EO.\u003C4\u003E__CacheOnRowPersisting ?? (PXFilterDetailView.\u003C\u003EO.\u003C4\u003E__CacheOnRowPersisting = new PXRowPersisting(PXFilterDetailView.CacheOnRowPersisting));
    this.Cache.RowPersisted += new PXRowPersisted(this.CacheOnRowPersisted);
  }

  private void CacheOnRowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (!(e.NewRow is FilterRow newRow))
      return;
    PXFilterDetailView.ValidateFilterRow(sender.Graph, newRow);
    PXFilterDetailView.ValidateFilterVariable(newRow);
  }

  private static void CacheOnRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FilterRow row = e.Row as FilterRow;
    PXFilterDetailView.ValidateFilterRow(sender.Graph, row);
    PXFilterDetailView.ValidateRigths(row);
  }

  private void CacheOnRowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (sender.Graph == null || sender.Graph.GetType().IsSubclassOf(typeof (FilterMaint)) || !string.IsNullOrEmpty(PXUIFieldAttribute.GetError(sender, e.Row, (string) null)))
      return;
    this.RequestRefresh();
  }

  private static void CacheOnRowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is FilterRow row))
      return;
    PXFilterDetailView.ValidateFilterRow(sender.Graph, row);
    PXFilterDetailView.ValidateFilterVariable(row);
  }

  private static void CacheOnRowDeleted(PXCache sender, PXRowDeletedEventArgs args)
  {
    if (sender.Graph == null || sender.Graph.GetType().IsSubclassOf(typeof (FilterMaint)))
      return;
    sender.IsDirty = false;
  }

  private static void CacheOnRowUpdated(PXCache sender, PXRowUpdatedEventArgs args)
  {
    PXFilterDetailView.CorrectValuesFields(sender.Graph, args.Row);
  }

  private static void CacheOnRowInserted(PXCache sender, PXRowInsertedEventArgs args)
  {
    PXFilterDetailView.CorrectValuesFields(sender.Graph, args.Row);
    sender.IsDirty = false;
  }

  private static void ValidateFilterVariable(FilterRow row)
  {
    if (FilterVariable.GetVariableType(row.ValueSt2).HasValue)
      throw new PXSetPropertyException("Filter variables can be used only in the Value field.");
    FilterVariableType? variableType = FilterVariable.GetVariableType(row.ValueSt);
    if (!row.Condition.HasValue)
      return;
    PXCondition condition = (PXCondition) Enum.Parse(typeof (PXCondition), row.Condition.Value.ToString());
    string violationMessage = FilterVariable.GetConditionViolationMessage(variableType.HasValue ? row.ValueSt : string.Empty, condition);
    if (!string.IsNullOrEmpty(violationMessage))
      throw new PXSetPropertyException(violationMessage);
  }

  private static void ValidateFilterRow(PXGraph graph, FilterRow filterRow)
  {
    if (graph == null || graph.GetType().IsSubclassOf(typeof (FilterMaint)) || filterRow == null)
      return;
    if (filterRow.DataField == null)
      throw new PXSetPropertyException("The Property value cannot be empty.");
    if (!filterRow.Condition.HasValue)
      throw new PXSetPropertyException("The Condition value cannot be empty.");
  }

  private static void ValidateRigths(FilterRow filterRow)
  {
    bool flag = PXSiteMap.Provider.FindSiteMapNodeByScreenID("CS209010") != null;
    if (!(filterRow == null | flag) && FilterHeader.Definition.Get().First<FilterHeader>((Func<FilterHeader, bool>) (f =>
    {
      Guid? filterId1 = f.FilterID;
      Guid? filterId2 = filterRow.FilterID;
      if (filterId1.HasValue != filterId2.HasValue)
        return false;
      return !filterId1.HasValue || filterId1.GetValueOrDefault() == filterId2.GetValueOrDefault();
    })).IsShared.GetValueOrDefault())
      throw new PXException("You are not allowed to edit shared filters. If you need to edit this filter contact your system administrator.");
  }

  private static string GetViewName(PXGraph graph, Guid? filterID)
  {
    if (!filterID.HasValue)
      return (string) null;
    return FilterHeader.Definition.Get().SingleOrDefault<FilterHeader>((Func<FilterHeader, bool>) (f =>
    {
      Guid? filterId = f.FilterID;
      Guid? nullable = filterID;
      if (filterId.HasValue != nullable.HasValue)
        return false;
      return !filterId.HasValue || filterId.GetValueOrDefault() == nullable.GetValueOrDefault();
    }))?.ViewName;
  }

  private static void CorrectValuesFields(PXGraph graph, object row)
  {
    if (!(row is FilterRow filterRow))
      return;
    string viewName = PXFilterDetailView.GetViewName(graph, filterRow.FilterID);
    if (string.IsNullOrEmpty(viewName) || !graph.Views.ContainsKey(viewName))
      return;
    PXView view = graph.Views[viewName];
    string dataField = filterRow.DataField;
    ref string local = ref dataField;
    PXCache targetCache = PXFilterDetailView.CalculateTargetCache(view, ref local);
    if (targetCache == null)
      return;
    if (!targetCache.Fields.Contains(dataField))
    {
      filterRow.ValueSt = (string) null;
      filterRow.ValueSt2 = (string) null;
    }
    else
    {
      string str = (string) null;
      if (filterRow.ValueSt == null || filterRow.ValueSt2 == null)
      {
        object val = PXFilterDetailView.GetDefault(targetCache.GetFieldType(dataField));
        str = targetCache.ValueToString(dataField, val);
      }
      if (filterRow.ValueSt == null)
        filterRow.ValueSt = str;
      if (filterRow.ValueSt2 != null)
        return;
      filterRow.ValueSt2 = str;
    }
  }

  private static PXCache CalculateTargetCache(PXView view, ref string field)
  {
    return string.IsNullOrEmpty(field) ? (PXCache) null : PXFilterDetailView.CalculateJoinedTableCache(view, ref field) ?? PXFilterDetailView.CalculatePrimaryTableCache(view, field);
  }

  private static bool IsSelectorDescription(string field)
  {
    return field.EndsWith("_description", StringComparison.OrdinalIgnoreCase);
  }

  private static bool IsSelectorNestedTable(string field)
  {
    return field != null && PXFilterDetailView._selectorNestedFieldsRegex.IsMatch(field);
  }

  private static PXCache CalculatePrimaryTableCache(PXView view, string field)
  {
    return !field.Contains("_") || field.EndsWith("_Attributes") || view.Graph is PXGenericInqGrph || PXFilterDetailView.IsSelectorNestedTable(field) || PXFilterDetailView.IsSelectorDescription(field) ? view.Cache : (PXCache) null;
  }

  private static PXCache CalculateJoinedTableCache(PXView view, ref string field)
  {
    int length = field.IndexOf("__");
    if (length < 0)
      return (PXCache) null;
    if (length >= field.Length - "__".Length)
      return (PXCache) null;
    string b = field.Substring(0, length);
    string str = field.Substring(length + "__".Length);
    foreach (System.Type table in view.BqlSelect.GetTables())
    {
      if (string.Equals(table.Name, b, StringComparison.OrdinalIgnoreCase))
      {
        field = str;
        return view.Graph.Caches[table];
      }
    }
    return (PXCache) null;
  }

  private static object GetDefault(System.Type type)
  {
    switch (System.Type.GetTypeCode(type))
    {
      case TypeCode.Boolean:
        return (object) false;
      case TypeCode.Char:
        return (object) char.MinValue;
      case TypeCode.SByte:
        return (object) (sbyte) 0;
      case TypeCode.Byte:
        return (object) (byte) 0;
      case TypeCode.Int16:
        return (object) (short) 0;
      case TypeCode.UInt16:
        return (object) (ushort) 0;
      case TypeCode.UInt32:
        return (object) 0U;
      case TypeCode.Int64:
        return (object) 0L;
      case TypeCode.UInt64:
        return (object) 0UL;
      case TypeCode.Single:
        return (object) 0.0f;
      case TypeCode.Double:
        return (object) 0.0;
      case TypeCode.Decimal:
        return (object) 0M;
      case TypeCode.DateTime:
        return (object) null;
      case TypeCode.String:
        return (object) string.Empty;
      default:
        return (object) null;
    }
  }

  private void ClearCache(object filterId)
  {
    List<FilterRow> filterRowList = new List<FilterRow>();
    if (filterId == null)
    {
      foreach (FilterRow filterRow in this._Cache.Cached)
      {
        if (this._Cache.GetStatus((object) filterRow) != PXEntryStatus.Inserted)
          filterRowList.Add(filterRow);
      }
    }
    else
    {
      Guid guid1 = (Guid) filterId;
      foreach (FilterRow filterRow in this._Cache.Cached)
      {
        Guid? filterId1 = filterRow.FilterID;
        Guid guid2 = guid1;
        if ((filterId1.HasValue ? (filterId1.HasValue ? (filterId1.GetValueOrDefault() != guid2 ? 1 : 0) : 0) : 1) != 0)
          filterRowList.Add(filterRow);
      }
    }
    foreach (object obj in filterRowList)
      this._Cache.Remove(obj);
  }
}
