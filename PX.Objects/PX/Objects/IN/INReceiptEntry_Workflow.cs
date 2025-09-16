// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReceiptEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.IN;

public class INReceiptEntry_Workflow : 
  INRegisterEntryBase_Workflow<INReceiptEntry, INDocType.receipt>
{
  public virtual void Configure(PXScreenConfiguration configuration)
  {
    INReceiptEntry_Workflow.Configure(configuration.GetScreenConfigurationContext<INReceiptEntry, INRegister>());
  }

  protected static void Configure(
    WorkflowContext<INReceiptEntry, INRegister> context)
  {
    INRegisterEntryBase_Workflow<INReceiptEntry, INDocType.receipt>.ConfigureCommon(context);
    context.UpdateScreenConfigurationFor((Func<BoundedTo<INReceiptEntry, INRegister>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<INReceiptEntry, INRegister>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<INReceiptEntry, INRegister>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add((Expression<Func<INReceiptEntry, PXAction<INRegister>>>) (g => g.iNItemLabels), (Func<BoundedTo<INReceiptEntry, INRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INReceiptEntry, INRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<INReceiptEntry, INRegister>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 2))))).UpdateDefaultFlow((Func<BoundedTo<INReceiptEntry, INRegister>.Workflow.ConfiguratorFlow, BoundedTo<INReceiptEntry, INRegister>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<INReceiptEntry, INRegister>.BaseFlowStep.ContainerAdjusterStates>) (flowStates => flowStates.Update<INDocStatus.released>((Func<BoundedTo<INReceiptEntry, INRegister>.FlowState.ConfiguratorState, BoundedTo<INReceiptEntry, INRegister>.FlowState.ConfiguratorState>) (flowState => flowState.WithActions((Action<BoundedTo<INReceiptEntry, INRegister>.ActionState.ContainerAdjusterActions>) (actions => actions.Add((Expression<Func<INReceiptEntry, PXAction<INRegister>>>) (g => g.iNItemLabels), (Func<BoundedTo<INReceiptEntry, INRegister>.ActionState.IAllowOptionalConfig, BoundedTo<INReceiptEntry, INRegister>.ActionState.IConfigured>) null)))))))))));
  }
}
