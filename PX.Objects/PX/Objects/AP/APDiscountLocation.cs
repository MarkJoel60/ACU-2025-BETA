// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDiscountLocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXCacheName("AP Discount Location")]
[Serializable]
public class APDiscountLocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DiscountID;
  protected string _DiscountSequenceID;
  protected int? _VendorID;
  protected int? _LocationID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsKey = true, IsUnicode = true)]
  [PXDBDefault(typeof (VendorDiscountSequence.discountID))]
  [PXUIField(DisplayName = "DiscountID")]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsKey = true, IsUnicode = true)]
  [PXDBDefault(typeof (VendorDiscountSequence.discountSequenceID))]
  [PXParent(typeof (APDiscountLocation.FK.VendorDiscountSequence))]
  [PXUIField(DisplayName = "DiscountSequenceID")]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDefault(typeof (VendorDiscountSequence.vendorID))]
  [PXDBInt(IsKey = true)]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDefault]
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<VendorDiscountSequence.vendorID>>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), Visibility = PXUIVisibility.SelectorVisible)]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
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
    PrimaryKeyOf<APDiscountLocation>.By<APDiscountLocation.discountID, APDiscountLocation.discountSequenceID, APDiscountLocation.vendorID, APDiscountLocation.locationID>
  {
    public static APDiscountLocation Find(
      PXGraph graph,
      int? discountID,
      string discountSequenceID,
      int? vendorID,
      int? locationID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APDiscountLocation>.By<APDiscountLocation.discountID, APDiscountLocation.discountSequenceID, APDiscountLocation.vendorID, APDiscountLocation.locationID>.FindBy(graph, (object) discountID, (object) discountSequenceID, (object) vendorID, (object) locationID, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APDiscountLocation>.By<APDiscountLocation.vendorID>
    {
    }

    public class VendorDiscountSequence : 
      PrimaryKeyOf<VendorDiscountSequence>.By<VendorDiscountSequence.vendorID, VendorDiscountSequence.discountID, VendorDiscountSequence.discountSequenceID>.ForeignKeyOf<APDiscountLocation>.By<APDiscountLocation.vendorID, APDiscountLocation.discountID, APDiscountLocation.discountSequenceID>
    {
    }

    public class Location : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<APDiscountLocation>.By<APDiscountLocation.vendorID, APDiscountLocation.locationID>
    {
    }
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APDiscountLocation.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDiscountLocation.discountSequenceID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APDiscountLocation.vendorID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APDiscountLocation.locationID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APDiscountLocation.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDiscountLocation.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APDiscountLocation.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APDiscountLocation.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDiscountLocation.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APDiscountLocation.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APDiscountLocation.Tstamp>
  {
  }
}
