// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProgressWorksheetEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PM;

public class ProgressWorksheetEntry : PXGraph<
#nullable disable
ProgressWorksheetEntry, PMProgressWorksheet>
{
  [PXViewName("Progress Worksheet")]
  public FbqlSelect<SelectFromBase<PMProgressWorksheet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProject>.On<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  PMProgressWorksheet.projectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.contractID, 
  #nullable disable
  IsNull>>>>.Or<MatchUserFor<PMProject>>>>>.And<BqlOperand<
  #nullable enable
  PMProgressWorksheet.hidden, IBqlBool>.IsNotEqual<
  #nullable disable
  True>>>, PMProgressWorksheet>.View Document;
  public PXSelect<PMProgressWorksheet, Where<PMProgressWorksheet.refNbr, Equal<Current<PMProgressWorksheet.refNbr>>>> DocumentSettings;
  [PXViewName("Project")]
  public PXSetup<PMProject>.Where<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProgressWorksheet.projectID, IBqlInt>.FromCurrent>> Project;
  public 
  #nullable disable
  PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<PMSetup> Setup;
  [PXImport(typeof (PMProgressWorksheet))]
  [PXFilterable(new System.Type[] {})]
  public PXSelect<PMProgressWorksheetLine, Where<PMProgressWorksheetLine.refNbr, Equal<Current<PMProgressWorksheet.refNbr>>>> Details;
  public PXWorkflowEventHandler<PMProgressWorksheet> OnRelease;
  [PXCopyPasteHiddenView]
  [PXViewName("Approval")]
  public EPApprovalAutomation<PMProgressWorksheet, PMProgressWorksheet.approved, PMProgressWorksheet.rejected, PMProgressWorksheet.hold, PMSetupProgressWorksheetApproval> Approval;
  public PXFilter<ProgressWorksheetEntry.CostBudgetLineFilter> costBudgetfilter;
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PMCostBudget, LeftJoin<PMCostCode, On<PMCostBudget.costCodeID, Equal<PMCostCode.costCodeID>>, InnerJoin<PMTask, On<PMCostBudget.projectID, Equal<PMTask.projectID>, And<PMCostBudget.projectTaskID, Equal<PMTask.taskID>>>, InnerJoin<PMAccountGroup, On<PMCostBudget.accountGroupID, Equal<PMAccountGroup.groupID>>>>>, Where<PMCostBudget.projectID, Equal<Current<ProgressWorksheetEntry.CostBudgetLineFilter.projectID>>, And<PMCostBudget.type, Equal<AccountType.expense>, And<PMCostBudget.productivityTracking, IsNotNull, And<PMCostBudget.productivityTracking, NotEqual<PMProductivityTrackingType.notAllowed>, And<PMTask.status, NotEqual<ProjectTaskStatus.canceled>, And<PMTask.status, NotEqual<ProjectTaskStatus.completed>, And<PMCostCode.isActive, NotEqual<False>, And<PMAccountGroup.isActive, Equal<True>>>>>>>>>> CostBudgets;
  public PXAction<PMProgressWorksheet> removeHold;
  public PXAction<PMProgressWorksheet> hold;
  public PXAction<PMProgressWorksheet> release;
  public PXAction<PMProgressWorksheet> loadTemplate;
  public PXAction<PMProgressWorksheet> selectBudgetLines;
  public PXAction<PMProgressWorksheet> addSelectedBudgetLines;
  public const string ProgressWorksheetReport = "PM657000";
  public PXAction<PMProgressWorksheet> print;
  public PXAction<PMProgressWorksheet> correct;
  public PXAction<PMProgressWorksheet> reverse;
  private bool blockLineSelectingEvent;

  [PXMergeAttributes]
  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  public IEnumerable costBudgets()
  {
    List<object> objectList1 = new List<object>();
    PXSelectJoin<PMCostBudget, LeftJoin<PMCostCode, On<PMCostBudget.costCodeID, Equal<PMCostCode.costCodeID>>, InnerJoin<PMTask, On<PMCostBudget.projectID, Equal<PMTask.projectID>, And<PMCostBudget.projectTaskID, Equal<PMTask.taskID>>>, InnerJoin<PMAccountGroup, On<PMCostBudget.accountGroupID, Equal<PMAccountGroup.groupID>>>>>, Where<PMCostBudget.projectID, Equal<Current<ProgressWorksheetEntry.CostBudgetLineFilter.projectID>>, And<PMCostBudget.type, Equal<AccountType.expense>, And<PMCostBudget.productivityTracking, IsNotNull, And<PMCostBudget.productivityTracking, NotEqual<PMProductivityTrackingType.notAllowed>, And<PMTask.status, NotEqual<ProjectTaskStatus.canceled>, And<PMTask.status, NotEqual<ProjectTaskStatus.completed>, And<PMCostCode.isActive, NotEqual<False>, And<PMAccountGroup.isActive, Equal<True>>>>>>>>>> pxSelectJoin = new PXSelectJoin<PMCostBudget, LeftJoin<PMCostCode, On<PMCostBudget.costCodeID, Equal<PMCostCode.costCodeID>>, InnerJoin<PMTask, On<PMCostBudget.projectID, Equal<PMTask.projectID>, And<PMCostBudget.projectTaskID, Equal<PMTask.taskID>>>, InnerJoin<PMAccountGroup, On<PMCostBudget.accountGroupID, Equal<PMAccountGroup.groupID>>>>>, Where<PMCostBudget.projectID, Equal<Current<ProgressWorksheetEntry.CostBudgetLineFilter.projectID>>, And<PMCostBudget.type, Equal<AccountType.expense>, And<PMCostBudget.productivityTracking, IsNotNull, And<PMCostBudget.productivityTracking, NotEqual<PMProductivityTrackingType.notAllowed>, And<PMTask.status, NotEqual<ProjectTaskStatus.canceled>, And<PMTask.status, NotEqual<ProjectTaskStatus.completed>, And<PMCostCode.isActive, NotEqual<False>, And<PMAccountGroup.isActive, Equal<True>>>>>>>>>>((PXGraph) this);
    if (((PXSelectBase<ProgressWorksheetEntry.CostBudgetLineFilter>) this.costBudgetfilter).Current.TaskID.HasValue)
    {
      ((PXSelectBase<PMCostBudget>) pxSelectJoin).WhereAnd(typeof (Where<PMCostBudget.projectTaskID, Equal<Required<PMCostBudget.projectTaskID>>>));
      objectList1.Add((object) ((PXSelectBase<ProgressWorksheetEntry.CostBudgetLineFilter>) this.costBudgetfilter).Current.TaskID);
    }
    int? nullable = ((PXSelectBase<ProgressWorksheetEntry.CostBudgetLineFilter>) this.costBudgetfilter).Current.AccountGroupID;
    if (nullable.HasValue)
    {
      ((PXSelectBase<PMCostBudget>) pxSelectJoin).WhereAnd(typeof (Where<PMCostBudget.accountGroupID, Equal<Required<PMCostBudget.accountGroupID>>>));
      objectList1.Add((object) ((PXSelectBase<ProgressWorksheetEntry.CostBudgetLineFilter>) this.costBudgetfilter).Current.AccountGroupID);
    }
    nullable = ((PXSelectBase<ProgressWorksheetEntry.CostBudgetLineFilter>) this.costBudgetfilter).Current.InventoryID;
    if (nullable.HasValue)
    {
      ((PXSelectBase<PMCostBudget>) pxSelectJoin).WhereAnd(typeof (Where<PMCostBudget.inventoryID, Equal<Required<PMCostBudget.inventoryID>>>));
      objectList1.Add((object) ((PXSelectBase<ProgressWorksheetEntry.CostBudgetLineFilter>) this.costBudgetfilter).Current.InventoryID);
    }
    nullable = ((PXSelectBase<ProgressWorksheetEntry.CostBudgetLineFilter>) this.costBudgetfilter).Current.CostCodeFrom;
    if (nullable.HasValue)
    {
      PMCostCode pmCostCode = ((PXSelectBase<PMCostCode>) new PXSelect<PMCostCode, Where<PMCostCode.costCodeID, Equal<Current<ProgressWorksheetEntry.CostBudgetLineFilter.costCodeFrom>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>());
      ((PXSelectBase<PMCostBudget>) pxSelectJoin).WhereAnd(typeof (Where<PMCostCode.costCodeCD, GreaterEqual<Required<PMCostCode.costCodeCD>>>));
      objectList1.Add((object) pmCostCode.CostCodeCD);
    }
    nullable = ((PXSelectBase<ProgressWorksheetEntry.CostBudgetLineFilter>) this.costBudgetfilter).Current.CostCodeTo;
    if (nullable.HasValue)
    {
      PMCostCode pmCostCode = ((PXSelectBase<PMCostCode>) new PXSelect<PMCostCode, Where<PMCostCode.costCodeID, Equal<Current<ProgressWorksheetEntry.CostBudgetLineFilter.costCodeTo>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>());
      ((PXSelectBase<PMCostBudget>) pxSelectJoin).WhereAnd(typeof (Where<PMCostCode.costCodeCD, LessEqual<Required<PMCostCode.costCodeCD>>>));
      objectList1.Add((object) pmCostCode.CostCodeCD);
    }
    objectList1.AddRange((IEnumerable<object>) PXView.Parameters);
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    ((List<object>) pxDelegateResult).Capacity = 202;
    pxDelegateResult.IsResultFiltered = false;
    pxDelegateResult.IsResultSorted = true;
    pxDelegateResult.IsResultTruncated = false;
    PXView pxView = new PXView((PXGraph) this, false, ((PXSelectBase) pxSelectJoin).View.BqlSelect);
    int startRow = PXView.StartRow;
    int num = 0;
    object[] currents = PXView.Currents;
    object[] array = objectList1.ToArray();
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList2 = pxView.Select(currents, array, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    HashSet<BudgetKeyTuple> existingCostBudgetLines = this.GetExistingCostBudgetLines();
    foreach (PXResult<PMCostBudget, PMCostCode> pxResult in objectList2)
    {
      PMCostBudget budget = PXResult<PMCostBudget, PMCostCode>.op_Implicit(pxResult);
      if (!existingCostBudgetLines.Contains(BudgetKeyTuple.Create((IProjectFilter) budget)))
        ((List<object>) pxDelegateResult).Add((object) budget);
    }
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual HashSet<BudgetKeyTuple> GetExistingCostBudgetLines()
  {
    HashSet<BudgetKeyTuple> existingCostBudgetLines = new HashSet<BudgetKeyTuple>();
    foreach (PXResult<PMProgressWorksheetLine> pxResult in ((PXSelectBase<PMProgressWorksheetLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMProgressWorksheetLine budget = PXResult<PMProgressWorksheetLine>.op_Implicit(pxResult);
      existingCostBudgetLines.Add(BudgetKeyTuple.Create((IProjectFilter) budget));
    }
    return existingCostBudgetLines;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable RemoveHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable Hold(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Release")]
  [PXProcessButton]
  public IEnumerable Release(PXAdapter adapter)
  {
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CRelease\u003Eb__19_0)));
    return adapter.Get();
  }

  public virtual void ReleaseDocument(PMProgressWorksheet doc)
  {
    if (doc == null)
      throw new ArgumentNullException();
    doc.Released = !doc.Released.GetValueOrDefault() ? new bool?(true) : throw new PXException("This document is already released.");
    ((PXSelectBase<PMProgressWorksheet>) this.Document).Update(doc);
    ((PXAction) this.Save).Press();
  }

  [PXUIField]
  [PXLookupButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable LoadTemplate(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProgressWorksheet>) this.Document).Current.Hold.GetValueOrDefault() && ((PXSelectBase<PMProgressWorksheet>) this.Document).Current.ProjectID.HasValue)
    {
      ((PXSelectBase) this.Details).Cache.ForceExceptionHandling = true;
      HashSet<BudgetKeyTuple> existingCostBudgetLines = this.GetExistingCostBudgetLines();
      foreach (PXResult<PMCostBudget> pxResult in ((PXSelectBase<PMCostBudget>) new PXSelectJoin<PMCostBudget, InnerJoin<PMTask, On<PMCostBudget.projectID, Equal<PMTask.projectID>, And<PMCostBudget.projectTaskID, Equal<PMTask.taskID>>>, InnerJoin<PMAccountGroup, On<PMCostBudget.accountGroupID, Equal<PMAccountGroup.groupID>>>>, Where<PMCostBudget.projectID, Equal<Required<PMProject.contractID>>, And<PMCostBudget.type, Equal<AccountType.expense>, And<PMCostBudget.productivityTracking, Equal<PMProductivityTrackingType.template>, And<PMTask.status, NotEqual<ProjectTaskStatus.canceled>, And<PMTask.status, NotEqual<ProjectTaskStatus.completed>, And<PMAccountGroup.isActive, Equal<True>>>>>>>>((PXGraph) this)).Select(new object[1]
      {
        (object) ((PXSelectBase<PMProgressWorksheet>) this.Document).Current.ProjectID
      }))
      {
        PMCostBudget budget = PXResult<PMCostBudget>.op_Implicit(pxResult);
        if (!existingCostBudgetLines.Contains(BudgetKeyTuple.Create((IProjectFilter) budget)))
          ((PXSelectBase<PMProgressWorksheetLine>) this.Details).Insert(new PMProgressWorksheetLine()
          {
            ProjectID = budget.ProjectID,
            TaskID = budget.ProjectTaskID,
            InventoryID = budget.InventoryID,
            AccountGroupID = budget.AccountGroupID,
            CostCodeID = budget.CostCodeID
          });
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable SelectBudgetLines(PXAdapter adapter)
  {
    IEnumerable enumerable = (IEnumerable) null;
    if (((PXSelectBase<PMProgressWorksheet>) this.Document).Current.Hold.GetValueOrDefault() && ((PXSelectBase<PMProgressWorksheet>) this.Document).Current.ProjectID.HasValue)
    {
      ((PXSelectBase<ProgressWorksheetEntry.CostBudgetLineFilter>) this.costBudgetfilter).Current.ProjectID = ((PXSelectBase<PMProgressWorksheet>) this.Document).Current.ProjectID;
      if (((PXSelectBase<PMCostBudget>) this.CostBudgets).AskExt() == 1)
        enumerable = this.AddSelectedBudgetLines(adapter);
      ((PXSelectBase) this.costBudgetfilter).Cache.Clear();
      ((PXSelectBase) this.CostBudgets).Cache.Clear();
      ((PXSelectBase<PMCostBudget>) this.CostBudgets).ClearDialog();
      ((PXSelectBase) this.CostBudgets).View.Clear();
      ((PXSelectBase) this.CostBudgets).View.ClearDialog();
    }
    return enumerable ?? adapter.Get();
  }

  [PXUIField]
  [PXLookupButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable AddSelectedBudgetLines(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProgressWorksheet>) this.Document).Current.Hold.GetValueOrDefault())
    {
      ((PXSelectBase) this.Details).Cache.ForceExceptionHandling = true;
      HashSet<BudgetKeyTuple> existingCostBudgetLines = this.GetExistingCostBudgetLines();
      foreach (PMCostBudget budget in ((PXSelectBase) this.CostBudgets).Cache.Cached)
      {
        if (budget.Selected.GetValueOrDefault() && !existingCostBudgetLines.Contains(BudgetKeyTuple.Create((IProjectFilter) budget)))
          ((PXSelectBase<PMProgressWorksheetLine>) this.Details).Insert(new PMProgressWorksheetLine()
          {
            ProjectID = budget.ProjectID,
            TaskID = budget.ProjectTaskID,
            InventoryID = budget.InventoryID,
            AccountGroupID = budget.AccountGroupID,
            CostCodeID = budget.CostCodeID
          });
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Print(PXAdapter adapter)
  {
    this.OpenReport("PM657000", ((PXSelectBase<PMProgressWorksheet>) this.Document).Current);
    return adapter.Get();
  }

  public virtual void OpenReport(string reportID, PMProgressWorksheet doc)
  {
    if (doc != null && doc.ProjectID.HasValue)
    {
      string str = new NotificationUtility((PXGraph) this).SearchProjectReport(reportID, ((PXSelectBase<PMProject>) this.Project).Current.ContractID, ((PXSelectBase<PMProject>) this.Project).Current.DefaultBranchID);
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["ProjectID"] = ((PXSelectBase<PMProject>) this.Project).Current.ContractCD,
        ["EndDate"] = doc.Date.ToString()
      }, str, str, (CurrentLocalization) null);
    }
  }

  [PXUIField(DisplayName = "Correct")]
  [PXProcessButton]
  public IEnumerable Correct(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProgressWorksheet>) this.Document).Current != null)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CCorrect\u003Eb__32_0)));
    }
    return adapter.Get();
  }

  public virtual void CorrectDocument(PMProgressWorksheet doc)
  {
    doc.Hold = new bool?(true);
    doc.Approved = new bool?(false);
    doc.Rejected = new bool?(false);
    doc.Released = new bool?(false);
    doc.Status = "H";
    ((PXSelectBase<PMProgressWorksheet>) this.Document).Update(doc);
    ((PXAction) this.Save).Press();
  }

  [PXUIField(DisplayName = "Reverse")]
  [PXProcessButton]
  public IEnumerable Reverse(PXAdapter adapter)
  {
    this.ReverseDocument();
    return (IEnumerable) new PMProgressWorksheet[1]
    {
      ((PXSelectBase<PMProgressWorksheet>) this.Document).Current
    };
  }

  public virtual void ReverseDocument()
  {
    if (((PXSelectBase<PMProgressWorksheet>) this.Document).Current == null)
      return;
    ProgressWorksheetEntry instance = PXGraph.CreateInstance<ProgressWorksheetEntry>();
    ((PXGraph) instance).SelectTimeStamp();
    PMProgressWorksheet copy1 = (PMProgressWorksheet) ((PXSelectBase) this.Document).Cache.CreateCopy((object) ((PXSelectBase<PMProgressWorksheet>) this.Document).Current);
    List<PMProgressWorksheetLine> progressWorksheetLineList = new List<PMProgressWorksheetLine>();
    foreach (PXResult<PMProgressWorksheetLine> pxResult in ((PXSelectBase<PMProgressWorksheetLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMProgressWorksheetLine copy2 = (PMProgressWorksheetLine) ((PXSelectBase) this.Details).Cache.CreateCopy((object) PXResult<PMProgressWorksheetLine>.op_Implicit(pxResult));
      progressWorksheetLineList.Add(copy2);
    }
    copy1.RefNbr = (string) null;
    copy1.Released = new bool?(false);
    copy1.Hold = new bool?(true);
    copy1.Approved = new bool?(false);
    copy1.Rejected = new bool?(false);
    copy1.Status = "H";
    copy1.LineCntr = new int?(0);
    copy1.NoteID = new Guid?(Guid.NewGuid());
    PMProgressWorksheet progressWorksheet = ((PXSelectBase<PMProgressWorksheet>) instance.Document).Insert(copy1);
    foreach (PMProgressWorksheetLine progressWorksheetLine in progressWorksheetLineList)
    {
      progressWorksheetLine.RefNbr = progressWorksheet.RefNbr;
      progressWorksheetLine.Qty = new Decimal?(-progressWorksheetLine.Qty.GetValueOrDefault());
      progressWorksheetLine.NoteID = new Guid?();
      progressWorksheetLine.LineNbr = new int?();
      ((PXSelectBase<PMProgressWorksheetLine>) instance.Details).Insert(progressWorksheetLine);
    }
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 0);
  }

  [PXDBDate]
  [PXDefault(typeof (PMProgressWorksheet.date))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.docDate> e)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault(typeof (PMProgressWorksheet.description))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.descr> e)
  {
  }

  public virtual bool CanDeleteDocument(PMProgressWorksheet doc)
  {
    if (doc == null)
      return true;
    bool? nullable = doc.Released;
    if (nullable.GetValueOrDefault())
      return false;
    nullable = doc.Hold;
    if (nullable.GetValueOrDefault())
      return true;
    nullable = doc.Rejected;
    if (nullable.GetValueOrDefault())
      return false;
    nullable = doc.Approved;
    return nullable.GetValueOrDefault() && (!PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() || !((PXSelectBase<PMSetup>) this.Setup).Current.ProgressWorksheetApprovalMapID.HasValue);
  }

  public virtual bool IsProjectEnabled()
  {
    return !((PXSelectBase) this.Details).Cache.IsInsertedUpdatedDeleted && ((PXSelectBase<PMProgressWorksheetLine>) this.Details).Select(Array.Empty<object>()).Count <= 0;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.taskID> e)
  {
    if (e.Row == null)
      return;
    PMTask pmTask = (PMTask) PXSelectorAttribute.Select<PMProgressWorksheetLine.taskID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.taskID>>) e).Cache, e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.taskID>, object, object>) e).NewValue);
    PMProgressWorksheetLine row = (PMProgressWorksheetLine) e.Row;
    ProgressWorksheetEntry.CheckDublicateLine(((PXSelectBase) this.Details).Cache, GraphHelper.RowCast<PMProgressWorksheetLine>((IEnumerable) ((PXSelectBase<PMProgressWorksheetLine>) this.Details).Select(Array.Empty<object>())), row, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.taskID>, object, object>) e).NewValue, row.AccountGroupID, row.InventoryID, row.CostCodeID, pmTask?.TaskCD);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.accountGroupID> e)
  {
    if (e.Row == null)
      return;
    PMAccountGroup pmAccountGroup = (PMAccountGroup) PXSelectorAttribute.Select<PMProgressWorksheetLine.accountGroupID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.accountGroupID>>) e).Cache, e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.accountGroupID>, object, object>) e).NewValue);
    PMProgressWorksheetLine row = (PMProgressWorksheetLine) e.Row;
    ProgressWorksheetEntry.CheckDublicateLine(((PXSelectBase) this.Details).Cache, GraphHelper.RowCast<PMProgressWorksheetLine>((IEnumerable) ((PXSelectBase<PMProgressWorksheetLine>) this.Details).Select(Array.Empty<object>())), row, row.TaskID, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.accountGroupID>, object, object>) e).NewValue, row.InventoryID, row.CostCodeID, pmAccountGroup?.GroupCD);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.inventoryID> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<PMProgressWorksheetLine.inventoryID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.inventoryID>>) e).Cache, e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.inventoryID>, object, object>) e).NewValue);
    PMProgressWorksheetLine row = (PMProgressWorksheetLine) e.Row;
    ProgressWorksheetEntry.CheckDublicateLine(((PXSelectBase) this.Details).Cache, GraphHelper.RowCast<PMProgressWorksheetLine>((IEnumerable) ((PXSelectBase<PMProgressWorksheetLine>) this.Details).Select(Array.Empty<object>())), row, row.TaskID, row.AccountGroupID, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.inventoryID>, object, object>) e).NewValue, row.CostCodeID, inventoryItem?.InventoryCD);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.costCodeID> e)
  {
    if (e.Row == null)
      return;
    PMCostCode pmCostCode = (PMCostCode) PXSelectorAttribute.Select<PMProgressWorksheetLine.costCodeID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.costCodeID>>) e).Cache, e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.costCodeID>, object, object>) e).NewValue);
    PMProgressWorksheetLine row = (PMProgressWorksheetLine) e.Row;
    ProgressWorksheetEntry.CheckDublicateLine(((PXSelectBase) this.Details).Cache, GraphHelper.RowCast<PMProgressWorksheetLine>((IEnumerable) ((PXSelectBase<PMProgressWorksheetLine>) this.Details).Select(Array.Empty<object>())), row, row.TaskID, row.AccountGroupID, row.InventoryID, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProgressWorksheetLine.costCodeID>, object, object>) e).NewValue, pmCostCode?.CostCodeCD);
  }

  public static void CheckDublicateLine(
    PXCache cache,
    IEnumerable<PMProgressWorksheetLine> lines,
    PMProgressWorksheetLine row,
    int? taskID,
    int? accountGroupID,
    int? inventoryID,
    int? costCodeID,
    string newValueCD)
  {
    if (taskID.HasValue && accountGroupID.HasValue)
    {
      foreach (PMProgressWorksheetLine line in lines)
      {
        if (line != row)
        {
          int? nullable1 = line.TaskID;
          int? nullable2 = taskID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = line.AccountGroupID;
            nullable1 = accountGroupID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              nullable1 = line.InventoryID;
              nullable2 = inventoryID;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                nullable2 = line.CostCodeID;
                nullable1 = costCodeID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                  throw new PXSetPropertyException("A line with the same project budget key already exists.", (PXErrorLevel) 4)
                  {
                    ErrorValue = (object) newValueCD
                  };
              }
            }
          }
        }
      }
    }
    PXUIFieldAttribute.SetError<PMProgressWorksheetLine.accountGroupID>(cache, (object) row, (string) null);
  }

  public static void CheckCostBudgetLine(
    PXGraph graph,
    PXCache cache,
    PMProject project,
    PMProgressWorksheetLine line)
  {
    if (((PXSelectBase<PMCostBudget>) new PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Required<PMCostBudget.projectID>>, And<PMCostBudget.projectTaskID, Equal<Required<PMCostBudget.projectTaskID>>, And<PMCostBudget.accountGroupID, Equal<Required<PMCostBudget.accountGroupID>>, And<PMCostBudget.inventoryID, Equal<Required<PMCostBudget.inventoryID>>, And<PMCostBudget.costCodeID, Equal<Required<PMCostBudget.costCodeID>>, And<PMCostBudget.type, Equal<AccountType.expense>, And<PMCostBudget.productivityTracking, IsNotNull, And<PMCostBudget.productivityTracking, NotEqual<PMProductivityTrackingType.notAllowed>>>>>>>>>>(graph)).SelectSingle(new object[5]
    {
      (object) line.ProjectID,
      (object) line.TaskID,
      (object) line.AccountGroupID,
      (object) line.InventoryID,
      (object) line.CostCodeID
    }) == null)
    {
      PXUIFieldAttribute.SetError<PMProgressWorksheetLine.accountGroupID>(cache, (object) line, $"A line with the specified project budget key has not been found in the cost budget of the {project.ContractCD} project.", PMAccountGroup.PK.Find(graph, line.AccountGroupID)?.GroupCD);
      throw new PXException("A line with the specified project budget key has not been found in the cost budget of the {0} project.", new object[1]
      {
        (object) project.ContractCD
      });
    }
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<PMProgressWorksheet>) this.Document).Current != null)
    {
      if (((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.Status != "A")
      {
        PXUIFieldAttribute.SetError<PMProgressWorksheet.projectID>(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<PMProgressWorksheet>) this.Document).Current, $"The {((PXSelectBase<PMProject>) this.Project).Current.ContractCD} project is not active.", ((PXSelectBase<PMProject>) this.Project).Current.ContractCD);
        throw new PXException("The {0} project is not active.", new object[1]
        {
          (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD
        });
      }
      List<PMProgressWorksheetLine> list;
      try
      {
        this.blockLineSelectingEvent = true;
        list = GraphHelper.RowCast<PMProgressWorksheetLine>((IEnumerable) ((PXSelectBase<PMProgressWorksheetLine>) this.Details).Select(Array.Empty<object>())).ToList<PMProgressWorksheetLine>();
      }
      finally
      {
        this.blockLineSelectingEvent = false;
      }
      foreach (PMProgressWorksheetLine line in list)
      {
        int? nullable = line.AccountGroupID;
        if (nullable.HasValue)
        {
          PMAccountGroup pmAccountGroup = PXResultset<PMAccountGroup>.op_Implicit(PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.groupID, Equal<Required<PMAccountGroup.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) line.AccountGroupID
          }));
          bool? isActive = pmAccountGroup.IsActive;
          bool flag = false;
          if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
          {
            PXUIFieldAttribute.SetError<PMProgressWorksheetLine.accountGroupID>(((PXSelectBase) this.Details).Cache, (object) line, $"The {pmAccountGroup.GroupCD} account group is inactive. You can activate it on the Account Groups (PM201000) form.", pmAccountGroup.GroupCD);
            throw new PXException("The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new object[1]
            {
              (object) pmAccountGroup.GroupCD
            });
          }
        }
        nullable = line.TaskID;
        if (nullable.HasValue)
        {
          PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) line.ProjectID,
            (object) line.TaskID
          }));
          if (pmTask.Status == "C" || pmTask.Status == "F")
          {
            string str;
            new ProjectTaskStatus.ListAttribute().ValueLabelDic.TryGetValue(pmTask.Status, out str);
            PXUIFieldAttribute.SetError<PMProgressWorksheetLine.taskID>(((PXSelectBase) this.Details).Cache, (object) line, $"The {pmTask.TaskCD} project task has the {str} status and cannot be selected. Select another project task.", pmTask.TaskCD);
            throw new PXException("The {0} project task has the {1} status and cannot be selected. Select another project task.", new object[2]
            {
              (object) pmTask.TaskCD,
              (object) str
            });
          }
        }
        ProgressWorksheetEntry.CheckCostBudgetLine((PXGraph) this, ((PXSelectBase) this.Details).Cache, ((PXSelectBase<PMProject>) this.Project).Current, line);
      }
    }
    ((PXGraph) this).Persist();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProgressWorksheet> e)
  {
    if (e.Row == null)
      return;
    bool flag1 = this.CanDeleteDocument(e.Row);
    bool valueOrDefault = e.Row.Hold.GetValueOrDefault();
    bool flag2 = ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.Status == "A";
    ((PXSelectBase) this.Document).Cache.AllowDelete = flag1;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = valueOrDefault & flag2;
    ((PXSelectBase) this.Details).Cache.AllowDelete = valueOrDefault;
    ((PXSelectBase) this.Details).Cache.AllowInsert = valueOrDefault & flag2;
    ((PXAction) this.correct).SetEnabled(this.CanBeCorrected(e.Row));
    ((PXAction) this.loadTemplate).SetEnabled(valueOrDefault & flag2);
    ((PXAction) this.selectBudgetLines).SetEnabled(valueOrDefault & flag2);
    PXUIFieldAttribute.SetEnabled<PMProgressWorksheet.date>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProgressWorksheet>>) e).Cache, (object) e.Row, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<PMProgressWorksheet.description>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProgressWorksheet>>) e).Cache, (object) e.Row, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<PMProgressWorksheet.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProgressWorksheet>>) e).Cache, (object) e.Row, valueOrDefault && this.IsProjectEnabled());
    PXUIFieldAttribute.SetVisible<PMProgressWorksheetLine.inventoryID>(((PXSelectBase) this.Details).Cache, (object) null, this.IsInventoryVisible());
    PXUIFieldAttribute.SetVisible<PMProgressWorksheetLine.costCodeID>(((PXSelectBase) this.Details).Cache, (object) null, this.IsCostCodeVisible());
    if (PXUIFieldAttribute.GetError<PMProgressWorksheet.projectID>(((PXSelectBase) this.Document).Cache, (object) e.Row) != null || !(((PXSelectBase<PMProgressWorksheet>) this.Document).Current.Status != "C") || !(((PXSelectBase<PMProgressWorksheet>) this.Document).Current.Status != "R") || ((PXSelectBase<PMProject>) this.Project).Current == null || !(((PXSelectBase<PMProject>) this.Project).Current.Status != "A"))
      return;
    PXUIFieldAttribute.SetError<PMProgressWorksheet.projectID>(((PXSelectBase) this.Document).Cache, (object) e.Row, $"The {((PXSelectBase<PMProject>) this.Project).Current.ContractCD} project is not active.", ((PXSelectBase<PMProject>) this.Project).Current.ContractCD);
  }

  public virtual bool CanBeCorrected(PMProgressWorksheet row) => row.Released.GetValueOrDefault();

  protected virtual void _(PX.Data.Events.RowSelecting<PMProgressWorksheetLine> e)
  {
    if (this.blockLineSelectingEvent)
      return;
    this.CalculateQuantities(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<PMProgressWorksheetLine>>) e).Cache, e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProgressWorksheetLine.taskID> e)
  {
    this.CalculateQuantities(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProgressWorksheetLine.taskID>>) e).Cache, (PMProgressWorksheetLine) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProgressWorksheetLine.accountGroupID> e)
  {
    this.CalculateQuantities(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProgressWorksheetLine.accountGroupID>>) e).Cache, (PMProgressWorksheetLine) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProgressWorksheetLine.inventoryID> e)
  {
    this.CalculateQuantities(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProgressWorksheetLine.inventoryID>>) e).Cache, (PMProgressWorksheetLine) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProgressWorksheetLine.costCodeID> e)
  {
    this.CalculateQuantities(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProgressWorksheetLine.costCodeID>>) e).Cache, (PMProgressWorksheetLine) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMProgressWorksheetLine.qty> e)
  {
    this.CalculateQuantities(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProgressWorksheetLine.qty>>) e).Cache, (PMProgressWorksheetLine) e.Row);
  }

  protected virtual void CalculateQuantities(PXCache cache, PMProgressWorksheetLine line)
  {
    try
    {
      this.blockLineSelectingEvent = true;
      if (((PXSelectBase<PMProgressWorksheet>) this.Document).Current == null)
        return;
      ProgressWorksheetEntry.CalculateQuantities((PXGraph) this, cache, ((PXSelectBase<PMProgressWorksheet>) this.Document).Current.Date.Value, ((PXSelectBase<PMProgressWorksheet>) this.Document).Current.Status, line, ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<PMProgressWorksheet>) this.Document).Current) == 2, ((PXSelectBase<PMSetup>) this.Setup).Current, ((PXSelectBase<PMProject>) this.Project).Current);
    }
    finally
    {
      this.blockLineSelectingEvent = false;
    }
  }

  public static void CalculateQuantities(
    PXGraph graph,
    PXCache cache,
    DateTime docDate,
    string docStatus,
    PMProgressWorksheetLine calculatedLine,
    bool newRefNbr,
    PMSetup setup,
    PMProject project)
  {
    if (calculatedLine != null && calculatedLine.TaskID.HasValue && calculatedLine.AccountGroupID.HasValue && calculatedLine.InventoryID.HasValue && calculatedLine.CostCodeID.HasValue)
    {
      IFinPeriodRepository periodRepository = ((Func<PXGraph, IFinPeriodRepository>) ((IServiceProvider) ServiceLocator.Current).GetService(typeof (Func<PXGraph, IFinPeriodRepository>)))(graph);
      int? parentOrganizationId = PXAccess.GetParentOrganizationID(graph.Accessinfo.BranchID);
      DateTime dateTime1 = docDate;
      FinPeriod finPeriodByDate1 = periodRepository.GetFinPeriodByDate(new DateTime?(dateTime1), parentOrganizationId);
      FinPeriod finPeriod = (FinPeriod) null;
      if (finPeriodByDate1 != null)
        finPeriod = periodRepository.FindPrevPeriod(parentOrganizationId, finPeriodByDate1.FinPeriodID);
      using (new PXConnectionScope())
      {
        PMCostBudget pmCostBudget = ((PXSelectBase<PMCostBudget>) new PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Required<PMCostBudget.projectID>>, And<PMCostBudget.projectTaskID, Equal<Required<PMCostBudget.projectTaskID>>, And<PMCostBudget.accountGroupID, Equal<Required<PMCostBudget.accountGroupID>>, And<PMCostBudget.inventoryID, Equal<Required<PMCostBudget.inventoryID>>, And<PMCostBudget.costCodeID, Equal<Required<PMCostBudget.costCodeID>>, And<PMCostBudget.type, Equal<AccountType.expense>>>>>>>>(graph)).SelectSingle(new object[5]
        {
          (object) calculatedLine.ProjectID,
          (object) calculatedLine.TaskID,
          (object) calculatedLine.AccountGroupID,
          (object) calculatedLine.InventoryID,
          (object) calculatedLine.CostCodeID
        });
        List<object> objectList = new List<object>()
        {
          (object) calculatedLine.ProjectID,
          (object) calculatedLine.TaskID,
          (object) calculatedLine.AccountGroupID,
          (object) calculatedLine.InventoryID,
          (object) calculatedLine.CostCodeID
        };
        PXSelectJoin<PMProgressWorksheetLine, InnerJoin<PMProgressWorksheet, On<PMProgressWorksheetLine.refNbr, Equal<PMProgressWorksheet.refNbr>>>, Where<PMProgressWorksheetLine.projectID, Equal<Required<PMProgressWorksheetLine.projectID>>, And<PMProgressWorksheetLine.taskID, Equal<Required<PMProgressWorksheetLine.taskID>>, And<PMProgressWorksheetLine.accountGroupID, Equal<Required<PMProgressWorksheetLine.accountGroupID>>, And<PMProgressWorksheetLine.inventoryID, Equal<Required<PMProgressWorksheetLine.inventoryID>>, And<PMProgressWorksheetLine.costCodeID, Equal<Required<PMProgressWorksheetLine.costCodeID>>, And<PMProgressWorksheet.status, Equal<ProgressWorksheetStatus.closed>>>>>>>> pxSelectJoin = new PXSelectJoin<PMProgressWorksheetLine, InnerJoin<PMProgressWorksheet, On<PMProgressWorksheetLine.refNbr, Equal<PMProgressWorksheet.refNbr>>>, Where<PMProgressWorksheetLine.projectID, Equal<Required<PMProgressWorksheetLine.projectID>>, And<PMProgressWorksheetLine.taskID, Equal<Required<PMProgressWorksheetLine.taskID>>, And<PMProgressWorksheetLine.accountGroupID, Equal<Required<PMProgressWorksheetLine.accountGroupID>>, And<PMProgressWorksheetLine.inventoryID, Equal<Required<PMProgressWorksheetLine.inventoryID>>, And<PMProgressWorksheetLine.costCodeID, Equal<Required<PMProgressWorksheetLine.costCodeID>>, And<PMProgressWorksheet.status, Equal<ProgressWorksheetStatus.closed>>>>>>>>(graph);
        if (!newRefNbr)
        {
          ((PXSelectBase<PMProgressWorksheetLine>) pxSelectJoin).WhereAnd(typeof (Where<PMProgressWorksheet.refNbr, NotEqual<Required<PMProgressWorksheet.refNbr>>>));
          objectList.Add((object) calculatedLine.RefNbr);
        }
        Decimal num1 = 0M;
        Decimal num2 = 0M;
        Decimal num3 = 0M;
        Decimal num4 = 0M;
        foreach (PXResult<PMProgressWorksheetLine, PMProgressWorksheet> pxResult in ((PXSelectBase<PMProgressWorksheetLine>) pxSelectJoin).Select(objectList.ToArray()))
        {
          PMProgressWorksheet progressWorksheet = PXResult<PMProgressWorksheetLine, PMProgressWorksheet>.op_Implicit(pxResult);
          PMProgressWorksheetLine progressWorksheetLine = PXResult<PMProgressWorksheetLine, PMProgressWorksheet>.op_Implicit(pxResult);
          if (progressWorksheetLine.RefNbr != calculatedLine.RefNbr)
          {
            FinPeriod finPeriodByDate2 = periodRepository.GetFinPeriodByDate(progressWorksheet.Date, parentOrganizationId);
            DateTime? date1 = progressWorksheet.Date;
            DateTime dateTime2 = dateTime1;
            Decimal? qty;
            if ((date1.HasValue ? (date1.GetValueOrDefault() <= dateTime2 ? 1 : 0) : 0) != 0)
            {
              Decimal num5 = num1;
              qty = progressWorksheetLine.Qty;
              Decimal num6 = qty.Value;
              num1 = num5 + num6;
            }
            if (finPeriodByDate2 != null && finPeriod != null && finPeriodByDate2.FinPeriodID == finPeriod.FinPeriodID)
            {
              Decimal num7 = num2;
              qty = progressWorksheetLine.Qty;
              Decimal num8 = qty.Value;
              num2 = num7 + num8;
            }
            if (finPeriodByDate2 != null && finPeriodByDate1 != null && finPeriodByDate2.FinPeriodID == finPeriodByDate1.FinPeriodID)
            {
              DateTime? date2 = progressWorksheet.Date;
              DateTime dateTime3 = dateTime1;
              if ((date2.HasValue ? (date2.GetValueOrDefault() <= dateTime3 ? 1 : 0) : 0) != 0)
              {
                Decimal num9 = num3;
                qty = progressWorksheetLine.Qty;
                Decimal num10 = qty.Value;
                num3 = num9 + num10;
              }
            }
          }
        }
        ((PXSelectBase) pxSelectJoin).View.Clear();
        Decimal? nullable;
        if (docStatus == "C")
        {
          Decimal num11 = num3;
          nullable = calculatedLine.Qty;
          Decimal num12 = nullable ?? 0M;
          num3 = num11 + num12;
        }
        Decimal num13 = num1;
        nullable = calculatedLine.Qty;
        Decimal num14 = nullable ?? 0M;
        Decimal num15 = num13 + num14;
        if (pmCostBudget != null)
        {
          nullable = pmCostBudget.RevisedQty;
          Decimal num16 = 0M;
          if (!(nullable.GetValueOrDefault() == num16 & nullable.HasValue) && (pmCostBudget.ProductivityTracking != "N" || docStatus == "C"))
          {
            Decimal num17 = 100.0M * num15;
            nullable = pmCostBudget.RevisedQty;
            Decimal num18 = nullable.Value;
            num4 = Math.Round(num17 / num18, 2);
          }
        }
        cache.SetValue<PMProgressWorksheetLine.previouslyCompletedQuantity>((object) calculatedLine, (object) num1);
        cache.SetValue<PMProgressWorksheetLine.priorPeriodQuantity>((object) calculatedLine, (object) num2);
        cache.SetValue<PMProgressWorksheetLine.currentPeriodQuantity>((object) calculatedLine, (object) num3);
        cache.SetValue<PMProgressWorksheetLine.totalCompletedQuantity>((object) calculatedLine, (object) num15);
        cache.SetValue<PMProgressWorksheetLine.completedPercentTotalQuantity>((object) calculatedLine, (object) num4);
        if (pmCostBudget == null)
        {
          if (docStatus == "C")
          {
            string str = (string) null;
            if (project.CostBudgetLevel == "C" || project.CostBudgetLevel == "D")
              str = PMCostCode.PK.Find(graph, calculatedLine.CostCodeID).Description;
            else if (project.CostBudgetLevel == "I")
              str = PX.Objects.IN.InventoryItem.PK.Find(graph, calculatedLine.InventoryID).Descr;
            cache.SetValue<PMProgressWorksheetLine.description>((object) calculatedLine, (object) str);
            cache.SetValue<PMProgressWorksheetLine.uOM>((object) calculatedLine, (object) setup.EmptyItemUOM);
            cache.SetValue<PMProgressWorksheetLine.totalBudgetedQuantity>((object) calculatedLine, (object) 0.0M);
          }
          else
          {
            cache.SetValue<PMProgressWorksheetLine.description>((object) calculatedLine, (object) null);
            cache.SetValue<PMProgressWorksheetLine.uOM>((object) calculatedLine, (object) null);
            cache.SetValue<PMProgressWorksheetLine.totalBudgetedQuantity>((object) calculatedLine, (object) 0.0M);
          }
        }
        else if (pmCostBudget.ProductivityTracking == "N")
        {
          if (docStatus == "C")
          {
            cache.SetValue<PMProgressWorksheetLine.description>((object) calculatedLine, (object) pmCostBudget.Description);
            cache.SetValue<PMProgressWorksheetLine.uOM>((object) calculatedLine, (object) pmCostBudget.UOM);
            cache.SetValue<PMProgressWorksheetLine.totalBudgetedQuantity>((object) calculatedLine, (object) pmCostBudget.RevisedQty);
          }
          else
          {
            cache.SetValue<PMProgressWorksheetLine.description>((object) calculatedLine, (object) null);
            cache.SetValue<PMProgressWorksheetLine.uOM>((object) calculatedLine, (object) null);
            cache.SetValue<PMProgressWorksheetLine.totalBudgetedQuantity>((object) calculatedLine, (object) 0.0M);
          }
        }
        else
        {
          cache.SetValue<PMProgressWorksheetLine.description>((object) calculatedLine, (object) pmCostBudget.Description);
          cache.SetValue<PMProgressWorksheetLine.uOM>((object) calculatedLine, (object) pmCostBudget.UOM);
          cache.SetValue<PMProgressWorksheetLine.totalBudgetedQuantity>((object) calculatedLine, (object) pmCostBudget.RevisedQty);
        }
      }
    }
    else
    {
      if (calculatedLine == null)
        return;
      cache.SetValue<PMProgressWorksheetLine.previouslyCompletedQuantity>((object) calculatedLine, (object) 0.0M);
      cache.SetValue<PMProgressWorksheetLine.priorPeriodQuantity>((object) calculatedLine, (object) 0.0M);
      cache.SetValue<PMProgressWorksheetLine.currentPeriodQuantity>((object) calculatedLine, (object) 0.0M);
      cache.SetValue<PMProgressWorksheetLine.totalCompletedQuantity>((object) calculatedLine, (object) 0.0M);
      cache.SetValue<PMProgressWorksheetLine.completedPercentTotalQuantity>((object) calculatedLine, (object) 0.0M);
      cache.SetValue<PMProgressWorksheetLine.description>((object) calculatedLine, (object) null);
      cache.SetValue<PMProgressWorksheetLine.uOM>((object) calculatedLine, (object) null);
      cache.SetValue<PMProgressWorksheetLine.totalBudgetedQuantity>((object) calculatedLine, (object) 0.0M);
    }
  }

  protected virtual bool IsInventoryVisible()
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return false;
    return ((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "I" || ((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "D";
  }

  protected virtual bool IsCostCodeVisible()
  {
    return ((PXSelectBase<PMProject>) this.Project).Current != null && (((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "C" || ((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "D") && PXAccess.FeatureInstalled<FeaturesSet.costCodes>();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProgressWorksheetLine, PMProgressWorksheetLine.inventoryID> e)
  {
    if (e.Row == null || e.Row.InventoryID.HasValue || this.IsInventoryVisible())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProgressWorksheetLine, PMProgressWorksheetLine.inventoryID>, PMProgressWorksheetLine, object>) e).NewValue = (object) PMInventorySelectorAttribute.EmptyInventoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProgressWorksheetLine, PMProgressWorksheetLine.costCodeID> e)
  {
    if (e.Row == null || e.Row.CostCodeID.HasValue || this.IsCostCodeVisible())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProgressWorksheetLine, PMProgressWorksheetLine.costCodeID>, PMProgressWorksheetLine, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  [PXCacheName("Cost Budget Line Filter")]
  [Serializable]
  public class CostBudgetLineFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the Cost Budget Line Filter.
    /// </summary>
    /// <value>
    /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
    /// </value>
    [PXInt]
    public virtual int? ProjectID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the cost budget line filter.
    /// </summary>
    /// <value>
    /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
    /// </value>
    [ActiveOrInPlanningProjectTask(typeof (ProgressWorksheetEntry.CostBudgetLineFilter.projectID), CheckMandatoryCondition = typeof (Where<True, Equal<False>>), DisplayName = "Project Task")]
    public virtual int? TaskID { get; set; }

    /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> associated with the Cost Budget Line Filter.</summary>
    /// <value>
    /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
    /// </value>
    [PXUIField(DisplayName = "Account Group")]
    [PXInt]
    [PXSelector(typeof (FbqlSelect<SelectFromBase<PMAccountGroup, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMCostBudget>.On<BqlOperand<PMAccountGroup.groupID, IBqlInt>.IsEqual<PMCostBudget.accountGroupID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostBudget.projectID, Equal<BqlField<ProgressWorksheetEntry.CostBudgetLineFilter.projectID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMCostBudget.type, IBqlString>.IsEqual<AccountType.expense>>>, And<BqlOperand<PMCostBudget.accountGroupID, IBqlInt>.IsNotNull>>>.And<BqlOperand<PMAccountGroup.isExpense, IBqlBool>.IsEqual<True>>>.Aggregate<To<GroupBy<PMCostBudget.accountGroupID>>>, PMAccountGroup>.SearchFor<PMAccountGroup.groupID>), SubstituteKey = typeof (PMAccountGroup.groupCD), DescriptionField = typeof (PMAccountGroup.description))]
    public virtual int? AccountGroupID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the Cost Budget Line Filter.
    /// </summary>
    /// <value>
    /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
    /// </value>
    [PXUIField(DisplayName = "Inventory ID")]
    [PXInt]
    [PXSelector(typeof (FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMCostBudget>.On<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<PMCostBudget.inventoryID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostBudget.projectID, Equal<BqlField<ProgressWorksheetEntry.CostBudgetLineFilter.projectID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMCostBudget.type, IBqlString>.IsEqual<AccountType.expense>>>>.And<BqlOperand<PMCostBudget.inventoryID, IBqlInt>.IsNotNull>>.Aggregate<To<GroupBy<PMCostBudget.inventoryID>>>, PX.Objects.IN.InventoryItem>.SearchFor<PX.Objects.IN.InventoryItem.inventoryID>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
    public virtual int? InventoryID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the Cost Budget Line Filter.
    /// </summary>
    [PXUIField(DisplayName = "Cost Code From", FieldClass = "COSTCODE")]
    [PXInt]
    [PXDimensionSelector("COSTCODE", typeof (FbqlSelect<SelectFromBase<PMCostCode, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMCostBudget>.On<BqlOperand<PMCostCode.costCodeID, IBqlInt>.IsEqual<PMCostBudget.costCodeID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostBudget.projectID, Equal<BqlField<ProgressWorksheetEntry.CostBudgetLineFilter.projectID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMCostBudget.type, IBqlString>.IsEqual<AccountType.expense>>>>.And<BqlOperand<PMCostBudget.costCodeID, IBqlInt>.IsNotNull>>.Aggregate<To<GroupBy<PMCostBudget.costCodeID>>>, PMCostCode>.SearchFor<PMCostCode.costCodeID>), typeof (PMCostCode.costCodeCD))]
    public virtual int? CostCodeFrom { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the Cost Budget Line Filter.
    /// </summary>
    [PXUIField(DisplayName = "Cost Code To", FieldClass = "COSTCODE")]
    [PXInt]
    [PXDimensionSelector("COSTCODE", typeof (FbqlSelect<SelectFromBase<PMCostCode, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMCostBudget>.On<BqlOperand<PMCostCode.costCodeID, IBqlInt>.IsEqual<PMCostBudget.costCodeID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostBudget.projectID, Equal<BqlField<ProgressWorksheetEntry.CostBudgetLineFilter.projectID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMCostBudget.type, IBqlString>.IsEqual<AccountType.expense>>>>.And<BqlOperand<PMCostBudget.costCodeID, IBqlInt>.IsNotNull>>.Aggregate<To<GroupBy<PMCostBudget.costCodeID>>>, PMCostCode>.SearchFor<PMCostCode.costCodeID>), typeof (PMCostCode.costCodeCD))]
    public virtual int? CostCodeTo { get; set; }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProgressWorksheetEntry.CostBudgetLineFilter.projectID>
    {
    }

    public abstract class taskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProgressWorksheetEntry.CostBudgetLineFilter.taskID>
    {
    }

    public abstract class accountGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProgressWorksheetEntry.CostBudgetLineFilter.accountGroupID>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProgressWorksheetEntry.CostBudgetLineFilter.inventoryID>
    {
    }

    public abstract class costCodeFrom : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProgressWorksheetEntry.CostBudgetLineFilter.costCodeFrom>
    {
    }

    public abstract class costCodeTo : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProgressWorksheetEntry.CostBudgetLineFilter.costCodeTo>
    {
    }
  }
}
