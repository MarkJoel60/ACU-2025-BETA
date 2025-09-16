// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.CCTranType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

/// <summary>Defines the credit card transaction types.</summary>
public enum CCTranType
{
  /// <summary>Checks if the requested amount might be taken from the credit card, locks it on the credit card account, and takes the authorized amount from the card simultaneously.</summary>
  AuthorizeAndCapture,
  /// <summary>Checks if the requested amount might be taken from the credit card and locks it on the credit card account. </summary>
  AuthorizeOnly,
  /// <summary>Captures the previously authorized transaction.</summary>
  PriorAuthorizedCapture,
  /// <summary>Captures the manually authorized transaction.</summary>
  CaptureOnly,
  /// <summary>Returns the money back to the card.</summary>
  Credit,
  /// <summary>Reverses the authorized or captured transaction.</summary>
  Void,
  /// <summary>Performs a void first and then performs a credit if the void failed.</summary>
  VoidOrCredit,
  /// <summary>Imports the unknown transaction.</summary>
  Unknown,
  /// <summary>Increases amount of the previously authorized transaction. </summary>
  IncreaseAuthorizedAmount,
  /// <summary>Rejects transaction that was previously accepted.</summary>
  Reject,
}
