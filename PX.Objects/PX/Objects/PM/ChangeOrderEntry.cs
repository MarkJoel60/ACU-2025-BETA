// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeOrderEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AP;
using PX.Objects.CN.Subcontracts.PO.CacheExtensions;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CR;
using PX.Objects.CR.Descriptor.Exceptions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.Services;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.PM;

public class ChangeOrderEntry : 
  PXGraph<
  #nullable disable
  ChangeOrderEntry, PMChangeOrder>,
  PXImportAttribute.IPXPrepareItems,
  IGraphWithInitialization
{
  public const string ChangeOrderReport = "PM643000";
  public const string ChangeOrderNotificationCD = "CHANGE ORDER";
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PMChangeOrder.projectNbr)})]
  [PXViewName("Change Order")]
  public FbqlSelect<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProject>.On<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  PMChangeOrder.projectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.contractID, 
  #nullable disable
  IsNull>>>>.Or<MatchUserFor<PMProject>>>, PMChangeOrder>.View Document;
  public PXSelect<PMChangeOrder, Where<PMChangeOrder.refNbr, Equal<Current<PMChangeOrder.refNbr>>>> DocumentSettings;
  public PXSelect<PMChangeOrder, Where<PMChangeOrder.refNbr, Equal<Current<PMChangeOrder.refNbr>>>> VisibilitySettings;
  [PXCopyPasteHiddenView]
  [PXViewName("Project")]
  public PXSetup<PMProject>.Where<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMChangeOrder.projectID, IBqlInt>.FromCurrent>> Project;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public 
  #nullable disable
  PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMChangeOrder.projectID>>>> ProjectProperties;
  [PXCopyPasteHiddenView]
  [PXViewName("Customer")]
  public PXSetup<PX.Objects.AR.Customer>.Where<BqlOperand<
  #nullable enable
  PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMChangeOrder.customerID, IBqlInt>.AsOptional>> Customer;
  [PXImport(typeof (PMChangeOrder))]
  [PXFilterable(new System.Type[] {})]
  public 
  #nullable disable
  PXSelectJoin<PMChangeOrderCostBudget, LeftJoin<PMBudget, On<PMBudget.projectID, Equal<PMChangeOrderCostBudget.projectID>, And<PMBudget.projectTaskID, Equal<PMChangeOrderCostBudget.projectTaskID>, And<PMBudget.accountGroupID, Equal<PMChangeOrderCostBudget.accountGroupID>, And<PMBudget.inventoryID, Equal<PMChangeOrderCostBudget.inventoryID>, And<PMBudget.costCodeID, Equal<PMChangeOrderCostBudget.costCodeID>>>>>>>, Where<PMChangeOrderCostBudget.refNbr, Equal<Current<PMChangeOrder.refNbr>>, And<PMChangeOrderCostBudget.type, Equal<AccountType.expense>>>> CostBudget;
  [PXImport(typeof (PMChangeOrder))]
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<PMChangeOrderRevenueBudget, LeftJoin<PMBudget, On<PMBudget.projectID, Equal<PMChangeOrderRevenueBudget.projectID>, And<PMBudget.projectTaskID, Equal<PMChangeOrderRevenueBudget.projectTaskID>, And<PMBudget.accountGroupID, Equal<PMChangeOrderRevenueBudget.accountGroupID>, And<PMBudget.inventoryID, Equal<PMChangeOrderRevenueBudget.inventoryID>, And<PMBudget.costCodeID, Equal<PMChangeOrderRevenueBudget.costCodeID>>>>>>>, Where<PMChangeOrderRevenueBudget.refNbr, Equal<Current<PMChangeOrder.refNbr>>, And<PMChangeOrderRevenueBudget.type, Equal<AccountType.income>>>> RevenueBudget;
  [PXCopyPasteHiddenView]
  public PXSelect<PMCostBudget> AvailableCostBudget;
  [PXCopyPasteHiddenView]
  public PXSelect<PMRevenueBudget> AvailableRevenueBudget;
  [PXImport(typeof (PMChangeOrder))]
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<PMChangeOrderLine, LeftJoin<POLinePM, On<POLinePM.orderType, Equal<PMChangeOrderLine.pOOrderType>, And<POLinePM.orderNbr, Equal<PMChangeOrderLine.pOOrderNbr>, And<POLinePM.lineNbr, Equal<PMChangeOrderLine.pOLineNbr>>>>>, Where<PMChangeOrderLine.refNbr, Equal<Current<PMChangeOrder.refNbr>>>> Details;
  protected Dictionary<ChangeOrderEntry.POLineKey, POLinePM> polines;
  public PXFilter<ChangeOrderEntry.POLineFilter> AvailablePOLineFilter;
  [PXCopyPasteHiddenView]
  public PXSelect<POLinePM> AvailablePOLines;
  [PXViewName("Change Order Class")]
  public PXSetup<PMChangeOrderClass>.Where<BqlOperand<
  #nullable enable
  PMChangeOrderClass.classID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMChangeOrder.classID, IBqlString>.AsOptional>> ChangeOrderClass;
  [PXViewName("Answers")]
  public 
  #nullable disable
  CRAttributeList<PMChangeOrder> Answers;
  [PXCopyPasteHiddenView]
  [PXViewName("Approval")]
  public EPApprovalAutomation<PMChangeOrder, PMChangeOrder.approved, PMChangeOrder.rejected, PMChangeOrder.hold, PMSetupChangeOrderApproval> Approval;
  public PXSetup<PMSetup> Setup;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<APSetup> apSetup;
  public PXSetup<POSetup> poSetup;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PMBudgetAccum> Budget;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<PMProjectBudgetHistoryAccum, TypeArrayOf<IFbqlJoin>.Empty>, PMProjectBudgetHistoryAccum>.View ProjectBudgetHistory;
  [PXHidden]
  public PXSelect<PMForecastHistoryAccum> ForecastHistory;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.PO.POOrder> Order;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.PO.POLine> dummyPOLine;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.AP.Vendor> dummyVendor;
  private IFinPeriodRepository finPeriodsRepo;
  protected ProjectBalance balanceCalculator;
  public PXAction<PMChangeOrder> release;
  public PXAction<PMChangeOrder> reverse;
  public PXAction<PMChangeOrder> approve;
  public PXAction<PMChangeOrder> reject;
  public PXAction<PMChangeOrder> coReport;
  public PXAction<PMChangeOrder> send;
  public PXAction<PMChangeOrder> addCostBudget;
  public PXAction<PMChangeOrder> appendSelectedCostBudget;
  public PXAction<PMChangeOrder> addRevenueBudget;
  public PXAction<PMChangeOrder> appendSelectedRevenueBudget;
  public PXAction<PMChangeOrder> addPOLines;
  public PXAction<PMChangeOrder> appendSelectedPOLines;
  public PXAction<PMChangeOrder> viewCommitments;
  public PXAction<PMChangeOrder> viewChangeOrder;
  public PXAction<PMChangeOrder> viewRevenueBudgetTask;
  public PXAction<PMChangeOrder> viewCostBudgetTask;
  public PXAction<PMChangeOrder> viewCommitmentTask;
  public PXAction<PMChangeOrder> viewRevenueBudgetInventory;
  public PXAction<PMChangeOrder> viewCostBudgetInventory;
  public PXAction<PMChangeOrder> viewCommitmentInventory;
  public PXAction<PMChangeOrder> hold;
  public PXAction<PMChangeOrder> removeHold;
  public PXAction<PMChangeOrder> coCancel;
  protected string draftChangeOrderBudgetStatsKey;
  protected Dictionary<BudgetKeyTuple, Decimal> draftChangeOrderBudgetStats;
  protected Dictionary<BudgetKeyTuple, PMCostBudget> costBudgets;
  protected Dictionary<BudgetKeyTuple, PMChangeOrderBudget> previousTotals;
  protected Dictionary<BudgetKeyTuple, PMRevenueBudget> revenueBudgets;
  private readonly string[] LineTypesToValidate = new string[2]
  {
    "L",
    "D"
  };
  private string LastRefNbr = string.Empty;

  [PXDBDate]
  [PXDefault(typeof (PMChangeOrder.date))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.docDate> e)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (PMChangeOrder.customerID))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.bAccountID> e)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault(typeof (PMChangeOrder.description))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.descr> e)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (PMChangeOrder.revenueTotal))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.curyTotalAmount> e)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (PMChangeOrder.revenueTotal))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.totalAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Previously Approved CO Quantity", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderRevenueBudget.previouslyApprovedQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Previously Approved CO Amount", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderRevenueBudget.previouslyApprovedAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Current Committed CO Quantity", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderRevenueBudget.committedCOQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Current Committed CO Amount", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderRevenueBudget.committedCOAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Other Draft CO Amount", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderRevenueBudget.otherDraftRevisedAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Total Potentially Revised Amount", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderRevenueBudget.totalPotentialRevisedAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.cost>>), "Task Type is not valid", new System.Type[] {})]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMChangeOrderRevenueBudget.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderRevenueBudget.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Previously Approved CO Quantity", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderCostBudget.previouslyApprovedQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Previously Approved CO Amount", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderCostBudget.previouslyApprovedAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Current Committed CO Quantity", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderCostBudget.committedCOQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Current Committed CO Amount", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderCostBudget.committedCOAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Other Draft CO Amount", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderCostBudget.otherDraftRevisedAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Total Potentially Revised Amount", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderCostBudget.totalPotentialRevisedAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>), "Task Type is not valid", new System.Type[] {})]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMChangeOrderCostBudget.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.revenue>>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderCostBudget.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Potentially Revised Quantity", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderLine.potentialRevisedQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Potentially Revised Amount", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderLine.potentialRevisedAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>), "Task Type is not valid", new System.Type[] {})]
  [PXFormula(typeof (Validate<PMChangeOrderLine.costCodeID, PMChangeOrderLine.inventoryID, PMChangeOrderLine.description, PMChangeOrderLine.vendorID>))]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMChangeOrderLine.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.revenue>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMChangeOrderLine.taskID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField(DisplayName = "Order Nbr.")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.orderNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.lineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<PMChangeOrder.classID, PMChangeOrderClass.isRevenueBudgetEnabled>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrder.isRevenueVisible> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<PMChangeOrder.classID, PMChangeOrderClass.isCostBudgetEnabled>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrder.isCostVisible> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<Current<PMSetup.costCommitmentTracking>, Equal<True>>, Selector<PMChangeOrder.classID, PMChangeOrderClass.isPurchaseOrderEnabled>>, False>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrder.isDetailsVisible> e)
  {
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  [InjectDependency]
  public IUnitRateService RateService { get; set; }

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  public virtual IEnumerable costBudget()
  {
    List<PXResult<PMChangeOrderCostBudget, PMBudget>> pxResultList = new List<PXResult<PMChangeOrderCostBudget, PMBudget>>();
    foreach (PXResult<PMChangeOrderCostBudget> pxResult in ((PXSelectBase<PMChangeOrderCostBudget>) new PXSelect<PMChangeOrderCostBudget, Where<PMChangeOrderCostBudget.refNbr, Equal<Current<PMChangeOrder.refNbr>>, And<PMChangeOrderCostBudget.type, Equal<AccountType.expense>>>>((PXGraph) this)).Select(Array.Empty<object>()))
    {
      PMChangeOrderCostBudget changeOrderCostBudget = PXResult<PMChangeOrderCostBudget>.op_Implicit(pxResult);
      PMBudget pmBudget = (this.IsValidKey((PMChangeOrderBudget) changeOrderCostBudget) ? (PMBudget) this.GetOriginalCostBudget(BudgetKeyTuple.Create((IProjectFilter) changeOrderCostBudget)) : (PMBudget) null) ?? new PMBudget();
      pxResultList.Add(new PXResult<PMChangeOrderCostBudget, PMBudget>(changeOrderCostBudget, pmBudget));
    }
    return (IEnumerable) pxResultList;
  }

  public virtual IEnumerable revenueBudget()
  {
    List<PXResult<PMChangeOrderRevenueBudget, PMBudget>> pxResultList = new List<PXResult<PMChangeOrderRevenueBudget, PMBudget>>();
    foreach (PXResult<PMChangeOrderRevenueBudget> pxResult in ((PXSelectBase<PMChangeOrderRevenueBudget>) new PXSelect<PMChangeOrderRevenueBudget, Where<PMChangeOrderRevenueBudget.refNbr, Equal<Current<PMChangeOrder.refNbr>>, And<PMChangeOrderRevenueBudget.type, Equal<AccountType.income>>>>((PXGraph) this)).Select(Array.Empty<object>()))
    {
      PMChangeOrderRevenueBudget orderRevenueBudget = PXResult<PMChangeOrderRevenueBudget>.op_Implicit(pxResult);
      PMBudget pmBudget = (this.IsValidKey((PMChangeOrderBudget) orderRevenueBudget) ? (PMBudget) this.GetOriginalRevenueBudget(BudgetKeyTuple.Create((IProjectFilter) orderRevenueBudget)) : (PMBudget) null) ?? new PMBudget();
      pxResultList.Add(new PXResult<PMChangeOrderRevenueBudget, PMBudget>(orderRevenueBudget, pmBudget));
    }
    return (IEnumerable) pxResultList;
  }

  public virtual IEnumerable availableCostBudget()
  {
    HashSet<BudgetKeyTuple> existing = new HashSet<BudgetKeyTuple>();
    foreach (PXResult<PMChangeOrderCostBudget, PMBudget> pxResult in this.costBudget())
      existing.Add(BudgetKeyTuple.Create((IProjectFilter) PXResult<PMChangeOrderCostBudget, PMBudget>.op_Implicit(pxResult)));
    foreach (PMBudget budget in (IEnumerable<PMCostBudget>) this.GetCostBudget())
    {
      if (!(budget.Type != "E"))
      {
        if (existing.Contains(BudgetKeyTuple.Create((IProjectFilter) budget)))
          budget.Selected = new bool?(true);
        yield return (object) budget;
      }
    }
  }

  public virtual IEnumerable availableRevenueBudget()
  {
    HashSet<BudgetKeyTuple> existing = new HashSet<BudgetKeyTuple>();
    foreach (PXResult<PMChangeOrderRevenueBudget, PMBudget> pxResult in this.revenueBudget())
      existing.Add(BudgetKeyTuple.Create((IProjectFilter) PXResult<PMChangeOrderRevenueBudget, PMBudget>.op_Implicit(pxResult)));
    foreach (PMBudget budget in (IEnumerable<PMRevenueBudget>) this.GetRevenueBudget())
    {
      if (!(budget.Type != "I"))
      {
        if (existing.Contains(BudgetKeyTuple.Create((IProjectFilter) budget)))
          budget.Selected = new bool?(true);
        yield return (object) budget;
      }
    }
  }

  public IEnumerable details()
  {
    List<PXResult<PMChangeOrderLine, POLinePM>> pxResultList = new List<PXResult<PMChangeOrderLine, POLinePM>>(200);
    PXSelectJoin<PMChangeOrderLine, LeftJoin<POLinePM, On<POLinePM.orderType, Equal<PMChangeOrderLine.pOOrderType>, And<POLinePM.orderNbr, Equal<PMChangeOrderLine.pOOrderNbr>, And<POLinePM.lineNbr, Equal<PMChangeOrderLine.pOLineNbr>>>>>, Where<PMChangeOrderLine.refNbr, Equal<Current<PMChangeOrder.refNbr>>>> pxSelectJoin = new PXSelectJoin<PMChangeOrderLine, LeftJoin<POLinePM, On<POLinePM.orderType, Equal<PMChangeOrderLine.pOOrderType>, And<POLinePM.orderNbr, Equal<PMChangeOrderLine.pOOrderNbr>, And<POLinePM.lineNbr, Equal<PMChangeOrderLine.pOLineNbr>>>>>, Where<PMChangeOrderLine.refNbr, Equal<Current<PMChangeOrder.refNbr>>>>((PXGraph) this);
    int startRow = PXView.StartRow;
    int num = 0;
    if (this.polines == null || this.IsCacheUpdateRequired())
      this.polines = new Dictionary<ChangeOrderEntry.POLineKey, POLinePM>();
    foreach (PXResult<PMChangeOrderLine, POLinePM> pxResult in ((PXSelectBase) pxSelectJoin).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
    {
      PMChangeOrderLine pmChangeOrderLine = PXResult<PMChangeOrderLine, POLinePM>.op_Implicit(pxResult);
      POLinePM record = PXResult<PMChangeOrderLine, POLinePM>.op_Implicit(pxResult);
      int? lineNbr;
      if (this.IsValidKey(pmChangeOrderLine))
      {
        lineNbr = record.LineNbr;
        if (!lineNbr.HasValue)
          record = this.GetPOLine(pmChangeOrderLine) ?? PXResult<PMChangeOrderLine, POLinePM>.op_Implicit(pxResult);
      }
      lineNbr = record.LineNbr;
      if (lineNbr.HasValue)
      {
        ChangeOrderEntry.POLineKey key = this.GetKey(record);
        if (!this.polines.ContainsKey(key))
          this.polines.Add(key, record);
      }
      pxResultList.Add(new PXResult<PMChangeOrderLine, POLinePM>(pmChangeOrderLine, record));
    }
    PXView.StartRow = 0;
    return (IEnumerable) pxResultList;
  }

  public IEnumerable availablePOLines()
  {
    List<POLinePM> poLinePmList = new List<POLinePM>(200);
    FbqlSelect<SelectFromBase<POLinePM, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.vendorID>, IsNull>>>>.Or<BqlOperand<POLinePM.vendorID, IBqlInt>.IsEqual<BqlField<ChangeOrderEntry.POLineFilter.vendorID, IBqlInt>.FromCurrent>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.pOOrderNbr>, IsNull>>>>.Or<BqlOperand<POLinePM.orderNbr, IBqlString>.IsEqual<BqlField<ChangeOrderEntry.POLineFilter.pOOrderNbr, IBqlString>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.projectTaskID>, IsNull>>>>.Or<BqlOperand<POLinePM.taskID, IBqlInt>.IsEqual<BqlField<ChangeOrderEntry.POLineFilter.projectTaskID, IBqlInt>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.inventoryID>, IsNull>>>>.Or<BqlOperand<POLinePM.inventoryID, IBqlInt>.IsEqual<BqlField<ChangeOrderEntry.POLineFilter.inventoryID, IBqlInt>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.costCodeFrom>, IsNull>>>>.Or<BqlOperand<POLinePM.costCodeCD, IBqlString>.IsGreaterEqual<BqlField<ChangeOrderEntry.POLineFilter.costCodeFrom, IBqlString>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.costCodeTo>, IsNull>>>>.Or<BqlOperand<POLinePM.costCodeCD, IBqlString>.IsLessEqual<BqlField<ChangeOrderEntry.POLineFilter.costCodeTo, IBqlString>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLinePM.hasMultipleProjects, Equal<True>>>>>.Or<BqlOperand<POLinePM.projectID, IBqlInt>.IsEqual<BqlField<PMChangeOrder.projectID, IBqlInt>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLinePM.cancelled, NotEqual<True>>>>>.Or<BqlOperand<Current<ChangeOrderEntry.POLineFilter.includeNonOpen>, IBqlBool>.IsEqual<True>>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLinePM.completed, NotEqual<True>>>>>.Or<BqlOperand<Current<ChangeOrderEntry.POLineFilter.includeNonOpen>, IBqlBool>.IsEqual<True>>>>>, POLinePM>.View view = new FbqlSelect<SelectFromBase<POLinePM, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.vendorID>, IsNull>>>>.Or<BqlOperand<POLinePM.vendorID, IBqlInt>.IsEqual<BqlField<ChangeOrderEntry.POLineFilter.vendorID, IBqlInt>.FromCurrent>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.pOOrderNbr>, IsNull>>>>.Or<BqlOperand<POLinePM.orderNbr, IBqlString>.IsEqual<BqlField<ChangeOrderEntry.POLineFilter.pOOrderNbr, IBqlString>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.projectTaskID>, IsNull>>>>.Or<BqlOperand<POLinePM.taskID, IBqlInt>.IsEqual<BqlField<ChangeOrderEntry.POLineFilter.projectTaskID, IBqlInt>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.inventoryID>, IsNull>>>>.Or<BqlOperand<POLinePM.inventoryID, IBqlInt>.IsEqual<BqlField<ChangeOrderEntry.POLineFilter.inventoryID, IBqlInt>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.costCodeFrom>, IsNull>>>>.Or<BqlOperand<POLinePM.costCodeCD, IBqlString>.IsGreaterEqual<BqlField<ChangeOrderEntry.POLineFilter.costCodeFrom, IBqlString>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ChangeOrderEntry.POLineFilter.costCodeTo>, IsNull>>>>.Or<BqlOperand<POLinePM.costCodeCD, IBqlString>.IsLessEqual<BqlField<ChangeOrderEntry.POLineFilter.costCodeTo, IBqlString>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLinePM.hasMultipleProjects, Equal<True>>>>>.Or<BqlOperand<POLinePM.projectID, IBqlInt>.IsEqual<BqlField<PMChangeOrder.projectID, IBqlInt>.FromCurrent>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLinePM.cancelled, NotEqual<True>>>>>.Or<BqlOperand<Current<ChangeOrderEntry.POLineFilter.includeNonOpen>, IBqlBool>.IsEqual<True>>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLinePM.completed, NotEqual<True>>>>>.Or<BqlOperand<Current<ChangeOrderEntry.POLineFilter.includeNonOpen>, IBqlBool>.IsEqual<True>>>>>, POLinePM>.View((PXGraph) this);
    int startRow = PXView.StartRow;
    int num = 0;
    if (this.polines == null || this.IsCacheUpdateRequired())
      this.polines = new Dictionary<ChangeOrderEntry.POLineKey, POLinePM>();
    poLinePmList.AddRange(GraphHelper.RowCast<POLinePM>((IEnumerable) ((PXSelectBase) view).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num)));
    PXView.StartRow = 0;
    return (IEnumerable) poLinePmList;
  }

  [PXRemoveBaseAttribute(typeof (PXUIFieldAttribute))]
  [PXUIField]
  [POOrderType.RPSList]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.orderType> e)
  {
  }

  public ChangeOrderEntry()
  {
    APSetup current = ((PXSelectBase<APSetup>) this.apSetup).Current;
    ((PXSelectBase) this.AvailablePOLines).AllowInsert = false;
    ((PXSelectBase) this.AvailablePOLines).AllowDelete = false;
    ((PXAction) ((PXGraph) this).GetExtension<ChangeOrderEntry.ReversingChangeOrderExt>().viewChangeOrder).SetVisible(false);
    ((PXAction) ((PXGraph) this).GetExtension<ChangeOrderEntry.ReversingChangeOrderExt>().viewCurrentReversingChangeOrder).SetVisible(false);
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

  protected virtual void BeforeCommitHandler(PXGraph e)
  {
    Action<PXGraph> checkerDelegate1 = this._licenseLimits.GetCheckerDelegate<PMChangeOrder>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (PMChangeOrderBudget), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<PMChangeOrderBudget.refNbr>((object) ((PXSelectBase<PMChangeOrder>) ((ChangeOrderEntry) graph).Document).Current?.RefNbr)
      }))
    });
    try
    {
      checkerDelegate1(e);
    }
    catch (PXException ex)
    {
      throw new PXException("The total number of lines on the Cost Budget and Revenue Budget tabs has exceeded the limit set for the current license. Please reduce the number of lines to be able to save the document.");
    }
    Action<PXGraph> checkerDelegate2 = this._licenseLimits.GetCheckerDelegate<PMChangeOrder>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (PMChangeOrderLine), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<PMChangeOrderLine.refNbr>((object) ((PXSelectBase<PMChangeOrder>) ((ChangeOrderEntry) graph).Document).Current?.RefNbr)
      }))
    });
    try
    {
      checkerDelegate2(e);
    }
    catch (PXException ex)
    {
      throw new PXException("The number of lines on the Commitments tab has exceeded the limit set for the current license. Please reduce the number of lines to be able to save the document.");
    }
  }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits != null)
      ((PXGraph) this).OnBeforeCommit += new Action<PXGraph>(this.BeforeCommitHandler);
    ((PXGraph) this).OnBeforeCommit += new Action<PXGraph>(this.SaveChangesToTaskMadeAfterAccumulatorPersited);
  }

  protected virtual void SaveChangesToTaskMadeAfterAccumulatorPersited(PXGraph e)
  {
    ((PXGraph) this).Persist(typeof (PMTask), (PXDBOperation) 1);
  }

  public virtual ProjectBalance BalanceCalculator
  {
    get
    {
      if (this.balanceCalculator == null)
        this.balanceCalculator = new ProjectBalance((PXGraph) this);
      return this.balanceCalculator;
    }
  }

  [PXUIField(DisplayName = "Release")]
  [PXProcessButton]
  public IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ChangeOrderEntry.\u003C\u003Ec__DisplayClass89_0 cDisplayClass890 = new ChangeOrderEntry.\u003C\u003Ec__DisplayClass89_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass890.list = adapter.Get<PMChangeOrder>().ToList<PMChangeOrder>();
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass890.list.Count == 0)
    {
      // ISSUE: reference to a compiler-generated field
      return (IEnumerable) cDisplayClass890.list;
    }
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass890, __methodptr(\u003CRelease\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass890.list;
  }

  [PXUIField(DisplayName = "Reverse")]
  [PXProcessButton]
  public IEnumerable Reverse(PXAdapter adapter)
  {
    ((PXGraph) this).Actions.PressSave();
    try
    {
      this.ReverseDocument();
    }
    catch
    {
      ((PXGraph) this).Clear();
      throw;
    }
    return (IEnumerable) new PMChangeOrder[1]
    {
      ((PXSelectBase<PMChangeOrder>) this.Document).Current
    };
  }

  [PXUIField]
  [PXButton]
  public IEnumerable Approve(PXAdapter adapter)
  {
    ChangeOrderEntry changeOrderEntry = this;
    List<PMChangeOrder> pmChangeOrderList = new List<PMChangeOrder>();
    foreach (PXResult<PMChangeOrder, PMProject> pxResult in adapter.Get())
    {
      PMChangeOrder pmChangeOrder = PXResult<PMChangeOrder, PMProject>.op_Implicit(pxResult);
      pmChangeOrderList.Add(pmChangeOrder);
    }
    if (((PXGraph) changeOrderEntry).IsDirty)
      ((PXAction) changeOrderEntry.Save).Press();
    foreach (PMChangeOrder source in pmChangeOrderList)
    {
      try
      {
        if (!source.Approved.GetValueOrDefault())
        {
          if (((PXSelectBase<PMSetup>) changeOrderEntry.Setup).Current.ChangeOrderApprovalMapID.HasValue)
          {
            if (!changeOrderEntry.Approval.Approve(source))
              throw new PXSetPropertyException((IBqlTable) source, "You are not an authorized approver for this document.");
            source.Approved = new bool?(changeOrderEntry.Approval.IsApproved(source));
            if (source.Approved.GetValueOrDefault())
              source.Status = "O";
          }
          else
          {
            source.Approved = new bool?(true);
            source.Status = "O";
          }
          ((PXSelectBase<PMChangeOrder>) changeOrderEntry.Document).Update(source);
          ((PXAction) changeOrderEntry.Save).Press();
        }
        else
          continue;
      }
      catch (ReasonRejectedException ex)
      {
      }
      yield return (object) source;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Reject(PXAdapter adapter)
  {
    this.IncreaseDraftBucket(-1);
    ((PXAction) this.Save).Press();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable COReport(PXAdapter adapter)
  {
    this.OpenReport("PM643000", ((PXSelectBase<PMChangeOrder>) this.Document).Current);
    return adapter.Get();
  }

  public virtual void OpenReport(string reportID, PMChangeOrder doc)
  {
    if (doc != null && ((PXSelectBase) this.Document).Cache.GetStatus((object) doc) != 2)
    {
      string str = new NotificationUtility((PXGraph) this).SearchProjectReport(reportID, ((PXSelectBase<PMProject>) this.Project).Current.ContractID, ((PXSelectBase<PMProject>) this.Project).Current.DefaultBranchID);
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["RefNbr"] = doc.RefNbr
      }, str, str, (CurrentLocalization) null);
    }
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable Send(PXAdapter adapter)
  {
    if (((PXSelectBase<PMChangeOrder>) this.Document).Current != null)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ChangeOrderEntry.\u003C\u003Ec__DisplayClass100_0 displayClass1000 = new ChangeOrderEntry.\u003C\u003Ec__DisplayClass100_0();
      ((PXAction) this.Save).Press();
      // ISSUE: reference to a compiler-generated field
      displayClass1000.graph = PXGraph.CreateInstance<ChangeOrderEntry>();
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase<PMChangeOrder>) displayClass1000.graph.Document).Current = ((PXSelectBase<PMChangeOrder>) this.Document).Current;
      // ISSUE: reference to a compiler-generated field
      displayClass1000.changeOrderRefNumber = ((PXSelectBase<PMChangeOrder>) this.Document).Current.RefNbr;
      // ISSUE: reference to a compiler-generated field
      displayClass1000.massProcess = adapter.MassProcess;
      // ISSUE: reference to a compiler-generated field
      displayClass1000.defaultBranchId = ((PXSelectBase<PMProject>) this.Project).Current.DefaultBranchID;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1000, __methodptr(\u003CSend\u003Eb__0)));
    }
    return adapter.Get();
  }

  public virtual void SendReport(string notificationCD, PMChangeOrder doc, bool massProcess = false)
  {
    if (doc == null)
      return;
    if (((PXSelectBase<PMChangeOrder>) this.Document).Current != doc)
      ((PXSelectBase<PMChangeOrder>) this.Document).Current = doc;
    ChangeOrderEntry.SendReport(this, doc.RefNbr, notificationCD, ((PXSelectBase<PMProject>) this.Project).Current.DefaultBranchID, massProcess);
  }

  public static void SendReport(
    ChangeOrderEntry graph,
    string changeOrderRefNbr,
    string notificationCD,
    int? defaultBranchId,
    bool massProcess)
  {
    try
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>()
      {
        ["RefNbr"] = changeOrderRefNbr
      };
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        ((PXGraph) graph).GetExtension<ChangeOrderEntry.ChangeOrderEntry_ActivityDetailsExt>().SendNotification("Project", notificationCD, defaultBranchId, (IDictionary<string, string>) parameters, massProcess, (IList<Guid?>) null);
        ((PXAction) graph.Save).Press();
        transactionScope.Complete();
      }
    }
    catch (EmailFromReportCannotBeCreatedException ex)
    {
      throw new PXException((Exception) ex, "Email cannot be created for the {0} change order because the related printed form {1} that must be attached to the email contains no lines. You can modify the {1} printed form in the Report Designer to include cost and commitment lines to the report as well, or specify another report for the CHANGE ORDER mailing ID on the Mailing & Printing tab of the Project Preferences (PM101000) form.", new object[2]
      {
        (object) changeOrderRefNbr,
        (object) ex.ReportId
      });
    }
  }

  [PXUIField(DisplayName = "Add Budget Lines")]
  [PXButton]
  public IEnumerable AddCostBudget(PXAdapter adapter)
  {
    if (((PXSelectBase) this.AvailableCostBudget).View.AskExt() == 1)
      this.AddSelectedCostBudget();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Add Lines")]
  [PXButton]
  public IEnumerable AppendSelectedCostBudget(PXAdapter adapter)
  {
    this.AddSelectedCostBudget();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Add Budget Lines")]
  [PXButton]
  public IEnumerable AddRevenueBudget(PXAdapter adapter)
  {
    if (((PXSelectBase) this.AvailableRevenueBudget).View.AskExt() == 1)
      this.AddSelectedRevenueBudget();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Add Lines")]
  [PXButton]
  public IEnumerable AppendSelectedRevenueBudget(PXAdapter adapter)
  {
    this.AddSelectedRevenueBudget();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Add Commitments")]
  [PXButton]
  public IEnumerable AddPOLines(PXAdapter adapter)
  {
    // ISSUE: method pointer
    if (((PXSelectBase) this.AvailablePOLines).View.AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CAddPOLines\u003Eb__112_0)), true) == 1)
      this.AddSelectedPOLines();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Add Lines")]
  [PXButton]
  public IEnumerable AppendSelectedPOLines(PXAdapter adapter)
  {
    this.AddSelectedPOLines();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public IEnumerable ViewCommitments(PXAdapter adapter)
  {
    if (((PXSelectBase<PMChangeOrderLine>) this.Details).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PMChangeOrderLine>) this.Details).Current.POOrderNbr))
    {
      POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
      ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrder, PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<PMChangeOrderLine.pOOrderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Current<PMChangeOrderLine.pOOrderNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Commitments");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewChangeOrder(PXAdapter adapter)
  {
    if (((PXSelectBase<PMChangeOrder>) this.Document).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PMChangeOrder>) this.Document).Current.OrigRefNbr))
      ProjectAccountingService.NavigateToChangeOrderScreen(((PXSelectBase<PMChangeOrder>) this.Document).Current.OrigRefNbr, "View Change Order");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewRevenueBudgetTask(PXAdapter adapter)
  {
    ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
    ((PXSelectBase<PMTask>) instance.Task).Current = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMChangeOrderRevenueBudget.projectID>>, And<PMTask.taskID, Equal<Current<PMChangeOrderRevenueBudget.projectTaskID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Task");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCostBudgetTask(PXAdapter adapter)
  {
    ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
    ((PXSelectBase<PMTask>) instance.Task).Current = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMChangeOrderCostBudget.projectID>>, And<PMTask.taskID, Equal<Current<PMChangeOrderCostBudget.projectTaskID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Task");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCommitmentTask(PXAdapter adapter)
  {
    ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
    ((PXSelectBase<PMTask>) instance.Task).Current = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMChangeOrderLine.projectID>>, And<PMTask.taskID, Equal<Current<PMChangeOrderLine.taskID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Task");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewRevenueBudgetInventory(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToInventoryItemScreen(PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMChangeOrderRevenueBudget.inventoryID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCostBudgetInventory(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToInventoryItemScreen(PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMChangeOrderCostBudget.inventoryID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCommitmentInventory(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToInventoryItemScreen(PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMChangeOrderLine.inventoryID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())));
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable Hold(PXAdapter adapter)
  {
    if (((PXSelectBase<PMChangeOrder>) this.Document).Current.Status == "R" || ((PXSelectBase<PMChangeOrder>) this.Document).Current.Status == "L")
    {
      this.IncreaseDraftBucket(1);
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable RemoveHold(PXAdapter adapter)
  {
    this.ValidatePOLinesBeforeOpen();
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Cancel")]
  protected virtual IEnumerable COCancel(PXAdapter adapter)
  {
    if (((PXSelectBase<PMChangeOrder>) this.Document).Current.Status != "R")
      this.IncreaseDraftBucket(-1);
    ((PXSelectBase<PMChangeOrder>) this.Document).Current.Hold = new bool?(true);
    ((PXSelectBase<PMChangeOrder>) this.Document).Current.Approved = new bool?(false);
    ((PXSelectBase<PMChangeOrder>) this.Document).Current.Rejected = new bool?(false);
    ((PXSelectBase<PMChangeOrder>) this.Document).Current.Released = new bool?(false);
    ((PXSelectBase<PMChangeOrder>) this.Document).Update(((PXSelectBase<PMChangeOrder>) this.Document).Current);
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMChangeOrder> e)
  {
    bool valueOrDefault = ((PXSelectBase<PMSetup>) this.Setup).Current.CostCommitmentTracking.GetValueOrDefault();
    ((PXSelectBase) this.Details).Cache.AllowSelect = valueOrDefault;
    if (e.Row == null)
      return;
    bool flag1 = e.Row.ReverseStatus != "N";
    PXUIFieldAttribute.SetVisible<PMChangeOrder.reverseStatus>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetVisible<PMChangeOrder.origRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetVisible<PMChangeOrder.reversingRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag1);
    string budgetLevel1 = ((PXSelectBase<PMProject>) this.Project).Current?.CostBudgetLevel ?? "D";
    string budgetLevel2 = ((PXSelectBase<PMProject>) this.Project).Current?.BudgetLevel ?? "D";
    if (!((PXGraph) this).IsCopyPasteContext && !((PXGraph) this).IsImport && !((PXGraph) this).IsExport)
    {
      PXUIFieldAttribute.SetVisible<PMChangeOrderRevenueBudget.inventoryID>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, BudgetLevelHelper.ContainsItem(budgetLevel2));
      PXUIFieldAttribute.SetVisible<PMChangeOrderCostBudget.inventoryID>(((PXSelectBase) this.CostBudget).Cache, (object) null, BudgetLevelHelper.ContainsItem(budgetLevel1));
      PXUIFieldAttribute.SetVisible<PMChangeOrderRevenueBudget.costCodeID>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, BudgetLevelHelper.ContainsCostCode(budgetLevel2));
      PXUIFieldAttribute.SetVisible<PMChangeOrderCostBudget.costCodeID>(((PXSelectBase) this.CostBudget).Cache, (object) null, BudgetLevelHelper.ContainsCostCode(budgetLevel1));
    }
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedAmount>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.committedQty>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedInvoicedAmount>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.committedInvoicedQty>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedOpenAmount>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.committedOpenQty>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.committedReceivedQty>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.committedCOQty>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedCOAmount>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMChangeOrderCostBudget.committedCOQty>(((PXSelectBase) this.CostBudget).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMChangeOrderCostBudget.committedCOAmount>(((PXSelectBase) this.CostBudget).Cache, (object) null, valueOrDefault);
    PXUIVisibility pxuiVisibility = valueOrDefault ? (PXUIVisibility) 3 : (PXUIVisibility) 1;
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCommittedAmount>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisibility<PMBudget.committedQty>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCommittedInvoicedAmount>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisibility<PMBudget.committedInvoicedQty>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisibility<PMBudget.curyCommittedOpenAmount>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisibility<PMBudget.committedOpenQty>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisibility<PMBudget.committedReceivedQty>(((PXGraph) this).Caches[typeof (PMBudget)], (object) null, pxuiVisibility);
    bool flag2 = this.CanEditDocument(e.Row);
    ((PXSelectBase) this.Document).Cache.AllowDelete = flag2;
    PMChangeOrderClass current1 = ((PXSelectBase<PMChangeOrderClass>) this.ChangeOrderClass).Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.IsRevenueBudgetEnabled;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag3 = num1 != 0;
    ((PXAction) this.addRevenueBudget).SetEnabled(flag2 & flag3);
    ((PXSelectBase) this.RevenueBudget).Cache.AllowInsert = flag2 & flag3;
    ((PXSelectBase) this.RevenueBudget).Cache.AllowUpdate = flag2 & flag3;
    ((PXSelectBase) this.RevenueBudget).Cache.AllowDelete = flag2 & flag3;
    PMChangeOrderClass current2 = ((PXSelectBase<PMChangeOrderClass>) this.ChangeOrderClass).Current;
    int num2;
    if (current2 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = current2.IsCostBudgetEnabled;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag4 = num2 != 0;
    ((PXAction) this.addCostBudget).SetEnabled(flag2 & flag4);
    ((PXSelectBase) this.CostBudget).Cache.AllowInsert = flag2 & flag4;
    ((PXSelectBase) this.CostBudget).Cache.AllowUpdate = flag2 & flag4;
    ((PXSelectBase) this.CostBudget).Cache.AllowDelete = flag2 & flag4;
    PMChangeOrderClass current3 = ((PXSelectBase<PMChangeOrderClass>) this.ChangeOrderClass).Current;
    int num3;
    if (current3 == null)
    {
      num3 = 0;
    }
    else
    {
      nullable = current3.IsPurchaseOrderEnabled;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag5 = num3 != 0;
    PXUIFieldAttribute.SetVisible<PMChangeOrder.commitmentTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag5);
    ((PXAction) this.addPOLines).SetEnabled(flag2 & flag5);
    ((PXSelectBase) this.Details).Cache.AllowInsert = flag2 & flag5;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = flag2 & flag5;
    ((PXSelectBase) this.Details).Cache.AllowDelete = flag2 & flag5;
    ((PXSelectBase) this.Answers).Cache.AllowInsert = flag2;
    ((PXSelectBase) this.Answers).Cache.AllowUpdate = flag2;
    ((PXSelectBase) this.Answers).Cache.AllowDelete = flag2;
    if (!((PXGraph) this).IsContractBasedAPI)
    {
      PXUIFieldAttribute.SetEnabled<PMChangeOrder.classID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag2);
      PXUIFieldAttribute.SetEnabled<PMChangeOrder.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag2 && this.IsProjectEnabled());
      PXUIFieldAttribute.SetEnabled<PMChangeOrder.description>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag2);
      PXUIFieldAttribute.SetEnabled<PMChangeOrder.completionDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag2);
    }
    PXUIFieldAttribute.SetEnabled<PMChangeOrder.extRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag2);
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache;
    PMChangeOrder row = e.Row;
    int num4;
    if (flag2)
    {
      nullable = e.Row.IsRevenueVisible;
      if (nullable.GetValueOrDefault())
      {
        num4 = e.Row.ProjectNbr != "N/A" ? 1 : 0;
        goto label_18;
      }
    }
    num4 = 0;
label_18:
    PXUIFieldAttribute.SetEnabled<PMChangeOrder.projectNbr>(cache, (object) row, num4 != 0);
    PXUIFieldAttribute.SetEnabled<PMChangeOrder.date>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag2);
    PXUIFieldAttribute.SetEnabled<PMChangeOrder.delayDays>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag2);
    PXUIFieldAttribute.SetEnabled<PMChangeOrder.text>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrder>>) e).Cache, (object) e.Row, flag2);
    PXUIFieldAttribute.SetEnabled<PMChangeOrderLine.pOOrderType>(((PXSelectBase) this.Details).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<PMChangeOrderLine.pOOrderType>(((PXSelectBase) this.Details).Cache, (object) null, true);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMChangeOrder> e)
  {
    PMChangeOrder row = e.Row;
    PMChangeOrderClass changeOrderClass = PMChangeOrderClass.PK.Find((PXGraph) this, row.ClassID);
    if (changeOrderClass != null && changeOrderClass.IncrementsProjectNumber.GetValueOrDefault() && ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.LastChangeOrderNumber == row.ProjectNbr && !string.IsNullOrEmpty(((PXSelectBase<PMProject>) this.Project).Current.LastChangeOrderNumber))
    {
      ((PXSelectBase<PMProject>) this.Project).Current.LastChangeOrderNumber = NumberHelper.DecreaseNumber(((PXSelectBase<PMProject>) this.Project).Current.LastChangeOrderNumber, 1);
      ((PXSelectBase<PMProject>) this.ProjectProperties).Update(((PXSelectBase<PMProject>) this.Project).Current);
    }
    this.UpdateOriginalReverseStatus(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<PMChangeOrder>>) e).Cache, row);
  }

  private void UpdateOriginalReverseStatus(PXCache cache, PMChangeOrder order)
  {
    if (!(order.ReverseStatus == "R"))
      return;
    PMChangeOrder pmChangeOrder = PXResultset<PMChangeOrder>.op_Implicit(PXSelectBase<PMChangeOrder, PXViewOf<PMChangeOrder>.BasedOn<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMChangeOrder.refNbr, IBqlString>.IsEqual<BqlField<PMChangeOrder.origRefNbr, IBqlString>.FromCurrent>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    object[] objArray = new object[2]
    {
      (object) pmChangeOrder.RefNbr,
      (object) order.RefNbr
    };
    pmChangeOrder.ReverseStatus = PXSelectBase<PMChangeOrder, PXViewOf<PMChangeOrder>.BasedOn<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.origRefNbr, Equal<P.AsString>>>>>.And<BqlOperand<PMChangeOrder.refNbr, IBqlString>.IsNotEqual<P.AsString>>>>.Config>.Select((PXGraph) this, objArray).Count <= 0 ? (string.IsNullOrWhiteSpace(pmChangeOrder.OrigRefNbr) ? "N" : "R") : "X";
    cache.Update((object) pmChangeOrder);
    cache.GetAttributesOfType<PXDBTimestampAttribute>((object) null, "tstamp").First<PXDBTimestampAttribute>().VerifyTimestamp = (VerifyTimestampOptions) 1;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrder, PMChangeOrder.projectNbr> e)
  {
    PMChangeOrderClass changeOrderClass = PMChangeOrderClass.PK.Find((PXGraph) this, e.Row.ClassID);
    if (changeOrderClass != null && e.Row.ProjectID.HasValue && changeOrderClass.IncrementsProjectNumber.GetValueOrDefault())
    {
      PMProject project = this.GetProject(e.Row.ProjectID);
      string number = string.IsNullOrEmpty(project?.LastChangeOrderNumber) ? "0000" : project.LastChangeOrderNumber;
      if (!char.IsDigit(number[number.Length - 1]))
        number = $"{number}0000";
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrder, PMChangeOrder.projectNbr>, PMChangeOrder, object>) e).NewValue = (object) NumberHelper.IncreaseNumber(number, 1);
    }
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrder, PMChangeOrder.projectNbr>, PMChangeOrder, object>) e).NewValue = (object) "N/A";
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMChangeOrder, PMChangeOrder.projectNbr> e)
  {
    if (((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrder, PMChangeOrder.projectNbr>, PMChangeOrder, object>) e).NewValue).Equals("N/A", StringComparison.InvariantCultureIgnoreCase))
      return;
    PXSelect<PMChangeOrder, Where<PMChangeOrder.projectID, Equal<Current<PMChangeOrder.projectID>>, And<PMChangeOrder.projectNbr, Equal<Required<PMChangeOrder.projectNbr>>, And<PMChangeOrder.reverseStatus, NotEqual<ChangeOrderReverseStatus.reversed>>>>> pxSelect = new PXSelect<PMChangeOrder, Where<PMChangeOrder.projectID, Equal<Current<PMChangeOrder.projectID>>, And<PMChangeOrder.projectNbr, Equal<Required<PMChangeOrder.projectNbr>>, And<PMChangeOrder.reverseStatus, NotEqual<ChangeOrderReverseStatus.reversed>>>>>((PXGraph) this);
    if (((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMChangeOrder, PMChangeOrder.projectNbr>>) e).Cache.GetStatus((object) e.Row) != 2)
      ((PXSelectBase<PMChangeOrder>) pxSelect).WhereAnd<Where<PMChangeOrder.refNbr, NotEqual<Current<PMChangeOrder.refNbr>>>>();
    PMChangeOrder pmChangeOrder = PXResultset<PMChangeOrder>.op_Implicit(((PXSelectBase<PMChangeOrder>) pxSelect).Select(new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrder, PMChangeOrder.projectNbr>, PMChangeOrder, object>) e).NewValue
    }));
    if (pmChangeOrder != null && pmChangeOrder != e.Row && pmChangeOrder.RefNbr != e.Row.RefNbr)
      throw new PXSetPropertyException((IBqlTable) e.Row, "The project already has the {0} change order with this number.", new object[1]
      {
        (object) pmChangeOrder.RefNbr
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrder, PMChangeOrder.projectNbr> e)
  {
    PMChangeOrderClass changeOrderClass = PMChangeOrderClass.PK.Find((PXGraph) this, e.Row.ClassID);
    if (changeOrderClass == null || !e.Row.ProjectID.HasValue || !changeOrderClass.IncrementsProjectNumber.GetValueOrDefault())
      return;
    PMProject project = this.GetProject(e.Row.ProjectID);
    if (project == null || !(e.Row.ProjectNbr != "N/A") || !string.IsNullOrEmpty(project.LastChangeOrderNumber) && (string.IsNullOrEmpty(e.Row.ProjectNbr) || !(NumberHelper.GetTextPrefix(e.Row.ProjectNbr) == NumberHelper.GetTextPrefix(project.LastChangeOrderNumber)) || NumberHelper.GetNumericValue(e.Row.ProjectNbr) < NumberHelper.GetNumericValue(project.LastChangeOrderNumber)))
      return;
    project.LastChangeOrderNumber = e.Row.ProjectNbr;
    ((PXSelectBase<PMProject>) this.ProjectProperties).Update(project);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMChangeOrder, PMChangeOrder.projectID> e)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, new int?((int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrder, PMChangeOrder.projectID>, PMChangeOrder, object>) e).NewValue));
    if (pmProject != null && (pmProject.Status == "F" || pmProject.Status == "C" || pmProject.Status == "E"))
      throw new PXSetPropertyException("A change order cannot be created for the project with the {0} status.", (PXErrorLevel) 4, new object[1]
      {
        (object) new ProjectStatus.ListAttribute().ValueLabelDic[pmProject.Status]
      })
      {
        ErrorValue = (object) pmProject.ContractCD
      };
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrder, PMChangeOrder.projectID> e)
  {
    PMChangeOrderClass changeOrderClass = PMChangeOrderClass.PK.Find((PXGraph) this, e.Row.ClassID);
    if (changeOrderClass == null || !changeOrderClass.IncrementsProjectNumber.GetValueOrDefault())
      return;
    PMProject project = this.GetProject((int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMChangeOrder, PMChangeOrder.projectID>, PMChangeOrder, object>) e).OldValue);
    if (project != null && project.LastChangeOrderNumber == e.Row.ProjectNbr && !string.IsNullOrEmpty(((PXSelectBase<PMProject>) this.Project).Current.LastChangeOrderNumber))
    {
      project.LastChangeOrderNumber = NumberHelper.DecreaseNumber(project.LastChangeOrderNumber, 1);
      ((PXSelectBase<PMProject>) this.ProjectProperties).Update(project);
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrder, PMChangeOrder.projectID>>) e).Cache.SetDefaultExt<PMChangeOrder.projectNbr>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrder, PMChangeOrder.classID> e)
  {
    PMProject project = this.GetProject(e.Row.ProjectID);
    PMChangeOrderClass changeOrderClass1 = PMChangeOrderClass.PK.Find((PXGraph) this, (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMChangeOrder, PMChangeOrder.classID>, PMChangeOrder, object>) e).OldValue);
    PMChangeOrderClass changeOrderClass2 = PMChangeOrderClass.PK.Find((PXGraph) this, e.Row.ClassID);
    if (changeOrderClass1 != null && changeOrderClass2 != null)
    {
      bool? incrementsProjectNumber1 = changeOrderClass1.IncrementsProjectNumber;
      bool? incrementsProjectNumber2 = changeOrderClass2.IncrementsProjectNumber;
      if (!(incrementsProjectNumber1.GetValueOrDefault() == incrementsProjectNumber2.GetValueOrDefault() & incrementsProjectNumber1.HasValue == incrementsProjectNumber2.HasValue))
      {
        if (project != null)
        {
          incrementsProjectNumber2 = changeOrderClass1.IncrementsProjectNumber;
          if (incrementsProjectNumber2.GetValueOrDefault() && project.LastChangeOrderNumber == e.Row.ProjectNbr && !string.IsNullOrEmpty(((PXSelectBase<PMProject>) this.Project).Current.LastChangeOrderNumber))
          {
            project.LastChangeOrderNumber = NumberHelper.DecreaseNumber(((PXSelectBase<PMProject>) this.Project).Current.LastChangeOrderNumber, 1);
            ((PXSelectBase<PMProject>) this.ProjectProperties).Update(project);
          }
        }
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrder, PMChangeOrder.classID>>) e).Cache.SetDefaultExt<PMChangeOrder.projectNbr>((object) e.Row);
        return;
      }
    }
    if (changeOrderClass1 != null || changeOrderClass2 == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrder, PMChangeOrder.classID>>) e).Cache.SetDefaultExt<PMChangeOrder.projectNbr>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMChangeOrder, PMChangeOrder.classID> e)
  {
    if (string.IsNullOrEmpty(e.Row.ClassID))
      return;
    PMChangeOrderClass changeOrderClass = PMChangeOrderClass.PK.Find((PXGraph) this, (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrder, PMChangeOrder.classID>, PMChangeOrder, object>) e).NewValue);
    if (changeOrderClass == null)
      return;
    if (!changeOrderClass.IsRevenueBudgetEnabled.GetValueOrDefault() && ((PXSelectBase<PMChangeOrderRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()).Count > 0)
      throw new PXSetPropertyException<PMChangeOrder.classID>("The change order class you are about to select does not support project revenue budget modification. Before disabling revenue budget modification for the change order, please make sure there are no change order lines affecting project revenue budget.");
    if (!changeOrderClass.IsCostBudgetEnabled.GetValueOrDefault() && ((PXSelectBase<PMChangeOrderCostBudget>) this.CostBudget).Select(Array.Empty<object>()).Count > 0)
      throw new PXSetPropertyException<PMChangeOrder.classID>("The change order class you are about to select does not support project cost budget modification. Before disabling cost budget modification for the change order, please make sure there are no change order lines affecting project cost budget.");
    if (!changeOrderClass.IsPurchaseOrderEnabled.GetValueOrDefault() && ((PXSelectBase<PMChangeOrderLine>) this.Details).Select(Array.Empty<object>()).Count > 0)
      throw new PXSetPropertyException<PMChangeOrder.classID>("The change order class you are about to select does not support project commitments modification. Before disabling commitments modification for the change order, please make sure there are no change order lines affecting project commitments.");
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMChangeOrderCostBudget> e)
  {
    this.InitCostBudgetFields(e.Row);
    PMBudget originalCostBudget = this.IsValidKey((PMChangeOrderBudget) e.Row) ? (PMBudget) this.GetOriginalCostBudget(BudgetKeyTuple.Create((IProjectFilter) e.Row)) : (PMBudget) null;
    PXUIFieldAttribute.SetEnabled<PMChangeOrderCostBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderCostBudget>>) e).Cache, (object) e.Row, originalCostBudget == null);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PMChangeOrderCostBudget> e)
  {
    this.InitCostBudgetFields(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMChangeOrderCostBudget> e)
  {
    this.IncreaseDraftBucket((PMChangeOrderBudget) e.Row, 1);
    this.RemoveObsoleteLines();
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PMChangeOrderCostBudget> e)
  {
    this.InitCostBudgetFields(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMChangeOrderCostBudget> e)
  {
    this.IncreaseDraftBucket((PMChangeOrderBudget) e.OldRow, -1);
    this.IncreaseDraftBucket((PMChangeOrderBudget) e.Row, 1);
    this.RemoveObsoleteLines();
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMChangeOrderCostBudget> e)
  {
    this.IncreaseDraftBucket((PMChangeOrderBudget) e.Row, -1);
    this.RemoveObsoleteLines();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.costCodeID> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null || !(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel != "C"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.costCodeID>, PMChangeOrderCostBudget, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.inventoryID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.inventoryID>, PMChangeOrderCostBudget, object>) e).NewValue = (object) PMInventorySelectorAttribute.EmptyInventoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.projectTaskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMChangeOrderCostBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMChangeOrderCostBudget.description>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMChangeOrderCostBudget.accountGroupID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.accountGroupID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.accountGroupID>>) e).Cache.SetDefaultExt<PMChangeOrderCostBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.accountGroupID>>) e).Cache.SetDefaultExt<PMChangeOrderCostBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.inventoryID> e)
  {
    PMChangeOrderCostBudget row = e.Row;
    if (row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMChangeOrderCostBudget.accountGroupID>((object) row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMChangeOrderCostBudget.uOM>((object) row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMChangeOrderCostBudget.description>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.uOM> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.uOM>>) e).Cache.SetDefaultExt<PMChangeOrderCostBudget.rate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.costCodeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.costCodeID>>) e).Cache.SetDefaultExt<PMChangeOrderCostBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderCostBudget, PMChangeOrderCostBudget.costCodeID>>) e).Cache.SetDefaultExt<PMChangeOrderCostBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.uOM> e)
  {
    PMCostBudget originalCostBudget = this.IsValidKey((PMChangeOrderBudget) e.Row) ? this.GetOriginalCostBudget(BudgetKeyTuple.Create((IProjectFilter) e.Row)) : (PMCostBudget) null;
    if (originalCostBudget == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.uOM>, PMChangeOrderCostBudget, object>) e).NewValue = (object) originalCostBudget.UOM;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.rate> e)
  {
    PMCostBudget originalCostBudget = this.IsValidKey((PMChangeOrderBudget) e.Row) ? this.GetOriginalCostBudget(BudgetKeyTuple.Create((IProjectFilter) e.Row)) : (PMCostBudget) null;
    if (originalCostBudget != null)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.rate>, PMChangeOrderCostBudget, object>) e).NewValue = (object) originalCostBudget.CuryUnitRate;
    }
    else
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) this, e.Row.ProjectID);
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.rate>, PMChangeOrderCostBudget, object>) e).NewValue = (object) this.RateService.CalculateUnitCost(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.rate>>) e).Cache, e.Row.ProjectID, e.Row.ProjectTaskID, e.Row.InventoryID, e.Row.UOM, new int?(), (DateTime?) ((PXSelectBase<PMChangeOrder>) this.Document).Current?.Date, (long?) pmProject?.CuryInfoID);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.description> e)
  {
    if (e.Row == null || ((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    PMCostBudget originalCostBudget = this.GetOriginalCostBudget(BudgetKeyTuple.Create((IProjectFilter) e.Row));
    if (originalCostBudget != null)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.description>, PMChangeOrderCostBudget, object>) e).NewValue = (object) originalCostBudget.Description;
    else if (((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "C")
    {
      if (!e.Row.CostCodeID.HasValue)
        return;
      PMCostCode costCode = this.GetCostCode(e.Row.CostCodeID);
      if (costCode == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.description>, PMChangeOrderCostBudget, object>) e).NewValue = (object) costCode.Description;
    }
    else if (((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "T")
    {
      if (!e.Row.ProjectTaskID.HasValue || !(PXSelectorAttribute.Select<PMChangeOrderCostBudget.projectTaskID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.description>>) e).Cache, (object) e.Row) is PMTask pmTask))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.description>, PMChangeOrderCostBudget, object>) e).NewValue = (object) pmTask.Description;
    }
    else
    {
      if (!e.Row.InventoryID.HasValue)
        return;
      int? inventoryId = e.Row.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue)
        return;
      PX.Objects.IN.InventoryItem inventoryItem = this.GetInventoryItem(e.Row.InventoryID);
      if (inventoryItem == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderCostBudget, PMChangeOrderCostBudget.description>, PMChangeOrderCostBudget, object>) e).NewValue = (object) inventoryItem.Descr;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMChangeOrderRevenueBudget> e)
  {
    this.InitRevenueBudgetFields(e.Row);
    PMRevenueBudget originalRevenueBudget = this.IsValidKey((PMChangeOrderBudget) e.Row) ? this.GetOriginalRevenueBudget(BudgetKeyTuple.Create((IProjectFilter) e.Row)) : (PMRevenueBudget) null;
    PXUIFieldAttribute.SetEnabled<PMChangeOrderRevenueBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderRevenueBudget>>) e).Cache, (object) e.Row, originalRevenueBudget == null);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PMChangeOrderRevenueBudget> e)
  {
    this.InitRevenueBudgetFields(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMChangeOrderRevenueBudget> e)
  {
    this.IncreaseDraftBucket((PMChangeOrderBudget) e.Row, 1);
    this.RemoveObsoleteLines();
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PMChangeOrderRevenueBudget> e)
  {
    this.InitRevenueBudgetFields(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMChangeOrderRevenueBudget> e)
  {
    this.IncreaseDraftBucket((PMChangeOrderBudget) e.Row, -1);
    this.RemoveObsoleteLines();
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMChangeOrderRevenueBudget> e)
  {
    this.IncreaseDraftBucket((PMChangeOrderBudget) e.OldRow, -1);
    this.IncreaseDraftBucket((PMChangeOrderBudget) e.Row, 1);
    this.RemoveObsoleteLines();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.costCodeID> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null || !(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel != "C"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.costCodeID>, PMChangeOrderRevenueBudget, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.inventoryID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.inventoryID>, PMChangeOrderRevenueBudget, object>) e).NewValue = (object) PMInventorySelectorAttribute.EmptyInventoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.projectTaskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMChangeOrderRevenueBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMChangeOrderRevenueBudget.description>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMChangeOrderRevenueBudget.accountGroupID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.accountGroupID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.accountGroupID>>) e).Cache.SetDefaultExt<PMChangeOrderRevenueBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.accountGroupID>>) e).Cache.SetDefaultExt<PMChangeOrderRevenueBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.inventoryID> e)
  {
    PMChangeOrderRevenueBudget row = e.Row;
    if (row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMChangeOrderRevenueBudget.accountGroupID>((object) row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMChangeOrderRevenueBudget.uOM>((object) row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMChangeOrderRevenueBudget.description>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.uOM> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.uOM>>) e).Cache.SetDefaultExt<PMChangeOrderRevenueBudget.rate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.costCodeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.costCodeID>>) e).Cache.SetDefaultExt<PMChangeOrderRevenueBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.costCodeID>>) e).Cache.SetDefaultExt<PMChangeOrderRevenueBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.uOM> e)
  {
    PMRevenueBudget originalRevenueBudget = this.IsValidKey((PMChangeOrderBudget) e.Row) ? this.GetOriginalRevenueBudget(BudgetKeyTuple.Create((IProjectFilter) e.Row)) : (PMRevenueBudget) null;
    if (originalRevenueBudget == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.uOM>, PMChangeOrderRevenueBudget, object>) e).NewValue = (object) originalRevenueBudget.UOM;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.rate> e)
  {
    PMRevenueBudget originalRevenueBudget = this.IsValidKey((PMChangeOrderBudget) e.Row) ? this.GetOriginalRevenueBudget(BudgetKeyTuple.Create((IProjectFilter) e.Row)) : (PMRevenueBudget) null;
    if (originalRevenueBudget != null)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.rate>, PMChangeOrderRevenueBudget, object>) e).NewValue = (object) originalRevenueBudget.CuryUnitRate;
    }
    else
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) this, e.Row.ProjectID);
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.rate>, PMChangeOrderRevenueBudget, object>) e).NewValue = (object) this.RateService.CalculateUnitPrice(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.rate>>) e).Cache, e.Row.ProjectID, e.Row.ProjectTaskID, e.Row.InventoryID, e.Row.UOM, new Decimal?(), (DateTime?) ((PXSelectBase<PMChangeOrder>) this.Document).Current?.Date, (long?) pmProject?.CuryInfoID);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.description> e)
  {
    if (e.Row == null || ((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    PMRevenueBudget originalRevenueBudget = this.GetOriginalRevenueBudget(BudgetKeyTuple.Create((IProjectFilter) e.Row));
    if (originalRevenueBudget != null)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.description>, PMChangeOrderRevenueBudget, object>) e).NewValue = (object) originalRevenueBudget.Description;
    else if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "C")
    {
      if (!e.Row.CostCodeID.HasValue)
        return;
      PMCostCode costCode = this.GetCostCode(e.Row.CostCodeID);
      if (costCode == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.description>, PMChangeOrderRevenueBudget, object>) e).NewValue = (object) costCode.Description;
    }
    else if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "T")
    {
      if (!e.Row.ProjectTaskID.HasValue || !(PXSelectorAttribute.Select<PMChangeOrderRevenueBudget.projectTaskID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.description>>) e).Cache, (object) e.Row) is PMTask pmTask))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.description>, PMChangeOrderRevenueBudget, object>) e).NewValue = (object) pmTask.Description;
    }
    else
    {
      if (!e.Row.InventoryID.HasValue)
        return;
      int? inventoryId = e.Row.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue)
        return;
      PX.Objects.IN.InventoryItem inventoryItem = this.GetInventoryItem(e.Row.InventoryID);
      if (inventoryItem == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderRevenueBudget, PMChangeOrderRevenueBudget.description>, PMChangeOrderRevenueBudget, object>) e).NewValue = (object) inventoryItem.Descr;
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMChangeOrderLine> e)
  {
    POLinePM poLine = this.GetPOLine(e.Row);
    if (e.Row.POOrderNbr != e.OldRow.POOrderNbr)
    {
      if (!string.IsNullOrEmpty(e.OldRow.POOrderNbr))
      {
        e.Row.POLineNbr = new int?();
        e.Row.VendorID = new int?();
        e.Row.CuryID = (string) null;
        e.Row.AccountID = new int?();
      }
      POOrderPM poOrderPm = PXResultset<POOrderPM>.op_Implicit(PXSelectBase<POOrderPM, PXSelect<POOrderPM, Where<POOrderPM.orderType, Equal<Required<POOrderPM.orderType>>, And<POOrderPM.orderNbr, Equal<Required<POOrderPM.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) e.Row.POOrderType,
        (object) e.Row.POOrderNbr
      }));
      if (poOrderPm != null)
      {
        e.Row.VendorID = poOrderPm.VendorID;
        e.Row.CuryID = poOrderPm.CuryID;
        e.Row.AccountID = this.DefaultAccountID(e.Row);
      }
    }
    else
    {
      int? poLineNbr1 = e.Row.POLineNbr;
      int? poLineNbr2 = e.OldRow.POLineNbr;
      if (!(poLineNbr1.GetValueOrDefault() == poLineNbr2.GetValueOrDefault() & poLineNbr1.HasValue == poLineNbr2.HasValue) && this.IsValidKey(e.Row) && poLine != null)
      {
        e.Row.TaskID = poLine.TaskID;
        e.Row.UOM = poLine.UOM;
        e.Row.VendorID = poLine.VendorID;
        e.Row.CostCodeID = poLine.CostCodeID;
        e.Row.InventoryID = poLine.InventoryID;
        e.Row.UnitCost = poLine.CuryUnitCost;
        e.Row.CuryID = poLine.CuryID;
        e.Row.AccountID = poLine.ExpenseAcctID;
        ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PMChangeOrderLine>>) e).Cache.SetDefaultExt<PMChangeOrderLine.description>((object) e.Row);
      }
    }
    e.Row.LineType = this.ResolveChangeOrderLineType(e.Row, poLine);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMChangeOrderLine> e)
  {
    this.InitDetailLineFields(e.Row);
    if (e.Row == null)
      return;
    bool hasValue = e.Row.POLineNbr.HasValue;
    bool flag = !string.IsNullOrEmpty(e.Row.POOrderNbr);
    PXUIFieldAttribute.SetEnabled<PMChangeOrderLine.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderLine>>) e).Cache, (object) e.Row, !hasValue);
    PXUIFieldAttribute.SetEnabled<PMChangeOrderLine.inventoryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderLine>>) e).Cache, (object) e.Row, !hasValue);
    PXUIFieldAttribute.SetEnabled<PMChangeOrderLine.costCodeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderLine>>) e).Cache, (object) e.Row, !hasValue);
    PXUIFieldAttribute.SetEnabled<PMChangeOrderLine.vendorID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderLine>>) e).Cache, (object) e.Row, !flag);
    PXUIFieldAttribute.SetEnabled<PMChangeOrderLine.curyID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderLine>>) e).Cache, (object) e.Row, !flag);
    PXUIFieldAttribute.SetEnabled<PMChangeOrderLine.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderLine>>) e).Cache, (object) e.Row, !hasValue);
    PXUIFieldAttribute.SetEnabled<PMChangeOrderLine.accountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderLine>>) e).Cache, (object) e.Row, !hasValue);
    PXUIFieldAttribute.SetEnabled<PMChangeOrderLine.taxCategoryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderLine>>) e).Cache, (object) e.Row, !hasValue);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PMChangeOrderLine> e)
  {
    this.InitDetailLineFields(e.Row);
    POLinePM poLine = this.GetPOLine(e.Row);
    e.Row.LineType = this.ResolveChangeOrderLineType(e.Row, poLine);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PMChangeOrderLine> e)
  {
    this.InitDetailLineFields(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.accountID> e)
  {
    int? nullable = this.DefaultAccountID(e.Row);
    if (!nullable.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.accountID>, PMChangeOrderLine, object>) e).NewValue = (object) nullable;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.vendorID> e)
  {
    if (e.Row.AccountID.HasValue && !(e.Row?.POOrderType == "PD"))
    {
      if (!PXAccess.FeatureInstalled<FeaturesSet.interBranch>() || !(((PXSelectBase<APSetup>) this.apSetup).Current.IntercompanyExpenseAccountDefault == "L"))
        return;
      PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find((PXGraph) this, (int?) e.NewValue);
      if ((vendor != null ? (vendor.IsBranch.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.vendorID>>) e).Cache.SetDefaultExt<PMChangeOrderLine.accountID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.inventoryID>>) e).Cache.SetDefaultExt<PMChangeOrderLine.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.inventoryID>>) e).Cache.SetDefaultExt<PMChangeOrderLine.accountID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.inventoryID>>) e).Cache.SetDefaultExt<PMChangeOrderLine.taxCategoryID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.inventoryID>>) e).Cache.SetDefaultExt<PMChangeOrderLine.costCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.taskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.taskID>>) e).Cache.SetDefaultExt<PMChangeOrderLine.costCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.pOOrderType> e)
  {
    object obj;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.pOOrderType>>) e).Cache.RaiseFieldDefaulting<PMChangeOrderLine.accountID>((object) e.Row, ref obj);
    if (obj == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.pOOrderType>>) e).Cache.SetValueExt<PMChangeOrderLine.accountID>((object) e.Row, obj);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.costCodeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.costCodeID>>) e).Cache.SetDefaultExt<PMChangeOrderLine.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.accountID> e)
  {
    if (!e.Row.AccountID.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.accountID>>) e).Cache.SetDefaultExt<PMChangeOrderLine.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.pOOrderNbr> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.pOOrderNbr>>) e).Cache.SetDefaultExt<PMChangeOrderLine.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.pOLineNbr> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.pOLineNbr>>) e).Cache.SetDefaultExt<PMChangeOrderLine.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMChangeOrderLine, PMChangeOrderLine.qty> e)
  {
    PMChangeOrderLine row = e.Row;
    if (row == null)
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrderLine, PMChangeOrderLine.qty>, PMChangeOrderLine, object>) e).NewValue;
    Decimal? nullable = newValue;
    Decimal num1 = 0M;
    if (!(nullable.GetValueOrDefault() < num1 & nullable.HasValue) || ((PXGraph) this).IsCopyPasteContext)
      return;
    if (row.LineType == "D")
    {
      if (!(row.POOrderType == "RS"))
        throw new PXSetPropertyException("The quantity of a purchase order line with the New Line status cannot be negative.", (PXErrorLevel) 4, new object[1]
        {
          (object) row.Qty
        });
    }
    else
    {
      if (row.LineType == "L")
        throw new PXSetPropertyException("The quantity of a line with the New Document status cannot be negative.", (PXErrorLevel) 4, new object[1]
        {
          (object) row.Qty
        });
      POLinePM poLine = this.GetPOLine(e.Row);
      if (poLine == null)
        return;
      Decimal? calcOpenQty = poLine.CalcOpenQty;
      Decimal num2 = Math.Abs(newValue.Value);
      if (calcOpenQty.GetValueOrDefault() < num2 & calcOpenQty.HasValue)
        throw new PXSetPropertyException("The negative change cannot be applied because the value of the resulting document line cannot be negative or less than the received or billed value.", (PXErrorLevel) 4, new object[1]
        {
          (object) row.Qty
        });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMChangeOrderLine, PMChangeOrderLine.amount> e)
  {
    PMChangeOrderLine row = e.Row;
    if (row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrderLine, PMChangeOrderLine.amount>, PMChangeOrderLine, object>) e).NewValue == null)
      return;
    Decimal? nullable = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrderLine, PMChangeOrderLine.amount>, PMChangeOrderLine, object>) e).NewValue;
    Decimal num1 = nullable.Value;
    if (e.Row.LineType == "D")
    {
      nullable = e.Row.Qty;
      if ((nullable ?? 0M) * num1 < 0M)
        throw new PXSetPropertyException("In a line with the New Line status, the quantity and amount must either both have the same sign or be zero.", (PXErrorLevel) 4, new object[1]
        {
          (object) row.Amount
        });
    }
    if (row.LineType == "L")
      return;
    POLinePM poLine = this.GetPOLine(row);
    if (poLine == null)
      return;
    nullable = poLine.CuryLineAmt;
    Decimal valueOrDefault = nullable.GetValueOrDefault();
    nullable = poLine.CuryLineAmt;
    Decimal num2 = nullable.Value + num1;
    if (Math.Sign(num2) != Math.Sign(valueOrDefault) || !(Math.Abs(num2) < Math.Abs(valueOrDefault)))
      return;
    nullable = poLine.CuryUnbilledAmt;
    if (Math.Abs(nullable.GetValueOrDefault()) < Math.Abs(num1))
      throw new PXSetPropertyException(num2 > 0M ? "The change cannot be applied because the positive value of the Ext. Cost of the resulting document line must be greater than or equal to the value of its Billed Amount." : "The change cannot be applied because the negative value of the Ext. Cost of the resulting document line must be less than or equal to the value of its Billed Amount.", (PXErrorLevel) 4, new object[1]
      {
        (object) row.Amount
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.amount> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.amount>>) e).Cache.SetValueExt<PMChangeOrderLine.amountInProjectCury>((object) e.Row, (object) this.GetAmountInProjectCurrency(e.Row.CuryID, e.Row.Amount));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.curyID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.curyID>>) e).Cache.SetValueExt<PMChangeOrderLine.amountInProjectCury>((object) e.Row, (object) this.GetAmountInProjectCurrency(e.Row.CuryID, e.Row.Amount));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.description> e)
  {
    if (((PXGraph) this).IsContractBasedAPI && e.Row != null)
    {
      object valuePending = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.description>>) e).Cache.GetValuePending<PMChangeOrderLine.description>((object) e.Row);
      if (valuePending != null)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.description>, PMChangeOrderLine, object>) e).NewValue = valuePending;
        return;
      }
    }
    if (CostCodeAttribute.UseCostCode())
    {
      if (!e.Row.CostCodeID.HasValue)
        return;
      PMCostBudget pmCostBudget = (PMCostBudget) null;
      int? nullable = e.Row.TaskID;
      if (nullable.HasValue)
      {
        nullable = e.Row.AccountID;
        if (nullable.HasValue)
        {
          BudgetKeyTuple record;
          ref BudgetKeyTuple local = ref record;
          nullable = e.Row.ProjectID;
          int valueOrDefault1 = nullable.GetValueOrDefault();
          nullable = e.Row.TaskID;
          int valueOrDefault2 = nullable.GetValueOrDefault();
          nullable = this.GetProjectedAccountGroup(e.Row);
          int valueOrDefault3 = nullable.GetValueOrDefault();
          nullable = e.Row.InventoryID;
          int inventoryID = nullable ?? PMInventorySelectorAttribute.EmptyInventoryID;
          nullable = e.Row.CostCodeID;
          int costCodeID = nullable ?? CostCodeAttribute.GetDefaultCostCode();
          local = new BudgetKeyTuple(valueOrDefault1, valueOrDefault2, valueOrDefault3, inventoryID, costCodeID);
          pmCostBudget = this.GetOriginalCostBudget(record);
        }
      }
      if (pmCostBudget != null)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.description>, PMChangeOrderLine, object>) e).NewValue = (object) pmCostBudget.Description;
      }
      else
      {
        PMCostCode costCode = this.GetCostCode(e.Row.CostCodeID);
        if (costCode == null)
          return;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.description>, PMChangeOrderLine, object>) e).NewValue = (object) costCode.Description;
      }
    }
    else
    {
      if (!e.Row.InventoryID.HasValue)
        return;
      int? inventoryId = e.Row.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue)
        return;
      PX.Objects.IN.InventoryItem inventoryItem = this.GetInventoryItem(e.Row.InventoryID);
      if (inventoryItem == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.description>, PMChangeOrderLine, object>) e).NewValue = (object) inventoryItem.Descr;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.taxCategoryID> e)
  {
    if (!e.Row.InventoryID.HasValue)
      return;
    int? inventoryId = e.Row.InventoryID;
    int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
    if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = this.GetInventoryItem(e.Row.InventoryID);
    if (inventoryItem == null || inventoryItem.TaxCategoryID == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.taxCategoryID>, PMChangeOrderLine, object>) e).NewValue = (object) inventoryItem.TaxCategoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ChangeOrderEntry.POLineFilter, ChangeOrderEntry.POLineFilter.vendorID> e)
  {
    if (!e.Row.VendorID.HasValue)
      return;
    e.Row.POOrderNbr = (string) null;
  }

  public virtual void Persist()
  {
    this.ResetToStandardIfLastReferenceWasDeleted();
    this.SetToChangeOrderIfReferenceAdded();
    ((PXSelectBase) this.AvailableCostBudget).Cache.Clear();
    ((PXSelectBase) this.AvailableRevenueBudget).Cache.Clear();
    ((PXGraph) this).Persist();
  }

  private void SetToChangeOrderIfReferenceAdded()
  {
    foreach (PXResult<PMChangeOrderLine> pxResult in ((PXSelectBase<PMChangeOrderLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMChangeOrderLine pmChangeOrderLine = PXResult<PMChangeOrderLine>.op_Implicit(pxResult);
      if (pmChangeOrderLine.POLineNbr.HasValue)
        this.SetPOOrderBehavior(pmChangeOrderLine.POOrderType, pmChangeOrderLine.POOrderNbr, "C");
    }
  }

  private void ResetToStandardIfLastReferenceWasDeleted()
  {
    string str = (string) null;
    HashSet<ChangeOrderEntry.POLineKey> poLineKeySet = new HashSet<ChangeOrderEntry.POLineKey>();
    foreach (PMChangeOrderLine pmChangeOrderLine in ((PXSelectBase) this.Details).Cache.Deleted)
    {
      if (pmChangeOrderLine.POOrderType != null && pmChangeOrderLine.POOrderNbr != null)
      {
        ChangeOrderEntry.POLineKey poLineKey = new ChangeOrderEntry.POLineKey(pmChangeOrderLine.POOrderType, pmChangeOrderLine.POOrderNbr, 0);
        poLineKeySet.Add(poLineKey);
        str = pmChangeOrderLine.RefNbr;
      }
    }
    PXSelect<PMChangeOrderLine, Where<PMChangeOrderLine.pOOrderType, Equal<Required<PMChangeOrderLine.pOOrderType>>, And<PMChangeOrderLine.pOOrderNbr, Equal<Required<PMChangeOrderLine.pOOrderNbr>>, And<PMChangeOrderLine.refNbr, NotEqual<Required<PMChangeOrderLine.refNbr>>>>>> pxSelect = new PXSelect<PMChangeOrderLine, Where<PMChangeOrderLine.pOOrderType, Equal<Required<PMChangeOrderLine.pOOrderType>>, And<PMChangeOrderLine.pOOrderNbr, Equal<Required<PMChangeOrderLine.pOOrderNbr>>, And<PMChangeOrderLine.refNbr, NotEqual<Required<PMChangeOrderLine.refNbr>>>>>>((PXGraph) this);
    foreach (ChangeOrderEntry.POLineKey poLineKey in poLineKeySet)
    {
      if (PXResultset<PMChangeOrderLine>.op_Implicit(((PXSelectBase<PMChangeOrderLine>) pxSelect).SelectWindowed(0, 1, new object[3]
      {
        (object) poLineKey.OrderType,
        (object) poLineKey.OrderNbr,
        (object) str
      })) == null)
        this.SetPOOrderBehavior(poLineKey.OrderType, poLineKey.OrderNbr, "S");
    }
  }

  private void SetPOOrderBehavior(string orderType, string orderNbr, string behavior)
  {
    PX.Objects.PO.POOrder poOrder = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) new PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) orderType,
      (object) orderNbr
    }));
    if (poOrder == null || poOrder.Behavior == null || !(poOrder.Behavior != behavior))
      return;
    poOrder.Behavior = behavior;
    ((PXSelectBase<PX.Objects.PO.POOrder>) this.Order).Update(poOrder);
  }

  public virtual Dictionary<BudgetKeyTuple, Decimal> BuildBudgetStatsOnDraftChangeOrders()
  {
    Dictionary<BudgetKeyTuple, Decimal> dictionary = new Dictionary<BudgetKeyTuple, Decimal>();
    PXSelectGroupBy<PMChangeOrderBudget, Where<PMChangeOrderBudget.projectID, Equal<Current<PMChangeOrder.projectID>>, And<PMChangeOrderBudget.released, Equal<False>, And<PMChangeOrderBudget.refNbr, NotEqual<Current<PMChangeOrder.refNbr>>>>>, Aggregate<GroupBy<PMChangeOrderBudget.projectID, GroupBy<PMChangeOrderBudget.projectTaskID, GroupBy<PMChangeOrderBudget.accountGroupID, GroupBy<PMChangeOrderBudget.inventoryID, GroupBy<PMChangeOrderBudget.costCodeID, Sum<PMChangeOrderBudget.amount>>>>>>>> pxSelectGroupBy = new PXSelectGroupBy<PMChangeOrderBudget, Where<PMChangeOrderBudget.projectID, Equal<Current<PMChangeOrder.projectID>>, And<PMChangeOrderBudget.released, Equal<False>, And<PMChangeOrderBudget.refNbr, NotEqual<Current<PMChangeOrder.refNbr>>>>>, Aggregate<GroupBy<PMChangeOrderBudget.projectID, GroupBy<PMChangeOrderBudget.projectTaskID, GroupBy<PMChangeOrderBudget.accountGroupID, GroupBy<PMChangeOrderBudget.inventoryID, GroupBy<PMChangeOrderBudget.costCodeID, Sum<PMChangeOrderBudget.amount>>>>>>>>((PXGraph) this);
    using (new PXFieldScope(((PXSelectBase) pxSelectGroupBy).View, new System.Type[7]
    {
      typeof (PMChangeOrderBudget.projectID),
      typeof (PMChangeOrderBudget.projectTaskID),
      typeof (PMChangeOrderBudget.accountGroupID),
      typeof (PMChangeOrderBudget.inventoryID),
      typeof (PMChangeOrderBudget.costCodeID),
      typeof (PMChangeOrderBudget.amount),
      typeof (PMChangeOrderBudget.amount)
    }))
    {
      foreach (PXResult<PMChangeOrderBudget> pxResult in ((PXSelectBase<PMChangeOrderBudget>) pxSelectGroupBy).Select(Array.Empty<object>()))
      {
        PMChangeOrderBudget budget = PXResult<PMChangeOrderBudget>.op_Implicit(pxResult);
        dictionary.Add(BudgetKeyTuple.Create((IProjectFilter) budget), budget.Amount.GetValueOrDefault());
      }
    }
    return dictionary;
  }

  public virtual Decimal GetDraftChangeOrderBudgetAmount(PMChangeOrderBudget record)
  {
    if (!this.IsValidKey(record))
      return 0M;
    if (this.draftChangeOrderBudgetStats == null || this.draftChangeOrderBudgetStatsKey != record.RefNbr)
    {
      this.draftChangeOrderBudgetStats = this.BuildBudgetStatsOnDraftChangeOrders();
      this.draftChangeOrderBudgetStatsKey = record.RefNbr;
    }
    Decimal orderBudgetAmount = 0M;
    this.draftChangeOrderBudgetStats.TryGetValue(BudgetKeyTuple.Create((IProjectFilter) record), out orderBudgetAmount);
    return orderBudgetAmount;
  }

  public virtual int? DefaultAccountID(PMChangeOrderLine line)
  {
    if (line.POOrderType == "PD")
    {
      int? defaultAccountId = this.GetProjectDropshipLineDefaultAccountID(line);
      if (defaultAccountId.HasValue)
        return defaultAccountId;
    }
    int? nullable = new int?();
    PX.Objects.AP.Vendor vendor1 = (PX.Objects.AP.Vendor) null;
    if ((line?.LineType == "L" || line?.LineType == "D") && line.InventoryID.HasValue)
    {
      int? inventoryId = line.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue) && ((PXSelectBase<APSetup>) this.apSetup).Current.IntercompanyExpenseAccountDefault == "L" && (vendor1 = GetVendor()) != null && vendor1.IsBranch.GetValueOrDefault())
      {
        PX.Objects.CR.Location lineLocation = this.GetLineLocation(line);
        if (lineLocation != null && PXSelectorAttribute.Select<PX.Objects.CR.Location.vExpenseAcctID>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Location)], (object) lineLocation) is PX.Objects.GL.Account account && account.AccountGroupID.HasValue)
        {
          nullable = account.AccountID;
          goto label_14;
        }
        goto label_14;
      }
    }
    if (line.InventoryID.HasValue)
    {
      int? inventoryId = line.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue) && PXSelectorAttribute.Select<PMChangeOrderLine.inventoryID>(((PXSelectBase) this.Details).Cache, (object) line) is PX.Objects.IN.InventoryItem inventoryItem && PXSelectorAttribute.Select<PX.Objects.IN.InventoryItem.cOGSAcctID>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem) is PX.Objects.GL.Account account && account.AccountGroupID.HasValue)
        nullable = account.AccountID;
    }
    if (!nullable.HasValue && line.VendorID.HasValue)
    {
      PX.Objects.AP.Vendor vendor2 = vendor1 ?? GetVendor();
      if (vendor2 != null)
      {
        PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) vendor2.DefLocationID
        }));
        if (location != null && PXSelectorAttribute.Select<PX.Objects.CR.Location.vExpenseAcctID>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Location)], (object) location) is PX.Objects.GL.Account account && account.AccountGroupID.HasValue)
          nullable = account.AccountID;
      }
    }
