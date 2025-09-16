// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.PostGraph
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Data.WorkflowAPI;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.EntityInUse;
using PX.Objects.CS;
using PX.Objects.GL.DAC;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.Overrides.PostGraph;
using PX.Objects.PM;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.GL;

[Serializable]
public class PostGraph : PXGraph<PX.Objects.GL.PostGraph>
{
  public PXSelectJoin<GLTran, LeftJoin<PX.Objects.CM.CurrencyInfo, On<GLTran.curyInfoID, Equal<PX.Objects.CM.CurrencyInfo.curyInfoID>>, LeftJoin<Account, On<GLTran.accountID, Equal<Account.accountID>>, LeftJoin<Ledger, On<GLTran.ledgerID, Equal<Ledger.ledgerID>>>>>, Where<GLTran.module, Equal<Optional<Batch.module>>, And<GLTran.batchNbr, Equal<Optional<Batch.batchNbr>>>>> GLTran_Module_BatNbr;
  [Obsolete("CurrencyInfo table will be removed from the view in the later versions (2018R1).")]
  public PXSelectJoin<GLTran, LeftJoin<CATran, On<CATran.tranID, Equal<GLTran.cATranID>>, LeftJoin<PX.Objects.CM.CurrencyInfo, On<GLTran.curyInfoID, Equal<PX.Objects.CM.CurrencyInfo.curyInfoID>>, LeftJoin<Account, On<GLTran.accountID, Equal<Account.accountID>>, LeftJoin<Ledger, On<GLTran.ledgerID, Equal<Ledger.ledgerID>>>>>>, Where<GLTran.module, Equal<Optional<Batch.module>>, And<GLTran.batchNbr, Equal<Optional<Batch.batchNbr>>>>> GLTran_CATran_Module_BatNbr;
  public PXSelect<Batch, Where<Batch.module, Equal<Optional<Batch.module>>>> BatchModule;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Optional<GLTran.curyInfoID>>>, OrderBy<Asc<PX.Objects.CM.CurrencyInfo.curyInfoID>>> CurrencyInfo_ID;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public PXSelectReadonly<Account, Where<Account.accountID, Equal<Required<GLTran.accountID>>>, OrderBy<Asc<Account.accountCD>>> Account_AccountID;
  public PXSelectReadonly<Ledger, Where<Ledger.ledgerID, Equal<Optional<GLTran.ledgerID>>>, OrderBy<Asc<Ledger.ledgerCD>>> Ledger_LedgerID;
  public PXSelectJoin<GLAllocationAccountHistory, InnerJoin<Account, On<Account.accountID, Equal<GLAllocationAccountHistory.accountID>>>, Where<GLAllocationAccountHistory.batchNbr, Equal<Required<GLAllocationAccountHistory.batchNbr>>, And<GLAllocationAccountHistory.module, Equal<Required<GLAllocationAccountHistory.module>>>>> BatchAllocHistory;
  public PXSelect<TaxTran, Where<TaxTran.module, Equal<PX.Objects.GL.BatchModule.moduleGL>, And<TaxTran.module, Equal<Optional<Batch.module>>, And<TaxTran.refNbr, Equal<Optional<Batch.batchNbr>>>>>> GL_GLTran_Taxes;
  [PXHidden]
  public PXSelectJoin<GLTran, LeftJoin<CATran, On<GLTran.cATranID, Equal<CATran.tranID>>, LeftJoin<Account, On<GLTran.accountID, Equal<Account.accountID>>, LeftJoin<Ledger, On<GLTran.ledgerID, Equal<Ledger.ledgerID>>>>>, Where<GLTran.tranPeriodID, Equal<Required<GLTran.tranPeriodID>>, And<GLTran.ledgerID, Equal<Required<GLTran.ledgerID>>, And<GLTran.posted, Equal<True>>>>> TransactionsForPeriod;
  public PXSelect<GLHistoryFilter> Filter;
  public PXSelect<CATran> catran;
  public PXSetup<GLSetup> glsetup;
  protected Lazy<Account> netIncomeAccount;
  protected Lazy<Account> retainedEarningsAccount;
  protected bool _IsIntegrityCheck;
  protected string _IntegrityCheckStartingPeriod;
  private Lazy<PX.Objects.GL.PostGraph> lazyPostGraph = new Lazy<PX.Objects.GL.PostGraph>((Func<PX.Objects.GL.PostGraph>) (() => PXGraph.CreateInstance<PX.Objects.GL.PostGraph>()));

  [PXDBInt]
  [PXSelector(typeof (Search<Branch.branchID>), SubstituteKey = typeof (Branch.branchCD), CacheGlobal = true)]
  protected virtual void GLTran_BranchID_CacheAttached(PXCache cache)
  {
  }

  [PXDBInt]
  protected virtual void GLTran_LedgerID_CacheAttached(PXCache cache)
  {
  }

  [PXDBInt]
  [PXSelector(typeof (Search<Account.accountID>), SubstituteKey = typeof (Account.accountCD), CacheGlobal = true)]
  protected virtual void GLTran_AccountID_CacheAttached(PXCache cache)
  {
  }

  [SubAccount]
  protected virtual void GLTran_SubID_CacheAttached(PXCache cache)
  {
  }

  [PXDBInt]
  protected virtual void GLTran_OrigAccountID_CacheAttached(PXCache cache)
  {
  }

