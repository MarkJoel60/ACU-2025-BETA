// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.DataUploaderWithLogging
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Model.DataSet;
using PX.DbServices.Model.ImportExport;
using PX.DbServices.Model.Schema;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Maintenance;

public class DataUploaderWithLogging(
  PointDbmsBase point,
  PxDataSet dataFromFile,
  ExportTemplate relations,
  SchemaXmlLayout layout = null,
  System.Action<Dictionary<string, HashSet<string>>> OnDuplicateRows = null) : DataUploader(point, dataFromFile, relations, layout, OnDuplicateRows)
{
  protected virtual ExecutionContext context { get; } = new ExecutionContext((IExecutionObserver) new DdlCommandLoggingExecutionObserver());
}
