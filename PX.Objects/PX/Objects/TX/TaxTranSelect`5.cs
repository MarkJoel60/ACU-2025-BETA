// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxTranSelect`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.TX;

[Obsolete("This class is obsolete as mutiple installments are now supported with Pending taxes.")]
public class TaxTranSelect<InvoiceTable, TermsID, InvoiceTaxTran, TaxID, WhereSelect> : 
  PXSelectJoin<InvoiceTaxTran, LeftJoin<Tax, On<Tax.taxID, Equal<TaxID>>>, WhereSelect>
  where InvoiceTable : class, IBqlTable, new()
  where TermsID : IBqlField
  where InvoiceTaxTran : class, IBqlTable, new()
  where TaxID : IBqlField
  where WhereSelect : IBqlWhere, new()
{
  public TaxTranSelect(PXGraph graph)
    : base(graph)
  {
    this.AddHandlers(graph);
  }

  public TaxTranSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    this.AddHandlers(graph);
  }

  private void AddHandlers(PXGraph graph)
  {
    PXGraph.RowPersistingEvents rowPersisting = graph.RowPersisting;
    TaxTranSelect<InvoiceTable, TermsID, InvoiceTaxTran, TaxID, WhereSelect> taxTranSelect = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) taxTranSelect, __vmethodptr(taxTranSelect, RowPersisting));
    rowPersisting.AddHandler<InvoiceTable>(pxRowPersisting);
  }

  protected virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.vATReporting>() || e.Row == null)
      return;
    string TermsID = (string) sender.GetValue<TermsID>(e.Row);
    if (!(TermsAttribute.SelectTerms(sender.Graph, TermsID)?.InstallmentType == "M"))
      return;
    foreach (PXResult<InvoiceTaxTran, Tax> pxResult in ((PXSelectBase) this).View.SelectMulti(Array.Empty<object>()))
    {
      Tax tax = PXResult<InvoiceTaxTran, Tax>.op_Implicit(pxResult);
      if ((tax != null ? (tax.PendingTax.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        sender.RaiseExceptionHandling<TermsID>(e.Row, (object) TermsID, (Exception) new PXSetPropertyException("The document cannot be processed because VAT of the Pending type (recognized by payments) cannot be applied to documents with the multiple installment credit terms specified."));
        break;
      }
    }
  }
}
