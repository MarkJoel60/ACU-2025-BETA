// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.TranProcessingResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

/// <summary>A supplementary class to return the result of the authorization center transaction to Acumatica ERP.</summary>
public class TranProcessingResult
{
  /// <summary>The internal transaction identifier, which must be the same as the <see cref="!:ProcessingInput.TranID" /> passed to the payment gateway.</summary>
  public int TranID;
  /// <summary>The transaction status.</summary>
  public CCTranStatus TranStatus = CCTranStatus.Error;
  /// <summary>A field that indicates (if set to <tt>true</tt>) that the transaction was authorized.</summary>
  public bool Success;
  /// <summary>The transaction number that was assigned by the authorization center.</summary>
  public string PCTranNumber;
  /// <summary>The raw authorization center response code.</summary>
  public string PCResponseCode;
  /// <summary>The raw response reason code.</summary>
  public string PCResponseReasonCode;
  /// <summary>The complete raw response from the authorization center.</summary>
  public string PCResponse;
  /// <summary>The additional CVV code from the authorization center (part of the complete response).</summary>
  public string PCCVVResponse;
  /// <summary>The authorization number.</summary>
  public string AuthorizationNbr;
  /// <summary>The response reason message. This text will be displayed in the credit card payment processing interface.</summary>
  public string PCResponseReasonText;
  /// <summary>The error message.</summary>
  public string ErrorText;
  /// <summary>The period (in days) after which the transaction automatically expires (for authorization transactions).</summary>
  public int? ExpireAfterDays;
  /// <summary>The result flag.</summary>
  public CCResultFlag ResultFlag;
  /// <summary>The CVV verification status.</summary>
  public CcvVerificationStatus CcvVerificatonStatus;
  /// <summary>The error source.</summary>
  public CCError.CCErrorSource ErrorSource;

  /// <summary>The card type used for the transactions.</summary>
  public CCCardType CardType { get; set; }

  /// <summary> Original card type value sent by a processing center</summary>
  public string ProcCenterCardTypeCode { get; set; }

  /// <summary> A field that indicates (if set to <tt>true</tt> that the support Level 3 Data.</summary>
  public bool Level3Support { get; set; }

  /// <summary> The Level 3 Data status./// </summary>
  public L3TranStatus L3Status { get; set; }

  /// <summary> The Level 3 Data error message.	/// </summary>
  public string L3Error { get; set; }

  /// <summary>POS terminal ID</summary>
  public string POSTerminalID { get; set; }

  /// <summary>The last digits of card.</summary>
  public string LastDigits { get; set; }

  /// <summary>Transaction type.</summary>
  public CCTranType? TranType { get; set; }

  /// <summary>Transaction time.</summary>
  public DateTime? TranDateTimeUTC { get; set; }
}
