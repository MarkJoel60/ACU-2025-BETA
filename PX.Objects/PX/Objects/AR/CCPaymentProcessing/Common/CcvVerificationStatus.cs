// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.CcvVerificationStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

/// <summary>Defines the CVV verification statuses returned by the credit card authority.</summary>
public enum CcvVerificationStatus
{
  /// <summary>The CVV code is correct.</summary>
  Match,
  /// <summary>The CVV code is incorrect.</summary>
  NotMatch,
  /// <summary>The CVV code is not processed.</summary>
  NotProcessed,
  /// <summary>The CVV code was not provided but is required for the authorization.</summary>
  ShouldHaveBeenPresent,
  /// <summary>The card issue authority was unable to verify the code.</summary>
  IssuerUnableToProcessRequest,
  /// <summary>The CVV code has already been verified by the Acumatica ERP core.</summary>
  RelyOnPreviousVerification,
  /// <summary>Not applicable</summary>
  NotApplicable,
  /// <summary>Empty status is returned.</summary>
  Empty,
  /// <summary>Any other status is returned.</summary>
  Unknown,
}
