// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FACostDetailsInq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FA;

[TableAndChartDashboardType]
public class FACostDetailsInq : PXGraph<FACostDetailsInq>
{
  public PXCancel<AccountFilter> Cancel;
  public PXFilter<AccountFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelect<Transact> Transactions;

  public FACostDetailsInq()
  {
    ((PXSelectBase) this.Transactions).Cache.AllowInsert = false;
    ((PXSelectBase) this.Transactions).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Transactions).Cache.AllowDelete = false;
  }

  public virtual IEnumerable transactions(PXAdapter adapter)
  {
    FACostDetailsInq faCostDetailsInq = this;
    AccountFilter filter = ((PXSelectBase<AccountFilter>) faCostDetailsInq.Filter).Current;
    if (filter != null)
    {
      PXSelectBase<Transact> pxSelectBase = (PXSelectBase<Transact>) new PXSelectJoin<Transact, LeftJoin<FABook, On<FATran.bookID, Equal<FABook.bookID>>>, Where<FATran.assetID, Equal<Current<AccountFilter.assetID>>, And<FATran.released, Equal<True>>>>((PXGraph) faCostDetailsInq);
      if (!string.IsNullOrEmpty(filter.StartPeriodID))
        pxSelectBase.WhereAnd<Where<FATran.finPeriodID, GreaterEqual<Current<AccountFilter.startPeriodID>>>>();
      if (!string.IsNullOrEmpty(filter.EndPeriodID))
        pxSelectBase.WhereAnd<Where<FATran.finPeriodID, LessEqual<Current<AccountFilter.endPeriodID>>>>();
      if (filter.BookID.HasValue)
        pxSelectBase.WhereAnd<Where<FATran.bookID, Equal<Current<AccountFilter.bookID>>>>();
      if (filter.AccountID.HasValue)
        pxSelectBase.WhereAnd<Where<FATran.debitAccountID, Equal<Current<AccountFilter.accountID>>, Or<FATran.creditAccountID, Equal<Current<AccountFilter.accountID>>>>>();
      if (filter.SubID.HasValue)
        pxSelectBase.WhereAnd<Where<FATran.debitSubID, Equal<Current<AccountFilter.subID>>, Or<FATran.creditSubID, Equal<Current<AccountFilter.subID>>>>>();
      foreach (PXResult<Transact> pxResult in pxSelectBase.Select(Array.Empty<object>()))
      {
        Transact tran = PXResult<Transact>.op_Implicit(pxResult);
        int? nullable1 = filter.AccountID;
        int? nullable2;
        if (nullable1.HasValue)
        {
          nullable1 = filter.AccountID;
          nullable2 = tran.CreditAccountID;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            goto label_17;
        }
        nullable2 = filter.SubID;
        if (nullable2.HasValue)
        {
          nullable2 = filter.SubID;
          nullable1 = tran.CreditSubID;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            goto label_17;
        }
        Transact copy1 = (Transact) ((PXSelectBase) faCostDetailsInq.Transactions).Cache.CreateCopy((object) tran);
        copy1.AccountID = tran.CreditAccountID;
        copy1.SubID = tran.CreditSubID;
        copy1.CreditAmt = tran.TranAmt;
        copy1.DebitAmt = new Decimal?(0M);
        yield return (object) copy1;
label_17:
        nullable1 = filter.AccountID;
        if (nullable1.HasValue)
        {
          nullable1 = filter.AccountID;
          nullable2 = tran.DebitAccountID;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            goto label_22;
        }
        nullable2 = filter.SubID;
        if (nullable2.HasValue)
        {
          nullable2 = filter.SubID;
          nullable1 = tran.DebitSubID;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            goto label_22;
        }
        Transact copy2 = (Transact) ((PXSelectBase) faCostDetailsInq.Transactions).Cache.CreateCopy((object) tran);
        copy2.AccountID = tran.DebitAccountID;
        copy2.SubID = tran.DebitSubID;
        copy2.CreditAmt = new Decimal?(0M);
        copy2.DebitAmt = tran.TranAmt;
        yield return (object) copy2;
label_22:
        tran = (Transact) null;
      }
    }
  }
}
