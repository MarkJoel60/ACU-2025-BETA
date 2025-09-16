// Decompiled with JetBrains decompiler
// Type: PX.SM.AUStep
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
public class AUStep : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected string _StepID;
  protected string _Description;
  protected string _GraphName;
  protected string _ViewName;
  protected string _TableName;
  protected string _BqlTableName;
  protected string _TimeStampName;
  protected bool? _IsActive;
  protected bool? _IsStart;
  protected short? _FilterCntr;
  protected short? _FieldCntr;
  protected short? _ActionCntr;
  protected string _ActionName;
  protected string _MenuText;
  protected string _FieldTableName;
  protected string _FieldOrigTableName;
  protected string _FieldName;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault]
  [PXUIField(DisplayName = "Screen ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Step ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<AUStep.stepID, Where<AUStep.screenID, Equal<Optional<AUStep.screenID>>>>))]
  public virtual string StepID
  {
    get => this._StepID;
    set => this._StepID = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string GraphName
  {
    get => this._GraphName;
    set => this._GraphName = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string ViewName
  {
    get => this._ViewName;
    set => this._ViewName = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string TableName
  {
    get => this._TableName;
    set => this._TableName = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string BqlTableName
  {
    get => this._BqlTableName;
    set => this._BqlTableName = value;
  }

  [PXDBString(128 /*0x80*/)]
  public virtual string TimeStampName
  {
    get => this._TimeStampName;
    set => this._TimeStampName = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Start Point", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual bool? IsStart
  {
    get => this._IsStart;
    set => this._IsStart = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? FilterCntr
  {
    get => this._FilterCntr;
    set => this._FilterCntr = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? FieldCntr
  {
    get => this._FieldCntr;
    set => this._FieldCntr = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? ActionCntr
  {
    get => this._ActionCntr;
    set => this._ActionCntr = value;
  }

  [PXString]
  public virtual string ActionName
  {
    get => this._ActionName;
    set => this._ActionName = value;
  }

  [PXString(64 /*0x40*/, IsUnicode = true)]
  [PXUnboundDefault("", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string MenuText
  {
    get => this._MenuText;
    set => this._MenuText = value;
  }

  [PXString(128 /*0x80*/)]
  public virtual string FieldTableName
  {
    get => this._FieldTableName;
    set => this._FieldTableName = value;
  }

  [PXString(128 /*0x80*/)]
  public virtual string FieldOrigTableName
  {
    get => this._FieldOrigTableName;
    set => this._FieldOrigTableName = value;
  }

  [PXString(128 /*0x80*/)]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
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

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.screenID>
  {
  }

  public abstract class stepID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.stepID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.description>
  {
  }

  public abstract class graphName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.graphName>
  {
  }

  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.viewName>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.tableName>
  {
  }

  public abstract class bqlTableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.bqlTableName>
  {
  }

  public abstract class timeStampName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.timeStampName>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStep.isActive>
  {
  }

  public abstract class isStart : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStep.isStart>
  {
  }

  public abstract class filterCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUStep.filterCntr>
  {
  }

  public abstract class fieldCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUStep.fieldCntr>
  {
  }

  public abstract class actionCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUStep.actionCntr>
  {
  }

  public abstract class actionName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.actionName>
  {
  }

  public abstract class menuText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.menuText>
  {
  }

  public abstract class fieldTableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.fieldTableName>
  {
  }

  public abstract class fieldOrigTableName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStep.fieldOrigTableName>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStep.fieldName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUStep.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUStep.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStep.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUStep.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUStep.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStep.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUStep.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUStep.tStamp>
  {
  }
}
