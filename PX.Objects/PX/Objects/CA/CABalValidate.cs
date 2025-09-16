// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABalValidate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.CA;

[TableAndChartDashboardType]
public class CABalValidate : PXGraph<
#nullable disable
CABalValidate>
{
  public PXCancel<CABalValidate.CABalanceValidationPeriodFilter> Cancel;
  public PXFilter<CABalValidate.CABalanceValidationPeriodFilter> PeriodsFilter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<CashAccount, CABalValidate.CABalanceValidationPeriodFilter, Where<CashAccount.active, Equal<boolTrue>>> CABalValidateList;
  public PXSetup<PX.Objects.CA.CASetup> CASetup;

  public CABalValidate()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABalValidate.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new CABalValidate.\u003C\u003Ec__DisplayClass1_0();
    PX.Objects.CA.CASetup current = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.filter = ((PXSelectBase<CABalValidate.CABalanceValidationPeriodFilter>) this.PeriodsFilter).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<CashAccount>) this.CABalValidateList).SetProcessDelegate<CATranEntryLight>(new PXProcessingBase<CashAccount>.ProcessItemDelegate<CATranEntryLight>((object) cDisplayClass10, __methodptr(\u003C\u002Ector\u003Eb__0)));
    ((PXProcessing<CashAccount>) this.CABalValidateList).SetProcessCaption("Process");
    ((PXProcessing<CashAccount>) this.CABalValidateList).SetProcessAllCaption("Process All");
    ((PXProcessing<CashAccount>) this.CABalValidateList).SetProcessTooltip("Recalculate account balances based on cash entries");
    ((PXProcessing<CashAccount>) this.CABalValidateList).SetProcessAllTooltip("Recalculate account balances based on cash entries");
    ((PXProcessingBase<CashAccount>) this.CABalValidateList).SuppressMerge = true;
    ((PXProcessingBase<CashAccount>) this.CABalValidateList).SuppressUpdate = true;
    PXUIFieldAttribute.SetEnabled<CashAccount.selected>(((PXSelectBase) this.CABalValidateList).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<CashAccount.cashAccountCD>(((PXSelectBase) this.CABalValidateList).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CashAccount.descr>(((PXSelectBase) this.CABalValidateList).Cache, (object) null, false);
  }

  public virtual void CABalanceValidationPeriodFilter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    bool flag = PXUIFieldAttribute.GetErrors(sender, (object) null, new PXErrorLevel[2]
    {
      (PXErrorLevel) 4,
      (PXErrorLevel) 5
    }).Count > 0;
    ((PXProcessing<CashAccount>) this.CABalValidateList).SetProcessEnabled(!flag);
    ((PXProcessing<CashAccount>) this.CABalValidateList).SetProcessAllEnabled(!flag);
  }

  private static void Validate(
    CATranEntryLight graph,
    CashAccount cashAccount,
    CABalValidate.CABalanceValidationPeriodFilter filter)
  {
    if (string.IsNullOrEmpty(filter.FinPeriodID))
      throw new PXException("You must fill in the Fin. Period box to perform validation.");
    MasterFinPeriod period = PXResultset<MasterFinPeriod>.op_Implicit(PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod, Where<MasterFinPeriod.finPeriodID, Equal<Required<MasterFinPeriod.finPeriodID>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) filter.FinPeriodID
    }));
    if (period == null)
      throw new PXException("You must fill in the Fin. Period box to perform validation.");
    CABalValidate.ValidateAccount(graph, cashAccount);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateReleasedDocumentsWithGL(graph, cashAccount, period);
    CABalValidate.ValidateReleasedDocuments(graph, cashAccount, period);
    CABalValidate.ValidateUnreleasedDocuments(graph, cashAccount, period);
    CABalValidate.ValidateCleared(graph, cashAccount, (IFinPeriod) period);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateCADailySummary(graph, cashAccount, (IFinPeriod) period);
  }

  private static void ValidateUnreleasedDocuments(
    CATranEntryLight graph,
    CashAccount cashAccount,
    MasterFinPeriod period)
  {
    CABalValidate.ValidateCAAdjustments(graph, cashAccount, (IFinPeriod) period, false);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateCATransfers(graph, cashAccount, (IFinPeriod) period, false);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateCAExpences(graph, cashAccount, (IFinPeriod) period, false);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateCADeposits(graph, cashAccount, (IFinPeriod) period, false);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateCADepositDetails(graph, cashAccount, (IFinPeriod) period, false);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateARPayments(graph, cashAccount, (IFinPeriod) period, false);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateAPPayments(graph, cashAccount, (IFinPeriod) period, false);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateARPaymentChargeTranCharges(graph, cashAccount, (IFinPeriod) period, false);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateAPPaymentChargeTranCharges(graph, cashAccount, (IFinPeriod) period, false);
    ((PXGraph) graph).Clear();
  }

  private static void ValidateReleasedDocumentsWithGL(
    CATranEntryLight graph,
    CashAccount cashAccount,
    MasterFinPeriod period)
  {
    CABalValidate.ValidateCATrans(graph, cashAccount, (IFinPeriod) period);
    ((PXGraph) graph).Clear();
  }

  private static void ValidateReleasedDocuments(
    CATranEntryLight graph,
    CashAccount cashAccount,
    MasterFinPeriod period)
  {
    CABalValidate.ValidateCAAdjustments(graph, cashAccount, (IFinPeriod) period, true);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateCATransfers(graph, cashAccount, (IFinPeriod) period, true);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateCAExpences(graph, cashAccount, (IFinPeriod) period, true);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateCADeposits(graph, cashAccount, (IFinPeriod) period, true);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateCADepositDetails(graph, cashAccount, (IFinPeriod) period, true);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateARPayments(graph, cashAccount, (IFinPeriod) period, true);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateAPPayments(graph, cashAccount, (IFinPeriod) period, true);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateARPaymentChargeTranCharges(graph, cashAccount, (IFinPeriod) period, true);
    ((PXGraph) graph).Clear();
    CABalValidate.ValidateAPPaymentChargeTranCharges(graph, cashAccount, (IFinPeriod) period, true);
    ((PXGraph) graph).Clear();
  }

  private static void ValidateCADailySummary(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period)
  {
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        graph.dailycache.Clear();
        transactionScope.Complete((PXGraph) graph);
      }
    }
    PXDatabase.Delete<CADailySummary>(new PXDataFieldRestrict[2]
    {
      new PXDataFieldRestrict("CashAccountID", (PXDbType) 8, new int?(4), (object) cashAccount.CashAccountID, (PXComp) 0),
      new PXDataFieldRestrict("TranDate", (PXDbType) 4, new int?(8), (object) period.StartDate, (PXComp) 3)
    });
    foreach (PXResult<CATran> pxResult in PXSelectBase<CATran, PXSelect<CATran, Where<CATran.cashAccountID, Equal<Required<CATran.cashAccountID>>, And<CATran.tranDate, GreaterEqual<Required<CATran.tranDate>>>>>.Config>.Select((PXGraph) graph, new object[2]
    {
      (object) cashAccount.CashAccountID,
      (object) period.StartDate
    }))
    {
      CATran data = PXResult<CATran>.op_Implicit(pxResult);
      CADailyAccumulatorAttribute.RowInserted<CATran.tranDate>(graph.catrancache, (object) data);
    }
    graph.dailycache.Persist((PXDBOperation) 2);
    graph.dailycache.Persist((PXDBOperation) 1);
    graph.dailycache.Persisted(false);
  }

  private static void ValidateCATrans(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period)
  {
    PXDBDefaultAttribute.SetDefaultForUpdate<PX.Objects.GL.GLTran.module>(((PXGraph) graph).Caches[typeof (PX.Objects.GL.GLTran)], (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<PX.Objects.GL.GLTran.batchNbr>(((PXGraph) graph).Caches[typeof (PX.Objects.GL.GLTran)], (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<PX.Objects.GL.GLTran.ledgerID>(((PXGraph) graph).Caches[typeof (PX.Objects.GL.GLTran)], (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<PX.Objects.GL.GLTran.finPeriodID>(((PXGraph) graph).Caches[typeof (PX.Objects.GL.GLTran)], (object) null, false);
    using (new PXConnectionScope())
    {
      bool noMoreTran = false;
      int? nullable1 = new int?();
      int num = 0;
      while (!noMoreTran)
      {
        noMoreTran = true;
        int countRows = 0;
        int? lastGLTranID = new int?();
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          CABalValidate.ProcessGLTransactions(graph, new Func<CATranEntryLight, CashAccount, IFinPeriod, int, PXResultset<PX.Objects.GL.GLTran>>(CABalValidate.SelectGLTranWhereCATraIDIsNullOrCATranIDIsNotFound), cashAccount, period, 10000, ref noMoreTran, ref countRows, ref lastGLTranID);
          if (!noMoreTran && countRows == num)
          {
            int? nullable2 = lastGLTranID;
            int? nullable3 = nullable1;
            if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
              throw new PXException("The process cannot be completed. Please contact support service.");
          }
          num = countRows;
          nullable1 = lastGLTranID;
          graph.gltrancache.ClearQueryCache();
          graph.gltrancache.Persist((PXDBOperation) 1);
          graph.gltrancache.Clear();
          graph.catrancache.Clear();
          graph.catrancache.ClearQueryCacheObsolete();
          ((PXGraph) graph).Caches[typeof (PX.Objects.AP.APPayment)].Clear();
          ((PXGraph) graph).Caches[typeof (PX.Objects.AR.ARPayment)].Clear();
          transactionScope.Complete((PXGraph) graph);
        }
      }
      graph.gltrancache.Persisted(false);
      graph.catrancache.Persisted(false);
    }
  }

  private static void ProcessGLTransactions(
    CATranEntryLight graph,
    Func<CATranEntryLight, CashAccount, IFinPeriod, int, PXResultset<PX.Objects.GL.GLTran>> selectGLTran,
    CashAccount cashAccount,
    IFinPeriod period,
    int rowsPerCycle,
    ref bool noMoreTran,
    ref int countRows,
    ref int? lastGLTranID)
  {
    foreach (PXResult<PX.Objects.GL.GLTran, PX.Objects.GL.Ledger, PX.Objects.GL.Batch> pxResult in selectGLTran(graph, cashAccount, period, rowsPerCycle))
    {
      PX.Objects.GL.GLTran glTran = PXResult<PX.Objects.GL.GLTran, PX.Objects.GL.Ledger, PX.Objects.GL.Batch>.op_Implicit(pxResult);
      PX.Objects.GL.Batch batch = PXResult<PX.Objects.GL.GLTran, PX.Objects.GL.Ledger, PX.Objects.GL.Batch>.op_Implicit(pxResult);
      lastGLTranID = glTran.TranID;
      noMoreTran = false;
      ++countRows;
      CATran catran = GLCashTranIDAttribute.DefaultValues<PX.Objects.GL.GLTran.cATranID>(graph.gltrancache, (object) glTran);
      if (catran != null)
      {
        try
        {
          long id;
          CABalValidate.GetNewOrLocateCATran(graph, glTran, batch, catran, cashAccount, out id, out bool _);
          glTran.CATranID = new long?(id);
          graph.gltrancache.Update((object) glTran);
        }
        catch (PXException ex)
        {
          PXProcessing.SetError(((Exception) ex).Message);
        }
      }
    }
  }

  private static PXResultset<PX.Objects.GL.GLTran> SelectGLTranWhereCATraIDIsNullOrCATranIDIsNotFound(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period,
    int rowsPerCycle)
  {
    return PXSelectBase<PX.Objects.GL.GLTran, PXSelectJoin<PX.Objects.GL.GLTran, InnerJoin<PX.Objects.GL.Ledger, On<PX.Objects.GL.Ledger.ledgerID, Equal<PX.Objects.GL.GLTran.ledgerID>>, InnerJoin<PX.Objects.GL.Batch, On<PX.Objects.GL.Batch.module, Equal<PX.Objects.GL.GLTran.module>, And<PX.Objects.GL.Batch.batchNbr, Equal<PX.Objects.GL.GLTran.batchNbr>, And<PX.Objects.GL.Batch.scheduled, Equal<False>, And<PX.Objects.GL.Batch.voided, NotEqual<True>>>>>, LeftJoin<CATran, On<CATran.tranID, Equal<PX.Objects.GL.GLTran.cATranID>>>>>, Where<PX.Objects.GL.GLTran.accountID, Equal<Required<PX.Objects.GL.GLTran.accountID>>, And<PX.Objects.GL.GLTran.subID, Equal<Required<PX.Objects.GL.GLTran.subID>>, And<PX.Objects.GL.GLTran.branchID, Equal<Required<PX.Objects.GL.GLTran.branchID>>, And<PX.Objects.GL.Ledger.balanceType, Equal<LedgerBalanceType.actual>, And<PX.Objects.GL.GLTran.module, NotEqual<BatchModule.moduleCM>, And2<Where<PX.Objects.GL.GLTran.cATranID, IsNull, Or<PX.Objects.GL.GLTran.cATranID, IsNotNull, And<CATran.tranID, IsNull>>>, And<PX.Objects.GL.GLTran.tranPeriodID, GreaterEqual<Required<PX.Objects.GL.GLTran.tranPeriodID>>>>>>>>>, OrderBy<Asc<Switch<Case<Where<PX.Objects.GL.GLTran.tranType, In3<CATranType.cAVoidDeposit, CATranType.cACashDropVoidTransaction, APDocType.voidCheck, APDocType.voidQuickCheck, APDocType.voidRefund, ARDocType.voidPayment, ARDocType.voidRefund>>, True>, False>, Asc<PX.Objects.GL.GLTran.tranID>>>>.Config>.SelectWindowed((PXGraph) graph, 0, rowsPerCycle, new object[4]
    {
      (object) cashAccount.AccountID,
      (object) cashAccount.SubID,
      (object) cashAccount.BranchID,
      (object) period.FinPeriodID
    });
  }

  private static CATran GetNewOrLocateCATran(
    CATranEntryLight graph,
    PX.Objects.GL.GLTran gltran,
    PX.Objects.GL.Batch batch,
    CATran catran,
    CashAccount cashAccount,
    out long id,
    out bool newCATRan)
  {
    newCATRan = false;
    bool flag1 = graph.catrancache.Locate((object) catran) == null;
    CATran existingCATran;
    bool flag2 = !CABalValidate.TryToGetExistingCATran(graph, gltran, cashAccount.CashAccountID, out existingCATran);
    if (flag1 & flag2)
    {
      CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
      catran = (CATran) graph.catrancache.Insert((object) catran);
      newCATRan = true;
      graph.catrancache.PersistInserted((object) catran);
      id = Convert.ToInt64((object) PXDatabase.SelectIdentity());
    }
    else
    {
      if (flag1)
        catran = existingCATran;
      if (string.IsNullOrEmpty(catran.BatchNbr))
        catran.BatchNbr = gltran.BatchNbr;
      bool? posted1 = catran.Posted;
      bool? posted2 = batch.Posted;
      if (!(posted1.GetValueOrDefault() == posted2.GetValueOrDefault() & posted1.HasValue == posted2.HasValue))
        catran.Posted = batch.Posted;
      CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
      catran = (CATran) graph.catrancache.Update((object) catran);
      graph.catrancache.PersistUpdated((object) catran);
      id = catran.TranID.Value;
    }
    return catran;
  }

  private static bool TryToGetExistingCATran(
    CATranEntryLight graph,
    PX.Objects.GL.GLTran gltran,
    int? cashAccountID,
    out CATran existingCATran)
  {
    existingCATran = PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.cashAccountID, Equal<Required<CATran.cashAccountID>>, And<CATran.origModule, Equal<Required<CATran.origModule>>, And<CATran.origTranType, Equal<Required<CATran.origTranType>>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>, And<CATran.origLineNbr, Equal<Required<CATran.origLineNbr>>>>>>>>.Config>.Select((PXGraph) graph, new object[5]
    {
      (object) cashAccountID,
      (object) gltran.Module,
      (object) gltran.TranType,
      (object) gltran.RefNbr,
      (object) gltran.TranLineNbr
    }));
    CATran caTran = existingCATran;
    return caTran != null && caTran.TranID.HasValue;
  }

  private static CATran GetCATran(
    CATranEntryLight graph,
    int? cashAccountID,
    string module,
    string tranType,
    string refNbr,
    int? lineNbr)
  {
    return lineNbr.HasValue ? PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.cashAccountID, Equal<Required<CATran.cashAccountID>>, And<CATran.origModule, Equal<Required<CATran.origModule>>, And<CATran.origTranType, Equal<Required<CATran.origTranType>>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>, And<CATran.origLineNbr, Equal<Required<CATran.origLineNbr>>>>>>>>.Config>.Select((PXGraph) graph, new object[5]
    {
      (object) cashAccountID,
      (object) module,
      (object) tranType,
      (object) refNbr,
      (object) lineNbr
    })) : PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.cashAccountID, Equal<Required<CATran.cashAccountID>>, And<CATran.origModule, Equal<Required<CATran.origModule>>, And<CATran.origTranType, Equal<Required<CATran.origTranType>>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>, And<CATran.origLineNbr, IsNull>>>>>>.Config>.Select((PXGraph) graph, new object[4]
    {
      (object) cashAccountID,
      (object) module,
      (object) tranType,
      (object) refNbr
    }));
  }

  private static void ValidateCATransfers(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period,
    bool released)
  {
    using (new PXConnectionScope())
    {
      PXCache cach = ((PXGraph) graph).Caches[typeof (CATransfer)];
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<CATransfer, CATran> pxResult in PXSelectBase<CATransfer, PXSelectJoin<CATransfer, LeftJoin<CATran, On<CATran.tranID, Equal<CATransfer.tranIDIn>>>, Where<CATransfer.inAccountID, Equal<Required<CATransfer.inAccountID>>, And<CATransfer.released, Equal<Required<CATran.released>>, And<CATran.tranID, IsNull, And<CATransfer.inDate, GreaterEqual<Required<CATransfer.inDate>>>>>>>.Config>.Select((PXGraph) graph, new object[3]
        {
          (object) cashAccount.CashAccountID,
          (object) released,
          (object) period.StartDate
        }))
        {
          CATransfer data = PXResult<CATransfer, CATran>.op_Implicit(pxResult);
          if (released)
          {
            CATran caTran = CABalValidate.GetCATran(graph, cashAccount.CashAccountID, "CA", "CTI", data.TransferNbr, new int?());
            if (caTran != null && caTran.TranID.HasValue)
            {
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<CATransfer.tranIDIn>((object) data, (object) caTran.TranID);
              cach.Update((object) data);
            }
          }
          else
          {
            cach.SetValue<CATransfer.tranIDIn>((object) data, (object) null);
            cach.SetValue<CATransfer.clearedIn>((object) data, (object) false);
            if (cach.GetValue<CATransfer.clearedOut>((object) data) == null)
              cach.SetValue<CATransfer.clearedOut>((object) data, (object) false);
            CATran catran = TransferCashTranIDAttribute.DefaultValues<CATransfer.tranIDIn>(cach, (object) data);
            if (catran != null)
            {
              CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
              CATran caTran = (CATran) graph.catrancache.Insert((object) catran);
              graph.catrancache.PersistInserted((object) caTran);
              long int64 = Convert.ToInt64((object) PXDatabase.SelectIdentity());
              cach.SetValue<CATransfer.tranIDIn>((object) data, (object) int64);
              cach.Update((object) data);
            }
          }
        }
        foreach (PXResult<CATransfer, CATran> pxResult in PXSelectBase<CATransfer, PXSelectJoin<CATransfer, LeftJoin<CATran, On<CATran.tranID, Equal<CATransfer.tranIDOut>>>, Where<CATransfer.outAccountID, Equal<Required<CATransfer.outAccountID>>, And<CATransfer.released, Equal<Required<CATran.released>>, And<CATran.tranID, IsNull, And<CATransfer.outDate, GreaterEqual<Required<CATransfer.outDate>>>>>>>.Config>.Select((PXGraph) graph, new object[3]
        {
          (object) cashAccount.CashAccountID,
          (object) released,
          (object) period.StartDate
        }))
        {
          CATransfer data = PXResult<CATransfer, CATran>.op_Implicit(pxResult);
          if (released)
          {
            CATran caTran = CABalValidate.GetCATran(graph, cashAccount.CashAccountID, "CA", "CTO", data.TransferNbr, new int?());
            if (caTran != null && caTran.TranID.HasValue)
            {
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<CATransfer.tranIDOut>((object) data, (object) caTran.TranID);
              cach.Update((object) data);
            }
          }
          else
          {
            cach.SetValue<CATransfer.tranIDOut>((object) data, (object) null);
            cach.SetValue<CATransfer.clearedOut>((object) data, (object) false);
            if (cach.GetValue<CATransfer.clearedIn>((object) data) == null)
              cach.SetValue<CATransfer.clearedIn>((object) data, (object) false);
            CATran catran = TransferCashTranIDAttribute.DefaultValues<CATransfer.tranIDOut>(cach, (object) data);
            if (catran != null)
            {
              CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
              CATran caTran = (CATran) graph.catrancache.Insert((object) catran);
              graph.catrancache.PersistInserted((object) caTran);
              long int64 = Convert.ToInt64((object) PXDatabase.SelectIdentity());
              cach.SetValue<CATransfer.tranIDOut>((object) data, (object) int64);
              cach.Update((object) data);
            }
          }
        }
        cach.Persist((PXDBOperation) 1);
        transactionScope.Complete((PXGraph) graph);
      }
      cach.Persisted(false);
      graph.catrancache.Persisted(false);
    }
  }

  private static void ValidateCAAdjustments(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period,
    bool released)
  {
    using (new PXConnectionScope())
    {
      PXCache cach = ((PXGraph) graph).Caches[typeof (CAAdj)];
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<CAAdj, CATran> pxResult in PXSelectBase<CAAdj, PXSelectJoin<CAAdj, LeftJoin<CATran, On<CATran.tranID, Equal<CAAdj.tranID>>, LeftJoin<GLTranDoc, On<GLTranDoc.refNbr, Equal<CAAdj.adjRefNbr>, And<GLTranDoc.tranType, Equal<CAAdj.adjTranType>, And<GLTranDoc.tranModule, Equal<BatchModule.moduleCA>>>>>>, Where<CAAdj.cashAccountID, Equal<Required<CAAdj.cashAccountID>>, And<CAAdj.released, Equal<Required<CATran.released>>, And<CATran.tranID, IsNull, And<GLTranDoc.refNbr, IsNull, And<CAAdj.tranPeriodID, GreaterEqual<Required<CAAdj.tranPeriodID>>>>>>>>.Config>.Select((PXGraph) graph, new object[3]
        {
          (object) cashAccount.CashAccountID,
          (object) released,
          (object) period.FinPeriodID
        }))
        {
          CAAdj data = PXResult<CAAdj, CATran>.op_Implicit(pxResult);
          if (released)
          {
            CATran caTran = CABalValidate.GetCATran(graph, cashAccount.CashAccountID, "CA", data.AdjTranType, data.AdjRefNbr, new int?());
            if (caTran != null && caTran.TranID.HasValue)
            {
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<CAAdj.tranID>((object) data, (object) caTran.TranID);
              cach.Update((object) data);
            }
          }
          else
          {
            cach.SetValue<CAAdj.tranID>((object) data, (object) null);
            cach.SetValue<CAAdj.cleared>((object) data, (object) false);
            CATran catran = AdjCashTranIDAttribute.DefaultValues<CAAdj.tranID>(cach, (object) data);
            long? nullable = new long?();
            if (catran != null)
            {
              CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
              CATran caTran = (CATran) graph.catrancache.Insert((object) catran);
              graph.catrancache.PersistInserted((object) caTran);
              nullable = new long?(Convert.ToInt64((object) PXDatabase.SelectIdentity()));
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<CAAdj.tranID>((object) data, (object) nullable);
              cach.Update((object) data);
            }
          }
        }
        cach.Persist((PXDBOperation) 1);
        transactionScope.Complete((PXGraph) graph);
      }
      cach.Persisted(false);
      graph.catrancache.Persisted(false);
    }
  }

  private static void ValidateCAExpences(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period,
    bool released)
  {
    using (new PXConnectionScope())
    {
      PXCache cach = ((PXGraph) graph).Caches[typeof (CAExpense)];
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<CAExpense, CATran> pxResult in PXSelectBase<CAExpense, PXSelectJoin<CAExpense, LeftJoin<CATran, On<CATran.tranID, Equal<CAExpense.cashTranID>>>, Where<CAExpense.cashAccountID, Equal<Required<CAExpense.cashAccountID>>, And<CAExpense.released, Equal<Required<CATran.released>>, And<CATran.tranID, IsNull, And<CAExpense.tranPeriodID, GreaterEqual<Required<CAExpense.tranPeriodID>>>>>>>.Config>.Select((PXGraph) graph, new object[3]
        {
          (object) cashAccount.CashAccountID,
          (object) released,
          (object) period.FinPeriodID
        }))
        {
          CAExpense data = PXResult<CAExpense, CATran>.op_Implicit(pxResult);
          if (released)
          {
            CATran caTran = CABalValidate.GetCATran(graph, cashAccount.CashAccountID, "CA", "CTE", data.RefNbr, data.LineNbr);
            if (caTran != null && caTran.TranID.HasValue)
            {
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<CAExpense.cashTranID>((object) data, (object) caTran.TranID);
              cach.Update((object) data);
            }
          }
          else
          {
            cach.SetValue<CAExpense.cashTranID>((object) data, (object) null);
            cach.SetValue<CAExpense.cleared>((object) data, (object) false);
            CATran catran = ExpenseCashTranIDAttribute.DefaultValues<CAExpense.cashTranID>(cach, (object) data);
            long? nullable = new long?();
            if (catran != null)
            {
              CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
              CATran caTran = (CATran) graph.catrancache.Insert((object) catran);
              graph.catrancache.PersistInserted((object) caTran);
              nullable = new long?(Convert.ToInt64((object) PXDatabase.SelectIdentity()));
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<CAExpense.cashTranID>((object) data, (object) nullable);
              cach.Update((object) data);
            }
          }
        }
        cach.Persist((PXDBOperation) 1);
        transactionScope.Complete((PXGraph) graph);
      }
      cach.Persisted(false);
      graph.catrancache.Persisted(false);
    }
  }

  private static void ValidateCADeposits(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period,
    bool released)
  {
    using (new PXConnectionScope())
    {
      PXCache cach = ((PXGraph) graph).Caches[typeof (CADeposit)];
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<CADeposit, CATran> pxResult in PXSelectBase<CADeposit, PXSelectJoin<CADeposit, LeftJoin<CATran, On<CATran.tranID, Equal<CADeposit.tranID>>>, Where<CADeposit.cashAccountID, Equal<Required<CADeposit.cashAccountID>>, And<CADeposit.released, Equal<Required<CATran.released>>, And<CATran.tranID, IsNull, And<CADeposit.tranDate, GreaterEqual<Required<CADeposit.tranDate>>>>>>>.Config>.Select((PXGraph) graph, new object[3]
        {
          (object) cashAccount.CashAccountID,
          (object) released,
          (object) period.StartDate
        }))
        {
          CADeposit data = PXResult<CADeposit, CATran>.op_Implicit(pxResult);
          if (released)
          {
            CATran caTran = CABalValidate.GetCATran(graph, cashAccount.CashAccountID, "CA", data.TranType, data.RefNbr, new int?());
            if (caTran != null && caTran.TranID.HasValue)
            {
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<CADeposit.tranID>((object) data, (object) caTran.TranID);
              cach.Update((object) data);
            }
          }
          else
          {
            cach.SetValue<CADeposit.tranID>((object) data, (object) null);
            cach.SetValue<CADeposit.cleared>((object) data, (object) false);
            if (cach.GetValue<CADeposit.cleared>((object) data) == null)
              cach.SetValue<CADeposit.cleared>((object) data, (object) false);
            CATran catran = DepositTranIDAttribute.DefaultValues<CADeposit.tranID>(cach, (object) data);
            if (catran != null)
            {
              CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
              CATran caTran = (CATran) graph.catrancache.Insert((object) catran);
              graph.catrancache.PersistInserted((object) caTran);
              long int64 = Convert.ToInt64((object) PXDatabase.SelectIdentity());
              cach.SetValue<CADeposit.tranID>((object) data, (object) int64);
              cach.Update((object) data);
            }
          }
        }
        foreach (PXResult<CADeposit, CATran> pxResult in PXSelectBase<CADeposit, PXSelectJoin<CADeposit, LeftJoin<CATran, On<CATran.tranID, Equal<CADeposit.cashTranID>>>, Where<CADeposit.extraCashAccountID, Equal<Required<CADeposit.extraCashAccountID>>, And<CADeposit.released, Equal<Required<CATran.released>>, And<CATran.tranID, IsNull, And<CADeposit.tranDate, GreaterEqual<Required<CADeposit.tranDate>>>>>>>.Config>.Select((PXGraph) graph, new object[3]
        {
          (object) cashAccount.CashAccountID,
          (object) released,
          (object) period.StartDate
        }))
        {
          CADeposit data = PXResult<CADeposit, CATran>.op_Implicit(pxResult);
          if (released)
          {
            string tranType = data.TranType == "CDT" ? "CDX" : "CVX";
            CATran caTran = CABalValidate.GetCATran(graph, cashAccount.CashAccountID, "CA", tranType, data.RefNbr, new int?());
            if (caTran != null && caTran.TranID.HasValue)
            {
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<CADeposit.cashTranID>((object) data, (object) caTran.TranID);
              cach.Update((object) data);
            }
          }
          else
          {
            cach.SetValue<CADeposit.cashTranID>((object) data, (object) null);
            CATran catran = DepositCashTranIDAttribute.DefaultValues<CADeposit.cashTranID>(cach, (object) data);
            if (catran != null)
            {
              CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
              CATran caTran = (CATran) graph.catrancache.Insert((object) catran);
              graph.catrancache.PersistInserted((object) caTran);
              long int64 = Convert.ToInt64((object) PXDatabase.SelectIdentity());
              cach.SetValue<CADeposit.cashTranID>((object) data, (object) int64);
              cach.Update((object) data);
            }
          }
        }
        cach.Persist((PXDBOperation) 1);
        transactionScope.Complete((PXGraph) graph);
      }
      cach.Persisted(false);
      graph.catrancache.Persisted(false);
    }
  }

  private static void ValidateCADepositDetails(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period,
    bool released)
  {
    using (new PXConnectionScope())
    {
      PXCache cach = ((PXGraph) graph).Caches[typeof (CADepositDetail)];
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<CADepositDetail, CADeposit, CATran> pxResult in PXSelectBase<CADepositDetail, PXSelectJoin<CADepositDetail, InnerJoin<CADeposit, On<CADepositDetail.FK.CashAccountDeposit>, LeftJoin<CATran, On<CATran.tranID, Equal<CADepositDetail.tranID>>>>, Where<CADepositDetail.cashAccountID, Equal<Required<CADepositDetail.cashAccountID>>, And<CADeposit.released, Equal<Required<CATran.released>>, And<CATran.tranID, IsNull, And<CADeposit.tranPeriodID, GreaterEqual<Required<CADeposit.tranPeriodID>>>>>>>.Config>.Select((PXGraph) graph, new object[3]
        {
          (object) cashAccount.CashAccountID,
          (object) released,
          (object) period.FinPeriodID
        }))
        {
          CADepositDetail data = PXResult<CADepositDetail, CADeposit, CATran>.op_Implicit(pxResult);
          if (released)
          {
            CATran caTran = CABalValidate.GetCATran(graph, cashAccount.CashAccountID, "CA", data.TranType, data.RefNbr, data.LineNbr);
            if (caTran != null && caTran.TranID.HasValue)
            {
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<CADepositDetail.tranID>((object) data, (object) caTran.TranID);
              cach.Update((object) data);
            }
          }
          else
          {
            cach.SetValue<CADepositDetail.tranID>((object) data, (object) null);
            CATran catran = DepositDetailTranIDAttribute.DefaultValues<CADepositDetail.tranID>(cach, (object) data);
            long? nullable = new long?();
            if (catran != null)
            {
              CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
              CATran caTran = (CATran) graph.catrancache.Insert((object) catran);
              graph.catrancache.PersistInserted((object) caTran);
              nullable = new long?(Convert.ToInt64((object) PXDatabase.SelectIdentity()));
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<CADepositDetail.tranID>((object) data, (object) nullable);
              cach.Update((object) data);
            }
          }
        }
        cach.Persist((PXDBOperation) 1);
        transactionScope.Complete((PXGraph) graph);
      }
      cach.Persisted(false);
      graph.catrancache.Persisted(false);
    }
  }

  private static void ValidateARPayments(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period,
    bool released)
  {
    using (new PXConnectionScope())
    {
      PXCache cach = ((PXGraph) graph).Caches[typeof (PX.Objects.AR.ARPayment)];
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<PX.Objects.AR.ARPayment, CATran> pxResult in PXSelectBase<PX.Objects.AR.ARPayment, PXSelectJoin<PX.Objects.AR.ARPayment, LeftJoin<CATran, On<CATran.tranID, Equal<PX.Objects.AR.ARPayment.cATranID>>>, Where<PX.Objects.AR.ARPayment.cashAccountID, Equal<Required<PX.Objects.AR.ARPayment.cashAccountID>>, And<PX.Objects.AR.ARPayment.released, Equal<Required<CATran.released>>, And<CATran.tranID, IsNull, And<PX.Objects.AR.ARPayment.tranPeriodID, GreaterEqual<Required<PX.Objects.AR.ARPayment.tranPeriodID>>>>>>>.Config>.Select((PXGraph) graph, new object[3]
        {
          (object) cashAccount.CashAccountID,
          (object) released,
          (object) period.FinPeriodID
        }))
        {
          PX.Objects.AR.ARPayment data = PXResult<PX.Objects.AR.ARPayment, CATran>.op_Implicit(pxResult);
          if (released)
          {
            CATran caTran = CABalValidate.GetCATran(graph, cashAccount.CashAccountID, "AR", data.DocType, data.RefNbr, new int?());
            if (caTran != null && caTran.TranID.HasValue)
            {
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<PX.Objects.AR.ARPayment.cATranID>((object) data, (object) caTran.TranID);
              cach.Update((object) data);
            }
          }
          else
          {
            cach.SetValue<PX.Objects.AR.ARPayment.cATranID>((object) data, (object) null);
            cach.SetValue<PX.Objects.AR.ARPayment.cleared>((object) data, (object) false);
            CATran catran = !EnumerableExtensions.IsIn<string>(data.DocType, "CSL", "RCS") ? ARCashTranIDAttribute.DefaultValues<PX.Objects.AR.ARPayment.cATranID>(cach, (object) data) : ARCashSaleCashTranIDAttribute.DefaultValues<ARCashSale.cATranID>(((PXGraph) graph).Caches[typeof (ARCashSale)], (object) ARCashSale.PK.Find((PXGraph) graph, data.DocType, data.RefNbr));
            long? nullable = new long?();
            if (catran != null)
            {
              CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
              CATran caTran = (CATran) graph.catrancache.Insert((object) catran);
              graph.catrancache.PersistInserted((object) caTran);
              nullable = new long?(Convert.ToInt64((object) PXDatabase.SelectIdentity()));
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<PX.Objects.AR.ARPayment.cATranID>((object) data, (object) nullable);
              cach.Update((object) data);
            }
          }
        }
        cach.Persist((PXDBOperation) 1);
        transactionScope.Complete((PXGraph) graph);
      }
      cach.Persisted(false);
      graph.catrancache.Persisted(false);
    }
  }

  private static void ValidateAPPayments(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period,
    bool released)
  {
    using (new PXConnectionScope())
    {
      PXCache cach = ((PXGraph) graph).Caches[typeof (PX.Objects.AP.APPayment)];
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<PX.Objects.AP.APPayment, CATran> pxResult in PXSelectBase<PX.Objects.AP.APPayment, PXSelectJoin<PX.Objects.AP.APPayment, LeftJoin<CATran, On<CATran.tranID, Equal<PX.Objects.AP.APPayment.cATranID>>>, Where<PX.Objects.AP.APPayment.cashAccountID, Equal<Required<PX.Objects.AP.APPayment.cashAccountID>>, And<PX.Objects.AP.APPayment.released, Equal<Required<CATran.released>>, And<CATran.tranID, IsNull, And<PX.Objects.AP.APPayment.tranPeriodID, GreaterEqual<Required<PX.Objects.AP.APPayment.tranPeriodID>>>>>>>.Config>.Select((PXGraph) graph, new object[3]
        {
          (object) cashAccount.CashAccountID,
          (object) released,
          (object) period.FinPeriodID
        }))
        {
          PX.Objects.AP.APPayment data = PXResult<PX.Objects.AP.APPayment, CATran>.op_Implicit(pxResult);
          if (released)
          {
            CATran caTran = CABalValidate.GetCATran(graph, cashAccount.CashAccountID, "AP", data.DocType, data.RefNbr, new int?());
            if (caTran != null && caTran.TranID.HasValue)
            {
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<PX.Objects.AP.APPayment.cATranID>((object) data, (object) caTran.TranID);
              cach.Update((object) data);
            }
          }
          else
          {
            cach.SetValue<PX.Objects.AP.APPayment.cATranID>((object) data, (object) null);
            cach.SetValue<PX.Objects.AP.APPayment.cleared>((object) data, (object) false);
            CATran catran = !EnumerableExtensions.IsIn<string>(data.DocType, "QCK", "VQC") ? APCashTranIDAttribute.DefaultValues<PX.Objects.AP.APPayment.cATranID>(cach, (object) data) : APQuickCheckCashTranIDAttribute.DefaultValues<APQuickCheck.cATranID>(((PXGraph) graph).Caches[typeof (APQuickCheck)], (object) APQuickCheck.PK.Find((PXGraph) graph, data.DocType, data.RefNbr));
            long? nullable = new long?();
            if (catran != null)
            {
              CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
              CATran caTran = (CATran) graph.catrancache.Insert((object) catran);
              graph.catrancache.PersistInserted((object) caTran);
              nullable = new long?(Convert.ToInt64((object) PXDatabase.SelectIdentity()));
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<PX.Objects.AP.APPayment.cATranID>((object) data, (object) nullable);
              cach.Update((object) data);
            }
          }
        }
        cach.Persist((PXDBOperation) 1);
        transactionScope.Complete((PXGraph) graph);
      }
      cach.Persisted(false);
      graph.catrancache.Persisted(false);
    }
  }

  private static void ValidateARPaymentChargeTranCharges(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period,
    bool released)
  {
    using (new PXConnectionScope())
    {
      PXCache cach = ((PXGraph) graph).Caches[typeof (ARPaymentChargeTran)];
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<ARPaymentChargeTran, CATran> pxResult in PXSelectBase<ARPaymentChargeTran, PXSelectJoin<ARPaymentChargeTran, LeftJoin<CATran, On<CATran.tranID, Equal<ARPaymentChargeTran.cashTranID>>>, Where<ARPaymentChargeTran.cashAccountID, Equal<Required<ARPaymentChargeTran.cashAccountID>>, And<ARPaymentChargeTran.released, Equal<Required<CATran.released>>, And<CATran.tranID, IsNull, And<ARPaymentChargeTran.tranPeriodID, GreaterEqual<Required<ARPaymentChargeTran.tranPeriodID>>>>>>>.Config>.Select((PXGraph) graph, new object[3]
        {
          (object) cashAccount.CashAccountID,
          (object) released,
          (object) period.FinPeriodID
        }))
        {
          ARPaymentChargeTran data = PXResult<ARPaymentChargeTran, CATran>.op_Implicit(pxResult);
          if (released)
          {
            CATran caTran = CABalValidate.GetCATran(graph, cashAccount.CashAccountID, "AR", data.DocType, data.RefNbr, data.LineNbr);
            if (caTran != null && caTran.TranID.HasValue)
            {
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<ARPaymentChargeTran.cashTranID>((object) data, (object) caTran.TranID);
              cach.Update((object) data);
            }
          }
          else
          {
            cach.SetValue<ARPaymentChargeTran.cashTranID>((object) data, (object) null);
            cach.SetValue<ARPaymentChargeTran.cleared>((object) data, (object) false);
            CATran catran = ARPaymentChargeCashTranIDAttribute.DefaultValues<ARPaymentChargeTran.cashTranID>(cach, (object) data);
            long? nullable = new long?();
            if (catran != null)
            {
              CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
              CATran caTran = (CATran) graph.catrancache.Insert((object) catran);
              graph.catrancache.PersistInserted((object) caTran);
              nullable = new long?(Convert.ToInt64((object) PXDatabase.SelectIdentity()));
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<ARPaymentChargeTran.cashTranID>((object) data, (object) nullable);
              cach.Update((object) data);
            }
          }
        }
        cach.Persist((PXDBOperation) 1);
        transactionScope.Complete((PXGraph) graph);
      }
      cach.Persisted(false);
      graph.catrancache.Persisted(false);
    }
  }

  private static void ValidateAPPaymentChargeTranCharges(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period,
    bool released)
  {
    using (new PXConnectionScope())
    {
      PXCache cach = ((PXGraph) graph).Caches[typeof (APPaymentChargeTran)];
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<APPaymentChargeTran, CATran> pxResult in PXSelectBase<APPaymentChargeTran, PXSelectJoin<APPaymentChargeTran, LeftJoin<CATran, On<CATran.tranID, Equal<APPaymentChargeTran.cashTranID>>>, Where<APPaymentChargeTran.cashAccountID, Equal<Required<APPaymentChargeTran.cashAccountID>>, And<APPaymentChargeTran.released, Equal<Required<CATran.released>>, And<CATran.tranID, IsNull, And<APPaymentChargeTran.tranPeriodID, GreaterEqual<Required<APPaymentChargeTran.tranPeriodID>>>>>>>.Config>.Select((PXGraph) graph, new object[3]
        {
          (object) cashAccount.CashAccountID,
          (object) released,
          (object) period.FinPeriodID
        }))
        {
          APPaymentChargeTran data = PXResult<APPaymentChargeTran, CATran>.op_Implicit(pxResult);
          if (released)
          {
            CATran caTran = CABalValidate.GetCATran(graph, cashAccount.CashAccountID, "AP", data.DocType, data.RefNbr, data.LineNbr);
            if (caTran != null && caTran.TranID.HasValue)
            {
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<APPaymentChargeTran.cashTranID>((object) data, (object) caTran.TranID);
              cach.Update((object) data);
            }
          }
          else
          {
            cach.SetValue<APPaymentChargeTran.cashTranID>((object) data, (object) null);
            cach.SetValue<APPaymentChargeTran.cleared>((object) data, (object) false);
            CATran catran = APPaymentChargeCashTranIDAttribute.DefaultValues<APPaymentChargeTran.cashTranID>(cach, (object) data);
            long? nullable = new long?();
            if (catran != null)
            {
              CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran);
              CATran caTran = (CATran) graph.catrancache.Insert((object) catran);
              graph.catrancache.PersistInserted((object) caTran);
              nullable = new long?(Convert.ToInt64((object) PXDatabase.SelectIdentity()));
              ((PXGraph) graph).SelectTimeStamp();
              cach.SetValue<APPaymentChargeTran.cashTranID>((object) data, (object) nullable);
              cach.Update((object) data);
            }
          }
        }
        cach.Persist((PXDBOperation) 1);
        transactionScope.Complete((PXGraph) graph);
      }
      cach.Persisted(false);
      graph.catrancache.Persisted(false);
    }
  }

  private static void ValidateCleared(
    CATranEntryLight graph,
    CashAccount cashAccount,
    IFinPeriod period)
  {
    if (cashAccount.Reconcile.GetValueOrDefault())
      return;
    ((PXGraph) graph).Clear();
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<CATran> pxResult in PXSelectBase<CATran, PXSelect<CATran, Where<CATran.cashAccountID, Equal<Required<CATran.cashAccountID>>, And<CATran.tranPeriodID, GreaterEqual<Required<CATran.tranPeriodID>>>>>.Config>.Select((PXGraph) graph, new object[2]
        {
          (object) cashAccount.CashAccountID,
          (object) period.FinPeriodID
        }))
        {
          CATran catran = PXResult<CATran>.op_Implicit(pxResult);
          bool needToSync;
          CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran, out needToSync);
          if (needToSync)
            graph.catrancache.Update((object) catran);
        }
        graph.catrancache.Persist((PXDBOperation) 1);
        transactionScope.Complete((PXGraph) graph);
      }
      graph.catrancache.Persisted(false);
    }
  }

  private static void SyncronizeClearedAndClearDate(CashAccount cashAccount, CATran catran)
  {
    CABalValidate.SyncronizeClearedAndClearDate(cashAccount, catran, out bool _);
  }

  private static void SyncronizeClearedAndClearDate(
    CashAccount cashAccount,
    CATran catran,
    out bool needToSync)
  {
    needToSync = !cashAccount.Reconcile.GetValueOrDefault() && (!catran.Cleared.GetValueOrDefault() || !catran.TranDate.HasValue);
    if (!needToSync)
      return;
    catran.Cleared = new bool?(true);
    catran.ClearDate = catran.TranDate;
  }

  private static void ValidateAccount(CATranEntryLight graph, CashAccount cashAccount)
  {
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.isCashAccount, Equal<False>, And<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>>.Config>.Select((PXGraph) graph, new object[1]
        {
          (object) cashAccount.AccountID
        }));
        if (account != null)
        {
          account.IsCashAccount = new bool?(true);
          ((PXSelectBase<PX.Objects.GL.Account>) graph.account).Update(account);
          ((PXSelectBase) graph.account).Cache.Persist((PXDBOperation) 1);
          ((PXSelectBase) graph.account).Cache.Persisted(false);
        }
        transactionScope.Complete((PXGraph) graph);
      }
    }
  }

  [Serializable]
  public class CABalanceValidationPeriodFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [FinPeriodNonLockedSelector]
    [PXUIField(DisplayName = "Fin. Period")]
    public virtual string FinPeriodID { get; set; }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABalValidate.CABalanceValidationPeriodFilter.finPeriodID>
    {
    }
  }
}
