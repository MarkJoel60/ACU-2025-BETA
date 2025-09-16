// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.CCTranStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

/// <summary>Contains the transaction statuses returned by the processing center.</summary>
public enum CCTranStatus
{
  /// <summary>The transaction was approved.</summary>
  Approved,
  /// <summary>The transaction was declined.</summary>
  Declined,
  /// <summary>An error occurred when the transaction is processed.</summary>
  Error,
  /// <summary>The transaction is under review.</summary>
  HeldForReview,
  /// <summary>The transaction was expired.</summary>
  Expired,
  /// <summary>
  /// There was no answer or the answer can't be interpreted.
  /// </summary>
  Unknown,
}
