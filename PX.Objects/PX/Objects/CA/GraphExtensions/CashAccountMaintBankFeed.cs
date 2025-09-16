// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.GraphExtensions.CashAccountMaintBankFeed
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CA.GraphExtensions;

public class CashAccountMaintBankFeed : PXGraphExtension<CashAccountMaint>
{
  public PXSelectJoin<CABankFeed, InnerJoin<CABankFeedDetail, On<CABankFeedDetail.bankFeedID, Equal<CABankFeed.bankFeedID>>>, Where<CABankFeedDetail.cashAccountID, Equal<Current<CashAccount.cashAccountID>>, And<Where<CABankFeed.status, Equal<CABankFeedStatus.active>>>>> BankFeed;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankFeedIntegration>();

  protected virtual void _(
    Events.FieldSelecting<CABankFeed.statementImportSource> e)
  {
    if (!(e.Row is CABankFeed row))
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<CABankFeed.statementImportSource>>) e).ReturnValue = (object) $"{PXMessages.LocalizeNoPrefix("Bank Feed")} : {row.BankFeedID}";
  }

  protected virtual void _(Events.RowSelected<CABankFeed> e)
  {
    CABankFeed row = e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetVisible<CABankFeed.statementImportSource>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankFeed>>) e).Cache, (object) row, true);
  }

  protected virtual void _(Events.RowSelected<CashAccount> e)
  {
    CashAccount row = e.Row;
    if (row == null)
      return;
    bool flag = ((PXSelectBase<CABankFeed>) this.BankFeed).SelectSingle(Array.Empty<object>()) == null;
    PXUIFieldAttribute.SetVisible<CashAccount.statementImportTypeName>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CashAccount>>) e).Cache, (object) row, flag);
  }
}
