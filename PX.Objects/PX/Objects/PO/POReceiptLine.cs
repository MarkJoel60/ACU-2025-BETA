// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Purchase Receipt Line")]
public class POReceiptLine : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSPrimary,
  ILSMaster,
  IItemPlanMaster,
  ISortOrder,
  IPOReturnLineSource,
  ILSTransferPrimary,
  ILotSerialTrackableLine
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _ReceiptType;
  protected string _ReceiptNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected int? _InventoryID;
  protected string _LineType;
  protected bool? _AccrueCost;
  protected int? _VendorID;
  protected DateTime? _ReceiptDate;
  protected int? _SubItemID;
  protected string _UOM;
  protected string _POType;
  protected string _PONbr;
  protected int? _POLineNbr;
  protected short? _InvtMult;
  protected int? _SiteID;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected bool? _AllowComplete;
  protected bool? _AllowOpen;
  protected Decimal? _ReceiptQty;
  protected Decimal? _BaseReceiptQty;
  protected Decimal? _MaxTransferBaseQty;
  protected Decimal? _UnassignedQty;
  protected long? _CuryInfoID;
  protected Decimal? _CuryUnitCost;
  protected Decimal? _UnitCost;
  protected Decimal? _CuryTranUnitCost;
  protected bool? _ManualPrice;
  protected Decimal? _DiscPct;
  protected Decimal? _CuryExtCost;
  protected Decimal? _ExtCost;
  protected Decimal? _CuryDiscAmt;
  protected Decimal? _DiscAmt;
  protected Decimal? _GroupDiscountRate;
  protected Decimal? _DocumentDiscountRate;
  protected Decimal? _CuryTranCost;
  protected string _ReasonCode;
  protected int? _ExpenseAcctID;
  protected int? _ExpenseSubID;
  protected int? _POAccrualAcctID;
  protected int? _POAccrualSubID;
  protected string _AlternateID;
  protected string _TranDesc;
  protected Decimal? _UnitWeight;
  protected Decimal? _UnitVolume;
  protected Decimal? _ExtWeight;
  protected Decimal? _ExtVolume;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _CostCodeID;
  protected Guid? _NoteID;
  protected string _OrigDocType;
  protected string _OrigTranType;
  protected string _OrigRefNbr;
  protected int? _OrigLineNbr;
  protected string _SOOrderType;
  protected string _SOOrderNbr;
  protected int? _SOOrderLineNbr;
  protected string _SOShipmentType;
  protected string _SOShipmentNbr;
  protected string _OrigPlanType;
  protected bool? _Released;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected DateTime? _ExpireDate;
  protected Decimal? _OrigOrderQty;
  protected Decimal? _OpenOrderQty;
  protected Decimal? _UnbilledQty;
  protected Decimal? _BaseUnbilledQty;
  protected Decimal? _LastBaseReceivedQty;
  protected bool? _IsLSEntryBlocked;
  protected bool? _AllowEditUnitCost;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [Branch(typeof (POReceipt.branchID), null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXUIField(DisplayName = "Type")]
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault]
  public virtual string ReceiptType
  {
    get => this._ReceiptType;
    set => this._ReceiptType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXParent(typeof (POReceiptLine.FK.Receipt))]
  [PXUIField]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (POReceipt.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Line Order", Visible = false, Enabled = false)]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBBool]
  [PXUIField]
  public virtual bool? IsStockItem { get; set; }

  [PXDBString(2, IsFixed = true)]
  [POReceiptType.List]
  [PXUIField(DisplayName = "PO Receipt Type", Enabled = false)]
  public virtual string OrigReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Receipt Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<POReceipt.receiptNbr, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceipt.receiptType, Equal<BqlField<POReceiptLine.origReceiptType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<POReceipt.receiptNbr, IBqlString>.IsEqual<BqlField<POReceiptLine.origReceiptNbr, IBqlString>.FromCurrent>>>>), ValidateValue = false)]
  public virtual string OrigReceiptNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "PO Receipt Line Nbr.", Enabled = false, Visible = false)]
  public virtual int? OrigReceiptLineNbr { get; set; }

  /// <summary>
  /// Calculated field indicating if the record is of a Correction PO Receipt
  /// </summary>
  [PXBool]
  [PXFormula(typeof (BqlOperand<True, IBqlBool>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.origReceiptType, Equal<POReceiptType.poreceipt>>>>>.And<BqlOperand<POReceiptLine.receiptType, IBqlString>.IsEqual<POReceiptType.poreceipt>>>.Else<False>))]
  public virtual bool? IsCorrection { get; set; }

  [POReceiptLineInventory(typeof (POReceiptLine.receiptType), Filterable = true)]
  [PXDefault]
  [PXForeignReference(typeof (POReceiptLine.FK.InventoryItem))]
  [ConvertedInventoryItem(typeof (POReceiptLine.isStockItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("SV")]
  [POReceiptLineTypeList(typeof (POReceiptLine.inventoryID))]
  [PXUIField(DisplayName = "Line Type")]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that cost will be processed using expense accrual account.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (BqlOperand<IsNull<Selector<POReceiptLine.inventoryID, PX.Objects.IN.InventoryItem.postToExpenseAccount>, PX.Objects.IN.InventoryItem.postToExpenseAccount.purchases>, IBqlString>.IsEqual<PX.Objects.IN.InventoryItem.postToExpenseAccount.sales>))]
  [PXUIField(DisplayName = "Accrue Cost", Enabled = false, Visible = false)]
  public virtual bool? AccrueCost
  {
    get => this._AccrueCost;
    set => this._AccrueCost = value;
  }

  [PXDBBool]
  [PXDefault(typeof (POReceipt.isIntercompany))]
  public virtual bool? IsIntercompany { get; set; }

  [PXString]
  public string TranType
  {
    [PXDependsOnFields(new System.Type[] {typeof (POReceiptLine.receiptType)})] get
    {
      return POReceiptType.GetINTranType(this._ReceiptType);
    }
  }

  public virtual DateTime? TranDate => this._ReceiptDate;

  [Vendor(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>), CacheGlobal = true, Filterable = true)]
  [VerndorNonEmployeeOrOrganizationRestrictor]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBDate]
  [PXDefault]
  public virtual DateTime? ReceiptDate
  {
    get => this._ReceiptDate;
    set => this._ReceiptDate = value;
  }

  [SubItem(typeof (POReceiptLine.inventoryID))]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current2<POReceiptLine.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [PXFormula(typeof (Default<POReceiptLine.inventoryID>))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POReceiptLine.inventoryID>>>>))]
  [INUnit(typeof (POReceiptLine.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBString(2, IsFixed = true)]
  [POOrderType.List]
  [PXUIField(DisplayName = "PO Order Type")]
  public virtual string POType
  {
    get => this._POType;
    set => this._POType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Order Nbr.")]
  [PX.Objects.PO.PO.RefNbr(typeof (Search2<POOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, Where<POOrder.orderType, Equal<Optional<POReceiptLine.pOType>>, And<PX.Objects.AP.Vendor.bAccountID, IsNotNull>>, OrderBy<Desc<POOrder.orderNbr>>>), Filterable = true)]
  public virtual string PONbr
  {
    get => this._PONbr;
    set => this._PONbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "PO Line Nbr.")]
  public virtual int? POLineNbr
  {
    get => this._POLineNbr;
    set => this._POLineNbr = value;
  }

  [PXDBShort]
  [PXDefault]
  [PXUIField(DisplayName = "Inventory Multiplier")]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [POSiteAvail(typeof (POReceiptLine.inventoryID), typeof (POReceiptLine.subItemID), typeof (POReceiptLine.costCenterID), DocumentBranchType = typeof (POReceipt.branchID))]
  [PXDefault(typeof (Coalesce<Search<LocationBranchSettings.vSiteID, Where<LocationBranchSettings.locationID, Equal<Current2<POReceipt.vendorLocationID>>, And<LocationBranchSettings.bAccountID, Equal<Current2<POReceipt.vendorID>>, And<LocationBranchSettings.branchID, Equal<Current2<POReceipt.branchID>>>>>>, Search<PX.Objects.CR.Location.vSiteID, Where<PX.Objects.CR.Location.locationID, Equal<Current2<POReceipt.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current2<POReceipt.vendorID>>>>>, Search<InventoryItemCurySettings.dfltSiteID, Where<InventoryItemCurySettings.inventoryID, Equal<Current2<POReceiptLine.inventoryID>>, And<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current2<POReceipt.branchID>>>>>>))]
  [PXForeignReference(typeof (POReceiptLine.FK.Site))]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<POReceipt.branchID>>>))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [POLocationAvail(typeof (POReceiptLine.inventoryID), typeof (POReceiptLine.subItemID), typeof (POReceiptLine.costCenterID), typeof (POReceiptLine.siteID), typeof (POReceiptLine.tranType), typeof (POReceiptLine.invtMult), KeepEntry = false)]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [POLotSerialNbr(typeof (POReceiptLine.inventoryID), typeof (POReceiptLine.subItemID), typeof (POReceiptLine.locationID), typeof (POReceiptLine.costCenterID))]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXBool]
  [PXUIField]
  public virtual bool? AllowComplete
  {
    get => this._AllowComplete;
    set => this._AllowComplete = value;
  }

  [PXBool]
  [PXUIField]
  public virtual bool? AllowOpen
  {
    get => this._AllowOpen;
    set => this._AllowOpen = value;
  }

  [DBConditionalQuantity(typeof (POReceiptLine.uOM), typeof (POReceiptLine.baseReceiptQty), typeof (POReceiptLine.released), false, HandleEmptyKey = true, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<POReceipt.orderQty>))]
  [PXUIField(DisplayName = "Receipt Qty.")]
  public virtual Decimal? ReceiptQty
  {
    get => this._ReceiptQty;
    set => this._ReceiptQty = value;
  }

  public virtual Decimal? Qty
  {
    get => this._ReceiptQty;
    set => this._ReceiptQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Receipt Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseReceiptQty
  {
    get => this._BaseReceiptQty;
    set => this._BaseReceiptQty = value;
  }

  public virtual Decimal? BaseQty
  {
    get => this._BaseReceiptQty;
    set => this._BaseReceiptQty = value;
  }

  /// <summary>
  /// The actual quantity of items that were received on the RPA screen.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Received to Date", Visible = false, Enabled = false)]
  public Decimal? ReceivedToDateQty { get; set; }

  [PXDBQuantity]
  public virtual Decimal? MaxTransferBaseQty
  {
    get => this._MaxTransferBaseQty;
    set => this._MaxTransferBaseQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<POReceiptLine.baseReceiptQty, POReceiptLine.invtMult>))]
  public virtual Decimal? BaseMultReceiptQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unassigned Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? UnassignedQty
  {
    get => this._UnassignedQty;
    set => this._UnassignedQty = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (POReceipt.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrencyPriceCost(typeof (POReceiptLine.curyInfoID), typeof (POReceiptLine.unitCost))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitCost
  {
    get => this._CuryUnitCost;
    set => this._CuryUnitCost = value;
  }

  [PXDBPriceCost]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBCurrencyFixedPrecision(6, typeof (POReceiptLine.curyInfoID), typeof (POReceiptLine.tranUnitCost))]
  [PXDefault]
  public virtual Decimal? CuryTranUnitCost
  {
    get => this._CuryTranUnitCost;
    set => this._CuryTranUnitCost = value;
  }

  [PXDBDecimal(6)]
  [PXDefault]
  public virtual Decimal? TranUnitCost { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Cost", Enabled = false, Visible = true)]
  public virtual bool? ManualPrice
  {
    get => this._ManualPrice;
    set => this._ManualPrice = value;
  }

  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Discount Percent", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  [PXDBCurrency(typeof (POReceiptLine.curyInfoID), typeof (POReceiptLine.extCost))]
  [PXUIField(DisplayName = "Ext. Cost", Visible = false)]
  [PXFormula(typeof (Mult<POReceiptLine.receiptQty, POReceiptLine.curyUnitCost>), typeof (SumCalc<POReceipt.curyOrderTotal>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtCost
  {
    get => this._CuryExtCost;
    set => this._CuryExtCost = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtCost
  {
    get => this._ExtCost;
    set => this._ExtCost = value;
  }

  [PXDBCurrency(typeof (POReceiptLine.curyInfoID), typeof (POReceiptLine.discAmt))]
  [PXUIField(DisplayName = "Discount Amount")]
  [PXFormula(typeof (Div<Mult<POReceiptLine.curyExtCost, POReceiptLine.discPct>, decimal100>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscAmt
  {
    get => this._CuryDiscAmt;
    set => this._CuryDiscAmt = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt
  {
    get => this._DiscAmt;
    set => this._DiscAmt = value;
  }

  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? GroupDiscountRate
  {
    get => this._GroupDiscountRate;
    set => this._GroupDiscountRate = value;
  }

  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? DocumentDiscountRate
  {
    get => this._DocumentDiscountRate;
    set => this._DocumentDiscountRate = value;
  }

  [PXDBCurrency(typeof (POReceiptLine.curyInfoID), typeof (POReceiptLine.tranCost))]
  [PXFormula(typeof (Switch<Case<Where2<Where2<Where<POReceiptLine.manualPrice, Equal<True>, Or<POReceiptLine.curyTranUnitCost, IsNull>>, And<POReceiptLine.receiptType, NotEqual<POReceiptType.poreturn>>>, Or<Where<POReceiptLine.receiptType, Equal<POReceiptType.poreturn>, And<Where<Current<POReceipt.returnInventoryCostMode>, NotEqual<ReturnCostMode.originalCost>, Or<POReceiptLine.origReceiptNbr, IsNull, Or<POReceiptLine.curyTranUnitCost, IsNull>>>>>>>, Sub<POReceiptLine.curyExtCost, POReceiptLine.curyDiscAmt>>, Mult<POReceiptLine.receiptQty, POReceiptLine.curyTranUnitCost>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranCost
  {
    get => this._CuryTranCost;
    set => this._CuryTranCost = value;
  }

  [PXDBBaseCury]
  [PXDefault]
  [PXUIField(DisplayName = "Estimated IN Ext. Cost", Enabled = false, Visible = false)]
  public virtual Decimal? TranCost { get; set; }

  [PXDBBaseCury]
  [PXDefault]
  [PXUIField(DisplayName = "Final IN Ext. Cost", Enabled = false, Visible = false)]
  public virtual Decimal? TranCostFinal { get; set; }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<Current<POReceiptLine.receiptType>, Equal<POReceiptType.poreturn>, And<PX.Objects.CS.ReasonCode.usage, In3<ReasonCodeUsages.issue, ReasonCodeUsages.vendorReturn>, Or<Current<POReceiptLine.isCorrection>, Equal<True>, And<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.issue>>>>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField(DisplayName = "Reason Code", Visible = false)]
  [PXForeignReference(typeof (POReceiptLine.FK.ReasonCode))]
  [PXDefault]
  public virtual string ReasonCode
  {
    get => this._ReasonCode;
    set => this._ReasonCode = value;
  }

  [Account(typeof (POReceiptLine.branchID))]
  [PXRestrictor(typeof (Where<PX.Objects.GL.Account.curyID, IsNull, And<PX.Objects.GL.Account.isCashAccount, Equal<boolFalse>>>), "Cash account or denominated account cannot be specified here.", new System.Type[] {})]
  [PXDefault]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  [SubAccount(typeof (POReceiptLine.expenseAcctID), typeof (POReceiptLine.branchID), false)]
  [PXDefault]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [Account(typeof (POReceiptLine.branchID), DisplayName = "Accrual Account", Filterable = false, DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "PO")]
  [PXRestrictor(typeof (Where<PX.Objects.GL.Account.curyID, IsNull, And<PX.Objects.GL.Account.isCashAccount, Equal<boolFalse>>>), "Cash account or denominated account cannot be specified here.", new System.Type[] {})]
  [PXDefault]
  public virtual int? POAccrualAcctID
  {
    get => this._POAccrualAcctID;
    set => this._POAccrualAcctID = value;
  }

  [SubAccount(typeof (POReceiptLine.pOAccrualAcctID), typeof (POReceiptLine.branchID), false, DisplayName = "Accrual Sub.", Filterable = true)]
  [PXDefault]
  public virtual int? POAccrualSubID
  {
    get => this._POAccrualSubID;
    set => this._POAccrualSubID = value;
  }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.descr, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POReceiptLine.inventoryID>>>>))]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<PX.Objects.IN.InventoryItem.baseWeight, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POReceiptLine.inventoryID>>, And<PX.Objects.IN.InventoryItem.baseWeight, IsNotNull>>>))]
  [PXUIField(DisplayName = "Unit Weight")]
  public virtual Decimal? UnitWeight
  {
    get => this._UnitWeight;
    set => this._UnitWeight = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<PX.Objects.IN.InventoryItem.baseVolume, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POReceiptLine.inventoryID>>, And<PX.Objects.IN.InventoryItem.baseVolume, IsNotNull>>>))]
  public virtual Decimal? UnitVolume
  {
    get => this._UnitVolume;
    set => this._UnitVolume = value;
  }

  [PXDBDecimal(6)]
  [PXUIField]
  [PXFormula(typeof (Mult<Row<POReceiptLine.baseReceiptQty, POReceiptLine.receiptQty>, POReceiptLine.unitWeight>), typeof (SumCalc<POReceipt.receiptWeight>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtWeight
  {
    get => this._ExtWeight;
    set => this._ExtWeight = value;
  }

  [PXDBDecimal(6)]
  [PXUIField]
  [PXFormula(typeof (Mult<Row<POReceiptLine.baseReceiptQty, POReceiptLine.receiptQty>, POReceiptLine.unitVolume>), typeof (SumCalc<POReceipt.receiptVolume>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtVolume
  {
    get => this._ExtVolume;
    set => this._ExtVolume = value;
  }

  [POProjectDefault(typeof (POReceiptLine.lineType))]
  [PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInPO, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase(Visible = false)]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<POReceiptLine.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveProjectTask(typeof (POReceiptLine.projectID), "PO", DisplayName = "Project Task", Visible = false)]
  [PXForeignReference(typeof (CompositeKey<Field<POReceiptLine.projectID>.IsRelatedTo<PMTask.projectID>, Field<POReceiptLine.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [CostCode(ReleasedField = typeof (POReceiptLine.released))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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
  /// Denormalization of <see cref="P:PX.Objects.IN.INTransitLine.ToLocationID" />
  /// </summary>
  [PXDBInt]
  public virtual int? OrigToLocationID { get; set; }

  /// <summary>
  /// Denormalization of <see cref="P:PX.Objects.IN.INTransitLine.IsLotSerial" />
  /// </summary>
  [PXDBBool]
  public virtual bool? OrigIsLotSerial { get; set; }

  /// <summary>
  /// Denormalization of <see cref="P:PX.Objects.IN.INTransitLine.NoteID" />
  /// </summary>
  [PXDBGuid(false)]
  public virtual Guid? OrigNoteID { get; set; }

  /// <summary>
  /// Denormalization of <see cref="P:PX.Objects.IN.INTransitLine.IsFixedInTransit" />
  /// </summary>
  [PXDBBool]
  public virtual bool? OrigIsFixedInTransit { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Transfer Order Type", Enabled = false)]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Transfer Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<POReceiptLine.sOOrderType>>>>))]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Transfer Line Nbr.", Enabled = false)]
  public virtual int? SOOrderLineNbr
  {
    get => this._SOOrderLineNbr;
    set => this._SOOrderLineNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  public virtual string SOShipmentType
  {
    get => this._SOShipmentType;
    set => this._SOShipmentType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Transfer Shipment Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOShipment.shipmentNbr, Where<PX.Objects.SO.SOShipment.shipmentType, Equal<Current<POReceiptLine.sOShipmentType>>>>))]
  public virtual string SOShipmentNbr
  {
    get => this._SOShipmentNbr;
    set => this._SOShipmentNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  public virtual string OrigPlanType
  {
    get => this._OrigPlanType;
    set => this._OrigPlanType = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Released")]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// Sets up when the <see cref="P:PX.Objects.IN.INTran.Released" /> sets up (on <see cref="!:INDocumentRelease.ReleaseDoc(System.Collections.Generic.List&lt;INRegister&gt;,bool)" />)
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? INReleased { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsUnassigned { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  [POExpireDate(typeof (POReceiptLine.inventoryID))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXQuantity]
  [PXUIField(DisplayName = "Ordered Qty.")]
  [PXDependsOnFields(new System.Type[] {typeof (POReceiptLine.pOType), typeof (POReceiptLine.pONbr), typeof (POReceiptLine.pOLineNbr), typeof (POReceiptLine.inventoryID), typeof (POReceiptLine.uOM), typeof (POReceiptLine.origRefNbr), typeof (POReceiptLine.origLineNbr), typeof (POReceiptLine.origDocType)})]
  public virtual Decimal? OrigOrderQty
  {
    get => this._OrigOrderQty;
    set => this._OrigOrderQty = value;
  }

  [PXQuantity]
  [PXUIField(DisplayName = "Open Qty.")]
  [PXDependsOnFields(new System.Type[] {typeof (POReceiptLine.pOType), typeof (POReceiptLine.pONbr), typeof (POReceiptLine.pOLineNbr), typeof (POReceiptLine.inventoryID), typeof (POReceiptLine.uOM), typeof (POReceiptLine.origRefNbr), typeof (POReceiptLine.origLineNbr), typeof (POReceiptLine.origDocType)})]
  public virtual Decimal? OpenOrderQty
  {
    get => this._OpenOrderQty;
    set => this._OpenOrderQty = value;
  }

  [PXDBQuantity(typeof (POReceiptLine.uOM), typeof (POReceiptLine.baseUnbilledQty), HandleEmptyKey = true)]
  [PXFormula(null, typeof (SumCalc<POReceipt.unbilledQty>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Qty.", Enabled = false)]
  public virtual Decimal? UnbilledQty
  {
    get => this._UnbilledQty;
    set => this._UnbilledQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseUnbilledQty
  {
    get => this._BaseUnbilledQty;
    set => this._BaseUnbilledQty = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BillPPVAmt { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("R")]
  [PX.Objects.PO.POAccrualType.List]
  [PXUIField(DisplayName = "Billing Based On", Enabled = false)]
  public virtual string POAccrualType { get; set; }

  [PXDBGuid(false)]
  [PXDefault(typeof (POReceipt.noteID))]
  public virtual Guid? POAccrualRefNoteID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Current<POReceiptLine.lineNbr>))]
  public virtual int? POAccrualLineNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string OrigReceiptLineType { get; set; }

  [PXDBQuantity]
  public virtual Decimal? BaseOrigQty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseReturnedQty { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Returned Qty.", Enabled = false)]
  public virtual Decimal? ReturnedQty { get; set; }

  [PXBool]
  [PXUIField]
  [PXFormula(typeof (Selector<POReceiptLine.inventoryID, PX.Objects.IN.InventoryItem.kitItem>))]
  public virtual bool? IsKit { get; set; }

  public virtual Decimal? LastBaseReceivedQty
  {
    get => this._LastBaseReceivedQty;
    set => this._LastBaseReceivedQty = value;
  }

  [PXBool]
  public virtual bool? IsLSEntryBlocked
  {
    get => this._IsLSEntryBlocked;
    set => this._IsLSEntryBlocked = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Editable Unit Cost", Enabled = false, Visible = false)]
  [PXDefault(true)]
  public virtual bool? AllowEditUnitCost
  {
    get => this._AllowEditUnitCost;
    set => this._AllowEditUnitCost = value;
  }

  [PXDBInt]
  public virtual int? IntercompanyShipmentLineNbr { get; set; }

  [PXDecimal(4)]
  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public virtual Decimal? CuryLineAmt
  {
    get => new Decimal?();
    set
    {
    }
  }

  /// <summary>
  /// Store the <see cref="P:PX.Objects.IN.INLotSerClass.RequiredForDropship" /> value separately when creating the <see cref="T:PX.Objects.PO.POReceiptLine" />.
  /// This way, if the <see cref="P:PX.Objects.IN.INLotSerClass.RequiredForDropship" /> is changed later, it will not impact the already
  /// created <see cref="T:PX.Objects.PO.POReceiptLine" />.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? LotSerialNbrRequiredForDropship { get; set; }

  [PXDBString(1)]
  public virtual string DropshipExpenseRecording { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsSpecialOrder { get; set; }

  [PXDBInt]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Special Order Nbr.", FieldClass = "SpecialOrders")]
  [SpecialOrderCostCenterSelector(typeof (POReceiptLine.inventoryID), typeof (POReceiptLine.siteID), CostCenterIDField = typeof (POReceiptLine.costCenterID), IsSpecialOrderField = typeof (POReceiptLine.isSpecialOrder), AllowEnabled = false, CopyValueFromCostCenterID = true, DirtyRead = true)]
  public virtual int? SpecialOrderCostCenterID { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that Cancel action was applied to the receipt.
  /// Does not include receipts with a status of Canceled that have been corrected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(Enabled = false)]
  public virtual bool? CanceledWithoutCorrection { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the line of the Correction PO Receipt is adjusted manually.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Corrected", Enabled = false, Visible = false)]
  public virtual bool? IsAdjusted { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the line of the Correction PO Receipt is adjusted manually
  /// and requires inventory update.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsAdjustedIN { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the line is unreleased Correction line that was adjusted
  /// </summary>
  [PXFormula(typeof (BqlOperand<True, IBqlBool>.When<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.released, Equal<False>>>>, And<BqlOperand<POReceiptLine.isCorrection, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<POReceiptLine.isAdjusted, IBqlBool>.IsEqual<True>>>.Else<False>))]
  [PXUIField]
  [PXBool]
  public virtual bool? AllowResetCorrectionLine { get; set; }

  public class PK : 
    PrimaryKeyOf<POReceiptLine>.By<POReceiptLine.receiptType, POReceiptLine.receiptNbr, POReceiptLine.lineNbr>
  {
    public static POReceiptLine Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (POReceiptLine) PrimaryKeyOf<POReceiptLine>.By<POReceiptLine.receiptType, POReceiptLine.receiptNbr, POReceiptLine.lineNbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.branchID>
    {
    }

    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.receiptType, POReceiptLine.receiptNbr>
    {
    }

    public class OriginalReceipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.origReceiptType, POReceiptLine.origReceiptNbr>
    {
    }

    public class OriginalReceiptLine : 
      PrimaryKeyOf<POReceiptLine>.By<POReceiptLine.receiptType, POReceiptLine.receiptNbr, POReceiptLine.lineNbr>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.origReceiptType, POReceiptLine.origReceiptNbr, POReceiptLine.origReceiptLineNbr>
    {
    }

    public class AccrualStatus : 
      PrimaryKeyOf<POAccrualStatus>.By<POAccrualStatus.refNoteID, POAccrualStatus.lineNbr, POAccrualStatus.type>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.pOAccrualRefNoteID, POReceiptLine.pOAccrualLineNbr, POReceiptLine.pOAccrualType>
    {
    }

    public class OriginalPlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.origPlanType>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.inventoryID, POReceiptLine.subItemID, POReceiptLine.siteID>
    {
    }

    public class SiteStatusByCostCenter : 
      PrimaryKeyOf<INSiteStatusByCostCenter>.By<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.inventoryID, POReceiptLine.subItemID, POReceiptLine.siteID, POReceiptLine.costCenterID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.inventoryID, POReceiptLine.subItemID, POReceiptLine.siteID, POReceiptLine.locationID>
    {
    }

    public class LocationStatusByCostCenter : 
      PrimaryKeyOf<INLocationStatusByCostCenter>.By<INLocationStatusByCostCenter.inventoryID, INLocationStatusByCostCenter.subItemID, INLocationStatusByCostCenter.siteID, INLocationStatusByCostCenter.locationID, INLocationStatusByCostCenter.costCenterID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.inventoryID, POReceiptLine.subItemID, POReceiptLine.siteID, POReceiptLine.locationID, POReceiptLine.costCenterID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.inventoryID, POReceiptLine.subItemID, POReceiptLine.siteID, POReceiptLine.locationID, POReceiptLine.lotSerialNbr>
    {
    }

    public class LotSerialStatusByCostCenter : 
      PrimaryKeyOf<INLotSerialStatusByCostCenter>.By<INLotSerialStatusByCostCenter.inventoryID, INLotSerialStatusByCostCenter.subItemID, INLotSerialStatusByCostCenter.siteID, INLotSerialStatusByCostCenter.locationID, INLotSerialStatusByCostCenter.lotSerialNbr, INLotSerialStatusByCostCenter.costCenterID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.inventoryID, POReceiptLine.subItemID, POReceiptLine.siteID, POReceiptLine.locationID, POReceiptLine.lotSerialNbr, POReceiptLine.costCenterID>
    {
    }

    public class OriginalINRegister : 
      PrimaryKeyOf<PX.Objects.IN.INRegister>.By<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.origDocType, POReceiptLine.origRefNbr>
    {
    }

    public class OriginalINTran : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.origDocType, POReceiptLine.origRefNbr, POReceiptLine.origLineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.siteID>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.reasonCode>
    {
    }

    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.pOType, POReceiptLine.pONbr>
    {
    }

    public class OrderLine : 
      PrimaryKeyOf<POLine>.By<POLine.orderType, POLine.orderNbr, POLine.lineNbr>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.pOType, POReceiptLine.pONbr, POReceiptLine.pOLineNbr>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.sOOrderType, POReceiptLine.sOOrderNbr>
    {
    }

    public class SOLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.sOOrderType, POReceiptLine.sOOrderNbr, POReceiptLine.sOOrderLineNbr>
    {
    }

    public class SOShipment : 
      PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentType, PX.Objects.SO.SOShipment.shipmentNbr>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.sOShipmentType, POReceiptLine.sOShipmentNbr>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.vendorID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.curyInfoID>
    {
    }

    public class ExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.expenseAcctID>
    {
    }

    public class ExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.expenseSubID>
    {
    }

    public class AccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.pOAccrualAcctID>
    {
    }

    public class AccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.pOAccrualSubID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.projectID, POReceiptLine.taskID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<POReceiptLine>.By<POReceiptLine.costCodeID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.branchID>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.receiptType>
  {
    public const int Length = 2;
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.receiptNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.lineNbr>
  {
    public class PreventEditAccrualAcctIfPOReceiptLineExists : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<POLine.pOAccrualAcctID, POLine.pOAccrualSubID>>.On<POOrderEntry>.IfExists<Select<POReceiptLine, Where<POReceiptLine.pOType, Equal<Current<POLine.orderType>>, And<POReceiptLine.pONbr, Equal<Current<POLine.orderNbr>>, And<POReceiptLine.pOLineNbr, Equal<Current<POLine.lineNbr>>>>>>>
    {
      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object l,
        string fld,
        string tbl,
        string foreignTbl)
      {
        POReceiptLine poReceiptLine = (POReceiptLine) l;
        return PXMessages.LocalizeFormat("Cannot change accrual account because the line is used in the PO Receipt {0}, {1}.", new object[2]
        {
          (object) new POReceiptType.ListAttribute().ValueLabelDic[poReceiptLine.ReceiptType],
          (object) poReceiptLine.ReceiptNbr
        });
      }
    }
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.sortOrder>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.isStockItem>
  {
  }

  public abstract class origReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLine.origReceiptType>
  {
  }

  public abstract class origReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLine.origReceiptNbr>
  {
  }

  public abstract class origReceiptLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLine.origReceiptLineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.IsCorrection" />
  public abstract class isCorrection : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.isCorrection>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.inventoryID>
  {
    public class InventoryBaseUnitRule : 
      PX.Objects.IN.InventoryItem.baseUnit.PreventEditIfExists<Select<POReceiptLine, Where<POReceiptLine.inventoryID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>, And2<Where2<SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.goodsForInventory, POLineType.goodsForDropShip, POLineType.goodsForSalesOrder, POLineType.goodsForServiceOrder, POLineType.goodsForReplenishment, POLineType.goodsForManufacturing, POLineType.goodsForProject>>.AsStrings.Contains<POReceiptLine.lineType>, Or<SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.nonStock, POLineType.nonStockForDropShip, POLineType.nonStockForSalesOrder, POLineType.nonStockForServiceOrder, POLineType.service, POLineType.nonStockForManufacturing, POLineType.nonStockForProject>>.AsStrings.Contains<POReceiptLine.lineType>>>, And<POReceiptLine.released, NotEqual<True>>>>>>
    {
    }
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.lineType>
  {
    public const int Length = 2;
  }

  public abstract class accrueCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.accrueCost>
  {
  }

  public abstract class isIntercompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.isIntercompany>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.tranType>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.vendorID>
  {
  }

  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLine.receiptDate>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.subItemID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.uOM>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.pONbr>
  {
    public const int Length = 15;
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.pOLineNbr>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  POReceiptLine.invtMult>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.locationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.lotSerialNbr>
  {
  }

  public abstract class allowComplete : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.allowComplete>
  {
  }

  public abstract class allowOpen : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.allowOpen>
  {
  }

  public abstract class receiptQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.receiptQty>
  {
  }

  public abstract class baseReceiptQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.baseReceiptQty>
  {
  }

  public abstract class receivedToDateQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.receivedToDateQty>
  {
  }

  public abstract class maxTransferBaseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.maxTransferBaseQty>
  {
  }

  public abstract class baseMultReceiptQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.baseMultReceiptQty>
  {
  }

  public abstract class unassignedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.unassignedQty>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POReceiptLine.curyInfoID>
  {
  }

  public abstract class curyUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.curyUnitCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.unitCost>
  {
  }

  public abstract class curyTranUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.curyTranUnitCost>
  {
    public const int Precision = 6;
  }

  public abstract class tranUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.tranUnitCost>
  {
    public const int Precision = 6;
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.manualPrice>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.discPct>
  {
  }

  public abstract class curyExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.extCost>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.discAmt>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.documentDiscountRate>
  {
  }

  public abstract class curyTranCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.curyTranCost>
  {
  }

  public abstract class tranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.tranCost>
  {
  }

  public abstract class tranCostFinal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.tranCostFinal>
  {
  }

  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.reasonCode>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.expenseSubID>
  {
  }

  public abstract class pOAccrualAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.pOAccrualSubID>
  {
  }

  public abstract class alternateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.alternateID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.tranDesc>
  {
  }

  public abstract class unitWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.unitWeight>
  {
  }

  public abstract class unitVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.unitVolume>
  {
  }

  public abstract class extWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.extWeight>
  {
  }

  public abstract class extVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.extVolume>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.costCodeID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POReceiptLine.noteID>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.origDocType>
  {
  }

  public abstract class origTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.origTranType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.origRefNbr>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.origLineNbr>
  {
  }

  public abstract class origToLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLine.origToLocationID>
  {
  }

  public abstract class origIsLotSerial : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLine.origIsLotSerial>
  {
  }

  public abstract class origNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POReceiptLine.origNoteID>
  {
  }

  public abstract class origIsFixedInTransit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLine.origIsFixedInTransit>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.sOOrderNbr>
  {
  }

  public abstract class sOOrderLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.sOOrderLineNbr>
  {
  }

  public abstract class sOShipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLine.sOShipmentType>
  {
  }

  public abstract class sOShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLine.sOShipmentNbr>
  {
  }

  public abstract class origPlanType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine.origPlanType>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.released>
  {
  }

  public abstract class iNReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.iNReleased>
  {
  }

  public abstract class isUnassigned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.isUnassigned>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POReceiptLine.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POReceiptLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceiptLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLine.lastModifiedDateTime>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POReceiptLine.expireDate>
  {
  }

  public abstract class origOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.origOrderQty>
  {
  }

  public abstract class openOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.openOrderQty>
  {
  }

  public abstract class unbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.unbilledQty>
  {
  }

  public abstract class baseUnbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.baseUnbilledQty>
  {
  }

  public abstract class billPPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.billPPVAmt>
  {
  }

  public abstract class pOAccrualType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLine.pOAccrualType>
  {
  }

  public abstract class pOAccrualRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceiptLine.pOAccrualRefNoteID>
  {
  }

  public abstract class pOAccrualLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLine.pOAccrualLineNbr>
  {
  }

  public abstract class origReceiptLineType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLine.origReceiptLineType>
  {
  }

  public abstract class baseOrigQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.baseOrigQty>
  {
  }

  public abstract class baseReturnedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLine.baseReturnedQty>
  {
  }

  public abstract class returnedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.returnedQty>
  {
  }

  public abstract class isKit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.isKit>
  {
  }

  public abstract class isLSEntryBlocked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLine.isLSEntryBlocked>
  {
  }

  public abstract class allowEditUnitCost : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLine.allowEditUnitCost>
  {
  }

  public abstract class intercompanyShipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLine.intercompanyShipmentLineNbr>
  {
  }

  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public abstract class curyLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLine.curyLineAmt>
  {
  }

  public abstract class lotSerialNbrRequiredForDropship : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLine.lotSerialNbrRequiredForDropship>
  {
  }

  public abstract class dropshipExpenseRecording : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLine.dropshipExpenseRecording>
  {
  }

  public abstract class isSpecialOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.isSpecialOrder>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine.costCenterID>
  {
  }

  public abstract class specialOrderCostCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLine.specialOrderCostCenterID>
  {
  }

  public abstract class canceledWithoutCorrection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLine.canceledWithoutCorrection>
  {
  }

  public abstract class isAdjusted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.isAdjusted>
  {
  }

  public abstract class isAdjustedIN : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLine.isAdjustedIN>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.AllowResetCorrectionLine" />
  public abstract class allowResetCorrectionLine : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLine.allowResetCorrectionLine>
  {
  }
}
