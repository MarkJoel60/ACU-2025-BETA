// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerPaymentMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.CA;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Bql;
using PX.Objects.CR;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// The customer-specific settings of a <see cref="T:PX.Objects.CA.PaymentMethod">
/// payment method</see>. For instance payment methods (such as credit cards), a
/// customer payment method record is obligatory and defines all details
/// (see <see cref="T:PX.Objects.AR.CustomerPaymentMethodDetail" />) necessary to use the method
/// to record payments. For generic payment methods (such as cash or wire transfer),
/// the presence of a customer-specific payment method record is optional,
/// but it can nevertheless be defined to override the default payment method settings.
/// The entities of this type are edited on the Customer Payment Methods (AR303010)
/// form, which corresponds to the <see cref="T:PX.Objects.AR.CustomerPaymentMethodMaint" /> graph.
/// </summary>
[PXCacheName("Customer Payment Method")]
[PXPrimaryGraph(typeof (CustomerPaymentMethodMaint))]
[Serializable]
public class CustomerPaymentMethod : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ICCPaymentProfile,
  INotable
{
  protected bool? _Selected = new bool?(false);
  protected int? _BAccountID;
  protected int? _PMInstanceID;
  protected 
  #nullable disable
  string _PaymentMethodID;
  protected int? _CashAccountID;
  protected string _Descr;
  protected bool? _IsActive;
  protected bool? _IsDefault;
  protected Guid? _NoteID;
  protected DateTime? _ExpirationDate;
  protected int? _CVVVerifyTran;
  protected int? _BillAddressID;
  protected int? _BillContactID;
  protected bool? _HasBillingInfo;
  protected bool? _IsBillAddressSameAsMain;
  protected bool? _IsBillContactSameAsMain;
  protected bool? _Converted;
  protected bool? _AvailableOnPortals;
  protected bool? _IsPortalDefault;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected DateTime? _LastNotificationDate;

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the payment
  /// method record has been selected for processing.
  /// </summary>
  /// <value>This is a non-database bound field.</value>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// The identifier of <see cref="T:PX.Objects.AR.Customer">customer</see> to
  /// which the payment method belongs. This field is a part
  /// of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDefault(typeof (Customer.bAccountID))]
  [Customer(DescriptionField = typeof (Customer.acctName), IsKey = true, DirtyRead = true)]
  [PXParent(typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<CustomerPaymentMethod.bAccountID>>, And<PX.Objects.CR.BAccount.type, NotEqual<BAccountType.combinedType>>>>))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The unique identifier of the customer payment method.
  /// This field is part of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.PMInstance.PMInstanceID" />
  /// field. The <see cref="T:PX.Objects.CA.PMInstance" /> table provides identifiers
  /// for both <see cref="T:PX.Objects.CA.PaymentMethod">generic payment methods</see>
  /// and <see cref="T:PX.Objects.AR.CustomerPaymentMethod">customer payment methods</see>.
  /// </value>
  [PXDBForeignIdentity(typeof (PMInstance), IsKey = true)]
  [CustomerPaymentMethod.pMInstanceID.PMInstanceIDSelector(DescriptionField = typeof (CustomerPaymentMethod.paymentMethodID))]
  [PXUIField(DisplayName = "Card Number")]
  [PXReferentialIntegrityCheck]
  public virtual int? PMInstanceID
  {
    get => this._PMInstanceID;
    set => this._PMInstanceID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CA.PaymentMethod">payment method</see>
  /// associated with the customer payment method. The settings of this payment
  /// method are used as a template for the customer payment method.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.PaymentMethod.PaymentMethodID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And<PX.Objects.CA.PaymentMethod.paymentType, NotEqual<PaymentMethodType.posTerminal>>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXForeignReference(typeof (Field<CustomerPaymentMethod.paymentMethodID>.IsRelatedTo<PX.Objects.CA.PaymentMethod.paymentMethodID>))]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CA.CashAccount">cash account</see>
  /// associated with the customer payment method.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
  /// </value>
  [CashAccount(null, typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<CustomerPaymentMethod.paymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>>))]
  [PXDefault]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  /// <summary>The description of the payment method.</summary>
  /// <value>
  /// The value for this field is automatically generated
  /// by the system from the payment method description and
  /// <see cref="T:PX.Objects.AR.CustomerPaymentMethodDetail">payment method
  /// details</see> with applied display masks (if any).
  /// </value>
  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the customer
  /// payment method is available for recording payments.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  /// <summary>An unused obsolete field.</summary>
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Default", Enabled = false)]
  public virtual bool? IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
  }

  [PXNote(DescriptionField = typeof (CustomerPaymentMethod.descr))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>
  /// The expiration date of the customer payment method.
  /// Don't move the ExpirationDateAttribute down, it must be first.
  /// </summary>
  /// <value>
  /// The value of this field is filled in by the system automatically
  /// from the <see cref="T:PX.Objects.AR.CustomerPaymentMethodDetail">payment method
  /// detail</see> that corresponds to the expiration date, but only if
  /// the expiration date has no display mask.
  /// </value>
  [PX.Objects.AR.CCPaymentProcessing.ExpirationDate]
  [PXDBDateString(DateFormat = "MM/yy")]
  [PXUIField]
  public virtual DateTime? ExpirationDate
  {
    get => this._ExpirationDate;
    set => this._ExpirationDate = value;
  }

  /// <summary>
  /// This field is introduced to resolve sorting and filtering issue caused by
  /// ExpirationDate field as it behaves as a string while it is persisted as Date.
  /// Sole porrpose of this feild is to be used in grids only
  /// </summary>
  [PX.Objects.AR.CCPaymentProcessing.ExpirationDate]
  [PXDate(InputMask = "MM/yy", DisplayMask = "MM/yy")]
  [PXDBCalced(typeof (CustomerPaymentMethod.expirationDate), typeof (DateTime?))]
  [PXUIField(DisplayName = "Expiration Date", Enabled = false)]
  public virtual DateTime? ExpirationDateFormated { get; set; }

  /// <summary>
  /// Type of a card associated with the customer payment method.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Card/Account Type", Enabled = false)]
  [PX.Objects.AR.CardType.List]
  public virtual string CardType { get; set; }

  /// <summary>
  /// Original card type value received from the processing center.
  /// </summary>
  [PXDBString(25, IsFixed = true)]
  [PXUIField(DisplayName = "Proc. Center Card Type", Enabled = false)]
  public virtual string ProcCenterCardTypeCode { get; set; }

  /// <summary>
  /// Specifies display card type value.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXString(20, IsFixed = true)]
  [PXUIField(DisplayName = "Card/Account Type", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<CustomerPaymentMethod.cardType, IsNull>, Null, Case<Where<CustomerPaymentMethod.cardType, Equal<PX.Objects.AR.CardType.other>, And<CustomerPaymentMethod.procCenterCardTypeCode, IsNotNull>>, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<ListLabelOf<CustomerPaymentMethod.cardType>.Evaluator, Colon>>, IBqlString>.Concat<CustomerPaymentMethod.procCenterCardTypeCode>>>, ListLabelOf<CustomerPaymentMethod.cardType>>))]
  public virtual string DisplayCardType { get; set; }

  /// <summary>
  /// The identifier of the CVV code verification transaction.
  /// </summary>
  /// <value>
  /// Corresponds to <see cref="P:PX.Objects.AR.CCProcTran.TranNbr" />.
  /// </value>
  [PXDBInt]
  public virtual int? CVVVerifyTran
  {
    get => this._CVVVerifyTran;
    set => this._CVVVerifyTran = value;
  }

  /// <summary>
  /// For customer payment methods that require remittance information
  /// (that is, have <see cref="P:PX.Objects.AR.CustomerPaymentMethod.HasBillingInfo" /> set to <c>true</c>),
  /// contains the identifier of the <see cref="T:PX.Objects.CR.Address">billing
  /// address</see> associated with the payment method. The field
  /// defaults to the <see cref="P:PX.Objects.AR.Customer.DefBillAddressID"> default
  /// billing address of the customer</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Address.AddressID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
  public virtual int? BillAddressID
  {
    get => this._BillAddressID;
    set => this._BillAddressID = value;
  }

  /// <summary>
  /// For customer payment methods that require remittance information
  /// (that is, have <see cref="P:PX.Objects.AR.CustomerPaymentMethod.HasBillingInfo" /> set to <c>true</c>),
  /// contains the identifier of the <see cref="T:PX.Objects.CR.Contact">billing contact</see>
  /// associated with the payment method. The field defaults to the
  /// <see cref="P:PX.Objects.AR.Customer.DefBillContactID">default billing contact
  /// of the customer</see>.
  /// </summary>
  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  public virtual int? BillContactID
  {
    get => this._BillContactID;
    set => this._BillContactID = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the customer payment
  /// method requires remittance information. Defaults to
  /// <see cref="P:PX.Objects.CA.PaymentMethod.ARHasBillingInfo" />.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Has Billing Info", Visible = false, Enabled = false)]
  public virtual bool? HasBillingInfo
  {
    get => this._HasBillingInfo;
    set => this._HasBillingInfo = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the customer payment
  /// method should use the <see cref="P:PX.Objects.AR.Customer.DefBillAddressID">
  /// default billing address</see> of the associated customer record
  /// for sending payment remittance information.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Same as Main")]
  public virtual bool? IsBillAddressSameAsMain
  {
    get => this._IsBillAddressSameAsMain;
    set => this._IsBillAddressSameAsMain = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the customer payment
  /// method should use the <see cref="P:PX.Objects.AR.Customer.DefBillContactID">
  /// default billing contact</see> of the associated customer
  /// record for sending payment remittance information.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Same as Main")]
  public virtual bool? IsBillContactSameAsMain
  {
    get => this._IsBillContactSameAsMain;
    set => this._IsBillContactSameAsMain = value;
  }

  /// <summary>The identifier of the credit card processing center.</summary>
  /// <value>
  /// The field has a value if the customer payment method is configured
  /// to process payments through a payment gateway. The value corresponds to the
  /// value of the <see cref="T:PX.Objects.CA.CCProcessingCenterPmntMethod.processingCenterID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDBDefault(typeof (Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.isDefault, Equal<True>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>, And<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<CustomerPaymentMethod.paymentMethodID>>>>>>>))]
  [PXSelector(typeof (Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<CustomerPaymentMethod.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>>>>>))]
  [PXUIField]
  [DisabledProcCenter(CheckFieldValue = DisabledProcCenterAttribute.CheckFieldVal.ProcessingCenterId)]
  [DeprecatedProcessing(ChckVal = DeprecatedProcessingAttribute.CheckVal.ProcessingCenterId)]
  public virtual string CCProcessingCenterID { get; set; }

  /// <summary>
  /// The identifier of the customer profile associated with the customer
  /// account in Acumatica ERP and Authorize.Net. The main purpose of
  /// the identifier is to link multiple bank cards to a single customer
  /// entity and to synchronize record details between systems.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CustomerProcessingCenterID.CustomerCCPID" /> field.
  /// </value>
  [PXDBString(1024 /*0x0400*/, IsUnicode = true)]
  [PXDefault(typeof (Search<CustomerProcessingCenterID.customerCCPID, Where<CustomerProcessingCenterID.bAccountID, Equal<Current<CustomerPaymentMethod.bAccountID>>, And<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Current<CustomerPaymentMethod.cCProcessingCenterID>>>>, OrderBy<Desc<CustomerProcessingCenterID.createdDateTime>>>))]
  [PXSelector(typeof (Search<CustomerProcessingCenterID.customerCCPID, Where<CustomerProcessingCenterID.bAccountID, Equal<Current<CustomerPaymentMethod.bAccountID>>, And<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Current<CustomerPaymentMethod.cCProcessingCenterID>>>>>), ValidateValue = false)]
  [PXUIField]
  public virtual string CustomerCCPID { get; set; }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the customer payment
  /// method is the result of another payment method conversion using
  /// the Payment Method Converter (CA207000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  [Obsolete("This field will be removed in 2018R2 version.")]
  public virtual bool? Converted
  {
    get => this._Converted;
    set => this._Converted = value;
  }

  /// <summary>payment method is available on Portals.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Available on Portals")]
  public virtual bool? AvailableOnPortals
  {
    get => this._AvailableOnPortals;
    set => this._AvailableOnPortals = value;
  }

  /// <summary>payment method is default on Portal.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Default (Portal)")]
  public virtual bool? IsPortalDefault
  {
    get => this._IsPortalDefault;
    set => this._IsPortalDefault = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  [PXUIField]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <summary>
  /// The date of last notification to the customer about payment
  /// method expiration (e.g. such as expiration of a credit card).
  /// </summary>
  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Notification Date")]
  public virtual DateTime? LastNotificationDate
  {
    get => this._LastNotificationDate;
    set => this._LastNotificationDate = value;
  }

  public class PK : PrimaryKeyOf<CustomerPaymentMethod>.By<CustomerPaymentMethod.pMInstanceID>
  {
    public static CustomerPaymentMethod Find(
      PXGraph graph,
      int? pMInstanceID,
      PKFindOptions options = 0)
    {
      return (CustomerPaymentMethod) PrimaryKeyOf<CustomerPaymentMethod>.By<CustomerPaymentMethod.pMInstanceID>.FindBy(graph, (object) pMInstanceID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<CustomerPaymentMethod>.By<CustomerPaymentMethod.bAccountID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<CustomerPaymentMethod>.By<CustomerPaymentMethod.cashAccountID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<CustomerPaymentMethod>.By<CustomerPaymentMethod.paymentMethodID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustomerPaymentMethod.selected>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerPaymentMethod.bAccountID>
  {
  }

  public abstract class pMInstanceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerPaymentMethod.pMInstanceID>
  {
    /// <summary>
    /// Provides a selector for a Customer Payment Method - for example,<br />
    /// a list a credit cards that customer has. Customer is taken from the row<br />
    /// </summary>
    public class PMInstanceIDSelectorAttribute : PXSelectorAttribute
    {
      public PMInstanceIDSelectorAttribute()
        : base(typeof (Search<CustomerPaymentMethod.pMInstanceID, Where<CustomerPaymentMethod.bAccountID, Equal<Current<CustomerPaymentMethod.bAccountID>>>>))
      {
      }

      public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
      {
      }
    }
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethod.paymentMethodID>
  {
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerPaymentMethod.cashAccountID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerPaymentMethod.descr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustomerPaymentMethod.isActive>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustomerPaymentMethod.isDefault>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CustomerPaymentMethod.noteID>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerPaymentMethod.expirationDate>
  {
  }

  public abstract class expirationDateFormated : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerPaymentMethod.expirationDateFormated>
  {
  }

  public abstract class cardType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerPaymentMethod.cardType>
  {
  }

  public abstract class procCenterCardTypeCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethod.procCenterCardTypeCode>
  {
  }

  public abstract class displayCardType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethod.displayCardType>
  {
  }

  public abstract class cVVVerifyTran : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerPaymentMethod.cVVVerifyTran>
  {
  }

  public abstract class billAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerPaymentMethod.billAddressID>
  {
  }

  public abstract class billContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerPaymentMethod.billContactID>
  {
  }

  public abstract class hasBillingInfo : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerPaymentMethod.hasBillingInfo>
  {
  }

  public abstract class isBillAddressSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerPaymentMethod.isBillAddressSameAsMain>
  {
  }

  public abstract class isBillContactSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerPaymentMethod.isBillContactSameAsMain>
  {
  }

  public abstract class cCProcessingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethod.cCProcessingCenterID>
  {
  }

  public abstract class customerCCPID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethod.customerCCPID>
  {
  }

  public abstract class converted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustomerPaymentMethod.converted>
  {
  }

  public abstract class availableOnPortals : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerPaymentMethod.availableOnPortals>
  {
  }

  public abstract class isPortalDefault : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerPaymentMethod.isPortalDefault>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CustomerPaymentMethod.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethod.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerPaymentMethod.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CustomerPaymentMethod.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethod.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerPaymentMethod.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CustomerPaymentMethod.Tstamp>
  {
  }

  public abstract class lastNotificationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerPaymentMethod.lastNotificationDate>
  {
  }
}
