// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslationHistoryMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CM;

public class TranslationHistoryMaint : PXGraph<TranslationHistoryMaint>
{
  public PXSave<TranslationHistory> Save;
  public PXCancel<TranslationHistory> Cancel;
  public PXInsert<TranslationHistory> Insert;
  public PXDelete<TranslationHistory> Delete;
  public PXFirst<TranslationHistory> First;
  public PXAction<TranslationHistory> previous;
  public PXAction<TranslationHistory> next;
  public PXLast<TranslationHistory> Last;
  public PXAction<TranslationHistory> Release;
  public PXAction<TranslationHistory> viewBatch;
  public PXSelect<TranslationHistory> TranslHistRecords;
  public PXProcessing<TranslationHistoryDetails> TranslHistDetRecords;
  public PXSetup<CMSetup> TranslationSetup;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXAction<TranslationHistory> reportsFolder;
  public PXAction<TranslationHistory> translationDetailsReport;

  [PXUIField]
  [PXPreviousButton]
  protected virtual IEnumerable Previous(PXAdapter adapter)
  {
    IEnumerator enumerator = ((PXAction) new PXPrevious<TranslationHistory>((PXGraph) this, "Prev")).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        TranslationHistory current = (TranslationHistory) enumerator.Current;
        if (((PXSelectBase) this.TranslHistRecords).Cache.GetStatus((object) current) == 2)
          return ((PXAction) this.Last).Press(adapter);
        return (IEnumerable) new object[1]
        {
          (object) current
        };
      }
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXUIField]
  [PXNextButton]
  protected virtual IEnumerable Next(PXAdapter adapter)
  {
    IEnumerator enumerator = ((PXAction) new PXNext<TranslationHistory>((PXGraph) this, nameof (Next))).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        TranslationHistory current = (TranslationHistory) enumerator.Current;
        if (((PXSelectBase) this.TranslHistRecords).Cache.GetStatus((object) current) == 2)
          return ((PXAction) this.First).Press(adapter);
        return (IEnumerable) new object[1]
        {
          (object) current
        };
      }
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    TranslationHistoryMaint.\u003C\u003Ec__DisplayClass11_0 cDisplayClass110 = new TranslationHistoryMaint.\u003C\u003Ec__DisplayClass11_0();
    PXCache cach = ((PXGraph) this).Caches[typeof (TranslationHistory)];
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.header = ((PXSelectBase<TranslationHistory>) this.TranslHistRecords).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass110.header == null || cDisplayClass110.header.Status != "U")
      return adapter.Get();
    if (PXLongOperation.Exists(((PXGraph) this).UID))
      throw new PXException("The previous operation has not been completed.");
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.graph = PXGraph.CreateInstance<TranslationHistoryMaint>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass110, __methodptr(\u003Crelease\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<TranslationHistory>) this.TranslHistRecords).Current != null && ((PXSelectBase<TranslationHistory>) this.TranslHistRecords).Current.BatchNbr != null)
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<Batch>) instance.BatchModule).Current = PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) instance.BatchModule).Search<Batch.batchNbr>((object) ((PXSelectBase<TranslationHistory>) this.TranslHistRecords).Current.BatchNbr, new object[1]
      {
        (object) "CM"
      }));
      throw new PXRedirectRequiredException((PXGraph) instance, "View Translation Batch");
    }
    return adapter.Get();
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  protected virtual IEnumerable translHistDetRecords()
  {
    TranslationHistoryMaint translationHistoryMaint = this;
    KeyValuePair<PXErrorLevel, string>?[] statuses = PXLongOperation.GetCustomInfo(((PXGraph) translationHistoryMaint).UID) as KeyValuePair<PXErrorLevel, string>?[];
    int i = 0;
    foreach (PXResult<TranslationHistoryDetails> pxResult in PXSelectBase<TranslationHistoryDetails, PXSelect<TranslationHistoryDetails, Where<TranslationHistoryDetails.referenceNbr, Equal<Current<TranslationHistory.referenceNbr>>>, OrderBy<Asc<TranslationHistoryDetails.lineNbr>>>.Config>.Select((PXGraph) translationHistoryMaint, Array.Empty<object>()))
    {
      TranslationHistoryDetails translationHistoryDetails = PXResult<TranslationHistoryDetails>.op_Implicit(pxResult);
      if (statuses != null && i < statuses.Length && statuses[i].HasValue)
        ((PXSelectBase) translationHistoryMaint.TranslHistDetRecords).Cache.RaiseExceptionHandling<TranslationHistoryDetails.accountID>((object) translationHistoryDetails, (object) translationHistoryDetails.AccountID, (Exception) new PXSetPropertyException(statuses[i].Value.Value, statuses[i].Value.Key));
      ++i;
      yield return (object) translationHistoryDetails;
    }
  }

  public TranslationHistoryMaint()
  {
    CMSetup current = ((PXSelectBase<CMSetup>) this.TranslationSetup).Current;
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      throw new Exception("Multi-Currency is not activated");
    ((PXSelectBase) this.TranslHistRecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.TranslHistDetRecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.TranslHistDetRecords).Cache.AllowDelete = true;
    ((PXAction) this.Release).SetEnabled(false);
    ((PXAction) this.reportsFolder).MenuAutoOpen = true;
    ((PXAction) this.reportsFolder).AddMenuAction((PXAction) this.translationDetailsReport);
  }

  protected virtual void UpdateGain(PXCache sender, int? BranchID, Decimal? diff)
  {
    TranslationHistoryDetails translationHistoryDetails1 = PXResultset<TranslationHistoryDetails>.op_Implicit(PXSelectBase<TranslationHistoryDetails, PXSelect<TranslationHistoryDetails, Where<TranslationHistoryDetails.referenceNbr, Equal<Current<TranslationHistory.referenceNbr>>, And<TranslationHistoryDetails.lineType, Equal<TranslationLineType.gainLoss>, And<TranslationHistoryDetails.branchID, Equal<Required<TranslationHistoryDetails.branchID>>>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) BranchID
    }));
    if (translationHistoryDetails1 == null)
      return;
    Decimal? nullable1 = diff;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      return;
    TranslationHistoryDetails copy = PXCache<TranslationHistoryDetails>.CreateCopy(translationHistoryDetails1);
    Decimal? debitAmt = copy.DebitAmt;
    Decimal num2 = 0M;
    Decimal? nullable2;
    Decimal? nullable3;
    if (debitAmt.GetValueOrDefault() > num2 & debitAmt.HasValue)
    {
      TranslationHistoryDetails translationHistoryDetails2 = copy;
      nullable2 = translationHistoryDetails2.DebitAmt;
      nullable3 = diff;
      translationHistoryDetails2.DebitAmt = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      TranslationHistoryDetails translationHistoryDetails3 = copy;
      nullable3 = translationHistoryDetails3.CreditAmt;
      nullable2 = diff;
      translationHistoryDetails3.CreditAmt = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    }
    nullable2 = copy.DebitAmt;
    Decimal num3 = 0M;
    if (nullable2.GetValueOrDefault() < num3 & nullable2.HasValue)
    {
      TranslationHistoryDetails translationHistoryDetails4 = copy;
      Decimal num4 = -1M;
      nullable2 = copy.DebitAmt;
      Decimal? nullable4;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(num4 * nullable2.GetValueOrDefault());
      translationHistoryDetails4.CreditAmt = nullable4;
    }
    else
    {
      nullable2 = copy.CreditAmt;
      Decimal num5 = 0M;
      if (nullable2.GetValueOrDefault() < num5 & nullable2.HasValue)
      {
        TranslationHistoryDetails translationHistoryDetails5 = copy;
        Decimal num6 = -1M;
        nullable2 = copy.CreditAmt;
        Decimal? nullable5;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          nullable5 = nullable3;
        }
        else
          nullable5 = new Decimal?(num6 * nullable2.GetValueOrDefault());
        translationHistoryDetails5.DebitAmt = nullable5;
      }
    }
    ((PXSelectBase<TranslationHistoryDetails>) this.TranslHistDetRecords).Update(copy);
  }

  public virtual bool UpdateControlTotal(PXCache cache, TranslationHistory hist)
  {
    int num1 = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current.RequireControlTotal.GetValueOrDefault() ? 1 : 0;
    bool flag = true;
    if (num1 == 0)
    {
      Decimal? nullable;
      if (hist.CreditTot.HasValue)
      {
        nullable = hist.CreditTot;
        Decimal num2 = 0M;
        if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
        {
          cache.SetValue<TranslationHistory.controlTot>((object) hist, (object) hist.CreditTot);
          goto label_19;
        }
      }
      nullable = hist.DebitTot;
      if (nullable.HasValue)
      {
        nullable = hist.DebitTot;
        Decimal num3 = 0M;
        if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
        {
          cache.SetValue<TranslationHistory.controlTot>((object) hist, (object) hist.DebitTot);
          goto label_19;
        }
      }
      cache.SetValue<TranslationHistory.controlTot>((object) hist, (object) 0M);
    }
    else
    {
      Decimal? nullable = hist.DebitTot;
      Decimal num4 = 0M;
      if (nullable.GetValueOrDefault() == num4 & nullable.HasValue)
      {
        nullable = hist.CreditTot;
        Decimal num5 = 0M;
        if (nullable.GetValueOrDefault() == num5 & nullable.HasValue)
          goto label_12;
      }
      nullable = hist.CreditTot;
      Decimal num6 = 0M;
      if (nullable.GetValueOrDefault() == num6 & nullable.HasValue)
      {
        cache.RaiseExceptionHandling<TranslationHistory.controlTot>((object) hist, (object) hist.ControlTot, (Exception) new PXSetPropertyException("Translation History is out of Balance"));
        flag = false;
        goto label_13;
      }
label_12:
      cache.RaiseExceptionHandling<TranslationHistory.controlTot>((object) hist, (object) hist.ControlTot, (Exception) null);
label_13:
      nullable = hist.CreditTot;
      Decimal? controlTot = hist.ControlTot;
      if (!(nullable.GetValueOrDefault() == controlTot.GetValueOrDefault() & nullable.HasValue == controlTot.HasValue))
      {
        cache.RaiseExceptionHandling<TranslationHistory.controlTot>((object) hist, (object) hist.ControlTot, (Exception) new PXSetPropertyException("Translation History is out of Balance"));
        flag = false;
      }
      else
        cache.RaiseExceptionHandling<TranslationHistory.controlTot>((object) hist, (object) hist.ControlTot, (Exception) null);
      Decimal? debitTot = hist.DebitTot;
      nullable = hist.ControlTot;
      if (!(debitTot.GetValueOrDefault() == nullable.GetValueOrDefault() & debitTot.HasValue == nullable.HasValue))
      {
        cache.RaiseExceptionHandling<TranslationHistory.controlTot>((object) hist, (object) hist.ControlTot, (Exception) new PXSetPropertyException("Translation History is out of Balance"));
        flag = false;
      }
      else
        cache.RaiseExceptionHandling<TranslationHistory.controlTot>((object) hist, (object) hist.ControlTot, (Exception) null);
    }
label_19:
    return flag;
  }

  public void CreateBatch(TranslationHistory thist, bool setError)
  {
    TranslationHistoryMaint instance = PXGraph.CreateInstance<TranslationHistoryMaint>();
    ((PXGraph) instance).Clear();
    if (thist.Released.GetValueOrDefault())
      return;
    if (PXResultset<TranslationHistory>.op_Implicit(PXSelectBase<TranslationHistory, PXSelect<TranslationHistory, Where<TranslationHistory.finPeriodID, LessEqual<Required<TranslationHistory.finPeriodID>>, And<TranslationHistory.ledgerID, Equal<Required<TranslationHistory.ledgerID>>, And<TranslationHistory.released, Equal<boolFalse>, And<TranslationHistory.referenceNbr, NotEqual<Required<TranslationHistory.referenceNbr>>>>>>, OrderBy<Desc<TranslationHistory.finPeriodID>>>.Config>.Select((PXGraph) instance, new object[3]
    {
      (object) thist.FinPeriodID,
      (object) thist.LedgerID,
      (object) thist.ReferenceNbr
    })) != null)
      throw new PXException("Translation On Previos Period Not Released");
    List<TranslationHistoryDetails> thistDetList = new List<TranslationHistoryDetails>();
    foreach (PXResult<TranslationHistoryDetails> pxResult in PXSelectBase<TranslationHistoryDetails, PXSelect<TranslationHistoryDetails, Where<TranslationHistoryDetails.referenceNbr, Equal<Required<TranslationHistory.referenceNbr>>>, OrderBy<Asc<TranslationHistoryDetails.lineNbr, Asc<TranslationHistoryDetails.referenceNbr, Asc<TranslationHistoryDetails.branchID, Asc<TranslationHistoryDetails.accountID, Asc<TranslationHistoryDetails.subID, Asc<TranslationHistoryDetails.lineType>>>>>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) thist.ReferenceNbr
    }))
    {
      TranslationHistoryDetails translationHistoryDetails = PXResult<TranslationHistoryDetails>.op_Implicit(pxResult);
      thistDetList.Add(translationHistoryDetails);
    }
    if (thistDetList.Count == 0)
      throw new Exception("Nothing selected. Release not complete.");
    this.CreateBatchByHistDet(thist, thistDetList, setError);
  }

  public virtual void CreateBatchByHistDet(
    TranslationHistory thist,
    List<TranslationHistoryDetails> thistDetList,
    bool setError)
  {
    TranslationHistoryMaint instance1 = PXGraph.CreateInstance<TranslationHistoryMaint>();
    ((PXGraph) instance1).Clear();
    if (setError)
      PXLongOperation.SetCustomInfo((object) new KeyValuePair<PXErrorLevel, string>?[thistDetList.Count]);
    CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) instance1, Array.Empty<object>()));
    JournalEntry instance2 = PXGraph.CreateInstance<JournalEntry>();
    ((PXGraph) instance2).Clear();
    PX.Objects.GL.Ledger ledger = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelect<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Required<PX.Objects.GL.Ledger.ledgerID>>>>.Config>.Select((PXGraph) instance1, new object[1]
    {
      (object) thist.LedgerID
    }));
    CurrencyInfo currencyInfo1 = ((PXSelectBase<CurrencyInfo>) instance2.currencyinfo).Insert(new CurrencyInfo()
    {
      CuryID = ledger.BaseCuryID,
      BaseCuryID = ledger.BaseCuryID,
      CuryEffDate = thist.CuryEffDate,
      CuryRateTypeID = (string) null,
      CuryRate = new Decimal?(1M)
    });
    Batch Row = new Batch()
    {
      BranchID = thist.BranchID,
      LedgerID = ledger.LedgerID,
      Module = "CM",
      Status = "U",
      Released = new bool?(true),
      Hold = new bool?(false),
      CuryID = ledger.BaseCuryID,
      CuryInfoID = currencyInfo1.CuryInfoID,
      DateEntered = thist.DateEntered,
      FinPeriodID = thist.FinPeriodID
    };
    Row.CuryID = ledger.BaseCuryID;
    ((PXCache) GraphHelper.Caches<Batch>((PXGraph) instance2)).GetAttributes<Batch.finPeriodID>().OfType<OpenPeriodAttribute>().Single<OpenPeriodAttribute>().IsValidPeriod((PXCache) GraphHelper.Caches<Batch>((PXGraph) instance2), (object) Row, (object) Row.FinPeriodID);
    Batch b = ((PXSelectBase<Batch>) instance2.BatchModule).Insert(Row);
    bool flag = true;
    Exception exception = (Exception) null;
    this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) thistDetList.Where<TranslationHistoryDetails>((Func<TranslationHistoryDetails, bool>) (x => !x.Released.GetValueOrDefault())));
    for (int index = 0; index < thistDetList.Count; ++index)
    {
      TranslationHistoryDetails thistDet = thistDetList[index];
      if (!thistDet.Released.GetValueOrDefault())
      {
        try
        {
          CurrencyInfo currencyInfo2 = ((PXSelectBase<CurrencyInfo>) instance2.currencyinfo).Insert(new CurrencyInfo()
          {
            CuryID = ledger.BaseCuryID,
            BaseCuryID = ledger.BaseCuryID,
            CuryEffDate = thistDet.CuryEffDate,
            CuryRateTypeID = thistDet.RateTypeID,
            CuryMultDiv = thistDet.CuryMultDiv,
            CuryRate = new Decimal?(1M)
          });
          GLTran glTran = ((PXSelectBase<GLTran>) instance2.GLTranModuleBatNbr).Insert(new GLTran()
          {
            BranchID = thistDet.BranchID,
            AccountID = thistDet.AccountID,
            SubID = thistDet.SubID,
            CuryInfoID = currencyInfo2.CuryInfoID,
            CuryCreditAmt = thistDet.CreditAmt,
            CuryDebitAmt = thistDet.DebitAmt,
            CreditAmt = thistDet.CreditAmt,
            DebitAmt = thistDet.DebitAmt,
            TranType = "TRN",
            TranClass = thistDet.LineType,
            TranDate = thist.DateEntered,
            TranDesc = thist.Description,
            RefNbr = thistDet.ReferenceNbr,
            Released = new bool?(true)
          });
          if (glTran != null)
          {
            thistDet.LineNbr = glTran.LineNbr;
            ((PXSelectBase<TranslationHistoryDetails>) instance1.TranslHistDetRecords).Update(thistDet);
          }
        }
        catch (Exception ex)
        {
          if (setError)
            PXProcessing<TranslationHistoryDetails>.SetError(index, ex);
          flag = false;
          exception = ex;
        }
      }
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (!flag)
        throw exception;
      ((PXAction) instance2.Save).Press();
      for (int index = 0; index < thistDetList.Count; ++index)
      {
        TranslationHistoryDetails thistDet = thistDetList[index];
        try
        {
          thistDet.BatchNbr = b.BatchNbr;
          thistDet.Released = new bool?(true);
          ((PXSelectBase<TranslationHistoryDetails>) instance1.TranslHistDetRecords).Update(thistDet);
          if (setError)
            PXProcessing<TranslationHistoryDetails>.SetInfo(index, "GLTran successfully created");
        }
        catch (Exception ex)
        {
          if (setError)
            PXProcessing<TranslationHistoryDetails>.SetError(index, ex);
          flag = false;
        }
      }
      if (flag)
      {
        thist.Released = new bool?(true);
        thist.Status = "R";
        thist.BatchNbr = b.BatchNbr;
        ((PXSelectBase<TranslationHistory>) instance1.TranslHistRecords).Update(thist);
        ((PXAction) instance1.Save).Press();
      }
      transactionScope.Complete();
    }
    if (!flag || !cmSetup.AutoPostOption.GetValueOrDefault())
      return;
    PostGraph instance3 = PXGraph.CreateInstance<PostGraph>();
    ((PXGraph) instance3).TimeStamp = b.tstamp;
    instance3.PostBatchProc(b);
  }

  protected virtual void TranslationHistory_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    TranslationHistory row = (TranslationHistory) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<TranslationHistory.controlTot>(cache, (object) row, row.Status != "R");
    switch (row.Status)
    {
      case "U":
        ((PXAction) this.Release).SetEnabled(this.UpdateControlTotal(cache, row));
        ((PXAction) this.Save).SetEnabled(true);
        ((PXAction) this.Delete).SetEnabled(true);
        break;
      case "R":
      case "V":
        ((PXAction) this.Release).SetEnabled(false);
        ((PXAction) this.Save).SetEnabled(false);
        ((PXAction) this.Delete).SetEnabled(false);
        break;
      case "H":
        ((PXAction) this.Release).SetEnabled(false);
        ((PXAction) this.Save).SetEnabled(true);
        ((PXAction) this.Delete).SetEnabled(true);
        break;
    }
    bool flag = row.Status == "U" && !PXLongOperation.Exists(((PXGraph) this).UID);
    ((PXSelectBase) this.TranslHistDetRecords).Cache.AllowUpdate = flag;
    if (flag)
    {
      PXUIFieldAttribute.SetEnabled<TranslationHistoryDetails.creditAmt>(((PXSelectBase) this.TranslHistDetRecords).Cache, (object) null);
      PXUIFieldAttribute.SetEnabled<TranslationHistoryDetails.debitAmt>(((PXSelectBase) this.TranslHistDetRecords).Cache, (object) null);
    }
    ((PXAction) this.translationDetailsReport).SetEnabled(!string.IsNullOrEmpty(row.ReferenceNbr));
    ((PXSelectBase) this.TranslHistDetRecords).AllowDelete = row.Status != "R";
  }

  protected virtual void TranslationHistory_LedgerID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    TranslationHistory row = (TranslationHistory) e.Row;
    if (row == null || !row.LedgerID.HasValue)
      return;
    PX.Objects.GL.Ledger ledger = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelect<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Required<PX.Objects.GL.Ledger.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.LedgerID
    }));
    row.DestCuryID = ledger.BaseCuryID;
  }

  protected virtual void TranslationHistory_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    TranslationHistory row = (TranslationHistory) e.Row;
    PXDBOperation pxdbOperation = (PXDBOperation) (e.Operation & 3);
    if (pxdbOperation != 2)
    {
      if (pxdbOperation == 3 && row.Status != "U")
        throw new PXException("Released or voided transaction can not be deleted!");
    }
    else
    {
      if (!(((PXGraph) this).Accessinfo.ScreenID == "CM.30.40.00"))
        return;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void TranslationHistory_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    TranslationHistory row = (TranslationHistory) e.Row;
    this.UpdateControlTotal(cache, row);
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  protected virtual void TranslationHistoryDetails_DebitAmt_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    TranslationHistoryDetails row = (TranslationHistoryDetails) e.Row;
    if (!(((PXGraph) this).Accessinfo.ScreenID == "CM.30.40.00"))
      return;
    row.CreditAmt = new Decimal?(0M);
  }

  protected virtual void TranslationHistoryDetails_CreditAmt_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    TranslationHistoryDetails row = (TranslationHistoryDetails) e.Row;
    if (!(((PXGraph) this).Accessinfo.ScreenID == "CM.30.40.00"))
      return;
    row.DebitAmt = new Decimal?(0M);
  }

  protected virtual void TranslationHistoryDetails_RowUpdated(
    PXCache sender,
    PXRowUpdatedEventArgs e)
  {
    TranslationHistoryDetails row = (TranslationHistoryDetails) e.Row;
    TranslationHistoryDetails oldRow = (TranslationHistoryDetails) e.OldRow;
    if (!(row.LineType != "G"))
      return;
    Decimal? debitAmt1 = row.DebitAmt;
    Decimal? nullable1 = row.CreditAmt;
    Decimal? nullable2 = debitAmt1.HasValue & nullable1.HasValue ? new Decimal?(debitAmt1.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? debitAmt2 = oldRow.DebitAmt;
    Decimal? nullable3;
    if (!(nullable2.HasValue & debitAmt2.HasValue))
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = new Decimal?(nullable2.GetValueOrDefault() - debitAmt2.GetValueOrDefault());
    Decimal? nullable4 = nullable3;
    Decimal? creditAmt = oldRow.CreditAmt;
    Decimal? diff = nullable4.HasValue & creditAmt.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + creditAmt.GetValueOrDefault()) : new Decimal?();
    this.UpdateGain(sender, row.BranchID, diff);
  }

  protected virtual void TranslationHistoryDetails_RowDeleted(
    PXCache sender,
    PXRowDeletedEventArgs e)
  {
    TranslationHistoryDetails row = (TranslationHistoryDetails) e.Row;
    if (!(row.LineType != "G"))
      return;
    Decimal? nullable1 = row.DebitAmt;
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? creditAmt = row.CreditAmt;
    Decimal? nullable3;
    if (!(nullable2.HasValue & creditAmt.HasValue))
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = new Decimal?(nullable2.GetValueOrDefault() + creditAmt.GetValueOrDefault());
    Decimal? diff = nullable3;
    this.UpdateGain(sender, row.BranchID, diff);
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Reportsfolder(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  public virtual IEnumerable TranslationDetailsReport(PXAdapter adapter)
  {
    if (((PXSelectBase<TranslationHistory>) this.TranslHistRecords).Current != null && ((PXSelectBase<TranslationHistory>) this.TranslHistRecords).Current.ReferenceNbr != null)
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["ReferenceNbr"] = ((PXSelectBase<TranslationHistory>) this.TranslHistRecords).Current.ReferenceNbr,
        ["PeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(((PXSelectBase<TranslationHistory>) this.TranslHistRecords).Current.FinPeriodID)
      }, "CM651500", "Translation Details", (CurrentLocalization) null);
    return adapter.Get();
  }
}
