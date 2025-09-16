// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAddItemLookup`2
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
namespace PX.Objects.AR;

[Obsolete("This class is obsolete. Use ARPriceWorksheetAddItemLookupExt instead.")]
public class ARAddItemLookup<Status, StatusFilter> : INSiteStatusLookup<Status, StatusFilter>
  where Status : class, IBqlTable, new()
  where StatusFilter : AddItemFilter, new()
{
  public ARAddItemLookup(PXGraph graph)
    : base(graph)
  {
    PXGraph.RowSelectingEvents rowSelecting = graph.RowSelecting;
    Type type = typeof (ARAddItemSelected);
    ARAddItemLookup<Status, StatusFilter> arAddItemLookup = this;
    // ISSUE: virtual method pointer
    PXRowSelecting pxRowSelecting = new PXRowSelecting((object) arAddItemLookup, __vmethodptr(arAddItemLookup, OnRowSelecting));
    rowSelecting.AddHandler(type, pxRowSelecting);
  }

  public ARAddItemLookup(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    PXGraph.RowSelectingEvents rowSelecting = graph.RowSelecting;
    Type type = typeof (ARAddItemSelected);
    ARAddItemLookup<Status, StatusFilter> arAddItemLookup = this;
    // ISSUE: virtual method pointer
    PXRowSelecting pxRowSelecting = new PXRowSelecting((object) arAddItemLookup, __vmethodptr(arAddItemLookup, OnRowSelecting));
    rowSelecting.AddHandler(type, pxRowSelecting);
  }

  protected virtual void OnRowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
  }

  protected override void OnFilterSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.OnFilterSelected(sender, e);
    AddItemFilter row1 = (AddItemFilter) e.Row;
    PXUIFieldAttribute.SetVisible<ARAddItemSelected.curyID>(sender.Graph.Caches[typeof (ARAddItemSelected)], (object) null, true);
    PXCache pxCache1 = sender;
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
    PXUIFieldAttribute.SetEnabled<AddItemFilter.workGroupID>(pxCache1, row2, num1 != 0);
    PXCache pxCache2 = sender;
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
    PXUIFieldAttribute.SetEnabled<AddItemFilter.ownerID>(pxCache2, row3, num2 != 0);
  }

  protected override PXView CreateIntView(PXGraph graph)
  {
    PXView intView = base.CreateIntView(graph);
    intView.WhereAnd<Where<Current<AddItemFilter.ownerID>, IsNull, Or<Current<AddItemFilter.ownerID>, Equal<ARAddItemSelected.priceManagerID>>>>();
    intView.WhereAnd<Where<Current<AddItemFilter.myWorkGroup>, Equal<boolFalse>, Or<ARAddItemSelected.priceWorkgroupID, IsWorkgroupOfContact<CurrentValue<AddItemFilter.currentOwnerID>>>>>();
    intView.WhereAnd<Where<Current<AddItemFilter.workGroupID>, IsNull, Or<Current<AddItemFilter.workGroupID>, Equal<ARAddItemSelected.priceWorkgroupID>>>>();
    return intView;
  }
}
