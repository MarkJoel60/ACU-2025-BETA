// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.ProjectKey
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AP;

public class ProjectKey
{
  public int? BranchID { get; set; }

  public int? AccountID { get; set; }

  public int? SubID { get; set; }

  public int? ProjectID { get; set; }

  public int? TaskID { get; set; }

  public int? CostCodeID { get; set; }

  public int? InventoryID { get; set; }

  public bool? NonBillable { get; set; }

  public ProjectKey(
    int? branchID,
    int? accountID,
    int? subID,
    int? projectID,
    int? taskID,
    int? costCodeID,
    int? inventoryID,
    bool? nonBillable)
    : this(branchID, accountID, subID, projectID, taskID, costCodeID, inventoryID)
  {
    this.NonBillable = nonBillable;
  }

  public ProjectKey(
    int? branchID,
    int? accountID,
    int? subID,
    int? projectID,
    int? taskID,
    int? costCodeID,
    int? inventoryID)
  {
    this.BranchID = branchID;
    this.AccountID = accountID;
    this.SubID = subID;
    this.ProjectID = projectID;
    this.TaskID = taskID;
    this.CostCodeID = costCodeID;
    this.InventoryID = inventoryID;
  }

  public override int GetHashCode()
  {
    int? nullable = this.BranchID;
    int hashCode1 = nullable.GetHashCode();
    nullable = this.AccountID;
    int hashCode2 = nullable.GetHashCode();
    int num1 = hashCode1 ^ hashCode2;
    nullable = this.SubID;
    int hashCode3 = nullable.GetHashCode();
    int num2 = num1 ^ hashCode3;
    nullable = this.ProjectID;
    int hashCode4 = nullable.GetHashCode();
    int num3 = num2 ^ hashCode4;
    nullable = this.TaskID;
    int hashCode5 = nullable.GetHashCode();
    int num4 = num3 ^ hashCode5;
    nullable = this.CostCodeID;
    int hashCode6 = nullable.GetHashCode();
    int num5 = num4 ^ hashCode6;
    nullable = this.InventoryID;
    int hashCode7 = nullable.GetHashCode();
    return num5 ^ hashCode7 ^ this.NonBillable.GetHashCode();
  }

  public override bool Equals(object obj)
  {
    if (!(obj is ProjectKey))
      return false;
    ProjectKey projectKey = (ProjectKey) obj;
    int? nullable1 = this.BranchID;
    int? branchId = projectKey.BranchID;
    if (nullable1.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable1.HasValue == branchId.HasValue)
    {
      int? accountId = this.AccountID;
      nullable1 = projectKey.AccountID;
      if (accountId.GetValueOrDefault() == nullable1.GetValueOrDefault() & accountId.HasValue == nullable1.HasValue)
      {
        nullable1 = this.SubID;
        int? nullable2 = projectKey.SubID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = this.ProjectID;
          nullable1 = projectKey.ProjectID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            nullable1 = this.TaskID;
            nullable2 = projectKey.TaskID;
            if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            {
              nullable2 = this.CostCodeID;
              nullable1 = projectKey.CostCodeID;
              if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              {
                nullable1 = this.InventoryID;
                nullable2 = projectKey.InventoryID;
                if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                {
                  bool? nonBillable1 = this.NonBillable;
                  bool? nonBillable2 = projectKey.NonBillable;
                  return nonBillable1.GetValueOrDefault() == nonBillable2.GetValueOrDefault() & nonBillable1.HasValue == nonBillable2.HasValue;
                }
              }
            }
          }
        }
      }
    }
    return false;
  }
}
