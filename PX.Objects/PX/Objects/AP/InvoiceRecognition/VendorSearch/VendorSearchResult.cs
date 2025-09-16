// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.VendorSearch.VendorSearchResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AP.InvoiceRecognition.Feedback;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.VendorSearch;

internal readonly struct VendorSearchResult(
  int? vendorId,
  int? termIndex,
  VendorSearchFeedback feedback)
{
  public int? VendorId { get; } = vendorId;

  public int? TermIndex { get; } = termIndex;

  public VendorSearchFeedback Feedback { get; } = feedback;
}
