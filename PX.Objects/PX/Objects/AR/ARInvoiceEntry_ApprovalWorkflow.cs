// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.EP;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

public class ARInvoiceEntry_ApprovalWorkflow : 
  InvoiceEntry_ApprovalWorkflow<ARInvoiceEntry, ARInvoiceEntry_Workflow, ARInvoiceEntry_ApprovalWorkflow.ARConditions>
{
  [PXWorkflowDependsOnType(new Type[] {typeof (ARSetupApproval)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    InvoiceEntry_ApprovalWorkflow<ARInvoiceEntry, ARInvoiceEntry_Workflow, ARInvoiceEntry_ApprovalWorkflow.ARConditions>.ConfigureBase(config, (Func<WorkflowContext<ARInvoiceEntry, ARInvoice>, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured>) (ctx => ctx.Categories.Get("ApprovalID")));
    config.GetScreenConfigurationContext<ARInvoiceEntry, ARInvoice>().UpdateScreenConfigurationFor((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ARInvoiceEntry, ARInvoice>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Workflow.ConfiguratorFlow, BoundedTo<ARInvoiceEntry, ARInvoice>.Workflow.ConfiguratorFlow>) (flow => flow.WithTransitions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.ContainerAdjusterTransitions>) (transitions => transitions.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedSource, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.From<ARDocStatus.pendingApproval>().To<ARDocStatus.scheduled>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnConfirmSchedule)).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fas =>
    {
      fas.Add<ARInvoice.scheduled>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)));
      fas.Add<ARInvoice.scheduleID>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromExpression("@ScheduleID")));
    }))))))))));
  }

  public class ARConditions : 
    InvoiceEntry_ApprovalWorkflow<ARInvoiceEntry, ARInvoiceEntry_Workflow, ARInvoiceEntry_ApprovalWorkflow.ARConditions>.Conditions
  {
    public override BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (b => b.FromBql<Not<EPApprovalSettings<ARSetupApproval, ARSetupApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.IsDocumentApprovable<ARInvoice.docType, ARInvoice.status>>>()), nameof (IsApprovalDisabled));
      }
    }

    public override BoundedTo<ARInvoiceEntry, ARInvoice>.Condition NonEditable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (b => b.FromBql<EPApprovalSettings<ARSetupApproval, ARSetupApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.IsDocumentLockedByApproval<ARInvoice.docType, ARInvoice.status>>()), nameof (NonEditable));
      }
    }
  }
}
