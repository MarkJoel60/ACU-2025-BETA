// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.GraphExtensions.PoOrderEntrySubcontractExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PO.GraphExtensions;

public class PoOrderEntrySubcontractExtension : 
  PXGraphExtension<POOrderEntry_ChangeOrderExt, POOrderEntryExt, POOrderEntry>
{
  public virtual void Initialize() => this.SetChangeOrdersAvailability();

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POOrder, POOrder.projectID> args,
    PXFieldVerifying baseMethod)
  {
    if (this.IsSubcontractScreen())
    {
      int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POOrder, POOrder.projectID>, POOrder, object>) args)?.NewValue;
      if (!this.NeedToVerifyProjectId(newValue))
        return;
      this.VerifyProjectLockCommitments(newValue);
    }
    else
      baseMethod.Invoke(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<POOrder, POOrder.projectID>>) args).Cache, ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<POOrder, POOrder.projectID>>) args).Args);
  }

  private bool NeedToVerifyProjectId(int? projectId)
  {
    return projectId.HasValue && PXAccess.FeatureInstalled<FeaturesSet.changeOrder>() && !((PXGraphExtension<POOrderEntryExt, POOrderEntry>) this).Base1.SkipProjectLockCommitmentsVerification;
  }

  private void VerifyProjectLockCommitments(int? projectId)
  {
    PMProject project = this.GetProject(projectId);
    if (project != null && project.LockCommitments.GetValueOrDefault())
      throw new PXSetPropertyException("To be able to create original Subcontract commitments for this project, perform the Unlock Commitments action for the project on the Projects (PM301000) form.")
      {
        ErrorValue = (object) project.ContractCD
      };
  }

  private PMProject GetProject(int? projectId)
  {
    PMProject project;
    ProjectDefaultAttribute.IsProject((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, projectId, out project);
    return project;
  }

  private void SetChangeOrdersAvailability()
  {
    ((PXSelectBase) this.Base2.ChangeOrderDetails).AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.changeOrder>();
  }

  private bool IsSubcontractScreen()
  {
    return ((object) ((PXGraphExtension<POOrderEntry>) this).Base).GetType() == typeof (SubcontractEntry) || ((object) ((PXGraphExtension<POOrderEntry>) this).Base).GetType().BaseType == typeof (SubcontractEntry);
  }
}
