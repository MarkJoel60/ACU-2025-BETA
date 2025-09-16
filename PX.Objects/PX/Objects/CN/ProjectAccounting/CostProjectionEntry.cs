// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.CostProjectionEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#nullable enable
namespace PX.Objects.CN.ProjectAccounting;

public class CostProjectionEntry : 
  PXGraph<
  #nullable disable
  CostProjectionEntry, PMCostProjection>,
  PXImportAttribute.IPXPrepareItems
{
  [PXViewName("Cost Projection")]
  public FbqlSelect<SelectFromBase<PMCostProjection, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProject>.On<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  PMCostProjection.projectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.contractID, 
  #nullable disable
  IsNull>>>>.Or<MatchUserFor<PMProject>>>, PMCostProjection>.View Document;
  public PXSelect<PMCostProjection, Where<PMCostProjection.projectID, Equal<Current<PMCostProjection.projectID>>, And<PMCostProjection.revisionID, Equal<Current<PMCostProjection.revisionID>>>>> DocumentSettings;
  [PXViewName("Project")]
  public PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMCostProjection.projectID>>>> Project;
  [PXViewName("Cost Projection Class")]
  public PXSelect<PMCostProjectionClass, Where<PMCostProjectionClass.classID, Equal<Current<PMCostProjection.classID>>>> Class;
  [PXViewName("Cost Projection Detail")]
  [PXImport(typeof (PMCostProjection))]
  public PXSelect<PMCostProjectionLine, Where<PMCostProjectionLine.projectID, Equal<Current<PMCostProjection.projectID>>, And<PMCostProjectionLine.revisionID, Equal<Current<PMCostProjection.revisionID>>>>> Details;
  [PXViewName("Cost Projection History")]
  public PXSelectJoin<PMCostProjectionLine, InnerJoin<PMCostProjection, On<PMCostProjectionLine.projectID, Equal<PMCostProjection.projectID>, And<PMCostProjectionLine.revisionID, Equal<PMCostProjection.revisionID>>>>, Where<PMCostProjectionLine.projectID, Equal<Current<PMCostProjectionLine.projectID>>, And<PMCostProjectionLine.taskID, Equal<Current<PMCostProjectionLine.taskID>>, And<PMCostProjectionLine.accountGroupID, Equal<Current<PMCostProjectionLine.accountGroupID>>, And<PMCostProjectionLine.inventoryID, Equal<Current<PMCostProjectionLine.inventoryID>>, And<PMCostProjectionLine.costCodeID, Equal<Current<PMCostProjectionLine.costCodeID>>, And<PMCostProjection.classID, Equal<Current<PMCostProjection.classID>>>>>>>>> History;
  public PXSelectJoinGroupBy<PMCostProjectionLine, InnerJoin<PMCostProjection, On<PMCostProjectionLine.projectID, Equal<PMCostProjection.projectID>>>, Where<PMCostProjectionLine.projectID, Equal<Current<PMCostProjection.projectID>>, And<PMCostProjection.released, Equal<True>>>, Aggregate<GroupBy<PMCostProjectionLine.projectID, GroupBy<PMCostProjectionLine.taskID, GroupBy<PMCostProjectionLine.accountGroupID, GroupBy<PMCostProjectionLine.inventoryID, GroupBy<PMCostProjectionLine.costCodeID>>>>>>> ReleasedDetails;
  public PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Current<PMCostProjection.projectID>>, And<PMCostBudget.type, Equal<AccountType.expense>>>> CostBudget;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<PMSetup> Setup;
  public PXFilter<CostProjectionEntry.PMCostProjectionCopyDialogInfo> CopyDialog;
  [PXCopyPasteHiddenView]
  [PXViewName("Approval")]
  public EPApprovalAutomation<PMCostProjection, PMCostProjection.approved, PMCostProjection.rejected, PMCostProjection.hold, PMSetupCostProjectionApproval> Approval;
  [PXViewName("Project")]
  public PXSelect<PMForecastProject, Where<PMForecastProject.contractID, Equal<Current<PMCostProjection.projectID>>>> ProjectTotals;
  protected string budgetRecordsKey;
  protected Dictionary<BudgetKeyTuple, PMBudgetRecord> budgetRecords;
  public PXAction<PMCostProjection> createRevision;
  public PXAction<PMCostProjection> viewCostCommitments;
  public PXAction<PMCostProjection> viewCostTransactions;
  public PXSelect<PMBudgetRecord> AvailableCostBudget;
  public PXAction<PMCostProjection> addCostBudget;
  public PXAction<PMCostProjection> appendSelectedCostBudget;
  public PXAction<PMCostProjection> showHistory;
  public PXAction<PMCostProjection> refreshBudget;
  public PXAction<PMCostProjection> release;
  public PXAction<PMCostProjection> hold;
  public PXAction<PMCostProjection> removeHold;
  public PXAction<PMCostProjection> costProjectionReport;
  private Dictionary<BudgetKeyTuple, PMCostProjectionLine> lines;

  [PXDBDate]
  [PXDefault(typeof (PMCostProjection.date))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.docDate> e)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault(typeof (PMCostProjection.description))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.descr> e)
  {
  }

  public virtual IEnumerable projectTotals()
  {
    CostProjectionEntry costProjectionEntry = this;
    PXSelect<PMForecastProject, Where<PMForecastProject.contractID, Equal<Current<PMCostProjection.projectID>>>> pxSelect = new PXSelect<PMForecastProject, Where<PMForecastProject.contractID, Equal<Current<PMCostProjection.projectID>>>>((PXGraph) costProjectionEntry);
    PMCostBudget[] costBudget = GraphHelper.RowCast<PMCostBudget>((IEnumerable) ((PXSelectBase<PMCostBudget>) costProjectionEntry.CostBudget).Select(Array.Empty<object>())).ToArray<PMCostBudget>();
    // ISSUE: reference to a compiler-generated method
    BudgetKeyTuple[] detailsKey = GraphHelper.RowCast<PMCostProjectionLine>((IEnumerable) ((PXSelectBase<PMCostProjectionLine>) costProjectionEntry.Details).Select(Array.Empty<object>())).Select<PMCostProjectionLine, BudgetKeyTuple>(new Func<PMCostProjectionLine, BudgetKeyTuple>(costProjectionEntry.\u003CprojectTotals\u003Eb__15_0)).ToArray<BudgetKeyTuple>();
    // ISSUE: reference to a compiler-generated method
    BudgetKeyTuple[] releasedDetailsKey = GraphHelper.RowCast<PMCostProjectionLine>((IEnumerable) ((PXSelectBase<PMCostProjectionLine>) costProjectionEntry.ReleasedDetails).Select(Array.Empty<object>())).Select<PMCostProjectionLine, BudgetKeyTuple>(new Func<PMCostProjectionLine, BudgetKeyTuple>(costProjectionEntry.\u003CprojectTotals\u003Eb__15_1)).ToArray<BudgetKeyTuple>();
    foreach (PMForecastProject pmForecastProject1 in GraphHelper.RowCast<PMForecastProject>((IEnumerable) ((PXSelectBase<PMForecastProject>) pxSelect).Select(Array.Empty<object>())))
    {
      pmForecastProject1.TotalBudgetedVarianceAmount = new Decimal?(0M);
      Decimal? nullable1;
      Decimal? nullable2;
      foreach (PMCostBudget budget in costBudget)
      {
        BudgetKeyTuple budgetKeyTuple = BudgetKeyTuple.Create((IProjectFilter) budget);
        if (!((IEnumerable<BudgetKeyTuple>) detailsKey).Contains<BudgetKeyTuple>(budgetKeyTuple) && ((IEnumerable<BudgetKeyTuple>) releasedDetailsKey).Contains<BudgetKeyTuple>(budgetKeyTuple))
        {
          PMForecastProject pmForecastProject2 = pmForecastProject1;
          nullable1 = pmForecastProject2.TotalBudgetedVarianceAmount;
          nullable2 = budget.CuryCostProjectionCostAtCompletion;
          Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
          nullable2 = budget.CuryRevisedAmount;
          Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
          Decimal num = valueOrDefault1 - valueOrDefault2;
          Decimal? nullable3;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable3 = nullable2;
          }
          else
            nullable3 = new Decimal?(nullable1.GetValueOrDefault() + num);
          pmForecastProject2.TotalBudgetedVarianceAmount = nullable3;
        }
      }
      PMForecastProject pmForecastProject3 = pmForecastProject1;
      nullable1 = pmForecastProject3.TotalBudgetedVarianceAmount;
      nullable2 = ((PXSelectBase<PMCostProjection>) costProjectionEntry.Document).Current.TotalVarianceAmount;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      Decimal? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault3);
      pmForecastProject3.TotalBudgetedVarianceAmount = nullable4;
      PMForecastProject pmForecastProject4 = pmForecastProject1;
      nullable1 = pmForecastProject1.TotalBudgetedGrossProfit;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      nullable1 = pmForecastProject1.TotalBudgetedVarianceAmount;
      Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
      Decimal? nullable5 = new Decimal?(valueOrDefault4 - valueOrDefault5);
      pmForecastProject4.TotalProjectedGrossProfit = nullable5;
      yield return (object) pmForecastProject1;
    }
    ((PXSelectBase) costProjectionEntry.Project).Cache.IsDirty = false;
  }

  private string ImportFromExcelActionName => $"{"Details"}$ImportAction";

  private string GetRecordID(BudgetKeyTuple key)
  {
    return $"{key.ProjectID}.{key.ProjectTaskID}.{key.AccountGroupID}.{key.CostCodeID}.{key.InventoryID}";
  }

  public CostProjectionEntry() => ((PXAction) this.CopyPaste).SetVisible(false);

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable CreateRevision(PXAdapter adapter)
  {
    if (((PXSelectBase<PMCostProjection>) this.Document).Current != null)
      this.VerifyAndRaiseExceptionIfBudgetIncompatible(((PXSelectBase<PMCostProjection>) this.Document).Current.ClassID, true);
    ((PXAction) this.Save).Press();
    if (((PXSelectBase) this.CopyDialog).View.Answer == null)
    {
      ((PXSelectBase) this.CopyDialog).Cache.Clear();
      ((PXSelectBase) this.CopyDialog).Cache.Insert();
    }
    if (((PXSelectBase<CostProjectionEntry.PMCostProjectionCopyDialogInfo>) this.CopyDialog).AskExt() != 1 || string.IsNullOrEmpty(((PXSelectBase<CostProjectionEntry.PMCostProjectionCopyDialogInfo>) this.CopyDialog).Current.RevisionID) || ((PXSelectBase<PMCostProjection>) this.Document).Current == null)
      return adapter.Get();
    this.CreateNewProjection(((PXSelectBase<PMCostProjection>) this.Document).Current, ((PXSelectBase<CostProjectionEntry.PMCostProjectionCopyDialogInfo>) this.CopyDialog).Current);
    return adapter.Get();
  }

  protected virtual void CreateNewProjection(
    PMCostProjection original,
    CostProjectionEntry.PMCostProjectionCopyDialogInfo info)
  {
    PMCostProjection pmCostProjection1 = new PMCostProjection();
    pmCostProjection1.ProjectID = original.ProjectID;
    pmCostProjection1.ClassID = original.ClassID;
    pmCostProjection1.Description = original.Description;
    pmCostProjection1.RevisionID = info.RevisionID;
    CostProjectionEntry instance = PXGraph.CreateInstance<CostProjectionEntry>();
    ((PXGraph) instance).Clear();
    ((PXGraph) instance).SelectTimeStamp();
    PMCostProjection pmCostProjection2 = ((PXSelectBase<PMCostProjection>) instance.Document).Insert(pmCostProjection1);
    if (info.CopyNotes.GetValueOrDefault())
    {
      string note = PXNoteAttribute.GetNote(((PXSelectBase) this.Document).Cache, (object) original);
      PXNoteAttribute.SetNote(((PXSelectBase) instance.Document).Cache, (object) pmCostProjection2, note);
    }
    if (info.CopyFiles.GetValueOrDefault())
    {
      Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Document).Cache, (object) original);
      PXNoteAttribute.SetFileNotes(((PXSelectBase) instance.Document).Cache, (object) pmCostProjection2, fileNotes);
    }
    foreach (PXResult<PMCostProjectionLine> pxResult in ((PXSelectBase<PMCostProjectionLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMCostProjectionLine row = PXResult<PMCostProjectionLine>.op_Implicit(pxResult);
      if (this.GetBudgetRecord(row) != null)
      {
        PMCostProjectionLine copy = (PMCostProjectionLine) ((PXSelectBase) this.Details).Cache.CreateCopy((object) row);
        copy.RevisionID = pmCostProjection2.RevisionID;
        copy.NoteID = new Guid?();
        copy.Mode = "M";
        PMCostProjectionLine costProjectionLine = ((PXSelectBase<PMCostProjectionLine>) instance.Details).Insert(copy);
        costProjectionLine.Mode = row.Mode;
        bool? nullable = info.CopyNotes;
        if (nullable.GetValueOrDefault())
        {
          string note = PXNoteAttribute.GetNote(((PXSelectBase) this.Details).Cache, (object) row);
          PXNoteAttribute.SetNote(((PXSelectBase) instance.Details).Cache, (object) costProjectionLine, note);
        }
        nullable = info.CopyFiles;
        if (nullable.GetValueOrDefault())
        {
          Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Details).Cache, (object) row);
          PXNoteAttribute.SetFileNotes(((PXSelectBase) instance.Details).Cache, (object) costProjectionLine, fileNotes);
        }
      }
    }
    if (info.RefreshBudget.GetValueOrDefault())
      ((PXAction) instance.refreshBudget).Press();
    ((PXAction) instance.Save).Press();
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 0);
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry", DisplayOnMainToolbar = false)]
  public IEnumerable ViewCostCommitments(PXAdapter adapter)
  {
    if (((PXSelectBase<PMCostProjectionLine>) this.Details).Current != null)
    {
      CommitmentInquiry instance = PXGraph.CreateInstance<CommitmentInquiry>();
      ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.ProjectID;
      ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.ProjectTaskID = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.TaskID;
      ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.AccountGroupID = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.AccountGroupID;
      int? costCodeId = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.CostCodeID;
      int? defaultCostCode = CostCodeAttribute.DefaultCostCode;
      if (!(costCodeId.GetValueOrDefault() == defaultCostCode.GetValueOrDefault() & costCodeId.HasValue == defaultCostCode.HasValue))
        ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.CostCode = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.CostCodeID;
      int? inventoryId = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue))
        ((PXSelectBase<CommitmentInquiry.ProjectBalanceFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.InventoryID;
      ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Inquiry", DisplayOnMainToolbar = false)]
  public virtual IEnumerable ViewCostTransactions(PXAdapter adapter)
  {
    if (((PXSelectBase<PMCostProjectionLine>) this.Details).Current != null)
    {
      TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.ProjectID;
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectTaskID = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.TaskID;
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.AccountGroupID = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.AccountGroupID;
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.IncludeUnreleased = new bool?(false);
      int? costCodeId = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.CostCodeID;
      int? defaultCostCode = CostCodeAttribute.DefaultCostCode;
      if (!(costCodeId.GetValueOrDefault() == defaultCostCode.GetValueOrDefault() & costCodeId.HasValue == defaultCostCode.HasValue))
        ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.CostCode = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.CostCodeID;
      int? inventoryId = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue))
        ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<PMCostProjectionLine>) this.Details).Current.InventoryID;
      ProjectAccountingService.NavigateToView((PXGraph) instance, "transactions", "Transactions Inquiry - View Transactions", DataViewHelper.DataViewFilter.Create("Date", (PXCondition) 5, (object) ((PXSelectBase<PMCostProjection>) this.Document).Current.Date));
    }
    return adapter.Get();
  }

  protected virtual void OverrideProjectedProperties(
    PMCostProjectionLine target,
    IDictionary<BudgetKeyTuple, PMCostBudget> budget)
  {
    if (target == null)
      throw new ArgumentNullException(nameof (target));
    if (budget == null)
      throw new ArgumentNullException(nameof (budget));
    PMCostBudget source;
    if (budget.Count == 0 || !budget.TryGetValue(this.GetBudgetKey(target), out source))
      return;
    this.OverrideProjectedProperties((PMBudget) source, target);
  }

  protected virtual void OverrideProjectedProperties(PMBudget source, PMCostProjectionLine target)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (target == null)
      throw new ArgumentNullException(nameof (target));
    target.Amount = source.CuryCostProjectionCostToComplete;
    target.Quantity = source.CostProjectionQtyToComplete;
    target.ProjectedAmount = source.CuryCostProjectionCostAtCompletion;
    target.ProjectedQuantity = source.CostProjectionQtyAtCompletion;
    ((PXSelectBase<PMCostProjectionLine>) this.Details).Update(target);
  }

  protected virtual HashSet<BudgetKeyTuple> GetReleasedCostProjectionLineTuples()
  {
    return GraphHelper.RowCast<PMCostProjectionLine>((IEnumerable) ((PXSelectBase<PMCostProjectionLine>) new PXSelectJoin<PMCostProjectionLine, InnerJoin<PMCostProjection, On<PMCostProjectionLine.projectID, Equal<PMCostProjection.projectID>, And<PMCostProjectionLine.revisionID, Equal<PMCostProjection.revisionID>>>>, Where<PMCostProjection.projectID, Equal<Current<PMCostProjection.projectID>>, And<PMCostProjection.classID, Equal<Current<PMCostProjection.classID>>, And<PMCostProjection.released, Equal<True>>>>>((PXGraph) this)).Select(Array.Empty<object>())).Select<PMCostProjectionLine, BudgetKeyTuple>(new Func<PMCostProjectionLine, BudgetKeyTuple>(this.GetBudgetKey)).Distinct<BudgetKeyTuple>().ToHashSet<BudgetKeyTuple>();
  }

  protected virtual IDictionary<BudgetKeyTuple, PMCostBudget> BuildReleasedCostBudgetDictionary()
  {
    HashSet<BudgetKeyTuple> releasedLinesHashSet = this.GetReleasedCostProjectionLineTuples();
    return (IDictionary<BudgetKeyTuple, PMCostBudget>) GraphHelper.RowCast<PMCostBudget>((IEnumerable) ((PXSelectBase<PMCostBudget>) this.CostBudget).Select(Array.Empty<object>())).Select<PMCostBudget, KeyValuePair<BudgetKeyTuple, PMCostBudget>>((Func<PMCostBudget, KeyValuePair<BudgetKeyTuple, PMCostBudget>>) (budget => new KeyValuePair<BudgetKeyTuple, PMCostBudget>(BudgetKeyTuple.Create((IProjectFilter) budget), budget))).Where<KeyValuePair<BudgetKeyTuple, PMCostBudget>>((Func<KeyValuePair<BudgetKeyTuple, PMCostBudget>, bool>) (pair => releasedLinesHashSet.Contains(pair.Key))).ToDictionary<KeyValuePair<BudgetKeyTuple, PMCostBudget>, BudgetKeyTuple, PMCostBudget>((Func<KeyValuePair<BudgetKeyTuple, PMCostBudget>, BudgetKeyTuple>) (key => key.Key), (Func<KeyValuePair<BudgetKeyTuple, PMCostBudget>, PMCostBudget>) (value => value.Value));
  }

  protected Dictionary<BudgetKeyTuple, PMCostProjectionLine> GetProjectionLines()
  {
    Dictionary<BudgetKeyTuple, PMCostProjectionLine> projectionLines = new Dictionary<BudgetKeyTuple, PMCostProjectionLine>();
    foreach (PXResult<PMCostProjectionLine> pxResult in ((PXSelectBase<PMCostProjectionLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMCostProjectionLine costProjectionLine = PXResult<PMCostProjectionLine>.op_Implicit(pxResult);
      if (!projectionLines.ContainsKey(this.GetBudgetKey(costProjectionLine)))
        projectionLines.Add(this.GetBudgetKey(costProjectionLine), costProjectionLine);
      else
        PXTrace.WriteError("Projection lines contain duplicates for the same Budget key.");
    }
    return projectionLines;
  }

  public virtual IEnumerable availableCostBudget()
  {
    Dictionary<BudgetKeyTuple, PMCostProjectionLine> existing = this.GetProjectionLines();
    bool found = false;
    foreach (PMBudgetRecord pmBudgetRecord in ((PXSelectBase) this.AvailableCostBudget).Cache.Inserted)
    {
      found = true;
      yield return (object) pmBudgetRecord;
    }
    if (!found)
    {
      if (((PXSelectBase<PMCostProjection>) this.Document).Current != null)
      {
        foreach (PMBudgetRecord pmBudgetRecord in this.GetCostBudget(((PXSelectBase<PMCostProjection>) this.Document).Current).Values)
        {
          pmBudgetRecord.Selected = new bool?(existing.ContainsKey(this.GetBudgetKey(pmBudgetRecord)));
          if (((PXSelectBase<PMBudgetRecord>) this.AvailableCostBudget).Locate(pmBudgetRecord) == null)
            yield return (object) ((PXSelectBase<PMBudgetRecord>) this.AvailableCostBudget).Insert(pmBudgetRecord);
        }
      }
      ((PXSelectBase) this.AvailableCostBudget).Cache.IsDirty = false;
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

  public virtual void AddSelectedCostBudget()
  {
    Dictionary<BudgetKeyTuple, PMCostProjectionLine> projectionLines = this.GetProjectionLines();
    foreach (PMBudgetRecord pmBudgetRecord in ((PXSelectBase) this.AvailableCostBudget).Cache.Cached)
    {
      if (pmBudgetRecord.Selected.GetValueOrDefault() && !projectionLines.ContainsKey(this.GetBudgetKey(pmBudgetRecord)))
        ((PXSelectBase<PMCostProjectionLine>) this.Details).Insert(new PMCostProjectionLine()
        {
          ProjectID = pmBudgetRecord.ProjectID,
          TaskID = pmBudgetRecord.ProjectTaskID,
          AccountGroupID = pmBudgetRecord.AccountGroupID,
          InventoryID = pmBudgetRecord.InventoryID,
          CostCodeID = pmBudgetRecord.CostCodeID
        });
    }
  }

  [PXUIField(DisplayName = "History")]
  [PXButton]
  public IEnumerable ShowHistory(PXAdapter adapter)
  {
    ((PXSelectBase) this.History).View.AskExt();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Refresh and Recalculate")]
  [PXButton]
  public virtual IEnumerable RefreshBudget(PXAdapter adapter)
  {
    IDictionary<BudgetKeyTuple, PMCostBudget> budget = this.BuildReleasedCostBudgetDictionary();
    foreach (PXResult<PMCostProjectionLine> pxResult in ((PXSelectBase<PMCostProjectionLine>) this.Details).Select(Array.Empty<object>()))
    {
      PMCostProjectionLine row = PXResult<PMCostProjectionLine>.op_Implicit(pxResult);
      PMBudgetRecord budgetRecord = this.GetBudgetRecord(row);
      if (budgetRecord != null)
      {
        this.InitFieldsFromBudget(row, budgetRecord);
        if (row.Mode != "M")
        {
          row.CompletedPct = new Decimal?(this.GetCompletedPct(budgetRecord, row.Mode));
          this.RecalculateFromCompletedPct(row);
        }
        this.OverrideProjectedProperties(((PXSelectBase<PMCostProjectionLine>) this.Details).Update(row), budget);
      }
      else
        ((PXSelectBase<PMCostProjectionLine>) this.Details).Delete(row);
    }
    ((PXSelectBase<PMCostProjection>) this.Document).Update(((PXSelectBase<PMCostProjection>) this.Document).Current);
    ((PXAction) this.Save).Press();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Release")]
  [PXButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    if (((PXSelectBase<PMCostProjection>) this.Document).Current != null)
    {
      PMCostProjectionClass projectionClass = PXResultset<PMCostProjectionClass>.op_Implicit(((PXSelectBase<PMCostProjectionClass>) this.Class).Select(Array.Empty<object>()));
      if (projectionClass != null)
      {
        this.VerifyAndRaiseExceptionIfBudgetIncompatible(projectionClass, (System.Action) (() =>
        {
          throw new PXException("The cost projection cannot be released because the {0} cost projection class is not compatible with the structure of the project budget.", new object[1]
          {
            (object) projectionClass.Description
          });
        }), false);
        if (!projectionClass.AccountGroupID.GetValueOrDefault())
          throw new PXException("The projection is not compatible with the structure of the project budget.");
      }
      Dictionary<BudgetKeyTuple, PMCostProjectionLine> projectionLines = this.GetProjectionLines();
      foreach (PXResult<PMCostBudget> pxResult in ((PXSelectBase<PMCostBudget>) this.CostBudget).Select(Array.Empty<object>()))
      {
        PMCostBudget budget = PXResult<PMCostBudget>.op_Implicit(pxResult);
        PMCostProjectionLine costProjectionLine;
        if (projectionLines.TryGetValue(BudgetKeyTuple.Create((IProjectFilter) budget), out costProjectionLine))
        {
          budget.CostProjectionCompletedPct = costProjectionLine.CompletedPct;
          budget.CostProjectionQtyToComplete = costProjectionLine.Quantity;
          budget.CostProjectionQtyAtCompletion = costProjectionLine.ProjectedQuantity;
          budget.CuryCostProjectionCostToComplete = costProjectionLine.Amount;
          budget.CuryCostProjectionCostAtCompletion = costProjectionLine.ProjectedAmount;
          ((PXSelectBase<PMCostBudget>) this.CostBudget).Update(budget);
        }
      }
      ((PXSelectBase<PMCostProjection>) this.Document).Current.Released = new bool?(true);
      ((PXSelectBase<PMCostProjection>) this.Document).Update(((PXSelectBase<PMCostProjection>) this.Document).Current);
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable Hold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable RemoveHold(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable CostProjectionReport(PXAdapter adapter)
  {
    if (((PXSelectBase<PMCostProjection>) this.Document).Current != null)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["LvlOfDet"] = "A";
      dictionary["ProjectID"] = PMProject.PK.Find((PXGraph) this, ((PXSelectBase<PMCostProjection>) this.Document).Current.ProjectID).ContractCD;
      FinPeriod finPeriodByDate = ((PXGraph) this).GetService<IFinPeriodRepository>().GetFinPeriodByDate(((PXGraph) this).Accessinfo.BusinessDate, new int?(0));
      string str = finPeriodByDate.PeriodNbr + finPeriodByDate.FinYear;
      dictionary["From_Date"] = str;
      dictionary["To_Date"] = str;
      dictionary["ActToPeriod"] = str;
      dictionary["PCostEst"] = "P";
      dictionary["IncludePot"] = "False";
      throw new PXReportRequiredException(dictionary, "PM652500", "PM652500", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMCostProjection> e)
  {
    PMCostProjectionClass costProjectionClass = PXResultset<PMCostProjectionClass>.op_Implicit(((PXSelectBase<PMCostProjectionClass>) this.Class).Select(Array.Empty<object>()));
    if (costProjectionClass != null)
    {
      this.SetVisibility<PMCostProjectionLine.accountGroupID>(((PXSelectBase) this.Details).Cache, costProjectionClass.AccountGroupID);
      this.SetVisibility<PMBudgetRecord.accountGroupID>(((PXSelectBase) this.AvailableCostBudget).Cache, costProjectionClass.AccountGroupID);
      this.SetVisibility<PMCostProjectionLine.taskID>(((PXSelectBase) this.Details).Cache, costProjectionClass.TaskID);
      this.SetVisibility<PMBudgetRecord.projectTaskID>(((PXSelectBase) this.AvailableCostBudget).Cache, costProjectionClass.TaskID);
      this.SetVisibility<PMCostProjectionLine.costCodeID>(((PXSelectBase) this.Details).Cache, costProjectionClass.CostCodeID);
      this.SetVisibility<PMBudgetRecord.costCodeID>(((PXSelectBase) this.AvailableCostBudget).Cache, costProjectionClass.CostCodeID);
      this.SetVisibility<PMCostProjectionLine.inventoryID>(((PXSelectBase) this.Details).Cache, costProjectionClass.InventoryID);
      this.SetVisibility<PMBudgetRecord.inventoryID>(((PXSelectBase) this.AvailableCostBudget).Cache, costProjectionClass.InventoryID);
    }
    PMCostProjection row = e.Row;
    bool flag = row != null && row.Hold.GetValueOrDefault();
    ((PXSelectBase) this.Document).Cache.AllowDelete = flag;
    ((PXSelectBase) this.Details).Cache.AllowInsert = flag;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.Details).Cache.AllowDelete = flag;
    ((PXAction) this.addCostBudget).SetEnabled(flag && !string.IsNullOrEmpty(e.Row.ClassID));
    if (((OrderedDictionary) ((PXGraph) this).Actions).Contains((object) this.ImportFromExcelActionName))
      ((PXGraph) this).Actions[this.ImportFromExcelActionName].SetEnabled(flag && !string.IsNullOrEmpty(e.Row.ClassID));
    PXUIFieldAttribute.SetEnabled<PMCostProjection.classID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjection>>) e).Cache, (object) e.Row, flag && !string.IsNullOrEmpty(e.Row.RevisionID));
    PXUIFieldAttribute.SetEnabled<PMCostProjection.description>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjection>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<PMCostProjection.date>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostProjection>>) e).Cache, (object) e.Row, flag);
  }

  private void SetVisibility<Field>(PXCache cache, bool? value) where Field : IBqlField
  {
    PXUIFieldAttribute.SetVisible<Field>(cache, (object) null, value.GetValueOrDefault());
    PXUIFieldAttribute.SetVisibility<Field>(cache, (object) null, value.GetValueOrDefault() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMCostProjectionLine> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMCostProjectionLine.taskID>(((PXSelectBase) this.Details).Cache, (object) e.Row, ((PXGraph) this).IsImportFromExcel);
    PXUIFieldAttribute.SetEnabled<PMCostProjectionLine.accountGroupID>(((PXSelectBase) this.Details).Cache, (object) e.Row, ((PXGraph) this).IsImportFromExcel);
    PXUIFieldAttribute.SetEnabled<PMCostProjectionLine.inventoryID>(((PXSelectBase) this.Details).Cache, (object) e.Row, ((PXGraph) this).IsImportFromExcel);
    PXUIFieldAttribute.SetEnabled<PMCostProjectionLine.costCodeID>(((PXSelectBase) this.Details).Cache, (object) e.Row, ((PXGraph) this).IsImportFromExcel);
    PXUIFieldAttribute.SetEnabled<PMCostProjectionLine.quantity>(((PXSelectBase) this.Details).Cache, (object) e.Row, !string.IsNullOrEmpty(e.Row.UOM));
    PXUIFieldAttribute.SetEnabled<PMCostProjectionLine.projectedQuantity>(((PXSelectBase) this.Details).Cache, (object) e.Row, !string.IsNullOrEmpty(e.Row.UOM));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMCostProjection, PMCostProjection.classID> e)
  {
    if ((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjection, PMCostProjection.classID>, PMCostProjection, object>) e).NewValue != e.Row.ClassID)
      this.VerifyAndRaiseExceptionIfRowsExists();
    this.VerifyAndRaiseExceptionIfBudgetIncompatible((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMCostProjection, PMCostProjection.classID>, PMCostProjection, object>) e).NewValue, true);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMCostProjection> e)
  {
    if (!(e.Row.ClassID != e.OldRow.ClassID))
      return;
    ((PXSelectBase) this.AvailableCostBudget).Cache.Clear();
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMBudgetRecord> e) => e.Cancel = true;

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionLine, PMCostProjectionLine.quantity> e)
  {
    if (e.Row.Mode == "M" || e.Row.Mode == "Q")
      return;
    PMCostProjectionLine row1 = e.Row;
    Decimal? nullable1 = e.Row.Quantity;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.CompletedQuantity;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    row1.ProjectedQuantity = nullable2;
    PMCostProjectionLine row2 = e.Row;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.BudgetedQuantity;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(valueOrDefault3 - valueOrDefault4);
    row2.VarianceQuantity = nullable3;
    nullable1 = e.Row.ProjectedQuantity;
    if (!(nullable1.GetValueOrDefault() != 0M))
      return;
    nullable1 = e.Row.CompletedQuantity;
    Decimal num1 = nullable1.GetValueOrDefault() * 100M;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    Decimal num2 = num1 / valueOrDefault5;
    e.Row.CompletedPct = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(num2)));
    if (!(e.Row.Mode != "C"))
      return;
    if (num2 != 0M)
    {
      PMCostProjectionLine row3 = e.Row;
      nullable1 = e.Row.CompletedAmount;
      Decimal? nullable4 = new Decimal?(Math.Round(nullable1.GetValueOrDefault() * 100M / num2, 2));
      row3.ProjectedAmount = nullable4;
    }
    else
    {
      PMCostProjectionLine row4 = e.Row;
      nullable1 = e.Row.BudgetedAmount;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      nullable1 = e.Row.CompletedAmount;
      Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
      Decimal? nullable5 = new Decimal?(Math.Max(valueOrDefault6, valueOrDefault7));
      row4.ProjectedAmount = nullable5;
    }
    PMCostProjectionLine row5 = e.Row;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.CompletedAmount;
    Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(Math.Max(0M, valueOrDefault8 - valueOrDefault9));
    row5.Amount = nullable6;
    PMCostProjectionLine row6 = e.Row;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.BudgetedAmount;
    Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
    Decimal? nullable7 = new Decimal?(valueOrDefault10 - valueOrDefault11);
    row6.VarianceAmount = nullable7;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionLine, PMCostProjectionLine.projectedQuantity> e)
  {
    if (e.Row.Mode == "M" || e.Row.Mode == "Q")
      return;
    PMCostProjectionLine row1 = e.Row;
    Decimal valueOrDefault1 = e.Row.ProjectedQuantity.GetValueOrDefault();
    Decimal? nullable1 = e.Row.CompletedQuantity;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(Math.Max(0M, valueOrDefault1 - valueOrDefault2));
    row1.Quantity = nullable2;
    PMCostProjectionLine row2 = e.Row;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.BudgetedQuantity;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(valueOrDefault3 - valueOrDefault4);
    row2.VarianceQuantity = nullable3;
    nullable1 = e.Row.ProjectedQuantity;
    if (!(nullable1.GetValueOrDefault() != 0M))
      return;
    nullable1 = e.Row.CompletedQuantity;
    Decimal num1 = nullable1.GetValueOrDefault() * 100M;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    Decimal num2 = num1 / valueOrDefault5;
    e.Row.CompletedPct = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(num2)));
    if (!(e.Row.Mode != "C"))
      return;
    if (num2 != 0M)
    {
      PMCostProjectionLine row3 = e.Row;
      nullable1 = e.Row.CompletedAmount;
      Decimal val1 = Math.Round(nullable1.GetValueOrDefault() * 100M / num2);
      nullable1 = e.Row.CompletedAmount;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      Decimal? nullable4 = new Decimal?(Math.Max(val1, valueOrDefault6));
      row3.ProjectedAmount = nullable4;
    }
    else
    {
      PMCostProjectionLine row4 = e.Row;
      nullable1 = e.Row.BudgetedAmount;
      Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
      nullable1 = e.Row.CompletedAmount;
      Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
      Decimal? nullable5 = new Decimal?(Math.Max(valueOrDefault7, valueOrDefault8));
      row4.ProjectedAmount = nullable5;
    }
    PMCostProjectionLine row5 = e.Row;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.CompletedAmount;
    Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(Math.Max(0M, valueOrDefault9 - valueOrDefault10));
    row5.Amount = nullable6;
    PMCostProjectionLine row6 = e.Row;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.BudgetedAmount;
    Decimal valueOrDefault12 = nullable1.GetValueOrDefault();
    Decimal? nullable7 = new Decimal?(valueOrDefault11 - valueOrDefault12);
    row6.VarianceAmount = nullable7;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionLine, PMCostProjectionLine.varianceQuantity> e)
  {
    if (e.Row.Mode == "M" || e.Row.Mode == "Q")
      return;
    PMCostProjectionLine row1 = e.Row;
    Decimal? nullable1 = e.Row.BudgetedQuantity;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.VarianceQuantity;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    row1.ProjectedQuantity = nullable2;
    PMCostProjectionLine row2 = e.Row;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.CompletedQuantity;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(Math.Max(0M, valueOrDefault3 - valueOrDefault4));
    row2.Quantity = nullable3;
    nullable1 = e.Row.ProjectedQuantity;
    if (!(nullable1.GetValueOrDefault() != 0M))
      return;
    nullable1 = e.Row.CompletedQuantity;
    Decimal num1 = nullable1.GetValueOrDefault() * 100M;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    Decimal num2 = num1 / valueOrDefault5;
    e.Row.CompletedPct = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(num2)));
    if (!(e.Row.Mode != "C"))
      return;
    if (num2 != 0M)
    {
      PMCostProjectionLine row3 = e.Row;
      nullable1 = e.Row.CompletedAmount;
      Decimal val1 = Math.Round(nullable1.GetValueOrDefault() * 100M / num2);
      nullable1 = e.Row.CompletedAmount;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      Decimal? nullable4 = new Decimal?(Math.Max(val1, valueOrDefault6));
      row3.ProjectedAmount = nullable4;
    }
    else
    {
      PMCostProjectionLine row4 = e.Row;
      nullable1 = e.Row.BudgetedAmount;
      Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
      nullable1 = e.Row.CompletedAmount;
      Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
      Decimal? nullable5 = new Decimal?(Math.Max(valueOrDefault7, valueOrDefault8));
      row4.ProjectedAmount = nullable5;
    }
    PMCostProjectionLine row5 = e.Row;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.CompletedAmount;
    Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(Math.Max(0M, valueOrDefault9 - valueOrDefault10));
    row5.Amount = nullable6;
    PMCostProjectionLine row6 = e.Row;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.BudgetedAmount;
    Decimal valueOrDefault12 = nullable1.GetValueOrDefault();
    Decimal? nullable7 = new Decimal?(valueOrDefault11 - valueOrDefault12);
    row6.VarianceAmount = nullable7;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionLine, PMCostProjectionLine.amount> e)
  {
    if (e.Row.Mode == "M" || e.Row.Mode == "C")
      return;
    PMCostProjectionLine row1 = e.Row;
    Decimal? nullable1 = e.Row.Amount;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.CompletedAmount;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    row1.ProjectedAmount = nullable2;
    PMCostProjectionLine row2 = e.Row;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.BudgetedAmount;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(valueOrDefault3 - valueOrDefault4);
    row2.VarianceAmount = nullable3;
    nullable1 = e.Row.ProjectedAmount;
    if (!(nullable1.GetValueOrDefault() != 0M))
      return;
    nullable1 = e.Row.CompletedAmount;
    Decimal num1 = nullable1.GetValueOrDefault() * 100M;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    Decimal num2 = num1 / valueOrDefault5;
    e.Row.CompletedPct = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(num2)));
    if (!(e.Row.Mode != "Q"))
      return;
    if (num2 != 0M)
    {
      PMCostProjectionLine row3 = e.Row;
      nullable1 = e.Row.CompletedQuantity;
      Decimal? nullable4 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable1.GetValueOrDefault() * 100M / num2)));
      row3.ProjectedQuantity = nullable4;
    }
    else
    {
      PMCostProjectionLine row4 = e.Row;
      nullable1 = e.Row.BudgetedQuantity;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      nullable1 = e.Row.CompletedQuantity;
      Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
      Decimal? nullable5 = new Decimal?(Math.Max(valueOrDefault6, valueOrDefault7));
      row4.ProjectedQuantity = nullable5;
    }
    PMCostProjectionLine row5 = e.Row;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.CompletedQuantity;
    Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(Math.Max(0M, valueOrDefault8 - valueOrDefault9));
    row5.Quantity = nullable6;
    PMCostProjectionLine row6 = e.Row;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.BudgetedQuantity;
    Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
    Decimal? nullable7 = new Decimal?(valueOrDefault10 - valueOrDefault11);
    row6.VarianceQuantity = nullable7;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionLine, PMCostProjectionLine.projectedAmount> e)
  {
    if (e.Row.Mode == "M" || e.Row.Mode == "C")
      return;
    PMCostProjectionLine row1 = e.Row;
    Decimal valueOrDefault1 = e.Row.ProjectedAmount.GetValueOrDefault();
    Decimal? nullable1 = e.Row.CompletedAmount;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(Math.Max(0M, valueOrDefault1 - valueOrDefault2));
    row1.Amount = nullable2;
    PMCostProjectionLine row2 = e.Row;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.BudgetedAmount;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(valueOrDefault3 - valueOrDefault4);
    row2.VarianceAmount = nullable3;
    nullable1 = e.Row.ProjectedAmount;
    if (!(nullable1.GetValueOrDefault() != 0M))
      return;
    nullable1 = e.Row.CompletedAmount;
    Decimal num1 = nullable1.GetValueOrDefault() * 100M;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    Decimal num2 = num1 / valueOrDefault5;
    e.Row.CompletedPct = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(num2)));
    if (!(e.Row.Mode != "Q"))
      return;
    if (num2 != 0M)
    {
      PMCostProjectionLine row3 = e.Row;
      nullable1 = e.Row.CompletedQuantity;
      Decimal? nullable4 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable1.GetValueOrDefault() * 100M / num2)));
      row3.ProjectedQuantity = nullable4;
    }
    else
    {
      PMCostProjectionLine row4 = e.Row;
      nullable1 = e.Row.BudgetedQuantity;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      nullable1 = e.Row.CompletedQuantity;
      Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
      Decimal? nullable5 = new Decimal?(Math.Max(valueOrDefault6, valueOrDefault7));
      row4.ProjectedQuantity = nullable5;
    }
    PMCostProjectionLine row5 = e.Row;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.CompletedQuantity;
    Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(Math.Max(0M, valueOrDefault8 - valueOrDefault9));
    row5.Quantity = nullable6;
    PMCostProjectionLine row6 = e.Row;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.BudgetedQuantity;
    Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
    Decimal? nullable7 = new Decimal?(valueOrDefault10 - valueOrDefault11);
    row6.VarianceQuantity = nullable7;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionLine, PMCostProjectionLine.varianceAmount> e)
  {
    if (e.Row.Mode == "M" || e.Row.Mode == "C")
      return;
    PMCostProjectionLine row1 = e.Row;
    Decimal? nullable1 = e.Row.BudgetedAmount;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.VarianceAmount;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    row1.ProjectedAmount = nullable2;
    PMCostProjectionLine row2 = e.Row;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.CompletedAmount;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(Math.Max(0M, valueOrDefault3 - valueOrDefault4));
    row2.Amount = nullable3;
    nullable1 = e.Row.ProjectedAmount;
    if (!(nullable1.GetValueOrDefault() != 0M))
      return;
    nullable1 = e.Row.CompletedAmount;
    Decimal num1 = nullable1.GetValueOrDefault() * 100M;
    nullable1 = e.Row.ProjectedAmount;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    Decimal num2 = num1 / valueOrDefault5;
    e.Row.CompletedPct = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(num2)));
    if (!(e.Row.Mode != "Q"))
      return;
    if (num2 != 0M)
    {
      PMCostProjectionLine row3 = e.Row;
      nullable1 = e.Row.CompletedQuantity;
      Decimal? nullable4 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable1.GetValueOrDefault() * 100M / num2)));
      row3.ProjectedQuantity = nullable4;
    }
    else
    {
      PMCostProjectionLine row4 = e.Row;
      nullable1 = e.Row.BudgetedQuantity;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      nullable1 = e.Row.CompletedQuantity;
      Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
      Decimal? nullable5 = new Decimal?(Math.Max(valueOrDefault6, valueOrDefault7));
      row4.ProjectedQuantity = nullable5;
    }
    PMCostProjectionLine row5 = e.Row;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.CompletedQuantity;
    Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(Math.Max(0M, valueOrDefault8 - valueOrDefault9));
    row5.Quantity = nullable6;
    PMCostProjectionLine row6 = e.Row;
    nullable1 = e.Row.ProjectedQuantity;
    Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
    nullable1 = e.Row.BudgetedQuantity;
    Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
    Decimal? nullable7 = new Decimal?(valueOrDefault10 - valueOrDefault11);
    row6.VarianceQuantity = nullable7;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionLine, PMCostProjectionLine.completedPct> e)
  {
    if (!(e.Row.Mode != "M"))
      return;
    this.RecalculateFromCompletedPct(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostProjectionLine, PMCostProjectionLine.mode> e)
  {
    if (!(e.Row.Mode != "M"))
      return;
    e.Row.CompletedPct = new Decimal?(this.GetCompletedPct(e.Row));
    this.RecalculateFromCompletedPct(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PMCostProjectionLine> e)
  {
    if (e.Row.Mode != "M" || ((PXGraph) this).IsImportFromExcel)
    {
      PMBudgetRecord budgetRecord = this.GetBudgetRecord(e.Row);
      if (budgetRecord != null)
      {
        if (e.Row.Mode != "M")
          e.Row.CompletedPct = new Decimal?(this.GetCompletedPct(budgetRecord, e.Row.Mode));
        e.Row.UOM = budgetRecord.UOM;
        e.Row.Description = budgetRecord.Description;
        this.InitFieldsFromBudget(e.Row, budgetRecord);
      }
    }
    ((PXSelectBase<PMCostProjection>) this.Document).Update(((PXSelectBase<PMCostProjection>) this.Document).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CostProjectionEntry.PMCostProjectionCopyDialogInfo, CostProjectionEntry.PMCostProjectionCopyDialogInfo.revisionID> e)
  {
    if (PXResultset<PMCostProjection>.op_Implicit(((PXSelectBase<PMCostProjection>) new PXSelect<PMCostProjection, Where<PMCostProjection.projectID, Equal<Current<PMCostProjection.projectID>>, And<PMCostProjection.revisionID, Equal<Required<PMCostProjection.revisionID>>>>>((PXGraph) this)).Select(new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CostProjectionEntry.PMCostProjectionCopyDialogInfo, CostProjectionEntry.PMCostProjectionCopyDialogInfo.revisionID>, CostProjectionEntry.PMCostProjectionCopyDialogInfo, object>) e).NewValue
    })) != null)
      throw new PXSetPropertyException<PMCostProjection.revisionID>("Duplicate ID");
  }

  protected virtual void InitFieldsFromBudget(PMCostProjectionLine row, PMBudgetRecord budget)
  {
    if (budget == null)
      return;
    row.BudgetedQuantity = budget.RevisedQty;
    row.BudgetedAmount = budget.CuryRevisedAmount;
    row.ActualQuantity = budget.ActualQty;
    row.ActualAmount = budget.CuryActualAmount;
    row.UnbilledQuantity = budget.CommittedOpenQty;
    row.UnbilledAmount = budget.CuryCommittedOpenAmount;
    Decimal? nullable1;
    if (row.Mode != "M" && row.Mode != "C")
    {
      PMCostProjectionLine costProjectionLine1 = row;
      Decimal valueOrDefault1 = budget.CuryRevisedAmount.GetValueOrDefault();
      nullable1 = row.CompletedAmount;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal? nullable2 = new Decimal?(Math.Max(valueOrDefault1, valueOrDefault2));
      costProjectionLine1.ProjectedAmount = nullable2;
      PMCostProjectionLine costProjectionLine2 = row;
      nullable1 = budget.CuryRevisedAmount;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      nullable1 = row.CompletedAmount;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      Decimal? nullable3 = new Decimal?(Math.Max(0M, valueOrDefault3 - valueOrDefault4));
      costProjectionLine2.Amount = nullable3;
    }
    if (!(row.Mode != "M") || !(row.Mode != "Q"))
      return;
    PMCostProjectionLine costProjectionLine3 = row;
    nullable1 = budget.RevisedQty;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    nullable1 = row.CompletedQuantity;
    Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
    Decimal? nullable4 = new Decimal?(Math.Max(valueOrDefault5, valueOrDefault6));
    costProjectionLine3.ProjectedQuantity = nullable4;
    PMCostProjectionLine costProjectionLine4 = row;
    nullable1 = budget.RevisedQty;
    Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
    nullable1 = row.CompletedQuantity;
    Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
    Decimal? nullable5 = new Decimal?(Math.Max(0M, valueOrDefault7 - valueOrDefault8));
    costProjectionLine4.Quantity = nullable5;
  }

  protected virtual void RecalculateFromCompletedPct(PMCostProjectionLine row)
  {
    Decimal completedPct1 = this.GetCompletedPct(row);
    Decimal num1 = completedPct1;
    Decimal? completedPct2 = row.CompletedPct;
    Decimal valueOrDefault1 = completedPct2.GetValueOrDefault();
    Decimal? nullable1;
    if (!(num1 == valueOrDefault1 & completedPct2.HasValue))
    {
      nullable1 = row.CompletedPct;
      if (nullable1.GetValueOrDefault() != 0M)
      {
        Decimal num2;
        Decimal num3;
        if (completedPct1 != 0M)
        {
          nullable1 = row.BudgetedAmount;
          Decimal num4 = nullable1.GetValueOrDefault() * completedPct1;
          nullable1 = row.CompletedPct;
          Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
          num2 = Math.Round(num4 / valueOrDefault2, 2);
          nullable1 = row.BudgetedQuantity;
          Decimal num5 = nullable1.GetValueOrDefault() * completedPct1;
          nullable1 = row.CompletedPct;
          Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
          num3 = PXDBQuantityAttribute.Round(new Decimal?(num5 / valueOrDefault3));
        }
        else
        {
          nullable1 = row.BudgetedAmount;
          num2 = nullable1.GetValueOrDefault();
          nullable1 = row.BudgetedQuantity;
          num3 = nullable1.GetValueOrDefault();
        }
        if (row.Mode != "Q")
        {
          PMCostProjectionLine costProjectionLine1 = row;
          Decimal val1 = num3;
          nullable1 = row.CompletedQuantity;
          Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
          Decimal? nullable2 = new Decimal?(Math.Max(val1, valueOrDefault4));
          costProjectionLine1.ProjectedQuantity = nullable2;
          PMCostProjectionLine costProjectionLine2 = row;
          nullable1 = row.ProjectedQuantity;
          Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
          nullable1 = row.CompletedQuantity;
          Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
          Decimal? nullable3 = new Decimal?(Math.Max(0M, valueOrDefault5 - valueOrDefault6));
          costProjectionLine2.Quantity = nullable3;
          PMCostProjectionLine costProjectionLine3 = row;
          nullable1 = row.ProjectedQuantity;
          Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
          nullable1 = row.BudgetedQuantity;
          Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
          Decimal? nullable4 = new Decimal?(valueOrDefault7 - valueOrDefault8);
          costProjectionLine3.VarianceQuantity = nullable4;
        }
        if (!(row.Mode != "C"))
          return;
        PMCostProjectionLine costProjectionLine4 = row;
        Decimal val1_1 = num2;
        nullable1 = row.CompletedAmount;
        Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
        Decimal? nullable5 = new Decimal?(Math.Max(val1_1, valueOrDefault9));
        costProjectionLine4.ProjectedAmount = nullable5;
        PMCostProjectionLine costProjectionLine5 = row;
        nullable1 = row.ProjectedAmount;
        Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
        nullable1 = row.CompletedAmount;
        Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
        Decimal? nullable6 = new Decimal?(Math.Max(0M, valueOrDefault10 - valueOrDefault11));
        costProjectionLine5.Amount = nullable6;
        PMCostProjectionLine costProjectionLine6 = row;
        nullable1 = row.ProjectedAmount;
        Decimal valueOrDefault12 = nullable1.GetValueOrDefault();
        nullable1 = row.BudgetedAmount;
        Decimal valueOrDefault13 = nullable1.GetValueOrDefault();
        Decimal? nullable7 = new Decimal?(valueOrDefault12 - valueOrDefault13);
        costProjectionLine6.VarianceAmount = nullable7;
        return;
      }
    }
    if (row.Mode != "Q")
    {
      PMCostProjectionLine costProjectionLine7 = row;
      nullable1 = row.BudgetedQuantity;
      Decimal valueOrDefault14 = nullable1.GetValueOrDefault();
      nullable1 = row.CompletedQuantity;
      Decimal valueOrDefault15 = nullable1.GetValueOrDefault();
      Decimal? nullable8 = new Decimal?(Math.Max(valueOrDefault14, valueOrDefault15));
      costProjectionLine7.ProjectedQuantity = nullable8;
      PMCostProjectionLine costProjectionLine8 = row;
      nullable1 = row.BudgetedQuantity;
      Decimal valueOrDefault16 = nullable1.GetValueOrDefault();
      nullable1 = row.CompletedQuantity;
      Decimal valueOrDefault17 = nullable1.GetValueOrDefault();
      Decimal? nullable9 = new Decimal?(Math.Max(0M, valueOrDefault16 - valueOrDefault17));
      costProjectionLine8.Quantity = nullable9;
      PMCostProjectionLine costProjectionLine9 = row;
      nullable1 = row.ProjectedQuantity;
      Decimal valueOrDefault18 = nullable1.GetValueOrDefault();
      nullable1 = row.BudgetedQuantity;
      Decimal valueOrDefault19 = nullable1.GetValueOrDefault();
      Decimal? nullable10 = new Decimal?(valueOrDefault18 - valueOrDefault19);
      costProjectionLine9.VarianceQuantity = nullable10;
    }
    if (!(row.Mode != "C"))
      return;
    PMCostProjectionLine costProjectionLine10 = row;
    nullable1 = row.BudgetedAmount;
    Decimal valueOrDefault20 = nullable1.GetValueOrDefault();
    nullable1 = row.CompletedAmount;
    Decimal valueOrDefault21 = nullable1.GetValueOrDefault();
    Decimal? nullable11 = new Decimal?(Math.Max(valueOrDefault20, valueOrDefault21));
    costProjectionLine10.ProjectedAmount = nullable11;
    PMCostProjectionLine costProjectionLine11 = row;
    nullable1 = row.BudgetedAmount;
    Decimal valueOrDefault22 = nullable1.GetValueOrDefault();
    nullable1 = row.CompletedAmount;
    Decimal valueOrDefault23 = nullable1.GetValueOrDefault();
    Decimal? nullable12 = new Decimal?(Math.Max(0M, valueOrDefault22 - valueOrDefault23));
    costProjectionLine11.Amount = nullable12;
    PMCostProjectionLine costProjectionLine12 = row;
    nullable1 = row.ProjectedAmount;
    Decimal valueOrDefault24 = nullable1.GetValueOrDefault();
    nullable1 = row.BudgetedAmount;
    Decimal valueOrDefault25 = nullable1.GetValueOrDefault();
    Decimal? nullable13 = new Decimal?(valueOrDefault24 - valueOrDefault25);
    costProjectionLine12.VarianceAmount = nullable13;
  }

  private void VerifyAndRaiseExceptionIfRowsExists()
  {
    if (((PXSelectBase<PMCostProjectionLine>) this.Details).Select(Array.Empty<object>()).Count > 0)
      throw new PXSetPropertyException("The value can be changed only when there are no lines on the Details tab.");
  }

  private void VerifyAndRaiseExceptionIfBudgetIncompatible(
    string classID,
    bool classCanBeLessDetailed)
  {
    PMCostProjectionClass projectionClass = PMCostProjectionClass.PK.Find((PXGraph) this, classID);
    if (((PXSelectBase<PMCostProjection>) this.Document).Current == null || projectionClass == null)
      return;
    this.VerifyAndRaiseExceptionIfBudgetIncompatible(projectionClass, (System.Action) (() =>
    {
      throw new PXSetPropertyException("The class is not compatible with the structure of the project budget.");
    }), classCanBeLessDetailed);
  }

  private void VerifyAndRaiseExceptionIfBudgetIncompatible(
    PMCostProjectionClass projectionClass,
    System.Action throwExceptionMethod,
    bool classCanBeLessDetailed)
  {
    if (projectionClass == null)
      throw new ArgumentNullException(nameof (projectionClass));
    if (throwExceptionMethod == null)
      throw new ArgumentNullException(nameof (throwExceptionMethod));
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
    if (pmProject == null)
      return;
    if (pmProject.CostBudgetLevel == "T")
    {
      if (!projectionClass.TaskID.GetValueOrDefault() && !classCanBeLessDetailed)
        throwExceptionMethod();
      if (projectionClass.InventoryID.GetValueOrDefault())
        throwExceptionMethod();
      if (!projectionClass.CostCodeID.GetValueOrDefault())
        return;
      throwExceptionMethod();
    }
    else if (pmProject.CostBudgetLevel == "I")
    {
      if (!projectionClass.TaskID.GetValueOrDefault() && !classCanBeLessDetailed)
        throwExceptionMethod();
      if (!projectionClass.InventoryID.GetValueOrDefault() && !classCanBeLessDetailed)
        throwExceptionMethod();
      if (!projectionClass.CostCodeID.GetValueOrDefault())
        return;
      throwExceptionMethod();
    }
    else if (pmProject.CostBudgetLevel == "C")
    {
      if (!projectionClass.TaskID.GetValueOrDefault() && !classCanBeLessDetailed)
        throwExceptionMethod();
      if (!projectionClass.CostCodeID.GetValueOrDefault() && !classCanBeLessDetailed)
        throwExceptionMethod();
      if (!projectionClass.InventoryID.GetValueOrDefault())
        return;
      throwExceptionMethod();
    }
    else
    {
      if (!(pmProject.CostBudgetLevel == "D") || classCanBeLessDetailed)
        return;
      if (!projectionClass.TaskID.GetValueOrDefault())
        throwExceptionMethod();
      if (!projectionClass.CostCodeID.GetValueOrDefault())
        throwExceptionMethod();
      if (projectionClass.InventoryID.GetValueOrDefault())
        return;
      throwExceptionMethod();
    }
  }

  protected virtual Dictionary<BudgetKeyTuple, PMBudgetRecord> GetCostBudget(
    PMCostProjection costProjection)
  {
    string str = $"{costProjection.ProjectID}.{costProjection.ClassID}";
    if (str != this.budgetRecordsKey)
      this.budgetRecords = (Dictionary<BudgetKeyTuple, PMBudgetRecord>) null;
    if (this.budgetRecords == null)
    {
      PMCostProjectionClass costProjectionClass = PXResultset<PMCostProjectionClass>.op_Implicit(((PXSelectBase<PMCostProjectionClass>) this.Class).Select(Array.Empty<object>()));
      this.budgetRecords = new Dictionary<BudgetKeyTuple, PMBudgetRecord>();
      this.budgetRecordsKey = str;
      foreach (PXResult<PMBudget> pxResult in ((PXSelectBase<PMBudget>) new PXSelectReadonly<PMBudget, Where<PMBudget.projectID, Equal<Current<PMCostProjection.projectID>>, And<PMBudget.type, Equal<AccountType.expense>>>>((PXGraph) this)).Select(Array.Empty<object>()))
      {
        PMBudget y = PXResult<PMBudget>.op_Implicit(pxResult);
        int? nullable1 = y.AccountGroupID;
        int? nullable2 = y.TaskID;
        int? nullable3 = y.InventoryID;
        int emptyInventoryId = nullable3.Value;
        nullable3 = y.CostCodeID;
        int num = nullable3.Value;
        if (costProjectionClass != null)
        {
          bool? nullable4 = costProjectionClass.AccountGroupID;
          if (!nullable4.GetValueOrDefault())
            nullable1 = new int?();
          nullable4 = costProjectionClass.TaskID;
          if (!nullable4.GetValueOrDefault())
            nullable2 = new int?();
          nullable4 = costProjectionClass.CostCodeID;
          if (!nullable4.GetValueOrDefault())
          {
            nullable3 = CostCodeAttribute.DefaultCostCode;
            num = nullable3.Value;
          }
          nullable4 = costProjectionClass.InventoryID;
          if (!nullable4.GetValueOrDefault())
            emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
        }
        BudgetKeyTuple key;
        ref BudgetKeyTuple local = ref key;
        nullable3 = y.ProjectID;
        int projectID = nullable3.Value;
        int valueOrDefault1 = nullable2.GetValueOrDefault();
        int valueOrDefault2 = nullable1.GetValueOrDefault();
        int inventoryID = emptyInventoryId;
        int costCodeID = num;
        local = new BudgetKeyTuple(projectID, valueOrDefault1, valueOrDefault2, inventoryID, costCodeID);
        PMBudgetRecord x;
        if (!this.budgetRecords.TryGetValue(key, out x))
        {
          x = new PMBudgetRecord()
          {
            RecordID = this.GetRecordID(key),
            ProjectID = y.ProjectID,
            ProjectTaskID = nullable2,
            AccountGroupID = nullable1,
            CostCodeID = new int?(num),
            InventoryID = new int?(emptyInventoryId),
            UOM = y.UOM,
            Description = y.Description,
            CuryUnitRate = y.CuryUnitRate
          };
          this.budgetRecords.Add(key, x);
        }
        this.Add(x, y);
      }
    }
    return this.budgetRecords;
  }

  protected virtual PMBudgetRecord Add(PMBudgetRecord x, PMBudget y)
  {
    Decimal? nullable1;
    if (x.UOM == y.UOM)
    {
      PMBudgetRecord pmBudgetRecord1 = x;
      Decimal valueOrDefault1 = x.ActualQty.GetValueOrDefault();
      Decimal? nullable2 = y.ActualQty;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal? nullable3 = new Decimal?(valueOrDefault1 + valueOrDefault2);
      pmBudgetRecord1.ActualQty = nullable3;
      PMBudgetRecord pmBudgetRecord2 = x;
      nullable2 = x.ChangeOrderQty;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      Decimal? nullable4 = y.ChangeOrderQty;
      Decimal valueOrDefault4 = nullable4.GetValueOrDefault();
      Decimal? nullable5 = new Decimal?(valueOrDefault3 + valueOrDefault4);
      pmBudgetRecord2.ChangeOrderQty = nullable5;
      PMBudgetRecord pmBudgetRecord3 = x;
      nullable4 = x.CommittedInvoicedQty;
      Decimal valueOrDefault5 = nullable4.GetValueOrDefault();
      Decimal? nullable6 = y.CommittedInvoicedQty;
      Decimal valueOrDefault6 = nullable6.GetValueOrDefault();
      Decimal? nullable7 = new Decimal?(valueOrDefault5 + valueOrDefault6);
      pmBudgetRecord3.CommittedInvoicedQty = nullable7;
      PMBudgetRecord pmBudgetRecord4 = x;
      nullable6 = x.CommittedOpenQty;
      Decimal valueOrDefault7 = nullable6.GetValueOrDefault();
      Decimal? nullable8 = y.CommittedOpenQty;
      Decimal valueOrDefault8 = nullable8.GetValueOrDefault();
      Decimal? nullable9 = new Decimal?(valueOrDefault7 + valueOrDefault8);
      pmBudgetRecord4.CommittedOpenQty = nullable9;
      PMBudgetRecord pmBudgetRecord5 = x;
      nullable8 = x.CommittedOrigQty;
      Decimal valueOrDefault9 = nullable8.GetValueOrDefault();
      Decimal? nullable10 = y.CommittedOrigQty;
      Decimal valueOrDefault10 = nullable10.GetValueOrDefault();
      Decimal? nullable11 = new Decimal?(valueOrDefault9 + valueOrDefault10);
      pmBudgetRecord5.CommittedOrigQty = nullable11;
      PMBudgetRecord pmBudgetRecord6 = x;
      nullable10 = x.CommittedQty;
      Decimal valueOrDefault11 = nullable10.GetValueOrDefault();
      Decimal? nullable12 = y.CommittedQty;
      Decimal valueOrDefault12 = nullable12.GetValueOrDefault();
      Decimal? nullable13 = new Decimal?(valueOrDefault11 + valueOrDefault12);
      pmBudgetRecord6.CommittedQty = nullable13;
      PMBudgetRecord pmBudgetRecord7 = x;
      nullable12 = x.CommittedReceivedQty;
      Decimal valueOrDefault13 = nullable12.GetValueOrDefault();
      Decimal? nullable14 = y.CommittedReceivedQty;
      Decimal valueOrDefault14 = nullable14.GetValueOrDefault();
      Decimal? nullable15 = new Decimal?(valueOrDefault13 + valueOrDefault14);
      pmBudgetRecord7.CommittedReceivedQty = nullable15;
      PMBudgetRecord pmBudgetRecord8 = x;
      nullable14 = x.DraftChangeOrderQty;
      Decimal valueOrDefault15 = nullable14.GetValueOrDefault();
      Decimal? nullable16 = y.DraftChangeOrderQty;
      Decimal valueOrDefault16 = nullable16.GetValueOrDefault();
      Decimal? nullable17 = new Decimal?(valueOrDefault15 + valueOrDefault16);
      pmBudgetRecord8.DraftChangeOrderQty = nullable17;
      PMBudgetRecord pmBudgetRecord9 = x;
      nullable16 = x.Qty;
      Decimal valueOrDefault17 = nullable16.GetValueOrDefault();
      Decimal? nullable18 = y.Qty;
      Decimal valueOrDefault18 = nullable18.GetValueOrDefault();
      Decimal? nullable19 = new Decimal?(valueOrDefault17 + valueOrDefault18);
      pmBudgetRecord9.Qty = nullable19;
      PMBudgetRecord pmBudgetRecord10 = x;
      nullable18 = x.RevisedQty;
      Decimal valueOrDefault19 = nullable18.GetValueOrDefault();
      nullable1 = y.RevisedQty;
      Decimal valueOrDefault20 = nullable1.GetValueOrDefault();
      Decimal? nullable20 = new Decimal?(valueOrDefault19 + valueOrDefault20);
      pmBudgetRecord10.RevisedQty = nullable20;
    }
    else
    {
      x.UOM = (string) null;
      x.ActualQty = new Decimal?(0M);
      x.ChangeOrderQty = new Decimal?(0M);
      x.CommittedInvoicedQty = new Decimal?(0M);
      x.CommittedOpenQty = new Decimal?(0M);
      x.CommittedOrigQty = new Decimal?(0M);
      x.CommittedQty = new Decimal?(0M);
      x.CommittedReceivedQty = new Decimal?(0M);
      x.DraftChangeOrderQty = new Decimal?(0M);
      x.Qty = new Decimal?(0M);
      x.RevisedQty = new Decimal?(0M);
    }
    if (x.Description != y.Description)
      x.Description = (string) null;
    nullable1 = x.CuryUnitRate;
    Decimal? curyUnitRate = y.CuryUnitRate;
    if (!(nullable1.GetValueOrDefault() == curyUnitRate.GetValueOrDefault() & nullable1.HasValue == curyUnitRate.HasValue))
      x.CuryUnitRate = new Decimal?();
    PMBudgetRecord pmBudgetRecord11 = x;
    Decimal valueOrDefault21 = x.Amount.GetValueOrDefault();
    Decimal? nullable21 = y.Amount;
    Decimal valueOrDefault22 = nullable21.GetValueOrDefault();
    Decimal? nullable22 = new Decimal?(valueOrDefault21 + valueOrDefault22);
    pmBudgetRecord11.Amount = nullable22;
    PMBudgetRecord pmBudgetRecord12 = x;
    nullable21 = x.BaseActualAmount;
    Decimal valueOrDefault23 = nullable21.GetValueOrDefault();
    Decimal? nullable23 = y.BaseActualAmount;
    Decimal valueOrDefault24 = nullable23.GetValueOrDefault();
    Decimal? nullable24 = new Decimal?(valueOrDefault23 + valueOrDefault24);
    pmBudgetRecord12.BaseActualAmount = nullable24;
    PMBudgetRecord pmBudgetRecord13 = x;
    nullable23 = x.ChangeOrderAmount;
    Decimal valueOrDefault25 = nullable23.GetValueOrDefault();
    Decimal? nullable25 = y.ChangeOrderAmount;
    Decimal valueOrDefault26 = nullable25.GetValueOrDefault();
    Decimal? nullable26 = new Decimal?(valueOrDefault25 + valueOrDefault26);
    pmBudgetRecord13.ChangeOrderAmount = nullable26;
    PMBudgetRecord pmBudgetRecord14 = x;
    nullable25 = x.CommittedAmount;
    Decimal valueOrDefault27 = nullable25.GetValueOrDefault();
    Decimal? nullable27 = y.CommittedAmount;
    Decimal valueOrDefault28 = nullable27.GetValueOrDefault();
    Decimal? nullable28 = new Decimal?(valueOrDefault27 + valueOrDefault28);
    pmBudgetRecord14.CommittedAmount = nullable28;
    PMBudgetRecord pmBudgetRecord15 = x;
    nullable27 = x.CommittedInvoicedAmount;
    Decimal valueOrDefault29 = nullable27.GetValueOrDefault();
    Decimal? nullable29 = y.CommittedInvoicedAmount;
    Decimal valueOrDefault30 = nullable29.GetValueOrDefault();
    Decimal? nullable30 = new Decimal?(valueOrDefault29 + valueOrDefault30);
    pmBudgetRecord15.CommittedInvoicedAmount = nullable30;
    PMBudgetRecord pmBudgetRecord16 = x;
    nullable29 = x.CommittedOpenAmount;
    Decimal valueOrDefault31 = nullable29.GetValueOrDefault();
    Decimal? nullable31 = y.CommittedOpenAmount;
    Decimal valueOrDefault32 = nullable31.GetValueOrDefault();
    Decimal? nullable32 = new Decimal?(valueOrDefault31 + valueOrDefault32);
    pmBudgetRecord16.CommittedOpenAmount = nullable32;
    PMBudgetRecord pmBudgetRecord17 = x;
    nullable31 = x.CommittedOrigAmount;
    Decimal valueOrDefault33 = nullable31.GetValueOrDefault();
    Decimal? nullable33 = y.CommittedOrigAmount;
    Decimal valueOrDefault34 = nullable33.GetValueOrDefault();
    Decimal? nullable34 = new Decimal?(valueOrDefault33 + valueOrDefault34);
    pmBudgetRecord17.CommittedOrigAmount = nullable34;
    PMBudgetRecord pmBudgetRecord18 = x;
    nullable33 = x.CuryActualAmount;
    Decimal valueOrDefault35 = nullable33.GetValueOrDefault();
    Decimal? nullable35 = y.CuryActualAmount;
    Decimal valueOrDefault36 = nullable35.GetValueOrDefault();
    Decimal? nullable36 = new Decimal?(valueOrDefault35 + valueOrDefault36);
    pmBudgetRecord18.CuryActualAmount = nullable36;
    PMBudgetRecord pmBudgetRecord19 = x;
    nullable35 = x.CuryAmount;
    Decimal valueOrDefault37 = nullable35.GetValueOrDefault();
    Decimal? nullable37 = y.CuryAmount;
    Decimal valueOrDefault38 = nullable37.GetValueOrDefault();
    Decimal? nullable38 = new Decimal?(valueOrDefault37 + valueOrDefault38);
    pmBudgetRecord19.CuryAmount = nullable38;
    PMBudgetRecord pmBudgetRecord20 = x;
    nullable37 = x.CuryChangeOrderAmount;
    Decimal valueOrDefault39 = nullable37.GetValueOrDefault();
    Decimal? nullable39 = y.CuryChangeOrderAmount;
    Decimal valueOrDefault40 = nullable39.GetValueOrDefault();
    Decimal? nullable40 = new Decimal?(valueOrDefault39 + valueOrDefault40);
    pmBudgetRecord20.CuryChangeOrderAmount = nullable40;
    PMBudgetRecord pmBudgetRecord21 = x;
    nullable39 = x.CuryCommittedAmount;
    Decimal valueOrDefault41 = nullable39.GetValueOrDefault();
    Decimal? nullable41 = y.CuryCommittedAmount;
    Decimal valueOrDefault42 = nullable41.GetValueOrDefault();
    Decimal? nullable42 = new Decimal?(valueOrDefault41 + valueOrDefault42);
    pmBudgetRecord21.CuryCommittedAmount = nullable42;
    PMBudgetRecord pmBudgetRecord22 = x;
    nullable41 = x.CuryCommittedInvoicedAmount;
    Decimal valueOrDefault43 = nullable41.GetValueOrDefault();
    nullable41 = y.CuryCommittedInvoicedAmount;
    Decimal valueOrDefault44 = nullable41.GetValueOrDefault();
    Decimal? nullable43 = new Decimal?(valueOrDefault43 + valueOrDefault44);
    pmBudgetRecord22.CuryCommittedInvoicedAmount = nullable43;
    PMBudgetRecord pmBudgetRecord23 = x;
    nullable41 = x.CuryCommittedOpenAmount;
    Decimal valueOrDefault45 = nullable41.GetValueOrDefault();
    nullable41 = y.CuryCommittedOpenAmount;
    Decimal valueOrDefault46 = nullable41.GetValueOrDefault();
    Decimal? nullable44 = new Decimal?(valueOrDefault45 + valueOrDefault46);
    pmBudgetRecord23.CuryCommittedOpenAmount = nullable44;
    PMBudgetRecord pmBudgetRecord24 = x;
    nullable41 = x.CuryCommittedOrigAmount;
    Decimal valueOrDefault47 = nullable41.GetValueOrDefault();
    nullable41 = y.CuryCommittedOrigAmount;
    Decimal valueOrDefault48 = nullable41.GetValueOrDefault();
    Decimal? nullable45 = new Decimal?(valueOrDefault47 + valueOrDefault48);
    pmBudgetRecord24.CuryCommittedOrigAmount = nullable45;
    PMBudgetRecord pmBudgetRecord25 = x;
    nullable41 = x.CuryDraftChangeOrderAmount;
    Decimal valueOrDefault49 = nullable41.GetValueOrDefault();
    nullable41 = y.CuryDraftChangeOrderAmount;
    Decimal valueOrDefault50 = nullable41.GetValueOrDefault();
    Decimal? nullable46 = new Decimal?(valueOrDefault49 + valueOrDefault50);
    pmBudgetRecord25.CuryDraftChangeOrderAmount = nullable46;
    PMBudgetRecord pmBudgetRecord26 = x;
    nullable41 = x.CuryInvoicedAmount;
    Decimal valueOrDefault51 = nullable41.GetValueOrDefault();
    nullable41 = y.CuryInvoicedAmount;
    Decimal valueOrDefault52 = nullable41.GetValueOrDefault();
    Decimal? nullable47 = new Decimal?(valueOrDefault51 + valueOrDefault52);
    pmBudgetRecord26.CuryInvoicedAmount = nullable47;
    PMBudgetRecord pmBudgetRecord27 = x;
    nullable41 = x.CuryRevisedAmount;
    Decimal valueOrDefault53 = nullable41.GetValueOrDefault();
    nullable41 = y.CuryRevisedAmount;
    Decimal valueOrDefault54 = nullable41.GetValueOrDefault();
    Decimal? nullable48 = new Decimal?(valueOrDefault53 + valueOrDefault54);
    pmBudgetRecord27.CuryRevisedAmount = nullable48;
    PMBudgetRecord pmBudgetRecord28 = x;
    nullable41 = x.DraftChangeOrderAmount;
    Decimal valueOrDefault55 = nullable41.GetValueOrDefault();
    nullable41 = y.DraftChangeOrderAmount;
    Decimal valueOrDefault56 = nullable41.GetValueOrDefault();
    Decimal? nullable49 = new Decimal?(valueOrDefault55 + valueOrDefault56);
    pmBudgetRecord28.DraftChangeOrderAmount = nullable49;
    PMBudgetRecord pmBudgetRecord29 = x;
    nullable41 = x.InvoicedAmount;
    Decimal valueOrDefault57 = nullable41.GetValueOrDefault();
    nullable41 = y.InvoicedAmount;
    Decimal valueOrDefault58 = nullable41.GetValueOrDefault();
    Decimal? nullable50 = new Decimal?(valueOrDefault57 + valueOrDefault58);
    pmBudgetRecord29.InvoicedAmount = nullable50;
    PMBudgetRecord pmBudgetRecord30 = x;
    nullable41 = x.RevisedAmount;
    Decimal valueOrDefault59 = nullable41.GetValueOrDefault();
    nullable41 = y.RevisedAmount;
    Decimal valueOrDefault60 = nullable41.GetValueOrDefault();
    Decimal? nullable51 = new Decimal?(valueOrDefault59 + valueOrDefault60);
    pmBudgetRecord30.RevisedAmount = nullable51;
    return x;
  }

  private Decimal GetCompletedPct(PMBudgetRecord row, string mode)
  {
    if (mode == "C")
    {
      Decimal? nullable = row.ActualQty;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = row.CommittedOpenQty;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num1 = valueOrDefault1 + valueOrDefault2;
      nullable = row.RevisedQty;
      if (nullable.GetValueOrDefault() == 0M)
        return !(num1 == 0M) ? 100M : 0M;
      Decimal num2 = 100M * num1;
      nullable = row.RevisedQty;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return PXDBQuantityAttribute.Round(new Decimal?(num2 / valueOrDefault3));
    }
    Decimal? nullable1 = row.CuryActualAmount;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    nullable1 = row.CuryCommittedOpenAmount;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    Decimal num3 = valueOrDefault4 + valueOrDefault5;
    nullable1 = row.CuryRevisedAmount;
    if (nullable1.GetValueOrDefault() == 0M)
      return !(num3 == 0M) ? 100M : 0M;
    Decimal num4 = 100M * num3;
    nullable1 = row.CuryRevisedAmount;
    Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
    return PXDBQuantityAttribute.Round(new Decimal?(num4 / valueOrDefault6));
  }

  private Decimal GetCompletedPct(PMCostProjectionLine row)
  {
    if (row.Mode == "C")
    {
      if (!(row.BudgetedQuantity.GetValueOrDefault() == 0M))
        return PXDBQuantityAttribute.Round(new Decimal?(100M * row.CompletedQuantity.GetValueOrDefault() / row.BudgetedQuantity.GetValueOrDefault()));
      Decimal? completedQuantity = row.CompletedQuantity;
      Decimal num = 0M;
      return !(completedQuantity.GetValueOrDefault() == num & completedQuantity.HasValue) ? 100M : 0M;
    }
    if (!(row.BudgetedAmount.GetValueOrDefault() == 0M))
      return PXDBQuantityAttribute.Round(new Decimal?(100M * row.CompletedAmount.GetValueOrDefault() / row.BudgetedAmount.GetValueOrDefault()));
    Decimal? completedAmount = row.CompletedAmount;
    Decimal num1 = 0M;
    return !(completedAmount.GetValueOrDefault() == num1 & completedAmount.HasValue) ? 100M : 0M;
  }

  private PMBudgetRecord GetBudgetRecord(PMCostProjectionLine row)
  {
    PMBudgetRecord budgetRecord = (PMBudgetRecord) null;
    if (((PXSelectBase<PMCostProjection>) this.Document).Current != null)
      this.GetCostBudget(((PXSelectBase<PMCostProjection>) this.Document).Current).TryGetValue(this.GetBudgetKey(row), out budgetRecord);
    return budgetRecord;
  }

  private BudgetKeyTuple GetBudgetKey(PMBudgetRecord item)
  {
    int? nullable = item.ProjectID;
    int valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = item.ProjectTaskID;
    int valueOrDefault2 = nullable.GetValueOrDefault();
    nullable = item.AccountGroupID;
    int valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = item.InventoryID;
    int inventoryID = nullable ?? PMInventorySelectorAttribute.EmptyInventoryID;
    nullable = item.CostCodeID;
    int costCodeID = nullable ?? CostCodeAttribute.GetDefaultCostCode();
    return new BudgetKeyTuple(valueOrDefault1, valueOrDefault2, valueOrDefault3, inventoryID, costCodeID);
  }

  private BudgetKeyTuple GetBudgetKey(PMCostProjectionLine item)
  {
    int? nullable = item.ProjectID;
    int valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = item.TaskID;
    int valueOrDefault2 = nullable.GetValueOrDefault();
    nullable = item.AccountGroupID;
    int valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = item.InventoryID;
    int inventoryID = nullable ?? PMInventorySelectorAttribute.EmptyInventoryID;
    nullable = item.CostCodeID;
    int costCodeID = nullable ?? CostCodeAttribute.GetDefaultCostCode();
    return new BudgetKeyTuple(valueOrDefault1, valueOrDefault2, valueOrDefault3, inventoryID, costCodeID);
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (this.lines == null)
      this.lines = this.GetProjectionLines();
    PMCostProjectionClass costProjectionClass = PXResultset<PMCostProjectionClass>.op_Implicit(((PXSelectBase<PMCostProjectionClass>) this.Class).Select(Array.Empty<object>()));
    if (((PXSelectBase<PMCostProjection>) this.Document).Current != null && ((PXSelectBase<PMCostProjection>) this.Document).Current.ProjectID.HasValue && costProjectionClass != null)
    {
      int? nullable1 = new int?();
      if (values.Contains((object) "AccountGroupID"))
      {
        string accountGroupCD = (string) values[(object) "AccountGroupID"];
        nullable1 = (PMAccountGroup.UK.Find((PXGraph) this, accountGroupCD) ?? throw new PXException("The {0} account group from the source file does not exist in the system.", new object[1]
        {
          (object) accountGroupCD
        })).GroupID;
      }
      int? nullable2 = new int?();
      if (values.Contains((object) "TaskID"))
      {
        string taskCD = (string) values[(object) "TaskID"];
        nullable2 = (PMTask.UK.Find((PXGraph) this, ((PXSelectBase<PMCostProjection>) this.Document).Current.ProjectID, taskCD) ?? throw new PXException("The {0} cost task from the source file does not exist in the system.", new object[1]
        {
          (object) taskCD
        })).TaskID;
      }
      int? nullable3 = new int?();
      if (values.Contains((object) "CostCodeID"))
      {
        string costCodeCD = (string) values[(object) "CostCodeID"];
        nullable3 = (PMCostCode.UK.Find((PXGraph) this, costCodeCD) ?? throw new PXException("The {0} cost code from the source file does not exist in the system.", new object[1]
        {
          (object) costCodeCD
        })).CostCodeID;
      }
      int? nullable4 = new int?();
      if (values.Contains((object) "InventoryID"))
      {
        string inventoryCD = (string) values[(object) "InventoryID"];
        nullable4 = (PX.Objects.IN.InventoryItem.UK.Find((PXGraph) this, inventoryCD) ?? throw new PXException("The {0} inventory item from the source file does not exist in the system.", new object[1]
        {
          (object) inventoryCD
        })).InventoryID;
      }
      bool? nullable5 = costProjectionClass.AccountGroupID;
      if (nullable5.GetValueOrDefault() && !nullable1.HasValue)
        throw new PXException("The required AccountGroupID column is missed in the source file.");
      PMProject pmProject = PMProject.PK.Find((PXGraph) this, ((PXSelectBase<PMCostProjection>) this.Document).Current.ProjectID);
      nullable5 = costProjectionClass.TaskID;
      if (nullable5.GetValueOrDefault() && !nullable2.HasValue)
        throw new PXException("The required TaskID column is missed in the source file.");
      nullable5 = costProjectionClass.CostCodeID;
      if (nullable5.GetValueOrDefault() && (pmProject.CostBudgetLevel == "C" || pmProject.CostBudgetLevel == "D") && !nullable3.HasValue)
        throw new PXException("The required CostCodeID column is missed in the source file.");
      nullable5 = costProjectionClass.InventoryID;
      if (nullable5.GetValueOrDefault() && (pmProject.CostBudgetLevel == "I" || pmProject.CostBudgetLevel == "D") && !nullable4.HasValue)
        throw new PXException("The required InventoryID column is missed in the source file.");
      if (!nullable3.HasValue)
        nullable3 = CostCodeAttribute.DefaultCostCode;
      if (!nullable4.HasValue)
        nullable4 = new int?(PMInventorySelectorAttribute.EmptyInventoryID);
      BudgetKeyTuple key = new BudgetKeyTuple(((PXSelectBase<PMCostProjection>) this.Document).Current.ProjectID.Value, nullable2.GetValueOrDefault(), nullable1.GetValueOrDefault(), nullable4.GetValueOrDefault(), nullable3.GetValueOrDefault());
      PMCostProjectionLine costProjectionLine = (PMCostProjectionLine) null;
      if (this.lines.TryGetValue(key, out costProjectionLine) && keys.Contains((object) "LineNbr"))
        keys[(object) "LineNbr"] = (object) costProjectionLine.LineNbr;
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => true;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  [ExcludeFromCodeCoverage]
  [PXHidden]
  public class PMCostProjectionCopyDialogInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(30, InputMask = ">aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
    [PXDefault]
    [PXUIField(DisplayName = "New Revision")]
    public virtual string RevisionID { get; set; }

    [PXUnboundDefault(true)]
    [PXBool]
    [PXUIField(DisplayName = "Refresh Budget")]
    public virtual bool? RefreshBudget { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Copy Notes")]
    public virtual bool? CopyNotes { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Copy Files")]
    public virtual bool? CopyFiles { get; set; }

    public abstract class revisionID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CostProjectionEntry.PMCostProjectionCopyDialogInfo.revisionID>
    {
    }

    public abstract class refreshBudget : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CostProjectionEntry.PMCostProjectionCopyDialogInfo.copyFiles>
    {
    }

    public abstract class copyNotes : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CostProjectionEntry.PMCostProjectionCopyDialogInfo.copyNotes>
    {
    }

    public abstract class copyFiles : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CostProjectionEntry.PMCostProjectionCopyDialogInfo.copyFiles>
    {
    }
  }
}
