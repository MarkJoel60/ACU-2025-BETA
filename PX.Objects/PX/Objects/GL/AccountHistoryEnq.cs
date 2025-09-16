// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountHistoryEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
public class AccountHistoryEnq : PXGraph<AccountHistoryEnq>
{
  public PXCancel<GLHistoryEnqFilter> Cancel;
  public PXAction<GLHistoryEnqFilter> PreviousPeriod;
  public PXAction<GLHistoryEnqFilter> NextPeriod;
  public PXFilter<GLHistoryEnqFilter> Filter;
  public PXAction<GLHistoryEnqFilter> accountDetails;
  public PXAction<GLHistoryEnqFilter> accountBySub;
  public PXAction<GLHistoryEnqFilter> accountByPeriod;
  [PXFilterable(new Type[] {})]
  public PXSelectOrderBy<GLHistoryEnquiryResult, OrderBy<Asc<GLHistoryEnquiryResult.accountCD>>> EnqResult;
  public PXSetup<GLSetup> glsetup;
  public PXAction<GLHistoryEnqFilter> viewDetails;

  private GLHistoryEnqFilter CurrentFilter
  {
    get => ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current;
  }

  public AccountHistoryEnq()
  {
    GLSetup current = ((PXSelectBase<GLSetup>) this.glsetup).Current;
    ((PXSelectBase) this.EnqResult).Cache.AllowInsert = false;
    ((PXSelectBase) this.EnqResult).Cache.AllowDelete = false;
    ((PXSelectBase) this.EnqResult).Cache.AllowUpdate = false;
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

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
    if (((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current != null)
    {
      int? accountId = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.AccountID;
      int? ytdNetIncAccountId = ((PXSelectBase<GLSetup>) this.glsetup).Current.YtdNetIncAccountID;
      if (accountId.GetValueOrDefault() == ytdNetIncAccountId.GetValueOrDefault() & accountId.HasValue == ytdNetIncAccountId.HasValue)
        throw new PXException("Year to Date Net Income account cannot be selected for inquiry.");
      this.RefirectToAccountByPeriodEnq();
    }
    return adapter.Get();
  }

  private void RefirectToAccountByPeriodEnq()
  {
    AccountByPeriodEnq instance = PXGraph.CreateInstance<AccountByPeriodEnq>();
    AccountByPeriodFilter copy = PXCache<AccountByPeriodFilter>.CreateCopy(((PXSelectBase<AccountByPeriodFilter>) instance.Filter).Current);
    copy.OrgBAccountID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.OrgBAccountID;
    copy.OrganizationID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.OrganizationID;
    copy.BranchID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.BranchID;
    copy.LedgerID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.LedgerID;
    copy.AccountID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.AccountID;
    copy.SubID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.SubCD;
    copy.StartPeriodID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.LastActivityPeriod;
    copy.EndPeriodID = copy.StartPeriodID;
    copy.ShowCuryDetail = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.ShowCuryDetail;
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
      copy.OrgBAccountID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.OrgBAccountID;
      copy.OrganizationID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.OrganizationID;
      copy.BranchID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.BranchID;
      copy.LedgerID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.LedgerID;
      copy.AccountID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.AccountID;
      copy.SubCD = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.SubCD;
      copy.FinPeriodID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.LastActivityPeriod;
      copy.ShowCuryDetail = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.ShowCuryDetail;
      ((PXSelectBase<GLHistoryEnqFilter>) instance.Filter).Update(copy);
      throw new PXRedirectRequiredException((PXGraph) instance, "Account by Subaccount");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Account by Period")]
  [PXButton]
  protected virtual IEnumerable AccountByPeriod(PXAdapter adapter)
  {
    if (((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current != null)
    {
      AccountHistoryByYearEnq instance = PXGraph.CreateInstance<AccountHistoryByYearEnq>();
      AccountByYearFilter copy = PXCache<AccountByYearFilter>.CreateCopy(((PXSelectBase<AccountByYearFilter>) instance.Filter).Current);
      copy.OrgBAccountID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.OrgBAccountID;
      copy.OrganizationID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.OrganizationID;
      copy.BranchID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.BranchID;
      copy.LedgerID = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.LedgerID;
      copy.AccountID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.AccountID;
      copy.SubCD = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.SubCD;
      copy.FinPeriodID = ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current.LastActivityPeriod;
      OrganizationFinPeriod organizationFinPeriod = PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) copy.FinPeriodID,
        (object) PXAccess.GetParentOrganizationID(copy.BranchID)
      }));
      if (organizationFinPeriod != null)
        copy.FinYear = organizationFinPeriod.FinYear;
      copy.ShowCuryDetail = ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current.ShowCuryDetail;
      ((PXSelectBase<AccountByYearFilter>) instance.Filter).Update(copy);
      throw new PXRedirectRequiredException((PXGraph) instance, "Account by Period");
    }
    return adapter.Get();
  }

  protected virtual IEnumerable enqResult()
  {
    AccountHistoryEnq accountHistoryEnq = this;
    GLHistoryEnqFilter filter = ((PXSelectBase<GLHistoryEnqFilter>) accountHistoryEnq.Filter).Current;
    bool? nullable1 = filter.ShowCuryDetail;
    int num;
    if (nullable1.HasValue)
    {
      nullable1 = filter.ShowCuryDetail;
      num = nullable1.Value ? 1 : 0;
    }
    else
      num = 0;
    bool flag = num != 0;
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyID>(((PXSelectBase) accountHistoryEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyPtdCreditTotal>(((PXSelectBase) accountHistoryEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyPtdDebitTotal>(((PXSelectBase) accountHistoryEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyBegBalance>(((PXSelectBase) accountHistoryEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.curyEndBalance>(((PXSelectBase) accountHistoryEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.signCuryBegBalance>(((PXSelectBase) accountHistoryEnq.EnqResult).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<GLHistoryEnquiryResult.signCuryEndBalance>(((PXSelectBase) accountHistoryEnq.EnqResult).Cache, (object) null, flag);
    if (filter.LedgerID.HasValue && filter.FinPeriodID != null)
    {
      PXSelectBase<GLHistoryByPeriod> pxSelectBase1 = (PXSelectBase<GLHistoryByPeriod>) new PXSelectJoinGroupBy<GLHistoryByPeriod, InnerJoin<Account, On<GLHistoryByPeriod.accountID, Equal<Account.accountID>, And<Match<Account, Current<AccessInfo.userName>>>>, InnerJoin<Sub, On<GLHistoryByPeriod.subID, Equal<Sub.subID>, And<Match<Sub, Current<AccessInfo.userName>>>>, LeftJoin<GLHistory, On<GLHistoryByPeriod.accountID, Equal<GLHistory.accountID>, And<GLHistoryByPeriod.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryByPeriod.branchID, Equal<GLHistory.branchID>, And<GLHistoryByPeriod.subID, Equal<GLHistory.subID>, And<GLHistoryByPeriod.finPeriodID, Equal<GLHistory.finPeriodID>>>>>>, LeftJoin<AH, On<GLHistoryByPeriod.ledgerID, Equal<AH.ledgerID>, And<GLHistoryByPeriod.branchID, Equal<AH.branchID>, And<GLHistoryByPeriod.accountID, Equal<AH.accountID>, And<GLHistoryByPeriod.subID, Equal<AH.subID>, And<GLHistoryByPeriod.lastActivityPeriod, Equal<AH.finPeriodID>>>>>>>>>>, Where<GLHistoryByPeriod.ledgerID, Equal<Current<GLHistoryEnqFilter.ledgerID>>, And<GLHistoryByPeriod.finPeriodID, Equal<Current<GLHistoryEnqFilter.finPeriodID>>, And<Where2<Where<Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>, And<Where<Account.type, Equal<AccountType.asset>, Or<Account.type, Equal<AccountType.liability>>>>>, Or<Where<GLHistoryByPeriod.lastActivityPeriod, GreaterEqual<Required<GLHistoryByPeriod.lastActivityPeriod>>, And<Where<Account.type, Equal<AccountType.expense>, Or<Account.type, Equal<AccountType.income>, Or<Account.accountID, Equal<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>>>>, Aggregate<Sum<AH.finYtdBalance, Sum<AH.tranYtdBalance, Sum<AH.curyFinYtdBalance, Sum<AH.curyTranYtdBalance, Sum<GLHistory.finPtdDebit, Sum<GLHistory.tranPtdDebit, Sum<GLHistory.finPtdCredit, Sum<GLHistory.tranPtdCredit, Sum<GLHistory.finBegBalance, Sum<GLHistory.tranBegBalance, Sum<GLHistory.finYtdBalance, Sum<GLHistory.tranYtdBalance, Sum<GLHistory.curyFinBegBalance, Sum<GLHistory.curyTranBegBalance, Sum<GLHistory.curyFinYtdBalance, Sum<GLHistory.curyTranYtdBalance, Sum<GLHistory.curyFinPtdCredit, Sum<GLHistory.curyTranPtdCredit, Sum<GLHistory.curyFinPtdDebit, Sum<GLHistory.curyTranPtdDebit, GroupBy<GLHistoryByPeriod.branchID, GroupBy<GLHistoryByPeriod.ledgerID, GroupBy<GLHistoryByPeriod.accountID, GroupBy<GLHistoryByPeriod.finPeriodID>>>>>>>>>>>>>>>>>>>>>>>>>>((PXGraph) accountHistoryEnq);
      int? nullable2 = filter.LedgerID;
      if (nullable2.HasValue && ((Ledger) PXSelectorAttribute.Select<GLHistoryEnqFilter.ledgerID>(((PXSelectBase) accountHistoryEnq.Filter).Cache, (object) filter))?.BalanceType == "B")
        pxSelectBase1.WhereAnd<Where<Substring<GLHistoryByPeriod.finPeriodID, int1, int4>, Equal<Substring<GLHistoryByPeriod.lastActivityPeriod, int1, int4>>>>();
      nullable2 = filter.AccountID;
      if (nullable2.HasValue)
        pxSelectBase1.WhereAnd<Where<GLHistoryByPeriod.accountID, Equal<Current<GLHistoryEnqFilter.accountID>>>>();
      if (filter.AccountClassID != null)
        pxSelectBase1.WhereAnd<Where<Account.accountClassID, Equal<Current<GLHistoryEnqFilter.accountClassID>>>>();
      nullable2 = filter.SubID;
      if (nullable2.HasValue)
        pxSelectBase1.WhereAnd<Where<GLHistoryByPeriod.subID, Equal<Current<GLHistoryEnqFilter.subID>>>>();
      int[] numArray = (int[]) null;
      nullable2 = filter.OrgBAccountID;
      if (nullable2.HasValue)
        pxSelectBase1.WhereAnd<Where<GLHistoryByPeriod.branchID, InsideBranchesOf<Current<GLHistoryEnqFilter.orgBAccountID>>, And<MatchWithBranch<GLHistoryByPeriod.branchID>>>>();
      if (!SubCDUtils.IsSubCDEmpty(filter.SubCD))
        pxSelectBase1.WhereAnd<Where<Sub.subCD, Like<Current<GLHistoryEnqFilter.subCDWildcard>>>>();
      string begFinPeriod = filter.BegFinPeriod;
      GLSetup glSetup = ((PXSelectBase<GLSetup>) accountHistoryEnq.glsetup).Current;
      bool reverseSign = glSetup != null && glSetup.TrialBalanceSign == "R";
      PXSelectBase<GLHistoryByPeriod> pxSelectBase2 = pxSelectBase1;
      object[] objArray = new object[2]
      {
        (object) begFinPeriod,
        (object) numArray
      };
      foreach (PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH> pxResult in pxSelectBase2.Select(objArray))
      {
        GLHistoryByPeriod glHistoryByPeriod = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
        Account account = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
        GLHistory glHistory1 = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
        AH ah1 = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
        GLHistory glHistory2 = glHistory1;
        nullable1 = filter.UseMasterCalendar;
        bool? nullable3 = new bool?(!nullable1.GetValueOrDefault());
        glHistory2.FinFlag = nullable3;
        AH ah2 = ah1;
        nullable1 = filter.UseMasterCalendar;
        bool? nullable4 = new bool?(!nullable1.GetValueOrDefault());
        ah2.FinFlag = nullable4;
        if (reverseSign)
        {
          nullable2 = account.AccountID;
          int? ytdNetIncAccountId = glSetup.YtdNetIncAccountID;
          if (nullable2.GetValueOrDefault() == ytdNetIncAccountId.GetValueOrDefault() & nullable2.HasValue == ytdNetIncAccountId.HasValue)
            continue;
        }
        GLHistoryEnquiryResult historyEnquiryResult1 = new GLHistoryEnquiryResult();
        historyEnquiryResult1.BranchID = glHistoryByPeriod.BranchID;
        historyEnquiryResult1.LedgerID = glHistoryByPeriod.LedgerID;
        historyEnquiryResult1.AccountID = glHistoryByPeriod.AccountID;
        historyEnquiryResult1.AccountCD = account.AccountCD;
        historyEnquiryResult1.Type = account.Type;
        historyEnquiryResult1.Description = account.Description;
        historyEnquiryResult1.LastActivityPeriod = glHistoryByPeriod.LastActivityPeriod;
        GLHistoryEnquiryResult historyEnquiryResult2 = historyEnquiryResult1;
        Decimal? nullable5 = glHistory1.PtdCredit;
        Decimal? nullable6 = new Decimal?(nullable5.GetValueOrDefault());
        historyEnquiryResult2.PtdCreditTotal = nullable6;
        GLHistoryEnquiryResult historyEnquiryResult3 = historyEnquiryResult1;
        nullable5 = glHistory1.PtdDebit;
        Decimal? nullable7 = new Decimal?(nullable5.GetValueOrDefault());
        historyEnquiryResult3.PtdDebitTotal = nullable7;
        GLHistoryEnquiryResult historyEnquiryResult4 = historyEnquiryResult1;
        nullable5 = ah1.YtdBalance;
        Decimal? nullable8 = new Decimal?(nullable5.GetValueOrDefault());
        historyEnquiryResult4.EndBalance = nullable8;
        historyEnquiryResult1.ConsolAccountCD = account.GLConsolAccountCD;
        historyEnquiryResult1.AccountClassID = account.AccountClassID;
        if (!string.IsNullOrEmpty(glHistory1.CuryID) || !string.IsNullOrEmpty(ah1.CuryID))
        {
          GLHistoryEnquiryResult historyEnquiryResult5 = historyEnquiryResult1;
          nullable5 = ah1.CuryYtdBalance;
          Decimal? nullable9 = new Decimal?(nullable5.GetValueOrDefault());
          historyEnquiryResult5.CuryEndBalance = nullable9;
          GLHistoryEnquiryResult historyEnquiryResult6 = historyEnquiryResult1;
          nullable5 = glHistory1.CuryPtdCredit;
          Decimal? nullable10 = new Decimal?(nullable5.GetValueOrDefault());
          historyEnquiryResult6.CuryPtdCreditTotal = nullable10;
          GLHistoryEnquiryResult historyEnquiryResult7 = historyEnquiryResult1;
          nullable5 = glHistory1.CuryPtdDebit;
          Decimal? nullable11 = new Decimal?(nullable5.GetValueOrDefault());
          historyEnquiryResult7.CuryPtdDebitTotal = nullable11;
          historyEnquiryResult1.CuryID = string.IsNullOrEmpty(glHistory1.CuryID) ? ah1.CuryID : glHistory1.CuryID;
        }
        else
        {
          GLHistoryEnquiryResult historyEnquiryResult8 = historyEnquiryResult1;
          nullable5 = new Decimal?();
          Decimal? nullable12 = nullable5;
          historyEnquiryResult8.CuryEndBalance = nullable12;
          GLHistoryEnquiryResult historyEnquiryResult9 = historyEnquiryResult1;
          nullable5 = new Decimal?();
          Decimal? nullable13 = nullable5;
          historyEnquiryResult9.CuryPtdCreditTotal = nullable13;
          GLHistoryEnquiryResult historyEnquiryResult10 = historyEnquiryResult1;
          nullable5 = new Decimal?();
          Decimal? nullable14 = nullable5;
          historyEnquiryResult10.CuryPtdDebitTotal = nullable14;
          historyEnquiryResult1.CuryID = (string) null;
        }
        historyEnquiryResult1.recalculate(true);
        historyEnquiryResult1.recalculateSignAmount(reverseSign);
        yield return (object) historyEnquiryResult1;
      }
    }
  }

  public virtual bool IsDirty => false;

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Current != null && ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Current != null)
      this.RefirectToAccountByPeriodEnq();
    return (IEnumerable) ((PXSelectBase<GLHistoryEnqFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  public static void Copy(AccountByPeriodFilter filter, GLHistoryEnqFilter histFilter)
  {
    filter.OrganizationID = histFilter.OrganizationID;
    filter.BranchID = histFilter.BranchID;
    filter.StartPeriodID = histFilter.FinPeriodID;
    filter.EndPeriodID = histFilter.FinPeriodID;
    filter.LedgerID = histFilter.LedgerID;
    filter.AccountID = histFilter.AccountID;
    filter.SubID = histFilter.SubCD;
    filter.ShowCuryDetail = histFilter.ShowCuryDetail;
    filter.UseMasterCalendar = histFilter.UseMasterCalendar;
  }

  protected virtual void GLHistoryEnqFilter_SubCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLHistoryEnqFilter_AccountClassID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
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

  protected virtual void GLHistoryEnqFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase<GLHistoryEnquiryResult>) this.EnqResult).Select(Array.Empty<object>());
  }
}
