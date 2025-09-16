// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SO2PO.SO2POSyncFromPOReceiptExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.BQLConstants;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SO2PO;

public class SO2POSyncFromPOReceiptExtension<TGraph> : SO2POSyncBase<TGraph> where TGraph : PXGraph
{
  public virtual void Process(
    (string Type, string Nbr) poReceiptKey,
    IEnumerable<PXResult<INItemPlan, INPlanType>> poDemands)
  {
    PX.Objects.PO.POReceipt receipt = (PX.Objects.PO.POReceipt) null;
    if (poReceiptKey.Nbr != null && poDemands.Any<PXResult<INItemPlan, INPlanType>>())
      receipt = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptNbr, Equal<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.AsOptional>>>>, And<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.AsOptional>>>>.And<BqlOperand<PX.Objects.PO.POReceipt.origReceiptNbr, IBqlString>.IsNotNull>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object[]) new string[2]
      {
        poReceiptKey.Nbr,
        poReceiptKey.Type
      }));
    List<PX.Objects.SO.SOLineSplit> soLineSplitList1 = new List<PX.Objects.SO.SOLineSplit>();
    List<PX.Objects.SO.SOLineSplit> soLineSplitList2 = new List<PX.Objects.SO.SOLineSplit>();
    List<INItemPlan> source = new List<INItemPlan>();
    foreach (PXResult<INItemPlan, INPlanType> poDemand in poDemands)
    {
      INItemPlan copy1 = PXCache<INItemPlan>.CreateCopy(PXResult<INItemPlan, INPlanType>.op_Implicit(poDemand));
      INPlanType inPlanType = PXResult<INItemPlan, INPlanType>.op_Implicit(poDemand);
      if (this.PlanCache.GetStatus(copy1) != 2)
        ((PXCache) this.PlanCache).SetStatus((object) copy1, (PXEntryStatus) 0);
      PX.Objects.SO.SOLineSplit soLineSplit1 = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelect<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.planID, Equal<Required<PX.Objects.SO.SOLineSplit.planID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) copy1.DemandPlanID
      }));
      if (soLineSplit1 != null)
      {
        bool? nullable1 = soLineSplit1.Completed;
        bool flag1 = false;
        if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue || this.SOSplitCache.GetStatus(soLineSplit1) == 1)
        {
          PX.Objects.SO.SOLineSplit copy2 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit1);
          PX.Objects.SO.SOLineSplit soLineSplit2 = copy2;
          Decimal? nullable2 = soLineSplit2.BaseReceivedQty;
          Decimal? nullable3 = copy1.PlanQty;
          soLineSplit2.BaseReceivedQty = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          PX.Objects.SO.SOLineSplit soLineSplit3 = copy2;
          PXCache<PX.Objects.SO.SOLineSplit> soSplitCache1 = this.SOSplitCache;
          int? inventoryId1 = copy2.InventoryID;
          string uom1 = copy2.UOM;
          nullable3 = copy2.BaseReceivedQty;
          Decimal num1 = nullable3.Value;
          Decimal? nullable4 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) soSplitCache1, inventoryId1, uom1, num1, INPrecision.QUANTITY));
          soLineSplit3.ReceivedQty = nullable4;
          this.SOSplitCache.Update(copy2);
          INItemPlan inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) copy1.DemandPlanID
          }));
          if (inItemPlan1 != null)
          {
            INItemPlan inItemPlan2 = inItemPlan1;
            nullable3 = copy2.BaseQty;
            nullable2 = copy2.BaseReceivedQty;
            Decimal? nullable5 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
            inItemPlan2.PlanQty = nullable5;
            this.PlanCache.Update(inItemPlan1);
          }
          PXSelectBase<INItemPlan> pxSelectBase = (PXSelectBase<INItemPlan>) new PXSelectJoin<INItemPlan, InnerJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.planID, Equal<INItemPlan.planID>>>, Where<INItemPlan.demandPlanID, Equal<Required<INItemPlan.demandPlanID>>, And<PX.Objects.SO.SOLineSplit.isAllocated, Equal<True>, And<PX.Objects.SO.SOLineSplit.siteID, Equal<Required<PX.Objects.SO.SOLineSplit.siteID>>>>>>((PXGraph) this.Base);
          if (!string.IsNullOrEmpty(copy1.LotSerialNbr))
            pxSelectBase.WhereAnd<Where<INItemPlan.lotSerialNbr, Equal<Required<INItemPlan.lotSerialNbr>>>>();
          PXResult<INItemPlan> pxResult1 = PXResultset<INItemPlan>.op_Implicit(pxSelectBase.Select(new object[3]
          {
            (object) copy1.DemandPlanID,
            (object) copy1.SiteID,
            (object) copy1.LotSerialNbr
          }));
          if (pxResult1 != null)
          {
            PX.Objects.SO.SOLineSplit soLineSplit4 = PXResult.Unwrap<PX.Objects.SO.SOLineSplit>((object) pxResult1);
            ((PXCache) this.SOSplitCache).SetStatus((object) soLineSplit4, (PXEntryStatus) 0);
            PX.Objects.SO.SOLineSplit copy3 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit4);
            PX.Objects.SO.SOLineSplit soLineSplit5 = copy3;
            nullable2 = soLineSplit5.BaseQty;
            nullable3 = copy1.PlanQty;
            soLineSplit5.BaseQty = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            PX.Objects.SO.SOLineSplit soLineSplit6 = copy3;
            PXCache<PX.Objects.SO.SOLineSplit> soSplitCache2 = this.SOSplitCache;
            int? inventoryId2 = copy3.InventoryID;
            string uom2 = copy3.UOM;
            nullable3 = copy3.BaseQty;
            Decimal num2 = nullable3.Value;
            Decimal? nullable6 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) soSplitCache2, inventoryId2, uom2, num2, INPrecision.QUANTITY));
            soLineSplit6.Qty = nullable6;
            copy3.POReceiptType = poReceiptKey.Type;
            copy3.POReceiptNbr = poReceiptKey.Nbr;
            this.SOSplitCache.Update(copy3);
            INItemPlan copy4 = PXCache<INItemPlan>.CreateCopy(PXResult<INItemPlan, INPlanType>.op_Implicit(poDemand));
            INItemPlan inItemPlan3 = copy4;
            nullable3 = inItemPlan3.PlanQty;
            nullable2 = copy1.PlanQty;
            inItemPlan3.PlanQty = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
            this.PlanCache.Update(copy4);
            inPlanType = PXCache<INPlanType>.CreateCopy(inPlanType);
            inPlanType.ReplanOnEvent = (string) null;
            inPlanType.DeleteOnEvent = new bool?(true);
            goto label_47;
          }
          this.SOOrderCache.Rows.Current = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>((PXCache) this.SOSplitCache, (object) copy2);
          long? planId = copy2.PlanID;
          PX.Objects.SO.SOLineSplit splitWithShipment = receipt != null ? this.GetExistingSOSplitWithShipment(receipt, copy2, copy1) : (PX.Objects.SO.SOLineSplit) null;
          PX.Objects.SO.SOLineSplit schedule;
          if (splitWithShipment == null)
          {
            schedule = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(copy2);
            this.ClearScheduleReferences(ref schedule);
          }
          else
          {
            schedule = splitWithShipment;
            schedule.Completed = new bool?(false);
            schedule.PlanID = new long?();
          }
          PX.Objects.SO.SOLineSplit soLineSplit7 = schedule;
          nullable1 = schedule.IsStockItem;
          bool flag2 = false;
          bool? nullable7 = new bool?(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue || inPlanType.ReplanOnEvent != "60");
          soLineSplit7.IsAllocated = nullable7;
          schedule.LotSerialNbr = copy1.LotSerialNbr;
          schedule.POCreate = new bool?(false);
          schedule.POSource = (string) null;
          schedule.POReceiptType = poReceiptKey.Type;
          schedule.POReceiptNbr = poReceiptKey.Nbr;
          schedule.SiteID = copy1.SiteID;
          schedule.CostCenterID = copy1.CostCenterID;
          schedule.VendorID = new int?();
          schedule.BaseReceivedQty = new Decimal?(0M);
          schedule.ReceivedQty = new Decimal?(0M);
          schedule.QtyOnOrders = new Decimal?(0M);
          schedule.BaseQty = copy1.PlanQty;
          PX.Objects.SO.SOLineSplit soLineSplit8 = schedule;
          PXCache<PX.Objects.SO.SOLineSplit> soSplitCache3 = this.SOSplitCache;
          int? inventoryId3 = schedule.InventoryID;
          string uom3 = schedule.UOM;
          nullable2 = schedule.BaseQty;
          Decimal num3 = nullable2.Value;
          Decimal? nullable8 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) soSplitCache3, inventoryId3, uom3, num3, INPrecision.QUANTITY));
          soLineSplit8.Qty = nullable8;
          if (!string.IsNullOrEmpty(schedule.LotSerialNbr))
          {
            int? costCenterId = schedule.CostCenterID;
            int num4 = 0;
            if (!(costCenterId.GetValueOrDefault() == num4 & costCenterId.HasValue))
            {
              PX.Objects.SO.SOLine soLine = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>((PXCache) this.SOSplitCache, (object) schedule);
              int num5;
              if (soLine == null)
              {
                num5 = 0;
              }
              else
              {
                nullable1 = soLine.IsSpecialOrder;
                num5 = nullable1.GetValueOrDefault() ? 1 : 0;
              }
              if (num5 != 0)
              {
                PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, schedule.InventoryID);
                if (INLotSerClass.PK.Find((PXGraph) this.Base, inventoryItem?.LotSerClassID)?.LotSerTrack == "S")
                {
                  schedule.UOM = inventoryItem.BaseUnit;
                  schedule.Qty = copy1.PlanQty;
                }
              }
            }
          }
          foreach (PXResult<INItemPlan> pxResult2 in PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) planId
          }))
          {
            INItemPlan copy5 = PXCache<INItemPlan>.CreateCopy(PXResult<INItemPlan>.op_Implicit(pxResult2));
            ((PXCache) this.PlanCache).SetStatus((object) copy5, (PXEntryStatus) 0);
            copy5.SupplyPlanID = copy1.PlanID;
            this.PlanCache.Update(copy5);
          }
          schedule.PlanID = copy1.PlanID;
          PX.Objects.SO.SOLineSplit soLineSplit9;
          if (splitWithShipment == null)
          {
            soLineSplit9 = this.SOSplitCache.Insert(schedule);
          }
          else
          {
            foreach (PXResult<INItemPlan> pxResult3 in PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLineSplit>.On<SOShipLineSplit.FK.ItemPlan>>>.Where<KeysRelation<CompositeKey<Field<SOShipLineSplit.origOrderType>.IsRelatedTo<PX.Objects.SO.SOLineSplit.orderType>, Field<SOShipLineSplit.origOrderNbr>.IsRelatedTo<PX.Objects.SO.SOLineSplit.orderNbr>, Field<SOShipLineSplit.origLineNbr>.IsRelatedTo<PX.Objects.SO.SOLineSplit.lineNbr>, Field<SOShipLineSplit.origSplitLineNbr>.IsRelatedTo<PX.Objects.SO.SOLineSplit.splitLineNbr>>.WithTablesOf<PX.Objects.SO.SOLineSplit, SOShipLineSplit>, PX.Objects.SO.SOLineSplit, SOShipLineSplit>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new PX.Objects.SO.SOLineSplit[1]
            {
              schedule
            }, Array.Empty<object>()))
            {
              INItemPlan inItemPlan4 = PXResult<INItemPlan>.op_Implicit(pxResult3);
              inItemPlan4.OrigPlanID = copy1.PlanID;
              this.PlanCache.Update(inItemPlan4);
            }
            soLineSplit9 = this.SOSplitCache.Update(schedule);
          }
          soLineSplitList2.Add(soLineSplit9);
          copy1.UOM = soLineSplit9.UOM;
          this.PlanCache.Update(copy1);
          goto label_47;
        }
      }
      if (copy1.DemandPlanID.HasValue)
      {
        inPlanType = PXCache<INPlanType>.CreateCopy(inPlanType);
        inPlanType.ReplanOnEvent = (string) null;
        inPlanType.DeleteOnEvent = new bool?(true);
      }
      else
      {
        PX.Objects.SO.SOLineSplit soLineSplit10 = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelect<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.planID, Equal<Required<PX.Objects.SO.SOLineSplit.planID>>, And<PX.Objects.SO.SOLineSplit.completed, Equal<False>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) copy1.PlanID
        }));
        if (soLineSplit10 != null)
        {
          ((PXCache) this.SOSplitCache).SetStatus((object) soLineSplit10, (PXEntryStatus) 0);
          PX.Objects.SO.SOLineSplit copy6 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit10);
          PX.Objects.SO.SOLineSplit soLineSplit11 = copy6;
          int? openChildLineCntr = copy6.OpenChildLineCntr;
          int num = 0;
          bool? nullable = new bool?(openChildLineCntr.GetValueOrDefault() == num & openChildLineCntr.HasValue);
          soLineSplit11.Completed = nullable;
          copy6.POCompleted = new bool?(true);
          soLineSplitList1.Add(copy6);
          this.SOSplitCache.Update(copy6);
          INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) copy1.PlanID
          }));
          source.Add(inItemPlan);
          this.PlanCache.Delete(inItemPlan);
        }
      }
