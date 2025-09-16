// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Discount;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.PM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Shipment Line")]
[Serializable]
public class SOShipLine : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSPrimary,
  ILSMaster,
  IItemPlanMaster,
  IHasMinGrossProfit,
  ISortOrder
{
  protected 
  #nullable disable
  string _ShipmentNbr;
  protected string _ShipmentType;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected int? _CustomerID;
  protected DateTime? _ShipDate;
  protected bool? _Confirmed;
  protected bool? _Released;
  protected string _LineType;
  protected string _OrigOrderType;
  protected string _OrigOrderNbr;
  protected int? _OrigLineNbr;
  protected int? _OrigSplitLineNbr;
  protected string _Operation;
  protected short? _InvtMult;
  protected int? _InventoryID;
  protected string _TranType;
  protected string _PlanType;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected DateTime? _ExpireDate;
  protected string _OrderUOM;
  protected string _UOM;
  protected Decimal? _ShippedQty;
  protected Decimal? _BaseShippedQty;
  protected Decimal? _BaseOriginalShippedQty;
  protected Decimal? _OriginalShippedQty;
  protected Decimal? _UnassignedQty;
  protected Decimal? _CompleteQtyMin;
  protected Decimal? _BaseOrigOrderQty;
  protected Decimal? _OrigOrderQty;
  protected Decimal? _OpenOrderQty;
  protected Decimal? _UnitCost;
  protected Decimal? _ExtCost;
  protected Decimal? _UnitPrice;
  protected Decimal? _DiscPct;
  protected Decimal? _LineAmt;
  protected string _AlternateID;
  protected string _TranDesc;
  protected Decimal? _UnitWeigth;
  protected Decimal? _UnitVolume;
  protected Decimal? _ExtWeight;
  protected Decimal? _ExtVolume;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _CostCodeID;
  protected string _ReasonCode;
  protected bool? _IsFree;
  protected bool? _ManualPrice;
  protected bool? _ManualDisc;
  protected bool? _IsUnassigned;
  protected ushort[] _DiscountsAppliedToLine;
  protected string _DiscountID;
  protected string _DiscountSequenceID;
  protected bool? _KeepManualFreight = new bool?(false);
  protected string _ShipComplete;
  protected Decimal? _PackedQty;
  protected Decimal? _BasePackedQty;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected bool? _HasKitComponents = new bool?(false);
  protected bool? _HasSerialComponents = new bool?(false);
  public Dictionary<SOShipLine.KitComponentKey, Decimal?> Unshipped = new Dictionary<SOShipLine.KitComponentKey, Decimal?>();
  public Dictionary<SOShipLine.KitComponentKey, Decimal?> Planned = new Dictionary<SOShipLine.KitComponentKey, Decimal?>();
  protected bool? _IsClone = new bool?(false);

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (SOShipment.shipmentNbr))]
  [PXUIField(DisplayName = "Shipment Nbr.", Visible = false, Enabled = false)]
  [PXParent(typeof (Select<SOShipment, Where<SOShipment.shipmentNbr, Equal<Current<SOShipLine.shipmentNbr>>, And<SOShipment.shipmentType, Equal<Current<SOShipLine.shipmentType>>>>>))]
  public virtual string ShipmentNbr
  {
    get => this._ShipmentNbr;
    set => this._ShipmentNbr = value;
  }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (SOShipment.shipmentType))]
  public virtual string ShipmentType
  {
    get => this._ShipmentType;
    set => this._ShipmentType = value;
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (SOShipment.lineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBInt]
  [PXDefault(typeof (SOShipment.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (SOShipment.shipDate))]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Confirmed
  {
    get => this._Confirmed;
    set => this._Confirmed = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDefault]
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public virtual string OrigOrderType
  {
    get => this._OrigOrderType;
    set => this._OrigOrderType = value;
  }

  [PXDefault]
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOShipLine.origOrderType>>>>), ValidateValue = false)]
  public virtual string OrigOrderNbr
  {
    get => this._OrigOrderNbr;
    set => this._OrigOrderNbr = value;
  }

  [PXDefault]
  [PXDBInt]
  [PXUIField(DisplayName = "Order Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? OrigLineNbr
  {
    get => this._OrigLineNbr;
    set => this._OrigLineNbr = value;
  }

  [PXDefault]
  [PXDBInt]
  [PXUIField(DisplayName = "Split Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? OrigSplitLineNbr
  {
    get => this._OrigSplitLineNbr;
    set => this._OrigSplitLineNbr = value;
  }

  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  [PXDefault]
  [PXSelector(typeof (Search<SOOrderTypeOperation.operation, Where<SOOrderTypeOperation.orderType, Equal<Current<SOShipLine.origOrderType>>>>), ValidateValue = false)]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBShort]
  [PXDefault]
  public virtual short? SOLineSign { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  public virtual string OrigPlanType { get; set; }

  [PXDBShort]
  [PXDefault(-1)]
  [PXUIField(DisplayName = "Inventory Multiplier")]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [PXDBBool]
  [PXUIField]
  public virtual bool? IsStockItem { get; set; }

  [Inventory(Enabled = false)]
  [PXForeignReference(typeof (Field<SOShipLine.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  [ConvertedInventoryItem(typeof (SOShipLine.isStockItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBBool]
  [PXDefault(typeof (SOShipment.isIntercompany))]
  public virtual bool? IsIntercompany { get; set; }

  [PXFormula(typeof (Selector<SOShipLine.operation, SOOrderTypeOperation.iNDocType>))]
  [PXString(3, IsFixed = true)]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  public virtual DateTime? TranDate => this._ShipDate;

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (Search<SOOrderTypeOperation.shipmentPlanType, Where<SOOrderTypeOperation.orderType, Equal<Current<SOShipLine.origOrderType>>, And<SOOrderTypeOperation.operation, Equal<Current<SOShipLine.operation>>>>>))]
  public virtual string PlanType
  {
    get => this._PlanType;
    set => this._PlanType = value;
  }

  [SubItem(typeof (SOShipLine.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [SiteAvail(typeof (SOShipLine.inventoryID), typeof (SOShipLine.subItemID), typeof (SOShipLine.costCenterID), Enabled = false, Visible = false)]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [SOLocationAvail(typeof (SOShipLine.inventoryID), typeof (SOShipLine.subItemID), typeof (SOShipLine.costCenterID), typeof (SOShipLine.siteID), typeof (SOShipLine.tranType), typeof (SOShipLine.invtMult))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [INLotSerialNbr(typeof (SOShipLine.inventoryID), typeof (SOShipLine.subItemID), typeof (SOShipLine.locationID), typeof (SOShipLine.costCenterID))]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [INExpireDate(typeof (SOShipLine.inventoryID))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  public virtual string OrderUOM
  {
    get => this._OrderUOM;
    set => this._OrderUOM = value;
  }

  [PXDefault]
  [INUnit(typeof (SOShipLine.inventoryID), DisplayName = "UOM")]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (SOShipLine.uOM), typeof (SOShipLine.baseShippedQty), InventoryUnitType.SalesUnit, HandleEmptyKey = true)]
  [PXUIField(DisplayName = "Shipped Qty.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ShippedQty
  {
    get => this._ShippedQty;
    set => this._ShippedQty = value;
  }

  public virtual Decimal? Qty
  {
    get => this._ShippedQty;
    set => this._ShippedQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Shipped Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseShippedQty
  {
    get => this._BaseShippedQty;
    set => this._BaseShippedQty = value;
  }

  public virtual Decimal? BaseQty
  {
    get => this._BaseShippedQty;
    set => this._BaseShippedQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOriginalShippedQty
  {
    get => this._BaseOriginalShippedQty;
    set => this._BaseOriginalShippedQty = value;
  }

  [PXCalcQuantity(typeof (SOShipLine.uOM), typeof (SOShipLine.baseOriginalShippedQty), false)]
  [PXDependsOnFields(new Type[] {typeof (SOShipLine.uOM), typeof (SOShipLine.baseOriginalShippedQty)})]
  [PXUIField]
  public virtual Decimal? OriginalShippedQty
  {
    get => this._OriginalShippedQty;
    set => this._OriginalShippedQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unassigned Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? UnassignedQty
  {
    get => this._UnassignedQty;
    set => this._UnassignedQty = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField(DisplayName = "Undership Threshold (%)", Visible = false, IsReadOnly = true)]
  public virtual Decimal? CompleteQtyMin
  {
    get => this._CompleteQtyMin;
    set => this._CompleteQtyMin = value;
  }

  [Obsolete("This DAC field has been deprecated and will be removed in a later Acumatica ERP version.")]
  [PXDBBaseQtyWithOrigQty(typeof (SOShipLine.uOM), typeof (SOShipLine.origOrderQty), typeof (SOShipLine.orderUOM), typeof (SOShipLine.baseFullOrderQty), typeof (SOShipLine.fullOrderQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOrigOrderQty
  {
    get => this._BaseOrigOrderQty;
    set => this._BaseOrigOrderQty = value;
  }

  [Obsolete("This DAC field has been deprecated and will be removed in a later Acumatica ERP version.")]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigOrderQty
  {
    get => this._OrigOrderQty;
    set => this._OrigOrderQty = value;
  }

  [Obsolete("This DAC field has been deprecated and will be removed in a later Acumatica ERP version.")]
  [PXQuantity]
  [PXFormula(typeof (Switch<Case<Where<SOShipLine.confirmed, Equal<boolTrue>, And<Where<SOShipLine.shipComplete, Equal<SOShipComplete.cancelRemainder>, Or<Sub<Mult<SOShipLine.origOrderQty, Div<SOShipLine.completeQtyMin, decimal100>>, SOShipLine.shippedQty>, LessEqual<decimal0>>>>>, decimal0>, Sub<SOShipLine.origOrderQty, SOShipLine.shippedQty>>))]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false, Visible = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OpenOrderQty
  {
    get => this._OpenOrderQty;
    set => this._OpenOrderQty = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Ordered Qty.", Enabled = false, Visible = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FullOrderQty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseFullOrderQty { get; set; }

  /// <summary>
  /// The back-ordered quantity of the related sales order line in the units of the sales order line.
  /// </summary>
  [PXDBQuantity]
  [PXUIField(DisplayName = "Open Order Qty.", Enabled = false, Visible = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FullOpenQty { get; set; }

  /// <summary>
  /// The back-ordered quantity of the related sales order line in the base unit of the item.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseFullOpenQty { get; set; }

  [PXDBPriceCost]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<SOShipLine.shippedQty, SOShipLine.unitCost>))]
  public virtual Decimal? ExtCost
  {
    get => this._ExtCost;
    set => this._ExtCost = value;
  }

  [PXDBDecimal(6)]
  public virtual Decimal? UnitPrice
  {
    get => this._UnitPrice;
    set => this._UnitPrice = value;
  }

  [PXDBDecimal(6)]
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  [PXDecimal(6)]
  public virtual Decimal? LineAmt
  {
    get => this._LineAmt;
    set => this._LineAmt = value;
  }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXUIField(DisplayName = "Unit Weight")]
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<PX.Objects.IN.InventoryItem.baseWeight, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOShipLine.inventoryID>>>>), CacheGlobal = true)]
  public virtual Decimal? UnitWeigth
  {
    get => this._UnitWeigth;
    set => this._UnitWeigth = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<PX.Objects.IN.InventoryItem.baseVolume, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOShipLine.inventoryID>>>>), CacheGlobal = true)]
  public virtual Decimal? UnitVolume
  {
    get => this._UnitVolume;
    set => this._UnitVolume = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtWeight
  {
    get => this._ExtWeight;
    set => this._ExtWeight = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtVolume
  {
    get => this._ExtVolume;
    set => this._ExtVolume = value;
  }

  [PXDBInt]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBInt]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [CostCode(ReleasedField = typeof (SOShipLine.released))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<Current<SOShipLine.tranType>, Equal<INTranType.transfer>, And<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.transfer>, Or<Current<SOShipLine.tranType>, NotEqual<INTranType.transfer>, And<PX.Objects.CS.ReasonCode.usage, In3<ReasonCodeUsages.sales, ReasonCodeUsages.issue>>>>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField(DisplayName = "Reason Code")]
  [PXForeignReference(typeof (SOShipLine.FK.ReasonCode))]
  public virtual string ReasonCode
  {
    get => this._ReasonCode;
    set => this._ReasonCode = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Free Item", Enabled = false)]
  public virtual bool? IsFree
  {
    get => this._IsFree;
    set => this._IsFree = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ManualPrice
  {
    get => this._ManualPrice;
    set => this._ManualPrice = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ManualDisc
  {
    get => this._ManualDisc;
    set => this._ManualDisc = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsUnassigned
  {
    get => this._IsUnassigned;
    set => this._IsUnassigned = value;
  }

  [PXDBPackedIntegerArray]
  public virtual ushort[] DiscountsAppliedToLine
  {
    get => this._DiscountsAppliedToLine;
    set => this._DiscountsAppliedToLine = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, Equal<DiscountType.LineDiscount>>>))]
  [PXUIField(DisplayName = "Discount Code", Visible = true, Enabled = false)]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Discount Sequence", Visible = false, Enabled = false)]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXBool]
  public virtual bool? KeepManualFreight
  {
    get => this._KeepManualFreight;
    set => this._KeepManualFreight = value;
  }

  [PXDBString(1, IsFixed = true)]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Shipping Rule", Enabled = false, Visible = false)]
  public virtual string ShipComplete
  {
    get => this._ShipComplete;
    set => this._ShipComplete = value;
  }

  [PXDBBool]
  [PXDefault]
  [PXUIRequired(typeof (BqlOperand<SOShipLine.confirmed, IBqlBool>.IsEqual<True>))]
  public bool? RequireINUpdate { get; set; }

  [PXDBQuantity(typeof (SOShipLine.uOM), typeof (SOShipLine.basePickedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Picked Qty.", Enabled = false)]
  public virtual Decimal? PickedQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BasePickedQty { get; set; }

  [PXDBQuantity(typeof (SOShipLine.uOM), typeof (SOShipLine.basePackedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Packed Qty.", Enabled = false)]
  public virtual Decimal? PackedQty
  {
    get => this._PackedQty;
    set => this._PackedQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BasePackedQty
  {
    get => this._BasePackedQty;
    set => this._BasePackedQty = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBString(2, IsFixed = true)]
  public virtual string BlanketType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Blanket SO Ref. Nbr.", Enabled = false, Visible = false)]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOShipLine.blanketType>>>>), ValidateValue = false)]
  public virtual string BlanketNbr { get; set; }

  [PXDBInt]
  public virtual int? BlanketLineNbr { get; set; }

  [PXDBInt]
  public virtual int? BlanketSplitLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsSpecialOrder { get; set; }

  [PXDBInt]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  /// <summary>
  /// Number of the group of shipment lines referencing the same <see cref="T:PX.Objects.SO.SOLine">sales order line</see> which
  /// are going to be invoiced together in the single <see cref="T:PX.Objects.AR.ARTran">invoice line</see>.
  /// </summary>
  [PXDBInt]
  public virtual int? InvoiceGroupNbr { get; set; }

  public PX.Objects.SO.Unassigned.SOShipLineSplit ToUnassignedSplit()
  {
    return new PX.Objects.SO.Unassigned.SOShipLineSplit()
    {
      ShipmentNbr = this.ShipmentNbr,
      LineNbr = this.LineNbr,
      OrigOrderType = this.OrigOrderType,
      Operation = this.Operation,
      SplitLineNbr = new int?(1),
      InventoryID = this.InventoryID,
      SiteID = this.SiteID,
      SubItemID = this.SubItemID,
      LocationID = this.LocationID,
      LotSerialNbr = this.LotSerialNbr,
      ExpireDate = this.ExpireDate,
      Qty = this.Qty,
      BaseQty = this.BaseQty,
      PickedQty = new Decimal?(0M),
      BasePickedQty = new Decimal?(0M),
      PackedQty = new Decimal?(0M),
      BasePackedQty = new Decimal?(0M),
      UOM = this.UOM,
      ShipDate = this.ShipDate,
      InvtMult = this.InvtMult,
      PlanType = this.PlanType,
      Released = this.Released,
      ProjectID = this.ProjectID,
      TaskID = this.TaskID
    };
  }

  public static SOShipLine FromSOLine(SOLine item)
  {
    SOShipLine soShipLine = new SOShipLine();
    soShipLine.OrigOrderType = item.OrderType;
    soShipLine.OrigOrderNbr = item.OrderNbr;
    soShipLine.OrigLineNbr = item.LineNbr;
    soShipLine.ShipmentType = INTranType.DocType(item.TranType);
    soShipLine.ShipmentNbr = "<NEW>";
    soShipLine.SOLineSign = item.LineSign;
    soShipLine.Operation = item.Operation;
    soShipLine.LineType = item.LineType;
    soShipLine.LineNbr = item.LineNbr;
    soShipLine.IsStockItem = item.IsStockItem;
    soShipLine.InventoryID = item.InventoryID;
    soShipLine.SubItemID = item.SubItemID;
    soShipLine.SiteID = item.SiteID;
    soShipLine.LocationID = item.LocationID;
    soShipLine.LotSerialNbr = item.LotSerialNbr;
    soShipLine.ExpireDate = item.ExpireDate;
    Decimal? orderQty = item.OrderQty;
    Decimal num = 0M;
    short? nullable;
    if (!(orderQty.GetValueOrDefault() < num & orderQty.HasValue))
    {
      nullable = item.InvtMult;
    }
    else
    {
      short? invtMult = item.InvtMult;
      nullable = invtMult.HasValue ? new short?(-invtMult.GetValueOrDefault()) : new short?();
    }
    soShipLine.InvtMult = nullable;
    soShipLine.UOM = item.UOM;
    soShipLine.ShippedQty = new Decimal?(Math.Abs(item.OrderQty.GetValueOrDefault()));
    soShipLine.ShipComplete = item.ShipComplete;
    soShipLine.ProjectID = item.ProjectID;
    soShipLine.TaskID = item.TaskID;
    return soShipLine;
  }

  public static SOShipLine FromDropShip(PX.Objects.PO.POReceiptLine receiptLine, SOLine soLine)
  {
    return new SOShipLine()
    {
      OrigOrderType = soLine.OrderType,
      OrigOrderNbr = soLine.OrderNbr,
      OrigLineNbr = soLine.LineNbr,
      ShipmentNbr = receiptLine.ReceiptNbr,
      ShipmentType = "H",
      LineType = soLine.LineType,
      LineNbr = receiptLine.LineNbr,
      InventoryID = receiptLine.InventoryID,
      SubItemID = receiptLine.SubItemID,
      SiteID = receiptLine.SiteID,
      UOM = receiptLine.UOM,
      ShippedQty = receiptLine.ReceiptQty,
      ProjectID = receiptLine.ProjectID,
      TaskID = receiptLine.TaskID
    };
  }

  public bool? HasKitComponents
  {
    get => this._HasKitComponents;
    set => this._HasKitComponents = value;
  }

  public bool? HasSerialComponents
  {
    get => this._HasSerialComponents;
    set => this._HasSerialComponents = value;
  }

  public bool? IsClone
  {
    get => this._IsClone;
    set => this._IsClone = value;
  }

  public class PK : PrimaryKeyOf<SOShipLine>.By<SOShipLine.shipmentNbr, SOShipLine.lineNbr>
  {
    public static SOShipLine Find(
      PXGraph graph,
      string shipmentNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (SOShipLine) PrimaryKeyOf<SOShipLine>.By<SOShipLine.shipmentNbr, SOShipLine.lineNbr>.FindBy(graph, (object) shipmentNbr, (object) lineNbr, options);
    }
  }

  public class UK : 
    PrimaryKeyOf<SOShipLine>.By<SOShipLine.shipmentType, SOShipLine.shipmentNbr, SOShipLine.lineNbr>
  {
    public static SOShipLine Find(
      PXGraph graph,
      string shipmentType,
      string shipmentNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (SOShipLine) PrimaryKeyOf<SOShipLine>.By<SOShipLine.shipmentType, SOShipLine.shipmentNbr, SOShipLine.lineNbr>.FindBy(graph, (object) shipmentType, (object) shipmentNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentType, SOShipment.shipmentNbr>.ForeignKeyOf<SOShipLine>.By<SOShipLine.shipmentType, SOShipLine.shipmentNbr>
    {
    }

    public class OrderShipment : 
      PrimaryKeyOf<SOOrderShipment>.By<SOOrderShipment.shipmentType, SOOrderShipment.shipmentNbr, SOOrderShipment.orderType, SOOrderShipment.orderNbr>.ForeignKeyOf<SOShipLine>.By<SOShipLine.shipmentType, SOShipLine.shipmentNbr, SOShipLine.origOrderType, SOShipLine.origOrderNbr>
    {
    }

    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOShipLine>.By<SOShipLine.origOrderType>
    {
    }

    public class OrderOperation : 
      PrimaryKeyOf<SOOrderTypeOperation>.By<SOOrderTypeOperation.orderType, SOOrderTypeOperation.operation>.ForeignKeyOf<SOShipLine>.By<SOShipLine.origOrderType, SOShipLine.operation>
    {
    }

    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOShipLine>.By<SOShipLine.origOrderType, SOShipLine.origOrderNbr>
    {
    }

    public class OrderLine : 
      PrimaryKeyOf<SOLine>.By<SOLine.orderType, SOLine.orderNbr, SOLine.lineNbr>.ForeignKeyOf<SOShipLine>.By<SOShipLine.origOrderType, SOShipLine.origOrderNbr, SOShipLine.origLineNbr>
    {
    }

    public class OrderLineSplit : 
      PrimaryKeyOf<SOLineSplit>.By<SOLineSplit.orderType, SOLineSplit.orderNbr, SOLineSplit.lineNbr, SOLineSplit.splitLineNbr>.ForeignKeyOf<SOShipLine>.By<SOShipLine.origOrderType, SOShipLine.origOrderNbr, SOShipLine.origLineNbr, SOShipLine.origSplitLineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.inventoryID, SOShipLine.subItemID, SOShipLine.siteID>
    {
    }

    public class SiteStatusByCostCenter : 
      PrimaryKeyOf<INSiteStatusByCostCenter>.By<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.inventoryID, SOShipLine.subItemID, SOShipLine.siteID, SOShipLine.costCenterID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.locationID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.inventoryID, SOShipLine.subItemID, SOShipLine.siteID, SOShipLine.locationID>
    {
    }

    public class LocationStatusByCostCenter : 
      PrimaryKeyOf<INLocationStatusByCostCenter>.By<INLocationStatusByCostCenter.inventoryID, INLocationStatusByCostCenter.subItemID, INLocationStatusByCostCenter.siteID, INLocationStatusByCostCenter.locationID, INLocationStatusByCostCenter.costCenterID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.inventoryID, SOShipLine.subItemID, SOShipLine.siteID, SOShipLine.locationID, SOShipLine.costCenterID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<SOShipLine>.By<SOShipLine.inventoryID, SOShipLine.subItemID, SOShipLine.siteID, SOShipLine.locationID, SOShipLine.lotSerialNbr>
    {
    }

    public class LotSerialStatusByCostCenter : 
      PrimaryKeyOf<INLotSerialStatusByCostCenter>.By<INLotSerialStatusByCostCenter.inventoryID, INLotSerialStatusByCostCenter.subItemID, INLotSerialStatusByCostCenter.siteID, INLotSerialStatusByCostCenter.locationID, INLotSerialStatusByCostCenter.lotSerialNbr, INLotSerialStatusByCostCenter.costCenterID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.inventoryID, SOShipLine.subItemID, SOShipLine.siteID, SOShipLine.locationID, SOShipLine.lotSerialNbr, SOShipLine.costCenterID>
    {
    }

    public class PlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<SOShipLine>.By<SOShipLine.planType>
    {
    }

    public class OrderPlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<SOShipLine>.By<SOShipLine.origPlanType>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.reasonCode>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.customerID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.projectID, SOShipLine.taskID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.costCodeID>
    {
    }

    public class Discount : 
      PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.discountID>
    {
    }

    public class DiscountSequence : 
      PrimaryKeyOf<PX.Objects.AR.DiscountSequence>.By<PX.Objects.AR.DiscountSequence.discountID, PX.Objects.AR.DiscountSequence.discountSequenceID>.ForeignKeyOf<SOShipLine>.By<SOShipLine.discountID, SOShipLine.discountSequenceID>
    {
    }

    public class BlanketOrderLink : 
      PrimaryKeyOf<SOBlanketOrderLink>.By<SOBlanketOrderLink.blanketType, SOBlanketOrderLink.blanketNbr, SOBlanketOrderLink.orderType, SOBlanketOrderLink.orderNbr>.ForeignKeyOf<SOShipLine>.By<SOShipLine.blanketType, SOShipLine.blanketNbr, SOShipLine.origOrderType, SOShipLine.origOrderNbr>
    {
    }
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.shipmentNbr>
  {
  }

  public abstract class shipmentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.shipmentType>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.sortOrder>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.customerID>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOShipLine.shipDate>
  {
  }

  public abstract class confirmed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLine.confirmed>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLine.released>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.lineType>
  {
  }

  public abstract class origOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.origOrderType>
  {
  }

  public abstract class origOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.origOrderNbr>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.origLineNbr>
  {
  }

  public abstract class origSplitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.origSplitLineNbr>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.operation>
  {
  }

  public abstract class sOLineSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOShipLine.sOLineSign>
  {
  }

  public abstract class origPlanType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.origPlanType>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOShipLine.invtMult>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLine.isStockItem>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.inventoryID>
  {
    public class InventoryBaseUnitRule : 
      PX.Objects.IN.InventoryItem.baseUnit.PreventEditIfExists<Select<SOShipLine, Where<SOShipLine.inventoryID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>, And<SOShipLine.lineType, In3<SOLineType.inventory, SOLineType.nonInventory>, And<SOShipLine.confirmed, NotEqual<True>>>>>>
    {
    }
  }

  public abstract class isIntercompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLine.isIntercompany>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.tranType>
  {
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.planType>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.locationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.lotSerialNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOShipLine.expireDate>
  {
  }

  public abstract class orderUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.orderUOM>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.uOM>
  {
  }

  public abstract class shippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.shippedQty>
  {
  }

  public abstract class baseShippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLine.baseShippedQty>
  {
  }

  public abstract class baseOriginalShippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLine.baseOriginalShippedQty>
  {
  }

  public abstract class originalShippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLine.originalShippedQty>
  {
  }

  public abstract class unassignedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.unassignedQty>
  {
  }

  public abstract class completeQtyMin : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLine.completeQtyMin>
  {
  }

  [Obsolete("This DAC field has been deprecated and will be removed in a later Acumatica ERP version.")]
  public abstract class baseOrigOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLine.baseOrigOrderQty>
  {
  }

  [Obsolete("This DAC field has been deprecated and will be removed in a later Acumatica ERP version.")]
  public abstract class origOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.origOrderQty>
  {
  }

  [Obsolete("This DAC field has been deprecated and will be removed in a later Acumatica ERP version.")]
  public abstract class openOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.openOrderQty>
  {
  }

  public abstract class fullOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.fullOrderQty>
  {
  }

  public abstract class baseFullOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLine.baseFullOrderQty>
  {
  }

  public abstract class fullOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.fullOpenQty>
  {
  }

  public abstract class baseFullOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLine.baseFullOpenQty>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.unitCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.extCost>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.unitPrice>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.discPct>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.lineAmt>
  {
  }

  public abstract class alternateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.alternateID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.tranDesc>
  {
  }

  public abstract class unitWeigth : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.unitWeigth>
  {
  }

  public abstract class unitVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.unitVolume>
  {
  }

  public abstract class extWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.extWeight>
  {
  }

  public abstract class extVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.extVolume>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.costCodeID>
  {
  }

  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.reasonCode>
  {
  }

  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLine.isFree>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLine.manualPrice>
  {
  }

  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLine.manualDisc>
  {
  }

  public abstract class isUnassigned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLine.isUnassigned>
  {
  }

  public abstract class discountAppliedToLine : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLine.discountAppliedToLine>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLine.discountSequenceID>
  {
  }

  public abstract class keepManualFreight : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipLine.keepManualFreight>
  {
  }

  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.shipComplete>
  {
  }

  public abstract class requireINUpdate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLine.requireINUpdate>
  {
  }

  public abstract class pickedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.pickedQty>
  {
  }

  public abstract class basePickedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.basePickedQty>
  {
  }

  public abstract class packedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.packedQty>
  {
  }

  public abstract class basePackedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLine.basePackedQty>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOShipLine.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOShipLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOShipLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipLine.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOShipLine.Tstamp>
  {
  }

  public abstract class blanketType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.blanketType>
  {
  }

  public abstract class blanketNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLine.blanketNbr>
  {
  }

  public abstract class blanketLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.blanketLineNbr>
  {
  }

  public abstract class blanketSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipLine.blanketSplitLineNbr>
  {
  }

  public abstract class isSpecialOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLine.isSpecialOrder>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.costCenterID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOShipLine.InvoiceGroupNbr" />
  public abstract class invoiceGroupNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLine.invoiceGroupNbr>
  {
  }

  public class KitComponentKey
  {
    public readonly int ItemID;
    public readonly int SubItemID;
    protected int _HashCode;

    public KitComponentKey(int? ItemID, int? SubItemID)
    {
      this.ItemID = ItemID.Value;
      this.SubItemID = SubItemID.Value;
      this._HashCode = this.ItemID.GetHashCode() * 397 ^ this.SubItemID.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      return obj is SOShipLine.KitComponentKey kitComponentKey && object.Equals((object) kitComponentKey.ItemID, (object) this.ItemID) && object.Equals((object) kitComponentKey.SubItemID, (object) this.SubItemID);
    }

    public override int GetHashCode() => this._HashCode;
  }
}
