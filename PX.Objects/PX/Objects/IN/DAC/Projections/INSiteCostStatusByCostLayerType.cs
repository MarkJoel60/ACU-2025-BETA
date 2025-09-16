// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Projections.INSiteCostStatusByCostLayerType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN.DAC.Projections;

/// <exclude />
[PXCacheName("IN Site Cost Status by Cost Layer Type")]
[PXProjection(typeof (SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCostSubItemXRef>.On<BqlOperand<INCostSubItemXRef.costSubItemID, IBqlInt>.IsEqual<INCostStatus.costSubItemID>>>, FbqlJoins.Inner<INCostSite>.On<BqlOperand<INCostStatus.costSiteID, IBqlInt>.IsEqual<INCostSite.costSiteID>>>, FbqlJoins.Left<INCostCenter>.On<BqlOperand<INCostStatus.costSiteID, IBqlInt>.IsEqual<INCostCenter.costCenterID>>>, FbqlJoins.Cross<CommonSetup>>>.Where<BqlOperand<INCostSite.costSiteType, IBqlString>.IsIn<NameOf<INCostCenter>, NameOf<INSite>>>.AggregateTo<GroupBy<INCostStatus.inventoryID>, GroupBy<INCostSubItemXRef.subItemID>, GroupBy<INCostStatus.siteID>, GroupBy<INCostCenter.costLayerType>, Sum<INCostStatus.qtyOnHand>, Sum<INCostStatus.totalCost>>))]
public class INSiteCostStatusByCostLayerType : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ICostStatus
{
  [PXDBInt(IsKey = true, BqlField = typeof (INCostStatus.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INCostSubItemXRef.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INCostStatus.siteID))]
  public virtual int? SiteID { get; set; }

  [PXDBCalced(typeof (IsNull<INCostCenter.costLayerType, PX.Objects.IN.CostLayerType.normal>), typeof (string))]
  [PXString(1, IsKey = true)]
  public virtual 
  #nullable disable
  string CostLayerType { get; set; }

  [PXDBQuantity(BqlField = typeof (INCostStatus.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyOnHand { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INCostStatus.totalCost))]
  public virtual Decimal? TotalCost { get; set; }

  [PXDBShort(BqlField = typeof (CommonSetup.decPlPrcCst))]
  public virtual short? DecPlPrcCst { get; set; }

  [PXPriceCost]
  public virtual Decimal? UnitCost
  {
    [PXDependsOnFields(new Type[] {typeof (INSiteCostStatusByCostLayerType.qtyOnHand), typeof (INSiteCostStatusByCostLayerType.totalCost), typeof (INSiteCostStatusByCostLayerType.decPlPrcCst)})] get
    {
      if (!this.QtyOnHand.HasValue || !this.TotalCost.HasValue)
        return new Decimal?();
      Decimal? nullable = this.QtyOnHand;
      Decimal num1 = 0M;
      Decimal num2;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      {
        num2 = 0M;
      }
      else
      {
        nullable = this.TotalCost;
        Decimal num3 = nullable.Value;
        nullable = this.QtyOnHand;
        Decimal num4 = nullable.Value;
        num2 = Math.Round(num3 / num4, (int) this.DecPlPrcCst.Value, MidpointRounding.AwayFromZero);
      }
      return new Decimal?(num2);
    }
    set
    {
    }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INSiteCostStatusByCostLayerType.inventoryID>
  {
  }

  public abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INSiteCostStatusByCostLayerType.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteCostStatusByCostLayerType.siteID>
  {
  }

  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteCostStatusByCostLayerType.costLayerType>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteCostStatusByCostLayerType.qtyOnHand>
  {
  }

  public abstract class totalCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteCostStatusByCostLayerType.totalCost>
  {
  }

  public abstract class decPlPrcCst : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INSiteCostStatusByCostLayerType.decPlPrcCst>
  {
  }

  public abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteCostStatusByCostLayerType.unitCost>
  {
  }
}
