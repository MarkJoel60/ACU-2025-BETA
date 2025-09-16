// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SalesOrderOnShipmentConfirmationExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using PX.Objects.SO.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class SalesOrderOnShipmentConfirmationExtension : 
  BaseOnShipmentConfirmationExtension<SOOrderEntry, PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>
{
  /// Extends <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.ConfirmShipmentExtension.ValidateShipment(PX.Objects.SO.SOShipment)" />
  /// . Checks if any related SOOrderShipment exists.
  [PXOverride]
  public void ValidateShipment(PX.Objects.SO.SOShipment shipment, Action<PX.Objects.SO.SOShipment> base_ValidateShipment)
  {
    base_ValidateShipment(shipment);
    if (PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).FindImplementation<SOOrderExtension>().OrderList).SelectWindowed(0, 1, Array.Empty<object>())) == null)
      throw new PXException("Unable to confirm empty shipment {0}.", new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.ShipmentNbr
      });
  }

  protected override PXResultset<PX.Objects.SO.SOOrderShipment> GetOrderShipments()
  {
    return ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).FindImplementation<SOOrderExtension>().OrderList).Select(Array.Empty<object>());
  }

  protected override bool ShouldBeProcessed(ConfirmShipmentArgs args)
  {
    return args.OrigDocumentGraph is SOOrderEntry;
  }

  protected override string GetLineShippingRule(PX.Objects.SO.SOLine line, SOShipLine shipline)
  {
    return line.ShipComplete;
  }

  protected override void ValidateLineShippingRuleAndQty(
    PX.Objects.SO.SOOrderShipment order,
    PX.Objects.SO.SOLine line,
    SOShipLine shipline)
  {
    base.ValidateLineShippingRuleAndQty(order, line, shipline);
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, line.InventoryID);
    if (shipline.ShipmentNbr == null && order.ShipComplete == "C" && line.POSource != "D")
      throw new PXException("The shipment cannot be confirmed because it does not include the {2} item from the {0} {1} sales order that has the Ship Complete order-level shipping rule on the Shipping tab of the Sales Orders (SO301000) form.", new object[3]
      {
        (object) line.OrderType,
        (object) line.OrderNbr,
        (object) inventoryItem.InventoryCD
      });
    this.ValidateLineType(line, inventoryItem, "The shipment cannot be confirmed because the settings of the {0} line in the {1} sales order have been changed. Delete the line from the sales order and add it again to update the line details.");
  }

  protected override bool IsFullyShipped(PX.Objects.SO.SOLine line)
  {
    short? lineSign1 = line.LineSign;
    Decimal? nullable1 = lineSign1.HasValue ? new Decimal?((Decimal) lineSign1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable2 = line.BaseShippedQty;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
    short? lineSign2 = line.LineSign;
    Decimal? nullable4 = lineSign2.HasValue ? new Decimal?((Decimal) lineSign2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5 = line.BaseOrderQty;
    nullable2 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
    Decimal? completeQtyMin = line.CompleteQtyMin;
    Decimal? nullable6;
    if (!(nullable2.HasValue & completeQtyMin.HasValue))
    {
      nullable5 = new Decimal?();
      nullable6 = nullable5;
    }
    else
      nullable6 = new Decimal?(nullable2.GetValueOrDefault() * completeQtyMin.GetValueOrDefault() / 100M);
    Decimal? nullable7 = nullable6;
    return nullable3.GetValueOrDefault() >= nullable7.GetValueOrDefault() & nullable3.HasValue & nullable7.HasValue;
  }

  protected override void RecreateOrigDocumentDetails(
    SOOrderEntry origDocumentGraph,
    PX.Objects.SO.SOOrderShipment order,
    HashSet<PX.Objects.SO.SOLine> processedLines,
    Dictionary<long?, List<INItemPlan>> demandItemPlans,
    PX.Objects.SO.SOLine line,
    SOShipLine shipline)
  {
    base.RecreateOrigDocumentDetails(origDocumentGraph, order, processedLines, demandItemPlans, line, shipline);
    this.CreateNewSOLines(origDocumentGraph, line, shipline);
  }

  protected override PX.Objects.SO.SOLineSplit ShipOrReplanOrigDocumentSplit(
    SOOrderEntry origDocumentGraph,
    Dictionary<long?, List<INItemPlan>> demandItemPlans,
    PX.Objects.SO.SOLine line,
    SOShipLine shipLine,
    PXResult<PX.Objects.SO.SOLineSplit, INItemPlan, SOShipLine, INSiteStatusByCostCenter> schedres)
  {
    PX.Objects.SO.SOLineSplit soLineSplit1 = base.ShipOrReplanOrigDocumentSplit(origDocumentGraph, demandItemPlans, line, shipLine, schedres);
    string lineShippingRule = this.GetLineShippingRule(line, shipLine);
    if (soLineSplit1 != null)
    {
      bool? completed = soLineSplit1.Completed;
      bool flag = false;
      if (completed.GetValueOrDefault() == flag & completed.HasValue && soLineSplit1.PlanID.HasValue)
      {
        bool? isAllocated;
        if (!((PXSelectBase<INSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.insetup).Current.ReplanBackOrders.GetValueOrDefault())
        {
          isAllocated = soLineSplit1.IsAllocated;
          if (!isAllocated.GetValueOrDefault())
            goto label_11;
        }
        Decimal valueOrDefault1 = PXResult<PX.Objects.SO.SOLineSplit, INItemPlan, SOShipLine, INSiteStatusByCostCenter>.op_Implicit(schedres).QtyHardAvail.GetValueOrDefault();
        isAllocated = soLineSplit1.IsAllocated;
        if (!isAllocated.GetValueOrDefault())
        {
          if (valueOrDefault1 > 0M)
          {
            if (!(lineShippingRule != "C"))
            {
              Decimal num = valueOrDefault1;
              Decimal? baseQty = soLineSplit1.BaseQty;
              Decimal valueOrDefault2 = baseQty.GetValueOrDefault();
              if (!(num >= valueOrDefault2 & baseQty.HasValue))
                goto label_11;
            }
          }
          else
            goto label_11;
        }
        INItemPlan plan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Current<PX.Objects.SO.SOLineSplit.planID>>>>.Config>.SelectSingleBound((PXGraph) origDocumentGraph, (object[]) new PX.Objects.SO.SOLineSplit[1]
        {
          soLineSplit1
        }, Array.Empty<object>()));
        if (plan != null)
        {
          PX.Objects.SO.SOOrderType ordertype = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) origDocumentGraph, Array.Empty<object>()));
          if (((PXGraph) origDocumentGraph).FindImplementation<SOLineSplitPlan>().IsPlanRegular(ordertype, plan))
          {
            PX.Objects.SO.SOLineSplit soLineSplit2 = soLineSplit1;
            isAllocated = soLineSplit1.IsAllocated;
            string str = isAllocated.GetValueOrDefault() ? soLineSplit1.AllocatedPlanType : soLineSplit1.BookedPlanType;
            soLineSplit2.PlanType = str;
            INItemPlan copy = PXCache<INItemPlan>.CreateCopy(plan);
            copy.IsSkippedWhenBackOrdered = new bool?(true);
            copy.PlanType = soLineSplit1.PlanType;
            GraphHelper.Caches<INItemPlan>((PXGraph) origDocumentGraph).Update(copy);
          }
        }
      }
    }
