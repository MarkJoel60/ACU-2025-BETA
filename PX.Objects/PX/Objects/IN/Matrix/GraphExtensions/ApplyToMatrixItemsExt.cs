// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.GraphExtensions.ApplyToMatrixItemsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN.Matrix.Graphs;
using PX.Objects.IN.Matrix.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.GraphExtensions;

public class ApplyToMatrixItemsExt : PXGraphExtension<ItemsGridExt, TemplateInventoryItemMaint>
{
  public PXAction<InventoryItem> applyToItems;

  [PXUIField]
  [PXProcessButton(IsLockedOnToolbar = true)]
  public virtual IEnumerable ApplyToItems(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ApplyToMatrixItemsExt.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new ApplyToMatrixItemsExt.\u003C\u003Ec__DisplayClass1_0();
    ((PXAction) ((PXGraphExtension<TemplateInventoryItemMaint>) this).Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.templateItem = ((PXSelectBase<InventoryItem>) ((PXGraphExtension<TemplateInventoryItemMaint>) this).Base.Item).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.childrenItems = ((PXSelectBase<ItemsGridExt.MatrixInventoryItem>) this.Base1.MatrixItems).SelectMain(Array.Empty<object>());
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass10.templateItem.UpdateOnlySelected.GetValueOrDefault())
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.childrenItems = ((IEnumerable<ItemsGridExt.MatrixInventoryItem>) cDisplayClass10.childrenItems).Where<ItemsGridExt.MatrixInventoryItem>((Func<ItemsGridExt.MatrixInventoryItem, bool>) (item => item.Selected.GetValueOrDefault())).ToArray<ItemsGridExt.MatrixInventoryItem>();
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.graph = cDisplayClass10.templateItem.StkItem.GetValueOrDefault() ? (InventoryItemMaintBase) PXGraph.CreateInstance<InventoryItemMaint>() : (InventoryItemMaintBase) PXGraph.CreateInstance<NonStockItemMaint>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.graph.DefaultSiteFromItemClass = false;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.helper = this.GetHelper((PXGraph) cDisplayClass10.graph);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) ((PXGraphExtension<TemplateInventoryItemMaint>) this).Base, new PXToggleAsyncDelegate((object) cDisplayClass10, __methodptr(\u003CApplyToItems\u003Eb__1)));
    return adapter.Get();
  }

  protected virtual void _(Events.RowSelected<InventoryItem> e)
  {
    ((PXAction) this.applyToItems).SetEnabled(EnumerableExtensions.IsIn<PXEntryStatus>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<InventoryItem>>) e).Cache.GetStatus((object) e.Row), (PXEntryStatus) 0, (PXEntryStatus) 1));
  }

  protected virtual CreateMatrixItemsHelper GetHelper(PXGraph graph)
  {
    return new CreateMatrixItemsHelper(graph);
  }
}
