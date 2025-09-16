// Decompiled with JetBrains decompiler
// Type: PX.CS.CSScreenAttribute
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
public class CSScreenAttribute : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DefaultValue;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CSAttribute.attributeID))]
  [PXUIField(DisplayName = "Attribute ID")]
  [PXParent(typeof (Select<CSAttribute, Where<CSAttribute.attributeID, Equal<Current<CSScreenAttribute.attributeID>>>>))]
  public virtual string AttributeID { get; set; }

  [PXDBString(8, InputMask = "CC.CC.CC.CC", IsKey = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Screen ID")]
  public virtual string ScreenID { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? Column { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? Row { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault("")]
  public virtual string TypeValue { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hidden")]
  public virtual bool? Hidden { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Required")]
  public virtual bool? Required { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Default Value")]
  public virtual string DefaultValue
  {
    get => this._DefaultValue;
    set => this._DefaultValue = value;
  }

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
    CSScreenAttribute.attributeID>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSScreenAttribute.screenID>
  {
  }

  public abstract class column : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CSScreenAttribute.column>
  {
  }

  public abstract class row : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CSScreenAttribute.row>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSScreenAttribute.noteID>
  {
  }

  public abstract class typeValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSScreenAttribute.typeValue>
  {
  }

  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSScreenAttribute.hidden>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSScreenAttribute.required>
  {
  }

  public abstract class defaultValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSScreenAttribute.defaultValue>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CSScreenAttribute.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSScreenAttribute.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSScreenAttribute.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    CSScreenAttribute.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CSScreenAttribute.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSScreenAttribute.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    CSScreenAttribute.lastModifiedDateTime>
  {
  }
}
