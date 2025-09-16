// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowHandler
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

[PXCacheName("Workflow Handler")]
public class AUWorkflowHandler : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUScreenDefinition.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Action Name")]
  public virtual string HandlerName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Display Name")]
  public virtual string DisplayName { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Event Name")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string EventName { get; set; }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Event Container Name")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string EventContainerName { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Condition")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string Condition { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Select Type")]
  public virtual string SelectType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Multiple Select")]
  public bool? AllowMultipleSelect { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Parameter As Primary Source")]
  public bool? UseParameterAsPrimarySource { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Target As Primary Source")]
  public bool? UseTargetAsPrimarySource { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public bool? SelectIsCommand { get; set; } = new bool?(false);

  [PXDBString]
  public virtual string UpcastType { get; set; }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowHandler.screenID>
  {
  }

  public abstract class handlerName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowHandler.handlerName>
  {
  }

  public abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowHandler.displayName>
  {
  }

  public abstract class eventName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowHandler.eventName>
  {
  }

  public abstract class eventContainerName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowHandler.eventContainerName>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowHandler.condition>
  {
  }

  public abstract class selectType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowHandler.selectType>
  {
  }

  public abstract class allowMultipleSelect : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandler.allowMultipleSelect>
  {
  }

  public abstract class useParameterAsPrimarySource : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandler.useParameterAsPrimarySource>
  {
  }

  public abstract class useTargetAsPrimarySource : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandler.useTargetAsPrimarySource>
  {
  }

  public abstract class selectIsCommand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandler.selectIsCommand>
  {
  }

  public abstract class upcastType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowHandler.upcastType>
  {
  }
}
