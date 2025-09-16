// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.SOInvoiceEntryExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using PX.Objects.SO;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.SOInvoiceEntryExt;

public class MultipleBaseCurrencyExt : 
  MultipleBaseCurrencyExtBase<SOInvoiceEntry, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran, PX.Objects.AR.ARInvoice.branchID, PX.Objects.AR.ARTran.branchID, PX.Objects.AR.ARTran.siteID>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.AR.ARInvoice.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARTran.siteID> e)
  {
  }

  protected override PXSelectBase<PX.Objects.AR.ARTran> GetTransactionView()
  {
    return (PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions;
  }
}
