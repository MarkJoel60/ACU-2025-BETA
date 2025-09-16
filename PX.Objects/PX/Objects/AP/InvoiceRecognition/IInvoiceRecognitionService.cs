// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.IInvoiceRecognitionService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

public interface IInvoiceRecognitionService
{
  Task<DocumentRecognitionResponse> SendFile(
    Guid fileId,
    byte[] file,
    string contentType,
    CancellationToken cancellationToken);

  Task<DocumentRecognitionResponse> GetResult(string state, CancellationToken cancellationToken);

  bool IsConfigured();

  string ModelName { get; }
}
