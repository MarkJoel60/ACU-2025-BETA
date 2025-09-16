// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowCategoryCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ProjectDefinition.Workflow;

#nullable enable
namespace PX.SM;

public class AUWorkflowCategoryCustomizedExtension : PXCacheExtension<
#nullable disable
AUWorkflowCategory>
{
  [PXDBBool]
  [PXDefault(false)]
  public bool? PlacementCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? AfterCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? DisplayNameCustomized { get; set; } = new bool?(false);

  public abstract class placementCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowCategoryCustomizedExtension.placementCustomized>
  {
  }

  public abstract class afterCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowCategoryCustomizedExtension.afterCustomized>
  {
  }

  public abstract class displayNameCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowCategoryCustomizedExtension.displayNameCustomized>
  {
  }
}