label_47:
      if (inPlanType.ReplanOnEvent != null)
      {
        copy1.PlanType = inPlanType.ReplanOnEvent;
        copy1.SupplyPlanID = new long?();
        copy1.DemandPlanID = new long?();
        this.PlanCache.Update(copy1);
      }
      else if (inPlanType.DeleteOnEvent.GetValueOrDefault())
        this.PlanCache.Delete(copy1);
    }
    PX.Objects.SO.SOLineSplit soLineSplit12 = (PX.Objects.SO.SOLineSplit) null;
    foreach (PX.Objects.SO.SOLineSplit soLineSplit13 in soLineSplitList2)
    {
      int? nullable9;
      if (soLineSplit12 != null && soLineSplit12.OrderType == soLineSplit13.OrderType && soLineSplit12.OrderNbr == soLineSplit13.OrderNbr)
      {
        nullable9 = soLineSplit12.LineNbr;
        int? nullable10 = soLineSplit13.LineNbr;
        if (nullable9.GetValueOrDefault() == nullable10.GetValueOrDefault() & nullable9.HasValue == nullable10.HasValue)
        {
          nullable10 = soLineSplit12.InventoryID;
          nullable9 = soLineSplit13.InventoryID;
          if (nullable10.GetValueOrDefault() == nullable9.GetValueOrDefault() & nullable10.HasValue == nullable9.HasValue)
          {
            nullable9 = soLineSplit12.SubItemID;
            nullable10 = soLineSplit13.SubItemID;
            if (nullable9.GetValueOrDefault() == nullable10.GetValueOrDefault() & nullable9.HasValue == nullable10.HasValue)
            {
              nullable10 = soLineSplit12.ParentSplitLineNbr;
              nullable9 = soLineSplit13.ParentSplitLineNbr;
              if (nullable10.GetValueOrDefault() == nullable9.GetValueOrDefault() & nullable10.HasValue == nullable9.HasValue && soLineSplit12.LotSerialNbr != null && soLineSplit13.LotSerialNbr != null)
                continue;
            }
          }
        }
      }
      PX.Objects.SO.SOLineSplit parentSchedule = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelect<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.orderType, Equal<Required<PX.Objects.SO.SOLineSplit.orderType>>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<Required<PX.Objects.SO.SOLineSplit.orderNbr>>, And<PX.Objects.SO.SOLineSplit.lineNbr, Equal<Required<PX.Objects.SO.SOLineSplit.lineNbr>>, And<PX.Objects.SO.SOLineSplit.splitLineNbr, Equal<Required<PX.Objects.SO.SOLineSplit.parentSplitLineNbr>>>>>>>.Config>.Select((PXGraph) this.Base, new object[4]
      {
        (object) soLineSplit13.OrderType,
        (object) soLineSplit13.OrderNbr,
        (object) soLineSplit13.LineNbr,
        (object) soLineSplit13.ParentSplitLineNbr
      }));
      PX.Objects.SO.SOLineSplit soLineSplit14 = parentSchedule;
      bool? nullable11;
      int num6;
      if (soLineSplit14 == null)
      {
        num6 = 0;
      }
      else
      {
        nullable11 = soLineSplit14.POCompleted;
        num6 = nullable11.GetValueOrDefault() ? 1 : 0;
      }
      if (num6 != 0)
      {
        nullable11 = parentSchedule.Completed;
        if (!nullable11.GetValueOrDefault())
        {
          nullable9 = parentSchedule.OpenChildLineCntr;
          int num7 = 0;
          if (nullable9.GetValueOrDefault() == num7 & nullable9.HasValue)
            goto label_70;
        }
        Decimal? baseQty = parentSchedule.BaseQty;
        Decimal? baseQtyOnOrders = parentSchedule.BaseQtyOnOrders;
        Decimal? baseReceivedQty = parentSchedule.BaseReceivedQty;
        Decimal? nullable12 = baseQtyOnOrders.HasValue & baseReceivedQty.HasValue ? new Decimal?(baseQtyOnOrders.GetValueOrDefault() + baseReceivedQty.GetValueOrDefault()) : new Decimal?();
        if (baseQty.GetValueOrDefault() > nullable12.GetValueOrDefault() & baseQty.HasValue & nullable12.HasValue && source.Exists((Predicate<INItemPlan>) (x =>
        {
          long? planId3 = x.PlanID;
          long? planId4 = parentSchedule.PlanID;
          return planId3.GetValueOrDefault() == planId4.GetValueOrDefault() & planId3.HasValue == planId4.HasValue;
        })))
        {
          this.SOOrderCache.Rows.Current = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>((PXCache) this.SOSplitCache, (object) parentSchedule);
          parentSchedule = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(parentSchedule);
          INItemPlan copy = PXCache<INItemPlan>.CreateCopy(source.First<INItemPlan>((Func<INItemPlan, bool>) (x =>
          {
            long? planId1 = x.PlanID;
            long? planId2 = parentSchedule.PlanID;
            return planId1.GetValueOrDefault() == planId2.GetValueOrDefault() & planId1.HasValue == planId2.HasValue;
          })));
          this.UpdateSchedulesFromCompletedPO(parentSchedule, copy);
        }
      }
