// Decompiled with JetBrains decompiler
// Type: PX.CS.RMColumnHeader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.CS;

[Serializable]
public class RMColumnHeader : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ColumnSetCode;
  protected string _ColumnCode;
  protected short? _HeaderNbr;
  protected string _Formula;
  protected string _StartColumn;
  protected string _EndColumn;
  protected int? _Height;
  protected string _GroupID;
  protected int? _StyleID;
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
  [PXUIField(Visible = false, Enabled = false)]
  public virtual string ColumnSetCode
  {
    get => this._ColumnSetCode;
    set => this._ColumnSetCode = value;
  }

  [RMDBShiftCodeString(3, IsKey = true, InputMask = ">aaa")]
  [PXDefault]
  [PXParent(typeof (Select<RMColumnSet, Where<RMColumnSet.columnSetCode, Equal<Current<RMColumnHeader.columnSetCode>>>>))]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual string ColumnCode
  {
    get => this._ColumnCode;
    set => this._ColumnCode = value;
  }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual short? HeaderNbr
  {
    get => this._HeaderNbr;
    set => this._HeaderNbr = value;
  }

  [PXDBLocalizableString(4000, IsUnicode = true)]
  [PXUIField(Visible = true, Enabled = true)]
  public virtual string Formula
  {
    get => this._Formula;
    set => this._Formula = value;
  }

  [RMDBShiftCodeString(3, InputMask = ">aaa")]
  [PXDefault]
  [PXUIField(DisplayName = "Column Range", Visible = true)]
  public virtual string StartColumn
  {
    get => this._StartColumn;
    set => this._StartColumn = value;
  }

  [RMDBShiftCodeString(3, InputMask = ">aaa")]
  [PXDefault]
  [PXUIField(DisplayName = "End Column", Visible = true)]
  public virtual string EndColumn
  {
    get => this._EndColumn;
    set => this._EndColumn = value;
  }

  [PXDBInt]
  [PXDefault(16 /*0x10*/)]
  [PXUIField(DisplayName = "Height")]
  public virtual int? Height
  {
    get => this._Height;
    set => this._Height = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Printing Group")]
  public virtual string GroupID
  {
    get => this._GroupID;
    set => this._GroupID = value;
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (RMColumnSetMaint.RMHeaderStyle.styleID))]
  [PXUIField(Visible = false)]
  public virtual int? StyleID
  {
    get => this._StyleID;
    set => this._StyleID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "", Visible = false)]
  public bool? IsRowSet { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Section Type", Visible = true)]
  public string SectionType { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBCreatedByID]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  [PXUIField(Visible = false, Enabled = false)]
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

  public abstract class columnSetCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMColumnHeader.columnSetCode>
  {
  }

  public abstract class columnCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumnHeader.columnCode>
  {
  }

  public abstract class headerNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMColumnHeader.headerNbr>
  {
  }

  public abstract class formula : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumnHeader.formula>
  {
  }

  public abstract class startColumn : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumnHeader.startColumn>
  {
  }

  public abstract class endColumn : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumnHeader.endColumn>
  {
  }

  public abstract class height : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMColumnHeader.height>
  {
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumnHeader.groupID>
  {
  }

  public abstract class styleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMColumnHeader.styleID>
  {
  }

  public abstract class isRowSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMColumnHeader.isRowSet>
  {
  }

  public abstract class sectionType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumnHeader.sectionType>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMColumnHeader.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMColumnHeader.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMColumnHeader.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMColumnHeader.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RMColumnHeader.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMColumnHeader.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMColumnHeader.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RMColumnHeader.Tstamp>
  {
  }
}
