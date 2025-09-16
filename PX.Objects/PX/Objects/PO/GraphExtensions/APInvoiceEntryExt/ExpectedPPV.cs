// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APInvoiceEntryExt.ExpectedPPV
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APInvoiceEntryExt;

public class ExpectedPPV : 
  BaseUpdatePOAccrual<APInvoiceEntry.MultiCurrency, APInvoiceEntry, PX.Objects.AP.APInvoice>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.inventory>();

  protected virtual void _(PX.Data.Events.RowInserted<APTax> e)
  {
    this.ResetPPVValid(e.Row?.TaxID != null);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<APTax> e) => this.ResetPPVValid();

  protected virtual void _(PX.Data.Events.RowUpdated<APTax> e)
  {
    bool flag = ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<APTax>>) e).Cache.ObjectsEqual<APTax.curyTaxableAmt, APTax.curyRetainedTaxableAmt, APTax.curyTaxableDiscountAmt>((object) e.Row, (object) e.OldRow);
    this.ResetPPVValid(e.Row?.TaxID != null && !flag);
  }

  protected virtual void _(PX.Data.Events.RowInserted<APInvoiceDiscountDetail> e)
  {
    this.ResetPPVValid(e.Row?.DiscountSequenceID != null);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<APInvoiceDiscountDetail> e)
  {
    this.ResetPPVValid();
  }

  protected virtual void _(PX.Data.Events.RowUpdated<APInvoiceDiscountDetail> e)
  {
    bool flag = ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<APInvoiceDiscountDetail>>) e).Cache.ObjectsEqual<APInvoiceDiscountDetail.discountID, APInvoiceDiscountDetail.discountSequenceID, APInvoiceDiscountDetail.curyDiscountableAmt, APInvoiceDiscountDetail.discountPct>((object) e.Row, (object) e.OldRow);
    this.ResetPPVValid(e.Row?.DiscountSequenceID != null && !flag);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AP.APTran> e)
  {
    this.ResetPPVValid(!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AP.APTran>>) e).Cache.ObjectsEqual<PX.Objects.AP.APTran.curyTranAmt, PX.Objects.AP.APTran.curyTaxableAmt, PX.Objects.AP.APTran.curyRetainageAmt, PX.Objects.AP.APTran.groupDiscountRate, PX.Objects.AP.APTran.documentDiscountRate>((object) e.Row, (object) e.OldRow) && !this.SkipPPVCalculation(e.Row));
  }

  protected virtual void ResetPPVValid(bool reset = true)
  {
    if (!reset)
      return;
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
    if ((current != null ? (current.Released.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      return;
    ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.IsExpectedPPVValid = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AP.APInvoice> e)
  {
    bool? hold1 = e.Row.Hold;
    bool flag = false;
    if (!(hold1.GetValueOrDefault() == flag & hold1.HasValue))
      return;
    bool? hold2 = e.Row.Hold;
    bool? hold3 = (bool?) e.OldRow?.Hold;
    if (hold2.GetValueOrDefault() == hold3.GetValueOrDefault() & hold2.HasValue == hold3.HasValue)
      return;
    this.RecalculatePPV();
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AP.APTran> e)
  {
    if ((e.Operation & 3) != 2)
      return;
    PX.Objects.PO.POReceiptLine receiptUnderCorrection = this.GetReceiptUnderCorrection(e.Row);
    if (receiptUnderCorrection == null)
      return;
    if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.receiptNbr>((object) e.Row, (object) e.Row.ReceiptNbr, (Exception) new PXSetPropertyException<PX.Objects.AP.APTran.receiptNbr>("The linked purchase receipt ({0}) is under correction or canceled.", (PXErrorLevel) 5, new object[1]
    {
      (object) receiptUnderCorrection.ReceiptNbr
    })))
      throw new PXRowPersistingException(PXDataUtils.FieldName<PX.Objects.AP.APTran.receiptNbr>(), (object) e.Row.ReceiptNbr, "The linked purchase receipt ({0}) is under correction or canceled.", new object[1]
      {
        (object) receiptUnderCorrection.ReceiptNbr
      });
  }

  [PXOverride]
  public virtual bool PrePersist(Func<bool> baseMethod)
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current != null && !((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Released.GetValueOrDefault())
      this.RecalculatePPV();
    return baseMethod();
  }

  protected override ARReleaseProcess.Amount GetExpensePostingAmount(
    PXGraph graph,
    PX.Objects.AP.APTran tran,
    PX.Objects.AP.APInvoice invoice)
  {
    PXResult<APTax> pxResult = ((IQueryable<PXResult<APTax>>) ((PXSelectBase<APTax>) new PXSelect<APTax, Where<APTax.tranType, Equal<Required<PX.Objects.AP.APTran.tranType>>, And<APTax.refNbr, Equal<Required<PX.Objects.AP.APTran.refNbr>>, And<APTax.lineNbr, Equal<Required<PX.Objects.AP.APTran.lineNbr>>>>>>(graph)).Select(new object[3]
    {
      (object) tran.TranType,
      (object) tran.RefNbr,
      (object) tran.LineNbr
    })).FirstOrDefault<PXResult<APTax>>();
    APTax lineTax = pxResult != null ? PXResult<APTax>.op_Implicit(pxResult) : new APTax();
    PX.Objects.TX.Tax salesTax = PX.Objects.TX.Tax.PK.Find(graph, lineTax.TaxID) ?? new PX.Objects.TX.Tax();
    Func<Decimal, Decimal> func = (Func<Decimal, Decimal>) (amount => PXDBCurrencyAttribute.Round(graph.Caches[typeof (PX.Objects.AP.APTran)], (object) tran, amount, CMPrecision.TRANCURY));
    return APReleaseProcess.GetExpensePostingAmount(graph, tran, lineTax, salesTax, invoice, func, func);
  }

  protected virtual void RecalculatePPV()
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.IsExpectedPPVValid.GetValueOrDefault())
      return;
    foreach (PX.Objects.AP.APTran tran in this.GetLinesToRecalculatePPV())
    {
      if (tran.POAccrualType != null)
      {
        POAccrualStatus dirty = POAccrualStatus.PK.FindDirty((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, tran.POAccrualRefNoteID, tran.POAccrualLineNbr, tran.POAccrualType);
        if (dirty != null)
        {
          tran.ExpectedPPVAmount = new Decimal?(this.CalculatePPV(((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current, tran, dirty).PPVAmount);
          ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Update(tran);
        }
      }
    }
    ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.IsExpectedPPVValid = new bool?(true);
    GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current);
  }

  protected virtual IEnumerable<PX.Objects.AP.APTran> GetLinesToRecalculatePPV()
  {
    return GraphHelper.RowCast<PX.Objects.AP.APTran>((IEnumerable) ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Select(Array.Empty<object>())).Where<PX.Objects.AP.APTran>((Func<PX.Objects.AP.APTran, bool>) (tran => !this.SkipPPVCalculation(tran)));
  }

  protected virtual bool SkipPPVCalculation(PX.Objects.AP.APTran tran)
  {
    return (tran == null || tran.PONbr == null) && (tran == null || tran.ReceiptNbr == null) || EnumerableExtensions.IsIn<string>(tran.LineType, "SV", "FT", "DN", "DS");
  }
}
