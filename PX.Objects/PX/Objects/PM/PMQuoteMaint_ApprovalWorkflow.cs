// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteMaint_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PM;

public class PMQuoteMaint_ApprovalWorkflow : PXGraphExtension<PMQuoteMaint_Workflow, PMQuoteMaint>
{
  public PXAction<PMQuote> approve;
  public PXAction<PMQuote> reject;
  public PXAction<PMQuote> reassignApproval;

  public static bool IsActive() => true;

  protected static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && PMQuoteMaint_ApprovalWorkflow.PMQuoteSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new System.Type[] {typeof (PMSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    PMQuoteMaint_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<PMQuoteMaint, PMQuote>());
  }

  protected static void Configure(WorkflowContext<PMQuoteMaint, PMQuote> context)
  {
    var conditions = new
    {
      IsRejected = Bql<BqlOperand<PMQuote.rejected, IBqlBool>.IsEqual<True>>(),
      IsNotRejected = Bql<BqlOperand<PMQuote.rejected, IBqlBool>.IsNotEqual<True>>(),
      IsApproved = Bql<BqlOperand<PMQuote.approved, IBqlBool>.IsEqual<True>>(),
      IsNotApproved = Bql<BqlOperand<PMQuote.approved, IBqlBool>.IsEqual<False>>(),
      IsApprovalDisabled = (PMQuoteMaint_ApprovalWorkflow.ApprovalIsActive() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : Bql<BqlOperand<PMQuote.status, IBqlString>.IsNotIn<CRQuoteStatusAttribute.pendingApproval, CRQuoteStatusAttribute.rejected>>()),
      IsApprovalEnabledAndDraft = (PMQuoteMaint_ApprovalWorkflow.ApprovalIsActive() ? Bql<BqlOperand<PMQuote.status, IBqlString>.IsEqual<CRQuoteStatusAttribute.draft>>() : Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>())
    }.AutoNameConditions();
    BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured>) (category => (BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<PMQuoteMaint_ApprovalWorkflow>((Expression<Func<PMQuoteMaint_ApprovalWorkflow, PXAction<PMQuote>>>) (g => g.approve), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (a => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.submit)).PlaceAfter((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.submit)).IsHiddenWhen((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMQuote.approved>((Func<BoundedTo<PMQuoteMaint, PMQuote>.Assignment.INeedRightOperand, BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured>) (e => (BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<PMQuoteMaint_ApprovalWorkflow>((Expression<Func<PMQuoteMaint_ApprovalWorkflow, PXAction<PMQuote>>>) (g => g.reject), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (a => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMQuote.rejected>((Func<BoundedTo<PMQuoteMaint, PMQuote>.Assignment.INeedRightOperand, BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured>) (e => (BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (a => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<PMQuoteMaint, PMQuote>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<PMQuoteMaint, PMQuote>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<PMQuoteMaint, PMQuote>.Workflow.ConfiguratorFlow, BoundedTo<PMQuoteMaint, PMQuote>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<CRQuoteStatusAttribute.pendingApproval>((Func<BoundedTo<PMQuoteMaint, PMQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add(reassign, (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.copyQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.primaryQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
      }))));
      fss.Add<CRQuoteStatusAttribute.rejected>((Func<BoundedTo<PMQuoteMaint, PMQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.copyQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.primaryQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
      }))));
    })).WithTransitions((Action<BoundedTo<PMQuoteMaint, PMQuote>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<CRQuoteStatusAttribute.draft>((Action<BoundedTo<PMQuoteMaint, PMQuote>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.approved>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.submit))), (Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.ConfiguratorTransition, BoundedTo<PMQuoteMaint, PMQuote>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.rejected>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.submit)).When((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.pendingApproval>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.submit)).When((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) conditions.IsNotRejected).When((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) conditions.IsNotApproved)));
        ts.Update((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.accepted>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.accept))), (Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.ConfiguratorTransition, BoundedTo<PMQuoteMaint, PMQuote>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) conditions.IsApprovalDisabled)));
      }));
      transitions.AddGroupFrom<CRQuoteStatusAttribute.pendingApproval>((Action<BoundedTo<PMQuoteMaint, PMQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.approved>().IsTriggeredOn(approve).When((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.rejected>().IsTriggeredOn(reject).When((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.draft>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote))));
      }));
      transitions.AddGroupFrom<CRQuoteStatusAttribute.rejected>((Action<BoundedTo<PMQuoteMaint, PMQuote>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.draft>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote))))));
    })))).WithActions((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.ConfiguratorAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<PMQuoteMaint, PMQuote>.Assignment.FieldContainerAdjusterAssignment>) (fa =>
      {
        fa.Add<PMQuote.approved>((Func<BoundedTo<PMQuoteMaint, PMQuote>.Assignment.INeedRightOperand, BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured>) (f => (BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<PMQuote.rejected>((Func<BoundedTo<PMQuoteMaint, PMQuote>.Assignment.INeedRightOperand, BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured>) (f => (BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
      actions.Update((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.accept), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.ConfiguratorAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.ConfiguratorAction>) (a => a.IsDisabledWhen((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) conditions.IsApprovalEnabledAndDraft)));
    })).WithCategories((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.ConfiguratorCategory, BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Processing"))));
    }))));

    BoundedTo<PMQuoteMaint, PMQuote>.Condition Bql<T>() where T : IBqlUnary, new()
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
    return ((PXGraphExtension<PMQuoteMaint>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class PMQuoteSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<PMQuoteMaint_ApprovalWorkflow.PMQuoteSetupApproval>(nameof (PMQuoteSetupApproval), new System.Type[1]
        {
          typeof (PMSetup)
        }).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PMSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<PMSetup.quoteApprovalMapID>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }
}
