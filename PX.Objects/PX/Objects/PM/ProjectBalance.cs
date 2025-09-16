// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectBalance
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class ProjectBalance
{
  protected PXGraph graph;
  protected IProjectSettingsManager settings;
  protected IBudgetService service;

  public IProjectSettingsManager Settings => this.settings;

  public ProjectBalance(PXGraph graph)
    : this(graph, (IBudgetService) new BudgetService(graph), ServiceLocator.Current.GetInstance<IProjectSettingsManager>())
  {
  }

  public ProjectBalance(PXGraph graph, IBudgetService service, IProjectSettingsManager settings)
  {
    this.graph = graph ?? throw new ArgumentNullException(nameof (graph));
    this.service = service ?? throw new ArgumentNullException(nameof (service));
    this.settings = settings ?? throw new ArgumentNullException(nameof (settings));
  }

  [Obsolete]
  public virtual List<ProjectBalance.Result> Calculate(
    PMProject project,
    PMTran tran,
    PX.Objects.GL.Account acc,
    PMAccountGroup ag,
    PX.Objects.GL.Account offsetAcc,
    PMAccountGroup offsetAg)
  {
    return this.Calculate(project, tran, ag, offsetAg);
  }

  public virtual List<ProjectBalance.Result> Calculate(
    PMProject project,
    PMTran tran,
    PMAccountGroup ag,
    PMAccountGroup offsetAg)
  {
    List<ProjectBalance.Result> resultList = new List<ProjectBalance.Result>();
    if (!project.NonProject.GetValueOrDefault() && tran.TaskID.HasValue)
    {
      bool flag = false;
      int? groupId1;
      if (offsetAg != null)
      {
        int? groupId2 = ag.GroupID;
        groupId1 = offsetAg.GroupID;
        if (groupId2.GetValueOrDefault() == groupId1.GetValueOrDefault() & groupId2.HasValue == groupId1.HasValue)
          flag = true;
      }
      if (!flag)
      {
        if (ag.Type == "I" || ag.Type == "L")
          resultList.Add(this.Calculate(project, tran, ag, -1, 1));
        else
          resultList.Add(this.Calculate(project, tran, ag, 1, 1));
        int num;
        if (offsetAg != null)
        {
          groupId1 = offsetAg.GroupID;
          num = groupId1.HasValue ? 1 : 0;
        }
        else
          num = 0;
        if (num != 0)
        {
          if (offsetAg.Type == "I" || offsetAg.Type == "L")
            resultList.Add(this.Calculate(project, tran, offsetAg, 1, -1));
          else
            resultList.Add(this.Calculate(project, tran, offsetAg, -1, -1));
        }
      }
    }
    return resultList;
  }

  public virtual ProjectBalance.Result Calculate(
    PMProject project,
    PMTran pmt,
    PMAccountGroup ag,
    string accountType,
    int amountSign,
    int qtySign)
  {
    return this.Calculate(project, pmt, ag, amountSign, qtySign);
  }

  public virtual ProjectBalance.Result Calculate(
    PMProject project,
    PMTran pmt,
    PMAccountGroup ag,
    int amountSign,
    int qtySign)
  {
    PX.Objects.PM.Lite.PMBudget budget = this.service.SelectProjectBalance((IProjectFilter) pmt, ag, project, out bool _);
    Decimal rollupQty = this.CalculateRollupQty<PMTran>(pmt, (IQuantify) budget);
    List<PMHistory> history = new List<PMHistory>();
    PMTaskTotal taskTotal = (PMTaskTotal) null;
    PMBudget status = (PMBudget) null;
    PMForecastHistory forecast = (PMForecastHistory) null;
    if (pmt.TaskID.HasValue)
    {
      Decimal? nullable1;
      if (!(rollupQty != 0M))
      {
        nullable1 = pmt.Amount;
        Decimal num = 0M;
        if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
          goto label_22;
      }
      status = new PMBudget();
      status.ProjectID = budget.ProjectID;
      status.ProjectTaskID = budget.TaskID;
      status.AccountGroupID = budget.AccountGroupID;
      status.Type = budget.Type;
      status.InventoryID = budget.InventoryID;
      status.CostCodeID = budget.CostCodeID;
      status.UOM = budget.UOM;
      status.IsProduction = budget.IsProduction;
      status.Description = budget.Description;
      if (!status.CuryInfoID.HasValue)
        status.CuryInfoID = project.CuryInfoID;
      Decimal num1 = (Decimal) amountSign;
      nullable1 = pmt.Amount;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      Decimal num2 = num1 * valueOrDefault1;
      Decimal num3 = (Decimal) amountSign;
      nullable1 = pmt.ProjectCuryAmount;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal num4 = num3 * valueOrDefault2;
      status.ActualQty = new Decimal?(rollupQty * (Decimal) qtySign);
      status.ActualAmount = new Decimal?(num2);
      status.CuryActualAmount = new Decimal?(num4);
      taskTotal = new PMTaskTotal();
      taskTotal.ProjectID = status.ProjectID;
      taskTotal.TaskID = status.TaskID;
      switch (ag.IsExpense.GetValueOrDefault() ? "E" : ag.Type)
      {
        case "A":
          taskTotal.CuryAsset = new Decimal?(num4);
          taskTotal.Asset = new Decimal?(num2);
          break;
        case "L":
          taskTotal.CuryLiability = new Decimal?(num4);
          taskTotal.Liability = new Decimal?(num2);
          break;
        case "I":
          taskTotal.CuryIncome = new Decimal?(num4);
          taskTotal.Income = new Decimal?(num2);
          break;
        case "E":
          taskTotal.CuryExpense = new Decimal?(num4);
          taskTotal.Expense = new Decimal?(num2);
          break;
      }
      PMHistory pmHistory1 = new PMHistory();
      pmHistory1.ProjectID = status.ProjectID;
      pmHistory1.ProjectTaskID = status.TaskID;
      pmHistory1.AccountGroupID = status.AccountGroupID;
      PMHistory pmHistory2 = pmHistory1;
      int? nullable2 = pmt.InventoryID;
      int? nullable3 = nullable2 ?? status.InventoryID;
      pmHistory2.InventoryID = nullable3;
      PMHistory pmHistory3 = pmHistory1;
      nullable2 = pmt.CostCodeID;
      int? nullable4 = nullable2 ?? status.CostCodeID;
      pmHistory3.CostCodeID = nullable4;
      pmHistory1.PeriodID = pmt.FinPeriodID;
      pmHistory1.BranchID = pmt.BranchID;
      Decimal num5 = 0M;
      history.Add(pmHistory1);
      nullable2 = pmt.InventoryID;
      if (nullable2.HasValue)
      {
        nullable2 = pmt.InventoryID;
        int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
        if (!(nullable2.GetValueOrDefault() == emptyInventoryId & nullable2.HasValue))
        {
          nullable1 = pmt.Qty;
          Decimal num6 = 0M;
          if (!(nullable1.GetValueOrDefault() == num6 & nullable1.HasValue))
          {
            if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
            {
              Decimal num7 = (Decimal) qtySign;
              PXCache cach = this.graph.Caches[typeof (PMHistory)];
              int? inventoryId = pmt.InventoryID;
              string uom = pmt.UOM;
              nullable1 = pmt.Qty;
              Decimal num8 = nullable1.Value;
              Decimal num9 = INUnitAttribute.ConvertToBase(cach, inventoryId, uom, num8, INPrecision.QUANTITY);
              num5 = num7 * num9;
            }
            else if (PXSelectorAttribute.Select<PMTran.inventoryID>(this.graph.Caches[typeof (PMTran)], (object) pmt) is PX.Objects.IN.InventoryItem inventoryItem && !string.IsNullOrEmpty(pmt.UOM))
            {
              Decimal num10 = (Decimal) qtySign;
              PXGraph graph = this.graph;
              string uom = pmt.UOM;
              string baseUnit = inventoryItem.BaseUnit;
              nullable1 = pmt.Qty;
              Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
              Decimal num11 = INUnitAttribute.ConvertGlobalUnits(graph, uom, baseUnit, valueOrDefault3, INPrecision.QUANTITY);
              num5 = num10 * num11;
            }
          }
        }
      }
      pmHistory1.FinPTDCuryAmount = new Decimal?(num4);
      pmHistory1.FinPTDAmount = new Decimal?(num2);
      pmHistory1.FinYTDCuryAmount = new Decimal?(num4);
      pmHistory1.FinYTDAmount = new Decimal?(num2);
      pmHistory1.FinPTDQty = new Decimal?(num5);
      pmHistory1.FinYTDQty = new Decimal?(num5);
      if (pmt.FinPeriodID == pmt.TranPeriodID)
      {
        pmHistory1.TranPTDCuryAmount = new Decimal?(num4);
        pmHistory1.TranPTDAmount = new Decimal?(num2);
        pmHistory1.TranYTDCuryAmount = new Decimal?(num4);
        pmHistory1.TranYTDAmount = new Decimal?(num2);
        pmHistory1.TranPTDQty = new Decimal?(num5);
        pmHistory1.TranYTDQty = new Decimal?(num5);
      }
      else
      {
        PMHistory pmHistory4 = new PMHistory();
        pmHistory4.ProjectID = status.ProjectID;
        pmHistory4.ProjectTaskID = status.TaskID;
        pmHistory4.AccountGroupID = status.AccountGroupID;
        PMHistory pmHistory5 = pmHistory4;
        nullable2 = pmt.InventoryID;
        int? nullable5 = new int?(nullable2 ?? PMInventorySelectorAttribute.EmptyInventoryID);
        pmHistory5.InventoryID = nullable5;
        PMHistory pmHistory6 = pmHistory4;
        nullable2 = pmt.CostCodeID;
        int? nullable6 = new int?(nullable2 ?? CostCodeAttribute.GetDefaultCostCode());
        pmHistory6.CostCodeID = nullable6;
        pmHistory4.PeriodID = pmt.TranPeriodID;
        pmHistory4.BranchID = pmt.BranchID;
        history.Add(pmHistory4);
        pmHistory4.TranPTDCuryAmount = new Decimal?(num4);
        pmHistory4.TranPTDAmount = new Decimal?(num2);
        pmHistory4.TranYTDCuryAmount = new Decimal?(num4);
        pmHistory4.TranYTDAmount = new Decimal?(num2);
        pmHistory4.TranPTDQty = new Decimal?(num5);
        pmHistory4.TranYTDQty = new Decimal?(num5);
      }
      forecast = new PMForecastHistory();
      forecast.ProjectID = status.ProjectID;
      forecast.ProjectTaskID = status.ProjectTaskID;
      forecast.AccountGroupID = status.AccountGroupID;
      forecast.InventoryID = status.InventoryID;
      forecast.CostCodeID = status.CostCodeID;
      forecast.PeriodID = pmt.TranPeriodID;
      forecast.ActualQty = status.ActualQty;
      forecast.CuryActualAmount = status.CuryActualAmount;
      forecast.ActualAmount = status.ActualAmount;
      if (pmt.TranType == "AR")
        forecast.CuryArAmount = forecast.CuryActualAmount;
    }
label_22:
    return new ProjectBalance.Result((IList<PMHistory>) history, status, taskTotal, forecast);
  }

  public virtual Decimal CalculateRollupQty<T>(T row, IQuantify budget) where T : IBqlTable, IQuantify
  {
    return this.CalculateRollupQty<T>(row, budget, row.Qty);
  }

  public virtual Decimal CalculateRollupQty<T>(T row, IQuantify budget, Decimal? quantity) where T : IBqlTable, IQuantify
  {
    return this.CalculateRollupQty<T>(row, budget, quantity.GetValueOrDefault());
  }

  private Decimal CalculateRollupQty<T>(T row, IQuantify budget, Decimal quantity) where T : IBqlTable, IQuantify
  {
    if (string.IsNullOrEmpty(budget.UOM) || string.IsNullOrEmpty(row.UOM) || quantity == 0M)
      return 0M;
    if (budget.UOM == row.UOM)
      return quantity;
    Decimal rollupQty = 0M;
    int? inventoryId = budget.InventoryID;
    int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
    if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue)
    {
      Decimal result;
      if (INUnitAttribute.TryConvertGlobalUnits(this.graph, row.UOM, budget.UOM, quantity, INPrecision.QUANTITY, out result))
        rollupQty = result;
    }
    else if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
    {
      Decimal num = INUnitAttribute.ConvertToBase(this.graph.Caches[typeof (T)], row.InventoryID, row.UOM, quantity, INPrecision.QUANTITY);
      try
      {
        rollupQty = INUnitAttribute.ConvertFromBase(this.graph.Caches[typeof (T)], row.InventoryID, budget.UOM, num, INPrecision.QUANTITY);
      }
      catch (PXUnitConversionException ex)
      {
        PX.Objects.IN.InventoryItem inventoryItem = PXSelectorAttribute.Select(this.graph.Caches[typeof (T)], (object) row, "inventoryID") as PX.Objects.IN.InventoryItem;
        throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Failed to Convert from {0} to {1} when updating the Budget for the Project. Unit conversion is not defined for {2}", new object[3]
        {
          (object) inventoryItem?.BaseUnit,
          (object) budget.UOM,
          (object) inventoryItem?.InventoryCD
        }), (Exception) ex);
      }
    }
    else
      rollupQty = INUnitAttribute.ConvertGlobalUnits(this.graph, row.UOM, budget.UOM, quantity, INPrecision.QUANTITY);
    return rollupQty;
  }

  public static bool IsFlipRequired(string accountType, string accountGroupType)
  {
    return !string.IsNullOrEmpty(accountGroupType) && !string.IsNullOrEmpty(accountType) && !(accountType == accountGroupType) && (!(accountType == "L") || !(accountGroupType == "I")) && (!(accountType == "I") || !(accountGroupType == "L")) && (!(accountType == "A") || !(accountGroupType == "E")) && (!(accountType == "E") || !(accountGroupType == "A")) && accountType != accountGroupType;
  }

  public class Result
  {
    public Result(
      IList<PMHistory> history,
      PMBudget status,
      PMTaskTotal taskTotal,
      PMForecastHistory forecast)
    {
      this.History = history;
      this.Status = status;
      this.TaskTotal = taskTotal;
      this.ForecastHistory = forecast;
    }

    public IList<PMHistory> History { get; protected set; }

    public PMBudget Status { get; protected set; }

    public PMTaskTotal TaskTotal { get; protected set; }

    public PMForecastHistory ForecastHistory { get; protected set; }
  }
}
