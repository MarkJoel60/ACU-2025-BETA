// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SpecialOrderCostCenterSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SpecialOrderCostCenterSupport : SpecialOrderCostCenterSupport<SOOrderEntry, PX.Objects.SO.SOLine>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  [PXMergeAttributes]
  [PXDBDefault(typeof (PX.Objects.SO.SOOrder.orderNbr))]
  protected virtual void _(PX.Data.Events.CacheAttached<INCostCenter.sOOrderNbr> e)
  {
  }

  [PXMergeAttributes]
  [SpecialOrderCostCenterSupport.SOCostCenterPersisting]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INCostCenter.sOOrderLineNbr> e)
  {
  }

  public override IEnumerable<Type> GetFieldsDependOn()
  {
    yield return typeof (PX.Objects.SO.SOLine.isSpecialOrder);
    yield return typeof (PX.Objects.SO.SOLine.siteID);
    yield return typeof (PX.Objects.SO.SOLine.operation);
  }

  public override bool IsSpecificCostCenter(PX.Objects.SO.SOLine line)
  {
    return line.IsSpecialOrder.GetValueOrDefault() && line.SiteID.HasValue && line.Behavior != "QT";
  }

  protected override SpecialOrderCostCenterSupport<SOOrderEntry, PX.Objects.SO.SOLine>.CostCenterKeys GetCostCenterKeys(
    PX.Objects.SO.SOLine line)
  {
    if (line.Operation == "R" || line.Behavior == "TR")
    {
      if (string.IsNullOrEmpty(line.OrigOrderType) || string.IsNullOrEmpty(line.OrigOrderNbr) || !line.OrigLineNbr.HasValue)
        throw new PXInvalidOperationException();
      return new SpecialOrderCostCenterSupport<SOOrderEntry, PX.Objects.SO.SOLine>.CostCenterKeys()
      {
        SiteID = line.SiteID,
        OrderType = line.OrigOrderType,
        OrderNbr = line.OrigOrderNbr,
        LineNbr = line.OrigLineNbr
      };
    }
    return new SpecialOrderCostCenterSupport<SOOrderEntry, PX.Objects.SO.SOLine>.CostCenterKeys()
    {
      SiteID = line.SiteID,
      OrderType = line.OrderType,
      OrderNbr = line.OrderNbr,
      LineNbr = line.LineNbr
    };
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.siteID> e)
  {
    if (!e.Row.SiteID.HasValue || !EnumerableExtensions.IsNotIn<int?>(e.Row.CostCenterID, new int?(), new int?(0)))
      return;
    PX.Objects.SO.SOLine tran = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.siteID>>) e).Cache, (object) e.Row);
    if (tran == null || !tran.IsSpecialOrder.GetValueOrDefault())
      return;
    SpecialOrderCostCenterSupport<SOOrderEntry, PX.Objects.SO.SOLine>.CostCenterKeys costCenterKeys = this.GetCostCenterKeys(tran);
    costCenterKeys.SiteID = e.Row.SiteID;
    int? costCenterId = this.FindOrCreateCostCenter(costCenterKeys).CostCenterID;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.siteID>>) e).Cache.SetValueExt<PX.Objects.SO.SOLineSplit.costCenterID>((object) e.Row, (object) costCenterId);
  }

  private class SOCostCenterPersistingAttribute : 
    PXEventSubscriberAttribute,
    IPXRowPersistingSubscriber
  {
    public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
    {
      if (!(e.Row is INCostCenter row))
        return;
      row.CostCenterCD = $"{row.SOOrderType.Trim()} {row.SOOrderNbr.Trim()} {row.SOOrderLineNbr}";
    }
  }
}
