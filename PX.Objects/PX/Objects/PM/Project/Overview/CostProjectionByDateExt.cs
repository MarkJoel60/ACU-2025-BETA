// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.CostProjectionByDateExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PM.Project.Overview;

/// <summary>
/// The 'Cost Projections' section on the PM301500 screen.
/// </summary>
public class CostProjectionByDateExt : ProjectOverviewExtension
{
  [PXViewName("Cost Projection By Date")]
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Empty>, PMCostProjectionByDate>.View ActualCostProjection;
  [PXViewName("Cost Projection Detail")]
  public FbqlSelect<SelectFromBase<PMCostProjectionByDateLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMCostProjectionByDateLine.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMCostProjectionByDate.refNbr, IBqlString>.FromCurrent>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  PMCostProjectionByDateLine.lineNbr, IBqlInt>.Asc>>, 
  #nullable disable
  PMCostProjectionByDateLine>.View ActualCostProjectionItems;
  public PXAction<PMCostProjectionByDate> viewCostProjectionCostCommitments;
  public PXAction<PMCostProjectionByDate> viewCostProjectionCostTransactions;
  public PXAction<PMCostProjectionByDate> runProjectCostAnalysis;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public virtual IEnumerable actualCostProjection()
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    CostProjectionByDateExt projectionByDateExt = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) projectionByDateExt.Base.GetActualCostProjectionByDate();
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  public virtual IEnumerable actualCostProjectionItems()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = false,
      IsResultTruncated = false,
      IsResultSorted = false
    };
    if (((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current == null)
      return (IEnumerable) pxDelegateResult;
    PMCostProjectionByDateLine[] array = GraphHelper.RowCast<PMCostProjectionByDateLine>((IEnumerable) PXSelectBase<PMCostProjectionByDateLine, PXViewOf<PMCostProjectionByDateLine>.BasedOn<SelectFromBase<PMCostProjectionByDateLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMCostProjectionByDateLine.refNbr, IBqlString>.IsEqual<BqlField<PMCostProjectionByDate.refNbr, IBqlString>.FromCurrent>>.Order<By<BqlField<PMCostProjectionByDateLine.lineNbr, IBqlInt>.Asc>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).ToArray<PMCostProjectionByDateLine>();
    if (array.Length > 1)
    {
      PMCostProjectionByDate current = ((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current;
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
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) array);
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual void _(Events.RowSelected<PMCostProjectionByDate> e)
  {
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMCostProjectionByDate>>) e).Cache.AllowInsert = false;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMCostProjectionByDate>>) e).Cache.AllowUpdate = false;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMCostProjectionByDate>>) e).Cache.AllowDelete = false;
  }

  protected virtual void _(Events.RowSelected<PMCostProjectionByDateLine> e)
  {
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMCostProjectionByDateLine>>) e).Cache.AllowInsert = false;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMCostProjectionByDateLine>>) e).Cache.AllowUpdate = false;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMCostProjectionByDateLine>>) e).Cache.AllowDelete = false;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable ViewCostProjectionCostCommitments(PXAdapter adapter)
  {
    CommitmentInquiry instance = PXGraph.CreateInstance<CommitmentInquiry>();
    ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current.ProjectID;
    ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectTaskID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current?.TaskID;
    ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.AccountGroupID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current?.AccountGroupID;
    ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.InventoryID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current?.InventoryID;
    ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.CostCode = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current?.CostCodeID;
    ProjectAccountingService.NavigateToView((PXGraph) instance, "Items", "View Commitments", DataViewHelper.DataViewFilter.Create("POOrder__OrderDate", (PXCondition) 5, (object) ((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current.ProjectionDate));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ViewCostProjectionCostTransactions(PXAdapter adapter)
  {
    TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
    ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current.ProjectID;
    ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectTaskID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current?.TaskID;
    ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.AccountGroupID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current?.AccountGroupID;
    TransactionInquiry.TranFilter current1 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current;
    int? nullable1 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.AccountGroupID;
    string str = !nullable1.HasValue ? "E" : (string) null;
    current1.AccountGroupType = str;
    TransactionInquiry.TranFilter current2 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current;
    PMCostProjectionByDateLine current3 = ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current;
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
    PMCostProjectionByDateLine current5 = ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current;
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
    ProjectAccountingService.NavigateToView((PXGraph) instance, "Transactions", "View Transactions", DataViewHelper.DataViewFilter.Create("Date", (PXCondition) 5, (object) ((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current.ProjectionDate));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable RunProjectCostAnalysis(PXAdapter adapter)
  {
    ProjectDateSensitiveCostsInquiry instance = PXGraph.CreateInstance<ProjectDateSensitiveCostsInquiry>();
    PMDateSensitiveDataRevision current = ((PXSelectBase<PMDateSensitiveDataRevision>) instance.Revision).Current;
    current.ProjectID = ((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current.ProjectID;
    current.ProjectTaskID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current?.TaskID;
    current.AccountGroupID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current?.AccountGroupID;
    current.AccountGroups = "E";
    current.InventoryID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current?.InventoryID;
    current.CostCodeID = (int?) ((PXSelectBase<PMCostProjectionByDateLine>) this.ActualCostProjectionItems).Current?.CostCodeID;
    current.EndDate = ((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current.ProjectionDate;
    current.GroupByAccountGroupID = ((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current.GroupByAccountGroupID;
    current.GroupByProjectTaskID = ((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current.GroupByProjectTaskID;
    current.GroupByInventoryID = ((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current.GroupByInventoryID;
    current.GroupByCostCodeID = ((PXSelectBase<PMCostProjectionByDate>) this.ActualCostProjection).Current.GroupByCostCodeID;
    ((PXSelectBase<PMDateSensitiveDataRevision>) instance.Revision).Insert(current);
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }
}
