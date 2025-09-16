// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxAdjustmentEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.TX;

public class TaxAdjustmentEntry_Workflow : PXGraphExtension<TaxAdjustmentEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    TaxAdjustmentEntry_Workflow.Configure(config.GetScreenConfigurationContext<TaxAdjustmentEntry, TaxAdjustment>());
  }

  protected static void Configure(
    WorkflowContext<TaxAdjustmentEntry, TaxAdjustment> context)
  {
    TaxAdjustmentEntry_Workflow.Conditions conditions = context.Conditions.GetPack<TaxAdjustmentEntry_Workflow.Conditions>();
    BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionCategory.IConfigured>) (category => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("Corrections", (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionCategory.IConfigured>) (category => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    context.AddScreenConfigurationFor((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ScreenConfiguration.IStartConfigScreen, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ScreenConfiguration.IConfigured) ((BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<TaxAdjustment.status>().AddDefaultFlow((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Workflow.INeedStatesFlow, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Workflow.IConfigured>) (flow => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<TaxAdjustmentEntry, PXAutoAction<TaxAdjustment>>>) (g => g.initializeState))));
      fss.Add<TaxAdjustmentStatus.hold>((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.releaseFromHold), (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IAllowOptionalConfig, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IConfigured>) (a => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))));
      fss.Add<TaxAdjustmentStatus.balanced>((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.release), (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IAllowOptionalConfig, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IConfigured>) (a => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.putOnHold), (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IAllowOptionalConfig, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IConfigured>) null);
      }))));
      fss.Add<TaxAdjustmentStatus.released>((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.reverseAdjustment), (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IAllowOptionalConfig, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IConfigured>) (a => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.INeedTarget, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.IConfigured>) (t => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.IConfigured) t.To<TaxAdjustmentStatus.hold>().IsTriggeredOn((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.initializeState)).When((BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ISharedCondition) conditions.IsOnHold)))));
      transitions.AddGroupFrom<TaxAdjustmentStatus.hold>((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.INeedTarget, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.IConfigured>) (t => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.IConfigured) t.To<TaxAdjustmentStatus.balanced>().IsTriggeredOn((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IContainerFillerFields>) (fas => fas.Add<TaxAdjustment.hold>((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.INeedRightOperand, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IConfigured>) (f => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IConfigured) f.SetFromValue((object) false)))))))));
      transitions.AddGroupFrom<TaxAdjustmentStatus.balanced>((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.INeedTarget, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.IConfigured>) (t => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.IConfigured) t.To<TaxAdjustmentStatus.hold>().IsTriggeredOn((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IContainerFillerFields>) (fas => fas.Add<TaxAdjustment.hold>((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.INeedRightOperand, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IConfigured>) (f => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.INeedTarget, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.IConfigured>) (t => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Transition.IConfigured) t.To<TaxAdjustmentStatus.released>().IsTriggeredOn((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.release))));
      }));
    })))).WithActions((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.initializeState), (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IConfigured>) (a => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.releaseFromHold), (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IConfigured>) (c => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IContainerFillerFields>) (fas => fas.Add<TaxAdjustment.hold>((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.INeedRightOperand, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IConfigured>) (f => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.putOnHold), (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IConfigured>) (c => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IContainerFillerFields>) (fas => fas.Add<TaxAdjustment.hold>((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.INeedRightOperand, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IConfigured>) (f => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.release), (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IConfigured>) (c => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<TaxAdjustmentEntry, PXAction<TaxAdjustment>>>) (g => g.reverseAdjustment), (Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IConfigured>) (c => (BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory)));
    })).WithCategories((Action<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
    }))));
  }

  public class Conditions : BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Condition.Pack
  {
    public BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Condition.ConditionBuilder, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Condition>) (c => c.FromBql<BqlOperand<TaxAdjustment.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Condition.ConditionBuilder, BoundedTo<TaxAdjustmentEntry, TaxAdjustment>.Condition>) (c => c.FromBql<BqlOperand<TaxAdjustment.hold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
      }
    }
  }

  public static class ActionCategoryNames
  {
    public const string Processing = "Processing";
    public const string Corrections = "Corrections";
  }

  public static class ActionCategory
  {
    public const string Processing = "Processing";
    public const string Corrections = "Corrections";
  }
}
