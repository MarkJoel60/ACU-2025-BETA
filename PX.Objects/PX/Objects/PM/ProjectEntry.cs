// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.LicensePolicy;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.CRCreateActions;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class ProjectEntry : 
  ProjectEntryBase<
  #nullable disable
  ProjectEntry>,
  PXImportAttribute.IPXPrepareItems,
  IGraphWithInitialization
{
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PX.Objects.CT.Contract.lastChangeOrderNumber)})]
  public PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMProject.contractID>>>> ProjectProperties;
  public PXSetup<PX.Objects.AR.Customer>.Where<BqlOperand<
  #nullable enable
  PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProject.customerID, IBqlInt>.AsOptional>> customer;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public 
  #nullable disable
  PXSelect<BAccountR> dummyAccountR;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PX.Objects.AP.Vendor> dummyVendor;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.CR.Standalone.EPEmployee> approver;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (ContractBillingSchedule.lastDate), typeof (ContractBillingSchedule.nextDate)})]
  public PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Current<PMProject.contractID>>>> Billing;
  public PXSelectJoin<EPEmployeeContract, LeftJoin<PX.Objects.CR.Standalone.EPEmployee, On<PX.Objects.CR.Standalone.EPEmployee.bAccountID, Equal<EPEmployeeContract.employeeID>>>, Where<EPEmployeeContract.contractID, Equal<Current<PMProject.contractID>>>> EmployeeContract;
  public PXSelectJoin<EPContractRate, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<EPContractRate.labourItemID>>, LeftJoin<EPEarningType, On<EPEarningType.typeCD, Equal<EPContractRate.earningType>>>>, Where<EPContractRate.employeeID, Equal<Optional<EPEmployeeContract.employeeID>>, And<EPContractRate.contractID, Equal<Optional<EPEmployeeContract.contractID>>>>, OrderBy<Asc<EPContractRate.contractID>>> ContractRates;
  [PXImport(typeof (PMProject))]
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<EPEquipmentRate, LeftJoin<EPEquipment, On<EPEquipmentRate.equipmentID, Equal<EPEquipment.equipmentID>>>, Where<EPEquipmentRate.projectID, Equal<Current<PMProject.contractID>>>> EquipmentRates;
  public PXSelect<PMAccountTask, Where<PMAccountTask.projectID, Equal<Current<PMProject.contractID>>>> Accounts;
  public EPDependNoteList<NotificationSource, NotificationSource.refNoteID, PMProject> NotificationSources;
  public PXSelect<NotificationRecipient, Where<NotificationRecipient.sourceID, Equal<Optional<NotificationSource.sourceID>>>> NotificationRecipients;
  public PXSelect<PMProjectRevenueTotal, Where<PMProjectRevenueTotal.projectID, Equal<Current<PMProject.contractID>>>> ProjectRevenueTotals;
  public PXSelect<PMRetainageStep, Where<PMRetainageStep.projectID, Equal<Current<PMProject.contractID>>>, OrderBy<Asc<PMRetainageStep.thresholdPct>>> RetainageSteps;
  public ChangeProjectID ChangeID;
  public PXSelectJoin<INCostCenter, InnerJoin<PMTask, On<PMTask.projectID, Equal<INCostCenter.projectID>, And<PMTask.taskID, Equal<INCostCenter.taskID>>>>, Where<INCostCenter.costLayerType, Equal<CostLayerType.project>, And<INCostCenter.projectID, Equal<Current<PMProject.contractID>>>>> CostCenters;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.projectID, Equal<Current<PMProject.contractID>>>> dummyInvoice;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMProforma, Where<PMProforma.projectID, Equal<Current<PMProject.contractID>>>> dummyProforma;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMCostCode> dummyCostCode;
  public PXFilter<ProjectEntry.TemplateSettingsFilter> TemplateSettings;
  [PXHidden]
  public PXSelect<PMRecurringItem, Where<PMRecurringItem.projectID, Equal<Current<PMTask.projectID>>>> BillingItems;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXSetup<PX.Objects.PO.POSetup> POSetup;
  public CMSetupSelect CMSetup;
  public PXSelectGroupBy<PMTaskTotal, Where<PMTaskTotal.projectID, Equal<Current<PMProject.contractID>>>, Aggregate<Sum<PMTaskTotal.asset, Sum<PMTaskTotal.liability, Sum<PMTaskTotal.income, Sum<PMTaskTotal.expense, Sum<PMTaskTotal.curyAsset, Sum<PMTaskTotal.curyLiability, Sum<PMTaskTotal.curyIncome, Sum<PMTaskTotal.curyExpense>>>>>>>>>> TaskTotals;
  [PXViewName("Project Answers")]
  public CRAttributeList<PMProject> Answers;
  [PXHidden]
  public ProjectTaskAttributeList TaskAnswers;
  [PXViewName("PM Billing Address")]
  public FbqlSelect<SelectFromBase<PMBillingAddress, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PX.Objects.PM.PMAddress.addressID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProject.billAddressID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PMBillingAddress>.View Billing_Address;
  [PXHidden]
  public FbqlSelect<SelectFromBase<PX.Objects.PM.PMAddress, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.PM.PMAddress>.View PMAddress;
  [PXViewName("PM Billing Contact")]
  public FbqlSelect<SelectFromBase<PMBillingContact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PX.Objects.PM.PMContact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProject.billContactID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PMBillingContact>.View Billing_Contact;
  [PXHidden]
  public FbqlSelect<SelectFromBase<PX.Objects.PM.PMContact, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.PM.PMContact>.View PMContact;
  [PXViewName("Site Address")]
  public PXSelect<PMSiteAddress, Where<PMSiteAddress.addressID, Equal<Current<PMProject.siteAddressID>>>> Site_Address;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<PMProject.curyInfoID>>>> CuryInfo;
  [PXHidden]
  public PXSelect<PMForecastHistoryAccum> ForecastHistory;
  [PXCopyPasteHiddenView]
  public PXSelect<PMBudgetProduction, Where<PMBudgetProduction.projectID, Equal<Current<PMProject.contractID>>>> BudgetProduction;
  [PXCopyPasteHiddenView]
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<ProjectEntry.SelectedTask, LeftJoin<PMProject, On<ProjectEntry.SelectedTask.projectID, Equal<PMProject.contractID>>>, Where<PMTask.autoIncludeInPrj, NotEqual<True>, And<ProjectEntry.SelectedTask.projectID, Equal<Current<PMProject.templateID>>, Or<PMProject.nonProject, Equal<True>>>>> TasksForAddition;
  [PXCopyPasteHiddenView]
  [PXViewName("Approval")]
  public EPApprovalAutomation<PMProject, PMProject.approved, PMProject.rejected, PMProject.hold, PMSetupProjectApproval> Approval;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PMTimeActivity> ActivityDummy;
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  [PXViewName("Purchase Order")]
  public FbqlSelect<SelectFromBase<PX.Objects.PO.POOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ProjectEntry.PMPOLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.PO.POOrder.orderNbr, 
  #nullable disable
  Equal<ProjectEntry.PMPOLine.orderNbr>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.PO.POOrder.orderType, IBqlString>.IsEqual<
  #nullable disable
  ProjectEntry.PMPOLine.orderType>>>>>.Where<BqlOperand<
  #nullable enable
  ProjectEntry.PMPOLine.projectID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>.Aggregate<
  #nullable disable
  To<GroupBy<PX.Objects.PO.POOrder.orderNbr>, GroupBy<PX.Objects.PO.POOrder.orderType>, GroupBy<ProjectEntry.PMPOLine.projectID>, Sum<ProjectEntry.PMPOLine.orderQty>, Sum<ProjectEntry.PMPOLine.curyLineCost>>>, PX.Objects.PO.POOrder>.View PurchaseOrders;
  public FbqlSelect<SelectFromBase<PMProjectBudgetHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMProjectBudgetHistory.projectID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PMProjectBudgetHistory>.View ProjectBudgetHistory;
  public FbqlSelect<SelectFromBase<PMProjectBudgetHistoryAccum, TypeArrayOf<IFbqlJoin>.Empty>, PMProjectBudgetHistoryAccum>.View ProjectBudgetHistoryAccum;
  [PXImport(typeof (PMProject))]
  [PXViewName("Union Locals")]
  public PXSelect<PMProjectUnion, Where<PMProjectUnion.projectID, Equal<Current<PMProject.contractID>>>> Unions;
  [PXCopyPasteHiddenView]
  public PXSelect<PMQuote> Quote;
  [PXCopyPasteHiddenView]
  public PXSelect<CROpportunityProducts> QuoteDetails;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PMForecastDetail> ForecastDetails;
  public PXFilter<ProjectEntry.CopyDialogInfo> CopyDialog;
  public PXFilter<ProjectEntry.LoadFromTemplateInfo> LoadFromTemplateDialog;
  public PXSelectJoin<PMProjectContact, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PMProjectContact.contactID>>>, Where<PMProjectContact.projectID, Equal<Current<PMProject.contractID>>>, OrderBy<Asc<PX.Objects.CR.Contact.displayName>>> ProjectContacts;
  private bool _isLoadFromTemplate;
  public PXAction<PMProject> validateAddresses;
  public PXAction<PMProject> bill;
  public PXAction<PMProject> laborCostRates;
  public PXAction<PMProject> forecast;
  public PXAction<PMProject> projectBalanceReport;
  public PXAction<PMProject> currencyRates;
  public PXAction<PMProject> setCurrencyRates;
  public PXAction<PMProjectContact> viewContact;
  public PXAction<PMProject> viewBalanceTransactions;
  public PXAction<PMProject> viewCommitments;
  public PXAction<PMProject> viewInvoice;
  public PXAction<PMProject> openProjectOverview;
  public PXAction<PMProject> viewOrigDocument;
  public PXAction<PMProject> viewProforma;
  public PXAction<PMProject> viewPurchaseOrder;
  public PXAction<PMProject> viewAddCommonTask;
  public PXAction<PMProject> addTasks;
  public PXAction<PMProject> activateTasks;
  public PXAction<PMProject> completeTasks;
  public PXAction<PMProject> createTemplate;
  public PXAction<PMProject> runAllocation;
  public PXAction<PMProject> validateBalance;
  public PXAction<PMProject> autoBudget;
  public PXAction<PMProject> hold;
  public PXAction<PMProject> activate;
  public PXAction<PMProject> lockBudget;
  public PXAction<PMProject> unlockBudget;
  public PXAction<PMProject> lockCommitments;
  public PXAction<PMProject> unlockCommitments;
  public PXAction<PMProject> updateRetainage;
  public PXAction<PMProject> viewReleaseRetainage;
  public PXAction<PMProject> copyProject;
  public PXAction<PMProject> createPurchaseOrder;
  public PXAction<PMProject> createDropShipOrder;
  public PXAction<PMProject> ViewAddressOnMap;
  public PXAction<PMProject> complete;
  public PXAction<PMProject> close;
  public PXAction<PMProject> addNewProjectTemplate;
  private IFinPeriodRepository finPeriodsRepo;
  private Dictionary<int, List<PMCostBudget>> costBudgetsByRevenueTaskID = new Dictionary<int, List<PMCostBudget>>();
  private Dictionary<int, List<PMProjectBudgetHistoryAccum>> budgetHistoryByTaskID = new Dictionary<int, List<PMProjectBudgetHistoryAccum>>();
  private Dictionary<int?, int?> persistedTask = new Dictionary<int?, int?>();
  private Dictionary<int?, int?> persistedTaskBudgetHistory = new Dictionary<int?, int?>();
  private int? negativeKey;
  public bool SuppressTemplateIDUpdated;
  public PXAction<PMProject> loadFromTemplate;
  private bool _BlockQtyToInvoiceCalculate;
  private int? negativeProjectKey;
  public bool _IsRecalculatingRevenueBudgetScope;
  private string _lastBadTaskCD;
  private string _lastBadInventoryCD;
  private string _lastBadCostCodeCD;
  private string _lastBadAccountGroupCD;

  public IProjectGroupMaskHelper ProjectGroupMaskHelper
  {
    get
    {
      return (IProjectGroupMaskHelper) ((PXGraph) this).GetExtension<ProjectEntry.ProjectGroupMaskHelperExt>();
    }
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Budget Currency Rate", IsReadOnly = true, FieldClass = "ProjectMultiCurrency")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.curyID> e)
  {
  }

  [PXMergeAttributes]
  [ProjectCDRestrictor]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.contractCD> e)
  {
  }

  [PXMergeAttributes]
  [PXDimensionSelector("TMPROJECT", typeof (Search2<PMProjectSearch.contractID, LeftJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<PMProjectSearch.contractID>>>, Where<PMProjectSearch.baseType, Equal<CTPRType.projectTemplate>, And<PMProjectSearch.isActive, Equal<True>>>>), typeof (PMProjectSearch.contractCD), new System.Type[] {typeof (PMProjectSearch.contractCD), typeof (PMProjectSearch.description), typeof (PMProjectSearch.budgetLevel), typeof (PMProjectSearch.billingID), typeof (ContractBillingSchedule.type), typeof (PMProjectSearch.ownerID)}, DescriptionField = typeof (PMProjectSearch.description))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.templateID> e)
  {
  }

  [Branch(null, null, true, true, true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.defaultBranchID> _)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.projectID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault("CU")]
  protected virtual void _(PX.Data.Events.CacheAttached<BAccountR.type> e)
  {
  }

  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMTaskTotal.income))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Income", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTaskTotal.curyIncome> e)
  {
  }

  [PXBaseCury]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTaskTotal.income> e)
  {
  }

  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMTaskTotal.expense))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Expenses", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTaskTotal.curyExpense> e)
  {
  }

  [PXBaseCury]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTaskTotal.expense> e)
  {
  }

  [PXBaseCury]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTaskTotal.asset> e)
  {
  }

  [PXBaseCury]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTaskTotal.liability> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Equipment ID")]
  [PXSelector(typeof (EPEquipment.equipmentID), DescriptionField = typeof (EPEquipment.description), SubstituteKey = typeof (EPEquipment.equipmentCD))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPEquipmentRate.equipmentID> e)
  {
  }

  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<EPEquipmentRate.projectID>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXDBInt(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<EPEquipmentRate.projectID> e)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [BillingType.ListForProject]
  [PXUIField(DisplayName = "Billing Period")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ContractBillingSchedule.type> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "AR Doc. Status")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.status> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "AR Doc. Description")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.docDesc> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Original Document", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.origRefNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "AR Doc. Total Amount", FieldClass = "Retainage")]
  [PXFormula(typeof (Add<PX.Objects.AR.ARRegister.curyOrigDocAmt, PX.Objects.AR.ARRegister.curyRetainageTotal>))]
  [ProjectEntry.InvoiceAmount(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARRegister.origDocAmtWithRetainageTotal))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.curyOrigDocAmtWithRetainageTotal> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "AR Doc. Orig. Amount")]
  [ProjectEntry.DBInvoiceAmount(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.origDocAmt))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.curyOrigDocAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "AR Doc. Date")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.docDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Open AR Balance")]
  [ProjectEntry.DBInvoiceAmount(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.docBal), BaseCalc = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.curyDocBal> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "AR Doc. Currency", FieldClass = "ProjectMultiCurrency")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.curyID> e)
  {
  }

  [PXMergeAttributes]
  [ProjectEntry.DBInvoiceAmount(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARRegister.retainageTotal))]
  [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.curyRetainageTotal> e)
  {
  }

  [PXMergeAttributes]
  [ProjectEntry.DBInvoiceAmount(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARRegister.retainageUnreleasedAmt))]
  [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.curyRetainageUnreleasedAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Invoice Total")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProforma.docTotal> e)
  {
  }

  [PXDBDate]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProforma.invoiceDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Pro Forma Currency", FieldClass = "ProjectMultiCurrency")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProforma.curyID> e)
  {
  }

  [PXMergeAttributes]
  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXEPEmployeeSelector]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPEmployeeContract.employeeID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<EPEmployeeContract.contractID>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPEmployeeContract.contractID> e)
  {
  }

  [PXDBIdentity]
  protected virtual void _(PX.Data.Events.CacheAttached<EPContractRate.recordID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PMProject.contractID))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPContractRate.contractID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<EPEmployeeContract, Where<EPEmployeeContract.employeeID, Equal<Current<EPContractRate.employeeID>>, And<EPEmployeeContract.contractID, Equal<Current<EPContractRate.contractID>>>>>))]
  [PXDefault(typeof (EPEmployeeContract.employeeID))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPContractRate.employeeID> e)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXRestrictor(typeof (Where<EPEarningType.isActive, Equal<True>>), "The earning type {0} selected on the Time & Expenses Preferences (EP101000) form is inactive. Inactive earning types are not available for data entry in new activities and time entries.", new System.Type[] {typeof (EPEarningType.typeCD)})]
  [PXSelector(typeof (EPEarningType.typeCD))]
  [PXUIField(DisplayName = "Earning Type")]
  protected virtual void _(PX.Data.Events.CacheAttached<EPContractRate.earningType> e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXSelector(typeof (Search<NotificationSetup.setupID, Where<NotificationSetup.sourceCD, Equal<PMNotificationSource.project>>>))]
  [PXUIField(DisplayName = "Mailing ID")]
  [PXUIEnabled(typeof (Where<BqlOperand<NotificationSource.setupID, IBqlGuid>.IsNull>))]
  protected virtual void _(PX.Data.Events.CacheAttached<NotificationSource.setupID> e)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<NotificationSource.classID> e)
  {
  }

  [PXMergeAttributes]
  [PXCheckUnique(new System.Type[] {typeof (NotificationSource.setupID)}, IgnoreNulls = false, Where = typeof (Where<NotificationSource.refNoteID, Equal<Current<NotificationSource.refNoteID>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationSource.nBranchID> e)
  {
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Report")]
  [PXDefault(typeof (Search<NotificationSetup.reportID, Where<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>))]
  [PXSelector(typeof (Search<SiteMap.screenID, Where2<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>, And<Where<SiteMap.screenID, Like<PXModule.pm_>>>>, OrderBy<Asc<SiteMap.screenID>>>), new System.Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXFormula(typeof (Default<NotificationSource.setupID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationSource.reportID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationSource.overrideSource> e)
  {
  }

  [PXDBInt]
  [PXDBDefault(typeof (NotificationSource.sourceID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationRecipient.sourceID> e)
  {
  }

  [PXDBString(10)]
  [PXDefault]
  [NotificationContactType.ProjectList]
  [PXUIField(DisplayName = "Contact Type")]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationRecipient.contactID)}, Where = typeof (Where<NotificationRecipient.sourceID, Equal<Current<NotificationRecipient.sourceID>>, And<NotificationRecipient.refNoteID, Equal<Current<PMProject.noteID>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationRecipient.contactType> e)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationRecipient.contactType), typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>, LeftJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.EP.EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>>, Where2<Where<Current<NotificationRecipient.contactType>, Equal<NotificationContactType.employee>, And<PX.Objects.EP.EPEmployee.acctCD, IsNotNull>>, Or<Where<Current<NotificationRecipient.contactType>, Equal<NotificationContactType.contact>, And<BAccountR.bAccountID, Equal<Current<PMProject.customerID>>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>>>>>>>), DirtyRead = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationRecipient.contactID> e)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationRecipient.classID> e)
  {
  }

  [PXString]
  [PXUIField(DisplayName = "Email", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationRecipient.email> e)
  {
  }

  [PXDefault(typeof (PMProject.startDate))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.docDate> e)
  {
  }

  [PXDefault(typeof (PMProject.customerID))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.bAccountID> e)
  {
  }

  [PXDefault(typeof (PMProject.ownerID))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.documentOwnerID> e)
  {
  }

  [PXDefault(typeof (PMProject.description))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.descr> e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.quoteID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Curr. Rate Type ID", IsReadOnly = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXDBInt(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMRetainageStep.projectID> e)
  {
  }

  [PXMergeAttributes]
  [PXDimensionSelector("PROTASK", typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMProject.contractID>>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>>), typeof (PMTask.taskCD), new System.Type[] {typeof (PMTask.description), typeof (PMTask.status), typeof (PMTask.taskCD), typeof (PMTask.type)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<RevenueBudgetFilter.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXDimensionSelector("PROTASK", typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMProject.contractID>>, And<PMTask.type, NotEqual<ProjectTaskType.revenue>>>>), typeof (PMTask.taskCD), new System.Type[] {typeof (PMTask.description), typeof (PMTask.status), typeof (PMTask.taskCD), typeof (PMTask.type)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CostBudgetFilter.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<PMTask, Where<PMTask.taskID, Equal<Current<PMForecastDetail.projectTaskID>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMForecastDetail.projectTaskID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMProjectUnion.projectID>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProjectUnion.projectID> e)
  {
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PMProject.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void PMRevenueBudget_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.cost>>), "Task Type is not valid", new System.Type[] {typeof (PMTask.type)})]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMRevenueBudget.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMRevenueBudget.projectTaskID> e)
  {
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PMProject.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void PMCostBudget_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>), "Task Type is not valid", new System.Type[] {typeof (PMTask.type)})]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMCostBudget.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.revenue>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCostBudget.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Coalesce<SearchFor<PX.Objects.CR.Address.countryID>.In<SelectFromBase<PX.Objects.CR.Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.Customer>.On<BqlOperand<PX.Objects.AR.Customer.defBillAddressID, IBqlInt>.IsEqual<PX.Objects.CR.Address.addressID>>>>.Where<BqlOperand<PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<BqlField<PMProject.customerID, IBqlInt>.FromCurrent>>>, SearchFor<CustomerClass.countryID>.In<SelectFromBase<CustomerClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CustomerClass.customerClassID, IBqlString>.IsEqual<BqlField<CustomerClass.customerClassID, IBqlString>.FromCurrent>>>, SearchFor<PX.Objects.GL.Branch.countryID>.In<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<BqlField<AccessInfo.branchID, IBqlInt>.FromCurrent>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMSiteAddress.countryID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Migrated Pro Forma Invoice")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMProforma.isMigratedRecord> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(2)]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.curyOrderTotal> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXDBInt(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProjectContact.projectID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Phone 2", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.phone2> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Phone 3", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.phone3> e)
  {
  }

  [InjectDependency]
  public IUnitRateService RateService { get; set; }

  public virtual int CompareBillingRecords(
    PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice> x,
    PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice> y)
  {
    DateTime? nullable1 = PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(x).DocDate ?? PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(x).InvoiceDate;
    DateTime? nullable2 = PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(y).DocDate ?? PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(y).InvoiceDate;
    if (!nullable1.HasValue && !nullable2.HasValue)
      return 0;
    if (!nullable1.HasValue)
      return -1;
    if (!nullable2.HasValue)
      return 1;
    DateTime? nullable3 = nullable1;
    DateTime? nullable4 = nullable2;
    if ((nullable3.HasValue == nullable4.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      return nullable1.Value.CompareTo(nullable2.Value);
    int? recordId;
    int maxValue1;
    if (PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(x).RecordID.HasValue)
    {
      recordId = PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(x).RecordID;
      int num = 0;
      if (!(recordId.GetValueOrDefault() < num & recordId.HasValue))
      {
        recordId = PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(x).RecordID;
        maxValue1 = recordId.Value;
        goto label_11;
      }
    }
    maxValue1 = int.MaxValue;
label_11:
    int num1 = maxValue1;
    recordId = PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(y).RecordID;
    int maxValue2;
    if (recordId.HasValue)
    {
      recordId = PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(y).RecordID;
      int num2 = 0;
      if (!(recordId.GetValueOrDefault() < num2 & recordId.HasValue))
      {
        recordId = PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(y).RecordID;
        maxValue2 = recordId.Value;
        goto label_15;
      }
    }
    maxValue2 = int.MaxValue;
label_15:
    int num3 = maxValue2;
    if (num1 != num3)
      return num1.CompareTo(num3);
    return num1 == int.MaxValue ? PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(x).RefNbr.CompareTo(PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(y).RefNbr) : 0;
  }

  protected IEnumerable tasksForAddition()
  {
    List<ProjectEntry.SelectedTask> list1 = GraphHelper.RowCast<ProjectEntry.SelectedTask>((IEnumerable) PXSelectBase<ProjectEntry.SelectedTask, PXViewOf<ProjectEntry.SelectedTask>.BasedOn<SelectFromBase<ProjectEntry.SelectedTask, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<ProjectEntry.SelectedTask.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<True>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<ProjectEntry.SelectedTask>();
    if (((PXSelectBase<PMProject>) this.Project).Current.TemplateID.HasValue)
    {
      List<ProjectEntry.SelectedTask> list2 = GraphHelper.RowCast<ProjectEntry.SelectedTask>((IEnumerable) PXSelectBase<ProjectEntry.SelectedTask, PXViewOf<ProjectEntry.SelectedTask>.BasedOn<SelectFromBase<ProjectEntry.SelectedTask, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<ProjectEntry.SelectedTask.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.contractID, Equal<BqlField<PMProject.templateID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PMTask.autoIncludeInPrj, IBqlBool>.IsNotEqual<True>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<ProjectEntry.SelectedTask>();
      if (list2 != null)
        list1.AddRange((IEnumerable<ProjectEntry.SelectedTask>) list2);
    }
    HashSet<string> existingTasksCDs = GraphHelper.RowCast<PMTask>((IEnumerable) ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>())).Where<PMTask>((Func<PMTask, bool>) (task => !string.IsNullOrWhiteSpace(task.TaskCD))).Select<PMTask, string>((Func<PMTask, string>) (task => task.TaskCD.Trim().ToUpperInvariant())).Distinct<string>().ToHashSet<string>();
    return (IEnumerable) GraphHelper.RowCast<ProjectEntry.SelectedTask>((IEnumerable) list1).Where<ProjectEntry.SelectedTask>((Func<ProjectEntry.SelectedTask, bool>) (task => !existingTasksCDs.Contains(task.TaskCD.Trim().ToUpperInvariant())));
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    ProjectEntry aGraph = this;
    foreach (PMProject pmProject in adapter.Get<PMProject>())
    {
      if (pmProject != null)
      {
        PX.Objects.PM.PMAddress aAddress1 = (PX.Objects.PM.PMAddress) PXResultset<PMBillingAddress>.op_Implicit(((PXSelectBase<PMBillingAddress>) aGraph.Billing_Address).Select(Array.Empty<object>()));
        bool? nullable;
        if (aAddress1 != null)
        {
          nullable = aAddress1.IsDefaultAddress;
          bool flag1 = false;
          if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
          {
            nullable = aAddress1.IsValidated;
            bool flag2 = false;
            if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
              PXAddressValidator.Validate<PX.Objects.PM.PMAddress>((PXGraph) aGraph, aAddress1, true, true);
          }
        }
        PMSiteAddress aAddress2 = PXResultset<PMSiteAddress>.op_Implicit(((PXSelectBase<PMSiteAddress>) aGraph.Site_Address).Select(Array.Empty<object>()));
        if (aAddress2 != null)
        {
          nullable = aAddress2.IsDefaultAddress;
          bool flag3 = false;
          if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          {
            nullable = aAddress2.IsValidated;
            bool flag4 = false;
            if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
              PXAddressValidator.Validate<PMSiteAddress>((PXGraph) aGraph, aAddress2, true, true);
          }
        }
      }
      yield return (object) pmProject;
    }
  }

  [PXUIField]
  [PXProcessButton(Tooltip = "Runs billing for the Next Billing Date")]
  public virtual IEnumerable Bill(PXAdapter adapter)
  {
    if (this.CanBeBilled())
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ProjectEntry.\u003C\u003Ec__DisplayClass139_0 displayClass1390 = new ProjectEntry.\u003C\u003Ec__DisplayClass139_0();
      ((PXAction) this.Save).Press();
      // ISSUE: reference to a compiler-generated field
      displayClass1390.projectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1390, __methodptr(\u003CBill\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable LaborCostRates(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      LaborCostRateMaint instance = PXGraph.CreateInstance<LaborCostRateMaint>();
      ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
      ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) instance.Filter).Select(Array.Empty<object>());
      throw new PXRedirectRequiredException((PXGraph) instance, "Labor Cost Rates");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Forecast(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      ForecastMaint instance = PXGraph.CreateInstance<ForecastMaint>();
      PMForecast pmForecast = PXResultset<PMForecast>.op_Implicit(((PXSelectBase<PMForecast>) new PXSelect<PMForecast, Where<PMForecast.projectID, Equal<Current<PMProject.contractID>>>, OrderBy<Desc<PMForecast.lastModifiedDateTime>>>((PXGraph) this)).Select(Array.Empty<object>()));
      if (pmForecast != null)
      {
        ((PXSelectBase<PMForecast>) instance.Revisions).Current = pmForecast;
      }
      else
      {
        ((PXSelectBase<PMForecast>) instance.Revisions).Insert();
        ((PXSelectBase<PMForecast>) instance.Revisions).Current.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
        ((PXSelectBase) instance.Revisions).Cache.IsDirty = false;
      }
      throw new PXRedirectRequiredException((PXGraph) instance, "Project Budget Forecast");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable ProjectBalanceReport(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current.ContractCD
      }, "PM621000", "PM621000", (CurrentLocalization) null);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable CurrencyRates(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["StartDate"] = ((PXSelectBase<PMProject>) this.Project).Current.StartDate.ToString();
      DateTime? expireDate = ((PXSelectBase<PMProject>) this.Project).Current.ExpireDate;
      if (expireDate.HasValue)
      {
        Dictionary<string, string> dictionary = parameters;
        expireDate = ((PXSelectBase<PMProject>) this.Project).Current.ExpireDate;
        string str = expireDate.ToString();
        dictionary["EndDate"] = str;
      }
      parameters["RateType"] = ((PXSelectBase<PMProject>) this.Project).Current.RateTypeID ?? ((PXSelectBase<PX.Objects.CM.CMSetup>) this.CMSetup).Current.PMRateTypeDflt;
      ProjectAccountingService.NavigateToReport("CM650500", parameters);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable SetCurrencyRates(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      CuryRateMaint instance = PXGraph.CreateInstance<CuryRateMaint>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<PX.Objects.CM.CuryRateFilter>) instance.Filter).Current.ToCurrency = ((PXSelectBase<PMProject>) this.Project).Current.CuryIDCopy;
      throw new PXRedirectRequiredException((PXGraph) instance, true, "Set Rates");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public IEnumerable ViewContact(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProjectContact>) this.ProjectContacts).Current != null)
    {
      PX.Objects.CR.Contact contact = PX.Objects.CR.Contact.PK.Find((PXGraph) this, ((PXSelectBase<PMProjectContact>) this.ProjectContacts).Current.ContactID);
      if (contact != null)
      {
        if (contact.ContactType == "EP")
        {
          ProjectAccountingService.NavigateToCustomerScreen((PX.Objects.CR.BAccount) PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Required<PX.Objects.EP.EPEmployee.userID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) contact.UserID
          })));
        }
        else
        {
          ContactMaint instance = PXGraph.CreateInstance<ContactMaint>();
          ((PXGraph) instance).Clear();
          ((PXSelectBase<PX.Objects.CR.Contact>) instance.Contact).Current = contact;
          ProjectAccountingService.NavigateToScreen((PXGraph) instance, "Contact Maintenance");
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public IEnumerable ViewBalanceTransactions(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProjectBalanceRecord>) this.BalanceRecords).Current != null)
    {
      int? recordId = ((PXSelectBase<PMProjectBalanceRecord>) this.BalanceRecords).Current.RecordID;
      int num = 0;
      if (recordId.GetValueOrDefault() > num & recordId.HasValue)
      {
        TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
        ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
        ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.AccountGroupID = ((PXSelectBase<PMProjectBalanceRecord>) this.BalanceRecords).Current.RecordID;
        ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.IncludeUnreleased = new bool?(false);
        ProjectAccountingService.NavigateToScreen((PXGraph) instance);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public IEnumerable ViewCommitments(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProjectBalanceRecord>) this.BalanceRecords).Current != null)
    {
      int? recordId = ((PXSelectBase<PMProjectBalanceRecord>) this.BalanceRecords).Current.RecordID;
      int num = 0;
      if (recordId.GetValueOrDefault() > num & recordId.HasValue)
      {
        CommitmentInquiry instance = PXGraph.CreateInstance<CommitmentInquiry>();
        ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
        ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.AccountGroupID = ((PXSelectBase<PMProjectBalanceRecord>) this.BalanceRecords).Current.RecordID;
        ProjectAccountingService.NavigateToScreen((PXGraph) instance);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Inquiry")]
  public virtual IEnumerable ViewInvoice(PXAdapter adapter)
  {
    if (((PXSelectBase<PMBillingRecord>) this.Invoices).Current != null)
      ProjectAccountingService.NavigateToArInvoiceScreen(((PXSelectBase<PMBillingRecord>) this.Invoices).Current.ARDocType, ((PXSelectBase<PMBillingRecord>) this.Invoices).Current.ARRefNbr);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenProjectOverview(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToProjectOverviewScreen(((PXSelectBase<PMProject>) this.Project).Current.ContractID, "Project", (PXBaseRedirectException.WindowMode) 1);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Inquiry")]
  public virtual IEnumerable ViewOrigDocument(PXAdapter adapter)
  {
    PX.Objects.AR.ARInvoice arInvoice1 = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Required<PMBillingRecord.aRDocType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PMBillingRecord.aRRefNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ((PXSelectBase<PMBillingRecord>) this.Invoices).Current.ARDocType,
      (object) ((PXSelectBase<PMBillingRecord>) this.Invoices).Current.ARRefNbr
    }));
    PX.Objects.AR.ARInvoice arInvoice2 = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PMBillingRecord.aRRefNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) arInvoice1.OrigRefNbr
    }));
    if (((PXSelectBase<PMBillingRecord>) this.Invoices).Current != null && !string.IsNullOrEmpty(arInvoice1.OrigRefNbr))
      ProjectAccountingService.NavigateToArInvoiceScreen(arInvoice2.DocType, arInvoice1.OrigRefNbr);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Inquiry")]
  public virtual IEnumerable ViewProforma(PXAdapter adapter)
  {
    if (((PXSelectBase<PMBillingRecord>) this.Invoices).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PMBillingRecord>) this.Invoices).Current.ProformaRefNbr))
      ProjectAccountingService.NavigateToProformaScreen(((PXSelectBase<PMBillingRecord>) this.Invoices).Current.ProformaRefNbr);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Inquiry")]
  public virtual IEnumerable ViewPurchaseOrder(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.PurchaseOrders).Current != null)
    {
      POOrderEntry poEntryGraph = this.CreatePOEntryGraph(((PXSelectBase<PX.Objects.PO.POOrder>) this.PurchaseOrders).Current);
      ((PXSelectBase<PX.Objects.PO.POOrder>) poEntryGraph.Document).Current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.PurchaseOrders).Current;
      ProjectAccountingService.NavigateToScreen((PXGraph) poEntryGraph, "View Purchase Order");
    }
    return adapter.Get();
  }

  public virtual POOrderEntry CreatePOEntryGraph(PX.Objects.PO.POOrder order)
  {
    return PXGraph.CreateInstance<POOrderEntry>();
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewAddCommonTask()
  {
    ((PXSelectBase<ProjectEntry.SelectedTask>) this.TasksForAddition).AskExt();
  }

  [PXUIField]
  [PXButton(ImageKey = "Process")]
  public virtual IEnumerable AddTasks(PXAdapter adapter)
  {
    foreach (ProjectEntry.SelectedTask task in ((PXSelectBase) this.TasksForAddition).Cache.Updated)
    {
      if (task.Selected.GetValueOrDefault())
      {
        this.CopyTask((PMTask) task, ((PXSelectBase<PMProject>) this.ProjectProperties).Current.ContractID.Value, ProjectEntry.DefaultFromTemplateSettings.Default);
        task.Selected = new bool?(false);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXDeleteButton(ConfirmationMessage = "The current {0} record will be deleted.")]
  public virtual IEnumerable delete(PXAdapter adapter)
  {
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003Cdelete\u003Eb__171_0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ActivateTasks(PXAdapter adapter)
  {
    bool flag = false;
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      if (pmTask.Status == "D" || pmTask.Status == "F")
      {
        try
        {
          pmTask.Status = "A";
          ((PXSelectBase<PMTask>) this.Tasks).Update(pmTask);
        }
        catch (PXSetPropertyException ex)
        {
          flag = true;
          ((PXSelectBase) this.Tasks).Cache.RaiseExceptionHandling<PMTask.status>((object) pmTask, (object) pmTask.Status, (Exception) ex);
        }
      }
    }
    if (flag)
      throw new PXException("At least one task could not be activated. Please, review the list of errors.");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CompleteTasks(PXAdapter adapter)
  {
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      if (pmTask.Status == "A")
      {
        pmTask.Status = "F";
        ((PXSelectBase<PMTask>) this.Tasks).Update(pmTask);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CreateTemplate(PXAdapter adapter)
  {
    PMProject pmProject1 = new PMProject();
    if (!DimensionMaint.IsAutonumbered((PXGraph) this, "TMPROJECT"))
    {
      if (((PXSelectBase<ProjectEntry.TemplateSettingsFilter>) this.TemplateSettings).AskExt() != 1 || string.IsNullOrEmpty(((PXSelectBase<ProjectEntry.TemplateSettingsFilter>) this.TemplateSettings).Current.TemplateID))
        return adapter.Get();
      pmProject1.ContractCD = ((PXSelectBase<ProjectEntry.TemplateSettingsFilter>) this.TemplateSettings).Current.TemplateID;
    }
    ((PXAction) this.Save).Press();
    PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
    TemplateMaint instance = PXGraph.CreateInstance<TemplateMaint>();
    PMProject pmProject2 = ((PXSelectBase<PMProject>) instance.Project).Insert(pmProject1);
    pmProject2.Description = current.Description;
    PXDBLocalizableStringAttribute.CopyTranslations<PMProject.description, PMProject.description>(((PXGraph) this).Caches[typeof (PMProject)], (object) current, ((PXGraph) this).Caches[typeof (PMProject)], (object) pmProject2);
    pmProject2.BudgetLevel = current.BudgetLevel;
    pmProject2.CostBudgetLevel = current.CostBudgetLevel;
    pmProject2.CreateProforma = current.CreateProforma;
    pmProject2.PrepaymentEnabled = current.PrepaymentEnabled;
    pmProject2.PrepaymentDefCode = current.PrepaymentDefCode;
    pmProject2.LimitsEnabled = current.LimitsEnabled;
    pmProject2.ChangeOrderWorkflow = current.ChangeOrderWorkflow;
    pmProject2.AllowIssueFromFreeStock = current.AllowIssueFromFreeStock;
    pmProject2.TermsID = current.TermsID;
    pmProject2.AutoAllocate = current.AutoAllocate;
    pmProject2.AutomaticReleaseAR = current.AutomaticReleaseAR;
    pmProject2.DefaultSalesAccountID = current.DefaultSalesAccountID;
    pmProject2.DefaultSalesSubID = current.DefaultSalesSubID;
    pmProject2.DefaultExpenseAccountID = current.DefaultExpenseAccountID;
    pmProject2.DefaultExpenseSubID = current.DefaultExpenseSubID;
    pmProject2.DefaultAccrualAccountID = current.DefaultAccrualAccountID;
    pmProject2.DefaultAccrualSubID = current.DefaultAccrualSubID;
    pmProject2.DefaultBranchID = current.DefaultBranchID;
    pmProject2.CalendarID = current.CalendarID;
    pmProject2.RestrictToEmployeeList = current.RestrictToEmployeeList;
    pmProject2.RestrictToResourceList = current.RestrictToResourceList;
    pmProject2.CuryID = current.CuryID;
    pmProject2.CuryIDCopy = current.CuryIDCopy;
    pmProject2.AllowOverrideCury = current.AllowOverrideCury;
    pmProject2.AllowOverrideRate = current.AllowOverrideRate;
    pmProject2.AllocationID = current.AllocationID;
    pmProject2.BillingID = current.BillingID;
    pmProject2.ApproverID = current.ApproverID;
    pmProject2.OwnerID = current.OwnerID;
    pmProject2.AssistantID = current.AssistantID;
    pmProject2.ProjectGroupID = current.ProjectGroupID;
    pmProject2.GroupMask = current.GroupMask;
    pmProject2.RateTableID = current.RateTableID;
    pmProject2.RetainagePct = current.RetainagePct;
    pmProject2.IncludeCO = current.IncludeCO;
    pmProject2.RetainageMaxPct = current.RetainageMaxPct;
    pmProject2.RetainageMode = current.RetainageMode;
    pmProject2.SteppedRetainage = current.SteppedRetainage;
    pmProject2.VisibleInAP = current.VisibleInAP;
    pmProject2.VisibleInGL = current.VisibleInGL;
    pmProject2.VisibleInAR = current.VisibleInAR;
    pmProject2.VisibleInSO = current.VisibleInSO;
    pmProject2.VisibleInPO = current.VisibleInPO;
    pmProject2.VisibleInTA = current.VisibleInTA;
    pmProject2.VisibleInEA = current.VisibleInEA;
    pmProject2.VisibleInIN = current.VisibleInIN;
    pmProject2.VisibleInCA = current.VisibleInCA;
    pmProject2.VisibleInCR = current.VisibleInCR;
    instance.Answers.CopyAllAttributes((object) pmProject2, (object) current);
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(PXSelectBase<ContractBillingSchedule, PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Current<PMProject.contractID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) current
    }, Array.Empty<object>()));
    if (contractBillingSchedule != null)
      ((PXSelectBase<ContractBillingSchedule>) instance.Billing).Current.Type = contractBillingSchedule.Type;
    Dimension dimension = PXResultset<Dimension>.op_Implicit(PXSelectBase<Dimension, PXSelect<Dimension, Where<Dimension.dimensionID, Equal<Required<Dimension.dimensionID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) "PROTASK"
    }));
    bool flag = dimension != null && dimension.NumberingID != null;
    Dictionary<int, int> taskMap = new Dictionary<int, int>();
    foreach (PXResult<PMTask> pxResult in PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) current
    }, Array.Empty<object>()))
    {
      PMTask pmTask1 = PXResult<PMTask>.op_Implicit(pxResult);
      PMTask pmTask2 = new PMTask()
      {
        TaskCD = !flag ? pmTask1.TaskCD : (string) null,
        ProjectID = pmProject2.ContractID
      };
      pmTask2.BillingID = pmTask1.BillingID;
      pmTask2.AllocationID = pmTask1.AllocationID;
      pmTask2.Description = pmTask1.Description;
      PXDBLocalizableStringAttribute.CopyTranslations<PMTask.description, PMTask.description>(((PXGraph) this).Caches[typeof (PMTask)], (object) pmTask1, ((PXGraph) this).Caches[typeof (PMTask)], (object) pmTask2);
      pmTask2.ApproverID = pmTask1.ApproverID;
      pmTask2.TaxCategoryID = pmTask1.TaxCategoryID;
      pmTask2.BillingOption = pmTask1.BillingOption;
      pmTask2.DefaultSalesAccountID = pmTask1.DefaultSalesAccountID;
      pmTask2.DefaultSalesSubID = pmTask1.DefaultSalesSubID;
      pmTask2.DefaultExpenseAccountID = pmTask1.DefaultExpenseAccountID;
      pmTask2.DefaultExpenseSubID = pmTask1.DefaultExpenseSubID;
      pmTask2.DefaultAccrualAccountID = pmTask1.DefaultAccrualAccountID;
      pmTask2.DefaultAccrualSubID = pmTask1.DefaultAccrualSubID;
      pmTask2.DefaultBranchID = pmTask1.DefaultBranchID;
      pmTask2.WipAccountGroupID = pmTask1.WipAccountGroupID;
      pmTask2.BillSeparately = pmTask1.BillSeparately;
      pmTask2.VisibleInGL = pmTask1.VisibleInGL;
      pmTask2.VisibleInAP = pmTask1.VisibleInAP;
      pmTask2.VisibleInAR = pmTask1.VisibleInAR;
      pmTask2.VisibleInSO = pmTask1.VisibleInSO;
      pmTask2.VisibleInPO = pmTask1.VisibleInPO;
      pmTask2.VisibleInTA = pmTask1.VisibleInTA;
      pmTask2.VisibleInEA = pmTask1.VisibleInEA;
      pmTask2.VisibleInIN = pmTask1.VisibleInIN;
      pmTask2.VisibleInCA = pmTask1.VisibleInCA;
      pmTask2.VisibleInCR = pmTask1.VisibleInCR;
      pmTask2.IsActive = new bool?(pmTask1.IsActive.GetValueOrDefault());
      pmTask2.CompletedPctMethod = pmTask1.CompletedPctMethod;
      pmTask2.RateTableID = pmTask1.RateTableID;
      pmTask2.IsDefault = pmTask1.IsDefault;
      pmTask2.Type = pmTask1.Type;
      PMTask pmTask3 = ((PXSelectBase<PMTask>) instance.Tasks).Insert(pmTask2);
      instance.TaskAnswers.CopyAllAttributes((object) pmTask3, (object) pmTask1);
      this.OnCopyPasteTaskInserted(pmProject2, pmTask3, pmTask1);
      Dictionary<int, int> dictionary = taskMap;
      int? taskId = pmTask1.TaskID;
      int key = taskId.Value;
      taskId = pmTask3.TaskID;
      int num = taskId.Value;
      dictionary.Add(key, num);
    }
    this.OnCreateTemplateTasksInserted(instance, pmProject2, taskMap);
    foreach (PXResult<PMRevenueBudget> pxResult in PXSelectBase<PMRevenueBudget, PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) current
    }, Array.Empty<object>()))
    {
      PMRevenueBudget sourceItem = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
      PMRevenueBudget copy = (PMRevenueBudget) ((PXSelectBase) instance.RevenueBudget).Cache.CreateCopy((object) sourceItem);
      copy.ProjectID = pmProject2.ContractID;
      copy.ProjectTaskID = new int?(taskMap[sourceItem.TaskID.GetValueOrDefault()]);
      copy.CuryActualAmount = new Decimal?(0M);
      copy.ActualAmount = new Decimal?(0M);
      copy.ActualQty = new Decimal?(0M);
      copy.CuryAmountToInvoice = new Decimal?(0M);
      copy.QtyToInvoice = new Decimal?(0M);
      copy.CuryDraftChangeOrderAmount = new Decimal?(0M);
      copy.DraftChangeOrderAmount = new Decimal?(0M);
      copy.DraftChangeOrderQty = new Decimal?(0M);
      copy.CuryChangeOrderAmount = new Decimal?(0M);
      copy.ChangeOrderAmount = new Decimal?(0M);
      copy.ChangeOrderQty = new Decimal?(0M);
      copy.CuryCommittedOrigAmount = new Decimal?(0M);
      copy.CommittedOrigAmount = new Decimal?(0M);
      copy.CommittedOrigQty = new Decimal?(0M);
      copy.CuryCommittedAmount = new Decimal?(0M);
      copy.CommittedAmount = new Decimal?(0M);
      copy.CuryCommittedInvoicedAmount = new Decimal?(0M);
      copy.CuryCommittedOpenAmount = new Decimal?(0M);
      copy.CommittedOpenQty = new Decimal?(0M);
      copy.CommittedQty = new Decimal?(0M);
      copy.CommittedReceivedQty = new Decimal?(0M);
      copy.CompletedPct = new Decimal?(0M);
      copy.CuryCostAtCompletion = new Decimal?(0M);
      copy.CostAtCompletion = new Decimal?(0M);
      copy.CuryCostToComplete = new Decimal?(0M);
      copy.CostToComplete = new Decimal?(0M);
      copy.CuryInvoicedAmount = new Decimal?(0M);
      copy.CuryLastCostAtCompletion = new Decimal?(0M);
      copy.LastCostAtCompletion = new Decimal?(0M);
      copy.CuryLastCostToComplete = new Decimal?(0M);
      copy.LastCostToComplete = new Decimal?(0M);
      copy.LastPercentCompleted = new Decimal?(0M);
      copy.PercentCompleted = new Decimal?(0M);
      copy.CuryPrepaymentInvoiced = new Decimal?(0M);
      copy.PrepaymentInvoiced = new Decimal?(0M);
      copy.CuryDraftRetainedAmount = new Decimal?(0M);
      copy.DraftRetainedAmount = new Decimal?(0M);
      copy.CuryRetainedAmount = new Decimal?(0M);
      copy.RetainedAmount = new Decimal?(0M);
      copy.CuryTotalRetainedAmount = new Decimal?(0M);
      copy.TotalRetainedAmount = new Decimal?(0M);
      copy.NoteID = new Guid?();
      PMRevenueBudget newItem = ((PXSelectBase<PMRevenueBudget>) instance.RevenueBudget).Insert(copy);
      this.OnCopyPasteRevenueBudgetInserted(pmProject2, newItem, sourceItem);
    }
    foreach (PXResult<PMCostBudget> pxResult in PXSelectBase<PMCostBudget, PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMCostBudget.type, Equal<AccountType.expense>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) current
    }, Array.Empty<object>()))
    {
      PMCostBudget sourceItem = PXResult<PMCostBudget>.op_Implicit(pxResult);
      PMCostBudget copy = (PMCostBudget) ((PXSelectBase) instance.CostBudget).Cache.CreateCopy((object) sourceItem);
      copy.ProjectID = pmProject2.ContractID;
      copy.ProjectTaskID = new int?(taskMap[sourceItem.TaskID.GetValueOrDefault()]);
      copy.ActualAmount = new Decimal?(0M);
      copy.CuryActualAmount = new Decimal?(0M);
      copy.ActualQty = new Decimal?(0M);
      copy.CuryAmountToInvoice = new Decimal?(0M);
      copy.QtyToInvoice = new Decimal?(0M);
      copy.CuryDraftChangeOrderAmount = new Decimal?(0M);
      copy.DraftChangeOrderAmount = new Decimal?(0M);
      copy.DraftChangeOrderQty = new Decimal?(0M);
      copy.CuryChangeOrderAmount = new Decimal?(0M);
      copy.ChangeOrderAmount = new Decimal?(0M);
      copy.ChangeOrderQty = new Decimal?(0M);
      copy.CuryCommittedOrigAmount = new Decimal?(0M);
      copy.CommittedOrigAmount = new Decimal?(0M);
      copy.CommittedOrigQty = new Decimal?(0M);
      copy.CuryCommittedAmount = new Decimal?(0M);
      copy.CommittedAmount = new Decimal?(0M);
      copy.CuryCommittedInvoicedAmount = new Decimal?(0M);
      copy.CuryCommittedOpenAmount = new Decimal?(0M);
      copy.CommittedOpenQty = new Decimal?(0M);
      copy.CommittedQty = new Decimal?(0M);
      copy.CommittedReceivedQty = new Decimal?(0M);
      copy.CompletedPct = new Decimal?(0M);
      copy.CuryCostAtCompletion = new Decimal?(0M);
      copy.CostAtCompletion = new Decimal?(0M);
      copy.CuryCostToComplete = new Decimal?(0M);
      copy.CostToComplete = new Decimal?(0M);
      copy.CuryInvoicedAmount = new Decimal?(0M);
      copy.CuryLastCostAtCompletion = new Decimal?(0M);
      copy.LastCostAtCompletion = new Decimal?(0M);
      copy.CuryLastCostToComplete = new Decimal?(0M);
      copy.LastCostToComplete = new Decimal?(0M);
      copy.LastPercentCompleted = new Decimal?(0M);
      copy.PercentCompleted = new Decimal?(0M);
      copy.CuryPrepaymentInvoiced = new Decimal?(0M);
      copy.PrepaymentInvoiced = new Decimal?(0M);
      copy.CuryDraftRetainedAmount = new Decimal?(0M);
      copy.DraftRetainedAmount = new Decimal?(0M);
      copy.CuryRetainedAmount = new Decimal?(0M);
      copy.RetainedAmount = new Decimal?(0M);
      copy.CuryTotalRetainedAmount = new Decimal?(0M);
      copy.TotalRetainedAmount = new Decimal?(0M);
      copy.NoteID = new Guid?();
      int? nullable = new int?();
      if (copy.RevenueTaskID.HasValue)
      {
        if (taskMap.ContainsKey(copy.RevenueTaskID.Value))
        {
          nullable = copy.RevenueInventoryID;
          copy.RevenueTaskID = new int?(taskMap[copy.RevenueTaskID.GetValueOrDefault()]);
          copy.RevenueInventoryID = new int?();
        }
        else
        {
          copy.RevenueTaskID = new int?();
          copy.RevenueInventoryID = new int?();
        }
      }
      PMCostBudget newItem = ((PXSelectBase<PMCostBudget>) instance.CostBudget).Insert(copy);
      if (nullable.HasValue)
        ((PXSelectBase) this.CostBudget).Cache.SetValue<PMCostBudget.revenueInventoryID>((object) newItem, (object) nullable);
      this.OnCopyPasteCostBudgetInserted(pmProject2, newItem, sourceItem);
    }
    foreach (PXResult<EPEmployeeContract> pxResult in PXSelectBase<EPEmployeeContract, PXSelect<EPEmployeeContract, Where<EPEmployeeContract.contractID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) current
    }, Array.Empty<object>()))
    {
      EPEmployeeContract sourceItem = PXResult<EPEmployeeContract>.op_Implicit(pxResult);
      EPEmployeeContract newItem = ((PXSelectBase<EPEmployeeContract>) instance.EmployeeContract).Insert(new EPEmployeeContract());
      newItem.EmployeeID = sourceItem.EmployeeID;
      this.OnCopyPasteEmployeeContractInserted(pmProject2, newItem, sourceItem);
    }
    foreach (PXResult<EPContractRate> pxResult in PXSelectBase<EPContractRate, PXSelect<EPContractRate, Where<EPContractRate.contractID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) current
    }, Array.Empty<object>()))
    {
      EPContractRate sourceItem = PXResult<EPContractRate>.op_Implicit(pxResult);
      EPContractRate newItem = ((PXSelectBase<EPContractRate>) instance.ContractRates).Insert(new EPContractRate());
      newItem.IsActive = sourceItem.IsActive;
      newItem.EmployeeID = sourceItem.EmployeeID;
      newItem.EarningType = sourceItem.EarningType;
      newItem.LabourItemID = sourceItem.LabourItemID;
      this.OnCopyPasteContractRateInserted(pmProject2, newItem, sourceItem);
    }
    foreach (PXResult<EPEquipmentRate> pxResult in PXSelectBase<EPEquipmentRate, PXSelect<EPEquipmentRate, Where<EPEquipmentRate.projectID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) current
    }, Array.Empty<object>()))
    {
      EPEquipmentRate sourceItem = PXResult<EPEquipmentRate>.op_Implicit(pxResult);
      EPEquipmentRate newItem = ((PXSelectBase<EPEquipmentRate>) instance.EquipmentRates).Insert(new EPEquipmentRate());
      newItem.IsActive = sourceItem.IsActive;
      newItem.EquipmentID = sourceItem.EquipmentID;
      newItem.RunRate = sourceItem.RunRate;
      newItem.SuspendRate = sourceItem.SuspendRate;
      newItem.SetupRate = sourceItem.SetupRate;
      this.OnCopyPasteEquipmentRateInserted(pmProject2, newItem, sourceItem);
    }
    foreach (PXResult<PMProjectContact> pxResult in PXSelectBase<PMProjectContact, PXSelect<PMProjectContact, Where<PMProjectContact.projectID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) current
    }, Array.Empty<object>()))
    {
      PMProjectContact sourceItem = PXResult<PMProjectContact>.op_Implicit(pxResult);
      int? contactId = sourceItem.ContactID;
      sourceItem.ContactID = new int?();
      PMProjectContact copy = (PMProjectContact) ((PXSelectBase) this.ProjectContacts).Cache.CreateCopy((object) sourceItem);
      copy.ProjectID = new int?();
      copy.NoteID = new Guid?();
      copy.IsActive = new bool?();
      PMProjectContact newItem = ((PXSelectBase<PMProjectContact>) instance.ProjectContacts).Insert(copy);
      newItem.ContactID = contactId;
      this.OnCopyPasteProjectContactInserted(pmProject2, newItem, sourceItem);
    }
    foreach (PXResult<PMAccountTask> pxResult in PXSelectBase<PMAccountTask, PXSelect<PMAccountTask, Where<PMAccountTask.projectID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) current
    }, Array.Empty<object>()))
    {
      PMAccountTask sourceItem = PXResult<PMAccountTask>.op_Implicit(pxResult);
      PMAccountTask newItem = (PMAccountTask) ((PXSelectBase) instance.Accounts).Cache.Insert();
      newItem.ProjectID = pmProject2.ContractID;
      newItem.TaskID = new int?(taskMap[sourceItem.TaskID.GetValueOrDefault()]);
      newItem.AccountID = sourceItem.AccountID;
      this.OnCopyPasteAccountTaskInserted(pmProject2, newItem, sourceItem);
    }
    foreach (PXResult<PMRetainageStep> pxResult in ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Select(Array.Empty<object>()))
    {
      PMRetainageStep pmRetainageStep = PXResult<PMRetainageStep>.op_Implicit(pxResult);
      pmRetainageStep.ProjectID = pmProject2.ContractID;
      pmRetainageStep.NoteID = new Guid?();
      ((PXSelectBase<PMRetainageStep>) instance.RetainageSteps).Insert(pmRetainageStep);
    }
    foreach (PXResult<NotificationSource> pxResult1 in ((PXSelectBase<NotificationSource>) instance.NotificationSources).Select(Array.Empty<object>()))
    {
      NotificationSource notificationSource = PXResult<NotificationSource>.op_Implicit(pxResult1);
      foreach (PXResult<NotificationRecipient> pxResult2 in ((PXSelectBase<NotificationRecipient>) instance.NotificationRecipients).Select(new object[1]
      {
        (object) notificationSource.SourceID
      }))
      {
        NotificationRecipient notificationRecipient = PXResult<NotificationRecipient>.op_Implicit(pxResult2);
        ((PXSelectBase<NotificationRecipient>) instance.NotificationRecipients).Delete(notificationRecipient);
      }
      ((PXSelectBase<NotificationSource>) instance.NotificationSources).Delete(notificationSource);
    }
    foreach (PXResult<NotificationSource> pxResult3 in ((PXSelectBase<NotificationSource>) this.NotificationSources).Select(Array.Empty<object>()))
    {
      NotificationSource sourceItem = PXResult<NotificationSource>.op_Implicit(pxResult3);
      int? sourceId = sourceItem.SourceID;
      sourceItem.SourceID = new int?();
      sourceItem.RefNoteID = new Guid?();
      NotificationSource newItem = ((PXSelectBase<NotificationSource>) instance.NotificationSources).Insert(sourceItem);
      foreach (PXResult<NotificationRecipient> pxResult4 in ((PXSelectBase<NotificationRecipient>) this.NotificationRecipients).Select(new object[1]
      {
        (object) sourceId
      }))
      {
        NotificationRecipient notificationRecipient = PXResult<NotificationRecipient>.op_Implicit(pxResult4);
        if (notificationRecipient.ContactType == "P" || notificationRecipient.ContactType == "E")
        {
          notificationRecipient.NotificationID = new int?();
          notificationRecipient.SourceID = newItem.SourceID;
          notificationRecipient.RefNoteID = new Guid?();
          ((PXSelectBase<NotificationRecipient>) instance.NotificationRecipients).Insert(notificationRecipient);
        }
      }
      this.OnCopyPasteNotificationSourceInserted(pmProject2, newItem, sourceItem);
    }
    foreach (PXResult<PMRecurringItem> pxResult in PXSelectBase<PMRecurringItem, PXSelect<PMRecurringItem, Where<PMRecurringItem.projectID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) current
    }, Array.Empty<object>()))
    {
      PMRecurringItem sourceItem = PXResult<PMRecurringItem>.op_Implicit(pxResult);
      PMRecurringItem newItem = ((PXSelectBase<PMRecurringItem>) instance.BillingItems).Insert(new PMRecurringItem()
      {
        ProjectID = pmProject2.ContractID,
        TaskID = new int?(taskMap[sourceItem.TaskID.GetValueOrDefault()]),
        InventoryID = sourceItem.InventoryID,
        UOM = sourceItem.UOM,
        Description = sourceItem.Description,
        Amount = sourceItem.Amount,
        AccountSource = sourceItem.AccountSource,
        SubMask = sourceItem.SubMask,
        AccountID = sourceItem.AccountID,
        SubID = sourceItem.SubID,
        ResetUsage = sourceItem.ResetUsage,
        Included = sourceItem.Included
      });
      this.OnCopyPasteRecurringItemInserted(pmProject2, newItem, sourceItem);
    }
    this.OnCopyPasteCompleted(pmProject2, current);
    ProjectAccountingService.OpenInTheSameWindow((PXGraph) instance);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Tooltip = "Run Allocation")]
  public virtual IEnumerable RunAllocation(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      ((PXAction) this.Save).Press();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CRunAllocation\u003Eb__179_0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Tooltip = "Recalculate Project Balance")]
  public virtual IEnumerable ValidateBalance(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ProjectEntry.\u003C\u003Ec__DisplayClass181_0 displayClass1810 = new ProjectEntry.\u003C\u003Ec__DisplayClass181_0();
      ((PXAction) this.Save).Press();
      // ISSUE: reference to a compiler-generated field
      displayClass1810.graph = PXGraph.CreateInstance<ProjectBalanceValidationProcess>();
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase<PMProject>) displayClass1810.graph.Project).Current = ((PXSelectBase<PMProject>) this.Project).Current;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1810, __methodptr(\u003CValidateBalance\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Tooltip = "Creates projected budget based on the expenses and Allocation Rules")]
  public virtual IEnumerable AutoBudget(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      ((PXAction) this.Save).Press();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CAutoBudget\u003Eb__183_0)));
    }
    return adapter.Get();
  }

  public virtual void RunAutoBudget()
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    foreach (AutoBudgetWorkerProcess.Balance balance in PXGraph.CreateInstance<AutoBudgetWorkerProcess>().Run(((PXSelectBase<PMProject>) this.Project).Current.ContractID))
    {
      bool flag = false;
      foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()))
      {
        PMRevenueBudget pmRevenueBudget = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
        int? taskId1 = pmRevenueBudget.TaskID;
        int taskId2 = balance.TaskID;
        if (taskId1.GetValueOrDefault() == taskId2 & taskId1.HasValue)
        {
          int? accountGroupId1 = pmRevenueBudget.AccountGroupID;
          int accountGroupId2 = balance.AccountGroupID;
          if (accountGroupId1.GetValueOrDefault() == accountGroupId2 & accountGroupId1.HasValue)
          {
            int? inventoryId1 = pmRevenueBudget.InventoryID;
            int inventoryId2 = balance.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2 & inventoryId1.HasValue)
            {
              int? costCodeId1 = pmRevenueBudget.CostCodeID;
              int costCodeId2 = balance.CostCodeID;
              if (costCodeId1.GetValueOrDefault() == costCodeId2 & costCodeId1.HasValue)
              {
                flag = true;
                pmRevenueBudget.Qty = new Decimal?(balance.Quantity);
                pmRevenueBudget.CuryAmount = new Decimal?(balance.Amount);
                ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Update(pmRevenueBudget);
              }
            }
          }
        }
      }
      if (!flag)
      {
        PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, new int?(balance.AccountGroupID));
        if (pmAccountGroup != null && pmAccountGroup.Type == "I")
        {
          PMRevenueBudget pmRevenueBudget1 = new PMRevenueBudget();
          pmRevenueBudget1.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
          pmRevenueBudget1.ProjectTaskID = new int?(balance.TaskID);
          pmRevenueBudget1.AccountGroupID = new int?(balance.AccountGroupID);
          pmRevenueBudget1.InventoryID = new int?(balance.InventoryID);
          pmRevenueBudget1.CostCodeID = new int?(balance.CostCodeID);
          PMRevenueBudget pmRevenueBudget2 = ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Insert(pmRevenueBudget1);
          pmRevenueBudget2.Qty = new Decimal?(balance.Quantity);
          pmRevenueBudget2.CuryAmount = new Decimal?(balance.Amount);
          ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Update(pmRevenueBudget2);
        }
      }
    }
    ((PXAction) this.Save).Press();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable Hold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Activate")]
  protected virtual IEnumerable Activate(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable LockBudget(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      ((PXSelectBase<PMProject>) this.Project).Current.BudgetFinalized = new bool?(true);
      ((PXSelectBase<PMProject>) this.Project).Update(((PXSelectBase<PMProject>) this.Project).Current);
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable UnlockBudget(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      ((PXSelectBase<PMProject>) this.Project).Current.BudgetFinalized = new bool?(false);
      ((PXSelectBase<PMProject>) this.Project).Update(((PXSelectBase<PMProject>) this.Project).Current);
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable LockCommitments(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      ((PXSelectBase<PMProject>) this.Project).Current.LockCommitments = new bool?(true);
      ((PXSelectBase<PMProject>) this.Project).Update(((PXSelectBase<PMProject>) this.Project).Current);
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable UnlockCommitments(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      ((PXSelectBase<PMProject>) this.Project).Current.LockCommitments = new bool?(false);
      ((PXSelectBase<PMProject>) this.Project).Update(((PXSelectBase<PMProject>) this.Project).Current);
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false)]
  public virtual IEnumerable UpdateRetainage(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      bool flag = false;
      foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()))
      {
        Decimal? retainagePct1 = PXResult<PMRevenueBudget>.op_Implicit(pxResult).RetainagePct;
        Decimal? retainagePct2 = ((PXSelectBase<PMProject>) this.Project).Current.RetainagePct;
        if (!(retainagePct1.GetValueOrDefault() == retainagePct2.GetValueOrDefault() & retainagePct1.HasValue == retainagePct2.HasValue))
          flag = true;
      }
      if (flag)
      {
        if (((PXSelectBase<PMProject>) this.Project).Current.RetainageMode == "C")
          this.SyncRetainage();
        else if (((PXSelectBase<PMProject>) this.Project).Ask("Default Retainage (%) Changed", "Update Retainage (%) in the revenue budget lines?", (MessageButtons) 4, (MessageIcon) 2) == 6)
          this.SyncRetainage();
      }
    }
    return adapter.Get();
  }

  public virtual void SyncRetainage()
  {
    List<PMRevenueBudget> pmRevenueBudgetList = new List<PMRevenueBudget>();
    foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()))
    {
      PMRevenueBudget pmRevenueBudget = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
      Decimal? retainagePct1 = pmRevenueBudget.RetainagePct;
      Decimal? retainagePct2 = ((PXSelectBase<PMProject>) this.Project).Current.RetainagePct;
      if (!(retainagePct1.GetValueOrDefault() == retainagePct2.GetValueOrDefault() & retainagePct1.HasValue == retainagePct2.HasValue))
        pmRevenueBudgetList.Add(pmRevenueBudget);
    }
    if (pmRevenueBudgetList.Count <= 0)
      return;
    foreach (PMRevenueBudget pmRevenueBudget in pmRevenueBudgetList)
    {
      pmRevenueBudget.RetainagePct = ((PXSelectBase<PMProject>) this.Project).Current.RetainagePct;
      ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Update(pmRevenueBudget);
    }
  }

  [PXUIField]
  [PXButton]
  public IEnumerable ViewReleaseRetainage(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      ARRetainageRelease instance = PXGraph.CreateInstance<ARRetainageRelease>();
      ((PXSelectBase<ARRetainageFilter>) instance.Filter).Insert(new ARRetainageFilter());
      ((PXSelectBase<ARRetainageFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
      ((PXSelectBase<ARRetainageFilter>) instance.Filter).Current.CustomerID = ((PXSelectBase<PMProject>) this.Project).Current.CustomerID;
      ((PXSelectBase<ARRetainageFilter>) instance.Filter).Current.ShowBillsWithOpenBalance = new bool?(true);
      throw new PXRedirectRequiredException((PXGraph) instance, "Release Retainage", true);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable CopyProject(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      ((PXAction) this.Save).Press();
      this.IsCopyPaste = true;
      try
      {
        this.Copy(((PXSelectBase<PMProject>) this.Project).Current);
      }
      finally
      {
        this.IsCopyPaste = false;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CreatePurchaseOrder(PXAdapter adapter)
  {
    return this.CreatePOOrderBase<POOrderEntry>(adapter, "Create Purchase Order");
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CreateDropShipOrder(PXAdapter adapter)
  {
    POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
    PX.Objects.PO.POOrder poOrder = ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Insert();
    ((PXSelectBase) instance.Document).Cache.SetValueExt<PX.Objects.PO.POOrder.orderType>((object) poOrder, (object) "PD");
    ((PXSelectBase) instance.Document).Cache.SetValueExt<PX.Objects.PO.POOrder.projectID>((object) poOrder, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Create Drop-Ship");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
    throw requiredException;
  }

  public virtual IEnumerable CreatePOOrderBase<TGraph>(PXAdapter adapter, string windowHeader) where TGraph : POOrderEntry, new()
  {
    TGraph instance = PXGraph.CreateInstance<TGraph>();
    ((PXSelectBase) instance.Document).Cache.SetValueExt<PX.Objects.PO.POOrder.projectID>((object) ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Insert(), (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) (object) instance, true, windowHeader);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
    throw requiredException;
  }

  [PXUIField(DisplayName = "View on Map")]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false)]
  public virtual void viewAddressOnMap()
  {
    new MapService((PXGraph) this).viewAddressOnMap((IAddressLocation) PXResultset<PMSiteAddress>.op_Implicit(((PXSelectBase<PMSiteAddress>) this.Site_Address).Select(Array.Empty<object>())));
  }

  [PXButton]
  [PXUIField(DisplayName = "Complete")]
  public virtual IEnumerable Complete(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      if (PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMProject.contractID>>, And<PMTask.isCompleted, Equal<False>, And<PMTask.isCancelled, Equal<False>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()).Count > 0)
      {
        ((PXSelectBase) this.Project).Cache.RaiseExceptionHandling<PMProject.status>((object) ((PXSelectBase<PMProject>) this.Project).Current, (object) ((PXSelectBase<PMProject>) this.Project).Current.Status, (Exception) new PXSetPropertyException<PMProject.status>("The project cannot be completed because at least one project task is not completed.", (PXErrorLevel) 4));
        throw new PXException("The project cannot be completed because at least one project task is not completed.");
      }
      ((PXSelectBase<PMProject>) this.Project).Current.IsCompleted = new bool?(true);
      if (!((PXSelectBase<PMProject>) this.Project).Current.ExpireDate.HasValue)
        ((PXSelectBase<PMProject>) this.Project).Current.ExpireDate = ((PXGraph) this).Accessinfo.BusinessDate;
      ((PXSelectBase<PMProject>) this.Project).Update(((PXSelectBase<PMProject>) this.Project).Current);
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Close")]
  public virtual IEnumerable Close(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ProjectEntry.\u003C\u003Ec__DisplayClass214_0 displayClass2140 = new ProjectEntry.\u003C\u003Ec__DisplayClass214_0();
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    displayClass2140.projectID = ((PXSelectBase<PMProject>) this.Project).Current.ProjectID;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass2140, __methodptr(\u003CClose\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void AddNewProjectTemplate()
  {
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<TemplateMaint>(), "New Project Template");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  public ProjectEntry()
  {
    if (((PXSelectBase<PMSetup>) this.Setup).Current == null)
      throw new PXException("Project Management Setup is not configured.");
    ((PXAction) this.CopyPaste).SetVisible(false);
    ((PXSelectBase) this.Invoices).Cache.AllowDelete = false;
    ((PXSelectBase) this.Invoices).Cache.AllowInsert = false;
    ((PXSelectBase) this.Invoices).Cache.AllowUpdate = false;
    ((PXSelectBase) this.BalanceRecords).Cache.AllowDelete = false;
    ((PXSelectBase) this.BalanceRecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.BalanceRecords).Cache.AllowUpdate = false;
    if (((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.NonProject.GetValueOrDefault())
      PXUIFieldAttribute.SetReadOnly(((PXSelectBase) this.Project).Cache, (object) null, true);
    bool flag1 = this.CommitmentLockVisible();
    bool flag2 = this.CostCommitmentTrackingEnabled();
    ((PXAction) this.lockCommitments).SetVisible(flag1);
    ((PXAction) this.unlockCommitments).SetVisible(flag1);
    ((PXAction) this.viewCostCommitments).SetVisible(flag2);
    ((PXAction) this.viewCommitments).SetVisible(flag2);
    bool flag3 = PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>();
    ((PXAction) this.currencyRates).SetVisible(flag3);
    ((PXAction) this.setCurrencyRates).SetVisible(flag3);
    ((PXAction) this.forecast).SetVisible(PXAccess.FeatureInstalled<FeaturesSet.budgetForecast>());
    ((PXAction) this.viewReleaseRetainage).SetVisible(PXAccess.FeatureInstalled<FeaturesSet.retainage>());
    if (!new ProjectSettingsManager().CalculateProjectSpecificTaxes)
      PXDefaultAttribute.SetPersistingCheck<PMSiteAddress.countryID>(((PXSelectBase) this.Site_Address).Cache, (object) null, (PXPersistingCheck) 2);
    ((PXSelectBase) this.Tasks).GetAttribute<PXImportAttribute>().MappingPropertiesInit += new EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs>(this.MappingTaskPropertiesInit);
    ((PXAction) this.openProjectOverview).SetVisible(PXAccess.FeatureInstalled<FeaturesSet.projectOverview>());
  }

  public virtual IFinPeriodRepository FinPeriodRepository
  {
    get
    {
      if (this.finPeriodsRepo == null)
        this.finPeriodsRepo = (IFinPeriodRepository) new PX.Objects.GL.FinPeriods.FinPeriodRepository((PXGraph) this);
      return this.finPeriodsRepo;
    }
  }

  private void BeforeCommitHandler(PXGraph e)
  {
    Action<PXGraph> checkerDelegate1 = this._licenseLimits.GetCheckerDelegate<PMProject>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (PMTask), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<PMTask.projectID>((object) (int?) ((PXSelectBase<PMProject>) ((ProjectEntryBase<ProjectEntry>) graph).Project).Current?.ContractID)
      }))
    });
    try
    {
      checkerDelegate1(e);
    }
    catch (PXException ex)
    {
      throw new PXException("The total number of lines on the Tasks tab has exceeded the limit set for the current license. Please reduce the number of lines to be able to save the document.");
    }
    Action<PXGraph> checkerDelegate2 = this._licenseLimits.GetCheckerDelegate<PMProject>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (PMBudget), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<PMBudget.projectID>((object) (int?) ((PXSelectBase<PMProject>) ((ProjectEntryBase<ProjectEntry>) graph).Project).Current?.ContractID),
        (PXDataFieldValue) new PXDataFieldValue<PMBudget.type>((PXDbType) 3, (object) "I")
      }))
    });
    try
    {
      checkerDelegate2(e);
    }
    catch (PXException ex)
    {
      throw new PXException("The total number of lines on the Revenue Budget tab has exceeded the limit set for the current license. To be able to save the project, reduce the number of the revenue budget lines.");
    }
    Action<PXGraph> checkerDelegate3 = this._licenseLimits.GetCheckerDelegate<PMProject>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (PMBudget), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<PMBudget.projectID>((object) (int?) ((PXSelectBase<PMProject>) ((ProjectEntryBase<ProjectEntry>) graph).Project).Current?.ContractID),
        (PXDataFieldValue) new PXDataFieldValue<PMBudget.type>((PXDbType) 3, (object) "E")
      }))
    });
    try
    {
      checkerDelegate3(e);
    }
    catch (PXException ex)
    {
      throw new PXException("The total number of lines on the Cost Budget tab has exceeded the limit set for the current license. To be able to save the project, reduce the number of the cost budget lines.");
    }
  }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += new Action<PXGraph>(this.BeforeCommitHandler);
  }

  public static bool IsProjectEditable(PMProject project)
  {
    return !project.IsCompleted.GetValueOrDefault() && !project.IsCancelled.GetValueOrDefault() && !project.Rejected.GetValueOrDefault() && !(project.Status == "L");
  }

  protected virtual void _(PX.Data.Events.RowSelected<ProjectEntry.SelectedTask> e)
  {
    if (e.Row == null)
      return;
    bool flag = false;
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(pmTask.TaskCD) && string.Equals(pmTask.TaskCD.Trim(), e.Row.TaskCD.Trim(), StringComparison.InvariantCultureIgnoreCase))
      {
        flag = true;
        break;
      }
    }
    PXUIFieldAttribute.SetWarning<ProjectEntry.SelectedTask.taskCD>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ProjectEntry.SelectedTask>>) e).Cache, (object) e.Row, flag ? "Task with this ID already exists in the Project." : (string) null);
    PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ProjectEntry.SelectedTask>>) e).Cache, (object) e.Row, !flag);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<ProjectEntry.SelectedTask> e)
  {
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMTask> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMTask.locationID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTask>>) e).Cache, (object) e.Row, e.Row.Status == "D");
    PXUIFieldAttribute.SetEnabled<PMTask.billingOption>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTask>>) e).Cache, (object) e.Row, e.Row.Status == "D");
    PXUIFieldAttribute.SetEnabled<PMTask.completedPercent>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTask>>) e).Cache, (object) e.Row, e.Row.Status != "D");
    PXUIFieldAttribute.SetEnabled<PMTask.plannedStartDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTask>>) e).Cache, (object) e.Row, e.Row.Status == "D");
    PXUIFieldAttribute.SetEnabled<PMTask.plannedEndDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTask>>) e).Cache, (object) e.Row, e.Row.Status == "D");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMTask> e)
  {
    if (e.Row == null)
      return;
    if (((PXGraph) this).IsCopyPasteContext)
    {
      e.Row.Status = "D";
      e.Row.IsActive = new bool?(false);
      e.Row.IsCompleted = new bool?(false);
      e.Row.IsCancelled = new bool?(false);
      e.Row.StartDate = new DateTime?();
      e.Row.EndDate = new DateTime?();
      e.Row.CompletedPercent = new Decimal?(0M);
    }
    if (!((PXGraph) this).IsMobile || !string.IsNullOrEmpty(e.Row.TaskCD))
      return;
    PXUIFieldAttribute.SetError<PMTask.taskCD>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PMTask>>) e).Cache, (object) e.Row, "Task ID cannot be empty.");
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PMTask> e)
  {
    if (e.Row == null)
      return;
    this.ValidateAndRaiseErrorTaskCanBeDeleted(e.Row);
  }

  protected virtual void ValidateAndRaiseErrorTaskCanBeDeleted(PMTask row)
  {
    if (row.IsActive.GetValueOrDefault())
    {
      bool? isCancelled = row.IsCancelled;
      bool flag = false;
      if (isCancelled.GetValueOrDefault() == flag & isCancelled.HasValue)
        throw new PXException("The task cannot be deleted. You can delete a task with only the Planning or Canceled status.");
    }
    if (PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXSelect<PMTran, Where<PMTran.projectID, Equal<Required<PMTask.projectID>>, And<PMTran.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) row.ProjectID,
      (object) row.TaskID
    })) != null)
      throw new PXException("Cannot delete Task since it already has at least one Transaction associated with it.");
    if (PXResultset<PMTimeActivity>.op_Implicit(PXSelectBase<PMTimeActivity, PXSelect<PMTimeActivity, Where<PMTimeActivity.projectID, Equal<Required<PMTask.projectID>>, And<PMTimeActivity.projectTaskID, Equal<Required<PMTask.taskID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) row.ProjectID,
      (object) row.TaskID
    })) != null)
      throw new PXException("Cannot delete Task since it already has at least on Activity associated with it.");
    if (PXResultset<EPTimeCardItem>.op_Implicit(PXSelectBase<EPTimeCardItem, PXSelect<EPTimeCardItem, Where<EPTimeCardItem.projectID, Equal<Required<PMTask.projectID>>, And<EPTimeCardItem.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) row.ProjectID,
      (object) row.TaskID
    })) != null)
      throw new PXException("Cannot delete Task since it already has at least one Time Card Item Record associated with it.");
  }

  [PXMergeAttributes]
  [PXDateAndTime]
  [PXUIField(DisplayName = "Start Date")]
  [PXFormula(typeof (IsNull<Current<CRActivity.startDate>, Current<PMCRActivity.date>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCRActivity.startDate> e)
  {
    if (((PXGraph) this).IsExport)
      return;
    EPSetup epSetup1 = (EPSetup) null;
    try
    {
      if (!(((PXGraph) this).Caches[typeof (EPSetup)]?.Current is EPSetup epSetup2))
        epSetup2 = ((PXSelectBase<EPSetup>) new PXSetupSelect<EPSetup>((PXGraph) this)).SelectSingle(Array.Empty<object>());
      epSetup1 = epSetup2;
    }
    catch
    {
    }
    PXDateAndTimeAttribute attribute = PXDateAndTimeAttribute.GetAttribute(e.Cache, (object) null, "startDate");
    string str1;
    string str2 = str1 = epSetup1 == null || !epSetup1.RequireTimes.GetValueOrDefault() ? "d" : "g";
    ((PXDateAttribute) attribute).DisplayMask = str1;
    ((PXDateAttribute) attribute).InputMask = str2;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedStartDate> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedStartDate>, PMTask, object>) e).NewValue == null || ((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    DateTime? nullable = ((PXSelectBase<PMProject>) this.Project).Current.StartDate;
    if (!nullable.HasValue)
      return;
    DateTime newValue = (DateTime) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedStartDate>, PMTask, object>) e).NewValue;
    DateTime date1 = newValue.Date;
    nullable = ((PXSelectBase<PMProject>) this.Project).Current.StartDate;
    DateTime dateTime = nullable.Value;
    DateTime date2 = dateTime.Date;
    if (date1 < date2)
      ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedStartDate>>) e).Cache.RaiseExceptionHandling<PMTask.plannedStartDate>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedStartDate>, PMTask, object>) e).NewValue, (Exception) new PXSetPropertyException<PMTask.plannedStartDate>("The Start Date of the {0} project task must be within the date range defined by the Start Date and End Date of the project. ", (PXErrorLevel) 2, new object[1]
      {
        (object) e.Row.TaskCD.Trim()
      }));
    nullable = e.Row.PlannedEndDate;
    if (!nullable.HasValue)
      return;
    dateTime = newValue;
    nullable = e.Row.PlannedEndDate;
    if ((nullable.HasValue ? (dateTime > nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedStartDate>>) e).Cache.RaiseExceptionHandling<PMTask.plannedStartDate>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedStartDate>, PMTask, object>) e).NewValue, (Exception) new PXSetPropertyException<PMTask.plannedStartDate>("The Planned Start Date of the project task must be earlier than the Planned End Date.", (PXErrorLevel) 4));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedEndDate> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedEndDate>, PMTask, object>) e).NewValue == null || ((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    DateTime? nullable = ((PXSelectBase<PMProject>) this.Project).Current.ExpireDate;
    if (!nullable.HasValue)
      return;
    DateTime newValue = (DateTime) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedEndDate>, PMTask, object>) e).NewValue;
    DateTime date1 = newValue.Date;
    nullable = ((PXSelectBase<PMProject>) this.Project).Current.ExpireDate;
    DateTime dateTime = nullable.Value;
    DateTime date2 = dateTime.Date;
    if (date1 > date2)
      ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedEndDate>>) e).Cache.RaiseExceptionHandling<PMTask.plannedEndDate>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedEndDate>, PMTask, object>) e).NewValue, (Exception) new PXSetPropertyException<PMTask.plannedEndDate>("The End Date of the {0} project task must be within the date range defined by the Start Date and End Date of the project. ", (PXErrorLevel) 2, new object[1]
      {
        (object) e.Row.TaskCD.Trim()
      }));
    nullable = e.Row.PlannedStartDate;
    if (!nullable.HasValue)
      return;
    dateTime = newValue;
    nullable = e.Row.PlannedStartDate;
    if ((nullable.HasValue ? (dateTime < nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedEndDate>>) e).Cache.RaiseExceptionHandling<PMTask.plannedEndDate>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTask, PMTask.plannedEndDate>, PMTask, object>) e).NewValue, (Exception) new PXSetPropertyException<PMTask.plannedEndDate>("The Planned Start Date of the project task must be earlier than the Planned End Date.", (PXErrorLevel) 4));
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PMTask, PMTask.status> e)
  {
    if (!((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTask, PMTask.status>, PMTask, object>) e).NewValue == "A"))
      return;
    HashSet<string> stringSet = new HashSet<string>();
    foreach (PXResult<CSAttributeGroup> pxResult in ((PXSelectBase<CSAttributeGroup>) new PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityClassID, Equal<GroupTypes.TaskType>, And<CSAttributeGroup.required, Equal<True>>>>((PXGraph) this)).Select(Array.Empty<object>()))
    {
      CSAttributeGroup csAttributeGroup = PXResult<CSAttributeGroup>.op_Implicit(pxResult);
      stringSet.Add(csAttributeGroup.AttributeID);
    }
    if (stringSet.Count <= 0)
      return;
    if (((PXSelectBase) this.Tasks).Cache.GetStatus((object) e.Row) == 2)
      throw new PXSetPropertyException<PMTask.status>("The project tasks cannot be activated because required attributes of the tasks have no values. Please, use the Project Tasks (PM302000) form to fill in required attribute values.");
    foreach (PXResult<CSAnswers> pxResult in ((PXSelectBase<CSAnswers>) new PXSelect<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<PMTask.noteID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) e.Row.NoteID
    }))
    {
      CSAnswers csAnswers = PXResult<CSAnswers>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(csAnswers.Value))
        stringSet.Remove(csAnswers.AttributeID);
    }
    if (stringSet.Count > 0)
      throw new PXSetPropertyException<PMTask.status>("The project tasks cannot be activated because required attributes of the tasks have no values. Please, use the Project Tasks (PM302000) form to fill in required attribute values.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTask, PMTask.completedPctMethod> e)
  {
    ProjectTaskEntry.OnTaskCompletedPctMethodUpdated((PXGraph) this, e);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTask, PMTask.status> e)
  {
    if (e.Row == null)
      return;
    switch (e.Row.Status)
    {
      case "A":
        e.Row.IsActive = new bool?(true);
        e.Row.IsCompleted = new bool?(false);
        e.Row.IsCancelled = new bool?(false);
        if (e.Row.StartDate.HasValue)
          break;
        e.Row.StartDate = ((PXGraph) this).Accessinfo.BusinessDate;
        break;
      case "C":
        e.Row.IsActive = new bool?(false);
        e.Row.IsCompleted = new bool?(false);
        e.Row.IsCancelled = new bool?(true);
        break;
      case "F":
        e.Row.IsActive = new bool?(true);
        e.Row.IsCompleted = new bool?(true);
        e.Row.IsCancelled = new bool?(false);
        if (!e.Row.EndDate.HasValue)
          e.Row.EndDate = ((PXGraph) this).Accessinfo.BusinessDate;
        e.Row.CompletedPercent = new Decimal?(PMTaskCompletedAttribute.GetCompletionPercentageOfCompletedTask(e.Row));
        break;
      case "D":
        e.Row.IsActive = new bool?(false);
        e.Row.IsCompleted = new bool?(false);
        e.Row.IsCancelled = new bool?(false);
        break;
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTask, PMTask.isDefault> e)
  {
    if (!e.Row.IsDefault.GetValueOrDefault())
      return;
    bool flag = false;
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      if (pmTask.IsDefault.GetValueOrDefault())
      {
        int? taskId1 = pmTask.TaskID;
        int? taskId2 = e.Row.TaskID;
        if (!(taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue))
        {
          ((PXSelectBase) this.Tasks).Cache.SetValue<PMTask.isDefault>((object) pmTask, (object) false);
          GraphHelper.SmartSetStatus(((PXSelectBase) this.Tasks).Cache, (object) pmTask, (PXEntryStatus) 1, (PXEntryStatus) 0);
          flag = true;
        }
      }
    }
    if (!flag)
      return;
    ((PXSelectBase) this.Tasks).View.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMTask> e)
  {
    if (e.Operation != 3 && e.Row.Status == "A" && !e.Row.IsActive.GetValueOrDefault())
      throw new PXException("The {0} task cannot be saved. Please save the task with the Planned status first and then change the status to Active.", new object[1]
      {
        (object) e.Row.TaskCD
      });
    if (e.Operation != 2)
      return;
    this.negativeKey = e.Row.TaskID;
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PMTask> e)
  {
    if (e.Operation == 2 && e.TranStatus == null && this.negativeKey.HasValue)
    {
      int? taskId = e.Row.TaskID;
      List<PMCostBudget> pmCostBudgetList;
      if (this.negativeKey.HasValue && this.costBudgetsByRevenueTaskID.TryGetValue(this.negativeKey.Value, out pmCostBudgetList))
      {
        foreach (object obj in pmCostBudgetList)
        {
          ((PXSelectBase) this.CostBudget).Cache.SetValue<PMCostBudget.revenueTaskID>(obj, (object) taskId);
          if (!this.persistedTask.ContainsKey(taskId))
            this.persistedTask.Add(taskId, this.negativeKey);
        }
      }
      List<PMProjectBudgetHistoryAccum> budgetHistoryAccumList;
      if (this.negativeKey.HasValue && this.budgetHistoryByTaskID.TryGetValue(this.negativeKey.Value, out budgetHistoryAccumList))
      {
        foreach (object obj in budgetHistoryAccumList)
        {
          ((PXSelectBase) this.ProjectBudgetHistoryAccum).Cache.SetValue<PMProjectBudgetHistoryAccum.taskID>(obj, (object) taskId);
          if (!this.persistedTaskBudgetHistory.ContainsKey(taskId))
            this.persistedTaskBudgetHistory.Add(taskId, this.negativeKey);
        }
      }
      this.negativeKey = new int?();
    }
    if (e.Operation != 2 || e.TranStatus != 2)
      return;
    int? nullable;
    List<PMCostBudget> pmCostBudgetList1;
    if (this.persistedTask.TryGetValue(e.Row.TaskID, out nullable) && this.negativeKey.HasValue && this.costBudgetsByRevenueTaskID.TryGetValue(nullable.Value, out pmCostBudgetList1))
    {
      foreach (object obj in pmCostBudgetList1)
        ((PXSelectBase) this.CostBudget).Cache.SetValue<PMCostBudget.revenueTaskID>(obj, (object) nullable);
    }
    List<PMProjectBudgetHistoryAccum> budgetHistoryAccumList1;
    if (!this.persistedTaskBudgetHistory.TryGetValue(e.Row.TaskID, out nullable) || !this.negativeKey.HasValue || !this.budgetHistoryByTaskID.TryGetValue(nullable.Value, out budgetHistoryAccumList1))
      return;
    foreach (object obj in budgetHistoryAccumList1)
      ((PXSelectBase) this.ProjectBudgetHistoryAccum).Cache.SetValue<PMProjectBudgetHistoryAccum.taskID>(obj, (object) nullable);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PMProject> e)
  {
    if (e.Row == null || e.Row.CuryCapAmount.HasValue)
      return;
    using (new PXConnectionScope())
      e.Row.CuryCapAmount = this.CalculateCapAmount(e.Row, (PMProjectRevenueTotal) ((PXSelectBase) this.ProjectRevenueTotals).View.SelectSingleBound(new object[1]
      {
        (object) e.Row
      }, Array.Empty<object>()));
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProject> e)
  {
    ((PXAction) this.openProjectOverview).SetEnabled(!this.IsNewProject());
    bool flag1 = ((PXGraph) this).GetExtension<ProjectEntry_ChangeOrderExt>() != null && ((PXGraph) this).GetExtension<ProjectEntry_ChangeOrderExt>().ChangeOrderEnabled();
    int num1 = ((PXGraph) this).GetExtension<ProjectEntry_ChangeOrderExt>() == null ? 0 : (((PXGraph) this).GetExtension<ProjectEntry_ChangeOrderExt>().ChangeOrderVisible() ? 1 : 0);
    bool flag2 = this.CommitmentLockVisible();
    bool flag3 = this.CostCommitmentTrackingEnabled();
    ((PXAction) this.lockCommitments).SetVisible(flag2);
    ((PXAction) this.unlockCommitments).SetVisible(flag2);
    bool purchaseOrderVisible = this.CreatePurchaseOrderVisible();
    ((PXAction) this.createPurchaseOrder).SetVisible(purchaseOrderVisible);
    ((PXAction) this.createDropShipOrder).SetVisible(purchaseOrderVisible);
    if (e.Row == null)
      return;
    this.ThrowIfStatusCodeIsNotValid(e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache);
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row1 = e.Row;
    bool? nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInGL;
    int num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInGL>(cache1, (object) row1, num2 != 0);
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row2 = e.Row;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInAP;
    int num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInAP>(cache2, (object) row2, num3 != 0);
    PXCache cache3 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row3 = e.Row;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInAR;
    int num4 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInAR>(cache3, (object) row3, num4 != 0);
    PXCache cache4 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row4 = e.Row;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInSO;
    int num5 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInSO>(cache4, (object) row4, num5 != 0);
    PXCache cache5 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row5 = e.Row;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInPO;
    int num6 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInPO>(cache5, (object) row5, num6 != 0);
    PXCache cache6 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row6 = e.Row;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInTA;
    int num7 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInTA>(cache6, (object) row6, num7 != 0);
    PXCache cache7 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row7 = e.Row;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInEA;
    int num8 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInEA>(cache7, (object) row7, num8 != 0);
    PXCache cache8 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row8 = e.Row;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInIN;
    int num9 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInIN>(cache8, (object) row8, num9 != 0);
    PXCache cache9 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row9 = e.Row;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInCA;
    int num10 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInCA>(cache9, (object) row9, num10 != 0);
    PXCache cache10 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row10 = e.Row;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInCR;
    int num11 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInCR>(cache10, (object) row10, num11 != 0);
    PXCache cache11 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row11 = e.Row;
    int? nullable2 = e.Row.TemplateID;
    int num12 = nullable2.HasValue ? 0 : (((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache.GetStatus((object) e.Row) == 2 ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<PMProject.templateID>(cache11, (object) row11, num12 != 0);
    PXCache cache12 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row12 = e.Row;
    nullable2 = ((PXSelectBase<PMProject>) this.Project).Current.CustomerID;
    int num13 = nullable2.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CT.Contract.createProforma>(cache12, (object) row12, num13 != 0);
    PXCache cache13 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row13 = e.Row;
    nullable2 = ((PXSelectBase<PMProject>) this.Project).Current.CustomerID;
    int num14 = nullable2.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CT.Contract.automaticReleaseAR>(cache13, (object) row13, num14 != 0);
    bool flag4 = e.Row.Status == "L";
    ((PXSelectBase) this.Project).Cache.AllowUpdate = !flag4;
    ((PXSelectBase) this.Site_Address).Cache.SetAllEditPermissions(!flag4);
    ((PXSelectBase) this.Unions).Cache.SetAllEditPermissions(!flag4);
    ((PXSelectBase) this.Accounts).Cache.SetAllEditPermissions(!flag4);
    ((PXSelectBase) this.ProjectContacts).Cache.SetAllEditPermissions(!flag4);
    if (flag4)
    {
      ((PXAction) this.activate).SetEnabled(true);
      ((PXAction) this.createTemplate).SetEnabled(true);
    }
    ((PXAction) this.forecast).SetEnabled(!flag4);
    ((PXAction) this.currencyRates).SetEnabled(!flag4);
    ((PXAction) this.projectBalanceReport).SetEnabled(!flag4);
    ((PXAction) this.ChangeID).SetEnabled(!flag4);
    nullable1 = e.Row.IsCompleted;
    int num15;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = e.Row.IsCancelled;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = e.Row.Rejected;
        if (!nullable1.GetValueOrDefault())
        {
          num15 = !flag4 ? 1 : 0;
          goto label_8;
        }
      }
    }
    num15 = 0;
label_8:
    bool isProjectEditable = num15 != 0;
    ((PXAction) this.createPurchaseOrder).SetEnabled(purchaseOrderVisible & isProjectEditable);
    ((PXAction) this.createDropShipOrder).SetEnabled(purchaseOrderVisible & isProjectEditable);
    this.ConfigureTasksTab(isProjectEditable);
    ((PXSelectBase) this.ContractRates).Cache.AllowInsert = isProjectEditable;
    ((PXSelectBase) this.ContractRates).Cache.AllowUpdate = isProjectEditable;
    ((PXSelectBase) this.ContractRates).Cache.AllowDelete = isProjectEditable;
    ((PXSelectBase) this.EmployeeContract).Cache.AllowInsert = isProjectEditable;
    ((PXSelectBase) this.EmployeeContract).Cache.AllowUpdate = isProjectEditable;
    ((PXSelectBase) this.EmployeeContract).Cache.AllowDelete = isProjectEditable;
    ((PXSelectBase) this.EquipmentRates).Cache.AllowInsert = isProjectEditable;
    ((PXSelectBase) this.EquipmentRates).Cache.AllowUpdate = isProjectEditable;
    ((PXSelectBase) this.EquipmentRates).Cache.AllowDelete = isProjectEditable;
    nullable1 = ((PXSelectBase<PMProject>) this.Project).Current.Rejected;
    bool valueOrDefault1 = nullable1.GetValueOrDefault();
    ((PXSelectBase) this.CostBudget).Cache.SetAllEditPermissions(this.CostBudgetIsEditable() && !valueOrDefault1 && !flag4);
    ((PXSelectBase) this.RevenueBudget).Cache.SetAllEditPermissions(this.RevenueBudgetIsEditable() && !valueOrDefault1 && !flag4);
    ((PXSelectBase) this.Billing_Contact).Cache.AllowUpdate = isProjectEditable;
    ((PXSelectBase) this.Billing_Address).Cache.AllowUpdate = isProjectEditable;
    nullable1 = e.Row.SteppedRetainage;
    bool valueOrDefault2 = nullable1.GetValueOrDefault();
    ((PXSelectBase) this.RetainageSteps).AllowSelect = valueOrDefault2;
    ((PXSelectBase) this.RetainageSteps).AllowInsert = valueOrDefault2;
    ((PXSelectBase) this.RetainageSteps).AllowUpdate = valueOrDefault2;
    ((PXSelectBase) this.RetainageSteps).AllowDelete = valueOrDefault2;
    ((PXSelectBase) this.PurchaseOrders).Cache.AllowSelect = flag3;
    PXAction<PMProject> lockCommitments = this.lockCommitments;
    nullable1 = e.Row.LockCommitments;
    int num16 = !nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXAction) lockCommitments).SetEnabled(num16 != 0);
    PXAction<PMProject> unlockCommitments = this.unlockCommitments;
    nullable1 = e.Row.LockCommitments;
    int num17 = nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXAction) unlockCommitments).SetEnabled(num17 != 0);
    PXAction<PMProject> lockBudget = this.lockBudget;
    nullable1 = e.Row.BudgetFinalized;
    int num18 = !nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXAction) lockBudget).SetEnabled(num18 != 0);
    PXAction<PMProject> unlockBudget = this.unlockBudget;
    nullable1 = e.Row.BudgetFinalized;
    int num19 = nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXAction) unlockBudget).SetEnabled(num19 != 0);
    ((PXAction) this.laborCostRates).SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache.GetStatus((object) e.Row) != 2 && !flag4);
    bool flag5 = PXAccess.FeatureInstalled<FeaturesSet.paymentsByLines>();
    bool flag6 = PXAccess.FeatureInstalled<FeaturesSet.retainage>();
    PXUIFieldAttribute.SetVisible<PX.Objects.CT.Contract.retainageMode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, flag5);
    PXUIFieldAttribute.SetVisible<PX.Objects.CT.Contract.retainageMaxPct>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, flag5 && e.Row.RetainageMode == "C");
    PXUIFieldAttribute.SetVisible<PMProject.curyCapAmount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, flag5 && e.Row.RetainageMode == "C");
    PXCache cache14 = ((PXSelectBase) this.ProjectRevenueTotals).Cache;
    int num20;
    if (PXAccess.FeatureInstalled<FeaturesSet.retainage>())
    {
      nullable1 = e.Row.IncludeCO;
      num20 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num20 = 0;
    PXUIFieldAttribute.SetVisible<PMProjectRevenueTotal.curyAmount>(cache14, (object) null, num20 != 0);
    PXCache cache15 = ((PXSelectBase) this.ProjectRevenueTotals).Cache;
    int num21;
    if (PXAccess.FeatureInstalled<FeaturesSet.retainage>())
    {
      nullable1 = e.Row.IncludeCO;
      num21 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num21 = 0;
    PXUIFieldAttribute.SetVisible<PMProjectRevenueTotal.curyRevisedAmount>(cache15, (object) null, num21 != 0);
    PXCache cache16 = ((PXSelectBase) this.ProjectRevenueTotals).Cache;
    int num22;
    if (PXAccess.FeatureInstalled<FeaturesSet.retainage>())
    {
      nullable1 = e.Row.IncludeCO;
      num22 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num22 = 0;
    PXUIFieldAttribute.SetVisible<PMProjectRevenueTotal.contractCompletedPct>(cache16, (object) null, num22 != 0);
    PXCache cache17 = ((PXSelectBase) this.ProjectRevenueTotals).Cache;
    int num23;
    if (PXAccess.FeatureInstalled<FeaturesSet.retainage>())
    {
      nullable1 = e.Row.IncludeCO;
      num23 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num23 = 0;
    PXUIFieldAttribute.SetVisible<PMProjectRevenueTotal.contractCompletedWithCOPct>(cache17, (object) null, num23 != 0);
    PXCache cache18 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row14 = e.Row;
    nullable1 = e.Row.SteppedRetainage;
    int num24 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PMProject.retainagePct>(cache18, (object) row14, num24 != 0);
    PXUIFieldAttribute.SetEnabled<PMProject.steppedRetainageOption>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, e.Row.RetainageMode != "L");
    PXUIFieldAttribute.SetEnabled<PMProject.accountingMode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, PXAccess.FeatureInstalled<FeaturesSet.materialManagement>());
    PXCache cache19 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row15 = e.Row;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.CalculateProjectSpecificTaxes;
    int num25 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.revenueTaxZoneID>(cache19, (object) row15, num25 != 0);
    PXCache cache20 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row16 = e.Row;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.CalculateProjectSpecificTaxes;
    int num26 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.costTaxZoneID>(cache20, (object) row16, num26 != 0);
    PXUIFieldAttribute.SetVisible<PMProject.rateTypeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() || PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() || PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>());
    PXCache cache21 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache;
    PMProject row17 = e.Row;
    nullable1 = e.Row.ChangeOrderWorkflow;
    int num27 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMProject.includeCO>(cache21, (object) row17, num27 != 0);
    PXUIFieldAttribute.SetVisible<PMProject.includeCO>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, flag6);
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMCostBudget.committedQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedOrigAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMBudget.committedOrigQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedCOAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1 & flag3);
    PXUIFieldAttribute.SetVisible<PMBudget.committedCOQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1 & flag3 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedInvoicedAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMCostBudget.committedInvoicedQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedOpenAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMCostBudget.committedOpenQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.committedReceivedQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.curyActualPlusOpenCommittedAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMCostBudget.curyVarianceAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMCostBudget.inventoryID>(((PXSelectBase) this.CostBudget).Cache, (object) null, (PXGraph.ProxyIsActive || e.Row.CostBudgetLevel == "I" || e.Row.CostBudgetLevel == "D") && this.CostBudgetIsEditable());
    PXUIFieldAttribute.SetVisible<PMCostBudget.costCodeID>(((PXSelectBase) this.CostBudget).Cache, (object) null, (PXGraph.ProxyIsActive || e.Row.CostBudgetLevel == "C" || e.Row.CostBudgetLevel == "D") && this.CostBudgetIsEditable());
    PXUIFieldAttribute.SetVisible<PMCostBudget.revenueInventoryID>(((PXSelectBase) this.CostBudget).Cache, (object) null, (PXGraph.ProxyIsActive || e.Row.CostBudgetLevel == "I" || e.Row.CostBudgetLevel == "D") && this.CostBudgetIsEditable());
    PXUIFieldAttribute.SetVisible<PMCostBudget.revenueTaskID>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostBudgetIsEditable());
    PXUIFieldAttribute.SetVisible<PMCostBudget.curyUnitRate>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.qty>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.revisedQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.actualQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.uOM>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.isProduction>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.accountGroupID>(((PXSelectBase) this.CostBudget).Cache, (object) null, !this.IsCostGroupByTask());
    PXUIFieldAttribute.SetEnabled<PMCostBudget.curyUnitRate>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostBudgetIsEditable() && this.BudgetEditable());
    PXUIFieldAttribute.SetEnabled<PMCostBudget.qty>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostBudgetIsEditable() && this.BudgetEditable());
    PXUIFieldAttribute.SetEnabled<PMCostBudget.curyAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostBudgetIsEditable() && this.BudgetEditable());
    PXUIFieldAttribute.SetEnabled<PMCostBudget.uOM>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostBudgetIsEditable() && this.BudgetEditable());
    PXUIFieldAttribute.SetEnabled<PMCostBudget.revisedQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostBudgetIsEditable() && this.RevisedEditable());
    PXUIFieldAttribute.SetEnabled<PMCostBudget.curyRevisedAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.CostBudgetIsEditable() && this.RevisedEditable());
    PXUIFieldAttribute.SetVisible<PMCostBudget.draftChangeOrderQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.curyDraftChangeOrderAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<PMCostBudget.changeOrderQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.curyChangeOrderAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<PMBudget.curyLastCostAtCompletion>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCostAtCompletion>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyLastCostToComplete>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCostToComplete>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.lastPercentCompleted>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.percentCompleted>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetEnabled<PMBudget.curyCostAtCompletion>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetEnabled<PMBudget.curyCostToComplete>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetEnabled<PMBudget.percentCompleted>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetVisibility<PMCostBudget.revenueInventoryID>(((PXSelectBase) this.CostBudget).Cache, (object) null, e.Row.BudgetLevel == "I" || e.Row.BudgetLevel == "D" ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCommittedAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMCostBudget.committedQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCommittedCOAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1 & flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.committedCOQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1 & flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCommittedInvoicedAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMCostBudget.committedInvoicedQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCommittedOpenAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMCostBudget.committedOpenQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMCostBudget.committedReceivedQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMCostBudget.curyActualPlusOpenCommittedAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMCostBudget.curyVarianceAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMCostBudget.draftChangeOrderQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMCostBudget.curyDraftChangeOrderAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMCostBudget.changeOrderQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMCostBudget.curyChangeOrderAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyLastCostAtCompletion>(((PXSelectBase) this.CostBudget).Cache, (object) null, CostCodeAttribute.UseCostCode() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCostAtCompletion>(((PXSelectBase) this.CostBudget).Cache, (object) null, CostCodeAttribute.UseCostCode() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyLastCostToComplete>(((PXSelectBase) this.CostBudget).Cache, (object) null, CostCodeAttribute.UseCostCode() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCostToComplete>(((PXSelectBase) this.CostBudget).Cache, (object) null, CostCodeAttribute.UseCostCode() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.lastPercentCompleted>(((PXSelectBase) this.CostBudget).Cache, (object) null, CostCodeAttribute.UseCostCode() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.percentCompleted>(((PXSelectBase) this.CostBudget).Cache, (object) null, CostCodeAttribute.UseCostCode() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMTask.defaultCostCodeID>(((PXSelectBase) this.Tasks).Cache, (object) null, CostCodeAttribute.UseCostCode() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.committedQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedInvoicedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.committedInvoicedQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedOpenAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.committedOpenQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.committedReceivedQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyActualPlusOpenCommittedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.inventoryID>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueBudgetIsEditable() && (PXGraph.ProxyIsActive || e.Row.BudgetLevel == "I" || e.Row.BudgetLevel == "D"));
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.costCodeID>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueBudgetIsEditable() && (PXGraph.ProxyIsActive || e.Row.BudgetLevel == "C" || e.Row.BudgetLevel == "D"));
    PXUIFieldAttribute.SetRequired<PMRevenueBudget.inventoryID>(((PXSelectBase) this.RevenueBudget).Cache, e.Row.BudgetLevel == "I" || e.Row.BudgetLevel == "D");
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.curyUnitRate>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueBudgetIsEditable() && this.BudgetEditable());
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.qty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueBudgetIsEditable() && this.BudgetEditable());
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.curyAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueBudgetIsEditable() && this.BudgetEditable());
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.uOM>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueBudgetIsEditable() && this.BudgetEditable());
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.revisedQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueBudgetIsEditable() && this.RevisedEditable());
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.curyRevisedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueBudgetIsEditable() && this.RevisedEditable());
    PXCache cache22 = ((PXSelectBase) this.RevenueBudget).Cache;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.AutoCompleteRevenueBudget;
    int num28 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.completedPct>(cache22, (object) null, num28 != 0);
    PXCache cache23 = ((PXSelectBase) this.RevenueBudget).Cache;
    nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.AutoCompleteRevenueBudget;
    int num29 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.curyAmountToInvoice>(cache23, (object) null, num29 != 0);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.draftChangeOrderQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag1 && this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyDraftChangeOrderAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.changeOrderQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag1 && this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyChangeOrderAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyPrepaymentAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyPrepaymentInvoiced>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyPrepaymentAvailable>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.prepaymentPct>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.limitQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.maxQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.limitAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.LimitsVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyMaxAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.LimitsVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyUnitRate>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.qty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.revisedQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.actualQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.uOM>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.isProduction>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.accountGroupID>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, !this.IsRevenueGroupByTask());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.completedPct>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.taxCategoryID>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.qtyToInvoice>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.invoicedQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCommittedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.committedQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCommittedInvoicedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.committedInvoicedQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCommittedOpenAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.committedOpenQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.committedReceivedQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.curyActualPlusOpenCommittedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.draftChangeOrderQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.curyDraftChangeOrderAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.changeOrderQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.curyChangeOrderAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.curyPrepaymentAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.curyPrepaymentInvoiced>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.curyPrepaymentAvailable>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.prepaymentPct>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.limitQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.maxQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.limitAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.LimitsVisible() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.curyMaxAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.LimitsVisible() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXCache cache24 = ((PXSelectBase) this.RevenueBudget).Cache;
    int num30;
    if (e.Row.RetainageMode != "C")
    {
      nullable1 = e.Row.SteppedRetainage;
      num30 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num30 = 0;
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.retainagePct>(cache24, (object) null, num30 != 0);
    PXCache cache25 = ((PXSelectBase) this.RevenueBudget).Cache;
    int num31;
    if (e.Row.RetainageMode != "C")
    {
      nullable1 = e.Row.SteppedRetainage;
      if (!nullable1.GetValueOrDefault())
      {
        num31 = 3;
        goto label_27;
      }
    }
    num31 = 1;
label_27:
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.retainagePct>(cache25, (object) null, (PXUIVisibility) num31);
    PXUIFieldAttribute.SetVisible<PMBudget.curyDraftRetainedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag5);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyDraftRetainedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag5 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<PMBudget.curyRetainedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag5);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyRetainedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag5 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<PMBudget.curyTotalRetainedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag5);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyTotalRetainedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag5 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<PMBudget.retainageMaxPct>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, e.Row.RetainageMode == "L");
    PXUIFieldAttribute.SetVisibility<PMBudget.retainageMaxPct>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, e.Row.RetainageMode == "L" ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyCapAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, e.Row.RetainageMode == "L");
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.curyCapAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, e.Row.RetainageMode == "L" ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetEnabled<PMOtherBudget.curyUnitRate>(((PXSelectBase) this.OtherBudget).Cache, (object) null, this.BudgetEditable());
    PXUIFieldAttribute.SetEnabled<PMOtherBudget.qty>(((PXSelectBase) this.OtherBudget).Cache, (object) null, this.BudgetEditable());
    PXUIFieldAttribute.SetEnabled<PMOtherBudget.curyAmount>(((PXSelectBase) this.OtherBudget).Cache, (object) null, this.BudgetEditable());
    PXUIFieldAttribute.SetEnabled<PMOtherBudget.uOM>(((PXSelectBase) this.OtherBudget).Cache, (object) null, this.BudgetEditable());
    PXUIFieldAttribute.SetVisible<PMProjectBalanceRecord.curyCommittedAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMProjectBalanceRecord.curyCommittedInvoicedAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMProjectBalanceRecord.curyCommittedOpenAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMProjectBalanceRecord.curyActualPlusOpenCommittedAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMProjectBalanceRecord.curyVarianceAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMProjectBalanceRecord.curyDraftCOAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<PMProjectBalanceRecord.curyBudgetedCOAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<PMProjectBalanceRecord.curyCommittedCOAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3 & flag1);
    PXUIFieldAttribute.SetVisible<PMProjectBalanceRecord.curyOriginalCommittedAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisibility<PMProjectBalanceRecord.curyCommittedAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMProjectBalanceRecord.curyCommittedInvoicedAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMProjectBalanceRecord.curyCommittedOpenAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMProjectBalanceRecord.curyActualPlusOpenCommittedAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMProjectBalanceRecord.curyVarianceAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMProjectBalanceRecord.curyDraftCOAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMProjectBalanceRecord.curyBudgetedCOAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMProjectBalanceRecord.curyCommittedCOAmount>(((PXSelectBase) this.BalanceRecords).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXDefaultAttribute.SetPersistingCheck<PMProject.dropshipExpenseSubMask>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, !PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() || !PXAccess.FeatureInstalled<FeaturesSet.subAccount>() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      PXStringListAttribute.SetList<PMProject.dropshipExpenseAccountSource>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, new string[3]
      {
        "O",
        "P",
        "T"
      }, new string[3]{ "Item", "Project", "Task" });
    nullable2 = e.Row.CustomerID;
    if (!nullable2.HasValue)
      return;
    PX.Objects.PM.PMAddress pmAddress = (PX.Objects.PM.PMAddress) PXResultset<PMBillingAddress>.op_Implicit(((PXSelectBase<PMBillingAddress>) this.Billing_Address).Select(Array.Empty<object>()));
    PMSiteAddress pmSiteAddress = PXResultset<PMSiteAddress>.op_Implicit(((PXSelectBase<PMSiteAddress>) this.Site_Address).Select(Array.Empty<object>()));
    int num32;
    if (pmAddress != null)
    {
      nullable1 = pmAddress.IsDefaultAddress;
      bool flag7 = false;
      if (nullable1.GetValueOrDefault() == flag7 & nullable1.HasValue)
      {
        nullable1 = pmAddress.IsValidated;
        bool flag8 = false;
        if (nullable1.GetValueOrDefault() == flag8 & nullable1.HasValue)
        {
          num32 = 1;
          goto label_38;
        }
      }
    }
    if (pmSiteAddress != null)
    {
      nullable1 = pmSiteAddress.IsDefaultAddress;
      bool flag9 = false;
      if (nullable1.GetValueOrDefault() == flag9 & nullable1.HasValue)
      {
        nullable1 = pmSiteAddress.IsValidated;
        bool flag10 = false;
        num32 = nullable1.GetValueOrDefault() == flag10 & nullable1.HasValue ? 1 : 0;
        goto label_38;
      }
    }
    num32 = 0;
label_38:
    ((PXAction) this.validateAddresses).SetEnabled(num32 != 0);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMProject> e)
  {
    PXSelect<NotificationSetup, Where<NotificationSetup.module, Equal<BatchModule.modulePM>>> pxSelect = new PXSelect<NotificationSetup, Where<NotificationSetup.module, Equal<BatchModule.modulePM>>>((PXGraph) this);
    bool isDirty = ((PXSelectBase) this.NotificationSources).Cache.IsDirty;
    object[] objArray = Array.Empty<object>();
    foreach (PXResult<NotificationSetup> pxResult in ((PXSelectBase<NotificationSetup>) pxSelect).Select(objArray))
    {
      NotificationSetup notificationSetup = PXResult<NotificationSetup>.op_Implicit(pxResult);
      ((PXSelectBase<NotificationSource>) this.NotificationSources).Insert(new NotificationSource()
      {
        SetupID = notificationSetup.SetupID,
        Active = notificationSetup.Active,
        EMailAccountID = notificationSetup.EMailAccountID,
        NotificationID = notificationSetup.NotificationID,
        ReportID = notificationSetup.ReportID,
        Format = notificationSetup.Format
      });
    }
    ((PXSelectBase) this.NotificationSources).Cache.IsDirty = isDirty;
    if (e.Row == null)
      return;
    ((PXSelectBase<ContractBillingSchedule>) this.Billing).Insert(new ContractBillingSchedule()
    {
      ContractID = e.Row.ContractID
    });
    ((PXSelectBase) this.Billing).Cache.IsDirty = false;
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMRevenueBudget> e)
  {
    this.ImportFromExcelRowInserted(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PMRevenueBudget>>) e).Cache, e.Row);
    this.UpdateBudgetHistoryLine((PMBudget) e.Row, 1);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMCostBudget> e)
  {
    this.ImportFromExcelRowInserted(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PMCostBudget>>) e).Cache, e.Row);
    this.UpdateBudgetHistoryLine((PMBudget) e.Row, 1);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMOtherBudget> e)
  {
    this.ImportFromExcelRowInserted(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PMOtherBudget>>) e).Cache, e.Row);
  }

  [Obsolete]
  protected virtual void _(PX.Data.Events.RowDeleting<PMProject> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMProject> e)
  {
    if (!string.IsNullOrEmpty(e.Row.QuoteNbr))
    {
      PMQuote pmQuote = PXResultset<PMQuote>.op_Implicit(PXSelectBase<PMQuote, PXSelect<PMQuote, Where<PMQuote.quoteNbr, Equal<Required<PMQuote.quoteNbr>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) e.Row.QuoteNbr
      }));
      if (pmQuote != null)
      {
        pmQuote.QuoteProjectID = new int?();
        pmQuote.Status = "A";
        ((PXSelectBase<PMQuote>) this.Quote).Update(pmQuote);
        foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) new PXSelect<CROpportunityProducts, Where<CROpportunityProducts.quoteID, Equal<Required<CROpportunityProducts.quoteID>>>>((PXGraph) this)).Select(new object[1]
        {
          (object) pmQuote.QuoteID
        }))
        {
          CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
          opportunityProducts.ProjectID = new int?();
          opportunityProducts.TaskID = new int?();
          ((PXSelectBase<CROpportunityProducts>) this.QuoteDetails).Update(opportunityProducts);
        }
      }
    }
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      pmTask.AllocationID = (string) null;
      pmTask.BillingID = (string) null;
      pmTask.RateTableID = (string) null;
      pmTask.TaxCategoryID = (string) null;
      ((PXSelectBase<PMTask>) this.Tasks).Update(pmTask);
    }
  }

  protected virtual void _(PX.Data.Events.RowInserted<NotificationSource> e)
  {
    bool isDirty = ((PXSelectBase) this.NotificationRecipients).Cache.IsDirty;
    foreach (PXResult<NotificationSetupRecipient> pxResult in ((PXSelectBase<NotificationSetupRecipient>) new PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Required<NotificationSetupRecipient.setupID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) e.Row.SetupID
    }))
    {
      NotificationSetupRecipient notificationSetupRecipient = PXResult<NotificationSetupRecipient>.op_Implicit(pxResult);
      ((PXSelectBase<NotificationRecipient>) this.NotificationRecipients).Insert(new NotificationRecipient()
      {
        SetupID = notificationSetupRecipient.SetupID,
        Active = notificationSetupRecipient.Active,
        ContactID = notificationSetupRecipient.ContactID,
        AddTo = notificationSetupRecipient.AddTo,
        ContactType = notificationSetupRecipient.ContactType,
        Format = e.Row.Format
      });
    }
    ((PXSelectBase) this.NotificationRecipients).Cache.IsDirty = isDirty;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.customerID> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) new PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) e.Row.CustomerID
    }));
    if (string.IsNullOrEmpty(e.Row.QuoteNbr) && customer != null)
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>() && !this.ProjectHasTransactions())
      {
        if (!string.IsNullOrEmpty(customer.CuryID))
          ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.customerID>>) e).Cache.SetValueExt<PMProject.curyIDCopy>((object) e.Row, (object) customer.CuryID);
        else
          ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.customerID>>) e).Cache.SetDefaultExt<PMProject.curyIDCopy>((object) e.Row);
      }
      if (!string.IsNullOrEmpty(customer.CuryRateTypeID))
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.customerID>>) e).Cache.SetValueExt<PMProject.rateTypeID>((object) e.Row, (object) customer.CuryRateTypeID);
      else
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.customerID>>) e).Cache.SetDefaultExt<PMProject.rateTypeID>((object) e.Row);
    }
    try
    {
      SharedRecordAttribute.DefaultRecord<PMProject.billAddressID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.customerID>>) e).Cache, (object) e.Row);
      SharedRecordAttribute.DefaultRecord<PMProject.billContactID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.customerID>>) e).Cache, (object) e.Row);
    }
    catch (PXFieldValueProcessingException ex)
    {
      throw new PXException(((Exception) ex).Message);
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.customerID>>) e).Cache.SetDefaultExt<PMProject.termsID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.customerID>>) e).Cache.SetDefaultExt<PMProject.locationID>((object) e.Row);
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      ((PXSelectBase) this.Tasks).Cache.SetDefaultExt<PMTask.customerID>((object) pmTask);
      ((PXSelectBase) this.Tasks).Cache.SetDefaultExt<PMTask.locationID>((object) pmTask);
      ((PXSelectBase<PMTask>) this.Tasks).Update(pmTask);
    }
    int? customerId;
    if (((PXSelectBase<ContractBillingSchedule>) this.Billing).Current != null)
    {
      customerId = e.Row.CustomerID;
      if (!customerId.HasValue)
      {
        ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.Type = (string) null;
        ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.NextDate = new DateTime?();
        ((PXSelectBase<ContractBillingSchedule>) this.Billing).Update(((PXSelectBase<ContractBillingSchedule>) this.Billing).Current);
      }
    }
    if (((PXSelectBase<ContractBillingSchedule>) this.Billing).Current != null)
    {
      customerId = e.Row.CustomerID;
      if (customerId.HasValue && ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.Type == null)
      {
        ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.Type = "M";
        ((PXSelectBase<ContractBillingSchedule>) this.Billing).Update(((PXSelectBase<ContractBillingSchedule>) this.Billing).Current);
      }
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.retainage>() && customer != null)
    {
      Decimal? retainagePct1 = e.Row.RetainagePct;
      e.Row.RetainagePct = customer.RetainagePct;
      List<PMRevenueBudget> pmRevenueBudgetList = new List<PMRevenueBudget>();
      foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()))
      {
        PMRevenueBudget pmRevenueBudget = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
        Decimal? retainagePct2 = pmRevenueBudget.RetainagePct;
        Decimal? retainagePct3 = ((PXSelectBase<PMProject>) this.Project).Current.RetainagePct;
        if (!(retainagePct2.GetValueOrDefault() == retainagePct3.GetValueOrDefault() & retainagePct2.HasValue == retainagePct3.HasValue))
          pmRevenueBudgetList.Add(pmRevenueBudget);
      }
      if (pmRevenueBudgetList.Count > 0)
      {
        if (((PXSelectBase<PMProject>) this.Project).Ask("Default Retainage (%) Changed", PXMessages.LocalizeFormatNoPrefix("Changing Customer will update the default project Retainage (%) from {0:f} to {1:f}. Would you also like to update Retainage (%) in the revenue budget lines?", new object[2]
        {
          (object) retainagePct1,
          (object) ((PXSelectBase<PMProject>) this.Project).Current.RetainagePct
        }), (MessageButtons) 4, (MessageIcon) 2) == 6)
        {
          foreach (PMRevenueBudget pmRevenueBudget in pmRevenueBudgetList)
          {
            pmRevenueBudget.RetainagePct = ((PXSelectBase<PMProject>) this.Project).Current.RetainagePct;
            ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Update(pmRevenueBudget);
          }
        }
      }
    }
    ((PXSelectBase) this.Site_Address).Cache.SetDefaultExt<PMSiteAddress.countryID>(((PXSelectBase) this.Site_Address).Cache.Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.createProforma> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.createProforma>>) e).Cache.GetStatus((object) e.Row) == 2)
      return;
    if (PXResultset<PMProforma>.op_Implicit(((PXSelectBase<PMProforma>) new PXSelect<PMProforma, Where<PMProforma.projectID, Equal<Required<PMProforma.projectID>>, And<PMProforma.released, Equal<False>>>>((PXGraph) this)).SelectWindowed(0, 1, new object[1]
    {
      (object) e.Row.ContractID
    })) != null)
      throw new PXSetPropertyException("All existing pro forma and Accounts Receivable invoices of the project have to be released before changing this setting.");
    if (PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) new PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.projectID, Equal<Required<PMProforma.projectID>>, And<PX.Objects.AR.ARInvoice.released, Equal<False>, And<PX.Objects.AR.ARInvoice.scheduled, Equal<False>, And<PX.Objects.AR.ARInvoice.voided, Equal<False>>>>>>((PXGraph) this)).SelectWindowed(0, 1, new object[1]
    {
      (object) e.Row.ContractID
    })) != null)
      throw new PXSetPropertyException("All existing pro forma and Accounts Receivable invoices of the project have to be released before changing this setting.");
    if (!((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.createProforma>, PMProject, object>) e).NewValue).GetValueOrDefault() && e.Row.RetainageMode == "C")
      throw new PXSetPropertyException<PX.Objects.CT.Contract.createProforma>("To enable the creation of pro forma invoices, the retainage mode must be changed first.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.changeOrderWorkflow> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.changeOrderWorkflow>>) e).Cache.GetStatus((object) e.Row) == 2 || !((PXSelectBase<PMSetup>) this.Setup).Current.CostCommitmentTracking.GetValueOrDefault())
      return;
    PXSelect<PX.Objects.PO.POLine, Where<PX.Objects.PO.POLine.projectID, Equal<Required<PX.Objects.PO.POLine.projectID>>, And<Where<PX.Objects.PO.POLine.cancelled, Equal<False>, And<PX.Objects.PO.POLine.completed, Equal<False>, And<PX.Objects.PO.POLine.closed, Equal<False>>>>>>> pxSelect1 = new PXSelect<PX.Objects.PO.POLine, Where<PX.Objects.PO.POLine.projectID, Equal<Required<PX.Objects.PO.POLine.projectID>>, And<Where<PX.Objects.PO.POLine.cancelled, Equal<False>, And<PX.Objects.PO.POLine.completed, Equal<False>, And<PX.Objects.PO.POLine.closed, Equal<False>>>>>>>((PXGraph) this);
    PXSelect<PMChangeOrder, Where<PMChangeOrder.projectID, Equal<Required<PMChangeOrder.projectID>>>> pxSelect2 = new PXSelect<PMChangeOrder, Where<PMChangeOrder.projectID, Equal<Required<PMChangeOrder.projectID>>>>((PXGraph) this);
    PXSelect<PMChangeRequest, Where<PMChangeRequest.projectID, Equal<Required<PMChangeRequest.projectID>>>> pxSelect3 = new PXSelect<PMChangeRequest, Where<PMChangeRequest.projectID, Equal<Required<PMChangeRequest.projectID>>>>((PXGraph) this);
    if (((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.changeOrderWorkflow>, PMProject, object>) e).NewValue).GetValueOrDefault())
    {
      if (((IQueryable<PXResult<PX.Objects.PO.POLine>>) ((PXSelectBase<PX.Objects.PO.POLine>) pxSelect1).SelectWindowed(0, 1, new object[1]
      {
        (object) e.Row.ContractID
      })).Any<PXResult<PX.Objects.PO.POLine>>())
        throw new PXSetPropertyException("Before you enable the change order workflow for the project, please make sure that all the related purchase order lines of the project have one of the following statuses: Completed, Closed, or Canceled.");
    }
    else
    {
      if (((IQueryable<PXResult<PMChangeOrder>>) ((PXSelectBase<PMChangeOrder>) pxSelect2).SelectWindowed(0, 1, new object[1]
      {
        (object) e.Row.ContractID
      })).Any<PXResult<PMChangeOrder>>())
        throw new PXSetPropertyException("Before canceling change order workflow for the project, please make sure that the project has no related change orders.");
      if (((IQueryable<PXResult<PMChangeRequest>>) ((PXSelectBase<PMChangeRequest>) pxSelect3).SelectWindowed(0, 1, new object[1]
      {
        (object) e.Row.ContractID
      })).Any<PXResult<PMChangeRequest>>())
        throw new PXSetPropertyException("Before canceling change order workflow for the project, please make sure that the project has no related change requests.");
      if (((IQueryable<PXResult<PX.Objects.PO.POLine>>) ((PXSelectBase<PX.Objects.PO.POLine>) pxSelect1).SelectWindowed(0, 1, new object[1]
      {
        (object) e.Row.ContractID
      })).Any<PXResult<PX.Objects.PO.POLine>>())
        throw new PXSetPropertyException("Before canceling change order workflow for the project, please make sure that the project has no related non-canceled purchase order lines.");
    }
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    int num;
    if (values.Contains((object) "templateID") && values[(object) "templateID"] != null && (((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase) this.Project).Cache.GetStatus((object) ((PXSelectBase<PMProject>) this.Project).Current) == 2 && !this.IsCopyPaste && ((PXSelectBase<PMProject>) this.Project).Current.QuoteNbr == null || ((PXGraph) this).IsMobile))
    {
      int? templateId1 = ((PXSelectBase<PMProject>) this.Project).Current.TemplateID;
      try
      {
        this.SuppressTemplateIDUpdated = true;
        num = ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
      }
      finally
      {
        this.SuppressTemplateIDUpdated = false;
      }
      int? templateId2 = ((PXSelectBase<PMProject>) this.Project).Current.TemplateID;
      if (templateId2.HasValue)
      {
        int? nullable1 = templateId2;
        int? nullable2 = templateId1;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          if (!((PXGraph) this).IsMobile && !((PXGraph) this).IsImport && this.IsLargeTemplate(templateId2.Value))
          {
            ((PXSelectBase<ProjectEntry.LoadFromTemplateInfo>) this.LoadFromTemplateDialog).Current.TemplateID = templateId2;
            ((PXSelectBase<ProjectEntry.LoadFromTemplateInfo>) this.LoadFromTemplateDialog).AskExt();
          }
          else
          {
            this._isLoadFromTemplate = false;
            this.DefaultFromTemplate(((PXSelectBase<PMProject>) this.Project).Current, templateId2, ProjectEntry.DefaultFromTemplateSettings.Default);
          }
        }
      }
    }
    else
    {
      if (viewName == "RevenueBudget" && values.Contains((object) "CompletedPct") && values[(object) "CompletedPct"] != PXCache.NotSetValue)
        values[(object) "Mode"] = (object) "M";
      num = ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
    }
    return num;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.templateID> e)
  {
    if (this.SuppressTemplateIDUpdated || this.IsCopyPaste)
      return;
    this.DefaultFromTemplate(e.Row, e.Row.TemplateID, ProjectEntry.DefaultFromTemplateSettings.Default);
  }

  private bool IsLargeTemplate(int templateID)
  {
    int num1 = ((PXSelectBase<PMSetup>) this.Setup).Current.LargeProjectTemplateSize ?? 1000;
    int num2 = ((IQueryable<PXResult<PMTask>>) PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) templateID
    })).Count<PXResult<PMTask>>();
    if (num2 >= num1)
      return true;
    int num3 = ((IQueryable<PXResult<PMBudget>>) PXSelectBase<PMBudget, PXSelect<PMBudget, Where<PMBudget.projectID, Equal<Required<PMBudget.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) templateID
    })).Count<PXResult<PMBudget>>();
    return num2 + num3 >= num1;
  }

  [PXUIField]
  [PXButton(ImageKey = "Process")]
  public virtual IEnumerable LoadFromTemplate(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ProjectEntry.\u003C\u003Ec__DisplayClass261_0 displayClass2610 = new ProjectEntry.\u003C\u003Ec__DisplayClass261_0();
    // ISSUE: reference to a compiler-generated field
    displayClass2610.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass2610.templ = PMProject.PK.Find((PXGraph) this, ((PXSelectBase<ProjectEntry.LoadFromTemplateInfo>) this.LoadFromTemplateDialog).Current.TemplateID);
    // ISSUE: reference to a compiler-generated field
    if (displayClass2610.templ != null)
    {
      if (ProjectEntry.DefaultFromTemplateSettings.Default.CopyProperties)
      {
        // ISSUE: reference to a compiler-generated field
        this.DefaultFromTemplateProjectSettings(((PXSelectBase<PMProject>) this.Project).Current, displayClass2610.templ);
      }
      ((PXSelectBase<PMProject>) this.Project).Update(((PXSelectBase<PMProject>) this.Project).Current);
      string contractCd = ((PXSelectBase<PMProject>) this.Project).Current.ContractCD;
      ((PXAction) this.Save).Press();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass2610, __methodptr(\u003CLoadFromTemplate\u003Eb__0)));
      if (((PXSelectBase<PMProject>) this.Project).Current.ContractCD != contractCd)
        throw new PXRedirectRequiredException((PXGraph) this, (string) null);
    }
    return adapter.Get();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.locationID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.locationID>>) e).Cache.SetDefaultExt<PMProject.defaultSalesSubID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.ownerID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.ownerID>>) e).Cache.SetDefaultExt<PMProject.approverID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.projectGroupID> e)
  {
    this.ProjectGroupMaskHelper.UpdateProjectMaskFromProjectGroup(e.Row, (string) e.NewValue, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.projectGroupID>>) e).Cache);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.approverID> e)
  {
    PX.Objects.CR.Standalone.EPEmployee epEmployee = PXResultset<PX.Objects.CR.Standalone.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.EPEmployee, PXSelect<PX.Objects.CR.Standalone.EPEmployee, Where<PX.Objects.CR.Standalone.EPEmployee.defContactID, Equal<Current<PMProject.ownerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (epEmployee == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.approverID>, PMProject, object>) e).NewValue = (object) epEmployee.BAccountID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.contractCD> e)
  {
    this.OnProjectCDChanged();
  }

  private bool ProjectHasTransactions()
  {
    using (new PXReadBranchRestrictedScope())
      return ((IQueryable<PXResult<PMTran>>) ((PXSelectBase<PMTran>) new PXSelectReadonly<PMTran, Where<PMTran.projectID, Equal<Current<PMProject.contractID>>>>((PXGraph) this)).SelectWindowed(0, 1, Array.Empty<object>())).Any<PXResult<PMTran>>();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.costBudgetLevel> e)
  {
    if (CostCodeAttribute.UseCostCode())
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.costBudgetLevel>, PMProject, object>) e).NewValue = (object) "C";
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.costBudgetLevel>, PMProject, object>) e).NewValue = (object) "I";
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.billingCuryID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.billingCuryID>, PMProject, object>) e).NewValue = (object) e.Row.CuryIDCopy;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PMProject.billingCuryID> e)
  {
    if (e.Row != null && !string.IsNullOrEmpty(e.Row.BaseCuryID) && e.Row.CuryIDCopy != e.Row.BaseCuryID && ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.billingCuryID>, PMProject, object>) e).NewValue as string != e.Row.CuryIDCopy)
      throw new PXSetPropertyException<PMProject.billingCuryID>("Another billing currency is not supported because the project currency {0} differs from the base currency {1}.", new object[2]
      {
        (object) e.Row.CuryIDCopy,
        (object) e.Row.BaseCuryID
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PMProject.retainagePct> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.retainagePct>, PMProject, object>) e).NewValue == null)
      return;
    Decimal newValue = (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.retainagePct>, PMProject, object>) e).NewValue;
    if (newValue < 0M || newValue > 100M)
      throw new PXSetPropertyException<PMProject.retainagePct>("Percentage value should be between 0 and 100");
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.accountingMode> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.accountingMode>, PMProject, object>) e).NewValue = PXAccess.FeatureInstalled<FeaturesSet.materialManagement>() ? (object) "P" : (object) "L";
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PMProject.accountingMode> e)
  {
    if (e.Row == null)
      return;
    this.VerifyModeForLinkedLocations((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.accountingMode>, PMProject, object>) e).NewValue);
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.accountingMode>, PMProject, object>) e).NewValue == e.OldValue)
      return;
    this.VerifyNoItemsOnHand();
  }

  private void VerifyModeForLinkedLocations(string newMode)
  {
    if (((PXSelectBase<INLocation>) new PXSelect<INLocation, Where<INLocation.projectID, Equal<Current<PMProject.contractID>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>()) != null && newMode != "L")
      throw new PXSetPropertyException<PMProject.accountingMode>("You cannot change the inventory tracking method because there is at least one warehouse location linked to the project. You must unlink all warehouse locations from the project on the Warehouses (IN204000) form first. See the trace log for details.");
  }

  private void VerifyNoItemsOnHand()
  {
    if (((PXSelectBase<INSiteStatusByCostCenter>) new PXSelectJoin<INSiteStatusByCostCenter, InnerJoin<INCostCenter, On<INCostCenter.costCenterID, Equal<INSiteStatusByCostCenter.costCenterID>>>, Where<INCostCenter.projectID, Equal<Current<PMProject.contractID>>, And<INSiteStatusByCostCenter.qtyOnHand, NotEqual<decimal0>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>()) != null)
      throw new PXSetPropertyException<PMProject.accountingMode>("You cannot change the inventory tracking method because there are items on hand in warehouse locations linked to the project. You must issue or transfer all items from these warehouse locations and unlink all warehouse locations from the project first. See the trace log for details.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.retainagePct> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.retainagePct>>) e).Cache.SetDefaultExt<PMProject.curyCapAmount>((object) e.Row);
    if (!PXAccess.FeatureInstalled<FeaturesSet.retainage>())
      return;
    ((PXAction) this.updateRetainage).Press();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate> e)
  {
    if (((PXSelectBase) this.Project).Cache.Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) ((PXSelectBase<PMProject>) this.Project).Current.StartDate;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID> e)
  {
    if (!this.IsCopyPaste || ((PXSelectBase<PMProject>) this.CopySource.Project).Current == null || string.IsNullOrEmpty(((PXSelectBase<PMProject>) this.CopySource.Project).Current.RateTypeID))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) ((PXSelectBase<PMProject>) this.CopySource.Project).Current.RateTypeID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.startDate> e)
  {
    if (e.Row != null && ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current != null && PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>() && !CurrencyCollection.IsBaseCuryInfo(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current))
    {
      ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current.CuryEffDate = e.Row.StartDate;
      ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Update(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current);
      this.ShowWaringOnProjectCurrecyIfExcahngeRateNotFound(e.Row);
    }
    this.BudgetHistoryUpdateDate((DateTime) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProject, PMProject.startDate>, PMProject, object>) e).OldValue, (DateTime) e.NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.rateTypeID> e)
  {
    if (e.Row == null || ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current == null || !PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>() || CurrencyCollection.IsBaseCuryInfo(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current))
      return;
    ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current.CuryRateTypeID = e.Row.RateTypeID ?? ((PXSelectBase<PX.Objects.CM.CMSetup>) this.CMSetup).Current.PMRateTypeDflt;
    ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Update(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current);
    if (!e.Row.StartDate.HasValue)
      return;
    this.ShowWaringOnProjectCurrecyIfExcahngeRateNotFound(e.Row);
  }

  private void ShowWaringOnProjectCurrecyIfExcahngeRateNotFound(PMProject row)
  {
    if (((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current == null)
      return;
    PXUIFieldAttribute.SetError<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>(((PXSelectBase) this.CuryInfo).Cache, (object) ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current, (string) null);
    ((PXSelectBase) this.CuryInfo).Cache.RaiseFieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>((object) ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current, (object) row.StartDate);
    if (string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>(((PXSelectBase) this.CuryInfo).Cache, (object) ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current)))
      return;
    ((PXSelectBase) this.Project).Cache.RaiseExceptionHandling<PMProject.curyIDCopy>((object) row, (object) null, (Exception) new PXSetPropertyException("Please define a conversion rate from the {0} to {1} currency within the {2} currency rate type and the {3:d} effective date on the Currency Rates (CM301000) form.", (PXErrorLevel) 2, new object[4]
    {
      (object) ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current.CuryID,
      (object) ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current.BaseCuryID,
      (object) ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current.CuryRateTypeID,
      (object) ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Current.CuryEffDate
    }));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PX.Objects.CT.Contract.steppedRetainage> e)
  {
    if (this.IsCopyPaste || !e.Row.SteppedRetainage.GetValueOrDefault() || ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Select(Array.Empty<object>()).Count != 0)
      return;
    PMRetainageStep pmRetainageStep = ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Insert();
    pmRetainageStep.ThresholdPct = new Decimal?(0M);
    pmRetainageStep.RetainagePct = e.Row.RetainagePct;
    ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Update(pmRetainageStep);
    this.SyncRetainage();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.retainageMode> e)
  {
    if ((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.retainageMode>, PMProject, object>) e).NewValue == "C" && !e.Row.CreateProforma.GetValueOrDefault())
      throw new PXSetPropertyException<PX.Objects.CT.Contract.retainageMode>("To select the Contract Cap mode, the creation of pro forma invoices must be enabled first.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PX.Objects.CT.Contract.retainageMode> e)
  {
    if (e.Row.RetainageMode == "L")
      e.Row.SteppedRetainage = new bool?(false);
    if (!(e.Row.RetainageMode == "C"))
      return;
    this.SyncRetainage();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.curyCapAmount> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.curyCapAmount>, PMProject, object>) e).NewValue = (object) this.CalculateCapAmount(e.Row, (PMProjectRevenueTotal) ((PXSelectBase) this.ProjectRevenueTotals).View.SelectSingleBound(new object[1]
    {
      (object) e.Row
    }, Array.Empty<object>()));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PX.Objects.CT.Contract.retainageMaxPct> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PX.Objects.CT.Contract.retainageMaxPct>>) e).Cache.SetDefaultExt<PMProject.curyCapAmount>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.includeCO> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.includeCO>>) e).Cache.SetDefaultExt<PMProject.curyCapAmount>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.baseCuryID> e)
  {
    if (e.Row == null || this.IsCopyPaste)
      return;
    PX.Objects.GL.Branch branch = (PX.Objects.GL.Branch) null;
    if (e.Row.DefaultBranchID.HasValue)
      branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, e.Row.DefaultBranchID);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.baseCuryID>, PMProject, object>) e).NewValue = (object) (branch?.BaseCuryID ?? ((PXGraph) this).Accessinfo.BaseCuryID ?? ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.defaultBranchID> e)
  {
    if (!this.IsCopyPaste)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.defaultBranchID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PMProject.baseCuryID> e)
  {
    string newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.baseCuryID>, PMProject, object>) e).NewValue as string;
    if (e.Row == null || string.IsNullOrEmpty(e.Row.BaseCuryID) || !(e.Row.BaseCuryID != newValue) || string.IsNullOrEmpty(newValue))
      return;
    if (e.Row.DefaultBranchID.HasValue)
    {
      PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, e.Row.DefaultBranchID);
      if (branch != null && branch.BaseCuryID != ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.baseCuryID>, PMProject, object>) e).NewValue as string)
        throw new PXSetPropertyException<PMProject.baseCuryID>("You cannot select a currency different from the currency of branch.");
    }
    if (this.ProjectHasTransactions())
      throw new PXSetPropertyException<PMProject.baseCuryID>("You cannot change the base currency for the project that has project transactions.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.baseCuryID> e)
  {
    if (e.Row == null)
      return;
    string str = e.Row.BaseCuryID ?? ((PXGraph) this).Accessinfo.BaseCuryID ?? ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Search<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>((object) e.Row.CuryInfoID, Array.Empty<object>()));
    if (currencyInfo1 != null && currencyInfo1.BaseCuryID != str)
    {
      if (CurrencyCollection.IsBaseCuryInfo(currencyInfo1))
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXGraph) this).GetExtension<ProjectEntry.MultiCurrency>().CloneCurrencyInfo(currencyInfo1);
        currencyInfo2.BaseCuryID = str;
        currencyInfo2.CuryEffDate = ((PXGraph) this).Accessinfo.BusinessDate;
        e.Row.CuryInfoID = (long?) currencyInfo2?.CuryInfoID;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CuryInfo).Update(currencyInfo2);
        ((PXSelectBase<PX.Objects.Extensions.MultiCurrency.Document>) ((PXGraph) this).GetExtension<ProjectEntry.MultiCurrency>().Documents).Current.CuryInfoID = (long?) currencyInfo3?.CuryInfoID;
      }
      else
      {
        currencyInfo1.BaseCuryID = str;
        currencyInfo1.CuryEffDate = new DateTime?(DateTime.MinValue);
        ((PXSelectBase) this.CuryInfo).Cache.Update((object) currencyInfo1);
        currencyInfo1.CuryEffDate = ((PXGraph) this).Accessinfo.BusinessDate;
        ((PXSelectBase) this.CuryInfo).Cache.Update((object) currencyInfo1);
      }
      this.ShowWaringOnProjectCurrecyIfExcahngeRateNotFound(e.Row);
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.baseCuryID>>) e).Cache.SetDefaultExt<PMProject.curyIDCopy>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PMProject.defaultBranchID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.defaultBranchID>, PMProject, object>) e).NewValue == null || !(e.Row.BaseCuryID != ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.defaultBranchID>, PMProject, object>) e).NewValue as string))
      return;
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.defaultBranchID>, PMProject, object>) e).NewValue);
    if (branch.BaseCuryID != e.Row.BaseCuryID && this.ProjectHasTransactions())
      throw new PXSetPropertyException("You cannot select a branch with the currency different from {0} for the project.", new object[1]
      {
        (object) e.Row.BaseCuryID
      })
      {
        ErrorValue = (object) branch.BranchCD
      };
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.defaultBranchID> e)
  {
    if (e.Row == null || !e.Row.DefaultBranchID.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.defaultBranchID>>) e).Cache.SetDefaultExt<PMProject.baseCuryID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPEmployeeContract> e)
  {
    ((PXSelectBase) this.ContractRates).AllowInsert = e.Row != null;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<EPEmployeeContract> e)
  {
    if (e.OldRow == null)
      return;
    foreach (PXResult<EPContractRate> pxResult in PXSelectBase<EPContractRate, PXSelect<EPContractRate, Where<EPContractRate.contractID, Equal<Current<PMProject.contractID>>, And<EPContractRate.employeeID, Equal<Required<EPContractRate.employeeID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.OldRow.EmployeeID
    }))
    {
      EPContractRate epContractRate = PXResult<EPContractRate>.op_Implicit(pxResult);
      epContractRate.EmployeeID = e.Row.EmployeeID;
      ((PXSelectBase<EPContractRate>) this.ContractRates).Update(epContractRate);
    }
    EPContractRate.UpdateKeyFields((PXGraph) this, e.OldRow.ContractID, e.OldRow.EmployeeID, e.Row.ContractID, e.Row.EmployeeID);
  }

  protected virtual void EPEmployeeContract_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is EPEmployeeContract) || !((PXGraph) this).IsMobile)
      return;
    List<object> objectList = new List<object>();
    foreach (object obj in sender.Inserted)
    {
      if (!((EPEmployeeContract) obj).EmployeeID.HasValue)
        objectList.Add(obj);
    }
    foreach (object obj in objectList)
      sender.Delete(obj);
  }

  protected virtual void EPEmployeeContract_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (!(e.Row is EPEmployeeContract row) || !((PXGraph) this).IsMobile)
      return;
    List<object> objectList = new List<object>();
    foreach (object obj in sender.Inserted)
    {
      int? employeeId1 = ((EPEmployeeContract) obj).EmployeeID;
      int? employeeId2 = row.EmployeeID;
      if (employeeId1.GetValueOrDefault() == employeeId2.GetValueOrDefault() & employeeId1.HasValue == employeeId2.HasValue)
        objectList.Add(obj);
    }
    foreach (object obj in objectList)
      sender.Delete(obj);
  }

  protected virtual void EPEmployeeContract_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EPEmployeeContract row) || !((PXGraph) this).IsMobile || e.Operation == 3 || row.EmployeeID.HasValue)
      return;
    sender.Delete((object) row);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void EPEquipmentRate_EquipmentID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!((PXGraph) this).IsMobile || e.Row == null)
      return;
    EPEquipmentRate row = e.Row as EPEquipmentRate;
    int? oldValue = (int?) e.OldValue;
    int? equipmentId = row.EquipmentID;
    if (oldValue.GetValueOrDefault() == equipmentId.GetValueOrDefault() & oldValue.HasValue == equipmentId.HasValue)
      return;
    sender.SetDefaultExt<EPEquipmentRate.runRate>((object) row);
    sender.SetDefaultExt<EPEquipmentRate.setupRate>((object) row);
    sender.SetDefaultExt<EPEquipmentRate.suspendRate>((object) row);
  }

  protected virtual void EPEquipmentRate_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is EPEquipmentRate) || !((PXGraph) this).IsMobile)
      return;
    List<object> objectList = new List<object>();
    foreach (object obj in sender.Inserted)
    {
      if (!((EPEquipmentRate) obj).EquipmentID.HasValue)
        objectList.Add(obj);
    }
    foreach (object obj in objectList)
      sender.Delete(obj);
  }

  protected virtual void EPEquipmentRate_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (!(e.Row is EPEquipmentRate row) || !((PXGraph) this).IsMobile)
      return;
    List<object> objectList = new List<object>();
    foreach (object obj in sender.Inserted)
    {
      int? equipmentId1 = ((EPEquipmentRate) obj).EquipmentID;
      int? equipmentId2 = row.EquipmentID;
      if (equipmentId1.GetValueOrDefault() == equipmentId2.GetValueOrDefault() & equipmentId1.HasValue == equipmentId2.HasValue)
        objectList.Add(obj);
    }
    foreach (object obj in objectList)
      sender.Delete(obj);
  }

  protected virtual void EPEquipmentRate_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EPEquipmentRate row) || !((PXGraph) this).IsMobile || e.Operation == 3 || row.EquipmentID.HasValue)
      return;
    sender.Delete((object) row);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMRevenueBudget> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.limitQty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, !string.IsNullOrEmpty(e.Row.UOM));
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.maxQty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, e.Row.LimitQty.GetValueOrDefault() && !string.IsNullOrEmpty(e.Row.UOM));
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.curyMaxAmount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, e.Row.LimitAmount.GetValueOrDefault());
    bool flag = this.RevenueQuantityVisible();
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.qtyToInvoice>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, flag);
    if (flag)
    {
      Decimal? qty = e.Row.Qty;
      Decimal num1 = 0M;
      if (qty.GetValueOrDefault() == num1 & qty.HasValue)
      {
        Decimal? revisedQty = e.Row.RevisedQty;
        Decimal num2 = 0M;
        if (revisedQty.GetValueOrDefault() == num2 & revisedQty.HasValue)
          goto label_7;
      }
      if (string.IsNullOrEmpty(e.Row.UOM))
      {
        if (string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMRevenueBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row)))
        {
          PXUIFieldAttribute.SetWarning<PMRevenueBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, "The value of the Actual Qty. will not be updated if no UOM is defined.");
          goto label_9;
        }
        goto label_9;
      }
label_7:
      if (PXUIFieldAttribute.GetError<PMRevenueBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row) == PXLocalizer.Localize("The value of the Actual Qty. will not be updated if no UOM is defined."))
        PXUIFieldAttribute.SetWarning<PMRevenueBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, (string) null);
    }
label_9:
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.curyAmountToInvoice>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, e.Row.ProgressBillingBase == "A");
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.qtyToInvoice>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, e.Row.ProgressBillingBase == "Q");
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.progressBillingBase>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache.Graph.IsImportFromExcel || ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache.Graph.IsExport);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.curyAmount> e)
  {
    if (e.Row == null)
      return;
    PMRevenueBudget row = e.Row;
    Decimal? curyAmount = e.Row.CuryAmount;
    Decimal? changeOrderAmount = e.Row.CuryChangeOrderAmount;
    Decimal? nullable = curyAmount.HasValue & changeOrderAmount.HasValue ? new Decimal?(curyAmount.GetValueOrDefault() + changeOrderAmount.GetValueOrDefault()) : new Decimal?();
    row.CuryRevisedAmount = nullable;
    try
    {
      this._BlockQtyToInvoiceCalculate = true;
      this.RecalculateRevenueBudget(e.Row);
    }
    finally
    {
      this._BlockQtyToInvoiceCalculate = false;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.qty> e)
  {
    if (e.Row != null)
      e.Row.RevisedQty = e.Row.Qty;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.qty>>) e).Cache.SetDefaultExt<PMRevenueBudget.curyUnitRate>((object) e.Row);
    this.RecalculateRevenueBudget(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.costCodeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.costCodeID>>) e).Cache.SetDefaultExt<PMRevenueBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMRevenueBudget.description>((object) e.Row);
    if (!e.Row.AccountGroupID.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMRevenueBudget.accountGroupID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMRevenueBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMRevenueBudget.curyUnitRate>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMRevenueBudget.taxCategoryID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.uOM> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.uOM>>) e).Cache.SetDefaultExt<PMRevenueBudget.curyUnitRate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.progressBillingBase> e)
  {
    if (e.Row == null)
      return;
    switch (e.NewValue as string)
    {
      case "A":
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.progressBillingBase>>) e).Cache.SetValueExt<PMRevenueBudget.qtyToInvoice>((object) e.Row, (object) 0.0M);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.progressBillingBase>>) e).Cache.RaiseFieldUpdated<PMRevenueBudget.completedPct>((object) e.Row, (object) 0.0M);
        break;
      case "Q":
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.progressBillingBase>>) e).Cache.SetValueExt<PMRevenueBudget.curyAmountToInvoice>((object) e.Row, (object) 0.0M);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.progressBillingBase>>) e).Cache.RaiseFieldUpdated<PMRevenueBudget.completedPct>((object) e.Row, (object) 0.0M);
        break;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description> e)
  {
    if (e.Row == null || ((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "C" || ((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "D")
    {
      if (!e.Row.CostCodeID.HasValue || !(PXSelectorAttribute.Select<PMRevenueBudget.costCodeID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>>) e).Cache, (object) e.Row) is PMCostCode pmCostCode))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>, PMRevenueBudget, object>) e).NewValue = (object) pmCostCode.Description;
    }
    else if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "T")
    {
      if (!e.Row.ProjectTaskID.HasValue || !(PXSelectorAttribute.Select<PMRevenueBudget.projectTaskID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>>) e).Cache, (object) e.Row) is PMTask pmTask))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>, PMRevenueBudget, object>) e).NewValue = (object) pmTask.Description;
    }
    else
    {
      if (!(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "I") || !e.Row.InventoryID.HasValue)
        return;
      int? inventoryId = e.Row.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue || !(PXSelectorAttribute.Select<PMRevenueBudget.inventoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>>) e).Cache, (object) e.Row) is PX.Objects.IN.InventoryItem inventoryItem))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>, PMRevenueBudget, object>) e).NewValue = (object) inventoryItem.Descr;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.projectTaskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMRevenueBudget.description>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMRevenueBudget.taxCategoryID>((object) e.Row);
    if (e.Row == null || e.NewValue == null || ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.projectTaskID>>) e).Cache.Graph.IsImportFromExcel && e.Row.ProgressBillingBase != null)
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, e.Row.ProjectID, e.NewValue as int?);
    if (dirty == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.projectTaskID>>) e).Cache.SetValueExt<PMRevenueBudget.progressBillingBase>((object) e.Row, (object) dirty.ProgressBillingBase);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.inventoryID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.inventoryID>, PMRevenueBudget, object>) e).NewValue = (object) PMInventorySelectorAttribute.EmptyInventoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.costCodeID> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null || !(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel != "C"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.costCodeID>, PMRevenueBudget, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.projectTaskID> e)
  {
    if (((PXSelectBase<RevenueBudgetFilter>) this.RevenueFilter).Current == null || !((PXSelectBase<RevenueBudgetFilter>) this.RevenueFilter).Current.ProjectTaskID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.projectTaskID>, PMRevenueBudget, object>) e).NewValue = (object) ((PXSelectBase<RevenueBudgetFilter>) this.RevenueFilter).Current.ProjectTaskID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.curyUnitRate> e)
  {
    if (e.Row == null)
      return;
    PMRevenueBudget row1 = e.Row;
    Decimal? curyAmount = e.Row.CuryAmount;
    Decimal? nullable1 = e.Row.CuryChangeOrderAmount;
    Decimal? nullable2 = curyAmount.HasValue & nullable1.HasValue ? new Decimal?(curyAmount.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    row1.CuryRevisedAmount = nullable2;
    if (!(e.Row.ProgressBillingBase == "Q"))
      return;
    PMRevenueBudget row2 = e.Row;
    nullable1 = e.Row.QtyToInvoice;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.CuryUnitRate;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(valueOrDefault1 * valueOrDefault2);
    row2.CuryAmountToInvoice = nullable3;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.curyUnitRate> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    Decimal? unitPrice = this.RateService.CalculateUnitPrice(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.curyUnitRate>>) e).Cache, e.Row.ProjectID, e.Row.ProjectTaskID, e.Row.InventoryID, e.Row.UOM, e.Row.Qty, ((PXSelectBase<PMProject>) this.Project).Current.StartDate, ((PXSelectBase<PMProject>) this.Project).Current.CuryInfoID);
    if (unitPrice.HasValue)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.curyUnitRate>, PMRevenueBudget, object>) e).NewValue = (object) unitPrice;
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.curyUnitRate>, PMRevenueBudget, object>) e).NewValue = (object) e.Row.CuryUnitRate;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.taxCategoryID> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    int? inventoryId = e.Row.InventoryID;
    if (inventoryId.HasValue)
    {
      inventoryId = e.Row.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue))
      {
        PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<PMRevenueBudget.inventoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.taxCategoryID>>) e).Cache, (object) e.Row);
        if (inventoryItem != null && inventoryItem.TaxCategoryID != null)
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.taxCategoryID>, PMRevenueBudget, object>) e).NewValue = (object) inventoryItem.TaxCategoryID;
      }
    }
    if (((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.taxCategoryID>, PMRevenueBudget, object>) e).NewValue != null)
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, e.Row.ProjectID, e.Row.ProjectTaskID);
    if (dirty == null || dirty.TaxCategoryID == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.taxCategoryID>, PMRevenueBudget, object>) e).NewValue = (object) dirty.TaxCategoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.dropshipExpenseSubMask> e)
  {
    if (((PXSelectBase<PMSetup>) this.Setup).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.dropshipExpenseSubMask>, PMProject, object>) e).NewValue = (object) ((PXSelectBase<PMSetup>) this.Setup).Current.DropshipExpenseSubMask;
    if (((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.dropshipExpenseSubMask>, PMProject, object>) e).NewValue == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.dropshipExpenseSubMask>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMRevenueBudget> e)
  {
    Decimal? nullable1;
    Decimal? nullable2;
    if (!(e.Row.Mode != e.OldRow.Mode) && !(e.Row.ProgressBillingBase != e.OldRow.ProgressBillingBase))
    {
      Decimal? completedPct = e.Row.CompletedPct;
      nullable1 = e.OldRow.CompletedPct;
      if (!(completedPct.GetValueOrDefault() == nullable1.GetValueOrDefault() & completedPct.HasValue == nullable1.HasValue))
      {
        nullable1 = e.Row.CuryAmountToInvoice;
        nullable2 = e.OldRow.CuryAmountToInvoice;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = e.Row.QtyToInvoice;
          nullable1 = e.OldRow.QtyToInvoice;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            goto label_7;
        }
        else
          goto label_7;
      }
      else
        goto label_7;
    }
    try
    {
      nullable1 = e.Row.QtyToInvoice;
      nullable2 = e.OldRow.QtyToInvoice;
      this._BlockQtyToInvoiceCalculate = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue);
      this.RecalculateRevenueBudget(e.Row);
    }
    finally
    {
      this._BlockQtyToInvoiceCalculate = false;
    }
label_7:
    this.UpdateBudgetHistoryLine((PMBudget) e.OldRow, -1);
    this.UpdateBudgetHistoryLine((PMBudget) e.Row, 1);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMRevenueBudget, PMRevenueBudget.completedPct> e)
  {
    if (e.Row == null || !(e.Row.Mode == "A"))
      return;
    PXFieldState instance = PXDecimalState.CreateInstance((object) this.CalculateCompletedPercent(e.Row), new int?(2), "CompletedPct", new bool?(false), new int?(0), new Decimal?(Decimal.MinValue), new Decimal?(Decimal.MaxValue));
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMRevenueBudget, PMRevenueBudget.completedPct>>) e).ReturnState = (object) instance;
  }

  private Decimal CalculateCompletedPercent(PMRevenueBudget row)
  {
    Decimal completedPercent = row.CompletedPct.GetValueOrDefault();
    if (row.ProgressBillingBase == "Q")
    {
      Decimal valueOrDefault = row.RevisedQty.GetValueOrDefault();
      if (valueOrDefault != 0.0M)
        completedPercent = Decimal.Round(100M * (row.InvoicedQty.GetValueOrDefault() + row.ActualQty.GetValueOrDefault() + row.QtyToInvoice.GetValueOrDefault()) / valueOrDefault, 2);
    }
    else
    {
      Decimal valueOrDefault = row.CuryRevisedAmount.GetValueOrDefault();
      if (valueOrDefault != 0M)
        completedPercent = Decimal.Round(100M * (row.CuryAmountToInvoice.GetValueOrDefault() + row.CuryActualAmount.GetValueOrDefault() + row.CuryInclTaxAmount.GetValueOrDefault() + row.CuryInvoicedAmount.GetValueOrDefault() + (row.CuryPrepaymentAmount.GetValueOrDefault() - row.CuryPrepaymentInvoiced.GetValueOrDefault())) / valueOrDefault, 2);
    }
    return completedPercent;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.completedPct> e)
  {
    e.Row.Mode = "M";
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.curyRevisedAmount> e)
  {
    try
    {
      this._BlockQtyToInvoiceCalculate = true;
      this.RecalculateRevenueBudget(e.Row);
    }
    finally
    {
      this._BlockQtyToInvoiceCalculate = false;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.curyAmountToInvoice> e)
  {
    if (this._IsRecalculatingRevenueBudgetScope || !(e.Row?.ProgressBillingBase == "A"))
      return;
    e.Row.CompletedPct = new Decimal?(this.CalculateCompletedPercent(e.Row));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.qtyToInvoice> e)
  {
    PMRevenueBudget row = e.Row;
    if (this._IsRecalculatingRevenueBudgetScope || !(row?.ProgressBillingBase == "Q"))
      return;
    Decimal valueOrDefault1 = row.RevisedQty.GetValueOrDefault();
    if (valueOrDefault1 != 0.0M)
    {
      Decimal valueOrDefault2 = row.InvoicedQty.GetValueOrDefault();
      Decimal? nullable = row.ActualQty;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault2 + valueOrDefault3;
      nullable = row.QtyToInvoice;
      Decimal valueOrDefault4 = nullable.GetValueOrDefault();
      Decimal d = 100M * (num + valueOrDefault4) / valueOrDefault1;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.qtyToInvoice>>) e).Cache.SetValueExt<PMRevenueBudget.completedPct>((object) e.Row, (object) Decimal.Round(d, 2));
    }
    try
    {
      this._BlockQtyToInvoiceCalculate = true;
      this.RecalculateRevenueBudget(e.Row);
    }
    finally
    {
      this._BlockQtyToInvoiceCalculate = false;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMRevenueBudget, PMRevenueBudget.prepaymentPct> e)
  {
    if (e.Row == null)
      return;
    Decimal valueOrDefault = e.Row.CuryRevisedAmount.GetValueOrDefault();
    Decimal d = 0M;
    if (valueOrDefault != 0M)
      d = e.Row.CuryPrepaymentAmount.GetValueOrDefault() * 100M / valueOrDefault;
    PXFieldState instance = PXDecimalState.CreateInstance((object) Math.Round(d, 2), new int?(2), "prepaymentPct", new bool?(false), new int?(0), new Decimal?(Decimal.MinValue), new Decimal?(Decimal.MaxValue));
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMRevenueBudget, PMRevenueBudget.prepaymentPct>>) e).ReturnState = (object) instance;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.prepaymentPct> e)
  {
    Decimal num = Math.Max(0M, e.Row.CuryRevisedAmount.GetValueOrDefault() * ((Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.prepaymentPct>, PMRevenueBudget, object>) e).NewValue).GetValueOrDefault() / 100M);
    Decimal? prepaymentInvoiced = e.Row.CuryPrepaymentInvoiced;
    Decimal valueOrDefault = prepaymentInvoiced.GetValueOrDefault();
    if (num < valueOrDefault & prepaymentInvoiced.HasValue)
      throw new PXSetPropertyException<PMRevenueBudget.prepaymentPct>("The Prepaid Amount can not be decreased less than the already invoiced amount.", (PXErrorLevel) 4);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.prepaymentPct> e)
  {
    if (e.Row == null)
      return;
    Decimal num = Math.Max(0M, e.Row.CuryRevisedAmount.GetValueOrDefault() * e.Row.PrepaymentPct.GetValueOrDefault() / 100M);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.prepaymentPct>>) e).Cache.SetValueExt<PMRevenueBudget.curyPrepaymentAmount>((object) e.Row, (object) num);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyPrepaymentAmount> e)
  {
    PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
    if ((current != null ? (current.PrepaymentEnabled.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    Decimal valueOrDefault1 = e.Row.CuryRevisedAmount.GetValueOrDefault();
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyPrepaymentAmount>, PMRevenueBudget, object>) e).NewValue;
    Decimal valueOrDefault2 = newValue.GetValueOrDefault();
    Decimal? nullable = e.Row.CuryPrepaymentInvoiced;
    Decimal valueOrDefault3 = nullable.GetValueOrDefault();
    if (valueOrDefault2 < valueOrDefault3 & nullable.HasValue)
      throw new PXSetPropertyException<PMRevenueBudget.curyPrepaymentAmount>("The Prepaid Amount can not be decreased less than the already invoiced amount.", (PXErrorLevel) 4);
    Decimal actualAmountWithTaxes = this.GetCuryActualAmountWithTaxes(e.Row);
    nullable = e.Row.CuryInvoicedAmount;
    Decimal valueOrDefault4 = nullable.GetValueOrDefault();
    Decimal num1 = actualAmountWithTaxes + valueOrDefault4;
    nullable = newValue;
    Decimal num2 = num1;
    Decimal num3 = valueOrDefault1 - num2;
    if (!(nullable.GetValueOrDefault() > num3 & nullable.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyPrepaymentAmount>>) e).Cache.RaiseExceptionHandling<PMRevenueBudget.curyPrepaymentAmount>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyPrepaymentAmount>, PMRevenueBudget, object>) e).NewValue, (Exception) new PXSetPropertyException<PMRevenueBudget.curyPrepaymentAmount>("The Prepaid Amount exceeds the uninvoiced balance.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.curyPrepaymentAmount> e)
  {
    if (e.Row == null)
      return;
    e.Row.CuryPrepaymentAvailable = new Decimal?(e.Row.CuryPrepaymentAmount.GetValueOrDefault() - e.Row.CuryPrepaymentInvoiced.GetValueOrDefault());
    this.RecalculateRevenueBudget(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMRevenueBudget, PMRevenueBudget.curyAmountToInvoice> e)
  {
    if (e.Row == null || !((PXSelectBase<PMSetup>) this.Setup).Current.AutoCompleteRevenueBudget.GetValueOrDefault() || this.IsRevenueGroupByTask())
      return;
    Decimal completedPercentByCost = this.CalculateCompletedPercentByCost(e.Row);
    PXFieldState instance = PXDecimalState.CreateInstance((object) Math.Round(Math.Max(0M, e.Row.CuryRevisedAmount.GetValueOrDefault() * completedPercentByCost / 100M - e.Row.CuryInvoicedAmount.GetValueOrDefault()), 2), new int?(2), "CuryAmountToInvoice", new bool?(false), new int?(0), new Decimal?(Decimal.MinValue), new Decimal?(Decimal.MaxValue));
    instance.Enabled = false;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMRevenueBudget, PMRevenueBudget.curyAmountToInvoice>>) e).ReturnState = (object) instance;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyAmountToInvoice> e)
  {
    if (!(((Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyAmountToInvoice>, PMRevenueBudget, object>) e).NewValue).GetValueOrDefault() != 0M))
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, e.Row.ProjectID, e.Row.ProjectTaskID);
    if (dirty != null && string.IsNullOrEmpty(dirty.BillingID))
      throw new PXSetPropertyException("The invoice cannot be created because no billing rule is specified for the task.");
    if (this.ContainsProgressiveBillingRule(e.Row.ProjectID, e.Row.ProjectTaskID))
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyAmountToInvoice>>) e).Cache.RaiseExceptionHandling<PMRevenueBudget.curyAmountToInvoice>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyAmountToInvoice>, PMRevenueBudget, object>) e).NewValue, (Exception) new PXSetPropertyException<PMRevenueBudget.curyAmountToInvoice>("The billing rule of the task contains only Time and Material steps. The Completed (%) and Pending Invoice Amount columns are not used for billing.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.completedPct> e)
  {
    if (!(((Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.completedPct>, PMRevenueBudget, object>) e).NewValue).GetValueOrDefault() != 0M))
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, e.Row.ProjectID, e.Row.ProjectTaskID);
    if (dirty != null && string.IsNullOrEmpty(dirty.BillingID))
      throw new PXSetPropertyException("The invoice cannot be created because no billing rule is specified for the task.");
    if (this.ContainsProgressiveBillingRule(e.Row.ProjectID, e.Row.ProjectTaskID))
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.completedPct>>) e).Cache.RaiseExceptionHandling<PMRevenueBudget.completedPct>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.completedPct>, PMRevenueBudget, object>) e).NewValue, (Exception) new PXSetPropertyException<PMRevenueBudget.completedPct>("The billing rule of the task contains only Time and Material steps. The Completed (%) and Pending Invoice Amount columns are not used for billing.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.retainagePct> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.retainagePct>>) e).Cache.SetDefaultExt<PMRevenueBudget.curyCapAmount>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMBudget.retainageMaxPct> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMBudget.retainageMaxPct>>) e).Cache.SetDefaultExt<PMRevenueBudget.curyCapAmount>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PMRevenueBudget> e)
  {
    if (!this.BudgetEditable() && ((PXSelectBase) this.Project).Cache.GetStatus((object) ((PXSelectBase<PMProject>) this.Project).Current) != 3)
      throw new PXException("The line cannot be deleted because the project budget is locked. Please unlock the project budget and try again.");
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMRevenueBudget> e)
  {
    foreach (PXResult<PMCostBudget> pxResult in ((PXSelectBase<PMCostBudget>) new PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Required<PMCostBudget.projectID>>, And<PMCostBudget.revenueTaskID, Equal<Required<PMCostBudget.projectTaskID>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) e.Row.ProjectID,
      (object) e.Row.ProjectTaskID
    }))
    {
      PMCostBudget pmCostBudget = PXResult<PMCostBudget>.op_Implicit(pxResult);
      pmCostBudget.RevenueTaskID = new int?();
      pmCostBudget.RevenueInventoryID = new int?();
      ((PXSelectBase<PMCostBudget>) this.CostBudget).Update(pmCostBudget);
    }
    this.UpdateBudgetHistoryLine((PMBudget) e.Row, -1);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.retainagePct> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.retainagePct>, PMRevenueBudget, object>) e).NewValue == null)
      return;
    Decimal newValue = (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.retainagePct>, PMRevenueBudget, object>) e).NewValue;
    if (newValue < 0M || newValue > 100M)
      throw new PXSetPropertyException<PMRevenueBudget.retainagePct>("Percentage value should be between 0 and 100");
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMCostBudget> e)
  {
    if (e.Row == null || !this.CostQuantityVisible())
      return;
    Decimal? qty = e.Row.Qty;
    Decimal num1 = 0M;
    if (qty.GetValueOrDefault() == num1 & qty.HasValue)
    {
      Decimal? revisedQty = e.Row.RevisedQty;
      Decimal num2 = 0M;
      if (revisedQty.GetValueOrDefault() == num2 & revisedQty.HasValue)
        goto label_6;
    }
    if (string.IsNullOrEmpty(e.Row.UOM))
    {
      if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMCostBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostBudget>>) e).Cache, (object) e.Row)))
        return;
      PXUIFieldAttribute.SetWarning<PMCostBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostBudget>>) e).Cache, (object) e.Row, "The value of the Actual Qty. will not be updated if no UOM is defined.");
      return;
    }
label_6:
    if (!(PXUIFieldAttribute.GetError<PMCostBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostBudget>>) e).Cache, (object) e.Row) == PXLocalizer.Localize("The value of the Actual Qty. will not be updated if no UOM is defined.")))
      return;
    PXUIFieldAttribute.SetWarning<PMCostBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostBudget>>) e).Cache, (object) e.Row, (string) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.curyAmount> e)
  {
    if (e.Row == null)
      return;
    PMCostBudget row = e.Row;
    Decimal? curyAmount = e.Row.CuryAmount;
    Decimal? changeOrderAmount = e.Row.CuryChangeOrderAmount;
    Decimal? nullable = curyAmount.HasValue & changeOrderAmount.HasValue ? new Decimal?(curyAmount.GetValueOrDefault() + changeOrderAmount.GetValueOrDefault()) : new Decimal?();
    row.CuryRevisedAmount = nullable;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.qty> e)
  {
    if (e.Row != null)
      e.Row.RevisedQty = e.Row.Qty;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.qty>>) e).Cache.SetDefaultExt<PMCostBudget.curyUnitPrice>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.curyUnitRate> e)
  {
    if (e.Row == null)
      return;
    PMCostBudget row = e.Row;
    Decimal? curyAmount = e.Row.CuryAmount;
    Decimal? changeOrderAmount = e.Row.CuryChangeOrderAmount;
    Decimal? nullable = curyAmount.HasValue & changeOrderAmount.HasValue ? new Decimal?(curyAmount.GetValueOrDefault() + changeOrderAmount.GetValueOrDefault()) : new Decimal?();
    row.CuryRevisedAmount = nullable;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.curyUnitRate> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    Decimal? unitCost = this.RateService.CalculateUnitCost(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.curyUnitRate>>) e).Cache, e.Row.ProjectID, e.Row.ProjectTaskID, e.Row.InventoryID, e.Row.UOM, new int?(), ((PXSelectBase<PMProject>) this.Project).Current.StartDate, ((PXSelectBase<PMProject>) this.Project).Current.CuryInfoID);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.curyUnitRate>, PMCostBudget, object>) e).NewValue = (object) unitCost.GetValueOrDefault();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.curyUnitPrice> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    Decimal? unitPrice = this.RateService.CalculateUnitPrice(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.curyUnitPrice>>) e).Cache, e.Row.ProjectID, e.Row.ProjectTaskID, e.Row.InventoryID, e.Row.UOM, e.Row.Qty, ((PXSelectBase<PMProject>) this.Project).Current.StartDate, ((PXSelectBase<PMProject>) this.Project).Current.CuryInfoID);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.curyUnitPrice>, PMCostBudget, object>) e).NewValue = (object) unitPrice.GetValueOrDefault();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.costCodeID> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null || !(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel != "C"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.costCodeID>, PMCostBudget, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.projectTaskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMCostBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.description>((object) e.Row);
    if (!e.Row.AccountGroupID.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.accountGroupID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.curyUnitRate>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.curyUnitPrice>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.uOM> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.uOM>>) e).Cache.SetDefaultExt<PMCostBudget.curyUnitRate>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.uOM>>) e).Cache.SetDefaultExt<PMCostBudget.curyUnitPrice>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.revenueTaskID> e)
  {
    if (PXResultset<PMRevenueBudget>.op_Implicit(((PXSelectBase<PMRevenueBudget>) new PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMCostBudget.projectID>>, And<PMRevenueBudget.projectTaskID, Equal<Current<PMCostBudget.revenueTaskID>>, And<PMRevenueBudget.inventoryID, Equal<Current<PMCostBudget.revenueInventoryID>>>>>>((PXGraph) this)).Select(Array.Empty<object>())) != null)
      return;
    e.Row.RevenueInventoryID = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.costCodeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.costCodeID>>) e).Cache.SetDefaultExt<PMCostBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description> e)
  {
    if (e.Row == null || ((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    if (((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "C" || ((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "D")
    {
      if (!e.Row.CostCodeID.HasValue || !(PXSelectorAttribute.Select<PMCostBudget.costCodeID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>>) e).Cache, (object) e.Row) is PMCostCode pmCostCode))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>, PMCostBudget, object>) e).NewValue = (object) pmCostCode.Description;
    }
    else if (((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "T")
    {
      if (!e.Row.ProjectTaskID.HasValue || !(PXSelectorAttribute.Select<PMCostBudget.projectTaskID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>>) e).Cache, (object) e.Row) is PMTask pmTask))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>, PMCostBudget, object>) e).NewValue = (object) pmTask.Description;
    }
    else
    {
      if (!(((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "I") || !e.Row.InventoryID.HasValue)
        return;
      int? inventoryId = e.Row.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue || !(PXSelectorAttribute.Select<PMCostBudget.inventoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>>) e).Cache, (object) e.Row) is PX.Objects.IN.InventoryItem inventoryItem))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>, PMCostBudget, object>) e).NewValue = (object) inventoryItem.Descr;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.projectTaskID> e)
  {
    if (((PXSelectBase<CostBudgetFilter>) this.CostFilter).Current == null || !((PXSelectBase<CostBudgetFilter>) this.CostFilter).Current.ProjectTaskID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.projectTaskID>, PMCostBudget, object>) e).NewValue = (object) ((PXSelectBase<CostBudgetFilter>) this.CostFilter).Current.ProjectTaskID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.inventoryID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.inventoryID>, PMCostBudget, object>) e).NewValue = (object) PMInventorySelectorAttribute.EmptyInventoryID;
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PMCostBudget> e)
  {
    if (!this.BudgetEditable() && ((PXSelectBase) this.Project).Cache.GetStatus((object) ((PXSelectBase<PMProject>) this.Project).Current) != 3)
      throw new PXException("The line cannot be deleted because the project budget is locked. Please unlock the project budget and try again.");
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMCostBudget> e)
  {
    this.UpdateBudgetHistoryLine((PMBudget) e.Row, -1);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMCostBudget> e)
  {
    int? projectTaskId1 = e.Row.ProjectTaskID;
    int? projectTaskId2 = e.OldRow.ProjectTaskID;
    if (projectTaskId1.GetValueOrDefault() == projectTaskId2.GetValueOrDefault() & projectTaskId1.HasValue == projectTaskId2.HasValue)
    {
      int? costCodeId1 = e.Row.CostCodeID;
      int? costCodeId2 = e.OldRow.CostCodeID;
      if (costCodeId1.GetValueOrDefault() == costCodeId2.GetValueOrDefault() & costCodeId1.HasValue == costCodeId2.HasValue)
      {
        int? accountGroupId1 = e.Row.AccountGroupID;
        int? accountGroupId2 = e.OldRow.AccountGroupID;
        if (accountGroupId1.GetValueOrDefault() == accountGroupId2.GetValueOrDefault() & accountGroupId1.HasValue == accountGroupId2.HasValue)
        {
          int? inventoryId1 = e.Row.InventoryID;
          int? inventoryId2 = e.OldRow.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            goto label_15;
        }
      }
    }
    foreach (PXResult<PMBudgetProduction> pxResult in ((PXSelectBase<PMBudgetProduction>) this.BudgetProduction).Select(Array.Empty<object>()))
    {
      PMBudgetProduction budgetProduction = PXResult<PMBudgetProduction>.op_Implicit(pxResult);
      int? nullable1 = budgetProduction.ProjectTaskID;
      int? nullable2 = e.OldRow.ProjectTaskID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = budgetProduction.CostCodeID;
        nullable1 = e.OldRow.CostCodeID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = budgetProduction.AccountGroupID;
          nullable2 = e.OldRow.AccountGroupID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = budgetProduction.InventoryID;
            nullable1 = e.OldRow.InventoryID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              PMBudgetProduction copy = (PMBudgetProduction) ((PXSelectBase) this.BudgetProduction).Cache.CreateCopy((object) budgetProduction);
              copy.ProjectTaskID = e.Row.ProjectTaskID;
              copy.CostCodeID = e.Row.CostCodeID;
              copy.AccountGroupID = e.Row.AccountGroupID;
              copy.InventoryID = e.Row.InventoryID;
              ((PXSelectBase<PMBudgetProduction>) this.BudgetProduction).Delete(budgetProduction);
              ((PXSelectBase<PMBudgetProduction>) this.BudgetProduction).Insert(copy);
            }
          }
        }
      }
    }
label_15:
    this.UpdateBudgetHistoryLine((PMBudget) e.OldRow, -1);
    this.UpdateBudgetHistoryLine((PMBudget) e.Row, 1);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMOtherBudget> e)
  {
    if (e.Row == null)
      return;
    Decimal? nullable = e.Row.Qty;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
    {
      nullable = e.Row.RevisedQty;
      Decimal num2 = 0M;
      if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
        goto label_6;
    }
    if (string.IsNullOrEmpty(e.Row.UOM))
    {
      if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMOtherBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMOtherBudget>>) e).Cache, (object) e.Row)))
        return;
      PXUIFieldAttribute.SetWarning<PMOtherBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMOtherBudget>>) e).Cache, (object) e.Row, "The value of the Actual Qty. will not be updated if no UOM is defined.");
      return;
    }
label_6:
    if (!(PXUIFieldAttribute.GetError<PMOtherBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMOtherBudget>>) e).Cache, (object) e.Row) == PXLocalizer.Localize("The value of the Actual Qty. will not be updated if no UOM is defined.")))
      return;
    PXUIFieldAttribute.SetWarning<PMOtherBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMOtherBudget>>) e).Cache, (object) e.Row, (string) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMOtherBudget, PMOtherBudget.costCodeID> e)
  {
    if (!CostCodeAttribute.UseCostCode() || !(((PXSelectBase<PMProject>) this.Project).Current?.BudgetLevel == "C"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMOtherBudget, PMOtherBudget.costCodeID>>) e).Cache.SetDefaultExt<PMOtherBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMOtherBudget, PMOtherBudget.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMOtherBudget, PMOtherBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.description>((object) e.Row);
    if (!e.Row.AccountGroupID.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMOtherBudget, PMOtherBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.accountGroupID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMOtherBudget, PMOtherBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMOtherBudget, PMOtherBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.curyUnitRate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMOtherBudget, PMOtherBudget.projectTaskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMOtherBudget, PMOtherBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMOtherBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMOtherBudget, PMOtherBudget.costCodeID> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null || !(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel != "C"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMOtherBudget, PMOtherBudget.costCodeID>, PMOtherBudget, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMOtherBudget, PMOtherBudget.curyAmount> e)
  {
    if (e.Row == null)
      return;
    e.Row.CuryRevisedAmount = e.Row.CuryAmount;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMOtherBudget, PMOtherBudget.qty> e)
  {
    if (e.Row == null)
      return;
    e.Row.RevisedQty = e.Row.Qty;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ContractBillingSchedule, ContractBillingSchedule.type> e)
  {
    if (e.Row == null || e.Row.Type == null)
      return;
    PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
    if ((current != null ? (current.StartDate.HasValue ? 1 : 0) : 0) == 0)
      return;
    e.Row.NextDate = PMBillEngine.GetNextBillingDate((PXGraph) this, e.Row, ((PXSelectBase<PMProject>) this.Project).Current.StartDate);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ContractBillingSchedule> e)
  {
    ContractBillingSchedule row = e.Row;
    if (row == null)
      return;
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ContractBillingSchedule>>) e).Cache;
      ContractBillingSchedule contractBillingSchedule1 = row;
      int? customerId = ((PXSelectBase<PMProject>) this.Project).Current.CustomerID;
      int num1 = !customerId.HasValue ? 0 : (!((PXSelectBase<PMProject>) this.Project).Current.IsActive.GetValueOrDefault() ? 1 : 0);
      PXUIFieldAttribute.SetEnabled<ContractBillingSchedule.type>(cache1, (object) contractBillingSchedule1, num1 != 0);
      PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ContractBillingSchedule>>) e).Cache;
      customerId = ((PXSelectBase<PMProject>) this.Project).Current.CustomerID;
      int num2 = customerId.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetRequired<ContractBillingSchedule.type>(cache2, num2 != 0);
      PXCache cache3 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ContractBillingSchedule>>) e).Cache;
      ContractBillingSchedule contractBillingSchedule2 = row;
      int num3;
      if (((PXSelectBase<PMProject>) this.Project).Current.IsActive.GetValueOrDefault() || ((PXGraph) this).IsContractBasedAPI)
      {
        customerId = ((PXSelectBase<PMProject>) this.Project).Current.CustomerID;
        num3 = customerId.HasValue ? 1 : 0;
      }
      else
        num3 = 0;
      PXUIFieldAttribute.SetEnabled<ContractBillingSchedule.nextDate>(cache3, (object) contractBillingSchedule2, num3 != 0);
      PXUIFieldAttribute.SetRequired<ContractBillingSchedule.nextDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ContractBillingSchedule>>) e).Cache, true);
    }
    if (!(row.Type == "D"))
      return;
    PXUIFieldAttribute.SetEnabled<ContractBillingSchedule.nextDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ContractBillingSchedule>>) e).Cache, (object) row, false);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<ContractBillingSchedule> e)
  {
    if (e.Operation == 3)
      return;
    ContractBillingSchedule row = e.Row;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMProjectBalanceRecord> e)
  {
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMBillingRecord> e)
  {
    int? recordId = e.Row.RecordID;
    int num = 0;
    if (!(recordId.GetValueOrDefault() <= num & recordId.HasValue))
      return;
    e.Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CostBudgetFilter, CostBudgetFilter.groupByTask> e)
  {
    if (e.Row.GroupByTask.GetValueOrDefault())
    {
      e.Row.ProjectTaskID = new int?();
      ((PXSelectBase) this.Project).Cache.RaiseRowSelected((object) ((PXSelectBase<PMProject>) this.Project).Current);
    }
    ((PXSelectBase<PMCostBudget>) this.CostBudget).Current = (PMCostBudget) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<RevenueBudgetFilter, RevenueBudgetFilter.groupByTask> e)
  {
    if (!e.Row.GroupByTask.GetValueOrDefault())
      return;
    e.Row.ProjectTaskID = new int?();
    ((PXSelectBase) this.Project).Cache.RaiseRowSelected((object) ((PXSelectBase<PMProject>) this.Project).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.dropshipReceiptProcessing> e)
  {
    if (e.Row == null || !((string) e.NewValue == "S"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.dropshipReceiptProcessing>>) e).Cache.SetValueExt<PMProject.dropshipExpenseRecording>((object) e.Row, (object) "B");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PMProject.dropshipExpenseRecording> e)
  {
    if (e.Row == null || !((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.dropshipExpenseRecording>, PMProject, object>) e).NewValue == "R") || !PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return;
    INSetup inSetup = PXResultset<INSetup>.op_Implicit(PXSelectBase<INSetup, PXSelect<INSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if ((inSetup != null ? (!inSetup.UpdateGL.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      throw new PXSetPropertyException<PMProject.dropshipExpenseRecording>("The On Receipt Release option cannot be selected because the Update GL check box is cleared on the Inventory Preferences (IN101000) form.");
  }

  protected virtual void _(PX.Data.Events.RowSelected<RevenueBudgetFilter> e)
  {
    if (e.Row == null)
      return;
    if (((PXSelectBase<PMSetup>) this.Setup).Current.AutoCompleteRevenueBudget.GetValueOrDefault())
    {
      Decimal num = 0M;
      foreach (PMRevenueTotal autoRevenueTotal in this.GetAutoRevenueTotals())
        num += autoRevenueTotal.CuryAmountToInvoiceProjected.GetValueOrDefault();
      e.Row.CuryAmountToInvoiceTotal = new Decimal?(num);
    }
    else
    {
      PMRevenueBudget pmRevenueBudget1 = PXResultset<PMRevenueBudget>.op_Implicit(((PXSelectBase<PMRevenueBudget>) new PXSelectGroupBy<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>, Aggregate<Sum<PMRevenueBudget.curyAmountToInvoice>>>((PXGraph) this)).Select(Array.Empty<object>()));
      if (pmRevenueBudget1 == null)
        return;
      e.Row.CuryAmountToInvoiceTotal = pmRevenueBudget1.CuryAmountToInvoice;
      foreach (PMRevenueBudget pmRevenueBudget2 in ((PXSelectBase) this.RevenueBudget).Cache.Deleted)
      {
        RevenueBudgetFilter row = e.Row;
        Decimal? amountToInvoiceTotal = row.CuryAmountToInvoiceTotal;
        Decimal? nullable1 = pmRevenueBudget2.CuryAmountToInvoice;
        Decimal valueOrDefault = nullable1.GetValueOrDefault();
        Decimal? nullable2;
        if (!amountToInvoiceTotal.HasValue)
        {
          nullable1 = new Decimal?();
          nullable2 = nullable1;
        }
        else
          nullable2 = new Decimal?(amountToInvoiceTotal.GetValueOrDefault() - valueOrDefault);
        row.CuryAmountToInvoiceTotal = nullable2;
      }
      foreach (PMRevenueBudget pmRevenueBudget3 in ((PXSelectBase) this.RevenueBudget).Cache.Inserted)
      {
        RevenueBudgetFilter row = e.Row;
        Decimal? amountToInvoiceTotal = row.CuryAmountToInvoiceTotal;
        Decimal? nullable3 = pmRevenueBudget3.CuryAmountToInvoice;
        Decimal valueOrDefault = nullable3.GetValueOrDefault();
        Decimal? nullable4;
        if (!amountToInvoiceTotal.HasValue)
        {
          nullable3 = new Decimal?();
          nullable4 = nullable3;
        }
        else
          nullable4 = new Decimal?(amountToInvoiceTotal.GetValueOrDefault() + valueOrDefault);
        row.CuryAmountToInvoiceTotal = nullable4;
      }
      foreach (PMRevenueBudget pmRevenueBudget4 in ((PXSelectBase) this.RevenueBudget).Cache.Updated)
      {
        Decimal? valueOriginal = (Decimal?) ((PXSelectBase) this.RevenueBudget).Cache.GetValueOriginal<PMRevenueBudget.curyAmountToInvoice>((object) pmRevenueBudget4);
        if (valueOriginal.HasValue)
        {
          RevenueBudgetFilter row = e.Row;
          Decimal? amountToInvoiceTotal = row.CuryAmountToInvoiceTotal;
          Decimal num = valueOriginal.Value;
          row.CuryAmountToInvoiceTotal = amountToInvoiceTotal.HasValue ? new Decimal?(amountToInvoiceTotal.GetValueOrDefault() - num) : new Decimal?();
        }
        RevenueBudgetFilter row1 = e.Row;
        Decimal? amountToInvoiceTotal1 = row1.CuryAmountToInvoiceTotal;
        Decimal valueOrDefault = pmRevenueBudget4.CuryAmountToInvoice.GetValueOrDefault();
        row1.CuryAmountToInvoiceTotal = amountToInvoiceTotal1.HasValue ? new Decimal?(amountToInvoiceTotal1.GetValueOrDefault() + valueOrDefault) : new Decimal?();
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowInserting<PMAccountTask> e)
  {
    if (e.Row == null || !((PXGraph) this).IsImport || !this.AccountTaskIsPresent(e.Row))
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMProjectContact> e)
  {
    if (e.Operation == 3 || !e.Row.ContactID.HasValue)
      return;
    PX.Objects.CR.Contact contact = PX.Objects.CR.Contact.PK.Find((PXGraph) this, e.Row.ContactID);
    if (contact == null || contact.IsActive.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProjectContact>>) e).Cache.RaiseExceptionHandling<PMProjectContact.contactID>((object) e.Row, (object) contact.DisplayName, (Exception) new PXSetPropertyException<PMProjectContact.contactID>(contact.ContactType == "EP" ? "The employee is not active." : "The contact is not active."));
  }

  protected bool AccountTaskIsPresent(PMAccountTask pmAccountTask)
  {
    if (pmAccountTask == null)
      return false;
    foreach (PXResult<PMAccountTask> pxResult in ((PXSelectBase<PMAccountTask>) this.Accounts).Select(Array.Empty<object>()))
    {
      int? accountId1 = PXResult<PMAccountTask>.op_Implicit(pxResult).AccountID;
      int? accountId2 = pmAccountTask.AccountID;
      if (accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue)
        return true;
    }
    return false;
  }

  protected virtual void _(PX.Data.Events.RowSelected<NotificationRecipient> e)
  {
    if (e.Row == null)
      return;
    if (PXSelectorAttribute.Select<NotificationRecipient.contactID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<NotificationRecipient>>) e).Cache, (object) e.Row) is PX.Objects.CR.Contact contact)
    {
      e.Row.Email = contact.EMail;
    }
    else
    {
      if (!(e.Row.ContactType == "P"))
        return;
      PX.Objects.PM.PMContact pmContact = (PX.Objects.PM.PMContact) ((PXSelectBase<PMBillingContact>) this.Billing_Contact).SelectSingle(Array.Empty<object>());
      if (pmContact == null)
        return;
      e.Row.Email = pmContact.Email;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<NotificationSource> e)
  {
    if (e.Row == null)
      return;
    NotificationSetup notificationSetup = PXResultset<NotificationSetup>.op_Implicit(PXSelectBase<NotificationSetup, PXSelect<NotificationSetup, Where<NotificationSetup.setupID, Equal<Required<NotificationSetup.setupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.SetupID
    }));
    if (notificationSetup != null && (notificationSetup.NotificationCD == "PROFORMA" || notificationSetup.NotificationCD == "CHANGE ORDER"))
      PXUIFieldAttribute.SetEnabled<NotificationSource.active>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<NotificationSource>>) e).Cache, (object) e.Row, false);
    else
      PXUIFieldAttribute.SetEnabled<NotificationSource.active>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<NotificationSource>>) e).Cache, (object) e.Row, true);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<NotificationSource.reportID> e)
  {
    if ((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<NotificationSource.reportID>, object, object>) e).NewValue == "PM644000" || (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<NotificationSource.reportID>, object, object>) e).NewValue == "PM644500")
      throw new PXSetPropertyException("The AIA Report ({0}) and AIA Report with Quantity ({1}) reports cannot be used in mailing settings. These reports are to be generated only by clicking the AIA Report button on the form toolbar of the Projects (PM301000) and Pro Forma Invoices (PM307000) forms.", new object[2]
      {
        (object) "PM644000",
        (object) "PM644500"
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ProjectEntry.CopyDialogInfo, ProjectEntry.CopyDialogInfo.projectID> e)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) new PXSelect<PMProject, Where<PMProject.contractCD, Equal<Required<PMProject.contractCD>>, And<PMProject.baseType, Equal<CTPRType.project>>>>((PXGraph) this)).Select(new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ProjectEntry.CopyDialogInfo, ProjectEntry.CopyDialogInfo.projectID>, ProjectEntry.CopyDialogInfo, object>) e).NewValue
    }));
    if (pmProject != null)
      throw new PXSetPropertyException<ProjectEntry.CopyDialogInfo.projectID>("The project with the {0} identifier already exists. Specify another project ID.", new object[1]
      {
        (object) pmProject.ContractCD.Trim()
      });
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<PMSiteAddress, PMSiteAddress.countryID> args)
  {
    if (args.Row == null)
      return;
    args.Row.State = (string) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProjectContact, PMProjectContact.contactID> e)
  {
    e.Row.RoleID = (string) null;
  }

  /// <summary>
  /// Creates a new instance of ProjectEntry graph and inserts copies of entities from current graph.
  /// Redirects to target graph on completion.
  /// </summary>
  public virtual void Copy(PMProject project)
  {
    int num1 = DimensionMaint.IsAutonumbered((PXGraph) this, "PROJECT") ? 1 : 0;
    string str1 = (string) null;
    if (num1 == 0)
    {
      if (!WebDialogResultExtension.IsPositive(((PXSelectBase<ProjectEntry.CopyDialogInfo>) this.CopyDialog).AskExt()) || string.IsNullOrEmpty(((PXSelectBase<ProjectEntry.CopyDialogInfo>) this.CopyDialog).Current.ProjectID))
        return;
      str1 = ((PXSelectBase<ProjectEntry.CopyDialogInfo>) this.CopyDialog).Current.ProjectID;
    }
    ProjectEntry instance = PXGraph.CreateInstance<ProjectEntry>();
    ProjectEntryRevenueTaskExt extension = ((PXGraph) instance).GetExtension<ProjectEntryRevenueTaskExt>();
    if (extension != null)
      extension.SuppressRevenueTaskIDDefaulting = true;
    ((PXGraph) instance).SelectTimeStamp();
    instance.IsCopyPaste = true;
    instance.CopySource = this;
    PMProject copy1 = (PMProject) ((PXSelectBase) this.Project).Cache.CreateCopy((object) project);
    copy1.ContractID = new int?();
    copy1.ContractCD = str1;
    copy1.Status = (string) null;
    copy1.Hold = new bool?();
    copy1.StartDate = new DateTime?();
    copy1.ExpireDate = new DateTime?();
    copy1.BudgetFinalized = new bool?();
    copy1.LastChangeOrderNumber = (string) null;
    copy1.LastProformaNumber = (string) null;
    copy1.IsActive = new bool?();
    copy1.IsCompleted = new bool?();
    copy1.IsCancelled = new bool?();
    copy1.NoteID = new Guid?();
    copy1.CuryInfoID = new long?();
    copy1.BaseCuryID = (string) null;
    copy1.QuoteNbr = (string) null;
    copy1.StatusCode = new int?();
    copy1.SiteAddressID = new int?();
    copy1.BillAddressID = new int?();
    copy1.BillContactID = new int?();
    copy1.OwnerID = new int?();
    copy1.CustomerID = new int?();
    copy1.LocationID = new int?();
    PMProject pmProject = ((PXSelectBase<PMProject>) instance.Project).Insert(copy1);
    pmProject.OwnerID = project.OwnerID;
    pmProject.CustomerID = project.CustomerID;
    pmProject.LocationID = project.LocationID;
    pmProject.ApproverID = project.ApproverID;
    pmProject.AssistantID = project.AssistantID;
    object customerId = (object) pmProject.CustomerID;
    ((PXSelectBase) instance.Project).Cache.RaiseFieldVerifying<PMProject.customerID>((object) pmProject, ref customerId);
    SharedRecordAttribute.CopyRecord<PMProject.siteAddressID>(((PXSelectBase) instance.Project).Cache, (object) pmProject, (object) project, false);
    SharedRecordAttribute.CopyRecord<PMProject.billAddressID>(((PXSelectBase) instance.Project).Cache, (object) pmProject, (object) project, false);
    SharedRecordAttribute.CopyRecord<PMProject.billContactID>(((PXSelectBase) instance.Project).Cache, (object) pmProject, (object) project, false);
    ((PXSelectBase) instance.Billing).Cache.Clear();
    ContractBillingSchedule copy2 = (ContractBillingSchedule) ((PXSelectBase) this.Billing).Cache.CreateCopy((object) ((PXSelectBase<ContractBillingSchedule>) this.Billing).SelectSingle(Array.Empty<object>()));
    copy2.ContractID = pmProject.ContractID;
    copy2.LastDate = new DateTime?();
    ((PXSelectBase<ContractBillingSchedule>) instance.Billing).Insert(copy2);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldDefaulting.AddHandler<PMTask.billingID>(ProjectEntry.\u003C\u003Ec.\u003C\u003E9__384_0 ?? (ProjectEntry.\u003C\u003Ec.\u003C\u003E9__384_0 = new PXFieldDefaulting((object) ProjectEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCopy\u003Eb__384_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldDefaulting.AddHandler<PMTask.allocationID>(ProjectEntry.\u003C\u003Ec.\u003C\u003E9__384_1 ?? (ProjectEntry.\u003C\u003Ec.\u003C\u003E9__384_1 = new PXFieldDefaulting((object) ProjectEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCopy\u003Eb__384_1))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldDefaulting.AddHandler<PMTask.rateTableID>(ProjectEntry.\u003C\u003Ec.\u003C\u003E9__384_2 ?? (ProjectEntry.\u003C\u003Ec.\u003C\u003E9__384_2 = new PXFieldDefaulting((object) ProjectEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCopy\u003Eb__384_2))));
    Dictionary<int, int> taskMap = new Dictionary<int, int>();
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMTask pmTask1 = PXResult<PMTask>.op_Implicit(pxResult);
      PMTask copy3 = (PMTask) ((PXSelectBase) this.Tasks).Cache.CreateCopy((object) pmTask1);
      PMTask pmTask2 = copy3;
      int? nullable1 = new int?();
      int? nullable2 = nullable1;
      pmTask2.TaskID = nullable2;
      copy3.ProjectID = pmProject.ContractID;
      copy3.IsActive = new bool?();
      copy3.IsCompleted = new bool?();
      copy3.IsCancelled = new bool?();
      copy3.Status = (string) null;
      PMTask pmTask3 = copy3;
      DateTime? nullable3 = new DateTime?();
      DateTime? nullable4 = nullable3;
      pmTask3.StartDate = nullable4;
      PMTask pmTask4 = copy3;
      nullable3 = new DateTime?();
      DateTime? nullable5 = nullable3;
      pmTask4.EndDate = nullable5;
      PMTask pmTask5 = copy3;
      nullable3 = new DateTime?();
      DateTime? nullable6 = nullable3;
      pmTask5.PlannedStartDate = nullable6;
      PMTask pmTask6 = copy3;
      nullable3 = new DateTime?();
      DateTime? nullable7 = nullable3;
      pmTask6.PlannedEndDate = nullable7;
      copy3.CompletedPercent = new Decimal?();
      copy3.NoteID = new Guid?();
      nullable3 = pmTask1.PlannedStartDate;
      if (nullable3.HasValue)
      {
        nullable3 = ((PXGraph) this).Accessinfo.BusinessDate;
        if (nullable3.HasValue)
        {
          nullable3 = project.StartDate;
          if (nullable3.HasValue)
          {
            PMTask pmTask7 = copy3;
            nullable3 = ((PXGraph) this).Accessinfo.BusinessDate;
            DateTime dateTime1 = nullable3.Value;
            ref DateTime local1 = ref dateTime1;
            nullable3 = pmTask1.PlannedStartDate;
            DateTime dateTime2 = nullable3.Value;
            ref DateTime local2 = ref dateTime2;
            nullable3 = project.StartDate;
            DateTime dateTime3 = nullable3.Value;
            double totalDays1 = local2.Subtract(dateTime3).TotalDays;
            DateTime? nullable8 = new DateTime?(local1.AddDays(totalDays1));
            pmTask7.PlannedStartDate = nullable8;
            nullable3 = pmTask1.PlannedEndDate;
            if (nullable3.HasValue)
            {
              PMTask pmTask8 = copy3;
              nullable3 = copy3.PlannedStartDate;
              dateTime1 = nullable3.Value;
              ref DateTime local3 = ref dateTime1;
              nullable3 = pmTask1.PlannedEndDate;
              dateTime2 = nullable3.Value;
              ref DateTime local4 = ref dateTime2;
              nullable3 = pmTask1.PlannedStartDate;
              DateTime dateTime4 = nullable3.Value;
              double totalDays2 = local4.Subtract(dateTime4).TotalDays;
              DateTime? nullable9 = new DateTime?(local3.AddDays(totalDays2));
              pmTask8.PlannedEndDate = nullable9;
            }
          }
        }
      }
      PMTask pmTask9 = ((PXSelectBase<PMTask>) instance.Tasks).Insert(copy3);
      Dictionary<int, int> dictionary = taskMap;
      nullable1 = pmTask1.TaskID;
      int key = nullable1.Value;
      nullable1 = pmTask9.TaskID;
      int num2 = nullable1.Value;
      dictionary.Add(key, num2);
      instance.TaskAnswers.CopyAllAttributes((object) pmTask9, (object) pmTask1);
      this.OnCopyPasteTaskInserted(pmProject, pmTask9, pmTask1);
    }
    this.OnCopyPasteTasksInserted(instance, taskMap);
    foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()))
    {
      PMRevenueBudget sourceItem = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
      PMRevenueBudget copy4 = (PMRevenueBudget) ((PXSelectBase) this.RevenueBudget).Cache.CreateCopy((object) sourceItem);
      copy4.ProjectID = pmProject.ContractID;
      copy4.ProjectTaskID = new int?(taskMap[sourceItem.TaskID.Value]);
      copy4.CuryActualAmount = new Decimal?(0M);
      copy4.ActualAmount = new Decimal?(0M);
      copy4.CuryInclTaxAmount = new Decimal?(0M);
      copy4.InclTaxAmount = new Decimal?(0M);
      copy4.ActualQty = new Decimal?(0M);
      copy4.QtyToInvoice = new Decimal?(0M);
      copy4.CuryAmountToInvoice = new Decimal?(0M);
      copy4.AmountToInvoice = new Decimal?(0M);
      copy4.CuryDraftChangeOrderAmount = new Decimal?(0M);
      copy4.DraftChangeOrderAmount = new Decimal?(0M);
      copy4.DraftChangeOrderQty = new Decimal?(0M);
      copy4.CuryChangeOrderAmount = new Decimal?(0M);
      copy4.ChangeOrderAmount = new Decimal?(0M);
      copy4.ChangeOrderQty = new Decimal?(0M);
      copy4.CuryCommittedOrigAmount = new Decimal?(0M);
      copy4.CommittedOrigAmount = new Decimal?(0M);
      copy4.CommittedOrigQty = new Decimal?(0M);
      copy4.CuryCommittedAmount = new Decimal?(0M);
      copy4.CommittedAmount = new Decimal?(0M);
      copy4.CuryCommittedInvoicedAmount = new Decimal?(0M);
      copy4.CommittedInvoicedAmount = new Decimal?(0M);
      copy4.CommittedInvoicedQty = new Decimal?(0M);
      copy4.CuryCommittedOpenAmount = new Decimal?(0M);
      copy4.CommittedOpenAmount = new Decimal?(0M);
      copy4.CommittedOpenQty = new Decimal?(0M);
      copy4.CommittedQty = new Decimal?(0M);
      copy4.CommittedReceivedQty = new Decimal?(0M);
      copy4.CompletedPct = new Decimal?(0M);
      copy4.CuryCostAtCompletion = new Decimal?(0M);
      copy4.CostAtCompletion = new Decimal?(0M);
      copy4.CuryCostToComplete = new Decimal?(0M);
      copy4.CostToComplete = new Decimal?(0M);
      copy4.CuryInvoicedAmount = new Decimal?(0M);
      copy4.InvoicedAmount = new Decimal?(0M);
      copy4.InvoicedQty = new Decimal?(0M);
      copy4.CuryLastCostAtCompletion = new Decimal?(0M);
      copy4.LastCostAtCompletion = new Decimal?(0M);
      copy4.CuryLastCostToComplete = new Decimal?(0M);
      copy4.LastCostToComplete = new Decimal?(0M);
      copy4.LastPercentCompleted = new Decimal?(0M);
      copy4.PercentCompleted = new Decimal?(0M);
      copy4.CuryPrepaymentInvoiced = new Decimal?(0M);
      copy4.PrepaymentInvoiced = new Decimal?(0M);
      copy4.CuryDraftRetainedAmount = new Decimal?(0M);
      copy4.DraftRetainedAmount = new Decimal?(0M);
      copy4.CuryRetainedAmount = new Decimal?(0M);
      copy4.RetainedAmount = new Decimal?(0M);
      copy4.CuryTotalRetainedAmount = new Decimal?(0M);
      copy4.TotalRetainedAmount = new Decimal?(0M);
      copy4.LineCntr = new int?();
      copy4.NoteID = new Guid?();
      copy4.CuryInfoID = pmProject.CuryInfoID;
      copy4.RevenueTaskID = new int?();
      if (project.ChangeOrderWorkflow.GetValueOrDefault())
      {
        copy4.RevisedQty = new Decimal?();
        copy4.RevisedAmount = new Decimal?();
        copy4.CuryRevisedAmount = new Decimal?();
      }
      PMRevenueBudget newItem = ((PXSelectBase<PMRevenueBudget>) instance.RevenueBudget).Insert(copy4);
      ((PXSelectBase) instance.RevenueBudget).Cache.SetValueExt<PMRevenueBudget.progressBillingBase>((object) newItem, (object) sourceItem.ProgressBillingBase);
      this.OnCopyPasteRevenueBudgetInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<PMCostBudget> pxResult in ((PXSelectBase<PMCostBudget>) this.CostBudget).Select(Array.Empty<object>()))
    {
      PMCostBudget sourceItem = PXResult<PMCostBudget>.op_Implicit(pxResult);
      PMCostBudget copy5 = (PMCostBudget) ((PXSelectBase) this.CostBudget).Cache.CreateCopy((object) sourceItem);
      copy5.ProjectID = pmProject.ContractID;
      copy5.ProjectTaskID = new int?(taskMap[sourceItem.TaskID.Value]);
      copy5.CuryActualAmount = new Decimal?(0M);
      copy5.ActualAmount = new Decimal?(0M);
      copy5.ActualQty = new Decimal?(0M);
      copy5.CuryAmountToInvoice = new Decimal?(0M);
      copy5.QtyToInvoice = new Decimal?(0M);
      copy5.AmountToInvoice = new Decimal?(0M);
      copy5.CuryDraftChangeOrderAmount = new Decimal?(0M);
      copy5.DraftChangeOrderAmount = new Decimal?(0M);
      copy5.DraftChangeOrderQty = new Decimal?(0M);
      copy5.CuryChangeOrderAmount = new Decimal?(0M);
      copy5.ChangeOrderAmount = new Decimal?(0M);
      copy5.ChangeOrderQty = new Decimal?(0M);
      copy5.CuryCommittedOrigAmount = new Decimal?(0M);
      copy5.CommittedOrigAmount = new Decimal?(0M);
      copy5.CommittedOrigQty = new Decimal?(0M);
      copy5.CuryCommittedAmount = new Decimal?(0M);
      copy5.CommittedAmount = new Decimal?(0M);
      copy5.CuryCommittedInvoicedAmount = new Decimal?(0M);
      copy5.CommittedInvoicedAmount = new Decimal?(0M);
      copy5.CommittedInvoicedQty = new Decimal?(0M);
      copy5.CuryCommittedOpenAmount = new Decimal?(0M);
      copy5.CommittedOpenAmount = new Decimal?(0M);
      copy5.CommittedOpenQty = new Decimal?(0M);
      copy5.CommittedQty = new Decimal?(0M);
      copy5.CommittedReceivedQty = new Decimal?(0M);
      copy5.CompletedPct = new Decimal?(0M);
      copy5.CuryCostAtCompletion = new Decimal?(0M);
      copy5.CostAtCompletion = new Decimal?(0M);
      copy5.CuryCostToComplete = new Decimal?(0M);
      copy5.CostToComplete = new Decimal?(0M);
      copy5.CuryInvoicedAmount = new Decimal?(0M);
      copy5.InvoicedAmount = new Decimal?(0M);
      copy5.CuryLastCostAtCompletion = new Decimal?(0M);
      copy5.LastCostAtCompletion = new Decimal?(0M);
      copy5.CuryLastCostToComplete = new Decimal?(0M);
      copy5.LastCostToComplete = new Decimal?(0M);
      copy5.LastPercentCompleted = new Decimal?(0M);
      copy5.PercentCompleted = new Decimal?(0M);
      copy5.CuryPrepaymentInvoiced = new Decimal?(0M);
      copy5.PrepaymentInvoiced = new Decimal?(0M);
      copy5.CuryDraftRetainedAmount = new Decimal?(0M);
      copy5.DraftRetainedAmount = new Decimal?(0M);
      copy5.CuryRetainedAmount = new Decimal?(0M);
      copy5.RetainedAmount = new Decimal?(0M);
      copy5.CuryTotalRetainedAmount = new Decimal?(0M);
      copy5.TotalRetainedAmount = new Decimal?(0M);
      copy5.LineCntr = new int?();
      copy5.NoteID = new Guid?();
      copy5.CuryInfoID = pmProject.CuryInfoID;
      if (project.ChangeOrderWorkflow.GetValueOrDefault())
      {
        copy5.RevisedQty = new Decimal?();
        copy5.RevisedAmount = new Decimal?();
        copy5.CuryRevisedAmount = new Decimal?();
      }
      if (sourceItem.RevenueTaskID.HasValue)
      {
        copy5.RevenueTaskID = new int?(taskMap[sourceItem.RevenueTaskID.Value]);
      }
      else
      {
        PMTask dirty = PMTask.PK.FindDirty((PXGraph) instance, copy5.ProjectID, copy5.ProjectTaskID);
        if (dirty != null && dirty.Type == "CostRev")
          copy5.RevenueTaskID = copy5.ProjectTaskID;
      }
      PMCostBudget newItem = ((PXSelectBase<PMCostBudget>) instance.CostBudget).Insert(copy5);
      this.OnCopyPasteCostBudgetInserted(pmProject, newItem, sourceItem);
    }
    ((PXSelectBase) instance.Project).Cache.SetValueExt<PMProject.baseCuryID>((object) pmProject, (object) project.BaseCuryID);
    ((PXSelectBase) instance.Project).Cache.SetValueExt<PMProject.curyID>((object) pmProject, (object) project.CuryID);
    ((PXSelectBase) instance.Project).Cache.SetValueExt<PMProject.curyIDCopy>((object) pmProject, (object) project.CuryIDCopy);
    foreach (KeyValuePair<string, object> keyValuePair in ((IEnumerable<string>) ((PXSelectBase) this.Project).Cache.Fields).Where<string>(new Func<string, bool>(((PXSelectBase) this.Project).Cache.IsKvExtAttribute)).ToDictionary<string, string, object>((Func<string, string>) (udField => udField), (Func<string, object>) (udField => ((PXFieldState) ((PXSelectBase) this.Project).Cache.GetValueExt((object) project, udField))?.Value)))
    {
      string str2;
      object obj1;
      EnumerableExtensions.Deconstruct<string, object>(keyValuePair, ref str2, ref obj1);
      string str3 = str2;
      object obj2 = obj1;
      ((PXSelectBase) instance.Project).Cache.SetValueExt((object) pmProject, str3, obj2);
    }
    foreach (PXResult<EPEmployeeContract> pxResult in ((PXSelectBase<EPEmployeeContract>) this.EmployeeContract).Select(Array.Empty<object>()))
    {
      EPEmployeeContract sourceItem = PXResult<EPEmployeeContract>.op_Implicit(pxResult);
      EPEmployeeContract copy6 = (EPEmployeeContract) ((PXSelectBase) this.EmployeeContract).Cache.CreateCopy((object) sourceItem);
      copy6.ContractID = pmProject.ContractID;
      EPEmployeeContract newItem = ((PXSelectBase<EPEmployeeContract>) instance.EmployeeContract).Insert(copy6);
      this.OnCopyPasteEmployeeContractInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<EPContractRate> pxResult in ((PXSelectBase<EPContractRate>) this.ContractRates).Select(Array.Empty<object>()))
    {
      EPContractRate sourceItem = PXResult<EPContractRate>.op_Implicit(pxResult);
      EPContractRate copy7 = (EPContractRate) ((PXSelectBase) this.ContractRates).Cache.CreateCopy((object) sourceItem);
      copy7.ContractID = pmProject.ContractID;
      EPContractRate newItem = ((PXSelectBase<EPContractRate>) instance.ContractRates).Insert(copy7);
      this.OnCopyPasteContractRateInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<EPEquipmentRate> pxResult in ((PXSelectBase<EPEquipmentRate>) this.EquipmentRates).Select(Array.Empty<object>()))
    {
      EPEquipmentRate sourceItem = PXResult<EPEquipmentRate>.op_Implicit(pxResult);
      EPEquipmentRate copy8 = (EPEquipmentRate) ((PXSelectBase) this.EquipmentRates).Cache.CreateCopy((object) sourceItem);
      copy8.ProjectID = pmProject.ContractID;
      copy8.NoteID = new Guid?();
      EPEquipmentRate newItem = ((PXSelectBase<EPEquipmentRate>) instance.EquipmentRates).Insert(copy8);
      this.OnCopyPasteEquipmentRateInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<PMProjectContact> pxResult in ((PXSelectBase<PMProjectContact>) this.ProjectContacts).Select(Array.Empty<object>()))
    {
      PMProjectContact sourceItem = PXResult<PMProjectContact>.op_Implicit(pxResult);
      int? contactId = sourceItem.ContactID;
      sourceItem.ContactID = new int?();
      PMProjectContact copy9 = (PMProjectContact) ((PXSelectBase) this.ProjectContacts).Cache.CreateCopy((object) sourceItem);
      copy9.ProjectID = new int?();
      copy9.NoteID = new Guid?();
      copy9.IsActive = new bool?();
      PMProjectContact newItem = ((PXSelectBase<PMProjectContact>) instance.ProjectContacts).Insert(copy9);
      newItem.ContactID = contactId;
      this.OnCopyPasteProjectContactInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<PMAccountTask> pxResult in ((PXSelectBase<PMAccountTask>) this.Accounts).Select(Array.Empty<object>()))
    {
      PMAccountTask sourceItem = PXResult<PMAccountTask>.op_Implicit(pxResult);
      PMAccountTask copy10 = (PMAccountTask) ((PXSelectBase) this.Accounts).Cache.CreateCopy((object) sourceItem);
      copy10.ProjectID = pmProject.ContractID;
      copy10.TaskID = new int?(taskMap[sourceItem.TaskID.Value]);
      copy10.NoteID = new Guid?();
      PMAccountTask newItem = ((PXSelectBase<PMAccountTask>) instance.Accounts).Insert(copy10);
      this.OnCopyPasteAccountTaskInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<PMRetainageStep> pxResult in ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Select(Array.Empty<object>()))
    {
      PMRetainageStep pmRetainageStep = PXResult<PMRetainageStep>.op_Implicit(pxResult);
      pmRetainageStep.ProjectID = ((PXSelectBase<PMProject>) instance.Project).Current.ContractID;
      pmRetainageStep.NoteID = new Guid?();
      ((PXGraph) instance).Caches[typeof (PMRetainageStep)].Insert((object) pmRetainageStep);
    }
    ((PXSelectBase) instance.NotificationSources).Cache.Clear();
    ((PXSelectBase) instance.NotificationRecipients).Cache.Clear();
    foreach (PXResult<NotificationSource> pxResult1 in ((PXSelectBase<NotificationSource>) this.NotificationSources).Select(Array.Empty<object>()))
    {
      NotificationSource sourceItem = PXResult<NotificationSource>.op_Implicit(pxResult1);
      int? sourceId = sourceItem.SourceID;
      sourceItem.SourceID = new int?();
      sourceItem.RefNoteID = new Guid?();
      NotificationSource newItem = ((PXSelectBase<NotificationSource>) instance.NotificationSources).Insert(sourceItem);
      foreach (PXResult<NotificationRecipient> pxResult2 in ((PXSelectBase<NotificationRecipient>) this.NotificationRecipients).Select(new object[1]
      {
        (object) sourceId
      }))
      {
        NotificationRecipient notificationRecipient = PXResult<NotificationRecipient>.op_Implicit(pxResult2);
        if (notificationRecipient.ContactType == "P" || notificationRecipient.ContactType == "E")
        {
          notificationRecipient.NotificationID = new int?();
          notificationRecipient.SourceID = newItem.SourceID;
          notificationRecipient.RefNoteID = new Guid?();
          ((PXSelectBase<NotificationRecipient>) instance.NotificationRecipients).Insert(notificationRecipient);
        }
      }
      this.OnCopyPasteNotificationSourceInserted(pmProject, newItem, sourceItem);
    }
    ((PXGraph) instance).Views.Caches.Add(typeof (PMRecurringItem));
    foreach (PXResult<PMRecurringItem> pxResult in PXSelectBase<PMRecurringItem, PXSelect<PMRecurringItem, Where<PMRecurringItem.projectID, Equal<Required<PMRecurringItem.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMRecurringItem sourceItem = PXResult<PMRecurringItem>.op_Implicit(pxResult);
      PMRecurringItem copy11 = (PMRecurringItem) ((PXGraph) this).Caches[typeof (PMRecurringItem)].CreateCopy((object) sourceItem);
      copy11.ProjectID = pmProject.ContractID;
      copy11.TaskID = new int?(taskMap[sourceItem.TaskID.Value]);
      copy11.Used = new Decimal?();
      copy11.LastBilledDate = new DateTime?();
      copy11.LastBilledQty = new Decimal?();
      copy11.NoteID = new Guid?();
      PMRecurringItem newItem = (PMRecurringItem) ((PXGraph) instance).Caches[typeof (PMRecurringItem)].Insert((object) copy11);
      this.OnCopyPasteRecurringItemInserted(pmProject, newItem, sourceItem);
    }
    instance.Answers.CopyAllAttributes((object) pmProject, (object) project);
    this.OnCopyPasteCompleted(pmProject, project);
    ProjectAccountingService.OpenInTheSameWindow((PXGraph) instance);
  }

  protected virtual void OnCopyPasteTasksInserted(ProjectEntry target, Dictionary<int, int> taskMap)
  {
  }

  protected virtual void OnCopyPasteTaskInserted(
    PMProject target,
    PMTask newItem,
    PMTask sourceItem)
  {
  }

  protected virtual void OnCopyPasteRevenueBudgetInserted(
    PMProject target,
    PMRevenueBudget newItem,
    PMRevenueBudget sourceItem)
  {
  }

  protected virtual void OnCopyPasteCostBudgetInserted(
    PMProject target,
    PMCostBudget newItem,
    PMCostBudget sourceItem)
  {
  }

  protected virtual void OnCopyPasteEmployeeContractInserted(
    PMProject target,
    EPEmployeeContract newItem,
    EPEmployeeContract sourceItem)
  {
  }

  protected virtual void OnCopyPasteContractRateInserted(
    PMProject target,
    EPContractRate newItem,
    EPContractRate sourceItem)
  {
  }

  protected virtual void OnCopyPasteEquipmentRateInserted(
    PMProject target,
    EPEquipmentRate newItem,
    EPEquipmentRate sourceItem)
  {
  }

  protected virtual void OnCopyPasteProjectContactInserted(
    PMProject target,
    PMProjectContact newItem,
    PMProjectContact sourceItem)
  {
  }

  protected virtual void OnCopyPasteAccountTaskInserted(
    PMProject target,
    PMAccountTask newItem,
    PMAccountTask sourceItem)
  {
  }

  protected virtual void OnCopyPasteNotificationSourceInserted(
    PMProject target,
    NotificationSource newItem,
    NotificationSource sourceItem)
  {
  }

  protected virtual void OnCopyPasteRecurringItemInserted(
    PMProject target,
    PMRecurringItem newItem,
    PMRecurringItem sourceItem)
  {
  }

  protected virtual void OnCopyPasteCompleted(PMProject target, PMProject source)
  {
  }

  protected virtual void OnDefaultFromTemplateTasksInserted(
    PMProject project,
    PMProject template,
    Dictionary<int, int> taskMap)
  {
  }

  protected virtual void OnCreateTemplateTasksInserted(
    TemplateMaint target,
    PMProject template,
    Dictionary<int, int> taskMap)
  {
  }

  /// <summary>
  /// During Paste of Copied Project this propert holds the reference to the Graph with source data.
  /// </summary>
  public ProjectEntry CopySource { get; private set; }

  public virtual bool BudgetEditable()
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      bool? nullable = ((PXSelectBase<PMProject>) this.Project).Current.BudgetFinalized;
      if (!nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<PMProject>) this.Project).Current.Rejected;
        return !nullable.GetValueOrDefault();
      }
    }
    return false;
  }

  public virtual bool RevisedEditable()
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return true;
    bool? nullable = ((PXSelectBase<PMProject>) this.Project).Current.ChangeOrderWorkflow;
    if (nullable.GetValueOrDefault())
      return false;
    nullable = ((PXSelectBase<PMProject>) this.Project).Current.Rejected;
    return !nullable.GetValueOrDefault();
  }

  public virtual bool CreatePurchaseOrderVisible()
  {
    return this.CostCommitmentTrackingEnabled() && !this.IsUserNumberingOn(((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current?.RegularPONumberingID);
  }

  public virtual bool CommitmentLockVisible()
  {
    return this.CostCommitmentTrackingEnabled() && ((PXGraph) this).GetExtension<ProjectEntry_ChangeOrderExt>() != null && ((PXGraph) this).GetExtension<ProjectEntry_ChangeOrderExt>().ChangeOrderEnabled();
  }

  public virtual void ConfigureTasksTab(bool isProjectEditable)
  {
    PXCache cache = ((PXSelectBase) this.Tasks).Cache;
    cache.AllowInsert = isProjectEditable;
    cache.AllowUpdate = isProjectEditable;
    cache.AllowDelete = isProjectEditable;
    ((PXAction) this.activateTasks).SetEnabled(isProjectEditable);
    ((PXAction) this.completeTasks).SetEnabled(isProjectEditable);
    ((PXAction) this.viewAddCommonTask).SetEnabled(isProjectEditable);
    foreach (System.Type hiddenTaskField in this.GetHiddenTaskFields())
      PXUIFieldAttribute.SetVisible(cache, hiddenTaskField.Name, false);
  }

  public virtual List<string> ValidateProjectClosure(int? projectID)
  {
    List<string> stringList = new List<string>();
    stringList.AddRange((IEnumerable<string>) this.ValidatePMDocumentsClosure(projectID));
    stringList.AddRange((IEnumerable<string>) this.ValidateEPDocumentsClosure(projectID));
    stringList.AddRange((IEnumerable<string>) this.ValidateARDocumentsClosure(projectID));
    stringList.AddRange((IEnumerable<string>) this.ValidateAPDocumentsClosure(projectID));
    IEnumerable<PX.Objects.GL.GLTran> firstTableItems1 = PXSelectBase<PX.Objects.GL.GLTran, PXViewOf<PX.Objects.GL.GLTran>.BasedOn<SelectFromBase<PX.Objects.GL.GLTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.GLTran.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.GL.GLTran.released, IBqlBool>.IsEqual<False>>>.Aggregate<To<GroupBy<PX.Objects.GL.GLTran.module>, GroupBy<PX.Objects.GL.GLTran.batchNbr>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems1.Select<PX.Objects.GL.GLTran, string>((Func<PX.Objects.GL.GLTran, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the related journal transaction is unreleased. Journal transaction: {0} {1}", new object[2]
    {
      (object) x.Module,
      (object) x.RefNbr
    }))));
    IEnumerable<CAAdj> firstTableItems2 = PXSelectBase<CAAdj, PXViewOf<CAAdj>.BasedOn<SelectFromBase<CAAdj, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CASplit>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CAAdj.adjRefNbr, Equal<CASplit.adjRefNbr>>>>>.And<BqlOperand<CAAdj.adjTranType, IBqlString>.IsEqual<CASplit.adjTranType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CASplit.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<CAAdj.released, IBqlBool>.IsEqual<False>>>.Aggregate<To<GroupBy<CAAdj.adjRefNbr>, GroupBy<CAAdj.adjTranType>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems2.Select<CAAdj, string>((Func<CAAdj, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} cash transaction related to the project is unreleased.", new object[1]
    {
      (object) x.AdjRefNbr
    }))));
    IEnumerable<DRSchedule> firstTableItems3 = PXSelectBase<DRSchedule, PXViewOf<DRSchedule>.BasedOn<SelectFromBase<DRSchedule, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<DRScheduleDetail>.On<BqlOperand<DRScheduleDetail.scheduleID, IBqlInt>.IsEqual<DRSchedule.scheduleID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DRSchedule.projectID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DRSchedule.isDraft, Equal<True>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DRScheduleDetail.isOpen, Equal<True>>>>>.And<BqlOperand<DRScheduleDetail.isResidual, IBqlBool>.IsEqual<False>>>>>.Aggregate<To<GroupBy<DRSchedule.scheduleID>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems3.Select<DRSchedule, string>((Func<DRSchedule, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because revenue or expenses related to the project have not been fully recognized for the {0} deferral schedule.", new object[1]
    {
      (object) x.ScheduleNbr
    }))));
    return stringList;
  }

  public virtual List<string> ValidatePMDocumentsClosure(int? projectID)
  {
    List<string> stringList = new List<string>();
    IEnumerable<PMProforma> firstTableItems1 = PXSelectBase<PMProforma, PXViewOf<PMProforma>.BasedOn<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProforma.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMProforma.released, IBqlBool>.IsEqual<False>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems1.Select<PMProforma, string>((Func<PMProforma, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} pro forma invoice related to the project is unreleased.", new object[1]
    {
      (object) x.RefNbr
    }))));
    IEnumerable<PMTran> firstTableItems2 = PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMTran.released, IBqlBool>.IsEqual<False>>>.Aggregate<To<GroupBy<PMTran.tranType>, GroupBy<PMTran.refNbr>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems2.Select<PMTran, string>((Func<PMTran, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} project transaction related to the project is unreleased.", new object[1]
    {
      (object) x.RefNbr
    }))));
    IEnumerable<PMCostProjectionByDate> firstTableItems3 = PXSelectBase<PMCostProjectionByDate, PXViewOf<PMCostProjectionByDate>.BasedOn<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMCostProjectionByDate.released, IBqlBool>.IsEqual<False>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems3.Select<PMCostProjectionByDate, string>((Func<PMCostProjectionByDate, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} cost projection by date related to the project is unreleased.", new object[1]
    {
      (object) x.RefNbr
    }))));
    IEnumerable<PMTimeActivity> firstTableItems4 = PXSelectBase<PMTimeActivity, PXViewOf<PMTimeActivity>.BasedOn<SelectFromBase<PMTimeActivity, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTimeActivity.projectID, Equal<P.AsInt>>>>, And<BqlOperand<PMTimeActivity.trackTime, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PMTimeActivity.released, IBqlBool>.IsEqual<False>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems4.Select<PMTimeActivity, string>((Func<PMTimeActivity, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} time activity related to the project is unreleased.", new object[1]
    {
      (object) x.Summary
    }))));
    return stringList;
  }

  public virtual List<string> ValidateARDocumentsClosure(int? projectID)
  {
    List<string> stringList = new List<string>();
    string[] strArray = new string[6]
    {
      "INV",
      "DRM",
      "CRM",
      "FCH",
      "CSL",
      "RCS"
    };
    IEnumerable<PX.Objects.AR.ARInvoice> firstTableItems1 = PXSelectBase<PX.Objects.AR.ARInvoice, PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.ARTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.refNbr, Equal<PX.Objects.AR.ARTran.refNbr>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.docType, IBqlString>.IsEqual<PX.Objects.AR.ARTran.tranType>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.projectID, Equal<P.AsInt>>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.docType, IBqlString>.IsIn<P.AsString>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.released, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.voided, Equal<False>>>>>.And<BqlOperand<PX.Objects.AR.ARRegister.rejected, IBqlBool>.IsEqual<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.released, Equal<True>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.openDoc, IBqlBool>.IsEqual<True>>>>>.Aggregate<To<GroupBy<PX.Objects.AR.ARInvoice.refNbr>, GroupBy<PX.Objects.AR.ARInvoice.docType>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
    {
      (object) projectID,
      (object) strArray
    }).FirstTableItems;
    stringList.AddRange(firstTableItems1.Select<PX.Objects.AR.ARInvoice, string>((Func<PX.Objects.AR.ARInvoice, string>) (x =>
    {
      string str;
      switch (x.DocType)
      {
        case "CSL":
          str = PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} cash sale related to the project is unreleased.", new object[1]
          {
            (object) x.RefNbr
          });
          break;
        case "RCS":
          str = PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} cash return related to the project is unreleased.", new object[1]
          {
            (object) x.RefNbr
          });
          break;
        default:
          str = PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the related accounts receivable document is unreleased or unpaid. AR document: {0} {1}", new object[2]
          {
            (object) x.DocType,
            (object) x.RefNbr
          });
          break;
      }
      return str;
    })));
    IEnumerable<ARAdjust> firstTableItems2 = PXSelectBase<ARAdjust, PXViewOf<ARAdjust>.BasedOn<SelectFromBase<ARAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.ARPayment>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<PX.Objects.AR.ARPayment.docType>>>>>.And<BqlOperand<ARAdjust.adjgRefNbr, IBqlString>.IsEqual<PX.Objects.AR.ARPayment.refNbr>>>>, FbqlJoins.Inner<PX.Objects.AR.ARTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdRefNbr, Equal<PX.Objects.AR.ARTran.refNbr>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<PX.Objects.AR.ARTran.tranType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<ARAdjust.released, IBqlBool>.IsEqual<False>>>.Aggregate<To<GroupBy<ARAdjust.adjdRefNbr>, GroupBy<ARAdjust.adjdDocType>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems2.Select<ARAdjust, string>((Func<ARAdjust, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because a payment application related to the accounts receivable document is unreleased. AR document: {0} {1}", new object[2]
    {
      (object) x.AdjdDocType,
      (object) x.AdjdRefNbr
    }))));
    return stringList;
  }

  public virtual List<string> ValidateAPDocumentsClosure(int? projectID)
  {
    List<string> stringList = new List<string>();
    string[] strArray = new string[7]
    {
      "INV",
      "ACR",
      "ADR",
      "PPR",
      "QCK",
      "VQC",
      "RQC"
    };
    IEnumerable<PX.Objects.AP.APRegister> firstTableItems1 = PXSelectBase<PX.Objects.AP.APRegister, PXViewOf<PX.Objects.AP.APRegister>.BasedOn<SelectFromBase<PX.Objects.AP.APRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AP.APTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APRegister.refNbr, Equal<PX.Objects.AP.APTran.refNbr>>>>>.And<BqlOperand<PX.Objects.AP.APRegister.docType, IBqlString>.IsEqual<PX.Objects.AP.APTran.tranType>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APTran.projectID, Equal<P.AsInt>>>>, And<BqlOperand<PX.Objects.AP.APRegister.docType, IBqlString>.IsIn<P.AsString>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APRegister.released, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APRegister.voided, Equal<False>>>>>.And<BqlOperand<PX.Objects.AP.APRegister.rejected, IBqlBool>.IsEqual<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APRegister.released, Equal<True>>>>>.And<BqlOperand<PX.Objects.AP.APRegister.openDoc, IBqlBool>.IsEqual<True>>>>>.Aggregate<To<GroupBy<PX.Objects.AP.APRegister.refNbr>, GroupBy<PX.Objects.AP.APRegister.docType>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
    {
      (object) projectID,
      (object) strArray
    }).FirstTableItems;
    stringList.AddRange(firstTableItems1.Select<PX.Objects.AP.APRegister, string>((Func<PX.Objects.AP.APRegister, string>) (x =>
    {
      string str;
      switch (x.DocType)
      {
        case "QCK":
          str = PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} cash purchase related to the project is unreleased.", new object[1]
          {
            (object) x.RefNbr
          });
          break;
        case "VQC":
          str = PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} voided cash purchase related to the project is unreleased.", new object[1]
          {
            (object) x.RefNbr
          });
          break;
        case "RQC":
          str = PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} cash return related to the project is unreleased.", new object[1]
          {
            (object) x.RefNbr
          });
          break;
        default:
          str = PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the related accounts payable document is unreleased or unpaid. AP document: {0} {1}", new object[2]
          {
            (object) x.DocType,
            (object) x.RefNbr
          });
          break;
      }
      return str;
    })));
    IEnumerable<APAdjust> firstTableItems2 = PXSelectBase<APAdjust, PXViewOf<APAdjust>.BasedOn<SelectFromBase<APAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AP.APPayment>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APAdjust.adjgDocType, Equal<PX.Objects.AP.APPayment.docType>>>>>.And<BqlOperand<APAdjust.adjgRefNbr, IBqlString>.IsEqual<PX.Objects.AP.APPayment.refNbr>>>>, FbqlJoins.Inner<PX.Objects.AP.APTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APTran.refNbr>>>>>.And<BqlOperand<APAdjust.adjdDocType, IBqlString>.IsEqual<PX.Objects.AP.APTran.tranType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APTran.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<APAdjust.released, IBqlBool>.IsEqual<False>>>.Aggregate<To<GroupBy<APAdjust.adjdRefNbr, GroupBy<APAdjust.adjdDocType>>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems2.Select<APAdjust, string>((Func<APAdjust, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because a payment application related to the accounts payable document is unreleased. AP document: {0} {1}", new object[2]
    {
      (object) x.AdjdDocType,
      (object) x.AdjdRefNbr
    }))));
    return stringList;
  }

  public virtual List<string> ValidateEPDocumentsClosure(int? projectID)
  {
    List<string> stringList = new List<string>();
    IEnumerable<EPExpenseClaimDetails> firstTableItems1 = PXSelectBase<EPExpenseClaimDetails, PXViewOf<EPExpenseClaimDetails>.BasedOn<SelectFromBase<EPExpenseClaimDetails, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPExpenseClaimDetails.contractID, Equal<P.AsInt>>>>>.And<BqlOperand<EPExpenseClaimDetails.released, IBqlBool>.IsEqual<False>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems1.Select<EPExpenseClaimDetails, string>((Func<EPExpenseClaimDetails, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} expense receipt related to the project is unprocessed.", new object[1]
    {
      (object) x.ClaimDetailCD
    }))));
    IEnumerable<EPExpenseClaim> firstTableItems2 = PXSelectBase<EPExpenseClaim, PXViewOf<EPExpenseClaim>.BasedOn<SelectFromBase<EPExpenseClaim, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<EPExpenseClaimDetails>.On<BqlOperand<EPExpenseClaimDetails.refNbr, IBqlString>.IsEqual<EPExpenseClaim.refNbr>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPExpenseClaimDetails.contractID, Equal<P.AsInt>>>>>.And<BqlOperand<EPExpenseClaim.released, IBqlBool>.IsEqual<False>>>.Aggregate<To<GroupBy<EPExpenseClaim.refNbr>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems2.Select<EPExpenseClaim, string>((Func<EPExpenseClaim, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} expense claim related to the project is unreleased.", new object[1]
    {
      (object) x.RefNbr
    }))));
    return stringList;
  }

  public virtual void MappingTaskPropertiesInit(
    object sender,
    PXImportAttribute.MappingPropertiesInitEventArgs e)
  {
    foreach (System.Type hiddenTaskField in this.GetHiddenTaskFields())
    {
      e.Names.Add(hiddenTaskField.Name);
      e.DisplayNames.Add(PXUIFieldAttribute.GetDisplayName(((PXSelectBase) this.Tasks).Cache, hiddenTaskField.Name));
    }
  }

  public virtual PMTask CopyTask(
    PMTask task,
    int ProjectID,
    ProjectEntry.DefaultFromTemplateSettings settings)
  {
    task = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMTask.projectID>>, And<PMTask.taskCD, Equal<Current<PMTask.taskCD>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) task
    }, Array.Empty<object>()));
    PMTask row = this.CopyTask(task, ((PXSelectBase<PMProject>) this.Project).Current, settings.CopyNotes, settings.CopyFiles);
    if (settings.CopyAttributes)
      this.TaskAnswers.CopyAllAttributes((object) row, (object) task);
    if (settings.CopyBudget)
    {
      PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Required<PMCostBudget.projectID>>, And<PMCostBudget.projectTaskID, Equal<Required<PMCostBudget.projectTaskID>>, And<PMCostBudget.type, Equal<AccountType.expense>>>>> pxSelect1 = new PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Required<PMCostBudget.projectID>>, And<PMCostBudget.projectTaskID, Equal<Required<PMCostBudget.projectTaskID>>, And<PMCostBudget.type, Equal<AccountType.expense>>>>>((PXGraph) this);
      foreach (PXResult<PMCostBudget> pxResult in ((PXSelectBase<PMCostBudget>) pxSelect1).Select(new object[2]
      {
        (object) task.ProjectID,
        (object) task.TaskID
      }))
      {
        PMCostBudget sourceItem = PXResult<PMCostBudget>.op_Implicit(pxResult);
        if (!sourceItem.RevenueTaskID.HasValue)
        {
          PMCostBudget copy = (PMCostBudget) ((PXSelectBase) pxSelect1).Cache.CreateCopy((object) sourceItem);
          copy.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
          copy.ProjectTaskID = row.TaskID;
          copy.RevisedQty = sourceItem.Qty;
          copy.CuryRevisedAmount = sourceItem.CuryAmount;
          copy.NoteID = new Guid?();
          this.OnCopyPasteCostBudgetInserted(((PXSelectBase<PMProject>) this.Project).Current, ((PXSelectBase<PMCostBudget>) this.CostBudget).Insert(copy), sourceItem);
        }
      }
      PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Required<PMRevenueBudget.projectID>>, And<PMRevenueBudget.projectTaskID, Equal<Required<PMRevenueBudget.projectTaskID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>> pxSelect2 = new PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Required<PMRevenueBudget.projectID>>, And<PMRevenueBudget.projectTaskID, Equal<Required<PMRevenueBudget.projectTaskID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>>((PXGraph) this);
      if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel != "T")
      {
        foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) pxSelect2).Select(new object[2]
        {
          (object) task.ProjectID,
          (object) task.TaskID
        }))
        {
          PMRevenueBudget sourceItem = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
          PMRevenueBudget copy = (PMRevenueBudget) ((PXSelectBase) pxSelect2).Cache.CreateCopy((object) sourceItem);
          copy.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
          copy.ProjectTaskID = row.TaskID;
          copy.RevisedQty = sourceItem.Qty;
          copy.CuryRevisedAmount = sourceItem.CuryAmount;
          copy.NoteID = new Guid?();
          copy.ProgressBillingBase = row.ProgressBillingBase;
          this.OnCopyPasteRevenueBudgetInserted(((PXSelectBase<PMProject>) this.Project).Current, ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Insert(copy), sourceItem);
        }
      }
      else
      {
        Dictionary<string, PMRevenueBudget> dictionary = new Dictionary<string, PMRevenueBudget>();
        foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) pxSelect2).Select(new object[2]
        {
          (object) task.ProjectID,
          (object) task.TaskID
        }))
        {
          PMRevenueBudget pmRevenueBudget1 = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
          string key = $"{pmRevenueBudget1.ProjectTaskID}.{pmRevenueBudget1.AccountGroupID}";
          PMRevenueBudget pmRevenueBudget2 = (PMRevenueBudget) null;
          if (!dictionary.TryGetValue(key, out pmRevenueBudget2))
          {
            pmRevenueBudget1.InventoryID = new int?(PMInventorySelectorAttribute.EmptyInventoryID);
            pmRevenueBudget1.CostCodeID = new int?(CostCodeAttribute.GetDefaultCostCode());
            dictionary.Add(key, pmRevenueBudget1);
          }
          else
          {
            if (pmRevenueBudget1.UOM != pmRevenueBudget2.UOM || string.IsNullOrEmpty(pmRevenueBudget2.UOM))
            {
              pmRevenueBudget2.UOM = (string) null;
              pmRevenueBudget2.Qty = new Decimal?(0M);
              pmRevenueBudget2.RevisedQty = new Decimal?(0M);
            }
            Decimal? nullable;
            if (!string.IsNullOrEmpty(pmRevenueBudget2.UOM))
            {
              PMRevenueBudget pmRevenueBudget3 = pmRevenueBudget2;
              nullable = pmRevenueBudget3.Qty;
              Decimal valueOrDefault1 = pmRevenueBudget1.Qty.GetValueOrDefault();
              pmRevenueBudget3.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
              PMRevenueBudget pmRevenueBudget4 = pmRevenueBudget2;
              nullable = pmRevenueBudget4.RevisedQty;
              Decimal valueOrDefault2 = pmRevenueBudget1.Qty.GetValueOrDefault();
              pmRevenueBudget4.RevisedQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
            }
            PMRevenueBudget pmRevenueBudget5 = pmRevenueBudget2;
            nullable = pmRevenueBudget5.CuryAmount;
            Decimal valueOrDefault3 = pmRevenueBudget1.CuryAmount.GetValueOrDefault();
            pmRevenueBudget5.CuryAmount = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
            PMRevenueBudget pmRevenueBudget6 = pmRevenueBudget2;
            nullable = pmRevenueBudget6.CuryRevisedAmount;
            Decimal valueOrDefault4 = pmRevenueBudget1.CuryAmount.GetValueOrDefault();
            pmRevenueBudget6.CuryRevisedAmount = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
          }
        }
        foreach (PMRevenueBudget pmRevenueBudget in dictionary.Values)
        {
          pmRevenueBudget.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
          pmRevenueBudget.ProjectTaskID = row.TaskID;
          pmRevenueBudget.RevisedQty = pmRevenueBudget.Qty;
          pmRevenueBudget.CuryRevisedAmount = pmRevenueBudget.CuryAmount;
          pmRevenueBudget.NoteID = new Guid?();
          pmRevenueBudget.ProgressBillingBase = row.ProgressBillingBase;
          ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Insert(pmRevenueBudget);
        }
      }
    }
    if (settings.CopyRecurring)
    {
      foreach (PXResult<PMRecurringItem> pxResult in PXSelectBase<PMRecurringItem, PXSelect<PMRecurringItem, Where<PMRecurringItem.projectID, Equal<Current<PMTask.projectID>>, And<PMRecurringItem.taskID, Equal<Current<PMTask.taskID>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
      {
        (object) task
      }, Array.Empty<object>()))
      {
        PMRecurringItem sourceItem = PXResult<PMRecurringItem>.op_Implicit(pxResult);
        this.OnCopyPasteRecurringItemInserted(((PXSelectBase<PMProject>) this.Project).Current, ((PXSelectBase<PMRecurringItem>) this.BillingItems).Insert(new PMRecurringItem()
        {
          ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID,
          TaskID = row.TaskID,
          InventoryID = sourceItem.InventoryID,
          UOM = sourceItem.UOM,
          Description = sourceItem.Description,
          Amount = sourceItem.Amount,
          AccountSource = sourceItem.AccountSource,
          SubMask = sourceItem.SubMask,
          AccountID = sourceItem.AccountID,
          SubID = sourceItem.SubID,
          BranchID = sourceItem.BranchID,
          ResetUsage = sourceItem.ResetUsage,
          Included = sourceItem.Included
        }), sourceItem);
      }
    }
    return row;
  }

  private PMTask CopyTask(PMTask task, PMProject project, bool? copyNotes = true, bool? copyFiles = true)
  {
    PMTask pmTask1 = new PMTask()
    {
      TaskCD = task.TaskCD,
      ProjectID = new int?(project.ContractID.Value)
    };
    pmTask1.RateTableID = task.RateTableID ?? ((PXSelectBase<PMProject>) this.Project).Current.RateTableID;
    pmTask1.AllocationID = task.AllocationID ?? ((PXSelectBase<PMProject>) this.Project).Current.AllocationID;
    pmTask1.BillingID = task.BillingID ?? ((PXSelectBase<PMProject>) this.Project).Current.BillingID;
    pmTask1.Description = task.Description;
    PXDBLocalizableStringAttribute.CopyTranslations<PMTask.description, PMTask.description>(((PXSelectBase) this.Tasks).Cache, (object) task, ((PXSelectBase) this.Tasks).Cache, (object) pmTask1);
    pmTask1.ApproverID = task.ApproverID;
    pmTask1.TaxCategoryID = task.TaxCategoryID;
    pmTask1.BillingOption = task.BillingOption;
    PMTask pmTask2 = pmTask1;
    int? nullable1 = task.DefaultSalesAccountID;
    int? nullable2 = nullable1 ?? ((PXSelectBase<PMProject>) this.Project).Current.DefaultSalesAccountID;
    pmTask2.DefaultSalesAccountID = nullable2;
    PMTask pmTask3 = pmTask1;
    nullable1 = task.DefaultSalesSubID;
    int? nullable3 = nullable1 ?? ((PXSelectBase<PMProject>) this.Project).Current.DefaultSalesSubID;
    pmTask3.DefaultSalesSubID = nullable3;
    PMTask pmTask4 = pmTask1;
    nullable1 = task.DefaultExpenseAccountID;
    int? nullable4 = nullable1 ?? ((PXSelectBase<PMProject>) this.Project).Current.DefaultExpenseAccountID;
    pmTask4.DefaultExpenseAccountID = nullable4;
    PMTask pmTask5 = pmTask1;
    nullable1 = task.DefaultExpenseSubID;
    int? nullable5 = nullable1 ?? ((PXSelectBase<PMProject>) this.Project).Current.DefaultExpenseSubID;
    pmTask5.DefaultExpenseSubID = nullable5;
    PMTask pmTask6 = pmTask1;
    nullable1 = task.DefaultAccrualAccountID;
    int? nullable6 = nullable1 ?? ((PXSelectBase<PMProject>) this.Project).Current.DefaultAccrualAccountID;
    pmTask6.DefaultAccrualAccountID = nullable6;
    PMTask pmTask7 = pmTask1;
    nullable1 = task.DefaultAccrualSubID;
    int? nullable7 = nullable1 ?? ((PXSelectBase<PMProject>) this.Project).Current.DefaultAccrualSubID;
    pmTask7.DefaultAccrualSubID = nullable7;
    PMTask pmTask8 = pmTask1;
    nullable1 = task.DefaultBranchID;
    int? nullable8 = nullable1 ?? ((PXSelectBase<PMProject>) this.Project).Current.DefaultBranchID;
    pmTask8.DefaultBranchID = nullable8;
    pmTask1.DefaultCostCodeID = task.DefaultCostCodeID;
    pmTask1.CompletedPctMethod = task.CompletedPctMethod;
    pmTask1.BillSeparately = task.BillSeparately;
    pmTask1.WipAccountGroupID = task.WipAccountGroupID;
    pmTask1.TaxCategoryID = task.TaxCategoryID;
    pmTask1.VisibleInGL = task.VisibleInGL;
    pmTask1.VisibleInAP = task.VisibleInAP;
    pmTask1.VisibleInAR = task.VisibleInAR;
    pmTask1.VisibleInSO = task.VisibleInSO;
    pmTask1.VisibleInPO = task.VisibleInPO;
    pmTask1.VisibleInTA = task.VisibleInTA;
    pmTask1.VisibleInEA = task.VisibleInEA;
    pmTask1.VisibleInIN = task.VisibleInIN;
    pmTask1.VisibleInCA = task.VisibleInCA;
    pmTask1.VisibleInCR = task.VisibleInCR;
    pmTask1.IsActive = new bool?(task.IsActive.GetValueOrDefault());
    pmTask1.TemplateID = task.TaskID;
    pmTask1.IsDefault = task.IsDefault;
    pmTask1.ProgressBillingBase = task.ProgressBillingBase;
    pmTask1.Type = task.Type;
    PMTask newItem = ((PXSelectBase<PMTask>) this.Tasks).Insert(pmTask1);
    PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.Tasks).Cache, (object) task, ((PXSelectBase) this.Tasks).Cache, (object) newItem, copyNotes, copyFiles);
    this.OnCopyPasteTaskInserted(project, newItem, task);
    return newItem;
  }

  public virtual void DefaultFromTemplate(
    PMProject prj,
    int? templateID,
    ProjectEntry.DefaultFromTemplateSettings settings)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, templateID);
    if (pmProject == null)
      return;
    if (settings.CopyProperties && !this._isLoadFromTemplate)
      this.DefaultFromTemplateProjectSettings(prj, pmProject);
    if (settings.CopyAttributes)
    {
      this.Answers.CopyAllAttributes((object) prj, (object) pmProject);
      foreach (CSAnswers csAnswers in ((PXSelectBase) this.Answers).Cache.Inserted)
      {
        Guid? refNoteId = csAnswers.RefNoteID;
        Guid? noteId = pmProject.NoteID;
        if ((refNoteId.HasValue == noteId.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() == noteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          this.Answers.Delete(csAnswers);
      }
    }
    prj.StartDate = ((PXGraph) this).Accessinfo.BusinessDate;
    if (settings.CopyCurrency)
      ((PXSelectBase) this.Project).Cache.SetDefaultExt<PMProject.baseCuryID>((object) prj);
    Dictionary<string, PMTask> dictionary1 = GraphHelper.RowCast<PMTask>((IEnumerable) PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) pmProject
    }, Array.Empty<object>())).ToDictionary<PMTask, string>((Func<PMTask, string>) (t => t.TaskCD), (IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
    Dictionary<int, PMTask> dictionary2 = dictionary1.Values.ToDictionary<PMTask, int>((Func<PMTask, int>) (t => t.TaskID.Value));
    if (settings.CopyTasks)
    {
      foreach (PMTask task in dictionary1.Values)
      {
        if (task.AutoIncludeInPrj.GetValueOrDefault())
          this.CopyTask(task, prj);
      }
      if (this._isLoadFromTemplate)
        ((PXAction) this.Save).Press();
    }
    Dictionary<string, PMTask> dictionary3 = GraphHelper.RowCast<PMTask>((IEnumerable) PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<PMProject>) this.Project).Current
    }, Array.Empty<object>())).ToDictionary<PMTask, string>((Func<PMTask, string>) (t => t.TaskCD), (IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
    Dictionary<int, int> taskMap = new Dictionary<int, int>();
    if (settings.CopyTasks)
    {
      Dictionary<string, List<CSAnswers>> dictionary4 = new Dictionary<string, List<CSAnswers>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
      foreach (PXResult<CSAnswers, PMTask> pxResult in PXSelectBase<CSAnswers, PXSelectJoin<CSAnswers, InnerJoin<PMTask, On<CSAnswers.refNoteID, Equal<PMTask.noteID>>>, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) pmProject.ContractID
      }))
      {
        PMTask pmTask = PXResult<CSAnswers, PMTask>.op_Implicit(pxResult);
        CSAnswers csAnswers = PXResult<CSAnswers, PMTask>.op_Implicit(pxResult);
        List<CSAnswers> csAnswersList;
        if (!dictionary4.TryGetValue(pmTask.TaskCD, out csAnswersList))
        {
          csAnswersList = new List<CSAnswers>();
          dictionary4.Add(pmTask.TaskCD, csAnswersList);
        }
        csAnswersList.Add(csAnswers);
      }
      foreach (PMTask row in dictionary3.Values)
      {
        PMTask src;
        if (dictionary1.TryGetValue(row.TaskCD, out src))
        {
          Dictionary<int, int> dictionary5 = taskMap;
          int? taskId = src.TaskID;
          int key = taskId.Value;
          taskId = row.TaskID;
          int num = taskId.Value;
          dictionary5.Add(key, num);
          List<CSAnswers> csAnswersList;
          if (src.AutoIncludeInPrj.GetValueOrDefault() && settings.CopyAttributes && dictionary4.TryGetValue(row.TaskCD, out csAnswersList) && csAnswersList.Count > 0)
            this.TaskAnswers.CopyAllAttributes((object) row, (object) src);
        }
      }
      if (settings.CopyBudget)
      {
        Dictionary<string, List<PMRevenueBudget>> dictionary6 = new Dictionary<string, List<PMRevenueBudget>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
        foreach (PXResult<PMRevenueBudget> pxResult in PXSelectBase<PMRevenueBudget, PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Required<PMRevenueBudget.projectID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) pmProject.ContractID
        }))
        {
          PMRevenueBudget pmRevenueBudget = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
          PMTask pmTask;
          if (dictionary2.TryGetValue(pmRevenueBudget.ProjectTaskID.Value, out pmTask))
          {
            List<PMRevenueBudget> pmRevenueBudgetList;
            if (!dictionary6.TryGetValue(pmTask.TaskCD, out pmRevenueBudgetList))
            {
              pmRevenueBudgetList = new List<PMRevenueBudget>();
              dictionary6.Add(pmTask.TaskCD, pmRevenueBudgetList);
            }
            pmRevenueBudgetList.Add(pmRevenueBudget);
          }
        }
        if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel != "T")
        {
          foreach (KeyValuePair<string, List<PMRevenueBudget>> keyValuePair in dictionary6)
          {
            PMTask pmTask;
            if (dictionary3.TryGetValue(keyValuePair.Key, out pmTask))
            {
              foreach (PMRevenueBudget pmRevenueBudget in keyValuePair.Value)
              {
                pmRevenueBudget.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
                pmRevenueBudget.ProjectTaskID = pmTask.TaskID;
                pmRevenueBudget.RevisedQty = pmRevenueBudget.Qty;
                pmRevenueBudget.CuryRevisedAmount = pmRevenueBudget.CuryAmount;
                pmRevenueBudget.NoteID = new Guid?();
                pmRevenueBudget.RevenueTaskID = new int?();
                pmRevenueBudget.RevenueInventoryID = new int?();
                ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Insert(pmRevenueBudget);
              }
            }
          }
        }
        else
        {
          foreach (KeyValuePair<string, List<PMRevenueBudget>> keyValuePair in dictionary6)
          {
            PMTask pmTask;
            if (dictionary3.TryGetValue(keyValuePair.Key, out pmTask))
            {
              Dictionary<string, PMRevenueBudget> dictionary7 = new Dictionary<string, PMRevenueBudget>();
              foreach (PMRevenueBudget pmRevenueBudget1 in keyValuePair.Value)
              {
                string key = $"{pmRevenueBudget1.ProjectTaskID}.{pmRevenueBudget1.AccountGroupID}";
                PMRevenueBudget pmRevenueBudget2 = (PMRevenueBudget) null;
                if (!dictionary7.TryGetValue(key, out pmRevenueBudget2))
                {
                  pmRevenueBudget1.InventoryID = new int?(PMInventorySelectorAttribute.EmptyInventoryID);
                  pmRevenueBudget1.CostCodeID = new int?(CostCodeAttribute.GetDefaultCostCode());
                  dictionary7.Add(key, pmRevenueBudget1);
                }
                else
                {
                  if (pmRevenueBudget1.UOM != pmRevenueBudget2.UOM || string.IsNullOrEmpty(pmRevenueBudget2.UOM))
                  {
                    pmRevenueBudget2.UOM = (string) null;
                    pmRevenueBudget2.Qty = new Decimal?(0M);
                    pmRevenueBudget2.RevisedQty = new Decimal?(0M);
                  }
                  if (!string.IsNullOrEmpty(pmRevenueBudget2.UOM))
                  {
                    PMRevenueBudget pmRevenueBudget3 = pmRevenueBudget2;
                    Decimal? qty = pmRevenueBudget3.Qty;
                    Decimal valueOrDefault1 = pmRevenueBudget1.Qty.GetValueOrDefault();
                    pmRevenueBudget3.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
                    PMRevenueBudget pmRevenueBudget4 = pmRevenueBudget2;
                    Decimal? revisedQty = pmRevenueBudget4.RevisedQty;
                    Decimal valueOrDefault2 = pmRevenueBudget1.Qty.GetValueOrDefault();
                    pmRevenueBudget4.RevisedQty = revisedQty.HasValue ? new Decimal?(revisedQty.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
                  }
                  PMRevenueBudget pmRevenueBudget5 = pmRevenueBudget2;
                  Decimal? curyAmount = pmRevenueBudget5.CuryAmount;
                  Decimal valueOrDefault3 = pmRevenueBudget1.CuryAmount.GetValueOrDefault();
                  pmRevenueBudget5.CuryAmount = curyAmount.HasValue ? new Decimal?(curyAmount.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
                  PMRevenueBudget pmRevenueBudget6 = pmRevenueBudget2;
                  Decimal? curyRevisedAmount = pmRevenueBudget6.CuryRevisedAmount;
                  Decimal valueOrDefault4 = pmRevenueBudget1.CuryAmount.GetValueOrDefault();
                  pmRevenueBudget6.CuryRevisedAmount = curyRevisedAmount.HasValue ? new Decimal?(curyRevisedAmount.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
                }
              }
              foreach (PMRevenueBudget pmRevenueBudget in dictionary7.Values)
              {
                pmRevenueBudget.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
                pmRevenueBudget.ProjectTaskID = pmTask.TaskID;
                pmRevenueBudget.RevisedQty = pmRevenueBudget.Qty;
                pmRevenueBudget.CuryRevisedAmount = pmRevenueBudget.CuryAmount;
                pmRevenueBudget.NoteID = new Guid?();
                ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Insert(pmRevenueBudget);
              }
            }
          }
        }
        Dictionary<string, List<PMCostBudget>> dictionary8 = new Dictionary<string, List<PMCostBudget>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
        foreach (PXResult<PMCostBudget> pxResult in PXSelectBase<PMCostBudget, PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Required<PMCostBudget.projectID>>, And<PMCostBudget.type, Equal<AccountType.expense>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) pmProject.ContractID
        }))
        {
          PMCostBudget pmCostBudget = PXResult<PMCostBudget>.op_Implicit(pxResult);
          PMTask pmTask;
          if (dictionary2.TryGetValue(pmCostBudget.ProjectTaskID.Value, out pmTask))
          {
            List<PMCostBudget> pmCostBudgetList;
            if (!dictionary8.TryGetValue(pmTask.TaskCD, out pmCostBudgetList))
            {
              pmCostBudgetList = new List<PMCostBudget>();
              dictionary8.Add(pmTask.TaskCD, pmCostBudgetList);
            }
            pmCostBudgetList.Add(pmCostBudget);
          }
        }
        foreach (KeyValuePair<string, List<PMCostBudget>> keyValuePair in dictionary8)
        {
          PMTask pmTask1;
          if (dictionary3.TryGetValue(keyValuePair.Key, out pmTask1))
          {
            foreach (PMCostBudget pmCostBudget1 in keyValuePair.Value)
            {
              pmCostBudget1.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
              pmCostBudget1.ProjectTaskID = pmTask1.TaskID;
              pmCostBudget1.RevisedQty = pmCostBudget1.Qty;
              pmCostBudget1.CuryRevisedAmount = pmCostBudget1.CuryAmount;
              pmCostBudget1.NoteID = new Guid?();
              int? nullable1 = new int?();
              int? nullable2 = pmCostBudget1.RevenueTaskID;
              if (nullable2.HasValue)
              {
                Dictionary<int, PMTask> dictionary9 = dictionary2;
                nullable2 = pmCostBudget1.RevenueTaskID;
                int key = nullable2.Value;
                PMTask pmTask2;
                ref PMTask local = ref pmTask2;
                PMTask pmTask3;
                if (dictionary9.TryGetValue(key, out local) && dictionary3.TryGetValue(pmTask2.TaskCD, out pmTask3))
                {
                  pmCostBudget1.RevenueTaskID = pmTask3.TaskID;
                  nullable1 = pmCostBudget1.RevenueInventoryID;
                  PMCostBudget pmCostBudget2 = pmCostBudget1;
                  nullable2 = new int?();
                  int? nullable3 = nullable2;
                  pmCostBudget2.RevenueInventoryID = nullable3;
                  goto label_102;
                }
              }
              PMCostBudget pmCostBudget3 = pmCostBudget1;
              nullable2 = new int?();
              int? nullable4 = nullable2;
              pmCostBudget3.RevenueTaskID = nullable4;
              PMCostBudget pmCostBudget4 = pmCostBudget1;
              nullable2 = new int?();
              int? nullable5 = nullable2;
              pmCostBudget4.RevenueInventoryID = nullable5;
label_102:
              PMCostBudget pmCostBudget5 = ((PXSelectBase<PMCostBudget>) this.CostBudget).Insert(pmCostBudget1);
              if (nullable1.HasValue)
                ((PXSelectBase) this.CostBudget).Cache.SetValue<PMCostBudget.revenueInventoryID>((object) pmCostBudget5, (object) nullable1);
            }
          }
        }
      }
      if (settings.CopyRecurring)
      {
        Dictionary<string, List<PMRecurringItem>> dictionary10 = new Dictionary<string, List<PMRecurringItem>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
        foreach (PXResult<PMRecurringItem> pxResult in PXSelectBase<PMRecurringItem, PXSelect<PMRecurringItem, Where<PMRecurringItem.projectID, Equal<Required<PMRecurringItem.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) pmProject.ContractID
        }))
        {
          PMRecurringItem pmRecurringItem = PXResult<PMRecurringItem>.op_Implicit(pxResult);
          PMTask pmTask;
          if (dictionary2.TryGetValue(pmRecurringItem.TaskID.Value, out pmTask))
          {
            List<PMRecurringItem> pmRecurringItemList;
            if (!dictionary10.TryGetValue(pmTask.TaskCD, out pmRecurringItemList))
            {
              pmRecurringItemList = new List<PMRecurringItem>();
              dictionary10.Add(pmTask.TaskCD, pmRecurringItemList);
            }
            pmRecurringItemList.Add(pmRecurringItem);
          }
        }
        foreach (KeyValuePair<string, List<PMRecurringItem>> keyValuePair in dictionary10)
        {
          PMTask pmTask;
          if (dictionary3.TryGetValue(keyValuePair.Key, out pmTask))
          {
            foreach (PMRecurringItem pmRecurringItem in keyValuePair.Value)
              ((PXSelectBase<PMRecurringItem>) this.BillingItems).Insert(new PMRecurringItem()
              {
                ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID,
                TaskID = pmTask.TaskID,
                InventoryID = pmRecurringItem.InventoryID,
                UOM = pmRecurringItem.UOM,
                Description = pmRecurringItem.Description,
                Amount = pmRecurringItem.Amount,
                AccountSource = pmRecurringItem.AccountSource,
                SubMask = pmRecurringItem.SubMask,
                AccountID = pmRecurringItem.AccountID,
                SubID = pmRecurringItem.SubID,
                BranchID = pmRecurringItem.BranchID,
                ResetUsage = pmRecurringItem.ResetUsage,
                Included = pmRecurringItem.Included
              });
          }
        }
      }
      this.OnDefaultFromTemplateTasksInserted(prj, pmProject, taskMap);
    }
    if (settings.CopyCurrency)
    {
      ((PXSelectBase) this.Project).Cache.SetValueExt<PMProject.curyID>((object) prj, (object) pmProject.CuryID);
      ((PXSelectBase) this.Project).Cache.SetValueExt<PMProject.curyIDCopy>((object) prj, (object) pmProject.CuryIDCopy);
      ((PXSelectBase) this.Project).Cache.SetDefaultExt<PMProject.billingCuryID>((object) prj);
    }
    if (settings.CopyEmployees)
    {
      foreach (PXResult<EPEmployeeContract> pxResult in PXSelectBase<EPEmployeeContract, PXSelect<EPEmployeeContract, Where<EPEmployeeContract.contractID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
      {
        (object) pmProject
      }, Array.Empty<object>()))
      {
        EPEmployeeContract employeeContract = PXResult<EPEmployeeContract>.op_Implicit(pxResult);
        ((PXSelectBase<EPEmployeeContract>) this.EmployeeContract).Insert(new EPEmployeeContract()).EmployeeID = employeeContract.EmployeeID;
      }
      ((PXSelectBase) this.EmployeeContract).Cache.Normalize();
      foreach (PXResult<EPContractRate> pxResult in PXSelectBase<EPContractRate, PXSelect<EPContractRate, Where<EPContractRate.contractID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
      {
        (object) pmProject
      }, Array.Empty<object>()))
      {
        EPContractRate epContractRate1 = PXResult<EPContractRate>.op_Implicit(pxResult);
        EPContractRate epContractRate2 = ((PXSelectBase<EPContractRate>) this.ContractRates).Insert(new EPContractRate());
        epContractRate2.IsActive = epContractRate1.IsActive;
        epContractRate2.EmployeeID = epContractRate1.EmployeeID;
        epContractRate2.LabourItemID = epContractRate1.LabourItemID;
        epContractRate2.EarningType = epContractRate1.EarningType;
        epContractRate2.ContractID = prj.ContractID;
      }
    }
    if (settings.CopyContacts)
    {
      foreach (PXResult<PMProjectContact> pxResult in PXSelectBase<PMProjectContact, PXSelect<PMProjectContact, Where<PMProjectContact.projectID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
      {
        (object) pmProject
      }, Array.Empty<object>()))
      {
        PMProjectContact pmProjectContact = PXResult<PMProjectContact>.op_Implicit(pxResult);
        int? contactId = pmProjectContact.ContactID;
        pmProjectContact.ContactID = new int?();
        PMProjectContact copy = (PMProjectContact) ((PXSelectBase) this.ProjectContacts).Cache.CreateCopy((object) pmProjectContact);
        copy.ProjectID = new int?();
        copy.NoteID = new Guid?();
        copy.IsActive = new bool?();
        ((PXSelectBase<PMProjectContact>) this.ProjectContacts).Insert(copy).ContactID = contactId;
      }
    }
    if (settings.CopyEquipment)
    {
      foreach (PXResult<EPEquipmentRate> pxResult in PXSelectBase<EPEquipmentRate, PXSelect<EPEquipmentRate, Where<EPEquipmentRate.projectID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
      {
        (object) pmProject
      }, Array.Empty<object>()))
      {
        EPEquipmentRate epEquipmentRate1 = PXResult<EPEquipmentRate>.op_Implicit(pxResult);
        EPEquipmentRate epEquipmentRate2 = ((PXSelectBase<EPEquipmentRate>) this.EquipmentRates).Insert(new EPEquipmentRate());
        epEquipmentRate2.IsActive = epEquipmentRate1.IsActive;
        epEquipmentRate2.EquipmentID = epEquipmentRate1.EquipmentID;
        epEquipmentRate2.RunRate = epEquipmentRate1.RunRate;
        epEquipmentRate2.SuspendRate = epEquipmentRate1.SuspendRate;
        epEquipmentRate2.SetupRate = epEquipmentRate1.SetupRate;
      }
    }
    if (settings.CopyAccountMapping)
    {
      foreach (PXResult<PMAccountTask> pxResult in PXSelectBase<PMAccountTask, PXSelect<PMAccountTask, Where<PMAccountTask.projectID, Equal<Current<PMProject.contractID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
      {
        (object) pmProject
      }, Array.Empty<object>()))
      {
        PMAccountTask pmAccountTask1 = PXResult<PMAccountTask>.op_Implicit(pxResult);
        if (taskMap.ContainsKey(pmAccountTask1.TaskID.GetValueOrDefault()))
        {
          PMAccountTask pmAccountTask2 = (PMAccountTask) ((PXSelectBase) this.Accounts).Cache.Insert();
          pmAccountTask2.AccountID = pmAccountTask1.AccountID;
          pmAccountTask2.TaskID = new int?(taskMap[pmAccountTask1.TaskID.GetValueOrDefault()]);
        }
      }
    }
    if (settings.CopyNotification)
      this.DefaultFromTemplateNotificationSettings(pmProject);
    foreach (PXResult<PMRetainageStep> pxResult in ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Select(Array.Empty<object>()))
      ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Delete(PXResult<PMRetainageStep>.op_Implicit(pxResult));
    PXView view = ((PXSelectBase) this.RetainageSteps).View;
    object[] objArray1 = new object[1]{ (object) pmProject };
    object[] objArray2 = Array.Empty<object>();
    foreach (PMRetainageStep pmRetainageStep in view.SelectMultiBound(objArray1, objArray2))
    {
      pmRetainageStep.ProjectID = prj.ContractID;
      pmRetainageStep.NoteID = new Guid?();
      ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Insert(pmRetainageStep);
    }
    PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.Project).Cache, (object) pmProject, ((PXSelectBase) this.Project).Cache, (object) prj, settings.CopyNotes, settings.CopyFiles);
    this.OnCopyPasteCompleted(prj, pmProject);
  }

  public virtual void DefaultFromTemplateProjectSettings(PMProject prj, PMProject templ)
  {
    prj.Description = templ.Description;
    PXDBLocalizableStringAttribute.CopyTranslations<PMProject.description, PMProject.description>(((PXGraph) this).Caches[typeof (PMProject)], (object) templ, ((PXGraph) this).Caches[typeof (PMProject)], (object) prj);
    prj.BudgetLevel = templ.BudgetLevel;
    prj.CostBudgetLevel = templ.CostBudgetLevel;
    prj.AccountingMode = templ.AccountingMode;
    prj.TermsID = templ.TermsID;
    prj.AutoAllocate = templ.AutoAllocate;
    prj.LimitsEnabled = templ.LimitsEnabled;
    prj.PrepaymentEnabled = templ.PrepaymentEnabled;
    prj.PrepaymentDefCode = templ.PrepaymentDefCode;
    prj.DefaultBranchID = templ.DefaultBranchID;
    prj.DefaultSalesAccountID = templ.DefaultSalesAccountID;
    prj.DefaultSalesSubID = templ.DefaultSalesSubID;
    prj.DefaultExpenseAccountID = templ.DefaultExpenseAccountID;
    prj.DefaultExpenseSubID = templ.DefaultExpenseSubID;
    prj.DefaultAccrualAccountID = templ.DefaultAccrualAccountID;
    prj.DefaultAccrualSubID = templ.DefaultAccrualSubID;
    prj.DefaultOverbillingAccountID = templ.DefaultOverbillingAccountID;
    prj.DefaultOverbillingSubID = templ.DefaultOverbillingSubID;
    prj.DefaultUnderbillingAccountID = templ.DefaultUnderbillingAccountID;
    prj.DefaultUnderbillingSubID = templ.DefaultUnderbillingSubID;
    prj.CalendarID = templ.CalendarID;
    prj.RestrictToEmployeeList = templ.RestrictToEmployeeList;
    prj.RestrictToResourceList = templ.RestrictToResourceList;
    prj.AllowOverrideCury = templ.AllowOverrideCury;
    prj.AllowOverrideRate = templ.AllowOverrideRate;
    prj.RateTableID = templ.RateTableID;
    prj.AllocationID = templ.AllocationID;
    prj.BillingID = templ.BillingID;
    ((PXSelectBase<PMProject>) this.Project).SetValueExt<PMProject.ownerID>(prj, (object) templ.OwnerID);
    prj.ApproverID = templ.ApproverID;
    prj.AssistantID = templ.AssistantID;
    prj.ProjectGroupID = templ.ProjectGroupID;
    prj.GroupMask = templ.GroupMask;
    prj.AutomaticReleaseAR = templ.AutomaticReleaseAR;
    prj.CreateProforma = templ.CreateProforma;
    prj.ChangeOrderWorkflow = templ.ChangeOrderWorkflow;
    prj.AllowIssueFromFreeStock = templ.AllowIssueFromFreeStock;
    prj.RetainagePct = templ.RetainagePct;
    prj.BudgetMetricsEnabled = templ.BudgetMetricsEnabled;
    prj.IncludeCO = templ.IncludeCO;
    prj.RetainageMaxPct = templ.RetainageMaxPct;
    prj.RetainageMode = templ.RetainageMode;
    prj.SteppedRetainage = templ.SteppedRetainage;
    prj.AIALevel = templ.AIALevel;
    prj.LastProformaNumber = templ.LastProformaNumber;
    prj.IncludeQtyInAIA = templ.IncludeQtyInAIA;
    prj.DropshipExpenseAccountSource = templ.DropshipExpenseAccountSource;
    prj.DropshipExpenseSubMask = templ.DropshipExpenseSubMask;
    prj.DropshipReceiptProcessing = templ.DropshipReceiptProcessing;
    prj.DropshipExpenseRecording = templ.DropshipExpenseRecording;
    prj.VisibleInAP = templ.VisibleInAP;
    prj.VisibleInGL = templ.VisibleInGL;
    prj.VisibleInAR = templ.VisibleInAR;
    prj.VisibleInSO = templ.VisibleInSO;
    prj.VisibleInPO = templ.VisibleInPO;
    prj.VisibleInTA = templ.VisibleInTA;
    prj.VisibleInEA = templ.VisibleInEA;
    prj.VisibleInIN = templ.VisibleInIN;
    prj.VisibleInCA = templ.VisibleInCA;
    prj.VisibleInCR = templ.VisibleInCR;
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(PXSelectBase<ContractBillingSchedule, PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Current<PMProject.contractID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) templ
    }, Array.Empty<object>()));
    if (contractBillingSchedule == null)
      return;
    if (((PXSelectBase<ContractBillingSchedule>) this.Billing).Current == null)
      ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.Billing).Select(Array.Empty<object>()));
    if (((PXSelectBase<ContractBillingSchedule>) this.Billing).Current == null)
      return;
    ((PXSelectBase<ContractBillingSchedule>) this.Billing).SetValueExt<ContractBillingSchedule.type>(((PXSelectBase<ContractBillingSchedule>) this.Billing).Current, (object) contractBillingSchedule.Type);
    ((PXSelectBase<ContractBillingSchedule>) this.Billing).Update(((PXSelectBase<ContractBillingSchedule>) this.Billing).Current);
  }

  public virtual void DefaultFromTemplateNotificationSettings(PMProject templ)
  {
    foreach (PXResult<NotificationSource> pxResult1 in ((PXSelectBase<NotificationSource>) this.NotificationSources).Select(Array.Empty<object>()))
    {
      NotificationSource notificationSource = PXResult<NotificationSource>.op_Implicit(pxResult1);
      foreach (PXResult<NotificationRecipient> pxResult2 in ((PXSelectBase<NotificationRecipient>) this.NotificationRecipients).Select(new object[1]
      {
        (object) notificationSource.SourceID
      }))
        ((PXSelectBase<NotificationRecipient>) this.NotificationRecipients).Delete(PXResult<NotificationRecipient>.op_Implicit(pxResult2));
      ((PXSelectBase<NotificationSource>) this.NotificationSources).Delete(notificationSource);
    }
    PXSelect<NotificationSource, Where<NotificationSource.refNoteID, Equal<Required<NotificationSource.refNoteID>>>> pxSelect1 = new PXSelect<NotificationSource, Where<NotificationSource.refNoteID, Equal<Required<NotificationSource.refNoteID>>>>((PXGraph) this);
    PXSelect<NotificationRecipient, Where<NotificationRecipient.sourceID, Equal<Required<NotificationRecipient.sourceID>>>> pxSelect2 = new PXSelect<NotificationRecipient, Where<NotificationRecipient.sourceID, Equal<Required<NotificationRecipient.sourceID>>>>((PXGraph) this);
    object[] objArray = new object[1]
    {
      (object) templ.NoteID
    };
    foreach (PXResult<NotificationSource> pxResult3 in ((PXSelectBase<NotificationSource>) pxSelect1).Select(objArray))
    {
      NotificationSource notificationSource1 = PXResult<NotificationSource>.op_Implicit(pxResult3);
      int? sourceId = notificationSource1.SourceID;
      notificationSource1.SourceID = new int?();
      notificationSource1.RefNoteID = new Guid?();
      NotificationSource notificationSource2 = ((PXSelectBase<NotificationSource>) this.NotificationSources).Insert(notificationSource1);
      foreach (PXResult<NotificationRecipient> pxResult4 in ((PXSelectBase<NotificationRecipient>) this.NotificationRecipients).Select(new object[1]
      {
        (object) notificationSource2.SourceID
      }))
        ((PXSelectBase<NotificationRecipient>) this.NotificationRecipients).Delete(PXResult<NotificationRecipient>.op_Implicit(pxResult4));
      foreach (PXResult<NotificationRecipient> pxResult5 in ((PXSelectBase<NotificationRecipient>) pxSelect2).Select(new object[1]
      {
        (object) sourceId
      }))
      {
        NotificationRecipient notificationRecipient = PXResult<NotificationRecipient>.op_Implicit(pxResult5);
        notificationRecipient.NotificationID = new int?();
        notificationRecipient.SourceID = notificationSource2.SourceID;
        notificationRecipient.RefNoteID = new Guid?();
        ((PXSelectBase<NotificationRecipient>) this.NotificationRecipients).Insert(notificationRecipient);
      }
    }
  }

  protected void _(PX.Data.Events.RowPersisting<PMProject> e)
  {
    string contractCd = e.Row?.ContractCD;
    if (string.IsNullOrEmpty(contractCd))
      return;
    this.ValidateProjectCD(contractCd, e.Operation);
    object obj1 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.GetValue<PMProject.ownerID>((object) e.Row);
    object valueOriginal1 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.GetValueOriginal<PMProject.ownerID>((object) e.Row);
    if (obj1 != null && (e.Operation == 2 || !obj1.Equals(valueOriginal1)))
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.VerifyFieldAndRaiseException<PMProject.ownerID>((object) e.Row, true);
    object obj2 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.GetValue<PMProject.assistantID>((object) e.Row);
    object valueOriginal2 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.GetValueOriginal<PMProject.assistantID>((object) e.Row);
    if (obj2 != null && (e.Operation == 2 || !obj2.Equals(valueOriginal2)))
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.VerifyFieldAndRaiseException<PMProject.assistantID>((object) e.Row, true);
    if (e.Operation != 2)
      return;
    this.negativeProjectKey = e.Row.ContractID;
  }

  protected void _(PX.Data.Events.RowPersisted<PMProject> e)
  {
    if (e.Operation == 2 && e.TranStatus == null && this.negativeProjectKey.HasValue)
    {
      int? contractId = e.Row.ContractID;
      foreach (PMProjectBudgetHistoryAccum budgetHistoryAccum in ((PXSelectBase) this.ProjectBudgetHistoryAccum).Cache.Inserted)
        ((PXSelectBase) this.ProjectBudgetHistoryAccum).Cache.SetValue<PMProjectBudgetHistoryAccum.projectID>((object) budgetHistoryAccum, (object) contractId);
    }
    if (e.Operation != 2 || e.TranStatus != 2)
      return;
    foreach (PMProjectBudgetHistoryAccum budgetHistoryAccum in ((PXSelectBase) this.ProjectBudgetHistoryAccum).Cache.Inserted)
      ((PXSelectBase) this.ProjectBudgetHistoryAccum).Cache.SetValue<PMProjectBudgetHistoryAccum.projectID>((object) budgetHistoryAccum, (object) this.negativeProjectKey);
  }

  protected void _(
    PX.Data.Events.FieldVerifying<PMProject, PMProject.assistantID> e)
  {
    if (this.IsCopyPaste)
      return;
    PX.Objects.EP.EPEmployee epEmployee = PX.Objects.EP.EPEmployee.PK.Find((PXGraph) this, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.assistantID>, PMProject, object>) e).NewValue);
    if (epEmployee != null && epEmployee.VStatus != "A")
    {
      PXSetPropertyException<PMProject.assistantID> propertyException = new PXSetPropertyException<PMProject.assistantID>("The employee is not active.");
      ((PXSetPropertyException) propertyException).ErrorValue = (object) epEmployee.AcctCD;
      throw propertyException;
    }
  }

  public void _(
    PX.Data.Events.FieldSelecting<PMProject, PMProject.ownerID> e)
  {
    PMProject row = e.Row;
    if ((row != null ? (!row.OwnerID.HasValue ? 1 : 0) : 1) != 0 || !string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMProject.ownerID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProject, PMProject.ownerID>>) e).Cache, (object) e.Row)))
      return;
    PX.Objects.EP.EPEmployee epEmployee = ((PXSelectBase<PX.Objects.EP.EPEmployee>) new FbqlSelect<SelectFromBase<PX.Objects.EP.EPEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.EP.EPEmployee.defContactID, IBqlInt>.IsEqual<P.AsInt>>, PX.Objects.EP.EPEmployee>.View((PXGraph) this)).SelectSingle(new object[1]
    {
      (object) e.Row.OwnerID
    });
    if (epEmployee == null || !(epEmployee.VStatus != "A"))
      return;
    PXUIFieldAttribute.SetWarning<PMProject.ownerID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProject, PMProject.ownerID>>) e).Cache, (object) e.Row, "The employee is not active.");
  }

  public void _(
    PX.Data.Events.FieldSelecting<PMProject, PMProject.assistantID> e)
  {
    PMProject row = e.Row;
    if ((row != null ? (!row.AssistantID.HasValue ? 1 : 0) : 1) != 0 || !string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMProject.assistantID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProject, PMProject.assistantID>>) e).Cache, (object) e.Row)))
      return;
    PX.Objects.EP.EPEmployee epEmployee = ((PXSelectBase<PX.Objects.EP.EPEmployee>) new FbqlSelect<SelectFromBase<PX.Objects.EP.EPEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.EP.EPEmployee.bAccountID, IBqlInt>.IsEqual<P.AsInt>>, PX.Objects.EP.EPEmployee>.View((PXGraph) this)).SelectSingle(new object[1]
    {
      (object) e.Row.AssistantID
    });
    if (epEmployee == null || !(epEmployee.VStatus != "A"))
      return;
    PXUIFieldAttribute.SetWarning<PMProject.assistantID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProject, PMProject.assistantID>>) e).Cache, (object) e.Row, "The employee is not active.");
  }

  public void _(
    PX.Data.Events.FieldSelecting<PMProjectContact, PMProjectContact.contactID> e)
  {
    PMProjectContact row = e.Row;
    if ((row != null ? (!row.ContactID.HasValue ? 1 : 0) : 1) != 0 || !string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMProjectContact.contactID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProjectContact, PMProjectContact.contactID>>) e).Cache, (object) e.Row)))
      return;
    PX.Objects.CR.Contact contact = PX.Objects.CR.Contact.PK.Find((PXGraph) this, e.Row.ContactID);
    if (contact == null || contact.IsActive.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetWarning<PMProjectContact.contactID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProjectContact, PMProjectContact.contactID>>) e).Cache, (object) e.Row, contact.ContactType == "EP" ? "The employee is not active." : "The contact is not active.");
  }

  public virtual bool Validate()
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null || ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current == null || !((PXSelectBase<PMProject>) this.Project).Current.CustomerID.HasValue || !string.IsNullOrEmpty(((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.Type))
      return true;
    ((PXSelectBase) this.Billing).Cache.RaiseExceptionHandling<ContractBillingSchedule.type>((object) ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) "[type]"
    }));
    return false;
  }

  public virtual void Persist()
  {
    if (!this.Validate())
      throw new PXException("One or more rows failed to validate. Please correct and try again.");
    if (((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.BudgetMetricsEnabled.GetValueOrDefault())
    {
      foreach (PMCostBudget budget in ((PXSelectBase) this.CostBudget).Cache.Inserted)
        this.RecordProductionRecord(budget);
      foreach (PMCostBudget budget in ((PXSelectBase) this.CostBudget).Cache.Updated)
      {
        if (this.UpdateLastXXXFields(budget))
          this.RecordProductionRecord(budget);
      }
    }
    this.costBudgetsByRevenueTaskID = new Dictionary<int, List<PMCostBudget>>();
    foreach (PMCostBudget pmCostBudget in ((PXSelectBase) this.CostBudget).Cache.Inserted)
    {
      int? revenueTaskId = pmCostBudget.RevenueTaskID;
      int num = 0;
      if (revenueTaskId.GetValueOrDefault() < num & revenueTaskId.HasValue)
      {
        Dictionary<int, List<PMCostBudget>> budgetsByRevenueTaskId1 = this.costBudgetsByRevenueTaskID;
        revenueTaskId = pmCostBudget.RevenueTaskID;
        int key1 = revenueTaskId.Value;
        List<PMCostBudget> pmCostBudgetList1;
        ref List<PMCostBudget> local = ref pmCostBudgetList1;
        if (!budgetsByRevenueTaskId1.TryGetValue(key1, out local))
        {
          pmCostBudgetList1 = new List<PMCostBudget>();
          Dictionary<int, List<PMCostBudget>> budgetsByRevenueTaskId2 = this.costBudgetsByRevenueTaskID;
          revenueTaskId = pmCostBudget.RevenueTaskID;
          int key2 = revenueTaskId.Value;
          List<PMCostBudget> pmCostBudgetList2 = pmCostBudgetList1;
          budgetsByRevenueTaskId2.Add(key2, pmCostBudgetList2);
        }
        pmCostBudgetList1.Add(pmCostBudget);
      }
    }
    foreach (PMCostBudget pmCostBudget in ((PXSelectBase) this.CostBudget).Cache.Updated)
    {
      int? revenueTaskId = pmCostBudget.RevenueTaskID;
      int num = 0;
      if (revenueTaskId.GetValueOrDefault() < num & revenueTaskId.HasValue)
      {
        Dictionary<int, List<PMCostBudget>> budgetsByRevenueTaskId3 = this.costBudgetsByRevenueTaskID;
        revenueTaskId = pmCostBudget.RevenueTaskID;
        int key3 = revenueTaskId.Value;
        List<PMCostBudget> pmCostBudgetList3;
        ref List<PMCostBudget> local = ref pmCostBudgetList3;
        if (!budgetsByRevenueTaskId3.TryGetValue(key3, out local))
        {
          pmCostBudgetList3 = new List<PMCostBudget>();
          Dictionary<int, List<PMCostBudget>> budgetsByRevenueTaskId4 = this.costBudgetsByRevenueTaskID;
          revenueTaskId = pmCostBudget.RevenueTaskID;
          int key4 = revenueTaskId.Value;
          List<PMCostBudget> pmCostBudgetList4 = pmCostBudgetList3;
          budgetsByRevenueTaskId4.Add(key4, pmCostBudgetList4);
        }
        pmCostBudgetList3.Add(pmCostBudget);
      }
    }
    foreach (PMBudgetProduction budgetProduction in ((PXSelectBase) this.BudgetProduction).Cache.Deleted)
      ((PXSelectBase) this.BudgetProduction).Cache.PersistDeleted((object) budgetProduction);
    this.budgetHistoryByTaskID = new Dictionary<int, List<PMProjectBudgetHistoryAccum>>();
    foreach (PMProjectBudgetHistoryAccum budgetHistoryAccum in ((PXSelectBase) this.ProjectBudgetHistoryAccum).Cache.Inserted)
    {
      int? taskId = budgetHistoryAccum.TaskID;
      int num = 0;
      if (taskId.GetValueOrDefault() < num & taskId.HasValue)
      {
        Dictionary<int, List<PMProjectBudgetHistoryAccum>> budgetHistoryByTaskId1 = this.budgetHistoryByTaskID;
        taskId = budgetHistoryAccum.TaskID;
        int key5 = taskId.Value;
        List<PMProjectBudgetHistoryAccum> budgetHistoryAccumList1;
        ref List<PMProjectBudgetHistoryAccum> local = ref budgetHistoryAccumList1;
        if (!budgetHistoryByTaskId1.TryGetValue(key5, out local))
        {
          budgetHistoryAccumList1 = new List<PMProjectBudgetHistoryAccum>();
          Dictionary<int, List<PMProjectBudgetHistoryAccum>> budgetHistoryByTaskId2 = this.budgetHistoryByTaskID;
          taskId = budgetHistoryAccum.TaskID;
          int key6 = taskId.Value;
          List<PMProjectBudgetHistoryAccum> budgetHistoryAccumList2 = budgetHistoryAccumList1;
          budgetHistoryByTaskId2.Add(key6, budgetHistoryAccumList2);
        }
        budgetHistoryAccumList1.Add(budgetHistoryAccum);
      }
    }
    ((PXGraph) this).Persist();
  }

  public virtual void ValidateProjectCD(string projectCD, PXDBOperation operation)
  {
    if (PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.contractCD, Equal<P.AsString>>>>>.And<Not<MatchUser>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectCD
    })) != null)
      throw new PXException("The project cannot be created because the specified project ID ({0}) already exists in the system but your user has no sufficient access rights for it. Specify another Project ID.", new object[1]
      {
        (object) projectCD.Trim()
      });
    if (operation != 2)
      return;
    if (PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMProject.contractCD, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectCD
    })) != null)
      throw new PXException("The project cannot be created because the specified project ID ({0}) already exists in the system. Specify another Project ID.", new object[1]
      {
        (object) projectCD.Trim()
      });
  }

  public virtual void FillBudgetHistory()
  {
    foreach (PXResult<PMBudget> pxResult in PXSelectBase<PMBudget, PXViewOf<PMBudget>.BasedOn<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMBudget.projectID, IBqlInt>.IsEqual<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      this.UpdateBudgetHistoryLine(PXResult<PMBudget>.op_Implicit(pxResult), 1);
  }

  public virtual void ClearBudgetHistory()
  {
    foreach (PXResult<PMProjectBudgetHistory> pxResult in PXSelectBase<PMProjectBudgetHistory, PXViewOf<PMProjectBudgetHistory>.BasedOn<SelectFromBase<PMProjectBudgetHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProjectBudgetHistory.projectID, Equal<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PMProjectBudgetHistory.changeOrderRefNbr, IBqlString>.IsEqual<ProjectBudgetHistoryChangeOrderRef.empty>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      ((PXSelectBase<PMProjectBudgetHistory>) this.ProjectBudgetHistory).Delete(PXResult<PMProjectBudgetHistory>.op_Implicit(pxResult));
  }

  public virtual void BudgetHistoryUpdateDate(DateTime oldDate, DateTime newDate)
  {
    PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
    ((PXSelectBase) this.ProjectBudgetHistoryAccum).Cache.Clear();
    this.ClearBudgetHistory();
    this.FillBudgetHistory();
  }

  public virtual void UpdateBudgetHistoryLine(PMBudget budget, int sign)
  {
    if (budget == null)
      return;
    int? nullable1 = budget.ProjectTaskID;
    if (!nullable1.HasValue)
      return;
    nullable1 = budget.CostCodeID;
    if (!nullable1.HasValue)
      return;
    nullable1 = budget.AccountGroupID;
    if (!nullable1.HasValue)
      return;
    nullable1 = budget.InventoryID;
    if (!nullable1.HasValue)
      return;
    Decimal? qty = budget.Qty;
    Decimal num1 = 0M;
    if (qty.GetValueOrDefault() == num1 & qty.HasValue)
    {
      Decimal? curyAmount = budget.CuryAmount;
      Decimal num2 = 0M;
      if (curyAmount.GetValueOrDefault() == num2 & curyAmount.HasValue)
        return;
    }
    if (!GraphHelper.RowCast<PMTask>((IEnumerable) ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>())).Where<PMTask>((Func<PMTask, bool>) (task =>
    {
      int? taskId = task.TaskID;
      int? projectTaskId = budget.ProjectTaskID;
      return taskId.GetValueOrDefault() == projectTaskId.GetValueOrDefault() & taskId.HasValue == projectTaskId.HasValue;
    })).Any<PMTask>())
      return;
    PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
    PMProjectBudgetHistoryAccum budgetHistoryAccum1 = new PMProjectBudgetHistoryAccum();
    budgetHistoryAccum1.Date = current.StartDate;
    budgetHistoryAccum1.ProjectID = budget.ProjectID;
    budgetHistoryAccum1.TaskID = budget.ProjectTaskID;
    budgetHistoryAccum1.AccountGroupID = budget.AccountGroupID;
    budgetHistoryAccum1.InventoryID = budget.InventoryID;
    budgetHistoryAccum1.CostCodeID = budget.CostCodeID;
    budgetHistoryAccum1.ChangeOrderRefNbr = "X";
    PMProjectBudgetHistoryAccum budgetHistoryAccum2 = ((PXSelectBase<PMProjectBudgetHistoryAccum>) this.ProjectBudgetHistoryAccum).Insert(budgetHistoryAccum1);
    budgetHistoryAccum2.Type = budget.Type;
    budgetHistoryAccum2.UOM = budget.UOM;
    PMProjectBudgetHistoryAccum budgetHistoryAccum3 = budgetHistoryAccum2;
    Decimal? nullable2 = budgetHistoryAccum3.RevisedBudgetQty;
    Decimal? nullable3 = budget.Qty;
    Decimal num3 = (Decimal) sign;
    Decimal? nullable4 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num3) : new Decimal?();
    Decimal? nullable5;
    if (!(nullable2.HasValue & nullable4.HasValue))
    {
      nullable3 = new Decimal?();
      nullable5 = nullable3;
    }
    else
      nullable5 = new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault());
    budgetHistoryAccum3.RevisedBudgetQty = nullable5;
    PMProjectBudgetHistoryAccum budgetHistoryAccum4 = budgetHistoryAccum2;
    Decimal? revisedBudgetAmt = budgetHistoryAccum4.CuryRevisedBudgetAmt;
    nullable3 = budget.CuryAmount;
    Decimal num4 = (Decimal) sign;
    nullable2 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num4) : new Decimal?();
    Decimal? nullable6;
    if (!(revisedBudgetAmt.HasValue & nullable2.HasValue))
    {
      nullable3 = new Decimal?();
      nullable6 = nullable3;
    }
    else
      nullable6 = new Decimal?(revisedBudgetAmt.GetValueOrDefault() + nullable2.GetValueOrDefault());
    budgetHistoryAccum4.CuryRevisedBudgetAmt = nullable6;
  }

  public virtual void RecalculateRevenueBudget(PMRevenueBudget row)
  {
    if (row == null)
      return;
    int? taskID = row.ProjectTaskID;
    if (!taskID.HasValue)
    {
      PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMProject.contractID>>, And<PMTask.taskCD, Equal<Required<PMTask.taskCD>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        ((PXSelectBase) this.RevenueBudget).Cache.GetValuePending<PMRevenueBudget.projectTaskID>((object) row)
      }));
      if (pmTask != null)
        taskID = pmTask.TaskID;
    }
    try
    {
      this._IsRecalculatingRevenueBudgetScope = true;
      if (row.ProgressBillingBase == "A" && this.ContainsProgressiveBillingRule(row.ProjectID, taskID))
      {
        Decimal valueOrDefault1 = row.CuryRevisedAmount.GetValueOrDefault();
        Decimal num1 = this.GetCuryActualAmountWithTaxes(row) + row.CuryInvoicedAmount.GetValueOrDefault() + (row.CuryPrepaymentAmount.GetValueOrDefault() - row.CuryPrepaymentInvoiced.GetValueOrDefault());
        Decimal valueOrDefault2 = row.CompletedPct.GetValueOrDefault();
        Decimal num2 = valueOrDefault1 * valueOrDefault2 / 100M - num1;
        ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).SetValueExt<PMRevenueBudget.curyAmountToInvoice>(row, (object) num2);
      }
      else
      {
        if (!(row.ProgressBillingBase == "Q"))
          return;
        Decimal num3;
        if (this._BlockQtyToInvoiceCalculate)
        {
          num3 = row.QtyToInvoice.GetValueOrDefault();
        }
        else
        {
          Decimal num4 = row.CompletedPct.GetValueOrDefault() / 100.0M;
          Decimal? nullable = row.RevisedQty;
          Decimal valueOrDefault = nullable.GetValueOrDefault();
          Decimal num5 = num4 * valueOrDefault;
          nullable = row.InvoicedQty;
          Decimal num6 = nullable.GetValueOrDefault() + row.ActualQty.GetValueOrDefault();
          num3 = Decimal.Round(num5 - num6, CommonSetupDecPl.Qty);
          ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).SetValueExt<PMRevenueBudget.qtyToInvoice>(row, (object) num3);
        }
        ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).SetValueExt<PMRevenueBudget.curyAmountToInvoice>(row, (object) (num3 * row.CuryUnitRate.GetValueOrDefault()));
      }
    }
    finally
    {
      this._IsRecalculatingRevenueBudgetScope = false;
    }
  }

  public virtual bool ContainsProgressiveBillingRule(int? projectID, int? taskID)
  {
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, projectID, taskID);
    if (dirty == null || string.IsNullOrEmpty(dirty.BillingID))
      return false;
    return PXResultset<PMBillingRule>.op_Implicit(PXSelectBase<PMBillingRule, PXSelect<PMBillingRule, Where<PMBillingRule.billingID, Equal<Required<PMBillingRule.billingID>>, And<PMBillingRule.type, Equal<PMBillingType.budget>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) dirty.BillingID
    })) != null;
  }

  private PMProject GetProjectByID(int? id)
  {
    return !id.HasValue ? (PMProject) null : PMProject.PK.Find((PXGraph) this, id);
  }

  private bool CanBeBilled()
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return false;
    bool? nullable = ((PXSelectBase<PMProject>) this.Project).Current.IsActive;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXException("Inactive Project cannot be billed.");
    nullable = ((PXSelectBase<PMProject>) this.Project).Current.IsCancelled;
    if (nullable.GetValueOrDefault())
      throw new PXException("The project is canceled and cannot be billed.");
    if (((PXSelectBase) this.Project).Cache.GetStatus((object) ((PXSelectBase<PMProject>) this.Project).Current) == 2)
      return false;
    if (!((PXSelectBase<PMProject>) this.Project).Current.CustomerID.HasValue)
      throw new PXException("This Project has no Customer associated with it and thus cannot be billed.");
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.Billing).Select(Array.Empty<object>()));
    if (contractBillingSchedule == null)
      return false;
    if (!contractBillingSchedule.NextDate.HasValue && contractBillingSchedule.Type != "D")
      throw new PXException("Project can not be Billed if Next Billing Date is empty.");
    return true;
  }

  public virtual bool UpdateLastXXXFields(PMCostBudget budget)
  {
    Decimal? valueOriginal1 = (Decimal?) ((PXSelectBase) this.CostBudget).Cache.GetValueOriginal<PMBudget.curyCostToComplete>((object) budget);
    Decimal? valueOriginal2 = (Decimal?) ((PXSelectBase) this.CostBudget).Cache.GetValueOriginal<PMBudget.costToComplete>((object) budget);
    Decimal? valueOriginal3 = (Decimal?) ((PXSelectBase) this.CostBudget).Cache.GetValueOriginal<PMBudget.percentCompleted>((object) budget);
    Decimal? valueOriginal4 = (Decimal?) ((PXSelectBase) this.CostBudget).Cache.GetValueOriginal<PMBudget.curyCostAtCompletion>((object) budget);
    Decimal? valueOriginal5 = (Decimal?) ((PXSelectBase) this.CostBudget).Cache.GetValueOriginal<PMBudget.costAtCompletion>((object) budget);
    bool flag = false;
    Decimal? curyCostToComplete = budget.CuryCostToComplete;
    Decimal? nullable1 = valueOriginal1;
    if (curyCostToComplete.GetValueOrDefault() == nullable1.GetValueOrDefault() & curyCostToComplete.HasValue == nullable1.HasValue)
    {
      Decimal? percentCompleted = budget.PercentCompleted;
      Decimal? nullable2 = valueOriginal3;
      if (percentCompleted.GetValueOrDefault() == nullable2.GetValueOrDefault() & percentCompleted.HasValue == nullable2.HasValue)
      {
        Decimal? costAtCompletion = budget.CuryCostAtCompletion;
        Decimal? nullable3 = valueOriginal4;
        if (costAtCompletion.GetValueOrDefault() == nullable3.GetValueOrDefault() & costAtCompletion.HasValue == nullable3.HasValue)
          goto label_4;
      }
    }
    budget.CuryLastCostToComplete = valueOriginal1;
    budget.LastCostToComplete = valueOriginal2;
    budget.LastPercentCompleted = valueOriginal3;
    budget.CuryLastCostAtCompletion = valueOriginal4;
    budget.LastCostAtCompletion = valueOriginal5;
    flag = true;
label_4:
    return flag;
  }

  public virtual PMBudgetProduction RecordProductionRecord(PMCostBudget budget)
  {
    ((PXSelectBase) this.CostBudget).Cache.SetValue<PMCostBudget.lineCntr>((object) budget, (object) (budget.LineCntr.GetValueOrDefault() + 1));
    GraphHelper.MarkUpdated(((PXSelectBase) this.CostBudget).Cache, (object) budget, true);
    return ((PXSelectBase<PMBudgetProduction>) this.BudgetProduction).Insert(new PMBudgetProduction()
    {
      ProjectID = budget.ProjectID,
      ProjectTaskID = budget.ProjectTaskID,
      AccountGroupID = budget.AccountGroupID,
      CostCodeID = budget.CostCodeID,
      InventoryID = budget.InventoryID,
      CuryCostToComplete = budget.CuryCostToComplete,
      CostToComplete = budget.CostToComplete,
      PercentCompleted = budget.PercentCompleted,
      CuryCostAtCompletion = budget.CuryCostAtCompletion,
      CostAtCompletion = budget.CostAtCompletion,
      LineNbr = budget.LineCntr
    });
  }

  protected virtual Decimal CalculateCompletedPercentByCost(PMRevenueBudget row)
  {
    PMProductionBudget productionBudget = PXResultset<PMProductionBudget>.op_Implicit(((PXSelectBase<PMProductionBudget>) new PXSelect<PMProductionBudget, Where<PMProductionBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMProductionBudget.revenueTaskID, Equal<Required<PMProductionBudget.revenueTaskID>>, And<PMProductionBudget.revenueInventoryID, Equal<Required<PMProductionBudget.revenueInventoryID>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) row.ProjectTaskID,
      (object) row.InventoryID
    }));
    Decimal completedPercentByCost = 0M;
    if (productionBudget != null)
    {
      Decimal? nullable = productionBudget.CuryRevisedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = productionBudget.CuryActualAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      if (valueOrDefault1 != 0M)
        completedPercentByCost = Decimal.Round(100M * valueOrDefault2 / valueOrDefault1, 2);
    }
    return completedPercentByCost;
  }

  protected virtual Decimal? CalculateCapAmount(PMProject project, PMProjectRevenueTotal totals)
  {
    Decimal? capAmount = new Decimal?();
    if (project != null)
    {
      Decimal num1 = 0M;
      Decimal? nullable;
      if (totals != null)
      {
        Decimal valueOrDefault;
        if (!project.IncludeCO.GetValueOrDefault())
        {
          nullable = totals.CuryAmount;
          valueOrDefault = nullable.GetValueOrDefault();
        }
        else
        {
          nullable = totals.CuryRevisedAmount;
          valueOrDefault = nullable.GetValueOrDefault();
        }
        num1 = valueOrDefault;
      }
      if (num1 > 0M)
      {
        ref Decimal? local = ref capAmount;
        Decimal num2 = num1;
        nullable = project.RetainageMaxPct;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        Decimal num3 = num2 * valueOrDefault1;
        nullable = project.RetainagePct;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        Decimal num4 = Decimal.Round(num3 * valueOrDefault2 * 0.01M * 0.01M, 2);
        local = new Decimal?(num4);
      }
    }
    return capAmount;
  }

  protected virtual Decimal GetCuryActualAmountWithTaxes(PMRevenueBudget row)
  {
    Decimal? nullable = row.CuryActualAmount;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.CuryInclTaxAmount;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    return valueOrDefault1 + valueOrDefault2;
  }

  protected virtual void OnProjectCDChanged()
  {
    foreach (PXResult<INCostCenter, PMTask> pxResult in ((PXSelectBase<INCostCenter>) this.CostCenters).Select(Array.Empty<object>()))
    {
      INCostCenter inCostCenter = PXResult<INCostCenter, PMTask>.op_Implicit(pxResult);
      PMTask task = PXResult<INCostCenter, PMTask>.op_Implicit(pxResult);
      inCostCenter.CostCenterCD = this.BuildCostCenterCD(((PXSelectBase<PMProject>) this.Project).Current, task);
      ((PXSelectBase<INCostCenter>) this.CostCenters).Update(inCostCenter);
    }
  }

  protected virtual string BuildCostCenterCD(PMProject project, PMTask task)
  {
    return $"{project.ContractCD.Trim()}/{task.TaskCD.Trim()}";
  }

  public virtual int ExecuteDelete(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    return ((PXGraph) this).ExecuteDelete(viewName, keys, values, parameters);
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    switch (viewName)
    {
      case "RevenueBudget":
        return this.PrepareRevenueBudgetImportRow(keys, values);
      case "CostBudget":
        return this.PrepareCostBudgetImportRow(keys, values);
      case "OtherBudget":
        return this.PreparOtherBudgetImportRow(keys, values);
      default:
        return true;
    }
  }

  public bool RowImporting(string viewName, object row) => true;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  protected virtual void ImportFromExcelRowInserted(PXCache cache, PMRevenueBudget row)
  {
    if (!((PXGraph) this).IsImportFromExcel || row == null)
      return;
    if (!string.IsNullOrWhiteSpace(this._lastBadAccountGroupCD))
      this.RaiseExceptionFieldCanNotBeFound<PMRevenueBudget.accountGroupID>(cache, (object) row, (object) this._lastBadAccountGroupCD);
    if (!string.IsNullOrWhiteSpace(this._lastBadTaskCD))
      this.RaiseExceptionFieldCanNotBeFound<PMRevenueBudget.projectTaskID>(cache, (object) row, (object) this._lastBadTaskCD);
    if (!string.IsNullOrWhiteSpace(this._lastBadInventoryCD))
      this.RaiseExceptionFieldCanNotBeFound<PMRevenueBudget.inventoryID>(cache, (object) row, (object) this._lastBadInventoryCD);
    if (string.IsNullOrWhiteSpace(this._lastBadCostCodeCD))
      return;
    this.RaiseExceptionFieldCanNotBeFound<PMRevenueBudget.costCodeID>(cache, (object) row, (object) this._lastBadCostCodeCD);
  }

  protected virtual void ImportFromExcelRowInserted(PXCache cache, PMCostBudget row)
  {
    if (!((PXGraph) this).IsImportFromExcel || row == null)
      return;
    if (!string.IsNullOrWhiteSpace(this._lastBadAccountGroupCD))
      this.RaiseExceptionFieldCanNotBeFound<PMCostBudget.accountGroupID>(cache, (object) row, (object) this._lastBadAccountGroupCD);
    if (!string.IsNullOrWhiteSpace(this._lastBadTaskCD))
      this.RaiseExceptionFieldCanNotBeFound<PMCostBudget.projectTaskID>(cache, (object) row, (object) this._lastBadTaskCD);
    if (!string.IsNullOrWhiteSpace(this._lastBadInventoryCD))
      this.RaiseExceptionFieldCanNotBeFound<PMCostBudget.inventoryID>(cache, (object) row, (object) this._lastBadInventoryCD);
    if (string.IsNullOrWhiteSpace(this._lastBadCostCodeCD))
      return;
    this.RaiseExceptionFieldCanNotBeFound<PMCostBudget.costCodeID>(cache, (object) row, (object) this._lastBadCostCodeCD);
  }

  protected virtual void ImportFromExcelRowInserted(PXCache cache, PMOtherBudget row)
  {
    if (!((PXGraph) this).IsImportFromExcel || row == null)
      return;
    if (!string.IsNullOrWhiteSpace(this._lastBadAccountGroupCD))
      this.RaiseExceptionFieldCanNotBeFound<PMOtherBudget.accountGroupID>(cache, (object) row, (object) this._lastBadAccountGroupCD);
    if (!string.IsNullOrWhiteSpace(this._lastBadTaskCD))
      this.RaiseExceptionFieldCanNotBeFound<PMOtherBudget.projectTaskID>(cache, (object) row, (object) this._lastBadTaskCD);
    if (!string.IsNullOrWhiteSpace(this._lastBadInventoryCD))
      this.RaiseExceptionFieldCanNotBeFound<PMOtherBudget.inventoryID>(cache, (object) row, (object) this._lastBadInventoryCD);
    if (string.IsNullOrWhiteSpace(this._lastBadCostCodeCD))
      return;
    this.RaiseExceptionFieldCanNotBeFound<PMOtherBudget.costCodeID>(cache, (object) row, (object) this._lastBadCostCodeCD);
  }

  private void RaiseExceptionFieldCanNotBeFound<Field>(PXCache cache, object row, object value) where Field : IBqlField
  {
    cache.RaiseExceptionHandling<Field>(row, value, (Exception) new PXSetPropertyException<Field>("'{0}' cannot be found in the system.", (PXErrorLevel) 4, new object[1]
    {
      (object) typeof (Field).Name
    }));
  }

  protected virtual bool PrepareRevenueBudgetImportRow(IDictionary keys, IDictionary values)
  {
    return this.CheckAccountGroupBeforeImport(keys, values, "AccountGroupID", (Func<PMAccountGroup, bool>) (accountGroup => accountGroup.Type == "I")) && this.CheckTaskBeforeImport(keys, values, "ProjectTaskID") && this.CheckInventoryBeforeImport(keys, values, "InventoryID") && this.CheckCostCodeBeforeImport(keys, values, "CostCodeID");
  }

  protected virtual bool PrepareCostBudgetImportRow(IDictionary keys, IDictionary values)
  {
    return this.CheckAccountGroupBeforeImport(keys, values, "AccountGroupID", (Func<PMAccountGroup, bool>) (accountGroup => accountGroup.IsExpense.GetValueOrDefault())) && this.CheckTaskBeforeImport(keys, values, "ProjectTaskID") && this.CheckInventoryBeforeImport(keys, values, "InventoryID") && this.CheckCostCodeBeforeImport(keys, values, "CostCodeID");
  }

  protected virtual bool PreparOtherBudgetImportRow(IDictionary keys, IDictionary values)
  {
    return this.CheckAccountGroupBeforeImport(keys, values, "AccountGroupID", (Func<PMAccountGroup, bool>) (accountGroup => !accountGroup.IsExpense.GetValueOrDefault() && accountGroup.Type != "I")) && this.CheckTaskBeforeImport(keys, values, "ProjectTaskID") && this.CheckInventoryBeforeImport(keys, values, "InventoryID") && this.CheckCostCodeBeforeImport(keys, values, "CostCodeID");
  }

  protected virtual bool CheckTaskBeforeImport(IDictionary keys, IDictionary values, string key)
  {
    this._lastBadTaskCD = (string) null;
    if (keys.Contains((object) key))
    {
      string key1 = (string) keys[(object) key];
      if (!string.IsNullOrWhiteSpace(key1))
      {
        if (PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXViewOf<PMTask>.BasedOn<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTask.projectID, Equal<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PMTask.taskCD, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) key1
        })) != null)
          return true;
        this._lastBadTaskCD = key1;
      }
      keys[(object) key] = (object) null;
      values[(object) key] = (object) null;
    }
    return true;
  }

  protected virtual bool CheckInventoryBeforeImport(
    IDictionary keys,
    IDictionary values,
    string key)
  {
    this._lastBadInventoryCD = (string) null;
    if (keys.Contains((object) key))
    {
      int? id;
      string cd;
      this.ObtainInstanceIdOrCd(keys[(object) key], out id, out cd);
      if (id.HasValue)
      {
        if (PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, id) != null)
          return true;
        this._lastBadInventoryCD = id.ToString();
      }
      else if (!string.IsNullOrWhiteSpace(cd))
      {
        if (PX.Objects.IN.InventoryItem.UK.Find((PXGraph) this, cd) != null)
          return true;
        this._lastBadInventoryCD = cd;
      }
      keys[(object) key] = (object) null;
      values[(object) key] = (object) null;
    }
    return true;
  }

  protected virtual bool CheckCostCodeBeforeImport(
    IDictionary keys,
    IDictionary values,
    string key)
  {
    this._lastBadCostCodeCD = (string) null;
    if (keys.Contains((object) key))
    {
      int? id;
      string cd;
      this.ObtainInstanceIdOrCd(keys[(object) key], out id, out cd);
      if (id.HasValue)
      {
        if (PMCostCode.PK.Find((PXGraph) this, id) != null)
          return true;
        this._lastBadCostCodeCD = id.ToString();
      }
      else if (!string.IsNullOrWhiteSpace(cd))
      {
        if (PMCostCode.UK.Find((PXGraph) this, cd) != null)
          return true;
        this._lastBadCostCodeCD = cd;
      }
      keys[(object) key] = (object) null;
      values[(object) key] = (object) null;
    }
    return true;
  }

  protected virtual bool CheckAccountGroupBeforeImport(
    IDictionary keys,
    IDictionary values,
    string key,
    Func<PMAccountGroup, bool> checkDelegate)
  {
    this._lastBadAccountGroupCD = (string) null;
    if (keys.Contains((object) key))
    {
      int? id;
      string cd;
      this.ObtainInstanceIdOrCd(keys[(object) key], out id, out cd);
      if (id.HasValue)
      {
        PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, id);
        if (pmAccountGroup != null)
          return checkDelegate(pmAccountGroup);
        this._lastBadAccountGroupCD = id.ToString();
      }
      else if (!string.IsNullOrWhiteSpace(cd))
      {
        PMAccountGroup pmAccountGroup = PXResultset<PMAccountGroup>.op_Implicit(PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.groupCD, Equal<Required<PMAccountGroup.groupCD>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) cd
        }));
        if (pmAccountGroup != null)
          return checkDelegate(pmAccountGroup);
        this._lastBadAccountGroupCD = cd;
      }
      keys[(object) key] = (object) null;
      values[(object) key] = (object) null;
    }
    return true;
  }

  private void ObtainInstanceIdOrCd(object keyValue, out int? id, out string cd)
  {
    id = new int?();
    cd = (string) null;
    if (keyValue is int num)
      id = new int?(num);
    else
      cd = (string) keyValue;
  }

  public class DBInvoiceAmountAttribute(System.Type keyField, System.Type resultField) : 
    PX.Objects.CM.Extensions.PXDBCurrencyAttribute(keyField, resultField)
  {
    protected override void CuryFieldSelecting(
      PXCache sender,
      PXFieldSelectingEventArgs e,
      CuryField curyField)
    {
      base.CuryFieldSelecting(sender, e, curyField);
      PX.Objects.AR.ARInvoice row = e.Row as PX.Objects.AR.ARInvoice;
      Decimal? returnValue = e.ReturnValue as Decimal?;
      if (row == null || !returnValue.HasValue)
        return;
      PXFieldSelectingEventArgs selectingEventArgs = e;
      Decimal? nullable1 = ARDocType.SignAmount(row.DocType);
      Decimal? nullable2 = returnValue;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?());
      selectingEventArgs.ReturnValue = (object) local;
    }
  }

  public class InvoiceAmountAttribute(System.Type keyField, System.Type resultField) : 
    PX.Objects.CM.Extensions.PXCurrencyAttribute(keyField, resultField)
  {
    protected override void CuryFieldSelecting(
      PXCache sender,
      PXFieldSelectingEventArgs e,
      CuryField curyField)
    {
      base.CuryFieldSelecting(sender, e, curyField);
      PX.Objects.AR.ARInvoice row = e.Row as PX.Objects.AR.ARInvoice;
      Decimal? returnValue = e.ReturnValue as Decimal?;
      if (row == null || !returnValue.HasValue)
        return;
      PXFieldSelectingEventArgs selectingEventArgs = e;
      Decimal? nullable1 = ARDocType.SignAmount(row.DocType);
      Decimal? nullable2 = returnValue;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?());
      selectingEventArgs.ReturnValue = (object) local;
    }
  }

  public class MultiCurrency : MultiCurrencyGraph<ProjectEntry, PMProject>
  {
    protected override string Module => "PM";

    protected override MultiCurrencyGraph<ProjectEntry, PMProject>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<ProjectEntry, PMProject>.CurySourceMapping(typeof (PX.Objects.AR.Customer));
    }

    protected override MultiCurrencyGraph<ProjectEntry, PMProject>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ProjectEntry, PMProject>.DocumentMapping(typeof (PMProject));
    }

    protected override CurySource CurrentSourceSelect()
    {
      CurySource curySource = base.CurrentSourceSelect();
      if (curySource == null)
        return (CurySource) null;
      curySource.AllowOverrideCury = new bool?(PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>());
      curySource.AllowOverrideRate = new bool?(true);
      return curySource;
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[8]
      {
        (PXSelectBase) this.Base.Project,
        (PXSelectBase) this.Base.Tasks,
        (PXSelectBase) this.Base.BalanceRecords,
        (PXSelectBase) this.Base.RevenueBudget,
        (PXSelectBase) this.Base.CostBudget,
        (PXSelectBase) this.Base.ProjectBudgetHistory,
        (PXSelectBase) this.Base.dummyProforma,
        (PXSelectBase) this.Base.dummyInvoice
      };
    }

    protected override PXSelectBase[] GetTrackedExceptChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.TaskTotals
      };
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PMProject, PMProject.curyIDCopy> e)
    {
      if (e.Row == null)
        return;
      if (PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>())
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.curyIDCopy>, PMProject, object>) e).NewValue = (object) (((PXGraph) this.Base).Accessinfo.BaseCuryID ?? ((PXSelectBase<PX.Objects.GL.Company>) this.Base.Company).Current.BaseCuryID);
      else
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.curyIDCopy>, PMProject, object>) e).NewValue = (object) e.Row.BaseCuryID;
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<PMProject, PMProject.curyIDCopy> e)
    {
      if (e.Row == null || !((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMProject, PMProject.curyIDCopy>>) e).ExternalCall)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.curyIDCopy>>) e).Cache.SetDefaultExt<PMProject.billingCuryID>((object) e.Row);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.curyIDCopy>>) e).Cache.SetValueExt<PMProject.curyID>((object) e.Row, (object) e.Row.CuryIDCopy);
      this.Base.ShowWaringOnProjectCurrecyIfExcahngeRateNotFound(e.Row);
    }

    protected void _(
      PX.Data.Events.FieldVerifying<PMProject, PMProject.curyIDCopy> e)
    {
      this.ThrowIfCuryIDCannotBeChangedDueToExistingTransactions(e.Row?.CuryID, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.curyIDCopy>, PMProject, object>) e).NewValue as string);
    }

    private void ThrowIfCuryIDCannotBeChangedDueToExistingTransactions(
      string currentCuryID,
      string newCuryIDValue)
    {
      if (currentCuryID != null && !(currentCuryID == newCuryIDValue) && this.Base.ProjectHasTransactions())
        throw new PXSetPropertyException<PMProject.curyIDCopy>("The project currency cannot be changed because the project already has transactions.");
    }
  }

  public class ProjectEntry_ActivityDetailsExt_Actions : 
    ActivityDetailsExt_Inversed_Actions<ProjectEntry.ProjectEntry_ActivityDetailsExt, ProjectEntry, PMProject>
  {
  }

  public class ProjectEntry_ActivityDetailsExt : ActivityDetailsExt_Inversed<ProjectEntry, PMProject>
  {
    public static readonly System.Type LinkConditionClause = typeof (Where<CRPMTimeActivity.projectID, Equal<Current<PMProject.contractID>>>);
    public static readonly System.Type BAccountIDCommand = typeof (Select<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProject.customerID>>>>);

    public override System.Type GetLinkConditionClause()
    {
      return ProjectEntry.ProjectEntry_ActivityDetailsExt.LinkConditionClause;
    }

    public override System.Type GetBAccountIDCommand()
    {
      return ProjectEntry.ProjectEntry_ActivityDetailsExt.BAccountIDCommand;
    }

    public override string GetCustomMailTo()
    {
      return ProjectEntry.ProjectEntry_ActivityDetailsExt.GetProjectMailTo((PXGraph) this.Base, ((PXSelectBase<PMProject>) this.Base.Project).Current);
    }

    public static string GetProjectMailTo(PXGraph graph, PMProject project)
    {
      if (project == null)
        return (string) null;
      PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select(graph, new object[1]
      {
        (object) project.CustomerID
      }));
      return string.IsNullOrWhiteSpace(contact?.EMail) ? (string) null : PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(contact.EMail, contact.DisplayName);
    }

    public override void CreateTimeActivity(PXGraph targetGraph, int classID, string activityType)
    {
      base.CreateTimeActivity(targetGraph, classID, activityType);
      ProjectEntry.ProjectEntry_ActivityDetailsExt.CreateProjectTimeActivity((PXGraph) this.Base, targetGraph, classID);
    }

    public static void CreateProjectTimeActivity(
      PXGraph thisGraph,
      PXGraph targetGraph,
      int classID)
    {
      PXCache cach = targetGraph.Caches[typeof (PMTimeActivity)];
      if (cach == null)
        return;
      PMTimeActivity current = (PMTimeActivity) cach.Current;
      if (current == null)
        return;
      bool flag = classID != 0 && classID != 1;
      current.TrackTime = new bool?(flag);
      current.ProjectID = (int?) ((PX.Objects.CT.Contract) thisGraph.Caches[typeof (PMProject)].Current)?.ContractID;
      cach.Update((object) current);
    }

    protected virtual void _(PX.Data.Events.RowSelected<PMProject> e)
    {
      if (e.Row == null)
        return;
      bool flag = e.Row.Status != "F";
      if (e.Row.RestrictToEmployeeList.GetValueOrDefault() && !((PXGraph) this.Base).IsExport)
      {
        EPEmployeeContract employeeContract = ((PXSelectBase<EPEmployeeContract>) new PXSelectJoin<EPEmployeeContract, InnerJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.bAccountID, Equal<EPEmployeeContract.employeeID>>>, Where<EPEmployeeContract.contractID, Equal<Current<PMProject.contractID>>, And<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>>((PXGraph) this.Base)).SelectSingle(Array.Empty<object>());
        flag = flag && employeeContract != null;
      }
      ((PXSelectBase) this.Activities).Cache.AllowInsert = flag;
    }
  }

  public sealed class ProjectGroupMaskHelperExt : PX.Objects.PM.ProjectGroupMaskHelper<ProjectEntry>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
  }

  public class CreateContactFromProjectGraphExt : CRCreateContactActionBase<ProjectEntry, PMProject>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

    protected override CRCreateActionBaseInit<ProjectEntry, PMProject>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<ProjectEntry, PMProject>.DocumentContactMapping(typeof (PX.Objects.CR.Contact))
      {
        Email = typeof (PX.Objects.CR.Contact.eMail)
      };
    }

    protected override CRCreateActionBaseInit<ProjectEntry, PMProject>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<ProjectEntry, PMProject>.DocumentAddressMapping(typeof (PX.Objects.CR.Address));
    }

    public override ConversionResult<PX.Objects.CR.Contact> Convert(ContactConversionOptions options = null)
    {
      ConversionResult<PX.Objects.CR.Contact> conversionResult = base.Convert(options);
      if (conversionResult.Converted)
      {
        ((PXSelectBase<PMProjectContact>) this.Base.ProjectContacts).Insert(new PMProjectContact()
        {
          ContactID = conversionResult.Entity.ContactID
        });
        ((PXAction) this.Base.Save).Press();
      }
      return conversionResult;
    }

    protected override PX.Objects.CR.Contact CreateMaster(
      ContactMaint graph,
      ContactConversionOptions options)
    {
      PX.Objects.CR.Extensions.CRCreateActions.Document current1 = ((PXSelectBase<PX.Objects.CR.Extensions.CRCreateActions.Document>) this.Documents).Current;
      ContactFilter current2 = ((PXSelectBase<ContactFilter>) this.ContactInfo).Current;
      PX.Objects.CR.Contact contact1 = new PX.Objects.CR.Contact()
      {
        ContactType = "PN",
        FirstName = current2.FirstName,
        LastName = current2.LastName,
        EMail = current2.Email,
        Salutation = current2.Salutation,
        ClassID = current2.ContactClass,
        Phone1 = current2.Phone1,
        Phone2 = current2.Phone2
      };
      PX.Objects.CR.Contact contact2 = ((PXSelectBase<PX.Objects.CR.Contact>) graph.Contact).Insert(contact1);
      this.FillAttributes(graph.Answers, contact2);
      this.FillUDF(((PXSelectBase) this.ContactInfoUDF).Cache, ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.CR.Extensions.CRCreateActions.Document>(current1), ((PXSelectBase) graph.Contact).Cache, contact2, contact2.ClassID);
      this.FillNotesAndAttachments((PXGraph) graph, ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.CR.Extensions.CRCreateActions.Document>(current1), ((PXSelectBase) graph.Contact).Cache, contact2);
      return ((PXSelectBase<PX.Objects.CR.Contact>) graph.Contact).Update(contact2);
    }

    protected virtual void _(PX.Data.Events.RowSelected<PMProject> e)
    {
      if (e.Row == null)
        return;
      ((PXAction) this.CreateContact).SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache.GetStatus((object) e.Row) != 2);
    }
  }

  [PXCacheName("Tasks for Addition")]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class SelectedTask : PMTask
  {
    [PXDBInt(IsKey = true)]
    public override int? ProjectID
    {
      get => this._ProjectID;
      set => this._ProjectID = value;
    }

    [PXDimension("PROTASK")]
    [PXDBString(30, IsUnicode = true, IsKey = true)]
    [PXUIField(DisplayName = "Task ID")]
    public override string TaskCD
    {
      get => this._TaskCD;
      set => this._TaskCD = value;
    }

    public new abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProjectEntry.SelectedTask.projectID>
    {
    }

    public new abstract class taskCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectEntry.SelectedTask.taskCD>
    {
    }

    public new abstract class taskID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ProjectEntry.SelectedTask.taskID>
    {
    }

    public new abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectEntry.SelectedTask.description>
    {
    }
  }

  /// <summary>Commitment totals by project</summary>
  [PXCacheName("PO Line")]
  public class PMPOLine : PX.Objects.PO.POLine
  {
    /// <summary>
    /// The total quantity of the items in the purchase order lines or subcontract lines that are associated with the current project.
    /// </summary>
    [PXDBQuantity(typeof (PX.Objects.PO.POLine.uOM), typeof (PX.Objects.PO.POLine.baseOrderQty), HandleEmptyKey = true)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public override Decimal? OrderQty
    {
      get => this._OrderQty;
      set => this._OrderQty = value;
    }

    /// <summary>
    /// The total amount of the document lines that are associated with the current project. The amount is shown in the document currency.
    /// </summary>
    [PXDBPriceCostCalced(typeof (Sub<PX.Objects.PO.POLine.curyLineAmt, PX.Objects.PO.POLine.curyDiscAmt>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
    [PXPriceCost]
    [PXUIField(DisplayName = "Project Amount", Enabled = false)]
    public virtual Decimal? CuryLineCost { get; set; }

    public new abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectEntry.PMPOLine.orderType>
    {
    }

    public new abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectEntry.PMPOLine.orderNbr>
    {
    }

    public new abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProjectEntry.PMPOLine.projectID>
    {
    }

    public new abstract class orderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ProjectEntry.PMPOLine.orderQty>
    {
    }

    public abstract class curyLineCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ProjectEntry.PMPOLine.curyLineCost>
    {
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class TemplateSettingsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _TemplateID;

    [PXString]
    [PXUIField(DisplayName = "Template ID", Required = true)]
    [PXDimension("TMPROJECT")]
    public virtual string TemplateID
    {
      get => this._TemplateID;
      set => this._TemplateID = value;
    }

    public abstract class templateID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectEntry.TemplateSettingsFilter.templateID>
    {
    }
  }

  public class DefaultFromTemplateSettings
  {
    public bool CopyProperties { get; set; }

    public bool CopyTasks { get; set; }

    public bool CopyBudget { get; set; }

    public bool CopyAttributes { get; set; }

    public bool CopyEmployees { get; set; }

    public bool CopyEquipment { get; set; }

    public bool CopyNotification { get; set; }

    public bool CopyAccountMapping { get; set; }

    public bool CopyRecurring { get; set; }

    public bool CopyContacts { get; set; }

    public bool CopyCurrency { get; set; }

    public bool? CopyNotes { get; set; }

    public bool? CopyFiles { get; set; }

    public static ProjectEntry.DefaultFromTemplateSettings Default
    {
      get
      {
        return new ProjectEntry.DefaultFromTemplateSettings()
        {
          CopyProperties = true,
          CopyTasks = true,
          CopyBudget = true,
          CopyAttributes = true,
          CopyRecurring = true,
          CopyEmployees = true,
          CopyEquipment = true,
          CopyNotification = true,
          CopyAccountMapping = true,
          CopyContacts = true,
          CopyCurrency = true,
          CopyNotes = new bool?(true),
          CopyFiles = new bool?(true)
        };
      }
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  public class LoadFromTemplateInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    [PXUIField(DisplayName = "Template ID", Required = true)]
    [PXDimension("PROJECT")]
    public virtual int? TemplateID { get; set; }

    public abstract class templateID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectEntry.LoadFromTemplateInfo.templateID>
    {
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class CopyDialogInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString]
    [PXUIField(DisplayName = "Project ID", Required = true)]
    [PXDimension("PROJECT")]
    public virtual string ProjectID { get; set; }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectEntry.CopyDialogInfo.projectID>
    {
    }
  }
}