label_14:
    return nullable;

    PX.Objects.AP.Vendor GetVendor() => PX.Objects.AP.Vendor.PK.Find((PXGraph) this, line.VendorID);
  }

  protected virtual int? GetProjectDropshipLineDefaultAccountID(PMChangeOrderLine line)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
    if (pmProject == null)
      return new int?();
    switch (pmProject.DropshipExpenseAccountSource)
    {
      case "T":
        return PMTask.PK.FindDirty((PXGraph) this, line.ProjectID, line.TaskID)?.DefaultExpenseAccountID;
      case "P":
        return pmProject?.DefaultExpenseAccountID;
      case "O":
        return this.GetExpenseAccountUsingDefaultRules(line);
      default:
        return new int?();
    }
  }

  protected virtual PX.Objects.CR.Location GetLineLocation(PMChangeOrderLine line)
  {
    PX.Objects.CR.Location lineLocation = (PX.Objects.CR.Location) null;
    if (line?.LineType == "D" && !string.IsNullOrEmpty(line.POOrderNbr))
      lineLocation = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXViewOf<PX.Objects.CR.Location>.BasedOn<SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POOrder>.On<BqlOperand<PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<PX.Objects.PO.POOrder.vendorLocationID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.orderType, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.PO.POOrder.orderNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
      {
        (object) line.POOrderType,
        (object) line.POOrderNbr
      }));
    if (line?.LineType == "L")
    {
      PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find((PXGraph) this, line.VendorID);
      if (vendor == null)
        return (PX.Objects.CR.Location) null;
      lineLocation = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXViewOf<PX.Objects.CR.Location>.BasedOn<SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) vendor.DefLocationID
      }));
    }
    return lineLocation;
  }

  protected virtual int? GetExpenseAccountUsingDefaultRules(PMChangeOrderLine line)
  {
    if (!line.InventoryID.HasValue)
      return new int?();
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, line.InventoryID);
    if (inventoryItem == null)
      return new int?();
    PX.Objects.CR.Location lineLocation = this.GetLineLocation(line);
    if (lineLocation == null)
      return new int?();
    INPostClass postclass = INPostClass.PK.Find((PXGraph) this, inventoryItem.PostClassID);
    if (postclass == null)
      return new int?();
    INSite site = INSite.PK.Find((PXGraph) this, lineLocation.VSiteID);
    try
    {
      PMProject project;
      PMTask task;
      PMProjectHelper.TryToGetProjectAndTask((PXGraph) this, (int?) line?.ProjectID, (int?) line?.TaskID, out project, out task);
      return INReleaseProcess.GetAcctID<INPostClass.cOGSAcctID>((PXGraph) this, postclass.COGSAcctDefault, InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
    }
    catch (PXMaskArgumentException ex)
    {
    }
    return new int?();
  }

  private ChangeOrderEntry.POLineKey GetKey(POLinePM record)
  {
    return new ChangeOrderEntry.POLineKey(record.OrderType, record.OrderNbr, record.LineNbr.Value);
  }

  private ChangeOrderEntry.POLineKey GetKey(PMChangeOrderLine record)
  {
    return new ChangeOrderEntry.POLineKey(record.POOrderType, record.POOrderNbr, record.POLineNbr.Value);
  }

  private bool IsValidKey(PMChangeOrderBudget record)
  {
    if (record == null)
      return false;
    int? nullable = record.CostCodeID;
    if (!nullable.HasValue)
      return false;
    nullable = record.InventoryID;
    if (!nullable.HasValue)
      return false;
    nullable = record.AccountGroupID;
    if (!nullable.HasValue)
      return false;
    nullable = record.TaskID;
    if (!nullable.HasValue)
      return false;
    nullable = record.ProjectID;
    return nullable.HasValue;
  }

  private bool IsValidKey(PMChangeOrderLine record)
  {
    return record != null && record.POLineNbr.HasValue && !string.IsNullOrEmpty(record.POOrderNbr) && !string.IsNullOrEmpty(record.POOrderType);
  }

  public virtual Dictionary<BudgetKeyTuple, PMCostBudget> BuildCostBudgetLookup()
  {
    Dictionary<BudgetKeyTuple, PMCostBudget> dictionary = new Dictionary<BudgetKeyTuple, PMCostBudget>();
    foreach (PXResult<PMCostBudget> pxResult in ((PXSelectBase<PMCostBudget>) new PXSelectReadonly<PMCostBudget, Where<PMCostBudget.projectID, Equal<Current<PMChangeOrder.projectID>>, And<PMCostBudget.type, Equal<AccountType.expense>>>>((PXGraph) this)).Select(Array.Empty<object>()))
    {
      PMCostBudget budget = PXResult<PMCostBudget>.op_Implicit(pxResult);
      PMCostCode costCode = this.GetCostCode(budget.CostCodeID);
      if (costCode == null || costCode.IsActive.GetValueOrDefault())
        dictionary.Add(BudgetKeyTuple.Create((IProjectFilter) budget), budget);
    }
    return dictionary;
  }

  public virtual Dictionary<BudgetKeyTuple, PMRevenueBudget> BuildRevenueBudgetLookup()
  {
    Dictionary<BudgetKeyTuple, PMRevenueBudget> dictionary = new Dictionary<BudgetKeyTuple, PMRevenueBudget>();
    foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) new PXSelectReadonly<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMChangeOrder.projectID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>((PXGraph) this)).Select(Array.Empty<object>()))
    {
      PMRevenueBudget budget = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
      PMCostCode costCode = this.GetCostCode(budget.CostCodeID);
      if (costCode == null || costCode.IsActive.GetValueOrDefault())
        dictionary.Add(BudgetKeyTuple.Create((IProjectFilter) budget), budget);
    }
    return dictionary;
  }

  public virtual Dictionary<BudgetKeyTuple, PMChangeOrderBudget> BuildPreviousTotals()
  {
    Dictionary<BudgetKeyTuple, PMChangeOrderBudget> dictionary = new Dictionary<BudgetKeyTuple, PMChangeOrderBudget>();
    PXSelectJoinGroupBy<PMChangeOrderBudget, InnerJoin<PMChangeOrder, On<PMChangeOrder.refNbr, Equal<PMChangeOrderBudget.refNbr>>>, Where<PMChangeOrderBudget.projectID, Equal<Current<PMChangeOrder.projectID>>, And<PMChangeOrder.released, Equal<True>, And<PMChangeOrder.reverseStatus, Equal<ChangeOrderReverseStatus.none>>>>, Aggregate<GroupBy<PMChangeOrderBudget.projectID, GroupBy<PMChangeOrderBudget.projectTaskID, GroupBy<PMChangeOrderBudget.accountGroupID, GroupBy<PMChangeOrderBudget.inventoryID, GroupBy<PMChangeOrderBudget.costCodeID, Sum<PMChangeOrderBudget.qty, Sum<PMChangeOrderBudget.amount>>>>>>>>> selectJoinGroupBy = new PXSelectJoinGroupBy<PMChangeOrderBudget, InnerJoin<PMChangeOrder, On<PMChangeOrder.refNbr, Equal<PMChangeOrderBudget.refNbr>>>, Where<PMChangeOrderBudget.projectID, Equal<Current<PMChangeOrder.projectID>>, And<PMChangeOrder.released, Equal<True>, And<PMChangeOrder.reverseStatus, Equal<ChangeOrderReverseStatus.none>>>>, Aggregate<GroupBy<PMChangeOrderBudget.projectID, GroupBy<PMChangeOrderBudget.projectTaskID, GroupBy<PMChangeOrderBudget.accountGroupID, GroupBy<PMChangeOrderBudget.inventoryID, GroupBy<PMChangeOrderBudget.costCodeID, Sum<PMChangeOrderBudget.qty, Sum<PMChangeOrderBudget.amount>>>>>>>>>((PXGraph) this);
    List<object> objectList = new List<object>();
    if (((PXSelectBase<PMChangeOrder>) this.Document).Current != null)
    {
      if (((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<PMChangeOrder>) this.Document).Current) == 2)
      {
        foreach (PMChangeOrder pmChangeOrder in ((PXSelectBase) this.Document).Cache.Updated)
        {
          if (pmChangeOrder.ReverseStatus == "X")
          {
            ((PXSelectBase<PMChangeOrderBudget>) selectJoinGroupBy).WhereAnd<Where<PMChangeOrderBudget.refNbr, NotEqual<Required<PMChangeOrderBudget.refNbr>>>>();
            objectList.Add((object) pmChangeOrder.RefNbr);
          }
        }
      }
      else
        ((PXSelectBase<PMChangeOrderBudget>) selectJoinGroupBy).WhereAnd<Where<PMChangeOrderBudget.refNbr, Less<Current<PMChangeOrder.refNbr>>>>();
    }
    foreach (PXResult<PMChangeOrderBudget> pxResult in ((PXSelectBase<PMChangeOrderBudget>) selectJoinGroupBy).Select(objectList.ToArray()))
    {
      PMChangeOrderBudget budget = PXResult<PMChangeOrderBudget>.op_Implicit(pxResult);
      dictionary.Add(BudgetKeyTuple.Create((IProjectFilter) budget), budget);
    }
    return dictionary;
  }

  public virtual POLinePM GetPOLine(PMChangeOrderLine line)
  {
    POLinePM poLine = (POLinePM) null;
    if (this.IsValidKey(line))
    {
      ChangeOrderEntry.POLineKey key = this.GetKey(line);
      if (this.polines != null)
        this.polines.TryGetValue(key, out poLine);
      else
        this.polines = new Dictionary<ChangeOrderEntry.POLineKey, POLinePM>();
      if (poLine == null)
      {
        poLine = PXResultset<POLinePM>.op_Implicit(PXSelectBase<POLinePM, PXSelect<POLinePM, Where<POLinePM.orderType, Equal<Required<POLinePM.orderType>>, And<POLinePM.orderNbr, Equal<Required<POLinePM.orderNbr>>, And<POLinePM.lineNbr, Equal<Required<POLinePM.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) key.OrderType,
          (object) key.OrderNbr,
          (object) key.LineNbr
        }));
        if (poLine != null)
          this.polines.Add(key, poLine);
      }
    }
    return poLine;
  }

  public virtual PMCostBudget GetOriginalCostBudget(BudgetKeyTuple record)
  {
    if (this.costBudgets == null || this.IsCacheUpdateRequired())
      this.costBudgets = this.BuildCostBudgetLookup();
    PMCostBudget originalCostBudget = (PMCostBudget) null;
    this.costBudgets.TryGetValue(record, out originalCostBudget);
    return originalCostBudget;
  }

  public virtual PMRevenueBudget GetOriginalRevenueBudget(BudgetKeyTuple record)
  {
    if (this.revenueBudgets == null || this.IsCacheUpdateRequired())
      this.revenueBudgets = this.BuildRevenueBudgetLookup();
    PMRevenueBudget originalRevenueBudget = (PMRevenueBudget) null;
    this.revenueBudgets.TryGetValue(record, out originalRevenueBudget);
    return originalRevenueBudget;
  }

  public virtual PMChangeOrderBudget GetPreviousTotals(BudgetKeyTuple record)
  {
    if (this.previousTotals == null || this.IsCacheUpdateRequired())
      this.previousTotals = this.BuildPreviousTotals();
    PMChangeOrderBudget previousTotals = (PMChangeOrderBudget) null;
    this.previousTotals.TryGetValue(record, out previousTotals);
    return previousTotals;
  }

  public virtual ICollection<PMCostBudget> GetCostBudget()
  {
    if (this.costBudgets == null || this.IsCacheUpdateRequired())
      this.costBudgets = this.BuildCostBudgetLookup();
    return (ICollection<PMCostBudget>) this.costBudgets.Values;
  }

  public virtual ICollection<PMRevenueBudget> GetRevenueBudget()
  {
    if (this.revenueBudgets == null || this.IsCacheUpdateRequired())
      this.revenueBudgets = this.BuildRevenueBudgetLookup();
    return (ICollection<PMRevenueBudget>) this.revenueBudgets.Values;
  }

  public virtual void ReleaseDocuments(IEnumerable<PMChangeOrder> docs)
  {
    foreach (PMChangeOrder doc in docs)
      this.ReleaseDocument(doc);
  }

  public virtual void ReleaseDocument(PMChangeOrder doc)
  {
    if (doc.Released.GetValueOrDefault())
      return;
    ((PXSelectBase<PMChangeOrder>) this.Document).Current = doc;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) this, doc.ProjectID);
      if (pmProject != null && (pmProject.Status == "F" || pmProject.Status == "C" || pmProject.Status == "E"))
        throw new PXException("A change order cannot be released for the project with the {0} status.", new object[1]
        {
          (object) new ProjectStatus.ListAttribute().ValueLabelDic[pmProject.Status]
        });
      this.ValidateOrdersTotal(doc);
      foreach (PXResult<PMChangeOrderCostBudget> pxResult in ((PXSelectBase<PMChangeOrderCostBudget>) this.CostBudget).Select(Array.Empty<object>()))
      {
        PMChangeOrderCostBudget row = PXResult<PMChangeOrderCostBudget>.op_Implicit(pxResult);
        if (!row.Released.GetValueOrDefault())
        {
          this.ValidateBudgetRow((PMChangeOrderBudget) row);
          this.ApplyChangeOrderBudget((PMChangeOrderBudget) row, doc);
          ((PXSelectBase) this.CostBudget).Cache.SetValue<PMChangeOrderBudget.released>((object) row, (object) true);
          GraphHelper.MarkUpdated(((PXSelectBase) this.CostBudget).Cache, (object) row, true);
        }
      }
      foreach (PXResult<PMChangeOrderRevenueBudget> pxResult in ((PXSelectBase<PMChangeOrderRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()))
      {
        PMChangeOrderRevenueBudget row = PXResult<PMChangeOrderRevenueBudget>.op_Implicit(pxResult);
        if (!row.Released.GetValueOrDefault())
        {
          this.ValidateBudgetRow((PMChangeOrderBudget) row);
          this.ApplyChangeOrderBudget((PMChangeOrderBudget) row, doc);
          ((PXSelectBase) this.RevenueBudget).Cache.SetValue<PMChangeOrderBudget.released>((object) row, (object) true);
          GraphHelper.MarkUpdated(((PXSelectBase) this.RevenueBudget).Cache, (object) row, true);
        }
      }
      this.ReleaseLineChanges();
      foreach (PXResult<PMChangeOrderLine> pxResult in ((PXSelectBase<PMChangeOrderLine>) this.Details).Select(Array.Empty<object>()))
      {
        PMChangeOrderLine pmChangeOrderLine = PXResult<PMChangeOrderLine>.op_Implicit(pxResult);
        if (!pmChangeOrderLine.Released.GetValueOrDefault())
        {
          ((PXSelectBase) this.Details).Cache.SetValue<PMChangeOrderLine.released>((object) pmChangeOrderLine, (object) true);
          GraphHelper.MarkUpdated(((PXSelectBase) this.Details).Cache, (object) pmChangeOrderLine, true);
        }
      }
      ((PXAction) this.Save).Press();
      transactionScope.Complete();
    }
  }

  protected virtual void ValidateBudgetRow(PMChangeOrderBudget row)
  {
    if (row == null)
      return;
    PMCostCode pmCostCode = PMCostCode.PK.Find((PXGraph) this, row.CostCodeID);
    bool? isActive;
    if (pmCostCode != null)
    {
      isActive = pmCostCode.IsActive;
      if (!isActive.GetValueOrDefault())
        throw new PXException("The {0} cost code is inactive.", new object[1]
        {
          (object) pmCostCode.CostCodeCD
        });
    }
    PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, row.AccountGroupID);
    if (pmAccountGroup == null)
      return;
    isActive = pmAccountGroup.IsActive;
    if (!isActive.GetValueOrDefault())
      throw new PXException("The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new object[1]
      {
        (object) pmAccountGroup.GroupCD
      });
  }

  protected virtual void ValidatePOLinesBeforeOpen()
  {
    if (!((PXSelectBase<PMSetup>) this.Setup).Current.CostCommitmentTracking.GetValueOrDefault())
      return;
    List<PXException> source = new List<PXException>();
    foreach (PXResult<PMChangeOrderLine> pxResult in ((PXSelectBase<PMChangeOrderLine>) this.Details).Select(Array.Empty<object>()))
    {
      PXException pxException = this.ValidateLineAccount(((PXSelectBase) this.Details).Cache, PXResult<PMChangeOrderLine>.op_Implicit(pxResult));
      if (pxException != null)
        source.Add(pxException);
    }
    if (source.Any<PXException>())
      throw new PXException("A project commitment cannot be created for at least one document line. For details, see the trace log.");
  }

  public virtual PXException ValidateLineAccount(PXCache cache, PMChangeOrderLine line)
  {
    if (!line.AccountID.HasValue)
      return (PXException) null;
    if (!((IEnumerable<string>) this.LineTypesToValidate).Contains<string>(line.LineType))
      return (PXException) null;
    if (!ProjectDefaultAttribute.IsProject((PXGraph) this, line.ProjectID))
      return (PXException) null;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, (int?) line?.AccountID);
    if (account == null || account.AccountGroupID.HasValue)
      return (PXException) null;
    PXSetPropertyException<PMChangeOrderLine.accountID> propertyException = new PXSetPropertyException<PMChangeOrderLine.accountID>("The account specified in the project-related line must be mapped to an account group. Assign an account group to the {0} account and recalculate the project balance for the {1} project.", (PXErrorLevel) 4, new object[2]
    {
      (object) account.AccountCD,
      (object) ((PXSelectBase<PMProject>) this.Project).Current?.ContractCD
    });
    PXTrace.WriteError((Exception) propertyException);
    cache.RaiseExceptionHandling<PMChangeOrderLine.accountID>((object) line, (object) account.AccountCD, (Exception) propertyException);
    return (PXException) propertyException;
  }

  public virtual void ValidateOrdersTotal(PMChangeOrder doc)
  {
    Dictionary<Tuple<int?, string, string>, Decimal> dictionary1 = new Dictionary<Tuple<int?, string, string>, Decimal>();
    Dictionary<Tuple<int?, string, string>, Decimal> dictionary2 = new Dictionary<Tuple<int?, string, string>, Decimal>();
    FbqlSelect<SelectFromBase<PX.Objects.PO.POOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.orderType, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.PO.POOrder.orderNbr, IBqlString>.IsEqual<P.AsString>>>, PX.Objects.PO.POOrder>.View view1 = new FbqlSelect<SelectFromBase<PX.Objects.PO.POOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.orderType, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.PO.POOrder.orderNbr, IBqlString>.IsEqual<P.AsString>>>, PX.Objects.PO.POOrder>.View((PXGraph) this);
    FbqlSelect<SelectFromBase<PMChangeOrderLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<PMChangeOrderLine.refNbr, IBqlString>.IsEqual<PMChangeOrder.refNbr>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrderLine.pOOrderType, Equal<P.AsString>>>>, And<BqlOperand<PMChangeOrderLine.pOOrderNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.status, Equal<ChangeOrderStatus.onHold>>>>, Or<BqlOperand<PMChangeOrder.status, IBqlString>.IsEqual<ChangeOrderStatus.pendingApproval>>>>.Or<BqlOperand<PMChangeOrder.status, IBqlString>.IsEqual<ChangeOrderStatus.open>>>>>.And<BqlOperand<PMChangeOrderLine.refNbr, IBqlString>.IsNotEqual<P.AsString>>>.Aggregate<To<Sum<PMChangeOrderLine.amount>>>, PMChangeOrderLine>.View view2 = new FbqlSelect<SelectFromBase<PMChangeOrderLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<PMChangeOrderLine.refNbr, IBqlString>.IsEqual<PMChangeOrder.refNbr>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrderLine.pOOrderType, Equal<P.AsString>>>>, And<BqlOperand<PMChangeOrderLine.pOOrderNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.status, Equal<ChangeOrderStatus.onHold>>>>, Or<BqlOperand<PMChangeOrder.status, IBqlString>.IsEqual<ChangeOrderStatus.pendingApproval>>>>.Or<BqlOperand<PMChangeOrder.status, IBqlString>.IsEqual<ChangeOrderStatus.open>>>>>.And<BqlOperand<PMChangeOrderLine.refNbr, IBqlString>.IsNotEqual<P.AsString>>>.Aggregate<To<Sum<PMChangeOrderLine.amount>>>, PMChangeOrderLine>.View((PXGraph) this);
    foreach (PXResult<PMChangeOrderLine> pxResult in ((PXSelectBase<PMChangeOrderLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMChangeOrderLine pmChangeOrderLine = PXResult<PMChangeOrderLine>.op_Implicit(pxResult);
      if (!pmChangeOrderLine.Released.GetValueOrDefault())
      {
        Tuple<int?, string, string> key1 = new Tuple<int?, string, string>(pmChangeOrderLine.VendorID, pmChangeOrderLine.POOrderType, pmChangeOrderLine.POOrderNbr ?? string.Empty);
        Decimal? nullable;
        if (!dictionary1.ContainsKey(key1))
        {
          if (pmChangeOrderLine.LineType == "L")
          {
            dictionary1[key1] = 0M;
            dictionary2[key1] = 0M;
          }
          else
          {
            using (new PXFieldScope(((PXSelectBase) view1).View, new System.Type[4]
            {
              typeof (PX.Objects.PO.POOrder.orderType),
              typeof (PX.Objects.PO.POOrder.orderNbr),
              typeof (PX.Objects.PO.POOrder.curyUnbilledOrderTotal),
              typeof (PX.Objects.PO.POOrder.unbilledOrderQty)
            }))
            {
              PX.Objects.PO.POOrder poOrder = ((PXSelectBase<PX.Objects.PO.POOrder>) view1).SelectSingle(new object[2]
              {
                (object) pmChangeOrderLine.POOrderType,
                (object) pmChangeOrderLine.POOrderNbr
              });
              Dictionary<Tuple<int?, string, string>, Decimal> dictionary3 = dictionary1;
              Tuple<int?, string, string> key2 = key1;
              nullable = poOrder.CuryUnbilledOrderTotal;
              Decimal valueOrDefault1 = nullable.GetValueOrDefault();
              dictionary3[key2] = valueOrDefault1;
              Dictionary<Tuple<int?, string, string>, Decimal> dictionary4 = dictionary2;
              Tuple<int?, string, string> key3 = key1;
              nullable = poOrder.UnbilledOrderQty;
              Decimal valueOrDefault2 = nullable.GetValueOrDefault();
              dictionary4[key3] = valueOrDefault2;
            }
            Dictionary<Tuple<int?, string, string>, Decimal> dictionary5 = dictionary1;
            Tuple<int?, string, string> key4 = key1;
            Dictionary<Tuple<int?, string, string>, Decimal> dictionary6 = dictionary5;
            Tuple<int?, string, string> key5 = key4;
            Decimal num1 = dictionary5[key4];
            nullable = ((PXSelectBase<PMChangeOrderLine>) view2).SelectSingle(new object[3]
            {
              (object) pmChangeOrderLine.POOrderType,
              (object) pmChangeOrderLine.POOrderNbr,
              (object) doc.RefNbr
            }).Amount;
            Decimal valueOrDefault = nullable.GetValueOrDefault();
            Decimal num2 = num1 + valueOrDefault;
            dictionary6[key5] = num2;
          }
        }
        Dictionary<Tuple<int?, string, string>, Decimal> dictionary7 = dictionary1;
        Tuple<int?, string, string> key6 = key1;
        Dictionary<Tuple<int?, string, string>, Decimal> dictionary8 = dictionary7;
        Tuple<int?, string, string> key7 = key6;
        Decimal num3 = dictionary7[key6];
        nullable = pmChangeOrderLine.Amount;
        Decimal valueOrDefault3 = nullable.GetValueOrDefault();
        Decimal num4 = num3 + valueOrDefault3;
        dictionary8[key7] = num4;
        Dictionary<Tuple<int?, string, string>, Decimal> dictionary9 = dictionary2;
        Tuple<int?, string, string> key8 = key1;
        Dictionary<Tuple<int?, string, string>, Decimal> dictionary10 = dictionary9;
        Tuple<int?, string, string> key9 = key8;
        Decimal num5 = dictionary9[key8];
        nullable = pmChangeOrderLine.Qty;
        Decimal valueOrDefault4 = nullable.GetValueOrDefault();
        Decimal num6 = num5 + valueOrDefault4;
        dictionary10[key9] = num6;
      }
    }
    foreach (KeyValuePair<Tuple<int?, string, string>, Decimal> keyValuePair in dictionary1)
    {
      if (keyValuePair.Value < 0M)
      {
        if (keyValuePair.Key.Item3 == string.Empty)
          throw new PXException("The change order cannot be released because the total amount of the lines with the New Document status on the Commitments tab must be greater than or equal to zero.");
        throw new PXException(keyValuePair.Key.Item2 == "RS" ? "The change order cannot be released because the total unbilled amount of the related subcontract ({0}) will become negative." : "The change order cannot be released because the total unbilled amount of the related purchase order ({0}) will become negative.", new object[1]
        {
          (object) keyValuePair.Key.Item3
        });
      }
    }
    foreach (KeyValuePair<Tuple<int?, string, string>, Decimal> keyValuePair in dictionary2)
    {
      if (keyValuePair.Value < 0M)
      {
        if (keyValuePair.Key.Item3 == string.Empty)
          throw new PXException("The quantity of a line with the New Document status cannot be negative.");
        throw new PXException(keyValuePair.Key.Item2 == "RS" ? "The change order cannot be released because the total unbilled quantity of the related subcontract ({0}) will become negative." : "The value has to be greater than 0.", new object[1]
        {
          (object) keyValuePair.Key.Item3
        });
      }
    }
  }

  public virtual void ReverseDocument()
  {
    if (((PXSelectBase<PMChangeOrder>) this.Document).Current == null)
      return;
    PMChangeOrder copy1 = (PMChangeOrder) ((PXSelectBase) this.Document).Cache.CreateCopy((object) ((PXSelectBase<PMChangeOrder>) this.Document).Current);
    copy1.ReverseStatus = "X";
    ((PXSelectBase<PMChangeOrder>) this.Document).Update(copy1);
    List<PMChangeOrderRevenueBudget> orderRevenueBudgetList = new List<PMChangeOrderRevenueBudget>();
    foreach (PXResult<PMChangeOrderRevenueBudget> pxResult in ((PXSelectBase<PMChangeOrderRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()))
    {
      PMChangeOrderRevenueBudget copy2 = (PMChangeOrderRevenueBudget) ((PXSelectBase) this.RevenueBudget).Cache.CreateCopy((object) PXResult<PMChangeOrderRevenueBudget>.op_Implicit(pxResult));
      orderRevenueBudgetList.Add(copy2);
    }
    List<PMChangeOrderCostBudget> changeOrderCostBudgetList = new List<PMChangeOrderCostBudget>();
    foreach (PXResult<PMChangeOrderCostBudget> pxResult in ((PXSelectBase<PMChangeOrderCostBudget>) this.CostBudget).Select(Array.Empty<object>()))
    {
      PMChangeOrderCostBudget copy3 = (PMChangeOrderCostBudget) ((PXSelectBase) this.CostBudget).Cache.CreateCopy((object) PXResult<PMChangeOrderCostBudget>.op_Implicit(pxResult));
      changeOrderCostBudgetList.Add(copy3);
    }
    List<PXResult<PMChangeOrderLine, POLinePM>> pxResultList = new List<PXResult<PMChangeOrderLine, POLinePM>>();
    foreach (PXResult<PMChangeOrderLine, POLinePM> pxResult in ((PXSelectBase<PMChangeOrderLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMChangeOrderLine pmChangeOrderLine = PXResult<PMChangeOrderLine, POLinePM>.op_Implicit(pxResult);
      POLinePM poLinePm = PXResult<PMChangeOrderLine, POLinePM>.op_Implicit(pxResult);
      PMChangeOrderLine copy4 = (PMChangeOrderLine) ((PXSelectBase) this.Details).Cache.CreateCopy((object) pmChangeOrderLine);
      pxResultList.Add(new PXResult<PMChangeOrderLine, POLinePM>(copy4, poLinePm));
    }
    copy1.OrigRefNbr = copy1.RefNbr;
    copy1.RefNbr = (string) null;
    copy1.ExtRefNbr = (string) null;
    copy1.ProjectNbr = "N/A";
    copy1.Released = new bool?(false);
    copy1.ReverseStatus = "R";
    copy1.Hold = new bool?(true);
    copy1.Approved = new bool?(false);
    copy1.Status = "H";
    copy1.LineCntr = new int?(0);
    copy1.BudgetLineCntr = new int?(0);
    copy1.CommitmentTotal = new Decimal?(0M);
    copy1.RevenueTotal = new Decimal?(0M);
    copy1.CostTotal = new Decimal?(0M);
    copy1.NoteID = new Guid?(Guid.NewGuid());
    PMChangeOrder pmChangeOrder = ((PXSelectBase<PMChangeOrder>) this.Document).Insert(copy1);
    foreach (PXResult<CSAnswers> pxResult in this.Answers.Select(Array.Empty<object>()))
    {
      CSAnswers csAnswers1 = PXResult<CSAnswers>.op_Implicit(pxResult);
      CSAnswers csAnswers2 = this.Answers.Insert(new CSAnswers()
      {
        RefNoteID = pmChangeOrder.NoteID,
        AttributeID = csAnswers1.AttributeID
      });
      if (csAnswers2 != null)
        csAnswers2.Value = csAnswers1.Value;
    }
    foreach (PMChangeOrderRevenueBudget orderRevenueBudget in orderRevenueBudgetList)
    {
      orderRevenueBudget.RefNbr = pmChangeOrder.RefNbr;
      orderRevenueBudget.Released = new bool?(false);
      orderRevenueBudget.Amount = new Decimal?(-orderRevenueBudget.Amount.GetValueOrDefault());
      orderRevenueBudget.Qty = new Decimal?(-orderRevenueBudget.Qty.GetValueOrDefault());
      orderRevenueBudget.NoteID = new Guid?();
      orderRevenueBudget.IsDisabled = new bool?(false);
      ((PXSelectBase<PMChangeOrderRevenueBudget>) this.RevenueBudget).Insert(orderRevenueBudget);
    }
    foreach (PMChangeOrderCostBudget changeOrderCostBudget in changeOrderCostBudgetList)
    {
      changeOrderCostBudget.RefNbr = pmChangeOrder.RefNbr;
      changeOrderCostBudget.Released = new bool?(false);
      changeOrderCostBudget.Amount = new Decimal?(-changeOrderCostBudget.Amount.GetValueOrDefault());
      changeOrderCostBudget.Qty = new Decimal?(-changeOrderCostBudget.Qty.GetValueOrDefault());
      changeOrderCostBudget.NoteID = new Guid?();
      changeOrderCostBudget.IsDisabled = new bool?(false);
      ((PXSelectBase<PMChangeOrderCostBudget>) this.CostBudget).Insert(changeOrderCostBudget);
    }
    foreach (PXResult<PMChangeOrderLine, POLinePM> pxResult in pxResultList)
    {
      PMChangeOrderLine pmChangeOrderLine1 = PXResult<PMChangeOrderLine, POLinePM>.op_Implicit(pxResult);
      POLinePM poLinePm = PXResult<PMChangeOrderLine, POLinePM>.op_Implicit(pxResult);
      pmChangeOrderLine1.LineType = "U";
      pmChangeOrderLine1.RefNbr = pmChangeOrder.RefNbr;
      pmChangeOrderLine1.LineNbr = new int?();
      pmChangeOrderLine1.Released = new bool?(false);
      PMChangeOrderLine pmChangeOrderLine2 = pmChangeOrderLine1;
      Decimal? nullable1 = pmChangeOrderLine1.Amount;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = poLinePm.CalcCuryOpenAmt;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal? nullable2 = new Decimal?(-Math.Min(valueOrDefault1, valueOrDefault2));
      pmChangeOrderLine2.Amount = nullable2;
      PMChangeOrderLine pmChangeOrderLine3 = pmChangeOrderLine1;
      nullable1 = pmChangeOrderLine1.Qty;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      nullable1 = poLinePm.CalcOpenQty;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      Decimal? nullable3 = new Decimal?(-Math.Min(valueOrDefault3, valueOrDefault4));
      pmChangeOrderLine3.Qty = nullable3;
      PMChangeOrderLine pmChangeOrderLine4 = pmChangeOrderLine1;
      nullable1 = new Decimal?();
      Decimal? nullable4 = nullable1;
      pmChangeOrderLine4.AmountInProjectCury = nullable4;
      PMChangeOrderLine pmChangeOrderLine5 = pmChangeOrderLine1;
      nullable1 = new Decimal?();
      Decimal? nullable5 = nullable1;
      pmChangeOrderLine5.RetainageAmt = nullable5;
      PMChangeOrderLine pmChangeOrderLine6 = pmChangeOrderLine1;
      nullable1 = new Decimal?();
      Decimal? nullable6 = nullable1;
      pmChangeOrderLine6.RetainageAmtInProjectCury = nullable6;
      pmChangeOrderLine1.NoteID = new Guid?();
      ((PXSelectBase<PMChangeOrderLine>) this.Details).Insert(pmChangeOrderLine1);
    }
  }

  public virtual void ReleaseLineChanges()
  {
    Dictionary<string, PX.Objects.PO.POOrder> dictionary1 = new Dictionary<string, PX.Objects.PO.POOrder>();
    Dictionary<string, List<PMChangeOrderLine>> dictionary2 = new Dictionary<string, List<PMChangeOrderLine>>();
    Dictionary<string, List<PMChangeOrderLine>> dictionary3 = new Dictionary<string, List<PMChangeOrderLine>>();
    SortedList<string, PX.Objects.PO.POOrder> sortedList1 = new SortedList<string, PX.Objects.PO.POOrder>();
    SortedList<string, List<PMChangeOrderLine>> sortedList2 = new SortedList<string, List<PMChangeOrderLine>>();
    foreach (PXResult<PMChangeOrderLine, POLinePM> pxResult in ((PXSelectBase<PMChangeOrderLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMChangeOrderLine line = PXResult<PMChangeOrderLine, POLinePM>.op_Implicit(pxResult);
      POLinePM poLinePm = PXResult<PMChangeOrderLine, POLinePM>.op_Implicit(pxResult);
      if (!line.Released.GetValueOrDefault())
      {
        PX.Objects.PO.POOrder orderFromChangedLine = this.CreatePOOrderFromChangedLine(line);
        string poOrderKey = this.CreatePOOrderKey(orderFromChangedLine);
        if (string.IsNullOrEmpty(orderFromChangedLine.OrderNbr))
        {
          if (!sortedList1.ContainsKey(poOrderKey))
          {
            sortedList1.Add(poOrderKey, orderFromChangedLine);
            sortedList2.Add(poOrderKey, new List<PMChangeOrderLine>());
          }
          sortedList2[poOrderKey].Add(line);
        }
        else
        {
          if (!dictionary1.ContainsKey(poOrderKey))
          {
            dictionary1.Add(poOrderKey, orderFromChangedLine);
            dictionary2.Add(poOrderKey, new List<PMChangeOrderLine>());
            dictionary3.Add(poOrderKey, new List<PMChangeOrderLine>());
          }
          if (poLinePm.LineNbr.HasValue)
            dictionary2[poOrderKey].Add(line);
          else
            dictionary3[poOrderKey].Add(line);
        }
      }
    }
    foreach (KeyValuePair<string, PX.Objects.PO.POOrder> keyValuePair in dictionary1)
      this.ModifyExistingOrder(keyValuePair.Value, dictionary2[keyValuePair.Key], dictionary3[keyValuePair.Key]);
    foreach (KeyValuePair<string, PX.Objects.PO.POOrder> keyValuePair in sortedList1)
    {
      PX.Objects.PO.POOrder newOrder = this.CreateNewOrder(keyValuePair.Value, sortedList2[keyValuePair.Key]);
      foreach (PMChangeOrderLine line in sortedList2[keyValuePair.Key])
        this.SetReferences(line, newOrder);
    }
  }

  public virtual void ModifyExistingOrder(
    PX.Objects.PO.POOrder order,
    List<PMChangeOrderLine> updated,
    List<PMChangeOrderLine> added)
  {
    POOrderEntry target = this.CreateTarget(order);
    ((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).Current = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).Search<PX.Objects.PO.POOrder.orderNbr>((object) order.OrderNbr, new object[1]
    {
      (object) order.OrderType
    }));
    ((PXGraph) target).GetExtension<POOrderEntryExt>().SkipProjectLockCommitmentsVerification = true;
    if (updated.Count > 0)
      EnumerableExtensions.Consume<PXResult<PX.Objects.PO.POLine>>((IEnumerable<PXResult<PX.Objects.PO.POLine>>) ((PXSelectBase<PX.Objects.PO.POLine>) target.Transactions).Select(Array.Empty<object>()));
    PXGraph.FieldUpdatedEvents fieldUpdated = ((PXGraph) target).FieldUpdated;
    System.Type type = typeof (PX.Objects.PO.POOrder);
    string name = typeof (PX.Objects.PO.POOrder.cancelled).Name;
    POOrderEntry poOrderEntry = target;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) poOrderEntry, __vmethodptr(poOrderEntry, POOrder_Cancelled_FieldUpdated));
    fieldUpdated.RemoveHandler(type, name, pxFieldUpdated);
    ((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).Current.LockCommitment = new bool?(true);
    POSetup current = ((PXSelectBase<POSetup>) target.POSetup).Current;
    current.RequireOrderControlTotal = new bool?(false);
    current.RequireBlanketControlTotal = new bool?(false);
    current.RequireDropShipControlTotal = new bool?(false);
    current.RequireProjectDropShipControlTotal = new bool?(false);
    ((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).Update(((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).Current);
    List<PX.Objects.PO.POLine> source = new List<PX.Objects.PO.POLine>(updated.Count + added.Count);
    foreach (PMChangeOrderLine line in updated)
      source.Add(this.ModifyExistsingLineInOrder(target, line));
    foreach (PMChangeOrderLine line in added)
      source.Add(this.AddNewLineToOrder(target, line));
    if (source.Any<PX.Objects.PO.POLine>((Func<PX.Objects.PO.POLine, bool>) (x => x.RetainagePct.GetValueOrDefault() != 0M)))
      ((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).Current.RetainageApply = new bool?(true);
    if (added.Count > 0)
      ((SelectedEntityEvent<PX.Objects.PO.POOrder>) PXEntityEventBase<PX.Objects.PO.POOrder>.Container<PX.Objects.PO.POOrder.Events>.Select((Expression<Func<PX.Objects.PO.POOrder.Events, PXEntityEvent<PX.Objects.PO.POOrder.Events>>>) (ev => ev.LinesReopened))).FireOn((PXGraph) target, ((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).Current);
    ((SelectedEntityEvent<PX.Objects.PO.POOrder>) PXEntityEventBase<PX.Objects.PO.POOrder>.Container<PX.Objects.PO.POOrder.Events>.Select((Expression<Func<PX.Objects.PO.POOrder.Events, PXEntityEvent<PX.Objects.PO.POOrder.Events>>>) (ev => ev.ReleaseChangeOrder))).FireOn((PXGraph) target, ((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).Current);
    ((PXAction) target.Save).Press();
  }

  protected virtual PX.Objects.PO.POLine ModifyExistsingLineInOrder(
    POOrderEntry target,
    PMChangeOrderLine line)
  {
    PX.Objects.PO.POLine poLine1 = new PX.Objects.PO.POLine()
    {
      OrderType = line.POOrderType,
      OrderNbr = line.POOrderNbr,
      LineNbr = line.POLineNbr
    };
    PX.Objects.PO.POLine poLine2 = (PX.Objects.PO.POLine) ((PXSelectBase) target.Transactions).Cache.Locate((object) poLine1);
    if (!poLine2.OrigExtCost.HasValue)
    {
      poLine2.OrigOrderQty = poLine2.OrderQty;
      poLine2.OrigExtCost = poLine2.CuryLineAmt;
    }
    Decimal num1 = poLine2.CuryLineAmt.GetValueOrDefault() + line.Amount.GetValueOrDefault();
    bool? nullable1 = poLine2.Cancelled;
    int num2;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = poLine2.Completed;
      num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 1;
    poLine2.ManualPrice = new bool?(true);
    poLine2.Cancelled = new bool?(false);
    poLine2.Completed = new bool?(false);
    PX.Objects.PO.POLine poLine3 = ((PXSelectBase<PX.Objects.PO.POLine>) target.Transactions).Update(poLine2);
    Decimal? nullable2 = poLine3.OrderQty;
    Decimal? nullable3 = line.Qty;
    Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
    Decimal? nullable4;
    if (!nullable2.HasValue)
    {
      nullable3 = new Decimal?();
      nullable4 = nullable3;
    }
    else
      nullable4 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault1);
    Decimal? nullable5 = nullable4;
    Decimal num3 = 0M;
    if (nullable5.GetValueOrDefault() == num3 & nullable5.HasValue)
    {
      Decimal? orderQty = poLine3.OrderQty;
      Decimal num4 = 0M;
      if (!(orderQty.GetValueOrDefault() == num4 & orderQty.HasValue))
      {
        poLine3.Cancelled = new bool?(true);
        goto label_15;
      }
    }
    PX.Objects.PO.POLine poLine4 = poLine3;
    Decimal? orderQty1 = poLine4.OrderQty;
    nullable2 = line.Qty;
    Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
    Decimal? nullable6;
    if (!orderQty1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable6 = nullable2;
    }
    else
      nullable6 = new Decimal?(orderQty1.GetValueOrDefault() + valueOrDefault2);
    poLine4.OrderQty = nullable6;
label_15:
    poLine3.CuryUnitCost = line.UnitCost;
    PX.Objects.PO.POLine poLine5 = ((PXSelectBase<PX.Objects.PO.POLine>) target.Transactions).Update(poLine3);
    poLine5.CuryLineAmt = new Decimal?(num1);
    PX.Objects.PO.POLine poLine6 = ((PXSelectBase<PX.Objects.PO.POLine>) target.Transactions).Update(poLine5);
    if (num2 != 0)
    {
      nullable1 = poLine6.Cancelled;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = poLine6.Completed;
        if (!nullable1.GetValueOrDefault())
          ((SelectedEntityEvent<PX.Objects.PO.POOrder>) PXEntityEventBase<PX.Objects.PO.POOrder>.Container<PX.Objects.PO.POOrder.Events>.Select((Expression<Func<PX.Objects.PO.POOrder.Events, PXEntityEvent<PX.Objects.PO.POOrder.Events>>>) (ev => ev.LinesReopened))).FireOn((PXGraph) target, ((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).Current);
      }
    }
    return poLine6;
  }

  protected virtual PX.Objects.PO.POLine AddNewLineToOrder(
    POOrderEntry target,
    PMChangeOrderLine line)
  {
    PX.Objects.PO.POLine fromChangeOrderLine = this.CreatePOLineFromChangeOrderLine(line);
    Decimal valueOrDefault = fromChangeOrderLine.CuryLineAmt.GetValueOrDefault();
    fromChangeOrderLine.CuryLineAmt = new Decimal?();
    PX.Objects.PO.POLine poLine = ((PXSelectBase<PX.Objects.PO.POLine>) target.Transactions).Insert(fromChangeOrderLine);
    ((PXSelectBase) this.Details).Cache.SetValue<PMChangeOrderLine.pOLineNbr>((object) line, (object) poLine.LineNbr);
    poLine.CuryLineAmt = new Decimal?(valueOrDefault);
    return ((PXSelectBase<PX.Objects.PO.POLine>) target.Transactions).Update(poLine);
  }

  public virtual PX.Objects.PO.POOrder CreateNewOrder(PX.Objects.PO.POOrder order, List<PMChangeOrderLine> added)
  {
    POOrderEntry target = this.CreateTarget(order);
    ((PXGraph) target).GetExtension<POOrderEntryExt>().SkipProjectLockCommitmentsVerification = true;
    this.ValidateAutoNumbering(order);
    PX.Objects.PO.POOrder newOrder = ((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).Insert(order);
    ((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).SetValueExt<PX.Objects.PO.POOrder.curyID>(newOrder, (object) order.CuryID);
    bool flag = false;
    foreach (PMChangeOrderLine line in added)
    {
      PX.Objects.PO.POLine fromChangeOrderLine = this.CreatePOLineFromChangeOrderLine(line);
      PX.Objects.PO.POLine poLine1 = fromChangeOrderLine;
      Decimal? nullable1 = new Decimal?();
      Decimal? nullable2 = nullable1;
      poLine1.CuryUnitCost = nullable2;
      PX.Objects.PO.POLine poLine2 = fromChangeOrderLine;
      nullable1 = new Decimal?();
      Decimal? nullable3 = nullable1;
      poLine2.CuryLineAmt = nullable3;
      PX.Objects.PO.POLine poLine3 = ((PXSelectBase<PX.Objects.PO.POLine>) target.Transactions).Insert(fromChangeOrderLine);
      poLine3.CuryUnitCost = line.UnitCost;
      PX.Objects.PO.POLine poLine4 = ((PXSelectBase<PX.Objects.PO.POLine>) target.Transactions).Update(poLine3);
      poLine4.CuryLineAmt = line.Amount;
      nullable1 = line.Amount;
      Decimal num = 0M;
      if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
      {
        poLine4.CuryDiscAmt = new Decimal?(0M);
        poLine4.DiscAmt = new Decimal?(0M);
        poLine4.DiscPct = new Decimal?(0M);
      }
      nullable1 = line.RetainageAmt;
      if (nullable1.GetValueOrDefault() != 0M)
        flag = true;
      ((PXSelectBase<PX.Objects.PO.POLine>) target.Transactions).Update(poLine4);
      ((PXSelectBase) this.Details).Cache.SetValue<PMChangeOrderLine.pOLineNbr>((object) line, (object) poLine4.LineNbr);
    }
    if (flag)
      ((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).Current.RetainageApply = new bool?(true);
    if (target.GetRequireControlTotal(newOrder.OrderType))
    {
      Decimal? curyControlTotal = newOrder.CuryControlTotal;
      Decimal? curyOrderTotal = newOrder.CuryOrderTotal;
      if (!(curyControlTotal.GetValueOrDefault() == curyOrderTotal.GetValueOrDefault() & curyControlTotal.HasValue == curyOrderTotal.HasValue))
        ((PXSelectBase<PX.Objects.PO.POOrder>) target.Document).SetValueExt<PX.Objects.PO.POOrder.curyControlTotal>(newOrder, (object) newOrder.CuryOrderTotal);
    }
    newOrder.LockCommitment = new bool?(true);
    newOrder.Approved = new bool?(true);
    if (newOrder.Hold.GetValueOrDefault())
      ((PXAction) target.releaseFromHold).Press();
    else
      ((PXAction) target.Save).Press();
    return newOrder;
  }

  protected virtual void ValidateAutoNumbering(PX.Objects.PO.POOrder order)
  {
    if (order.OrderType == "RS")
    {
      PoSetupExt extension = ((PXSelectBase) this.poSetup).Cache.GetExtension<PoSetupExt>((object) ((PXSelectBase<POSetup>) this.poSetup).Current);
      Numbering numbering = Numbering.PK.Find((PXGraph) this, extension.SubcontractNumberingID);
      if ((numbering != null ? (numbering.UserNumbering.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXException("The change order cannot be released. The system cannot create a commitment because its numbering sequence uses manual numbering. To be able to release the change order, clear the Manual Numbering check box on the Numbering Sequences (CS201010) form for the {0} numbering sequence first.", new object[1]
        {
          (object) extension.SubcontractNumberingID
        });
    }
    else
    {
      Numbering numbering = Numbering.PK.Find((PXGraph) this, ((PXSelectBase<POSetup>) this.poSetup).Current.RegularPONumberingID);
      if ((numbering != null ? (numbering.UserNumbering.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXException("The change order cannot be released. The system cannot create a commitment because its numbering sequence uses manual numbering. To be able to release the change order, clear the Manual Numbering check box on the Numbering Sequences (CS201010) form for the {0} numbering sequence first.", new object[1]
        {
          (object) ((PXSelectBase<POSetup>) this.poSetup).Current.RegularPONumberingID
        });
    }
  }

  public virtual void SetReferences(PMChangeOrderLine line, PX.Objects.PO.POOrder newOrder)
  {
    ((PXSelectBase) this.Details).Cache.SetValue<PMChangeOrderLine.pOOrderType>((object) line, (object) newOrder.OrderType);
    ((PXSelectBase) this.Details).Cache.SetValue<PMChangeOrderLine.pOOrderNbr>((object) line, (object) newOrder.OrderNbr);
  }

  public virtual string CreatePOOrderKey(PX.Objects.PO.POOrder order)
  {
    return $"{order.VendorID}.{order.OrderType}.{order.OrderNbr}.{order.CuryID}";
  }

  public virtual PX.Objects.PO.POOrder CreatePOOrderFromChangedLine(PMChangeOrderLine line)
  {
    return new PX.Objects.PO.POOrder()
    {
      OrderType = line.POOrderType,
      OrderNbr = line.POOrderNbr,
      VendorID = line.VendorID,
      PayToVendorID = line.VendorID,
      Behavior = "C",
      OrderDesc = PXMessages.LocalizeFormatNoPrefix("Change Order #{0}", new object[1]
      {
        (object) line.RefNbr
      }),
      ProjectID = line.ProjectID,
      CuryID = line.CuryID
    };
  }

  public virtual PX.Objects.PO.POLine CreatePOLineFromChangeOrderLine(PMChangeOrderLine line)
  {
    PX.Objects.PO.POLine fromChangeOrderLine = new PX.Objects.PO.POLine()
    {
      InventoryID = line.InventoryID,
      SubItemID = line.SubItemID,
      TranDesc = line.Description,
      UOM = line.UOM,
      OrigOrderQty = new Decimal?(0M),
      OrigExtCost = new Decimal?(0M),
      OrderQty = line.Qty,
      CuryUnitCost = line.UnitCost,
      CuryLineAmt = line.Amount,
      RetainagePct = line.RetainagePct,
      CuryRetainageAmt = line.RetainageAmt,
      ExpenseAcctID = line.AccountID,
      ProjectID = line.ProjectID,
      TaskID = line.TaskID,
      CostCodeID = line.CostCodeID
    };
    fromChangeOrderLine.TranDesc = line.Description;
    fromChangeOrderLine.ManualPrice = new bool?(true);
    fromChangeOrderLine.TaxCategoryID = line.TaxCategoryID;
    return fromChangeOrderLine;
  }

  public virtual int? GetProjectedAccountGroup(PMChangeOrderLine line)
  {
    return PXSelectorAttribute.Select<PMChangeOrderLine.accountID>(((PXSelectBase) this.Details).Cache, (object) line, (object) line.AccountID) is PX.Objects.GL.Account account ? account.AccountGroupID : new int?();
  }

  public virtual POOrderEntry CreateTarget(PX.Objects.PO.POOrder order)
  {
    return order.OrderType == "RS" ? (POOrderEntry) PXGraph.CreateInstance<SubcontractEntry>() : PXGraph.CreateInstance<POOrderEntry>();
  }

  public virtual void AddSelectedCostBudget()
  {
    foreach (PMCostBudget pmCostBudget in ((PXSelectBase) this.AvailableCostBudget).Cache.Updated)
    {
      if (!(pmCostBudget.Type != "E") && pmCostBudget.Selected.GetValueOrDefault())
      {
        PMChangeOrderCostBudget changeOrderCostBudget1 = new PMChangeOrderCostBudget();
        changeOrderCostBudget1.ProjectID = pmCostBudget.ProjectID;
        changeOrderCostBudget1.ProjectTaskID = pmCostBudget.ProjectTaskID;
        changeOrderCostBudget1.AccountGroupID = pmCostBudget.AccountGroupID;
        changeOrderCostBudget1.InventoryID = pmCostBudget.InventoryID;
        changeOrderCostBudget1.CostCodeID = pmCostBudget.CostCodeID;
        PMChangeOrderCostBudget changeOrderCostBudget2 = changeOrderCostBudget1;
        if (((PXSelectBase<PMChangeOrderCostBudget>) this.CostBudget).Locate(changeOrderCostBudget2) == null)
          ((PXSelectBase<PMChangeOrderCostBudget>) this.CostBudget).Insert(changeOrderCostBudget2);
      }
    }
  }

  public virtual void AddSelectedRevenueBudget()
  {
    foreach (PMRevenueBudget pmRevenueBudget in ((PXSelectBase) this.AvailableRevenueBudget).Cache.Updated)
    {
      if (!(pmRevenueBudget.Type != "I") && pmRevenueBudget.Selected.GetValueOrDefault())
      {
        PMChangeOrderRevenueBudget orderRevenueBudget1 = new PMChangeOrderRevenueBudget();
        orderRevenueBudget1.ProjectID = pmRevenueBudget.ProjectID;
        orderRevenueBudget1.ProjectTaskID = pmRevenueBudget.ProjectTaskID;
        orderRevenueBudget1.AccountGroupID = pmRevenueBudget.AccountGroupID;
        orderRevenueBudget1.InventoryID = pmRevenueBudget.InventoryID;
        orderRevenueBudget1.CostCodeID = pmRevenueBudget.CostCodeID;
        PMChangeOrderRevenueBudget orderRevenueBudget2 = orderRevenueBudget1;
        if (((PXSelectBase<PMChangeOrderRevenueBudget>) this.RevenueBudget).Locate(orderRevenueBudget2) == null)
          ((PXSelectBase<PMChangeOrderRevenueBudget>) this.RevenueBudget).Insert(orderRevenueBudget2);
      }
    }
  }

  public virtual void AddSelectedPOLines()
  {
    HashSet<ChangeOrderEntry.POLineKey> poLineKeySet1 = new HashSet<ChangeOrderEntry.POLineKey>();
    foreach (PXResult<PMChangeOrderLine> pxResult in ((PXSelectBase<PMChangeOrderLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMChangeOrderLine pmChangeOrderLine = PXResult<PMChangeOrderLine>.op_Implicit(pxResult);
      int? poLineNbr = pmChangeOrderLine.POLineNbr;
      if (poLineNbr.HasValue)
      {
        HashSet<ChangeOrderEntry.POLineKey> poLineKeySet2 = poLineKeySet1;
        string poOrderType = pmChangeOrderLine.POOrderType;
        string poOrderNbr = pmChangeOrderLine.POOrderNbr;
        poLineNbr = pmChangeOrderLine.POLineNbr;
        int lineNbr = poLineNbr.Value;
        ChangeOrderEntry.POLineKey poLineKey = new ChangeOrderEntry.POLineKey(poOrderType, poOrderNbr, lineNbr);
        poLineKeySet2.Add(poLineKey);
      }
    }
    foreach (POLinePM poLine in ((PXSelectBase) this.AvailablePOLines).Cache.Updated)
    {
      if (poLine.Selected.GetValueOrDefault())
      {
        ChangeOrderEntry.POLineKey poLineKey = new ChangeOrderEntry.POLineKey(poLine.OrderType, poLine.OrderNbr, poLine.LineNbr.Value);
        if (!poLineKeySet1.Contains(poLineKey))
          ((PXSelectBase<PMChangeOrderLine>) this.Details).Insert(this.CreateChangeOrderLine(poLine));
      }
    }
  }

  public virtual PMChangeOrderLine CreateChangeOrderLine(POLinePM poLine)
  {
    return new PMChangeOrderLine()
    {
      POOrderType = poLine.OrderType,
      POOrderNbr = poLine.OrderNbr,
      POLineNbr = poLine.LineNbr,
      TaskID = poLine.TaskID,
      UOM = poLine.UOM,
      UnitCost = poLine.CuryUnitCost,
      VendorID = poLine.VendorID,
      CostCodeID = poLine.CostCodeID,
      CuryID = poLine.CuryID,
      AccountID = poLine.ExpenseAcctID,
      LineType = poLine.Completed.GetValueOrDefault() || poLine.Cancelled.GetValueOrDefault() ? "R" : "U",
      InventoryID = poLine.InventoryID,
      SubItemID = poLine.SubItemID,
      TaxCategoryID = poLine.TaxCategoryID
    };
  }

  public virtual void InitCostBudgetFields(PMChangeOrderCostBudget record)
  {
    if (record == null)
      return;
    PMBudget originalCostBudget = this.IsValidKey((PMChangeOrderBudget) record) ? (PMBudget) this.GetOriginalCostBudget(BudgetKeyTuple.Create((IProjectFilter) record)) : (PMBudget) null;
    this.InitBudgetFields((PMChangeOrderBudget) record, originalCostBudget);
  }

  public virtual void InitRevenueBudgetFields(PMChangeOrderRevenueBudget record)
  {
    if (record == null)
      return;
    PMBudget originalRevenueBudget = this.IsValidKey((PMChangeOrderBudget) record) ? (PMBudget) this.GetOriginalRevenueBudget(BudgetKeyTuple.Create((IProjectFilter) record)) : (PMBudget) null;
    this.InitBudgetFields((PMChangeOrderBudget) record, originalRevenueBudget);
  }

  public virtual void InitBudgetFields(PMChangeOrderBudget record, PMBudget budget)
  {
    if (record == null)
      return;
    BudgetKeyTuple budgetKeyTuple = BudgetKeyTuple.Create((IProjectFilter) record);
    record.OtherDraftRevisedAmount = new Decimal?(this.GetDraftChangeOrderBudgetAmount(record));
    record.RevisedAmount = new Decimal?(record.Amount.GetValueOrDefault());
    if (this.IsValidKey(record))
      record.CommittedCOAmount = new Decimal?(this.GetCurrentCommittedCOAmount(budgetKeyTuple));
    PMChangeOrderBudget previousTotals = this.GetPreviousTotals(budgetKeyTuple);
    Decimal? nullable1;
    if (previousTotals != null)
    {
      PMChangeOrderBudget changeOrderBudget1 = record;
      nullable1 = previousTotals.Amount;
      Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
      changeOrderBudget1.PreviouslyApprovedAmount = nullable2;
      PMChangeOrderBudget changeOrderBudget2 = record;
      nullable1 = previousTotals.Qty;
      Decimal? nullable3 = new Decimal?(nullable1.GetValueOrDefault());
      changeOrderBudget2.PreviouslyApprovedQty = nullable3;
    }
    else
    {
      record.PreviouslyApprovedAmount = new Decimal?(0M);
      record.PreviouslyApprovedQty = new Decimal?(0M);
    }
    if (budget != null)
    {
      PMChangeOrderBudget changeOrderBudget3 = record;
      nullable1 = budget.CuryAmount;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = record.PreviouslyApprovedAmount;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal num1 = valueOrDefault1 + valueOrDefault2;
      nullable1 = record.Amount;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      Decimal? nullable4 = new Decimal?(num1 + valueOrDefault3);
      changeOrderBudget3.RevisedAmount = nullable4;
      PMChangeOrderBudget changeOrderBudget4 = record;
      nullable1 = budget.Qty;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      nullable1 = record.PreviouslyApprovedQty;
      Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
      Decimal num2 = valueOrDefault4 + valueOrDefault5;
      nullable1 = record.Qty;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      Decimal? nullable5 = new Decimal?(num2 + valueOrDefault6);
      changeOrderBudget4.RevisedQty = nullable5;
      if (this.IsValidKey(record))
        record.CommittedCOQty = new Decimal?(this.GetCurrentCommittedCOQty(BudgetKeyTuple.Create((IProjectFilter) budget), (IQuantify) budget));
      PMChangeOrderBudget changeOrderBudget5 = record;
      nullable1 = budget.CuryRevisedAmount;
      Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
      nullable1 = record.OtherDraftRevisedAmount;
      Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
      Decimal? nullable6 = new Decimal?(valueOrDefault7 + valueOrDefault8);
      changeOrderBudget5.TotalPotentialRevisedAmount = nullable6;
      if (record.Released.GetValueOrDefault())
        return;
      PMChangeOrderBudget changeOrderBudget6 = record;
      nullable1 = changeOrderBudget6.TotalPotentialRevisedAmount;
      Decimal valueOrDefault9 = record.Amount.GetValueOrDefault();
      changeOrderBudget6.TotalPotentialRevisedAmount = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault9) : new Decimal?();
    }
    else
    {
      if (string.IsNullOrEmpty(record.UOM))
        return;
      record.RevisedQty = record.Qty;
      if (!this.IsValidKey(record))
        return;
      record.CommittedCOQty = new Decimal?(this.GetCurrentCommittedCOQty(budgetKeyTuple, (IQuantify) record));
    }
  }

  public virtual Decimal GetCurrentCommittedCOAmount(BudgetKeyTuple key)
  {
    Decimal committedCoAmount = 0M;
    foreach (PXResult<PMChangeOrderLine> pxResult in ((PXSelectBase<PMChangeOrderLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMChangeOrderLine line = PXResult<PMChangeOrderLine>.op_Implicit(pxResult);
      int? nullable = line.CostCodeID;
      if ((nullable ?? CostCodeAttribute.GetDefaultCostCode()) == key.CostCodeID)
      {
        nullable = line.InventoryID;
        if ((nullable ?? PMInventorySelectorAttribute.EmptyInventoryID) == key.InventoryID)
        {
          nullable = line.TaskID;
          int projectTaskId = key.ProjectTaskID;
          if (nullable.GetValueOrDefault() == projectTaskId & nullable.HasValue)
          {
            nullable = this.GetProjectedAccountGroup(line);
            int accountGroupId = key.AccountGroupID;
            if (nullable.GetValueOrDefault() == accountGroupId & nullable.HasValue)
              committedCoAmount += line.AmountInProjectCury.GetValueOrDefault();
          }
        }
      }
    }
    return committedCoAmount;
  }

  public virtual Decimal GetCurrentCommittedCOQty(BudgetKeyTuple key, IQuantify budget)
  {
    Decimal currentCommittedCoQty = 0M;
    foreach (PXResult<PMChangeOrderLine> pxResult in ((PXSelectBase<PMChangeOrderLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMChangeOrderLine pmChangeOrderLine = PXResult<PMChangeOrderLine>.op_Implicit(pxResult);
      int? nullable = pmChangeOrderLine.CostCodeID;
      if ((nullable ?? CostCodeAttribute.GetDefaultCostCode()) == key.CostCodeID)
      {
        nullable = pmChangeOrderLine.InventoryID;
        if ((nullable ?? PMInventorySelectorAttribute.EmptyInventoryID) == key.InventoryID)
        {
          nullable = pmChangeOrderLine.TaskID;
          int projectTaskId = key.ProjectTaskID;
          if (nullable.GetValueOrDefault() == projectTaskId & nullable.HasValue)
          {
            nullable = this.GetProjectedAccountGroup(pmChangeOrderLine);
            int accountGroupId = key.AccountGroupID;
            if (nullable.GetValueOrDefault() == accountGroupId & nullable.HasValue)
            {
              Decimal rollupQty = this.BalanceCalculator.CalculateRollupQty<PMChangeOrderLine>(pmChangeOrderLine, budget);
              if (rollupQty != 0M)
                currentCommittedCoQty += rollupQty;
            }
          }
        }
      }
    }
    return currentCommittedCoQty;
  }

  public virtual void InitDetailLineFields(PMChangeOrderLine line)
  {
    if (line == null)
      return;
    if (!line.Released.GetValueOrDefault())
    {
      line.PotentialRevisedAmount = new Decimal?(line.Amount.GetValueOrDefault());
      line.PotentialRevisedQty = new Decimal?(line.Qty.GetValueOrDefault());
    }
    else
    {
      line.PotentialRevisedAmount = new Decimal?(0M);
      line.PotentialRevisedQty = new Decimal?(0M);
    }
    POLinePM poLine = this.GetPOLine(line);
    if (poLine == null)
      return;
    PMChangeOrderLine pmChangeOrderLine1 = line;
    Decimal? potentialRevisedAmount = pmChangeOrderLine1.PotentialRevisedAmount;
    Decimal valueOrDefault1 = poLine.CuryLineAmt.GetValueOrDefault();
    pmChangeOrderLine1.PotentialRevisedAmount = potentialRevisedAmount.HasValue ? new Decimal?(potentialRevisedAmount.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    PMChangeOrderLine pmChangeOrderLine2 = line;
    Decimal? potentialRevisedQty = pmChangeOrderLine2.PotentialRevisedQty;
    Decimal? nullable1 = poLine.OrderQty;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable2;
    if (!potentialRevisedQty.HasValue)
    {
      nullable1 = new Decimal?();
      nullable2 = nullable1;
    }
    else
      nullable2 = new Decimal?(potentialRevisedQty.GetValueOrDefault() + valueOrDefault2);
    pmChangeOrderLine2.PotentialRevisedQty = nullable2;
  }

  public virtual string ResolveChangeOrderLineType(PMChangeOrderLine line, POLinePM poLine)
  {
    if (this.IsValidKey(line))
    {
      bool? nullable;
      int num;
      if (poLine != null)
      {
        nullable = poLine.Completed;
        if (nullable.GetValueOrDefault())
        {
          num = 1;
          goto label_7;
        }
      }
      if (poLine == null)
      {
        num = 0;
      }
      else
      {
        nullable = poLine.Cancelled;
        num = nullable.GetValueOrDefault() ? 1 : 0;
      }
label_7:
      return num == 0 ? "U" : "R";
    }
    return !string.IsNullOrEmpty(line.POOrderNbr) ? "D" : "L";
  }

  public virtual bool PrepaymentVisible()
  {
    return ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.PrepaymentEnabled.GetValueOrDefault();
  }

  public virtual bool LimitsVisible()
  {
    return ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.LimitsEnabled.GetValueOrDefault();
  }

  public virtual bool ProductivityVisible()
  {
    return ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.BudgetMetricsEnabled.GetValueOrDefault();
  }

  public virtual bool CanEditDocument(PMChangeOrder doc)
  {
    if (doc == null)
      return true;
    bool? nullable = doc.Released;
    if (nullable.GetValueOrDefault())
      return false;
    nullable = doc.Hold;
    return nullable.GetValueOrDefault() && doc.Status != "L";
  }

  public Decimal GetAmountInProjectCurrency(string fromCuryID, Decimal? value)
  {
    return this.MultiCurrencyService.GetValueInProjectCurrency((PXGraph) this, ((PXSelectBase<PMProject>) this.Project).Current, fromCuryID, ((PXSelectBase<PMChangeOrder>) this.Document).Current.Date, value);
  }

  public virtual bool IsProjectEnabled()
  {
    return !((PXSelectBase) this.CostBudget).Cache.IsInsertedUpdatedDeleted && !((PXSelectBase) this.RevenueBudget).Cache.IsInsertedUpdatedDeleted && !((PXSelectBase) this.Details).Cache.IsInsertedUpdatedDeleted && ((PXSelectBase<PMChangeOrderCostBudget>) this.CostBudget).Select(Array.Empty<object>()).Count <= 0 && ((PXSelectBase<PMChangeOrderRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()).Count <= 0 && ((PXSelectBase<PMChangeOrderLine>) this.Details).Select(Array.Empty<object>()).Count <= 0;
  }

  public virtual void IncreaseDraftBucket(int mult)
  {
    foreach (PXResult<PMChangeOrderCostBudget> pxResult in ((PXSelectBase<PMChangeOrderCostBudget>) this.CostBudget).Select(Array.Empty<object>()))
      this.IncreaseDraftBucket((PMChangeOrderBudget) PXResult<PMChangeOrderCostBudget>.op_Implicit(pxResult), mult);
    foreach (PXResult<PMChangeOrderRevenueBudget> pxResult in ((PXSelectBase<PMChangeOrderRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()))
      this.IncreaseDraftBucket((PMChangeOrderBudget) PXResult<PMChangeOrderRevenueBudget>.op_Implicit(pxResult), mult);
  }

  public virtual void IncreaseDraftBucket(PMChangeOrderBudget row, int mult)
  {
    if (!row.ProjectID.HasValue || !row.ProjectTaskID.HasValue || !row.AccountGroupID.HasValue || !row.InventoryID.HasValue || !row.CostCodeID.HasValue)
      return;
    PMAccountGroup ag = PMAccountGroup.PK.Find((PXGraph) this, row.AccountGroupID);
    bool isExisting;
    PX.Objects.PM.Lite.PMBudget pmBudget = new BudgetService((PXGraph) this).SelectProjectBalance((IProjectFilter) row, ag, ((PXSelectBase<PMProject>) this.Project).Current, out isExisting);
    PXSelect<PMBudgetAccum> budget1 = this.Budget;
    PMBudgetAccum pmBudgetAccum1 = new PMBudgetAccum();
    pmBudgetAccum1.ProjectID = row.ProjectID;
    pmBudgetAccum1.ProjectTaskID = row.ProjectTaskID;
    pmBudgetAccum1.AccountGroupID = row.AccountGroupID;
    pmBudgetAccum1.InventoryID = pmBudget.InventoryID;
    pmBudgetAccum1.CostCodeID = pmBudget.CostCodeID;
    pmBudgetAccum1.CuryInfoID = ((PXSelectBase<PMProject>) this.Project).Current.CuryInfoID;
    PMBudgetAccum budget2 = ((PXSelectBase<PMBudgetAccum>) budget1).Insert(pmBudgetAccum1);
    budget2.UOM = pmBudget.UOM;
    budget2.Description = row.Description;
    budget2.Type = pmBudget.Type;
    budget2.CuryUnitRate = row.Rate;
    budget2.RetainagePct = ((PXSelectBase<PMProject>) this.Project).Current.RetainagePct;
    if (pmBudget.Type == "E")
    {
      PMTask pmTask = PMTask.PK.Find((PXGraph) this, pmBudget.ProjectID, pmBudget.TaskID);
      if (pmTask != null && pmTask.Type == "CostRev")
        budget2.RevenueTaskID = pmBudget.ProjectTaskID;
    }
    if (budget2.Type == "I")
    {
      PMTask pmTask = PMTask.PK.Find((PXGraph) this, row.ProjectID, row.ProjectTaskID);
      budget2.TaxCategoryID = pmTask?.TaxCategoryID;
      if (!isExisting)
        budget2.ProgressBillingBase = pmTask?.ProgressBillingBase;
    }
    Decimal rollupQty = this.BalanceCalculator.CalculateRollupQty<PMChangeOrderBudget>(row, (IQuantify) budget2);
    PMBudgetAccum pmBudgetAccum2 = budget2;
    Decimal? nullable = pmBudgetAccum2.CuryDraftChangeOrderAmount;
    Decimal num1 = (Decimal) mult * row.Amount.GetValueOrDefault();
    pmBudgetAccum2.CuryDraftChangeOrderAmount = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num1) : new Decimal?();
    PMBudgetAccum pmBudgetAccum3 = budget2;
    nullable = pmBudgetAccum3.DraftChangeOrderQty;
    Decimal num2 = (Decimal) mult * rollupQty;
    pmBudgetAccum3.DraftChangeOrderQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num2) : new Decimal?();
    if (((PXSelectBase<PMChangeOrder>) this.Document).Current == null)
      return;
    FinPeriod finPeriodByDate = this.FinPeriodRepository.GetFinPeriodByDate(((PXSelectBase<PMChangeOrder>) this.Document).Current.Date, new int?(0));
    if (finPeriodByDate == null)
      return;
    PMForecastHistoryAccum forecastHistoryAccum1 = new PMForecastHistoryAccum();
    forecastHistoryAccum1.ProjectID = pmBudget.ProjectID;
    forecastHistoryAccum1.ProjectTaskID = pmBudget.ProjectTaskID;
    forecastHistoryAccum1.AccountGroupID = pmBudget.AccountGroupID;
    forecastHistoryAccum1.InventoryID = pmBudget.InventoryID;
    forecastHistoryAccum1.CostCodeID = pmBudget.CostCodeID;
    forecastHistoryAccum1.PeriodID = finPeriodByDate.FinPeriodID;
    PMForecastHistoryAccum forecastHistoryAccum2 = ((PXSelectBase<PMForecastHistoryAccum>) this.ForecastHistory).Insert(forecastHistoryAccum1);
    PMForecastHistoryAccum forecastHistoryAccum3 = forecastHistoryAccum2;
    nullable = forecastHistoryAccum3.DraftChangeOrderQty;
    Decimal num3 = (Decimal) mult * rollupQty;
    forecastHistoryAccum3.DraftChangeOrderQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num3) : new Decimal?();
    PMForecastHistoryAccum forecastHistoryAccum4 = forecastHistoryAccum2;
    nullable = forecastHistoryAccum4.CuryDraftChangeOrderAmount;
    Decimal num4 = (Decimal) mult * row.Amount.GetValueOrDefault();
    forecastHistoryAccum4.CuryDraftChangeOrderAmount = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num4) : new Decimal?();
  }

  public virtual void RemoveObsoleteLines()
  {
    foreach (PMBudgetAccum pmBudgetAccum in ((PXSelectBase) this.Budget).Cache.Inserted)
    {
      if (pmBudgetAccum.CuryDraftChangeOrderAmount.GetValueOrDefault() == 0M && pmBudgetAccum.DraftChangeOrderQty.GetValueOrDefault() == 0M)
        ((PXSelectBase) this.Budget).Cache.Remove((object) pmBudgetAccum);
    }
    foreach (PMForecastHistoryAccum forecastHistoryAccum in ((PXSelectBase) this.ForecastHistory).Cache.Inserted)
    {
      if (forecastHistoryAccum.CuryDraftChangeOrderAmount.GetValueOrDefault() == 0M && forecastHistoryAccum.DraftChangeOrderQty.GetValueOrDefault() == 0M)
        ((PXSelectBase) this.ForecastHistory).Cache.Remove((object) forecastHistoryAccum);
    }
  }

  public virtual void ApplyChangeOrderBudget(PMChangeOrderBudget row, PMChangeOrder order)
  {
    PMAccountGroup ag = PMAccountGroup.PK.Find((PXGraph) this, row.AccountGroupID);
    Func<Decimal, (Decimal, Decimal)> func = (Func<Decimal, (Decimal, Decimal)>) null;
    bool isExisting;
    PX.Objects.PM.Lite.PMBudget budget1 = new BudgetService((PXGraph) this).SelectProjectBalance((IProjectFilter) row, ag, ((PXSelectBase<PMProject>) this.Project).Current, out isExisting);
    PMBudgetAccum pmBudgetAccum1 = new PMBudgetAccum();
    pmBudgetAccum1.ProjectID = budget1.ProjectID;
    pmBudgetAccum1.ProjectTaskID = budget1.ProjectTaskID;
    pmBudgetAccum1.AccountGroupID = budget1.AccountGroupID;
    pmBudgetAccum1.InventoryID = budget1.InventoryID;
    pmBudgetAccum1.CostCodeID = budget1.CostCodeID;
    pmBudgetAccum1.UOM = budget1.UOM;
    pmBudgetAccum1.Description = budget1.Description;
    pmBudgetAccum1.Type = budget1.Type;
    pmBudgetAccum1.CuryInfoID = ((PXSelectBase<PMProject>) this.Project).Current.CuryInfoID;
    PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
    Decimal num1 = 0M;
    if (!isExisting)
    {
      pmBudgetAccum2.CuryUnitRate = row.Rate;
      pmBudgetAccum2.UOM = row.UOM ?? budget1.UOM;
      pmBudgetAccum2.Description = row.Description ?? budget1.Description;
      if (budget1.Type == "E")
      {
        PMTask pmTask = PMTask.PK.Find((PXGraph) this, budget1.ProjectID, budget1.TaskID);
        if (pmTask != null && pmTask.Type == "CostRev")
          pmBudgetAccum2.RevenueTaskID = budget1.ProjectTaskID;
      }
      else if (budget1.Type == "I")
      {
        PMTask pmTask = PMTask.PK.Find((PXGraph) this, budget1.ProjectID, budget1.TaskID);
        if (pmTask != null)
          pmBudgetAccum2.ProgressBillingBase = pmTask.ProgressBillingBase;
      }
    }
    else if (pmBudgetAccum2.Type == "I")
    {
      PMRevenueBudget revenue = this.GetOriginalRevenueBudget(BudgetKeyTuple.Create((IProjectFilter) budget1));
      if (revenue != null)
      {
        if (revenue.ProgressBillingBase == "A")
        {
          Decimal valueOrDefault1 = revenue.CuryRevisedAmount.GetValueOrDefault();
          Decimal? nullable1 = row.Amount;
          Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
          Decimal num2 = valueOrDefault1 + valueOrDefault2;
          nullable1 = revenue.CuryActualAmount;
          Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
          Decimal? nullable2 = revenue.CuryInclTaxAmount;
          Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
          Decimal num3 = valueOrDefault3 + valueOrDefault4;
          nullable2 = revenue.CuryInvoicedAmount;
          Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
          Decimal num4 = num3 + valueOrDefault5;
          nullable2 = revenue.CuryPrepaymentAmount;
          Decimal valueOrDefault6 = nullable2.GetValueOrDefault();
          nullable2 = revenue.CuryPrepaymentInvoiced;
          Decimal valueOrDefault7 = nullable2.GetValueOrDefault();
          Decimal num5 = valueOrDefault6 - valueOrDefault7;
          Decimal num6 = num4 + num5;
          nullable2 = revenue.CompletedPct;
          Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
          num1 = num2 * valueOrDefault8 / 100M - num6 - revenue.CuryAmountToInvoice.GetValueOrDefault();
        }
        else if (revenue.ProgressBillingBase == "Q")
          func = (Func<Decimal, (Decimal, Decimal)>) (rollupQty =>
          {
            Decimal? nullable = revenue.CompletedPct;
            Decimal num7 = nullable.GetValueOrDefault() / 100.0M * rollupQty;
            nullable = revenue.InvoicedQty;
            Decimal valueOrDefault9 = nullable.GetValueOrDefault();
            nullable = revenue.ActualQty;
            Decimal valueOrDefault10 = nullable.GetValueOrDefault();
            Decimal num8 = valueOrDefault9 + valueOrDefault10;
            Decimal num9 = Decimal.Round(num7 - num8, CommonSetupDecPl.Qty);
            nullable = revenue.CuryUnitRate;
            return (num9, num9 * nullable.GetValueOrDefault());
          });
      }
    }
    PMBudgetAccum budget2 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(pmBudgetAccum2);
    Decimal rollupQty1 = this.BalanceCalculator.CalculateRollupQty<PMChangeOrderBudget>(row, (IQuantify) budget2);
    Decimal? nullable3;
    if (func != null)
    {
      (Decimal, Decimal) valueTuple = func(rollupQty1);
      PMBudgetAccum pmBudgetAccum3 = budget2;
      nullable3 = pmBudgetAccum3.QtyToInvoice;
      Decimal num10 = valueTuple.Item1;
      pmBudgetAccum3.QtyToInvoice = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num10) : new Decimal?();
      num1 = valueTuple.Item2;
    }
    PMBudgetAccum pmBudgetAccum4 = budget2;
    nullable3 = pmBudgetAccum4.DraftChangeOrderQty;
    Decimal num11 = rollupQty1;
    pmBudgetAccum4.DraftChangeOrderQty = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - num11) : new Decimal?();
    PMBudgetAccum pmBudgetAccum5 = budget2;
    nullable3 = pmBudgetAccum5.ChangeOrderQty;
    Decimal num12 = rollupQty1;
    pmBudgetAccum5.ChangeOrderQty = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num12) : new Decimal?();
    PMBudgetAccum pmBudgetAccum6 = budget2;
    nullable3 = pmBudgetAccum6.RevisedQty;
    Decimal num13 = rollupQty1;
    pmBudgetAccum6.RevisedQty = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num13) : new Decimal?();
    PMBudgetAccum pmBudgetAccum7 = budget2;
    nullable3 = pmBudgetAccum7.CuryDraftChangeOrderAmount;
    Decimal? nullable4 = row.Amount;
    Decimal valueOrDefault11 = nullable4.GetValueOrDefault();
    Decimal? nullable5;
    if (!nullable3.HasValue)
    {
      nullable4 = new Decimal?();
      nullable5 = nullable4;
    }
    else
      nullable5 = new Decimal?(nullable3.GetValueOrDefault() - valueOrDefault11);
    pmBudgetAccum7.CuryDraftChangeOrderAmount = nullable5;
    PMBudgetAccum pmBudgetAccum8 = budget2;
    nullable3 = pmBudgetAccum8.CuryChangeOrderAmount;
    nullable4 = row.Amount;
    Decimal valueOrDefault12 = nullable4.GetValueOrDefault();
    Decimal? nullable6;
    if (!nullable3.HasValue)
    {
      nullable4 = new Decimal?();
      nullable6 = nullable4;
    }
    else
      nullable6 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault12);
    pmBudgetAccum8.CuryChangeOrderAmount = nullable6;
    PMBudgetAccum pmBudgetAccum9 = budget2;
    nullable3 = pmBudgetAccum9.CuryRevisedAmount;
    nullable4 = row.Amount;
    Decimal valueOrDefault13 = nullable4.GetValueOrDefault();
    Decimal? nullable7;
    if (!nullable3.HasValue)
    {
      nullable4 = new Decimal?();
      nullable7 = nullable4;
    }
    else
      nullable7 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault13);
    pmBudgetAccum9.CuryRevisedAmount = nullable7;
    PMBudgetAccum pmBudgetAccum10 = budget2;
    nullable3 = pmBudgetAccum10.CuryAmountToInvoice;
    Decimal num14 = num1;
    Decimal? nullable8;
    if (!nullable3.HasValue)
    {
      nullable4 = new Decimal?();
      nullable8 = nullable4;
    }
    else
      nullable8 = new Decimal?(nullable3.GetValueOrDefault() + num14);
    pmBudgetAccum10.CuryAmountToInvoice = nullable8;
    PMChangeOrder order1 = order;
    PMBudgetAccum budget3 = budget2;
    Decimal revisedBudgetQty = rollupQty1;
    nullable3 = row.Amount;
    Decimal valueOrDefault14 = nullable3.GetValueOrDefault();
    this.UpdateBudgetHistoryLine(order1, budget3, revisedBudgetQty, valueOrDefault14);
    FinPeriod finPeriodByDate = this.FinPeriodRepository.GetFinPeriodByDate(order.Date, new int?(0));
    if (finPeriodByDate == null)
      return;
    PMForecastHistoryAccum forecastHistoryAccum1 = new PMForecastHistoryAccum();
    forecastHistoryAccum1.ProjectID = budget1.ProjectID;
    forecastHistoryAccum1.ProjectTaskID = budget1.ProjectTaskID;
    forecastHistoryAccum1.AccountGroupID = budget1.AccountGroupID;
    forecastHistoryAccum1.InventoryID = budget1.InventoryID;
    forecastHistoryAccum1.CostCodeID = budget1.CostCodeID;
    forecastHistoryAccum1.PeriodID = finPeriodByDate.FinPeriodID;
    PMForecastHistoryAccum forecastHistoryAccum2 = ((PXSelectBase<PMForecastHistoryAccum>) this.ForecastHistory).Insert(forecastHistoryAccum1);
    PMForecastHistoryAccum forecastHistoryAccum3 = forecastHistoryAccum2;
    nullable3 = forecastHistoryAccum3.DraftChangeOrderQty;
    Decimal num15 = rollupQty1;
    Decimal? nullable9;
    if (!nullable3.HasValue)
    {
      nullable4 = new Decimal?();
      nullable9 = nullable4;
    }
    else
      nullable9 = new Decimal?(nullable3.GetValueOrDefault() - num15);
    forecastHistoryAccum3.DraftChangeOrderQty = nullable9;
    PMForecastHistoryAccum forecastHistoryAccum4 = forecastHistoryAccum2;
    nullable3 = forecastHistoryAccum4.CuryDraftChangeOrderAmount;
    nullable4 = row.Amount;
    Decimal valueOrDefault15 = nullable4.GetValueOrDefault();
    Decimal? nullable10;
    if (!nullable3.HasValue)
    {
      nullable4 = new Decimal?();
      nullable10 = nullable4;
    }
    else
      nullable10 = new Decimal?(nullable3.GetValueOrDefault() - valueOrDefault15);
    forecastHistoryAccum4.CuryDraftChangeOrderAmount = nullable10;
    PMForecastHistoryAccum forecastHistoryAccum5 = forecastHistoryAccum2;
    nullable3 = forecastHistoryAccum5.ChangeOrderQty;
    Decimal num16 = rollupQty1;
    Decimal? nullable11;
    if (!nullable3.HasValue)
    {
      nullable4 = new Decimal?();
      nullable11 = nullable4;
    }
    else
      nullable11 = new Decimal?(nullable3.GetValueOrDefault() + num16);
    forecastHistoryAccum5.ChangeOrderQty = nullable11;
    PMForecastHistoryAccum forecastHistoryAccum6 = forecastHistoryAccum2;
    nullable3 = forecastHistoryAccum6.CuryChangeOrderAmount;
    nullable4 = row.Amount;
    Decimal valueOrDefault16 = nullable4.GetValueOrDefault();
    Decimal? nullable12;
    if (!nullable3.HasValue)
    {
      nullable4 = new Decimal?();
      nullable12 = nullable4;
    }
    else
      nullable12 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault16);
    forecastHistoryAccum6.CuryChangeOrderAmount = nullable12;
  }

  public void UpdateBudgetHistoryLine(
    PMChangeOrder order,
    PMBudgetAccum budget,
    Decimal revisedBudgetQty,
    Decimal curyRevisedBudgetAmt)
  {
    PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
    PMProjectBudgetHistoryAccum budgetHistoryAccum1 = new PMProjectBudgetHistoryAccum();
    budgetHistoryAccum1.Date = order.Date;
    budgetHistoryAccum1.ProjectID = budget.ProjectID;
    budgetHistoryAccum1.TaskID = budget.ProjectTaskID;
    budgetHistoryAccum1.AccountGroupID = budget.AccountGroupID;
    budgetHistoryAccum1.InventoryID = budget.InventoryID;
    budgetHistoryAccum1.CostCodeID = budget.CostCodeID;
    budgetHistoryAccum1.ChangeOrderRefNbr = order.RefNbr;
    PMProjectBudgetHistoryAccum budgetHistoryAccum2 = ((PXSelectBase<PMProjectBudgetHistoryAccum>) this.ProjectBudgetHistory).Insert(budgetHistoryAccum1);
    budgetHistoryAccum2.Type = budget.Type;
    budgetHistoryAccum2.CuryInfoID = current.CuryInfoID;
    budgetHistoryAccum2.UOM = budget.UOM;
    PMProjectBudgetHistoryAccum budgetHistoryAccum3 = budgetHistoryAccum2;
    Decimal? revisedBudgetQty1 = budgetHistoryAccum3.RevisedBudgetQty;
    Decimal num1 = revisedBudgetQty;
    budgetHistoryAccum3.RevisedBudgetQty = revisedBudgetQty1.HasValue ? new Decimal?(revisedBudgetQty1.GetValueOrDefault() + num1) : new Decimal?();
    PMProjectBudgetHistoryAccum budgetHistoryAccum4 = budgetHistoryAccum2;
    Decimal? revisedBudgetAmt = budgetHistoryAccum4.CuryRevisedBudgetAmt;
    Decimal num2 = curyRevisedBudgetAmt;
    budgetHistoryAccum4.CuryRevisedBudgetAmt = revisedBudgetAmt.HasValue ? new Decimal?(revisedBudgetAmt.GetValueOrDefault() + num2) : new Decimal?();
  }

  private PMProject GetProject(int? projectID) => PMProject.PK.Find((PXGraph) this, projectID);

  private PMCostCode GetCostCode(int? costCodeID) => PMCostCode.PK.Find((PXGraph) this, costCodeID);

  private PX.Objects.IN.InventoryItem GetInventoryItem(int? inventoryID)
  {
    return PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) inventoryID
    }));
  }

  /// <summary>
  /// You must invalidate local cache, when document key changes (next, previous, last, first actions).
  /// </summary>
  private bool IsCacheUpdateRequired()
  {
    if (((PXSelectBase<PMChangeOrder>) this.Document).Current == null)
      return false;
    string refNbr = ((PXSelectBase<PMChangeOrder>) this.Document).Current.RefNbr;
    int num = refNbr != this.LastRefNbr ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.ClearLocalCache();
    this.LastRefNbr = refNbr;
    return num != 0;
  }

  public virtual void Clear()
  {
    ((PXGraph) this).Clear();
    this.ClearLocalCache();
  }

  private void ClearLocalCache()
  {
    this.costBudgets = (Dictionary<BudgetKeyTuple, PMCostBudget>) null;
    this.revenueBudgets = (Dictionary<BudgetKeyTuple, PMRevenueBudget>) null;
    this.polines = (Dictionary<ChangeOrderEntry.POLineKey, POLinePM>) null;
    this.previousTotals = (Dictionary<BudgetKeyTuple, PMChangeOrderBudget>) null;
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    switch (viewName)
    {
      case "RevenueBudget":
        string str1 = (string) null;
        if (keys.Contains((object) "AccountGroupID"))
        {
          object key = keys[(object) "AccountGroupID"];
          if (key is int)
          {
            PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, (int?) key);
            if (pmAccountGroup != null)
              return pmAccountGroup.Type == "I";
          }
          else
            str1 = (string) keys[(object) "AccountGroupID"];
        }
        if (string.IsNullOrEmpty(str1))
          return true;
        PMAccountGroup pmAccountGroup1 = PXResultset<PMAccountGroup>.op_Implicit(PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.groupCD, Equal<Required<PMAccountGroup.groupCD>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) str1
        }));
        return pmAccountGroup1 != null && pmAccountGroup1.Type == "I";
      case "CostBudget":
        string str2 = (string) null;
        if (keys.Contains((object) "AccountGroupID"))
          str2 = (string) keys[(object) "AccountGroupID"];
        if (string.IsNullOrEmpty(str2))
          return true;
        PMAccountGroup pmAccountGroup2 = PXResultset<PMAccountGroup>.op_Implicit(PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.groupCD, Equal<Required<PMAccountGroup.groupCD>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) str2
        }));
        return pmAccountGroup2 != null && pmAccountGroup2.IsExpense.GetValueOrDefault();
      default:
        return true;
    }
  }

  public bool RowImporting(string viewName, object row) => true;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public class MultiCurrency : ProjectBudgetMultiCurrency<ChangeOrderEntry>
  {
    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[2]
      {
        (PXSelectBase) this.Base.Budget,
        (PXSelectBase) this.Base.ProjectBudgetHistory
      };
    }
  }

  public class ChangeOrderEntry_ActivityDetailsExt : 
    PMActivityDetailsExt<ChangeOrderEntry, PMChangeOrder, PMChangeOrder.noteID>
  {
    public override System.Type GetBAccountIDCommand()
    {
      return typeof (Select<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMChangeOrder.customerID>>>>);
    }

    public override System.Type GetEmailMessageTarget()
    {
      return typeof (Select2<PX.Objects.CR.Contact, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.AR.Customer.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMChangeOrder.customerID>>>>);
    }
  }

  /// <summary>
  /// This class implements graph extension to use reversing change order logic
  /// </summary>
  public class ReversingChangeOrderExt : ChangeOrderExt<ChangeOrderEntry, PMChangeOrder>
  {
    [PXViewName("Change Order")]
    public PXSelect<PMChangeOrder, Where<PMChangeOrder.refNbr, Equal<Current<PMChangeOrder.refNbr>>>> ChangeOrders;

    public override PXSelectBase<PMChangeOrder> ChangeOrder
    {
      get => (PXSelectBase<PMChangeOrder>) this.ChangeOrders;
    }

    public override PMChangeOrder CurrentChangeOrder
    {
      get => ((PXSelectBase<PMChangeOrder>) this.Base.Document).Current;
    }
  }

  [PXHidden]
  [Serializable]
  public class POLineFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _ProjectTaskID;
    protected int? _InventoryID;

    [ProjectTask(typeof (PMProject.contractID), AlwaysEnabled = true)]
    public virtual int? ProjectTaskID
    {
      get => this._ProjectTaskID;
      set => this._ProjectTaskID = value;
    }

    [POVendor]
    public virtual int? VendorID { get; set; }

    [PXDBString(40)]
    [PXUIField(DisplayName = "Vendor Ref.", Enabled = false)]
    public virtual string VendorRefNbr { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "PO Nbr.")]
    [PXSelector(typeof (Search4<PX.Objects.PO.POLine.orderNbr, Where<PX.Objects.PO.POLine.orderType, In3<POOrderType.regularOrder, POOrderType.projectDropShip>, And<PX.Objects.PO.POLine.projectID, Equal<Current<PMChangeOrder.projectID>>, And2<Where<PX.Objects.PO.POLine.cancelled, Equal<False>, Or<Current<ChangeOrderEntry.POLineFilter.includeNonOpen>, Equal<True>>>, And2<Where<PX.Objects.PO.POLine.completed, Equal<False>, Or<Current<ChangeOrderEntry.POLineFilter.includeNonOpen>, Equal<True>>>, And<Where<Current<ChangeOrderEntry.POLineFilter.vendorID>, IsNull, Or<PX.Objects.PO.POLine.vendorID, Equal<Current<ChangeOrderEntry.POLineFilter.vendorID>>>>>>>>>, Aggregate<GroupBy<PX.Objects.PO.POLine.orderType, GroupBy<PX.Objects.PO.POLine.orderNbr, GroupBy<PX.Objects.PO.POLine.vendorID>>>>>), new System.Type[] {typeof (PX.Objects.PO.POLine.orderType), typeof (PX.Objects.PO.POLine.orderNbr), typeof (PX.Objects.PO.POLine.vendorID)})]
    public virtual string POOrderNbr { get; set; }

    [PXDimensionSelector("COSTCODE", typeof (Search<PMCostCode.costCodeCD>))]
    [PXDBString(IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Cost Code From", FieldClass = "COSTCODE")]
    public virtual string CostCodeFrom { get; set; }

    [PXDimensionSelector("COSTCODE", typeof (PMCostCode.costCodeCD))]
    [PXDBString(IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Cost Code To", FieldClass = "COSTCODE")]
    public virtual string CostCodeTo { get; set; }

    [Inventory(Filterable = true)]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Include Non-Open Commitments")]
    public virtual bool? IncludeNonOpen { get; set; }

    public abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ChangeOrderEntry.POLineFilter.projectTaskID>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ChangeOrderEntry.POLineFilter.vendorID>
    {
    }

    public abstract class vendorRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ChangeOrderEntry.POLineFilter.vendorRefNbr>
    {
    }

    public abstract class pOOrderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ChangeOrderEntry.POLineFilter.pOOrderNbr>
    {
    }

    public abstract class costCodeFrom : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ChangeOrderEntry.POLineFilter.costCodeFrom>
    {
    }

    public abstract class costCodeTo : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ChangeOrderEntry.POLineFilter.costCodeTo>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ChangeOrderEntry.POLineFilter.inventoryID>
    {
    }

    public abstract class includeNonOpen : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ChangeOrderEntry.POLineFilter.includeNonOpen>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class CostBudgetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _ProjectTaskID;

    [ProjectTask(typeof (PMProject.contractID), AlwaysEnabled = true, DirtyRead = true)]
    public virtual int? ProjectTaskID
    {
      get => this._ProjectTaskID;
      set => this._ProjectTaskID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Group by Task")]
    public virtual bool? GroupByTask { get; set; }

    public abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ChangeOrderEntry.CostBudgetFilter.projectTaskID>
    {
    }

    public abstract class groupByTask : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ChangeOrderEntry.CostBudgetFilter.groupByTask>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class RevenueBudgetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _ProjectTaskID;

    [ProjectTask(typeof (PMProject.contractID), AlwaysEnabled = true, DirtyRead = true)]
    public virtual int? ProjectTaskID
    {
      get => this._ProjectTaskID;
      set => this._ProjectTaskID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Group by Task")]
    public virtual bool? GroupByTask { get; set; }

    public abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ChangeOrderEntry.RevenueBudgetFilter.projectTaskID>
    {
    }

    public abstract class groupByTask : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ChangeOrderEntry.RevenueBudgetFilter.groupByTask>
    {
    }
  }

  public struct POLineKey(string orderType, string orderNbr, int lineNbr)
  {
    public readonly string OrderType = orderType;
    public readonly string OrderNbr = orderNbr;
    public readonly int LineNbr = lineNbr;

    public override int GetHashCode()
    {
      return ((17 * 23 + this.OrderType.GetHashCode()) * 23 + this.OrderNbr.GetHashCode()) * 23 + this.LineNbr.GetHashCode();
    }
  }
}
