// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.AP.GraphExtensions.APInvoiceEntryExt_Workflow
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
namespace PX.Objects.CN.ProjectAccounting.AP.GraphExtensions;

public class APInvoiceEntryExt_Workflow : PXGraphExtension<APInvoiceEntry_Workflow, APInvoiceEntry>
{
  protected static bool ReclassificationIsActive()
  {
    return APInvoiceEntryExt_Workflow.APSetupReclassification.IsActive;
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    APInvoiceEntryExt_Workflow.Configure(config.GetScreenConfigurationContext<APInvoiceEntry, APInvoice>());
  }

  protected static void Configure(WorkflowContext<APInvoiceEntry, APInvoice> context)
  {
    APInvoiceEntryExt_Workflow.Conditions conditions = context.Conditions.GetPack<APInvoiceEntryExt_Workflow.Conditions>();
    context.UpdateScreenConfigurationFor((Func<BoundedTo<APInvoiceEntry, APInvoice>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<APInvoiceEntry, APInvoice>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<APInvoiceEntry, APInvoice>.Workflow.ConfiguratorFlow, BoundedTo<APInvoiceEntry, APInvoice>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Update<APDocStatus.open>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.ConfiguratorState, BoundedTo<APInvoiceEntry, APInvoice>.FlowState.ConfiguratorState>) (flowState => flowState.WithActions((Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.ContainerAdjusterActions>) (actions => actions.Add<APInvoiceEntryReclassifyingExt>((Expression<Func<APInvoiceEntryReclassifyingExt, PXAction<APInvoice>>>) (g => g.reclassify), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) null)))));
      fss.Update<APDocStatus.closed>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.ConfiguratorState, BoundedTo<APInvoiceEntry, APInvoice>.FlowState.ConfiguratorState>) (flowState => flowState.WithActions((Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.ContainerAdjusterActions>) (actions => actions.Add<APInvoiceEntryReclassifyingExt>((Expression<Func<APInvoiceEntryReclassifyingExt, PXAction<APInvoice>>>) (g => g.reclassify), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) null)))));
      fss.Add<APDocStatus.underReclassification>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.release), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
      }))).WithEventHandlers((Action<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnReleaseDocument))))));
    })).WithTransitions((Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<APDocStatus.open>((Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.underReclassification>().IsTriggeredOn<APInvoiceEntryReclassifyingExt>((Expression<Func<APInvoiceEntryReclassifyingExt, PXAction<APInvoice>>>) (g => g.reclassify))))));
      transitions.UpdateGroupFrom<APDocStatus.closed>((Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.underReclassification>().IsTriggeredOn<APInvoiceEntryReclassifyingExt>((Expression<Func<APInvoiceEntryReclassifyingExt, PXAction<APInvoice>>>) (g => g.reclassify))))));
      transitions.AddGroupFrom<APDocStatus.underReclassification>((Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When(context.Conditions.Get("IsOpen"))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When(context.Conditions.Get("IsClosed"))));
      }));
    })))).WithActions((Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add<APInvoiceEntryReclassifyingExt>((Expression<Func<APInvoiceEntryReclassifyingExt, PXAction<APInvoice>>>) (g => g.reclassify), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(context.Categories.Get("Corrections"));
        BoundedTo<APInvoiceEntry, APInvoice>.Condition reclassificationNotActive = conditions.IsReclassificationNotActive;
        BoundedTo<APInvoiceEntry, APInvoice>.Condition condition = BoundedTo<APInvoiceEntry, APInvoice>.Condition.op_True(reclassificationNotActive) ? reclassificationNotActive : BoundedTo<APInvoiceEntry, APInvoice>.Condition.op_BitwiseOr(reclassificationNotActive, conditions.IsRetainageDocument);
        return (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) condition);
      }));
      actions.Update((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reverseInvoice), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.ConfiguratorAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.ConfiguratorAction>) (a => a.PlaceAfterInCategory("reclassify")));
    }))));
  }

  private class APSetupReclassification : IPrefetchable, IPXCompanyDependent
  {
    private bool ReclassifyInvoices;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<APInvoiceEntryExt_Workflow.APSetupReclassification>(nameof (APSetupReclassification), new Type[1]
        {
          typeof (APSetup)
        }).ReclassifyInvoices;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<APSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<APSetupExt.reclassifyInvoices>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.ReclassifyInvoices = pxDataRecord.GetBoolean(0).GetValueOrDefault();
      }
    }
  }

  public class Conditions : BoundedTo<APInvoiceEntry, APInvoice>.Condition.Pack
  {
    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsReclassificationNotActive
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => !APInvoiceEntryExt_Workflow.ReclassificationIsActive() ? c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<True>>() : c.FromBql<BqlOperand<APInvoice.docType, IBqlString>.IsNotEqual<APDocType.invoice>>()), nameof (IsReclassificationNotActive));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsRetainageDocument
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlOperand<APInvoice.isRetainageDocument, IBqlBool>.IsEqual<True>>()), nameof (IsRetainageDocument));
      }
    }
  }
}
