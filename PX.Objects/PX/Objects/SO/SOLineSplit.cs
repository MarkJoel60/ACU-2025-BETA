// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Sales Order Line Split")]
[Serializable]
public class SOLineSplit : 
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
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected int? _SplitLineNbr;
  protected int? _ParentSplitLineNbr;
  protected string _Operation;
  protected short? _InvtMult;
  protected bool? _RequireShipping;
  protected bool? _RequireAllocation;
  protected bool? _RequireLocation;
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
  protected string _BackOrderPlanType;
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

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (SOOrder.orderType))]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType>), CacheGlobal = true)]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  [PXParent(typeof (SOLineSplit.FK.Order))]
  [PXParent(typeof (SOLineSplit.FK.OrderLine))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (SOLine.lineNbr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (SOOrder.lineCntr))]
  [PXUIField(DisplayName = "Allocation ID", Visible = false, IsReadOnly = true, Enabled = false)]
  public virtual int? SplitLineNbr
  {
    get => this._SplitLineNbr;
    set => this._SplitLineNbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Parent Allocation ID", Visible = false, IsReadOnly = true, Enabled = false)]
  public virtual int? ParentSplitLineNbr
  {
    get => this._ParentSplitLineNbr;
    set => this._ParentSplitLineNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (SOLine.behavior))]
  public virtual string Behavior { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SOLine.operation))]
  [PXSelectorMarker(typeof (Search<SOOrderTypeOperation.operation, Where<SOOrderTypeOperation.orderType, Equal<Current<SOLineSplit.orderType>>>>))]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBShort]
  [PXDefault(typeof (INTran.invtMult))]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [PXBool]
  [PXFormula(typeof (Selector<SOLineSplit.orderType, PX.Objects.SO.SOOrderType.requireShipping>))]
  public virtual bool? RequireShipping
  {
    get => this._RequireShipping;
    set => this._RequireShipping = value;
  }

  [PXBool]
  [PXFormula(typeof (Selector<SOLineSplit.orderType, PX.Objects.SO.SOOrderType.requireAllocation>))]
  public virtual bool? RequireAllocation
  {
    get => this._RequireAllocation;
    set => this._RequireAllocation = value;
  }

  [PXBool]
  [PXFormula(typeof (Selector<SOLineSplit.orderType, PX.Objects.SO.SOOrderType.requireLocation>))]
  public virtual bool? RequireLocation
  {
    get => this._RequireLocation;
    set => this._RequireLocation = value;
  }

  [Inventory(Enabled = false, Visible = true)]
  [PXDefault(typeof (SOLine.inventoryID))]
  [PXForeignReference(typeof (SOLineSplit.FK.InventoryItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (SOLine.lineType))]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDBBool]
  [PXFormula(typeof (Selector<SOLineSplit.inventoryID, PX.Objects.IN.InventoryItem.stkItem>))]
  public bool? IsStockItem { get; set; }

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

  [SiteAvail(typeof (SOLineSplit.inventoryID), typeof (SOLineSplit.subItemID), typeof (SOLineSplit.costCenterID), new Type[] {typeof (PX.Objects.IN.INSite.siteCD), typeof (INSiteStatusByCostCenter.qtyOnHand), typeof (INSiteStatusByCostCenter.qtyAvail), typeof (INSiteStatusByCostCenter.active), typeof (PX.Objects.IN.INSite.descr)}, DisplayName = "Alloc. Warehouse", DocumentBranchType = typeof (SOOrder.branchID))]
  [PXFormula(typeof (Switch<Case<Where<SOLineSplit.isAllocated, Equal<False>>, Current<SOLine.siteID>>, SOLineSplit.siteID>))]
  [PXDefault]
  [PXUIRequired(typeof (IIf<Where<SOLineSplit.lineType, NotEqual<SOLineType.miscCharge>>, True, False>))]
  [PXForeignReference(typeof (Field<SOLineSplit.siteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>))]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<SOOrder.branchID>>>))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [SOLocationAvail(typeof (SOLineSplit.inventoryID), typeof (SOLineSplit.subItemID), typeof (SOLine.costCenterID), typeof (SOLineSplit.siteID), typeof (SOLineSplit.tranType), typeof (SOLineSplit.invtMult))]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [Site(DisplayName = "Orig. Warehouse")]
  [PXDefault(typeof (SOLine.siteID))]
  [PXUIRequired(typeof (IIf<Where<SOLineSplit.lineType, NotEqual<SOLineType.miscCharge>>, True, False>))]
  public virtual int? ToSiteID
  {
    get => this._ToSiteID;
    set => this._ToSiteID = value;
  }

  [SubItem(typeof (SOLineSplit.inventoryID))]
  [PXDefault]
  [SubItemStatusVeryfier(typeof (SOLineSplit.inventoryID), typeof (SOLineSplit.siteID), new string[] {"IN", "NS"})]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (SOLine.shipDate))]
  [PXUIField]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SOLine.shipComplete))]
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

  [PXDBInt]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  [SOLotSerialNbrAttribute.SOAllocationLotSerialNbr(typeof (SOLineSplit.inventoryID), typeof (SOLineSplit.subItemID), typeof (SOLineSplit.siteID), typeof (SOLineSplit.locationID), typeof (SOLine.lotSerialNbr), typeof (SOLineSplit.costCenterID))]
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

  [INExpireDate(typeof (SOLineSplit.inventoryID))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [INUnit(typeof (SOLineSplit.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXDefault(typeof (SOLine.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (SOLineSplit.uOM), typeof (SOLineSplit.baseQty), MinValue = 0.0)]
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

  [PXDBQuantity(typeof (SOLineSplit.uOM), typeof (SOLineSplit.baseShippedQty))]
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

  /// <summary>
  /// For regular splits is 0. For Blanket order splits gets or sets quantity of items in <see cref="P:PX.Objects.SO.SOLineSplit.UOM" /> that were transferred to child orders.
  /// </summary>
  [PXDBQuantity(typeof (SOLineSplit.uOM), typeof (SOLineSplit.baseClosedQty), MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClosedQty { get; set; }

  /// <summary>
  /// For regular splits is 0. For Blanket order splits gets or sets quantity of items in base units that were transferred to child orders.
  /// </summary>
  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseClosedQty { get; set; }

  [PXDBQuantity(typeof (SOLineSplit.uOM), typeof (SOLineSplit.baseReceivedQty), MinValue = 0.0)]
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

  [PXQuantity(typeof (SOLineSplit.uOM), typeof (SOLineSplit.baseUnreceivedQty), MinValue = 0.0)]
  [PXFormula(typeof (Sub<SOLineSplit.qty, SOLineSplit.receivedQty>))]
  public virtual Decimal? UnreceivedQty
  {
    get => this._UnreceivedQty;
    set => this._UnreceivedQty = value;
  }

  [PXDecimal(6, MinValue = 0.0)]
  [PXFormula(typeof (Sub<SOLineSplit.baseQty, SOLineSplit.baseReceivedQty>))]
  public virtual Decimal? BaseUnreceivedQty
  {
    get => this._BaseUnreceivedQty;
    set => this._BaseUnreceivedQty = value;
  }

  [PXQuantity(typeof (SOLineSplit.uOM), typeof (SOLineSplit.baseOpenQty), MinValue = 0.0)]
  [PXFormula(typeof (Sub<SOLineSplit.qty, SOLineSplit.shippedQty>))]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXDecimal(6, MinValue = 0.0)]
  [PXFormula(typeof (Sub<SOLineSplit.baseQty, SOLineSplit.baseShippedQty>))]
  public virtual Decimal? BaseOpenQty
  {
    get => this._BaseOpenQty;
    set => this._BaseOpenQty = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (SOOrder.orderDate))]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [PXFormula(typeof (Selector<SOLineSplit.operation, SOOrderTypeOperation.iNDocType>))]
  [PXString(3, IsFixed = true)]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  public virtual DateTime? TranDate => this._OrderDate;

  [PXFormula(typeof (Selector<SOLineSplit.operation, SOOrderTypeOperation.orderPlanType>))]
  [PXString(2, IsFixed = true)]
  public virtual string PlanType
  {
    get => this._PlanType;
    set => this._PlanType = value;
  }

  [PXFormula(typeof (INPlanConstants.plan61))]
  public virtual string AllocatedPlanType { get; set; }

  [PXFormula(typeof (INPlanConstants.plan68))]
  public virtual string BackOrderPlanType
  {
    get => this._BackOrderPlanType;
    set => this._BackOrderPlanType = value;
  }

  [PXFormula(typeof (INPlanConstants.plan60))]
  public virtual string BookedPlanType { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  public virtual string OrigPlanType { get; set; }

  [PXDBBool]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<SOLineSplit.isAllocated, Equal<False>, And<SOLineSplit.pOReceiptNbr, IsNull>>, Current<SOLine.pOCreate>>, False>))]
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
  [PXFormula(typeof (Switch<Case<Where<SOLineSplit.isAllocated, Equal<False>>, Current<SOLine.pOSource>>, Null>))]
  public virtual string POSource
  {
    get => this._POSource;
    set => this._POSource = value;
  }

  [PXString(1, IsFixed = true)]
  [PXDBCalced(typeof (Switch<Case<Where<SOLineSplit.pOCreate, Equal<True>>, INReplenishmentSource.purchased, Case<Where<SOLineSplit.aMProdCreate, Equal<True>>, INReplenishmentSource.manufactured, Case<Where<SOLineSplit.siteID, NotEqual<SOLineSplit.toSiteID>>, INReplenishmentSource.transfer>>>, INReplenishmentSource.none>), typeof (string))]
  public virtual string FixedSource
  {
    get => this._FixedSource;
    set => this._FixedSource = value;
  }

  [PXDBInt]
  [PXFormula(typeof (Switch<Case<Where<SOLineSplit.isAllocated, Equal<False>>, Current<SOLine.vendorID>>, Null>))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt]
  [PXFormula(typeof (Switch<Case<Where<SOLineSplit.isAllocated, Equal<False>>, Current<SOLine.pOSiteID>>, Null>))]
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
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<SOLineSplit.pOType>>>>), DescriptionField = typeof (PX.Objects.PO.POOrder.orderDesc))]
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
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<PX.Objects.PO.POReceipt.receiptType, Equal<Current<SOLineSplit.pOReceiptType>>>>), DescriptionField = typeof (PX.Objects.PO.POReceipt.invoiceNbr))]
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
  [SOLineSplit.PXRefNote]
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

  [PXInt]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXInt]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  bool? ILSMaster.IsIntercompany => new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Mark for Production", Enabled = false)]
  public bool? AMProdCreate { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Customer Order Nbr.")]
  public virtual string CustomerOrderNbr { get; set; }

  [PXDBDate]
  [PXDefault(typeof (IsNull<Current<SOLine.schedOrderDate>, Current<AccessInfo.businessDate>>))]
  [PXUIField(DisplayName = "Sched. Order Date")]
  public virtual DateTime? SchedOrderDate { get; set; }

  [PXDBDate]
  [PXDefault(typeof (SOLine.schedShipDate))]
  [PXUIField(DisplayName = "Sched. Shipment Date")]
  public virtual DateTime? SchedShipDate { get; set; }

  [PXDBDate]
  [PXDefault(typeof (IsNull<Current<SOLine.pOCreateDate>, Current<AccessInfo.businessDate>>))]
  [PXUIField(DisplayName = "PO Creation Date")]
  public virtual DateTime? POCreateDate { get; set; }

  [PXDBQuantity(typeof (SOLineSplit.uOM), typeof (SOLineSplit.baseQtyOnOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Orders", Enabled = false)]
  public virtual Decimal? QtyOnOrders { get; set; }

  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQtyOnOrders { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<SOLineSplit.lineType, NotEqual<SOLineType.miscCharge>, And<SOLineSplit.completed, Equal<False>>>, Sub<SOLineSplit.qty, Add<SOLineSplit.qtyOnOrders, SOLineSplit.receivedQty>>>, decimal0>), typeof (Decimal))]
  [PXFormula(typeof (Switch<Case<Where<SOLineSplit.lineType, NotEqual<SOLineType.miscCharge>, And<SOLineSplit.completed, Equal<False>>>, Sub<SOLineSplit.qty, Add<SOLineSplit.qtyOnOrders, SOLineSplit.receivedQty>>>, decimal0>))]
  [PXDefault]
  [PXUIField(DisplayName = "Blanket Open Qty.", Enabled = false)]
  public virtual Decimal? BlanketOpenQty { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ChildLineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? EffectiveChildLineCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? OpenChildLineCntr { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the <see cref="T:PX.Objects.SO.SOLineSplit" /> record was orchestrated.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Orchestrated", Enabled = false, FieldClass = "OrderOrchestration")]
  public bool? IsOrchestratedLine { get; set; }

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

  public static SOLineSplit FromSOLine(SOLine item)
  {
    SOLineSplit soLineSplit = new SOLineSplit();
    soLineSplit.OrderType = item.OrderType;
    soLineSplit.OrderNbr = item.OrderNbr;
    soLineSplit.LineNbr = item.LineNbr;
    soLineSplit.Behavior = item.Behavior;
    soLineSplit.Operation = item.Operation;
    soLineSplit.SplitLineNbr = new int?(1);
    soLineSplit.InventoryID = item.InventoryID;
    soLineSplit.SiteID = item.SiteID;
    soLineSplit.ToSiteID = item.SiteID;
    soLineSplit.SubItemID = item.SubItemID;
    soLineSplit.LocationID = item.LocationID;
    soLineSplit.LotSerialNbr = item.LotSerialNbr;
    soLineSplit.ExpireDate = item.ExpireDate;
    soLineSplit.Qty = item.Qty;
    soLineSplit.UOM = item.UOM;
    soLineSplit.OrderDate = item.OrderDate;
    soLineSplit.BaseQty = item.BaseQty;
    soLineSplit.InvtMult = item.InvtMult;
    soLineSplit.PlanType = item.PlanType;
    int num1;
    if (item.RequireShipping.GetValueOrDefault())
    {
      Decimal? orderQty = item.OrderQty;
      Decimal num2 = 0M;
      if (orderQty.GetValueOrDefault() > num2 & orderQty.HasValue)
      {
        Decimal? openQty = item.OpenQty;
        Decimal num3 = 0M;
        if (openQty.GetValueOrDefault() == num3 & openQty.HasValue)
        {
          num1 = 1;
          goto label_5;
        }
      }
    }
    num1 = item.Completed.GetValueOrDefault() ? 1 : 0;
label_5:
    soLineSplit.Completed = new bool?(num1 != 0);
    soLineSplit.ShipDate = item.ShipDate;
    soLineSplit.RequireAllocation = item.RequireAllocation;
    soLineSplit.RequireLocation = item.RequireLocation;
    soLineSplit.RequireShipping = item.RequireShipping;
    soLineSplit.ProjectID = item.ProjectID;
    soLineSplit.TaskID = item.TaskID;
    soLineSplit.CostCenterID = item.CostCenterID;
    soLineSplit.IsStockItem = item.IsStockItem;
    soLineSplit.AllocatedPlanType = "61";
    soLineSplit.BackOrderPlanType = "68";
    soLineSplit.BookedPlanType = "60";
    soLineSplit.LineType = item.LineType;
    soLineSplit.ShipComplete = item.ShipComplete;
    soLineSplit.ShippedQty = item.ShippedQty;
    soLineSplit.BaseShippedQty = item.BaseShippedQty;
    Decimal? nullable1 = item.Qty;
    Decimal? nullable2 = item.ShippedQty;
    soLineSplit.OpenQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = item.BaseQty;
    nullable1 = item.BaseShippedQty;
    soLineSplit.BaseOpenQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    soLineSplit.TranType = item.TranType;
    soLineSplit.POCreate = item.POCreate;
    soLineSplit.POSource = item.POSource;
    return soLineSplit;
  }

  public class PK : 
    PrimaryKeyOf<SOLineSplit>.By<SOLineSplit.orderType, SOLineSplit.orderNbr, SOLineSplit.lineNbr, SOLineSplit.splitLineNbr>
  {
    public static SOLineSplit Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      int? splitLineNbr,
      PKFindOptions options = 0)
    {
      return (SOLineSplit) PrimaryKeyOf<SOLineSplit>.By<SOLineSplit.orderType, SOLineSplit.orderNbr, SOLineSplit.lineNbr, SOLineSplit.splitLineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, (object) splitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.orderType, SOLineSplit.orderNbr>
    {
    }

    public class OrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.orderType>
    {
    }

    public class OrderTypeOperation : 
      PrimaryKeyOf<SOOrderTypeOperation>.By<SOOrderTypeOperation.orderType, SOOrderTypeOperation.operation>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.orderType, SOLineSplit.operation>
    {
    }

    public class OrderLine : 
      PrimaryKeyOf<SOLine>.By<SOLine.orderType, SOLine.orderNbr, SOLine.lineNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.orderType, SOLineSplit.orderNbr, SOLineSplit.lineNbr>
    {
    }

    public class ParentLineSplit : 
      PrimaryKeyOf<SOLineSplit>.By<SOLineSplit.orderType, SOLineSplit.orderNbr, SOLineSplit.lineNbr, SOLineSplit.splitLineNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.orderType, SOLineSplit.orderNbr, SOLineSplit.lineNbr, SOLineSplit.parentSplitLineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.inventoryID, SOLineSplit.subItemID, SOLineSplit.siteID>
    {
    }

    public class ToSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.toSiteID>
    {
    }

    public class ToSiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.inventoryID, SOLineSplit.subItemID, SOLineSplit.toSiteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.locationID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.inventoryID, SOLineSplit.subItemID, SOLineSplit.siteID, SOLineSplit.locationID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.inventoryID, SOLineSplit.subItemID, SOLineSplit.siteID, SOLineSplit.locationID, SOLineSplit.lotSerialNbr>
    {
    }

    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.shipmentNbr>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.vendorID>
    {
    }

    public class POSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.pOSiteID>
    {
    }

    public class POOrder : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.pOType, SOLineSplit.pONbr>
    {
    }

    public class POLine : 
      PrimaryKeyOf<PX.Objects.PO.POLine>.By<PX.Objects.PO.POLine.orderType, PX.Objects.PO.POLine.orderNbr, PX.Objects.PO.POLine.lineNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.pOType, SOLineSplit.pONbr, SOLineSplit.pOLineNbr>
    {
    }

    public class POReceipt : 
      PrimaryKeyOf<PX.Objects.PO.POReceipt>.By<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.pOReceiptType, SOLineSplit.pOReceiptNbr>
    {
    }

    public class RelatedOrder : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.sOOrderType, SOLineSplit.sOOrderNbr>
    {
    }

    public class RelatedOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.sOOrderType>
    {
    }

    public class RelatedOrderLine : 
      PrimaryKeyOf<SOLine>.By<SOLine.orderType, SOLine.orderNbr, SOLine.lineNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.sOOrderType, SOLineSplit.sOOrderNbr, SOLineSplit.sOLineNbr>
    {
    }

    public class RelatedOrderLineSplit : 
      PrimaryKeyOf<SOLineSplit>.By<SOLineSplit.orderType, SOLineSplit.orderNbr, SOLineSplit.lineNbr, SOLineSplit.splitLineNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.sOOrderType, SOLineSplit.sOOrderNbr, SOLineSplit.sOLineNbr, SOLineSplit.sOSplitLineNbr>
    {
    }

    public class ItemPlan : 
      PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.planID>
    {
    }

    public class SupplyLine : 
      PrimaryKeyOf<SupplyPOLine>.By<SupplyPOLine.orderType, SupplyPOLine.orderNbr, SupplyPOLine.lineNbr>.ForeignKeyOf<SOLineSplit>.By<SOLineSplit.pOType, SOLineSplit.pONbr, SOLineSplit.pOLineNbr>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.lineNbr>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.splitLineNbr>
  {
  }

  public abstract class parentSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOLineSplit.parentSplitLineNbr>
  {
  }

  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.behavior>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.operation>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOLineSplit.invtMult>
  {
  }

  public abstract class requireShipping : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit.requireShipping>
  {
  }

  public abstract class requireAllocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOLineSplit.requireAllocation>
  {
  }

  public abstract class requireLocation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit.requireLocation>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.inventoryID>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.lineType>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit.isStockItem>
  {
  }

  public abstract class isAllocated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit.isAllocated>
  {
  }

  public abstract class isMergeable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit.isMergeable>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.locationID>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.toSiteID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.subItemID>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLineSplit.shipDate>
  {
  }

  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.shipComplete>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit.completed>
  {
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.shipmentNbr>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.costCenterID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.lotSerialNbr>
  {
  }

  public abstract class lotSerClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.lotSerClassID>
  {
  }

  public abstract class assignedNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.assignedNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLineSplit.expireDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLineSplit.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLineSplit.baseQty>
  {
  }

  public abstract class shippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLineSplit.shippedQty>
  {
  }

  public abstract class baseShippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSplit.baseShippedQty>
  {
  }

  public abstract class closedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLineSplit.closedQty>
  {
  }

  public abstract class baseClosedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSplit.baseClosedQty>
  {
  }

  public abstract class receivedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLineSplit.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSplit.baseReceivedQty>
  {
  }

  public abstract class unreceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSplit.unreceivedQty>
  {
  }

  public abstract class baseUnreceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSplit.baseUnreceivedQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLineSplit.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLineSplit.baseOpenQty>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLineSplit.orderDate>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.tranType>
  {
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.planType>
  {
  }

  public abstract class allocatedPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineSplit.allocatedPlanType>
  {
  }

  public abstract class backOrderPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineSplit.backOrderPlanType>
  {
  }

  public abstract class bookedPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineSplit.bookedPlanType>
  {
  }

  public abstract class origPlanType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.origPlanType>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit.pOCreate>
  {
  }

  public abstract class pOCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit.pOCompleted>
  {
  }

  public abstract class pOCancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit.pOCancelled>
  {
  }

  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.pOSource>
  {
  }

  public abstract class fixedSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.fixedSource>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.vendorID>
  {
  }

  public abstract class pOSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.pOSiteID>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.pOLineNbr>
  {
  }

  public abstract class pOReceiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.pOReceiptNbr>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit.sOOrderNbr>
  {
  }

  public abstract class sOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.sOLineNbr>
  {
  }

  public abstract class sOSplitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.sOSplitLineNbr>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOLineSplit.refNoteID>
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
      sender.Graph.Actions[str] = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(typeof (SOOrder)), (object) sender.Graph, (object) str, (object) pxButtonDelegate, (object) new PXEventSubscriberAttribute[2]
      {
        (PXEventSubscriberAttribute) new PXUIFieldAttribute()
        {
          MapEnableRights = (PXCacheRights) 1
        },
        (PXEventSubscriberAttribute) new PXButtonAttribute()
        {
          DisplayOnMainToolbar = false
        }
      });
    }

    public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      if (e.Row is SOLineSplit row && !string.IsNullOrEmpty(row.PONbr))
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
        e.ReturnValue = this.GetEntityRowID(sender.Graph.Caches[typeof (SOShipment)], new object[1]
        {
          (object) row.ShipmentNbr
        });
        e.ReturnState = (object) PXRefNoteBaseAttribute.PXLinkState.CreateInstance(e.ReturnState, typeof (SOShipment), new object[1]
        {
          (object) row.ShipmentNbr
        });
      }
      else if (row != null && !string.IsNullOrEmpty(row.SOOrderNbr))
      {
        e.ReturnValue = this.GetEntityRowID(sender.Graph.Caches[typeof (SOOrder)], new object[2]
        {
          (object) row.SOOrderType,
          (object) row.SOOrderNbr
        });
        e.ReturnState = (object) PXRefNoteBaseAttribute.PXLinkState.CreateInstance(e.ReturnState, typeof (SOOrder), new object[2]
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
  SOLineSplit.planID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.taskID>
  {
  }

  public abstract class aMProdCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit.aMProdCreate>
  {
  }

  public abstract class customerOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineSplit.customerOrderNbr>
  {
  }

  public abstract class schedOrderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOLineSplit.schedOrderDate>
  {
  }

  public abstract class schedShipDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOLineSplit.schedShipDate>
  {
  }

  public abstract class pOCreateDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOLineSplit.pOCreateDate>
  {
  }

  public abstract class qtyOnOrders : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLineSplit.qtyOnOrders>
  {
  }

  public abstract class baseQtyOnOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSplit.baseQtyOnOrders>
  {
  }

  public abstract class blanketOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSplit.blanketOpenQty>
  {
  }

  public abstract class childLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit.childLineCntr>
  {
  }

  public abstract class effectiveChildLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOLineSplit.effectiveChildLineCntr>
  {
  }

  public abstract class openChildLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOLineSplit.openChildLineCntr>
  {
  }

  public abstract class isOrchestratedLine : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOLineSplit.isOrchestratedLine>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOLineSplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineSplit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOLineSplit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOLineSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineSplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOLineSplit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOLineSplit.Tstamp>
  {
  }
}
