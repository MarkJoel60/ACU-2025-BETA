// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxRev
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Globalization;

#nullable enable
namespace PX.Objects.TX;

[PXCacheName("Tax Revision")]
[Serializable]
public class TaxRev : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const 
  #nullable disable
  string DefaultStartDate = "01/01/1900";
  public const string DefaultEndDate = "06/06/9999";
  protected string _TaxID;
  protected int? _TaxVendorID;
  protected int? _RevisionID;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected Decimal? _TaxRate;
  protected bool? _Outdated;
  protected string _TaxType;
  protected int? _TaxBucketID;
  protected bool? _IsImported;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  public DateTime GetDefaultEndDate()
  {
    return DateTime.Parse("06/06/9999", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None);
  }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (Tax.taxID))]
  [PXUIField(DisplayName = "Tax ID", Visible = false)]
  [PXParent(typeof (Select<Tax, Where<Tax.taxID, Equal<Current<TaxRev.taxID>>>>))]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (Tax.taxVendorID))]
  public virtual int? TaxVendorID
  {
    get => this._TaxVendorID;
    set => this._TaxVendorID = value;
  }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "RevisionID", Visible = false)]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "06/06/9999")]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Rate")]
  public virtual Decimal? TaxRate
  {
    get => this._TaxRate;
    set => this._TaxRate = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField]
  public virtual Decimal? NonDeductibleTaxRate { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Min. Taxable Amount")]
  public virtual Decimal? TaxableMin { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Max. Taxable Amount")]
  public virtual Decimal? TaxableMax { get; set; }

  /// <summary>
  /// The maximum taxable quantity for Specific/Per Unit taxes.
  /// </summary>
  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Max. Taxable Quantity", FieldClass = "PerUnitTaxSupport")]
  public virtual Decimal? TaxableMaxQty { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Outdated
  {
    get => this._Outdated;
    set => this._Outdated = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("S")]
  [PXUIField]
  [PXStringList(new string[] {"S", "P"}, new string[] {"Output", "Input"})]
  public virtual string TaxType
  {
    get => this._TaxType;
    set => this._TaxType = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXIntList(new int[] {0}, new string[] {"undefined"})]
  [PXDefault]
  public virtual int? TaxBucketID
  {
    get => this._TaxBucketID;
    set => this._TaxBucketID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsImported
  {
    get => this._IsImported;
    set => this._IsImported = value;
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
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<TaxRev>.By<TaxRev.taxID, TaxRev.revisionID>
  {
    public static TaxRev Find(
      PXGraph graph,
      string taxID,
      DateTime? tranDate,
      int? revisionID,
      PKFindOptions options = 0)
    {
      return (TaxRev) PrimaryKeyOf<TaxRev>.By<TaxRev.taxID, TaxRev.revisionID>.FindBy(graph, (object) taxID, (object) revisionID, options);
    }
  }

  public static class FK
  {
    public class Tax : PrimaryKeyOf<Tax>.By<Tax.taxID>.ForeignKeyOf<TaxRev>.By<TaxRev.taxID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxRev>.By<TaxRev.taxVendorID>
    {
    }

    public class ReportingGroup : 
      PrimaryKeyOf<TaxBucket>.By<TaxBucket.vendorID, TaxBucket.bucketID>.ForeignKeyOf<TaxRev>.By<TaxRev.taxVendorID, TaxRev.taxBucketID>
    {
    }
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxRev.taxID>
  {
  }

  public abstract class taxVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxRev.taxVendorID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxRev.revisionID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxRev.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxRev.endDate>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxRev.taxRate>
  {
  }

  public abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxRev.nonDeductibleTaxRate>
  {
  }

  public abstract class taxableMin : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxRev.taxableMin>
  {
  }

  public abstract class taxableMax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxRev.taxableMax>
  {
  }

  /// <summary>
  /// The maximum taxable quantity for Specific/Per Unit taxes.
  /// </summary>
  public abstract class taxableMaxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxRev.taxableMaxQty>
  {
  }

  public abstract class outdated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxRev.outdated>
  {
  }

  public abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxRev.taxType>
  {
  }

  public abstract class taxBucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxRev.taxBucketID>
  {
  }

  public abstract class isImported : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxRev.isImported>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxRev.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxRev.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxRev.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxRev.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxRev.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxRev.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxRev.Tstamp>
  {
  }
}
