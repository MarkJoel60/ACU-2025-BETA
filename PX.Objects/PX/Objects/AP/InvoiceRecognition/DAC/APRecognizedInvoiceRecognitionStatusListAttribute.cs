// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DAC.APRecognizedInvoiceRecognitionStatusListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DAC;
using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.DAC;

[PXInternalUseOnly]
public class APRecognizedInvoiceRecognitionStatusListAttribute : RecognizedRecordStatusListAttribute
{
  public const string PendingFile = "F";
  private const string PendingFileLabel = "New";

  public APRecognizedInvoiceRecognitionStatusListAttribute()
    : base((IEnumerable<string>) new string[1]{ "F" }, (IEnumerable<string>) new string[1]
    {
      "New"
    })
  {
  }
}
