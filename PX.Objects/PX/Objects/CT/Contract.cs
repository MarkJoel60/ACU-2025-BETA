// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.Contract
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.TM;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.CT;

[PXPrimaryGraph(new System.Type[] {typeof (TemplateMaint), typeof (ContractMaint), typeof (PX.Objects.PM.TemplateMaint), typeof (ProjectEntry), typeof (PX.Objects.PM.TemplateMaint), typeof (ProjectEntry)}, new System.Type[] {typeof (Select<Contract, Where<Contract.contractID, Equal<Current<Contract.contractID>>, And<Contract.baseType, Equal<CTPRType.contractTemplate>>>>), typeof (Select<Contract, Where<Contract.contractID, Equal<Current<Contract.contractID>>, And<Contract.baseType, Equal<CTPRType.contract>>>>), typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMProject.contractID>>, And<PMProject.baseType, Equal<CTPRType.projectTemplate>, And<PMProject.nonProject, Equal<False>>>>>), typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMProject.contractID>>, And<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>>>), typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<Contract.contractID>>, And<PMProject.baseType, Equal<CTPRType.projectTemplate>, And<PMProject.nonProject, Equal<False>>>>>), typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<Contract.contractID>>, And<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>>>)})]
[PXCacheName("Contract")]
[Serializable]
public class Contract : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _ContractID;
  protected 
  #nullable disable
  string _ContractCD;
  protected int? _TemplateID;
  protected string _Description;
  public string _contractInfo;
  protected int? _OriginalContractID;
  protected int? _MasterContractID;
  protected int? _CaseItemID;
  protected string _Type;
  protected string _ClassType;
  protected int? _CustomerID;
  protected int? _LocationID;
  protected string _RateTableID;
  protected Decimal? _Balance;
  protected string _Status;
  protected int? _Duration;
  protected string _DurationType;
  protected DateTime? _StartDate;
  protected DateTime? _ActivationDate;
  protected DateTime? _ExpireDate;
  protected DateTime? _TerminationDate;
  protected int? _GracePeriod;
  protected bool? _AutoRenew;
  protected int? _AutoRenewDays;
  protected string _CuryID;
  protected string _RateTypeID;
  protected bool? _AllowOverrideCury;
  protected bool? _AllowOverrideRate;
  protected string _CalendarID;
  protected bool? _AutomaticReleaseAR;
  protected bool? _Refundable;
  protected int? _RefundPeriod;
  protected DateTime? _EffectiveFrom;
  protected DateTime? _DiscontinueAfter;
  protected string _DiscountID;
  protected int? _DetailedBilling;
  protected bool? _AllowOverride;
  protected bool? _RefreshOnRenewal;
  protected bool? _IsContinuous;
  protected int? _DefaultAccrualAccountID;
  protected int? _DefaultAccrualSubID;
  protected int? _DefaultBranchID;
  protected bool? _RestrictToEmployeeList;
  protected bool? _RestrictToResourceList;
  protected int? _ApproverID;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected int? _SalesPersonID;
  protected string _ScheduleStartsOn;
  protected string _BillingID;
  protected string _AllocationID;
  protected int? _ContractAccountGroup;
  protected Decimal? _PendingSetup;
  protected Decimal? _PendingRecurring;
  protected Decimal? _PendingRenewal;
  protected Decimal? _TotalPending;
  protected Decimal? _CurrentSetup;
  protected Decimal? _CurrentRecurring;
  protected Decimal? _CurrentRenewal;
  protected Decimal? _TotalRecurring;
  protected Decimal? _TotalUsage;
  protected Decimal? _TotalDue;
  protected bool? _Hold;
  protected bool? _Approved;
  protected bool? _Rejected = new bool?(false);
  protected bool? _IsActive;
  protected bool? _IsCompleted;
  protected bool? _IsCancelled;
  protected bool? _IsPendingUpdate;
  protected bool? _AutoAllocate;
  protected bool? _IsLastActionUndoable;
  protected bool? _VisibleInGL;
  protected bool? _VisibleInAP;
  protected bool? _VisibleInAR;
  protected bool? _VisibleInSO;
  protected bool? _VisibleInPO;
  protected bool? _VisibleInTA;
  protected bool? _VisibleInEA;
  protected bool? _VisibleInIN;
  protected bool? _VisibleInCA;
  protected bool? _VisibleInCR;
  protected bool? _NonProject;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _GroupMask;
  protected string _DaysBeforeExpiration;
  protected string _Days;
  protected string _Min;
  protected bool? _ServiceActivate;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBIdentity]
  [PXUIField(DisplayName = "Contract ID")]
  public virtual int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXUIField(DisplayName = "Entity Type", Enabled = false)]
  [PXDBString(1, IsFixed = true, IsKey = true)]
  [CTPRType.List]
  [PXDefault("C")]
  public virtual string BaseType { get; set; }

  [PXDimensionSelector("CONTRACT", typeof (Search2<Contract.contractCD, InnerJoin<ContractBillingSchedule, On<Contract.contractID, Equal<ContractBillingSchedule.contractID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<Contract.customerID>>>>, Where<Contract.baseType, Equal<CTPRType.contract>>>), typeof (Contract.contractCD), new System.Type[] {typeof (Contract.contractCD), typeof (Contract.customerID), typeof (PX.Objects.AR.Customer.acctName), typeof (Contract.locationID), typeof (Contract.description), typeof (Contract.status), typeof (Contract.expireDate), typeof (ContractBillingSchedule.lastDate), typeof (ContractBillingSchedule.nextDate)}, DescriptionField = typeof (Contract.description), Filterable = true)]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string ContractCD
  {
    get => this._ContractCD;
    set
    {
      if (this._ContractCD != value)
        this._contractInfo = (string) null;
      this._ContractCD = value;
    }
  }

  [PXDefault]
  [ContractTemplate(Required = true)]
  [PXRestrictor(typeof (Where<ContractTemplate.status, Equal<Contract.status.active>>), "Template {0} is not activated.", new System.Type[] {typeof (ContractTemplate.contractCD)})]
  [PXRestrictor(typeof (Where<Contract.effectiveFrom, LessEqual<Current<AccessInfo.businessDate>>, Or<Contract.effectiveFrom, IsNull>>), "Template is not effective yet.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Contract.discontinueAfter, GreaterEqual<Current<AccessInfo.businessDate>>, Or<Contract.discontinueAfter, IsNull>>), "Template is expired.", new System.Type[] {})]
  public virtual int? TemplateID
  {
    get => this._TemplateID;
    set => this._TemplateID = value;
  }

  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set
    {
      if (this._Description != value)
        this._contractInfo = (string) null;
      this._Description = value;
    }
  }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string ContractInfo
  {
    get
    {
      if (this._contractInfo == null)
        this._contractInfo = $"{this.ContractCD} - {this.Description}";
      return this._contractInfo;
    }
  }

  [Contract(DisplayName = "Contract", Enabled = false)]
  public virtual int? OriginalContractID
  {
    get => this._OriginalContractID;
    set => this._OriginalContractID = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Contract.contractID))]
  [PXUIField(DisplayName = "Master Contract")]
  public virtual int? MasterContractID
  {
    get => this._MasterContractID;
    set => this._MasterContractID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Case Count Item")]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXForeignReference(typeof (Field<Contract.caseItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? CaseItemID
  {
    get => this._CaseItemID;
    set => this._CaseItemID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Contract Type")]
  [Contract.type.List]
  [PXDefault("R")]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBString(1, IsFixed = true)]
  public virtual string ClassType
  {
    get => this._ClassType;
    set => this._ClassType = value;
  }

  [CustomerActive(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<Contract.customerID>>>))]
  [PXDefault(typeof (Search<PX.Objects.CR.BAccount.defLocationID, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<Contract.customerID>>>>))]
  [PXForeignReference(typeof (CompositeKey<Field<Contract.customerID>.IsRelatedTo<PX.Objects.CR.Location.bAccountID>, Field<Contract.locationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>>))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string RateTableID
  {
    get => this._RateTableID;
    set => this._RateTableID = value;
  }

  [PXBaseCury]
  [PXUIField(DisplayName = "Balance", Enabled = false)]
  public Decimal? Balance
  {
    get => this._Balance;
    set => this._Balance = value;
  }

  [PXDBString(1, IsFixed = true)]
  [Contract.status.List]
  [PXDefault("D")]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBInt(MinValue = 1, MaxValue = 1000)]
  [PXUIField(DisplayName = "Duration")]
  [PXDefault(1)]
  public virtual int? Duration
  {
    get => this._Duration;
    set => this._Duration = value;
  }

  [PXDBString(1, IsFixed = true)]
  [Contract.durationType.List]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Duration Unit")]
  public virtual string DurationType
  {
    get => this._DurationType;
    set => this._DurationType = value;
  }

  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXDBDate]
  [PXUIField(DisplayName = "Setup Date", Required = true)]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDefault(typeof (Contract.startDate))]
  [PXDBDate]
  [PXFormula(typeof (Switch<Case<Where<Contract.startDate, Greater<Current<Contract.activationDate>>>, Current<Contract.startDate>>, Contract.activationDate>))]
  [PXUIField(DisplayName = "Activation Date")]
  public virtual DateTime? ActivationDate
  {
    get => this._ActivationDate;
    set => this._ActivationDate = value;
  }

  [PXDBDate]
  [PXFormula(typeof (Switch<Case<Where<Contract.scheduleStartsOn, Equal<Contract.scheduleStartsOn.setupDate>>, Contract.startDate, Case<Where<Contract.scheduleStartsOn, Equal<Contract.scheduleStartsOn.activationDate>>, Contract.activationDate>>>))]
  public virtual DateTime? RenewalBillingStartDate { get; set; }

  [PXDBDate]
  [PXUIField]
  [PXVerifyEndDate(typeof (Contract.startDate), AllowAutoChange = false)]
  [PXUIEnabled(typeof (Where<Contract.status, NotEqual<Contract.status.expired>, And<Contract.status, NotEqual<Contract.status.canceled>, And<Contract.type, NotEqual<Contract.type.unlimited>>>>))]
  [PXFormula(typeof (ContractExpirationDate<Contract.type, Contract.durationType, Contract.renewalBillingStartDate, Contract.duration>))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? TerminationDate
  {
    get => this._TerminationDate;
    set => this._TerminationDate = value;
  }

  /// <summary>
  /// Period in days Contract is serviced even after it has expired. Warning is shown whenever user
  /// selects the contract that falls in this period.
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Grace Period")]
  [PXUIEnabled(typeof (Where<Contract.type, Equal<Contract.type.renewable>, And<Where<EntryStatus, Equal<EntryStatus.inserted>, Or<Contract.status, NotEqual<Contract.status.canceled>>>>>))]
  public virtual int? GracePeriod
  {
    get => this._GracePeriod;
    set => this._GracePeriod = value;
  }

  /// <summary>End Date of Grace Period.</summary>
  [PXDBCalced(typeof (Add<Contract.expireDate, Contract.gracePeriod>), typeof (DateTime))]
  public virtual DateTime? GraceDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Mass Renewal")]
  public virtual bool? AutoRenew
  {
    get => this._AutoRenew;
    set => this._AutoRenew = value;
  }

  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Renewal Point")]
  public virtual int? AutoRenewDays
  {
    get => this._AutoRenewDays;
    set => this._AutoRenewDays = value;
  }

  [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2019R2.")]
  [PXDBCalced(typeof (Switch<Case<Where<Contract.baseType, Equal<CTPRType.contractTemplate>, Or<Contract.baseType, Equal<CTPRType.projectTemplate>>>, True>, False>), typeof (bool))]
  public virtual bool? IsTemplate
  {
    get => new bool?(CTPRType.IsTemplate(this.BaseType));
    set
    {
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Type")]
  [PXStringList(new string[] {"Contract", "Template"}, new string[] {"Contract", "Template"})]
  public virtual string StrIsTemplate
  {
    get => !CTPRType.IsTemplate(this.BaseType) ? nameof (Contract) : "Template";
  }

  [PXDefault]
  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString(6, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID))]
  [PXUIField(DisplayName = "Rate Type", Required = true)]
  [PXDefault(typeof (Search<CMSetup.aRRateTypeDflt>))]
  public virtual string RateTypeID
  {
    get => this._RateTypeID;
    set => this._RateTypeID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Currency Override")]
  public virtual bool? AllowOverrideCury
  {
    get => this._AllowOverrideCury;
    set => this._AllowOverrideCury = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Rate Override")]
  public virtual bool? AllowOverrideRate
  {
    get => this._AllowOverrideRate;
    set => this._AllowOverrideRate = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Calendar")]
  [PXSelector(typeof (Search<PX.Objects.CS.CSCalendar.calendarID>), DescriptionField = typeof (PX.Objects.CS.CSCalendar.description))]
  public virtual string CalendarID
  {
    get => this._CalendarID;
    set => this._CalendarID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Create Pro Forma Invoice on Billing")]
  public virtual bool? CreateProforma { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? AutomaticReleaseAR
  {
    get => this._AutomaticReleaseAR;
    set => this._AutomaticReleaseAR = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Refundable")]
  public virtual bool? Refundable
  {
    get => this._Refundable;
    set => this._Refundable = value;
  }

  [PXDBInt(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Refund Period")]
  public virtual int? RefundPeriod
  {
    get => this._RefundPeriod;
    set => this._RefundPeriod = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Effective From")]
  public virtual DateTime? EffectiveFrom
  {
    get => this._EffectiveFrom;
    set => this._EffectiveFrom = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Discontinue After")]
  public virtual DateTime? DiscontinueAfter
  {
    get => this._DiscontinueAfter;
    set => this._DiscontinueAfter = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Promo Code")]
  [Contract.PromoDiscIDSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, Equal<DiscountType.LineDiscount>>>))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXIntList(new int[] {0, 1}, new string[] {"Summary", "Detail"})]
  [PXUIField(DisplayName = "Billing Format")]
  public virtual int? DetailedBilling
  {
    get => this._DetailedBilling;
    set => this._DetailedBilling = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Template Item Override")]
  public virtual bool? AllowOverride
  {
    get => this._AllowOverride;
    set => this._AllowOverride = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Refresh Items from Template on Renewal")]
  public virtual bool? RefreshOnRenewal
  {
    get => this._RefreshOnRenewal;
    set => this._RefreshOnRenewal = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public virtual bool? IsContinuous
  {
    get => this._IsContinuous;
    set => this._IsContinuous = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">default sales account</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(DisplayName = "Default Sales Account", AvoidControlAccounts = true)]
  public virtual int? DefaultSalesAccountID { get; set; }

  [SubAccount]
  public virtual int? DefaultSalesSubID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">default cost account</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(DisplayName = "Default Cost Account", AvoidControlAccounts = true)]
  public virtual int? DefaultExpenseAccountID { get; set; }

  [SubAccount]
  public virtual int? DefaultExpenseSubID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">default accrual account</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(DisplayName = "Accrual Account", AvoidControlAccounts = true)]
  public virtual int? DefaultAccrualAccountID
  {
    get => this._DefaultAccrualAccountID;
    set => this._DefaultAccrualAccountID = value;
  }

  [SubAccount]
  public virtual int? DefaultAccrualSubID
  {
    get => this._DefaultAccrualSubID;
    set => this._DefaultAccrualSubID = value;
  }

  /// <summary>The default overbilling account of the project.</summary>
  [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.type, Equal<AccountType.liability>>>>>), DisplayName = "Overbilling Account", AvoidControlAccounts = true)]
  public virtual int? DefaultOverbillingAccountID { get; set; }

  /// <summary>The default overbilling subaccount of the project.</summary>
  [SubAccount]
  [PXDefault(typeof (BqlField<GLSetup.defaultSubID, IBqlInt>.FromSetup))]
  public virtual int? DefaultOverbillingSubID { get; set; }

  /// <summary>The default underbilling account of the project.</summary>
  [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.type, Equal<AccountType.asset>>>>>), DisplayName = "Underbilling Account", AvoidControlAccounts = true)]
  public virtual int? DefaultUnderbillingAccountID { get; set; }

  /// <summary>The default underbilling subaccount of the project.</summary>
  [SubAccount]
  [PXDefault(typeof (BqlField<GLSetup.defaultSubID, IBqlInt>.FromSetup))]
  public virtual int? DefaultUnderbillingSubID { get; set; }

  /// <summary>
  /// This field is used in the <see cref="T:PX.Objects.PM.PMProject" /> class.
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch" /> associated with the project.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(null, null, true, true, true)]
  public virtual int? DefaultBranchID
  {
    get => this._DefaultBranchID;
    set => this._DefaultBranchID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search2<PMQuote.quoteNbr, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PMQuote.bAccountID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PMQuote.contactID>>>>, Where<PMQuote.quoteType, Equal<CRQuoteTypeAttribute.project>>, OrderBy<Desc<PMQuote.quoteNbr>>>), new System.Type[] {typeof (PMQuote.quoteNbr), typeof (PMQuote.status), typeof (PMQuote.subject), typeof (PX.Objects.CR.BAccount.acctCD), typeof (PMQuote.documentDate), typeof (PMQuote.expirationDate)}, Filterable = true)]
  [PXUIField(DisplayName = "Quote Ref. Nbr.", FieldClass = "ProjectQuotes")]
  [PXFieldDescription]
  public virtual string QuoteNbr { get; set; }

  /// <summary>
  /// This field is used in the <see cref="T:PX.Objects.PM.PMProject" /> class.
  /// Specifies (if set to <see langword="true" />) that only the employees associated with the current
  /// <see cref="T:PX.Objects.PM.PMProject">project</see> can create activities and documents that is associated with the current project.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Restrict Employees")]
  public virtual bool? RestrictToEmployeeList
  {
    get => this._RestrictToEmployeeList;
    set => this._RestrictToEmployeeList = value;
  }

  /// <summary>
  /// This field is used in the <see cref="T:PX.Objects.PM.PMProject" /> class.
  /// Specifies (if set to <see langword="true" />) that equipment time cards can be associated with the
  /// current <see cref="T:PX.Objects.PM.PMProject">project</see> for only the equipment that is associated with the current project.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Restrict Equipment")]
  public virtual bool? RestrictToResourceList
  {
    get => this._RestrictToResourceList;
    set => this._RestrictToResourceList = value;
  }

  [PXDBInt]
  [PXEPEmployeeSelector]
  [PXUIField]
  public virtual int? ApproverID
  {
    get => this._ApproverID;
    set => this._ApproverID = value;
  }

  /// <summary>The workgroup that is responsible for the document.</summary>
  [PXInt]
  [PXFormula(typeof (Selector<Contract.customerID, PX.Objects.CR.BAccount.workgroupID>))]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  [PXDefault]
  [Owner(typeof (Contract.workgroupID))]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [PXDefault]
  [SalesPerson(DescriptionField = typeof (PX.Objects.AR.SalesPerson.descr), DisplayName = "Sales Person")]
  [PXForeignReference(typeof (Field<Contract.salesPersonID>.IsRelatedTo<PX.Objects.AR.SalesPerson.salesPersonID>))]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  [PXDefault("A")]
  [PXDBString(1, IsFixed = true)]
  [Contract.scheduleStartsOn.List]
  [PXUIField(DisplayName = "Billing Schedule Starts On")]
  public virtual string ScheduleStartsOn
  {
    get => this._ScheduleStartsOn;
    set => this._ScheduleStartsOn = value;
  }

  [PXDBString(1)]
  [PXDefault("P")]
  public virtual string AccountingMode { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Prepaid Amount", Visible = false)]
  public virtual bool? PrepaymentEnabled { get; set; }

  [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID, Where<DRDeferredCode.accountType, Equal<DeferredAccountType.income>>>), DescriptionField = typeof (DRDeferredCode.description))]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Deferral Code", FieldClass = "DEFFERED", Visible = false)]
  public virtual string PrepaymentDefCode { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use T&M Revenue Budget Limits")]
  public virtual bool? LimitsEnabled { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Search<FeaturesSet.changeOrder>))]
  [PXUIField(DisplayName = "Change Order Workflow", FieldClass = "CHANGEORDER")]
  public virtual bool? ChangeOrderWorkflow { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? LockCommitments { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Track Production Data")]
  public virtual bool? BudgetMetricsEnabled { get; set; }

  /// <summary>
  /// This field is used in the <see cref="T:PX.Objects.PM.PMProject" /> class.
  /// Specifies (if set to <see langword="true" />) that the job performed in the scope of the <see cref="T:PX.Objects.PM.PMProject">project</see>
  /// is a certified job performed for the government, such as a hospital construction project.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Certified Job", FieldClass = "Construction")]
  public virtual bool? CertifiedJob { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Quantity in AIA Report", FieldClass = "Construction")]
  public bool? IncludeQtyInAIA { get; set; }

  [PXDBString(1, IsFixed = true)]
  [Contract.aIALevel.List]
  [PXDefault("S")]
  [PXUIField(DisplayName = "AIA Level", FieldClass = "Construction")]
  public virtual string AIALevel { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string BillingID
  {
    get => this._BillingID;
    set => this._BillingID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string AllocationID
  {
    get => this._AllocationID;
    set => this._AllocationID = value;
  }

  [PXSelector(typeof (Search<PMAccountGroup.groupID, Where<PMAccountGroup.type, Equal<PMAccountType.offBalance>>>), SubstituteKey = typeof (PMAccountGroup.groupCD))]
  [PXUIField(DisplayName = "Account Group")]
  [PXDBInt]
  public virtual int? ContractAccountGroup
  {
    get => this._ContractAccountGroup;
    set => this._ContractAccountGroup = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Terms">Credit Terms</see> object associated with the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.AR.Customer.TermsID">credit terms</see> that are selected for the <see cref="P:PX.Objects.CT.Contract.CustomerID">customer</see>.
  /// Corresponds to the <see cref="P:PX.Objects.CS.Terms.TermsID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<PX.Objects.AR.Customer.termsID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<Contract.customerID>>>>))]
  [PXUIField(DisplayName = "Terms")]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  public virtual string TermsID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Last Revenue Change Nbr.", FieldClass = "CHANGEORDER")]
  public virtual string LastChangeOrderNumber { get; set; }

  /// <summary>
  /// This field is used in the <see cref="T:PX.Objects.PM.PMProject" /> class.
  /// The application number that has been assigned to the last <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see>
  /// with progress billing lines prepared for the current <see cref="T:PX.Objects.PM.PMProject">project</see>.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Last Application Nbr.", FieldClass = "Construction")]
  public virtual string LastProformaNumber { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainagePct { get; set; }

  /// <summary>Retainage Cap %</summary>
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cap (%)", FieldClass = "Retainage")]
  public virtual Decimal? RetainageMaxPct { get; set; }

  /// <summary>Retainage Mode</summary>
  [PXDBString(1, IsFixed = true)]
  [RetainageModes.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Retainage Mode", FieldClass = "Retainage")]
  public virtual string RetainageMode { get; set; }

  /// <summary>Include Change Orders in Contract Total</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include CO", FieldClass = "Retainage")]
  public virtual bool? IncludeCO { get; set; }

  /// <summary>Stepped Retainage</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Steps", FieldClass = "Retainage")]
  public virtual bool? SteppedRetainage { get; set; }

  [Obsolete]
  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Site Address", FieldClass = "Construction")]
  public virtual string SiteAddress { get; set; }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Setup", Enabled = false)]
  public virtual Decimal? PendingSetup
  {
    get => this._PendingSetup;
    set => this._PendingSetup = value;
  }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Recurring", Enabled = false)]
  public virtual Decimal? PendingRecurring
  {
    get => this._PendingRecurring;
    set => this._PendingRecurring = value;
  }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Renewal", Enabled = false)]
  public virtual Decimal? PendingRenewal
  {
    get => this._PendingRenewal;
    set => this._PendingRenewal = value;
  }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Add<Contract.pendingRecurring, Add<Contract.pendingRenewal, Contract.pendingSetup>>))]
  [PXUIField(DisplayName = "Total Pending", Enabled = false)]
  public virtual Decimal? TotalPending
  {
    get => this._TotalPending;
    set => this._TotalPending = value;
  }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Current Setup", Enabled = false)]
  public virtual Decimal? CurrentSetup
  {
    get => this._CurrentSetup;
    set => this._CurrentSetup = value;
  }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Current Recurring", Enabled = false)]
  public virtual Decimal? CurrentRecurring
  {
    get => this._CurrentRecurring;
    set => this._CurrentRecurring = value;
  }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Current Renewal", Enabled = false)]
  public virtual Decimal? CurrentRenewal
  {
    get => this._CurrentRenewal;
    set => this._CurrentRenewal = value;
  }

  [PXInt]
  public virtual int? TotalsCalculated { get; set; }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Recurring Total", Enabled = false)]
  public virtual Decimal? TotalRecurring
  {
    get => this._TotalRecurring;
    set => this._TotalRecurring = value;
  }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Extra Usage Total", Enabled = false)]
  public virtual Decimal? TotalUsage
  {
    get => this._TotalUsage;
    set => this._TotalUsage = value;
  }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Due", Enabled = false)]
  public virtual Decimal? TotalDue
  {
    get => this._TotalDue;
    set => this._TotalDue = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  [PXNoUpdate]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Approved
  {
    get => this._Approved;
    set => this._Approved = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Rejected
  {
    get => this._Rejected;
    set => this._Rejected = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCompleted
  {
    get => this._IsCompleted;
    set => this._IsCompleted = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCancelled
  {
    get => this._IsCancelled;
    set => this._IsCancelled = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsPendingUpdate
  {
    get => this._IsPendingUpdate;
    set => this._IsPendingUpdate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? AutoAllocate
  {
    get => this._AutoAllocate;
    set => this._AutoAllocate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsLastActionUndoable
  {
    get => this._IsLastActionUndoable;
    set => this._IsLastActionUndoable = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? VisibleInGL
  {
    get => this._VisibleInGL;
    set => this._VisibleInGL = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? VisibleInAP
  {
    get => this._VisibleInAP;
    set => this._VisibleInAP = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? VisibleInAR
  {
    get => this._VisibleInAR;
    set => this._VisibleInAR = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? VisibleInSO
  {
    get => this._VisibleInSO;
    set => this._VisibleInSO = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? VisibleInPO
  {
    get => this._VisibleInPO;
    set => this._VisibleInPO = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? VisibleInTA
  {
    get => this._VisibleInTA;
    set => this._VisibleInTA = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? VisibleInEA
  {
    get => this._VisibleInEA;
    set => this._VisibleInEA = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? VisibleInIN
  {
    get => this._VisibleInIN;
    set => this._VisibleInIN = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "CA")]
  public virtual bool? VisibleInCA
  {
    get => this._VisibleInCA;
    set => this._VisibleInCA = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "CRM")]
  public virtual bool? VisibleInCR
  {
    get => this._VisibleInCR;
    set => this._VisibleInCR = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? NonProject
  {
    get => this._NonProject;
    set => this._NonProject = value;
  }

  [PXDBInt(MinValue = 1)]
  [PXDefault(1)]
  public virtual int? RevID { get; set; }

  [PXDBInt(MinValue = 1)]
  public virtual int? LastActiveRevID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCtr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? BillingLineCntr { get; set; }

  [PXSearchable(2, "Contract: {0} - {2}", new System.Type[] {typeof (Contract.contractCD), typeof (Contract.customerID), typeof (PX.Objects.CR.BAccount.acctName)}, new System.Type[] {typeof (Contract.description)}, NumberFields = new System.Type[] {typeof (Contract.contractCD)}, Line1Format = "{0}{1}{2}", Line1Fields = new System.Type[] {typeof (Contract.templateID), typeof (Contract.status), typeof (Contract.type)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (Contract.description)}, WhereConstraint = typeof (Where<Current<Contract.baseType>, Equal<CTPRType.contract>, Or<Current<Contract.baseType>, Equal<CTPRType.contractTemplate>>>), MatchWithJoin = typeof (LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<Contract.customerID>>>))]
  [PXNote(DescriptionField = typeof (Contract.contractCD))]
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

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBGroupMask]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  [PXString(IsUnicode = true)]
  [PXUIField]
  public virtual string DaysBeforeExpiration
  {
    get => PXLocalizer.Localize("Days Before Expiration", typeof (Messages).FullName);
  }

  [PXString(IsUnicode = true)]
  [PXUIField]
  public virtual string Days => PXLocalizer.Localize(nameof (Days), typeof (Messages).FullName);

  [PXString]
  [PXUIField]
  public virtual string Min => PXLocalizer.Localize(nameof (Min), typeof (Messages).FullName);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ServiceActivate
  {
    get => this._ServiceActivate;
    set => this._ServiceActivate = value;
  }

  /// <summary>
  /// The string size is not restricted because the field is mapped to an unrestricted field with <see cref="T:PX.Data.PXDimensionSelectorAttribute" />.
  /// </summary>
  [PXString]
  [PXFormula(typeof (Selector<Contract.templateID, ContractTemplate.contractCD>))]
  [PXSelector(typeof (Search<ContractTemplate.contractCD, Where<ContractTemplate.baseType, Equal<CTPRType.contractTemplate>>>))]
  public virtual string ClassID { get; set; }

  [CRAttributesField(typeof (Contract.templateID))]
  public virtual string[] Attributes { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("O")]
  public virtual string DropshipExpenseAccountSource { get; set; }

  [PXDefault(typeof (PMSetup.dropshipExpenseSubMask))]
  [DropshipExpenseSubAccountMask]
  public virtual string DropshipExpenseSubMask { get; set; }

  [PXDBString(1)]
  [PXDefault("R")]
  public virtual string DropshipReceiptProcessing { get; set; }

  [PXDBString(1)]
  [PXDefault("B")]
  public virtual string DropshipExpenseRecording { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string CostTaxZoneID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string RevenueTaxZoneID { get; set; }

  [Obsolete("This class has been deprecated and will be removed in Acumatica ERP 2019R2.")]
  public class ContractBaseType : CTPRType.contract
  {
  }

  public class Events : PXEntityEventBase<Contract>.Container<Contract.Events>
  {
    public PXEntityEvent<Contract> SetupContract;
    public PXEntityEvent<Contract> ActivateContract;
    public PXEntityEvent<Contract> ExpireContract;
    public PXEntityEvent<Contract> CancelContract;
    public PXEntityEvent<Contract> UpgradeContract;
  }

  public class PK : PrimaryKeyOf<Contract>.By<Contract.contractID>
  {
    public static Contract Find(PXGraph graph, int? contractID, PKFindOptions options = 0)
    {
      return (Contract) PrimaryKeyOf<Contract>.By<Contract.contractID>.FindBy(graph, (object) contractID, options);
    }
  }

  public class UK : PrimaryKeyOf<Contract>.By<Contract.baseType, Contract.contractCD>
  {
    public static Contract Find(
      PXGraph graph,
      string baseType,
      string contractCD,
      PKFindOptions options = 0)
    {
      return (Contract) PrimaryKeyOf<Contract>.By<Contract.baseType, Contract.contractCD>.FindBy(graph, (object) baseType, (object) contractCD, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<Contract>.By<Contract.customerID>
    {
    }

    public class Location : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<Contract>.By<Contract.customerID, Contract.locationID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<Contract>.By<Contract.curyID>
    {
    }

    public class RateType : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyRateType>.By<PX.Objects.CM.CurrencyRateType.curyRateTypeID>.ForeignKeyOf<Contract>.By<Contract.rateTypeID>
    {
    }

    public class CSCalendar : 
      PrimaryKeyOf<PX.Objects.CS.CSCalendar>.By<PX.Objects.CS.CSCalendar.calendarID>.ForeignKeyOf<Contract>.By<Contract.calendarID>
    {
    }

    public class DefaultSaleAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Contract>.By<Contract.defaultSalesAccountID>
    {
    }

    public class DefaultSalesSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Contract>.By<Contract.defaultSalesSubID>
    {
    }

    public class DefaultExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Contract>.By<Contract.defaultExpenseAccountID>
    {
    }

    public class DefaultExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Contract>.By<Contract.defaultExpenseSubID>
    {
    }

    public class AccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Contract>.By<Contract.defaultAccrualAccountID>
    {
    }

    public class AccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Contract>.By<Contract.defaultAccrualSubID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<Contract>.By<Contract.defaultBranchID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<Contract>.By<Contract.salesPersonID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<Contract>.By<Contract.termsID>
    {
    }

    public class PromoCode : 
      PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.ForeignKeyOf<Contract>.By<Contract.discountID>
    {
    }

    public class ProjectManager : 
      PrimaryKeyOf<PX.Objects.EP.EPEmployee>.By<PX.Objects.EP.EPEmployee.bAccountID>.ForeignKeyOf<Contract>.By<Contract.approverID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.selected>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.contractID>
  {
  }

  public abstract class baseType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.baseType>
  {
  }

  public abstract class contractCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.contractCD>
  {
  }

  public abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.templateID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.description>
  {
  }

  public abstract class contractInfo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.contractInfo>
  {
  }

  public abstract class originalContractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.originalContractID>
  {
  }

  public abstract class masterContractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.masterContractID>
  {
  }

  public abstract class caseItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.caseItemID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.type>
  {
    public const string Renewable = "R";
    public const string Expiring = "E";
    public const string Unlimited = "U";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "R", "E", "U" }, new string[3]
        {
          "Renewable",
          "Expiring",
          "Unlimited"
        })
      {
      }
    }

    public class renewable : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.type.renewable>
    {
      public renewable()
        : base("R")
      {
      }
    }

    public class expiring : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.type.expiring>
    {
      public expiring()
        : base("E")
      {
      }
    }

    public class unlimited : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.type.unlimited>
    {
      public unlimited()
        : base("U")
      {
      }
    }
  }

  public abstract class classType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.classType>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.customerID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.locationID>
  {
  }

  public abstract class rateTableID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.rateTableID>
  {
  }

  public abstract class balance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Contract.balance>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.status>
  {
    public const string Draft = "D";
    public const string InApproval = "I";
    public const string Active = "A";
    public const string Expired = "E";
    public const string Canceled = "C";
    public const string Completed = "F";
    public const string InUpgrade = "U";
    public const string PendingActivation = "P";
    public const string OnHold = "H";
    public const string Rejected = "R";
    public const string Closed = "L";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[7]
        {
          "D",
          "I",
          "A",
          "E",
          "C",
          "U",
          "P"
        }, new string[7]
        {
          "Draft",
          "Pending Approval",
          "Active",
          "Expired",
          "Canceled",
          "Pending Upgrade",
          "Pending Activation"
        })
      {
      }
    }

    public class draft : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.status.draft>
    {
      public draft()
        : base("D")
      {
      }
    }

    public class inApproval : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.status.inApproval>
    {
      public inApproval()
        : base("I")
      {
      }
    }

    public class active : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.status.active>
    {
      public active()
        : base("A")
      {
      }
    }

    public class expired : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.status.expired>
    {
      public expired()
        : base("E")
      {
      }
    }

    public class canceled : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.status.canceled>
    {
      public canceled()
        : base("C")
      {
      }
    }

    public class completed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.status.completed>
    {
      public completed()
        : base("F")
      {
      }
    }

    public class inUpgrade : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.status.inUpgrade>
    {
      public inUpgrade()
        : base("U")
      {
      }
    }

    public class pendingActivation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      Contract.status.pendingActivation>
    {
      public pendingActivation()
        : base("P")
      {
      }
    }

    public class onHold : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.status.onHold>
    {
      public onHold()
        : base("H")
      {
      }
    }

    public class rejected : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.status.rejected>
    {
      public rejected()
        : base("R")
      {
      }
    }

    public class closed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.status.closed>
    {
      public closed()
        : base("L")
      {
      }
    }
  }

  public abstract class duration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.duration>
  {
  }

  public abstract class durationType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.durationType>
  {
    public const string Monthly = "M";
    public const string Quarterly = "Q";
    public const string Annual = "A";
    public const string Custom = "C";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[4]{ "A", "Q", "M", "C" }, new string[4]
        {
          "Year",
          "Quarter",
          "Month",
          "Custom (days)"
        })
      {
      }
    }

    public class annual : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.durationType.annual>
    {
      public annual()
        : base("A")
      {
      }
    }

    public class monthly : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.durationType.monthly>
    {
      public monthly()
        : base("M")
      {
      }
    }

    public class quarterly : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.durationType.quarterly>
    {
      public quarterly()
        : base("Q")
      {
      }
    }

    public class custom : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.durationType.custom>
    {
      public custom()
        : base("C")
      {
      }
    }
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Contract.startDate>
  {
  }

  public abstract class activationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Contract.activationDate>
  {
  }

  public abstract class renewalBillingStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Contract.renewalBillingStartDate>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Contract.expireDate>
  {
  }

  public abstract class terminationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Contract.terminationDate>
  {
  }

  public abstract class gracePeriod : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.gracePeriod>
  {
  }

  public abstract class graceDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Contract.graceDate>
  {
  }

  public abstract class autoRenew : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.autoRenew>
  {
  }

  public abstract class autoRenewDays : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.autoRenewDays>
  {
  }

  [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2019R2.")]
  public abstract class isTemplate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.isTemplate>
  {
  }

  public abstract class strIsTemplate : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.strIsTemplate>
  {
    public const string Contract = "Contract";
    public const string Template = "Template";
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.curyID>
  {
  }

  public abstract class rateTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.rateTypeID>
  {
  }

  public abstract class allowOverrideCury : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.allowOverrideCury>
  {
  }

  public abstract class allowOverrideRate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.allowOverrideRate>
  {
  }

  public abstract class calendarID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.calendarID>
  {
  }

  public abstract class createProforma : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.createProforma>
  {
  }

  public abstract class automaticReleaseAR : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Contract.automaticReleaseAR>
  {
  }

  public abstract class refundable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.refundable>
  {
  }

  public abstract class refundPeriod : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.refundPeriod>
  {
  }

  public abstract class effectiveFrom : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Contract.effectiveFrom>
  {
  }

  public abstract class discontinueAfter : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Contract.discontinueAfter>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.discountID>
  {
  }

  public class PromoDiscIDSelectorAttribute : PXCustomSelectorAttribute
  {
    protected BqlCommand _select;

    public PromoDiscIDSelectorAttribute(System.Type type)
      : base(type)
    {
      ((PXSelectorAttribute) this)._ViewName = "_SODiscount_LinePromo_";
    }

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      this._select = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select5<ARDiscount, InnerJoin<PX.Objects.AR.DiscountSequence, On<ARDiscount.discountID, Equal<PX.Objects.AR.DiscountSequence.discountID>>, LeftJoin<DiscountCustomer, On<DiscountCustomer.discountID, Equal<PX.Objects.AR.DiscountSequence.discountID>, And<DiscountCustomer.discountSequenceID, Equal<PX.Objects.AR.DiscountSequence.discountSequenceID>>>, LeftJoin<DiscountItem, On<DiscountItem.discountID, Equal<PX.Objects.AR.DiscountSequence.discountID>, And<DiscountItem.discountSequenceID, Equal<PX.Objects.AR.DiscountSequence.discountSequenceID>>>, LeftJoin<DiscountCustomerPriceClass, On<DiscountCustomerPriceClass.discountID, Equal<PX.Objects.AR.DiscountSequence.discountID>, And<DiscountCustomerPriceClass.discountSequenceID, Equal<PX.Objects.AR.DiscountSequence.discountSequenceID>>>, LeftJoin<DiscountInventoryPriceClass, On<DiscountInventoryPriceClass.discountID, Equal<PX.Objects.AR.DiscountSequence.discountID>, And<DiscountInventoryPriceClass.discountSequenceID, Equal<PX.Objects.AR.DiscountSequence.discountSequenceID>>>>>>>>, Where2<Where<ARDiscount.applicableTo, NotEqual<DiscountTarget.customer>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.customerAndInventory>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.customerAndInventoryPrice>, Or<DiscountCustomer.customerID, Equal<Current<Contract.customerID>>>>>>, And2<Where<ARDiscount.applicableTo, NotEqual<DiscountTarget.customerPrice>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.customerPriceAndInventory>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.customerPriceAndInventoryPrice>, Or<DiscountCustomerPriceClass.customerPriceClassID, Equal<Current<PX.Objects.CR.Location.cPriceClassID>>>>>>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.branch>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouse>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomer>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomerPrice>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventory>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventoryPrice>, And<PX.Objects.AR.DiscountSequence.isActive, Equal<True>, And<ARDiscount.type, Equal<DiscountType.LineDiscount>, And<Where<PX.Objects.AR.DiscountSequence.isPromotion, Equal<False>, Or<Current<Contract.startDate>, Between<PX.Objects.AR.DiscountSequence.startDate, PX.Objects.AR.DiscountSequence.endDate>>>>>>>>>>>>>>, Aggregate<GroupBy<ARDiscount.discountID>>>)
      });
    }

    public virtual IEnumerable GetRecords()
    {
      PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<Contract.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<Contract.locationID>>>>>.Config>.Select(this._Graph, Array.Empty<object>()));
      return (IEnumerable) this._Graph.TypedViews.GetView(this._select, true).SelectMultiBound(new object[1]
      {
        (object) location
      }, Array.Empty<object>());
    }

    public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
    {
      if (string.IsNullOrEmpty((string) e.NewValue))
        return;
      if (PXSelectBase<ARDiscount, PXSelect<ARDiscount, Where<ARDiscount.discountID, Equal<Required<ARDiscount.discountID>>, And<ARDiscount.type, Equal<DiscountType.LineDiscount>>>>.Config>.Select(sender.Graph, new object[1]
      {
        e.NewValue
      }).Count == 0)
        throw new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
        {
          e.NewValue
        });
    }
  }

  public abstract class detailedBilling : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.detailedBilling>
  {
    public const int Summary = 0;
    public const int Detail = 1;
  }

  public abstract class allowOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.allowOverride>
  {
  }

  public abstract class refreshOnRenewal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.refreshOnRenewal>
  {
  }

  public abstract class isContinuous : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.isContinuous>
  {
  }

  public abstract class defaultSalesAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultSalesAccountID>
  {
  }

  public abstract class defaultSalesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.defaultSalesSubID>
  {
  }

  public abstract class defaultExpenseAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultExpenseAccountID>
  {
  }

  public abstract class defaultExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultExpenseSubID>
  {
  }

  public abstract class defaultAccrualAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultAccrualAccountID>
  {
  }

  public abstract class defaultAccrualSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultAccrualSubID>
  {
  }

  public abstract class defaultOverbillingAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultOverbillingAccountID>
  {
  }

  public abstract class defaultOverbillingSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultOverbillingSubID>
  {
  }

  public abstract class defaultUnderbillingAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultUnderbillingAccountID>
  {
  }

  public abstract class defaultUnderbillingSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultUnderbillingSubID>
  {
  }

  public abstract class defaultBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.defaultBranchID>
  {
  }

  public abstract class quoteNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.quoteNbr>
  {
  }

  public abstract class restrictToEmployeeList : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Contract.restrictToEmployeeList>
  {
  }

  public abstract class restrictToResourceList : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Contract.restrictToResourceList>
  {
  }

  public abstract class approverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.approverID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.ownerID>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.salesPersonID>
  {
  }

  public abstract class scheduleStartsOn : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.scheduleStartsOn>
  {
    public const string SetupDate = "S";
    public const string ActivationDate = "A";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "S", "A" }, new string[2]
        {
          "Setup Date",
          "Activation Date"
        })
      {
      }
    }

    public class setupDate : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      Contract.scheduleStartsOn.setupDate>
    {
      public setupDate()
        : base("S")
      {
      }
    }

    public class activationDate : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      Contract.scheduleStartsOn.activationDate>
    {
      public activationDate()
        : base("A")
      {
      }
    }
  }

  public abstract class accountingMode : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.accountingMode>
  {
  }

  public abstract class prepaymentEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.prepaymentEnabled>
  {
  }

  public abstract class prepaymentDefCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.prepaymentDefCode>
  {
  }

  public abstract class limitsEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.limitsEnabled>
  {
  }

  public abstract class changeOrderWorkflow : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Contract.changeOrderWorkflow>
  {
  }

  public abstract class lockCommitments : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.lockCommitments>
  {
  }

  public abstract class budgetMetricsEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Contract.budgetMetricsEnabled>
  {
  }

  public abstract class certifiedJob : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.certifiedJob>
  {
  }

  public abstract class includeQtyInAIA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.includeQtyInAIA>
  {
  }

  public abstract class aIALevel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.aIALevel>
  {
    public const string Summary = "S";
    public const string Detail = "D";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "S", "D" }, new string[2]
        {
          "Summary",
          "Detail"
        })
      {
      }
    }

    public class summary : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.aIALevel.summary>
    {
      public summary()
        : base("S")
      {
      }
    }

    public class detail : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Contract.aIALevel.detail>
    {
      public detail()
        : base("D")
      {
      }
    }
  }

  public abstract class billingID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.billingID>
  {
  }

  public abstract class allocationID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.allocationID>
  {
  }

  public abstract class contractAccountGroup : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.contractAccountGroup>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.termsID>
  {
  }

  public abstract class lastChangeOrderNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.lastChangeOrderNumber>
  {
  }

  public abstract class lastProformaNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.lastProformaNumber>
  {
  }

  public abstract class retainagePct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Contract.retainagePct>
  {
  }

  /// <exclude />
  public abstract class retainageMaxPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Contract.retainageMaxPct>
  {
  }

  /// <exclude />
  public abstract class retainageMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.retainageMode>
  {
  }

  /// <exclude />
  public abstract class includeCO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.includeCO>
  {
  }

  /// <exclude />
  public abstract class steppedRetainage : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.steppedRetainage>
  {
  }

  public abstract class siteAddress : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.siteAddress>
  {
  }

  public abstract class pendingSetup : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Contract.pendingSetup>
  {
  }

  public abstract class pendingRecurring : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Contract.pendingRecurring>
  {
  }

  public abstract class pendingRenewal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Contract.pendingRenewal>
  {
  }

  public abstract class totalPending : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Contract.totalPending>
  {
  }

  public abstract class currentSetup : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Contract.currentSetup>
  {
  }

  public abstract class currentRecurring : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Contract.currentRecurring>
  {
  }

  public abstract class currentRenewal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Contract.currentRenewal>
  {
  }

  public abstract class totalsCalculated : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.totalsCalculated>
  {
  }

  public abstract class totalRecurring : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Contract.totalRecurring>
  {
  }

  public abstract class totalUsage : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Contract.totalUsage>
  {
  }

  public abstract class totalDue : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Contract.totalDue>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.rejected>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.isActive>
  {
  }

  public abstract class isCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.isCompleted>
  {
  }

  public abstract class isCancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.isCancelled>
  {
  }

  public abstract class isPendingUpdate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.isPendingUpdate>
  {
  }

  public abstract class autoAllocate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.autoAllocate>
  {
  }

  public abstract class isLastActionUndoable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Contract.isLastActionUndoable>
  {
  }

  public abstract class visibleInGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.visibleInGL>
  {
  }

  public abstract class visibleInAP : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.visibleInAP>
  {
  }

  public abstract class visibleInAR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.visibleInAR>
  {
  }

  public abstract class visibleInSO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.visibleInSO>
  {
  }

  public abstract class visibleInPO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.visibleInPO>
  {
  }

  public abstract class visibleInTA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.visibleInTA>
  {
  }

  public abstract class visibleInEA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.visibleInEA>
  {
  }

  public abstract class visibleInIN : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.visibleInIN>
  {
  }

  public abstract class visibleInCA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.visibleInCA>
  {
  }

  public abstract class visibleInCR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.visibleInCR>
  {
  }

  public abstract class nonProject : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.nonProject>
  {
  }

  public abstract class revID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.revID>
  {
  }

  public abstract class lastActiveRevID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.lastActiveRevID>
  {
  }

  public abstract class lineCtr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.lineCtr>
  {
  }

  public abstract class billingLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.billingLineCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Contract.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Contract.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Contract.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Contract.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Contract.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Contract.lastModifiedDateTime>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Contract.groupMask>
  {
  }

  public abstract class daysBeforeExpiration : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.daysBeforeExpiration>
  {
  }

  public abstract class days : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.days>
  {
  }

  public abstract class min : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.min>
  {
  }

  public abstract class serviceActivate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.serviceActivate>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.classID>
  {
  }

  public abstract class attributes : BqlType<IBqlAttributes, string[]>.Field<Contract.attributes>
  {
  }

  public abstract class dropshipExpenseAccountSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.dropshipExpenseAccountSource>
  {
  }

  public abstract class dropshipExpenseSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.dropshipExpenseSubMask>
  {
  }

  public abstract class dropshipReceiptProcessing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.dropshipReceiptProcessing>
  {
  }

  public abstract class dropshipExpenseRecording : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.dropshipExpenseRecording>
  {
  }

  public abstract class costTaxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.costTaxZoneID>
  {
  }

  public abstract class revenueTaxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contract.revenueTaxZoneID>
  {
  }
}