  [PXDBInt]
  protected virtual void GLTran_OrigSubID_CacheAttached(PXCache cache)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  protected virtual void GLTran_RefNbr_CacheAttached(PXCache cache)
  {
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  protected virtual void GLTran_TranDesc_CacheAttached(PXCache cache)
  {
  }

  [PXDBDecimal(4)]
  protected virtual void GLTran_DebitAmt_CacheAttached(PXCache cache)
  {
  }

  [PXDBDecimal(4)]
  protected virtual void GLTran_CreditAmt_CacheAttached(PXCache cache)
  {
  }

  [PXDBDecimal(4)]
  protected virtual void GLTran_CuryDebitAmt_CacheAttached(PXCache cache)
  {
  }

  [PXDBDecimal(4)]
  protected virtual void GLTran_CuryCreditAmt_CacheAttached(PXCache cache)
  {
  }

  [PXDBLong]
  protected virtual void GLTran_CuryInfoID_CacheAttached(PXCache cache)
  {
  }

  [PXDBLong]
  protected virtual void GLTran_CATranID_CacheAttached(PXCache cache)
  {
  }

  [PXDBInt]
  protected virtual void GLTran_ProjectID_CacheAttached(PXCache cache)
  {
  }

  [PXDBInt]
  protected virtual void GLTran_TaskID_CacheAttached(PXCache cache)
  {
  }

  [PXDBLong]
  protected virtual void GLTran_PMTranID_CacheAttached(PXCache cache)
  {
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDefault]
  protected virtual void TaxTran_FinPeriodID_CacheAttached(PXCache cache)
  {
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  protected virtual void TaxTran_TaxPeriodID_CacheAttached(PXCache cache)
  {
  }

  [PXDBInt]
  [PXDefault]
  protected virtual void TaxTran_VendorID_CacheAttached(PXCache cache)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Ref. Nbr.", Enabled = false, Visible = false)]
  protected virtual void TaxTran_RefNbr_CacheAttached(PXCache cache)
  {
  }

  [PXDBDate]
  [PXDefault]
  protected virtual void TaxTran_TranDate_CacheAttached(PXCache cache)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault(" ")]
  protected virtual void TaxTran_TranType_CacheAttached(PXCache cache)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Zone")]
  protected virtual void TaxTran_TaxZoneID_CacheAttached(PXCache cache)
  {
  }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.TX.Tax.taxID>))]
  protected virtual void TaxTran_TaxID_CacheAttached(PXCache cache)
  {
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public bool AutoPost
  {
    get => ((PXSelectBase<GLSetup>) this.glsetup).Current.AutoPostOption.GetValueOrDefault();
  }

  public bool AutoRevEntry
  {
    get => ((PXSelectBase<GLSetup>) this.glsetup).Current.AutoRevEntry.GetValueOrDefault();
  }

  public bool IsIntegrityCheck => this._IsIntegrityCheck;

  public PostGraph()
  {
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyCreditAmt>(((PXSelectBase) this.GLTran_Module_BatNbr).Cache, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyDebitAmt>(((PXSelectBase) this.GLTran_Module_BatNbr).Cache, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<Batch.curyCreditTotal>(((PXSelectBase) this.BatchModule).Cache, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<Batch.curyDebitTotal>(((PXSelectBase) this.BatchModule).Cache, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<Batch.curyControlTotal>(((PXSelectBase) this.BatchModule).Cache, (object) null, false);
    this.retainedEarningsAccount = new Lazy<Account>((Func<Account>) (() =>
    {
      return Account.PK.Find((PXGraph) this, ((PXSelectBase<GLSetup>) this.glsetup).Current.RetEarnAccountID) ?? throw new PXException("Retained Earnings account is not configured properly in General Ledger Preferences.");
    }));
    this.netIncomeAccount = new Lazy<Account>((Func<Account>) (() =>
    {
      return Account.PK.Find((PXGraph) this, ((PXSelectBase<GLSetup>) this.glsetup).Current.YtdNetIncAccountID) ?? throw new PXException("Year to Date Net Income account is not configured properly in General Ledger Preferences.");
    }));
  }

  public static Dictionary<Batch, Exception> Post(List<Batch> created)
  {
    Dictionary<Batch, Exception> dictionary = new Dictionary<Batch, Exception>();
    PX.Objects.GL.PostGraph instance = PXGraph.CreateInstance<PX.Objects.GL.PostGraph>();
    foreach (Batch batch in created)
    {
      try
      {
        ((PXGraph) instance).Clear();
        instance.PostBatchProc(batch);
      }
      catch (Exception ex)
      {
        dictionary.Add(batch, ex);
      }
    }
    return dictionary;
  }

  public static void NormalizeAmounts(GLTran tran)
  {
    if (tran.SkipNormalizeAmounts.GetValueOrDefault())
      return;
    Decimal? curyDebitAmt = tran.CuryDebitAmt;
    Decimal? nullable1 = tran.CuryCreditAmt;
    Decimal? nullable2 = curyDebitAmt.HasValue & nullable1.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal num1 = 0M;
    Decimal? nullable3;
    if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
    {
      nullable1 = tran.DebitAmt;
      nullable3 = tran.CreditAmt;
      Decimal? nullable4 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = 0M;
      if (nullable4.GetValueOrDefault() < num2 & nullable4.HasValue)
        return;
    }
    nullable3 = tran.CuryDebitAmt;
    nullable1 = tran.CuryCreditAmt;
    Decimal? nullable5 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal num3 = 0M;
    if (nullable5.GetValueOrDefault() < num3 & nullable5.HasValue)
    {
      nullable1 = tran.DebitAmt;
      nullable3 = tran.CreditAmt;
      Decimal? nullable6 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal num4 = 0M;
      if (nullable6.GetValueOrDefault() > num4 & nullable6.HasValue)
        return;
    }
    nullable3 = tran.CuryDebitAmt;
    nullable1 = tran.CuryCreditAmt;
    Decimal? nullable7 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal num5 = 0M;
    if (!(nullable7.GetValueOrDefault() == num5 & nullable7.HasValue))
    {
      nullable1 = tran.CuryDebitAmt;
      nullable3 = tran.CuryCreditAmt;
      Decimal? nullable8 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal num6 = 0M;
      if (nullable8.GetValueOrDefault() < num6 & nullable8.HasValue)
      {
        tran.CuryCreditAmt = new Decimal?(Math.Abs(tran.CuryDebitAmt.Value - tran.CuryCreditAmt.Value));
        GLTran glTran = tran;
        Decimal? nullable9 = tran.DebitAmt;
        Decimal num7 = nullable9.Value;
        nullable9 = tran.CreditAmt;
        Decimal num8 = nullable9.Value;
        Decimal? nullable10 = new Decimal?(Math.Abs(num7 - num8));
        glTran.CreditAmt = nullable10;
        tran.CuryDebitAmt = new Decimal?(0M);
        tran.DebitAmt = new Decimal?(0M);
      }
      else
      {
        tran.CuryDebitAmt = new Decimal?(Math.Abs(tran.CuryDebitAmt.Value - tran.CuryCreditAmt.Value));
        GLTran glTran = tran;
        Decimal? nullable11 = tran.DebitAmt;
        Decimal num9 = nullable11.Value;
        nullable11 = tran.CreditAmt;
        Decimal num10 = nullable11.Value;
        Decimal? nullable12 = new Decimal?(Math.Abs(num9 - num10));
        glTran.DebitAmt = nullable12;
        tran.CuryCreditAmt = new Decimal?(0M);
        tran.CreditAmt = new Decimal?(0M);
      }
    }
    else
    {
      nullable3 = tran.DebitAmt;
      nullable1 = tran.CreditAmt;
      Decimal? nullable13 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal num11 = 0M;
      if (nullable13.GetValueOrDefault() < num11 & nullable13.HasValue)
      {
        tran.CuryCreditAmt = new Decimal?(Math.Abs(tran.CuryDebitAmt.Value - tran.CuryCreditAmt.Value));
        GLTran glTran = tran;
        Decimal? nullable14 = tran.DebitAmt;
        Decimal num12 = nullable14.Value;
        nullable14 = tran.CreditAmt;
        Decimal num13 = nullable14.Value;
        Decimal? nullable15 = new Decimal?(Math.Abs(num12 - num13));
        glTran.CreditAmt = nullable15;
        tran.CuryDebitAmt = new Decimal?(0M);
        tran.DebitAmt = new Decimal?(0M);
      }
      else
      {
        tran.CuryDebitAmt = new Decimal?(Math.Abs(tran.CuryDebitAmt.Value - tran.CuryCreditAmt.Value));
        GLTran glTran = tran;
        Decimal? nullable16 = tran.DebitAmt;
        Decimal num14 = nullable16.Value;
        nullable16 = tran.CreditAmt;
        Decimal num15 = nullable16.Value;
        Decimal? nullable17 = new Decimal?(Math.Abs(num14 - num15));
        glTran.DebitAmt = nullable17;
        tran.CuryCreditAmt = new Decimal?(0M);
        tran.CreditAmt = new Decimal?(0M);
      }
    }
  }

  /// <summary>
  /// This method will return without executing if <see cref="!:IsNeedUpdateHistoryForTransaction" /> returns <c>false</c>.
  /// </summary>
  private void UpdateHistory(
    GLTran tran,
    Account acct,
    string finPeriodID,
    PX.Objects.GL.PostGraph.HistoryUpdateAmountType amountUpdateType,
    PX.Objects.GL.PostGraph.HistoryUpdateMode historyUpdateMode)
  {
    bool flag = historyUpdateMode == PX.Objects.GL.PostGraph.HistoryUpdateMode.NextYearRetainedEarnings;
    AcctHist acctHist1 = new AcctHist();
    acctHist1.AccountID = acct.AccountID;
    acctHist1.FinPeriodID = finPeriodID;
    acctHist1.LedgerID = tran.LedgerID;
    acctHist1.BranchID = tran.BranchID;
    acctHist1.SubID = tran.SubID;
    acctHist1.CuryID = acct.CuryID;
    AcctHist acctHist2 = (AcctHist) ((PXGraph) this).Caches[typeof (AcctHist)].Insert((object) acctHist1);
    if (acctHist2 != null)
    {
      acctHist2.FinFlag = new bool?(amountUpdateType == PX.Objects.GL.PostGraph.HistoryUpdateAmountType.FinAmounts);
      Decimal? nullable1 = tran.CuryDebitAmt;
      Decimal num1 = 0M;
      if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
      {
        nullable1 = tran.CuryCreditAmt;
        Decimal num2 = 0M;
        if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
          goto label_5;
      }
      nullable1 = tran.DebitAmt;
      Decimal num3 = 0M;
      if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
      {
        nullable1 = tran.CreditAmt;
        Decimal num4 = 0M;
        if (!(nullable1.GetValueOrDefault() == num4 & nullable1.HasValue))
          goto label_5;
      }
      Decimal? nullable2;
      Decimal? nullable3;
      Decimal? nullable4;
      if (!flag)
      {
        AcctHist acctHist3 = acctHist2;
        nullable1 = acctHist3.PtdDebit;
        Decimal? debitAmt1 = tran.DebitAmt;
        Decimal num5 = 0M;
        Decimal? nullable5;
        if (!(debitAmt1.GetValueOrDefault() == num5 & debitAmt1.HasValue))
        {
          Decimal? creditAmt = tran.CreditAmt;
          Decimal num6 = 0M;
          if (creditAmt.GetValueOrDefault() == num6 & creditAmt.HasValue)
          {
            Decimal? debitAmt2 = tran.DebitAmt;
            nullable2 = tran.CreditAmt;
            nullable5 = debitAmt2.HasValue & nullable2.HasValue ? new Decimal?(debitAmt2.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
            goto label_11;
          }
        }
        nullable5 = new Decimal?(0M);
label_11:
        nullable3 = nullable5;
        Decimal? nullable6;
        if (!(nullable1.HasValue & nullable3.HasValue))
        {
          nullable2 = new Decimal?();
          nullable6 = nullable2;
        }
        else
          nullable6 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
        acctHist3.PtdDebit = nullable6;
        AcctHist acctHist4 = acctHist2;
        nullable3 = acctHist4.PtdCredit;
        nullable2 = tran.CreditAmt;
        Decimal num7 = 0M;
        Decimal? nullable7;
        if (!(nullable2.GetValueOrDefault() == num7 & nullable2.HasValue))
        {
          nullable2 = tran.DebitAmt;
          Decimal num8 = 0M;
          if (nullable2.GetValueOrDefault() == num8 & nullable2.HasValue)
          {
            nullable2 = tran.CreditAmt;
            nullable4 = tran.DebitAmt;
            nullable7 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            goto label_18;
          }
        }
        nullable7 = new Decimal?(0M);
label_18:
        nullable1 = nullable7;
        Decimal? nullable8;
        if (!(nullable3.HasValue & nullable1.HasValue))
        {
          nullable4 = new Decimal?();
          nullable8 = nullable4;
        }
        else
          nullable8 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
        acctHist4.PtdCredit = nullable8;
        if (acctHist2.CuryID != null)
        {
          AcctHist acctHist5 = acctHist2;
          nullable1 = acctHist5.CuryPtdDebit;
          nullable4 = tran.CuryDebitAmt;
          Decimal num9 = 0M;
          Decimal? nullable9;
          if (!(nullable4.GetValueOrDefault() == num9 & nullable4.HasValue))
          {
            nullable4 = tran.CuryCreditAmt;
            Decimal num10 = 0M;
            if (nullable4.GetValueOrDefault() == num10 & nullable4.HasValue)
            {
              nullable4 = tran.CuryDebitAmt;
              nullable2 = tran.CuryCreditAmt;
              nullable9 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
              goto label_26;
            }
          }
          nullable9 = new Decimal?(0M);
label_26:
          nullable3 = nullable9;
          Decimal? nullable10;
          if (!(nullable1.HasValue & nullable3.HasValue))
          {
            nullable2 = new Decimal?();
            nullable10 = nullable2;
          }
          else
            nullable10 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
          acctHist5.CuryPtdDebit = nullable10;
          AcctHist acctHist6 = acctHist2;
          nullable3 = acctHist6.CuryPtdCredit;
          nullable2 = tran.CuryCreditAmt;
          Decimal num11 = 0M;
          Decimal? nullable11;
          if (!(nullable2.GetValueOrDefault() == num11 & nullable2.HasValue))
          {
            nullable2 = tran.CuryDebitAmt;
            Decimal num12 = 0M;
            if (nullable2.GetValueOrDefault() == num12 & nullable2.HasValue)
            {
              nullable2 = tran.CuryCreditAmt;
              nullable4 = tran.CuryDebitAmt;
              nullable11 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
              goto label_33;
            }
          }
          nullable11 = new Decimal?(0M);
label_33:
          nullable1 = nullable11;
          Decimal? nullable12;
          if (!(nullable3.HasValue & nullable1.HasValue))
          {
            nullable4 = new Decimal?();
            nullable12 = nullable4;
          }
          else
            nullable12 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
          acctHist6.CuryPtdCredit = nullable12;
        }
        else
        {
          acctHist2.CuryPtdDebit = acctHist2.PtdDebit;
          acctHist2.CuryPtdCredit = acctHist2.PtdCredit;
        }
      }
      if (acct.Type == "I" || acct.Type == "L")
      {
        AcctHist acctHist7 = acctHist2;
        nullable1 = acctHist7.YtdBalance;
        nullable4 = tran.CreditAmt;
        nullable2 = tran.DebitAmt;
        nullable3 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable13;
        if (!(nullable1.HasValue & nullable3.HasValue))
        {
          nullable2 = new Decimal?();
          nullable13 = nullable2;
        }
        else
          nullable13 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
        acctHist7.YtdBalance = nullable13;
        if (flag)
        {
          AcctHist acctHist8 = acctHist2;
          nullable3 = acctHist8.BegBalance;
          nullable2 = tran.CreditAmt;
          nullable4 = tran.DebitAmt;
          nullable1 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable14;
          if (!(nullable3.HasValue & nullable1.HasValue))
          {
            nullable4 = new Decimal?();
            nullable14 = nullable4;
          }
          else
            nullable14 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
          acctHist8.BegBalance = nullable14;
        }
        if (acctHist2.CuryID != null)
        {
          AcctHist acctHist9 = acctHist2;
          nullable1 = acctHist9.CuryYtdBalance;
          nullable4 = tran.CuryCreditAmt;
          nullable2 = tran.CuryDebitAmt;
          nullable3 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable15;
          if (!(nullable1.HasValue & nullable3.HasValue))
          {
            nullable2 = new Decimal?();
            nullable15 = nullable2;
          }
          else
            nullable15 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
          acctHist9.CuryYtdBalance = nullable15;
          if (!flag)
            return;
          AcctHist acctHist10 = acctHist2;
          nullable3 = acctHist10.CuryBegBalance;
          nullable2 = tran.CuryCreditAmt;
          nullable4 = tran.CuryDebitAmt;
          nullable1 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable16;
          if (!(nullable3.HasValue & nullable1.HasValue))
          {
            nullable4 = new Decimal?();
            nullable16 = nullable4;
          }
          else
            nullable16 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
          acctHist10.CuryBegBalance = nullable16;
          return;
        }
        acctHist2.CuryYtdBalance = acctHist2.YtdBalance;
        if (!flag)
          return;
        acctHist2.CuryBegBalance = acctHist2.BegBalance;
        return;
      }
      AcctHist acctHist11 = acctHist2;
      nullable1 = acctHist11.YtdBalance;
      nullable4 = tran.DebitAmt;
      nullable2 = tran.CreditAmt;
      nullable3 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable17;
      if (!(nullable1.HasValue & nullable3.HasValue))
      {
        nullable2 = new Decimal?();
        nullable17 = nullable2;
      }
      else
        nullable17 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
      acctHist11.YtdBalance = nullable17;
      if (flag)
      {
        AcctHist acctHist12 = acctHist2;
        nullable3 = acctHist12.BegBalance;
        nullable2 = tran.DebitAmt;
        nullable4 = tran.CreditAmt;
        nullable1 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable18;
        if (!(nullable3.HasValue & nullable1.HasValue))
        {
          nullable4 = new Decimal?();
          nullable18 = nullable4;
        }
        else
          nullable18 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
        acctHist12.BegBalance = nullable18;
      }
      if (acctHist2.CuryID != null)
      {
        AcctHist acctHist13 = acctHist2;
        nullable1 = acctHist13.CuryYtdBalance;
        nullable4 = tran.CuryDebitAmt;
        nullable2 = tran.CuryCreditAmt;
        nullable3 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable19;
        if (!(nullable1.HasValue & nullable3.HasValue))
        {
          nullable2 = new Decimal?();
          nullable19 = nullable2;
        }
        else
          nullable19 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
        acctHist13.CuryYtdBalance = nullable19;
        if (!flag)
          return;
        AcctHist acctHist14 = acctHist2;
        nullable3 = acctHist14.CuryBegBalance;
        nullable2 = tran.CuryDebitAmt;
        nullable4 = tran.CuryCreditAmt;
        nullable1 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable20;
        if (!(nullable3.HasValue & nullable1.HasValue))
        {
          nullable4 = new Decimal?();
          nullable20 = nullable4;
        }
        else
          nullable20 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
        acctHist14.CuryBegBalance = nullable20;
        return;
      }
      acctHist2.CuryYtdBalance = acctHist2.YtdBalance;
      if (!flag)
        return;
      acctHist2.CuryBegBalance = acctHist2.BegBalance;
      return;
label_5:
      throw new PXException("Both Debit and Credit parts of transaction are not zero. Exiting with error.");
    }
  }

  protected virtual void UpdateAllocationBalance(Batch b)
  {
    foreach (PXResult<GLAllocationAccountHistory, Account> pxResult in ((PXSelectBase<GLAllocationAccountHistory>) this.BatchAllocHistory).Select(new object[2]
    {
      (object) b.BatchNbr,
      (object) b.Module
    }))
    {
      GLAllocationAccountHistory allocationAccountHistory = PXResult<GLAllocationAccountHistory, Account>.op_Implicit(pxResult);
      Account account = PXResult<GLAllocationAccountHistory, Account>.op_Implicit(pxResult);
      Decimal? nullable1 = allocationAccountHistory.AllocatedAmount;
      if (this.DoExceedsNegligibleDifference(new Decimal?(nullable1 ?? 0.0M)))
      {
        AcctHist acctHist1 = new AcctHist();
        acctHist1.AccountID = allocationAccountHistory.AccountID;
        acctHist1.FinPeriodID = b.TranPeriodID;
        acctHist1.LedgerID = b.LedgerID;
        acctHist1.SubID = allocationAccountHistory.SubID;
        acctHist1.CuryID = account.CuryID;
        acctHist1.BranchID = allocationAccountHistory.BranchID;
        if (((PXGraph) this).Caches[typeof (Ledger)].Current == null)
        {
          Ledger ledger = PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelectReadonly<Ledger, Where<Ledger.ledgerID, Equal<Required<Batch.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) b.LedgerID
          }));
          if (ledger != null)
            acctHist1.BalanceType = ledger.BalanceType;
        }
        AcctHist acctHist2 = (AcctHist) ((PXGraph) this).Caches[typeof (AcctHist)].Insert((object) acctHist1);
        if (acctHist2 != null)
        {
          AcctHist acctHist3 = acctHist2;
          Decimal num1 = acctHist2.AllocPtdBalance ?? 0.0M;
          nullable1 = allocationAccountHistory.AllocatedAmount;
          Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num1 + nullable1.GetValueOrDefault()) : new Decimal?();
          acctHist3.AllocPtdBalance = nullable2;
          AcctHist acctHist4 = acctHist2;
          Decimal num2 = acctHist2.AllocBegBalance ?? 0.0M;
          nullable1 = allocationAccountHistory.PriorPeriodsAllocAmount;
          Decimal? nullable3 = nullable1.HasValue ? new Decimal?(num2 + nullable1.GetValueOrDefault()) : new Decimal?();
          acctHist4.AllocBegBalance = nullable3;
        }
      }
    }
  }

  private bool UpdateConsolidationBalance(Batch b)
  {
    bool flag = false;
    if (b.BatchType == "C")
    {
      GLConsolBatch glConsolBatch = PXResultset<GLConsolBatch>.op_Implicit(PXSelectBase<GLConsolBatch, PXSelect<GLConsolBatch, Where<GLConsolBatch.batchNbr, Equal<Required<GLConsolBatch.batchNbr>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) b.BatchNbr
      }));
      if (glConsolBatch == null && b.AutoReverseCopy.GetValueOrDefault())
        glConsolBatch = PXResultset<GLConsolBatch>.op_Implicit(PXSelectBase<GLConsolBatch, PXSelect<GLConsolBatch, Where<GLConsolBatch.batchNbr, Equal<Required<GLConsolBatch.batchNbr>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) b.OrigBatchNbr
        }));
      if (glConsolBatch != null)
      {
        PXCache cach = ((PXGraph) this).Caches[typeof (ConsolHist)];
        foreach (AcctHist acctHist in ((PXGraph) this).Caches[typeof (AcctHist)].Inserted)
        {
          int? nullable1 = acctHist.AccountID;
          int? nullable2 = ((PXSelectBase<GLSetup>) this.glsetup).Current.YtdNetIncAccountID;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          {
            nullable2 = acctHist.AccountID;
            nullable1 = ((PXSelectBase<GLSetup>) this.glsetup).Current.RetEarnAccountID;
            if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue) || !(acctHist.FinPeriodID != b.FinPeriodID))
            {
              ConsolHist consolHist1 = new ConsolHist();
              consolHist1.SetupID = glConsolBatch.SetupID;
              consolHist1.BranchID = acctHist.BranchID;
              consolHist1.LedgerID = acctHist.LedgerID;
              consolHist1.AccountID = acctHist.AccountID;
              consolHist1.SubID = acctHist.SubID;
              consolHist1.FinPeriodID = acctHist.FinPeriodID;
              ConsolHist consolHist2 = (ConsolHist) cach.Insert((object) consolHist1);
              if (consolHist2 != null)
              {
                ConsolHist consolHist3 = consolHist2;
                Decimal? nullable3 = consolHist3.PtdCredit;
                Decimal? nullable4 = acctHist.FinPtdCredit;
                consolHist3.PtdCredit = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                ConsolHist consolHist4 = consolHist2;
                nullable4 = consolHist4.PtdDebit;
                nullable3 = acctHist.FinPtdDebit;
                consolHist4.PtdDebit = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                flag = true;
              }
            }
          }
        }
      }
    }
    return flag;
  }

  private void DecimalSwap(ref Decimal? d1, ref Decimal? d2)
  {
    Decimal? nullable = d1;
    d1 = d2;
    d2 = nullable;
  }

  public virtual Batch ReverseBatchProc(Batch b)
  {
    Batch batch1 = PXCache<Batch>.CreateCopy(b);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      int? parentOrganizationId = PXAccess.GetParentOrganizationID(batch1.BranchID);
      batch1.OrigBatchNbr = batch1.BatchNbr;
      batch1.OrigModule = batch1.Module;
      batch1.BatchNbr = (string) null;
      batch1.NoteID = new Guid?();
      batch1.ReverseCount = new int?(0);
      try
      {
        FinPeriod offsetPeriod = this.FinPeriodRepository.GetOffsetPeriod(batch1.FinPeriodID, 1, parentOrganizationId);
        batch1.FinPeriodID = offsetPeriod.FinPeriodID;
        batch1.TranPeriodID = offsetPeriod.MasterFinPeriodID;
      }
      catch (PXFinPeriodException ex)
      {
        throw new PXFinPeriodException("There are no open Financial Periods after {0} defined in the system.", new object[1]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForError(batch1.FinPeriodID)
        });
      }
      if (batch1.FinPeriodID == null)
        throw new PXException("There are no open Financial Periods defined in the system.");
      batch1.DateEntered = new DateTime?(this.FinPeriodRepository.PeriodStartDate(batch1.FinPeriodID, parentOrganizationId));
      batch1.AutoReverse = new bool?(false);
      batch1.AutoReverseCopy = new bool?(true);
      batch1.CuryInfoID = new long?();
      PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) b.CuryInfoID
      }));
      if (currencyInfo != null)
        batch1.CuryInfoID = currencyInfo.CuryInfoID;
      batch1.Posted = new bool?(false);
      batch1.Status = "U";
      batch1 = (Batch) ((PXGraph) this).Caches[typeof (Batch)].Insert((object) batch1);
      PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this).Caches[typeof (Batch)], (object) b, ((PXGraph) this).Caches[typeof (Batch)], (object) batch1, (PXNoteAttribute.IPXCopySettings) null);
      foreach (PXResult<GLTran> pxResult in ((PXSelectBase<GLTran>) this.GLTran_Module_BatNbr).Select(new object[2]
      {
        (object) b.Module,
        (object) b.BatchNbr
      }))
      {
        GLTran copy = PXCache<GLTran>.CreateCopy(PXResult<GLTran>.op_Implicit(pxResult));
        copy.OrigBatchNbr = copy.BatchNbr;
        copy.OrigModule = copy.Module;
        copy.BatchNbr = (string) null;
        copy.CuryInfoID = batch1.CuryInfoID;
        copy.CATranID = new long?();
        copy.TranID = new int?();
        copy.Posted = new bool?(false);
        copy.PMTranID = new long?();
        copy.OrigPMTranID = new long?();
        GLTran glTran1 = copy;
        Decimal num = -1M;
        Decimal? qty = copy.Qty;
        Decimal? nullable = qty.HasValue ? new Decimal?(num * qty.GetValueOrDefault()) : new Decimal?();
        glTran1.Qty = nullable;
        copy.TranDate = batch1.DateEntered;
        FinPeriodIDAttribute.SetPeriodsByMaster<GLTran.finPeriodID>(((PXGraph) this).Caches[typeof (GLTran)], (object) copy, batch1.TranPeriodID);
        Decimal? curyDebitAmt = copy.CuryDebitAmt;
        copy.CuryDebitAmt = copy.CuryCreditAmt;
        copy.CuryCreditAmt = curyDebitAmt;
        Decimal? debitAmt = copy.DebitAmt;
        copy.DebitAmt = copy.CreditAmt;
        copy.CreditAmt = debitAmt;
        copy.NoteID = new Guid?();
        GLTran glTran2;
        ((PXGraph) this).Caches[typeof (GLTran)].SetValueExt<GLTran.taxID>((object) (glTran2 = (GLTran) ((PXGraph) this).Caches[typeof (GLTran)].Insert((object) copy)), (object) glTran2.TaxID);
      }
      ((PXGraph) this).Caches[typeof (Batch)].Persist((PXDBOperation) 2);
      foreach (GLTran data in ((PXGraph) this).Caches[typeof (GLTran)].Inserted)
      {
        foreach (Batch batch2 in ((PXGraph) this).Caches[typeof (Batch)].Cached)
        {
          if (object.Equals((object) data.OrigBatchNbr, (object) batch2.OrigBatchNbr))
          {
            data.BatchNbr = batch2.BatchNbr;
            data.CuryInfoID = batch2.CuryInfoID;
            break;
          }
        }
        CATran caTran1 = GLCashTranIDAttribute.DefaultValues(((PXGraph) this).Caches[typeof (GLTran)], (object) data);
        if (caTran1 != null)
        {
          CATran caTran2 = (CATran) ((PXGraph) this).Caches[typeof (CATran)].Insert((object) caTran1);
          ((PXGraph) this).Caches[typeof (CATran)].PersistInserted((object) caTran2);
          long int64 = Convert.ToInt64((object) PXDatabase.SelectIdentity());
          data.CATranID = new long?(int64);
          caTran2.TranID = new long?(int64);
          ((PXGraph) this).Caches[typeof (CATran)].Normalize();
        }
      }
      ((PXGraph) this).Caches[typeof (GLTran)].Persist((PXDBOperation) 2);
      ((PXGraph) this).Caches[typeof (CADailySummary)].Persist((PXDBOperation) 2);
      transactionScope.Complete((PXGraph) this);
    }
    ((PXGraph) this).Caches[typeof (Batch)].Persisted(false);
    ((PXGraph) this).Caches[typeof (GLTran)].Persisted(false);
    ((PXGraph) this).Caches[typeof (CATran)].Persisted(false);
    ((PXGraph) this).Caches[typeof (CADailySummary)].Persisted(false);
    return batch1;
  }

  private void AccountForLegacyFinancialPeriods(Ledger ledger, ref string startingPeriod)
  {
    GLHistory glHistory = PXResultset<GLHistory>.op_Implicit(PXSelectBase<GLHistory, PXSelectGroupBy<GLHistory, Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>, And<GLHistory.detDeleted, Equal<True>>>, Aggregate<Max<GLHistory.finPeriodID>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ledger.LedgerID
    }));
    if (glHistory == null || glHistory.FinPeriodID == null || string.CompareOrdinal(glHistory.FinPeriodID, startingPeriod) < 0)
      return;
    startingPeriod = this.FinPeriodRepository.NextPeriod(glHistory.FinPeriodID, new int?(0));
  }

  public virtual void IntegrityCheckProc(Ledger ledger, string startingPeriod)
  {
    if (string.IsNullOrEmpty(startingPeriod))
      throw new PXArgumentException(nameof (startingPeriod));
    this.AccountForLegacyFinancialPeriods(ledger, ref startingPeriod);
    this._IsIntegrityCheck = true;
    this._IntegrityCheckStartingPeriod = startingPeriod;
    foreach (PXResult<MasterFinPeriod> pxResult in PXSelectBase<MasterFinPeriod, PXSelectReadonly<MasterFinPeriod, Where<MasterFinPeriod.finPeriodID, GreaterEqual<Required<MasterFinPeriod.finPeriodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) startingPeriod
    }))
    {
      IEnumerable<PXResult<GLTran, CATran, Account, Ledger>> glHistoryUpdateData = ((IEnumerable<PXResult<GLTran>>) ((PXSelectBase<GLTran>) this.TransactionsForPeriod).Select(new object[2]
      {
        (object) PXResult<MasterFinPeriod>.op_Implicit(pxResult).FinPeriodID,
        (object) ledger.LedgerID
      })).AsEnumerable<PXResult<GLTran>>().Cast<PXResult<GLTran, CATran, Account, Ledger>>();
      ((PXSelectBase) this.TransactionsForPeriod).View.Clear();
      this.UpdateHistoryProc(glHistoryUpdateData);
      ((PXSelectBase) this.TransactionsForPeriod).Cache.Clear();
    }
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(new int?(0), startingPeriod);
    if (prevPeriod != null)
    {
      string finPeriodId1 = prevPeriod.FinPeriodID;
    }
    FinPeriod financialPeriodOfYear = this.FinPeriodRepository.FindLastFinancialPeriodOfYear(PX.Objects.GL.FinPeriods.FinPeriodUtils.FiscalYear(startingPeriod), new int?(0));
    if (financialPeriodOfYear != null)
    {
      string finPeriodId2 = financialPeriodOfYear.FinPeriodID;
    }
    GLHistoryFilter glHistoryFilter = new GLHistoryFilter();
    glHistoryFilter.FinPeriodID = startingPeriod;
    ((PXSelectBase<GLHistoryFilter>) this.Filter).Current = glHistoryFilter;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PXUpdateJoin<Set<GLHistory.curyFinBegBalance, IsNull<AcctHist2.curyFinYtdBalance, Zero>, Set<GLHistory.curyFinYtdBalance, IsNull<AcctHist2.curyFinYtdBalance, Zero>, Set<GLHistory.finBegBalance, IsNull<AcctHist2.finYtdBalance, Zero>, Set<GLHistory.finYtdBalance, IsNull<AcctHist2.finYtdBalance, Zero>, Set<GLHistory.curyFinPtdCredit, Zero, Set<GLHistory.curyFinPtdDebit, Zero, Set<GLHistory.finPtdCredit, Zero, Set<GLHistory.finPtdDebit, Zero, Set<GLHistory.allocBegBalance, Zero, Set<GLHistory.allocPtdBalance, Zero, Set<GLHistory.finPtdRevalued, Zero>>>>>>>>>>>, GLHistory, CrossJoin<GLSetup, InnerJoin<Branch, On<GLHistory.branchID, Equal<Branch.branchID>>, LeftJoin<FinPeriod, On<GLHistory.finPeriodID, Equal<FinPeriod.finPeriodID>, And<Branch.organizationID, Equal<FinPeriod.organizationID>>>, LeftJoin<OrganizationFinPeriodMin, On<OrganizationFinPeriodMin.organizationID, Equal<Branch.organizationID>>, InnerJoin<Account, On<GLHistory.accountID, Equal<Account.accountID>>, LeftJoin<GLHistoryByPeriodCurrent, On<GLHistoryByPeriodCurrent.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryByPeriodCurrent.branchID, Equal<GLHistory.branchID>, And<GLHistoryByPeriodCurrent.accountID, Equal<GLHistory.accountID>, And<GLHistoryByPeriodCurrent.subID, Equal<GLHistory.subID>>>>>, LeftJoin<AcctHist2, On<GLHistoryByPeriodCurrent.branchID, Equal<AcctHist2.branchID>, And<GLHistoryByPeriodCurrent.ledgerID, Equal<AcctHist2.ledgerID>, And<GLHistoryByPeriodCurrent.accountID, Equal<AcctHist2.accountID>, And<GLHistoryByPeriodCurrent.subID, Equal<AcctHist2.subID>, And<GLHistoryByPeriodCurrent.lastActivityPeriod, Equal<AcctHist2.finPeriodID>, And<Where<Account.type, Equal<AccountType.asset>, Or2<Where<Account.type, Equal<AccountType.liability>, And<Account.accountID, NotEqual<GLSetup.ytdNetIncAccountID>>>, Or<FinPeriod.finYear, Equal<Substring<GLHistoryByPeriodCurrent.lastActivityPeriod, int1, int4>>>>>>>>>>>>>>>>>>, Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>, And<GLHistory.finPeriodID, GreaterEqual<OrganizationFinPeriodMin.finPeriodID>>>>.Update((PXGraph) this, new object[1]
      {
        (object) ledger.LedgerID
      });
      PXUpdateJoin<Set<GLHistory.curyTranBegBalance, IsNull<AcctHist2.curyTranYtdBalance, Zero>, Set<GLHistory.curyTranYtdBalance, IsNull<AcctHist2.curyTranYtdBalance, Zero>, Set<GLHistory.tranBegBalance, IsNull<AcctHist2.tranYtdBalance, Zero>, Set<GLHistory.tranYtdBalance, IsNull<AcctHist2.tranYtdBalance, Zero>, Set<GLHistory.curyTranPtdCredit, Zero, Set<GLHistory.curyTranPtdDebit, Zero, Set<GLHistory.tranPtdCredit, Zero, Set<GLHistory.tranPtdDebit, Zero>>>>>>>>, GLHistory, CrossJoin<GLSetup, LeftJoin<FinPeriod, On<GLHistory.finPeriodID, Equal<FinPeriod.finPeriodID>, And<FinPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>>>, InnerJoin<Account, On<GLHistory.accountID, Equal<Account.accountID>>, LeftJoin<GLHistoryByPeriodMasterCurrent, On<GLHistoryByPeriodMasterCurrent.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryByPeriodMasterCurrent.branchID, Equal<GLHistory.branchID>, And<GLHistoryByPeriodMasterCurrent.accountID, Equal<GLHistory.accountID>, And<GLHistoryByPeriodMasterCurrent.subID, Equal<GLHistory.subID>>>>>, LeftJoin<AcctHist2, On<GLHistoryByPeriodMasterCurrent.branchID, Equal<AcctHist2.branchID>, And<GLHistoryByPeriodMasterCurrent.ledgerID, Equal<AcctHist2.ledgerID>, And<GLHistoryByPeriodMasterCurrent.accountID, Equal<AcctHist2.accountID>, And<GLHistoryByPeriodMasterCurrent.subID, Equal<AcctHist2.subID>, And<GLHistoryByPeriodMasterCurrent.lastActivityPeriod, Equal<AcctHist2.finPeriodID>, And<Where<Account.type, Equal<AccountType.asset>, Or2<Where<Account.type, Equal<AccountType.liability>, And<Account.accountID, NotEqual<GLSetup.ytdNetIncAccountID>>>, Or<FinPeriod.finYear, Equal<Substring<GLHistoryByPeriodMasterCurrent.lastActivityPeriod, int1, int4>>>>>>>>>>>>>>>>, Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>, And<GLHistory.finPeriodID, GreaterEqual<Required<GLHistory.finPeriodID>>>>>.Update((PXGraph) this, new object[2]
      {
        (object) ledger.LedgerID,
        (object) glHistoryFilter.FinPeriodID
      });
      ((PXGraph) this).Caches[typeof (AcctHist)].Persist((PXDBOperation) 2);
      glHistoryFilter.FinPeriodID = startingPeriod;
      ((PXSelectBase<GLHistoryFilter>) this.Filter).Current = glHistoryFilter;
      string finPeriodIdOfYear = PX.Objects.GL.FinPeriods.FinPeriodUtils.GetFirstFinPeriodIDOfYear(glHistoryFilter.FinPeriodID);
      PXUpdateJoin<Set<GLHistory.finBegBalance, IsNull<GLHistoryCalcRetainedEarnings.finBegBalanceNew, Zero>, Set<GLHistory.finYtdBalance, IsNull<GLHistoryCalcRetainedEarnings.finYtdBalanceNew, Zero>, Set<GLHistory.curyFinBegBalance, IsNull<GLHistoryCalcRetainedEarnings.curyFinBegBalanceNew, Zero>, Set<GLHistory.curyFinYtdBalance, IsNull<GLHistoryCalcRetainedEarnings.curyFinYtdBalanceNew, Zero>>>>>, GLHistory, LeftJoin<Branch, On<GLHistory.branchID, Equal<Branch.branchID>>, LeftJoin<OrganizationFinPeriodMin, On<OrganizationFinPeriodMin.organizationID, Equal<Branch.organizationID>>, LeftJoin<GLHistoryCalcRetainedEarnings, On<GLHistoryCalcRetainedEarnings.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryCalcRetainedEarnings.branchID, Equal<GLHistory.branchID>, And<GLHistoryCalcRetainedEarnings.finPeriodID, Equal<GLHistory.finPeriodID>, And<GLHistoryCalcRetainedEarnings.accountID, Equal<GLHistory.accountID>, And<GLHistoryCalcRetainedEarnings.subID, Equal<GLHistory.subID>>>>>>>>>, Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>, And<GLHistory.accountID, Equal<Required<GLHistory.accountID>>, And<GLHistory.finPeriodID, GreaterEqual<IsNull<OrganizationFinPeriodMin.finPeriodID, Required<OrganizationFinPeriodMin.finPeriodID>>>>>>>.Update((PXGraph) this, new object[3]
      {
        (object) ledger.LedgerID,
        (object) ((PXSelectBase<GLSetup>) this.glsetup).Current.RetEarnAccountID,
        (object) finPeriodIdOfYear
      });
      PXUpdateJoin<Set<GLHistory.tranBegBalance, IsNull<GLHistoryCalcRetainedEarnings.tranBegBalanceNew, Zero>, Set<GLHistory.tranYtdBalance, IsNull<GLHistoryCalcRetainedEarnings.tranYtdBalanceNew, Zero>, Set<GLHistory.curyTranBegBalance, IsNull<GLHistoryCalcRetainedEarnings.curyTranBegBalanceNew, Zero>, Set<GLHistory.curyTranYtdBalance, IsNull<GLHistoryCalcRetainedEarnings.curyTranYtdBalanceNew, Zero>>>>>, GLHistory, LeftJoin<GLHistoryCalcRetainedEarnings, On<GLHistoryCalcRetainedEarnings.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryCalcRetainedEarnings.branchID, Equal<GLHistory.branchID>, And<GLHistoryCalcRetainedEarnings.finPeriodID, Equal<GLHistory.finPeriodID>, And<GLHistoryCalcRetainedEarnings.accountID, Equal<GLHistory.accountID>, And<GLHistoryCalcRetainedEarnings.subID, Equal<GLHistory.subID>>>>>>>, Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>, And<GLHistory.accountID, Equal<Required<GLHistory.accountID>>, And<GLHistory.finPeriodID, GreaterEqual<Required<GLHistory.finPeriodID>>>>>>.Update((PXGraph) this, new object[3]
      {
        (object) ledger.LedgerID,
        (object) ((PXSelectBase<GLSetup>) this.glsetup).Current.RetEarnAccountID,
        (object) glHistoryFilter.FinPeriodID
      });
      transactionScope.Complete((PXGraph) this);
    }
    ((PXGraph) this).Caches[typeof (AcctHist)].Persisted(false);
  }

  public virtual void PostBatchesRequiredPosting()
  {
    PXResultset<Batch> pxResultset = PXSelectBase<Batch, PXSelectReadonly<Batch, Where<Batch.requirePost, Equal<True>, And<Batch.posted, Equal<False>, And<Batch.postErrorCount, Less<Batch.postErrorCountLimit>>>>, OrderBy<Asc<Batch.postErrorCount>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    bool recordComesFirst = PXTimeStampScope.GetRecordComesFirst(typeof (Batch));
    PXTimeStampScope.SetRecordComesFirst(typeof (Batch), true);
    foreach (PXResult<Batch> pxResult in pxResultset)
    {
      Batch b = PXResult<Batch>.op_Implicit(pxResult);
      object copy = ((PXSelectBase) this.BatchModule).Cache.CreateCopy((object) b);
      try
      {
        ((PXGraph) this).Clear();
        ((PXGraph) this).SelectTimeStamp();
        using (new RunningFlagScope<PX.Objects.GL.PostGraph>())
          this.PostBatchProc(b, true);
      }
      catch (Exception ex1)
      {
        PXTrace.WriteError(ex1);
        try
        {
          ((PXSelectBase) this.BatchModule).Cache.RestoreCopy(copy, (object) b);
          b.PostErrorCount = new int?(b.PostErrorCount.GetValueOrDefault() + 1);
          ((PXSelectBase<Batch>) this.BatchModule).Update(b);
          ((PXSelectBase) this.BatchModule).Cache.Persist((PXDBOperation) 1);
        }
        catch (Exception ex2)
        {
          PXTrace.WriteError(ex2);
        }
      }
      PXTimeStampScope.SetRecordComesFirst(typeof (Batch), recordComesFirst);
    }
  }

  public virtual void UpdateHistoryProc(
    IEnumerable<PXResult<GLTran, CATran, Account, Ledger>> glHistoryUpdateData)
  {
    if (glHistoryUpdateData == null)
      throw new ArgumentNullException(nameof (glHistoryUpdateData));
    foreach (PXResult<GLTran, CATran, Account, Ledger> pxResult in glHistoryUpdateData)
    {
      GLTran tran = PXResult<GLTran, CATran, Account, Ledger>.op_Implicit(pxResult);
      CATran caTran = PXResult<GLTran, CATran, Account, Ledger>.op_Implicit(pxResult);
      Account acct1 = ((PXSelectBase<Account>) this.Account_AccountID).Current = PXResult<GLTran, CATran, Account, Ledger>.op_Implicit(pxResult);
      Ledger ledger = ((PXSelectBase<Ledger>) this.Ledger_LedgerID).Current = PXResult<GLTran, CATran, Account, Ledger>.op_Implicit(pxResult);
      PXCache<GLTran>.StoreOriginal((PXGraph) this, tran);
      PXCache<CATran>.StoreOriginal((PXGraph) this, caTran);
      if (!this._IsIntegrityCheck)
      {
        if (PXResultset<OrganizationLedgerLink>.op_Implicit(PXSelectBase<OrganizationLedgerLink, PXSelectReadonly2<OrganizationLedgerLink, InnerJoin<Branch, On<OrganizationLedgerLink.organizationID, Equal<Branch.organizationID>>>, Where<Branch.branchID, Equal<Required<Branch.branchID>>, And<OrganizationLedgerLink.ledgerID, Equal<Required<OrganizationLedgerLink.ledgerID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) tran.BranchID,
          (object) tran.LedgerID
        })) == null)
        {
          Branch branchById = BranchMaint.FindBranchByID((PXGraph) this, tran.BranchID);
          throw new PXException("The {0} transaction cannot be posted because the {1} branch and {2} ledger are not associated with one another on the Ledgers (GL201500) form.", new object[3]
          {
            (object) tran.GetKeyImage(),
            (object) branchById.BranchCD.Trim(),
            (object) ledger.LedgerCD.Trim()
          });
        }
      }
      ((PXSelectBase) this.Account_AccountID).Cache.SetStatus((object) acct1, (PXEntryStatus) 5);
      int? nullable = acct1.AccountID;
      nullable = nullable.HasValue ? ledger.LedgerID : throw new PXException("One or more GL Accounts cannot be found.");
      if (!nullable.HasValue)
        throw new PXException("One or more Ledgers cannot be found.");
      Account acct2 = this.retainedEarningsAccount.Value;
      Account acct3 = this.netIncomeAccount.Value;
      if ((acct1.Type == "I" || acct1.Type == "E") && ledger.BalanceType != "S")
      {
        if (object.Equals((object) tran.AccountID, (object) ((PXSelectBase<GLSetup>) this.glsetup).Current.YtdNetIncAccountID))
          throw new PXException("Cannot post transactions directly to Year to Date Net Income.");
        GLTran copy = PXCache<GLTran>.CreateCopy(tran);
        copy.CuryDebitAmt = new Decimal?(0M);
        copy.CuryCreditAmt = new Decimal?(0M);
        copy.DebitAmt = new Decimal?(0M);
        copy.CreditAmt = new Decimal?(0M);
        this.UpdateHistory(tran, acct1, tran.FinPeriodID, PX.Objects.GL.PostGraph.HistoryUpdateAmountType.FinAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
        this.UpdateHistory(tran, acct1, tran.TranPeriodID, PX.Objects.GL.PostGraph.HistoryUpdateAmountType.TranAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
        if (ledger.BalanceType == "A" || ledger.BalanceType == "R")
        {
          this.UpdateHistory(copy, acct2, PX.Objects.GL.FinPeriods.FinPeriodUtils.GetFirstFinPeriodIDOfYear(tran.FinPeriodID), PX.Objects.GL.PostGraph.HistoryUpdateAmountType.FinAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
          this.UpdateHistory(copy, acct2, PX.Objects.GL.FinPeriods.FinPeriodUtils.GetFirstFinPeriodIDOfYear(tran.TranPeriodID), PX.Objects.GL.PostGraph.HistoryUpdateAmountType.TranAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
          string finPeriodIdOfYear1 = PX.Objects.GL.FinPeriods.FinPeriodUtils.GetFirstFinPeriodIDOfYear(PX.Objects.GL.FinPeriods.FinPeriodUtils.GetNextYearID(tran.FinPeriodID));
          this.UpdateHistory(tran, acct2, finPeriodIdOfYear1, PX.Objects.GL.PostGraph.HistoryUpdateAmountType.FinAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.NextYearRetainedEarnings);
          string finPeriodIdOfYear2 = PX.Objects.GL.FinPeriods.FinPeriodUtils.GetFirstFinPeriodIDOfYear(PX.Objects.GL.FinPeriods.FinPeriodUtils.GetNextYearID(tran.TranPeriodID));
          this.UpdateHistory(tran, acct2, finPeriodIdOfYear2, PX.Objects.GL.PostGraph.HistoryUpdateAmountType.TranAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.NextYearRetainedEarnings);
          this.UpdateHistory(tran, acct3, tran.FinPeriodID, PX.Objects.GL.PostGraph.HistoryUpdateAmountType.FinAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
          this.UpdateHistory(tran, acct3, tran.TranPeriodID, PX.Objects.GL.PostGraph.HistoryUpdateAmountType.TranAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
          this.UpdateHistory(copy, acct3, finPeriodIdOfYear1, PX.Objects.GL.PostGraph.HistoryUpdateAmountType.FinAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
          this.UpdateHistory(copy, acct3, finPeriodIdOfYear2, PX.Objects.GL.PostGraph.HistoryUpdateAmountType.TranAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
        }
      }
      else
      {
        this.UpdateHistory(tran, acct1, tran.FinPeriodID, PX.Objects.GL.PostGraph.HistoryUpdateAmountType.FinAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
        this.UpdateHistory(tran, acct1, tran.TranPeriodID, PX.Objects.GL.PostGraph.HistoryUpdateAmountType.TranAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
        if (ledger.BalanceType == "A" || ledger.BalanceType == "R")
        {
          nullable = acct1.AccountID;
          int? accountId = acct2.AccountID;
          if (nullable.GetValueOrDefault() == accountId.GetValueOrDefault() & nullable.HasValue == accountId.HasValue)
          {
            GLTran copy = PXCache<GLTran>.CreateCopy(tran);
            copy.CuryDebitAmt = new Decimal?(0M);
            copy.CuryCreditAmt = new Decimal?(0M);
            copy.DebitAmt = new Decimal?(0M);
            copy.CreditAmt = new Decimal?(0M);
            this.UpdateHistory(copy, acct3, PX.Objects.GL.FinPeriods.FinPeriodUtils.GetFirstFinPeriodIDOfYear(tran.FinPeriodID), PX.Objects.GL.PostGraph.HistoryUpdateAmountType.FinAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
            this.UpdateHistory(copy, acct2, PX.Objects.GL.FinPeriods.FinPeriodUtils.GetFirstFinPeriodIDOfYear(tran.FinPeriodID), PX.Objects.GL.PostGraph.HistoryUpdateAmountType.FinAmounts, PX.Objects.GL.PostGraph.HistoryUpdateMode.Common);
          }
        }
      }
      if (!this._IsIntegrityCheck)
      {
        tran.Posted = new bool?(true);
        ((PXSelectBase) this.GLTran_Module_BatNbr).Cache.SetStatus((object) tran, (PXEntryStatus) 1);
        if (caTran.TranID.HasValue)
        {
          CATran copy = PXCache<CATran>.CreateCopy(caTran);
          copy.Released = new bool?(true);
          copy.Posted = new bool?(true);
          copy.BatchNbr = tran.BatchNbr;
          ((PXSelectBase<CATran>) this.catran).Update(copy);
        }
      }
    }
  }

  public static bool GetAccountMapping(
    PXGraph graph,
    Batch batch,
    GLTran tran,
    out BranchAcctMapFrom mapfrom,
    out BranchAcctMapTo mapto)
  {
    mapfrom = (BranchAcctMapFrom) null;
    mapto = (BranchAcctMapTo) null;
    PXCache cach = graph.Caches[typeof (Batch)];
    int? branchId1 = batch.BranchID;
    int? branchId2 = tran.BranchID;
    if (branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue || !tran.BranchID.HasValue || !tran.AccountID.HasValue)
      return true;
    JournalEntry.CheckBatchBranchHasLedger(cach, batch);
    Ledger ledger = (Ledger) PXSelectorAttribute.Select<Batch.ledgerID>(cach, (object) batch, (object) batch.LedgerID);
    if (ledger == null)
      throw new PXException("One or more Ledgers cannot be found.");
    if (ledger.BalanceType != "A")
      return true;
    Branch branch1 = (Branch) PXSelectorAttribute.Select<GLTran.branchID>(graph.Caches[typeof (GLTran)], (object) tran, (object) tran.BranchID);
    if (branch1 == null)
      throw new PXException("One or more Branches cannot be found.");
    Branch branch2 = (Branch) PXSelectorAttribute.Select<Batch.branchID>(graph.Caches[typeof (Batch)], (object) batch, (object) batch.BranchID);
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID(graph, branch2.OrganizationID);
    int? organizationId1 = branch2.OrganizationID;
    int? organizationId2 = branch1.OrganizationID;
    if (organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue)
    {
      if (organizationById.OrganizationType == "Balancing")
      {
        int? branchId3 = tran.BranchID;
        int? branchId4 = batch.BranchID;
        if (!(branchId3.GetValueOrDefault() == branchId4.GetValueOrDefault() & branchId3.HasValue == branchId4.HasValue))
          goto label_12;
      }
      return true;
    }
label_12:
    if (!PXAccess.FeatureInstalled<FeaturesSet.interBranch>())
      return false;
    Account account1 = (Account) PXSelectorAttribute.Select<GLTran.accountID>(graph.Caches[typeof (GLTran)], (object) tran, (object) tran.AccountID);
    if (account1 == null)
      throw new PXException("One or more GL Accounts cannot be found.");
    mapfrom = PXResultset<BranchAcctMapFrom>.op_Implicit(PXSelectBase<BranchAcctMapFrom, PXSelectReadonly<BranchAcctMapFrom, Where<BranchAcctMapFrom.fromBranchID, Equal<Required<Batch.branchID>>, And<BranchAcctMapFrom.toBranchID, Equal<Required<GLTran.branchID>>, And<Required<Account.accountCD>, Between<BranchAcctMapFrom.fromAccountCD, BranchAcctMapFrom.toAccountCD>>>>>.Config>.Select(graph, new object[3]
    {
      (object) batch.BranchID,
      (object) tran.BranchID,
      (object) account1.AccountCD
    }));
    if (mapfrom == null)
    {
      mapfrom = PXResultset<BranchAcctMapFrom>.op_Implicit(PXSelectBase<BranchAcctMapFrom, PXSelectReadonly<BranchAcctMapFrom, Where<BranchAcctMapFrom.fromBranchID, Equal<Required<Batch.branchID>>, And<BranchAcctMapFrom.toBranchID, IsNull, And<Required<Account.accountCD>, Between<BranchAcctMapFrom.fromAccountCD, BranchAcctMapFrom.toAccountCD>>>>>.Config>.Select(graph, new object[2]
      {
        (object) batch.BranchID,
        (object) account1.AccountCD
      }));
      if (mapfrom == null || !mapfrom.MapSubID.HasValue)
        return false;
    }
    Account account2 = Account.PK.Find(graph, mapfrom.MapAccountID);
    if (!account2.Active.GetValueOrDefault())
      throw new PXException("Inter-branch balancing entries cannot be created because the {0} account is inactive.", new object[1]
      {
        (object) account2.AccountCD
      });
    mapto = PXResultset<BranchAcctMapTo>.op_Implicit(PXSelectBase<BranchAcctMapTo, PXSelectReadonly<BranchAcctMapTo, Where<BranchAcctMapTo.toBranchID, Equal<Required<Batch.branchID>>, And<BranchAcctMapTo.fromBranchID, Equal<Required<GLTran.branchID>>, And<Required<Account.accountCD>, Between<BranchAcctMapTo.fromAccountCD, BranchAcctMapTo.toAccountCD>>>>>.Config>.Select(graph, new object[3]
    {
      (object) batch.BranchID,
      (object) tran.BranchID,
      (object) account1.AccountCD
    }));
    if (mapto == null)
    {
      mapto = PXResultset<BranchAcctMapTo>.op_Implicit(PXSelectBase<BranchAcctMapTo, PXSelectReadonly<BranchAcctMapTo, Where<BranchAcctMapTo.toBranchID, Equal<Required<Batch.branchID>>, And<BranchAcctMapTo.fromBranchID, IsNull, And<Required<Account.accountCD>, Between<BranchAcctMapTo.fromAccountCD, BranchAcctMapTo.toAccountCD>>>>>.Config>.Select(graph, new object[2]
      {
        (object) batch.BranchID,
        (object) account1.AccountCD
      }));
      if (mapto == null || !mapto.MapSubID.HasValue)
        return false;
    }
    Account account3 = Account.PK.Find(graph, mapto.MapAccountID);
    if (!account3.Active.GetValueOrDefault())
      throw new PXException("Inter-branch balancing entries cannot be created because the {0} account is inactive.", new object[1]
      {
        (object) account3.AccountCD
      });
    return true;
  }

  protected virtual Batch CreateInterCompany(Batch b)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PX.Objects.GL.PostGraph.\u003C\u003Ec__DisplayClass74_0 cDisplayClass740 = new PX.Objects.GL.PostGraph.\u003C\u003Ec__DisplayClass74_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass740.b = b;
    // ISSUE: reference to a compiler-generated field
    if (GeneralLedgerMaint.FindLedgerByID((PXGraph) this, cDisplayClass740.b.LedgerID).BalanceType != "A")
      return (Batch) null;
    // ISSUE: reference to a compiler-generated field
    ((PXGraph) this).Caches[typeof (Batch)].Current = (object) cDisplayClass740.b;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass740.glTranInterCompany = new Dictionary<GLTran, GLTran>((IEqualityComparer<GLTran>) new GLTranInterCompanyComparer());
    // ISSUE: method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) cDisplayClass740, __methodptr(\u003CCreateInterCompany\u003Eb__0));
    // ISSUE: method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) cDisplayClass740, __methodptr(\u003CCreateInterCompany\u003Eb__1));
    // ISSUE: method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) cDisplayClass740, __methodptr(\u003CCreateInterCompany\u003Eb__2));
    // ISSUE: method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) cDisplayClass740, __methodptr(\u003CCreateInterCompany\u003Eb__3));
    bool flag;
    try
    {
      ((PXGraph) this).RowInserting.AddHandler<GLTran>(pxRowInserting);
      ((PXGraph) this).RowInserted.AddHandler<GLTran>(pxRowInserted);
      ((PXGraph) this).RowUpdated.AddHandler<GLTran>(pxRowUpdated);
      ((PXGraph) this).RowDeleted.AddHandler<GLTran>(pxRowDeleted);
      // ISSUE: reference to a compiler-generated field
      Branch branchById = BranchMaint.FindBranchByID((PXGraph) this, cDisplayClass740.b.BranchID);
      PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, branchById.OrganizationID);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      IEnumerable<PXResult<GLTran, Branch, Account>> pxResults = ((IEnumerable<PXResult<GLTran>>) PXSelectBase<GLTran, PXSelectJoin<GLTran, LeftJoin<Branch, On<GLTran.branchID, Equal<Branch.branchID>>, LeftJoin<Account, On<Account.accountID, Equal<GLTran.accountID>>, LeftJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<GLTran.curyInfoID>>>>>, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<Where2<Where<Branch.organizationID, NotEqual<Required<Branch.organizationID>>, Or<GLTran.branchID, NotEqual<Required<Batch.branchID>>, And<Required<PX.Objects.GL.DAC.Organization.organizationType>, Equal<OrganizationTypes.withBranchesBalancing>>>>, Or<Account.accountID, IsNull, Or<Branch.branchID, IsNull>>>>>>, OrderBy<Asc<GLTran.module, Asc<GLTran.batchNbr, Asc<GLTran.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[5]
      {
        (object) cDisplayClass740.b.Module,
        (object) cDisplayClass740.b.BatchNbr,
        (object) branchById.OrganizationID,
        (object) cDisplayClass740.b.BranchID,
        (object) organizationById.OrganizationType
      })).AsEnumerable<PXResult<GLTran>>().Cast<PXResult<GLTran, Branch, Account>>();
      flag = false;
      foreach (PXResult<GLTran, Branch, Account, PX.Objects.CM.CurrencyInfo> pxResult in pxResults)
      {
        GLTran tran = PXResult<GLTran, Branch, Account, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
        Account account = PXResult<GLTran, Branch, Account, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
        Branch branch = PXResult<GLTran, Branch, Account, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
        ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.CurrencyInfo_CuryInfoID).StoreResult((IBqlTable) PXResult<GLTran, Branch, Account, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult));
        int? nullable1 = account.AccountID;
        nullable1 = nullable1.HasValue ? branch.BranchID : throw new PXException("One or more GL Accounts cannot be found.");
        if (!nullable1.HasValue)
          throw new PXException("One or more Branches cannot be found.");
        PXSelectorAttribute.StoreCached<GLTran.accountID>(((PXGraph) this).Caches[typeof (GLTran)], (object) tran, (object) account);
        PXSelectorAttribute.StoreCached<GLTran.branchID>(((PXGraph) this).Caches[typeof (GLTran)], (object) tran, (object) branch);
        BranchAcctMapFrom mapfrom;
        BranchAcctMapTo mapto;
        // ISSUE: reference to a compiler-generated field
        if (!PX.Objects.GL.PostGraph.GetAccountMapping((PXGraph) this, cDisplayClass740.b, tran, out mapfrom, out mapto))
        {
          // ISSUE: reference to a compiler-generated field
          throw new PXException("Account mapping is missing for the {0} and {1} branches. Specify account mapping on the Inter-Branch Account Mapping (GL101010) form.", new object[2]
          {
            (object) (PXResultset<Branch>.op_Implicit(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Optional<Batch.branchID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
            {
              (object) cDisplayClass740.b
            }, Array.Empty<object>()))?.BranchCD?.Trim() ?? "Undefined"),
            (object) (branch?.BranchCD?.Trim() ?? "Undefined")
          });
        }
        GLTran copy1 = PXCache<GLTran>.CreateCopy(tran);
        copy1.AccountID = mapfrom.MapAccountID;
        copy1.SubID = mapfrom.MapSubID;
        // ISSUE: reference to a compiler-generated field
        copy1.BranchID = cDisplayClass740.b.BranchID;
        // ISSUE: reference to a compiler-generated field
        copy1.LedgerID = cDisplayClass740.b.LedgerID;
        copy1.FinPeriodID = (string) null;
        GLTran glTran1 = copy1;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        glTran1.TranLineNbr = nullable2;
        GLTran glTran2 = copy1;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        glTran2.LineNbr = nullable3;
        GLTran glTran3 = copy1;
        nullable1 = new int?();
        int? nullable4 = nullable1;
        glTran3.TranID = nullable4;
        copy1.CATranID = new long?();
        GLTran glTran4 = copy1;
        nullable1 = new int?();
        int? nullable5 = nullable1;
        glTran4.ProjectID = nullable5;
        GLTran glTran5 = copy1;
        nullable1 = new int?();
        int? nullable6 = nullable1;
        glTran5.TaskID = nullable6;
        GLTran glTran6 = copy1;
        nullable1 = new int?();
        int? nullable7 = nullable1;
        glTran6.CostCodeID = nullable7;
        copy1.IsInterCompany = new bool?(true);
        // ISSUE: reference to a compiler-generated field
        copy1.TranDesc = PXMessages.LocalizeFormatNoPrefix("Balancing entry for: {0}", new object[1]
        {
          ((PXGraph) this).Caches[typeof (Batch)].GetValueExt<Batch.branchID>((object) cDisplayClass740.b)
        });
        copy1.TaxID = (string) null;
        copy1.NoteID = new Guid?();
        copy1.PMTranID = new long?();
        copy1.OrigPMTranID = new long?();
        this.ClearReclassificationFields(copy1);
        ((PXGraph) this).Caches[typeof (GLTran)].Insert((object) copy1);
        GLTran copy2 = PXCache<GLTran>.CreateCopy(tran);
        copy2.AccountID = mapto.MapAccountID;
        copy2.SubID = mapto.MapSubID;
        copy2.LedgerID = branch.LedgerID;
        copy2.FinPeriodID = (string) null;
        GLTran glTran7 = copy2;
        nullable1 = new int?();
        int? nullable8 = nullable1;
        glTran7.TranLineNbr = nullable8;
        GLTran glTran8 = copy2;
        nullable1 = new int?();
        int? nullable9 = nullable1;
        glTran8.LineNbr = nullable9;
        GLTran glTran9 = copy2;
        nullable1 = new int?();
        int? nullable10 = nullable1;
        glTran9.TranID = nullable10;
        copy2.CATranID = new long?();
        GLTran glTran10 = copy2;
        nullable1 = new int?();
        int? nullable11 = nullable1;
        glTran10.ProjectID = nullable11;
        GLTran glTran11 = copy2;
        nullable1 = new int?();
        int? nullable12 = nullable1;
        glTran11.TaskID = nullable12;
        GLTran glTran12 = copy2;
        nullable1 = new int?();
        int? nullable13 = nullable1;
        glTran12.CostCodeID = nullable13;
        copy2.IsInterCompany = new bool?(true);
        copy2.TranDesc = PXMessages.LocalizeFormatNoPrefix("Balancing entry for: {0}", new object[1]
        {
          (object) branch.BranchCD
        });
        copy2.TaxID = (string) null;
        copy2.NoteID = new Guid?();
        copy2.PMTranID = new long?();
        copy2.OrigPMTranID = new long?();
        Decimal? curyCreditAmt = copy2.CuryCreditAmt;
        copy2.CuryCreditAmt = copy2.CuryDebitAmt;
        copy2.CuryDebitAmt = curyCreditAmt;
        Decimal? creditAmt = copy2.CreditAmt;
        copy2.CreditAmt = copy2.DebitAmt;
        copy2.DebitAmt = creditAmt;
        this.ClearReclassificationFields(copy2);
        ((PXGraph) this).Caches[typeof (GLTran)].Insert((object) copy2);
        GLTran copy3 = PXCache<GLTran>.CreateCopy(tran);
        copy3.LedgerID = branch.LedgerID;
        ((PXGraph) this).Caches[typeof (GLTran)].Update((object) copy3);
        flag = true;
      }
    }
    finally
    {
      ((PXGraph) this).RowInserting.RemoveHandler<GLTran>(pxRowInserting);
      ((PXGraph) this).RowInserted.RemoveHandler<GLTran>(pxRowInserted);
      ((PXGraph) this).RowUpdated.RemoveHandler<GLTran>(pxRowUpdated);
      ((PXGraph) this).RowDeleted.RemoveHandler<GLTran>(pxRowDeleted);
    }
    // ISSUE: reference to a compiler-generated field
    cDisplayClass740.glTranInterCompany.Clear();
    ((PXGraph) this).Caches[typeof (GLTran)].Persist((PXDBOperation) 2);
    ((PXGraph) this).Caches[typeof (GLTran)].Persist((PXDBOperation) 1);
    ((PXGraph) this).SelectTimeStamp();
    ((PXGraph) this).Caches[typeof (GLTran)].Persisted(false);
    // ISSUE: reference to a compiler-generated field
    Decimal? nullable14 = cDisplayClass740.b.DebitTotal;
    // ISSUE: reference to a compiler-generated field
    Decimal? creditTotal = cDisplayClass740.b.CreditTotal;
    if (!(nullable14.GetValueOrDefault() == creditTotal.GetValueOrDefault() & nullable14.HasValue == creditTotal.HasValue))
      throw new PXException("The batch is not balanced. Review the debit and credit amounts.");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    List<GLTran> list = GraphHelper.RowCast<GLTran>((IEnumerable) PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cDisplayClass740.b.Module,
      (object) cDisplayClass740.b.BatchNbr
    })).ToList<GLTran>();
    Decimal? nullable15 = list.Sum<GLTran>((Func<GLTran, Decimal?>) (_ => _.DebitAmt));
    Decimal? nullable16 = list.Sum<GLTran>((Func<GLTran, Decimal?>) (_ => _.CreditAmt));
    Decimal? nullable17 = nullable15;
    nullable14 = nullable16;
    if (!(nullable17.GetValueOrDefault() == nullable14.GetValueOrDefault() & nullable17.HasValue == nullable14.HasValue))
      throw new PXException("The batch is not balanced. Review the debit and credit amounts.");
    // ISSUE: reference to a compiler-generated field
    return !flag ? (Batch) null : cDisplayClass740.b;
  }

  protected virtual void ClearReclassificationFields(GLTran tran)
  {
    tran.ReclassSourceTranModule = (string) null;
    tran.ReclassSourceTranBatchNbr = (string) null;
    tran.ReclassSourceTranLineNbr = new int?();
    tran.ReclassBatchNbr = (string) null;
    tran.ReclassBatchModule = (string) null;
    tran.ReclassType = (string) null;
    tran.CuryReclassRemainingAmt = new Decimal?();
    tran.ReclassRemainingAmt = new Decimal?();
    tran.Reclassified = new bool?(false);
    tran.ReclassSeqNbr = new int?();
    tran.IsReclassReverse = new bool?(false);
    tran.ReclassificationProhibited = new bool?(false);
    tran.ReclassOrigTranDate = new DateTime?();
    tran.ReclassTotalCount = new int?();
    tran.ReclassReleasedCount = new int?();
  }

  public virtual void PostBatchProc(Batch b)
  {
    if (RunningFlagScope<GLHistoryValidate>.IsRunning)
    {
      if (!b.RequirePost.GetValueOrDefault())
      {
        b.RequirePost = new bool?(true);
        b.PostErrorCount = new int?(1);
        ((PXSelectBase<Batch>) this.BatchModule).Update(b);
        ((PXSelectBase) this.BatchModule).Cache.Persist((PXDBOperation) 1);
      }
      throw new PXSetPropertyException("The batch has been released but not posted because GL history validation is in progress. It will be posted automatically after validation is completed.", (PXErrorLevel) 2);
    }
    using (new RunningFlagScope<PX.Objects.GL.PostGraph>())
      this.PostBatchProc(b, true);
  }

  public virtual void PostBatchProc(Batch b, bool createintercompany)
  {
    if (!(b.Status != "U"))
    {
      bool? nullable1 = b.Released;
      bool flag1 = false;
      if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
      {
        this.ValidateBatchFinPeriod(b);
        PXCache<Batch>.StoreOriginal((PXGraph) this, b);
        if (((PXSelectBase<GLSetup>) this.glsetup).Current.AutoRevOption == "P")
        {
          nullable1 = b.AutoReverse;
          if (nullable1.GetValueOrDefault())
          {
            nullable1 = b.Released;
            if (nullable1.GetValueOrDefault())
            {
              PX.Objects.GL.PostGraph postGraph = this.lazyPostGraph.Value;
              ((PXGraph) postGraph).Clear((PXClearOption) 3);
              if (PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelect<Batch, Where<Batch.origModule, Equal<Required<Batch.module>>, And<Batch.origBatchNbr, Equal<Required<Batch.batchNbr>>, And<Batch.autoReverseCopy, Equal<True>>>>>.Config>.SelectSingleBound((PXGraph) postGraph, (object[]) null, new object[2]
              {
                (object) b.Module,
                (object) b.BatchNbr
              })) == null)
              {
                ((PXGraph) postGraph).Clear();
                Batch b1 = (Batch) null;
                using (PXTransactionScope transactionScope = new PXTransactionScope())
                {
                  b1 = postGraph.ReverseBatchProc(b);
                  postGraph.ReleaseBatchProc(b1);
                  transactionScope.Complete();
                }
                if (((PXSelectBase<GLSetup>) this.glsetup).Current.AutoPostOption.GetValueOrDefault() && b1 != null)
                  postGraph.PostBatchProc(b1);
              }
            }
          }
        }
        Ledger ledgerById = GeneralLedgerMaint.FindLedgerByID((PXGraph) this, b.LedgerID);
        if (createintercompany && ledgerById.BalanceType == "A")
        {
          PX.Objects.GL.PostGraph postGraph = this.lazyPostGraph.Value;
          ((PXGraph) postGraph).Clear((PXClearOption) 3);
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            if (postGraph.CreateInterCompany(b) != null)
            {
              postGraph.PostBatchProc(b, false);
              transactionScope.Complete();
              return;
            }
            transactionScope.Complete();
          }
        }
        ((PXSelectBase) this.GLTran_CATran_Module_BatNbr).View.Clear();
        ICollection<PXResult<GLTran, CATran, Account, Ledger>> array = (ICollection<PXResult<GLTran, CATran, Account, Ledger>>) ((IEnumerable<PXResult<GLTran>>) ((PXSelectBase<GLTran>) this.GLTran_CATran_Module_BatNbr).Select(new object[2]
        {
          (object) b.Module,
          (object) b.BatchNbr
        })).AsEnumerable<PXResult<GLTran>>().Cast<PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>>().Select<PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>, PXResult<GLTran, CATran, Account, Ledger>>((Func<PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>, PXResult<GLTran, CATran, Account, Ledger>>) (result => new PXResult<GLTran, CATran, Account, Ledger>(PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(result), PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(result), PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(result), PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(result)))).ToArray<PXResult<GLTran, CATran, Account, Ledger>>();
        this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) array.Select<PXResult<GLTran, CATran, Account, Ledger>, GLTran>((Func<PXResult<GLTran, CATran, Account, Ledger>, GLTran>) (t => PXResult<GLTran, CATran, Account, Ledger>.op_Implicit(t))));
        this.UpdateHistoryProc((IEnumerable<PXResult<GLTran, CATran, Account, Ledger>>) array);
        this.UpdateAllocationBalance(b);
        bool flag2 = this.UpdateConsolidationBalance(b);
        b.Posted = new bool?(true);
        b.PostedToVerify = new bool?(false);
        Batch batch = b;
        nullable1 = new bool?();
        bool? nullable2 = nullable1;
        batch.ReleasedToVerify = nullable2;
        ((SelectedEntityEvent<Batch>) PXEntityEventBase<Batch>.Container<Batch.Events>.Select((Expression<Func<Batch.Events, PXEntityEvent<Batch.Events>>>) (ev => ev.PostBatch))).FireOn((PXGraph) this, b);
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          ((PXSelectBase) this.BatchModule).Cache.Persist((PXDBOperation) 1);
          ((PXSelectBase) this.GLTran_Module_BatNbr).Cache.Persist((PXDBOperation) 1);
          ((PXSelectBase) this.catran).Cache.Persist((PXDBOperation) 1);
          ((PXGraph) this).Caches[typeof (AcctHist)].Persist((PXDBOperation) 2);
          ((PXGraph) this).Caches[typeof (PMHistoryAccum)].Persist((PXDBOperation) 2);
          ((PXGraph) this).Caches[typeof (CADailySummary)].Persist((PXDBOperation) 2);
          if (flag2)
            ((PXGraph) this).Caches[typeof (ConsolHist)].Persist((PXDBOperation) 2);
          this.ExtensionsPersist();
          transactionScope.Complete((PXGraph) this);
        }
        ((PXSelectBase) this.BatchModule).Cache.Persisted(false);
        ((PXSelectBase) this.GLTran_Module_BatNbr).Cache.Persisted(false);
        ((PXSelectBase) this.catran).Cache.Persisted(false);
        ((PXGraph) this).Caches[typeof (AcctHist)].Persisted(false);
        ((PXGraph) this).Caches[typeof (PMHistoryAccum)].Persisted(false);
        ((PXGraph) this).Caches[typeof (CADailySummary)].Persisted(false);
        if (flag2)
          ((PXGraph) this).Caches[typeof (ConsolHist)].Persisted(false);
        this.ExtensionsPersisted();
        ((PXSelectBase) this.BatchModule).Cache.RestoreCopy((object) b, (object) ((PXSelectBase<Batch>) this.BatchModule).Current);
        return;
      }
    }
    throw new PXException("Batch Status invalid for processing.");
  }

  public virtual void ExtensionsPersist()
  {
  }

  public virtual void ExtensionsPersisted()
  {
  }

  private void ValidateBatchFinPeriod(Batch batch)
  {
    PXCache cach = ((PXGraph) this).Caches[typeof (Batch)];
    foreach (PXEventSubscriberAttribute attribute in cach.GetAttributes<Batch.finPeriodID>())
    {
      if (attribute is OpenPeriodAttribute)
        ((OpenPeriodAttribute) attribute).IsValidPeriod(cach, (object) batch, (object) batch.FinPeriodID);
    }
  }

  public virtual void ReleaseBatchProc(Batch b, bool unholdBatch = false)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (unholdBatch)
      {
        b.Hold = new bool?(false);
        b.Approved = new bool?(true);
        PXEntityEventBase<Batch>.Container<Batch.Events>.FireOnPropertyChanged<Batch.hold>((PXGraph) this, (Batch.Events) b);
      }
      bool? nullable1;
      if (string.IsNullOrEmpty(b.OrigBatchNbr))
      {
        if (!(b.Status != "B"))
        {
          nullable1 = b.Released;
          if (!nullable1.Value)
            goto label_7;
        }
        throw new PXException("Batch Status invalid for processing.");
      }
