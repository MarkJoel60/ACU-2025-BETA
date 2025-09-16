// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoiceEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOInvoiceEntry_ApprovalWorkflow : 
  InvoiceEntry_ApprovalWorkflow<SOInvoiceEntry, SOInvoiceEntry_Workflow, SOInvoiceEntry_ApprovalWorkflow.SOConditions>
{
  [PXWorkflowDependsOnType(new Type[] {typeof (SOSetupInvoiceApproval)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    InvoiceEntry_ApprovalWorkflow<SOInvoiceEntry, SOInvoiceEntry_Workflow, SOInvoiceEntry_ApprovalWorkflow.SOConditions>.ConfigureBase(config, (Func<WorkflowContext<SOInvoiceEntry, PX.Objects.AR.ARInvoice>, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IConfigured>) (ctx => CommonActionCategories.Get<SOInvoiceEntry, PX.Objects.AR.ARInvoice>(ctx).Approval));
  }

  public class SOConditions : 
    InvoiceEntry_ApprovalWorkflow<SOInvoiceEntry, SOInvoiceEntry_Workflow, SOInvoiceEntry_ApprovalWorkflow.SOConditions>.Conditions
  {
    public override BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<Not<EPApprovalSettings<SOSetupInvoiceApproval, SOSetupInvoiceApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.IsDocumentApprovable<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.status>>>()), nameof (IsApprovalDisabled));
      }
    }

    public override BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition NonEditable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<EPApprovalSettings<SOSetupInvoiceApproval, SOSetupInvoiceApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.IsDocumentLockedByApproval<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.status>>()), nameof (NonEditable));
      }
    }
  }
}
