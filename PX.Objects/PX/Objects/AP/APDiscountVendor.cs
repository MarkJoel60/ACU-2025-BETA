// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDiscountVendor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXCacheName("AP Discount Vendor")]
[Serializable]
public class APDiscountVendor : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DiscountID;
  protected string _DiscountSequenceID;
  protected int? _VendorID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (VendorDiscountSequence.discountID))]
  [PXParent(typeof (Select<APDiscount, Where<APDiscount.discountID, Equal<Current<APDiscountVendor.discountID>>>>))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (VendorDiscountSequence.discountSequenceID))]
  [PXParent(typeof (Select<DiscountSequence, Where<DiscountSequence.discountSequenceID, Equal<Current<APDiscountVendor.discountSequenceID>>, And<DiscountSequence.discountID, Equal<Current<APDiscountVendor.discountID>>>>>))]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDefault(typeof (VendorDiscountSequence.vendorID))]
  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<Vendor, Where<Vendor.bAccountID, Equal<Current<APDiscountVendor.vendorID>>>>))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
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
    PrimaryKeyOf<APDiscountVendor>.By<APDiscountVendor.discountID, APDiscountVendor.discountSequenceID, APDiscountVendor.vendorID>
  {
    public static APDiscountVendor Find(
      PXGraph graph,
      int? discountID,
      string discountSequenceID,
      int? vendorID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APDiscountVendor>.By<APDiscountVendor.discountID, APDiscountVendor.discountSequenceID, APDiscountVendor.vendorID>.FindBy(graph, (object) discountID, (object) discountSequenceID, (object) vendorID, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APDiscountVendor>.By<APDiscountVendor.vendorID>
    {
    }
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APDiscountVendor.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDiscountVendor.discountSequenceID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APDiscountVendor.vendorID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APDiscountVendor.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDiscountVendor.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APDiscountVendor.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APDiscountVendor.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDiscountVendor.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APDiscountVendor.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APDiscountVendor.Tstamp>
  {
  }
}
