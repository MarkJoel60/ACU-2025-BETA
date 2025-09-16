// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POOrderEntry_ChangeOrderExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// This class implements graph extension to use change order extension
/// </summary>
public class POOrderEntry_ChangeOrderExt : ChangeOrderExt<POOrderEntry, POOrder>
{
  [PXFilterable(new Type[] {})]
  [PXViewName("Change Order")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PMChangeOrderLine, InnerJoin<PMChangeOrder, On<PMChangeOrderLine.refNbr, Equal<PMChangeOrder.refNbr>>>, Where<PMChangeOrderLine.pOOrderType, Equal<Current<POOrder.orderType>>, And<PMChangeOrderLine.pOOrderNbr, Equal<Current<POOrder.orderNbr>>>>> ChangeOrderDetails;
  [PXViewName("Change Order")]
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMChangeOrder, Where<PMChangeOrder.refNbr, Equal<Current<PMChangeOrderLine.refNbr>>>> ChangeOrders;

  public new static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.changeOrder>();

  public override PXSelectBase<PMChangeOrder> ChangeOrder
  {
    get => (PXSelectBase<PMChangeOrder>) this.ChangeOrders;
  }

  public override PMChangeOrder CurrentChangeOrder
  {
    get
    {
      return PXResultset<PMChangeOrder>.op_Implicit(((PXSelectBase<PMChangeOrder>) this.ChangeOrders).Select(new object[1]
      {
        (object) ((PXSelectBase<PMChangeOrderLine>) this.ChangeOrderDetails).Current.RefNbr
      }));
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<POOrder> e)
  {
    ((PXSelectBase) this.ChangeOrderDetails).Cache.AllowSelect = this.IsCommitmentsEnabled();
  }

  protected virtual void _(PX.Data.Events.RowDeleting<POOrder> e)
  {
    POOrder row = e.Row;
    if (!row.Hold.GetValueOrDefault() && row.Behavior == "C")
      throw new PXException("The purchase order cannot be removed: change order workflow has been enabled for the document because it contains lines related to projects with change order workflow enabled.");
  }

  private bool IsCommitmentsEnabled()
  {
    PMSetup current = ((PXSelectBase<PMSetup>) this.Setup).Current;
    return current != null && current.CostCommitmentTracking.GetValueOrDefault();
  }
}
