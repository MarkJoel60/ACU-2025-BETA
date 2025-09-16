// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARInvoiceEntryExt.Correction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.Common;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ARInvoiceEntryExt;

public class Correction : ARAdjustCorrectionExtension<ARInvoiceEntry, PX.Objects.AR.ARRegister.isCancellation>
{
  public PXAction<PX.Objects.AR.ARInvoice> ViewCorrectionDocument;

  [PXUIField]
  [PXLookupButton]
  protected virtual IEnumerable viewCorrectionDocument(PXAdapter adapter)
  {
    RedirectionToOrigDoc.TryRedirect(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.CorrectionDocType, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.CorrectionRefNbr, "SO");
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Correction Document", Enabled = false)]
  [PXUIVisible(typeof (Where<PX.Objects.AR.ARRegister.isUnderCorrection, Equal<True>, And<PX.Objects.AR.ARInvoice.correctionRefNbr, IsNotNull>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.correctionRefNbr> e)
  {
  }

  protected override PX.Objects.AR.ARInvoice GetParentInvoice(PXCache cache, PX.Objects.AR.ARAdjust aRAdjust)
  {
    return PX.Objects.AR.ARInvoice.PK.Find((PXGraph) this.Base, aRAdjust.AdjdDocType, aRAdjust.AdjdRefNbr);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.AR.ARInvoice> e)
  {
    PX.Objects.AR.ARInvoice row = e.Row;
    if ((row != null ? (!row.IsUnderCorrection.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    PXView view = ((PXSelectBase) new PXViewOf<ARRegisterAlias>.BasedOn<SelectFromBase<ARRegisterAlias, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterAlias.origDocType, Equal<BqlField<PX.Objects.AR.ARInvoice.docType, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterAlias.origRefNbr, Equal<BqlField<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.FromCurrent>>>>>.And<Where2<BqlOperand<ARRegisterAlias.isCorrection, IBqlBool>.IsEqual<True>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterAlias.isCancellation, Equal<True>>>>>.And<BqlOperand<ARRegisterAlias.released, IBqlBool>.IsNotEqual<True>>>>>>>>.ReadOnly((PXGraph) this.Base)).View;
    using (new PXFieldScope(view, new Type[3]
    {
      typeof (ARRegisterAlias.docType),
      typeof (ARRegisterAlias.refNbr),
      typeof (ARRegisterAlias.isCancellation)
    }))
    {
      ARRegisterAlias arRegisterAlias = (ARRegisterAlias) view.SelectSingleBound((object[]) new PX.Objects.AR.ARInvoice[1]
      {
        e.Row
      }, Array.Empty<object>());
      e.Row.CorrectionDocType = arRegisterAlias?.DocType;
      e.Row.CorrectionRefNbr = arRegisterAlias?.RefNbr;
      e.Row.IsUnderCancellation = new bool?(arRegisterAlias != null && arRegisterAlias.IsCancellation.GetValueOrDefault());
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e)
  {
    if (e.Row == null)
      return;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice>>) e).Cache;
    PX.Objects.AR.ARInvoice row1 = e.Row;
    string origRefNbr = e.Row.OrigRefNbr;
    PXSetPropertyException<PX.Objects.AR.ARInvoice.origRefNbr> propertyException1;
    if (!this.IsCorrectionFromOriginal(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice>>) e).Cache, e.Row))
      propertyException1 = (PXSetPropertyException<PX.Objects.AR.ARInvoice.origRefNbr>) null;
    else
      propertyException1 = new PXSetPropertyException<PX.Objects.AR.ARInvoice.origRefNbr>("The current document is the correction invoice for the {0} invoice.", (PXErrorLevel) 2, new object[1]
      {
        (object) e.Row.OrigRefNbr
      });
    cache1.RaiseExceptionHandling<PX.Objects.AR.ARInvoice.origRefNbr>((object) row1, (object) origRefNbr, (Exception) propertyException1);
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice>>) e).Cache;
    PX.Objects.AR.ARInvoice row2 = e.Row;
    string correctionRefNbr = e.Row.CorrectionRefNbr;
    PXSetPropertyException<PX.Objects.AR.ARInvoice.correctionRefNbr> propertyException2;
    if (!this.IsOriginalOfCorrection(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice>>) e).Cache, e.Row))
      propertyException2 = (PXSetPropertyException<PX.Objects.AR.ARInvoice.correctionRefNbr>) null;
    else
      propertyException2 = new PXSetPropertyException<PX.Objects.AR.ARInvoice.correctionRefNbr>("The current document has been corrected with the {0} correction invoice.", (PXErrorLevel) 2, new object[1]
      {
        (object) e.Row.CorrectionRefNbr
      });
    cache2.RaiseExceptionHandling<PX.Objects.AR.ARInvoice.correctionRefNbr>((object) row2, (object) correctionRefNbr, (Exception) propertyException2);
  }

  protected virtual bool IsCorrectionFromOriginal(PXCache cache, PX.Objects.AR.ARInvoice invoice)
  {
    return invoice != null && invoice.OrigRefNbr != null && invoice.IsCorrection.GetValueOrDefault();
  }

  protected virtual bool IsOriginalOfCorrection(PXCache cache, PX.Objects.AR.ARInvoice invoice)
  {
    return invoice != null && invoice.CorrectionRefNbr != null && !invoice.IsUnderCancellation.GetValueOrDefault();
  }

  [PXOverride]
  public virtual PX.Objects.AR.ARRegister OnBeforeRelease(
    PX.Objects.AR.ARRegister doc,
    Func<PX.Objects.AR.ARRegister, PX.Objects.AR.ARRegister> baseMethod)
  {
    if ((doc.IsCancellation.GetValueOrDefault() || doc.IsCorrection.GetValueOrDefault()) && doc.OrigRefNbr == null)
      throw new PXException("The correction document does not have the link to the original document and cannot be released. Contact your Acumatica support provider for assistance.");
    return baseMethod(doc);
  }

  [PXOverride]
  public virtual ARTran CreateReversalARTran(
    ARTran srcTran,
    ReverseInvoiceArgs reverseArgs,
    Func<ARTran, ReverseInvoiceArgs, ARTran> baseMethod)
  {
    ARTran reversalArTran = baseMethod(srcTran, reverseArgs);
    if (reversalArTran == null)
      return (ARTran) null;
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current?.OrigModule != "SO")
    {
      reversalArTran.SOOrderType = (string) null;
      reversalArTran.SOOrderNbr = (string) null;
      reversalArTran.SOOrderLineNbr = new int?();
      reversalArTran.SOOrderLineOperation = (string) null;
      reversalArTran.SOOrderSortOrder = new int?();
      reversalArTran.SOOrderLineSign = new short?();
      reversalArTran.SOShipmentType = (string) null;
      reversalArTran.SOShipmentNbr = (string) null;
      reversalArTran.SOShipmentLineGroupNbr = new int?();
      reversalArTran.SOShipmentLineNbr = new int?();
    }
    return reversalArTran;
  }
}
