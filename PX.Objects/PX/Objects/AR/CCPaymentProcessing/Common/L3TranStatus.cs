// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.L3TranStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

/// <summary>Contains the Level 3 Data transaction statuses</summary>
public enum L3TranStatus
{
  /// <summary>
  /// The transaction is not subject for L3 and will not be available for further L3processing.
  /// </summary>
  NotApplicable,
  /// <summary>The transaction is ready for sending Level 3 Data.</summary>
  Pending,
  /// <summary>The Level 3 Data has been sent.</summary>
  Sent,
  /// <summary>Sending of Level 3 is failed.</summary>
  Failed,
  /// <summary>
  /// The Level 3 Data has been rejected by processing center.
  /// </summary>
  Rejected,
  /// <summary>
  /// Reseneded Level 3 Data has been rejected by processing center.
  /// </summary>
  ResendRejected,
  /// <summary>Resending of Level 3 Data is failed.</summary>
  ResendFailed,
}
