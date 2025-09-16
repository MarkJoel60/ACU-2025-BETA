// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.INTurnoverCalcItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN.Turnover;

[PXCacheName]
public class INTurnoverCalcItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? BranchID { get; set; }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string FromPeriodID { get; set; }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  [PXParent(typeof (INTurnoverCalcItem.FK.TurnoverCalc))]
  public virtual string ToPeriodID { get; set; }

  [AnyInventory(typeof (FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionLite<Match<BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>.And<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>, PX.Objects.IN.InventoryItem>.SearchFor<PX.Objects.IN.InventoryItem.inventoryID>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), IsKey = true)]
  [PXRestrictor(typeof (Where<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.markedForDeletion>>), "The inventory item is {0}.", new Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  public virtual int? InventoryID { get; set; }

  [SiteAny(IsKey = true)]
  public virtual int? SiteID { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BegQty { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BegCost { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdQty { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdCost { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AvgQty { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AvgCost { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? SoldQty { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? SoldCost { get; set; }

  [PXDBQuantity]
  public virtual Decimal? QtyRatio { get; set; }

  [PXDBPriceCost(true)]
  public virtual Decimal? CostRatio { get; set; }

  [PXDBQuantity]
  public virtual Decimal? QtySellDays { get; set; }

  [PXDBQuantity]
  public virtual Decimal? CostSellDays { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsVirtual { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<INTurnoverCalcItem>.By<INTurnoverCalcItem.branchID, INTurnoverCalcItem.fromPeriodID, INTurnoverCalcItem.toPeriodID, INTurnoverCalcItem.inventoryID, INTurnoverCalcItem.siteID>
  {
    public static INTurnoverCalcItem Find(
      PXGraph graph,
      int? branchID,
      string fromPeriodID,
      string toPeriodID,
      int? inventoryID,
      int? siteID,
      PKFindOptions options = 0)
    {
      return (INTurnoverCalcItem) PrimaryKeyOf<INTurnoverCalcItem>.By<INTurnoverCalcItem.branchID, INTurnoverCalcItem.fromPeriodID, INTurnoverCalcItem.toPeriodID, INTurnoverCalcItem.inventoryID, INTurnoverCalcItem.siteID>.FindBy(graph, (object) branchID, (object) fromPeriodID, (object) toPeriodID, (object) inventoryID, (object) siteID, options);
    }
  }

  public static class FK
  {
    public class TurnoverCalc : 
      PrimaryKeyOf<INTurnoverCalc>.By<INTurnoverCalc.branchID, INTurnoverCalc.fromPeriodID, INTurnoverCalc.toPeriodID>.ForeignKeyOf<INTurnoverCalcItem>.By<INTurnoverCalcItem.branchID, INTurnoverCalcItem.fromPeriodID, INTurnoverCalcItem.toPeriodID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INTurnoverCalcItem>.By<INTurnoverCalcItem.branchID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTurnoverCalcItem>.By<INTurnoverCalcItem.siteID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INTurnoverCalcItem>.By<INTurnoverCalcItem.inventoryID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalcItem.branchID>
  {
  }

  public abstract class fromPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTurnoverCalcItem.fromPeriodID>
  {
  }

  public abstract class toPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTurnoverCalcItem.toPeriodID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalcItem.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalcItem.siteID>
  {
  }

  public abstract class begQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTurnoverCalcItem.begQty>
  {
  }

  public abstract class begCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTurnoverCalcItem.begCost>
  {
  }

  public abstract class ytdQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTurnoverCalcItem.ytdQty>
  {
  }

  public abstract class ytdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTurnoverCalcItem.ytdCost>
  {
  }

  public abstract class avgQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTurnoverCalcItem.avgQty>
  {
  }

  public abstract class avgCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTurnoverCalcItem.avgCost>
  {
  }

  public abstract class soldQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTurnoverCalcItem.soldQty>
  {
  }

  public abstract class soldCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTurnoverCalcItem.soldCost>
  {
  }

  public abstract class qtyRatio : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTurnoverCalcItem.qtyRatio>
  {
  }

  public abstract class costRatio : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTurnoverCalcItem.costRatio>
  {
  }

  public abstract class qtySellDays : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItem.qtySellDays>
  {
  }

  public abstract class costSellDays : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItem.costSellDays>
  {
  }

  public abstract class isVirtual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTurnoverCalcItem.isVirtual>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTurnoverCalcItem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTurnoverCalcItem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTurnoverCalcItem.createdDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INTurnoverCalcItem.Tstamp>
  {
  }
}
