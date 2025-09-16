// Decompiled with JetBrains decompiler
// Type: PX.SM.AUStepAction
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
public class AUStepAction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected string _StepID;
  protected short? _RowNbr;
  protected bool? _IsActive;
  protected string _ActionName;
  protected bool? _IsAutomatic;
  protected bool? _IsDefault;
  protected bool? _IsDisabled;
  protected bool? _BatchMode;
  protected string _MenuText;
  protected short? _AutoSave;
  protected string _MenuIcon;
  protected string _ProcessingScreenID;
  protected string _ProcessingGraphName;
  protected bool? _SplitByValues;
  protected bool? _IsRetryActive;
  protected string _RetryScreenID;
  protected string _RetryStepID;
  protected string _RetryActionName;
  protected short? _RetryCntr;
  protected bool? _IsSuccessActive;
  protected string _SuccessScreenID;
  protected string _SuccessStepID;
  protected string _SuccessActionName;
  protected bool? _IsFailActive;
  protected string _FailScreenID;
  protected string _FailStepID;
  protected string _FailActionName;
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
  [PXLineNbr(typeof (AUStep.actionCntr))]
  [PXParent(typeof (Select<AUStep, Where<AUStep.screenID, Equal<Current<AUStepAction.screenID>>, And<AUStep.stepID, Equal<Current<AUStepAction.stepID>>>>>))]
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
  [PXUIField(DisplayName = "Action Name", Required = true)]
  [PXStringList(new string[] {null}, new string[] {""})]
  [PXDefault]
  public virtual string ActionName
  {
    get => this._ActionName;
    set => this._ActionName = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Run Auto")]
  public virtual bool? IsAutomatic
  {
    get => this._IsAutomatic;
    set => this._IsAutomatic = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Default")]
  public virtual bool? IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
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
  [PXUIField(DisplayName = "Batch Mode")]
  public virtual bool? BatchMode
  {
    get => this._BatchMode;
    set => this._BatchMode = value;
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Menu Text")]
  [PXDefault]
  public virtual string MenuText
  {
    get => this._MenuText;
    set => this._MenuText = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXIntList("0;Never,1;Before Run Auto,2;After Run Auto,3;Before Any Run,4;After Any Run")]
  [PXUIField(DisplayName = "Save Auto")]
  public virtual short? AutoSave
  {
    get => this._AutoSave;
    set => this._AutoSave = value;
  }

  [PXString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Menu Icon")]
  [PXIconsList]
  public virtual string MenuIcon
  {
    get => this._MenuIcon;
    set => this._MenuIcon = value;
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Screen ID")]
  public virtual string ProcessingScreenID
  {
    get => this._ProcessingScreenID;
    set => this._ProcessingScreenID = value;
  }

  [PXDBString(128 /*0x80*/)]
  public virtual string ProcessingGraphName
  {
    get => this._ProcessingGraphName;
    set => this._ProcessingGraphName = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Split by Values")]
  public virtual bool? SplitByValues
  {
    get => this._SplitByValues;
    set => this._SplitByValues = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsRetryActive
  {
    get => this._IsRetryActive;
    set => this._IsRetryActive = value;
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXDefault(typeof (AUStep.screenID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Screen ID")]
  public virtual string RetryScreenID
  {
    get => this._RetryScreenID;
    set => this._RetryScreenID = value;
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Step ID")]
  [PXDefault(typeof (AUStep.stepID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXSelector(typeof (Search<AUStep.stepID, Where<AUStep.screenID, Equal<Optional<AUStepAction.retryScreenID>>>>))]
  public virtual string RetryStepID
  {
    get => this._RetryStepID;
    set => this._RetryStepID = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Action Name")]
  [PXStringList(new string[] {null}, new string[] {""})]
  public virtual string RetryActionName
  {
    get => this._RetryActionName;
    set => this._RetryActionName = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Count")]
  public virtual short? RetryCntr
  {
    get => this._RetryCntr;
    set => this._RetryCntr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsSuccessActive
  {
    get => this._IsSuccessActive;
    set => this._IsSuccessActive = value;
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXDefault(typeof (AUStep.screenID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Screen ID")]
  public virtual string SuccessScreenID
  {
    get => this._SuccessScreenID;
    set => this._SuccessScreenID = value;
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Step ID")]
  [PXSelector(typeof (Search<AUStep.stepID, Where<AUStep.screenID, Equal<Optional<AUStepAction.successScreenID>>>>))]
  [PXDefault(typeof (AUStep.stepID), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string SuccessStepID
  {
    get => this._SuccessStepID;
    set => this._SuccessStepID = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Action Name")]
  [PXStringList(new string[] {null}, new string[] {""})]
  public virtual string SuccessActionName
  {
    get => this._SuccessActionName;
    set => this._SuccessActionName = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsFailActive
  {
    get => this._IsFailActive;
    set => this._IsFailActive = value;
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXDefault(typeof (AUStep.screenID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Screen ID")]
  public virtual string FailScreenID
  {
    get => this._FailScreenID;
    set => this._FailScreenID = value;
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Step ID")]
  [PXSelector(typeof (Search<AUStep.stepID, Where<AUStep.screenID, Equal<Optional<AUStepAction.failScreenID>>>>))]
  [PXDefault(typeof (AUStep.stepID), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string FailStepID
  {
    get => this._FailStepID;
    set => this._FailStepID = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Action Name")]
  [PXStringList(new string[] {null}, new string[] {""})]
  public virtual string FailActionName
  {
    get => this._FailActionName;
    set => this._FailActionName = value;
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
  AUStepAction.screenID>
  {
  }

  public abstract class stepID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepAction.stepID>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUStepAction.rowNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepAction.isActive>
  {
  }

  public abstract class actionName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepAction.actionName>
  {
  }

  public abstract class isAutomatic : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepAction.isAutomatic>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepAction.isDefault>
  {
  }

  public abstract class isDisabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepAction.isDisabled>
  {
  }

  public abstract class batchMode : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepAction.batchMode>
  {
  }

  public abstract class menuText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepAction.menuText>
  {
  }

  public abstract class autoSave : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUStepAction.autoSave>
  {
  }

  public abstract class menuIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepAction.menuIcon>
  {
  }

  public abstract class processingScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepAction.processingScreenID>
  {
  }

  public abstract class processingGraphName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepAction.processingGraphName>
  {
  }

  public abstract class splitByValues : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepAction.splitByValues>
  {
  }

  public abstract class isRetryActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepAction.isRetryActive>
  {
  }

  public abstract class retryScreenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepAction.retryScreenID>
  {
  }

  public abstract class retryStepID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepAction.retryStepID>
  {
  }

  public abstract class retryActionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepAction.retryActionName>
  {
  }

  public abstract class retryCnt : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUStepAction.retryCnt>
  {
  }

  public abstract class isSuccessActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepAction.isSuccessActive>
  {
  }

  public abstract class successScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepAction.successScreenID>
  {
  }

  public abstract class successStepID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepAction.successStepID>
  {
  }

  public abstract class successActionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepAction.successActionName>
  {
  }

  public abstract class isFailActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepAction.isFailActive>
  {
  }

  public abstract class failScreenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepAction.failScreenID>
  {
  }

  public abstract class failStepID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepAction.failStepID>
  {
  }

  public abstract class failActionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepAction.failActionName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUStepAction.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUStepAction.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepAction.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUStepAction.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUStepAction.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepAction.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUStepAction.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUStepAction.tStamp>
  {
  }
}
