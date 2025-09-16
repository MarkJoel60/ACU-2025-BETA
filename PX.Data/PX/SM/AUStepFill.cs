// Decompiled with JetBrains decompiler
// Type: PX.SM.AUStepFill
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
public class AUStepFill : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected string _StepID;
  protected string _ActionName;
  protected string _MenuText;
  protected short? _RowNbr;
  protected bool? _IsActive;
  protected string _FieldName;
  protected bool? _IsRelative;
  protected string _Value;
  protected bool? _IsDelayed;
  protected bool? _IgnoreError;
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

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault(typeof (AUStep.actionName))]
  public virtual string ActionName
  {
    get => this._ActionName;
    set => this._ActionName = value;
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (AUStep.menuText))]
  public virtual string MenuText
  {
    get => this._MenuText;
    set => this._MenuText = value;
  }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (Select<AUStep, Where<AUStep.screenID, Equal<Current<AUStepFill.screenID>>, And<AUStep.stepID, Equal<Current<AUStepFill.stepID>>>>>), LeaveChildren = true)]
  [PXLineNbr(typeof (AUStep))]
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
  [PXUIField(DisplayName = "Field Name", Required = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
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
  [PXUIField(DisplayName = "Value")]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Delayed")]
  public virtual bool? IsDelayed
  {
    get => this._IsDelayed;
    set => this._IsDelayed = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ignore Error")]
  public virtual bool? IgnoreError
  {
    get => this._IgnoreError;
    set => this._IgnoreError = value;
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
  AUStepFill.screenID>
  {
  }

  public abstract class stepID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepFill.stepID>
  {
  }

  public abstract class actionName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepFill.actionName>
  {
  }

  public abstract class menuText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepFill.menuText>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUStepFill.rowNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepFill.isActive>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepFill.fieldName>
  {
  }

  public abstract class isRelative : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepFill.isRelative>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepFill.value>
  {
  }

  public abstract class isDelayed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepFill.isDelayed>
  {
  }

  public abstract class ignoreError : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepFill.ignoreError>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUStepFill.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepFill.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUStepFill.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUStepFill.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepFill.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUStepFill.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUStepFill.tStamp>
  {
  }
}
