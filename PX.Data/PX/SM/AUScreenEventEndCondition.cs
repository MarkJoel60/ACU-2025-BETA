// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenEventEndCondition
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
public class AUScreenEventEndCondition : AUScreenConditionFilter
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
  [PXParent(typeof (Select<AUScreenEvent, Where<AUScreenEvent.screenID, Equal<Current<AUScreenEventEndCondition.screenID>>, And<AUScreenEvent.projectID, Equal<Current<AUScreenEventEndCondition.projectID>>, And<AUScreenEvent.itemCD, Equal<Current<AUScreenEventEndCondition.conditionID>>>>>>))]
  public override 
  #nullable disable
  string ConditionID
  {
    get => this._ConditionID;
    set => this._ConditionID = value;
  }

  [PXDBString(1, IsUnicode = false)]
  [PXDefault(typeof (AUScreenEventStateType.endState))]
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
    AUScreenEventEndCondition.screenID>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenEventEndCondition.projectID>
  {
  }

  public new abstract class rowNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenEventEndCondition.rowNbr>
  {
  }

  public new abstract class conditionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventEndCondition.conditionID>
  {
  }

  public new abstract class eventStateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventEndCondition.eventStateType>
  {
  }
}
