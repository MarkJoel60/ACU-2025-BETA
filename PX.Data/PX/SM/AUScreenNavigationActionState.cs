// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenNavigationActionState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXTable]
[Serializable]
public class AUScreenNavigationActionState : AUScreenActionBaseState
{
  [PXDBString(8, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  public virtual 
  #nullable disable
  string DestinationScreenID { get; set; }

  [PXDefault("S")]
  [PXWindowMode]
  [PXDBString(1)]
  public virtual string WindowMode { get; set; }

  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  public string Icon { get; set; }

  public new abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenNavigationActionState.screenID>
  {
  }

  public new abstract class actionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenNavigationActionState.actionName>
  {
  }

  public abstract class destinationScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenNavigationActionState.destinationScreenID>
  {
  }

  public abstract class windowMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenNavigationActionState.windowMode>
  {
  }

  public abstract class icon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenNavigationActionState.icon>
  {
  }
}
