// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEmployee
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Represents an Employee of the organization utilizing Acumatica ERP.
/// </summary>
/// <remarks>
/// An employee is a person working for the organization that utilizes Acumatica ERP.
/// The records of this type are created and edited on the <i>Employees (EP203000)</i> form,
/// which correspond to the <see cref="T:PX.Objects.EP.EmployeeMaint" /> graph.
/// EPEmployee is a child of <see cref="T:PX.Objects.CR.BAccount" /> class.
/// </remarks>
[PXTable(new System.Type[] {typeof (PX.Objects.CR.BAccount.bAccountID)})]
[PXCacheName("Employee")]
[EPEmployeePrimaryGraph]
[Serializable]
public class EPEmployee : PX.Objects.AP.Vendor
{
  protected 
  #nullable disable
  string _DepartmentID;
  protected int? _SupervisorID;
  protected int? _SalesPersonID;
  protected int? _LabourItemID;
  protected int? _SalesAcctID;
  protected int? _SalesSubID;
  protected int? _ExpenseAcctID;
  protected int? _ExpenseSubID;
  protected string _CalendarID;
  protected int? _DefaultWorkgroupID;
  protected int? _PositionLineCntr;
  protected string _HoursValidation;

  /// <summary>
  /// The employee's base <see cref="T:System.Currency" />, which is the base currency of the branch selected in the Branch box.
  /// </summary>
  /// <value>
  /// This field corresponds to <see cref="!:Organization.BaseCuryID" />.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXDefault(typeof (Switch<Case<Where<PX.Objects.CR.BAccount.isBranch, Equal<True>>, Null>, Current<AccessInfo.baseCuryID>>))]
  [PXUIField(DisplayName = "Base Currency ID", Visible = false)]
  public override string BaseCuryID { get; set; }

  /// <summary>The status of the employee.</summary>
  /// <value>
  /// The possible values of the field are listed in
  /// the <see cref="T:PX.Objects.AP.VendorStatus" /> class. The set of these values can be changed and extended by using the workflow engine.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status")]
  [PXDefault("A")]
  [VendorStatus.List]
  public override string VStatus { get; set; }

  /// <summary>
  /// The human-readable identifier of the employee that is
  /// specified by the user or defined by the EMPLOYEE auto-numbering sequence during the
  /// creation of the employee. This field is a natural key, as opposed
  /// to the surrogate key <see cref="!:BAccountID" />.
  /// </summary>
  [EmployeeRaw]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public override string AcctCD
  {
    get => this._AcctCD;
    set => this._AcctCD = value;
  }

