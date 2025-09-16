// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenConditionFilter
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
public class AUScreenConditionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected Guid? _ProjectID;
  protected string _ConditionID;
  protected int? _RowNbr;
  protected string _EventStateType;
  protected bool? _IsActive;
  protected int? _OpenBrackets;
  protected string _FieldName;
  protected int? _Condition;
  protected string _Value;
  protected string _Value2;
  protected int? _CloseBrackets;
  protected int? _Operator;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault(typeof (AUScreenDefinition.screenID))]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (AUScreenDefinition.projectID))]
  public virtual Guid? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (AUScreenCondition.itemCD))]
  [PXParent(typeof (Select<AUScreenCondition, Where<AUScreenCondition.screenID, Equal<Current<AUScreenConditionFilter.screenID>>, And<AUScreenCondition.projectID, Equal<Current<AUScreenConditionFilter.projectID>>, And<AUScreenCondition.itemCD, Equal<Current<AUScreenConditionFilter.conditionID>>>>>>))]
  public virtual string ConditionID
  {
    get => this._ConditionID;
    set => this._ConditionID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (AUScreenCondition.childRowCntr))]
  public virtual int? RowNbr
  {
    get => this._RowNbr;
    set => this._RowNbr = value;
  }

  [PXDBString(1)]
  public virtual string EventStateType
  {
    get => this._EventStateType;
    set => this._EventStateType = value;
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
  [PXUIField(FieldName = "Operator")]
  [PXDefault(0)]
  public virtual int? Operator
  {
    get => this._Operator;
    set => this._Operator = value;
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

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionFilter.screenID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenConditionFilter.projectID>
  {
  }

  public abstract class conditionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionFilter.conditionID>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenConditionFilter.rowNbr>
  {
  }

  public abstract class eventStateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionFilter.eventStateType>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenConditionFilter.isActive>
  {
  }

  public abstract class openBrackets : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenConditionFilter.openBrackets>
  {
  }

  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionFilter.fieldName>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenConditionFilter.condition>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenConditionFilter.value>
  {
  }

  public abstract class value2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenConditionFilter.value2>
  {
  }

  public abstract class closeBrackets : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenConditionFilter.closeBrackets>
  {
  }

  public abstract class operatoR : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenConditionFilter.operatoR>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenConditionFilter.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionFilter.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScreenConditionFilter.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenConditionFilter.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionFilter.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScreenConditionFilter.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUScreenConditionFilter.tStamp>
  {
  }
}
