// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAddItemLookup`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.TM;
using System;

#nullable disable
namespace PX.Objects.AP;

[Obsolete("This class is obsolete. Use APPriceWorksheetAddItemLookupExt instead.")]
public class APAddItemLookup<Status, StatusFilter> : INSiteStatusLookup<Status, StatusFilter>
  where Status : class, IBqlTable, new()
  where StatusFilter : AddItemFilter, new()
{
  public APAddItemLookup(PXGraph graph)
    : base(graph)
  {
    graph.RowSelecting.AddHandler(typeof (APAddItemSelected), new PXRowSelecting(this.OnRowSelecting));
  }

  public APAddItemLookup(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    graph.RowSelecting.AddHandler(typeof (APAddItemSelected), new PXRowSelecting(this.OnRowSelecting));
  }

  protected virtual void OnRowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
  }

  protected override void OnFilterSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.OnFilterSelected(sender, e);
    AddItemFilter row1 = (AddItemFilter) e.Row;
    PXUIFieldAttribute.SetVisible<APAddItemSelected.curyID>(sender.Graph.Caches[typeof (APAddItemSelected)], (object) null, true);
    PXCache cache1 = sender;
    object row2 = e.Row;
    bool? nullable;
    int num1;
    if (row1 == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = row1.MyWorkGroup;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<AddItemFilter.workGroupID>(cache1, row2, num1 != 0);
    PXCache cache2 = sender;
    object row3 = e.Row;
    int num2;
    if (row1 == null)
    {
      num2 = 1;
    }
    else
    {
      nullable = row1.MyOwner;
      num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<AddItemFilter.ownerID>(cache2, row3, num2 != 0);
  }

  protected override PXView CreateIntView(PXGraph graph)
  {
    PXView intView = base.CreateIntView(graph);
    intView.WhereAnd<Where<PX.Data.Current<AddItemFilter.ownerID>, IsNull, Or<PX.Data.Current<AddItemFilter.ownerID>, Equal<APAddItemSelected.productManagerID>>>>();
    intView.WhereAnd<Where<PX.Data.Current<AddItemFilter.myWorkGroup>, Equal<boolFalse>, Or<APAddItemSelected.productWorkgroupID, IsWorkgroupOfContact<CurrentValue<AddItemFilter.currentOwnerID>>>>>();
    intView.WhereAnd<Where<PX.Data.Current<AddItemFilter.workGroupID>, IsNull, Or<PX.Data.Current<AddItemFilter.workGroupID>, Equal<APAddItemSelected.productWorkgroupID>>>>();
    return intView;
  }
}
