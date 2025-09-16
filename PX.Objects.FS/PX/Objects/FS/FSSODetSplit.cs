// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSODetSplit
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Service Order Lot/Serial Detail")]
[Serializable]
public class FSSODetSplit : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSDetail,
  ILSMaster,
  IItemPlanMaster,
  IItemPlanSource
{
  protected 
  #nullable disable
  string _RefNbr;
  protected int? _LineNbr;
  protected int? _SplitLineNbr;
  protected int? _ParentSplitLineNbr;
  protected string _Operation;
  protected short? _InvtMult;
  protected int? _InventoryID;
  protected string _LineType;
  protected bool? _IsAllocated;
  protected bool? _IsMergeable;
  protected int? _SiteID;
  protected int? _LocationID;
  protected int? _ToSiteID;
  protected int? _SubItemID;
  protected DateTime? _ShipDate;
  protected string _ShipComplete;
  protected bool? _Completed;
  protected string _ShipmentNbr;
  protected string _LotSerialNbr;
  protected string _LotSerClassID;
  protected string _AssignedNbr;
  protected DateTime? _ExpireDate;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _BaseQty;
  protected Decimal? _ShippedQty;
  protected Decimal? _BaseShippedQty;
  protected Decimal? _ReceivedQty;
  protected Decimal? _BaseReceivedQty;
  protected Decimal? _UnreceivedQty;
  protected Decimal? _BaseUnreceivedQty;
  protected Decimal? _OpenQty;
  protected Decimal? _BaseOpenQty;
  protected DateTime? _OrderDate;
  protected string _TranType;
  protected string _PlanType;
  protected string _AllocatedPlanType;
  protected string _BackOrderPlanType;
  protected bool? _RequireShipping;
  protected bool? _RequireAllocation;
  protected bool? _RequireLocation;
  protected bool? _POCreate;
  protected bool? _POCompleted;
  protected bool? _POCancelled;
  protected string _POSource;
  protected string _FixedSource;
  protected int? _VendorID;
  protected int? _POSiteID;
  protected string _POType;
  protected string _PONbr;
  protected int? _POLineNbr;
  protected string _POReceiptType;
  protected string _POReceiptNbr;
  protected string _SOOrderType;
  protected string _SOOrderNbr;
  protected int? _SOLineNbr;
  protected int? _SOSplitLineNbr;
  protected Guid? _RefNoteID;
  protected long? _PlanID;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSServiceOrder.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (FSServiceOrder.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSSODetSplit.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<FSSODetSplit.refNbr>>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (FSSODet.lineNbr))]
  [PXParent(typeof (Select<FSSODet, Where<FSSODet.srvOrdType, Equal<Current<FSSODetSplit.srvOrdType>>, And<FSSODet.refNbr, Equal<Current<FSSODetSplit.refNbr>>, And<FSSODet.lineNbr, Equal<Current<FSSODetSplit.lineNbr>>>>>>))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSServiceOrder.splitLineCntr), ValidateLineCounterCalculation = true)]
  [PXUIField(DisplayName = "Allocation ID", Visible = false, IsReadOnly = true)]
  public virtual int? SplitLineNbr
  {
    get => this._SplitLineNbr;
    set => this._SplitLineNbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Parent Allocation ID", Visible = false, IsReadOnly = true)]
  public virtual int? ParentSplitLineNbr
  {
    get => this._ParentSplitLineNbr;
    set => this._ParentSplitLineNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (FSSODet.operation))]
  [PXSelectorMarker(typeof (Search<SOOrderTypeOperation.operation, Where<SOOrderTypeOperation.orderType, Equal<Current<FSSrvOrdType.allocationOrderType>>>>))]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBShort]
  [PXDefault(typeof (FSSODet.invtMult))]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [Inventory(Enabled = false, Visible = true)]
  [PXDefault(typeof (FSSODet.inventoryID))]
  [PXForeignReference(typeof (FSSODetSplit.FK.InventoryItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (Selector<FSSODetSplit.inventoryID, Switch<Case<Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>, Or<PX.Objects.IN.InventoryItem.kitItem, Equal<True>>>, SOLineType.inventory, Case<Where<PX.Objects.IN.InventoryItem.nonStockShip, Equal<True>>, SOLineType.nonInventory>>, SOLineType.miscCharge>>))]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDBBool]
  [PXFormula(typeof (Selector<FSSODetSplit.inventoryID, PX.Objects.IN.InventoryItem.stkItem>))]
  public virtual bool? IsStockItem { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allocated")]
  public virtual bool? IsAllocated
  {
    get => this._IsAllocated;
    set => this._IsAllocated = value;
  }

  [PXBool]
  [PXFormula(typeof (True))]
  public virtual bool? IsMergeable
  {
    get => this._IsMergeable;
    set => this._IsMergeable = value;
  }

  [SiteAvail(typeof (FSSODetSplit.inventoryID), typeof (FSSODetSplit.subItemID), typeof (FSSODetSplit.costCenterID), new Type[] {typeof (PX.Objects.IN.INSite.siteCD), typeof (INSiteStatusByCostCenter.qtyOnHand), typeof (INSiteStatusByCostCenter.qtyAvail), typeof (PX.Objects.IN.INSite.descr)}, DisplayName = "Alloc. Warehouse")]
  [PXFormula(typeof (Switch<Case<Where<FSSODetSplit.isAllocated, Equal<False>>, Current<FSSODet.siteID>>, FSSODetSplit.siteID>))]
  [PXForeignReference(typeof (FSSODetSplit.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [LocationAvail(typeof (FSSODetSplit.inventoryID), typeof (FSSODetSplit.subItemID), typeof (FSSODetSplit.costCenterID), typeof (FSSODetSplit.siteID), typeof (FSSODetSplit.tranType), typeof (FSSODetSplit.invtMult))]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [Site(DisplayName = "Orig. Warehouse")]
  [PXDefault(typeof (FSSODet.siteID))]
  public virtual int? ToSiteID
  {
    get => this._ToSiteID;
    set => this._ToSiteID = value;
  }

  [SubItem(typeof (FSSODetSplit.inventoryID))]
  [PXDefault]
  [SubItemStatusVeryfier(typeof (FSSODetSplit.inventoryID), typeof (FSSODetSplit.siteID), new string[] {"IN", "NS"})]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (FSServiceOrder.orderDate))]
  [PXUIField]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (FSSODet.shipComplete))]
  public virtual string ShipComplete
  {
    get => this._ShipComplete;
    set => this._ShipComplete = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Completed", Enabled = false)]
  public virtual bool? Completed
  {
    get => this._Completed;
    set => this._Completed = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Shipment Nbr.", Enabled = false)]
  public virtual string ShipmentNbr
  {
    get => this._ShipmentNbr;
    set => this._ShipmentNbr = value;
  }

  [SOLotSerialNbr(typeof (FSSODetSplit.inventoryID), typeof (FSSODetSplit.subItemID), typeof (FSSODetSplit.locationID), typeof (FSSODet.lotSerialNbr), typeof (FSSODetSplit.costCenterID))]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXString(10, IsUnicode = true)]
  public virtual string LotSerClassID
  {
    get => this._LotSerClassID;
    set => this._LotSerClassID = value;
  }

  [PXString(30, IsUnicode = true)]
  public virtual string AssignedNbr
  {
    get => this._AssignedNbr;
    set => this._AssignedNbr = value;
  }

  [INExpireDate(typeof (FSSODetSplit.inventoryID))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [INUnit(typeof (FSSODetSplit.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXDefault(typeof (FSSODet.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (FSSODetSplit.uOM), typeof (FSSODetSplit.baseQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBDecimal(6)]
  public virtual Decimal? BaseQty
  {
    get => this._BaseQty;
    set => this._BaseQty = value;
  }

  [PXDBQuantity(typeof (FSSODetSplit.uOM), typeof (FSSODetSplit.baseShippedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Shipments", Enabled = false)]
  public virtual Decimal? ShippedQty
  {
    get => this._ShippedQty;
    set => this._ShippedQty = value;
  }

  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseShippedQty
  {
    get => this._BaseShippedQty;
    set => this._BaseShippedQty = value;
  }

  [PXDBQuantity(typeof (FSSODetSplit.uOM), typeof (FSSODetSplit.baseReceivedQty), MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Received", Enabled = false)]
  public virtual Decimal? ReceivedQty
  {
    get => this._ReceivedQty;
    set => this._ReceivedQty = value;
  }

  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseReceivedQty
  {
    get => this._BaseReceivedQty;
    set => this._BaseReceivedQty = value;
  }

  [PXQuantity(typeof (FSSODetSplit.uOM), typeof (FSSODetSplit.baseUnreceivedQty), MinValue = 0.0)]
  [PXFormula(typeof (Sub<FSSODetSplit.qty, FSSODetSplit.receivedQty>))]
  public virtual Decimal? UnreceivedQty
  {
    get => this._UnreceivedQty;
    set => this._UnreceivedQty = value;
  }

  [PXDecimal(6, MinValue = 0.0)]
  [PXFormula(typeof (Sub<FSSODetSplit.baseQty, FSSODetSplit.baseReceivedQty>))]
  public virtual Decimal? BaseUnreceivedQty
  {
    get => this._BaseUnreceivedQty;
    set => this._BaseUnreceivedQty = value;
  }

  [PXQuantity(typeof (FSSODetSplit.uOM), typeof (FSSODetSplit.baseOpenQty), MinValue = 0.0)]
  [PXFormula(typeof (Sub<FSSODetSplit.qty, FSSODetSplit.shippedQty>))]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXDecimal(6, MinValue = 0.0)]
  [PXFormula(typeof (Sub<FSSODetSplit.baseQty, FSSODetSplit.baseShippedQty>))]
  public virtual Decimal? BaseOpenQty
  {
    get => this._BaseOpenQty;
    set => this._BaseOpenQty = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (FSServiceOrder.orderDate))]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [PXFormula(typeof (Selector<FSSODetSplit.operation, SOOrderTypeOperation.iNDocType>))]
  [PXString(3, IsFixed = true)]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  public virtual DateTime? TranDate => this._OrderDate;

  [PXString(2, IsFixed = true)]
  [PXDBScalar(typeof (Search<INPlanType.planType, Where<INPlanType.inclQtyFSSrvOrdBooked, Equal<True>>>))]
  [PXDefault(typeof (Search<INPlanType.planType, Where<INPlanType.inclQtyFSSrvOrdBooked, Equal<True>>>))]
  public virtual string PlanType
  {
    get => this._PlanType;
    set => this._PlanType = value;
  }

  [PXDBScalar(typeof (Search<INPlanType.planType, Where<INPlanType.inclQtyFSSrvOrdAllocated, Equal<True>>>))]
  [PXDefault(typeof (Search<INPlanType.planType, Where<INPlanType.inclQtyFSSrvOrdAllocated, Equal<True>>>))]
  public virtual string AllocatedPlanType
  {
    get => this._AllocatedPlanType;
    set => this._AllocatedPlanType = value;
  }

  [PXDBScalar(typeof (Search<INPlanType.planType, Where<INPlanType.inclQtySOBackOrdered, Equal<True>>>))]
  [PXDefault(typeof (Search<INPlanType.planType, Where<INPlanType.inclQtySOBackOrdered, Equal<True>>>))]
  public virtual string BackOrderPlanType
  {
    get => this._BackOrderPlanType;
    set => this._BackOrderPlanType = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  public virtual string OrigPlanType { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Search<PX.Objects.SO.SOOrderType.requireShipping, Where<PX.Objects.SO.SOOrderType.orderType, Equal<Current<FSSrvOrdType.allocationOrderType>>>>))]
  public virtual bool? RequireShipping
  {
    get => this._RequireShipping;
    set => this._RequireShipping = value;
  }

  [PXBool]
  [PXUnboundDefault(typeof (Search<PX.Objects.SO.SOOrderType.requireAllocation, Where<PX.Objects.SO.SOOrderType.orderType, Equal<Current<FSSrvOrdType.allocationOrderType>>>>))]
  public virtual bool? RequireAllocation
  {
    get => this._RequireAllocation;
    set => this._RequireAllocation = value;
  }

  [PXBool]
  [PXUnboundDefault(typeof (Search<PX.Objects.SO.SOOrderType.requireLocation, Where<PX.Objects.SO.SOOrderType.orderType, Equal<Current<FSSrvOrdType.allocationOrderType>>>>))]
  public virtual bool? RequireLocation
  {
    get => this._RequireLocation;
    set => this._RequireLocation = value;
  }

  [PXDBBool]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<FSSODetSplit.isAllocated, Equal<False>, And<FSSODetSplit.pOReceiptNbr, IsNull>>, Current<FSSODet.pOCreate>>, False>))]
  [PXUIField(DisplayName = "Mark for PO", Visible = true, Enabled = false)]
  public virtual bool? POCreate
  {
    get => this._POCreate;
    set => this._POCreate = new bool?(value.GetValueOrDefault());
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? POCompleted
  {
    get => this._POCompleted;
    set => this._POCompleted = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? POCancelled
  {
    get => this._POCancelled;
    set => this._POCancelled = value;
  }

  [PXDBString]
  [PXFormula(typeof (Switch<Case<Where<FSSODetSplit.isAllocated, Equal<False>>, Current<FSSODet.pOSource>>, Null>))]
  public virtual string POSource
  {
    get => this._POSource;
    set => this._POSource = value;
  }

  [PXString(1, IsFixed = true)]
  [PXDBCalced(typeof (Switch<Case<Where<FSSODetSplit.pOCreate, Equal<True>>, INReplenishmentSource.purchased, Case<Where<FSSODetSplit.siteID, NotEqual<FSSODetSplit.toSiteID>>, INReplenishmentSource.transfer>>, INReplenishmentSource.none>), typeof (string))]
  public virtual string FixedSource
  {
    get => this._FixedSource;
    set => this._FixedSource = value;
  }

  [PXDBInt]
  [PXFormula(typeof (Switch<Case<Where<FSSODetSplit.isAllocated, Equal<False>>, Current<FSSODet.vendorID>>, Null>))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt]
  [PXFormula(typeof (Switch<Case<Where<FSSODetSplit.isAllocated, Equal<False>>, Current<FSSODet.pOSiteID>>, Null>))]
  public virtual int? POSiteID
  {
    get => this._POSiteID;
    set => this._POSiteID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "PO Type", Enabled = false)]
  [POOrderType.RBDList]
  public virtual string POType
  {
    get => this._POType;
    set => this._POType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<FSSODetSplit.pOType>>>>), DescriptionField = typeof (PX.Objects.PO.POOrder.orderDesc))]
  public virtual string PONbr
  {
    get => this._PONbr;
    set => this._PONbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "PO Line Nbr.", Enabled = false)]
  public virtual int? POLineNbr
  {
    get => this._POLineNbr;
    set => this._POLineNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "PO Receipt Type", Enabled = false)]
  public virtual string POReceiptType
  {
    get => this._POReceiptType;
    set => this._POReceiptType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Receipt Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<PX.Objects.PO.POReceipt.receiptType, Equal<Current<FSSODetSplit.pOReceiptType>>>>), DescriptionField = typeof (PX.Objects.PO.POReceipt.invoiceNbr))]
  public virtual string POReceiptNbr
  {
    get => this._POReceiptNbr;
    set => this._POReceiptNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  [PXDBInt]
  public virtual int? SOLineNbr
  {
    get => this._SOLineNbr;
    set => this._SOLineNbr = value;
  }

  [PXDBInt]
  public virtual int? SOSplitLineNbr
  {
    get => this._SOSplitLineNbr;
    set => this._SOSplitLineNbr = value;
  }

  [PXUIField(DisplayName = "Related Document", Enabled = false)]
  [FSSODetSplit.PXRefNote]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXDBLong(IsImmutable = true)]
  public virtual long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  [PXFormula(typeof (Selector<FSSODetSplit.locationID, INLocation.projectID>))]
  [PXInt]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXFormula(typeof (Selector<FSSODetSplit.locationID, INLocation.taskID>))]
  [PXInt]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  /// <exclude />
  [PXDBInt]
  [PXDefault(typeof (PX.Objects.IN.CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (FSSODet.curyInfoID))]
  [PXDefault(typeof (FSSODet.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (FSSODetSplit.curyInfoID), typeof (FSSODetSplit.unitCost))]
  [PXUIField]
  [PXDefault(typeof (FSSODet.curyUnitCost))]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBPriceCost]
  public virtual Decimal? UnitCost { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (FSSODetSplit.curyInfoID), typeof (FSSODetSplit.extCost))]
  [PXUIField(DisplayName = "Ext. Cost", Enabled = false)]
  [PXFormula(typeof (Mult<FSSODetSplit.curyUnitCost, FSSODetSplit.qty>))]
  public virtual Decimal? CuryExtCost { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBPriceCost]
  public virtual Decimal? ExtCost { get; set; }

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

  bool? ILSMaster.IsIntercompany => new bool?(false);

  public class PK : 
    PrimaryKeyOf<FSSODetSplit>.By<FSSODetSplit.srvOrdType, FSSODetSplit.refNbr, FSSODetSplit.lineNbr, FSSODetSplit.splitLineNbr>
  {
    public static FSSODetSplit Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      int? splitLineNbr,
      PKFindOptions options = 0)
    {
      return (FSSODetSplit) PrimaryKeyOf<FSSODetSplit>.By<FSSODetSplit.srvOrdType, FSSODetSplit.refNbr, FSSODetSplit.lineNbr, FSSODetSplit.splitLineNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, (object) splitLineNbr, options);
    }
  }

  public static class FK
  {
    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.srvOrdType, FSSODetSplit.refNbr>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.srvOrdType>
    {
    }

    public class SrvOrdLine : 
      PrimaryKeyOf<FSSODet>.By<FSSODet.srvOrdType, FSSODet.refNbr, FSSODet.lineNbr>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.srvOrdType, FSSODetSplit.refNbr, FSSODetSplit.lineNbr>
    {
    }

    public class ParenLineSplit : 
      PrimaryKeyOf<FSSODetSplit>.By<FSSODetSplit.srvOrdType, FSSODetSplit.refNbr, FSSODetSplit.lineNbr, FSSODetSplit.splitLineNbr>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.srvOrdType, FSSODetSplit.refNbr, FSSODetSplit.lineNbr, FSSODetSplit.parentSplitLineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.inventoryID, FSSODetSplit.subItemID, FSSODetSplit.siteID>
    {
    }

    public class ToSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.toSiteID>
    {
    }

    public class ToSiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.inventoryID, FSSODetSplit.subItemID, FSSODetSplit.toSiteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.locationID>
    {
    }

    public class CostCenter : 
      PrimaryKeyOf<INCostCenter>.By<INCostCenter.costCenterID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.costCenterID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.inventoryID, FSSODetSplit.subItemID, FSSODetSplit.siteID, FSSODetSplit.locationID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.inventoryID, FSSODetSplit.subItemID, FSSODetSplit.siteID, FSSODetSplit.locationID, FSSODetSplit.lotSerialNbr>
    {
    }

    public class Shipment : 
      PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentNbr>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.shipmentNbr>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.vendorID>
    {
    }

    public class POSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.pOSiteID>
    {
    }

    public class POOrder : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.pOType, FSSODetSplit.pONbr>
    {
    }

    public class POLine : 
      PrimaryKeyOf<PX.Objects.PO.POLine>.By<PX.Objects.PO.POLine.orderType, PX.Objects.PO.POLine.orderNbr, PX.Objects.PO.POLine.lineNbr>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.pOType, FSSODetSplit.pONbr, FSSODetSplit.pOLineNbr>
    {
    }

    public class POReceipt : 
      PrimaryKeyOf<PX.Objects.PO.POReceipt>.By<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.pOReceiptType, FSSODetSplit.pOReceiptNbr>
    {
    }

    public class ItemPlan : 
      PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.ForeignKeyOf<FSSODetSplit>.By<FSSODetSplit.planID>
    {
    }
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.lineNbr>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.splitLineNbr>
  {
  }

  public abstract class parentSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSODetSplit.parentSplitLineNbr>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.operation>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSODetSplit.invtMult>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.inventoryID>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.lineType>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODetSplit.isStockItem>
  {
  }

  public abstract class isAllocated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODetSplit.isAllocated>
  {
  }

  public abstract class isMergeable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODetSplit.isMergeable>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.locationID>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.toSiteID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.subItemID>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSSODetSplit.shipDate>
  {
  }

  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.shipComplete>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODetSplit.completed>
  {
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.shipmentNbr>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.lotSerialNbr>
  {
  }

  public abstract class lotSerClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.lotSerClassID>
  {
  }

  public abstract class assignedNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.assignedNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSSODetSplit.expireDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODetSplit.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODetSplit.baseQty>
  {
  }

  public abstract class shippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODetSplit.shippedQty>
  {
  }

  public abstract class baseShippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODetSplit.baseShippedQty>
  {
  }

  public abstract class receivedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODetSplit.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODetSplit.baseReceivedQty>
  {
  }

  public abstract class unreceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODetSplit.unreceivedQty>
  {
  }

  public abstract class baseUnreceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODetSplit.baseUnreceivedQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODetSplit.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODetSplit.baseOpenQty>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSSODetSplit.orderDate>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.tranType>
  {
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.planType>
  {
  }

  public abstract class allocatedPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODetSplit.allocatedPlanType>
  {
  }

  public abstract class backOrderPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODetSplit.backOrderPlanType>
  {
  }

  public abstract class origPlanType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.origPlanType>
  {
  }

  public abstract class requireShipping : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODetSplit.requireShipping>
  {
  }

  public abstract class requireAllocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSODetSplit.requireAllocation>
  {
  }

  public abstract class requireLocation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODetSplit.requireLocation>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODetSplit.pOCreate>
  {
  }

  public abstract class pOCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODetSplit.pOCompleted>
  {
  }

  public abstract class pOCancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODetSplit.pOCancelled>
  {
  }

  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.pOSource>
  {
  }

  public abstract class fixedSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.fixedSource>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.vendorID>
  {
  }

  public abstract class pOSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.pOSiteID>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.pOLineNbr>
  {
  }

  public abstract class pOReceiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.pOReceiptNbr>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetSplit.sOOrderNbr>
  {
  }

  public abstract class sOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.sOLineNbr>
  {
  }

  public abstract class sOSplitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.sOSplitLineNbr>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSODetSplit.refNoteID>
  {
  }

  public class PXRefNoteAttribute : PXRefNoteBaseAttribute
  {
    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      // ISSUE: method pointer
      PXButtonDelegate pxButtonDelegate = new PXButtonDelegate((object) this, __methodptr(\u003CCacheAttached\u003Eb__1_0));
      string str = $"{sender.GetItemType().Name}${((PXEventSubscriberAttribute) this)._FieldName}$Link";
      sender.Graph.Actions[str] = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(typeof (FSServiceOrder)), (object) sender.Graph, (object) str, (object) pxButtonDelegate, (object) new PXEventSubscriberAttribute[1]
      {
        (PXEventSubscriberAttribute) new PXUIFieldAttribute()
        {
          MapEnableRights = (PXCacheRights) 1
        }
      });
    }

    public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      if (e.Row is FSSODetSplit row && !string.IsNullOrEmpty(row.PONbr))
      {
        e.ReturnValue = this.GetEntityRowID(sender.Graph.Caches[typeof (PX.Objects.PO.POOrder)], new object[2]
        {
          (object) row.POType,
          (object) row.PONbr
        });
        e.ReturnState = (object) PXRefNoteBaseAttribute.PXLinkState.CreateInstance(e.ReturnState, typeof (PX.Objects.PO.POOrder), new object[2]
        {
          (object) row.POType,
          (object) row.PONbr
        });
      }
      else if (row != null && !string.IsNullOrEmpty(row.ShipmentNbr))
      {
        e.ReturnValue = this.GetEntityRowID(sender.Graph.Caches[typeof (PX.Objects.SO.SOShipment)], new object[1]
        {
          (object) row.ShipmentNbr
        });
        e.ReturnState = (object) PXRefNoteBaseAttribute.PXLinkState.CreateInstance(e.ReturnState, typeof (PX.Objects.SO.SOShipment), new object[1]
        {
          (object) row.ShipmentNbr
        });
      }
      else if (row != null && !string.IsNullOrEmpty(row.SOOrderNbr))
      {
        e.ReturnValue = this.GetEntityRowID(sender.Graph.Caches[typeof (PX.Objects.SO.SOOrder)], new object[2]
        {
          (object) row.SOOrderType,
          (object) row.SOOrderNbr
        });
        e.ReturnState = (object) PXRefNoteBaseAttribute.PXLinkState.CreateInstance(e.ReturnState, typeof (PX.Objects.SO.SOOrder), new object[2]
        {
          (object) row.SOOrderType,
          (object) row.SOOrderNbr
        });
      }
      else if (row != null && !string.IsNullOrEmpty(row.POReceiptNbr))
      {
        e.ReturnValue = this.GetEntityRowID(sender.Graph.Caches[typeof (PX.Objects.PO.POReceipt)], new object[2]
        {
          (object) row.POReceiptType,
          (object) row.POReceiptNbr
        });
        e.ReturnState = (object) PXRefNoteBaseAttribute.PXLinkState.CreateInstance(e.ReturnState, typeof (PX.Objects.PO.POReceipt), new object[2]
        {
          (object) row.POReceiptType,
          (object) row.POReceiptNbr
        });
      }
      else
        base.FieldSelecting(sender, e);
    }
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSSODetSplit.planID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.taskID>
  {
  }

  /// <exclude />
  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetSplit.costCenterID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSSODetSplit.curyInfoID>
  {
  }

  public abstract class curyUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODetSplit.curyUnitCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODetSplit.unitCost>
  {
  }

  public abstract class curyExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODetSplit.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODetSplit.extCost>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSODetSplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODetSplit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSODetSplit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSSODetSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODetSplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSODetSplit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSSODetSplit.Tstamp>
  {
  }
}
