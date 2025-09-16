// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.Attributes;
using PX.Objects.PM;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Transaction")]
[Serializable]
public class INTran : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSPrimary,
  ILSMaster,
  IItemPlanMaster,
  ISortOrder,
  ILSTransferPrimary,
  ILotSerialTrackableLine
{
  protected bool? _Selected;
  protected int? _BranchID;
  protected 
  #nullable disable
  string _DocType;
  protected string _OrigModule;
  protected string _TranType;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected DateTime? _TranDate;
  protected string _POLineType;
  protected short? _InvtMult;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected int? _BAccountID;
  protected string _SOOrderType;
  protected string _SOOrderNbr;
  protected int? _SOOrderLineNbr;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _CostCodeID;
  protected string _OrigDocType;
  protected string _OrigTranType;
  protected string _OrigRefNbr;
  protected int? _OrigLineNbr;
  protected int? _AcctID;
  protected int? _SubID;
  protected bool? _ReclassificationProhibited;
  protected int? _InvtAcctID;
  protected int? _InvtSubID;
  protected int? _COGSAcctID;
  protected int? _COGSSubID;
  protected int? _ToSiteID;
  protected int? _ToLocationID;
  protected string _LotSerialNbr;
  protected DateTime? _ExpireDate;
  protected string _UOM;
  protected Decimal? _Qty;
  protected bool? _Released;
  protected string _FinPeriodID;
  protected string _TranPeriodID;
  protected Decimal? _UnitPrice;
  protected Decimal? _TranAmt;
  protected Decimal? _UnitCost;
  protected Decimal? _AvgCost;
  protected Decimal? _TranCost;
  protected string _TranDesc;
  protected bool? _AccrueCost;
  protected string _ReasonCode;
  protected Decimal? _OrigQty;
  protected Decimal? _BaseQty;
  protected Decimal? _MaxTransferBaseQty;
  protected Decimal? _UnassignedQty;
  protected Decimal? _CostedQty;
  protected Decimal? _OrigTranCost;
  protected Decimal? _OrigTranAmt;
  protected string _ARDocType;
  protected string _ARRefNbr;
  protected int? _ARLineNbr;
  protected string _SOShipmentType;
  protected string _SOShipmentNbr;
  protected int? _SOShipmentLineNbr;
  protected string _SOLineType;
  protected bool? _UpdateShippedNotInvoiced;
  protected string _POReceiptType;
  protected string _POReceiptNbr;
  protected int? _POReceiptLineNbr;
  protected string _AssyType;
  protected Decimal? _ReceiptedBaseQty;
  protected Decimal? _INTransitBaseQty;
  protected Decimal? _ReceiptedQty;
  protected Decimal? _INTransitQty;
  protected bool? _IsCostUnmanaged;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXBool]
  [PXFormula(typeof (False))]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [Branch(typeof (INRegister.branchID), null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXUIField(DisplayName = "Document Type", Enabled = false)]
  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (INRegister.docType))]
  [INDocType.List]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDBDefault(typeof (INRegister.origModule))]
  [PXUIField(DisplayName = "Source", Enabled = false)]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [INTranType.List]
  [PXUIField(DisplayName = "Tran. Type")]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (INRegister.refNbr))]
  [PXParent(typeof (INTran.FK.INRegister))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (INRegister.lineCntr))]
  [PXUIField(DisplayName = "Line Number", Enabled = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXUIField(DisplayName = "Line Order", Visible = false, Enabled = false)]
  [PXDBInt]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (INRegister.tranDate))]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsIntercompany { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string POLineType
  {
    get => this._POLineType;
    set => this._POLineType = value;
  }

  [PXDBShort]
  [PXDefault]
  [PXUIField(DisplayName = "Multiplier", Enabled = false)]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [PXDBBool]
  public virtual bool? IsStockItem { get; set; }

  [PXDefault]
  [StockItem(DisplayName = "Inventory ID")]
  [PXForeignReference(typeof (INTran.FK.InventoryItem))]
  [ConvertedInventoryItem(typeof (INTran.isStockItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault(typeof (Search<InventoryItem.defaultSubItemID, Where<InventoryItem.inventoryID, Equal<Current<INTran.inventoryID>>, And<InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [SubItem(typeof (INTran.inventoryID), typeof (LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.subItemID, Equal<INSubItem.subItemID>, And<INSiteStatusByCostCenter.inventoryID, Equal<Optional<INTran.inventoryID>>, And<INSiteStatusByCostCenter.siteID, Equal<Optional<INTran.siteID>>, And<INSiteStatusByCostCenter.costCenterID, Equal<Optional<INTran.costCenterID>>>>>>>))]
  [PXFormula(typeof (Default<INTran.inventoryID>))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [SiteAvail(typeof (INTran.inventoryID), typeof (INTran.subItemID), typeof (INTran.costCenterID), DocumentBranchType = typeof (INRegister.branchID))]
  [PXDefault(typeof (INRegister.siteID))]
  [PXForeignReference(typeof (INTran.FK.Site))]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<INSite.branchID, Current<INRegister.branchID>>>))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [LocationAvail(typeof (INTran.inventoryID), typeof (INTran.subItemID), typeof (INTran.costCenterID), typeof (INTran.siteID), typeof (INTran.tranType), typeof (INTran.invtMult))]
  [PXForeignReference(typeof (INTran.FK.Location))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBInt]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBInt]
  public virtual int? DestBranchID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "SO Order Type", Visible = false, Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType>))]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "SO Order Nbr.", Visible = false, Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<INTran.sOOrderType>>>>))]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  [PXDBInt]
  public virtual int? SOOrderLineNbr
  {
    get => this._SOOrderLineNbr;
    set => this._SOOrderLineNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Special Order", FieldClass = "SpecialOrders")]
  public virtual bool? IsSpecialOrder { get; set; }

  /// <summary>The source of material for a transaction line.</summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("F")]
  [InventorySourceType.List(true)]
  [PXUIField(DisplayName = "Inventory Source", FieldClass = "InventorySource")]
  public virtual string InventorySource { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Cost Layer Type", FieldClass = "CostLayerType")]
  [PX.Objects.IN.CostLayerType.List]
  public virtual string CostLayerType { get; set; }

  [PXDBInt]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Special Order Nbr.", FieldClass = "SpecialOrders")]
  [SpecialOrderCostCenterSelector(typeof (INTran.inventoryID), typeof (INTran.siteID), SOOrderTypeField = typeof (INTran.sOOrderType), SOOrderNbrField = typeof (INTran.sOOrderNbr), SOOrderLineNbrField = typeof (INTran.sOOrderLineNbr), IsSpecialOrderField = typeof (INTran.isSpecialOrder), CostCenterIDField = typeof (INTran.costCenterID), CostLayerTypeField = typeof (INTran.costLayerType), OrigModuleField = typeof (INTran.origModule), ReleasedField = typeof (INTran.released), ProjectIDField = typeof (INTran.projectID), TaskIDField = typeof (INTran.taskID), CostCodeIDField = typeof (INTran.costCodeID), InventorySourceField = typeof (INTran.inventorySource))]
  public virtual int? SpecialOrderCostCenterID { get; set; }

  [ProjectDefault("IN")]
  [PXRestrictor(typeof (Where<PX.Objects.PM.PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new System.Type[] {typeof (PX.Objects.PM.PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.PM.PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PX.Objects.PM.PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PX.Objects.PM.PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.PM.PMProject.visibleInIN, Equal<True>, Or<PX.Objects.PM.PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PX.Objects.PM.PMProject.contractCD)})]
  [ProjectBase]
  [PXForeignReference(typeof (INTran.FK.Project))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault(typeof (Search<PX.Objects.PM.PMTask.taskID, Where<PX.Objects.PM.PMTask.projectID, Equal<Current<INTran.projectID>>, And<PX.Objects.PM.PMTask.isDefault, Equal<True>>>>))]
  [ActiveProjectTask(typeof (INTran.projectID), "IN", DisplayName = "Project Task")]
  [PXForeignReference(typeof (INTran.FK.Task))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [CostCode(null, typeof (INTran.taskID), null, ReleasedField = typeof (INTran.released))]
  [PXForeignReference(typeof (INTran.FK.CostCode))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBString(1, IsFixed = true)]
  public virtual string OrigDocType
  {
    get => this._OrigDocType;
    set => this._OrigDocType = value;
  }

  [PXDBString(3, IsFixed = true)]
  public virtual string OrigTranType
  {
    get => this._OrigTranType;
    set => this._OrigTranType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Receipt Nbr.", Enabled = false)]
  [PXVerifySelector(typeof (Search2<INCostStatus.receiptNbr, InnerJoin<INCostSubItemXRef, On<INCostSubItemXRef.costSubItemID, Equal<INCostStatus.costSubItemID>>, InnerJoin<INLocation, On<INLocation.locationID, Equal<Optional<INTran.locationID>>>>>, Where<INCostStatus.inventoryID, Equal<Optional<INTran.inventoryID>>, And<INCostSubItemXRef.subItemID, Equal<Optional<INTran.subItemID>>, And2<Where<CostCenter.freeStock, Equal<Optional<INTran.costCenterID>>, And<Where<INCostStatus.costSiteID, Equal<Optional<INTran.siteID>>, And<INLocation.isCosted, Equal<boolFalse>, Or<INCostStatus.costSiteID, Equal<Optional<INTran.locationID>>>>>>>, Or<INCostStatus.costSiteID, Equal<Optional<INTran.costCenterID>>>>>>>), VerifyField = false)]
  public virtual string OrigRefNbr
  {
    get => this._OrigRefNbr;
    set => this._OrigRefNbr = value;
  }

  [PXDBInt]
  public virtual int? OrigLineNbr
  {
    get => this._OrigLineNbr;
    set => this._OrigLineNbr = value;
  }

  /// <summary>
  /// Denormalization of <see cref="P:PX.Objects.IN.INTranSplit.ToLocationID" />
  /// </summary>
  [PXDBInt]
  public virtual int? OrigToLocationID { get; set; }

  /// <summary>
  /// Denormalization of <see cref="P:PX.Objects.IN.INTranSplit.LotSerialNbr" />
  /// </summary>
  [PXDBBool]
  public virtual bool? OrigIsLotSerial { get; set; }

  /// <summary>
  /// Denormalization of <see cref="P:PX.Objects.IN.INRegister.NoteID" />
  /// </summary>
  [PXDBGuid(false)]
  public virtual Guid? OrigNoteID { get; set; }

  [Account(Enabled = false)]
  public virtual int? AcctID
  {
    get => this._AcctID;
    set => this._AcctID = value;
  }

  [SubAccount(typeof (INTran.acctID), Enabled = false)]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ReclassificationProhibited
  {
    get => this._ReclassificationProhibited;
    set => this._ReclassificationProhibited = value;
  }

  [Account(Enabled = false)]
  public virtual int? InvtAcctID
  {
    get => this._InvtAcctID;
    set => this._InvtAcctID = value;
  }

  [SubAccount(typeof (INTran.invtAcctID), Enabled = false)]
  public virtual int? InvtSubID
  {
    get => this._InvtSubID;
    set => this._InvtSubID = value;
  }

  [Account(Enabled = false)]
  public virtual int? COGSAcctID
  {
    get => this._COGSAcctID;
    set => this._COGSAcctID = value;
  }

  [SubAccount(typeof (INTran.cOGSAcctID), Enabled = false)]
  public virtual int? COGSSubID
  {
    get => this._COGSSubID;
    set => this._COGSSubID = value;
  }

  [ToSite(Enabled = false)]
  [PXForeignReference(typeof (INTran.FK.ToSite))]
  public virtual int? ToSiteID
  {
    get => this._ToSiteID;
    set => this._ToSiteID = value;
  }

  [LocationAvail(typeof (INTran.inventoryID), typeof (INTran.subItemID), typeof (INTran.toCostCenterID), typeof (INTran.toSiteID), false, false, true, DisplayName = "To Location ID", Enabled = false)]
  [PXForeignReference(typeof (INTran.FK.ToLocation))]
  public virtual int? ToLocationID
  {
    get => this._ToLocationID;
    set => this._ToLocationID = value;
  }

  [INTranLotSerialNbr(typeof (INTran.inventoryID), typeof (INTran.subItemID), typeof (INTran.locationID), typeof (INTran.costCenterID))]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [INExpireDate(typeof (INTran.inventoryID))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDefault]
  [INUnit(typeof (INTran.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (INTran.uOM), typeof (INTran.baseQty), InventoryUnitType.BaseUnit)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  [PXFormula(null, typeof (SumCalc<INRegister.totalQty>), ValidateAggregateCalculation = true)]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXUIField(DisplayName = "Release Date", Enabled = false)]
  [DBConditionalModifiedDateTime(typeof (INTran.released), true)]
  public virtual DateTime? ReleasedDateTime { get; set; }

  [PX.Objects.GL.FinPeriodID(null, typeof (INTran.branchID), null, null, null, null, true, false, null, typeof (INTran.tranPeriodID), typeof (INRegister.tranPeriodID), true, true)]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<INItemSite.basePrice, Where<INItemSite.inventoryID, Equal<Current<INTran.inventoryID>>, And<INItemSite.siteID, Equal<Current<INTran.siteID>>>>>))]
  [PXUIField(DisplayName = "Unit Price")]
  public virtual Decimal? UnitPrice
  {
    get => this._UnitPrice;
    set => this._UnitPrice = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Price")]
  [PXFormula(typeof (Mult<INTran.qty, INTran.unitPrice>))]
  public virtual Decimal? TranAmt
  {
    get => this._TranAmt;
    set => this._TranAmt = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ExactCost { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Cost", Visible = false, Enabled = false)]
  public virtual bool? ManualCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Coalesce<Search<INItemSite.tranUnitCost, Where<INItemSite.inventoryID, Equal<Current<INTran.inventoryID>>, And<INItemSite.siteID, Equal<Current<INTran.siteID>>>>>, Search<INItemCost.tranUnitCost, Where<INItemCost.inventoryID, Equal<Current<INTran.inventoryID>>, And<INItemCost.curyID, EqualBaseCuryID<Current2<INRegister.branchID>>>>>>))]
  [PXUIField(DisplayName = "Unit Cost")]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXBool]
  public virtual bool? OverrideUnitCost { get; set; }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<INItemSite.avgCost, Where<INItemSite.inventoryID, Equal<Current<INTran.inventoryID>>, And<INItemSite.siteID, Equal<Current<INTran.siteID>>>>>))]
  public virtual Decimal? AvgCost
  {
    get => this._AvgCost;
    set => this._AvgCost = value;
  }

  [PXDBBaseCury(null, null, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXFormula(typeof (Mult<INTran.qty, INTran.unitCost>), typeof (SumCalc<INRegister.totalCost>))]
  public virtual Decimal? TranCost
  {
    get => this._TranCost;
    set => this._TranCost = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXDefault(typeof (Select<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<INTran.inventoryID>>>>))]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that cost will be processed using expense accrual account.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? AccrueCost
  {
    get => this._AccrueCost;
    set => this._AccrueCost = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID>))]
  [PXRestrictor(typeof (Where<PX.Objects.CS.ReasonCode.usage, Equal<Optional<INTran.docType>>>), "The usage type of the reason code does not match the document type.", new System.Type[] {})]
  [PXUIField(DisplayName = "Reason Code")]
  [PXForeignReference(typeof (INTran.FK.ReasonCode))]
  public virtual string ReasonCode
  {
    get => this._ReasonCode;
    set => this._ReasonCode = value;
  }

  [PXDBCalced(typeof (Minus<INTran.qty>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigQty
  {
    get => this._OrigQty;
    set => this._OrigQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQty
  {
    get => this._BaseQty;
    set => this._BaseQty = value;
  }

  [PXDBQuantity]
  public virtual Decimal? MaxTransferBaseQty
  {
    get => this._MaxTransferBaseQty;
    set => this._MaxTransferBaseQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnassignedQty
  {
    get => this._UnassignedQty;
    set => this._UnassignedQty = value;
  }

  [PXDecimal(6)]
  [PXFormula(typeof (decimal0))]
  public virtual Decimal? CostedQty
  {
    get => this._CostedQty;
    set => this._CostedQty = value;
  }

  [PXBaseCury]
  public virtual Decimal? OrigTranCost
  {
    get => this._OrigTranCost;
    set => this._OrigTranCost = value;
  }

  [PXBaseCury]
  public virtual Decimal? OrigTranAmt
  {
    get => this._OrigTranAmt;
    set => this._OrigTranAmt = value;
  }

  [PXDBString(3)]
  public virtual string ARDocType
  {
    get => this._ARDocType;
    set => this._ARDocType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string ARRefNbr
  {
    get => this._ARRefNbr;
    set => this._ARRefNbr = value;
  }

  [PXDBInt]
  public virtual int? ARLineNbr
  {
    get => this._ARLineNbr;
    set => this._ARLineNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  public virtual string SOShipmentType
  {
    get => this._SOShipmentType;
    set => this._SOShipmentType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "SO Shipment Nbr.", Visible = false, Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.Navigate.SOOrderShipment.shipmentNbr, Where<PX.Objects.SO.Navigate.SOOrderShipment.orderType, Equal<Current<INTran.sOOrderType>>, And<PX.Objects.SO.Navigate.SOOrderShipment.orderNbr, Equal<Current<INTran.sOOrderNbr>>, And<PX.Objects.SO.Navigate.SOOrderShipment.shipmentType, Equal<Current<INTran.sOShipmentType>>>>>>))]
  public virtual string SOShipmentNbr
  {
    get => this._SOShipmentNbr;
    set => this._SOShipmentNbr = value;
  }

  [PXDBInt]
  public virtual int? SOShipmentLineNbr
  {
    get => this._SOShipmentLineNbr;
    set => this._SOShipmentLineNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  public virtual string SOLineType
  {
    get => this._SOLineType;
    set => this._SOLineType = value;
  }

  /// <summary>
  /// The destination source of material for a transaction line.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("F")]
  [InventorySourceType.List(false)]
  [PXUIField(DisplayName = "To Inventory Source", FieldClass = "InventorySource")]
  public virtual string ToInventorySource { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField(DisplayName = "To Cost Layer Type", FieldClass = "CostLayerType")]
  [PX.Objects.IN.CostLayerType.List]
  public virtual string ToCostLayerType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? UpdateShippedNotInvoiced
  {
    get => this._UpdateShippedNotInvoiced;
    set => this._UpdateShippedNotInvoiced = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PX.Objects.PO.POReceiptType.List]
  [PXUIField(DisplayName = "PO Receipt Type", Visible = false, Enabled = false)]
  public virtual string POReceiptType
  {
    get => this._POReceiptType;
    set => this._POReceiptType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Receipt Nbr.", Visible = false, Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<BqlField<INTran.pOReceiptType, IBqlString>.FromCurrent>>>))]
  public virtual string POReceiptNbr
  {
    get => this._POReceiptNbr;
    set => this._POReceiptNbr = value;
  }

  [PXDBInt]
  public virtual int? POReceiptLineNbr
  {
    get => this._POReceiptLineNbr;
    set => this._POReceiptLineNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  public virtual string AssyType
  {
    get => this._AssyType;
    set => this._AssyType = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  public virtual string OrigPlanType { get; set; }

  [PXRestrictor(typeof (Where<PX.Objects.PM.PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new System.Type[] {typeof (PX.Objects.PM.PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.PM.PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PX.Objects.PM.PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PX.Objects.PM.PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.PM.PMProject.visibleInIN, Equal<True>, Or<PX.Objects.PM.PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PX.Objects.PM.PMProject.contractCD)})]
  [ProjectBase(DisplayName = "To Project")]
  [PXForeignReference(typeof (INTran.FK.Project))]
  public virtual int? ToProjectID { get; set; }

  [PXDefault(typeof (Search<PX.Objects.PM.PMTask.taskID, Where<PX.Objects.PM.PMTask.projectID, Equal<Current<INTran.toProjectID>>, And<PX.Objects.PM.PMTask.isDefault, Equal<True>>>>))]
  [ActiveProjectTask(typeof (INTran.toProjectID), "IN", DisplayName = "To Project Task")]
  [PXForeignReference(typeof (INTran.FK.Task))]
  public virtual int? ToTaskID { get; set; }

  [CostCode(null, typeof (INTran.toTaskID), null, ReleasedField = typeof (INTran.released), DisplayName = "To Cost Code")]
  [PXForeignReference(typeof (INTran.FK.CostCode))]
  public virtual int? ToCostCodeID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? ToCostCenterID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "To Special Order Nbr.", FieldClass = "SpecialOrders")]
  [SpecialOrderCostCenterSelector(typeof (INTran.inventoryID), typeof (INTran.toSiteID), typeof (INTran.invtMult), typeof (INTran.specialOrderCostCenterID), CostLayerTypeField = typeof (INTran.toCostLayerType), CostCenterIDField = typeof (INTran.toCostCenterID), OrigModuleField = typeof (INTran.origModule), ReleasedField = typeof (INTran.released), ProjectIDField = typeof (INTran.toProjectID), TaskIDField = typeof (INTran.toTaskID), CostCodeIDField = typeof (INTran.toCostCodeID), InventorySourceField = typeof (INTran.toInventorySource), AllowEnabled = false)]
  public virtual int? ToSpecialOrderCostCenterID { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Received Base Qty.", Visible = false, Enabled = true, IsReadOnly = true)]
  public virtual Decimal? ReceiptedBaseQty
  {
    get => this._ReceiptedBaseQty;
    set => this._ReceiptedBaseQty = value;
  }

  [PXQuantity]
  [PXUIField(DisplayName = "In-Transit Base Qty.", Visible = false, Enabled = true, IsReadOnly = true)]
  public virtual Decimal? INTransitBaseQty
  {
    get => this._INTransitBaseQty;
    set => this._INTransitBaseQty = value;
  }

  [PXQuantity(typeof (INTran.uOM), typeof (INTran.receiptedBaseQty))]
  [PXUIField(DisplayName = "Received Qty.", Visible = false, Enabled = true, IsReadOnly = true)]
  public virtual Decimal? ReceiptedQty
  {
    [PXDependsOnFields(new System.Type[] {typeof (INTran.receiptedBaseQty)})] get
    {
      return this._ReceiptedQty;
    }
    set => this._ReceiptedQty = value;
  }

  [PXQuantity(typeof (INTran.uOM), typeof (INTran.iNTransitBaseQty))]
  [PXUIField(DisplayName = "In-Transit Qty.", Visible = false, Enabled = true, IsReadOnly = true)]
  public virtual Decimal? INTransitQty
  {
    [PXDependsOnFields(new System.Type[] {typeof (INTran.iNTransitBaseQty)})] get
    {
      return this._INTransitQty;
    }
    set => this._INTransitQty = value;
  }

  [PXDBBool]
  public virtual bool? IsCostUnmanaged
  {
    get => this._IsCostUnmanaged;
    set => this._IsCostUnmanaged = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public bool? IsComponentItem { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string PIID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "PI Line Nbr.", Enabled = false, Visible = false)]
  public virtual int? PILineNbr { get; set; }

  [PXDBString(6, IsUnicode = true)]
  public virtual string OrigUOM { get; set; }

  [PXDBQuantity]
  public virtual Decimal? OrigFullQty { get; set; }

  [PXDBQuantity]
  public virtual Decimal? BaseOrigFullQty { get; set; }

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

  [PXShort]
  public virtual short? SalesMult
  {
    [PXDependsOnFields(new System.Type[] {typeof (INTran.tranType)})] get
    {
      return INTranType.SalesMult(this._TranType);
    }
    set
    {
    }
  }

  public static INTran FromINTranSplit(INTranSplit item)
  {
    return new INTran()
    {
      DocType = item.DocType,
      TranType = item.TranType,
      RefNbr = item.RefNbr,
      LineNbr = item.LineNbr,
      InventoryID = item.InventoryID,
      SiteID = item.SiteID,
      SubItemID = item.SubItemID,
      LocationID = item.LocationID,
      LotSerialNbr = item.LotSerialNbr,
      Qty = item.Qty,
      UOM = item.UOM,
      TranDate = item.TranDate,
      BaseQty = item.BaseQty,
      InvtMult = item.InvtMult,
      Released = item.Released,
      POLineType = item.POLineType,
      SOLineType = item.SOLineType,
      ToSiteID = item.ToSiteID,
      ToLocationID = item.ToLocationID,
      OrigModule = item.OrigModule,
      IsIntercompany = item.IsIntercompany,
      ProjectID = item.ProjectID,
      TaskID = item.TaskID
    };
  }

  public class PK : PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>
  {
    public static INTran Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (INTran) PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INTran>.By<INTran.branchID>
    {
    }

    public class INRegister : 
      PrimaryKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr>.ForeignKeyOf<INTran>.By<INTran.docType, INTran.refNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INTran>.By<INTran.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INTran>.By<INTran.subItemID>
    {
    }

    public class Site : PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTran>.By<INTran.siteID>
    {
    }

    public class ToSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTran>.By<INTran.toSiteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INTran>.By<INTran.locationID>
    {
    }

    public class ToLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INTran>.By<INTran.toLocationID>
    {
    }

    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<INTran>.By<INTran.bAccountID>
    {
    }

    public class ARRegister : 
      PrimaryKeyOf<PX.Objects.AR.ARRegister>.By<PX.Objects.AR.ARRegister.docType, PX.Objects.AR.ARRegister.refNbr>.ForeignKeyOf<INTran>.By<INTran.aRDocType, INTran.aRRefNbr>
    {
    }

    public class ARTran : 
      PrimaryKeyOf<PX.Objects.AR.ARTran>.By<PX.Objects.AR.ARTran.tranType, PX.Objects.AR.ARTran.refNbr, PX.Objects.AR.ARTran.lineNbr>.ForeignKeyOf<INTran>.By<INTran.aRDocType, INTran.aRRefNbr, INTran.aRLineNbr>
    {
    }

    public class OriginalReceipt : 
      PrimaryKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr>.ForeignKeyOf<INTran>.By<INTran.origDocType, INTran.origRefNbr>
    {
    }

    public class OriginalTran : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<INTran>.By<INTran.origDocType, INTran.origRefNbr, INTran.origLineNbr>
    {
    }

    public class OriginalToLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INTran>.By<INTran.origToLocationID>
    {
    }

    public class OriginalPlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<INTran>.By<INTran.origPlanType>
    {
    }

    public class SOOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<INTran>.By<INTran.sOOrderType>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<INTran>.By<INTran.sOOrderType, INTran.sOOrderNbr>
    {
    }

    public class SOLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<INTran>.By<INTran.sOOrderType, INTran.sOOrderNbr, INTran.sOOrderLineNbr>
    {
    }

    public class SOShipment : 
      PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentType, PX.Objects.SO.SOShipment.shipmentNbr>.ForeignKeyOf<INTran>.By<INTran.sOShipmentType, INTran.sOShipmentNbr>
    {
    }

    public class SOShipmentLine : 
      PrimaryKeyOf<SOShipLine>.By<SOShipLine.shipmentType, SOShipLine.shipmentNbr, SOShipLine.lineNbr>.ForeignKeyOf<INTran>.By<INTran.sOShipmentType, INTran.sOShipmentNbr, INTran.sOShipmentLineNbr>
    {
    }

    public class POReceipt : 
      PrimaryKeyOf<PX.Objects.PO.POReceipt>.By<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>.ForeignKeyOf<INTran>.By<INTran.pOReceiptType, INTran.pOReceiptNbr>
    {
    }

    public class POReceiptLine : 
      PrimaryKeyOf<PX.Objects.PO.POReceiptLine>.By<PX.Objects.PO.POReceiptLine.receiptType, PX.Objects.PO.POReceiptLine.receiptNbr, PX.Objects.PO.POReceiptLine.lineNbr>.ForeignKeyOf<INTran>.By<INTran.pOReceiptType, INTran.pOReceiptNbr, INTran.pOReceiptLineNbr>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INTran>.By<INTran.reasonCode>
    {
    }

    public class DestinationBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INTran>.By<INTran.destBranchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INTran>.By<INTran.acctID>
    {
    }

    public class Subaccount : PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INTran>.By<INTran.subID>
    {
    }

    public class InventoryAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INTran>.By<INTran.invtAcctID>
    {
    }

    public class InventorySubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INTran>.By<INTran.invtSubID>
    {
    }

    public class COGSAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INTran>.By<INTran.cOGSAcctID>
    {
    }

    public class COGSSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INTran>.By<INTran.cOGSSubID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PX.Objects.PM.PMProject>.By<PX.Objects.PM.PMProject.contractID>.ForeignKeyOf<INTran>.By<INTran.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PX.Objects.PM.PMTask>.By<PX.Objects.PM.PMTask.projectID, PX.Objects.PM.PMTask.taskID>.ForeignKeyOf<INTran>.By<INTran.projectID, INTran.taskID>
    {
    }

    public class ProjectL : 
      PrimaryKeyOf<PX.Objects.PM.Lite.PMProject>.By<PX.Objects.PM.Lite.PMProject.contractID>.ForeignKeyOf<INTran>.By<INTran.projectID>
    {
    }

    public class TaskL : 
      PrimaryKeyOf<PX.Objects.PM.Lite.PMTask>.By<PX.Objects.PM.Lite.PMTask.projectID, PX.Objects.PM.Lite.PMTask.taskID>.ForeignKeyOf<INTran>.By<INTran.projectID, INTran.taskID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<INTran>.By<INTran.costCodeID>
    {
    }

    public class PIHeader : 
      PrimaryKeyOf<INPIHeader>.By<INPIHeader.pIID>.ForeignKeyOf<INTran>.By<INTran.pIID>
    {
    }

    public class PIDetail : 
      PrimaryKeyOf<INPIDetail>.By<INPIDetail.pIID, INPIDetail.lineNbr>.ForeignKeyOf<INTran>.By<INTran.pIID, INTran.pILineNbr>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTran.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.branchID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.docType>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.origModule>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.sortOrder>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INTran.tranDate>
  {
  }

  public abstract class isIntercompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTran.isIntercompany>
  {
  }

  public abstract class pOLineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.pOLineType>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INTran.invtMult>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTran.isStockItem>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.inventoryID>
  {
    public class InventoryBaseUnitRule : 
      InventoryItem.baseUnit.PreventEditIfExists<Select<INTran, Where<INTran.inventoryID, Equal<Current<InventoryItem.inventoryID>>, And<INTran.released, NotEqual<True>>>>>
    {
    }
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.locationID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.bAccountID>
  {
  }

  public abstract class destBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.destBranchID>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.sOOrderNbr>
  {
  }

  public abstract class sOOrderLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.sOOrderLineNbr>
  {
  }

  public abstract class isSpecialOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTran.isSpecialOrder>
  {
  }

  public abstract class inventorySource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.inventorySource>
  {
  }

  public abstract class costLayerType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.costLayerType>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.costCenterID>
  {
  }

  public abstract class specialOrderCostCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTran.specialOrderCostCenterID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.costCodeID>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.origDocType>
  {
  }

  public abstract class origTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.origTranType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.origRefNbr>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.origLineNbr>
  {
  }

  public abstract class origToLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.origToLocationID>
  {
  }

  public abstract class origIsLotSerial : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTran.origIsLotSerial>
  {
  }

  public abstract class origNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTran.origNoteID>
  {
  }

  public abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.acctID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.subID>
  {
  }

  public abstract class reclassificationProhibited : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTran.reclassificationProhibited>
  {
  }

  public abstract class invtAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.invtAcctID>
  {
  }

  public abstract class invtSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.invtSubID>
  {
  }

  public abstract class cOGSAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.cOGSAcctID>
  {
  }

  public abstract class cOGSSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.cOGSSubID>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.toSiteID>
  {
  }

  public abstract class toLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.toLocationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.lotSerialNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INTran.expireDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.qty>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTran.released>
  {
  }

  public abstract class releasedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTran.releasedDateTime>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.tranPeriodID>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.unitPrice>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.tranAmt>
  {
  }

  public abstract class exactCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTran.exactCost>
  {
  }

  public abstract class manualCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTran.manualCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.unitCost>
  {
  }

  public abstract class overrideUnitCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTran.overrideUnitCost>
  {
  }

  public abstract class avgCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.avgCost>
  {
  }

  public abstract class tranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.tranCost>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.tranDesc>
  {
  }

  public abstract class accrueCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTran.accrueCost>
  {
  }

  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.reasonCode>
  {
  }

  public abstract class origQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.origQty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.baseQty>
  {
  }

  public abstract class maxTransferBaseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTran.maxTransferBaseQty>
  {
  }

  public abstract class unassignedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.unassignedQty>
  {
  }

  public abstract class costedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.costedQty>
  {
  }

  public abstract class origTranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.origTranCost>
  {
  }

  public abstract class origTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.origTranAmt>
  {
  }

  public abstract class aRDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.aRDocType>
  {
  }

  public abstract class aRRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.aRRefNbr>
  {
  }

  public abstract class aRLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.aRLineNbr>
  {
  }

  public abstract class sOShipmentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.sOShipmentType>
  {
  }

  public abstract class sOShipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.sOShipmentNbr>
  {
  }

  public abstract class sOShipmentLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.sOShipmentLineNbr>
  {
  }

  public abstract class sOLineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.sOLineType>
  {
  }

  public abstract class toInventorySource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTran.toInventorySource>
  {
  }

  public abstract class toCostLayerType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.toCostLayerType>
  {
  }

  public abstract class updateShippedNotInvoiced : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTran.updateShippedNotInvoiced>
  {
  }

  public abstract class pOReceiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.pOReceiptNbr>
  {
  }

  public abstract class pOReceiptLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.pOReceiptLineNbr>
  {
  }

  public abstract class assyType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.assyType>
  {
  }

  public abstract class origPlanType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.origPlanType>
  {
  }

  public abstract class toProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.toProjectID>
  {
  }

  public abstract class toTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.toTaskID>
  {
  }

  public abstract class toCostCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.toCostCodeID>
  {
  }

  public abstract class toCostCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.toCostCenterID>
  {
  }

  public abstract class toSpecialOrderCostCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTran.toSpecialOrderCostCenterID>
  {
  }

  public abstract class receiptedBaseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTran.receiptedBaseQty>
  {
  }

  public abstract class iNTransitBaseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTran.iNTransitBaseQty>
  {
  }

  public abstract class receiptedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.receiptedQty>
  {
  }

  public abstract class iNTransitQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.iNTransitQty>
  {
  }

  public abstract class isCostUnmanaged : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTran.isCostUnmanaged>
  {
  }

  public abstract class isComponentItem : IBqlField, IBqlOperand
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTran.noteID>
  {
  }

  public abstract class pIID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.pIID>
  {
  }

  public abstract class pILineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran.pILineNbr>
  {
  }

  public abstract class origUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran.origUOM>
  {
  }

  public abstract class origFullQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.origFullQty>
  {
  }

  public abstract class baseOrigFullQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTran.baseOrigFullQty>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTran.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INTran.Tstamp>
  {
  }

  public abstract class salesMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INTran.salesMult>
  {
  }
}