label_70:
      soLineSplit12 = soLineSplit13;
    }
    foreach (PX.Objects.SO.SOLineSplit soLineSplit15 in soLineSplitList1)
    {
      PX.Objects.SO.SOLineSplit soLineSplit16 = this.SOSplitCache.Locate(soLineSplit15);
      if (soLineSplit16 != null)
      {
        soLineSplit16.PlanID = new long?();
        this.SOSplitCache.Update(soLineSplit16);
      }
    }
    foreach (PX.Objects.SO.SOLineSplit split in soLineSplitList2)
      this.BreakupSplitByUom(split);
  }

  protected virtual PX.Objects.SO.SOLineSplit GetExistingSOSplitWithShipment(
    PX.Objects.PO.POReceipt receipt,
    PX.Objects.SO.SOLineSplit parentSchedule,
    INItemPlan plan)
  {
    if (receipt == null)
      return (PX.Objects.SO.SOLineSplit) null;
    return PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderNbr, Equal<BqlField<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.orderType, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.orderType, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.siteID, IBqlInt>.IsEqual<BqlField<INItemPlan.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.inventoryID, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.inventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOReceiptType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.origReceiptNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.parentSplitLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.splitLineNbr, IBqlInt>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.baseShippedQty, IBqlDecimal>.IsGreater<decimal0>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.baseQty, IBqlDecimal>.IsEqual<decimal0>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.completed, IBqlBool>.IsEqual<True>>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<INItemPlan.lotSerialNbr>, IsNull>>>>.Or<BqlOperand<Current2<INItemPlan.lotSerialNbr>, IBqlString>.IsEqual<EmptyString>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.lotSerialNbr, IsNull>>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<EmptyString>>>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<INItemPlan.lotSerialNbr, IBqlString>.FromCurrent.NoDefault>>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.baseShippedQty, IBqlDecimal>.IsLessEqual<BqlField<INItemPlan.planQty, IBqlDecimal>.FromCurrent>>>.Order<By<BqlField<PX.Objects.SO.SOLineSplit.baseShippedQty, IBqlDecimal>.Desc>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[3]
    {
      (object) receipt,
      (object) parentSchedule,
      (object) plan
    }, Array.Empty<object>()));
  }
}
