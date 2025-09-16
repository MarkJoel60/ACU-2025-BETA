// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProformaEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CN.ProjectAccounting.PM.GraphExtensions;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PM;

public class ProformaEntry_ApprovalWorkflow : PXGraphExtension<ProformaEntry_Workflow, ProformaEntry>
{
  public PXAction<PMProforma> approve;
  public PXAction<PMProforma> reject;
  public PXAction<PMProforma> reassignApproval;

  protected static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && ProformaEntry_ApprovalWorkflow.ProformaSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (PMSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    ProformaEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<ProformaEntry, PMProforma>());
  }

  protected static void Configure(WorkflowContext<ProformaEntry, PMProforma> context)
  {
    var conditions = new
    {
      IsRejected = Bql<BqlOperand<PMProforma.rejected, IBqlBool>.IsEqual<True>>(),
      IsNotRejected = Bql<BqlOperand<PMProforma.rejected, IBqlBool>.IsNotEqual<True>>(),
      IsApproved = Bql<BqlOperand<PMProforma.approved, IBqlBool>.IsEqual<True>>(),
      IsNotApproved = Bql<BqlOperand<PMProforma.approved, IBqlBool>.IsEqual<False>>(),
      IsApprovalDisabled = (ProformaEntry_ApprovalWorkflow.ApprovalIsActive() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : Bql<BqlOperand<PMProforma.status, IBqlString>.IsNotIn<ProformaStatus.pendingApproval, ProformaStatus.rejected>>())
    }.AutoNameConditions();
    BoundedTo<ProformaEntry, PMProforma>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<ProformaEntry, PMProforma>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProformaEntry, PMProforma>.ActionCategory.IConfigured>) (category => (BoundedTo<ProformaEntry, PMProforma>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<ProformaEntry_ApprovalWorkflow>((Expression<Func<ProformaEntry_ApprovalWorkflow, PXAction<PMProforma>>>) (g => g.approve), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.removeHold)).PlaceAfter((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.removeHold)).IsHiddenWhen((BoundedTo<ProformaEntry, PMProforma>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ProformaEntry, PMProforma>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMProforma.approved>((Func<BoundedTo<ProformaEntry, PMProforma>.Assignment.INeedRightOperand, BoundedTo<ProformaEntry, PMProforma>.Assignment.IConfigured>) (e => (BoundedTo<ProformaEntry, PMProforma>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<ProformaEntry_ApprovalWorkflow>((Expression<Func<ProformaEntry_ApprovalWorkflow, PXAction<PMProforma>>>) (g => g.reject), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<ProformaEntry, PMProforma>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ProformaEntry, PMProforma>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMProforma.rejected>((Func<BoundedTo<ProformaEntry, PMProforma>.Assignment.INeedRightOperand, BoundedTo<ProformaEntry, PMProforma>.Assignment.IConfigured>) (e => (BoundedTo<ProformaEntry, PMProforma>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<ProformaEntry, PMProforma>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ProformaEntry, PMProforma>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ProformaEntry, PMProforma>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ProformaEntry, PMProforma>.Workflow.ConfiguratorFlow, BoundedTo<ProformaEntry, PMProforma>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ProformaEntry, PMProforma>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<ProformaStatus.pendingApproval>((Func<BoundedTo<ProformaEntry, PMProforma>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProformaEntry, PMProforma>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProformaEntry, PMProforma>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProformaEntry, PMProforma>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) (c => (BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) (c => (BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.hold), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.send), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) null);
        actions.Add<ProformaEntryExt>((Expression<Func<ProformaEntryExt, PXAction<PMProforma>>>) (g => g.aia), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) (c => (BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      }))));
      fss.Add<ProformaStatus.rejected>((Func<BoundedTo<ProformaEntry, PMProforma>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProformaEntry, PMProforma>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProformaEntry, PMProforma>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProformaEntry, PMProforma>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.hold), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) (c => (BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.send), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) null);
        actions.Add<ProformaEntryExt>((Expression<Func<ProformaEntryExt, PXAction<PMProforma>>>) (g => g.aia), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionState.IAllowOptionalConfig, BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured>) (c => (BoundedTo<ProformaEntry, PMProforma>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      }))));
    })).WithTransitions((Action<BoundedTo<ProformaEntry, PMProforma>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<ProformaStatus.onHold>((Action<BoundedTo<ProformaEntry, PMProforma>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<ProformaEntry, PMProforma>.Transition.INeedTarget, BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured>) (t => (BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured) t.To<ProformaStatus.open>().IsTriggeredOn((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.removeHold))), (Func<BoundedTo<ProformaEntry, PMProforma>.Transition.ConfiguratorTransition, BoundedTo<ProformaEntry, PMProforma>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<ProformaEntry, PMProforma>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ProformaEntry, PMProforma>.Transition.INeedTarget, BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured>) (t => (BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured) t.To<ProformaStatus.rejected>().IsTriggeredOn((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.removeHold)).When((BoundedTo<ProformaEntry, PMProforma>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ProformaEntry, PMProforma>.Transition.INeedTarget, BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured>) (t => (BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured) t.To<ProformaStatus.pendingApproval>().IsTriggeredOn((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.removeHold)).When((BoundedTo<ProformaEntry, PMProforma>.ISharedCondition) conditions.IsNotRejected).When((BoundedTo<ProformaEntry, PMProforma>.ISharedCondition) conditions.IsNotApproved)));
      }));
      transitions.AddGroupFrom<ProformaStatus.pendingApproval>((Action<BoundedTo<ProformaEntry, PMProforma>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ProformaEntry, PMProforma>.Transition.INeedTarget, BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured>) (t => (BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured) t.To<ProformaStatus.open>().IsTriggeredOn(approve).When((BoundedTo<ProformaEntry, PMProforma>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ProformaEntry, PMProforma>.Transition.INeedTarget, BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured>) (t => (BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured) t.To<ProformaStatus.rejected>().IsTriggeredOn(reject).When((BoundedTo<ProformaEntry, PMProforma>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ProformaEntry, PMProforma>.Transition.INeedTarget, BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured>) (t => (BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured) t.To<ProformaStatus.onHold>().IsTriggeredOn((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.hold))));
      }));
      transitions.AddGroupFrom<ProformaStatus.rejected>((Action<BoundedTo<ProformaEntry, PMProforma>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ProformaEntry, PMProforma>.Transition.INeedTarget, BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured>) (t => (BoundedTo<ProformaEntry, PMProforma>.Transition.IConfigured) t.To<ProformaStatus.onHold>().IsTriggeredOn((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.hold))))));
    })))).WithActions((Action<BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<ProformaEntry, PXAction<PMProforma>>>) (g => g.hold), (Func<BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.ConfiguratorAction, BoundedTo<ProformaEntry, PMProforma>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<ProformaEntry, PMProforma>.Assignment.FieldContainerAdjusterAssignment>) (fa =>
      {
        fa.Add<PMProforma.approved>((Func<BoundedTo<ProformaEntry, PMProforma>.Assignment.INeedRightOperand, BoundedTo<ProformaEntry, PMProforma>.Assignment.IConfigured>) (f => (BoundedTo<ProformaEntry, PMProforma>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<PMProforma.rejected>((Func<BoundedTo<ProformaEntry, PMProforma>.Assignment.INeedRightOperand, BoundedTo<ProformaEntry, PMProforma>.Assignment.IConfigured>) (f => (BoundedTo<ProformaEntry, PMProforma>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    })).WithCategories((Action<BoundedTo<ProformaEntry, PMProforma>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<ProformaEntry, PMProforma>.ActionCategory.ConfiguratorCategory, BoundedTo<ProformaEntry, PMProforma>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Corrections"))));
    }))));

    BoundedTo<ProformaEntry, PMProforma>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable Approve(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable Reject(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReassignApproval(PXAdapter adapter)
  {
    return ((PXGraphExtension<ProformaEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class ProformaSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<ProformaEntry_ApprovalWorkflow.ProformaSetupApproval>(nameof (ProformaSetupApproval), new Type[1]
        {
          typeof (PMSetup)
        }).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PMSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<PMSetup.proformaApprovalMapID>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }
}
