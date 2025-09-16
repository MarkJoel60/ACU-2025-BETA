// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementReportParams
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public static class ARStatementReportParams
{
  public const string CS_StatementReportID = "AR641500";
  public const string CS_CuryStatementReportID = "AR642000";

  public static Dictionary<string, string> FromCustomer(Customer customer)
  {
    return new Dictionary<string, string>()
    {
      ["StatementCustomerId"] = customer.AcctCD,
      ["StatementCycleId"] = customer.StatementCycleId
    };
  }

  public static string ReportIDForCustomer(PXGraph graph, Customer customer, int? branchID)
  {
    string reportID = customer.PrintCuryStatements.GetValueOrDefault() ? "AR642000" : "AR641500";
    return ARStatementPrint.GetCustomerReportID(graph, reportID, customer.BAccountID, branchID);
  }

  public static class Fields
  {
    public static readonly string BranchID = "ARStatement.BranchID";
    public static readonly string StatementCycleID = "ARStatement.StatementCycleId";
    public static readonly string StatementDate = "ARStatement.StatementDate";
    public static readonly string CustomerID = "ARStatement.StatementCustomerID";
    public static readonly string CuryID = "ARStatement.CuryID";
    public static readonly string SendStatementsByEmail = "Customer.SendStatementByEmail";
    public static readonly string PrintStatements = "Customer.PrintStatements";
  }

  public static class Parameters
  {
    public const string BranchID = "BranchID";
    public const string OrganizationID = "OrganizationID";
    public const string StatementCycleID = "StatementCycleId";
    public const string StatementDate = "StatementDate";
    public const string CustomerID = "StatementCustomerId";
    public const string StatementMessage = "StatementMessage";
  }

  public static class BoolValues
  {
    public const string True = "true";
    public const string False = "false";
  }
}
