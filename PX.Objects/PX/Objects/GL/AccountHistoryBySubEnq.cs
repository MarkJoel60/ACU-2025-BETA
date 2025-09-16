// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountHistoryBySubEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
public class AccountHistoryBySubEnq : PXGraph<AccountHistoryBySubEnq>
{
  public PXCancel<GLHistoryEnqFilter> Cancel;
  public PXAction<GLHistoryEnqFilter> PreviousPeriod;
  public PXAction<GLHistoryEnqFilter> NextPeriod;
  public PXFilter<GLHistoryEnqFilter> Filter;
  public PXAction<GLHistoryEnqFilter> accountDetails;
  [PXFilterable(new Type[] {})]
  public PXSelectOrderBy<GLHistoryEnquiryResult, OrderBy<Asc<GLHistoryEnquiryResult.subCD>>> EnqResult;
  public PXSetup<GLSetup> glsetup;
  public PXSelect<Account, Where<Account.accountID, Equal<Current<GLHistoryEnqFilter.accountID>>>> AccountInfo;

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimension("SUBACCOUNT")]
  protected virtual void GLHistoryEnquiryResult_SubCD_CacheAttached(PXCache sender)
  {
  }

  [AccountAny]
  [PXDefault]
  protected virtual void GLHistoryEnquiryResult_AccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  protected virtual void GLHistoryEnquiryResult_Description_CacheAttached(PXCache sender)
  {
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField(DisplayName = "Financial Period")]
  protected virtual void GLHistoryEnquiryResult_LastActivityPeriod_CacheAttached(PXCache sender)
  {
  }

  private GLHistoryEnqFilter CurrentFilter
  {
    get => ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current;
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public AccountHistoryBySubEnq()
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
    GLHistoryEnqFilter current = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current;
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.FinPeriodID, true);
    current.FinPeriodID = prevPeriod?.FinPeriodID;
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable nextperiod(PXAdapter adapter)
  {
    GLHistoryEnqFilter current = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current;
    FinPeriod nextPeriod = this.FinPeriodRepository.FindNextPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.FinPeriodID, true);
    current.FinPeriodID = nextPeriod?.FinPeriodID;
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
    copy.OrgBAccountID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.OrgBAccountID;
    copy.OrganizationID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.OrganizationID;
    copy.BranchID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.BranchID;
    copy.LedgerID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.LedgerID;
    copy.AccountID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.AccountID;
    copy.SubID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.SubCD;
    copy.StartPeriodID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.FinPeriodID;
    copy.EndPeriodID = copy.StartPeriodID;
    copy.ShowCuryDetail = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.ShowCuryDetail;
    copy.UseMasterCalendar = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.UseMasterCalendar;
    ((PXSelectBase<AccountByPeriodFilter>) instance.Filter).Update(copy);
    ((PXSelectBase<AccountByPeriodFilter>) instance.Filter).Select(Array.Empty<object>());
    throw new PXRedirectRequiredException((PXGraph) instance, "Account Details");
  }

  protected virtual IEnumerable enqResult()
  {
    AccountHistoryBySubEnq accountHistoryBySubEnq = this;
    GLHistoryEnqFilter filter = accountHistoryBySubEnq.CurrentFilter;
    bool valueOrDefault = filter.ShowCuryDetail.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyID>(((PXSelectBase) accountHistoryBySubEnq.EnqResult).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyPtdCreditTotal>(((PXSelectBase) accountHistoryBySubEnq.EnqResult).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyPtdDebitTotal>(((PXSelectBase) accountHistoryBySubEnq.EnqResult).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyBegBalance>(((PXSelectBase) accountHistoryBySubEnq.EnqResult).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyEndBalance>(((PXSelectBase) accountHistoryBySubEnq.EnqResult).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.signCuryBegBalance>(((PXSelectBase) accountHistoryBySubEnq.EnqResult).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.signCuryEndBalance>(((PXSelectBase) accountHistoryBySubEnq.EnqResult).Cache, (object) null, valueOrDefault);
    int? nullable1 = filter.AccountID;
    if (nullable1.HasValue)
    {
      nullable1 = filter.LedgerID;
      if (nullable1.HasValue && filter.FinPeriodID != null)
      {
        PXSelectBase<GLHistoryByPeriod> pxSelectBase1 = (PXSelectBase<GLHistoryByPeriod>) new PXSelectJoinGroupBy<GLHistoryByPeriod, InnerJoin<Account, On<GLHistoryByPeriod.accountID, Equal<Account.accountID>, And<Match<Account, Current<AccessInfo.userName>>>>, InnerJoin<Sub, On<GLHistoryByPeriod.subID, Equal<Sub.subID>, And<Match<Sub, Current<AccessInfo.userName>>>>, LeftJoin<GLHistory, On<GLHistoryByPeriod.accountID, Equal<GLHistory.accountID>, And<GLHistoryByPeriod.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryByPeriod.branchID, Equal<GLHistory.branchID>, And<GLHistoryByPeriod.subID, Equal<GLHistory.subID>, And<GLHistoryByPeriod.finPeriodID, Equal<GLHistory.finPeriodID>>>>>>, LeftJoin<AH, On<GLHistoryByPeriod.ledgerID, Equal<AH.ledgerID>, And<GLHistoryByPeriod.branchID, Equal<AH.branchID>, And<GLHistoryByPeriod.accountID, Equal<AH.accountID>, And<GLHistoryByPeriod.subID, Equal<AH.subID>, And<GLHistoryByPeriod.lastActivityPeriod, Equal<AH.finPeriodID>>>>>>>>>>, Where<GLHistoryByPeriod.ledgerID, Equal<Current<GLHistoryEnqFilter.ledgerID>>, And<GLHistoryByPeriod.accountID, Equal<Current<GLHistoryEnqFilter.accountID>>, And<GLHistoryByPeriod.finPeriodID, Equal<Current<GLHistoryEnqFilter.finPeriodID>>, And<Where2<Where<Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>, And<Where<Account.type, Equal<AccountType.asset>, Or<Account.type, Equal<AccountType.liability>>>>>, Or<Where<GLHistoryByPeriod.lastActivityPeriod, GreaterEqual<Required<GLHistoryByPeriod.lastActivityPeriod>>, And<Where<Account.type, Equal<AccountType.expense>, Or<Account.type, Equal<AccountType.income>, Or<Account.accountID, Equal<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>>>>>, Aggregate<Sum<AH.finYtdBalance, Sum<AH.tranYtdBalance, Sum<AH.curyFinYtdBalance, Sum<AH.curyTranYtdBalance, Sum<GLHistory.finPtdDebit, Sum<GLHistory.tranPtdDebit, Sum<GLHistory.finPtdCredit, Sum<GLHistory.tranPtdCredit, Sum<GLHistory.finBegBalance, Sum<GLHistory.tranBegBalance, Sum<GLHistory.finYtdBalance, Sum<GLHistory.tranYtdBalance, Sum<GLHistory.curyFinBegBalance, Sum<GLHistory.curyTranBegBalance, Sum<GLHistory.curyFinYtdBalance, Sum<GLHistory.curyTranYtdBalance, Sum<GLHistory.curyFinPtdCredit, Sum<GLHistory.curyTranPtdCredit, Sum<GLHistory.curyFinPtdDebit, Sum<GLHistory.curyTranPtdDebit, GroupBy<GLHistoryByPeriod.ledgerID, GroupBy<GLHistoryByPeriod.accountID, GroupBy<GLHistoryByPeriod.subID>>>>>>>>>>>>>>>>>>>>>>>>>((PXGraph) accountHistoryBySubEnq);
        int[] numArray = (int[]) null;
        nullable1 = filter.OrgBAccountID;
        if (nullable1.HasValue)
          pxSelectBase1.WhereAnd<Where<GLHistoryByPeriod.branchID, InsideBranchesOf<Current<GLHistoryEnqFilter.orgBAccountID>>, And<MatchWithBranch<GLHistoryByPeriod.branchID>>>>();
        if (!SubCDUtils.IsSubCDEmpty(filter.SubCD))
          pxSelectBase1.WhereAnd<Where<Sub.subCD, Like<Current<GLHistoryEnqFilter.subCDWildcard>>>>();
        PXSelectBase<GLHistoryByPeriod> pxSelectBase2 = pxSelectBase1;
        object[] objArray = new object[2]
        {
          (object) filter.BegFinPeriod,
          (object) numArray
        };
        foreach (PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH> pxResult in pxSelectBase2.Select(objArray))
        {
          GLHistoryByPeriod glHistoryByPeriod = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
          Account account = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
          GLHistory glHistory = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
          AH ah = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
          glHistory.FinFlag = new bool?(!filter.UseMasterCalendar.GetValueOrDefault());
          ah.FinFlag = new bool?(!filter.UseMasterCalendar.GetValueOrDefault());
          GLHistoryEnquiryResult historyEnquiryResult1 = new GLHistoryEnquiryResult();
          historyEnquiryResult1.AccountID = glHistoryByPeriod.AccountID;
          historyEnquiryResult1.AccountCD = account.AccountCD;
          historyEnquiryResult1.LedgerID = glHistoryByPeriod.LedgerID;
          historyEnquiryResult1.Type = account.Type;
          historyEnquiryResult1.Description = account.Description;
          historyEnquiryResult1.LastActivityPeriod = glHistoryByPeriod.LastActivityPeriod;
          historyEnquiryResult1.PtdCreditTotal = new Decimal?(glHistory.PtdCredit.GetValueOrDefault());
          Decimal? nullable2 = glHistory.PtdDebit;
          historyEnquiryResult1.PtdDebitTotal = new Decimal?(nullable2.GetValueOrDefault());
          historyEnquiryResult1.CuryID = ah.CuryID;
          historyEnquiryResult1.SubCD = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult).SubCD;
          nullable2 = ah.YtdBalance;
          historyEnquiryResult1.EndBalance = new Decimal?(nullable2.GetValueOrDefault());
          GLHistoryEnquiryResult historyEnquiryResult2 = historyEnquiryResult1;
          if (!string.IsNullOrEmpty(ah.CuryID))
          {
            GLHistoryEnquiryResult historyEnquiryResult3 = historyEnquiryResult2;
            nullable2 = ah.CuryYtdBalance;
            Decimal? nullable3 = new Decimal?(nullable2.GetValueOrDefault());
            historyEnquiryResult3.CuryEndBalance = nullable3;
            GLHistoryEnquiryResult historyEnquiryResult4 = historyEnquiryResult2;
            nullable2 = glHistory.CuryTranPtdCredit;
            Decimal? nullable4 = new Decimal?(nullable2.GetValueOrDefault());
            historyEnquiryResult4.CuryPtdCreditTotal = nullable4;
            GLHistoryEnquiryResult historyEnquiryResult5 = historyEnquiryResult2;
            nullable2 = glHistory.CuryTranPtdDebit;
            Decimal? nullable5 = new Decimal?(nullable2.GetValueOrDefault());
            historyEnquiryResult5.CuryPtdDebitTotal = nullable5;
          }
          else
          {
            GLHistoryEnquiryResult historyEnquiryResult6 = historyEnquiryResult2;
            nullable2 = new Decimal?();
            Decimal? nullable6 = nullable2;
            historyEnquiryResult6.CuryEndBalance = nullable6;
            GLHistoryEnquiryResult historyEnquiryResult7 = historyEnquiryResult2;
            nullable2 = new Decimal?();
            Decimal? nullable7 = nullable2;
            historyEnquiryResult7.CuryPtdCreditTotal = nullable7;
            GLHistoryEnquiryResult historyEnquiryResult8 = historyEnquiryResult2;
            nullable2 = new Decimal?();
            Decimal? nullable8 = nullable2;
            historyEnquiryResult8.CuryPtdDebitTotal = nullable8;
          }
          historyEnquiryResult2.recalculate(true);
          historyEnquiryResult2.recalculateSignAmount(((PXSelectBase<GLSetup>) accountHistoryBySubEnq.glsetup).Current?.TrialBalanceSign == "R");
          yield return (object) historyEnquiryResult2;
        }
      }
    }
  }

  public virtual bool IsDirty => false;

  protected virtual void GLHistoryEnqFilter_SubCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLHistoryEnqFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Select(Array.Empty<object>());
  }

  protected virtual void GLHistoryEnqFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
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
    PXUIFieldAttribute.SetEnabled<GLHistoryEnqFilter.showCuryDetail>(cache, e.Row, flag);
    if (flag)
      return;
    row.ShowCuryDetail = new bool?(false);
  }

  protected virtual void GLHistoryEnqFilter_BranchID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<GLHistoryEnqFilter.ledgerID>(e.Row);
  }

  protected virtual void GLHistoryEnqFilter_OrganizationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<GLHistoryEnqFilter.ledgerID>(e.Row);
  }
}
