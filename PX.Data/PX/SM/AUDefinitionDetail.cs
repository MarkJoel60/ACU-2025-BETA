// Decompiled with JetBrains decompiler
// Type: PX.SM.AUDefinitionDetail
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
public class AUDefinitionDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DefinitionID;
  protected string _ScreenID;
  protected string _TableName;
  protected string _Steps;
  protected string _Content;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true)]
  [PXDefault(typeof (AUDefinition.definitionID))]
  public virtual string DefinitionID
  {
    get => this._DefinitionID;
    set => this._DefinitionID = value;
  }

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault]
  [PXUIField(DisplayName = "Screen ID")]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBString(255 /*0xFF*/)]
  public virtual string TableName
  {
    get => this._TableName;
    set => this._TableName = value;
  }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Steps", Enabled = false)]
  public virtual string Steps
  {
    get => this._Steps;
    set => this._Steps = value;
  }

  [PXDBText(IsUnicode = true)]
  public virtual string Content
  {
    get => this._Content;
    set => this._Content = value;
  }

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
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  public abstract class definitionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUDefinitionDetail.definitionID>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUDefinitionDetail.screenID>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUDefinitionDetail.tableName>
  {
  }

  public abstract class steps : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUDefinitionDetail.steps>
  {
  }

  public abstract class content : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUDefinitionDetail.content>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUDefinitionDetail.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUDefinitionDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUDefinitionDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUDefinitionDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUDefinitionDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUDefinitionDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUDefinitionDetail.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUDefinitionDetail.tStamp>
  {
  }
}
