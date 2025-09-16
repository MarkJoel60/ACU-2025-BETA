// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectEntryBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PM;

public abstract class ProjectEntryBase<TGraph> : PXGraph<
#nullable disable
TGraph, PMProject> where TGraph : ProjectEntryBase<TGraph>
{
  [PXViewName("Project")]
  public FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.baseType, 
  #nullable disable
  Equal<CTPRType.project>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.nonProject, 
  #nullable disable
  Equal<False>>>>>.And<Match<BqlField<
  #nullable enable
  AccessInfo.userName, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  PMProject>.View Project;
  public PXSetup<PMSetup> Setup;
  public PXSetup<PX.Objects.GL.Company> Company;
  [PXImport(typeof (PMProject))]
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenFields(new Type[] {typeof (PMTask.completedPercent), typeof (PMTask.endDate)})]
  public FbqlSelect<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMTask.projectID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PMTask>.View Tasks;
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PMBillingRecord, LeftJoin<PMProforma, On<PMProforma.refNbr, Equal<PMBillingRecord.proformaRefNbr>, And<PMProforma.corrected, Equal<False>>>, LeftJoin<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<PMBillingRecord.aRDocType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<PMBillingRecord.aRRefNbr>>>>>, Where<PMBillingRecord.projectID, Equal<Current<PMProject.contractID>>>, OrderBy<Asc<PMBillingRecord.sortOrder, Asc<PMBillingRecord.proformaRefNbr>>>> Invoices;
  public PXFilter<CostBudgetFilter> CostFilter;
  [PXCopyPasteHiddenFields(new Type[] {typeof (PMCostBudget.revisedQty), typeof (PMCostBudget.curyRevisedAmount), typeof (PMBudget.curyCostToComplete), typeof (PMBudget.percentCompleted), typeof (PMBudget.curyCostAtCompletion), typeof (PMBudget.completedPct)})]
  [PXImport(typeof (PMProject))]
  [PXDependToCache(new Type[] {typeof (PMProject), typeof (CostBudgetFilter)})]
  [PXFilterable(new Type[] {})]
  public FbqlSelect<SelectFromBase<PMCostBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMCostBudget.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PMCostBudget.type, IBqlString>.IsEqual<
  #nullable disable
  AccountType.expense>>>, PMCostBudget>.View CostBudget;
  protected string costBudgetTaskSearch;
  public PXFilter<RevenueBudgetFilter> RevenueFilter;
  [PXCopyPasteHiddenFields(new Type[] {typeof (PMRevenueBudget.completedPct), typeof (PMRevenueBudget.revisedQty), typeof (PMRevenueBudget.curyRevisedAmount), typeof (PMRevenueBudget.curyAmountToInvoice)})]
  [PXImport(typeof (PMProject))]
  [PXFilterable(new Type[] {})]
  [PXDependToCache(new Type[] {typeof (PMProject), typeof (RevenueBudgetFilter)})]
  public FbqlSelect<SelectFromBase<PMRevenueBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMRevenueBudget.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PMRevenueBudget.type, IBqlString>.IsEqual<
  #nullable disable
  AccountType.income>>>, PMRevenueBudget>.View RevenueBudget;
  protected string revenueBudgetTaskSearch;
  [PXImport(typeof (PMProject))]
  [PXFilterable(new Type[] {})]
  public FbqlSelect<SelectFromBase<PMOtherBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMOtherBudget.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PMOtherBudget.type, IBqlString>.IsNotEqual<
  #nullable disable
  AccountType.expense>>>>.And<BqlOperand<
  #nullable enable
  PMOtherBudget.type, IBqlString>.IsNotEqual<
  #nullable disable
  AccountType.income>>>, PMOtherBudget>.View OtherBudget;
  public PXAction<PMProject> viewTask;
  public PXAction<PMProject> viewRevenueBudgetInventory;
  public PXAction<PMProject> viewCostBudgetInventory;
  public PXAction<PMProject> viewCostCommitments;
  public PXAction<PMProject> viewCostTransactions;
  public PXAction<PMProject> viewRevenueTransactions;
  [PXCopyPasteHiddenView]
  [PXVirtualDAC]
  public PXSelect<PMProjectBalanceRecord, Where<PMProjectBalanceRecord.recordID, IsNotNull>, OrderBy<Asc<PMProjectBalanceRecord.sortOrder>>> BalanceRecords;

  /// <summary>
  /// Returns true both for source as well as target graph during copy-paste procedure.
  /// </summary>
  public bool IsCopyPaste { get; protected set; }

  public bool CostCommitmentTrackingEnabled()
  {
    return ServiceLocator.Current.GetInstance<IProjectSettingsManager>().CostCommitmentTracking;
  }

  public virtual bool IsNewProject()
  {
    return ((PXSelectBase) this.Project).Cache.GetStatus((object) ((PXSelectBase<PMProject>) this.Project).Current) == 2;
  }

  public virtual bool ProjectHasOneOfTheStatuses(params string[] statuses)
  {
    return ((IEnumerable<string>) statuses).Contains<string>(((PXSelectBase<PMProject>) this.Project).Current?.Status);
  }

  /// <summary>
  /// Returns true if project is completed, rejected, cancelled or suspended.
  /// </summary>
  /// <returns></returns>
  public virtual bool ProjectIsInactive() => this.ProjectHasOneOfTheStatuses("F", "R", "C", "E");

  public virtual bool IsUserNumberingOn(string numberingSequenceName)
  {
    if (string.IsNullOrWhiteSpace(numberingSequenceName))
      return false;
    PXResult<Numbering> pxResult = ((IQueryable<PXResult<Numbering>>) PXSelectBase<Numbering, PXViewOf<Numbering>.BasedOn<SelectFromBase<Numbering, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Numbering.numberingID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) numberingSequenceName
    })).FirstOrDefault<PXResult<Numbering>>();
    return pxResult != null && PXResult.Unwrap<Numbering>((object) pxResult).UserNumbering.GetValueOrDefault();
  }

  public virtual bool ProductivityVisible()
  {
    return ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.BudgetMetricsEnabled.GetValueOrDefault();
  }

  public virtual bool PrepaymentVisible()
  {
    return ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.PrepaymentEnabled.GetValueOrDefault();
  }

  public virtual bool LimitsVisible()
  {
    return ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.LimitsEnabled.GetValueOrDefault();
  }

  public virtual Type[] GetHiddenTaskFields()
  {
    return new Type[14]
    {
      typeof (PMTask.completedPctMethod),
      typeof (PMTask.defaultSalesAccountID),
      typeof (PMTask.defaultSalesSubID),
      typeof (PMTask.defaultExpenseAccountID),
      typeof (PMTask.defaultExpenseSubID),
      typeof (PMTask.plannedStartDate),
      typeof (PMTask.plannedEndDate),
      typeof (PMTask.billSeparately),
      typeof (PMTask.defaultAccrualAccountID),
      typeof (PMTask.defaultAccrualSubID),
      typeof (PMTask.defaultBranchID),
      typeof (PMTask.locationID),
      typeof (PMTask.wipAccountGroupID),
      typeof (PMTask.defaultCostCodeID)
    };
  }

  public virtual IEnumerable invoices()
  {
    PXResultset<PX.Objects.AR.ARInvoice> pxResultset1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) new PXSelectReadonly2<PX.Objects.AR.ARInvoice, LeftJoin<PMBillingRecord, On<PX.Objects.AR.ARInvoice.docType, Equal<PMBillingRecord.aRDocType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<PMBillingRecord.aRRefNbr>>>, LeftJoin<PMProforma, On<PX.Objects.AR.ARInvoice.docType, Equal<PMProforma.aRInvoiceDocType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<PMProforma.aRInvoiceRefNbr>>>>>, Where<PMBillingRecord.recordID, IsNull, And<PX.Objects.AR.ARInvoice.projectID, Equal<Current<PMProject.contractID>>>>>((PXGraph) this)).Select(Array.Empty<object>());
    if (pxResultset1.Count == 0)
    {
      PXView pxView = new PXView((PXGraph) this, false, ((PXSelectBase) this.Invoices).View.BqlSelect);
      int startRow = PXView.StartRow;
      int num = 0;
      object[] currents = PXView.Currents;
      object[] parameters = PXView.Parameters;
      object[] searches = PXView.Searches;
      string[] sortColumns = PXView.SortColumns;
      bool[] descendings = PXView.Descendings;
      PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
      ref int local1 = ref startRow;
      int maximumRows = PXView.MaximumRows;
      ref int local2 = ref num;
      List<object> objectList = pxView.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
      PXView.StartRow = 0;
      return (IEnumerable) objectList;
    }
    HashSet<string> stringSet = new HashSet<string>();
    PXResultset<PMBillingRecord> pxResultset2 = ((PXSelectBase<PMBillingRecord>) new PXSelectReadonly2<PMBillingRecord, LeftJoin<PMProforma, On<PMProforma.refNbr, Equal<PMBillingRecord.proformaRefNbr>, And<PMProforma.corrected, Equal<False>>>, LeftJoin<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<PMBillingRecord.aRDocType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<PMBillingRecord.aRRefNbr>>>>>, Where<PMBillingRecord.projectID, Equal<Current<PMProject.contractID>>>>((PXGraph) this)).Select(Array.Empty<object>());
    List<PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>> pxResultList = new List<PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>>(pxResultset2.Count + pxResultset1.Count);
    foreach (PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice> pxResult in pxResultset2)
    {
      PX.Objects.AR.ARInvoice arInvoice = PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(arInvoice.RefNbr))
        stringSet.Add($"{arInvoice.DocType}.{arInvoice.RefNbr}");
      pxResultList.Add(pxResult);
    }
    int num1 = -1;
    foreach (PXResult<PX.Objects.AR.ARInvoice, PMBillingRecord, PMProforma> pxResult in pxResultset1)
    {
      PX.Objects.AR.ARInvoice arInvoice = PXResult<PX.Objects.AR.ARInvoice, PMBillingRecord, PMProforma>.op_Implicit(pxResult);
      PMBillingRecord pmBillingRecord = PXResult<PX.Objects.AR.ARInvoice, PMBillingRecord, PMProforma>.op_Implicit(pxResult);
      PMProforma pmProforma = PXResult<PX.Objects.AR.ARInvoice, PMBillingRecord, PMProforma>.op_Implicit(pxResult);
      if (!stringSet.Contains($"{arInvoice.DocType}.{arInvoice.RefNbr}"))
      {
        if (string.IsNullOrEmpty(pmBillingRecord.ARDocType))
        {
          pmBillingRecord.ARDocType = arInvoice.DocType;
          pmBillingRecord.ARRefNbr = arInvoice.RefNbr;
          pmBillingRecord.RecordID = new int?(num1--);
          pmBillingRecord.BillingTag = "P";
          pmBillingRecord.ProjectID = arInvoice.ProjectID;
        }
        bool flag = !string.IsNullOrEmpty(pmProforma?.RefNbr) && pmBillingRecord.ARDocType == arInvoice.DocType && pmBillingRecord.ARRefNbr == arInvoice.RefNbr && (pmProforma == null || !pmProforma.Corrected.GetValueOrDefault());
        if (flag)
          pmBillingRecord.ProformaRefNbr = pmProforma.RefNbr;
        pxResultList.Add(new PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>(pmBillingRecord, flag ? pmProforma : new PMProforma(), arInvoice));
      }
    }
    int num2 = 0;
    bool isDirty = ((PXSelectBase) this.Invoices).Cache.IsDirty;
    foreach (PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice> pxResult in pxResultList)
    {
      PMBillingRecord pmBillingRecord = PXResult<PMBillingRecord, PMProforma, PX.Objects.AR.ARInvoice>.op_Implicit(pxResult);
      pmBillingRecord.SortOrder = new int?(++num2);
      ((PXSelectBase) this.Invoices).Cache.SetStatus((object) pmBillingRecord, (PXEntryStatus) 5);
    }
    ((PXSelectBase) this.Invoices).Cache.IsDirty = isDirty;
    return (IEnumerable) pxResultList;
  }

  public IEnumerable costBudget()
  {
    PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMCostBudget.type, Equal<AccountType.expense>, And<Where<Current<CostBudgetFilter.projectTaskID>, IsNull, Or<Current<CostBudgetFilter.projectTaskID>, Equal<PMCostBudget.projectTaskID>>>>>>, OrderBy<Asc<PMCostBudget.projectID, Asc<PMCostBudget.projectTaskID, Asc<PMCostBudget.inventoryID, Asc<PMCostBudget.costCodeID, Asc<PMCostBudget.accountGroupID>>>>>>> pxSelect = new PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMCostBudget.type, Equal<AccountType.expense>, And<Where<Current<CostBudgetFilter.projectTaskID>, IsNull, Or<Current<CostBudgetFilter.projectTaskID>, Equal<PMCostBudget.projectTaskID>>>>>>, OrderBy<Asc<PMCostBudget.projectID, Asc<PMCostBudget.projectTaskID, Asc<PMCostBudget.inventoryID, Asc<PMCostBudget.costCodeID, Asc<PMCostBudget.accountGroupID>>>>>>>((PXGraph) this);
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    ((List<object>) pxDelegateResult).Capacity = 202;
    pxDelegateResult.IsResultFiltered = false;
    pxDelegateResult.IsResultSorted = true;
    pxDelegateResult.IsResultTruncated = false;
    if (this.IsCostGroupByTask() && !this.IsCopyPaste)
    {
      if (PXView.Searches != null && PXView.Searches.Length == 5 && PXView.MaximumRows == 1 && PXView.Searches[1] != null)
        this.costBudgetTaskSearch = PXView.Searches[1] as string;
      List<PMCostBudget> list = new List<PMCostBudget>(GraphHelper.RowCast<PMCostBudget>((IEnumerable) ((PXSelectBase<PMCostBudget>) pxSelect).Select(Array.Empty<object>())));
      ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) this.AggregateBudget<PMCostBudget>((IList<PMCostBudget>) list));
    }
    else
    {
      PXView pxView = new PXView((PXGraph) this, false, ((PXSelectBase) pxSelect).View.BqlSelect);
      int startRow = PXView.StartRow;
      int num = 0;
      object[] currents = PXView.Currents;
      object[] parameters = PXView.Parameters;
      object[] searches = PXView.Searches;
      string[] sortColumns = PXView.SortColumns;
      bool[] descendings = PXView.Descendings;
      PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
      ref int local1 = ref startRow;
      int maximumRows = PXView.MaximumRows;
      ref int local2 = ref num;
      List<object> objectList = pxView.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
      PXView.StartRow = 0;
      ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) GraphHelper.RowCast<PMCostBudget>((IEnumerable) objectList));
    }
    return (IEnumerable) pxDelegateResult;
  }

  public IEnumerable revenueBudget()
  {
    PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMRevenueBudget.type, Equal<AccountType.income>, And<Where<Current<RevenueBudgetFilter.projectTaskID>, IsNull, Or<Current<RevenueBudgetFilter.projectTaskID>, Equal<PMRevenueBudget.projectTaskID>>>>>>, OrderBy<Asc<PMRevenueBudget.projectID, Asc<PMRevenueBudget.projectTaskID, Asc<PMRevenueBudget.inventoryID, Asc<PMRevenueBudget.costCodeID, Asc<PMRevenueBudget.accountGroupID>>>>>>> pxSelect = new PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMRevenueBudget.type, Equal<AccountType.income>, And<Where<Current<RevenueBudgetFilter.projectTaskID>, IsNull, Or<Current<RevenueBudgetFilter.projectTaskID>, Equal<PMRevenueBudget.projectTaskID>>>>>>, OrderBy<Asc<PMRevenueBudget.projectID, Asc<PMRevenueBudget.projectTaskID, Asc<PMRevenueBudget.inventoryID, Asc<PMRevenueBudget.costCodeID, Asc<PMRevenueBudget.accountGroupID>>>>>>>((PXGraph) this);
    PXDelegateResult pxDelegateResult1 = new PXDelegateResult();
    ((List<object>) pxDelegateResult1).Capacity = 202;
    pxDelegateResult1.IsResultFiltered = this.IsRevenueGroupByTask();
    pxDelegateResult1.IsResultSorted = true;
    pxDelegateResult1.IsResultTruncated = false;
    PXDelegateResult pxDelegateResult2 = pxDelegateResult1;
    if (this.IsRevenueGroupByTask() && !this.IsCopyPaste)
    {
      if (PXView.Searches != null && PXView.Searches.Length == 5 && PXView.MaximumRows == 1 && PXView.Searches[1] != null)
        this.revenueBudgetTaskSearch = PXView.Searches[1] as string;
      List<PMRevenueBudget> list = new List<PMRevenueBudget>(GraphHelper.RowCast<PMRevenueBudget>((IEnumerable) ((PXSelectBase<PMRevenueBudget>) pxSelect).Select(Array.Empty<object>())));
      ((List<object>) pxDelegateResult2).AddRange((IEnumerable<object>) this.AggregateBudget<PMRevenueBudget>((IList<PMRevenueBudget>) list));
    }
    else
    {
      PXView pxView = new PXView((PXGraph) this, false, ((PXSelectBase) pxSelect).View.BqlSelect);
      int startRow = PXView.StartRow;
      int num = 0;
      object[] currents = PXView.Currents;
      object[] parameters = PXView.Parameters;
      object[] searches = PXView.Searches;
      string[] sortColumns = PXView.SortColumns;
      bool[] descendings = PXView.Descendings;
      PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
      ref int local1 = ref startRow;
      int maximumRows = PXView.MaximumRows;
      ref int local2 = ref num;
      List<object> objectList = pxView.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
      PXView.StartRow = 0;
      ((List<object>) pxDelegateResult2).AddRange((IEnumerable<object>) GraphHelper.RowCast<PMRevenueBudget>((IEnumerable) objectList));
    }
    return (IEnumerable) pxDelegateResult2;
  }

  public virtual bool IsCostGroupByTask()
  {
    CostBudgetFilter current = ((PXSelectBase<CostBudgetFilter>) this.CostFilter).Current;
    return current != null && current.GroupByTask.GetValueOrDefault();
  }

  public virtual bool IsRevenueGroupByTask()
  {
    RevenueBudgetFilter current = ((PXSelectBase<RevenueBudgetFilter>) this.RevenueFilter).Current;
    return current != null && current.GroupByTask.GetValueOrDefault();
  }

  public virtual bool CostQuantityVisible() => !this.IsCostGroupByTask();

  public virtual bool RevenueQuantityVisible() => !this.IsRevenueGroupByTask();

  public virtual bool CostBudgetIsEditable() => !this.IsCostGroupByTask();

  public virtual bool RevenueBudgetIsEditable() => !this.IsRevenueGroupByTask();

  public virtual List<PMBudget> AggregateBudget<T>(IList<T> list) where T : PMBudget, new()
  {
    Dictionary<int, PMTask> dictionary1 = new Dictionary<int, PMTask>();
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      dictionary1.Add(pmTask.TaskID.Value, pmTask);
    }
    T obj1 = new T();
    obj1.ProjectID = ((PXSelectBase<PMProject>) this.Project).Current.ContractID;
    obj1.ProjectTaskID = new int?();
    obj1.AccountGroupID = new int?();
    obj1.CostCodeID = new int?();
    obj1.InventoryID = new int?();
    obj1.Description = "Total:";
    obj1.SortOrder = new int?(1);
    T summary1 = obj1;
    Dictionary<int, PMBudget> dictionary2 = new Dictionary<int, PMBudget>();
    foreach (T obj2 in (IEnumerable<T>) list)
    {
      PMBudget record = (PMBudget) obj2;
      int? nullable1 = record.ProjectTaskID;
      int key1 = nullable1.Value;
      PMBudget summary2;
      if (!dictionary2.TryGetValue(key1, out summary2))
      {
        T obj3 = new T();
        obj3.ProjectID = record.ProjectID;
        obj3.ProjectTaskID = record.ProjectTaskID;
        obj3.AccountGroupID = record.AccountGroupID;
        // ISSUE: variable of a boxed type
        __Boxed<T> local1 = (object) obj3;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        local1.CostCodeID = nullable2;
        // ISSUE: variable of a boxed type
        __Boxed<T> local2 = (object) obj3;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        local2.InventoryID = nullable3;
        summary2 = (PMBudget) obj3;
        nullable1 = record.ProjectTaskID;
        if (nullable1.HasValue)
        {
          Dictionary<int, PMTask> dictionary3 = dictionary1;
          nullable1 = record.ProjectTaskID;
          int key2 = nullable1.Value;
          PMTask pmTask;
          ref PMTask local3 = ref pmTask;
          if (dictionary3.TryGetValue(key2, out local3))
            summary2.Description = pmTask.Description;
        }
        dictionary2.Add(key1, summary2);
      }
      this.AddToSummary(summary2, record);
      this.AddToSummary((PMBudget) summary1, record);
      if (((PXSelectBase<PMSetup>) this.Setup).Current.AutoCompleteRevenueBudget.GetValueOrDefault())
        summary2.CuryAmountToInvoice = new Decimal?(0M);
    }
    if (((PXSelectBase<PMSetup>) this.Setup).Current.AutoCompleteRevenueBudget.GetValueOrDefault())
    {
      summary1.CuryAmountToInvoice = new Decimal?(0M);
      foreach (PMRevenueTotal autoRevenueTotal in this.GetAutoRevenueTotals())
      {
        PMBudget pmBudget = dictionary2[autoRevenueTotal.ProjectTaskID.Value];
        Decimal? curyAmountToInvoice = pmBudget.CuryAmountToInvoice;
        Decimal? nullable4 = autoRevenueTotal.CuryAmountToInvoiceProjected;
        Decimal valueOrDefault1 = nullable4.GetValueOrDefault();
        Decimal? nullable5;
        if (!curyAmountToInvoice.HasValue)
        {
          nullable4 = new Decimal?();
          nullable5 = nullable4;
        }
        else
          nullable5 = new Decimal?(curyAmountToInvoice.GetValueOrDefault() + valueOrDefault1);
        pmBudget.CuryAmountToInvoice = nullable5;
        ref T local4 = ref summary1;
        // ISSUE: variable of a boxed type
        __Boxed<T> local5 = (object) local4;
        curyAmountToInvoice = local4.CuryAmountToInvoice;
        nullable4 = autoRevenueTotal.CuryAmountToInvoiceProjected;
        Decimal valueOrDefault2 = nullable4.GetValueOrDefault();
        Decimal? nullable6;
        if (!curyAmountToInvoice.HasValue)
        {
          nullable4 = new Decimal?();
          nullable6 = nullable4;
        }
        else
          nullable6 = new Decimal?(curyAmountToInvoice.GetValueOrDefault() + valueOrDefault2);
        local5.CuryAmountToInvoice = nullable6;
      }
    }
    return new List<PMBudget>((IEnumerable<PMBudget>) dictionary2.Values)
    {
      (PMBudget) summary1
    };
  }

  public virtual void AddToSummary(PMBudget summary, PMBudget record)
  {
    if (summary == null)
      throw new ArgumentNullException(nameof (summary));
    if (record == null)
      throw new ArgumentNullException(nameof (record));
    PMBudget pmBudget1 = summary;
    Decimal? nullable1 = summary.CuryAmount;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryAmount;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    pmBudget1.CuryAmount = nullable2;
    PMBudget pmBudget2 = summary;
    nullable1 = summary.CuryRevisedAmount;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryRevisedAmount;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(valueOrDefault3 + valueOrDefault4);
    pmBudget2.CuryRevisedAmount = nullable3;
    PMBudget pmBudget3 = summary;
    nullable1 = summary.CuryActualAmount;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryActualAmount;
    Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
    Decimal? nullable4 = new Decimal?(valueOrDefault5 + valueOrDefault6);
    pmBudget3.CuryActualAmount = nullable4;
    PMBudget pmBudget4 = summary;
    nullable1 = summary.ActualAmount;
    Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
    nullable1 = record.ActualAmount;
    Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
    Decimal? nullable5 = new Decimal?(valueOrDefault7 + valueOrDefault8);
    pmBudget4.ActualAmount = nullable5;
    PMBudget pmBudget5 = summary;
    nullable1 = summary.CuryInclTaxAmount;
    Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryInclTaxAmount;
    Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(valueOrDefault9 + valueOrDefault10);
    pmBudget5.CuryInclTaxAmount = nullable6;
    PMBudget pmBudget6 = summary;
    nullable1 = summary.InclTaxAmount;
    Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
    nullable1 = record.InclTaxAmount;
    Decimal valueOrDefault12 = nullable1.GetValueOrDefault();
    Decimal? nullable7 = new Decimal?(valueOrDefault11 + valueOrDefault12);
    pmBudget6.InclTaxAmount = nullable7;
    PMBudget pmBudget7 = summary;
    nullable1 = summary.CuryCommittedAmount;
    Decimal valueOrDefault13 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryCommittedAmount;
    Decimal valueOrDefault14 = nullable1.GetValueOrDefault();
    Decimal? nullable8 = new Decimal?(valueOrDefault13 + valueOrDefault14);
    pmBudget7.CuryCommittedAmount = nullable8;
    PMBudget pmBudget8 = summary;
    nullable1 = summary.CommittedAmount;
    Decimal valueOrDefault15 = nullable1.GetValueOrDefault();
    nullable1 = record.CommittedAmount;
    Decimal valueOrDefault16 = nullable1.GetValueOrDefault();
    Decimal? nullable9 = new Decimal?(valueOrDefault15 + valueOrDefault16);
    pmBudget8.CommittedAmount = nullable9;
    PMBudget pmBudget9 = summary;
    nullable1 = summary.CuryCommittedOrigAmount;
    Decimal valueOrDefault17 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryCommittedOrigAmount;
    Decimal valueOrDefault18 = nullable1.GetValueOrDefault();
    Decimal? nullable10 = new Decimal?(valueOrDefault17 + valueOrDefault18);
    pmBudget9.CuryCommittedOrigAmount = nullable10;
    PMBudget pmBudget10 = summary;
    nullable1 = summary.CuryCommittedOpenAmount;
    Decimal valueOrDefault19 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryCommittedOpenAmount;
    Decimal valueOrDefault20 = nullable1.GetValueOrDefault();
    Decimal? nullable11 = new Decimal?(valueOrDefault19 + valueOrDefault20);
    pmBudget10.CuryCommittedOpenAmount = nullable11;
    PMBudget pmBudget11 = summary;
    nullable1 = summary.CuryCommittedInvoicedAmount;
    Decimal valueOrDefault21 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryCommittedInvoicedAmount;
    Decimal valueOrDefault22 = nullable1.GetValueOrDefault();
    Decimal? nullable12 = new Decimal?(valueOrDefault21 + valueOrDefault22);
    pmBudget11.CuryCommittedInvoicedAmount = nullable12;
    PMBudget pmBudget12 = summary;
    nullable1 = summary.CuryChangeOrderAmount;
    Decimal valueOrDefault23 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryChangeOrderAmount;
    Decimal valueOrDefault24 = nullable1.GetValueOrDefault();
    Decimal? nullable13 = new Decimal?(valueOrDefault23 + valueOrDefault24);
    pmBudget12.CuryChangeOrderAmount = nullable13;
    PMBudget pmBudget13 = summary;
    nullable1 = summary.CuryDraftChangeOrderAmount;
    Decimal valueOrDefault25 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryDraftChangeOrderAmount;
    Decimal valueOrDefault26 = nullable1.GetValueOrDefault();
    Decimal? nullable14 = new Decimal?(valueOrDefault25 + valueOrDefault26);
    pmBudget13.CuryDraftChangeOrderAmount = nullable14;
    PMBudget pmBudget14 = summary;
    nullable1 = summary.CuryInvoicedAmount;
    Decimal valueOrDefault27 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryInvoicedAmount;
    Decimal valueOrDefault28 = nullable1.GetValueOrDefault();
    Decimal? nullable15 = new Decimal?(valueOrDefault27 + valueOrDefault28);
    pmBudget14.CuryInvoicedAmount = nullable15;
    PMBudget pmBudget15 = summary;
    nullable1 = summary.InvoicedAmount;
    Decimal valueOrDefault29 = nullable1.GetValueOrDefault();
    nullable1 = record.InvoicedAmount;
    Decimal valueOrDefault30 = nullable1.GetValueOrDefault();
    Decimal? nullable16 = new Decimal?(valueOrDefault29 + valueOrDefault30);
    pmBudget15.InvoicedAmount = nullable16;
    PMBudget pmBudget16 = summary;
    nullable1 = summary.CuryAmountToInvoice;
    Decimal valueOrDefault31 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryAmountToInvoice;
    Decimal valueOrDefault32 = nullable1.GetValueOrDefault();
    Decimal? nullable17 = new Decimal?(valueOrDefault31 + valueOrDefault32);
    pmBudget16.CuryAmountToInvoice = nullable17;
    PMBudget pmBudget17 = summary;
    nullable1 = summary.AmountToInvoice;
    Decimal valueOrDefault33 = nullable1.GetValueOrDefault();
    nullable1 = record.AmountToInvoice;
    Decimal valueOrDefault34 = nullable1.GetValueOrDefault();
    Decimal? nullable18 = new Decimal?(valueOrDefault33 + valueOrDefault34);
    pmBudget17.AmountToInvoice = nullable18;
    PMBudget pmBudget18 = summary;
    nullable1 = summary.CuryMaxAmount;
    Decimal valueOrDefault35 = nullable1.GetValueOrDefault();
    nullable1 = record.CuryMaxAmount;
    Decimal valueOrDefault36 = nullable1.GetValueOrDefault();
    Decimal? nullable19 = new Decimal?(valueOrDefault35 + valueOrDefault36);
    pmBudget18.CuryMaxAmount = nullable19;
    PMBudget pmBudget19 = summary;
    nullable1 = summary.MaxAmount;
    Decimal valueOrDefault37 = nullable1.GetValueOrDefault();
    nullable1 = record.MaxAmount;
    Decimal valueOrDefault38 = nullable1.GetValueOrDefault();
    Decimal? nullable20 = new Decimal?(valueOrDefault37 + valueOrDefault38);
    pmBudget19.MaxAmount = nullable20;
    if (!string.IsNullOrEmpty(summary.Description))
      return;
    summary.Description = record.Description;
  }

  protected virtual List<PMRevenueTotal> GetAutoRevenueTotals()
  {
    List<PMRevenueTotal> autoRevenueTotals = new List<PMRevenueTotal>();
    Dictionary<string, Decimal> completedPercent = this.CalculateCostCompletedPercent();
    foreach (PXResult<PMRevenueTotal> pxResult in ((PXSelectBase<PMRevenueTotal>) new PXSelect<PMRevenueTotal, Where<PMRevenueTotal.projectID, Equal<Current<PMProject.contractID>>>>((PXGraph) this)).Select(Array.Empty<object>()))
    {
      PMRevenueTotal pmRevenueTotal = PXResult<PMRevenueTotal>.op_Implicit(pxResult);
      string key = $"{pmRevenueTotal.ProjectTaskID}.{pmRevenueTotal.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID}";
      Decimal num;
      if (completedPercent.TryGetValue(key, out num))
      {
        Decimal valueOrDefault = pmRevenueTotal.CuryRevisedAmount.GetValueOrDefault();
        pmRevenueTotal.CuryAmountToInvoiceProjected = new Decimal?(Math.Max(0M, valueOrDefault * num * 0.01M - pmRevenueTotal.CuryInvoicedAmount.GetValueOrDefault()));
      }
      autoRevenueTotals.Add(pmRevenueTotal);
    }
    return autoRevenueTotals;
  }

  protected virtual Dictionary<string, Decimal> CalculateCostCompletedPercent()
  {
    PXSelect<PMProductionBudget, Where<PMProductionBudget.projectID, Equal<Current<PMProject.contractID>>>> pxSelect = new PXSelect<PMProductionBudget, Where<PMProductionBudget.projectID, Equal<Current<PMProject.contractID>>>>((PXGraph) this);
    Dictionary<string, Decimal> completedPercent = new Dictionary<string, Decimal>();
    object[] objArray = Array.Empty<object>();
    foreach (PXResult<PMProductionBudget> pxResult in ((PXSelectBase<PMProductionBudget>) pxSelect).Select(objArray))
    {
      PMProductionBudget productionBudget = PXResult<PMProductionBudget>.op_Implicit(pxResult);
      string key = $"{productionBudget.RevenueTaskID}.{productionBudget.RevenueInventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID}";
      Decimal num = 0M;
      if (productionBudget.CuryRevisedAmount.GetValueOrDefault() != 0M)
        num = Decimal.Round(100M * productionBudget.CuryActualAmount.GetValueOrDefault() / productionBudget.CuryRevisedAmount.GetValueOrDefault(), 2);
      completedPercent.Add(key, num);
    }
    return completedPercent;
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public IEnumerable ViewTask(PXAdapter adapter)
  {
    if (((PXSelectBase<PMTask>) this.Tasks).Current != null && ((PXSelectBase) this.Project).Cache.GetStatus((object) ((PXSelectBase<PMProject>) this.Project).Current) != 2)
    {
      if (((PXSelectBase) this.Tasks).Cache.GetStatus((object) ((PXSelectBase<PMTask>) this.Tasks).Current) == 2 || ((PXSelectBase) this.Tasks).Cache.GetStatus((object) ((PXSelectBase<PMTask>) this.Tasks).Current) == 1)
        ((PXAction) this.Save).Press();
      ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
      ((PXSelectBase<PMTask>) instance.Task).Current = PMTask.PK.FindDirty((PXGraph) this, ((PXSelectBase<PMTask>) this.Tasks).Current.ProjectID, ((PXSelectBase<PMTask>) this.Tasks).Current.TaskID);
      ProjectAccountingService.NavigateToScreen((PXGraph) instance, "Project Task Entry - View Task");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewRevenueBudgetInventory(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToInventoryItemScreen(PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMRevenueBudget.inventoryID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCostBudgetInventory(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToInventoryItemScreen(PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMCostBudget.inventoryID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public IEnumerable ViewCostCommitments(PXAdapter adapter)
  {
    if (((PXSelectBase<PMCostBudget>) this.CostBudget).Current != null)
    {
      CommitmentInquiry instance = PXGraph.CreateInstance<CommitmentInquiry>();
      ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.ProjectID;
      ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectTaskID = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.ProjectTaskID;
      ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.AccountGroupID = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.AccountGroupID;
      ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.CostCode = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.CostCodeID;
      if (this.IsCostGroupByTask())
      {
        if (!string.IsNullOrEmpty(this.costBudgetTaskSearch))
        {
          PMTask pmTask = PMTask.UK.Find((PXGraph) this, ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.ProjectID, this.costBudgetTaskSearch);
          ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectTaskID = (int?) pmTask?.TaskID;
        }
        ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.AccountGroupID = new int?();
        ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.CostCode = new int?();
      }
      ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Inquiry")]
  public IEnumerable ViewCostTransactions(PXAdapter adapter)
  {
    if (((PXSelectBase<PMCostBudget>) this.CostBudget).Current != null)
    {
      TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.ProjectID;
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectTaskID = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.TaskID;
      if (this.IsCostGroupByTask())
      {
        if (!string.IsNullOrEmpty(this.costBudgetTaskSearch))
        {
          PMTask pmTask = PMTask.UK.Find((PXGraph) this, ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.ProjectID, this.costBudgetTaskSearch);
          ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectTaskID = (int?) pmTask?.TaskID;
        }
      }
      else
      {
        ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.AccountGroupID = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.AccountGroupID;
        if (PXAccess.FeatureInstalled<FeaturesSet.costCodes>())
        {
          int? costCodeId = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.CostCodeID;
          if (costCodeId.HasValue)
          {
            costCodeId = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.CostCodeID;
            int? defaultCostCode = CostCodeAttribute.DefaultCostCode;
            if (!(costCodeId.GetValueOrDefault() == defaultCostCode.GetValueOrDefault() & costCodeId.HasValue == defaultCostCode.HasValue))
              ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.CostCode = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.CostCodeID;
          }
        }
        else
        {
          int? inventoryId = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.InventoryID;
          if (inventoryId.HasValue)
          {
            inventoryId = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.InventoryID;
            int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
            if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue))
              ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.InventoryID;
          }
        }
      }
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.IncludeUnreleased = new bool?(false);
      ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Inquiry")]
  public IEnumerable ViewRevenueTransactions(PXAdapter adapter)
  {
    if (((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Current != null)
    {
      TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Current.ProjectID;
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectTaskID = ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Current.TaskID;
      if (this.IsRevenueGroupByTask())
      {
        if (!string.IsNullOrEmpty(this.revenueBudgetTaskSearch))
        {
          PMTask pmTask = PMTask.UK.Find((PXGraph) this, ((PXSelectBase<PMCostBudget>) this.CostBudget).Current.ProjectID, this.revenueBudgetTaskSearch);
          ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectTaskID = (int?) pmTask?.TaskID;
        }
      }
      else
      {
        ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.AccountGroupID = ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Current.AccountGroupID;
        int? nullable;
        if (PXAccess.FeatureInstalled<FeaturesSet.costCodes>() && ((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "C")
        {
          int? costCodeId = ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Current.CostCodeID;
          if (costCodeId.HasValue)
          {
            costCodeId = ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Current.CostCodeID;
            nullable = CostCodeAttribute.DefaultCostCode;
            if (!(costCodeId.GetValueOrDefault() == nullable.GetValueOrDefault() & costCodeId.HasValue == nullable.HasValue))
              ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.CostCode = ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Current.CostCodeID;
          }
        }
        if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "I" || ((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "D")
        {
          nullable = ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Current.InventoryID;
          if (nullable.HasValue)
          {
            nullable = ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Current.InventoryID;
            int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
            if (!(nullable.GetValueOrDefault() == emptyInventoryId & nullable.HasValue))
              ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Current.InventoryID;
          }
        }
      }
      ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    }
    return adapter.Get();
  }

  public IEnumerable balanceRecords()
  {
    ProjectEntryBase<TGraph> projectEntryBase = this;
    List<PMProjectBalanceRecord> records = new List<PMProjectBalanceRecord>();
    List<PMProjectBalanceRecord> liability = new List<PMProjectBalanceRecord>();
    List<PMProjectBalanceRecord> income = new List<PMProjectBalanceRecord>();
    List<PMProjectBalanceRecord> expense = new List<PMProjectBalanceRecord>();
    List<PMProjectBalanceRecord> offbalance = new List<PMProjectBalanceRecord>();
    foreach (PXResult<PMBudget, PMAccountGroup> pxResult in ((PXSelectBase<PMBudget>) new PXSelectJoinGroupBy<PMBudget, InnerJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<PMBudget.accountGroupID>>>, Where<PMBudget.projectID, Equal<Current<PMProject.contractID>>>, Aggregate<GroupBy<PMBudget.accountGroupID, Sum<PMBudget.curyAmount, Sum<PMBudget.amount, Sum<PMBudget.curyRevisedAmount, Sum<PMBudget.revisedAmount, Sum<PMBudget.curyActualAmount, Sum<PMBudget.actualAmount, Sum<PMBudget.curyInclTaxAmount, Sum<PMBudget.inclTaxAmount, Sum<PMBudget.curyCommittedAmount, Sum<PMBudget.committedAmount, Sum<PMBudget.curyCommittedOrigAmount, Sum<PMBudget.committedOrigAmount, Sum<PMBudget.curyCommittedOpenAmount, Sum<PMBudget.committedOpenAmount, Sum<PMBudget.curyChangeOrderAmount, Sum<PMBudget.changeOrderAmount, Sum<PMBudget.curyCommittedInvoicedAmount, Sum<PMBudget.committedInvoicedAmount, Sum<PMBudget.curyDraftChangeOrderAmount>>>>>>>>>>>>>>>>>>>>>>((PXGraph) projectEntryBase)).Select(Array.Empty<object>()))
    {
      PMBudget ps = PXResult<PMBudget, PMAccountGroup>.op_Implicit(pxResult);
      PMAccountGroup ag = PXResult<PMBudget, PMAccountGroup>.op_Implicit(pxResult);
      if (ag.IsExpense.GetValueOrDefault())
      {
        expense.Add(projectEntryBase.BalanceRecordFromBudget(ps, ag));
      }
      else
      {
        switch (ag.Type)
        {
          case "A":
            records.Add(projectEntryBase.BalanceRecordFromBudget(ps, ag));
            continue;
          case "L":
            liability.Add(projectEntryBase.BalanceRecordFromBudget(ps, ag));
            continue;
          case "I":
            income.Add(projectEntryBase.BalanceRecordFromBudget(ps, ag));
            continue;
          case "E":
            expense.Add(projectEntryBase.BalanceRecordFromBudget(ps, ag));
            continue;
          case "O":
            offbalance.Add(projectEntryBase.BalanceRecordFromBudget(ps, ag));
            continue;
          default:
            continue;
        }
      }
    }
    // ISSUE: reference to a compiler-generated method
    records.Sort(new Comparison<PMProjectBalanceRecord>(projectEntryBase.\u003CbalanceRecords\u003Eb__51_0));
    // ISSUE: reference to a compiler-generated method
    liability.Sort(new Comparison<PMProjectBalanceRecord>(projectEntryBase.\u003CbalanceRecords\u003Eb__51_1));
    // ISSUE: reference to a compiler-generated method
    income.Sort(new Comparison<PMProjectBalanceRecord>(projectEntryBase.\u003CbalanceRecords\u003Eb__51_2));
    // ISSUE: reference to a compiler-generated method
    expense.Sort(new Comparison<PMProjectBalanceRecord>(projectEntryBase.\u003CbalanceRecords\u003Eb__51_3));
    // ISSUE: reference to a compiler-generated method
    offbalance.Sort(new Comparison<PMProjectBalanceRecord>(projectEntryBase.\u003CbalanceRecords\u003Eb__51_4));
    ((PXSelectBase) projectEntryBase.BalanceRecords).Cache.Clear();
    int cx = 0;
    PXCache cache = ((PXSelectBase) projectEntryBase.BalanceRecords).Cache;
    foreach (PMProjectBalanceRecord balanceLine in projectEntryBase.GetBalanceLines("A", records))
    {
      balanceLine.SortOrder = new int?(cx++);
      yield return cache.Locate((object) balanceLine) ?? cache.Insert((object) balanceLine);
    }
    foreach (PMProjectBalanceRecord balanceLine in projectEntryBase.GetBalanceLines("L", liability))
    {
      balanceLine.SortOrder = new int?(cx++);
      yield return cache.Locate((object) balanceLine) ?? cache.Insert((object) balanceLine);
    }
    foreach (PMProjectBalanceRecord balanceLine in projectEntryBase.GetBalanceLines("I", income))
    {
      balanceLine.SortOrder = new int?(cx++);
      yield return cache.Locate((object) balanceLine) ?? cache.Insert((object) balanceLine);
    }
    foreach (PMProjectBalanceRecord balanceLine in projectEntryBase.GetBalanceLines("E", expense))
    {
      balanceLine.SortOrder = new int?(cx++);
      yield return cache.Locate((object) balanceLine) ?? cache.Insert((object) balanceLine);
    }
    foreach (PMProjectBalanceRecord balanceLine in projectEntryBase.GetBalanceLines("O", offbalance))
    {
      balanceLine.SortOrder = new int?(cx++);
      yield return cache.Locate((object) balanceLine) ?? cache.Insert((object) balanceLine);
    }
    ((PXSelectBase) projectEntryBase.BalanceRecords).Cache.IsDirty = false;
  }

  private int CompareBalanceRecords(PMProjectBalanceRecord x, PMProjectBalanceRecord y)
  {
    return x.AccountGroup.CompareTo(y.AccountGroup);
  }

  public virtual IEnumerable GetBalanceLines(
    string accountType,
    List<PMProjectBalanceRecord> records)
  {
    ProjectEntryBase<TGraph> projectEntryBase = this;
    if (records.Count > 0)
    {
      if (!((PXGraph) projectEntryBase).IsMobile)
        yield return (object) projectEntryBase.CreateHeader(accountType);
      Decimal curyTotalAmt = 0M;
      Decimal curyTotalRevAmt = 0M;
      Decimal curyTotalActAmt = 0M;
      Decimal totalActAmt = 0M;
      Decimal curyTotalARInclTax = 0M;
      Decimal totalARInclTax = 0M;
      Decimal curyTotalComAmt = 0M;
      Decimal curyTotalComOpenAmt = 0M;
      Decimal curyTotalComInvoicedAmt = 0M;
      Decimal curyTotalComOrigAmt = 0M;
      Decimal curyDraftCOAmt = 0M;
      Decimal curyBudgetedCOAmt = 0M;
      Decimal curyCommittedCOAmt = 0M;
      foreach (PMProjectBalanceRecord record in records)
      {
        if (!((PXGraph) projectEntryBase).IsMobile)
        {
          curyTotalAmt += record.CuryAmount.GetValueOrDefault();
          curyTotalRevAmt += record.CuryRevisedAmount.GetValueOrDefault();
          curyTotalActAmt += record.CuryActualAmount.GetValueOrDefault();
          Decimal num1 = totalActAmt;
          Decimal? nullable = record.ActualAmount;
          Decimal valueOrDefault1 = nullable.GetValueOrDefault();
          totalActAmt = num1 + valueOrDefault1;
          Decimal num2 = curyTotalARInclTax;
          nullable = record.CuryInclTaxAmount;
          Decimal valueOrDefault2 = nullable.GetValueOrDefault();
          curyTotalARInclTax = num2 + valueOrDefault2;
          Decimal num3 = totalARInclTax;
          nullable = record.InclTaxAmount;
          Decimal valueOrDefault3 = nullable.GetValueOrDefault();
          totalARInclTax = num3 + valueOrDefault3;
          Decimal num4 = curyTotalComAmt;
          nullable = record.CuryCommittedAmount;
          Decimal valueOrDefault4 = nullable.GetValueOrDefault();
          curyTotalComAmt = num4 + valueOrDefault4;
          Decimal num5 = curyTotalComOpenAmt;
          nullable = record.CuryCommittedOpenAmount;
          Decimal valueOrDefault5 = nullable.GetValueOrDefault();
          curyTotalComOpenAmt = num5 + valueOrDefault5;
          Decimal num6 = curyTotalComInvoicedAmt;
          nullable = record.CuryCommittedInvoicedAmount;
          Decimal valueOrDefault6 = nullable.GetValueOrDefault();
          curyTotalComInvoicedAmt = num6 + valueOrDefault6;
          Decimal num7 = curyTotalComOrigAmt;
          nullable = record.CuryOriginalCommittedAmount;
          Decimal valueOrDefault7 = nullable.GetValueOrDefault();
          curyTotalComOrigAmt = num7 + valueOrDefault7;
          Decimal num8 = curyDraftCOAmt;
          nullable = record.CuryDraftCOAmount;
          Decimal valueOrDefault8 = nullable.GetValueOrDefault();
          curyDraftCOAmt = num8 + valueOrDefault8;
          Decimal num9 = curyBudgetedCOAmt;
          nullable = record.CuryBudgetedCOAmount;
          Decimal valueOrDefault9 = nullable.GetValueOrDefault();
          curyBudgetedCOAmt = num9 + valueOrDefault9;
          Decimal num10 = curyCommittedCOAmt;
          nullable = record.CuryCommittedCOAmount;
          Decimal valueOrDefault10 = nullable.GetValueOrDefault();
          curyCommittedCOAmt = num10 + valueOrDefault10;
        }
        yield return (object) record;
      }
      if (!((PXGraph) projectEntryBase).IsMobile)
        yield return (object) projectEntryBase.CreateTotal(accountType, curyTotalAmt, curyTotalRevAmt, curyTotalActAmt, totalActAmt, curyTotalARInclTax, totalARInclTax, curyTotalComAmt, curyTotalComOpenAmt, curyTotalComInvoicedAmt, curyTotalComOrigAmt, curyDraftCOAmt, curyBudgetedCOAmt, curyCommittedCOAmt);
    }
  }

  public virtual PMProjectBalanceRecord BalanceRecordFromBudget(PMBudget ps, PMAccountGroup ag)
  {
    return new PMProjectBalanceRecord()
    {
      RecordID = ps.AccountGroupID,
      AccountGroup = ag.GroupCD,
      Description = ag.Description,
      CuryAmount = ps.CuryAmount,
      Amount = ps.Amount,
      CuryRevisedAmount = ps.CuryRevisedAmount,
      RevisedAmount = ps.RevisedAmount,
      CuryActualAmount = ps.CuryActualAmount,
      ActualAmount = ps.ActualAmount,
      CuryInclTaxAmount = ps.CuryInclTaxAmount,
      InclTaxAmount = ps.InclTaxAmount,
      CuryDraftCOAmount = ps.CuryDraftChangeOrderAmount,
      CuryBudgetedCOAmount = ps.CuryChangeOrderAmount,
      BudgetedCOAmount = ps.ChangeOrderAmount,
      CuryOriginalCommittedAmount = ps.CuryCommittedOrigAmount,
      OriginalCommittedAmount = ps.CommittedOrigAmount,
      CuryCommittedCOAmount = ps.CuryCommittedCOAmount,
      CommittedCOAmount = ps.CommittedCOAmount,
      CuryCommittedAmount = ps.CuryCommittedAmount,
      CommittedAmount = ps.CommittedAmount,
      CuryCommittedOpenAmount = ps.CuryCommittedOpenAmount,
      CommittedOpenAmount = ps.CommittedOpenAmount,
      CuryCommittedInvoicedAmount = ps.CuryCommittedInvoicedAmount,
      CommittedInvoicedAmount = ps.CommittedInvoicedAmount
    };
  }

  public virtual PMProjectBalanceRecord CreateHeader(string accountType)
  {
    PMProjectBalanceRecord header = new PMProjectBalanceRecord();
    switch (accountType)
    {
      case "A":
        header.RecordID = new int?(-10);
        header.AccountGroup = Messages.GetLocal("Asset");
        break;
      case "L":
        header.RecordID = new int?(-20);
        header.AccountGroup = Messages.GetLocal("Liability");
        break;
      case "I":
        header.RecordID = new int?(-30);
        header.AccountGroup = Messages.GetLocal("Income");
        break;
      case "E":
        header.RecordID = new int?(-40);
        header.AccountGroup = Messages.GetLocal("Expense");
        break;
      case "O":
        header.RecordID = new int?(-50);
        header.AccountGroup = Messages.GetLocal("Off-Balance");
        break;
    }
    return header;
  }

  public virtual PMProjectBalanceRecord CreateTotal(
    string accountType,
    Decimal curyAmount,
    Decimal curyRevisedAmt,
    Decimal curyActualAmt,
    Decimal actualAmt,
    Decimal curyInclTaxAmt,
    Decimal inclTaxAmt,
    Decimal curyCommittedAmt,
    Decimal curyCommittedOpenAmt,
    Decimal curyCommittedInvoicedAmt,
    Decimal curyCommitteOrigAmt,
    Decimal curyDraftCOAmt,
    Decimal curyBudgetedCOAmt,
    Decimal curyCommittedCOAmt)
  {
    PMProjectBalanceRecord total = new PMProjectBalanceRecord();
    switch (accountType)
    {
      case "A":
        total.RecordID = new int?(-11);
        total.Description = Messages.GetLocal("Asset Totals");
        break;
      case "L":
        total.RecordID = new int?(-21);
        total.Description = Messages.GetLocal("Liability Totals");
        break;
      case "I":
        total.RecordID = new int?(-31);
        total.Description = Messages.GetLocal("Income Totals");
        break;
      case "E":
        total.RecordID = new int?(-41);
        total.Description = Messages.GetLocal("Expense Totals");
        break;
      case "O":
        total.RecordID = new int?(-51);
        total.Description = Messages.GetLocal("Off-Balance Totals");
        break;
    }
    total.CuryAmount = new Decimal?(curyAmount);
    total.CuryRevisedAmount = new Decimal?(curyRevisedAmt);
    total.CuryActualAmount = new Decimal?(curyActualAmt);
    total.ActualAmount = new Decimal?(actualAmt);
    total.CuryInclTaxAmount = new Decimal?(curyInclTaxAmt);
    total.InclTaxAmount = new Decimal?(inclTaxAmt);
    total.CuryCommittedAmount = new Decimal?(curyCommittedAmt);
    total.CuryCommittedOpenAmount = new Decimal?(curyCommittedOpenAmt);
    total.CuryCommittedInvoicedAmount = new Decimal?(curyCommittedInvoicedAmt);
    total.CuryOriginalCommittedAmount = new Decimal?(curyCommitteOrigAmt);
    total.CuryDraftCOAmount = new Decimal?(curyDraftCOAmt);
    total.CuryBudgetedCOAmount = new Decimal?(curyBudgetedCOAmt);
    total.CuryCommittedCOAmount = new Decimal?(curyCommittedCOAmt);
    return total;
  }

  protected virtual void ThrowIfStatusCodeIsNotValid(PMProject project, PXCache cache)
  {
    string statusDescription;
    PXErrorLevel errorLevel;
    if (project == null || !project.StatusCode.HasValue || StatusCodeHelper.IsValidStatus(project.StatusCode, out statusDescription, out errorLevel))
      return;
    cache.RaiseExceptionHandling<PMProject.contractCD>((object) project, (object) project.ContractCD, (Exception) new PXSetPropertyException<PMProject.contractCD>(statusDescription, errorLevel, new object[1]
    {
      (object) project.ContractCD
    }));
  }

  public virtual PMProjectBalanceRecord CreateFooter(string accountType)
  {
    PMProjectBalanceRecord footer = new PMProjectBalanceRecord();
    switch (accountType)
    {
      case "A":
        footer.RecordID = new int?(-12);
        break;
      case "L":
        footer.RecordID = new int?(-22);
        break;
      case "I":
        footer.RecordID = new int?(-32);
        break;
      case "E":
        footer.RecordID = new int?(-42);
        break;
      case "O":
        footer.RecordID = new int?(-52);
        break;
    }
    return footer;
  }
}
