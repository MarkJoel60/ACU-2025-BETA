// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRevenueBudgetLineSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

[Serializable]
public class PMRevenueBudgetLineSelectorAttribute : PXCustomSelectorAttribute
{
  public PMRevenueBudgetLineSelectorAttribute()
    : base(typeof (PX.Objects.IN.InventoryItem.inventoryID), new Type[2]
    {
      typeof (PX.Objects.IN.InventoryItem.inventoryCD),
      typeof (PX.Objects.IN.InventoryItem.descr)
    })
  {
    ((PXSelectorAttribute) this).SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD);
    ((PXSelectorAttribute) this).DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr);
  }

  protected virtual IEnumerable GetRecords()
  {
    object obj = PXView.Currents == null || PXView.Currents.Length == 0 ? this._Graph.Caches[((PXSelectorAttribute) this)._CacheType].Current : PXView.Currents[0];
    PXSelectJoin<PMRevenueBudget, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<PMRevenueBudget.inventoryID>>>, Where<PMRevenueBudget.projectID, Equal<Current<PMCostBudget.projectID>>, And<PMRevenueBudget.projectTaskID, Equal<Current<PMCostBudget.revenueTaskID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>> pxSelectJoin = new PXSelectJoin<PMRevenueBudget, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<PMRevenueBudget.inventoryID>>>, Where<PMRevenueBudget.projectID, Equal<Current<PMCostBudget.projectID>>, And<PMRevenueBudget.projectTaskID, Equal<Current<PMCostBudget.revenueTaskID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>>(this._Graph);
    List<PX.Objects.IN.InventoryItem> records = new List<PX.Objects.IN.InventoryItem>();
    PXView view = ((PXSelectBase) pxSelectJoin).View;
    object[] objArray1 = new object[2]{ obj, obj };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<PMRevenueBudget, PX.Objects.IN.InventoryItem> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<PMRevenueBudget, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      PMRevenueBudget pmRevenueBudget = PXResult<PMRevenueBudget, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      inventoryItem.Descr = pmRevenueBudget.Description;
      records.Add(inventoryItem);
    }
    return (IEnumerable) records;
  }
}
