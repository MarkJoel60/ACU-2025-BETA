// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPriceWorksheetMaint_Workflow
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
namespace PX.Objects.AR;

public class ARPriceWorksheetMaint_Workflow : PXGraphExtension<ARPriceWorksheetMaint>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    ARPriceWorksheetMaint_Workflow.Configure(config.GetScreenConfigurationContext<ARPriceWorksheetMaint, ARPriceWorksheet>());
  }

  protected static void Configure(
    WorkflowContext<ARPriceWorksheetMaint, ARPriceWorksheet> context)
  {
    ARPriceWorksheetMaint_Workflow.Conditions conditions = context.Conditions.GetPack<ARPriceWorksheetMaint_Workflow.Conditions>();
    BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("ProcessingID", (Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionCategory.IConfigured>) (category => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionCategory.IConfigured otherCategory = context.Categories.CreateNew("OtherID", (Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionCategory.IConfigured>) (category => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ScreenConfiguration.IConfigured) ((BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<ARPriceWorksheet.status>().AddDefaultFlow((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Workflow.INeedStatesFlow, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Workflow.IConfigured>) (flow => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<ARPriceWorksheetMaint, PXAutoAction<ARPriceWorksheet>>>) (g => g.initializeState))));
      fss.Add<SPWorksheetStatus.hold>((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ARPriceWorksheetMaint, PXAction<ARPriceWorksheet>>>) (g => g.releaseFromHold), (Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))));
      fss.Add<SPWorksheetStatus.open>((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARPriceWorksheetMaint, PXAction<ARPriceWorksheet>>>) (g => g.ReleasePriceWorksheet), (Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionState.IConfigured>) (a => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ARPriceWorksheetMaint, PXAction<ARPriceWorksheet>>>) (g => g.putOnHold), (Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionState.IConfigured>) null);
      }))));
      fss.Add<SPWorksheetStatus.released>((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.BaseFlowStep.IConfigured>) null);
    })).WithTransitions((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.INeedTarget, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.IConfigured>) (t => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.IConfigured) t.To<SPWorksheetStatus.hold>().IsTriggeredOn((Expression<Func<ARPriceWorksheetMaint, PXAction<ARPriceWorksheet>>>) (g => g.initializeState)).When((BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ISharedCondition) conditions.IsOnHold)))));
      transitions.AddGroupFrom<SPWorksheetStatus.hold>((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.INeedTarget, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.IConfigured>) (t => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.IConfigured) t.To<SPWorksheetStatus.open>().IsTriggeredOn((Expression<Func<ARPriceWorksheetMaint, PXAction<ARPriceWorksheet>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARPriceWorksheet.hold>((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.INeedRightOperand, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IConfigured>) (f => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IConfigured) f.SetFromValue((object) false)))))))));
      transitions.AddGroupFrom<SPWorksheetStatus.open>((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.INeedTarget, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.IConfigured>) (t => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.IConfigured) t.To<SPWorksheetStatus.hold>().IsTriggeredOn((Expression<Func<ARPriceWorksheetMaint, PXAction<ARPriceWorksheet>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARPriceWorksheet.hold>((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.INeedRightOperand, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IConfigured>) (f => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.INeedTarget, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.IConfigured>) (t => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Transition.IConfigured) t.To<SPWorksheetStatus.released>().IsTriggeredOn((Expression<Func<ARPriceWorksheetMaint, PXAction<ARPriceWorksheet>>>) (g => g.ReleasePriceWorksheet))));
      }));
    })))).WithActions((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ARPriceWorksheetMaint, PXAction<ARPriceWorksheet>>>) (g => g.initializeState), (Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<ARPriceWorksheetMaint, PXAction<ARPriceWorksheet>>>) (g => g.releaseFromHold), (Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARPriceWorksheet.hold>((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.INeedRightOperand, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IConfigured>) (f => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<ARPriceWorksheetMaint, PXAction<ARPriceWorksheet>>>) (g => g.putOnHold), (Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARPriceWorksheet.hold>((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.INeedRightOperand, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IConfigured>) (f => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<ARPriceWorksheetMaint, PXAction<ARPriceWorksheet>>>) (g => g.ReleasePriceWorksheet), (Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
    })).WithCategories((Action<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(otherCategory);
    }))));
  }

  public class Conditions : BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Condition.Pack
  {
    public BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Condition.ConditionBuilder, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Condition>) (c => c.FromBql<BqlOperand<ARPriceWorksheet.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Condition.ConditionBuilder, BoundedTo<ARPriceWorksheetMaint, ARPriceWorksheet>.Condition>) (c => c.FromBql<BqlOperand<ARPriceWorksheet.hold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
      }
    }
  }

  public static class CategoryNames
  {
    public const string Processing = "Processing";
    public const string Other = "Other";
  }

  public static class CategoryID
  {
    public const string Processing = "ProcessingID";
    public const string Other = "OtherID";
  }
}
