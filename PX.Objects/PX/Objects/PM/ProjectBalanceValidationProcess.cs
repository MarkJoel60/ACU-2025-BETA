// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectBalanceValidationProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class ProjectBalanceValidationProcess : PXGraph<
#nullable disable
ProjectBalanceValidationProcess>
{
  public PXSelect<PMBudgetAccum> Budget;
  public PXSelect<PMForecastHistoryAccum> ForecastHistory;
  public PXSelect<PMHistoryByDateAccum> TranHistory;
  public PXSelect<PMTaskTotal> TaskTotals;
  public PXSelect<PMTaskAllocTotalAccum> AllocationTotals;
  public PXSelect<PMHistoryAccum> History;
  public PXSelectJoin<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<PX.Objects.PO.POLine.orderNbr>>>>, Where<PX.Objects.PO.POLine.projectID, Equal<Required<PX.Objects.PO.POLine.projectID>>>> polines;
  public PXSelect<PMCommitment, Where<PMCommitment.type, Equal<PMCommitmentType.externalType>, And<PMCommitment.projectID, Equal<Required<PMCommitment.projectID>>>>> ExternalCommitments;
  public PXSetup<PMSetup> Setup;
  public PXSelect<PMProject> Project;
  public PXSelect<PMBillingRecord> BillingRecords;
  public FbqlSelect<SelectFromBase<PMProjectBudgetHistoryAccum, TypeArrayOf<IFbqlJoin>.Empty>, PMProjectBudgetHistoryAccum>.View ProjectBudgetHistory;
  public Dictionary<int, PMAccountGroup> AccountGroups;
  public HashSet<int> CostRevTasks;
  private ProjectBalanceValidationProcess.BudgetServiceMassUpdate budgetService;
  private IFinPeriodRepository finPeriodsRepo;

  [POCommitment]
  [PXDBGuid(false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.commitmentID> e)
  {
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.orderType> e)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXParent(typeof (Select<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Current<PX.Objects.PO.POLine.orderNbr>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.orderNbr> e)
  {
  }

  [PXDBDate]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.orderDate> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.vendorID> e)
  {
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.orderType> e)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXParent(typeof (Select<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<PX.Objects.SO.SOLine.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Current<PX.Objects.SO.SOLine.orderNbr>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.orderNbr> e)
  {
  }

  [PXDBDate]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.orderDate> e)
  {
  }

  [PXDefault]
  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCommitment.projectID> e)
  {
  }

  [PXDefault]
  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCommitment.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt(IsKey = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMBudgetAccum.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCommitment.costCodeID> e)
  {
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

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  protected virtual void ClearBalance(PMProject project, PMValidationFilter options)
  {
    if (project == null || options == null)
      return;
    PXDatabase.Delete<PMTaskTotal>(new PXDataFieldRestrict[1]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<PMTaskTotal.projectID>((PXDbType) 8, new int?(4), (object) project.ContractID, (PXComp) 0)
    });
    PXDatabase.Delete<PMTaskAllocTotal>(new PXDataFieldRestrict[1]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<PMTaskAllocTotal.projectID>((PXDbType) 8, new int?(4), (object) project.ContractID, (PXComp) 0)
    });
    PXDatabase.Delete<PMHistory>(new PXDataFieldRestrict[1]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<PMHistory.projectID>((PXDbType) 8, new int?(4), (object) project.ContractID, (PXComp) 0)
    });
    bool? nullable = options.RebuildCommitments;
    if (nullable.GetValueOrDefault())
      PXDatabase.Delete<PMCommitment>(new PXDataFieldRestrict[2]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<PMCommitment.projectID>((PXDbType) 8, new int?(4), (object) project.ContractID, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<PMCommitment.type>((PXDbType) 3, new int?(1), (object) "I", (PXComp) 0)
      });
    nullable = options.RecalculateUnbilledSummary;
    if (nullable.GetValueOrDefault())
      PXDatabase.Delete<PMUnbilledDailySummary>(new PXDataFieldRestrict[1]
      {
        new PXDataFieldRestrict(typeof (PMUnbilledDailySummary.projectID).Name, (PXDbType) 8, new int?(4), (object) project.ContractID, (PXComp) 0)
      });
    nullable = options.RecalculateChangeOrders;
    if (nullable.GetValueOrDefault())
    {
      nullable = project.ChangeOrderWorkflow;
      if (nullable.GetValueOrDefault())
      {
        using (Dictionary<BudgetKeyTuple, ProjectBalanceValidationProcess.PMBudgetLiteEx>.ValueCollection.Enumerator enumerator = this.budgetService.BudgetRecords.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ProjectBalanceValidationProcess.PMBudgetLiteEx current = enumerator.Current;
            PXDatabase.Update<PMBudget>(this.BuildBudgetClearCommandWithChangeOrders(options, current).ToArray());
          }
          goto label_20;
        }
      }
    }
    foreach (int usedAccountGroup in (IEnumerable<int>) this.budgetService.GetUsedAccountGroups())
      PXDatabase.Update<PMBudget>(this.BuildBudgetClearCommand(options, project, new int?(usedAccountGroup)).ToArray());
