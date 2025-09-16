// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAssignmentRule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.EP;

[DebuggerDisplay("Entity={Entity} FieldName={FieldName} FieldValue={FieldValue}")]
[PXCacheName("Legacy Assignmnent Rule")]
[Serializable]
public class EPAssignmentRule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int FieldLength = 60;
  protected int? _AssignmentRuleID;
  protected int? _AssignmentRouteID;
  protected 
  #nullable disable
  string _Entity;
  protected string _FieldName;
  protected string _FieldValue;
  protected int? _Condition;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? AssignmentRuleID
  {
    get => this._AssignmentRuleID;
    set => this._AssignmentRuleID = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (EPAssignmentRoute.assignmentRouteID))]
  [PXParent(typeof (Select<EPAssignmentRoute, Where<EPAssignmentRoute.assignmentRouteID, Equal<Current<EPAssignmentRule.assignmentRouteID>>>>))]
  public virtual int? AssignmentRouteID
  {
    get => this._AssignmentRouteID;
    set => this._AssignmentRouteID = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Entity")]
  public virtual string Entity
  {
    get => this._Entity;
    set => this._Entity = value;
  }

  [PXDBString(60)]
  [PXDefault("")]
  [PXUIField]
  [PXFormula(typeof (Default<EPAssignmentRule.entity>))]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXDefault]
  [PXFormula(typeof (Default<EPAssignmentRule.fieldName>))]
  public virtual string FieldValue
  {
    get => this._FieldValue;
    set => this._FieldValue = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [RuleConditionsList]
  [PXUIField]
  public virtual int? Condition
  {
    get => this._Condition;
    set => this._Condition = value;
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
  public virtual DateTime? CreatedDateTime
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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class assignmentRuleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPAssignmentRule.assignmentRuleID>
  {
  }

  public abstract class assignmentRouteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPAssignmentRule.assignmentRouteID>
  {
  }

  public abstract class entity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAssignmentRule.entity>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAssignmentRule.fieldName>
  {
  }

  public abstract class fieldValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPAssignmentRule.fieldValue>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPAssignmentRule.condition>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPAssignmentRule.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPAssignmentRule.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPAssignmentRule.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPAssignmentRule.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPAssignmentRule.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPAssignmentRule.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPAssignmentRule.lastModifiedDateTime>
  {
  }
}
