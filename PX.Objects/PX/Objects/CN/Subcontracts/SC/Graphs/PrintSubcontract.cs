// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SC.Graphs.PrintSubcontract
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CN.Common.Helpers;
using PX.Objects.CS;
using PX.Objects.PO;
using System.Collections;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SC.Graphs;

public class PrintSubcontract : POPrintOrder
{
  public PXAction<POPrintOrderFilter> ViewSubcontractDetails;

  public PrintSubcontract()
  {
    FeaturesSetHelper.CheckConstructionFeature();
    ((PXSelectBase<POPrintOrder.POPrintOrderOwned>) this.Records).WhereAnd<Where<BqlOperand<POPrintOrder.POPrintOrderOwned.orderType, IBqlString>.IsEqual<POOrderType.regularSubcontract>>>();
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Subcontract Nbr.")]
  public virtual void _(
    Events.CacheAttached<POPrintOrder.POPrintOrderOwned.orderNbr> e)
  {
  }

  [PXUIField]
  [PXEditDetailButton]
  public override IEnumerable Details(PXAdapter adapter)
  {
    if (((PXSelectBase<POPrintOrder.POPrintOrderOwned>) this.Records).Current != null && ((PXSelectBase<POPrintOrderFilter>) this.Filter).Current != null)
      this.OpenSubcontractDetails();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void viewSubcontractDetails()
  {
    if (((PXSelectBase<POPrintOrder.POPrintOrderOwned>) this.Records).Current == null)
      return;
    this.OpenSubcontractDetails();
  }

  private void OpenSubcontractDetails()
  {
    SubcontractEntry instance = PXGraph.CreateInstance<SubcontractEntry>();
    ((PXSelectBase<POOrder>) instance.Document).Current = (POOrder) ((PXSelectBase<POPrintOrder.POPrintOrderOwned>) this.Records).Current;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Subcontract");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  public override bool IsPrintingAllowed(POPrintOrderFilter filter)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() && filter?.Action == "SC301000$printSubcontract";
  }
}
