// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.INItemCostHistRange
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

#nullable enable
namespace PX.Objects.IN.Turnover;

[PXHidden]
[PXProjection(typeof (SelectFromBase<INItemCostHist, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemCostHist.finPeriodID, IBqlString>.IsLessEqual<BqlField<INTurnoverCalc.toPeriodID, IBqlString>.FromCurrent.Value>>.AggregateTo<GroupBy<INItemCostHist.inventoryID>, GroupBy<INItemCostHist.costSubItemID>, GroupBy<INItemCostHist.costSiteID>, GroupBy<INItemCostHist.accountID>, GroupBy<INItemCostHist.subID>, Max<INItemCostHist.siteID>>), Persistent = false)]
public class INItemCostHistRange : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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

  [Site(true, BqlField = typeof (INItemCostHist.siteID))]
  public virtual int? SiteID { get; set; }

  public abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  INItemCostHistRange.inventoryID>
  {
  }

  public abstract class costSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemCostHistRange.costSubItemID>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHistRange.costSiteID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHistRange.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHistRange.subID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHistRange.siteID>
  {
  }
}
