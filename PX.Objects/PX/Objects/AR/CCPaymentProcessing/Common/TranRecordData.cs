// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.TranRecordData
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

public class TranRecordData
{
  public string ExternalTranId { get; set; }

  public string ExternalTranApiId { get; set; }

  public string CommerceTranNumber { get; set; }

  public string RefExternalTranId { get; set; }

  public string ProcessingCenterId { get; set; }

  public int? RefInnerTranId { get; set; }

  public string AuthCode { get; set; }

  public string TranStatus { get; set; }

  public Decimal? Amount { get; set; }

  public string ResponseCode { get; set; }

  public string ResponseText { get; set; }

  public DateTime? ExpirationDate { get; set; }

  public DateTime? TransactionDate { get; set; }

  public string CvvVerificationCode { get; set; }

  public string ExtProfileId { get; set; }

  public bool CreateProfile { get; set; }

  public bool NeedSync { get; set; }

  public bool Imported { get; set; }

  public bool NewDoc { get; set; }

  public bool AllowFillVoidRef { get; set; }

  public bool KeepNewTranDeactivated { get; set; }

  /// <summary>The <see cref="P:PX.Objects.AR.ExternalTransaction.TransactionID" /> identifier after recording operation.</summary>
  public int? InnerTranId { get; set; }

  public string PayLinkExternalId { get; set; }

  public bool ValidateDoc { get; set; } = true;

  public Guid? TranUID { get; set; }

  public CCCardType CardType { get; set; }

  public string ProcCenterCardTypeCode { get; set; }

  public bool IsLocalValidation { get; set; }

  public Decimal? Tax { get; set; }

  public Decimal? Subtotal { get; set; }

  public bool Level3Support { get; set; }

  public string TerminalID { get; set; }

  public string LastDigits { get; set; }

  public string TranType { get; set; }
}
