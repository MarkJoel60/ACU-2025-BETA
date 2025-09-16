// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryAllocDetEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.Attributes;
using PX.Objects.PO;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Compilation;

#nullable enable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class InventoryAllocDetEnq : PXGraph<
#nullable disable
InventoryAllocDetEnq>
{
  public PXFilter<PX.Objects.CR.BAccount> Dummy_bAccount;
  public PXAction<InventoryAllocDetEnqFilter> RefreshTotal;
  public PXCancel<InventoryAllocDetEnqFilter> Cancel;
  public PXAction<InventoryAllocDetEnqFilter> viewDocument;
  public PXFilter<InventoryAllocDetEnqFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectOrderBy<InventoryAllocDetEnqResult, OrderBy<Asc<InventoryAllocDetEnqResult.module, Asc<InventoryAllocDetEnqResult.qADocType, Asc<InventoryAllocDetEnqResult.refNbr>>>>> ResultRecords;
  [PXFilterable(new System.Type[] {})]
  public PXSelectOrderBy<InventoryQtyByPlanType, OrderBy<Asc<InventoryQtyByPlanType.sortOrder>>> Addition;
  [PXFilterable(new System.Type[] {})]
  public PXSelectOrderBy<InventoryQtyByPlanType, OrderBy<Asc<InventoryQtyByPlanType.sortOrder>>> Deduction;
  public PXSetup<INSetup> insetup;
  protected Lazy<IEnumerable<InventoryAllocDetEnqResult>> cachedResultRecords;
  private readonly bool _warehouseLocationFeature;
  private readonly bool _lotSerialFeature;
  private readonly EntityHelper _entityHelper;
  private Dictionary<string, string> _displayNameByEntityType;
  public PXAction<InventoryAllocDetEnqFilter> viewSummary;

  public InventoryAllocDetEnq.ItemPlanHelper ItemPlanHelperExt
  {
    get => ((PXGraph) this).FindImplementation<InventoryAllocDetEnq.ItemPlanHelper>();
  }

  [PXButton]
  [PXUIField(DisplayName = "")]
  protected virtual IEnumerable refreshTotal(PXAdapter a)
  {
    ((PXSelectBase) this.Filter).Cache.Current = (object) PXResultset<InventoryAllocDetEnqFilter>.op_Implicit(((PXSelectBase<InventoryAllocDetEnqFilter>) this.Filter).Select(Array.Empty<object>()));
    ((PXSelectBase) this.ResultRecords).View.RequestRefresh();
    return a.Get();
  }

  public InventoryAllocDetEnq()
  {
    this._displayNameByEntityType = new Dictionary<string, string>();
    ((PXSelectBase) this.ResultRecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.ResultRecords).Cache.AllowDelete = false;
    ((PXSelectBase) this.ResultRecords).Cache.AllowUpdate = false;
    this.cachedResultRecords = new Lazy<IEnumerable<InventoryAllocDetEnqResult>>(new Func<IEnumerable<InventoryAllocDetEnqResult>>(this.CalculateResultRecords));
    this._warehouseLocationFeature = PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>();
    this._lotSerialFeature = PXAccess.FeatureInstalled<FeaturesSet.lotSerialTracking>();
    this._entityHelper = new EntityHelper((PXGraph) this);
    if (!this._warehouseLocationFeature && !this._lotSerialFeature)
    {
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtyInTransit>(((PXSelectBase) this.Filter).Cache, "In-Transit");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtySOBooked>(((PXSelectBase) this.Filter).Cache, "SO Booked");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtySOShipping>(((PXSelectBase) this.Filter).Cache, "SO Allocated");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtySOShipped>(((PXSelectBase) this.Filter).Cache, "SO Shipped");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtyINIssues>(((PXSelectBase) this.Filter).Cache, "IN Issues");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtyINReceipts>(((PXSelectBase) this.Filter).Cache, "IN Receipts");
    }
    else if (this._warehouseLocationFeature && this._lotSerialFeature)
    {
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtyInTransit>(((PXSelectBase) this.Filter).Cache, "In-Transit [**]");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtySOBooked>(((PXSelectBase) this.Filter).Cache, "SO Booked [**]");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtySOShipping>(((PXSelectBase) this.Filter).Cache, "SO Allocated [**]");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtySOShipped>(((PXSelectBase) this.Filter).Cache, "SO Shipped [**]");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtyINIssues>(((PXSelectBase) this.Filter).Cache, "IN Issues [**]");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtyINReceipts>(((PXSelectBase) this.Filter).Cache, "IN Receipts [*]");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtyExpired>(((PXSelectBase) this.Filter).Cache, "Expired [*]");
    }
    else
    {
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtyInTransit>(((PXSelectBase) this.Filter).Cache, "In-Transit [*]");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtySOBooked>(((PXSelectBase) this.Filter).Cache, "SO Booked [*]");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtySOShipping>(((PXSelectBase) this.Filter).Cache, "SO Allocated [*]");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtySOShipped>(((PXSelectBase) this.Filter).Cache, "SO Shipped [*]");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtyINIssues>(((PXSelectBase) this.Filter).Cache, "IN Issues [*]");
      if (this._warehouseLocationFeature || !this._lotSerialFeature)
        return;
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtyINReceipts>(((PXSelectBase) this.Filter).Cache, "IN Receipts");
      PXUIFieldAttribute.SetDisplayName<InventoryAllocDetEnqFilter.qtyExpired>(((PXSelectBase) this.Filter).Cache, "Expired");
    }
  }

  protected virtual void InventoryAllocDetEnqFilter_LocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void InventoryAllocDetEnqFilter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    InventoryAllocDetEnqFilter row = (InventoryAllocDetEnqFilter) e.Row;
    if (!this._warehouseLocationFeature && !this._lotSerialFeature)
    {
      PXUIFieldAttribute.SetVisible<InventoryAllocDetEnqFilter.label2>(sender, (object) row, false);
      PXUIFieldAttribute.SetVisible<InventoryAllocDetEnqFilter.label>(sender, (object) row, false);
    }
    else
    {
      PXUIFieldAttribute.SetVisible<InventoryAllocDetEnqFilter.label2>(sender, (object) row, true);
      PXUIFieldAttribute.SetVisible<InventoryAllocDetEnqFilter.label>(sender, (object) row, true);
      row.Label2 = PXMessages.LocalizeNoPrefix("[**] Except Expired and  Loc. Not Available");
      if (!this._warehouseLocationFeature)
      {
        PXUIFieldAttribute.SetVisible<InventoryAllocDetEnqFilter.label>(sender, (object) row, false);
        row.Label2 = PXMessages.LocalizeNoPrefix("[*] Except Expired");
      }
      if (!this._lotSerialFeature)
        PXUIFieldAttribute.SetVisible<InventoryAllocDetEnqFilter.label2>(sender, (object) row, false);
    }
    if (!string.Equals(row.Label, "[*]  Except Location Not Available", StringComparison.Ordinal))
      return;
    row.Label = PXMessages.LocalizeNoPrefix("[*]  Except Location Not Available");
  }

  protected virtual void InventoryAllocDetEnqResult_LocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual IEnumerable filter()
  {
    InventoryAllocDetEnq graph = this;
    PXCache cache = ((PXGraph) graph).Caches[typeof (InventoryAllocDetEnqFilter)];
    if (cache != null && cache.Current is InventoryAllocDetEnqFilter current)
    {
      current.QtyOnHand = new Decimal?(0M);
      current.QtyTotalAddition = new Decimal?(0M);
      current.QtyPOPrepared = new Decimal?(0M);
      current.QtyPOOrders = new Decimal?(0M);
      current.QtyPOReceipts = new Decimal?(0M);
      current.QtyINReceipts = new Decimal?(0M);
      current.QtyInTransit = new Decimal?(0M);
      current.QtyInTransitToSO = new Decimal?(0M);
      current.QtyINAssemblySupply = new Decimal?(0M);
      current.QtyInTransitToProduction = new Decimal?(0M);
      current.QtyProductionSupplyPrepared = new Decimal?(0M);
      current.QtyProductionSupply = new Decimal?(0M);
      current.QtyPOFixedProductionPrepared = new Decimal?(0M);
      current.QtyPOFixedProductionOrders = new Decimal?(0M);
      current.QtyProdFixedProdOrdersPrepared = new Decimal?(0M);
      current.QtyProdFixedProdOrders = new Decimal?(0M);
      current.QtyProdFixedSalesOrdersPrepared = new Decimal?(0M);
      current.QtyProdFixedSalesOrders = new Decimal?(0M);
      current.QtyTotalDeduction = new Decimal?(0M);
      current.QtyHardAvail = new Decimal?(0M);
      current.QtyActual = new Decimal?(0M);
      current.QtyNotAvail = new Decimal?(0M);
      current.QtyOnHandNotAvail = new Decimal?(0M);
      current.QtyExpired = new Decimal?(0M);
      current.QtyFSSrvOrdPrepared = new Decimal?(0M);
      current.QtyFSSrvOrdBooked = new Decimal?(0M);
      current.QtyFSSrvOrdAllocated = new Decimal?(0M);
      current.QtySOPrepared = new Decimal?(0M);
      current.QtySOBooked = new Decimal?(0M);
      current.QtySOShipping = new Decimal?(0M);
      current.QtySOShippingReverse = new Decimal?(0M);
      current.QtySOShipped = new Decimal?(0M);
      current.QtySOShippedReverse = new Decimal?(0M);
      current.QtyINIssues = new Decimal?(0M);
      current.QtyINAssemblyDemand = new Decimal?(0M);
      current.QtyProductionDemandPrepared = new Decimal?(0M);
      current.QtyProductionDemand = new Decimal?(0M);
      current.QtyProductionAllocated = new Decimal?(0M);
      current.QtySOFixedProduction = new Decimal?(0M);
      current.QtyProdFixedPurchase = new Decimal?(0M);
      current.QtyProdFixedProduction = new Decimal?(0M);
      current.QtySOBackOrdered = new Decimal?(0M);
      current.QtyFixedFSSrvOrd = new Decimal?(0M);
      current.QtyPOFixedFSSrvOrd = new Decimal?(0M);
      current.QtyPOFixedFSSrvOrdPrepared = new Decimal?(0M);
      current.QtyPOFixedFSSrvOrdReceipts = new Decimal?(0M);
      current.QtySOFixed = new Decimal?(0M);
      current.QtyPOFixedOrders = new Decimal?(0M);
      current.QtyPOFixedPrepared = new Decimal?(0M);
      current.QtyPOFixedReceipts = new Decimal?(0M);
      current.QtySODropShip = new Decimal?(0M);
      current.QtyPODropShipOrders = new Decimal?(0M);
      current.QtyPODropShipPrepared = new Decimal?(0M);
      current.QtyPODropShipReceipts = new Decimal?(0M);
      current.QtyAvail = new Decimal?(0M);
      if (current.InventoryID.HasValue)
      {
        InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) graph, current.InventoryID);
        INAvailabilityScheme availabilityScheme = PXResultset<INAvailabilityScheme>.op_Implicit(PXSelectBase<INAvailabilityScheme, PXSelectReadonly2<INAvailabilityScheme, InnerJoin<INItemClass, On<INItemClass.FK.AvailabilityScheme>>, Where<INItemClass.itemClassID, Equal<Required<INItemClass.itemClassID>>>>.Config>.Select((PXGraph) graph, new object[1]
        {
          (object) inventoryItem.ItemClassID
        }));
        current.InclQtyPOPrepared = availabilityScheme.InclQtyPOPrepared;
        current.InclQtyPOOrders = availabilityScheme.InclQtyPOOrders;
        current.InclQtyPOReceipts = availabilityScheme.InclQtyPOReceipts;
        current.InclQtyINReceipts = availabilityScheme.InclQtyINReceipts;
        current.InclQtyInTransit = availabilityScheme.InclQtyInTransit;
        current.InclQtyFixedSOPO = availabilityScheme.InclQtyFixedSOPO;
        current.InclQtySOFixed = new bool?(availabilityScheme.InclQtyFixedSOPO.GetValueOrDefault() && availabilityScheme.InclQtySOBooked.GetValueOrDefault());
        current.InclQtyPOFixedPrepared = new bool?(availabilityScheme.InclQtyFixedSOPO.GetValueOrDefault() && availabilityScheme.InclQtyPOPrepared.GetValueOrDefault());
        current.InclQtyPOFixedOrders = new bool?(availabilityScheme.InclQtyFixedSOPO.GetValueOrDefault() && availabilityScheme.InclQtyPOOrders.GetValueOrDefault());
        current.InclQtyPOFixedReceipts = new bool?(availabilityScheme.InclQtyFixedSOPO.GetValueOrDefault() && availabilityScheme.InclQtyPOReceipts.GetValueOrDefault());
        current.InclQtyFSSrvOrdPrepared = availabilityScheme.InclQtyFSSrvOrdPrepared;
        current.InclQtyFSSrvOrdBooked = availabilityScheme.InclQtyFSSrvOrdBooked;
        current.InclQtyFSSrvOrdAllocated = availabilityScheme.InclQtyFSSrvOrdAllocated;
        current.InclQtySOPrepared = availabilityScheme.InclQtySOPrepared;
        current.InclQtySOBooked = availabilityScheme.InclQtySOBooked;
        current.InclQtySOShipping = availabilityScheme.InclQtySOShipping;
        current.InclQtySOShipped = availabilityScheme.InclQtySOShipped;
        current.InclQtyINIssues = availabilityScheme.InclQtyINIssues;
        current.InclQtyINAssemblyDemand = availabilityScheme.InclQtyINAssemblyDemand;
        current.InclQtyINAssemblySupply = availabilityScheme.InclQtyINAssemblySupply;
        current.InclQtyProductionDemandPrepared = availabilityScheme.InclQtyProductionDemandPrepared;
        current.InclQtyProductionDemand = availabilityScheme.InclQtyProductionDemand;
        current.InclQtyProductionAllocated = availabilityScheme.InclQtyProductionAllocated;
        current.InclQtyProductionSupplyPrepared = availabilityScheme.InclQtyProductionSupplyPrepared;
        current.InclQtyProductionSupply = availabilityScheme.InclQtyProductionSupply;
        current.InclQtySOBackOrdered = availabilityScheme.InclQtySOBackOrdered;
        current.InclQtySOReverse = availabilityScheme.InclQtySOReverse;
        current.BaseUnit = inventoryItem.BaseUnit;
        PXSelectBase<INLocationStatusByCostCenter> pxSelectBase = (PXSelectBase<INLocationStatusByCostCenter>) new PXSelectReadonly2<INLocationStatusByCostCenter, InnerJoin<InventoryItem, On<INLocationStatusByCostCenter.FK.InventoryItem>, InnerJoin<InventoryAllocDetEnq.INLocation, On<INLocationStatusByCostCenter.FK.Location>, InnerJoin<INSubItem, On<INLocationStatusByCostCenter.FK.SubItem>, LeftJoin<INLotSerClass, On<InventoryItem.FK.LotSerialClass>, LeftJoin<INLotSerialStatusByCostCenter, On<INLotSerialStatusByCostCenter.inventoryID, Equal<INLocationStatusByCostCenter.inventoryID>, And<INLotSerClass.lotSerAssign, Equal<INLotSerAssign.whenReceived>, And<INLotSerialStatusByCostCenter.subItemID, Equal<INLocationStatusByCostCenter.subItemID>, And<INLotSerialStatusByCostCenter.siteID, Equal<INLocationStatusByCostCenter.siteID>, And<INLotSerialStatusByCostCenter.locationID, Equal<INLocationStatusByCostCenter.locationID>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<INLocationStatusByCostCenter.costCenterID>, And<INLotSerClass.lotSerClassID, IsNotNull, And<INLotSerClass.lotSerTrack, NotEqual<INLotSerTrack.notNumbered>>>>>>>>>, InnerJoin<InventoryAllocDetEnq.INSite, On2<INLocationStatusByCostCenter.FK.Site, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>>>>, Where<INLocationStatusByCostCenter.inventoryID, Equal<Current<InventoryAllocDetEnqFilter.inventoryID>>>, OrderBy<Asc<InventoryItem.inventoryCD, Asc<INLocationStatusByCostCenter.siteID, Asc<INSubItem.subItemCD, Asc<INLocationStatusByCostCenter.locationID, Asc<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>>>((PXGraph) graph);
        if (!SubCDUtils.IsSubCDEmpty(current.SubItemCD))
          pxSelectBase.WhereAnd<Where<INSubItem.subItemCD, Like<Current<InventoryAllocDetEnqFilter.subItemCDWildcard>>>>();
        if (current.SiteID.HasValue)
          pxSelectBase.WhereAnd<Where<INLocationStatusByCostCenter.siteID, Equal<Current<InventoryAllocDetEnqFilter.siteID>>>>();
        if (current.LocationID.HasValue && PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
          pxSelectBase.WhereAnd<Where<INLocationStatusByCostCenter.locationID, Equal<Current<InventoryAllocDetEnqFilter.locationID>>>>();
        if (!string.IsNullOrEmpty(current.LotSerialNbr))
          pxSelectBase.WhereAnd<Where<INLotSerialStatusByCostCenter.lotSerialNbr, Like<Current<InventoryAllocDetEnqFilter.lotSerialNbrWildcard>>>>();
        foreach (PXResult<INLocationStatusByCostCenter, InventoryItem, InventoryAllocDetEnq.INLocation, INSubItem, INLotSerClass, INLotSerialStatusByCostCenter> pxResult in pxSelectBase.Select(Array.Empty<object>()))
        {
          INLocationStatusByCostCenter statusByCostCenter1 = PXResult<INLocationStatusByCostCenter, InventoryItem, InventoryAllocDetEnq.INLocation, INSubItem, INLotSerClass, INLotSerialStatusByCostCenter>.op_Implicit(pxResult);
          InventoryAllocDetEnq.INLocation inLocation = PXResult<INLocationStatusByCostCenter, InventoryItem, InventoryAllocDetEnq.INLocation, INSubItem, INLotSerClass, INLotSerialStatusByCostCenter>.op_Implicit(pxResult);
          INLotSerialStatusByCostCenter statusByCostCenter2 = PXResult<INLocationStatusByCostCenter, InventoryItem, InventoryAllocDetEnq.INLocation, INSubItem, INLotSerClass, INLotSerialStatusByCostCenter>.op_Implicit(pxResult);
          InventoryAllocDetEnqFilter allocDetEnqFilter1 = current;
          Decimal? nullable1 = allocDetEnqFilter1.QtyOnHand;
          Decimal? nullable2 = statusByCostCenter2.QtyOnHand;
          Decimal? nullable3 = nullable2 ?? statusByCostCenter1.QtyOnHand;
          Decimal? nullable4;
          if (!(nullable1.HasValue & nullable3.HasValue))
          {
            nullable2 = new Decimal?();
            nullable4 = nullable2;
          }
          else
            nullable4 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
          allocDetEnqFilter1.QtyOnHand = nullable4;
          if (!(inLocation.InclQtyAvail ?? true))
          {
            InventoryAllocDetEnqFilter allocDetEnqFilter2 = current;
            nullable3 = allocDetEnqFilter2.QtyNotAvail;
            nullable2 = statusByCostCenter2.QtyAvail;
            nullable1 = nullable2 ?? statusByCostCenter1.QtyAvail;
            Decimal? nullable5;
            if (!(nullable3.HasValue & nullable1.HasValue))
            {
              nullable2 = new Decimal?();
              nullable5 = nullable2;
            }
            else
              nullable5 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
            allocDetEnqFilter2.QtyNotAvail = nullable5;
            InventoryAllocDetEnqFilter allocDetEnqFilter3 = current;
            nullable1 = allocDetEnqFilter3.QtyOnHandNotAvail;
            nullable2 = statusByCostCenter2.QtyOnHand;
            nullable3 = nullable2 ?? statusByCostCenter1.QtyOnHand;
            Decimal? nullable6;
            if (!(nullable1.HasValue & nullable3.HasValue))
            {
              nullable2 = new Decimal?();
              nullable6 = nullable2;
            }
            else
              nullable6 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
            allocDetEnqFilter3.QtyOnHandNotAvail = nullable6;
          }
          else if (statusByCostCenter2.ExpireDate.HasValue && DateTime.Compare(((PXGraph) graph).Accessinfo.BusinessDate.Value, statusByCostCenter2.ExpireDate.Value) > 0)
          {
            InventoryAllocDetEnqFilter allocDetEnqFilter4 = current;
            nullable3 = allocDetEnqFilter4.QtyExpired;
            nullable1 = statusByCostCenter2.QtyOnHand;
            Decimal? nullable7;
            if (!(nullable3.HasValue & nullable1.HasValue))
            {
              nullable2 = new Decimal?();
              nullable7 = nullable2;
            }
            else
              nullable7 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
            allocDetEnqFilter4.QtyExpired = nullable7;
          }
        }
        foreach (PXResult<InventoryAllocDetEnqResult> pxResult in ((PXSelectBase<InventoryAllocDetEnqResult>) graph.ResultRecords).Select(Array.Empty<object>()))
        {
          InventoryAllocDetEnqResult aSrc = PXResult<InventoryAllocDetEnqResult>.op_Implicit(pxResult);
          graph.Aggregate(current, aSrc);
        }
        InventoryAllocDetEnqFilter allocDetEnqFilter5 = current;
        Decimal? nullable8 = current.InclQtyPOPrepared.GetValueOrDefault() ? current.QtyPOPrepared : new Decimal?(0M);
        Decimal? nullable9 = current.InclQtyPOOrders.GetValueOrDefault() ? current.QtyPOOrders : new Decimal?(0M);
        Decimal? nullable10 = nullable8.HasValue & nullable9.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable11 = current.InclQtyPOReceipts.GetValueOrDefault() ? current.QtyPOReceipts : new Decimal?(0M);
        Decimal? nullable12;
        if (!(nullable10.HasValue & nullable11.HasValue))
        {
          nullable9 = new Decimal?();
          nullable12 = nullable9;
        }
        else
          nullable12 = new Decimal?(nullable10.GetValueOrDefault() + nullable11.GetValueOrDefault());
        Decimal? nullable13 = nullable12;
        Decimal? nullable14 = current.InclQtyPOFixedPrepared.GetValueOrDefault() ? current.QtyPOFixedPrepared : new Decimal?(0M);
        Decimal? nullable15;
        if (!(nullable13.HasValue & nullable14.HasValue))
        {
          nullable11 = new Decimal?();
          nullable15 = nullable11;
        }
        else
          nullable15 = new Decimal?(nullable13.GetValueOrDefault() + nullable14.GetValueOrDefault());
        Decimal? nullable16 = nullable15;
        Decimal? nullable17 = current.InclQtyPOFixedOrders.GetValueOrDefault() ? current.QtyPOFixedOrders : new Decimal?(0M);
        Decimal? nullable18;
        if (!(nullable16.HasValue & nullable17.HasValue))
        {
          nullable14 = new Decimal?();
          nullable18 = nullable14;
        }
        else
          nullable18 = new Decimal?(nullable16.GetValueOrDefault() + nullable17.GetValueOrDefault());
        Decimal? nullable19 = nullable18;
        Decimal? nullable20 = current.InclQtyPOFixedReceipts.GetValueOrDefault() ? current.QtyPOFixedReceipts : new Decimal?(0M);
        Decimal? nullable21;
        if (!(nullable19.HasValue & nullable20.HasValue))
        {
          nullable17 = new Decimal?();
          nullable21 = nullable17;
        }
        else
          nullable21 = new Decimal?(nullable19.GetValueOrDefault() + nullable20.GetValueOrDefault());
        Decimal? nullable22 = nullable21;
        Decimal? nullable23 = current.InclQtyINReceipts.GetValueOrDefault() ? current.QtyINReceipts : new Decimal?(0M);
        Decimal? nullable24;
        if (!(nullable22.HasValue & nullable23.HasValue))
        {
          nullable20 = new Decimal?();
          nullable24 = nullable20;
        }
        else
          nullable24 = new Decimal?(nullable22.GetValueOrDefault() + nullable23.GetValueOrDefault());
        Decimal? nullable25 = nullable24;
        Decimal? nullable26 = current.InclQtyInTransit.GetValueOrDefault() ? current.QtyInTransit : new Decimal?(0M);
        Decimal? nullable27;
        if (!(nullable25.HasValue & nullable26.HasValue))
        {
          nullable23 = new Decimal?();
          nullable27 = nullable23;
        }
        else
          nullable27 = new Decimal?(nullable25.GetValueOrDefault() + nullable26.GetValueOrDefault());
        Decimal? nullable28 = nullable27;
        Decimal? nullable29 = current.InclQtyINAssemblySupply.GetValueOrDefault() ? current.QtyINAssemblySupply : new Decimal?(0M);
        Decimal? nullable30;
        if (!(nullable28.HasValue & nullable29.HasValue))
        {
          nullable26 = new Decimal?();
          nullable30 = nullable26;
        }
        else
          nullable30 = new Decimal?(nullable28.GetValueOrDefault() + nullable29.GetValueOrDefault());
        Decimal? nullable31 = nullable30;
        Decimal? nullable32 = current.InclQtyProductionSupplyPrepared.GetValueOrDefault() ? current.QtyProductionSupplyPrepared : new Decimal?(0M);
        Decimal? nullable33;
        if (!(nullable31.HasValue & nullable32.HasValue))
        {
          nullable29 = new Decimal?();
          nullable33 = nullable29;
        }
        else
          nullable33 = new Decimal?(nullable31.GetValueOrDefault() + nullable32.GetValueOrDefault());
        Decimal? nullable34 = nullable33;
        Decimal? nullable35 = current.InclQtyProductionSupply.GetValueOrDefault() ? current.QtyProductionSupply : new Decimal?(0M);
        Decimal? nullable36;
        if (!(nullable34.HasValue & nullable35.HasValue))
        {
          nullable32 = new Decimal?();
          nullable36 = nullable32;
        }
        else
          nullable36 = new Decimal?(nullable34.GetValueOrDefault() + nullable35.GetValueOrDefault());
        allocDetEnqFilter5.QtyTotalAddition = nullable36;
        InventoryAllocDetEnqFilter allocDetEnqFilter6 = current;
        Decimal? qtyExpired = current.QtyExpired;
        Decimal? nullable37 = current.InclQtySOPrepared.GetValueOrDefault() ? current.QtySOPrepared : new Decimal?(0M);
        Decimal? nullable38 = qtyExpired.HasValue & nullable37.HasValue ? new Decimal?(qtyExpired.GetValueOrDefault() + nullable37.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable39 = current.InclQtyFSSrvOrdPrepared.GetValueOrDefault() ? current.QtyFSSrvOrdPrepared : new Decimal?(0M);
        Decimal? nullable40;
        if (!(nullable38.HasValue & nullable39.HasValue))
        {
          nullable37 = new Decimal?();
          nullable40 = nullable37;
        }
        else
          nullable40 = new Decimal?(nullable38.GetValueOrDefault() + nullable39.GetValueOrDefault());
        Decimal? nullable41 = nullable40;
        Decimal? nullable42 = current.InclQtyFSSrvOrdBooked.GetValueOrDefault() ? current.QtyFSSrvOrdBooked : new Decimal?(0M);
        Decimal? nullable43;
        if (!(nullable41.HasValue & nullable42.HasValue))
        {
          nullable39 = new Decimal?();
          nullable43 = nullable39;
        }
        else
          nullable43 = new Decimal?(nullable41.GetValueOrDefault() + nullable42.GetValueOrDefault());
        Decimal? nullable44 = nullable43;
        Decimal? nullable45 = current.InclQtyFSSrvOrdAllocated.GetValueOrDefault() ? current.QtyFSSrvOrdAllocated : new Decimal?(0M);
        Decimal? nullable46;
        if (!(nullable44.HasValue & nullable45.HasValue))
        {
          nullable42 = new Decimal?();
          nullable46 = nullable42;
        }
        else
          nullable46 = new Decimal?(nullable44.GetValueOrDefault() + nullable45.GetValueOrDefault());
        nullable9 = nullable46;
        Decimal? nullable47 = current.InclQtySOBooked.GetValueOrDefault() ? current.QtySOBooked : new Decimal?(0M);
        Decimal? nullable48;
        if (!(nullable9.HasValue & nullable47.HasValue))
        {
          nullable45 = new Decimal?();
          nullable48 = nullable45;
        }
        else
          nullable48 = new Decimal?(nullable9.GetValueOrDefault() + nullable47.GetValueOrDefault());
        nullable11 = nullable48;
        Decimal? nullable49 = current.InclQtySOFixed.GetValueOrDefault() ? current.QtySOFixed : new Decimal?(0M);
        Decimal? nullable50;
        if (!(nullable11.HasValue & nullable49.HasValue))
        {
          nullable47 = new Decimal?();
          nullable50 = nullable47;
        }
        else
          nullable50 = new Decimal?(nullable11.GetValueOrDefault() + nullable49.GetValueOrDefault());
        nullable14 = nullable50;
        Decimal? nullable51 = current.InclQtySOShipping.GetValueOrDefault() ? current.QtySOShipping : new Decimal?(0M);
        Decimal? nullable52;
        if (!(nullable14.HasValue & nullable51.HasValue))
        {
          nullable49 = new Decimal?();
          nullable52 = nullable49;
        }
        else
          nullable52 = new Decimal?(nullable14.GetValueOrDefault() + nullable51.GetValueOrDefault());
        nullable17 = nullable52;
        Decimal? nullable53 = current.InclQtySOShipped.GetValueOrDefault() ? current.QtySOShipped : new Decimal?(0M);
        Decimal? nullable54;
        if (!(nullable17.HasValue & nullable53.HasValue))
        {
          nullable51 = new Decimal?();
          nullable54 = nullable51;
        }
        else
          nullable54 = new Decimal?(nullable17.GetValueOrDefault() + nullable53.GetValueOrDefault());
        nullable20 = nullable54;
        Decimal? nullable55 = current.InclQtyINIssues.GetValueOrDefault() ? current.QtyINIssues : new Decimal?(0M);
        Decimal? nullable56;
        if (!(nullable20.HasValue & nullable55.HasValue))
        {
          nullable53 = new Decimal?();
          nullable56 = nullable53;
        }
        else
          nullable56 = new Decimal?(nullable20.GetValueOrDefault() + nullable55.GetValueOrDefault());
        nullable23 = nullable56;
        Decimal? nullable57 = current.InclQtyINAssemblyDemand.GetValueOrDefault() ? current.QtyINAssemblyDemand : new Decimal?(0M);
        Decimal? nullable58;
        if (!(nullable23.HasValue & nullable57.HasValue))
        {
          nullable55 = new Decimal?();
          nullable58 = nullable55;
        }
        else
          nullable58 = new Decimal?(nullable23.GetValueOrDefault() + nullable57.GetValueOrDefault());
        nullable26 = nullable58;
        Decimal? nullable59 = current.InclQtyProductionDemandPrepared.GetValueOrDefault() ? current.QtyProductionDemandPrepared : new Decimal?(0M);
        Decimal? nullable60;
        if (!(nullable26.HasValue & nullable59.HasValue))
        {
          nullable57 = new Decimal?();
          nullable60 = nullable57;
        }
        else
          nullable60 = new Decimal?(nullable26.GetValueOrDefault() + nullable59.GetValueOrDefault());
        nullable29 = nullable60;
        Decimal? nullable61 = current.InclQtyProductionDemand.GetValueOrDefault() ? current.QtyProductionDemand : new Decimal?(0M);
        Decimal? nullable62;
        if (!(nullable29.HasValue & nullable61.HasValue))
        {
          nullable59 = new Decimal?();
          nullable62 = nullable59;
        }
        else
          nullable62 = new Decimal?(nullable29.GetValueOrDefault() + nullable61.GetValueOrDefault());
        nullable32 = nullable62;
        Decimal? nullable63 = current.InclQtyProductionAllocated.GetValueOrDefault() ? current.QtyProductionAllocated : new Decimal?(0M);
        Decimal? nullable64;
        if (!(nullable32.HasValue & nullable63.HasValue))
        {
          nullable61 = new Decimal?();
          nullable64 = nullable61;
        }
        else
          nullable64 = new Decimal?(nullable32.GetValueOrDefault() + nullable63.GetValueOrDefault());
        nullable35 = nullable64;
        Decimal? nullable65 = current.InclQtySOBackOrdered.GetValueOrDefault() ? current.QtySOBackOrdered : new Decimal?(0M);
        Decimal? nullable66;
        if (!(nullable35.HasValue & nullable65.HasValue))
        {
          nullable63 = new Decimal?();
          nullable66 = nullable63;
        }
        else
          nullable66 = new Decimal?(nullable35.GetValueOrDefault() + nullable65.GetValueOrDefault());
        allocDetEnqFilter6.QtyTotalDeduction = nullable66;
        InventoryAllocDetEnqFilter allocDetEnqFilter7 = current;
        nullable63 = current.QtyOnHand;
        nullable32 = current.QtyTotalAddition;
        Decimal? nullable67;
        if (!(nullable63.HasValue & nullable32.HasValue))
        {
          nullable61 = new Decimal?();
          nullable67 = nullable61;
        }
        else
          nullable67 = new Decimal?(nullable63.GetValueOrDefault() + nullable32.GetValueOrDefault());
        nullable65 = nullable67;
        nullable32 = current.QtyTotalDeduction;
        nullable63 = current.QtyNotAvail;
        Decimal? nullable68;
        if (!(nullable32.HasValue & nullable63.HasValue))
        {
          nullable61 = new Decimal?();
          nullable68 = nullable61;
        }
        else
          nullable68 = new Decimal?(nullable32.GetValueOrDefault() + nullable63.GetValueOrDefault());
        nullable35 = nullable68;
        Decimal? nullable69;
        if (!(nullable65.HasValue & nullable35.HasValue))
        {
          nullable63 = new Decimal?();
          nullable69 = nullable63;
        }
        else
          nullable69 = new Decimal?(nullable65.GetValueOrDefault() - nullable35.GetValueOrDefault());
        allocDetEnqFilter7.QtyAvail = nullable69;
        InventoryAllocDetEnqFilter allocDetEnqFilter8 = current;
        nullable53 = current.QtyOnHand;
        nullable17 = current.QtyOnHandNotAvail;
        Decimal? nullable70;
        if (!(nullable53.HasValue & nullable17.HasValue))
        {
          nullable51 = new Decimal?();
          nullable70 = nullable51;
        }
        else
          nullable70 = new Decimal?(nullable53.GetValueOrDefault() - nullable17.GetValueOrDefault());
        nullable55 = nullable70;
        nullable20 = current.QtyINIssues;
        Decimal? nullable71;
        if (!(nullable55.HasValue & nullable20.HasValue))
        {
          nullable17 = new Decimal?();
          nullable71 = nullable17;
        }
        else
          nullable71 = new Decimal?(nullable55.GetValueOrDefault() - nullable20.GetValueOrDefault());
        nullable57 = nullable71;
        nullable20 = current.QtySOShipping;
        nullable55 = current.QtySOShippingReverse;
        Decimal? nullable72;
        if (!(nullable20.HasValue & nullable55.HasValue))
        {
          nullable17 = new Decimal?();
          nullable72 = nullable17;
        }
        else
          nullable72 = new Decimal?(nullable20.GetValueOrDefault() - nullable55.GetValueOrDefault());
        nullable23 = nullable72;
        Decimal? nullable73;
        if (!(nullable57.HasValue & nullable23.HasValue))
        {
          nullable55 = new Decimal?();
          nullable73 = nullable55;
        }
        else
          nullable73 = new Decimal?(nullable57.GetValueOrDefault() - nullable23.GetValueOrDefault());
        nullable59 = nullable73;
        nullable23 = current.QtySOShipped;
        nullable57 = current.QtySOShippedReverse;
        Decimal? nullable74;
        if (!(nullable23.HasValue & nullable57.HasValue))
        {
          nullable55 = new Decimal?();
          nullable74 = nullable55;
        }
        else
          nullable74 = new Decimal?(nullable23.GetValueOrDefault() - nullable57.GetValueOrDefault());
        nullable26 = nullable74;
        Decimal? nullable75;
        if (!(nullable59.HasValue & nullable26.HasValue))
        {
          nullable57 = new Decimal?();
          nullable75 = nullable57;
        }
        else
          nullable75 = new Decimal?(nullable59.GetValueOrDefault() - nullable26.GetValueOrDefault());
        nullable61 = nullable75;
        nullable29 = current.QtyProductionAllocated;
        Decimal? nullable76;
        if (!(nullable61.HasValue & nullable29.HasValue))
        {
          nullable26 = new Decimal?();
          nullable76 = nullable26;
        }
        else
          nullable76 = new Decimal?(nullable61.GetValueOrDefault() - nullable29.GetValueOrDefault());
        nullable63 = nullable76;
        nullable32 = current.QtyFSSrvOrdAllocated;
        Decimal? nullable77;
        if (!(nullable63.HasValue & nullable32.HasValue))
        {
          nullable29 = new Decimal?();
          nullable77 = nullable29;
        }
        else
          nullable77 = new Decimal?(nullable63.GetValueOrDefault() - nullable32.GetValueOrDefault());
        nullable35 = nullable77;
        nullable65 = current.QtyINAssemblyDemand;
        Decimal? nullable78;
        if (!(nullable35.HasValue & nullable65.HasValue))
        {
          nullable32 = new Decimal?();
          nullable78 = nullable32;
        }
        else
          nullable78 = new Decimal?(nullable35.GetValueOrDefault() - nullable65.GetValueOrDefault());
        allocDetEnqFilter8.QtyHardAvail = nullable78;
        InventoryAllocDetEnqFilter allocDetEnqFilter9 = current;
        nullable32 = current.QtyOnHand;
        nullable63 = current.QtyOnHandNotAvail;
        Decimal? nullable79;
        if (!(nullable32.HasValue & nullable63.HasValue))
        {
          nullable29 = new Decimal?();
          nullable79 = nullable29;
        }
        else
          nullable79 = new Decimal?(nullable32.GetValueOrDefault() - nullable63.GetValueOrDefault());
        nullable65 = nullable79;
        nullable63 = current.QtySOShipped;
        nullable32 = current.QtySOShippedReverse;
        Decimal? nullable80;
        if (!(nullable63.HasValue & nullable32.HasValue))
        {
          nullable29 = new Decimal?();
          nullable80 = nullable29;
        }
        else
          nullable80 = new Decimal?(nullable63.GetValueOrDefault() - nullable32.GetValueOrDefault());
        nullable35 = nullable80;
        Decimal? nullable81;
        if (!(nullable65.HasValue & nullable35.HasValue))
        {
          nullable32 = new Decimal?();
          nullable81 = nullable32;
        }
        else
          nullable81 = new Decimal?(nullable65.GetValueOrDefault() - nullable35.GetValueOrDefault());
        allocDetEnqFilter9.QtyActual = nullable81;
      }
    }
    yield return cache.Current;
    cache.IsDirty = false;
  }

  protected virtual void InventoryAllocDetEnqFilter_RowUpdated(
    PXCache sender,
    PXRowUpdatedEventArgs e)
  {
    this.cachedResultRecords = new Lazy<IEnumerable<InventoryAllocDetEnqResult>>(new Func<IEnumerable<InventoryAllocDetEnqResult>>(this.CalculateResultRecords));
  }

  protected virtual IEnumerable GetInventoryQtyByPlanType(bool isAddition, bool sort = true)
  {
    InventoryAllocDetEnqFilter header = PXResultset<InventoryAllocDetEnqFilter>.op_Implicit(((PXSelectBase<InventoryAllocDetEnqFilter>) this.Filter).Select(Array.Empty<object>()));
    PXCache headerCache = ((PXGraph) this).Caches[typeof (InventoryAllocDetEnqFilter)];
    List<InventoryQtyByPlanType> list1 = headerCache.GetAttributesOfType<InventoryAllocationFieldAttribute>((object) header, (string) null).Where<InventoryAllocationFieldAttribute>((Func<InventoryAllocationFieldAttribute, bool>) (p => p.IsAddition == isAddition)).Select(f => new
    {
      allocationField = f,
      uiField = headerCache.GetAttributesOfType<PXUIFieldAttribute>((object) header, f.FieldName).FirstOrDefault<PXUIFieldAttribute>()
    }).Where(f =>
    {
      PXUIFieldAttribute uiField = f.uiField;
      return uiField != null && uiField.Visible;
    }).Select(p => new InventoryQtyByPlanType()
    {
      PlanType = PXMessages.LocalizeNoPrefix(p.uiField.DisplayName),
      Qty = (Decimal?) headerCache.GetValue((object) header, ((PXEventSubscriberAttribute) p.uiField).FieldName),
      Included = !string.IsNullOrEmpty(p.allocationField.InclQtyFieldName) ? (bool?) headerCache.GetValue((object) header, p.allocationField.InclQtyFieldName) : new bool?(),
      IsTotal = new bool?(p.allocationField.IsTotal),
      SortOrder = new int?(p.allocationField.SortOrder)
    }).ToList<InventoryQtyByPlanType>();
    if (!sort)
      return (IEnumerable) list1;
    InventoryQtyByPlanType inventoryQtyByPlanType1 = list1.Where<InventoryQtyByPlanType>((Func<InventoryQtyByPlanType, bool>) (r => r.IsTotal.GetValueOrDefault())).SingleOrDefault<InventoryQtyByPlanType>();
    IEnumerable<InventoryQtyByPlanType> collection;
    if (inventoryQtyByPlanType1 != null)
    {
      list1.Remove(inventoryQtyByPlanType1);
      List<InventoryQtyByPlanType> list2 = GraphHelper.RowCast<InventoryQtyByPlanType>(PXView.Sort((IEnumerable) list1)).ToList<InventoryQtyByPlanType>();
      if (!PXView.ReverseOrder)
        list2.Add(inventoryQtyByPlanType1);
      else
        list2.Insert(0, inventoryQtyByPlanType1);
      collection = (IEnumerable<InventoryQtyByPlanType>) list2;
    }
    else
      collection = GraphHelper.RowCast<InventoryQtyByPlanType>(PXView.Sort((IEnumerable) list1));
    PXDelegateResult inventoryQtyByPlanType2 = new PXDelegateResult();
    ((List<object>) inventoryQtyByPlanType2).AddRange((IEnumerable<object>) collection);
    inventoryQtyByPlanType2.IsResultSorted = true;
    return (IEnumerable) inventoryQtyByPlanType2;
  }

  protected virtual IEnumerable addition() => this.GetInventoryQtyByPlanType(true);

  protected virtual IEnumerable deduction() => this.GetInventoryQtyByPlanType(false);

  public virtual void AdjustParentResult(
    System.Type allocationType,
    List<InventoryAllocDetEnqResult> resultList,
    InventoryAllocDetEnq.INItemPlan itemPlan,
    Guid? origNoteID = null)
  {
    Decimal? nullable1;
    Decimal? nullable2;
    if (!itemPlan.Reverse.GetValueOrDefault())
    {
      nullable2 = itemPlan.PlanQty;
    }
    else
    {
      nullable1 = itemPlan.PlanQty;
      nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    }
    Decimal? nullable3 = nullable2;
    foreach (InventoryAllocDetEnqResult allocDetEnqResult1 in resultList.Where<InventoryAllocDetEnqResult>((Func<InventoryAllocDetEnqResult, bool>) (parent =>
    {
      long? planId = parent.PlanID;
      long? origPlanId = itemPlan.OrigPlanID;
      if (planId.GetValueOrDefault() == origPlanId.GetValueOrDefault() & planId.HasValue == origPlanId.HasValue)
        return true;
      if (origNoteID.HasValue)
      {
        Guid? refNoteId = parent.RefNoteID;
        Guid? nullable5 = origNoteID;
        if ((refNoteId.HasValue == nullable5.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() == nullable5.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && parent.AllocationType == allocationType.Name)
        {
          bool? reverse1 = parent.Reverse;
          bool? reverse2 = itemPlan.Reverse;
          if (reverse1.GetValueOrDefault() == reverse2.GetValueOrDefault() & reverse1.HasValue == reverse2.HasValue)
          {
            int? subItemId1 = parent.SubItemID;
            int? subItemId2 = itemPlan.SubItemID;
            if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
            {
              int? siteId1 = parent.SiteID;
              int? siteId2 = itemPlan.SiteID;
              if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
              {
                int? locationId1 = parent.LocationID;
                if (locationId1.HasValue)
                {
                  locationId1 = parent.LocationID;
                  int? locationId2 = itemPlan.LocationID;
                  if (!(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue))
                    goto label_11;
                }
                return string.IsNullOrEmpty(parent.LotSerialNbr) || string.Equals(parent.LotSerialNbr, itemPlan.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
              }
            }
          }
        }
      }
label_11:
      return false;
    })).ToArray<InventoryAllocDetEnqResult>())
    {
      nullable1 = allocDetEnqResult1.PlanQty;
      Decimal num1 = 0M;
      Decimal? nullable4;
      if (nullable1.GetValueOrDefault() >= num1 & nullable1.HasValue)
      {
        nullable1 = allocDetEnqResult1.PlanQty;
        nullable4 = nullable3;
        if (nullable1.GetValueOrDefault() > nullable4.GetValueOrDefault() & nullable1.HasValue & nullable4.HasValue)
          goto label_8;
      }
      nullable4 = allocDetEnqResult1.PlanQty;
      Decimal num2 = 0M;
      if (nullable4.GetValueOrDefault() < num2 & nullable4.HasValue)
      {
        nullable4 = allocDetEnqResult1.PlanQty;
        nullable1 = nullable3;
        if (nullable4.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable4.HasValue & nullable1.HasValue)
          goto label_8;
      }
      nullable4 = nullable3;
      nullable1 = allocDetEnqResult1.PlanQty;
      nullable3 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      allocDetEnqResult1.PlanQty = new Decimal?(0M);
      resultList.Remove(allocDetEnqResult1);
      goto label_10;
label_8:
      InventoryAllocDetEnqResult allocDetEnqResult2 = allocDetEnqResult1;
      nullable1 = allocDetEnqResult2.PlanQty;
      nullable4 = nullable3;
      allocDetEnqResult2.PlanQty = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
      nullable3 = new Decimal?(0M);
label_10:
      nullable1 = nullable3;
      Decimal num3 = 0M;
      if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue)
        break;
    }
  }

  public virtual void ProcessItemPlanRecAs(
    System.Type planTypeInclQtyField,
    List<InventoryAllocDetEnqResult> resultList,
    InventoryAllocDetEnq.ItemPlanWithExtraInfo itemPlanWithExtraInfo)
  {
    short valueOrDefault1 = (((PXGraph) this).Caches[typeof (INPlanType)].GetValue((object) itemPlanWithExtraInfo.PlanType, planTypeInclQtyField.Name) as short?).GetValueOrDefault();
    if (valueOrDefault1 == (short) 0)
      return;
    InventoryAllocDetEnq.INItemPlan itemPlan = itemPlanWithExtraInfo.ItemPlan;
    bool? nullable = itemPlan.Hold;
    if (nullable.GetValueOrDefault() && INPlanConstants.ToModuleField(itemPlan.PlanType) == "IN")
    {
      nullable = ((PXSelectBase<INSetup>) this.insetup).Current.AllocateDocumentsOnHold;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        return;
    }
    if (planTypeInclQtyField == typeof (INPlanType.inclQtyPOReceipts))
    {
      if ((itemPlan.OrigPlanType ?? "70") == "70")
        this.AdjustParentResult(typeof (INPlanType.inclQtyPOOrders), resultList, itemPlan);
    }
    else if (planTypeInclQtyField == typeof (INPlanType.inclQtyPOFixedReceipts))
    {
      if ((itemPlan.OrigPlanType ?? "76") == "76")
        this.AdjustParentResult(typeof (INPlanType.inclQtyPOFixedOrders), resultList, itemPlan);
    }
    else if (planTypeInclQtyField == typeof (INPlanType.inclQtyPOFixedFSSrvOrdReceipts))
    {
      if ((itemPlan.OrigPlanType ?? "F7") == "F7")
        this.AdjustParentResult(typeof (INPlanType.inclQtyPOFixedFSSrvOrd), resultList, itemPlan);
    }
    else if (planTypeInclQtyField == typeof (INPlanType.inclQtyPODropShipReceipts))
    {
      if ((itemPlan.OrigPlanType ?? "74") == "74")
        this.AdjustParentResult(typeof (INPlanType.inclQtyPODropShipOrders), resultList, itemPlan);
    }
    else if (planTypeInclQtyField == typeof (INPlanType.inclQtySOShipping))
    {
      if (itemPlan.OrigPlanType == "60")
        this.AdjustParentResult(typeof (INPlanType.inclQtySOBooked), resultList, itemPlan);
      else if (itemPlan.OrigPlanType == "61" || itemPlan.OrigPlanType == "63")
        this.AdjustParentResult(planTypeInclQtyField, resultList, itemPlan);
    }
    if (planTypeInclQtyField == typeof (INPlanType.inclQtyINReceipts) || planTypeInclQtyField == typeof (INPlanType.inclQtyPOReceipts) || planTypeInclQtyField == typeof (INPlanType.inclQtyPOFixedReceipts))
    {
      if (itemPlan.OrigPlanType == "42")
        this.AdjustParentResult(typeof (INPlanType.inclQtyInTransit), resultList, itemPlan, itemPlan.OrigNoteID);
      if (itemPlan.OrigPlanType == "44")
        this.AdjustParentResult(typeof (INPlanType.inclQtyInTransitToSO), resultList, itemPlan, itemPlan.OrigNoteID);
    }
    InventoryAllocDetEnq.INLocation location = itemPlanWithExtraInfo.Location;
    INLotSerialStatusByCostCenter lotSerialStatus = itemPlanWithExtraInfo.LotSerialStatus;
    InventoryAllocDetEnq.BAccount baccount = itemPlanWithExtraInfo.BAccount;
    object refEntity = itemPlanWithExtraInfo.RefEntity;
    string str1 = "N";
    int? costCenterId = itemPlan.CostCenterID;
    int num1 = 0;
    if (!(costCenterId.GetValueOrDefault() == num1 & costCenterId.HasValue))
    {
      INCostCenter inCostCenter = INCostCenter.PK.Find((PXGraph) this, itemPlan.CostCenterID);
      if (inCostCenter != null)
        str1 = inCostCenter.CostLayerType;
    }
    Decimal num2 = (Decimal) valueOrDefault1;
    Decimal? planQty = itemPlan.PlanQty;
    Decimal valueOrDefault2 = (planQty.HasValue ? new Decimal?(num2 * planQty.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
    InventoryAllocDetEnqResult allocDetEnqResult1 = new InventoryAllocDetEnqResult();
    allocDetEnqResult1.Module = INPlanConstants.ToModuleField(itemPlan.PlanType);
    allocDetEnqResult1.AllocationType = planTypeInclQtyField.Name;
    allocDetEnqResult1.PlanDate = itemPlan.PlanDate;
    allocDetEnqResult1.QADocType = itemPlan.RefEntityType;
    allocDetEnqResult1.RefNoteID = itemPlan.RefNoteID;
    allocDetEnqResult1.RefNbr = refEntity != null ? this._entityHelper.GetEntityRowID(refEntity.GetType(), refEntity, ", ") : (string) null;
    allocDetEnqResult1.SiteID = itemPlan.SiteID;
    allocDetEnqResult1.LocationID = itemPlan.LocationID;
    allocDetEnqResult1.Reverse = itemPlan.Reverse;
    nullable = itemPlan.Reverse;
    allocDetEnqResult1.PlanQty = new Decimal?(nullable.GetValueOrDefault() ? -valueOrDefault2 : valueOrDefault2);
    allocDetEnqResult1.BAccountID = baccount.BAccountID;
    allocDetEnqResult1.AcctName = baccount.AcctName;
    nullable = location.InclQtyAvail;
    bool flag1 = false;
    allocDetEnqResult1.LocNotAvailable = new bool?(nullable.GetValueOrDefault() == flag1 & nullable.HasValue);
    DateTime? expireDate = lotSerialStatus.ExpireDate;
    int num3;
    if (expireDate.HasValue)
    {
      expireDate = lotSerialStatus.ExpireDate;
      DateTime? businessDate = ((PXGraph) this).Accessinfo.BusinessDate;
      num3 = expireDate.HasValue & businessDate.HasValue ? (expireDate.GetValueOrDefault() < businessDate.GetValueOrDefault() ? 1 : 0) : 0;
    }
    else
      num3 = 0;
    allocDetEnqResult1.Expired = new bool?(num3 != 0);
    allocDetEnqResult1.LotSerialNbr = itemPlan.LotSerialNbr;
    allocDetEnqResult1.SubItemID = itemPlan.SubItemID;
    allocDetEnqResult1.PlanID = itemPlan.PlanID;
    allocDetEnqResult1.ExcludePlanLevel = itemPlan.ExcludePlanLevel;
    allocDetEnqResult1.Hold = itemPlan.Hold;
    allocDetEnqResult1.CostLayerType = str1;
    InventoryAllocDetEnqResult allocDetEnqResult2 = allocDetEnqResult1;
    if (refEntity is InventoryAllocDetEnq.INRegister inRegister)
    {
      allocDetEnqResult2.QADocType += inRegister.DocType;
      if (!this._displayNameByEntityType.ContainsKey(allocDetEnqResult2.QADocType))
      {
        string name = ((PXCacheNameAttribute) new INRegisterCacheNameAttribute("Receipt")).GetName((object) new PX.Objects.IN.INRegister()
        {
          DocType = inRegister.DocType
        });
        this._displayNameByEntityType.Add(allocDetEnqResult2.QADocType, name);
      }
    }
    else if (!this._displayNameByEntityType.ContainsKey(allocDetEnqResult2.QADocType))
    {
      System.Type type = PXBuildManager.GetType(allocDetEnqResult2.QADocType, false);
      string str2 = ((object) type != null ? (PXNameAttribute) ((IEnumerable<object>) type.GetCustomAttributes(typeof (PXCacheNameAttribute), false)).FirstOrDefault<object>() : (PXNameAttribute) (object) null)?.GetName() ?? allocDetEnqResult2.QADocType;
      this._displayNameByEntityType.Add(allocDetEnqResult2.QADocType, str2);
    }
    resultList.Add(allocDetEnqResult2);
  }

  protected virtual IEnumerable resultRecords() => (IEnumerable) this.cachedResultRecords.Value;

  protected virtual IEnumerable<InventoryAllocDetEnqResult> CalculateResultRecords()
  {
    this._displayNameByEntityType.Clear();
    InventoryAllocDetEnqFilter current = ((PXSelectBase<InventoryAllocDetEnqFilter>) this.Filter).Current;
    if (!current.InventoryID.HasValue)
      return Enumerable.Empty<InventoryAllocDetEnqResult>();
    PXResultset<InventoryAllocDetEnq.INItemPlan> records = this.GetResultRecordsSelect(current).Select(Array.Empty<object>());
    List<InventoryAllocDetEnqResult> resultList = new List<InventoryAllocDetEnqResult>();
    foreach (InventoryAllocDetEnq.ItemPlanWithExtraInfo itemPlanWithExtraInfo in this.UnwrapAndGroup(records))
    {
      System.Type inclQtyField = INPlanConstants.ToInclQtyField(itemPlanWithExtraInfo.ItemPlan.PlanType);
      if (inclQtyField != (System.Type) null && inclQtyField != typeof (INPlanType.inclQtyINReplaned))
        this.ProcessItemPlanRecAs(inclQtyField, resultList, itemPlanWithExtraInfo);
    }
    int num = 1;
    DateTime dateTime1 = new DateTime(1900, 1, 1);
    foreach (InventoryAllocDetEnqResult allocDetEnqResult in resultList)
    {
      DateTime? planDate = allocDetEnqResult.PlanDate;
      DateTime dateTime2 = dateTime1;
      if ((planDate.HasValue ? (planDate.GetValueOrDefault() == dateTime2 ? 1 : 0) : 0) != 0)
        allocDetEnqResult.PlanDate = new DateTime?();
      allocDetEnqResult.GridLineNbr = new int?(num++);
    }
    PXStringListAttribute.SetList<InventoryAllocDetEnqResult.qADocType>((PXCache) GraphHelper.Caches<InventoryAllocDetEnqResult>((PXGraph) this), (object) null, this._displayNameByEntityType.Keys.ToArray<string>(), this._displayNameByEntityType.Values.ToArray<string>());
    return (IEnumerable<InventoryAllocDetEnqResult>) resultList;
  }

  public virtual bool IsDirty => false;

  public virtual PXSelectBase<InventoryAllocDetEnq.INItemPlan> GetResultRecordsSelect(
    InventoryAllocDetEnqFilter filter)
  {
    PXSelectBase<InventoryAllocDetEnq.INItemPlan> resultRecordsSelect = (PXSelectBase<InventoryAllocDetEnq.INItemPlan>) new PXSelectJoin<InventoryAllocDetEnq.INItemPlan, InnerJoin<INPlanType, On<PX.Objects.IN.INItemPlan.FK.PlanType>, LeftJoin<InventoryAllocDetEnq.INLocation, On<PX.Objects.IN.INItemPlan.FK.Location>, LeftJoin<INLotSerialStatusByCostCenter, On<PX.Objects.IN.INItemPlan.FK.LotSerialStatusByCostCenter>, LeftJoin<InventoryAllocDetEnq.BAccount, On<PX.Objects.IN.INItemPlan.FK.BAccount>, LeftJoin<INSubItem, On<PX.Objects.IN.INItemPlan.FK.SubItem>, InnerJoin<InventoryAllocDetEnq.INSite, On<PX.Objects.IN.INItemPlan.FK.Site>, LeftJoin<InventoryAllocDetEnq.SOShipment, On<InventoryAllocDetEnq.SOShipment.noteID, Equal<PX.Objects.IN.INItemPlan.refNoteID>>, LeftJoin<InventoryAllocDetEnq.ARRegister, On<InventoryAllocDetEnq.ARRegister.noteID, Equal<PX.Objects.IN.INItemPlan.refNoteID>>, LeftJoin<InventoryAllocDetEnq.INRegister, On<InventoryAllocDetEnq.INRegister.noteID, Equal<PX.Objects.IN.INItemPlan.refNoteID>>, LeftJoin<InventoryAllocDetEnq.SOOrder, On<InventoryAllocDetEnq.SOOrder.noteID, Equal<PX.Objects.IN.INItemPlan.refNoteID>>, LeftJoin<InventoryAllocDetEnq.POOrder, On<InventoryAllocDetEnq.POOrder.noteID, Equal<PX.Objects.IN.INItemPlan.refNoteID>>, LeftJoin<InventoryAllocDetEnq.POReceipt, On<InventoryAllocDetEnq.POReceipt.noteID, Equal<PX.Objects.IN.INItemPlan.refNoteID>>, LeftJoin<InventoryAllocDetEnq.INTransitLine, On<InventoryAllocDetEnq.INTransitLine.noteID, Equal<PX.Objects.IN.INItemPlan.refNoteID>>>>>>>>>>>>>>>, Where<PX.Objects.IN.INItemPlan.planQty, NotEqual<decimal0>, And<PX.Objects.IN.INItemPlan.inventoryID, Equal<Current<InventoryAllocDetEnqFilter.inventoryID>>, And<Match<InventoryAllocDetEnq.INSite, Current<AccessInfo.userName>>>>>, OrderBy<Asc<INSubItem.subItemCD, Asc<InventoryAllocDetEnq.INSite.siteCD, Asc<PX.Objects.IN.INItemPlan.origPlanType, Asc<PX.Objects.IN.INItemPlan.planType, Asc<InventoryAllocDetEnq.INLocation.locationCD>>>>>>>((PXGraph) this);
    if (!SubCDUtils.IsSubCDEmpty(filter.SubItemCD) && PXAccess.FeatureInstalled<FeaturesSet.subItem>())
      resultRecordsSelect.WhereAnd<Where<INSubItem.subItemCD, Like<Current<InventoryAllocDetEnqFilter.subItemCDWildcard>>>>();
    int? nullable = filter.SiteID;
    if (nullable.HasValue)
      resultRecordsSelect.WhereAnd<Where<PX.Objects.IN.INItemPlan.siteID, Equal<Current<InventoryAllocDetEnqFilter.siteID>>>>();
    nullable = filter.LocationID;
    if (nullable.HasValue)
    {
      nullable = filter.LocationID;
      if (nullable.GetValueOrDefault() != -1 && PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
        resultRecordsSelect.WhereAnd<Where<PX.Objects.IN.INItemPlan.locationID, Equal<Current<InventoryAllocDetEnqFilter.locationID>>>>();
    }
    if (!string.IsNullOrEmpty(filter.LotSerialNbr))
      resultRecordsSelect.WhereAnd<Where<PX.Objects.IN.INItemPlan.lotSerialNbr, Like<Current<InventoryAllocDetEnqFilter.lotSerialNbrWildcard>>>>();
    return resultRecordsSelect;
  }

  public virtual InventoryAllocDetEnq.ItemPlanWithExtraInfo[] UnwrapAndGroup(
    PXResultset<InventoryAllocDetEnq.INItemPlan> records)
  {
    return ((IEnumerable<PXResult<InventoryAllocDetEnq.INItemPlan>>) ((IEnumerable<PXResult<InventoryAllocDetEnq.INItemPlan>>) records).ToArray<PXResult<InventoryAllocDetEnq.INItemPlan>>()).Select<PXResult<InventoryAllocDetEnq.INItemPlan>, InventoryAllocDetEnq.ItemPlanWithExtraInfo>((Func<PXResult<InventoryAllocDetEnq.INItemPlan>, InventoryAllocDetEnq.ItemPlanWithExtraInfo>) (rec => new InventoryAllocDetEnq.ItemPlanWithExtraInfo((PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>) rec))).ToArray<InventoryAllocDetEnq.ItemPlanWithExtraInfo>();
  }

  protected virtual void Aggregate(
    InventoryAllocDetEnqFilter aDest,
    InventoryAllocDetEnqResult aSrc)
  {
    if (aSrc.ExcludePlanLevel.HasValue)
    {
      if (aDest.LocationID.HasValue && !string.IsNullOrEmpty(aDest.LotSerialNbr))
      {
        int? excludePlanLevel = aSrc.ExcludePlanLevel;
        if ((excludePlanLevel.HasValue ? new int?(excludePlanLevel.GetValueOrDefault() & 393216 /*0x060000*/) : new int?()).GetValueOrDefault() == 393216 /*0x060000*/)
          return;
      }
      else if (aDest.LocationID.HasValue)
      {
        int? excludePlanLevel = aSrc.ExcludePlanLevel;
        if ((excludePlanLevel.HasValue ? new int?(excludePlanLevel.GetValueOrDefault() & 131072 /*0x020000*/) : new int?()).GetValueOrDefault() == 131072 /*0x020000*/)
          return;
      }
      else
      {
        int? excludePlanLevel = aSrc.ExcludePlanLevel;
        if ((excludePlanLevel.HasValue ? new int?(excludePlanLevel.GetValueOrDefault() & 65536 /*0x010000*/) : new int?()).GetValueOrDefault() == 65536 /*0x010000*/)
          return;
      }
    }
    bool flag1 = EnumerableExtensions.IsIn<string>(aSrc.AllocationType, "inclQtySOPrepared", "inclQtySOBooked", "inclQtySOShipping", "inclQtySOShipped", "inclQtySOBackOrdered", Array.Empty<string>());
    if (((aDest.InclQtySOReverse.GetValueOrDefault() ? 0 : (aSrc.Reverse.GetValueOrDefault() ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
      return;
    string allocationType = aSrc.AllocationType;
    if (allocationType != null)
    {
      switch (allocationType.Length)
      {
        case 15:
          switch (allocationType[7])
          {
            case 'I':
              if (allocationType == "inclQtyINIssues")
              {
                InventoryAllocDetEnqFilter allocDetEnqFilter = aDest;
                Decimal? qtyInIssues = allocDetEnqFilter.QtyINIssues;
                bool? locNotAvailable = aSrc.LocNotAvailable;
                bool flag2 = false;
                Decimal? nullable1;
                if (locNotAvailable.GetValueOrDefault() == flag2 & locNotAvailable.HasValue)
                {
                  bool? expired = aSrc.Expired;
                  bool flag3 = false;
                  if (expired.GetValueOrDefault() == flag3 & expired.HasValue)
                  {
                    nullable1 = aSrc.PlanQty;
                    goto label_72;
                  }
                }
                nullable1 = new Decimal?(0M);
label_72:
                Decimal? nullable2 = nullable1;
                allocDetEnqFilter.QtyINIssues = qtyInIssues.HasValue & nullable2.HasValue ? new Decimal?(qtyInIssues.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
                return;
              }
              break;
            case 'S':
              if (allocationType == "inclQtySOBooked")
              {
                InventoryAllocDetEnqFilter allocDetEnqFilter = aDest;
                Decimal? qtySoBooked = allocDetEnqFilter.QtySOBooked;
                bool? locNotAvailable = aSrc.LocNotAvailable;
                bool flag4 = false;
                Decimal? nullable3;
                if (locNotAvailable.GetValueOrDefault() == flag4 & locNotAvailable.HasValue)
                {
                  bool? expired = aSrc.Expired;
                  bool flag5 = false;
                  if (expired.GetValueOrDefault() == flag5 & expired.HasValue)
                  {
                    nullable3 = aSrc.PlanQty;
                    goto label_39;
                  }
                }
                nullable3 = new Decimal?(0M);
label_39:
                Decimal? nullable4 = nullable3;
                allocDetEnqFilter.QtySOBooked = qtySoBooked.HasValue & nullable4.HasValue ? new Decimal?(qtySoBooked.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                return;
              }
              break;
          }
          break;
        case 16 /*0x10*/:
          switch (allocationType[7])
          {
            case 'I':
              if (allocationType == "inclQtyInTransit")
              {
                InventoryAllocDetEnqFilter allocDetEnqFilter = aDest;
                Decimal? qtyInTransit = allocDetEnqFilter.QtyInTransit;
                bool? locNotAvailable = aSrc.LocNotAvailable;
                bool flag6 = false;
                Decimal? nullable5;
                if (locNotAvailable.GetValueOrDefault() == flag6 & locNotAvailable.HasValue)
                {
                  bool? expired = aSrc.Expired;
                  bool flag7 = false;
                  if (expired.GetValueOrDefault() == flag7 & expired.HasValue)
                  {
                    nullable5 = aSrc.PlanQty;
                    goto label_29;
                  }
                }
                nullable5 = new Decimal?(0M);
label_29:
                Decimal? nullable6 = nullable5;
                allocDetEnqFilter.QtyInTransit = qtyInTransit.HasValue & nullable6.HasValue ? new Decimal?(qtyInTransit.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
                return;
              }
              break;
            case 'S':
              if (allocationType == "inclQtySOShipped")
              {
                InventoryAllocDetEnqFilter allocDetEnqFilter1 = aDest;
                Decimal? qtySoShipped = allocDetEnqFilter1.QtySOShipped;
                bool? locNotAvailable = aSrc.LocNotAvailable;
                bool flag8 = false;
                bool? nullable7;
                Decimal? nullable8;
                if (locNotAvailable.GetValueOrDefault() == flag8 & locNotAvailable.HasValue)
                {
                  nullable7 = aSrc.Expired;
                  bool flag9 = false;
                  if (nullable7.GetValueOrDefault() == flag9 & nullable7.HasValue)
                  {
                    nullable8 = aSrc.PlanQty;
                    goto label_58;
                  }
                }
                nullable8 = new Decimal?(0M);
label_58:
                Decimal? nullable9 = nullable8;
                allocDetEnqFilter1.QtySOShipped = qtySoShipped.HasValue & nullable9.HasValue ? new Decimal?(qtySoShipped.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
                InventoryAllocDetEnqFilter allocDetEnqFilter2 = aDest;
                nullable9 = allocDetEnqFilter2.QtySOShippedReverse;
                nullable7 = aSrc.LocNotAvailable;
                bool flag10 = false;
                Decimal? nullable10;
                if (nullable7.GetValueOrDefault() == flag10 & nullable7.HasValue)
                {
                  nullable7 = aSrc.Expired;
                  bool flag11 = false;
                  if (nullable7.GetValueOrDefault() == flag11 & nullable7.HasValue)
                  {
                    nullable7 = aSrc.Reverse;
                    if (nullable7.GetValueOrDefault())
                    {
                      nullable10 = aSrc.PlanQty;
                      goto label_63;
                    }
                  }
                }
                nullable10 = new Decimal?(0M);
label_63:
                Decimal? nullable11 = nullable10;
                allocDetEnqFilter2.QtySOShippedReverse = nullable9.HasValue & nullable11.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + nullable11.GetValueOrDefault()) : new Decimal?();
                InventoryAllocDetEnqFilter allocDetEnqFilter3 = aDest;
                nullable11 = allocDetEnqFilter3.QtyNotAvail;
                nullable7 = aSrc.LocNotAvailable;
                Decimal? nullable12;
                if (nullable7.GetValueOrDefault())
                {
                  nullable7 = aDest.InclQtySOBooked;
                  if (nullable7.GetValueOrDefault())
                  {
                    nullable12 = aSrc.PlanQty;
                    goto label_67;
                  }
                }
                nullable12 = new Decimal?(0M);
label_67:
                nullable9 = nullable12;
                allocDetEnqFilter3.QtyNotAvail = nullable11.HasValue & nullable9.HasValue ? new Decimal?(nullable11.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
                return;
              }
              break;
          }
          break;
        case 17:
          switch (allocationType[7])
          {
            case 'I':
              if (allocationType == "inclQtyINReceipts")
              {
                InventoryAllocDetEnqFilter allocDetEnqFilter = aDest;
                Decimal? qtyInReceipts = allocDetEnqFilter.QtyINReceipts;
                bool? locNotAvailable = aSrc.LocNotAvailable;
                bool flag12 = false;
                Decimal? nullable = locNotAvailable.GetValueOrDefault() == flag12 & locNotAvailable.HasValue ? aSrc.PlanQty : new Decimal?(0M);
                allocDetEnqFilter.QtyINReceipts = qtyInReceipts.HasValue & nullable.HasValue ? new Decimal?(qtyInReceipts.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
                return;
              }
              break;
            case 'P':
              if (allocationType == "inclQtyPOReceipts")
              {
                InventoryAllocDetEnqFilter allocDetEnqFilter = aDest;
                Decimal? qtyPoReceipts = allocDetEnqFilter.QtyPOReceipts;
                Decimal? planQty = aSrc.PlanQty;
                allocDetEnqFilter.QtyPOReceipts = qtyPoReceipts.HasValue & planQty.HasValue ? new Decimal?(qtyPoReceipts.GetValueOrDefault() + planQty.GetValueOrDefault()) : new Decimal?();
                return;
              }
              break;
            case 'S':
              switch (allocationType)
              {
                case "inclQtySOPrepared":
                  InventoryAllocDetEnqFilter allocDetEnqFilter4 = aDest;
                  Decimal? qtySoPrepared = allocDetEnqFilter4.QtySOPrepared;
                  bool? locNotAvailable1 = aSrc.LocNotAvailable;
                  bool flag13 = false;
                  Decimal? nullable13;
                  if (locNotAvailable1.GetValueOrDefault() == flag13 & locNotAvailable1.HasValue)
                  {
                    bool? expired = aSrc.Expired;
                    bool flag14 = false;
                    if (expired.GetValueOrDefault() == flag14 & expired.HasValue)
                    {
                      nullable13 = aSrc.PlanQty;
                      goto label_34;
                    }
                  }
                  nullable13 = new Decimal?(0M);
label_34:
                  Decimal? nullable14 = nullable13;
                  allocDetEnqFilter4.QtySOPrepared = qtySoPrepared.HasValue & nullable14.HasValue ? new Decimal?(qtySoPrepared.GetValueOrDefault() + nullable14.GetValueOrDefault()) : new Decimal?();
                  return;
                case "inclQtySOShipping":
                  InventoryAllocDetEnqFilter allocDetEnqFilter5 = aDest;
                  Decimal? qtySoShipping = allocDetEnqFilter5.QtySOShipping;
                  bool? locNotAvailable2 = aSrc.LocNotAvailable;
                  bool flag15 = false;
                  bool? nullable15;
                  Decimal? nullable16;
                  if (locNotAvailable2.GetValueOrDefault() == flag15 & locNotAvailable2.HasValue)
                  {
                    nullable15 = aSrc.Expired;
                    bool flag16 = false;
                    if (nullable15.GetValueOrDefault() == flag16 & nullable15.HasValue)
                    {
                      nullable16 = aSrc.PlanQty;
                      goto label_44;
                    }
                  }
                  nullable16 = new Decimal?(0M);
label_44:
                  Decimal? nullable17 = nullable16;
                  allocDetEnqFilter5.QtySOShipping = qtySoShipping.HasValue & nullable17.HasValue ? new Decimal?(qtySoShipping.GetValueOrDefault() + nullable17.GetValueOrDefault()) : new Decimal?();
                  InventoryAllocDetEnqFilter allocDetEnqFilter6 = aDest;
                  nullable17 = allocDetEnqFilter6.QtySOShippingReverse;
                  nullable15 = aSrc.LocNotAvailable;
                  bool flag17 = false;
                  Decimal? nullable18;
                  if (nullable15.GetValueOrDefault() == flag17 & nullable15.HasValue)
                  {
                    nullable15 = aSrc.Expired;
                    bool flag18 = false;
                    if (nullable15.GetValueOrDefault() == flag18 & nullable15.HasValue)
                    {
                      nullable15 = aSrc.Reverse;
                      if (nullable15.GetValueOrDefault())
                      {
                        nullable18 = aSrc.PlanQty;
                        goto label_49;
                      }
                    }
                  }
                  nullable18 = new Decimal?(0M);
label_49:
                  Decimal? nullable19 = nullable18;
                  allocDetEnqFilter6.QtySOShippingReverse = nullable17.HasValue & nullable19.HasValue ? new Decimal?(nullable17.GetValueOrDefault() + nullable19.GetValueOrDefault()) : new Decimal?();
                  InventoryAllocDetEnqFilter allocDetEnqFilter7 = aDest;
                  nullable19 = allocDetEnqFilter7.QtyNotAvail;
                  nullable15 = aSrc.LocNotAvailable;
                  Decimal? nullable20;
                  if (nullable15.GetValueOrDefault())
                  {
                    nullable15 = aDest.InclQtySOBooked;
                    if (nullable15.GetValueOrDefault())
                    {
                      nullable20 = aSrc.PlanQty;
                      goto label_53;
                    }
                  }
                  nullable20 = new Decimal?(0M);
label_53:
                  nullable17 = nullable20;
                  allocDetEnqFilter7.QtyNotAvail = nullable19.HasValue & nullable17.HasValue ? new Decimal?(nullable19.GetValueOrDefault() + nullable17.GetValueOrDefault()) : new Decimal?();
                  return;
              }
              break;
          }
          break;
        case 20:
          if (allocationType == "inclQtySOBackOrdered")
          {
            InventoryAllocDetEnqFilter allocDetEnqFilter8 = aDest;
            Decimal? qtySoBackOrdered = allocDetEnqFilter8.QtySOBackOrdered;
            bool? locNotAvailable3 = aSrc.LocNotAvailable;
            bool flag19 = false;
            Decimal? nullable21;
            if (locNotAvailable3.GetValueOrDefault() == flag19 & locNotAvailable3.HasValue)
            {
              bool? expired = aSrc.Expired;
              bool flag20 = false;
              if (expired.GetValueOrDefault() == flag20 & expired.HasValue)
              {
                nullable21 = aSrc.PlanQty;
                goto label_77;
              }
            }
            nullable21 = new Decimal?(0M);
label_77:
            Decimal? nullable22 = nullable21;
            allocDetEnqFilter8.QtySOBackOrdered = qtySoBackOrdered.HasValue & nullable22.HasValue ? new Decimal?(qtySoBackOrdered.GetValueOrDefault() + nullable22.GetValueOrDefault()) : new Decimal?();
            return;
          }
          break;
      }
    }
    INPlanType plantype = new INPlanType();
    ((PXGraph) this).Caches[typeof (INPlanType)].SetValue((object) plantype, aSrc.AllocationType, (object) (short) 1);
    this.ItemPlanHelperExt.UpdateAllocatedQuantitiesBase<InventoryAllocDetEnqFilter>(aDest, (IQtyPlanned) aSrc, plantype, new bool?(), aSrc.Hold, aSrc.QADocType);
  }

  [PXButton]
  [PXUIField(DisplayName = "Inventory Summary")]
  protected virtual IEnumerable ViewSummary(PXAdapter a)
  {
    object obj = ((PXSelectBase<InventoryAllocDetEnqResult>) this.ResultRecords).Current.With<InventoryAllocDetEnqResult, object>((Func<InventoryAllocDetEnqResult, object>) (cur => ((PXSelectBase) this.ResultRecords).Cache.GetValueExt<InventorySummaryEnquiryResult.subItemID>((object) cur)));
    string str = obj is PXSegmentedState ? (string) ((PXFieldState) obj).Value : (string) obj;
    int? inventoryId = ((PXSelectBase<InventoryAllocDetEnqFilter>) this.Filter).Current.InventoryID;
    string subItemCD = str ?? ((PXSelectBase<InventoryAllocDetEnqFilter>) this.Filter).Current.SubItemCD;
    int? nullable = (int?) ((PXSelectBase<InventoryAllocDetEnqResult>) this.ResultRecords).Current?.SiteID;
    int? siteID = nullable ?? ((PXSelectBase<InventoryAllocDetEnqFilter>) this.Filter).Current.SiteID;
    nullable = (int?) ((PXSelectBase<InventoryAllocDetEnqResult>) this.ResultRecords).Current?.LocationID;
    int? locationID = nullable ?? ((PXSelectBase<InventoryAllocDetEnqFilter>) this.Filter).Current.LocationID;
    InventorySummaryEnq.Redirect(inventoryId, subItemCD, siteID, locationID, false);
    return a.Get();
  }

  [PXUIField(DisplayName = "View Document")]
  [PXButton]
  protected virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<InventoryAllocDetEnqResult>) this.ResultRecords).Current != null)
    {
      InventoryAllocDetEnqResult current = ((PXSelectBase<InventoryAllocDetEnqResult>) this.ResultRecords).Current;
      switch (this._entityHelper.GetEntityRow(current.RefNoteID))
      {
        case PX.Objects.IN.INTransitLine inTransitLine:
          this._entityHelper.NavigateToRow(new Guid?(inTransitLine.RefNoteID.Value), (PXRedirectHelper.WindowMode) 3);
          break;
        case INReplenishmentOrder _:
          if (current.PlanID.HasValue)
          {
            PXRedirectHelper.TryRedirect((PXGraph) this, (object) PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXSelectJoin<PX.Objects.SO.SOOrder, InnerJoin<INReplenishmentLine, On<PX.Objects.SO.SOOrder.orderType, Equal<INReplenishmentLine.sOType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<INReplenishmentLine.sONbr>>>>, Where<INReplenishmentLine.planID, Equal<Required<InventoryAllocDetEnqResult.planID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) current.PlanID
            })), (PXRedirectHelper.WindowMode) 3);
            break;
          }
          goto default;
        default:
          this._entityHelper.NavigateToRow(new Guid?(current.RefNoteID.Value), (PXRedirectHelper.WindowMode) 3);
          break;
      }
      throw new PXException("Unable to navigate on document.");
    }
    return adapter.Get();
  }

  public static void Redirect(
    int? inventoryID,
    string subItemCD,
    string lotSerNum,
    int? siteID,
    int? locationID,
    PXBaseRedirectException.WindowMode mode = 1)
  {
    InventoryAllocDetEnq instance = PXGraph.CreateInstance<InventoryAllocDetEnq>();
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) instance, inventoryID);
    if (inventoryItem != null && inventoryItem.IsConverted.GetValueOrDefault() && !inventoryItem.StkItem.GetValueOrDefault())
      throw new PXException("The {0} item has been converted to a non-stock item.", new object[1]
      {
        (object) inventoryItem.InventoryCD.Trim()
      });
    ((PXSelectBase<InventoryAllocDetEnqFilter>) instance.Filter).Current.InventoryID = inventoryID;
    ((PXSelectBase<InventoryAllocDetEnqFilter>) instance.Filter).Current.SubItemCD = subItemCD;
    ((PXSelectBase<InventoryAllocDetEnqFilter>) instance.Filter).Current.SiteID = siteID;
    ((PXSelectBase<InventoryAllocDetEnqFilter>) instance.Filter).Current.LocationID = locationID;
    ((PXSelectBase<InventoryAllocDetEnqFilter>) instance.Filter).Current.LotSerialNbr = lotSerNum;
    ((PXSelectBase<InventoryAllocDetEnqFilter>) instance.Filter).Select(Array.Empty<object>());
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Allocation Details");
    ((PXBaseRedirectException) requiredException).Mode = mode;
    throw requiredException;
  }

  public class ItemPlanHelper : PX.Objects.IN.GraphExtensions.ItemPlanHelper<InventoryAllocDetEnq>
  {
  }

  [PXHidden]
  public class INItemPlan : PX.Objects.IN.INItemPlan
  {
    [PXDBInt(IsKey = true)]
    [PXDefault]
    public override int? InventoryID { get; set; }
  }

  [PXHidden]
  public class INLocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBForeignIdentity(typeof (INCostSite))]
    public virtual int? LocationID { get; set; }

    [Site(IsKey = true)]
    public virtual int? SiteID { get; set; }

    [LocationRaw(IsKey = true)]
    public virtual string LocationCD { get; set; }

    [PXDBBool]
    public virtual bool? InclQtyAvail { get; set; }

    public abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryAllocDetEnq.INLocation.locationID>
    {
    }

    public abstract class siteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryAllocDetEnq.INLocation.siteID>
    {
    }

    public abstract class locationCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.INLocation.locationCD>
    {
    }

    public abstract class inclQtyAvail : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      InventoryAllocDetEnq.INLocation.inclQtyAvail>
    {
    }
  }

  [PXHidden]
  public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBIdentity]
    public virtual int? BAccountID { get; set; }

    [PXDBString(30, IsUnicode = true, IsKey = true)]
    public virtual string AcctCD { get; set; }

    [PXDBString(60, IsUnicode = true)]
    public virtual string AcctName { get; set; }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryAllocDetEnq.BAccount.bAccountID>
    {
    }

    public abstract class acctCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.BAccount.acctCD>
    {
    }

    public abstract class acctName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.BAccount.acctName>
    {
    }
  }

  [PXHidden]
  public class INSite : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBForeignIdentity(typeof (INCostSite))]
    public virtual int? SiteID { get; set; }

    [SiteRaw(true, IsKey = true)]
    public virtual string SiteCD { get; set; }

    public abstract class siteID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryAllocDetEnq.INSite.siteID>
    {
    }

    public abstract class siteCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.INSite.siteCD>
    {
    }
  }

  [PXHidden]
  public class INRegister : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(1, IsKey = true, IsFixed = true)]
    [INDocType.List]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true)]
    public virtual string RefNbr { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? NoteID { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.INRegister.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.INRegister.refNbr>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      InventoryAllocDetEnq.INRegister.noteID>
    {
    }
  }

  [PXHidden]
  public class SOOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(2, IsKey = true, IsFixed = true)]
    public virtual string OrderType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true)]
    public virtual string OrderNbr { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? NoteID { get; set; }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOOrder.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOOrder.orderNbr>
    {
    }

    public abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    InventoryAllocDetEnq.SOOrder.noteID>
    {
    }
  }

  [PXHidden]
  public class SOOrderShipment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(2, IsKey = true, IsFixed = true)]
    public virtual string OrderType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true)]
    public virtual string OrderNbr { get; set; }

    [PXDBString(1, IsKey = true, IsFixed = true)]
    public virtual string ShipmentType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true)]
    public virtual string ShipmentNbr { get; set; }

    [PXDBString(1, IsFixed = true)]
    public virtual string InvtDocType { get; set; }

    [PXDBString(15, IsUnicode = true, InputMask = "")]
    public virtual string InvtRefNbr { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? OrderNoteID { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? ShipmentNoteID { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? InvtNoteID { get; set; }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOOrderShipment.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOOrderShipment.orderNbr>
    {
    }

    public abstract class shipmentType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOOrderShipment.shipmentType>
    {
    }

    public abstract class shipmentNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOOrderShipment.shipmentNbr>
    {
    }

    public abstract class invtDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOOrderShipment.invtDocType>
    {
    }

    public abstract class invtRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOOrderShipment.invtRefNbr>
    {
    }

    public abstract class orderNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOOrderShipment.orderNoteID>
    {
    }

    public abstract class shipmentNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOOrderShipment.shipmentNoteID>
    {
    }

    public abstract class invtNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOOrderShipment.invtNoteID>
    {
    }
  }

  [PXHidden]
  public class SOShipment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(15, IsKey = true, IsUnicode = true)]
    public virtual string ShipmentNbr { get; set; }

    [PXDBString(1, IsFixed = true)]
    [SOShipmentType.List]
    public virtual string ShipmentType { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? NoteID { get; set; }

    public abstract class shipmentNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOShipment.shipmentNbr>
    {
    }

    public abstract class shipmentType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOShipment.shipmentType>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      InventoryAllocDetEnq.SOShipment.noteID>
    {
    }
  }

  [PXHidden]
  public class ARRegister : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(3, IsKey = true, IsFixed = true)]
    [ARDocType.List]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true)]
    public virtual string RefNbr { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? NoteID { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.ARRegister.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.ARRegister.refNbr>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      InventoryAllocDetEnq.ARRegister.noteID>
    {
    }
  }

  [PXHidden]
  public class POOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(2, IsKey = true, IsFixed = true)]
    [POOrderType.List]
    public virtual string OrderType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true)]
    public virtual string OrderNbr { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? NoteID { get; set; }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.POOrder.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.POOrder.orderNbr>
    {
    }

    public abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    InventoryAllocDetEnq.POOrder.noteID>
    {
    }
  }

  [PXHidden]
  public class POOrderReceipt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(15, IsUnicode = true, IsKey = true)]
    public virtual string ReceiptNbr { get; set; }

    [PXDBString(2, IsKey = true, IsFixed = true)]
    public virtual string POType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true)]
    public virtual string PONbr { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? OrderNoteID { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? ReceiptNoteID { get; set; }

    public abstract class receiptNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.POOrderReceipt.receiptNbr>
    {
    }

    public abstract class pOType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.POOrderReceipt.pOType>
    {
    }

    public abstract class pONbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.POOrderReceipt.pONbr>
    {
    }

    public abstract class orderNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      InventoryAllocDetEnq.POOrderReceipt.orderNoteID>
    {
    }

    public abstract class receiptNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      InventoryAllocDetEnq.POOrderReceipt.receiptNoteID>
    {
    }
  }

  [PXHidden]
  public class POReceipt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(2, IsFixed = true, IsKey = true)]
    [POReceiptType.List]
    public virtual string ReceiptType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true)]
    public virtual string ReceiptNbr { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? NoteID { get; set; }

    public abstract class receiptType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.POReceipt.receiptType>
    {
    }

    public abstract class receiptNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.POReceipt.receiptNbr>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      InventoryAllocDetEnq.POReceipt.noteID>
    {
    }
  }

  [PXHidden]
  public class INTransitLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(15, IsUnicode = true, IsKey = true)]
    public virtual string TransferNbr { get; set; }

    [PXDBInt(IsKey = true)]
    public virtual int? TransferLineNbr { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? NoteID { get; set; }

    [PXDBGuid(false)]
    public virtual Guid? RefNoteID { get; set; }

    public abstract class transferNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryAllocDetEnq.INTransitLine.transferNbr>
    {
    }

    public abstract class transferLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryAllocDetEnq.INTransitLine.transferLineNbr>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      InventoryAllocDetEnq.INTransitLine.noteID>
    {
    }

    public abstract class refNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      InventoryAllocDetEnq.INTransitLine.refNoteID>
    {
    }
  }

  public class ItemPlanWithExtraInfo
  {
    public InventoryAllocDetEnq.INItemPlan ItemPlan { get; }

    public INPlanType PlanType { get; }

    public InventoryAllocDetEnq.INLocation Location { get; }

    public INLotSerialStatusByCostCenter LotSerialStatus { get; }

    public InventoryAllocDetEnq.BAccount BAccount { get; }

    public object RefEntity { get; set; }

    public ItemPlanWithExtraInfo(
      PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine> actualRecord)
    {
      this.ItemPlan = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      this.PlanType = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      this.Location = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      this.LotSerialStatus = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      this.BAccount = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      InventoryAllocDetEnq.INRegister inRegister = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      InventoryAllocDetEnq.SOOrder soOrder = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      InventoryAllocDetEnq.ARRegister arRegister = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      InventoryAllocDetEnq.POOrder poOrder = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      InventoryAllocDetEnq.POReceipt poReceipt = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      InventoryAllocDetEnq.INTransitLine inTransitLine = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      InventoryAllocDetEnq.SOShipment soShipment = PXResult<InventoryAllocDetEnq.INItemPlan, INPlanType, InventoryAllocDetEnq.INLocation, INLotSerialStatusByCostCenter, InventoryAllocDetEnq.BAccount, INSubItem, InventoryAllocDetEnq.INSite, InventoryAllocDetEnq.SOShipment, InventoryAllocDetEnq.ARRegister, InventoryAllocDetEnq.INRegister, InventoryAllocDetEnq.SOOrder, InventoryAllocDetEnq.POOrder, InventoryAllocDetEnq.POReceipt, InventoryAllocDetEnq.INTransitLine>.op_Implicit(actualRecord);
      this.RefEntity = inRegister.RefNbr != null ? (object) inRegister : (soOrder.OrderNbr != null ? (object) soOrder : (poOrder.OrderNbr != null ? (object) poOrder : (soShipment == null || soShipment.ShipmentNbr == null ? (arRegister.RefNbr != null ? (object) arRegister : (poReceipt.ReceiptNbr != null ? (object) poReceipt : (inTransitLine.TransferNbr != null ? (object) inTransitLine : (object) (PXBqlTable) null))) : (object) soShipment)));
    }
  }
}