label_7:
      this.ValidateBatchFinPeriod(b);
      PXResultset<GLTran> pxResultset = ((PXSelectBase<GLTran>) this.GLTran_CATran_Module_BatNbr).Select(new object[2]
      {
        (object) b.Module,
        (object) b.BatchNbr
      });
      this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) GraphHelper.RowCast<GLTran>((IEnumerable) pxResultset));
      b.CreditTotal = new Decimal?(0.0M);
      b.DebitTotal = new Decimal?(0.0M);
      b.CuryCreditTotal = new Decimal?(0.0M);
      b.CuryDebitTotal = new Decimal?(0.0M);
      PX.Objects.CM.CurrencyInfo info = (PX.Objects.CM.CurrencyInfo) null;
      Ledger ledger = (Ledger) null;
      bool isCurrencyTB = false;
      int num1;
      if (PXAccess.FeatureInstalled<FeaturesSet.taxEntryFromGL>())
      {
        nullable1 = b.CreateTaxTrans;
        num1 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 0;
      bool flag1 = num1 != 0;
      PX.Objects.GL.PostGraph.TaxTranCreationProcessor creationProcessor = (PX.Objects.GL.PostGraph.TaxTranCreationProcessor) null;
      if (flag1)
      {
        PXCache cache1 = ((PXSelectBase) this.GLTran_CATran_Module_BatNbr).Cache;
        PXCache cache2 = ((PXSelectBase) this.GL_GLTran_Taxes).Cache;
        nullable1 = b.SkipTaxValidation;
        int num2 = nullable1.GetValueOrDefault() ? 1 : 0;
        creationProcessor = new PX.Objects.GL.PostGraph.TaxTranCreationProcessor(cache1, cache2, num2 != 0);
      }
      bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.interBranch>();
      foreach (PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger> aTran in pxResultset)
      {
        GLTran glTran1 = PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(aTran);
        CATran caTran = PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(aTran);
        Account account = PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(aTran);
        JournalEntry.AssertBatchAndDetailHaveSameMasterPeriod((PXSelectBase<GLTran>) this.GLTran_Module_BatNbr, b, glTran1);
        ledger = PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(aTran);
        info = PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(aTran);
        int? nullable2 = account.AccountID;
        nullable2 = nullable2.HasValue ? ledger.LedgerID : throw new PXException("One or more GL Accounts cannot be found.");
        if (!nullable2.HasValue)
          throw new PXException("One or more Ledgers cannot be found.");
        Branch branch1 = (Branch) PXSelectorAttribute.Select<Batch.branchID>(((PXSelectBase) this.BatchModule).Cache, (object) b, (object) b.BranchID);
        Branch branch2 = (Branch) PXSelectorAttribute.Select<GLTran.branchID>(((PXSelectBase) this.GLTran_Module_BatNbr).Cache, (object) glTran1, (object) glTran1.BranchID);
        nullable2 = branch2.OrganizationID;
        int? organizationId = branch1.OrganizationID;
        if (!(nullable2.GetValueOrDefault() == organizationId.GetValueOrDefault() & nullable2.HasValue == organizationId.HasValue) && !flag2 && (ledger.BalanceType == "R" || ledger.BalanceType == "S"))
          throw new PXException("The document cannot be released because the Inter-Branch Transactions feature is disabled on the Enable/Disable Features (CS100000) form.");
        PXSelectorAttribute.StoreCached<GLTran.accountID>(((PXGraph) this).Caches[typeof (GLTran)], (object) glTran1, (object) account);
        Decimal? nullable3 = PX.Objects.GL.PostGraph.GetAccountMapping((PXGraph) this, b, glTran1, out BranchAcctMapFrom _, out BranchAcctMapTo _) ? glTran1.CuryDebitAmt : throw new PXException("Account mapping is missing for the {0} and {1} branches. Specify account mapping on the Inter-Branch Account Mapping (GL101010) form.", new object[2]
        {
          (object) (branch1?.BranchCD?.Trim() ?? "Undefined"),
          (object) (branch2?.BranchCD?.Trim() ?? "Undefined")
        });
        Decimal num3 = 0M;
        if (!(nullable3.GetValueOrDefault() == num3 & nullable3.HasValue))
        {
          nullable3 = glTran1.CuryCreditAmt;
          Decimal num4 = 0M;
          if (!(nullable3.GetValueOrDefault() == num4 & nullable3.HasValue))
            goto label_26;
        }
        nullable3 = glTran1.DebitAmt;
        Decimal num5 = 0M;
        if (!(nullable3.GetValueOrDefault() == num5 & nullable3.HasValue))
        {
          nullable3 = glTran1.CreditAmt;
          Decimal num6 = 0M;
          if (!(nullable3.GetValueOrDefault() == num6 & nullable3.HasValue))
            goto label_26;
        }
        if (flag1)
          creationProcessor.AddToDocuments(aTran);
        nullable1 = b.AutoReverseCopy;
        Decimal? nullable4;
        if (nullable1.GetValueOrDefault() && this.AutoRevEntry)
        {
          nullable3 = glTran1.CuryDebitAmt;
          Decimal num7 = 0M;
          if (!(nullable3.GetValueOrDefault() == num7 & nullable3.HasValue))
          {
            nullable3 = glTran1.CuryCreditAmt;
            Decimal num8 = 0M;
            if (nullable3.GetValueOrDefault() == num8 & nullable3.HasValue)
              goto label_34;
          }
          nullable3 = glTran1.DebitAmt;
          Decimal num9 = 0M;
          if (!(nullable3.GetValueOrDefault() == num9 & nullable3.HasValue))
          {
            nullable3 = glTran1.CreditAmt;
            Decimal num10 = 0M;
            if (nullable3.GetValueOrDefault() == num10 & nullable3.HasValue)
              goto label_34;
          }
          nullable3 = glTran1.CuryCreditAmt;
          Decimal num11 = 0M;
          if (!(nullable3.GetValueOrDefault() == num11 & nullable3.HasValue))
          {
            nullable3 = glTran1.CuryDebitAmt;
            Decimal num12 = 0M;
            if (nullable3.GetValueOrDefault() == num12 & nullable3.HasValue)
              goto label_45;
          }
          nullable3 = glTran1.CreditAmt;
          Decimal num13 = 0M;
          if (!(nullable3.GetValueOrDefault() == num13 & nullable3.HasValue))
          {
            nullable3 = glTran1.DebitAmt;
            Decimal num14 = 0M;
            if (!(nullable3.GetValueOrDefault() == num14 & nullable3.HasValue))
              goto label_52;
          }
          else
            goto label_52;
label_45:
          GLTran glTran2 = glTran1;
          Decimal num15 = -1M;
          nullable3 = glTran1.CuryCreditAmt;
          Decimal? nullable5;
          if (!nullable3.HasValue)
          {
            nullable4 = new Decimal?();
            nullable5 = nullable4;
          }
          else
            nullable5 = new Decimal?(num15 * nullable3.GetValueOrDefault());
          glTran2.CuryDebitAmt = nullable5;
          GLTran glTran3 = glTran1;
          Decimal num16 = -1M;
          nullable3 = glTran1.CreditAmt;
          Decimal? nullable6;
          if (!nullable3.HasValue)
          {
            nullable4 = new Decimal?();
            nullable6 = nullable4;
          }
          else
            nullable6 = new Decimal?(num16 * nullable3.GetValueOrDefault());
          glTran3.DebitAmt = nullable6;
          glTran1.CuryCreditAmt = new Decimal?(0M);
          glTran1.CreditAmt = new Decimal?(0M);
          goto label_52;
label_34:
          GLTran glTran4 = glTran1;
          Decimal num17 = -1M;
          nullable3 = glTran1.CuryDebitAmt;
          Decimal? nullable7;
          if (!nullable3.HasValue)
          {
            nullable4 = new Decimal?();
            nullable7 = nullable4;
          }
          else
            nullable7 = new Decimal?(num17 * nullable3.GetValueOrDefault());
          glTran4.CuryCreditAmt = nullable7;
          GLTran glTran5 = glTran1;
          Decimal num18 = -1M;
          nullable3 = glTran1.DebitAmt;
          Decimal? nullable8;
          if (!nullable3.HasValue)
          {
            nullable4 = new Decimal?();
            nullable8 = nullable4;
          }
          else
            nullable8 = new Decimal?(num18 * nullable3.GetValueOrDefault());
          glTran5.CreditAmt = nullable8;
          glTran1.CuryDebitAmt = new Decimal?(0M);
          glTran1.DebitAmt = new Decimal?(0M);
        }
label_52:
        Batch batch1 = b;
        nullable3 = batch1.CreditTotal;
        nullable4 = glTran1.CreditAmt;
        batch1.CreditTotal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        Batch batch2 = b;
        nullable4 = batch2.DebitTotal;
        nullable3 = glTran1.DebitAmt;
        batch2.DebitTotal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        Batch batch3 = b;
        nullable3 = batch3.CuryCreditTotal;
        nullable4 = glTran1.CuryCreditAmt;
        batch3.CuryCreditTotal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        Batch batch4 = b;
        nullable4 = batch4.CuryDebitTotal;
        nullable3 = glTran1.CuryDebitAmt;
        batch4.CuryDebitTotal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        if (ledger.BalanceType == "S")
        {
          Batch batch5 = b;
          nullable3 = b.CuryDebitTotal;
          Decimal num19 = Math.Abs(nullable3.Value);
          nullable3 = b.CuryCreditTotal;
          Decimal num20 = Math.Abs(nullable3.Value);
          Decimal? nullable9 = num19 > num20 ? b.CuryDebitTotal : b.CuryCreditTotal;
          batch5.CuryControlTotal = nullable9;
          Batch batch6 = b;
          nullable3 = b.DebitTotal;
          Decimal num21 = Math.Abs(nullable3.Value);
          nullable3 = b.CreditTotal;
          Decimal num22 = Math.Abs(nullable3.Value);
          Decimal? nullable10 = num21 > num22 ? b.DebitTotal : b.CreditTotal;
          batch6.ControlTotal = nullable10;
        }
        else
        {
          b.CuryControlTotal = b.CuryDebitTotal;
          b.ControlTotal = b.DebitTotal;
        }
        glTran1.CuryReclassRemainingAmt = new Decimal?(0M);
        glTran1.Released = new bool?(true);
        ((PXSelectBase) this.GLTran_Module_BatNbr).Cache.Update((object) glTran1);
        if (caTran.TranID.HasValue)
        {
          CATran copy = PXCache<CATran>.CreateCopy(caTran);
          copy.Released = new bool?(true);
          copy.BatchNbr = glTran1.BatchNbr;
          copy.Hold = b.Hold;
          ((PXSelectBase<CATran>) this.catran).Update(copy);
        }
        nullable1 = glTran1.IsReclassReverse;
        if (nullable1.GetValueOrDefault())
        {
          nullable3 = glTran1.CuryDebitAmt;
          nullable4 = glTran1.CuryCreditAmt;
          Decimal? curyDrCrAmt = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          nullable4 = glTran1.DebitAmt;
          nullable3 = glTran1.CreditAmt;
          Decimal? drCrAmt = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          if (this.UpdateSourceReclassificationTran(glTran1, curyDrCrAmt, drCrAmt) == 0)
            throw new PXException("The original transaction has been already reclassified, or the reclassifying amount is greater than the original amount. See the original batch: {0}", new object[1]
            {
              (object) glTran1.OrigBatchNbr
            });
        }
        isCurrencyTB = ((isCurrencyTB ? 1 : 0) | (!(ledger.BalanceType == "A") || !(b.BatchType == "T") ? 0 : (info.CuryID != ledger.BaseCuryID ? 1 : 0))) != 0;
        continue;
label_26:
        throw new PXException("Both Debit and Credit parts of transaction are not zero. Exiting with error.");
      }
      GLTran glTran = this.InsertRoundingTran(b, info, ledger, isCurrencyTB);
      Batch batch = b;
      bool? nullable11 = b.Released;
      bool? nullable12;
      if (!nullable11.GetValueOrDefault())
      {
        nullable12 = new bool?(false);
      }
      else
      {
        nullable11 = new bool?();
        nullable12 = nullable11;
      }
      batch.ReleasedToVerify = nullable12;
      b.Released = new bool?(true);
      ((SelectedEntityEvent<Batch>) PXEntityEventBase<Batch>.Container<Batch.Events>.Select((Expression<Func<Batch.Events, PXEntityEvent<Batch.Events>>>) (ev => ev.ReleaseBatch))).FireOn((PXGraph) this, b);
      ((PXSelectBase) this.BatchModule).Cache.Current = (object) b;
      ((PXSelectBase) this.BatchModule).Cache.Persist((PXDBOperation) 1);
      this.CreateProjectTransactions(b);
      bool flag3 = false;
      if (flag1 && ledger.BalanceType == "A")
        flag3 = creationProcessor.CreateTaxTransactions();
      if (glTran != null)
        ((PXSelectBase) this.GLTran_Module_BatNbr).Cache.Persist((PXDBOperation) 2);
      ((PXSelectBase) this.GLTran_Module_BatNbr).Cache.Persist((PXDBOperation) 1);
      ((PXSelectBase) this.catran).Cache.Persist((PXDBOperation) 1);
      ((PXGraph) this).Caches[typeof (CADailySummary)].Persist((PXDBOperation) 2);
      if (flag3)
        ((PXSelectBase) this.GL_GLTran_Taxes).Cache.Persist((PXDBOperation) 2);
      EntityInUseHelper.MarkEntityAsInUse<CurrencyInUse>((object) b.CuryID);
      if (flag3)
        EntityInUseHelper.MarkEntityAsInUse<FeatureInUse>((object) FeaturesSet.taxEntryFromGL.EntityInUseKey);
      transactionScope.Complete((PXGraph) this);
    }
    ((PXSelectBase) this.BatchModule).Cache.Persisted(false);
    ((PXSelectBase) this.CurrencyInfo_ID).Cache.Persisted(false);
    ((PXSelectBase) this.GLTran_Module_BatNbr).Cache.Persisted(false);
    ((PXSelectBase) this.catran).Cache.Persisted(false);
    ((PXGraph) this).Caches[typeof (CADailySummary)].Persisted(false);
    ((PXSelectBase) this.GL_GLTran_Taxes).Cache.Persisted(false);
    ((PXSelectBase) this.BatchModule).Cache.RestoreCopy((object) b, (object) ((PXSelectBase<Batch>) this.BatchModule).Current);
  }

  public virtual int UpdateSourceReclassificationTran(
    GLTran tran,
    Decimal? curyDrCrAmt,
    Decimal? drCrAmt)
  {
    return PXUpdate<Set<GLTran.curyReclassRemainingAmt, Sub<Switch<Case<Where<GLTran.reclassified, Equal<False>>, Add<GLTran.curyDebitAmt, GLTran.curyCreditAmt>>, GLTran.curyReclassRemainingAmt>, Required<GLTran.curyDebitAmt>>, Set<GLTran.reclassRemainingAmt, Sub<Switch<Case<Where<GLTran.reclassified, Equal<False>>, Add<GLTran.debitAmt, GLTran.creditAmt>>, GLTran.reclassRemainingAmt>, Required<GLTran.debitAmt>>, Set<GLTran.reclassReleasedCount, Add<IsNull<GLTran.reclassReleasedCount, Zero>, int1>, Set<GLTran.reclassified, True>>>>, GLTran, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.lineNbr, Equal<Required<GLTran.lineNbr>>, And<Mult<IIf<Where<Add<GLTran.curyDebitAmt, GLTran.curyCreditAmt>, Greater<Zero>>, int1, Minus<int1>>, Sub<Switch<Case<Where<GLTran.reclassified, Equal<False>>, Add<GLTran.curyDebitAmt, GLTran.curyCreditAmt>>, GLTran.curyReclassRemainingAmt>, Required<GLTran.curyDebitAmt>>>, GreaterEqual<Zero>>>>>>.Update((PXGraph) this, new object[6]
    {
      (object) curyDrCrAmt,
      (object) drCrAmt,
      (object) tran.OrigModule,
      (object) tran.OrigBatchNbr,
      (object) tran.OrigLineNbr,
      (object) curyDrCrAmt
    });
  }

  protected virtual bool DoExceedsNegligibleDifference(Decimal? difference)
  {
    return Math.Abs(Math.Round(difference.Value, 4)) >= 0.00005M;
  }

  protected virtual GLTran InsertRoundingTran(
    Batch b,
    PX.Objects.CM.CurrencyInfo info,
    Ledger ledger,
    bool isCurrencyTB)
  {
    if (ledger == null)
      return (GLTran) null;
    if (ledger.BalanceType == "S")
      return (GLTran) null;
    Decimal? nullable1;
    Decimal? nullable2;
    Decimal? difference;
    if (!isCurrencyTB)
    {
      Decimal? curyDebitTotal = b.CuryDebitTotal;
      nullable1 = b.CuryCreditTotal;
      difference = curyDebitTotal.HasValue & nullable1.HasValue ? new Decimal?(curyDebitTotal.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      Decimal? debitTotal = b.DebitTotal;
      nullable2 = b.CreditTotal;
      difference = debitTotal.HasValue & nullable2.HasValue ? new Decimal?(debitTotal.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    }
    if (this.DoExceedsNegligibleDifference(difference))
      throw new PXException("The batch is not balanced. Review the debit and credit amounts.");
    nullable2 = b.DebitTotal;
    nullable1 = b.CreditTotal;
    if (!this.DoExceedsNegligibleDifference(nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?()))
      return (GLTran) null;
    ((PXSelectBase<Batch>) this.BatchModule).Current = b;
    GLTran glTran1 = new GLTran();
    PX.Objects.CM.Currency currency = PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelect<PX.Objects.CM.Currency, Where<PX.Objects.CM.Currency.curyID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) info.CuryID
    }));
    nullable1 = b.DebitTotal;
    nullable2 = b.CreditTotal;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    if (Math.Sign(nullable3.Value) == 1)
    {
      glTran1.AccountID = currency.RoundingGainAcctID;
      glTran1.SubID = currency.RoundingGainSubID;
      GLTran glTran2 = glTran1;
      nullable1 = b.DebitTotal;
      nullable2 = b.CreditTotal;
      Decimal? nullable4;
      if (!(nullable1.HasValue & nullable2.HasValue))
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault());
      nullable3 = nullable4;
      Decimal? nullable5 = new Decimal?(Math.Round(nullable3.Value, 4));
      glTran2.CreditAmt = nullable5;
      glTran1.DebitAmt = new Decimal?(0M);
      b.ControlTotal = b.DebitTotal;
      b.CreditTotal = b.DebitTotal;
    }
    else
    {
      glTran1.AccountID = currency.RoundingLossAcctID;
      glTran1.SubID = currency.RoundingLossSubID;
      glTran1.CreditAmt = new Decimal?(0M);
      GLTran glTran3 = glTran1;
      nullable1 = b.CreditTotal;
      nullable2 = b.DebitTotal;
      Decimal? nullable6;
      if (!(nullable1.HasValue & nullable2.HasValue))
      {
        nullable3 = new Decimal?();
        nullable6 = nullable3;
      }
      else
        nullable6 = new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault());
      nullable3 = nullable6;
      Decimal? nullable7 = new Decimal?(Math.Round(nullable3.Value, 4));
      glTran3.DebitAmt = nullable7;
      b.ControlTotal = b.CreditTotal;
      b.DebitTotal = b.CreditTotal;
    }
    glTran1.BranchID = b.BranchID;
    glTran1.CuryInfoID = b.CuryInfoID;
    glTran1.CuryCreditAmt = new Decimal?(0M);
    glTran1.CuryDebitAmt = new Decimal?(0M);
    glTran1.TranDesc = PXMessages.LocalizeNoPrefix("Rounding difference");
    glTran1.LedgerID = b.LedgerID;
    glTran1.FinPeriodID = b.FinPeriodID;
    glTran1.TranDate = b.DateEntered;
    glTran1.Released = new bool?(true);
    return (GLTran) ((PXSelectBase) this.GLTran_Module_BatNbr).Cache.Insert((object) glTran1);
  }

  /// <summary>Extension point to create project transaction</summary>
  protected virtual void CreateProjectTransactions(Batch b)
  {
  }

  public class GetFieldValueToResetBase<TFinPeriodIDField, TConst, TDestField> : 
    IBqlOperand,
    IBqlCreator,
    IBqlVerifier
    where TFinPeriodIDField : IBqlField
    where TConst : IBqlOperand
    where TDestField : IBqlField
  {
    private static IBqlCreator GetFieldValueFunc
    {
      get
      {
        return (IBqlCreator) new Switch<Case<Where<TFinPeriodIDField, GreaterEqual<Required<TFinPeriodIDField>>>, TConst>, TDestField>();
      }
    }

    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      return PX.Objects.GL.PostGraph.GetFieldValueToResetBase<TFinPeriodIDField, TConst, TDestField>.GetFieldValueFunc.AppendExpression(ref exp, graph, info, selection);
    }

    public void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      ((IBqlVerifier) PX.Objects.GL.PostGraph.GetFieldValueToResetBase<TFinPeriodIDField, TConst, TDestField>.GetFieldValueFunc).Verify(cache, item, pars, ref result, ref value);
    }
  }

  public class GetFinFieldValueToReset<TConst, TDestField> : 
    PX.Objects.GL.PostGraph.GetFieldValueToResetBase<FinPeriod.masterFinPeriodID, TConst, TDestField>
    where TConst : IBqlOperand
    where TDestField : IBqlField
  {
  }

  public class GetTranFieldValueToReset<TConst, TDestField> : 
    PX.Objects.GL.PostGraph.GetFieldValueToResetBase<GLHistory.finPeriodID, TConst, TDestField>
    where TConst : IBqlOperand
    where TDestField : IBqlField
  {
  }

  public enum HistoryUpdateAmountType
  {
    FinAmounts,
    TranAmounts,
  }

  public enum HistoryUpdateMode
  {
    Common,
    NextYearRetainedEarnings,
  }

  public class TaxTranCreationProcessor
  {
    protected PXCache GLTranCache;
    protected PXCache TaxTranCache;
    protected PXGraph graph;
    protected Dictionary<string, List<PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>>> Documents;
    protected bool skipTaxValidation;

    public TaxTranCreationProcessor(
      PXCache aGLTranCache,
      PXCache aTaxTranCache,
      bool aSkipTaxValidation)
    {
      this.GLTranCache = aGLTranCache;
      this.TaxTranCache = aTaxTranCache;
      this.graph = this.GLTranCache.Graph;
      this.skipTaxValidation = aSkipTaxValidation;
      this.Documents = new Dictionary<string, List<PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>>>();
    }

    public void AddToDocuments(
      PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger> aTran)
    {
      GLTran glTran = PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(aTran);
      if (string.IsNullOrEmpty(glTran.RefNbr))
        return;
      List<PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>> pxResultList;
      if (!this.Documents.TryGetValue(glTran.RefNbr, out pxResultList))
      {
        pxResultList = new List<PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>>(1);
        this.Documents.Add(glTran.RefNbr, pxResultList);
      }
      pxResultList.Add(aTran);
    }

    public virtual bool CreateTaxTransactions()
    {
      bool taxTransactions = false;
      if (this.Documents.Count > 0)
      {
        foreach (KeyValuePair<string, List<PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>>> document in this.Documents)
        {
          List<PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>> source1 = document.Value;
          string key = document.Key;
          Decimal num1 = 0M;
          Decimal num2 = 0M;
          List<GLTran> glTranList1 = new List<GLTran>();
          List<GLTran> glTranList2 = new List<GLTran>();
          List<GLTran> taxLines1 = new List<GLTran>();
          List<GLTran> taxLines2 = new List<GLTran>();
          List<GLTran> taxLines3 = new List<GLTran>();
          List<GLTran> glTranList3 = new List<GLTran>();
          List<GLTran> glTranList4 = new List<GLTran>();
          PX.Objects.CM.CurrencyInfo currencyInfo1 = (PX.Objects.CM.CurrencyInfo) null;
          Dictionary<string, PX.Objects.TX.Tax> taxes = new Dictionary<string, PX.Objects.TX.Tax>();
          Dictionary<string, List<GLTran>> taxCategories = new Dictionary<string, List<GLTran>>();
          Dictionary<string, HashSet<GLTran>> taxGroups = new Dictionary<string, HashSet<GLTran>>();
          foreach (PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger> pxResult in source1)
          {
            GLTran glTran = PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(pxResult);
            Account account = PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(pxResult);
            if (currencyInfo1 == null)
              currencyInfo1 = PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(pxResult);
            num1 += glTran.CuryCreditAmt.Value;
            num2 += glTran.CuryDebitAmt.Value;
            if (!string.IsNullOrEmpty(glTran.TaxID))
            {
              PX.Objects.TX.Tax tax;
              if (!taxes.TryGetValue(glTran.TaxID, out tax))
              {
                tax = PXResultset<PX.Objects.TX.Tax>.op_Implicit(PXSelectBase<PX.Objects.TX.Tax, PXSelect<PX.Objects.TX.Tax, Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>>.Config>.Select(this.graph, new object[1]
                {
                  (object) glTran.TaxID
                }));
                if (tax == null)
                  throw new PXException("Tax {0} used in Document with Reference Nbr. {1} is not found in the system", new object[2]
                  {
                    (object) glTran.TaxID,
                    (object) key
                  });
                taxes.Add(tax.TaxID, tax);
              }
              int? nullable1 = tax.PurchTaxAcctID;
              int? salesTaxAcctId = tax.SalesTaxAcctID;
              int? nullable2;
              if (nullable1.GetValueOrDefault() == salesTaxAcctId.GetValueOrDefault() & nullable1.HasValue == salesTaxAcctId.HasValue)
              {
                nullable2 = tax.PurchTaxSubID;
                nullable1 = tax.SalesTaxSubID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                  throw new PXException("Tax Claimable and Tax Payable accounts and subaccounts for Tax {0} are the same. It's impossible to enter this Tax via GL in this configuration.", new object[1]
                  {
                    (object) glTran.TaxID
                  });
              }
              nullable1 = tax.PurchTaxAcctID;
              nullable2 = glTran.AccountID;
              int num3;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                nullable2 = tax.PurchTaxSubID;
                nullable1 = glTran.SubID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                {
                  num3 = !tax.ReverseTax.GetValueOrDefault() ? 1 : 0;
                  goto label_19;
                }
              }
              num3 = 0;
label_19:
              bool flag1 = num3 != 0;
              nullable1 = tax.SalesTaxAcctID;
              nullable2 = glTran.AccountID;
              int num4;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                nullable2 = tax.SalesTaxSubID;
                nullable1 = glTran.SubID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                {
                  num4 = !tax.ReverseTax.GetValueOrDefault() ? 1 : 0;
                  goto label_23;
                }
              }
              num4 = 0;
label_23:
              bool flag2 = num4 != 0;
              nullable1 = tax.PurchTaxAcctID;
              nullable2 = glTran.AccountID;
              int num5;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                nullable2 = tax.PurchTaxSubID;
                nullable1 = glTran.SubID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                {
                  num5 = tax.ReverseTax.GetValueOrDefault() ? 1 : 0;
                  goto label_27;
                }
              }
              num5 = 0;
label_27:
              bool flag3 = num5 != 0;
              nullable1 = tax.SalesTaxAcctID;
              nullable2 = glTran.AccountID;
              int num6;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                nullable2 = tax.SalesTaxSubID;
                nullable1 = glTran.SubID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                {
                  num6 = tax.ReverseTax.GetValueOrDefault() ? 1 : 0;
                  goto label_31;
                }
              }
              num6 = 0;
label_31:
              bool flag4 = num6 != 0;
              int num7;
              if (tax.TaxType == "W")
              {
                nullable1 = tax.SalesTaxAcctID;
                nullable2 = glTran.AccountID;
                if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                {
                  nullable2 = tax.SalesTaxSubID;
                  nullable1 = glTran.SubID;
                  num7 = nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue ? 1 : 0;
                }
                else
                  num7 = 0;
              }
              else
                num7 = 0;
              if (num7 != 0)
                taxLines3.Add(glTran);
              else if (flag2 | flag3)
                taxLines2.Add(glTran);
              else if (flag1 | flag4)
                taxLines1.Add(glTran);
            }
            else if (!string.IsNullOrEmpty(glTran.TaxCategoryID))
            {
              List<GLTran> glTranList5;
              if (!taxCategories.TryGetValue(glTran.TaxCategoryID, out glTranList5))
              {
                glTranList5 = new List<GLTran>();
                taxCategories.Add(glTran.TaxCategoryID, glTranList5);
              }
              glTranList5.Add(glTran);
              if (account.Type == "A")
                glTranList1.Add(glTran);
              if (account.Type == "L")
                glTranList2.Add(glTran);
              if (account.Type == "E")
                glTranList4.Add(glTran);
              if (account.Type == "I")
                glTranList3.Add(glTran);
            }
          }
          if (taxLines2.Count > 0)
            PX.Objects.GL.PostGraph.TaxTranCreationProcessor.TaxTranCreationHelper.SegregateTaxGroup(this.GLTranCache, taxLines2, taxes, taxCategories, taxGroups);
          if (taxLines1.Count > 0)
            PX.Objects.GL.PostGraph.TaxTranCreationProcessor.TaxTranCreationHelper.SegregateTaxGroup(this.GLTranCache, taxLines1, taxes, taxCategories, taxGroups);
          if (taxLines3.Count > 0)
            PX.Objects.GL.PostGraph.TaxTranCreationProcessor.TaxTranCreationHelper.SegregateTaxGroup(this.GLTranCache, taxLines3, taxes, taxCategories, taxGroups);
          if ((taxLines2.Count > 0 || taxLines1.Count > 0 ? 1 : (taxLines3.Count > 0 ? 1 : 0)) != 0)
          {
            if (num1 != num2)
              throw new PXException("Document with Reference Nbr. {0} is not balanced. Tax information may not be created for it.", new object[1]
              {
                (object) key
              });
            if (taxLines2.Count > 0 && taxLines1.Count > 0)
              throw new PXException("Document with Reference Nbr. {0} contains both sales and purhase Tax transactions. Tax information may not be created for it.", new object[1]
              {
                (object) key
              });
            if (taxLines3.Count > 0 && (taxLines1.Count > 0 || taxLines2.Count > 0))
              throw new PXException("Tax information cannot be created for document with Reference Nbr. {0} because it contains both withholding tax transaction and sales (or purchase) tax transaction.", new object[1]
              {
                (object) key
              });
            if (taxLines2.Count > 0 && glTranList3.Count == 0 && glTranList1.Count == 0)
              throw new PXException("Document with Reference Nbr. {0} contains sales tax transactions, but there are no transactions to income or asset accounts which may be considered as taxable. Tax information may not be created for it.", new object[1]
              {
                (object) key
              });
            if (taxLines1.Count > 0 && glTranList4.Count == 0 && glTranList1.Count == 0)
              throw new PXException("Document with Reference Nbr. {0} contains purchase tax transactions, but there are no transactions to expense or asset accounts which may be considered as taxable. Tax information may not be created for it.", new object[1]
              {
                (object) key
              });
            if (taxLines3.Count > 0 && glTranList4.Count == 0 && glTranList2.Count == 0)
              throw new PXException("Document with Reference Nbr. {0} contains withholding tax transactions but tax information cannot be created for it because no expense or liability accounts, which may be considered as taxable, are specified in the transactions.", new object[1]
              {
                (object) key
              });
            List<GLTran> glTranList6 = taxLines2.Count > 0 ? taxLines2 : (taxLines1.Count > 0 ? taxLines1 : taxLines3);
            string str = taxLines2.Count > 0 || taxLines3.Count > 0 ? "S" : "P";
            Dictionary<string, GLTran> dictionary = new Dictionary<string, GLTran>();
            PXCache glTranCache = this.GLTranCache;
            foreach (GLTran aSrc in glTranList6)
            {
              GLTran copy;
              if (!dictionary.TryGetValue(aSrc.TaxID, out copy))
              {
                copy = (GLTran) glTranCache.CreateCopy((object) aSrc);
                dictionary.Add(copy.TaxID, copy);
              }
              else
                PX.Objects.GL.PostGraph.TaxTranCreationProcessor.TaxTranCreationHelper.Aggregate(copy, aSrc);
            }
            foreach (GLTran glTran1 in dictionary.Values)
            {
              List<GLTran> glTranList7 = new List<GLTran>();
              string taxId = glTran1.TaxID;
              PX.Objects.CM.CurrencyInfo currencyInfo2 = PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>.op_Implicit(source1.First<PXResult<GLTran, CATran, PX.Objects.CM.CurrencyInfo, Account, Ledger>>());
              TaxTran taxTran1 = new TaxTran();
              taxTran1.TaxID = taxId;
              taxTran1.CuryID = currencyInfo2.CuryID;
              TaxTran taxTran2 = taxTran1;
              PX.Objects.TX.Tax taxDef = taxes[taxId];
              string taxType = str;
              if (taxDef.ReverseTax.GetValueOrDefault())
                taxType = str == "S" ? "P" : "S";
              PX.Objects.GL.PostGraph.TaxTranCreationProcessor.TaxTranCreationHelper.Copy(taxTran2, glTran1, taxDef);
              HashSet<GLTran> source2;
              if (taxGroups.TryGetValue(taxId, out source2))
                glTranList7.AddRange((IEnumerable<GLTran>) source2.ToArray<GLTran>());
              if (source2 == null || glTranList7.Count == 0)
                throw new PXException("Document Reference Nbr. {0} doesn't contain any taxable lines that can be applied to {1} Tax", new object[2]
                {
                  (object) key,
                  (object) taxId
                });
              taxTran2.CuryTaxableAmt = taxTran2.TaxableAmt = new Decimal?(0M);
              foreach (GLTran glTran2 in glTranList7)
              {
                Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelectReadonly<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select(glTranCache.Graph, new object[1]
                {
                  (object) glTran2.AccountID
                }));
                int num8;
                if (taxDef.TaxType == "W")
                {
                  num8 = -1;
                  if (account.Type == "I" || account.Type == "A")
                    throw new PXException("Document with Reference Nbr. {0} contains withholding tax transactions, in which the income or asset accounts are specified as taxable. Tax information cannot be created for this document because only expense or liablility accounts can be specified as taxable for withholding tax.", new object[1]
                    {
                      (object) key
                    });
                }
                else
                  num8 = (account.Type == "I" || account.Type == "L") && taxType == "S" || (account.Type == "A" || account.Type == "E") && taxType == "P" ? 1 : -1;
                TaxTran taxTran3 = taxTran2;
                Decimal? nullable3 = taxTran3.CuryTaxableAmt;
                Decimal? nullable4 = glTran2.CuryDebitAmt;
                Decimal? nullable5 = glTran2.CuryCreditAmt;
                Decimal? nullable6 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
                Decimal num9 = (Decimal) num8;
                Decimal? nullable7;
                if (!nullable6.HasValue)
                {
                  nullable5 = new Decimal?();
                  nullable7 = nullable5;
                }
                else
                  nullable7 = new Decimal?(nullable6.GetValueOrDefault() * num9);
                Decimal? nullable8 = nullable7;
                taxTran3.CuryTaxableAmt = nullable3.HasValue & nullable8.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
                TaxTran taxTran4 = taxTran2;
                nullable8 = taxTran4.TaxableAmt;
                nullable5 = glTran2.DebitAmt;
                nullable4 = glTran2.CreditAmt;
                Decimal? nullable9 = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
                Decimal num10 = (Decimal) num8;
                Decimal? nullable10;
                if (!nullable9.HasValue)
                {
                  nullable4 = new Decimal?();
                  nullable10 = nullable4;
                }
                else
                  nullable10 = new Decimal?(nullable9.GetValueOrDefault() * num10);
                nullable3 = nullable10;
                taxTran4.TaxableAmt = nullable8.HasValue & nullable3.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
              }
              int sign = Math.Sign(taxTran2.CuryTaxableAmt.Value);
              TaxTran taxTran5 = taxTran2;
              Decimal? nullable11 = taxTran5.CuryTaxableAmt;
              Decimal num11 = (Decimal) sign;
              taxTran5.CuryTaxableAmt = nullable11.HasValue ? new Decimal?(nullable11.GetValueOrDefault() * num11) : new Decimal?();
              TaxTran taxTran6 = taxTran2;
              nullable11 = taxTran6.TaxableAmt;
              Decimal num12 = (Decimal) sign;
              taxTran6.TaxableAmt = nullable11.HasValue ? new Decimal?(nullable11.GetValueOrDefault() * num12) : new Decimal?();
              TaxTran taxTran7 = taxTran2;
              nullable11 = glTran1.CuryDebitAmt;
              Decimal valueOrDefault1 = nullable11.GetValueOrDefault();
              nullable11 = glTran1.CuryCreditAmt;
              Decimal valueOrDefault2 = nullable11.GetValueOrDefault();
              Decimal? nullable12 = new Decimal?((valueOrDefault1 - valueOrDefault2) * (Decimal) sign);
              taxTran7.CuryTaxAmt = nullable12;
              TaxTran taxTran8 = taxTran2;
              nullable11 = glTran1.DebitAmt;
              Decimal valueOrDefault3 = nullable11.GetValueOrDefault();
              nullable11 = glTran1.CreditAmt;
              Decimal valueOrDefault4 = nullable11.GetValueOrDefault();
              Decimal? nullable13 = new Decimal?((valueOrDefault3 - valueOrDefault4) * (Decimal) sign);
              taxTran8.TaxAmt = nullable13;
              taxTran2.TranType = PX.Objects.GL.PostGraph.TaxTranCreationProcessor.GetTranType(taxType, (Decimal) sign);
              TaxRev taxRev1 = (TaxRev) null;
              foreach (PXResult<TaxRev> pxResult in PXSelectBase<TaxRev, PXSelectReadonly<TaxRev, Where<TaxRev.taxID, Equal<Required<TaxRev.taxID>>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<Required<TaxRev.taxType>>, And<Required<GLTran.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>.Config>.Select(this.graph, new object[3]
              {
                (object) taxId,
                (object) taxType,
                (object) taxTran2.TranDate
              }))
              {
                TaxRev taxRev2 = PXResult<TaxRev>.op_Implicit(pxResult);
                if (taxRev1 != null && taxRev2.TaxID == taxId)
                  throw new PXException("There are several records for the Tax Rate for tax {0} and date {1}. Tax inforamtion may not be created", new object[2]
                  {
                    (object) taxId,
                    (object) taxTran2.TranDate
                  });
                taxRev1 = (TaxRev) this.graph.Caches[typeof (TaxRev)].CreateCopy((object) taxRev2);
                if (taxRev1 != null && !taxDef.DeductibleVAT.GetValueOrDefault())
                  taxRev1.NonDeductibleTaxRate = new Decimal?(100M);
              }
              if (taxRev1 != null)
              {
                PX.Objects.GL.PostGraph.TaxTranCreationProcessor.TaxTranCreationHelper.Copy(taxTran2, taxRev1);
                PX.Objects.GL.PostGraph.TaxTranCreationProcessor.TaxTranCreationHelper.AdjustExpenseAmt(this.TaxTranCache, taxTran2);
                if (!this.skipTaxValidation)
                  PX.Objects.GL.PostGraph.TaxTranCreationProcessor.TaxTranCreationHelper.Validate(this.TaxTranCache, taxTran2, taxRev1, glTran1);
                taxTran2.Released = new bool?(true);
                TaxTran taxTran9 = (TaxTran) this.TaxTranCache.Insert((object) taxTran2);
                taxTransactions = true;
              }
              else
                throw new PXException("The {0} tax rate is not specified in the settings of the selected tax.", new object[2]
                {
                  (object) taxDef.TaxID,
                  (object) PXMessages.LocalizeNoPrefix(GetLabel.For<TaxType>(taxType)).ToLower()
                });
            }
          }
        }
      }
      return taxTransactions;
    }

    public static string GetTranType(string taxType, Decimal sign)
    {
      string str = string.Empty;
      if (taxType == "S")
        str = sign > 0M ? "TRV" : "TFW";
      if (taxType == "P")
        str = sign > 0M ? "TFW" : "TRV";
      return !string.IsNullOrEmpty(str) ? str : throw new PXException("Type of tax transaction is not recognized from Tax Type{0} and sign {1}", new object[2]
      {
        (object) taxType,
        (object) sign
      });
    }

    public static class TaxTranCreationHelper
    {
      public static void Copy(TaxTran aDest, TaxRev aSrc)
      {
        aDest.TaxRate = aSrc.TaxRate;
        aDest.TaxType = aSrc.TaxType;
        aDest.TaxBucketID = aSrc.TaxBucketID;
        aDest.NonDeductibleTaxRate = aSrc.NonDeductibleTaxRate;
      }

      public static void Copy(TaxTran aDest, GLTran aTaxTran, PX.Objects.TX.Tax taxDef)
      {
        aDest.TaxID = aTaxTran.TaxID;
        aDest.AccountID = aTaxTran.AccountID;
        aDest.SubID = aTaxTran.SubID;
        aDest.CuryInfoID = aTaxTran.CuryInfoID;
        aDest.Module = aTaxTran.Module;
        aDest.TranDate = aTaxTran.TranDate;
        aDest.BranchID = aTaxTran.BranchID;
        aDest.TranType = aTaxTran.TranType;
        aDest.RefNbr = aTaxTran.BatchNbr;
        aDest.FinPeriodID = aTaxTran.FinPeriodID;
        aDest.LineRefNbr = aTaxTran.RefNbr;
        aDest.Description = aTaxTran.TranDesc;
        aDest.VendorID = taxDef.TaxVendorID;
      }

      public static void Aggregate(GLTran aDest, GLTran aSrc)
      {
        GLTran glTran1 = aDest;
        Decimal? nullable = glTran1.CuryCreditAmt;
        Decimal valueOrDefault1 = aSrc.CuryCreditAmt.GetValueOrDefault();
        glTran1.CuryCreditAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
        GLTran glTran2 = aDest;
        nullable = glTran2.CuryDebitAmt;
        Decimal valueOrDefault2 = aSrc.CuryDebitAmt.GetValueOrDefault();
        glTran2.CuryDebitAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
        GLTran glTran3 = aDest;
        nullable = glTran3.CreditAmt;
        Decimal valueOrDefault3 = aSrc.CreditAmt.GetValueOrDefault();
        glTran3.CreditAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
        GLTran glTran4 = aDest;
        nullable = glTran4.DebitAmt;
        Decimal valueOrDefault4 = aSrc.DebitAmt.GetValueOrDefault();
        glTran4.DebitAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
      }

      public static void Validate(PXCache cache, TaxTran tran, TaxRev taxRev, GLTran aDocTran)
      {
        Decimal? nullable;
        if (Math.Sign(tran.CuryTaxAmt.Value) != 0)
        {
          int num1 = Math.Sign(tran.CuryTaxableAmt.Value);
          nullable = tran.CuryTaxAmt;
          int num2 = Math.Sign(nullable.Value);
          if (num1 != num2)
            throw new PXException("Taxable and Tax Amount have different signs for tax {0} in the document {1}", new object[2]
            {
              (object) aDocTran.TaxID,
              (object) aDocTran.RefNbr
            });
        }
        nullable = tran.CuryTaxAmt;
        if (nullable.Value < 0M)
          throw new PXException("Tax Amount is negative for tax {0} in the document {1}", new object[2]
          {
            (object) aDocTran.TaxID,
            (object) aDocTran.RefNbr
          });
        TaxTran tax = PX.Objects.GL.PostGraph.TaxTranCreationProcessor.TaxTranCreationHelper.CalculateTax(cache, tran, taxRev);
        nullable = tax.CuryTaxAmt;
        Decimal? curyTaxAmt = tran.CuryTaxAmt;
        if (!(nullable.GetValueOrDefault() == curyTaxAmt.GetValueOrDefault() & nullable.HasValue == curyTaxAmt.HasValue))
        {
          if (Decimal.Compare(tran.NonDeductibleTaxRate.Value, 100M) < 0)
            throw new PXException("Tax Amount {0} does not match the amount {1} calculated from taxable amount {2} and tax Rate {3}% and Non-deductible tax rate {4}% for Tax {5} in the document {6}", new object[7]
            {
              (object) tran.CuryTaxAmt.Value,
              (object) tax.CuryTaxAmt,
              (object) tran.CuryTaxableAmt.Value,
              (object) tran.TaxRate,
              (object) tran.NonDeductibleTaxRate,
              (object) aDocTran.TaxID,
              (object) aDocTran.RefNbr
            });
          throw new PXException("Tax Amount {0} does not match the amount {1} calculated from taxable amount {2} and tax Rate {3}% for Tax {4} in the document {5}", new object[6]
          {
            (object) tran.CuryTaxAmt.Value,
            (object) tax.CuryTaxAmt,
            (object) tran.CuryTaxableAmt.Value,
            (object) tran.TaxRate,
            (object) aDocTran.TaxID,
            (object) aDocTran.RefNbr
          });
        }
        Decimal? curyTaxableAmt = tran.CuryTaxableAmt;
        nullable = tax.CuryTaxableAmt;
        if (curyTaxableAmt.GetValueOrDefault() == nullable.GetValueOrDefault() & curyTaxableAmt.HasValue == nullable.HasValue)
          return;
        tran.CuryTaxableAmt = tax.CuryTaxableAmt;
      }

      public static TaxTran CalculateTax(PXCache cache, TaxTran aTran, TaxRev aTaxRev)
      {
        Decimal val1 = aTran.CuryTaxableAmt.GetValueOrDefault();
        Decimal num1 = aTran.TaxableAmt.GetValueOrDefault();
        Decimal? taxableMin = aTaxRev.TaxableMin;
        Decimal num2 = 0.0M;
        if (!(taxableMin.GetValueOrDefault() == num2 & taxableMin.HasValue) && num1 < aTaxRev.TaxableMin.Value)
        {
          val1 = 0.0M;
          num1 = 0.0M;
        }
        Decimal? nullable = aTaxRev.TaxableMax;
        num2 = 0.0M;
        if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
        {
          Decimal num3 = num1;
          nullable = aTaxRev.TaxableMax;
          Decimal num4 = nullable.Value;
          if (num3 > num4)
          {
            PXCache sender = cache;
            TaxTran row = aTran;
            nullable = aTaxRev.TaxableMax;
            Decimal baseval = nullable.Value;
            ref Decimal local = ref val1;
            PXDBCurrencyAttribute.CuryConvCury(sender, (object) row, baseval, out local);
            nullable = aTaxRev.TaxableMax;
            Decimal num5 = nullable.Value;
          }
        }
        Decimal val2 = 0M;
        Decimal num6 = val1;
        nullable = aTaxRev.TaxRate;
        Decimal num7 = nullable.Value;
        Decimal val3 = num6 * num7 / 100M;
        nullable = aTaxRev.NonDeductibleTaxRate;
        if (nullable.Value < 100M)
        {
          Decimal num8 = val3;
          nullable = aTaxRev.NonDeductibleTaxRate;
          Decimal num9 = 1M - nullable.Value / 100M;
          val2 = num8 * num9;
          val3 -= val2;
        }
        TaxTran copy = (TaxTran) cache.CreateCopy((object) aTran);
        copy.CuryTaxableAmt = new Decimal?(PXDBCurrencyAttribute.RoundCury(cache, (object) aTran, val1));
        copy.CuryTaxAmt = new Decimal?(PXDBCurrencyAttribute.RoundCury(cache, (object) aTran, val3));
        copy.CuryExpenseAmt = new Decimal?(PXDBCurrencyAttribute.RoundCury(cache, (object) aTran, val2));
        return copy;
      }

      public static void AdjustExpenseAmt(PXCache cache, TaxTran tran)
      {
        if (tran == null || !(tran.NonDeductibleTaxRate.Value < 100M))
          return;
        Decimal num1 = tran.TaxRate.Value;
        Decimal? nullable = tran.NonDeductibleTaxRate;
        Decimal num2 = 100M - nullable.Value;
        Decimal num3 = num1 * num2 / 100M;
        nullable = tran.CuryTaxableAmt;
        Decimal val1 = nullable.Value / ((100M + num3) / 100M);
        Decimal val2 = val1 * num3 / 100M;
        tran.CuryExpenseAmt = new Decimal?(PXDBCurrencyAttribute.RoundCury(cache, (object) tran, val2));
        tran.CuryTaxableAmt = new Decimal?(PXDBCurrencyAttribute.RoundCury(cache, (object) tran, val1));
      }

      public static void SegregateTaxGroup(
        PXCache cache,
        List<GLTran> taxLines,
        Dictionary<string, PX.Objects.TX.Tax> taxes,
        Dictionary<string, List<GLTran>> taxCategories,
        Dictionary<string, HashSet<GLTran>> taxGroups)
      {
        foreach (GLTran taxLine in taxLines)
        {
          if (taxLine.TaxID != null || taxes.ContainsKey(taxLine.TaxID))
          {
            PXResultset<PX.Objects.TX.TaxCategory> pxResultset = ((PXSelectBase<PX.Objects.TX.TaxCategory>) new PXSelectJoin<PX.Objects.TX.TaxCategory, LeftJoin<TaxCategoryDet, On<PX.Objects.TX.TaxCategory.taxCategoryID, Equal<TaxCategoryDet.taxCategoryID>>>, Where2<Where<PX.Objects.TX.TaxCategory.taxCatFlag, Equal<True>, And<Where<TaxCategoryDet.taxID, NotEqual<Required<TaxCategoryDet.taxID>>, Or<TaxCategoryDet.taxID, IsNull>>>>, Or<Where<PX.Objects.TX.TaxCategory.taxCatFlag, Equal<False>, And<TaxCategoryDet.taxID, Equal<Required<TaxCategoryDet.taxID>>>>>>>(cache.Graph)).Select(new object[2]
            {
              (object) taxLine.TaxID,
              (object) taxLine.TaxID
            });
            if (pxResultset.Count != 0)
            {
              foreach (PXResult<PX.Objects.TX.TaxCategory> pxResult in pxResultset)
              {
                PX.Objects.TX.TaxCategory taxCategory = PXResult<PX.Objects.TX.TaxCategory>.op_Implicit(pxResult);
                List<GLTran> other;
                if (taxCategories.TryGetValue(taxCategory.TaxCategoryID, out other))
                {
                  HashSet<GLTran> glTranSet;
                  if (!taxGroups.TryGetValue(taxLine.TaxID, out glTranSet))
                  {
                    glTranSet = new HashSet<GLTran>();
                    taxGroups.Add(taxLine.TaxID, glTranSet);
                  }
                  glTranSet.UnionWith((IEnumerable<GLTran>) other);
                }
              }
            }
          }
        }
      }
    }
  }
}
