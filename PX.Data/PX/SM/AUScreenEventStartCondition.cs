// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenEventStartCondition
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
public class AUScreenEventStartCondition : AUScreenConditionFilter
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (AUScreenEvent.childRowCntr))]
  public override int? RowNbr
  {
    get => this._RowNbr;
    set => this._RowNbr = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (AUScreenEvent.itemCD))]
  [PXParent(typeof (Select<AUScreenEvent, Where<AUScreenEvent.screenID, Equal<Current<AUScreenEventStartCondition.screenID>>, And<AUScreenEvent.projectID, Equal<Current<AUScreenEventStartCondition.projectID>>, And<AUScreenEvent.itemCD, Equal<Current<AUScreenEventStartCondition.conditionID>>, And<Current<AUScreenEventStartCondition.conditionID>, Equal<AUScreenEventStateType.startState>, And<AUScreenEvent.itemType, Equal<AUScreenItemType.eventsConditional>>>>>>>))]
  public override 
  #nullable disable
  string ConditionID
  {
    get => this._ConditionID;
    set => this._ConditionID = value;
  }

  [PXDBString(1, IsUnicode = false)]
  [PXDefault(typeof (AUScreenEventStateType.startState))]
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
    AUScreenEventStartCondition.screenID>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenEventStartCondition.projectID>
  {
  }

  public new abstract class rowNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenEventStartCondition.rowNbr>
  {
  }

  public new abstract class conditionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventStartCondition.conditionID>
  {
  }

  public new abstract class eventStateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventStartCondition.eventStateType>
  {
  }
}
