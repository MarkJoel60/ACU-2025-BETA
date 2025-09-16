// Decompiled with JetBrains decompiler
// Type: PX.CS.RMColumn
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Reports.ARm;
using System;

#nullable enable
namespace PX.CS;

[PXCacheName("Column")]
public class RMColumn : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ColumnSetCode;
  protected string _ColumnCode;
  protected string _Description;
  protected short? _ColumnType;
  protected string _Formula;
  protected short? _Rounding;
  protected string _Format;
  protected int? _Width;
  protected int? _ExtraSpace;
  protected bool? _SuppressEmpty;
  protected bool? _HideZero;
  protected bool? _SuppressLine;
  protected string _GroupID;
  protected string _UnitGroupID;
  protected short? _PrintControl;
  protected string _VisibleFormula;
  protected bool? _PageBreak;
  protected int? _StyleID;
  protected int? _DataSourceID;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (RMColumnSet.columnSetCode))]
  [PXUIField(Visible = false)]
  [PXForeignReference(typeof (RMColumn.FK.ColumnSet))]
  public virtual string ColumnSetCode
  {
    get => this._ColumnSetCode;
    set => this._ColumnSetCode = value;
  }

  [RMDBShiftCodeString(3, IsKey = true, InputMask = ">aaa")]
  [PXDefault]
  [RMColumnCode]
  [PXUIField(DisplayName = "Code", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  [PXParent(typeof (Select<RMColumnSet, Where<RMColumnSet.columnSetCode, Equal<Current<RMColumn.columnSetCode>>>>))]
  [PXFormula(null, typeof (MaxCalc<RMColumnSet.lastColumn>))]
  [PXReferentialIntegrityCheck]
  public virtual string ColumnCode
  {
    get => this._ColumnCode;
    set => this._ColumnCode = value;
  }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXIntList(typeof (ARmColumnType), new string[] {"GL", "Calc", "Descr"})]
  [PXUIField(DisplayName = "Type")]
  public virtual short? ColumnType
  {
    get => this._ColumnType;
    set => this._ColumnType = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXIntList(typeof (ARmCellEvalOrder), new string[] {"Row", "Column"})]
  [PXUIField(DisplayName = "Cell Evaluation Order")]
  public virtual short? CellEvalOrder { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  [PXIntList(typeof (ARmCellFormatOrder), new string[] {"Row", "Column"})]
  [PXUIField(DisplayName = "Cell Format Order")]
  public virtual short? CellFormatOrder { get; set; }

  [PXDBLocalizableString(4000, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string Formula
  {
    get => this._Formula;
    set => this._Formula = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXIntList(typeof (ARmColumnRounding), new string[] {"No Rounding", "Whole Dollars", "Thousands", "Whole Thousands", "Millions", "Whole Millions", "Billions", "Whole Billions"})]
  [PXUIField(DisplayName = "Rounding")]
  public virtual short? Rounding
  {
    get => this._Rounding;
    set => this._Rounding = value;
  }

  [PXDBString(4000, IsUnicode = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField]
  public virtual string Format
  {
    get => this._Format;
    set => this._Format = value;
  }

  [PXDBInt]
  [PXDefault(70)]
  [PXUIField(DisplayName = "Width")]
  public virtual int? Width
  {
    get => this._Width;
    set => this._Width = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Auto Height")]
  public virtual bool? AutoHeight { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Extra Space")]
  public virtual int? ExtraSpace
  {
    get => this._ExtraSpace;
    set => this._ExtraSpace = value;
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

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Suppress Line")]
  public virtual bool? SuppressLine
  {
    get => this._SuppressLine;
    set => this._SuppressLine = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Printing Group")]
  public virtual string GroupID
  {
    get => this._GroupID;
    set => this._GroupID = value;
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
  [PXIntList(typeof (ColPrintControl), new string[] {"Print", "Hidden", "Merge Next"})]
  public virtual short? PrintControl
  {
    get => this._PrintControl;
    set => this._PrintControl = value;
  }

  [PXDBString(4000, IsUnicode = true)]
  [PXUIField(DisplayName = "Visible Formula")]
  public virtual string VisibleFormula
  {
    get => this._VisibleFormula;
    set => this._VisibleFormula = value;
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
  [PXForeignReference(typeof (RMColumn.FK.DataSource))]
  public virtual int? DataSourceID
  {
    get => this._DataSourceID;
    set => this._DataSourceID = value;
  }

  [RMColumnPreview]
  [PXUIField(DisplayName = "Data Summary", Enabled = false, Visible = false)]
  public virtual string Preview { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBCreatedByID]
  [PXUIField(Visible = false)]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  [PXUIField(Visible = false)]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  [PXUIField(Visible = false)]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  [PXUIField(Visible = false)]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  [PXUIField(Visible = false)]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  [PXUIField(Visible = false)]
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

  public class PK : PrimaryKeyOf<RMColumn>.By<RMColumn.columnSetCode, RMColumn.columnCode>
  {
    public static RMColumn Find(
      PXGraph graph,
      string columnSetCode,
      string columnCode,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RMColumn>.By<RMColumn.columnSetCode, RMColumn.columnCode>.FindBy(graph, (object) columnSetCode, (object) columnCode, options);
    }
  }

  public static class FK
  {
    public class DataSource : 
      PrimaryKeyOf<RMDataSource>.By<RMDataSource.dataSourceID>.ForeignKeyOf<RMColumn>.By<RMColumn.dataSourceID>
    {
    }

    public class ColumnSet : 
      PrimaryKeyOf<RMColumnSet>.By<RMColumnSet.columnSetCode>.ForeignKeyOf<RMColumn>.By<RMColumn.columnSetCode>
    {
    }
  }

  public abstract class columnSetCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumn.columnSetCode>
  {
  }

  public abstract class columnCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumn.columnCode>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumn.description>
  {
  }

  public abstract class columnType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMColumn.columnType>
  {
  }

  public abstract class cellEvalOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMColumn.cellEvalOrder>
  {
  }

  public abstract class cellFormatOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMColumn.cellFormatOrder>
  {
  }

  public abstract class formula : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumn.formula>
  {
  }

  public abstract class rounding : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMColumn.rounding>
  {
  }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumn.format>
  {
  }

  public abstract class width : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMColumn.width>
  {
  }

  public abstract class autoHeight : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMColumn.autoHeight>
  {
  }

  public abstract class extraSpace : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMColumn.extraSpace>
  {
  }

  public abstract class suppressEmpty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMColumn.suppressEmpty>
  {
  }

  public abstract class hideZero : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMColumn.hideZero>
  {
  }

  public abstract class suppressLine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMColumn.suppressLine>
  {
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumn.groupID>
  {
  }

  public abstract class unitGroupID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumn.unitGroupID>
  {
  }

  public abstract class printControl : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMColumn.printControl>
  {
  }

  public abstract class visibleFormula : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumn.visibleFormula>
  {
  }

  public abstract class pageBreak : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMColumn.pageBreak>
  {
  }

  public abstract class styleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMColumn.styleID>
  {
  }

  public abstract class dataSourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMColumn.dataSourceID>
  {
  }

  public abstract class preview : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumn.preview>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMColumn.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMColumn.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMColumn.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMColumn.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMColumn.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMColumn.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMColumn.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RMColumn.Tstamp>
  {
  }
}
