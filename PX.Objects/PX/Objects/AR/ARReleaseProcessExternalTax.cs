// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARReleaseProcessExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Text;

#nullable disable
namespace PX.Objects.AR;

public class ARReleaseProcessExternalTax : PXGraphExtension<ARReleaseProcess>
{
  protected Func<PXGraph, string, ITaxProvider> TaxProviderFactory;
  protected Lazy<SOInvoiceEntry> LazySoInvoiceEntry = new Lazy<SOInvoiceEntry>((Func<SOInvoiceEntry>) (() => PXGraph.CreateInstance<SOInvoiceEntry>()));
  protected Lazy<ARInvoiceEntry> LazyArInvoiceEntry = new Lazy<ARInvoiceEntry>((Func<ARInvoiceEntry>) (() => PXGraph.CreateInstance<ARInvoiceEntry>()));

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  public ARReleaseProcessExternalTax()
  {
    this.TaxProviderFactory = ExternalTaxBase<PXGraph>.TaxProviderFactory;
  }

  public virtual bool IsExternalTax(string taxZoneID)
  {
    return ExternalTaxBase<PXGraph>.IsExternalTax((PXGraph) this.Base, taxZoneID);
  }

  [PXOverride]
  public virtual ARRegister OnBeforeRelease(ARRegister ardoc)
  {
    if (!(ardoc is ARInvoice invoice) && ARInvoiceType.DrCr(ardoc.DocType) != null)
      invoice = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) this.Base.ARInvoice_DocType_RefNbr).Select(new object[2]
      {
        (object) ardoc.DocType,
        (object) ardoc.RefNbr
      }));
    if (invoice == null || invoice.IsTaxValid.GetValueOrDefault() || !this.IsExternalTax(invoice.TaxZoneID))
      return ardoc;
    ARInvoiceEntry arInvoiceEntry = invoice.OrigModule == "SO" ? (ARInvoiceEntry) this.LazySoInvoiceEntry.Value : this.LazyArInvoiceEntry.Value;
    ((PXGraph) arInvoiceEntry).Clear();
    return (ARRegister) arInvoiceEntry.RecalculateExternalTax(invoice);
  }

  [PXOverride]
  public virtual ARInvoice CommitExternalTax(ARInvoice doc)
  {
    if (doc != null && doc.IsTaxValid.GetValueOrDefault())
    {
      bool? nonTaxable = doc.NonTaxable;
      bool flag = false;
      if (nonTaxable.GetValueOrDefault() == flag & nonTaxable.HasValue && this.IsExternalTax(doc.TaxZoneID) && !doc.InstallmentNbr.HasValue && !doc.IsTaxPosted.GetValueOrDefault() && TaxPluginMaint.IsActive((PXGraph) this.Base, doc.TaxZoneID))
      {
        ITaxProvider itaxProvider = ExternalTaxBase<PXGraph>.TaxProviderFactory((PXGraph) this.Base, doc.TaxZoneID);
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<ARInvoice>) instance.Document).Current = doc;
        CommitTaxRequest commitTaxRequest = ((PXGraph) instance).GetExtension<ARInvoiceEntryExternalTax>().BuildCommitTaxRequest(doc);
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
            doc.WarningMessage = Helper.AppendWithDot(PXMessages.LocalizeFormatNoPrefixNLA("Document was released successfully but the tax was not posted to the external tax provider with the following message: {0}", new object[1]
            {
              (object) stringBuilder.ToString()
            }), PXMessages.LocalizeNoPrefix("Use the Post Taxes (TX501500) form to post the taxes for the document to the external tax provider."));
        }
      }
    }
    return doc;
  }
}
