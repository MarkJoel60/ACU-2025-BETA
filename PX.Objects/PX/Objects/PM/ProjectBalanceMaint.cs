// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectBalanceMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CT;
using PX.Objects.Extensions.MultiCurrency;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class ProjectBalanceMaint : PXGraph<
#nullable disable
ProjectBalanceMaint>, PXImportAttribute.IPXPrepareItems
{
  [PXImport(typeof (PMBudget))]
  [PXViewName("Budget")]
  [PXFilterable(new Type[] {})]
  public FbqlSelect<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMTask>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTask.projectID, 
  #nullable disable
  Equal<PMBudget.projectID>>>>>.And<BqlOperand<
  #nullable enable
  PMTask.taskID, IBqlInt>.IsEqual<
  #nullable disable
  PMBudget.projectTaskID>>>>, FbqlJoins.Inner<PMProject>.On<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  PMBudget.projectID>>>, FbqlJoins.Inner<PMAccountGroup>.On<BqlOperand<
  #nullable enable
  PMAccountGroup.groupID, IBqlInt>.IsEqual<
  #nullable disable
  PMBudget.accountGroupID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.nonProject, 
  #nullable disable
  Equal<False>>>>, And<BqlOperand<
  #nullable enable
  PMProject.baseType, IBqlString>.IsEqual<
  #nullable disable
  CTPRType.project>>>, And<MatchUser>>>.And<MatchUserFor<PMProject>>>.Order<By<BqlField<
  #nullable enable
  PMProject.contractCD, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PMTask.taskCD, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PMAccountGroup.groupCD, IBqlString>.Asc>>, 
  #nullable disable
  PMBudget>.View Items;
  public FbqlSelect<SelectFromBase<PMProjectBudgetHistory, TypeArrayOf<IFbqlJoin>.Empty>, PMProjectBudgetHistory>.View ProjectBudgetHistory;
  public FbqlSelect<SelectFromBase<PMProjectBudgetHistoryAccum, TypeArrayOf<IFbqlJoin>.Empty>, PMProjectBudgetHistoryAccum>.View ProjectBudgetHistoryAccum;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMCostCode> dummyCostCode;
  public PXSetup<PMProject>.Where<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMBudget.projectID, IBqlInt>.AsOptional>> project;
  public 
  #nullable disable
  PXSavePerRow<PMBudget> Save;
  public PXCancel<PMBudget> Cancel;
  public PXSetup<PMSetup> Setup;
  public PXSelect<PMTask> Task;
  public PXAction<PMBudget> viewProject;
  public PXAction<PMBudget> viewTask;
  public PXAction<PMBudget> viewTransactions;
  public PXAction<PMBudget> viewCommitments;

  [InjectDependency]
  public IUnitRateService RateService { get; set; }

  [PXDefault]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMBudget.projectID>>>>))]
  [Project(typeof (Where<PMProject.nonProject, Equal<False>, And<PMProject.baseType, Equal<CTPRType.project>>>), IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMBudget.projectID> e)
  {
  }

  [PMTaskCompleted]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMBudget.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ProjectTask(typeof (PMBudget.projectID), IsKey = true, AlwaysEnabled = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMBudget.projectTaskID> e)
  {
  }

  [PXDefault]
  [AccountGroup(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMBudget.accountGroupID> e)
  {
  }

  [PXMergeAttributes]
  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  public ProjectBalanceMaint() => this.SetDefaultColumnVisibility();

  [PXUIField]
  [PXButton]
  public IEnumerable ViewProject(PXAdapter adapter)
  {
    if (((PXSelectBase<PMBudget>) this.Items).Current != null)
      ProjectAccountingService.NavigateToProjectScreen(((PXSelectBase<PMBudget>) this.Items).Current.ProjectID);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public IEnumerable ViewTask(PXAdapter adapter)
  {
    if (((PXSelectBase<PMBudget>) this.Items).Current != null)
    {
      ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
      ((PXSelectBase<PMTask>) instance.Task).Current = PMTask.PK.FindDirty((PXGraph) this, ((PXSelectBase<PMBudget>) this.Items).Current.ProjectID, ((PXSelectBase<PMBudget>) this.Items).Current.ProjectTaskID);
      ProjectAccountingService.NavigateToScreen((PXGraph) instance, "Project Task Entry - View Task");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public IEnumerable ViewTransactions(PXAdapter adapter)
  {
    if (((PXSelectBase<PMBudget>) this.Items).Current != null)
    {
      TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMBudget>) this.Items).Current.ProjectID;
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectTaskID = ((PXSelectBase<PMBudget>) this.Items).Current.ProjectTaskID;
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.AccountGroupID = ((PXSelectBase<PMBudget>) this.Items).Current.AccountGroupID;
      int? inventoryId = ((PXSelectBase<PMBudget>) this.Items).Current.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue))
        ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<PMBudget>) this.Items).Current.InventoryID;
      ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public IEnumerable ViewCommitments(PXAdapter adapter)
  {
    if (((PXSelectBase<PMBudget>) this.Items).Current != null)
    {
      CommitmentInquiry instance = PXGraph.CreateInstance<CommitmentInquiry>();
      ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMBudget>) this.Items).Current.ProjectID;
      ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectTaskID = ((PXSelectBase<PMBudget>) this.Items).Current.ProjectTaskID;
      ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.AccountGroupID = ((PXSelectBase<PMBudget>) this.Items).Current.AccountGroupID;
      int? inventoryId = ((PXSelectBase<PMBudget>) this.Items).Current.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue))
        ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<PMBudget>) this.Items).Current.InventoryID;
      ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    }
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project Currency")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.curyID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMBudget> e)
  {
    if (e.Row == null)
      return;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, e.Row.ProjectID);
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMBudget>>) e).Cache;
    bool? nullable;
    int num1;
    if (pmProject == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = pmProject.BudgetFinalized;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<PMCostBudget.curyUnitRate>(cache1, (object) null, num1 != 0);
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMBudget>>) e).Cache;
    int num2;
    if (pmProject == null)
    {
      num2 = 1;
    }
    else
    {
      nullable = pmProject.BudgetFinalized;
      num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<PMCostBudget.qty>(cache2, (object) null, num2 != 0);
    PXCache cache3 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMBudget>>) e).Cache;
    int num3;
    if (pmProject == null)
    {
      num3 = 1;
    }
    else
    {
      nullable = pmProject.BudgetFinalized;
      num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<PMCostBudget.curyAmount>(cache3, (object) null, num3 != 0);
    PXCache cache4 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMBudget>>) e).Cache;
    int num4;
    if (pmProject == null)
    {
      num4 = 1;
    }
    else
    {
      nullable = pmProject.ChangeOrderWorkflow;
      num4 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<PMCostBudget.revisedQty>(cache4, (object) null, num4 != 0);
    PXCache cache5 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMBudget>>) e).Cache;
    int num5;
    if (pmProject == null)
    {
      num5 = 1;
    }
    else
    {
      nullable = pmProject.ChangeOrderWorkflow;
      num5 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<PMCostBudget.curyRevisedAmount>(cache5, (object) null, num5 != 0);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMBudget> e)
  {
    this.UpdateBudgetHistoryLine(e.Row, 1);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMBudget> e)
  {
    this.UpdateBudgetHistoryLine(e.OldRow, -1);
    this.UpdateBudgetHistoryLine(e.Row, 1);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMBudget> e)
  {
    this.UpdateBudgetHistoryLine(e.Row, -1);
  }

  public virtual void UpdateBudgetHistoryLine(PMBudget budget, int sign)
  {
    if (budget == null || !budget.ProjectTaskID.HasValue || !budget.CostCodeID.HasValue || !budget.AccountGroupID.HasValue || !budget.InventoryID.HasValue)
      return;
    Decimal? qty1 = budget.Qty;
    Decimal num1 = 0M;
    if (qty1.GetValueOrDefault() == num1 & qty1.HasValue)
    {
      Decimal? curyAmount = budget.CuryAmount;
      Decimal num2 = 0M;
      if (curyAmount.GetValueOrDefault() == num2 & curyAmount.HasValue)
        return;
    }
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, budget.ProjectID);
    PMProjectBudgetHistoryAccum budgetHistoryAccum1 = new PMProjectBudgetHistoryAccum();
    budgetHistoryAccum1.Date = pmProject.StartDate;
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
    Decimal? nullable1 = budgetHistoryAccum3.RevisedBudgetQty;
    Decimal? qty2 = budget.Qty;
    Decimal num3 = (Decimal) sign;
    Decimal? nullable2 = qty2.HasValue ? new Decimal?(qty2.GetValueOrDefault() * num3) : new Decimal?();
    budgetHistoryAccum3.RevisedBudgetQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    PMProjectBudgetHistoryAccum budgetHistoryAccum4 = budgetHistoryAccum2;
    Decimal? revisedBudgetAmt = budgetHistoryAccum4.CuryRevisedBudgetAmt;
    Decimal? curyAmount1 = budget.CuryAmount;
    Decimal num4 = (Decimal) sign;
    nullable1 = curyAmount1.HasValue ? new Decimal?(curyAmount1.GetValueOrDefault() * num4) : new Decimal?();
    budgetHistoryAccum4.CuryRevisedBudgetAmt = revisedBudgetAmt.HasValue & nullable1.HasValue ? new Decimal?(revisedBudgetAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<PMBudget, PMBudget.revenueTaskID> e)
  {
    PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, e.Row.AccountGroupID);
    if (pmAccountGroup == null || !(pmAccountGroup.Type == "I"))
      return;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<PMBudget, PMBudget.revenueTaskID>>) e).Cancel = true;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<PMBudget, PMBudget.revenueTaskID>>) e).NewValue = (object) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMBudget, PMCostBudget.curyAmount> e)
  {
    if (e.Row == null)
      return;
    PMBudget row = e.Row;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, row.ProjectID);
    if ((pmProject != null ? (pmProject.BudgetFinalized.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    row.CuryAmount = ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMBudget, PMCostBudget.curyAmount>, PMBudget, object>) e).OldValue as Decimal?;
  }

  protected virtual void PMBudget_UOM_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is PMBudget row) || string.IsNullOrEmpty(row.UOM))
      return;
    PXSelect<PMTran, Where<PMTran.projectID, Equal<Current<PMBudget.projectID>>, And<PMTran.taskID, Equal<Current<PMBudget.projectTaskID>>, And<PMTran.costCodeID, Equal<Current<PMBudget.costCodeID>>, And<PMTran.inventoryID, Equal<Current<PMBudget.inventoryID>>, And2<Where<PMTran.accountGroupID, Equal<Current<PMBudget.accountGroupID>>, Or<PMTran.offsetAccountGroupID, Equal<Current<PMBudget.accountGroupID>>>>, And<PMTran.released, Equal<True>, And<PMTran.uOM, NotEqual<Required<PMTran.uOM>>>>>>>>>> pxSelect = new PXSelect<PMTran, Where<PMTran.projectID, Equal<Current<PMBudget.projectID>>, And<PMTran.taskID, Equal<Current<PMBudget.projectTaskID>>, And<PMTran.costCodeID, Equal<Current<PMBudget.costCodeID>>, And<PMTran.inventoryID, Equal<Current<PMBudget.inventoryID>>, And2<Where<PMTran.accountGroupID, Equal<Current<PMBudget.accountGroupID>>, Or<PMTran.offsetAccountGroupID, Equal<Current<PMBudget.accountGroupID>>>>, And<PMTran.released, Equal<True>, And<PMTran.uOM, NotEqual<Required<PMTran.uOM>>>>>>>>>>((PXGraph) this);
    string newValue = (string) e.NewValue;
    if (string.IsNullOrEmpty(newValue))
      return;
    if (PXResultset<PMTran>.op_Implicit(((PXSelectBase<PMTran>) pxSelect).SelectWindowed(0, 1, new object[1]
    {
      (object) newValue
    })) != null)
      throw new PXSetPropertyException("Cannot set the UOM on the budget line. There already exists one or more transactions with a different UOM.")
      {
        ErrorValue = (object) newValue
      };
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMBudget, PMBudget.accountGroupID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.accountGroupID>>) e).Cache.SetDefaultExt<PMBudget.type>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.accountGroupID>>) e).Cache.SetDefaultExt<PMBudget.curyUnitRate>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMBudget, PMBudget.type> e)
  {
    if (!(e.Row.Type == "I"))
      return;
    e.Row.RevenueTaskID = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMBudget, PMBudget.curyAmount> e)
  {
    if (e.Row == null)
      return;
    e.Row.CuryRevisedAmount = e.Row.CuryAmount;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMBudget, PMBudget.qty> e)
  {
    if (e.Row == null)
      return;
    e.Row.RevisedQty = e.Row.Qty;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.type> e)
  {
    PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, e.Row.AccountGroupID);
    if (pmAccountGroup == null)
      return;
    if (pmAccountGroup.Type == "O")
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.type>, PMBudget, object>) e).NewValue = pmAccountGroup.IsExpense.GetValueOrDefault() ? (object) "E" : (object) pmAccountGroup.Type;
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.type>, PMBudget, object>) e).NewValue = (object) pmAccountGroup.Type;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.costCodeID> e)
  {
    if (e.Row == null)
      return;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, e.Row.ProjectID);
    if (pmProject == null || !(pmProject.BudgetLevel != "C"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.costCodeID>, PMBudget, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.inventoryID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.inventoryID>, PMBudget, object>) e).NewValue = (object) PMInventorySelectorAttribute.EmptyInventoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.description> e)
  {
    if (e.Row == null)
      return;
    if (CostCodeAttribute.UseCostCode())
    {
      if (!e.Row.CostCodeID.HasValue)
        return;
      int? costCodeId = e.Row.CostCodeID;
      int defaultCostCode = CostCodeAttribute.GetDefaultCostCode();
      if (costCodeId.GetValueOrDefault() == defaultCostCode & costCodeId.HasValue || !(PXSelectorAttribute.Select<PMBudget.costCodeID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.description>>) e).Cache, (object) e.Row) is PMCostCode pmCostCode))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.description>, PMBudget, object>) e).NewValue = (object) pmCostCode.Description;
    }
    else
    {
      if (!e.Row.InventoryID.HasValue)
        return;
      int? inventoryId = e.Row.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue || !(PXSelectorAttribute.Select<PMBudget.inventoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.description>>) e).Cache, (object) e.Row) is PX.Objects.IN.InventoryItem inventoryItem))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.description>, PMBudget, object>) e).NewValue = (object) inventoryItem.Descr;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMBudget, PMBudget.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.inventoryID>>) e).Cache.SetDefaultExt<PMBudget.description>((object) e.Row);
    if (!e.Row.AccountGroupID.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.inventoryID>>) e).Cache.SetDefaultExt<PMBudget.accountGroupID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.inventoryID>>) e).Cache.SetDefaultExt<PMBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.inventoryID>>) e).Cache.SetDefaultExt<PMBudget.curyUnitRate>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.inventoryID>>) e).Cache.SetDefaultExt<PMBudget.taxCategoryID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMBudget, PMBudget.uOM> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.uOM>>) e).Cache.SetDefaultExt<PMBudget.curyUnitRate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMBudget, PMBudget.costCodeID> e)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, e.Row.ProjectID);
    if (!CostCodeAttribute.UseCostCode() || !(pmProject?.BudgetLevel == "C"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMBudget, PMBudget.costCodeID>>) e).Cache.SetDefaultExt<PMBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.curyUnitRate> e)
  {
    PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, e.Row.AccountGroupID);
    if (pmAccountGroup == null)
      return;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, e.Row.ProjectID);
    if (pmAccountGroup.IsExpense.GetValueOrDefault())
    {
      Decimal? unitCost = this.RateService.CalculateUnitCost(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.curyUnitRate>>) e).Cache, e.Row.ProjectID, e.Row.ProjectTaskID, e.Row.InventoryID, e.Row.UOM, new int?(), pmProject.StartDate, pmProject.CuryInfoID);
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.curyUnitRate>, PMBudget, object>) e).NewValue = (object) unitCost.GetValueOrDefault();
    }
    else
    {
      Decimal? unitPrice = this.RateService.CalculateUnitPrice(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.curyUnitRate>>) e).Cache, e.Row.ProjectID, e.Row.ProjectTaskID, e.Row.InventoryID, e.Row.UOM, e.Row.Qty, pmProject.StartDate, pmProject.CuryInfoID);
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMBudget, PMBudget.curyUnitRate>, PMBudget, object>) e).NewValue = (object) unitPrice.GetValueOrDefault();
    }
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (!CostCodeAttribute.UseCostCode())
    {
      PMCostCode pmCostCode = PXResultset<PMCostCode>.op_Implicit(PXSelectBase<PMCostCode, PXSelect<PMCostCode, Where<PMCostCode.isDefault, Equal<True>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmCostCode != null)
      {
        keys[(object) "CostCodeID"] = (object) pmCostCode.CostCodeCD;
        values[(object) "CostCodeID"] = (object) pmCostCode.CostCodeCD;
      }
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual void SetDefaultColumnVisibility()
  {
    bool valueOrDefault = ((PXSelectBase<PMSetup>) this.Setup).Current.CostCommitmentTracking.GetValueOrDefault();
    ((PXAction) this.viewCommitments).SetVisible(valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedAmount>(((PXSelectBase) this.Items).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.committedQty>(((PXSelectBase) this.Items).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedInvoicedAmount>(((PXSelectBase) this.Items).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.committedInvoicedQty>(((PXSelectBase) this.Items).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.curyCommittedOpenAmount>(((PXSelectBase) this.Items).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.committedOpenQty>(((PXSelectBase) this.Items).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMBudget.committedReceivedQty>(((PXSelectBase) this.Items).Cache, (object) null, valueOrDefault);
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  public class MultiCurrency : MultiCurrencyGraph<ProjectBalanceMaint, PMBudget>
  {
    protected override MultiCurrencyGraph<ProjectBalanceMaint, PMBudget>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<ProjectBalanceMaint, PMBudget>.CurySourceMapping(typeof (PMProject))
      {
        AllowOverrideCury = typeof (Contract.allowOverrideCury),
        AllowOverrideRate = typeof (Contract.allowOverrideRate),
        CuryRateTypeID = typeof (PMProject.rateTypeID),
        CuryID = typeof (PMProject.curyID)
      };
    }

    protected override bool AllowOverrideCury() => false;

    protected override string Module => "PM";

    protected override MultiCurrencyGraph<ProjectBalanceMaint, PMBudget>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ProjectBalanceMaint, PMBudget>.DocumentMapping(typeof (PMBudget))
      {
        BAccountID = typeof (PMBudget.projectID),
        CuryInfoID = typeof (PMBudget.curyInfoID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[2]
      {
        (PXSelectBase) this.Base.Items,
        (PXSelectBase) this.Base.ProjectBudgetHistory
      };
    }

    protected override void DocumentRowInserting<CuryInfoID, CuryID>(PXCache sender, object row)
    {
      if (!((row is PX.Objects.Extensions.MultiCurrency.Document document ? document.Base : (object) null) is PMBudget pmBudget))
        return;
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Base.project).Select(new object[1]
      {
        (object) pmBudget.ProjectID
      }));
      if (pmProject == null)
        return;
      document.CuryInfoID = pmProject.CuryInfoID;
      pmBudget.CuryInfoID = pmProject.CuryInfoID;
    }
  }
}
