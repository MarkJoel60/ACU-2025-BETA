// Decompiled with JetBrains decompiler
// Type: PX.CS.CSAttributeDetail
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.CS;

[PXHidden]
[Serializable]
public class CSAttributeDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int ParameterIdLength = 10;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CSAttribute.attributeID))]
  [PXUIField(DisplayName = "Attribute ID")]
  [PXParent(typeof (Select<CSAttribute, Where<CSAttribute.attributeID, Equal<Current<CSAttributeDetail.attributeID>>>>))]
  public virtual string AttributeID { get; set; }

  [PXDBString(10, InputMask = "", IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Value ID")]
  public virtual string ValueID { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Sort Order")]
  public virtual short? SortOrder { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disabled", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual bool? Disabled { get; set; }

  [PXDBBool]
  public virtual bool? DeletedDatabaseRecord { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  public abstract class attributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeDetail.attributeID>
  {
  }

  public abstract class valueID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttributeDetail.valueID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeDetail.description>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CSAttributeDetail.sortOrder>
  {
  }

  public abstract class disabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSAttributeDetail.disabled>
  {
  }

  public abstract class deletedDatabaseRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CSAttributeDetail.deletedDatabaseRecord>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSAttributeDetail.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CSAttributeDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSAttributeDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    CSAttributeDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CSAttributeDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    CSAttributeDetail.lastModifiedDateTime>
  {
  }
}
