// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_ProjectEntry_DBox
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class SM_ProjectEntry_DBox : DialogBoxSOApptCreation<SM_ProjectEntry, ProjectEntry, PMProject>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual void Configure(PXScreenConfiguration configuration)
  {
    SM_ProjectEntry_DBox.Configure(configuration.GetScreenConfigurationContext<ProjectEntry, PMProject>());
  }

  protected static void Configure(WorkflowContext<ProjectEntry, PMProject> context)
  {
    BoundedTo<ProjectEntry, PMProject>.ActionCategory.IConfigured servicesCategory = context.Categories.CreateNew("Services", (Func<BoundedTo<ProjectEntry, PMProject>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectEntry, PMProject>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectEntry, PMProject>.ActionCategory.IConfigured) category.DisplayName("Services")));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ProjectEntry, PMProject>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ProjectEntry, PMProject>.ScreenConfiguration.ConfiguratorScreen>) (config => config.WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.ContainerAdjusterActions>) (a =>
    {
      a.Add<SM_ProjectEntry_DBox>((Expression<Func<SM_ProjectEntry_DBox, PXAction<PMProject>>>) (e => e.CreateSrvOrdDocument), (Func<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured) c.InFolder(servicesCategory)));
      a.Add<SM_ProjectEntry_DBox>((Expression<Func<SM_ProjectEntry_DBox, PXAction<PMProject>>>) (e => e.CreateApptDocument), (Func<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured) c.InFolder(servicesCategory)));
    })).UpdateDefaultFlow((Func<BoundedTo<ProjectEntry, PMProject>.Workflow.ConfiguratorFlow, BoundedTo<ProjectEntry, PMProject>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ProjectEntry, PMProject>.BaseFlowStep.ContainerAdjusterStates>) (states => states.Update("A", (Func<BoundedTo<ProjectEntry, PMProject>.FlowState.ConfiguratorState, BoundedTo<ProjectEntry, PMProject>.FlowState.ConfiguratorState>) (state => state.WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionState.ContainerAdjusterActions>) (actions =>
    {
      actions.Add<SM_ProjectEntry_DBox>((Expression<Func<SM_ProjectEntry_DBox, PXAction<PMProject>>>) (e => e.CreateSrvOrdDocument), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
      actions.Add<SM_ProjectEntry_DBox>((Expression<Func<SM_ProjectEntry_DBox, PXAction<PMProject>>>) (e => e.CreateApptDocument), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
    })))))))).WithCategories((Action<BoundedTo<ProjectEntry, PMProject>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(servicesCategory);
      categories.Update("Services", (Func<BoundedTo<ProjectEntry, PMProject>.ActionCategory.ConfiguratorCategory, BoundedTo<ProjectEntry, PMProject>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Commitments"))));
    }))));
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProject> e)
  {
    bool flag1 = this.GetFSSetup() != null;
    bool flag2 = ((PXSelectBase) ((PXGraphExtension<ProjectEntry>) this).Base.Project).Cache.GetStatus((object) ((PXSelectBase<PMProject>) ((PXGraphExtension<ProjectEntry>) this).Base.Project).Current) == 2;
    this.ProjectSelectorEnabled = ProjectDefaultAttribute.IsNonProject(e.Row.ContractID);
    ((PXAction) this.CreateSrvOrdDocument).SetEnabled(flag1 && e.Row != null && !flag2);
    ((PXAction) this.CreateApptDocument).SetEnabled(flag1 && e.Row != null && !flag2);
  }

  protected new virtual void _(PX.Data.Events.RowSelected<DBoxDocSettings> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType fsSrvOrdType = FSSrvOrdType.PK.Find((PXGraph) ((PXGraphExtension<ProjectEntry>) this).Base, e.Row.SrvOrdType);
    PXUIFieldAttribute.SetEnabled<DBoxDocSettings.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DBoxDocSettings>>) e).Cache, (object) e.Row, false);
    PXDefaultAttribute.SetPersistingCheck<DBoxDocSettings.contactID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DBoxDocSettings>>) e).Cache, (object) e.Row, fsSrvOrdType == null || !fsSrvOrdType.RequireContact.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
  }

  public override void PrepareDBoxDefaults()
  {
    PMProject current = ((PXSelectBase<PMProject>) ((PXGraphExtension<ProjectEntry>) this).Base.Project).Current;
    ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current.CustomerID = current.CustomerID;
    ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current.Description = current.Description;
    ((PXSelectBase) this.DocumentSettings).Cache.SetValueExt<DBoxDocSettings.branchID>((object) ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current, (object) current.DefaultBranchID);
    ((PXSelectBase) this.DocumentSettings).Cache.SetValueExt<DBoxDocSettings.projectID>((object) ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current, (object) current.ContractID);
  }

  public override void PrepareHeaderAndDetails(DBoxHeader header, List<DBoxDetails> details)
  {
    if (header == null || ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current == null)
      return;
    PMProject current = ((PXSelectBase<PMProject>) ((PXGraphExtension<ProjectEntry>) this).Base.Project).Current;
    header.LocationID = current.LocationID;
    header.CuryID = current.BillingCuryID;
    header.sourceDocument = (object) current;
    if (((PXSelectBase<PMSetup>) ((PXGraphExtension<ProjectEntry>) this).Base.Setup).Current.CalculateProjectSpecificTaxes.GetValueOrDefault())
    {
      header.Address = (DBoxHeaderAddress) ((PXSelectBase<PMSiteAddress>) ((PXGraphExtension<ProjectEntry>) this).Base.Site_Address).Current;
      header.TaxZoneID = current.RevenueTaxZoneID;
    }
    else
    {
      PX.Objects.CR.Location location = PX.Objects.CR.Location.PK.Find((PXGraph) ((PXGraphExtension<ProjectEntry>) this).Base, current.CustomerID, current.LocationID);
      header.TaxZoneID = location.CTaxZoneID;
    }
  }

  public override void CreateDocument(
    ServiceOrderEntry srvOrdGraph,
    AppointmentEntry apptGraph,
    DBoxHeader header,
    List<DBoxDetails> details)
  {
    this.CreateDocument(srvOrdGraph, apptGraph, (string) null, (string) null, (string) null, new int?(), ((PXSelectBase) ((PXGraphExtension<ProjectEntry>) this).Base.Project).Cache, (PXCache) null, header, details, header.CreateAppointment.GetValueOrDefault());
  }

  public virtual FSSetup GetFSSetup()
  {
    return ((PXSelectBase<FSSetup>) this.Base1.SetupRecord).Current == null ? PXResultset<FSSetup>.op_Implicit(((PXSelectBase<FSSetup>) this.Base1.SetupRecord).Select(Array.Empty<object>())) : ((PXSelectBase<FSSetup>) this.Base1.SetupRecord).Current;
  }
}
