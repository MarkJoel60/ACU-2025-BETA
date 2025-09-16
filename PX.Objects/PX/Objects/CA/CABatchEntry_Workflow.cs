// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABatchEntry_Workflow
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
namespace PX.Objects.CA;

public class CABatchEntry_Workflow : PXGraphExtension<CABatchEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    CABatchEntry_Workflow.Configure(config.GetScreenConfigurationContext<CABatchEntry, CABatch>());
  }

  protected static void Configure(WorkflowContext<CABatchEntry, CABatch> context)
  {
    CABatchEntry_Workflow.Conditions conditions = context.Conditions.GetPack<CABatchEntry_Workflow.Conditions>();
    BoundedTo<CABatchEntry, CABatch>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<CABatchEntry, CABatch>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CABatchEntry, CABatch>.ActionCategory.IConfigured>) (category => (BoundedTo<CABatchEntry, CABatch>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<CABatchEntry, CABatch>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("Corrections", (Func<BoundedTo<CABatchEntry, CABatch>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CABatchEntry, CABatch>.ActionCategory.IConfigured>) (category => (BoundedTo<CABatchEntry, CABatch>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    context.AddScreenConfigurationFor((Func<BoundedTo<CABatchEntry, CABatch>.ScreenConfiguration.IStartConfigScreen, BoundedTo<CABatchEntry, CABatch>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<CABatchEntry, CABatch>.ScreenConfiguration.IConfigured) ((BoundedTo<CABatchEntry, CABatch>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<CABatch.status>().AddDefaultFlow((Func<BoundedTo<CABatchEntry, CABatch>.Workflow.INeedStatesFlow, BoundedTo<CABatchEntry, CABatch>.Workflow.IConfigured>) (flow => (BoundedTo<CABatchEntry, CABatch>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<CABatchEntry, CABatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<CABatchEntry, PXAutoAction<CABatch>>>) (g => g.initializeState))));
      fss.Add<CABatchStatus.hold>((Func<BoundedTo<CABatchEntry, CABatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CABatchEntry, CABatch>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.releaseFromHold), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) (a => (BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.addPayments), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) null);
      }))));
      fss.Add<CABatchStatus.balanced>((Func<BoundedTo<CABatchEntry, CABatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CABatchEntry, CABatch>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.release), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) (a => (BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.export), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) (a => (BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.putOnHold), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.addPayments), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) null);
      }))));
      fss.Add<CABatchStatus.exported>((Func<BoundedTo<CABatchEntry, CABatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CABatchEntry, CABatch>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.release), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) (a => (BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.cancelBatch), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.setBalanced), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.export), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) null);
      }))));
      fss.Add<CABatchStatus.released>((Func<BoundedTo<CABatchEntry, CABatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CABatchEntry, CABatch>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.export), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) (a => (BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.voidBatch), (Func<BoundedTo<CABatchEntry, CABatch>.ActionState.IAllowOptionalConfig, BoundedTo<CABatchEntry, CABatch>.ActionState.IConfigured>) null);
      }))));
      fss.Add<CABatchStatus.canceled>((Func<BoundedTo<CABatchEntry, CABatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CABatchEntry, CABatch>.ActionState.IContainerFillerActions>) (actions => { }))));
      fss.Add<CABatchStatus.voided>((Func<BoundedTo<CABatchEntry, CABatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CABatchEntry, CABatch>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CABatchEntry, CABatch>.ActionState.IContainerFillerActions>) (actions => { }))));
    })).WithTransitions((Action<BoundedTo<CABatchEntry, CABatch>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<CABatchEntry, CABatch>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<CABatchEntry, CABatch>.Transition.INeedTarget, BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured>) (t => (BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured) t.To<CABatchStatus.hold>().IsTriggeredOn((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.initializeState)).When((BoundedTo<CABatchEntry, CABatch>.ISharedCondition) conditions.IsOnHold)))));
      transitions.AddGroupFrom<CABatchStatus.hold>((Action<BoundedTo<CABatchEntry, CABatch>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CABatchEntry, CABatch>.Transition.INeedTarget, BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured>) (t => (BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured) t.To<CABatchStatus.balanced>().IsTriggeredOn((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<CABatchEntry, CABatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<CABatch.hold>((Func<BoundedTo<CABatchEntry, CABatch>.Assignment.INeedRightOperand, BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured>) (f => (BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<CABatchEntry, CABatch>.Transition.INeedTarget, BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured>) (t => (BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured) t.To<CABatchStatus.exported>().IsTriggeredOn((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.releaseFromHold)).When((BoundedTo<CABatchEntry, CABatch>.ISharedCondition) conditions.IsExported).WithFieldAssignments((Action<BoundedTo<CABatchEntry, CABatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<CABatch.hold>((Func<BoundedTo<CABatchEntry, CABatch>.Assignment.INeedRightOperand, BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured>) (f => (BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      }));
      transitions.AddGroupFrom<CABatchStatus.balanced>((Action<BoundedTo<CABatchEntry, CABatch>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CABatchEntry, CABatch>.Transition.INeedTarget, BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured>) (t => (BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured) t.To<CABatchStatus.hold>().IsTriggeredOn((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<CABatchEntry, CABatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<CABatch.hold>((Func<BoundedTo<CABatchEntry, CABatch>.Assignment.INeedRightOperand, BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured>) (f => (BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<CABatchEntry, CABatch>.Transition.INeedTarget, BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured>) (t => (BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured) t.To<CABatchStatus.exported>().IsTriggeredOn((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.export)).WithFieldAssignments((Action<BoundedTo<CABatchEntry, CABatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<CABatch.exported>((Func<BoundedTo<CABatchEntry, CABatch>.Assignment.INeedRightOperand, BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured>) (f => (BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<CABatchEntry, CABatch>.Transition.INeedTarget, BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured>) (t => (BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured) t.To<CABatchStatus.released>().IsTriggeredOn((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.release))));
      }));
      transitions.AddGroupFrom<CABatchStatus.exported>((Action<BoundedTo<CABatchEntry, CABatch>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CABatchEntry, CABatch>.Transition.INeedTarget, BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured>) (t => (BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured) t.To<CABatchStatus.balanced>().IsTriggeredOn((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.setBalanced)).WithFieldAssignments((Action<BoundedTo<CABatchEntry, CABatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<CABatch.exported>((Func<BoundedTo<CABatchEntry, CABatch>.Assignment.INeedRightOperand, BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured>) (f => (BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<CABatchEntry, CABatch>.Transition.INeedTarget, BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured>) (t => (BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured) t.To<CABatchStatus.canceled>().IsTriggeredOn((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.cancelBatch))));
      }));
      transitions.AddGroupFrom<CABatchStatus.released>((Action<BoundedTo<CABatchEntry, CABatch>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<CABatchEntry, CABatch>.Transition.INeedTarget, BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured>) (t => (BoundedTo<CABatchEntry, CABatch>.Transition.IConfigured) t.To<CABatchStatus.voided>().IsTriggeredOn((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.voidBatch))))));
    })))).WithActions((Action<BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.initializeState), (Func<BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured>) (a => (BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.releaseFromHold), (Func<BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured>) (c => (BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<CABatchEntry, CABatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<CABatch.hold>((Func<BoundedTo<CABatchEntry, CABatch>.Assignment.INeedRightOperand, BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured>) (f => (BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.setBalanced), (Func<BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured>) (c => (BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<CABatchEntry, CABatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<CABatch.exported>((Func<BoundedTo<CABatchEntry, CABatch>.Assignment.INeedRightOperand, BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured>) (f => (BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.putOnHold), (Func<BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured>) (c => (BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<CABatchEntry, CABatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<CABatch.hold>((Func<BoundedTo<CABatchEntry, CABatch>.Assignment.INeedRightOperand, BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured>) (f => (BoundedTo<CABatchEntry, CABatch>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.export), (Func<BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.InFolder(processingCategory);
        BoundedTo<CABatchEntry, CABatch>.Condition skipExport = conditions.SkipExport;
        BoundedTo<CABatchEntry, CABatch>.Condition condition8 = BoundedTo<CABatchEntry, CABatch>.Condition.op_False(skipExport) ? skipExport : BoundedTo<CABatchEntry, CABatch>.Condition.op_BitwiseAnd(skipExport, BoundedTo<CABatchEntry, CABatch>.Condition.op_LogicalNot(conditions.IsReleased));
        BoundedTo<CABatchEntry, CABatch>.Condition condition9 = BoundedTo<CABatchEntry, CABatch>.Condition.op_False(condition8) ? condition8 : BoundedTo<CABatchEntry, CABatch>.Condition.op_BitwiseAnd(condition8, BoundedTo<CABatchEntry, CABatch>.Condition.op_LogicalNot(conditions.IsCanceled));
        BoundedTo<CABatchEntry, CABatch>.Condition condition10 = BoundedTo<CABatchEntry, CABatch>.Condition.op_False(condition9) ? condition9 : BoundedTo<CABatchEntry, CABatch>.Condition.op_BitwiseAnd(condition9, BoundedTo<CABatchEntry, CABatch>.Condition.op_LogicalNot(conditions.IsVoided));
        return (BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<CABatchEntry, CABatch>.ISharedCondition) condition10);
      }));
      actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.release), (Func<BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.InFolder(processingCategory);
        BoundedTo<CABatchEntry, CABatch>.Condition condition11 = BoundedTo<CABatchEntry, CABatch>.Condition.op_LogicalNot(conditions.SkipExport);
        BoundedTo<CABatchEntry, CABatch>.Condition condition12 = BoundedTo<CABatchEntry, CABatch>.Condition.op_False(condition11) ? condition11 : BoundedTo<CABatchEntry, CABatch>.Condition.op_BitwiseAnd(condition11, BoundedTo<CABatchEntry, CABatch>.Condition.op_LogicalNot(conditions.IsExported));
        BoundedTo<CABatchEntry, CABatch>.Condition condition13 = BoundedTo<CABatchEntry, CABatch>.Condition.op_False(condition12) ? condition12 : BoundedTo<CABatchEntry, CABatch>.Condition.op_BitwiseAnd(condition12, BoundedTo<CABatchEntry, CABatch>.Condition.op_LogicalNot(conditions.IsCanceled));
        BoundedTo<CABatchEntry, CABatch>.Condition condition14 = BoundedTo<CABatchEntry, CABatch>.Condition.op_False(condition13) ? condition13 : BoundedTo<CABatchEntry, CABatch>.Condition.op_BitwiseAnd(condition13, BoundedTo<CABatchEntry, CABatch>.Condition.op_LogicalNot(conditions.IsVoided));
        return (BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<CABatchEntry, CABatch>.ISharedCondition) condition14);
      }));
      actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.cancelBatch), (Func<BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured>) (c => (BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory).IsDisabledWhen((BoundedTo<CABatchEntry, CABatch>.ISharedCondition) conditions.SkipExport)));
      actions.Add((Expression<Func<CABatchEntry, PXAction<CABatch>>>) (g => g.voidBatch), (Func<BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured>) (c => (BoundedTo<CABatchEntry, CABatch>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory)));
    }))));
  }

  public class Conditions : BoundedTo<CABatchEntry, CABatch>.Condition.Pack
  {
    public BoundedTo<CABatchEntry, CABatch>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CABatchEntry, CABatch>.Condition.ConditionBuilder, BoundedTo<CABatchEntry, CABatch>.Condition>) (c => c.FromBql<BqlOperand<CABatch.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<CABatchEntry, CABatch>.Condition IsReleased
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CABatchEntry, CABatch>.Condition.ConditionBuilder, BoundedTo<CABatchEntry, CABatch>.Condition>) (c => c.FromBql<BqlOperand<CABatch.released, IBqlBool>.IsEqual<True>>()), nameof (IsReleased));
      }
    }

    public BoundedTo<CABatchEntry, CABatch>.Condition IsExported
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CABatchEntry, CABatch>.Condition.ConditionBuilder, BoundedTo<CABatchEntry, CABatch>.Condition>) (c => c.FromBql<BqlOperand<CABatch.exported, IBqlBool>.IsEqual<True>>()), nameof (IsExported));
      }
    }

    public BoundedTo<CABatchEntry, CABatch>.Condition SkipExport
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CABatchEntry, CABatch>.Condition.ConditionBuilder, BoundedTo<CABatchEntry, CABatch>.Condition>) (c => c.FromBql<BqlOperand<CABatch.skipExport, IBqlBool>.IsEqual<True>>()), nameof (SkipExport));
      }
    }

    public BoundedTo<CABatchEntry, CABatch>.Condition IsCanceled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CABatchEntry, CABatch>.Condition.ConditionBuilder, BoundedTo<CABatchEntry, CABatch>.Condition>) (c => c.FromBql<BqlOperand<CABatch.canceled, IBqlBool>.IsEqual<True>>()), nameof (IsCanceled));
      }
    }

    public BoundedTo<CABatchEntry, CABatch>.Condition IsVoided
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CABatchEntry, CABatch>.Condition.ConditionBuilder, BoundedTo<CABatchEntry, CABatch>.Condition>) (c => c.FromBql<BqlOperand<CABatch.voided, IBqlBool>.IsEqual<True>>()), nameof (IsVoided));
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
