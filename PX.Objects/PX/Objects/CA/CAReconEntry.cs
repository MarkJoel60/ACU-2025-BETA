// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAReconEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.CA;

public class CAReconEntry : PXGraph<
#nullable disable
CAReconEntry>, ICaptionable
{
  public PXSave<CARecon> Save;
  public PXCancel<CARecon> Cancel;
  public PXInsert<CARecon> Insert;
  public PXDelete<CARecon> Delete;
  public PXFirstOrderBy<CARecon, OrderBy<Asc<CARecon.cashAccountID, Asc<CARecon.reconNbr>>>> First;
  public PXPreviousOrderBy<CARecon, OrderBy<Asc<CARecon.cashAccountID, Asc<CARecon.reconNbr>>>> Previous;
  public PXNextOrderBy<CARecon, OrderBy<Asc<CARecon.cashAccountID, Asc<CARecon.reconNbr>>>> Next;
  public PXLastOrderBy<CARecon, OrderBy<Asc<CARecon.cashAccountID, Asc<CARecon.reconNbr>>>> Last;
  public PXAutoAction<CARecon> initializeState;
  public PXAction<CARecon> putOnHold;
  public PXAction<CARecon> releaseFromHold;
  public PXWorkflowEventHandler<CARecon> OnUpdateStatus;
  public PXAction<CARecon> ToggleReconciled;
  public PXAction<CARecon> ToggleCleared;
  public PXAction<CARecon> ReconcileProcessed;
  public PXAction<CARecon> Release;
  public PXAction<CARecon> Voided;
  public PXAction<CARecon> viewDoc;
  public PXAction<CARecon> printReconciliationReport;
  public ToggleCurrency<CARecon> CurrencyView;
  [PXViewName("Reconciliation Statement")]
  public PXSelectJoin<CARecon, LeftJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CARecon.cashAccountID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<CashAccount.accountID>>, LeftJoin<PX.Objects.GL.Sub, On<PX.Objects.GL.Sub.subID, Equal<CashAccount.subID>>>>>, Where<CARecon.cashAccountID, IsNull, Or2<Match<PX.Objects.GL.Account, Current<AccessInfo.userName>>, And<Match<PX.Objects.GL.Sub, Current<AccessInfo.userName>>>>>, OrderBy<Asc<CARecon.cashAccountID, Desc<CARecon.reconDate>>>> CAReconRecords;
  public PXSelect<CARecon, Where<CARecon.cashAccountID, Equal<Current<CARecon.cashAccountID>>, And<CARecon.reconNbr, Equal<Current<CARecon.reconNbr>>>>> CurrentDocument;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<CAReconEntry.CATranExt, LeftJoin<BAccountR, On<CATran.referenceID, Equal<BAccountR.bAccountID>>>, Where<True, Equal<True>>, OrderBy<Asc<CATran.tranDate>>> CAReconTranRecords;
  public PXSelect<CAReconEntry.CATranVoiding, Where<CAReconEntry.CATranVoiding.cashAccountID, Equal<Required<CAReconEntry.CATranVoiding.cashAccountID>>, And<CAReconEntry.CATranVoiding.tranID, Equal<Required<CAReconEntry.CATranVoiding.tranID>>>>> VoidingTrans;
  public PXSelect<CABatch, Where<CABatch.batchNbr, Equal<Required<CABatch.batchNbr>>>> CABatches;
  public PXSelectJoin<CAReconEntry.CATranInBatch, InnerJoinSingleTable<PX.Objects.CA.Light.APPayment, On<PX.Objects.CA.Light.APPayment.cATranID, Equal<CAReconEntry.CATranInBatch.tranID>>, InnerJoin<CABatchDetail, On<BatchModule.moduleAP, Equal<CABatchDetail.origModule>, And<PX.Objects.CA.Light.APPayment.docType, Equal<CABatchDetail.origDocType>, And<PX.Objects.CA.Light.APPayment.refNbr, Equal<CABatchDetail.origRefNbr>>>>>>, Where<CABatchDetail.batchNbr, Equal<Required<CABatch.batchNbr>>>> TransactionsInBatch;
  public PXSelect<PX.Objects.CM.CurrencyInfo> currencyinfo;
  public PXSetup<CashAccount, Where<CashAccount.reconcile, Equal<boolTrue>>> cashaccount;
  public PXSetup<CASetup> casetup;
  public PXSetup<APSetup> apsetup;
  public PXSetup<ARSetup> arsetup;
  public CMSetupSelect CMSetup;
  public PXSelect<CASetupApproval> SetupApproval;
  [PXViewName("Approval")]
  public EPApprovalAutomation<CARecon, CARecon.approved, CARecon.rejected, CARecon.hold, CASetupApproval> Approval;
  public PXSelect<BAccountR> BaccountCache;

  public string Caption()
  {
    CARecon current = ((PXSelectBase<CARecon>) this.CAReconRecords).Current;
    if (current == null)
      return string.Empty;
    CashAccount cashAccount = PXSelectorAttribute.Select<CashAccount.cashAccountID>(((PXSelectBase) this.CAReconRecords).Cache, (object) current) as CashAccount;
    if (((PXSelectBase) this.CAReconRecords).Cache.GetStatus((object) current) == 2)
      return cashAccount != null ? $"{cashAccount.CashAccountCD} {cashAccount.Descr}" : string.Empty;
    if (cashAccount == null)
      return current.ReconNbr;
    return $"{current.ReconNbr} - {cashAccount.CashAccountCD} {cashAccount.Descr}: {current.ReconDate.Value.ToShortDateString()}";
  }

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable cancel(PXAdapter a)
  {
    CAReconEntry caReconEntry = this;
    CARecon caRecon1 = (CARecon) null;
    CARecon caRecon2 = (CARecon) null;
    object search1 = a.Searches == null || a.Searches.Length == 0 ? (object) null : a.Searches[0];
    object search2 = a.Searches == null || a.Searches.Length <= 1 ? (object) null : a.Searches[1];
    foreach (PXResult<CARecon> pxResult in ((PXAction) new PXCancel<CARecon>((PXGraph) caReconEntry, "Cancel")).Press(a))
      caRecon2 = PXResult<CARecon>.op_Implicit(pxResult);
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountCD, Equal<Required<CashAccount.cashAccountCD>>>>.Config>.Select((PXGraph) caReconEntry, new object[1]
    {
      search1
    }));
    if (cashAccount != null)
    {
      bool flag1 = caRecon2 != null && ((PXSelectBase) caReconEntry.CAReconRecords).Cache.GetStatus((object) caRecon2) == 2;
      bool flag2 = false;
      if (flag1)
      {
        Numbering numbering = PXResultset<Numbering>.op_Implicit(PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<CashAccount.reconNumberingID>>>>.Config>.Select((PXGraph) caReconEntry, new object[1]
        {
          (object) cashAccount.ReconNumberingID
        }));
        flag2 = numbering != null && numbering.UserNumbering.GetValueOrDefault();
      }
      if (search2 != null && !flag2)
        caRecon1 = (CARecon) ((PXSelectBase) new PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.reconNbr, Equal<Required<CARecon.reconNbr>>>>>((PXGraph) caReconEntry)).View.SelectSingle(new object[2]
        {
          (object) cashAccount.CashAccountID,
          search2
        });
      if (caRecon1 == null && !flag2)
        caRecon1 = (CARecon) ((PXSelectBase) new PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.reconciled, Equal<boolFalse>, And<CARecon.voided, Equal<False>>>>, OrderBy<Desc<CARecon.reconDate, Desc<CARecon.reconNbr>>>>((PXGraph) caReconEntry)).View.SelectSingle(new object[1]
        {
          (object) cashAccount.CashAccountID
        });
      if (caRecon1 == null)
      {
        if (flag1)
          caRecon1 = caRecon2;
        else
          caRecon1 = ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Insert(new CARecon()
          {
            CashAccountID = cashAccount.CashAccountID
          });
      }
      if (caRecon2 != null && caRecon1 != null)
      {
        int? cashAccountId1 = caRecon2.CashAccountID;
        int? cashAccountId2 = caRecon1.CashAccountID;
        if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue) || caRecon2.ReconNbr != caRecon1.ReconNbr)
        {
          if (((PXSelectBase) caReconEntry.CAReconRecords).Cache.GetStatus((object) caRecon2) == 2)
          {
            ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Delete(caRecon2);
            ((PXSelectBase) caReconEntry.CAReconRecords).Cache.IsDirty = false;
          }
          ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Current = caRecon1;
        }
      }
    }
    yield return (object) caRecon1;
  }

  [PXDeleteButton(ConfirmationMessage = "The current {0} record will be deleted.")]
  [PXUIField]
  protected virtual IEnumerable delete(PXAdapter a)
  {
    CAReconEntry caReconEntry = this;
    CARecon caRecon = (CARecon) null;
    object search = a.Searches == null || a.Searches.Length == 0 ? (object) null : a.Searches[0];
    foreach (PXResult<CARecon> pxResult in ((PXAction) new PXDelete<CARecon>((PXGraph) caReconEntry, "Delete")).Press(a))
      caRecon = PXResult<CARecon>.op_Implicit(pxResult);
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountCD, Equal<Required<CashAccount.cashAccountCD>>>>.Config>.Select((PXGraph) caReconEntry, new object[1]
    {
      search
    }));
    if (cashAccount != null)
      caRecon = (CARecon) ((PXSelectBase) new PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.reconciled, Equal<boolFalse>>>, OrderBy<Asc<CARecon.cashAccountID, Desc<CARecon.reconNbr>>>>((PXGraph) caReconEntry)).View.SelectSingle(new object[1]
      {
        (object) cashAccount.CashAccountID
      });
    if (caRecon == null)
    {
      caRecon = ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Insert(new CARecon()
      {
        CashAccountID = cashAccount.CashAccountID
      });
      ((PXSelectBase) caReconEntry.CAReconRecords).Cache.SetStatus((object) caRecon, (PXEntryStatus) 2);
    }
    yield return (object) caRecon;
  }

  [PXFirstButton]
  [PXUIField]
  protected virtual IEnumerable first(PXAdapter a)
  {
    CAReconEntry caReconEntry = this;
    PXLongOperation.ClearStatus(((PXGraph) caReconEntry).UID);
    CARecon caRecon1 = (CARecon) null;
    CARecon caRecon2 = (CARecon) null;
    object search1 = a.Searches == null || a.Searches.Length == 0 ? (object) null : a.Searches[0];
    object search2 = a.Searches == null || a.Searches.Length <= 1 ? (object) null : a.Searches[1];
    foreach (PXResult<CARecon> pxResult in ((PXAction) new PXFirst<CARecon>((PXGraph) caReconEntry, "First")).Press(a))
      caRecon2 = PXResult<CARecon>.op_Implicit(pxResult);
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountCD, Equal<Required<CashAccount.cashAccountCD>>>>.Config>.Select((PXGraph) caReconEntry, new object[1]
    {
      search1
    }));
    if (cashAccount != null)
    {
      if (search2 != null)
        caRecon1 = (CARecon) ((PXSelectBase) new PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>>, OrderBy<Asc<CARecon.reconDate, Asc<CARecon.reconNbr>>>>((PXGraph) caReconEntry)).View.SelectSingle(new object[1]
        {
          (object) cashAccount.CashAccountID
        });
      if (caRecon2 != null && caRecon1 != null)
      {
        int? cashAccountId1 = caRecon2.CashAccountID;
        int? cashAccountId2 = caRecon1.CashAccountID;
        if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue) || caRecon2.ReconNbr != caRecon1.ReconNbr)
        {
          if (((PXSelectBase) caReconEntry.CAReconRecords).Cache.GetStatus((object) caRecon2) == 2)
          {
            ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Delete(caRecon2);
            ((PXSelectBase) caReconEntry.CAReconRecords).Cache.IsDirty = false;
          }
          ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Current = caRecon1;
        }
      }
    }
    yield return (object) caRecon1;
  }

  [PXPreviousButton]
  [PXUIField]
  protected virtual IEnumerable previous(PXAdapter a)
  {
    CAReconEntry caReconEntry = this;
    CARecon caRecon1 = (CARecon) null;
    CARecon current = ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Current;
    CARecon caRecon2 = (CARecon) null;
    object search1 = a.Searches == null || a.Searches.Length == 0 ? (object) null : a.Searches[0];
    object search2 = a.Searches == null || a.Searches.Length <= 1 ? (object) null : a.Searches[1];
    foreach (PXResult<CARecon> pxResult in ((PXAction) new PXPrevious<CARecon>((PXGraph) caReconEntry, "Prev")).Press(a))
      caRecon2 = PXResult<CARecon>.op_Implicit(pxResult);
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountCD, Equal<Required<CashAccount.cashAccountCD>>>>.Config>.Select((PXGraph) caReconEntry, new object[1]
    {
      search1
    }));
    if (cashAccount != null)
    {
      if (search2 != null)
        caRecon1 = (CARecon) ((PXSelectBase) new PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.reconNbr, Less<Required<CARecon.reconNbr>>>>, OrderBy<Desc<CARecon.reconDate, Desc<CARecon.reconNbr>>>>((PXGraph) caReconEntry)).View.SelectSingle(new object[2]
        {
          (object) cashAccount.CashAccountID,
          search2
        });
      if (caRecon1 == null)
        caRecon1 = (CARecon) ((PXSelectBase) new PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>>, OrderBy<Desc<CARecon.reconDate, Desc<CARecon.reconNbr>>>>((PXGraph) caReconEntry)).View.SelectSingle(new object[1]
        {
          (object) cashAccount.CashAccountID
        });
      if (caRecon2 != null && caRecon1 != null)
      {
        int? cashAccountId1 = caRecon2.CashAccountID;
        int? cashAccountId2 = caRecon1.CashAccountID;
        if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue) || caRecon2.ReconNbr != caRecon1.ReconNbr)
        {
          if (((PXSelectBase) caReconEntry.CAReconRecords).Cache.GetStatus((object) caRecon2) == 2)
          {
            ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Delete(caRecon2);
            ((PXSelectBase) caReconEntry.CAReconRecords).Cache.IsDirty = false;
          }
          ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Current = caRecon1;
        }
      }
    }
    yield return (object) caRecon1;
  }

  [PXNextButton]
  [PXUIField]
  protected virtual IEnumerable next(PXAdapter a)
  {
    CAReconEntry caReconEntry = this;
    PXLongOperation.ClearStatus(((PXGraph) caReconEntry).UID);
    CARecon caRecon1 = (CARecon) null;
    CARecon caRecon2 = (CARecon) null;
    object search1 = a.Searches == null || a.Searches.Length == 0 ? (object) null : a.Searches[0];
    object search2 = a.Searches == null || a.Searches.Length <= 1 ? (object) null : a.Searches[1];
    foreach (PXResult<CARecon> pxResult in ((PXAction) new PXNext<CARecon>((PXGraph) caReconEntry, "Next")).Press(a))
      caRecon2 = PXResult<CARecon>.op_Implicit(pxResult);
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountCD, Equal<Required<CashAccount.cashAccountCD>>>>.Config>.Select((PXGraph) caReconEntry, new object[1]
    {
      search1
    }));
    if (cashAccount != null)
    {
      if (search2 != null)
        caRecon1 = (CARecon) ((PXSelectBase) new PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.reconNbr, Greater<Required<CARecon.reconNbr>>>>, OrderBy<Asc<CARecon.reconDate, Asc<CARecon.reconNbr>>>>((PXGraph) caReconEntry)).View.SelectSingle(new object[2]
        {
          (object) cashAccount.CashAccountID,
          search2
        });
      if (caRecon2 != null && caRecon1 != null)
      {
        int? cashAccountId1 = caRecon2.CashAccountID;
        int? cashAccountId2 = caRecon1.CashAccountID;
        if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue) || caRecon2.ReconNbr != caRecon1.ReconNbr)
        {
          if (((PXSelectBase) caReconEntry.CAReconRecords).Cache.GetStatus((object) caRecon2) == 2)
          {
            ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Delete(caRecon2);
            ((PXSelectBase) caReconEntry.CAReconRecords).Cache.IsDirty = false;
          }
          ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Current = caRecon1;
        }
      }
    }
    yield return (object) caRecon1;
  }

  [PXLastButton]
  [PXUIField]
  protected virtual IEnumerable last(PXAdapter a)
  {
    CAReconEntry caReconEntry = this;
    PXLongOperation.ClearStatus(((PXGraph) caReconEntry).UID);
    CARecon caRecon1 = (CARecon) null;
    CARecon caRecon2 = (CARecon) null;
    object search1 = a.Searches == null || a.Searches.Length == 0 ? (object) null : a.Searches[0];
    object search2 = a.Searches == null || a.Searches.Length <= 1 ? (object) null : a.Searches[1];
    foreach (PXResult<CARecon> pxResult in ((PXAction) new PXLast<CARecon>((PXGraph) caReconEntry, "Last")).Press(a))
      caRecon2 = PXResult<CARecon>.op_Implicit(pxResult);
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountCD, Equal<Required<CashAccount.cashAccountCD>>>>.Config>.Select((PXGraph) caReconEntry, new object[1]
    {
      search1
    }));
    if (cashAccount != null)
    {
      if (search2 != null)
        caRecon1 = (CARecon) ((PXSelectBase) new PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>>, OrderBy<Desc<CARecon.reconDate, Desc<CARecon.reconNbr>>>>((PXGraph) caReconEntry)).View.SelectSingle(new object[1]
        {
          (object) cashAccount.CashAccountID
        });
      if (caRecon2 != null && caRecon1 != null)
      {
        int? cashAccountId1 = caRecon2.CashAccountID;
        int? cashAccountId2 = caRecon1.CashAccountID;
        if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue) || caRecon2.ReconNbr != caRecon1.ReconNbr)
        {
          if (((PXSelectBase) caReconEntry.CAReconRecords).Cache.GetStatus((object) caRecon2) == 2)
          {
            ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Delete(caRecon2);
            ((PXSelectBase) caReconEntry.CAReconRecords).Cache.IsDirty = false;
          }
          ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Current = caRecon1;
        }
      }
    }
    yield return (object) caRecon1;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable toggleReconciled(PXAdapter adapter)
  {
    bool? nullable1 = new bool?();
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    foreach (CAReconEntry.CATranExt reconTran in this.GetReconTrans())
    {
      if (reconTran.Released.GetValueOrDefault())
      {
        bool? nullable2 = nullable1;
        bool? nullable3;
        if (!nullable2.HasValue)
        {
          bool? reconciled = reconTran.Reconciled;
          nullable3 = reconciled.HasValue ? new bool?(!reconciled.GetValueOrDefault()) : new bool?();
        }
        else
          nullable3 = nullable2;
        nullable1 = nullable3;
        CAReconEntry.CATranExt copy = PXCache<CAReconEntry.CATranExt>.CreateCopy(reconTran);
        copy.Reconciled = nullable1;
        try
        {
          ((PXSelectBase<CAReconEntry.CATranExt>) this.CAReconTranRecords).Update(copy);
        }
        catch (PXSetPropertyException ex)
        {
          propertyException = ex;
        }
      }
    }
    if (propertyException != null)
      throw propertyException;
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable toggleCleared(PXAdapter adapter)
  {
    bool? nullable1 = new bool?();
    foreach (CAReconEntry.CATranExt reconTran in this.GetReconTrans())
    {
      bool? nullable2 = reconTran.Released;
      if (nullable2.GetValueOrDefault())
      {
        nullable2 = reconTran.Cleared;
        if (nullable2.GetValueOrDefault())
        {
          nullable2 = reconTran.Reconciled;
          if (!nullable2.GetValueOrDefault())
          {
            nullable2 = reconTran.ProcessedFromFeed;
            if (nullable2.GetValueOrDefault())
              continue;
          }
          else
            continue;
        }
        if (!this.IsDisabledBecauseOfVoidingNotReleased(reconTran))
        {
          nullable2 = nullable1;
          bool? nullable3;
          if (!nullable2.HasValue)
          {
            bool? cleared = reconTran.Cleared;
            nullable3 = cleared.HasValue ? new bool?(!cleared.GetValueOrDefault()) : new bool?();
          }
          else
            nullable3 = nullable2;
          nullable1 = nullable3;
          CAReconEntry.CATranExt copy = PXCache<CAReconEntry.CATranExt>.CreateCopy(reconTran);
          copy.Cleared = nullable1;
          ((PXSelectBase<CAReconEntry.CATranExt>) this.CAReconTranRecords).Update(copy);
        }
      }
    }
    return adapter.Get();
  }

  private IEnumerable<CAReconEntry.CATranExt> GetReconTrans()
  {
    int num1 = 0;
    int num2 = 0;
    ((PXSelectBase<CARecon>) this.CAReconRecords).Current.SkipVoided.GetValueOrDefault();
    foreach (PXResult<CAReconEntry.CATranExt> pxResult in ((PXSelectBase) this.CAReconTranRecords).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, ((PXSelectBase) this.CAReconTranRecords).View.GetExternalFilters(), ref num1, PXView.MaximumRows, ref num2))
      yield return PXResult<CAReconEntry.CATranExt>.op_Implicit(pxResult);
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable reconcileProcessed(PXAdapter adapter)
  {
    foreach (PXResult<CAReconEntry.CATranExt> pxResult in ((PXSelectBase<CAReconEntry.CATranExt>) this.CAReconTranRecords).Select(Array.Empty<object>()))
    {
      CAReconEntry.CATranExt caTranExt = PXResult<CAReconEntry.CATranExt>.op_Implicit(pxResult);
      bool? nullable = caTranExt.ProcessedFromFeed;
      if (nullable.GetValueOrDefault())
      {
        nullable = caTranExt.Reconciled;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          ((PXSelectBase) this.CAReconTranRecords).Cache.SetValueExt<CAReconEntry.CATranExt.reconciled>((object) caTranExt, (object) true);
          ((PXSelectBase<CAReconEntry.CATranExt>) this.CAReconTranRecords).Update(caTranExt);
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable release(PXAdapter adapter)
  {
    PXCache cache = ((PXSelectBase) this.CAReconRecords).Cache;
    List<CARecon> caReconList = new List<CARecon>();
    foreach (PXResult<CARecon> pxResult in adapter.Get())
      caReconList.Add(PXResult<CARecon>.op_Implicit(pxResult));
    ((PXAction) this.Save).Press();
    foreach (CARecon caRecon in caReconList)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new CAReconEntry.\u003C\u003Ec__DisplayClass35_0()
      {
        recon = caRecon
      }, __methodptr(\u003Crelease\u003Eb__0)));
    }
    return (IEnumerable) caReconList;
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable voided(PXAdapter adapter)
  {
    CAReconEntry caReconEntry = this;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CAReconEntry.\u003C\u003Ec__DisplayClass37_0 cDisplayClass370 = new CAReconEntry.\u003C\u003Ec__DisplayClass37_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass370.recon = ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Current;
    if (((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Ask("Void", "Are you sure you want to void the statement?", (MessageButtons) 4) == 6)
    {
      PXLongOperation.ClearStatus(((PXGraph) caReconEntry).UID);
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) caReconEntry, new PXToggleAsyncDelegate((object) cDisplayClass370, __methodptr(\u003Cvoided\u003Eb__0)));
      // ISSUE: reference to a compiler-generated field
      yield return (object) cDisplayClass370.recon;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      yield return (object) cDisplayClass370.recon;
    }
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDoc(PXAdapter adapter)
  {
    CATran current = (CATran) ((PXSelectBase<CAReconEntry.CATranExt>) this.CAReconTranRecords).Current;
    ((PXSelectBase<CAReconEntry.CATranExt>) this.CAReconTranRecords).Current = (CAReconEntry.CATranExt) null;
    PXSelectBase<CATran, PXSelect<CATran, Where<CATran.cashAccountID, Equal<Current<CARecon.cashAccountID>>, And<Where<CATran.reconNbr, Equal<Current<CARecon.reconNbr>>, Or<Current<CARecon.reconciled>, Equal<boolFalse>, And<CATran.reconNbr, IsNull, And<Where<CATran.tranDate, LessEqual<Current<CARecon.loadDocumentsTill>>, Or<Current<CARecon.loadDocumentsTill>, IsNull>>>>>>>>>.Config>.Clear((PXGraph) this);
    CATran.Redirect(((PXSelectBase) this.CAReconRecords).Cache, current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable PrintReconciliationReport(PXAdapter adapter, string reportID = null)
  {
    reportID = reportID ?? "CA627000";
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    foreach (CARecon caRecon in adapter.Get<CARecon>())
    {
      CashAccount cashAccount = CashAccount.PK.Find((PXGraph) this, caRecon.CashAccountID);
      dictionary["CashAccount"] = cashAccount?.CashAccountCD;
      dictionary["ReconNbr"] = caRecon.ReconNbr;
      dictionary["ShowReconciledTransactions"] = (!caRecon.Reconciled.GetValueOrDefault()).ToString();
    }
    throw new PXReportRequiredException(dictionary, reportID, "Report " + reportID, (CurrentLocalization) null);
  }

  private bool SearchInCache { get; set; } = true;

  protected virtual IEnumerable carecontranrecords()
  {
    CAReconEntry caReconEntry = this;
    if (((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Current != null)
    {
      bool? nullable1 = ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Current.Voided;
      if (!nullable1.GetValueOrDefault())
      {
        CARecon doc = ((PXSelectBase<CARecon>) caReconEntry.CAReconRecords).Current;
        if (PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CARecon.cashAccountID>>>>.Config>.Select((PXGraph) caReconEntry, new object[1]
        {
          (object) doc.CashAccountID
        })) != null)
        {
          HashSet<long> catrans = new HashSet<long>();
          nullable1 = doc.SkipVoided;
          bool skipVoids = nullable1.GetValueOrDefault();
          nullable1 = doc.ShowBatchPayments;
          long? nullable2;
          if (nullable1.GetValueOrDefault())
          {
            foreach (CAReconEntry.CATranExt batchTransaction in caReconEntry.GetBatchTransactions(catrans))
            {
              PXCache cach = ((PXGraph) caReconEntry).Caches[typeof (CAReconEntry.CATranExt)];
              CAReconEntry.CATranExt caTranExt1 = (CAReconEntry.CATranExt) null;
              foreach (CAReconEntry.CATranExt caTranExt2 in cach.Cached)
              {
                if ((caTranExt2.OrigModule == "AP" || caTranExt2.OrigModule == "PR") && caTranExt2.OrigTranType == "CBT" && caTranExt2.OrigRefNbr == batchTransaction.OrigRefNbr)
                {
                  GraphHelper.Hold(cach, (object) caTranExt2);
                  caTranExt1 = caTranExt2;
                  break;
                }
              }
              if (caTranExt1 == null)
              {
                caTranExt1 = batchTransaction;
                object obj = (object) null;
                cach.RaiseFieldDefaulting<CATran.tranID>((object) caTranExt1, ref obj);
                caTranExt1.TranID = (long?) obj;
                cach.SetStatus((object) caTranExt1, (PXEntryStatus) 5);
              }
              HashSet<long> longSet1 = catrans;
              nullable2 = caTranExt1.TranID;
              long num1 = nullable2.Value;
              if (!longSet1.Contains(num1))
              {
                HashSet<long> longSet2 = catrans;
                nullable2 = caTranExt1.TranID;
                long num2 = nullable2.Value;
                longSet2.Add(num2);
                yield return (object) new PXResult<CAReconEntry.CATranExt>(caTranExt1);
              }
            }
          }
          IEnumerable<\u003C\u003Ef__AnonymousType20<CAReconEntry.CATranExt, BAccountR>> source = ((IEnumerable<PXResult<CAReconEntry.CATranExt>>) PXSelectBase<CAReconEntry.CATranExt, PXSelectJoin<CAReconEntry.CATranExt, LeftJoin<BAccountR, On<CATran.referenceID, Equal<BAccountR.bAccountID>>>, Where<CAReconEntry.CATranExt.cashAccountID, Equal<Current<CARecon.cashAccountID>>, And<Where<CAReconEntry.CATranExt.reconNbr, Equal<Current<CARecon.reconNbr>>, Or<Current<CARecon.reconciled>, Equal<boolFalse>, And<CAReconEntry.CATranExt.reconNbr, IsNull, And<Where<CAReconEntry.CATranExt.tranDate, LessEqual<Current2<CARecon.loadDocumentsTill>>, Or<Current2<CARecon.loadDocumentsTill>, IsNull, Or<CAReconEntry.CATranExt.reconNbr, Equal<Current2<CARecon.reconNbr>>>>>>>>>>>>.Config>.Select((PXGraph) caReconEntry, Array.Empty<object>())).AsEnumerable<PXResult<CAReconEntry.CATranExt>>().Select(result => new
          {
            CATran = ((PXResult) result).GetItem<CAReconEntry.CATranExt>(),
            BAccount = ((PXResult) result).GetItem<BAccountR>()
          });
          if (source.Any())
          {
            Dictionary<long?, CABankTran> bankTrans;
            Dictionary<long?, CAReconEntry.CATranVoiding> voidingTrans;
            caReconEntry.SelectAdditionalTables(doc, source.Select(result => result.CATran), out bankTrans, out voidingTrans);
            foreach (var data in source)
            {
              CAReconEntry.CATranExt caTran = data.CATran;
              caTran.CuryEffTranAmt = caTran.CuryTranAmt;
              caTran.EffTranAmt = caTran.TranAmt;
              caTran.Voided = new bool?(false);
              caTran.VoidingNotReleased = new bool?(false);
              CAReconEntry.CATranExt caTranExt3 = caTran;
              nullable2 = new long?();
              long? nullable3 = nullable2;
              caTranExt3.VoidingTranID = nullable3;
              if (skipVoids)
              {
                if (!caReconEntry.Skip((CATran) caTran))
                {
                  CAReconEntry.CATranVoiding voidingTran = (CAReconEntry.CATranVoiding) null;
                  voidingTrans.TryGetValue(caTran.TranID, out voidingTran);
                  if (voidingTran != null)
                  {
                    nullable2 = voidingTran.TranID;
                    if (nullable2.HasValue)
                    {
                      nullable1 = voidingTran.Released;
                      if (nullable1.GetValueOrDefault())
                      {
                        if (caReconEntry.Skip((CATran) voidingTran, (CATran) caTran))
                        {
                          CAReconEntry.CATranExt caTranExt4 = caTran;
                          Decimal? nullable4 = caTranExt4.CuryEffTranAmt;
                          Decimal? nullable5 = voidingTran.CuryTranAmt;
                          Decimal valueOrDefault1 = nullable5.GetValueOrDefault();
                          Decimal? nullable6;
                          if (!nullable4.HasValue)
                          {
                            nullable5 = new Decimal?();
                            nullable6 = nullable5;
                          }
                          else
                            nullable6 = new Decimal?(nullable4.GetValueOrDefault() + valueOrDefault1);
                          caTranExt4.CuryEffTranAmt = nullable6;
                          CAReconEntry.CATranExt caTranExt5 = caTran;
                          nullable4 = caTranExt5.EffTranAmt;
                          nullable5 = voidingTran.TranAmt;
                          Decimal valueOrDefault2 = nullable5.GetValueOrDefault();
                          Decimal? nullable7;
                          if (!nullable4.HasValue)
                          {
                            nullable5 = new Decimal?();
                            nullable7 = nullable5;
                          }
                          else
                            nullable7 = new Decimal?(nullable4.GetValueOrDefault() + valueOrDefault2);
                          caTranExt5.EffTranAmt = nullable7;
                        }
                        caTran.Voided = new bool?(true);
                      }
                      else
                        caTran.VoidingNotReleased = new bool?(true);
                      caTran.VoidingTranID = voidingTran.TranID;
                    }
                  }
                }
                else
                  continue;
              }
              CABankTran caBankTran = (CABankTran) null;
              bankTrans.TryGetValue(caTran.TranID, out caBankTran);
              if (caBankTran != null)
              {
                nullable1 = caBankTran.Processed;
                if (nullable1.GetValueOrDefault())
                  caTran.ProcessedFromFeed = new bool?(true);
              }
              GraphHelper.Hold(((PXGraph) caReconEntry).Caches[typeof (CAReconEntry.CATranExt)], (object) caTran);
              HashSet<long> longSet3 = catrans;
              nullable2 = caTran.TranID;
              long num3 = nullable2.Value;
              if (!longSet3.Contains(num3))
              {
                HashSet<long> longSet4 = catrans;
                nullable2 = caTran.TranID;
                long num4 = nullable2.Value;
                longSet4.Add(num4);
                yield return (object) new PXResult<CAReconEntry.CATranExt, BAccountR>(caTran, data.BAccount);
              }
            }
            bankTrans = (Dictionary<long?, CABankTran>) null;
            voidingTrans = (Dictionary<long?, CAReconEntry.CATranVoiding>) null;
          }
        }
      }
    }
  }

  private void SelectAdditionalTables(
    CARecon doc,
    IEnumerable<CAReconEntry.CATranExt> transToReconcile,
    out Dictionary<long?, CABankTran> bankTrans,
    out Dictionary<long?, CAReconEntry.CATranVoiding> voidingTrans)
  {
    bankTrans = new Dictionary<long?, CABankTran>();
    voidingTrans = new Dictionary<long?, CAReconEntry.CATranVoiding>();
    foreach (IEnumerable<long?> source in transToReconcile.Select<CAReconEntry.CATranExt, long?>((Func<CAReconEntry.CATranExt, long?>) (tran => tran.TranID)).BreakInto<long?>(500))
    {
      long?[] array = source.ToArray<long?>();
      foreach (PXResult<CABankTran, CABankTranMatch> pxResult in PXSelectBase<CABankTran, PXSelectJoin<CABankTran, InnerJoin<CABankTranMatch, On<CABankTran.tranID, Equal<CABankTranMatch.tranID>>>, Where<CABankTranMatch.cATranID, In<Required<CAReconEntry.CATranExt.tranID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) array
      }))
      {
        CABankTranMatch caBankTranMatch = PXResult<CABankTran, CABankTranMatch>.op_Implicit(pxResult);
        if (!bankTrans.ContainsKey(caBankTranMatch.CATranID))
          bankTrans.Add(caBankTranMatch.CATranID, PXResult<CABankTran, CABankTranMatch>.op_Implicit(pxResult));
        else
          PXTrace.WriteError($"Error message: {"Multiple bank transactions have been matched to the same cash transaction in the system. Please contact the support service."}; Date: {DateTime.Now}; Screen: {((PXGraph) this).Accessinfo.ScreenID}; CATranID: {caBankTranMatch.CATranID};");
      }
      if (doc.SkipVoided.GetValueOrDefault())
        EnumerableExtensions.AddRange<long?, CAReconEntry.CATranVoiding>((IDictionary<long?, CAReconEntry.CATranVoiding>) voidingTrans, (IEnumerable<KeyValuePair<long?, CAReconEntry.CATranVoiding>>) ((IEnumerable<PXResult<CAReconEntry.CATranVoiding>>) PXSelectBase<CAReconEntry.CATranVoiding, PXSelect<CAReconEntry.CATranVoiding, Where<CAReconEntry.CATranVoiding.voidedTranID, In<Required<CAReconEntry.CATranExt.tranID>>, And<CAReconEntry.CATranVoiding.cashAccountID, Equal<Required<CAReconEntry.CATranExt.cashAccountID>>, And<Where<CAReconEntry.CATranVoiding.tranDate, LessEqual<Current2<CARecon.loadDocumentsTill>>, Or<Current2<CARecon.loadDocumentsTill>, IsNull, Or<CAReconEntry.CATranVoiding.reconNbr, Equal<Current2<CARecon.reconNbr>>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) array,
          (object) doc.CashAccountID
        })).ToDictionary<PXResult<CAReconEntry.CATranVoiding>, long?, CAReconEntry.CATranVoiding>((Func<PXResult<CAReconEntry.CATranVoiding>, long?>) (result => PXResult<CAReconEntry.CATranVoiding>.op_Implicit(result).VoidedTranID), (Func<PXResult<CAReconEntry.CATranVoiding>, CAReconEntry.CATranVoiding>) (result => PXResult<CAReconEntry.CATranVoiding>.op_Implicit(result))));
    }
  }

  private List<CAReconEntry.CATranExt> GetBatchTransactions(HashSet<long> catrans)
  {
    HashSet<string> stringSet1 = new HashSet<string>();
    HashSet<string> stringSet2 = new HashSet<string>();
    PXResultset<CABatch> pxResultset = PXSelectBase<CABatch, PXSelectJoin<CABatch, InnerJoin<CABatchDetail, On<CABatchDetail.batchNbr, Equal<CABatch.batchNbr>>, InnerJoin<CATran, On<CATran.origModule, Equal<CABatchDetail.origModule>, And<CATran.origTranType, Equal<CABatchDetail.origDocType>, And<CATran.origRefNbr, Equal<CABatchDetail.origRefNbr>, And<CATran.isPaymentChargeTran, Equal<False>>>>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.docRefNbr, Equal<CABatch.batchNbr>, And<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>, And<CABankTranMatch.docType, Equal<CATranType.cABatch>>>>, LeftJoin<CABankTran, On<CABankTran.tranID, Equal<CABankTranMatch.tranID>>, LeftJoin<CAReconEntry.CABankTranMatch2, On<CAReconEntry.CABankTranMatch2.cATranID, Equal<CATran.tranID>>>>>>>, Where<CABatch.cashAccountID, Equal<Current<CARecon.cashAccountID>>, And<Where<CABatch.reconNbr, Equal<Current<CARecon.reconNbr>>, Or<Current<CARecon.reconciled>, Equal<boolFalse>, And<CABatch.reconNbr, IsNull>>>>>, OrderBy<Asc<CABatch.batchNbr, Desc<CAReconEntry.CABankTranMatch2.tranID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    foreach (PXResult<CABatch, CABatchDetail, CATran, CABankTranMatch, CABankTran, CAReconEntry.CABankTranMatch2> pxResult in pxResultset)
    {
      CATran caTran = PXResult<CABatch, CABatchDetail, CATran, CABankTranMatch, CABankTran, CAReconEntry.CABankTranMatch2>.op_Implicit(pxResult);
      CABatch caBatch = PXResult<CABatch, CABatchDetail, CATran, CABankTranMatch, CABankTran, CAReconEntry.CABankTranMatch2>.op_Implicit(pxResult);
      CAReconEntry.CABankTranMatch2 caBankTranMatch2 = PXResult<CABatch, CABatchDetail, CATran, CABankTranMatch, CABankTran, CAReconEntry.CABankTranMatch2>.op_Implicit(pxResult);
      if (caTran.ReconNbr != ((PXSelectBase<CARecon>) this.CAReconRecords).Current.ReconNbr && (((PXSelectBase<CARecon>) this.CAReconRecords).Current.Reconciled.GetValueOrDefault() || caTran.ReconNbr != null))
        stringSet2.Add(caBatch.BatchNbr);
      if (caBankTranMatch2 != null && caBankTranMatch2.TranID.HasValue)
        stringSet1.Add(caBatch.BatchNbr);
    }
    List<CAReconEntry.CATranExt> batchTransactions = new List<CAReconEntry.CATranExt>();
    CAReconEntry.CATranExt destinationTran = (CAReconEntry.CATranExt) null;
    string str = (string) null;
    foreach (PXResult<CABatch, CABatchDetail, CATran, CABankTranMatch, CABankTran, CAReconEntry.CABankTranMatch2> pxResult in pxResultset)
    {
      CATran tran = PXResult<CABatch, CABatchDetail, CATran, CABankTranMatch, CABankTran, CAReconEntry.CABankTranMatch2>.op_Implicit(pxResult);
      CABatch batch = PXResult<CABatch, CABatchDetail, CATran, CABankTranMatch, CABankTran, CAReconEntry.CABankTranMatch2>.op_Implicit(pxResult);
      if (!stringSet2.Contains(batch.BatchNbr) && !stringSet1.Contains(batch.BatchNbr) && !this.SkipTransaction(tran, batch))
      {
        catrans.Add(tran.TranID.Value);
        bool? nullable1 = batch.Reconciled;
        bool flag = false;
        if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
        {
          DateTime? tranDate = batch.TranDate;
          DateTime? loadDocumentsTill = ((PXSelectBase<CARecon>) this.CAReconRecords).Current.LoadDocumentsTill;
          if ((tranDate.HasValue & loadDocumentsTill.HasValue ? (tranDate.GetValueOrDefault() > loadDocumentsTill.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            continue;
        }
        if (str != batch.BatchNbr)
        {
          destinationTran = new CAReconEntry.CATranExt();
          this.CopyCABatchToCATran(batch, (CATran) destinationTran);
          destinationTran.Voided = new bool?(false);
          destinationTran.VoidingNotReleased = new bool?(false);
          destinationTran.VoidingTranID = new long?();
          destinationTran.TranPeriodID = tran.TranPeriodID;
          destinationTran.FinPeriodID = tran.FinPeriodID;
          destinationTran.CuryEffTranAmt = destinationTran.CuryTranAmt;
          destinationTran.EffTranAmt = destinationTran.TranAmt;
          destinationTran.ReferenceID = tran.ReferenceID;
          CABankTran caBankTran = PXResult<CABatch, CABatchDetail, CATran, CABankTranMatch, CABankTran, CAReconEntry.CABankTranMatch2>.op_Implicit(pxResult);
          if (caBankTran != null)
          {
            nullable1 = caBankTran.Processed;
            if (nullable1.GetValueOrDefault())
              destinationTran.ProcessedFromFeed = new bool?(true);
          }
          batchTransactions.Add(destinationTran);
          str = batch.BatchNbr;
        }
        else if (destinationTran != null)
        {
          int? referenceId = destinationTran.ReferenceID;
          int? nullable2 = PXResult<CABatch, CABatchDetail, CATran, CABankTranMatch, CABankTran, CAReconEntry.CABankTranMatch2>.op_Implicit(pxResult).ReferenceID;
          if (!(referenceId.GetValueOrDefault() == nullable2.GetValueOrDefault() & referenceId.HasValue == nullable2.HasValue))
          {
            CAReconEntry.CATranExt caTranExt = destinationTran;
            nullable2 = new int?();
            int? nullable3 = nullable2;
            caTranExt.ReferenceID = nullable3;
          }
        }
      }
    }
    return batchTransactions;
  }

  public virtual void Persist()
  {
    List<CATran> caTranList = new List<CATran>((IEnumerable<CATran>) ((PXGraph) this).Caches[typeof (CATran)].Updated);
    caTranList.AddRange((IEnumerable<CATran>) ((PXGraph) this).Caches[typeof (CAReconEntry.CATranVoiding)].Updated);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXGraph) this).Persist();
      foreach (CATran tran in caTranList)
      {
        if (tran.OrigModule == "AP" && tran.OrigTranType == "CBT")
        {
          foreach (PXResult<CAReconEntry.CATranInBatch> pxResult in ((PXSelectBase<CAReconEntry.CATranInBatch>) this.TransactionsInBatch).Select(new object[1]
          {
            (object) tran.OrigRefNbr
          }))
            CAReconEntry.UpdateClearedOnSourceDoc((CATran) PXResult<CAReconEntry.CATranInBatch>.op_Implicit(pxResult));
        }
        else
          CAReconEntry.UpdateClearedOnSourceDoc(tran);
      }
      transactionScope.Complete();
    }
    this.PerformOnPersist();
    ((PXSelectBase) this.CAReconRecords).Cache.RaiseRowSelected((object) ((PXSelectBase<CARecon>) this.CAReconRecords).Current);
  }

  protected virtual void PerformOnPersist()
  {
  }

  public CAReconEntry()
  {
    CASetup current = ((PXSelectBase<CASetup>) this.casetup).Current;
    PXCache cache1 = ((PXSelectBase) this.CAReconTranRecords).Cache;
    PXParentAttribute.SetLeaveChildren<CATran.reconNbr>(cache1, (object) null, true);
    PXDBCurrencyAttribute.SetBaseCalc<CATran.curyClearedCreditAmt>(cache1, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<CATran.curyClearedDebitAmt>(cache1, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<CATran.curyCreditAmt>(cache1, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<CATran.curyDebitAmt>(cache1, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<CAReconEntry.CATranExt.curyReconciledCredit>(cache1, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<CAReconEntry.CATranExt.curyReconciledDebit>(cache1, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<CATran.curyTranAmt>(cache1, (object) null, false);
    PXCache cache2 = ((PXSelectBase) this.CAReconRecords).Cache;
    PXDBCurrencyAttribute.SetBaseCalc<CARecon.curyReconciledBalance>(cache2, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<CARecon.curyReconciledCredits>(cache2, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<CARecon.curyReconciledDebits>(cache2, (object) null, false);
    PXDBCurrencyAttribute.SetBaseCalc<CARecon.curyReconciledTurnover>(cache2, (object) null, false);
    PXUIFieldAttribute.SetVisible<CARecon.curyID>(cache2, (object) null, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
  }

  public static void UpdateClearedOnSourceDoc(CATran tran)
  {
    bool? paymentChargeTran = tran.IsPaymentChargeTran;
    bool flag = false;
    if (!(paymentChargeTran.GetValueOrDefault() == flag & paymentChargeTran.HasValue))
      return;
    System.Type type = (System.Type) null;
    List<PXDataFieldParam> source = new List<PXDataFieldParam>()
    {
      (PXDataFieldParam) new PXDataFieldAssign("Cleared", (object) tran.Cleared),
      (PXDataFieldParam) new PXDataFieldAssign("ClearDate", (object) tran.ClearDate)
    };
    switch (tran.OrigModule)
    {
      case "AP":
        if (tran.OrigTranType != "CBT")
        {
          type = typeof (PX.Objects.AP.APPayment);
          source.Add((PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.AP.APPayment.docType>((PXDbType) 3, (object) tran.OrigTranType));
          source.Add((PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.AP.APPayment.refNbr>((object) tran.OrigRefNbr));
          break;
        }
        type = typeof (CABatch);
        source.Add((PXDataFieldParam) new PXDataFieldRestrict<CABatch.batchNbr>((object) tran.OrigRefNbr));
        break;
      case "PR":
        if (tran.OrigTranType == "CBT")
        {
          type = typeof (CABatch);
          source.Add((PXDataFieldParam) new PXDataFieldRestrict<CABatch.batchNbr>((object) tran.OrigRefNbr));
          break;
        }
        break;
      case "AR":
        type = typeof (PX.Objects.AR.ARPayment);
        source.Add((PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.AR.ARPayment.docType>((PXDbType) 3, (object) tran.OrigTranType));
        source.Add((PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.AR.ARPayment.refNbr>((object) tran.OrigRefNbr));
        break;
      case "CA":
        switch (tran.OrigTranType)
        {
          case "CDT":
          case "CVD":
            type = typeof (CADeposit);
            source.Add((PXDataFieldParam) new PXDataFieldRestrict<CADeposit.tranType>((PXDbType) 3, (object) tran.OrigTranType));
            source.Add((PXDataFieldParam) new PXDataFieldRestrict<CADeposit.refNbr>((object) tran.OrigRefNbr));
            break;
          case "CAE":
            type = typeof (CAAdj);
            source.Add((PXDataFieldParam) new PXDataFieldRestrict<CAAdj.adjTranType>((PXDbType) 3, (object) tran.OrigTranType));
            source.Add((PXDataFieldParam) new PXDataFieldRestrict<CAAdj.adjRefNbr>((object) tran.OrigRefNbr));
            break;
          case "CTE":
            type = typeof (CAExpense);
            source.Add((PXDataFieldParam) new PXDataFieldRestrict<CAExpense.cashTranID>((object) tran.TranID));
            break;
          case "CTI":
            type = typeof (CATransfer);
            source = new List<PXDataFieldParam>()
            {
              (PXDataFieldParam) new PXDataFieldAssign<CATransfer.clearedIn>((object) tran.Cleared),
              (PXDataFieldParam) new PXDataFieldAssign<CATransfer.clearDateIn>((object) tran.ClearDate),
              (PXDataFieldParam) new PXDataFieldRestrict<CATransfer.transferNbr>((object) tran.OrigRefNbr)
            };
            break;
          case "CTO":
            type = typeof (CATransfer);
            source = new List<PXDataFieldParam>()
            {
              (PXDataFieldParam) new PXDataFieldAssign<CATransfer.clearedOut>((object) tran.Cleared),
              (PXDataFieldParam) new PXDataFieldAssign<CATransfer.clearDateOut>((object) tran.ClearDate),
              (PXDataFieldParam) new PXDataFieldRestrict<CATransfer.transferNbr>((object) tran.OrigRefNbr)
            };
            break;
        }
        break;
    }
    if (!(type != (System.Type) null) || !source.Any<PXDataFieldParam>())
      return;
    PXDatabase.Update(type, source.ToArray());
  }

  public static void ReconCreate(CashAccount acct)
  {
    if (acct == null)
      return;
    if (!acct.Reconcile.GetValueOrDefault())
      throw new Exception("Cash account does not require reconciliation");
    CAReconEntry instance = PXGraph.CreateInstance<CAReconEntry>();
    CARecon caRecon = (CARecon) ((PXSelectBase) new PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>>, OrderBy<Desc<CARecon.reconDate, Desc<CARecon.reconNbr>>>>((PXGraph) instance)).View.SelectSingle(new object[1]
    {
      (object) acct.CashAccountID
    });
    if (caRecon != null && !caRecon.Reconciled.GetValueOrDefault())
      throw new Exception("Can not create statement - current statement is not reconciled.");
    ((PXSelectBase<CARecon>) instance.CAReconRecords).Insert(new CARecon()
    {
      CashAccountID = acct.CashAccountID
    });
    throw new PXRedirectRequiredException((PXGraph) instance, "Document");
  }

  public static void VoidCARecon(CARecon recon)
  {
    CAReconEntry instance = PXGraph.CreateInstance<CAReconEntry>();
    if (!CAReconEntry.IsVoidAllowed(recon, instance))
      throw new Exception("There are newer non-voided statements.");
    recon = ((PXSelectBase<CARecon>) instance.CAReconRecords).Update(recon);
    Decimal? reconciledDebits1 = recon.ReconciledDebits;
    Decimal? reconciledCredits1 = recon.ReconciledCredits;
    Decimal? reconciledDebits2 = recon.CuryReconciledDebits;
    Decimal? reconciledCredits2 = recon.CuryReconciledCredits;
    int? countDebit = recon.CountDebit;
    int? countCredit = recon.CountCredit;
    foreach (PXResult<CAReconEntry.CATranExt> pxResult in PXSelectBase<CAReconEntry.CATranExt, PXSelect<CAReconEntry.CATranExt, Where<CAReconEntry.CATranExt.reconNbr, Equal<Required<CARecon.reconNbr>>, And<CAReconEntry.CATranExt.cashAccountID, Equal<Required<CARecon.cashAccountID>>>>>.Config>.Select((PXGraph) instance, new object[2]
    {
      (object) recon.ReconNbr,
      (object) recon.CashAccountID
    }))
    {
      CAReconEntry.CATranExt caTranExt = PXResult<CAReconEntry.CATranExt>.op_Implicit(pxResult);
      if (caTranExt.Reconciled.GetValueOrDefault())
      {
        CAReconEntry.CATranExt copy = PXCache<CAReconEntry.CATranExt>.CreateCopy(caTranExt);
        copy.Reconciled = new bool?(false);
        ((PXSelectBase<CAReconEntry.CATranExt>) instance.CAReconTranRecords).Update(copy);
      }
    }
    foreach (PXResult<CABatch> pxResult in PXSelectBase<CABatch, PXSelect<CABatch, Where<CABatch.reconNbr, Equal<Required<CARecon.reconNbr>>, And<CABatch.cashAccountID, Equal<Required<CARecon.cashAccountID>>>>>.Config>.Select((PXGraph) instance, new object[2]
    {
      (object) recon.ReconNbr,
      (object) recon.CashAccountID
    }))
    {
      CABatch caBatch = PXResult<CABatch>.op_Implicit(pxResult);
      if (caBatch.Reconciled.GetValueOrDefault())
      {
        CABatch copy = PXCache<CABatch>.CreateCopy(caBatch);
        copy.Reconciled = new bool?(false);
        copy.ReconNbr = (string) null;
        copy.ReconDate = new DateTime?();
        ((PXSelectBase<CABatch>) instance.CABatches).Update(copy);
      }
    }
    CARecon copy1 = (CARecon) ((PXSelectBase) instance.CAReconRecords).Cache.CreateCopy((object) recon);
    copy1.Reconciled = new bool?(false);
    copy1.Voided = new bool?(true);
    copy1.ReconciledDebits = reconciledDebits1;
    copy1.ReconciledCredits = reconciledCredits1;
    copy1.CuryReconciledDebits = reconciledDebits2;
    copy1.CuryReconciledCredits = reconciledCredits2;
    copy1.CountDebit = countDebit;
    copy1.CountCredit = countCredit;
    ((PXSelectBase<CARecon>) instance.CAReconRecords).Update(copy1);
    ((PXAction) instance.Save).Press();
  }

  private static bool IsVoidAllowed(CARecon recon, CAReconEntry graph)
  {
    foreach (PXResult<CARecon> pxResult in ((PXSelectBase<CARecon>) new PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.reconDate, GreaterEqual<Required<CARecon.reconDate>>>>, OrderBy<Desc<CARecon.reconDate, Desc<CARecon.reconNbr>>>>((PXGraph) graph)).Select(new object[2]
    {
      (object) recon.CashAccountID,
      (object) recon.ReconDate
    }))
    {
      CARecon caRecon = PXResult<CARecon>.op_Implicit(pxResult);
      if (caRecon != null)
      {
        bool? nullable;
        if (caRecon.ReconNbr == recon.ReconNbr)
        {
          nullable = caRecon.Reconciled;
          return nullable.GetValueOrDefault();
        }
        nullable = caRecon.Voided;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          return false;
      }
    }
    return false;
  }

  public static void ReleaseCARecon(CARecon recon)
  {
    if (recon.Hold.GetValueOrDefault())
      throw new PXException("Document is On Hold and cannot be released.");
    CAReconEntry instance = PXGraph.CreateInstance<CAReconEntry>();
    long? tranId;
    if (recon.SkipVoided.GetValueOrDefault())
    {
      CATran caTran = PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelectJoin<CATran, InnerJoin<CAReconEntry.CATranVoiding, On<CAReconEntry.CATranVoiding.cashAccountID, Equal<CATran.cashAccountID>, And<CAReconEntry.CATranVoiding.voidedTranID, Equal<CATran.tranID>, And<CAReconEntry.CATranVoiding.released, Equal<False>>>>>, Where<CATran.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<Where<CATran.reconNbr, Equal<Required<CARecon.reconNbr>>>>>>.Config>.SelectWindowed((PXGraph) instance, 0, 1, new object[2]
      {
        (object) recon.CashAccountID,
        (object) recon.ReconNbr
      }));
      int num;
      if (caTran == null)
      {
        num = 0;
      }
      else
      {
        tranId = caTran.TranID;
        num = tranId.HasValue ? 1 : 0;
      }
      if (num != 0)
        throw new PXException("Transactions having a 'Void Pending' status may not be added to the reconciliation");
    }
    CATran caTran1 = PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.reconNbr, Equal<Required<CARecon.reconNbr>>, And<CATran.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CATran.reconciled, Equal<True>, And<CATran.released, Equal<False>>>>>>.Config>.SelectWindowed((PXGraph) instance, 0, 1, new object[2]
    {
      (object) recon.ReconNbr,
      (object) recon.CashAccountID
    }));
    int num1;
    if (caTran1 == null)
    {
      num1 = 0;
    }
    else
    {
      tranId = caTran1.TranID;
      num1 = tranId.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      throw new Exception("One or more items are not released and statement cannot be completed");
    recon.Reconciled = new bool?(true);
    ((PXSelectBase<CARecon>) instance.CAReconRecords).Update(recon);
    ((PXAction) instance.Save).Press();
  }

  public virtual CARecon GetLastRecon(CARecon row)
  {
    if ((row != null ? (!row.CashAccountID.HasValue ? 1 : 0) : 1) != 0)
      return (CARecon) null;
    ((PXSelectBase) this.CAReconRecords).Cache.ClearQueryCacheObsolete();
    return ((PXSelectBase) new PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.voided, NotEqual<True>>>, OrderBy<Desc<CARecon.reconDate, Desc<CARecon.reconNbr>>>>((PXGraph) this)).View.SelectSingle(new object[1]
    {
      (object) row.CashAccountID
    }) as CARecon;
  }

  public virtual bool IsHeaderUpdateEnabled(CARecon header)
  {
    return !header.Reconciled.GetValueOrDefault() && !header.Voided.GetValueOrDefault() && header.CashAccountID.HasValue;
  }

  private bool IsDisabledBecauseOfVoidingNotReleased(CAReconEntry.CATranExt tran)
  {
    return ((PXSelectBase<CARecon>) this.CAReconRecords).Current.SkipVoided.GetValueOrDefault() && tran.VoidingNotReleased.GetValueOrDefault() && tran.VoidingTranID.HasValue;
  }

  protected virtual bool Skip(CATran voidingTran, CATran voidedTran = null)
  {
    if (!voidingTran.VoidedTranID.HasValue || !voidingTran.Released.GetValueOrDefault())
      return false;
    if (voidedTran == null)
      voidedTran = PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATran.voidedTranID>>, And<CATran.cashAccountID, Equal<Required<CATran.cashAccountID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) voidingTran.VoidedTranID,
        (object) voidingTran.CashAccountID
      }));
    if (voidedTran == null)
      return false;
    bool? nullable = ((PXSelectBase<CARecon>) this.CAReconRecords).Current.ShowBatchPayments;
    if (nullable.GetValueOrDefault())
    {
      bool flag = false;
      foreach (PXResult<CABatchDetail, CAReconEntry.CABatchDetail2, PX.Objects.AP.Standalone.APPayment, CABankTranMatch> pxResult in PXSelectBase<CABatchDetail, PXSelectJoin<CABatchDetail, LeftJoin<CAReconEntry.CABatchDetail2, On<CAReconEntry.CABatchDetail2.batchNbr, Equal<CABatchDetail.batchNbr>, And<CAReconEntry.CABatchDetail2.origModule, Equal<BatchModule.moduleAP>>>, LeftJoin<PX.Objects.AP.Standalone.APPayment, On<PX.Objects.AP.Standalone.APPayment.docType, Equal<CAReconEntry.CABatchDetail2.origDocType>, And<PX.Objects.AP.Standalone.APPayment.refNbr, Equal<CAReconEntry.CABatchDetail2.origRefNbr>>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.cATranID, Equal<PX.Objects.AP.Standalone.APPayment.cATranID>>>>>, Where<CABatchDetail.origDocType, Equal<Required<CABatchDetail.origDocType>>, And<CABatchDetail.origModule, Equal<Required<CABatchDetail.origModule>>, And<CABatchDetail.origRefNbr, Equal<Required<CABatchDetail.origRefNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) voidedTran.OrigTranType,
        (object) voidedTran.OrigModule,
        (object) voidedTran.OrigRefNbr
      }))
      {
        flag = true;
        CABankTranMatch caBankTranMatch = PXResult<CABatchDetail, CAReconEntry.CABatchDetail2, PX.Objects.AP.Standalone.APPayment, CABankTranMatch>.op_Implicit(pxResult);
        if (caBankTranMatch != null && caBankTranMatch.TranID.HasValue)
        {
          flag = false;
          break;
        }
      }
      if (flag)
        return false;
    }
    CAReconEntry.CATranExt caTranExt1 = voidingTran as CAReconEntry.CATranExt;
    if (this.SearchInCache)
    {
      voidingTran = (CATran) PXResultset<CAReconEntry.CATranVoiding>.op_Implicit(PXSelectBase<CAReconEntry.CATranVoiding, PXViewOf<CAReconEntry.CATranVoiding>.BasedOn<SelectFromBase<CAReconEntry.CATranVoiding, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CAReconEntry.CATranVoiding.voidedTranID, IBqlLong>.IsEqual<P.AsLong>>.Order<By<BqlField<CAReconEntry.CATranVoiding.voidedTranID, IBqlLong>.Asc, BqlField<CAReconEntry.CATranVoiding.tranID, IBqlLong>.Asc>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) voidedTran.TranID
      }));
      PXSelectJoin<CAReconEntry.CATranExt, LeftJoin<BAccountR, On<CATran.referenceID, Equal<BAccountR.bAccountID>>>, Where<True, Equal<True>>, OrderBy<Asc<CATran.tranDate>>> reconTranRecords = this.CAReconTranRecords;
      CAReconEntry.CATranExt caTranExt2 = new CAReconEntry.CATranExt();
      caTranExt2.CashAccountID = voidingTran.CashAccountID;
      caTranExt2.TranID = voidingTran.TranID;
      caTranExt1 = ((PXSelectBase<CAReconEntry.CATranExt>) reconTranRecords).Locate(caTranExt2);
    }
    nullable = voidedTran.Reconciled;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
    {
      nullable = voidingTran.Reconciled;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        goto label_28;
    }
    bool? reconciled;
    if (voidedTran.ReconNbr == voidingTran.ReconNbr)
    {
      nullable = voidedTran.Reconciled;
      reconciled = voidingTran.Reconciled;
      if (nullable.GetValueOrDefault() == reconciled.GetValueOrDefault() & nullable.HasValue == reconciled.HasValue)
        goto label_28;
    }
    reconciled = voidedTran.Reconciled;
    if (reconciled.GetValueOrDefault())
    {
      reconciled = voidingTran.Reconciled;
      bool flag3 = false;
      if (reconciled.GetValueOrDefault() == flag3 & reconciled.HasValue && caTranExt1 != null)
      {
        reconciled = caTranExt1.Reconciled;
        if (reconciled.GetValueOrDefault())
          return voidedTran.ReconNbr == caTranExt1?.ReconNbr;
      }
    }
    return false;
label_28:
    return true;
  }

  private void UpdateVoidingTran(CAReconEntry.CATranExt row, CAReconEntry.CATranExt oldRow)
  {
    CARecon current = ((PXSelectBase<CARecon>) this.CAReconRecords).Current;
    long? nullable1;
    int num1;
    if (row == null)
    {
      num1 = 1;
    }
    else
    {
      nullable1 = row.VoidingTranID;
      num1 = !nullable1.HasValue ? 1 : 0;
    }
    if (num1 != 0 || current == null)
      return;
    bool? nullable2 = current.SkipVoided;
    bool valueOrDefault = nullable2.GetValueOrDefault();
    CAReconEntry.CATranVoiding voidingTran = PXResultset<CAReconEntry.CATranVoiding>.op_Implicit(((PXSelectBase<CAReconEntry.CATranVoiding>) this.VoidingTrans).Select(new object[2]
    {
      (object) row.CashAccountID,
      (object) row.VoidingTranID
    }));
    if (!valueOrDefault)
      return;
    int num2;
    if (voidingTran == null)
    {
      num2 = 1;
    }
    else
    {
      nullable1 = voidingTran.TranID;
      num2 = !nullable1.HasValue ? 1 : 0;
    }
    if (num2 != 0 || !this.Skip((CATran) voidingTran, (CATran) oldRow))
      return;
    bool flag = false;
    nullable2 = row.Cleared;
    bool? nullable3 = oldRow.Cleared;
    DateTime? nullable4;
    if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
    {
      nullable4 = row.ClearDate;
      DateTime? clearDate = oldRow.ClearDate;
      if ((nullable4.HasValue == clearDate.HasValue ? (nullable4.HasValue ? (nullable4.GetValueOrDefault() != clearDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        goto label_14;
    }
    voidingTran.ClearDate = row.ClearDate;
    voidingTran.Cleared = row.Cleared;
    flag = true;
label_14:
    nullable3 = row.Reconciled;
    nullable2 = oldRow.Reconciled;
    if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
    {
      DateTime? reconDate = row.ReconDate;
      nullable4 = oldRow.ReconDate;
      if ((reconDate.HasValue == nullable4.HasValue ? (reconDate.HasValue ? (reconDate.GetValueOrDefault() != nullable4.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && !(row.ReconNbr != oldRow.ReconNbr))
        goto label_19;
    }
    flag = true;
    voidingTran.Reconciled = row.Reconciled;
    voidingTran.ReconDate = row.ReconDate;
    voidingTran.ReconNbr = row.ReconNbr;
    nullable2 = voidingTran.Reconciled;
    if (nullable2.GetValueOrDefault())
    {
      nullable2 = voidingTran.Cleared;
      if (!nullable2.GetValueOrDefault())
      {
        voidingTran.ClearDate = row.ClearDate;
        voidingTran.Cleared = row.Cleared;
      }
    }
label_19:
    if (!flag)
      return;
    ((PXSelectBase<CAReconEntry.CATranVoiding>) this.VoidingTrans).Update(voidingTran);
  }

  protected virtual void UpdateBatchTransactions(CAReconEntry.CATranExt row)
  {
    if (!(row.OrigModule == "AP") || !(row.OrigTranType == "CBT"))
      return;
    foreach (PXResult<CAReconEntry.CATranInBatch> pxResult in ((PXSelectBase<CAReconEntry.CATranInBatch>) this.TransactionsInBatch).Select(new object[1]
    {
      (object) row.OrigRefNbr
    }))
    {
      CAReconEntry.CATranInBatch caTranInBatch = PXResult<CAReconEntry.CATranInBatch>.op_Implicit(pxResult);
      caTranInBatch.CopyFrom((CATran) row);
      ((PXSelectBase<CAReconEntry.CATranInBatch>) this.TransactionsInBatch).Update(caTranInBatch);
    }
    this.UpdateBatch(row);
  }

  public virtual void UpdateBatch(CAReconEntry.CATranExt row)
  {
    CABatch caBatch = PXResultset<CABatch>.op_Implicit(((PXSelectBase<CABatch>) this.CABatches).Select(new object[1]
    {
      (object) row.OrigRefNbr
    }));
    caBatch.Cleared = row.Cleared;
    caBatch.ClearDate = row.ClearDate;
    caBatch.Reconciled = row.Reconciled;
    caBatch.ReconDate = row.ReconDate;
    caBatch.ReconNbr = row.ReconNbr;
    ((PXSelectBase<CABatch>) this.CABatches).Update(caBatch);
  }

  public virtual CATran CopyCABatchToCATran(CABatch batch, CATran destinationTran)
  {
    batch.CopyTo(destinationTran);
    if (batch.OrigModule == "PR")
      destinationTran.OrigModule = "PR";
    return destinationTran;
  }

  protected virtual bool SkipTransaction(CATran tran, CABatch batch) => false;

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() || string.IsNullOrEmpty(((PXSelectBase<CashAccount>) this.cashaccount).Current?.CuryID))
      return;
    e.NewValue = (object) ((PXSelectBase<CashAccount>) this.cashaccount).Current.CuryID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    if (!string.IsNullOrEmpty(((PXSelectBase<CashAccount>) this.cashaccount).Current?.CuryRateTypeID))
    {
      e.NewValue = (object) ((PXSelectBase<CashAccount>) this.cashaccount).Current.CuryRateTypeID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      PX.Objects.CM.CMSetup cmSetup = PXResultset<PX.Objects.CM.CMSetup>.op_Implicit(PXSelectBase<PX.Objects.CM.CMSetup, PXSelect<PX.Objects.CM.CMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (string.IsNullOrEmpty(cmSetup?.CARateTypeDflt))
        return;
      e.NewValue = (object) cmSetup.CARateTypeDflt;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    PX.Objects.CM.CurrencyInfo row = (PX.Objects.CM.CurrencyInfo) e.Row;
    if (((PXSelectBase<CARecon>) this.CAReconRecords).Current == null)
      return;
    long? curyInfoId1 = ((PXSelectBase<CARecon>) this.CAReconRecords).Current.CuryInfoID;
    long? curyInfoId2 = row.CuryInfoID;
    if (!(curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue))
      return;
    e.NewValue = (object) ((PXSelectBase<CARecon>) this.CAReconRecords).Current.ReconDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CATranExt_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CAReconEntry.CATranExt row = (CAReconEntry.CATranExt) e.Row;
    bool? nullable = (bool?) ((PXSelectBase<CARecon>) this.CAReconRecords).Current?.SkipVoided;
    bool valueOrDefault = nullable.GetValueOrDefault();
    if ((row.OrigModule == "AP" || row.OrigModule == "PR") && row.OrigTranType == "CBT")
      ((CancelEventArgs) e).Cancel = true;
    if (valueOrDefault && row.VoidedTranID.HasValue && GraphHelper.RowCast<CAReconEntry.CATranVoiding>(((PXSelectBase) this.VoidingTrans).Cache.Updated).Any<CAReconEntry.CATranVoiding>((Func<CAReconEntry.CATranVoiding, bool>) (tran =>
    {
      long? tranId1 = tran.TranID;
      long? tranId2 = row.TranID;
      return tranId1.GetValueOrDefault() == tranId2.GetValueOrDefault() & tranId1.HasValue == tranId2.HasValue;
    })))
      ((CancelEventArgs) e).Cancel = true;
    nullable = row.Reconciled;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.Cleared;
      if (!nullable.GetValueOrDefault())
        sender.RaiseExceptionHandling<CATran.cleared>((object) row, (object) row.Cleared, (Exception) new PXSetPropertyException("The document has to be cleared before it is reconciled"));
      if (!row.ClearDate.HasValue)
        sender.RaiseExceptionHandling<CATran.clearDate>((object) row, (object) row.ClearDate, (Exception) new PXSetPropertyException("The document has to be cleared before it is reconciled"));
      nullable = row.Released;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        sender.RaiseExceptionHandling<CATran.reconciled>((object) row, (object) row.Reconciled, (Exception) new PXSetPropertyException("Unreleased document cannot be added to Reconciliation."));
      if (valueOrDefault && row.VoidingTranID.HasValue)
      {
        nullable = row.VoidingNotReleased;
        if (nullable.GetValueOrDefault())
          sender.RaiseExceptionHandling<CATran.reconciled>((object) row, (object) row.ClearDate, (Exception) new PXSetPropertyException("This transaction has a voiding transaction which is not released. It may not be added to the reconciliation"));
      }
    }
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    CARecon current = ((PXSelectBase<CARecon>) this.CAReconRecords).Current;
    nullable = row.Reconciled;
    if (nullable.GetValueOrDefault())
    {
      row.ReconNbr = current.ReconNbr;
      row.ReconDate = current.ReconDate;
      this.UpdateBatchTransactions(row);
    }
    else
    {
      row.ReconNbr = (string) null;
      row.ReconDate = new DateTime?();
    }
  }

  protected virtual void CATranExt_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    CATran row = (CATran) e.Row;
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1 || e.TranStatus != 2 || !row.Reconciled.GetValueOrDefault())
      return;
    CARecon current = ((PXSelectBase<CARecon>) this.CAReconRecords).Current;
    row.ReconNbr = current.ReconNbr;
    row.ReconDate = current.ReconDate;
  }

  protected virtual void CATranExt_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CAReconEntry.CATranExt row = (CAReconEntry.CATranExt) e.Row;
    CARecon current = ((PXSelectBase<CARecon>) this.CAReconRecords).Current;
    if (row == null || current == null)
      return;
    bool? nullable = current.Reconciled;
    bool flag1 = !nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CATran.tranID>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CATran.hold>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CATran.reconciled>(cache, (object) row, flag1);
    int num1;
    if (flag1)
    {
      nullable = row.ProcessedFromFeed;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag2 = num1 != 0;
    PXUIFieldAttribute.SetEnabled<CATran.cleared>(((PXSelectBase) this.CAReconTranRecords).Cache, (object) row, flag2);
    PXCache cache1 = ((PXSelectBase) this.CAReconTranRecords).Cache;
    CAReconEntry.CATranExt caTranExt = row;
    int num2;
    if (flag2)
    {
      nullable = row.Cleared;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<CATran.clearDate>(cache1, (object) caTranExt, num2 != 0);
    if (!flag1)
      return;
    nullable = row.Released;
    if (nullable.GetValueOrDefault() && !this.IsDisabledBecauseOfVoidingNotReleased(row))
      return;
    PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
    if (!this.IsDisabledBecauseOfVoidingNotReleased(row))
      return;
    nullable = row.Reconciled;
    if (!nullable.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled<CATran.reconciled>(cache, (object) row, true);
  }

  protected virtual void CATranExt_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    CAReconEntry.CATranExt newRow = (CAReconEntry.CATranExt) e.NewRow;
    CAReconEntry.CATranExt row = (CAReconEntry.CATranExt) e.Row;
    CARecon current = ((PXSelectBase<CARecon>) this.CAReconRecords).Current;
    if (newRow == null || row == null || current == null)
      return;
    bool? nullable = current.SkipVoided;
    bool valueOrDefault = nullable.GetValueOrDefault();
    using (new CAReconEntry.CATranExtRowUpdatingContext(this))
    {
      if (valueOrDefault)
      {
        nullable = newRow.Reconciled;
        if (nullable.GetValueOrDefault() && this.Skip((CATran) newRow) && !this.Skip((CATran) row))
        {
          PXSelectJoin<CAReconEntry.CATranExt, LeftJoin<BAccountR, On<CATran.referenceID, Equal<BAccountR.bAccountID>>>, Where<True, Equal<True>>, OrderBy<Asc<CATran.tranDate>>> reconTranRecords = this.CAReconTranRecords;
          CAReconEntry.CATranExt caTranExt = new CAReconEntry.CATranExt();
          caTranExt.TranID = newRow.VoidedTranID;
          nullable = ((PXSelectBase<CAReconEntry.CATranExt>) reconTranRecords).Locate(caTranExt).Reconciled;
          if (!nullable.GetValueOrDefault())
            return;
          newRow.SkipCount = new bool?(true);
          ((PXSelectBase) this.CAReconTranRecords).View.RequestRefresh();
          return;
        }
      }
      if (!valueOrDefault)
        return;
      nullable = newRow.Reconciled;
      bool flag = false;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) || !newRow.VoidingTranID.HasValue)
        return;
      ((PXSelectBase) this.CAReconTranRecords).View.RequestRefresh();
    }
  }

  protected virtual void CATranExt_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    CAReconEntry.CATranExt row = (CAReconEntry.CATranExt) e.Row;
    CAReconEntry.CATranExt oldRow = (CAReconEntry.CATranExt) e.OldRow;
    this.UpdateVoidingTran(row, oldRow);
    this.UpdateBatchTransactions(row);
    row.SkipCount = new bool?(false);
  }

  protected virtual void CATranExt_Reconciled_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    CAReconEntry.CATranExt row = (CAReconEntry.CATranExt) e.Row;
    bool valueOrDefault = ((PXSelectBase<CARecon>) this.CAReconRecords).Current.SkipVoided.GetValueOrDefault();
    if (!(bool) e.NewValue)
      return;
    string str1 = string.Empty;
    bool? nullable1 = row.Hold;
    if (nullable1.GetValueOrDefault())
    {
      e.NewValue = (object) false;
      ((CancelEventArgs) e).Cancel = true;
      str1 = "Document on hold cannot be added to reconciliation.";
    }
    nullable1 = row.Released;
    if (!nullable1.GetValueOrDefault() && string.IsNullOrEmpty(str1))
    {
      e.NewValue = (object) false;
      ((CancelEventArgs) e).Cancel = true;
      str1 = "Unreleased document cannot be added to Reconciliation.";
    }
    if (valueOrDefault && row.VoidingTranID.HasValue)
    {
      nullable1 = row.VoidingNotReleased;
      if (nullable1.GetValueOrDefault() && string.IsNullOrEmpty(str1))
        str1 = "Transactions having a 'Void Pending' status may not be added to the reconciliation";
    }
    if (valueOrDefault && this.Skip((CATran) row) && string.IsNullOrEmpty(str1))
      str1 = "Transactions having a 'Void Pending' status may not be added to the reconciliation";
    if (!string.IsNullOrEmpty(str1))
    {
      PXFieldVerifyingEventArgs verifyingEventArgs = e;
      object newValue = e.NewValue;
      System.Type type = typeof (string);
      bool? nullable2 = new bool?(false);
      nullable1 = new bool?();
      bool? nullable3 = nullable1;
      int? nullable4 = new int?();
      int? nullable5 = new int?();
      int? nullable6 = new int?();
      string name = typeof (CAReconEntry.CATranExt.reconciled).Name;
      string str2 = str1;
      nullable1 = new bool?();
      bool? nullable7 = nullable1;
      nullable1 = new bool?();
      bool? nullable8 = nullable1;
      nullable1 = new bool?();
      bool? nullable9 = nullable1;
      PXFieldState instance = PXFieldState.CreateInstance(newValue, type, nullable2, nullable3, nullable4, nullable5, nullable6, (object) null, name, (string) null, (string) null, str2, (PXErrorLevel) 3, nullable7, nullable8, nullable9, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
      verifyingEventArgs.NewValue = (object) instance;
      ((CancelEventArgs) e).Cancel = true;
      e.NewValue = (object) false;
      throw new PXSetPropertyException(str1);
    }
  }

  protected virtual void CATranExt_Cleared_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    CAReconEntry.CATranExt row = (CAReconEntry.CATranExt) e.Row;
    if (row.Cleared.GetValueOrDefault())
      row.ClearDate = ((PXSelectBase<CARecon>) this.CAReconRecords).Current.ReconDate;
    else
      row.ClearDate = new DateTime?();
  }

  protected virtual void CATranExt_Reconciled_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    CAReconEntry.CATranExt row = (CAReconEntry.CATranExt) e.Row;
    if (row.Reconciled.GetValueOrDefault())
    {
      row.Cleared = new bool?(true);
      row.ReconNbr = ((PXSelectBase<CARecon>) this.CAReconRecords).Current.ReconNbr;
      row.ReconDate = ((PXSelectBase<CARecon>) this.CAReconRecords).Current.ReconDate;
      if (row.ClearDate.HasValue)
        return;
      row.ClearDate = ((PXSelectBase<CARecon>) this.CAReconRecords).Current.ReconDate;
    }
    else
    {
      row.ReconNbr = (string) null;
      row.ReconDate = new DateTime?();
    }
  }

  protected virtual void CATranExt_Status_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    CAReconEntry.CATranExt row = (CAReconEntry.CATranExt) e.Row;
    if (row == null)
      return;
    bool? nullable1 = ((PXSelectBase<CARecon>) this.CAReconRecords).Current.SkipVoided;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    Dictionary<long, CAMessage> customInfo = PXLongOperation.GetCustomInfo(((PXGraph) this).UID) as Dictionary<long, CAMessage>;
    TimeSpan timeSpan;
    Exception exception;
    PXLongRunStatus status = PXLongOperation.GetStatus(((PXGraph) this).UID, ref timeSpan, ref exception);
    string name = typeof (CAReconEntry.CATranExt.status).Name;
    if ((status == 3 || status == 2) && customInfo != null)
    {
      CAMessage caMessage = (CAMessage) null;
      Dictionary<long, CAMessage> dictionary1 = customInfo;
      long? tranId = row.TranID;
      long key1 = tranId.Value;
      if (dictionary1.ContainsKey(key1))
      {
        Dictionary<long, CAMessage> dictionary2 = customInfo;
        tranId = row.TranID;
        long key2 = tranId.Value;
        caMessage = dictionary2[key2];
      }
      if (caMessage == null)
        return;
      PXFieldSelectingEventArgs selectingEventArgs = e;
      object returnState = e.ReturnState;
      System.Type type = typeof (string);
      bool? nullable2 = new bool?(false);
      nullable1 = new bool?();
      bool? nullable3 = nullable1;
      int? nullable4 = new int?();
      int? nullable5 = new int?();
      int? nullable6 = new int?();
      string str1 = name;
      string str2 = PXMessages.LocalizeNoPrefix(caMessage.Message);
      PXErrorLevel errorLevel = caMessage.ErrorLevel;
      nullable1 = new bool?();
      bool? nullable7 = nullable1;
      nullable1 = new bool?();
      bool? nullable8 = nullable1;
      nullable1 = new bool?();
      bool? nullable9 = nullable1;
      PXFieldState instance = PXFieldState.CreateInstance(returnState, type, nullable2, nullable3, nullable4, nullable5, nullable6, (object) null, str1, (string) null, (string) null, str2, errorLevel, nullable7, nullable8, nullable9, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
      selectingEventArgs.ReturnState = (object) instance;
      e.IsAltered = true;
    }
    else
    {
      string str3 = string.Empty;
      PXErrorLevel pxErrorLevel1 = (PXErrorLevel) 3;
      if (valueOrDefault)
      {
        nullable1 = row.VoidingNotReleased;
        if (nullable1.GetValueOrDefault() && row.VoidingTranID.HasValue)
        {
          str3 = "This transaction has a voiding transaction which is not released. It may not be added to the reconciliation";
          nullable1 = row.Reconciled;
          if (nullable1.GetValueOrDefault())
          {
            pxErrorLevel1 = (PXErrorLevel) 5;
            goto label_14;
          }
          goto label_14;
        }
      }
      nullable1 = row.Released;
      if (!nullable1.GetValueOrDefault())
      {
        str3 = "Unreleased document cannot be added to Reconciliation.";
      }
      else
      {
        nullable1 = row.Hold;
        if (nullable1.GetValueOrDefault())
          str3 = "Unreleased document cannot be added to Reconciliation.";
      }
label_14:
      if (string.IsNullOrEmpty(str3))
        return;
      PXFieldSelectingEventArgs selectingEventArgs = e;
      object returnState = e.ReturnState;
      System.Type type = typeof (string);
      bool? nullable10 = new bool?(false);
      nullable1 = new bool?();
      bool? nullable11 = nullable1;
      int? nullable12 = new int?();
      int? nullable13 = new int?();
      int? nullable14 = new int?();
      string str4 = name;
      string str5 = PXMessages.LocalizeNoPrefix(str3);
      PXErrorLevel pxErrorLevel2 = pxErrorLevel1;
      nullable1 = new bool?();
      bool? nullable15 = nullable1;
      nullable1 = new bool?();
      bool? nullable16 = nullable1;
      nullable1 = new bool?();
      bool? nullable17 = nullable1;
      PXFieldState instance = PXFieldState.CreateInstance(returnState, type, nullable10, nullable11, nullable12, nullable13, nullable14, (object) null, str4, (string) null, (string) null, str5, pxErrorLevel2, nullable15, nullable16, nullable17, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
      selectingEventArgs.ReturnState = (object) instance;
      e.IsAltered = true;
    }
  }

  protected virtual void CATranVoiding_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CAReconEntry.CATranVoiding row = (CAReconEntry.CATranVoiding) e.Row;
    bool? nullable1;
    DateTime? nullable2;
    if (row.Reconciled.GetValueOrDefault())
    {
      nullable1 = row.Cleared;
      if (!nullable1.GetValueOrDefault())
        sender.RaiseExceptionHandling<CATran.cleared>((object) row, (object) row.Cleared, (Exception) new PXSetPropertyException("The document has to be cleared before it is reconciled"));
      nullable2 = row.ClearDate;
      if (!nullable2.HasValue)
        sender.RaiseExceptionHandling<CATran.clearDate>((object) row, (object) row.ClearDate, (Exception) new PXSetPropertyException("The document has to be cleared before it is reconciled"));
    }
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    CARecon current = ((PXSelectBase<CARecon>) this.CAReconRecords).Current;
    nullable1 = row.Reconciled;
    if (nullable1.GetValueOrDefault())
    {
      row.ReconNbr = current.ReconNbr;
      row.ReconDate = current.ReconDate;
    }
    else
    {
      row.ReconNbr = (string) null;
      CAReconEntry.CATranVoiding caTranVoiding = row;
      nullable2 = new DateTime?();
      DateTime? nullable3 = nullable2;
      caTranVoiding.ReconDate = nullable3;
    }
  }

  protected virtual void CATranVoiding_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    CAReconEntry.CATranVoiding row = (CAReconEntry.CATranVoiding) e.Row;
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1 || e.TranStatus != 2 || !row.Reconciled.GetValueOrDefault())
      return;
    CARecon current = ((PXSelectBase<CARecon>) this.CAReconRecords).Current;
    row.ReconNbr = current.ReconNbr;
    row.ReconDate = current.ReconDate;
  }

  [PXMergeAttributes]
  [PXDefault]
  [CashAccount(true, true, null, typeof (Search<CashAccount.cashAccountID, Where<CashAccount.active, Equal<True>, And<CashAccount.reconcile, Equal<True>, And<Match<Current<AccessInfo.userName>>>>>>))]
  protected virtual void CARecon_CashAccountID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void CARecon_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    CARecon row = (CARecon) e.Row;
    if (row.Reconciled.GetValueOrDefault() || row.Voided.GetValueOrDefault())
      throw new PXException("The operation cannot be performed because the reconciliation statement has been already released or voided.");
  }

  protected virtual void CARecon_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    CARecon row = (CARecon) e.Row;
    foreach (PXResult<CAReconEntry.CATranExt> pxResult in ((PXSelectBase<CAReconEntry.CATranExt>) this.CAReconTranRecords).Select(Array.Empty<object>()))
    {
      CAReconEntry.CATranExt caTranExt = PXResult<CAReconEntry.CATranExt>.op_Implicit(pxResult);
      if (caTranExt.Reconciled.GetValueOrDefault())
      {
        CAReconEntry.CATranExt copy = (CAReconEntry.CATranExt) ((PXSelectBase) this.CAReconTranRecords).Cache.CreateCopy((object) caTranExt);
        copy.Reconciled = new bool?(false);
        ((PXSelectBase<CAReconEntry.CATranExt>) this.CAReconTranRecords).Update(copy);
      }
    }
    if (((PXSelectBase) this.CAReconRecords).Cache.GetStatus((object) row) == 2)
      return;
    ((PXAction) this.Save).Press();
  }

  protected virtual void CARecon_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    CARecon row = (CARecon) e.Row;
    bool? nullable;
    if (cache.GetStatus((object) row) == 2)
    {
      if (PXResultset<CARecon>.op_Implicit(PXSelectBase<CARecon, PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.voided, Equal<False>, And<CARecon.reconciled, Equal<False>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.CashAccountID
      })) != null)
        cache.RaiseExceptionHandling<CARecon.reconNbr>((object) row, (object) row.ReconNbr, (Exception) new PXSetPropertyException("Previous Statement Not Reconciled"));
      PXResultset<CashAccount> source = PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.CashAccountID
      });
      CashAccount cashAccount = PXResult<CashAccount>.op_Implicit(source != null ? ((IQueryable<PXResult<CashAccount>>) source).First<PXResult<CashAccount>>() : (PXResult<CashAccount>) null);
      if (cashAccount != null)
      {
        nullable = cashAccount.Active;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          cache.RaiseExceptionHandling<CABankTranHeader.cashAccountID>((object) row, (object) row.CashAccountID, (Exception) new PXSetPropertyException("The cash account {0} is deactivated on the Cash Accounts (CA202000) form.", (PXErrorLevel) 5, new object[1]
          {
            (object) cashAccount.CashAccountCD
          }));
      }
      if (cashAccount != null)
      {
        nullable = cashAccount.Reconcile;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          throw new PXSetPropertyException("The {0} cash account does not require reconciliation. Verify if the Requires Reconciliation check box is selected on the Cash Accounts (CA202000) form.", (PXErrorLevel) 5, new object[1]
          {
            (object) cashAccount.CashAccountCD
          });
      }
    }
    nullable = row.Voided;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
    {
      if (PXResultset<CARecon>.op_Implicit(PXSelectBase<CARecon, PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.voided, Equal<False>, And<CARecon.reconNbr, NotEqual<Required<CARecon.reconNbr>>, And<CARecon.reconDate, GreaterEqual<Required<CARecon.reconDate>>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) row.CashAccountID,
        (object) row.ReconNbr,
        (object) row.ReconDate
      })) != null)
        cache.RaiseExceptionHandling<CARecon.reconDate>((object) row, (object) row.ReconDate, (Exception) new PXSetPropertyException<CARecon.reconDate>("Reconciliation date must be later than the date of the previous released Reconciliation Statement"));
    }
    nullable = row.Hold;
    if (nullable.GetValueOrDefault())
      return;
    nullable = row.Voided;
    if (nullable.GetValueOrDefault())
      return;
    Decimal? curyDiffBalance = row.CuryDiffBalance;
    Decimal num = 0M;
    if (curyDiffBalance.GetValueOrDefault() == num & curyDiffBalance.HasValue)
      return;
    cache.RaiseExceptionHandling<CARecon.curyBalance>((object) row, (object) row.CuryBalance, (Exception) new PXSetPropertyException<CARecon.curyBalance>("The document is out of the balance."));
  }

  protected virtual void CARecon_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    CARecon row = (CARecon) e.Row;
    CARecon oldRow = (CARecon) e.OldRow;
    DateTime? nullable = oldRow.ReconDate;
    DateTime? reconDate = row.ReconDate;
    if ((nullable.HasValue == reconDate.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != reconDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
    {
      DateTime? loadDocumentsTill = oldRow.LoadDocumentsTill;
      nullable = row.LoadDocumentsTill;
      if ((loadDocumentsTill.HasValue == nullable.HasValue ? (loadDocumentsTill.HasValue ? (loadDocumentsTill.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        return;
    }
    ((PXSelectBase) this.CAReconTranRecords).View.RequestRefresh();
  }

  protected virtual void CARecon_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    CARecon row = (CARecon) e.Row;
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
    {
      PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.SetDefaults<CARecon.curyInfoID>(cache, (object) row);
      (string, PXErrorLevel) errorWithLevel1 = PXUIFieldAttribute.GetErrorWithLevel<PX.Objects.CM.CurrencyInfo.curyID>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo);
      string str1 = errorWithLevel1.Item1;
      if (!string.IsNullOrEmpty(str1) && errorWithLevel1.Item2 >= 4)
        throw new PXSetPropertyException(str1, (PXErrorLevel) 4);
      (string, PXErrorLevel) errorWithLevel2 = PXUIFieldAttribute.GetErrorWithLevel<PX.Objects.CM.CurrencyInfo.curyEffDate>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo);
      string str2 = errorWithLevel2.Item1;
      if (!string.IsNullOrEmpty(str2) && errorWithLevel2.Item2 >= 4)
        throw new PXSetPropertyException(str2, (PXErrorLevel) 4);
      if (currencyInfo != null)
        row.CuryID = currencyInfo.CuryID;
    }
    CARecon caRecon1 = PXResultset<CARecon>.op_Implicit(PXSelectBase<CARecon, PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.reconNbr, NotEqual<Required<CARecon.reconNbr>>, And<CARecon.voided, NotEqual<True>>>>, OrderBy<Asc<CARecon.cashAccountID, Desc<CARecon.reconDate>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.CashAccountID,
      (object) row.ReconNbr
    }));
    if (caRecon1 == null || !caRecon1.ReconDate.HasValue)
      return;
    if (caRecon1.Reconciled.GetValueOrDefault())
    {
      row.LastReconDate = caRecon1.ReconDate;
      cache.SetValueExt<CARecon.curyBegBalance>((object) row, (object) caRecon1.CuryBalance);
      PXCache pxCache1 = cache;
      CARecon caRecon2 = row;
      Decimal? curyBegBalance = row.CuryBegBalance;
      Decimal? reconciledDebits = row.CuryReconciledDebits;
      Decimal? nullable = curyBegBalance.HasValue & reconciledDebits.HasValue ? new Decimal?(curyBegBalance.GetValueOrDefault() + reconciledDebits.GetValueOrDefault()) : new Decimal?();
      Decimal? reconciledCredits = row.CuryReconciledCredits;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local1 = (ValueType) (nullable.HasValue & reconciledCredits.HasValue ? new Decimal?(nullable.GetValueOrDefault() - reconciledCredits.GetValueOrDefault()) : new Decimal?());
      pxCache1.SetValueExt<CARecon.curyReconciledBalance>((object) caRecon2, (object) local1);
      PXCache pxCache2 = cache;
      CARecon caRecon3 = row;
      Decimal? curyBalance = row.CuryBalance;
      Decimal? reconciledBalance = row.CuryReconciledBalance;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local2 = (ValueType) (curyBalance.HasValue & reconciledBalance.HasValue ? new Decimal?(curyBalance.GetValueOrDefault() - reconciledBalance.GetValueOrDefault()) : new Decimal?());
      pxCache2.SetValueExt<CARecon.curyDiffBalance>((object) caRecon3, (object) local2);
    }
    else
      cache.RaiseExceptionHandling<CARecon.reconNbr>((object) row, (object) row.ReconNbr, (Exception) new PXSetPropertyException("Previous Statement Not Reconciled"));
  }

  protected virtual void CARecon_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CARecon row = (CARecon) e.Row;
    if (row == null)
    {
      ((PXSelectBase) this.CAReconTranRecords).Cache.SetAllEditPermissions(false);
      ((PXSelectBase) this.CAReconRecords).Cache.SetAllEditPermissions(false);
      ((PXAction) this.Next).SetEnabled(false);
      ((PXAction) this.Previous).SetEnabled(false);
      ((PXAction) this.First).SetEnabled(false);
      ((PXAction) this.Last).SetEnabled(false);
    }
    else
    {
      bool flag1 = !this.IsApprovalRequired(row, cache);
      bool? excludeFromApproval = row.ExcludeFromApproval;
      bool flag2 = flag1;
      if (!(excludeFromApproval.GetValueOrDefault() == flag2 & excludeFromApproval.HasValue))
        cache.SetValueExt<CARecon.excludeFromApproval>((object) row, (object) flag1);
      int? cashAccountId1 = row.CashAccountID;
      if (cashAccountId1.HasValue)
      {
        cashAccountId1 = (int?) ((PXSelectBase<CashAccount>) this.cashaccount).Current?.CashAccountID;
        int? cashAccountId2 = row.CashAccountID;
        if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue))
          ((PXSelectBase<CashAccount>) this.cashaccount).Current = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelectReadonly<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) row.CashAccountID
          }));
      }
      PXUIFieldAttribute.SetEnabled<CARecon.reconNbr>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<CARecon.cashAccountID>(cache, (object) row, true);
      bool valueOrDefault1 = row.Reconciled.GetValueOrDefault();
      row.Hold.GetValueOrDefault();
      bool? nullable1 = row.Voided;
      bool valueOrDefault2 = nullable1.GetValueOrDefault();
      nullable1 = row.Hold;
      int num1;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Approved;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = row.Rejected;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = row.ExcludeFromApproval;
            num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
            goto label_12;
          }
        }
      }
      num1 = 0;
