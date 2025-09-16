// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.DdlCommandLoggingExecutionObserver
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Commands;
using PX.DbServices.Points;

#nullable disable
namespace PX.Data.Maintenance;

public class DdlCommandLoggingExecutionObserver : SimpleExecutionObserver
{
  public virtual void AfterSqlExecution(
    CommandBase cmd,
    int iQuery,
    string queryText,
    int? valueReturnedByQuery,
    double millisecondsTaken)
  {
    if (!cmd.IsDdlCommand)
      return;
    PXTrace.Logger.ForTelemetry("ExecutionObserver", "DDL Command Execution").Information<string, string, string>("Command: {Command}\r\nAffected table: {AffectedTable}\r\nSql: {Sql}", cmd.GetType().Name, cmd.AffectedTable, queryText);
  }
}
