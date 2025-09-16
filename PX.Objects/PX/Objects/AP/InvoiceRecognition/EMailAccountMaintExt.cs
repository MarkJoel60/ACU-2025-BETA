// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.EMailAccountMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.SM;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

[PXInternalUseOnly]
public class EMailAccountMaintExt : PXGraphExtension<EMailAccountMaint>
{
  [InjectDependency]
  internal IInvoiceRecognitionService InvoiceRecognitionClient { get; set; }

  protected void _(PX.Data.Events.RowSelected<EMailAccount> e, PXRowSelected baseEvent)
  {
    baseEvent(e.Cache, e.Args);
    if (!(e.Args.Row is EMailAccount row))
      return;
    PXUIFieldAttribute.SetVisible<EMailAccountExt.submitToIncomingAPDocuments>(e.Cache, (object) row, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.apDocumentRecognition>() && this.InvoiceRecognitionClient.IsConfigured());
    PXUIFieldAttribute.SetEnabled<EMailAccountExt.submitToIncomingAPDocuments>(e.Cache, (object) row, row.IncomingProcessing.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<EMailAccountExt.defaultBranchID>(e.Cache, (object) row, row.IncomingProcessing.GetValueOrDefault());
  }
}
