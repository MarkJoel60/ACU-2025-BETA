// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.BaseOnShipmentConfirmationExtension`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.SO.Models;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public abstract class BaseOnShipmentConfirmationExtension<TOrigDocumentGraph, TOrderShipment, TOrigDocument, TOrigDocumentLine, TOrigDocumentLineSplit> : 
  PXGraphExtension<ConfirmShipmentExtension, SOShipmentEntry>
  where TOrigDocumentGraph : PXGraph<TOrigDocumentGraph, TOrigDocument>
  where TOrderShipment : class, IBqlTable, new()
  where TOrigDocument : class, IBqlTable, new()
  where TOrigDocumentLine : class, IBqlTable, ILSPrimary, new()
  where TOrigDocumentLineSplit : class, IBqlTable, IItemPlanSource, ILSDetail, new()
{
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.ConfirmShipmentExtension.UpdateOrigDocumentOnConfirmShipment(PX.Objects.SO.Models.ConfirmShipmentArgs)" />
  /// .
  [PXOverride]
  public void UpdateOrigDocumentOnConfirmShipment(
    ConfirmShipmentArgs args,
    Action<ConfirmShipmentArgs> base_UpdateOrigDocumentOnConfirmShipment)
  {
    base_UpdateOrigDocumentOnConfirmShipment(args);
    if (!this.ShouldBeProcessed(args))
      return;
    TOrigDocumentGraph origDocumentGraph = args.OrigDocumentGraph as TOrigDocumentGraph;
    foreach (PXResult<TOrderShipment> orderShipment in this.GetOrderShipments())
    {
      TOrderShipment order = PXResult<TOrderShipment>.op_Implicit(orderShipment);
      this.UpdateOrigDocument(origDocumentGraph, args.Shipment, order);
      bool backorderExists = false;
      HashSet<TOrigDocumentLine> processedLines = new HashSet<TOrigDocumentLine>(PXCacheEx.GetComparer<TOrigDocumentLine>(GraphHelper.Caches<TOrigDocumentLine>((PXGraph) (object) origDocumentGraph)));
      Dictionary<long?, List<INItemPlan>> demandItemPlans = this.CollectDemandItemPlans(origDocumentGraph, order);
      (BqlCommand bqlCommand, object[] objArray) = this.GetOrigDocumentLinesWithShipLinesCommandAndCurrents(origDocumentGraph, args.Shipment, order);
      foreach (PXResult<TOrigDocumentLine, SOShipLine> pxResult in new PXView((PXGraph) (object) origDocumentGraph, false, bqlCommand).SelectMultiBound(objArray, Array.Empty<object>()))
      {
        TOrigDocumentLine line = PXResult<TOrigDocumentLine, SOShipLine>.op_Implicit(pxResult);
        SOShipLine shipline = PXResult<TOrigDocumentLine, SOShipLine>.op_Implicit(pxResult);
        int? siteId1 = shipline.SiteID;
        int num1 = 0;
        if (siteId1.GetValueOrDefault() > num1 & siteId1.HasValue)
        {
          siteId1 = ((ILSMaster) line).SiteID;
          int? siteId2 = shipline.SiteID;
          if (!(siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue))
          {
            Decimal? shippedQty = shipline.ShippedQty;
            Decimal num2 = 0M;
            if (shippedQty.GetValueOrDefault() == num2 & shippedQty.HasValue)
            {
              PXCache cache = ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache;
              SOShipLine soShipLine = (SOShipLine) cache.Locate((object) shipline) ?? shipline;
              soShipLine.Confirmed = new bool?(true);
              soShipLine.RequireINUpdate = new bool?(false);
              GraphHelper.MarkUpdated(cache, (object) soShipLine, true);
              cache.IsDirty = true;
              continue;
            }
            continue;
          }
        }
        if (shipline.ShipmentNbr != null && Math.Abs(shipline.BaseQty.Value) < 0.0000005M)
        {
          bool? addAllToShipment = ((PXSelectBase<SOSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.sosetup).Current.AddAllToShipment;
          bool flag = false;
          if (addAllToShipment.GetValueOrDefault() == flag & addAllToShipment.HasValue)
          {
            ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).Caches[typeof (SOShipLine)].SetStatus((object) shipline, (PXEntryStatus) 3);
            ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).Caches[typeof (SOShipLine)].ClearQueryCacheObsolete();
            shipline = new SOShipLine();
          }
        }
        this.ValidateLineShippingRuleAndQty(order, line, shipline);
        this.RecreateOrigDocumentDetails(origDocumentGraph, order, processedLines, demandItemPlans, line, shipline);
        this.ConfirmOrigDocumentLine(origDocumentGraph, line, shipline, ref backorderExists);
        this.ConfirmShipLine(order, line, shipline);
      }
      this.SaveOrigDocumentGraph(origDocumentGraph);
    }
  }

  /// <summary>
  /// Checks that line/shipline quantities and settings satisfy line shipping rules. Throws exceptions if they do not.
  /// </summary>
  protected virtual void ValidateLineShippingRuleAndQty(
    TOrderShipment order,
    TOrigDocumentLine line,
    SOShipLine shipline)
  {
    string lineShippingRule = this.GetLineShippingRule(line, shipline);
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, ((ILSMaster) line).InventoryID);
    if (shipline.ShipmentNbr != null && lineShippingRule == "C" && !this.IsFullyShipped(line))
      throw new PXException("Shipment cannot be confirmed because the quantity of the item for the line '{0}' with the Ship Complete setting is less than the ordered quantity.", new object[1]
      {
        (object) inventoryItem.InventoryCD
      });
    if (shipline.ShipmentNbr == null)
      return;
    bool? stkItem = inventoryItem.StkItem;
    bool flag = false;
    if (!(stkItem.GetValueOrDefault() == flag & stkItem.HasValue) || !inventoryItem.KitItem.GetValueOrDefault() || !(Math.Abs(shipline.BaseQty.Value) >= 0.0000005M))
      return;
    if (PXResultset<SOShipLineSplit>.op_Implicit(PXSelectBase<SOShipLineSplit, PXSelect<SOShipLineSplit, Where<SOShipLineSplit.shipmentNbr, Equal<Current<SOShipLine.shipmentNbr>>, And<SOShipLineSplit.lineNbr, Equal<Current<SOShipLine.lineNbr>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[1]
    {
      (object) shipline
    }, Array.Empty<object>())) == null)
      throw new PXException("The shipment cannot be confirmed for the order with the empty non-stock kit '{0}' in the line '{1}'.", new object[2]
      {
        (object) inventoryItem.InventoryCD,
        (object) shipline.LineNbr
      });
  }

  /// <summary>
  /// Updates and creates new OrigDocument splits and related item plans.
  /// </summary>
  /// <param name="origDocumentGraph">Graph for operations on the original document and its lines/splits.</param>
  /// <param name="order"></param>
  /// <param name="processedLines">Hashset of original document's lines that are already processed.</param>
  /// <param name="demandItemPlans"></param>
  /// <param name="line">Currently being processed line of original document.</param>
  /// <param name="shipline">ShipLine corresponding to <paramref name="line" />.</param>
  protected virtual void RecreateOrigDocumentDetails(
    TOrigDocumentGraph origDocumentGraph,
    TOrderShipment order,
    HashSet<TOrigDocumentLine> processedLines,
    Dictionary<long?, List<INItemPlan>> demandItemPlans,
    TOrigDocumentLine line,
    SOShipLine shipline)
  {
    string lineShippingRule = this.GetLineShippingRule(line, shipline);
    if (shipline.ShipmentNbr == null && !(lineShippingRule == "L") && !((PXSelectBase<INSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.insetup).Current.ReplanBackOrders.GetValueOrDefault() || !processedLines.Add(line))
      return;
    (BqlCommand bqlCommand, object[] objArray) = this.GetOrigDocumentSchedulesCommandAndCurrents(order, line, shipline);
    foreach (PXResult<TOrigDocumentLineSplit, INItemPlan, SOShipLine, INSiteStatusByCostCenter> schedres in new PXView((PXGraph) (object) origDocumentGraph, false, bqlCommand).SelectMultiBound(objArray, Array.Empty<object>()))
      this.ShipOrReplanOrigDocumentSplit(origDocumentGraph, demandItemPlans, line, shipline, schedres);
  }

  protected virtual TOrigDocumentLineSplit ShipOrReplanOrigDocumentSplit(
    TOrigDocumentGraph origDocumentGraph,
    Dictionary<long?, List<INItemPlan>> demandItemPlans,
    TOrigDocumentLine line,
    SOShipLine shipline,
    PXResult<TOrigDocumentLineSplit, INItemPlan, SOShipLine, INSiteStatusByCostCenter> schedres)
  {
    TOrigDocumentLineSplit origDocumentSplit = PXResult<TOrigDocumentLineSplit, INItemPlan, SOShipLine, INSiteStatusByCostCenter>.op_Implicit(schedres);
    SOShipLine shline = PXResult<TOrigDocumentLineSplit, INItemPlan, SOShipLine, INSiteStatusByCostCenter>.op_Implicit(schedres);
    string lineShippingRule = this.GetLineShippingRule(line, shipline);
    if (shipline.ShipmentNbr != null || lineShippingRule == "L")
    {
      if (shline.ShipmentNbr != null && Math.Abs(shline.BaseQty.Value) < 0.0000005M)
      {
        bool? addAllToShipment = ((PXSelectBase<SOSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.sosetup).Current.AddAllToShipment;
        bool flag = false;
        if (addAllToShipment.GetValueOrDefault() == flag & addAllToShipment.HasValue)
          shline = new SOShipLine();
      }
      List<INItemPlan> demand = this.UpdateAndGetDemand(origDocumentGraph, demandItemPlans, origDocumentSplit, shline);
      origDocumentSplit = this.CompleteOrRecreateOrigDocumentSplit(origDocumentGraph, line, lineShippingRule, (PXResult<TOrigDocumentLineSplit, INItemPlan, SOShipLine>) schedres, demand);
    }
    return origDocumentSplit;
  }

  protected virtual TOrigDocumentLineSplit CompleteOrRecreateOrigDocumentSplit(
    TOrigDocumentGraph origDocumentGraph,
    TOrigDocumentLine line,
    string lineShippingRule,
    PXResult<TOrigDocumentLineSplit, INItemPlan, SOShipLine> result,
    List<INItemPlan> scheduleDemand)
  {
    TOrigDocumentLineSplit origDocumentSplit = PXResult<TOrigDocumentLineSplit, INItemPlan, SOShipLine>.op_Implicit(result);
    INItemPlan origDocumentSplitPlan = PXResult<TOrigDocumentLineSplit, INItemPlan, SOShipLine>.op_Implicit(result);
    SOShipLine shline = PXResult<TOrigDocumentLineSplit, INItemPlan, SOShipLine>.op_Implicit(result);
    using (((PXGraph) (object) origDocumentGraph).FindImplementation<LineSplittingExtension<TOrigDocumentGraph, TOrigDocument, TOrigDocumentLine, TOrigDocumentLineSplit>>().SuppressedModeScope(true))
    {
      if (shline.ShipmentNbr != null || lineShippingRule == "C" || lineShippingRule == "L")
      {
        TOrigDocumentLineSplit copy = PXCache<TOrigDocumentLineSplit>.CreateCopy(origDocumentSplit);
        origDocumentSplit = this.CompleteOrigDocumentSplit(origDocumentGraph, line, shline, copy, origDocumentSplitPlan);
      }
      if (shline.ShipmentNbr != null && EnumerableExtensions.IsNotIn<string>(lineShippingRule, "C", "L") && !this.IsFullyShipped(line))
      {
        TOrigDocumentLineSplit copy = PXCache<TOrigDocumentLineSplit>.CreateCopy(origDocumentSplit);
        TOrigDocumentLineSplit split = this.FillBackorderedSplit(origDocumentGraph, origDocumentSplit, copy);
        Decimal? baseQty;
        if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>() && ((PXSelectBase<CommonSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.commonsetup).Current != null)
        {
          short? decPlQty = ((PXSelectBase<CommonSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.commonsetup).Current.DecPlQty;
          Decimal? nullable = decPlQty.HasValue ? new Decimal?((Decimal) decPlQty.GetValueOrDefault()) : new Decimal?();
          Decimal num1 = 0M;
          if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
          {
            Decimal num2 = INUnitAttribute.ConvertToBase((PXCache) GraphHelper.Caches<TOrigDocumentLineSplit>((PXGraph) (object) origDocumentGraph), ((ILSMaster) split).InventoryID, split.UOM, split.Qty.Value, INPrecision.QUANTITY);
            baseQty = split.BaseQty;
            Decimal valueOrDefault = baseQty.GetValueOrDefault();
            if (!(num2 == valueOrDefault & baseQty.HasValue))
              throw new PXException("The decimal precision for quantity values is too low to perform UOM conversion for the item {0} without loss of quantity. Increase the quantity decimal precision, or use another UOM.", new object[1]
              {
                (object) ((PXCache) GraphHelper.Caches<TOrigDocumentLineSplit>((PXGraph) (object) origDocumentGraph)).GetValueExt<PX.Objects.SO.SOLineSplit.inventoryID>((object) split).ToString().Trim()
              });
          }
        }
        baseQty = split.BaseQty;
        Decimal num = 0M;
        if (baseQty.GetValueOrDefault() > num & baseQty.HasValue)
        {
          origDocumentSplit = this.InsertBackorderedSplit(origDocumentGraph, split);
          if (scheduleDemand != null)
          {
            foreach (INItemPlan inItemPlan in scheduleDemand)
            {
              inItemPlan.SupplyPlanID = origDocumentSplit.PlanID;
              GraphHelper.MarkUpdated(((PXGraph) (object) origDocumentGraph).Caches[typeof (INItemPlan)], (object) inItemPlan);
            }
          }
        }
      }
      return origDocumentSplit;
    }
  }

  protected virtual TOrigDocumentLineSplit CompleteOrigDocumentSplit(
    TOrigDocumentGraph origDocumentGraph,
    TOrigDocumentLine line,
    SOShipLine shline,
    TOrigDocumentLineSplit origDocumentSplit,
    INItemPlan origDocumentSplitPlan)
  {
    origDocumentSplit.PlanID = new long?();
    origDocumentSplit = GraphHelper.Caches<TOrigDocumentLineSplit>((PXGraph) (object) origDocumentGraph).Update(origDocumentSplit);
    GraphHelper.Caches<INItemPlan>((PXGraph) (object) origDocumentGraph).Delete(origDocumentSplitPlan);
    return origDocumentSplit;
  }

  protected virtual bool ConfirmShipLine(
    TOrderShipment order,
    TOrigDocumentLine line,
    SOShipLine shipline)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, ((ILSMaster) line).InventoryID);
    return shipline.ShipmentNbr != null && this.Base1.ConfirmSingleLine(shipline, inventoryItem);
  }

  protected virtual List<INItemPlan> UpdateAndGetDemand(
    TOrigDocumentGraph origDocumentGraph,
    Dictionary<long?, List<INItemPlan>> demand,
    TOrigDocumentLineSplit origDocumentSplit,
    SOShipLine shline)
  {
    List<INItemPlan> demand1 = (List<INItemPlan>) null;
    if (origDocumentSplit.PlanID.HasValue && demand.TryGetValue(origDocumentSplit.PlanID, out demand1))
    {
      INItemPlan inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelectJoin<INItemPlan, InnerJoin<SOShipLineSplit, On<SOShipLineSplit.planID, Equal<INItemPlan.planID>>>, Where<SOShipLineSplit.shipmentNbr, Equal<Current2<SOShipLine.shipmentNbr>>, And<SOShipLineSplit.lineNbr, Equal<Current2<SOShipLine.lineNbr>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[1]
      {
        (object) shline
      }, Array.Empty<object>()));
      if (inItemPlan1 != null && inItemPlan1.PlanID.HasValue)
      {
        foreach (INItemPlan inItemPlan2 in demand1)
        {
          inItemPlan2.SupplyPlanID = inItemPlan1.PlanID;
          GraphHelper.MarkUpdated(((PXGraph) (object) origDocumentGraph).Caches[typeof (INItemPlan)], (object) inItemPlan2, true);
        }
        demand.Remove(origDocumentSplit.PlanID);
        demand1 = (List<INItemPlan>) null;
      }
    }
    return demand1;
  }

  protected virtual Dictionary<long?, List<INItemPlan>> CollectDemandItemPlans(
    TOrigDocumentGraph origDocumentGraph,
    TOrderShipment order)
  {
    Dictionary<long?, List<INItemPlan>> dictionary = new Dictionary<long?, List<INItemPlan>>();
    (BqlCommand bqlCommand, object[] objArray) = this.GetOrigDocumentDemandSplitsWithPlansCommandAndCurrents(order);
    foreach (PXResult<TOrigDocumentLineSplit, INItemPlan> pxResult in new PXView((PXGraph) (object) origDocumentGraph, true, bqlCommand).SelectMultiBound(objArray, Array.Empty<object>()))
    {
      TOrigDocumentLineSplit documentLineSplit1;
      INItemPlan inItemPlan1;
      pxResult.Deconstruct(ref documentLineSplit1, ref inItemPlan1);
      TOrigDocumentLineSplit documentLineSplit2 = documentLineSplit1;
      INItemPlan inItemPlan2 = inItemPlan1;
      EnumerableEx.Ensure<long?, List<INItemPlan>>((IDictionary<long?, List<INItemPlan>>) dictionary, documentLineSplit2.PlanID, (Func<List<INItemPlan>>) (() => new List<INItemPlan>())).Add(inItemPlan2);
    }
    return dictionary;
  }

  protected virtual void SaveOrigDocumentGraph(TOrigDocumentGraph origDocumentGraph)
  {
    ((PXAction) origDocumentGraph.Save).Press();
  }

  protected abstract PXResultset<TOrderShipment> GetOrderShipments();

  protected abstract bool ShouldBeProcessed(ConfirmShipmentArgs args);

  protected abstract string GetLineShippingRule(TOrigDocumentLine line, SOShipLine shipline);

  /// <summary>
  /// Returns true if shipped quantity of the line is fully shipped.
  /// </summary>
  protected abstract bool IsFullyShipped(TOrigDocumentLine line);

  protected abstract TOrigDocumentLineSplit InsertBackorderedSplit(
    TOrigDocumentGraph origDocumentGraph,
    TOrigDocumentLineSplit split);

  protected abstract TOrigDocumentLineSplit FillBackorderedSplit(
    TOrigDocumentGraph origDocumentGraph,
    TOrigDocumentLineSplit origDocumentSplit,
    TOrigDocumentLineSplit newOrigDocumentSplit);

  protected abstract void ConfirmOrigDocumentLine(
    TOrigDocumentGraph origDocumentGraph,
    TOrigDocumentLine line,
    SOShipLine shipline,
    ref bool backorderExists);

  /// <summary>
  /// Gets command and current objects bundle that should return original document lines combined with existing related shiplines for specified shipment.<br />
  /// The command should return set of PXResult&lt;TOrigDocumentLine, SOShipline&gt;.
  /// </summary>
  protected abstract (BqlCommand, object[]) GetOrigDocumentLinesWithShipLinesCommandAndCurrents(
    TOrigDocumentGraph origDocumentGraph,
    PX.Objects.SO.SOShipment shipment,
    TOrderShipment order);

  /// <summary>
  /// Gets command and current objects to get original document splits combined with item plans and existing shiplines.<br />
  /// The command should return set of PXResult&lt;TOrigDocumentLineSplit, INItemPlan, SOShipLine, INSiteStatusByCostCenter&gt;.
  /// </summary>
  protected abstract (BqlCommand, object[]) GetOrigDocumentSchedulesCommandAndCurrents(
    TOrderShipment order,
    TOrigDocumentLine line,
    SOShipLine shipline);

  /// <summary>
  /// Gets command and current objects to get splits of original document combined with item plans.
  /// The command should return set of PXResult&lt;TOrigDocumentLineSplit, INItemPlan&gt;.
  /// </summary>
  protected abstract (BqlCommand, object[]) GetOrigDocumentDemandSplitsWithPlansCommandAndCurrents(
    TOrderShipment order);

  protected abstract void UpdateOrigDocument(
    TOrigDocumentGraph origDocumentGraph,
    PX.Objects.SO.SOShipment shipment,
    TOrderShipment order);
}
