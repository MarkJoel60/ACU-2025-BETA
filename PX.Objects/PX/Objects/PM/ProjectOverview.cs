// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectOverview
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PM.Project.Overview;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class ProjectOverview : ProjectEntryBase<
#nullable disable
ProjectOverview>
{
  [PXFilterable(new System.Type[] {})]
  public FbqlSelect<SelectFromBase<PMBillingRecord, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProforma>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProforma.refNbr, 
  #nullable disable
  Equal<PMBillingRecord.proformaRefNbr>>>>>.And<BqlOperand<
  #nullable enable
  PMProforma.corrected, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>, FbqlJoins.Left<PX.Objects.AR.ARInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.AR.ARInvoice.docType, 
  #nullable disable
  Equal<PMBillingRecord.aRDocType>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.AR.ARInvoice.refNbr, IBqlString>.IsEqual<
  #nullable disable
  PMBillingRecord.aRRefNbr>>>>>, PMBillingRecord>.View ProformaInvoices;
  [PXFilterable(new System.Type[] {})]
  public FbqlSelect<SelectFromBase<PMRevenueBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMRevenueBudget.projectID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PMRevenueBudget>.View RevenueBudgetReport;
  [PXFilterable(new System.Type[] {})]
  public FbqlSelect<SelectFromBase<PMCostBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMCostBudget.projectID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PMCostBudget>.View CostBudgetReport;
  public FbqlSelect<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMCostProjectionByDate.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PMCostProjectionByDate.released, IBqlBool>.IsEqual<
  #nullable disable
  True>>>.Order<By<BqlField<
  #nullable enable
  PMCostProjectionByDate.projectionDate, IBqlDateTime>.Asc>>, 
  #nullable disable
  PMCostProjectionByDate>.View ReleasedCostProjections;
  public FbqlSelect<SelectFromBase<EPApprovalProcess.EPOwned, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  EPApproval.docDate, IBqlDateTime>.Desc>>, 
  #nullable disable
  EPApprovalProcess.EPOwned>.View Approvals;
  public FbqlSelect<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ProjectARTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ProjectARTran.tranType, 
  #nullable disable
  Equal<PX.Objects.AR.ARInvoice.docType>>>>>.And<BqlOperand<
  #nullable enable
  ProjectARTran.refNbr, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.AR.ARInvoice.refNbr>>>>>.Where<BqlOperand<
  #nullable enable
  ProjectARTran.projectID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  PX.Objects.AR.ARInvoice.docDate, IBqlDateTime>.Desc>>, 
  #nullable disable
  PX.Objects.AR.ARInvoice>.View ArInvoices;
  public FbqlSelect<SelectFromBase<Restriction, TypeArrayOf<IFbqlJoin>.Empty>, Restriction>.View Restrictions;
  public FbqlSelect<SelectFromBase<NavigationTreeViewNode, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  NavigationTreeViewNode.index, IBqlInt>.Asc>>, 
  #nullable disable
  NavigationTreeViewNode>.View TreeView;
  public FbqlSelect<SelectFromBase<ScorecardState, TypeArrayOf<IFbqlJoin>.Empty>, ScorecardState>.View Scorecards;
  public PXAction<PMProject> openProjectCompleted;
  public PXAction<PMProject> openGrossProfitAtCompletion;
  public PXAction<PMProject> openGrossProfitToDate;
  public PXAction<PMProject> openMarginToDate;
  public PXAction<PMProject> openProjectedMargin;
  public PXAction<PMProject> openBudgetedRevenue;
  public PXAction<PMProject> openActualRevenueToDate;
  public PXAction<PMProject> openContractBilled;
  public PXAction<PMProject> openOverbillingUnderbilling;
  public PXAction<PMProject> openRevenueBudgetBacklog;
  public PXAction<PMProject> openExpectedRevenue;
  public PXAction<PMProject> openBudgetedCost;
  public PXAction<PMProject> openActualCostsToDate;
  public PXAction<PMProject> openAnticipatedCost;
  public PXAction<PMProject> openOpenCommitments;
  public PXAction<PMProject> openCostBudgetBacklog;
  public PXAction<PMProject> openPerformance;
  public PXAction<PMProject> openRemainingBudget;
  public PXAction<PMProject> openOutstandingAPBills;
  public PXAction<PMProject> openPotentialChangeOrders;
  public PXAction<PMProject> openOpenProjectIssues;
  public PXAction<PMProject> openOpenRFIs;
  public PXViewOf<ChartPoint>.BasedOn<SelectFromBase<ChartPoint, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly BudgetVsActualChart;
  public PXAction<PMProject> openCostProjection;
  public PXAction<PMProject> openWipAdjustments;
  public PXAction<PMProject> openPM652500;
  public PXAction<PMProject> openProjectTransactionDetails;
  public PXAction<PMProject> openProjectFinancialVision;
  public PXAction<PMProject> openSC644000;
  public PXAction<PMProject> openPM650000;
  public PXAction<PMProject> openPM650050;
  public PXAction<PMProject> openAR634100;
  public PXAction<PMProject> openAP634100;
  public PXAction<PMProject> openAR631200;
  public PXAction<PMProject> openAP631200;
  public PXAction<PMProject> openAR630600;
  public PXAction<PMProject> openAP630600;
  public PXAction<PMProject> openPM650500;
  public PXAction<PMProject> openPM622000;
  public PXAction<PMProject> openProjectEntry;
  public PXAction<PMProject> openARInvoices;
  public PXAction<PMProject> openAPInvoices;
  public PXAction<PMProject> openApprovals;
  public PXAction<PX.Objects.AR.ARInvoice> openArInvoice;
  public PXAction<EPApprovalProcess.EPOwned> openApprovalEntity;
  public PXAction<PMProject> openProjectTasks;
  public PXAction<PMProject> openProFormaInvoices;
  public PXAction<PMProject> createProjectTask;
  private IDictionary<string, IEnumerable<IDictionary<string, object>>> _queryGiCache = (IDictionary<string, IEnumerable<IDictionary<string, object>>>) new Dictionary<string, IEnumerable<IDictionary<string, object>>>();
  private IDictionary<string, PXGenericInqGrph> _giCache = (IDictionary<string, PXGenericInqGrph>) new Dictionary<string, PXGenericInqGrph>();

  [PXMergeAttributes]
  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Project Currency", Enabled = false, FieldClass = "ProjectMultiCurrency")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.curyID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDate]
  [PXUIField(DisplayName = "Start Date", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.startDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project Address", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.siteAddressID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Document Date", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.docDate> e)
  {
  }

  public ProjectOverview()
  {
    ((PXAction) this.Insert).SetVisible(false);
    ((PXAction) this.Delete).SetVisible(false);
    ((PXAction) this.Save).SetVisible(false);
    ((PXAction) this.CopyPaste).SetVisible(false);
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.construction>();
    ((PXAction) this.openPM650500).SetVisible(flag);
    ((PXAction) this.openSC644000).SetVisible(flag);
    ((PXAction) this.openPM650000).SetVisible(flag);
    ((PXAction) this.openPM650050).SetVisible(flag);
    ((PXAction) this.openCostProjection).SetVisible(flag);
    ((PXAction) this.openProjectFinancialVision).SetVisible(flag);
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    WorkflowContext<ProjectOverview, PMProject> configurationContext = config.GetScreenConfigurationContext<ProjectOverview, PMProject>();
    configurationContext.Categories.CreateNew("Financial Reports", (Func<BoundedTo<ProjectOverview, PMProject>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured) category.DisplayName("Financial Reports")));
    configurationContext.Categories.CreateNew("Time Reports", (Func<BoundedTo<ProjectOverview, PMProject>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured) category.DisplayName("Time Reports")));
    configurationContext.Categories.CreateNew("Project Invoices", (Func<BoundedTo<ProjectOverview, PMProject>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured) category.DisplayName("Project Invoices")));
    configurationContext.Categories.CreateNew("Project WIP", (Func<BoundedTo<ProjectOverview, PMProject>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured) category.DisplayName("Project WIP")));
    configurationContext.Categories.CreateNew("Project Reports", (Func<BoundedTo<ProjectOverview, PMProject>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured) category.DisplayName("Project Reports")));
    configurationContext.Categories.CreateNew("Project Settings", (Func<BoundedTo<ProjectOverview, PMProject>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectOverview, PMProject>.ActionCategory.IConfigured) category.DisplayName("Project Settings")));
    SidePanel.Configure(configurationContext);
  }

  public virtual PMCostProjectionByDate GetActualCostProjectionByDate()
  {
    return this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>();
  }

  protected virtual void OpenCostProjectionByDateEntryOrGI()
  {
    PMCostProjectionByDate projectionByDate = this.GetActualCostProjectionByDate();
    if (projectionByDate == null)
      ProjectAccountingService.NavigateToGenericIquiry("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    ProjectCostProjectionByDateEntry instance = PXGraph.CreateInstance<ProjectCostProjectionByDateEntry>();
    ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current = PMCostProjectionByDate.PK.Find((PXGraph) this, projectionByDate.RefNbr);
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
  }

  public virtual IEnumerable proformaInvoices()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = false,
      IsResultTruncated = false,
      IsResultSorted = true
    };
    PXResultset<PMBillingRecord> pxResultset = ((PXSelectBase<PMBillingRecord>) this.Invoices).Select(Array.Empty<object>());
    if (!NonGenericIEnumerableExtensions.Any_((IEnumerable) pxResultset))
      return (IEnumerable) pxDelegateResult;
    PX.Objects.AR.ARInvoice arInvoice1 = new PX.Objects.AR.ARInvoice();
    PMProforma pmProforma1 = new PMProforma();
    ((List<object>) pxDelegateResult).Add((object) new PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>(new PMBillingRecord()
    {
      RecordID = new int?(0)
    }, pmProforma1, arInvoice1));
    foreach (PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice> pxResult in pxResultset)
    {
      ((List<object>) pxDelegateResult).Add((object) pxResult);
      PX.Objects.AR.ARInvoice arInvoice2 = PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(pxResult);
      PMProforma pmProforma2 = PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(pxResult);
      PX.Objects.AR.ARInvoice arInvoice3 = arInvoice1;
      Decimal? nullable1 = arInvoice1.CuryOrigDocAmt;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = arInvoice2.CuryOrigDocAmt;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal? nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
      arInvoice3.CuryOrigDocAmt = nullable2;
      PX.Objects.AR.ARInvoice arInvoice4 = arInvoice1;
      nullable1 = arInvoice1.CuryRetainageTotal;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      nullable1 = arInvoice2.CuryRetainageTotal;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      Decimal? nullable3 = new Decimal?(valueOrDefault3 + valueOrDefault4);
      arInvoice4.CuryRetainageTotal = nullable3;
      PX.Objects.AR.ARInvoice arInvoice5 = arInvoice1;
      nullable1 = arInvoice1.CuryOrigDocAmtWithRetainageTotal;
      Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
      nullable1 = arInvoice2.CuryOrigDocAmtWithRetainageTotal;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      Decimal? nullable4 = new Decimal?(valueOrDefault5 + valueOrDefault6);
      arInvoice5.CuryOrigDocAmtWithRetainageTotal = nullable4;
      PX.Objects.AR.ARInvoice arInvoice6 = arInvoice1;
      nullable1 = arInvoice1.CuryDocBal;
      Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
      nullable1 = arInvoice2.CuryDocBal;
      Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
      Decimal? nullable5 = new Decimal?(valueOrDefault7 + valueOrDefault8);
      arInvoice6.CuryDocBal = nullable5;
      PX.Objects.AR.ARInvoice arInvoice7 = arInvoice1;
      nullable1 = arInvoice1.CuryRetainageUnreleasedAmt;
      Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
      nullable1 = arInvoice2.CuryRetainageUnreleasedAmt;
      Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
      Decimal? nullable6 = new Decimal?(valueOrDefault9 + valueOrDefault10);
      arInvoice7.CuryRetainageUnreleasedAmt = nullable6;
      PX.Objects.AR.ARInvoice arInvoice8 = arInvoice1;
      nullable1 = arInvoice1.CuryRetainageReleased;
      Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
      nullable1 = arInvoice2.CuryRetainageReleased;
      Decimal valueOrDefault12 = nullable1.GetValueOrDefault();
      Decimal? nullable7 = new Decimal?(valueOrDefault11 + valueOrDefault12);
      arInvoice8.CuryRetainageReleased = nullable7;
      PX.Objects.AR.ARInvoice arInvoice9 = arInvoice1;
      nullable1 = arInvoice1.CuryRetainagePaidTotal;
      Decimal valueOrDefault13 = nullable1.GetValueOrDefault();
      nullable1 = arInvoice2.CuryRetainagePaidTotal;
      Decimal valueOrDefault14 = nullable1.GetValueOrDefault();
      Decimal? nullable8 = new Decimal?(valueOrDefault13 + valueOrDefault14);
      arInvoice9.CuryRetainagePaidTotal = nullable8;
      PMProforma pmProforma3 = pmProforma1;
      nullable1 = pmProforma1.CuryDocTotal;
      Decimal valueOrDefault15 = nullable1.GetValueOrDefault();
      nullable1 = pmProforma2.CuryDocTotal;
      Decimal valueOrDefault16 = nullable1.GetValueOrDefault();
      Decimal? nullable9 = new Decimal?(valueOrDefault15 + valueOrDefault16);
      pmProforma3.CuryDocTotal = nullable9;
    }
    return (IEnumerable) pxDelegateResult;
  }

  public virtual IEnumerable revenueBudgetReport()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = false,
      IsResultTruncated = false,
      IsResultSorted = false
    };
    PXResultset<PMRevenueBudget> pxResultset = ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>());
    if (!NonGenericIEnumerableExtensions.Any_((IEnumerable) pxResultset))
      return (IEnumerable) pxDelegateResult;
    PMRevenueBudget totalBudget = new PMRevenueBudget();
    ((List<object>) pxDelegateResult).Add((object) totalBudget);
    foreach (PXResult<PMRevenueBudget> pxResult in pxResultset)
    {
      PMRevenueBudget itemBudget = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
      this.AddBudgets((PMBudget) totalBudget, (PMBudget) itemBudget);
      ((List<object>) pxDelegateResult).Add((object) itemBudget);
    }
    return (IEnumerable) pxDelegateResult;
  }

  public virtual IEnumerable costBudgetReport()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = false,
      IsResultTruncated = false,
      IsResultSorted = false
    };
    PXResultset<PMCostBudget> pxResultset = ((PXSelectBase<PMCostBudget>) this.CostBudget).Select(Array.Empty<object>());
    if (!NonGenericIEnumerableExtensions.Any_((IEnumerable) pxResultset))
      return (IEnumerable) pxDelegateResult;
    PMCostBudget totalBudget = new PMCostBudget();
    ((List<object>) pxDelegateResult).Add((object) totalBudget);
    foreach (PXResult<PMCostBudget> pxResult in pxResultset)
    {
      PMCostBudget itemBudget = PXResult<PMCostBudget>.op_Implicit(pxResult);
      this.AddBudgets((PMBudget) totalBudget, (PMBudget) itemBudget);
      ((List<object>) pxDelegateResult).Add((object) itemBudget);
    }
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual void AddBudgets(PMBudget totalBudget, PMBudget itemBudget)
  {
    PMBudget pmBudget1 = totalBudget;
    Decimal? curyActualAmount = totalBudget.CuryActualAmount;
    Decimal valueOrDefault1 = curyActualAmount.GetValueOrDefault();
    curyActualAmount = itemBudget.CuryActualAmount;
    Decimal valueOrDefault2 = curyActualAmount.GetValueOrDefault();
    Decimal? nullable1 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    pmBudget1.CuryActualAmount = nullable1;
    PMBudget pmBudget2 = totalBudget;
    Decimal? curyAmount = totalBudget.CuryAmount;
    Decimal valueOrDefault3 = curyAmount.GetValueOrDefault();
    curyAmount = itemBudget.CuryAmount;
    Decimal valueOrDefault4 = curyAmount.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(valueOrDefault3 + valueOrDefault4);
    pmBudget2.CuryAmount = nullable2;
    PMBudget pmBudget3 = totalBudget;
    Decimal? curyAmountToInvoice = totalBudget.CuryAmountToInvoice;
    Decimal valueOrDefault5 = curyAmountToInvoice.GetValueOrDefault();
    curyAmountToInvoice = itemBudget.CuryAmountToInvoice;
    Decimal valueOrDefault6 = curyAmountToInvoice.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(valueOrDefault5 + valueOrDefault6);
    pmBudget3.CuryAmountToInvoice = nullable3;
    PMBudget pmBudget4 = totalBudget;
    Decimal? changeOrderAmount1 = totalBudget.CuryChangeOrderAmount;
    Decimal valueOrDefault7 = changeOrderAmount1.GetValueOrDefault();
    changeOrderAmount1 = itemBudget.CuryChangeOrderAmount;
    Decimal valueOrDefault8 = changeOrderAmount1.GetValueOrDefault();
    Decimal? nullable4 = new Decimal?(valueOrDefault7 + valueOrDefault8);
    pmBudget4.CuryChangeOrderAmount = nullable4;
    PMBudget pmBudget5 = totalBudget;
    Decimal? curyCommittedAmount = totalBudget.CuryCommittedAmount;
    Decimal valueOrDefault9 = curyCommittedAmount.GetValueOrDefault();
    curyCommittedAmount = itemBudget.CuryCommittedAmount;
    Decimal valueOrDefault10 = curyCommittedAmount.GetValueOrDefault();
    Decimal? nullable5 = new Decimal?(valueOrDefault9 + valueOrDefault10);
    pmBudget5.CuryCommittedAmount = nullable5;
    PMBudget pmBudget6 = totalBudget;
    Decimal? committedInvoicedAmount1 = totalBudget.CuryCommittedInvoicedAmount;
    Decimal valueOrDefault11 = committedInvoicedAmount1.GetValueOrDefault();
    committedInvoicedAmount1 = itemBudget.CuryCommittedInvoicedAmount;
    Decimal valueOrDefault12 = committedInvoicedAmount1.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(valueOrDefault11 + valueOrDefault12);
    pmBudget6.CuryCommittedInvoicedAmount = nullable6;
    PMBudget pmBudget7 = totalBudget;
    Decimal? committedOpenAmount1 = totalBudget.CuryCommittedOpenAmount;
    Decimal valueOrDefault13 = committedOpenAmount1.GetValueOrDefault();
    committedOpenAmount1 = itemBudget.CuryCommittedOpenAmount;
    Decimal valueOrDefault14 = committedOpenAmount1.GetValueOrDefault();
    Decimal? nullable7 = new Decimal?(valueOrDefault13 + valueOrDefault14);
    pmBudget7.CuryCommittedOpenAmount = nullable7;
    PMBudget pmBudget8 = totalBudget;
    Decimal? committedOrigAmount1 = totalBudget.CuryCommittedOrigAmount;
    Decimal valueOrDefault15 = committedOrigAmount1.GetValueOrDefault();
    committedOrigAmount1 = itemBudget.CuryCommittedOrigAmount;
    Decimal valueOrDefault16 = committedOrigAmount1.GetValueOrDefault();
    Decimal? nullable8 = new Decimal?(valueOrDefault15 + valueOrDefault16);
    pmBudget8.CuryCommittedOrigAmount = nullable8;
    PMBudget pmBudget9 = totalBudget;
    Decimal? costAtCompletion1 = totalBudget.CuryCostAtCompletion;
    Decimal valueOrDefault17 = costAtCompletion1.GetValueOrDefault();
    costAtCompletion1 = itemBudget.CuryCostAtCompletion;
    Decimal valueOrDefault18 = costAtCompletion1.GetValueOrDefault();
    Decimal? nullable9 = new Decimal?(valueOrDefault17 + valueOrDefault18);
    pmBudget9.CuryCostAtCompletion = nullable9;
    PMBudget pmBudget10 = totalBudget;
    Decimal? costAtCompletion2 = totalBudget.CuryCostProjectionCostAtCompletion;
    Decimal valueOrDefault19 = costAtCompletion2.GetValueOrDefault();
    costAtCompletion2 = itemBudget.CuryCostProjectionCostAtCompletion;
    Decimal valueOrDefault20 = costAtCompletion2.GetValueOrDefault();
    Decimal? nullable10 = new Decimal?(valueOrDefault19 + valueOrDefault20);
    pmBudget10.CuryCostProjectionCostAtCompletion = nullable10;
    PMBudget pmBudget11 = totalBudget;
    Decimal? projectionCostToComplete1 = totalBudget.CuryCostProjectionCostToComplete;
    Decimal valueOrDefault21 = projectionCostToComplete1.GetValueOrDefault();
    projectionCostToComplete1 = itemBudget.CuryCostProjectionCostToComplete;
    Decimal valueOrDefault22 = projectionCostToComplete1.GetValueOrDefault();
    Decimal? nullable11 = new Decimal?(valueOrDefault21 + valueOrDefault22);
    pmBudget11.CuryCostProjectionCostToComplete = nullable11;
    PMBudget pmBudget12 = totalBudget;
    Decimal? curyCostToComplete = totalBudget.CuryCostToComplete;
    Decimal valueOrDefault23 = curyCostToComplete.GetValueOrDefault();
    curyCostToComplete = itemBudget.CuryCostToComplete;
    Decimal valueOrDefault24 = curyCostToComplete.GetValueOrDefault();
    Decimal? nullable12 = new Decimal?(valueOrDefault23 + valueOrDefault24);
    pmBudget12.CuryCostToComplete = nullable12;
    PMBudget pmBudget13 = totalBudget;
    Decimal? changeOrderAmount2 = totalBudget.CuryDraftChangeOrderAmount;
    Decimal valueOrDefault25 = changeOrderAmount2.GetValueOrDefault();
    changeOrderAmount2 = itemBudget.CuryDraftChangeOrderAmount;
    Decimal valueOrDefault26 = changeOrderAmount2.GetValueOrDefault();
    Decimal? nullable13 = new Decimal?(valueOrDefault25 + valueOrDefault26);
    pmBudget13.CuryDraftChangeOrderAmount = nullable13;
    PMBudget pmBudget14 = totalBudget;
    Decimal? draftRetainedAmount1 = totalBudget.CuryDraftRetainedAmount;
    Decimal valueOrDefault27 = draftRetainedAmount1.GetValueOrDefault();
    draftRetainedAmount1 = itemBudget.CuryDraftRetainedAmount;
    Decimal valueOrDefault28 = draftRetainedAmount1.GetValueOrDefault();
    Decimal? nullable14 = new Decimal?(valueOrDefault27 + valueOrDefault28);
    pmBudget14.CuryDraftRetainedAmount = nullable14;
    PMBudget pmBudget15 = totalBudget;
    Decimal? curyInclTaxAmount = totalBudget.CuryInclTaxAmount;
    Decimal valueOrDefault29 = curyInclTaxAmount.GetValueOrDefault();
    curyInclTaxAmount = itemBudget.CuryInclTaxAmount;
    Decimal valueOrDefault30 = curyInclTaxAmount.GetValueOrDefault();
    Decimal? nullable15 = new Decimal?(valueOrDefault29 + valueOrDefault30);
    pmBudget15.CuryInclTaxAmount = nullable15;
    PMBudget pmBudget16 = totalBudget;
    Decimal? curyInvoicedAmount = totalBudget.CuryInvoicedAmount;
    Decimal valueOrDefault31 = curyInvoicedAmount.GetValueOrDefault();
    curyInvoicedAmount = itemBudget.CuryInvoicedAmount;
    Decimal valueOrDefault32 = curyInvoicedAmount.GetValueOrDefault();
    Decimal? nullable16 = new Decimal?(valueOrDefault31 + valueOrDefault32);
    pmBudget16.CuryInvoicedAmount = nullable16;
    PMBudget pmBudget17 = totalBudget;
    Decimal? costAtCompletion3 = totalBudget.CuryLastCostAtCompletion;
    Decimal valueOrDefault33 = costAtCompletion3.GetValueOrDefault();
    costAtCompletion3 = itemBudget.CuryLastCostAtCompletion;
    Decimal valueOrDefault34 = costAtCompletion3.GetValueOrDefault();
    Decimal? nullable17 = new Decimal?(valueOrDefault33 + valueOrDefault34);
    pmBudget17.CuryLastCostAtCompletion = nullable17;
    PMBudget pmBudget18 = totalBudget;
    Decimal? lastCostToComplete1 = totalBudget.CuryLastCostToComplete;
    Decimal valueOrDefault35 = lastCostToComplete1.GetValueOrDefault();
    lastCostToComplete1 = itemBudget.CuryLastCostToComplete;
    Decimal valueOrDefault36 = lastCostToComplete1.GetValueOrDefault();
    Decimal? nullable18 = new Decimal?(valueOrDefault35 + valueOrDefault36);
    pmBudget18.CuryLastCostToComplete = nullable18;
    PMBudget pmBudget19 = totalBudget;
    Decimal? curyMaxAmount = totalBudget.CuryMaxAmount;
    Decimal valueOrDefault37 = curyMaxAmount.GetValueOrDefault();
    curyMaxAmount = itemBudget.CuryMaxAmount;
    Decimal valueOrDefault38 = curyMaxAmount.GetValueOrDefault();
    Decimal? nullable19 = new Decimal?(valueOrDefault37 + valueOrDefault38);
    pmBudget19.CuryMaxAmount = nullable19;
    PMBudget pmBudget20 = totalBudget;
    Decimal? curyRetainedAmount = totalBudget.CuryRetainedAmount;
    Decimal valueOrDefault39 = curyRetainedAmount.GetValueOrDefault();
    curyRetainedAmount = itemBudget.CuryRetainedAmount;
    Decimal valueOrDefault40 = curyRetainedAmount.GetValueOrDefault();
    Decimal? nullable20 = new Decimal?(valueOrDefault39 + valueOrDefault40);
    pmBudget20.CuryRetainedAmount = nullable20;
    PMBudget pmBudget21 = totalBudget;
    Decimal? curyRevisedAmount = totalBudget.CuryRevisedAmount;
    Decimal valueOrDefault41 = curyRevisedAmount.GetValueOrDefault();
    curyRevisedAmount = itemBudget.CuryRevisedAmount;
    Decimal valueOrDefault42 = curyRevisedAmount.GetValueOrDefault();
    Decimal? nullable21 = new Decimal?(valueOrDefault41 + valueOrDefault42);
    pmBudget21.CuryRevisedAmount = nullable21;
    PMBudget pmBudget22 = totalBudget;
    Decimal? totalRetainedAmount1 = totalBudget.CuryTotalRetainedAmount;
    Decimal valueOrDefault43 = totalRetainedAmount1.GetValueOrDefault();
    totalRetainedAmount1 = itemBudget.CuryTotalRetainedAmount;
    Decimal valueOrDefault44 = totalRetainedAmount1.GetValueOrDefault();
    Decimal? nullable22 = new Decimal?(valueOrDefault43 + valueOrDefault44);
    pmBudget22.CuryTotalRetainedAmount = nullable22;
    PMBudget pmBudget23 = totalBudget;
    Decimal? actualAmount = totalBudget.ActualAmount;
    Decimal valueOrDefault45 = actualAmount.GetValueOrDefault();
    actualAmount = itemBudget.ActualAmount;
    Decimal valueOrDefault46 = actualAmount.GetValueOrDefault();
    Decimal? nullable23 = new Decimal?(valueOrDefault45 + valueOrDefault46);
    pmBudget23.ActualAmount = nullable23;
    PMBudget pmBudget24 = totalBudget;
    Decimal? amount = totalBudget.Amount;
    Decimal valueOrDefault47 = amount.GetValueOrDefault();
    amount = itemBudget.Amount;
    Decimal valueOrDefault48 = amount.GetValueOrDefault();
    Decimal? nullable24 = new Decimal?(valueOrDefault47 + valueOrDefault48);
    pmBudget24.Amount = nullable24;
    PMBudget pmBudget25 = totalBudget;
    Decimal? amountToInvoice = totalBudget.AmountToInvoice;
    Decimal valueOrDefault49 = amountToInvoice.GetValueOrDefault();
    amountToInvoice = itemBudget.AmountToInvoice;
    Decimal valueOrDefault50 = amountToInvoice.GetValueOrDefault();
    Decimal? nullable25 = new Decimal?(valueOrDefault49 + valueOrDefault50);
    pmBudget25.AmountToInvoice = nullable25;
    PMBudget pmBudget26 = totalBudget;
    Decimal? changeOrderAmount3 = totalBudget.ChangeOrderAmount;
    Decimal valueOrDefault51 = changeOrderAmount3.GetValueOrDefault();
    changeOrderAmount3 = itemBudget.ChangeOrderAmount;
    Decimal valueOrDefault52 = changeOrderAmount3.GetValueOrDefault();
    Decimal? nullable26 = new Decimal?(valueOrDefault51 + valueOrDefault52);
    pmBudget26.ChangeOrderAmount = nullable26;
    PMBudget pmBudget27 = totalBudget;
    Decimal? committedAmount = totalBudget.CommittedAmount;
    Decimal valueOrDefault53 = committedAmount.GetValueOrDefault();
    committedAmount = itemBudget.CommittedAmount;
    Decimal valueOrDefault54 = committedAmount.GetValueOrDefault();
    Decimal? nullable27 = new Decimal?(valueOrDefault53 + valueOrDefault54);
    pmBudget27.CommittedAmount = nullable27;
    PMBudget pmBudget28 = totalBudget;
    Decimal? committedInvoicedAmount2 = totalBudget.CommittedInvoicedAmount;
    Decimal valueOrDefault55 = committedInvoicedAmount2.GetValueOrDefault();
    committedInvoicedAmount2 = itemBudget.CommittedInvoicedAmount;
    Decimal valueOrDefault56 = committedInvoicedAmount2.GetValueOrDefault();
    Decimal? nullable28 = new Decimal?(valueOrDefault55 + valueOrDefault56);
    pmBudget28.CommittedInvoicedAmount = nullable28;
    PMBudget pmBudget29 = totalBudget;
    Decimal? committedOpenAmount2 = totalBudget.CommittedOpenAmount;
    Decimal valueOrDefault57 = committedOpenAmount2.GetValueOrDefault();
    committedOpenAmount2 = itemBudget.CommittedOpenAmount;
    Decimal valueOrDefault58 = committedOpenAmount2.GetValueOrDefault();
    Decimal? nullable29 = new Decimal?(valueOrDefault57 + valueOrDefault58);
    pmBudget29.CommittedOpenAmount = nullable29;
    PMBudget pmBudget30 = totalBudget;
    Decimal? committedOrigAmount2 = totalBudget.CommittedOrigAmount;
    Decimal valueOrDefault59 = committedOrigAmount2.GetValueOrDefault();
    committedOrigAmount2 = itemBudget.CommittedOrigAmount;
    Decimal valueOrDefault60 = committedOrigAmount2.GetValueOrDefault();
    Decimal? nullable30 = new Decimal?(valueOrDefault59 + valueOrDefault60);
    pmBudget30.CommittedOrigAmount = nullable30;
    PMBudget pmBudget31 = totalBudget;
    Decimal? costAtCompletion4 = totalBudget.CostAtCompletion;
    Decimal valueOrDefault61 = costAtCompletion4.GetValueOrDefault();
    costAtCompletion4 = itemBudget.CostAtCompletion;
    Decimal valueOrDefault62 = costAtCompletion4.GetValueOrDefault();
    Decimal? nullable31 = new Decimal?(valueOrDefault61 + valueOrDefault62);
    pmBudget31.CostAtCompletion = nullable31;
    PMBudget pmBudget32 = totalBudget;
    Decimal? costAtCompletion5 = totalBudget.CostProjectionCostAtCompletion;
    Decimal valueOrDefault63 = costAtCompletion5.GetValueOrDefault();
    costAtCompletion5 = itemBudget.CostProjectionCostAtCompletion;
    Decimal valueOrDefault64 = costAtCompletion5.GetValueOrDefault();
    Decimal? nullable32 = new Decimal?(valueOrDefault63 + valueOrDefault64);
    pmBudget32.CostProjectionCostAtCompletion = nullable32;
    PMBudget pmBudget33 = totalBudget;
    Decimal? projectionCostToComplete2 = totalBudget.CostProjectionCostToComplete;
    Decimal valueOrDefault65 = projectionCostToComplete2.GetValueOrDefault();
    projectionCostToComplete2 = itemBudget.CostProjectionCostToComplete;
    Decimal valueOrDefault66 = projectionCostToComplete2.GetValueOrDefault();
    Decimal? nullable33 = new Decimal?(valueOrDefault65 + valueOrDefault66);
    pmBudget33.CostProjectionCostToComplete = nullable33;
    PMBudget pmBudget34 = totalBudget;
    Decimal? costToComplete = totalBudget.CostToComplete;
    Decimal valueOrDefault67 = costToComplete.GetValueOrDefault();
    costToComplete = itemBudget.CostToComplete;
    Decimal valueOrDefault68 = costToComplete.GetValueOrDefault();
    Decimal? nullable34 = new Decimal?(valueOrDefault67 + valueOrDefault68);
    pmBudget34.CostToComplete = nullable34;
    PMBudget pmBudget35 = totalBudget;
    Decimal? changeOrderAmount4 = totalBudget.DraftChangeOrderAmount;
    Decimal valueOrDefault69 = changeOrderAmount4.GetValueOrDefault();
    changeOrderAmount4 = itemBudget.DraftChangeOrderAmount;
    Decimal valueOrDefault70 = changeOrderAmount4.GetValueOrDefault();
    Decimal? nullable35 = new Decimal?(valueOrDefault69 + valueOrDefault70);
    pmBudget35.DraftChangeOrderAmount = nullable35;
    PMBudget pmBudget36 = totalBudget;
    Decimal? draftRetainedAmount2 = totalBudget.DraftRetainedAmount;
    Decimal valueOrDefault71 = draftRetainedAmount2.GetValueOrDefault();
    draftRetainedAmount2 = itemBudget.DraftRetainedAmount;
    Decimal valueOrDefault72 = draftRetainedAmount2.GetValueOrDefault();
    Decimal? nullable36 = new Decimal?(valueOrDefault71 + valueOrDefault72);
    pmBudget36.DraftRetainedAmount = nullable36;
    PMBudget pmBudget37 = totalBudget;
    Decimal? inclTaxAmount = totalBudget.InclTaxAmount;
    Decimal valueOrDefault73 = inclTaxAmount.GetValueOrDefault();
    inclTaxAmount = itemBudget.InclTaxAmount;
    Decimal valueOrDefault74 = inclTaxAmount.GetValueOrDefault();
    Decimal? nullable37 = new Decimal?(valueOrDefault73 + valueOrDefault74);
    pmBudget37.InclTaxAmount = nullable37;
    PMBudget pmBudget38 = totalBudget;
    Decimal? invoicedAmount = totalBudget.InvoicedAmount;
    Decimal valueOrDefault75 = invoicedAmount.GetValueOrDefault();
    invoicedAmount = itemBudget.InvoicedAmount;
    Decimal valueOrDefault76 = invoicedAmount.GetValueOrDefault();
    Decimal? nullable38 = new Decimal?(valueOrDefault75 + valueOrDefault76);
    pmBudget38.InvoicedAmount = nullable38;
    PMBudget pmBudget39 = totalBudget;
    Decimal? costAtCompletion6 = totalBudget.LastCostAtCompletion;
    Decimal valueOrDefault77 = costAtCompletion6.GetValueOrDefault();
    costAtCompletion6 = itemBudget.LastCostAtCompletion;
    Decimal valueOrDefault78 = costAtCompletion6.GetValueOrDefault();
    Decimal? nullable39 = new Decimal?(valueOrDefault77 + valueOrDefault78);
    pmBudget39.LastCostAtCompletion = nullable39;
    PMBudget pmBudget40 = totalBudget;
    Decimal? lastCostToComplete2 = totalBudget.LastCostToComplete;
    Decimal valueOrDefault79 = lastCostToComplete2.GetValueOrDefault();
    lastCostToComplete2 = itemBudget.LastCostToComplete;
    Decimal valueOrDefault80 = lastCostToComplete2.GetValueOrDefault();
    Decimal? nullable40 = new Decimal?(valueOrDefault79 + valueOrDefault80);
    pmBudget40.LastCostToComplete = nullable40;
    PMBudget pmBudget41 = totalBudget;
    Decimal? maxAmount = totalBudget.MaxAmount;
    Decimal valueOrDefault81 = maxAmount.GetValueOrDefault();
    maxAmount = itemBudget.MaxAmount;
    Decimal valueOrDefault82 = maxAmount.GetValueOrDefault();
    Decimal? nullable41 = new Decimal?(valueOrDefault81 + valueOrDefault82);
    pmBudget41.MaxAmount = nullable41;
    PMBudget pmBudget42 = totalBudget;
    Decimal? retainedAmount = totalBudget.RetainedAmount;
    Decimal valueOrDefault83 = retainedAmount.GetValueOrDefault();
    retainedAmount = itemBudget.RetainedAmount;
    Decimal valueOrDefault84 = retainedAmount.GetValueOrDefault();
    Decimal? nullable42 = new Decimal?(valueOrDefault83 + valueOrDefault84);
    pmBudget42.RetainedAmount = nullable42;
    PMBudget pmBudget43 = totalBudget;
    Decimal? revisedAmount = totalBudget.RevisedAmount;
    Decimal valueOrDefault85 = revisedAmount.GetValueOrDefault();
    revisedAmount = itemBudget.RevisedAmount;
    Decimal valueOrDefault86 = revisedAmount.GetValueOrDefault();
    Decimal? nullable43 = new Decimal?(valueOrDefault85 + valueOrDefault86);
    pmBudget43.RevisedAmount = nullable43;
    PMBudget pmBudget44 = totalBudget;
    Decimal? totalRetainedAmount2 = totalBudget.TotalRetainedAmount;
    Decimal valueOrDefault87 = totalRetainedAmount2.GetValueOrDefault();
    totalRetainedAmount2 = itemBudget.TotalRetainedAmount;
    Decimal valueOrDefault88 = totalRetainedAmount2.GetValueOrDefault();
    Decimal? nullable44 = new Decimal?(valueOrDefault87 + valueOrDefault88);
    pmBudget44.TotalRetainedAmount = nullable44;
  }

  public virtual int MaxApprovalRecordsToShow { get; set; } = 5;

  public IEnumerable approvals()
  {
    ProjectOverview projectOverview = this;
    PXResultset<EPApprovalProcess.EPOwned> pxResultset = PXSelectBase<EPApprovalProcess.EPOwned, PXViewOf<EPApprovalProcess.EPOwned>.BasedOn<SelectFromBase<EPApprovalProcess.EPOwned, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<EPApproval.docDate, IBqlDateTime>.Desc>>>.Config>.Select((PXGraph) projectOverview, Array.Empty<object>());
    EntityHelper helper = new EntityHelper((PXGraph) projectOverview);
    int recordsCount = 0;
    foreach (PXResult<EPApprovalProcess.EPOwned> pxResult in pxResultset)
    {
      EPApprovalProcess.EPOwned epOwned = PXResult<EPApprovalProcess.EPOwned>.op_Implicit(pxResult);
      if (projectOverview.IsProjectItem(helper.GetEntityRow(epOwned.RefNoteID), (int?) ((PXSelectBase<PMProject>) projectOverview.Project).Current?.ProjectID))
      {
        yield return (object) epOwned;
        ++recordsCount;
      }
      if (projectOverview.MaxApprovalRecordsToShow > 0 && recordsCount == projectOverview.MaxApprovalRecordsToShow)
        break;
    }
  }

  public virtual bool IsProjectItem(object entity, int? projectID)
  {
    bool flag;
    switch (entity)
    {
      case PX.Objects.AP.APInvoice apInvoice:
        int? projectId1 = apInvoice.ProjectID;
        int? nullable1 = projectID;
        flag = projectId1.GetValueOrDefault() == nullable1.GetValueOrDefault() & projectId1.HasValue == nullable1.HasValue;
        break;
      case PX.Objects.AR.ARInvoice arInvoice:
        int? projectId2 = arInvoice.ProjectID;
        int? nullable2 = projectID;
        flag = projectId2.GetValueOrDefault() == nullable2.GetValueOrDefault() & projectId2.HasValue == nullable2.HasValue;
        break;
      case PMChangeOrder pmChangeOrder:
        int? projectId3 = pmChangeOrder.ProjectID;
        int? nullable3 = projectID;
        flag = projectId3.GetValueOrDefault() == nullable3.GetValueOrDefault() & projectId3.HasValue == nullable3.HasValue;
        break;
      case PMChangeRequest pmChangeRequest:
        int? projectId4 = pmChangeRequest.ProjectID;
        int? nullable4 = projectID;
        flag = projectId4.GetValueOrDefault() == nullable4.GetValueOrDefault() & projectId4.HasValue == nullable4.HasValue;
        break;
      case PMCostProjection pmCostProjection:
        int? projectId5 = pmCostProjection.ProjectID;
        int? nullable5 = projectID;
        flag = projectId5.GetValueOrDefault() == nullable5.GetValueOrDefault() & projectId5.HasValue == nullable5.HasValue;
        break;
      case PMCostProjectionByDate projectionByDate:
        int? projectId6 = projectionByDate.ProjectID;
        int? nullable6 = projectID;
        flag = projectId6.GetValueOrDefault() == nullable6.GetValueOrDefault() & projectId6.HasValue == nullable6.HasValue;
        break;
      case EPExpenseClaimDetails expenseClaimDetails:
        int? contractId = expenseClaimDetails.ContractID;
        int? nullable7 = projectID;
        flag = contractId.GetValueOrDefault() == nullable7.GetValueOrDefault() & contractId.HasValue == nullable7.HasValue;
        break;
      case PMProforma pmProforma:
        int? projectId7 = pmProforma.ProjectID;
        int? nullable8 = projectID;
        flag = projectId7.GetValueOrDefault() == nullable8.GetValueOrDefault() & projectId7.HasValue == nullable8.HasValue;
        break;
      case PMProgressWorksheet progressWorksheet:
        int? projectId8 = progressWorksheet.ProjectID;
        int? nullable9 = projectID;
        flag = projectId8.GetValueOrDefault() == nullable9.GetValueOrDefault() & projectId8.HasValue == nullable9.HasValue;
        break;
      case PMQuote pmQuote:
        int? projectId9 = pmQuote.ProjectID;
        int? nullable10 = projectID;
        flag = projectId9.GetValueOrDefault() == nullable10.GetValueOrDefault() & projectId9.HasValue == nullable10.HasValue;
        break;
      case PX.Objects.PO.POOrder poOrder:
        int? projectId10 = poOrder.ProjectID;
        int? nullable11 = projectID;
        flag = projectId10.GetValueOrDefault() == nullable11.GetValueOrDefault() & projectId10.HasValue == nullable11.HasValue;
        break;
      case PX.Objects.SO.SOOrder soOrder:
        int? projectId11 = soOrder.ProjectID;
        int? nullable12 = projectID;
        flag = projectId11.GetValueOrDefault() == nullable12.GetValueOrDefault() & projectId11.HasValue == nullable12.HasValue;
        break;
      case IProjectHeader projectHeader:
        int? projectId12 = projectHeader.ProjectID;
        int? nullable13 = projectID;
        flag = projectId12.GetValueOrDefault() == nullable13.GetValueOrDefault() & projectId12.HasValue == nullable13.HasValue;
        break;
      default:
        flag = false;
        break;
    }
    return flag;
  }

  public virtual int MaxArInvoiceRecordsToShow { get; set; } = 5;

  public virtual IEnumerable arInvoices()
  {
    return (IEnumerable) PXSelectBase<PX.Objects.AR.ARInvoice, PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ProjectARTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ProjectARTran.tranType, Equal<PX.Objects.AR.ARInvoice.docType>>>>>.And<BqlOperand<ProjectARTran.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARInvoice.refNbr>>>>>.Where<BqlOperand<ProjectARTran.projectID, IBqlInt>.IsEqual<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>.Order<By<BqlField<PX.Objects.AR.ARInvoice.docDate, IBqlDateTime>.Desc>>>.Config>.SelectWindowed((PXGraph) this, 0, this.MaxArInvoiceRecordsToShow, Array.Empty<object>());
  }

  public virtual IEnumerable restrictions()
  {
    yield return (object) RestrictionHelper.CreateByScreenAccessRights("AP3010PL");
    yield return (object) RestrictionHelper.CreateByScreenAccessRights("AR3010PL");
    yield return (object) RestrictionHelper.CreateByScreenAccessRights<FeaturesSet.approvalWorkflow>("EP503010");
    foreach (object extraRestriction in this.GetExtraRestrictions())
      yield return extraRestriction;
  }

  public virtual IEnumerable<Restriction> GetExtraRestrictions() => Enumerable.Empty<Restriction>();

  public virtual IEnumerable treeView()
  {
    if (NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.TreeView).Cache.Cached))
      return (IEnumerable) GraphHelper.RowCast<NavigationTreeViewNode>(((PXSelectBase) this.TreeView).Cache.Cached);
    IEnumerable<NavigationTreeViewNode> navigationTreeViewNodes = this.GetTree().Concat<NavigationTreeViewNode>(this.GetExtraTreeViewNodes());
    int num = 0;
    foreach (NavigationTreeViewNode navigationTreeViewNode in navigationTreeViewNodes)
    {
      navigationTreeViewNode.Index = new int?(++num);
      GraphHelper.Hold(((PXSelectBase) this.TreeView).Cache, (object) navigationTreeViewNode);
    }
    return (IEnumerable) navigationTreeViewNodes;
  }

  public virtual IEnumerable<NavigationTreeViewNode> GetTree()
  {
    return (IEnumerable<NavigationTreeViewNode>) DefaultNavigation.GetNavigationTree();
  }

  public virtual IEnumerable<NavigationTreeViewNode> GetExtraTreeViewNodes()
  {
    return Enumerable.Empty<NavigationTreeViewNode>();
  }

  public virtual IEnumerable scorecards()
  {
    yield return (object) this.GetProjectCompletedScorecard();
    yield return (object) this.GetGrossProfitAtCompletionScorecard();
    yield return (object) this.GetGrossProfitToDateScorecard();
    yield return (object) this.GetMarginToDateScorecard();
    yield return (object) this.GetProjectedMarginScorecard();
    yield return (object) this.GetBudgetedRevenueScorecard();
    yield return (object) this.GetActualRevenueToDateScorecard();
    yield return (object) this.GetContractBilledScorecard();
    yield return (object) this.GetOverbillingUnderbillingScorecard();
    yield return (object) this.GetRevenueBudgetBacklogScorecard();
    yield return (object) this.GetExpectedRevenueScorecard();
    yield return (object) this.GetBudgetedCostScorecard();
    yield return (object) this.GetActualCostsToDateScorecard();
    yield return (object) this.GetAnticipatedCostScorecard();
    yield return (object) this.GetOpenCommitmentsScorecard();
    yield return (object) this.GetCostBudgetBacklogScorecard();
    yield return (object) this.GetPerformanceScorecard();
    yield return (object) this.GetRemainingBudgetScorecard();
    yield return (object) this.GetOutstandingAPBillsScorecard();
    yield return (object) this.GetPotentialChangeOrders();
    yield return (object) this.GetOpenProjectIssuesScorecard();
    yield return (object) this.GetOpenRFIsScorecard();
    foreach (object extraScorecard in this.GetExtraScorecards())
      yield return extraScorecard;
  }

  public virtual IEnumerable<ScorecardState> GetExtraScorecards()
  {
    return Enumerable.Empty<ScorecardState>();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenProjectCompleted(PXAdapter adapter)
  {
    this.OpenCostProjectionByDateEntryOrGI();
    return adapter.Get();
  }

  protected virtual ScorecardState GetProjectCompletedScorecard()
  {
    ScorecardState completedScorecard = new ScorecardState()
    {
      Key = "ProjectCompleted",
      Name = "Project Completion",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.Construction) || !PXAccess.VerifyRights("PMGI3055"))
    };
    if (completedScorecard.Disabled.GetValueOrDefault())
      return completedScorecard;
    Decimal valueOrDefault = ((Decimal?) this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>()?.CompletedPctTotal).GetValueOrDefault();
    completedScorecard.Value = ValueFormatter.FormatPercent(valueOrDefault);
    completedScorecard.Level = LevelHelper.NormalNonConditional();
    return completedScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenGrossProfitAtCompletion(PXAdapter adapter)
  {
    this.OpenCostProjectionByDateEntryOrGI();
    return adapter.Get();
  }

  protected virtual ScorecardState GetGrossProfitAtCompletionScorecard()
  {
    ScorecardState completionScorecard = new ScorecardState()
    {
      Key = "GrossProfitAtCompletion",
      Name = "Gross Profit at Completion",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.ProjectAccounting))
    };
    if (completionScorecard.Disabled.GetValueOrDefault())
      return completionScorecard;
    Decimal valueOrDefault = ((Decimal?) this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>()?.CuryProjectedMarginTotal).GetValueOrDefault();
    completionScorecard.Value = ValueFormatter.FormatAmount(valueOrDefault);
    completionScorecard.Level = LevelHelper.NormalIfPositive(valueOrDefault);
    return completionScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenGrossProfitToDate(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PM658100", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Project, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  protected virtual ScorecardState GetGrossProfitToDateScorecard()
  {
    ScorecardState profitToDateScorecard = new ScorecardState()
    {
      Key = "GrossProfitToDate",
      Name = "Gross Profit to Date",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.ProjectAccounting))
    };
    if (profitToDateScorecard.Disabled.GetValueOrDefault())
      return profitToDateScorecard;
    IDictionary<string, object> dataDictionary = this.GetGenericInquiryDataDictionary("PM658100", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Project, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD)).FirstOrDefault<IDictionary<string, object>>();
    Decimal amount = 0M;
    if (dataDictionary != null)
      amount = dataDictionary.GetGenericResultValue<Decimal>("ProjectHistoryByDate_Formula4a71b92eae83444f9fdd6a2908cf1ed7");
    profitToDateScorecard.Value = ValueFormatter.FormatAmount(amount);
    profitToDateScorecard.Level = LevelHelper.NormalIfPositive(amount);
    return profitToDateScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenMarginToDate(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PM658100", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Project, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  protected virtual ScorecardState GetMarginToDateScorecard()
  {
    ScorecardState marginToDateScorecard = new ScorecardState()
    {
      Key = "MarginToDate",
      Name = "Margin to Date",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.ProjectAccounting))
    };
    if (marginToDateScorecard.Disabled.GetValueOrDefault())
      return marginToDateScorecard;
    IDictionary<string, object> dataDictionary = this.GetGenericInquiryDataDictionary("PM658100", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Project, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD)).FirstOrDefault<IDictionary<string, object>>();
    Decimal percent = 0M;
    if (dataDictionary != null)
      percent = dataDictionary.GetGenericResultValue<Decimal>("ProjectHistoryByDate_Formulafbf33e89568b49f5acfe9994febb3577");
    marginToDateScorecard.Value = ValueFormatter.FormatPercent(percent);
    marginToDateScorecard.Level = LevelHelper.NormalNonConditional();
    return marginToDateScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenProjectedMargin(PXAdapter adapter)
  {
    this.OpenCostProjectionByDateEntryOrGI();
    return adapter.Get();
  }

  protected virtual ScorecardState GetProjectedMarginScorecard()
  {
    ScorecardState projectedMarginScorecard = new ScorecardState()
    {
      Key = "ProjectedMargin",
      Name = "Projected Margin",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.ProjectAccounting))
    };
    if (projectedMarginScorecard.Disabled.GetValueOrDefault())
      return projectedMarginScorecard;
    Decimal valueOrDefault = ((Decimal?) this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>()?.ProjectedMarginPctTotal).GetValueOrDefault();
    projectedMarginScorecard.Value = ValueFormatter.FormatPercent(valueOrDefault);
    projectedMarginScorecard.Level = LevelHelper.NormalIfPositive(valueOrDefault);
    return projectedMarginScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenBudgetedRevenue(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PM658100", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Project, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  protected virtual ScorecardState GetBudgetedRevenueScorecard()
  {
    ScorecardState revenueScorecard = new ScorecardState()
    {
      Key = "BudgetedRevenue",
      Name = "Budgeted Revenue",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.ProjectAccounting))
    };
    if (revenueScorecard.Disabled.GetValueOrDefault())
      return revenueScorecard;
    IDictionary<string, object> dataDictionary = this.GetGenericInquiryDataDictionary("PM658100", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Project, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD)).FirstOrDefault<IDictionary<string, object>>();
    Decimal amount = 0M;
    if (dataDictionary != null)
      amount = dataDictionary.GetGenericResultValue<Decimal>("BudgetHistory_Formula4034c5f5942c4a0eade7e4c61d2983bf");
    revenueScorecard.Value = ValueFormatter.FormatAmount(amount);
    revenueScorecard.Level = LevelHelper.NormalNonConditional();
    return revenueScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenActualRevenueToDate(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PM658100", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Project, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  protected virtual ScorecardState GetActualRevenueToDateScorecard()
  {
    ScorecardState revenueToDateScorecard = new ScorecardState()
    {
      Key = "ActualRevenueToDate",
      Name = "Actual Revenue to Date",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.ProjectAccounting))
    };
    if (revenueToDateScorecard.Disabled.GetValueOrDefault())
      return revenueToDateScorecard;
    IDictionary<string, object> dataDictionary = this.GetGenericInquiryDataDictionary("PM658100", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Project, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD)).FirstOrDefault<IDictionary<string, object>>();
    Decimal amount = 0M;
    if (dataDictionary != null)
      amount = dataDictionary.GetGenericResultValue<Decimal>("ProjectHistoryByDate_Formulaa812d4fd79e84e9aa22b05e50ecefb8f");
    revenueToDateScorecard.Value = ValueFormatter.FormatAmount(amount);
    revenueToDateScorecard.Level = LevelHelper.NormalNonConditional();
    return revenueToDateScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenContractBilled(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PM658100", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Project, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  protected virtual ScorecardState GetContractBilledScorecard()
  {
    ScorecardState contractBilledScorecard = new ScorecardState()
    {
      Key = "ContractBilled",
      Name = "Contract Billed",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.ProjectAccounting))
    };
    if (contractBilledScorecard.Disabled.GetValueOrDefault())
      return contractBilledScorecard;
    IDictionary<string, object> dataDictionary = this.GetGenericInquiryDataDictionary("PM658100", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Project, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD)).FirstOrDefault<IDictionary<string, object>>();
    Decimal percent = 0M;
    if (dataDictionary != null)
    {
      Decimal genericResultValue1 = dataDictionary.GetGenericResultValue<Decimal>("ProjectHistoryByDate_Formulaa812d4fd79e84e9aa22b05e50ecefb8f");
      Decimal genericResultValue2 = dataDictionary.GetGenericResultValue<Decimal>("BudgetHistory_Formula4034c5f5942c4a0eade7e4c61d2983bf");
      if (genericResultValue2 != 0M)
        percent = Math.Round(genericResultValue1 / genericResultValue2 * 100M, 2);
    }
    contractBilledScorecard.Value = ValueFormatter.FormatPercent(percent);
    contractBilledScorecard.Level = LevelHelper.NormalNonConditional();
    return contractBilledScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenOverbillingUnderbilling(PXAdapter adapter)
  {
    this.OpenCostProjectionByDateEntryOrGI();
    return adapter.Get();
  }

  protected virtual ScorecardState GetOverbillingUnderbillingScorecard()
  {
    ScorecardState underbillingScorecard = new ScorecardState()
    {
      Key = "OverbillingUnderbilling",
      Name = "Overbilling or Underbilling",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.Construction) || !PXAccess.VerifyRights("PMGI3055"))
    };
    if (underbillingScorecard.Disabled.GetValueOrDefault())
      return underbillingScorecard;
    PMCostProjectionByDate projectionByDate = this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>();
    Decimal valueOrDefault = projectionByDate != null ? projectionByDate.CuryOverbillingAmountTotal.GetValueOrDefault() : 0M;
    underbillingScorecard.Value = ValueFormatter.FormatAmount(valueOrDefault);
    underbillingScorecard.Level = LevelHelper.NormalIfNonNegative(valueOrDefault);
    return underbillingScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenRevenueBudgetBacklog(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  protected virtual ScorecardState GetRevenueBudgetBacklogScorecard()
  {
    ScorecardState backlogScorecard = new ScorecardState()
    {
      Key = "RevenueBudgetBacklog",
      Name = "Revenue Budget Backlog",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.Construction) || !PXAccess.VerifyRights("PMGI3055"))
    };
    if (backlogScorecard.Disabled.GetValueOrDefault())
      return backlogScorecard;
    PMCostProjectionByDate projectionByDate = this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>();
    Decimal valueOrDefault = projectionByDate != null ? projectionByDate.CuryRevenueBudgetBacklogAmountTotal.GetValueOrDefault() : 0M;
    backlogScorecard.Value = ValueFormatter.FormatAmount(valueOrDefault);
    backlogScorecard.Level = LevelHelper.NormalNonConditional();
    return backlogScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenExpectedRevenue(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  protected virtual ScorecardState GetExpectedRevenueScorecard()
  {
    ScorecardState revenueScorecard = new ScorecardState()
    {
      Key = "ExpectedRevenue",
      Name = "Expected Current Revenue",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.Construction) || !PXAccess.VerifyRights("PMGI3055"))
    };
    if (revenueScorecard.Disabled.GetValueOrDefault())
      return revenueScorecard;
    PMCostProjectionByDate projectionByDate = this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>();
    Decimal valueOrDefault = projectionByDate != null ? projectionByDate.CuryRevenueExpectedAmountTotal.GetValueOrDefault() : 0M;
    revenueScorecard.Value = ValueFormatter.FormatAmount(valueOrDefault);
    revenueScorecard.Level = LevelHelper.NormalNonConditional();
    return revenueScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenBudgetedCost(PXAdapter adapter)
  {
    this.OpenCostProjectionByDateEntryOrGI();
    return adapter.Get();
  }

  protected virtual ScorecardState GetBudgetedCostScorecard()
  {
    ScorecardState budgetedCostScorecard = new ScorecardState()
    {
      Key = "BudgetedCost",
      Name = "Budgeted Cost",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.Construction) || !PXAccess.VerifyRights("PMGI3055"))
    };
    if (budgetedCostScorecard.Disabled.GetValueOrDefault())
      return budgetedCostScorecard;
    PMCostProjectionByDate projectionByDate = this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>();
    Decimal valueOrDefault = projectionByDate != null ? projectionByDate.CuryBudgetedAmountTotal.GetValueOrDefault() : 0M;
    budgetedCostScorecard.Value = ValueFormatter.FormatAmount(valueOrDefault);
    budgetedCostScorecard.Level = LevelHelper.NormalNonConditional();
    return budgetedCostScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenActualCostsToDate(PXAdapter adapter)
  {
    this.OpenCostProjectionByDateEntryOrGI();
    return adapter.Get();
  }

  protected virtual ScorecardState GetActualCostsToDateScorecard()
  {
    ScorecardState costsToDateScorecard = new ScorecardState()
    {
      Key = "ActualCostsToDate",
      Name = "Actual Cost to Date",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.Construction) || !PXAccess.VerifyRights("PMGI3055"))
    };
    if (costsToDateScorecard.Disabled.GetValueOrDefault())
      return costsToDateScorecard;
    PMCostProjectionByDate projectionByDate = this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>();
    Decimal valueOrDefault = projectionByDate != null ? projectionByDate.CuryActualAmountTotal.GetValueOrDefault() : 0M;
    costsToDateScorecard.Value = ValueFormatter.FormatAmount(valueOrDefault);
    costsToDateScorecard.Level = LevelHelper.NormalNonConditional();
    return costsToDateScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenAnticipatedCost(PXAdapter adapter)
  {
    this.OpenCostProjectionByDateEntryOrGI();
    return adapter.Get();
  }

  protected virtual ScorecardState GetAnticipatedCostScorecard()
  {
    ScorecardState anticipatedCostScorecard = new ScorecardState()
    {
      Key = "AnticipatedCost",
      Name = "Anticipated Cost",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.Construction) || !PXAccess.VerifyRights("PMGI3055"))
    };
    if (anticipatedCostScorecard.Disabled.GetValueOrDefault())
      return anticipatedCostScorecard;
    PMCostProjectionByDate projectionByDate = this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>();
    Decimal valueOrDefault1 = projectionByDate != null ? projectionByDate.CuryCompletedAmountTotal.GetValueOrDefault() : 0M;
    Decimal valueOrDefault2 = projectionByDate != null ? projectionByDate.CuryActualAmountTotal.GetValueOrDefault() : 0M;
    anticipatedCostScorecard.Value = ValueFormatter.FormatAmount(valueOrDefault1);
    anticipatedCostScorecard.Level = LevelHelper.NormalIfNonNegative(valueOrDefault2 - valueOrDefault1);
    return anticipatedCostScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenOpenCommitments(PXAdapter adapter)
  {
    this.OpenCostProjectionByDateEntryOrGI();
    return adapter.Get();
  }

  protected virtual ScorecardState GetOpenCommitmentsScorecard()
  {
    ScorecardState commitmentsScorecard = new ScorecardState()
    {
      Key = "OpenCommitments",
      Name = "Open Commitments",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.Construction) || !PXAccess.VerifyRights("PMGI3055"))
    };
    if (commitmentsScorecard.Disabled.GetValueOrDefault())
      return commitmentsScorecard;
    PMCostProjectionByDate projectionByDate = this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>();
    Decimal valueOrDefault = projectionByDate != null ? projectionByDate.CuryCommitmentOpenAmountTotal.GetValueOrDefault() : 0M;
    commitmentsScorecard.Value = ValueFormatter.FormatAmount(valueOrDefault);
    commitmentsScorecard.Level = LevelHelper.NormalNonConditional();
    return commitmentsScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenCostBudgetBacklog(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  protected virtual ScorecardState GetCostBudgetBacklogScorecard()
  {
    ScorecardState backlogScorecard = new ScorecardState()
    {
      Key = "CostBudgetBacklog",
      Name = "Cost Budget Backlog",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.Construction) || !PXAccess.VerifyRights("PMGI3055"))
    };
    if (backlogScorecard.Disabled.GetValueOrDefault())
      return backlogScorecard;
    PMCostProjectionByDate projectionByDate = this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>();
    Decimal valueOrDefault = projectionByDate != null ? projectionByDate.CuryBudgetBacklogAmountTotal.GetValueOrDefault() : 0M;
    backlogScorecard.Value = ValueFormatter.FormatAmount(valueOrDefault);
    backlogScorecard.Level = LevelHelper.NormalIfPositive(valueOrDefault);
    return backlogScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenPerformance(PXAdapter adapter)
  {
    this.OpenCostProjectionByDateEntryOrGI();
    return adapter.Get();
  }

  protected virtual ScorecardState GetPerformanceScorecard()
  {
    ScorecardState performanceScorecard = new ScorecardState()
    {
      Key = "Performance",
      Name = "Performance",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.Construction) || !PXAccess.VerifyRights("PMGI3055"))
    };
    if (performanceScorecard.Disabled.GetValueOrDefault())
      return performanceScorecard;
    Decimal valueOrDefault = ((Decimal?) this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>()?.PerformanceTotal).GetValueOrDefault();
    performanceScorecard.Value = ValueFormatter.FormatPercent(valueOrDefault);
    performanceScorecard.Level = LevelHelper.NormalIfLessThen(valueOrDefault, 100M);
    return performanceScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenRemainingBudget(PXAdapter adapter)
  {
    this.OpenCostProjectionByDateEntryOrGI();
    return adapter.Get();
  }

  protected virtual ScorecardState GetRemainingBudgetScorecard()
  {
    ScorecardState remainingBudgetScorecard = new ScorecardState()
    {
      Key = "RemainingBudget",
      Name = "Remaining Budget",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.Construction) || !PXAccess.VerifyRights("PMGI3055"))
    };
    if (remainingBudgetScorecard.Disabled.GetValueOrDefault())
      return remainingBudgetScorecard;
    PMCostProjectionByDate projectionByDate = this.GetGenericInquiryData<PMCostProjectionByDate>("PMGI3055", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.Document, PMCostProjectionByDate.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID)).FirstOrDefault<PMCostProjectionByDate>();
    Decimal num1 = 0M;
    if (projectionByDate != null)
    {
      Decimal? nullable = projectionByDate.CuryBudgetedAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      if (valueOrDefault1 != 0M)
      {
        Decimal num2 = valueOrDefault1;
        nullable = projectionByDate.CuryActualAmountTotal;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        num1 = Math.Round((num2 - valueOrDefault2) / valueOrDefault1 * 100M, 2);
      }
    }
    remainingBudgetScorecard.Value = ValueFormatter.FormatPercent(num1);
    remainingBudgetScorecard.Level = LevelHelper.NormalIfPositive(num1);
    return remainingBudgetScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenOutstandingAPBills(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("AP3011DB", DataViewHelper.DataViewFilter.Create<PMProject, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  protected virtual ScorecardState GetOutstandingAPBillsScorecard()
  {
    ScorecardState apBillsScorecard = new ScorecardState()
    {
      Key = "OutstandingAPBills",
      Name = "Unpaid AP Bills",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.ProjectAccounting))
    };
    if (apBillsScorecard.Disabled.GetValueOrDefault())
      return apBillsScorecard;
    int inquiryRecordsCount = this.GetGenericInquiryRecordsCount("AP3011DB", DataViewHelper.DataViewFilter.Create<PMProject, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    apBillsScorecard.Value = ValueFormatter.FormatAmount((Decimal) inquiryRecordsCount);
    apBillsScorecard.Level = LevelHelper.NormalIfZero((Decimal) inquiryRecordsCount);
    return apBillsScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenPotentialChangeOrders(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PM3080PL", DataViewHelper.DataViewFilter.Create<PMProject, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD), DataViewHelper.DataViewFilter.Create<PMChangeOrder, PMChangeOrder.status>((PXCondition) 0, (object[]) new string[3]
    {
      "H",
      "O",
      "A"
    }));
    return adapter.Get();
  }

  protected virtual ScorecardState GetPotentialChangeOrders()
  {
    ScorecardState potentialChangeOrders = new ScorecardState()
    {
      Key = "PotentialChangeOrders",
      Name = "Potential Change Orders",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.ChangeOrders))
    };
    if (potentialChangeOrders.Disabled.GetValueOrDefault())
      return potentialChangeOrders;
    int inquiryRecordsCount = this.GetGenericInquiryRecordsCount("PM3080PL", DataViewHelper.DataViewFilter.Create<PMProject, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD), DataViewHelper.DataViewFilter.Create<PMChangeOrder, PMChangeOrder.status>((PXCondition) 0, (object[]) new string[3]
    {
      "H",
      "O",
      "A"
    }));
    potentialChangeOrders.Value = ValueFormatter.FormatAmount((Decimal) inquiryRecordsCount);
    potentialChangeOrders.Level = LevelHelper.NormalIfZero((Decimal) inquiryRecordsCount);
    return potentialChangeOrders;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenOpenProjectIssues(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PJ3020PL", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.ProjectIssue, GenericInquiryAliases.ProjectIssue.ProjectId>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD), DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.ProjectIssue, GenericInquiryAliases.ProjectIssue.status>((PXCondition) 0, (object) "O"));
    return adapter.Get();
  }

  protected virtual ScorecardState GetOpenProjectIssuesScorecard()
  {
    ScorecardState projectIssuesScorecard = new ScorecardState()
    {
      Key = "OpenProjectIssues",
      Name = "Open Project Issues",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.ProjectManagement) || !PXAccess.VerifyRights("PJ3020PL"))
    };
    if (projectIssuesScorecard.Disabled.GetValueOrDefault())
      return projectIssuesScorecard;
    int inquiryRecordsCount = this.GetGenericInquiryRecordsCount("PJ3020PL", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.ProjectIssue, GenericInquiryAliases.ProjectIssue.ProjectId>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID), DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.ProjectIssue, GenericInquiryAliases.ProjectIssue.status>((PXCondition) 0, (object) "O"));
    projectIssuesScorecard.Value = ValueFormatter.FormatAmount((Decimal) inquiryRecordsCount);
    projectIssuesScorecard.Level = LevelHelper.NormalIfZero((Decimal) inquiryRecordsCount);
    return projectIssuesScorecard;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenOpenRFIs(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PJ3010PL", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.RFI, GenericInquiryAliases.RFI.ProjectId>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD), DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.RFI, GenericInquiryAliases.RFI.status>((PXCondition) 1, (object) "C"));
    return adapter.Get();
  }

  protected virtual ScorecardState GetOpenRFIsScorecard()
  {
    ScorecardState openRfIsScorecard = new ScorecardState()
    {
      Key = "OpenRFIs",
      Name = "Open RFIs",
      Disabled = new bool?(FeatureSetHelper.IsProjectFeatureDisabled(ProjectFeatureSet.ProjectManagement) || !PXAccess.VerifyRights("PJ3010PL"))
    };
    if (openRfIsScorecard.Disabled.GetValueOrDefault())
      return openRfIsScorecard;
    int inquiryRecordsCount = this.GetGenericInquiryRecordsCount("PJ3010PL", DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.RFI, GenericInquiryAliases.RFI.ProjectId>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractID), DataViewHelper.DataViewFilter.Create<GenericInquiryAliases.RFI, GenericInquiryAliases.RFI.status>((PXCondition) 1, (object) "C"));
    openRfIsScorecard.Value = ValueFormatter.FormatAmount((Decimal) inquiryRecordsCount);
    openRfIsScorecard.Level = LevelHelper.NormalIfZero((Decimal) inquiryRecordsCount);
    return openRfIsScorecard;
  }

  public virtual IEnumerable budgetVsActualChart()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = true,
      IsResultSorted = true
    };
    int num = 0;
    foreach (PXResult<PMCostProjectionByDate> pxResult in ((PXSelectBase<PMCostProjectionByDate>) this.ReleasedCostProjections).Select(Array.Empty<object>()))
    {
      PMCostProjectionByDate projectionByDate = PXResult<PMCostProjectionByDate>.op_Implicit(pxResult);
      string str = $"{projectionByDate.ProjectionDate:d}";
      ++num;
      ((List<object>) pxDelegateResult).Add((object) new ChartPoint()
      {
        GraphKey = "Budget",
        PointName = str,
        PointIndex = new int?(num),
        PointValue = projectionByDate.CuryBudgetedAmountTotal
      });
      ((List<object>) pxDelegateResult).Add((object) new ChartPoint()
      {
        GraphKey = "Actual",
        PointName = str,
        PointIndex = new int?(num),
        PointValue = projectionByDate.CuryActualAmountTotal
      });
    }
    return (IEnumerable) pxDelegateResult;
  }

  [PXUIField]
  [PXButton(Category = "Project WIP")]
  public virtual IEnumerable OpenCostProjection(PXAdapter adapter)
  {
    this.OpenCostProjectionByDateEntryOrGI();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Project WIP")]
  public virtual IEnumerable OpenWipAdjustments(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToScreen((PXGraph) PXGraph.CreateInstance<ProjectWipAdjustmentEntry>());
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Project WIP")]
  public virtual IEnumerable OpenPM652500(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("PM652500", new Dictionary<string, string>()
    {
      ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Project Reports")]
  public virtual IEnumerable OpenProjectTransactionDetails(PXAdapter adapter)
  {
    TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
    ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ProjectID;
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Project Reports")]
  public virtual IEnumerable OpenProjectFinancialVision(PXAdapter adapter)
  {
    ProjectDateSensitiveCostsInquiry instance = PXGraph.CreateInstance<ProjectDateSensitiveCostsInquiry>();
    ((PXSelectBase<PMDateSensitiveDataRevision>) instance.Revision).Current.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ProjectID;
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Project Reports")]
  public virtual IEnumerable OpenSC644000(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("SC644000", new Dictionary<string, string>()
    {
      ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Project Invoices")]
  public virtual IEnumerable OpenPM650000(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("PM650000", new Dictionary<string, string>()
    {
      ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Project Invoices")]
  public virtual IEnumerable OpenPM650050(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("PM650050", new Dictionary<string, string>()
    {
      ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Financial Reports")]
  public virtual IEnumerable OpenAR634100(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("AR634100", new Dictionary<string, string>()
    {
      ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Financial Reports")]
  public virtual IEnumerable OpenAP634100(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("AP634100", new Dictionary<string, string>()
    {
      ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Financial Reports")]
  public virtual IEnumerable OpenAR631200(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("AR631200", new Dictionary<string, string>()
    {
      ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Financial Reports")]
  public virtual IEnumerable OpenAP631200(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("AP631200", new Dictionary<string, string>()
    {
      ["ProjectCD"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Financial Reports")]
  public virtual IEnumerable OpenAR630600(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("AR630600", new Dictionary<string, string>()
    {
      ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Financial Reports")]
  public virtual IEnumerable OpenAP630600(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("AP630600", new Dictionary<string, string>()
    {
      ["ProjectCD"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Financial Reports")]
  public virtual IEnumerable OpenPM650500(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("PM650500", new Dictionary<string, string>()
    {
      ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Time Reports")]
  public virtual IEnumerable OpenPM622000(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("PM622000", new Dictionary<string, string>()
    {
      ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenProjectEntry(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToProjectScreen((int?) ((PXSelectBase<PMProject>) this.Project).Current?.ContractID, "Project", (PXBaseRedirectException.WindowMode) 1);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenARInvoices(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("AR3010PL", DataViewHelper.DataViewFilter.Create<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenAPInvoices(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("AP3010PL", DataViewHelper.DataViewFilter.Create<PX.Objects.AP.APInvoice, PX.Objects.AP.APInvoice.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenApprovals(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToScreen((PXGraph) PXGraph.CreateInstance<EPApprovalProcess>());
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenArInvoice(PXAdapter adapter)
  {
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.ArInvoices).Current;
    if (current == null)
      return adapter.Get();
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = (PX.Objects.AR.ARInvoice) PrimaryKeyOf<PX.Objects.AR.ARInvoice>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>>.Find((PXGraph) instance, (TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>) current, (PKFindOptions) 0);
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenApprovalEntity(PXAdapter adapter)
  {
    EPApprovalProcess.OnEditDetail((PXGraph) this, ((PXSelectBase<EPApprovalProcess.EPOwned>) this.Approvals).Current, (PXRedirectHelper.WindowMode) 1);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenProjectTasks(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PM3020PL", DataViewHelper.DataViewFilter.Create<PMProject, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenProFormaInvoices(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PM3070PL", DataViewHelper.DataViewFilter.Create<PMProject, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public IEnumerable CreateProjectTask(PXAdapter adapter)
  {
    ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
    ((PXSelectBase<PMTask>) instance.Task).Insert();
    ((PXSelectBase<PMTask>) instance.Task).SetValueExt<PMTask.projectID>(((PXSelectBase<PMTask>) instance.Task).Current, (object) (int?) ((PXSelectBase<PMProject>) this.Project).Current?.ProjectID);
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMProject, PMProject.siteAddressID> e)
  {
    PMSiteAddress pmSiteAddress = PXResultset<PMSiteAddress>.op_Implicit(PXSelectBase<PMSiteAddress, PXViewOf<PMSiteAddress>.BasedOn<SelectFromBase<PMSiteAddress, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMSiteAddress.addressID, IBqlInt>.IsEqual<BqlField<PMProject.siteAddressID, IBqlInt>.FromCurrent>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    string str1;
    if (string.IsNullOrWhiteSpace(pmSiteAddress?.AddressLine1))
      str1 = "";
    else
      str1 = $"{pmSiteAddress.AddressLine1}, {pmSiteAddress.City}, {pmSiteAddress.State}, {pmSiteAddress.PostalCode}, {pmSiteAddress.CountryID}";
    string str2 = str1;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMProject, PMProject.siteAddressID>>) e).ReturnState = (object) PXFieldState.CreateInstance((object) str2, typeof (string), new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProject> e)
  {
    bool flag1 = this.IsNewProject();
    ((PXAction) this.openProjectEntry).SetEnabled(!flag1);
    PXAction<PMProject> createProjectTask = this.createProjectTask;
    int num1;
    if (!flag1)
      num1 = !this.ProjectHasOneOfTheStatuses("F", "C", "R") ? 1 : 0;
    else
      num1 = 0;
    ((PXAction) createProjectTask).SetEnabled(num1 != 0);
    bool flag2 = this.CostCommitmentTrackingEnabled();
    bool? nullable;
    int num2;
    if (PXAccess.FeatureInstalled<FeaturesSet.changeOrder>())
    {
      PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
      if (current == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = current.ChangeOrderWorkflow;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num2 = 0;
    bool flag3 = num2 != 0;
    bool flag4 = PXAccess.FeatureInstalled<FeaturesSet.paymentsByLines>();
    foreach (MemberInfo hiddenTaskField in this.GetHiddenTaskFields())
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Tasks).Cache, hiddenTaskField.Name, false);
    PXUIFieldAttribute.SetVisible<PMTask.defaultCostCodeID>(((PXSelectBase) this.Tasks).Cache, (object) null, CostCodeAttribute.UseCostCode());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<PMCostBudget.committedQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag2 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedOrigAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<PMBudget.committedOrigQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag2 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedCOAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 & flag2);
    PXUIFieldAttribute.SetVisible<PMBudget.committedCOQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 & flag2 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedInvoicedAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<PMCostBudget.committedInvoicedQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag2 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedOpenAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<PMCostBudget.committedOpenQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag2 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.committedReceivedQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag2 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.curyActualPlusOpenCommittedAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<PMCostBudget.curyVarianceAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag2);
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
    PXUIFieldAttribute.SetVisible<PMCostBudget.draftChangeOrderQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.curyDraftChangeOrderAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMCostBudget.changeOrderQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3 && this.CostQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMCostBudget.curyChangeOrderAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMBudget.curyLastCostAtCompletion>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCostAtCompletion>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyLastCostToComplete>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.curyCostToComplete>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.lastPercentCompleted>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
    PXUIFieldAttribute.SetVisible<PMBudget.percentCompleted>(((PXSelectBase) this.CostBudget).Cache, (object) null, this.ProductivityVisible());
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
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.draftChangeOrderQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag3 && this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyDraftChangeOrderAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.changeOrderQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag3 && this.RevenueQuantityVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyChangeOrderAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag3);
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
    PXCache cache = ((PXSelectBase) this.RevenueBudget).Cache;
    int num3;
    if (e.Row.RetainageMode != "C")
    {
      nullable = e.Row.SteppedRetainage;
      num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.retainagePct>(cache, (object) null, num3 != 0);
    PXUIFieldAttribute.SetVisible<PMBudget.curyDraftRetainedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisible<PMBudget.curyRetainedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisible<PMBudget.curyTotalRetainedAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisible<PMBudget.retainageMaxPct>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, e.Row.RetainageMode == "L");
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyCapAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, e.Row.RetainageMode == "L");
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMTask> e)
  {
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTask>>) e).Cache.AllowInsert = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTask>>) e).Cache.AllowUpdate = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTask>>) e).Cache.AllowDelete = false;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMCostBudget> e)
  {
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostBudget>>) e).Cache.AllowInsert = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostBudget>>) e).Cache.AllowUpdate = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostBudget>>) e).Cache.AllowDelete = false;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMRevenueBudget> e)
  {
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache.AllowInsert = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache.AllowUpdate = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache.AllowDelete = false;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMBillingRecord> e)
  {
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMBillingRecord>>) e).Cache.AllowInsert = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMBillingRecord>>) e).Cache.AllowUpdate = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMBillingRecord>>) e).Cache.AllowDelete = false;
  }

  public IEnumerable<T> GetGenericInquiryData<T>(
    string giScreenID,
    params DataViewHelper.DataViewFilter[] filters)
    where T : class, IBqlTable, new()
  {
    return this.GetGenericInquiryDataDictionary(giScreenID, filters).Select<IDictionary<string, object>, T>((Func<IDictionary<string, object>, T>) (x => x.GetValue<T>()));
  }

  public IEnumerable<IDictionary<string, object>> GetGenericInquiryDataDictionary(
    string giScreenID,
    params DataViewHelper.DataViewFilter[] filters)
  {
    return this.GetGenericInquiryCachedDataDictionary(giScreenID, ProjectOverview.ToFilterRows(filters));
  }

  private static PXFilterRow[] ToFilterRows(DataViewHelper.DataViewFilter[] filters)
  {
    return ((IEnumerable<DataViewHelper.DataViewFilter>) filters).SelectMany<DataViewHelper.DataViewFilter, PXFilterRow>((Func<DataViewHelper.DataViewFilter, IEnumerable<PXFilterRow>>) (x => (IEnumerable<PXFilterRow>) x.ToFilterArray())).ToArray<PXFilterRow>();
  }

  private IEnumerable<IDictionary<string, object>> GetGenericInquiryCachedDataDictionary(
    string giScreenID,
    PXFilterRow[] filters)
  {
    IEnumerable<IDictionary<string, object>> dictionaries;
    return this._queryGiCache.TryGetValue($"{giScreenID}: {this.GetFiltersKey(filters)}", out dictionaries) ? dictionaries : this.GetGenericInquiryDataDictionary(giScreenID, filters);
  }

  private IEnumerable<IDictionary<string, object>> GetGenericInquiryDataDictionary(
    string giScreenID,
    PXFilterRow[] filters)
  {
    PXGenericInqGrph instance;
    if (!this._giCache.TryGetValue(giScreenID, out instance))
      this._giCache.Add(giScreenID, instance = PXGenericInqGrph.CreateInstance(giScreenID));
    foreach (GenericResult genericResult in this.GetGenericInquiryData(instance, filters))
      yield return genericResult.Values;
  }

  public int GetGenericInquiryRecordsCount(
    string giScreenID,
    params DataViewHelper.DataViewFilter[] filters)
  {
    return this.GetGenericInquiryRecordsCount(giScreenID, ProjectOverview.ToFilterRows(filters));
  }

  private int GetGenericInquiryRecordsCount(string giScreenID, PXFilterRow[] filters)
  {
    PXGenericInqGrph instance;
    if (!this._giCache.TryGetValue(giScreenID, out instance))
      this._giCache.Add(giScreenID, instance = PXGenericInqGrph.CreateInstance(giScreenID));
    return this.GetGenericInquiryData(instance, filters).Count<object>();
  }

  private IEnumerable<object> GetGenericInquiryData(PXGenericInqGrph gi, PXFilterRow[] filters)
  {
    int num1 = 0;
    int num2 = 0;
    return (IEnumerable<object>) ((PXSelectBase) gi.Results).View.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, filters == null || filters.Length == 0 ? (PXFilterRow[]) null : filters, ref num1, PXView.MaximumRows, ref num2);
  }

  private string GetFiltersKey(PXFilterRow[] filters)
  {
    return filters != null ? string.Join(";", ((IEnumerable<PXFilterRow>) filters).Select<PXFilterRow, string>(new Func<PXFilterRow, string>(this.GetFilterKey))) : string.Empty;
  }

  private string GetFilterKey(PXFilterRow filter)
  {
    return $"[{filter.DataField}]IF({filter.Condition})[{filter.Value}|{filter.Value2}]";
  }

  public class ProjectWipAdjustmentEntry_ActivityDetailsExt : 
    ActivityDetailsExt<ProjectOverview, PMProject, PMProject.noteID>
  {
    private bool _getAssistantContact;
    public PXAction<PMProject> NewAssistantMailActivity;

    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

    public override string GetDefaultMailTo()
    {
      int? contactID = (int?) ((PXSelectBase<PMProject>) this.Base.Project).Current?.OwnerID;
      if (this._getAssistantContact)
        contactID = (int?) PX.Objects.CR.BAccount.PK.Find((PXGraph) this.Base, ((PXSelectBase<PMProject>) this.Base.Project).Current.AssistantID, (PKFindOptions) 1)?.DefContactID;
      return PX.Objects.CR.Contact.PK.Find((PXGraph) this.Base, contactID, (PKFindOptions) 1)?.Address;
    }

    public override void CreateTimeActivity(PXGraph targetGraph, int classID, string activityType)
    {
      base.CreateTimeActivity(targetGraph, classID, activityType);
      ProjectEntry.ProjectEntry_ActivityDetailsExt.CreateProjectTimeActivity((PXGraph) this.Base, targetGraph, classID);
    }

    [PXUIField(DisplayName = "Create Email")]
    [PXButton(DisplayOnMainToolbar = false, PopupCommand = "RefreshActivities")]
    public virtual IEnumerable newAssistantMailActivity(PXAdapter adapter)
    {
      this._getAssistantContact = true;
      this.CreateNewActivityAndRedirect(4, (string) null);
      return adapter.Get();
    }
  }
}
