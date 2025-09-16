// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.GraphExtensions.PoSetupMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.PO.CacheExtensions;
using PX.Objects.CN.Subcontracts.PO.DAC;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PO.GraphExtensions;

public class PoSetupMaintExt : PXGraphExtension<POSetupMaint>
{
  public PXFilter<PurchaseOrderTypeFilter> Filter;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.ApplyBaseTypeFiltering();
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<EPAssignmentMap.graphType, Equal<Current<PurchaseOrderTypeFilter.graph>>>), "Invalid assignment map", new Type[] {})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POSetupApproval.assignmentMapID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<POSetup.orderRequestApproval> args)
  {
    this.ChangeSetupApprovalStatuses(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<POSetup.orderRequestApproval>>) args).NewValue as bool?, false);
  }

  public virtual void POSetup_SubcontractRequestApproval_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    this.ChangeSetupApprovalStatuses(e.NewValue as bool?, true);
  }

  private void ChangeSetupApprovalStatuses(bool? isActive, bool forSubcontractsOnly)
  {
    foreach (POSetupApproval setupApproval in this.GetSetupApprovals(forSubcontractsOnly))
    {
      setupApproval.IsActive = isActive;
      ((PXSelectBase<POSetupApproval>) this.Base.SetupApproval).Update(setupApproval);
    }
  }

  private IEnumerable<POSetupApproval> GetSetupApprovals(bool forSubcontractsOnly)
  {
    return (forSubcontractsOnly ? (PXSelectBase<POSetupApproval>) (object) new PXSelect<POSetupApproval, Where<POSetupApproval.orderType, Equal<POOrderType.regularSubcontract>>>((PXGraph) this.Base) : (PXSelectBase<POSetupApproval>) (object) new PXSelect<POSetupApproval, Where<POSetupApproval.orderType, NotEqual<POOrderType.regularSubcontract>>>((PXGraph) this.Base)).Select(Array.Empty<object>()).FirstTableItems;
  }

  private void ApplyBaseTypeFiltering()
  {
    if (this.IsSubcontractScreen())
    {
      ((PXSelectBase<POSetupApproval>) this.Base.SetupApproval).WhereAnd<Where<POSetupApproval.orderType, Equal<POOrderType.regularSubcontract>>>();
      ((PXSelectBase<PurchaseOrderTypeFilter>) this.Filter).Current.Graph = typeof (SubcontractEntry).FullName;
    }
    else
    {
      ((PXSelectBase<POSetupApproval>) this.Base.SetupApproval).WhereAnd<Where<POSetupApproval.orderType, NotEqual<POOrderType.regularSubcontract>>>();
      PXDefaultAttribute.SetPersistingCheck<PoSetupExt.subcontractNumberingID>(((PXSelectBase) this.Base.Setup).Cache, (object) null, (PXPersistingCheck) 2);
      ((PXSelectBase<PurchaseOrderTypeFilter>) this.Filter).Current.Graph = typeof (POOrderEntry).FullName;
    }
  }

  private bool IsSubcontractScreen()
  {
    return ((object) this.Base).GetType() == typeof (SubcontractSetupMaint);
  }
}
