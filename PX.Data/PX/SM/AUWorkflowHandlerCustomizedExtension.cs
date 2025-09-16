// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowHandlerCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowHandlerCustomizedExtension : PXCacheExtension<
#nullable disable
AUWorkflowHandler>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ConditionCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? SelectTypeCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? AllowMultipleSelectCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? UseParameterAsPrimarySourceCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? UseTargetAsPrimarySourceCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? SelectIsCommandCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? DisplayNameCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? EventNameCustomized { get; set; } = new bool?(false);

  public abstract class conditionCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandlerCustomizedExtension.conditionCustomized>
  {
  }

  public abstract class selectTypeCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandlerCustomizedExtension.selectTypeCustomized>
  {
  }

  public abstract class allowMultipleSelectCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandlerCustomizedExtension.allowMultipleSelectCustomized>
  {
  }

  public abstract class useParameterAsPrimarySourceCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandlerCustomizedExtension.useParameterAsPrimarySourceCustomized>
  {
  }

  public abstract class useTargetAsPrimarySourceCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandlerCustomizedExtension.useTargetAsPrimarySourceCustomized>
  {
  }

  public abstract class selectIsCommandCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandlerCustomizedExtension.selectIsCommandCustomized>
  {
  }

  public abstract class displayNameCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandlerCustomizedExtension.displayNameCustomized>
  {
  }

  public abstract class eventNameCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowHandlerCustomizedExtension.eventNameCustomized>
  {
  }
}
