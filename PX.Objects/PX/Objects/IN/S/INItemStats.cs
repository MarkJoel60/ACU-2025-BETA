// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.S.INItemStats
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
namespace PX.Objects.IN.S;

[PXHidden]
[Serializable]
public class INItemStats : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SiteID;
  protected 
  #nullable disable
  string _ValMethod;
  protected Decimal? _StdCost;
  protected Decimal? _LastCost;
  protected DateTime? _LastCostDate;
  protected Decimal? _AvgCost;
  protected Decimal? _MinCost;
  protected Decimal? _MaxCost;
  protected Decimal? _QtyOnHand;
  protected Decimal? _TotalCost;
  protected Decimal? _QtyReceived;
  protected Decimal? _CostReceived;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.valMethod, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<INItemStats.inventoryID>>>>))]
  public virtual string ValMethod
  {
    get => this._ValMethod;
    set => this._ValMethod = value;
  }

  [PXDBCostScalar(typeof (Search<INItemSite.stdCost, Where<INItemSite.inventoryID, Equal<INItemStats.inventoryID>, And<INItemSite.siteID, Equal<INItemStats.siteID>>>>))]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search2<InventoryItemCurySettings.stdCost, InnerJoin<INSite, On<INSite.baseCuryID, Equal<InventoryItemCurySettings.curyID>>>, Where<InventoryItemCurySettings.inventoryID, Equal<Current<INItemStats.inventoryID>>, And<INSite.siteID, Equal<Current<INItemStats.siteID>>>>>))]
  [PXUIField(DisplayName = "Current Cost", Enabled = false)]
  public virtual Decimal? StdCost
  {
    get => this._StdCost;
    set => this._StdCost = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Cost", Enabled = false)]
  public virtual Decimal? LastCost
  {
    get => this._LastCost;
    set => this._LastCost = value;
  }

  [PXDBDate(PreserveTime = true)]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  public virtual DateTime? LastCostDate
  {
    get => this._LastCostDate;
    set => this._LastCostDate = value;
  }

  [PXDBPriceCostCalced(typeof (Switch<Case<Where<INItemStats.qtyOnHand, Equal<decimal0>>, decimal0>, Div<INItemStats.totalCost, INItemStats.qtyOnHand>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Average Cost", Enabled = false)]
  public virtual Decimal? AvgCost
  {
    get => this._AvgCost;
    set => this._AvgCost = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Minimal Cost", Enabled = false)]
  public virtual Decimal? MinCost
  {
    get => this._MinCost;
    set => this._MinCost = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Max. Cost", Enabled = false)]
  public virtual Decimal? MaxCost
  {
    get => this._MaxCost;
    set => this._MaxCost = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalCost
  {
    get => this._TotalCost;
    set => this._TotalCost = value;
  }

  [PXDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyReceived
  {
    get => this._QtyReceived;
    set => this._QtyReceived = value;
  }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CostReceived
  {
    get => this._CostReceived;
    set => this._CostReceived = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemStats.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemStats.siteID>
  {
  }

  public abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemStats.valMethod>
  {
  }

  public abstract class stdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStats.stdCost>
  {
  }

  public abstract class lastCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStats.lastCost>
  {
  }

  public abstract class lastCostDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemStats.lastCostDate>
  {
  }

  public abstract class avgCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStats.avgCost>
  {
  }

  public abstract class minCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStats.minCost>
  {
  }

  public abstract class maxCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStats.maxCost>
  {
  }

  public abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStats.qtyOnHand>
  {
  }

  public abstract class totalCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStats.totalCost>
  {
  }

  public abstract class qtyReceived : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStats.qtyReceived>
  {
  }

  public abstract class costReceived : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStats.costReceived>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemStats.Tstamp>
  {
  }
}
