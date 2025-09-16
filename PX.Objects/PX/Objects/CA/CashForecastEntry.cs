// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashForecastEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.CA;

public class CashForecastEntry : PXGraph<
#nullable disable
CashForecastEntry>
{
  public PXSave<CashAccount> Save;
  public PXCancel<CashAccount> Cancel;
  public PXFilter<CashForecastEntry.Filter> filter;
  [PXReadOnlyView]
  public PXSelect<CashAccount> filterCashAccounts;
  [PXImport(typeof (CashForecastEntry.Filter))]
  public PXSelect<CashForecastTran, Where<CashForecastTran.tranDate, GreaterEqual<Current<CashForecastEntry.Filter.startDate>>, And<CashForecastTran.cashAccountID, Equal<Current<CashAccount.cashAccountID>>>>, OrderBy<Asc<CashForecastTran.tranDate>>> cashForecastTrans;
  public PXSetup<CASetup> casetup;

  [CashAccount]
  [PXDefault(typeof (CashAccount.cashAccountID))]
  protected virtual void CashForecastTran_CashAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<CashAccount.active, Equal<True>>), "The cash account {0} is deactivated on the Cash Accounts (CA202000) form.", new Type[] {typeof (CashAccount.cashAccountCD)})]
  protected virtual void CashAccount_CashAccountCD_CacheAttached(PXCache sender)
  {
  }

  public CashForecastEntry()
  {
    CASetup current = ((PXSelectBase<CASetup>) this.casetup).Current;
  }

  protected virtual void CashAccount_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CashAccount row = (CashAccount) e.Row;
    if (row != null && !row.Active.GetValueOrDefault())
    {
      string str = $"The cash account {row.CashAccountCD} is deactivated on the Cash Accounts (CA202000) form.";
      ((PXSelectBase) this.cashForecastTrans).Cache.AllowInsert = false;
      ((PXSelectBase) this.cashForecastTrans).Cache.AllowUpdate = false;
      ((PXSelectBase) this.cashForecastTrans).Cache.AllowDelete = false;
      sender.RaiseExceptionHandling<CashAccount.cashAccountCD>((object) row, (object) row.CashAccountCD, (Exception) new PXSetPropertyException<CashAccount.cashAccountCD>(str, (PXErrorLevel) 4));
    }
    else
    {
      bool flag = row != null && row.CashAccountID.HasValue;
      ((PXSelectBase) this.cashForecastTrans).Cache.AllowInsert = flag;
      ((PXSelectBase) this.cashForecastTrans).Cache.AllowUpdate = flag;
    }
  }

  protected virtual void CashForecastTran_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CashForecastTran row = (CashForecastTran) e.Row;
    sender.SetDefaultExt<CashForecastTran.curyID>(e.Row);
  }

  protected virtual void CashForecastTran_TranDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CashForecastEntry.Filter current = ((PXSelectBase<CashForecastEntry.Filter>) this.filter).Current;
    if (current == null || !current.StartDate.HasValue)
      return;
    e.NewValue = (object) current.StartDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  [Serializable]
  public class Filter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _StartDate;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? StartDate
    {
      get => this._StartDate;
      set => this._StartDate = value;
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CashForecastEntry.Filter.startDate>
    {
    }
  }
}