label_12:
      bool flag3 = num1 != 0;
      nullable1 = row.Approved;
      int num2;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Rejected;
        if (!nullable1.GetValueOrDefault())
        {
          num2 = 0;
          goto label_16;
        }
      }
      nullable1 = row.ExcludeFromApproval;
      num2 = !nullable1.GetValueOrDefault() ? 1 : 0;
label_16:
      bool flag4 = num2 != 0;
      nullable1 = row.Reconciled;
      int num3;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Hold;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = row.ExcludeFromApproval;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = row.Approved;
            num3 = nullable1.GetValueOrDefault() ? 1 : 0;
            goto label_22;
          }
          num3 = 1;
          goto label_22;
        }
      }
      num3 = 0;
label_22:
      bool flag5 = num3 != 0;
      CARecon lastRecon = this.GetLastRecon(row);
      int num4;
      if (((PXSelectBase<CashAccount>) this.cashaccount).Current != null)
      {
        nullable1 = ((PXSelectBase<CashAccount>) this.cashaccount).Current.Active;
        num4 = !nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num4 = 0;
      bool flag6 = num4 != 0;
      nullable1 = row.Reconciled;
      int num5;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.Voided;
        bool flag7 = false;
        if (nullable1.GetValueOrDefault() == flag7 & nullable1.HasValue && !flag6 && lastRecon != null)
        {
          num5 = lastRecon.ReconNbr == row.ReconNbr ? 1 : 0;
          goto label_29;
        }
      }
      num5 = 0;
