// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPriceWorksheetMaint_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AP;

public class APPriceWorksheetMaint_Workflow : PXGraphExtension<APPriceWorksheetMaint>
{
  public sealed override void Configure(PXScreenConfiguration config)
  {
    APPriceWorksheetMaint_Workflow.Configure(config.GetScreenConfigurationContext<APPriceWorksheetMaint, APPriceWorksheet>());
  }

  protected static void Configure(
    WorkflowContext<APPriceWorksheetMaint, APPriceWorksheet> context)
  {
    APPriceWorksheetMaint_Workflow.Conditions conditions = context.Conditions.GetPack<APPriceWorksheetMaint_Workflow.Conditions>();
    BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionCategory.IConfigured>) (category => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    context.AddScreenConfigurationFor((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ScreenConfiguration.IStartConfigScreen, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ScreenConfiguration.IConfigured) screen.StateIdentifierIs<APPriceWorksheet.status>().AddDefaultFlow((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Workflow.INeedStatesFlow, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Workflow.IConfigured>) (flow => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Workflow.IConfigured) flow.WithFlowStates((System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<APPriceWorksheetMaint, PXAutoAction<APPriceWorksheet>>>) (g => g.initializeState))));
      fss.Add<SPWorksheetStatus.hold>((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<APPriceWorksheetMaint, PXAction<APPriceWorksheet>>>) (g => g.releaseFromHold), (Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)))))));
      fss.Add<SPWorksheetStatus.open>((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APPriceWorksheetMaint, PXAction<APPriceWorksheet>>>) (g => g.ReleasePriceWorksheet), (Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        actions.Add((Expression<Func<APPriceWorksheetMaint, PXAction<APPriceWorksheet>>>) (g => g.putOnHold));
      }))));
      fss.Add<SPWorksheetStatus.released>();
    })).WithTransitions((System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.INeedTarget, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.IConfigured>) (t => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.IConfigured) t.To<SPWorksheetStatus.hold>().IsTriggeredOn((Expression<Func<APPriceWorksheetMaint, PXAction<APPriceWorksheet>>>) (g => g.initializeState)).When((BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ISharedCondition) conditions.IsOnHold)))));
      transitions.AddGroupFrom<SPWorksheetStatus.hold>((System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.INeedTarget, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.IConfigured>) (t => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.IConfigured) t.To<SPWorksheetStatus.open>().IsTriggeredOn((Expression<Func<APPriceWorksheetMaint, PXAction<APPriceWorksheet>>>) (g => g.releaseFromHold)).WithFieldAssignments((System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IContainerFillerFields>) (fas => fas.Add<APPriceWorksheet.hold>((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.INeedRightOperand, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IConfigured>) (f => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IConfigured) f.SetFromValue((object) false)))))))));
      transitions.AddGroupFrom<SPWorksheetStatus.open>((System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.INeedTarget, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.IConfigured>) (t => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.IConfigured) t.To<SPWorksheetStatus.hold>().IsTriggeredOn((Expression<Func<APPriceWorksheetMaint, PXAction<APPriceWorksheet>>>) (g => g.putOnHold)).WithFieldAssignments((System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IContainerFillerFields>) (fas => fas.Add<APPriceWorksheet.hold>((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.INeedRightOperand, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IConfigured>) (f => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.INeedTarget, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.IConfigured>) (t => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Transition.IConfigured) t.To<SPWorksheetStatus.released>().IsTriggeredOn((Expression<Func<APPriceWorksheetMaint, PXAction<APPriceWorksheet>>>) (g => g.ReleasePriceWorksheet))));
      }));
    })))).WithActions((System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<APPriceWorksheetMaint, PXAction<APPriceWorksheet>>>) (g => g.initializeState), (Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<APPriceWorksheetMaint, PXAction<APPriceWorksheet>>>) (g => g.releaseFromHold), (Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions(ActionPersistOptions.NoPersist).WithFieldAssignments((System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IContainerFillerFields>) (fas => fas.Add<APPriceWorksheet.hold>((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.INeedRightOperand, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IConfigured>) (f => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<APPriceWorksheetMaint, PXAction<APPriceWorksheet>>>) (g => g.putOnHold), (Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions(ActionPersistOptions.NoPersist).WithFieldAssignments((System.Action<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IContainerFillerFields>) (fas => fas.Add<APPriceWorksheet.hold>((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.INeedRightOperand, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IConfigured>) (f => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<APPriceWorksheetMaint, PXAction<APPriceWorksheet>>>) (g => g.ReleasePriceWorksheet), (Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
    }))));
  }

  public class Conditions : BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Condition.Pack
  {
    public BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Condition.ConditionBuilder, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Condition>) (c => c.FromBql<BqlOperand<APPriceWorksheet.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Condition.ConditionBuilder, BoundedTo<APPriceWorksheetMaint, APPriceWorksheet>.Condition>) (c => c.FromBql<BqlOperand<APPriceWorksheet.hold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
      }
    }
  }

  public static class ActionCategoryNames
  {
    public const string Processing = "Processing";
  }

  public static class ActionCategory
  {
    public const string Processing = "Processing";
  }
}
