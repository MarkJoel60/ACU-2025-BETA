// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerPaymentMethodInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.Common.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A service projection DAC that is used to display customer payment method
/// settings on the Customers (AR303000) form. The DAC is designed so that the
/// <see cref="T:PX.Objects.AR.CustomerPaymentMethod">customer payment method</see> settings are
/// displayed if they exist; <see cref="T:PX.Objects.CA.PaymentMethod">generic payment method</see>
/// settings are displayed otherwise.
/// </summary>
[PXCacheName("Customer Payment Method")]
[PXProjection(typeof (Select2<PMInstance, LeftJoin<PX.Objects.CA.PaymentMethod, On<PMInstance.pMInstanceID, Equal<PX.Objects.CA.PaymentMethod.pMInstanceID>>, LeftJoin<CustomerPaymentMethod, On<PMInstance.pMInstanceID, Equal<CustomerPaymentMethod.pMInstanceID>>, LeftJoin<PaymentMethodActive, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<PaymentMethodActive.paymentMethodID>, Or<CustomerPaymentMethod.paymentMethodID, Equal<PaymentMethodActive.paymentMethodID>>>>>>, Where2<Where<PX.Objects.CA.PaymentMethod.aRIsOnePerCustomer, Equal<True>, Or<PX.Objects.CA.PaymentMethod.aRIsOnePerCustomer, IsNull, Or<Where<PX.Objects.CA.PaymentMethod.aRIsOnePerCustomer, Equal<False>, And<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.creditCard>>>>>>, And2<Where<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, Or<PX.Objects.CA.PaymentMethod.useForAR, IsNull>>, And<Where<PX.Objects.CA.PaymentMethod.pMInstanceID, IsNotNull, Or<CustomerPaymentMethod.pMInstanceID, IsNotNull>>>>>>))]
[Serializable]
public class CustomerPaymentMethodInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Descr;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.Customer" /> record
  /// from the <see cref="T:PX.Objects.AR.CustomerPaymentMethod">customer
  /// payment method</see> settings.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.CustomerPaymentMethod.BAccountID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (CustomerPaymentMethod.bAccountID))]
  public virtual int? BAccountID { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the payment method
  /// is used by default for the customer.
  /// </summary>
  /// <value>This is an unbound field.</value>
  [PXDBBool]
  [PXUIField(DisplayName = "Is Default")]
  public virtual bool? IsDefault { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CA.PaymentMethod">payment method</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.PaymentMethod.PaymentMethodID" /> field.
  /// </value>
  [PXString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.CA.PaymentMethod.paymentMethodID, IsNotNull>, PX.Objects.CA.PaymentMethod.paymentMethodID>, CustomerPaymentMethod.paymentMethodID>), typeof (string))]
  public virtual string PaymentMethodID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CA.PMInstance">payment method instance</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.PMInstance.PMInstanceID" /> field.
  /// </value>
  [PXDBInt(IsKey = true, BqlField = typeof (PMInstance.pMInstanceID))]
  [DeprecatedProcessing(ErrorMappedFieldName = "Descr")]
  [DisabledProcCenter(ErrorMappedFieldName = "Descr")]
  public virtual int? PMInstanceID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CA.CashAccount">cash account</see>
  /// from the <see cref="T:PX.Objects.AR.CustomerPaymentMethod">customer payment method</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (CustomerPaymentMethod.cashAccountID))]
  [PXSelector(typeof (PX.Objects.CA.CashAccount.cashAccountID), SubstituteKey = typeof (PX.Objects.CA.CashAccount.cashAccountCD))]
  [PXUIField]
  public virtual int? CashAccountID { get; set; }

  /// <summary>The description of the payment method.</summary>
  /// <value>
  /// The value of the field is taken from either the <see cref="P:PX.Objects.CA.PaymentMethod.Descr" />
  /// field, or the <see cref="P:PX.Objects.AR.CustomerPaymentMethod.Descr" /> field (if the
  /// <see cref="P:PX.Objects.CA.PaymentMethod.Descr" /> field is empty).
  /// </value>
  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true, NonDB = true, BqlField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXDefault("")]
  [PXUIField]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.CA.PaymentMethod.descr, IsNotNull>, PX.Objects.CA.PaymentMethod.descr>, CustomerPaymentMethod.descr>), typeof (string))]
  public virtual string Descr { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the payment method is active.
  /// </summary>
  /// <value>
  /// The value of the field is taken from either the <see cref="P:PX.Objects.CA.PaymentMethod.IsActive" />
  /// field, or the <see cref="P:PX.Objects.AR.CustomerPaymentMethod.IsActive" /> field (if the
  /// <see cref="P:PX.Objects.CA.PaymentMethod.IsActive" /> field is <c>null</c> or <c>false</c>).
  /// </value>
  [PXBool]
  [PXDefault(true)]
  [PXUIField]
  [PXDBCalced(typeof (Switch<Case<Where<PaymentMethodActive.isActive, Equal<True>>, Switch<Case<Where<CustomerPaymentMethod.isActive, IsNotNull>, CustomerPaymentMethod.isActive>, PX.Objects.CA.PaymentMethod.isActive>>, Null>), typeof (bool))]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that there can be only one
  /// instance of the payment method for each <see cref="T:PX.Objects.AR.Customer">
  /// customer</see>.
  /// </summary>
  [PXDBBool(BqlField = typeof (PX.Objects.CA.PaymentMethod.aRIsOnePerCustomer))]
  [PXDefault(false)]
  public virtual bool? ARIsOnePerCustomer { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the payment method has
  /// been overridden at the <see cref="T:PX.Objects.AR.Customer">customer</see>
  /// level; that is, there is a <see cref="T:PX.Objects.AR.CustomerPaymentMethod" />
  /// record defined for the combination of the customer record
  /// and the generic payment method.
  /// </summary>
  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<CustomerPaymentMethod.pMInstanceID, IsNotNull>, True>, False>), typeof (bool))]
  [PXUIField(DisplayName = "Override", Enabled = false)]
  public virtual bool? IsCustomerPaymentMethod { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.CustomerPaymentMethod.ExpirationDate" />
  [PX.Objects.AR.CCPaymentProcessing.ExpirationDate]
  [PXDBDateString(DateFormat = "MM/yy", BqlField = typeof (CustomerPaymentMethod.expirationDate))]
  [PXUIField]
  public virtual DateTime? ExpirationDate { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.CustomerPaymentMethod.CCProcessingCenterID" />
  [PXDBString(10, IsUnicode = true, BqlField = typeof (CustomerPaymentMethod.cCProcessingCenterID))]
  [PXUIField]
  [DisabledProcCenter(CheckFieldValue = DisabledProcCenterAttribute.CheckFieldVal.ProcessingCenterId)]
  [DeprecatedProcessing(ChckVal = DeprecatedProcessingAttribute.CheckVal.ProcessingCenterId)]
  public virtual string CCProcessingCenterID { get; set; }

  [PXDBDateAndTime(BqlField = typeof (CustomerPaymentMethod.createdDateTime))]
  [PXUIField]
  public virtual DateTime? CreatedDateTime { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.CustomerPaymentMethod.AvailableOnPortals" />
  [PXDBBool(BqlField = typeof (CustomerPaymentMethod.availableOnPortals))]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? AvailableOnPortals { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.CustomerPaymentMethod.IsPortalDefault" />
  [PXDBBool(BqlField = typeof (CustomerPaymentMethod.isPortalDefault))]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsPortalDefault { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CA.PaymentMethod.PaymentType" />
  [PXDBString(BqlField = typeof (PX.Objects.CA.PaymentMethod.paymentType))]
  [PXDefault("")]
  public virtual string PaymentType { get; set; }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<CustomerPaymentMethodInfo>.By<CustomerPaymentMethodInfo.bAccountID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<CustomerPaymentMethodInfo>.By<CustomerPaymentMethodInfo.paymentMethodID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<CustomerPaymentMethodInfo>.By<CustomerPaymentMethodInfo.cashAccountID>
    {
    }
  }

  public abstract class bAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.bAccountID>
  {
  }

  public abstract class isDefault : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.isDefault>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.paymentMethodID>
  {
  }

  public abstract class pMInstanceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.pMInstanceID>
  {
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.cashAccountID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerPaymentMethodInfo.descr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustomerPaymentMethodInfo.isActive>
  {
  }

  public abstract class aRIsOnePerCustomer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.aRIsOnePerCustomer>
  {
  }

  public abstract class isCustomerPaymentMethod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.isCustomerPaymentMethod>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.expirationDate>
  {
  }

  public abstract class cCProcessingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.cCProcessingCenterID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.createdDateTime>
  {
  }

  public abstract class availableOnPortals : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.availableOnPortals>
  {
  }

  public abstract class isPortalDefault : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.isPortalDefault>
  {
  }

  public abstract class paymentType : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerPaymentMethodInfo.paymentType>
  {
  }
}