label_29:
      bool flag8 = num5 != 0;
      int num6;
      if (row.CashAccountID.HasValue)
      {
        if (lastRecon != null)
        {
          nullable1 = lastRecon.Reconciled;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = lastRecon.Voided;
            num6 = nullable1.GetValueOrDefault() ? 1 : 0;
            goto label_35;
          }
        }
        num6 = 1;
      }
      else
        num6 = 0;
label_35:
      bool flag9 = num6 != 0;
      bool flag10 = this.IsHeaderUpdateEnabled(row) && !flag3 && !flag4;
      bool flag11 = !valueOrDefault1 && !valueOrDefault2 && !flag3 && !flag4 && (cache.GetStatus((object) row) == 2 || lastRecon != null && lastRecon.ReconNbr == row.ReconNbr);
      ((PXAction) this.Voided).SetEnabled(flag8);
      ((PXSelectBase) this.CAReconRecords).Cache.AllowUpdate = flag10 | flag5;
      ((PXSelectBase) this.CAReconRecords).Cache.AllowDelete = flag11;
      ((PXSelectBase) this.CAReconRecords).Cache.AllowInsert = flag9;
      ((PXSelectBase) this.CAReconTranRecords).Cache.AllowDelete = false;
      ((PXSelectBase) this.CAReconTranRecords).Cache.AllowInsert = false;
      ((PXSelectBase) this.CAReconTranRecords).Cache.AllowUpdate = flag10;
      PXUIFieldAttribute.SetEnabled<CARecon.curyBalance>(cache, (object) row, !valueOrDefault1 && !valueOrDefault2 && !flag4);
      PXUIFieldAttribute.SetEnabled<CARecon.hold>(cache, (object) row, !valueOrDefault1 && !valueOrDefault2);
      PXUIFieldAttribute.SetEnabled<CARecon.reconDate>(cache, (object) row, !valueOrDefault1 && !valueOrDefault2 && !flag4);
      PXUIFieldAttribute.SetVisible<CARecon.loadDocumentsTill>(cache, (object) row, !valueOrDefault1 && !valueOrDefault2 && !flag4);
      PXCache pxCache = cache;
      CARecon caRecon1 = row;
      nullable1 = PXResultset<CASetup>.op_Implicit(PXSelectBase<CASetup, PXSelect<CASetup>.Config>.Select((PXGraph) this, Array.Empty<object>())).SkipVoided;
      bool? nullable2 = row.SkipVoided;
      int num7 = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) ? 1 : 0;
      PXUIFieldAttribute.SetVisible<CARecon.skipVoided>(pxCache, (object) caRecon1, num7 != 0);
      CashAccount current = ((PXSelectBase<CashAccount>) this.cashaccount).Current;
      int num8;
      if (current == null)
      {
        num8 = 0;
      }
      else
      {
        nullable2 = current.MatchToBatch;
        num8 = nullable2.HasValue ? 1 : 0;
      }
      int num9;
      if (num8 != 0)
      {
        nullable2 = row.ShowBatchPayments;
        if (nullable2.HasValue)
        {
          nullable2 = ((PXSelectBase<CashAccount>) this.cashaccount).Current.MatchToBatch;
          nullable1 = row.ShowBatchPayments;
          num9 = !(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue) ? 1 : 0;
          goto label_42;
        }
      }
      num9 = 0;
