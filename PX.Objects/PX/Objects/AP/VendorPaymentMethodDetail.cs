// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorPaymentMethodDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Vendor-specific values for AP-related payment method settings
/// (which are stored in <see cref="T:PX.Objects.CA.PaymentMethodDetail" />).
/// They are edited on the Payment tab of the Vendor Locations (AP303010) form.
/// For the main vendor location, they can also be edited on the Payment tab of the Vendors (AP303000) form.
/// </summary>
[PXCacheName("Payment Type Detail")]
[Serializable]
public class VendorPaymentMethodDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BAccountID;
  protected int? _LocationID;
  protected 
  #nullable disable
  string _PaymentMethodID;
  protected string _DetailID;
  protected string _DetailValue;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.CR.Location.bAccountID))]
  [PXUIField(DisplayName = "BAccountID", Visible = false, Enabled = false)]
  [PXParent(typeof (Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<VendorPaymentMethodDetail.bAccountID>>>>))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.CR.Location.locationID))]
  [PXUIField(Visible = false, Enabled = false, Visibility = PXUIVisibility.Invisible)]
  [PXParent(typeof (Select<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<VendorPaymentMethodDetail.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<VendorPaymentMethodDetail.locationID>>>>>))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vPaymentMethodID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<VendorPaymentMethodDetail.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<VendorPaymentMethodDetail.locationID>>>>>))]
  [PXUIField(DisplayName = "Payment Method", Visible = false)]
  [PXSelector(typeof (PX.Objects.CA.PaymentMethod.paymentMethodID), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXParent(typeof (Select<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<VendorPaymentMethodDetail.paymentMethodID>>>>))]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (Search<PaymentMethodDetail.detailID, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<VendorPaymentMethodDetail.paymentMethodID>>>>))]
  [PXUIField(DisplayName = "ID", Visible = true, Enabled = true)]
  [PXParent(typeof (Select<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<VendorPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, And<PaymentMethodDetail.detailID, Equal<Current<VendorPaymentMethodDetail.detailID>>>>>>))]
  public virtual string DetailID
  {
    get => this._DetailID;
    set => this._DetailID = value;
  }

  [PXDBStringWithMask(255 /*0xFF*/, typeof (Search<PaymentMethodDetail.entryMask, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<VendorPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Current<VendorPaymentMethodDetail.detailID>>, PX.Data.And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>), IsUnicode = true)]
  [DynamicValueValidation(typeof (Search<PaymentMethodDetail.validRegexp, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<VendorPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Current<VendorPaymentMethodDetail.detailID>>, PX.Data.And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>))]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Value")]
  public virtual string DetailValue
  {
    get => this._DetailValue;
    set => this._DetailValue = value;
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
  public virtual System.DateTime? CreatedDateTime
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
  public virtual System.DateTime? LastModifiedDateTime
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

  public class PK : 
    PrimaryKeyOf<VendorPaymentMethodDetail>.By<VendorPaymentMethodDetail.bAccountID, VendorPaymentMethodDetail.locationID, VendorPaymentMethodDetail.paymentMethodID, VendorPaymentMethodDetail.detailID>
  {
    public static VendorPaymentMethodDetail Find(
      PXGraph graph,
      int? bAccountID,
      int? locationID,
      string paymentMethodID,
      string detailID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<VendorPaymentMethodDetail>.By<VendorPaymentMethodDetail.bAccountID, VendorPaymentMethodDetail.locationID, VendorPaymentMethodDetail.paymentMethodID, VendorPaymentMethodDetail.detailID>.FindBy(graph, (object) bAccountID, (object) locationID, (object) paymentMethodID, (object) detailID, options);
    }
  }

  public static class FK
  {
    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<VendorPaymentMethodDetail>.By<VendorPaymentMethodDetail.bAccountID>
    {
    }

    public class Location : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<VendorPaymentMethodDetail>.By<VendorPaymentMethodDetail.bAccountID, VendorPaymentMethodDetail.locationID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<VendorPaymentMethodDetail>.By<VendorPaymentMethodDetail.paymentMethodID>
    {
    }
  }

  public abstract class bAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorPaymentMethodDetail.bAccountID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorPaymentMethodDetail.locationID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorPaymentMethodDetail.paymentMethodID>
  {
  }

  public abstract class detailID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorPaymentMethodDetail.detailID>
  {
  }

  public abstract class detailValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorPaymentMethodDetail.detailValue>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    VendorPaymentMethodDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorPaymentMethodDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    VendorPaymentMethodDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    VendorPaymentMethodDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorPaymentMethodDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    VendorPaymentMethodDetail.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    VendorPaymentMethodDetail.Tstamp>
  {
  }
}
