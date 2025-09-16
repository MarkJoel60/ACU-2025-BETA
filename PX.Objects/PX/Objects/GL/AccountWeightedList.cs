// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountWeightedList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.GL;

public class AccountWeightedList
{
  public System.Collections.Generic.List<AccountWeight> List;
  public Decimal totalWeight;

  public AccountWeightedList() => this.List = new System.Collections.Generic.List<AccountWeight>();

  public virtual void Add(AccountWeight item)
  {
    int index = this.List.BinarySearch(item);
    if (index < 0)
      this.List.Add(item);
    else
      this.List[index].Weight += item.Weight;
    this.totalWeight += item.Weight;
  }

  public virtual int Find(AccountWeight item) => this.List.BinarySearch(item);

  public virtual bool IsExist(int aBranch, int aAccount, int aSub)
  {
    return this.Find(aBranch, aAccount, aSub) >= 0;
  }

  public virtual int Find(int aBranch, int aAccount, int aSub)
  {
    return this.List.BinarySearch(new AccountWeight(aBranch, aAccount, aSub, 0.0M));
  }

  public virtual void Add(int branchID, int accountId, int subID, Decimal weight)
  {
    this.Add(new AccountWeight(branchID, accountId, subID, weight));
  }

  public virtual void Recalculate()
  {
    this.totalWeight = 0.0M;
    foreach (AccountWeight accountWeight in this.List)
      this.totalWeight += accountWeight.Weight;
  }

  public virtual bool isPercent() => Decimal.Round(this.totalWeight, 2) == 100.0M;
}
