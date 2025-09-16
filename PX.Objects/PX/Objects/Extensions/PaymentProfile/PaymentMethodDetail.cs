// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;

#nullable disable
namespace PX.Objects.Extensions.PaymentProfile;

public class PaymentMethodDetail : PXMappedCacheExtension, ICCPaymentMethodDetail
{
  public virtual string PaymentMethodID { get; set; }

  public virtual string UseFor { get; set; }

  public virtual string DetailID { get; set; }

  public virtual string Descr { get; set; }

  public virtual bool? IsEncrypted { get; set; }

  public virtual bool? IsRequired { get; set; }

  public virtual bool? IsIdentifier { get; set; }

  public virtual bool? IsExpirationDate { get; set; }

  public virtual bool? IsOwnerName { get; set; }

  public virtual bool? IsCCProcessingID { get; set; }

  public virtual bool? IsCVV { get; set; }

  public abstract class paymentMethodID : IBqlField, IBqlOperand
  {
  }

  public abstract class useFor : IBqlField, IBqlOperand
  {
  }

  public abstract class detailID : IBqlField, IBqlOperand
  {
  }

  public abstract class descr : IBqlField, IBqlOperand
  {
  }

  public abstract class isEncrypted : IBqlField, IBqlOperand
  {
  }

  public abstract class isRequired : IBqlField, IBqlOperand
  {
  }

  public abstract class isIdentifier : IBqlField, IBqlOperand
  {
  }

  public abstract class isExpirationDate : IBqlField, IBqlOperand
  {
  }

  public abstract class isOwnerName : IBqlField, IBqlOperand
  {
  }

  public abstract class isCCProcessingID : IBqlField, IBqlOperand
  {
  }

  public abstract class isCVV : IBqlField, IBqlOperand
  {
  }
}
