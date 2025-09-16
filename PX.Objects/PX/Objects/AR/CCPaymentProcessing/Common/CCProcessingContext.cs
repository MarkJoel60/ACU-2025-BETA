// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.CCProcessingContext
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

public class CCProcessingContext
{
  public CCProcessingCenter processingCenter;
  public int? aPMInstanceID;
  public string PaymentMethodID;
  public int? aCustomerID = new int?(0);
  public string aCustomerCD;
  public string PrefixForCustomerCD;
  public string aDocType;
  public string aRefNbr;
  public PXGraph callerGraph;
  public String2DateConverterFunc expirationDateConverter;
}
