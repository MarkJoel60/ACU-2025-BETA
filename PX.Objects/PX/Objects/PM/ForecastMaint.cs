// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ForecastMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

public class ForecastMaint : PXGraph<
#nullable disable
ForecastMaint, PMForecast>, PXImportAttribute.IPXPrepareItems
{
  public FbqlSelect<SelectFromBase<PMForecast, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProject>.On<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  PMForecast.projectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.contractID, 
  #nullable disable
  IsNull>>>>.Or<MatchUserFor<PMProject>>>, PMForecast>.View Revisions;
  public PXFilter<ForecastMaint.PMForecastFilter> Filter;
  [PXImport(typeof (PMForecast))]
  [PXVirtualDAC]
  public PXSelect<PMForecastRecord> Items;
  public PXFilter<ForecastMaint.PMForecastAddPeriodDialogInfo> AddPeriodDialog;
  public PXFilter<ForecastMaint.PMForecastCopyDialogInfo> CopyDialog;
  public PXFilter<ForecastMaint.PMForecastDistributeDialogInfo> DistributeDialog;
  public PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMForecast.projectID>>>> Project;
  public PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMForecast.projectID>>>> Tasks;
  public PXSelect<PMBudgetInfo, Where<PMBudgetInfo.projectID, Equal<Current<PMForecast.projectID>>, And2<Where<Current<ForecastMaint.PMForecastFilter.projectTaskID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.projectTaskID>, Equal<PMBudgetInfo.projectTaskID>>>, And2<Where<Current<ForecastMaint.PMForecastFilter.accountGroupType>, Equal<PMAccountType.all>, Or<Current<ForecastMaint.PMForecastFilter.accountGroupType>, Equal<PMBudgetInfo.accountGroupType>, Or<Where<Current<ForecastMaint.PMForecastFilter.accountGroupType>, Equal<AccountType.expense>, And<PMBudgetInfo.isExpense, Equal<True>>>>>>, And2<Where<Current<ForecastMaint.PMForecastFilter.accountGroupID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.accountGroupID>, Equal<PMBudgetInfo.accountGroupID>>>, And2<Where<Current<ForecastMaint.PMForecastFilter.inventoryID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.inventoryID>, Equal<PMBudgetInfo.inventoryID>>>, And2<Where<Current<ForecastMaint.PMForecastFilter.costCodeID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.costCodeID>, Equal<PMBudgetInfo.costCodeID>>>, And<Where<Current<ForecastMaint.PMForecastFilter.accountGroupID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.accountGroupID>, Equal<PMBudgetInfo.accountGroupID>>>>>>>>>>> Budgets;
  public PXSelect<PMForecastHistoryInfo, Where<PMForecastHistory.projectID, Equal<Current<PMForecast.projectID>>, And2<Where<Current<PMForecastHistory.projectTaskID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.projectTaskID>, Equal<PMForecastHistory.projectTaskID>>>, And2<Where<Current<ForecastMaint.PMForecastFilter.accountGroupType>, Equal<PMAccountType.all>, Or<Current<ForecastMaint.PMForecastFilter.accountGroupType>, Equal<PMForecastHistoryInfo.accountGroupType>, Or<Where<Current<ForecastMaint.PMForecastFilter.accountGroupType>, Equal<AccountType.expense>, And<PMForecastHistoryInfo.isExpense, Equal<True>>>>>>, And2<Where<Current<PMForecastHistory.accountGroupID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.accountGroupID>, Equal<PMForecastHistory.accountGroupID>>>, And2<Where<Current<PMForecastHistory.inventoryID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.inventoryID>, Equal<PMForecastHistory.inventoryID>>>, And2<Where<Current<PMForecastHistory.costCodeID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.costCodeID>, Equal<PMForecastHistory.costCodeID>>>, And<Where<Current<PMForecastHistory.accountGroupID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.accountGroupID>, Equal<PMForecastHistory.accountGroupID>>>>>>>>>>> Actuals;
  public PXSelect<PMForecastDetailInfo, Where<PMForecastDetailInfo.projectID, Equal<Current<PMForecast.projectID>>, And<PMForecastDetailInfo.revisionID, Equal<Current<PMForecast.revisionID>>, And2<Where<Current<ForecastMaint.PMForecastFilter.projectTaskID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.projectTaskID>, Equal<PMForecastDetailInfo.projectTaskID>>>, And2<Where<Current<ForecastMaint.PMForecastFilter.accountGroupType>, Equal<PMAccountType.all>, Or<Current<ForecastMaint.PMForecastFilter.accountGroupType>, Equal<PMForecastDetailInfo.accountGroupType>, Or<Where<Current<ForecastMaint.PMForecastFilter.accountGroupType>, Equal<AccountType.expense>, And<PMForecastDetailInfo.isExpense, Equal<True>>>>>>, And2<Where<Current<ForecastMaint.PMForecastFilter.accountGroupID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.accountGroupID>, Equal<PMForecastDetail.accountGroupID>>>, And2<Where<Current<ForecastMaint.PMForecastFilter.inventoryID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.inventoryID>, Equal<PMForecastDetail.inventoryID>>>, And2<Where<Current<ForecastMaint.PMForecastFilter.costCodeID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.costCodeID>, Equal<PMForecastDetail.costCodeID>>>, And<Where<Current<ForecastMaint.PMForecastFilter.accountGroupID>, IsNull, Or<Current<ForecastMaint.PMForecastFilter.accountGroupID>, Equal<PMForecastDetail.accountGroupID>>>>>>>>>>>> Details;
  public Dictionary<int, PMTask> TaskLookup;
  public Dictionary<BudgetKeyTuple, PMBudget> BudgetLookup;
  protected IFinPeriodRepository finPeriodsRepo;
  public PXAction<PMForecast> addPeriods;
  public PXAction<PMForecast> settleBalances;
  public PXAction<PMForecast> addMissingLines;
  public PXAction<PMForecast> generatePeriods;
  public PXAction<PMForecast> copyRevision;
  public PXAction<PMForecast> distribute;
  private string projectTaskID;
  private string accountGroupID;
  private string inventoryID;
  private string costCodeID;

  public virtual IFinPeriodRepository FinPeriodRepository
  {
    get
    {
      if (this.finPeriodsRepo == null)
        this.finPeriodsRepo = (IFinPeriodRepository) new PX.Objects.GL.FinPeriods.FinPeriodRepository((PXGraph) this);
      return this.finPeriodsRepo;
    }
  }

  public ForecastMaint() => ((PXAction) this.CopyPaste).SetVisible(false);

  public IEnumerable items()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultFiltered = true;
    pxDelegateResult.IsResultSorted = true;
    pxDelegateResult.IsResultTruncated = true;
    if (((PXSelectBase<PMForecast>) this.Revisions).Current != null && ((PXSelectBase<PMForecast>) this.Revisions).Current.ProjectID.HasValue && ((PXSelectBase<PMForecast>) this.Revisions).Current.RevisionID != null)
    {
      if (PXView.Searches != null && PXView.Searches.Length == 6 && PXView.MaximumRows == 1 && PXView.Searches[0] != null)
      {
        PMForecastRecord pmForecastRecord = this.SelectSingleRecord();
        if (pmForecastRecord != null)
          ((List<object>) pxDelegateResult).Add((object) pmForecastRecord);
      }
      else
      {
        pxDelegateResult.IsResultTruncated = PXView.MaximumRows != 1;
        List<PMForecastRecord> pmForecastRecordList = new List<PMForecastRecord>(200);
        foreach (ForecastMaint.Summary summary in this.GetSummaries())
          pmForecastRecordList.AddRange((IEnumerable<PMForecastRecord>) summary.GetList());
        foreach (PMForecastRecord pmForecastRecord in pmForecastRecordList)
        {
          if (((PXSelectBase) this.Items).Cache.Locate((object) pmForecastRecord) == null)
            GraphHelper.Hold(((PXSelectBase) this.Items).Cache, (object) pmForecastRecord);
        }
        int index = PXView.StartRow;
        if (PXView.ReverseOrder)
          index = Math.Max(0, pmForecastRecordList.Count + PXView.StartRow);
        int num = pmForecastRecordList.Count - index;
        if (PXView.MaximumRows != 0 && pxDelegateResult.IsResultTruncated)
          num = Math.Min(PXView.MaximumRows, num);
        ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) pmForecastRecordList.GetRange(index, num));
      }
    }
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual List<ForecastMaint.Summary> GetSummaries()
  {
    string[] strArray1 = (string[]) PXView.SortColumns.Clone();
    for (int index = 0; index < strArray1.Length; ++index)
    {
      if (strArray1[index] == "ProjectTask")
        strArray1[index] = "ProjectTaskID";
    }
    PXView pxView = new PXView((PXGraph) this, false, ((PXSelectBase) this.Budgets).View.BqlSelect);
    int num1 = 0;
    int num2 = 0;
    string[] strArray2 = strArray1;
    bool[] descendings = PXView.Descendings;
    ref int local1 = ref num2;
    ref int local2 = ref num1;
    List<object> objectList = pxView.Select((object[]) null, (object[]) null, (object[]) null, strArray2, descendings, (PXFilterRow[]) null, ref local1, 0, ref local2);
    Dictionary<BudgetKeyTuple, ForecastMaint.Summary> dictionary = new Dictionary<BudgetKeyTuple, ForecastMaint.Summary>();
    List<ForecastMaint.Summary> summaries = new List<ForecastMaint.Summary>(objectList.Count);
    foreach (PMBudgetInfo budget in objectList)
    {
      ForecastMaint.Summary summary = new ForecastMaint.Summary(budget);
      summaries.Add(summary);
      dictionary.Add(BudgetKeyTuple.Create((IProjectFilter) budget), summary);
    }
    foreach (PXResult<PMForecastDetailInfo> pxResult in ((PXSelectBase<PMForecastDetailInfo>) this.Details).Select(Array.Empty<object>()))
    {
      PMForecastDetailInfo forecastDetailInfo = PXResult<PMForecastDetailInfo>.op_Implicit(pxResult);
      ForecastMaint.Summary summary = (ForecastMaint.Summary) null;
      if (dictionary.TryGetValue(BudgetKeyTuple.Create((IProjectFilter) forecastDetailInfo), out summary))
        summary.Add(forecastDetailInfo);
    }
    foreach (PXResult<PMForecastHistoryInfo> pxResult in ((PXSelectBase<PMForecastHistoryInfo>) this.Actuals).Select(Array.Empty<object>()))
    {
      PMForecastHistoryInfo forecastHistoryInfo = PXResult<PMForecastHistoryInfo>.op_Implicit(pxResult);
      ForecastMaint.Summary summary = (ForecastMaint.Summary) null;
      if (dictionary.TryGetValue(BudgetKeyTuple.Create((IProjectFilter) forecastHistoryInfo), out summary))
        summary.Add(forecastHistoryInfo);
    }
    return summaries;
  }

  protected virtual PMForecastRecord SelectSingleRecord()
  {
    PMBudgetInfo pmBudgetInfo = PXResultset<PMBudgetInfo>.op_Implicit(((PXSelectBase<PMBudgetInfo>) this.Budgets).Search<PMBudgetInfo.projectID, PMBudgetInfo.projectTaskID, PMBudgetInfo.accountGroupID, PMBudgetInfo.inventoryID, PMBudgetInfo.costCodeID>(PXView.Searches[0], PXView.Searches[1], PXView.Searches[2], PXView.Searches[3], PXView.Searches[4], Array.Empty<object>()));
    string search = (string) PXView.Searches[5];
    if (search == "000000")
    {
      if (pmBudgetInfo != null)
        return ForecastMaint.Summary.CreateSummaryRecord(pmBudgetInfo);
    }
    else if (pmBudgetInfo != null && search != "999998" && search != "999999")
    {
      PMForecastDetailInfo detail = PXResultset<PMForecastDetailInfo>.op_Implicit(((PXSelectBase<PMForecastDetailInfo>) new PXSelect<PMForecastDetailInfo, Where<PMForecastDetailInfo.projectID, Equal<Current<PMForecast.projectID>>, And<PMForecastDetailInfo.revisionID, Equal<Current<PMForecast.revisionID>>, And<PMForecastDetailInfo.projectTaskID, Equal<Required<PMForecastDetailInfo.projectTaskID>>, And<PMForecastDetail.accountGroupID, Equal<Required<PMForecastDetail.accountGroupID>>, And<PMForecastDetail.inventoryID, Equal<Required<PMForecastDetail.inventoryID>>, And<PMForecastDetail.costCodeID, Equal<Required<PMForecastDetail.costCodeID>>, And<PMForecastDetail.periodID, Equal<Required<PMForecastDetail.periodID>>>>>>>>>>((PXGraph) this)).Select(new object[5]
      {
        (object) pmBudgetInfo.ProjectTaskID,
        (object) pmBudgetInfo.AccountGroupID,
        (object) pmBudgetInfo.InventoryID,
        (object) pmBudgetInfo.CostCodeID,
        (object) search
      }));
      PMForecastHistoryInfo history = PXResultset<PMForecastHistoryInfo>.op_Implicit(((PXSelectBase<PMForecastHistoryInfo>) new PXSelect<PMForecastHistoryInfo, Where<PMForecastHistory.projectID, Equal<Current<PMForecast.projectID>>, And<PMForecastHistory.projectTaskID, Equal<Required<PMForecastDetailInfo.projectTaskID>>, And<PMForecastHistory.accountGroupID, Equal<Required<PMForecastDetail.accountGroupID>>, And<PMForecastHistory.inventoryID, Equal<Required<PMForecastDetail.inventoryID>>, And<PMForecastHistory.costCodeID, Equal<Required<PMForecastDetail.costCodeID>>, And<PMForecastHistory.periodID, Equal<Required<PMForecastDetail.periodID>>>>>>>>>((PXGraph) this)).Select(new object[5]
      {
        (object) pmBudgetInfo.ProjectTaskID,
        (object) pmBudgetInfo.AccountGroupID,
        (object) pmBudgetInfo.InventoryID,
        (object) pmBudgetInfo.CostCodeID,
        (object) search
      }));
      if (detail != null)
        return ForecastMaint.Summary.CreateDetailRecord((PMForecastDetail) detail, pmBudgetInfo, history);
    }
    else
    {
      if (pmBudgetInfo != null && search == "999999")
      {
        ForecastMaint.Summary summaryFromBudget = this.GetSummaryFromBudget(pmBudgetInfo);
        return ForecastMaint.Summary.CreateDifferenceRecord(pmBudgetInfo, summaryFromBudget.GetTotalRecord());
      }
      if (pmBudgetInfo != null && search == "999998")
        return this.GetSummaryFromBudget(pmBudgetInfo).GetTotalRecord();
    }
    return (PMForecastRecord) null;
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project Currency", IsReadOnly = true, FieldClass = "ProjectMultiCurrency")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.curyID> e)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable AddPeriods(PXAdapter adapter)
  {
    if (((PXGraph) this).IsImport)
      this.TryGeneratePeriodsForBudgetFromBudgetInfo(this.GetBudgetInfoFromKey(new BudgetKeyTuple(((PXSelectBase<PMForecast>) this.Revisions).Current.ProjectID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.ProjectTaskID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.AccountGroupID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.InventoryID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.CostCodeID.GetValueOrDefault())));
    else if (((PXSelectBase<PMForecastRecord>) this.Items).Current != null)
    {
      if (((PXSelectBase) this.AddPeriodDialog).View.Answer == null)
      {
        ((PXSelectBase) this.AddPeriodDialog).Cache.Clear();
        ((PXSelectBase) this.AddPeriodDialog).Cache.Insert();
      }
      if (((PXSelectBase<ForecastMaint.PMForecastAddPeriodDialogInfo>) this.AddPeriodDialog).AskExt() != 1)
        return adapter.Get();
      this.TryGeneratePeriodsForBudgetFromBudgetInfo(this.GetBudgetInfoFromKey(BudgetKeyTuple.Create((IProjectFilter) ((PXSelectBase<PMForecastRecord>) this.Items).Current)));
    }
    return adapter.Get();
  }

  private void TryGeneratePeriodsForBudgetFromBudgetInfo(PMBudgetInfo budget)
  {
    if (budget == null)
      return;
    string startPeriodID = ((PXSelectBase<ForecastMaint.PMForecastAddPeriodDialogInfo>) this.AddPeriodDialog).Current.StartPeriodID;
    string endPeriodID = ((PXSelectBase<ForecastMaint.PMForecastAddPeriodDialogInfo>) this.AddPeriodDialog).Current.EndPeriodID;
    if (startPeriodID == null)
      startPeriodID = this.GetMinStartPeriodID(budget);
    if (endPeriodID == null)
      endPeriodID = this.GetMaxEndPeriodID(budget) ?? startPeriodID;
    this.GeneratePeriodsForBudget(budget, startPeriodID, endPeriodID);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable SettleBalances(PXAdapter adapter)
  {
    PMBudgetInfo row = (PMBudgetInfo) null;
    if (((PXGraph) this).IsImport)
      row = this.GetBudgetInfoFromKey(new BudgetKeyTuple(((PXSelectBase<PMForecast>) this.Revisions).Current.ProjectID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.ProjectTaskID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.AccountGroupID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.InventoryID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.CostCodeID.GetValueOrDefault()));
    else if (((PXSelectBase<PMForecastRecord>) this.Items).Current != null)
      row = this.GetBudgetInfoFromKey(BudgetKeyTuple.Create((IProjectFilter) ((PXSelectBase<PMForecastRecord>) this.Items).Current));
    if (row != null)
      this.SettleBalancesProc(row);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable AddMissingLines(PXAdapter adapter)
  {
    PMBudgetInfo row = (PMBudgetInfo) null;
    if (((PXGraph) this).IsImport)
      row = this.GetBudgetInfoFromKey(new BudgetKeyTuple(((PXSelectBase<PMForecast>) this.Revisions).Current.ProjectID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.ProjectTaskID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.AccountGroupID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.InventoryID.GetValueOrDefault(), ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.CostCodeID.GetValueOrDefault()));
    else if (((PXSelectBase<PMForecastRecord>) this.Items).Current != null)
      row = this.GetBudgetInfoFromKey(BudgetKeyTuple.Create((IProjectFilter) ((PXSelectBase<PMForecastRecord>) this.Items).Current));
    if (row != null)
      this.AddMissingLinesProc(row);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Budget Forecasting", DisplayOnMainToolbar = true)]
  public virtual IEnumerable GeneratePeriods(PXAdapter adapter)
  {
    foreach (PXResult<PMBudgetInfo> pxResult in ((PXSelectBase<PMBudgetInfo>) this.Budgets).Select(Array.Empty<object>()))
    {
      PMBudgetInfo row = PXResult<PMBudgetInfo>.op_Implicit(pxResult);
      string str1 = this.GetMinStartPeriodID(row);
      string str2 = this.GetMaxEndPeriodID(row) ?? str1;
      if (int.Parse(str2) < int.Parse(str1))
        str1 = str2;
      this.GeneratePeriodsForBudget(row, str1, str2);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Other", DisplayOnMainToolbar = true)]
  public virtual IEnumerable CopyRevision(PXAdapter adapter)
  {
    ((PXAction) this.Save).Press();
    if (((PXSelectBase) this.CopyDialog).View.Answer == null)
    {
      ((PXSelectBase) this.CopyDialog).Cache.Clear();
      ((PXSelectBase) this.CopyDialog).Cache.Insert();
    }
    if (((PXSelectBase<ForecastMaint.PMForecastCopyDialogInfo>) this.CopyDialog).AskExt() != 1 || string.IsNullOrEmpty(((PXSelectBase<ForecastMaint.PMForecastCopyDialogInfo>) this.CopyDialog).Current.RevisionID))
      return adapter.Get();
    return (IEnumerable) new PMForecast[1]
    {
      this.CreateNewRevision(((PXSelectBase<ForecastMaint.PMForecastCopyDialogInfo>) this.CopyDialog).Current)
    };
  }

  [PXUIField]
  [PXButton(Category = "Budget Forecasting", DisplayOnMainToolbar = true)]
  public virtual IEnumerable Distribute(PXAdapter adapter)
  {
    ((PXAction) this.Save).Press();
    if (((PXSelectBase) this.DistributeDialog).View.Answer == null)
    {
      ((PXSelectBase) this.DistributeDialog).Cache.Clear();
      ((PXSelectBase) this.DistributeDialog).Cache.Insert();
    }
    if (((PXSelectBase<ForecastMaint.PMForecastDistributeDialogInfo>) this.DistributeDialog).AskExt() == 1)
      this.DistributeForecast(((PXSelectBase<ForecastMaint.PMForecastDistributeDialogInfo>) this.DistributeDialog).Current);
    return adapter.Get();
  }

  protected virtual void DistributeForecast(
    ForecastMaint.PMForecastDistributeDialogInfo filterdata)
  {
    if (filterdata.ApplyOption == "0")
    {
      foreach (ForecastMaint.Summary summary in this.GetSummaries())
        this.DistributeForecast(filterdata, summary);
    }
    else
    {
      if (((PXSelectBase<PMForecastRecord>) this.Items).Current == null)
        return;
      PMBudgetInfo budgetInfoFromKey = this.GetBudgetInfoFromKey(BudgetKeyTuple.Create((IProjectFilter) ((PXSelectBase<PMForecastRecord>) this.Items).Current));
      if (budgetInfoFromKey == null)
        return;
      ForecastMaint.Summary summary = new ForecastMaint.Summary(budgetInfoFromKey);
      foreach (PXResult<PMForecastDetailInfo> pxResult in ((PXSelectBase<PMForecastDetailInfo>) new PXSelect<PMForecastDetailInfo, Where<PMForecastDetailInfo.projectID, Equal<Required<PMForecastDetailInfo.projectID>>, And<PMForecastDetailInfo.revisionID, Equal<Current<PMForecast.revisionID>>, And<PMForecastDetailInfo.projectTaskID, Equal<Required<PMForecastDetailInfo.projectTaskID>>, And<PMForecastDetail.accountGroupID, Equal<Required<PMForecastDetail.accountGroupID>>, And<PMForecastDetail.inventoryID, Equal<Required<PMForecastDetail.inventoryID>>, And<PMForecastDetail.costCodeID, Equal<Required<PMForecastDetail.costCodeID>>>>>>>>>((PXGraph) this)).Select(new object[5]
      {
        (object) budgetInfoFromKey.ProjectID,
        (object) budgetInfoFromKey.ProjectTaskID,
        (object) budgetInfoFromKey.AccountGroupID,
        (object) budgetInfoFromKey.InventoryID,
        (object) budgetInfoFromKey.CostCodeID
      }))
      {
        PMForecastDetailInfo detail = PXResult<PMForecastDetailInfo>.op_Implicit(pxResult);
        summary.Add(detail);
      }
      this.DistributeForecast(filterdata, summary);
    }
  }

  protected virtual void DistributeForecast(
    ForecastMaint.PMForecastDistributeDialogInfo filterdata,
    ForecastMaint.Summary summary)
  {
    if (filterdata.ValueOption == "0")
      this.DistributeForecastRedistribution(filterdata, summary);
    if (!(filterdata.ValueOption == "1"))
      return;
    this.DistributeForecastAppendVariance(filterdata, summary);
  }

  private int GetBasePrecision()
  {
    return ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this).BaseDecimalPlaces();
  }

  protected virtual void DistributeForecastRedistribution(
    ForecastMaint.PMForecastDistributeDialogInfo filterdata,
    ForecastMaint.Summary summary)
  {
    if (summary.Details.Count == 0)
      return;
    Decimal? nullable1 = summary.Budget.Qty;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = summary.Budget.RevisedQty;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    nullable1 = summary.Budget.CuryAmount;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = summary.Budget.CuryRevisedAmount;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    nullable1 = summary.Budget.Qty;
    Decimal num1 = nullable1.GetValueOrDefault() / (Decimal) summary.Details.Count;
    nullable1 = summary.Budget.RevisedQty;
    Decimal num2 = nullable1.GetValueOrDefault() / (Decimal) summary.Details.Count;
    nullable1 = summary.Budget.CuryAmount;
    Decimal num3 = nullable1.GetValueOrDefault() / (Decimal) summary.Details.Count;
    nullable1 = summary.Budget.CuryRevisedAmount;
    Decimal num4 = nullable1.GetValueOrDefault() / (Decimal) summary.Details.Count;
    Decimal num5 = PXDBQuantityAttribute.Round(new Decimal?(this.DistibuteRound(num1, summary.Details.Count - 1)));
    Decimal num6 = PXDBQuantityAttribute.Round(new Decimal?(this.DistibuteRound(num2, summary.Details.Count - 1)));
    int basePrecision = this.GetBasePrecision();
    Decimal num7 = Math.Round(this.DistibuteRound(num3, summary.Details.Count - 1), basePrecision);
    Decimal num8 = Math.Round(this.DistibuteRound(num4, summary.Details.Count - 1), basePrecision);
    bool? nullable2;
    for (int index = 0; index < summary.Details.Count - 1; ++index)
    {
      nullable2 = filterdata.Qty;
      if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "0")
        summary.Details[index].Qty = new Decimal?(num5);
      nullable2 = filterdata.RevisedQty;
      if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "0")
        summary.Details[index].RevisedQty = new Decimal?(num6);
      nullable2 = filterdata.Amount;
      if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "0")
        summary.Details[index].CuryAmount = new Decimal?(num7);
      nullable2 = filterdata.RevisedAmount;
      if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "0")
        summary.Details[index].CuryRevisedAmount = new Decimal?(num8);
      ((PXSelectBase<PMForecastDetailInfo>) this.Details).Update(summary.Details[index]);
      valueOrDefault1 -= num5;
      valueOrDefault2 -= num6;
      valueOrDefault3 -= num7;
      valueOrDefault4 -= num8;
    }
    nullable2 = filterdata.Qty;
    if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "0")
      summary.Details[summary.Details.Count - 1].Qty = new Decimal?(valueOrDefault1);
    nullable2 = filterdata.RevisedQty;
    if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "0")
      summary.Details[summary.Details.Count - 1].RevisedQty = new Decimal?(valueOrDefault2);
    nullable2 = filterdata.Amount;
    if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "0")
      summary.Details[summary.Details.Count - 1].CuryAmount = new Decimal?(valueOrDefault3);
    nullable2 = filterdata.RevisedAmount;
    if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "0")
      summary.Details[summary.Details.Count - 1].CuryRevisedAmount = new Decimal?(valueOrDefault4);
    ((PXSelectBase<PMForecastDetailInfo>) this.Details).Update(summary.Details[summary.Details.Count - 1]);
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  protected virtual void DistributeForecastAppendVariance(
    ForecastMaint.PMForecastDistributeDialogInfo filterdata,
    ForecastMaint.Summary summary)
  {
    List<PMForecastRecord> list = summary.GetList();
    PMForecastRecord pmForecastRecord = list[list.Count - 1];
    if (list.Count == 0 || !pmForecastRecord.IsDifference)
      return;
    Decimal valueOrDefault1 = summary.Budget.Qty.GetValueOrDefault();
    Decimal? nullable1 = summary.Budget.RevisedQty;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    nullable1 = summary.Budget.CuryAmount;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = summary.Budget.CuryRevisedAmount;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    bool? nullable2;
    for (int index = 0; index < summary.Details.Count - 1; ++index)
    {
      Decimal num1 = 0M;
      nullable1 = summary.Budget.Qty;
      if (nullable1.GetValueOrDefault() != 0M)
      {
        nullable1 = summary.Details[index].Qty;
        Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
        nullable1 = summary.Details[index].Qty;
        Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
        nullable1 = pmForecastRecord.Qty;
        Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
        Decimal num2 = valueOrDefault6 * valueOrDefault7;
        nullable1 = summary.Budget.Qty;
        Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
        Decimal num3 = num2 / valueOrDefault8;
        num1 = valueOrDefault5 + num3;
      }
      Decimal num4 = 0M;
      nullable1 = summary.Budget.RevisedQty;
      if (nullable1.GetValueOrDefault() != 0M)
      {
        nullable1 = summary.Details[index].RevisedQty;
        Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
        nullable1 = summary.Details[index].RevisedQty;
        Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
        nullable1 = pmForecastRecord.RevisedQty;
        Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
        Decimal num5 = valueOrDefault10 * valueOrDefault11;
        nullable1 = summary.Budget.RevisedQty;
        Decimal valueOrDefault12 = nullable1.GetValueOrDefault();
        Decimal num6 = num5 / valueOrDefault12;
        num4 = valueOrDefault9 + num6;
      }
      Decimal num7 = 0M;
      nullable1 = summary.Budget.CuryAmount;
      if (nullable1.GetValueOrDefault() != 0M)
      {
        nullable1 = summary.Details[index].CuryAmount;
        Decimal valueOrDefault13 = nullable1.GetValueOrDefault();
        nullable1 = summary.Details[index].CuryAmount;
        Decimal valueOrDefault14 = nullable1.GetValueOrDefault();
        nullable1 = pmForecastRecord.CuryAmount;
        Decimal valueOrDefault15 = nullable1.GetValueOrDefault();
        Decimal num8 = valueOrDefault14 * valueOrDefault15;
        nullable1 = summary.Budget.CuryAmount;
        Decimal valueOrDefault16 = nullable1.GetValueOrDefault();
        Decimal num9 = num8 / valueOrDefault16;
        num7 = valueOrDefault13 + num9;
      }
      Decimal num10 = 0M;
      nullable1 = summary.Budget.CuryRevisedAmount;
      if (nullable1.GetValueOrDefault() != 0M)
      {
        nullable1 = summary.Details[index].CuryRevisedAmount;
        Decimal valueOrDefault17 = nullable1.GetValueOrDefault();
        nullable1 = summary.Details[index].CuryRevisedAmount;
        Decimal valueOrDefault18 = nullable1.GetValueOrDefault();
        nullable1 = pmForecastRecord.CuryRevisedAmount;
        Decimal valueOrDefault19 = nullable1.GetValueOrDefault();
        Decimal num11 = valueOrDefault18 * valueOrDefault19;
        nullable1 = summary.Budget.CuryRevisedAmount;
        Decimal valueOrDefault20 = nullable1.GetValueOrDefault();
        Decimal num12 = num11 / valueOrDefault20;
        num10 = valueOrDefault17 + num12;
      }
      Decimal num13 = PXDBQuantityAttribute.Round(new Decimal?(this.DistibuteRound(num1, summary.Details.Count - 1)));
      Decimal num14 = PXDBQuantityAttribute.Round(new Decimal?(this.DistibuteRound(num4, summary.Details.Count - 1)));
      int basePrecision = this.GetBasePrecision();
      Decimal num15 = Math.Round(this.DistibuteRound(num7, summary.Details.Count - 1), basePrecision);
      Decimal num16 = Math.Round(this.DistibuteRound(num10, summary.Details.Count - 1), basePrecision);
      nullable2 = filterdata.Qty;
      if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "1")
        summary.Details[index].Qty = new Decimal?(num13);
      nullable2 = filterdata.RevisedQty;
      if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "1")
        summary.Details[index].RevisedQty = new Decimal?(num14);
      nullable2 = filterdata.Amount;
      if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "1")
        summary.Details[index].CuryAmount = new Decimal?(num15);
      nullable2 = filterdata.RevisedAmount;
      if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "1")
        summary.Details[index].CuryRevisedAmount = new Decimal?(num16);
      ((PXSelectBase<PMForecastDetailInfo>) this.Details).Update(summary.Details[index]);
      valueOrDefault1 -= num13;
      valueOrDefault2 -= num14;
      valueOrDefault3 -= num15;
      valueOrDefault4 -= num16;
    }
    nullable2 = filterdata.Qty;
    if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "1")
      summary.Details[summary.Details.Count - 1].Qty = new Decimal?(valueOrDefault1);
    nullable2 = filterdata.RevisedQty;
    if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "1")
      summary.Details[summary.Details.Count - 1].RevisedQty = new Decimal?(valueOrDefault2);
    nullable2 = filterdata.Amount;
    if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "1")
      summary.Details[summary.Details.Count - 1].CuryAmount = new Decimal?(valueOrDefault3);
    nullable2 = filterdata.RevisedAmount;
    if (nullable2.GetValueOrDefault() && filterdata.ValueOption == "1")
      summary.Details[summary.Details.Count - 1].CuryRevisedAmount = new Decimal?(valueOrDefault4);
    ((PXSelectBase<PMForecastDetailInfo>) this.Details).Update(summary.Details[summary.Details.Count - 1]);
  }

  protected virtual Decimal DistibuteRound(Decimal value, int rowCount)
  {
    int num = (int) Math.Log10((double) value);
    int decimals = (int) Math.Log10((double) rowCount) - num + 1;
    if (rowCount <= 1 || !(value > 0M))
      return value;
    return decimals >= 0 ? Math.Round(value, decimals) : Math.Round(value / (Decimal) Math.Pow(10.0, (double) -decimals)) * (Decimal) Math.Pow(10.0, (double) -decimals);
  }

  protected virtual string GetMinStartPeriodID(PMBudgetInfo row)
  {
    DateTime? date = row.PlannedStartDate;
    string minStartPeriodId = (string) null;
    DateTime? nullable1;
    DateTime? nullable2;
    if (PXAccess.FeatureInstalled<FeaturesSet.changeRequest>())
    {
      DateTime? changeRequestDate = this.GetFirstChangeRequestDate(row.ProjectID, row.ProjectTaskID);
      if (changeRequestDate.HasValue)
      {
        if (!date.HasValue)
        {
          date = changeRequestDate;
        }
        else
        {
          nullable1 = changeRequestDate;
          nullable2 = date;
          if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            date = changeRequestDate;
        }
      }
    }
    PMTran pmTran = PXResultset<PMTran>.op_Implicit(((PXSelectBase<PMTran>) new PXSelect<PMTran, Where<PMTran.projectID, Equal<Required<PMTran.projectID>>, And<PMTran.taskID, Equal<Required<PMTran.taskID>>>>, OrderBy<Asc<PMTran.date>>>((PXGraph) this)).SelectWindowed(0, 1, new object[2]
    {
      (object) row.ProjectID,
      (object) row.ProjectTaskID
    }));
    if (pmTran != null)
    {
      if (!date.HasValue)
      {
        date = pmTran.Date;
        minStartPeriodId = pmTran.FinPeriodID;
      }
      else
      {
        nullable2 = pmTran.Date;
        nullable1 = date;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          date = pmTran.Date;
          minStartPeriodId = pmTran.FinPeriodID;
        }
      }
    }
    else if (!date.HasValue)
    {
      PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.ProjectTaskID);
      if (dirty != null)
      {
        nullable1 = dirty.StartDate;
        if (nullable1.HasValue)
          date = dirty.StartDate;
      }
      if (!date.HasValue)
      {
        PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
        if (pmProject != null)
          date = pmProject.StartDate;
      }
    }
    if (!date.HasValue)
    {
      ref DateTime? local = ref date;
      nullable1 = ((PXGraph) this).Accessinfo.BusinessDate;
      DateTime dateTime = nullable1 ?? DateTime.Now;
      local = new DateTime?(dateTime);
    }
    if (minStartPeriodId == null)
      minStartPeriodId = this.FinPeriodRepository.GetPeriodIDFromDate(date, new int?(0));
    return minStartPeriodId;
  }

  protected virtual DateTime? GetFirstChangeRequestDate(int? projectID, int? projectTaskID)
  {
    return PXResultset<PMChangeRequest>.op_Implicit(((PXSelectBase<PMChangeRequest>) new PXSelectJoin<PMChangeRequest, InnerJoin<PMChangeRequestLine, On<PMChangeRequest.refNbr, Equal<PMChangeRequestLine.refNbr>, And<PMChangeRequest.hold, Equal<False>>>>, Where<PMChangeRequestLine.projectID, Equal<Required<PMChangeRequestLine.projectID>>, And<Where<PMChangeRequestLine.costTaskID, Equal<Required<PMChangeRequestLine.costTaskID>>, Or<PMChangeRequestLine.revenueTaskID, Equal<Required<PMChangeRequestLine.revenueTaskID>>>>>>, OrderBy<Asc<PMChangeRequest.date>>>((PXGraph) this)).SelectWindowed(0, 1, new object[3]
    {
      (object) projectID,
      (object) projectTaskID,
      (object) projectTaskID
    }))?.Date;
  }

  protected virtual DateTime? GetLastChangeRequestDate(int? projectID, int? projectTaskID)
  {
    return PXResultset<PMChangeRequest>.op_Implicit(((PXSelectBase<PMChangeRequest>) new PXSelectJoin<PMChangeRequest, InnerJoin<PMChangeRequestLine, On<PMChangeRequest.refNbr, Equal<PMChangeRequestLine.refNbr>, And<PMChangeRequest.hold, Equal<False>>>>, Where<PMChangeRequestLine.projectID, Equal<Required<PMChangeRequestLine.projectID>>, And<Where<PMChangeRequestLine.costTaskID, Equal<Required<PMChangeRequestLine.costTaskID>>, Or<PMChangeRequestLine.revenueTaskID, Equal<Required<PMChangeRequestLine.revenueTaskID>>>>>>, OrderBy<Desc<PMChangeRequest.date>>>((PXGraph) this)).SelectWindowed(0, 1, new object[3]
    {
      (object) projectID,
      (object) projectTaskID,
      (object) projectTaskID
    }))?.Date;
  }

  protected virtual string GetMaxEndPeriodID(PMBudgetInfo row)
  {
    DateTime? date = row.PlannedEndDate;
    string maxEndPeriodId = (string) null;
    PMTran pmTran = PXResultset<PMTran>.op_Implicit(((PXSelectBase<PMTran>) new PXSelect<PMTran, Where<PMTran.projectID, Equal<Required<PMTran.projectID>>, And<PMTran.taskID, Equal<Required<PMTran.taskID>>>>, OrderBy<Desc<PMTran.date>>>((PXGraph) this)).SelectWindowed(0, 1, new object[2]
    {
      (object) row.ProjectID,
      (object) row.ProjectTaskID
    }));
    DateTime? nullable1;
    DateTime? nullable2;
    if (pmTran != null)
    {
      if (!date.HasValue)
      {
        date = pmTran.Date;
        maxEndPeriodId = pmTran.FinPeriodID;
      }
      else
      {
        nullable1 = pmTran.Date;
        nullable2 = date;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          date = pmTran.Date;
          maxEndPeriodId = pmTran.FinPeriodID;
        }
      }
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.changeRequest>())
    {
      DateTime? changeRequestDate = this.GetLastChangeRequestDate(row.ProjectID, row.ProjectTaskID);
      if (changeRequestDate.HasValue)
      {
        if (!date.HasValue)
        {
          date = changeRequestDate;
        }
        else
        {
          nullable2 = changeRequestDate;
          nullable1 = date;
          if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            date = changeRequestDate;
        }
      }
    }
    if (maxEndPeriodId == null && date.HasValue)
      maxEndPeriodId = this.FinPeriodRepository.GetPeriodIDFromDate(date, new int?(0));
    return maxEndPeriodId;
  }

  protected virtual void GeneratePeriodsForBudget(
    PMBudgetInfo row,
    string startPeriodID,
    string endPeriodID)
  {
    PXSelect<PMForecastDetailInfo, Where<PMForecastDetailInfo.projectID, Equal<Required<PMForecastDetailInfo.projectID>>, And<PMForecastDetailInfo.revisionID, Equal<Current<PMForecast.revisionID>>, And<PMForecastDetailInfo.projectTaskID, Equal<Required<PMForecastDetailInfo.projectTaskID>>, And<PMForecastDetail.accountGroupID, Equal<Required<PMForecastDetail.accountGroupID>>, And<PMForecastDetail.inventoryID, Equal<Required<PMForecastDetail.inventoryID>>, And<PMForecastDetail.costCodeID, Equal<Required<PMForecastDetail.costCodeID>>>>>>>>> pxSelect = new PXSelect<PMForecastDetailInfo, Where<PMForecastDetailInfo.projectID, Equal<Required<PMForecastDetailInfo.projectID>>, And<PMForecastDetailInfo.revisionID, Equal<Current<PMForecast.revisionID>>, And<PMForecastDetailInfo.projectTaskID, Equal<Required<PMForecastDetailInfo.projectTaskID>>, And<PMForecastDetail.accountGroupID, Equal<Required<PMForecastDetail.accountGroupID>>, And<PMForecastDetail.inventoryID, Equal<Required<PMForecastDetail.inventoryID>>, And<PMForecastDetail.costCodeID, Equal<Required<PMForecastDetail.costCodeID>>>>>>>>>((PXGraph) this);
    HashSet<string> stringSet = new HashSet<string>();
    object[] objArray = new object[5]
    {
      (object) row.ProjectID,
      (object) row.ProjectTaskID,
      (object) row.AccountGroupID,
      (object) row.InventoryID,
      (object) row.CostCodeID
    };
    foreach (PXResult<PMForecastDetailInfo> pxResult in ((PXSelectBase<PMForecastDetailInfo>) pxSelect).Select(objArray))
    {
      PMForecastDetailInfo forecastDetailInfo = PXResult<PMForecastDetailInfo>.op_Implicit(pxResult);
      stringSet.Add(forecastDetailInfo.PeriodID);
    }
    foreach (FinPeriod finPeriod in this.AllPeriodsBetweenInclusive(startPeriodID, endPeriodID, new int?(0)))
    {
      if (!stringSet.Contains(finPeriod.FinPeriodID))
      {
        PMForecastDetailInfo forecastDetailInfo = new PMForecastDetailInfo();
        forecastDetailInfo.RevisionID = ((PXSelectBase<PMForecast>) this.Revisions).Current.RevisionID;
        forecastDetailInfo.ProjectID = row.ProjectID;
        forecastDetailInfo.ProjectTaskID = row.ProjectTaskID;
        forecastDetailInfo.AccountGroupID = row.AccountGroupID;
        forecastDetailInfo.InventoryID = row.InventoryID;
        forecastDetailInfo.CostCodeID = row.CostCodeID;
        forecastDetailInfo.PeriodID = finPeriod.FinPeriodID;
        forecastDetailInfo.AccountGroupType = row.AccountGroupType;
        ((PXSelectBase<PMForecastDetailInfo>) this.Details).Insert(forecastDetailInfo);
      }
    }
  }

  public IEnumerable<FinPeriod> AllPeriodsBetweenInclusive(
    string startPeriodID,
    string endPeriodID,
    int? organizationID)
  {
    if (int.Parse(startPeriodID) > int.Parse(endPeriodID))
      throw new PXArgumentException(nameof (startPeriodID));
    return GraphHelper.RowCast<FinPeriod>((IEnumerable) PXSelectBase<FinPeriod, PXViewOf<FinPeriod>.BasedOn<SelectFromBase<FinPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FinPeriod.finPeriodID, Between<P.AsString, P.AsString>>>>>.And<BqlOperand<FinPeriod.organizationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) startPeriodID,
      (object) endPeriodID,
      (object) organizationID
    }));
  }

  protected virtual void SettleBalancesProc(PMBudgetInfo row)
  {
    ForecastMaint.Summary summaryFromBudget = this.GetSummaryFromBudget(row);
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
    PMBudget delta = summaryFromBudget.GetDelta();
    bool flag = false;
    Decimal? nullable1;
    Decimal? nullable2;
    if ((pmProject != null ? (!pmProject.BudgetFinalized.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      nullable1 = delta.Qty;
      if (nullable1.GetValueOrDefault() != 0M)
      {
        PMBudgetInfo pmBudgetInfo = row;
        nullable1 = pmBudgetInfo.Qty;
        nullable2 = delta.Qty;
        Decimal num = nullable2.Value;
        Decimal? nullable3;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new Decimal?(nullable1.GetValueOrDefault() + num);
        pmBudgetInfo.Qty = nullable3;
        flag = true;
      }
    }
    if ((pmProject != null ? (!pmProject.BudgetFinalized.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      nullable1 = delta.CuryAmount;
      if (nullable1.GetValueOrDefault() != 0M)
      {
        PMBudgetInfo pmBudgetInfo = row;
        nullable1 = pmBudgetInfo.CuryAmount;
        nullable2 = delta.CuryAmount;
        Decimal num = nullable2.Value;
        Decimal? nullable4;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new Decimal?(nullable1.GetValueOrDefault() + num);
        pmBudgetInfo.CuryAmount = nullable4;
        flag = true;
      }
    }
    if ((pmProject != null ? (pmProject.ChangeOrderWorkflow.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      nullable1 = delta.Qty;
      if (nullable1.GetValueOrDefault() != 0M)
      {
        PMBudgetInfo pmBudgetInfo = row;
        nullable1 = pmBudgetInfo.RevisedQty;
        nullable2 = delta.Qty;
        Decimal num = nullable2.Value;
        Decimal? nullable5;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable5 = nullable2;
        }
        else
          nullable5 = new Decimal?(nullable1.GetValueOrDefault() + num);
        pmBudgetInfo.RevisedQty = nullable5;
        flag = true;
      }
      nullable1 = delta.CuryAmount;
      if (nullable1.GetValueOrDefault() != 0M)
      {
        PMBudgetInfo pmBudgetInfo = row;
        nullable1 = pmBudgetInfo.CuryRevisedAmount;
        nullable2 = delta.CuryAmount;
        Decimal num = nullable2.Value;
        Decimal? nullable6;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable6 = nullable2;
        }
        else
          nullable6 = new Decimal?(nullable1.GetValueOrDefault() + num);
        pmBudgetInfo.CuryRevisedAmount = nullable6;
        flag = true;
      }
    }
    else
    {
      nullable1 = delta.RevisedQty;
      if (nullable1.GetValueOrDefault() != 0M)
      {
        PMBudgetInfo pmBudgetInfo = row;
        nullable1 = pmBudgetInfo.RevisedQty;
        nullable2 = delta.RevisedQty;
        Decimal num = nullable2.Value;
        Decimal? nullable7;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable7 = nullable2;
        }
        else
          nullable7 = new Decimal?(nullable1.GetValueOrDefault() + num);
        pmBudgetInfo.RevisedQty = nullable7;
        flag = true;
      }
      nullable1 = delta.CuryRevisedAmount;
      if (nullable1.GetValueOrDefault() != 0M)
      {
        PMBudgetInfo pmBudgetInfo = row;
        nullable1 = pmBudgetInfo.CuryRevisedAmount;
        nullable2 = delta.CuryRevisedAmount;
        Decimal num = nullable2.Value;
        Decimal? nullable8;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable8 = nullable2;
        }
        else
          nullable8 = new Decimal?(nullable1.GetValueOrDefault() + num);
        pmBudgetInfo.CuryRevisedAmount = nullable8;
        flag = true;
      }
    }
    if (!flag)
      return;
    ((PXSelectBase<PMBudgetInfo>) this.Budgets).Update(row);
  }

  protected virtual void AddMissingLinesProc(PMBudgetInfo row)
  {
    foreach (string missingPeriod in (IEnumerable<string>) this.GetSummaryFromBudget(row).GetMissingPeriods())
    {
      PMForecastDetailInfo forecastDetailInfo = new PMForecastDetailInfo();
      forecastDetailInfo.ProjectID = ((PXSelectBase<PMForecast>) this.Revisions).Current.ProjectID;
      forecastDetailInfo.RevisionID = ((PXSelectBase<PMForecast>) this.Revisions).Current.RevisionID;
      forecastDetailInfo.ProjectTaskID = row.ProjectTaskID;
      forecastDetailInfo.AccountGroupID = row.AccountGroupID;
      forecastDetailInfo.AccountGroupType = row.AccountGroupType;
      forecastDetailInfo.InventoryID = row.InventoryID;
      forecastDetailInfo.CostCodeID = row.CostCodeID;
      forecastDetailInfo.PeriodID = missingPeriod;
      ((PXSelectBase<PMForecastDetailInfo>) this.Details).Insert(forecastDetailInfo);
    }
  }

  protected virtual ForecastMaint.Summary GetSummaryFromBudget(PMBudgetInfo row)
  {
    ForecastMaint.Summary summaryFromBudget = new ForecastMaint.Summary(row);
    foreach (PXResult<PMForecastDetailInfo> pxResult in ((PXSelectBase<PMForecastDetailInfo>) new PXSelect<PMForecastDetailInfo, Where<PMForecastDetailInfo.projectID, Equal<Current<PMForecast.projectID>>, And<PMForecastDetailInfo.revisionID, Equal<Current<PMForecast.revisionID>>, And<PMForecastDetailInfo.projectTaskID, Equal<Required<PMForecastDetailInfo.projectTaskID>>, And<PMForecastDetail.accountGroupID, Equal<Required<PMForecastDetail.accountGroupID>>, And<PMForecastDetail.inventoryID, Equal<Required<PMForecastDetail.inventoryID>>, And<PMForecastDetail.costCodeID, Equal<Required<PMForecastDetail.costCodeID>>>>>>>>>((PXGraph) this)).Select(new object[4]
    {
      (object) row.ProjectTaskID,
      (object) row.AccountGroupID,
      (object) row.InventoryID,
      (object) row.CostCodeID
    }))
    {
      PMForecastDetailInfo detail = PXResult<PMForecastDetailInfo>.op_Implicit(pxResult);
      summaryFromBudget.Add(detail);
    }
    foreach (PXResult<PMForecastHistoryInfo> pxResult in ((PXSelectBase<PMForecastHistoryInfo>) new PXSelect<PMForecastHistoryInfo, Where<PMForecastHistory.projectID, Equal<Current<PMForecast.projectID>>, And<PMForecastHistory.projectTaskID, Equal<Required<PMForecastDetailInfo.projectTaskID>>, And<PMForecastHistory.accountGroupID, Equal<Required<PMForecastDetail.accountGroupID>>, And<PMForecastHistory.inventoryID, Equal<Required<PMForecastDetail.inventoryID>>, And<PMForecastHistory.costCodeID, Equal<Required<PMForecastDetail.costCodeID>>>>>>>>((PXGraph) this)).Select(new object[4]
    {
      (object) row.ProjectTaskID,
      (object) row.AccountGroupID,
      (object) row.InventoryID,
      (object) row.CostCodeID
    }))
    {
      PMForecastHistoryInfo history = PXResult<PMForecastHistoryInfo>.op_Implicit(pxResult);
      summaryFromBudget.Add(history);
    }
    return summaryFromBudget;
  }

  protected virtual PMForecast CreateNewRevision(ForecastMaint.PMForecastCopyDialogInfo info)
  {
    PXSelect<PMForecastDetailInfo, Where<PMForecastDetailInfo.projectID, Equal<Current<PMForecast.projectID>>, And<PMForecastDetailInfo.revisionID, Equal<Required<PMForecast.revisionID>>>>> pxSelect = new PXSelect<PMForecastDetailInfo, Where<PMForecastDetailInfo.projectID, Equal<Current<PMForecast.projectID>>, And<PMForecastDetailInfo.revisionID, Equal<Required<PMForecast.revisionID>>>>>((PXGraph) this);
    string revisionId = ((PXSelectBase<PMForecast>) this.Revisions).Current.RevisionID;
    PMForecast copy = ((PXSelectBase) this.Revisions).Cache.CreateCopy((object) ((PXSelectBase<PMForecast>) this.Revisions).Current) as PMForecast;
    copy.RevisionID = info.RevisionID;
    copy.NoteID = new Guid?();
    PMForecast newRevision = ((PXSelectBase<PMForecast>) this.Revisions).Insert(copy);
    object[] objArray = new object[1]{ (object) revisionId };
    foreach (PXResult<PMForecastDetailInfo> pxResult in ((PXSelectBase<PMForecastDetailInfo>) pxSelect).Select(objArray))
    {
      PMForecastDetailInfo forecastDetailInfo = PXResult<PMForecastDetailInfo>.op_Implicit(pxResult);
      forecastDetailInfo.RevisionID = newRevision.RevisionID;
      forecastDetailInfo.NoteID = new Guid?();
      ((PXSelectBase<PMForecastDetailInfo>) this.Details).Insert(forecastDetailInfo);
    }
    return newRevision;
  }

  protected virtual void _(PX.Data.Events.RowInserting<PMForecastRecord> e)
  {
    if (((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current == null)
      return;
    int? nullable = e.Row.ProjectTaskID;
    if (!nullable.HasValue)
      e.Row.ProjectTaskID = ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.ProjectTaskID;
    nullable = e.Row.AccountGroupID;
    if (!nullable.HasValue)
      e.Row.AccountGroupID = ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.AccountGroupID;
    nullable = e.Row.InventoryID;
    if (!nullable.HasValue)
      e.Row.InventoryID = ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.InventoryID;
    nullable = e.Row.CostCodeID;
    if (!nullable.HasValue)
      e.Row.CostCodeID = ((PXSelectBase<ForecastMaint.PMForecastFilter>) this.Filter).Current.CostCodeID;
    if (e.Row.FinPeriodID == null && !string.IsNullOrEmpty(e.Row.Period))
    {
      string[] strArray = e.Row.Period.Split('-');
      e.Row.FinPeriodID = strArray[1] + strArray[0];
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.costCodes>())
      e.Row.CostCodeID = CostCodeAttribute.DefaultCostCode;
    this.UpdateInsertDetailInfo(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMForecastRecord> e)
  {
    this.UpdateInsertDetailInfo(e.Row);
  }

  protected virtual void UpdateInsertDetailInfo(PMForecastRecord row)
  {
    PMForecastDetailInfo detailInfo = this.GetDetailInfo(row);
    if (detailInfo != null)
    {
      Decimal? nullable = row.Qty;
      if (nullable.HasValue)
        detailInfo.Qty = row.Qty;
      nullable = row.CuryAmount;
      if (nullable.HasValue)
        detailInfo.CuryAmount = row.CuryAmount;
      nullable = row.RevisedQty;
      if (nullable.HasValue)
        detailInfo.RevisedQty = row.RevisedQty;
      nullable = row.CuryRevisedAmount;
      if (nullable.HasValue)
        detailInfo.CuryRevisedAmount = row.CuryRevisedAmount;
      ((PXSelectBase<PMForecastDetailInfo>) this.Details).Update(detailInfo);
    }
    else
    {
      PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, row.AccountGroupID);
      PMForecastDetailInfo forecastDetailInfo = new PMForecastDetailInfo();
      forecastDetailInfo.RevisionID = ((PXSelectBase<PMForecast>) this.Revisions).Current.RevisionID;
      forecastDetailInfo.ProjectID = row.ProjectID;
      forecastDetailInfo.ProjectTaskID = row.ProjectTaskID;
      forecastDetailInfo.AccountGroupID = row.AccountGroupID;
      forecastDetailInfo.InventoryID = row.InventoryID;
      forecastDetailInfo.CostCodeID = row.CostCodeID;
      forecastDetailInfo.PeriodID = row.FinPeriodID;
      forecastDetailInfo.AccountGroupType = pmAccountGroup.Type;
      forecastDetailInfo.Qty = row.Qty;
      forecastDetailInfo.CuryAmount = row.CuryAmount;
      forecastDetailInfo.RevisedQty = row.RevisedQty;
      forecastDetailInfo.CuryRevisedAmount = row.CuryRevisedAmount;
      ((PXSelectBase<PMForecastDetailInfo>) this.Details).Insert(forecastDetailInfo);
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMForecastDetailInfo> e)
  {
    ((PXSelectBase) this.Items).View.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMForecastRecord> e)
  {
    PXUIFieldAttribute.SetEnabled<PMForecastRecord.projectTask>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, e.Row == null);
    PXUIFieldAttribute.SetEnabled<PMForecastRecord.accountGroup>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, e.Row == null);
    PXUIFieldAttribute.SetEnabled<PMForecastRecord.inventory>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, e.Row == null);
    PXUIFieldAttribute.SetEnabled<PMForecastRecord.costCode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, e.Row == null);
    PXUIFieldAttribute.SetEnabled<PMForecastRecord.period>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, e.Row == null);
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMForecastRecord.qty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, !e.Row.IsSummary && !e.Row.IsTotal && !e.Row.IsDifference);
    PXUIFieldAttribute.SetEnabled<PMForecastRecord.curyAmount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, !e.Row.IsSummary && !e.Row.IsTotal && !e.Row.IsDifference);
    PXUIFieldAttribute.SetEnabled<PMForecastRecord.revisedQty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, !e.Row.IsSummary && !e.Row.IsTotal && !e.Row.IsDifference);
    PXUIFieldAttribute.SetEnabled<PMForecastRecord.curyRevisedAmount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, !e.Row.IsSummary && !e.Row.IsTotal && !e.Row.IsDifference);
    PXUIFieldAttribute.SetEnabled<PMForecastRecord.description>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, !e.Row.IsSummary && !e.Row.IsTotal && !e.Row.IsDifference);
    if (!e.Row.IsDifference || !pmProject.BudgetFinalized.GetValueOrDefault())
      return;
    Decimal? nullable = e.Row.Qty;
    Decimal num1 = 0M;
    if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
      PXUIFieldAttribute.SetWarning<PMForecastRecord.qty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, "Original budgeted values of the selected project budget line will not be updated because the project budget is locked.");
    nullable = e.Row.CuryAmount;
    Decimal num2 = 0M;
    if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
      return;
    PXUIFieldAttribute.SetWarning<PMForecastRecord.curyAmount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMForecastRecord>>) e).Cache, (object) e.Row, "Original budgeted values of the selected project budget line will not be updated because the project budget is locked.");
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<ForecastMaint.PMForecastFilter> e)
  {
    if (e.Row == null || !e.Row.ProjectID.HasValue)
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
    if (pmProject != null)
    {
      bool? nullable = pmProject.BudgetFinalized;
      if (nullable.GetValueOrDefault())
      {
        nullable = pmProject.ChangeOrderWorkflow;
        if (nullable.GetValueOrDefault())
        {
          ((PXAction) this.settleBalances).SetEnabled(false);
          return;
        }
      }
    }
    ((PXAction) this.settleBalances).SetEnabled(true);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PMForecastRecord> e)
  {
    e.Cancel = e.Row.IsSummary || e.Row.IsTotal || e.Row.IsDifference;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMForecastRecord> e)
  {
    PMForecastDetailInfo detailInfo = this.GetDetailInfo(e.Row);
    if (detailInfo == null)
      return;
    ((PXSelectBase<PMForecastDetailInfo>) this.Details).Delete(detailInfo);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMForecastRecord> e) => e.Cancel = true;

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.startPeriodID> e)
  {
    if (((PXSelectBase<PMForecastRecord>) this.Items).Current == null)
      return;
    PMBudgetInfo budgetInfoFromKey = this.GetBudgetInfoFromKey(BudgetKeyTuple.Create((IProjectFilter) ((PXSelectBase<PMForecastRecord>) this.Items).Current));
    if (budgetInfoFromKey == null)
      return;
    string minStartPeriodId = this.GetMinStartPeriodID(budgetInfoFromKey);
    if (minStartPeriodId == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.startPeriodID>, ForecastMaint.PMForecastAddPeriodDialogInfo, object>) e).NewValue = (object) PeriodIDAttribute.FormatForDisplay(minStartPeriodId);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.endPeriodID> e)
  {
    if (((PXSelectBase<PMForecastRecord>) this.Items).Current == null)
      return;
    string maxEndPeriodId = this.GetMaxEndPeriodID(this.GetBudgetInfoFromKey(BudgetKeyTuple.Create((IProjectFilter) ((PXSelectBase<PMForecastRecord>) this.Items).Current)));
    if (maxEndPeriodId == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.endPeriodID>, ForecastMaint.PMForecastAddPeriodDialogInfo, object>) e).NewValue = (object) PeriodIDAttribute.FormatForDisplay(maxEndPeriodId);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.startPeriodID> e)
  {
    e.Row.StartDate = new DateTime?();
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.startPeriodID>>) e).Cache.SetDefaultExt<ForecastMaint.PMForecastAddPeriodDialogInfo.startDate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.endPeriodID> e)
  {
    e.Row.EndDate = new DateTime?();
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.endPeriodID>>) e).Cache.SetDefaultExt<ForecastMaint.PMForecastAddPeriodDialogInfo.endDate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.startDate> e)
  {
    if (string.IsNullOrEmpty(e.Row.StartPeriodID))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.startDate>, ForecastMaint.PMForecastAddPeriodDialogInfo, object>) e).NewValue = (object) this.FinPeriodRepository.FindByID(new int?(0), e.Row.StartPeriodID).StartDateUI;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.endDate> e)
  {
    if (string.IsNullOrEmpty(e.Row.EndPeriodID))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastAddPeriodDialogInfo, ForecastMaint.PMForecastAddPeriodDialogInfo.endDate>, ForecastMaint.PMForecastAddPeriodDialogInfo, object>) e).NewValue = (object) this.FinPeriodRepository.FindByID(new int?(0), e.Row.EndPeriodID).EndDateUI;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ForecastMaint.PMForecastCopyDialogInfo, ForecastMaint.PMForecastCopyDialogInfo.revisionID> e)
  {
    if (PXResultset<PMForecast>.op_Implicit(((PXSelectBase<PMForecast>) new PXSelect<PMForecast, Where<PMForecast.projectID, Equal<Current<PMForecast.projectID>>, And<PMForecast.revisionID, Equal<Required<PMForecast.revisionID>>>>>((PXGraph) this)).Select(new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ForecastMaint.PMForecastCopyDialogInfo, ForecastMaint.PMForecastCopyDialogInfo.revisionID>, ForecastMaint.PMForecastCopyDialogInfo, object>) e).NewValue
    })) != null)
      throw new PXSetPropertyException<PMForecast.revisionID>("Duplicate ID");
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastDistributeDialogInfo, ForecastMaint.PMForecastDistributeDialogInfo.qty> e)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
    if (pmProject == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastDistributeDialogInfo, ForecastMaint.PMForecastDistributeDialogInfo.qty>, ForecastMaint.PMForecastDistributeDialogInfo, object>) e).NewValue = (object) !pmProject.BudgetFinalized.GetValueOrDefault();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastDistributeDialogInfo, ForecastMaint.PMForecastDistributeDialogInfo.amount> e)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
    if (pmProject == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ForecastMaint.PMForecastDistributeDialogInfo, ForecastMaint.PMForecastDistributeDialogInfo.amount>, ForecastMaint.PMForecastDistributeDialogInfo, object>) e).NewValue = (object) !pmProject.BudgetFinalized.GetValueOrDefault();
  }

  protected virtual PMBudgetInfo GetBudgetInfoFromKey(BudgetKeyTuple key)
  {
    return PXResultset<PMBudgetInfo>.op_Implicit(((PXSelectBase<PMBudgetInfo>) new PXSelect<PMBudgetInfo, Where<PMBudgetInfo.projectID, Equal<Required<PMBudgetInfo.projectID>>, And<PMBudgetInfo.projectTaskID, Equal<Required<PMBudgetInfo.projectTaskID>>, And<PMBudgetInfo.accountGroupID, Equal<Required<PMBudgetInfo.accountGroupID>>, And<PMBudgetInfo.inventoryID, Equal<Required<PMBudgetInfo.inventoryID>>, And<PMBudgetInfo.costCodeID, Equal<Required<PMBudgetInfo.costCodeID>>>>>>>>((PXGraph) this)).Select(new object[5]
    {
      (object) key.ProjectID,
      (object) key.ProjectTaskID,
      (object) key.AccountGroupID,
      (object) key.InventoryID,
      (object) key.CostCodeID
    }));
  }

  protected virtual PMForecastDetailInfo GetPMForecastDetailInfoKey(PMForecastRecord row)
  {
    PMForecastDetailInfo forecastDetailInfoKey = new PMForecastDetailInfo();
    forecastDetailInfoKey.RevisionID = ((PXSelectBase<PMForecast>) this.Revisions).Current.RevisionID;
    forecastDetailInfoKey.ProjectID = row.ProjectID;
    forecastDetailInfoKey.ProjectTaskID = row.ProjectTaskID;
    forecastDetailInfoKey.AccountGroupID = row.AccountGroupID;
    forecastDetailInfoKey.InventoryID = row.InventoryID;
    forecastDetailInfoKey.CostCodeID = row.CostCodeID;
    forecastDetailInfoKey.PeriodID = row.FinPeriodID;
    forecastDetailInfoKey.AccountGroupType = row.AccountGroupType;
    return forecastDetailInfoKey;
  }

  protected virtual PMForecastDetailInfo GetDetailInfo(PMForecastRecord row)
  {
    return PXResultset<PMForecastDetailInfo>.op_Implicit(((PXSelectBase<PMForecastDetailInfo>) new PXSelect<PMForecastDetailInfo, Where<PMForecastDetailInfo.projectID, Equal<Required<PMForecastDetailInfo.projectID>>, And<PMForecastDetailInfo.revisionID, Equal<Current<PMForecast.revisionID>>, And<PMForecastDetailInfo.projectTaskID, Equal<Required<PMForecastDetailInfo.projectTaskID>>, And<PMForecastDetail.accountGroupID, Equal<Required<PMForecastDetail.accountGroupID>>, And<PMForecastDetail.inventoryID, Equal<Required<PMForecastDetail.inventoryID>>, And<PMForecastDetail.costCodeID, Equal<Required<PMForecastDetail.costCodeID>>, And<PMForecastDetail.periodID, Equal<Required<PMForecastDetail.periodID>>>>>>>>>>((PXGraph) this)).Select(new object[6]
    {
      (object) row.ProjectID,
      (object) row.ProjectTaskID,
      (object) row.AccountGroupID,
      (object) row.InventoryID,
      (object) row.CostCodeID,
      (object) row.FinPeriodID
    }));
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (!(viewName == "Items"))
      return true;
    string str = (string) values[(object) "Period"];
    if (string.IsNullOrEmpty(str))
    {
      this.projectTaskID = (string) values[(object) "ProjectTask"];
      this.accountGroupID = (string) values[(object) "AccountGroup"];
      this.inventoryID = (string) values[(object) "Inventory"];
      this.costCodeID = (string) values[(object) "CostCode"];
      return false;
    }
    if (str == "Total:" || str == "Delta:")
    {
      this.projectTaskID = (string) null;
      this.accountGroupID = (string) null;
      this.inventoryID = (string) null;
      this.costCodeID = (string) null;
      return false;
    }
    PMCostCode pmCostCode = PXResultset<PMCostCode>.op_Implicit(PXSelectBase<PMCostCode, PXSelect<PMCostCode, Where<PMCostCode.isDefault, Equal<True>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    string[] strArray = str.Split('-');
    keys[(object) "ProjectTaskID"] = (object) this.projectTaskID;
    keys[(object) "AccountGroupID"] = (object) this.accountGroupID;
    keys[(object) "InventoryID"] = (object) this.inventoryID;
    keys[(object) "CostCodeID"] = (object) (this.costCodeID ?? pmCostCode.CostCodeCD);
    keys[(object) "FinPeriodID"] = (object) (strArray[1] + strArray[0]);
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public class Summary
  {
    private SortedList<string, PMForecastDetailInfo> details;
    private SortedList<string, PMForecastHistoryInfo> actuals;
    private SortedList<string, PMForecastHistoryInfo> missingActuals;
    private Decimal totalQty;
    private Decimal curyTotalAmount;
    private Decimal totalAmount;
    private Decimal totalRevisedQty;
    private Decimal curyTotalRevisedAmount;
    private Decimal totalRevisedAmount;
    private Decimal totalDraftChangeOrderQty;
    private Decimal curyTotalDraftChangeOrderAmount;
    private Decimal totalChangeOrderQty;
    private Decimal curyTotalChangeOrderAmount;
    private Decimal totalActualQty;
    private Decimal curyTotalActualAmount;
    private Decimal totalActualAmount;

    public PMBudgetInfo Budget { get; private set; }

    public Summary(PMBudgetInfo budget)
    {
      this.Budget = budget;
      this.details = new SortedList<string, PMForecastDetailInfo>();
      this.actuals = new SortedList<string, PMForecastHistoryInfo>();
      this.missingActuals = new SortedList<string, PMForecastHistoryInfo>();
    }

    public void Add(PMForecastDetailInfo detail)
    {
      this.details.Add(detail.PeriodID, detail);
      this.totalQty += detail.Qty.GetValueOrDefault();
      this.curyTotalAmount += detail.CuryAmount.GetValueOrDefault();
      this.totalRevisedQty += detail.RevisedQty.GetValueOrDefault();
      this.curyTotalRevisedAmount += detail.CuryRevisedAmount.GetValueOrDefault();
    }

    public void Add(PMForecastHistoryInfo history)
    {
      if (this.details.ContainsKey(history.PeriodID))
      {
        this.actuals.Add(history.PeriodID, history);
        Decimal draftChangeOrderQty = this.totalDraftChangeOrderQty;
        Decimal? nullable = history.DraftChangeOrderQty;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        this.totalDraftChangeOrderQty = draftChangeOrderQty + valueOrDefault1;
        Decimal changeOrderAmount1 = this.curyTotalDraftChangeOrderAmount;
        nullable = history.CuryDraftChangeOrderAmount;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        this.curyTotalDraftChangeOrderAmount = changeOrderAmount1 + valueOrDefault2;
        Decimal totalChangeOrderQty = this.totalChangeOrderQty;
        nullable = history.ChangeOrderQty;
        Decimal valueOrDefault3 = nullable.GetValueOrDefault();
        this.totalChangeOrderQty = totalChangeOrderQty + valueOrDefault3;
        Decimal changeOrderAmount2 = this.curyTotalChangeOrderAmount;
        nullable = history.CuryChangeOrderAmount;
        Decimal valueOrDefault4 = nullable.GetValueOrDefault();
        this.curyTotalChangeOrderAmount = changeOrderAmount2 + valueOrDefault4;
        Decimal totalActualQty = this.totalActualQty;
        nullable = history.ActualQty;
        Decimal valueOrDefault5 = nullable.GetValueOrDefault();
        this.totalActualQty = totalActualQty + valueOrDefault5;
        Decimal totalActualAmount1 = this.curyTotalActualAmount;
        nullable = history.CuryActualAmount;
        Decimal valueOrDefault6 = nullable.GetValueOrDefault();
        this.curyTotalActualAmount = totalActualAmount1 + valueOrDefault6;
        Decimal totalActualAmount2 = this.totalActualAmount;
        nullable = history.ActualAmount;
        Decimal valueOrDefault7 = nullable.GetValueOrDefault();
        this.totalActualAmount = totalActualAmount2 + valueOrDefault7;
      }
      else
        this.missingActuals.Add(history.PeriodID, history);
    }

    public List<PMForecastRecord> GetList()
    {
      List<PMForecastRecord> list = new List<PMForecastRecord>();
      list.Add(ForecastMaint.Summary.CreateSummaryRecord(this.Budget));
      foreach (PMForecastDetail detail in (IEnumerable<PMForecastDetailInfo>) this.details.Values)
      {
        PMForecastHistoryInfo history = (PMForecastHistoryInfo) null;
        this.actuals.TryGetValue(detail.PeriodID, out history);
        list.Add(ForecastMaint.Summary.CreateDetailRecord(detail, this.Budget, history));
      }
      if (this.details.Count > 0)
      {
        PMForecastRecord totalRecord = this.GetTotalRecord();
        list.Add(totalRecord);
        PMForecastRecord differenceRecord = ForecastMaint.Summary.CreateDifferenceRecord(this.Budget, totalRecord);
        Decimal? qty = differenceRecord.Qty;
        Decimal num1 = 0M;
        if (qty.GetValueOrDefault() == num1 & qty.HasValue)
        {
          Decimal? curyAmount = differenceRecord.CuryAmount;
          Decimal num2 = 0M;
          if (curyAmount.GetValueOrDefault() == num2 & curyAmount.HasValue)
          {
            Decimal? revisedQty = differenceRecord.RevisedQty;
            Decimal num3 = 0M;
            if (revisedQty.GetValueOrDefault() == num3 & revisedQty.HasValue)
            {
              Decimal? curyRevisedAmount = differenceRecord.CuryRevisedAmount;
              Decimal num4 = 0M;
              if (curyRevisedAmount.GetValueOrDefault() == num4 & curyRevisedAmount.HasValue)
              {
                Decimal? actualQty = differenceRecord.ActualQty;
                Decimal num5 = 0M;
                if (actualQty.GetValueOrDefault() == num5 & actualQty.HasValue)
                {
                  Decimal? curyActualAmount = differenceRecord.CuryActualAmount;
                  Decimal num6 = 0M;
                  if (curyActualAmount.GetValueOrDefault() == num6 & curyActualAmount.HasValue)
                  {
                    Decimal? draftChangeOrderQty = differenceRecord.DraftChangeOrderQty;
                    Decimal num7 = 0M;
                    if (draftChangeOrderQty.GetValueOrDefault() == num7 & draftChangeOrderQty.HasValue)
                    {
                      Decimal? changeOrderAmount1 = differenceRecord.CuryDraftChangeOrderAmount;
                      Decimal num8 = 0M;
                      if (changeOrderAmount1.GetValueOrDefault() == num8 & changeOrderAmount1.HasValue)
                      {
                        Decimal? changeOrderQty = differenceRecord.ChangeOrderQty;
                        Decimal num9 = 0M;
                        if (changeOrderQty.GetValueOrDefault() == num9 & changeOrderQty.HasValue)
                        {
                          Decimal? changeOrderAmount2 = differenceRecord.CuryChangeOrderAmount;
                          Decimal num10 = 0M;
                          if (changeOrderAmount2.GetValueOrDefault() == num10 & changeOrderAmount2.HasValue)
                            goto label_19;
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
        list.Add(differenceRecord);
      }
label_19:
      return list;
    }

    public PMForecastRecord GetTotalRecord()
    {
      PMForecastRecord totalRecord = ForecastMaint.Summary.CreateTotalRecord(this.Budget);
      totalRecord.Qty = new Decimal?(this.totalQty);
      totalRecord.CuryAmount = new Decimal?(this.curyTotalAmount);
      totalRecord.RevisedQty = new Decimal?(this.totalRevisedQty);
      totalRecord.CuryRevisedAmount = new Decimal?(this.curyTotalRevisedAmount);
      totalRecord.DraftChangeOrderQty = new Decimal?(this.totalDraftChangeOrderQty);
      totalRecord.CuryDraftChangeOrderAmount = new Decimal?(this.curyTotalDraftChangeOrderAmount);
      totalRecord.ChangeOrderQty = new Decimal?(this.totalChangeOrderQty);
      totalRecord.CuryChangeOrderAmount = new Decimal?(this.curyTotalChangeOrderAmount);
      totalRecord.ActualQty = new Decimal?(this.totalActualQty);
      totalRecord.CuryActualAmount = new Decimal?(this.curyTotalActualAmount);
      totalRecord.ActualAmount = new Decimal?(this.totalActualAmount);
      return totalRecord;
    }

    public IList<PMForecastDetailInfo> Details => this.details.Values;

    public IList<string> GetMissingPeriods() => this.missingActuals.Keys;

    public PMBudget GetDelta()
    {
      return new PMBudget()
      {
        Qty = new Decimal?(this.totalQty - this.Budget.Qty.GetValueOrDefault()),
        CuryAmount = new Decimal?(this.curyTotalAmount - this.Budget.CuryAmount.GetValueOrDefault()),
        Amount = new Decimal?(this.totalAmount - this.Budget.Amount.GetValueOrDefault()),
        RevisedQty = new Decimal?(this.totalRevisedQty - this.Budget.RevisedQty.GetValueOrDefault()),
        CuryRevisedAmount = new Decimal?(this.curyTotalRevisedAmount - this.Budget.CuryRevisedAmount.GetValueOrDefault()),
        RevisedAmount = new Decimal?(this.totalRevisedAmount - this.Budget.RevisedAmount.GetValueOrDefault())
      };
    }

    public static PMForecastRecord CreateSummaryRecord(PMBudgetInfo budget)
    {
      return new PMForecastRecord()
      {
        ProjectID = budget.ProjectID,
        ProjectTaskID = budget.ProjectTaskID,
        AccountGroupID = budget.AccountGroupID,
        InventoryID = budget.InventoryID,
        CostCodeID = budget.CostCodeID,
        FinPeriodID = "000000",
        ProjectTask = budget.ProjectTaskID,
        AccountGroup = budget.AccountGroupID,
        Inventory = budget.InventoryID,
        CostCode = budget.CostCodeID,
        Period = (string) null,
        AccountGroupType = budget.AccountGroupType,
        PlannedStartDate = budget.PlannedStartDate,
        PlannedEndDate = budget.PlannedEndDate,
        Description = budget.Description,
        Qty = budget.Qty,
        CuryAmount = budget.CuryAmount,
        RevisedQty = budget.RevisedQty,
        CuryRevisedAmount = budget.CuryRevisedAmount,
        DraftChangeOrderQty = budget.DraftChangeOrderQty,
        CuryDraftChangeOrderAmount = budget.CuryDraftChangeOrderAmount,
        ChangeOrderQty = budget.ChangeOrderQty,
        CuryChangeOrderAmount = budget.CuryChangeOrderAmount,
        ActualQty = budget.ActualQty,
        CuryActualAmount = budget.CuryActualAmount,
        ActualAmount = budget.ActualAmount
      };
    }

    public static PMForecastRecord CreateTotalRecord(PMBudgetInfo budget)
    {
      return new PMForecastRecord()
      {
        ProjectID = budget.ProjectID,
        ProjectTaskID = budget.ProjectTaskID,
        AccountGroupID = budget.AccountGroupID,
        InventoryID = budget.InventoryID,
        CostCodeID = budget.CostCodeID,
        FinPeriodID = "999998",
        ProjectTask = new int?(),
        AccountGroup = new int?(),
        Inventory = new int?(),
        CostCode = new int?(),
        Period = "Total:",
        AccountGroupType = budget.AccountGroupType,
        PlannedStartDate = new DateTime?(),
        PlannedEndDate = new DateTime?(),
        Description = (string) null,
        Qty = new Decimal?(0M),
        CuryAmount = new Decimal?(0M),
        RevisedQty = new Decimal?(0M),
        CuryRevisedAmount = new Decimal?(0M),
        DraftChangeOrderQty = new Decimal?(0M),
        CuryDraftChangeOrderAmount = new Decimal?(0M),
        ChangeOrderQty = new Decimal?(0M),
        CuryChangeOrderAmount = new Decimal?(0M),
        ActualQty = new Decimal?(0M),
        CuryActualAmount = new Decimal?(0M),
        ActualAmount = new Decimal?(0M)
      };
    }

    public static PMForecastRecord CreateDifferenceRecord(
      PMBudgetInfo budget,
      PMForecastRecord totalRecord)
    {
      PMForecastRecord differenceRecord = new PMForecastRecord();
      differenceRecord.ProjectID = budget.ProjectID;
      differenceRecord.ProjectTaskID = budget.ProjectTaskID;
      differenceRecord.AccountGroupID = budget.AccountGroupID;
      differenceRecord.InventoryID = budget.InventoryID;
      differenceRecord.CostCodeID = budget.CostCodeID;
      differenceRecord.FinPeriodID = "999999";
      differenceRecord.ProjectTask = new int?();
      differenceRecord.AccountGroup = new int?();
      differenceRecord.Inventory = new int?();
      differenceRecord.CostCode = new int?();
      differenceRecord.Period = "Delta:";
      differenceRecord.AccountGroupType = budget.AccountGroupType;
      differenceRecord.PlannedStartDate = new DateTime?();
      differenceRecord.PlannedEndDate = new DateTime?();
      differenceRecord.Description = (string) null;
      Decimal? qty = budget.Qty;
      Decimal valueOrDefault1 = qty.GetValueOrDefault();
      qty = totalRecord.Qty;
      Decimal valueOrDefault2 = qty.GetValueOrDefault();
      differenceRecord.Qty = new Decimal?(valueOrDefault1 - valueOrDefault2);
      Decimal? curyAmount = budget.CuryAmount;
      Decimal valueOrDefault3 = curyAmount.GetValueOrDefault();
      curyAmount = totalRecord.CuryAmount;
      Decimal valueOrDefault4 = curyAmount.GetValueOrDefault();
      differenceRecord.CuryAmount = new Decimal?(valueOrDefault3 - valueOrDefault4);
      Decimal? revisedQty = budget.RevisedQty;
      Decimal valueOrDefault5 = revisedQty.GetValueOrDefault();
      revisedQty = totalRecord.RevisedQty;
      Decimal valueOrDefault6 = revisedQty.GetValueOrDefault();
      differenceRecord.RevisedQty = new Decimal?(valueOrDefault5 - valueOrDefault6);
      Decimal? curyRevisedAmount = budget.CuryRevisedAmount;
      Decimal valueOrDefault7 = curyRevisedAmount.GetValueOrDefault();
      curyRevisedAmount = totalRecord.CuryRevisedAmount;
      Decimal valueOrDefault8 = curyRevisedAmount.GetValueOrDefault();
      differenceRecord.CuryRevisedAmount = new Decimal?(valueOrDefault7 - valueOrDefault8);
      Decimal? draftChangeOrderQty = budget.DraftChangeOrderQty;
      Decimal valueOrDefault9 = draftChangeOrderQty.GetValueOrDefault();
      draftChangeOrderQty = totalRecord.DraftChangeOrderQty;
      Decimal valueOrDefault10 = draftChangeOrderQty.GetValueOrDefault();
      differenceRecord.DraftChangeOrderQty = new Decimal?(valueOrDefault9 - valueOrDefault10);
      Decimal? changeOrderAmount1 = budget.CuryDraftChangeOrderAmount;
      Decimal valueOrDefault11 = changeOrderAmount1.GetValueOrDefault();
      changeOrderAmount1 = totalRecord.CuryDraftChangeOrderAmount;
      Decimal valueOrDefault12 = changeOrderAmount1.GetValueOrDefault();
      differenceRecord.CuryDraftChangeOrderAmount = new Decimal?(valueOrDefault11 - valueOrDefault12);
      Decimal? changeOrderQty = budget.ChangeOrderQty;
      Decimal valueOrDefault13 = changeOrderQty.GetValueOrDefault();
      changeOrderQty = totalRecord.ChangeOrderQty;
      Decimal valueOrDefault14 = changeOrderQty.GetValueOrDefault();
      differenceRecord.ChangeOrderQty = new Decimal?(valueOrDefault13 - valueOrDefault14);
      Decimal? changeOrderAmount2 = budget.CuryChangeOrderAmount;
      Decimal valueOrDefault15 = changeOrderAmount2.GetValueOrDefault();
      changeOrderAmount2 = totalRecord.CuryChangeOrderAmount;
      Decimal valueOrDefault16 = changeOrderAmount2.GetValueOrDefault();
      differenceRecord.CuryChangeOrderAmount = new Decimal?(valueOrDefault15 - valueOrDefault16);
      Decimal? actualQty = budget.ActualQty;
      Decimal valueOrDefault17 = actualQty.GetValueOrDefault();
      actualQty = totalRecord.ActualQty;
      Decimal valueOrDefault18 = actualQty.GetValueOrDefault();
      differenceRecord.ActualQty = new Decimal?(valueOrDefault17 - valueOrDefault18);
      Decimal? curyActualAmount = budget.CuryActualAmount;
      Decimal valueOrDefault19 = curyActualAmount.GetValueOrDefault();
      curyActualAmount = totalRecord.CuryActualAmount;
      Decimal valueOrDefault20 = curyActualAmount.GetValueOrDefault();
      differenceRecord.CuryActualAmount = new Decimal?(valueOrDefault19 - valueOrDefault20);
      Decimal? actualAmount = budget.ActualAmount;
      Decimal valueOrDefault21 = actualAmount.GetValueOrDefault();
      actualAmount = totalRecord.ActualAmount;
      Decimal valueOrDefault22 = actualAmount.GetValueOrDefault();
      differenceRecord.ActualAmount = new Decimal?(valueOrDefault21 - valueOrDefault22);
      return differenceRecord;
    }

    public static PMForecastRecord CreateDetailRecord(
      PMForecastDetail detail,
      PMBudgetInfo budget,
      PMForecastHistoryInfo history)
    {
      PMForecastRecord detailRecord = new PMForecastRecord();
      detailRecord.ProjectID = budget.ProjectID;
      detailRecord.ProjectTaskID = budget.ProjectTaskID;
      detailRecord.AccountGroupID = budget.AccountGroupID;
      detailRecord.InventoryID = budget.InventoryID;
      detailRecord.CostCodeID = budget.CostCodeID;
      detailRecord.FinPeriodID = detail.PeriodID;
      detailRecord.ProjectTask = new int?();
      detailRecord.AccountGroup = new int?();
      detailRecord.Inventory = new int?();
      detailRecord.CostCode = new int?();
      detailRecord.Period = FinPeriodIDFormattingAttribute.FormatForError(detail.PeriodID);
      detailRecord.AccountGroupType = budget.AccountGroupType;
      detailRecord.PlannedStartDate = new DateTime?();
      detailRecord.PlannedEndDate = new DateTime?();
      detailRecord.Description = detail.Description;
      detailRecord.Qty = detail.Qty;
      detailRecord.CuryAmount = detail.CuryAmount;
      detailRecord.RevisedQty = detail.RevisedQty;
      detailRecord.CuryRevisedAmount = detail.CuryRevisedAmount;
      if (history != null)
      {
        detailRecord.DraftChangeOrderQty = new Decimal?(history.DraftChangeOrderQty.GetValueOrDefault());
        detailRecord.CuryDraftChangeOrderAmount = new Decimal?(history.CuryDraftChangeOrderAmount.GetValueOrDefault());
        detailRecord.ChangeOrderQty = new Decimal?(history.ChangeOrderQty.GetValueOrDefault());
        detailRecord.CuryChangeOrderAmount = new Decimal?(history.CuryChangeOrderAmount.GetValueOrDefault());
        detailRecord.ActualQty = new Decimal?(history.ActualQty.GetValueOrDefault());
        detailRecord.CuryActualAmount = new Decimal?(history.CuryActualAmount.GetValueOrDefault());
        detailRecord.ActualAmount = new Decimal?(history.ActualAmount.GetValueOrDefault());
      }
      return detailRecord;
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  public class PMForecastFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _ProjectID;

    [PXDefault(typeof (PMForecast.projectID))]
    [PXDBInt]
    public virtual int? ProjectID
    {
      get => this._ProjectID;
      set => this._ProjectID = value;
    }

    [ProjectTask(typeof (PMForecast.projectID))]
    public virtual int? ProjectTaskID { get; set; }

    [PXDBString(1)]
    [PXDefault("X")]
    [PMAccountType.FilterList]
    [PXUIField(DisplayName = "Type")]
    public virtual string AccountGroupType { get; set; }

    [AccountGroup]
    public virtual int? AccountGroupID { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "Inventory ID")]
    [PMInventorySelector(Filterable = true)]
    public virtual int? InventoryID { get; set; }

    [CostCode(Filterable = false, SkipVerification = true)]
    public virtual int? CostCodeID { get; set; }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ForecastMaint.PMForecastFilter.projectID>
    {
    }

    public abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ForecastMaint.PMForecastFilter.projectTaskID>
    {
    }

    public abstract class accountGroupType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ForecastMaint.PMForecastFilter.accountGroupType>
    {
    }

    public abstract class accountGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ForecastMaint.PMForecastFilter.accountGroupID>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ForecastMaint.PMForecastFilter.inventoryID>
    {
    }

    public abstract class costCodeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ForecastMaint.PMForecastFilter.costCodeID>
    {
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  public class PMCostForecastFilter : ForecastMaint.PMForecastFilter
  {
    [AccountGroup(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.groupID, IsNotNull>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.expense>>>>>.Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<PMAccountType.offBalance>>>>>.And<BqlOperand<PMAccountGroup.isExpense, IBqlBool>.IsEqual<True>>>>>>>))]
    public override int? AccountGroupID { get; set; }

    public new abstract class accountGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ForecastMaint.PMCostForecastFilter.accountGroupID>
    {
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  public class PMForecastAddPeriodDialogInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [FinPeriodSelector(null, null, null, null, null, null, null, true, false, false, false, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
    [PXUIField(DisplayName = "Period From")]
    public virtual string StartPeriodID { get; set; }

    [FinPeriodSelector(null, null, null, null, null, null, null, true, false, false, false, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
    [PXUIField(DisplayName = "Period To")]
    public virtual string EndPeriodID { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Start Date", Enabled = false)]
    public virtual DateTime? StartDate { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "End Date", Enabled = false)]
    public virtual DateTime? EndDate { get; set; }

    public abstract class startPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ForecastMaint.PMForecastAddPeriodDialogInfo.startPeriodID>
    {
    }

    public abstract class endPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ForecastMaint.PMForecastAddPeriodDialogInfo.endPeriodID>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ForecastMaint.PMForecastAddPeriodDialogInfo.startDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ForecastMaint.PMForecastAddPeriodDialogInfo.endDate>
    {
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  public class PMForecastCopyDialogInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(10, InputMask = ">aaaaaaaaaa")]
    [PXDefault]
    [PXUIField(DisplayName = "New Revision")]
    public virtual string RevisionID { get; set; }

    public abstract class revisionID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ForecastMaint.PMForecastCopyDialogInfo.revisionID>
    {
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  public class PMForecastDistributeDialogInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public const string AllRecords = "0";
    public const string Redistribute = "0";
    public const string SelectedRecord = "1";
    public const string AppendVariance = "1";

    [PXStringList(new string[] {"0", "1"}, new string[] {"Distribute Total", "Add Delta"})]
    [PXDBString(1)]
    [PXDefault("0")]
    public virtual string ValueOption { get; set; }

    [PXDBBool]
    [PXUIField(DisplayName = "Original Budgeted Quantity")]
    public virtual bool? Qty { get; set; }

    [PXDBBool]
    [PXUIField(DisplayName = "Original Budgeted Amount")]
    public virtual bool? Amount { get; set; }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Revised Budgeted Quantity")]
    public virtual bool? RevisedQty { get; set; }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Revised Budgeted Amount")]
    public virtual bool? RevisedAmount { get; set; }

    [PXStringList(new string[] {"0", "1"}, new string[] {"All Budget Lines", "Selected Budget Line"})]
    [PXDBString(1)]
    [PXDefault("0")]
    public virtual string ApplyOption { get; set; }

    public abstract class valueOption : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ForecastMaint.PMForecastDistributeDialogInfo.valueOption>
    {
    }

    public abstract class qty : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ForecastMaint.PMForecastDistributeDialogInfo.qty>
    {
    }

    public abstract class amount : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ForecastMaint.PMForecastDistributeDialogInfo.amount>
    {
    }

    public abstract class revisedQty : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ForecastMaint.PMForecastDistributeDialogInfo.revisedQty>
    {
    }

    public abstract class revisedAmount : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ForecastMaint.PMForecastDistributeDialogInfo.revisedAmount>
    {
    }

    public abstract class applyOption : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ForecastMaint.PMForecastDistributeDialogInfo.applyOption>
    {
    }
  }
}
