// Decompiled with JetBrains decompiler
// Type: PX.SM.AUStepFilter
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
public class AUStepFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected string _StepID;
  protected short? _RowNbr;
  protected bool? _IsActive;
  protected int? _OpenBrackets;
  protected string _FieldName;
  protected int? _Condition;
  protected bool? _IsRelative;
  protected string _Value;
  protected string _Value2;
  protected int? _CloseBrackets;
  protected int? _Operator;
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
  [PXLineNbr(typeof (AUStep.filterCntr))]
  [PXParent(typeof (Select<AUStep, Where<AUStep.screenID, Equal<Current<AUStepFilter.screenID>>, And<AUStep.stepID, Equal<Current<AUStepFilter.stepID>>>>>))]
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

  [PXDBInt]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5, 6, 7}, new string[] {"-", "(", "((", "(((", "((((", "(((((", "Activity Exists (", "Activity Not Exists ("})]
  [PXUIField(DisplayName = "Brackets")]
  [PXDefault(0)]
  public virtual int? OpenBrackets
  {
    get => this._OpenBrackets;
    set => this._OpenBrackets = value;
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

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Condition", Required = true)]
  [AUConditionType]
  public virtual int? Condition
  {
    get => this._Condition;
    set => this._Condition = value;
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

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value 2")]
  public virtual string Value2
  {
    get => this._Value2;
    set => this._Value2 = value;
  }

  [PXDBInt]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5}, new string[] {"-", ")", "))", ")))", "))))", ")))))"})]
  [PXUIField(DisplayName = "Brackets")]
  [PXDefault(0)]
  public virtual int? CloseBrackets
  {
    get => this._CloseBrackets;
    set => this._CloseBrackets = value;
  }

  [PXDBInt]
  [PXIntList(new int[] {0, 1}, new string[] {"And", "Or"})]
  [PXUIField(FieldName = "Operator", DisplayName = "Operator")]
  [PXDefault(0)]
  public virtual int? Operator
  {
    get => this._Operator;
    set => this._Operator = value;
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
  AUStepFilter.screenID>
  {
  }

  public abstract class stepID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepFilter.stepID>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUStepFilter.rowNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepFilter.isActive>
  {
  }

  public abstract class openBrackets : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUStepFilter.openBrackets>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepFilter.fieldName>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUStepFilter.condition>
  {
  }

  public abstract class isRelative : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUStepFilter.isRelative>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepFilter.value>
  {
  }

  public abstract class value2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUStepFilter.value2>
  {
  }

  public abstract class closeBrackets : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUStepFilter.closeBrackets>
  {
  }

  public abstract class operatoR : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUStepFilter.operatoR>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUStepFilter.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUStepFilter.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepFilter.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUStepFilter.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUStepFilter.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUStepFilter.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUStepFilter.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUStepFilter.tStamp>
  {
  }
}
