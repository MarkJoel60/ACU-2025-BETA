// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Report.SOShipLineSplitForPacking
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO.Report;

[PXCacheName("Shipment Line Split For Packing")]
[PXProjection(typeof (SelectFromBase<PX.Objects.SO.Table.SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<PX.Objects.SO.Table.SOShipLineSplit.shipmentNbr>, GroupBy<PX.Objects.SO.Table.SOShipLineSplit.inventoryID>, GroupBy<PX.Objects.SO.Table.SOShipLineSplit.subItemID>, GroupBy<PX.Objects.SO.Table.SOShipLineSplit.lotSerialNbr>, Sum<PX.Objects.SO.Table.SOShipLineSplit.qty>, Sum<PX.Objects.SO.Table.SOShipLineSplit.baseQty>, Sum<PX.Objects.SO.Table.SOShipLineSplit.pickedQty>, Sum<PX.Objects.SO.Table.SOShipLineSplit.basePickedQty>, Sum<PX.Objects.SO.Table.SOShipLineSplit.packedQty>, Sum<PX.Objects.SO.Table.SOShipLineSplit.basePackedQty>>), Persistent = false)]
public class SOShipLineSplitForPacking : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual 
  #nullable disable
  string ShipmentNbr { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false, Visible = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual string OrigOrderType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual string OrigOrderNbr { get; set; }

  [PXDBInt(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual int? OrigLineNbr { get; set; }

  [PXDBInt(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual int? OrigSplitLineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  public virtual string OrigPlanType { get; set; }

  [PXDBString(1, IsFixed = true, InputMask = ">a", BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  [PXSelector(typeof (Search<SOOrderTypeOperation.operation, Where<SOOrderTypeOperation.orderType, Equal<Current<SOShipLineSplitForPacking.origOrderType>>>>))]
  public virtual string Operation { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual int? SplitLineNbr { get; set; }

  [PXDBShort(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual short? InvtMult { get; set; }

  [Inventory(Enabled = false, Visible = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(2, IsFixed = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual string LineType { get; set; }

  [PXDBBool(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  [PXFormula(typeof (Selector<SOShipLineSplitForPacking.inventoryID, PX.Objects.IN.InventoryItem.stkItem>))]
  public bool? IsStockItem { get; set; }

  [PXDBBool(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  [PXFormula(typeof (Switch<Case<Where<SOShipLineSplitForPacking.inventoryID, Equal<Current<SOShipLine.inventoryID>>>, False>, True>))]
  public bool? IsComponentItem { get; set; }

  [PXString(3, IsFixed = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  [PXFormula(typeof (Selector<SOShipLineSplitForPacking.operation, SOOrderTypeOperation.iNDocType>))]
  public virtual string TranType { get; set; }

  public virtual DateTime? TranDate => this.ShipDate;

  [PXDBString(2, IsFixed = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual string PlanType { get; set; }

  [PXDBLong(IsImmutable = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual long? PlanID { get; set; }

  [Site(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual int? SiteID { get; set; }

  [SubItem(typeof (SOShipLineSplitForPacking.inventoryID), BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual int? SubItemID { get; set; }

  [PXDBString(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual string LotSerialNbr { get; set; }

  [PXString(100, IsUnicode = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual string LastLotSerialNbr { get; set; }

  [PXString(10, IsUnicode = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual string LotSerClassID { get; set; }

  [PXString(30, IsUnicode = true, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual string AssignedNbr { get; set; }

  [PXDBDate(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual DateTime? ExpireDate { get; set; }

  [INUnit(typeof (SOShipLineSplitForPacking.inventoryID), DisplayName = "UOM", Enabled = false, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (SOShipLineSplitForPacking.uOM), typeof (SOShipLineSplitForPacking.baseQty), BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(6, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBQuantity(typeof (SOShipLineSplitForPacking.uOM), typeof (SOShipLineSplitForPacking.basePickedQty), BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  [PXUIField(DisplayName = "Picked Quantity", Enabled = false)]
  public virtual Decimal? PickedQty { get; set; }

  [PXDBDecimal(6, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual Decimal? BasePickedQty { get; set; }

  [PXDBQuantity(typeof (SOShipLineSplitForPacking.uOM), typeof (SOShipLineSplitForPacking.basePackedQty), BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  [PXUIField(DisplayName = "Packed Quantity", Enabled = false)]
  public virtual Decimal? PackedQty { get; set; }

  [PXDBDecimal(6, BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual Decimal? BasePackedQty { get; set; }

  [PXDBDate(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual DateTime? ShipDate { get; set; }

  [PXDBBool(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  [PXUIField(DisplayName = "Confirmed")]
  public virtual bool? Confirmed { get; set; }

  [PXDBBool(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? Released { get; set; }

  [PXDBBool(BqlTable = typeof (PX.Objects.SO.Table.SOShipLineSplit))]
  public virtual bool? IsUnassigned { get; set; }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.shipmentNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplitForPacking.lineNbr>
  {
  }

  public abstract class origOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.origOrderType>
  {
  }

  public abstract class origOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.origOrderNbr>
  {
  }

  public abstract class origLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipLineSplitForPacking.origLineNbr>
  {
  }

  public abstract class origSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipLineSplitForPacking.origSplitLineNbr>
  {
  }

  public abstract class origPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.origPlanType>
  {
  }

  public abstract class operation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.operation>
  {
  }

  public abstract class splitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipLineSplitForPacking.splitLineNbr>
  {
  }

  public abstract class invtMult : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    SOShipLineSplitForPacking.invtMult>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipLineSplitForPacking.inventoryID>
  {
  }

  public abstract class lineType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.lineType>
  {
  }

  public abstract class isStockItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipLineSplitForPacking.isStockItem>
  {
  }

  public abstract class isComponentItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipLineSplitForPacking.isComponentItem>
  {
  }

  public abstract class tranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.tranType>
  {
  }

  public abstract class planType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.planType>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOShipLineSplitForPacking.planID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplitForPacking.siteID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplitForPacking.subItemID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.lotSerialNbr>
  {
  }

  public abstract class lastLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.lastLotSerialNbr>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.lotSerClassID>
  {
  }

  public abstract class assignedNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitForPacking.assignedNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipLineSplitForPacking.expireDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLineSplitForPacking.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLineSplitForPacking.qty>
  {
  }

  public abstract class baseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLineSplitForPacking.baseQty>
  {
  }

  public abstract class pickedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLineSplitForPacking.pickedQty>
  {
  }

  public abstract class basePickedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLineSplitForPacking.basePickedQty>
  {
  }

  public abstract class packedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLineSplitForPacking.packedQty>
  {
  }

  public abstract class basePackedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLineSplitForPacking.basePackedQty>
  {
  }

  public abstract class shipDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipLineSplitForPacking.shipDate>
  {
  }

  public abstract class confirmed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipLineSplitForPacking.confirmed>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLineSplitForPacking.released>
  {
  }

  public abstract class isUnassigned : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipLineSplitForPacking.isUnassigned>
  {
  }
}
