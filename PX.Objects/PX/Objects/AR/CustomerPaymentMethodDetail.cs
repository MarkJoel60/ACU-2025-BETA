// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerPaymentMethodDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.CA;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A payment method setting for a <see cref="T:PX.Objects.AR.CustomerPaymentMethod">
/// customer payment method</see>. The purpose of this entity is to define
/// customer-specific values for <see cref="T:PX.Objects.CA.PaymentMethodDetail">payment
/// method settings</see>. The entities of this type are created and edited
/// on the Customer Payment Methods (AR303010) form, which corresponds to
/// the <see cref="T:PX.Objects.AR.CustomerPaymentMethodMaint" /> graph.
/// </summary>
[PXCacheName("Customer Payment Method Detail")]
[Serializable]
public class CustomerPaymentMethodDetail : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ICCPaymentProfileDetail
{
  protected int? _PMInstanceID;
  protected 
  #nullable disable
  string _PaymentMethodID;
  protected string _DetailID;
  protected string _Value;
  protected byte[] _tstamp;
  protected string _CreatedByScreenID;
  protected Guid? _CreatedByID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The identifier of the payment method instance from the
  /// parent <see cref="T:PX.Objects.AR.CustomerPaymentMethod">customer payment
  /// method</see> record. This field is a part of the compound
  /// key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.CustomerPaymentMethod.PMInstanceID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (CustomerPaymentMethod.pMInstanceID))]
  [PXParent(typeof (Select<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Current<CustomerPaymentMethodDetail.pMInstanceID>>>>))]
  public virtual int? PMInstanceID
  {
    get => this._PMInstanceID;
    set => this._PMInstanceID = value;
  }

  /// <summary>
  /// The identifier of the payment method from the parent
  /// <see cref="T:PX.Objects.AR.CustomerPaymentMethod">customer payment method</see>
  /// record. This field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.CustomerPaymentMethod.PMInstanceID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (Search<CustomerPaymentMethod.paymentMethodID, Where<CustomerPaymentMethod.pMInstanceID, Equal<Current<CustomerPaymentMethodDetail.pMInstanceID>>>>))]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  /// <summary>
  /// The name of the payment method setting, such as Card Number,
  /// Expiration Date. This field is a part of the compound key
  /// of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.PaymentMethodDetail.DetailID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description", Enabled = false, IsReadOnly = true)]
  [CustomerPaymentMethodDetail.detailID.DetailIDSelector]
  public virtual string DetailID
  {
    get => this._DetailID;
    set => this._DetailID = value;
  }

  /// <summary>
  /// The value for the customer payment method setting,
  /// such as the actual credit card number, the expiration date.
  /// This value in this field can be subject to dynamic value
  /// validation depending on the regular expression defined
  /// in the corresponding <see cref="T:PX.Objects.CA.PaymentMethodDetail" />.
  /// </summary>
  [DynamicValueValidation(typeof (Search<PaymentMethodDetail.validRegexp, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<CustomerPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>, And<PaymentMethodDetail.detailID, Equal<Current<CustomerPaymentMethodDetail.detailID>>>>>>))]
  [PXDefault]
  [PXRSACryptStringWithMask(1028, typeof (Search<PaymentMethodDetail.entryMask, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<CustomerPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>, And<PaymentMethodDetail.detailID, Equal<Current<CustomerPaymentMethodDetail.detailID>>>>>>), IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  [PXPersonalDataFieldAttribute.Value]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<CustomerPaymentMethodDetail>.By<CustomerPaymentMethodDetail.pMInstanceID, CustomerPaymentMethodDetail.paymentMethodID, CustomerPaymentMethodDetail.detailID>
  {
    public static CustomerPaymentMethodDetail Find(
      PXGraph graph,
      int? pMInstanceID,
      string paymentMethodID,
      string detailID,
      PKFindOptions options = 0)
    {
      return (CustomerPaymentMethodDetail) PrimaryKeyOf<CustomerPaymentMethodDetail>.By<CustomerPaymentMethodDetail.pMInstanceID, CustomerPaymentMethodDetail.paymentMethodID, CustomerPaymentMethodDetail.detailID>.FindBy(graph, (object) pMInstanceID, (object) paymentMethodID, (object) detailID, options);
    }
  }

  public static class FK
  {
    public class CustomerPaymentMethod : 
      PrimaryKeyOf<CustomerPaymentMethod>.By<CustomerPaymentMethod.pMInstanceID>.ForeignKeyOf<CustomerPaymentMethodDetail>.By<CustomerPaymentMethodDetail.pMInstanceID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<CustomerPaymentMethodDetail>.By<CustomerPaymentMethodDetail.paymentMethodID>
    {
    }
  }

  public abstract class pMInstanceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerPaymentMethodDetail.pMInstanceID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethodDetail.paymentMethodID>
  {
  }

  public abstract class detailID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethodDetail.detailID>
  {
    public class DetailIDSelectorAttribute : PXSelectorAttribute
    {
      public DetailIDSelectorAttribute()
        : base(typeof (Search<PaymentMethodDetail.detailID, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<CustomerPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>))
      {
        this._UnconditionalSelect = this._PrimarySelect;
        this.DescriptionField = typeof (PaymentMethodDetail.descr);
        this.SelectorMode = (PXSelectorMode) 16 /*0x10*/;
      }
    }
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerPaymentMethodDetail.value>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    CustomerPaymentMethodDetail.Tstamp>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethodDetail.createdByScreenID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CustomerPaymentMethodDetail.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerPaymentMethodDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CustomerPaymentMethodDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethodDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerPaymentMethodDetail.lastModifiedDateTime>
  {
  }
}
