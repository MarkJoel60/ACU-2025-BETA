// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APReleaseProcessExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Text;

#nullable disable
namespace PX.Objects.AP;

public class APReleaseProcessExternalTax : PXGraphExtension<APReleaseProcess>
{
  protected Func<PXGraph, string, ITaxProvider> TaxProviderFactory;
  protected Lazy<APInvoiceEntry> LazyApInvoiceEntry = new Lazy<APInvoiceEntry>((Func<APInvoiceEntry>) (() => PXGraph.CreateInstance<APInvoiceEntry>()));

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.avalaraTax>();

  public APReleaseProcessExternalTax()
  {
    this.TaxProviderFactory = ExternalTaxBase<PXGraph>.TaxProviderFactory;
  }

  public bool IsExternalTax(string taxZoneID)
  {
    return ExternalTaxBase<PXGraph>.IsExternalTax((PXGraph) this.Base, taxZoneID);
  }

  [PXOverride]
  public virtual APRegister OnBeforeRelease(APRegister apdoc)
  {
    if (!(apdoc is APInvoice apInvoice) || apInvoice.IsTaxValid.GetValueOrDefault() || !this.IsExternalTax(apInvoice.TaxZoneID))
      return apdoc;
    APInvoiceEntry graph = this.LazyApInvoiceEntry.Value;
    graph.Clear();
    graph.Document.Current = (APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.Select((PXGraph) graph, (object) apInvoice.DocType, (object) apInvoice.RefNbr);
    return (APRegister) graph.CalculateExternalTax(graph.Document.Current);
  }

  [PXOverride]
  public virtual APInvoice CommitExternalTax(APInvoice doc)
  {
    if (doc != null && doc.IsTaxValid.GetValueOrDefault())
    {
      bool? nonTaxable = doc.NonTaxable;
      bool flag = false;
      if (nonTaxable.GetValueOrDefault() == flag & nonTaxable.HasValue && this.IsExternalTax(doc.TaxZoneID) && !doc.InstallmentNbr.HasValue && !doc.IsTaxPosted.GetValueOrDefault() && TaxPluginMaint.IsActive((PXGraph) this.Base, doc.TaxZoneID))
      {
        ITaxProvider itaxProvider = this.TaxProviderFactory((PXGraph) this.Base, doc.TaxZoneID);
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        instance.Document.Current = doc;
        CommitTaxRequest commitTaxRequest = instance.GetExtension<APInvoiceEntryExternalTax>().BuildCommitTaxRequest(doc);
        CommitTaxResult commitTaxResult = itaxProvider.CommitTax(commitTaxRequest);
        if (((ResultBase) commitTaxResult).IsSuccess)
        {
          doc.IsTaxPosted = new bool?(true);
        }
        else
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (string message in ((ResultBase) commitTaxResult).Messages)
            stringBuilder.AppendLine(message);
          if (stringBuilder.Length > 0)
            doc.WarningMessage = Helper.AppendWithDot(PXMessages.LocalizeFormatNoPrefixNLA("Document was released successfully but the tax was not posted to the external tax provider with the following message: {0}", (object) stringBuilder.ToString()), PXMessages.LocalizeNoPrefix("Use the Post Taxes (TX501500) form to post the taxes for the document to the external tax provider."));
        }
      }
    }
    return doc;
  }

  public virtual TaxDocumentType GetTaxDocumentType(APInvoice invoice)
  {
    switch (invoice.DrCr)
    {
      case "D":
        return (TaxDocumentType) 4;
      case "C":
        return (TaxDocumentType) 6;
      default:
        throw new PXException("The document type is not supported or implemented.");
    }
  }
}
