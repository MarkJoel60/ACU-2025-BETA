// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxReportLine
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
/// A line of a tax report. The class defines the structure of the tax report for a particular tax agency,
/// and is a part of formation amount rules.
/// The line is mapped as many-to-many to <see cref="T:PX.Objects.TX.TaxBucket">reporting groups</see> and through them to taxes.
/// </summary>
[PXCacheName("Tax Report Line")]
[Serializable]
public class TaxReportLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  protected int? _VendorID;
  protected int? _LineNbr;
  protected 
  #nullable disable
  string _LineType;
  protected short? _LineMult;
  protected string _TaxZoneID;
  protected bool? _NetTax;
  protected int? _TempLineNbr;
  protected string _Descr;
  protected string _ReportLineNbr;
  protected bool? _HideReportLine;
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

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Report Version", Visible = false)]
  [PXDefault(1)]
  public virtual int? TaxReportRevisionID { get; set; }

  /// <summary>
  /// The number of the report line. The field is a part of the primary key.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (TaxReport.lineCntr), DecrementOnDelete = false)]
  [PXParent(typeof (Select<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<TaxReportLine.vendorID>>>>))]
  [PXUIField]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  /// <summary>
  /// The type of the report line, which indicates whether the tax amount or taxable amount should be used to update the line.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"P"</c>: Tax amount.
  /// <c>"A"</c>: Taxable amount.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [PXUIField]
  [TaxReportLineType.List]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  /// <summary>The rule (sign) of updating the report line.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"1"</c>: +Output-Input.
  /// <c>"-1"</c>: +Input-Output.
  /// </value>
  [PXDBShort]
  [PXDefault(1)]
  [PXUIField]
  [PXIntList(new int[] {1, -1}, new string[] {"+Output-Input", "+Input-Output"})]
  public virtual short? LineMult
  {
    get => this._LineMult;
    set => this._LineMult = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.TX.TaxZone" />.
  /// If the field contains NULL, the report line contains aggregated data for all tax zones.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<TaxZone.taxZoneID>))]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  /// <summary>
  /// The field indicates (if set to <c>true</c>) that the line shows the net tax amount.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? NetTax
  {
    get => this._NetTax;
    set => this._NetTax = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the report line should be sliced by tax zones, and that the line is parent.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? TempLine { get; set; }

  /// <summary>
  /// The reference to the parent line (<see cref="P:PX.Objects.TX.TaxReportLine.LineNbr" />).
  /// The child lines are created for each tax zone if the <see cref="P:PX.Objects.TX.TaxReportLine.TempLine" /> of the parent line is set to <c>true</c>.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Parent Line")]
  public virtual int? TempLineNbr
  {
    get => this._TempLineNbr;
    set => this._TempLineNbr = value;
  }

  /// <summary>
  /// The description of the report line, which can be specified by the user.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>
  /// The number of the corresponding box of the original report form; the number is unique for each tax agency.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  public virtual string ReportLineNbr
  {
    get => this._ReportLineNbr;
    set => this._ReportLineNbr = value;
  }

  /// <summary>
  /// The calculation rule, which is filled in by the system automatically if the report line is an aggregate line and the appropriate settings have been specified.
  /// </summary>
  [PXString(50, IsUnicode = true)]
  [PXUIField]
  public virtual string BucketSum { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the line will not be included in the tax report during generation of the report.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? HideReportLine
  {
    get => this._HideReportLine;
    set => this._HideReportLine = value;
  }

  [PXDBInt]
  [PXUIField]
  public virtual int? SortOrder { get; set; }

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
    PrimaryKeyOf<TaxReportLine>.By<TaxReportLine.vendorID, TaxReportLine.taxReportRevisionID, TaxReportLine.lineNbr>
  {
    public static TaxReportLine Find(
      PXGraph graph,
      int? vendorID,
      int? taxReportRevisionID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (TaxReportLine) PrimaryKeyOf<TaxReportLine>.By<TaxReportLine.vendorID, TaxReportLine.taxReportRevisionID, TaxReportLine.lineNbr>.FindBy(graph, (object) vendorID, (object) taxReportRevisionID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxReportLine>.By<TaxReportLine.vendorID>
    {
    }

    public class TaxReport : 
      PrimaryKeyOf<TaxReport>.By<TaxReport.vendorID, TaxReport.revisionID>.ForeignKeyOf<TaxReportLine>.By<TaxReportLine.vendorID, TaxReportLine.taxReportRevisionID>
    {
    }
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxReportLine.vendorID>
  {
  }

  public abstract class taxReportRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxReportLine.taxReportRevisionID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxReportLine.lineNbr>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxReportLine.lineType>
  {
  }

  public abstract class lineMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  TaxReportLine.lineMult>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxReportLine.taxZoneID>
  {
  }

  public abstract class netTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxReportLine.netTax>
  {
  }

  public abstract class tempLine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxReportLine.tempLine>
  {
  }

  public abstract class tempLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxReportLine.tempLineNbr>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxReportLine.descr>
  {
  }

  public abstract class reportLineNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxReportLine.reportLineNbr>
  {
  }

  public abstract class bucketSum : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxReportLine.bucketSum>
  {
  }

  public abstract class hideReportLine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxReportLine.hideReportLine>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxReportLine.sortOrder>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxReportLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxReportLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxReportLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TaxReportLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxReportLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxReportLine.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxReportLine.Tstamp>
  {
  }
}
