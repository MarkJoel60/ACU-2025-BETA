// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INPIEntryExt.SpecialOrderCostCenterSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INPIEntryExt;

public class SpecialOrderCostCenterSupport : PXGraphExtension<INPIEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.INPIEntry.CreateProjectedTran(PX.Objects.IN.INPIEntry.CostLayerInfo,PX.Objects.IN.INPIDetail,PX.Objects.IN.INItemSiteSettings)" />
  /// </summary>
  [PXOverride]
  public virtual INPIEntry.ProjectedTranRec CreateProjectedTran(
    INPIEntry.CostLayerInfo costLayerInfo,
    INPIDetail line,
    INItemSiteSettings itemSiteSettings,
    Func<INPIEntry.CostLayerInfo, INPIDetail, INItemSiteSettings, INPIEntry.ProjectedTranRec> baseMethod)
  {
    INPIEntry.ProjectedTranRec projectedTran = baseMethod(costLayerInfo, line, itemSiteSettings);
    if (costLayerInfo.CostLayerType == "S")
    {
      INCostCenter inCostCenter = INCostCenter.PK.Find((PXGraph) this.Base, costLayerInfo.CostLayer.CostSiteID);
      projectedTran.IsSpecialOrder = true;
      projectedTran.SOOrderType = inCostCenter?.SOOrderType;
      projectedTran.SOOrderNbr = inCostCenter?.SOOrderNbr;
      projectedTran.SOOrderLineNbr = (int?) inCostCenter?.SOOrderLineNbr;
    }
    return projectedTran;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.INPIEntry.ReadCostLayers(PX.Objects.IN.INPIDetail)" />
  /// </summary>
  [PXOverride]
  public virtual IEnumerable<INPIEntry.CostLayerInfo> ReadCostLayers(
    INPIDetail detail,
    Func<INPIDetail, IEnumerable<INPIEntry.CostLayerInfo>> baseMethod)
  {
    List<INPIEntry.CostLayerInfo> costLayerInfoList = new List<INPIEntry.CostLayerInfo>();
    costLayerInfoList.AddRange(baseMethod(detail));
    costLayerInfoList.AddRange(this.ReadCostLayersFromSpecialOrderCostCenter(detail));
    return (IEnumerable<INPIEntry.CostLayerInfo>) costLayerInfoList;
  }

  private IEnumerable<INPIEntry.CostLayerInfo> ReadCostLayersFromSpecialOrderCostCenter(
    INPIDetail detail)
  {
    return (IEnumerable<INPIEntry.CostLayerInfo>) GraphHelper.RowCast<INCostStatus>((IEnumerable) ((PXSelectBase<INCostStatus>) new PXSelectJoin<INCostStatus, InnerJoin<INCostCenter, On<INCostStatus.costSiteID, Equal<INCostCenter.costCenterID>>, InnerJoin<INCostSubItemXRef, On<INCostSubItemXRef.costSubItemID, Equal<INCostStatus.costSubItemID>>>>, Where<INCostStatus.inventoryID, Equal<Required<INCostStatus.inventoryID>>, And<INCostStatus.qtyOnHand, Greater<decimal0>, And<INCostSubItemXRef.subItemID, Equal<Required<INCostSubItemXRef.subItemID>>, And<INCostCenter.siteID, Equal<Required<INCostCenter.siteID>>, And<INCostCenter.costLayerType, Equal<CostLayerType.special>, And<Where<INCostStatus.lotSerialNbr, Equal<Required<INCostStatus.lotSerialNbr>>, Or<INCostStatus.lotSerialNbr, IsNull, Or<INCostStatus.lotSerialNbr, Equal<Empty>>>>>>>>>>>((PXGraph) this.Base)).Select(new object[4]
    {
      (object) detail.InventoryID,
      (object) detail.SubItemID,
      (object) detail.SiteID,
      (object) detail.LotSerialNbr
    })).AsEnumerable<INCostStatus>().Select<INCostStatus, INPIEntry.CostLayerInfo>((Func<INCostStatus, INPIEntry.CostLayerInfo>) (layer => new INPIEntry.CostLayerInfo(layer, "S"))).ToList<INPIEntry.CostLayerInfo>();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.INPIEntry.GetIntersectionQty(PX.Objects.IN.INPIEntry.CostLayerInfo,PX.Objects.IN.INPIDetail)" />
  /// </summary>
  [PXOverride]
  public virtual Decimal GetIntersectionQty(
    INPIEntry.CostLayerInfo costLayer,
    INPIDetail line,
    Func<INPIEntry.CostLayerInfo, INPIDetail, Decimal> base_GetIntersectionQty)
  {
    Decimal val1_1 = base_GetIntersectionQty(costLayer, line);
    int num;
    if (costLayer.CostLayerType == "S")
    {
      num = costLayer.CostLayer.CostSiteID.Value;
    }
    else
    {
      if (!(costLayer.CostLayerType == "N") || PXAccess.FeatureInstalled<FeaturesSet.materialManagement>())
        return val1_1;
      num = 0;
    }
    INLocationStatusByCostCenter statusByCostCenter1 = INLocationStatusByCostCenter.PK.Find((PXGraph) this.Base, line.InventoryID, line.SubItemID, line.SiteID, line.LocationID, new int?(num));
    Decimal val1_2 = Math.Min(val1_1, Math.Min(((Decimal?) statusByCostCenter1?.QtyOnHand).GetValueOrDefault(), ((Decimal?) statusByCostCenter1?.QtyActual).GetValueOrDefault()));
    if (!string.IsNullOrEmpty(line.LotSerialNbr))
    {
      INLotSerialStatusByCostCenter statusByCostCenter2 = INLotSerialStatusByCostCenter.PK.Find((PXGraph) this.Base, line.InventoryID, line.SubItemID, line.SiteID, line.LocationID, line.LotSerialNbr, new int?(num));
      val1_2 = Math.Min(val1_2, Math.Min(((Decimal?) statusByCostCenter2?.QtyOnHand).GetValueOrDefault(), ((Decimal?) statusByCostCenter2?.QtyActual).GetValueOrDefault()));
    }
    return val1_2;
  }
}
