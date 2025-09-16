// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.BatchDocumentList`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class BatchDocumentList<T, BatchField>(PXGraph Graph) : DocumentList<T>(Graph)
  where T : class, IBqlTable
  where BatchField : IBqlField
{
  public int BatchSize = 500;

  protected override object GetValue(object data, Type field)
  {
    int? nullable = this._Graph.Caches[typeof (T)].GetValue<BatchField>(data) as int?;
    int batchSize = this.BatchSize;
    return !(nullable.GetValueOrDefault() < batchSize & nullable.HasValue) && !WebConfig.ParallelProcessingDisabled ? (object) null : base.GetValue(data, field);
  }
}
