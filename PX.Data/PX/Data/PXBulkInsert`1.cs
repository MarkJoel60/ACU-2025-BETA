// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBulkInsert`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert.Provider;
using PX.Common;
using PX.Data.Update;
using PX.DbServices.Model.DataSet;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.PXDataSet;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace PX.Data;

public class PXBulkInsert<Table> where Table : IBqlTable
{
  /// <summary>
  /// We do not recommend that you use this method in standard scenarios because it doesn't utilize audit, push notifications, database slots, and other platform mechanisms.
  /// </summary>
  [PXInternalUseOnly]
  public static void Insert(PXGraph graph, IEnumerable records)
  {
    PXCache cach = graph.Caches[typeof (Table)];
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    PxDataTable pxDataTable = new PxDataTable(dbServicesPoint.Schema.GetTable(typeof (Table).Name), (IEnumerable<object[]>) null);
    TransferTableTask transferTableTask = new TransferTableTask()
    {
      Source = (ITableAdapter) new PxDataTableAdapter(pxDataTable),
      Destination = dbServicesPoint.GetTable(typeof (Table).Name, FileMode.Open),
      AppendData = true
    };
    BatchTransferExecutorSync transferExecutorSync = new BatchTransferExecutorSync((DataTransferObserver) new SimpleDataTransferObserver(), (string) null);
    transferExecutorSync.Tasks.Enqueue(transferTableTask);
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    int currentCompany = PXInstanceHelper.CurrentCompany;
    List<TableColumn> columns = ((PxDataRows) pxDataTable).Columns;
    int[] numArray = new int[columns.Count];
    for (int index = 0; index < columns.Count; ++index)
    {
      string name = ((TableEntityBase) columns[index]).Name;
      int num = cach.GetFieldOrdinal(name);
      if (num == -1 && ((TableEntityBase) columns[index]).Name != "CompanyID")
        num = -3;
      if (columns[index].Type == SqlDbType.Timestamp)
        num = -2;
      numArray[index] = num;
    }
    foreach (object record in records)
    {
      object[] objArray = new object[columns.Count];
      for (int index = 0; index < columns.Count; ++index)
      {
        objArray[index] = numArray[index] == -1 ? (object) currentCompany : (numArray[index] == -2 ? (object) new byte[0] : (numArray[index] == -3 ? (object) null : cach.GetValue(record, numArray[index])));
        if (numArray[index] == -2)
          cach.SetValue(record, numArray[index], (object) graph.TimeStamp);
      }
      ((PxDataRows) pxDataTable).AddRow(objArray);
      cach.SetStatus(record, PXEntryStatus.Notchanged);
    }
    stopwatch.Restart();
    transferExecutorSync.StartSync();
  }

  private enum SpecialIndex
  {
    Unknown = -3, // 0xFFFFFFFD
    Timestamp = -2, // 0xFFFFFFFE
    CompanyID = -1, // 0xFFFFFFFF
  }
}
