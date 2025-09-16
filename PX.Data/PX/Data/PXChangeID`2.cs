// Decompiled with JetBrains decompiler
// Type: PX.Data.PXChangeID`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXChangeID<Table, Field> : PXAction<Table>
  where Table : class, IBqlTable, new()
  where Field : class, IBqlField
{
  protected const string ChangeIdDialogView = "ChangeIDDialog";
  protected string DuplicatedKeyMessage = "The {0} identifier is already used for another business account record (vendor, customer, employee, branch, company, or company group).";
  private PXFilter<ChangeIDParam> filter;

  protected bool HasError
  {
    get
    {
      PXFilter<ChangeIDParam> filter = this.filter;
      return filter != null && filter.HasError<ChangeIDParam.cD>();
    }
  }

  public PXChangeID(PXGraph graph, string name)
    : base(graph, name)
  {
    this.Initialize();
  }

  public PXChangeID(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    this.Initialize();
  }

  protected virtual void Initialize()
  {
    this.filter = new PXFilter<ChangeIDParam>(this._Graph);
    PXUIFieldAttribute.SetDisplayName<ChangeIDParam.cD>((this._Graph.Views["ChangeIDDialog"] = this.filter.View).Cache, PXUIFieldAttribute.GetNeutralDisplayName(this._Graph.Caches[typeof (Table)], typeof (Field).Name));
    this._Graph.RowSelected.AddHandler<Table>(new PXRowSelected(this.RowSelected));
    this._Graph.FieldSelecting.AddHandler<ChangeIDParam.cD>(new PXFieldSelecting(this.FieldSelecting));
    this._Graph.FieldUpdating.AddHandler<ChangeIDParam.cD>(new PXFieldUpdating(this.FieldUpdating));
    this._Graph.FieldVerifying.AddHandler<ChangeIDParam.cD>(new PXFieldVerifying(this.FieldVerifying));
  }

  public static void ChangeCD(PXCache cache, string oldCD, string newCD)
  {
    PXChangeID<Table, Field>.ChangeCD<Field>(cache, oldCD, newCD);
  }

  public static void ChangeCD<TField>(PXCache cache, string oldCD, string newCD) where TField : class, IBqlField
  {
    OrderedDictionary keys = new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase)
    {
      {
        (object) typeof (TField).Name,
        (object) oldCD
      }
    };
    OrderedDictionary values = new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase)
    {
      {
        (object) typeof (TField).Name,
        (object) newCD
      }
    };
    foreach (string key in (IEnumerable<string>) cache.Keys)
    {
      if (!typeof (TField).Name.Equals(key, StringComparison.OrdinalIgnoreCase))
        keys.Add((object) key, PXChangeID<Table, Field>.GetValue(cache, key));
    }
    cache.Update((IDictionary) keys, (IDictionary) values);
  }

  /// <summary>
  /// Sets error message text to be raised when specified key already exists
  /// </summary>
  /// <param name="messageTemplate">Error message template with {0} placeholder for the field value</param>
  public virtual void SetDuplicateKeyMessage(string messageTemplate)
  {
    this.DuplicatedKeyMessage = messageTemplate;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Change ID", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    string newCd;
    if (adapter.View.Cache.Current != null && adapter.View.Cache.GetStatus(adapter.View.Cache.Current) != PXEntryStatus.Inserted && this.filter.AskExtFullyValid(DialogAnswerType.Positive) && !string.IsNullOrWhiteSpace(newCd = PXChangeID<Table, Field>.GetNewCD(adapter)))
    {
      PXChangeID<Table, Field>.ChangeCD(adapter.View.Cache, PXChangeID<Table, Field>.GetOldCD(adapter), newCd);
      if (adapter.SortColumns != null && adapter.SortColumns.Length != 0 && string.Equals(adapter.SortColumns[0], typeof (Field).Name, StringComparison.OrdinalIgnoreCase) && adapter.Searches != null && adapter.Searches.Length != 0)
        adapter.Searches[0] = (object) newCd;
    }
    if (this.Graph.IsContractBasedAPI)
      this.Graph.Actions.PressSave();
    return adapter.Get();
  }

  protected static string GetNewCD(PXAdapter adapter)
  {
    return ((ChangeIDParam) adapter.View.Cache.Graph.Caches[typeof (ChangeIDParam)].Current).CD;
  }

  protected static string GetOldCD(PXAdapter adapter)
  {
    return (string) adapter.View.Cache.GetValue(adapter.View.Cache.Current, typeof (Field).Name);
  }

  protected static object GetValue(PXCache Cache, string FieldName)
  {
    return Cache.GetValue(Cache.Current, FieldName);
  }

  protected virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    this.SetEnabled(sender.GetStatus(e.Row) != PXEntryStatus.Inserted);
  }

  protected virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    e.IsAltered = true;
    object returnValue = e.ReturnValue;
    PXDimensionAttribute dimensionAttribute1 = (PXDimensionAttribute) null;
    foreach (PXDimensionAttribute dimensionAttribute2 in sender.Graph.Caches[typeof (Table)].GetAttributesReadonly<Field>().OfType<PXDimensionAttribute>())
    {
      if (dimensionAttribute2.ValidComboRequired)
      {
        dimensionAttribute2.ValidComboRequired = false;
        dimensionAttribute1 = dimensionAttribute2;
        break;
      }
    }
    sender.Graph.Caches[typeof (Table)].RaiseFieldSelecting<Field>((object) null, ref returnValue, true);
    if (returnValue is PXFieldState pxFieldState)
    {
      pxFieldState.ViewName = (string) null;
      pxFieldState.FieldList = (string[]) null;
      pxFieldState.HeaderList = (string[]) null;
      pxFieldState.ValueField = (string) null;
      pxFieldState.DescriptionName = (string) null;
    }
    if (dimensionAttribute1 != null)
      dimensionAttribute1.ValidComboRequired = true;
    e.ReturnState = returnValue;
  }

  protected virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    object newValue = e.NewValue;
    sender.Graph.Caches[typeof (Table)].RaiseFieldUpdating<Field>((object) null, ref newValue);
    e.NewValue = newValue;
    PXUIFieldAttribute.SetError<ChangeIDParam.cD>(sender, e.Row, (string) null);
  }

  protected virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (PXSelectBase<Table, PXSelectReadonly<Table, Where<Field, Equal<Required<Field>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, e.NewValue).AsEnumerable<PXResult<Table>>().Any<PXResult<Table>>())
      throw new PXSetPropertyException(this.DuplicatedKeyMessage, new object[1]
      {
        (object) ((string) e.NewValue).Trim()
      });
    object newValue = e.NewValue;
    sender.Graph.Caches[typeof (Table)].RaiseFieldVerifying<Field>((object) null, ref newValue);
    e.NewValue = newValue;
    PXUIFieldAttribute.SetError<ChangeIDParam.cD>(sender, e.Row, (string) null);
  }
}
