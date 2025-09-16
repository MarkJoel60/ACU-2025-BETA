// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemXRefBAccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CR;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.IN;

public class INItemXRefBAccountAttribute : BAccountAttribute
{
  public override bool HideInactiveCustomers { get; set; }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatingEvents fieldUpdating1 = sender.Graph.FieldUpdating;
    PXDimensionSelectorAttribute selectorAttribute1 = this.SelectorAttribute;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) selectorAttribute1, __vmethodptr(selectorAttribute1, FieldUpdating));
    fieldUpdating1.RemoveHandler<INItemXRef.bAccountID>(pxFieldUpdating1);
    PXGraph.FieldUpdatingEvents fieldUpdating2 = sender.Graph.FieldUpdating;
    INItemXRefBAccountAttribute baccountAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) baccountAttribute1, __vmethodptr(baccountAttribute1, FieldUpdating));
    fieldUpdating2.AddHandler<INItemXRef.bAccountID>(pxFieldUpdating2);
    PXGraph.FieldSelectingEvents fieldSelecting1 = sender.Graph.FieldSelecting;
    PXDimensionSelectorAttribute selectorAttribute2 = this.SelectorAttribute;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting1 = new PXFieldSelecting((object) selectorAttribute2, __vmethodptr(selectorAttribute2, FieldSelecting));
    fieldSelecting1.RemoveHandler<INItemXRef.bAccountID>(pxFieldSelecting1);
    PXGraph.FieldSelectingEvents fieldSelecting2 = sender.Graph.FieldSelecting;
    INItemXRefBAccountAttribute baccountAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting2 = new PXFieldSelecting((object) baccountAttribute2, __vmethodptr(baccountAttribute2, FieldSelecting));
    fieldSelecting2.AddHandler<INItemXRef.bAccountID>(pxFieldSelecting2);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    INItemXRef row = (INItemXRef) e.Row;
    bool? nullable;
    if (row == null)
    {
      nullable = new bool?();
    }
    else
    {
      string alternateType = row.AlternateType;
      nullable = alternateType != null ? new bool?(EnumerableExtensions.IsNotIn<string>(alternateType, "0CPN", "0VPN")) : new bool?();
    }
    if (nullable ?? true)
      e.ReturnValue = (object) null;
    this.SelectorAttribute.FieldSelecting(sender, e);
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    this.SelectorAttribute.FieldUpdating(sender, e);
    INItemXRef row = (INItemXRef) e.Row;
    bool? nullable;
    if (row == null)
    {
      nullable = new bool?();
    }
    else
    {
      string alternateType = row.AlternateType;
      nullable = alternateType != null ? new bool?(EnumerableExtensions.IsNotIn<string>(alternateType, "0CPN", "0VPN")) : new bool?();
    }
    if (!(nullable ?? true))
      return;
    e.NewValue = (object) 0;
    ((CancelEventArgs) e).Cancel = true;
  }
}
