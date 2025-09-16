// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPExpenseClaim
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.TX;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Contains the main properties of the claim document, which an employee can use for the reimbursement of expenses he or she incurred on behalf of the company.
/// An expense claim is edited on the Expense Claim (EP301000) form (which corresponds to the <see cref="T:PX.Objects.EP.ExpenseClaimEntry" /> graph).
/// </summary>
[PXPrimaryGraph(typeof (ExpenseClaimEntry))]
[PXCacheName("Expense Claim")]
[Serializable]
public class EPExpenseClaim : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAssign,
  IAccountable
{
  public const 
  #nullable disable
  string DocType = "ECL";

  /// <summary>The branch of the claim.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<EPEmployee, On<PX.Objects.GL.Branch.bAccountID, Equal<EPEmployee.parentBAccountID>>>, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaim.employeeID>>>>), null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The unique reference number of the expense claim document, which the system assigns based on the numbering sequence
  /// specified for claims on the Time and Expenses Preferences (EP101000) form (which corresponds to the <see cref="T:PX.Objects.EP.EPSetupMaint" /> graph).
  /// This field is the key field.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [EPExpenceClaimSelector]
  [AutoNumber(typeof (EPSetup.claimNumberingID), typeof (EPExpenseClaim.docDate))]
  [PXFieldDescription]
  public virtual string RefNbr { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.EPEmployee">employee</see> who claims the expenses.
  /// When the claim is released, an Accounts Payable bill will be generated for this employee.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.EP.EPEmployee.bAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (Search<EPEmployee.bAccountID, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [PXSubordinateAndWingmenSelector(typeof (EPDelegationOf.expenses))]
  [PXUIField]
  [PXForeignReference(typeof (Field<EPExpenseClaim.employeeID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? EmployeeID { get; set; }

  /// <summary>
  /// The workgroup that is responsible for the document approval process.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Approval Workgroup")]
  [PXSelector(typeof (EPCompanyTreeOwner.workGroupID), SubstituteKey = typeof (EPCompanyTreeOwner.description))]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Contact">contact</see> responsible for the document approval process.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [Owner(typeof (EPExpenseClaim.workgroupID), DisplayName = "Approver", Visible = false)]
  [PXDefault(typeof (Search<EPCompanyTreeMember.contactID, Where<EPCompanyTreeMember.workGroupID, Equal<Current<EPExpenseClaim.workgroupID>>, And<EPCompanyTreeMember.isOwner, Equal<boolTrue>>>>))]
  public virtual int? ApproverID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Contact">contact</see> who actually approved the document.
  /// </summary>
  [Owner]
  public virtual int? ApprovedByID { get; set; }

  /// <summary>The department associated with the expense claim.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.EP.EPEmployee.DepartmentID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<EPEmployee.departmentID, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaim.employeeID>>>>))]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField]
  public virtual string DepartmentID { get; set; }

  /// <summary>
  /// The location ID that is set to the AP bill created as a result of claim release.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [PXDefault(typeof (Search<EPEmployee.defLocationID, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaim.employeeID>>>>))]
  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<EPExpenseClaim.employeeID>>>))]
  public virtual int? LocationID { get; set; }

  /// <summary>The date when the claim was entered.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  /// <summary>The date when the claim was approved.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Approval Date", Enabled = false)]
  public virtual DateTime? ApproveDate { get; set; }

  /// <summary>
  /// The <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">financial period</see> of the document.
  /// </summary>
  /// <value>
  /// Is determined by the <see cref="P:PX.Objects.EP.EPExpenseClaim.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.EP.EPExpenseClaim.FinPeriodID" />
  /// the value of this field can't be overridden by user.
  /// </value>
  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  /// <summary>
  /// The period to which the AP document should be posted.
  /// The selected period is copied to the Post Period box on the Bills and Adjustments form (AP301000) (which corresponds to the <see cref="T:PX.Objects.AP.APInvoiceEntry" /> graph)
  /// for the AP document created upon the release of the expense claim.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod.FinPeriodID" /> field.
  /// </value>
  [APOpenPeriod(null, typeof (EPExpenseClaim.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (EPExpenseClaim.tranPeriodID), ValidatePeriod = PeriodValidation.Nothing)]
  [PXFormula(typeof (Switch<Case<Where<EPExpenseClaim.hold, Equal<True>>, Null>, EPExpenseClaim.finPeriodID>))]
  [PXUIField(DisplayName = "Post to Period")]
  public virtual string FinPeriodID { get; set; }

  /// <summary>A description of the claim.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string DocDesc { get; set; }

  /// <summary>
  /// The code of the <see cref="T:PX.Objects.CM.Currency">currency</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isActive, Equal<True>, And<CurrencyList.isFinancial, Equal<True>>>>))]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
  /// </summary>
  /// <value>
  /// Is generated automatically, and corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo(ModuleCode = "EP")]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// The total amount of the claim in the <see cref="P:PX.Objects.EP.EPExpenseClaim.CuryID">currency of the document</see>.
  /// The amount is calculated as the sum of the amounts in the <see cref="!:EPExpenseClaim.CuryTranAmt">Claim Amount</see> column
  /// of the Expense Claim Details table located on the Expense Claim (EP301000) form for all lines specified for the claim with taxes applied.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (EPExpenseClaim.curyInfoID), typeof (EPExpenseClaim.docBal))]
  [PXUIField]
  public virtual Decimal? CuryDocBal { get; set; }

  /// <summary>
  /// The total amount of the claim in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// The amount is calculated as the sum of the amounts in the <see cref="!:EPExpenseClaim.TranAmt">Claim Amount</see> column
  /// for all lines specified for the claim, with taxes applied.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocBal { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the expense claim is in the On Hold <see cref="P:PX.Objects.EP.EPExpenseClaim.Status">status</see>,
  /// which means that the claim can be edited but cannot be release.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? Hold { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the expense claim has been approved by a responsible person
  /// and is in the Approved <see cref="P:PX.Objects.EP.EPExpenseClaim.Status">status</see> now.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the expense claim has been rejected by a responsible person.
  /// When the claim is rejected, its <see cref="P:PX.Objects.EP.EPExpenseClaim.Status">status</see> changes to Rejected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Rejected { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the expense claim was released
  /// and is in the Released <see cref="P:PX.Objects.EP.EPExpenseClaim.Status">status</see> now.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? Released { get; set; }

  /// <summary>The status of the expense claim.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.EP.EPExpenseClaimStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1)]
  [PXDefault("H")]
  [EPExpenseClaimStatus.List]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string Status { get; set; }

  /// <summary>The tax zone associated with the branch.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXDefault(typeof (Coalesce<Search<EPEmployee.receiptAndClaimTaxZoneID, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaim.employeeID>>>>, Search2<PX.Objects.CR.Location.vTaxZoneID, RightJoin<EPEmployee, On<EPEmployee.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaim.employeeID>>>>>))]
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

  /// <summary>
  /// A Boolean value that indicates whether withholding taxes are applied to the document.
  /// This is a technical field, which is calculated on the fly and is used to restrict the values of the <see cref="P:PX.Objects.EP.EPExpenseClaim.TaxCalcMode" /> field.
  /// </summary>
  [PXBool]
  [RestrictWithholdingTaxCalcMode(typeof (EPExpenseClaim.taxZoneID), typeof (EPExpenseClaim.taxCalcMode))]
  public virtual bool? HasWithHoldTax { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether use taxes are applied to the document.
  /// This is a technical field, which is calculated on the fly and is used to restrict the values of the <see cref="P:PX.Objects.EP.EPExpenseClaim.TaxCalcMode" /> field.
  /// </summary>
  [PXBool]
  [RestrictUseTaxCalcMode(typeof (EPExpenseClaim.taxZoneID), typeof (EPExpenseClaim.taxCalcMode))]
  public virtual bool? HasUseTax { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXSearchable(4096 /*0x1000*/, "Expense Claim: {0} - {2}", new System.Type[] {typeof (EPExpenseClaim.refNbr), typeof (EPExpenseClaim.employeeID), typeof (EPEmployee.acctName)}, new System.Type[] {typeof (EPExpenseClaim.docDesc)}, NumberFields = new System.Type[] {typeof (EPExpenseClaim.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (EPExpenseClaim.docDate), typeof (EPExpenseClaim.status), typeof (EPExpenseClaim.refNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (EPExpenseClaim.docDesc)}, SelectForFastIndexing = typeof (Select2<EPExpenseClaim, InnerJoin<EPEmployee, On<EPExpenseClaim.employeeID, Equal<EPEmployee.bAccountID>>>>), SelectDocumentUser = typeof (Select2<Users, InnerJoin<EPEmployee, On<Users.pKID, Equal<EPEmployee.userID>>>, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaim.employeeID>>>>))]
  [PXNote(ShowInReferenceSelector = true, DescriptionField = typeof (EPExpenseClaim.refNbr), Selector = typeof (EPExpenseClaim.refNbr))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public bool? Selected { get; set; }

  /// <summary>
  /// The total amount of taxes associated with the document in the <see cref="P:PX.Objects.EP.EPExpenseClaim.CuryID">currency of the document</see>.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.EP.EPExpenseClaim.CuryID" />)
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaim.curyInfoID), typeof (EPExpenseClaim.taxTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  /// <summary>
  /// The total amount of taxes associated with the document in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaim.curyInfoID), typeof (EPExpenseClaim.taxRoundDiff), BaseCalc = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Discrepancy", Enabled = false)]
  public Decimal? CuryTaxRoundDiff { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the base currency.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? TaxRoundDiff { get; set; }

  [Obsolete]
  [PXCurrency(typeof (EPExpenseClaim.curyInfoID), typeof (EPExpenseClaim.lineTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal { get; set; }

  [Obsolete]
  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.Customer" /> associated with the expense claim.
  /// </summary>
  [PXDefault]
  [CustomerActive(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
  public virtual int? CustomerID { get; set; }

  /// <summary>
  /// The identifier of the customer <see cref="T:PX.Objects.CR.Location">location</see> associated with the document.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.AR.Customer.defLocationID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<EPExpenseClaim.customerID>>>>))]
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current2<EPExpenseClaim.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXUIEnabled(typeof (Where<Current2<EPExpenseClaim.customerID>, IsNotNull>))]
  [PXFormula(typeof (Switch<Case<Where<Current2<EPExpenseClaim.customerID>, IsNull>, Null>, Selector<EPExpenseClaim.customerID, PX.Objects.AR.Customer.defLocationID>>))]
  public virtual int? CustomerLocationID { get; set; }

  /// <summary>
  /// The document total (in the <see cref="P:PX.Objects.EP.EPExpenseClaim.CuryID">currency of the document</see>) that is exempt from VAT.
  /// This total is calculated as the taxable amount for the tax with the Include in VAT Exempt Total check box selected on the Taxes (TX205000) form.
  /// This box is available only if the VAT Reporting feature is enabled on the Enable/Disable Features (CS100000) form (which corresponds to the <see cref="T:PX.Objects.CS.FeaturesMaint" /> graph).
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.EP.EPExpenseClaim.CuryID" />)
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaim.curyInfoID), typeof (EPExpenseClaim.vatExemptTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  /// <summary>
  /// The document total (in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>) that is exempt from VAT.
  /// This total is calculated as the taxable amount for the tax with the Include in VAT Exempt Total check box selected on the Taxes form.
  /// This box is available only if the VAT Reporting feature is enabled on the Enable/Disable Features (CS100000) form (which corresponds to the <see cref="T:PX.Objects.CS.FeaturesMaint" /> graph).
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  /// <summary>
  /// The document total (in the <see cref="P:PX.Objects.EP.EPExpenseClaim.CuryID">currency of the document</see>) that is subject to VAT.
  /// This box is available only if the VAT Reporting feature is enabled on the Enable/Disable Features (CS100000) form (which corresponds to the <see cref="T:PX.Objects.CS.FeaturesMaint" /> graph).
  /// The VAT taxable amount is displayed in this box only if the Include in VAT Taxable Total check box
  /// is selected for the applicable tax on the Taxes (TX205000) form (which corresponds to the <see cref="T:PX.Objects.TX.SalesTaxMaint" /> graph). If the check box is cleared, this box will be empty.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.EP.EPExpenseClaim.CuryID" />)
  /// </summary>
  [PXDBCurrency(typeof (EPExpenseClaim.curyInfoID), typeof (EPExpenseClaim.vatTaxableTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  /// <summary>
  /// The document total (in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>) that is subject to VAT.
  /// This box is available only if the VAT Reporting feature is enabled on the Enable/Disable Features (CS100000) form (which corresponds to the <see cref="T:PX.Objects.CS.FeaturesMaint" /> graph).
  /// The VAT taxable amount is displayed in this box only if the Include in VAT Taxable Total check box
  /// is selected for the applicable tax on the Taxes (TX205000) form (which corresponds to the <see cref="T:PX.Objects.TX.SalesTaxMaint" /> graph). If the check box is cleared, this box will be empty.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  public int? ID => new int?();

  public string EntityType => (string) null;

  /// <summary>
  /// The <see cref="T:PX.Objects.CR.Contact">Contact</see> responsible
  /// for the document approval process.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXInt]
  public int? OwnerID
  {
    get => this.ApproverID;
    set => this.ApproverID = value;
  }

  [PXString]
  [PXFormula(typeof (Selector<EPExpenseClaim.employeeID, PX.Objects.CR.BAccount.acctName>))]
  public string FormCaptionDescription { get; set; }

  public class docType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPExpenseClaim.docType>
  {
    public docType()
      : base("ECL")
    {
    }
  }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<EPExpenseClaim>.By<EPExpenseClaim.refNbr>
  {
    public static EPExpenseClaim Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (EPExpenseClaim) PrimaryKeyOf<EPExpenseClaim>.By<EPExpenseClaim.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Branch</summary>
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<EPExpenseClaim>.By<EPExpenseClaim.branchID>
    {
    }

    /// <summary>ClaimedByEmployee</summary>
    public class ClaimedByEmployee : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<EPExpenseClaim>.By<EPExpenseClaim.employeeID>
    {
    }

    /// <summary>Owner/Approver</summary>
    public class OwnerContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPExpenseClaim>.By<EPExpenseClaim.approverID>
    {
    }

    /// <summary>Tax Zone</summary>
    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<EPExpenseClaim>.By<EPExpenseClaim.taxZoneID>
    {
    }

    /// <summary>Customer</summary>
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<EPExpenseClaim>.By<EPExpenseClaim.customerID>
    {
    }

    /// <summary>Customer Location</summary>
    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<EPExpenseClaim>.By<EPExpenseClaim.customerID, EPExpenseClaim.customerLocationID>
    {
    }

    /// <summary>Department</summary>
    public class Department : 
      PrimaryKeyOf<EPDepartment>.By<EPDepartment.departmentID>.ForeignKeyOf<EPExpenseClaim>.By<EPExpenseClaim.departmentID>
    {
    }

    /// <summary>Claim Location</summary>
    public class ClaimLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<EPExpenseClaim>.By<EPExpenseClaim.employeeID, EPExpenseClaim.locationID>
    {
    }
  }

  public class Events : PXEntityEventBase<EPExpenseClaim>.Container<EPExpenseClaim.Events>
  {
    public PXEntityEvent<EPExpenseClaim> Submit;
    public PXEntityEvent<EPExpenseClaim> UpdateStatus;
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaim.branchID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaim.refNbr>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaim.employeeID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaim.workgroupID>
  {
  }

  public abstract class approverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaim.approverID>
  {
  }

  public abstract class approvedByID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaim.approvedByID>
  {
  }

  public abstract class departmentID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaim.departmentID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaim.locationID>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPExpenseClaim.docDate>
  {
  }

  public abstract class approveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPExpenseClaim.approveDate>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaim.tranPeriodID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaim.finPeriodID>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaim.docDesc>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaim.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  EPExpenseClaim.curyInfoID>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPExpenseClaim.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPExpenseClaim.docBal>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaim.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaim.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaim.rejected>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaim.released>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaim.status>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaim.taxZoneID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPExpenseClaim.taxCalcMode>
  {
  }

  public abstract class hasWithHoldTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaim.hasWithHoldTax>
  {
  }

  public abstract class hasUseTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaim.hasUseTax>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaim.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPExpenseClaim.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPExpenseClaim.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPExpenseClaim.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPExpenseClaim.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPExpenseClaim.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPExpenseClaim.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPExpenseClaim.Tstamp>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPExpenseClaim.selected>
  {
  }

  public abstract class curyTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaim.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPExpenseClaim.taxTotal>
  {
  }

  public abstract class curyTaxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaim.curyTaxRoundDiff>
  {
  }

  public abstract class taxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaim.taxRoundDiff>
  {
  }

  public abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaim.curyLineTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPExpenseClaim.lineTotal>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPExpenseClaim.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPExpenseClaim.customerLocationID>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaim.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaim.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaim.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPExpenseClaim.vatTaxableTotal>
  {
  }

  public abstract class ownerID : IBqlField, IBqlOperand
  {
  }
}