label_42:
      bool flag12 = num9 != 0;
      PXUIFieldAttribute.SetVisible<CARecon.showBatchPayments>(cache, (object) row, flag12);
      bool flag13 = false;
      bool flag14 = false;
      bool flag15 = false;
      bool flag16 = false;
      if (row.CashAccountID.HasValue)
      {
        if (((PXSelectBase) this.CAReconRecords).Cache.GetStatus((object) row) == 2)
        {
          flag14 = PXResultset<CARecon>.op_Implicit(PXSelectBase<CARecon, PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>>, OrderBy<Desc<CARecon.reconDate, Desc<CARecon.reconNbr>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) row.CashAccountID
          })) != null;
        }
        else
        {
          CARecon caRecon2 = PXResultset<CARecon>.op_Implicit(PXSelectBase<CARecon, PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.reconNbr, Greater<Required<CARecon.reconNbr>>>>, OrderBy<Asc<CARecon.reconDate, Asc<CARecon.reconNbr>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) row.CashAccountID,
            (object) row.ReconNbr
          }));
          CARecon caRecon3 = PXResultset<CARecon>.op_Implicit(PXSelectBase<CARecon, PXSelectReadonly<CARecon, Where<CARecon.cashAccountID, Equal<Required<CARecon.cashAccountID>>, And<CARecon.reconNbr, Less<Required<CARecon.reconNbr>>>>, OrderBy<Desc<CARecon.reconDate, Desc<CARecon.reconNbr>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) row.CashAccountID,
            (object) row.ReconNbr
          }));
          flag13 = caRecon2 != null;
          flag14 = caRecon3 > null;
        }
        flag16 = flag14;
        flag15 = flag13;
      }
      ((PXAction) this.Next).SetEnabled(flag13);
      ((PXAction) this.Previous).SetEnabled(flag14);
      ((PXAction) this.First).SetEnabled(flag16);
      ((PXAction) this.Last).SetEnabled(flag15);
      ((PXAction) this.ToggleCleared).SetEnabled(flag10);
      ((PXAction) this.ToggleReconciled).SetEnabled(flag10);
      ((PXAction) this.ReconcileProcessed).SetEnabled(flag10);
      bool flag17 = !((PXGraph) this).Accessinfo.CuryViewState;
      PXUIFieldAttribute.SetVisible<CARecon.curyBegBalance>(cache, (object) row, flag17);
      PXUIFieldAttribute.SetVisible<CARecon.curyBalance>(cache, (object) row, flag17);
      PXUIFieldAttribute.SetVisible<CARecon.curyDiffBalance>(cache, (object) row, flag17);
      PXUIFieldAttribute.SetVisible<CARecon.curyReconciledBalance>(cache, (object) row, flag17);
      this.SetLoadDocumentsTillWarning(cache, row);
    }
  }

  private void SetLoadDocumentsTillWarning(PXCache cache, CARecon header)
  {
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (header.ReconDate.HasValue && header.LoadDocumentsTill.HasValue)
    {
      DateTime? loadDocumentsTill = header.LoadDocumentsTill;
      DateTime? reconDate = header.ReconDate;
      if ((loadDocumentsTill.HasValue & reconDate.HasValue ? (loadDocumentsTill.GetValueOrDefault() < reconDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        propertyException = new PXSetPropertyException(header.IsUserLoadDocumentsTill.GetValueOrDefault() ? "The date is earlier than the reconciliation date. Note, that the list of documents will be filtered by the specified date." : "The date in the Load Documents Up To box is earlier than the Reconciliation Date. You can change the date in the Load Documents Up To box, but note that it may affect the system performance.", (PXErrorLevel) 2);
    }
    cache.RaiseExceptionHandling<CARecon.loadDocumentsTill>((object) header, (object) header.LoadDocumentsTill, (Exception) propertyException);
  }

  private bool IsApprovalRequired(CARecon statement, PXCache cache)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() & this.Approval.GetAssignedMaps(statement, cache).Any<ApprovalMap>();
  }

  protected virtual void CARecon_CashAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is CARecon row) || e.NewValue == null)
      return;
    PXResultset<CashAccount> source = PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    });
    CashAccount cashAccount = PXResult<CashAccount>.op_Implicit(source != null ? ((IQueryable<PXResult<CashAccount>>) source).First<PXResult<CashAccount>>() : (PXResult<CashAccount>) null);
    if (cashAccount == null)
      return;
    bool? active = cashAccount.Active;
    bool flag = false;
    if (!(active.GetValueOrDefault() == flag & active.HasValue))
      return;
    PXCache pxCache = sender;
    object newValue = e.NewValue;
    PXSetPropertyException propertyException = new PXSetPropertyException("The cash account {0} is deactivated on the Cash Accounts (CA202000) form.", (PXErrorLevel) 5, new object[1]
    {
      (object) cashAccount.CashAccountCD
    });
    pxCache.RaiseExceptionHandling<CARecon.cashAccountID>((object) row, newValue, (Exception) propertyException);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CARecon_Hold_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    CARecon row = (CARecon) e.Row;
    if ((bool) e.NewValue)
      return;
    Decimal? curyDiffBalance = row.CuryDiffBalance;
    if (!curyDiffBalance.HasValue)
      return;
    curyDiffBalance = row.CuryDiffBalance;
    Decimal num = 0M;
    if (curyDiffBalance.GetValueOrDefault() == num & curyDiffBalance.HasValue)
      return;
    cache.RaiseExceptionHandling<CARecon.curyBalance>((object) row, (object) row.CuryBalance, (Exception) new PXSetPropertyException((IBqlTable) row, "The document is out of the balance."));
    if (!row.ExcludeFromApproval.GetValueOrDefault())
      throw new PXSetPropertyException((IBqlTable) row, "The document is out of the balance.");
  }

  protected virtual void CARecon_ReconDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    CARecon row = (CARecon) e.Row;
    if (!row.CashAccountID.HasValue || e.NewValue == null)
      return;
    DateTime? lastReconDate = row.LastReconDate;
    if (!lastReconDate.HasValue)
      return;
    DateTime date1 = ((DateTime) e.NewValue).Date;
    lastReconDate = row.LastReconDate;
    DateTime date2 = lastReconDate.Value.Date;
    if (date1 <= date2)
      throw new PXSetPropertyException("Reconciliation date must be later than the date of the previous released Reconciliation Statement");
  }

  protected virtual void CARecon_ReconDate_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    CARecon row = (CARecon) e.Row;
    if (row == null)
      return;
    row.LoadDocumentsTill = row.ReconDate;
    foreach (PXResult<CAReconEntry.CATranExt> pxResult in PXSelectBase<CAReconEntry.CATranExt, PXSelect<CAReconEntry.CATranExt, Where<CAReconEntry.CATranExt.reconNbr, Equal<Required<CARecon.reconNbr>>, And<CAReconEntry.CATranExt.cashAccountID, Equal<Required<CARecon.cashAccountID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.ReconNbr,
      (object) row.CashAccountID
    }))
    {
      CAReconEntry.CATranExt voidingTran = PXResult<CAReconEntry.CATranExt>.op_Implicit(pxResult);
      bool? nullable = voidingTran.Reconciled;
      if (nullable.GetValueOrDefault())
      {
        nullable = row.SkipVoided;
        if (!nullable.GetValueOrDefault() || !this.Skip((CATran) voidingTran))
        {
          CAReconEntry.CATranExt copy = PXCache<CAReconEntry.CATranExt>.CreateCopy(voidingTran);
          copy.ReconDate = row.ReconDate;
          ((PXSelectBase<CAReconEntry.CATranExt>) this.CAReconTranRecords).Update(copy);
        }
      }
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<CARecon.loadDocumentsTill> e)
  {
    CARecon row = (CARecon) e.Row;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CARecon.loadDocumentsTill>>) e).Cache.SetValue<CARecon.isUserLoadDocumentsTill>((object) row, (object) true);
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (BatchRefAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<CABatch.batchSeqNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account")]
  protected virtual void _(PX.Data.Events.CacheAttached<BAccountR.acctCD> sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account Name")]
  protected virtual void _(PX.Data.Events.CacheAttached<BAccountR.acctName> sender)
  {
  }

  [PXCacheName("CA Transaction")]
  [Serializable]
  public class CATranExt : CATran
  {
    [PXDBTimestamp]
    public override byte[] tstamp { get; set; }

    [PXCury(typeof (CATran.curyID))]
    [PXUIField]
    public virtual Decimal? CuryEffTranAmt { get; set; }

    [PXDecimal(4)]
    [PXUIField(DisplayName = "Tran. Amount")]
    public virtual Decimal? EffTranAmt { get; set; }

    [PXDecimal]
    [PXUIField(DisplayName = "Receipt")]
    public virtual Decimal? CuryEffDebitAmt
    {
      [PXDependsOnFields(new System.Type[] {typeof (CATran.drCr), typeof (CATran.curyTranAmt)})] get
      {
        if (this.DrCr == null)
          return new Decimal?();
        return !(this.DrCr == "D") ? new Decimal?(0M) : this.CuryEffTranAmt;
      }
      set
      {
      }
    }

    [PXDecimal]
    [PXUIField(DisplayName = "Disbursement")]
    public virtual Decimal? CuryEffCreditAmt
    {
      [PXDependsOnFields(new System.Type[] {typeof (CATran.drCr), typeof (CAReconEntry.CATranExt.curyEffTranAmt)})] get
      {
        if (this.DrCr == null)
          return new Decimal?();
        if (!(this.DrCr == "C"))
          return new Decimal?(0M);
        Decimal? curyEffTranAmt = this.CuryEffTranAmt;
        return !curyEffTranAmt.HasValue ? new Decimal?() : new Decimal?(-curyEffTranAmt.GetValueOrDefault());
      }
      set
      {
      }
    }

    [PXDecimal]
    [PXUIField(DisplayName = "Receipt")]
    public virtual Decimal? CuryEffClearedDebitAmt
    {
      [PXDependsOnFields(new System.Type[] {typeof (CAReconEntry.CATranExt.cleared), typeof (CATran.drCr), typeof (CAReconEntry.CATranExt.curyEffDebitAmt)})] get
      {
        return new Decimal?(this.Cleared.GetValueOrDefault() ? this.CuryEffDebitAmt.GetValueOrDefault() : 0M);
      }
      set
      {
      }
    }

    [PXDecimal]
    [PXUIField(DisplayName = "Disbursement")]
    public virtual Decimal? CuryEffClearedCreditAmt
    {
      [PXDependsOnFields(new System.Type[] {typeof (CAReconEntry.CATranExt.cleared), typeof (CATran.drCr), typeof (CATran.curyTranAmt)})] get
      {
        if (!this.Cleared.GetValueOrDefault() || !(this.DrCr == "C"))
          return new Decimal?(0M);
        Decimal? curyEffTranAmt = this.CuryEffTranAmt;
        return !curyEffTranAmt.HasValue ? new Decimal?() : new Decimal?(-curyEffTranAmt.GetValueOrDefault());
      }
      set
      {
      }
    }

    [PXDecimal]
    [PXFormula(null, typeof (SumCalc<CARecon.reconciledDebits>))]
    public virtual Decimal? ReconciledDebit
    {
      [PXDependsOnFields(new System.Type[] {typeof (CAReconEntry.CATranExt.reconciled), typeof (CATran.drCr), typeof (CAReconEntry.CATranExt.effTranAmt), typeof (CAReconEntry.CATranExt.skipCount)})] get
      {
        return this.SkipCount.GetValueOrDefault() ? (!this.Reconciled.GetValueOrDefault() || !(this.DrCr == "C") ? new Decimal?(0M) : this.EffTranAmt) : (!this.Reconciled.GetValueOrDefault() || !(this.DrCr == "D") ? new Decimal?(0M) : this.EffTranAmt);
      }
      set
      {
      }
    }

    [PXDecimal]
    [PXFormula(null, typeof (SumCalc<CARecon.curyReconciledDebits>))]
    public virtual Decimal? CuryReconciledDebit
    {
      [PXDependsOnFields(new System.Type[] {typeof (CAReconEntry.CATranExt.reconciled), typeof (CATran.drCr), typeof (CAReconEntry.CATranExt.curyEffTranAmt), typeof (CAReconEntry.CATranExt.skipCount)})] get
      {
        return this.SkipCount.GetValueOrDefault() ? (!this.Reconciled.GetValueOrDefault() || !(this.DrCr == "C") ? new Decimal?(0M) : this.CuryEffTranAmt) : (!this.Reconciled.GetValueOrDefault() || !(this.DrCr == "D") ? new Decimal?(0M) : this.CuryEffTranAmt);
      }
      set
      {
      }
    }

    [PXDecimal]
    [PXFormula(null, typeof (SumCalc<CARecon.reconciledCredits>))]
    public virtual Decimal? ReconciledCredit
    {
      [PXDependsOnFields(new System.Type[] {typeof (CAReconEntry.CATranExt.reconciled), typeof (CATran.drCr), typeof (CAReconEntry.CATranExt.effTranAmt), typeof (CAReconEntry.CATranExt.skipCount)})] get
      {
        if (this.SkipCount.GetValueOrDefault())
        {
          if (!this.Reconciled.GetValueOrDefault() || !(this.DrCr == "D"))
            return new Decimal?(0M);
          Decimal? effTranAmt = this.EffTranAmt;
          return !effTranAmt.HasValue ? new Decimal?() : new Decimal?(-effTranAmt.GetValueOrDefault());
        }
        if (!this.Reconciled.GetValueOrDefault() || !(this.DrCr == "C"))
          return new Decimal?(0M);
        Decimal? effTranAmt1 = this.EffTranAmt;
        return !effTranAmt1.HasValue ? new Decimal?() : new Decimal?(-effTranAmt1.GetValueOrDefault());
      }
      set
      {
      }
    }

    [PXDecimal]
    [PXFormula(null, typeof (SumCalc<CARecon.curyReconciledCredits>))]
    public virtual Decimal? CuryReconciledCredit
    {
      [PXDependsOnFields(new System.Type[] {typeof (CAReconEntry.CATranExt.reconciled), typeof (CATran.drCr), typeof (CAReconEntry.CATranExt.curyEffTranAmt), typeof (CAReconEntry.CATranExt.skipCount)})] get
      {
        if (this.SkipCount.GetValueOrDefault())
        {
          if (!this.Reconciled.GetValueOrDefault() || !(this.DrCr == "D"))
            return new Decimal?(0M);
          Decimal? curyEffTranAmt = this.CuryEffTranAmt;
          return !curyEffTranAmt.HasValue ? new Decimal?() : new Decimal?(-curyEffTranAmt.GetValueOrDefault());
        }
        if (!this.Reconciled.GetValueOrDefault() || !(this.DrCr == "C"))
          return new Decimal?(0M);
        Decimal? curyEffTranAmt1 = this.CuryEffTranAmt;
        return !curyEffTranAmt1.HasValue ? new Decimal?() : new Decimal?(-curyEffTranAmt1.GetValueOrDefault());
      }
      set
      {
      }
    }

    /// <summary>
    /// This flag is required for the specific situation when SkipVoids is true and
    /// transaction was included to Reconcillation Statement before it was voided and then voiding transaction was included.
    /// </summary>
    /// <value>
    /// If this flag is set to true Transaction will not be counted in Reconcillation Statement header and
    /// Transaction's credit amount will be subtracted from Reconcillation Statement's debit amount
    /// instead of adding to Reconcillation Statement's credit amount and
    /// Transaction's debit amount will be subtracted from Reconcillation Statement's credit amount
    /// instead of adding to Reconcillation Statement's debit amount.
    /// </value>
    [PXBool]
    public virtual bool? SkipCount { get; set; }

    [PXInt]
    [PXFormula(null, typeof (SumCalc<CARecon.countDebit>))]
    public virtual int? CountDebit
    {
      [PXDependsOnFields(new System.Type[] {typeof (CAReconEntry.CATranExt.reconciled), typeof (CATran.drCr), typeof (CAReconEntry.CATranExt.skipCount)})] get
      {
        return new int?(!this.Reconciled.GetValueOrDefault() || !(this.DrCr == "D") || this.SkipCount.GetValueOrDefault() ? 0 : 1);
      }
      set
      {
      }
    }

    [PXInt]
    [PXFormula(null, typeof (SumCalc<CARecon.countCredit>))]
    public virtual int? CountCredit
    {
      [PXDependsOnFields(new System.Type[] {typeof (CAReconEntry.CATranExt.reconciled), typeof (CATran.drCr), typeof (CAReconEntry.CATranExt.skipCount)})] get
      {
        return new int?(!this.Reconciled.GetValueOrDefault() || !(this.DrCr == "C") || this.SkipCount.GetValueOrDefault() ? 0 : 1);
      }
      set
      {
      }
    }

    [PXBool]
    [PXUIField(DisplayName = "Voided")]
    public override bool? Voided { get; set; }

    [PXLong]
    public virtual long? VoidingTranID { get; set; }

    [PXBool]
    public virtual bool? VoidingNotReleased { get; set; }

    [PXString(11, IsFixed = true)]
    [PXUIField(DisplayName = "Status", Enabled = false)]
    [CAReconEntry.CATranExt.ExtTranStatus.List]
    public override string Status
    {
      [PXDependsOnFields(new System.Type[] {typeof (CATran.posted), typeof (CAReconEntry.CATranExt.released), typeof (CATran.hold), typeof (CAReconEntry.CATranExt.voided), typeof (CAReconEntry.CATranExt.voidingNotReleased)})] get
      {
        if (this.VoidingNotReleased.GetValueOrDefault())
          return "PV";
        if (this.Voided.GetValueOrDefault())
          return "V";
        if (this.Posted.GetValueOrDefault())
          return this.Released.GetValueOrDefault() ? "P" : "U";
        if (this.Released.GetValueOrDefault() && !this.Posted.GetValueOrDefault())
          return "R";
        if (this.Hold.GetValueOrDefault())
          return "H";
        return this.PendingApproval.GetValueOrDefault() ? "D" : "B";
      }
      set
      {
      }
    }

    [PXBool]
    public bool? ProcessedFromFeed { get; set; }

    public new abstract class tranID : BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    CAReconEntry.CATranExt.tranID>
    {
    }

    public new abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CAReconEntry.CATranExt.cashAccountID>
    {
    }

    public new abstract class origModule : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranExt.origModule>
    {
    }

    public new abstract class origTranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranExt.origTranType>
    {
    }

    public new abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranExt.origRefNbr>
    {
    }

    public new abstract class origLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CAReconEntry.CATranExt.origLineNbr>
    {
    }

    public new abstract class isPaymentChargeTran : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranExt.isPaymentChargeTran>
    {
    }

    public new abstract class reconNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranExt.reconNbr>
    {
    }

    public new abstract class tranDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CAReconEntry.CATranExt.tranDate>
    {
    }

    public new abstract class voidedTranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      CAReconEntry.CATranExt.voidedTranID>
    {
    }

    public new abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranExt.released>
    {
    }

    public new abstract class reconciled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranExt.reconciled>
    {
    }

    public new abstract class reconDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CAReconEntry.CATranExt.reconDate>
    {
    }

    public new abstract class cleared : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CAReconEntry.CATranExt.cleared>
    {
    }

    public new abstract class clearDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CAReconEntry.CATranExt.clearDate>
    {
    }

    public new class Tstamp : BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    CAReconEntry.CATranExt.Tstamp>
    {
    }

    public abstract class curyEffTranAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CAReconEntry.CATranExt.curyEffTranAmt>
    {
    }

    public abstract class effTranAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CAReconEntry.CATranExt.effTranAmt>
    {
    }

    public abstract class curyEffDebitAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CAReconEntry.CATranExt.curyEffDebitAmt>
    {
    }

    public abstract class curyEffCreditAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CAReconEntry.CATranExt.curyEffCreditAmt>
    {
    }

    public abstract class curyEffClearedDebitAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CAReconEntry.CATranExt.curyEffClearedDebitAmt>
    {
    }

    public abstract class curyEffClearedCreditAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CAReconEntry.CATranExt.curyEffClearedCreditAmt>
    {
    }

    public abstract class reconciledDebit : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CAReconEntry.CATranExt.reconciledDebit>
    {
    }

    public abstract class curyReconciledDebit : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CAReconEntry.CATranExt.curyReconciledDebit>
    {
    }

    public abstract class reconciledCredit : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CAReconEntry.CATranExt.reconciledCredit>
    {
    }

    public abstract class curyReconciledCredit : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CAReconEntry.CATranExt.curyReconciledCredit>
    {
    }

    public abstract class skipCount : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CAReconEntry.CATranExt.skipCount>
    {
    }

    public abstract class countDebit : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CAReconEntry.CATranExt.countDebit>
    {
    }

    public abstract class countCredit : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CAReconEntry.CATranExt.countCredit>
    {
    }

    public new abstract class voided : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CAReconEntry.CATranExt.voided>
    {
    }

    public abstract class voidingTranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      CAReconEntry.CATranExt.voidingTranID>
    {
    }

    public abstract class voidingNotReleased : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranExt.voidingNotReleased>
    {
    }

    public new abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranExt.status>
    {
    }

    public abstract class processedFromFeed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranExt.processedFromFeed>
    {
    }

    public class ExtTranStatus : BatchStatus
    {
      public const string PartiallyVoided = "PV";

      public new class ListAttribute : PXStringListAttribute
      {
        public ListAttribute()
          : base(new string[11]
          {
            "H",
            "B",
            "U",
            "P",
            "C",
            "V",
            "R",
            "Q",
            "S",
            "PV",
            "D"
          }, new string[11]
          {
            "On Hold",
            "Balanced",
            "Unposted",
            "Posted",
            "Completed",
            "Voided",
            "Released",
            "Partially Released",
            "Scheduled",
            "Void Pending",
            "Pending Approval"
          })
        {
        }
      }
    }
  }

  [UseIndexWhenJoined("CATran_VoidedTranID", On = typeof (On<CAReconEntry.CATranVoiding.voidedTranID, Equal<CAReconEntry.CATranExt.tranID>>))]
  [PXCacheName("CA Transaction (alias)")]
  [Serializable]
  public class CATranVoiding : CATran
  {
    public new abstract class tranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.tranID>
    {
    }

    public new abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.cashAccountID>
    {
    }

    public new abstract class origModule : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.origModule>
    {
    }

    public new abstract class origTranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.origTranType>
    {
    }

    public new abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.origRefNbr>
    {
    }

    public new abstract class origLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.origLineNbr>
    {
    }

    public new abstract class isPaymentChargeTran : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.isPaymentChargeTran>
    {
    }

    public new abstract class reconNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.reconNbr>
    {
    }

    public new abstract class tranDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.tranDate>
    {
    }

    public new abstract class voidedTranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.voidedTranID>
    {
    }

    public new abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.released>
    {
    }

    public new abstract class reconciled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.reconciled>
    {
    }

    public new abstract class reconDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.reconDate>
    {
    }

    public new abstract class cleared : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.cleared>
    {
    }

    public new abstract class clearDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CAReconEntry.CATranVoiding.clearDate>
    {
    }
  }

  [Serializable]
  public class CATranInBatch : CATran
  {
    public void CopyFrom(CATran source)
    {
      this.ClearDate = source.ClearDate;
      this.Cleared = source.Cleared;
      this.Reconciled = source.Reconciled;
      this.ReconDate = source.ReconDate;
      this.ReconNbr = source.ReconNbr;
    }

    public new abstract class tranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.tranID>
    {
    }

    public new abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.cashAccountID>
    {
    }

    public new abstract class origModule : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.origModule>
    {
    }

    public new abstract class origTranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.origTranType>
    {
    }

    public new abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.origRefNbr>
    {
    }

    public new abstract class origLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.origLineNbr>
    {
    }

    public new abstract class isPaymentChargeTran : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.isPaymentChargeTran>
    {
    }

    public new abstract class reconNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.reconNbr>
    {
    }

    public new abstract class tranDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.tranDate>
    {
    }

    public new abstract class voidedTranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.voidedTranID>
    {
    }

    public new abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.released>
    {
    }

    public new abstract class reconciled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.reconciled>
    {
    }

    public new abstract class reconDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.reconDate>
    {
    }

    public new abstract class cleared : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.cleared>
    {
    }

    public new abstract class clearDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CAReconEntry.CATranInBatch.clearDate>
    {
    }
  }

  [Serializable]
  public class CABatchDetail2 : CABatchDetail
  {
    public new abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CABatchDetail2.batchNbr>
    {
    }

    public new abstract class origModule : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CABatchDetail2.origModule>
    {
    }

    public new abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CABatchDetail2.origDocType>
    {
    }

    public new abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CAReconEntry.CABatchDetail2.origRefNbr>
    {
    }
  }

  [Serializable]
  public class CABankTranMatch2 : CABankTranMatch
  {
    public new abstract class cATranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      CAReconEntry.CABankTranMatch2.cATranID>
    {
    }

    public new abstract class tranID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CAReconEntry.CABankTranMatch2.tranID>
    {
    }
  }

  internal class CATranExtRowUpdatingContext : IDisposable
  {
    private CAReconEntry Graph { get; set; }

    public CATranExtRowUpdatingContext(CAReconEntry graph)
    {
      this.Graph = graph;
      graph.SearchInCache = false;
    }

    public void Dispose() => this.Graph.SearchInCache = true;
  }

  public class AddTransactionExtension : AddTransactionExtensionBase<CAReconEntry, CARecon>
  {
    public PXAction<CARecon> CreateAdjustment;

    public static bool IsActive() => true;

    [PXUIField]
    [PXProcessButton]
    public virtual IEnumerable createAdjustment(PXAdapter adapter)
    {
      CAReconEntry.AddTransactionExtension transactionExtension = this;
      CARecon current = ((PXSelectBase<CARecon>) transactionExtension.Base.CAReconRecords).Current;
      if (current != null && transactionExtension.Base.IsHeaderUpdateEnabled(current))
      {
        if (!((PXSelectBase<AddTrxFilter>) transactionExtension.AddFilter).Current.DocumentCreationContext.GetValueOrDefault())
          ((PXAction) transactionExtension.Base.Save).Press();
        transactionExtension.SetDocumentCreationContext(true);
        // ISSUE: method pointer
        WebDialogResult webDialogResult = ((PXSelectBase<AddTrxFilter>) transactionExtension.AddFilter).AskExt(new PXView.InitializePanel((object) transactionExtension, __methodptr(\u003CcreateAdjustment\u003Eb__2_0)), true);
        if (webDialogResult == 1 || webDialogResult == 6)
        {
          bool releaseTransaction = webDialogResult == 6;
          using (new PXTimeStampScope(((PXGraph) transactionExtension.Base).TimeStamp))
            AddTrxFilter.VerifyAndCreateTransaction((PXGraph) transactionExtension.Base, transactionExtension.AddFilter, (PXSelectBase<PX.Objects.CM.CurrencyInfo>) transactionExtension.Base.currencyinfo, releaseTransaction);
          transactionExtension.SetDocumentCreationContext(false);
        }
        ((PXSelectBase) transactionExtension.AddFilter).Cache.Clear();
      }
      yield return (object) current;
    }

    private void SetDocumentCreationContext(bool isActive)
    {
      ((PXSelectBase) this.AddFilter).Cache.SetValueExt<AddTrxFilter.documentCreationContext>((object) ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current, (object) isActive);
      OpenPeriodAttribute.SetValidatePeriod<AddTrxFilter.finPeriodID>(((PXSelectBase) this.AddFilter).Cache, (object) null, isActive ? PeriodValidation.DefaultSelectUpdate : PeriodValidation.Nothing);
      if (!isActive)
        return;
      this.OptionallySetCurrencyInfo();
    }

    private void OptionallySetCurrencyInfo()
    {
      PXCache cach = ((PXGraph) this.Base).Caches[typeof (PX.Objects.CM.CurrencyInfo)];
      if (((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo).Current == null)
        return;
      bool flag = false;
      foreach (PX.Objects.CM.CurrencyInfo currencyInfo in cach.Inserted)
      {
        if (object.Equals((object) currencyInfo.CuryInfoID, (object) (long?) ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo).Current?.CuryInfoID))
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      long? curyInfoId1 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo).Current.CuryInfoID;
      long num = 0;
      if (curyInfoId1.GetValueOrDefault() > num & curyInfoId1.HasValue)
      {
        long? curyInfoId2 = ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current.CuryInfoID;
        long? nullable1 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo).Current.CuryInfoID;
        if (curyInfoId2.GetValueOrDefault() == nullable1.GetValueOrDefault() & curyInfoId2.HasValue == nullable1.HasValue)
          return;
        PX.Objects.CM.CurrencyInfo copy = (PX.Objects.CM.CurrencyInfo) cach.CreateCopy((object) ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo).Current);
        PX.Objects.CM.CurrencyInfo currencyInfo = copy;
        nullable1 = new long?();
        long? nullable2 = nullable1;
        currencyInfo.CuryInfoID = nullable2;
        ((PXSelectBase) this.AddFilter).Cache.SetValue<AddTrxFilter.curyInfoID>((object) ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current, (object) ((PX.Objects.CM.CurrencyInfo) cach.Insert((object) copy)).CuryInfoID);
      }
      else
        ((PXSelectBase) this.Base.currencyinfo).Cache.SetStatus((object) ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo).Current, (PXEntryStatus) 2);
    }

    public virtual void PerformOnPersist(
      CAReconEntry.AddTransactionExtension.PerformOnPersistDelegate baseMethod)
    {
      baseMethod();
      foreach (PX.Objects.CM.CurrencyInfo currencyInfo in ((PXGraph) this.Base).Caches[typeof (PX.Objects.CM.CurrencyInfo)].Cached)
      {
        long? curyInfoId1 = ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current.CuryInfoID;
        long? curyInfoId2 = currencyInfo.CuryInfoID;
        long? nullable = curyInfoId1;
        if (curyInfoId2.GetValueOrDefault() == nullable.GetValueOrDefault() & curyInfoId2.HasValue == nullable.HasValue)
          ((PXGraph) this.Base).Caches[typeof (PX.Objects.CM.CurrencyInfo)].SetStatus((object) currencyInfo, (PXEntryStatus) 2);
      }
    }

    [PXOverride]
    public virtual void Clear(System.Action base_Clear)
    {
      if (((PXSelectBase<AddTrxFilter>) this.AddFilter).Current != null)
      {
        ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current.TranDate = new DateTime?();
        ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current.FinPeriodID = (string) null;
        ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current.CuryInfoID = new long?();
      }
      base_Clear();
    }

    protected virtual void CARecon_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
    {
      CARecon row = (CARecon) e.Row;
      if (row == null)
        return;
      AddTrxFilter current = ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current;
      if ((current != null ? (!current.DocumentCreationContext.GetValueOrDefault() ? 1 : 0) : 1) != 0)
        OpenPeriodAttribute.SetValidatePeriod<AddTrxFilter.finPeriodID>(((PXSelectBase) this.AddFilter).Cache, (object) null, PeriodValidation.Nothing);
      if (((PXSelectBase<CashAccount>) this.Base.cashaccount).Current != null && ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current != null)
      {
        int? cashAccountId1 = ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current.CashAccountID;
        int? cashAccountId2 = row.CashAccountID;
        if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue))
          ((PXSelectBase) this.AddFilter).Cache.SetValueExt<AddTrxFilter.cashAccountID>((object) ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current, (object) ((PXSelectBase<CashAccount>) this.Base.cashaccount).Current.CashAccountCD);
      }
      ((PXSelectBase) this.AddFilter).Cache.RaiseRowSelected((object) ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current);
      bool? nullable;
      int num1;
      if (!row.Hold.GetValueOrDefault())
      {
        nullable = row.Approved;
        if (!nullable.GetValueOrDefault())
        {
          nullable = row.Rejected;
          if (!nullable.GetValueOrDefault())
          {
            nullable = row.ExcludeFromApproval;
            num1 = !nullable.GetValueOrDefault() ? 1 : 0;
            goto label_11;
          }
        }
      }
      num1 = 0;
label_11:
      bool flag1 = num1 != 0;
      nullable = row.Approved;
      int num2;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.Rejected;
        if (!nullable.GetValueOrDefault())
        {
          num2 = 0;
          goto label_15;
        }
      }
      nullable = row.ExcludeFromApproval;
      num2 = !nullable.GetValueOrDefault() ? 1 : 0;
label_15:
      bool flag2 = num2 != 0;
      ((PXAction) this.CreateAdjustment).SetEnabled(this.Base.IsHeaderUpdateEnabled(row) && !flag1 && !flag2);
    }

    protected virtual void CurrencyInfo_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
    {
      if (((PXSelectBase<AddTrxFilter>) this.AddFilter).Current == null)
        return;
      long? curyInfoId1 = ((PXSelectBase<AddTrxFilter>) this.AddFilter).Current.CuryInfoID;
      long? curyInfoId2 = ((PX.Objects.CM.CurrencyInfo) e.Row).CuryInfoID;
      if (!(curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue))
        return;
      ((CancelEventArgs) e).Cancel = true;
    }

    public delegate void PerformOnPersistDelegate();
  }
}
