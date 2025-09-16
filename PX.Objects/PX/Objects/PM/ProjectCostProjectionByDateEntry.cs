// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectCostProjectionByDateEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class ProjectCostProjectionByDateEntry : 
  PXGraph<
  #nullable disable
  ProjectCostProjectionByDateEntry, PMCostProjectionByDate>
{
  public PXSetup<PMSetup> Setup;
  [PXViewName("Cost Projection By Date")]
  public FbqlSelect<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProject>.On<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  PMCostProjectionByDate.projectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.contractID, 
  #nullable disable
  IsNull>>>>.Or<MatchUserFor<PMProject>>>, PMCostProjectionByDate>.View Document;
  [PXViewName("Project")]
  public FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PMProject>.View Project;
  [PXViewName("Cost Projection Detail")]
  public FbqlSelect<SelectFromBase<PMCostProjectionByDateLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMCostProjectionByDateLine.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMCostProjectionByDate.refNbr, IBqlString>.FromCurrent>>.Order<
  #nullable disable
  PX.Data.BQL.Fluent.By<BqlField<
  #nullable enable
  PMCostProjectionByDateLine.lineNbr, IBqlInt>.Asc>>, 
  #nullable disable
  PMCostProjectionByDateLine>.View Items;
  [PXCopyPasteHiddenView]
  [PXViewName("Approval")]
  public EPApprovalAutomation<PMCostProjectionByDate, PMCostProjectionByDate.approved, PMCostProjectionByDate.rejected, PMCostProjectionByDate.hold, PMSetupCostProjectionByDateApproval> Approval;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<PMCostProjectionByDateLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMCostProjectionByDateLine.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMCostProjectionByDate.refNbr, IBqlString>.FromCurrent>>.Order<
  #nullable disable
  PX.Data.BQL.Fluent.By<BqlField<
  #nullable enable
  PMCostProjectionByDateLine.lineNbr, IBqlInt>.Asc>>, 
  #nullable disable
  PMCostProjectionByDateLine>.View Report;
  [PXViewName("Commitments")]
  public FbqlSelect<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POOrder>.On<PX.Objects.PO.POLine.FK.Order>>, FbqlJoins.Left<PX.Objects.AP.APTran>.On<PX.Objects.AP.APTran.FK.POLine>>, FbqlJoins.Left<PMTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.origTranType, 
  #nullable disable
  Equal<PX.Objects.AP.APTran.tranType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.origRefNbr, 
  #nullable disable
  Equal<PX.Objects.AP.APTran.refNbr>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.origLineNbr, 
  #nullable disable
  Equal<PX.Objects.AP.APTran.lineNbr>>>>>.And<BqlOperand<
  #nullable enable
  PMTran.date, IBqlDateTime>.IsLessEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>>>.Where<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.PO.POOrder.orderDate, 
  #nullable disable
  LessEqual<BqlField<
  #nullable enable
  PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.PO.POLine.projectID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  PX.Objects.PO.POLine.orderType, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.PO.POLine.orderNbr, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.PO.POLine.lineNbr, IBqlInt>.Asc>>, 
  #nullable disable
  PX.Objects.PO.POLine>.View LineCommitments;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<PMCostBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMCostBudget.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PMCostBudget.type, IBqlString>.IsEqual<
  #nullable disable
  AccountType.expense>>>, PMCostBudget>.View CostBudget;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<ProjectCostProjectionByDateEntry.SelectedCostProjection, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ProjectCostProjectionByDateEntry.SelectedCostProjection.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ProjectCostProjectionByDateEntry.SelectedCostProjection.refNbr, 
  #nullable disable
  NotEqual<BqlField<
  #nullable enable
  PMCostProjectionByDate.refNbr, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ProjectCostProjectionByDateEntry.SelectedCostProjection.groupByAccountGroupID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMCostProjectionByDate.groupByAccountGroupID, IBqlBool>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ProjectCostProjectionByDateEntry.SelectedCostProjection.groupByProjectTaskID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMCostProjectionByDate.groupByProjectTaskID, IBqlBool>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ProjectCostProjectionByDateEntry.SelectedCostProjection.groupByInventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMCostProjectionByDate.groupByInventoryID, IBqlBool>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ProjectCostProjectionByDateEntry.SelectedCostProjection.groupByCostCodeID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMCostProjectionByDate.groupByCostCodeID, IBqlBool>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  ProjectCostProjectionByDateEntry.SelectedCostProjection.includePendingChangeOrders, IBqlBool>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMCostProjectionByDate.includePendingChangeOrders, IBqlBool>.FromCurrent>>>>>>>>, 
  #nullable disable
  ProjectCostProjectionByDateEntry.SelectedCostProjection>.View CostProjectionsToCopyFrom;
  public PXAction<PMCostProjectionByDate> viewCostCommitments;
  public PXAction<PMCostProjectionByDate> viewLineCostCommitments;
  public PXAction<PMCostProjectionByDate> viewCostTransactions;
  public PXAction<PX.Objects.PO.POLine> viewPurchaseOrder;
  public PXAction<PMCostProjectionByDate> runProjectCostAnalysis;
  public PXAction<PMCostProjectionByDate> rebuildProjection;
  public PXAction<PMCostProjectionByDate> removeHold;
  public PXAction<PMCostProjectionByDate> hold;
  public PXAction<PMCostProjectionByDate> release;
  public PXAction<PMCostProjectionByDate> uploadProjection;
  private bool _isRebuilding;

  public ProjectCostProjectionByDateEntry()
  {
    ((PXAction) this.CopyPaste).SetVisible(false);
    this.AddSubcontractType();
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project Currency", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.curyID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project Manager", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostProjectionByDate.ownerID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.lineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.APTran.tranType> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PX.Objects.AP.APRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AP.APTran>.On<PX.Objects.AP.APTran.FK.Document>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APTran.pOOrderType, Equal<BqlField<PX.Objects.PO.POLine.orderType, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APTran.pONbr, Equal<BqlField<PX.Objects.PO.POLine.orderNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.AP.APTran.pOLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POLine.lineNbr, IBqlInt>.FromCurrent>>>>, PX.Objects.AP.APRegister>.SearchFor<PX.Objects.AP.APRegister.refNbr>))]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.APTran.refNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.date> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.projectCuryAmount> e)
  {
  }

  public virtual IEnumerable report()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = false,
      IsResultSorted = true
    };
    if (((PXSelectBase<PMCostProjectionByDate>) this.Document).Current == null)
      return (IEnumerable) pxDelegateResult;
    IEnumerable<PMCostProjectionByDateLine> projectionByDateLines = GraphHelper.RowCast<PMCostProjectionByDateLine>(GraphHelper.QuickSelect(((PXSelectBase) this.Items).View));
    if (this.GroupingIsActive() && projectionByDateLines.Any<PMCostProjectionByDateLine>())
    {
      PMCostProjectionByDate current = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current;
      PMCostProjectionByDateLine projectionByDateLine = new PMCostProjectionByDateLine()
      {
        RefNbr = current.RefNbr,
        LineNbr = new int?(0),
        ProjectID = current.ProjectID,
        CompletedPct = current.CompletedPctTotal,
        CuryAmountToComplete = current.CuryAmountToCompleteTotal,
        AmountToComplete = current.AmountToCompleteTotal,
        CuryProjectedAmount = current.CuryProjectedAmountTotal,
        ProjectedAmount = current.ProjectedAmountTotal,
        CuryBudgetedAmount = current.CuryBudgetedAmountTotal,
        BudgetedAmount = current.BudgetedAmountTotal,
        CuryOriginalBudgetedAmount = current.CuryOriginalBudgetedAmountTotal,
        OriginalBudgetedAmount = current.OriginalBudgetedAmountTotal,
        CuryRevisedBudgetedAmount = current.CuryRevisedBudgetedAmountTotal,
        RevisedBudgetedAmount = current.RevisedBudgetedAmountTotal,
        CuryChangeOrderAmount = current.CuryChangeOrderAmountTotal,
        ChangeOrderAmount = current.ChangeOrderAmountTotal,
        CuryPendingChangeOrderAmount = current.CuryPendingChangeOrderAmountTotal,
        PendingChangeOrderAmount = current.PendingChangeOrderAmountTotal,
        CuryActualAmount = current.CuryActualAmountTotal,
        ActualAmount = current.ActualAmountTotal,
        CuryCommitmentOpenAmount = current.CuryCommitmentOpenAmountTotal,
        CommitmentOpenAmount = current.CommitmentOpenAmountTotal,
        CuryPendingCommitmentAmount = current.CuryPendingCommitmentAmountTotal,
        PendingCommitmentAmount = current.PendingCommitmentAmountTotal
      };
      ((List<object>) pxDelegateResult).Add((object) projectionByDateLine);
    }
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) projectionByDateLines);
    return (IEnumerable) pxDelegateResult;
  }

  public virtual IEnumerable lineCommitments()
  {
    return (IEnumerable) this.GetCurrentLineOpenCommitments();
  }

  public void AddSubcontractType()
  {
    PXStringListAttribute.AppendList<PX.Objects.PO.POOrder.orderType>(((PXGraph) this).Caches[typeof (PX.Objects.PO.POOrder)], (object) null, "RS".CreateArray<string>(), "Subcontract".CreateArray<string>());
  }

  private PMProject GetDocumentProject(PMCostProjectionByDate document)
  {
    return PMProject.PK.Find((PXGraph) this, document.ProjectID);
  }

  private bool IsGroupByInventorySupported(PMCostProjectionByDate document)
  {
    return ((IEnumerable<string>) BudgetLevels.BudgetLevelsWithItem).Contains<string>(this.GetDocumentProject(document)?.CostBudgetLevel);
  }

  private bool IsGroupByCostCodeSupported(PMCostProjectionByDate document)
  {
    return ((IEnumerable<string>) BudgetLevels.BudgetLevelsWithCostCode).Contains<string>(this.GetDocumentProject(document)?.CostBudgetLevel);
  }

  protected bool IsCurrentBudgetEqual(PMCostProjectionByDate document)
  {
    if (!document.GroupByAccountGroupID.GetValueOrDefault() || !document.GroupByProjectTaskID.GetValueOrDefault())
      return false;
    bool? nullable = document.GroupByInventoryID;
    bool flag1 = this.IsGroupByInventorySupported(document);
    if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
      return false;
    nullable = document.GroupByCostCodeID;
    bool flag2 = this.IsGroupByCostCodeSupported(document);
    return nullable.GetValueOrDefault() == flag2 & nullable.HasValue;
  }

  protected virtual Type[] GetBudgetKeyGroupingTypes()
  {
    return this.GetKeyGroupingTypes<PMBudget.projectTaskID, PMBudget.accountGroupID, PMBudget.inventoryID, PMBudget.costCodeID>();
  }

  protected virtual Type[] GetTransactionKeyGroupingTypes()
  {
    return this.GetKeyGroupingTypes<PMHistoryByDate.projectTaskID, PMHistoryByDate.accountGroupID, PMHistoryByDate.inventoryID, PMHistoryByDate.costCodeID>();
  }

  protected virtual Type[] GetChangeOrderKeyGroupingTypes()
  {
    return this.GetKeyGroupingTypes<PMChangeOrderBudget.projectTaskID, PMChangeOrderBudget.accountGroupID, PMChangeOrderBudget.inventoryID, PMChangeOrderBudget.costCodeID>();
  }

  protected virtual Type[] GetCommitmentChangeOrderKeyGroupingTypes()
  {
    return this.GetKeyGroupingTypes<PMChangeOrderLine.taskID, PMAccountGroup.groupID, PMChangeOrderLine.inventoryID, PMChangeOrderLine.costCodeID>();
  }

  private bool GroupingIsActive()
  {
    PMCostProjectionByDate current1 = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.GroupByProjectTaskID;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 == 0)
    {
      PMCostProjectionByDate current2 = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current;
      int num2;
      if (current2 == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = current2.GroupByAccountGroupID;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num2 == 0)
      {
        PMCostProjectionByDate current3 = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current;
        int num3;
        if (current3 == null)
        {
          num3 = 0;
        }
        else
        {
          nullable = current3.GroupByInventoryID;
          num3 = nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num3 == 0)
        {
          PMCostProjectionByDate current4 = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current;
          if (current4 == null)
            return false;
          nullable = current4.GroupByCostCodeID;
          return nullable.GetValueOrDefault();
        }
      }
    }
    return true;
  }

  private Type[] GetKeyGroupingTypes<ProjectTask, AccountGroup, Inventory, CostCode>()
    where ProjectTask : IBqlField
    where AccountGroup : IBqlField
    where Inventory : IBqlField
    where CostCode : IBqlField
  {
    if (((PXSelectBase<PMCostProjectionByDate>) this.Document).Current == null)
      return Array.Empty<Type>();
    List<Type> typeList = new List<Type>();
    bool? nullable = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByProjectTaskID;
    if (nullable.GetValueOrDefault())
      typeList.Add(typeof (ProjectTask));
    nullable = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByAccountGroupID;
    if (nullable.GetValueOrDefault())
      typeList.Add(typeof (AccountGroup));
    nullable = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByInventoryID;
    if (nullable.GetValueOrDefault())
      typeList.Add(typeof (Inventory));
    nullable = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByCostCodeID;
    if (nullable.GetValueOrDefault())
      typeList.Add(typeof (CostCode));
    return typeList.ToArray();
  }

  private Type[] ExtendTypeArray<EmptyField>(Type[] types, int length) where EmptyField : IBqlField
  {
    if (types.Length >= length)
      return types;
    List<Type> typeList = new List<Type>((IEnumerable<Type>) types);
    for (int length1 = types.Length; length1 < length; ++length1)
      typeList.Add(typeof (EmptyField));
    return typeList.ToArray();
  }

  private IEnumerable<T> RunSelectCommandReadOnly<T>(BqlCommand bqlCommand) where T : IBqlTable
  {
    foreach (object obj in new PXView((PXGraph) this, true, bqlCommand).SelectMulti(Array.Empty<object>()))
      yield return PXResult.Unwrap<T>(obj);
  }

  private T RunSingleSelectCommandReadOnly<T>(BqlCommand bqlCommand) where T : IBqlTable
  {
    object obj = new PXView((PXGraph) this, true, bqlCommand).SelectSingle(Array.Empty<object>());
    return obj == null ? default (T) : PXResult.Unwrap<T>(obj);
  }

  private IEnumerable<Table> SelectAggregatedData<Table, EmptyField, Command>(
    Func<Type[]> getGroupingTypes)
    where Table : IBqlTable
    where EmptyField : IBqlField
    where Command : BqlCommand
  {
    Type[] typeArray = this.ExtendTypeArray<EmptyField>(getGroupingTypes(), 4);
    return this.RunSelectCommandReadOnly<Table>(BqlTemplate.OfCommand<Command>.Replace<ProjectCostProjectionByDateEntry.GroupingPlaceholderA>(typeArray[0]).Replace<ProjectCostProjectionByDateEntry.GroupingPlaceholderB>(typeArray[1]).Replace<ProjectCostProjectionByDateEntry.GroupingPlaceholderC>(typeArray[2]).Replace<ProjectCostProjectionByDateEntry.GroupingPlaceholderD>(typeArray[3]).ToCommand());
  }

  private IEnumerable<PMBudget> SelectAggregatedCostBudget()
  {
    return this.SelectAggregatedData<PMBudget, PMBudget.projectID, ProjectCostProjectionByDateEntry.ProjectCostBudgetQueryTemplate>(new Func<Type[]>(this.GetBudgetKeyGroupingTypes));
  }

  private IEnumerable<PMHistoryByDate> SelectAggregatedTransactions()
  {
    return this.SelectAggregatedData<PMHistoryByDate, PMHistoryByDate.groupID, ProjectCostProjectionByDateEntry.ProjectCostTransactionQueryTemplate>(new Func<Type[]>(this.GetTransactionKeyGroupingTypes));
  }

  private IEnumerable<PMChangeOrderBudget> SelectAggregatedChangeOrders()
  {
    return this.SelectAggregatedData<PMChangeOrderBudget, PMChangeOrderBudget.projectID, ProjectCostProjectionByDateEntry.ProjectCostChangeOrderQueryTemplate>(new Func<Type[]>(this.GetChangeOrderKeyGroupingTypes));
  }

  private IEnumerable<PMChangeOrderBudget> SelectAggregatedPendingChangeOrders()
  {
    return this.SelectAggregatedData<PMChangeOrderBudget, PMChangeOrderBudget.projectID, ProjectCostProjectionByDateEntry.ProjectPendingCostChangeOrderQueryTemplate>(new Func<Type[]>(this.GetChangeOrderKeyGroupingTypes));
  }

  private IEnumerable<PMChangeOrderLine> SelectAggregatedPendingCommitmentsChangeOrders()
  {
    return this.SelectAggregatedData<PMChangeOrderLine, PMChangeOrderLine.projectID, ProjectCostProjectionByDateEntry.ProjectPendingCommitmentChangeOrderQueryTemplate>(new Func<Type[]>(this.GetCommitmentChangeOrderKeyGroupingTypes));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<PMCostProjectionByDate, PMCostProjectionByDate.projectID> e)
  {
    this.ClearItems();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID> e)
  {
    ((PXSelectBase) this.Project).Cache.Clear();
    ((PXSelectBase<PMProject>) this.Project).Current = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
    if (e.Row.ProjectID.HasValue)
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByAccountGroupID>((object) e.Row, (object) true);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByProjectTaskID>((object) e.Row, (object) true);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByInventoryID>((object) e.Row, (object) this.IsGroupByInventorySupported(e.Row));
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByCostCodeID>((object) e.Row, (object) this.IsGroupByCostCodeSupported(e.Row));
    }
    else
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByAccountGroupID>((object) e.Row, (object) false);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByProjectTaskID>((object) e.Row, (object) false);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByInventoryID>((object) e.Row, (object) false);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByCostCodeID>((object) e.Row, (object) false);
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.ownerID>((object) e.Row, (object) (int?) ((PXSelectBase<PMProject>) this.Project).Current?.OwnerID);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.workgroupID>((object) e.Row, (object) (int?) ((PXSelectBase<PMProject>) this.Project).Current?.WorkgroupID);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectID>>) e).Cache.SetDefaultExt<PMCostProjectionByDate.updateProjectBudget>((object) e.Row);
    this.ValidateProjectionDate();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.projectID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.projectID>, PMCostProjectionByDate, object>) e).NewValue == null)
      return;
    this.CheckProjectWarning(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.projectID>, PMCostProjectionByDate, object>) e).NewValue as int?);
  }

  protected virtual void CheckProjectWarning(int? projectID)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, projectID);
    if (pmProject == null || !pmProject.StatusCode.HasValue || !StatusCodeHelper.CheckStatus(pmProject.StatusCode, StatusCodes.DateSensitiveActualsIntroduced))
      return;
    ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<PMCostProjectionByDate.projectID>((object) ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current, (object) pmProject.ContractCD, (Exception) new PXSetPropertyException<PMCostProjectionByDate.projectID>("Recalculate the project balance by using the Recalculate Project Balance command on the More menu.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.groupByProjectTaskID> e)
  {
    this.AskAboutFieldUpdatingIfLinesExist((System.Action) (() => ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.groupByProjectTaskID>, PMCostProjectionByDate, object>) e).NewValue = e.OldValue));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.groupByProjectTaskID> e)
  {
    this.ClearItems();
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.groupByProjectTaskID>>) e).Cache.SetDefaultExt<PMCostProjectionByDate.updateProjectBudget>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.groupByAccountGroupID> e)
  {
    this.AskAboutFieldUpdatingIfLinesExist((System.Action) (() => ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.groupByAccountGroupID>, PMCostProjectionByDate, object>) e).NewValue = e.OldValue));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.groupByAccountGroupID> e)
  {
    this.ClearItems();
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.groupByAccountGroupID>>) e).Cache.SetDefaultExt<PMCostProjectionByDate.updateProjectBudget>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.groupByInventoryID> e)
  {
    this.AskAboutFieldUpdatingIfLinesExist((System.Action) (() => ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.groupByInventoryID>, PMCostProjectionByDate, object>) e).NewValue = e.OldValue));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.groupByInventoryID> e)
  {
    this.ClearItems();
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.groupByInventoryID>>) e).Cache.SetDefaultExt<PMCostProjectionByDate.updateProjectBudget>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.groupByCostCodeID> e)
  {
    this.AskAboutFieldUpdatingIfLinesExist((System.Action) (() => ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.groupByCostCodeID>, PMCostProjectionByDate, object>) e).NewValue = e.OldValue));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.groupByCostCodeID> e)
  {
    this.ClearItems();
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.groupByCostCodeID>>) e).Cache.SetDefaultExt<PMCostProjectionByDate.updateProjectBudget>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.includePendingChangeOrders> e)
  {
    this.AskAboutFieldUpdatingIfLinesExist((System.Action) (() => ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.includePendingChangeOrders>, PMCostProjectionByDate, object>) e).NewValue = e.OldValue));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.includePendingChangeOrders> e)
  {
    this.ClearItems();
    this.InitializeTotals(((PXSelectBase<PMCostProjectionByDate>) this.Document).Current, this.GetDocumentProject(((PXSelectBase<PMCostProjectionByDate>) this.Document).Current));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.projectionDate> e)
  {
    this.AskAboutFieldUpdatingIfLinesExist((System.Action) (() => ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDate, PMCostProjectionByDate.projectionDate>, PMCostProjectionByDate, object>) e).NewValue = e.OldValue));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionByDate, PMCostProjectionByDate.projectionDate> e)
  {
    this.ClearItems();
    this.ValidateProjectionDate();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCostProjectionByDate, PMCostProjectionByDate.updateProjectBudget> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostProjectionByDate, PMCostProjectionByDate.updateProjectBudget>, PMCostProjectionByDate, object>) e).NewValue = (object) this.IsCurrentBudgetEqual(e.Row);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostProjectionByDate, PMCostProjectionByDate.updateProjectBudget>>) e).Cancel = true;
  }

  protected virtual void AskAboutFieldUpdatingIfLinesExist(System.Action ifNoAction)
  {
    if (!((IQueryable<PXResult<PMCostProjectionByDateLine>>) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Select(Array.Empty<object>())).Any<PXResult<PMCostProjectionByDateLine>>())
      return;
    if (((PXSelectBase) this.Document).Ask("Warning", "If you change any of the calculation parameters, all cost projection lines will be cleared. Would you like to proceed with changing the parameters?", (MessageButtons) 4, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
    {
      [(WebDialogResult) 6] = "Update",
      [(WebDialogResult) 7] = "Cancel"
    }) != 7)
      return;
    ifNoAction();
  }

  protected virtual void ValidateProjectionDate()
  {
    if (!((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ProjectID.HasValue)
      return;
    PMCostProjectionByDate releasedAtTheSameDate = this.GetAnotherReleasedAtTheSameDate();
    if (releasedAtTheSameDate == null)
      return;
    ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<PMCostProjectionByDate.projectionDate>((object) ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current, (object) ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ProjectionDate, (Exception) new PXSetPropertyException<PMCostProjectionByDate.projectionDate>("For the {0} cost projection date, a released cost projection ({1}) already exists.", (PXErrorLevel) 2, new object[2]
    {
      (object) releasedAtTheSameDate.ProjectionDate.Value.ToShortDateString(),
      (object) releasedAtTheSameDate.RefNbr
    }));
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMCostProjectionByDate> e)
  {
    bool flag1 = e.Row?.Status == "H";
    bool flag2 = e.Row?.Status == "O";
    int num1;
    if (flag1)
    {
      PMCostProjectionByDate row = e.Row;
      num1 = row != null ? (row.ProjectID.HasValue ? 1 : 0) : 0;
    }
    else
      num1 = 0;
    bool flag3 = num1 != 0;
    bool flag4 = flag3 | flag2;
    bool flag5 = ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current != null;
    PXUIFieldAttribute.SetEnabled<PMCostProjectionByDate.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDate>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<PMCostProjectionByDate.date>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDate>>) e).Cache, (object) e.Row, flag3);
    PXUIFieldAttribute.SetEnabled<PMCostProjectionByDate.projectionDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDate>>) e).Cache, (object) e.Row, flag3);
    PXUIFieldAttribute.SetEnabled<PMCostProjectionByDate.groupByAccountGroupID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDate>>) e).Cache, (object) e.Row, flag3);
    PXUIFieldAttribute.SetEnabled<PMCostProjectionByDate.groupByProjectTaskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDate>>) e).Cache, (object) e.Row, flag3);
    PXUIFieldAttribute.SetEnabled<PMCostProjectionByDate.groupByInventoryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDate>>) e).Cache, (object) e.Row, flag3 && this.IsGroupByInventorySupported(e.Row));
    PXUIFieldAttribute.SetEnabled<PMCostProjectionByDate.groupByCostCodeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDate>>) e).Cache, (object) e.Row, flag3 && this.IsGroupByCostCodeSupported(e.Row));
    PXUIFieldAttribute.SetEnabled<PMCostProjectionByDate.includePendingChangeOrders>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDate>>) e).Cache, (object) e.Row, flag3);
    PXUIFieldAttribute.SetEnabled<PMCostProjectionByDate.updateProjectBudget>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDate>>) e).Cache, (object) e.Row, flag4 && this.IsCurrentBudgetEqual(e.Row));
    PXUIFieldAttribute.SetEnabled<PMCostProjectionByDate.description>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDate>>) e).Cache, (object) e.Row, flag4);
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDate>>) e).Cache;
    PMCostProjectionByDate row1 = e.Row;
    PMCostProjectionByDate row2 = e.Row;
    int num2 = row2 != null ? (row2.IncludePendingChangeOrders.GetValueOrDefault() ? 1 : 0) : 0;
    PXUIFieldAttribute.SetVisible<PMCostProjectionByDate.curyPendingRevenueChangeOrderAmountTotal>(cache1, (object) row1, num2 != 0);
    PXCache cache2 = ((PXSelectBase) this.Items).Cache;
    PMCostProjectionByDate row3 = e.Row;
    int num3 = row3 != null ? (row3.GroupByProjectTaskID.GetValueOrDefault() ? 1 : 0) : 0;
    PXUIFieldAttribute.SetVisible<PMCostProjectionByDateLine.projectTaskID>(cache2, (object) null, num3 != 0);
    PXCache cache3 = ((PXSelectBase) this.Items).Cache;
    PMCostProjectionByDate row4 = e.Row;
    int num4 = row4 != null ? (row4.GroupByAccountGroupID.GetValueOrDefault() ? 1 : 0) : 0;
    PXUIFieldAttribute.SetVisible<PMCostProjectionByDateLine.accountGroupID>(cache3, (object) null, num4 != 0);
    PXCache cache4 = ((PXSelectBase) this.Items).Cache;
    PMCostProjectionByDate row5 = e.Row;
    int num5 = row5 != null ? (row5.GroupByInventoryID.GetValueOrDefault() ? 1 : 0) : 0;
    PXUIFieldAttribute.SetVisible<PMCostProjectionByDateLine.inventoryID>(cache4, (object) null, num5 != 0);
    PXCache cache5 = ((PXSelectBase) this.Items).Cache;
    PMCostProjectionByDate row6 = e.Row;
    int num6 = row6 != null ? (row6.GroupByCostCodeID.GetValueOrDefault() ? 1 : 0) : 0;
    PXUIFieldAttribute.SetVisible<PMCostProjectionByDateLine.costCodeID>(cache5, (object) null, num6 != 0);
    PXCache cache6 = ((PXSelectBase) this.Items).Cache;
    PMCostProjectionByDate row7 = e.Row;
    int num7 = row7 != null ? (row7.IncludePendingChangeOrders.GetValueOrDefault() ? 1 : 0) : 0;
    PXUIFieldAttribute.SetVisible<PMCostProjectionByDateLine.curyPendingChangeOrderAmount>(cache6, (object) null, num7 != 0);
    PXCache cache7 = ((PXSelectBase) this.Items).Cache;
    PMCostProjectionByDate row8 = e.Row;
    int num8 = row8 != null ? (row8.IncludePendingChangeOrders.GetValueOrDefault() ? 1 : 0) : 0;
    PXUIFieldAttribute.SetVisible<PMCostProjectionByDateLine.curyPendingCommitmentAmount>(cache7, (object) null, num8 != 0);
    PXUIFieldAttribute.SetVisible<PMProject.curyID>(((PXSelectBase) this.Project).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>());
    ((PXSelectBase) this.Document).Cache.AllowDelete = flag1;
    ((PXSelectBase) this.Items).Cache.AllowInsert = false;
    ((PXSelectBase) this.Items).Cache.AllowDelete = false;
    ((PXSelectBase) this.Items).Cache.AllowUpdate = flag3;
    ((PXAction) this.uploadProjection).SetEnabled(flag3 & flag5);
    ((PXAction) this.removeHold).SetEnabled(flag3 & flag5);
    ((PXAction) this.rebuildProjection).SetEnabled(flag3);
    ((PXAction) this.rebuildProjection).SetCaption(flag5 ? "Refresh" : "Load Lines");
    ((PXSelectBase) this.Approval).AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMCostProjectionByDateLine> e)
  {
    int? nullable;
    int num1;
    if (((PXSelectBase<PMCostProjectionByDate>) this.Document).Current?.Status == "H")
    {
      PMCostProjectionByDate current = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current;
      if (current == null)
      {
        num1 = 0;
      }
      else
      {
        nullable = current.ProjectID;
        num1 = nullable.HasValue ? 1 : 0;
      }
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    bool flag2 = e.Row != null;
    if (e.Row != null)
    {
      PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDateLine>>) e).Cache;
      PMCostProjectionByDateLine row = e.Row;
      Decimal? curyActualAmount = e.Row.CuryActualAmount;
      int num2;
      if (curyActualAmount.HasValue)
      {
        curyActualAmount = e.Row.CuryActualAmount;
        Decimal num3 = 0M;
        num2 = !(curyActualAmount.GetValueOrDefault() == num3 & curyActualAmount.HasValue) ? 1 : 0;
      }
      else
        num2 = 0;
      PXUIFieldAttribute.SetEnabled<PMCostProjectionByDateLine.completedPct>(cache, (object) row, num2 != 0);
    }
    PMCostProjectionByDateLine row1 = e.Row;
    int num4;
    if (row1 == null)
    {
      num4 = 0;
    }
    else
    {
      nullable = row1.LineNbr;
      int num5 = 0;
      num4 = nullable.GetValueOrDefault() == num5 & nullable.HasValue ? 1 : 0;
    }
    if (num4 != 0)
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjectionByDateLine>>) e).Cache, (object) e.Row, false);
    ((PXAction) this.uploadProjection).SetEnabled(flag1 & flag2);
    ((PXAction) this.removeHold).SetEnabled(flag1 & flag2);
    ((PXAction) this.rebuildProjection).SetCaption(flag2 ? "Refresh" : "Load Lines");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyAmountToComplete> e)
  {
    if (this._isRebuilding)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyAmountToComplete>>) e).Cache.SetValue<PMCostProjectionByDateLine.curyProjectedAmount>((object) e.Row, (object) (e.Row.CuryAmountToComplete.GetValueOrDefault() + e.Row.CuryActualAmount.GetValueOrDefault()));
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyAmountToComplete>>) e).Cache.SetValue<PMCostProjectionByDateLine.completedPct>((object) e.Row, (object) this.CalculateCompletedPct(e.Row));
    ((PXSelectBase) this.Report).View.RequestRefresh();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyProjectedAmount> e)
  {
    if (this._isRebuilding)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyProjectedAmount>>) e).Cache.SetValue<PMCostProjectionByDateLine.curyAmountToComplete>((object) e.Row, (object) (e.Row.CuryProjectedAmount.GetValueOrDefault() - e.Row.CuryActualAmount.GetValueOrDefault()));
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyProjectedAmount>>) e).Cache.SetValue<PMCostProjectionByDateLine.completedPct>((object) e.Row, (object) this.CalculateCompletedPct(e.Row));
    ((PXSelectBase) this.Report).View.RequestRefresh();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionByDateLine, PMCostProjectionByDateLine.completedPct> e)
  {
    if (this._isRebuilding)
      return;
    Decimal? nullable = e.Row.CompletedPct;
    Decimal num = 0M;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
    {
      PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDateLine, PMCostProjectionByDateLine.completedPct>>) e).Cache;
      PMCostProjectionByDateLine row = e.Row;
      nullable = e.Row.CuryActualAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = e.Row.CompletedPct;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      // ISSUE: variable of a boxed type
      __Boxed<Decimal> local = (ValueType) (valueOrDefault1 / valueOrDefault2 * 100M);
      cache.SetValue<PMCostProjectionByDateLine.curyProjectedAmount>((object) row, (object) local);
    }
    PXCache cache1 = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostProjectionByDateLine, PMCostProjectionByDateLine.completedPct>>) e).Cache;
    PMCostProjectionByDateLine row1 = e.Row;
    nullable = e.Row.CuryProjectedAmount;
    Decimal valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = e.Row.CuryActualAmount;
    Decimal valueOrDefault4 = nullable.GetValueOrDefault();
    // ISSUE: variable of a boxed type
    __Boxed<Decimal> local1 = (ValueType) (valueOrDefault3 - valueOrDefault4);
    cache1.SetValue<PMCostProjectionByDateLine.curyAmountToComplete>((object) row1, (object) local1);
    ((PXSelectBase) this.Report).View.RequestRefresh();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyAmountToComplete> e)
  {
    if (this._isRebuilding)
      return;
    Decimal? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyAmountToComplete>, PMCostProjectionByDateLine, object>) e).NewValue as Decimal?;
    Decimal num = 0M;
    if (!(newValue.GetValueOrDefault() < num & newValue.HasValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyAmountToComplete>, PMCostProjectionByDateLine, object>) e).NewValue = e.OldValue;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyProjectedAmount> e)
  {
    if (this._isRebuilding)
      return;
    Decimal? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyProjectedAmount>, PMCostProjectionByDateLine, object>) e).NewValue as Decimal?;
    Decimal? curyActualAmount = e.Row.CuryActualAmount;
    if (!(newValue.GetValueOrDefault() < curyActualAmount.GetValueOrDefault() & newValue.HasValue & curyActualAmount.HasValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDateLine, PMCostProjectionByDateLine.curyProjectedAmount>, PMCostProjectionByDateLine, object>) e).NewValue = e.OldValue;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostProjectionByDateLine, PMCostProjectionByDateLine.completedPct> e)
  {
    if (this._isRebuilding)
      return;
    Decimal? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDateLine, PMCostProjectionByDateLine.completedPct>, PMCostProjectionByDateLine, object>) e).NewValue as Decimal?;
    Decimal? nullable = newValue;
    Decimal num1 = 0M;
    if (!(nullable.GetValueOrDefault() < num1 & nullable.HasValue))
    {
      nullable = newValue;
      Decimal num2 = (Decimal) 100;
      if (!(nullable.GetValueOrDefault() > num2 & nullable.HasValue))
        return;
    }
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjectionByDateLine, PMCostProjectionByDateLine.completedPct>, PMCostProjectionByDateLine, object>) e).NewValue = e.OldValue;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable ViewCostCommitments(PXAdapter adapter)
  {
    CommitmentInquiry instance = PXGraph.CreateInstance<CommitmentInquiry>();
    ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ProjectID;
    ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectTaskID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current?.TaskID;
    ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.AccountGroupID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current?.AccountGroupID;
    ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.InventoryID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current?.InventoryID;
    ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.CostCode = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current?.CostCodeID;
    ProjectAccountingService.NavigateToView((PXGraph) instance, "Items", "View Commitments", DataViewHelper.DataViewFilter.Create("POOrder__OrderDate", (PXCondition) 5, (object) ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ProjectionDate));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable ViewLineCostCommitments(PXAdapter adapter)
  {
    ((PXSelectBase<PX.Objects.PO.POLine>) this.LineCommitments).AskExt();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ViewCostTransactions(PXAdapter adapter)
  {
    TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
    ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ProjectID;
    ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectTaskID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current?.TaskID;
    ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.AccountGroupID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current?.AccountGroupID;
    TransactionInquiry.TranFilter current1 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current;
    int? nullable1 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.AccountGroupID;
    string str = !nullable1.HasValue ? "E" : (string) null;
    current1.AccountGroupType = str;
    TransactionInquiry.TranFilter current2 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current;
    PMCostProjectionByDateLine current3 = ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current;
    int? nullable2;
    if (current3 == null)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = current3.InventoryID;
    current2.InventoryID = nullable2;
    TransactionInquiry.TranFilter current4 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current;
    PMCostProjectionByDateLine current5 = ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current;
    int? nullable3;
    if (current5 == null)
    {
      nullable1 = new int?();
      nullable3 = nullable1;
    }
    else
      nullable3 = current5.CostCodeID;
    current4.CostCode = nullable3;
    ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.IncludeUnreleased = new bool?(false);
    ProjectAccountingService.NavigateToView((PXGraph) instance, "Transactions", "View Transactions", DataViewHelper.DataViewFilter.Create("Date", (PXCondition) 5, (object) ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ProjectionDate));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ViewPurchaseOrder(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToPurchaseOrderScreen(PX.Objects.PO.POOrder.PK.Find((PXGraph) this, ((PXSelectBase<PX.Objects.PO.POLine>) this.LineCommitments).Current?.OrderType, ((PXSelectBase<PX.Objects.PO.POLine>) this.LineCommitments).Current?.OrderNbr), string.Empty, (PXBaseRedirectException.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable RunProjectCostAnalysis(PXAdapter adapter)
  {
    ProjectDateSensitiveCostsInquiry instance = PXGraph.CreateInstance<ProjectDateSensitiveCostsInquiry>();
    PMDateSensitiveDataRevision current = ((PXSelectBase<PMDateSensitiveDataRevision>) instance.Revision).Current;
    current.ProjectID = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ProjectID;
    current.ProjectTaskID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current?.TaskID;
    current.AccountGroupID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current?.AccountGroupID;
    current.AccountGroups = "E";
    current.InventoryID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current?.InventoryID;
    current.CostCodeID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Current?.CostCodeID;
    current.EndDate = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ProjectionDate;
    current.GroupByAccountGroupID = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByAccountGroupID;
    current.GroupByProjectTaskID = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByProjectTaskID;
    current.GroupByInventoryID = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByInventoryID;
    current.GroupByCostCodeID = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByCostCodeID;
    ((PXSelectBase<PMDateSensitiveDataRevision>) instance.Revision).Insert(current);
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RebuildProjection(PXAdapter adapter)
  {
    if (((IQueryable<PXResult<PMCostProjectionByDateLine>>) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Select(Array.Empty<object>())).Any<PXResult<PMCostProjectionByDateLine>>())
    {
      if (((PXSelectBase) this.Items).Ask("Warning", "All cost projection lines will be recalculated. Would you like to proceed with refreshing the cost projection lines?", (MessageButtons) 5, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
      {
        [(WebDialogResult) 4] = "Refresh",
        [(WebDialogResult) 2] = "Cancel"
      }) == 2)
        return adapter.Get();
    }
    this.RebuildItems();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Remove Hold", Enabled = false)]
  [PXButton]
  public virtual IEnumerable RemoveHold(PXAdapter adapter)
  {
    this.ValidateProjectionDate();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Hold", Enabled = false)]
  [PXButton]
  public virtual IEnumerable Hold(PXAdapter adapter)
  {
    if (((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.Status == "C")
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ProjectCostProjectionByDateEntry.\u003C\u003Ec__DisplayClass97_0 cDisplayClass970 = new ProjectCostProjectionByDateEntry.\u003C\u003Ec__DisplayClass97_0();
      ((PXAction) this.Save).Press();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass970.refNbr = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.RefNbr;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass970, __methodptr(\u003CHold\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Release", Enabled = false)]
  [PXButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ProjectCostProjectionByDateEntry.\u003C\u003Ec__DisplayClass99_0 cDisplayClass990 = new ProjectCostProjectionByDateEntry.\u003C\u003Ec__DisplayClass99_0();
    if (!((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ProjectID.HasValue)
      return adapter.Get();
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass990.refNbr = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.RefNbr;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass990, __methodptr(\u003CRelease\u003Eb__0)));
    return adapter.Get();
  }

  public static void ReleaseDocument(string refNbr)
  {
    ProjectCostProjectionByDateEntry instance = PXGraph.CreateInstance<ProjectCostProjectionByDateEntry>();
    ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current = PMCostProjectionByDate.PK.Find((PXGraph) instance, refNbr);
    instance.ReleaseCurrentDocument();
  }

  public static void UnreleaseDocument(string refNbr)
  {
    ProjectCostProjectionByDateEntry instance = PXGraph.CreateInstance<ProjectCostProjectionByDateEntry>();
    ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current = PMCostProjectionByDate.PK.Find((PXGraph) instance, refNbr);
    instance.UnreleaseCurrentDocument();
  }

  public virtual void UnreleaseCurrentDocument()
  {
    DateTime? actualTillDate = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ActualTillDate;
    PXResultset<PMCostProjectionByDate> pxResultset = PXSelectBase<PMCostProjectionByDate, PXViewOf<PMCostProjectionByDate>.BasedOn<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.refNbr, NotEqual<BqlField<PMCostProjectionByDate.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.projectID, Equal<BqlField<PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMCostProjectionByDate.status, IBqlString>.IsEqual<ProjectCostProjectionByDateStatus.released>>>>.And<BqlOperand<PMCostProjectionByDate.actualTillDate, IBqlDateTime>.IsEqual<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>());
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (PXResult<PMCostProjectionByDate> pxResult in pxResultset)
      {
        PMCostProjectionByDate projectionByDate = PXResult<PMCostProjectionByDate>.op_Implicit(pxResult);
        ProjectCostProjectionByDateEntry instance = PXGraph.CreateInstance<ProjectCostProjectionByDateEntry>();
        ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current = projectionByDate;
        ((PXSelectBase) instance.Document).Cache.SetValueExt<PMCostProjectionByDate.actualTillDate>((object) ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current, (object) actualTillDate);
        ((PXSelectBase) instance.Document).Cache.Update((object) ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current);
        ((PXAction) instance.Save).Press();
      }
      transactionScope.Complete();
    }
  }

  public virtual void ReleaseCurrentDocument()
  {
    PMCostProjectionByDate releasedAtTheSameDate = this.GetAnotherReleasedAtTheSameDate();
    if (releasedAtTheSameDate != null)
      throw new PXException("For the {0} cost projection date, a released cost projection ({1}) already exists.", new object[2]
      {
        (object) releasedAtTheSameDate.ProjectionDate.Value.ToShortDateString(),
        (object) releasedAtTheSameDate.RefNbr
      });
    PXResultset<PMCostProjectionByDate> pxResultset = PXSelectBase<PMCostProjectionByDate, PXViewOf<PMCostProjectionByDate>.BasedOn<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.refNbr, NotEqual<BqlField<PMCostProjectionByDate.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.projectID, Equal<BqlField<PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMCostProjectionByDate.status, IBqlString>.IsEqual<ProjectCostProjectionByDateStatus.released>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.projectionDate, LessEqual<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.actualTillDate, IsNull>>>>.Or<BqlOperand<PMCostProjectionByDate.actualTillDate, IBqlDateTime>.IsGreater<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>());
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (PXResult<PMCostProjectionByDate> pxResult in pxResultset)
      {
        PMCostProjectionByDate projectionByDate = PXResult<PMCostProjectionByDate>.op_Implicit(pxResult);
        ProjectCostProjectionByDateEntry instance = PXGraph.CreateInstance<ProjectCostProjectionByDateEntry>();
        ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current = projectionByDate;
        ((PXSelectBase) instance.Document).Cache.SetValueExt<PMCostProjectionByDate.actualTillDate>((object) ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current, (object) ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ProjectionDate);
        ((PXSelectBase) instance.Document).Cache.Update((object) ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current);
        ((PXAction) instance.Save).Press();
      }
      ParameterExpression parameterExpression;
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: type reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: type reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: type reference
      // ISSUE: method reference
      // ISSUE: method reference
      DateTime? nullable = ((PXGraph) this).Select<PMCostProjectionByDate>().Where<PMCostProjectionByDate>(Expression.Lambda<Func<PMCostProjectionByDate, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.NotEqual(x.RefNbr, (Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) this, typeof (ProjectCostProjectionByDateEntry)), FieldInfo.GetFieldFromHandle(__fieldref (ProjectCostProjectionByDateEntry.Document))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXSelectBase<PMCostProjectionByDate>.get_Current), __typeref (PXSelectBase<PMCostProjectionByDate>))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PMCostProjectionByDate.get_RefNbr)))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PMCostProjectionByDate.get_ProjectID))), (Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) this, typeof (ProjectCostProjectionByDateEntry)), FieldInfo.GetFieldFromHandle(__fieldref (ProjectCostProjectionByDateEntry.Document))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXSelectBase<PMCostProjectionByDate>.get_Current), __typeref (PXSelectBase<PMCostProjectionByDate>))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PMCostProjectionByDate.get_ProjectID))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PMCostProjectionByDate.get_Status))), (Expression) Expression.Constant((object) "C", typeof (string)))), (Expression) Expression.GreaterThan((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PMCostProjectionByDate.get_ProjectionDate))), (Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) this, typeof (ProjectCostProjectionByDateEntry)), FieldInfo.GetFieldFromHandle(__fieldref (ProjectCostProjectionByDateEntry.Document))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXSelectBase<PMCostProjectionByDate>.get_Current), __typeref (PXSelectBase<PMCostProjectionByDate>))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PMCostProjectionByDate.get_ProjectionDate))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (DateTime.op_GreaterThan)))), parameterExpression)).Min<PMCostProjectionByDate, DateTime?>((Expression<Func<PMCostProjectionByDate, DateTime?>>) (x => x.ProjectionDate));
      if (nullable.HasValue)
      {
        ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.ActualTillDate = nullable;
        ((PXSelectBase) this.Document).Cache.Update((object) ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current);
        ((PXAction) this.Save).Press();
      }
      if (((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.UpdateProjectBudget.GetValueOrDefault())
        this.UpdateProjectBudget();
      transactionScope.Complete();
    }
  }

  [PXUIField(DisplayName = "Copy Projection", Enabled = false)]
  [PXButton]
  public virtual IEnumerable UploadProjection(PXAdapter adapter)
  {
    if (((PXSelectBase<ProjectCostProjectionByDateEntry.SelectedCostProjection>) this.CostProjectionsToCopyFrom).AskExt() == 1 && ((PXSelectBase<ProjectCostProjectionByDateEntry.SelectedCostProjection>) this.CostProjectionsToCopyFrom).Current != null)
      this.CopyProjectedValues((PMCostProjectionByDate) ((PXSelectBase<ProjectCostProjectionByDateEntry.SelectedCostProjection>) this.CostProjectionsToCopyFrom).Current);
    return adapter.Get();
  }

  protected virtual void CopyProjectedValues(PMCostProjectionByDate projection)
  {
    Dictionary<BudgetKeyTuple, PMCostProjectionByDateLine> dictionary = GraphHelper.RowCast<PMCostProjectionByDateLine>((IEnumerable) PXSelectBase<PMCostProjectionByDateLine, PXViewOf<PMCostProjectionByDateLine>.BasedOn<SelectFromBase<PMCostProjectionByDateLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMCostProjectionByDateLine.refNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projection.RefNbr
    })).ToDictionary<PMCostProjectionByDateLine, BudgetKeyTuple, PMCostProjectionByDateLine>((Func<PMCostProjectionByDateLine, BudgetKeyTuple>) (key => BudgetKeyTuple.Create((IProjectFilter) key)), (Func<PMCostProjectionByDateLine, PMCostProjectionByDateLine>) (value => value));
    foreach (PXResult<PMCostProjectionByDateLine> pxResult in ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Select(Array.Empty<object>()))
    {
      PMCostProjectionByDateLine budget = PXResult<PMCostProjectionByDateLine>.op_Implicit(pxResult);
      PMCostProjectionByDateLine projectionByDateLine;
      if (dictionary.TryGetValue(BudgetKeyTuple.Create((IProjectFilter) budget), out projectionByDateLine))
      {
        Decimal? curyProjectedAmount = projectionByDateLine.CuryProjectedAmount;
        Decimal? curyCompletedAmount = budget.CuryCompletedAmount;
        if (curyProjectedAmount.GetValueOrDefault() > curyCompletedAmount.GetValueOrDefault() & curyProjectedAmount.HasValue & curyCompletedAmount.HasValue)
        {
          budget.CuryProjectedAmount = projectionByDateLine.CuryProjectedAmount;
          ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Update(budget);
        }
      }
    }
  }

  protected virtual void UpdateProjectBudget()
  {
    if (!this.IsCurrentBudgetEqual(((PXSelectBase<PMCostProjectionByDate>) this.Document).Current))
      return;
    PMCostProjectionByDate projectionByDate = PXResultset<PMCostProjectionByDate>.op_Implicit(PXSelectBase<PMCostProjectionByDate, PXViewOf<PMCostProjectionByDate>.BasedOn<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.projectID, Equal<BqlField<PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.refNbr, NotEqual<BqlField<PMCostProjectionByDate.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.projectionDate, Greater<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.released, Equal<True>>>>>.And<BqlOperand<PMCostProjectionByDate.updateProjectBudget, IBqlBool>.IsEqual<True>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (projectionByDate != null)
      throw new PXException("The cost projection for the {0} project cannot be released because the {1} cost projection has already been released for the {2} date. To be able to release this cost projection, clear the Update Project Budget check box first.", new object[3]
      {
        (object) this.GetDocumentProject(((PXSelectBase<PMCostProjectionByDate>) this.Document).Current)?.ContractCD,
        (object) projectionByDate.RefNbr,
        (object) projectionByDate.ProjectionDate.Value.ToShortDateString()
      });
    Dictionary<BudgetKeyTuple, PMCostProjectionByDateLine> dictionary = GraphHelper.RowCast<PMCostProjectionByDateLine>((IEnumerable) ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Select(Array.Empty<object>())).ToDictionary<PMCostProjectionByDateLine, BudgetKeyTuple, PMCostProjectionByDateLine>((Func<PMCostProjectionByDateLine, BudgetKeyTuple>) (key => BudgetKeyTuple.Create((IProjectFilter) key)), (Func<PMCostProjectionByDateLine, PMCostProjectionByDateLine>) (value => value));
    GraphHelper.RowCast<PMCostBudget>((IEnumerable) ((PXSelectBase<PMCostBudget>) this.CostBudget).Select(Array.Empty<object>())).ToDictionary<PMCostBudget, BudgetKeyTuple, PMCostBudget>((Func<PMCostBudget, BudgetKeyTuple>) (key => BudgetKeyTuple.Create((IProjectFilter) key)), (Func<PMCostBudget, PMCostBudget>) (value => value));
    foreach (PMCostBudget budget in GraphHelper.RowCast<PMCostBudget>((IEnumerable) ((PXSelectBase<PMCostBudget>) this.CostBudget).Select(Array.Empty<object>())))
    {
      PMCostProjectionByDateLine projectionByDateLine;
      if (dictionary.TryGetValue(BudgetKeyTuple.Create((IProjectFilter) budget), out projectionByDateLine))
      {
        budget.CostProjectionCompletedPct = projectionByDateLine.CompletedPct;
        budget.CuryCostProjectionCostToComplete = projectionByDateLine.CuryAmountToComplete;
        budget.CuryCostProjectionCostAtCompletion = projectionByDateLine.CuryProjectedAmount;
        budget.CostProjectionQtyToComplete = new Decimal?(0M);
        budget.CostProjectionQtyAtCompletion = new Decimal?(0M);
        ((PXSelectBase<PMCostBudget>) this.CostBudget).Update(budget);
      }
    }
  }

  protected virtual PMCostProjectionByDate GetAnotherReleasedAtTheSameDate()
  {
    PMCostProjectionByDate current1 = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current;
    if ((current1 != null ? (!current1.ProjectID.HasValue ? 1 : 0) : 1) == 0)
    {
      PMCostProjectionByDate current2 = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current;
      if ((current2 != null ? (!current2.ProjectionDate.HasValue ? 1 : 0) : 1) == 0)
      {
        BqlCommand bqlCommand = PXSelectBase<PMCostProjectionByDate, PXViewOf<PMCostProjectionByDate>.BasedOn<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.projectID, Equal<BqlField<PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.projectionDate, Equal<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlOperand<PMCostProjectionByDate.released, IBqlBool>.IsEqual<True>>>>>.Config>.GetCommand();
        if (((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current) != 2)
          bqlCommand = bqlCommand.WhereAnd<Where<BqlOperand<PMCostProjectionByDate.refNbr, IBqlString>.IsNotEqual<BqlField<PMCostProjectionByDate.refNbr, IBqlString>.FromCurrent>>>();
        return new PXView((PXGraph) this, true, bqlCommand).SelectSingle(Array.Empty<object>()) as PMCostProjectionByDate;
      }
    }
    return (PMCostProjectionByDate) null;
  }

  protected virtual string ProjectKey(IProjectFilter item)
  {
    return this.ProjectKey(item.TaskID, item.AccountGroupID, item.InventoryID, item.CostCodeID);
  }

  protected virtual string ProjectKey(
    int? projectTaskID,
    int? accountGroupID,
    int? inventoryID,
    int? costCodeID)
  {
    return $"T{(((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByProjectTaskID.GetValueOrDefault() ? projectTaskID?.ToString() : string.Empty)}G{(((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByAccountGroupID.GetValueOrDefault() ? accountGroupID?.ToString() : string.Empty)}I{(((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByInventoryID.GetValueOrDefault() ? inventoryID?.ToString() : string.Empty)}C{(((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByCostCodeID.GetValueOrDefault() ? costCodeID?.ToString() : string.Empty)}";
  }

  protected virtual string[] ProjectKeys(IProjectFilter item)
  {
    List<string> stringList = new List<string>()
    {
      this.ProjectKey(item)
    };
    int num1;
    if (((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByInventoryID.GetValueOrDefault() && item.InventoryID.HasValue)
    {
      int? inventoryId = item.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      num1 = !(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    if (flag)
      stringList.Add(this.ProjectKey(item.TaskID, item.AccountGroupID, new int?(PMInventorySelectorAttribute.EmptyInventoryID), item.CostCodeID));
    int num2;
    if (((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByCostCodeID.GetValueOrDefault() && item.CostCodeID.HasValue)
    {
      int? costCodeId = item.CostCodeID;
      int defaultCostCode = CostCodeAttribute.GetDefaultCostCode();
      num2 = !(costCodeId.GetValueOrDefault() == defaultCostCode & costCodeId.HasValue) ? 1 : 0;
    }
    else
      num2 = 0;
    if (num2 != 0)
    {
      stringList.Add(this.ProjectKey(item.TaskID, item.AccountGroupID, item.InventoryID, new int?(CostCodeAttribute.GetDefaultCostCode())));
      if (flag)
        stringList.Add(this.ProjectKey(item.TaskID, item.AccountGroupID, new int?(PMInventorySelectorAttribute.EmptyInventoryID), new int?(CostCodeAttribute.GetDefaultCostCode())));
    }
    return stringList.ToArray();
  }

  protected virtual IList<PMCostProjectionByDateLine> BuildCostProjectionByDateLines()
  {
    List<PMCostProjectionByDateLine> projectionByDateLineList = new List<PMCostProjectionByDateLine>();
    int num = 0;
    foreach (PMBudget pmBudget in this.SelectAggregatedCostBudget())
    {
      PMCostProjectionByDateLine projectionByDateLine1 = new PMCostProjectionByDateLine();
      projectionByDateLine1.RefNbr = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.RefNbr;
      projectionByDateLine1.LineNbr = new int?(++num);
      bool? nullable = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByProjectTaskID;
      projectionByDateLine1.ProjectTaskID = nullable.GetValueOrDefault() ? pmBudget.ProjectTaskID : new int?();
      nullable = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByAccountGroupID;
      projectionByDateLine1.AccountGroupID = nullable.GetValueOrDefault() ? pmBudget.AccountGroupID : new int?();
      nullable = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByInventoryID;
      projectionByDateLine1.InventoryID = nullable.GetValueOrDefault() ? pmBudget.InventoryID : new int?();
      nullable = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.GroupByCostCodeID;
      projectionByDateLine1.CostCodeID = nullable.GetValueOrDefault() ? pmBudget.CostCodeID : new int?();
      projectionByDateLine1.CuryOriginalBudgetedAmount = pmBudget.CuryAmount;
      projectionByDateLine1.OriginalBudgetedAmount = pmBudget.Amount;
      projectionByDateLine1.CuryRevisedBudgetedAmount = pmBudget.CuryRevisedAmount;
      projectionByDateLine1.RevisedBudgetedAmount = pmBudget.RevisedAmount;
      PMCostProjectionByDateLine projectionByDateLine2 = projectionByDateLine1;
      projectionByDateLineList.Add(projectionByDateLine2);
    }
    return (IList<PMCostProjectionByDateLine>) projectionByDateLineList;
  }

  protected virtual void MergeActuals(IList<PMCostProjectionByDateLine> reportItems)
  {
    Dictionary<string, PMCostProjectionByDateLine> dictionary = reportItems.ToDictionary<PMCostProjectionByDateLine, string, PMCostProjectionByDateLine>(new Func<PMCostProjectionByDateLine, string>(this.ProjectKey), (Func<PMCostProjectionByDateLine, PMCostProjectionByDateLine>) (x => x));
    foreach (PMHistoryByDate aggregatedTransaction in this.SelectAggregatedTransactions())
    {
      PMCostProjectionByDateLine projectionByDateLine1 = (PMCostProjectionByDateLine) null;
      foreach (string projectKey in this.ProjectKeys((IProjectFilter) aggregatedTransaction))
      {
        if (dictionary.TryGetValue(projectKey, out projectionByDateLine1))
          break;
      }
      if (projectionByDateLine1 != null)
      {
        PMCostProjectionByDateLine projectionByDateLine2 = projectionByDateLine1;
        Decimal? nullable1 = projectionByDateLine1.CuryActualAmount;
        Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
        nullable1 = aggregatedTransaction.CuryActualAmount;
        Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
        Decimal? nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
        projectionByDateLine2.CuryActualAmount = nullable2;
        PMCostProjectionByDateLine projectionByDateLine3 = projectionByDateLine1;
        nullable1 = projectionByDateLine1.ActualAmount;
        Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
        nullable1 = aggregatedTransaction.ActualAmount;
        Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
        Decimal? nullable3 = new Decimal?(valueOrDefault3 + valueOrDefault4);
        projectionByDateLine3.ActualAmount = nullable3;
      }
    }
  }

  protected virtual void MergeChanges(
    IList<PMCostProjectionByDateLine> reportItems,
    PMProject project)
  {
    Dictionary<string, PMCostProjectionByDateLine> dictionary = reportItems.ToDictionary<PMCostProjectionByDateLine, string, PMCostProjectionByDateLine>(new Func<PMCostProjectionByDateLine, string>(this.ProjectKey), (Func<PMCostProjectionByDateLine, PMCostProjectionByDateLine>) (x => x));
    foreach (PMChangeOrderBudget aggregatedChangeOrder in this.SelectAggregatedChangeOrders())
    {
      PMCostProjectionByDateLine projectionByDateLine1 = (PMCostProjectionByDateLine) null;
      foreach (string projectKey in this.ProjectKeys((IProjectFilter) aggregatedChangeOrder))
      {
        if (dictionary.TryGetValue(projectKey, out projectionByDateLine1))
          break;
      }
      if (projectionByDateLine1 != null)
      {
        PMCostProjectionByDateLine projectionByDateLine2 = projectionByDateLine1;
        Decimal? nullable1 = projectionByDateLine1.CuryChangeOrderAmount;
        Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
        nullable1 = aggregatedChangeOrder.Amount;
        Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
        Decimal? nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
        projectionByDateLine2.CuryChangeOrderAmount = nullable2;
      }
    }
    bool? nullable3 = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.IncludePendingChangeOrders;
    if (!nullable3.GetValueOrDefault())
      return;
    nullable3 = project.ChangeOrderWorkflow;
    if (!nullable3.GetValueOrDefault())
      return;
    foreach (PMChangeOrderBudget pendingChangeOrder in this.SelectAggregatedPendingChangeOrders())
    {
      PMCostProjectionByDateLine projectionByDateLine3 = (PMCostProjectionByDateLine) null;
      foreach (string projectKey in this.ProjectKeys((IProjectFilter) pendingChangeOrder))
      {
        if (dictionary.TryGetValue(projectKey, out projectionByDateLine3))
          break;
      }
      if (projectionByDateLine3 != null)
      {
        PMCostProjectionByDateLine projectionByDateLine4 = projectionByDateLine3;
        Decimal? nullable4 = projectionByDateLine3.CuryPendingChangeOrderAmount;
        Decimal valueOrDefault3 = nullable4.GetValueOrDefault();
        nullable4 = pendingChangeOrder.Amount;
        Decimal valueOrDefault4 = nullable4.GetValueOrDefault();
        Decimal? nullable5 = new Decimal?(valueOrDefault3 + valueOrDefault4);
        projectionByDateLine4.CuryPendingChangeOrderAmount = nullable5;
      }
    }
  }

  protected virtual void MergeCommitments(
    IList<PMCostProjectionByDateLine> reportItems,
    PMProject project)
  {
    Dictionary<string, PMCostProjectionByDateLine> dictionary = reportItems.ToDictionary<PMCostProjectionByDateLine, string, PMCostProjectionByDateLine>(new Func<PMCostProjectionByDateLine, string>(this.ProjectKey), (Func<PMCostProjectionByDateLine, PMCostProjectionByDateLine>) (x => x));
    foreach (KeyValuePair<BudgetKeyTuple, Decimal> gatherOpenCommitment in (IEnumerable<KeyValuePair<BudgetKeyTuple, Decimal>>) this.GatherOpenCommitments())
    {
      if (!(gatherOpenCommitment.Value == 0M))
      {
        PMCostProjectionByDateLine projectionByDateLine = (PMCostProjectionByDateLine) null;
        foreach (string projectKey in this.ProjectKeys((IProjectFilter) gatherOpenCommitment.Key))
        {
          if (dictionary.TryGetValue(projectKey, out projectionByDateLine))
            break;
        }
        if (projectionByDateLine != null)
          projectionByDateLine.CuryCommitmentOpenAmount = new Decimal?(projectionByDateLine.CuryCommitmentOpenAmount.GetValueOrDefault() + gatherOpenCommitment.Value);
      }
    }
    bool? nullable1 = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.IncludePendingChangeOrders;
    if (!nullable1.GetValueOrDefault())
      return;
    nullable1 = project.ChangeOrderWorkflow;
    if (!nullable1.GetValueOrDefault())
      return;
    foreach (PMChangeOrderLine commitmentsChangeOrder in this.SelectAggregatedPendingCommitmentsChangeOrders())
    {
      PMCostProjectionByDateLine projectionByDateLine1 = (PMCostProjectionByDateLine) null;
      BudgetKeyTuple budgetKeyTuple;
      ref BudgetKeyTuple local = ref budgetKeyTuple;
      int? nullable2 = commitmentsChangeOrder.ProjectID;
      int valueOrDefault1 = nullable2.GetValueOrDefault();
      nullable2 = commitmentsChangeOrder.TaskID;
      int valueOrDefault2 = nullable2.GetValueOrDefault();
      int? nullable3 = (int?) PX.Objects.GL.Account.PK.Find((PXGraph) this, commitmentsChangeOrder.AccountID)?.AccountGroupID;
      int valueOrDefault3 = nullable3.GetValueOrDefault();
      nullable3 = commitmentsChangeOrder.InventoryID;
      int valueOrDefault4 = nullable3.GetValueOrDefault();
      nullable3 = commitmentsChangeOrder.CostCodeID;
      int valueOrDefault5 = nullable3.GetValueOrDefault();
      local = new BudgetKeyTuple(valueOrDefault1, valueOrDefault2, valueOrDefault3, valueOrDefault4, valueOrDefault5);
      foreach (string projectKey in this.ProjectKeys((IProjectFilter) budgetKeyTuple))
      {
        if (dictionary.TryGetValue(projectKey, out projectionByDateLine1))
          break;
      }
      if (projectionByDateLine1 != null)
      {
        PMCostProjectionByDateLine projectionByDateLine2 = projectionByDateLine1;
        Decimal? nullable4 = projectionByDateLine1.CuryPendingCommitmentAmount;
        Decimal valueOrDefault6 = nullable4.GetValueOrDefault();
        nullable4 = commitmentsChangeOrder.AmountInProjectCury;
        Decimal valueOrDefault7 = nullable4.GetValueOrDefault();
        Decimal? nullable5 = new Decimal?(valueOrDefault6 + valueOrDefault7);
        projectionByDateLine2.CuryPendingCommitmentAmount = nullable5;
      }
    }
  }

  protected virtual IList<KeyValuePair<BudgetKeyTuple, Decimal>> GatherOpenCommitments()
  {
    IProjectMultiCurrency instance = ServiceLocator.Current.GetInstance<IProjectMultiCurrency>();
    PMProject documentProject = this.GetDocumentProject(((PXSelectBase<PMCostProjectionByDate>) this.Document).Current);
    Dictionary<BudgetKeyTuple, Decimal> source = new Dictionary<BudgetKeyTuple, Decimal>();
    PX.Objects.PO.POLine poLine = (PX.Objects.PO.POLine) null;
    foreach (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran, PMTran> pxResult in PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POOrder>.On<PX.Objects.PO.POLine.FK.Order>>, FbqlJoins.Left<PX.Objects.AP.APTran>.On<PX.Objects.AP.APTran.FK.POLine>>, FbqlJoins.Left<PMTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.origTranType, Equal<PX.Objects.AP.APTran.tranType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.origRefNbr, Equal<PX.Objects.AP.APTran.refNbr>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.origLineNbr, Equal<PX.Objects.AP.APTran.lineNbr>>>>>.And<BqlOperand<PMTran.date, IBqlDateTime>.IsLessEqual<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.orderDate, LessEqual<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.PO.POLine.projectID, IBqlInt>.IsEqual<BqlField<PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>.Order<By<BqlField<PX.Objects.PO.POLine.orderType, IBqlString>.Asc, BqlField<PX.Objects.PO.POLine.orderNbr, IBqlString>.Asc, BqlField<PX.Objects.PO.POLine.lineNbr, IBqlInt>.Asc>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      PX.Objects.PO.POOrder poOrder = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran, PMTran>.op_Implicit(pxResult);
      PX.Objects.PO.POLine line = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran, PMTran>.op_Implicit(pxResult);
      int? accountGroupId = POCommitmentAttribute.GetAccountGroupID((PXGraph) this, line);
      if (accountGroupId.HasValue)
      {
        BudgetKeyTuple key = new BudgetKeyTuple(line.ProjectID.GetValueOrDefault(), line.TaskID.GetValueOrDefault(), accountGroupId.GetValueOrDefault(), line.InventoryID.GetValueOrDefault(), line.CostCodeID.GetValueOrDefault());
        if (!source.ContainsKey(key))
          source[key] = 0M;
        if (poLine != null && !(poLine.OrderType != line.OrderType) && !(poLine.OrderNbr != line.OrderNbr))
        {
          int? lineNbr1 = poLine.LineNbr;
          int? lineNbr2 = line.LineNbr;
          if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
            goto label_8;
        }
        source[key] += instance.GetValueInProjectCurrency((PXGraph) this, documentProject, poOrder.CuryID, poOrder.OrderDate, line.CuryLineAmt);
label_8:
        PMTran pmTran = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran, PMTran>.op_Implicit(pxResult);
        if (pmTran != null)
          source[key] -= pmTran.ProjectCuryAmount.GetValueOrDefault();
      }
      poLine = line;
    }
    return (IList<KeyValuePair<BudgetKeyTuple, Decimal>>) source.ToList<KeyValuePair<BudgetKeyTuple, Decimal>>();
  }

  protected virtual PXResultset<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran, PMTran> GetCurrentLineOpenCommitments()
  {
    PXResultset<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran, PMTran> lineOpenCommitments = new PXResultset<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran, PMTran>();
    if (!(((PXSelectBase) this.Report).Cache.Current is PMCostProjectionByDateLine current))
      return lineOpenCommitments;
    BqlCommand bqlCommand = PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POOrder>.On<PX.Objects.PO.POLine.FK.Order>>, FbqlJoins.Left<PX.Objects.AP.APTran>.On<PX.Objects.AP.APTran.FK.POLine>>, FbqlJoins.Left<PMTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.origTranType, Equal<PX.Objects.AP.APTran.tranType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.origRefNbr, Equal<PX.Objects.AP.APTran.refNbr>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.origLineNbr, Equal<PX.Objects.AP.APTran.lineNbr>>>>>.And<BqlOperand<PMTran.date, IBqlDateTime>.IsLessEqual<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.orderDate, LessEqual<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.PO.POLine.projectID, IBqlInt>.IsEqual<BqlField<PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>.Order<By<BqlField<PX.Objects.PO.POLine.orderType, IBqlString>.Asc, BqlField<PX.Objects.PO.POLine.orderNbr, IBqlString>.Asc, BqlField<PX.Objects.PO.POLine.lineNbr, IBqlInt>.Asc>>>.Config>.GetCommand();
    int? nullable1;
    if (current != null)
    {
      nullable1 = current.ProjectTaskID;
      if (nullable1.HasValue)
        bqlCommand = bqlCommand.WhereAnd<Where<BqlOperand<PX.Objects.PO.POLine.taskID, IBqlInt>.IsEqual<BqlField<PMCostProjectionByDateLine.projectTaskID, IBqlInt>.FromCurrent>>>();
    }
    if (current != null)
    {
      nullable1 = current.InventoryID;
      if (nullable1.HasValue)
        bqlCommand = bqlCommand.WhereAnd<Where<BqlOperand<PX.Objects.PO.POLine.inventoryID, IBqlInt>.IsEqual<BqlField<PMCostProjectionByDateLine.inventoryID, IBqlInt>.FromCurrent>>>();
    }
    if (current != null)
    {
      nullable1 = current.CostCodeID;
      if (nullable1.HasValue)
        bqlCommand = bqlCommand.WhereAnd<Where<BqlOperand<PX.Objects.PO.POLine.costCodeID, IBqlInt>.IsEqual<BqlField<PMCostProjectionByDateLine.costCodeID, IBqlInt>.FromCurrent>>>();
    }
    PXView pxView = new PXView((PXGraph) this, true, bqlCommand);
    PX.Objects.PO.POLine poLine = (PX.Objects.PO.POLine) null;
    object[] objArray = Array.Empty<object>();
    foreach (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran, PMTran> pxResult in pxView.SelectMulti(objArray))
    {
      PX.Objects.PO.POLine line = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran, PMTran>.op_Implicit(pxResult);
      int? accountGroupId = POCommitmentAttribute.GetAccountGroupID((PXGraph) this, line);
      if (accountGroupId.HasValue)
      {
        nullable1 = current.AccountGroupID;
        int? nullable2;
        if (nullable1.HasValue)
        {
          nullable1 = current.AccountGroupID;
          nullable2 = accountGroupId;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            goto label_23;
        }
        PMTran pmTran = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran, PMTran>.op_Implicit(pxResult);
        if (pmTran != null)
        {
          nullable2 = pmTran.AccountGroupID;
          if (!nullable2.HasValue)
            pmTran.AccountGroupID = accountGroupId;
        }
        if (poLine != null && poLine.OrderType == line.OrderType && poLine.OrderNbr == line.OrderNbr)
        {
          nullable2 = poLine.LineNbr;
          nullable1 = line.LineNbr;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            line.CuryLineAmt = new Decimal?();
        }
        ((PXResultset<PX.Objects.PO.POLine>) lineOpenCommitments).Add((PXResult<PX.Objects.PO.POLine>) pxResult);
      }
label_23:
      poLine = line;
    }
    return lineOpenCommitments;
  }

  protected virtual void InitializeLine(PMCostProjectionByDateLine line, PMProject project)
  {
    PMCostProjectionByDateLine projectionByDateLine1 = line;
    Decimal? nullable1;
    Decimal? nullable2;
    if (!project.ChangeOrderWorkflow.GetValueOrDefault())
    {
      nullable2 = line.CuryRevisedBudgetedAmount;
    }
    else
    {
      nullable1 = line.CuryOriginalBudgetedAmount;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = line.CuryChangeOrderAmount;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    }
    projectionByDateLine1.CuryBudgetedAmount = nullable2;
    PMCostProjectionByDateLine projectionByDateLine2 = line;
    nullable1 = line.CuryBudgetedAmount;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = line.CuryPendingChangeOrderAmount;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    Decimal val1 = valueOrDefault3 + valueOrDefault4;
    nullable1 = line.CuryCompletedAmount;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(Math.Max(val1, valueOrDefault5));
    projectionByDateLine2.CuryProjectedAmount = nullable3;
    PMCostProjectionByDateLine projectionByDateLine3 = line;
    nullable1 = line.CuryProjectedAmount;
    Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
    nullable1 = line.CuryActualAmount;
    Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
    Decimal? nullable4 = new Decimal?(valueOrDefault6 - valueOrDefault7);
    projectionByDateLine3.CuryAmountToComplete = nullable4;
    line.CompletedPct = this.CalculateCompletedPct(line);
  }

  protected virtual void InitializeTotals(PMCostProjectionByDate totals, PMProject project)
  {
    PMBudget pmBudget = this.RunSingleSelectCommandReadOnly<PMBudget>(PXSelectBase<PMBudget, PXViewOf<PMBudget>.BasedOn<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBudget.projectID, Equal<BqlField<PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PMBudget.type, IBqlString>.IsEqual<AccountType.income>>>.Aggregate<To<GroupBy<PMBudget.projectID>, Sum<PMBudget.curyAmount>, Sum<PMBudget.amount>, Sum<PMBudget.curyRevisedAmount>, Sum<PMBudget.revisedAmount>>>>.Config>.GetCommand());
    PMChangeOrderBudget changeOrderBudget1 = this.RunSingleSelectCommandReadOnly<PMChangeOrderBudget>(PXSelectBase<PMChangeOrderBudget, PXViewOf<PMChangeOrderBudget>.BasedOn<SelectFromBase<PMChangeOrderBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<PMChangeOrder.refNbr, IBqlString>.IsEqual<PMChangeOrderBudget.refNbr>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrderBudget.projectID, Equal<BqlField<PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMChangeOrderBudget.type, IBqlString>.IsEqual<AccountType.income>>>, And<BqlOperand<PMChangeOrder.released, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PMChangeOrder.date, IBqlDateTime>.IsLessEqual<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>.Aggregate<To<GroupBy<PMChangeOrderBudget.projectID>, Sum<PMChangeOrderBudget.amount>>>>.Config>.GetCommand());
    PMTran pmTran = this.RunSingleSelectCommandReadOnly<PMTran>(PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.projectID, Equal<BqlField<PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMTran.tranType, IBqlString>.IsEqual<BatchModule.moduleAR>>>>.And<BqlOperand<PMTran.date, IBqlDateTime>.IsLessEqual<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>.Aggregate<To<GroupBy<PMTran.projectID>, Sum<PMTran.projectCuryAmount>, Sum<PMTran.amount>>>>.Config>.GetCommand());
    bool? changeOrderWorkflow;
    if (((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.IncludePendingChangeOrders.GetValueOrDefault())
    {
      changeOrderWorkflow = project.ChangeOrderWorkflow;
      if (changeOrderWorkflow.GetValueOrDefault())
      {
        PMChangeOrderBudget changeOrderBudget2 = this.RunSingleSelectCommandReadOnly<PMChangeOrderBudget>(PXSelectBase<PMChangeOrderBudget, PXViewOf<PMChangeOrderBudget>.BasedOn<SelectFromBase<PMChangeOrderBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<PMChangeOrder.refNbr, IBqlString>.IsEqual<PMChangeOrderBudget.refNbr>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrderBudget.projectID, Equal<BqlField<PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMChangeOrderBudget.type, IBqlString>.IsEqual<AccountType.income>>>, And<BqlOperand<PMChangeOrder.released, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PMChangeOrder.status, IBqlString>.IsIn<ChangeOrderStatus.onHold, ChangeOrderStatus.open, ChangeOrderStatus.pendingApproval>>>>.And<BqlOperand<PMChangeOrder.date, IBqlDateTime>.IsLessEqual<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>.Aggregate<To<GroupBy<PMChangeOrderBudget.projectID>, Sum<PMChangeOrderBudget.amount>>>>.Config>.GetCommand());
        totals.CuryPendingRevenueChangeOrderAmountTotal = (Decimal?) changeOrderBudget2?.Amount;
        goto label_4;
      }
    }
    totals.CuryPendingRevenueChangeOrderAmountTotal = new Decimal?();
label_4:
    PMCostProjectionByDate projectionByDate = totals;
    changeOrderWorkflow = project.ChangeOrderWorkflow;
    Decimal? nullable = new Decimal?(changeOrderWorkflow.GetValueOrDefault() ? ((Decimal?) pmBudget?.CuryAmount).GetValueOrDefault() + ((Decimal?) changeOrderBudget1?.Amount).GetValueOrDefault() : ((Decimal?) pmBudget?.CuryRevisedAmount).GetValueOrDefault());
    projectionByDate.CuryRevisedRevenueBudgetedAmountTotal = nullable;
    totals.CuryBilledRevenueAmountTotal = new Decimal?(-((Decimal?) pmTran?.ProjectCuryAmount).GetValueOrDefault());
    totals.BilledRevenueAmountTotal = new Decimal?(-((Decimal?) pmTran?.Amount).GetValueOrDefault());
  }

  protected virtual Decimal? CalculateCompletedPct(PMCostProjectionByDateLine line)
  {
    return line.CuryProjectedAmount.GetValueOrDefault() != 0M ? new Decimal?(this.RoundPct(line.CuryActualAmount.GetValueOrDefault() / line.CuryProjectedAmount.GetValueOrDefault() * 100M)) : new Decimal?();
  }

  protected virtual Decimal RoundPct(Decimal value) => Math.Round(value, 2);

  protected virtual void ClearItems()
  {
    foreach (PXResult<PMCostProjectionByDateLine> pxResult in ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Select(Array.Empty<object>()))
      ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Delete(PXResult<PMCostProjectionByDateLine>.op_Implicit(pxResult));
  }

  protected virtual void RebuildItems()
  {
    PMCostProjectionByDate current1 = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current;
    if ((current1 != null ? (!current1.ProjectID.HasValue ? 1 : 0) : 1) != 0 || ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current.Status != "H")
      return;
    PMProject documentProject = this.GetDocumentProject(((PXSelectBase<PMCostProjectionByDate>) this.Document).Current);
    if (documentProject == null)
      return;
    this.ClearItems();
    IList<PMCostProjectionByDateLine> reportItems = this.BuildCostProjectionByDateLines();
    this.MergeActuals(reportItems);
    this.MergeChanges(reportItems, documentProject);
    this.MergeCommitments(reportItems, documentProject);
    foreach (PMCostProjectionByDateLine line in (IEnumerable<PMCostProjectionByDateLine>) reportItems)
    {
      this.InitializeLine(line, documentProject);
      this._isRebuilding = true;
      try
      {
        ((PXSelectBase<PMCostProjectionByDateLine>) this.Items).Insert(line);
      }
      finally
      {
        this._isRebuilding = false;
      }
    }
    PMCostProjectionByDate current2 = ((PXSelectBase<PMCostProjectionByDate>) this.Document).Current;
    this.InitializeTotals(current2, documentProject);
    ((PXSelectBase) this.Document).Cache.Update((object) current2);
  }

  public class MultiCurrency : MultiCurrencyGraph<ProjectCostProjectionByDateEntry, PMProject>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();

    protected override string Module => "PM";

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.Items
      };
    }

    protected override MultiCurrencyGraph<ProjectCostProjectionByDateEntry, PMProject>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<ProjectCostProjectionByDateEntry, PMProject>.CurySourceMapping(typeof (PMProject));
    }

    protected override MultiCurrencyGraph<ProjectCostProjectionByDateEntry, PMProject>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ProjectCostProjectionByDateEntry, PMProject>.DocumentMapping(typeof (PMProject))
      {
        CuryInfoID = typeof (PMProject.curyInfoID)
      };
    }
  }

  /// <exclude />
  [PXHidden]
  public sealed class GroupingPlaceholderA : 
    BqlPlaceholder.Named<ProjectCostProjectionByDateEntry.GroupingPlaceholderA>
  {
  }

  /// <exclude />
  [PXHidden]
  public sealed class GroupingPlaceholderB : 
    BqlPlaceholder.Named<ProjectCostProjectionByDateEntry.GroupingPlaceholderB>
  {
  }

  /// <exclude />
  [PXHidden]
  public sealed class GroupingPlaceholderC : 
    BqlPlaceholder.Named<ProjectCostProjectionByDateEntry.GroupingPlaceholderC>
  {
  }

  /// <exclude />
  [PXHidden]
  public sealed class GroupingPlaceholderD : 
    BqlPlaceholder.Named<ProjectCostProjectionByDateEntry.GroupingPlaceholderD>
  {
  }

  public class ProjectCostBudgetQueryTemplate : 
    SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMBudget.projectID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>>.And<
    #nullable disable
    BqlOperand<
    #nullable enable
    PMBudget.type, IBqlString>.IsEqual<
    #nullable disable
    AccountType.expense>>>.Aggregate<To<GroupBy<PMBudget.projectID>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderA>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderB>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderC>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderD>, Sum<PMBudget.curyAmount>, Sum<PMBudget.amount>, Sum<PMBudget.curyRevisedAmount>, Sum<PMBudget.revisedAmount>>>.OrderBy<BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderA, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderB, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderC, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderD, BqlPlaceholder.IBqlAny>.Asc>
  {
  }

  public class ProjectRevenueBudgetQuery : 
    SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMBudget.projectID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>>.And<
    #nullable disable
    BqlOperand<
    #nullable enable
    PMBudget.type, IBqlString>.IsEqual<
    #nullable disable
    AccountType.income>>>.AggregateTo<GroupBy<PMBudget.projectID>, Sum<PMBudget.curyAmount>, Sum<PMBudget.amount>, Sum<PMBudget.curyRevisedAmount>, Sum<PMBudget.revisedAmount>>
  {
  }

  public class ProjectCostTransactionQueryTemplate : 
    SelectFromBase<PMHistoryByDate, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAccountGroup>.On<PMHistoryByDate.FK.AccountGroup>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.projectID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, 
    #nullable disable
    And<BqlOperand<
    #nullable enable
    PMAccountGroup.type, IBqlString>.IsEqual<
    #nullable disable
    AccountType.expense>>>>.And<BqlOperand<
    #nullable enable
    PMHistoryByDate.date, IBqlDateTime>.IsLessEqual<
    #nullable disable
    BqlField<
    #nullable enable
    PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>.Aggregate<
    #nullable disable
    To<GroupBy<PMHistoryByDate.projectID>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderA>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderB>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderC>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderD>, Sum<PMHistoryByDate.curyActualAmount>, Sum<PMHistoryByDate.actualAmount>>>.OrderBy<BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderA, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderB, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderC, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderD, BqlPlaceholder.IBqlAny>.Asc>
  {
  }

  public class ProjectArTransactionQuery : 
    SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMTran.projectID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, 
    #nullable disable
    And<BqlOperand<
    #nullable enable
    PMTran.tranType, IBqlString>.IsEqual<
    #nullable disable
    BatchModule.moduleAR>>>>.And<BqlOperand<
    #nullable enable
    PMTran.date, IBqlDateTime>.IsLessEqual<
    #nullable disable
    BqlField<
    #nullable enable
    PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>.AggregateTo<
    #nullable disable
    GroupBy<PMTran.projectID>, Sum<PMTran.projectCuryAmount>, Sum<PMTran.amount>>
  {
  }

  public class ProjectCostChangeOrderQueryTemplate : 
    SelectFromBase<PMChangeOrderBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<
    #nullable enable
    PMChangeOrder.refNbr, IBqlString>.IsEqual<
    #nullable disable
    PMChangeOrderBudget.refNbr>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMChangeOrderBudget.projectID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, 
    #nullable disable
    And<BqlOperand<
    #nullable enable
    PMChangeOrderBudget.type, IBqlString>.IsEqual<
    #nullable disable
    AccountType.expense>>>, And<BqlOperand<
    #nullable enable
    PMChangeOrder.released, IBqlBool>.IsEqual<
    #nullable disable
    True>>>>.And<BqlOperand<
    #nullable enable
    PMChangeOrder.date, IBqlDateTime>.IsLessEqual<
    #nullable disable
    BqlField<
    #nullable enable
    PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>.Aggregate<
    #nullable disable
    To<GroupBy<PMChangeOrderBudget.projectID>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderA>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderB>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderC>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderD>, Sum<PMChangeOrderBudget.amount>>>.OrderBy<BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderA, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderB, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderC, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderD, BqlPlaceholder.IBqlAny>.Asc>
  {
  }

  public class ProjectPendingCostChangeOrderQueryTemplate : 
    SelectFromBase<PMChangeOrderBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<
    #nullable enable
    PMChangeOrder.refNbr, IBqlString>.IsEqual<
    #nullable disable
    PMChangeOrderBudget.refNbr>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMChangeOrderBudget.projectID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, 
    #nullable disable
    And<BqlOperand<
    #nullable enable
    PMChangeOrderBudget.type, IBqlString>.IsEqual<
    #nullable disable
    AccountType.expense>>>, And<BqlOperand<
    #nullable enable
    PMChangeOrder.released, IBqlBool>.IsEqual<
    #nullable disable
    False>>>, And<BqlOperand<
    #nullable enable
    PMChangeOrder.status, IBqlString>.IsIn<
    #nullable disable
    ChangeOrderStatus.onHold, ChangeOrderStatus.open, ChangeOrderStatus.pendingApproval>>>>.And<BqlOperand<
    #nullable enable
    PMChangeOrder.date, IBqlDateTime>.IsLessEqual<
    #nullable disable
    BqlField<
    #nullable enable
    PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>.Aggregate<
    #nullable disable
    To<GroupBy<PMChangeOrderBudget.projectID>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderA>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderB>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderC>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderD>, Sum<PMChangeOrderBudget.amount>>>.OrderBy<BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderA, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderB, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderC, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderD, BqlPlaceholder.IBqlAny>.Asc>
  {
  }

  public class ProjectRevenueChangeOrderQuery : 
    SelectFromBase<PMChangeOrderBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<
    #nullable enable
    PMChangeOrder.refNbr, IBqlString>.IsEqual<
    #nullable disable
    PMChangeOrderBudget.refNbr>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMChangeOrderBudget.projectID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, 
    #nullable disable
    And<BqlOperand<
    #nullable enable
    PMChangeOrderBudget.type, IBqlString>.IsEqual<
    #nullable disable
    AccountType.income>>>, And<BqlOperand<
    #nullable enable
    PMChangeOrder.released, IBqlBool>.IsEqual<
    #nullable disable
    True>>>>.And<BqlOperand<
    #nullable enable
    PMChangeOrder.date, IBqlDateTime>.IsLessEqual<
    #nullable disable
    BqlField<
    #nullable enable
    PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>.AggregateTo<
    #nullable disable
    GroupBy<PMChangeOrderBudget.projectID>, Sum<PMChangeOrderBudget.amount>>
  {
  }

  public class ProjectPendingRevenueChangeOrderQuery : 
    SelectFromBase<PMChangeOrderBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<
    #nullable enable
    PMChangeOrder.refNbr, IBqlString>.IsEqual<
    #nullable disable
    PMChangeOrderBudget.refNbr>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMChangeOrderBudget.projectID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, 
    #nullable disable
    And<BqlOperand<
    #nullable enable
    PMChangeOrderBudget.type, IBqlString>.IsEqual<
    #nullable disable
    AccountType.income>>>, And<BqlOperand<
    #nullable enable
    PMChangeOrder.released, IBqlBool>.IsEqual<
    #nullable disable
    False>>>, And<BqlOperand<
    #nullable enable
    PMChangeOrder.status, IBqlString>.IsIn<
    #nullable disable
    ChangeOrderStatus.onHold, ChangeOrderStatus.open, ChangeOrderStatus.pendingApproval>>>>.And<BqlOperand<
    #nullable enable
    PMChangeOrder.date, IBqlDateTime>.IsLessEqual<
    #nullable disable
    BqlField<
    #nullable enable
    PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>.AggregateTo<
    #nullable disable
    GroupBy<PMChangeOrderBudget.projectID>, Sum<PMChangeOrderBudget.amount>>
  {
  }

  public class ProjectCommitmentQuery : 
    SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POOrder>.On<PX.Objects.PO.POLine.FK.Order>>, FbqlJoins.Left<PX.Objects.AP.APTran>.On<PX.Objects.AP.APTran.FK.POLine>>, FbqlJoins.Left<PMTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMTran.origTranType, 
    #nullable disable
    Equal<PX.Objects.AP.APTran.tranType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMTran.origRefNbr, 
    #nullable disable
    Equal<PX.Objects.AP.APTran.refNbr>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMTran.origLineNbr, 
    #nullable disable
    Equal<PX.Objects.AP.APTran.lineNbr>>>>>.And<BqlOperand<
    #nullable enable
    PMTran.date, IBqlDateTime>.IsLessEqual<
    #nullable disable
    BqlField<
    #nullable enable
    PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>>>.Where<
    #nullable disable
    BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PX.Objects.PO.POOrder.orderDate, 
    #nullable disable
    LessEqual<BqlField<
    #nullable enable
    PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<
    #nullable disable
    BqlOperand<
    #nullable enable
    PX.Objects.PO.POLine.projectID, IBqlInt>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>.OrderBy<
    #nullable disable
    BqlField<
    #nullable enable
    PX.Objects.PO.POLine.orderType, IBqlString>.Asc, 
    #nullable disable
    BqlField<
    #nullable enable
    PX.Objects.PO.POLine.orderNbr, IBqlString>.Asc, 
    #nullable disable
    BqlField<
    #nullable enable
    PX.Objects.PO.POLine.lineNbr, IBqlInt>.Asc>
  {
  }

  public class ProjectPendingCommitmentChangeOrderQueryTemplate : 
    SelectFromBase<
    #nullable disable
    PMChangeOrderLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<
    #nullable enable
    PMChangeOrder.refNbr, IBqlString>.IsEqual<
    #nullable disable
    PMChangeOrderLine.refNbr>>>, FbqlJoins.Inner<PX.Objects.GL.Account>.On<BqlOperand<
    #nullable enable
    PX.Objects.GL.Account.accountID, IBqlInt>.IsEqual<
    #nullable disable
    PMChangeOrderLine.accountID>>>, FbqlJoins.Inner<PMAccountGroup>.On<BqlOperand<
    #nullable enable
    PMAccountGroup.groupID, IBqlInt>.IsEqual<
    #nullable disable
    PX.Objects.GL.Account.accountGroupID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMChangeOrderLine.projectID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMCostProjectionByDate.projectID, IBqlInt>.FromCurrent>>>>, 
    #nullable disable
    And<BqlOperand<
    #nullable enable
    PMChangeOrder.released, IBqlBool>.IsEqual<
    #nullable disable
    False>>>, And<BqlOperand<
    #nullable enable
    PMChangeOrder.status, IBqlString>.IsIn<
    #nullable disable
    ChangeOrderStatus.onHold, ChangeOrderStatus.open, ChangeOrderStatus.pendingApproval>>>>.And<BqlOperand<
    #nullable enable
    PMChangeOrder.date, IBqlDateTime>.IsLessEqual<
    #nullable disable
    BqlField<
    #nullable enable
    PMCostProjectionByDate.projectionDate, IBqlDateTime>.FromCurrent>>>.Aggregate<
    #nullable disable
    To<GroupBy<PMChangeOrderLine.projectID>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderA>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderB>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderC>, GroupBy<ProjectCostProjectionByDateEntry.GroupingPlaceholderD>, Sum<PMChangeOrderLine.amountInProjectCury>>>.OrderBy<BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderA, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderB, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderC, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectCostProjectionByDateEntry.GroupingPlaceholderD, BqlPlaceholder.IBqlAny>.Asc>
  {
  }

  /// <exclude />
  [PXCacheName("Cost Projection By Date To Copy")]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class SelectedCostProjection : PMCostProjectionByDate
  {
    public new abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProjectCostProjectionByDateEntry.SelectedCostProjection.projectID>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectCostProjectionByDateEntry.SelectedCostProjection.refNbr>
    {
    }

    public new abstract class groupByAccountGroupID : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ProjectCostProjectionByDateEntry.SelectedCostProjection.groupByAccountGroupID>
    {
    }

    public new abstract class groupByProjectTaskID : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ProjectCostProjectionByDateEntry.SelectedCostProjection.groupByProjectTaskID>
    {
    }

    public new abstract class groupByInventoryID : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ProjectCostProjectionByDateEntry.SelectedCostProjection.groupByInventoryID>
    {
    }

    public new abstract class groupByCostCodeID : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ProjectCostProjectionByDateEntry.SelectedCostProjection.groupByCostCodeID>
    {
    }

    public new abstract class includePendingChangeOrders : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ProjectCostProjectionByDateEntry.SelectedCostProjection.includePendingChangeOrders>
    {
    }

    public new abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectCostProjectionByDateEntry.SelectedCostProjection.description>
    {
    }
  }
}
