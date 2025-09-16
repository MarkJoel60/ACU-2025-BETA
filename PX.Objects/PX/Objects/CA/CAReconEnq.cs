// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAReconEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CA;

[TableAndChartDashboardType]
public class CAReconEnq : PXGraph<
#nullable disable
CAReconEnq>
{
  public PXAction<CAEnqFilter> cancel;
  public PXAction<CAEnqFilter> viewDoc;
  public PXAction<CAEnqFilter> voided;
  public PXAction<CAEnqFilter> createRecon;
  public PXFilter<CAEnqFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelect<CARecon> CAReconRecords;
  public PXFilter<CAReconEnq.CashAccountFilter> cashAccountFilter;
  public PXSetup<CASetup> casetup;
  public PXSelect<CashAccount> cashAccount;

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    ((PXSelectBase) this.CAReconRecords).Cache.Clear();
    ((PXSelectBase) this.Filter).Cache.Clear();
    ((PXGraph) this).TimeStamp = (byte[]) null;
    PXLongOperation.ClearStatus(((PXGraph) this).UID);
    return adapter.Get();
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDoc(PXAdapter adapter)
  {
    CARecon current = ((PXSelectBase<CARecon>) this.CAReconRecords).Current;
    CAReconEntry instance = PXGraph.CreateInstance<CAReconEntry>();
    ((PXSelectBase<CARecon>) instance.CAReconRecords).Current = current;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable Voided(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CAReconEnq.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new CAReconEnq.\u003C\u003Ec__DisplayClass6_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass60.recon = ((PXSelectBase<CARecon>) this.CAReconRecords).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass60.recon != null)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass60, __methodptr(\u003CVoided\u003Eb__0)));
      ((PXSelectBase) this.CAReconRecords).View.RequestRefresh();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable CreateRecon(PXAdapter adapter)
  {
    if (((Dictionary<string, PXView>) ((PXGraph) this).Views).ContainsKey("cashAccountFilter"))
    {
      CAReconEnq.CashAccountFilter current = ((PXSelectBase<CAReconEnq.CashAccountFilter>) this.cashAccountFilter).Current;
      if (((PXGraph) this).Views["cashAccountFilter"].AskExt() == 1)
      {
        CAReconEntry.ReconCreate(PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<AddTrxFilter.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.CashAccountID
        })));
        ((PXSelectBase) this.CAReconRecords).View.RequestRefresh();
      }
    }
    return adapter.Get();
  }

  protected virtual IEnumerable careconrecords()
  {
    CAReconEnq caReconEnq1 = this;
    List<CAReconMessage> listMessages = PXLongOperation.GetCustomInfo(((PXGraph) caReconEnq1).UID) as List<CAReconMessage>;
    CAEnqFilter current = ((PXSelectBase<CAEnqFilter>) caReconEnq1.Filter).Current;
    CAReconEnq caReconEnq2 = caReconEnq1;
    object[] objArray = new object[6]
    {
      (object) current.CashAccountID,
      (object) current.CashAccountID,
      (object) current.StartDate,
      (object) current.StartDate,
      (object) current.EndDate,
      (object) current.EndDate
    };
    foreach (PXResult<CARecon> pxResult in PXSelectBase<CARecon, PXSelectJoin<CARecon, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CARecon.cashAccountID>>, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<CashAccount.accountID>, And<Match<PX.Objects.GL.Account, Current<AccessInfo.userName>>>>, InnerJoin<Sub, On<Sub.subID, Equal<CashAccount.subID>, And<Match<Sub, Current<AccessInfo.userName>>>>>>>, Where2<Where<CARecon.cashAccountID, Equal<Required<CAEnqFilter.cashAccountID>>, Or<Required<CAEnqFilter.cashAccountID>, IsNull>>, And2<Where<CARecon.reconDate, GreaterEqual<Required<CAEnqFilter.startDate>>, Or<Required<CAEnqFilter.startDate>, IsNull>>, And<Where<CARecon.reconDate, LessEqual<Required<CAEnqFilter.endDate>>, Or<Required<CAEnqFilter.endDate>, IsNull>>>>>, OrderBy<Asc<CARecon.reconDate, Asc<CARecon.reconNbr>>>>.Config>.Select((PXGraph) caReconEnq2, objArray))
    {
      CARecon caRecon = PXResult<CARecon>.op_Implicit(pxResult);
      TimeSpan timeSpan;
      Exception exception;
      if ((PXLongOperation.GetStatus(((PXGraph) caReconEnq1).UID, ref timeSpan, ref exception) == 3 || PXLongOperation.GetStatus(((PXGraph) caReconEnq1).UID, ref timeSpan, ref exception) == 2) && listMessages != null && listMessages.Count > 0)
      {
        for (int index = 0; index < listMessages.Count; ++index)
        {
          CAReconMessage caReconMessage = listMessages[index];
          int keyCashAccount = caReconMessage.KeyCashAccount;
          int? cashAccountId = caRecon.CashAccountID;
          int valueOrDefault = cashAccountId.GetValueOrDefault();
          if (keyCashAccount == valueOrDefault & cashAccountId.HasValue && caReconMessage.KeyReconNbr == caRecon.ReconNbr)
            ((PXSelectBase) caReconEnq1.CAReconRecords).Cache.RaiseExceptionHandling<CARecon.reconNbr>((object) caRecon, (object) caRecon.ReconNbr, (Exception) new PXSetPropertyException(caReconMessage.Message, caReconMessage.ErrorLevel));
        }
      }
      yield return (object) caRecon;
    }
  }

  protected virtual void CAEnqFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if ((CAEnqFilter) e.Row == null)
      return;
    PXCache cache1 = ((PXSelectBase) this.CAReconRecords).Cache;
    cache1.AllowInsert = false;
    cache1.AllowUpdate = false;
    cache1.AllowDelete = false;
    ((PXSelectBase) this.cashAccountFilter).Cache.RaiseRowSelected((object) ((PXSelectBase<CAReconEnq.CashAccountFilter>) this.cashAccountFilter).Current);
  }

  protected virtual void CashAccountFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CAReconEnq.CashAccountFilter row = (CAReconEnq.CashAccountFilter) e.Row;
    cache.AllowUpdate = true;
    PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CAReconEnq.CashAccountFilter.cashAccountID>(cache, (object) row, true);
  }

  protected virtual void CashAccountFilter_CashAccountID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if ((CAReconEnq.CashAccountFilter) e.Row == null)
      return;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountCD, Equal<Required<CashAccount.cashAccountCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (string) e.NewValue
    }));
    if (cashAccount != null && !cashAccount.Reconcile.GetValueOrDefault())
      throw new PXSetPropertyException("Cash account does not require reconciliation");
  }

  [PXHidden]
  [Serializable]
  public class CashAccountFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDefault]
    [CashAccount(typeof (Search<CashAccount.cashAccountID, Where<CashAccount.active, Equal<True>, And<CashAccount.reconcile, Equal<True>>>>))]
    public virtual int? CashAccountID { get; set; }

    public abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CAReconEnq.CashAccountFilter.cashAccountID>
    {
    }
  }
}
