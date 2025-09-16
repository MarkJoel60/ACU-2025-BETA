// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxBucketLine
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
/// A detail of <see cref="T:PX.Objects.TX.TaxBucket" />. It implements the many-to-many relationship
/// between <see cref="T:PX.Objects.TX.TaxReportLine" /> and <see cref="T:PX.Objects.TX.TaxBucket" />.
/// The records of this type are also created for the report lines that are detailed by tax zones.
/// </summary>
[PXCacheName("Tax Group Line")]
[Serializable]
public class TaxBucketLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected int? _BucketID;
  protected Guid? _CreatedByID;
  protected 
  #nullable disable
  string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (TaxBucketMaster.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>
  /// The foreign key to the master record (<see cref="T:PX.Objects.TX.TaxBucket" />).
  /// The field is a part of the primary key.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (TaxBucketMaster.bucketID))]
  public virtual int? BucketID
  {
    get => this._BucketID;
    set => this._BucketID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (TaxBucketMaster.taxReportRevisionID))]
  [PXUIField]
  public virtual int? TaxReportRevisionID { get; set; }

  /// <summary>
  /// The reference to the report line (<see cref="T:PX.Objects.TX.TaxReportLine" />), which is included in the reporting group (<see cref="T:PX.Objects.TX.TaxBucket" />).
  /// The field is a part of the primary key.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [TaxReportLineSelector(typeof (Search<TaxReportLine.lineNbr, Where<TaxReportLine.vendorID, Equal<Current<TaxBucketLine.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Current<TaxBucketLine.taxReportRevisionID>>, And<TaxReportLine.tempLineNbr, IsNull>>>>), new Type[] {typeof (TaxReportLine.sortOrder), typeof (TaxReportLine.descr), typeof (TaxReportLine.reportLineNbr), typeof (TaxReportLine.bucketSum), typeof (TaxReportLine.taxReportRevisionID)}, SubstituteKey = typeof (TaxReportLine.sortOrder))]
  public virtual int? LineNbr { get; set; }

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

  public class PK : 
    PrimaryKeyOf<TaxBucketLine>.By<TaxBucketLine.vendorID, TaxBucketLine.bucketID, TaxBucketLine.taxReportRevisionID, TaxBucketLine.lineNbr>
  {
    public static TaxBucketLine Find(
      PXGraph graph,
      int? vendorID,
      int? bucketID,
      int? taxReportRevisionID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (TaxBucketLine) PrimaryKeyOf<TaxBucketLine>.By<TaxBucketLine.vendorID, TaxBucketLine.bucketID, TaxBucketLine.taxReportRevisionID, TaxBucketLine.lineNbr>.FindBy(graph, (object) vendorID, (object) bucketID, (object) taxReportRevisionID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxBucketLine>.By<TaxBucketLine.vendorID>
    {
    }

    public class TaxBucket : 
      PrimaryKeyOf<TaxBucket>.By<TaxBucket.vendorID, TaxBucket.bucketID>.ForeignKeyOf<TaxBucketLine>.By<TaxBucketLine.vendorID, TaxBucketLine.bucketID>
    {
    }

    public class ReportLine : 
      PrimaryKeyOf<TaxReportLine>.By<TaxReportLine.vendorID, TaxReportLine.taxReportRevisionID, TaxReportLine.lineNbr>.ForeignKeyOf<TaxBucketLine>.By<TaxBucketLine.vendorID, TaxBucketLine.taxReportRevisionID, TaxBucketLine.lineNbr>
    {
    }
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxBucketLine.vendorID>
  {
  }

  public abstract class bucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxBucketLine.bucketID>
  {
  }

  public abstract class taxReportRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxBucketLine.taxReportRevisionID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxBucketLine.lineNbr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxBucketLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxBucketLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxBucketLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TaxBucketLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxBucketLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxBucketLine.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxBucketLine.Tstamp>
  {
  }
}
