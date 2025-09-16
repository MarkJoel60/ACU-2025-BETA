// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProformaLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>The base class for the <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see> line.
/// The class provides the fields that are common to the <see cref="T:PX.Objects.PM.PMProformaProgressLine" />
/// and <see cref="T:PX.Objects.PM.PMProformaTransactLine" /> types.</summary>
[PXCacheName("Pro Forma Line")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMProformaLine : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ISortOrder,
  IQuantify,
  IProjectFilter
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _RefNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected string _Type;
  protected int? _BranchID;
  protected string _Description;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _InventoryID;
  protected int? _CostCodeID;
  protected int? _AccountGroupID;
  protected int? _ResourceID;
  protected int? _VendorID;
  protected DateTime? _Date;
  protected int? _SubID;
  protected string _UOM;
  protected long? _CuryInfoID;
  protected Decimal? _BillableQty;
  /// <summary>
  /// The amount to bill the customer provided by the billing rule in the base currency.
  /// </summary>
  protected Decimal? _BillableAmount;
  protected Decimal? _Qty;
  protected string _Option;
  protected bool? _Released;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// The reference number of the parent <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see>.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search<PMProforma.refNbr>), Filterable = true)]
  [PXUIField]
  [PXDBDefault(typeof (PMProforma.refNbr))]
  [PXFormula(null, typeof (CountCalc<PMProforma.numberOfLines>))]
  [PXParent(typeof (Select<PMProforma, Where<PMProforma.refNbr, Equal<Current<PMProformaLine.refNbr>>, And<PMProforma.revisionID, Equal<Current<PMProformaLine.revisionID>>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>The revision number of the parent <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see>.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProforma.RevisionID" /> field.
  /// </value>
  [PXUIField(DisplayName = "Revision", Visible = false)]
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PMProforma.revisionID))]
  public virtual int? RevisionID { get; set; }

  /// <summary>
  /// The original sequence number of the line among all the pro forma invoice lines.
  /// </summary>
  /// <remarks>The sequence of line numbers of the pro forma invoice lines belonging to a single document can include gaps.</remarks>
  [PXUIField(DisplayName = "Line Number")]
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (PMProforma.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  /// <summary>
  /// The sequence number of the line, which is used to sort the lines on the tab.
  /// These numbers are assigned automatically and are changed automatically when reordering lines by dragging them to appropriate positions.
  /// </summary>
  [PXUIField(DisplayName = "Sort Order", Visible = false)]
  [PXDBInt]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  /// <summary>The type of the pro forma invoice line.</summary>
  /// <value>The field can have one of the following values and regulates on which tab (<strong>Progress Billing</strong> or <strong>Time and Material
  /// Billing</strong>) of the Pro Forma Invoices (PM307000) form the invoice line appears: <c>"P"</c>: Progressive, <c>"T"</c>: Transaction</value>
  [PXDBString(1)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> associated with the pro forma invoice line.</summary>
  /// <value>
  /// The branch is provided from the source defined by the <see cref="P:PX.Objects.PM.PMBillingRule.BranchSource">Use Destination Branch from</see> setting of the particular step of the billing rule.
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(typeof (PMProforma.branchID), null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// The description of the line, which is provided by the billing rule and can be manually modified.
  /// </summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the pro forma invoice line.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.PM.PMProforma.ProjectID">project</see> of the parent pro forma invoice.
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (PMProforma.projectID))]
  [PXForeignReference(typeof (Field<PMProformaLine.projectID>.IsRelatedTo<PMProject.contractID>))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the pro forma invoice line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMProformaLine.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveProjectTask(typeof (PMProformaLine.projectID), "AR", DisplayName = "Project Task", AllowCompleted = true, Enabled = false, Required = true)]
  [PXForeignReference(typeof (CompositeKey<Field<PMProformaLine.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMProformaLine.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the pro forma invoice line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXUIField(DisplayName = "Inventory ID", Enabled = false)]
  [PXDBInt]
  [PMInventorySelector]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the pro forma invoice line.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMCostCode.CostCodeID" /> field.
  /// </value>
  [CostCode(typeof (PMProformaLine.accountID), typeof (PMProformaLine.taskID), "I", ReleasedField = typeof (PMProformaLine.released), Required = true)]
  [PXForeignReference(typeof (Field<PMProformaLine.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> associated with the pro forma invoice line.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXDBInt]
  [PXForeignReference(typeof (Field<PMProformaLine.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  /// <summary>The identifier of the original cost <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> associated with the pro forma invoice line.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXDBInt]
  [PXForeignReference(typeof (Field<PMProformaLine.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public virtual int? OrigAccountGroupID { get; set; }

  /// <summary>
  /// The line number of the progress billing record to which this transaction line is merged.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Progress Billing Line Nbr.", Enabled = false)]
  public virtual int? MergedToLineNbr { get; set; }

  /// <summary>
  /// The identifier of the employee associated with the pro forma invoice line.
  /// </summary>
  [PXEPEmployeeSelector]
  [PXDBInt]
  [PXUIField(DisplayName = "Employee", Enabled = false)]
  public virtual int? ResourceID
  {
    get => this._ResourceID;
    set => this._ResourceID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.AP.Vendor">vendor</see> associated with the pro forma invoice line.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [Vendor(Enabled = false)]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>The date of the pro forma invoice line.</summary>
  /// <value>Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">business date</see>.</value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  /// <summary>The identifier of the sales <see cref="T:PX.Objects.GL.Account">account</see> associated with the pro forma invoice line.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(typeof (PMProformaLine.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where<PX.Objects.GL.Account.accountGroupID, IsNotNull>>))]
  public virtual int? AccountID { get; set; }

  /// <summary>
  /// The identifier of the sales <see cref="T:PX.Objects.GL.Sub">subaccount</see> associated with the pro forma invoice line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (PMProformaLine.accountID), typeof (PMProformaLine.branchID), true)]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.TX.TaxCategory">tax category</see> associated with the pro forma invoice line.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [PMTax(typeof (PMProforma), typeof (PMTax), typeof (PMTaxTran))]
  [PMRetainedTax(typeof (PMProforma), typeof (PMTax), typeof (PMTaxTran))]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMProformaLine.inventoryID>>>>))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public virtual string TaxCategoryID { get; set; }

  /// <summary>The <see cref="T:PX.Objects.IN.INUnit">unit of measure</see> for the <see cref="P:PX.Objects.PM.PMProformaLine.Qty">quantity</see> associated with the pro forma invoice line.</summary>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMProformaLine.inventoryID>>>>))]
  [PMUnit(typeof (PMProformaLine.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>An identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">currency info</see> object associated with the pro forma invoice line.</summary>
  [PXDBLong]
  [CurrencyInfo(typeof (PMProforma.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>The price of the item or the rate of the service.</summary>
  [PXDBCurrencyPriceCost(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.unitPrice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price")]
  public virtual Decimal? CuryUnitPrice { get; set; }

  /// <summary>
  /// The price of the item or the rate of the service in the base currency.
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price in Base Currency")]
  public virtual Decimal? UnitPrice { get; set; }

  /// <summary>
  /// The percentage of the revised budgeted amount of the revenue budget line of the project
  /// that has been invoiced by all the pro forma invoices of the project, including the current one.
  /// </summary>
  [PXDecimal(2, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Completed (%)")]
  public virtual Decimal? CompletedPct { get; set; }

  /// <summary>
  /// The percentage of the revised budgeted amount of the revenue budget line
  /// of the project that is invoiced by this pro forma invoice line.
  /// </summary>
  [PXDecimal(2, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Currently Invoiced (%)")]
  public virtual Decimal? CurrentInvoicedPct { get; set; }

  /// <summary>
  /// The quantity to bill the customer provided by the billing rule.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billed Quantity", Enabled = false)]
  public virtual Decimal? BillableQty
  {
    get => this._BillableQty;
    set => this._BillableQty = value;
  }

  /// <summary>The amount to bill the customer provided by the billing rule.</summary>
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.billableAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billed Amount", Enabled = false)]
  public virtual Decimal? CuryBillableAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billed Amount in Base Currency", Enabled = false)]
  public virtual Decimal? BillableAmount
  {
    get => this._BillableAmount;
    set => this._BillableAmount = value;
  }

  /// <summary>The quantity to bill the customer. The value can be manually modified.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity to Invoice")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  /// <summary>The line amount.</summary>
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.amount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", FieldClass = "Construction")]
  public virtual Decimal? CuryAmount { get; set; }

  /// <summary>The line amount in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", FieldClass = "Construction")]
  public virtual Decimal? Amount { get; set; }

  /// <summary>
  /// The field is reserved for a feature that is currently not supported.
  /// </summary>
  /// <exclude />
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.prepaidAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Applied")]
  public virtual Decimal? CuryPrepaidAmount { get; set; }

  /// <summary>
  /// The field is reserved for a feature that is currently not supported.
  /// </summary>
  /// <exclude />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Applied in Base Currency")]
  public virtual Decimal? PrepaidAmount { get; set; }

  /// <summary>The amount of stored material.</summary>
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.materialStoredAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Stored Material", FieldClass = "Construction")]
  public virtual Decimal? CuryMaterialStoredAmount { get; set; }

  /// <summary>The amount of stored material (in the base currency).</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Stored Material in Base Currency", FieldClass = "Construction")]
  public virtual Decimal? MaterialStoredAmount { get; set; }

  /// <summary>
  /// The amount that is merged to the progress billing line.
  /// </summary>
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.mergedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Included in Progress Billing", Enabled = false)]
  public virtual Decimal? CuryMergedAmount { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? MergedAmount { get; set; }

  /// <summary>The amount of material stored.</summary>
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.timeMaterialAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Time and Material Amount", Enabled = false)]
  public virtual Decimal? CuryTimeMaterialAmount { get; set; }

  /// <summary>The amount of material stored in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Time and Material Amount in Base Currency")]
  public virtual Decimal? TimeMaterialAmount { get; set; }

  /// <summary>The amount that is billed to the customer.</summary>
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.lineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount to Invoice")]
  public virtual Decimal? CuryLineTotal { get; set; }

  /// <summary>The amount to bill the customer in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount To Invoice in Base Currency")]
  public virtual Decimal? LineTotal { get; set; }

  /// <summary>
  /// The percent of the invoice line amount to be retained by the customer.
  /// </summary>
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage (%)", FieldClass = "Retainage")]
  public virtual Decimal? RetainagePct { get; set; }

  /// <summary>The amount to be retained by the customer.</summary>
  /// <value>The amount is calculated by multiplying the values of <see cref="P:PX.Objects.PM.PMProformaLine.CuryLineTotal">Amount to Invoice</see> and <see cref="P:PX.Objects.PM.PMProformaLine.RetainagePct">Retainage</see>.</value>
  [PXFormula(typeof (Mult<PMProformaLine.curyLineTotal, Div<PMProformaLine.retainagePct, decimal100>>))]
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.retainage))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Amount", FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainage { get; set; }

  /// <summary>
  /// The amount to be retained by the customer in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retained Amount in Base Currency", FieldClass = "Retainage")]
  public virtual Decimal? Retainage { get; set; }

  /// <summary>The allocated retained amount.</summary>
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.allocatedRetainedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Allocated Retained Amount", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? CuryAllocatedRetainedAmount { get; set; }

  /// <summary>The allocated retained amount (in the base currency).</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AllocatedRetainedAmount { get; set; }

  /// <summary>The status that defines how to bill the line.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <list>
  /// <item><description><c>N</c>: Bill</description></item>
  /// <item><description><c>C</c>: Write Off Remainder</description></item>
  /// <item><description><c>U</c>: Hold Remainder</description></item>
  /// <item><description><c>X</c>: Write Off</description></item>
  /// </list>
  /// </value>
  [PXDefault("N")]
  [PXDBString]
  [PXUIField(DisplayName = "Status")]
  public virtual string Option
  {
    get => this._Option;
    set => this._Option = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the parent <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see> has been released.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Released")]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the parent <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see> has been corrected.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Corrected")]
  [PXDefault(false)]
  public virtual bool? Corrected { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the line is merged into the progress line.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Include in Progress Billing")]
  [PXDefault(false)]
  public virtual bool? Merged { get; set; }

  /// <summary>
  /// The field is reserved for a feature that is currently not supported.
  /// </summary>
  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsPrepayment { get; set; }

  /// <summary>
  /// The deferral code assigned to the stock item or non-stock item specified in this document line.
  /// </summary>
  [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID, Where<DRDeferredCode.accountType, Equal<DeferredAccountType.income>>>), DescriptionField = typeof (DRDeferredCode.description))]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Deferral Code", FieldClass = "DEFFERED")]
  public virtual string DefCode { get; set; }

  /// <summary>The reference to a revenue budget line by task.</summary>
  [ProjectTask(typeof (PMProformaLine.projectID), typeof (Where<PMTask.type, NotEqual<ProjectTaskType.cost>>), AlwaysEnabled = true, AllowNull = true, DisplayName = "Revenue Task", SkipDefaultTask = true)]
  public virtual int? RevenueTaskID { get; set; }

  /// <summary>The type of the corresponding <see cref="!:ARInvoice">accounts receivable document</see> created on the release of the pro forma invoice.</summary>
  [PXDBString(3)]
  public virtual string ARInvoiceDocType { get; set; }

  /// <summary>The reference number of the corresponding <see cref="T:PX.Objects.AR.ARInvoice">accounts receivable document</see> created on the release of the pro forma invoice.</summary>
  [PXDBString(15, IsUnicode = true)]
  public virtual string ARInvoiceRefNbr { get; set; }

  /// <summary>The <see cref="P:PX.Objects.AR.ARTran.LineNbr">line number</see> of the corresponding accounts receivable document created on the release of the pro forma invoice.</summary>
  [PXDBInt]
  public virtual int? ARInvoiceLineNbr { get; set; }

  /// <summary>
  /// The value that the system has used as the basis for the progress billing operation that has generated this pro forma invoice line.
  /// </summary>
  [PXDBString]
  [PXUIField(DisplayName = "Progress Billing Basis")]
  [PX.Objects.PM.ProgressBillingBase.List]
  public virtual string ProgressBillingBase { get; set; }

  /// <summary>The running total of the <see cref="P:PX.Objects.PM.PMProformaLine.CuryLineTotal">amount to invoice</see> column for all the lines of preceding pro forma invoices that refer to the same revenue budget line.
  /// The preceding pro forma invoices are the pro forma invoices that have a reference number that is less than the reference number of the current pro forma
  /// invoice, and have the same project budget key (that is, the same project task, account group, and optionally inventory item or cost code).</summary>
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.previouslyInvoiced), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Previously Invoiced Amount", Enabled = false)]
  public virtual Decimal? CuryPreviouslyInvoiced { get; set; }

  /// <summary>The running total of the <strong>Amount to I<span>nvoice</span></strong> column in the base currency for all the lines of preceding pro forma invoices that
  /// refer to the same revenue budget line.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Previously Invoiced in Base Currency", Enabled = false)]
  public virtual Decimal? PreviouslyInvoiced { get; set; }

  /// <summary>
  /// The running total of the Quantity to Invoice column
  /// for all the lines of preceding pro forma invoices that refer to the same revenue budget line.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Previously Invoiced Quantity", Enabled = false)]
  public virtual Decimal? PreviouslyInvoicedQty { get; set; }

  /// <summary>
  /// The total quantity of the lines of the released accounts receivable invoices that correspond to the budget line.
  /// </summary>
  [PXDBQuantity]
  [PXUIField(DisplayName = "Actual Quantity", Enabled = false)]
  public virtual Decimal? ActualQty { get; set; }

  [PXNote(DescriptionField = typeof (PMProformaLine.refNbr))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

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

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProformaLine.selected>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaLine.refNbr>
  {
    public const int Length = 15;
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.revisionID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.sortOrder>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaLine.type>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.branchID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaLine.description>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.taskID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.inventoryID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.costCodeID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.accountGroupID>
  {
  }

  public abstract class origAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaLine.origAccountGroupID>
  {
  }

  public abstract class mergedToLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.mergedToLineNbr>
  {
  }

  public abstract class resourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.resourceID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.vendorID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMProformaLine.date>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.subID>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaLine.taxCategoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaLine.uOM>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMProformaLine.curyInfoID>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.curyUnitPrice>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProformaLine.unitPrice>
  {
  }

  public abstract class completedPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.completedPct>
  {
    public const int Precision = 2;
  }

  public abstract class currentInvoicedPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.currentInvoicedPct>
  {
  }

  public abstract class billableQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProformaLine.billableQty>
  {
  }

  public abstract class curyBillableAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.curyBillableAmount>
  {
  }

  public abstract class billableAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.billableAmount>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProformaLine.qty>
  {
  }

  public abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProformaLine.curyAmount>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProformaLine.amount>
  {
  }

  public abstract class curyPrepaidAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.curyPrepaidAmount>
  {
  }

  public abstract class prepaidAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.prepaidAmount>
  {
  }

  public abstract class curyMaterialStoredAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.curyMaterialStoredAmount>
  {
  }

  public abstract class materialStoredAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.materialStoredAmount>
  {
  }

  public abstract class curyMergedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.curyMergedAmount>
  {
  }

  public abstract class mergedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.mergedAmount>
  {
  }

  public abstract class curyTimeMaterialAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.curyTimeMaterialAmount>
  {
  }

  public abstract class timeMaterialAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.timeMaterialAmount>
  {
  }

  public abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.curyLineTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProformaLine.lineTotal>
  {
  }

  public abstract class retainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.retainagePct>
  {
    public const int Precision = 6;
  }

  public abstract class curyRetainage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.curyRetainage>
  {
  }

  public abstract class retainage : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProformaLine.retainage>
  {
  }

  /// <exclude />
  public abstract class curyAllocatedRetainedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.curyAllocatedRetainedAmount>
  {
  }

  /// <exclude />
  public abstract class allocatedRetainedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.allocatedRetainedAmount>
  {
  }

  public abstract class option : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaLine.option>
  {
    public const string BillNow = "N";
    public const string WriteOffRemainder = "C";
    public const string HoldRemainder = "U";
    public const string Writeoff = "X";

    public class holdRemainder : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PMProformaLine.option.holdRemainder>
    {
      public holdRemainder()
        : base("U")
      {
      }
    }

    public class writeoff : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMProformaLine.option.writeoff>
    {
      public writeoff()
        : base("X")
      {
      }
    }

    public class bill : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMProformaLine.option.bill>
    {
      public bill()
        : base("N")
      {
      }
    }

    public class writeOffRemainder : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PMProformaLine.option.writeOffRemainder>
    {
      public writeOffRemainder()
        : base("C")
      {
      }
    }
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProformaLine.released>
  {
  }

  public abstract class corrected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProformaLine.corrected>
  {
  }

  public abstract class merged : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProformaLine.merged>
  {
  }

  public abstract class isPrepayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProformaLine.isPrepayment>
  {
  }

  public abstract class defCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaLine.defCode>
  {
  }

  public abstract class revenueTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLine.revenueTaskID>
  {
  }

  public abstract class aRInvoiceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaLine.aRInvoiceDocType>
  {
  }

  public abstract class aRInvoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaLine.aRInvoiceRefNbr>
  {
  }

  public abstract class aRInvoiceLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaLine.aRInvoiceLineNbr>
  {
  }

  public abstract class progressBillingBase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.progressBillingBase>
  {
  }

  public abstract class curyPreviouslyInvoiced : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.curyPreviouslyInvoiced>
  {
  }

  public abstract class previouslyInvoiced : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.previouslyInvoiced>
  {
  }

  public abstract class previouslyInvoicedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLine.previouslyInvoicedQty>
  {
  }

  public abstract class actualQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProformaLine.actualQty>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProformaLine.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMProformaLine.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProformaLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProformaLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMProformaLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProformaLine.lastModifiedDateTime>
  {
  }
}