label_20:
    if (options.RecalculateProjectBudgetHistory.GetValueOrDefault())
      PXDatabase.Delete<PMProjectBudgetHistory>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<PMHistory.projectID>((PXDbType) 8, new int?(4), (object) project.ContractID, (PXComp) 0)
      });
    PXDatabase.Update<PMForecastHistory>(this.BuildForecastClearCommand(options, project.ContractID).ToArray());
    PXDatabase.Update<PMHistoryByDate>(this.BuildTranHistoryClearCommand(options, project.ContractID).ToArray());
  }

  public virtual void RunProjectBalanceVerification(PMValidationFilter options)
  {
    this.RunProjectBalanceVerification(((PXSelectBase<PMProject>) this.Project).Current, options);
  }

  public virtual void RunProjectBalanceVerification(PMProject project, PMValidationFilter options)
  {
    this.budgetService = new ProjectBalanceValidationProcess.BudgetServiceMassUpdate((PXGraph) this, project);
    this.InitAccountGroup();
    this.InitCostRevTasks(project);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.ClearBalance(project, options);
      this.RecalculateBalance(project, options);
      this.PersistCaches();
      this.HandleProjectStatusCode(project, options);
      transactionScope.Complete();
    }
    this.OnCachePersisted();
  }

  /// <summary>
  /// Recalculates the balances for all projects that may be affected when the Account type is modified.
  /// </summary>
  /// <param name="modifiedAccounts">List of modified Accounts.</param>
  public virtual void RebuildBalanceOnAccountTypeChange(IList<PX.Objects.GL.Account> modifiedAccounts)
  {
  }

  protected virtual void PersistCaches()
  {
    ((PXSelectBase) this.Budget).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.Budget).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.ForecastHistory).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.ForecastHistory).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.TranHistory).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.TranHistory).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.TaskTotals).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.TaskTotals).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.AllocationTotals).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.AllocationTotals).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.History).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.History).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.ExternalCommitments).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.ExternalCommitments).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.ExternalCommitments).Cache.Persist((PXDBOperation) 3);
    ((PXSelectBase) this.BillingRecords).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.polines).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.ProjectBudgetHistory).Cache.Persist((PXDBOperation) 2);
    ((PXGraph) this).Caches[typeof (PMUnbilledDailySummaryAccum)].Persist((PXDBOperation) 2);
    ((PXGraph) this).Caches[typeof (PMUnbilledDailySummaryAccum)].Persist((PXDBOperation) 1);
  }

  protected virtual void OnCachePersisted()
  {
    ((PXSelectBase) this.Budget).Cache.Persisted(false);
    ((PXSelectBase) this.ForecastHistory).Cache.Persisted(false);
    ((PXSelectBase) this.TranHistory).Cache.Persisted(false);
    ((PXSelectBase) this.TaskTotals).Cache.Persisted(false);
    ((PXSelectBase) this.AllocationTotals).Cache.Persisted(false);
    ((PXSelectBase) this.History).Cache.Persisted(false);
    ((PXSelectBase) this.ExternalCommitments).Cache.Persisted(false);
    ((PXSelectBase) this.BillingRecords).Cache.Persisted(false);
    ((PXSelectBase) this.polines).Cache.Persisted(false);
    ((PXSelectBase) this.ProjectBudgetHistory).Cache.Persisted(false);
    ((PXGraph) this).Caches[typeof (PMUnbilledDailySummaryAccum)].Persisted(false);
  }

  public virtual void RecalculateBalance(PMProject project, PMValidationFilter options)
  {
    ProjectBalance projectBalance = this.CreateProjectBalance();
    PXSelectBase<PMTran> pxSelectBase = !options.RecalculateUnbilledSummary.GetValueOrDefault() ? (PXSelectBase<PMTran>) new PXSelectGroupBy<PMTran, Where<PMTran.projectID, Equal<Required<PMTran.projectID>>, And<PMTran.released, Equal<True>>>, Aggregate<GroupBy<PMTran.tranType, GroupBy<PMTran.branchID, GroupBy<PMTran.finPeriodID, GroupBy<PMTran.tranPeriodID, GroupBy<PMTran.projectID, GroupBy<PMTran.taskID, GroupBy<PMTran.inventoryID, GroupBy<PMTran.costCodeID, GroupBy<PMTran.accountID, GroupBy<PMTran.accountGroupID, GroupBy<PMTran.offsetAccountID, GroupBy<PMTran.offsetAccountGroupID, GroupBy<PMTran.uOM, GroupBy<PMTran.released, GroupBy<PMTran.remainderOfTranID, Sum<PMTran.qty, Sum<PMTran.amount, Sum<PMTran.projectCuryAmount>>>>>>>>>>>>>>>>>>>>((PXGraph) this) : (PXSelectBase<PMTran>) new PXSelectGroupBy<PMTran, Where<PMTran.projectID, Equal<Required<PMTran.projectID>>, And<PMTran.released, Equal<True>>>, Aggregate<GroupBy<PMTran.tranType, GroupBy<PMTran.branchID, GroupBy<PMTran.finPeriodID, GroupBy<PMTran.tranPeriodID, GroupBy<PMTran.projectID, GroupBy<PMTran.taskID, GroupBy<PMTran.inventoryID, GroupBy<PMTran.costCodeID, GroupBy<PMTran.date, GroupBy<PMTran.accountID, GroupBy<PMTran.accountGroupID, GroupBy<PMTran.offsetAccountID, GroupBy<PMTran.offsetAccountGroupID, GroupBy<PMTran.uOM, GroupBy<PMTran.released, GroupBy<PMTran.remainderOfTranID, GroupBy<PMTran.tranType, GroupBy<PMTran.origModule, GroupBy<PMTran.origTranType, Sum<PMTran.qty, Sum<PMTran.amount, Sum<PMTran.projectCuryAmount, Max<PMTran.billable, GroupBy<PMTran.billed, GroupBy<PMTran.excludedFromBilling>>>>>>>>>>>>>>>>>>>>>>>>>>>((PXGraph) this);
    using (new PXFieldScope(((PXSelectBase) pxSelectBase).View, new Type[27]
    {
      typeof (PMTran.tranID),
      typeof (PMTran.tranType),
      typeof (PMTran.branchID),
      typeof (PMTran.finPeriodID),
      typeof (PMTran.tranPeriodID),
      typeof (PMTran.tranDate),
      typeof (PMTran.date),
      typeof (PMTran.projectID),
      typeof (PMTran.taskID),
      typeof (PMTran.inventoryID),
      typeof (PMTran.costCodeID),
      typeof (PMTran.accountID),
      typeof (PMTran.accountGroupID),
      typeof (PMTran.offsetAccountID),
      typeof (PMTran.offsetAccountGroupID),
      typeof (PMTran.uOM),
      typeof (PMTran.released),
      typeof (PMTran.remainderOfTranID),
      typeof (PMTran.qty),
      typeof (PMTran.amount),
      typeof (PMTran.projectCuryAmount),
      typeof (PMTran.billable),
      typeof (PMTran.billed),
      typeof (PMTran.excludedFromBilling),
      typeof (PMTran.excludedFromBalance),
      typeof (PMTran.origModule),
      typeof (PMTran.origTranType)
    }))
    {
      foreach (PXResult<PMTran> pxResult in pxSelectBase.Select(new object[1]
      {
        (object) project.ContractID
      }))
      {
        PMTran tran = PXResult<PMTran>.op_Implicit(pxResult);
        PMAccountGroup ag = (PMAccountGroup) null;
        PMAccountGroup offsetAg = (PMAccountGroup) null;
        Dictionary<int, PMAccountGroup> accountGroups1 = this.AccountGroups;
        int? nullable = tran.AccountGroupID;
        int key1 = nullable.Value;
        ref PMAccountGroup local1 = ref ag;
        accountGroups1.TryGetValue(key1, out local1);
        nullable = tran.OffsetAccountGroupID;
        if (nullable.HasValue)
        {
          Dictionary<int, PMAccountGroup> accountGroups2 = this.AccountGroups;
          nullable = tran.OffsetAccountGroupID;
          int key2 = nullable.Value;
          ref PMAccountGroup local2 = ref offsetAg;
          accountGroups2.TryGetValue(key2, out local2);
        }
        if (ag == null)
          ag = new PMAccountGroup();
        if (offsetAg == null)
          offsetAg = new PMAccountGroup();
        RegisterReleaseProcess.AddToUnbilledSummary((PXGraph) this, tran);
        if (!tran.ExcludedFromBalance.GetValueOrDefault())
        {
          try
          {
            this.ProcessTransaction(project, tran, ag, offsetAg, projectBalance);
          }
          catch (PXUnitConversionException ex)
          {
            PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, tran.InventoryID);
            string screenName = this.GetScreenName(inventoryItem?.CreatedByScreenID);
            object[] objArray = new object[4]
            {
              (object) tran.UOM,
              (object) inventoryItem?.BaseUnit,
              (object) inventoryItem?.InventoryCD,
              (object) screenName
            };
            throw new PXException((Exception) ex, "Project balance cannot be recalculated because conversion from {0} to {1} is not defined for the {2} item on the {3} form. Specify the unit conversion rule, and recalculate the project balance again.", objArray);
          }
        }
      }
    }
    this.RebuildAllocationTotals(project);
    if (options.RebuildCommitments.GetValueOrDefault())
      this.ProcessCommitments(project);
    if (options.RecalculateDraftInvoicesAmount.GetValueOrDefault())
      this.RecalculateDraftInvoicesAmount(project, projectBalance);
    if (options.RecalculateChangeOrders.GetValueOrDefault() && project.ChangeOrderWorkflow.GetValueOrDefault())
    {
      this.RecalculateChangeRequests(project, projectBalance);
      this.RecalculateChangeOrders(project, projectBalance);
    }
    this.InitCostCodeOnModifiedEntities();
    if (PXAccess.FeatureInstalled<FeaturesSet.retainage>())
      this.ProcessRetainage(project, options);
    if (((PXSelectBase<PMSetup>) this.Setup).Current.MigrationMode.GetValueOrDefault())
      this.RestoreBillingRecords(project);
    if (PXAccess.FeatureInstalled<FeaturesSet.construction>())
      this.ProcessProgressWorksheets(projectBalance, project, options);
    this.ProcessInclusiveTaxes(project, options);
    if (!options.RecalculateProjectBudgetHistory.GetValueOrDefault())
      return;
    this.FillBudgetHistory(project);
  }

  public virtual void FillBudgetHistory(PMProject project)
  {
    foreach (PXResult<PMBudget> pxResult in PXSelectBase<PMBudget, PXViewOf<PMBudget>.BasedOn<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMBudget.projectID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMBudget pmBudget = PXResult<PMBudget>.op_Implicit(pxResult);
      PMProjectBudgetHistoryAccum budgetHistoryAccum1 = new PMProjectBudgetHistoryAccum();
      budgetHistoryAccum1.Date = project.StartDate;
      budgetHistoryAccum1.ProjectID = project.ContractID;
      budgetHistoryAccum1.TaskID = pmBudget.TaskID;
      budgetHistoryAccum1.AccountGroupID = pmBudget.AccountGroupID;
      budgetHistoryAccum1.InventoryID = pmBudget.InventoryID;
      budgetHistoryAccum1.CostCodeID = pmBudget.CostCodeID;
      budgetHistoryAccum1.ChangeOrderRefNbr = "X";
      PMProjectBudgetHistoryAccum budgetHistoryAccum2 = ((PXSelectBase<PMProjectBudgetHistoryAccum>) this.ProjectBudgetHistory).Insert(budgetHistoryAccum1);
      budgetHistoryAccum2.Type = pmBudget.Type;
      budgetHistoryAccum2.CuryInfoID = project.CuryInfoID;
      budgetHistoryAccum2.UOM = pmBudget.UOM;
      PMProjectBudgetHistoryAccum budgetHistoryAccum3 = budgetHistoryAccum2;
      Decimal? nullable1 = budgetHistoryAccum3.RevisedBudgetQty;
      Decimal? nullable2 = pmBudget.Qty;
      budgetHistoryAccum3.RevisedBudgetQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      PMProjectBudgetHistoryAccum budgetHistoryAccum4 = budgetHistoryAccum2;
      nullable2 = budgetHistoryAccum4.CuryRevisedBudgetAmt;
      nullable1 = pmBudget.CuryAmount;
      budgetHistoryAccum4.CuryRevisedBudgetAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    BudgetService budgetService = new BudgetService((PXGraph) this);
    ProjectBalance projectBalance = this.CreateProjectBalance();
    foreach (PXResult<PMChangeOrderBudget, PMChangeOrder> pxResult in PXSelectBase<PMChangeOrderBudget, PXViewOf<PMChangeOrderBudget>.BasedOn<SelectFromBase<PMChangeOrderBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<PMChangeOrderBudget.refNbr, IBqlString>.IsEqual<PMChangeOrder.refNbr>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrderBudget.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMChangeOrder.released, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMChangeOrder pmChangeOrder = PXResult<PMChangeOrderBudget, PMChangeOrder>.op_Implicit(pxResult);
      PMChangeOrderBudget changeOrderBudget = PXResult<PMChangeOrderBudget, PMChangeOrder>.op_Implicit(pxResult);
      PMAccountGroup ag = (PMAccountGroup) null;
      this.AccountGroups.TryGetValue(changeOrderBudget.AccountGroupID.Value, out ag);
      bool isExisting;
      PX.Objects.PM.Lite.PMBudget pmBudget = budgetService.SelectProjectBalance((IProjectFilter) changeOrderBudget, ag, project, out isExisting);
      PMProjectBudgetHistoryAccum budgetHistoryAccum5 = new PMProjectBudgetHistoryAccum();
      budgetHistoryAccum5.Date = pmChangeOrder.Date;
      budgetHistoryAccum5.ProjectID = pmBudget.ProjectID;
      budgetHistoryAccum5.TaskID = pmBudget.ProjectTaskID;
      budgetHistoryAccum5.AccountGroupID = pmBudget.AccountGroupID;
      budgetHistoryAccum5.InventoryID = pmBudget.InventoryID;
      budgetHistoryAccum5.CostCodeID = pmBudget.CostCodeID;
      budgetHistoryAccum5.ChangeOrderRefNbr = pmChangeOrder.RefNbr;
      PMProjectBudgetHistoryAccum budget = ((PXSelectBase<PMProjectBudgetHistoryAccum>) this.ProjectBudgetHistory).Insert(budgetHistoryAccum5);
      budget.Type = changeOrderBudget.Type;
      budget.CuryInfoID = project.CuryInfoID;
      budget.UOM = isExisting ? pmBudget.UOM : changeOrderBudget.UOM ?? pmBudget.UOM;
      Decimal rollupQty = projectBalance.CalculateRollupQty<PMChangeOrderBudget>(changeOrderBudget, (IQuantify) budget);
      PMProjectBudgetHistoryAccum budgetHistoryAccum6 = budget;
      Decimal? nullable3 = budgetHistoryAccum6.RevisedBudgetQty;
      Decimal num = rollupQty;
      Decimal? nullable4;
      Decimal? nullable5;
      if (!nullable3.HasValue)
      {
        nullable4 = new Decimal?();
        nullable5 = nullable4;
      }
      else
        nullable5 = new Decimal?(nullable3.GetValueOrDefault() + num);
      budgetHistoryAccum6.RevisedBudgetQty = nullable5;
      PMProjectBudgetHistoryAccum budgetHistoryAccum7 = budget;
      nullable3 = budgetHistoryAccum7.CuryRevisedBudgetAmt;
      nullable4 = changeOrderBudget.Amount;
      budgetHistoryAccum7.CuryRevisedBudgetAmt = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    }
  }

  public virtual void ProcessTransaction(
    PMProject project,
    PMTran tran,
    PMAccountGroup ag,
    PMAccountGroup offsetAg,
    ProjectBalance pb)
  {
    foreach (ProjectBalance.Result result in (IEnumerable<ProjectBalance.Result>) pb.Calculate(project, tran, ag, offsetAg))
    {
      Decimal? nullable1;
      if (result.Status != null)
      {
        PMBudgetAccum pmBudgetAccum1 = new PMBudgetAccum();
        pmBudgetAccum1.ProjectID = result.Status.ProjectID;
        pmBudgetAccum1.ProjectTaskID = result.Status.ProjectTaskID;
        pmBudgetAccum1.AccountGroupID = result.Status.AccountGroupID;
        pmBudgetAccum1.InventoryID = result.Status.InventoryID;
        pmBudgetAccum1.CostCodeID = result.Status.CostCodeID;
        pmBudgetAccum1.UOM = result.Status.UOM;
        pmBudgetAccum1.IsProduction = result.Status.IsProduction;
        pmBudgetAccum1.Type = result.Status.Type;
        pmBudgetAccum1.Description = result.Status.Description;
        pmBudgetAccum1.CuryInfoID = result.Status.CuryInfoID;
        PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
        if (pmBudgetAccum2.Type == "E" && this.CostRevTasks != null && this.CostRevTasks.Contains(pmBudgetAccum2.ProjectTaskID.Value))
          pmBudgetAccum2.RevenueTaskID = pmBudgetAccum2.ProjectTaskID;
        PMBudgetAccum pmBudgetAccum3 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(pmBudgetAccum2);
        PMBudgetAccum pmBudgetAccum4 = pmBudgetAccum3;
        nullable1 = pmBudgetAccum4.ActualQty;
        Decimal valueOrDefault1 = result.Status.ActualQty.GetValueOrDefault();
        pmBudgetAccum4.ActualQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
        PMBudgetAccum pmBudgetAccum5 = pmBudgetAccum3;
        nullable1 = pmBudgetAccum5.CuryActualAmount;
        Decimal valueOrDefault2 = result.Status.CuryActualAmount.GetValueOrDefault();
        pmBudgetAccum5.CuryActualAmount = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
        PMBudgetAccum pmBudgetAccum6 = pmBudgetAccum3;
        nullable1 = pmBudgetAccum6.ActualAmount;
        Decimal valueOrDefault3 = result.Status.ActualAmount.GetValueOrDefault();
        pmBudgetAccum6.ActualAmount = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
        pmBudgetAccum3.ProgressBillingBase = (string) null;
        PMHistoryByDateAccum historyByDateAccum1 = new PMHistoryByDateAccum();
        historyByDateAccum1.ProjectID = tran.ProjectID;
        historyByDateAccum1.ProjectTaskID = tran.TaskID;
        historyByDateAccum1.AccountGroupID = tran.AccountGroupID ?? pmBudgetAccum3.AccountGroupID;
        historyByDateAccum1.InventoryID = tran.InventoryID ?? pmBudgetAccum3.InventoryID;
        historyByDateAccum1.CostCodeID = tran.CostCodeID ?? pmBudgetAccum3.CostCodeID;
        historyByDateAccum1.Date = tran.Date;
        historyByDateAccum1.PeriodID = tran.TranPeriodID;
        PMHistoryByDateAccum historyByDateAccum2 = ((PXSelectBase<PMHistoryByDateAccum>) this.TranHistory).Insert(historyByDateAccum1);
        PMHistoryByDateAccum historyByDateAccum3 = historyByDateAccum2;
        nullable1 = historyByDateAccum3.ActualQty;
        Decimal valueOrDefault4 = result.Status.ActualQty.GetValueOrDefault();
        historyByDateAccum3.ActualQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
        PMHistoryByDateAccum historyByDateAccum4 = historyByDateAccum2;
        nullable1 = historyByDateAccum4.CuryActualAmount;
        Decimal valueOrDefault5 = result.Status.CuryActualAmount.GetValueOrDefault();
        historyByDateAccum4.CuryActualAmount = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault5) : new Decimal?();
        PMHistoryByDateAccum historyByDateAccum5 = historyByDateAccum2;
        nullable1 = historyByDateAccum5.ActualAmount;
        Decimal valueOrDefault6 = result.Status.ActualAmount.GetValueOrDefault();
        historyByDateAccum5.ActualAmount = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault6) : new Decimal?();
      }
      Decimal? nullable2;
      if (result.ForecastHistory != null)
      {
        PMForecastHistoryAccum forecastHistoryAccum1 = new PMForecastHistoryAccum();
        forecastHistoryAccum1.ProjectID = result.ForecastHistory.ProjectID;
        forecastHistoryAccum1.ProjectTaskID = result.ForecastHistory.ProjectTaskID;
        forecastHistoryAccum1.AccountGroupID = result.ForecastHistory.AccountGroupID;
        forecastHistoryAccum1.InventoryID = result.ForecastHistory.InventoryID;
        forecastHistoryAccum1.CostCodeID = result.ForecastHistory.CostCodeID;
        forecastHistoryAccum1.PeriodID = result.ForecastHistory.PeriodID;
        PMForecastHistoryAccum forecastHistoryAccum2 = ((PXSelectBase<PMForecastHistoryAccum>) this.ForecastHistory).Insert(forecastHistoryAccum1);
        PMForecastHistoryAccum forecastHistoryAccum3 = forecastHistoryAccum2;
        nullable1 = forecastHistoryAccum3.ActualQty;
        nullable2 = result.ForecastHistory.ActualQty;
        Decimal valueOrDefault7 = nullable2.GetValueOrDefault();
        Decimal? nullable3;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault7);
        forecastHistoryAccum3.ActualQty = nullable3;
        PMForecastHistoryAccum forecastHistoryAccum4 = forecastHistoryAccum2;
        nullable1 = forecastHistoryAccum4.CuryActualAmount;
        nullable2 = result.ForecastHistory.CuryActualAmount;
        Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
        Decimal? nullable4;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault8);
        forecastHistoryAccum4.CuryActualAmount = nullable4;
        PMForecastHistoryAccum forecastHistoryAccum5 = forecastHistoryAccum2;
        nullable1 = forecastHistoryAccum5.ActualAmount;
        nullable2 = result.ForecastHistory.ActualAmount;
        Decimal valueOrDefault9 = nullable2.GetValueOrDefault();
        Decimal? nullable5;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable5 = nullable2;
        }
        else
          nullable5 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault9);
        forecastHistoryAccum5.ActualAmount = nullable5;
        PMForecastHistoryAccum forecastHistoryAccum6 = forecastHistoryAccum2;
        nullable1 = forecastHistoryAccum6.CuryArAmount;
        nullable2 = result.ForecastHistory.CuryArAmount;
        Decimal valueOrDefault10 = nullable2.GetValueOrDefault();
        Decimal? nullable6;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable6 = nullable2;
        }
        else
          nullable6 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault10);
        forecastHistoryAccum6.CuryArAmount = nullable6;
      }
      if (result.TaskTotal != null)
      {
        PMTaskTotal pmTaskTotal1 = ((PXSelectBase<PMTaskTotal>) this.TaskTotals).Insert(new PMTaskTotal()
        {
          ProjectID = result.TaskTotal.ProjectID,
          TaskID = result.TaskTotal.TaskID
        });
        PMTaskTotal pmTaskTotal2 = pmTaskTotal1;
        nullable1 = pmTaskTotal2.CuryAsset;
        nullable2 = result.TaskTotal.CuryAsset;
        Decimal valueOrDefault11 = nullable2.GetValueOrDefault();
        Decimal? nullable7;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable7 = nullable2;
        }
        else
          nullable7 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault11);
        pmTaskTotal2.CuryAsset = nullable7;
        PMTaskTotal pmTaskTotal3 = pmTaskTotal1;
        nullable1 = pmTaskTotal3.Asset;
        nullable2 = result.TaskTotal.Asset;
        Decimal valueOrDefault12 = nullable2.GetValueOrDefault();
        Decimal? nullable8;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable8 = nullable2;
        }
        else
          nullable8 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault12);
        pmTaskTotal3.Asset = nullable8;
        PMTaskTotal pmTaskTotal4 = pmTaskTotal1;
        nullable1 = pmTaskTotal4.CuryLiability;
        nullable2 = result.TaskTotal.CuryLiability;
        Decimal valueOrDefault13 = nullable2.GetValueOrDefault();
        Decimal? nullable9;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable9 = nullable2;
        }
        else
          nullable9 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault13);
        pmTaskTotal4.CuryLiability = nullable9;
        PMTaskTotal pmTaskTotal5 = pmTaskTotal1;
        nullable1 = pmTaskTotal5.Liability;
        nullable2 = result.TaskTotal.Liability;
        Decimal valueOrDefault14 = nullable2.GetValueOrDefault();
        Decimal? nullable10;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable10 = nullable2;
        }
        else
          nullable10 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault14);
        pmTaskTotal5.Liability = nullable10;
        PMTaskTotal pmTaskTotal6 = pmTaskTotal1;
        nullable1 = pmTaskTotal6.CuryIncome;
        nullable2 = result.TaskTotal.CuryIncome;
        Decimal valueOrDefault15 = nullable2.GetValueOrDefault();
        Decimal? nullable11;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable11 = nullable2;
        }
        else
          nullable11 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault15);
        pmTaskTotal6.CuryIncome = nullable11;
        PMTaskTotal pmTaskTotal7 = pmTaskTotal1;
        nullable1 = pmTaskTotal7.Income;
        nullable2 = result.TaskTotal.Income;
        Decimal valueOrDefault16 = nullable2.GetValueOrDefault();
        Decimal? nullable12;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable12 = nullable2;
        }
        else
          nullable12 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault16);
        pmTaskTotal7.Income = nullable12;
        PMTaskTotal pmTaskTotal8 = pmTaskTotal1;
        nullable1 = pmTaskTotal8.CuryExpense;
        nullable2 = result.TaskTotal.CuryExpense;
        Decimal valueOrDefault17 = nullable2.GetValueOrDefault();
        Decimal? nullable13;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable13 = nullable2;
        }
        else
          nullable13 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault17);
        pmTaskTotal8.CuryExpense = nullable13;
        PMTaskTotal pmTaskTotal9 = pmTaskTotal1;
        nullable1 = pmTaskTotal9.Expense;
        nullable2 = result.TaskTotal.Expense;
        Decimal valueOrDefault18 = nullable2.GetValueOrDefault();
        Decimal? nullable14;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable14 = nullable2;
        }
        else
          nullable14 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault18);
        pmTaskTotal9.Expense = nullable14;
      }
      foreach (PMHistory pmHistory in (IEnumerable<PMHistory>) result.History)
      {
        PMHistoryAccum pmHistoryAccum1 = new PMHistoryAccum();
        pmHistoryAccum1.ProjectID = pmHistory.ProjectID;
        pmHistoryAccum1.ProjectTaskID = pmHistory.ProjectTaskID;
        pmHistoryAccum1.AccountGroupID = pmHistory.AccountGroupID;
        pmHistoryAccum1.InventoryID = pmHistory.InventoryID;
        pmHistoryAccum1.CostCodeID = pmHistory.CostCodeID;
        pmHistoryAccum1.PeriodID = pmHistory.PeriodID;
        pmHistoryAccum1.BranchID = pmHistory.BranchID;
        PMHistoryAccum pmHistoryAccum2 = ((PXSelectBase<PMHistoryAccum>) this.History).Insert(pmHistoryAccum1);
        PMHistoryAccum pmHistoryAccum3 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum3.FinPTDCuryAmount;
        nullable2 = pmHistory.FinPTDCuryAmount;
        Decimal valueOrDefault19 = nullable2.GetValueOrDefault();
        Decimal? nullable15;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable15 = nullable2;
        }
        else
          nullable15 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault19);
        pmHistoryAccum3.FinPTDCuryAmount = nullable15;
        PMHistoryAccum pmHistoryAccum4 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum4.FinPTDAmount;
        nullable2 = pmHistory.FinPTDAmount;
        Decimal valueOrDefault20 = nullable2.GetValueOrDefault();
        Decimal? nullable16;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable16 = nullable2;
        }
        else
          nullable16 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault20);
        pmHistoryAccum4.FinPTDAmount = nullable16;
        PMHistoryAccum pmHistoryAccum5 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum5.FinYTDCuryAmount;
        nullable2 = pmHistory.FinYTDCuryAmount;
        Decimal valueOrDefault21 = nullable2.GetValueOrDefault();
        Decimal? nullable17;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable17 = nullable2;
        }
        else
          nullable17 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault21);
        pmHistoryAccum5.FinYTDCuryAmount = nullable17;
        PMHistoryAccum pmHistoryAccum6 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum6.FinYTDAmount;
        nullable2 = pmHistory.FinYTDAmount;
        Decimal valueOrDefault22 = nullable2.GetValueOrDefault();
        Decimal? nullable18;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable18 = nullable2;
        }
        else
          nullable18 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault22);
        pmHistoryAccum6.FinYTDAmount = nullable18;
        PMHistoryAccum pmHistoryAccum7 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum7.FinPTDQty;
        nullable2 = pmHistory.FinPTDQty;
        Decimal valueOrDefault23 = nullable2.GetValueOrDefault();
        Decimal? nullable19;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable19 = nullable2;
        }
        else
          nullable19 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault23);
        pmHistoryAccum7.FinPTDQty = nullable19;
        PMHistoryAccum pmHistoryAccum8 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum8.FinYTDQty;
        nullable2 = pmHistory.FinYTDQty;
        Decimal valueOrDefault24 = nullable2.GetValueOrDefault();
        Decimal? nullable20;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable20 = nullable2;
        }
        else
          nullable20 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault24);
        pmHistoryAccum8.FinYTDQty = nullable20;
        PMHistoryAccum pmHistoryAccum9 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum9.TranPTDCuryAmount;
        nullable2 = pmHistory.TranPTDCuryAmount;
        Decimal valueOrDefault25 = nullable2.GetValueOrDefault();
        Decimal? nullable21;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable21 = nullable2;
        }
        else
          nullable21 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault25);
        pmHistoryAccum9.TranPTDCuryAmount = nullable21;
        PMHistoryAccum pmHistoryAccum10 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum10.TranPTDAmount;
        nullable2 = pmHistory.TranPTDAmount;
        Decimal valueOrDefault26 = nullable2.GetValueOrDefault();
        Decimal? nullable22;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable22 = nullable2;
        }
        else
          nullable22 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault26);
        pmHistoryAccum10.TranPTDAmount = nullable22;
        PMHistoryAccum pmHistoryAccum11 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum11.TranYTDCuryAmount;
        nullable2 = pmHistory.TranYTDCuryAmount;
        Decimal valueOrDefault27 = nullable2.GetValueOrDefault();
        Decimal? nullable23;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable23 = nullable2;
        }
        else
          nullable23 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault27);
        pmHistoryAccum11.TranYTDCuryAmount = nullable23;
        PMHistoryAccum pmHistoryAccum12 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum12.TranYTDAmount;
        nullable2 = pmHistory.TranYTDAmount;
        Decimal valueOrDefault28 = nullable2.GetValueOrDefault();
        Decimal? nullable24;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable24 = nullable2;
        }
        else
          nullable24 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault28);
        pmHistoryAccum12.TranYTDAmount = nullable24;
        PMHistoryAccum pmHistoryAccum13 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum13.TranPTDQty;
        nullable2 = pmHistory.TranPTDQty;
        Decimal valueOrDefault29 = nullable2.GetValueOrDefault();
        Decimal? nullable25;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable25 = nullable2;
        }
        else
          nullable25 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault29);
        pmHistoryAccum13.TranPTDQty = nullable25;
        PMHistoryAccum pmHistoryAccum14 = pmHistoryAccum2;
        nullable1 = pmHistoryAccum14.TranYTDQty;
        nullable2 = pmHistory.TranYTDQty;
        Decimal valueOrDefault30 = nullable2.GetValueOrDefault();
        Decimal? nullable26;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable26 = nullable2;
        }
        else
          nullable26 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault30);
        pmHistoryAccum14.TranYTDQty = nullable26;
      }
    }
  }

  public virtual void RebuildAllocationTotals(PMProject project)
  {
    foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) new PXSelectJoinGroupBy<PMTran, LeftJoin<ProjectBalanceValidationProcess.PMTranAllocReversal, On<ProjectBalanceValidationProcess.PMTranAllocReversal.origTranID, Equal<PMTran.tranID>>>, Where<PMTran.origProjectID, Equal<Required<PMTran.origProjectID>>, And<PMTran.origTaskID, IsNotNull, And<PMTran.origAccountGroupID, IsNotNull, And<Where<ProjectBalanceValidationProcess.PMTranAllocReversal.tranID, IsNull, Or<ProjectBalanceValidationProcess.PMTranAllocReversal.origDocType, Equal<PMOrigDocType.allocationReversal>>>>>>>, Aggregate<GroupBy<PMTran.origProjectID, GroupBy<PMTran.origTaskID, GroupBy<PMTran.origAccountGroupID, GroupBy<PMTran.inventoryID, GroupBy<PMTran.costCodeID, Sum<PMTran.projectCuryAmount, Sum<PMTran.qty>>>>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMTran pmTran = PXResult<PMTran>.op_Implicit(pxResult);
      PMTaskAllocTotalAccum taskAllocTotalAccum1 = new PMTaskAllocTotalAccum();
      taskAllocTotalAccum1.ProjectID = pmTran.OrigProjectID;
      taskAllocTotalAccum1.TaskID = pmTran.OrigTaskID;
      taskAllocTotalAccum1.AccountGroupID = pmTran.OrigAccountGroupID;
      PMTaskAllocTotalAccum taskAllocTotalAccum2 = taskAllocTotalAccum1;
      int? nullable1 = pmTran.InventoryID;
      int? nullable2 = new int?(nullable1 ?? PMInventorySelectorAttribute.EmptyInventoryID);
      taskAllocTotalAccum2.InventoryID = nullable2;
      PMTaskAllocTotalAccum taskAllocTotalAccum3 = taskAllocTotalAccum1;
      nullable1 = pmTran.CostCodeID;
      int? nullable3 = new int?(nullable1 ?? CostCodeAttribute.GetDefaultCostCode());
      taskAllocTotalAccum3.CostCodeID = nullable3;
      PMTaskAllocTotalAccum taskAllocTotalAccum4 = ((PXSelectBase<PMTaskAllocTotalAccum>) this.AllocationTotals).Insert(taskAllocTotalAccum1);
      PMTaskAllocTotalAccum taskAllocTotalAccum5 = taskAllocTotalAccum4;
      Decimal? nullable4 = taskAllocTotalAccum5.Amount;
      Decimal valueOrDefault1 = pmTran.ProjectCuryAmount.GetValueOrDefault();
      taskAllocTotalAccum5.Amount = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
      PMTaskAllocTotalAccum taskAllocTotalAccum6 = taskAllocTotalAccum4;
      nullable4 = taskAllocTotalAccum6.Quantity;
      Decimal valueOrDefault2 = pmTran.Qty.GetValueOrDefault();
      taskAllocTotalAccum6.Quantity = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    }
  }

  public virtual void ProcessCommitments(PMProject project)
  {
    foreach (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder> pxResult in ((PXSelectBase<PX.Objects.PO.POLine>) this.polines).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PX.Objects.PO.POLine data = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
      PX.Objects.PO.POOrder poOrder = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
      PXParentAttribute.SetParent(((PXSelectBase) this.polines).Cache, (object) data, typeof (PX.Objects.PO.POOrder), (object) poOrder);
      PMCommitmentAttribute.Sync(((PXSelectBase) this.polines).Cache, (object) data);
    }
  }

  public virtual void RecalculateDraftInvoicesAmount(PMProject project, ProjectBalance pb)
  {
    PXSelectJoinGroupBy<PMProformaLine, InnerJoin<PX.Objects.GL.Account, On<PMProformaLine.accountID, Equal<PX.Objects.GL.Account.accountID>>, InnerJoin<PMProforma, On<PMProformaLine.refNbr, Equal<PMProforma.refNbr>, And<PMProformaLine.revisionID, Equal<PMProforma.revisionID>>>, InnerJoin<PX.Objects.GL.Branch, On<PMProforma.branchID, Equal<PX.Objects.GL.Branch.branchID>>>>>, Where<PMProformaLine.projectID, Equal<Required<PMProformaLine.projectID>>, And<PMProformaLine.released, Equal<False>, And<PMProformaLine.corrected, Equal<False>, And<PX.Objects.GL.Account.accountGroupID, IsNotNull>>>>, Aggregate<GroupBy<PMProformaLine.projectID, GroupBy<PMProformaLine.taskID, GroupBy<PMProformaLine.accountID, GroupBy<PMProformaLine.inventoryID, GroupBy<PMProformaLine.costCodeID, GroupBy<PMProforma.branchID, GroupBy<PMProformaLine.uOM, Sum<PMProformaLine.curyLineTotal, Sum<PMProformaLine.lineTotal, Sum<PMProformaLine.qty>>>>>>>>>>>> selectJoinGroupBy1 = new PXSelectJoinGroupBy<PMProformaLine, InnerJoin<PX.Objects.GL.Account, On<PMProformaLine.accountID, Equal<PX.Objects.GL.Account.accountID>>, InnerJoin<PMProforma, On<PMProformaLine.refNbr, Equal<PMProforma.refNbr>, And<PMProformaLine.revisionID, Equal<PMProforma.revisionID>>>, InnerJoin<PX.Objects.GL.Branch, On<PMProforma.branchID, Equal<PX.Objects.GL.Branch.branchID>>>>>, Where<PMProformaLine.projectID, Equal<Required<PMProformaLine.projectID>>, And<PMProformaLine.released, Equal<False>, And<PMProformaLine.corrected, Equal<False>, And<PX.Objects.GL.Account.accountGroupID, IsNotNull>>>>, Aggregate<GroupBy<PMProformaLine.projectID, GroupBy<PMProformaLine.taskID, GroupBy<PMProformaLine.accountID, GroupBy<PMProformaLine.inventoryID, GroupBy<PMProformaLine.costCodeID, GroupBy<PMProforma.branchID, GroupBy<PMProformaLine.uOM, Sum<PMProformaLine.curyLineTotal, Sum<PMProformaLine.lineTotal, Sum<PMProformaLine.qty>>>>>>>>>>>>((PXGraph) this);
    PXSelectJoinGroupBy<PX.Objects.AR.ARTran, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.AR.ARTran.accountID, Equal<PX.Objects.GL.Account.accountID>>, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARTran.refNbr, Equal<PX.Objects.AR.ARRegister.refNbr>, And<PX.Objects.AR.ARTran.tranType, Equal<PX.Objects.AR.ARRegister.docType>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.AR.ARRegister.branchID, Equal<PX.Objects.GL.Branch.branchID>>>>>, Where<PX.Objects.AR.ARTran.projectID, Equal<Required<PX.Objects.AR.ARTran.projectID>>, And<PX.Objects.AR.ARTran.released, Equal<False>, And<PX.Objects.GL.Account.accountGroupID, IsNotNull, And<PX.Objects.AR.ARRegister.scheduled, Equal<False>, And<PX.Objects.AR.ARRegister.voided, Equal<False>>>>>>, Aggregate<GroupBy<PX.Objects.AR.ARTran.tranType, GroupBy<PX.Objects.AR.ARTran.projectID, GroupBy<PX.Objects.AR.ARTran.taskID, GroupBy<PX.Objects.AR.ARTran.accountID, GroupBy<PX.Objects.AR.ARTran.inventoryID, GroupBy<PX.Objects.AR.ARTran.costCodeID, GroupBy<PX.Objects.AR.ARRegister.branchID, GroupBy<PX.Objects.AR.ARTran.uOM, Sum<PX.Objects.AR.ARTran.curyExtPrice, Sum<PX.Objects.AR.ARTran.extPrice, Sum<PX.Objects.AR.ARTran.qty>>>>>>>>>>>>> selectJoinGroupBy2 = new PXSelectJoinGroupBy<PX.Objects.AR.ARTran, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.AR.ARTran.accountID, Equal<PX.Objects.GL.Account.accountID>>, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARTran.refNbr, Equal<PX.Objects.AR.ARRegister.refNbr>, And<PX.Objects.AR.ARTran.tranType, Equal<PX.Objects.AR.ARRegister.docType>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.AR.ARRegister.branchID, Equal<PX.Objects.GL.Branch.branchID>>>>>, Where<PX.Objects.AR.ARTran.projectID, Equal<Required<PX.Objects.AR.ARTran.projectID>>, And<PX.Objects.AR.ARTran.released, Equal<False>, And<PX.Objects.GL.Account.accountGroupID, IsNotNull, And<PX.Objects.AR.ARRegister.scheduled, Equal<False>, And<PX.Objects.AR.ARRegister.voided, Equal<False>>>>>>, Aggregate<GroupBy<PX.Objects.AR.ARTran.tranType, GroupBy<PX.Objects.AR.ARTran.projectID, GroupBy<PX.Objects.AR.ARTran.taskID, GroupBy<PX.Objects.AR.ARTran.accountID, GroupBy<PX.Objects.AR.ARTran.inventoryID, GroupBy<PX.Objects.AR.ARTran.costCodeID, GroupBy<PX.Objects.AR.ARRegister.branchID, GroupBy<PX.Objects.AR.ARTran.uOM, Sum<PX.Objects.AR.ARTran.curyExtPrice, Sum<PX.Objects.AR.ARTran.extPrice, Sum<PX.Objects.AR.ARTran.qty>>>>>>>>>>>>>((PXGraph) this);
    object[] objArray = new object[1]
    {
      (object) project.ContractID
    };
    foreach (PXResult<PMProformaLine, PX.Objects.GL.Account, PMProforma, PX.Objects.GL.Branch> pxResult in ((PXSelectBase<PMProformaLine>) selectJoinGroupBy1).Select(objArray))
    {
      PMProformaLine line = PXResult<PMProformaLine, PX.Objects.GL.Account, PMProforma, PX.Objects.GL.Branch>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<PMProformaLine, PX.Objects.GL.Account, PMProforma, PX.Objects.GL.Branch>.op_Implicit(pxResult);
      PMProforma pmProforma = PXResult<PMProformaLine, PX.Objects.GL.Account, PMProforma, PX.Objects.GL.Branch>.op_Implicit(pxResult);
      PXResult<PMProformaLine, PX.Objects.GL.Account, PMProforma, PX.Objects.GL.Branch>.op_Implicit(pxResult);
      bool? nullable1 = pmProforma.IsMigratedRecord;
      if (!nullable1.GetValueOrDefault() || !(line.Type == "T"))
      {
        PMBudgetAccum targetBudget = this.GetTargetBudget(project, account.AccountGroupID, line);
        if (targetBudget != null)
        {
          PMBudgetAccum pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(targetBudget);
          Decimal inProjectCurrency = this.MultiCurrencyService.GetValueInProjectCurrency((PXGraph) this, project, pmProforma.CuryID, pmProforma.InvoiceDate, line.CuryLineTotal);
          string uom1 = line.UOM;
          string uom2 = pmBudgetAccum1.UOM;
          Decimal? nullable2 = line.Qty;
          Decimal valueOrDefault = nullable2.GetValueOrDefault();
          Decimal num1;
          ref Decimal local = ref num1;
          INUnitAttribute.TryConvertGlobalUnits((PXGraph) this, uom1, uom2, valueOrDefault, INPrecision.QUANTITY, out local);
          PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
          nullable2 = pmBudgetAccum2.CuryInvoicedAmount;
          Decimal num2 = inProjectCurrency;
          pmBudgetAccum2.CuryInvoicedAmount = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num2) : new Decimal?();
          PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
          nullable2 = pmBudgetAccum3.InvoicedQty;
          Decimal num3 = num1;
          pmBudgetAccum3.InvoicedQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num3) : new Decimal?();
          pmBudgetAccum1.ProgressBillingBase = (string) null;
          nullable1 = line.IsPrepayment;
          if (nullable1.GetValueOrDefault())
          {
            PMBudgetAccum pmBudgetAccum4 = pmBudgetAccum1;
            nullable2 = pmBudgetAccum4.CuryPrepaymentInvoiced;
            Decimal num4 = inProjectCurrency;
            pmBudgetAccum4.CuryPrepaymentInvoiced = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num4) : new Decimal?();
          }
        }
      }
    }
    foreach (PXResult<PX.Objects.AR.ARTran, PX.Objects.GL.Account, PX.Objects.AR.ARRegister, PX.Objects.GL.Branch> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) selectJoinGroupBy2).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PX.Objects.AR.ARTran line = PXResult<PX.Objects.AR.ARTran, PX.Objects.GL.Account, PX.Objects.AR.ARRegister, PX.Objects.GL.Branch>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<PX.Objects.AR.ARTran, PX.Objects.GL.Account, PX.Objects.AR.ARRegister, PX.Objects.GL.Branch>.op_Implicit(pxResult);
      PX.Objects.AR.ARRegister arRegister = PXResult<PX.Objects.AR.ARTran, PX.Objects.GL.Account, PX.Objects.AR.ARRegister, PX.Objects.GL.Branch>.op_Implicit(pxResult);
      PXResult<PX.Objects.AR.ARTran, PX.Objects.GL.Account, PX.Objects.AR.ARRegister, PX.Objects.GL.Branch>.op_Implicit(pxResult);
      PMBudgetAccum targetBudget = this.GetTargetBudget(project, account.AccountGroupID, line);
      if (targetBudget != null)
      {
        PMBudgetAccum pmBudgetAccum5 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(targetBudget);
        Decimal? nullable3 = ARDocType.SignAmount(line.TranType);
        PMBudgetAccum pmBudgetAccum6 = pmBudgetAccum5;
        Decimal? nullable4 = pmBudgetAccum6.CuryInvoicedAmount;
        Decimal? nullable5 = nullable3;
        Decimal inProjectCurrency = this.MultiCurrencyService.GetValueInProjectCurrency((PXGraph) this, project, arRegister.CuryID, arRegister.DocDate, line.CuryExtPrice);
        Decimal? nullable6 = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * inProjectCurrency) : new Decimal?();
        Decimal? nullable7;
        if (!(nullable4.HasValue & nullable6.HasValue))
        {
          nullable5 = new Decimal?();
          nullable7 = nullable5;
        }
        else
          nullable7 = new Decimal?(nullable4.GetValueOrDefault() + nullable6.GetValueOrDefault());
        pmBudgetAccum6.CuryInvoicedAmount = nullable7;
        string uom3 = line.UOM;
        string uom4 = pmBudgetAccum5.UOM;
        nullable6 = line.Qty;
        Decimal valueOrDefault = nullable6.GetValueOrDefault();
        Decimal num5;
        ref Decimal local = ref num5;
        INUnitAttribute.TryConvertGlobalUnits((PXGraph) this, uom3, uom4, valueOrDefault, INPrecision.QUANTITY, out local);
        PMBudgetAccum pmBudgetAccum7 = pmBudgetAccum5;
        nullable6 = pmBudgetAccum7.InvoicedQty;
        nullable5 = nullable3;
        Decimal num6 = num5;
        nullable4 = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * num6) : new Decimal?();
        Decimal? nullable8;
        if (!(nullable6.HasValue & nullable4.HasValue))
        {
          nullable5 = new Decimal?();
          nullable8 = nullable5;
        }
        else
          nullable8 = new Decimal?(nullable6.GetValueOrDefault() + nullable4.GetValueOrDefault());
        pmBudgetAccum7.InvoicedQty = nullable8;
        pmBudgetAccum5.ProgressBillingBase = (string) null;
      }
    }
  }

  public virtual void RecalculateChangeOrders(PMProject project, ProjectBalance projectBalance)
  {
    foreach (PXResult<PMChangeOrderBudget, PMChangeOrder> pxResult in ((PXSelectBase<PMChangeOrderBudget>) new PXSelectJoin<PMChangeOrderBudget, InnerJoin<PMChangeOrder, On<PMChangeOrder.refNbr, Equal<PMChangeOrderBudget.refNbr>>>, Where<PMChangeOrderBudget.projectID, Equal<Required<PMChangeOrderBudget.projectID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMChangeOrderBudget change = PXResult<PMChangeOrderBudget, PMChangeOrder>.op_Implicit(pxResult);
      PMChangeOrder pmChangeOrder = PXResult<PMChangeOrderBudget, PMChangeOrder>.op_Implicit(pxResult);
      this.UpdateChangeBuckets(change, projectBalance, project, pmChangeOrder.Date);
    }
  }

  public virtual void RecalculateChangeRequests(PMProject project, ProjectBalance projectBalance)
  {
    foreach (PXResult<PMChangeRequestLine, PMChangeRequest> pxResult in ((PXSelectBase<PMChangeRequestLine>) new PXSelectJoin<PMChangeRequestLine, InnerJoin<PMChangeRequest, On<PMChangeRequest.refNbr, Equal<PMChangeRequestLine.refNbr>>>, Where<PMChangeRequestLine.projectID, Equal<Required<PMChangeRequestLine.projectID>>, And<PMChangeRequest.released, Equal<False>, And<PMChangeRequest.approved, Equal<True>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMChangeRequestLine changeRequestLine = PXResult<PMChangeRequestLine, PMChangeRequest>.op_Implicit(pxResult);
      PMChangeRequest pmChangeRequest = PXResult<PMChangeRequestLine, PMChangeRequest>.op_Implicit(pxResult);
      PMChangeOrderBudget change1 = new PMChangeOrderBudget();
      change1.ProjectID = changeRequestLine.ProjectID;
      change1.ProjectTaskID = changeRequestLine.CostTaskID;
      change1.AccountGroupID = changeRequestLine.CostAccountGroupID;
      change1.InventoryID = changeRequestLine.InventoryID;
      change1.CostCodeID = changeRequestLine.CostCodeID;
      change1.Qty = changeRequestLine.Qty;
      change1.Amount = changeRequestLine.ExtCost;
      change1.UOM = changeRequestLine.UOM;
      int? nullable = change1.TaskID;
      if (nullable.HasValue)
      {
        nullable = change1.AccountGroupID;
        if (nullable.HasValue)
          this.UpdateChangeBuckets(change1, projectBalance, project, pmChangeRequest.Date);
      }
      PMChangeOrderBudget change2 = new PMChangeOrderBudget();
      change2.ProjectID = changeRequestLine.ProjectID;
      change2.ProjectTaskID = changeRequestLine.RevenueTaskID;
      change2.AccountGroupID = changeRequestLine.RevenueAccountGroupID;
      change2.InventoryID = changeRequestLine.InventoryID;
      change2.CostCodeID = changeRequestLine.RevenueCodeID;
      change2.Qty = changeRequestLine.Qty;
      change2.Amount = changeRequestLine.LineAmount;
      change2.UOM = changeRequestLine.UOM;
      nullable = change2.TaskID;
      if (nullable.HasValue)
      {
        nullable = change2.AccountGroupID;
        if (nullable.HasValue)
          this.UpdateChangeBuckets(change2, projectBalance, project, pmChangeRequest.Date);
      }
    }
    foreach (PXResult<PMChangeRequestMarkup, PMChangeRequest> pxResult in ((PXSelectBase<PMChangeRequestMarkup>) new PXSelectJoin<PMChangeRequestMarkup, InnerJoin<PMChangeRequest, On<PMChangeRequest.refNbr, Equal<PMChangeRequestMarkup.refNbr>>>, Where<PMChangeRequest.projectID, Equal<Required<PMChangeRequest.projectID>>, And<PMChangeRequest.released, Equal<False>, And<PMChangeRequest.approved, Equal<True>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMChangeRequestMarkup changeRequestMarkup = PXResult<PMChangeRequestMarkup, PMChangeRequest>.op_Implicit(pxResult);
      PMChangeRequest pmChangeRequest = PXResult<PMChangeRequestMarkup, PMChangeRequest>.op_Implicit(pxResult);
      PMChangeOrderBudget change = new PMChangeOrderBudget();
      change.ProjectID = pmChangeRequest.ProjectID;
      change.ProjectTaskID = changeRequestMarkup.TaskID;
      change.AccountGroupID = changeRequestMarkup.AccountGroupID;
      change.InventoryID = changeRequestMarkup.InventoryID;
      change.CostCodeID = changeRequestMarkup.CostCodeID;
      change.Amount = changeRequestMarkup.MarkupAmount;
      int? nullable = change.TaskID;
      if (nullable.HasValue)
      {
        nullable = change.AccountGroupID;
        if (nullable.HasValue)
          this.UpdateChangeBuckets(change, projectBalance, project, pmChangeRequest.Date);
      }
    }
  }

  protected virtual void UpdateChangeBuckets(
    PMChangeOrderBudget change,
    ProjectBalance projectBalance,
    PMProject project,
    DateTime? changeDate)
  {
    PMBudgetAccum pmBudgetAccum1 = (PMBudgetAccum) null;
    bool isExisting;
    PX.Objects.PM.Lite.PMBudget budget1 = this.budgetService.SelectProjectBalance((IProjectFilter) change, this.AccountGroups[change.AccountGroupID.Value], project, out isExisting);
    bool? released;
    if (isExisting)
    {
      PXSelect<PMBudgetAccum> budget2 = this.Budget;
      PMBudgetAccum pmBudgetAccum2 = new PMBudgetAccum();
      pmBudgetAccum2.ProjectID = budget1.ProjectID;
      pmBudgetAccum2.ProjectTaskID = budget1.ProjectTaskID;
      pmBudgetAccum2.AccountGroupID = budget1.AccountGroupID;
      pmBudgetAccum2.InventoryID = budget1.InventoryID;
      pmBudgetAccum2.CostCodeID = budget1.CostCodeID;
      pmBudgetAccum2.UOM = budget1.UOM;
      pmBudgetAccum2.CuryInfoID = project.CuryInfoID;
      pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) budget2).Insert(pmBudgetAccum2);
      if (change.Released.GetValueOrDefault())
      {
        PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
        Decimal? changeOrderAmount = pmBudgetAccum3.CuryChangeOrderAmount;
        Decimal valueOrDefault1 = change.Amount.GetValueOrDefault();
        pmBudgetAccum3.CuryChangeOrderAmount = changeOrderAmount.HasValue ? new Decimal?(changeOrderAmount.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
        PMBudgetAccum pmBudgetAccum4 = pmBudgetAccum1;
        Decimal? curyRevisedAmount = pmBudgetAccum4.CuryRevisedAmount;
        Decimal valueOrDefault2 = change.Amount.GetValueOrDefault();
        pmBudgetAccum4.CuryRevisedAmount = curyRevisedAmount.HasValue ? new Decimal?(curyRevisedAmount.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
      }
      else
      {
        PMBudgetAccum pmBudgetAccum5 = pmBudgetAccum1;
        Decimal? changeOrderAmount = pmBudgetAccum5.CuryDraftChangeOrderAmount;
        Decimal valueOrDefault = change.Amount.GetValueOrDefault();
        pmBudgetAccum5.CuryDraftChangeOrderAmount = changeOrderAmount.HasValue ? new Decimal?(changeOrderAmount.GetValueOrDefault() + valueOrDefault) : new Decimal?();
      }
      if (projectBalance.CalculateRollupQty<PMChangeOrderBudget>(change, (IQuantify) budget1) != 0M)
      {
        released = change.Released;
        if (released.GetValueOrDefault())
        {
          PMBudgetAccum pmBudgetAccum6 = pmBudgetAccum1;
          Decimal? changeOrderQty = pmBudgetAccum6.ChangeOrderQty;
          Decimal valueOrDefault3 = change.Qty.GetValueOrDefault();
          pmBudgetAccum6.ChangeOrderQty = changeOrderQty.HasValue ? new Decimal?(changeOrderQty.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
          PMBudgetAccum pmBudgetAccum7 = pmBudgetAccum1;
          Decimal? revisedQty = pmBudgetAccum7.RevisedQty;
          Decimal valueOrDefault4 = change.Qty.GetValueOrDefault();
          pmBudgetAccum7.RevisedQty = revisedQty.HasValue ? new Decimal?(revisedQty.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
        }
        else
        {
          PMBudgetAccum pmBudgetAccum8 = pmBudgetAccum1;
          Decimal? draftChangeOrderQty = pmBudgetAccum8.DraftChangeOrderQty;
          Decimal valueOrDefault = change.Qty.GetValueOrDefault();
          pmBudgetAccum8.DraftChangeOrderQty = draftChangeOrderQty.HasValue ? new Decimal?(draftChangeOrderQty.GetValueOrDefault() + valueOrDefault) : new Decimal?();
        }
      }
    }
    else
    {
      Dictionary<int, PMAccountGroup> accountGroups = this.AccountGroups;
      int? nullable = change.AccountGroupID;
      int key = nullable.Value;
      PMAccountGroup pmAccountGroup;
      ref PMAccountGroup local = ref pmAccountGroup;
      if (accountGroups.TryGetValue(key, out local))
      {
        PMBudgetAccum pmBudgetAccum9 = new PMBudgetAccum();
        pmBudgetAccum9.ProjectID = budget1.ProjectID;
        pmBudgetAccum9.ProjectTaskID = budget1.ProjectTaskID;
        pmBudgetAccum9.AccountGroupID = budget1.AccountGroupID;
        pmBudgetAccum9.InventoryID = budget1.InventoryID;
        pmBudgetAccum9.CostCodeID = budget1.CostCodeID;
        pmBudgetAccum9.Type = budget1.Type;
        pmBudgetAccum9.Description = budget1.Description;
        pmBudgetAccum9.IsProduction = budget1.IsProduction;
        pmBudgetAccum9.CuryInfoID = project.CuryInfoID;
        PMBudgetAccum pmBudgetAccum10 = pmBudgetAccum9;
        if (pmBudgetAccum10.Type == "E" && this.CostRevTasks != null)
        {
          HashSet<int> costRevTasks = this.CostRevTasks;
          nullable = pmBudgetAccum10.ProjectTaskID;
          int num = nullable.Value;
          if (costRevTasks.Contains(num))
            pmBudgetAccum10.RevenueTaskID = pmBudgetAccum10.ProjectTaskID;
        }
        pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(pmBudgetAccum10);
        if (change.Released.GetValueOrDefault())
        {
          PMBudgetAccum pmBudgetAccum11 = pmBudgetAccum1;
          Decimal? changeOrderAmount = pmBudgetAccum11.CuryChangeOrderAmount;
          Decimal valueOrDefault5 = change.Amount.GetValueOrDefault();
          pmBudgetAccum11.CuryChangeOrderAmount = changeOrderAmount.HasValue ? new Decimal?(changeOrderAmount.GetValueOrDefault() + valueOrDefault5) : new Decimal?();
          PMBudgetAccum pmBudgetAccum12 = pmBudgetAccum1;
          Decimal? curyRevisedAmount = pmBudgetAccum12.CuryRevisedAmount;
          Decimal valueOrDefault6 = change.Amount.GetValueOrDefault();
          pmBudgetAccum12.CuryRevisedAmount = curyRevisedAmount.HasValue ? new Decimal?(curyRevisedAmount.GetValueOrDefault() + valueOrDefault6) : new Decimal?();
        }
        else
        {
          PMBudgetAccum pmBudgetAccum13 = pmBudgetAccum1;
          Decimal? changeOrderAmount = pmBudgetAccum13.CuryDraftChangeOrderAmount;
          Decimal valueOrDefault = change.Amount.GetValueOrDefault();
          pmBudgetAccum13.CuryDraftChangeOrderAmount = changeOrderAmount.HasValue ? new Decimal?(changeOrderAmount.GetValueOrDefault() + valueOrDefault) : new Decimal?();
        }
      }
    }
    if (pmBudgetAccum1 == null)
      return;
    FinPeriod finPeriodByDate = this.FinPeriodRepository.GetFinPeriodByDate(changeDate, new int?(0));
    if (finPeriodByDate != null)
    {
      PMForecastHistoryAccum forecastHistoryAccum1 = new PMForecastHistoryAccum();
      forecastHistoryAccum1.ProjectID = pmBudgetAccum1.ProjectID;
      forecastHistoryAccum1.ProjectTaskID = pmBudgetAccum1.ProjectTaskID;
      forecastHistoryAccum1.AccountGroupID = pmBudgetAccum1.AccountGroupID;
      forecastHistoryAccum1.InventoryID = pmBudgetAccum1.InventoryID;
      forecastHistoryAccum1.CostCodeID = pmBudgetAccum1.CostCodeID;
      forecastHistoryAccum1.PeriodID = finPeriodByDate.FinPeriodID;
      PMForecastHistoryAccum forecastHistoryAccum2 = ((PXSelectBase<PMForecastHistoryAccum>) this.ForecastHistory).Insert(forecastHistoryAccum1);
      released = change.Released;
      if (released.GetValueOrDefault())
      {
        PMForecastHistoryAccum forecastHistoryAccum3 = forecastHistoryAccum2;
        Decimal? changeOrderAmount = forecastHistoryAccum3.CuryChangeOrderAmount;
        Decimal valueOrDefault7 = change.Amount.GetValueOrDefault();
        forecastHistoryAccum3.CuryChangeOrderAmount = changeOrderAmount.HasValue ? new Decimal?(changeOrderAmount.GetValueOrDefault() + valueOrDefault7) : new Decimal?();
        PMForecastHistoryAccum forecastHistoryAccum4 = forecastHistoryAccum2;
        Decimal? changeOrderQty = forecastHistoryAccum4.ChangeOrderQty;
        Decimal valueOrDefault8 = change.Qty.GetValueOrDefault();
        forecastHistoryAccum4.ChangeOrderQty = changeOrderQty.HasValue ? new Decimal?(changeOrderQty.GetValueOrDefault() + valueOrDefault8) : new Decimal?();
      }
      else
      {
        PMForecastHistoryAccum forecastHistoryAccum5 = forecastHistoryAccum2;
        Decimal? changeOrderAmount = forecastHistoryAccum5.CuryDraftChangeOrderAmount;
        Decimal valueOrDefault9 = change.Amount.GetValueOrDefault();
        forecastHistoryAccum5.CuryDraftChangeOrderAmount = changeOrderAmount.HasValue ? new Decimal?(changeOrderAmount.GetValueOrDefault() + valueOrDefault9) : new Decimal?();
        PMForecastHistoryAccum forecastHistoryAccum6 = forecastHistoryAccum2;
        Decimal? draftChangeOrderQty = forecastHistoryAccum6.DraftChangeOrderQty;
        Decimal valueOrDefault10 = change.Qty.GetValueOrDefault();
        forecastHistoryAccum6.DraftChangeOrderQty = draftChangeOrderQty.HasValue ? new Decimal?(draftChangeOrderQty.GetValueOrDefault() + valueOrDefault10) : new Decimal?();
      }
    }
    else
      PXTrace.WriteError("Failed to find FinPeriodID for date {0}", new object[1]
      {
        (object) changeDate
      });
  }

  public virtual void InitAccountGroup()
  {
    if (this.AccountGroups != null)
      return;
    this.AccountGroups = new Dictionary<int, PMAccountGroup>();
    foreach (PXResult<PMAccountGroup> pxResult in PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      PMAccountGroup pmAccountGroup = PXResult<PMAccountGroup>.op_Implicit(pxResult);
      this.AccountGroups.Add(pmAccountGroup.GroupID.Value, pmAccountGroup);
    }
  }

  public virtual void InitCostRevTasks(PMProject project)
  {
    if (this.CostRevTasks != null)
      return;
    this.CostRevTasks = new HashSet<int>();
    PXSelectReadonly<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.type, Equal<ProjectTaskType.costRevenue>>>> pxSelectReadonly = new PXSelectReadonly<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.type, Equal<ProjectTaskType.costRevenue>>>>((PXGraph) this);
    using (new PXFieldScope(((PXSelectBase) pxSelectReadonly).View, new Type[3]
    {
      typeof (PMTask.taskCD),
      typeof (PMTask.taskID),
      typeof (PMTask.type)
    }))
    {
      foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) pxSelectReadonly).Select(new object[1]
      {
        (object) project.ContractID
      }))
        this.CostRevTasks.Add(PXResult<PMTask>.op_Implicit(pxResult).TaskID.Value);
    }
  }

  public virtual void InitCostCodeOnModifiedEntities()
  {
    if (!CostCodeAttribute.UseCostCode())
      return;
    int? defaultCostCode = CostCodeAttribute.DefaultCostCode;
    foreach (PX.Objects.PO.POLine poLine in ((PXSelectBase) this.polines).Cache.Updated)
    {
      if (!poLine.CostCodeID.HasValue)
        ((PXSelectBase) this.polines).Cache.SetValue<PX.Objects.PO.POLine.costCodeID>((object) poLine, (object) defaultCostCode);
    }
  }

  public virtual string GetAccountGroupType(int? accountGroup)
  {
    PMAccountGroup accountGroup1 = this.AccountGroups[accountGroup.Value];
    return accountGroup1.Type == "O" && accountGroup1.IsExpense.GetValueOrDefault() ? "E" : accountGroup1.Type;
  }

  public virtual ProjectBalance CreateProjectBalance()
  {
    return new ProjectBalance((PXGraph) this, (IBudgetService) this.budgetService, ServiceLocator.Current.GetInstance<IProjectSettingsManager>());
  }

  public List<PXDataFieldParam> BuildBudgetClearCommand(
    PMValidationFilter options,
    PMProject project,
    int? accountGroupID)
  {
    List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<PMBudget.projectID>((PXDbType) 8, new int?(4), (object) project.ContractID, (PXComp) 0));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<PMBudget.accountGroupID>((PXDbType) 8, new int?(4), (object) accountGroupID, (PXComp) 0));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.type>((PXDbType) 3, new int?(1), (object) this.GetAccountGroupType(accountGroupID)));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyActualAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.actualAmount>((PXDbType) 5, (object) 0M));
    if (options.RecalculateInclusiveTaxes.GetValueOrDefault())
    {
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyInclTaxAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.inclTaxAmount>((PXDbType) 5, (object) 0M));
    }
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.actualQty>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyDraftRetainedAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.draftRetainedAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyRetainedAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.retainedAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyTotalRetainedAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.totalRetainedAmount>((PXDbType) 5, (object) 0M));
    if (options.RebuildCommitments.GetValueOrDefault())
    {
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyCommittedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyCommittedOpenAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedOpenAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedOpenQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedReceivedQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyCommittedInvoicedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedInvoicedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedInvoicedQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedOrigQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyCommittedOrigAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedOrigAmount>((PXDbType) 5, (object) 0M));
    }
    if (options.RecalculateDraftInvoicesAmount.GetValueOrDefault())
    {
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyInvoicedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.invoicedQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.invoicedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyPrepaymentInvoiced>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.prepaymentInvoiced>((PXDbType) 5, (object) 0M));
    }
    return pxDataFieldParamList;
  }

  public List<PXDataFieldParam> BuildBudgetClearCommandWithChangeOrders(
    PMValidationFilter options,
    ProjectBalanceValidationProcess.PMBudgetLiteEx status)
  {
    List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<PMBudget.projectID>((PXDbType) 8, new int?(4), (object) status.ProjectID, (PXComp) 0));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<PMBudget.accountGroupID>((PXDbType) 8, new int?(4), (object) status.AccountGroupID, (PXComp) 0));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<PMBudget.projectTaskID>((PXDbType) 8, new int?(4), (object) status.ProjectTaskID, (PXComp) 0));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<PMBudget.inventoryID>((PXDbType) 8, new int?(4), (object) status.InventoryID, (PXComp) 0));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<PMBudget.costCodeID>((PXDbType) 8, new int?(4), (object) status.CostCodeID, (PXComp) 0));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.type>((PXDbType) 3, new int?(1), (object) this.GetAccountGroupType(status.AccountGroupID)));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyActualAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.actualAmount>((PXDbType) 5, (object) 0M));
    if (options.RecalculateInclusiveTaxes.GetValueOrDefault())
    {
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyInclTaxAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.inclTaxAmount>((PXDbType) 5, (object) 0M));
    }
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.actualQty>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.revisedQty>((PXDbType) 5, (object) status.Qty));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyRevisedAmount>((PXDbType) 5, (object) status.CuryAmount));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.revisedAmount>((PXDbType) 5, (object) status.Amount));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.draftChangeOrderQty>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyDraftChangeOrderAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.draftChangeOrderAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.changeOrderQty>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyChangeOrderAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.changeOrderAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyDraftRetainedAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.draftRetainedAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyRetainedAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.retainedAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyTotalRetainedAmount>((PXDbType) 5, (object) 0M));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.totalRetainedAmount>((PXDbType) 5, (object) 0M));
    if (options.RebuildCommitments.GetValueOrDefault())
    {
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyCommittedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyCommittedOpenAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedOpenAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedOpenQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedReceivedQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyCommittedInvoicedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedInvoicedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedInvoicedQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedOrigQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyCommittedOrigAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.committedOrigAmount>((PXDbType) 5, (object) 0M));
    }
    if (options.RecalculateDraftInvoicesAmount.GetValueOrDefault())
    {
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyInvoicedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.invoicedQty>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.invoicedAmount>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.curyPrepaymentInvoiced>((PXDbType) 5, (object) 0M));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<PMBudget.prepaymentInvoiced>((PXDbType) 5, (object) 0M));
    }
    return pxDataFieldParamList;
  }

  public List<PXDataFieldParam> BuildForecastClearCommand(
    PMValidationFilter options,
    int? projectID)
  {
    List<PXDataFieldParam> list = new List<PXDataFieldParam>();
    this.AddRestrictorsForcast(options, projectID, list);
    this.AddFieldAssignsForecast(options, list);
    return list;
  }

  public virtual void AddRestrictorsForcast(
    PMValidationFilter options,
    int? projectID,
    List<PXDataFieldParam> list)
  {
    list.Add((PXDataFieldParam) new PXDataFieldRestrict(typeof (PMForecastHistory.projectID).Name, (PXDbType) 8, new int?(4), (object) projectID, (PXComp) 0));
  }

  public virtual void AddFieldAssignsForecast(
    PMValidationFilter options,
    List<PXDataFieldParam> list)
  {
    list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMForecastHistory.curyActualAmount).Name, (PXDbType) 5, (object) 0M));
    list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMForecastHistory.actualAmount).Name, (PXDbType) 5, (object) 0M));
    list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMForecastHistory.curyArAmount).Name, (PXDbType) 5, (object) 0M));
    if (options.RecalculateInclusiveTaxes.GetValueOrDefault())
    {
      list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMForecastHistory.curyInclTaxAmount).Name, (PXDbType) 5, (object) 0M));
      list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMForecastHistory.inclTaxAmount).Name, (PXDbType) 5, (object) 0M));
    }
    list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMForecastHistory.actualQty).Name, (PXDbType) 5, (object) 0M));
    if (!options.RecalculateChangeOrders.GetValueOrDefault())
      return;
    list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMForecastHistory.draftChangeOrderQty).Name, (PXDbType) 5, (object) 0M));
    list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMForecastHistory.curyDraftChangeOrderAmount).Name, (PXDbType) 5, (object) 0M));
    list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMForecastHistory.changeOrderQty).Name, (PXDbType) 5, (object) 0M));
    list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMForecastHistory.curyChangeOrderAmount).Name, (PXDbType) 5, (object) 0M));
  }

  public List<PXDataFieldParam> BuildTranHistoryClearCommand(
    PMValidationFilter options,
    int? projectID)
  {
    List<PXDataFieldParam> list = new List<PXDataFieldParam>();
    this.AddRestrictorsForTranHistory(options, projectID, list);
    this.AddFieldAssignsForTranHistory(options, list);
    return list;
  }

  public virtual void AddRestrictorsForTranHistory(
    PMValidationFilter options,
    int? projectID,
    List<PXDataFieldParam> list)
  {
    list.Add((PXDataFieldParam) new PXDataFieldRestrict(typeof (PMHistoryByDate.projectID).Name, (PXDbType) 8, new int?(4), (object) projectID, (PXComp) 0));
  }

  public virtual void AddFieldAssignsForTranHistory(
    PMValidationFilter options,
    List<PXDataFieldParam> list)
  {
    list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMHistoryByDate.curyActualAmount).Name, (PXDbType) 5, (object) 0M));
    list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMHistoryByDate.actualAmount).Name, (PXDbType) 5, (object) 0M));
    list.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (PMHistoryByDate.actualQty).Name, (PXDbType) 5, (object) 0M));
  }

  protected virtual void ProcessProgressWorksheets(
    ProjectBalance pb,
    PMProject project,
    PMValidationFilter options)
  {
    HashSet<BudgetKeyTuple> budgetKeyTupleSet = new HashSet<BudgetKeyTuple>();
    Dictionary<int, PMAccountGroup> dictionary1 = new Dictionary<int, PMAccountGroup>();
    foreach (PXResult<PMProgressWorksheet, PMProgressWorksheetLine> pxResult in ((PXSelectBase<PMProgressWorksheet>) new PXSelectJoin<PMProgressWorksheet, InnerJoin<PMProgressWorksheetLine, On<PMProgressWorksheet.refNbr, Equal<PMProgressWorksheetLine.refNbr>>>, Where<PMProgressWorksheet.projectID, Equal<Required<PMProgressWorksheet.projectID>>, And<PMProgressWorksheet.status, Equal<ProgressWorksheetStatus.closed>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMProgressWorksheetLine progressWorksheetLine = PXResult<PMProgressWorksheet, PMProgressWorksheetLine>.op_Implicit(pxResult);
      BudgetKeyTuple budgetKeyTuple = BudgetKeyTuple.Create((IProjectFilter) progressWorksheetLine);
      if (!budgetKeyTupleSet.Contains(budgetKeyTuple))
      {
        Dictionary<int, PMAccountGroup> accountGroups = this.AccountGroups;
        int? nullable = progressWorksheetLine.AccountGroupID;
        int key1 = nullable.Value;
        PMAccountGroup ag;
        ref PMAccountGroup local1 = ref ag;
        if (!accountGroups.TryGetValue(key1, out local1))
        {
          Dictionary<int, PMAccountGroup> dictionary2 = dictionary1;
          nullable = progressWorksheetLine.AccountGroupID;
          int key2 = nullable.Value;
          ref PMAccountGroup local2 = ref ag;
          if (!dictionary2.TryGetValue(key2, out local2))
          {
            object[] objArray = new object[1];
            nullable = progressWorksheetLine.AccountGroupID;
            objArray[0] = (object) nullable.Value;
            ag = PXResultset<PMAccountGroup>.op_Implicit(PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.groupID, Equal<Required<PMAccountGroup.groupID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, objArray));
            if (ag != null)
            {
              Dictionary<int, PMAccountGroup> dictionary3 = dictionary1;
              nullable = progressWorksheetLine.AccountGroupID;
              int key3 = nullable.Value;
              PMAccountGroup pmAccountGroup = ag;
              dictionary3.Add(key3, pmAccountGroup);
            }
          }
        }
        if (ag != null)
        {
          bool isExisting;
          PX.Objects.PM.Lite.PMBudget pmBudget = this.budgetService.SelectProjectBalance((IProjectFilter) progressWorksheetLine, ag, project, out isExisting);
          if (!isExisting)
          {
            PMBudgetAccum pmBudgetAccum1 = new PMBudgetAccum();
            pmBudgetAccum1.ProjectID = pmBudget.ProjectID;
            pmBudgetAccum1.ProjectTaskID = pmBudget.ProjectTaskID;
            pmBudgetAccum1.AccountGroupID = pmBudget.AccountGroupID;
            pmBudgetAccum1.InventoryID = pmBudget.InventoryID;
            pmBudgetAccum1.CostCodeID = pmBudget.CostCodeID;
            pmBudgetAccum1.Type = pmBudget.Type;
            pmBudgetAccum1.Description = pmBudget.Description;
            pmBudgetAccum1.UOM = pmBudget.UOM;
            pmBudgetAccum1.CuryInfoID = project.CuryInfoID;
            PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
            if (pmBudgetAccum2.Type == "E" && this.CostRevTasks != null)
            {
              HashSet<int> costRevTasks = this.CostRevTasks;
              nullable = pmBudgetAccum2.ProjectTaskID;
              int num = nullable.Value;
              if (costRevTasks.Contains(num))
                pmBudgetAccum2.RevenueTaskID = pmBudgetAccum2.ProjectTaskID;
            }
            ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(pmBudgetAccum2).ProductivityTracking = "D";
          }
        }
        budgetKeyTupleSet.Add(budgetKeyTuple);
      }
    }
  }

  protected virtual void ProcessRetainage(PMProject project, PMValidationFilter options)
  {
    PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.CuryInfoID
    }));
    foreach (PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) new PXSelectJoinGroupBy<PX.Objects.AR.ARTran, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARRegister.docType, Equal<PX.Objects.AR.ARTran.tranType>, And<PX.Objects.AR.ARRegister.refNbr, Equal<PX.Objects.AR.ARTran.refNbr>>>, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.AR.ARTran.accountID, Equal<PX.Objects.GL.Account.accountID>, And<PX.Objects.GL.Account.accountGroupID, IsNotNull>>>>, Where<PX.Objects.AR.ARTran.projectID, Equal<Required<PX.Objects.AR.ARTran.projectID>>, And<PX.Objects.AR.ARTran.released, Equal<True>, And<PX.Objects.AR.ARRegister.paymentsByLinesAllowed, Equal<False>>>>, Aggregate<GroupBy<PX.Objects.AR.ARRegister.branchID, GroupBy<PX.Objects.AR.ARTran.accountID, GroupBy<PX.Objects.AR.ARTran.projectID, GroupBy<PX.Objects.AR.ARTran.taskID, GroupBy<PX.Objects.AR.ARTran.inventoryID, GroupBy<PX.Objects.AR.ARTran.costCodeID, GroupBy<PX.Objects.AR.ARTran.tranType, Sum<PX.Objects.AR.ARTran.retainageAmt, Sum<PX.Objects.AR.ARTran.curyRetainageAmt, Sum<PX.Objects.AR.ARTran.retainageBal, Sum<PX.Objects.AR.ARTran.curyRetainageBal>>>>>>>>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PX.Objects.AR.ARRegister arRegister = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.AR.ARTran line = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PMBudgetAccum targetBudget = this.GetTargetBudget(project, account.AccountGroupID, line);
      if (targetBudget != null)
      {
        PMBudgetAccum pmBudgetAccum = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(targetBudget);
        Decimal? curyRetainedAmount = pmBudgetAccum.CuryRetainedAmount;
        IProjectMultiCurrency multiCurrencyService = this.MultiCurrencyService;
        PMProject project1 = project;
        string curyId = arRegister.CuryID;
        DateTime? docDate = arRegister.DocDate;
        Decimal? nullable1 = line.CuryRetainageAmt;
        Decimal valueOrDefault = nullable1.GetValueOrDefault();
        nullable1 = ARDocType.SignAmount(line.TranType);
        Decimal num = nullable1 ?? 1M;
        Decimal? nullable2 = new Decimal?(valueOrDefault * num);
        Decimal inProjectCurrency = multiCurrencyService.GetValueInProjectCurrency((PXGraph) this, project1, curyId, docDate, nullable2);
        Decimal? nullable3;
        if (!curyRetainedAmount.HasValue)
        {
          nullable1 = new Decimal?();
          nullable3 = nullable1;
        }
        else
          nullable3 = new Decimal?(curyRetainedAmount.GetValueOrDefault() + inProjectCurrency);
        pmBudgetAccum.CuryRetainedAmount = nullable3;
      }
    }
    foreach (PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) new PXSelectJoinGroupBy<PX.Objects.AR.ARTran, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARRegister.docType, Equal<PX.Objects.AR.ARTran.tranType>, And<PX.Objects.AR.ARRegister.refNbr, Equal<PX.Objects.AR.ARTran.refNbr>>>, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.AR.ARTran.accountID, Equal<PX.Objects.GL.Account.accountID>, And<PX.Objects.GL.Account.accountGroupID, IsNotNull>>>>, Where<PX.Objects.AR.ARTran.projectID, Equal<Required<PX.Objects.AR.ARTran.projectID>>, And<PX.Objects.AR.ARTran.released, Equal<True>, And<PX.Objects.AR.ARRegister.paymentsByLinesAllowed, Equal<True>, And<PX.Objects.AR.ARRegister.isRetainageReversing, Equal<False>>>>>, Aggregate<GroupBy<PX.Objects.AR.ARRegister.branchID, GroupBy<PX.Objects.AR.ARTran.accountID, GroupBy<PX.Objects.AR.ARTran.projectID, GroupBy<PX.Objects.AR.ARTran.taskID, GroupBy<PX.Objects.AR.ARTran.inventoryID, GroupBy<PX.Objects.AR.ARTran.costCodeID, GroupBy<PX.Objects.AR.ARTran.tranType, Sum<PX.Objects.AR.ARTran.retainageAmt, Sum<PX.Objects.AR.ARTran.curyRetainageAmt, Sum<PX.Objects.AR.ARTran.retainageBal, Sum<PX.Objects.AR.ARTran.curyRetainageBal>>>>>>>>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PX.Objects.AR.ARRegister arRegister = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.AR.ARTran line = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PMBudgetAccum targetBudget = this.GetTargetBudget(project, account.AccountGroupID, line);
      if (targetBudget != null)
      {
        PMBudgetAccum pmBudgetAccum = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(targetBudget);
        Decimal? curyRetainedAmount = pmBudgetAccum.CuryRetainedAmount;
        IProjectMultiCurrency multiCurrencyService = this.MultiCurrencyService;
        PMProject project2 = project;
        string curyId = arRegister.CuryID;
        DateTime? docDate = arRegister.DocDate;
        Decimal? nullable4 = line.CuryRetainageBal;
        Decimal valueOrDefault = nullable4.GetValueOrDefault();
        nullable4 = ARDocType.SignAmount(line.TranType);
        Decimal num = nullable4 ?? 1M;
        Decimal? nullable5 = new Decimal?(valueOrDefault * num);
        Decimal inProjectCurrency = multiCurrencyService.GetValueInProjectCurrency((PXGraph) this, project2, curyId, docDate, nullable5);
        Decimal? nullable6;
        if (!curyRetainedAmount.HasValue)
        {
          nullable4 = new Decimal?();
          nullable6 = nullable4;
        }
        else
          nullable6 = new Decimal?(curyRetainedAmount.GetValueOrDefault() + inProjectCurrency);
        pmBudgetAccum.CuryRetainedAmount = nullable6;
      }
    }
    foreach (PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) new PXSelectJoinGroupBy<PX.Objects.AR.ARTran, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARRegister.docType, Equal<PX.Objects.AR.ARTran.tranType>, And<PX.Objects.AR.ARRegister.refNbr, Equal<PX.Objects.AR.ARTran.refNbr>>>, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.AR.ARTran.accountID, Equal<PX.Objects.GL.Account.accountID>, And<PX.Objects.GL.Account.accountGroupID, IsNotNull>>>>, Where<PX.Objects.AR.ARTran.projectID, Equal<Required<PX.Objects.AR.ARTran.projectID>>, And<PX.Objects.AR.ARTran.released, Equal<False>>>, Aggregate<GroupBy<PX.Objects.AR.ARRegister.branchID, GroupBy<PX.Objects.AR.ARTran.accountID, GroupBy<PX.Objects.AR.ARTran.projectID, GroupBy<PX.Objects.AR.ARTran.taskID, GroupBy<PX.Objects.AR.ARTran.inventoryID, GroupBy<PX.Objects.AR.ARTran.costCodeID, GroupBy<PX.Objects.AR.ARTran.tranType, Sum<PX.Objects.AR.ARTran.retainageAmt, Sum<PX.Objects.AR.ARTran.curyRetainageAmt, Sum<PX.Objects.AR.ARTran.retainageBal, Sum<PX.Objects.AR.ARTran.curyRetainageBal>>>>>>>>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PX.Objects.AR.ARRegister arRegister = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.AR.ARTran line = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PMBudgetAccum targetBudget = this.GetTargetBudget(project, account.AccountGroupID, line);
      if (targetBudget != null)
      {
        PMBudgetAccum pmBudgetAccum = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(targetBudget);
        Decimal? draftRetainedAmount = pmBudgetAccum.CuryDraftRetainedAmount;
        IProjectMultiCurrency multiCurrencyService = this.MultiCurrencyService;
        PMProject project3 = project;
        string curyId = arRegister.CuryID;
        DateTime? docDate = arRegister.DocDate;
        Decimal? nullable7 = line.CuryRetainageAmt;
        Decimal valueOrDefault = nullable7.GetValueOrDefault();
        nullable7 = ARDocType.SignAmount(line.TranType);
        Decimal num = nullable7 ?? 1M;
        Decimal? nullable8 = new Decimal?(valueOrDefault * num);
        Decimal inProjectCurrency = multiCurrencyService.GetValueInProjectCurrency((PXGraph) this, project3, curyId, docDate, nullable8);
        Decimal? nullable9;
        if (!draftRetainedAmount.HasValue)
        {
          nullable7 = new Decimal?();
          nullable9 = nullable7;
        }
        else
          nullable9 = new Decimal?(draftRetainedAmount.GetValueOrDefault() + inProjectCurrency);
        pmBudgetAccum.CuryDraftRetainedAmount = nullable9;
      }
    }
    foreach (PXResult<PMProformaLine, PMProforma> pxResult in ((PXSelectBase<PMProformaLine>) new PXSelectJoinGroupBy<PMProformaLine, InnerJoin<PMProforma, On<PMProforma.refNbr, Equal<PMProformaLine.refNbr>, And<PMProforma.revisionID, Equal<PMProformaLine.revisionID>>>>, Where<PMProformaLine.projectID, Equal<Required<PMProformaLine.projectID>>, And<PMProformaLine.type, Equal<PMProformaLineType.progressive>, And<PMProformaLine.corrected, Equal<False>>>>, Aggregate<GroupBy<PMProforma.branchID, GroupBy<PMProformaLine.accountGroupID, GroupBy<PMProformaLine.projectID, GroupBy<PMProformaLine.taskID, GroupBy<PMProformaLine.inventoryID, GroupBy<PMProformaLine.costCodeID, GroupBy<PMProformaLine.released, Sum<PMProformaLine.retainage, Sum<PMProformaLine.curyRetainage>>>>>>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMProformaLine line = PXResult<PMProformaLine, PMProforma>.op_Implicit(pxResult);
      PMProforma pmProforma = PXResult<PMProformaLine, PMProforma>.op_Implicit(pxResult);
      PMBudgetAccum targetBudget = this.GetTargetBudget(project, line.AccountGroupID, line);
      if (targetBudget != null)
      {
        PMBudgetAccum pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(targetBudget);
        IProjectMultiCurrency multiCurrencyService = this.MultiCurrencyService;
        PMProject project4 = project;
        string curyId = pmProforma.CuryID;
        DateTime? invoiceDate = pmProforma.InvoiceDate;
        Decimal? nullable10 = line.CuryRetainage;
        Decimal? nullable11 = new Decimal?(nullable10.GetValueOrDefault());
        Decimal inProjectCurrency = multiCurrencyService.GetValueInProjectCurrency((PXGraph) this, project4, curyId, invoiceDate, nullable11);
        if (!line.Released.GetValueOrDefault())
        {
          PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
          nullable10 = pmBudgetAccum2.CuryDraftRetainedAmount;
          Decimal num = inProjectCurrency;
          pmBudgetAccum2.CuryDraftRetainedAmount = nullable10.HasValue ? new Decimal?(nullable10.GetValueOrDefault() + num) : new Decimal?();
        }
        PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
        nullable10 = pmBudgetAccum3.CuryTotalRetainedAmount;
        Decimal num1 = inProjectCurrency;
        pmBudgetAccum3.CuryTotalRetainedAmount = nullable10.HasValue ? new Decimal?(nullable10.GetValueOrDefault() + num1) : new Decimal?();
      }
    }
    foreach (PXResult<PMProformaLine, PMProforma, PX.Objects.GL.Account> pxResult in ((PXSelectBase<PMProformaLine>) new PXSelectJoinGroupBy<PMProformaLine, InnerJoin<PMProforma, On<PMProforma.refNbr, Equal<PMProformaLine.refNbr>, And<PMProforma.revisionID, Equal<PMProformaLine.revisionID>>>, InnerJoin<PX.Objects.GL.Account, On<PMProformaLine.accountID, Equal<PX.Objects.GL.Account.accountID>, And<PX.Objects.GL.Account.accountGroupID, IsNotNull>>>>, Where<PMProformaLine.projectID, Equal<Required<PMProformaLine.projectID>>, And<PMProformaLine.type, Equal<PMProformaLineType.transaction>, And<PMProformaLine.corrected, Equal<False>>>>, Aggregate<GroupBy<PMProforma.branchID, GroupBy<PMProformaLine.accountID, GroupBy<PMProformaLine.projectID, GroupBy<PMProformaLine.taskID, GroupBy<PMProformaLine.inventoryID, GroupBy<PMProformaLine.costCodeID, GroupBy<PMProformaLine.released, Sum<PMProformaLine.retainage, Sum<PMProformaLine.curyRetainage>>>>>>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMProformaLine line = PXResult<PMProformaLine, PMProforma, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PMProforma pmProforma = PXResult<PMProformaLine, PMProforma, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<PMProformaLine, PMProforma, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PMBudgetAccum targetBudget = this.GetTargetBudget(project, account.AccountGroupID, line);
      if (targetBudget != null)
      {
        PMBudgetAccum pmBudgetAccum4 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(targetBudget);
        IProjectMultiCurrency multiCurrencyService = this.MultiCurrencyService;
        PMProject project5 = project;
        string curyId = pmProforma.CuryID;
        DateTime? invoiceDate = pmProforma.InvoiceDate;
        Decimal? nullable12 = line.CuryRetainage;
        Decimal? nullable13 = new Decimal?(nullable12.GetValueOrDefault());
        Decimal inProjectCurrency = multiCurrencyService.GetValueInProjectCurrency((PXGraph) this, project5, curyId, invoiceDate, nullable13);
        if (!line.Released.GetValueOrDefault())
        {
          PMBudgetAccum pmBudgetAccum5 = pmBudgetAccum4;
          nullable12 = pmBudgetAccum5.CuryDraftRetainedAmount;
          Decimal num = inProjectCurrency;
          pmBudgetAccum5.CuryDraftRetainedAmount = nullable12.HasValue ? new Decimal?(nullable12.GetValueOrDefault() + num) : new Decimal?();
        }
        PMBudgetAccum pmBudgetAccum6 = pmBudgetAccum4;
        nullable12 = pmBudgetAccum6.CuryTotalRetainedAmount;
        Decimal num2 = inProjectCurrency;
        pmBudgetAccum6.CuryTotalRetainedAmount = nullable12.HasValue ? new Decimal?(nullable12.GetValueOrDefault() + num2) : new Decimal?();
      }
    }
  }

  public virtual (Decimal? CuryAmount, Decimal? Amount) GetInclusiveTaxAmount(
    PXGraph graph,
    PX.Objects.AR.ARTran tran)
  {
    return ProjectRevenueTaxAmountProvider.GetInclusiveTaxAmount(graph, tran);
  }

  public virtual (Decimal? CuryAmount, Decimal? Amount) GetRetainedInclusiveTaxAmount(
    PXGraph graph,
    PX.Objects.AR.ARTran tran)
  {
    return ProjectRevenueTaxAmountProvider.GetRetainedInclusiveTaxAmount(graph, tran);
  }

  protected virtual void ProcessInclusiveTaxes(PMProject project, PMValidationFilter options)
  {
    if ((options != null ? (!options.RecalculateInclusiveTaxes.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    foreach (PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account> pxResult in PXSelectBase<PX.Objects.AR.ARTran, PXViewOf<PX.Objects.AR.ARTran>.BasedOn<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.ARRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARRegister.docType, Equal<PX.Objects.AR.ARTran.tranType>>>>>.And<BqlOperand<PX.Objects.AR.ARRegister.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARTran.refNbr>>>>, FbqlJoins.Inner<PX.Objects.GL.Account>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.accountID, Equal<PX.Objects.GL.Account.accountID>>>>>.And<BqlOperand<PX.Objects.GL.Account.accountGroupID, IBqlInt>.IsNotNull>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.AR.ARTran.released, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.ContractID
    }))
    {
      PX.Objects.AR.ARRegister arRegister = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<PX.Objects.AR.ARTran, PX.Objects.AR.ARRegister, PX.Objects.GL.Account>.op_Implicit(pxResult);
      Decimal? nullable1 = ARDocType.SignAmount(arTran.TranType);
      PMBudgetAccum targetBudget = this.GetTargetBudget(project, account.AccountGroupID, arTran);
      if (targetBudget != null)
      {
        PMBudgetAccum pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(targetBudget);
        (Decimal? CuryAmount, Decimal? Amount) inclusiveTaxAmount1 = this.GetInclusiveTaxAmount((PXGraph) this, arTran);
        (Decimal? CuryAmount, Decimal? Amount) inclusiveTaxAmount2 = this.GetRetainedInclusiveTaxAmount((PXGraph) this, arTran);
        IProjectMultiCurrency multiCurrencyService = this.MultiCurrencyService;
        PMProject project1 = project;
        string curyId = arRegister.CuryID;
        DateTime? docDate = arRegister.DocDate;
        Decimal? curyAmount = inclusiveTaxAmount1.CuryAmount;
        Decimal? nullable2 = inclusiveTaxAmount2.CuryAmount;
        Decimal? nullable3;
        Decimal? nullable4;
        if (!(curyAmount.HasValue & nullable2.HasValue))
        {
          nullable3 = new Decimal?();
          nullable4 = nullable3;
        }
        else
          nullable4 = new Decimal?(curyAmount.GetValueOrDefault() + nullable2.GetValueOrDefault());
        Decimal inProjectCurrency = multiCurrencyService.GetValueInProjectCurrency((PXGraph) this, project1, curyId, docDate, nullable4);
        Decimal? nullable5 = nullable1;
        Decimal? nullable6;
        if (!nullable5.HasValue)
        {
          nullable2 = new Decimal?();
          nullable6 = nullable2;
        }
        else
          nullable6 = new Decimal?(inProjectCurrency * nullable5.GetValueOrDefault());
        Decimal? nullable7 = nullable6;
        Decimal? amount = inclusiveTaxAmount1.Amount;
        nullable3 = inclusiveTaxAmount2.Amount;
        nullable5 = amount.HasValue & nullable3.HasValue ? new Decimal?(amount.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        nullable2 = nullable1;
        Decimal? nullable8;
        if (!(nullable5.HasValue & nullable2.HasValue))
        {
          nullable3 = new Decimal?();
          nullable8 = nullable3;
        }
        else
          nullable8 = new Decimal?(nullable5.GetValueOrDefault() * nullable2.GetValueOrDefault());
        Decimal? nullable9 = nullable8;
        PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
        nullable2 = pmBudgetAccum2.CuryInclTaxAmount;
        Decimal valueOrDefault1 = nullable7.GetValueOrDefault();
        Decimal? nullable10;
        if (!nullable2.HasValue)
        {
          nullable5 = new Decimal?();
          nullable10 = nullable5;
        }
        else
          nullable10 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault1);
        pmBudgetAccum2.CuryInclTaxAmount = nullable10;
        PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
        nullable2 = pmBudgetAccum3.InclTaxAmount;
        Decimal valueOrDefault2 = nullable9.GetValueOrDefault();
        Decimal? nullable11;
        if (!nullable2.HasValue)
        {
          nullable5 = new Decimal?();
          nullable11 = nullable5;
        }
        else
          nullable11 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault2);
        pmBudgetAccum3.InclTaxAmount = nullable11;
        PMForecastHistoryAccum forecastHistoryAccum1 = new PMForecastHistoryAccum();
        forecastHistoryAccum1.ProjectID = pmBudgetAccum1.ProjectID;
        forecastHistoryAccum1.ProjectTaskID = pmBudgetAccum1.ProjectTaskID;
        forecastHistoryAccum1.AccountGroupID = pmBudgetAccum1.AccountGroupID;
        forecastHistoryAccum1.InventoryID = pmBudgetAccum1.InventoryID;
        forecastHistoryAccum1.CostCodeID = pmBudgetAccum1.CostCodeID;
        forecastHistoryAccum1.PeriodID = arTran.TranPeriodID;
        PMForecastHistoryAccum forecastHistoryAccum2 = ((PXSelectBase<PMForecastHistoryAccum>) this.ForecastHistory).Insert(forecastHistoryAccum1);
        PMForecastHistoryAccum forecastHistoryAccum3 = forecastHistoryAccum2;
        nullable2 = forecastHistoryAccum3.CuryInclTaxAmount;
        Decimal valueOrDefault3 = nullable7.GetValueOrDefault();
        Decimal? nullable12;
        if (!nullable2.HasValue)
        {
          nullable5 = new Decimal?();
          nullable12 = nullable5;
        }
        else
          nullable12 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault3);
        forecastHistoryAccum3.CuryInclTaxAmount = nullable12;
        PMForecastHistoryAccum forecastHistoryAccum4 = forecastHistoryAccum2;
        nullable2 = forecastHistoryAccum4.InclTaxAmount;
        Decimal valueOrDefault4 = nullable9.GetValueOrDefault();
        Decimal? nullable13;
        if (!nullable2.HasValue)
        {
          nullable5 = new Decimal?();
          nullable13 = nullable5;
        }
        else
          nullable13 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault4);
        forecastHistoryAccum4.InclTaxAmount = nullable13;
      }
    }
  }

  protected virtual void HandleProjectStatusCode(PMProject project, PMValidationFilter options)
  {
    if (StatusCodeHelper.CheckStatus(project.StatusCode, StatusCodes.InclusiveTaxesInRevenueBudgetIntroduced) && options.RecalculateInclusiveTaxes.GetValueOrDefault())
      ProjectBalanceValidationProcess.ResetProjectStatusCode(project, StatusCodes.InclusiveTaxesInRevenueBudgetIntroduced);
    if (StatusCodeHelper.CheckStatus(project.StatusCode, StatusCodes.DateSensitiveActualsIntroduced))
      ProjectBalanceValidationProcess.ResetProjectStatusCode(project, StatusCodes.DateSensitiveActualsIntroduced);
    if (!StatusCodeHelper.CheckStatus(project.StatusCode, StatusCodes.ProjectBudgetHistoryIntroduced) || !options.RecalculateProjectBudgetHistory.GetValueOrDefault())
      return;
    ProjectBalanceValidationProcess.ResetProjectStatusCode(project, StatusCodes.ProjectBudgetHistoryIntroduced);
  }

  public static void ResetProjectStatusCode(PMProject project, StatusCodes statusCodeToReset)
  {
    PXDatabase.Update<Contract>(new PXDataFieldParam[2]
    {
      (PXDataFieldParam) new PXDataFieldRestrict<PMProject.contractID>((PXDbType) 8, new int?(4), (object) project.ContractID, (PXComp) 0),
      (PXDataFieldParam) new PXDataFieldAssign<PMProject.statusCode>((PXDbType) 8, (object) StatusCodeHelper.ResetStatus(project.StatusCode, statusCodeToReset))
    });
  }

  protected virtual void RestoreBillingRecords(PMProject project)
  {
    PXSelect<PMBillingRecord, Where<PMBillingRecord.projectID, Equal<Required<PMBillingRecord.projectID>>>, OrderBy<Asc<PMBillingRecord.recordID>>> pxSelect = new PXSelect<PMBillingRecord, Where<PMBillingRecord.projectID, Equal<Required<PMBillingRecord.projectID>>>, OrderBy<Asc<PMBillingRecord.recordID>>>((PXGraph) this);
    HashSet<string> stringSet = new HashSet<string>();
    object[] objArray = new object[1]
    {
      (object) project.ContractID
    };
    foreach (PXResult<PMBillingRecord> pxResult in ((PXSelectBase<PMBillingRecord>) pxSelect).Select(objArray))
    {
      PMBillingRecord pmBillingRecord = PXResult<PMBillingRecord>.op_Implicit(pxResult);
      stringSet.Add(pmBillingRecord.ProformaRefNbr);
    }
    foreach (PXResult<PMProforma> pxResult in ((PXSelectBase<PMProforma>) new PXSelect<PMProforma, Where<PMProforma.projectID, Equal<Required<PMProforma.projectID>>, And<PMProforma.corrected, Equal<False>>>, OrderBy<Asc<PMProforma.refNbr>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMProforma pmProforma = PXResult<PMProforma>.op_Implicit(pxResult);
      if (!stringSet.Contains(pmProforma.RefNbr))
      {
        project.BillingLineCntr = new int?(project.BillingLineCntr.GetValueOrDefault() + 1);
        ((PXSelectBase<PMProject>) this.Project).Update(project);
        PMBillingRecord instance = (PMBillingRecord) ((PXSelectBase) this.BillingRecords).Cache.CreateInstance();
        instance.ProjectID = project.ContractID;
        instance.RecordID = project.BillingLineCntr;
        instance.ProformaRefNbr = pmProforma.RefNbr;
        instance.ARDocType = pmProforma.ARInvoiceDocType;
        instance.ARRefNbr = pmProforma.ARInvoiceRefNbr;
        instance.BillingTag = "P";
        instance.Date = pmProforma.InvoiceDate;
        ((PXSelectBase<PMBillingRecord>) this.BillingRecords).Insert(instance);
      }
    }
  }

  private string GetScreenName(string screenID)
  {
    string screenName = string.Empty;
    PXSiteMapNode mapNodeByScreenId;
    if (!string.IsNullOrEmpty(screenID) && (mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenID)) != null && !string.IsNullOrEmpty(mapNodeByScreenId.Title))
      screenName = $"{mapNodeByScreenId.Title} {screenID}";
    return screenName;
  }

  private PMBudgetAccum GetTargetBudget(PMProject project, int? accountGroupID, PX.Objects.AR.ARTran line)
  {
    PMAccountGroup ag;
    if (!this.AccountGroups.TryGetValue(accountGroupID.Value, out ag))
      return (PMBudgetAccum) null;
    PX.Objects.PM.Lite.PMBudget pmBudget = this.budgetService.SelectProjectBalance(ag, project, line.TaskID, line.InventoryID, line.CostCodeID, out bool _);
    PMBudgetAccum targetBudget = new PMBudgetAccum();
    targetBudget.Type = pmBudget.Type;
    targetBudget.ProjectID = pmBudget.ProjectID;
    targetBudget.ProjectTaskID = pmBudget.TaskID;
    targetBudget.AccountGroupID = pmBudget.AccountGroupID;
    targetBudget.InventoryID = pmBudget.InventoryID;
    targetBudget.CostCodeID = pmBudget.CostCodeID;
    targetBudget.UOM = pmBudget.UOM;
    targetBudget.Description = pmBudget.Description;
    targetBudget.CuryInfoID = project.CuryInfoID;
    return targetBudget;
  }

  private PMBudgetAccum GetTargetBudget(
    PMProject project,
    int? accountGroupID,
    PMProformaLine line)
  {
    PMAccountGroup ag;
    if (!this.AccountGroups.TryGetValue(accountGroupID.Value, out ag))
      return (PMBudgetAccum) null;
    PX.Objects.PM.Lite.PMBudget pmBudget = this.budgetService.SelectProjectBalance(ag, project, line.TaskID, line.InventoryID, line.CostCodeID, out bool _);
    PMBudgetAccum targetBudget = new PMBudgetAccum();
    targetBudget.Type = pmBudget.Type;
    targetBudget.ProjectID = pmBudget.ProjectID;
    targetBudget.ProjectTaskID = pmBudget.TaskID;
    targetBudget.AccountGroupID = pmBudget.AccountGroupID;
    targetBudget.InventoryID = pmBudget.InventoryID;
    targetBudget.CostCodeID = pmBudget.CostCodeID;
    targetBudget.UOM = pmBudget.UOM;
    targetBudget.Description = pmBudget.Description;
    targetBudget.CuryInfoID = project.CuryInfoID;
    return targetBudget;
  }

  public class MultiCurrency : ProjectBudgetMultiCurrency<ProjectBalanceValidationProcess>
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

  [PXHidden]
  [PXBreakInheritance]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class PMBudgetLiteEx : PX.Objects.PM.Lite.PMBudget
  {
    [PXDBDecimal]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Original Budgeted Amount")]
    public virtual Decimal? CuryAmount { get; set; }

    [PXDBDecimal]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Original Budgeted Amount in Base Currency")]
    public virtual Decimal? Amount { get; set; }

    public abstract class curyAmount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ProjectBalanceValidationProcess.PMBudgetLiteEx.curyAmount>
    {
    }

    public abstract class amount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ProjectBalanceValidationProcess.PMBudgetLiteEx.amount>
    {
    }
  }

  [Obsolete("The class will be removed in the future versions. Instead, use PMTranAllocReversal.")]
  [PXHidden]
  [PXBreakInheritance]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class PMTranReversal : PMTran
  {
    public new abstract class tranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ProjectBalanceValidationProcess.PMTranReversal.tranID>
    {
    }

    public new abstract class origTranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ProjectBalanceValidationProcess.PMTranReversal.origTranID>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectBalanceValidationProcess.PMTranReversal.refNbr>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (Select2<PMTran, InnerJoin<PMRegister, On<PMRegister.module, Equal<PMTran.tranType>, And<PMRegister.refNbr, Equal<PMTran.refNbr>>>>>))]
  public class PMTranAllocReversal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBLong(IsKey = true, BqlField = typeof (PMTran.tranID))]
    public virtual long? TranID { get; set; }

    [PXDBString(2, IsFixed = true, BqlField = typeof (PMTran.tranType))]
    public virtual string TranType { get; set; }

    [PXDBLong(BqlField = typeof (PMTran.origTranID))]
    public virtual long? OrigTranID { get; set; }

    [PXDBString(2, IsFixed = true, BqlField = typeof (PMRegister.origDocType))]
    public virtual string OrigDocType { get; set; }

    public abstract class tranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ProjectBalanceValidationProcess.PMTranAllocReversal.tranID>
    {
    }

    public abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectBalanceValidationProcess.PMTranAllocReversal.tranType>
    {
    }

    public abstract class origTranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ProjectBalanceValidationProcess.PMTranAllocReversal.origTranID>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectBalanceValidationProcess.PMTranAllocReversal.origDocType>
    {
    }
  }

  public class BudgetServiceMassUpdate : BudgetService
  {
    private int? projectID;
    private Dictionary<BudgetKeyTuple, ProjectBalanceValidationProcess.PMBudgetLiteEx> Budget;
    private HashSet<int> accountGroups;

    public BudgetServiceMassUpdate(PXGraph graph, PMProject project)
      : base(graph)
    {
      this.projectID = project.ContractID;
    }

    protected override List<PX.Objects.PM.Lite.PMBudget> SelectExistingBalances(
      int projectID,
      int taskID,
      int accountGroupID,
      int?[] costCodes,
      int?[] items)
    {
      if (this.Budget == null)
        this.PreSelectProjectBudget();
      List<PX.Objects.PM.Lite.PMBudget> pmBudgetList = new List<PX.Objects.PM.Lite.PMBudget>();
      foreach (int? costCode in costCodes)
      {
        int costCodeID = costCode.Value;
        foreach (int? nullable in items)
        {
          int inventoryID = nullable.Value;
          ProjectBalanceValidationProcess.PMBudgetLiteEx pmBudgetLiteEx;
          if (this.Budget.TryGetValue(new BudgetKeyTuple(projectID, taskID, accountGroupID, inventoryID, costCodeID), out pmBudgetLiteEx))
            pmBudgetList.Add((PX.Objects.PM.Lite.PMBudget) pmBudgetLiteEx);
        }
      }
      return pmBudgetList;
    }

    private void PreSelectProjectBudget()
    {
      this.Budget = new Dictionary<BudgetKeyTuple, ProjectBalanceValidationProcess.PMBudgetLiteEx>();
      this.accountGroups = new HashSet<int>();
      foreach (PXResult<ProjectBalanceValidationProcess.PMBudgetLiteEx> pxResult in ((PXSelectBase<ProjectBalanceValidationProcess.PMBudgetLiteEx>) new PXSelect<ProjectBalanceValidationProcess.PMBudgetLiteEx, Where<PX.Objects.PM.Lite.PMBudget.projectID, Equal<Required<PX.Objects.PM.Lite.PMBudget.projectID>>>>(this.graph)).Select(new object[1]
      {
        (object) this.projectID
      }))
      {
        ProjectBalanceValidationProcess.PMBudgetLiteEx budget = PXResult<ProjectBalanceValidationProcess.PMBudgetLiteEx>.op_Implicit(pxResult);
        this.Budget.Add(BudgetKeyTuple.Create((IProjectFilter) budget), budget);
        this.accountGroups.Add(budget.AccountGroupID.Value);
      }
    }

    public ICollection<int> GetUsedAccountGroups()
    {
      if (this.Budget == null)
        this.PreSelectProjectBudget();
      return (ICollection<int>) this.accountGroups;
    }

    public Dictionary<BudgetKeyTuple, ProjectBalanceValidationProcess.PMBudgetLiteEx>.ValueCollection BudgetRecords
    {
      get
      {
        if (this.Budget == null)
          this.PreSelectProjectBudget();
        return this.Budget.Values;
      }
    }
  }
}
