// Decompiled with JetBrains decompiler
// Type: PX.CS.RMReport
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.Search;
using System;

#nullable enable
namespace PX.CS;

[PXCacheName("Report")]
[PXPrimaryGraph(typeof (RMReportMaint))]
public class RMReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReportCode;
  protected string _Description;
  protected string _Type;
  protected string _RowSetCode;
  protected string _ColumnSetCode;
  protected string _UnitSetCode;
  protected string _StartUnitCode;
  protected bool? _Landscape;
  protected bool? _ApplyRestrictionGroups;
  protected short? _PaperKind;
  protected int? _StyleID;
  protected int? _DataSourceID;
  protected double? _MarginLeft;
  protected short? _MarginLeftType;
  protected double? _MarginRight;
  protected short? _MarginRightType;
  protected double? _MarginTop;
  protected short? _MarginTopType;
  protected double? _MarginBottom;
  protected short? _MarginBottomType;
  protected double? _Width;
  protected short? _WidthType;
  protected double? _Height;
  protected short? _HeightType;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Code", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault]
  [PXSelector(typeof (RMReport.reportCode))]
  public virtual string ReportCode
  {
    get => this._ReportCode;
    set => this._ReportCode = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBGuid(false)]
  public Guid? ReportUID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Type", Required = true, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXSelector(typeof (PX.Data.Search<RMRowSet.rowSetCode, Where<RMRowSet.type, Equal<Current<RMReport.type>>>>), new System.Type[] {typeof (RMRowSet.rowSetCode), typeof (RMRowSet.type), typeof (RMRowSet.description)}, DescriptionField = typeof (RMRowSet.description))]
  [PXUIField(DisplayName = "Row Set")]
  [PXForeignReference(typeof (RMReport.FK.RowSet))]
  public virtual string RowSetCode
  {
    get => this._RowSetCode;
    set => this._RowSetCode = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXSelector(typeof (PX.Data.Search<RMColumnSet.columnSetCode, Where<RMColumnSet.type, Equal<Current<RMReport.type>>>>), DescriptionField = typeof (RMColumnSet.description))]
  [PXUIField(DisplayName = "Column Set")]
  [PXForeignReference(typeof (RMReport.FK.ColumnSet))]
  public virtual string ColumnSetCode
  {
    get => this._ColumnSetCode;
    set => this._ColumnSetCode = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXSelector(typeof (PX.Data.Search<RMUnitSet.unitSetCode, Where<RMUnitSet.type, Equal<Current<RMReport.type>>>>), DescriptionField = typeof (RMUnitSet.description))]
  [PXUIField(DisplayName = "Unit Set")]
  [PXForeignReference(typeof (RMReport.FK.UnitSet))]
  public virtual string UnitSetCode
  {
    get => this._UnitSetCode;
    set => this._UnitSetCode = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXSelector(typeof (PX.Data.Search<RMUnit.unitCode, Where<RMUnit.unitSetCode, Equal<Optional<RMReport.unitSetCode>>>>), DescriptionField = typeof (RMUnit.description))]
  [PXUIField(DisplayName = "Start Unit")]
  public virtual string StartUnitCode
  {
    get => this._StartUnitCode;
    set => this._StartUnitCode = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Landscape")]
  public virtual bool? Landscape
  {
    get => this._Landscape;
    set => this._Landscape = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Apply Restriction Groups")]
  public virtual bool? ApplyRestrictionGroups
  {
    get => this._ApplyRestrictionGroups;
    set => this._ApplyRestrictionGroups = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Paper Kind")]
  [PXIntList(typeof (System.Drawing.Printing.PaperKind), false)]
  public virtual short? PaperKind
  {
    get => this._PaperKind;
    set => this._PaperKind = value;
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (RMStyle.styleID))]
  public virtual int? StyleID
  {
    get => this._StyleID;
    set => this._StyleID = value;
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (RMDataSource.dataSourceID))]
  [PXForeignReference(typeof (RMReport.FK.DataSource))]
  public virtual int? DataSourceID
  {
    get => this._DataSourceID;
    set => this._DataSourceID = value;
  }

  [PXString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Title", Enabled = false)]
  public string SitemapTitle { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Left")]
  [PXDefault(0.0, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual double? MarginLeft
  {
    get => this._MarginLeft;
    set => this._MarginLeft = value;
  }

  [SizeType(DisplayName = "Margin Left Type")]
  public virtual short? MarginLeftType
  {
    get => this._MarginLeftType;
    set => this._MarginLeftType = value;
  }

  [PXDBDouble]
  [PXUIField(DisplayName = "Right")]
  [PXDefault(0.0, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual double? MarginRight
  {
    get => this._MarginRight;
    set => this._MarginRight = value;
  }

  [SizeType(DisplayName = "Margin Right Type")]
  public virtual short? MarginRightType
  {
    get => this._MarginRightType;
    set => this._MarginRightType = value;
  }

  [PXDBDouble]
  [PXUIField(DisplayName = "Top")]
  [PXDefault(0.0, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual double? MarginTop
  {
    get => this._MarginTop;
    set => this._MarginTop = value;
  }

  [SizeType(DisplayName = "Margin Top Type")]
  public virtual short? MarginTopType
  {
    get => this._MarginTopType;
    set => this._MarginTopType = value;
  }

  [PXDBDouble]
  [PXUIField(DisplayName = "Bottom")]
  [PXDefault(0.0, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual double? MarginBottom
  {
    get => this._MarginBottom;
    set => this._MarginBottom = value;
  }

  [SizeType(DisplayName = "Margin Bottom Type")]
  public virtual short? MarginBottomType
  {
    get => this._MarginBottomType;
    set => this._MarginBottomType = value;
  }

  [PXDBDouble]
  [PXUIField(DisplayName = "Width")]
  public virtual double? Width
  {
    get => this._Width;
    set => this._Width = value;
  }

  [SizeType(DisplayName = "Width Type")]
  public virtual short? WidthType
  {
    get => this._WidthType;
    set => this._WidthType = value;
  }

  [PXDBDouble]
  [PXUIField(DisplayName = "Height")]
  public virtual double? Height
  {
    get => this._Height;
    set => this._Height = value;
  }

  [SizeType(DisplayName = "Height Type")]
  public virtual short? HeightType
  {
    get => this._HeightType;
    set => this._HeightType = value;
  }

  [PXNote]
  [PXSearchable(65535 /*0xFFFF*/, "Report: {0}", new System.Type[] {typeof (RMReport.reportCode)}, new System.Type[] {typeof (RMReport.description), typeof (PX.SM.SiteMap.title), typeof (PX.SM.SiteMap.screenID)}, typeof (ForeignDAC<PX.SM.SiteMap>.GetFields<PX.SM.SiteMap.title, PX.SM.SiteMap.screenID>.WithParameter<RMReport.reportUID>.AndQuery<Select2<PX.SM.SiteMap, InnerJoin<RMReport, On<PX.SM.SiteMap.url, In3<Add<RMReport.RMReportParameters.rmReportUrl, RMReport.reportCode>, Add<RMReport.RMReportParameters.rmReportUrl, Add<RMReport.reportCode, RMReport.RMReportParameters.rmReportPostfix>>>, And<PX.SM.SiteMap.screenID, PX.Data.StartsWith<RMReport.RMReportParameters.rmReportScreenPrefix>>>>, Where<RMReport.reportUID, Equal<Required<RMReport.reportUID>>>>>), Line1Format = "{0}, {1}", Line1Fields = new System.Type[] {typeof (PX.SM.SiteMap.title), typeof (PX.SM.SiteMap.screenID)}, Line2Format = "{0}, {1}", Line2Fields = new System.Type[] {typeof (RMReport.type), typeof (RMReport.description)}, SelectForFastIndexing = typeof (Select2<RMReport, InnerJoin<PX.SM.SiteMap, On<PX.SM.SiteMap.url, In3<Add<RMReport.RMReportParameters.rmReportUrl, RMReport.reportCode>, Add<RMReport.RMReportParameters.rmReportUrl, Add<RMReport.reportCode, RMReport.RMReportParameters.rmReportPostfix>>>, And<PX.SM.SiteMap.screenID, PX.Data.StartsWith<RMReport.RMReportParameters.rmReportScreenPrefix>>>>>))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
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

  public class PK : PrimaryKeyOf<RMReport>.By<RMReport.reportCode>
  {
    public static RMReport Find(PXGraph graph, string reportCode, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RMReport>.By<RMReport.reportCode>.FindBy(graph, (object) reportCode, options);
    }
  }

  public static class FK
  {
    public class RowSet : 
      PrimaryKeyOf<RMRowSet>.By<RMRowSet.rowSetCode>.ForeignKeyOf<RMReport>.By<RMReport.rowSetCode>
    {
    }

    public class ColumnSet : 
      PrimaryKeyOf<RMColumnSet>.By<RMColumnSet.columnSetCode>.ForeignKeyOf<RMReport>.By<RMReport.columnSetCode>
    {
    }

    public class UnitSet : 
      PrimaryKeyOf<RMUnitSet>.By<RMUnitSet.unitSetCode>.ForeignKeyOf<RMReport>.By<RMReport.unitSetCode>
    {
    }

    public class DataSource : 
      PrimaryKeyOf<RMDataSource>.By<RMDataSource.dataSourceID>.ForeignKeyOf<RMReport>.By<RMReport.dataSourceID>
    {
    }
  }

  public abstract class reportCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMReport.reportCode>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMReport.description>
  {
  }

  public abstract class reportUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMReport.reportUID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMReport.type>
  {
  }

  public abstract class rowSetCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMReport.rowSetCode>
  {
  }

  public abstract class columnSetCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMReport.columnSetCode>
  {
  }

  public abstract class unitSetCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMReport.unitSetCode>
  {
  }

  public abstract class startUnitCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMReport.startUnitCode>
  {
  }

  public abstract class landscape : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMReport.landscape>
  {
  }

  public abstract class applyRestrictionGroups : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReport.applyRestrictionGroups>
  {
  }

  public abstract class paperKind : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMReport.paperKind>
  {
  }

  public abstract class styleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMReport.styleID>
  {
  }

  public abstract class dataSourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMReport.dataSourceID>
  {
  }

  public abstract class sitemapTitle : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMReport.sitemapTitle>
  {
  }

  public abstract class marginLeft : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  RMReport.marginLeft>
  {
  }

  public abstract class marginLeftType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMReport.marginLeftType>
  {
  }

  public abstract class marginRight : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  RMReport.marginRight>
  {
  }

  public abstract class marginRightType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMReport.marginRightType>
  {
  }

  public abstract class marginTop : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  RMReport.marginTop>
  {
  }

  public abstract class marginTopType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMReport.marginTopType>
  {
  }

  public abstract class marginBottom : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  RMReport.marginBottom>
  {
  }

  public abstract class marginBottomType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMReport.marginBottomType>
  {
  }

  public abstract class width : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  RMReport.width>
  {
  }

  public abstract class widthType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMReport.widthType>
  {
  }

  public abstract class height : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  RMReport.height>
  {
  }

  public abstract class heightType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMReport.heightType>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMReport.noteID>
  {
  }

  public class RMReportParameters
  {
    public const string RMReportScreenPrefix = "RM";

    public class rmReportUrl : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      RMReport.RMReportParameters.rmReportUrl>
    {
      public rmReportUrl()
        : base("~/Frames/RmLauncher.aspx?id=")
      {
      }
    }

    public class rmReportPostfix : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      RMReport.RMReportParameters.rmReportPostfix>
    {
      public rmReportPostfix()
        : base(".rpx")
      {
      }
    }

    public class rmReportScreenPrefix : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      RMReport.RMReportParameters.rmReportScreenPrefix>
    {
      public rmReportScreenPrefix()
        : base("RM")
      {
      }
    }
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMReport.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMReport.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMReport.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMReport.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMReport.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMReport.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RMReport.Tstamp>
  {
  }
}
