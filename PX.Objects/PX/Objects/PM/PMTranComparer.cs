// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTranComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class PMTranComparer : IComparer<PMTran>
{
  private bool ByItem;
  private bool ByEmployee;
  private bool ByVendor;
  private bool ByDate;
  private bool ByAccountGroup;
  private bool ByBranch;
  private bool ByFinPeriod;
  private bool ByTranCuryID;

  public PMTranComparer(
    bool? byItem,
    bool? byVendor,
    bool? byDate,
    bool? byEmployee,
    bool byAccountGroup)
    : this(byItem, byVendor, byDate, byEmployee, byAccountGroup, true, true, true)
  {
  }

  public PMTranComparer(
    bool? byItem,
    bool? byVendor,
    bool? byDate,
    bool? byEmployee,
    bool byAccountGroup,
    bool byBranch,
    bool byFinPeriod,
    bool bytranCuryID)
  {
    this.ByItem = byItem.GetValueOrDefault();
    this.ByEmployee = byEmployee.GetValueOrDefault();
    this.ByVendor = byVendor.GetValueOrDefault();
    this.ByDate = byDate.GetValueOrDefault();
    this.ByAccountGroup = byAccountGroup;
    this.ByBranch = byBranch;
    this.ByFinPeriod = byFinPeriod;
    this.ByTranCuryID = bytranCuryID;
  }

  public int Compare(PMTran x, PMTran y)
  {
    int? nullable;
    if (this.ByBranch)
    {
      int? branchId = x.BranchID;
      nullable = y.BranchID;
      if (!(branchId.GetValueOrDefault() == nullable.GetValueOrDefault() & branchId.HasValue == nullable.HasValue))
      {
        nullable = x.BranchID;
        int num1 = nullable.Value;
        ref int local = ref num1;
        nullable = y.BranchID;
        int num2 = nullable.Value;
        return local.CompareTo(num2);
      }
    }
    if (this.ByFinPeriod && x.FinPeriodID != y.FinPeriodID)
      return x.FinPeriodID.CompareTo(y.FinPeriodID);
    if (this.ByTranCuryID && x.TranCuryID != y.TranCuryID)
      return x.TranCuryID.CompareTo(y.TranCuryID);
    if (this.ByAccountGroup)
    {
      nullable = x.AccountGroupID;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = y.AccountGroupID;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      if (valueOrDefault1 != valueOrDefault2)
        return valueOrDefault1.CompareTo(valueOrDefault2);
    }
    if (this.ByItem)
    {
      nullable = x.InventoryID;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      nullable = y.InventoryID;
      int valueOrDefault4 = nullable.GetValueOrDefault();
      if (valueOrDefault3 != valueOrDefault4)
        return valueOrDefault3.CompareTo(valueOrDefault4);
    }
    if (this.ByEmployee)
    {
      nullable = x.ResourceID;
      int valueOrDefault5 = nullable.GetValueOrDefault();
      nullable = y.ResourceID;
      int valueOrDefault6 = nullable.GetValueOrDefault();
      if (valueOrDefault5 != valueOrDefault6)
        return valueOrDefault5.CompareTo(valueOrDefault6);
    }
    if (this.ByVendor)
    {
      nullable = x.BAccountID;
      int valueOrDefault7 = nullable.GetValueOrDefault();
      nullable = y.BAccountID;
      int valueOrDefault8 = nullable.GetValueOrDefault();
      if (valueOrDefault7 != valueOrDefault8)
        return valueOrDefault7.CompareTo(valueOrDefault8);
    }
    return this.ByDate ? x.Date.Value.CompareTo(y.Date.Value) : x.Billable.GetValueOrDefault().CompareTo(y.Billable.GetValueOrDefault());
  }
}
