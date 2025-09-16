// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.CostHistoryBySiteByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class CostHistoryBySiteByPeriod
{
  protected PXGraph context;
  protected int siteID;
  protected string finPeriod;
  protected Dictionary<int, Decimal> tranYTDCostTable;

  public CostHistoryBySiteByPeriod(PXGraph context, int siteID, string finperiod)
  {
    if (context == null)
      throw new ArgumentNullException(nameof (context));
    if (string.IsNullOrEmpty(finperiod))
      throw new ArgumentNullException(nameof (finperiod));
    this.context = context;
    this.siteID = siteID;
    this.finPeriod = finperiod;
  }

  protected virtual Dictionary<int, Decimal> GetTranYTDCostTableByPeriod(string periodID)
  {
    if (string.IsNullOrEmpty(periodID))
      throw new ArgumentNullException();
    if (this.tranYTDCostTable != null)
      return this.tranYTDCostTable;
    this.tranYTDCostTable = new Dictionary<int, Decimal>();
    foreach (PXResult<INItemCostHist, INItemCostHistByPeriod> pxResult in ((PXSelectBase<INItemCostHist>) new PXSelectJoinGroupBy<INItemCostHist, InnerJoin<INItemCostHistByPeriod, On<INItemCostHist.inventoryID, Equal<INItemCostHistByPeriod.inventoryID>, And<INItemCostHist.costSiteID, Equal<INItemCostHistByPeriod.costSiteID>, And<INItemCostHist.costSubItemID, Equal<INItemCostHistByPeriod.costSubItemID>, And<INItemCostHist.accountID, Equal<INItemCostHistByPeriod.accountID>, And<INItemCostHist.subID, Equal<INItemCostHistByPeriod.subID>, And<INItemCostHist.finPeriodID, Equal<INItemCostHistByPeriod.lastActivityPeriod>>>>>>>>, Where<INItemCostHistByPeriod.finPeriodID, Equal<Required<INItemCostHistByPeriod.finPeriodID>>, And<INItemCostHistByPeriod.siteID, Equal<Required<INItemCostHistByPeriod.siteID>>>>, Aggregate<GroupBy<INItemCostHist.costSiteID, GroupBy<INItemCostHist.inventoryID, GroupBy<INItemCostHist.finPeriodID, Sum<INItemCostHist.tranYtdCost>>>>>>(this.context)).Select(new object[2]
    {
      (object) periodID,
      (object) this.siteID
    }))
    {
      INItemCostHist inItemCostHist = PXResult<INItemCostHist, INItemCostHistByPeriod>.op_Implicit(pxResult);
      Dictionary<int, Decimal> tranYtdCostTable1 = this.tranYTDCostTable;
      int? inventoryId = inItemCostHist.InventoryID;
      int key1 = inventoryId.Value;
      Decimal? finYtdCost;
      if (!tranYtdCostTable1.ContainsKey(key1))
      {
        Dictionary<int, Decimal> tranYtdCostTable2 = this.tranYTDCostTable;
        inventoryId = inItemCostHist.InventoryID;
        int key2 = inventoryId.Value;
        finYtdCost = inItemCostHist.FinYtdCost;
        Decimal valueOrDefault = finYtdCost.GetValueOrDefault();
        tranYtdCostTable2.Add(key2, valueOrDefault);
      }
      else
      {
        Dictionary<int, Decimal> tranYtdCostTable3 = this.tranYTDCostTable;
        inventoryId = inItemCostHist.InventoryID;
        int key3 = inventoryId.Value;
        Dictionary<int, Decimal> dictionary = tranYtdCostTable3;
        int key4 = key3;
        Decimal num1 = tranYtdCostTable3[key3];
        finYtdCost = inItemCostHist.FinYtdCost;
        Decimal valueOrDefault = finYtdCost.GetValueOrDefault();
        Decimal num2 = num1 + valueOrDefault;
        dictionary[key4] = num2;
      }
    }
    return this.tranYTDCostTable;
  }

  public virtual Decimal GetTranYTDCost(int inventoryID)
  {
    Dictionary<int, Decimal> costTableByPeriod = this.GetTranYTDCostTableByPeriod(this.finPeriod);
    Decimal tranYtdCost = 0M;
    int key = inventoryID;
    ref Decimal local = ref tranYtdCost;
    costTableByPeriod.TryGetValue(key, out local);
    return tranYtdCost;
  }

  public virtual Decimal GetTotalCostOnSite()
  {
    return this.GetTranYTDCostTableByPeriod(this.finPeriod).Sum<KeyValuePair<int, Decimal>>((Func<KeyValuePair<int, Decimal>, Decimal>) (x => x.Value));
  }
}
