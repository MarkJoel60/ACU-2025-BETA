// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenNavigationParameterState
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
public class AUScreenNavigationParameterState : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true, InputMask = "", IsKey = true)]
  public virtual string ActionName { get; set; }

  [PXDBString(256 /*0x0100*/, IsKey = true, InputMask = "", IsUnicode = true)]
  public virtual string FieldName { get; set; }

  [PXDBString(512 /*0x0200*/, InputMask = "", IsUnicode = true)]
  public virtual string Value { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public bool? IsFromSchema { get; set; }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenNavigationParameterState.screenID>
  {
  }

  public abstract class actionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenNavigationParameterState.actionName>
  {
  }

  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenNavigationParameterState.fieldName>
  {
  }

  public abstract class value : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenNavigationParameterState.value>
  {
  }

  public abstract class isFromSchema : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenNavigationParameterState.isFromSchema>
  {
  }
}
