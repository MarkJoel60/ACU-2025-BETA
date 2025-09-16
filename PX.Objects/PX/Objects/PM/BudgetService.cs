// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.BudgetService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class BudgetService : IBudgetService
{
  protected PXGraph graph;
  protected IProjectSettingsManager settings;

  public BudgetService(PXGraph graph)
    : this(graph, ServiceLocator.Current.GetInstance<IProjectSettingsManager>())
  {
  }

  public BudgetService(PXGraph graph, IProjectSettingsManager settings)
  {
    this.graph = graph ?? throw new ArgumentNullException(nameof (graph));
    this.settings = settings ?? throw new ArgumentNullException(nameof (settings));
  }

  public virtual PX.Objects.PM.Lite.PMBudget SelectProjectBalance(
    IProjectFilter filter,
    PMAccountGroup ag,
    PMProject project,
    out bool isExisting)
  {
    return this.SelectProjectBalance(ag, project, filter.TaskID, filter.InventoryID, filter.CostCodeID, out isExisting);
  }

  public virtual PX.Objects.PM.Lite.PMBudget SelectExistingProjectBalance(
    int? projectId,
    int? taskID,
    int? accountGroupId,
    int? costCodeID,
    int? inventoryID)
  {
    bool isExisting;
    PX.Objects.PM.Lite.PMBudget pmBudget = this.SelectProjectBalance(PMAccountGroup.PK.Find(this.graph, accountGroupId), PMProject.PK.Find(this.graph, projectId), taskID, inventoryID, costCodeID, out isExisting);
    return !isExisting ? (PX.Objects.PM.Lite.PMBudget) null : pmBudget;
  }

  public virtual PX.Objects.PM.Lite.PMBudget SelectProjectBalance(
    PMAccountGroup ag,
    PMProject project,
    int? taskID,
    int? inventoryID,
    int? costCodeID,
    out bool isExisting)
  {
    isExisting = false;
    if (ag == null || project == null || !taskID.HasValue)
      return (PX.Objects.PM.Lite.PMBudget) null;
    BudgetKeyTuple key;
    ref BudgetKeyTuple local = ref key;
    int? nullable = project.ContractID;
    int projectID = nullable.Value;
    int projectTaskID = taskID.Value;
    nullable = ag.GroupID;
    int accountGroupID = nullable.Value;
    int inventoryID1 = inventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID;
    int costCodeID1 = costCodeID ?? CostCodeAttribute.GetDefaultCostCode();
    local = new BudgetKeyTuple(projectID, projectTaskID, accountGroupID, inventoryID1, costCodeID1);
    string budgetLevel = "T";
    string budgetUpdateMode = "S";
    if (ag.Type == "I")
    {
      budgetLevel = project.BudgetLevel;
      budgetUpdateMode = this.settings.RevenueBudgetUpdateMode;
    }
    else if (ag.IsExpense.GetValueOrDefault())
    {
      budgetLevel = project.CostBudgetLevel;
      budgetUpdateMode = this.settings.CostBudgetUpdateMode;
    }
    else if (ag.Type == "A" || ag.Type == "L")
    {
      budgetLevel = !CostCodeAttribute.UseCostCode() ? "I" : "D";
      budgetUpdateMode = "D";
    }
    PX.Objects.PM.Lite.PMBudget pmBudget = this.SelectExistingBalance(key, budgetLevel, budgetUpdateMode);
    if (pmBudget != null)
    {
      isExisting = true;
    }
    else
    {
      isExisting = false;
      pmBudget = this.BuildTarget(key, ag, budgetLevel, budgetUpdateMode);
    }
    return pmBudget;
  }

  protected virtual PX.Objects.PM.Lite.PMBudget SelectExistingBalance(
    BudgetKeyTuple key,
    string budgetLevel,
    string budgetUpdateMode)
  {
    PX.Objects.PM.Lite.PMBudget pmBudget1 = (PX.Objects.PM.Lite.PMBudget) null;
    PX.Objects.PM.Lite.PMBudget pmBudget2 = (PX.Objects.PM.Lite.PMBudget) null;
    foreach (PX.Objects.PM.Lite.PMBudget selectExistingBalance in this.SelectExistingBalances(key, budgetLevel, budgetUpdateMode))
    {
      int? nullable1 = selectExistingBalance.CostCodeID;
      int costCodeId1 = key.CostCodeID;
      if (nullable1.GetValueOrDefault() == costCodeId1 & nullable1.HasValue)
      {
        nullable1 = selectExistingBalance.InventoryID;
        int inventoryId = key.InventoryID;
        if (nullable1.GetValueOrDefault() == inventoryId & nullable1.HasValue)
        {
          pmBudget1 = selectExistingBalance;
          break;
        }
      }
      int? nullable2;
      if (budgetLevel == "C")
      {
        nullable1 = selectExistingBalance.InventoryID;
        int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
        if (nullable1.GetValueOrDefault() == emptyInventoryId & nullable1.HasValue)
        {
          nullable1 = selectExistingBalance.CostCodeID;
          int costCodeId2 = key.CostCodeID;
          if (nullable1.GetValueOrDefault() == costCodeId2 & nullable1.HasValue)
          {
            pmBudget1 = selectExistingBalance;
            break;
          }
        }
      }
      else
      {
        nullable1 = selectExistingBalance.InventoryID;
        int inventoryId = key.InventoryID;
        if (nullable1.GetValueOrDefault() == inventoryId & nullable1.HasValue)
        {
          nullable1 = selectExistingBalance.CostCodeID;
          nullable2 = CostCodeAttribute.DefaultCostCode;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            pmBudget1 = selectExistingBalance;
            break;
          }
        }
      }
      nullable2 = selectExistingBalance.InventoryID;
      int emptyInventoryId1 = PMInventorySelectorAttribute.EmptyInventoryID;
      if (nullable2.GetValueOrDefault() == emptyInventoryId1 & nullable2.HasValue)
      {
        nullable2 = selectExistingBalance.CostCodeID;
        nullable1 = CostCodeAttribute.DefaultCostCode;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          pmBudget2 = selectExistingBalance;
      }
    }
    return pmBudget1 ?? pmBudget2;
  }

  protected virtual List<PX.Objects.PM.Lite.PMBudget> SelectExistingBalances(
    BudgetKeyTuple key,
    string budgetLevel,
    string budgetUpdateMode)
  {
    List<int?> nullableList1 = new List<int?>();
    List<int?> nullableList2 = new List<int?>();
    switch (budgetLevel)
    {
      case "T":
        nullableList2.Add(new int?(PMInventorySelectorAttribute.EmptyInventoryID));
        nullableList1.Add(CostCodeAttribute.DefaultCostCode);
        break;
      case "I":
        nullableList2.Add(new int?(key.InventoryID));
        nullableList1.Add(CostCodeAttribute.DefaultCostCode);
        if (budgetUpdateMode == "S" && key.InventoryID != PMInventorySelectorAttribute.EmptyInventoryID)
        {
          nullableList2.Add(new int?(PMInventorySelectorAttribute.EmptyInventoryID));
          break;
        }
        break;
      case "C":
        nullableList2.Add(new int?(PMInventorySelectorAttribute.EmptyInventoryID));
        nullableList1.Add(new int?(key.CostCodeID));
        if (budgetUpdateMode == "S")
        {
          int costCodeId = key.CostCodeID;
          int? defaultCostCode = CostCodeAttribute.DefaultCostCode;
          int valueOrDefault = defaultCostCode.GetValueOrDefault();
          if (!(costCodeId == valueOrDefault & defaultCostCode.HasValue))
          {
            nullableList1.Add(CostCodeAttribute.DefaultCostCode);
            break;
          }
          break;
        }
        break;
      case "D":
        nullableList2.Add(new int?(key.InventoryID));
        nullableList1.Add(new int?(key.CostCodeID));
        if (budgetUpdateMode == "S" && key.InventoryID != PMInventorySelectorAttribute.EmptyInventoryID)
          nullableList2.Add(new int?(PMInventorySelectorAttribute.EmptyInventoryID));
        if (budgetUpdateMode == "S")
        {
          int costCodeId = key.CostCodeID;
          int? defaultCostCode = CostCodeAttribute.DefaultCostCode;
          int valueOrDefault = defaultCostCode.GetValueOrDefault();
          if (!(costCodeId == valueOrDefault & defaultCostCode.HasValue))
          {
            nullableList1.Add(CostCodeAttribute.DefaultCostCode);
            break;
          }
          break;
        }
        break;
      default:
        throw new ArgumentException($"Unknown budget level = {budgetLevel}", nameof (budgetLevel));
    }
    return this.SelectExistingBalances(key.ProjectID, key.ProjectTaskID, key.AccountGroupID, nullableList1.ToArray(), nullableList2.ToArray());
  }

  protected virtual List<PX.Objects.PM.Lite.PMBudget> SelectExistingBalances(
    int projectID,
    int taskID,
    int accountGroupID,
    int?[] costCodes,
    int?[] items)
  {
    List<PX.Objects.PM.Lite.PMBudget> pmBudgetList = new List<PX.Objects.PM.Lite.PMBudget>();
    foreach (PXResult<PX.Objects.PM.Lite.PMBudget> pxResult in ((PXSelectBase<PX.Objects.PM.Lite.PMBudget>) new PXSelectReadonly<PX.Objects.PM.Lite.PMBudget, Where<PX.Objects.PM.Lite.PMBudget.accountGroupID, Equal<Required<PX.Objects.PM.Lite.PMBudget.accountGroupID>>, And<PX.Objects.PM.Lite.PMBudget.projectID, Equal<Required<PX.Objects.PM.Lite.PMBudget.projectID>>, And<PX.Objects.PM.Lite.PMBudget.projectTaskID, Equal<Required<PX.Objects.PM.Lite.PMBudget.projectTaskID>>, And<PX.Objects.PM.Lite.PMBudget.costCodeID, In<Required<PX.Objects.PM.Lite.PMBudget.costCodeID>>, And<PX.Objects.PM.Lite.PMBudget.inventoryID, In<Required<PX.Objects.PM.Lite.PMBudget.inventoryID>>>>>>>>(this.graph)).Select(new object[5]
    {
      (object) accountGroupID,
      (object) projectID,
      (object) taskID,
      (object) ((IEnumerable<int?>) costCodes).ToArray<int?>(),
      (object) ((IEnumerable<int?>) items).ToArray<int?>()
    }))
    {
      PX.Objects.PM.Lite.PMBudget pmBudget = PXResult<PX.Objects.PM.Lite.PMBudget>.op_Implicit(pxResult);
      pmBudgetList.Add(pmBudget);
    }
    return pmBudgetList;
  }

  protected virtual PX.Objects.PM.Lite.PMBudget BuildTarget(
    BudgetKeyTuple key,
    PMAccountGroup accountGroup,
    string budgetLevel,
    string budgetUpdateMode)
  {
    int? nullable = accountGroup != null ? accountGroup.GroupID : throw new ArgumentNullException(nameof (accountGroup));
    int accountGroupId = key.AccountGroupID;
    if (!(nullable.GetValueOrDefault() == accountGroupId & nullable.HasValue))
      throw new ArgumentException("AccountGroup doesnot match key.AccountGroupID");
    PX.Objects.PM.Lite.PMBudget pmBudget = new PX.Objects.PM.Lite.PMBudget();
    pmBudget.ProjectID = new int?(key.ProjectID);
    pmBudget.ProjectTaskID = new int?(key.ProjectTaskID);
    pmBudget.AccountGroupID = new int?(key.AccountGroupID);
    pmBudget.InventoryID = new int?(PMInventorySelectorAttribute.EmptyInventoryID);
    pmBudget.CostCodeID = new int?(CostCodeAttribute.GetDefaultCostCode());
    pmBudget.IsProduction = new bool?(false);
    pmBudget.Type = accountGroup.IsExpense.GetValueOrDefault() ? "E" : accountGroup.Type;
    switch (budgetLevel)
    {
      case "T":
        return pmBudget;
      case "I":
        pmBudget.InventoryID = new int?(budgetUpdateMode == "S" ? PMInventorySelectorAttribute.EmptyInventoryID : key.InventoryID);
        nullable = pmBudget.InventoryID;
        int emptyInventoryId1 = PMInventorySelectorAttribute.EmptyInventoryID;
        if (!(nullable.GetValueOrDefault() == emptyInventoryId1 & nullable.HasValue))
        {
          PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(this.graph, new object[1]
          {
            (object) pmBudget.InventoryID
          }));
          if (inventoryItem != null)
          {
            pmBudget.Description = inventoryItem.Descr;
            pmBudget.UOM = inventoryItem.BaseUnit;
            goto case "T";
          }
          goto case "T";
        }
        goto case "T";
      case "C":
        pmBudget.CostCodeID = new int?(budgetUpdateMode == "S" ? CostCodeAttribute.GetDefaultCostCode() : key.CostCodeID);
        PMCostCode pmCostCode1 = PMCostCode.PK.Find(this.graph, pmBudget.CostCodeID);
        if (pmCostCode1 != null)
        {
          pmBudget.Description = pmCostCode1.Description;
          goto case "T";
        }
        goto case "T";
      case "D":
        pmBudget.InventoryID = new int?(budgetUpdateMode == "S" ? PMInventorySelectorAttribute.EmptyInventoryID : key.InventoryID);
        pmBudget.CostCodeID = new int?(budgetUpdateMode == "S" ? CostCodeAttribute.GetDefaultCostCode() : key.CostCodeID);
        PMCostCode pmCostCode2 = PMCostCode.PK.Find(this.graph, pmBudget.CostCodeID);
        if (pmCostCode2 != null)
          pmBudget.Description = pmCostCode2.Description;
        nullable = pmBudget.InventoryID;
        int emptyInventoryId2 = PMInventorySelectorAttribute.EmptyInventoryID;
        if (!(nullable.GetValueOrDefault() == emptyInventoryId2 & nullable.HasValue))
        {
          PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(this.graph, new object[1]
          {
            (object) pmBudget.InventoryID
          }));
          if (inventoryItem != null)
          {
            pmBudget.UOM = inventoryItem.BaseUnit;
            goto case "T";
          }
          goto case "T";
        }
        goto case "T";
      default:
        throw new ArgumentException($"Unknown budget level = {budgetLevel}", nameof (budgetLevel));
    }
  }
}
