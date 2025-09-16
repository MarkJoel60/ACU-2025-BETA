// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenCondition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select<AUScreenItem, Where<AUScreenItem.itemType, Equal<AUScreenItemType.condition>>>), Persistent = true)]
[Serializable]
public class AUScreenCondition : AUScreenItem
{
  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Condition ID")]
  public override 
  #nullable disable
  string ItemCD
  {
    get => this._ItemCD;
    set => this._ItemCD = value;
  }

  [PXDBString(2, IsUnicode = false, InputMask = "")]
  [PXDefault("C")]
  public override string ItemType
  {
    get => this._ItemType;
    set => this._ItemType = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override", Enabled = true)]
  [PXDBCalced(typeof (Switch<Case<Where<AUScreenCondition.projectID, Equal<CurrentValue<AUScreenDefinition.projectID>>>, PX.Data.True>, False>), typeof (bool))]
  [PXFormula(typeof (Switch<Case<Where<AUScreenCondition.projectID, Equal<CurrentValue<AUScreenDefinition.projectID>>>, PX.Data.True>, False>))]
  public override bool? IsOverride
  {
    get => this._IsOverride;
    set => this._IsOverride = value;
  }

  public new abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenCondition.screenID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenCondition.projectID>
  {
  }

  public new abstract class itemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenCondition.itemCD>
  {
  }

  public new abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenCondition.itemType>
  {
  }

  public new abstract class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenCondition.itemID>
  {
  }

  public new abstract class childRowCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenCondition.childRowCntr>
  {
  }

  public new abstract class isOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenCondition.isOverride>
  {
  }
}
