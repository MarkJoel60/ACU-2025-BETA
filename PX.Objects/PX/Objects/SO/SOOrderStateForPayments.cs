// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderStateForPayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.SO;

public class SOOrderStateForPayments
{
  public bool PaymentsAllowed;
  public bool CreatePaymentEnabled;
  public bool Inserted;
  public bool DocStatusAllowsPayment;
  public bool IsMixedOrder;
  public bool PaymentsAllowedByAmount;
  public bool RefundsAllowed;
  public bool RefundsAllowedByAmount;
  public bool CreateRefundEnabled;
  public string PaymentType;
  public bool ImportPaymentEnabled;
  public bool IsReqPrepaymentVisible;
  public bool ChildExists;
  public bool IsPrepaymentReqEnabled;
  public bool IsOverridePrepaymentEnabled;
  public bool IsIncreaseAuthorizedAmountEnabled;
}
