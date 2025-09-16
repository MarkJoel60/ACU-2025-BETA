// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATranEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.CA;

[TableAndChartDashboardType]
[Serializable]
public class CATranEnq : PXGraph<
#nullable disable
CATranEnq>
{
  public PXSave<CAEnqFilter> Save;
  public PXAction<CAEnqFilter> cancel;
  public PXAction<CAEnqFilter> Release;
  public PXAction<CAEnqFilter> Clearence;
  public PXAction<CAEnqFilter> viewDoc;
  public PXAction<CAEnqFilter> viewRecon;
  public PXAction<CAEnqFilter> doubleClick;
  public PXAction<CAEnqFilter> viewBatch;
  public PXAction<CAEnqFilter> viewBatchPayment;
  public PXAction<CAEnqFilter> viewBankDeposit;
  public PXAction<CAEnqFilter> viewBAccount;
  public PXFilter<CAEnqFilter> Filter;
  public PXSelectReadonly<CADailySummary, Where<CADailySummary.cashAccountID, Equal<Current<CAEnqFilter.cashAccountID>>, And<CADailySummary.tranDate, Between<Current<CAEnqFilter.startDate>, Current<CAEnqFilter.endDate>>>>> CATranListSummarized;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoinOrderBy<CATranEnq.CATranExt, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<CATran.referenceID>>>, OrderBy<Asc<CATran.tranDate, Asc<CATran.tranID>>>> CATranListRecords;
  public PXSelect<PX.Objects.CM.CurrencyInfo> currencyinfo;
  public ToggleCurrency<CAEnqFilter> CurrencyView;
  public PXSelect<CAAdj, Where<CAAdj.adjTranType, Equal<CATranType.cAAdjustment>, And<CAAdj.adjRefNbr, Equal<Required<CAAdj.adjRefNbr>>>>> caadj_adjRefNbr;
  public PXSelect<CASplit, Where<CASplit.adjTranType, Equal<CATranType.cAAdjustment>, And<CASplit.adjRefNbr, Equal<Required<CASplit.adjRefNbr>>>>> casplit_adjRefNbr;
  public PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Current<CAEnqFilter.cashAccountID>>>> cashaccount;
  public PXSetup<CASetup> casetup;
  public PXSetup<APSetup> apsetup;
  public PXSetup<ARSetup> arsetup;
  public PXSelect<BAccountR> BAccountRCache;
  [PXHidden]
  public PXSelect<EPApproval> EPApprovalCache;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    ((PXSelectBase) this.CATranListRecords).Cache.Clear();
    ((PXSelectBase) this.CATranListRecords).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.Filter).Cache.Clear();
    ((PXGraph) this).Caches[typeof (CADailySummary)].Clear();
    ((PXGraph) this).TimeStamp = (byte[]) null;
    PXLongOperation.ClearStatus(((PXGraph) this).UID);
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton(IsLockedOnToolbar = true)]
  public virtual IEnumerable release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CATranEnq.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new CATranEnq.\u003C\u003Ec__DisplayClass9_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.setup = ((PXSelectBase<CASetup>) this.casetup).Current;
    CAEnqFilter current = ((PXSelectBase<CAEnqFilter>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.tranList = new List<CATran>();
    foreach (PXResult<CATran> pxResult in PXSelectBase<CATran, PXSelect<CATran, Where2<Where<Required<CAEnqFilter.includeUnreleased>, Equal<boolTrue>, Or<CATran.released, Equal<boolTrue>>>, And<CATran.cashAccountID, Equal<Required<CAEnqFilter.cashAccountID>>, And<CATran.tranDate, Between<Required<CAEnqFilter.startDate>, Required<CAEnqFilter.endDate>>>>>, OrderBy<Asc<CATran.tranDate, Asc<CATran.extRefNbr, Asc<CATran.tranID>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) current.IncludeUnreleased,
      (object) current.CashAccountID,
      (object) current.StartDate,
      (object) current.EndDate
    }))
    {
      CATran caTran = PXResult<CATran>.op_Implicit(pxResult);
      if (caTran.Selected.GetValueOrDefault())
      {
        // ISSUE: reference to a compiler-generated field
        cDisplayClass90.tranList.Add(caTran);
      }
    }
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.tranList.Count == 0)
      throw new PXException("No document selected. Please select one or more documents to process.");
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.clone = GraphHelper.Clone<CATranEnq>(this);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass90, __methodptr(\u003Crelease\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton(IsLockedOnToolbar = true)]
  public virtual IEnumerable clearence(PXAdapter adapter)
  {
    CAEnqFilter current = ((PXSelectBase<CAEnqFilter>) this.Filter).Current;
    foreach (PXResult<CATranEnq.CATranExt> pxResult in PXSelectBase<CATranEnq.CATranExt, PXSelect<CATranEnq.CATranExt, Where2<Where<Required<CAEnqFilter.includeUnreleased>, Equal<boolTrue>, Or<CATran.released, Equal<boolTrue>>>, And<CATran.cashAccountID, Equal<Required<CAEnqFilter.cashAccountID>>, And<CATran.tranDate, Between<Required<CAEnqFilter.startDate>, Required<CAEnqFilter.endDate>>>>>, OrderBy<Asc<CATran.tranDate, Asc<CATran.extRefNbr, Asc<CATran.tranID>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) current.IncludeUnreleased,
      (object) current.CashAccountID,
      (object) current.StartDate,
      (object) current.EndDate
    }))
    {
      CATranEnq.CATranExt caTranExt = PXResult<CATranEnq.CATranExt>.op_Implicit(pxResult);
      bool? nullable = caTranExt.Reconciled;
      if (!nullable.GetValueOrDefault())
      {
        nullable = caTranExt.Selected;
        if (nullable.GetValueOrDefault())
        {
          CATranEnq.CATranExt copy = PXCache<CATranEnq.CATranExt>.CreateCopy(caTranExt);
          copy.Cleared = new bool?(true);
          ((PXSelectBase) this.CATranListRecords).Cache.Update((object) copy);
        }
      }
    }
    ((PXAction) this.Save).Press();
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDoc(PXAdapter adapter)
  {
    CATran.Redirect(((PXSelectBase) this.Filter).Cache, (CATran) ((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Current);
    return (IEnumerable) ((PXSelectBase<CAEnqFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewRecon(PXAdapter adapter)
  {
    if (((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Current.ReconNbr != null)
    {
      CAReconEntry instance = PXGraph.CreateInstance<CAReconEntry>();
      CARecon caRecon = PXResultset<CARecon>.op_Implicit(PXSelectBase<CARecon, PXSelect<CARecon, Where<CARecon.reconNbr, Equal<Required<CATran.reconNbr>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) ((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Current.ReconNbr
      }));
      if (caRecon != null)
      {
        ((PXSelectBase<CARecon>) instance.CAReconRecords).Current = caRecon;
        throw new PXRedirectRequiredException((PXGraph) instance, "Reconciliation");
      }
    }
    return (IEnumerable) ((PXSelectBase<CAEnqFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable DoubleClick(PXAdapter adapter)
  {
    CAEnqFilter current1 = ((PXSelectBase<CAEnqFilter>) this.Filter).Current;
    if (current1.ShowSummary.GetValueOrDefault())
    {
      CATran current2 = (CATran) ((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Current;
      current1.LastStartDate = current1.StartDate;
      current1.LastEndDate = current1.EndDate;
      current1.StartDate = current2.TranDate;
      current1.EndDate = current2.TranDate;
      current1.ShowSummary = new bool?(false);
      ((PXSelectBase) this.CATranListRecords).Cache.Clear();
      ((PXGraph) this).Caches[typeof (CADailySummary)].Clear();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    PX.Objects.GL.Batch batch = PXResultset<PX.Objects.GL.Batch>.op_Implicit(PXSelectBase<PX.Objects.GL.Batch, PXSelect<PX.Objects.GL.Batch, Where<PX.Objects.GL.Batch.module, Equal<Required<PX.Objects.GL.Batch.module>>, And<PX.Objects.GL.Batch.batchNbr, Equal<Required<PX.Objects.GL.Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Current.OrigModule,
      (object) ((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Current.BatchNbr
    }));
    if (batch != null)
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<PX.Objects.GL.Batch>) instance.BatchModule).Current = batch;
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewBatchPayment(PXAdapter adapter)
  {
    if (((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Current?.OrigModule == "AP")
    {
      CABatchEntry instance = PXGraph.CreateInstance<CABatchEntry>();
      ((PXSelectBase<CABatch>) instance.Document).Current = CABatch.PK.Find((PXGraph) instance, ((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Current.BatchPaymentRefNbr);
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewBankDeposit(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Current?.DepositNbr))
    {
      CADepositEntry instance = PXGraph.CreateInstance<CADepositEntry>();
      ((PXSelectBase<CADeposit>) instance.Document).Current = PXResultset<CADeposit>.op_Implicit(((PXSelectBase<CADeposit>) instance.Document).Search<CADeposit.refNbr>((object) ((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Current.DepositNbr, Array.Empty<object>()));
      if (((PXSelectBase<CADeposit>) instance.Document).Current != null)
        PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewBAccount()
  {
    CATranEnq.CATranExt current = ((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Current;
    if ((current != null ? (!current.ReferenceID.HasValue ? 1 : 0) : 1) != 0)
      return;
    PXRedirectHelper.TryRedirect(((PXGraph) this).Caches[typeof (PX.Objects.CR.BAccount)], (object) PX.Objects.CR.BAccount.PK.Find((PXGraph) this, current.ReferenceID), "Business Account", (PXRedirectHelper.WindowMode) 3);
  }

  public virtual void Persist()
  {
    List<CATran> caTranList = new List<CATran>((IEnumerable<CATran>) ((PXGraph) this).Caches[typeof (CATran)].Updated);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXGraph) this).Persist();
      foreach (CATran tran in caTranList)
      {
        if (!tran.Reconciled.GetValueOrDefault())
          CAReconEntry.UpdateClearedOnSourceDoc(tran);
      }
      transactionScope.Complete((PXGraph) this);
    }
    ((PXGraph) this).Caches[typeof (PX.Objects.CM.CurrencyInfo)].SetStatus(((PXGraph) this).Caches[typeof (PX.Objects.CM.CurrencyInfo)].Current, (PXEntryStatus) 2);
  }

  public CATranEnq()
  {
    PXUIFieldAttribute.SetVisible<CATran.reconNbr>(((PXSelectBase) this.CATranListRecords).Cache, (object) null, false);
    CASetup current = ((PXSelectBase<CASetup>) this.casetup).Current;
  }

  public virtual void GetRange(
    DateTime date,
    string Range,
    int? cashAccountID,
    out DateTime? RangeStart,
    out DateTime? RangeEnd)
  {
    switch (Range)
    {
      case "W":
        RangeStart = new DateTime?(date.AddDays((double) (-1 * ((int) PXDateTime.DayOfWeekOrdinal(date.DayOfWeek) - 1))));
        RangeEnd = new DateTime?(date.AddDays((double) (7 - (int) PXDateTime.DayOfWeekOrdinal(date.DayOfWeek))));
        break;
      case "M":
        RangeStart = new DateTime?(new DateTime(date.Year, date.Month, 1));
        ref DateTime? local = ref RangeEnd;
        DateTime dateTime = new DateTime(date.Year, date.Month, 1);
        dateTime = dateTime.AddMonths(1);
        DateTime? nullable = new DateTime?(dateTime.AddDays(-1.0));
        local = nullable;
        break;
      case "P":
        int? parentOrganizationId = PXAccess.GetParentOrganizationID((int?) PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) cashAccountID
        }))?.BranchID);
        FinPeriod finPeriodByDate = this.FinPeriodRepository.FindFinPeriodByDate(new DateTime?(date), parentOrganizationId);
        RangeStart = (DateTime?) finPeriodByDate?.StartDate;
        RangeEnd = (DateTime?) finPeriodByDate?.EndDate;
        break;
      default:
        RangeStart = new DateTime?(date);
        RangeEnd = new DateTime?(date);
        break;
    }
  }

  protected virtual IEnumerable filter()
  {
    CATranEnq caTranEnq1 = this;
    PXCache cache = ((PXGraph) caTranEnq1).Caches[typeof (CAEnqFilter)];
    if (cache != null && cache.Current is CAEnqFilter current)
    {
      DateTime? nullable1;
      if (!current.StartDate.HasValue || !current.EndDate.HasValue)
      {
        CATranEnq caTranEnq2 = caTranEnq1;
        nullable1 = ((PXGraph) caTranEnq1).Accessinfo.BusinessDate;
        DateTime date = nullable1.Value;
        string dateRangeDefault = ((PXSelectBase<CASetup>) caTranEnq1.casetup).Current.DateRangeDefault;
        int? cashAccountId = current.CashAccountID;
        DateTime? nullable2;
        ref DateTime? local1 = ref nullable2;
        DateTime? nullable3;
        ref DateTime? local2 = ref nullable3;
        caTranEnq2.GetRange(date, dateRangeDefault, cashAccountId, out local1, out local2);
        current.StartDate = nullable2;
        current.EndDate = nullable3;
      }
      if (current.CashAccountID.HasValue)
      {
        nullable1 = current.StartDate;
        if (nullable1.HasValue)
        {
          CADailySummary caDailySummary = PXResultset<CADailySummary>.op_Implicit(PXSelectBase<CADailySummary, PXSelectGroupBy<CADailySummary, Where<CADailySummary.cashAccountID, Equal<Required<CAEnqFilter.cashAccountID>>, And<CADailySummary.tranDate, Less<Required<CAEnqFilter.startDate>>>>, Aggregate<Sum<CADailySummary.amtReleasedClearedCr, Sum<CADailySummary.amtReleasedClearedDr, Sum<CADailySummary.amtReleasedUnclearedCr, Sum<CADailySummary.amtReleasedUnclearedDr, Sum<CADailySummary.amtUnreleasedClearedCr, Sum<CADailySummary.amtUnreleasedClearedDr, Sum<CADailySummary.amtUnreleasedUnclearedCr, Sum<CADailySummary.amtUnreleasedUnclearedDr, GroupBy<CADailySummary.cashAccountID>>>>>>>>>>>.Config>.Select((PXGraph) caTranEnq1, new object[2]
          {
            (object) current.CashAccountID,
            (object) current.StartDate
          }));
          Decimal? nullable4;
          Decimal? nullable5;
          Decimal? nullable6;
          if (caDailySummary == null || !caDailySummary.CashAccountID.HasValue)
          {
            current.BegBal = new Decimal?(0M);
            current.BegClearedBal = new Decimal?(0M);
          }
          else
          {
            CAEnqFilter caEnqFilter1 = current;
            Decimal? nullable7 = caDailySummary.AmtReleasedClearedDr;
            Decimal? nullable8 = caDailySummary.AmtReleasedClearedCr;
            Decimal? nullable9 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
            nullable4 = caDailySummary.AmtReleasedUnclearedDr;
            Decimal? nullable10;
            if (!(nullable9.HasValue & nullable4.HasValue))
            {
              nullable8 = new Decimal?();
              nullable10 = nullable8;
            }
            else
              nullable10 = new Decimal?(nullable9.GetValueOrDefault() + nullable4.GetValueOrDefault());
            Decimal? nullable11 = nullable10;
            nullable5 = caDailySummary.AmtReleasedUnclearedCr;
            Decimal? nullable12;
            if (!(nullable11.HasValue & nullable5.HasValue))
            {
              nullable4 = new Decimal?();
              nullable12 = nullable4;
            }
            else
              nullable12 = new Decimal?(nullable11.GetValueOrDefault() - nullable5.GetValueOrDefault());
            caEnqFilter1.BegBal = nullable12;
            CAEnqFilter caEnqFilter2 = current;
            nullable5 = caDailySummary.AmtReleasedClearedDr;
            nullable6 = caDailySummary.AmtReleasedClearedCr;
            Decimal? nullable13;
            if (!(nullable5.HasValue & nullable6.HasValue))
            {
              nullable4 = new Decimal?();
              nullable13 = nullable4;
            }
            else
              nullable13 = new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault());
            caEnqFilter2.BegClearedBal = nullable13;
            if (current.IncludeUnreleased.GetValueOrDefault())
            {
              CAEnqFilter caEnqFilter3 = current;
              nullable6 = caEnqFilter3.BegBal;
              Decimal? unreleasedClearedDr = caDailySummary.AmtUnreleasedClearedDr;
              Decimal? nullable14 = caDailySummary.AmtUnreleasedClearedCr;
              nullable8 = unreleasedClearedDr.HasValue & nullable14.HasValue ? new Decimal?(unreleasedClearedDr.GetValueOrDefault() - nullable14.GetValueOrDefault()) : new Decimal?();
              nullable7 = caDailySummary.AmtUnreleasedUnclearedDr;
              Decimal? nullable15;
              if (!(nullable8.HasValue & nullable7.HasValue))
              {
                nullable14 = new Decimal?();
                nullable15 = nullable14;
              }
              else
                nullable15 = new Decimal?(nullable8.GetValueOrDefault() + nullable7.GetValueOrDefault());
              nullable4 = nullable15;
              Decimal? nullable16 = caDailySummary.AmtUnreleasedUnclearedCr;
              Decimal? nullable17;
              if (!(nullable4.HasValue & nullable16.HasValue))
              {
                nullable7 = new Decimal?();
                nullable17 = nullable7;
              }
              else
                nullable17 = new Decimal?(nullable4.GetValueOrDefault() - nullable16.GetValueOrDefault());
              nullable5 = nullable17;
              Decimal? nullable18;
              if (!(nullable6.HasValue & nullable5.HasValue))
              {
                nullable16 = new Decimal?();
                nullable18 = nullable16;
              }
              else
                nullable18 = new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault());
              caEnqFilter3.BegBal = nullable18;
              CAEnqFilter caEnqFilter4 = current;
              nullable5 = caEnqFilter4.BegClearedBal;
              nullable16 = caDailySummary.AmtUnreleasedClearedDr;
              nullable4 = caDailySummary.AmtUnreleasedClearedCr;
              Decimal? nullable19;
              if (!(nullable16.HasValue & nullable4.HasValue))
              {
                nullable7 = new Decimal?();
                nullable19 = nullable7;
              }
              else
                nullable19 = new Decimal?(nullable16.GetValueOrDefault() - nullable4.GetValueOrDefault());
              nullable6 = nullable19;
              Decimal? nullable20;
              if (!(nullable5.HasValue & nullable6.HasValue))
              {
                nullable4 = new Decimal?();
                nullable20 = nullable4;
              }
              else
                nullable20 = new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault());
              caEnqFilter4.BegClearedBal = nullable20;
            }
          }
          current.DebitTotal = new Decimal?(0M);
          current.CreditTotal = new Decimal?(0M);
          current.DebitClearedTotal = new Decimal?(0M);
          current.CreditClearedTotal = new Decimal?(0M);
          int num1 = 0;
          int num2 = 0;
          foreach (PXResult<CATranEnq.CATranExt> pxResult in ((PXSelectBase) caTranEnq1.CATranListRecords).View.Select(PXView.Currents, PXView.Parameters, new object[0], new string[0], new bool[0], ((PXSelectBase) caTranEnq1.CATranListRecords).View.GetExternalFilters(), ref num1, 0, ref num2))
          {
            CATranEnq.CATranExt caTranExt = PXResult<CATranEnq.CATranExt>.op_Implicit(pxResult);
            CAEnqFilter caEnqFilter5 = current;
            nullable6 = caEnqFilter5.DebitTotal;
            nullable5 = caTranExt.CuryDebitAmt;
            Decimal? nullable21;
            if (!(nullable6.HasValue & nullable5.HasValue))
            {
              nullable4 = new Decimal?();
              nullable21 = nullable4;
            }
            else
              nullable21 = new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault());
            caEnqFilter5.DebitTotal = nullable21;
            CAEnqFilter caEnqFilter6 = current;
            nullable5 = caEnqFilter6.CreditTotal;
            nullable6 = caTranExt.CuryCreditAmt;
            Decimal? nullable22;
            if (!(nullable5.HasValue & nullable6.HasValue))
            {
              nullable4 = new Decimal?();
              nullable22 = nullable4;
            }
            else
              nullable22 = new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault());
            caEnqFilter6.CreditTotal = nullable22;
            CAEnqFilter caEnqFilter7 = current;
            nullable6 = caEnqFilter7.DebitClearedTotal;
            nullable5 = current.ShowSummary.GetValueOrDefault() ? caTranExt.CuryClearedDebitTotalAmt : caTranExt.CuryClearedDebitAmt;
            Decimal? nullable23;
            if (!(nullable6.HasValue & nullable5.HasValue))
            {
              nullable4 = new Decimal?();
              nullable23 = nullable4;
            }
            else
              nullable23 = new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault());
            caEnqFilter7.DebitClearedTotal = nullable23;
            CAEnqFilter caEnqFilter8 = current;
            nullable5 = caEnqFilter8.CreditClearedTotal;
            nullable6 = current.ShowSummary.GetValueOrDefault() ? caTranExt.CuryClearedCreditTotalAmt : caTranExt.CuryClearedCreditAmt;
            Decimal? nullable24;
            if (!(nullable5.HasValue & nullable6.HasValue))
            {
              nullable4 = new Decimal?();
              nullable24 = nullable4;
            }
            else
              nullable24 = new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault());
            caEnqFilter8.CreditClearedTotal = nullable24;
          }
          CAEnqFilter caEnqFilter9 = current;
          Decimal? nullable25 = current.BegBal;
          Decimal? nullable26 = current.DebitTotal;
          Decimal? nullable27 = nullable25.HasValue & nullable26.HasValue ? new Decimal?(nullable25.GetValueOrDefault() + nullable26.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable28 = current.CreditTotal;
          Decimal? nullable29;
          if (!(nullable27.HasValue & nullable28.HasValue))
          {
            nullable26 = new Decimal?();
            nullable29 = nullable26;
          }
          else
            nullable29 = new Decimal?(nullable27.GetValueOrDefault() - nullable28.GetValueOrDefault());
          caEnqFilter9.EndBal = nullable29;
          CAEnqFilter caEnqFilter10 = current;
          nullable26 = current.BegClearedBal;
          nullable25 = current.DebitClearedTotal;
          nullable28 = nullable26.HasValue & nullable25.HasValue ? new Decimal?(nullable26.GetValueOrDefault() + nullable25.GetValueOrDefault()) : new Decimal?();
          Decimal? creditClearedTotal = current.CreditClearedTotal;
          Decimal? nullable30;
          if (!(nullable28.HasValue & creditClearedTotal.HasValue))
          {
            nullable25 = new Decimal?();
            nullable30 = nullable25;
          }
          else
            nullable30 = new Decimal?(nullable28.GetValueOrDefault() - creditClearedTotal.GetValueOrDefault());
          caEnqFilter10.EndClearedBal = nullable30;
        }
      }
    }
    yield return cache.Current;
    cache.IsDirty = false;
  }

  public virtual IEnumerable cATranListRecords()
  {
    CAEnqFilter current1 = ((PXSelectBase<CAEnqFilter>) this.Filter).Current;
    Decimal valueOrDefault1 = ((Decimal?) current1?.BegBal).GetValueOrDefault();
    List<PXResult<CATranEnq.CATranExt, BAccountR>> pxResultList = new List<PXResult<CATranEnq.CATranExt, BAccountR>>();
    if (current1 != null)
    {
      bool? nullable1 = current1.ShowSummary;
      if (nullable1.GetValueOrDefault())
      {
        long? nullable2 = new long?(0L);
        int num1 = 0;
        int num2 = 0;
        using (List<object>.Enumerator enumerator = ((PXSelectBase) this.CATranListSummarized).View.Select((object[]) null, (object[]) null, new object[PXView.SortColumns.Length], PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num1, 0, ref num2).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            CADailySummary current2 = (CADailySummary) enumerator.Current;
            CATranEnq.CATranExt caTranExt1 = new CATranEnq.CATranExt();
            long? nullable3 = nullable2;
            nullable2 = nullable3.HasValue ? new long?(nullable3.GetValueOrDefault() + 1L) : new long?();
            caTranExt1.TranID = nullable2;
            caTranExt1.CashAccountID = current2.CashAccountID;
            caTranExt1.TranDate = current2.TranDate;
            CATranEnq.CATranExt caTranExt2 = caTranExt1;
            Decimal? nullable4 = current2.AmtReleasedClearedDr;
            Decimal? nullable5 = current2.AmtReleasedUnclearedDr;
            Decimal? nullable6 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
            caTranExt2.CuryDebitAmt = nullable6;
            CATranEnq.CATranExt caTranExt3 = caTranExt1;
            nullable5 = current2.AmtReleasedClearedCr;
            nullable4 = current2.AmtReleasedUnclearedCr;
            Decimal? nullable7 = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            caTranExt3.CuryCreditAmt = nullable7;
            caTranExt1.CuryClearedDebitTotalAmt = current2.AmtReleasedClearedDr;
            caTranExt1.CuryClearedCreditTotalAmt = current2.AmtReleasedClearedCr;
            nullable1 = ((PXSelectBase<CAEnqFilter>) this.Filter).Current.IncludeUnreleased;
            Decimal? nullable8;
            if (nullable1.GetValueOrDefault())
            {
              CATranEnq.CATranExt caTranExt4 = caTranExt1;
              nullable4 = caTranExt4.CuryDebitAmt;
              Decimal? unreleasedClearedDr = current2.AmtUnreleasedClearedDr;
              nullable8 = current2.AmtUnreleasedUnclearedDr;
              nullable5 = unreleasedClearedDr.HasValue & nullable8.HasValue ? new Decimal?(unreleasedClearedDr.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable9;
              if (!(nullable4.HasValue & nullable5.HasValue))
              {
                nullable8 = new Decimal?();
                nullable9 = nullable8;
              }
              else
                nullable9 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
              caTranExt4.CuryDebitAmt = nullable9;
              CATranEnq.CATranExt caTranExt5 = caTranExt1;
              nullable5 = caTranExt5.CuryCreditAmt;
              nullable8 = current2.AmtUnreleasedClearedCr;
              Decimal? unreleasedUnclearedCr = current2.AmtUnreleasedUnclearedCr;
              nullable4 = nullable8.HasValue & unreleasedUnclearedCr.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + unreleasedUnclearedCr.GetValueOrDefault()) : new Decimal?();
              caTranExt5.CuryCreditAmt = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
              CATranEnq.CATranExt caTranExt6 = caTranExt1;
              nullable4 = caTranExt6.CuryClearedDebitTotalAmt;
              nullable5 = current2.AmtUnreleasedClearedDr;
              caTranExt6.CuryClearedDebitTotalAmt = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
              CATranEnq.CATranExt caTranExt7 = caTranExt1;
              nullable5 = caTranExt7.CuryClearedCreditTotalAmt;
              nullable4 = current2.AmtUnreleasedClearedCr;
              caTranExt7.CuryClearedCreditTotalAmt = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            }
            caTranExt1.BegBal = new Decimal?(valueOrDefault1);
            CATranEnq.CATranExt caTranExt8 = caTranExt1;
            Decimal? begBal = caTranExt1.BegBal;
            nullable8 = caTranExt1.CuryDebitAmt;
            nullable4 = begBal.HasValue & nullable8.HasValue ? new Decimal?(begBal.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
            nullable5 = caTranExt1.CuryCreditAmt;
            Decimal? nullable10;
            if (!(nullable4.HasValue & nullable5.HasValue))
            {
              nullable8 = new Decimal?();
              nullable10 = nullable8;
            }
            else
              nullable10 = new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault());
            caTranExt8.EndBal = nullable10;
            nullable5 = caTranExt1.EndBal;
            valueOrDefault1 = nullable5.Value;
            caTranExt1.DayDesc = EPCalendarFilter.CalendarTypeAttribute.GetDayName(caTranExt1.TranDate.Value.DayOfWeek);
            pxResultList.Add(new PXResult<CATranEnq.CATranExt, BAccountR>(caTranExt1, new BAccountR()));
          }
          goto label_33;
        }
      }
    }
    string[] strArray = PXView.SortColumns;
    if (PXView.MaximumRows == 1)
    {
      string[] sortColumns = PXView.SortColumns;
      if ((sortColumns != null ? (sortColumns.Length != 0 ? 1 : 0) : 0) != 0 && PXView.SortColumns[0].Equals("tranID", StringComparison.OrdinalIgnoreCase))
      {
        object[] searches = PXView.Searches;
        if ((searches != null ? (searches.Length != 0 ? 1 : 0) : 0) != 0 && PXView.Searches[0] != null)
        {
          PXCache cache = ((PXSelectBase) this.CATranListRecords).Cache;
          CATranEnq.CATranExt caTranExt = new CATranEnq.CATranExt();
          caTranExt.TranID = new long?((long) Convert.ToInt32(PXView.Searches[0]));
          object obj = cache.Locate((object) caTranExt);
          if (obj != null)
            return (IEnumerable) new object[1]{ obj };
          List<string> list = EnumerableExtensions.ToList<string>((IEnumerable<string>) PXView.SortColumns, PXView.SortColumns.Length);
          list.RemoveAt(0);
          list.Add("tranID");
          strArray = list.ToArray();
        }
      }
    }
    Dictionary<long, CAMessage> customInfo = PXLongOperation.GetCustomInfo(((PXGraph) this).UID) as Dictionary<long, CAMessage>;
    PXSelectJoin<CATranEnq.CATranExt, LeftJoin<PX.Objects.AR.ARPayment, On<PX.Objects.AR.ARPayment.docType, Equal<CATran.origTranType>, And<PX.Objects.AR.ARPayment.refNbr, Equal<CATran.origRefNbr>>>, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<CATran.referenceID>>>>, Where2<Where<Current<CAEnqFilter.includeUnreleased>, Equal<boolTrue>, Or<CATran.released, Equal<boolTrue>>>, And<CATran.cashAccountID, Equal<Current<CAEnqFilter.cashAccountID>>, And<CATran.tranDate, Between<Current<CAEnqFilter.startDate>, Current<CAEnqFilter.endDate>>>>>, OrderBy<Asc<CATran.tranDate, Asc<CATran.tranID>>>> pxSelectJoin = new PXSelectJoin<CATranEnq.CATranExt, LeftJoin<PX.Objects.AR.ARPayment, On<PX.Objects.AR.ARPayment.docType, Equal<CATran.origTranType>, And<PX.Objects.AR.ARPayment.refNbr, Equal<CATran.origRefNbr>>>, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<CATran.referenceID>>>>, Where2<Where<Current<CAEnqFilter.includeUnreleased>, Equal<boolTrue>, Or<CATran.released, Equal<boolTrue>>>, And<CATran.cashAccountID, Equal<Current<CAEnqFilter.cashAccountID>>, And<CATran.tranDate, Between<Current<CAEnqFilter.startDate>, Current<CAEnqFilter.endDate>>>>>, OrderBy<Asc<CATran.tranDate, Asc<CATran.tranID>>>>((PXGraph) this);
    int num3 = 0;
    int num4 = 0;
    foreach (PXResult<CATranEnq.CATranExt, PX.Objects.AR.ARPayment, BAccountR> pxResult in ((PXSelectBase) pxSelectJoin).View.Select((object[]) null, (object[]) null, new object[strArray.Length], strArray, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num3, 0, ref num4))
    {
      CATranEnq.CATranExt caTranExt9 = PXResult<CATranEnq.CATranExt, PX.Objects.AR.ARPayment, BAccountR>.op_Implicit(pxResult);
      PX.Objects.AR.ARPayment arPayment = PXResult<CATranEnq.CATranExt, PX.Objects.AR.ARPayment, BAccountR>.op_Implicit(pxResult);
      BAccountR baccountR = PXResult<CATranEnq.CATranExt, PX.Objects.AR.ARPayment, BAccountR>.op_Implicit(pxResult);
      caTranExt9.DayDesc = EPCalendarFilter.CalendarTypeAttribute.GetDayName(caTranExt9.TranDate.Value.DayOfWeek);
      caTranExt9.DepositNbr = arPayment.DepositNbr;
      caTranExt9.DepositType = arPayment.DepositType;
      caTranExt9.BegBal = new Decimal?(valueOrDefault1);
      CATranEnq.CATranExt caTranExt10 = caTranExt9;
      Decimal? begBal = caTranExt9.BegBal;
      Decimal? nullable11 = caTranExt9.CuryDebitAmt;
      Decimal? nullable12 = begBal.HasValue & nullable11.HasValue ? new Decimal?(begBal.GetValueOrDefault() + nullable11.GetValueOrDefault()) : new Decimal?();
      Decimal? curyCreditAmt = caTranExt9.CuryCreditAmt;
      Decimal? nullable13;
      if (!(nullable12.HasValue & curyCreditAmt.HasValue))
      {
        nullable11 = new Decimal?();
        nullable13 = nullable11;
      }
      else
        nullable13 = new Decimal?(nullable12.GetValueOrDefault() - curyCreditAmt.GetValueOrDefault());
      caTranExt10.EndBal = nullable13;
      valueOrDefault1 = caTranExt9.EndBal.Value;
      if (customInfo != null)
      {
        Dictionary<long, CAMessage> dictionary = customInfo;
        long? tranId = caTranExt9.TranID;
        long key1 = tranId.Value;
        CAMessage caMessage;
        ref CAMessage local = ref caMessage;
        if (dictionary.TryGetValue(key1, out local) && caMessage != null)
        {
          long key2 = caMessage.Key;
          tranId = caTranExt9.TranID;
          long valueOrDefault2 = tranId.GetValueOrDefault();
          if (key2 == valueOrDefault2 & tranId.HasValue)
            ((PXSelectBase) this.CATranListRecords).Cache.RaiseExceptionHandling<CATran.origRefNbr>((object) caTranExt9, (object) caTranExt9.OrigRefNbr, (Exception) new PXSetPropertyException<CATran.origRefNbr>(caMessage.Message, caMessage.ErrorLevel));
        }
      }
      pxResultList.Add(new PXResult<CATranEnq.CATranExt, BAccountR>(caTranExt9, baccountR));
    }
label_33:
    return (IEnumerable) pxResultList;
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    CashAccount current = ((PXSelectBase<CashAccount>) this.cashaccount).Current;
    if (current == null || string.IsNullOrEmpty(current.CuryRateTypeID))
      return;
    e.NewValue = (object) current.CuryRateTypeID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CAEnqFilter_StartDate_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    DateTime? RangeStart;
    this.GetRange(((PXGraph) this).Accessinfo.BusinessDate.Value, ((PXSelectBase<CASetup>) this.casetup).Current.DateRangeDefault, (int?) ((CAEnqFilter) e.Row)?.CashAccountID, out RangeStart, out DateTime? _);
    e.NewValue = (object) RangeStart;
  }

  protected virtual void CAEnqFilter_EndDate_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    DateTime? RangeEnd;
    this.GetRange(((PXGraph) this).Accessinfo.BusinessDate.Value, ((PXSelectBase<CASetup>) this.casetup).Current.DateRangeDefault, (int?) ((CAEnqFilter) e.Row)?.CashAccountID, out DateTime? _, out RangeEnd);
    e.NewValue = (object) RangeEnd;
  }

  protected virtual void CAEnqFilter_ShowSummary_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CAEnqFilter row = e.Row as CAEnqFilter;
    if (!row.ShowSummary.GetValueOrDefault())
      return;
    DateTime? nullable1 = row.LastEndDate;
    if (!nullable1.HasValue)
      return;
    row.StartDate = row.LastStartDate;
    row.EndDate = row.LastEndDate;
    CAEnqFilter caEnqFilter1 = row;
    nullable1 = new DateTime?();
    DateTime? nullable2 = nullable1;
    caEnqFilter1.LastStartDate = nullable2;
    CAEnqFilter caEnqFilter2 = row;
    nullable1 = new DateTime?();
    DateTime? nullable3 = nullable1;
    caEnqFilter2.LastEndDate = nullable3;
  }

  protected virtual void CAEnqFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CAEnqFilter row = (CAEnqFilter) e.Row;
    if (row == null)
      return;
    bool? nullable = row.ShowSummary;
    bool flag1 = !nullable.GetValueOrDefault();
    int num;
    if (((PXSelectBase<CashAccount>) this.cashaccount).Current != null)
    {
      nullable = ((PXSelectBase<CashAccount>) this.cashaccount).Current.Reconcile;
      num = nullable.Value ? 1 : 0;
    }
    else
      num = 0;
    bool flag2 = num != 0;
    PXCache cache1 = ((PXSelectBase) this.CATranListRecords).Cache;
    cache1.AllowInsert = false;
    cache1.AllowUpdate = flag1;
    cache1.AllowDelete = flag1;
    PXUIFieldAttribute.SetVisible<CATran.selected>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CATran.hold>(cache1, (object) null, false);
    PXUIFieldAttribute.SetVisible<CATran.status>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CATran.origModule>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CATran.origRefNbr>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CATran.origTranTypeUI>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CATran.extRefNbr>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CATran.batchNbr>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CATran.finPeriodID>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CATran.tranDesc>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CATran.referenceName>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CATran.reconciled>(cache1, (object) null, flag1 & flag2);
    PXUIFieldAttribute.SetVisible<CATran.clearDate>(cache1, (object) null, flag1 & flag2);
    PXUIFieldAttribute.SetVisible<CATran.cleared>(cache1, (object) null, flag1 & flag2);
    PXUIFieldAttribute.SetVisible<CATran.dayDesc>(cache1, (object) null, !flag1);
    PXUIFieldAttribute.SetVisible<CATran.dayDesc>(cache1, (object) null, !flag1);
    PXUIFieldAttribute.SetVisible<CATranEnq.CATranExt.depositNbr>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CATran.referenceID>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<BAccountR.acctName>(((PXGraph) this).Caches[typeof (BAccountR)], (object) null, flag1);
    ((PXAction) this.Clearence).SetEnabled(flag2);
    bool flag3 = PXLongOperation.GetStatus(((PXGraph) this).UID) == 1;
    ((PXAction) this.Save).SetEnabled(!flag3);
    ((PXAction) this.Release).SetEnabled(!flag3);
    ((PXAction) this.Clearence).SetEnabled(!flag3);
  }

  protected virtual void CAEnqFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CAEnqFilter row = e.Row as CAEnqFilter;
    PX.Objects.CM.CurrencyInfo current = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current;
    ((PXSelectBase) this.CATranListRecords).Cache.Clear();
    ((PXGraph) this).Caches[typeof (CADailySummary)].Clear();
    if (e.ExternalCall)
      return;
    DateTime? RangeStart;
    DateTime? RangeEnd;
    this.GetRange(((PXGraph) this).Accessinfo.BusinessDate.Value, ((PXSelectBase<CASetup>) this.casetup).Current.DateRangeDefault, row.CashAccountID, out RangeStart, out RangeEnd);
    if (row == null)
      return;
    row.StartDate = RangeStart;
    row.EndDate = RangeEnd;
  }

  protected virtual void CAEnqFilter_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CAEnqFilter row) || !row.CashAccountID.HasValue)
      return;
    ((PXSelectBase<CashAccount>) this.cashaccount).Current = (CashAccount) PXSelectorAttribute.Select<CAEnqFilter.cashAccountID>(sender, (object) row);
    sender.SetDefaultExt<CAEnqFilter.curyID>((object) row);
    if (row == null)
      return;
    bool? nullable = row.ShowSummary;
    if (nullable.GetValueOrDefault())
      return;
    bool flag = false;
    foreach (PXResult<CATranEnq.CATranExt> pxResult in PXSelectBase<CATranEnq.CATranExt, PXSelect<CATranEnq.CATranExt, Where2<Where<Required<CAEnqFilter.includeUnreleased>, Equal<boolTrue>, Or<CATran.released, Equal<boolTrue>>>, And<CATran.cashAccountID, Equal<Required<CAEnqFilter.cashAccountID>>, And<CATran.tranDate, Between<Required<CAEnqFilter.startDate>, Required<CAEnqFilter.endDate>>>>>, OrderBy<Asc<CATran.tranDate, Asc<CATran.extRefNbr, Asc<CATran.tranID>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) row.IncludeUnreleased,
      (object) row.CashAccountID,
      (object) row.StartDate,
      (object) row.EndDate
    }))
    {
      CATranEnq.CATranExt caTranExt = PXResult<CATranEnq.CATranExt>.op_Implicit(pxResult);
      nullable = caTranExt.Selected;
      if (nullable.GetValueOrDefault())
      {
        caTranExt.Selected = new bool?(false);
        ((PXSelectBase<CATranEnq.CATranExt>) this.CATranListRecords).Update(caTranExt);
        flag = true;
      }
    }
    if (!flag)
      return;
    ((PXAction) this.Save).Press();
  }

  protected virtual void CATranExt_ClearDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (((CATran) e.Row).Cleared.Value && e.NewValue == null)
      throw new PXSetPropertyException("Clear Date NOT Available;");
  }

  protected virtual void CATranExt_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CATran row = (CATran) e.Row;
    if (row == null)
      return;
    bool? nullable;
    int num1;
    if (((PXSelectBase<CashAccount>) this.cashaccount).Current != null)
    {
      nullable = ((PXSelectBase<CashAccount>) this.cashaccount).Current.Reconcile;
      num1 = nullable.Value ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    nullable = row.Reconciled;
    bool flag2 = !nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CATran.selected>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<CATran.reconciled>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CATran.cleared>(cache, (object) row, flag2 & flag1);
    PXCache pxCache = cache;
    CATran caTran = row;
    int num2;
    if (flag2 & flag1)
    {
      nullable = row.Cleared;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<CATran.clearDate>(pxCache, (object) caTran, num2 != 0);
  }

  protected virtual void CATranExt_Cleared_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    CATran row = (CATran) e.Row;
    if (row.Cleared.GetValueOrDefault())
    {
      row.ClearDate = ((PXGraph) this).Accessinfo.BusinessDate;
      bool? nullable1 = ((PXSelectBase<CAEnqFilter>) this.Filter).Current.IncludeUnreleased;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Released;
        if (!nullable1.GetValueOrDefault())
          return;
      }
      CAEnqFilter current1 = ((PXSelectBase<CAEnqFilter>) this.Filter).Current;
      Decimal? nullable2 = current1.DebitClearedTotal;
      Decimal? curyDebitAmt1 = row.CuryDebitAmt;
      current1.DebitClearedTotal = nullable2.HasValue & curyDebitAmt1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + curyDebitAmt1.GetValueOrDefault()) : new Decimal?();
      CAEnqFilter current2 = ((PXSelectBase<CAEnqFilter>) this.Filter).Current;
      Decimal? creditClearedTotal = current2.CreditClearedTotal;
      nullable2 = row.CuryCreditAmt;
      current2.CreditClearedTotal = creditClearedTotal.HasValue & nullable2.HasValue ? new Decimal?(creditClearedTotal.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      CAEnqFilter current3 = ((PXSelectBase<CAEnqFilter>) this.Filter).Current;
      nullable2 = current3.EndClearedBal;
      Decimal? curyDebitAmt2 = row.CuryDebitAmt;
      Decimal? nullable3 = row.CuryCreditAmt;
      Decimal? nullable4 = curyDebitAmt2.HasValue & nullable3.HasValue ? new Decimal?(curyDebitAmt2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable5;
      if (!(nullable2.HasValue & nullable4.HasValue))
      {
        nullable3 = new Decimal?();
        nullable5 = nullable3;
      }
      else
        nullable5 = new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault());
      current3.EndClearedBal = nullable5;
    }
    else
    {
      row.ClearDate = new DateTime?();
      bool? nullable6 = ((PXSelectBase<CAEnqFilter>) this.Filter).Current.IncludeUnreleased;
      if (!nullable6.GetValueOrDefault())
      {
        nullable6 = row.Released;
        if (!nullable6.GetValueOrDefault())
          return;
      }
      CAEnqFilter current4 = ((PXSelectBase<CAEnqFilter>) this.Filter).Current;
      Decimal? nullable7 = current4.DebitClearedTotal;
      Decimal? curyDebitAmt3 = row.CuryDebitAmt;
      current4.DebitClearedTotal = nullable7.HasValue & curyDebitAmt3.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - curyDebitAmt3.GetValueOrDefault()) : new Decimal?();
      CAEnqFilter current5 = ((PXSelectBase<CAEnqFilter>) this.Filter).Current;
      Decimal? creditClearedTotal = current5.CreditClearedTotal;
      nullable7 = row.CuryCreditAmt;
      current5.CreditClearedTotal = creditClearedTotal.HasValue & nullable7.HasValue ? new Decimal?(creditClearedTotal.GetValueOrDefault() - nullable7.GetValueOrDefault()) : new Decimal?();
      CAEnqFilter current6 = ((PXSelectBase<CAEnqFilter>) this.Filter).Current;
      nullable7 = current6.EndClearedBal;
      Decimal? curyDebitAmt4 = row.CuryDebitAmt;
      Decimal? nullable8 = row.CuryCreditAmt;
      Decimal? nullable9 = curyDebitAmt4.HasValue & nullable8.HasValue ? new Decimal?(curyDebitAmt4.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable10;
      if (!(nullable7.HasValue & nullable9.HasValue))
      {
        nullable8 = new Decimal?();
        nullable10 = nullable8;
      }
      else
        nullable10 = new Decimal?(nullable7.GetValueOrDefault() - nullable9.GetValueOrDefault());
      current6.EndClearedBal = nullable10;
    }
  }

  protected virtual void CATranExt_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    CATran row = (CATran) e.Row;
    if (row.Released.GetValueOrDefault() || row.OrigModule != "CA" || row.OrigTranType != "CAE")
      throw new PXException("The record cannot be deleted.");
    CAAdj caAdj = PXResultset<CAAdj>.op_Implicit(((PXSelectBase<CAAdj>) this.caadj_adjRefNbr).Select(new object[1]
    {
      (object) row.OrigRefNbr
    }));
    if (caAdj != null)
      ((PXSelectBase<CAAdj>) this.caadj_adjRefNbr).Delete(caAdj);
    foreach (PXResult<CASplit> pxResult in ((PXSelectBase<CASplit>) this.casplit_adjRefNbr).Select(new object[1]
    {
      (object) row.OrigRefNbr
    }))
      ((PXSelectBase<CASplit>) this.casplit_adjRefNbr).Delete(PXResult<CASplit>.op_Implicit(pxResult));
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account")]
  protected virtual void BAccountR_AcctCD_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account Name")]
  protected virtual void BAccountR_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXParent(typeof (SelectFromBase<CAAdj, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CAAdj.noteID, IBqlGuid>.IsEqual<BqlField<EPApproval.refNoteID, IBqlGuid>.FromCurrent>>))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.refNoteID> e)
  {
  }

  /// <exclude />
  [Serializable]
  public class CATranExt : CATran
  {
    protected Decimal? _CuryDebitAmt;
    protected Decimal? _CuryCreditAmt;

    [PXString(3, IsFixed = true)]
    public virtual string DepositType { get; set; }

    [PXString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "CA Deposit Nbr.", Enabled = false)]
    public virtual string DepositNbr { get; set; }

    [PXDecimal]
    [PXUIField(DisplayName = "Receipt")]
    public override Decimal? CuryDebitAmt
    {
      [PXDependsOnFields(new System.Type[] {typeof (CATran.drCr), typeof (CATran.curyTranAmt)})] get
      {
        return this._CuryDebitAmt.HasValue ? this._CuryDebitAmt : base.CuryDebitAmt;
      }
      set => this._CuryDebitAmt = value;
    }

    [PXDecimal]
    [PXUIField(DisplayName = "Disbursement")]
    public override Decimal? CuryCreditAmt
    {
      [PXDependsOnFields(new System.Type[] {typeof (CATran.drCr), typeof (CATran.curyTranAmt)})] get
      {
        return this._CuryCreditAmt.HasValue ? this._CuryCreditAmt : base.CuryCreditAmt;
      }
      set => this._CuryCreditAmt = value;
    }

    [PXDecimal]
    [PXUIField(DisplayName = "Receipt Total")]
    public virtual Decimal? CuryClearedDebitTotalAmt { get; set; }

    [PXDecimal]
    [PXUIField(DisplayName = "Disbursement Total")]
    public virtual Decimal? CuryClearedCreditTotalAmt { get; set; }

    [PXString]
    [PXDBScalar(typeof (Search<CABatchDetail.batchNbr, Where<CATran.origModule, Equal<CABatchDetail.origModule>, And<CATran.origTranType, Equal<CABatchDetail.origDocType>, And<CATran.origRefNbr, Equal<CABatchDetail.origRefNbr>>>>>))]
    [PXUIField]
    public virtual string BatchPaymentRefNbr { get; set; }

    [PXDBTimestamp]
    public override byte[] tstamp { get; set; }

    public abstract class depositType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CATranEnq.CATranExt.depositType>
    {
    }

    public abstract class depositNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CATranEnq.CATranExt.depositNbr>
    {
    }

    public abstract class curyClearedDebitTotalAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CATranEnq.CATranExt.curyClearedDebitTotalAmt>
    {
    }

    public abstract class curyClearedCreditTotalAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CATranEnq.CATranExt.curyClearedCreditTotalAmt>
    {
    }

    public abstract class batchPaymentRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CATranEnq.CATranExt.batchPaymentRefNbr>
    {
    }

    public new abstract class Tstamp : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      CATranEnq.CATranExt.Tstamp>
    {
    }
  }

  public class AddTransactionExtension : AddTransactionExtensionBase<CATranEnq, CAEnqFilter>
  {
    public PXAction<CAEnqFilter> AddDet;

    public static bool IsActive() => true;

    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      OpenPeriodAttribute.SetValidatePeriod<AddTrxFilter.finPeriodID>(((PXSelectBase) this.AddFilter).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    }

    [PXOverride]
    public virtual void Clear(System.Action base_Clear)
    {
      ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current.TranDate = new DateTime?();
      ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current.FinPeriodID = (string) null;
      ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current.CuryInfoID = new long?();
      base_Clear();
    }

    [PXUIField]
    [PXProcessButton]
    public virtual IEnumerable addDet(PXAdapter adapter)
    {
      // ISSUE: method pointer
      ((PXSelectBase<AddTrxFilter>) this.AddFilter).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CaddDet\u003Eb__4_0)), true);
      if (((PXSelectBase) this.AddFilter).View.Answer == 1 || ((PXSelectBase) this.AddFilter).View.Answer == 6)
      {
        using (new PXTimeStampScope(((PXGraph) this.Base).TimeStamp))
        {
          CATran transaction = AddTrxFilter.VerifyAndCreateTransaction((PXGraph) this.Base, this.AddFilter, (PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo, ((PXSelectBase) this.AddFilter).View.Answer == 6);
          if (transaction != null)
          {
            CATranEnq.CATranExt caTranExt = new CATranEnq.CATranExt();
            PXCache<CATran>.RestoreCopy((CATran) caTranExt, transaction);
            ((PXSelectBase<CATranEnq.CATranExt>) this.Base.CATranListRecords).Update(caTranExt);
            ((PXAction) this.Base.Save).Press();
          }
        }
        ((PXSelectBase) this.Base.CATranListRecords).Cache.Clear();
        ((PXGraph) this.Base).Caches[typeof (CADailySummary)].Clear();
        ((PXSelectBase<CAEnqFilter>) this.Base.Filter).Current.BegBal = new Decimal?();
      }
      ((PXSelectBase) this.AddFilter).Cache.Clear();
      return adapter.Get();
    }

    protected virtual void CAEnqFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
    {
      CAEnqFilter row = (CAEnqFilter) e.Row;
      if (row == null)
        return;
      int? cashAccountId1;
      if (((PXSelectBase<CashAccount>) this.Base.cashaccount).Current != null && ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current != null)
      {
        int? cashAccountId2 = ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current.CashAccountID;
        cashAccountId1 = row.CashAccountID;
        if (!(cashAccountId2.GetValueOrDefault() == cashAccountId1.GetValueOrDefault() & cashAccountId2.HasValue == cashAccountId1.HasValue))
          ((PXSelectBase) this.AddFilter).Cache.SetValueExt<AddTrxFilter.cashAccountID>((object) ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current, (object) ((PXSelectBase<CashAccount>) this.Base.cashaccount).Current.CashAccountCD);
      }
      PXAction<CAEnqFilter> addDet = this.AddDet;
      cashAccountId1 = row.CashAccountID;
      int num = cashAccountId1.HasValue ? 1 : 0;
      ((PXAction) addDet).SetEnabled(num != 0);
      ((PXSelectBase) this.AddFilter).Cache.RaiseRowSelected((object) ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current);
    }

    protected virtual void CAEnqFilter_AccountID_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      if (!(e.Row is CAEnqFilter row) || !row.CashAccountID.HasValue)
        return;
      ((PXSelectBase) this.AddFilter).Cache.SetValueExt<AddTrxFilter.cashAccountID>((object) ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current, (object) row.CashAccountID);
    }
  }
}
