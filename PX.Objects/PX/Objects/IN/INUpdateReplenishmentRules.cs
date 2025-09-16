// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUpdateReplenishmentRules
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
[Serializable]
public class INUpdateReplenishmentRules : PXGraph<
#nullable disable
INUpdateReplenishmentRules>
{
  public PXFilter<INUpdateReplenishmentRules.Filter> filter;
  public PXCancel<INUpdateReplenishmentRules.Filter> Cancel;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessingJoin<INItemSite, INUpdateReplenishmentRules.Filter, InnerJoin<INUpdateReplenishmentRules.InventoryItemRO, On<INUpdateReplenishmentRules.InventoryItemRO.inventoryID, Equal<INItemSite.inventoryID>>>, Where2<Where<INItemSite.siteID, Equal<Current<INUpdateReplenishmentRules.Filter.siteID>>, Or<Current<INUpdateReplenishmentRules.Filter.siteID>, IsNull>>, And<INItemSite.replenishmentPolicyID, IsNotNull, And2<Where<INItemSite.replenishmentPolicyID, Equal<Current<INUpdateReplenishmentRules.Filter.replenishmentPolicyID>>, Or<Current<INUpdateReplenishmentRules.Filter.replenishmentPolicyID>, IsNull>>, And<INItemSite.replenishmentClassID, IsNotNull, And<Where<INItemSite.replenishmentSource, Equal<INReplenishmentSource.transfer>, Or<INItemSite.replenishmentSource, Equal<INReplenishmentSource.purchased>>>>>>>>> Records;

  [StockItem(IsKey = true, DisplayName = "Inventory ID", TabOrder = 1, CacheGlobal = true)]
  protected virtual void INItemSite_InventoryID_CacheAttached(PXCache sender)
  {
  }

  public IEnumerable records()
  {
    INUpdateReplenishmentRules replenishmentRules = this;
    INUpdateReplenishmentRules.Filter current = ((PXSelectBase<INUpdateReplenishmentRules.Filter>) replenishmentRules.filter).Current;
    if (current != null && current.ForecastDate.HasValue)
    {
      if (current.Action == "Calc")
      {
        INUpdateReplenishmentRules.Filter filter1 = current;
        DateTime? forecastDate = current.ForecastDate;
        DateTime? nullable1 = new DateTime?(INUpdateReplenishmentRules.PeriodInfo.CalcStartDate("MT", forecastDate.Value));
        filter1.StartDateForDemandPeriodMonth = nullable1;
        INUpdateReplenishmentRules.Filter filter2 = current;
        forecastDate = current.ForecastDate;
        DateTime? nullable2 = new DateTime?(INUpdateReplenishmentRules.PeriodInfo.CalcStartDate("QT", forecastDate.Value));
        filter2.StartDateForDemandPeriodQuarter = nullable2;
        INUpdateReplenishmentRules.Filter filter3 = current;
        forecastDate = current.ForecastDate;
        DateTime? nullable3 = new DateTime?(INUpdateReplenishmentRules.PeriodInfo.CalcStartDate("WK", forecastDate.Value));
        filter3.StartDateForDemandPeriodWeek = nullable3;
        INUpdateReplenishmentRules.Filter filter4 = current;
        forecastDate = current.ForecastDate;
        DateTime? nullable4 = new DateTime?(INUpdateReplenishmentRules.PeriodInfo.CalcStartDate("DY", forecastDate.Value));
        filter4.StartDateForDemandPeriodDay = nullable4;
        PXSelectBase<INItemSite> pxSelectBase = (PXSelectBase<INItemSite>) new PXSelectJoin<INItemSite, InnerJoin<INUpdateReplenishmentRules.InventoryItemRO, On<INUpdateReplenishmentRules.InventoryItemRO.inventoryID, Equal<INItemSite.inventoryID>, And<INUpdateReplenishmentRules.InventoryItemRO.itemStatus, Equal<InventoryItemStatus.active>>>, InnerJoin<INSite, On<INItemSite.siteID, Equal<INSite.siteID>>, InnerJoin<INItemRep, On<INItemRep.inventoryID, Equal<INItemSite.inventoryID>, And<INItemRep.curyID, Equal<INSite.baseCuryID>, And<INItemRep.replenishmentClassID, Equal<INItemSite.replenishmentClassID>>>>, LeftJoin<INItemClass, On<INUpdateReplenishmentRules.InventoryItemRO.FK.ItemClass>>>>>, Where<INItemSite.planningMethod, Equal<INPlanningMethod.inventoryReplenishment>, And<INItemSite.replenishmentPolicyID, IsNotNull, And<INItemSite.replenishmentClassID, IsNotNull, And<INItemRep.forecastModelType, IsNotNull, And<INItemRep.forecastModelType, NotEqual<DemandForecastModelType.none>, And2<Where<INItemRep.launchDate, IsNull, Or<INItemRep.launchDate, LessEqual<Current<INUpdateReplenishmentRules.Filter.forecastDate>>>>, And2<Where<INItemRep.terminationDate, IsNull, Or<INItemRep.terminationDate, Greater<Current<INUpdateReplenishmentRules.Filter.forecastDate>>>>, And<Where<INItemSite.lastForecastDate, IsNull, Or<INItemSite.lastForecastDate, LessEqual<Switch<Case<Where<INItemRep.forecastPeriodType, Equal<DemandPeriodType.month>>, Current<INUpdateReplenishmentRules.Filter.startDateForDemandPeriodMonth>, Case<Where<INItemRep.forecastPeriodType, Equal<DemandPeriodType.quarter>>, Current<INUpdateReplenishmentRules.Filter.startDateForDemandPeriodQuarter>, Case<Where<INItemRep.forecastPeriodType, Equal<DemandPeriodType.week>>, Current<INUpdateReplenishmentRules.Filter.startDateForDemandPeriodWeek>, Case<Where<INItemRep.forecastPeriodType, Equal<DemandPeriodType.day>>, Current<INUpdateReplenishmentRules.Filter.startDateForDemandPeriodDay>>>>>, Current<INUpdateReplenishmentRules.Filter.forecastDate>>>>>>>>>>>>>>((PXGraph) replenishmentRules);
        if (!string.IsNullOrEmpty(current.ItemClassCD))
          pxSelectBase.WhereAnd<Where<INItemClass.itemClassCD, Like<Current<INUpdateReplenishmentRules.Filter.itemClassCDWildcard>>>>();
        if (!string.IsNullOrEmpty(current.ReplenishmentPolicyID))
          pxSelectBase.WhereAnd<Where<INItemSite.replenishmentPolicyID, Equal<Current<INUpdateReplenishmentRules.Filter.replenishmentPolicyID>>>>();
        if (current.SiteID.HasValue)
          pxSelectBase.WhereAnd<Where<INItemSite.siteID, Equal<Current<INUpdateReplenishmentRules.Filter.siteID>>>>();
        int count = 0;
        int totalCount = 0;
        int startRow = PXView.StartRow;
        int num = 0;
        foreach (PXResult<INItemSite, INUpdateReplenishmentRules.InventoryItemRO, INSite, INItemRep> pxResult in ((PXSelectBase) pxSelectBase).View.Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
        {
          ++totalCount;
          ++count;
          yield return (object) new PXResult<INItemSite, INUpdateReplenishmentRules.InventoryItemRO>(PXResult<INItemSite, INUpdateReplenishmentRules.InventoryItemRO, INSite, INItemRep>.op_Implicit(pxResult), PXResult<INItemSite, INUpdateReplenishmentRules.InventoryItemRO, INSite, INItemRep>.op_Implicit(pxResult));
        }
        PXView.StartRow = 0;
      }
      else if (current.Action == "Clear")
      {
        PXSelectBase<INItemSite> pxSelectBase = (PXSelectBase<INItemSite>) new PXSelectJoin<INItemSite, InnerJoin<INUpdateReplenishmentRules.InventoryItemRO, On<INUpdateReplenishmentRules.InventoryItemRO.inventoryID, Equal<INItemSite.inventoryID>, And<INUpdateReplenishmentRules.InventoryItemRO.itemStatus, Equal<InventoryItemStatus.active>>>, InnerJoin<INSite, On<INItemSite.siteID, Equal<INSite.siteID>>, InnerJoin<INItemRep, On<INItemRep.inventoryID, Equal<INItemSite.inventoryID>, And<INItemRep.curyID, Equal<INSite.baseCuryID>, And<INItemRep.replenishmentClassID, Equal<INItemSite.replenishmentClassID>>>>, LeftJoin<INItemClass, On<INUpdateReplenishmentRules.InventoryItemRO.FK.ItemClass>>>>>, Where<INItemSite.replenishmentPolicyID, IsNotNull, And<INItemSite.replenishmentClassID, IsNotNull, And<INItemRep.forecastModelType, IsNotNull, And<INItemRep.forecastModelType, NotEqual<DemandForecastModelType.none>, And2<Where<INItemRep.launchDate, IsNull, Or<INItemRep.launchDate, LessEqual<Current<INUpdateReplenishmentRules.Filter.forecastDate>>>>, And2<Where<INItemRep.terminationDate, IsNull, Or<INItemRep.terminationDate, Greater<Current<INUpdateReplenishmentRules.Filter.forecastDate>>>>, And<INItemSite.lastForecastDate, GreaterEqual<Current<INUpdateReplenishmentRules.Filter.forecastDate>>, And<Where<INItemSite.lastFCApplicationDate, IsNull, Or<INItemSite.lastFCApplicationDate, Less<INItemSite.lastForecastDate>>>>>>>>>>>>((PXGraph) replenishmentRules);
        if (!string.IsNullOrEmpty(current.ItemClassCD))
          pxSelectBase.WhereAnd<Where<INItemClass.itemClassCD, Like<Current<INUpdateReplenishmentRules.Filter.itemClassCDWildcard>>>>();
        if (!string.IsNullOrEmpty(current.ReplenishmentPolicyID))
          pxSelectBase.WhereAnd<Where<INItemSite.replenishmentPolicyID, Equal<Current<INUpdateReplenishmentRules.Filter.replenishmentPolicyID>>>>();
        if (current.SiteID.HasValue)
          pxSelectBase.WhereAnd<Where<INItemSite.siteID, Equal<Current<INUpdateReplenishmentRules.Filter.siteID>>>>();
        int startRow = PXView.StartRow;
        int num = 0;
        foreach (PXResult<INItemSite, INUpdateReplenishmentRules.InventoryItemRO, INSite, INItemRep> pxResult in ((PXSelectBase) pxSelectBase).View.Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
          yield return (object) new PXResult<INItemSite, INUpdateReplenishmentRules.InventoryItemRO>(PXResult<INItemSite, INUpdateReplenishmentRules.InventoryItemRO, INSite, INItemRep>.op_Implicit(pxResult), PXResult<INItemSite, INUpdateReplenishmentRules.InventoryItemRO, INSite, INItemRep>.op_Implicit(pxResult));
        PXView.StartRow = 0;
      }
    }
  }

  public INUpdateReplenishmentRules()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<INItemSite>) this.Records).SetProcessDelegate<ReplenishmentStatsUpdateGraph>(new PXProcessingBase<INItemSite>.ProcessItemDelegate<ReplenishmentStatsUpdateGraph>((object) new INUpdateReplenishmentRules.\u003C\u003Ec__DisplayClass8_0()
    {
      filter = ((PXSelectBase<INUpdateReplenishmentRules.Filter>) this.filter).Current
    }, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  protected virtual void Filter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || string.IsNullOrEmpty(((INUpdateReplenishmentRules.Filter) e.Row).Action))
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    INUpdateReplenishmentRules.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new INUpdateReplenishmentRules.\u003C\u003Ec__DisplayClass9_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.row = (INUpdateReplenishmentRules.Filter) e.Row;
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetEnabled<INUpdateReplenishmentRules.Filter.siteID>(sender, (object) cDisplayClass90.row, PXAccess.FeatureInstalled<FeaturesSet.warehouse>());
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.row.Action == "Calc")
    {
      // ISSUE: method pointer
      ((PXProcessingBase<INItemSite>) this.Records).SetProcessDelegate<ReplenishmentStatsUpdateGraph>(new PXProcessingBase<INItemSite>.ProcessItemDelegate<ReplenishmentStatsUpdateGraph>((object) cDisplayClass90, __methodptr(\u003CFilter_RowSelected\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    if (!(cDisplayClass90.row.Action == "Clear"))
      return;
    // ISSUE: method pointer
    ((PXProcessingBase<INItemSite>) this.Records).SetProcessDelegate<ReplenishmentStatsUpdateGraph>(new PXProcessingBase<INItemSite>.ProcessItemDelegate<ReplenishmentStatsUpdateGraph>((object) cDisplayClass90, __methodptr(\u003CFilter_RowSelected\u003Eb__1)));
  }

  public static void UpdateReplenishmentProc(
    ReplenishmentStatsUpdateGraph graph,
    INItemSite aItem,
    INUpdateReplenishmentRules.Filter filter)
  {
    ((PXGraph) graph).Clear();
    PXCache cach1 = ((PXGraph) graph).Caches[typeof (INItemSite)];
    PXCache cach2 = ((PXGraph) graph).Caches[typeof (INItemSiteReplenishment)];
    INSite inSite = INSite.PK.Find((PXGraph) graph, aItem.SiteID);
    INItemRep aItemRepSettings = PXResultset<INItemRep>.op_Implicit(PXSelectBase<INItemRep, PXSelect<INItemRep, Where<INItemRep.inventoryID, Equal<Required<INItemRep.inventoryID>>, And<INItemRep.curyID, Equal<Required<INItemRep.curyID>>, And<INItemRep.replenishmentClassID, Equal<Required<INItemRep.replenishmentClassID>>>>>>.Config>.Select((PXGraph) graph, new object[3]
    {
      (object) aItem.InventoryID,
      (object) inSite.BaseCuryID,
      (object) aItem.ReplenishmentClassID
    }));
    if (string.IsNullOrEmpty(aItemRepSettings.ForecastModelType) || aItemRepSettings.ForecastModelType == "NNN")
      return;
    DateTime? nullable1 = filter.ForecastDate;
    DateTime aFromDate = nullable1.Value;
    DateTime? minDate = new DateTime?();
    string forecastPeriodType = aItemRepSettings.ForecastPeriodType;
    List<INUpdateReplenishmentRules.PeriodInfo> periodInfoList = (List<INUpdateReplenishmentRules.PeriodInfo>) null;
    INUpdateReplenishmentRules.PeriodInfo periodInfo = new INUpdateReplenishmentRules.PeriodInfo(forecastPeriodType, aFromDate);
    DateTime startDate = periodInfo.StartDate;
    nullable1 = aItem.LastForecastDate;
    if (nullable1.HasValue)
    {
      nullable1 = aItem.LastForecastDate;
      if (nullable1.Value >= startDate)
        return;
    }
    if (aItemRepSettings.ForecastModelType == "CMA")
    {
      int? nullable2 = new int?(aItemRepSettings.HistoryDepth.Value);
      if (nullable2.HasValue)
      {
        List<INUpdateReplenishmentRules.PeriodInfo> periods = INUpdateReplenishmentRules.CreatePeriods(forecastPeriodType, startDate, nullable2.Value);
        minDate = new DateTime?(periods.Count > 0 ? periods[0].StartDate : startDate);
      }
    }
    else
    {
      nullable1 = aItem.LastForecastDate;
      if (nullable1.HasValue)
      {
        nullable1 = aItem.LastForecastDate;
        if (nullable1.Value < periodInfo.StartDate)
        {
          minDate = aItem.LastForecastDate;
          periodInfoList = INUpdateReplenishmentRules.CreatePeriods(forecastPeriodType, minDate.Value, startDate, true);
        }
      }
    }
    List<INReplenishmentSeason> seasonality = INUpdateReplenishmentRules.RetrivePredefinedSeasonality((PXGraph) graph, aItem, false);
    Dictionary<int, INUpdateReplenishmentRules.SalesStatInfo> subSalesStats;
    Dictionary<int, List<INUpdateReplenishmentRules.PeriodSalesStatInfo>> periodValues;
    INUpdateReplenishmentRules.RetrieveSalesHistory2((PXGraph) graph, aItem, aItemRepSettings, minDate, startDate, out subSalesStats, out periodValues);
    List<INUpdateReplenishmentRules.PeriodSalesStatInfo> periodSalesStatInfoList1 = new List<INUpdateReplenishmentRules.PeriodSalesStatInfo>();
    Dictionary<int, List<INUpdateReplenishmentRules.PeriodSalesStatInfo>> dictionary = new Dictionary<int, List<INUpdateReplenishmentRules.PeriodSalesStatInfo>>(periodValues.Count);
    foreach (KeyValuePair<int, List<INUpdateReplenishmentRules.PeriodSalesStatInfo>> keyValuePair in periodValues)
    {
      dictionary.Add(keyValuePair.Key, INUpdateReplenishmentRules.Normalize(keyValuePair.Value, periodInfo));
      INUpdateReplenishmentRules.AppendToList(periodSalesStatInfoList1, keyValuePair.Value);
    }
    List<INUpdateReplenishmentRules.PeriodSalesStatInfo> periodSalesStatInfoList2 = INUpdateReplenishmentRules.Normalize(periodSalesStatInfoList1, periodInfo);
    periodValues.Clear();
    DemandForecastModel model = INUpdateReplenishmentRules.CreateModel(aItemRepSettings.ForecastModelType);
    if (model == null)
      return;
    foreach (KeyValuePair<int, List<INUpdateReplenishmentRules.PeriodSalesStatInfo>> keyValuePair in dictionary)
    {
      model.Init((IEnumerable<DemandForecastModel.IDataPoint>) keyValuePair.Value.ConvertAll<INUpdateReplenishmentRules.DataPointSeasonalAdapter>((Converter<INUpdateReplenishmentRules.PeriodSalesStatInfo, INUpdateReplenishmentRules.DataPointSeasonalAdapter>) (op => new INUpdateReplenishmentRules.DataPointSeasonalAdapter(op, (IEnumerable<INReplenishmentSeason>) seasonality))));
      INUpdateReplenishmentRules.PeriodSalesStatInfo op1 = new INUpdateReplenishmentRules.PeriodSalesStatInfo(periodInfo);
      model.GetForecast((DemandForecastModel.IDataPoint) new INUpdateReplenishmentRules.DataPointSeasonalAdapter(op1, (IEnumerable<INReplenishmentSeason>) seasonality));
      INUpdateReplenishmentRules.SalesStatInfo salesStatInfo;
      if (!subSalesStats.TryGetValue(keyValuePair.Key, out salesStatInfo))
      {
        salesStatInfo = new INUpdateReplenishmentRules.SalesStatInfo();
        subSalesStats.Add(keyValuePair.Key, salesStatInfo);
      }
      salesStatInfo.Average = op1.SalesPerDaySnAjusted;
      salesStatInfo.MSE = op1.SalesPerDayMSE;
    }
    model.Init((IEnumerable<DemandForecastModel.IDataPoint>) periodSalesStatInfoList2.ConvertAll<INUpdateReplenishmentRules.DataPointSeasonalAdapter>((Converter<INUpdateReplenishmentRules.PeriodSalesStatInfo, INUpdateReplenishmentRules.DataPointSeasonalAdapter>) (op => new INUpdateReplenishmentRules.DataPointSeasonalAdapter(op, (IEnumerable<INReplenishmentSeason>) seasonality))));
    INUpdateReplenishmentRules.PeriodSalesStatInfo op2 = new INUpdateReplenishmentRules.PeriodSalesStatInfo(periodInfo);
    model.GetForecast((DemandForecastModel.IDataPoint) new INUpdateReplenishmentRules.DataPointSeasonalAdapter(op2, (IEnumerable<INReplenishmentSeason>) seasonality));
    INUpdateReplenishmentRules.SalesStatInfo src1 = new INUpdateReplenishmentRules.SalesStatInfo();
    src1.Average = op2.SalesPerDaySnAjusted;
    src1.MSE = op2.SalesPerDayMSE;
    Decimal? aLeadTimeAve;
    Decimal? aLeadTimeMSE;
    INUpdateReplenishmentRules.RecalcLeadTime((PXGraph) graph, aItem, out aLeadTimeAve, out aLeadTimeMSE);
    INItemSite copy1 = (INItemSite) cach1.CreateCopy((object) aItem);
    copy1.LastForecastDate = filter.ForecastDate;
    copy1.ForecastModelType = aItemRepSettings.ForecastModelType;
    copy1.ForecastPeriodType = aItemRepSettings.ForecastPeriodType;
    INUpdateReplenishmentRules.Copy(copy1, src1);
    if (aLeadTimeAve.HasValue && aLeadTimeMSE.HasValue)
    {
      copy1.LeadTimeAverage = aLeadTimeAve;
      copy1.LeadTimeMSE = aLeadTimeMSE;
    }
    Decimal? nullable3 = aItem.ServiceLevel;
    double num1 = INUpdateReplenishmentRules.StatsUtilities.NormsInv((double) (nullable3 ?? 0.5M));
    copy1.SafetyStockSuggested = new Decimal?((Decimal) (num1 * Math.Sqrt((double) aLeadTimeMSE.GetValueOrDefault() * Math.Pow((double) src1.Average, 2.0) + Math.Pow((double) aLeadTimeAve.GetValueOrDefault(), 2.0) * (double) src1.MSE)));
    nullable3 = copy1.LeadTimeAverage;
    if (nullable3.HasValue)
    {
      INItemSite inItemSite = copy1;
      nullable3 = copy1.LeadTimeAverage;
      Decimal num2 = nullable3.GetValueOrDefault() * src1.Average;
      nullable3 = copy1.SafetyStockSuggested;
      Decimal valueOrDefault = nullable3.GetValueOrDefault();
      Decimal? nullable4 = new Decimal?(num2 + valueOrDefault);
      inItemSite.MinQtySuggested = nullable4;
      copy1.MaxQtySuggested = copy1.MinQtySuggested;
    }
    INItemSite inItemSite1 = (INItemSite) cach1.Update((object) copy1);
    foreach (PXResult<INItemSiteReplenishment> pxResult in ((PXSelectBase<INItemSiteReplenishment>) graph.inSubItemsSite).Select(new object[2]
    {
      (object) aItem.SiteID,
      (object) aItem.InventoryID
    }))
    {
      INItemSiteReplenishment siteReplenishment1 = PXResult<INItemSiteReplenishment>.op_Implicit(pxResult);
      INItemSiteReplenishment copy2 = (INItemSiteReplenishment) cach2.CreateCopy((object) siteReplenishment1);
      INUpdateReplenishmentRules.SalesStatInfo src2;
      if (subSalesStats.TryGetValue(copy2.SubItemID.Value, out src2))
      {
        INUpdateReplenishmentRules.Copy(copy2, src2);
        copy2.SafetyStockSuggested = new Decimal?((Decimal) (num1 * Math.Sqrt((double) aLeadTimeMSE.GetValueOrDefault() * Math.Pow((double) src2.Average, 2.0) + Math.Pow((double) aLeadTimeAve.GetValueOrDefault(), 2.0) * (double) src2.MSE)));
        INItemSiteReplenishment siteReplenishment2 = copy2;
        nullable3 = inItemSite1.LeadTimeAverage;
        Decimal num3 = nullable3.GetValueOrDefault() * src2.Average;
        nullable3 = copy2.SafetyStockSuggested;
        Decimal valueOrDefault = nullable3.GetValueOrDefault();
        Decimal? nullable5 = new Decimal?(num3 + valueOrDefault);
        siteReplenishment2.MinQtySuggested = nullable5;
        copy2.MaxQtySuggested = copy2.MinQtySuggested;
      }
      INItemSiteReplenishment siteReplenishment3 = (INItemSiteReplenishment) cach2.Update((object) copy2);
    }
    ((PXGraph) graph).Actions.PressSave();
  }

  public static void ClearReplenishmentProc(
    ReplenishmentStatsUpdateGraph graph,
    INItemSite aItem,
    INUpdateReplenishmentRules.Filter filter)
  {
    ((PXGraph) graph).Clear();
    PXCache cach1 = ((PXGraph) graph).Caches[typeof (INItemSite)];
    PXCache cach2 = ((PXGraph) graph).Caches[typeof (INItemSiteReplenishment)];
    INItemSite copy1 = (INItemSite) cach1.CreateCopy((object) aItem);
    INUpdateReplenishmentRules.ClearReplenishmentInfo(copy1);
    INItemSite inItemSite = (INItemSite) cach1.Update((object) copy1);
    foreach (PXResult<INItemSiteReplenishment> pxResult in ((PXSelectBase<INItemSiteReplenishment>) graph.inSubItemsSite).Select(new object[2]
    {
      (object) aItem.SiteID,
      (object) aItem.InventoryID
    }))
    {
      INItemSiteReplenishment siteReplenishment1 = PXResult<INItemSiteReplenishment>.op_Implicit(pxResult);
      INItemSiteReplenishment copy2 = (INItemSiteReplenishment) cach2.CreateCopy((object) siteReplenishment1);
      INUpdateReplenishmentRules.ClearReplenishmentInfo(copy2);
      INItemSiteReplenishment siteReplenishment2 = (INItemSiteReplenishment) cach2.Update((object) copy2);
    }
    ((PXGraph) graph).Actions.PressSave();
  }

  protected static void RecalcLeadTime(
    PXGraph aGraph,
    INItemSite aItemInfo,
    out Decimal? aLeadTimeAve,
    out Decimal? aLeadTimeMSE)
  {
    aLeadTimeAve = new Decimal?();
    aLeadTimeMSE = new Decimal?();
    if (aItemInfo.ReplenishmentSource == "P")
      INUpdateReplenishmentRules.RecalcVendorLeadTime(aGraph, aItemInfo, out aLeadTimeAve, out aLeadTimeMSE);
    if (!(aItemInfo.ReplenishmentSource == "T"))
      return;
    INUpdateReplenishmentRules.RetrieveTransferLeadTime(aGraph, aItemInfo, out aLeadTimeAve, out aLeadTimeMSE);
  }

  protected static void RecalcVendorLeadTime(
    PXGraph aGraph,
    INItemSite aItemInfo,
    out Decimal? aLeadTimeAve,
    out Decimal? aLeadTimeMSE)
  {
    aLeadTimeAve = new Decimal?();
    aLeadTimeMSE = new Decimal?();
    int? preferredVendorId = aItemInfo.PreferredVendorID;
    int? vendorLocationId = aItemInfo.PreferredVendorLocationID;
    List<Decimal> numList = new List<Decimal>();
    Decimal num1 = 0M;
    PXSelectBase<PX.Objects.PO.POLine> pxSelectBase = (PXSelectBase<PX.Objects.PO.POLine>) new PXSelectReadonly2<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POReceiptLine, On<PX.Objects.PO.POReceiptLine.pOType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.PO.POReceiptLine.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<PX.Objects.PO.POLine.lineNbr, Equal<PX.Objects.PO.POReceiptLine.pOLineNbr>>>>, InnerJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POReceiptLine.FK.Receipt>>>, Where<PX.Objects.PO.POReceiptLine.receiptDate, IsNotNull, And<PX.Objects.PO.POLine.requestedDate, IsNotNull, And<PX.Objects.PO.POLine.costCenterID, Equal<CostCenter.freeStock>, And<PX.Objects.PO.POReceiptLine.inventoryID, Equal<Required<PX.Objects.PO.POReceiptLine.inventoryID>>, And<PX.Objects.PO.POReceiptLine.siteID, Equal<Required<PX.Objects.PO.POReceiptLine.siteID>>>>>>>, OrderBy<Asc<PX.Objects.PO.POLine.requestedDate>>>(aGraph);
    if (preferredVendorId.HasValue)
    {
      pxSelectBase.WhereAnd<Where<PX.Objects.PO.POReceipt.vendorID, Equal<Required<PX.Objects.PO.POReceipt.vendorID>>>>();
      if (vendorLocationId.HasValue)
        pxSelectBase.WhereAnd<Where<PX.Objects.PO.POReceipt.vendorLocationID, Equal<Required<PX.Objects.PO.POReceipt.vendorLocationID>>>>();
    }
    foreach (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceipt> pxResult in pxSelectBase.Select(new object[4]
    {
      (object) aItemInfo.InventoryID,
      (object) aItemInfo.SiteID,
      (object) preferredVendorId,
      (object) vendorLocationId
    }))
    {
      PX.Objects.PO.POLine poLine = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceipt>.op_Implicit(pxResult);
      DateTime? nullable = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceipt>.op_Implicit(pxResult).ReceiptDate;
      DateTime dateTime1 = nullable.Value;
      nullable = poLine.RequestedDate;
      DateTime dateTime2 = nullable.Value;
      Decimal days = (Decimal) (dateTime1 - dateTime2).Days;
      if (!(days < 0M))
      {
        numList.Add(days);
        num1 += days;
      }
    }
    if (numList.Count > 0)
    {
      Decimal average = num1 / (Decimal) numList.Count;
      Decimal mse = 0M;
      Decimal mad = 0M;
      numList.ForEach((Action<Decimal>) (value =>
      {
        Decimal num2 = value - average;
        mse += num2 * num2;
        mad += Math.Abs(num2);
      }));
      aLeadTimeMSE = numList.Count <= 1 ? new Decimal?(0M) : new Decimal?((Decimal) ((double) mse / (double) numList.Count));
      Decimal stdev = (Decimal) Math.Sqrt((double) aLeadTimeMSE.Value);
      if (numList.Count > 3 && stdev > average)
      {
        numList.RemoveAll((Predicate<Decimal>) (value => Math.Abs(value - average) > 2M * stdev));
        average = 0M;
        mse = 0M;
        mad = 0M;
        numList.ForEach((Action<Decimal>) (value => average += value));
        if (numList.Count > 1)
        {
          average /= (Decimal) numList.Count;
          numList.ForEach((Action<Decimal>) (value =>
          {
            Decimal num3 = value - average;
            mse += num3 * num3;
            mad += Math.Abs(num3);
          }));
        }
      }
      aLeadTimeAve = new Decimal?(average);
      if (numList.Count > 1)
        aLeadTimeMSE = new Decimal?((Decimal) ((double) mse / (double) numList.Count));
      else
        aLeadTimeMSE = new Decimal?(0M);
    }
    else
    {
      if (!preferredVendorId.HasValue || !vendorLocationId.HasValue)
        return;
      PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectReadonly<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select(aGraph, new object[2]
      {
        (object) preferredVendorId,
        (object) vendorLocationId
      }));
      if (location == null)
        return;
      short? vleadTime = location.VLeadTime;
      if (!vleadTime.HasValue)
        return;
      ref Decimal? local = ref aLeadTimeAve;
      vleadTime = location.VLeadTime;
      Decimal? nullable = new Decimal?((Decimal) vleadTime.Value);
      local = nullable;
      aLeadTimeMSE = new Decimal?(0M);
    }
  }

  protected static void RetrieveTransferLeadTime(
    PXGraph aGraph,
    INItemSite aItemInfo,
    out Decimal? aLeadTimeAve,
    out Decimal? aLeadTimeMSE)
  {
    INSite inSite = INSite.PK.Find(aGraph, aItemInfo.SiteID);
    aLeadTimeAve = new Decimal?();
    aLeadTimeMSE = new Decimal?();
    INItemClassRep inItemClassRep = PXResultset<INItemClassRep>.op_Implicit(PXSelectBase<INItemClassRep, PXSelectReadonly2<INItemClassRep, InnerJoin<INUpdateReplenishmentRules.InventoryItemRO, On<INUpdateReplenishmentRules.InventoryItemRO.itemClassID, Equal<INItemClassRep.itemClassID>>>, Where<INItemClassRep.replenishmentClassID, Equal<Required<INItemClassRep.replenishmentClassID>>, And<INItemClassRep.curyID, Equal<Required<INItemClassRep.curyID>>, And<INUpdateReplenishmentRules.InventoryItemRO.inventoryID, Equal<Required<INUpdateReplenishmentRules.InventoryItemRO.inventoryID>>>>>>.Config>.Select(aGraph, new object[3]
    {
      (object) aItemInfo.ReplenishmentClassID,
      (object) inSite.BaseCuryID,
      (object) aItemInfo.InventoryID
    }));
    if (inItemClassRep == null)
      return;
    ref Decimal? local = ref aLeadTimeAve;
    short? transferLeadTime = inItemClassRep.TransferLeadTime;
    Decimal? nullable = transferLeadTime.HasValue ? new Decimal?((Decimal) transferLeadTime.GetValueOrDefault()) : new Decimal?();
    local = nullable;
    aLeadTimeMSE = new Decimal?(0M);
  }

  protected static void RetrieveSalesHistory2(
    PXGraph aGraph,
    INItemSite aItemInfo,
    INItemRep aItemRepSettings,
    DateTime? minDate,
    DateTime maxDate,
    out Dictionary<int, INUpdateReplenishmentRules.SalesStatInfo> subSalesStats,
    out Dictionary<int, List<INUpdateReplenishmentRules.PeriodSalesStatInfo>> periodValues)
  {
    subSalesStats = new Dictionary<int, INUpdateReplenishmentRules.SalesStatInfo>();
    periodValues = new Dictionary<int, List<INUpdateReplenishmentRules.PeriodSalesStatInfo>>();
    string forecastPeriodType = aItemRepSettings.ForecastPeriodType;
    Lazy<PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment>[]> lazy = Lazy.By<PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment>[]>((Func<PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment>[]>) (() => ((IEnumerable<PXResult<INItemSiteHistByCostCenterD>>) PXSelectBase<INItemSiteHistByCostCenterD, PXSelectJoin<INItemSiteHistByCostCenterD, LeftJoin<INItemSiteReplenishment, On<INItemSiteHistByCostCenterD.FK.ItemSiteReplenishment>>, Where<INItemSiteHistByCostCenterD.siteID, Equal<Required<INItemSiteHistByCostCenterD.siteID>>, And<INItemSiteHistByCostCenterD.inventoryID, Equal<Required<INItemSiteHistByCostCenterD.inventoryID>>, And<INItemSiteHistByCostCenterD.costCenterID, Equal<CostCenter.freeStock>, And<INItemSiteHistByCostCenterD.sDate, Less<Required<INItemSiteHistByCostCenterD.sDate>>>>>>, OrderBy<Asc<INItemSiteHistByCostCenterD.sDate, Asc<INItemSiteHistByCostCenterD.subItemID>>>>.Config>.Select(aGraph, new object[3]
    {
      (object) aItemInfo.SiteID,
      (object) aItemInfo.InventoryID,
      (object) maxDate
    })).AsEnumerable<PXResult<INItemSiteHistByCostCenterD>>().Cast<PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment>>().ToArray<PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment>>()));
    DateTime? nullable1 = new DateTime?();
    nullable1 = !aItemInfo.LaunchDate.HasValue ? (DateTime?) ((IEnumerable<PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment>>) lazy.Value).Select<PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment>, INItemSiteHistByCostCenterD>((Func<PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment>, INItemSiteHistByCostCenterD>) (h => PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment>.op_Implicit(h))).FirstOrDefault<INItemSiteHistByCostCenterD>((Func<INItemSiteHistByCostCenterD, bool>) (h => h.QtySales.GetValueOrDefault() > 0M || h.QtyTransferOut.GetValueOrDefault() > 0M || h.QtyAssemblyOut.GetValueOrDefault() > 0M))?.SDate : new DateTime?(aItemInfo.LaunchDate.Value);
    if (!nullable1.HasValue)
      return;
    DateTime? nullable2 = nullable1;
    DateTime dateTime1 = maxDate;
    if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() >= dateTime1 ? 1 : 0) : 0) != 0)
      return;
    DateTime? nullable3;
    DateTime dateTime2;
    if (minDate.HasValue)
    {
      DateTime dateTime3 = nullable1.Value;
      nullable3 = minDate;
      if ((nullable3.HasValue ? (dateTime3 < nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        dateTime2 = minDate.Value;
        goto label_7;
      }
    }
    dateTime2 = nullable1.Value;
label_7:
    DateTime initDate = dateTime2;
    INUpdateReplenishmentRules.InitSalesStats(aGraph, aItemInfo, aItemRepSettings, initDate, subSalesStats, periodValues);
    foreach (PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment> pxResult in lazy.Value)
    {
      INItemSiteHistByCostCenterD aSalesInfo = PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment>.op_Implicit(pxResult);
      PXResult<INItemSiteHistByCostCenterD, INItemSiteReplenishment>.op_Implicit(pxResult);
      int key = aSalesInfo.SubItemID.Value;
      INUpdateReplenishmentRules.SalesStatInfo salesStatInfo1;
      if (!subSalesStats.TryGetValue(key, out salesStatInfo1))
      {
        salesStatInfo1 = new INUpdateReplenishmentRules.SalesStatInfo();
        INUpdateReplenishmentRules.SalesStatInfo salesStatInfo2 = salesStatInfo1;
        nullable3 = aSalesInfo.SDate;
        DateTime dateTime4 = nullable3.Value;
        salesStatInfo2.minDate = dateTime4;
        subSalesStats.Add(key, salesStatInfo1);
      }
      nullable3 = aItemInfo.LaunchDate;
      if (nullable3.HasValue)
      {
        INUpdateReplenishmentRules.SalesStatInfo salesStatInfo3 = salesStatInfo1;
        nullable3 = aItemInfo.LaunchDate;
        DateTime dateTime5 = nullable3.Value;
        salesStatInfo3.minDate = dateTime5;
      }
      else
      {
        DateTime minDate1 = salesStatInfo1.minDate;
        nullable3 = aSalesInfo.SDate;
        DateTime dateTime6 = nullable3.Value;
        if (minDate1 > dateTime6 && (aSalesInfo.QtySales.GetValueOrDefault() > 0M || aSalesInfo.QtyTransferOut.GetValueOrDefault() > 0M || aSalesInfo.QtyAssemblyOut.GetValueOrDefault() > 0M))
        {
          INUpdateReplenishmentRules.SalesStatInfo salesStatInfo4 = salesStatInfo1;
          nullable3 = aSalesInfo.SDate;
          DateTime dateTime7 = nullable3.Value;
          salesStatInfo4.minDate = dateTime7;
        }
      }
      DateTime? nullable4;
      if (minDate.HasValue)
      {
        nullable3 = aSalesInfo.SDate;
        nullable4 = minDate;
        if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          continue;
      }
      nullable4 = aItemInfo.LaunchDate;
      if (nullable4.HasValue)
      {
        nullable4 = aSalesInfo.SDate;
        nullable3 = aItemInfo.LaunchDate;
        DateTime dateTime8 = nullable3.Value;
        if ((nullable4.HasValue ? (nullable4.GetValueOrDefault() < dateTime8 ? 1 : 0) : 0) != 0)
          continue;
      }
      nullable4 = aItemInfo.LaunchDate;
      DateTime aSalesStartingDate = nullable4 ?? salesStatInfo1.minDate;
      INUpdateReplenishmentRules.PeriodSalesStatInfo aInfo = new INUpdateReplenishmentRules.PeriodSalesStatInfo(forecastPeriodType, aSalesInfo, aSalesStartingDate);
      List<INUpdateReplenishmentRules.PeriodSalesStatInfo> aList;
      if (!periodValues.TryGetValue(key, out aList))
      {
        aList = new List<INUpdateReplenishmentRules.PeriodSalesStatInfo>();
        periodValues.Add(key, aList);
      }
      INUpdateReplenishmentRules.AppendToList(aList, aInfo);
    }
  }

  protected static List<INReplenishmentSeason> RetrivePredefinedSeasonality(
    PXGraph aGraph,
    INItemSite aItemInfo,
    bool normalize)
  {
    List<INReplenishmentSeason> replenishmentSeasonList = new List<INReplenishmentSeason>();
    Decimal totalWeight = 0M;
    foreach (PXResult<INReplenishmentSeason> pxResult in PXSelectBase<INReplenishmentSeason, PXSelectReadonly<INReplenishmentSeason, Where<INReplenishmentSeason.active, Equal<True>, And<INReplenishmentSeason.replenishmentPolicyID, Equal<Required<INReplenishmentSeason.replenishmentPolicyID>>>>>.Config>.Select(aGraph, new object[1]
    {
      (object) aItemInfo.ReplenishmentPolicyID
    }))
    {
      INReplenishmentSeason replenishmentSeason = PXResult<INReplenishmentSeason>.op_Implicit(pxResult);
      replenishmentSeasonList.Add(replenishmentSeason);
      totalWeight += replenishmentSeason.Factor ?? 1M;
    }
    if (normalize && totalWeight > 0M)
      replenishmentSeasonList.ForEach((Action<INReplenishmentSeason>) (op =>
      {
        INReplenishmentSeason replenishmentSeason = op;
        Decimal? factor = replenishmentSeason.Factor;
        Decimal num = totalWeight;
        replenishmentSeason.Factor = factor.HasValue ? new Decimal?(factor.GetValueOrDefault() / num) : new Decimal?();
      }));
    return replenishmentSeasonList;
  }

  protected static void InitSalesStats(
    PXGraph aGraph,
    INItemSite aItemInfo,
    INItemRep aItemRepSettings,
    DateTime initDate,
    Dictionary<int, INUpdateReplenishmentRules.SalesStatInfo> subSalesStats,
    Dictionary<int, List<INUpdateReplenishmentRules.PeriodSalesStatInfo>> periodValues)
  {
    string forecastPeriodType = aItemRepSettings.ForecastPeriodType;
    int num = 0;
    foreach (PXResult<INItemSiteReplenishment> pxResult in PXSelectBase<INItemSiteReplenishment, PXSelectReadonly<INItemSiteReplenishment, Where<INItemSiteReplenishment.siteID, Equal<Required<INItemSiteReplenishment.siteID>>, And<INItemSiteReplenishment.inventoryID, Equal<Required<INItemSiteReplenishment.inventoryID>>>>>.Config>.Select(aGraph, new object[2]
    {
      (object) aItemInfo.SiteID,
      (object) aItemInfo.InventoryID
    }))
    {
      INItemSiteReplenishment siteReplenishment = PXResult<INItemSiteReplenishment>.op_Implicit(pxResult);
      INUpdateReplenishmentRules.PeriodSalesStatInfo aInfo = new INUpdateReplenishmentRules.PeriodSalesStatInfo(forecastPeriodType, initDate);
      int key = siteReplenishment.SubItemID.Value;
      List<INUpdateReplenishmentRules.PeriodSalesStatInfo> aList;
      if (!periodValues.TryGetValue(key, out aList))
      {
        aList = new List<INUpdateReplenishmentRules.PeriodSalesStatInfo>();
        periodValues.Add(key, aList);
      }
      INUpdateReplenishmentRules.AppendToList(aList, aInfo);
      INUpdateReplenishmentRules.SalesStatInfo salesStatInfo;
      if (!subSalesStats.TryGetValue(key, out salesStatInfo))
      {
        salesStatInfo = new INUpdateReplenishmentRules.SalesStatInfo();
        salesStatInfo.minDate = initDate;
        subSalesStats.Add(key, salesStatInfo);
      }
      ++num;
    }
    if (num != 0)
      return;
    INSubItem inSubItem = PXResultset<INSubItem>.op_Implicit(PXSelectBase<INSubItem, PXSelectReadonly2<INSubItem, CrossJoin<FeaturesSet>, Where<INSubItem.subItemCD, Equal<INSubItem.Zero>, And<FeaturesSet.subItem, Equal<False>>>>.Config>.Select(aGraph, Array.Empty<object>()));
    if (inSubItem == null || !inSubItem.SubItemID.HasValue)
      return;
    INUpdateReplenishmentRules.PeriodSalesStatInfo aInfo1 = new INUpdateReplenishmentRules.PeriodSalesStatInfo(forecastPeriodType, initDate);
    int key1 = inSubItem.SubItemID.Value;
    List<INUpdateReplenishmentRules.PeriodSalesStatInfo> aList1;
    if (!periodValues.TryGetValue(key1, out aList1))
    {
      aList1 = new List<INUpdateReplenishmentRules.PeriodSalesStatInfo>();
      periodValues.Add(key1, aList1);
    }
    INUpdateReplenishmentRules.AppendToList(aList1, aInfo1);
    INUpdateReplenishmentRules.SalesStatInfo salesStatInfo1;
    if (subSalesStats.TryGetValue(key1, out salesStatInfo1))
      return;
    salesStatInfo1 = new INUpdateReplenishmentRules.SalesStatInfo();
    salesStatInfo1.minDate = initDate;
    subSalesStats.Add(key1, salesStatInfo1);
  }

  public static List<INUpdateReplenishmentRules.PeriodInfo> CreatePeriods(
    string periodType,
    DateTime maxDate,
    int count)
  {
    List<INUpdateReplenishmentRules.PeriodInfo> periods = new List<INUpdateReplenishmentRules.PeriodInfo>(count);
    INUpdateReplenishmentRules.PeriodInfo periodInfo1 = new INUpdateReplenishmentRules.PeriodInfo(periodType, maxDate);
    INUpdateReplenishmentRules.PeriodInfo periodInfo2 = new INUpdateReplenishmentRules.PeriodInfo(periodType, maxDate);
    maxDate = periodInfo1.StartDate;
    for (int index = 0; index < count; ++index)
    {
      INUpdateReplenishmentRules.PeriodInfo periodInfo3 = new INUpdateReplenishmentRules.PeriodInfo(periodInfo1.PeriodType, periodInfo1.StartDate.AddDays(-1.0));
      periods.Add(periodInfo3);
      periodInfo1 = periodInfo3;
    }
    periods.Reverse();
    return periods;
  }

  public static List<INUpdateReplenishmentRules.PeriodInfo> CreatePeriods(
    string periodType,
    DateTime minDate,
    DateTime maxDate,
    bool skipLast)
  {
    List<INUpdateReplenishmentRules.PeriodInfo> periods = new List<INUpdateReplenishmentRules.PeriodInfo>();
    INUpdateReplenishmentRules.PeriodInfo periodInfo1 = new INUpdateReplenishmentRules.PeriodInfo(periodType, maxDate);
    if (!skipLast)
      periods.Add(periodInfo1);
    INUpdateReplenishmentRules.PeriodInfo periodInfo2;
    for (; periodInfo1.StartDate >= minDate; periodInfo1 = periodInfo2)
    {
      periodInfo2 = new INUpdateReplenishmentRules.PeriodInfo(periodInfo1.PeriodType, periodInfo1.StartDate.AddDays(-1.0));
      periods.Add(periodInfo2);
    }
    periods.Reverse();
    return periods;
  }

  public static DemandForecastModel CreateModel(string aModelName)
  {
    switch (aModelName)
    {
      case "CMA":
        return (DemandForecastModel) new MovingAverageModel();
      case "NNN":
        return (DemandForecastModel) null;
      default:
        throw new PXException("The model type {0} is not implemented yet", new object[1]
        {
          (object) aModelName
        });
    }
  }

  protected static List<INUpdateReplenishmentRules.PeriodSalesStatInfo> Normalize(
    List<INUpdateReplenishmentRules.PeriodSalesStatInfo> aSequence,
    INUpdateReplenishmentRules.PeriodInfo maxPeriod)
  {
    List<INUpdateReplenishmentRules.PeriodSalesStatInfo> periodSalesStatInfoList = new List<INUpdateReplenishmentRules.PeriodSalesStatInfo>(aSequence.Count);
    INUpdateReplenishmentRules.PeriodSalesStatInfo periodSalesStatInfo1 = (INUpdateReplenishmentRules.PeriodSalesStatInfo) null;
    foreach (INUpdateReplenishmentRules.PeriodSalesStatInfo periodSalesStatInfo2 in aSequence)
    {
      if (periodSalesStatInfo1 != null)
      {
        if (periodSalesStatInfo1.Period.CompareTo(periodSalesStatInfo2.Period) >= 0)
          throw new PXException("InternalError: Sequence's  sorting order is wrong or it's not sorted");
        while (!periodSalesStatInfo1.Period.IsAdjacent(periodSalesStatInfo2.Period))
        {
          periodSalesStatInfo1 = new INUpdateReplenishmentRules.PeriodSalesStatInfo(periodSalesStatInfo1.Period.PeriodType, periodSalesStatInfo1.Period.EndDate);
          periodSalesStatInfoList.Add(periodSalesStatInfo1);
        }
      }
      periodSalesStatInfoList.Add(periodSalesStatInfo2);
      periodSalesStatInfo1 = periodSalesStatInfo2;
    }
    if (periodSalesStatInfo1 != null)
    {
      if (periodSalesStatInfo1.Period.CompareTo(maxPeriod) >= 0)
        throw new PXException("InternalError: Sequence's  sorting order is wrong or it's not sorted");
      while (!periodSalesStatInfo1.Period.IsAdjacent(maxPeriod))
      {
        periodSalesStatInfo1 = new INUpdateReplenishmentRules.PeriodSalesStatInfo(periodSalesStatInfo1.Period.PeriodType, periodSalesStatInfo1.Period.EndDate);
        periodSalesStatInfoList.Add(periodSalesStatInfo1);
      }
    }
    return periodSalesStatInfoList;
  }

  protected static void AppendToList(
    List<INUpdateReplenishmentRules.PeriodSalesStatInfo> aList,
    INUpdateReplenishmentRules.PeriodSalesStatInfo aInfo)
  {
    int index1 = aList.BinarySearch(aInfo);
    if (index1 < 0)
    {
      int index2 = ~index1;
      aList.Insert(index2, aInfo);
    }
    else
      aList[index1].Append(aInfo);
  }

  protected static void AppendToList(
    List<INUpdateReplenishmentRules.PeriodSalesStatInfo> aDest,
    List<INUpdateReplenishmentRules.PeriodSalesStatInfo> aSrc)
  {
    foreach (INUpdateReplenishmentRules.PeriodSalesStatInfo aInfo in aSrc)
      INUpdateReplenishmentRules.AppendToList(aDest, aInfo);
  }

  protected static void Copy(INItemSite dest, INUpdateReplenishmentRules.SalesStatInfo src)
  {
    dest.DemandPerDayAverage = new Decimal?(src.Average);
    dest.DemandPerDayMSE = new Decimal?(src.MSE);
    dest.DemandPerDayMAD = new Decimal?(src.MAD);
  }

  protected static void Copy(
    INItemSiteReplenishment dest,
    INUpdateReplenishmentRules.SalesStatInfo src)
  {
    dest.DemandPerDayAverage = new Decimal?(src.Average);
    dest.DemandPerDayMSE = new Decimal?(src.MSE);
    dest.DemandPerDayMAD = new Decimal?(src.MAD);
  }

  protected static void ClearReplenishmentInfo(INItemSite dest)
  {
    dest.DemandPerDayAverage = new Decimal?();
    dest.DemandPerDayMSE = new Decimal?();
    dest.DemandPerDayMAD = new Decimal?();
    dest.SafetyStockSuggested = new Decimal?();
    dest.MinQtySuggested = new Decimal?();
    dest.MaxQtySuggested = new Decimal?();
    dest.ForecastModelType = (string) null;
    dest.ForecastPeriodType = (string) null;
    dest.LastForecastDate = new DateTime?();
  }

  protected static void ClearReplenishmentInfo(INItemSiteReplenishment dest)
  {
    dest.DemandPerDayAverage = new Decimal?();
    dest.DemandPerDayMSE = new Decimal?();
    dest.DemandPerDayMAD = new Decimal?();
    dest.SafetyStockSuggested = new Decimal?();
    dest.MinQtySuggested = new Decimal?();
    dest.MaxQtySuggested = new Decimal?();
  }

  [Serializable]
  public class Filter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _ForecastDate;
    protected string _ItemClassCD;
    protected int? _SiteID;
    protected string _ReplenishmentPolicyID;
    protected string _Action;

    /// <summary>
    /// A start date for the <see cref="P:PX.Objects.IN.INUpdateReplenishmentRules.Filter.ForecastDate">forecast date</see> based on the <see cref="F:PX.Objects.IN.DemandPeriodType.Month" /> value.
    /// </summary>
    [PXDate]
    public virtual DateTime? StartDateForDemandPeriodMonth { get; set; }

    /// <summary>
    /// A start date for the <see cref="P:PX.Objects.IN.INUpdateReplenishmentRules.Filter.ForecastDate">forecast date</see> based on the <see cref="F:PX.Objects.IN.DemandPeriodType.Quarter" /> value.
    /// </summary>
    [PXDate]
    public virtual DateTime? StartDateForDemandPeriodQuarter { get; set; }

    /// <summary>
    /// A start date for the <see cref="P:PX.Objects.IN.INUpdateReplenishmentRules.Filter.ForecastDate">forecast date</see> based on the <see cref="F:PX.Objects.IN.DemandPeriodType.Week" /> value.
    /// </summary>
    [PXDate]
    public virtual DateTime? StartDateForDemandPeriodWeek { get; set; }

    /// <summary>
    /// A start date for the <see cref="P:PX.Objects.IN.INUpdateReplenishmentRules.Filter.ForecastDate">forecast date</see> based on the <see cref="F:PX.Objects.IN.DemandPeriodType.Day" /> value.
    /// </summary>
    [PXDate]
    public virtual DateTime? StartDateForDemandPeriodDay { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Forecast Date")]
    [PXDefault(typeof (AccessInfo.businessDate))]
    public virtual DateTime? ForecastDate
    {
      get => this._ForecastDate;
      set => this._ForecastDate = value;
    }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField]
    [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
    public virtual string ItemClassCD
    {
      get => this._ItemClassCD;
      set => this._ItemClassCD = value;
    }

    [PXString(IsUnicode = true)]
    [PXUIField]
    [PXDimension("INITEMCLASS", ParentSelect = typeof (Select<INItemClass>), ParentValueField = typeof (INItemClass.itemClassCD), AutoNumbering = false)]
    public virtual string ItemClassCDWildcard
    {
      get => DimensionTree<INItemClass.dimension>.MakeWildcard(this.ItemClassCD);
      set
      {
      }
    }

    [Site(DisplayName = "Warehouse")]
    public virtual int? SiteID
    {
      get => this._SiteID;
      set => this._SiteID = value;
    }

    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "Seasonality")]
    [PXSelector(typeof (Search<INReplenishmentPolicy.replenishmentPolicyID>), DescriptionField = typeof (INReplenishmentPolicy.descr))]
    public virtual string ReplenishmentPolicyID
    {
      get => this._ReplenishmentPolicyID;
      set => this._ReplenishmentPolicyID = value;
    }

    [INUpdateReplenishmentRules.Filter.Actions.ActionsList]
    [PXDBString(10, IsFixed = false)]
    [PXUIField(DisplayName = "Action")]
    [PXDefault("Calc")]
    public virtual string Action
    {
      get => this._Action;
      set => this._Action = value;
    }

    public abstract class startDateForDemandPeriodMonth : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      INUpdateReplenishmentRules.Filter.startDateForDemandPeriodMonth>
    {
    }

    public abstract class startDateForDemandPeriodQuarter : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      INUpdateReplenishmentRules.Filter.startDateForDemandPeriodQuarter>
    {
    }

    public abstract class startDateForDemandPeriodWeek : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      INUpdateReplenishmentRules.Filter.startDateForDemandPeriodWeek>
    {
    }

    public abstract class startDateForDemandPeriodDay : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      INUpdateReplenishmentRules.Filter.startDateForDemandPeriodDay>
    {
    }

    public abstract class forecastDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      INUpdateReplenishmentRules.Filter.forecastDate>
    {
    }

    public abstract class itemClassCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INUpdateReplenishmentRules.Filter.itemClassCD>
    {
    }

    public abstract class itemClassCDWildcard : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INUpdateReplenishmentRules.Filter.itemClassCDWildcard>
    {
    }

    public abstract class siteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INUpdateReplenishmentRules.Filter.siteID>
    {
    }

    public abstract class replenishmentPolicyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INUpdateReplenishmentRules.Filter.replenishmentPolicyID>
    {
    }

    public abstract class action : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INUpdateReplenishmentRules.Filter.action>
    {
    }

    public static class Actions
    {
      public const string Calculate = "Calc";
      public const string Clear = "Clear";

      public class ActionsListAttribute : PXStringListAttribute
      {
        public ActionsListAttribute()
          : base(new Tuple<string, string>[2]
          {
            PXStringListAttribute.Pair("Calc", "Calculate"),
            PXStringListAttribute.Pair("Clear", "Clear")
          })
        {
        }
      }
    }
  }

  [PXProjection(typeof (Select<InventoryItem>), Persistent = false)]
  [PXHidden]
  [Serializable]
  public class InventoryItemRO : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _InventoryID;
    protected string _InventoryCD;
    protected int? _ItemClassID;
    protected string _ItemStatus;
    protected string _ItemType;

    [PXDBInt(BqlField = typeof (InventoryItem.inventoryID))]
    [PXUIField]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [PXDBString(InputMask = "", IsUnicode = true, BqlField = typeof (InventoryItem.inventoryCD))]
    [PXUIField(DisplayName = "Inventory ID")]
    public virtual string InventoryCD
    {
      get => this._InventoryCD;
      set => this._InventoryCD = value;
    }

    /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.Descr" />
    [PXUIField(DisplayName = "Description")]
    [PXDBString(BqlField = typeof (InventoryItem.descr))]
    public virtual string Descr { get; set; }

    [PXDBInt(BqlField = typeof (InventoryItem.itemClassID))]
    [PXUIField]
    public virtual int? ItemClassID
    {
      get => this._ItemClassID;
      set => this._ItemClassID = value;
    }

    [PXDBString(2, IsFixed = true, BqlField = typeof (InventoryItem.itemStatus))]
    [PXUIField]
    [InventoryItemStatus.List]
    public virtual string ItemStatus
    {
      get => this._ItemStatus;
      set => this._ItemStatus = value;
    }

    [PXDBString(1, IsFixed = true, BqlField = typeof (InventoryItem.itemType))]
    [PXUIField]
    [INItemTypes.List]
    public virtual string ItemType
    {
      get => this._ItemType;
      set => this._ItemType = value;
    }

    /// <summary>
    /// Planning method - Decide which planning method applicable for the stock item.
    /// </summary>
    [PXDBString(1, IsFixed = true, BqlField = typeof (InventoryItem.planningMethod))]
    [PXUIField(DisplayName = "Planning Method")]
    [INPlanningMethod.List]
    public string PlanningMethod { get; set; }

    public class PK : 
      PrimaryKeyOf<INUpdateReplenishmentRules.InventoryItemRO>.By<INUpdateReplenishmentRules.InventoryItemRO.inventoryID>
    {
      public static INUpdateReplenishmentRules.InventoryItemRO Find(
        PXGraph graph,
        int? inventoryID,
        PKFindOptions options = 0)
      {
        return (INUpdateReplenishmentRules.InventoryItemRO) PrimaryKeyOf<INUpdateReplenishmentRules.InventoryItemRO>.By<INUpdateReplenishmentRules.InventoryItemRO.inventoryID>.FindBy(graph, (object) inventoryID, options);
      }
    }

    public static class FK
    {
      public class ItemClass : 
        PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INUpdateReplenishmentRules.InventoryItemRO>.By<INUpdateReplenishmentRules.InventoryItemRO.itemClassID>
      {
      }
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INUpdateReplenishmentRules.InventoryItemRO.inventoryID>
    {
    }

    public abstract class inventoryCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INUpdateReplenishmentRules.InventoryItemRO.inventoryCD>
    {
    }

    public abstract class descr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INUpdateReplenishmentRules.InventoryItemRO.descr>
    {
    }

    public abstract class itemClassID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INUpdateReplenishmentRules.InventoryItemRO.itemClassID>
    {
    }

    public abstract class itemStatus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INUpdateReplenishmentRules.InventoryItemRO.itemStatus>
    {
    }

    public abstract class itemType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INUpdateReplenishmentRules.InventoryItemRO.itemType>
    {
    }

    public abstract class planningMethod : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INUpdateReplenishmentRules.InventoryItemRO.planningMethod>
    {
    }
  }

  [PXProjection(typeof (Select<INTran>), Persistent = false)]
  [PXHidden]
  [Serializable]
  public class INTranSrc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _DocType;
    protected string _RefNbr;
    protected int? _LineNbr;
    protected int? _SiteID;
    protected DateTime? _TranDate;
    protected bool? _Released;

    [PXUIField(DisplayName = "Document Type")]
    [PXDBString(1, IsFixed = true, IsKey = true, BqlField = typeof (INTran.docType))]
    public virtual string DocType
    {
      get => this._DocType;
      set => this._DocType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (INTran.refNbr))]
    public virtual string RefNbr
    {
      get => this._RefNbr;
      set => this._RefNbr = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (INTran.lineNbr))]
    public virtual int? LineNbr
    {
      get => this._LineNbr;
      set => this._LineNbr = value;
    }

    [PXDBInt(BqlField = typeof (INTran.siteID))]
    public virtual int? SiteID
    {
      get => this._SiteID;
      set => this._SiteID = value;
    }

    [PXDBDate(BqlField = typeof (INTran.tranDate))]
    public virtual DateTime? TranDate
    {
      get => this._TranDate;
      set => this._TranDate = value;
    }

    [PXDBBool(BqlField = typeof (INTran.released))]
    public virtual bool? Released
    {
      get => this._Released;
      set => this._Released = value;
    }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INUpdateReplenishmentRules.INTranSrc.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INUpdateReplenishmentRules.INTranSrc.refNbr>
    {
    }

    public abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INUpdateReplenishmentRules.INTranSrc.lineNbr>
    {
    }

    public abstract class siteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INUpdateReplenishmentRules.INTranSrc.siteID>
    {
    }

    public abstract class tranDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      INUpdateReplenishmentRules.INTranSrc.tranDate>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      INUpdateReplenishmentRules.INTranSrc.released>
    {
    }
  }

  public class PeriodInfo : IComparable<INUpdateReplenishmentRules.PeriodInfo>, IComparable<DateTime>
  {
    private string _PeriodType;
    private DateTime _StartDate;
    private DateTime _EndDate;

    public PeriodInfo(string aPeriodType, DateTime aFromDate)
    {
      this._StartDate = INUpdateReplenishmentRules.PeriodInfo.CalcStartDate(aPeriodType, aFromDate);
      this._EndDate = INUpdateReplenishmentRules.PeriodInfo.CalcEndDate(aPeriodType, aFromDate);
      this._PeriodType = aPeriodType;
    }

    public PeriodInfo(INUpdateReplenishmentRules.PeriodInfo aPeriod)
    {
      this._StartDate = aPeriod.StartDate;
      this._EndDate = aPeriod.EndDate;
      this._PeriodType = aPeriod.PeriodType;
    }

    public string PeriodType => this._PeriodType;

    public DateTime StartDate => this._StartDate;

    public DateTime EndDate => this._EndDate;

    public bool IsAdjacent(INUpdateReplenishmentRules.PeriodInfo op)
    {
      return op.StartDate == this.EndDate || op.EndDate == this.StartDate;
    }

    public int CompareTo(DateTime date)
    {
      if (DateTime.Compare(date, this._StartDate) < 0)
        return -1;
      return DateTime.Compare(date, this._EndDate) >= 0 ? 1 : 0;
    }

    public int CompareTo(INUpdateReplenishmentRules.PeriodInfo op)
    {
      return !(op.PeriodType != this.PeriodType) ? DateTime.Compare(this.StartDate, op.StartDate) : throw new Exception("Period of different types can not be compared");
    }

    public static DateTime CalcStartDate(string aPeriodType, DateTime aDate)
    {
      DateTime dateTime = aDate;
      switch (aPeriodType)
      {
        case "MT":
          dateTime = new DateTime(aDate.Year, aDate.Month, 1);
          break;
        case "QT":
          int num1 = (aDate.Month - 1) / 3;
          dateTime = new DateTime(aDate.Year, 1 + num1 * 3, 1);
          break;
        case "WK":
          int num2 = (int) (1 - aDate.DayOfWeek);
          if (num2 > 0)
            num2 -= 7;
          dateTime = aDate.AddDays((double) num2);
          break;
        case "DY":
          dateTime = aDate;
          break;
      }
      return dateTime;
    }

    public static DateTime CalcEndDate(string aPeriodType, DateTime aDate)
    {
      DateTime dateTime = aDate;
      switch (aPeriodType)
      {
        case "MT":
          dateTime = new DateTime(aDate.Year, aDate.Month, 1).AddMonths(1);
          break;
        case "QT":
          int num1 = (aDate.Month - 1) / 3;
          dateTime = new DateTime(aDate.Year, 1 + num1 * 3, 1).AddMonths(3);
          break;
        case "WK":
          int num2 = (int) (1 - aDate.DayOfWeek);
          if (num2 > 0)
            num2 -= 7;
          dateTime = aDate.AddDays((double) (num2 + 7));
          break;
        case "DY":
          dateTime = aDate.AddDays(1.0);
          break;
      }
      return dateTime;
    }

    int IComparable<INUpdateReplenishmentRules.PeriodInfo>.CompareTo(
      INUpdateReplenishmentRules.PeriodInfo other)
    {
      throw new NotImplementedException();
    }
  }

  public class PeriodSalesStatInfo : IComparable<INUpdateReplenishmentRules.PeriodSalesStatInfo>
  {
    protected DateTime SalesStartDate;
    public INUpdateReplenishmentRules.PeriodInfo Period;
    public Decimal SalesPerDay;
    public Decimal SalesPerDaySnAjusted;
    public Decimal SalesTotal;
    public Decimal SalesPerDayMSE;
    public Decimal SeasonalFactor = 1M;

    public PeriodSalesStatInfo(string aPeriodType, DateTime aFromDate)
    {
      this.Period = new INUpdateReplenishmentRules.PeriodInfo(aPeriodType, aFromDate);
      this.SalesStartDate = this.Period.StartDate;
      this.SalesTotal = 0M;
    }

    public PeriodSalesStatInfo(INUpdateReplenishmentRules.PeriodInfo aPeriod)
    {
      this.Period = new INUpdateReplenishmentRules.PeriodInfo(aPeriod);
      this.SalesStartDate = this.Period.StartDate;
      this.SalesTotal = 0M;
    }

    public PeriodSalesStatInfo(
      string aPeriodType,
      INItemSiteHistByCostCenterD aSalesInfo,
      DateTime aSalesStartingDate)
    {
      this.Period = new INUpdateReplenishmentRules.PeriodInfo(aPeriodType, aSalesInfo.SDate.Value);
      this.SalesStartDate = aSalesStartingDate > this.Period.StartDate ? aSalesStartingDate : this.Period.StartDate;
      this.SalesTotal = 0M;
      this.Append(aSalesInfo);
    }

    public virtual int Days => (this.Period.EndDate - this.Period.StartDate).Days;

    public virtual int DaysSinceStart => (this.Period.EndDate - this.SalesStartDate).Days;

    public Decimal Append(INItemSiteHistByCostCenterD aSales)
    {
      Decimal salesTotal = this.SalesTotal;
      Decimal? nullable = aSales.QtySales;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = aSales.QtyCreditMemos;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num1 = valueOrDefault1 - valueOrDefault2;
      nullable = aSales.QtyTransferOut;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      Decimal num2 = num1 + valueOrDefault3;
      nullable = aSales.QtyAssemblyOut;
      Decimal valueOrDefault4 = nullable.GetValueOrDefault();
      Decimal num3 = num2 + valueOrDefault4;
      return this.SalesTotal = salesTotal + num3;
    }

    public INUpdateReplenishmentRules.PeriodSalesStatInfo Append(
      INUpdateReplenishmentRules.PeriodSalesStatInfo op)
    {
      this.SalesTotal += op.SalesTotal;
      if (this.SalesStartDate > op.SalesStartDate)
        this.SalesStartDate = op.SalesStartDate;
      return this;
    }

    public void Recalc()
    {
      if (this.SalesTotal == 0M)
      {
        this.SalesPerDay = 0M;
        this.SalesPerDaySnAjusted = 0M;
      }
      else
      {
        this.SalesPerDay = this.SalesTotal / (Decimal) this.DaysSinceStart;
        this.SalesPerDaySnAjusted = this.SalesTotal / ((Decimal) this.DaysSinceStart * this.SeasonalFactor);
      }
    }

    public void CalcSeasonalFactor(IEnumerable<INReplenishmentSeason> seasonality)
    {
      int year1 = this.Period.StartDate.Year;
      int year2 = this.Period.EndDate.Year;
      int year3 = this.Period.StartDate.Year;
      int year4 = this.Period.StartDate.Year;
      Dictionary<Decimal, int> dictionary = new Dictionary<Decimal, int>();
      foreach (INReplenishmentSeason season in seasonality)
      {
        int num1 = 0;
        KeyValuePair<DateTime, DateTime> range1 = this.GetRange(year3, season);
        TimeSpan timeSpan;
        if ((this.Period.EndDate < range1.Key ? 1 : (this.Period.StartDate > range1.Value ? 1 : 0)) == 0)
        {
          DateTime dateTime1 = this.Period.StartDate > range1.Key ? this.Period.StartDate : range1.Key;
          DateTime dateTime2 = this.Period.EndDate < range1.Value ? this.Period.EndDate : range1.Value;
          int num2 = num1;
          timeSpan = dateTime2 - dateTime1;
          int num3 = Math.Abs(timeSpan.Days);
          num1 = num2 + num3;
        }
        if (year3 != year4)
        {
          KeyValuePair<DateTime, DateTime> range2 = this.GetRange(year3, season);
          if (true)
          {
            DateTime dateTime3 = this.Period.StartDate > range2.Key ? this.Period.StartDate : range2.Key;
            DateTime dateTime4 = this.Period.EndDate < range2.Value ? this.Period.EndDate : range2.Value;
            int num4 = num1;
            timeSpan = dateTime4 - dateTime3;
            int num5 = Math.Abs(timeSpan.Days);
            num1 = num4 + num5;
          }
        }
        if (num1 > 0)
        {
          Decimal key = season.Factor ?? 1M;
          if (!dictionary.TryGetValue(key, out int _))
            dictionary.Add(key, num1);
          else
            dictionary[key] += num1;
        }
      }
      int days = this.Days;
      Decimal num = 0M;
      foreach (KeyValuePair<Decimal, int> keyValuePair in dictionary)
      {
        num += keyValuePair.Key * (Decimal) keyValuePair.Value / (Decimal) this.Days;
        days -= keyValuePair.Value;
      }
      if (days < 0)
        throw new PXException("Seasonal settings are not defined correctly (overlap detected)");
      this.SeasonalFactor = num + 1M * (Decimal) days / (Decimal) this.Days;
    }

    protected virtual KeyValuePair<DateTime, DateTime> GetRange(
      int year,
      INReplenishmentSeason season)
    {
      DateTime dateTime1 = season.StartDate.Value;
      DateTime key = dateTime1.AddYears(year - season.StartDate.Value.Year);
      dateTime1 = season.EndDate.Value;
      DateTime dateTime2 = dateTime1.AddYears(year - season.EndDate.Value.Year);
      return new KeyValuePair<DateTime, DateTime>(key, dateTime2);
    }

    public int CompareTo(
      INUpdateReplenishmentRules.PeriodSalesStatInfo other)
    {
      return this.Period.CompareTo(other.Period);
    }
  }

  public class DataPointAdapter : DemandForecastModel.IDataPoint
  {
    protected INUpdateReplenishmentRules.PeriodSalesStatInfo _info;
    protected readonly DateTime basisDate;

    public DataPointAdapter(INUpdateReplenishmentRules.PeriodSalesStatInfo op)
    {
      this._info = op;
      this._info.Recalc();
      this.basisDate = new DateTime(2004, 1, 1);
    }

    public virtual double X => (double) (this._info.Period.StartDate - this.basisDate).Days;

    public virtual double Y
    {
      get => (double) this._info.SalesPerDay;
      set => this._info.SalesPerDay = (Decimal) value;
    }

    public virtual double YError
    {
      get => (double) this._info.SalesPerDayMSE;
      set => this._info.SalesPerDayMSE = (Decimal) value;
    }
  }

  public class DataPointSeasonalAdapter : INUpdateReplenishmentRules.DataPointAdapter
  {
    public DataPointSeasonalAdapter(
      INUpdateReplenishmentRules.PeriodSalesStatInfo op,
      IEnumerable<INReplenishmentSeason> seansons)
      : base(op)
    {
      this._info.CalcSeasonalFactor(seansons);
      this._info.Recalc();
    }

    public override double Y
    {
      get => (double) this._info.SalesPerDaySnAjusted;
      set => this._info.SalesPerDaySnAjusted = (Decimal) value;
    }
  }

  public class SalesStatInfo
  {
    public Decimal Total;
    public Decimal Average;
    public Decimal MSE;
    public Decimal MAD;
    private Decimal SETotal;
    private Decimal ADTotal;
    public DateTime minDate;
    public int Count;

    public SalesStatInfo()
    {
      this.Total = 0M;
      this.Average = 0M;
      this.MSE = 0M;
      this.SETotal = 0M;
      this.MAD = 0M;
      this.minDate = DateTime.MaxValue;
      this.Count = 0;
    }

    public void Clear()
    {
      this.Total = 0M;
      this.Average = 0M;
      this.MSE = 0M;
      this.SETotal = 0M;
      this.MAD = 0M;
      this.minDate = DateTime.MaxValue;
      this.Count = 0;
    }

    public void CalcDevs(Decimal iValue)
    {
      Decimal num = iValue - this.Average;
      this.ADTotal += Math.Abs(num);
      this.SETotal += num * num;
    }

    public void Recalc()
    {
      this.CalcAverage();
      this.CalcMSE();
      this.CalcMAD();
    }

    public void CalcAverage()
    {
      if (this.Count <= 0)
        return;
      this.Average = this.Total / (Decimal) this.Count;
    }

    private Decimal CalcMSE()
    {
      this.MSE = this.Count <= 1 ? 0M : (Decimal) ((double) this.SETotal / (double) this.Count);
      return this.MSE;
    }

    private Decimal CalcMAD()
    {
      this.MAD = this.Count <= 1 ? this.ADTotal : this.ADTotal / (Decimal) this.Count;
      return this.MAD;
    }
  }

  public static class StatsUtilities
  {
    /// <summary>Implement a Excel NormsInv function</summary>
    public static double NormsInv(double aProbability)
    {
      double num1 = 0.0;
      if (aProbability <= 0.0)
        aProbability = double.Epsilon;
      if (aProbability >= 1.0)
        aProbability = 1.0;
      if (aProbability < double.Epsilon)
      {
        double num2 = Math.Sqrt(-2.0 * Math.Log(aProbability));
        num1 = (((((-0.00778489400243029 * num2 - 0.322396458041136) * num2 - 2.40075827716184) * num2 - 2.54973253934373) * num2 + 4.37466414146497) * num2 + 2.93816398269878) / ((((0.00778469570904146 * num2 + 0.32246712907004) * num2 + 2.445134137143) * num2 + 3.75440866190742) * num2 + 1.0);
      }
      else if (aProbability <= 1.0)
      {
        double num3 = aProbability - 0.5;
        double num4 = num3 * num3;
        num1 = (((((-39.6968302866538 * num4 + 220.946098424521) * num4 - 275.928510446969) * num4 + 138.357751867269) * num4 - 30.6647980661472) * num4 + 2.50662827745924) * num3 / (((((-54.4760987982241 * num4 + 161.585836858041) * num4 - 155.698979859887) * num4 + 66.8013118877197) * num4 - 13.2806815528857) * num4 + 1.0);
      }
      else if (aProbability < 1.0)
      {
        double num5 = Math.Sqrt(-2.0 * Math.Log(1.0 - aProbability));
        num1 = -(((((-0.00778489400243029 * num5 - 0.322396458041136) * num5 - 2.40075827716184) * num5 - 2.54973253934373) * num5 + 4.37466414146497) * num5 + 2.93816398269878) / ((((0.00778469570904146 * num5 + 0.32246712907004) * num5 + 2.445134137143) * num5 + 3.75440866190742) * num5 + 1.0);
      }
      return num1;
    }
  }
}