  /// <summary>
  /// Represents the branch of your organization where the employee works.
  /// </summary>
  [PXDBInt]
  [PXDefault(typeof (Search2<PX.Objects.GL.Branch.bAccountID, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>, Where<PX.Objects.GL.Branch.active, Equal<True>, And<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>))]
  [PXUIField(DisplayName = "Branch")]
  [PXDimensionSelector("BIZACCT", typeof (Search2<PX.Objects.GL.Branch.bAccountID, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>, Where<PX.Objects.GL.Branch.active, Equal<True>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>), typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public override int? ParentBAccountID
  {
    get => this._ParentBAccountID;
    set => this._ParentBAccountID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Contact" /> object linked with the current employee as Contact Info.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  [PXUIField(DisplayName = "Default Contact")]
  [PXSelector(typeof (Search<PX.Objects.CR.Contact.contactID, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<EPEmployee.parentBAccountID>>>>))]
  public override int? DefContactID
  {
    get => this._DefContactID;
    set => this._DefContactID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Address" /> object linked with the current employee as Address Info.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Address.AddressID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
  [PXUIField(DisplayName = "Default Address")]
  [PXSelector(typeof (Search<PX.Objects.CR.Address.addressID, Where<PX.Objects.CR.Address.bAccountID, Equal<Current<EPEmployee.parentBAccountID>>>>))]
  public override int? DefAddressID
  {
    get => this._DefAddressID;
    set => this._DefAddressID = value;
  }

  /// <summary>
  /// A field inherited from <see cref="T:PX.Objects.CR.BAccount" /> represents the type of the business account.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.BAccountType" /> class.
  /// For Employee the only possible value is <see cref="F:PX.Objects.CR.BAccountType.EmployeeType" />.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("EP")]
  [PXUIField(DisplayName = "Type")]
  [BAccountType.List]
  public override string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>
  /// The employee name, which is usually a concatenation of the
  /// <see cref="P:PX.Objects.CR.Contact.FirstName">first</see> and <see cref="P:PX.Objects.CR.Contact.LastName">last name</see>
  /// of the appropriate contact.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public override string AcctName
  {
    get => base.AcctName;
    set => base.AcctName = value;
  }

  /// <summary>The external reference number of the employee.</summary>
  /// <remarks>It can be an additional number of the employee used in external integration.
  /// </remarks>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  public override string AcctReferenceNbr
  {
    get => base.AcctReferenceNbr;
    set => base.AcctReferenceNbr = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.EPEmployeeClass">employee class</see>
  /// that the employee belongs to.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.EP.EPEmployeeClass.VendorClassID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (EPEmployeeClass.vendorClassID), DescriptionField = typeof (VendorClass.descr))]
  public override string VendorClassID
  {
    get => this._VendorClassID;
    set => this._VendorClassID = value;
  }

  /// <summary>
  /// The attributes list available for the current employee.
  /// </summary>
  [CRAttributesField(typeof (EPEmployee.vendorClassID), typeof (PX.Objects.CR.BAccount.noteID))]
  public override string[] Attributes { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.EP.EPDepartment">employee department</see>
  /// that the employee belongs to.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField]
  public virtual string DepartmentID
  {
    get => this._DepartmentID;
    set => this._DepartmentID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Location" /> object linked with the employee and marked as default.
  /// The fields from the linked location are shown on the <b>Financial Settings</b> tab.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  /// <remarks>
  /// Also, the <see cref="P:PX.Objects.CR.Location.BAccountID">Location.BAccountID</see> value must be equal to
  /// the <see cref="P:PX.Objects.CR.BAccount.BAccountID">BAccount.BAccountID</see> value of the current employee.
  /// </remarks>
  [PXDefault]
  [PXDBInt]
  [PXUIField]
  [PX.Objects.CR.DefLocationID(typeof (Search<PX.Objects.CR.Location.locationID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<EPEmployee.bAccountID>>>>), SubstituteKey = typeof (PX.Objects.CR.Location.locationCD), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Location.locationID))]
  public override int? DefLocationID
  {
    get => base.DefLocationID;
    set => base.DefLocationID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.BAccount.PrimaryContactID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Primary Contact")]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  public override int? PrimaryContactID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.EPEmployee">employee</see> to whom the current employee sends reports.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXEPEPEmployeeSelector]
  [PXUIField]
  public virtual int? SupervisorID
  {
    get => this._SupervisorID;
    set => this._SupervisorID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.SalesPerson">sales person</see> to whom the current employee matches.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.AR.SalesPerson.SalesPersonID" /> field.
  /// </value>
  [PXDBInt]
  [PXDimensionSelector("SALESPER", typeof (Search5<PX.Objects.AR.SalesPerson.salesPersonID, LeftJoin<EPEmployee, On<PX.Objects.AR.SalesPerson.salesPersonID, Equal<EPEmployee.salesPersonID>>>, Where<EPEmployee.bAccountID, IsNull, Or<EPEmployee.bAccountID, Equal<Current<EPEmployee.bAccountID>>>>, Aggregate<GroupBy<PX.Objects.AR.SalesPerson.salesPersonID>>>), typeof (PX.Objects.AR.SalesPerson.salesPersonCD), new System.Type[] {typeof (PX.Objects.AR.SalesPerson.salesPersonCD), typeof (PX.Objects.AR.SalesPerson.descr)})]
  [PXUIField]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  /// <summary>
  /// The identifier of the labor item for the current employee.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  /// <remarks>
  /// The labor item is a non-stock item (of the Labor type) associated with the employee and used as a source of expense accounts for transactions associated with projects or contracts.
  /// </remarks>
  [PXDBInt]
  [PXUIField(DisplayName = "Labor Item")]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.laborItem>, And<Match<Current<AccessInfo.userName>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXForeignReference(typeof (Field<EPEmployee.labourItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? LabourItemID
  {
    get => this._LabourItemID;
    set => this._LabourItemID = value;
  }

  /// <summary>
  /// The identifier of the shift code that the system inserts by default for any new time activity or earning record entered for the employee.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.EP.EPShiftCode.shiftID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Shift Code", FieldClass = "ShiftDifferential")]
  [PXSelector(typeof (SearchFor<EPShiftCode.shiftID>.Where<BqlOperand<EPShiftCode.isManufacturingShift, IBqlBool>.IsEqual<False>>), SubstituteKey = typeof (EPShiftCode.shiftCD), DescriptionField = typeof (EPShiftCode.description))]
  [PXRestrictor(typeof (Where<BqlOperand<EPShiftCode.isActive, IBqlBool>.IsEqual<True>>), "The shift code is not active.", new System.Type[] {})]
  public virtual int? ShiftID { get; set; }

  /// <summary>
  /// The local identifier of the union associated with the employee.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.PM.PMUnion.unionID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMUnion.isActive, Equal<True>>), "The {0} union local is inactive.", new System.Type[] {typeof (PMUnion.unionID)})]
  [PXSelector(typeof (Search<PMUnion.unionID>))]
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Union Local ID", FieldClass = "Construction")]
  public virtual string UnionID { get; set; }

  /// <inheritdoc />
  [PXDBString(5, IsUnicode = true, BqlTable = typeof (PX.Objects.AP.Vendor))]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isFinancial, Equal<True>>>), DescriptionField = typeof (CurrencyList.description), CacheGlobal = true)]
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<PX.Objects.AP.Vendor.vendorClassID>>>>))]
  [PXUIField(DisplayName = "Currency")]
  public override string CuryID { get; set; }

  /// <inheritdoc />
  [PXDBString(6, IsUnicode = true, BqlTable = typeof (PX.Objects.AP.Vendor))]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID), DescriptionField = typeof (PX.Objects.CM.CurrencyRateType.descr))]
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<PX.Objects.AP.Vendor.vendorClassID>>>>))]
  [PXForeignReference(typeof (Field<EPEmployee.curyRateTypeID>.IsRelatedTo<PX.Objects.CM.CurrencyRateType.curyRateTypeID>))]
  [PXUIField(DisplayName = "Curr. Rate Type")]
  public override string CuryRateTypeID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.SM.Users">Users</see> to be used for the employee to sign into the system.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.SM.Users.PKID">Users.PKID</see> field.
  /// </value>
  [PXDBGuid(false)]
  [PXUIField]
  [PXForeignReference(typeof (EPEmployee.FK.User))]
  public virtual Guid? UserID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Account">account</see> to be used to record sales made by the employee, if applicable.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<EPEmployee.vendorClassID>>>>), SourceField = typeof (EPEmployeeClass.salesAcctID))]
  [Account]
  public virtual int? SalesAcctID
  {
    get => this._SalesAcctID;
    set => this._SalesAcctID = value;
  }

  /// <summary>
  /// The identifier of the corresponding <see cref="T:PX.Objects.GL.Sub">subaccount</see> to be used to record sales made by the employee.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID">AccountID</see> field.
  /// </value>
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<EPEmployee.vendorClassID>>>>), SourceField = typeof (EPEmployeeClass.salesSubID))]
  [SubAccount(typeof (EPEmployee.salesAcctID))]
  public virtual int? SalesSubID
  {
    get => this._SalesSubID;
    set => this._SalesSubID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Account">account</see> used to record the cash discount amounts received from the vendor due to credit terms.
  /// Inherited from <see cref="T:PX.Objects.AP.Vendor" /> class.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<EPEmployee.vendorClassID>>>>), SourceField = typeof (EPEmployeeClass.discTakenAcctID))]
  public override int? DiscTakenAcctID
  {
    get => this._DiscTakenAcctID;
    set => this._DiscTakenAcctID = value;
  }

  /// <summary>
  /// The identifier of the corresponding <see cref="T:PX.Objects.GL.Sub">subaccount</see> used to record the cash discount amounts received from the vendor due to credit terms.
  /// Inherited from <see cref="T:PX.Objects.AP.Vendor" /> class.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID">AccountID</see> field.
  /// </value>
  [SubAccount(typeof (PX.Objects.AP.Vendor.discTakenAcctID))]
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<EPEmployee.vendorClassID>>>>), SourceField = typeof (EPEmployeeClass.discTakenSubID))]
  public override int? DiscTakenSubID
  {
    get => this._DiscTakenSubID;
    set => this._DiscTakenSubID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Account">account</see> that will be used to record compensation amounts paid to the employee.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<EPEmployee.vendorClassID>>>>), SourceField = typeof (VendorClass.expenseAcctID))]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  /// <summary>
  /// The identifier of the corresponding <see cref="T:PX.Objects.GL.Sub">subaccount</see> that will be used to record compensation amounts paid to the employee.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID">AccountID</see> field.
  /// </value>
  [SubAccount(typeof (EPEmployee.expenseAcctID))]
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<EPEmployee.vendorClassID>>>>), SourceField = typeof (VendorClass.expenseSubID))]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  /// <summary>
  /// Identifier of the AP <see cref="T:PX.Objects.GL.Account">account</see> to be used to record prepayments paid to the employee.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(DisplayName = "Prepayment Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "AP")]
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<EPEmployee.vendorClassID>>>>))]
  public override int? PrepaymentAcctID
  {
    get => this._PrepaymentAcctID;
    set => this._PrepaymentAcctID = value;
  }

  /// <summary>
  /// The identifier of the corresponding <see cref="T:PX.Objects.GL.Sub">subaccount</see> to be used to record prepayments paid to the employee.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID">AccountID</see> field.
  /// </value>
  [SubAccount(typeof (EPEmployee.prepaymentAcctID), DisplayName = "Prepayment Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<EPEmployee.vendorClassID>>>>))]
  public override int? PrepaymentSubID
  {
    get => this._PrepaymentSubID;
    set => this._PrepaymentSubID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.CSCalendar">calendar</see> that records the working hours of the employee and the time zone of the employee.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.CSCalendar.CalendarID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<EPEmployee.vendorClassID>>>>), SourceField = typeof (EPEmployeeClass.calendarID))]
  [PXUIField]
  [PXSelector(typeof (Search<CSCalendar.calendarID>), DescriptionField = typeof (CSCalendar.description))]
  public virtual string CalendarID
  {
    get => this._CalendarID;
    set => this._CalendarID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.TM.EPCompanyTree">workgroup</see> that the system inserts by default
  /// for each new record entered on the <b>Details</b> tab of the <i>Employee Time Card (EP305000)</i> form for this employee.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="T:PX.TM.EPCompanyTree.workGroupID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Default Workgroup")]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<EPEmployee.defContactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
  public virtual int? DefaultWorkgroupID
  {
    get => this._DefaultWorkgroupID;
    set => this._DefaultWorkgroupID = value;
  }

  /// <exclude />
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? PositionLineCntr
  {
    get => this._PositionLineCntr;
    set => this._PositionLineCntr = value;
  }

  /// <inheritdoc />
  [PXSearchable(64 /*0x40*/, "Employee: {0} {1}", new System.Type[] {typeof (EPEmployee.acctCD), typeof (EPEmployee.acctName)}, new System.Type[] {typeof (EPEmployee.defContactID), typeof (PX.Objects.CR.Contact.eMail)}, NumberFields = new System.Type[] {typeof (EPEmployee.acctCD)}, Line1Format = "{1}{2}", Line1Fields = new System.Type[] {typeof (EPEmployee.defContactID), typeof (PX.Objects.CR.Contact.eMail), typeof (PX.Objects.CR.Contact.phone1)}, Line2Format = "{1}", Line2Fields = new System.Type[] {typeof (EPEmployee.departmentID), typeof (EPDepartment.description)}, SelectForFastIndexing = typeof (Select2<EPEmployee, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<EPEmployee.defContactID>>>>))]
  [PXUniqueNote(Selector = typeof (Search2<EPEmployee.acctCD, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<EPEmployee.bAccountID>>>, Where<PX.Objects.CR.BAccount.bAccountID, IsNull, Or<Match<PX.Objects.CR.BAccount, Current<AccessInfo.userName>>>>>), DescriptionField = typeof (EPEmployee.acctCD), ShowInReferenceSelector = true, PopupTextEnabled = true)]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>
  /// Specifies whether the emails addressed to this employee should be routed
  /// from an email account to the employee's email address if the processing of incoming mail is enabled
  /// for the email account and the <b>Route Employee Emails</b> check box is selected
  /// on the <i>Email Accounts (SM204002)</i> form. For details, see <i>Incoming Mail Processing</i>.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Route Emails")]
  public virtual bool? RouteEmails { get; set; }

  /// <summary>
  /// Specifies whether time cards are required for this employee.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Time Card Is Required")]
  [PXDefault(false)]
  public virtual bool? TimeCardRequired { get; set; }

  /// <summary>
  /// The extent of validation of regular work hours for this employee.
  /// </summary>
  /// <value>
  /// The default value is set to the <see cref="T:PX.Objects.EP.EPEmployeeClass.hoursValidation">regular hours validation</see> for the selected <see cref="P:PX.Objects.EP.EPEmployee.VendorClassID">vendor class</see>.
  /// </value>
  [PXDBString(1)]
  [PXUIField(DisplayName = "Regular Hours Validation")]
  [HoursValidationOption.List]
  [PXDefault(typeof (Select<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<EPEmployee.vendorClassID>>>>), SourceField = typeof (EPEmployeeClass.hoursValidation), Constant = "V")]
  public virtual string HoursValidation
  {
    get => this._HoursValidation;
    set => this._HoursValidation = value;
  }

  /// <summary>The identifier of the Receipt and Claim tax zone.</summary>
  [PXDBString(10, IsUnicode = true)]
  public virtual string ReceiptAndClaimTaxZoneID { get; set; }

  /// <summary>Primary Key</summary>
  public new class PK : PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>
  {
    public static EPEmployee Find(PXGraph graph, int? bAccountID, PKFindOptions options = 0)
    {
      return (EPEmployee) PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  /// <summary>Unique Key</summary>
  public new class UK : PrimaryKeyOf<EPEmployee>.By<EPEmployee.acctCD>
  {
    public static EPEmployee Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (EPEmployee) PrimaryKeyOf<EPEmployee>.By<EPEmployee.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public new static class FK
  {
    /// <summary>Customer Class</summary>
    public class Class : 
      PrimaryKeyOf<CRCustomerClass>.By<CRCustomerClass.cRCustomerClassID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.classID>
    {
    }

    /// <summary>Branch or location</summary>
    public class ParentBusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.parentBAccountID>
    {
    }

    /// <summary>Address</summary>
    public class Address : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.defAddressID>
    {
    }

    /// <summary>Contact</summary>
    public class ContactInfo : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.defContactID>
    {
    }

    /// <summary>Default Location</summary>
    public class DefaultLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.bAccountID, EPEmployee.defLocationID>
    {
    }

    /// <summary>Primary Contact</summary>
    public class PrimaryContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.primaryContactID>
    {
    }

    /// <summary>Department</summary>
    public class Department : 
      PrimaryKeyOf<EPDepartment>.By<EPDepartment.departmentID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.departmentID>
    {
    }

    /// <summary>
    /// The employee's supervisor to whom the reports are sent
    /// </summary>
    public class ReportsTo : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.supervisorID>
    {
    }

    /// <summary>Sales Person</summary>
    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.salesPersonID>
    {
    }

    /// <summary>Labor Item</summary>
    public class LabourItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.labourItemID>
    {
    }

    /// <summary>Account for the sales records</summary>
    public class SalesAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.salesAcctID>
    {
    }

    /// <summary>Subaccount for the sales records</summary>
    public class SalesSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.salesSubID>
    {
    }

    /// <summary>
    /// The account used to record the cash discount amounts received from the vendor due to credit terms.
    /// </summary>
    public class CashDiscountAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployee>.By<PX.Objects.AP.Vendor.discTakenAcctID>
    {
    }

    /// <summary>
    /// The subaccount used to record the cash discount amounts received from the vendor due to credit terms.
    /// </summary>
    public class CashDiscountSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<EPEmployee>.By<PX.Objects.AP.Vendor.discTakenSubID>
    {
    }

    /// <summary>Account to record compensations</summary>
    public class ExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.expenseAcctID>
    {
    }

    /// <summary>Subaccount to record compensations</summary>
    public class ExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.expenseSubID>
    {
    }

    /// <summary>Account to record prepayments paid to the employee</summary>
    public class PrepaymentAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.prepaymentAcctID>
    {
    }

    /// <summary>Subaccount to record prepayments paid to the employee</summary>
    public class PrepaymentSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.prepaymentSubID>
    {
    }

    /// <summary>Vendor's owner</summary>
    public class Owner : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPEmployee>.By<PX.Objects.AP.Vendor.ownerID>
    {
    }

    /// <summary>Workgroup</summary>
    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<EPEmployee>.By<PX.Objects.CR.BAccount.workgroupID>
    {
    }

    /// <summary>Login information</summary>
    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.userID>
    {
    }
  }

  public new abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.baseCuryID>
  {
  }

  public new abstract class vStatus : PX.Objects.AP.Vendor.vStatus
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.bAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.acctCD>
  {
  }

  public new abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployee.parentBAccountID>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.defContactID>
  {
  }

  public new abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.defAddressID>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.acctName>
  {
  }

  public abstract class departmentID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.departmentID>
  {
  }

  public new abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.defLocationID>
  {
  }

  public new abstract class primaryContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployee.primaryContactID>
  {
  }

  public abstract class supervisorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.supervisorID>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.salesPersonID>
  {
  }

  public abstract class labourItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.labourItemID>
  {
  }

  public abstract class shiftID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.shiftID>
  {
  }

  public abstract class unionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.unionID>
  {
  }

  public new abstract class vendorClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployee.vendorClassID>
  {
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.classID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.curyID>
  {
  }

  public new abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployee.curyRateTypeID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployee.userID>
  {
  }

  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.salesAcctID>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.salesSubID>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.expenseSubID>
  {
  }

  public new abstract class prepaymentAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployee.prepaymentAcctID>
  {
  }

  public new abstract class prepaymentSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.prepaymentSubID>
  {
  }

  public abstract class calendarID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.calendarID>
  {
  }

  public abstract class defaultWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployee.defaultWorkgroupID>
  {
  }

  public abstract class positionLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.positionLineCntr>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployee.noteID>
  {
  }

  public abstract class routeEmails : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEmployee.routeEmails>
  {
  }

  public abstract class timeCardRequired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEmployee.timeCardRequired>
  {
  }

  public abstract class hoursValidation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployee.hoursValidation>
  {
  }

  public abstract class receiptAndClaimTaxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployee.receiptAndClaimTaxZoneID>
  {
  }
}
