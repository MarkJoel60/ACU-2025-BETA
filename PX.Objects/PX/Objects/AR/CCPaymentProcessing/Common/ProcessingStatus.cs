// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.ProcessingStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

public enum ProcessingStatus
{
  Unknown,
  AuthorizeFail,
  AuthorizeIncreaseFail,
  CaptureFail,
  VoidFail,
  CreditFail,
  AuthorizeSuccess,
  AuthorizeExpired,
  CaptureSuccess,
  VoidSuccess,
  CreditSuccess,
  AuthorizeHeldForReview,
  CaptureHeldForReview,
  VoidHeldForReview,
  CreditHeldForReview,
  AuthorizeDecline,
  CaptureDecline,
  VoidDecline,
  CreditDecline,
  CaptureExpired,
  RejectSuccess,
}
