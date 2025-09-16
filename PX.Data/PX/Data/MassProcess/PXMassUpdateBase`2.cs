// Decompiled with JetBrains decompiler
// Type: PX.Data.MassProcess.PXMassUpdateBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.MassProcess;

/// <exclude />
public abstract class PXMassUpdateBase<TGraph, TPrimary> : 
  PXMassProcessBase<TGraph, TPrimary>,
  IMassProcess<TPrimary>
  where TGraph : PXGraph, IMassProcess<TPrimary>, new()
  where TPrimary : class, IBqlTable, new()
{
  public PXSelect<FieldValue, Where<FieldValue.attributeID, IsNull>, OrderBy<Asc<FieldValue.order>>> Fields;

  protected PXMassUpdateBase()
  {
    typeof (FieldValue).GetCustomAttributes(typeof (PXVirtualAttribute), false);
  }

  public IEnumerable fields(PXAdapter adapter)
  {
    return (IEnumerable) this.Caches[typeof (FieldValue)].Cached.Cast<FieldValue>().Where<FieldValue>((Func<FieldValue, bool>) (row => row.AttributeID == null));
  }

  protected virtual void FieldValue_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    FieldValue newRow = (FieldValue) e.NewRow;
    FieldValue row = (FieldValue) e.Row;
    FieldValue fieldValue = newRow;
    bool? selected = newRow.Selected;
    bool flag = true;
    bool? nullable = new bool?(selected.GetValueOrDefault() == flag & selected.HasValue || row.Value != newRow.Value);
    fieldValue.Selected = nullable;
  }

  protected virtual void FieldValue_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) this.InitValueFieldState(e.Row as FieldValue);
  }

  protected PXFieldState InitValueFieldState(FieldValue field = null)
  {
    return PXMassProcessHelper.InitValueFieldState(this.Caches[typeof (TPrimary)], field);
  }

  public static IEnumerable<FieldValue> GetProcessingProperties(
    PXGraph graph,
    ref int firstSortOrder)
  {
    PXCache cache = graph.Caches[typeof (TPrimary)];
    int order = firstSortOrder;
    IEnumerable<FieldValue> processingProperties = ((IEnumerable<PropertyInfo>) cache.BqlTable.GetProperties(BindingFlags.Instance | BindingFlags.Public)).Select(p => new
    {
      p = p,
      t = new
      {
        Name = p.Name,
        State = cache.GetStateExt((object) null, p.Name) as PXFieldState
      }
    }).Where(_param1 => _param1.t.State != null).Select(_param1 => new FieldValue()
    {
      Selected = new bool?(false),
      CacheName = typeof (TPrimary).FullName,
      Name = _param1.t.Name,
      DisplayName = _param1.t.State.DisplayName,
      AttributeID = (string) null,
      Order = new int?(order++)
    });
    firstSortOrder = order;
    return processingProperties;
  }

  protected virtual IEnumerable<FieldValue> ProcessingProperties
  {
    get
    {
      int firstSortOrder = 0;
      return PXMassUpdateBase<TGraph, TPrimary>.GetProcessingProperties((PXGraph) this, ref firstSortOrder);
    }
  }

  protected void FillPropertyValue(PXGraph graph, string viewName)
  {
    PXCache cach = this.Caches[typeof (FieldValue)];
    cach.Clear();
    foreach (FieldValue processingProperty in this.ProcessingProperties)
      cach.Insert((object) processingProperty);
    cach.IsDirty = false;
  }

  protected override bool AskParameters()
  {
    return this.Fields.AskExt(new PXView.InitializePanel(this.FillPropertyValue)) == WebDialogResult.OK;
  }

  public override void ProccessItem(PXGraph graph, TPrimary item)
  {
    PXCache cach = graph.Caches[typeof (TPrimary)];
    TPrimary instance = (TPrimary) cach.CreateInstance();
    PXCache<TPrimary>.RestoreCopy(instance, item);
    PXView view = graph.Views[graph.PrimaryView];
    object[] searches = new object[view.Cache.BqlKeys.Count];
    string[] sortcolumns = new string[view.Cache.BqlKeys.Count];
    for (int index = 0; index < cach.BqlKeys.Count<System.Type>(); ++index)
    {
      sortcolumns[index] = cach.BqlKeys[index].Name;
      searches[index] = cach.GetValue((object) instance, sortcolumns[index]);
    }
    int startRow = 0;
    int totalRows = 0;
    List<object> objectList1 = view.Select((object[]) null, (object[]) null, searches, sortcolumns, (bool[]) null, (PXFilterRow[]) null, ref startRow, 1, ref totalRows);
    TPrimary copy = (TPrimary) cach.CreateCopy((object) PXResult.Unwrap<TPrimary>(objectList1[0]));
    foreach (FieldValue fieldValue1 in this.Fields.Cache.Cached.Cast<FieldValue>().Where<FieldValue>((Func<FieldValue, bool>) (o =>
    {
      if (o.AttributeID != null)
        return false;
      bool? selected = o.Selected;
      bool flag = true;
      return selected.GetValueOrDefault() == flag & selected.HasValue;
    })))
    {
      FieldValue fieldValue = fieldValue1;
      PXFieldState stateExt = cach.GetStateExt((object) copy, fieldValue.Name) as PXFieldState;
      PXIntState pxIntState = stateExt as PXIntState;
      PXStringState pxStringState = stateExt as PXStringState;
      if (pxIntState != null && pxIntState.AllowedValues != null && pxIntState.AllowedValues.Length != 0 && ((IEnumerable<int>) pxIntState.AllowedValues).All<int>((Func<int, bool>) (v => v != int.Parse(fieldValue.Value))) || pxStringState != null && pxStringState.AllowedValues != null && pxStringState.AllowedValues.Length != 0 && ((IEnumerable<string>) pxStringState.AllowedValues).All<string>((Func<string, bool>) (v => v != fieldValue.Value)))
        throw new PXSetPropertyException("The list value '{0}' is not allowed for the {1} field.", new object[2]
        {
          (object) fieldValue.Value,
          (object) fieldValue.Name
        });
      if (stateExt != null && !object.Equals(stateExt.Value, (object) fieldValue.Value))
      {
        cach.SetValueExt((object) copy, fieldValue.Name, (object) fieldValue.Value);
        cach.Update((object) copy);
      }
      List<object> objectList2 = view.Select((object[]) null, (object[]) null, searches, sortcolumns, (bool[]) null, (PXFilterRow[]) null, ref startRow, 1, ref totalRows);
      copy = (TPrimary) cach.CreateCopy((object) PXResult.Unwrap<TPrimary>(objectList2[0]));
    }
  }
}
