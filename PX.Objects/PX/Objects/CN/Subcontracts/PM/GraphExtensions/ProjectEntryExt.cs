// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PM.GraphExtensions.ProjectEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Objects.PO;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PM.GraphExtensions;

public class ProjectEntryExt : PXGraphExtension<ProjectEntry>
{
  public PXAction<PMProject> createSubcontract;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public virtual void Initialize() => this.AddSubcontractType();

  [PXOverride]
  public virtual IEnumerable ViewPurchaseOrder(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> baseHandler)
  {
    POOrder current = ((PXSelectBase<POOrder>) this.Base.PurchaseOrders).Current;
    if (current.OrderType == "RS")
      ProjectEntryExt.RedirectToSubcontractEntry(current);
    return baseHandler(adapter);
  }

  public virtual bool CreateSubcontractVisible()
  {
    return this.Base.CostCommitmentTrackingEnabled() && !this.Base.IsUserNumberingOn("SUBCONTR");
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProject> e)
  {
    ((PXAction) this.createSubcontract).SetVisible(this.CreateSubcontractVisible());
    ((PXAction) this.createSubcontract).SetEnabled(this.CreateSubcontractVisible() && ProjectEntry.IsProjectEditable(e.Row));
  }

  private static void RedirectToSubcontractEntry(POOrder commitment)
  {
    SubcontractEntry instance = PXGraph.CreateInstance<SubcontractEntry>();
    ((PXSelectBase<POOrder>) instance.Document).Current = commitment;
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "Subcontract");
  }

  private void AddSubcontractType()
  {
    PXStringListAttribute.AppendList<POOrder.orderType>(((PXSelectBase) this.Base.PurchaseOrders).Cache, (object) null, "RS".CreateArray<string>(), "Subcontract".CreateArray<string>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CreateSubcontract(PXAdapter adapter)
  {
    return this.Base.CreatePOOrderBase<SubcontractEntry>(adapter, "Create Subcontract");
  }
}
