// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.InvoiceBaseGraphExtension`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract;

public abstract class InvoiceBaseGraphExtension<TGraph, TInvoice, TInvoiceMapping> : 
  DocumentWithLinesGraphExtension<TGraph, TInvoice, TInvoiceMapping>
  where TGraph : PXGraph
  where TInvoice : InvoiceBase, new()
  where TInvoiceMapping : IBqlMapping
{
  public PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.Contragent> Contragent;
  public PXSelectExtension<InvoiceTran> InvoiceTrans;
  public PXSelectExtension<GenericTaxTran> TaxTrans;
  public PXSelectExtension<LineTax> LineTaxes;

  protected virtual ContragentMapping GetContragentMapping()
  {
    return new ContragentMapping(typeof (InvoiceBaseGraphExtension<TGraph, TInvoice, TInvoiceMapping>.Stub));
  }

  public abstract void SuppressApproval();

  public virtual PXSelectBase<PX.Objects.CR.Location> Location { get; }

  public virtual PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo> CurrencyInfo { get; }

  protected virtual InvoiceTranMapping GetInvoiceTranMapping()
  {
    return new InvoiceTranMapping(typeof (InvoiceBaseGraphExtension<TGraph, TInvoice, TInvoiceMapping>.Stub));
  }

  protected virtual GenericTaxTranMapping GetGenericTaxTranMapping()
  {
    return new GenericTaxTranMapping(typeof (InvoiceBaseGraphExtension<TGraph, TInvoice, TInvoiceMapping>.Stub));
  }

  protected virtual LineTaxMapping GetLineTaxMapping()
  {
    return new LineTaxMapping(typeof (InvoiceBaseGraphExtension<TGraph, TInvoice, TInvoiceMapping>.Stub));
  }

  protected override bool ShouldUpdateDetailsOnDocumentUpdated(Events.RowUpdated<TInvoice> e)
  {
    return base.ShouldUpdateDetailsOnDocumentUpdated(e) || !((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TInvoice>>) e).Cache.ObjectsEqual<Document.headerDocDate, InvoiceBase.curyID>((object) e.Row, (object) e.OldRow);
  }

  protected override void ProcessLineOnDocumentUpdated(
    Events.RowUpdated<TInvoice> e,
    DocumentLine line)
  {
    base.ProcessLineOnDocumentUpdated(e, line);
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TInvoice>>) e).Cache.ObjectsEqual<Document.headerDocDate, InvoiceBase.curyID>((object) e.Row, (object) e.OldRow))
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) this.Lines).Cache, (object) line);
  }

  [PXHidden]
  protected class Stub : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }
}
