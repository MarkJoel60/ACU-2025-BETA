// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.BudgetKeyTuple
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.PM;
using System;
using System.Diagnostics;

#nullable disable
namespace PX.Objects.CS;

[DebuggerDisplay("{ProjectID}.{ProjectTaskID}.{AccountGroupID}.{InventoryID}.{CostCodeID}")]
public readonly struct BudgetKeyTuple(
  int projectID,
  int projectTaskID,
  int accountGroupID,
  int inventoryID,
  int costCodeID) : IEquatable<BudgetKeyTuple>, IProjectFilter
{
  public readonly int ProjectID = projectID;
  public readonly int ProjectTaskID = projectTaskID;
  public readonly int AccountGroupID = accountGroupID;
  public readonly int InventoryID = inventoryID;
  public readonly int CostCodeID = costCodeID;

  public static BudgetKeyTuple Create(IProjectFilter budget)
  {
    int? nullable = budget.ProjectID;
    int valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = budget.TaskID;
    int valueOrDefault2 = nullable.GetValueOrDefault();
    nullable = budget.AccountGroupID;
    int valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = budget.InventoryID;
    int inventoryID = nullable ?? PMInventorySelectorAttribute.EmptyInventoryID;
    nullable = budget.CostCodeID;
    int costCodeID = nullable ?? CostCodeAttribute.GetDefaultCostCode();
    return new BudgetKeyTuple(valueOrDefault1, valueOrDefault2, valueOrDefault3, inventoryID, costCodeID);
  }

  public override int GetHashCode()
  {
    return ((((17 * 23 + this.ProjectID.GetHashCode()) * 23 + this.ProjectTaskID.GetHashCode()) * 23 + this.AccountGroupID.GetHashCode()) * 23 + this.InventoryID.GetHashCode()) * 23 + this.CostCodeID.GetHashCode();
  }

  public override bool Equals(object obj) => obj is BudgetKeyTuple other && this.Equals(other);

  public bool Equals(BudgetKeyTuple other)
  {
    return this.ProjectID == other.ProjectID && this.ProjectTaskID == other.ProjectTaskID && this.AccountGroupID == other.AccountGroupID && this.InventoryID == other.InventoryID && this.CostCodeID == other.CostCodeID;
  }

  int? IProjectFilter.AccountGroupID => new int?(this.AccountGroupID);

  int? IProjectFilter.ProjectID => new int?(this.ProjectID);

  int? IProjectFilter.TaskID => new int?(this.ProjectTaskID);

  int? IProjectFilter.InventoryID => new int?(this.InventoryID);

  int? IProjectFilter.CostCodeID => new int?(this.CostCodeID);
}
