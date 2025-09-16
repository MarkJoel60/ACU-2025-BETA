// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountWeight
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.GL;

public class AccountWeight : IComparable, IComparable<AccountWeight>
{
  public int BranchID;
  public int AccountID;
  public int SubID;
  public Decimal Weight;

  public AccountWeight(int aBranchID, int aAcctID, int aSubId, Decimal aWeight)
  {
    this.BranchID = aBranchID;
    this.AccountID = aAcctID;
    this.SubID = aSubId;
    this.Weight = aWeight;
  }

  public AccountWeight(int aBranchID, int aAcctID, int aSubId)
  {
    this.BranchID = aBranchID;
    this.AccountID = aAcctID;
    this.SubID = aSubId;
    this.Weight = 0.0M;
  }

  public virtual int CompareTo(object op2) => this.CompareTo((AccountWeight) op2);

  public virtual int CompareTo(AccountWeight op2)
  {
    if (op2 == null)
      return 1;
    if (op2.BranchID != this.BranchID)
      return Math.Sign(this.BranchID - op2.BranchID);
    return op2.AccountID == this.AccountID ? Math.Sign(this.SubID - op2.SubID) : Math.Sign(this.AccountID - op2.AccountID);
  }
}
