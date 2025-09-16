// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxBucket
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.TX;

/// <summary>
/// A reporting group. The head of the master-detail aggregate,
/// which specifies a set of report lines to which tax amounts should be aggregated on report prepare.
/// </summary>
[PXCacheName("Tax Group")]
[Serializable]
public class TaxBucket : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected int? _BucketID;
  protected 
  #nullable disable
  string _BucketType;
  protected string _Name;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>
  /// The identifier of the reporting group.
  /// The field is a part of the primary key.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual int? BucketID
  {
    get => this._BucketID;
    set => this._BucketID = value;
  }

  /// <summary>The type of the reporting group.</summary>
  /// 
  ///             /// <value>
  /// The field can have one of the following values:
  /// <c>"S"</c>: Output (sales).
  /// <c>"P"</c>: Input (purchase).
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("S")]
  [PXUIField]
  [PXStringList(new string[] {"S", "P"}, new string[] {"Output", "Input"})]
  public virtual string BucketType
  {
    get => this._BucketType;
    set => this._BucketType = value;
  }

  /// <summary>
  /// The name of the reporting group, which can be specified by the user.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<TaxBucket>.By<TaxBucket.vendorID, TaxBucket.bucketID>
  {
    public static TaxBucket Find(
      PXGraph graph,
      int? vendorID,
      int? bucketID,
      PKFindOptions options = 0)
    {
      return (TaxBucket) PrimaryKeyOf<TaxBucket>.By<TaxBucket.vendorID, TaxBucket.bucketID>.FindBy(graph, (object) vendorID, (object) bucketID, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxBucket>.By<TaxBucket.vendorID>
    {
    }
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxBucket.vendorID>
  {
  }

  public abstract class bucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxBucket.bucketID>
  {
  }

  public abstract class bucketType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxBucket.bucketType>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxBucket.name>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxBucket.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxBucket.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxBucket.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxBucket.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxBucket.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxBucket.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxBucket.Tstamp>
  {
  }
}
