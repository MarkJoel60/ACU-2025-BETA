// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SpecialOrderCostCenterSupport`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

public abstract class SpecialOrderCostCenterSupport<TGraph, TLine> : 
  PXGraphExtension<TGraph>,
  ICostCenterSupport<TLine>
  where TGraph : PXGraph
  where TLine : class, IItemPlanMaster, IBqlTable, new()
{
  public virtual int SortOrder => 100;

  public abstract IEnumerable<Type> GetFieldsDependOn();

  public abstract bool IsSpecificCostCenter(TLine tran);

  protected abstract SpecialOrderCostCenterSupport<TGraph, TLine>.CostCenterKeys GetCostCenterKeys(
    TLine tran);

  public int GetCostCenterID(TLine tran)
  {
    return this.FindOrCreateCostCenter(this.GetCostCenterKeys(tran)).CostCenterID.Value;
  }

  public INCostCenter GetCostCenter(TLine tran)
  {
    return this.FindOrCreateCostCenter(this.GetCostCenterKeys(tran));
  }

  protected virtual string BuildCostCenterCD(
    int? siteID,
    string orderType,
    string orderNbr,
    int? lineNbr)
  {
    return $"{orderType.Trim()} {orderNbr.Trim()} {lineNbr}";
  }

  protected virtual INCostCenter FindOrCreateCostCenter(
    SpecialOrderCostCenterSupport<TGraph, TLine>.CostCenterKeys k)
  {
    return PXResultset<INCostCenter>.op_Implicit(((PXSelectBase<INCostCenter>) new PXSelect<INCostCenter, Where<INCostCenter.siteID, Equal<Required<INCostCenter.siteID>>, And<INCostCenter.sOOrderType, Equal<Required<INCostCenter.sOOrderType>>, And<INCostCenter.sOOrderNbr, Equal<Required<INCostCenter.sOOrderNbr>>, And<INCostCenter.sOOrderLineNbr, Equal<Required<INCostCenter.sOOrderLineNbr>>>>>>>((PXGraph) this.Base)).Select(new object[4]
    {
      (object) k.SiteID,
      (object) k.OrderType,
      (object) k.OrderNbr,
      (object) k.LineNbr
    })) ?? this.InsertNewCostSite(k.SiteID, k.OrderType, k.OrderNbr, k.LineNbr);
  }

  private INCostCenter InsertNewCostSite(
    int? siteID,
    string orderType,
    string orderNbr,
    int? lineNbr)
  {
    INCostCenter inCostCenter = new INCostCenter()
    {
      CostLayerType = "S",
      SiteID = siteID,
      SOOrderType = orderType,
      SOOrderNbr = orderNbr,
      SOOrderLineNbr = lineNbr,
      CostCenterCD = this.BuildCostCenterCD(siteID, orderType, orderNbr, lineNbr)
    };
    return GraphHelper.Caches<INCostCenter>((PXGraph) this.Base).Insert(inCostCenter) ?? throw new InvalidOperationException("Failed to insert new INCostCenter");
  }

  protected class CostCenterKeys
  {
    public int? SiteID { get; set; }

    public string OrderType { get; set; }

    public string OrderNbr { get; set; }

    public int? LineNbr { get; set; }
  }
}
