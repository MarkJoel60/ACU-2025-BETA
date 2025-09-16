// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DocumentRecognitionResponse
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DocumentRecognition;
using System;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

/// <remarks>
/// Can be in one of two states:
/// <list type="bullet">
/// <item>
/// <description>
/// <see cref="P:PX.Objects.AP.InvoiceRecognition.DocumentRecognitionResponse.State" /> is not <see langword="null" />: the recognition is in progress
/// </description>
/// </item>
/// <item>
/// <description>
/// <see cref="P:PX.Objects.AP.InvoiceRecognition.DocumentRecognitionResponse.Result" /> is not <see langword="null" />: the recognition has completed successfully
/// </description>
/// </item>
/// </list>
/// The other property is always <see langword="null" />.
/// </remarks>
public sealed class DocumentRecognitionResponse
{
  public DocumentRecognitionResponse(string state)
  {
    this.State = state ?? throw new ArgumentNullException(nameof (state));
  }

  public DocumentRecognitionResponse(DocumentRecognitionResult result)
  {
    this.Result = result ?? throw new ArgumentNullException(nameof (result));
  }

  public string State { get; }

  public DocumentRecognitionResult Result { get; }

  public void Deconstruct(out DocumentRecognitionResult result, out string state)
  {
    result = this.Result;
    state = this.State;
  }
}
