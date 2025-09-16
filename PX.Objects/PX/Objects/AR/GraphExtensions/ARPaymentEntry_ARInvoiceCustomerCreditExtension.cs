// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.GraphExtensions.ARPaymentEntry_ARInvoiceCustomerCreditExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.AR.GraphExtensions;

public class ARPaymentEntry_ARInvoiceCustomerCreditExtension : 
  ARInvoiceCustomerCreditExtension<ARPaymentEntry>
{
  protected override void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e)
  {
  }

  protected override void _(PX.Data.Events.RowPersisting<PX.Objects.AR.ARInvoice> e)
  {
  }

  protected override Decimal? GetDocumentBalance(PXCache cache, PX.Objects.AR.ARInvoice row)
  {
    if (this.GetHoldValue(cache, row).GetValueOrDefault() || this.IsFullAmountApproved(row))
      return new Decimal?(0M);
    Decimal? signBalance = row.SignBalance;
    Decimal? origDocAmt = row.OrigDocAmt;
    return !(signBalance.HasValue & origDocAmt.HasValue) ? new Decimal?() : new Decimal?(signBalance.GetValueOrDefault() * origDocAmt.GetValueOrDefault());
  }

  protected override ARSetup GetARSetup() => ((PXSelectBase<ARSetup>) this.Base.arsetup).Current;

  protected override SOSetup GetSOSetup()
  {
    return PXResultset<SOSetup>.op_Implicit(PXSetup<SOSetup>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }
}
