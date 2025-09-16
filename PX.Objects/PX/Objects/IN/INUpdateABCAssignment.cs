// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUpdateABCAssignment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class INUpdateABCAssignment : PXGraph<INUpdateABCAssignment>
{
  public PXCancel<UpdateABCAssignmentSettings> Cancel;
  public PXFilter<UpdateABCAssignmentSettings> UpdateSettings;
  [PXFilterable(new Type[] {})]
  public PXSelectOrderBy<UpdateABCAssignmentResult, OrderBy<Desc<UpdateABCAssignmentResult.ytdCost, Asc<UpdateABCAssignmentResult.cumulativeRatio>>>> ResultPreview;
  public PXSelect<INItemSite> itemsite;
  public PXAction<UpdateABCAssignmentSettings> Process;

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable process(PXAdapter adapter)
  {
    this.CalcABCAssignments(true);
    return adapter.Get();
  }

  public INUpdateABCAssignment()
  {
    ((PXSelectBase) this.ResultPreview).Cache.AllowInsert = false;
    ((PXSelectBase) this.ResultPreview).Cache.AllowDelete = false;
    ((PXSelectBase) this.ResultPreview).Cache.AllowUpdate = false;
  }

  private List<UpdateABCAssignmentResult> CalcABCAssignments(bool updateDB)
  {
    UpdateABCAssignmentSettings current = ((PXSelectBase<UpdateABCAssignmentSettings>) this.UpdateSettings).Current;
    List<UpdateABCAssignmentResult> assignmentResultList = new List<UpdateABCAssignmentResult>();
    if (current == null || !current.SiteID.HasValue || current.FinPeriodID == null)
      return assignmentResultList;
    if (updateDB)
      ((PXSelectBase) this.itemsite).Cache.Clear();
    int? nullable1 = current.SiteID;
    CostHistoryBySiteByPeriod costHistory = this.CreateCostHistory(nullable1.Value, current.FinPeriodID);
    PXResultset<INItemSite> pxResultset1 = ((PXSelectBase<INItemSite>) new PXSelectJoin<INItemSite, InnerJoin<InventoryItem, On2<INItemSite.FK.InventoryItem, And<InventoryItem.stkItem, NotEqual<boolFalse>, And<Match<InventoryItem, Current<AccessInfo.userName>>>>>>, Where<INItemSite.siteID, Equal<Current<UpdateABCAssignmentSettings.siteID>>>>((PXGraph) this)).Select(Array.Empty<object>());
    PXResultset<INABCCode> pxResultset2 = PXSelectBase<INABCCode, PXSelectOrderBy<INABCCode, OrderBy<Asc<INABCCode.aBCCodeID>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    Decimal totalCostOnSite = costHistory.GetTotalCostOnSite();
    if (pxResultset2.Count == 0 || totalCostOnSite == 0M)
    {
      foreach (PXResult<INItemSite, InventoryItem> pxResult in pxResultset1)
      {
        INItemSite inItemSite = PXResult<INItemSite, InventoryItem>.op_Implicit(pxResult);
        InventoryItem inventoryItem = PXResult<INItemSite, InventoryItem>.op_Implicit(pxResult);
        assignmentResultList.Add(new UpdateABCAssignmentResult()
        {
          ABCCodeFixed = inItemSite.ABCCodeIsFixed,
          Descr = inventoryItem.Descr,
          InventoryID = inItemSite.InventoryID,
          OldABCCode = inItemSite.ABCCodeID,
          NewABCCode = inItemSite.ABCCodeID
        });
      }
      return assignmentResultList;
    }
    pxResultset1.Sort((Comparison<PXResult<INItemSite>>) ((x, y) =>
    {
      INItemSite inItemSite1 = PXResult<INItemSite>.op_Implicit(y);
      INItemSite inItemSite2 = PXResult<INItemSite>.op_Implicit(x);
      return costHistory.GetTranYTDCost(inItemSite1.InventoryID.Value).CompareTo(costHistory.GetTranYTDCost(inItemSite2.InventoryID.Value));
    }));
    int num1 = 0;
    Decimal num2 = 0M;
    Decimal num3 = 0M;
    foreach (PXResult<INABCCode> pxResult1 in pxResultset2)
    {
      INABCCode inabcCode = PXResult<INABCCode>.op_Implicit(pxResult1);
      num3 += inabcCode.ABCPct.GetValueOrDefault();
      for (; num1 < pxResultset1.Count; ++num1)
      {
        PXResult<INItemSite, InventoryItem> pxResult2 = (PXResult<INItemSite, InventoryItem>) pxResultset1[num1];
        INItemSite inItemSite = PXResult<INItemSite, InventoryItem>.op_Implicit(pxResult2);
        InventoryItem inventoryItem = PXResult<INItemSite, InventoryItem>.op_Implicit(pxResult2);
        CostHistoryBySiteByPeriod historyBySiteByPeriod = costHistory;
        nullable1 = inventoryItem.InventoryID;
        int inventoryID = nullable1.Value;
        Decimal tranYtdCost = historyBySiteByPeriod.GetTranYTDCost(inventoryID);
        if ((num2 + tranYtdCost) / totalCostOnSite <= num3 / 100M)
        {
          num2 += tranYtdCost;
          UpdateABCAssignmentResult assignmentResult1 = new UpdateABCAssignmentResult();
          assignmentResult1.ABCCodeFixed = inItemSite.ABCCodeIsFixed;
          assignmentResult1.Descr = inventoryItem.Descr;
          assignmentResult1.InventoryID = inItemSite.InventoryID;
          bool? abcCodeIsFixed = inItemSite.ABCCodeIsFixed;
          assignmentResult1.NewABCCode = !abcCodeIsFixed.GetValueOrDefault() ? inabcCode.ABCCodeID : inItemSite.ABCCodeID;
          assignmentResult1.OldABCCode = inItemSite.ABCCodeID;
          assignmentResult1.YtdCost = new Decimal?(tranYtdCost);
          UpdateABCAssignmentResult assignmentResult2 = assignmentResult1;
          Decimal? nullable2 = assignmentResult1.YtdCost;
          Decimal num4 = totalCostOnSite;
          Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / num4) : new Decimal?();
          Decimal num5 = (Decimal) 100;
          Decimal? nullable4;
          if (!nullable3.HasValue)
          {
            nullable2 = new Decimal?();
            nullable4 = nullable2;
          }
          else
            nullable4 = new Decimal?(nullable3.GetValueOrDefault() * num5);
          assignmentResult2.Ratio = nullable4;
          assignmentResult1.CumulativeRatio = new Decimal?(num2 / totalCostOnSite * 100M);
          assignmentResultList.Add(assignmentResult1);
          if (updateDB && inItemSite.ABCCodeID != assignmentResult1.NewABCCode)
          {
            inItemSite.ABCCodeID = assignmentResult1.NewABCCode;
            ((PXSelectBase<INItemSite>) this.itemsite).Update(inItemSite);
          }
        }
        else
          break;
      }
    }
    if (updateDB)
      ((PXGraph) this).Actions.PressSave();
    return assignmentResultList;
  }

  public virtual CostHistoryBySiteByPeriod CreateCostHistory(int siteID, string finperiod)
  {
    return new CostHistoryBySiteByPeriod((PXGraph) this, siteID, finperiod);
  }

  protected virtual IEnumerable resultPreview() => (IEnumerable) this.CalcABCAssignments(false);
}
