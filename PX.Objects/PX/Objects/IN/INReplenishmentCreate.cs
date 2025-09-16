// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentCreate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PO;
using PX.Objects.RQ;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class INReplenishmentCreate : PXGraph<INReplenishmentCreate>
{
  public PXFilter<PX.Objects.AP.Vendor> _vendor;
  public PXFilter<INReplenishmentFilter> Filter;
  public PXCancel<INReplenishmentFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public INReplenishmentCreate.Processing<INReplenishmentItem> Records;
  public PXAction<INReplenishmentFilter> ViewVendorCatalogue;
  public PXAction<INReplenishmentFilter> ViewInventoryItem;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewInventoryItem(PXAdapter adapter)
  {
    if (((PXSelectBase<INReplenishmentItem>) this.Records).Current != null && ((PXSelectBase<INReplenishmentItem>) this.Records).Current.InventoryID.HasValue)
    {
      InventoryItemMaint instance = PXGraph.CreateInstance<InventoryItemMaint>();
      InventoryItem inventoryItem = PXResultset<InventoryItem>.op_Implicit(((PXSelectBase<InventoryItem>) instance.Item).Search<InventoryItem.inventoryID>((object) ((PXSelectBase<INReplenishmentItem>) this.Records).Current.InventoryID, Array.Empty<object>()));
      if (inventoryItem != null)
      {
        ((PXSelectBase<InventoryItem>) instance.Item).Current = inventoryItem;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Inventory Item");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(VisibleOnProcessingResults = true)]
  public virtual IEnumerable viewVendorCatalogue(PXAdapter adapter)
  {
    if (((PXSelectBase<INReplenishmentItem>) this.Records).Current != null && ((PXSelectBase<INReplenishmentItem>) this.Records).Current.InventoryID.HasValue)
    {
      POVendorCatalogueMaint instance = PXGraph.CreateInstance<POVendorCatalogueMaint>();
      PX.Objects.PO.VendorLocation vendorLocation = PXResultset<PX.Objects.PO.VendorLocation>.op_Implicit(PXSelectBase<PX.Objects.PO.VendorLocation, PXSelect<PX.Objects.PO.VendorLocation, Where<PX.Objects.PO.VendorLocation.bAccountID, Equal<Required<PX.Objects.PO.VendorLocation.bAccountID>>, And<PX.Objects.PO.VendorLocation.locationID, Equal<Required<PX.Objects.PO.VendorLocation.locationID>>>>>.Config>.Select((PXGraph) instance, new object[2]
      {
        (object) ((PXSelectBase<INReplenishmentItem>) this.Records).Current.PreferredVendorID,
        (object) ((PXSelectBase<INReplenishmentItem>) this.Records).Current.PreferredVendorLocationID
      }));
      if (vendorLocation != null)
      {
        ((PXSelectBase<PX.Objects.PO.VendorLocation>) instance.BAccount).Current = vendorLocation;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Vendor Catalogue");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  public INReplenishmentCreate()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<INReplenishmentItem>) this.Records).SetProcessDelegate(new PXProcessingBase<INReplenishmentItem>.ProcessListDelegate((object) new INReplenishmentCreate.\u003C\u003Ec__DisplayClass9_0()
    {
      filter = ((PXSelectBase<INReplenishmentFilter>) this.Filter).Current
    }, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  protected virtual void INReplenishmentFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<INReplenishmentFilter.replenishmentSiteID>(sender, (object) null, PXAccess.FeatureInstalled<FeaturesSet.warehouse>());
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Records).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.IN.S.INItemSite.selected>(((PXSelectBase) this.Records).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<INReplenishmentItem.qtyProcess>(((PXSelectBase) this.Records).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<INReplenishmentItem.replenishmentSource>(((PXSelectBase) this.Records).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<INReplenishmentItem.replenishmentSourceSiteID>(((PXSelectBase) this.Records).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.warehouse>());
    PXUIFieldAttribute.SetEnabled<INReplenishmentItem.preferredVendorID>(((PXSelectBase) this.Records).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<INReplenishmentItem.preferredVendorLocationID>(((PXSelectBase) this.Records).Cache, (object) null, true);
  }

  protected virtual void INReplenishmentItem_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    INReplenishmentItem row = (INReplenishmentItem) e.Row;
    if ((row != null ? (!row.QtyProcess.HasValue ? 1 : 0) : 1) == 0)
      return;
    using (new PXConnectionScope())
      row.QtyProcess = this.RecalcQty(row);
  }

  protected virtual void INReplenishmentItem_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    INReplenishmentItem row = (INReplenishmentItem) e.Row;
    if (row == null)
      return;
    Decimal? nullable = row.QtyProcessInt;
    if (!nullable.HasValue)
    {
      PXDBCalcedAttribute.Calculate<INReplenishmentItem.qtyProcessInt>(((PXGraph) this).Caches[typeof (INReplenishmentItem)], (object) row);
      row.QtyProcess = this.RecalcQty(row);
    }
    PXUIFieldAttribute.SetEnabled<INReplenishmentItem.replenishmentSourceSiteID>(sender, (object) row, PXAccess.FeatureInstalled<FeaturesSet.warehouse>() && INReplenishmentSource.IsTransfer(row.ReplenishmentSource));
    PXUIFieldAttribute.SetEnabled<INReplenishmentItem.preferredVendorID>(sender, (object) row, row.ReplenishmentSource == "P");
    PXUIFieldAttribute.SetEnabled<INReplenishmentItem.preferredVendorLocationID>(sender, (object) row, row.ReplenishmentSource == "P");
    nullable = row.QtyINReplaned;
    Decimal num1 = 0M;
    bool? selected;
    if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
    {
      selected = row.Selected;
      if (selected.GetValueOrDefault())
      {
        nullable = row.QtyProcess;
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
        {
          sender.RaiseExceptionHandling<INReplenishmentItem.qtyProcess>((object) row, (object) row.QtyProcess, (Exception) new PXSetPropertyException("Processing of replenishment with 0 quantity will delete previous plan.", (PXErrorLevel) 2));
          goto label_8;
        }
      }
    }
    this.ManageQtyProcessRoundedWarning(row);
label_8:
    nullable = row.QtyProcess;
    Decimal num3 = 0M;
    if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
    {
      selected = row.Selected;
      if (selected.GetValueOrDefault() && row.ReplenishmentSource == "N")
      {
        sender.RaiseExceptionHandling<INReplenishmentItem.replenishmentSource>((object) row, (object) row.ReplenishmentSource, (Exception) new PXSetPropertyException("No replenishment source has been specified for the item. Specify a replenishment source.", (PXErrorLevel) 2));
        return;
      }
    }
    sender.RaiseExceptionHandling<INReplenishmentItem.replenishmentSource>(e.Row, (object) null, (Exception) null);
  }

  protected virtual void INReplenishmentItem_QtyProcess_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    INReplenishmentItem row = (INReplenishmentItem) e.Row;
    if (row == null)
      return;
    Decimal? qtyProcess = row.QtyProcess;
    if (!qtyProcess.HasValue)
      return;
    INReplenishmentItem replenishmentItem = row;
    qtyProcess = row.QtyProcess;
    Decimal num = 0M;
    bool? nullable = new bool?(qtyProcess.GetValueOrDefault() > num & qtyProcess.HasValue);
    replenishmentItem.Selected = nullable;
  }

  protected virtual void INReplenishmentItem_ReplenishmentSource_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    INReplenishmentItem row = (INReplenishmentItem) e.Row;
    if (row == null || e.OldValue == null || !(e.OldValue.ToString() != "N"))
      return;
    row.QtyProcessInt = new Decimal?();
    row.QtyProcess = new Decimal?();
  }

  protected virtual void INReplenishmentItem_PreferredVendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    INReplenishmentItem row = (INReplenishmentItem) e.Row;
    if (row == null)
      return;
    row.QtyProcessInt = new Decimal?();
    row.QtyProcess = new Decimal?();
  }

  protected virtual bool ManageQtyProcessRoundedWarning(INReplenishmentItem rec) => false;

  protected virtual Decimal? RecalcQty(INReplenishmentItem rec)
  {
    Decimal qty = rec.QtyProcessInt.GetValueOrDefault();
    if (qty > 0M && rec.ReplenishmentSource != "T")
      qty = this.OnRoundQtyByVendor(rec, qty);
    return new Decimal?(qty > 0M ? qty : 0M);
  }

  protected virtual Decimal OnRoundQtyByVendor(INReplenishmentItem rec, Decimal qty)
  {
    POVendorInventory poVendorInventory = INReplenishmentCreate.FetchVendorSettings((PXGraph) this, rec) ?? new POVendorInventory();
    if (rec.ReplenishmentMethod == "F")
    {
      qty = poVendorInventory.ERQ.GetValueOrDefault();
    }
    else
    {
      Decimal? nullable = poVendorInventory.LotSize;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
      {
        nullable = poVendorInventory.LotSize;
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        qty = Decimal.Ceiling(qty / valueOrDefault) * valueOrDefault;
      }
      Decimal num2 = qty;
      nullable = poVendorInventory.MinOrdQty;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      if (num2 < valueOrDefault1)
      {
        nullable = poVendorInventory.MinOrdQty;
        qty = nullable.GetValueOrDefault();
      }
      nullable = poVendorInventory.MaxOrdQty;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      if (valueOrDefault2 > 0M && qty > valueOrDefault2)
        qty = valueOrDefault2;
    }
    return qty;
  }

  protected static void ReplenishmentCreateProc(
    List<INReplenishmentItem> list,
    INReplenishmentFilter filter)
  {
    INReplenishmentMaint instance = PXGraph.CreateInstance<INReplenishmentMaint>();
    int num1 = 0;
    bool flag = false;
    foreach (INReplenishmentItem replenishmentItem in list)
    {
      ++num1;
      Decimal? qtyProcess;
      if (!replenishmentItem.Selected.GetValueOrDefault())
      {
        qtyProcess = replenishmentItem.QtyProcess;
        Decimal num2 = 0M;
        if (!(qtyProcess.GetValueOrDefault() > num2 & qtyProcess.HasValue))
          goto label_11;
      }
      foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planType, Equal<Required<INItemPlan.planType>>, And<INItemPlan.inventoryID, Equal<Required<INItemPlan.inventoryID>>, And<INItemPlan.subItemID, Equal<Required<INItemPlan.subItemID>>, And<INItemPlan.siteID, Equal<Required<INItemPlan.siteID>>, And<INItemPlan.supplyPlanID, IsNull>>>>>>.Config>.Select((PXGraph) instance, new object[4]
      {
        (object) "90",
        (object) replenishmentItem.InventoryID,
        (object) replenishmentItem.SubItemID,
        (object) replenishmentItem.SiteID
      }))
      {
        INItemPlan inItemPlan = PXResult<INItemPlan>.op_Implicit(pxResult);
        GraphHelper.Caches<INItemPlan>((PXGraph) instance).Delete(inItemPlan);
      }
label_11:
      qtyProcess = replenishmentItem.QtyProcess;
      Decimal num3 = 0M;
      if (qtyProcess.GetValueOrDefault() > num3 & qtyProcess.HasValue)
      {
        INItemPlan inItemPlan1 = new INItemPlan();
        inItemPlan1.InventoryID = replenishmentItem.InventoryID;
        inItemPlan1.SubItemID = replenishmentItem.SubItemID;
        inItemPlan1.SiteID = replenishmentItem.SiteID;
        int? nullable1;
        int? nullable2;
        if (PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
        {
          int? replenishmentSourceSiteId = replenishmentItem.ReplenishmentSourceSiteID;
          nullable1 = replenishmentItem.SiteID;
          if (!(replenishmentSourceSiteId.GetValueOrDefault() == nullable1.GetValueOrDefault() & replenishmentSourceSiteId.HasValue == nullable1.HasValue))
          {
            nullable2 = replenishmentItem.ReplenishmentSourceSiteID;
            goto label_16;
          }
        }
        nullable2 = new int?();
label_16:
        int? nullable3 = nullable2;
        inItemPlan1.SourceSiteID = nullable3;
        INItemPlan inItemPlan2 = inItemPlan1;
        int? nullable4;
        if (!(replenishmentItem.ReplenishmentSource == "P"))
        {
          nullable1 = new int?();
          nullable4 = nullable1;
        }
        else
          nullable4 = replenishmentItem.PreferredVendorID;
        inItemPlan2.VendorID = nullable4;
        INItemPlan inItemPlan3 = inItemPlan1;
        int? nullable5;
        if (!(replenishmentItem.ReplenishmentSource == "P"))
        {
          nullable1 = new int?();
          nullable5 = nullable1;
        }
        else
          nullable5 = replenishmentItem.PreferredVendorLocationID;
        inItemPlan3.VendorLocationID = nullable5;
        inItemPlan1.PlanQty = replenishmentItem.QtyProcess;
        inItemPlan1.PlanDate = filter.PurchaseDate;
        inItemPlan1.FixedSource = nullable3.HasValue ? (replenishmentItem.ReplenishmentSource == "P" ? "X" : "T") : "P";
        inItemPlan1.PlanType = "90";
        inItemPlan1.Hold = new bool?(false);
        GraphHelper.Caches<INItemPlan>((PXGraph) instance).Update(inItemPlan1);
        flag = true;
      }
    }
    if (flag)
      ((PXSelectBase<INReplenishmentOrder>) instance.Document).Update(new INReplenishmentOrder()
      {
        OrderDate = filter.PurchaseDate,
        SiteID = filter.ReplenishmentSiteID,
        VendorID = filter.PreferredVendorID
      });
    ((PXAction) instance.Save).Press();
  }

  private static POVendorInventory FetchVendorSettings(PXGraph graph, INReplenishmentItem r)
  {
    PXSelect<POVendorInventory, Where<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>, And<POVendorInventory.subItemID, Equal<Required<POVendorInventory.subItemID>>, And<POVendorInventory.vendorID, Equal<Required<POVendorInventory.vendorID>>, And<Where2<Where<Required<POVendorInventory.vendorLocationID>, IsNull, And<POVendorInventory.vendorLocationID, IsNull>>, Or<POVendorInventory.vendorLocationID, Equal<Required<POVendorInventory.vendorLocationID>>>>>>>>> pxSelect = new PXSelect<POVendorInventory, Where<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>, And<POVendorInventory.subItemID, Equal<Required<POVendorInventory.subItemID>>, And<POVendorInventory.vendorID, Equal<Required<POVendorInventory.vendorID>>, And<Where2<Where<Required<POVendorInventory.vendorLocationID>, IsNull, And<POVendorInventory.vendorLocationID, IsNull>>, Or<POVendorInventory.vendorLocationID, Equal<Required<POVendorInventory.vendorLocationID>>>>>>>>>(graph);
    POVendorInventory poVendorInventory1 = PXResultset<POVendorInventory>.op_Implicit(((PXSelectBase<POVendorInventory>) pxSelect).SelectWindowed(0, 1, new object[5]
    {
      (object) r.InventoryID,
      (object) r.SubItemID,
      (object) r.PreferredVendorID,
      (object) r.PreferredVendorLocationID,
      (object) r.PreferredVendorLocationID
    }));
    if (poVendorInventory1 != null)
      return poVendorInventory1;
    POVendorInventory poVendorInventory2 = PXResultset<POVendorInventory>.op_Implicit(((PXSelectBase<POVendorInventory>) pxSelect).SelectWindowed(0, 1, new object[5]
    {
      (object) r.InventoryID,
      (object) r.DefaultSubItemID,
      (object) r.PreferredVendorID,
      (object) r.PreferredVendorLocationID,
      (object) r.PreferredVendorLocationID
    }));
    if (poVendorInventory2 != null)
      return poVendorInventory2;
    POVendorInventory poVendorInventory3 = PXResultset<POVendorInventory>.op_Implicit(((PXSelectBase<POVendorInventory>) pxSelect).SelectWindowed(0, 1, new object[5]
    {
      (object) r.InventoryID,
      (object) r.SubItemID,
      (object) r.PreferredVendorID,
      null,
      null
    }));
    if (poVendorInventory3 != null)
      return poVendorInventory3;
    return PXResultset<POVendorInventory>.op_Implicit(((PXSelectBase<POVendorInventory>) pxSelect).SelectWindowed(0, 1, new object[5]
    {
      (object) r.InventoryID,
      (object) r.DefaultSubItemID,
      (object) r.PreferredVendorID,
      null,
      null
    }));
  }

  public class Processing<Type> : 
    PXFilteredProcessingJoin<INReplenishmentItem, INReplenishmentFilter, LeftJoin<INItemClass, On<INReplenishmentItem.FK.ItemClass>>, Where<INReplenishmentItem.siteID, Equal<Current<INReplenishmentFilter.replenishmentSiteID>>, And2<Where<Current<INReplenishmentFilter.itemClassCDWildcard>, IsNull, Or<INItemClass.itemClassCD, Like<Current<INReplenishmentFilter.itemClassCDWildcard>>>>, And2<Where<INReplenishmentItem.launchDate, IsNull, Or<INReplenishmentItem.launchDate, LessEqual<Current<INReplenishmentFilter.purchaseDate>>>>, And<Where<INReplenishmentItem.terminationDate, IsNull, Or<INReplenishmentItem.terminationDate, GreaterEqual<Current<INReplenishmentFilter.purchaseDate>>>>>>>>>
    where Type : INReplenishmentItem
  {
    public Processing(PXGraph graph)
      : base(graph)
    {
      ((PXProcessingBase<INReplenishmentItem>) this)._OuterView = new PXView(graph, false, BqlCommand.CreateInstance(new Type[1]
      {
        typeof (Select2<INReplenishmentItem, LeftJoin<INItemClass, On<INReplenishmentItem.FK.ItemClass>>, Where<INReplenishmentItem.siteID, Equal<Current<INReplenishmentFilter.replenishmentSiteID>>, And2<Where<Current<INReplenishmentFilter.itemClassCDWildcard>, IsNull, Or<INItemClass.itemClassCD, Like<Current<INReplenishmentFilter.itemClassCDWildcard>>>>, And2<Where<INReplenishmentItem.launchDate, IsNull, Or<INReplenishmentItem.launchDate, LessEqual<Current<INReplenishmentFilter.purchaseDate>>>>, And<Where<INReplenishmentItem.terminationDate, IsNull, Or<INReplenishmentItem.terminationDate, GreaterEqual<Current<INReplenishmentFilter.purchaseDate>>>>>>>>>)
      }));
      ((PXProcessingBase<INReplenishmentItem>) this)._OuterView.WhereAndCurrent<INReplenishmentFilter>("itemClassCDWildcard", "itemClassCD");
      ((PXProcessingBase<INReplenishmentItem>) this)._OuterView.WhereAnd<Where<Current<INReplenishmentFilter.onlySuggested>, Equal<False>, Or<INReplenishmentItem.qtyProcessInt, Greater<decimal0>>>>();
    }
  }
}
