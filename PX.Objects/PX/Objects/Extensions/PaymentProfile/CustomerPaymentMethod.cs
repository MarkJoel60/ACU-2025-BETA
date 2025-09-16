// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;

#nullable disable
namespace PX.Objects.Extensions.PaymentProfile;

public class CustomerPaymentMethod : PXMappedCacheExtension, ICCPaymentProfile
{
  public virtual int? BAccountID { get; set; }

  public virtual int? PMInstanceID { get; set; }

  public virtual string CCProcessingCenterID { get; set; }

  public virtual string CustomerCCPID { get; set; }

  public virtual string PaymentMethodID { get; set; }

  public virtual int? CashAccountID { get; set; }

  public virtual string Descr { get; set; }

  public virtual System.DateTime? ExpirationDate { get; set; }

  public string CardType { get; set; }

  public string ProcCenterCardTypeCode { get; set; }

  public abstract class bAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class pMInstanceID : IBqlField, IBqlOperand
  {
  }

  public abstract class cCProcessingCenterID : IBqlField, IBqlOperand
  {
  }

  public abstract class customerCCPID : IBqlField, IBqlOperand
  {
  }

  public abstract class paymentMethodID : IBqlField, IBqlOperand
  {
  }

  public abstract class cashAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class descr : IBqlField, IBqlOperand
  {
  }

  public abstract class expirationDate : IBqlField, IBqlOperand
  {
  }
}
