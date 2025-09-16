// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.IInvoiceRecognitionFeedback
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

internal interface IInvoiceRecognitionFeedback
{
  Task Send(Uri address, HttpContent content, CancellationToken cancellationToken = default (CancellationToken));
}
