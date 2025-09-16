// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceOrderLineSplittingAllocatedExtension
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

[PXProtectedAccess(typeof (FSServiceOrderLineSplittingExtension))]
public abstract class FSServiceOrderLineSplittingAllocatedExtension : 
  PXGraphExtension<FSServiceOrderLineSplittingExtension, ServiceOrderEntry>
{
  protected FSServiceOrder _LastSelected;

  protected virtual FSServiceOrderItemAvailabilityExtension Availability
  {
    get
    {
      return ((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base).FindImplementation<FSServiceOrderItemAvailabilityExtension>();
    }
  }

  public bool IsAllocationEntryEnabled
  {
    get
    {
      return ((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base).FindImplementation<FSServiceOrderItemAvailabilityAllocatedExtension>().IsAllocationEntryEnabled;
    }
  }

  public bool IsQuoteServiceOrder
  {
    get
    {
      FSSrvOrdType fsSrvOrdType = PXResultset<FSSrvOrdType>.op_Implicit(PXSetup<FSSrvOrdType>.Select((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base, Array.Empty<object>()));
      return fsSrvOrdType == null || fsSrvOrdType.Behavior == "QT";
    }
  }

  [PXProtectedAccess(null)]
  protected abstract Dictionary<FSSODet, LSSelect.Counters> LineCounters { get; }

  [PXProtectedAccess(null)]
  protected abstract PXDBOperation CurrentOperation { get; }

  [PXProtectedAccess(null)]
  protected abstract PXCache<FSSODet> LineCache { get; }

  [PXProtectedAccess(null)]
  protected abstract PXCache<FSSODetSplit> SplitCache { get; }

  [PXProtectedAccess(null)]
  protected abstract PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> ReadInventoryItem(
    int? inventoryID);

  [PXProtectedAccess(null)]
  protected abstract FSSODetSplit[] SelectSplits(FSSODetSplit split);

  [PXProtectedAccess(null)]
  protected abstract FSSODetSplit[] SelectSplitsReversed(FSSODetSplit split);

  [PXProtectedAccess(null)]
  protected abstract FSSODetSplit[] SelectSplitsReversed(FSSODetSplit split, bool excludeCompleted = true);

  [PXOverride]
  public virtual void EventHandler(
    AbstractEvents.IRowSelected<FSServiceOrder> e,
    Action<AbstractEvents.IRowSelected<FSServiceOrder>> base_EventHandler)
  {
    base_EventHandler(e);
    if (this._LastSelected == null || this._LastSelected != e.Row)
    {
      PXUIFieldAttribute.SetVisible<FSSODetSplit.shipDate>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.isAllocated>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.completed>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.shippedQty>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.shipmentNbr>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.pOType>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.pONbr>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.pOReceiptNbr>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.pOSource>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.pOCreate>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.receivedQty>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.refNoteID>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      if (e.Row != null)
        this._LastSelected = e.Row;
    }
    if (!this.IsAllocationEntryEnabled)
      return;
    this.Base1.showSplits.SetEnabled(true);
  }

  [PXOverride]
  public virtual void EventHandlerInternal(
    AbstractEvents.IRowInserted<FSSODet> e,
    Action<AbstractEvents.IRowInserted<FSSODet>> base_EventHandlerInternal)
  {
    if (!e.Row.InventoryID.HasValue || e.Row.IsPrepaid.GetValueOrDefault() || this.IsQuoteServiceOrder)
      return;
    if (this.IsSplitRequired(e.Row, e.ExternalCall))
    {
      base_EventHandlerInternal(e);
    }
    else
    {
      ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache.SetValue<FSSODet.expireDate>((object) e.Row, (object) null);
      if (this.IsAllocationEntryEnabled && e.Row != null)
      {
        Decimal? baseOpenQty = e.Row.BaseOpenQty;
        Decimal num = 0M;
        if (!(baseOpenQty.GetValueOrDefault() == num & baseOpenQty.HasValue) && this.ReadInventoryItem(e.Row.InventoryID) != null && EnumerableExtensions.IsIn<string>(e.Row.SOLineType, "GI", "GN"))
          this.IssueAvailable(e.Row);
      }
      this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
    }
  }

  [PXOverride]
  public virtual void EventHandlerInternal(
    AbstractEvents.IRowUpdated<FSSODet> e,
    Action<AbstractEvents.IRowUpdated<FSSODet>> base_EventHandlerInternal)
  {
    if (e.Row == null || !e.OldRow.InventoryID.HasValue && !e.Row.InventoryID.HasValue || e.Row.IsPrepaid.GetValueOrDefault() || this.IsQuoteServiceOrder)
      return;
    PX.Objects.IN.InventoryItem inventoryItem;
    if (this.IsSplitRequired(e.Row, e.ExternalCall && !e.Row.POCreate.GetValueOrDefault(), out inventoryItem))
    {
      base_EventHandlerInternal(e);
      if (inventoryItem == null)
        return;
      bool? nullable = inventoryItem.KitItem;
      if (!nullable.GetValueOrDefault())
      {
        nullable = inventoryItem.StkItem;
        if (!nullable.GetValueOrDefault())
          return;
      }
      this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
    }
    else
    {
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetValue<FSSODet.expireDate>((object) e.Row, (object) null);
      if (this.IsAllocationEntryEnabled)
      {
        PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
        if (e.OldRow != null)
        {
          int? nullable1 = e.OldRow.InventoryID;
          int? inventoryId = e.Row.InventoryID;
          if (nullable1.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable1.HasValue == inventoryId.HasValue)
          {
            int? nullable2 = e.OldRow.SubItemID;
            nullable1 = e.Row.SubItemID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              short? invtMult1 = e.OldRow.InvtMult;
              nullable1 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
              short? invtMult2 = e.Row.InvtMult;
              nullable2 = invtMult2.HasValue ? new int?((int) invtMult2.GetValueOrDefault()) : new int?();
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && !(e.OldRow.UOM != e.Row.UOM))
                goto label_13;
            }
          }
          this.Base1.RaiseRowDeleted(e.OldRow);
          this.Base1.RaiseRowInserted(e.Row);
          goto label_40;
        }
label_13:
        if (pxResult != null && EnumerableExtensions.IsIn<string>(e.Row.SOLineType, "GI", "GN"))
        {
          if (((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base).IsMobile && !e.Row.OrderQty.HasValue)
            e.Row.OrderQty = e.OldRow.OrderQty;
          bool? nullable;
          if (!((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<FSSODet.orderQty, FSSODet.completed>((object) e.Row, (object) e.OldRow))
          {
            e.Row.BaseOpenQty = new Decimal?(INUnitAttribute.ConvertToBase(((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache, e.Row.InventoryID, e.Row.UOM, e.Row.OpenQty.Value, e.Row.BaseOpenQty, INPrecision.QUANTITY));
            nullable = e.Row.Completed;
            if (nullable.GetValueOrDefault())
            {
              nullable = e.OldRow.Completed;
              bool flag = false;
              if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
              {
                this.CompleteSchedules(e.Row);
                this.Base1.UpdateParent(e.Row);
                goto label_27;
              }
            }
            nullable = e.Row.Completed;
            bool flag1 = false;
            if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
            {
              nullable = e.OldRow.Completed;
              if (nullable.GetValueOrDefault())
              {
                this.UncompleteSchedules(e.Row);
                this.Base1.UpdateParent(e.Row);
                goto label_27;
              }
            }
            Decimal? baseOpenQty1 = e.Row.BaseOpenQty;
            Decimal? baseOpenQty2 = e.OldRow.BaseOpenQty;
            if (baseOpenQty1.GetValueOrDefault() > baseOpenQty2.GetValueOrDefault() & baseOpenQty1.HasValue & baseOpenQty2.HasValue)
            {
              this.IssueAvailable(e.Row, new Decimal?(e.Row.BaseOpenQty.Value - e.OldRow.BaseOpenQty.Value));
              this.Base1.UpdateParent(e.Row);
            }
            else
            {
              Decimal? baseOpenQty3 = e.Row.BaseOpenQty;
              baseOpenQty1 = e.OldRow.BaseOpenQty;
              if (baseOpenQty3.GetValueOrDefault() < baseOpenQty1.GetValueOrDefault() & baseOpenQty3.HasValue & baseOpenQty1.HasValue)
              {
                FSSODet row = e.Row;
                baseOpenQty1 = e.OldRow.BaseOpenQty;
                Decimal num1 = baseOpenQty1.Value;
                baseOpenQty1 = e.Row.BaseOpenQty;
                Decimal num2 = baseOpenQty1.Value;
                Decimal baseQty = num1 - num2;
                this.TruncateSchedules(row, baseQty);
                this.Base1.UpdateParent(e.Row);
              }
            }
          }
label_27:
          if (!((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<FSSODet.pOCreate, FSSODet.pOSource, FSSODet.vendorID, FSSODet.poVendorLocationID, FSSODet.pOSiteID, FSSODet.locationID, FSSODet.siteLocationID, FSSODet.siteID>((object) e.Row, (object) e.OldRow))
          {
            foreach (FSSODetSplit selectSplit in this.SelectSplits((FSSODetSplit) e.Row))
            {
              nullable = selectSplit.IsAllocated;
              bool flag2 = false;
              if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
              {
                nullable = selectSplit.Completed;
                bool flag3 = false;
                if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
                {
                  selectSplit.SiteID = e.Row.SiteID;
                  if (selectSplit.PONbr == null)
                  {
                    selectSplit.POCreate = e.Row.POCreate;
                    selectSplit.POSource = e.Row.POSource;
                    selectSplit.VendorID = e.Row.VendorID;
                    selectSplit.POSiteID = e.Row.POSiteID;
                    selectSplit.LocationID = e.Row.SiteLocationID;
                  }
                  this.SplitCache.Update(selectSplit);
                }
              }
            }
          }
          DateTime? shipDate1 = e.Row.ShipDate;
          DateTime? shipDate2 = e.OldRow.ShipDate;
          if ((shipDate1.HasValue == shipDate2.HasValue ? (shipDate1.HasValue ? (shipDate1.GetValueOrDefault() != shipDate2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 || e.Row.ShipComplete != e.OldRow.ShipComplete && e.Row.ShipComplete != "B")
          {
            foreach (FSSODetSplit selectSplit in this.SelectSplits((FSSODetSplit) e.Row))
            {
              selectSplit.ShipDate = e.Row.ShipDate;
              this.SplitCache.Update(selectSplit);
            }
          }
        }
      }
label_40:
      this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
    }
  }

  [PXOverride]
  public virtual void EventHandler(
    AbstractEvents.IRowInserting<FSSODetSplit> e,
    Action<AbstractEvents.IRowInserting<FSSODetSplit>> base_EventHandler)
  {
    base_EventHandler(e);
    if (this.Base1.IsLSEntryEnabled || !this.IsAllocationEntryEnabled)
      return;
    PXCacheEx.AttributeAdjuster<PXLineNbrAttribute> attributeAdjuster;
    if (!e.ExternalCall && this.CurrentOperation == 1)
    {
      foreach (FSSODetSplit selectSplit in this.SelectSplits(e.Row))
      {
        if (this.SchedulesEqual(e.Row, selectSplit))
        {
          FSSODetSplit copy = PXCache<FSSODetSplit>.CreateCopy(selectSplit);
          FSSODetSplit fssoDetSplit1 = selectSplit;
          Decimal? baseQty = fssoDetSplit1.BaseQty;
          Decimal? nullable1 = e.Row.BaseQty;
          fssoDetSplit1.BaseQty = baseQty.HasValue & nullable1.HasValue ? new Decimal?(baseQty.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          FSSODetSplit fssoDetSplit2 = selectSplit;
          PXCache cache1 = ((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache;
          int? inventoryId1 = selectSplit.InventoryID;
          string uom1 = selectSplit.UOM;
          nullable1 = selectSplit.BaseQty;
          Decimal num1 = nullable1.Value;
          Decimal? nullable2 = new Decimal?(INUnitAttribute.ConvertFromBase(cache1, inventoryId1, uom1, num1, INPrecision.QUANTITY));
          fssoDetSplit2.Qty = nullable2;
          FSSODetSplit fssoDetSplit3 = selectSplit;
          nullable1 = fssoDetSplit3.BaseUnreceivedQty;
          Decimal? nullable3 = e.Row.BaseQty;
          fssoDetSplit3.BaseUnreceivedQty = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          FSSODetSplit fssoDetSplit4 = selectSplit;
          PXCache cache2 = ((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache;
          int? inventoryId2 = selectSplit.InventoryID;
          string uom2 = selectSplit.UOM;
          nullable3 = selectSplit.BaseUnreceivedQty;
          Decimal num2 = nullable3.Value;
          Decimal? nullable4 = new Decimal?(INUnitAttribute.ConvertFromBase(cache2, inventoryId2, uom2, num2, INPrecision.QUANTITY));
          fssoDetSplit4.UnreceivedQty = nullable4;
          ((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache.Current = (object) selectSplit;
          ((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache.RaiseRowUpdated((object) selectSplit, (object) copy);
          GraphHelper.MarkUpdated(((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache, (object) selectSplit);
          ((ICancelEventArgs) e).Cancel = true;
          attributeAdjuster = PXCacheEx.Adjust<PXLineNbrAttribute>(((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache, (object) null);
          attributeAdjuster.For<FSSODetSplit.splitLineNbr>((Action<PXLineNbrAttribute>) (a => a.ClearLastDefaultValue()));
          break;
        }
      }
    }
    if (e.Row.InventoryID.HasValue && !string.IsNullOrEmpty(e.Row.UOM))
      return;
    ((ICancelEventArgs) e).Cancel = true;
    attributeAdjuster = PXCacheEx.Adjust<PXLineNbrAttribute>(((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache, (object) null);
    attributeAdjuster.For<FSSODetSplit.splitLineNbr>((Action<PXLineNbrAttribute>) (a => a.ClearLastDefaultValue()));
  }

  [PXOverride]
  public virtual void EventHandler(
    AbstractEvents.IRowInserted<FSSODetSplit> e,
    Action<AbstractEvents.IRowInserted<FSSODetSplit>> base_EventHandler)
  {
    base_EventHandler(e);
    if (!this.IsAllocationEntryEnabled || (this.Base1.SuppressedMode || !e.Row.IsAllocated.GetValueOrDefault()) && (string.IsNullOrEmpty(e.Row.LotSerialNbr) || e.Row.IsAllocated.GetValueOrDefault()))
      return;
    this.AllocatedUpdated(e.Row, e.ExternalCall);
    ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache.RaiseExceptionHandling<FSSODetSplit.qty>((object) e.Row, (object) null, (Exception) null);
    this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
  }

  [PXOverride]
  public virtual void EventHandler(
    AbstractEvents.IRowUpdated<FSSODetSplit> e,
    Action<AbstractEvents.IRowUpdated<FSSODetSplit>> base_EventHandler)
  {
    base_EventHandler(e);
    INLotSerClass inLotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(e.Row.InventoryID));
    if (this.Base1.SuppressedMode || !this.IsAllocationEntryEnabled || !(inLotSerClass.LotSerAssign != "U"))
      return;
    bool? isAllocated1 = e.Row.IsAllocated;
    bool? isAllocated2 = e.OldRow.IsAllocated;
    int? nullable1;
    int? nullable2;
    if (isAllocated1.GetValueOrDefault() == isAllocated2.GetValueOrDefault() & isAllocated1.HasValue == isAllocated2.HasValue)
    {
      nullable1 = e.Row.POLineNbr;
      int? poLineNbr = e.OldRow.POLineNbr;
      if (!(nullable1.GetValueOrDefault() == poLineNbr.GetValueOrDefault() & nullable1.HasValue == poLineNbr.HasValue))
      {
        nullable2 = e.Row.POLineNbr;
        if (!nullable2.HasValue)
        {
          bool? isAllocated3 = e.Row.IsAllocated;
          bool flag = false;
          if (!(isAllocated3.GetValueOrDefault() == flag & isAllocated3.HasValue))
            goto label_21;
        }
        else
          goto label_21;
      }
      else
        goto label_21;
    }
    if (e.Row.IsAllocated.GetValueOrDefault())
    {
      this.AllocatedUpdated(e.Row, e.ExternalCall);
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseExceptionHandling<FSSODetSplit.qty>((object) e.Row, (object) null, (Exception) null);
      this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
    }
    else
    {
      e.Row.SOOrderType = (string) null;
      e.Row.SOOrderNbr = (string) null;
      FSSODetSplit row1 = e.Row;
      nullable2 = new int?();
      int? nullable3 = nullable2;
      row1.SOLineNbr = nullable3;
      FSSODetSplit row2 = e.Row;
      nullable2 = new int?();
      int? nullable4 = nullable2;
      row2.SOSplitLineNbr = nullable4;
      foreach (FSSODetSplit a in this.SelectSplitsReversed(e.Row))
      {
        nullable2 = a.SplitLineNbr;
        nullable1 = e.Row.SplitLineNbr;
        if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue) && this.SchedulesEqual(a, e.Row))
        {
          FSSODetSplit row3 = e.Row;
          Decimal? nullable5 = row3.Qty;
          Decimal? nullable6 = a.Qty;
          row3.Qty = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
          FSSODetSplit row4 = e.Row;
          nullable6 = row4.BaseQty;
          nullable5 = a.BaseQty;
          row4.BaseQty = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
          FSSODetSplit row5 = e.Row;
          nullable5 = row5.UnreceivedQty;
          nullable6 = a.Qty;
          row5.UnreceivedQty = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
          FSSODetSplit row6 = e.Row;
          nullable6 = row6.BaseUnreceivedQty;
          nullable5 = a.BaseQty;
          row6.BaseUnreceivedQty = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
          if (e.Row.LotSerialNbr != null)
          {
            FSSODetSplit copy = PXCache<FSSODetSplit>.CreateCopy(e.Row);
            e.Row.LotSerialNbr = (string) null;
            ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseRowUpdated((object) e.Row, (object) copy);
          }
          ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetStatus((object) a, ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.GetStatus((object) a) == 2 ? (PXEntryStatus) 4 : (PXEntryStatus) 3);
          ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ClearQueryCacheObsolete();
          PXCache cach = ((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base).Caches[typeof (INItemPlan)];
          INItemPlan inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<P.AsLong>>>.Config>.Select((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base, new object[1]
          {
            (object) e.Row.PlanID
          }));
          if (inItemPlan1 != null)
          {
            INItemPlan inItemPlan2 = inItemPlan1;
            nullable5 = inItemPlan2.PlanQty;
            nullable6 = a.BaseQty;
            inItemPlan2.PlanQty = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
            if (cach.GetStatus((object) inItemPlan1) != 2)
              cach.SetStatus((object) inItemPlan1, (PXEntryStatus) 1);
          }
          INItemPlan inItemPlan3 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<P.AsLong>>>.Config>.Select((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base, new object[1]
          {
            (object) a.PlanID
          }));
          if (inItemPlan3 != null)
          {
            cach.SetStatus((object) inItemPlan3, cach.GetStatus((object) inItemPlan3) == 2 ? (PXEntryStatus) 4 : (PXEntryStatus) 3);
            cach.ClearQueryCacheObsolete();
          }
          this.RefreshViewOf(((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache);
        }
        else
        {
          nullable1 = a.SplitLineNbr;
          nullable2 = e.Row.SplitLineNbr;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && this.SchedulesEqual(a, e.Row) && e.Row.LotSerialNbr != null)
          {
            FSSODetSplit copy = PXCache<FSSODetSplit>.CreateCopy(e.Row);
            e.Row.LotSerialNbr = (string) null;
            ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseRowUpdated((object) e.Row, (object) copy);
          }
        }
      }
    }
label_21:
    if (string.Equals(e.Row.LotSerialNbr, e.OldRow.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
      return;
    if (e.Row.LotSerialNbr != null)
    {
      this.LotSerialNbrUpdated(e.Row, e.ExternalCall);
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseExceptionHandling<FSSODetSplit.qty>((object) e.Row, (object) null, (Exception) null);
      this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
    }
    else
    {
      foreach (FSSODetSplit a in this.SelectSplitsReversed(e.Row))
      {
        nullable2 = a.SplitLineNbr;
        nullable1 = e.Row.SplitLineNbr;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && this.SchedulesEqual(a, e.Row))
        {
          FSSODetSplit copy = PXCache<FSSODetSplit>.CreateCopy(a);
          e.Row.IsAllocated = new bool?(false);
          ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseFieldUpdated<FSSODetSplit.isAllocated>((object) e.Row, (object) e.Row.IsAllocated);
          ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseRowUpdated((object) a, (object) copy);
        }
      }
    }
  }

  [PXOverride]
  public virtual void EventHandlerQty(
    AbstractEvents.IFieldVerifying<FSSODetSplit, IBqlField, Decimal?> e,
    Action<AbstractEvents.IFieldVerifying<FSSODetSplit, IBqlField, Decimal?>> base_EventHandlerQty)
  {
    if (this.IsAllocationEntryEnabled)
      e.NewValue = this.Base1.VerifySNQuantity(((IGenericEventWith<PXFieldVerifyingEventArgs>) e).Cache, (ILSMaster) e.Row, e.NewValue, "qty");
    else
      base_EventHandlerQty(e);
  }

  [PXOverride]
  public virtual void EventHandlerUOM(
    AbstractEvents.IFieldDefaulting<FSSODetSplit, IBqlField, string> e,
    Action<AbstractEvents.IFieldDefaulting<FSSODetSplit, IBqlField, string>> base_EventHandlerUOM)
  {
    if (!this.IsAllocationEntryEnabled)
    {
      base_EventHandlerUOM(e);
    }
    else
    {
      PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
      if (pxResult == null || !(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack == "S"))
        return;
      e.NewValue = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).BaseUnit;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual bool AllocatedUpdated(FSSODetSplit split, bool externalCall)
  {
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter1 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
    statusByCostCenter1.InventoryID = split.InventoryID;
    statusByCostCenter1.SiteID = split.SiteID;
    statusByCostCenter1.SubItemID = split.SubItemID;
    statusByCostCenter1.CostCenterID = split.CostCenterID;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter copy1 = PXCache<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>.CreateCopy(PXCache<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>.Insert((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base, statusByCostCenter1));
    INSiteStatusByCostCenter statusByCostCenter2 = INSiteStatusByCostCenter.PK.Find((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base, split.InventoryID, split.SubItemID, split.SiteID, split.CostCenterID);
    Decimal? nullable1;
    Decimal? nullable2;
    if (statusByCostCenter2 != null)
    {
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter3 = copy1;
      nullable1 = statusByCostCenter3.QtyAvail;
      Decimal? qtyAvail = statusByCostCenter2.QtyAvail;
      statusByCostCenter3.QtyAvail = nullable1.HasValue & qtyAvail.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + qtyAvail.GetValueOrDefault()) : new Decimal?();
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter4 = copy1;
      nullable2 = statusByCostCenter4.QtyHardAvail;
      nullable1 = statusByCostCenter2.QtyHardAvail;
      statusByCostCenter4.QtyHardAvail = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(split.InventoryID));
    string tranType = split.TranType;
    short? invtMult = split.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    if (INLotSerialNbrAttribute.IsTrack(lotSerClass, tranType, invMult))
    {
      if (split.LotSerialNbr != null)
      {
        this.LotSerialNbrUpdated(split, externalCall);
        return true;
      }
    }
    else
    {
      nullable1 = copy1.QtyHardAvail;
      Decimal num1 = 0M;
      if (nullable1.GetValueOrDefault() < num1 & nullable1.HasValue)
      {
        FSSODetSplit copy2 = PXCache<FSSODetSplit>.CreateCopy(split);
        nullable2 = split.BaseQty;
        Decimal? qtyHardAvail1 = copy1.QtyHardAvail;
        nullable1 = nullable2.HasValue & qtyHardAvail1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + qtyHardAvail1.GetValueOrDefault()) : new Decimal?();
        Decimal num2 = 0M;
        if (nullable1.GetValueOrDefault() > num2 & nullable1.HasValue)
        {
          FSSODetSplit fssoDetSplit = split;
          nullable1 = fssoDetSplit.BaseQty;
          Decimal? qtyHardAvail2 = copy1.QtyHardAvail;
          Decimal? nullable3;
          if (!(nullable1.HasValue & qtyHardAvail2.HasValue))
          {
            nullable2 = new Decimal?();
            nullable3 = nullable2;
          }
          else
            nullable3 = new Decimal?(nullable1.GetValueOrDefault() + qtyHardAvail2.GetValueOrDefault());
          fssoDetSplit.BaseQty = nullable3;
          split.Qty = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) this.SplitCache, split.InventoryID, split.UOM, split.BaseQty.Value, INPrecision.QUANTITY));
        }
        else
        {
          split.IsAllocated = new bool?(false);
          ((PXCache) this.SplitCache).RaiseExceptionHandling<FSSODetSplit.isAllocated>((object) split, (object) true, (Exception) new PXSetPropertyException("Inventory quantity will go negative."));
        }
        ((PXCache) this.SplitCache).RaiseFieldUpdated<FSSODetSplit.isAllocated>((object) split, (object) copy2.IsAllocated);
        this.SplitCache.RaiseRowUpdated(split, copy2);
        if (split.IsAllocated.GetValueOrDefault())
        {
          copy2.SplitLineNbr = new int?();
          copy2.PlanID = new long?();
          copy2.IsAllocated = new bool?(false);
          FSSODetSplit fssoDetSplit1 = copy2;
          Decimal? nullable4 = copy1.QtyHardAvail;
          Decimal? nullable5;
          if (!nullable4.HasValue)
          {
            nullable1 = new Decimal?();
            nullable5 = nullable1;
          }
          else
            nullable5 = new Decimal?(-nullable4.GetValueOrDefault());
          fssoDetSplit1.BaseQty = nullable5;
          FSSODetSplit fssoDetSplit2 = copy2;
          PXCache<FSSODetSplit> splitCache = this.SplitCache;
          int? inventoryId = copy2.InventoryID;
          string uom = copy2.UOM;
          nullable4 = copy2.BaseQty;
          Decimal num3 = nullable4.Value;
          Decimal? nullable6 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) splitCache, inventoryId, uom, num3, INPrecision.QUANTITY));
          fssoDetSplit2.Qty = nullable6;
          this.SplitCache.Insert(copy2);
        }
        this.RefreshViewOf((PXCache) this.SplitCache);
        return true;
      }
    }
    return false;
  }

  protected virtual bool LotSerialNbrUpdated(FSSODetSplit split, bool externalCall)
  {
    SiteLotSerial siteLotSerial1 = new SiteLotSerial();
    siteLotSerial1.InventoryID = split.InventoryID;
    siteLotSerial1.SiteID = split.SiteID;
    siteLotSerial1.LotSerialNbr = split.LotSerialNbr;
    SiteLotSerial copy1 = PXCache<SiteLotSerial>.CreateCopy(PXCache<SiteLotSerial>.Insert((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base, siteLotSerial1));
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(split.InventoryID);
    INSiteLotSerial inSiteLotSerial = INSiteLotSerial.PK.Find((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base, split.InventoryID, split.SiteID, split.LotSerialNbr);
    Decimal? nullable1;
    Decimal? nullable2;
    if (inSiteLotSerial != null)
    {
      SiteLotSerial siteLotSerial2 = copy1;
      nullable1 = siteLotSerial2.QtyAvail;
      Decimal? qtyAvail = inSiteLotSerial.QtyAvail;
      siteLotSerial2.QtyAvail = nullable1.HasValue & qtyAvail.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + qtyAvail.GetValueOrDefault()) : new Decimal?();
      SiteLotSerial siteLotSerial3 = copy1;
      nullable2 = siteLotSerial3.QtyHardAvail;
      nullable1 = inSiteLotSerial.QtyHardAvail;
      siteLotSerial3.QtyHardAvail = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    INLotSerClass lotSerClass1 = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType1 = split.TranType;
    short? invtMult = split.InvtMult;
    int? invMult1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    if (INLotSerialNbrAttribute.IsTrackSerial(lotSerClass1, tranType1, invMult1) && split.LotSerialNbr != null)
    {
      FSSODetSplit copy2 = PXCache<FSSODetSplit>.CreateCopy(split);
      if (inSiteLotSerial != null)
      {
        nullable1 = inSiteLotSerial.QtyAvail;
        Decimal num1 = 0M;
        if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
        {
          nullable1 = inSiteLotSerial.QtyHardAvail;
          Decimal num2 = 0M;
          if (nullable1.GetValueOrDefault() > num2 & nullable1.HasValue)
          {
            if (split.Operation != "R")
            {
              split.BaseQty = new Decimal?((Decimal) 1);
              FSSODetSplit fssoDetSplit = split;
              PXCache<FSSODetSplit> splitCache = this.SplitCache;
              int? inventoryId = split.InventoryID;
              string uom = split.UOM;
              nullable1 = split.BaseQty;
              Decimal num3 = nullable1.Value;
              Decimal? nullable3 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) splitCache, inventoryId, uom, num3, INPrecision.QUANTITY));
              fssoDetSplit.Qty = nullable3;
              split.IsAllocated = new bool?(true);
              goto label_14;
            }
            split.IsAllocated = new bool?(false);
            ((PXCache) this.SplitCache).RaiseExceptionHandling<FSSODetSplit.lotSerialNbr>((object) split, (object) null, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefixNLA("Serial Number '{1}' for item '{0}' is already received.", new object[2]
            {
              (object) PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).InventoryCD,
              (object) split.LotSerialNbr
            })));
            goto label_14;
          }
        }
      }
      bool? isAllocated;
      if (split.Operation != "R")
      {
        bool flag = this.ShouldThrowExceptions();
        if (externalCall || flag)
        {
          split.IsAllocated = new bool?(false);
          split.LotSerialNbr = (string) null;
          ((PXCache) this.SplitCache).RaiseExceptionHandling<FSSODetSplit.lotSerialNbr>((object) split, (object) null, (Exception) new PXSetPropertyException("Inventory quantity will go negative."));
          isAllocated = split.IsAllocated;
          if (isAllocated.GetValueOrDefault())
            return false;
        }
      }
      else
      {
        split.BaseQty = new Decimal?((Decimal) 1);
        FSSODetSplit fssoDetSplit = split;
        PXCache<FSSODetSplit> splitCache = this.SplitCache;
        int? inventoryId = split.InventoryID;
        string uom = split.UOM;
        nullable1 = split.BaseQty;
        Decimal num = nullable1.Value;
        Decimal? nullable4 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) splitCache, inventoryId, uom, num, INPrecision.QUANTITY));
        fssoDetSplit.Qty = nullable4;
      }
label_14:
      ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.isAllocated>((object) split, (object) copy2.IsAllocated);
      this.SplitCache.RaiseRowUpdated(split, copy2);
      nullable2 = copy2.BaseQty;
      Decimal num4 = (Decimal) 1;
      nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num4) : new Decimal?();
      Decimal num5 = 0M;
      if (nullable1.GetValueOrDefault() > num5 & nullable1.HasValue)
      {
        isAllocated = split.IsAllocated;
        if (!isAllocated.GetValueOrDefault())
        {
          isAllocated = split.IsAllocated;
          if (isAllocated.GetValueOrDefault() || !(split.Operation == "R"))
            goto label_21;
        }
        copy2.SplitLineNbr = new int?();
        copy2.PlanID = new long?();
        copy2.IsAllocated = new bool?(false);
        copy2.LotSerialNbr = (string) null;
        FSSODetSplit fssoDetSplit1 = copy2;
        nullable1 = fssoDetSplit1.BaseQty;
        Decimal num6 = (Decimal) 1;
        Decimal? nullable5;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable5 = nullable2;
        }
        else
          nullable5 = new Decimal?(nullable1.GetValueOrDefault() - num6);
        fssoDetSplit1.BaseQty = nullable5;
        FSSODetSplit fssoDetSplit2 = copy2;
        PXCache<FSSODetSplit> splitCache = this.SplitCache;
        int? inventoryId = copy2.InventoryID;
        string uom = copy2.UOM;
        nullable1 = copy2.BaseQty;
        Decimal num7 = nullable1.Value;
        Decimal? nullable6 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) splitCache, inventoryId, uom, num7, INPrecision.QUANTITY));
        fssoDetSplit2.Qty = nullable6;
        this.SplitCache.Insert(copy2);
label_21:
        this.RefreshViewOf((PXCache) this.SplitCache);
        return true;
      }
    }
    else
    {
      INLotSerClass lotSerClass2 = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
      string tranType2 = split.TranType;
      invtMult = split.InvtMult;
      int? invMult2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      if (INLotSerialNbrAttribute.IsTrack(lotSerClass2, tranType2, invMult2) && split.LotSerialNbr != null)
      {
        INLotSerClass lotSerClass3 = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
        string tranType3 = split.TranType;
        invtMult = split.InvtMult;
        int? invMult3 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
        if (!INLotSerialNbrAttribute.IsTrackSerial(lotSerClass3, tranType3, invMult3))
        {
          nullable1 = split.BaseQty;
          Decimal num8 = 0M;
          if (nullable1.GetValueOrDefault() > num8 & nullable1.HasValue)
          {
            bool? isAllocated = split.IsAllocated;
            bool flag = false;
            if (isAllocated.GetValueOrDefault() == flag & isAllocated.HasValue)
            {
              if (inSiteLotSerial != null)
              {
                nullable1 = inSiteLotSerial.QtyOnHand;
                Decimal num9 = 0M;
                if (nullable1.GetValueOrDefault() > num9 & nullable1.HasValue)
                {
                  nullable1 = copy1.QtyHardAvail;
                  Decimal num10 = 0M;
                  if (nullable1.GetValueOrDefault() <= num10 & nullable1.HasValue)
                    goto label_30;
                }
                nullable1 = inSiteLotSerial.QtyOnHand;
                Decimal num11 = 0M;
                if (!(nullable1.GetValueOrDefault() <= num11 & nullable1.HasValue))
                  goto label_32;
label_30:
                if (split.Operation != "R")
                  goto label_31;
label_32:
                FSSODetSplit copy3 = PXCache<FSSODetSplit>.CreateCopy(split);
                split.IsAllocated = new bool?(true);
                ((PXCache) this.SplitCache).RaiseFieldUpdated<FSSODetSplit.isAllocated>((object) split, (object) copy3.IsAllocated);
                this.SplitCache.RaiseRowUpdated(split, copy3);
                return true;
              }
label_31:
              return this.NegativeInventoryError(split);
            }
            if (split.IsAllocated.GetValueOrDefault())
            {
              FSSODetSplit copy4 = PXCache<FSSODetSplit>.CreateCopy(split);
              if (inSiteLotSerial != null)
              {
                nullable1 = inSiteLotSerial.QtyOnHand;
                Decimal num12 = 0M;
                if (nullable1.GetValueOrDefault() > num12 & nullable1.HasValue)
                {
                  nullable1 = copy1.QtyHardAvail;
                  Decimal num13 = 0M;
                  if (nullable1.GetValueOrDefault() >= num13 & nullable1.HasValue && split.Operation != "R")
                  {
                    FSSODetSplit fssoDetSplit = split;
                    PXCache<FSSODetSplit> splitCache = this.SplitCache;
                    int? inventoryId = split.InventoryID;
                    string uom = split.UOM;
                    nullable1 = split.BaseQty;
                    Decimal num14 = nullable1.Value;
                    Decimal? nullable7 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) splitCache, inventoryId, uom, num14, INPrecision.QUANTITY));
                    fssoDetSplit.Qty = nullable7;
                    goto label_48;
                  }
                }
              }
              if (inSiteLotSerial != null)
              {
                nullable1 = inSiteLotSerial.QtyOnHand;
                Decimal num15 = 0M;
                if (nullable1.GetValueOrDefault() > num15 & nullable1.HasValue)
                {
                  nullable1 = copy1.QtyHardAvail;
                  Decimal num16 = 0M;
                  if (nullable1.GetValueOrDefault() < num16 & nullable1.HasValue && split.Operation != "R")
                  {
                    FSSODetSplit fssoDetSplit3 = split;
                    nullable1 = fssoDetSplit3.BaseQty;
                    nullable2 = copy1.QtyHardAvail;
                    fssoDetSplit3.BaseQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
                    nullable2 = split.BaseQty;
                    Decimal num17 = 0M;
                    if (nullable2.GetValueOrDefault() <= num17 & nullable2.HasValue && this.NegativeInventoryError(split))
                      return false;
                    FSSODetSplit fssoDetSplit4 = split;
                    PXCache<FSSODetSplit> splitCache = this.SplitCache;
                    int? inventoryId = split.InventoryID;
                    string uom = split.UOM;
                    nullable2 = split.BaseQty;
                    Decimal num18 = nullable2.Value;
                    Decimal? nullable8 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) splitCache, inventoryId, uom, num18, INPrecision.QUANTITY));
                    fssoDetSplit4.Qty = nullable8;
                    goto label_48;
                  }
                }
              }
              if (inSiteLotSerial != null)
              {
                nullable2 = inSiteLotSerial.QtyOnHand;
                Decimal num19 = 0M;
                if (!(nullable2.GetValueOrDefault() <= num19 & nullable2.HasValue) || !(split.Operation != "R"))
                  goto label_48;
              }
              if (this.NegativeInventoryError(split))
                return false;
label_48:
              ((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base).FindImplementation<FSSODetSplitPlan>().RaiseRowUpdated(split);
              nullable1 = copy4.BaseQty;
              Decimal? baseQty = split.BaseQty;
              nullable2 = nullable1.HasValue & baseQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - baseQty.GetValueOrDefault()) : new Decimal?();
              Decimal num20 = 0M;
              if (nullable2.GetValueOrDefault() > num20 & nullable2.HasValue && split.IsAllocated.GetValueOrDefault())
              {
                using (this.Base1.SuppressedModeScope(true))
                {
                  copy4.SplitLineNbr = new int?();
                  copy4.PlanID = new long?();
                  copy4.IsAllocated = new bool?(false);
                  copy4.LotSerialNbr = (string) null;
                  FSSODetSplit fssoDetSplit5 = copy4;
                  nullable2 = fssoDetSplit5.BaseQty;
                  baseQty = split.BaseQty;
                  Decimal? nullable9;
                  if (!(nullable2.HasValue & baseQty.HasValue))
                  {
                    nullable1 = new Decimal?();
                    nullable9 = nullable1;
                  }
                  else
                    nullable9 = new Decimal?(nullable2.GetValueOrDefault() - baseQty.GetValueOrDefault());
                  fssoDetSplit5.BaseQty = nullable9;
                  FSSODetSplit fssoDetSplit6 = copy4;
                  PXCache<FSSODetSplit> splitCache = this.SplitCache;
                  int? inventoryId = copy4.InventoryID;
                  string uom = copy4.UOM;
                  baseQty = copy4.BaseQty;
                  Decimal num21 = baseQty.Value;
                  Decimal? nullable10 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) splitCache, inventoryId, uom, num21, INPrecision.QUANTITY));
                  fssoDetSplit6.Qty = nullable10;
                  FSSODetSplit fssoDetSplit7 = this.SplitCache.Insert(copy4);
                  if (fssoDetSplit7.LotSerialNbr != null)
                  {
                    if (!fssoDetSplit7.IsAllocated.GetValueOrDefault())
                      ((PXCache) this.SplitCache).SetValue<FSSODetSplit.lotSerialNbr>((object) fssoDetSplit7, (object) null);
                  }
                }
              }
              this.RefreshViewOf((PXCache) this.SplitCache);
              return true;
            }
          }
        }
      }
    }
    return false;
  }

  /// <summary>
  /// Overrides <see cref="!:FSServiceOrderItemAvailabilityExtension.ShowSplits(PXAdapter)" />
  /// </summary>
  [PXOverride]
  public virtual IEnumerable ShowSplits(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> base_ShowSplits)
  {
    int num = this.IsAllocationEntryEnabled ? 1 : 0;
    return base_ShowSplits(adapter);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.UpdateParent(`2)" />
  /// </summary>
  [PXOverride]
  public virtual void UpdateParent(FSSODet line, Action<FSSODet> base_UpdateParent)
  {
    if (line != null && line.RequireShipping.GetValueOrDefault())
      this.Base1.UpdateParent(line, (FSSODetSplit) null, (FSSODetSplit) null, out Decimal _);
    else
      base_UpdateParent(line);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.UpdateParent(`3,`3)" />
  /// </summary>
  [PXOverride]
  public virtual FSSODet UpdateParent(
    FSSODetSplit newSplit,
    FSSODetSplit oldSplit,
    Func<FSSODetSplit, FSSODetSplit, FSSODet> base_UpdateParent)
  {
    FSSODetSplit row = newSplit ?? oldSplit;
    FSSODet line1 = (FSSODet) LSParentAttribute.SelectParent((PXCache) this.SplitCache, (object) row, typeof (FSSODet));
    if (line1 != null)
    {
      bool? nullable = line1.RequireShipping;
      if (nullable.GetValueOrDefault())
      {
        if (row != null)
        {
          int? inventoryId1 = row.InventoryID;
          int? inventoryId2 = line1.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            FSSODet copy = PXCache<FSSODet>.CreateCopy(line1);
            FSServiceOrderLineSplittingExtension base1 = this.Base1;
            FSSODet line2 = line1;
            FSSODetSplit newSplit1;
            if (newSplit != null)
            {
              nullable = newSplit.Completed;
              bool flag = false;
              if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
              {
                newSplit1 = newSplit;
                goto label_8;
              }
            }
            newSplit1 = (FSSODetSplit) null;
label_8:
            FSSODetSplit oldSplit1;
            if (oldSplit != null)
            {
              nullable = oldSplit.Completed;
              bool flag = false;
              if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
              {
                oldSplit1 = oldSplit;
                goto label_12;
              }
            }
            oldSplit1 = (FSSODetSplit) null;
label_12:
            Decimal num;
            ref Decimal local = ref num;
            base1.UpdateParent(line2, newSplit1, oldSplit1, out local);
            using (new LineSplittingExtension<ServiceOrderEntry, FSServiceOrder, FSSODet, FSSODetSplit>.InvtMultScope(line1))
            {
              if (this.Base1.IsLotSerialRequired && newSplit != null)
                line1.UnassignedQty = !this.IsLotSerialItem((ILSMaster) newSplit) ? new Decimal?(0M) : ((IEnumerable<FSSODetSplit>) this.SelectSplits(newSplit)).Where<FSSODetSplit>((Func<FSSODetSplit, bool>) (s => s.LotSerialNbr == null)).Sum<FSSODetSplit>((Func<FSSODetSplit, Decimal?>) (s => s.BaseQty));
              line1.BaseQty = new Decimal?(num);
              line1.Qty = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) this.LineCache, line1.InventoryID, line1.UOM, line1.BaseQty.Value, INPrecision.QUANTITY));
            }
            GraphHelper.MarkUpdated((PXCache) this.LineCache, (object) line1);
            ((PXCache) this.LineCache).RaiseFieldUpdated<FSSODet.orderQty>((object) line1, (object) copy.Qty);
            if (this.LineCache.RaiseRowUpdating(copy, line1))
              this.LineCache.RaiseRowUpdated(line1, copy);
            else
              PXCache<FSSODet>.RestoreCopy(line1, copy);
          }
        }
        return line1;
      }
    }
    return base_UpdateParent(newSplit, oldSplit);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.UpdateParent(`2,`3,`3,System.Decimal@)" />
  /// </summary>
  [PXOverride]
  public virtual void UpdateParent(
    FSSODet line,
    FSSODetSplit newSplit,
    FSSODetSplit oldSplit,
    out Decimal baseQty,
    FSServiceOrderLineSplittingAllocatedExtension.UpdateParentDelegate base_UpdateParent)
  {
    FSServiceOrderLineSplittingAllocatedExtension.ResetAvailabilityCounters(line);
    int num1 = this.LineCounters.ContainsKey(line) ? 1 : 0;
    base_UpdateParent(line, newSplit, oldSplit, out baseQty);
    LSSelect.Counters counters1;
    if (num1 != 0 || oldSplit == null || !this.LineCounters.TryGetValue(line, out counters1))
      return;
    if (oldSplit.POCreate.GetValueOrDefault())
    {
      LSSelect.Counters counters2 = counters1;
      Decimal baseQty1 = counters2.BaseQty;
      Decimal? nullable = oldSplit.BaseReceivedQty;
      Decimal num2 = nullable.Value;
      nullable = oldSplit.BaseShippedQty;
      Decimal num3 = nullable.Value;
      Decimal num4 = num2 + num3;
      counters2.BaseQty = baseQty1 + num4;
    }
    if (oldSplit.ShipmentNbr != null)
    {
      LSSelect.Counters counters3 = counters1;
      Decimal baseQty2 = counters3.BaseQty;
      Decimal? nullable = oldSplit.BaseQty;
      Decimal num5 = nullable.Value;
      nullable = oldSplit.BaseShippedQty;
      Decimal num6 = nullable.Value;
      Decimal num7 = num5 - num6;
      counters3.BaseQty = baseQty2 + num7;
    }
    baseQty = counters1.BaseQty;
  }

  public static void ResetAvailabilityCounters(FSSODet line)
  {
    line.LineQtyAvail = new Decimal?();
    line.LineQtyHardAvail = new Decimal?();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.FS.FSServiceOrderLineSplittingExtension.UpdateCounters(PX.Objects.IN.LSSelect.Counters,PX.Objects.FS.FSSODetSplit)" />
  /// </summary>
  [PXOverride]
  public virtual void UpdateCounters(
    LSSelect.Counters counters,
    FSSODetSplit split,
    Action<LSSelect.Counters, FSSODetSplit> base_UpdateCounters)
  {
    base_UpdateCounters(counters, split);
    if (!this.IsAllocationEntryEnabled)
      return;
    counters.LotSerNumbersNull = -1;
    counters.LotSerNumber = (string) null;
    counters.LotSerNumbers.Clear();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.DefaultLotSerialNbr(`3)" />
  /// </summary>
  [PXOverride]
  public virtual void DefaultLotSerialNbr(
    FSSODetSplit row,
    Action<FSSODetSplit> base_DefaultLotSerialNbr)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(row.InventoryID);
    if ((pxResult == null || this.IsAllocationEntryEnabled) && !(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerAssign != "U"))
      return;
    base_DefaultLotSerialNbr(row);
  }

  public virtual void UncompleteSchedules(FSSODet line)
  {
    this.LineCounters.Remove(line);
    Decimal? nullable1 = line.BaseOpenQty;
    foreach (FSSODetSplit fssoDetSplit in this.SelectSplitsReversed((FSSODetSplit) line, false))
    {
      if (fssoDetSplit.ShipmentNbr == null)
      {
        Decimal? nullable2 = nullable1;
        Decimal? baseQty = fssoDetSplit.BaseQty;
        nullable1 = nullable2.HasValue & baseQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - baseQty.GetValueOrDefault()) : new Decimal?();
        FSSODetSplit copy = PXCache<FSSODetSplit>.CreateCopy(fssoDetSplit);
        copy.Completed = new bool?(false);
        this.SplitCache.Update(copy);
      }
    }
    this.IssueAvailable(line, new Decimal?(nullable1.Value), true);
  }

  public virtual void CompleteSchedules(FSSODet line)
  {
    this.LineCounters.Remove(line);
    string str = (string) null;
    Decimal? nullable1 = new Decimal?(0M);
    foreach (FSSODetSplit fssoDetSplit in this.SelectSplitsReversed((FSSODetSplit) line, false))
    {
      if (str == null && fssoDetSplit.ShipmentNbr != null)
        str = fssoDetSplit.ShipmentNbr;
      if (str != null && fssoDetSplit.ShipmentNbr == str)
      {
        Decimal? nullable2 = nullable1;
        Decimal? baseOpenQty = fssoDetSplit.BaseOpenQty;
        nullable1 = nullable2.HasValue & baseOpenQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + baseOpenQty.GetValueOrDefault()) : new Decimal?();
      }
    }
    this.TruncateSchedules(line, nullable1.Value);
    foreach (FSSODetSplit fssoDetSplit in this.SelectSplitsReversed((FSSODetSplit) line))
    {
      FSSODetSplit copy = PXCache<FSSODetSplit>.CreateCopy(fssoDetSplit);
      copy.Completed = new bool?(true);
      this.SplitCache.Update(copy);
    }
  }

  public virtual void TruncateSchedules(FSSODet line, Decimal baseQty)
  {
    this.LineCounters.Remove(line);
    foreach (FSSODetSplit fssoDetSplit1 in this.SelectSplitsReversed((FSSODetSplit) line))
    {
      Decimal num1 = baseQty;
      Decimal? baseQty1 = fssoDetSplit1.BaseQty;
      Decimal valueOrDefault = baseQty1.GetValueOrDefault();
      if (num1 >= valueOrDefault & baseQty1.HasValue)
      {
        Decimal num2 = baseQty;
        baseQty1 = fssoDetSplit1.BaseQty;
        Decimal num3 = baseQty1.Value;
        baseQty = num2 - num3;
        this.SplitCache.Delete(fssoDetSplit1);
      }
      else
      {
        FSSODetSplit copy = PXCache<FSSODetSplit>.CreateCopy(fssoDetSplit1);
        FSSODetSplit fssoDetSplit2 = copy;
        baseQty1 = fssoDetSplit2.BaseQty;
        Decimal num4 = baseQty;
        fssoDetSplit2.BaseQty = baseQty1.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() - num4) : new Decimal?();
        FSSODetSplit fssoDetSplit3 = copy;
        PXCache<FSSODetSplit> splitCache = this.SplitCache;
        int? inventoryId = copy.InventoryID;
        string uom = copy.UOM;
        baseQty1 = copy.BaseQty;
        Decimal num5 = baseQty1.Value;
        Decimal? nullable = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) splitCache, inventoryId, uom, num5, INPrecision.QUANTITY));
        fssoDetSplit3.Qty = nullable;
        this.SplitCache.Update(copy);
        break;
      }
    }
  }

  protected virtual bool SchedulesEqual(FSSODetSplit a, FSSODetSplit b)
  {
    if (a == null || b == null)
      return a != null;
    int? nullable1 = a.InventoryID;
    int? inventoryId = b.InventoryID;
    if (nullable1.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable1.HasValue == inventoryId.HasValue)
    {
      int? nullable2 = a.SubItemID;
      nullable1 = b.SubItemID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      {
        nullable1 = a.SiteID;
        nullable2 = b.SiteID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = a.ToSiteID;
          nullable1 = b.ToSiteID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            DateTime? shipDate1 = a.ShipDate;
            DateTime? shipDate2 = b.ShipDate;
            if ((shipDate1.HasValue == shipDate2.HasValue ? (shipDate1.HasValue ? (shipDate1.GetValueOrDefault() == shipDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
            {
              bool? nullable3 = a.IsAllocated;
              bool? isAllocated = b.IsAllocated;
              if (nullable3.GetValueOrDefault() == isAllocated.GetValueOrDefault() & nullable3.HasValue == isAllocated.HasValue)
              {
                bool? nullable4 = a.IsMergeable;
                bool flag1 = false;
                if (!(nullable4.GetValueOrDefault() == flag1 & nullable4.HasValue))
                {
                  nullable4 = b.IsMergeable;
                  bool flag2 = false;
                  if (!(nullable4.GetValueOrDefault() == flag2 & nullable4.HasValue) && a.ShipmentNbr == b.ShipmentNbr)
                  {
                    nullable4 = a.Completed;
                    nullable3 = b.Completed;
                    if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                    {
                      nullable3 = a.POCreate;
                      nullable4 = b.POCreate;
                      if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                      {
                        nullable4 = a.POCompleted;
                        nullable3 = b.POCompleted;
                        if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue && a.PONbr == b.PONbr)
                        {
                          nullable1 = a.POLineNbr;
                          nullable2 = b.POLineNbr;
                          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && a.SOOrderType == b.SOOrderType && a.SOOrderNbr == b.SOOrderNbr)
                          {
                            nullable2 = a.SOLineNbr;
                            nullable1 = b.SOLineNbr;
                            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                            {
                              nullable1 = a.SOSplitLineNbr;
                              nullable2 = b.SOSplitLineNbr;
                              return nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue;
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    return false;
  }

  protected virtual void IssueAvailable(FSSODet line)
  {
    this.IssueAvailable(line, line.BaseOpenQty);
  }

  protected virtual void IssueAvailable(FSSODet line, Decimal? baseQty)
  {
    this.IssueAvailable(line, baseQty, false);
  }

  protected virtual void IssueAvailable(FSSODet line, Decimal? baseQty, bool isUncomplete)
  {
    this.LineCounters.Remove(line);
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult1 = this.ReadInventoryItem(line.InventoryID);
    foreach (PXResult<INSiteStatusByCostCenter> pxResult2 in PXSelectBase<INSiteStatusByCostCenter, PXViewOf<INSiteStatusByCostCenter>.BasedOn<SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INSiteStatusByCostCenter.subItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INSiteStatusByCostCenter.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INSiteStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<P.AsInt>>>.Order<By<BqlField<INLocation.pickPriority, IBqlShort>.Asc>>>.ReadOnly.Config>.Select((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base, new object[4]
    {
      (object) line.InventoryID,
      (object) line.SubItemID,
      (object) line.SiteID,
      (object) line.CostCenterID
    }))
    {
      INSiteStatusByCostCenter statusByCostCenter1 = PXResult<INSiteStatusByCostCenter>.op_Implicit(pxResult2);
      FSSODetSplit data = (FSSODetSplit) line;
      if (pxResult1 != null && PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult1).LotSerTrack == "S")
        data.UOM = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult1).BaseUnit;
      data.SplitLineNbr = new int?();
      data.IsAllocated = line.RequireAllocation;
      data.SiteID = line.SiteID;
      object obj1;
      ((PXCache) this.SplitCache).RaiseFieldDefaulting<FSSODetSplit.allocatedPlanType>((object) data, ref obj1);
      ((PXCache) this.SplitCache).SetValue<FSSODetSplit.allocatedPlanType>((object) data, obj1);
      object obj2;
      ((PXCache) this.SplitCache).RaiseFieldDefaulting<FSSODetSplit.backOrderPlanType>((object) data, ref obj2);
      ((PXCache) this.SplitCache).SetValue<FSSODetSplit.backOrderPlanType>((object) data, obj2);
      Sign signQtyHardAvail = ((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base).FindImplementation<IItemPlanHandler<FSSODetSplit>>().GetAvailabilitySigns<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>(data).SignQtyHardAvail;
      if (((Sign) ref signQtyHardAvail).IsMinus)
      {
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter2 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
        PXCache<INSiteStatusByCostCenter>.RestoreCopy((INSiteStatusByCostCenter) statusByCostCenter2, statusByCostCenter1);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter3 = PXCache<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>.Insert((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base, statusByCostCenter2);
        Decimal? nullable1 = statusByCostCenter1.QtyHardAvail;
        Decimal? nullable2 = statusByCostCenter3.QtyHardAvail;
        Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        nullable2 = nullable3;
        Decimal num = 0M;
        if (!(nullable2.GetValueOrDefault() <= num & nullable2.HasValue))
        {
          nullable2 = nullable3;
          nullable1 = baseQty;
          if (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
          {
            data.BaseQty = nullable3;
            data.Qty = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) this.LineCache, data.InventoryID, data.UOM, nullable3.Value, INPrecision.QUANTITY));
            this.SplitCache.Insert(data);
            nullable1 = baseQty;
            nullable2 = nullable3;
            baseQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          }
          else
          {
            data.BaseQty = baseQty;
            data.Qty = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) this.LineCache, data.InventoryID, data.UOM, baseQty.Value, INPrecision.QUANTITY));
            this.SplitCache.Insert(data);
            baseQty = new Decimal?(0M);
            break;
          }
        }
      }
    }
    Decimal? nullable4 = baseQty;
    Decimal num1 = 0M;
    if (!(nullable4.GetValueOrDefault() > num1 & nullable4.HasValue) || !line.InventoryID.HasValue || !line.SiteID.HasValue || !line.SubItemID.HasValue && (line.SubItemID.HasValue || line.IsStockItem.GetValueOrDefault() || !line.IsKit.GetValueOrDefault()) && !(line.SOLineType == "GN"))
      return;
    FSSODetSplit fssoDetSplit1 = (FSSODetSplit) line;
    if (pxResult1 != null && PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult1).LotSerTrack == "S")
      fssoDetSplit1.UOM = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult1).BaseUnit;
    fssoDetSplit1.SplitLineNbr = new int?();
    fssoDetSplit1.IsAllocated = new bool?(false);
    fssoDetSplit1.BaseQty = baseQty;
    FSSODetSplit fssoDetSplit2 = fssoDetSplit1;
    PXCache<FSSODet> lineCache = this.LineCache;
    int? inventoryId = fssoDetSplit1.InventoryID;
    string uom = fssoDetSplit1.UOM;
    nullable4 = fssoDetSplit1.BaseQty;
    Decimal num2 = nullable4.Value;
    Decimal? nullable5 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) lineCache, inventoryId, uom, num2, INPrecision.QUANTITY));
    fssoDetSplit2.Qty = nullable5;
    if (isUncomplete)
    {
      fssoDetSplit1.POCreate = new bool?(false);
      fssoDetSplit1.POSource = (string) null;
    }
    this.SplitCache.Insert(PXCache<FSSODetSplit>.CreateCopy(fssoDetSplit1));
  }

  protected virtual bool IsSplitRequired(FSSODet line, bool externalCall)
  {
    return this.IsSplitRequired(line, externalCall, out PX.Objects.IN.InventoryItem _);
  }

  protected virtual bool IsSplitRequired(FSSODet line, bool externalCall, out PX.Objects.IN.InventoryItem item)
  {
    if (line == null)
    {
      item = (PX.Objects.IN.InventoryItem) null;
      return false;
    }
    bool flag1 = false;
    item = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<FSSODet.inventoryID>((PXCache) this.LineCache, (object) line);
    bool? nullable;
    if (this.Base1.IsLocationEnabled && item != null)
    {
      nullable = item.StkItem;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        nullable = item.KitItem;
        bool flag3 = false;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
        {
          nullable = item.NonStockShip;
          bool flag4 = false;
          if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
            flag1 = true;
        }
      }
    }
    if (item != null)
    {
      nullable = item.StkItem;
      bool flag5 = false;
      if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue)
      {
        nullable = item.KitItem;
        if (nullable.GetValueOrDefault() && line.Behavior != "CM" && line.Behavior != "IN")
          flag1 = true;
      }
    }
    if (flag1)
      return false;
    if (this.Base1.IsLocationEnabled)
      return true;
    if (this.Base1.IsLotSerialRequired & externalCall)
    {
      nullable = line.POCreate;
      if (!nullable.GetValueOrDefault())
        return this.IsLotSerialItem((ILSMaster) line);
    }
    return false;
  }

  protected virtual bool ShouldThrowExceptions()
  {
    return ((PXGraphExtension<ServiceOrderEntry>) this).Base.GraphAppointmentEntryCaller != null;
  }

  protected virtual bool IsLotSerialItem(ILSMaster line)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(line.InventoryID);
    if (pxResult == null)
      return false;
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType = line.TranType;
    short? invtMult = line.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    return INLotSerialNbrAttribute.IsTrack(lotSerClass, tranType, invMult);
  }

  protected virtual bool NegativeInventoryError(FSSODetSplit split)
  {
    split.IsAllocated = new bool?(false);
    split.LotSerialNbr = (string) null;
    ((PXCache) this.SplitCache).RaiseExceptionHandling<FSSODetSplit.lotSerialNbr>((object) split, (object) null, (Exception) new PXSetPropertyException("Inventory quantity will go negative."));
    return split.IsAllocated.GetValueOrDefault();
  }

  private void RefreshViewOf(PXCache cache)
  {
    foreach (KeyValuePair<string, PXView> view in (Dictionary<string, PXView>) ((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base).Views)
    {
      PXView pxView = view.Value;
      if (!pxView.IsReadOnly && pxView.GetItemType() == cache.GetItemType())
        pxView.RequestRefresh();
    }
  }

  public delegate void UpdateParentDelegate(
    FSSODet line,
    FSSODetSplit newSplit,
    FSSODetSplit oldSplit,
    out Decimal baseQty);
}
