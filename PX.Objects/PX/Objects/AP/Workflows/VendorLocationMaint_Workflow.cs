// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Workflows.VendorLocationMaint_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CR.Workflows;

#nullable disable
namespace PX.Objects.AP.Workflows;

public class VendorLocationMaint_Workflow : PXGraphExtension<VendorLocationMaint>
{
  public static bool IsActive() => false;

  public sealed override void Configure(PXScreenConfiguration configuration)
  {
    LocationWorkflow.Configure(configuration);
  }
}
