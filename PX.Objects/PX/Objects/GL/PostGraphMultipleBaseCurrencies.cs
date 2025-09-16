// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.PostGraphMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.GL;

public sealed class PostGraphMultipleBaseCurrencies : PXGraphExtension<PostGraph>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXOverride]
  public void PostBatchProc(
    Batch b,
    bool createintercompany,
    PostGraphMultipleBaseCurrencies.PostBatchProcDelegate baseMethod)
  {
    this.CheckBatchAndLedgerBaseCurrency((PXCache) GraphHelper.Caches<Batch>((PXGraph) this.Base), b);
    baseMethod(b, createintercompany);
  }

  [PXOverride]
  public void ReleaseBatchProc(
    Batch b,
    bool unholdBatch = false,
    PostGraphMultipleBaseCurrencies.ReleaseBatchProcDelegate baseMethod = null)
  {
    this.CheckBatchAndLedgerBaseCurrency((PXCache) GraphHelper.Caches<Batch>((PXGraph) this.Base), b);
    baseMethod(b, unholdBatch);
  }

  protected void CheckBatchAndLedgerBaseCurrency(PXCache cache, Batch batch)
  {
    PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) batch.CuryInfoID
    }));
    Ledger ledger = Ledger.PK.Find(cache.Graph, batch.LedgerID);
    if (currencyInfo != null && ledger != null && !currencyInfo.BaseCuryID.Equals(ledger.BaseCuryID))
      throw new PXException("The transaction cannot be released because the currency of the {0} ledger differs from the transaction base currency.", new object[1]
      {
        (object) ledger.LedgerCD
      });
  }

  public delegate void PostBatchProcDelegate(Batch b, bool createintercompany);

  public delegate void ReleaseBatchProcDelegate(Batch b, bool unholdBatch = false);
}
