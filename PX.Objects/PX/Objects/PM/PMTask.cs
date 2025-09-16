// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTask
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.GL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>The smallest identifiable and essential piece of a job that serves as a unit of work, and as a method of differentiating between the various components of
/// a project. A task is always defined within the scope of a project. The task budget, profitability and balances are monitored in scope of account groups.</summary>
[PXCacheName("Project Task")]
[PXPrimaryGraph(new System.Type[] {typeof (TemplateGlobalTaskMaint), typeof (TemplateTaskMaint), typeof (ProjectTaskEntry)}, new System.Type[] {typeof (Select2<PMTask, InnerJoin<PMProject, On<PMTask.projectID, Equal<PMProject.contractID>>>, Where<PMProject.nonProject, Equal<True>, And<PMProject.baseType, Equal<CTPRType.projectTemplate>, And<PMTask.taskID, Equal<Current<PMTask.taskID>>>>>>), typeof (Select2<PMTask, InnerJoin<PMProject, On<PMTask.projectID, Equal<PMProject.contractID>>>, Where<PMProject.nonProject, Equal<False>, And<PMProject.baseType, Equal<CTPRType.projectTemplate>, And<PMTask.taskID, Equal<Current<PMTask.taskID>>>>>>), typeof (Select2<PMTask, InnerJoin<PMProject, On<PMTask.projectID, Equal<PMProject.contractID>>>, Where<PMProject.nonProject, Equal<False>, And<PMProject.baseType, Equal<CTPRType.project>, And<PMTask.taskID, Equal<Current<PMTask.taskID>>>>>>)})]
[PXGroupMask(typeof (LeftJoin<PMProject, On<PMProject.contractID, Equal<PMTask.projectID>>>), WhereRestriction = typeof (Where<PMProject.contractID, IsNull, Or<Match<PMProject, Current<AccessInfo.userName>>>>))]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMTask : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  INotable,
  IProjectTaskAccountsSource,
  IProjectAccountsSource
{
  protected bool? _Selected = new bool?(false);
  /// <summary>Gets or sets the parent Project.</summary>
  protected int? _ProjectID;
  protected int? _TaskID;
  protected 
  #nullable disable
  string _TaskCD;
  protected string _Description;
  protected int? _CustomerID;
  protected int? _LocationID;
  protected string _RateTableID;
  protected string _BillingID;
  protected string _AllocationID;
  protected string _BillingOption;
  protected string _CompletedPctMethod;
  protected string _Status;
  protected DateTime? _PlannedStartDate;
  protected DateTime? _PlannedEndDate;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected string _ExtRefNbr;
  protected int? _ApproverID;
  protected string _TaxCategoryID;
  protected int? _DefaultAccrualAccountID;
  protected int? _DefaultAccrualSubID;
  protected int? _DefaultBranchID;
  protected int? _WipAccountGroupID;
  protected bool? _IsActive;
  protected bool? _IsCompleted;
  protected bool? _IsCancelled;
  protected bool? _BillSeparately;
  protected bool? _VisibleInGL;
  protected bool? _VisibleInAP;
  protected bool? _VisibleInAR;
  protected bool? _VisibleInSO;
  protected bool? _VisibleInPO;
  /// <summary>
  /// Gets or sets whether the Task is visible in the EP Time Module.
  /// If Project Task is set as invisible - it will not show up in the field selectors in the given module.
  /// </summary>
  protected bool? _VisibleInTA;
  /// <summary>
  /// Gets or sets whether the Task is visible in the EP Expense Module.
  /// If Project Task is set as invisible - it will not show up in the field selectors in the given module.
  /// </summary>
  protected bool? _VisibleInEA;
  protected bool? _VisibleInIN;
  protected bool? _VisibleInCA;
  protected bool? _VisibleInCR;
  protected bool? _AutoIncludeInPrj;
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

  [Project(DisplayName = "Project ID", IsKey = true, DirtyRead = true)]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>The unique identifier of the task.</summary>
  [PXDBIdentity]
  [PXReferentialIntegrityCheck]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  /// <summary>The unique identifier of the task. This is a segmented key, which format is configured on the Segmented Keys (CS202000) form.</summary>
  [PXDimension("PROTASK")]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string TaskCD
  {
    get => this._TaskCD;
    set => this._TaskCD = value;
  }

  /// <summary>The description of the task.</summary>
  [PXDBLocalizableString(250, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>The customer for the task. The customer is always set at the project level.</summary>
  /// <value>The value is copied from the project.</value>
  [PXDefault(typeof (Search<PMProject.customerID, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [Customer]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>The customer location.</summary>
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMTask.customerID>>>))]
  [PXDefault(typeof (Search<PMProject.locationID, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  /// <summary>The <see cref="T:PX.Objects.PM.PMRateTable">rate table</see>.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXDefault(typeof (Search<PMProject.rateTableID, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>, And<PMProject.nonProject, Equal<False>>>>))]
  [PXUIField(DisplayName = "Rate Table Code")]
  [PXSelector(typeof (PMRateTable.rateTableID), DescriptionField = typeof (PMRateTable.description))]
  [PXForeignReference(typeof (Field<PMTask.rateTableID>.IsRelatedTo<PMRateTable.rateTableID>))]
  public virtual string RateTableID
  {
    get => this._RateTableID;
    set => this._RateTableID = value;
  }

  /// <summary>The <see cref="T:PX.Objects.PM.PMBilling">billing rules</see>.</summary>
  [PXForeignReference(typeof (Field<PMTask.billingID>.IsRelatedTo<PMBilling.billingID>))]
  [PXDefault(typeof (PMProject.billingID))]
  [PXSelector(typeof (Search<PMBilling.billingID, Where<PMBilling.isActive, Equal<True>>>), DescriptionField = typeof (PMBilling.description))]
  [PXUIField(DisplayName = "Billing Rule")]
  [PXDBString(15, IsUnicode = true)]
  public virtual string BillingID
  {
    get => this._BillingID;
    set => this._BillingID = value;
  }

  /// <summary>The <see cref="T:PX.Objects.PM.PMAllocation">allocation rules</see>.</summary>
  [PXForeignReference(typeof (Field<PMTask.allocationID>.IsRelatedTo<PMAllocation.allocationID>))]
  [PXDefault(typeof (PMProject.allocationID))]
  [PXSelector(typeof (Search<PMAllocation.allocationID, Where<PMAllocation.isActive, Equal<True>>>), DescriptionField = typeof (PMAllocation.description))]
  [PXUIField(DisplayName = "Allocation Rule")]
  [PXDBString(15, IsUnicode = true)]
  public virtual string AllocationID
  {
    get => this._AllocationID;
    set => this._AllocationID = value;
  }

  /// <summary>The <see cref="T:PX.Objects.PM.PMBillingOption">way</see> the project is billed.</summary>
  [PXDBString(1, IsFixed = true)]
  [PMBillingOption.List]
  [PXDefault("B")]
  [PXUIField]
  public virtual string BillingOption
  {
    get => this._BillingOption;
    set => this._BillingOption = value;
  }

  /// <summary>The calculation method of the completion.</summary>
  [PXDBString(1, IsFixed = true)]
  [PMCompletedPctMethod.List]
  [PXDefault("M")]
  [PXUIField]
  public virtual string CompletedPctMethod
  {
    get => this._CompletedPctMethod;
    set => this._CompletedPctMethod = value;
  }

  /// <summary>The task <see cref="T:PX.Objects.PM.ProjectTaskStatus">status</see>.</summary>
  [PXDBString(1, IsFixed = true)]
  [ProjectTaskStatus.List]
  [PXDefault("D")]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>The date when the task is supposed to be started.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Planned Start Date")]
  public virtual DateTime? PlannedStartDate
  {
    get => this._PlannedStartDate;
    set => this._PlannedStartDate = value;
  }

  /// <summary>The date when the task is supposed to be finished.</summary>
  [PXDBDate]
  [PXVerifyEndDate(typeof (PMTask.plannedStartDate), AutoChangeWarning = true)]
  [PXUIField(DisplayName = "Planned End Date")]
  public virtual DateTime? PlannedEndDate
  {
    get => this._PlannedEndDate;
    set => this._PlannedEndDate = value;
  }

  /// <summary>The actual date when the task is started.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  /// <summary>The actual date when the task is finished.</summary>
  [PXDBDate]
  [PXVerifyEndDate(typeof (PMTask.startDate), AutoChangeWarning = true)]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "External Ref. Nbr")]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  /// <summary>The <see cref="T:PX.Objects.EP.EPEmployee" /> that approves or rejects the activities created under the given task.</summary>
  /// <value>If the value is null, the approval is not required. Otherwise, either <see cref="P:PX.Objects.PM.PMTask.ApproverID" /> or <see cref="P:PX.Objects.PM.PMProject.ApproverID" /> must approve the activity before it can be
  /// released to the project.</value>
  [PXDBInt]
  [PXEPEmployeeSelector(Filterable = true)]
  [PXUIField]
  public virtual int? ApproverID
  {
    get => this._ApproverID;
    set => this._ApproverID = value;
  }

  /// <summary>The default cost code of the task.</summary>
  [PXDBInt]
  [PXForeignReference(typeof (Field<PMTask.defaultCostCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  [PXUIField]
  [PXUIEnabled(typeof (Where<FeatureInstalled<FeaturesSet.costCodes>>))]
  [PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.costCodes>>))]
  [CostCodeDimensionSelector(null, null, null, null, true)]
  public virtual int? DefaultCostCodeID { get; set; }

  /// <summary>Obsolete field. Not used anywhere.</summary>
  /// <exclude />
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  /// <summary>The default sales account. The value can be used in an allocation or a billing as a default for the new <see cref="T:PX.Objects.PM.PMTran" /> and <see cref="T:PX.Objects.AR.ARTran" />.</summary>
  [PXDefault(typeof (PMProject.defaultSalesAccountID))]
  [Account(DisplayName = "Default Sales Account", AvoidControlAccounts = true)]
  public virtual int? DefaultSalesAccountID { get; set; }

  /// <summary>The default sales subaccount. The value can be used in an allocation or a billing as a default for the new <see cref="T:PX.Objects.PM.PMTran" /> and <see cref="T:PX.Objects.AR.ARTran" />.</summary>
  [PXDefault(typeof (PMProject.defaultSalesSubID))]
  [SubAccount(typeof (PMTask.defaultSalesAccountID), typeof (PMTask.defaultBranchID), false)]
  public virtual int? DefaultSalesSubID { get; set; }

  /// <summary>The default cost account. The value can be used in an allocation or a cost transaction as a default for the new <see cref="T:PX.Objects.PM.PMTran" />.</summary>
  [PXDefault(typeof (PMProject.defaultExpenseAccountID))]
  [Account(DisplayName = "Default Cost Account", AvoidControlAccounts = true)]
  public virtual int? DefaultExpenseAccountID { get; set; }

  /// <summary>The default cost subaccount. The value can be used in an allocation or a cost transaction as a default for the new <see cref="T:PX.Objects.PM.PMTran" />.</summary>
  [PXDefault(typeof (PMProject.defaultExpenseSubID))]
  [SubAccount(typeof (PMTask.defaultExpenseAccountID), typeof (PMTask.defaultBranchID), false)]
  public virtual int? DefaultExpenseSubID { get; set; }

  /// <summary>The default accrual account. The field is used depending on the <see cref="P:PX.Objects.PM.PMSetup.ExpenseAccrualSubMask" /> mask setting.</summary>
  [PXDefault(typeof (PMProject.defaultAccrualAccountID))]
  [Account(DisplayName = "Accrual Account", AvoidControlAccounts = true)]
  public virtual int? DefaultAccrualAccountID
  {
    get => this._DefaultAccrualAccountID;
    set => this._DefaultAccrualAccountID = value;
  }

  /// <summary>The default accrual subaccount. The field is used depending on the <see cref="P:PX.Objects.PM.PMSetup.ExpenseAccrualSubMask" /> mask setting.</summary>
  [PXDefault(typeof (PMProject.defaultAccrualSubID))]
  [SubAccount]
  public virtual int? DefaultAccrualSubID
  {
    get => this._DefaultAccrualSubID;
    set => this._DefaultAccrualSubID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch" /> associated with the project task.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(null, null, true, true, false)]
  [PXDefault(typeof (Search<PMProject.defaultBranchID, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  public virtual int? DefaultBranchID
  {
    get => this._DefaultBranchID;
    set => this._DefaultBranchID = value;
  }

  /// <summary>
  /// The identifier of the work-in-progress <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> associated with the task.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [AccountGroup(DisplayName = "Non-Billable WIP Account Group")]
  public virtual int? WipAccountGroupID
  {
    get => this._WipAccountGroupID;
    set => this._WipAccountGroupID = value;
  }

  /// <summary>The task completion state in percents. Depending on settings, this value either maintained manually or can be auto-calculated based on the budget ratio of
  /// actual or revised values.</summary>
  [PXDBDecimal(2, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIEnabled(typeof (Where<PMTask.completedPctMethod, Equal<PMCompletedPctMethod.manual>>))]
  [PXUIField(DisplayName = "Completed (%)")]
  public virtual Decimal? CompletedPercent { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is default.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Default")]
  public virtual bool? IsDefault { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is active. <see cref="T:PX.Objects.PM.PMTran" /> can be created only for active tasks.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is completed.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsCompleted
  {
    get => this._IsCompleted;
    set => this._IsCompleted = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is cancelled.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsCancelled
  {
    get => this._IsCancelled;
    set => this._IsCancelled = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is billed in a separate invoice.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Bill Separately")]
  public virtual bool? BillSeparately
  {
    get => this._BillSeparately;
    set => this._BillSeparately = value;
  }

  [PXDBString]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Progress Billing Basis")]
  [PX.Objects.PM.ProgressBillingBase.List]
  public string ProgressBillingBase { get; set; }

  /// <summary>The task type.</summary>
  [PXDBString(10)]
  [PXDefault("CostRev")]
  [PXUIField(DisplayName = "Type", Required = true)]
  [ProjectTaskType.List]
  public string Type { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is visible in the GL module. If the task is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMProject.visibleInGL, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXUIField(DisplayName = "GL")]
  public virtual bool? VisibleInGL
  {
    get => this._VisibleInGL;
    set => this._VisibleInGL = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is visible in the AP module. If the task is invisible, it will not be displayed in the field selectors in
  /// this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMProject.visibleInAP, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXUIField(DisplayName = "AP")]
  public virtual bool? VisibleInAP
  {
    get => this._VisibleInAP;
    set => this._VisibleInAP = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is visible in the AR module. If the task is invisible, it will not be displayed in the field selectors in
  /// this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMProject.visibleInAR, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXUIField(DisplayName = "AR")]
  public virtual bool? VisibleInAR
  {
    get => this._VisibleInAR;
    set => this._VisibleInAR = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is visible in the SO module. If the task is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMProject.visibleInSO, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXUIField(DisplayName = "SO")]
  public virtual bool? VisibleInSO
  {
    get => this._VisibleInSO;
    set => this._VisibleInSO = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is visible in the PO module. If the task is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMProject.visibleInPO, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXUIField(DisplayName = "PO")]
  public virtual bool? VisibleInPO
  {
    get => this._VisibleInPO;
    set => this._VisibleInPO = value;
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMProject.visibleInTA, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXUIField(DisplayName = "Time Entries")]
  public virtual bool? VisibleInTA
  {
    get => this._VisibleInTA;
    set => this._VisibleInTA = value;
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMProject.visibleInEA, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXUIField(DisplayName = "Expenses")]
  public virtual bool? VisibleInEA
  {
    get => this._VisibleInEA;
    set => this._VisibleInEA = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is visible in the IN module. If the task is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMProject.visibleInIN, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXUIField(DisplayName = "IN")]
  public virtual bool? VisibleInIN
  {
    get => this._VisibleInIN;
    set => this._VisibleInIN = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is visible in the CA module. If the task is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMProject.visibleInCA, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXUIField(DisplayName = "CA")]
  public virtual bool? VisibleInCA
  {
    get => this._VisibleInCA;
    set => this._VisibleInCA = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the task is visible in the CR module. If the task is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMProject.visibleInCR, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXUIField(DisplayName = "CRM")]
  public virtual bool? VisibleInCR
  {
    get => this._VisibleInCR;
    set => this._VisibleInCR = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that this task should be automatically created when a template is assigned to the project. This field is used for
  /// project templates.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatically Include in Project")]
  public virtual bool? AutoIncludeInPrj
  {
    get => this._AutoIncludeInPrj;
    set => this._AutoIncludeInPrj = value;
  }

  [PXString]
  [PXFormula(typeof (PMTask.description))]
  public string FormCaptionDescription { get; set; }

  /// <summary>The entity attributes.</summary>
  [CRAttributesField(typeof (PMTask.classID))]
  public virtual string[] Attributes { get; set; }

  /// <summary>The class ID for the attributes.</summary>
  /// <value>Always returns <see cref="F:PX.Objects.PM.GroupTypes.Task" />.</value>
  [PXString(20)]
  public virtual string ClassID => "TASK";

  [PXInt]
  public virtual int? TemplateID { get; set; }

  [PXNote(DescriptionField = typeof (PMTask.taskCD))]
  [NotePersist(typeof (PMTask.noteID))]
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

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>
  {
    public static PMTask Find(PXGraph graph, int? projectID, int? taskID, PKFindOptions options = 0)
    {
      return (PMTask) PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.FindBy(graph, (object) projectID, (object) taskID, options);
    }

    public static PMTask FindDirty(PXGraph graph, int? projectID, int? taskID)
    {
      return PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) projectID,
        (object) taskID
      }));
    }
  }

  /// <summary>Unique Key</summary>
  public class UK : PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskCD>
  {
    public static PMTask Find(PXGraph graph, int? projectID, string taskCD, PKFindOptions options = 0)
    {
      return (PMTask) PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskCD>.FindBy(graph, (object) projectID, (object) taskCD, options);
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.selected>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.taskID>
  {
  }

  public abstract class taskCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.taskCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.description>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.customerID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.locationID>
  {
  }

  public abstract class rateTableID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.rateTableID>
  {
  }

  public abstract class billingID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.billingID>
  {
  }

  public abstract class allocationID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.allocationID>
  {
  }

  public abstract class billingOption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.billingOption>
  {
  }

  public abstract class completedPctMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTask.completedPctMethod>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.status>
  {
  }

  public abstract class plannedStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMTask.plannedStartDate>
  {
  }

  public abstract class plannedEndDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMTask.plannedEndDate>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMTask.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMTask.endDate>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.extRefNbr>
  {
  }

  public abstract class approverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.approverID>
  {
  }

  public abstract class defaultCostCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.defaultCostCodeID>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.taxCategoryID>
  {
  }

  public abstract class defaultSalesAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTask.defaultSalesAccountID>
  {
  }

  public abstract class defaultSalesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.defaultSalesSubID>
  {
  }

  public abstract class defaultExpenseAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTask.defaultExpenseAccountID>
  {
  }

  public abstract class defaultExpenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.defaultExpenseSubID>
  {
  }

  public abstract class defaultAccrualAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTask.defaultAccrualAccountID>
  {
  }

  public abstract class defaultAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.defaultAccrualSubID>
  {
  }

  public abstract class defaultBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.defaultBranchID>
  {
  }

  public abstract class wipAccountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.wipAccountGroupID>
  {
  }

  public abstract class completedPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTask.completedPercent>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.isDefault>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.isActive>
  {
  }

  public abstract class isCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.isCompleted>
  {
  }

  public abstract class isCancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.isCancelled>
  {
  }

  public abstract class billSeparately : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.billSeparately>
  {
  }

  public abstract class progressBillingBase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTask.progressBillingBase>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.type>
  {
  }

  public abstract class visibleInGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.visibleInGL>
  {
  }

  public abstract class visibleInAP : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.visibleInAP>
  {
  }

  public abstract class visibleInAR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.visibleInAR>
  {
  }

  public abstract class visibleInSO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.visibleInSO>
  {
  }

  public abstract class visibleInPO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.visibleInPO>
  {
  }

  public abstract class visibleInTA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.visibleInTA>
  {
  }

  public abstract class visibleInEA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.visibleInEA>
  {
  }

  public abstract class visibleInIN : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.visibleInIN>
  {
  }

  public abstract class visibleInCA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.visibleInCA>
  {
  }

  public abstract class visibleInCR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.visibleInCR>
  {
  }

  public abstract class autoIncludeInPrj : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.autoIncludeInPrj>
  {
  }

  public abstract class attributes : BqlType<IBqlAttributes, string[]>.Field<PMTask.attributes>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.classID>
  {
  }

  public abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.templateID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMTask.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMTask.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMTask.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTask.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMTask.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMTask.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTask.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMTask.lastModifiedDateTime>
  {
  }
}
