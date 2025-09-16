// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;

#nullable disable
namespace PX.Objects.Extensions.PaymentProfile;

public class CustomerPaymentMethodDetail : PXMappedCacheExtension, ICCPaymentProfileDetail
{
  public virtual int? PMInstanceID { get; set; }

  public virtual string PaymentMethodID { get; set; }

  public virtual string DetailID { get; set; }

  public virtual string Value { get; set; }

  public virtual bool? IsIdentifier { get; set; }

  public virtual bool? IsCCProcessingID { get; set; }

  public abstract class pMInstanceID : IBqlField, IBqlOperand
  {
  }

  public abstract class paymentMethodID : IBqlField, IBqlOperand
  {
  }

  public abstract class detailID : IBqlField, IBqlOperand
  {
  }

  public abstract class value : IBqlField, IBqlOperand
  {
  }

  public abstract class isIdentifier : IBqlField, IBqlOperand
  {
  }

  public abstract class isCCProcessingID : IBqlField, IBqlOperand
  {
  }
}
