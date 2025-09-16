// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerStatements.ARStatementKey
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AR.CustomerStatements;

public class ARStatementKey : Tuple<int, string, int, DateTime>
{
  public int BranchID => this.Item1;

  public string CurrencyID => this.Item2;

  public int CustomerID => this.Item3;

  public DateTime StatementDate => this.Item4;

  public ARStatementKey(int branchID, string currencyID, int customerID, DateTime statementDate)
    : base(branchID, currencyID, customerID, statementDate)
  {
  }

  public ARStatementKey(ARStatement statement)
  {
    int? nullable = statement.BranchID;
    int branchID = nullable.Value;
    string curyId = statement.CuryID;
    nullable = statement.CustomerID;
    int customerID = nullable.Value;
    DateTime statementDate = statement.StatementDate.Value;
    // ISSUE: explicit constructor call
    this.\u002Ector(branchID, curyId, customerID, statementDate);
  }

  public ARStatementKey CopyForAnotherCustomer(int customerID)
  {
    return new ARStatementKey(this.BranchID, this.CurrencyID, customerID, this.StatementDate);
  }
}
