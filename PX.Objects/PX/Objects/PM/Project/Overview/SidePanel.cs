// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.SidePanel
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.PM.Project.Overview;

public static class SidePanel
{
  public static void Configure(
    WorkflowContext<ProjectOverview, PMProject> context)
  {
    context.AddScreenConfigurationFor((Func<BoundedTo<ProjectOverview, PMProject>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ProjectOverview, PMProject>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ProjectOverview, PMProject>.ScreenConfiguration.IConfigured) ((BoundedTo<ProjectOverview, PMProject>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<ProjectOverview, PMProject>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.AddNew("ProjectCustomers", (Func<BoundedTo<ProjectOverview, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectOverview, PMProject>.ActionDefinition.IConfigured>) (config => (BoundedTo<ProjectOverview, PMProject>.ActionDefinition.IConfigured) config.IsSidePanelScreen((Func<BoundedTo<ProjectOverview, PMProject>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<ProjectOverview, PMProject>.NavigationDefinition.IConfiguredSidePanel>) (sidePanelAction => sidePanelAction.NavigateToScreen<CustomerMaint>().WithIcon("work").WithAssignments((Action<BoundedTo<ProjectOverview, PMProject>.NavigationParameter.IContainerFillerNavigationActionParameters>) (containerFiller => containerFiller.Add<PX.Objects.AR.Customer.acctCD>((Func<BoundedTo<ProjectOverview, PMProject>.NavigationParameter.INeedRightOperand, BoundedTo<ProjectOverview, PMProject>.NavigationParameter.IConfigured>) (c => c.SetFromField<PMProject.customerID>())))))).DisplayName("Customers")));
      actions.AddNew("Activities", (Func<BoundedTo<ProjectOverview, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectOverview, PMProject>.ActionDefinition.IConfigured>) (config => (BoundedTo<ProjectOverview, PMProject>.ActionDefinition.IConfigured) config.IsSidePanelScreen((Func<BoundedTo<ProjectOverview, PMProject>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<ProjectOverview, PMProject>.NavigationDefinition.IConfiguredSidePanel>) (sidePanelAction => sidePanelAction.NavigateToScreen<ProjectActivities>().WithIcon("update").WithAssignments((Action<BoundedTo<ProjectOverview, PMProject>.NavigationParameter.IContainerFillerNavigationActionParameters>) (containerFiller => containerFiller.Add<ProjectActivities.ActivitiesFilter.projectID>((Func<BoundedTo<ProjectOverview, PMProject>.NavigationParameter.INeedRightOperand, BoundedTo<ProjectOverview, PMProject>.NavigationParameter.IConfigured>) (c => c.SetFromField<PMProject.contractID>())))))).DisplayName("Activities")));
      actions.AddNew("ShowProjects", (Func<BoundedTo<ProjectOverview, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectOverview, PMProject>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectOverview, PMProject>.ActionDefinition.IConfigured) a.DisplayName("Project Contacts").IsSidePanelScreen((Func<BoundedTo<ProjectOverview, PMProject>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<ProjectOverview, PMProject>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen("PM3011SP").WithIcon("person").WithAssignments((Action<BoundedTo<ProjectOverview, PMProject>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass => ass.Add("PMProjectContact_projectID", (Func<BoundedTo<ProjectOverview, PMProject>.NavigationParameter.INeedRightOperand, BoundedTo<ProjectOverview, PMProject>.NavigationParameter.IConfigured>) (e => e.SetFromField<PMProject.contractCD>()))))))));
    }))));
  }
}
