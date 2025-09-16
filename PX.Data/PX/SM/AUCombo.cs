// Decompiled with JetBrains decompiler
// Type: PX.SM.AUCombo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AUCombo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TableName;
  protected string _FieldName;
  protected bool? _IsExplicit;
  protected string _Value;
  protected string _Description;
  protected short? _RefCntr;
  protected short? _RowNbr;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault(typeof (AUStep.fieldTableName))]
  public virtual string TableName
  {
    get => this._TableName;
    set => this._TableName = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault(typeof (AUStep.fieldName))]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Explicit")]
  public virtual bool? IsExplicit
  {
    get => this._IsExplicit;
    set => this._IsExplicit = value;
  }

  [PXDBString(10, IsKey = true)]
  [PXDefault]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? RefCntr
  {
    get => this._RefCntr;
    set => this._RefCntr = value;
  }

  [PXDBShort]
  [PXDefault]
  public virtual short? RowNbr
  {
    get => this._RowNbr;
    set => this._RowNbr = value;
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
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUCombo.tableName>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUCombo.fieldName>
  {
  }

  public abstract class isExplicit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUCombo.isExplicit>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUCombo.value>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUCombo.description>
  {
  }

  public abstract class refCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUCombo.refCntr>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUCombo.rowNbr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUCombo.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUCombo.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUCombo.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUCombo.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUCombo.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUCombo.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUCombo.tStamp>
  {
  }
}
