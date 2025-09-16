// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountByPeriodEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.GraphBaseExtensions;
using PX.Objects.GL.Reclassification.UI;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
public class AccountByPeriodEnq : PXGraph<AccountByPeriodEnq>
{
  public PXCancel<AccountByPeriodFilter> Cancel;
  public PXAction<AccountByPeriodFilter> PreviousPeriod;
  public PXAction<AccountByPeriodFilter> NextPeriod;
  public PXAction<AccountByPeriodFilter> DoubleClick;
  public PXAction<AccountByPeriodFilter> ViewBatch;
  public PXAction<AccountByPeriodFilter> ViewDocument;
  public PXAction<AccountByPeriodFilter> reclassify;
  public PXAction<AccountByPeriodFilter> reclassifyAll;
  public PXAction<AccountByPeriodFilter> reclassificationHistory;
  public PXFilter<AccountByPeriodFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectOrderBy<GLTranR, OrderBy<Asc<GLTranR.tranDate, Asc<GLTranR.refNbr, Asc<GLTranR.batchNbr, Asc<GLTranR.module, Asc<GLTranR.lineNbr>>>>>>> GLTranEnq;
  public PXSetup<GLSetup> glsetup;
  public PXSelect<Account, Where<Account.accountID, Equal<Current<AccountByPeriodFilter.accountID>>>> AccountInfo;
  protected bool InReclassifyAllSelectingContext;
  public PXAction<AccountByPeriodFilter> ViewReclassBatch;
  public PXAction<AccountByPeriodFilter> ViewProject;
  public PXAction<AccountByPeriodFilter> ViewTask;

