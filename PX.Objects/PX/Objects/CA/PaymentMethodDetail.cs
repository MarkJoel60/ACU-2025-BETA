// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.Common;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The additional setting that are required to use the payment method.
/// </summary>
[PXCacheName("Payment Method Detail")]
[Serializable]
public class PaymentMethodDetail : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ICCPaymentMethodDetail
{
  protected bool? _IsCVV;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (PaymentMethod.paymentMethodID))]
  [PXUIField(DisplayName = "Payment Method", Visible = false)]
  [PXParent(typeof (Select<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Current<PaymentMethodDetail.paymentMethodID>>>>))]
  public virtual 
  #nullable disable
  string PaymentMethodID { get; set; }

  /// <summary>
  /// The field identifies the type of records it belongs to.
  /// The list of the possible values can be found in <see cref="T:PX.Objects.CA.PaymentMethodDetailUsage" /> class.
  /// </summary>
  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Used In")]
  public virtual string UseFor { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "ID", Visible = true)]
  public virtual string DetailID { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXDefault]
  public virtual string Descr { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Entry Mask")]
  public virtual string EntryMask { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Validation Reg. Exp.")]
  public virtual string ValidRegexp { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Display Mask", Enabled = false)]
  public virtual string DisplayMask { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Encrypted")]
  public virtual bool? IsEncrypted { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Required")]
  public virtual bool? IsRequired { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [UniqueBool(typeof (PaymentMethodDetail.paymentMethodID))]
  public virtual bool? IsIdentifier { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exp. Date")]
  [UniqueBool(typeof (PaymentMethodDetail.paymentMethodID))]
  public virtual bool? IsExpirationDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Name on Card")]
  [UniqueBool(typeof (PaymentMethodDetail.paymentMethodID))]
  public virtual bool? IsOwnerName { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Payment Profile ID")]
  [UniqueBool(typeof (PaymentMethodDetail.paymentMethodID))]
  public virtual bool? IsCCProcessingID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "CVV Code")]
  [UniqueBool(typeof (PaymentMethodDetail.paymentMethodID))]
  public virtual bool? IsCVV
  {
    get => this._IsCVV;
    set => this._IsCVV = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Control Type")]
  [PXIntList(new int[] {0, 1}, new string[] {"Text", "Account Type List"})]
  public virtual int? ControlType { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Default Value")]
  public virtual string DefaultValue { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Sort Order")]
  public virtual short? OrderIndex { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public class PK : 
    PrimaryKeyOf<PaymentMethodDetail>.By<PaymentMethodDetail.paymentMethodID, PaymentMethodDetail.detailID>
  {
    public static PaymentMethodDetail Find(
      PXGraph graph,
      string paymentMethodID,
      string detailID,
      PKFindOptions options = 0)
    {
      return (PaymentMethodDetail) PrimaryKeyOf<PaymentMethodDetail>.By<PaymentMethodDetail.paymentMethodID, PaymentMethodDetail.detailID>.FindBy(graph, (object) paymentMethodID, (object) detailID, options);
    }
  }

  public static class FK
  {
    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<PaymentMethodDetail>.By<PaymentMethodDetail.paymentMethodID>
    {
    }
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethodDetail.paymentMethodID>
  {
  }

  public abstract class useFor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentMethodDetail.useFor>
  {
  }

  public abstract class detailID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentMethodDetail.detailID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentMethodDetail.descr>
  {
  }

  public abstract class entryMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentMethodDetail.entryMask>
  {
  }

  public abstract class validRegexp : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethodDetail.validRegexp>
  {
  }

  public abstract class displayMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethodDetail.displayMask>
  {
  }

  public abstract class isEncrypted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethodDetail.isEncrypted>
  {
  }

  public abstract class isRequired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethodDetail.isRequired>
  {
  }

  public abstract class isIdentifier : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethodDetail.isIdentifier>
  {
  }

  public abstract class isExpirationDate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethodDetail.isExpirationDate>
  {
  }

  public abstract class isOwnerName : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethodDetail.isOwnerName>
  {
  }

  public abstract class isCCProcessingID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethodDetail.isCCProcessingID>
  {
  }

  public abstract class isCVV : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethodDetail.isCVV>
  {
  }

  public abstract class controlType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PaymentMethodDetail.controlType>
  {
  }

  public abstract class defaultValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethodDetail.defaultValue>
  {
  }

  public abstract class orderIndex : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  PaymentMethodDetail.orderIndex>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PaymentMethodDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PaymentMethodDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethodDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PaymentMethodDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PaymentMethodDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethodDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PaymentMethodDetail.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PaymentMethodDetail.noteID>
  {
  }
}
