// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntryImport_Workflow
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
namespace PX.Objects.GL;

public class JournalEntryImport_Workflow : PXGraphExtension<JournalEntryImport>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    JournalEntryImport_Workflow.Configure(config.GetScreenConfigurationContext<JournalEntryImport, GLTrialBalanceImportMap>());
  }

  protected static void Configure(
    WorkflowContext<JournalEntryImport, GLTrialBalanceImportMap> context)
  {
    JournalEntryImport_Workflow.Conditions conditions = context.Conditions.GetPack<JournalEntryImport_Workflow.Conditions>();
    BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionCategory.IConfigured>) (category => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    context.AddScreenConfigurationFor((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ScreenConfiguration.IStartConfigScreen, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ScreenConfiguration.IConfigured) ((BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<GLTrialBalanceImportMap.status>().AddDefaultFlow((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Workflow.INeedStatesFlow, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Workflow.IConfigured>) (flow => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<JournalEntryImport, PXAutoAction<GLTrialBalanceImportMap>>>) (g => g.initializeState))));
      fss.Add<TrialBalanceImportMapStatusAttribute.hold>((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<JournalEntryImport, PXAction<GLTrialBalanceImportMap>>>) (g => g.releaseFromHold), (Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))));
      fss.Add<TrialBalanceImportMapStatusAttribute.balanced>((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<JournalEntryImport, PXAction<GLTrialBalanceImportMap>>>) (g => g.release), (Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<JournalEntryImport, PXAction<GLTrialBalanceImportMap>>>) (g => g.putOnHold), (Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
      }))));
      fss.Add<TrialBalanceImportMapStatusAttribute.released>((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.BaseFlowStep.IConfigured>) null);
    })).WithTransitions((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.INeedTarget, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.IConfigured>) (t => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.IConfigured) t.To<TrialBalanceImportMapStatusAttribute.hold>().IsTriggeredOn((Expression<Func<JournalEntryImport, PXAction<GLTrialBalanceImportMap>>>) (g => g.initializeState)).When((BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ISharedCondition) conditions.IsOnHold)))));
      transitions.AddGroupFrom<TrialBalanceImportMapStatusAttribute.hold>((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.INeedTarget, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.IConfigured>) (t => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.IConfigured) t.To<TrialBalanceImportMapStatusAttribute.balanced>().IsTriggeredOn((Expression<Func<JournalEntryImport, PXAction<GLTrialBalanceImportMap>>>) (g => g.releaseFromHold)).DoesNotPersist().WithFieldAssignments((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IContainerFillerFields>) (fas => fas.Add<GLTrialBalanceImportMap.isHold>((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.INeedRightOperand, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IConfigured>) (f => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IConfigured) f.SetFromValue((object) false)))))))));
      transitions.AddGroupFrom<TrialBalanceImportMapStatusAttribute.balanced>((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.INeedTarget, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.IConfigured>) (t => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.IConfigured) t.To<TrialBalanceImportMapStatusAttribute.hold>().IsTriggeredOn((Expression<Func<JournalEntryImport, PXAction<GLTrialBalanceImportMap>>>) (g => g.putOnHold)).DoesNotPersist().WithFieldAssignments((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IContainerFillerFields>) (fas => fas.Add<GLTrialBalanceImportMap.isHold>((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.INeedRightOperand, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IConfigured>) (f => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.INeedTarget, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.IConfigured>) (t => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Transition.IConfigured) t.To<TrialBalanceImportMapStatusAttribute.released>().IsTriggeredOn((Expression<Func<JournalEntryImport, PXAction<GLTrialBalanceImportMap>>>) (g => g.release))));
      }));
    })))).WithActions((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<JournalEntryImport, PXAction<GLTrialBalanceImportMap>>>) (g => g.initializeState), (Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IConfigured>) (a => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<JournalEntryImport, PXAction<GLTrialBalanceImportMap>>>) (g => g.releaseFromHold), (Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IConfigured) c.InFolder(processingCategory).PlaceAfter("Last").WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IContainerFillerFields>) (fas => fas.Add<GLTrialBalanceImportMap.isHold>((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.INeedRightOperand, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IConfigured>) (f => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<JournalEntryImport, PXAction<GLTrialBalanceImportMap>>>) (g => g.putOnHold), (Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IContainerFillerFields>) (fas => fas.Add<GLTrialBalanceImportMap.isHold>((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.INeedRightOperand, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IConfigured>) (f => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<JournalEntryImport, PXAction<GLTrialBalanceImportMap>>>) (g => g.release), (Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
    })).WithCategories((Action<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(processingCategory)))));
  }

  public class Conditions : BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Condition.Pack
  {
    public BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Condition.ConditionBuilder, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Condition>) (c => c.FromBql<BqlOperand<GLTrialBalanceImportMap.isHold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Condition.ConditionBuilder, BoundedTo<JournalEntryImport, GLTrialBalanceImportMap>.Condition>) (c => c.FromBql<BqlOperand<GLTrialBalanceImportMap.isHold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
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
