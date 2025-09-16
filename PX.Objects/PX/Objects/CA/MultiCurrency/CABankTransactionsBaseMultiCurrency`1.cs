// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.MultiCurrency.CABankTransactionsBaseMultiCurrency`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;

#nullable disable
namespace PX.Objects.CA.MultiCurrency;

public abstract class CABankTransactionsBaseMultiCurrency<TGraph> : 
  CAMultiCurrencyGraph<TGraph, CABankTran>
  where TGraph : PXGraph
{
  protected override string Module => "CA";

  protected override MultiCurrencyGraph<TGraph, CABankTran>.CurySourceMapping GetCurySourceMapping()
  {
    return new MultiCurrencyGraph<TGraph, CABankTran>.CurySourceMapping(typeof (CashAccount))
    {
      CuryID = typeof (CashAccount.curyID),
      CuryRateTypeID = typeof (CashAccount.curyRateTypeID)
    };
  }

  protected override MultiCurrencyGraph<TGraph, CABankTran>.DocumentMapping GetDocumentMapping()
  {
    return new MultiCurrencyGraph<TGraph, CABankTran>.DocumentMapping(typeof (CABankTran))
    {
      CuryID = typeof (CABankTran.curyID),
      CuryInfoID = typeof (CABankTran.curyInfoID),
      BAccountID = typeof (CABankTran.cashAccountID),
      DocumentDate = typeof (CABankTran.matchingPaymentDate)
    };
  }

  protected override void DocumentRowInserting<CuryInfoID, CuryID>(PXCache sender, object row)
  {
    if (!CABankTransactionsBaseMultiCurrency<TGraph>.GetCABankTranFromDocument(row).CanCreateCurrencyInfo())
      return;
    base.DocumentRowInserting<CuryInfoID, CuryID>(sender, row);
  }

  protected override void DocumentRowUpdating<CuryInfoID, CuryID>(PXCache sender, object row)
  {
    if (!CABankTransactionsBaseMultiCurrency<TGraph>.GetCABankTranFromDocument(row).CanCreateCurrencyInfo())
      return;
    base.DocumentRowUpdating<CuryInfoID, CuryID>(sender, row);
  }

  protected void _(Events.RowPersisting<CABankTran> e)
  {
    CABankTran row1 = e.Row;
    int num;
    if ((row1 != null ? (row1.CreateDocument.GetValueOrDefault() ? 1 : 0) : 0) == 0)
    {
      CABankTran row2 = e.Row;
      num = row2 != null ? (row2.MultipleMatching.GetValueOrDefault() ? 1 : 0) : 0;
    }
    else
      num = 1;
    if (num != 0)
      return;
    PXDBDefaultAttribute.SetPersistingCheck<CABankTran.curyInfoID>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CABankTran>>) e).Cache, (object) e.Row, (PXPersistingCheck) 2);
  }

  protected void _(Events.RowInserting<CABankTranMatch> e)
  {
    this.InsertNewCurrencyInfoWithoutCuryID<CABankTranMatch.curyInfoID>(((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<CABankTranMatch>>) e).Cache, (object) e.Row);
  }

  protected override void _(
    Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) this.Base.Accessinfo.BaseCuryID;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID>>) e).Cancel = true;
  }

  protected virtual void InsertNewCurrencyInfoWithoutCuryID<CuryInfoID>(PXCache sender, object row) where CuryInfoID : class, IBqlField
  {
    long? curyInfoId = (long?) sender.GetValue<CuryInfoID>(row);
    PX.Objects.CM.Extensions.CurrencyInfo info = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Insert(new PX.Objects.CM.Extensions.CurrencyInfo());
    ((PXSelectBase) this.currencyinfo).Cache.IsDirty = false;
    if (info == null)
      return;
    sender.SetValue<CuryInfoID>(row, (object) info.CuryInfoID);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) null;
    if (curyInfoId.HasValue)
    {
      currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfobykey).Select(new object[1]
      {
        (object) curyInfoId
      }));
      if (currencyInfo != null)
        currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) ((PXSelectBase) this.currencyinfo).Cache.GetOriginal((object) currencyInfo);
    }
    if (currencyInfo == null)
    {
      this.defaultCurrencyRate(((PXSelectBase) this.currencyinfo).Cache, info, true, true);
    }
    else
    {
      curyInfoId = info.CuryInfoID;
      ((PXSelectBase) this.currencyinfo).Cache.RestoreCopy((object) info, (object) currencyInfo);
      info.CuryInfoID = curyInfoId;
      ((PXSelectBase) this.currencyinfo).Cache.Remove((object) currencyInfo);
    }
  }

  private static CABankTran GetCABankTranFromDocument(object row)
  {
    return (row is Document document ? document.Base : (object) null) as CABankTran;
  }
}
