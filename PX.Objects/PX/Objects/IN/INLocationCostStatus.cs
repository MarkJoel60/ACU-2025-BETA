// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLocationCostStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select5<INCostStatus, InnerJoin<INLocation, On<INLocation.locationID, Equal<INCostStatus.costSiteID>>, InnerJoin<INCostSubItemXRef, On<INCostSubItemXRef.costSubItemID, Equal<INCostStatus.costSubItemID>>, CrossJoin<CommonSetup>>>, Where<INLocation.isCosted, Equal<boolTrue>>, Aggregate<GroupBy<INCostStatus.inventoryID, GroupBy<INCostSubItemXRef.subItemID, GroupBy<INCostStatus.costSiteID, Sum<INCostStatus.qtyOnHand, Sum<INCostStatus.totalCost>>>>>>>))]
public class INLocationCostStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ICostStatus
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _LocationID;
  protected Decimal? _QtyOnHand;
  protected Decimal? _TotalCost;
  protected short? _DecPlPrcCst;
  protected Decimal? _UnitCost;

  [PXDBInt(IsKey = true, BqlField = typeof (INCostStatus.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (INCostSubItemXRef.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INCostStatus.SiteID" />
  [PXDBInt(IsKey = true, BqlField = typeof (INCostStatus.siteID))]
  public virtual int? SiteID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INCostStatus.costSiteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBQuantity(BqlField = typeof (INCostStatus.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (INCostStatus.totalCost))]
  public virtual Decimal? TotalCost
  {
    get => this._TotalCost;
    set => this._TotalCost = value;
  }

  [PXDBShort(BqlField = typeof (CommonSetup.decPlPrcCst))]
  public virtual short? DecPlPrcCst
  {
    get => this._DecPlPrcCst;
    set => this._DecPlPrcCst = value;
  }

  [PXPriceCost]
  public virtual Decimal? UnitCost
  {
    [PXDependsOnFields(new Type[] {typeof (INLocationCostStatus.qtyOnHand), typeof (INLocationCostStatus.totalCost), typeof (INLocationCostStatus.decPlPrcCst)})] get
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

  public abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  INLocationCostStatus.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocationCostStatus.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocationCostStatus.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocationCostStatus.locationID>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationCostStatus.qtyOnHand>
  {
  }

  public abstract class totalCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationCostStatus.totalCost>
  {
  }

  public abstract class decPlPrcCst : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INLocationCostStatus.decPlPrcCst>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INLocationCostStatus.unitCost>
  {
  }
}
