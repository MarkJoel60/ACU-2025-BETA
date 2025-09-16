// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCase
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
using PX.Data.Search;
using PX.Objects.AR;
using PX.Objects.CR.MassProcess;
using PX.Objects.CR.Workflows;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.TM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// Represents a case (also referred to as support cases or tickets).
/// </summary>
/// <remarks>
/// A <i>case</i> (called a <i>ticket</i> in some companies) is a special entity (such as a customer request, complaint,
/// or question) that may require discussion, investigation, resolution (perhaps making a decision or fixing a problem), and explanation.
/// The records of this type are created and edited on the <i>Cases (CR306000)</i> form,
/// which corresponds to the <see cref="T:PX.Objects.CR.CRCaseMaint" /> graph.
/// </remarks>
[PXPrimaryGraph(typeof (CRCaseMaint))]
[PXCacheName("Case")]
[CREmailContactsView(typeof (Select2<Contact, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>, Where<Contact.bAccountID, Equal<Optional<CRCase.customerID>>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>))]
[PXGroupMask(typeof (LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRCase.customerID>, And<Match<BAccount, Current<AccessInfo.userName>>>>>), WhereRestriction = typeof (Where<BAccount.bAccountID, IsNotNull, Or<CRCase.customerID, IsNull>>))]
[Serializable]
public class CRCase : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAssign,
  INotable,
  IPXSelectable
{
  protected 
  #nullable disable
  string _Description;
  private string _plainText;
  private DateTime? _assignDate;
  protected int? _timeBillable;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  /// <summary>
  /// The unique identifier assigned to the case in accordance with the numbering sequence assigned to cases on the <i>Customer Management Preferences (CR101000)</i> form.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [AutoNumber(typeof (CRSetup.caseNumberingID), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (Search2<CRCase.caseCD, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRCase.customerID>>>, Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>, OrderBy<Desc<CRCase.caseCD>>>), new System.Type[] {typeof (CRCase.caseCD), typeof (CRCase.subject), typeof (CRCase.status), typeof (CRCase.priority), typeof (CRCase.severity), typeof (CRCase.caseClassID), typeof (CRCase.isActive), typeof (BAccount.acctName)}, Filterable = true)]
  [PXFieldDescription]
  [PXReferentialIntegrityCheck]
  public virtual string CaseCD { get; set; }

  /// <summary>
  /// The date and time when the case was created. The field is filled in by the system.
  /// </summary>
  [PXDBCreatedDateTime(InputMask = "g")]
  [PXUIField(DisplayName = "Date Reported", Enabled = false)]
  public virtual DateTime? CreatedDateTime { get; set; }

  /// <summary>The identifier of the case class.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRCaseClass.CaseClassID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault(typeof (PX.Data.Search<CRSetup.defaultCaseClassID>))]
  [PXUIField(DisplayName = "Case Class")]
  [PXSelector(typeof (CRCaseClass.caseClassID), DescriptionField = typeof (CRCaseClass.description), CacheGlobal = true)]
  [PXMassUpdatableField]
  public virtual string CaseClassID { get; set; }

  /// <summary>A subject of the case.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Subject { get; set; }

  /// <summary>A detailed description of the case or relevant notes.</summary>
  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set
    {
      this._Description = value;
      this._plainText = (string) null;
    }
  }

  /// <summary>
  /// A detailed description of the case in plain text format.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRCase.Description" /> field formatted in plain text.
  /// </value>
  [PXString(IsUnicode = true)]
  [PXUIField(Visible = false)]
  [PXDependsOnFields(new System.Type[] {typeof (CRCase.description)})]
  public virtual string DescriptionAsPlainText
  {
    get => this._plainText ?? (this._plainText = SearchService.Html2PlainText(this.Description));
  }

  /// <summary>The business account associated with the case.</summary>
  [PXDefault]
  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType), typeof (BAccountType.branchType), typeof (BAccountType.vendorType)}, null, null, null)]
  [PXFormula(typeof (Switch<Case<Where<CRCase.caseClassID, IsNotNull, And<Selector<CRCase.caseClassID, CRCaseClass.requireCustomer>, Equal<True>, And<Current<CRCase.customerID>, IsNotNull, And<Selector<Current<CRCase.customerID>, BAccount.type>, NotEqual<BAccountType.customerType>, And<Selector<Current<CRCase.customerID>, BAccount.type>, NotEqual<BAccountType.combinedType>>>>>>, Null>, CRCase.customerID>))]
  [PXFormula(typeof (Switch<Case<Where<CRCase.caseClassID, IsNotNull, And<Selector<CRCase.caseClassID, CRCaseClass.requireVendor>, Equal<True>, And<Current<CRCase.customerID>, IsNotNull, And<Selector<Current<CRCase.customerID>, BAccount.type>, NotEqual<BAccountType.vendorType>, And<Selector<Current<CRCase.customerID>, BAccount.type>, NotEqual<BAccountType.combinedType>>>>>>, Null>, CRCase.customerID>))]
  [PXFormula(typeof (Switch<Case<Where<Current<CRCase.customerID>, IsNull, And<Current<CRCase.contractID>, IsNotNull>>, IsNull<Selector<CRCase.contractID, Selector<ContractBillingSchedule.accountID, BAccount.acctCD>>, Selector<CRCase.contractID, Selector<PX.Objects.CT.Contract.customerID, BAccount.acctCD>>>>, CRCase.customerID>))]
  [PXFormula(typeof (Switch<Case<Where<Current<CRCase.customerID>, IsNull, And<Current<CRCase.contactID>, IsNotNull, And<Selector<CRCase.contactID, Contact.bAccountID>, IsNotNull>>>, Selector<CRCase.contactID, Selector<Contact.bAccountID, BAccount.acctCD>>>, CRCase.customerID>))]
  public virtual int? CustomerID { get; set; }

  /// <summary>
  /// The identifier of the default <see cref="T:PX.Objects.CR.Location" /> object linked with the prospective or existing customer that is selected in the Business Account box.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  /// <remarks>
  /// Also, the <see cref="P:PX.Objects.CR.Location.BAccountID" /> value must be equal to
  /// the <see cref="P:PX.Objects.CR.CRCase.CustomerID" /> value of the current case.
  /// </remarks>
  [LocationActive(typeof (Where<Location.bAccountID, Equal<Current<CRCase.customerID>>>), DescriptionField = typeof (Location.descr))]
  [PXFormula(typeof (Switch<Case<Where<Current<CRCase.locationID>, IsNull, And<Current<CRCase.contractID>, IsNotNull>>, IsNull<Selector<CRCase.contractID, Selector<ContractBillingSchedule.locationID, Location.locationCD>>, Selector<CRCase.contractID, Selector<PX.Objects.CT.Contract.locationID, Location.locationCD>>>, Case<Where<Current<CRCase.locationID>, IsNull, And<Current<CRCase.customerID>, IsNotNull>>, Selector<CRCase.customerID, Selector<BAccount.defLocationID, Location.locationCD>>, Case<Where<Current<CRCase.customerID>, IsNull>, Null>>>, CRCase.locationID>))]
  [PXFormula(typeof (Default<CRCase.customerID>))]
  public virtual int? LocationID { get; set; }

  /// <summary>The contract associated with the case.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.CT.Contract.contractID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Contract")]
  [PXSelector(typeof (Search2<PX.Objects.CT.Contract.contractID, LeftJoin<ContractBillingSchedule, On<PX.Objects.CT.Contract.contractID, Equal<ContractBillingSchedule.contractID>>>, Where<PX.Objects.CT.Contract.baseType, Equal<CTPRType.contract>, And<Where<Current<CRCase.customerID>, IsNull, Or2<Where<PX.Objects.CT.Contract.customerID, Equal<Current<CRCase.customerID>>, And<Current<CRCase.locationID>, IsNull>>, Or2<Where<ContractBillingSchedule.accountID, Equal<Current<CRCase.customerID>>, And<Current<CRCase.locationID>, IsNull>>, Or2<Where<PX.Objects.CT.Contract.customerID, Equal<Current<CRCase.customerID>>, And<PX.Objects.CT.Contract.locationID, Equal<Current<CRCase.locationID>>>>, Or<Where<ContractBillingSchedule.accountID, Equal<Current<CRCase.customerID>>, And<ContractBillingSchedule.locationID, Equal<Current<CRCase.locationID>>>>>>>>>>>, OrderBy<Desc<PX.Objects.CT.Contract.contractCD>>>), DescriptionField = typeof (PX.Objects.CT.Contract.description), SubstituteKey = typeof (PX.Objects.CT.Contract.contractCD), Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.status, Equal<PX.Objects.CT.Contract.status.active>>), "Contract is not active.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, LessEqual<PX.Objects.CT.Contract.graceDate>, Or<PX.Objects.CT.Contract.expireDate, IsNull>>), "Contract has expired.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, GreaterEqual<PX.Objects.CT.Contract.startDate>>), "Contract activation date is in future. This contract can only be used starting from {0}", new System.Type[] {typeof (PX.Objects.CT.Contract.startDate)})]
  [PXFormula(typeof (Default<CRCase.customerID>))]
  [PXDefault]
  public virtual int? ContractID { get; set; }

  /// <summary>
  /// The customer representative to be contacted about the case.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [ContactRaw(typeof (CRCase.customerID), new System.Type[] {typeof (ContactTypesAttribute.person), typeof (ContactTypesAttribute.employee)}, null, null, null, null, WithContactDefaultingByBAccount = true)]
  [PXDefault]
  public virtual int? ContactID { get; set; }

  /// <summary>The current status of the case.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.Workflows.CaseWorkflow.States.ListAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.Workflows.CaseWorkflow.States.New" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [CaseWorkflow.States.List]
  [PXUIField(DisplayName = "Status")]
  [PXChildUpdatable(UpdateRequest = true)]
  public virtual string Status { get; set; }

  /// <summary>
  /// This field indicates whether the case is active. A case is considered active if further communication or action is expected for it.
  /// If the check box is selected, the case is displayed in the list of cases on the related mass processing forms, such as the <i>Assign Cases (CR503210)</i> form.
  /// By default, the check box is cleared when the case is closed.
  /// </summary>
  /// <value>
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// This field indicates whether the case has been released.
  /// </summary>
  /// <value>
  /// Thus, the field indicates whether the <see cref="P:PX.Objects.CR.CRCase.Status" /> field has the <see cref="F:PX.Objects.CR.Workflows.CaseWorkflow.States.Released" /> value.
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// The reason why the case has been changed to the current status.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.Workflows.CaseWorkflow" /> class.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Reason")]
  [PXChildUpdatable]
  [PXMassUpdatableField]
  [PXStringList(new string[] {}, new string[] {})]
  public virtual string Resolution { get; set; }

  /// <summary>The company tree workgroup to work on the case.</summary>
  [PXDBInt]
  [PXChildUpdatable(UpdateRequest = true)]
  [PXUIField(DisplayName = "Workgroup")]
  [PXCompanyTreeSelector]
  [PXMassUpdatableField]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>The user name of the employee assigned to the case.</summary>
  [Owner(typeof (CRCase.workgroupID))]
  [PXChildUpdatable(AutoRefresh = true, TextField = "AcctName", ShowHint = false)]
  [PXMassUpdatableField]
  public virtual int? OwnerID { get; set; }

  /// <summary>
  /// The date and time when the case was assigned by <see cref="P:PX.Objects.CR.CRCase.OwnerID" />.
  /// </summary>
  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Assignment Date")]
  public virtual DateTime? AssignDate
  {
    get => this._assignDate ?? this.CreatedDateTime;
    set => this._assignDate = value;
  }

  /// <summary>The source of the case.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.CaseSourcesAttribute" /> class.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Source")]
  [CaseSources]
  public virtual string Source { get; set; }

  /// <summary>The severity level of the case.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.CRCaseSeverityAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.CRCaseSeverityAttribute._MEDIUM" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Severity")]
  [CRCaseSeverity]
  public virtual string Severity { get; set; }

  /// <summary>The priority of the case.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.CRCasePriorityAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.CRCasePriorityAttribute._MEDIUM" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Priority")]
  [CRCasePriority]
  public virtual string Priority { get; set; }

  /// <summary>Date and time of the case creation.</summary>
  [PXDBDateAndTime(InputMask = "g", PreserveTime = true)]
  [PXUIField(DisplayName = "Reported On", Required = false)]
  public DateTime? ReportedOnDateTime { get; set; }

  /// <summary>
  /// The activity identifier which contains the solution for the case.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.CRActivity.NoteID" /> field.
  /// </value>
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Solution Provided In", FieldClass = "CaseCommitmentsTracking")]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<CRActivity, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPActivityType>.On<CRActivity.FK.ActivityType>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRActivity.refNoteID, Equal<BqlField<CRCase.noteID, IBqlGuid>.FromCurrent>>>>>.And<BqlOperand<CRActivity.providesCaseSolution, IBqlBool>.IsEqual<True>>>.Order<By<BqlField<CRActivity.createdDateTime, IBqlDateTime>.Desc>>, CRActivity>.SearchFor<CRActivity.noteID>), new System.Type[] {typeof (EPActivityType.description), typeof (CRActivity.startDate), typeof (CRActivity.subject), typeof (CRActivity.uistatus), typeof (CRActivity.ownerID), typeof (CRActivity.completedDate)})]
  public Guid? SolutionActivityNoteID { get; set; }

  /// <summary>
  /// The date and time when the solution was provided for the case.
  /// </summary>
  [PXDBDate(PreserveTime = true, InputMask = "g")]
  [PXFormula(typeof (CRCase.solutionProvidedDateTime.SolutionProvidedDateTimeFormula))]
  [PXUIField(DisplayName = "Solution Provided On", Enabled = false, FieldClass = "CaseCommitmentsTracking")]
  public DateTime? SolutionProvidedDateTime { get; set; }

  /// <summary>
  /// The date and time when the case should be closed according to the SLA.
  /// </summary>
  [Obsolete]
  [PXDate(DisplayMask = "g")]
  [PXUIField(DisplayName = "SLA")]
  public virtual DateTime? SLAETA { get; set; }

  /// <summary>
  /// The estimation of the time (in minutes) required for the case resolution.
  /// </summary>
  [Obsolete]
  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Estimation")]
  public virtual int? TimeEstimated { get; set; }

  /// <summary>
  /// The date given to the contact as the date of resolution.
  /// </summary>
  /// <value>
  /// This field is calculated as <see cref="P:PX.Objects.CR.CRCase.CreatedDateTime" /> increases by <see cref="P:PX.Objects.CR.CRCase.TimeEstimated" /> minutes.
  /// </value>
  [Obsolete]
  [PXDate(DisplayMask = "g")]
  [PXUIField(DisplayName = "Promised")]
  public virtual DateTime? ETA
  {
    get
    {
      return !this.ReportedOnDateTime.HasValue || !this.TimeEstimated.HasValue ? new DateTime?() : new DateTime?(this.ReportedOnDateTime.Value.AddMinutes((double) this.TimeEstimated.Value));
    }
  }

  /// <summary>
  /// The amount of time required to close the case (in minutes).
  /// </summary>
  [Obsolete]
  [PXTimeSpanLong]
  [PXUIField(DisplayName = "Remaining")]
  public virtual int? RemaininingDate { get; set; }

  /// <summary>
  /// On closing case user can record information about closing case or can update data if Closure Notes had any data before on case process steps.
  /// </summary>
  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Closure Notes")]
  public virtual string ClosureNotes { get; set; }

  /// <exclude />
  [Obsolete]
  [PXInt]
  [PXUIField(DisplayName = "Remaining (minutes)", Enabled = false, Visible = false)]
  public virtual int? RemaininingDateMinutes => this.RemaininingDate;

  /// <summary>The date and time of the last activity of this case.</summary>
  [PXDate(InputMask = "g", DisplayMask = "g")]
  [PXUIField(DisplayName = "Last Activity", Enabled = false)]
  public virtual DateTime? LastActivity { get; set; }

  /// <summary>
  /// The date and time of the last modification of this case.
  /// </summary>
  [PXDate(InputMask = "g")]
  [PXFormula(typeof (Switch<Case<Where<CRCase.lastActivity, IsNotNull, And<CRCase.lastModifiedDateTime, IsNull>>, CRCase.lastActivity, Case<Where<CRCase.lastModifiedDateTime, IsNotNull, And<CRCase.lastActivity, IsNull>>, CRCase.lastModifiedDateTime, Case<Where<CRCase.lastActivity, Greater<CRCase.lastModifiedDateTime>>, CRCase.lastActivity>>>, CRCase.lastModifiedDateTime>))]
  [PXUIField(DisplayName = "Last Modified", Enabled = false)]
  public virtual DateTime? LastModified { get; set; }

  /// <summary>
  /// The time from the creation of the case to the initial response.
  /// </summary>
  [Obsolete]
  [CRTimeSpanCalced(typeof (Minus1<PX.Data.Search<CRActivity.startDate, Where<CRActivity.refNoteID, Equal<CRCase.noteID>, And2<Where<CRActivity.isPrivate, IsNull, Or<CRActivity.isPrivate, Equal<False>>>, And<CRActivity.ownerID, IsNotNull, And2<Where<CRActivity.incoming, IsNull, Or<CRActivity.incoming, Equal<False>>>, And<Where<CRActivity.isExternal, IsNull, Or<CRActivity.isExternal, Equal<False>>>>>>>>, OrderBy<Asc<CRActivity.startDate>>>, CRCase.reportedOnDateTime>))]
  [PXFormula(typeof (IsNull<Minus1<PX.Data.Search<CRActivity.startDate, Where<CRActivity.refNoteID, Equal<CRCase.noteID>, And2<Where<CRActivity.isPrivate, IsNull, Or<CRActivity.isPrivate, Equal<False>>>, And<CRActivity.ownerID, IsNotNull, And2<Where<CRActivity.incoming, IsNull, Or<CRActivity.incoming, Equal<False>>>, And<Where<CRActivity.isExternal, IsNull, Or<CRActivity.isExternal, Equal<False>>>>>>>>, OrderBy<Asc<CRActivity.startDate>>>, CRCase.reportedOnDateTime>, int0>))]
  [PXUIField(DisplayName = "Init. Response", Enabled = false, Visible = false)]
  [PXTimeSpanLong]
  public virtual int? InitResponse { get; set; }

  /// <exclude />
  [Obsolete]
  [PXInt]
  [PXUIField(DisplayName = "Init. Response (minutes)", Enabled = false, Visible = false)]
  public virtual int? InitResponseMinutes => this.InitResponse;

  /// <summary>The time (in hours) spent on the case activity.</summary>
  [CaseTimeSpent]
  [PXUIField(DisplayName = "Time Spent", Enabled = false)]
  public virtual int? TimeSpent { get; set; }

  /// <summary>The overtime (in hours) spent on the case activity.</summary>
  [CaseTimeSpent]
  [PXUIField(DisplayName = "Overtime Spent", Enabled = false)]
  public virtual int? OvertimeSpent { get; set; }

  /// <summary>The field indicates whether the case is billable.</summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Billable", FieldClass = "BILLABLE")]
  [PXFormula(typeof (Selector<CRCase.caseClassID, CRCaseClass.isBillable>))]
  public virtual bool? IsBillable { get; set; }

  /// <summary>
  /// The field indicates whether the billable time and billable overtime can be changed manually.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Manual Override", FieldClass = "BILLABLE")]
  [PXFormula(typeof (Switch<Case<Where<Selector<CRCase.caseClassID, CRCaseClass.perItemBilling>, Equal<BillingTypeListAttribute.perActivity>>, False>, CRCase.manualBillableTimes>))]
  public virtual bool? ManualBillableTimes { get; set; }

  /// <summary>The billable time (in hours) spent on the case.</summary>
  [CaseTimeSpent]
  [PXUIField(DisplayName = "Billable Time", Enabled = false, FieldClass = "BILLABLE")]
  public virtual int? TimeBillable
  {
    get => this._timeBillable;
    set => this._timeBillable = value;
  }

  /// <summary>The billable overtime (in hours) spent on the case.</summary>
  [CaseTimeSpent]
  [PXUIField(DisplayName = "Billable Overtime", FieldClass = "BILLABLE")]
  public virtual int? OvertimeBillable { get; set; }

  /// <summary>
  /// The date and time when the case was closed. The field is filled in by the system.
  /// </summary>
  [PXDBDateAndTime(PreserveTime = true, DisplayMask = "g")]
  [PXUIField(DisplayName = "Closed On", Enabled = false)]
  public virtual DateTime? ResolutionDate { get; set; }

  /// <summary>The time (in minutes) of the case resolution.</summary>
  /// <value>
  /// If the case is open, the field contains the time elapsed since the case was created. If the case is closed, the field contains the time from the creation of the case to its resolution.
  /// </value>
  [PXTimeSpanLong]
  [PXDBCalced(typeof (DateDiff<CRCase.reportedOnDateTime, CRCase.solutionProvidedDateTime, DateDiff.minute>), typeof (int?))]
  [PXUIField(DisplayName = "Resolution Time", Enabled = false, Visible = false)]
  public virtual int? TimeResolution { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.CRCase.TimeResolution" />
  [PXInt]
  [PXUIField(DisplayName = "Resolution Time (Minutes)", Enabled = false, Visible = false)]
  public virtual int? TimeResolutionMinutes => this.TimeResolution;

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.RefNbr" />
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "AR Reference Nbr.")]
  [PXSelector(typeof (PX.Data.Search<ARInvoice.refNbr>))]
  public virtual string ARRefNbr { get; set; }

  /// <summary>
  /// The attributes list available for the current case.
  /// The field is preserved for internal use.
  /// </summary>
  [CRAttributesField(typeof (CRCase.caseClassID))]
  public virtual string[] Attributes { get; set; }

  /// <exclude />
  public string ClassID => this.CaseClassID;

  /// <exclude />
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Billing Date")]
  public virtual DateTime? Date { get; set; }

  /// <summary>The time elapsed from the creation of the case.</summary>
  [PXTimeSpanLong(InputMask = "#### ds ## hrs ## mins")]
  [PXFormula(typeof (DateDiff<CRCase.reportedOnDateTime, PXDateAndTimeAttribute.now, DateDiff.minute>))]
  [PXUIField(DisplayName = "Age")]
  public virtual int? Age { get; set; }

  /// <summary>
  /// This field contains the age (in years) of the last activity of the case.
  /// </summary>
  [Obsolete]
  [PXTimeSpanLong(InputMask = "#### ds ## hrs ## mins")]
  [PXFormula(typeof (DateDiff<CRCase.lastActivity, PXDateAndTimeAttribute.now, DateDiff.minute>))]
  [PXUIField(DisplayName = "Last Activity Age")]
  public virtual int? LastActivityAge { get; set; }

  /// <summary>
  /// This field contains the date when the current <see cref="P:PX.Objects.CR.CRCase.Status">case status</see> is set.
  /// </summary>
  [PXDBLastChangeDateTime(typeof (CRCase.status))]
  public virtual DateTime? StatusDate { get; set; }

  [PXDBRevision(typeof (CRCase.status))]
  public virtual int? StatusRevision { get; set; }

  [PXSearchable(1024 /*0x0400*/, "Case: {0} - {2}", new System.Type[] {typeof (CRCase.caseCD), typeof (CRCase.customerID), typeof (BAccount.acctName)}, new System.Type[] {typeof (CRCase.contactID), typeof (Contact.firstName), typeof (Contact.lastName), typeof (Contact.eMail), typeof (CRCase.ownerID), typeof (PX.Objects.EP.EPEmployee.acctCD), typeof (PX.Objects.EP.EPEmployee.acctName), typeof (CRCase.subject)}, NumberFields = new System.Type[] {typeof (CRCase.caseCD)}, MatchWithJoin = typeof (LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRCase.customerID>>>), Line1Format = "{1}{3}{4}", Line1Fields = new System.Type[] {typeof (CRCase.caseClassID), typeof (CRCaseClass.description), typeof (CRCase.contactID), typeof (Contact.fullName), typeof (CRCase.status)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (CRCase.subject)})]
  [PXNote(DescriptionField = typeof (CRCase.caseCD), Selector = typeof (CRCase.caseCD), ShowInReferenceSelector = true)]
  public virtual Guid? NoteID { get; set; }

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

  [PXDBLastModifiedDateTime(InputMask = "g")]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<CRCase>.By<CRCase.caseCD>
  {
    public static CRCase Find(PXGraph graph, string caseCD, PKFindOptions options = 0)
    {
      return (CRCase) PrimaryKeyOf<CRCase>.By<CRCase.caseCD>.FindBy(graph, (object) caseCD, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Case class</summary>
    public class Class : 
      PrimaryKeyOf<CRCaseClass>.By<CRCaseClass.caseClassID>.ForeignKeyOf<CRCase>.By<CRCase.caseClassID>
    {
    }

    /// <summary>Contact</summary>
    public class Contact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRCase>.By<CRCase.contactID>
    {
    }

    /// <summary>Business Account</summary>
    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CRCase>.By<CRCase.customerID>
    {
    }

    /// <summary>Location</summary>
    public class Location : 
      PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationID>.ForeignKeyOf<CRCase>.By<CRCase.customerID, CRCase.locationID>
    {
    }

    /// <summary>Contract</summary>
    public class Contract : 
      PrimaryKeyOf<PX.Objects.CT.Contract>.By<PX.Objects.CT.Contract.contractID>.ForeignKeyOf<CRCase>.By<CRCase.contractID>
    {
    }

    /// <summary>Owner</summary>
    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRCase>.By<CRCase.ownerID>
    {
    }

    /// <summary>Workgroup</summary>
    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<CRCase>.By<CRCase.workgroupID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCase.selected>
  {
  }

  public abstract class caseCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCase.caseCD>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCase.createdDateTime>
  {
  }

  public abstract class caseClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCase.caseClassID>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCase.subject>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCase.description>
  {
  }

  public abstract class descriptionAsPlainText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCase.descriptionAsPlainText>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.customerID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.locationID>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.contractID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.contactID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCase.status>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCase.isActive>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCase.released>
  {
  }

  public abstract class resolution : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCase.resolution>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.ownerID>
  {
  }

  public abstract class assignDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRCase.assignDate>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCase.source>
  {
  }

  public abstract class severity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCase.severity>
  {
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCase.priority>
  {
  }

  public abstract class reportedOnDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCase.reportedOnDateTime>
  {
  }

  public abstract class solutionActivityNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRCase.solutionActivityNoteID>
  {
  }

  public abstract class solutionProvidedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCase.solutionProvidedDateTime>
  {
    public class SolutionProvidedDateTimeFormula : 
      BqlFormulaEvaluator<CRCase.caseClassID, CRCase.severity, CRCase.solutionActivityNoteID, CRCase.isActive>
    {
      public virtual object Evaluate(
        PXCache cache,
        object item,
        Dictionary<System.Type, object> parameters)
      {
        PXGraph graph = cache.Graph;
        if (parameters[typeof (CRCase.caseClassID)] is string parameter3)
        {
          CRCaseClass crCaseClass = CRCaseClass.PK.Find(graph, parameter3);
          if (crCaseClass != null)
          {
            if (parameters[typeof (CRCase.severity)] is string parameter)
            {
              CRClassSeverityTime classSeverityTime = CRClassSeverityTime.PK.Find(graph, parameter3, parameter);
              if (classSeverityTime != null)
              {
                bool? trackResolutionTime = classSeverityTime.TrackResolutionTime;
                if (trackResolutionTime.HasValue && trackResolutionTime.GetValueOrDefault())
                {
                  if (crCaseClass.StopTimeCounterType.GetValueOrDefault() == 1)
                    return (object) (!(parameters[typeof (CRCase.solutionActivityNoteID)] is Guid parameter1) ? new DateTime?() : CRActivity.PK.Find(graph, new Guid?(parameter1))?.CompletedDate);
                  return !(parameters[typeof (CRCase.isActive)] is bool parameter2) || !parameter2 ? (object) PXTimeZoneInfo.Now : (object) null;
                }
              }
            }
            return (object) null;
          }
        }
        return (object) null;
      }
    }
  }

  [Obsolete]
  public abstract class sLAETA : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRCase.sLAETA>
  {
  }

  [Obsolete]
  public abstract class timeEstimated : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.timeEstimated>
  {
  }

  [Obsolete]
  public abstract class eTA : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRCase.eTA>
  {
  }

  [Obsolete]
  public abstract class remaininingDate : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.remaininingDate>
  {
  }

  public abstract class closureNotes : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCase.closureNotes>
  {
  }

  [Obsolete]
  public abstract class remaininingDateMinutes : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCase.remaininingDateMinutes>
  {
  }

  public abstract class lastActivity : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRCase.lastActivity>
  {
  }

  public abstract class lastModified : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRCase.lastModified>
  {
  }

  [Obsolete]
  public abstract class initResponse : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.initResponse>
  {
  }

  [Obsolete]
  public abstract class initResponseMinutes : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.initResponseMinutes>
  {
  }

  public abstract class timeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.timeSpent>
  {
  }

  public abstract class overtimeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.overtimeSpent>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCase.isBillable>
  {
  }

  public abstract class manualBillableTimes : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRCase.manualBillableTimes>
  {
  }

  public abstract class timeBillable : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.timeBillable>
  {
  }

  public abstract class overtimeBillable : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.overtimeBillable>
  {
  }

  public abstract class resolutionDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRCase.resolutionDate>
  {
  }

  public abstract class timeResolution : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.timeResolution>
  {
  }

  public abstract class timeResolutionMinutes : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCase.timeResolutionMinutes>
  {
  }

  public abstract class aRRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCase.aRRefNbr>
  {
  }

  public abstract class attributes : BqlType<IBqlAttributes, string[]>.Field<CRCase.attributes>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRCase.date>
  {
  }

  public abstract class age : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.age>
  {
  }

  [Obsolete]
  public abstract class lastActivityAge : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.lastActivityAge>
  {
  }

  public abstract class statusDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRCase.statusDate>
  {
  }

  public abstract class statusRevision : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCase.statusRevision>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCase.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRCase.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCase.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCase.createdByScreenID>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCase.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCase.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCase.lastModifiedDateTime>
  {
  }
}
