// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenActionState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ProjectDefinition.Attributes;
using System;

#nullable enable
namespace PX.SM;

[PXTable]
[Serializable]
public class AUScreenActionState : AUScreenActionBaseState
{
  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Method")]
  [PXActions(typeof (AUScreenDefinition.screenID), typeof (AUScreenActionBaseState.dataMember))]
  public virtual 
  #nullable disable
  string Method { get; set; }

  public virtual ScreenActionExtraData ExtraData { get; set; }

  public new abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionState.screenID>
  {
  }

  public new abstract class actionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionState.actionName>
  {
  }

  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenActionState.method>
  {
  }
}
