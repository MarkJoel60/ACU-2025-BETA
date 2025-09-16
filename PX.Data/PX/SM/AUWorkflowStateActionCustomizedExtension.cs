// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowStateActionCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowStateActionCustomizedExtension : PXCacheExtension<
#nullable disable
AUWorkflowStateAction>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? StateActionLineNbrCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? IsTopLevelCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? IsDisabledCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? IsHideCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? AutoRunCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ConnotationCustomized { get; set; } = new bool?(false);

  public abstract class stateActionLineNbrCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateActionCustomizedExtension.stateActionLineNbrCustomized>
  {
  }

  public abstract class isTopLevelCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateActionCustomizedExtension.isTopLevelCustomized>
  {
  }

  public abstract class isDisabledCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateActionCustomizedExtension.isDisabledCustomized>
  {
  }

  public abstract class isHideCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateActionCustomizedExtension.isHideCustomized>
  {
  }

  public abstract class autoRunCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateActionCustomizedExtension.autoRunCustomized>
  {
  }

  public abstract class connotationCustomized : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateActionCustomizedExtension.connotationCustomized>
  {
  }
}
