// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.ProjectEntry_InventoryTransferExt_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class ProjectEntry_InventoryTransferExt_Workflow : 
  PXGraphExtension<ProjectEntry_Workflow, ProjectEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.materialManagement>();

  public virtual void Configure(PXScreenConfiguration config)
  {
    ProjectEntry_InventoryTransferExt_Workflow.Configure(config.GetScreenConfigurationContext<ProjectEntry, PMProject>());
  }

  protected static void Configure(WorkflowContext<ProjectEntry, PMProject> context)
  {
    var conditions = new
    {
      TrackedByLocation = context.Conditions.FromBql<BqlOperand<PMProject.accountingMode, IBqlString>.IsEqual<ProjectAccountingModes.linked>>(),
      NotVisibleInIN = context.Conditions.FromBql<BqlOperand<PMProject.visibleInIN, IBqlBool>.IsEqual<False>>()
    }.AutoNameConditions();
    BoundedTo<ProjectEntry, PMProject>.ActionCategory.IConfigured inventoryCategory = context.Categories.CreateNew("Inventory", (Func<BoundedTo<ProjectEntry, PMProject>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectEntry, PMProject>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectEntry, PMProject>.ActionCategory.IConfigured) category.DisplayName("Inventory")));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ProjectEntry, PMProject>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ProjectEntry, PMProject>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ProjectEntry, PMProject>.Workflow.ConfiguratorFlow, BoundedTo<ProjectEntry, PMProject>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ProjectEntry, PMProject>.BaseFlowStep.ContainerAdjusterStates>) (fss => fss.Update<ProjectStatus.active>((Func<BoundedTo<ProjectEntry, PMProject>.FlowState.ConfiguratorState, BoundedTo<ProjectEntry, PMProject>.FlowState.ConfiguratorState>) (flowState => flowState.WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionState.ContainerAdjusterActions>) (actions => actions.Add<ProjectEntry_InventoryTransferExt>((Expression<Func<ProjectEntry_InventoryTransferExt, PXAction<PMProject>>>) (g => g.transferInventory), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null))))))))).WithCategories((Action<BoundedTo<ProjectEntry, PMProject>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(inventoryCategory);
      categories.Update("Inventory", (Func<BoundedTo<ProjectEntry, PMProject>.ActionCategory.ConfiguratorCategory, BoundedTo<ProjectEntry, PMProject>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Commitments"))));
    })).WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<ProjectEntry_InventoryTransferExt>((Expression<Func<ProjectEntry_InventoryTransferExt, PXAction<PMProject>>>) (g => g.transferInventory), (Func<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured) c.InFolder(context.Categories.Get("Inventory")).IsDisabledWhen((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) conditions.TrackedByLocation).IsHiddenWhen((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) conditions.NotVisibleInIN)))))));
  }
}
