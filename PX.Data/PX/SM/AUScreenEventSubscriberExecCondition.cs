// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenEventSubscriberExecCondition
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
public class AUScreenEventSubscriberExecCondition : AUScreenConditionFilter
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (AUScreenEventSubscriber.childRowCntr))]
  public override int? RowNbr
  {
    get => this._RowNbr;
    set => this._RowNbr = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (AUScreenEventSubscriber.itemCD))]
  [PXParent(typeof (Select<AUScreenEventSubscriber, Where<AUScreenEventSubscriber.screenID, Equal<Current<AUScreenEventSubscriberExecCondition.screenID>>, And<AUScreenEventSubscriber.projectID, Equal<Current<AUScreenEventSubscriberExecCondition.projectID>>, And<AUScreenEventSubscriber.itemCD, Equal<Current<AUScreenEventSubscriberExecCondition.conditionID>>>>>>))]
  public override 
  #nullable disable
  string ConditionID
  {
    get => this._ConditionID;
    set => this._ConditionID = value;
  }

  [PXDBString(1, IsUnicode = false)]
  [PXDefault(typeof (AUScreenEventStateType.eventSubscriberCriterias))]
  public override string EventStateType
  {
    get => this._EventStateType;
    set => this._EventStateType = value;
  }

  public new abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventSubscriberExecCondition.screenID>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenEventSubscriberExecCondition.projectID>
  {
  }

  public new abstract class rowNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenEventSubscriberExecCondition.rowNbr>
  {
  }

  public new abstract class conditionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventSubscriberExecCondition.conditionID>
  {
  }

  public new abstract class eventStateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventSubscriberExecCondition.eventStateType>
  {
  }
}
