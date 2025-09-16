// Decompiled with JetBrains decompiler
// Type: PX.Data.PXOrderedSelectBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>The abstract class that provides the functionality that allows users to drag rows, cut and paste rows, and insert new rows in the middle of the grid.</summary>
/// <typeparam name="Primary">The primary DAC in the graph.</typeparam>
/// <typeparam name="Table">The DAC of the grid rows.</typeparam>
public abstract class PXOrderedSelectBase<Primary, Table> : PXSelectBase<Table>
  where Primary : class, IBqlTable, new()
  where Table : class, IBqlTable, ISortOrder, new()
{
  /// <exclude />
  public const string PasteLineCommand = "PasteLine";
  /// <exclude />
  public const string ResetOrderCommand = "ResetOrder";

  /// <summary>
  /// Default value is TRUE. Performance customization point. Override in Initialize() method to allow gaps in numbering when record is deleted.
  /// </summary>
  protected bool RenumberTailOnDelete { get; set; } = true;

  public bool RenumberAllBeforePersist { get; set; } = true;

  /// <summary>Initializes a new instance of the class and adds the actions related to sorting to the graph.</summary>
  public virtual void Initialize()
  {
    this.AddActions(this._Graph);
    this._Graph.RowInserted.AddHandler<Table>(new PXRowInserted(this.OnRowInserted));
    this._Graph.RowDeleted.AddHandler<Table>(new PXRowDeleted(this.OnRowDeleted));
    this._Graph.OnBeforePersist += new System.Action<PXGraph>(this.OnBeforeGraphPersist);
  }

  /// <exclude />
  public IComparer<PXResult> CustomComparer { get; set; }

  [PXButton]
  protected virtual IEnumerable PasteLine(PXAdapter adapter)
  {
    PXSortColumnsTuple externalSortsWithDefs = this.View.GetExternalSortsWithDefs();
    if (externalSortsWithDefs.SortColumns != null && externalSortsWithDefs.SortColumns.Length != 0 && (externalSortsWithDefs.SortColumns.Length != 1 || !string.Equals(externalSortsWithDefs.SortColumns[0], "SortOrder", StringComparison.InvariantCultureIgnoreCase)) && !externalSortsWithDefs.IsOnlyDefSorts)
      throw new PXException("The lines cannot be rearranged, custom sorting has been applied. Clear the sorting, before rearranging the lines.");
    Table focus = this.GetFocus();
    if ((object) focus != null)
    {
      IList<Table> moved = this.GetMoved();
      if (moved.Count > 0)
        this.PasteLines(focus, moved);
    }
    return adapter.Get();
  }

  [PXButton]
  protected virtual IEnumerable ResetOrder(PXAdapter adapter)
  {
    if (this.CustomComparer != null)
      this.RenumberAll(this.CustomComparer);
    else
      this.RenumberAll();
    return adapter.Get();
  }

  protected virtual void PasteLines(Table focus, IList<Table> moved)
  {
    if (!this.Cache.AllowUpdate)
      return;
    int? nullable = focus.SortOrder;
    int valueOrDefault1 = nullable.GetValueOrDefault();
    int sortOrder1 = valueOrDefault1;
    HashSet<int> intSet1 = new HashSet<int>();
    foreach (Table able in (IEnumerable<Table>) moved)
    {
      HashSet<int> intSet2 = intSet1;
      nullable = able.LineNbr;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      intSet2.Add(valueOrDefault2);
      nullable = able.SortOrder;
      int num = sortOrder1;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue)
      {
        nullable = able.SortOrder;
        sortOrder1 = nullable.Value;
      }
    }
    List<Table> head = new List<Table>();
    List<Table> body = new List<Table>();
    List<Table> tail = new List<Table>();
    foreach (PXResult<Table> pxResult in this.Select())
    {
      Table able = (Table) pxResult;
      if (intSet1.Contains(able.LineNbr.GetValueOrDefault()))
      {
        body.Add(able);
      }
      else
      {
        int? sortOrder2 = able.SortOrder;
        int num1 = sortOrder1;
        if (sortOrder2.GetValueOrDefault() >= num1 & sortOrder2.HasValue)
        {
          if (this.Cache.InsertPositionMode)
          {
            int? sortOrder3 = able.SortOrder;
            int num2 = valueOrDefault1;
            if (sortOrder3.GetValueOrDefault() <= num2 & sortOrder3.HasValue)
            {
              head.Add(able);
            }
            else
            {
              int? sortOrder4 = able.SortOrder;
              int num3 = valueOrDefault1;
              if (sortOrder4.GetValueOrDefault() > num3 & sortOrder4.HasValue)
                tail.Add(able);
            }
          }
          else
          {
            int? sortOrder5 = able.SortOrder;
            int num4 = valueOrDefault1;
            if (sortOrder5.GetValueOrDefault() < num4 & sortOrder5.HasValue)
            {
              head.Add(able);
            }
            else
            {
              int? sortOrder6 = able.SortOrder;
              int num5 = valueOrDefault1;
              if (sortOrder6.GetValueOrDefault() >= num5 & sortOrder6.HasValue)
                tail.Add(able);
            }
          }
        }
      }
    }
    this.ReEnumerateOnPasteLines(head, body, tail, sortOrder1, valueOrDefault1);
    this.View.Clear();
  }

  protected virtual void ReEnumerateOnPasteLines(
    List<Table> head,
    List<Table> body,
    List<Table> tail,
    int sortOrder,
    int insertPos)
  {
    head.AddRange((IEnumerable<Table>) body);
    head.AddRange((IEnumerable<Table>) tail);
    int num1 = sortOrder;
    foreach (Table able in head)
    {
      int? sortOrder1 = able.SortOrder;
      int num2 = num1;
      if (!(sortOrder1.GetValueOrDefault() == num2 & sortOrder1.HasValue))
      {
        this.Cache.SetValue((object) able, "SortOrder", (object) num1);
        this.Cache.MarkUpdated((object) able);
        this.Cache.IsDirty = true;
      }
      ++num1;
    }
  }

  protected virtual void AddActions(PXGraph graph)
  {
    this.AddAction(graph, "PasteLine", "Paste Line", new PXButtonDelegate(this.PasteLine));
    this.AddAction(graph, "ResetOrder", "Reset Order", new PXButtonDelegate(this.ResetOrder));
  }

  protected PXAction AddAction(
    PXGraph graph,
    string name,
    string displayName,
    PXButtonDelegate handler,
    List<PXEventSubscriberAttribute> attributes = null)
  {
    PXUIFieldAttribute pxuiFieldAttribute = new PXUIFieldAttribute()
    {
      DisplayName = PXMessages.LocalizeNoPrefix(displayName),
      MapEnableRights = PXCacheRights.Select
    };
    PXButtonAttribute pxButtonAttribute = new PXButtonAttribute()
    {
      DisplayOnMainToolbar = false
    };
    if (attributes == null)
    {
      attributes = new List<PXEventSubscriberAttribute>()
      {
        (PXEventSubscriberAttribute) pxuiFieldAttribute,
        (PXEventSubscriberAttribute) pxButtonAttribute
      };
    }
    else
    {
      attributes.Add((PXEventSubscriberAttribute) pxuiFieldAttribute);
      attributes.Add((PXEventSubscriberAttribute) pxButtonAttribute);
    }
    PXNamedAction<Primary> pxNamedAction = new PXNamedAction<Primary>(graph, name, handler, attributes.ToArray());
    graph.Actions[name] = (PXAction) pxNamedAction;
    return (PXAction) pxNamedAction;
  }

  protected virtual Table GetFocus() => this.GetItemByKeys(this.Cache.InsertPosition);

  protected virtual IList<Table> GetMoved()
  {
    List<Table> moved = new List<Table>();
    if (this.Cache.RowsToMove != null)
    {
      foreach (Dictionary<string, object> keys in this.Cache.RowsToMove)
      {
        Table itemByKeys = this.GetItemByKeys(keys);
        if ((object) itemByKeys != null)
          moved.Add(itemByKeys);
      }
    }
    return (IList<Table>) moved;
  }

  protected virtual Table GetItemByKeys(Dictionary<string, object> keys)
  {
    Table itemByKeys = default (Table);
    if (keys != null && keys.Count > 0)
    {
      object current = this.Cache.Current;
      try
      {
        if (this.Cache.Locate((IDictionary) keys) == 1)
          itemByKeys = this.Cache.Current as Table;
      }
      finally
      {
        this.Cache.Current = current;
      }
    }
    return itemByKeys;
  }

  protected virtual void OnRowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    Table focus = this.GetFocus();
    if ((object) focus != null)
      this.InsertAboveFocus((Table) e.Row, focus);
    ISortOrder row = (ISortOrder) e.Row;
    if (row.SortOrder.HasValue)
      return;
    row.SortOrder = row.LineNbr;
  }

  protected virtual void OnRowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (e.Row == null)
      return;
    Table row = (Table) e.Row;
    if (!row.SortOrder.HasValue || this.IsPrimaryEntityNewlyInserted || this.IsPrimaryEntityDeleted || !this.RenumberTailOnDelete)
      return;
    int num1 = row.SortOrder.Value;
    bool flag = false;
    foreach (PXResult<Table> pxResult in this.Select())
    {
      Table able = (Table) pxResult;
      if (!flag)
      {
        int? sortOrder = able.SortOrder;
        int num2 = num1;
        if (sortOrder.GetValueOrDefault() >= num2 & sortOrder.HasValue)
          flag = true;
        else
          continue;
      }
      if (flag)
      {
        this.Cache.SetValue((object) able, "SortOrder", (object) num1);
        this.Cache.MarkUpdated((object) able);
        ++num1;
      }
    }
  }

  protected virtual void InsertAboveFocus(Table row, Table focus)
  {
    int num1 = focus.SortOrder.Value;
    int num2 = num1;
    bool flag = false;
    foreach (PXResult<Table> pxResult in this.Select())
    {
      Table able = (Table) pxResult;
      int? lineNbr = able.LineNbr;
      int? nullable = row.LineNbr;
      if (!(lineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & lineNbr.HasValue == nullable.HasValue))
      {
        if (!flag)
        {
          nullable = able.SortOrder;
          int num3 = num2;
          if (nullable.GetValueOrDefault() >= num3 & nullable.HasValue)
            flag = true;
          else
            continue;
        }
        if (flag)
        {
          ++num2;
          this.Cache.SetValue((object) able, "SortOrder", (object) num2);
          this.Cache.MarkUpdated((object) able);
        }
      }
    }
    this.Cache.SetValue((object) row, "SortOrder", (object) num1);
    this.Cache.MarkUpdated((object) row);
  }

  protected virtual void OnBeforeGraphPersist(PXGraph graph)
  {
    if (!this.RenumberAllBeforePersist)
      return;
    if (this.IsPrimaryEntityNewlyInserted)
      this.RenumberAll();
    else
      this.RenumberTail();
  }

  /// <summary>Renumbers all rows in the grid.</summary>
  public virtual void RenumberAll()
  {
    int num1 = 0;
    foreach (PXResult<Table> pxResult in this.Select())
    {
      Table able = (Table) pxResult;
      ++num1;
      int? sortOrder = able.SortOrder;
      int num2 = num1;
      if (!(sortOrder.GetValueOrDefault() == num2 & sortOrder.HasValue))
      {
        this.Cache.SetValue((object) able, "SortOrder", (object) num1);
        this.Cache.MarkUpdated((object) able);
        this.Cache.IsDirty = true;
      }
    }
  }

  /// <summary>Renumbers only the inserted rows in the grid.</summary>
  public virtual void RenumberTail()
  {
    List<Table> source = new List<Table>();
    int val1 = 0;
    foreach (Table able in this.Cache.Inserted)
    {
      if (this.Cache.GetStatus((object) able) == PXEntryStatus.Inserted)
        source.Add(able);
    }
    if (source.Count == 0)
      return;
    foreach (PXResult<Table> pxResult in this.Select())
    {
      Table able = (Table) pxResult;
      if (this.Cache.GetStatus((object) able) != PXEntryStatus.Inserted && this.Cache.GetStatus((object) able) != PXEntryStatus.Deleted)
        val1 = System.Math.Max(val1, able.SortOrder.GetValueOrDefault());
    }
    foreach (Table able in source.OrderBy<Table, bool>((Func<Table, bool>) (x => x.SortOrder.HasValue)).ThenBy<Table, int?>((Func<Table, int?>) (x => x.SortOrder)).ThenBy<Table, int?>((Func<Table, int?>) (x => x.LineNbr)).ToList<Table>())
    {
      int? sortOrder = able.SortOrder;
      int num = val1;
      if (sortOrder.GetValueOrDefault() > num & sortOrder.HasValue)
      {
        ++val1;
        this.Cache.SetValue((object) able, "SortOrder", (object) val1);
        this.Cache.MarkUpdated((object) able);
        this.Cache.IsDirty = true;
      }
    }
  }

  /// <summary>Renumbers all rows in the grid by using the specified comparer.</summary>
  /// <param name="comparer">A type that defines a custom comparison of rows.</param>
  public virtual void RenumberAll(IComparer<PXResult> comparer)
  {
    PXResultset<Table> pxResultset = this.Select();
    List<PXResult> pxResultList = new List<PXResult>(pxResultset.Count);
    foreach (PXResult pxResult in pxResultset)
      pxResultList.Add(pxResult);
    pxResultList.Sort(comparer);
    int num1 = 0;
    foreach (PXResult row in pxResultList)
    {
      ++num1;
      Table able = PXResult.Unwrap<Table>((object) row);
      int? sortOrder = able.SortOrder;
      int num2 = num1;
      if (!(sortOrder.GetValueOrDefault() == num2 & sortOrder.HasValue))
      {
        this.Cache.SetValue((object) able, "SortOrder", (object) num1);
        this.Cache.MarkUpdated((object) able);
        this.Cache.IsDirty = true;
      }
    }
    this.View.Clear();
  }

  protected virtual bool IsPrimaryEntityNewlyInserted
  {
    get
    {
      object current = this._Graph.Caches[typeof (Primary)].Current;
      return current != null && this._Graph.Caches[typeof (Primary)].GetStatus(current) == PXEntryStatus.Inserted;
    }
  }

  protected virtual bool IsPrimaryEntityDeleted
  {
    get
    {
      object current = this._Graph.Caches[typeof (Primary)].Current;
      if (current == null)
        return false;
      PXEntryStatus status = this._Graph.Caches[typeof (Primary)].GetStatus(current);
      return status == PXEntryStatus.Deleted || status == PXEntryStatus.InsertedDeleted;
    }
  }
}
