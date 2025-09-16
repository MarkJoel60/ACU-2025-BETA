// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.APInvoiceEntrySingleProject
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.PM;

public class APInvoiceEntrySingleProject : 
  SingleProjectExtension<APInvoiceEntry, APInvoice, APInvoice.projectID, APInvoice.hasMultipleProjects, APRegisterSingleProject, APTran, APTran.projectID>
{
  public static bool IsActive()
  {
    return SingleProjectExtension<APInvoiceEntry, APInvoice, APInvoice.projectID, APInvoice.hasMultipleProjects, APRegisterSingleProject, APTran, APTran.projectID>.IsExtensionEnabled();
  }

  protected override PXSelectBase<APInvoice> Document
  {
    get => (PXSelectBase<APInvoice>) this.Base.Document;
  }

  protected override PXSelectBase<APTran> Details => (PXSelectBase<APTran>) this.Base.Transactions;

  protected override bool IsDetailLineIgnored(APTran? detail) => detail?.LineType == "DS";

  [PXOverride]
  public virtual IEnumerable Release(PXAdapter adapter, Func<PXAdapter, IEnumerable> baseMethod)
  {
    APInvoice current = this.Document.Current;
    if (current != null && current.PaymentsByLinesAllowed.GetValueOrDefault())
    {
      Decimal? curyDiscTot = current.CuryDiscTot;
      Decimal num = 0M;
      if (!(curyDiscTot.GetValueOrDefault() == num & curyDiscTot.HasValue))
        throw new PXException("The Pay by Line check box cannot be selected because the document discount is applicable to the current document.");
    }
    return baseMethod(adapter);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APInvoice, APInvoice.paymentsByLinesAllowed> e)
  {
    APInvoice row = e.Row;
    if (!(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APInvoice, APInvoice.paymentsByLinesAllowed>, APInvoice, object>) e).NewValue as bool?).GetValueOrDefault() || row == null)
      return;
    Decimal? curyDiscTot = row.CuryDiscTot;
    Decimal num = 0M;
    if (curyDiscTot.GetValueOrDefault() == num & curyDiscTot.HasValue)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APInvoice, APInvoice.paymentsByLinesAllowed>>) e).Cancel = true;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APInvoice, APInvoice.paymentsByLinesAllowed>, APInvoice, object>) e).NewValue = (object) false;
    ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<APInvoice.paymentsByLinesAllowed>((object) row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APInvoice, APInvoice.paymentsByLinesAllowed>, APInvoice, object>) e).NewValue, (Exception) new PXSetPropertyException<APInvoice.paymentsByLinesAllowed>("The Pay by Line check box cannot be selected because the document discount is applicable to the current document."));
  }

  protected virtual void _(PX.Data.Events.RowUpdated<APInvoice> e)
  {
    APInvoice oldRow = e.OldRow;
    APInvoice row = e.Row;
    if (oldRow == null || row == null)
      return;
    Decimal? curyDiscTot1 = oldRow.CuryDiscTot;
    Decimal? curyDiscTot2 = row.CuryDiscTot;
    if (curyDiscTot1.GetValueOrDefault() == curyDiscTot2.GetValueOrDefault() & curyDiscTot1.HasValue == curyDiscTot2.HasValue || !row.PaymentsByLinesAllowed.GetValueOrDefault())
      return;
    Decimal? curyDiscTot3 = row.CuryDiscTot;
    Decimal num = 0M;
    if (curyDiscTot3.GetValueOrDefault() == num & curyDiscTot3.HasValue || !this.IsFromUI(true))
      return;
    ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<APInvoice.paymentsByLinesAllowed>((object) row, (object) row.PaymentsByLinesAllowed, (Exception) new PXSetPropertyException<APInvoice.paymentsByLinesAllowed>("The Pay by Line check box cannot be selected because the document discount is applicable to the current document."));
  }
}
