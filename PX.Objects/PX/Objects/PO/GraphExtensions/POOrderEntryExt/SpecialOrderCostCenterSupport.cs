// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.SpecialOrderCostCenterSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class SpecialOrderCostCenterSupport : SpecialOrderCostCenterSupport<POOrderEntry, PX.Objects.PO.POLine>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  public override IEnumerable<Type> GetFieldsDependOn()
  {
    yield return typeof (PX.Objects.PO.POLine.isSpecialOrder);
    yield return typeof (PX.Objects.PO.POLine.siteID);
  }

  public override bool IsSpecificCostCenter(PX.Objects.PO.POLine line)
  {
    return line.IsSpecialOrder.GetValueOrDefault() && line.SiteID.HasValue;
  }

  protected override SpecialOrderCostCenterSupport<POOrderEntry, PX.Objects.PO.POLine>.CostCenterKeys GetCostCenterKeys(
    PX.Objects.PO.POLine line)
  {
    if (!string.IsNullOrEmpty(line.SOOrderType) && !string.IsNullOrEmpty(line.SOOrderNbr) && line.SOLineNbr.HasValue)
      return new SpecialOrderCostCenterSupport<POOrderEntry, PX.Objects.PO.POLine>.CostCenterKeys()
      {
        SiteID = line.SiteID,
        OrderType = line.SOOrderType,
        OrderNbr = line.SOOrderNbr,
        LineNbr = line.SOLineNbr
      };
    SOLineSplit soLineSplit = (SOLineSplit) ((PXSelectBase) this.Base.RelatedSOLineSplit).View.SelectSingleBound((object[]) new PX.Objects.PO.POLine[1]
    {
      line
    }, Array.Empty<object>());
    if (soLineSplit == null)
      throw new RowNotFoundException(((PXSelectBase) this.Base.Transactions).Cache, new object[3]
      {
        (object) line.OrderType,
        (object) line.OrderNbr,
        (object) line.LineNbr
      });
    return new SpecialOrderCostCenterSupport<POOrderEntry, PX.Objects.PO.POLine>.CostCenterKeys()
    {
      SiteID = line.SiteID,
      OrderType = soLineSplit.OrderType,
      OrderNbr = soLineSplit.OrderNbr,
      LineNbr = soLineSplit.LineNbr
    };
  }
}
