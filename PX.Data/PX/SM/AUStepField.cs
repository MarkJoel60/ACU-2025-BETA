// Decompiled with JetBrains decompiler
// Type: PX.SM.AUStepField
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
public class AUStepField : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected string _StepID;
  protected short? _RowNbr;
  protected bool? _IsActive;
  protected string _TableName;
  protected string _FieldName;
  protected bool? _UseSavedState;
  protected bool? _IsDisabled;
  protected bool? _IsInvisible;
  protected bool? _IsRelative;
  protected string _MinValue;
  protected string _MaxValue;
  protected string _DefaultValue;
  protected string _InputMask;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault(typeof (AUStep.screenID))]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true)]
  [PXDefault(typeof (AUStep.stepID))]
  public virtual string StepID
  {
    get => this._StepID;
    set => this._StepID = value;
  }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (AUStep.fieldCntr))]
  [PXParent(typeof (Select<AUStep, Where<AUStep.screenID, Equal<Current<AUStepField.screenID>>, And<AUStep.stepID, Equal<Current<AUStepField.stepID>>>>>))]
  public virtual short? RowNbr
  {
    get => this._RowNbr;
    set => this._RowNbr = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Table Name", Required = true)]
  [PXStringList(new string[] {null}, new string[] {""})]
  public virtual string TableName
  {
    get => this._TableName;
    set => this._TableName = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault("<TABLE>")]
  [PXUIField(DisplayName = "Field Name", Required = true)]
  [PXStringList(new string[] {"<TABLE>"}, new string[] {"<TABLE>"}, ExclusiveValues = false)]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Saved State")]
  public virtual bool? UseSavedState
  {
    get => this._UseSavedState;
    set => this._UseSavedState = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disable")]
  public virtual bool? IsDisabled
  {
    get => this._IsDisabled;
    set => this._IsDisabled = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hidden")]
  public virtual bool? IsInvisible
  {
    get => this._IsInvisible;
    set => this._IsInvisible = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Relative")]
  public virtual bool? IsRelative
  {
    get => this._IsRelative;
    set => this._IsRelative = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Min Value")]
  public virtual string MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Max Value")]
  public virtual string MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Default Value")]
  public virtual string DefaultValue
  {
    get => this._DefaultValue;
    set => this._DefaultValue = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Input Mask")]
  public virtual string InputMask
  {
    get => this._InputMask;
    set => this._InputMask = value;
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
  AUStepField.screenID>
  {
  }

  public abstract class stepID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepField.stepID>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUStepField.rowNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepField.isActive>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepField.tableName>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepField.fieldName>
  {
    public const string Table = "<TABLE>";
    public const string AllFields = "*";
  }

  public abstract class useSavedState : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepField.useSavedState>
  {
  }

  public abstract class isDisabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepField.isDisabled>
  {
  }

  public abstract class isInvisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepField.isInvisible>
  {
  }

  public abstract class isRelative : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepField.isRelative>
  {
  }

  public abstract class minValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepField.minValue>
  {
  }

  public abstract class maxValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepField.maxValue>
  {
  }

  public abstract class defaultValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepField.defaultValue>
  {
  }

  public abstract class inputMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepField.inputMask>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUStepField.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUStepField.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepField.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUStepField.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUStepField.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepField.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUStepField.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUStepField.tStamp>
  {
  }
}
