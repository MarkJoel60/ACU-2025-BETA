// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.DR.Descriptor;
using PX.Objects.FA;
using PX.Objects.GL;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>An individual line of an accounts payable document.</summary>
[PXCacheName("AP Transactions")]
[Serializable]
public class APTran : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IDocumentLine,
  IDocumentTran,
  IProjectLine,
  ISortOrder,
  ITaxableDetail,
  IAccountable
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _TranType;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected int? _TranID;
  protected int? _VendorID;
  protected string _LineType;
  protected string _POOrderType;
  protected string _PONbr;
  protected int? _POLineNbr;
  protected string _ReceiptType;
  protected string _ReceiptNbr;
  protected int? _ReceiptLineNbr;
  protected int? _InventoryID;
  protected bool? _AccrueCost;
  protected int? _AccountID;
  protected int? _SubID;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _CostCodeID;
  protected short? _Box1099;
  protected string _TaxID;
  protected string _DeferredCode;
  protected System.DateTime? _DRTermStartDate;
  protected System.DateTime? _DRTermEndDate;
  protected long? _CuryInfoID;
  protected int? _SiteID;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _BaseQty;
  protected Decimal? _CuryUnitCost;
  protected Decimal? _UnitCost;
  protected Decimal? _CuryLineAmt;
  protected Decimal? _LineAmt;
  protected bool? _ManualDisc;
  protected bool? _FreezeManualDisc;
  protected bool? _SkipDisc;
  protected Decimal? _CuryDiscAmt;
  protected Decimal? _DiscAmt;
  protected Decimal? _CuryDiscCost;
  protected Decimal? _DiscCost;
  protected Decimal? _CuryTranAmt;
  protected Decimal? _TranAmt;
  protected bool? _ManualPrice;
  protected ushort[] _DiscountsAppliedToLine;
  protected Decimal? _OrigGroupDiscountRate;
  protected Decimal? _OrigDocumentDiscountRate;
  protected Decimal? _GroupDiscountRate;
  protected Decimal? _DocumentDiscountRate;
  protected string _TranClass;
  protected string _DrCr;
  protected string _FinPeriodID;
  protected string _TranDesc;
  protected bool? _Released;
  protected Decimal? _POPPVAmt;
  protected string _PPVDocType;
  protected string _PPVRefNbr;
  protected bool? _NonBillable;
  protected int? _DefScheduleID;
  protected string _LandedCostCodeID;
  protected bool? _CalculateDiscountsOnImport;
  protected Decimal? _DiscPct;
  protected string _DiscountID;
  protected string _DiscountSequenceID;
  protected string _TaxCategoryID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected Guid? _NoteID;
  protected int? _ClassID;
  protected Guid? _Custodian;
  protected int? _EmployeeID;

  /// <summary>
  /// Indicates whether the record is selected for mass processing or not.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the transaction belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
  /// </value>
  [Branch(typeof (APRegister.branchID), null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>The type of the transaction.</summary>
  /// <value>
  /// The field is determined by the type of the parent <see cref="T:PX.Objects.AP.APRegister">document</see>.
  /// For the list of possible values see <see cref="P:PX.Objects.AP.APRegister.DocType" />.
  /// </value>
  [APDocType.List]
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (APRegister.docType))]
  [PXUIField(DisplayName = "Tran. Type", Visibility = PXUIVisibility.Visible, Visible = false)]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  /// <summary>
  /// Reference number of the parent <see cref="T:PX.Objects.AP.APRegister">document</see>.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APRegister.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
  [PXParent(typeof (Select<APRegister, Where<APRegister.docType, Equal<Current<APTran.tranType>>, And<APRegister.refNbr, Equal<Current<APTran.refNbr>>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>The number of the transaction line in the document.</summary>
  /// <value>
  /// Note that the sequence of line numbers of the transactions belonging to a single document may include gaps.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
  [PXLineNbr(typeof (APRegister.lineCntr))]
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

  /// <summary>Internal unique identifier of the transaction line.</summary>
  /// <value>The value is an auto-generated database identity.</value>
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R2.")]
  [PXDBIdentity]
  public virtual int? TranID
  {
    get => this._TranID;
    set => this._TranID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.AP.Vendor" />, whom the parent document belongs.
  /// </summary>
  [Vendor]
  [PXDBDefault(typeof (APRegister.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
  /// This is non-database field. The field needs only for the discount calculation
  /// </summary>
  /// <value>
  /// An integer identifier of the vendor that supplied the goods.
  /// </value>
  [PXInt]
  [PXFormula(typeof (Switch<Case<Where<FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>>, Current<APInvoice.suppliedByVendorID>>>))]
  public virtual int? SuppliedByVendorID { get; set; }

  /// <summary>
  /// The type of the transaction line. This field is used to distinguish Discount lines from other ones.
  /// </summary>
  /// <value>
  /// Equals <c>"DS"</c> for discounts, <c>"LA"</c> for landed-cost transactions created in AP,
  /// <c>"LP"</c> for landed-cost transactions created from PO, and empty string for common lines.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("")]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  /// <summary>
  /// The type of the corresponding <see cref="T:PX.Objects.PO.POOrder">PO Order</see>.
  /// Together with <see cref="P:PX.Objects.AP.APTran.PONbr" /> and <see cref="P:PX.Objects.AP.APTran.POLineNbr" /> links APTrans to the PO Orders and their lines.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.POOrder.OrderType">POOrder.OrderType</see> field.
  /// See its description for the list of allowed values.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PX.Objects.PO.POOrderType.List]
  [PXUIField(DisplayName = "PO Type", Enabled = false, IsReadOnly = true, Visible = false)]
  public virtual string POOrderType
  {
    get => this._POOrderType;
    set => this._POOrderType = value;
  }

  /// <summary>
  /// The reference number of the corresponding <see cref="T:PX.Objects.PO.POOrder">PO Order</see>.
  /// Together with <see cref="P:PX.Objects.AP.APTran.POOrderType" /> and <see cref="P:PX.Objects.AP.APTran.POLineNbr" /> links APTrans to the PO Orders and their lines.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.POOrder.OrderNbr">POOrder.OrderNbr</see> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Number", Enabled = false, IsReadOnly = true)]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Optional<APTran.pOOrderType>>>>))]
  public virtual string PONbr
  {
    get => this._PONbr;
    set => this._PONbr = value;
  }

  /// <summary>
  /// The line number of the corresponding <see cref="T:PX.Objects.PO.POLine">PO Line</see>.
  /// Together with <see cref="P:PX.Objects.AP.APTran.POOrderType" /> and <see cref="P:PX.Objects.AP.APTran.PONbr" /> links AP transactions to the PO Orders and their lines.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.POLine.LineNbr">POLine.LineNbr</see> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "PO Line", Enabled = false, IsReadOnly = true, Visible = false)]
  public virtual int? POLineNbr
  {
    get => this._POLineNbr;
    set => this._POLineNbr = value;
  }

  /// <summary>
  /// The type of the corresponding <see cref="T:PX.Objects.PO.POLandedCostDoc">Landed Cost Document</see>.
  /// Together with <see cref="P:PX.Objects.AP.APTran.LCRefNbr" /> and <see cref="P:PX.Objects.AP.APTran.LCLineNbr" /> links APTrans to the PO Orders and their lines.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.POOrder.OrderType">POOrder.OrderType</see> field.
  /// See its description for the list of allowed values.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [POLandedCostDocType.List]
  [PXUIField(DisplayName = "LC Type", Enabled = false, IsReadOnly = true, Visible = false)]
  public virtual string LCDocType { get; set; }

  /// <summary>
  /// The reference number of the corresponding <see cref="T:PX.Objects.PO.POLandedCostDoc">Landed Cost Document</see>.
  /// Together with <see cref="P:PX.Objects.AP.APTran.LCDocType" /> and <see cref="P:PX.Objects.AP.APTran.LCLineNbr" /> links APTrans to the PO Orders and their lines.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.POLandedCostDoc.RefNbr">POLandedCostDoc.RefNbr</see> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "LC Number", Enabled = false, IsReadOnly = true, Visible = false)]
  [PXSelector(typeof (Search<POLandedCostDoc.refNbr, Where<POLandedCostDoc.docType, Equal<Optional<APTran.lCDocType>>>>))]
  public virtual string LCRefNbr { get; set; }

  /// <summary>
  /// The line number of the corresponding <see cref="T:PX.Objects.PO.POLandedCostDetail">Landed Cost Document Detail</see>.
  /// Together with <see cref="P:PX.Objects.AP.APTran.LCDocType" /> and <see cref="P:PX.Objects.AP.APTran.LCRefNbr" /> links AP transactions to the Landed Cost Document and their lines.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.POLandedCostDetail.LineNbr">POLine.LineNbr</see> field.
  /// </value>
  [PXDBInt]
  [PXParent(typeof (Select<POLandedCostDetail, Where<POLandedCostDetail.docType, Equal<Current<APTran.lCDocType>>, And<POLandedCostDetail.refNbr, Equal<Current<APTran.lCRefNbr>>, And<POLandedCostDetail.lineNbr, Equal<Current<APTran.lCRefNbr>>>>>>))]
  [PXUIField(DisplayName = "LC Line", Enabled = false, IsReadOnly = true, Visible = false)]
  public virtual int? LCLineNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [POReceiptType.List]
  [PXUIField(DisplayName = "PO Receipt Type", Enabled = false, IsReadOnly = true, Visible = false)]
  public virtual string ReceiptType
  {
    get => this._ReceiptType;
    set => this._ReceiptType = value;
  }

  /// <summary>
  /// The reference number of the corresponding <see cref="T:PX.Objects.PO.POReceipt">PO Receipt</see>.
  /// Together with <see cref="P:PX.Objects.AP.APTran.ReceiptLineNbr" /> field links AP transactions to PO Receipts and their lines.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.POReceipt.ReceiptNbr">POReceipt.ReceiptNbr</see> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Receipt Nbr.", Enabled = false, IsReadOnly = true)]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<BqlField<APTran.receiptType, IBqlString>.FromCurrent>>>))]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  /// <summary>
  /// The number of the corresponding line in the related <see cref="T:PX.Objects.PO.POReceipt">PO Receipt</see>.
  /// Together with <see cref="P:PX.Objects.AP.APTran.ReceiptNbr" /> field links AP transactions to PO Receipts and their lines.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.POReceiptLine.LineNbr">POReceiptLine.LineNbr</see> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "PO Receipt Line", Enabled = false, IsReadOnly = true, Visible = false)]
  public virtual int? ReceiptLineNbr
  {
    get => this._ReceiptLineNbr;
    set => this._ReceiptLineNbr = value;
  }

  [PXDBBool]
  public virtual bool? IsStockItem { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID">InventoryItem.InventoryID</see> field.
  /// </value>
  [APTranInventoryItem(Filterable = true)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<APTran.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  [ConvertedInventoryItem(typeof (APTran.isStockItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<APTran.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubItem(typeof (APTran.inventoryID), null, Enabled = false)]
  [PXFormula(typeof (Default<APTran.inventoryID>))]
  public virtual int? SubItemID { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that cost will be processed using expense accrual account.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (BqlOperand<IsNull<Selector<APTran.inventoryID, PX.Objects.IN.InventoryItem.postToExpenseAccount>, PX.Objects.IN.InventoryItem.postToExpenseAccount.purchases>, IBqlString>.IsEqual<PX.Objects.IN.InventoryItem.postToExpenseAccount.sales>))]
  [PXUIField(DisplayName = "Accrue Cost", Enabled = false, Visible = false)]
  public virtual bool? AccrueCost
  {
    get => this._AccrueCost;
    set => this._AccrueCost = value;
  }

  [PXString]
  [PXFormula(typeof (Switch<Case<Where<Current2<APPayment.refNbr>, PX.Data.IsNotNull>, ControlAccountModule.any, Case<Where<Current<APPayment.refNbr>, Equal<Current<APTran.refNbr>>>, ControlAccountModule.any, Case<Where<Current<APInvoice.masterRefNbr>, PX.Data.IsNotNull, And<Current<APInvoice.installmentNbr>, PX.Data.IsNotNull>>, ControlAccountModule.any, Case<Where<APTran.receiptNbr, PX.Data.IsNotNull, Or<APTran.pONbr, PX.Data.IsNotNull>>, ControlAccountModule.pO, Case<Where<Current<APInvoice.isRetainageDocument>, Equal<True>>, ControlAccountModule.aP, Case<Where<Current<APInvoice.isTaxDocument>, Equal<True>>, ControlAccountModule.tX, Case<Where<APTran.lCDocType, Equal<POLandedCostDocType.landedCost>, And<APTran.lCRefNbr, PX.Data.IsNotNull>>, ControlAccountModule.pO>>>>>>>, PX.Data.Empty>))]
  public virtual string AllowControlAccountForModule { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Account">expense account</see> to be updated by the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID">Account.AccountID</see> field. Defaults to the
  /// <see cref="P:PX.Objects.IN.InventoryItem.COGSAcctID">Cost of Goods Sold account</see> associated with the inventory item.
  /// </value>
  [Account(typeof (APTran.branchID), DisplayName = "Account", Visibility = PXUIVisibility.Visible, Filterable = false, DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true, AllowControlAccountForModuleField = typeof (APTran.allowControlAccountForModule))]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.cOGSAcctID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<APTran.inventoryID>>>>))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID">Sub.SubID</see> field. Defaults to the
  /// <see cref="P:PX.Objects.IN.InventoryItem.COGSSubID">Cost of Goods Sold subaccount</see> associated with the inventory item.
  /// </value>
  [SubAccount(typeof (APTran.accountID), typeof (APTran.branchID), true, DisplayName = "Subaccount", Visibility = PXUIVisibility.Visible, Filterable = true, TabOrder = 100)]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.cOGSSubID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<APTran.inventoryID>>>>))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> with which the item is associated or the non-project code if the item is not intended for any project.
  /// The field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.ProjectAccounting">Project Accounting</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMProject.ProjectID">PMProject.ProjectID</see> field.
  /// </value>
  [ProjectDefault("AP", AccountType = typeof (APTran.accountID))]
  [APActiveProject]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<APTran.projectID>.IsRelatedTo<PMProject.contractID>))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// Identifier of the particular <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the transaction. The task belongs to the <see cref="P:PX.Objects.AP.APTran.ProjectID">selected project</see>
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMTask.TaskID">PMTask.TaskID</see> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<APTran.projectID>>, And<PMTask.isDefault, Equal<True>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [ActiveProjectTask(typeof (APTran.projectID), "AP", CheckMandatoryCondition = typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current2<APInvoice.isRetainageDocument>, NotEqual<True>>>>>.Or<BqlOperand<Current2<APInvoice.paymentsByLinesAllowed>, IBqlBool>.IsEqual<True>>>), DisplayName = "Project Task")]
  [PXForeignReference(typeof (CompositeKey<PX.Data.ReferentialIntegrity.Attributes.Field<APTran.projectID>.IsRelatedTo<PMTask.projectID>, PX.Data.ReferentialIntegrity.Attributes.Field<APTran.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<APTran.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  [CostCode(typeof (APTran.accountID), typeof (APTran.taskID), "E", ReleasedField = typeof (APTran.released), ProjectField = typeof (APTran.projectID), VendorField = typeof (APTran.vendorID), InventoryField = typeof (APTran.inventoryID), UseNewDefaulting = true, Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof (PMCostCode.description))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.AP.AP1099Box">1099 Box</see> associated with the line.
  /// </summary>
  /// <value>
  /// Defaults to the 1099 Box associated with the <see cref="P:PX.Objects.AP.APTran.AccountID">expense account</see> or with the <see cref="P:PX.Objects.AP.APTran.VendorID">vendor</see>.
  /// </value>
  [PXDBShort]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.GL.Account.box1099, Where<PX.Objects.GL.Account.accountID, Equal<Current<APTran.accountID>>>>, Search<Vendor.box1099, Where<Vendor.bAccountID, Equal<Current<APTran.vendorID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "1099 Box", Visibility = PXUIVisibility.Visible)]
  [Box1099NumberSelector]
  public virtual short? Box1099
  {
    get => this._Box1099;
    set => this._Box1099 = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.TX.Tax">tax</see> associated with the line.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax ID", Visible = false)]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  /// <summary>
  /// The field holds one of the deferral codes defined in the system if the bill represents deferred expense.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.InventoryItem.DeferredCode">deferral code associated with the inventory item</see>
  /// with "Expense" <see cref="P:PX.Objects.DR.DRDeferredCode.AccountType">account type</see>.
  /// </value>
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Deferral Code")]
  [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID, Where<DRDeferredCode.accountType, Equal<DeferredAccountType.expense>, And<DRDeferredCode.method, NotEqual<DeferredMethodType.cashReceipt>, And<DRDeferredCode.multiDeliverableArrangement, Equal<False>>>>>))]
  [PXRestrictor(typeof (Where<DRDeferredCode.active, Equal<True>>), "The {0} deferral code is deactivated on the Deferral Codes (DR202000) form.", new System.Type[] {typeof (DRDeferredCode.deferredCodeID)})]
  [PXDefault(typeof (Search2<PX.Objects.IN.InventoryItem.deferredCode, InnerJoin<DRDeferredCode, On<DRDeferredCode.deferredCodeID, Equal<PX.Objects.IN.InventoryItem.deferredCode>>>, Where<DRDeferredCode.accountType, Equal<DeferredAccountType.expense>, And<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<APTran.inventoryID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string DeferredCode
  {
    get => this._DeferredCode;
    set => this._DeferredCode = value;
  }

  [PXDBDate]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Term Start Date")]
  public System.DateTime? DRTermStartDate
  {
    get => this._DRTermStartDate;
    set => this._DRTermStartDate = value;
  }

  [PXDBDate]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Term End Date")]
  public System.DateTime? DRTermEndDate
  {
    get => this._DRTermEndDate;
    set => this._DRTermEndDate = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the <see cref="P:PX.Objects.AP.APTran.DRTermStartDate" /> and <see cref="P:PX.Objects.AP.APTran.DRTermEndDate" />
  /// fields are enabled and should be filled for the line.
  /// </summary>
  /// <value>
  /// The value of this field is set by the <see cref="!:ARInvoiceEntry" /> and <see cref="!:ARCashSaleEntry" /> graphs
  /// based on the settings of the <see cref="P:PX.Objects.AP.APTran.InventoryID">item</see> and the <see cref="P:PX.Objects.AP.APTran.DeferredCode">Deferral Code</see> selected
  /// for the line. In other contexts it is not populated.
  /// See the attribute on the <see cref="!:ARInvoiceEntry.ARTran_RequiresTerms_CacheAttached" /> handler for details.
  /// </value>
  [PXBool]
  public virtual bool? RequiresTerms { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the transaction.
  /// </summary>
  /// <value>
  /// Generated automatically. Corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APRegister.curyInfoID), Required = true)]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBInt]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">unit of measure</see> for the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INUnit.FromUnit">INUnit.FromUnit</see> field.
  /// </value>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<APTran.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [INUnit(typeof (APTran.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>
  /// The quantity of the items or services associated with the line delivered by the vendor.
  /// </summary>
  [PXDBQuantity(typeof (APTran.uOM), typeof (APTran.baseQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseQty
  {
    get => this._BaseQty;
    set => this._BaseQty = value;
  }

  /// <summary>
  /// The unit cost of the item or service received from the vendor and associated with the line.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDBCurrencyPriceCost(typeof (APTran.curyInfoID), typeof (APTran.unitCost))]
  [PXUIField(DisplayName = "Unit Cost", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitCost
  {
    get => this._CuryUnitCost;
    set => this._CuryUnitCost = value;
  }

  /// <summary>
  /// /// <summary>
  /// The unit cost of the item or service received from the vendor and associated with the line.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(typeof (Search<INItemCost.lastCost, Where<INItemCost.inventoryID, Equal<Current<APTran.inventoryID>>, And<INItemCost.curyID, EqualBaseCuryID<Current2<APRegister.branchID>>>>>))]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  /// <summary>
  /// The extended cost of the item or service associated with the line, which is the unit price multiplied by the quantity.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.lineAmt))]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXFormula(typeof (Mult<APTran.qty, APTran.curyUnitCost>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineAmt
  {
    get => this._CuryLineAmt;
    set => this._CuryLineAmt = value;
  }

  /// <summary>
  /// The extended cost of the item or service associated with the line, which is the unit price multiplied by the quantity.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury(typeof (APTran.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineAmt
  {
    get => this._LineAmt;
    set => this._LineAmt = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the discount is applied to the line manually.
  /// In this case user may enter either the <see cref="P:PX.Objects.AP.APTran.DiscPct">discount percent</see>, or the
  /// <see cref="P:PX.Objects.AP.APTran.DiscAmt">discount amount</see> or a predefined <see cref="P:PX.Objects.AP.APTran.DiscountID">discount code</see>.
  /// This field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.VendorDiscounts">Vendor Discounts</see> feature is enabled.
  /// </summary>
  [ManualDiscountMode(typeof (APTran.curyDiscAmt), typeof (APTran.curyTranAmt), typeof (APTran.discPct), typeof (APTran.freezeManualDisc), DiscountFeatureType.VendorDiscount)]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Discount", Visibility = PXUIVisibility.Visible, Visible = false)]
  public virtual bool? ManualDisc
  {
    get => this._ManualDisc;
    set => this._ManualDisc = value;
  }

  [PXBool]
  public virtual bool? FreezeManualDisc
  {
    get => this._FreezeManualDisc;
    set => this._FreezeManualDisc = value;
  }

  [PXBool]
  public virtual bool? SkipDisc
  {
    get => this._SkipDisc;
    set => this._SkipDisc = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? AutomaticDiscountsDisabled { get; set; }

  /// <summary>
  /// The amount of the line-level discount that has been applied manually or automatically.
  /// This field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.VendorDiscounts">Vendor Discounts</see> feature is enabled.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.discAmt))]
  [PXUIField(DisplayName = "Discount Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscAmt
  {
    get => this._CuryDiscAmt;
    set => this._CuryDiscAmt = value;
  }

  /// <summary>
  /// The amount of the line-level discount that has been applied manually or automatically.
  /// This field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.VendorDiscounts">Vendor Discounts</see> feature is enabled.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt
  {
    get => this._DiscAmt;
    set => this._DiscAmt = value;
  }

  /// <summary>
  /// The unit cost of the item or service associated with the line after discount.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDBPriceCostCalced(typeof (Switch<Case<Where<APTran.qty, Equal<decimal0>>, decimal0>, Div<APTran.curyTranAmt, APTran.qty>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  [PXFormula(typeof (Div<APTran.curyTranAmt, NullIf<APTran.qty, decimal0>>))]
  [PXCurrencyPriceCost(typeof (APTran.curyInfoID), typeof (APTran.discCost))]
  [PXUIField(DisplayName = "Disc. Unit Cost", Enabled = false, Visible = false)]
  public virtual Decimal? CuryDiscCost
  {
    get => this._CuryDiscCost;
    set => this._CuryDiscCost = value;
  }

  /// <summary>
  /// The unit cost of the item or service associated with the line after discount.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBPriceCostCalced(typeof (Switch<Case<Where<APTran.qty, Equal<decimal0>>, decimal0>, Div<APTran.lineAmt, APTran.qty>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  [PXFormula(typeof (Div<Row<APTran.lineAmt, APTran.curyLineAmt>, NullIf<APTran.qty, decimal0>>))]
  public virtual Decimal? DiscCost
  {
    get => this._DiscCost;
    set => this._DiscCost = value;
  }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepayment Percent")]
  public virtual Decimal? PrepaymentPct { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.prepaymentAmt))]
  [PXFormula(typeof (BqlOperand<Div<Mult<Sub<APTran.curyLineAmt, APTran.curyDiscAmt>, APTran.prepaymentPct>, decimal100>, IBqlDecimal>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTran.tranType, Equal<APDocType.prepayment>>>>>.Or<BqlOperand<APTran.tranType, IBqlString>.IsEqual<APDocType.prepaymentInvoice>>>.Else<decimal0>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepayment Amount", Enabled = false)]
  public virtual Decimal? CuryPrepaymentAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PrepaymentAmt { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(Visibility = PXUIVisibility.Invisible)]
  public virtual Decimal? RetainagePct { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.retainageAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(Visibility = PXUIVisibility.Invisible)]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainageAmt { get; set; }

  /// <summary>
  /// The total amount for the specified quantity of items or services of this type (after discount has been taken),
  /// or the amount of debit adjustment or prepayment.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.tranAmt))]
  [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [PXFormula(typeof (BqlFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTran.tranType, Equal<APDocType.prepayment>>>>>.Or<BqlOperand<APTran.tranType, IBqlString>.IsEqual<APDocType.prepaymentInvoice>>>, APTran.curyPrepaymentAmt>>, BqlOperand<APTran.curyLineAmt, IBqlDecimal>.Subtract<APTran.curyDiscAmt>>, IBqlDecimal>.Subtract<APTran.curyRetainageAmt>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranAmt
  {
    get => this._CuryTranAmt;
    set => this._CuryTranAmt = value;
  }

  /// <summary>
  /// The total amount for the specified quantity of items or services of this type (after discount has been taken),
  /// or the amount of debit adjustment or prepayment.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury(typeof (APTran.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  public virtual Decimal? TranAmt
  {
    get => this._TranAmt;
    set => this._TranAmt = value;
  }

  /// <summary>
  /// The line amount that is subject to tax.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Net Amount", Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  /// <summary>
  /// The line amount that is subject to tax.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxableAmt { get; set; }

  /// <summary>
  /// The amount of tax (VAT) associated with the line.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "VAT", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual Decimal? CuryTaxAmt { get; set; }

  /// <summary>
  /// The amount of tax (VAT) associated with the line.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmt { get; set; }

  /// <summary>
  /// The line amount included into line balance.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.origTaxableAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Taxable Amount", Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual Decimal? CuryOrigTaxableAmt { get; set; }

  /// <summary>
  /// The line amount included into line balance.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigTaxableAmt { get; set; }

  /// <summary>
  /// The amount of tax included into line balance.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.origTaxAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual Decimal? CuryOrigTaxAmt { get; set; }

  /// <summary>
  /// The amount of tax included into line balance.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigTaxAmt { get; set; }

  /// <summary>
  /// The line amount that is subject to retained tax.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.retainedTaxableAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retained Taxable Amount", Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual Decimal? CuryRetainedTaxableAmt { get; set; }

  /// <summary>
  /// The line amount that is subject to retained tax.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainedTaxableAmt { get; set; }

  /// <summary>
  /// The amount of retained tax (VAT) associated with the line.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.retainedTaxAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retained Tax", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual Decimal? CuryRetainedTaxAmt { get; set; }

  /// <summary>
  /// The amount of retained tax (VAT) associated with the line.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainedTaxAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.cashDiscBal), BaseCalc = false)]
  [PXUIField(DisplayName = "Cash Discount Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, FieldClass = "PaymentsByLines")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryCashDiscBal { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CashDiscBal { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.origRetainageAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Retainage", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual Decimal? CuryOrigRetainageAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? OrigRetainageAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.retainageBal), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unreleased Retainage", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual Decimal? CuryRetainageBal { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainageBal { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.origTranAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Original Amount", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, FieldClass = "PaymentsByLines")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigTranAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigTranAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTran.curyInfoID), typeof (APTran.tranBal), BaseCalc = false)]
  [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, FieldClass = "PaymentsByLines")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranBal { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Balance", Enabled = false)]
  public virtual Decimal? TranBal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? ExpenseAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryExpenseAmt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Cost", Visible = false)]
  public virtual bool? ManualPrice
  {
    get => this._ManualPrice;
    set => this._ManualPrice = value;
  }

  [PXDBPackedIntegerArray]
  public virtual ushort[] DiscountsAppliedToLine
  {
    get => this._DiscountsAppliedToLine;
    set => this._DiscountsAppliedToLine = value;
  }

  [PXDBInt]
  public virtual int? OrigLineNbr { get; set; }

  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? OrigGroupDiscountRate
  {
    get => this._OrigGroupDiscountRate;
    set => this._OrigGroupDiscountRate = value;
  }

  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? OrigDocumentDiscountRate
  {
    get => this._OrigDocumentDiscountRate;
    set => this._OrigDocumentDiscountRate = value;
  }

  /// <summary>
  /// The effective rate of the group-level discount associated with the line.
  /// </summary>
  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? GroupDiscountRate
  {
    get => this._GroupDiscountRate;
    set => this._GroupDiscountRate = value;
  }

  /// <summary>
  /// The effective rate of the document-level discount associated with the line.
  /// </summary>
  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? DocumentDiscountRate
  {
    get => this._DocumentDiscountRate;
    set => this._DocumentDiscountRate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("")]
  public virtual string TranClass
  {
    get => this._TranClass;
    set => this._TranClass = value;
  }

  /// <summary>
  /// Indicates whether the line is of debit or credit type.
  /// </summary>
  /// <value>
  /// Is set to the parent's <see cref="P:PX.Objects.AP.APInvoice.DrCr" /> by default.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (APInvoice.drCr))]
  public virtual string DrCr
  {
    get => this._DrCr;
    set => this._DrCr = value;
  }

  /// <summary>The date of the transaction.</summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.AP.APRegister.DocDate">date of the parent document</see>.
  /// </value>
  [PXDBDate]
  [PXDBDefault(typeof (APRegister.docDate))]
  [PXUIField(DisplayName = "Document Date", Visible = false)]
  public virtual System.DateTime? TranDate { get; set; }

  /// <summary>
  /// <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">Financial period</see>, which the line is associated with.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.AP.APRegister.FinPeriodID">document's financial period</see>.
  /// </value>
  [PX.Objects.GL.FinPeriodID(null, typeof (APTran.branchID), null, null, null, null, true, false, null, typeof (APTran.tranPeriodID), typeof (APRegister.tranPeriodID), true, true)]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  /// <summary>The description text for the transaction.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Transaction Descr.", Visibility = PXUIVisibility.Visible)]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  /// <summary>Indicates whether the line is released or not.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// Expected purchase price variance amount associated with the line.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// The final amount will be presented in the <see cref="P:PX.Objects.AP.APTran.POPPVAmt" /> after the AP document release.
  /// </summary>
  /// <seealso cref="P:PX.Objects.PO.POReceiptLine.BillPPVAmt" />
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Estimated PPV Amount", Enabled = false, IsReadOnly = true, Visible = false)]
  public virtual Decimal? ExpectedPPVAmount { get; set; }

  /// <summary>
  /// Purchase price variance amount associated with the line.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  /// <seealso cref="P:PX.Objects.PO.POReceiptLine.BillPPVAmt" />
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "PO PPV Amount")]
  public virtual Decimal? POPPVAmt
  {
    get => this._POPPVAmt;
    set => this._POPPVAmt = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "PPV Doc. Type", Enabled = false, IsReadOnly = true, Visible = false)]
  [INDocType.List]
  public virtual string PPVDocType
  {
    get => this._PPVDocType;
    set => this._PPVDocType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PPV Ref. Nbr.", Enabled = false, IsReadOnly = true, Visible = false)]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Optional<APTran.pPVDocType>>>, OrderBy<Desc<PX.Objects.IN.INRegister.refNbr>>>), Filterable = true)]
  public virtual string PPVRefNbr
  {
    get => this._PPVRefNbr;
    set => this._PPVRefNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PX.Objects.PO.POAccrualType.List]
  [PXUIField(DisplayName = "Billing Based On", Enabled = false, IsReadOnly = true)]
  public virtual string POAccrualType { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? POAccrualRefNoteID { get; set; }

  [PXDBInt]
  public virtual int? POAccrualLineNbr { get; set; }

  [PXDBQuantity(typeof (APTran.uOM), typeof (APTran.baseUnreceivedQty), HandleEmptyKey = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? UnreceivedQty { get; set; }

  [PXDBQuantity]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? BaseUnreceivedQty { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document line is not billable in the project.
  /// The field is relevant only in case <see cref="P:PX.Objects.CS.FeaturesSet.ProjectAccounting">Project Accounting</see> feature is enabled.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Non Billable", FieldClass = "PROJECT")]
  public virtual bool? NonBillable
  {
    get => this._NonBillable;
    set => this._NonBillable = value;
  }

  /// <summary>
  /// A read-only field that shows the identifier of the <see cref="T:PX.Objects.DR.DRSchedule">schedule</see>
  /// automatically assigned to the bill based on the deferral code.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.DR.DRSchedule.ScheduleID">DRSchedule.ScheduleID</see> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Original Deferral Schedule")]
  [PXSelector(typeof (Search<DRSchedule.scheduleID, Where<DRSchedule.bAccountID, Equal<Current<APInvoice.vendorID>>, PX.Data.And<Where2<Where<Current<APTran.tranType>, NotEqual<APDocType.creditAdj>, And<Current<APTran.tranType>, NotEqual<APDocType.debitAdj>>>, PX.Data.Or<Where<DRSchedule.docType, NotEqual<Current<APTran.tranType>>, PX.Data.And<Where<Current<APTran.tranType>, Equal<APDocType.creditAdj>, Or<Current<APTran.tranType>, Equal<APDocType.debitAdj>>>>>>>>>>), SubstituteKey = typeof (DRSchedule.scheduleNbr))]
  public virtual int? DefScheduleID
  {
    get => this._DefScheduleID;
    set => this._DefScheduleID = value;
  }

  /// <summary>
  /// Expense date. When an <see cref="T:PX.Objects.EP.EPExpenseClaim">expense claim</see> is released this field is set to
  /// the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.ExpenseDate">expense date</see> in the resulting AP transactions.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Expense Date")]
  public virtual System.DateTime? Date { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PO.LandedCostCode">landed cost code</see> used to describe the specific landed costs incurred for the line.
  /// This code is one of the codes associated with the vendor.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.LandedCostCode.LandedCostCodeID">LandedCostCode.LandedCostCodeID</see> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsFixed = false)]
  [PXUIField(DisplayName = "Landed Cost Code")]
  [PXSelector(typeof (Search<LandedCostCode.landedCostCodeID>))]
  public virtual string LandedCostCodeID
  {
    get => this._LandedCostCodeID;
    set => this._LandedCostCodeID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Calculate automatic discounts on import")]
  public virtual bool? CalculateDiscountsOnImport
  {
    get => this._CalculateDiscountsOnImport;
    set => this._CalculateDiscountsOnImport = value;
  }

  /// <summary>
  /// The percent of the line-level discount, that has been applied manually or automatically.
  /// This field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.VendorDiscounts">Vendor Discounts</see> feature is enabled.
  /// </summary>
  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Discount Percent", Visible = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  /// <summary>
  /// The code of the <see cref="T:PX.Objects.AP.APDiscount">discount</see> that has been applied to this line.
  /// This field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.VendorDiscounts">Vendor Discounts</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AP.APDiscount.DiscountID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<APDiscount.discountID, Where<APDiscount.type, Equal<DiscountType.LineDiscount>, PX.Data.And<Where2<FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>, And<APDiscount.bAccountID, Equal<Current<APInvoice.suppliedByVendorID>>, Or2<Not<FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>>, And<APDiscount.bAccountID, Equal<Current<APTran.vendorID>>>>>>>>>))]
  [PXUIField(DisplayName = "Discount Code", Visible = false, Enabled = true)]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  /// <summary>
  /// The identifier of the discount sequence applied to the line.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Discount Sequence", Visible = false, Enabled = false)]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxCategory">tax category</see> associated with the line.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// Defaults to the <see cref="P:PX.Objects.IN.InventoryItem.TaxCategoryID">tax category associated with the line item</see>.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category", Visibility = PXUIVisibility.Visible)]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<APTran.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  /// <summary>Indicates whether the line is direct tax line or not.</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsDirectTaxLine { get; set; }

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
  public virtual System.DateTime? CreatedDateTime
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
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the line.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>The class of asset associated with the line.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.FA.FixedAsset.AssetID" /> field.
  /// </value>
  [PXInt]
  [PXSelector(typeof (Search2<FixedAsset.assetID, LeftJoin<FABookSettings, On<FixedAsset.assetID, Equal<FABookSettings.assetID>>, LeftJoin<FABook, On<FABookSettings.bookID, Equal<FABook.bookID>>>>, Where<FixedAsset.recordType, Equal<FARecordType.classType>, And<FABook.updateGL, Equal<True>>>>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXUIField(DisplayName = "Asset Class", Visibility = PXUIVisibility.Visible)]
  public virtual int? ClassID
  {
    get => this._ClassID;
    set => this._ClassID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.EP.EPEmployee">employee</see> responsible for the line.
  /// </summary>
  [PXGuid]
  [PXSelector(typeof (PX.Objects.EP.EPEmployee.userID), SubstituteKey = typeof (PX.Objects.EP.EPEmployee.acctCD), DescriptionField = typeof (PX.Objects.EP.EPEmployee.acctName))]
  [PXUIField(DisplayName = "Custodian")]
  public virtual Guid? Custodian
  {
    get => this._Custodian;
    set => this._Custodian = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.EP.EPEmployee">Employee</see> who created the document line.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:EP.EPEmployee.BAccountID">EPEmployee.BAccountID</see> field.
  /// This field is not visible to user and cannot be changed by them.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (Search<PX.Objects.EP.EPEmployee.bAccountID, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  /// <summary>
  /// Read-only field showing the sign of the line. Calculated based on the <see cref="P:PX.Objects.AP.APTran.DrCr" /> field.
  /// </summary>
  /// <value>
  /// <c>1</c> for debit lines and <c>-1</c> for credit ones.
  /// </value>
  public Decimal Sign => !(this.DrCr == "D") ? -1M : 1M;

  /// <summary>
  /// Read-only field showing the line quantity multiplied by the line sign.
  /// Based on the <see cref="P:PX.Objects.AP.APTran.Qty" /> and <see cref="P:PX.Objects.AP.APTran.Sign" /> fields.
  /// </summary>
  [PXQuantity]
  public virtual Decimal? SignedQty
  {
    [PXDependsOnFields(new System.Type[] {typeof (APTran.qty), typeof (APTran.drCr)})] get
    {
      Decimal? qty = this._Qty;
      Decimal sign = this.Sign;
      return !qty.HasValue ? new Decimal?() : new Decimal?(qty.GetValueOrDefault() * sign);
    }
  }

  /// <summary>
  /// Read-only field showing the signed line amount.
  /// Based on the <see cref="P:PX.Objects.AP.APTran.CuryTranAmt" /> and <see cref="P:PX.Objects.AP.APTran.Sign" /> fields.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDecimal]
  public virtual Decimal? SignedCuryTranAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (APTran.curyTranAmt), typeof (APTran.drCr)})] get
    {
      Decimal? curyTranAmt = this._CuryTranAmt;
      Decimal sign = this.Sign;
      return !curyTranAmt.HasValue ? new Decimal?() : new Decimal?(curyTranAmt.GetValueOrDefault() * sign);
    }
  }

  /// <summary>
  /// Read-only field showing the signed line amount.
  /// Based on the <see cref="P:PX.Objects.AP.APTran.CuryTranAmt" /> and <see cref="P:PX.Objects.AP.APTran.Sign" /> fields.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXBaseCury]
  public virtual Decimal? SignedTranAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (APTran.tranAmt), typeof (APTran.drCr)})] get
    {
      Decimal? tranAmt = this._TranAmt;
      Decimal sign = this.Sign;
      return !tranAmt.HasValue ? new Decimal?() : new Decimal?(tranAmt.GetValueOrDefault() * sign);
    }
  }

  [PXDBString(1)]
  public virtual string DropshipExpenseRecording { get; set; }

  string IDocumentLine.Module => "AP";

  /// <summary>
  /// Determines whether the <i>View Deferrals</i> grid action should be enabled or disabled on the <see cref="T:PX.Objects.AP.APInvoiceEntry">AP301000</see> screen
  /// </summary>
  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<Exists<Select<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAP>, And<DRSchedule.docType, Equal<APTran.tranType>, And<DRSchedule.refNbr, Equal<APTran.refNbr>, And<DRSchedule.lineNbr, Equal<APTran.lineNbr>>>>>>>>, True, Case<Where<APTran.deferredCode, PX.Data.IsNotNull>, True>>, False>), typeof (bool))]
  [PXUIField(DisplayName = "", Visibility = PXUIVisibility.Invisible, Visible = false, Enabled = false)]
  public virtual bool? CanViewDeferralSchedule { get; set; }

  public class PK : PrimaryKeyOf<APTran>.By<APTran.tranType, APTran.refNbr, APTran.lineNbr>
  {
    public static APTran Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APTran>.By<APTran.tranType, APTran.refNbr, APTran.lineNbr>.FindBy(graph, (object) tranType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APTran>.By<APTran.tranType, APTran.refNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<APInvoice>.By<APInvoice.docType, APInvoice.refNbr>.ForeignKeyOf<APTran>.By<APTran.tranType, APTran.refNbr>
    {
    }

    public class Payment : 
      PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>.ForeignKeyOf<APTran>.By<APTran.tranType, APTran.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APTran>.By<APTran.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APTran>.By<APTran.vendorID>
    {
    }

    public class POOrder : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<APTran>.By<APTran.pOOrderType, APTran.pONbr>
    {
    }

    public class POLine : 
      PrimaryKeyOf<PX.Objects.PO.POLine>.By<PX.Objects.PO.POLine.orderType, PX.Objects.PO.POLine.orderNbr, PX.Objects.PO.POLine.lineNbr>.ForeignKeyOf<APTran>.By<APTran.pOOrderType, APTran.pONbr, APTran.pOLineNbr>
    {
    }

    public class SuppliedByVendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APTran>.By<APTran.suppliedByVendorID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<APTran>.By<APTran.inventoryID>
    {
    }

    public class LandedCostDocument : 
      PrimaryKeyOf<POLandedCostDoc>.By<POLandedCostDoc.docType, POLandedCostDoc.refNbr>.ForeignKeyOf<APTran>.By<APTran.lCDocType, APTran.lCRefNbr>
    {
    }

    public class LandedCostLine : 
      PrimaryKeyOf<POLandedCostDetail>.By<POLandedCostDetail.docType, POLandedCostDetail.refNbr, POLandedCostDetail.lineNbr>.ForeignKeyOf<APTran>.By<APTran.lCDocType, APTran.lCRefNbr, APTran.lCLineNbr>
    {
    }

    public class POReceipt : 
      PrimaryKeyOf<PX.Objects.PO.POReceipt>.By<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>.ForeignKeyOf<APTran>.By<APTran.receiptType, APTran.receiptNbr>
    {
    }

    public class POReceiptLine : 
      PrimaryKeyOf<PX.Objects.PO.POReceiptLine>.By<PX.Objects.PO.POReceiptLine.receiptType, PX.Objects.PO.POReceiptLine.receiptNbr, PX.Objects.PO.POReceiptLine.lineNbr>.ForeignKeyOf<APTran>.By<APTran.receiptType, APTran.receiptNbr, APTran.receiptLineNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APTran>.By<APTran.curyInfoID>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<APTran>.By<APTran.taxID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<APTran>.By<APTran.taxCategoryID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APTran>.By<APTran.accountID>
    {
    }

    public class Subaccount : PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APTran>.By<APTran.subID>
    {
    }

    public class DeferralCode : 
      PrimaryKeyOf<DRDeferredCode>.By<DRDeferredCode.deferredCodeID>.ForeignKeyOf<APTran>.By<APTran.deferredCode>
    {
    }

    public class Site : PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<APTran>.By<APTran.siteID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTran.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.branchID>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.sortOrder>
  {
    public const string DispalyName = "Line Order";
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R2.")]
  public abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.tranID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.vendorID>
  {
  }

  public abstract class suppliedByVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.suppliedByVendorID>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.lineType>
  {
  }

  public abstract class pOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.pOOrderType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.pOLineNbr>
  {
    public class PreventEditAccrualAcctIfAPTranExists : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<PX.Objects.PO.POLine.pOAccrualAcctID, PX.Objects.PO.POLine.pOAccrualSubID>>.On<POOrderEntry>.IfExists<Select<APTran, Where<APTran.pOOrderType, Equal<Current<PX.Objects.PO.POLine.orderType>>, And<APTran.pONbr, Equal<Current<PX.Objects.PO.POLine.orderNbr>>, And<APTran.pOLineNbr, Equal<Current<PX.Objects.PO.POLine.lineNbr>>, And<APTran.tranType, NotEqual<APDocType.prepayment>>>>>>>
    {
      protected override string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object t,
        string fld,
        string tbl,
        string foreignTbl)
      {
        APTran apTran = (APTran) t;
        return PXMessages.LocalizeFormat("Cannot change accrual account because the line is used in the AP Document {0}, {1}.", (object) new APDocType.ListAttribute().ValueLabelDic[apTran.TranType], (object) apTran.RefNbr);
      }
    }
  }

  public abstract class lCDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.lCDocType>
  {
  }

  public abstract class lCRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.lCRefNbr>
  {
  }

  public abstract class lCLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.lCLineNbr>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.receiptNbr>
  {
  }

  public abstract class receiptLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.receiptLineNbr>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTran.isStockItem>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.subItemID>
  {
  }

  public abstract class accrueCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTran.accrueCost>
  {
  }

  public abstract class allowControlAccountForModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTran.allowControlAccountForModule>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.subID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.costCodeID>
  {
  }

  public abstract class box1099 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  APTran.box1099>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.taxID>
  {
  }

  public abstract class deferredCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.deferredCode>
  {
  }

  public abstract class dRTermStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APTran.dRTermStartDate>
  {
  }

  public abstract class dRTermEndDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APTran.dRTermEndDate>
  {
  }

  public abstract class requiresTerms : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTran.requiresTerms>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APTran.curyInfoID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.siteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.baseQty>
  {
  }

  public abstract class curyUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyUnitCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.unitCost>
  {
  }

  public abstract class curyLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyLineAmt>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.lineAmt>
  {
  }

  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTran.manualDisc>
  {
  }

  public abstract class freezeManualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTran.freezeManualDisc>
  {
  }

  public abstract class skipDisc : IBqlField, IBqlOperand
  {
  }

  public abstract class automaticDiscountsDisabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APTran.automaticDiscountsDisabled>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.discAmt>
  {
  }

  public abstract class curyDiscCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyDiscCost>
  {
  }

  public abstract class discCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.discCost>
  {
  }

  public abstract class prepaymentPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.prepaymentPct>
  {
  }

  public abstract class curyPrepaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.curyPrepaymentAmt>
  {
  }

  public abstract class prepaymentAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.prepaymentAmt>
  {
  }

  public abstract class retainagePct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.retainagePct>
  {
  }

  public abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.curyRetainageAmt>
  {
  }

  public abstract class retainageAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.retainageAmt>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.tranAmt>
  {
  }

  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.taxAmt>
  {
  }

  public abstract class curyOrigTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.curyOrigTaxableAmt>
  {
  }

  public abstract class origTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.origTaxableAmt>
  {
  }

  public abstract class curyOrigTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyOrigTaxAmt>
  {
  }

  public abstract class origTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.origTaxAmt>
  {
  }

  public abstract class curyRetainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.curyRetainedTaxableAmt>
  {
  }

  public abstract class retainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.retainedTaxableAmt>
  {
  }

  public abstract class curyRetainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.curyRetainedTaxAmt>
  {
  }

  public abstract class retainedTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.retainedTaxAmt>
  {
  }

  public abstract class curyCashDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyCashDiscBal>
  {
  }

  public abstract class cashDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.cashDiscBal>
  {
  }

  public abstract class curyOrigRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.curyOrigRetainageAmt>
  {
  }

  public abstract class origRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.origRetainageAmt>
  {
  }

  public abstract class curyRetainageBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.curyRetainageBal>
  {
  }

  public abstract class retainageBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.retainageBal>
  {
  }

  public abstract class curyOrigTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyOrigTranAmt>
  {
  }

  public abstract class origTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.origTranAmt>
  {
  }

  public abstract class curyTranBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyTranBal>
  {
  }

  public abstract class tranBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.tranBal>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.expenseAmt>
  {
  }

  public abstract class curyExpenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.curyExpenseAmt>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTran.manualPrice>
  {
  }

  public abstract class discountsAppliedToLine : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTran.discountsAppliedToLine>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.origLineNbr>
  {
  }

  public abstract class origGroupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.origGroupDiscountRate>
  {
  }

  public abstract class origDocumentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.origDocumentDiscountRate>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.documentDiscountRate>
  {
  }

  public abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.tranClass>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.drCr>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APTran.tranDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.finPeriodID>
  {
  }

  public abstract class tranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.tranDesc>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTran.released>
  {
  }

  public abstract class expectedPPVAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.expectedPPVAmount>
  {
  }

  public abstract class pOPPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.pOPPVAmt>
  {
  }

  public abstract class pPVDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.pPVDocType>
  {
  }

  public abstract class pPVRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.pPVRefNbr>
  {
  }

  public abstract class pOAccrualType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.pOAccrualType>
  {
  }

  public abstract class pOAccrualRefNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APTran.pOAccrualRefNoteID>
  {
  }

  public abstract class pOAccrualLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.pOAccrualLineNbr>
  {
  }

  public abstract class unreceivedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.unreceivedQty>
  {
  }

  public abstract class baseUnreceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.baseUnreceivedQty>
  {
  }

  public abstract class nonBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTran.nonBillable>
  {
  }

  public abstract class defScheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.defScheduleID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APTran.date>
  {
  }

  public abstract class landedCostCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.landedCostCodeID>
  {
  }

  public abstract class calculateDiscountsOnImport : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APTran.calculateDiscountsOnImport>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.discPct>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTran.discountSequenceID>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTran.taxCategoryID>
  {
  }

  public abstract class isDirectTaxLine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTran.isDirectTaxLine>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APTran.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APTran.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APTran.noteID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.classID>
  {
  }

  public abstract class custodian : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APTran.custodian>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTran.employeeID>
  {
  }

  public abstract class signedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.signedQty>
  {
  }

  public abstract class signedCuryTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTran.signedCuryTranAmt>
  {
  }

  public abstract class signedTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTran.signedTranAmt>
  {
  }

  public abstract class dropshipExpenseRecording : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTran.dropshipExpenseRecording>
  {
  }

  public abstract class canViewDeferralSchedule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APTran.canViewDeferralSchedule>
  {
  }
}
