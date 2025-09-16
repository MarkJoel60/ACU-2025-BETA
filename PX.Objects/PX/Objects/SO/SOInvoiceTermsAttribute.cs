// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoiceTermsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// Automatically tracks and Updates Cash discounts in accordance with Customer's Credit Terms.
/// </summary>
public class SOInvoiceTermsAttribute : TermsAttribute
{
  public SOInvoiceTermsAttribute()
    : base(typeof (PX.Objects.AR.ARInvoice.docDate), typeof (PX.Objects.AR.ARInvoice.dueDate), typeof (PX.Objects.AR.ARInvoice.discDate), (Type) null, (Type) null, typeof (PX.Objects.AR.ARInvoice.curyTaxTotal), typeof (PX.Objects.AR.ARInvoice.branchID))
  {
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.SubscribeCalcDisc(sender);
    // ISSUE: method pointer
    sender.Graph.FieldVerifying.AddHandler(typeof (PX.Objects.AR.ARInvoice), typeof (PX.Objects.AR.ARInvoice.curyOrigDiscAmt).Name, new PXFieldVerifying((object) this, __methodptr(VerifyDiscount<PX.Objects.AR.ARInvoice.curyOrigDocAmt>)));
    // ISSUE: method pointer
    sender.Graph.FieldVerifying.AddHandler(typeof (PX.Objects.AR.ARInvoice), typeof (PX.Objects.AR.ARInvoice.curyOrigDiscAmt).Name, new PXFieldVerifying((object) this, __methodptr(VerifyDiscount<PX.Objects.AR.ARInvoice.curyDocBal>)));
    this._CuryDiscBal = typeof (PX.Objects.AR.ARInvoice.curyOrigDiscAmt);
  }

  public override void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    base.FieldUpdated(sender, e);
    this.CalcDisc_CuryOrigDocAmt(sender, e);
    this.CalcDisc_CuryDocBal(sender, e);
  }

  protected override void UnsubscribeCalcDisc(PXCache sender)
  {
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.RemoveHandler(typeof (PX.Objects.AR.ARInvoice), typeof (PX.Objects.AR.ARInvoice.curyOrigDocAmt).Name, new PXFieldUpdated((object) this, __methodptr(CalcDisc_CuryOrigDocAmt)));
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.RemoveHandler(typeof (PX.Objects.AR.ARInvoice), typeof (PX.Objects.AR.ARInvoice.curyDocBal).Name, new PXFieldUpdated((object) this, __methodptr(CalcDisc_CuryDocBal)));
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.RemoveHandler(typeof (PX.Objects.AR.ARInvoice), typeof (PX.Objects.AR.ARInvoice.branchID).Name, new PXFieldUpdated((object) this, __methodptr(CalcDisc_CuryOrigDocAmt)));
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.RemoveHandler(typeof (PX.Objects.AR.ARInvoice), typeof (PX.Objects.AR.ARInvoice.branchID).Name, new PXFieldUpdated((object) this, __methodptr(CalcDisc_CuryDocBal)));
  }

  protected override void SubscribeCalcDisc(PXCache sender)
  {
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(typeof (PX.Objects.AR.ARInvoice), typeof (PX.Objects.AR.ARInvoice.curyOrigDocAmt).Name, new PXFieldUpdated((object) this, __methodptr(CalcDisc_CuryOrigDocAmt)));
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(typeof (PX.Objects.AR.ARInvoice), typeof (PX.Objects.AR.ARInvoice.curyDocBal).Name, new PXFieldUpdated((object) this, __methodptr(CalcDisc_CuryDocBal)));
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(typeof (PX.Objects.AR.ARInvoice), typeof (PX.Objects.AR.ARInvoice.branchID).Name, new PXFieldUpdated((object) this, __methodptr(CalcDisc_CuryOrigDocAmt)));
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(typeof (PX.Objects.AR.ARInvoice), typeof (PX.Objects.AR.ARInvoice.branchID).Name, new PXFieldUpdated((object) this, __methodptr(CalcDisc_CuryDocBal)));
  }

  public void CalcDisc_CuryOrigDocAmt(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (((PX.Objects.AR.ARRegister) e.Row).DocType != "CSL" && ((PX.Objects.AR.ARRegister) e.Row).DocType != "RCS")
      this._CuryDocBal = typeof (PX.Objects.AR.ARInvoice.curyOrigDocAmt);
    try
    {
      this.CalcDisc(sender, e);
    }
    finally
    {
      this._CuryDocBal = (Type) null;
    }
  }

  public void CalcDisc_CuryDocBal(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (((PX.Objects.AR.ARRegister) e.Row).DocType == "CSL" || ((PX.Objects.AR.ARRegister) e.Row).DocType == "RCS")
      this._CuryDocBal = typeof (PX.Objects.AR.ARInvoice.curyDocBal);
    try
    {
      this.CalcDisc(sender, e);
    }
    finally
    {
      this._CuryDocBal = (Type) null;
    }
  }

  public void VerifyDiscount<Field>(PXCache sender, PXFieldVerifyingEventArgs e) where Field : IBqlField
  {
    if (((PX.Objects.AR.ARRegister) e.Row).DocType == "CSL" && typeof (Field) == typeof (PX.Objects.AR.ARInvoice.curyDocBal) || ((PX.Objects.AR.ARRegister) e.Row).DocType != "CSL" && typeof (Field) == typeof (PX.Objects.AR.ARInvoice.curyOrigDocAmt))
      this._CuryDocBal = typeof (Field);
    try
    {
      this.VerifyDiscount(sender, e);
    }
    finally
    {
      this._CuryDocBal = (Type) null;
    }
  }
}
