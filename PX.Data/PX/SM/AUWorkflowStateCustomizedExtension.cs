// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowStateCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowStateCustomizedExtension : PXCacheExtension<
#nullable disable
AUWorkflowState>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? StateLineNbrCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsInitialCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? LayoutCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DescriptionCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? NextStateCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? StateTypeCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ParentStateCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? SkipConditionIDCustomized { get; set; } = new bool?(false);

  public abstract class stateLineNbrCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateCustomizedExtension.stateLineNbrCustomized>
  {
  }

  public abstract class isInitialCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateCustomizedExtension.isInitialCustomized>
  {
  }

  public abstract class layoutCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateCustomizedExtension.layoutCustomized>
  {
  }

  public abstract class descriptionCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateCustomizedExtension.descriptionCustomized>
  {
  }

  public abstract class nextStateCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateCustomizedExtension.nextStateCustomized>
  {
  }

  public abstract class stateTypeCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateCustomizedExtension.stateTypeCustomized>
  {
  }

  public abstract class parentStateCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateCustomizedExtension.parentStateCustomized>
  {
  }

  public abstract class skipConditionIDCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateCustomizedExtension.skipConditionIDCustomized>
  {
  }
}