  public FinPeriod CurrentStartPeriod
  {
    get
    {
      return this.FinPeriodRepository.FindByID(this.FinPeriodRepository.GetCalendarOrganizationID(((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.OrganizationID, ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.BranchID, ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.UseMasterCalendar), ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.StartPeriodID);
    }
  }

  public Ledger CurrentLedger
  {
    get
    {
      return PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelectReadonly<Ledger, Where<Ledger.ledgerID, Equal<Current<AccountByPeriodFilter.ledgerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    }
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  protected virtual int[] FilteringBranchIDs
  {
    get
    {
      int[] filteringBranchIds = (int[]) null;
      if (((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.BranchID.HasValue)
        filteringBranchIds = new int[1]
        {
          ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.BranchID.Value
        };
      else if (((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.OrganizationID.HasValue)
        filteringBranchIds = PXAccess.GetChildBranchIDs(((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.OrganizationID, false);
      return filteringBranchIds;
    }
  }

  protected virtual PXSelectBase<GLTranR> Command
  {
    get
    {
      AccountByPeriodFilter current = ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current;
      PXSelectBase<GLTranR> command = PXAccess.FeatureInstalled<FeaturesSet.rowLevelSecurity>() ? (PXSelectBase<GLTranR>) (object) new PXSelectJoin<GLTranR, InnerJoin<PX.Objects.GL.ADL.Sub, On<GLTranR.subID, Equal<Sub.subID>, And<Match<PX.Objects.GL.ADL.Sub, Current<AccessInfo.userName>>>>, LeftJoin<PX.Objects.GL.ADL.Batch, On<GLTranR.module, Equal<PX.Objects.GL.ADL.Batch.module>, And<GLTranR.batchNbr, Equal<PX.Objects.GL.ADL.Batch.batchNbr>>>>>, Where<GLTranR.ledgerID, Equal<Current<AccountByPeriodFilter.ledgerID>>, And<GLTranR.accountID, Equal<Current<AccountByPeriodFilter.accountID>>>>>((PXGraph) this) : (PXSelectBase<GLTranR>) (object) new PXSelectJoin<GLTranR, LeftJoin<PX.Objects.GL.ADL.Sub, On<GLTranR.subID, Equal<Sub.subID>>, LeftJoin<PX.Objects.GL.ADL.Batch, On<GLTranR.module, Equal<PX.Objects.GL.ADL.Batch.module>, And<GLTranR.batchNbr, Equal<PX.Objects.GL.ADL.Batch.batchNbr>>>>>, Where<GLTranR.ledgerID, Equal<Current<AccountByPeriodFilter.ledgerID>>, And<GLTranR.accountID, Equal<Current<AccountByPeriodFilter.accountID>>>>>((PXGraph) this);
      if (current.UseMasterCalendar.GetValueOrDefault())
        command.WhereAnd<Where<GLTranR.tranPeriodID, GreaterEqual<Current<AccountByPeriodFilter.startPeriodID>>, And<GLTranR.tranPeriodID, LessEqual<Current<AccountByPeriodFilter.endPeriodID>>>>>();
      else
        command.WhereAnd<Where<GLTranR.finPeriodID, GreaterEqual<Current<AccountByPeriodFilter.startPeriodID>>, And<GLTranR.finPeriodID, LessEqual<Current<AccountByPeriodFilter.endPeriodID>>>>>();
      bool? nullable1 = current.IncludeUnposted;
      if (!nullable1.GetValueOrDefault())
      {
        command.WhereAnd<Where<GLTranR.posted, Equal<True>>>();
      }
      else
      {
        nullable1 = current.IncludeUnposted;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = current.IncludeUnreleased;
          if (!nullable1.GetValueOrDefault())
          {
            command.WhereAnd<Where<GLTranR.released, Equal<True>>>();
            goto label_9;
          }
        }
        command.WhereAnd<Where<PX.Objects.GL.ADL.Batch.voided, NotEqual<True>, And<PX.Objects.GL.ADL.Batch.scheduled, NotEqual<True>>>>();
      }
label_9:
      nullable1 = current.IncludeReclassified;
      DateTime? nullable2;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = current.ShowSummary;
        if (!nullable1.GetValueOrDefault())
        {
          command.Join<LeftJoin<ReclassifyingGLTranAggregate, On<GLTranR.module, Equal<ReclassifyingGLTranAggregate.module>, And<GLTranR.batchNbr, Equal<ReclassifyingGLTranAggregate.batchNbr>, And<GLTranR.lineNbr, Equal<ReclassifyingGLTranAggregate.lineNbr>>>>>>();
          nullable2 = current.StartDate;
          if (!nullable2.HasValue)
          {
            nullable2 = current.EndDate;
            if (!nullable2.HasValue)
              command.WhereAnd<Where2<Where<GLTranR.isReclassReverse, Equal<False>>, And<Where<GLTranR.reclassified, Equal<False>, Or<Where<GLTranR.reclassified, Equal<True>, And<Where<Sub<GLTranR.curyDebitAmt, IsNull<ReclassifyingGLTranAggregate.curyCreditAmt, decimal0>>, NotEqual<Zero>, Or<Sub<GLTranR.curyCreditAmt, IsNull<ReclassifyingGLTranAggregate.curyDebitAmt, decimal0>>, NotEqual<Zero>>>>>>>>>>();
          }
          nullable2 = current.StartDate;
          if (nullable2.HasValue)
          {
            nullable2 = current.EndDate;
            if (!nullable2.HasValue)
              command.WhereAnd<Where2<Where<GLTranR.isReclassReverse, Equal<False>, Or<Where<GLTranR.isReclassReverse, Equal<True>, And<Where<GLTranR.reclassOrigTranDate, LessEqual<Current<AccountByPeriodFilter.startDate>>>>>>>, And<Where<GLTranR.reclassified, Equal<False>, Or<Where<GLTranR.reclassified, Equal<True>, And<Where<Sub<GLTranR.curyDebitAmt, IsNull<ReclassifyingGLTranAggregate.curyCreditAmt, decimal0>>, NotEqual<Zero>, Or<Sub<GLTranR.curyCreditAmt, IsNull<ReclassifyingGLTranAggregate.curyDebitAmt, decimal0>>, NotEqual<Zero>>>>>>>>>>();
          }
          nullable2 = current.StartDate;
          if (!nullable2.HasValue)
          {
            nullable2 = current.EndDate;
            if (nullable2.HasValue)
              command.WhereAnd<Where2<Where<GLTranR.isReclassReverse, Equal<False>, Or<Where<GLTranR.isReclassReverse, Equal<True>, And<Where<GLTranR.reclassOrigTranDate, GreaterEqual<Current<AccountByPeriodFilter.periodEndDate>>>>>>>, And<Where<GLTranR.reclassified, Equal<False>, Or<Where<GLTranR.reclassified, Equal<True>, And<Where<Sub<GLTranR.curyDebitAmt, IsNull<ReclassifyingGLTranAggregate.curyCreditAmt, decimal0>>, NotEqual<Zero>, Or<Sub<GLTranR.curyCreditAmt, IsNull<ReclassifyingGLTranAggregate.curyDebitAmt, decimal0>>, NotEqual<Zero>>>>>>>>>>();
          }
          nullable2 = current.StartDate;
          if (nullable2.HasValue)
          {
            nullable2 = current.EndDate;
            if (nullable2.HasValue)
              command.WhereAnd<Where2<Where<GLTranR.isReclassReverse, Equal<False>, Or<Where<GLTranR.isReclassReverse, Equal<True>, And<Where2<Where<GLTranR.reclassOrigTranDate, LessEqual<Current<AccountByPeriodFilter.startDate>>>, Or<Where<GLTranR.reclassOrigTranDate, GreaterEqual<Current<AccountByPeriodFilter.periodEndDate>>>>>>>>>, And<Where<GLTranR.reclassified, Equal<False>, Or<Where<GLTranR.reclassified, Equal<True>, And<Where<Sub<GLTranR.curyDebitAmt, IsNull<ReclassifyingGLTranAggregate.curyCreditAmt, decimal0>>, NotEqual<Zero>, Or<Sub<GLTranR.curyCreditAmt, IsNull<ReclassifyingGLTranAggregate.curyDebitAmt, decimal0>>, NotEqual<Zero>>>>>>>>>>();
          }
        }
      }
      nullable2 = current.StartDate;
      if (nullable2.HasValue)
        command.WhereAnd<Where<GLTranR.tranDate, GreaterEqual<Current<AccountByPeriodFilter.startDate>>>>();
      nullable2 = current.EndDate;
      if (nullable2.HasValue)
        command.WhereAnd<Where<GLTranR.tranDate, Less<Current<AccountByPeriodFilter.endDate>>>>();
      if (current.OrgBAccountID.HasValue)
        command.WhereAnd<Where<GLTranR.branchID, InsideBranchesOf<Required<AccountByPeriodFilter.orgBAccountID>>>>();
      if (!SubCDUtils.IsSubCDEmpty(current.SubID))
        command.WhereAnd<Where<PX.Objects.GL.ADL.Sub.subCD, Like<Current<AccountByPeriodFilter.subCDWildcard>>>>();
      nullable1 = current.ShowSummary;
      if (nullable1.GetValueOrDefault())
      {
        IBqlTemplate ibqlTemplate = BqlTemplate.FromType(typeof (Aggregate<Sum<GLTranR.creditAmt, Sum<GLTranR.debitAmt, Sum<GLTranR.curyCreditAmt, Sum<GLTranR.curyDebitAmt, GroupBy<GLTranR.ledgerID, GroupBy<GLTranR.accountID, GroupBy<BqlPlaceholder.A, GroupBy<GLTranR.tranDate>>>>>>>>>));
        System.Type type1;
        if (current != null)
        {
          nullable1 = current.UseMasterCalendar;
          if (nullable1.GetValueOrDefault())
          {
            type1 = typeof (GLTranR.tranPeriodID);
            goto label_36;
          }
        }
        type1 = typeof (GLTranR.finPeriodID);
label_36:
        System.Type type2 = ibqlTemplate.Replace<BqlPlaceholder.A>(type1).ToType();
        ((PXSelectBase) command).View = new PXView((PXGraph) this, true, ((PXSelectBase) command).View.BqlSelect.AggregateNew(type2));
      }
      return command;
    }
  }

  public AccountByPeriodEnq()
  {
    GLSetup current = ((PXSelectBase<GLSetup>) this.glsetup).Current;
    ((PXSelectBase) this.GLTranEnq).Cache.AllowInsert = false;
    ((PXSelectBase) this.GLTranEnq).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetReadOnly(((PXSelectBase) this.GLTranEnq).Cache, (object) null, true);
    PXUIFieldAttribute.SetReadOnly<GLTranR.selected>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<GLTranR.selected>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, true);
    PXCache cach = ((PXGraph) this).Caches[typeof (BAccountR)];
    PXUIFieldAttribute.SetDisplayName<BAccountR.acctName>(cach, "Business Account Name");
    PXUIFieldAttribute.SetVisible<BAccountR.acctName>(cach, (object) null, false);
    PXUIFieldAttribute.SetVisible<GLTranR.finPeriodID>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, true);
  }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable previousperiod(PXAdapter adapter)
  {
    AccountByPeriodFilter current = ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current;
    int? calendarOrganizationId = this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar);
    FinPeriod prevPeriod1 = this.FinPeriodRepository.FindPrevPeriod(calendarOrganizationId, current.StartPeriodID, true);
    current.StartPeriodID = prevPeriod1?.FinPeriodID;
    FinPeriod prevPeriod2 = this.FinPeriodRepository.FindPrevPeriod(calendarOrganizationId, current.EndPeriodID, true);
    current.EndPeriodID = prevPeriod2?.FinPeriodID;
    this.ResetFilterDates(current);
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable nextperiod(PXAdapter adapter)
  {
    AccountByPeriodFilter current = ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current;
    int? calendarOrganizationId = this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar);
    FinPeriod nextPeriod1 = this.FinPeriodRepository.FindNextPeriod(calendarOrganizationId, current.StartPeriodID, true);
    current.StartPeriodID = nextPeriod1?.FinPeriodID;
    FinPeriod nextPeriod2 = this.FinPeriodRepository.FindNextPeriod(calendarOrganizationId, current.EndPeriodID, true);
    current.EndPeriodID = nextPeriod2?.FinPeriodID;
    this.ResetFilterDates(current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Reclassify(PXAdapter adapter)
  {
    IReadOnlyCollection<GLTranR> selectedTrans = this.GetSelectedTrans();
    if (!selectedTrans.Any<GLTranR>())
      throw new PXException("No transactions, for which the reclassification can be performed, have been selected.");
    ReclassifyTransactionsProcess.OpenForReclassification((IReadOnlyCollection<GLTran>) selectedTrans);
    return (IEnumerable) ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ReclassifyAll(PXAdapter adapter)
  {
    IEnumerable<GLTranR> array;
    try
    {
      this.InReclassifyAllSelectingContext = true;
      array = (IEnumerable<GLTranR>) GraphHelper.RowCast<GLTranR>((IEnumerable) ((PXSelectBase<GLTranR>) this.GLTranEnq).Select(Array.Empty<object>())).ToArray<GLTranR>();
    }
    finally
    {
      this.InReclassifyAllSelectingContext = false;
    }
    ReclassifyTransactionsProcess.TryOpenForReclassification<GLTranR>((PXGraph) this, array, this.CurrentLedger, (Func<GLTranR, string>) (tran => tran.BatchType), ((PXSelectBase) this.GLTranEnq).View, "Some transactions that match the specified selection criteria cannot be reclassified. These transactions will not be loaded.", "No transactions, for which the reclassification can be performed, have been found to match the specified criteria.");
    return (IEnumerable) ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ReclassificationHistory(PXAdapter adapter)
  {
    if (((PXSelectBase<GLTranR>) this.GLTranEnq).Current != null)
      ReclassificationHistoryInq.OpenForTransaction((GLTran) ((PXSelectBase<GLTranR>) this.GLTranEnq).Current);
    return (IEnumerable) ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewBatch(PXAdapter adapter)
  {
    GLTranR current = ((PXSelectBase<GLTranR>) this.GLTranEnq).Current;
    if (current != null)
      this.RedirectToBatch((GLTran) current);
    return (IEnumerable) ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    GLTranR current = ((PXSelectBase<GLTranR>) this.GLTranEnq).Current;
    if (current != null)
    {
      Batch batch = JournalEntry.FindBatch((PXGraph) this, (GLTran) current);
      ((PXGraph) this).GetExtension<AccountByPeriodEnq.RedirectToSourceDocumentFromAccountByPeriodEnqExtension>().RedirectToSourceDocument((GLTran) current, batch);
    }
    return (IEnumerable) ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewReclassBatch(PXAdapter adapter)
  {
    GLTranR current = ((PXSelectBase<GLTranR>) this.GLTranEnq).Current;
    if (current != null)
      JournalEntry.RedirectToBatch((PXGraph) this, current.ReclassBatchModule, current.ReclassBatchNbr);
    return (IEnumerable) ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable doubleClick(PXAdapter adapter)
  {
    GLTranR current1 = ((PXSelectBase<GLTranR>) this.GLTranEnq).Current;
    AccountByPeriodFilter current2 = ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current;
    if (current1 != null && current2 != null)
    {
      if (current2.ShowSummary.GetValueOrDefault())
        this.SwitchToDetailsOfGroupedRow(current2);
      else
        this.RedirectToBatch((GLTran) current1);
    }
    return (IEnumerable) ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewProject(PXAdapter adapter)
  {
    GLTranR current = ((PXSelectBase<GLTranR>) this.GLTranEnq).Current;
    ProjectEntry instance = PXGraph.CreateInstance<ProjectEntry>();
    ((PXSelectBase<PMProject>) instance.Project).Current = PXResultset<PMProject>.op_Implicit(current != null ? ((PXSelectBase<PMProject>) instance.Project).Search<PMProject.contractID>((object) current.ProjectID, Array.Empty<object>()) : (PXResultset<PMProject>) null);
    if (((PXSelectBase<PMProject>) instance.Project).Current != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return (IEnumerable) ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewTask(PXAdapter adapter)
  {
    GLTranR current = ((PXSelectBase<GLTranR>) this.GLTranEnq).Current;
    ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
    ((PXSelectBase<PMTask>) instance.Task).Current = PXResultset<PMTask>.op_Implicit(current != null ? ((PXSelectBase<PMTask>) instance.Task).Search<PMTask.taskID>((object) (int?) current?.TaskID, Array.Empty<object>()) : (PXResultset<PMTask>) null);
    if (((PXSelectBase<PMTask>) instance.Task).Current != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return (IEnumerable) ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Select(Array.Empty<object>());
  }

  protected PXFilterRow[] TransactionFilters
  {
    get
    {
      return this.GetApplicableFilters((IEnumerable<PXFilterRow>) (((PXSelectBase) this.GLTranEnq).View.GetExternalFilters() ?? new PXFilterRow[0]));
    }
  }

  protected virtual IEnumerable glTranEnq() => this.RetrieveGLTran();

  public virtual IEnumerable RetrieveGLTran()
  {
    AccountByPeriodFilter current = ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current;
    if (!current.AccountID.HasValue || !current.LedgerID.HasValue || current.StartPeriodID == null || current.EndPeriodID == null)
      return (IEnumerable) new GLTranR[0];
    PXUIFieldAttribute.SetVisible<GLTranR.begBalance>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<GLTranR.endBalance>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, true);
    Decimal runningBalance = current.UnsignedBegBal.GetValueOrDefault();
    Decimal? runningCuryBalance = current.UnsignedCuryBegBal;
    bool isSyncPosition = ((IEnumerable<object>) PXView.Searches).Any<object>((Func<object, bool>) (search => search != null));
    List<GLTranR> satisfiedUpdated = new List<GLTranR>();
    PXNoteAttribute.SetTextFilesActivitiesRequired<GLTranR.noteID>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, true, true, false);
    int?[] array = this._currentUserInformationProvider.GetAllBranches().Select<BranchInfo, int?>((Func<BranchInfo, int?>) (b => new int?(b.Id))).Distinct<int?>().ToArray<int?>();
    Account account = ((PXSelectBase<Account>) this.AccountInfo).SelectSingle(Array.Empty<object>());
    IEnumerable<GLTranR> source;
    using (new PXReadBranchRestrictedScope((int?[]) null, array, true, false)
    {
      SpecificBranchTable = typeof (GLTran).Name
    })
    {
      bool? showSummary = current.ShowSummary;
      bool flag1 = false;
      if (showSummary.GetValueOrDefault() == flag1 & showSummary.HasValue)
      {
        bool? includeReclassified = current.IncludeReclassified;
        bool flag2 = false;
        source = !(includeReclassified.GetValueOrDefault() == flag2 & includeReclassified.HasValue) ? this.ExecuteCommandForRetrieveGLTran(isSyncPosition).Select<PXResult, GLTranR>((Func<PXResult, GLTranR>) (result =>
        {
          GLTranR tran = result.GetItem<GLTranR>();
          this.SetOrigAmounts(tran);
          tran.IncludedInReclassHistory = new bool?(JournalEntry.CanShowReclassHistory((GLTran) tran, tran.BatchType));
          this.PrepareDetailRow(ref runningBalance, ref runningCuryBalance, isSyncPosition, satisfiedUpdated, tran, account);
          if (tran.Reclassified.GetValueOrDefault())
          {
            tran.CreditAmt = tran.OrigCreditAmt;
            tran.DebitAmt = tran.OrigDebitAmt;
            tran.CuryCreditAmt = tran.CuryOrigCreditAmt;
            tran.CuryDebitAmt = tran.CuryOrigDebitAmt;
          }
          return tran;
        })) : this.ExecuteCommandForRetrieveGLTran(isSyncPosition).Select<PXResult, GLTranR>((Func<PXResult, GLTranR>) (result =>
        {
          GLTranR tran = result.GetItem<GLTranR>();
          this.SetOrigAmounts(tran);
          tran.IncludedInReclassHistory = new bool?(JournalEntry.CanShowReclassHistory((GLTran) tran, tran.BatchType));
          this.PrepareDetailRow(ref runningBalance, ref runningCuryBalance, isSyncPosition, satisfiedUpdated, tran, account);
          if (tran.Reclassified.GetValueOrDefault())
            this.ResetAmounts(result.GetItem<ReclassifyingGLTranAggregate>(), tran);
          return tran;
        }));
      }
      else
        source = this.ExecuteCommandForRetrieveGLTran(isSyncPosition).Select<PXResult, GLTranR>((Func<PXResult, GLTranR>) (result =>
        {
          GLTranR tran = result.GetItem<GLTranR>();
          tran.IncludedInReclassHistory = new bool?(JournalEntry.CanShowReclassHistory((GLTran) tran, tran.BatchType));
          this.PrepareDetailRow(ref runningBalance, ref runningCuryBalance, isSyncPosition, satisfiedUpdated, tran, account);
          return tran;
        }));
      if (!isSyncPosition)
      {
        ((PXSelectBase) this.GLTranEnq).Cache.Clear();
        satisfiedUpdated.ForEach((Action<GLTranR>) (tran => ((PXSelectBase) this.GLTranEnq).Cache.SetStatus((object) tran, (PXEntryStatus) 1)));
      }
    }
    return (IEnumerable) source.Select<GLTranR, GLTranR>(new Func<GLTranR, GLTranR>(PXCache<GLTranR>.CreateCopy));
  }

  private void PrepareDetailRow(
    ref Decimal runningBalance,
    ref Decimal? runningCuryBalance,
    bool isSyncPosition,
    List<GLTranR> satisfiedUpdated,
    GLTranR tran,
    Account account)
  {
    if (isSyncPosition)
      return;
    if (((PXSelectBase) this.GLTranEnq).Cache.GetStatus((object) tran) == 1)
      satisfiedUpdated.Add(tran);
    tran.Type = account.Type;
    tran.BegBalance = new Decimal?(runningBalance);
    runningBalance = tran.EndBalance.GetValueOrDefault();
    if (account.CuryID != null)
    {
      tran.CuryID = account.CuryID;
    }
    else
    {
      tran.CuryID = (string) null;
      tran.CuryCreditAmt = new Decimal?();
      tran.CuryDebitAmt = new Decimal?();
    }
    if (!string.IsNullOrEmpty(tran.CuryID))
    {
      tran.CuryBegBalance = new Decimal?(runningCuryBalance.GetValueOrDefault());
      runningCuryBalance = new Decimal?(tran.CuryEndBalance.GetValueOrDefault());
    }
    GLHistoryEnquiryResult.recalculateSignAmount((ISignedBalances) tran, ((PXSelectBase<GLSetup>) this.glsetup).Current?.TrialBalanceSign == "R");
  }

  private void ResetAmounts(ReclassifyingGLTranAggregate aggregate, GLTranR tran)
  {
    Decimal? curyCreditAmt = tran.CuryCreditAmt;
    Decimal? nullable1 = aggregate.CuryCreditAmt;
    Decimal? nullable2 = curyCreditAmt.HasValue & nullable1.HasValue ? new Decimal?(curyCreditAmt.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal num1 = 0M;
    Decimal? nullable3;
    if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
    {
      nullable1 = tran.CuryDebitAmt;
      nullable3 = aggregate.CuryDebitAmt;
      nullable2 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = 0M;
      if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
      {
        tran.CreditAmt = tran.OrigCreditAmt;
        tran.DebitAmt = tran.OrigDebitAmt;
        tran.CuryCreditAmt = tran.CuryOrigCreditAmt;
        tran.CuryDebitAmt = tran.CuryOrigDebitAmt;
        goto label_24;
      }
    }
    GLTranR glTranR1 = tran;
    nullable2 = tran.OrigCreditAmt;
    Decimal num3 = 0M;
    Decimal? nullable4;
    if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
    {
      nullable4 = new Decimal?(0M);
    }
    else
    {
      nullable2 = tran.OrigCreditAmt;
      nullable3 = aggregate.DebitAmt;
      Decimal valueOrDefault = nullable3.GetValueOrDefault();
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(nullable2.GetValueOrDefault() - valueOrDefault);
    }
    glTranR1.CreditAmt = nullable4;
    GLTranR glTranR2 = tran;
    nullable2 = tran.OrigDebitAmt;
    Decimal num4 = 0M;
    Decimal? nullable5;
    if (nullable2.GetValueOrDefault() == num4 & nullable2.HasValue)
    {
      nullable5 = new Decimal?(0M);
    }
    else
    {
      nullable2 = tran.OrigDebitAmt;
      nullable3 = aggregate.CreditAmt;
      Decimal valueOrDefault = nullable3.GetValueOrDefault();
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable5 = nullable3;
      }
      else
        nullable5 = new Decimal?(nullable2.GetValueOrDefault() - valueOrDefault);
    }
    glTranR2.DebitAmt = nullable5;
    GLTranR glTranR3 = tran;
    nullable2 = tran.CuryOrigCreditAmt;
    Decimal num5 = 0M;
    Decimal? nullable6;
    if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue)
    {
      nullable6 = new Decimal?(0M);
    }
    else
    {
      nullable2 = tran.CuryOrigCreditAmt;
      nullable3 = aggregate.CuryDebitAmt;
      Decimal valueOrDefault = nullable3.GetValueOrDefault();
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable6 = nullable3;
      }
      else
        nullable6 = new Decimal?(nullable2.GetValueOrDefault() - valueOrDefault);
    }
    glTranR3.CuryCreditAmt = nullable6;
    GLTranR glTranR4 = tran;
    nullable2 = tran.CuryOrigDebitAmt;
    Decimal num6 = 0M;
    Decimal? nullable7;
    if (nullable2.GetValueOrDefault() == num6 & nullable2.HasValue)
    {
      nullable7 = new Decimal?(0M);
    }
    else
    {
      nullable2 = tran.CuryOrigDebitAmt;
      nullable3 = aggregate.CuryCreditAmt;
      Decimal valueOrDefault = nullable3.GetValueOrDefault();
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable7 = nullable3;
      }
      else
        nullable7 = new Decimal?(nullable2.GetValueOrDefault() - valueOrDefault);
    }
    glTranR4.CuryDebitAmt = nullable7;
label_24:
    GLTranR glTranR5 = tran;
    nullable1 = tran.SignCuryBegBalance;
    Decimal? nullable8 = tran.CuryDebitAmt;
    nullable2 = nullable1.HasValue & nullable8.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
    nullable3 = tran.CuryCreditAmt;
    Decimal? nullable9;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable8 = new Decimal?();
      nullable9 = nullable8;
    }
    else
      nullable9 = new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    glTranR5.SignCuryEndBalance = nullable9;
    GLTranR glTranR6 = tran;
    nullable8 = tran.SignBegBalance;
    nullable1 = tran.DebitAmt;
    nullable3 = nullable8.HasValue & nullable1.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    nullable2 = tran.CreditAmt;
    Decimal? nullable10;
    if (!(nullable3.HasValue & nullable2.HasValue))
    {
      nullable1 = new Decimal?();
      nullable10 = nullable1;
    }
    else
      nullable10 = new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault());
    glTranR6.SignEndBalance = nullable10;
  }

  protected void SetVisibleCuryFields(PXCache cache, bool showCurrency)
  {
    PXUIFieldAttribute.SetVisible<GLTranR.curyCreditAmt>(cache, (object) null, showCurrency);
    PXUIFieldAttribute.SetVisible<GLTranR.curyDebitAmt>(cache, (object) null, showCurrency);
    PXUIFieldAttribute.SetVisible<GLTranR.curyBegBalance>(cache, (object) null, showCurrency);
    PXUIFieldAttribute.SetVisible<GLTranR.curyEndBalance>(cache, (object) null, showCurrency);
    PXUIFieldAttribute.SetVisible<GLTranR.signCuryBegBalance>(cache, (object) null, showCurrency);
    PXUIFieldAttribute.SetVisible<GLTranR.signCuryEndBalance>(cache, (object) null, showCurrency);
    PXUIFieldAttribute.SetVisible<GLTranR.curyID>(cache, (object) null, showCurrency);
  }

  protected virtual IEnumerable filter()
  {
    AccountByPeriodEnq accountByPeriodEnq1 = this;
    AccountByPeriodFilter current = ((PXSelectBase<AccountByPeriodFilter>) accountByPeriodEnq1.Filter).Current;
    AccountByPeriodEnq accountByPeriodEnq2 = accountByPeriodEnq1;
    bool? nullable1 = current.ShowSummary;
    int num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
    accountByPeriodEnq2.ShowDetailColumns(num1 != 0);
    AccountByPeriodEnq accountByPeriodEnq3 = accountByPeriodEnq1;
    PXCache cache = ((PXSelectBase) accountByPeriodEnq1.GLTranEnq).Cache;
    nullable1 = current.ShowCuryDetail;
    int num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    accountByPeriodEnq3.SetVisibleCuryFields(cache, num2 != 0);
    int? nullable2 = current.AccountID;
    if (nullable2.HasValue)
    {
      nullable2 = current.LedgerID;
      if (nullable2.HasValue && current.StartPeriodID != null && current.EndPeriodID != null)
      {
        Account account = ((PXSelectBase<Account>) accountByPeriodEnq1.AccountInfo).SelectSingle(Array.Empty<object>());
        if (account != null)
        {
          nullable1 = current.OrgBAccountIDIsEmpty;
          if (nullable1.GetValueOrDefault())
          {
            AccountByPeriodFilter accountByPeriodFilter = current;
            nullable2 = new int?();
            int? nullable3 = nullable2;
            accountByPeriodFilter.OrgBAccountID = nullable3;
          }
          else
          {
            nullable2 = current.BranchID;
            if (nullable2.HasValue)
            {
              nullable2 = current.OrgBAccountID;
              if (!nullable2.HasValue)
              {
                AccountByPeriodFilter accountByPeriodFilter = current;
                PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(current.BranchID);
                int? nullable4;
                if (branch == null)
                {
                  nullable2 = new int?();
                  nullable4 = nullable2;
                }
                else
                  nullable4 = new int?(branch.BAccountID);
                accountByPeriodFilter.OrgBAccountID = nullable4;
              }
            }
          }
          Decimal balance;
          Decimal? aCuryBalance;
          accountByPeriodEnq1.RetrieveStartingBalance(out balance, out aCuryBalance, out string _);
          PXNoteAttribute.SetTextFilesActivitiesRequired<GLTranR.noteID>(((PXSelectBase) accountByPeriodEnq1.GLTranEnq).Cache, (object) null, true, true, false);
          using (new PXReadBranchRestrictedScope((int?[]) null, accountByPeriodEnq1._currentUserInformationProvider.GetAllBranches().Select<BranchInfo, int?>((Func<BranchInfo, int?>) (b => new int?(b.Id))).Distinct<int?>().ToArray<int?>(), true, false)
          {
            SpecificBranchTable = typeof (GLTran).Name
          })
          {
            nullable1 = current.ShowSummary;
            bool flag1 = false;
            Decimal num3;
            if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
            {
              nullable1 = current.IncludeReclassified;
              bool flag2 = false;
              if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
              {
                num3 = accountByPeriodEnq1.ExecuteCommandForSummary().Sum<PXResult>((Func<PXResult, Decimal>) (result =>
                {
                  GLTranR tran = result.GetItem<GLTranR>();
                  ReclassifyingGLTranAggregate aggregate = result.GetItem<ReclassifyingGLTranAggregate>();
                  this.SetOrigAmounts(tran);
                  if (tran.Reclassified.GetValueOrDefault())
                    this.ResetAmounts(aggregate, tran);
                  return AccountRules.CalcSaldo(account.Type, tran.DebitAmt.GetValueOrDefault(), tran.CreditAmt.GetValueOrDefault());
                }));
                goto label_20;
              }
            }
            if (((IEnumerable<PXFilterRow>) accountByPeriodEnq1.TransactionFilters).Any<PXFilterRow>())
            {
              num3 = accountByPeriodEnq1.ExecuteCommandForSummary().Select<PXResult, GLTranR>((Func<PXResult, GLTranR>) (_ => _.GetItem<GLTranR>())).Sum<GLTranR>((Func<GLTranR, Decimal?>) (_ =>
              {
                Decimal? nullable5 = _.DebitAmt;
                Decimal valueOrDefault1 = nullable5.GetValueOrDefault();
                nullable5 = _.CreditAmt;
                Decimal valueOrDefault2 = nullable5.GetValueOrDefault();
                return new Decimal?(valueOrDefault1 - valueOrDefault2);
              })).GetValueOrDefault();
            }
            else
            {
              ParameterExpression parameterExpression;
              // ISSUE: method reference
              num3 = ((IQueryable<PXResult<GLTranR>>) accountByPeriodEnq1.Command.Select(new object[1]
              {
                (object) current.OrgBAccountID
              })).Select<PXResult<GLTranR>, GLTranR>(Expression.Lambda<Func<PXResult<GLTranR>, GLTranR>>((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).Sum<GLTranR>((Expression<Func<GLTranR, Decimal?>>) (_ => (_.DebitAmt ?? 0M) - (_.CreditAmt ?? 0M) as Decimal?)).GetValueOrDefault();
              ((PXCache) GraphHelper.Caches<GLTranR>((PXGraph) accountByPeriodEnq1)).Clear();
            }
            if (AccountRules.IsCreditBalance(account.Type))
              num3 *= -1M;
label_20:
            current.UnsignedBegBal = new Decimal?(balance);
            current.UnsignedCuryBegBal = aCuryBalance;
            int num4 = !(((PXSelectBase<GLSetup>) accountByPeriodEnq1.glsetup).Current?.TrialBalanceSign == "R") || !(account.Type == "I") && !(account.Type == "L") ? 1 : -1;
            current.TurnOver = new Decimal?((Decimal) num4 * num3);
            if (account.Type != "I" && account.Type != "E")
            {
              nullable2 = account.AccountID;
              int? retEarnAccountId = (int?) ((PXSelectBase<GLSetup>) accountByPeriodEnq1.glsetup).Current?.RetEarnAccountID;
              if (!(nullable2.GetValueOrDefault() == retEarnAccountId.GetValueOrDefault() & nullable2.HasValue == retEarnAccountId.HasValue))
                goto label_23;
            }
            if (!string.Equals(current.StartPeriodID?.Substring(0, 4), current.EndPeriodID?.Substring(0, 4)))
            {
              current.BegBal = new Decimal?(0M);
              current.EndBal = new Decimal?(0M);
              ((PXSelectBase) accountByPeriodEnq1.Filter).Cache.RaiseExceptionHandling<AccountByPeriodFilter.endBal>((object) current, (object) current.EndBal, (Exception) new PXSetPropertyException("The beginning and ending balances cannot be displayed for the selected account because the specified period spans multiple years.", (PXErrorLevel) 2));
              goto label_25;
            }
label_23:
            current.BegBal = new Decimal?((Decimal) num4 * balance);
            current.EndBal = new Decimal?((Decimal) num4 * (balance + num3));
            ((PXSelectBase) accountByPeriodEnq1.Filter).Cache.RaiseExceptionHandling<AccountByPeriodFilter.endBal>((object) current, (object) current.EndBal, (Exception) null);
label_25:
            ((PXSelectBase) accountByPeriodEnq1.Filter).Cache.IsDirty = false;
          }
        }
      }
    }
    yield return (object) current;
  }

  private IEnumerable<PXResult> ExecuteCommandForRetrieveGLTran(bool isSyncPosition)
  {
    int num1 = 0;
    int num2 = 0;
    return ((PXSelectBase) this.Command).View.Select((object[]) null, new object[1]
    {
      (object) ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.OrgBAccountID
    }, PXView.Searches, PXView.SortColumns, PXView.Descendings, this.TransactionFilters, ref num1, isSyncPosition ? PXView.MaximumRows : 0, ref num2).Cast<PXResult>().Where<PXResult>((Func<PXResult, bool>) (_ => GroupHelper.IsAccessibleToUser(((PXGraph) this).Caches[typeof (PX.Objects.GL.ADL.Sub)], (object) _.GetItem<PX.Objects.GL.ADL.Sub>(), ((PXGraph) this).Accessinfo.UserName, true)));
  }

  private IEnumerable<PXResult> ExecuteCommandForSummary()
  {
    int num1 = 0;
    int num2 = 0;
    return ((PXSelectBase) this.Command).View.Select((object[]) null, new object[1]
    {
      (object) ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.OrgBAccountID
    }, new object[PXView.SortColumns.Length], PXView.SortColumns, PXView.Descendings, this.TransactionFilters, ref num1, 0, ref num2).Cast<PXResult>();
  }

  protected virtual void AccountByPeriodFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is AccountByPeriodFilter row))
      return;
    this.ShowDetailColumns(!row.ShowSummary.GetValueOrDefault());
    AccountByPeriodFilter accountByPeriodFilter1 = row;
    bool? nullable1;
    int num1;
    if (row.IncludeUnposted.GetValueOrDefault())
    {
      nullable1 = row.IncludeUnreleased;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool? nullable2 = new bool?(num1 != 0);
    accountByPeriodFilter1.IncludeUnreleased = nullable2;
    PXCache pxCache1 = cache;
    AccountByPeriodFilter accountByPeriodFilter2 = row;
    nullable1 = row.IncludeUnposted;
    int num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<AccountByPeriodFilter.includeUnreleased>(pxCache1, (object) accountByPeriodFilter2, num2 != 0);
    PXCache pxCache2 = cache;
    AccountByPeriodFilter accountByPeriodFilter3 = row;
    nullable1 = row.ShowSummary;
    int num3 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<AccountByPeriodFilter.includeReclassified>(pxCache2, (object) accountByPeriodFilter3, num3 != 0);
    DateTime? nullable3;
    if (row.AccountID.HasValue)
    {
      Account account;
      if (((PXSelectBase<Account>) this.AccountInfo).Current != null)
      {
        int? accountId1 = row.AccountID;
        int? accountId2 = ((PXSelectBase<Account>) this.AccountInfo).Current.AccountID;
        if (accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue)
        {
          account = ((PXSelectBase<Account>) this.AccountInfo).Current;
          goto label_9;
        }
      }
      account = PXResultset<Account>.op_Implicit(((PXSelectBase<Account>) this.AccountInfo).Select(Array.Empty<object>()));
label_9:
      bool flag = !string.IsNullOrEmpty(account.CuryID);
      PXUIFieldAttribute.SetEnabled<AccountByPeriodFilter.showCuryDetail>(cache, e.Row, flag);
      if (!flag)
        row.ShowCuryDetail = new bool?(false);
      nullable3 = row.EndDate;
      if (nullable3.HasValue)
      {
        nullable3 = row.PeriodEndDate;
        if (nullable3.HasValue)
        {
          nullable3 = row.EndDate;
          DateTime? periodEndDate = row.PeriodEndDate;
          if ((nullable3.HasValue & periodEndDate.HasValue ? (nullable3.GetValueOrDefault() > periodEndDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            cache.RaiseExceptionHandling<AccountByPeriodFilter.endDateUI>(e.Row, (object) row.EndDateUI, (Exception) new PXSetPropertyException("To have an effect Date must be set between Period Start Date and Period End Date.", (PXErrorLevel) 2));
            goto label_16;
          }
        }
      }
      cache.RaiseExceptionHandling<AccountByPeriodFilter.endDateUI>(e.Row, (object) null, (Exception) null);
    }
label_16:
    PXCache cache1 = ((PXSelectBase) this.GLTranEnq).Cache;
    nullable1 = row.ShowCuryDetail;
    int num4 = nullable1.GetValueOrDefault() ? 1 : 0;
    this.SetVisibleCuryFields(cache1, num4 != 0);
    AccountByPeriodFilter current = ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current;
    int num5;
    if (current == null)
    {
      num5 = 0;
    }
    else
    {
      nullable1 = current.UseMasterCalendar;
      num5 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    bool flag1 = num5 != 0 && PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>();
    PXUIFieldAttribute.SetVisible<GLTranR.finPeriodID>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, !flag1);
    PXUIFieldAttribute.SetVisible<GLTranR.tranPeriodID>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, flag1);
    DateTime? nullable4;
    if (row.EndDate.HasValue)
    {
      nullable4 = row.PeriodEndDate;
      if (nullable4.HasValue)
      {
        nullable4 = row.EndDate;
        nullable3 = row.PeriodEndDate;
        if ((nullable4.HasValue & nullable3.HasValue ? (nullable4.GetValueOrDefault() > nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_24;
      }
      nullable3 = row.PeriodStartDate;
      if (nullable3.HasValue)
      {
        nullable3 = row.EndDate;
        nullable4 = row.PeriodStartDate;
        if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          goto label_25;
      }
      else
        goto label_25;
label_24:
      cache.RaiseExceptionHandling<AccountByPeriodFilter.endDateUI>(e.Row, (object) row.EndDateUI, (Exception) new PXSetPropertyException("To have an effect Date must be set between Period Start Date and Period End Date.", (PXErrorLevel) 2));
      goto label_26;
    }
label_25:
    cache.RaiseExceptionHandling<AccountByPeriodFilter.endDateUI>(e.Row, (object) null, (Exception) null);
label_26:
    nullable4 = row.StartDate;
    if (nullable4.HasValue)
    {
      nullable4 = row.PeriodStartDate;
      if (nullable4.HasValue)
      {
        nullable4 = row.StartDate;
        nullable3 = row.PeriodStartDate;
        if ((nullable4.HasValue & nullable3.HasValue ? (nullable4.GetValueOrDefault() < nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_31;
      }
      nullable3 = row.PeriodEndDate;
      if (nullable3.HasValue)
      {
        nullable3 = row.StartDate;
        nullable4 = row.PeriodEndDate;
        if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          goto label_32;
      }
      else
        goto label_32;
label_31:
      cache.RaiseExceptionHandling<AccountByPeriodFilter.startDateUI>(e.Row, (object) row.StartDateUI, (Exception) new PXSetPropertyException("To have an effect Date must be set between Period Start Date and Period End Date.", (PXErrorLevel) 2));
      return;
    }
label_32:
    cache.RaiseExceptionHandling<AccountByPeriodFilter.startDateUI>(e.Row, (object) null, (Exception) null);
  }

  protected virtual void AccountByPeriodFilter_StartPeriodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    AccountByPeriodFilter row = (AccountByPeriodFilter) e.Row;
    if (string.CompareOrdinal(row.StartPeriodID, row.EndPeriodID) > 0)
      cache.SetValue<AccountByPeriodFilter.endPeriodID>(e.Row, (object) row.StartPeriodID);
    this.ResetFilterDates(row);
  }

  protected virtual void AccountByPeriodFilter_EndPeriodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    AccountByPeriodFilter row = (AccountByPeriodFilter) e.Row;
    if (string.CompareOrdinal(row.StartPeriodID, row.EndPeriodID) > 0)
      cache.SetValue<AccountByPeriodFilter.startPeriodID>(e.Row, (object) row.EndPeriodID);
    this.ResetFilterDates(row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<AccountByPeriodFilter.includeReclassified> e)
  {
    if (((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<AccountByPeriodFilter.includeReclassified>>) e).NewValue != null)
      return;
    object obj;
    ((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<AccountByPeriodFilter.includeReclassified>>) e).Cache.RaiseFieldDefaulting<AccountByPeriodFilter.includeReclassified>(e.Row, ref obj);
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<AccountByPeriodFilter.includeReclassified>>) e).NewValue = obj;
  }

  protected virtual void AccountByPeriodFilter_UseMasterCalendar_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    AccountByPeriodFilter row = (AccountByPeriodFilter) e.Row;
    if (row == null)
      return;
    bool? oldValue = (bool?) e.OldValue;
    bool? useMasterCalendar = (bool?) row?.UseMasterCalendar;
    if (oldValue.GetValueOrDefault() == useMasterCalendar.GetValueOrDefault() & oldValue.HasValue == useMasterCalendar.HasValue)
      return;
    this.ResetFilterDates(row);
  }

  protected virtual void AccountByPeriodFilter_ShowSummary_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    AccountByPeriodFilter row = (AccountByPeriodFilter) e.Row;
    if (row.ShowSummary.GetValueOrDefault())
    {
      this.ResetFilterDates(row);
      row.IncludeReclassified = new bool?(false);
    }
    ((PXSelectBase) this.GLTranEnq).Cache.Clear();
    ((PXSelectBase) this.GLTranEnq).Cache.ClearQueryCacheObsolete();
  }

  protected virtual void AccountByPeriodFilter_SubID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<AccountByPeriodFilter, AccountByPeriodFilter.orgBAccountID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<AccountByPeriodFilter, AccountByPeriodFilter.orgBAccountID>>) e).Cache.SetDefaultExt<AccountByPeriodFilter.ledgerID>((object) e.Row);
  }

  public virtual bool IsDirty => false;

  protected virtual void GLTranR_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    GLTranR row = (GLTranR) e.Row;
    if (row == null)
      return;
    JournalEntry.SetReclassTranWarningsIfNeed(sender, (GLTran) row);
    bool flag = this.CannotBeReclassified(row);
    PXUIFieldAttribute.SetReadOnly<GLTran.selected>(sender, (object) row, flag);
  }

  protected virtual void GLTranR_Selected_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ((PXAction) this.reclassify).SetEnabled(this.GetSelectedTrans().Any<GLTranR>());
  }

  private IReadOnlyCollection<GLTranR> GetSelectedTrans()
  {
    return (IReadOnlyCollection<GLTranR>) ((PXSelectBase) this.GLTranEnq).Cache.Updated.Cast<GLTranR>().Where<GLTranR>((Func<GLTranR, bool>) (tran => tran.Selected.GetValueOrDefault())).ToArray<GLTranR>();
  }

  public bool CannotBeReclassified(GLTranR tran)
  {
    if (JournalEntry.IsTransactionReclassifiable((GLTran) tran, tran.BatchType, this.CurrentLedger.BalanceType, ProjectDefaultAttribute.NonProject()))
      return false;
    bool? showSummary = ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.ShowSummary;
    bool flag = false;
    return showSummary.GetValueOrDefault() == flag & showSummary.HasValue;
  }

  private PXFilterRow[] GetApplicableFilters(IEnumerable<PXFilterRow> filters)
  {
    return filters.Where<PXFilterRow>((Func<PXFilterRow, bool>) (f => (!((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current.ShowSummary.GetValueOrDefault() || !string.Equals(f.DataField, typeof (GLTranR.batchNbr).Name, StringComparison.OrdinalIgnoreCase)) && !string.Equals(f.DataField, typeof (GLTranR.begBalance).Name, StringComparison.OrdinalIgnoreCase) && !string.Equals(f.DataField, typeof (GLTranR.endBalance).Name, StringComparison.OrdinalIgnoreCase) && !string.Equals(f.DataField, typeof (GLTranR.curyEndBalance).Name, StringComparison.OrdinalIgnoreCase) && !string.Equals(f.DataField, typeof (GLTranR.curyBegBalance).Name, StringComparison.OrdinalIgnoreCase))).ToArray<PXFilterRow>();
  }

  public static void Copy(GLHistoryEnqFilter aDest, AccountByPeriodFilter aSrc)
  {
    aDest.AccountID = aSrc.AccountID;
    aDest.SubCD = aSrc.SubID;
    aDest.LedgerID = aSrc.LedgerID;
    aDest.FinPeriodID = aSrc.StartPeriodID;
    aDest.BranchID = aSrc.BranchID;
    aDest.OrganizationID = aSrc.OrganizationID;
    aDest.OrgBAccountID = aSrc.OrgBAccountID;
    aDest.UseMasterCalendar = aSrc.UseMasterCalendar;
  }

  protected virtual void RetrieveStartingBalance(
    out Decimal balance,
    out Decimal? aCuryBalance,
    out string aCuryID)
  {
    balance = 0M;
    aCuryBalance = new Decimal?(0M);
    aCuryID = (string) null;
    AccountByPeriodFilter current1 = ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current;
    if (current1 == null || !current1.AccountID.HasValue || !current1.LedgerID.HasValue)
      return;
    AccountHistoryEnq instance = PXGraph.CreateInstance<AccountHistoryEnq>();
    GLHistoryEnqFilter current2 = ((PXSelectBase<GLHistoryEnqFilter>) instance.Filter).Current;
    AccountByPeriodEnq.Copy(current2, current1);
    ((PXSelectBase<GLHistoryEnqFilter>) instance.Filter).Update(current2);
    bool flag = true;
    foreach (PXResult<GLHistoryEnquiryResult> pxResult in ((PXSelectBase<GLHistoryEnquiryResult>) instance.EnqResult).Select(Array.Empty<object>()))
    {
      GLHistoryEnquiryResult historyEnquiryResult = PXResult<GLHistoryEnquiryResult>.op_Implicit(pxResult);
      balance += historyEnquiryResult.BegBalance.GetValueOrDefault();
      if (flag)
      {
        aCuryID = historyEnquiryResult.CuryID;
        flag = false;
      }
      if (aCuryID != null && aCuryID == historyEnquiryResult.CuryID)
      {
        ref Decimal? local = ref aCuryBalance;
        Decimal? nullable1 = aCuryBalance;
        Decimal? curyBegBalance = historyEnquiryResult.CuryBegBalance;
        Decimal? nullable2 = nullable1.HasValue & curyBegBalance.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + curyBegBalance.GetValueOrDefault()) : new Decimal?();
        local = nullable2;
      }
      else
      {
        aCuryID = (string) null;
        aCuryBalance = new Decimal?();
      }
    }
    string aCuryID1 = aCuryID;
    Decimal aAjust;
    Decimal? aCuryAdjust;
    this.RetrieveStartingBalanceAdjustment(out aAjust, out aCuryAdjust, ref aCuryID1);
    balance += aAjust;
    if (aCuryID != null && aCuryID1 == aCuryID)
    {
      ref Decimal? local = ref aCuryBalance;
      Decimal? nullable3 = aCuryBalance;
      Decimal? nullable4 = aCuryAdjust;
      Decimal? nullable5 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      local = nullable5;
    }
    else
    {
      aCuryBalance = new Decimal?();
      aCuryID = (string) null;
    }
  }

  protected virtual void RetrieveStartingBalanceAdjustment(
    out Decimal aAjust,
    out Decimal? aCuryAdjust,
    ref string aCuryID)
  {
    aAjust = 0M;
    aCuryAdjust = new Decimal?(0M);
    AccountByPeriodFilter current = ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Current;
    if (current == null || !current.AccountID.HasValue || !current.LedgerID.HasValue || this.CurrentStartPeriod == null)
      return;
    Decimal aCreditAmt1 = 0M;
    Decimal aDebitAmt1 = 0M;
    Decimal aDebitAmt2 = 0M;
    Decimal aCreditAmt2 = 0M;
    string aAcctType = "E";
    bool flag1 = true;
    bool flag2 = true;
    if (current.StartDate.HasValue)
    {
      PXSelectBase<GLTran> pxSelectBase1 = (PXSelectBase<GLTran>) new PXSelectJoinGroupBy<GLTran, InnerJoin<PX.Objects.GL.ADL.Account, On<GLTran.accountID, Equal<PX.Objects.GL.ADL.Account.accountID>>>, Where<GLTran.ledgerID, Equal<Current<AccountByPeriodFilter.ledgerID>>, And<GLTran.accountID, Equal<Current<AccountByPeriodFilter.accountID>>, And<GLTran.finPeriodID, GreaterEqual<Current<AccountByPeriodFilter.startPeriodID>>, And<GLTran.finPeriodID, LessEqual<Current<AccountByPeriodFilter.endPeriodID>>, And<GLTran.tranDate, Less<Current<AccountByPeriodFilter.startDate>>>>>>>, Aggregate<Sum<GLTran.debitAmt, Sum<GLTran.creditAmt, Sum<GLTran.curyCreditAmt, Sum<GLTran.curyDebitAmt, GroupBy<GLTran.accountID>>>>>>>((PXGraph) this);
      int[] filteringBranchIds = this.FilteringBranchIDs;
      if (filteringBranchIds != null)
        pxSelectBase1.WhereAnd<Where<GLTran.branchID, In<Required<AccountByPeriodFilter.branchID>>, And<MatchWithBranch<GLTran.branchID>>>>();
      if (!SubCDUtils.IsSubCDEmpty(current.SubID))
      {
        pxSelectBase1.Join<InnerJoin<Sub, On<GLTran.subID, Equal<Sub.subID>>>>();
        pxSelectBase1.WhereAnd<Where<Sub.subCD, Like<Current<AccountByPeriodFilter.subCDWildcard>>>>();
      }
      PXSelectBase<GLTran> pxSelectBase2 = pxSelectBase1;
      object[] objArray;
      if (filteringBranchIds != null)
        objArray = new object[1]
        {
          (object) filteringBranchIds
        };
      else
        objArray = (object[]) null;
      foreach (PXResult<GLTran, PX.Objects.GL.ADL.Account> pxResult in pxSelectBase2.Select(objArray))
      {
        GLTran glTran = PXResult<GLTran, PX.Objects.GL.ADL.Account>.op_Implicit(pxResult);
        PX.Objects.GL.ADL.Account account = PXResult<GLTran, PX.Objects.GL.ADL.Account>.op_Implicit(pxResult);
        Decimal num1 = aDebitAmt1;
        Decimal? nullable = glTran.DebitAmt;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        aDebitAmt1 = num1 + valueOrDefault1;
        Decimal num2 = aCreditAmt1;
        nullable = glTran.CreditAmt;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        aCreditAmt1 = num2 + valueOrDefault2;
        if (flag2)
        {
          aCuryID = account.CuryID;
          flag2 = false;
        }
        if (flag1 && account.CuryID == aCuryID)
        {
          Decimal num3 = aDebitAmt2;
          nullable = glTran.CuryDebitAmt;
          Decimal valueOrDefault3 = nullable.GetValueOrDefault();
          aDebitAmt2 = num3 + valueOrDefault3;
          Decimal num4 = aCreditAmt2;
          nullable = glTran.CuryCreditAmt;
          Decimal valueOrDefault4 = nullable.GetValueOrDefault();
          aCreditAmt2 = num4 + valueOrDefault4;
        }
        else
          flag1 = false;
        aAcctType = account.Type;
      }
    }
    aAjust = AccountRules.CalcSaldo(aAcctType, aDebitAmt1, aCreditAmt1);
    if (flag1)
    {
      aCuryAdjust = new Decimal?(AccountRules.CalcSaldo(aAcctType, aDebitAmt2, aCreditAmt2));
    }
    else
    {
      aCuryAdjust = new Decimal?();
      aCuryID = (string) null;
    }
  }

  protected virtual void ResetFilterDates(AccountByPeriodFilter aRow)
  {
    int? calendarOrganizationId = this.FinPeriodRepository.GetCalendarOrganizationID(aRow.OrganizationID, aRow.BranchID, aRow.UseMasterCalendar);
    FinPeriod byId1 = this.FinPeriodRepository.FindByID(calendarOrganizationId, aRow.StartPeriodID);
    FinPeriod byId2 = this.FinPeriodRepository.FindByID(calendarOrganizationId, aRow.EndPeriodID);
    if (byId1 != null && byId2 != null)
    {
      AccountByPeriodFilter accountByPeriodFilter1 = aRow;
      DateTime? nullable1 = byId1.StartDate;
      DateTime? nullable2 = byId2.StartDate;
      DateTime? nullable3 = (nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? byId1.StartDate : byId2.StartDate;
      accountByPeriodFilter1.PeriodStartDate = nullable3;
      AccountByPeriodFilter accountByPeriodFilter2 = aRow;
      nullable2 = byId2.EndDate;
      nullable1 = byId1.EndDate;
      DateTime? nullable4 = (nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? byId2.EndDate : byId1.EndDate;
      accountByPeriodFilter2.PeriodEndDate = nullable4;
      AccountByPeriodFilter accountByPeriodFilter3 = aRow;
      nullable1 = new DateTime?();
      DateTime? nullable5 = nullable1;
      accountByPeriodFilter3.EndDate = nullable5;
      AccountByPeriodFilter accountByPeriodFilter4 = aRow;
      nullable1 = new DateTime?();
      DateTime? nullable6 = nullable1;
      accountByPeriodFilter4.StartDate = nullable6;
    }
    else if (byId1 != null || byId2 != null)
    {
      FinPeriod finPeriod = byId1 ?? byId2;
      aRow.PeriodStartDate = finPeriod.StartDate;
      aRow.PeriodEndDate = finPeriod.EndDate;
    }
    else
    {
      aRow.PeriodStartDate = new DateTime?();
      aRow.PeriodEndDate = new DateTime?();
    }
  }

  private void ShowDetailColumns(bool needShow)
  {
    PXUIFieldAttribute.SetVisible<GLTranR.module>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, needShow);
    PXUIFieldAttribute.SetVisible<GLTranR.batchNbr>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, needShow);
    PXUIFieldAttribute.SetVisible<GLTranR.accountID>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, needShow);
    PXUIFieldAttribute.SetVisible<GLTranR.subID>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, needShow);
    PXUIFieldAttribute.SetVisible<GLTranR.refNbr>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, needShow);
    PXUIFieldAttribute.SetVisible<GLTranR.tranDesc>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, needShow);
    PXUIFieldAttribute.SetVisible<GLTranR.selected>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, needShow);
    PXUIFieldAttribute.SetVisible<GLTranR.reclassBatchNbr>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, needShow);
    PXUIFieldAttribute.SetVisible<GLTranR.branchID>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, needShow);
    PXUIFieldAttribute.SetVisible<GLTranR.referenceID>(((PXSelectBase) this.GLTranEnq).Cache, (object) null, needShow);
  }

  private void SwitchToDetailsOfGroupedRow(AccountByPeriodFilter filter)
  {
    GLTranR current = ((PXSelectBase<GLTranR>) this.GLTranEnq).Current;
    filter.ShowSummary = new bool?(false);
    AccountByPeriodFilter accountByPeriodFilter = filter;
    DateTime dateTime = current.TranDate.Value;
    int year = dateTime.Year;
    dateTime = current.TranDate.Value;
    int month = dateTime.Month;
    dateTime = current.TranDate.Value;
    int day = dateTime.Day;
    DateTime? nullable = new DateTime?(new DateTime(year, month, day));
    accountByPeriodFilter.StartDate = nullable;
    filter.EndDate = new DateTime?(filter.StartDate.Value.AddDays(1.0));
    ((PXSelectBase<AccountByPeriodFilter>) this.Filter).Update(filter);
  }

  private void RedirectToBatch(GLTran tran)
  {
    Batch batch = JournalEntry.FindBatch((PXGraph) this, tran);
    if (batch == null)
      return;
    JournalEntry.RedirectToBatch(batch);
  }

  private void SetOrigAmounts(GLTranR tran)
  {
    if (tran.OrigDebitAmt.HasValue && tran.OrigCreditAmt.HasValue && tran.CuryOrigDebitAmt.HasValue && tran.CuryOrigCreditAmt.HasValue)
      return;
    tran.OrigDebitAmt = tran.DebitAmt;
    tran.OrigCreditAmt = tran.CreditAmt;
    tran.CuryOrigDebitAmt = tran.CuryDebitAmt;
    tran.CuryOrigCreditAmt = tran.CuryCreditAmt;
  }

  public class RedirectToSourceDocumentFromAccountByPeriodEnqExtension : 
    RedirectToSourceDocumentExtensionBase<AccountByPeriodEnq>
  {
    public static bool IsActive() => true;
  }

  [Obsolete("The type is obsolete and will be removed in Acumatica 8.0. Use JournalEntry.OpenDocumentByTran method.")]
  public class GraphFactory
  {
    public IDocGraphCreator this[string tranModule]
    {
      get
      {
        if (tranModule != null && tranModule.Length == 2)
        {
          switch (tranModule[0])
          {
            case 'A':
              switch (tranModule)
              {
                case "AP":
                  return (IDocGraphCreator) new APDocGraphCreator();
                case "AR":
                  return (IDocGraphCreator) new ARDocGraphCreator();
              }
              break;
            case 'C':
              if (tranModule == "CA")
                return (IDocGraphCreator) new CADocGraphCreator();
              break;
            case 'D':
              if (tranModule == "DR")
                return (IDocGraphCreator) new DRDocGraphCreator();
              break;
            case 'F':
              if (tranModule == "FA")
                return (IDocGraphCreator) new FADocGraphCreator();
              break;
            case 'I':
              if (tranModule == "IN")
                return (IDocGraphCreator) new INDocGraphCreator();
              break;
            case 'P':
              if (tranModule == "PM")
                return (IDocGraphCreator) new PMDocGraphCreator();
              break;
          }
        }
        return (IDocGraphCreator) null;
      }
    }
  }
}
