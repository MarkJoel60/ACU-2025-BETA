// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashAccountPaymentMethodDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// Cash account-specific values for the <see cref="T:PX.Objects.CA.PaymentMethodDetail">payment method settings</see> related to cash accounts.
/// The records of this type are edited on the Remittance Settings tab of the Cash Accounts (CA20200) form.
/// </summary>
[PXCacheName("Remittance Settings")]
[Serializable]
public class CashAccountPaymentMethodDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (CashAccount.cashAccountID))]
  [PXUIField(DisplayName = "Cash Account", Visible = false, Enabled = false)]
  [PXParent(typeof (Select<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Current<CashAccountPaymentMethodDetail.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<CashAccountPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>))]
  public virtual int? CashAccountID { get; set; }

  [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2023R2.")]
  [PXInt]
  public virtual int? AccountID
  {
    get => this.CashAccountID;
    set => this.CashAccountID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Payment Method", Visible = false)]
  [PXSelector(typeof (Search<PaymentMethod.paymentMethodID>))]
  public virtual 
  #nullable disable
  string PaymentMethodID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "ID", Visible = false, Enabled = false)]
  [PXSelector(typeof (Search<PaymentMethodDetail.detailID, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<CashAccountPaymentMethodDetail.paymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>))]
  public virtual string DetailID { get; set; }

  [PXDBStringWithMask(255 /*0xFF*/, typeof (Search<PaymentMethodDetail.entryMask, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<CashAccountPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Current<CashAccountPaymentMethodDetail.detailID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>), IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  [PXDefault]
  [DynamicRemittanceSettingValueValidation(typeof (Search<PaymentMethodDetail.validRegexp, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<CashAccountPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Current<CashAccountPaymentMethodDetail.detailID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>))]
  public virtual string DetailValue { get; set; }

  public class PK : 
    PrimaryKeyOf<CashAccountPaymentMethodDetail>.By<CashAccountPaymentMethodDetail.cashAccountID, CashAccountPaymentMethodDetail.paymentMethodID, CashAccountPaymentMethodDetail.detailID>
  {
    public static CashAccountPaymentMethodDetail Find(
      PXGraph graph,
      int? cashAccountID,
      string paymentMethodID,
      string detailID,
      PKFindOptions options = 0)
    {
      return (CashAccountPaymentMethodDetail) PrimaryKeyOf<CashAccountPaymentMethodDetail>.By<CashAccountPaymentMethodDetail.cashAccountID, CashAccountPaymentMethodDetail.paymentMethodID, CashAccountPaymentMethodDetail.detailID>.FindBy(graph, (object) cashAccountID, (object) paymentMethodID, (object) detailID, options);
    }
  }

  public static class FK
  {
    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CashAccountPaymentMethodDetail>.By<CashAccountPaymentMethodDetail.cashAccountID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<CashAccountPaymentMethodDetail>.By<CashAccountPaymentMethodDetail.paymentMethodID>
    {
    }

    public class PaymentMethodDetail : 
      PrimaryKeyOf<PaymentMethodDetail>.By<PaymentMethodDetail.paymentMethodID, PaymentMethodDetail.detailID>.ForeignKeyOf<CashAccountPaymentMethodDetail>.By<CashAccountPaymentMethodDetail.paymentMethodID, CashAccountPaymentMethodDetail.detailID>
    {
    }

    public class PaymentMethodForCashAccount : 
      PrimaryKeyOf<PaymentMethodAccount>.By<PaymentMethodAccount.paymentMethodID, PaymentMethodAccount.cashAccountID>.ForeignKeyOf<CashAccountPaymentMethodDetail>.By<CashAccountPaymentMethodDetail.paymentMethodID, CashAccountPaymentMethodDetail.cashAccountID>
    {
    }
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccountPaymentMethodDetail.cashAccountID>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2023R2.")]
  public abstract class accountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccountPaymentMethodDetail.accountID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountPaymentMethodDetail.paymentMethodID>
  {
  }

  public abstract class detailID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountPaymentMethodDetail.detailID>
  {
  }

  public abstract class detailValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountPaymentMethodDetail.detailValue>
  {
  }
}
