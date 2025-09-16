// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSODetSplitPlan
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class FSSODetSplitPlan : ItemPlan<ServiceOrderEntry, FSServiceOrder, FSSODetSplit>
{
  private bool _initPlan;
  private bool _initVendor;
  private bool _resetSupplyPlanID;

  public override void Initialize()
  {
    base.Initialize();
    PXGraph.FieldDefaultingEvents fieldDefaulting = ((PXGraph) this.Base).FieldDefaulting;
    FSSODetSplitPlan fssoDetSplitPlan1 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) fssoDetSplitPlan1, __vmethodptr(fssoDetSplitPlan1, NegAvailQtyFieldDefaulting));
    fieldDefaulting.AddHandler<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter.negAvailQty>(pxFieldDefaulting);
    PXGraph.RowUpdatedEvents rowUpdated1 = ((PXGraph) this.Base).RowUpdated;
    FSSODetSplitPlan fssoDetSplitPlan2 = this;
    // ISSUE: virtual method pointer
    PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSServiceOrder>>.EventDelegate eventDelegate1 = new PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSServiceOrder>>.EventDelegate((object) fssoDetSplitPlan2, __vmethodptr(fssoDetSplitPlan2, _));
    rowUpdated1.RemoveHandler<FSServiceOrder>(eventDelegate1);
    PXGraph.RowUpdatedEvents rowUpdated2 = ((PXGraph) this.Base).RowUpdated;
    FSSODetSplitPlan fssoDetSplitPlan3 = this;
    // ISSUE: virtual method pointer
    PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSServiceOrder>>.EventDelegate eventDelegate2 = new PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSServiceOrder>>.EventDelegate((object) fssoDetSplitPlan3, __vmethodptr(fssoDetSplitPlan3, _));
    rowUpdated2.AddHandler<FSServiceOrder>(eventDelegate2);
  }

  protected virtual void NegAvailQtyFieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select(sender.Graph, Array.Empty<object>()));
    if (((CancelEventArgs) e).Cancel || soOrderType == null || !soOrderType.RequireAllocation.GetValueOrDefault())
      return;
    e.NewValue = (object) false;
    ((CancelEventArgs) e).Cancel = true;
  }

  public override void _(PX.Data.Events.RowUpdated<FSServiceOrder> e)
  {
    base._(e);
    WebDialogResult webDialogResult = (WebDialogResult) 6;
    bool flag1 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSServiceOrder>>) e).Cache.ObjectsEqual<FSServiceOrder.orderDate>((object) e.Row, (object) e.OldRow) && webDialogResult == 6;
    bool flag2 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSServiceOrder>>) e).Cache.ObjectsEqual<FSServiceOrder.orderDate>((object) e.Row, (object) e.OldRow) && webDialogResult == 6;
    bool flag3 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSServiceOrder>>) e).Cache.ObjectsEqual<FSServiceOrder.billCustomerID>((object) e.Row, (object) e.OldRow);
    if (!(flag3 | flag1 | flag2) && ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSServiceOrder>>) e).Cache.ObjectsEqual<FSServiceOrder.hold, FSServiceOrder.status>((object) e.Row, (object) e.OldRow))
      return;
    bool? nullable1 = e.Row.Canceled;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    PXCache pxCache1 = (PXCache) GraphHelper.Caches<FSSODet>((PXGraph) this.Base);
    PXCache pxCache2 = (PXCache) GraphHelper.Caches<FSSODetSplit>((PXGraph) this.Base);
    PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
    FSSrvOrdType srvOrdType = PXResultset<FSSrvOrdType>.op_Implicit(PXSetup<FSSrvOrdType>.Select((PXGraph) this.Base, Array.Empty<object>()));
    string billingMode = ServiceOrderEntry.GetBillingMode((PXGraph) this.Base, PXResultset<FSBillingCycle>.op_Implicit(PXSelectBase<FSBillingCycle, PXSelect<FSBillingCycle>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())), srvOrdType, e.Row);
    Dictionary<long?, FSSODetSplit> dictionary = new Dictionary<long?, FSSODetSplit>();
    foreach (PXResult<FSSODetSplit> pxResult in PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Current<FSServiceOrder.refNbr>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new FSServiceOrder[1]
    {
      e.Row
    }, Array.Empty<object>()))
    {
      FSSODetSplit split = PXResult<FSSODetSplit>.op_Implicit(pxResult);
      FSSODet fssoDet1 = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.srvOrdType, Equal<Required<FSSODet.srvOrdType>>, And<FSSODet.refNbr, Equal<Required<FSSODet.refNbr>>, And<FSSODet.lineNbr, Equal<Required<FSSODet.refNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
      {
        (object) split.SrvOrdType,
        (object) split.RefNbr,
        (object) split.LineNbr
      }));
      long? nullable2;
      if (valueOrDefault)
      {
        EnumerableExtensions.ForEach<INItemPlan>(GraphHelper.RowCast<INItemPlan>(((PXCache) this.PlanCache).Inserted).Where<INItemPlan>((Func<INItemPlan, bool>) (_ =>
        {
          long? planId1 = _.PlanID;
          long? planId2 = split.PlanID;
          return planId1.GetValueOrDefault() == planId2.GetValueOrDefault() & planId1.HasValue == planId2.HasValue;
        })), (Action<INItemPlan>) (_ => this.PlanCache.Delete(_)));
        FSSODetSplit fssoDetSplit = split;
        nullable2 = new long?();
        long? nullable3 = nullable2;
        fssoDetSplit.PlanID = nullable3;
        split.Completed = new bool?(true);
        GraphHelper.MarkUpdated(pxCache2, (object) split);
      }
      else
      {
        nullable1 = e.OldRow.Canceled;
        if (nullable1.GetValueOrDefault())
        {
          if (string.IsNullOrEmpty(split.ShipmentNbr))
          {
            nullable1 = split.POCompleted;
            bool flag4 = false;
            if (nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue)
              split.Completed = new bool?(false);
          }
          INItemPlan inItemPlan1 = this.DefaultValuesInt(new INItemPlan(), split);
          if (inItemPlan1 != null)
          {
            INItemPlan inItemPlan2 = this.PlanCache.Insert(inItemPlan1);
            split.PlanID = inItemPlan2.PlanID;
          }
          GraphHelper.MarkUpdated(pxCache2, (object) split);
        }
        if (flag1)
        {
          split.ShipDate = e.Row.OrderDate;
          GraphHelper.MarkUpdated(pxCache2, (object) split);
        }
        nullable2 = split.PlanID;
        if (nullable2.HasValue)
          dictionary[split.PlanID] = split;
        nullable1 = e.OldRow.Closed;
        if (nullable1.GetValueOrDefault() && string.IsNullOrEmpty(split.ShipmentNbr))
        {
          nullable1 = split.POCompleted;
          bool flag5 = false;
          if (nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue)
          {
            nullable1 = split.Completed;
            if (nullable1.GetValueOrDefault() && split.LastModifiedByScreenID == "FS300100" && billingMode == "AP")
            {
              FSSODet fssoDet2 = fssoDet1;
              Decimal? nullable4 = fssoDet2.BaseShippedQty;
              Decimal? nullable5 = split.BaseShippedQty;
              fssoDet2.BaseShippedQty = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
              FSSODet fssoDet3 = fssoDet1;
              nullable5 = fssoDet3.ShippedQty;
              nullable4 = split.ShippedQty;
              fssoDet3.ShippedQty = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
              FSSODet fssoDet4 = fssoDet1;
              nullable4 = fssoDet1.OrderQty;
              nullable5 = fssoDet1.ShippedQty;
              Decimal? nullable6 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
              fssoDet4.OpenQty = nullable6;
              FSSODet fssoDet5 = fssoDet1;
              nullable5 = fssoDet1.BaseOrderQty;
              nullable4 = fssoDet1.BaseShippedQty;
              Decimal? nullable7 = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
              fssoDet5.BaseOpenQty = nullable7;
              fssoDet1.ClosedQty = fssoDet1.ShippedQty;
              fssoDet1.BaseClosedQty = fssoDet1.BaseShippedQty;
              GraphHelper.MarkUpdated(pxCache1, (object) fssoDet1);
              split.Completed = new bool?(false);
              split.ShippedQty = new Decimal?(0M);
              INItemPlan inItemPlan3 = this.DefaultValuesInt(new INItemPlan(), split);
              if (inItemPlan3 != null)
              {
                INItemPlan inItemPlan4 = this.PlanCache.Insert(inItemPlan3);
                split.PlanID = inItemPlan4.PlanID;
              }
              GraphHelper.MarkUpdated(pxCache2, (object) split);
            }
          }
        }
      }
    }
    foreach (PXResult<FSSODet> pxResult in PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSSODet.refNbr, Equal<Current<FSServiceOrder.refNbr>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new FSServiceOrder[1]
    {
      e.Row
    }, Array.Empty<object>()))
    {
      FSSODet row = PXResult<FSSODet>.op_Implicit(pxResult);
      if (valueOrDefault)
      {
        PXCache<FSSODet>.CreateCopy(row);
        row.OpenQty = new Decimal?(0M);
        pxCache1.RaiseFieldUpdated<FSSODet.openQty>((object) row, (object) 0M);
        row.Completed = new bool?(true);
        this.ResetAvailabilityCounters(row);
        GraphHelper.MarkUpdated(pxCache1, (object) row);
      }
      else
      {
        if (e.OldRow.Canceled.GetValueOrDefault())
        {
          PXCache<FSSODet>.CreateCopy(row);
          row.OpenQty = row.OrderQty;
          object openQty = (object) row.OpenQty;
          pxCache1.RaiseFieldVerifying<FSSODet.openQty>((object) row, ref openQty);
          pxCache1.RaiseFieldUpdated<FSSODet.openQty>((object) row, openQty);
          row.Completed = new bool?(false);
          GraphHelper.MarkUpdated(pxCache1, (object) row);
        }
        if (flag1)
        {
          row.ShipDate = e.Row.OrderDate;
          GraphHelper.MarkUpdated(pxCache1, (object) row);
        }
        if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSServiceOrder>>) e).Cache.ObjectsEqual<FSServiceOrder.hold>((object) e.Row, (object) e.OldRow))
          this.ResetAvailabilityCounters(row);
      }
    }
    if (valueOrDefault)
      PXFormulaAttribute.CalcAggregate<FSSODet.openQty>(pxCache1, (object) e.Row);
    foreach (INItemPlan plan in ((PXSelectBase) new PXSelect<INItemPlan, Where<INItemPlan.refNoteID, Equal<Current<FSServiceOrder.noteID>>>>((PXGraph) this.Base)).View.SelectMultiBound((object[]) new FSServiceOrder[1]
    {
      e.Row
    }, Array.Empty<object>()))
    {
      if (valueOrDefault)
      {
        this.PlanCache.Delete(plan);
      }
      else
      {
        INItemPlan copy = PXCache<INItemPlan>.CreateCopy(plan);
        if (flag1)
          plan.PlanDate = e.Row.OrderDate;
        if (flag3)
          plan.BAccountID = e.Row.CustomerID;
        plan.Hold = new bool?(this.IsOrderOnHold(e.Row));
        FSSODetSplit split;
        if (dictionary.TryGetValue(plan.PlanID, out split))
        {
          plan.PlanType = this.CalcPlanType(plan, e.Row, split);
          if (!string.Equals(copy.PlanType, plan.PlanType))
            this.PlanCache.RaiseRowUpdated(plan, copy);
        }
        if (EnumerableExtensions.IsIn<PXEntryStatus>(this.PlanCache.GetStatus(plan), (PXEntryStatus) 0, (PXEntryStatus) 5))
          ((PXCache) this.PlanCache).SetStatus((object) plan, (PXEntryStatus) 1);
      }
    }
  }

  protected virtual bool IsOrderOnHold(FSServiceOrder order)
  {
    return order != null && order.Hold.GetValueOrDefault();
  }

  public virtual void ResetAvailabilityCounters(FSSODet row)
  {
    row.LineQtyAvail = new Decimal?();
    row.LineQtyHardAvail = new Decimal?();
  }

  public override void _(PX.Data.Events.RowUpdated<FSSODetSplit> e)
  {
    bool flag = this.IsLineLinked(e.Row);
    this._initPlan = this.InitPlanRequired(e.Row, e.OldRow) && !flag;
    FSSODet fssoDet = (FSSODet) PXParentAttribute.SelectParent(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSSODetSplit>>) e).Cache, (object) e.Row, typeof (FSSODet));
    this._initVendor = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSSODetSplit>>) e).Cache.ObjectsEqual<FSSODetSplit.siteID, FSSODetSplit.subItemID, FSSODetSplit.vendorID, FSSODetSplit.pOCreate>((object) e.Row, (object) e.OldRow) && !flag;
    this._initVendor = this._initVendor || fssoDet.POVendorLocationID.HasValue;
    this._resetSupplyPlanID = !flag;
    try
    {
      base._(e);
    }
    finally
    {
      this._initPlan = false;
      this._resetSupplyPlanID = false;
    }
  }

  protected virtual bool InitPlanRequired(FSSODetSplit row, FSSODetSplit oldRow)
  {
    return !((PXCache) GraphHelper.Caches<FSSODetSplit>((PXGraph) this.Base)).ObjectsEqual<FSSODetSplit.isAllocated, FSSODetSplit.siteID, FSSODetSplit.pOCreate, FSSODetSplit.pOSource, FSSODetSplit.operation>((object) row, (object) oldRow);
  }

  protected virtual string CalcPlanType(
    INItemPlan plan,
    FSServiceOrder order,
    FSSODetSplit split,
    bool? backOrdered = null)
  {
    if (split.POCreate.GetValueOrDefault())
      return "F6";
    PX.Objects.SO.SOOrderType ordertype = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
    bool? nullable = split.IsAllocated;
    bool flag1 = nullable.GetValueOrDefault() || INPlanConstants.IsAllocated(plan.PlanType) || INPlanConstants.IsFixed(plan.PlanType);
    int num;
    if (this.IsOrderOnHold(order))
    {
      nullable = ordertype.RequireAllocation;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    bool isOrderOnHold = num != 0;
    string str = this.CalcPlanType(plan, split, ordertype, isOrderOnHold);
    bool flag2 = str == "F0";
    return !this._initPlan && !flag2 && !flag1 && (backOrdered.GetValueOrDefault() || !backOrdered.HasValue && plan.PlanType == "68") ? "68" : str;
  }

  protected virtual string CalcPlanType(
    INItemPlan plan,
    FSSODetSplit split,
    PX.Objects.SO.SOOrderType ordertype,
    bool isOrderOnHold)
  {
    if (ordertype == null || ordertype.RequireShipping.GetValueOrDefault())
    {
      if (split.IsAllocated.GetValueOrDefault())
        return split.AllocatedPlanType;
      if (isOrderOnHold)
        return "F0";
      return split.RequireAllocation.GetValueOrDefault() && split.IsStockItem.GetValueOrDefault() ? split.BackOrderPlanType : split.PlanType;
    }
    return isOrderOnHold && split.IsStockItem.GetValueOrDefault() ? "F0" : split.PlanType;
  }

  protected virtual bool IsLineLinked(FSSODetSplit soLineSplit)
  {
    if (soLineSplit == null)
      return false;
    if (soLineSplit.PONbr != null || soLineSplit.POReceiptNbr != null)
      return true;
    return soLineSplit.SOOrderNbr != null && soLineSplit.IsAllocated.GetValueOrDefault();
  }

  public override INItemPlan DefaultValues(INItemPlan planRow, FSSODetSplit splitRow)
  {
    if (!splitRow.Completed.GetValueOrDefault() && !splitRow.POCompleted.GetValueOrDefault() && !(splitRow.LineType == "MI"))
    {
      bool? nullable1;
      if (splitRow.LineType == "GN")
      {
        nullable1 = splitRow.RequireShipping;
        bool flag = false;
        if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
          goto label_3;
      }
      FSSODet fssoDet = (FSSODet) PXParentAttribute.SelectParent((PXCache) this.ItemPlanSourceCache, (object) splitRow, typeof (FSSODet));
      FSServiceOrder order1 = (FSServiceOrder) PXParentAttribute.SelectParent((PXCache) this.ItemPlanSourceCache, (object) splitRow, typeof (FSServiceOrder));
      int? nullable2;
      if (string.IsNullOrEmpty(planRow.PlanType) || this._initPlan)
      {
        INItemPlan inItemPlan1 = planRow;
        INItemPlan plan = planRow;
        FSServiceOrder order2 = order1;
        FSSODetSplit split = splitRow;
        nullable1 = new bool?();
        bool? backOrdered = nullable1;
        string str1 = this.CalcPlanType(plan, order2, split, backOrdered);
        inItemPlan1.PlanType = str1;
        nullable1 = splitRow.POCreate;
        if (nullable1.GetValueOrDefault())
        {
          planRow.FixedSource = "P";
          planRow.SourceSiteID = !(splitRow.POType != "BL") || !(splitRow.POType != "DP") || !(splitRow.POSource == "O") ? splitRow.SiteID : splitRow.SiteID;
        }
        else
        {
          planRow.Reverse = new bool?(splitRow.Operation == "R");
          INItemPlan inItemPlan2 = planRow;
          int? siteId = splitRow.SiteID;
          nullable2 = splitRow.ToSiteID;
          string str2 = !(siteId.GetValueOrDefault() == nullable2.GetValueOrDefault() & siteId.HasValue == nullable2.HasValue) ? "T" : "N";
          inItemPlan2.FixedSource = str2;
          planRow.SourceSiteID = splitRow.SiteID;
        }
      }
      if (this._resetSupplyPlanID)
        planRow.SupplyPlanID = new long?();
      planRow.VendorID = splitRow.VendorID;
      if (!this._initVendor)
      {
        nullable1 = splitRow.POCreate;
        if (nullable1.GetValueOrDefault())
        {
          nullable2 = planRow.VendorID;
          if (nullable2.HasValue)
          {
            nullable2 = planRow.VendorLocationID;
            if (nullable2.HasValue)
              goto label_19;
          }
          else
            goto label_19;
        }
        else
          goto label_19;
      }
      INItemPlan inItemPlan3 = planRow;
      int? nullable3;
      if (fssoDet == null)
      {
        nullable2 = new int?();
        nullable3 = nullable2;
      }
      else
        nullable3 = fssoDet.POVendorLocationID;
      inItemPlan3.VendorLocationID = nullable3;
      nullable2 = planRow.VendorLocationID;
      if (!nullable2.HasValue)
        planRow.VendorLocationID = POItemCostManager.FetchLocation((PXGraph) this.Base, splitRow.VendorID, splitRow.InventoryID, splitRow.SubItemID, splitRow.SiteID);
label_19:
      INItemPlan inItemPlan4 = planRow;
      int? nullable4;
      if (fssoDet != null)
      {
        nullable4 = fssoDet.BillCustomerID;
      }
      else
      {
        nullable2 = new int?();
        nullable4 = nullable2;
      }
      inItemPlan4.BAccountID = nullable4;
      planRow.InventoryID = splitRow.InventoryID;
      planRow.SubItemID = splitRow.SubItemID;
      planRow.SiteID = splitRow.SiteID;
      planRow.LocationID = splitRow.LocationID;
      planRow.CostCenterID = splitRow.CostCenterID;
      planRow.LotSerialNbr = splitRow.LotSerialNbr;
      if (!string.IsNullOrEmpty(splitRow.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(splitRow.AssignedNbr, splitRow.LotSerialNbr))
        planRow.LotSerialNbr = (string) null;
      planRow.PlanDate = splitRow.ShipDate;
      planRow.UOM = fssoDet?.UOM;
      INItemPlan inItemPlan5 = planRow;
      nullable1 = splitRow.POCreate;
      Decimal? nullable5;
      if (!nullable1.GetValueOrDefault())
      {
        nullable5 = splitRow.BaseQty;
      }
      else
      {
        Decimal? baseUnreceivedQty = splitRow.BaseUnreceivedQty;
        Decimal? baseShippedQty = splitRow.BaseShippedQty;
        nullable5 = baseUnreceivedQty.HasValue & baseShippedQty.HasValue ? new Decimal?(baseUnreceivedQty.GetValueOrDefault() - baseShippedQty.GetValueOrDefault()) : new Decimal?();
      }
      inItemPlan5.PlanQty = nullable5;
      planRow.RefNoteID = order1.NoteID;
      planRow.Hold = new bool?(this.IsOrderOnHold(order1));
      return string.IsNullOrEmpty(planRow.PlanType) ? (INItemPlan) null : planRow;
    }
label_3:
    return (INItemPlan) null;
  }

  public INItemPlan DefaultValues(FSSODetSplit split)
  {
    return this.DefaultValuesInt(new INItemPlan(), split);
  }
}
