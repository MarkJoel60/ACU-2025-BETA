// Decompiled with JetBrains decompiler
// Type: PX.CS.RMRowSet
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

[PXCacheName("Row Set")]
[PXPrimaryGraph(typeof (RMRowSetMaint))]
public class RMRowSet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IRMType
{
  protected 
  #nullable disable
  string _RowSetCode;
  protected string _Description;
  protected string _Type;
  protected short? _RowCntr;
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
  [PXSelector(typeof (RMRowSet.rowSetCode))]
  [PXReferentialIntegrityCheck]
  public virtual string RowSetCode
  {
    get => this._RowSetCode;
    set => this._RowSetCode = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXFieldDescription]
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

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? RowCntr
  {
    get => this._RowCntr;
    set => this._RowCntr = value;
  }

  [PXNote]
  [PXSearchable(65535 /*0xFFFF*/, "Row Set: {0}", new System.Type[] {typeof (RMRowSet.rowSetCode)}, new System.Type[] {typeof (RMRowSet.rowSetCode), typeof (RMRowSet.type), typeof (RMRowSet.description)}, Line1Format = "{0}", Line1Fields = new System.Type[] {typeof (RMRowSet.description)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (RMRowSet.type)})]
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

  public class PK : PrimaryKeyOf<RMRowSet>.By<RMRowSet.rowSetCode>
  {
    public static RMRowSet Find(PXGraph graph, string rowSetCode, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RMRowSet>.By<RMRowSet.rowSetCode>.FindBy(graph, (object) rowSetCode, options);
    }
  }

  public abstract class rowSetCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRowSet.rowSetCode>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRowSet.description>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMRowSet.type>
  {
  }

  public abstract class rowCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMRowSet.rowCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMRowSet.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMRowSet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMRowSet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMRowSet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMRowSet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMRowSet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMRowSet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RMRowSet.Tstamp>
  {
  }
}
