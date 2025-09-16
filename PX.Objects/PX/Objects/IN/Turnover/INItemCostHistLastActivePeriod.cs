// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.INItemCostHistLastActivePeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;

#nullable enable
namespace PX.Objects.IN.Turnover;

[PXHidden]
[PXProjection(typeof (SelectFromBase<INItemCostHist, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemCostHist.finPeriodID, IBqlString>.IsLess<BqlField<INTurnoverCalc.fromPeriodID, IBqlString>.FromCurrent.Value>>.AggregateTo<GroupBy<INItemCostHist.inventoryID>, GroupBy<INItemCostHist.costSubItemID>, GroupBy<INItemCostHist.costSiteID>, GroupBy<INItemCostHist.accountID>, GroupBy<INItemCostHist.subID>, Max<INItemCostHist.finPeriodID>>), Persistent = false)]
public class INItemCostHistLastActivePeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [StockItem(IsKey = true, BqlField = typeof (INItemCostHist.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [SubItem(IsKey = true, BqlField = typeof (INItemCostHist.costSubItemID))]
  public virtual int? CostSubItemID { get; set; }

  [Site(IsKey = true, BqlField = typeof (INItemCostHist.costSiteID))]
  public virtual int? CostSiteID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INItemCostHist.accountID))]
  public virtual int? AccountID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INItemCostHist.subID))]
  public virtual int? SubID { get; set; }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (INItemCostHist.finPeriodID))]
  public virtual 
  #nullable disable
  string LastActiveFinPeriodID { get; set; }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemCostHistLastActivePeriod.inventoryID>
  {
  }

  public abstract class costSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemCostHistLastActivePeriod.costSubItemID>
  {
  }

  public abstract class costSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemCostHistLastActivePeriod.costSiteID>
  {
  }

  public abstract class accountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemCostHistLastActivePeriod.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHistLastActivePeriod.subID>
  {
  }

  public abstract class lastActiveFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemCostHistLastActivePeriod.lastActiveFinPeriodID>
  {
  }
}
