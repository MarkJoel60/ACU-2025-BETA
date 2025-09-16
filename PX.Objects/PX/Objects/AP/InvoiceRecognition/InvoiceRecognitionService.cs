// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.InvoiceRecognitionService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Microsoft.Extensions.Options;
using PX.CloudServices.DocumentRecognition;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

internal class InvoiceRecognitionService : IInvoiceRecognitionService, IInvoiceRecognitionFeedback
{
  private readonly string ModelName;
  private readonly IDocumentRecognitionClient _documentRecognitionClient;

  public InvoiceRecognitionService(
    IDocumentRecognitionClient documentRecognitionClient,
    IOptions<InvoiceRecognitionModelOptions> modelOptions)
  {
    this._documentRecognitionClient = documentRecognitionClient;
    this.ModelName = modelOptions.Value.Name;
  }

  string IInvoiceRecognitionService.ModelName => this.ModelName;

  bool IInvoiceRecognitionService.IsConfigured() => this._documentRecognitionClient.IsConfigured();

  async Task<DocumentRecognitionResponse> IInvoiceRecognitionService.SendFile(
    Guid fileId,
    byte[] file,
    string contentType,
    CancellationToken cancellationToken)
  {
    return new DocumentRecognitionResponse(InvoiceRecognitionService.ToState(await this._documentRecognitionClient.SendFile(this.ModelName, fileId, file, contentType, cancellationToken)));
  }

  async Task<DocumentRecognitionResponse> IInvoiceRecognitionService.GetResult(
    string state,
    CancellationToken cancellationToken)
  {
    (DocumentRecognitionResult result, Uri uri) = await this._documentRecognitionClient.GetResult(InvoiceRecognitionService.FromState(state), cancellationToken);
    if (result != null)
      return new DocumentRecognitionResponse(result);
    return uri != (Uri) null ? new DocumentRecognitionResponse(InvoiceRecognitionService.ToState(uri)) : throw new InvalidOperationException("The result from GetResult is completely empty");
  }

  private static Uri FromState(string state) => new Uri(state);

  private static string ToState(Uri uri) => uri.ToString();

  Task IInvoiceRecognitionFeedback.Send(
    Uri address,
    HttpContent content,
    CancellationToken cancellationToken)
  {
    return this._documentRecognitionClient.Feedback(address, content, cancellationToken);
  }
}
