// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.APInvoiceEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition;

internal class APInvoiceEmailProcessor : BasicEmailProcessor
{
  private readonly 
  #nullable disable
  IInvoiceRecognitionService _invoiceRecognitionService;

  public APInvoiceEmailProcessor(
    IInvoiceRecognitionService invoiceRecognitionService)
  {
    this._invoiceRecognitionService = invoiceRecognitionService;
  }

  protected override bool Process(BasicEmailProcessor.Package package)
  {
    if ((!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.apDocumentRecognition>() ? 0 : (this._invoiceRecognitionService.IsConfigured() ? 1 : 0)) == 0)
      return false;
    EMailAccount account = package.Account;
    if (account == null)
      return false;
    PX.Objects.AP.InvoiceRecognition.DAC.EMailAccountExt extension = package.Graph.Caches[typeof (EMailAccount)].GetExtension<PX.Objects.AP.InvoiceRecognition.DAC.EMailAccountExt>((object) account);
    bool? nullable;
    int num1;
    if (account.IncomingProcessing.GetValueOrDefault())
    {
      nullable = extension.SubmitToIncomingAPDocuments;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    if (num1 == 0)
      return false;
    CRSMEmail message = package.Message;
    CRSMEmail crsmEmail = message;
    int num2;
    if (crsmEmail == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = crsmEmail.Incoming;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num2 == 0)
      return false;
    PXGraph graph = package.Graph;
    if (graph == null)
      return false;
    PX.SM.UploadFile[] filesToRecognize = APInvoiceRecognitionEntry.GetFilesToRecognize(graph.Caches[typeof (CRSMEmail)], (object) message);
    if (filesToRecognize == null || filesToRecognize.Length == 0)
      return false;
    IEnumerable<RecognizedRecordFileInfo> batch = ((IEnumerable<PX.SM.UploadFile>) filesToRecognize).Select<PX.SM.UploadFile, RecognizedRecordFileInfo>((Func<PX.SM.UploadFile, RecognizedRecordFileInfo>) (file => new RecognizedRecordFileInfo(file.Name, file.Data, file.FileID.Value)));
    graph.LongOperationManager.StartAsyncOperation((Func<CancellationToken, Task>) (cancellationToken => APInvoiceRecognitionEntry.RecognizeRecordsBatch(batch, message.Subject, message.MailFrom, message.MessageId, message.OwnerID, externalCancellationToken: cancellationToken)));
    return true;
  }
}
