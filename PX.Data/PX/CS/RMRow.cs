// Decompiled with JetBrains decompiler
// Type: PX.CS.RMRow
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.CS;

[PXCacheName("Row")]
public class RMRow : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IRMType, ISortOrder
{
  protected 
  #nullable disable
  string _RowSetCode;
  protected short? _RowNbr;
  protected string _RowCode;
  protected string _Description;
  protected short? _RowType;
  protected string _Formula;
  protected string _Format;
  protected bool? _SuppressEmpty;
  protected bool? _HideZero;
  protected int? _Height;
  protected int? _Indent;
  protected short? _LineStyle;
  protected string _LinkedRowCode;
  protected string _BaseRowCode;
  protected string _ColumnGroupID;
  protected string _UnitGroupID;
  protected short? _PrintControl;
  protected bool? _PageBreak;
  protected int? _StyleID;
  protected int? _DataSourceID;
  protected string _RMType;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (RMRowSet.rowSetCode))]
  [PXForeignReference(typeof (RMRow.FK.RowSet))]
  public virtual string RowSetCode
  {
    get => this._RowSetCode;
    set => this._RowSetCode = value;
  }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [RMRowNbr]
  [PXParent(typeof (Select<RMRowSet, Where<RMRowSet.rowSetCode, Equal<Current<RMRow.rowSetCode>>>>))]
  public virtual short? RowNbr
  {
    get => this._RowNbr;
    set => this._RowNbr = value;
  }

  [PX.Data.Reports.RowCode]
  [PXDefault]
  [PXUIField(DisplayName = "Code")]
  public virtual string RowCode
  {
    get => this._RowCode;
    set => this._RowCode = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Row", Enabled = false)]
  public virtual string RowCodeRO => this.RowCode;

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXIntList("0;GL,1;Caption,2;Line,3;Total,4;Header,5;Sort")]
  [PXUIField(DisplayName = "Type")]
  public virtual short? RowType
  {
    get => this._RowType;
    set => this._RowType = value;
  }

  [PXDBLocalizableString(4000, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string Formula
  {
    get => this._Formula;
    set => this._Formula = value;
  }

  [PXDBString(4000, IsUnicode = true)]
  [PXUIField(DisplayName = "Format")]
  public virtual string Format
  {
    get => this._Format;
    set => this._Format = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Suppress Empty")]
  public virtual bool? SuppressEmpty
  {
    get => this._SuppressEmpty;
    set => this._SuppressEmpty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hide Zero")]
  public virtual bool? HideZero
  {
    get => this._HideZero;
    set => this._HideZero = value;
  }

  [PXDBInt(MinValue = 0, MaxValue = 500)]
  [PXDefault(16 /*0x10*/)]
  [PXUIField(DisplayName = "Height")]
  public virtual int? Height
  {
    get => this._Height;
    set => this._Height = value;
  }

  [PXDBInt(MinValue = 0, MaxValue = 200)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Indent")]
  public virtual int? Indent
  {
    get => this._Indent;
    set => this._Indent = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXIntList("0;NotSet,1;None,2;Dotted,3;Dashed,4;Solid,5;Double,6;Groove,7;Ridge,8;Inset,9;Outset")]
  [PXUIField(DisplayName = "Line Style")]
  public virtual short? LineStyle
  {
    get => this._LineStyle;
    set => this._LineStyle = value;
  }

  [PX.Data.Reports.RowCode]
  [PXUIField(DisplayName = "Linked Row")]
  public virtual string LinkedRowCode
  {
    get => this._LinkedRowCode;
    set => this._LinkedRowCode = value;
  }

  [PX.Data.Reports.RowCode]
  [PXUIField(DisplayName = "Base Row")]
  public virtual string BaseRowCode
  {
    get => this._BaseRowCode;
    set => this._BaseRowCode = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Column Group")]
  public virtual string ColumnGroupID
  {
    get => this._ColumnGroupID;
    set => this._ColumnGroupID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Unit Group")]
  public virtual string UnitGroupID
  {
    get => this._UnitGroupID;
    set => this._UnitGroupID = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Printing Control")]
  [PXDefault(0)]
  [PXIntList("0;Line Break,1;Hidden,2;Merge Next,3;Start Box,4;End Box")]
  public virtual short? PrintControl
  {
    get => this._PrintControl;
    set => this._PrintControl = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Page Break")]
  public virtual bool? PageBreak
  {
    get => this._PageBreak;
    set => this._PageBreak = value;
  }

  [RMStyle]
  public virtual int? StyleID
  {
    get => this._StyleID;
    set => this._StyleID = value;
  }

  [RMDataSource]
  [PXForeignReference(typeof (RMRow.FK.DataSource))]
  public virtual int? DataSourceID
  {
    get => this._DataSourceID;
    set => this._DataSourceID = value;
  }

  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "RMType")]
  public virtual string RMType
  {
    get => this._RMType;
    set => this._RMType = value;
  }

  [RMRowPreview]
  [PXUIField(DisplayName = "Data Summary", Enabled = false)]
  public virtual string Preview { get; set; }

  public virtual int? SortOrder
  {
    get => !string.IsNullOrEmpty(this.RowCode) ? new int?(int.Parse(this.RowCode)) : new int?();
    set
    {
      string rowCode = this.RowCode;
      int num = rowCode != null ? rowCode.Length : (value.HasValue ? value.GetValueOrDefault().ToString().Length : 0);
      this.RowCode = value?.ToString($"D{num}") ?? (string) null;
    }
  }

  [PXInt]
  [PXUIField(DisplayName = "", Visible = false)]
  [PXDependsOnFields(new System.Type[] {typeof (RMRow.rowNbr)})]
  public int? LineNbr
  {
    get
    {
      short? rowNbr = this.RowNbr;
      return !rowNbr.HasValue ? new int?() : new int?((int) rowNbr.GetValueOrDefault());
    }
    set
    {
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "", Visible = false)]
  public bool? InCycle { get; set; }

  [PXBool]
  public bool? DataVisible { get; set; }

  [PXBool]
  public bool? DataSourceVisible { get; set; }

  [PXNote]
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

  public class PK : PrimaryKeyOf<RMRow>.By<RMRow.rowSetCode, RMRow.rowNbr>
  {
    public static RMRow Find(
      PXGraph graph,
      string rowSetCode,
      short? rowNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RMRow>.By<RMRow.rowSetCode, RMRow.rowNbr>.FindBy(graph, (object) rowSetCode, (object) rowNbr, options);
    }
  }

  public static class FK
  {
    public class DataSource : 
      PrimaryKeyOf<RMDataSource>.By<RMDataSource.dataSourceID>.ForeignKeyOf<RMRow>.By<RMRow.dataSourceID>
    {
    }

    public class RowSet : 
      PrimaryKeyOf<RMRowSet>.By<RMRowSet.rowSetCode>.ForeignKeyOf<RMRow>.By<RMRow.rowSetCode>
    {
    }
  }

  public abstract class rowSetCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.rowSetCode>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMRow.rowNbr>
  {
  }

  public abstract class rowCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.rowCode>
  {
  }

  public abstract class rowCodeRO : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.rowCodeRO>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.description>
  {
  }

  public abstract class rowType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMRow.rowType>
  {
  }

  public abstract class formula : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.formula>
  {
  }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.format>
  {
  }

  public abstract class suppressEmpty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMRow.suppressEmpty>
  {
  }

  public abstract class hideZero : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMRow.hideZero>
  {
  }

  public abstract class height : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMRow.height>
  {
  }

  public abstract class indent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMRow.indent>
  {
  }

  public abstract class lineStyle : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMRow.lineStyle>
  {
  }

  public abstract class linkedRowCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.linkedRowCode>
  {
  }

  public abstract class baseRowCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.baseRowCode>
  {
  }

  public abstract class columnGroupID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.columnGroupID>
  {
  }

  public abstract class unitGroupID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.unitGroupID>
  {
  }

  public abstract class printControl : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMRow.printControl>
  {
  }

  public abstract class pageBreak : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMRow.pageBreak>
  {
  }

  public abstract class styleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMRow.styleID>
  {
  }

  public abstract class dataSourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMRow.dataSourceID>
  {
  }

  public abstract class rMType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.rMType>
  {
  }

  public abstract class preview : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRow.preview>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMRow.lineNbr>
  {
  }

  public abstract class inCycle : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMRow.inCycle>
  {
  }

  public abstract class dataVisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMRow.dataVisible>
  {
  }

  public abstract class dataSourceVisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMRow.dataSourceVisible>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMRow.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMRow.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMRow.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMRow.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMRow.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMRow.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMRow.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RMRow.Tstamp>
  {
  }
}
