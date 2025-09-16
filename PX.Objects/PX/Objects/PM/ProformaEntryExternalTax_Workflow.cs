// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProformaEntryExternalTax_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PM;

public class ProformaEntryExternalTax_Workflow : 
  PXGraphExtension<ProformaEntry_ApprovalWorkflow, ProformaEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  public virtual void Configure(PXScreenConfiguration config)
  {
    ProformaEntryExternalTax_Workflow.Configure(config.GetScreenConfigurationContext<ProformaEntry, PMProforma>());
  }

  protected static void Configure(WorkflowContext<ProformaEntry, PMProforma> context)
  {
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ProformaEntry, PMProforma>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ProformaEntry, PMProforma>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ProformaEntry, PMProforma>.Workflow.ConfiguratorFlow, BoundedTo<ProformaEntry, PMProforma>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ProformaEntry, PMProforma>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Update<ProformaStatus.onHold>((Func<BoundedTo<ProformaEntry, PMProforma>.FlowState.ConfiguratorState, BoundedTo<ProformaEntry, PMProforma>.FlowState.ConfiguratorState>) (flowState => flowState.WithActions((Action<BoundedTo<ProformaEntry, PMProforma>.ActionState.ContainerAdjusterActions>) (actions => actions.Add<ProformaEntryExternalTax>((Expression<Func<ProformaEntryExternalTax, PXAction<PMProforma>>>) (g => g.recalcExternalTax), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) null)))));
      fss.Update<ProformaStatus.open>((Func<BoundedTo<ProformaEntry, PMProforma>.FlowState.ConfiguratorState, BoundedTo<ProformaEntry, PMProforma>.FlowState.ConfiguratorState>) (flowState => flowState.WithActions((Action<BoundedTo<ProformaEntry, PMProforma>.ActionState.ContainerAdjusterActions>) (actions => actions.Add<ProformaEntryExternalTax>((Expression<Func<ProformaEntryExternalTax, PXAction<PMProforma>>>) (g => g.recalcExternalTax), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) null)))));
      fss.Update<ProformaStatus.pendingApproval>((Func<BoundedTo<ProformaEntry, PMProforma>.FlowState.ConfiguratorState, BoundedTo<ProformaEntry, PMProforma>.FlowState.ConfiguratorState>) (flowState => flowState.WithActions((Action<BoundedTo<ProformaEntry, PMProforma>.ActionState.ContainerAdjusterActions>) (actions => actions.Add<ProformaEntryExternalTax>((Expression<Func<ProformaEntryExternalTax, PXAction<PMProforma>>>) (g => g.recalcExternalTax), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) null)))));
      fss.Update<ProformaStatus.rejected>((Func<BoundedTo<ProformaEntry, PMProforma>.FlowState.ConfiguratorState, BoundedTo<ProformaEntry, PMProforma>.FlowState.ConfiguratorState>) (flowState => flowState.WithActions((Action<BoundedTo<ProformaEntry, PMProforma>.ActionState.ContainerAdjusterActions>) (actions => actions.Add<ProformaEntryExternalTax>((Expression<Func<ProformaEntryExternalTax, PXAction<PMProforma>>>) (g => g.recalcExternalTax), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) null)))));
    })))).WithActions((Action<BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<ProformaEntryExternalTax>((Expression<Func<ProformaEntryExternalTax, PXAction<PMProforma>>>) (g => g.recalcExternalTax), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IConfigured) c.InFolder(context.Categories.Get("OtherCategory"), (Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.send)).PlaceAfter((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.send))))))));
  }
}
