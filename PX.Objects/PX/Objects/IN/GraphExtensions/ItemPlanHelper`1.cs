// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.ItemPlanHelper`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class ItemPlanHelper<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public virtual INPlanType GetTargetPlanType<TNode>(INItemPlan plan, INPlanType plantype) where TNode : class, IQtyAllocatedBase
  {
    if (plan.ExcludePlanLevel.HasValue)
    {
      if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter))
      {
        int? excludePlanLevel = plan.ExcludePlanLevel;
        if ((excludePlanLevel.HasValue ? new int?(excludePlanLevel.GetValueOrDefault() & 65536 /*0x010000*/) : new int?()).GetValueOrDefault() == 65536 /*0x010000*/)
          goto label_11;
      }
      if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter))
      {
        int? excludePlanLevel = plan.ExcludePlanLevel;
        if ((excludePlanLevel.HasValue ? new int?(excludePlanLevel.GetValueOrDefault() & 131072 /*0x020000*/) : new int?()).GetValueOrDefault() == 131072 /*0x020000*/)
          goto label_11;
      }
      if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter))
      {
        int? excludePlanLevel = plan.ExcludePlanLevel;
        if ((excludePlanLevel.HasValue ? new int?(excludePlanLevel.GetValueOrDefault() & 393216 /*0x060000*/) : new int?()).GetValueOrDefault() == 393216 /*0x060000*/)
          goto label_11;
      }
      if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial))
      {
        int? excludePlanLevel = plan.ExcludePlanLevel;
        if ((excludePlanLevel.HasValue ? new int?(excludePlanLevel.GetValueOrDefault() & 262144 /*0x040000*/) : new int?()).GetValueOrDefault() == 262144 /*0x040000*/)
          goto label_11;
      }
      if (typeof (TNode) == typeof (SiteLotSerial))
      {
        int? excludePlanLevel = plan.ExcludePlanLevel;
        if ((excludePlanLevel.HasValue ? new int?(excludePlanLevel.GetValueOrDefault() & 327680 /*0x050000*/) : new int?()).GetValueOrDefault() != 327680 /*0x050000*/)
          goto label_12;
      }
      else
        goto label_12;
label_11:
      return INPlanType.FromInt(0);
    }
label_12:
    if (!plan.IgnoreOrigPlan.GetValueOrDefault() && !string.IsNullOrEmpty(plan.OrigPlanType))
    {
      if (!(typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)))
      {
        if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter))
        {
          int? origPlanLevel = plan.OrigPlanLevel;
          if ((origPlanLevel.HasValue ? new int?(origPlanLevel.GetValueOrDefault() & 1) : new int?()).GetValueOrDefault() == 1)
            goto label_22;
        }
        if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter))
        {
          int? origPlanLevel = plan.OrigPlanLevel;
          if ((origPlanLevel.HasValue ? new int?(origPlanLevel.GetValueOrDefault() & 3) : new int?()).GetValueOrDefault() == 3)
            goto label_22;
        }
        if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial))
        {
          int? origPlanLevel = plan.OrigPlanLevel;
          if ((origPlanLevel.HasValue ? new int?(origPlanLevel.GetValueOrDefault() & 2) : new int?()).GetValueOrDefault() == 2)
            goto label_22;
        }
        if (typeof (TNode) == typeof (SiteLotSerial))
        {
          int? origPlanLevel = plan.OrigPlanLevel;
          if ((origPlanLevel.HasValue ? new int?(origPlanLevel.GetValueOrDefault() & 2) : new int?()).GetValueOrDefault() != 2)
            goto label_25;
        }
        else
          goto label_25;
      }
label_22:
      INPlanType inPlanType = INPlanType.PK.Find((PXGraph) this.Base, plan.OrigPlanType);
      return INPlanType.ToInt(plantype) <= 0 ? plantype + inPlanType : plantype - inPlanType;
    }
