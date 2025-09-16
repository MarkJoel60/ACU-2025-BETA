// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.AR.GraphExtensions.ARPaymentEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.AR.GraphExtensions;

public class ARPaymentEntryExt : PXGraphExtension<ARPaymentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  protected virtual void ARAdjust_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ARAdjust row = (ARAdjust) e.Row;
    PMProforma pmProforma = PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelect<PMProforma, Where<PMProforma.aRInvoiceDocType, Equal<Required<ARAdjust.adjdDocType>>, And<PMProforma.aRInvoiceRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) row.AdjdDocType,
      (object) row.AdjdRefNbr
    }));
    if (pmProforma == null || !pmProforma.Corrected.GetValueOrDefault() || !(pmProforma.Status != "C") || !(((PXSelectBase<ARPayment>) this.Base.Document).Current.DocType == "CRM"))
      return;
    sender.RaiseExceptionHandling<ARAdjust.adjdRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("The system cannot reverse the invoice {0} or apply it to a credit memo because the related pro forma invoice {1} is under correction. To be able to reverse the invoice {0} or apply it to a credit memo, release the pro forma invoice {1} on the Pro Forma Invoices (PM307000) form first.", new object[2]
    {
      (object) row.AdjdRefNbr,
      (object) pmProforma.RefNbr
    }));
  }
}
