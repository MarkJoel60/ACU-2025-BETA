// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TXInvoiceEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.TX;

public class TXInvoiceEntry_Workflow : PXGraphExtension<TXInvoiceEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    TXInvoiceEntry_Workflow.Configure(config.GetScreenConfigurationContext<TXInvoiceEntry, APInvoice>());
  }

  protected static void Configure(WorkflowContext<TXInvoiceEntry, APInvoice> context)
  {
    TXInvoiceEntry_Workflow.Conditions conditions = context.Conditions.GetPack<TXInvoiceEntry_Workflow.Conditions>();
    BoundedTo<TXInvoiceEntry, APInvoice>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<TXInvoiceEntry, APInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TXInvoiceEntry, APInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<TXInvoiceEntry, APInvoice>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    context.AddScreenConfigurationFor((Func<BoundedTo<TXInvoiceEntry, APInvoice>.ScreenConfiguration.IStartConfigScreen, BoundedTo<TXInvoiceEntry, APInvoice>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<TXInvoiceEntry, APInvoice>.ScreenConfiguration.IConfigured) ((BoundedTo<TXInvoiceEntry, APInvoice>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<APInvoice.status>().AddDefaultFlow((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Workflow.INeedStatesFlow, BoundedTo<TXInvoiceEntry, APInvoice>.Workflow.IConfigured>) (flow => (BoundedTo<TXInvoiceEntry, APInvoice>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<TXInvoiceEntry, APInvoice>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<TXInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TXInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TXInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<TXInvoiceEntry, PXAutoAction<APInvoice>>>) (g => g.initializeState))));
      fss.Add<APDocStatus.hold>((Func<BoundedTo<TXInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TXInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TXInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<TXInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.releaseFromHold), (Func<BoundedTo<TXInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<TXInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<TXInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))));
      fss.Add<APDocStatus.balanced>((Func<BoundedTo<TXInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TXInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TXInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<TXInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.release), (Func<BoundedTo<TXInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<TXInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<TXInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<TXInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<TXInvoiceEntry, APInvoice>.ActionState.IConfigured>) null);
      }))));
      fss.Add<APDocStatus.open>((Func<BoundedTo<TXInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TXInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) null);
      fss.Add<APDocStatus.closed>((Func<BoundedTo<TXInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TXInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) null);
    })).WithTransitions((Action<BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<TXInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.hold>().IsTriggeredOn((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.initializeState)).When((BoundedTo<TXInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.balanced>().IsTriggeredOn((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.initializeState)).When((BoundedTo<TXInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsNotOnHold)));
      }));
      transitions.AddGroupFrom<APDocStatus.hold>((Action<BoundedTo<TXInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.balanced>().IsTriggeredOn((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<APInvoice.hold>((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IConfigured>) (f => (BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IConfigured) f.SetFromValue((object) false)))))))));
      transitions.AddGroupFrom<APDocStatus.balanced>((Action<BoundedTo<TXInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.hold>().IsTriggeredOn((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<APInvoice.hold>((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IConfigured>) (f => (BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.release)).When((BoundedTo<TXInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsOpen)));
        ts.Add((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<TXInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.release)).When((BoundedTo<TXInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsClosed)));
      }));
    })))).WithActions((Action<BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.initializeState), (Func<BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (a => (BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.releaseFromHold), (Func<BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<APInvoice.hold>((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IConfigured>) (f => (BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<APInvoice.hold>((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IConfigured>) (f => (BoundedTo<TXInvoiceEntry, APInvoice>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.release), (Func<BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments), (Func<BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.InFolder((FolderType) 1).IsHiddenAlways()));
      actions.Add((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPEdit), (Func<BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.InFolder((FolderType) 2).IsHiddenAlways()));
      actions.Add((Expression<Func<TXInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPRegister), (Func<BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<TXInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.InFolder((FolderType) 2).IsHiddenAlways()));
    })).WithCategories((Action<BoundedTo<TXInvoiceEntry, APInvoice>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(processingCategory)))));
  }

  public class Conditions : BoundedTo<TXInvoiceEntry, APInvoice>.Condition.Pack
  {
    public BoundedTo<TXInvoiceEntry, APInvoice>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<TXInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlOperand<APInvoice.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<TXInvoiceEntry, APInvoice>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<TXInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlOperand<APInvoice.hold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
      }
    }

    public BoundedTo<TXInvoiceEntry, APInvoice>.Condition IsOpen
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<TXInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APInvoice.openDoc, Equal<True>>>>>.And<BqlOperand<APInvoice.released, IBqlBool>.IsEqual<True>>>()), nameof (IsOpen));
      }
    }

    public BoundedTo<TXInvoiceEntry, APInvoice>.Condition IsClosed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TXInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<TXInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APInvoice.openDoc, Equal<False>>>>>.And<BqlOperand<APInvoice.released, IBqlBool>.IsEqual<True>>>()), nameof (IsClosed));
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
