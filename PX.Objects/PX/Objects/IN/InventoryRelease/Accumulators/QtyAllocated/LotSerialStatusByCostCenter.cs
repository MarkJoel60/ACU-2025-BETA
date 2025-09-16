// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;

[PXHidden]
[LotSerialStatusByCostCenter.Accumulator(BqlTable = typeof (INLotSerialStatusByCostCenter))]
public class LotSerialStatusByCostCenter : 
  INLotSerialStatusByCostCenter,
  IQtyAllocated,
  IQtyAllocatedBase
{
  [PXDBInt(IsKey = true)]
  [PXForeignSelector(typeof (INTran.inventoryID))]
  [PXSelectorMarker(typeof (SearchFor<PX.Objects.IN.InventoryItem.inventoryID>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<LotSerialStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>), CacheGlobal = true, ValidateValue = false)]
  [PXDefault]
  public override int? InventoryID { get; set; }

  [Site(typeof (Where<True, Equal<True>>), false, false, IsKey = true, ValidateValue = false)]
  [PXRestrictor(typeof (Where<True, Equal<True>>), "", new Type[] {}, ReplaceInherited = true)]
  [PXDefault]
  public override int? SiteID
  {
    get => base.SiteID;
    set => base.SiteID = value;
  }

  [Location(typeof (INLotSerialStatus.siteID), IsKey = true, ValidateValue = false)]
  [PXDefault]
  public override int? LocationID
  {
    get => base.LocationID;
    set => base.LocationID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? CostCenterID
  {
    get => base.CostCenterID;
    set => base.CostCenterID = value;
  }

  [PXInt]
  [PXFormula(typeof (BqlOperand<PX.Objects.IN.InventoryItem.itemClassID, IBqlInt>.FromSelectorOf<LotSerialStatusByCostCenter.inventoryID>))]
  [PXSelectorMarker(typeof (SearchFor<INItemClass.itemClassID>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<LotSerialStatusByCostCenter.itemClassID, IBqlInt>.FromCurrent>>), CacheGlobal = true)]
  public virtual int? ItemClassID { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<LotSerialStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>))]
  [PXSelector(typeof (INLotSerClass.lotSerClassID), CacheGlobal = true)]
  public virtual 
  #nullable disable
  string LotSerClassID { get; set; }

  [PXString(1, IsFixed = true)]
  [PXFormula(typeof (Selector<LotSerialStatusByCostCenter.lotSerClassID, INLotSerClass.lotSerAssign>))]
  public virtual string LotSerAssign { get; set; }

  [PXDate]
  public override DateTime? ExpireDate { get; set; }

  [PXDBDate]
  public override DateTime? ReceiptDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLotSerClass.lotSerClassID, IBqlString>.IsEqual<BqlField<LotSerialStatusByCostCenter.lotSerClassID, IBqlString>.FromCurrent>>))]
  public override string LotSerTrack { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Selector<LotSerialStatusByCostCenter.itemClassID, INItemClass.negQty>))]
  public virtual bool? NegQty { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyAvail { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<LotSerialStatusByCostCenter.itemClassID, IBqlInt>.FromCurrent>>))]
  public virtual string AvailabilitySchemeID { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINIssues))]
  public virtual bool? InclQtyINIssues { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINReceipts))]
  public virtual bool? InclQtyINReceipts { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINAssemblyDemand))]
  public virtual bool? InclQtyINAssemblyDemand { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINAssemblySupply))]
  public virtual bool? InclQtyINAssemblySupply { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyInTransit))]
  public virtual bool? InclQtyInTransit { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOReverse))]
  public virtual bool? InclQtySOReverse { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOBackOrdered))]
  public virtual bool? InclQtySOBackOrdered { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOPrepared))]
  public virtual bool? InclQtySOPrepared { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOBooked))]
  public virtual bool? InclQtySOBooked { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOShipped))]
  public virtual bool? InclQtySOShipped { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOShipping))]
  public virtual bool? InclQtySOShipping { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOReceipts))]
  public virtual bool? InclQtyPOReceipts { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOPrepared))]
  public virtual bool? InclQtyPOPrepared { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOOrders))]
  public virtual bool? InclQtyPOOrders { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFixedSOPO))]
  public virtual bool? InclQtyFixedSOPO { get; set; }

  [AvailabilityFlag(false)]
  public virtual bool? InclQtyPOFixedReceipt { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionDemandPrepared))]
  public virtual bool? InclQtyProductionDemandPrepared { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionDemand))]
  public virtual bool? InclQtyProductionDemand { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionAllocated))]
  public virtual bool? InclQtyProductionAllocated { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionSupplyPrepared))]
  public virtual bool? InclQtyProductionSupplyPrepared { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionSupply))]
  public virtual bool? InclQtyProductionSupply { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdPrepared))]
  public virtual bool? InclQtyFSSrvOrdPrepared { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdBooked))]
  public virtual bool? InclQtyFSSrvOrdBooked { get; set; }

  [AvailabilityFlag(typeof (LotSerialStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdAllocated))]
  public virtual bool? InclQtyFSSrvOrdAllocated { get; set; }

  [AvailabilityFlag(false)]
  public virtual bool? NegActualQty { get; set; }

  public new class PK : 
    PrimaryKeyOf<LotSerialStatusByCostCenter>.By<LotSerialStatusByCostCenter.inventoryID, LotSerialStatusByCostCenter.subItemID, LotSerialStatusByCostCenter.siteID, LotSerialStatusByCostCenter.locationID, LotSerialStatusByCostCenter.lotSerialNbr, LotSerialStatusByCostCenter.costCenterID>
  {
    public static LotSerialStatusByCostCenter Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? locationID,
      string lotSerialNbr,
      int? costCenterID,
      PKFindOptions options = 0)
    {
      return (LotSerialStatusByCostCenter) PrimaryKeyOf<LotSerialStatusByCostCenter>.By<LotSerialStatusByCostCenter.inventoryID, LotSerialStatusByCostCenter.subItemID, LotSerialStatusByCostCenter.siteID, LotSerialStatusByCostCenter.locationID, LotSerialStatusByCostCenter.lotSerialNbr, LotSerialStatusByCostCenter.costCenterID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) locationID, (object) lotSerialNbr, (object) costCenterID, options);
    }
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LotSerialStatusByCostCenter.siteID>
  {
  }

  public new abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.locationID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.lotSerialNbr>
  {
  }

  public new abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.costCenterID>
  {
  }

  public abstract class itemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.itemClassID>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.lotSerClassID>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.qtyOnHand>
  {
  }

  public abstract class lotSerAssign : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.lotSerAssign>
  {
  }

  public new abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.qtyActual>
  {
  }

  public new abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.expireDate>
  {
  }

  public new abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.receiptDate>
  {
  }

  public new abstract class lotSerTrack : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.lotSerTrack>
  {
  }

  public abstract class negQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LotSerialStatusByCostCenter.negQty>
  {
  }

  public abstract class inclQtyAvail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyAvail>
  {
  }

  public abstract class availabilitySchemeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.availabilitySchemeID>
  {
  }

  public abstract class inclQtyINIssues : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyINIssues>
  {
  }

  public abstract class inclQtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyINReceipts>
  {
  }

  public abstract class inclQtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyINAssemblyDemand>
  {
  }

  public abstract class inclQtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyINAssemblySupply>
  {
  }

  public abstract class inclQtyInTransit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyInTransit>
  {
  }

  public abstract class inclQtySOReverse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtySOReverse>
  {
  }

  public abstract class inclQtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtySOBackOrdered>
  {
  }

  public abstract class inclQtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtySOPrepared>
  {
  }

  public abstract class inclQtySOBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtySOBooked>
  {
  }

  public abstract class inclQtySOShipped : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtySOShipped>
  {
  }

  public abstract class inclQtySOShipping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtySOShipping>
  {
  }

  public abstract class inclQtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyPOReceipts>
  {
  }

  public abstract class inclQtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyPOPrepared>
  {
  }

  public abstract class inclQtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyPOOrders>
  {
  }

  public abstract class inclQtyFixedSOPO : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyFixedSOPO>
  {
  }

  public abstract class inclQtyPOFixedReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyPOFixedReceipt>
  {
  }

  public abstract class inclQtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyProductionDemandPrepared>
  {
  }

  public abstract class inclQtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyProductionDemand>
  {
  }

  public abstract class inclQtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyProductionAllocated>
  {
  }

  public abstract class inclQtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyProductionSupplyPrepared>
  {
  }

  public abstract class inclQtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyProductionSupply>
  {
  }

  public abstract class inclQtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyFSSrvOrdPrepared>
  {
  }

  public abstract class inclQtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyFSSrvOrdBooked>
  {
  }

  public abstract class inclQtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.inclQtyFSSrvOrdAllocated>
  {
  }

  public abstract class negActualQty : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LotSerialStatusByCostCenter.negActualQty>
  {
  }

  public class AccumulatorAttribute : StatusAccumulatorAttribute
  {
    public AccumulatorAttribute() => this.SingleRecord = true;

    protected override bool PrepareInsert(
      PXCache cache,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(cache, row, columns))
        return false;
      columns.Update<LotSerialStatusByCostCenter.lotSerTrack>((PXDataFieldAssign.AssignBehavior) 4);
      columns.Update<LotSerialStatusByCostCenter.receiptDate>((PXDataFieldAssign.AssignBehavior) 4);
      columns.Update<LotSerialStatusByCostCenter.qtyOnHand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyHardAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<LotSerialStatusByCostCenter.qtyActual>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyINIssues>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyINReceipts>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyINAssemblyDemand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyINAssemblySupply>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyInTransit>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtySOPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtySOBooked>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtySOShipped>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtySOShipping>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyPOReceipts>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyPOPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyPOOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyInTransitToProduction>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyProductionSupplyPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyProductionSupply>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyPOFixedProductionPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyPOFixedProductionOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyProductionDemandPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyProductionDemand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyProductionAllocated>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtySOFixedProduction>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyProdFixedPurchase>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyProdFixedProduction>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyProdFixedProdOrdersPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyProdFixedProdOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyProdFixedSalesOrdersPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyProdFixedSalesOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyFSSrvOrdPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyFSSrvOrdBooked>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLotSerialStatusByCostCenter.qtyFSSrvOrdAllocated>((PXDataFieldAssign.AssignBehavior) 1);
      LotSerialStatusByCostCenter a = (LotSerialStatusByCostCenter) row;
      Decimal? qtyOnHand = a.QtyOnHand;
      Decimal num1 = 0M;
      Decimal? nullable;
      if (qtyOnHand.GetValueOrDefault() < num1 & qtyOnHand.HasValue)
      {
        PXAccumulatorCollection accumulatorCollection = columns;
        nullable = a.QtyOnHand;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> local = (ValueType) (nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?());
        accumulatorCollection.Restrict<LotSerialStatusByCostCenter.qtyOnHand>((PXComp) 3, (object) local);
      }
      bool? negActualQty = a.NegActualQty;
      bool flag = false;
      if (negActualQty.GetValueOrDefault() == flag & negActualQty.HasValue)
      {
        nullable = a.QtyActual;
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() < num2 & nullable.HasValue && a.LotSerAssign == "R")
        {
          PXAccumulatorCollection accumulatorCollection = columns;
          nullable = a.QtyActual;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local = (ValueType) (nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?());
          accumulatorCollection.Restrict<LotSerialStatusByCostCenter.qtyActual>((PXComp) 3, (object) local);
        }
      }
      if (cache.GetStatus(row) != 2 || !this.IsZero((IStatus) a))
        return true;
      cache.SetStatus(row, (PXEntryStatus) 4);
      return false;
    }

    public override bool PersistInserted(PXCache cache, object row)
    {
      try
      {
        return base.PersistInserted(cache, row);
      }
      catch (PXLockViolationException ex)
      {
        LotSerialStatusByCostCenter statusByCostCenter = (LotSerialStatusByCostCenter) row;
        LotSerialStatusByCostCenter a = (LotSerialStatusByCostCenter) PrimaryKeyOf<LotSerialStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<LotSerialStatusByCostCenter.inventoryID, LotSerialStatusByCostCenter.subItemID, LotSerialStatusByCostCenter.siteID, LotSerialStatusByCostCenter.locationID, LotSerialStatusByCostCenter.lotSerialNbr, LotSerialStatusByCostCenter.costCenterID>>.Find(cache.Graph, (TypeArrayOf<IBqlField>.IFilledWith<LotSerialStatusByCostCenter.inventoryID, LotSerialStatusByCostCenter.subItemID, LotSerialStatusByCostCenter.siteID, LotSerialStatusByCostCenter.locationID, LotSerialStatusByCostCenter.lotSerialNbr, LotSerialStatusByCostCenter.costCenterID>) statusByCostCenter, (PKFindOptions) 0);
        LotSerialStatusByCostCenter sumStatus = this.Aggregate<LotSerialStatusByCostCenter>(cache, a, statusByCostCenter);
        string errorMessage = this.GetErrorMessage(cache, statusByCostCenter, sumStatus, false);
        if (errorMessage != null)
          throw new PXException(errorMessage, new object[5]
          {
            PXForeignSelectorAttribute.GetValueExt<LotSerialStatusByCostCenter.inventoryID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<LotSerialStatusByCostCenter.subItemID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<LotSerialStatusByCostCenter.siteID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<LotSerialStatusByCostCenter.locationID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<LotSerialStatusByCostCenter.lotSerialNbr>(cache, row)
          });
        throw;
      }
    }

    public override void RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
    {
      if (PXDBOperationExt.Command(e.Operation) == 2 && e.TranStatus == null)
      {
        LotSerialStatusByCostCenter row = (LotSerialStatusByCostCenter) e.Row;
        string errorMessage = this.GetErrorMessage(cache, row, (LotSerialStatusByCostCenter) null, true);
        if (errorMessage != null)
          throw new PXException(errorMessage, new object[5]
          {
            PXForeignSelectorAttribute.GetValueExt<LotSerialStatusByCostCenter.inventoryID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<LotSerialStatusByCostCenter.subItemID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<LotSerialStatusByCostCenter.siteID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<LotSerialStatusByCostCenter.locationID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<LotSerialStatusByCostCenter.lotSerialNbr>(cache, e.Row)
          });
      }
      base.RowPersisted(cache, e);
    }

    protected virtual string GetErrorMessage(
      PXCache cache,
      LotSerialStatusByCostCenter bal,
      LotSerialStatusByCostCenter sumStatus,
      bool insert)
    {
      string errorMessage = (string) null;
      Decimal? qtyOnHand = bal.QtyOnHand;
      Decimal num1 = 0M;
      Decimal? nullable;
      if (qtyOnHand.GetValueOrDefault() < num1 & qtyOnHand.HasValue)
      {
        if (!insert)
        {
          nullable = sumStatus.QtyOnHand;
          Decimal num2 = 0M;
          if (!(nullable.GetValueOrDefault() < num2 & nullable.HasValue))
            goto label_4;
        }
        errorMessage = GetCostLayerType(bal.CostCenterID) == "S" ? "The on-hand quantity of the {0} special-order item with the {4} lot or serial number in the {3} location of the {2} warehouse is not sufficient to process the document." : "Updating item '{0} {1}' in warehouse '{2} {3}' lot/serial number '{4}' quantity on hand will go negative.";
      }
label_4:
      bool? negActualQty = bal.NegActualQty;
      bool flag = false;
      if (negActualQty.GetValueOrDefault() == flag & negActualQty.HasValue)
      {
        nullable = bal.QtyActual;
        Decimal num3 = 0M;
        if (nullable.GetValueOrDefault() < num3 & nullable.HasValue && bal.LotSerAssign == "R")
        {
          if (!insert)
          {
            nullable = sumStatus.QtyActual;
            Decimal num4 = 0M;
            if (!(nullable.GetValueOrDefault() < num4 & nullable.HasValue))
              goto label_9;
          }
          errorMessage = GetCostLayerType(bal.CostCenterID) == "S" ? "The available-for-issue quantity of the {0} special-order item with the {4} lot or serial number in the {3} location of the {2} warehouse is not sufficient to release the document." : "The document cannot be released because the available-for-issue quantity would become negative for the {0} {1} item. This item has the {4} lot/serial number and is located in the {3} location of the {2} warehouse.";
        }
      }
label_9:
      return errorMessage;

      string GetCostLayerType(int? costCenterID)
      {
        int? nullable = costCenterID;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          return "N";
        return INCostCenter.PK.Find(cache.Graph, costCenterID)?.CostLayerType;
      }
    }
  }
}
