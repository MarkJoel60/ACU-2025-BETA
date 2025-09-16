// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POCreate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Maintenance;
using PX.Objects.AP;
using PX.Objects.AP.MigrationMode;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO.GraphExtensions.POOrderEntryExt;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.PO;

[TableAndChartDashboardType]
public class POCreate : PXGraph<
#nullable disable
POCreate>
{
  public PXCancel<POCreate.POCreateFilter> Cancel;
  public PXFilter<POCreate.POCreateFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXProcessingViewOf<POFixedDemand>.BasedOn<SelectFromBase<POFixedDemand, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AP.Vendor>.On<BqlOperand<
  #nullable enable
  PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  POFixedDemand.vendorID>>>, FbqlJoins.Left<POVendorInventory>.On<BqlOperand<
  #nullable enable
  POVendorInventory.recordID, IBqlInt>.IsEqual<
  #nullable disable
  POFixedDemand.priceRecordID>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.Location>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Standalone.Location.bAccountID, 
  #nullable disable
  Equal<POFixedDemand.vendorID>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.CR.Standalone.Location.locationID, IBqlInt>.IsEqual<
  #nullable disable
  POFixedDemand.vendorLocationID>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  POFixedDemand.planQty, 
  #nullable disable
  NotEqual<decimal0>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  POCreate.POCreateFilter.itemClassCDWildcard>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  POFixedDemand.itemClassCD, IBqlString>.IsLike<
  #nullable disable
  BqlField<
  #nullable enable
  POCreate.POCreateFilter.itemClassCDWildcard, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  POCreate.POCreateFilter.inventoryID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  POFixedDemand.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POCreate.POCreateFilter.inventoryID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  POCreate.POCreateFilter.siteID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  POFixedDemand.siteID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POCreate.POCreateFilter.siteID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  POCreate.POCreateFilter.requestedOnDate>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  POFixedDemand.planDate, IBqlDateTime>.IsLessEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POCreate.POCreateFilter.requestedOnDate, IBqlDateTime>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  POCreate.POCreateFilter.vendorID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  POFixedDemand.vendorID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POCreate.POCreateFilter.vendorID, IBqlInt>.FromCurrent>>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  POFixedDemand.inventoryID, IBqlInt>.Asc>>>.FilteredBy<
  #nullable disable
  POCreate.POCreateFilter> FixedDemand;
  public PXAction<POCreate.POCreateFilter> viewDocument;
  public PXAction<POCreate.POCreateFilter> inventorySummary;

  public POCreate()
  {
    APSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUIReadonly(((PXSelectBase) this.FixedDemand).Cache, (object) null);
    attributeAdjuster = attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = attributeAdjuster.For<POFixedDemand.selected>((Action<PXUIFieldAttribute>) (a => a.Enabled = true));
    chained = chained.SameFor<POFixedDemand.pOSiteID>();
    chained = chained.SameFor<POFixedDemand.vendorID>();
    chained.SameFor<POFixedDemand.vendorLocationID>();
    PXUIFieldAttribute.SetDisplayName<PX.Objects.IN.InventoryItem.descr>((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this), "Item Description");
    PXUIFieldAttribute.SetDisplayName<INSite.descr>((PXCache) GraphHelper.Caches<INSite>((PXGraph) this), "Warehouse Description");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.AP.Vendor.acctName>((PXCache) GraphHelper.Caches<PX.Objects.AP.Vendor>((PXGraph) this), "Vendor Name");
  }

  protected virtual IEnumerable filter()
  {
    yield return (object) this.CalculateTotals(((PXSelectBase<POCreate.POCreateFilter>) this.Filter).Current);
  }

  protected virtual IEnumerable fixedDemand()
  {
    return this.EnumerateAndPrepareFixedDemands(this.SelectFromFixedDemandView());
  }

  protected virtual POCreate.POCreateFilter CalculateTotals(POCreate.POCreateFilter filter)
  {
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    Decimal num3 = 0M;
    Decimal num4 = 0M;
    foreach (POFixedDemand poFixedDemand in ((PXSelectBase) this.FixedDemand).Cache.Updated)
    {
      if (poFixedDemand.Selected.GetValueOrDefault())
      {
        Decimal num5 = num1;
        Decimal? nullable = poFixedDemand.ExtVolume;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        num1 = num5 + valueOrDefault1;
        Decimal num6 = num2;
        nullable = poFixedDemand.ExtWeight;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        num2 = num6 + valueOrDefault2;
        Decimal num7 = num3;
        nullable = poFixedDemand.ExtCost;
        Decimal valueOrDefault3 = nullable.GetValueOrDefault();
        num3 = num7 + valueOrDefault3;
        Decimal num8 = num4;
        nullable = poFixedDemand.OrderQty;
        Decimal valueOrDefault4 = nullable.GetValueOrDefault();
        num4 = num8 + valueOrDefault4;
      }
    }
    filter.OrderVolume = new Decimal?(num1);
    filter.OrderWeight = new Decimal?(num2);
    filter.OrderTotal = new Decimal?(num3);
    filter.OrderQty = new Decimal?(num4);
    return filter;
  }

  protected virtual PXResultset<POFixedDemand> SelectFromFixedDemandView()
  {
    PXView pxView = new PXView((PXGraph) this, false, ((PXSelectBase) this.FixedDemand).View.BqlSelect);
    object[] objArray = (object[]) null;
    if (PXView.MaximumRows == 1 && PXView.SortColumns != null && PXView.Searches != null)
    {
      int index = Array.FindIndex<string>(PXView.SortColumns, (Predicate<string>) (s => s.Equals("planID", StringComparison.OrdinalIgnoreCase)));
      if (index >= 0 && PXView.Searches[index] != null)
      {
        long int64 = Convert.ToInt64(PXView.Searches[index]);
        pxView.WhereAnd<Where<BqlOperand<POFixedDemand.planID, IBqlLong>.IsEqual<P.AsLong>>>();
        objArray = EnumerableExtensions.Append<object>(objArray, (object) int64);
      }
    }
    int startRow = PXView.StartRow;
    int num1 = 0;
    int num2 = startRow;
    PXResultset<POFixedDemand> pxResultset = new PXResultset<POFixedDemand>();
    using (new PXFieldScope(pxView, this.GetFixedDemandFieldScope(), true))
    {
      foreach (PXResult<POFixedDemand> pxResult in pxView.Select(PXView.Currents, objArray, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num2, PXView.MaximumRows, ref num1))
        pxResultset.Add(pxResult);
    }
    PXView.StartRow = 0;
    return pxResultset;
  }

  protected virtual IEnumerable<System.Type> GetFixedDemandFieldScope()
  {
    return (IEnumerable<System.Type>) new System.Type[10]
    {
      typeof (POFixedDemand),
      typeof (PX.Objects.AP.Vendor.bAccountID),
      typeof (PX.Objects.AP.Vendor.curyID),
      typeof (PX.Objects.AP.Vendor.termsID),
      typeof (POVendorInventory.recordID),
      typeof (POVendorInventory.lastPrice),
      typeof (POVendorInventory.addLeadTimeDays),
      typeof (PX.Objects.CR.Standalone.Location.locationID),
      typeof (PX.Objects.CR.Standalone.Location.vLeadTime),
      typeof (PX.Objects.CR.Standalone.Location.vCarrierID)
    };
  }

  protected virtual IEnumerable EnumerateAndPrepareFixedDemands(
    PXResultset<POFixedDemand> fixedDemands)
  {
    foreach (PXResult<POFixedDemand> fixedDemand in fixedDemands)
    {
      POFixedDemand poFixedDemand1 = PXResult.Unwrap<POFixedDemand>((object) fixedDemand);
      POFixedDemand poFixedDemand2 = ((PXSelectBase<POFixedDemand>) this.FixedDemand).Locate(poFixedDemand1);
      if (poFixedDemand2 != null && EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.FixedDemand).Cache.GetStatus((object) poFixedDemand2), (PXEntryStatus) 5, (PXEntryStatus) 1))
      {
        ((PXSelectBase) this.FixedDemand).Cache.RestoreCopy((object) poFixedDemand1, (object) poFixedDemand2);
      }
      else
      {
        this.EnumerateAndPrepareFixedDemandRow(fixedDemand);
        ((PXSelectBase) this.FixedDemand).Cache.SetStatus((object) poFixedDemand1, (PXEntryStatus) 5);
      }
      yield return (object) fixedDemand;
    }
  }

  protected virtual void EnumerateAndPrepareFixedDemandRow(PXResult<POFixedDemand> record)
  {
    POFixedDemand demand = PXResult.Unwrap<POFixedDemand>((object) record);
    PX.Objects.AP.Vendor vendor = PXResult.Unwrap<PX.Objects.AP.Vendor>((object) record);
    POFixedDemand poFixedDemand = demand;
    if (poFixedDemand.CuryID == null)
    {
      string curyId;
      poFixedDemand.CuryID = curyId = vendor.CuryID;
    }
    if (!demand.EffPrice.HasValue)
      this.RecalculateEffPrice(demand, (Func<int?, PX.Objects.AP.Vendor>) (vid => vendor), (Func<int?, POVendorInventory>) (viid => PXResult.Unwrap<POVendorInventory>((object) record)));
    demand.SorterString = this.GetSorterString(record);
  }

  [PXEditDetailButton]
  [PXUIField]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    Guid? refNoteId = (Guid?) ((PXSelectBase<POFixedDemand>) this.FixedDemand).Current?.RefNoteID;
    if (refNoteId.HasValue)
      this.TryRedirectToRelatedDocument(new Guid?(refNoteId.GetValueOrDefault()));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(VisibleOnProcessingResults = true)]
  public virtual IEnumerable InventorySummary(PXAdapter adapter)
  {
    POFixedDemand current = ((PXSelectBase<POFixedDemand>) this.FixedDemand).Current;
    if (current == null)
      return adapter.Get();
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, current.InventoryID);
    if (inventoryItem != null && inventoryItem.StkItem.GetValueOrDefault())
    {
      INSubItem inSubItem = (INSubItem) PXSelectorAttribute.Select<POFixedDemand.subItemID>(((PXSelectBase) this.FixedDemand).Cache, (object) current);
      InventorySummaryEnq.Redirect(inventoryItem.InventoryID, inSubItem?.SubItemCD, current.SiteID, current.LocationID);
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<POCreate.POCreateFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    POCreate.\u003C\u003Ec__DisplayClass15_0 cDisplayClass150 = new POCreate.\u003C\u003Ec__DisplayClass15_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.filter = ((PXSelectBase<POCreate.POCreateFilter>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass150.filter == null)
      return;
    // ISSUE: method pointer
    ((PXProcessingBase<POFixedDemand>) this.FixedDemand).SetProcessDelegate(new PXProcessingBase<POFixedDemand>.ProcessListDelegate((object) cDisplayClass150, __methodptr(\u003C_\u003Eb__0)));
    PXUIFieldAttribute.SetVisible<POLine.orderNbr>((PXCache) GraphHelper.Caches<POLine>((PXGraph) this), (object) null, EnumerableExtensions.IsIn<PXLongRunStatus>(PXLongOperation.GetStatus(((PXGraph) this).UID), (PXLongRunStatus) 2, (PXLongRunStatus) 3));
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetVisible<POCreate.POCreateFilter.orderTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POCreate.POCreateFilter>>) e).Cache, (object) null, cDisplayClass150.filter.VendorID.HasValue);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<POFixedDemand> e)
  {
    if (e.Row == null || e.Row.Selected.GetValueOrDefault())
      return;
    bool? selected1 = e.Row.Selected;
    bool? selected2 = e.OldRow.Selected;
    if (!(selected1.GetValueOrDefault() == selected2.GetValueOrDefault() & selected1.HasValue == selected2.HasValue))
      return;
    e.Row.Selected = new bool?(true);
  }

  protected virtual void _(PX.Data.Events.RowSelected<POFixedDemand> e)
  {
    if (e.Row == null)
      return;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POFixedDemand>>) e).Cache, (object) e.Row).For<POFixedDemand.orderQty>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = e.Row.PlanType == "90"));
    chained = chained.SameFor<POFixedDemand.fixedSource>();
    chained = chained.For<POFixedDemand.sourceSiteID>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = e.Row.FixedSource == "T"));
    chained = chained.For<POFixedDemand.pOSiteID>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = e.Row.FixedSource == "P"));
    chained = chained.SameFor<POFixedDemand.vendorID>();
    chained.SameFor<POFixedDemand.vendorLocationID>();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.vendorLocationID, int?> e)
  {
    if (((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.vendorLocationID, int?>, POFixedDemand, int?>) e).Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.vendorLocationID, int?>>) e).Cancel = true;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.vendorLocationID, int?>, POFixedDemand, int?>) e).NewValue = POItemCostManager.FetchLocation((PXGraph) this, ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.vendorLocationID, int?>, POFixedDemand, int?>) e).Row.VendorID, ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.vendorLocationID, int?>, POFixedDemand, int?>) e).Row.InventoryID, ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.vendorLocationID, int?>, POFixedDemand, int?>) e).Row.SubItemID, ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.vendorLocationID, int?>, POFixedDemand, int?>) e).Row.SiteID);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POFixedDemand, POFixedDemand.orderQty, Decimal?> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POFixedDemand, POFixedDemand.orderQty, Decimal?>, POFixedDemand, Decimal?>) e).Row == null)
      return;
    Decimal? planUnitQty = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POFixedDemand, POFixedDemand.orderQty, Decimal?>, POFixedDemand, Decimal?>) e).Row.PlanUnitQty;
    Decimal? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POFixedDemand, POFixedDemand.orderQty, Decimal?>, POFixedDemand, Decimal?>) e).NewValue;
    if (!(planUnitQty.GetValueOrDefault() < newValue.GetValueOrDefault() & planUnitQty.HasValue & newValue.HasValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POFixedDemand, POFixedDemand.orderQty, Decimal?>, POFixedDemand, Decimal?>) e).NewValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POFixedDemand, POFixedDemand.orderQty, Decimal?>, POFixedDemand, Decimal?>) e).Row.PlanUnitQty;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<POFixedDemand, POFixedDemand.orderQty, Decimal?>>) e).Cache.RaiseExceptionHandling<POFixedDemand.orderQty>((object) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POFixedDemand, POFixedDemand.orderQty, Decimal?>, POFixedDemand, Decimal?>) e).Row, (object) null, (Exception) new PXSetPropertyException<POFixedDemand.orderQty>("Insufficient quantity requested. Line quantity was changed to match.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<POFixedDemand, POFixedDemand.priceRecordID, int?> e)
  {
    if (((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<POFixedDemand, POFixedDemand.priceRecordID, int?>, POFixedDemand, int?>) e).Row == null || ((PXSelectBase<POCreate.POCreateFilter>) this.Filter).Current == null)
      return;
    this.RecalculateEffPrice(((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<POFixedDemand, POFixedDemand.priceRecordID, int?>, POFixedDemand, int?>) e).Row, (Func<int?, PX.Objects.AP.Vendor>) (vid => PX.Objects.AP.Vendor.PK.Find((PXGraph) this, vid)), (Func<int?, POVendorInventory>) (viid => POVendorInventory.PK.Find((PXGraph) this, viid)));
  }

  protected virtual string GetSorterString(PXResult<POFixedDemand> record) => string.Empty;

  protected virtual void TryRedirectToRelatedDocument(Guid? refNoteID)
  {
  }

  protected virtual void RecalculateEffPrice(
    POFixedDemand demand,
    Func<int?, PX.Objects.AP.Vendor> getVendor,
    Func<int?, POVendorInventory> getVendorInventory)
  {
    if (((PXSelectBase<POCreate.POCreateFilter>) this.Filter).Current.PurchDate.HasValue && demand.InventoryID.HasValue && demand.DemandUOM != null && demand.VendorID.HasValue)
    {
      PX.Objects.AP.Vendor vendor = getVendor(demand.VendorID);
      if (vendor != null && vendor.CuryID != null)
        demand.EffPrice = APVendorPriceMaint.CalculateCuryUnitCost(((PXSelectBase) this.FixedDemand).Cache, demand.VendorID, demand.VendorLocationID, demand.InventoryID, demand.SiteID, vendor.CuryID, demand.DemandUOM, demand.OrderQty, ((PXSelectBase<POCreate.POCreateFilter>) this.Filter).Current.PurchDate.Value, new Decimal?(0M));
    }
    Decimal valueOrDefault1;
    Decimal? nullable1;
    if (demand.PriceRecordID.HasValue)
    {
      POVendorInventory poVendorInventory = getVendorInventory(demand.PriceRecordID);
      if (poVendorInventory != null)
      {
        POFixedDemand poFixedDemand1 = demand;
        Decimal? effPrice = poFixedDemand1.EffPrice;
        valueOrDefault1 = effPrice.GetValueOrDefault();
        if (!effPrice.HasValue)
        {
          nullable1 = poVendorInventory.LastPrice;
          Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
          POFixedDemand poFixedDemand2 = poFixedDemand1;
          nullable1 = new Decimal?(valueOrDefault2);
          Decimal? nullable2 = nullable1;
          poFixedDemand2.EffPrice = nullable2;
        }
        demand.AddLeadTimeDays = new short?(poVendorInventory.AddLeadTimeDays.GetValueOrDefault());
        goto label_11;
      }
    }
    POFixedDemand poFixedDemand3 = demand;
    Decimal? effPrice1 = poFixedDemand3.EffPrice;
    valueOrDefault1 = effPrice1.GetValueOrDefault();
    if (!effPrice1.HasValue)
    {
      Decimal num = 0M;
      poFixedDemand3.EffPrice = new Decimal?(num);
    }
    demand.AddLeadTimeDays = new short?((short) 0);
label_11:
    POFixedDemand poFixedDemand4 = demand;
    Decimal? orderQty = demand.OrderQty;
    nullable1 = demand.EffPrice;
    Decimal? nullable3 = orderQty.HasValue & nullable1.HasValue ? new Decimal?(orderQty.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
    poFixedDemand4.ExtCost = nullable3;
  }

  [Obsolete("Use the version that accepts the POCreateFilter DAC", true)]
  public virtual void CreateProc(
    List<POFixedDemand> demands,
    DateTime? orderDate,
    bool particularSalesOrder,
    int? branchID = null)
  {
    this.CreateProc(demands, new POCreate.POCreateFilter()
    {
      PurchDate = orderDate,
      BranchID = branchID
    });
  }

  public virtual void CreateProc(
    List<POFixedDemand> demands,
    POCreate.POCreateFilter processingSettings)
  {
    PXRedirectRequiredException poOrders = this.CreatePOOrders(demands, processingSettings);
    if (poOrders != null)
      throw poOrders;
  }

  [Obsolete("Use the version that accepts the POCreateFilter DAC", true)]
  public virtual PXRedirectRequiredException CreatePOOrders(
    List<POFixedDemand> demands,
    DateTime? orderDate,
    bool particularSalesOrder,
    int? branchID = null)
  {
    return this.CreatePOOrders(demands, new POCreate.POCreateFilter()
    {
      PurchDate = orderDate,
      BranchID = branchID
    });
  }

  public virtual PXRedirectRequiredException CreatePOOrders(
    List<POFixedDemand> demands,
    POCreate.POCreateFilter processingSettings)
  {
    return ((PXGraph) PXGraph.CreateInstance<POOrderEntry>()).GetExtension<CreatePOOrdersFromDemandsExtension>().CreatePOOrders(demands, processingSettings);
  }

  [PXCacheName("Create Purchase Orders Filter")]
  public class POCreateFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _OwnerID;
    protected int? _WorkGroupID;

    [Branch(null, null, true, true, true, DisplayName = "PO Creation Branch", FieldClass = "MultipleBaseCurrencies")]
    public virtual int? BranchID { get; set; }

    [PXDBInt]
    [CRCurrentOwnerID]
    public virtual int? CurrentOwnerID { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Me")]
    public virtual bool? MyOwner { get; set; }

    [SubordinateOwner(DisplayName = "Product Manager")]
    public virtual int? OwnerID
    {
      get => !this.MyOwner.GetValueOrDefault() ? this._OwnerID : this.CurrentOwnerID;
      set => this._OwnerID = value;
    }

    [PXDBInt]
    [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
    [PXUIField(DisplayName = "Product  Workgroup")]
    public virtual int? WorkGroupID
    {
      get => !this.MyWorkGroup.GetValueOrDefault() ? this._WorkGroupID : new int?();
      set => this._WorkGroupID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField]
    public virtual bool? MyWorkGroup { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    public virtual bool? FilterSet
    {
      get
      {
        return new bool?(this.OwnerID.HasValue || this.WorkGroupID.HasValue || this.MyWorkGroup.GetValueOrDefault());
      }
    }

    [Vendor(typeof (Search<BAccountR.bAccountID, Where<BqlOperand<True, IBqlBool>.IsEqual<True>>>), CacheGlobal = true, Filterable = true)]
    [VerndorNonEmployeeOrOrganizationRestrictor]
    [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.vStatus, IsNull>>>>.Or<BqlOperand<PX.Objects.AP.Vendor.vStatus, IBqlString>.IsIn<VendorStatus.active, VendorStatus.oneTime, VendorStatus.holdPayments>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (PX.Objects.AP.Vendor.vStatus)})]
    public virtual int? VendorID { get; set; }

    [Site(DisplayName = "Warehouse")]
    public virtual int? SiteID { get; set; }

    [Site(DisplayName = "Source Warehouse", DescriptionField = typeof (INSite.descr))]
    public virtual int? SourceSiteID { get; set; }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Date Promised")]
    public virtual DateTime? EndDate { get; set; }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Creation Date")]
    public virtual DateTime? PurchDate { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Requested On")]
    public virtual DateTime? RequestedOnDate { get; set; }

    [StockItem]
    public virtual int? InventoryID { get; set; }

    [PXDBString(30, IsUnicode = true)]
    [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
    [PXUIField]
    public virtual string ItemClassCD { get; set; }

    [PXString(IsUnicode = true)]
    [PXDimension("INITEMCLASS", ParentSelect = typeof (Select<INItemClass>), ParentValueField = typeof (INItemClass.itemClassCD), AutoNumbering = false)]
    [PXUIField]
    public virtual string ItemClassCDWildcard
    {
      get => DimensionTree<INItemClass.dimension>.MakeWildcard(this.ItemClassCD);
      set
      {
      }
    }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Weight", Enabled = false)]
    public virtual Decimal? OrderWeight { get; set; }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Volume", Enabled = false)]
    public virtual Decimal? OrderVolume { get; set; }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Quantity", Enabled = false)]
    public virtual Decimal? OrderQty { get; set; }

    [PXDBDecimal(typeof (SearchFor<PX.Objects.CM.Currency.decimalPlaces>.Where<BqlOperand<PX.Objects.CM.Currency.curyID, IBqlString>.IsEqual<BqlOperand<PX.Objects.AP.Vendor.curyID, IBqlString>.FromSelectorOf<BqlField<POCreate.POCreateFilter.vendorID, IBqlInt>.FromCurrent>>>))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total", Enabled = false)]
    public virtual Decimal? OrderTotal { get; set; }

    public abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POCreate.POCreateFilter.branchID>
    {
    }

    public abstract class currentOwnerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POCreate.POCreateFilter.currentOwnerID>
    {
    }

    public abstract class myOwner : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POCreate.POCreateFilter.myOwner>
    {
    }

    public abstract class ownerID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POCreate.POCreateFilter.ownerID>
    {
    }

    public abstract class workGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POCreate.POCreateFilter.workGroupID>
    {
    }

    public abstract class myWorkGroup : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POCreate.POCreateFilter.myWorkGroup>
    {
    }

    public abstract class filterSet : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POCreate.POCreateFilter.filterSet>
    {
    }

    public abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POCreate.POCreateFilter.vendorID>
    {
    }

    public abstract class siteID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POCreate.POCreateFilter.siteID>
    {
    }

    public abstract class sourceSiteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POCreate.POCreateFilter.sourceSiteID>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      POCreate.POCreateFilter.endDate>
    {
    }

    public abstract class purchDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      POCreate.POCreateFilter.purchDate>
    {
    }

    public abstract class requestedOnDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      POCreate.POCreateFilter.requestedOnDate>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POCreate.POCreateFilter.inventoryID>
    {
    }

    public abstract class itemClassCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POCreate.POCreateFilter.itemClassCD>
    {
    }

    public abstract class itemClassCDWildcard : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POCreate.POCreateFilter.itemClassCDWildcard>
    {
    }

    public abstract class orderWeight : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POCreate.POCreateFilter.orderWeight>
    {
    }

    public abstract class orderVolume : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POCreate.POCreateFilter.orderVolume>
    {
    }

    public abstract class orderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POCreate.POCreateFilter.orderQty>
    {
    }

    public abstract class orderTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POCreate.POCreateFilter.orderTotal>
    {
    }
  }
}
