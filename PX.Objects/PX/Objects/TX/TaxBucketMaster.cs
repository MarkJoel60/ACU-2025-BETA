// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxBucketMaster
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.TX.Descriptor;
using System;

#nullable enable
namespace PX.Objects.TX;

[Serializable]
public class TaxBucketMaster : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected int? _BucketID;
  protected 
  #nullable disable
  string _BucketType;
  protected string _Descr;

  [PXDefault]
  [TaxAgencyActive]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt]
  [TaxBucketMaster.bucketID.TaxBucketSelector]
  [PXUIField]
  [PXDefault]
  public virtual int? BucketID
  {
    get => this._BucketID;
    set => this._BucketID = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Report Version", Required = true)]
  [PXSelector(typeof (Search<TaxReport.revisionID, Where<TaxReport.vendorID, Equal<Current<TaxBucketMaster.vendorID>>>, OrderBy<Desc<TaxReport.revisionID>>>), new Type[] {typeof (TaxReport.revisionID), typeof (TaxReport.validFrom)})]
  public virtual int? TaxReportRevisionID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("S")]
  [PXUIField]
  [LabelList(typeof (TaxType))]
  public virtual string BucketType
  {
    get => this._BucketType;
    set => this._BucketType = value;
  }

  [PXDBString(60, IsUnicode = true)]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxBucketMaster.vendorID>
  {
  }

  public abstract class bucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxBucketMaster.bucketID>
  {
    public class TaxBucketSelectorAttribute : PXSelectorAttribute
    {
      public TaxBucketSelectorAttribute()
        : base(typeof (Search<TaxBucket.bucketID, Where<TaxBucket.vendorID, Equal<Current<TaxBucketMaster.vendorID>>>>))
      {
        this.DescriptionField = typeof (TaxBucket.name);
        this._UnconditionalSelect = (BqlCommand) new Search<TaxBucket.bucketID, Where<TaxBucket.vendorID, Equal<Current<TaxBucketMaster.vendorID>>, And<TaxBucket.bucketID, Equal<Required<TaxBucket.bucketID>>>>>();
      }
    }
  }

  public abstract class taxReportRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxBucketMaster.taxReportRevisionID>
  {
  }

  public abstract class bucketType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxBucketMaster.bucketType>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxBucketMaster.descr>
  {
  }
}
