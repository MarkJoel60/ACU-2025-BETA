// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.NewPaymentMethodDetailHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CA;

public static class NewPaymentMethodDetailHelper
{
  public static PaymentMethodDetail ToPaymentMethodDetail(
    this NewPaymentMethodDetail newPaymentMethodDetail,
    PaymentMethod paymentMethod)
  {
    PaymentMethodDetail paymentMethodDetail = new PaymentMethodDetail();
    paymentMethodDetail.DetailID = newPaymentMethodDetail.DetailID;
    paymentMethodDetail.Descr = newPaymentMethodDetail.Description;
    paymentMethodDetail.ControlType = newPaymentMethodDetail.ControlType;
    paymentMethodDetail.DefaultValue = newPaymentMethodDetail.DefaultValue;
    paymentMethodDetail.EntryMask = newPaymentMethodDetail.EntryMask;
    paymentMethodDetail.IsRequired = newPaymentMethodDetail.IsRequired;
    paymentMethodDetail.UseFor = newPaymentMethodDetail.UseFor;
    int? detailIdInt = newPaymentMethodDetail.DetailIDInt;
    paymentMethodDetail.OrderIndex = detailIdInt.HasValue ? new short?((short) detailIdInt.GetValueOrDefault()) : new short?();
    paymentMethodDetail.PaymentMethodID = paymentMethod.PaymentMethodID;
    paymentMethodDetail.ValidRegexp = newPaymentMethodDetail.ValidRegexp;
    return paymentMethodDetail;
  }
}
