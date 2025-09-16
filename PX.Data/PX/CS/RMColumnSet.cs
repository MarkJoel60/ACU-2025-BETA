// Decompiled with JetBrains decompiler
// Type: PX.CS.RMColumnSet
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.CS;

[PXCacheName("Column Set")]
[PXPrimaryGraph(typeof (RMColumnSetMaint))]
public class RMColumnSet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IRMType
{
  protected 
  #nullable disable
  string _ColumnSetCode;
  protected string _Description;
  protected string _Type;
  protected string _LastColumn;
  protected short? _HeaderCntr;
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
  [PXFieldDescription]
  [PXSelector(typeof (RMColumnSet.columnSetCode))]
  [PXReferentialIntegrityCheck]
  public virtual string ColumnSetCode
  {
    get => this._ColumnSetCode;
    set => this._ColumnSetCode = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXFieldDescription]
  [PXDefault]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Type", Required = true, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [RMShiftCodeString(3)]
  [PXDefault("  @")]
  public virtual string LastColumn
  {
    get => this._LastColumn;
    set => this._LastColumn = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? HeaderCntr
  {
    get => this._HeaderCntr;
    set => this._HeaderCntr = value;
  }

  [PXNote]
  [PXSearchable(65535 /*0xFFFF*/, "Column Set: {0}", new System.Type[] {typeof (RMColumnSet.columnSetCode)}, new System.Type[] {typeof (RMColumnSet.columnSetCode), typeof (RMColumnSet.type), typeof (RMColumnSet.description)}, Line1Format = "{0}", Line1Fields = new System.Type[] {typeof (RMColumnSet.description)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (RMColumnSet.type)})]
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

  public string RMType => this.Type;

  public class PK : PrimaryKeyOf<RMColumnSet>.By<RMColumnSet.columnSetCode>
  {
    public static RMColumnSet Find(PXGraph graph, string columnSetCode, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RMColumnSet>.By<RMColumnSet.columnSetCode>.FindBy(graph, (object) columnSetCode, options);
    }
  }

  public abstract class columnSetCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumnSet.columnSetCode>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumnSet.description>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumnSet.type>
  {
  }

  public abstract class lastColumn : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumnSet.lastColumn>
  {
  }

  public abstract class headerCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMColumnSet.headerCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMColumnSet.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMColumnSet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMColumnSet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMColumnSet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RMColumnSet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMColumnSet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMColumnSet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RMColumnSet.Tstamp>
  {
  }
}
