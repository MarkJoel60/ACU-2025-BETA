// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountHistoryByYearEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
public class AccountHistoryByYearEnq : PXGraph<AccountHistoryByYearEnq>
{
  public PXCancel<AccountByYearFilter> Cancel;
  public PXAction<AccountByYearFilter> PreviousPeriod;
  public PXAction<AccountByYearFilter> NextPeriod;
  public PXFilter<AccountByYearFilter> Filter;
  public PXAction<AccountByYearFilter> accountDetails;
  public PXAction<AccountByYearFilter> accountBySub;
  [PXFilterable(new Type[] {})]
  public PXSelectOrderBy<GLHistoryEnquiryResult, OrderBy<Asc<GLHistoryEnquiryResult.lastActivityPeriod>>> EnqResult;
  public PXSetup<GLSetup> glsetup;
  public PXSelect<Account, Where<Account.accountID, Equal<Current<AccountByYearFilter.accountID>>>> AccountInfo;

  [AccountAny]
  protected virtual void GLHistoryEnquiryResult_AccountID_CacheAttached(PXCache sender)
  {
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXUIField]
  protected virtual void GLHistoryEnquiryResult_LastActivityPeriod_CacheAttached(PXCache sender)
  {
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public PX.Objects.GL.FinPeriods.TableDefinition.FinYear fiscalyear
  {
    get
    {
      return PXResultset<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>.op_Implicit(PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, PXSelect<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, Equal<Required<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>, And<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Required<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.FinYear,
        (object) this.FinPeriodRepository.GetCalendarOrganizationID(((PXSelectBase<AccountByYearFilter>) this.Filter).Current.OrganizationID, ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.BranchID, ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.UseMasterCalendar)
      }));
    }
  }

  private AccountByYearFilter CurrentFilter
  {
    get => ((PXSelectBase<AccountByYearFilter>) this.Filter).Current;
  }

  public AccountHistoryByYearEnq()
  {
    GLSetup current = ((PXSelectBase<GLSetup>) this.glsetup).Current;
    ((PXSelectBase) this.EnqResult).Cache.AllowInsert = false;
    ((PXSelectBase) this.EnqResult).Cache.AllowDelete = false;
    ((PXSelectBase) this.EnqResult).Cache.AllowUpdate = false;
  }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable previousperiod(PXAdapter adapter)
  {
    AccountByYearFilter currentFilter = this.CurrentFilter;
    int? calendarOrganizationId = this.FinPeriodRepository.GetCalendarOrganizationID(currentFilter.OrganizationID, currentFilter.BranchID, currentFilter.UseMasterCalendar);
    PXResultset<PX.Objects.GL.FinPeriods.TableDefinition.FinYear> pxResultset = PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, PXSelect<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, Less<Required<AccountByYearFilter.finYear>>, And<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Required<GLHistoryEnqFilter.organizationID>>>>, OrderBy<Desc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) currentFilter.FinYear,
      (object) calendarOrganizationId
    });
    if (pxResultset == null)
      pxResultset = PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, PXSelect<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Required<GLHistoryEnqFilter.organizationID>>>, OrderBy<Desc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) calendarOrganizationId
      });
    PX.Objects.GL.FinPeriods.TableDefinition.FinYear finYear = PXResultset<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>.op_Implicit(pxResultset);
    currentFilter.FinYear = finYear?.Year;
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable nextperiod(PXAdapter adapter)
  {
    AccountByYearFilter currentFilter = this.CurrentFilter;
    int? calendarOrganizationId = this.FinPeriodRepository.GetCalendarOrganizationID(currentFilter.OrganizationID, currentFilter.BranchID, currentFilter.UseMasterCalendar);
    PXResultset<PX.Objects.GL.FinPeriods.TableDefinition.FinYear> pxResultset = PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, PXSelect<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, Greater<Required<AccountByYearFilter.finYear>>, And<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Required<GLHistoryEnqFilter.organizationID>>>>, OrderBy<Asc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) currentFilter.FinYear,
      (object) calendarOrganizationId
    });
    if (pxResultset == null)
      pxResultset = PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, PXSelect<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Required<GLHistoryEnqFilter.organizationID>>>, OrderBy<Asc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) calendarOrganizationId
      });
    PX.Objects.GL.FinPeriods.TableDefinition.FinYear finYear = PXResultset<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>.op_Implicit(pxResultset);
    currentFilter.FinYear = finYear?.Year;
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Account Details")]
  [PXButton]
  protected virtual IEnumerable AccountDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current == null)
      return adapter.Get();
    int? accountId = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.AccountID;
    int? ytdNetIncAccountId = ((PXSelectBase<GLSetup>) this.glsetup).Current.YtdNetIncAccountID;
    if (accountId.GetValueOrDefault() == ytdNetIncAccountId.GetValueOrDefault() & accountId.HasValue == ytdNetIncAccountId.HasValue)
      throw new PXException("Year to Date Net Income account cannot be selected for inquiry.");
    AccountByPeriodEnq instance = PXGraph.CreateInstance<AccountByPeriodEnq>();
    AccountByPeriodFilter copy = PXCache<AccountByPeriodFilter>.CreateCopy(((PXSelectBase<AccountByPeriodFilter>) instance.Filter).Current);
    copy.OrgBAccountID = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.OrgBAccountID;
    copy.OrganizationID = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.OrganizationID;
    copy.BranchID = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.BranchID;
    copy.LedgerID = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.LedgerID;
    copy.AccountID = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.AccountID;
    copy.SubID = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.SubCD;
    copy.StartPeriodID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.LastActivityPeriod;
    copy.EndPeriodID = copy.StartPeriodID;
    copy.ShowCuryDetail = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.ShowCuryDetail;
    ((PXSelectBase<AccountByPeriodFilter>) instance.Filter).Update(copy);
    ((PXSelectBase<AccountByPeriodFilter>) instance.Filter).Select(Array.Empty<object>());
    throw new PXRedirectRequiredException((PXGraph) instance, "Account Details");
  }

  [PXUIField(DisplayName = "Account by Subaccount")]
  [PXButton]
  protected virtual IEnumerable AccountBySub(PXAdapter adapter)
  {
    if (((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current != null)
    {
      AccountHistoryBySubEnq instance = PXGraph.CreateInstance<AccountHistoryBySubEnq>();
      GLHistoryEnqFilter copy = PXCache<GLHistoryEnqFilter>.CreateCopy(((PXSelectBase<GLHistoryEnqFilter>) instance.Filter).Current);
      copy.AccountID = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.AccountID;
      copy.LedgerID = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.LedgerID;
      copy.OrgBAccountID = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.OrgBAccountID;
      copy.OrganizationID = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.OrganizationID;
      copy.BranchID = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.BranchID;
      copy.SubCD = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.SubCD;
      copy.FinPeriodID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.LastActivityPeriod;
      copy.ShowCuryDetail = ((PXSelectBase<AccountByYearFilter>) this.Filter).Current.ShowCuryDetail;
      ((PXSelectBase<GLHistoryEnqFilter>) instance.Filter).Update(copy);
      throw new PXRedirectRequiredException((PXGraph) instance, "Account by Subaccount");
    }
    return adapter.Get();
  }

  protected virtual IEnumerable enqResult()
  {
    AccountHistoryByYearEnq historyByYearEnq = this;
    AccountByYearFilter filter = historyByYearEnq.CurrentFilter;
    bool flag = filter.ShowCuryDetail.HasValue && filter.ShowCuryDetail.Value;
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyID>(((PXSelectBase) historyByYearEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyPtdCreditTotal>(((PXSelectBase) historyByYearEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyPtdDebitTotal>(((PXSelectBase) historyByYearEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyBegBalance>(((PXSelectBase) historyByYearEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyEndBalance>(((PXSelectBase) historyByYearEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.signCuryBegBalance>(((PXSelectBase) historyByYearEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.signCuryEndBalance>(((PXSelectBase) historyByYearEnq.EnqResult).Cache, (object) null, flag);
    if (filter.AccountID.HasValue && filter.LedgerID.HasValue && filter.FinYear != null)
    {
      using (new PXReadBranchRestrictedScope((int?[]) null, (int?[]) null, false, false))
      {
        PXSelectBase<GLHistoryByPeriod> pxSelectBase1 = (PXSelectBase<GLHistoryByPeriod>) new PXSelectJoinGroupBy<GLHistoryByPeriod, LeftJoin<Account, On<GLHistoryByPeriod.accountID, Equal<Account.accountID>, And<Match<Account, Current<AccessInfo.userName>>>>, LeftJoin<MasterFinPeriod, On<GLHistoryByPeriod.finPeriodID, Equal<MasterFinPeriod.finPeriodID>>, LeftJoin<Sub, On<GLHistoryByPeriod.subID, Equal<Sub.subID>, And<Match<Sub, Current<AccessInfo.userName>>>>, LeftJoin<GLHistory, On<GLHistoryByPeriod.accountID, Equal<GLHistory.accountID>, And<GLHistoryByPeriod.subID, Equal<GLHistory.subID>, And<GLHistoryByPeriod.branchID, Equal<GLHistory.branchID>, And<GLHistoryByPeriod.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryByPeriod.finPeriodID, Equal<GLHistory.finPeriodID>>>>>>, LeftJoin<AH, On<GLHistoryByPeriod.ledgerID, Equal<AH.ledgerID>, And<GLHistoryByPeriod.branchID, Equal<AH.branchID>, And<GLHistoryByPeriod.accountID, Equal<AH.accountID>, And<GLHistoryByPeriod.subID, Equal<AH.subID>, And<GLHistoryByPeriod.lastActivityPeriod, Equal<AH.finPeriodID>>>>>>>>>>>, Where<GLHistoryByPeriod.ledgerID, Equal<Current<AccountByYearFilter.ledgerID>>, And<MasterFinPeriod.finYear, Equal<Current<AccountByYearFilter.finYear>>, And<GLHistoryByPeriod.accountID, Equal<Current<AccountByYearFilter.accountID>>, And<Where2<Where<Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>, And<Where<Account.type, Equal<AccountType.asset>, Or<Account.type, Equal<AccountType.liability>>>>>, Or<Where<GLHistoryByPeriod.lastActivityPeriod, GreaterEqual<Required<GLHistoryByPeriod.lastActivityPeriod>>, And<Where<Account.type, Equal<AccountType.expense>, Or<Account.type, Equal<AccountType.income>, Or<Account.accountID, Equal<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>>>>>, Aggregate<Sum<AH.finYtdBalance, Sum<AH.tranYtdBalance, Sum<AH.curyFinYtdBalance, Sum<AH.curyTranYtdBalance, Sum<GLHistory.finPtdDebit, Sum<GLHistory.tranPtdDebit, Sum<GLHistory.finPtdCredit, Sum<GLHistory.tranPtdCredit, Sum<GLHistory.finBegBalance, Sum<GLHistory.tranBegBalance, Sum<GLHistory.finYtdBalance, Sum<GLHistory.tranYtdBalance, Sum<GLHistory.curyFinBegBalance, Sum<GLHistory.curyTranBegBalance, Sum<GLHistory.curyFinYtdBalance, Sum<GLHistory.curyTranYtdBalance, Sum<GLHistory.curyFinPtdCredit, Sum<GLHistory.curyTranPtdCredit, Sum<GLHistory.curyFinPtdDebit, Sum<GLHistory.curyTranPtdDebit, GroupBy<GLHistoryByPeriod.ledgerID, GroupBy<GLHistoryByPeriod.accountID, GroupBy<GLHistoryByPeriod.finPeriodID>>>>>>>>>>>>>>>>>>>>>>>>>((PXGraph) historyByYearEnq);
        int? nullable1 = filter.SubID;
        if (nullable1.HasValue)
          pxSelectBase1.WhereAnd<Where<GLHistoryByPeriod.subID, Equal<Current<AccountByYearFilter.subID>>>>();
        int[] numArray = (int[]) null;
        nullable1 = filter.OrgBAccountID;
        if (nullable1.HasValue)
          pxSelectBase1.WhereAnd<Where<GLHistoryByPeriod.branchID, InsideBranchesOf<Current<GLHistoryEnqFilter.orgBAccountID>>, And<MatchWithBranch<GLHistoryByPeriod.branchID>>>>();
        if (!SubCDUtils.IsSubCDEmpty(filter.SubCD))
          pxSelectBase1.WhereAnd<Where<Sub.subCD, Like<Current<AccountByYearFilter.subCDWildcard>>>>();
        PXSelectBase<GLHistoryByPeriod> pxSelectBase2 = pxSelectBase1;
        object[] objArray = new object[2]
        {
          (object) filter.BegFinPeriod,
          (object) numArray
        };
        foreach (PXResult<GLHistoryByPeriod, Account, MasterFinPeriod, Sub, GLHistory, AH> pxResult in pxSelectBase2.Select(objArray))
        {
          GLHistoryByPeriod glHistoryByPeriod = PXResult<GLHistoryByPeriod, Account, MasterFinPeriod, Sub, GLHistory, AH>.op_Implicit(pxResult);
          Account account = PXResult<GLHistoryByPeriod, Account, MasterFinPeriod, Sub, GLHistory, AH>.op_Implicit(pxResult);
          GLHistory glHistory1 = PXResult<GLHistoryByPeriod, Account, MasterFinPeriod, Sub, GLHistory, AH>.op_Implicit(pxResult);
          AH ah1 = PXResult<GLHistoryByPeriod, Account, MasterFinPeriod, Sub, GLHistory, AH>.op_Implicit(pxResult);
          GLHistory glHistory2 = glHistory1;
          bool? useMasterCalendar = filter.UseMasterCalendar;
          bool? nullable2 = new bool?(!useMasterCalendar.GetValueOrDefault());
          glHistory2.FinFlag = nullable2;
          AH ah2 = ah1;
          useMasterCalendar = filter.UseMasterCalendar;
          bool? nullable3 = new bool?(!useMasterCalendar.GetValueOrDefault());
          ah2.FinFlag = nullable3;
          GLHistoryEnquiryResult historyEnquiryResult1 = new GLHistoryEnquiryResult();
          historyEnquiryResult1.AccountID = glHistoryByPeriod.AccountID;
          historyEnquiryResult1.AccountCD = account.AccountCD;
          historyEnquiryResult1.LedgerID = glHistoryByPeriod.LedgerID;
          historyEnquiryResult1.LastActivityPeriod = glHistoryByPeriod.FinPeriodID;
          historyEnquiryResult1.PtdCreditTotal = new Decimal?(glHistory1.PtdCredit.GetValueOrDefault());
          Decimal? nullable4 = glHistory1.PtdDebit;
          historyEnquiryResult1.PtdDebitTotal = new Decimal?(nullable4.GetValueOrDefault());
          historyEnquiryResult1.CuryID = ah1.CuryID;
          historyEnquiryResult1.Type = account.Type;
          nullable4 = ah1.YtdBalance;
          historyEnquiryResult1.EndBalance = new Decimal?(nullable4.GetValueOrDefault());
          GLHistoryEnquiryResult historyEnquiryResult2 = historyEnquiryResult1;
          if (!string.IsNullOrEmpty(ah1.CuryID))
          {
            GLHistoryEnquiryResult historyEnquiryResult3 = historyEnquiryResult2;
            nullable4 = ah1.CuryYtdBalance;
            Decimal? nullable5 = new Decimal?(nullable4.GetValueOrDefault());
            historyEnquiryResult3.CuryEndBalance = nullable5;
            GLHistoryEnquiryResult historyEnquiryResult4 = historyEnquiryResult2;
            nullable4 = glHistory1.CuryPtdCredit;
            Decimal? nullable6 = new Decimal?(nullable4.GetValueOrDefault());
            historyEnquiryResult4.CuryPtdCreditTotal = nullable6;
            GLHistoryEnquiryResult historyEnquiryResult5 = historyEnquiryResult2;
            nullable4 = glHistory1.CuryPtdDebit;
            Decimal? nullable7 = new Decimal?(nullable4.GetValueOrDefault());
            historyEnquiryResult5.CuryPtdDebitTotal = nullable7;
          }
          else
          {
            GLHistoryEnquiryResult historyEnquiryResult6 = historyEnquiryResult2;
            nullable4 = new Decimal?();
            Decimal? nullable8 = nullable4;
            historyEnquiryResult6.CuryEndBalance = nullable8;
            GLHistoryEnquiryResult historyEnquiryResult7 = historyEnquiryResult2;
            nullable4 = new Decimal?();
            Decimal? nullable9 = nullable4;
            historyEnquiryResult7.CuryPtdCreditTotal = nullable9;
            GLHistoryEnquiryResult historyEnquiryResult8 = historyEnquiryResult2;
            nullable4 = new Decimal?();
            Decimal? nullable10 = nullable4;
            historyEnquiryResult8.CuryPtdDebitTotal = nullable10;
          }
          historyEnquiryResult2.recalculate(true);
          historyEnquiryResult2.recalculateSignAmount(((PXSelectBase<GLSetup>) historyByYearEnq.glsetup).Current?.TrialBalanceSign == "R");
          yield return (object) historyEnquiryResult2;
        }
      }
    }
  }

  public virtual bool IsDirty => false;

  protected virtual void AccountByYearFilter_AccountID_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || e.NewValue is string)
      return;
    Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account>.Config>.Search<Account.accountID>((PXGraph) this, e.NewValue, Array.Empty<object>()));
    if (account == null)
      return;
    e.NewValue = (object) account.AccountCD;
  }

  protected virtual void AccountByYearFilter_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is AccountByYearFilter row))
      return;
    if (!string.IsNullOrEmpty(row.FinPeriodID))
      row.FinYear = FinPeriodUtils.FiscalYear(row.FinPeriodID);
    if (!string.IsNullOrEmpty(row.FinYear))
      return;
    DateTime dateTime = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    row.FinYear = dateTime.Year.ToString("0000");
  }

  protected virtual void AccountByYearFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Select(Array.Empty<object>());
  }

  protected virtual void AccountByYearFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is GLHistoryEnqFilter row) || row == null)
      return;
    int? accountId1 = row.AccountID;
    if (!accountId1.HasValue)
      return;
    Account account;
    if (((PXSelectBase<Account>) this.AccountInfo).Current != null)
    {
      accountId1 = row.AccountID;
      int? accountId2 = ((PXSelectBase<Account>) this.AccountInfo).Current.AccountID;
      if (accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue)
      {
        account = ((PXSelectBase<Account>) this.AccountInfo).Current;
        goto label_6;
      }
    }
    account = PXResultset<Account>.op_Implicit(((PXSelectBase<Account>) this.AccountInfo).Select(Array.Empty<object>()));
label_6:
    bool flag = !string.IsNullOrEmpty(account.CuryID);
    PXUIFieldAttribute.SetEnabled<AccountByYearFilter.showCuryDetail>(cache, e.Row, flag);
    if (flag)
      return;
    row.ShowCuryDetail = new bool?(false);
  }

  protected virtual void AccountByYearFilter_SubCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void AccountByYearFilter_BranchID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<AccountByYearFilter.ledgerID>(e.Row);
  }

  protected virtual void AccountByYearFilter_OrganizationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<AccountByYearFilter.ledgerID>(e.Row);
  }
}
