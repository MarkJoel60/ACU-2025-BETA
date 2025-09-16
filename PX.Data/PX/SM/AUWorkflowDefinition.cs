// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

[PXCacheName("Workflow Definition")]
public class AUWorkflowDefinition : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IScreenItem
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  [PXDefault]
  [PXUIField(DisplayName = "State Identifier")]
  public virtual string StateField { get; set; }

  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  [PXUIField(DisplayName = "Type Identifier")]
  public virtual string FlowTypeField { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Users to Modify Type")]
  public bool? EnableWorkflowIDField { get; set; }

  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  [PXUIField(DisplayName = "Subtype Identifier")]
  public virtual string FlowSubTypeField { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Users to Modify Subtype")]
  public bool? EnableWorkflowSubTypeField { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? AutoSaveDefault { get; set; } = new bool?(false);

  [PXBool]
  [PXUnboundDefault(true)]
  public virtual bool? AllowWorkflowCustomization { get; set; }

  public abstract class screenID : IBqlField, IBqlOperand
  {
  }

  public abstract class stateField : IBqlField, IBqlOperand
  {
  }

  public abstract class flowTypeField : IBqlField, IBqlOperand
  {
  }

  public abstract class enableWorkflowIDField : IBqlField, IBqlOperand
  {
  }

  public abstract class flowSubTypeField : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowDefinition.flowSubTypeField>
  {
  }

  public abstract class enableWorkflowSubTypeField : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowDefinition.enableWorkflowSubTypeField>
  {
  }

  public abstract class autoSaveDefault : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowDefinition.autoSaveDefault>
  {
  }

  public abstract class allowWorkflowCustomization : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowDefinition.allowWorkflowCustomization>
  {
  }
}
