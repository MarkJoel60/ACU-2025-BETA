// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSODet
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Service Order Item Detail")]
[Serializable]
public class FSSODet : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IFSSODetBase,
  IDocLine,
  ILSPrimary,
  ILSMaster,
  IItemPlanMaster,
  ISortOrder
{
  private 
  #nullable disable
  string _Operation;
  private string _Behavior;
  private string _ShipComplete;
  public string _TranType;
  private short? _InvtMult;
  private bool? _Completed;
  private int? _BillCustomerID;
  private string _LineType;
  private string _SOLineType;
  private string _UOM;
  private string _LotSerialNbr;
  private Decimal? _UnassignedQty;
  private Decimal? _EstimatedQty;
  private Decimal? _BaseEstimatedQty;
  private Decimal? _DeductQty;
  private Decimal? _BaseDeductQty;
  private Decimal? _ShippedQty;
  private Decimal? _BaseShippedQty;
  private Decimal? _OpenQty;
  private Decimal? _BaseOpenQty;
  private Decimal? _ClosedQty;
  private Decimal? _BaseClosedQty;
  private DateTime? _TranDate;
  private string _PlanType;
  private bool? _RequireShipping;
  private bool? _RequireAllocation;
  private bool? _RequireLocation;
  private Decimal? _LineQtyHardAvail;
  private DateTime? _ShipDate;
  private string _POSource;
  private int? _POSiteID;
  public Decimal? _CuryBillableExtPrice;
  private Decimal? _CuryBillableTranAmt;
  public Decimal? _DiscPct;
  private DateTime? _ExpireDate;

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSServiceOrder.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Service Order Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSServiceOrder.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSSODet.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<FSSODet.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "SOID")]
  [PXDBDefault(typeof (FSServiceOrder.sOID))]
  public virtual int? SOID { get; set; }

  [PXDBIdentity]
  [PXUIField]
  public virtual int? SODetID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSServiceOrder.lineCntr))]
  [PXCheckUnique(new System.Type[] {}, Where = typeof (Where<FSSODet.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSSODet.refNbr, Equal<Current<FSServiceOrder.refNbr>>>>), UniqueKeyIsPartOfPrimaryKey = true, ClearOnDuplicate = false)]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  [PXFormula(null, typeof (MaxCalc<FSServiceOrder.maxLineNbr>))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField]
  public virtual string LineRef { get; set; }

  [Branch(typeof (FSServiceOrder.branchID), null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  [PXUIField]
  [PXDefault(typeof (SOOperation.issue))]
  [SOOperation.List]
  [PXSelectorMarker(typeof (Search<SOOrderTypeOperation.operation, Where<SOOrderTypeOperation.orderType, Equal<Current<FSSrvOrdType.allocationOrderType>>>>))]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXString(2, IsFixed = true, InputMask = ">aa")]
  [PXUnboundDefault(typeof (Search<PX.Objects.SO.SOOrderType.behavior, Where<PX.Objects.SO.SOOrderType.orderType, Equal<Current<FSSrvOrdType.allocationOrderType>>>>))]
  public virtual string Behavior
  {
    get => this._Behavior;
    set => this._Behavior = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Shipping Rule")]
  public virtual string ShipComplete
  {
    get => this._ShipComplete;
    set => this._ShipComplete = value;
  }

  [PXFormula(typeof (Selector<FSSODet.operation, SOOrderTypeOperation.iNDocType>))]
  [PXString(3, IsFixed = true)]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBShort]
  [PXFormula(typeof (Switch<Case<Where<Current<FSSrvOrdType.behavior>, Equal<ListField.ServiceOrderTypeBehavior.quote>>, short0>, shortMinus1>))]
  [PXUIField(DisplayName = "Inventory Multiplier")]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Completed", Enabled = true)]
  public virtual bool? Completed
  {
    get => this._Completed;
    set => this._Completed = value;
  }

  [PXDBInt]
  [PXDefault(typeof (FSServiceOrder.billCustomerID))]
  public virtual int? BillCustomerID
  {
    get => this._BillCustomerID;
    set => this._BillCustomerID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (FSServiceOrder.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Line Type")]
  [FSLineType.List]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDefault]
  [PXFormula(typeof (Default<FSSODet.lineType>))]
  [InventoryIDByLineType(typeof (FSSODet.lineType), null, Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>, Or<FSxServiceClass.requireRoute, NotEqual<False>, Or<Current<FSSrvOrdType.requireRoute>, Equal<False>>>>), "Non-route service cannot be handled with current route Service Order Type.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>, Or<FSxServiceClass.requireRoute, NotEqual<True>, Or<Current<FSSrvOrdType.requireRoute>, Equal<True>>>>), "Route service cannot be handled with current non-route Service Order Type.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, Or<Current<FSSrvOrdType.allowInventoryItems>, Equal<True>>>), "This stock item cannot be handled for current Service Order Type.", new System.Type[] {})]
  public virtual int? InventoryID { get; set; }

  [PXString(2, IsFixed = true)]
  [PX.Objects.SO.SOLineType.List]
  [PXUIField(DisplayName = "SO Line Type", Visible = false, Enabled = false)]
  [PXFormula(typeof (Selector<FSSODet.inventoryID, Switch<Case<Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>, Or<PX.Objects.IN.InventoryItem.kitItem, Equal<True>>>, PX.Objects.SO.SOLineType.inventory, Case<Where<PX.Objects.IN.InventoryItem.nonStockShip, Equal<True>>, PX.Objects.SO.SOLineType.nonInventory>>, PX.Objects.SO.SOLineType.miscCharge>>))]
  public virtual string SOLineType
  {
    get => this._SOLineType;
    set => this._SOLineType = value;
  }

  [PXDBString(4, IsFixed = true)]
  [ListField_BillingRule.List]
  [PXDefault("FLRA")]
  [PXUIField(DisplayName = "Billing Rule")]
  public virtual string BillingRule { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Prepaid Item", Enabled = false, Visible = false, FieldClass = "DISTINV")]
  public virtual bool? IsPrepaid { get; set; }

  [PXBool]
  [PXUIField]
  [PXFormula(typeof (Selector<FSSODet.inventoryID, PX.Objects.IN.InventoryItem.stkItem>))]
  public virtual bool? IsStockItem { get; set; }

  [PXBool]
  [PXUIField]
  [PXFormula(typeof (Selector<FSSODet.inventoryID, PX.Objects.IN.InventoryItem.kitItem>))]
  public virtual bool? IsKit { get; set; }

  [SubItem(typeof (FSSODet.inventoryID), DisplayName = "Subitem")]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSSODet.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<True>>>>))]
  [PXUIEnabled(typeof (Where<FSSODet.lineType, Equal<FSLineType.Service>>))]
  [SubItemStatusVeryfier(typeof (FSSODet.inventoryID), typeof (FSSODet.siteID), new string[] {"IN", "NS"})]
  public virtual int? SubItemID { get; set; }

  [PXDefault]
  [PXUIEnabled(typeof (Where<FSSODet.lineType, Equal<FSLineType.Service>>))]
  [INUnit(typeof (FSSODet.inventoryID), DisplayName = "UOM")]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [FSSiteAvail(typeof (FSSODet.inventoryID), typeof (FSSODet.subItemID), typeof (FSSODet.costCenterID), DisplayName = "Warehouse")]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  [LocationAvail(typeof (FSSODet.inventoryID), typeof (FSSODet.subItemID), typeof (FSSODet.costCenterID), typeof (FSSODet.siteID), typeof (FSSODet.tranType), typeof (FSSODet.invtMult))]
  [PXDefault]
  public virtual int? SiteLocationID { get; set; }

  public virtual int? LocationID
  {
    get => this.SiteLocationID;
    set => this.SiteLocationID = value;
  }

  [SOLotSerialNbr(typeof (FSSODet.inventoryID), typeof (FSSODet.subItemID), typeof (FSSODet.siteLocationID), typeof (FSSODet.costCenterID))]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBBool]
  [PXFormula(typeof (Default<FSSODet.billingRule, FSSODet.SMequipmentID, FSSODet.estimatedQty, FSSODet.inventoryID>))]
  [PXUIField(DisplayName = "Service Contract Item", IsReadOnly = true, FieldClass = "FSCONTRACT")]
  public virtual bool? ContractRelated { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnassignedQty
  {
    get => this._UnassignedQty;
    set => this._UnassignedQty = value;
  }

  [PXDBTimeSpanLong]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Estimated Duration")]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSSODet.lineType, Equal<FSLineType.Service>, And<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Canceled>, And<FSSODet.isTravelItem, Equal<False>>>>, FSSODet.estimatedDuration>, SharedClasses.int_0>), typeof (SumCalc<FSServiceOrder.estimatedDurationTotal>))]
  public virtual int? EstimatedDuration { get; set; }

  [FSDBQuantity(typeof (FSSODet.uOM), typeof (FSSODet.baseEstimatedQty))]
  [PXDefault(typeof (Switch<Case<Where<FSSODet.lineType, Equal<FSLineType.Comment>, Or<FSSODet.lineType, Equal<FSLineType.Instruction>>>, SharedClasses.decimal_0>, SharedClasses.decimal_1>))]
  [PXUIField(DisplayName = "Estimated Quantity")]
  public virtual Decimal? EstimatedQty
  {
    get => this._EstimatedQty;
    set => this._EstimatedQty = value;
  }

  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Estimated Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseEstimatedQty
  {
    get => this._BaseEstimatedQty;
    set => this._BaseEstimatedQty = value;
  }

  [FSQuantity(typeof (FSSODet.uOM), typeof (FSSODet.baseOrderQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Allocation Quantity")]
  public virtual Decimal? OrderQty
  {
    get => !(this.Status == "CC") ? this.EstimatedQty : this.BillableQty;
    set => this.EstimatedQty = value;
  }

  public virtual Decimal? Qty
  {
    get => this.OrderQty;
    set => this.OrderQty = value;
  }

  [PXDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Order Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseOrderQty
  {
    get => this.BaseEstimatedQty;
    set => this.BaseEstimatedQty = value;
  }

  public virtual Decimal? BaseQty
  {
    get => this.BaseOrderQty;
    set => this.BaseOrderQty = value;
  }

  [FSQuantity(typeof (FSSODet.uOM), typeof (FSSODet.baseDeductQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DeductQty
  {
    get => this._DeductQty;
    set => this._DeductQty = value;
  }

  [PXDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseDeductQty
  {
    get => this._BaseDeductQty;
    set => this._BaseDeductQty = value;
  }

  [FSDBQuantity(typeof (FSSODet.uOM), typeof (FSSODet.baseShippedQty), MinValue = 0.0)]
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

  [FSDBQuantity(typeof (FSSODet.uOM), typeof (FSSODet.baseOpenQty))]
  [PXFormula(typeof (Sub<FSSODet.orderQty, FSSODet.shippedQty>))]
  [PXFormula(typeof (Sub<FSSODet.billableQty, FSSODet.shippedQty>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Open Qty.")]
  public virtual Decimal? BaseOpenQty
  {
    get => this._BaseOpenQty;
    set => this._BaseOpenQty = value;
  }

  [PXDBCalced(typeof (Sub<FSSODet.billableQty, FSSODet.openQty>), typeof (Decimal))]
  [FSQuantity(typeof (FSSODet.uOM), typeof (FSSODet.baseClosedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClosedQty
  {
    get => this._ClosedQty;
    set => this._ClosedQty = value;
  }

  [PXDBCalced(typeof (Sub<FSSODet.baseBillableQty, FSSODet.baseOpenQty>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseClosedQty
  {
    get => this._BaseClosedQty;
    set => this._BaseClosedQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Cost", Visible = false)]
  public virtual bool? ManualCost { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Price")]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.postTo>, NotEqual<ListField_PostTo_CreateInvoice.PM>, Or<Current<FSSrvOrdType.postTo>, Equal<ListField_PostTo_CreateInvoice.PM>, And<Current<FSSrvOrdType.billingType>, NotEqual<ListField_SrvOrdType_BillingType.CostAsCost>>>>))]
  public virtual bool? ManualPrice { get; set; }

  [PXDBBool]
  [PXDefault(typeof (IIf<Where<FSSODet.lineType, Equal<FSLineType.Comment>, Or<FSSODet.lineType, Equal<FSLineType.Instruction>, Or<FSSODet.status, Equal<FSSODet.ListField_Status_SODet.Canceled>, Or<FSSODet.isPrepaid, Equal<True>>>>>, False, True>))]
  [PXUIField(DisplayName = "Billable")]
  [PXFormula(typeof (Default<FSSODet.isPrepaid>))]
  public virtual bool? IsBillable { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Free Item")]
  public virtual bool? IsFree { get; set; }

  [FSDBQuantity(typeof (FSSODet.uOM), typeof (FSSODet.baseBillableQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Default<FSSODet.isFree, FSSODet.lineType>), KeepIdleSelfUpdates = true)]
  [PXFormula(typeof (Switch<Case<Where<FSSODet.isPrepaid, Equal<True>, Or<FSSODet.isBillable, Equal<False>, Or<FSSODet.status, Equal<FSSODet.ListField_Status_SODet.Canceled>>>>, decimal0, Case<Where<FSSODet.contractRelated, Equal<True>>, FSSODet.extraUsageQty>>, FSSODet.estimatedQty>), KeepIdleSelfUpdates = true)]
  [PXFormula(typeof (Switch<Case<Where<FSSODet.isPrepaid, Equal<True>, Or<FSSODet.isBillable, Equal<False>>>, decimal0, Case<Where<FSSODet.contractRelated, Equal<True>>, FSSODet.extraUsageQty>>, FSSODet.orderQty>), KeepIdleSelfUpdates = true)]
  [PXUIField(DisplayName = "Quantity", Enabled = false)]
  public virtual Decimal? BillableQty { get; set; }

  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Billable Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseBillableQty { get; set; }

  [PXDBInt]
  [PXDefault(typeof (FSServiceOrder.projectID))]
  [PXUIField(Visible = false)]
  [PXForeignReference(typeof (FSSODet.FK.Project))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Switch<Case<Where<FSSODet.lineType, Equal<FSLineType.Comment>, Or<FSSODet.lineType, Equal<FSLineType.Instruction>>>, Null>, Current<FSServiceOrder.dfltProjectTaskID>>))]
  [PXUIField(DisplayName = "Project Task", FieldClass = "PROJECT")]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<FSSODet.projectID>>>))]
  [PXForeignReference(typeof (FSSODet.FK.Task))]
  public virtual int? ProjectTaskID { get; set; }

  public virtual int? TaskID
  {
    get => this.ProjectTaskID;
    set => this.ProjectTaskID = value;
  }

  /// <exclude />
  [PXDBInt]
  [PXDefault(typeof (PX.Objects.IN.CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Source Line ID", Enabled = false)]
  public virtual int? SourceLineID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Source Note ID", Enabled = false)]
  public virtual Guid? SourceNoteID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Source Line Nbr.", Enabled = false)]
  public virtual int? SourceLineNbr { get; set; }

  [PXDBDate]
  [PXDBDefault(typeof (FSServiceOrder.orderDate))]
  [PXUIField(DisplayName = "Transaction Date")]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBScalar(typeof (Search<INPlanType.planType, Where<INPlanType.inclQtyFSSrvOrdBooked, Equal<True>>>))]
  [PXDefault(typeof (Search<INPlanType.planType, Where<INPlanType.inclQtyFSSrvOrdBooked, Equal<True>>>))]
  [PXString(2, IsFixed = true)]
  public virtual string PlanType
  {
    get => this._PlanType;
    set => this._PlanType = value;
  }

  [PXBool]
  [PXFormula(typeof (Current<PX.Objects.SO.SOOrderType.requireShipping>))]
  public virtual bool? RequireShipping
  {
    get => this._RequireShipping;
    set => this._RequireShipping = value;
  }

  [PXBool]
  [PXFormula(typeof (Current<PX.Objects.SO.SOOrderType.requireAllocation>))]
  public virtual bool? RequireAllocation
  {
    get => this._RequireAllocation;
    set => this._RequireAllocation = value;
  }

  [PXBool]
  [PXFormula(typeof (Current<PX.Objects.SO.SOOrderType.requireLocation>))]
  public virtual bool? RequireLocation
  {
    get => this._RequireLocation;
    set => this._RequireLocation = value;
  }

  [PXDecimal(6)]
  public virtual Decimal? LineQtyAvail { get; set; }

  [PXDecimal(6)]
  public virtual Decimal? LineQtyHardAvail
  {
    get => this._LineQtyHardAvail;
    set => this._LineQtyHardAvail = value;
  }

  public virtual DateTime? OrderDate
  {
    get => this.TranDate;
    set => this.TranDate = value;
  }

  [PXDBDate]
  [PXDefault(typeof (FSServiceOrder.orderDate))]
  [PXUIField]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBString(2, IsFixed = true)]
  [FSSODet.ListField_Status_SODet.ListAtrribute]
  [PXUIField]
  [PXFormula(typeof (Default<FSSODet.lineType, FSSODet.billingRule, FSSODet.estimatedQty, FSSODet.estimatedDuration>))]
  [PXFormula(typeof (Default<FSSODet.linkedEntityType>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<Current<FSSrvOrdType.behavior>, NotEqual<ListField.ServiceOrderTypeBehavior.quote>, And<FSSODet.status, Equal<FSSODet.ListField_Status_SODet.ScheduleNeeded>, And<FSSODet.lineType, NotEqual<FSLineType.Comment>, And<FSSODet.lineType, NotEqual<FSLineType.Instruction>>>>>, int1>, int0>), typeof (SumCalc<FSServiceOrder.apptNeededLineCntr>))]
  public virtual string Status { get; set; }

  [PXDBInt]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual int? ScheduleDetID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ListField_EquipmentAction.ListAtrribute]
  [PXDefault("NO")]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.inventory>))]
  [PXUIField(DisplayName = "Equipment Action", FieldClass = "EQUIPMENTMANAGEMENT")]
  public virtual string EquipmentAction { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Target Equipment ID", FieldClass = "EQUIPMENTMANAGEMENT")]
  [PXUIEnabled(typeof (Where<Current<FSSODet.isTravelItem>, NotEqual<True>>))]
  [FSSelectorMaintenanceEquipment(typeof (FSServiceOrder.srvOrdType), typeof (FSServiceOrder.billCustomerID), typeof (FSServiceOrder.customerID), typeof (FSServiceOrder.locationID), typeof (FSServiceOrder.branchID), typeof (FSServiceOrder.branchLocationID))]
  [PXRestrictor(typeof (Where<FSEquipment.status, Equal<ID.Equipment_Status.Equipment_StatusActive>>), "Equipment is {0}.", new System.Type[] {typeof (FSEquipment.status)})]
  [PXForeignReference(typeof (Field<FSSODet.SMequipmentID>.IsRelatedTo<FSEquipment.SMequipmentID>))]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.inventory>))]
  [PXUIField(DisplayName = "Model Equipment Ref. Nbr.", FieldClass = "EQUIPMENTMANAGEMENT")]
  [PXUIEnabled(typeof (Where<Current<FSSODet.isTravelItem>, NotEqual<True>>))]
  [PXDefault]
  [FSSelectorNewTargetEquipmentServiceOrder]
  public virtual string NewTargetEquipmentLineNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component ID", FieldClass = "EQUIPMENTMANAGEMENT")]
  [PXUIEnabled(typeof (Where<Current<FSSODet.isTravelItem>, NotEqual<True>>))]
  [FSSelectorComponentIDServiceOrder(typeof (FSSODet), typeof (FSSODet))]
  public virtual int? ComponentID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component Ref. Nbr.", FieldClass = "EQUIPMENTMANAGEMENT")]
  [PXUIEnabled(typeof (Where<Current<FSSODet.isTravelItem>, NotEqual<True>>))]
  [FSSelectorEquipmentLineRefServiceOrderAppointment(typeof (FSSODet.inventoryID), typeof (FSSODet.SMequipmentID), typeof (FSSODet.componentID), typeof (FSSODet.equipmentAction))]
  public virtual int? EquipmentLineRef { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Post ID")]
  public virtual int? PostID { get; set; }

  [PXFormula(typeof (Default<FSSODet.inventoryID>))]
  [PXDefault]
  [Account]
  public virtual int? AcctID { get; set; }

  [PXFormula(typeof (Default<FSSODet.acctID>))]
  [PXDefault]
  [SubAccount(typeof (FSSODet.acctID))]
  public virtual int? SubID { get; set; }

  [PXDBInt]
  public virtual int? ScheduleID { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<INItemSiteSettings.pOCreate, Where<INItemSiteSettings.inventoryID, Equal<Current<FSSODet.inventoryID>>, And<INItemSiteSettings.siteID, Equal<Current<FSSODet.siteID>>>>>))]
  [PXUIField(DisplayName = "Mark for PO", FieldClass = "DISTINV", Visible = false)]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Canceled>, And<FSSODet.enablePO, Equal<True>, And<FSSODet.poCompleted, Equal<False>>>>, int1>, int0>), typeof (SumCalc<FSServiceOrder.pendingPOLineCntr>))]
  [PXUIEnabled(typeof (Where<FSSODet.lineType, NotEqual<ListField_LineType_ALL.Comment>, And<FSSODet.lineType, NotEqual<ListField_LineType_ALL.Instruction>>>))]
  public virtual bool? EnablePO { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Mark for PO", FieldClass = "DISTINV", Visible = false)]
  public virtual bool? POCreate
  {
    get => this.EnablePO;
    set => this.EnablePO = value;
  }

  [VendorNonEmployeeActive(DisplayName = "Vendor ID", DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), CacheGlobal = true, Filterable = true, FieldClass = "DISTINV")]
  [PXDefault(typeof (Search<INItemSiteSettings.preferredVendorID, Where<INItemSiteSettings.inventoryID, Equal<Current<FSSODet.inventoryID>>, And<INItemSiteSettings.siteID, Equal<Current<FSSODet.siteID>>>>>))]
  [PXFormula(typeof (Default<FSSODet.enablePO>))]
  public virtual int? POVendorID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Vendor ID", FieldClass = "DISTINV")]
  public virtual int? VendorID
  {
    get => this.POVendorID;
    set => this.POVendorID = value;
  }

  [PXFormula(typeof (Default<FSSODet.poVendorID>))]
  [PXDefault(typeof (Coalesce<Search<INItemSiteSettings.preferredVendorLocationID, Where<INItemSiteSettings.inventoryID, Equal<Current<FSSODet.inventoryID>>, And<INItemSiteSettings.preferredVendorID, Equal<Current<FSSODet.poVendorID>>>>>, Search2<PX.Objects.AP.Vendor.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.AP.Vendor.defLocationID>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<FSSODet.poVendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>>))]
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSSODet.poVendorID>>, And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>))]
  public virtual int? POVendorLocationID { get; set; }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Order Type", FieldClass = "DISTINV")]
  public virtual string POType { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "PO Nbr.", Enabled = false, FieldClass = "DISTINV")]
  [PX.Objects.PO.PO.RefNbr(typeof (Search2<PX.Objects.PO.POOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.PO.POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, Where<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.regularOrder>, And<PX.Objects.AP.Vendor.bAccountID, IsNotNull>>, OrderBy<Desc<PX.Objects.PO.POOrder.orderNbr>>>), Filterable = true)]
  public virtual string PONbr { get; set; }

  [PXDBString]
  [PXDefault(typeof (IIf<Where<FSSODet.enablePO, Equal<True>>, ListField_FSPOSource.purchaseToServiceOrder, Null>))]
  [PXFormula(typeof (Default<FSSODet.enablePO>))]
  [ListField_FSPOSource.List]
  [PXUIField(DisplayName = "PO Source", Enabled = false, FieldClass = "DISTINV")]
  public virtual string POSource
  {
    get => this._POSource;
    set => this._POSource = value;
  }

  [PXDBString]
  [POOrderStatus.List]
  [PXUIField(DisplayName = "PO Status", Enabled = false, FieldClass = "DISTINV", Visible = false)]
  public virtual string POStatus { get; set; }

  [Site(DisplayName = "Purchase Warehouse", FieldClass = "DISTINV")]
  [PXForeignReference(typeof (FSSODet.FK.PurchaseSite))]
  public virtual int? POSiteID
  {
    get => this._POSiteID;
    set => this._POSiteID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "PO Line Nbr.", FieldClass = "DISTINV")]
  public virtual int? POLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "PO Completed", Enabled = false, Visible = false, FieldClass = "DISTINV")]
  public virtual bool? POCompleted { get; set; }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (FSSODet.curyInfoID), typeof (FSSODet.unitCost))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Default<FSSODet.enablePO>), KeepIdleSelfUpdates = true)]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXDBPriceCost]
  public virtual Decimal? UnitCost { get; set; }

  [PXDBCurrency(typeof (FSSODet.curyInfoID), typeof (FSSODet.extCost))]
  [PXUIField(DisplayName = "Ext. Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<FSSODet.linkedEntityType, Equal<ListField_Linked_Entity_Type.expenseReceipt>, Or<FSSODet.linkedEntityType, Equal<ListField_Linked_Entity_Type.apBill>>>, IsNull<FSSODet.curyExtCost, decimal0>, Case<Where<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Canceled>, And<Where<FSSODet.lineType, Equal<FSLineType.NonStockItem>, Or<FSSODet.lineType, Equal<FSLineType.Inventory_Item>, Or<Where<FSSODet.lineType, Equal<FSLineType.Service>, And<FSSODet.curyUnitCost, Greater<SharedClasses.decimal_0>>>>>>>>, Mult<FSSODet.curyUnitCost, FSSODet.estimatedQty>>>, SharedClasses.decimal_0>), typeof (SumCalc<FSServiceOrder.curyCostTotal>), KeepIdleSelfUpdates = true)]
  public virtual Decimal? CuryExtCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtCost { get; set; }

  [PXDBCurrencyPriceCost(typeof (FSSODet.curyInfoID), typeof (FSSODet.origUnitCost))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigUnitCost { get; set; }

  [PXDBPriceCost]
  public virtual Decimal? OrigUnitCost { get; set; }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (FSSODet.curyInfoID), typeof (FSSODet.unitPrice))]
  [PXUIField(DisplayName = "Unit Price")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitPrice { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Unit Price", Enabled = false)]
  public virtual Decimal? UnitPrice { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Estimated Amount", Enabled = false)]
  public virtual Decimal? EstimatedTranAmt { get; set; }

  [PXDBCurrency(typeof (FSSODet.curyInfoID), typeof (FSSODet.estimatedTranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<FSSODet.lineType, Equal<FSLineType.Service>, And<FSSODet.billingRule, Equal<ListField_BillingRule.None>>>, SharedClasses.decimal_0, Case<Where<FSSODet.status, Equal<FSSODet.ListField_Status_SODet.Canceled>>, SharedClasses.decimal_0>>, Mult<FSSODet.curyUnitPrice, FSSODet.estimatedQty>>), typeof (SumCalc<FSServiceOrder.curyEstimatedOrderTotal>), KeepIdleSelfUpdates = true)]
  [PXUIField(DisplayName = "Estimated Amount", Enabled = false)]
  public virtual Decimal? CuryEstimatedTranAmt { get; set; }

  [PXDBCurrency(typeof (FSSODet.curyInfoID), typeof (FSSODet.billableExtPrice))]
  [PXUIField(DisplayName = "Ext. Price")]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.postTo>, NotEqual<ListField_PostTo_CreateInvoice.PM>, Or<Current<FSSrvOrdType.postTo>, Equal<ListField_PostTo_CreateInvoice.PM>, And<Current<FSSrvOrdType.billingType>, NotEqual<ListField_SrvOrdType_BillingType.CostAsCost>>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<FSSODet.isFree, Equal<True>>, IsNull<FSSODet.curyBillableExtPrice, decimal0>, Case<Where<FSSODet.contractRelated, Equal<False>, And<FSSODet.lineType, Equal<FSLineType.Service>, And<FSSODet.billingRule, Equal<ListField_BillingRule.None>>>>, SharedClasses.decimal_0, Case<Where<FSSODet.contractRelated, Equal<True>, And<FSSODet.isFree, Equal<False>>>, Mult<FSSODet.curyExtraUsageUnitPrice, FSSODet.billableQty>>>>, Mult<FSSODet.curyUnitPrice, FSSODet.billableQty>>), KeepIdleSelfUpdates = true)]
  public virtual Decimal? CuryBillableExtPrice
  {
    get => this._CuryBillableExtPrice;
    set => this._CuryBillableExtPrice = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BillableExtPrice { get; set; }

  [PXDBCurrency(typeof (FSSODet.curyInfoID), typeof (FSSODet.billableTranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<FSSODet.isPrepaid, Equal<True>, Or<FSSODet.contractRelated, Equal<True>>>, FSSODet.curyBillableExtPrice>, Sub<FSSODet.curyBillableExtPrice, FSSODet.curyDiscAmt>>), typeof (SumCalc<FSServiceOrder.curyBillableOrderTotal>), KeepIdleSelfUpdates = true)]
  public virtual Decimal? CuryBillableTranAmt
  {
    get => this._CuryBillableTranAmt;
    set => this._CuryBillableTranAmt = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Billable Amount", Enabled = false)]
  public virtual Decimal? BillableTranAmt { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Ext. Price")]
  public virtual Decimal? CuryExtPrice => this.CuryBillableExtPrice;

  [PXDecimal]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  public virtual Decimal? CuryLineAmt => this.CuryBillableTranAmt;

  [PXDBBool]
  [ManualDiscountMode(typeof (FSSODet.curyDiscAmt), typeof (FSSODet.discPct), DiscountFeatureType.CustomerDiscount)]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ManualDisc { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.postTo>, NotEqual<ListField_PostTo_CreateInvoice.PM>, Or<Current<FSSrvOrdType.postTo>, Equal<ListField_PostTo_CreateInvoice.PM>, And<Current<FSSrvOrdType.billingType>, NotEqual<ListField_SrvOrdType_BillingType.CostAsCost>>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Default<FSSODet.inventoryID>))]
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrencyPriceCost(typeof (FSSODet.curyInfoID), typeof (FSSODet.discAmt))]
  [PXFormula(typeof (Div<Mult<FSSODet.curyBillableExtPrice, FSSODet.discPct>, decimal100>), KeepIdleSelfUpdates = true)]
  [PXUIField(DisplayName = "Discount Amount")]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.postTo>, NotEqual<ListField_PostTo_CreateInvoice.PM>, Or<Current<FSSrvOrdType.postTo>, Equal<ListField_PostTo_CreateInvoice.PM>, And<Current<FSSrvOrdType.billingType>, NotEqual<ListField_SrvOrdType_BillingType.CostAsCost>>>>))]
  public virtual Decimal? CuryDiscAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, Equal<DiscountType.LineDiscount>>>))]
  [PXUIField(DisplayName = "Discount Code", Visible = true, Enabled = true)]
  public virtual string DiscountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Discount Sequence", Visible = false, Enabled = false)]
  public virtual string DiscountSequenceID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Appointment Count", Enabled = false)]
  public virtual int? ApptCntr { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Appointment Estimated Duration", Enabled = false)]
  [PXDefault(0)]
  public virtual int? ApptEstimatedDuration { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Appointment Duration", Enabled = false)]
  [PXDefault(0)]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSSODet.lineType, Equal<FSLineType.Service>, And<FSSODet.isTravelItem, Equal<False>>>, FSSODet.apptDuration>, SharedClasses.int_0>), typeof (SumCalc<FSServiceOrder.apptDurationTotal>))]
  public virtual int? ApptDuration { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Appointment Quantity", Enabled = false)]
  public virtual Decimal? ApptQty { get; set; }

  [PXDBCurrency(typeof (FSSODet.curyInfoID), typeof (FSSODet.apptTranAmt))]
  [PXUIField(DisplayName = "Appointment Amount", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUnboundFormula(typeof (FSSODet.curyApptTranAmt), typeof (SumCalc<FSServiceOrder.curyApptOrderTotal>))]
  public virtual Decimal? CuryApptTranAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Appointment Amount", Enabled = false)]
  [PXUnboundFormula(typeof (FSSODet.apptTranAmt), typeof (SumCalc<FSServiceOrder.apptOrderTotal>))]
  public virtual Decimal? ApptTranAmt { get; set; }

  [PXDBInt]
  [FSSelector_StaffMember_ServiceOrderProjectID]
  [PXUIField(DisplayName = "Staff Member ID")]
  public virtual int? StaffID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Warranty", Enabled = false, FieldClass = "EQUIPMENTMANAGEMENT")]
  public virtual bool? Warranty { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "SO NewTargetEquipmentLineNbr", FieldClass = "EQUIPMENTMANAGEMENT")]
  public virtual int? SONewTargetEquipmentLineNbr { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = false)]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.inventory>))]
  [PXUIField(DisplayName = "Equipment Action Comment", FieldClass = "EQUIPMENTMANAGEMENT", Visible = false)]
  public virtual string Comment { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string EquipmentItemClass { get; set; }

  [SMCostCode(typeof (FSSODet.skipCostCodeValidation), typeof (FSSODet.acctID), typeof (FSSODet.projectTaskID))]
  [PXFormula(typeof (Default<FSSODet.inventoryID, FSSODet.isPrepaid>))]
  [PXForeignReference(typeof (FSSODet.FK.CostCode))]
  public virtual int? CostCodeID { get; set; }

  [PXBool]
  [PXFormula(typeof (IIf<Where2<Where<FSSODet.inventoryID, IsNotNull>, And<Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>>>>>, False, True>))]
  public virtual bool? SkipCostCodeValidation { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Sort Order", Visible = false, Enabled = false)]
  public virtual int? SortOrder { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSSODet.inventoryID>>>>))]
  [PXFormula(typeof (Default<FSSODet.inventoryID>))]
  public virtual string TaxCategoryID { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? GroupDiscountRate { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? DocumentDiscountRate { get; set; }

  [PXDBQuantity]
  [PXFormula(typeof (Default<FSSODet.contractRelated>))]
  [PXUIField(DisplayName = "Covered Quantity", Enabled = false, Visible = false, FieldClass = "FSCONTRACT")]
  public virtual Decimal? CoveredQty { get; set; }

  [PXDBQuantity]
  [PXFormula(typeof (Switch<Case<Where<FSSODet.contractRelated, Equal<True>>, Sub<FSSODet.estimatedQty, FSSODet.coveredQty>>, SharedClasses.decimal_0>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Overage Quantity", Enabled = false, Visible = false, FieldClass = "FSCONTRACT")]
  public virtual Decimal? ExtraUsageQty { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Overage Unit Price", Enabled = false, FieldClass = "FSCONTRACT")]
  public virtual Decimal? ExtraUsageUnitPrice { get; set; }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (FSSODet.curyInfoID), typeof (FSSODet.extraUsageUnitPrice))]
  [PXFormula(typeof (Default<FSSODet.contractRelated>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Overage Unit Price", Enabled = false, Visible = false, FieldClass = "FSCONTRACT")]
  public virtual Decimal? CuryExtraUsageUnitPrice { get; set; }

  [INExpireDate(typeof (FSSODet.inventoryID))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Last Reference", Enabled = false, Visible = false)]
  [PXSelector(typeof (FSAppointment.refNbr))]
  public virtual string Mem_LastReferencedBy { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXInt]
  [PXFormula(typeof (Switch<Case<Where<FSSODet.lineType, NotEqual<ListField_LineType_ALL.Inventory_Item>>, FSSODet.estimatedDuration>, SharedClasses.int_0>))]
  public virtual int? EstimatedDurationReport { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Cost Code Description", FieldClass = "COSTCODE")]
  public virtual string CostCodeDescr { get; set; }

  [PXInt]
  public virtual int? TabOrigin => this.LineType == "SLPRO" ? new int?(1) : new int?(0);

  [PXBool]
  public virtual bool? SkipUnitPriceCalc { get; set; }

  [PXPriceCost]
  public virtual Decimal? AlreadyCalculatedUnitPrice { get; set; }

  [PXBool]
  [PXUIField]
  [PXFormula(typeof (Selector<FSSODet.inventoryID, FSxService.isTravelItem>))]
  public virtual bool? IsTravelItem { get; set; }

  [PXBool]
  [PXDefault(true)]
  public virtual bool? EnableStaffID { get; set; }

  [PXInt]
  [PXSelector(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noSales>, And<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.serviceItem>, And<Match<Current<AccessInfo.userName>>>>>>>>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  public virtual int? InventoryIDReport { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Related Doc. Type", IsReadOnly = true, Visible = false)]
  [ListField_Linked_Entity_Type.List]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSSODet.linkedEntityType, Equal<ListField_Linked_Entity_Type.apBill>>, int1>, int0>), typeof (SumCalc<FSServiceOrder.apBillLineCntr>))]
  public virtual string LinkedEntityType { get; set; }

  [PXDBString(4)]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual string LinkedDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual string LinkedDocRefNbr { get; set; }

  [PXString]
  [PXUIField]
  public virtual string LinkedDisplayRefNbr
  {
    get
    {
      return (this.LinkedDocType != null ? this.LinkedDocType.Trim() + ", " : "") + (this.LinkedDocRefNbr != null ? this.LinkedDocRefNbr.Trim() : "");
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Related Doc. Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? LinkedLineNbr { get; set; }

  public int? GetPrimaryDACDuration() => this.EstimatedDuration;

  public Decimal? GetPrimaryDACQty() => this.EstimatedQty;

  public Decimal? GetPrimaryDACTranAmt() => this.CuryEstimatedTranAmt;

  public int? GetDuration(FieldType fieldType)
  {
    if (fieldType == FieldType.EstimatedField || fieldType == FieldType.BillableField)
      return this.EstimatedDuration;
    throw new InvalidOperationException();
  }

  public int? GetApptDuration() => this.ApptEstimatedDuration;

  public Decimal? GetQty(FieldType fieldType)
  {
    if (fieldType == FieldType.EstimatedField)
      return this.EstimatedQty;
    if (fieldType == FieldType.BillableField)
      return this.BillableQty;
    throw new InvalidOperationException();
  }

  public Decimal? GetApptQty() => this.ApptQty;

  public Decimal? GetBaseQty(FieldType fieldType)
  {
    if (fieldType == FieldType.EstimatedField)
      return this.BaseEstimatedQty;
    if (fieldType == FieldType.BillableField)
      return this.BaseBillableQty;
    throw new InvalidOperationException();
  }

  public Decimal? GetTranAmt(FieldType fieldType)
  {
    if (fieldType == FieldType.EstimatedField)
      return this.CuryEstimatedTranAmt;
    if (fieldType == FieldType.BillableField)
      return this.CuryBillableTranAmt;
    throw new InvalidOperationException();
  }

  public void SetDuration(FieldType fieldType, int? duration, PXCache cache, bool raiseEvents)
  {
    if (fieldType == FieldType.EstimatedField)
    {
      if (raiseEvents)
        cache.SetValueExt<FSSODet.estimatedDuration>((object) this, (object) duration);
      else
        this.EstimatedDuration = duration;
    }
    else
    {
      if (fieldType != FieldType.BillableField)
        throw new InvalidOperationException();
      if (raiseEvents)
        cache.SetValueExt<FSSODet.estimatedDuration>((object) this, (object) duration);
      else
        this.EstimatedDuration = duration;
    }
  }

  public void SetQty(FieldType fieldType, Decimal? qty, PXCache cache, bool raiseEvents)
  {
    if (fieldType == FieldType.EstimatedField)
    {
      if (raiseEvents)
        cache.SetValueExt<FSSODet.estimatedQty>((object) this, (object) qty);
      else
        this.EstimatedQty = qty;
    }
    else
    {
      if (fieldType != FieldType.BillableField)
        throw new InvalidOperationException();
      if (raiseEvents)
        cache.SetValueExt<FSSODet.billableQty>((object) this, (object) qty);
      else
        this.BillableQty = qty;
    }
  }

  public virtual bool needToBePosted()
  {
    if (this.LineType == "SERVI" || this.LineType == "NSTKI" || this.LineType == "SLPRO")
    {
      bool? isPrepaid = this.IsPrepaid;
      bool flag = false;
      if (isPrepaid.GetValueOrDefault() == flag & isPrepaid.HasValue)
        return this.Status != "CC";
    }
    return false;
  }

  public virtual bool IsExpenseReceiptItem => this.LinkedEntityType == "ER";

  public virtual bool IsAPBillItem => this.LinkedEntityType == "AP";

  public virtual bool IsLinkedItem => this.IsExpenseReceiptItem || this.IsAPBillItem;

  public static implicit operator FSSODetSplit(FSSODet item)
  {
    FSSODetSplit fssoDetSplit = new FSSODetSplit();
    fssoDetSplit.SrvOrdType = item.SrvOrdType;
    fssoDetSplit.RefNbr = item.RefNbr;
    fssoDetSplit.LineNbr = item.LineNbr;
    fssoDetSplit.Operation = item.Operation;
    fssoDetSplit.SplitLineNbr = new int?(1);
    fssoDetSplit.InventoryID = item.InventoryID;
    fssoDetSplit.SiteID = item.SiteID;
    fssoDetSplit.SubItemID = item.SubItemID;
    fssoDetSplit.LocationID = item.SiteLocationID;
    fssoDetSplit.CostCenterID = item.CostCenterID;
    fssoDetSplit.LotSerialNbr = item.LotSerialNbr;
    fssoDetSplit.ExpireDate = item.ExpireDate;
    fssoDetSplit.Qty = item.Qty;
    fssoDetSplit.UOM = item.UOM;
    fssoDetSplit.OrderDate = item.OrderDate;
    fssoDetSplit.BaseQty = item.BaseQty;
    fssoDetSplit.InvtMult = item.InvtMult;
    fssoDetSplit.PlanType = item.PlanType;
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
    fssoDetSplit.Completed = new bool?(num1 != 0);
    fssoDetSplit.ShipDate = item.ShipDate;
    fssoDetSplit.RequireAllocation = item.RequireAllocation;
    fssoDetSplit.RequireLocation = item.RequireLocation;
    fssoDetSplit.RequireShipping = item.RequireShipping;
    return fssoDetSplit;
  }

  public static implicit operator FSSODet(FSSODetSplit item)
  {
    return new FSSODet()
    {
      SrvOrdType = item.SrvOrdType,
      RefNbr = item.RefNbr,
      LineNbr = item.LineNbr,
      Operation = item.Operation,
      InventoryID = item.InventoryID,
      SiteID = item.SiteID,
      SubItemID = item.SubItemID,
      LocationID = item.LocationID,
      SiteLocationID = item.LocationID,
      CostCenterID = item.CostCenterID,
      LotSerialNbr = item.LotSerialNbr,
      Qty = item.Qty,
      OpenQty = item.Qty,
      BaseOpenQty = item.BaseQty,
      UOM = item.UOM,
      OrderDate = item.OrderDate,
      BaseQty = item.BaseQty,
      InvtMult = item.InvtMult,
      PlanType = item.PlanType,
      ShipDate = item.ShipDate,
      RequireAllocation = item.RequireAllocation,
      RequireLocation = item.RequireLocation,
      RequireShipping = item.RequireShipping
    };
  }

  public int? DocID => this.SOID;

  public int? LineID => this.SODetID;

  public int? PostAppointmentID => new int?();

  public int? PostSODetID => this.SODetID;

  public int? PostAppDetID => new int?();

  public string BillingBy => "SO";

  public string SourceTable => nameof (FSSODet);

  public bool IsService => this.LineType == "SERVI" || this.LineType == "NSTKI";

  public bool IsInventoryItem => this.LineType == "SLPRO";

  public bool IsCommentInstruction => this.LineType == "CM_LN" || this.LineType == "IT_LN";

  bool? ILSMaster.IsIntercompany => new bool?(false);

  public class PK : PrimaryKeyOf<FSSODet>.By<FSSODet.srvOrdType, FSSODet.refNbr, FSSODet.lineNbr>
  {
    public static FSSODet Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSSODet) PrimaryKeyOf<FSSODet>.By<FSSODet.srvOrdType, FSSODet.refNbr, FSSODet.lineNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public class UK : PrimaryKeyOf<FSSODet>.By<FSSODet.sODetID>
  {
    public static FSSODet Find(PXGraph graph, int? sODetID, PKFindOptions options = 0)
    {
      return (FSSODet) PrimaryKeyOf<FSSODet>.By<FSSODet.sODetID>.FindBy(graph, (object) sODetID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSSODet>.By<FSSODet.branchID>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSSODet>.By<FSSODet.srvOrdType, FSSODet.refNbr>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSSODet>.By<FSSODet.srvOrdType>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSSODet>.By<FSSODet.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<FSSODet>.By<FSSODet.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<FSSODet>.By<FSSODet.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<FSSODet>.By<FSSODet.inventoryID, FSSODet.subItemID, FSSODet.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<FSSODet>.By<FSSODet.locationID>
    {
    }

    public class CostCenter : 
      PrimaryKeyOf<INCostCenter>.By<INCostCenter.costCenterID>.ForeignKeyOf<FSSODet>.By<FSSODet.costCenterID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<FSSODet>.By<FSSODet.inventoryID, FSSODet.subItemID, FSSODet.siteID, FSSODet.locationID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<FSSODet>.By<FSSODet.inventoryID, FSSODet.subItemID, FSSODet.siteID, FSSODet.locationID, FSSODet.lotSerialNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSSODet>.By<FSSODet.curyInfoID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<FSSODet>.By<FSSODet.taxCategoryID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSSODet>.By<FSSODet.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSSODet>.By<FSSODet.projectID, FSSODet.projectTaskID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FSSODet>.By<FSSODet.acctID>
    {
    }

    public class Subaccount : PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FSSODet>.By<FSSODet.subID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<FSSODet>.By<FSSODet.costCodeID>
    {
    }

    public class Discount : 
      PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.ForeignKeyOf<FSSODet>.By<FSSODet.discountID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSSODet>.By<FSSODet.vendorID>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSSODet>.By<FSSODet.billCustomerID>
    {
    }

    public class Equipment : 
      PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.ForeignKeyOf<FSSODet>.By<FSSODet.SMequipmentID>
    {
    }

    public class Component : 
      PrimaryKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.componentID>.ForeignKeyOf<FSSODet>.By<FSSODet.componentID>
    {
    }

    public class EquipmentComponent : 
      PrimaryKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.SMequipmentID, FSEquipmentComponent.lineNbr>.ForeignKeyOf<FSSODet>.By<FSSODet.SMequipmentID, FSSODet.equipmentLineRef>
    {
    }

    public class PostInfo : 
      PrimaryKeyOf<FSPostInfo>.By<FSPostInfo.postID>.ForeignKeyOf<FSSODet>.By<FSSODet.postID>
    {
    }

    public class PurchaseOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<FSSODet>.By<FSSODet.poType>
    {
    }

    public class PurchaseOrder : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<FSSODet>.By<FSSODet.poType, FSSODet.poNbr>
    {
    }

    public class PurchaseSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<FSSODet>.By<FSSODet.pOSiteID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<FSSODet>.By<FSSODet.scheduleID>
    {
    }

    public class ScheduleDetail : 
      PrimaryKeyOf<FSScheduleDet>.By<FSScheduleDet.scheduleID, FSScheduleDet.scheduleDetID>.ForeignKeyOf<FSSODet>.By<FSSODet.scheduleID, FSSODet.scheduleDetID>
    {
    }

    public class Staff : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<FSSOEmployee>.By<FSSODet.staffID>
    {
    }
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.refNbr>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.sOID>
  {
  }

  public abstract class sODetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.sODetID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.lineNbr>
  {
  }

  public abstract class lineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.lineRef>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.branchID>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.operation>
  {
  }

  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.behavior>
  {
  }

  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.shipComplete>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.tranType>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSODet.invtMult>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.completed>
  {
  }

  public abstract class billCustomerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.billCustomerID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSSODet.curyInfoID>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.lineType>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.inventoryID>
  {
  }

  public abstract class sOLineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.sOLineType>
  {
  }

  public abstract class billingRule : ListField_BillingRule
  {
  }

  public abstract class isPrepaid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.isPrepaid>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.isStockItem>
  {
  }

  public abstract class isKit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.isKit>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.subItemID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.uOM>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.siteID>
  {
  }

  public abstract class siteLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.siteLocationID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.locationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.lotSerialNbr>
  {
  }

  public abstract class contractRelated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.contractRelated>
  {
  }

  public abstract class unassignedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.unassignedQty>
  {
  }

  public abstract class estimatedDuration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.estimatedDuration>
  {
  }

  public abstract class estimatedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.estimatedQty>
  {
  }

  public abstract class baseEstimatedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.baseEstimatedQty>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.orderQty>
  {
  }

  public abstract class baseOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.baseOrderQty>
  {
  }

  public abstract class deductQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.deductQty>
  {
  }

  public abstract class baseDeductQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.baseDeductQty>
  {
  }

  public abstract class shippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.shippedQty>
  {
  }

  public abstract class baseShippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.baseShippedQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.baseOpenQty>
  {
  }

  public abstract class closedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.closedQty>
  {
  }

  public abstract class baseClosedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.baseClosedQty>
  {
  }

  public abstract class manualCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.manualCost>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.manualPrice>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.isBillable>
  {
  }

  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.isFree>
  {
  }

  public abstract class billableQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.billableQty>
  {
  }

  public abstract class baseBillableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.baseBillableQty>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.projectTaskID>
  {
  }

  /// <exclude />
  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.costCenterID>
  {
  }

  public abstract class sourceLineID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.sourceLineID>
  {
  }

  public abstract class sourceNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSODet.sourceNoteID>
  {
  }

  public abstract class sourceLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.sourceLineNbr>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSSODet.tranDate>
  {
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.planType>
  {
  }

  public abstract class requireShipping : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.requireShipping>
  {
  }

  public abstract class requireAllocation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.requireAllocation>
  {
  }

  public abstract class requireLocation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.requireLocation>
  {
  }

  public abstract class lineQtyAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.lineQtyAvail>
  {
  }

  public abstract class lineQtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.lineQtyHardAvail>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSSODet.shipDate>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.tranDesc>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSODet.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSODet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSODet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSODet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSODet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSSODet.Tstamp>
  {
  }

  public abstract class ListField_Status_SODet : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.status>
  {
    public class ListAtrribute : PXStringListAttribute
    {
      public ListAtrribute()
        : base(new Tuple<string, string>[4]
        {
          PXStringListAttribute.Pair("SN", "Requiring Scheduling"),
          PXStringListAttribute.Pair("SC", "Scheduled"),
          PXStringListAttribute.Pair("CC", "Canceled"),
          PXStringListAttribute.Pair("CP", "Completed")
        })
      {
      }
    }

    public class ScheduleNeeded : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSSODet.ListField_Status_SODet.ScheduleNeeded>
    {
      public ScheduleNeeded()
        : base("SN")
      {
      }
    }

    public class Scheduled : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSSODet.ListField_Status_SODet.Scheduled>
    {
      public Scheduled()
        : base("SC")
      {
      }
    }

    public class Completed : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSSODet.ListField_Status_SODet.Completed>
    {
      public Completed()
        : base("CP")
      {
      }
    }

    public class Canceled : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSSODet.ListField_Status_SODet.Canceled>
    {
      public Canceled()
        : base("CC")
      {
      }
    }
  }

  public abstract class status : FSSODet.ListField_Status_SODet
  {
  }

  public abstract class scheduleDetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.scheduleDetID>
  {
  }

  public abstract class equipmentAction : ListField_EquipmentAction
  {
  }

  public abstract class SMequipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.SMequipmentID>
  {
  }

  public abstract class newTargetEquipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODet.newTargetEquipmentLineNbr>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.componentID>
  {
  }

  public abstract class equipmentLineRef : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.equipmentLineRef>
  {
  }

  public abstract class postID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.postID>
  {
  }

  public abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.acctID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.subID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.scheduleID>
  {
  }

  public abstract class enablePO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.enablePO>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.pOCreate>
  {
  }

  public abstract class poVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.poVendorID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.vendorID>
  {
  }

  public abstract class poVendorLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.poVendorLocationID>
  {
  }

  public abstract class poType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.poType>
  {
  }

  public abstract class poNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.poNbr>
  {
  }

  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.pOSource>
  {
    public abstract class Values : ListField_FSPOSource
    {
    }
  }

  public abstract class poStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.poStatus>
  {
  }

  public abstract class pOSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.pOSiteID>
  {
  }

  public abstract class poLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.poLineNbr>
  {
  }

  public abstract class poCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.poCompleted>
  {
  }

  public abstract class curyUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.curyUnitCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.unitCost>
  {
  }

  public abstract class curyExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.extCost>
  {
  }

  public abstract class curyOrigUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.curyOrigUnitCost>
  {
  }

  public abstract class origUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.origUnitCost>
  {
  }

  public abstract class curyUnitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.curyUnitPrice>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.unitPrice>
  {
  }

  public abstract class estimatedTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.estimatedTranAmt>
  {
  }

  public abstract class curyEstimatedTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.curyEstimatedTranAmt>
  {
  }

  public abstract class curyBillableExtPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.curyBillableExtPrice>
  {
  }

  public abstract class billableExtPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.billableExtPrice>
  {
  }

  public abstract class curyBillableTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.curyBillableTranAmt>
  {
  }

  public abstract class billableTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.billableTranAmt>
  {
  }

  public abstract class curyExtPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.curyExtPrice>
  {
  }

  public abstract class curyLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.curyLineAmt>
  {
  }

  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.manualDisc>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.discPct>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.discAmt>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODet.discountSequenceID>
  {
  }

  public abstract class apptCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.apptCntr>
  {
  }

  public abstract class apptEstimatedDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSODet.apptEstimatedDuration>
  {
  }

  public abstract class apptDuration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.apptDuration>
  {
  }

  public abstract class apptQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.apptQty>
  {
  }

  public abstract class curyApptTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.curyApptTranAmt>
  {
  }

  public abstract class apptTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.apptTranAmt>
  {
  }

  public abstract class staffID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.staffID>
  {
  }

  public abstract class warranty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.warranty>
  {
  }

  public abstract class sONewTargetEquipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSODet.sONewTargetEquipmentLineNbr>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.comment>
  {
  }

  public abstract class equipmentItemClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODet.equipmentItemClass>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.costCodeID>
  {
  }

  public abstract class skipCostCodeValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSODet.skipCostCodeValidation>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.sortOrder>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.taxCategoryID>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.documentDiscountRate>
  {
  }

  public abstract class coveredQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.coveredQty>
  {
  }

  public abstract class extraUsageQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSODet.extraUsageQty>
  {
  }

  public abstract class extraUsageUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.extraUsageUnitPrice>
  {
  }

  public abstract class curyExtraUsageUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.curyExtraUsageUnitPrice>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSSODet.expireDate>
  {
  }

  public abstract class mem_LastReferencedBy : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODet.mem_LastReferencedBy>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.selected>
  {
  }

  public abstract class estimatedDurationReport : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSODet.estimatedDurationReport>
  {
  }

  public abstract class costCodeDescr : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.costCodeDescr>
  {
  }

  public abstract class tabOrigin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.tabOrigin>
  {
  }

  public abstract class skipUnitPriceCalc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.skipUnitPriceCalc>
  {
  }

  public abstract class alreadyCalculatedUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODet.alreadyCalculatedUnitPrice>
  {
  }

  public abstract class isTravelItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.isTravelItem>
  {
  }

  public abstract class enableStaffID : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSODet.enableStaffID>
  {
  }

  public abstract class inventoryIDReport : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.inventoryIDReport>
  {
  }

  public abstract class linkedEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODet.linkedEntityType>
  {
    public abstract class Values : ListField_Linked_Entity_Type
    {
    }
  }

  public abstract class linkedDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.linkedDocType>
  {
  }

  public abstract class linkedDocRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODet.linkedDocRefNbr>
  {
  }

  public abstract class linkedDisplayRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODet.linkedDisplayRefNbr>
  {
  }

  public abstract class linkedLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODet.linkedLineNbr>
  {
  }
}
