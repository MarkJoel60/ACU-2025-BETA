// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.DtObserver
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert.DataTransfer.RowTransforms;
using PX.BulkInsert.Provider;
using PX.DbServices.Points;
using PX.DbServices.Points.ZipArchive;
using System;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Data.Update;

public class DtObserver : DataTransferObserver
{
  public string TableName;
  public TransferTableTask currentTask;

  public void ReportError(Exception ex)
  {
    PXUpdateLog.WriteMessage(new PXUpdateEvent(EventLogEntryType.Error, $"An error occurred while importing data into the '{this.TableName}' table.", ex));
  }

  public ActionOnException AskHowToRecoverFromError(Exception ex)
  {
    throw new PXException(ex, "An error occurred while importing data into the '{0}' table.", new object[1]
    {
      (object) this.TableName
    });
  }

  public void TaskStarted(TransferTableTask task)
  {
    this.TableName = task.Source.TableName;
    this.currentTask = task;
  }

  public void TaskRunning(int rowsProcessed)
  {
  }

  public void TaskEnded(TransferTableTask task)
  {
    MaxDateTimeTransform dateTimeTransform = task.Transforms.OfType<MaxDateTimeTransform>().FirstOrDefault<MaxDateTimeTransform>();
    if (dateTimeTransform == null || dateTimeTransform.maxModifiedTime == System.DateTime.MinValue || !(task.Destination is ZipTableAdapter destination))
      return;
    destination.SetLastModifiedDatetime(dateTimeTransform.maxModifiedTime);
  }

  public void BatchEnded()
  {
  }

  public void BatchStarted(int cntTasksEstimate)
  {
  }
}
