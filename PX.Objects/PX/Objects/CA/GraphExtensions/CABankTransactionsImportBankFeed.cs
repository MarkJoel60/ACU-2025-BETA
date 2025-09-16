// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.GraphExtensions.CABankTransactionsImportBankFeed
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CA.GraphExtensions;

public class CABankTransactionsImportBankFeed : 
  CABankFeedBase<CABankTransactionsImport, CABankTranHeader>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankFeedIntegration>();

  protected virtual void RowSelected(Events.RowSelected<CABankTranHeader> e)
  {
    CABankTranHeader row = e?.Row;
    if (row == null)
      return;
    bool flag = ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).SelectSingle(new object[1]
    {
      (object) row.CashAccountID
    }) != null && row.CashAccountID.HasValue;
    ((PXAction) this.Base.uploadFile).SetVisible(!flag);
    ((PXAction) this.retrieveTransactions).SetVisible(flag);
  }

  public override int? GetCashAccountId()
  {
    return ((PXSelectBase<CABankTranHeader>) this.Base.Header).Current?.CashAccountID;
  }
}