label_11:
    return soLineSplit1;
  }

  protected override PX.Objects.SO.SOLineSplit CompleteOrigDocumentSplit(
    SOOrderEntry origDocumentGraph,
    PX.Objects.SO.SOLine line,
    SOShipLine shline,
    PX.Objects.SO.SOLineSplit origDocumentSplit,
    INItemPlan origDocumentSplitPlan)
  {
    string lineShippingRule = this.GetLineShippingRule(line, shline);
    origDocumentSplit.Completed = new bool?(true);
    if (shline.ShipmentNbr == null)
    {
      switch (lineShippingRule)
      {
        case "C":
          break;
        case "L":
          if (!(origDocumentSplit.FixedSource == "N"))
            goto label_4;
          break;
        default:
          goto label_4;
      }
    }
    origDocumentSplit.ShipmentNbr = shline.ShipmentNbr;
label_4:
    origDocumentSplit.ShipComplete = line.ShipComplete;
    if (lineShippingRule == "L" && origDocumentSplit.FixedSource == "N")
    {
      Decimal? shippedQty = origDocumentSplit.ShippedQty;
      Decimal num = 0M;
      if (shippedQty.GetValueOrDefault() == num & shippedQty.HasValue)
      {
        INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Current<INItemPlan.planID>>, And<INItemPlan.planType, Equal<INPlanConstants.plan94>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[1]
        {
          (object) origDocumentSplitPlan
        }, Array.Empty<object>()));
        if (inItemPlan != null)
          GraphHelper.Caches<INItemPlan>((PXGraph) origDocumentGraph).Delete(inItemPlan);
      }
    }
    return base.CompleteOrigDocumentSplit(origDocumentGraph, line, shline, origDocumentSplit, origDocumentSplitPlan);
  }

  protected override PX.Objects.SO.SOLineSplit InsertBackorderedSplit(
    SOOrderEntry origDocumentGraph,
    PX.Objects.SO.SOLineSplit split)
  {
    ((PXSelectBase<PX.Objects.SO.SOLine>) origDocumentGraph.Transactions).Current = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) origDocumentGraph.Transactions).Search<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>((object) split.OrderType, (object) split.OrderNbr, (object) split.LineNbr, Array.Empty<object>()));
    return ((PXGraph) origDocumentGraph).GetExtension<SOOrderLineSplittingAllocatedExtension>().InsertShipmentRemainder(split);
  }

  protected override PX.Objects.SO.SOLineSplit FillBackorderedSplit(
    SOOrderEntry origDocumentGraph,
    PX.Objects.SO.SOLineSplit origDocumentSplit,
    PX.Objects.SO.SOLineSplit split)
  {
    split.PlanID = new long?();
    split.PlanType = split.BackOrderPlanType;
    split.ParentSplitLineNbr = split.SplitLineNbr;
    split.SplitLineNbr = new int?();
    split.IsAllocated = origDocumentSplit.IsAllocated;
    split.Completed = new bool?(false);
    split.ShipmentNbr = (string) null;
    split.LotSerialNbr = origDocumentSplit.LotSerialNbr;
    split.LotSerClassID = origDocumentSplit.LotSerClassID;
    split.ClearPOFlags();
    split.ClearPOReferences();
    split.ClearSOReferences();
    split.VendorID = new int?();
    split.RefNoteID = new Guid?();
    split.BaseReceivedQty = new Decimal?(0M);
    split.ReceivedQty = new Decimal?(0M);
    split.BaseShippedQty = new Decimal?(0M);
    split.ShippedQty = new Decimal?(0M);
    PX.Objects.SO.SOLineSplit soLineSplit = split;
    Decimal? baseQty = origDocumentSplit.BaseQty;
    Decimal? baseShippedQty = origDocumentSplit.BaseShippedQty;
    Decimal? nullable = baseQty.HasValue & baseShippedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - baseShippedQty.GetValueOrDefault()) : new Decimal?();
    soLineSplit.BaseQty = nullable;
    split.Qty = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) origDocumentGraph.splits).Cache, split.InventoryID, split.UOM, split.BaseQty.Value, INPrecision.QUANTITY));
    return split;
  }

  protected override void ConfirmOrigDocumentLine(
    SOOrderEntry origDocumentGraph,
    PX.Objects.SO.SOLine line,
    SOShipLine shipline,
    ref bool backorderExists)
  {
    string lineShippingRule = this.GetLineShippingRule(line, shipline);
    origDocumentGraph.ConfirmSingleLine(line, shipline, lineShippingRule, ref backorderExists);
  }

  protected override bool ConfirmShipLine(PX.Objects.SO.SOOrderShipment order, PX.Objects.SO.SOLine line, SOShipLine shipline)
  {
    int num = base.ConfirmShipLine(order, line, shipline) ? 1 : 0;
    if (num == 0)
      return num != 0;
    order.CreateINDoc = new bool?(true);
    return num != 0;
  }

  protected override void SaveOrigDocumentGraph(SOOrderEntry origDocumentGraph)
  {
    ((SelectedEntityEvent<PX.Objects.SO.SOOrder>) PXEntityEventBase<PX.Objects.SO.SOOrder>.Container<PX.Objects.SO.SOOrder.Events>.Select((Expression<Func<PX.Objects.SO.SOOrder.Events, PXEntityEvent<PX.Objects.SO.SOOrder.Events>>>) (e => e.GotShipmentConfirmed))).FireOn((PXGraph) origDocumentGraph, ((PXSelectBase<PX.Objects.SO.SOOrder>) origDocumentGraph.Document).Current);
    ((PXAction) origDocumentGraph.Save).Press();
  }

  protected override (BqlCommand, object[]) GetOrigDocumentLinesWithShipLinesCommandAndCurrents(
    SOOrderEntry origDocumentGraph,
    PX.Objects.SO.SOShipment shipment,
    PX.Objects.SO.SOOrderShipment order)
  {
    return ((BqlCommand) new Select2<PX.Objects.SO.SOLine, LeftJoin<SOShipLine, On<SOShipLine.origOrderType, Equal<PX.Objects.SO.SOLine.orderType>, And<SOShipLine.origOrderNbr, Equal<PX.Objects.SO.SOLine.orderNbr>, And<SOShipLine.origLineNbr, Equal<PX.Objects.SO.SOLine.lineNbr>, And<SOShipLine.shipmentType, Equal<Current<PX.Objects.SO.SOOrderShipment.shipmentType>>, And<SOShipLine.shipmentNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.shipmentNbr>>>>>>>>, Where<PX.Objects.SO.SOLine.orderType, Equal<Current<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.orderNbr>>, And<PX.Objects.SO.SOLine.operation, Equal<Current<PX.Objects.SO.SOOrderShipment.operation>>, And2<Where<PX.Objects.SO.SOLine.siteID, Equal<Current<PX.Objects.SO.SOOrderShipment.siteID>>, Or<SOShipLine.shipmentNbr, IsNotNull>>, And<PX.Objects.SO.SOLine.lineType, NotEqual<SOLineType.miscCharge>, And<Where<SOShipLine.shipmentNbr, IsNotNull, Or2<Where<Current<PX.Objects.SO.SOOrder.siteCntr>, Equal<int1>, And<Current<PX.Objects.SO.SOOrder.shipComplete>, Equal<SOShipComplete.cancelRemainder>, And<PX.Objects.SO.SOLine.shipComplete, Equal<SOShipComplete.cancelRemainder>, Or<PX.Objects.SO.SOLine.shipDate, LessEqual<Current<PX.Objects.SO.SOOrderShipment.shipDate>>>>>>, And<Where<PX.Objects.SO.SOLine.openQty, Greater<decimal0>, Or<PX.Objects.SO.SOLine.openLine, Equal<True>>>>>>>>>>>>, OrderBy<Asc<PX.Objects.SO.SOLine.isFree>>>(), new object[2]
    {
      (object) order,
      ((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) origDocumentGraph)).Current
    });
  }

  protected override (BqlCommand, object[]) GetOrigDocumentSchedulesCommandAndCurrents(
    PX.Objects.SO.SOOrderShipment order,
    PX.Objects.SO.SOLine line,
    SOShipLine shipline)
  {
    return ((BqlCommand) new Select2<PX.Objects.SO.SOLineSplit, InnerJoin<INItemPlan, On<INItemPlan.planID, Equal<PX.Objects.SO.SOLineSplit.planID>>, LeftJoin<SOShipLine, On<SOShipLine.origOrderType, Equal<PX.Objects.SO.SOLineSplit.orderType>, And<SOShipLine.origOrderNbr, Equal<PX.Objects.SO.SOLineSplit.orderNbr>, And<SOShipLine.origLineNbr, Equal<PX.Objects.SO.SOLineSplit.lineNbr>, And<SOShipLine.origSplitLineNbr, Equal<PX.Objects.SO.SOLineSplit.splitLineNbr>, And<SOShipLine.shipmentType, Equal<Current<PX.Objects.SO.SOOrderShipment.shipmentType>>, And<SOShipLine.shipmentNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.shipmentNbr>>>>>>>>, LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.inventoryID, Equal<PX.Objects.SO.SOLineSplit.inventoryID>, And<INSiteStatusByCostCenter.subItemID, Equal<PX.Objects.SO.SOLineSplit.subItemID>, And<INSiteStatusByCostCenter.siteID, Equal<PX.Objects.SO.SOLineSplit.siteID>, And<INSiteStatusByCostCenter.costCenterID, Equal<PX.Objects.SO.SOLineSplit.costCenterID>>>>>>>>, Where<PX.Objects.SO.SOLineSplit.orderType, Equal<Current<PX.Objects.SO.SOLine.orderType>>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<Current<PX.Objects.SO.SOLine.orderNbr>>, And<PX.Objects.SO.SOLineSplit.lineNbr, Equal<Current<PX.Objects.SO.SOLine.lineNbr>>, And<PX.Objects.SO.SOLineSplit.siteID, Equal<Current<PX.Objects.SO.SOOrderShipment.siteID>>, And<Where<PX.Objects.SO.SOLineSplit.shipDate, LessEqual<Current<PX.Objects.SO.SOOrderShipment.shipDate>>, Or<SOShipLine.shipmentNbr, IsNotNull, Or<PX.Objects.SO.SOLineSplit.isAllocated, Equal<False>>>>>>>>>>(), new object[2]
    {
      (object) order,
      (object) line
    });
  }

  protected override (BqlCommand, object[]) GetOrigDocumentDemandSplitsWithPlansCommandAndCurrents(
    PX.Objects.SO.SOOrderShipment order)
  {
    return ((BqlCommand) new Select2<PX.Objects.SO.SOLineSplit, InnerJoin<INItemPlan, On<INItemPlan.supplyPlanID, Equal<PX.Objects.SO.SOLineSplit.planID>>>, Where<PX.Objects.SO.SOLineSplit.orderType, Equal<Current<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.orderNbr>>>>>(), new object[1]
    {
      (object) order
    });
  }

  protected override void UpdateOrigDocument(
    SOOrderEntry origDocumentGraph,
    PX.Objects.SO.SOShipment shipment,
    PX.Objects.SO.SOOrderShipment order)
  {
    Decimal? shipmentQty = order.ShipmentQty;
    Decimal num = 0M;
    if (shipmentQty.GetValueOrDefault() <= num & shipmentQty.HasValue)
      throw new PXException("Unable to confirm shipment {0} with zero Shipped Qty. for Order {1} {2}.", new object[3]
      {
        (object) shipment.ShipmentNbr,
        (object) order.OrderType,
        (object) order.OrderNbr
      });
    order.Confirmed = new bool?(true);
    GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrderShipment>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base), (object) order, true);
    ((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrderShipment>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).IsDirty = true;
    ((PXGraph) origDocumentGraph).Clear();
    ((PXSelectBase<PX.Objects.SO.SOOrder>) origDocumentGraph.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) origDocumentGraph.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) order.OrderNbr, new object[1]
    {
      (object) order.OrderType
    }));
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) origDocumentGraph.Document).Current;
    int? openShipmentCntr1 = current.OpenShipmentCntr;
    current.OpenShipmentCntr = openShipmentCntr1.HasValue ? new int?(openShipmentCntr1.GetValueOrDefault() - 1) : new int?();
    ((PXSelectBase<PX.Objects.SO.SOOrder>) origDocumentGraph.Document).Current.LastSiteID = order.SiteID;
    ((PXSelectBase<PX.Objects.SO.SOOrder>) origDocumentGraph.Document).Current.LastShipDate = order.ShipDate;
    GraphHelper.MarkUpdated(((PXSelectBase) origDocumentGraph.Document).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) origDocumentGraph.Document).Current, true);
    SOOrderSite soOrderSite1 = ((PXSelectBase<SOOrderSite>) origDocumentGraph.OrderSite).SelectSingle(new object[3]
    {
      (object) order.OrderType,
      (object) order.OrderNbr,
      (object) order.SiteID
    });
    SOOrderSite soOrderSite2 = soOrderSite1;
    int? openShipmentCntr2 = soOrderSite2.OpenShipmentCntr;
    soOrderSite2.OpenShipmentCntr = openShipmentCntr2.HasValue ? new int?(openShipmentCntr2.GetValueOrDefault() - 1) : new int?();
    ((PXSelectBase<SOOrderSite>) origDocumentGraph.OrderSite).Update(soOrderSite1);
    ((PXSelectBase<PX.Objects.SO.SOOrderType>) origDocumentGraph.soordertype).Current.RequireControlTotal = new bool?(false);
  }

  protected virtual void ValidateLineType(PX.Objects.SO.SOLine line, PX.Objects.IN.InventoryItem item, string message)
  {
    if (item.KitItem.GetValueOrDefault() && !item.StkItem.GetValueOrDefault() && line.LineType == "GN")
      throw new PXException(message, new object[2]
      {
        (object) line.LineNbr,
        (object) line.OrderNbr
      });
  }

  private void CreateNewSOLines(SOOrderEntry origDocumentGraph, PX.Objects.SO.SOLine line, SOShipLine shipline)
  {
    if (!line.AutoCreateIssueLine.GetValueOrDefault() || !(shipline.Operation == "R"))
      return;
    if (PXResultset<PX.Objects.SO.SOLine>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLine, PXSelect<PX.Objects.SO.SOLine, Where<PX.Objects.SO.SOLine.orderType, Equal<Required<PX.Objects.SO.SOLine.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Required<PX.Objects.SO.SOLine.orderNbr>>, And<PX.Objects.SO.SOLine.origOrderType, Equal<Required<PX.Objects.SO.SOLine.origOrderType>>, And<PX.Objects.SO.SOLine.origOrderNbr, Equal<Required<PX.Objects.SO.SOLine.origOrderNbr>>, And<PX.Objects.SO.SOLine.origLineNbr, Equal<Required<PX.Objects.SO.SOLine.origLineNbr>>>>>>>>.Config>.SelectWindowed((PXGraph) origDocumentGraph, 0, 1, new object[5]
    {
      (object) line.OrderType,
      (object) line.OrderNbr,
      (object) line.OrderType,
      (object) line.OrderNbr,
      (object) line.LineNbr
    })) != null)
      return;
    PX.Objects.SO.SOLine copy1 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) origDocumentGraph.Transactions).Insert(new PX.Objects.SO.SOLine()
    {
      OrderType = line.OrderType,
      OrderNbr = line.OrderNbr
    }));
    copy1.IsStockItem = line.IsStockItem;
    copy1.InventoryID = line.InventoryID;
    copy1.SubItemID = line.SubItemID;
    copy1.UOM = line.UOM;
    copy1.SiteID = line.SiteID;
    copy1.OrigOrderType = line.OrderType;
    copy1.OrigOrderNbr = line.OrderNbr;
    copy1.OrigLineNbr = line.LineNbr;
    copy1.ManualDisc = line.ManualDisc;
    copy1.ManualPrice = new bool?(true);
    copy1.CuryUnitPrice = line.CuryUnitPrice;
    copy1.SalesPersonID = line.SalesPersonID;
    copy1.ProjectID = line.ProjectID;
    copy1.TaskID = line.TaskID;
    copy1.CostCodeID = line.CostCodeID;
    copy1.ReasonCode = line.ReasonCode;
    copy1.IsSpecialOrder = new bool?(false);
    PX.Objects.SO.SOLine soLine1 = ((PXSelectBase<PX.Objects.SO.SOLine>) origDocumentGraph.Transactions).Update(copy1);
    bool flag1 = false;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) origDocumentGraph, line.InventoryID);
    Decimal? nullable1 = line.OrderQty;
    Decimal num1 = 1M;
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() % num1) : new Decimal?();
    Decimal num2 = 0M;
    bool? nullable3;
    Decimal? nullable4;
    if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue) && inventoryItem != null)
    {
      nullable3 = inventoryItem.DecimalSalesUnit;
      bool flag2 = false;
      if (nullable3.GetValueOrDefault() == flag2 & nullable3.HasValue && !string.Equals(inventoryItem.BaseUnit, line.UOM, StringComparison.OrdinalIgnoreCase))
      {
        INUnit inUnit = INUnit.UK.ByInventory.Find((PXGraph) origDocumentGraph, line.InventoryID, line.UOM);
        if (inUnit?.UnitMultDiv == "M")
        {
          nullable4 = inUnit.UnitRate;
          Decimal num3 = 1M;
          if (nullable4.GetValueOrDefault() > num3 & nullable4.HasValue)
            flag1 = true;
        }
      }
    }
    if (flag1)
    {
      PX.Objects.SO.SOLineSplit soLineSplit1 = new PX.Objects.SO.SOLineSplit()
      {
        UOM = inventoryItem.BaseUnit
      };
      PX.Objects.SO.SOLineSplit copy2 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLineSplit>) origDocumentGraph.splits).Insert(soLineSplit1));
      PX.Objects.SO.SOLineSplit soLineSplit2 = copy2;
      short? lineSign = line.LineSign;
      nullable4 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
      nullable1 = line.BaseOrderQty;
      Decimal? nullable5 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
      soLineSplit2.Qty = nullable5;
      ((PXSelectBase<PX.Objects.SO.SOLineSplit>) origDocumentGraph.splits).Update(copy2);
    }
    else
    {
      PX.Objects.SO.SOLine copy3 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(soLine1);
      PX.Objects.SO.SOLine soLine2 = copy3;
      nullable1 = line.OrderQty;
      Decimal? nullable6;
      if (!nullable1.HasValue)
      {
        nullable4 = new Decimal?();
        nullable6 = nullable4;
      }
      else
        nullable6 = new Decimal?(-nullable1.GetValueOrDefault());
      soLine2.OrderQty = nullable6;
      PX.Objects.SO.SOLine soLine3 = copy3;
      nullable1 = line.BaseOrderQty;
      Decimal? nullable7;
      if (!nullable1.HasValue)
      {
        nullable4 = new Decimal?();
        nullable7 = nullable4;
      }
      else
        nullable7 = new Decimal?(-nullable1.GetValueOrDefault());
      soLine3.BaseOrderQty = nullable7;
      soLine1 = ((PXSelectBase<PX.Objects.SO.SOLine>) origDocumentGraph.Transactions).Update(copy3);
    }
    nullable3 = line.ManualDisc;
    if (!nullable3.GetValueOrDefault())
      return;
    PX.Objects.SO.SOLine copy4 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(soLine1);
    copy4.DiscPct = line.DiscPct;
    PX.Objects.SO.SOLine soLine4 = copy4;
    nullable1 = line.CuryDiscAmt;
    Decimal? nullable8;
    if (!nullable1.HasValue)
    {
      nullable4 = new Decimal?();
      nullable8 = nullable4;
    }
    else
      nullable8 = new Decimal?(-nullable1.GetValueOrDefault());
    soLine4.CuryDiscAmt = nullable8;
    PX.Objects.SO.SOLine soLine5 = copy4;
    nullable1 = line.CuryLineAmt;
    Decimal? nullable9;
    if (!nullable1.HasValue)
    {
      nullable4 = new Decimal?();
      nullable9 = nullable4;
    }
    else
      nullable9 = new Decimal?(-nullable1.GetValueOrDefault());
    soLine5.CuryLineAmt = nullable9;
    ((PXSelectBase<PX.Objects.SO.SOLine>) origDocumentGraph.Transactions).Update(copy4);
  }
}
