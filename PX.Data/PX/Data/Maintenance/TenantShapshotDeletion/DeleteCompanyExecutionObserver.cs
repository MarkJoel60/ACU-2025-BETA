// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.TenantShapshotDeletion.DeleteCompanyExecutionObserver
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Points;
using System;

#nullable disable
namespace PX.Data.Maintenance.TenantShapshotDeletion;

internal class DeleteCompanyExecutionObserver : IExecutionObserver
{
  public string TableToTrack { get; set; }

  public int? LastDeleteTableRowsAffected { get; set; }

  public void AfterSqlExecution(
    CommandBase cmd,
    int iQuery,
    string queryText,
    int? valueReturnedByQuery,
    double millisecondsTaken)
  {
    if (!(cmd is CmdDelete cmdDelete) || !cmdDelete.Table.Name.Equals(this.TableToTrack, StringComparison.OrdinalIgnoreCase))
      return;
    this.LastDeleteTableRowsAffected = valueReturnedByQuery;
  }

  public void BeforeSqlExecution(string query, CommandBase command)
  {
  }

  public void PopProgressFrame()
  {
  }

  public ActionOnException Problem(Exception ex) => (ActionOnException) 1;

  public void Progress(string name, float progress)
  {
  }

  public void PushProgressFrame(float minValue, float maxValue, string processName)
  {
  }

  public void Warning(Exception ex)
  {
  }
}
