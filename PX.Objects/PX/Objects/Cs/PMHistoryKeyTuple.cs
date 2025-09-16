// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PMHistoryKeyTuple
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CS;

public readonly struct PMHistoryKeyTuple(
  int projectID,
  string projectTaskCD,
  int accountGroupID,
  int inventoryID,
  int costCodeID) : IEquatable<PMHistoryKeyTuple>
{
  public readonly int ProjectID = projectID;
  public readonly string ProjectTaskCD = projectTaskCD;
  public readonly int AccountGroupID = accountGroupID;
  public readonly int InventoryID = inventoryID;
  public readonly int CostCodeID = costCodeID;

  public override int GetHashCode()
  {
    return ((((17 * 23 + this.ProjectID.GetHashCode()) * 23 + this.ProjectTaskCD.GetHashCode()) * 23 + this.AccountGroupID.GetHashCode()) * 23 + this.InventoryID.GetHashCode()) * 23 + this.CostCodeID.GetHashCode();
  }

  public override bool Equals(object obj) => obj is PMHistoryKeyTuple other && this.Equals(other);

  public bool Equals(PMHistoryKeyTuple other)
  {
    return this.ProjectID == other.ProjectID && this.ProjectTaskCD == other.ProjectTaskCD && this.AccountGroupID == other.AccountGroupID && this.InventoryID == other.InventoryID && this.CostCodeID == other.CostCodeID;
  }
}
