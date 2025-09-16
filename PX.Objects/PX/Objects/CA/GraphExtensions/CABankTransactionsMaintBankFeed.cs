// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.GraphExtensions.CABankTransactionsMaintBankFeed
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CA.GraphExtensions;

public class CABankTransactionsMaintBankFeed : 
  CABankFeedBase<CABankTransactionsMaint, CABankTransactionsMaint.Filter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankFeedIntegration>();

  [PXOverride]
  public virtual IEnumerable UploadFile(
    PXAdapter adapter,
    CABankTransactionsMaintBankFeed.UploadFileDelegate baseMethod)
  {
    int? cashAccountId = this.GetCashAccountId();
    if (cashAccountId.HasValue && ((PXSelectBase<PX.Objects.CA.CASetup>) this.Base.CASetup).Current.ImportToSingleAccount.GetValueOrDefault())
    {
      CABankFeedDetail caBankFeedDetail = ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).SelectSingle(new object[1]
      {
        (object) cashAccountId
      });
      if (caBankFeedDetail != null)
        throw new PXException("The {0} cash account is already linked to the {1} bank account in the {2} bank feed.", new object[3]
        {
          (object) PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) cashAccountId
          }))?.CashAccountCD,
          (object) $"{caBankFeedDetail.AccountName}:{caBankFeedDetail.AccountMask}",
          (object) caBankFeedDetail.BankFeedID
        });
    }
    return baseMethod(adapter);
  }

  protected virtual void RowSelected(
    Events.RowSelected<CABankTransactionsMaint.Filter> e)
  {
    CABankTransactionsMaint.Filter row = e?.Row;
    TimeSpan timeSpan;
    Exception exception;
    ((PXAction) this.retrieveTransactions).SetEnabled(PXLongOperation.GetStatus(((PXGraph) this.Base).UID, ref timeSpan, ref exception) == null && row != null && row.CashAccountID.HasValue);
    if (row == null)
      return;
    bool flag = ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).SelectSingle(new object[1]
    {
      (object) row.CashAccountID
    }) != null;
    ((PXAction) this.Base.uploadFile).SetVisible(!flag);
    ((PXAction) this.retrieveTransactions).SetVisible(flag);
  }

  public override int? GetCashAccountId()
  {
    return ((PXSelectBase<CABankTransactionsMaint.Filter>) this.Base.TranFilter).Current?.CashAccountID;
  }

  public delegate IEnumerable UploadFileDelegate(PXAdapter adapter);
}
