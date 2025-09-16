// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.DAC.PMItemCostStatusByCostCenter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PM.DAC;

/// <summary>
/// The item cost statuses aggregated by the cost center ID, inventory ID, and warehouse ID.
/// </summary>
[PXCacheName("Item Cost Status by Cost Center")]
[PXProjection(typeof (SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCostCenter>.On<BqlOperand<INCostCenter.costCenterID, IBqlInt>.IsEqual<INCostStatus.costSiteID>>>>.AggregateTo<GroupBy<INCostStatus.costSiteID>, GroupBy<INCostStatus.inventoryID>, GroupBy<INCostStatus.siteID>, Sum<INCostStatus.qtyOnHand>, Sum<INCostStatus.totalCost>>))]
public class PMItemCostStatusByCostCenter : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ICostStatus
{
  /// <inheritdoc cref="P:PX.Objects.IN.INCostStatus.InventoryID" />
  [PXDBInt(IsKey = true, BqlField = typeof (INCostStatus.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INCostCenter.CostCenterID" />
  [PXDBInt(IsKey = true, BqlField = typeof (INCostCenter.costCenterID))]
  public virtual int? CostCenterID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INCostStatus.SiteID" />
  [PXDBInt(IsKey = true, BqlField = typeof (INCostStatus.siteID))]
  public virtual int? SiteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INCostStatus.QtyOnHand" />
  [PXDBQuantity(BqlField = typeof (INCostStatus.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyOnHand { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INCostStatus.TotalCost" />
  [PXDBPriceCost(BqlField = typeof (INCostStatus.totalCost))]
  public virtual Decimal? TotalCost { get; set; }

  public abstract class inventoryID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    PMItemCostStatusByCostCenter.inventoryID>
  {
  }

  public abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMItemCostStatusByCostCenter.costCenterID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMItemCostStatusByCostCenter.siteID>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMItemCostStatusByCostCenter.qtyOnHand>
  {
  }

  public abstract class totalCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMItemCostStatusByCostCenter.totalCost>
  {
  }
}
