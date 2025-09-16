// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLBudgetRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
public class GLBudgetRelease : PXGraph<GLBudgetRelease>
{
  public PXCancel<GLBudgetLine> Cancel;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<GLBudgetLine> EditDetail;
  [PXFilterable(new Type[] {})]
  public PXProcessingJoin<GLBudgetLine, InnerJoin<Account, On<GLBudgetLine.accountID, Equal<Account.accountID>>, InnerJoin<Sub, On<GLBudgetLine.subID, Equal<Sub.subID>>>>, Where<GLBudgetLine.released, NotEqual<boolTrue>, And<GLBudgetLine.amount, Equal<GLBudgetLine.allocatedAmount>, And<GLBudgetLine.rollup, Equal<False>, And<Account.active, Equal<True>, And<Sub.active, Equal<True>, And<Match<Current<AccessInfo.userName>>>>>>>>, OrderBy<Asc<GLBudgetLine.finYear>>> BudgetArticles;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  protected virtual void GLBudgetLine_Selected_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (BudgetFilter.ledgerID))]
  [PXUIField]
  [PXSelector(typeof (Ledger.ledgerID), SubstituteKey = typeof (Ledger.ledgerCD), CacheGlobal = true)]
  protected virtual void GLBudgetLine_LedgerID_CacheAttached(PXCache sender)
  {
  }

  [PXEditDetailButton]
  [PXUIField]
  protected virtual IEnumerable editDetail(PXAdapter adapter)
  {
    if (((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Current != null)
    {
      GLBudgetEntry instance = PXGraph.CreateInstance<GLBudgetEntry>();
      ((PXSelectBase<BudgetFilter>) instance.Filter).Current.BranchID = ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Current.BranchID;
      ((PXSelectBase<BudgetFilter>) instance.Filter).Current.LedgerID = ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Current.LedgerID;
      ((PXSelectBase<BudgetFilter>) instance.Filter).Current.FinYear = ((PXSelectBase<GLBudgetLine>) this.BudgetArticles).Current.FinYear;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Budget");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  public GLBudgetRelease()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    ((PXProcessing<GLBudgetLine>) this.BudgetArticles).SetProcessCaption("Release");
    ((PXProcessing<GLBudgetLine>) this.BudgetArticles).SetProcessAllCaption("Release All");
    // ISSUE: method pointer
    ((PXProcessingBase<GLBudgetLine>) this.BudgetArticles).SetProcessDelegate(new PXProcessingBase<GLBudgetLine>.ProcessListDelegate((object) null, __methodptr(Approve)));
    PXUIFieldAttribute.SetVisible<GLBudgetLine.branchID>(((PXSelectBase) this.BudgetArticles).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<GLBudgetLine.ledgerID>(((PXSelectBase) this.BudgetArticles).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<GLBudgetLine.finYear>(((PXSelectBase) this.BudgetArticles).Cache, (object) null, true);
  }

  public static void Approve(List<GLBudgetLine> list)
  {
    bool flag1 = false;
    GLBudgetEntry instance = PXGraph.CreateInstance<GLBudgetEntry>();
    ((PXGraph) instance).Views.Caches.Add(typeof (GLBudgetRelease.AccountHistory));
    PXCache<GLBudgetRelease.AccountHistory> pxCache1 = GraphHelper.Caches<GLBudgetRelease.AccountHistory>((PXGraph) instance);
    PXCache<GLBudgetLine> pxCache2 = GraphHelper.Caches<GLBudgetLine>((PXGraph) instance);
    PXCache<GLBudgetLineDetail> pxCache3 = GraphHelper.Caches<GLBudgetLineDetail>((PXGraph) instance);
    FinPeriodRepository periodRepository = new FinPeriodRepository((PXGraph) instance);
    for (int index = 0; index < list.Count; ++index)
    {
      try
      {
        bool flag2 = false;
        GLBudgetLine glBudgetLine = list[index];
        Ledger ledger = PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelect<Ledger, Where<Ledger.ledgerID, Equal<Required<Ledger.ledgerID>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) glBudgetLine.LedgerID
        }));
        Decimal? nullable1 = glBudgetLine.AllocatedAmount;
        Decimal? nullable2 = glBudgetLine.Amount;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          PXProcessing<GLBudgetLine>.SetError(index, "The Budget Article is not allocated properly.");
          flag1 = true;
        }
        else
        {
          Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select((PXGraph) instance, new object[1]
          {
            (object) glBudgetLine.AccountID
          }));
          PXResultset<GLBudgetLineDetail> source = PXSelectBase<GLBudgetLineDetail, PXSelect<GLBudgetLineDetail, Where<GLBudgetLineDetail.ledgerID, Equal<Required<GLBudgetLineDetail.ledgerID>>, And<GLBudgetLineDetail.branchID, Equal<Required<GLBudgetLineDetail.branchID>>, And<GLBudgetLineDetail.finYear, Equal<Required<GLBudgetLineDetail.finYear>>, And<GLBudgetLineDetail.groupID, Equal<Required<GLBudgetLineDetail.groupID>>>>>>>.Config>.Select((PXGraph) instance, new object[4]
          {
            (object) glBudgetLine.LedgerID,
            (object) glBudgetLine.BranchID,
            (object) glBudgetLine.FinYear,
            (object) glBudgetLine.GroupID
          });
          ProcessingResult processingResult = GLBudgetRelease.ValidateFinPeriods(instance, (IEnumerable<GLBudgetLineDetail>) ((IQueryable<PXResult<GLBudgetLineDetail>>) source).Select<PXResult<GLBudgetLineDetail>, GLBudgetLineDetail>((Expression<Func<PXResult<GLBudgetLineDetail>, GLBudgetLineDetail>>) (record => (GLBudgetLineDetail) record)));
          if (!processingResult.IsSuccess)
          {
            PXProcessing<GLBudgetLine>.SetError(index, processingResult.GetGeneralMessage());
            flag1 = true;
          }
          else
          {
            List<GLBudgetRelease.AccountHistory> accountHistoryList = new List<GLBudgetRelease.AccountHistory>();
            foreach (PXResult<GLBudgetLineDetail> pxResult in source)
            {
              GLBudgetLineDetail budgetLineDetail = PXResult<GLBudgetLineDetail>.op_Implicit(pxResult);
              nullable2 = budgetLineDetail.Amount;
              Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
              nullable2 = budgetLineDetail.ReleasedAmount;
              Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
              Decimal num1 = valueOrDefault1 - valueOrDefault2;
              if (num1 != 0M)
              {
                GLBudgetRelease.AccountHistory accountHistory1 = new GLBudgetRelease.AccountHistory();
                accountHistory1.BranchID = budgetLineDetail.BranchID;
                accountHistory1.AccountID = budgetLineDetail.AccountID;
                accountHistory1.FinPeriodID = budgetLineDetail.FinPeriodID;
                accountHistory1.LedgerID = budgetLineDetail.LedgerID;
                accountHistory1.SubID = budgetLineDetail.SubID;
                accountHistory1.CuryID = (string) null;
                accountHistory1.BalanceType = ledger.BalanceType;
                GLBudgetRelease.AccountHistory accountHistory2 = accountHistory1;
                GLBudgetRelease.AccountHistory accountHistory3 = new GLBudgetRelease.AccountHistory();
                accountHistory3.BranchID = budgetLineDetail.BranchID;
                accountHistory3.AccountID = budgetLineDetail.AccountID;
                accountHistory3.FinPeriodID = periodRepository.FindByID(PXAccess.GetParentOrganizationID(budgetLineDetail.BranchID), budgetLineDetail.FinPeriodID).MasterFinPeriodID;
                accountHistory3.LedgerID = budgetLineDetail.LedgerID;
                accountHistory3.SubID = budgetLineDetail.SubID;
                accountHistory3.CuryID = (string) null;
                accountHistory3.BalanceType = ledger.BalanceType;
                GLBudgetRelease.AccountHistory accountHistory4 = accountHistory3;
                GLBudgetRelease.AccountHistory accountHistory5 = pxCache1.Insert(accountHistory2);
                GLBudgetRelease.AccountHistory accountHistory6 = pxCache1.Insert(accountHistory4);
                if (accountHistory5 == null)
                {
                  PXProcessing<GLBudgetLine>.SetError(index, "Unexpected error occurred.");
                  flag2 = true;
                  break;
                }
                GLBudgetRelease.AccountHistory accountHistory7 = accountHistory5;
                nullable2 = accountHistory7.YtdBalance;
                Decimal num2 = num1;
                Decimal? nullable3;
                if (!nullable2.HasValue)
                {
                  nullable1 = new Decimal?();
                  nullable3 = nullable1;
                }
                else
                  nullable3 = new Decimal?(nullable2.GetValueOrDefault() + num2);
                accountHistory7.YtdBalance = nullable3;
                accountHistory5.CuryFinYtdBalance = accountHistory5.YtdBalance;
                GLBudgetRelease.AccountHistory accountHistory8 = accountHistory6;
                nullable2 = accountHistory8.TranYtdBalance;
                Decimal num3 = num1;
                Decimal? nullable4;
                if (!nullable2.HasValue)
                {
                  nullable1 = new Decimal?();
                  nullable4 = nullable1;
                }
                else
                  nullable4 = new Decimal?(nullable2.GetValueOrDefault() + num3);
                accountHistory8.TranYtdBalance = nullable4;
                accountHistory6.CuryTranYtdBalance = accountHistory5.TranYtdBalance;
                if (account.Type == "A" || account.Type == "E")
                {
                  GLBudgetRelease.AccountHistory accountHistory9 = accountHistory5;
                  nullable2 = accountHistory9.FinPtdDebit;
                  Decimal num4 = num1;
                  Decimal? nullable5;
                  if (!nullable2.HasValue)
                  {
                    nullable1 = new Decimal?();
                    nullable5 = nullable1;
                  }
                  else
                    nullable5 = new Decimal?(nullable2.GetValueOrDefault() + num4);
                  accountHistory9.FinPtdDebit = nullable5;
                  accountHistory5.CuryFinPtdDebit = accountHistory5.FinPtdDebit;
                  GLBudgetRelease.AccountHistory accountHistory10 = accountHistory6;
                  nullable2 = accountHistory10.TranPtdDebit;
                  Decimal num5 = num1;
                  Decimal? nullable6;
                  if (!nullable2.HasValue)
                  {
                    nullable1 = new Decimal?();
                    nullable6 = nullable1;
                  }
                  else
                    nullable6 = new Decimal?(nullable2.GetValueOrDefault() + num5);
                  accountHistory10.TranPtdDebit = nullable6;
                  accountHistory6.CuryTranPtdDebit = accountHistory6.TranPtdDebit;
                  accountHistory5.CuryFinPtdCredit = accountHistory5.FinPtdCredit;
                  accountHistory6.CuryTranPtdCredit = accountHistory6.TranPtdCredit;
                }
                else
                {
                  GLBudgetRelease.AccountHistory accountHistory11 = accountHistory5;
                  nullable2 = accountHistory11.FinPtdCredit;
                  Decimal num6 = num1;
                  Decimal? nullable7;
                  if (!nullable2.HasValue)
                  {
                    nullable1 = new Decimal?();
                    nullable7 = nullable1;
                  }
                  else
                    nullable7 = new Decimal?(nullable2.GetValueOrDefault() + num6);
                  accountHistory11.FinPtdCredit = nullable7;
                  accountHistory5.CuryFinPtdCredit = accountHistory5.FinPtdCredit;
                  GLBudgetRelease.AccountHistory accountHistory12 = accountHistory6;
                  nullable2 = accountHistory12.TranPtdCredit;
                  Decimal num7 = num1;
                  Decimal? nullable8;
                  if (!nullable2.HasValue)
                  {
                    nullable1 = new Decimal?();
                    nullable8 = nullable1;
                  }
                  else
                    nullable8 = new Decimal?(nullable2.GetValueOrDefault() + num7);
                  accountHistory12.TranPtdCredit = nullable8;
                  accountHistory6.CuryTranPtdCredit = accountHistory6.TranPtdCredit;
                  accountHistory5.CuryFinPtdDebit = accountHistory5.FinPtdDebit;
                  accountHistory6.CuryTranPtdDebit = accountHistory6.TranPtdDebit;
                }
                accountHistoryList.Add(accountHistory5);
                accountHistoryList.Add(accountHistory6);
              }
            }
            flag1 |= flag2;
            if (flag2)
            {
              foreach (GLBudgetRelease.AccountHistory accountHistory in accountHistoryList)
                pxCache1.Remove(accountHistory);
            }
            else
            {
              foreach (PXResult<GLBudgetLineDetail> pxResult in source)
              {
                GLBudgetLineDetail budgetLineDetail = PXResult<GLBudgetLineDetail>.op_Implicit(pxResult);
                budgetLineDetail.ReleasedAmount = budgetLineDetail.Amount;
                pxCache3.Update(budgetLineDetail);
              }
              glBudgetLine.ReleasedAmount = glBudgetLine.Amount;
              glBudgetLine.Released = new bool?(true);
              glBudgetLine.WasReleased = new bool?(true);
              pxCache2.Update(glBudgetLine);
              PXProcessing<GLBudgetLine>.SetInfo(index, "The record has been processed successfully.");
            }
          }
        }
      }
      catch (Exception ex)
      {
        PXProcessing<GLBudgetLine>.SetError(index, ex.Message);
        throw;
      }
    }
    ((PXAction) instance.Save).Press();
    if (flag1)
      throw new PXException("Several items failed to be approved.");
  }

  private static ProcessingResult ValidateFinPeriods(
    GLBudgetEntry graph,
    IEnumerable<GLBudgetLineDetail> records)
  {
    ProcessingResult processingResult1 = new ProcessingResult();
    foreach (IGrouping<string, GLBudgetLineDetail> source in records.GroupBy<GLBudgetLineDetail, string>((Func<GLBudgetLineDetail, string>) (record => record.FinPeriodID)))
    {
      string key = source.Key;
      int?[] array1 = source.GroupBy<GLBudgetLineDetail, int?>((Func<GLBudgetLineDetail, int?>) (t => PXAccess.GetParentOrganizationID(t.BranchID))).Select<IGrouping<int?, GLBudgetLineDetail>, int?>((Func<IGrouping<int?, GLBudgetLineDetail>, int?>) (g => g.Key)).ToArray<int?>();
      ICollection<OrganizationFinPeriod> array2 = (ICollection<OrganizationFinPeriod>) GraphHelper.RowCast<OrganizationFinPeriod>((IEnumerable) PXSelectBase<OrganizationFinPeriod, PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.organizationID, In<Required<OrganizationFinPeriod.organizationID>>, And<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>>>>.Config>.Select((PXGraph) graph, new object[2]
      {
        (object) array1,
        (object) key
      })).ToArray<OrganizationFinPeriod>();
      if (array2.Count != array1.Length)
      {
        string[] array3 = ((IEnumerable<int?>) array1).Except<int?>(array2.Select<OrganizationFinPeriod, int?>((Func<OrganizationFinPeriod, int?>) (period => period.OrganizationID))).Select<int?, string>(new Func<int?, string>(PXAccess.GetOrganizationCD)).ToArray<string>();
        processingResult1.AddErrorMessage("The {0} financial period does not exist for the following companies: {1}.", (object) FinPeriodIDFormattingAttribute.FormatForError(key), (object) ((ICollection<string>) array3).JoinIntoStringForMessageNoQuotes<string>(20));
      }
      foreach (OrganizationFinPeriod organizationFinPeriod in (IEnumerable<OrganizationFinPeriod>) array2)
      {
        ProcessingResult processingResult2 = new ProcessingResult();
        if (organizationFinPeriod.Status == "Locked")
          processingResult2.AddErrorMessage("The {0} financial period is locked in the {1} company.", (object) FinPeriodIDFormattingAttribute.FormatForError(organizationFinPeriod.FinPeriodID), (object) PXAccess.GetOrganizationCD(organizationFinPeriod.OrganizationID));
        processingResult1.Aggregate((ProcessingResultBase<ProcessingResult, object, ProcessingResultMessage>) processingResult2);
        if (processingResult1.Messages.Count > 20)
          return processingResult1;
      }
    }
    return processingResult1;
  }

  public class AHAccumulatorAttribute : PXAccumulatorAttribute
  {
    public AHAccumulatorAttribute()
      : base(new Type[8]
      {
        typeof (GLHistory.finYtdBalance),
        typeof (GLHistory.tranYtdBalance),
        typeof (GLHistory.curyFinYtdBalance),
        typeof (GLHistory.curyTranYtdBalance),
        typeof (GLHistory.finYtdBalance),
        typeof (GLHistory.tranYtdBalance),
        typeof (GLHistory.curyFinYtdBalance),
        typeof (GLHistory.curyTranYtdBalance)
      }, new Type[8]
      {
        typeof (GLHistory.finBegBalance),
        typeof (GLHistory.tranBegBalance),
        typeof (GLHistory.curyFinBegBalance),
        typeof (GLHistory.curyTranBegBalance),
        typeof (GLHistory.finYtdBalance),
        typeof (GLHistory.tranYtdBalance),
        typeof (GLHistory.curyFinYtdBalance),
        typeof (GLHistory.curyTranYtdBalance)
      })
    {
    }

    protected virtual bool PrepareInsert(
      PXCache sender,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(sender, row, columns))
        return false;
      GLBudgetRelease.AccountHistory accountHistory = (GLBudgetRelease.AccountHistory) row;
      columns.RestrictPast<GLHistory.finPeriodID>((PXComp) 3, (object) (accountHistory.FinPeriodID.Substring(0, 4) + "01"));
      columns.RestrictFuture<GLHistory.finPeriodID>((PXComp) 5, (object) (accountHistory.FinPeriodID.Substring(0, 4) + "99"));
      return true;
    }
  }

  [GLBudgetRelease.AHAccumulator]
  [PXHidden]
  [Serializable]
  public class AccountHistory : GLHistory
  {
    [PXDBInt(IsKey = true)]
    public override int? LedgerID
    {
      get => this._LedgerID;
      set => this._LedgerID = value;
    }

    [PXDBInt(IsKey = true)]
    public override int? AccountID
    {
      get => this._AccountID;
      set => this._AccountID = value;
    }

    [PXDBInt(IsKey = true)]
    public override int? SubID
    {
      get => this._SubID;
      set => this._SubID = value;
    }

    [PXDBString(6, IsFixed = true, IsKey = true)]
    public override string FinPeriodID { get; set; }

    [PXDBString(1, IsFixed = true)]
    [PXDefault]
    public override string BalanceType
    {
      get => this._BalanceType;
      set => this._BalanceType = value;
    }

    [PXDBString(5, IsUnicode = true)]
    public override string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }
  }
}