label_25:
    return plantype;
  }

  public virtual TNode UpdateAllocatedQuantitiesBase<TNode>(
    INItemPlan plan,
    INPlanType plantype,
    bool InclQtyAvail)
    where TNode : class, IQtyAllocatedBase
  {
    bool isDirty = this.Base.Caches[typeof (TNode)].IsDirty;
    TNode target = (TNode) this.Base.Caches[typeof (TNode)].Insert((object) this.ConvertPlan<TNode>(plan));
    this.Base.Caches[typeof (TNode)].IsDirty = isDirty;
    return this.UpdateAllocatedQuantitiesBase<TNode>(target, (IQtyPlanned) plan, plantype, new bool?(InclQtyAvail), plan.Hold, plan.RefEntityType);
  }

  protected TNode ConvertPlan<TNode>(INItemPlan item) where TNode : class, IQtyAllocatedBase
  {
    if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial))
      return INItemPlan.ToItemLotSerial(item) as TNode;
    if (typeof (TNode) == typeof (SiteLotSerial))
      return INItemPlan.ToSiteLotSerial(item) as TNode;
    if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter))
      return INItemPlan.ToSiteStatusByCostCenter(item) as TNode;
    if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter))
      return INItemPlan.ToLocationStatusByCostCenter(item) as TNode;
    return typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter) ? INItemPlan.ToLotSerialStatusByCostCenter(item) as TNode : default (TNode);
  }

  public virtual TNode UpdateAllocatedQuantitiesBase<TNode>(
    TNode target,
    IQtyPlanned plan,
    INPlanType plantype,
    bool? InclQtyAvail,
    bool? hold)
    where TNode : class, IQtyAllocatedBase
  {
    return this.UpdateAllocatedQuantitiesBase<TNode>(target, plan, plantype, InclQtyAvail, hold, (string) null);
  }

  public virtual TNode UpdateAllocatedQuantitiesBase<TNode>(
    TNode target,
    IQtyPlanned plan,
    INPlanType plantype,
    bool? InclQtyAvail,
    bool? hold,
    string refEntityType)
    where TNode : class, IQtyAllocatedBase
  {
    Decimal num1 = plan.PlanQty.GetValueOrDefault();
    if (plan.Reverse.GetValueOrDefault())
    {
      if (!target.InclQtySOReverse.GetValueOrDefault() && this.IsSORelated(plantype))
        return target;
      num1 = -num1;
    }
    if (hold.GetValueOrDefault() && INPlanConstants.ToModuleField(plantype.PlanType) == "IN" && !string.Equals(refEntityType, typeof (PX.Objects.PO.POReceipt).FullName, StringComparison.OrdinalIgnoreCase) && !this.GetAllocateDocumentsOnHold())
      num1 = 0M;
    short? nullable1;
    if (target is IQtyAllocated qtyAllocated)
    {
      IQtyAllocated qtyAllocated1 = qtyAllocated;
      Decimal? qtyInIssues = qtyAllocated1.QtyINIssues;
      Decimal num2 = (Decimal) plantype.InclQtyINIssues.GetValueOrDefault() * num1;
      qtyAllocated1.QtyINIssues = qtyInIssues.HasValue ? new Decimal?(qtyInIssues.GetValueOrDefault() + num2) : new Decimal?();
      IQtyAllocated qtyAllocated2 = qtyAllocated;
      Decimal? qtyInReceipts = qtyAllocated2.QtyINReceipts;
      nullable1 = plantype.InclQtyINReceipts;
      Decimal num3 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated2.QtyINReceipts = qtyInReceipts.HasValue ? new Decimal?(qtyInReceipts.GetValueOrDefault() + num3) : new Decimal?();
      IQtyAllocated qtyAllocated3 = qtyAllocated;
      Decimal? qtyPoPrepared = qtyAllocated3.QtyPOPrepared;
      nullable1 = plantype.InclQtyPOPrepared;
      Decimal num4 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated3.QtyPOPrepared = qtyPoPrepared.HasValue ? new Decimal?(qtyPoPrepared.GetValueOrDefault() + num4) : new Decimal?();
      IQtyAllocated qtyAllocated4 = qtyAllocated;
      Decimal? qtyPoOrders = qtyAllocated4.QtyPOOrders;
      nullable1 = plantype.InclQtyPOOrders;
      Decimal num5 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated4.QtyPOOrders = qtyPoOrders.HasValue ? new Decimal?(qtyPoOrders.GetValueOrDefault() + num5) : new Decimal?();
      IQtyAllocated qtyAllocated5 = qtyAllocated;
      Decimal? qtyPoReceipts = qtyAllocated5.QtyPOReceipts;
      nullable1 = plantype.InclQtyPOReceipts;
      Decimal num6 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated5.QtyPOReceipts = qtyPoReceipts.HasValue ? new Decimal?(qtyPoReceipts.GetValueOrDefault() + num6) : new Decimal?();
      IQtyAllocated qtyAllocated6 = qtyAllocated;
      Decimal? fsSrvOrdPrepared1 = qtyAllocated6.QtyFSSrvOrdPrepared;
      nullable1 = plantype.InclQtyFSSrvOrdPrepared;
      Decimal num7 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated6.QtyFSSrvOrdPrepared = fsSrvOrdPrepared1.HasValue ? new Decimal?(fsSrvOrdPrepared1.GetValueOrDefault() + num7) : new Decimal?();
      IQtyAllocated qtyAllocated7 = qtyAllocated;
      Decimal? qtyFsSrvOrdBooked = qtyAllocated7.QtyFSSrvOrdBooked;
      nullable1 = plantype.InclQtyFSSrvOrdBooked;
      Decimal num8 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated7.QtyFSSrvOrdBooked = qtyFsSrvOrdBooked.HasValue ? new Decimal?(qtyFsSrvOrdBooked.GetValueOrDefault() + num8) : new Decimal?();
      IQtyAllocated qtyAllocated8 = qtyAllocated;
      Decimal? fsSrvOrdAllocated = qtyAllocated8.QtyFSSrvOrdAllocated;
      nullable1 = plantype.InclQtyFSSrvOrdAllocated;
      Decimal num9 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated8.QtyFSSrvOrdAllocated = fsSrvOrdAllocated.HasValue ? new Decimal?(fsSrvOrdAllocated.GetValueOrDefault() + num9) : new Decimal?();
      IQtyAllocated qtyAllocated9 = qtyAllocated;
      Decimal? qtySoBackOrdered = qtyAllocated9.QtySOBackOrdered;
      nullable1 = plantype.InclQtySOBackOrdered;
      Decimal num10 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated9.QtySOBackOrdered = qtySoBackOrdered.HasValue ? new Decimal?(qtySoBackOrdered.GetValueOrDefault() + num10) : new Decimal?();
      IQtyAllocated qtyAllocated10 = qtyAllocated;
      Decimal? qtySoPrepared = qtyAllocated10.QtySOPrepared;
      nullable1 = plantype.InclQtySOPrepared;
      Decimal num11 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated10.QtySOPrepared = qtySoPrepared.HasValue ? new Decimal?(qtySoPrepared.GetValueOrDefault() + num11) : new Decimal?();
      IQtyAllocated qtyAllocated11 = qtyAllocated;
      Decimal? qtySoBooked = qtyAllocated11.QtySOBooked;
      nullable1 = plantype.InclQtySOBooked;
      Decimal num12 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated11.QtySOBooked = qtySoBooked.HasValue ? new Decimal?(qtySoBooked.GetValueOrDefault() + num12) : new Decimal?();
      IQtyAllocated qtyAllocated12 = qtyAllocated;
      Decimal? qtySoShipped = qtyAllocated12.QtySOShipped;
      nullable1 = plantype.InclQtySOShipped;
      Decimal num13 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated12.QtySOShipped = qtySoShipped.HasValue ? new Decimal?(qtySoShipped.GetValueOrDefault() + num13) : new Decimal?();
      IQtyAllocated qtyAllocated13 = qtyAllocated;
      Decimal? qtySoShipping = qtyAllocated13.QtySOShipping;
      nullable1 = plantype.InclQtySOShipping;
      Decimal num14 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated13.QtySOShipping = qtySoShipping.HasValue ? new Decimal?(qtySoShipping.GetValueOrDefault() + num14) : new Decimal?();
      IQtyAllocated qtyAllocated14 = qtyAllocated;
      Decimal? inAssemblySupply = qtyAllocated14.QtyINAssemblySupply;
      nullable1 = plantype.InclQtyINAssemblySupply;
      Decimal num15 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated14.QtyINAssemblySupply = inAssemblySupply.HasValue ? new Decimal?(inAssemblySupply.GetValueOrDefault() + num15) : new Decimal?();
      IQtyAllocated qtyAllocated15 = qtyAllocated;
      Decimal? inAssemblyDemand = qtyAllocated15.QtyINAssemblyDemand;
      nullable1 = plantype.InclQtyINAssemblyDemand;
      Decimal num16 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated15.QtyINAssemblyDemand = inAssemblyDemand.HasValue ? new Decimal?(inAssemblyDemand.GetValueOrDefault() + num16) : new Decimal?();
      IQtyAllocated qtyAllocated16 = qtyAllocated;
      Decimal? transitToProduction = qtyAllocated16.QtyInTransitToProduction;
      nullable1 = plantype.InclQtyInTransitToProduction;
      Decimal num17 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated16.QtyInTransitToProduction = transitToProduction.HasValue ? new Decimal?(transitToProduction.GetValueOrDefault() + num17) : new Decimal?();
      IQtyAllocated qtyAllocated17 = qtyAllocated;
      Decimal? productionSupplyPrepared = qtyAllocated17.QtyProductionSupplyPrepared;
      nullable1 = plantype.InclQtyProductionSupplyPrepared;
      Decimal num18 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated17.QtyProductionSupplyPrepared = productionSupplyPrepared.HasValue ? new Decimal?(productionSupplyPrepared.GetValueOrDefault() + num18) : new Decimal?();
      IQtyAllocated qtyAllocated18 = qtyAllocated;
      Decimal? productionSupply = qtyAllocated18.QtyProductionSupply;
      nullable1 = plantype.InclQtyProductionSupply;
      Decimal num19 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated18.QtyProductionSupply = productionSupply.HasValue ? new Decimal?(productionSupply.GetValueOrDefault() + num19) : new Decimal?();
      IQtyAllocated qtyAllocated19 = qtyAllocated;
      Decimal? productionPrepared = qtyAllocated19.QtyPOFixedProductionPrepared;
      nullable1 = plantype.InclQtyPOFixedProductionPrepared;
      Decimal num20 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated19.QtyPOFixedProductionPrepared = productionPrepared.HasValue ? new Decimal?(productionPrepared.GetValueOrDefault() + num20) : new Decimal?();
      IQtyAllocated qtyAllocated20 = qtyAllocated;
      Decimal? productionOrders = qtyAllocated20.QtyPOFixedProductionOrders;
      nullable1 = plantype.InclQtyPOFixedProductionOrders;
      Decimal num21 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated20.QtyPOFixedProductionOrders = productionOrders.HasValue ? new Decimal?(productionOrders.GetValueOrDefault() + num21) : new Decimal?();
      IQtyAllocated qtyAllocated21 = qtyAllocated;
      Decimal? productionDemandPrepared = qtyAllocated21.QtyProductionDemandPrepared;
      nullable1 = plantype.InclQtyProductionDemandPrepared;
      Decimal num22 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated21.QtyProductionDemandPrepared = productionDemandPrepared.HasValue ? new Decimal?(productionDemandPrepared.GetValueOrDefault() + num22) : new Decimal?();
      IQtyAllocated qtyAllocated22 = qtyAllocated;
      Decimal? productionDemand = qtyAllocated22.QtyProductionDemand;
      nullable1 = plantype.InclQtyProductionDemand;
      Decimal num23 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated22.QtyProductionDemand = productionDemand.HasValue ? new Decimal?(productionDemand.GetValueOrDefault() + num23) : new Decimal?();
      IQtyAllocated qtyAllocated23 = qtyAllocated;
      Decimal? productionAllocated = qtyAllocated23.QtyProductionAllocated;
      nullable1 = plantype.InclQtyProductionAllocated;
      Decimal num24 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated23.QtyProductionAllocated = productionAllocated.HasValue ? new Decimal?(productionAllocated.GetValueOrDefault() + num24) : new Decimal?();
      IQtyAllocated qtyAllocated24 = qtyAllocated;
      Decimal? soFixedProduction = qtyAllocated24.QtySOFixedProduction;
      nullable1 = plantype.InclQtySOFixedProduction;
      Decimal num25 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated24.QtySOFixedProduction = soFixedProduction.HasValue ? new Decimal?(soFixedProduction.GetValueOrDefault() + num25) : new Decimal?();
      IQtyAllocated qtyAllocated25 = qtyAllocated;
      Decimal? prodFixedPurchase = qtyAllocated25.QtyProdFixedPurchase;
      nullable1 = plantype.InclQtyProdFixedPurchase;
      Decimal num26 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated25.QtyProdFixedPurchase = prodFixedPurchase.HasValue ? new Decimal?(prodFixedPurchase.GetValueOrDefault() + num26) : new Decimal?();
      IQtyAllocated qtyAllocated26 = qtyAllocated;
      Decimal? prodFixedProduction = qtyAllocated26.QtyProdFixedProduction;
      nullable1 = plantype.InclQtyProdFixedProduction;
      Decimal num27 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated26.QtyProdFixedProduction = prodFixedProduction.HasValue ? new Decimal?(prodFixedProduction.GetValueOrDefault() + num27) : new Decimal?();
      IQtyAllocated qtyAllocated27 = qtyAllocated;
      Decimal? prodOrdersPrepared = qtyAllocated27.QtyProdFixedProdOrdersPrepared;
      nullable1 = plantype.InclQtyProdFixedProdOrdersPrepared;
      Decimal num28 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated27.QtyProdFixedProdOrdersPrepared = prodOrdersPrepared.HasValue ? new Decimal?(prodOrdersPrepared.GetValueOrDefault() + num28) : new Decimal?();
      IQtyAllocated qtyAllocated28 = qtyAllocated;
      Decimal? prodFixedProdOrders = qtyAllocated28.QtyProdFixedProdOrders;
      nullable1 = plantype.InclQtyProdFixedProdOrders;
      Decimal num29 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated28.QtyProdFixedProdOrders = prodFixedProdOrders.HasValue ? new Decimal?(prodFixedProdOrders.GetValueOrDefault() + num29) : new Decimal?();
      IQtyAllocated qtyAllocated29 = qtyAllocated;
      Decimal? salesOrdersPrepared = qtyAllocated29.QtyProdFixedSalesOrdersPrepared;
      nullable1 = plantype.InclQtyProdFixedSalesOrdersPrepared;
      Decimal num30 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated29.QtyProdFixedSalesOrdersPrepared = salesOrdersPrepared.HasValue ? new Decimal?(salesOrdersPrepared.GetValueOrDefault() + num30) : new Decimal?();
      IQtyAllocated qtyAllocated30 = qtyAllocated;
      Decimal? fixedSalesOrders = qtyAllocated30.QtyProdFixedSalesOrders;
      nullable1 = plantype.InclQtyProdFixedSalesOrders;
      Decimal num31 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated30.QtyProdFixedSalesOrders = fixedSalesOrders.HasValue ? new Decimal?(fixedSalesOrders.GetValueOrDefault() + num31) : new Decimal?();
      IQtyAllocated qtyAllocated31 = qtyAllocated;
      Decimal? qtyInReplaned = qtyAllocated31.QtyINReplaned;
      nullable1 = plantype.InclQtyINReplaned;
      Decimal num32 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated31.QtyINReplaned = qtyInReplaned.HasValue ? new Decimal?(qtyInReplaned.GetValueOrDefault() + num32) : new Decimal?();
      IQtyAllocated qtyAllocated32 = qtyAllocated;
      Decimal? qtyFixedFsSrvOrd = qtyAllocated32.QtyFixedFSSrvOrd;
      nullable1 = plantype.InclQtyFixedFSSrvOrd;
      Decimal num33 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated32.QtyFixedFSSrvOrd = qtyFixedFsSrvOrd.HasValue ? new Decimal?(qtyFixedFsSrvOrd.GetValueOrDefault() + num33) : new Decimal?();
      IQtyAllocated qtyAllocated33 = qtyAllocated;
      Decimal? qtyPoFixedFsSrvOrd = qtyAllocated33.QtyPOFixedFSSrvOrd;
      nullable1 = plantype.InclQtyPOFixedFSSrvOrd;
      Decimal num34 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated33.QtyPOFixedFSSrvOrd = qtyPoFixedFsSrvOrd.HasValue ? new Decimal?(qtyPoFixedFsSrvOrd.GetValueOrDefault() + num34) : new Decimal?();
      IQtyAllocated qtyAllocated34 = qtyAllocated;
      Decimal? fsSrvOrdPrepared2 = qtyAllocated34.QtyPOFixedFSSrvOrdPrepared;
      nullable1 = plantype.InclQtyPOFixedFSSrvOrdPrepared;
      Decimal num35 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated34.QtyPOFixedFSSrvOrdPrepared = fsSrvOrdPrepared2.HasValue ? new Decimal?(fsSrvOrdPrepared2.GetValueOrDefault() + num35) : new Decimal?();
      IQtyAllocated qtyAllocated35 = qtyAllocated;
      Decimal? fsSrvOrdReceipts = qtyAllocated35.QtyPOFixedFSSrvOrdReceipts;
      nullable1 = plantype.InclQtyPOFixedFSSrvOrdReceipts;
      Decimal num36 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated35.QtyPOFixedFSSrvOrdReceipts = fsSrvOrdReceipts.HasValue ? new Decimal?(fsSrvOrdReceipts.GetValueOrDefault() + num36) : new Decimal?();
      IQtyAllocated qtyAllocated36 = qtyAllocated;
      Decimal? qtySoFixed = qtyAllocated36.QtySOFixed;
      nullable1 = plantype.InclQtySOFixed;
      Decimal num37 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated36.QtySOFixed = qtySoFixed.HasValue ? new Decimal?(qtySoFixed.GetValueOrDefault() + num37) : new Decimal?();
      IQtyAllocated qtyAllocated37 = qtyAllocated;
      Decimal? qtyPoFixedOrders = qtyAllocated37.QtyPOFixedOrders;
      nullable1 = plantype.InclQtyPOFixedOrders;
      Decimal num38 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated37.QtyPOFixedOrders = qtyPoFixedOrders.HasValue ? new Decimal?(qtyPoFixedOrders.GetValueOrDefault() + num38) : new Decimal?();
      IQtyAllocated qtyAllocated38 = qtyAllocated;
      Decimal? qtyPoFixedPrepared = qtyAllocated38.QtyPOFixedPrepared;
      nullable1 = plantype.InclQtyPOFixedPrepared;
      Decimal num39 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated38.QtyPOFixedPrepared = qtyPoFixedPrepared.HasValue ? new Decimal?(qtyPoFixedPrepared.GetValueOrDefault() + num39) : new Decimal?();
      IQtyAllocated qtyAllocated39 = qtyAllocated;
      Decimal? qtyPoFixedReceipts = qtyAllocated39.QtyPOFixedReceipts;
      nullable1 = plantype.InclQtyPOFixedReceipts;
      Decimal num40 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated39.QtyPOFixedReceipts = qtyPoFixedReceipts.HasValue ? new Decimal?(qtyPoFixedReceipts.GetValueOrDefault() + num40) : new Decimal?();
      IQtyAllocated qtyAllocated40 = qtyAllocated;
      Decimal? qtySoDropShip = qtyAllocated40.QtySODropShip;
      nullable1 = plantype.InclQtySODropShip;
      Decimal num41 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated40.QtySODropShip = qtySoDropShip.HasValue ? new Decimal?(qtySoDropShip.GetValueOrDefault() + num41) : new Decimal?();
      IQtyAllocated qtyAllocated41 = qtyAllocated;
      Decimal? poDropShipOrders = qtyAllocated41.QtyPODropShipOrders;
      nullable1 = plantype.InclQtyPODropShipOrders;
      Decimal num42 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated41.QtyPODropShipOrders = poDropShipOrders.HasValue ? new Decimal?(poDropShipOrders.GetValueOrDefault() + num42) : new Decimal?();
      IQtyAllocated qtyAllocated42 = qtyAllocated;
      Decimal? dropShipPrepared = qtyAllocated42.QtyPODropShipPrepared;
      nullable1 = plantype.InclQtyPODropShipPrepared;
      Decimal num43 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated42.QtyPODropShipPrepared = dropShipPrepared.HasValue ? new Decimal?(dropShipPrepared.GetValueOrDefault() + num43) : new Decimal?();
      IQtyAllocated qtyAllocated43 = qtyAllocated;
      Decimal? dropShipReceipts = qtyAllocated43.QtyPODropShipReceipts;
      nullable1 = plantype.InclQtyPODropShipReceipts;
      Decimal num44 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated43.QtyPODropShipReceipts = dropShipReceipts.HasValue ? new Decimal?(dropShipReceipts.GetValueOrDefault() + num44) : new Decimal?();
      IQtyAllocated qtyAllocated44 = qtyAllocated;
      Decimal? qtyInTransitToSo = qtyAllocated44.QtyInTransitToSO;
      nullable1 = plantype.InclQtyInTransitToSO;
      Decimal num45 = (Decimal) nullable1.GetValueOrDefault() * num1;
      qtyAllocated44.QtyInTransitToSO = qtyInTransitToSo.HasValue ? new Decimal?(qtyInTransitToSo.GetValueOrDefault() + num45) : new Decimal?();
    }
    ref TNode local1 = ref target;
    // ISSUE: variable of a boxed type
    __Boxed<TNode> local2 = (object) local1;
    Decimal? nullable2 = local1.QtyInTransit;
    nullable1 = plantype.InclQtyInTransit;
    Decimal num46 = (Decimal) nullable1.GetValueOrDefault() * num1;
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num46) : new Decimal?();
    local2.QtyInTransit = nullable3;
    Decimal num47 = 0M;
    Decimal num48 = 0M;
    Decimal num49 = 0M;
    Decimal num50 = 0M;
    Decimal num51 = num47;
    Decimal num52;
    if (!target.InclQtyINIssues.GetValueOrDefault())
    {
      num52 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyINIssues;
      num52 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num53 = num51 - num52;
    Decimal num54;
    if (!target.InclQtyINReceipts.GetValueOrDefault())
    {
      num54 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyINReceipts;
      num54 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num55 = num53 + num54;
    Decimal num56;
    if (!target.InclQtyInTransit.GetValueOrDefault())
    {
      num56 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyInTransit;
      num56 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num57 = num55 + num56;
    Decimal num58;
    if (!target.InclQtyPOPrepared.GetValueOrDefault())
    {
      num58 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyPOPrepared;
      num58 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num59 = num57 + num58;
    Decimal num60;
    if (!target.InclQtyPOOrders.GetValueOrDefault())
    {
      num60 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyPOOrders;
      num60 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num61 = num59 + num60;
    Decimal num62;
    if (!target.InclQtyPOReceipts.GetValueOrDefault())
    {
      num62 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyPOReceipts;
      num62 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num63 = num61 + num62;
    Decimal num64;
    if (!target.InclQtyINAssemblySupply.GetValueOrDefault())
    {
      num64 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyINAssemblySupply;
      num64 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num65 = num63 + num64;
    Decimal num66;
    if (!target.InclQtyProductionSupplyPrepared.GetValueOrDefault())
    {
      num66 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyProductionSupplyPrepared;
      num66 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num67 = num65 + num66;
    Decimal num68;
    if (!target.InclQtyProductionSupply.GetValueOrDefault())
    {
      num68 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyProductionSupply;
      num68 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num69 = num67 + num68;
    Decimal num70;
    if (!target.InclQtyFSSrvOrdPrepared.GetValueOrDefault())
    {
      num70 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyFSSrvOrdPrepared;
      num70 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num71 = num69 - num70;
    Decimal num72;
    if (!target.InclQtyFSSrvOrdBooked.GetValueOrDefault())
    {
      num72 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyFSSrvOrdBooked;
      num72 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num73 = num71 - num72;
    Decimal num74;
    if (!target.InclQtyFSSrvOrdAllocated.GetValueOrDefault())
    {
      num74 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyFSSrvOrdAllocated;
      num74 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num75 = num73 - num74;
    Decimal num76;
    if (!target.InclQtySOBackOrdered.GetValueOrDefault())
    {
      num76 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtySOBackOrdered;
      num76 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num77 = num75 - num76;
    Decimal num78;
    if (!target.InclQtySOPrepared.GetValueOrDefault())
    {
      num78 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtySOPrepared;
      num78 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num79 = num77 - num78;
    Decimal num80;
    if (!target.InclQtySOBooked.GetValueOrDefault())
    {
      num80 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtySOBooked;
      num80 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num81 = num79 - num80;
    Decimal num82;
    if (!target.InclQtySOShipped.GetValueOrDefault())
    {
      num82 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtySOShipped;
      num82 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num83 = num81 - num82;
    Decimal num84;
    if (!target.InclQtySOShipping.GetValueOrDefault())
    {
      num84 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtySOShipping;
      num84 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num85 = num83 - num84;
    Decimal num86;
    if (!target.InclQtyINAssemblyDemand.GetValueOrDefault())
    {
      num86 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyINAssemblyDemand;
      num86 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num87 = num85 - num86;
    Decimal num88;
    if (!target.InclQtyProductionDemandPrepared.GetValueOrDefault())
    {
      num88 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyProductionDemandPrepared;
      num88 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num89 = num87 - num88;
    Decimal num90;
    if (!target.InclQtyProductionDemand.GetValueOrDefault())
    {
      num90 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyProductionDemand;
      num90 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num91 = num89 - num90;
    Decimal num92;
    if (!target.InclQtyProductionAllocated.GetValueOrDefault())
    {
      num92 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyProductionAllocated;
      num92 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num93 = num91 - num92;
    Decimal num94;
    if (!target.InclQtyPOFixedReceipt.GetValueOrDefault())
    {
      num94 = 0M;
    }
    else
    {
      nullable1 = plantype.InclQtyPOFixedReceipts;
      num94 = (Decimal) nullable1.GetValueOrDefault() * num1;
    }
    Decimal num95 = num93 + num94;
    bool? nullable4;
    if (target.InclQtyFixedSOPO.GetValueOrDefault())
    {
      Decimal num96 = num95;
      nullable4 = target.InclQtyPOOrders;
      Decimal num97;
      if (!nullable4.GetValueOrDefault())
      {
        num97 = 0M;
      }
      else
      {
        nullable1 = plantype.InclQtyPOFixedOrders;
        num97 = (Decimal) nullable1.GetValueOrDefault() * num1;
      }
      Decimal num98 = num96 + num97;
      nullable4 = target.InclQtyPOPrepared;
      Decimal num99;
      if (!nullable4.GetValueOrDefault())
      {
        num99 = 0M;
      }
      else
      {
        nullable1 = plantype.InclQtyPOFixedPrepared;
        num99 = (Decimal) nullable1.GetValueOrDefault() * num1;
      }
      Decimal num100 = num98 + num99;
      nullable4 = target.InclQtyPOReceipts;
      Decimal num101;
      if (!nullable4.GetValueOrDefault())
      {
        num101 = 0M;
      }
      else
      {
        nullable1 = plantype.InclQtyPOFixedReceipts;
        num101 = (Decimal) nullable1.GetValueOrDefault() * num1;
      }
      Decimal num102 = num100 + num101;
      nullable4 = target.InclQtySOBooked;
      Decimal num103;
      if (!nullable4.GetValueOrDefault())
      {
        num103 = 0M;
      }
      else
      {
        nullable1 = plantype.InclQtySOFixed;
        num103 = (Decimal) nullable1.GetValueOrDefault() * num1;
      }
      num95 = num102 - num103;
    }
    if (target is IQtyAllocatedSeparateReceipts separateReceipts1)
    {
      nullable4 = plan.Reverse;
      if (!nullable4.GetValueOrDefault())
      {
        Decimal num104 = num50;
        nullable4 = target.InclQtyINReceipts;
        Decimal num105;
        if (!nullable4.GetValueOrDefault())
        {
          num105 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtyINReceipts;
          num105 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num106 = num104 + num105;
        nullable4 = target.InclQtyPOPrepared;
        Decimal num107;
        if (!nullable4.GetValueOrDefault())
        {
          num107 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtyPOPrepared;
          num107 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num108 = num106 + num107;
        nullable4 = target.InclQtyPOOrders;
        Decimal num109;
        if (!nullable4.GetValueOrDefault())
        {
          num109 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtyPOOrders;
          num109 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num110 = num108 + num109;
        nullable4 = target.InclQtyPOReceipts;
        Decimal num111;
        if (!nullable4.GetValueOrDefault())
        {
          num111 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtyPOReceipts;
          num111 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num112 = num110 + num111;
        nullable4 = target.InclQtyPOFixedReceipt;
        Decimal num113;
        if (!nullable4.GetValueOrDefault())
        {
          num113 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtyPOFixedReceipts;
          num113 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num114 = num112 + num113;
        nullable4 = target.InclQtyINAssemblySupply;
        Decimal num115;
        if (!nullable4.GetValueOrDefault())
        {
          num115 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtyINAssemblySupply;
          num115 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        num50 = num114 + num115;
      }
      else
      {
        Decimal num116 = num50;
        nullable4 = target.InclQtySOBackOrdered;
        Decimal num117;
        if (!nullable4.GetValueOrDefault())
        {
          num117 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtySOBackOrdered;
          num117 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num118 = num116 - num117;
        nullable4 = target.InclQtySOPrepared;
        Decimal num119;
        if (!nullable4.GetValueOrDefault())
        {
          num119 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtySOPrepared;
          num119 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num120 = num118 - num119;
        nullable4 = target.InclQtySOBooked;
        Decimal num121;
        if (!nullable4.GetValueOrDefault())
        {
          num121 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtySOBooked;
          num121 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num122 = num120 - num121;
        nullable4 = target.InclQtySOShipped;
        Decimal num123;
        if (!nullable4.GetValueOrDefault())
        {
          num123 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtySOShipped;
          num123 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num124 = num122 - num123;
        nullable4 = target.InclQtySOShipping;
        Decimal num125;
        if (!nullable4.GetValueOrDefault())
        {
          num125 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtySOShipping;
          num125 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num126 = num124 - num125;
        nullable4 = target.InclQtyFSSrvOrdPrepared;
        Decimal num127;
        if (!nullable4.GetValueOrDefault())
        {
          num127 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtyFSSrvOrdPrepared;
          num127 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num128 = num126 - num127;
        nullable4 = target.InclQtyFSSrvOrdBooked;
        Decimal num129;
        if (!nullable4.GetValueOrDefault())
        {
          num129 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtyFSSrvOrdBooked;
          num129 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        Decimal num130 = num128 - num129;
        nullable4 = target.InclQtyFSSrvOrdAllocated;
        Decimal num131;
        if (!nullable4.GetValueOrDefault())
        {
          num131 = 0M;
        }
        else
        {
          nullable1 = plantype.InclQtyFSSrvOrdAllocated;
          num131 = (Decimal) nullable1.GetValueOrDefault() * num1;
        }
        num50 = num130 - num131;
      }
    }
    nullable4 = plan.Reverse;
    if (!nullable4.GetValueOrDefault())
    {
      Decimal num132 = num48;
      nullable1 = plantype.InclQtySOShipped;
      Decimal num133 = (Decimal) nullable1.GetValueOrDefault() * num1;
      Decimal num134 = num132 - num133;
      nullable1 = plantype.InclQtySOShipping;
      Decimal num135 = (Decimal) nullable1.GetValueOrDefault() * num1;
      Decimal num136 = num134 - num135;
      nullable1 = plantype.InclQtyINIssues;
      Decimal num137 = (Decimal) nullable1.GetValueOrDefault() * num1;
      Decimal num138 = num136 - num137;
      nullable1 = plantype.InclQtyProductionAllocated;
      Decimal num139 = (Decimal) nullable1.GetValueOrDefault() * num1;
      Decimal num140 = num138 - num139;
      nullable1 = plantype.InclQtyFSSrvOrdAllocated;
      Decimal num141 = (Decimal) nullable1.GetValueOrDefault() * num1;
      Decimal num142 = num140 - num141;
      nullable1 = plantype.InclQtyINAssemblyDemand;
      Decimal num143 = (Decimal) nullable1.GetValueOrDefault() * num1;
      num48 = num142 - num143;
      Decimal num144 = num49;
      nullable1 = plantype.InclQtySOShipped;
      Decimal num145 = (Decimal) nullable1.GetValueOrDefault() * num1;
      num49 = num144 - num145;
    }
    if (InclQtyAvail.GetValueOrDefault())
    {
      ref TNode local3 = ref target;
      // ISSUE: variable of a boxed type
      __Boxed<TNode> local4 = (object) local3;
      nullable2 = local3.QtyAvail;
      Decimal num146 = num95;
      Decimal? nullable5 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num146) : new Decimal?();
      local4.QtyAvail = nullable5;
      ref TNode local5 = ref target;
      // ISSUE: variable of a boxed type
      __Boxed<TNode> local6 = (object) local5;
      nullable2 = local5.QtyHardAvail;
      Decimal num147 = num48;
      Decimal? nullable6 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num147) : new Decimal?();
      local6.QtyHardAvail = nullable6;
      ref TNode local7 = ref target;
      // ISSUE: variable of a boxed type
      __Boxed<TNode> local8 = (object) local7;
      nullable2 = local7.QtyActual;
      Decimal num148 = num49;
      Decimal? nullable7 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num148) : new Decimal?();
      local8.QtyActual = nullable7;
      if (separateReceipts1 != null)
      {
        IQtyAllocatedSeparateReceipts separateReceipts2 = separateReceipts1;
        nullable2 = separateReceipts2.QtyOnReceipt;
        Decimal num149 = num50;
        separateReceipts2.QtyOnReceipt = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num149) : new Decimal?();
      }
    }
    else
    {
      nullable4 = InclQtyAvail;
      bool flag = false;
      if (nullable4.GetValueOrDefault() == flag & nullable4.HasValue)
      {
        ref TNode local9 = ref target;
        // ISSUE: variable of a boxed type
        __Boxed<TNode> local10 = (object) local9;
        nullable2 = local9.QtyNotAvail;
        Decimal num150 = num95;
        Decimal? nullable8 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num150) : new Decimal?();
        local10.QtyNotAvail = nullable8;
        if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter))
        {
          nullable1 = plantype.InclQtySOShipping;
          if (nullable1.GetValueOrDefault() != (short) 0)
          {
            ref TNode local11 = ref target;
            // ISSUE: variable of a boxed type
            __Boxed<TNode> local12 = (object) local11;
            nullable2 = local11.QtyNotAvail;
            nullable4 = target.InclQtySOBooked;
            Decimal num151;
            if (!nullable4.GetValueOrDefault())
            {
              num151 = 0M;
            }
            else
            {
              nullable1 = plantype.InclQtySOBooked;
              num151 = (Decimal) nullable1.GetValueOrDefault() * num1;
            }
            Decimal num152 = num151;
            Decimal? nullable9 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num152) : new Decimal?();
            local12.QtyNotAvail = nullable9;
            ref TNode local13 = ref target;
            // ISSUE: variable of a boxed type
            __Boxed<TNode> local14 = (object) local13;
            nullable2 = local13.QtyAvail;
            nullable4 = target.InclQtySOBooked;
            Decimal num153;
            if (!nullable4.GetValueOrDefault())
            {
              num153 = 0M;
            }
            else
            {
              nullable1 = plantype.InclQtySOBooked;
              num153 = (Decimal) nullable1.GetValueOrDefault() * num1;
            }
            Decimal num154 = num153;
            Decimal? nullable10 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num154) : new Decimal?();
            local14.QtyAvail = nullable10;
          }
        }
        if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter) && plantype.PlanType == "61")
        {
          nullable1 = plantype.InclQtySOBooked;
          if (nullable1.GetValueOrDefault() == (short) 0)
          {
            nullable1 = plantype.InclQtySOShipping;
            if (nullable1.GetValueOrDefault() == (short) 0)
            {
              nullable4 = plan.Reverse;
              if (!nullable4.GetValueOrDefault())
              {
                ref TNode local15 = ref target;
                // ISSUE: variable of a boxed type
                __Boxed<TNode> local16 = (object) local15;
                nullable2 = local15.QtyNotAvail;
                nullable4 = target.InclQtySOShipping;
                Decimal num155;
                if (!nullable4.GetValueOrDefault())
                {
                  num155 = 0M;
                }
                else
                {
                  nullable4 = plantype.DeleteOperation;
                  num155 = (nullable4.GetValueOrDefault() ? -1M : 1M) * num1;
                }
                Decimal num156 = num155;
                Decimal? nullable11 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num156) : new Decimal?();
                local16.QtyNotAvail = nullable11;
                ref TNode local17 = ref target;
                // ISSUE: variable of a boxed type
                __Boxed<TNode> local18 = (object) local17;
                nullable2 = local17.QtyAvail;
                nullable4 = target.InclQtySOShipping;
                Decimal num157;
                if (!nullable4.GetValueOrDefault())
                {
                  num157 = 0M;
                }
                else
                {
                  nullable4 = plantype.DeleteOperation;
                  num157 = (nullable4.GetValueOrDefault() ? -1M : 1M) * num1;
                }
                Decimal num158 = num157;
                Decimal? nullable12 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num158) : new Decimal?();
                local18.QtyAvail = nullable12;
                ref TNode local19 = ref target;
                // ISSUE: variable of a boxed type
                __Boxed<TNode> local20 = (object) local19;
                nullable2 = local19.QtyHardAvail;
                nullable4 = plantype.DeleteOperation;
                Decimal num159 = (nullable4.GetValueOrDefault() ? -1M : 1M) * num1;
                Decimal? nullable13 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num159) : new Decimal?();
                local20.QtyHardAvail = nullable13;
              }
            }
          }
        }
      }
    }
    return target;
  }

  protected bool IsSORelated(INPlanType plantype)
  {
    return plantype.InclQtySOBackOrdered.GetValueOrDefault() != (short) 0 || plantype.InclQtySOBooked.GetValueOrDefault() != (short) 0 || plantype.InclQtySODropShip.GetValueOrDefault() != (short) 0 || plantype.InclQtySOFixed.GetValueOrDefault() != (short) 0 || plantype.InclQtySOFixedProduction.GetValueOrDefault() != (short) 0 || plantype.InclQtySOPrepared.GetValueOrDefault() != (short) 0 || plantype.InclQtySOShipped.GetValueOrDefault() != (short) 0 || plantype.InclQtySOShipping.GetValueOrDefault() != (short) 0;
  }

  protected bool GetAllocateDocumentsOnHold()
  {
    bool? allocateDocumentsOnHold = ((PXSelectBase<INSetup>) new PXSetup<INSetup>((PXGraph) this.Base)).Current.AllocateDocumentsOnHold;
    bool flag = false;
    return !(allocateDocumentsOnHold.GetValueOrDefault() == flag & allocateDocumentsOnHold.HasValue);
  }

  public static void AddStatusDACsToCacheMapping(PXGraph graph)
  {
    PXCacheCollection caches = graph.Caches;
    caches.AddCacheMapping(typeof (INSiteStatusByCostCenter), typeof (INSiteStatusByCostCenter));
    caches.AddCacheMapping(typeof (INLocationStatusByCostCenter), typeof (INLocationStatusByCostCenter));
    caches.AddCacheMapping(typeof (INLotSerialStatusByCostCenter), typeof (INLotSerialStatusByCostCenter));
    caches.AddCacheMapping(typeof (INItemLotSerial), typeof (INItemLotSerial));
    caches.AddCacheMapping(typeof (INSiteLotSerial), typeof (INSiteLotSerial));
    caches.AddCacheMapping(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter), typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter));
    caches.AddCacheMapping(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter), typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter));
    caches.AddCacheMapping(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter), typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter));
    caches.AddCacheMapping(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial), typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial));
    caches.AddCacheMapping(typeof (SiteLotSerial), typeof (SiteLotSerial));
  }
}
