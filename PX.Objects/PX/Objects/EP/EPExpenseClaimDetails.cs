// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPExpenseClaimDetails
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using PX.SM;
using PX.TM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Contains the main properties of the expense receipt document, which is a record reflecting that an employee performed
/// a transaction while working for your organization, thus incurring certain expenses.
/// An expense receipt can be edited on the Expense Receipt (EP301020) form (which corresponds to the <see cref="T:PX.Objects.EP.ExpenseClaimDetailEntry" /> graph).
/// </summary>
[PXPrimaryGraph(typeof (ExpenseClaimDetailEntry))]
[PXCacheName("Expense Receipt")]
[Serializable]
public class EPExpenseClaimDetails : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAssign,
  INotable
{
  public const 
  #nullable disable
  string DocType = "ECD";
  protected int? _CostCodeID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  /// <summary>
  /// The identifier of the receipt record in the system, which the system assigns automatically when you save a newly entered receipt.
  /// This field is the key field.
  /// </summary>
  [PXDBIdentity]
  [PXUIField]
  public virtual int? ClaimDetailID { get; set; }

  /// <summary>The user-friendly unique identifier of the receipt.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [EPExpenseClaimDetails.claimDetailCD.Numbering(typeof (EPSetup.receiptNumberingID), typeof (EPExpenseClaimDetails.expenseDate))]
  [PXDefault]
  [EPExpenceReceiptSelector]
  [PXFieldDescription]
  public virtual string ClaimDetailCD { get; set; }

  /// <summary>
  /// The company branch that will incur the expenses. If multiple expense receipts associated with different branches are added to one expense claim,
  /// the branch specified for the claim on the Financial Details tab of the Expense Claim (EP301000) form (which corresponds to the <see cref="T:PX.Objects.EP.ExpenseClaimEntry" /> graph)
  /// will reimburse the expenses and the branches specified in this box for the receipts will incur the expenses.
  /// </summary>
  [Branch(typeof (EPExpenseClaim.branchID), null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The reference number, which usually matches the number of the original receipt.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXDBDefault(typeof (EPExpenseClaim.refNbr))]
  [PXParent(typeof (Select<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Current2<EPExpenseClaimDetails.refNbr>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search<EPExpenseClaim.refNbr>), DescriptionField = typeof (EPExpenseClaim.docDesc), ValidateValue = false, DirtyRead = true)]
  public virtual string RefNbr { get; set; }

  /// <summary>
  /// The service field that is used in PXFormula for the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.HoldClaim" /> and <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.StatusClaim" /> fields, which don't need restrictions of original RefNbr in a selector.
  /// </summary>
  [PXString(15, IsUnicode = true)]
  [PXFormula(typeof (Current<EPExpenseClaimDetails.refNbr>))]
  [PXVirtualSelector(typeof (Search<EPExpenseClaim.refNbr>), ValidateValue = false)]
  public virtual string RefNbrNotFiltered
  {
    get => this.RefNbr;
    set
    {
    }
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.EPEmployee">employee</see> who is claiming the expenses.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.EP.EPEmployee.bAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (EPExpenseClaim.employeeID))]
  [PXSubordinateAndWingmenSelector(typeof (EPDelegationOf.expenses))]
  [PXUIField]
  [PXForeignReference(typeof (Field<EPExpenseClaimDetails.employeeID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? EmployeeID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.CR.Contact">Contact</see> responsible
  /// for the document approval process.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [Owner]
  public virtual int? OwnerID { get; set; }

  /// <summary>
  /// The workgroup that is responsible for the document approval process.
  /// </summary>
  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Workgroup")]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the expense receipt has the On Hold <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.Status">status</see>,
  /// which means that the receipt can be edited but cannot be added to a claim and released.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(Visible = false)]
  public virtual bool? Hold { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the expense receipt has been approved by a responsible person
  /// and has the Approved <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.Status">status</see>.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the expense receipt has been rejected by a responsible person.
  /// When the receipt is rejected, its <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.Status">status</see> changes to Rejected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public bool? Rejected { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
  /// </summary>
  /// <value>
  /// Is generated automatically and corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo(CuryIDField = "CuryID", CuryDisplayName = "Currency")]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// The code of the <see cref="T:PX.Objects.CM.Currency">currency</see> of the document.
  /// By default, the receipt currency is the currency specified as the default for the employee.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isActive, Equal<True>>>))]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> record that stores
  /// exchange rate from the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.CardCuryID">corporate card currency</see> to the base currency.
  /// </summary>
  [PXDBLong]
  [CurrencyInfo(CuryIDField = "CardCuryID")]
  public virtual long? CardCuryInfoID { get; set; }

  /// <summary>The currency of the corporate card.</summary>
  [PXUIField(Enabled = false)]
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  public virtual string CardCuryID { get; set; }

  /// <summary>
  /// The code of the <see cref="T:PX.Objects.CM.Currency">currency</see> of the claim in which the current receipt is included.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
  /// </value>
  [PXDBLong]
  [CurrencyInfo(typeof (EPExpenseClaim.curyInfoID), CuryIDField = "ClaimCuryID", CuryDisplayName = "Claim Currency")]
  public virtual long? ClaimCuryInfoID { get; set; }

  /// <summary>
  /// The date of the receipt. By default, the current business date is used when a new receipt is created.
  /// </summary>
  [PXDBDate]
  [PXDefault(typeof (Search<EPExpenseClaim.docDate, Where<EPExpenseClaim.refNbr, Equal<Current<EPExpenseClaimDetails.refNbr>>>>))]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? ExpenseDate { get; set; }

  /// <summary>
  /// The total amount of taxes associated with the document in the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.CuryID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.taxTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  /// <summary>
  /// The total amount of taxes associated with the document
  /// in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  /// <summary>
  /// A fake field for correct working a Tax attribute. Always must be zero because a tip is tax exempt.
  /// The total amount of tips taxes associated with the document in the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.CuryID">currency of the document</see>.
  /// </summary>
  [PXCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.taxTipTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTipTotal { get; set; }

  /// <summary>
  /// A fake field for correct working a Tax attribute. Always must be zero because a tip is tax exempt.
  /// The total amount of tips taxes associated with the document
  /// in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTipTotal { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.taxRoundDiff), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Discrepancy", Enabled = false)]
  public Decimal? CuryTaxRoundDiff { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? TaxRoundDiff { get; set; }

  /// <summary>
  /// The reference number, which usually matches the number of the original receipt.
  /// </summary>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Ref. Nbr.")]
  public virtual string ExpenseRefNbr { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.InventoryItem">non-stock item</see>  of the expense, which determines the financial accounts,
  /// the default tax category, and the unit of measure used for the receipt.
  /// </summary>
  [PXDefault]
  [Inventory(DisplayName = "Expense Item")]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.expenseItem>>), "Only inventory items of the Expense type can be selected.", new System.Type[] {})]
  [PXForeignReference(typeof (Field<EPExpenseClaimDetails.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.TX.TaxZone">tax zone</see> associated with the receipt.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Zone", Required = false)]
  [PXDefault(typeof (Coalesce<Search<EPEmployee.receiptAndClaimTaxZoneID, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaimDetails.employeeID>>>>, Search2<PX.Objects.CR.Location.vTaxZoneID, RightJoin<EPEmployee, On<EPEmployee.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaimDetails.employeeID>>>>>))]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  public virtual string TaxZoneID { get; set; }

  /// <summary>
  /// The tax calculation mode, which defines which amounts (tax-inclusive or tax-exclusive)
  /// should be entered in the detail lines of a document.
  /// This field is displayed only if the <see cref="P:PX.Objects.CS.FeaturesSet.NetGrossEntryMode" /> field is set to <c>true</c>.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"T"</c> (Tax Settings): The tax amount for the document is calculated according to the settings of the applicable tax or taxes.
  /// <c>"G"</c> (Gross): The amount in the document detail line includes a tax or taxes.
  /// <c>"N"</c> (Net): The amount in the document detail line does not include taxes.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T")]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  /// <summary>The tax category associated with the expense item.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXFormula(typeof (Selector<EPExpenseClaimDetails.inventoryID, PX.Objects.IN.InventoryItem.taxCategoryID>))]
  public virtual string TaxCategoryID { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether withholding taxes are applied to the document.
  /// This is a technical field, which is calculated on the fly and is used to restrict the values of the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.TaxCalcMode" /> field.
  /// </summary>
  [PXBool]
  [RestrictWithholdingTaxCalcMode(typeof (EPExpenseClaimDetails.taxZoneID), typeof (EPExpenseClaimDetails.taxCalcMode))]
  public virtual bool? HasWithHoldTax { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether use taxes are applied to the document.
  /// This is a technical field, which is calculated on the fly and is used to restrict the values of the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.TaxCalcMode" /> field.
  /// </summary>
  [PXBool]
  [RestrictUseTaxCalcMode(typeof (EPExpenseClaimDetails.taxZoneID), typeof (EPExpenseClaimDetails.taxCalcMode))]
  public virtual bool? HasUseTax { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">unit of measure</see> of the expense item.
  /// </summary>
  [PXDefault]
  [INUnit(typeof (EPExpenseClaimDetails.inventoryID), DisplayName = "UOM")]
  [PXUIEnabled(typeof (Where<EPExpenseClaimDetails.inventoryID, IsNotNull, And<FeatureInstalled<FeaturesSet.multipleUnitMeasure>>>))]
  [PXFormula(typeof (Switch<Case<Where<EPExpenseClaimDetails.inventoryID, IsNull>, Null>, Selector<EPExpenseClaimDetails.inventoryID, PX.Objects.IN.InventoryItem.purchaseUnit>>))]
  public virtual string UOM { get; set; }

  /// <summary>
  /// The amount of non-taxable tips in the document currency that will not be included in the tax base of the receipt.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.tipAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [EPTaxTip]
  [PXUIVerify]
  public virtual Decimal? CuryTipAmt { get; set; }

  /// <summary>
  /// The amount of non-taxable tips in the base currency of the tenant that will not be included in the tax base of the receipt.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Tip Amount")]
  public virtual Decimal? TipAmt { get; set; }

  /// <summary>The tax category associated with the tip item.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  public virtual string TaxTipCategoryID { get; set; }

  /// <summary>
  /// The quantity of the expense item that the employee purchased according to the receipt.
  /// The quantity is expressed in the <see cref="T:PX.Objects.IN.INUnit">unit of measure</see> specified
  /// for the selected expense <see cref="T:PX.Objects.IN.InventoryItem">non-stock item</see>.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField]
  [PXUIVerify]
  public virtual Decimal? Qty { get; set; }

  /// <summary>
  /// The cost of one unit of the expense item in the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.CuryID">currency of the document</see>.
  /// If a standard cost is specified for the expense <see cref="T:PX.Objects.IN.InventoryItem">non-stock item</see>, the standard cost is used as the default unit cost.
  /// </summary>
  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.unitCost))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(typeof (Search<INItemCost.lastCost, Where<INItemCost.inventoryID, Equal<Current<EPExpenseClaimDetails.inventoryID>>>>))]
  public virtual Decimal? UnitCost { get; set; }

  /// <summary>
  /// The part of the total amount that will not be paid back to the employee in the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.CuryID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.employeePart), MinValue = 0.0)]
  [PXUIField(DisplayName = "Employee Part")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIEnabled(typeof (Where<EPExpenseClaimDetails.paidWith, NotEqual<EPExpenseClaimDetails.paidWith.cash>, Or<EPExpenseClaimDetails.curyExtCost, GreaterEqual<decimal0>>>))]
  [PXFormula(typeof (Switch<Case<Where<EPExpenseClaimDetails.paidWith, NotEqual<EPExpenseClaimDetails.paidWith.cash>, Or<EPExpenseClaimDetails.curyExtCost, LessEqual<decimal0>>>, decimal0>, EPExpenseClaimDetails.curyEmployeePart>))]
  public virtual Decimal? CuryEmployeePart { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Employee Part")]
  public virtual Decimal? EmployeePart { get; set; }

  /// <summary>
  /// The total amount of the receipt in the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.CuryID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.extCost))]
  [PXUIField(DisplayName = "Amount")]
  [PXFormula(typeof (Mult<EPExpenseClaimDetails.qty, EPExpenseClaimDetails.curyUnitCost>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtCost { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Total Amount")]
  public virtual Decimal? ExtCost { get; set; }

  /// <summary>
  /// The tax amount to be paid for the document in the document currency.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxAmt { get; set; }

  /// <summary>
  /// The tax amount to be paid for the document in the base currency of the tenant.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmt { get; set; }

  /// <summary>The taxable amount in the currency of the document.</summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.taxableAmtFromTax))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxableAmtFromTax { get; set; }

  /// <summary>
  /// The taxable amount in the base currency of the tenant.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxableAmtFromTax { get; set; }

  /// <summary>The taxable amount in the currency of the document.</summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.taxableAmt))]
  [PXFormula(typeof (Sub<EPExpenseClaimDetails.curyExtCost, EPExpenseClaimDetails.curyEmployeePart>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  /// <summary>
  /// The taxable amount in the base currency of the tenant.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxableAmt { get; set; }

  /// <summary>
  /// The amount to be reimbursed to the employee, which is calculated as the difference between the total amount
  /// and the employee part in the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.CuryID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.tranAmt))]
  [PXFormula(typeof (Add<EPExpenseClaimDetails.curyTaxableAmt, EPExpenseClaimDetails.curyTipAmt>))]
  [PXUIField(DisplayName = "Claim Amount", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranAmt { get; set; }

  /// <summary>
  /// The amount to be reimbursed to the employee in the currency of the document.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.tranAmtWithTaxes))]
  [PXFormula(typeof (Add<EPExpenseClaimDetails.curyAmountWithTaxes, EPExpenseClaimDetails.curyTipAmt>))]
  [PXUIField(DisplayName = "Claim Amount", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranAmtWithTaxes { get; set; }

  /// <summary>
  /// The amount of the expense receipt with taxes in the currency of the document.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAmountWithTaxes { get; set; }

  /// <summary>
  /// The amount to be reimbursed to the employee, which is calculated as the difference between the total amount
  /// and the employee part in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Claim Amount")]
  public virtual Decimal? TranAmt { get; set; }

  /// <summary>
  /// The amount to be reimbursed to the employee in the base currency of the tenant.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Claim Amount with Taxes")]
  public virtual Decimal? TranAmtWithTaxes { get; set; }

  /// <summary>
  /// The amount to be reimbursed to the employee, which is calculated as the difference between the total amount
  /// and the employee part in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">currency of the claim</see> in which the current receipt is included.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.claimCuryInfoID), typeof (EPExpenseClaimDetails.claimTranAmt))]
  [PXUIField(DisplayName = "Amount in Claim Curr.", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClaimCuryTranAmt { get; set; }

  /// <summary>The net amount in the currency of the document.</summary>
  [PXCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.netAmount))]
  [PXFormula(typeof (Sub<EPExpenseClaimDetails.curyTranAmtWithTaxes, EPExpenseClaimDetails.curyTaxTotal>))]
  [PXUIField(DisplayName = "Net Amount", Enabled = false, Visible = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryNetAmount { get; set; }

  /// <summary>The net amount in the base currency of the tenant.</summary>
  [PXDecimal(4)]
  public virtual Decimal? NetAmount { get; set; }

  /// <summary>
  /// The amount to be reimbursed to the employee, which is calculated as the difference between the total amount
  /// and the employee part in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company of the claim</see> in which the current receipt is included.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount in Claim Original")]
  public virtual Decimal? ClaimTranAmt { get; set; }

  /// <summary>
  /// The amount claimed by the employee, which is expressed in the currency of the expense claim.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.claimCuryInfoID), typeof (EPExpenseClaimDetails.claimTranAmtWithTaxes))]
  [PXUIField(DisplayName = "Amount in Claim Curr.", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClaimCuryTranAmtWithTaxes { get; set; }

  /// <summary>
  /// The amount (in the base currency of the tenant) that the employee has specified
  /// in the claim in which the current receipt is included.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount in Claim Original")]
  public virtual Decimal? ClaimTranAmtWithTaxes { get; set; }

  /// <summary>
  /// The total amount (in the currency of the expense claim) of taxes that are associated with the document.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.claimCuryInfoID), typeof (EPExpenseClaimDetails.taxTotal), BaseCalc = false)]
  [PXUIField(DisplayName = "Amount in Claim Curr.", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClaimCuryTaxTotal { get; set; }

  /// <summary>
  /// The total amount of taxes (in the base currency of the tenant)
  /// for the claim in which the current receipt is included.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount in Claim Original")]
  public virtual Decimal? ClaimTaxTotal { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the expense claim currency.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.claimCuryInfoID), typeof (EPExpenseClaimDetails.claimTaxRoundDiff))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClaimCuryTaxRoundDiff { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the
  /// base currency of the tenant for the claim in which the current receipt is included.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClaimTaxRoundDiff { get; set; }

  /// <summary>
  /// The document total that is exempt from VAT in the expense claim currency.
  /// This total is calculated as the taxable amount for the tax with the <see cref="P:PX.Objects.TX.Tax.ExemptTax" /> field set to <see langword="true" />
  /// (that is, with the Include in VAT Exempt Total check box selected on the Taxes (TX205000) form).
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.claimCuryInfoID), typeof (EPExpenseClaimDetails.claimVatExemptTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClaimCuryVatExemptTotal { get; set; }

  /// <summary>
  /// The document total that is exempt from VAT in the base currency
  /// of the tenant for the claim in which the current receipt is included.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClaimVatExemptTotal { get; set; }

  /// <summary>
  /// The document total that is subjected to VAT in the expense claim currency.
  /// The field is displayed only if the <see cref="P:PX.Objects.TX.Tax.IncludeInTaxable" /> field is set to <see langword="true" />
  /// (that is, the Include in VAT Exempt Total check box is selected on the Taxes (TX205000) form).
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.claimCuryInfoID), typeof (EPExpenseClaimDetails.claimVatTaxableTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClaimCuryVatTaxableTotal { get; set; }

  /// <summary>
  /// The document total that is subjected to VAT in the base currency
  /// of the tenant for the claim in which the current receipt is included.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClaimVatTaxableTotal { get; set; }

  /// <summary>
  /// The document total that is exempt from VAT in the selected currency.
  /// This total is calculated as the taxable amount for the tax
  /// with the <see cref="P:PX.Objects.TX.Tax.ExemptTax" /> field set to <see langword="true" /> (that is, with the Include in VAT Exempt Total check box selected on the Taxes (TX205000) form).
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.vatExemptTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  /// <summary>
  /// The document total that is exempt from VAT in the base currency of the tenant.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  /// <summary>
  /// The document total that is subjected to VAT in the selected currency.
  /// The field is displayed only if
  /// the <see cref="P:PX.Objects.TX.Tax.IncludeInTaxable" /> field is set to <see langword="true" /> (that is, the Include in VAT Exempt Total check box is selected on the Taxes (TX205000) form).
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.vatTaxableTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  /// <summary>
  /// The document total that is subjected to VAT in the base currency of the tenant.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  /// <summary>The description of the expense.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string TranDesc { get; set; }

  [PXDefault(typeof (EPExpenseClaim.customerID))]
  [CustomerActive(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
  [PXUIRequired(typeof (EPExpenseClaimDetails.billable))]
  [PXUIEnabled(typeof (Where<EPExpenseClaimDetails.contractID, IsNull, Or<Selector<EPExpenseClaimDetails.contractID, PX.Objects.CT.Contract.nonProject>, Equal<True>, Or<Selector<EPExpenseClaimDetails.contractID, PX.Objects.CT.Contract.customerID>, IsNull>>>))]
  [PXUIVerify]
  [PXFormula(typeof (Switch<Case<Where<Selector<EPExpenseClaimDetails.contractID, PX.Objects.CT.Contract.nonProject>, Equal<False>>, Selector<EPExpenseClaimDetails.contractID, PX.Objects.CT.Contract.customerID>>, Null>))]
  public virtual int? CustomerID { get; set; }

  /// <summary>The location of the customer related to the expenses.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PX.Objects.AR.Customer.defLocationID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<EPExpenseClaimDetails.customerID>>>>))]
  [PXUIRequired(typeof (EPExpenseClaimDetails.billable))]
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current2<EPExpenseClaimDetails.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXUIEnabled(typeof (Where<Current2<EPExpenseClaimDetails.customerID>, IsNotNull, And<Where<EPExpenseClaimDetails.contractID, IsNull, Or<Selector<EPExpenseClaimDetails.contractID, PX.Objects.CT.Contract.nonProject>, Equal<True>, Or<Selector<EPExpenseClaimDetails.contractID, PX.Objects.CT.Contract.customerID>, IsNull>>>>>))]
  [PXFormula(typeof (Switch<Case<Where<Current2<EPExpenseClaimDetails.customerID>, IsNull>, Null>, Selector<EPExpenseClaimDetails.customerID, PX.Objects.AR.Customer.defLocationID>>))]
  public virtual int? CustomerLocationID { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.CT.Contract.ContractID">project or contract</see>, which should be specified if the
  /// employee incurred the expenses while working on a particular project or contract.
  /// The value of this field can be specified only if the Project Accounting or Contract Management feature,
  /// respectively, is enabled on the Enable/Disable Features (CS100000) form.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Project/Contract")]
  [PXDimensionSelector("CONTRACT", typeof (Search2<PX.Objects.CT.Contract.contractID, LeftJoin<EPEmployeeContract, On<EPEmployeeContract.contractID, Equal<PX.Objects.CT.Contract.contractID>, And<EPEmployeeContract.employeeID, Equal<Current2<EPExpenseClaimDetails.employeeID>>>>>, Where<PX.Objects.CT.Contract.isActive, Equal<True>, And<PX.Objects.CT.Contract.isCompleted, Equal<False>, And<Where<PX.Objects.CT.Contract.nonProject, Equal<True>, Or2<Where<PX.Objects.CT.Contract.baseType, Equal<CTPRType.contract>, And<FeatureInstalled<FeaturesSet.contractManagement>>>, Or<PX.Objects.CT.Contract.baseType, Equal<CTPRType.project>, And2<Where<PX.Objects.CT.Contract.visibleInEA, Equal<True>>, And2<FeatureInstalled<FeaturesSet.projectAccounting>, And2<Match<Current<AccessInfo.userName>>, And<Where<PX.Objects.CT.Contract.restrictToEmployeeList, Equal<False>, Or<EPEmployeeContract.employeeID, IsNotNull>>>>>>>>>>>>, OrderBy<Desc<PX.Objects.CT.Contract.contractCD>>>), typeof (PX.Objects.CT.Contract.contractCD), new System.Type[] {typeof (PX.Objects.CT.Contract.contractCD), typeof (PX.Objects.CT.Contract.description), typeof (PX.Objects.CT.Contract.customerID), typeof (PX.Objects.CT.Contract.status)}, Filterable = true, ValidComboRequired = true, CacheGlobal = true, DescriptionField = typeof (PX.Objects.CT.Contract.description), DescriptionDisplayName = "Project/Contract Description")]
  [ProjectDefault("EA", AccountType = typeof (EPExpenseClaimDetails.expenseAccountID))]
  public virtual int? ContractID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMTask">project task</see> to which the expenses are related.
  /// This box is available only if the Project Management feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </summary>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<EPExpenseClaimDetails.contractID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [EPExpenseAllowProjectTask(typeof (EPExpenseClaimDetails.contractID), "EA", DisplayName = "Project Task")]
  [PXUIEnabled(typeof (Where<EPExpenseClaimDetails.contractID, IsNotNull, And<Selector<EPExpenseClaimDetails.contractID, PX.Objects.CT.Contract.baseType>, Equal<CTPRType.project>>>))]
  [PXFormula(typeof (Switch<Case<Where<EPExpenseClaimDetails.contractID, IsNull, Or<Selector<EPExpenseClaimDetails.contractID, PX.Objects.CT.Contract.baseType>, NotEqual<CTPRType.project>>>, Null>, EPExpenseClaimDetails.taskID>))]
  [PXForeignReference(typeof (CompositeKey<Field<EPExpenseClaimDetails.contractID>.IsRelatedTo<PMTask.projectID>, Field<EPExpenseClaimDetails.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the record.
  /// </summary>
  [CostCode(typeof (EPExpenseClaimDetails.expenseAccountID), typeof (EPExpenseClaimDetails.taskID), "E", ReleasedField = typeof (EPExpenseClaimDetails.released), ProjectField = typeof (EPExpenseClaimDetails.contractID), InventoryField = typeof (EPExpenseClaimDetails.inventoryID), UseNewDefaulting = true, DescriptionField = typeof (PMCostCode.description))]
  [PXForeignReference(typeof (Field<EPExpenseClaimDetails.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the customer should be billed for the claim amount.
  /// You can use the Bill Expense Claims (EP502000) form to bill the customer if no project is specified.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Billable")]
  public virtual bool? Billable { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that current receipt was billed.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Billed")]
  public virtual bool? Billed { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the expense receipt was released
  /// and has the Released <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.Status">status</see>.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Visible = false)]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Account">expense account</see> to which the system records the part of the expense to be paid back to the employee.
  /// </summary>
  [PXDefault]
  [PXFormula(typeof (Selector<EPExpenseClaimDetails.inventoryID, PX.Objects.IN.InventoryItem.cOGSAcctID>))]
  [Account]
  [PXUIVerify]
  public virtual int? ExpenseAccountID { get; set; }

  /// <summary>
  /// The corresponding <see cref="T:PX.Objects.GL.Sub">subaccount</see> the system uses to record the part of the expense to be paid back to the employee.
  /// The segments of the expense subaccount are combined according to the settings specified on the Time and Expenses Preferences (EP101000) form.
  /// </summary>
  [PXDefault]
  [PXFormula(typeof (Default<EPExpenseClaimDetails.inventoryID, EPExpenseClaimDetails.contractID, EPExpenseClaimDetails.taskID, EPExpenseClaimDetails.customerLocationID>))]
  [PXFormula(typeof (Default<EPExpenseClaimDetails.employeeID, EPExpenseClaimDetails.branchID, EPExpenseClaimDetails.billable>))]
  [SubAccount(typeof (EPExpenseClaimDetails.expenseAccountID), typeof (EPExpenseClaimDetails.branchID), false)]
  public virtual int? ExpenseSubID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Account">sales account</see> to which the system records the part of the amount to charge the customer for.
  /// If the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.Billable">Billable</see> check box is selected, the sales account specified for the expense non-stock item is filled in by default.
  /// </summary>
  [PXDefault]
  [PXFormula(typeof (ExpenseClaimDetailSalesAccountID<EPExpenseClaimDetails.billable, EPExpenseClaimDetails.inventoryID, EPExpenseClaimDetails.customerID, EPExpenseClaimDetails.customerLocationID>))]
  [PXUIRequired(typeof (EPExpenseClaimDetails.billable))]
  [PXUIEnabled(typeof (EPExpenseClaimDetails.billable))]
  [Account]
  public virtual int? SalesAccountID { get; set; }

  /// <summary>
  /// The corresponding <see cref="T:PX.Objects.GL.Sub">subaccount</see> the system uses to record the amount to charge the customer for.
  /// If the <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.Billable">Billable</see> check box is selected, the sales subaccount specified for the expense non-stock item is filled in by default.
  /// The segments of the sales subaccount are combined according to the settings specified on the Time and Expenses Preferences (EP101000) form.
  /// </summary>
  [PXDefault]
  [PXFormula(typeof (Default<EPExpenseClaimDetails.billable, EPExpenseClaimDetails.inventoryID, EPExpenseClaimDetails.contractID, EPExpenseClaimDetails.taskID>))]
  [PXFormula(typeof (Default<EPExpenseClaimDetails.employeeID, EPExpenseClaimDetails.customerLocationID>))]
  [SubAccount(typeof (EPExpenseClaimDetails.salesAccountID))]
  [PXUIRequired(typeof (EPExpenseClaimDetails.billable))]
  [PXUIEnabled(typeof (EPExpenseClaimDetails.billable))]
  public virtual int? SalesSubID { get; set; }

  /// <summary>
  /// The type of AR document created as a result of billing a claim.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocType.ListAttribute" />.
  /// </value>
  [PXDBString(3, IsFixed = true)]
  [PX.Objects.AR.ARDocType.List]
  [PXUIField]
  public virtual string ARDocType { get; set; }

  /// <summary>
  /// The reference number of the AR document created as a result of billing a claim.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "AR Reference Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.AR.ARInvoice.refNbr, Where<PX.Objects.AR.ARInvoice.docType, Equal<Optional<EPExpenseClaimDetails.aRDocType>>>>))]
  public virtual string ARRefNbr { get; set; }

  /// <summary>
  /// The type of ARPdocument created as a result of releasing a claim.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.AP.APDocType.ListAttribute" />.
  /// </value>
  [PXDBString(3, IsFixed = true)]
  [PX.Objects.AP.APDocType.List]
  [PXUIField]
  public virtual string APDocType { get; set; }

  /// <summary>
  /// The reference number of the AR document created as a result of releasing a claim.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "AP Reference Nbr.", Enabled = false, Visible = false)]
  [PXSelector(typeof (Search<PX.Objects.AP.APInvoice.refNbr, Where<PX.Objects.AP.APInvoice.docType, Equal<Optional<EPExpenseClaimDetails.aPDocType>>>>))]
  public virtual string APRefNbr { get; set; }

  /// <summary>
  /// The number of the AP document line created as a result of releasing an expense claim.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "AP Document Line Nbr.")]
  public virtual int? APLineNbr { get; set; }

  /// <summary>The status of the expense receipt.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.EP.EPExpenseClaimDetailsStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [EPExpenseClaimDetailsStatus.List]
  public virtual string Status { get; set; }

  /// <summary>
  /// The status of the Expense Claim (EP301000) form (which corresponds to the <see cref="T:PX.Objects.EP.ExpenseClaimEntry" /> graph).
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.EP.EPExpenseClaimStatus.ListAttribute" />.
  /// </value>
  [PXString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Expense Claim Status", Enabled = false)]
  [EPExpenseClaimStatus.List]
  [PXFormula(typeof (Switch<Case<Where<EPExpenseClaimDetails.refNbr, IsNotNull>, Selector<EPExpenseClaimDetails.refNbrNotFiltered, EPExpenseClaim.status>>, Null>))]
  public virtual string StatusClaim { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the Expense Claim (EP301000) (which corresponds to the <see cref="T:PX.Objects.EP.ExpenseClaimEntry" /> graph)
  /// has the On Hold <see cref="P:PX.Objects.EP.EPExpenseClaim.Status">status</see>,
  /// which means that user can pick another claim, otherwise user cannot change claim.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<EPExpenseClaimDetails.refNbr, IsNotNull>, Selector<EPExpenseClaimDetails.refNbrNotFiltered, EPExpenseClaim.hold>>, True>))]
  [PXDefault(true)]
  [PXUIField(Visible = false)]
  public virtual bool? HoldClaim { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether an expense receipt was created from an expense claim.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Created from Claim", Visible = false)]
  public virtual bool? CreatedFromClaim { get; set; }

  /// <summary>
  /// The date when the expense receipt was included in the expense claim.
  /// </summary>
  [PXDBDateAndTime]
  public DateTime? SubmitedDate { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the expense receipt was created in a previous version of Acumatica ERP.
  /// Taxes for this receipt will be calculated on update of the Tax Zone, Tax Category, Tax Calculation Mode, or Amount fields.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public bool? LegacyReceipt { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CA.CACorpCard">corporate card</see> that is used to pay the expense receipt.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Corporate Card")]
  [PXRestrictor(typeof (Where<CACorpCard.isActive, Equal<True>>), "The corporate card is inactive.", new System.Type[] {})]
  [PXSelector(typeof (Search<CACorpCard.corpCardID>), new System.Type[] {typeof (CACorpCard.corpCardCD), typeof (CACorpCard.name), typeof (CACorpCard.cardNumber), typeof (PX.Objects.GL.Account.curyID)}, SubstituteKey = typeof (CACorpCard.corpCardCD), DescriptionField = typeof (CACorpCard.name))]
  [PXForeignReference(typeof (Field<EPExpenseClaimDetails.corpCardID>.IsRelatedTo<CACorpCard.corpCardID>))]
  public int? CorpCardID { get; set; }

  /// <summary>The way the expense receipt has been paid.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <list>
  /// <item><description><c>PersAcc</c>: Personal Account</description></item>,
  /// <item><description><c>CardComp</c>: Corporate Card, Company Expense</description></item>,
  /// <item><description><c>CardPers</c>: Corporate Card, Personal Expense</description></item>
  /// </list>
  /// </value>
  [PXUIField(DisplayName = "Paid With")]
  [PXDefault("PersAcc")]
  [PXDBString(8)]
  [LabelList(typeof (EPExpenseClaimDetails.paidWith.Labels))]
  public virtual string PaidWith { get; set; }

  public virtual bool IsPaidWithCard => this.PaidWith == "CardComp" || this.PaidWith == "CardPers";

  /// <summary>The CA bank transaction date.</summary>
  [PXDBDate]
  public DateTime? BankTranDate { get; set; }

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

  [PXSearchable(4096 /*0x1000*/, "Expense Receipt: {0} by {2}", new System.Type[] {typeof (EPExpenseClaimDetails.refNbr), typeof (EPExpenseClaimDetails.employeeID), typeof (EPEmployee.acctName)}, new System.Type[] {typeof (EPExpenseClaimDetails.tranDesc)}, NumberFields = new System.Type[] {typeof (EPExpenseClaimDetails.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (EPExpenseClaimDetails.expenseDate), typeof (EPExpenseClaimDetails.status), typeof (EPExpenseClaimDetails.refNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (EPExpenseClaimDetails.tranDesc)}, SelectForFastIndexing = typeof (Select2<EPExpenseClaimDetails, InnerJoin<EPEmployee, On<EPExpenseClaimDetails.employeeID, Equal<EPEmployee.bAccountID>>>>), SelectDocumentUser = typeof (Select2<Users, InnerJoin<EPEmployee, On<Users.pKID, Equal<EPEmployee.userID>>>, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaimDetails.employeeID>>>>))]
  [PXNote(DescriptionField = typeof (EPExpenseClaimDetails.claimDetailID), Selector = typeof (EPExpenseClaimDetails.claimDetailID), ShowInReferenceSelector = true)]
  [NotePersist(typeof (EPExpenseClaimDetails.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class docType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPExpenseClaimDetails.docType>
  {
    public docType()
      : base("ECD")
    {
    }
  }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.claimDetailCD>
  {
    public static EPExpenseClaimDetails Find(
      PXGraph graph,
      string claimDetailCD,
      PKFindOptions options = 0)
    {
      return (EPExpenseClaimDetails) PrimaryKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.claimDetailCD>.FindBy(graph, (object) claimDetailCD, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Claim</summary>
    public class Claim : 
      PrimaryKeyOf<EPExpenseClaim>.By<EPExpenseClaim.refNbr>.ForeignKeyOf<EPExpenseClaim>.By<EPExpenseClaimDetails.refNbr>
    {
    }

    /// <summary>Project/Contract</summary>
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.contractID>
    {
    }

    /// <summary>Project Task</summary>
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.contractID, EPExpenseClaimDetails.taskID>
    {
    }

    /// <summary>Cost Code</summary>
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.costCodeID>
    {
    }

    /// <summary>Item</summary>
    public class Item : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.inventoryID>
    {
    }

    /// <summary>Branch</summary>
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.branchID>
    {
    }

    /// <summary>Claimed By</summary>
    public class Employee : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.employeeID>
    {
    }

    /// <summary>Owner</summary>
    public class OwnerContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.ownerID>
    {
    }

    /// <summary>Tax Zone</summary>
    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.taxZoneID>
    {
    }

    /// <summary>Tax Category</summary>
    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.taxCategoryID>
    {
    }

    /// <summary>Tip Tax Category</summary>
    public class TipTaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.taxTipCategoryID>
    {
    }

    /// <summary>Customer</summary>
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.customerID>
    {
    }

    /// <summary>Customer Location</summary>
    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.customerID, EPExpenseClaimDetails.customerLocationID>
    {
    }

    /// <summary>Expense Account</summary>
    public class ExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.expenseAccountID>
    {
    }

    /// <summary>Expense Subaccount</summary>
    public class ExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.expenseSubID>
    {
    }

    /// <summary>Sales Account</summary>
    public class SalesAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.salesAccountID>
    {
    }

    /// <summary>Sales Subaccount</summary>
    public class SalesSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.salesSubID>
    {
    }

    /// <summary>AR Invoice</summary>
    public class Invoice : 
      PrimaryKeyOf<PX.Objects.AR.ARInvoice>.By<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.aRDocType, EPExpenseClaimDetails.aRRefNbr>
    {
    }

    /// <summary>AP Bill</summary>
    public class Bill : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.aPDocType, EPExpenseClaimDetails.aPRefNbr>
    {
    }

    /// <summary>AP Bill Line</summary>
    public class BillLine : 
      PrimaryKeyOf<APTran>.By<APTran.tranType, APTran.refNbr, APTran.lineNbr>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.aPDocType, EPExpenseClaimDetails.aPRefNbr, EPExpenseClaimDetails.aPLineNbr>
    {
    }

    /// <summary>Corporate CC</summary>
    public class CorporateCC : 
      PrimaryKeyOf<CACorpCard>.By<CACorpCard.corpCardID>.ForeignKeyOf<EPExpenseClaimDetails>.By<EPExpenseClaimDetails.corpCardID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaimDetails.selected>
  {
  }

  public abstract class claimDetailID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimDetailID>
  {
  }

  public abstract class claimDetailCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimDetailCD>
  {
    public class NumberingAttribute(System.Type setupField, System.Type dateField) : 
      AutoNumberAttribute(setupField, dateField)
    {
      public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
      {
        EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
        if (sender.GetStatus((object) row) != 2)
          return;
        base.RowPersisting(sender, e);
        string newValue = (string) sender.GetValue(e.Row, this._FieldOrdinal);
        if (string.IsNullOrEmpty(newValue))
          return;
        this.CheckDuplicateAndAssignNextNumber(sender, row, newValue);
      }

      private void CheckDuplicateAndAssignNextNumber(
        PXCache sender,
        EPExpenseClaimDetails row,
        string newValue)
      {
        if (PXResultset<EPExpenseClaimDetails>.op_Implicit(PXSelectBase<EPExpenseClaimDetails, PXViewOf<EPExpenseClaimDetails>.BasedOn<SelectFromBase<EPExpenseClaimDetails, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPExpenseClaimDetails.claimDetailCD, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.Select(sender.Graph, new object[1]
        {
          (object) newValue
        })) == null)
          return;
        EPExpenseClaimDetails expenseClaimDetails = PXResultset<EPExpenseClaimDetails>.op_Implicit(PXSelectBase<EPExpenseClaimDetails, PXViewOf<EPExpenseClaimDetails>.BasedOn<SelectFromBase<EPExpenseClaimDetails, TypeArrayOf<IFbqlJoin>.Empty>.Order<PX.Data.BQL.Fluent.By<BqlField<EPExpenseClaimDetails.claimDetailCD, IBqlString>.Desc>>>.ReadOnly.Config>.Select(sender.Graph, Array.Empty<object>()));
        EPSetup epSetup = PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXViewOf<EPSetup>.BasedOn<SelectFromBase<EPSetup, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly.Config>.Select(sender.Graph, Array.Empty<object>()));
        string nextNumber = AutoNumberAttribute.GetNextNumber(sender, (object) row, epSetup.ReceiptNumberingID, row.ExpenseDate, expenseClaimDetails.ClaimDetailCD, out string _, out string _, out int? _);
        sender.SetValue((object) row, this._FieldName, (object) nextNumber);
      }
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.branchID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaimDetails.refNbr>
  {
  }

  public abstract class refNbrNotFiltered : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.refNbrNotFiltered>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.employeeID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.ownerID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.workgroupID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaimDetails.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaimDetails.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaimDetails.rejected>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  EPExpenseClaimDetails.curyInfoID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaimDetails.curyID>
  {
  }

  public abstract class cardCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    EPExpenseClaimDetails.cardCuryInfoID>
  {
  }

  public abstract class cardCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.cardCuryID>
  {
  }

  public abstract class claimCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimCuryInfoID>
  {
  }

  public abstract class expenseDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPExpenseClaimDetails.expenseDate>
  {
  }

  public abstract class curyTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyTaxTotal>
  {
  }

  public abstract class taxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.taxTotal>
  {
  }

  public abstract class curyTaxTipTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyTaxTipTotal>
  {
  }

  public abstract class taxTipTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.taxTipTotal>
  {
  }

  public abstract class curyTaxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyTaxRoundDiff>
  {
  }

  public abstract class taxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.taxRoundDiff>
  {
  }

  public abstract class expenseRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.expenseRefNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.inventoryID>
  {
  }

  public abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.taxZoneID>
  {
  }

  public abstract class taxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.taxCalcMode>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.taxCategoryID>
  {
  }

  public abstract class hasWithHoldTax : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPExpenseClaimDetails.hasWithHoldTax>
  {
  }

  public abstract class hasUseTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaimDetails.hasUseTax>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaimDetails.uOM>
  {
  }

  public abstract class curyTipAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyTipAmt>
  {
  }

  public abstract class tipAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPExpenseClaimDetails.tipAmt>
  {
  }

  public abstract class taxTipCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.taxTipCategoryID>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPExpenseClaimDetails.qty>
  {
  }

  public abstract class curyUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyUnitCost>
  {
  }

  public abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.unitCost>
  {
  }

  public abstract class curyEmployeePart : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyEmployeePart>
  {
  }

  public abstract class employeePart : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.employeePart>
  {
  }

  public abstract class curyExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPExpenseClaimDetails.extCost>
  {
  }

  public abstract class curyTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPExpenseClaimDetails.taxAmt>
  {
  }

  public abstract class curyTaxableAmtFromTax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyTaxableAmtFromTax>
  {
  }

  public abstract class taxableAmtFromTax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.taxableAmtFromTax>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.taxableAmt>
  {
  }

  public abstract class curyTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyTranAmt>
  {
  }

  public abstract class curyTranAmtWithTaxes : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyTranAmtWithTaxes>
  {
  }

  public abstract class curyAmountWithTaxes : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyAmountWithTaxes>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPExpenseClaimDetails.tranAmt>
  {
  }

  public abstract class tranAmtWithTaxes : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.tranAmtWithTaxes>
  {
  }

  public abstract class claimCuryTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimCuryTranAmt>
  {
  }

  public abstract class curyNetAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyNetAmount>
  {
  }

  public abstract class netAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.netAmount>
  {
  }

  public abstract class claimTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimTranAmt>
  {
  }

  public abstract class claimCuryTranAmtWithTaxes : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimCuryTranAmtWithTaxes>
  {
  }

  public abstract class claimTranAmtWithTaxes : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimTranAmtWithTaxes>
  {
  }

  public abstract class claimCuryTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimCuryTaxTotal>
  {
  }

  public abstract class claimTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimTaxTotal>
  {
  }

  public abstract class claimCuryTaxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimCuryTaxRoundDiff>
  {
  }

  public abstract class claimTaxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimTaxRoundDiff>
  {
  }

  public abstract class claimCuryVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimCuryVatExemptTotal>
  {
  }

  public abstract class claimVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimVatExemptTotal>
  {
  }

  public abstract class claimCuryVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimCuryVatTaxableTotal>
  {
  }

  public abstract class claimVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.claimVatTaxableTotal>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaimDetails.vatTaxableTotal>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaimDetails.tranDesc>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPExpenseClaimDetails.customerLocationID>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.contractID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.costCodeID>
  {
  }

  public abstract class billable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaimDetails.billable>
  {
  }

  public abstract class billed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaimDetails.billed>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaimDetails.released>
  {
  }

  public abstract class expenseAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPExpenseClaimDetails.expenseAccountID>
  {
  }

  public abstract class expenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPExpenseClaimDetails.expenseSubID>
  {
  }

  public abstract class salesAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPExpenseClaimDetails.salesAccountID>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.salesSubID>
  {
  }

  public abstract class aRDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.aRDocType>
  {
  }

  public abstract class aRRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaimDetails.aRRefNbr>
  {
  }

  public abstract class aPDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.aPDocType>
  {
  }

  public abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaimDetails.aPRefNbr>
  {
  }

  public abstract class aPLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.aPLineNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaimDetails.status>
  {
  }

  public abstract class statusClaim : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.statusClaim>
  {
  }

  public abstract class holdClaim : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaimDetails.holdClaim>
  {
  }

  public abstract class createdFromClaim : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPExpenseClaimDetails.createdFromClaim>
  {
  }

  public abstract class submitedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPExpenseClaimDetails.submitedDate>
  {
  }

  public abstract class legacyReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPExpenseClaimDetails.legacyReceipt>
  {
  }

  public abstract class corpCardID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.corpCardID>
  {
  }

  public abstract class paidWith : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaimDetails.paidWith>
  {
    public const string PersonalAccount = "PersAcc";
    public const string CardCompanyExpense = "CardComp";
    public const string CardPersonalExpense = "CardPers";

    public class cash : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPExpenseClaimDetails.paidWith.cash>
    {
      public cash()
        : base("PersAcc")
      {
      }
    }

    public class cardCompanyExpense : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EPExpenseClaimDetails.paidWith.cardCompanyExpense>
    {
      public cardCompanyExpense()
        : base("CardComp")
      {
      }
    }

    public class cardPersonalExpense : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EPExpenseClaimDetails.paidWith.cardPersonalExpense>
    {
      public cardPersonalExpense()
        : base("CardPers")
      {
      }
    }

    public class Labels : ILabelProvider
    {
      private static readonly IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
      {
        {
          "PersAcc",
          "Personal Account"
        },
        {
          "CardComp",
          "Corporate Card, Company Expense"
        },
        {
          "CardPers",
          "Corporate Card, Personal Expense"
        }
      };

      public IEnumerable<ValueLabelPair> ValueLabelPairs
      {
        get => EPExpenseClaimDetails.paidWith.Labels._valueLabelPairs;
      }
    }
  }

  public abstract class bankTranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPExpenseClaimDetails.bankTranDate>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPExpenseClaimDetails.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPExpenseClaimDetails.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPExpenseClaimDetails.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaimDetails.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPExpenseClaimDetails.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPExpenseClaimDetails.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPExpenseClaimDetails.Tstamp>
  {
  }
}
