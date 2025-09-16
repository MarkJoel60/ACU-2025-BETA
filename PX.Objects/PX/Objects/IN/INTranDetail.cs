// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTranDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Transaction Detail")]
[PXProjection(typeof (Select2<INTranSplit, InnerJoin<INTran, On<INTranSplit.FK.Tran>>, Where<INTranSplit.released, Equal<True>>>))]
[Serializable]
public class INTranDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TranType;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected int? _SplitLineNbr;
  protected DateTime? _TranDate;
  protected string _FinPeriodID;
  protected string _TranPeriodID;
  protected int? _InventoryID;
  protected int? _CostSubItemID;
  protected int? _CostSiteID;
  protected short? _InvtMult;
  protected Decimal? _SumQty;
  protected Decimal? _SumTranCost;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _BaseQty;
  protected Decimal? _TranCost;

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (INTranSplit.tranType))]
  [PXUIField(DisplayName = "Transaction Type")]
  [INTranType.List]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (INTranSplit.refNbr))]
  [PXUIField(DisplayName = "Ref. Number")]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (INTranSplit.lineNbr))]
  [PXUIField(DisplayName = "Line Number")]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (INTranSplit.splitLineNbr))]
  public virtual int? SplitLineNbr
  {
    get => this._SplitLineNbr;
    set => this._SplitLineNbr = value;
  }

  [PXDBDate(BqlField = typeof (INTranSplit.tranDate))]
  [PXUIField(DisplayName = "Transaction Date")]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (INTran.finPeriodID))]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (INTran.tranPeriodID))]
  public virtual string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  [StockItem(BqlField = typeof (INTranSplit.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(BqlField = typeof (INTranSplit.costSubItemID))]
  public virtual int? CostSubItemID
  {
    get => this._CostSubItemID;
    set => this._CostSubItemID = value;
  }

  [PXDBInt(BqlField = typeof (INTranSplit.costSiteID))]
  public virtual int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [PXDBShort(BqlField = typeof (INTranSplit.invtMult))]
  [PXUIField(DisplayName = "Inventory Multiplier")]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [PXDBQuantity(BqlField = typeof (INTranSplit.totalQty))]
  public virtual Decimal? SumQty
  {
    get => this._SumQty;
    set => this._SumQty = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (INTranSplit.totalCost))]
  public virtual Decimal? SumTranCost
  {
    get => this._SumTranCost;
    set => this._SumTranCost = value;
  }

  [SubItem(BqlField = typeof (INTranSplit.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Site(BqlField = typeof (INTranSplit.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (INTranDetail.siteID), BqlField = typeof (INTranSplit.locationID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (INTranSplit.lotSerialNbr))]
  [PXUIField(DisplayName = "Lot/Serial Number")]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBString(6, IsUnicode = true, BqlField = typeof (INTranSplit.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(BqlField = typeof (INTranSplit.qty))]
  [PXUIField(DisplayName = "Qty.")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBQuantity(BqlField = typeof (INTranSplit.baseQty))]
  [PXUIField(DisplayName = "Base Qty.")]
  public virtual Decimal? BaseQty
  {
    get => this._BaseQty;
    set => this._BaseQty = value;
  }

  [PXBaseCury]
  public virtual Decimal? TranCost
  {
    [PXDependsOnFields(new Type[] {typeof (INTranDetail.tranType), typeof (INTranDetail.sumTranCost), typeof (INTranDetail.sumQty), typeof (INTranDetail.baseQty)})] get
    {
      if (this.TranType == "ADJ" || this.TranType == "ASC" || this.TranType == "NSC")
        return this.SumTranCost;
      Decimal? nullable = this.SumQty;
      Decimal num = 0M;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue)
        return new Decimal?(0M);
      Decimal? baseQty = this.BaseQty;
      Decimal? sumTranCost = this.SumTranCost;
      nullable = baseQty.HasValue & sumTranCost.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * sumTranCost.GetValueOrDefault()) : new Decimal?();
      Decimal? sumQty = this.SumQty;
      return !(nullable.HasValue & sumQty.HasValue) ? new Decimal?() : new Decimal?(nullable.GetValueOrDefault() / sumQty.GetValueOrDefault());
    }
    set => this._TranCost = value;
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranDetail.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranDetail.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranDetail.lineNbr>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranDetail.splitLineNbr>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INTranDetail.tranDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranDetail.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranDetail.tranPeriodID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranDetail.inventoryID>
  {
  }

  public abstract class costSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranDetail.costSubItemID>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranDetail.costSiteID>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INTranDetail.invtMult>
  {
  }

  public abstract class sumQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranDetail.sumQty>
  {
  }

  public abstract class sumTranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranDetail.sumTranCost>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranDetail.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranDetail.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranDetail.locationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranDetail.lotSerialNbr>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranDetail.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranDetail.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranDetail.baseQty>
  {
  }

  public abstract class tranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranDetail.tranCost>
  {
  }
}
