// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.GLHistoryKeyTuple
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CS;

public struct GLHistoryKeyTuple(int ledgerID, int branchID, int accountID, int subID) : 
  IEquatable<GLHistoryKeyTuple>
{
  public readonly int LedgerID = ledgerID;
  public readonly int BranchID = branchID;
  public readonly int AccountID = accountID;
  public readonly int SubID = subID;

  public override bool Equals(object obj) => obj is GLHistoryKeyTuple other && this.Equals(other);

  public bool Equals(GLHistoryKeyTuple other)
  {
    return this.LedgerID == other.LedgerID && this.BranchID == other.BranchID && this.AccountID == other.AccountID && this.SubID == other.SubID;
  }

  public override int GetHashCode()
  {
    return (((17 * 23 + this.LedgerID.GetHashCode()) * 23 + this.BranchID.GetHashCode()) * 23 + this.AccountID.GetHashCode()) * 23 + this.SubID.GetHashCode();
  }
}
