// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBatchList
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Data;

internal static class PXBatchList
{
  private static Func<CancellationToken, object> Adapter(System.Action<CancellationToken> processor)
  {
    return (Func<CancellationToken, object>) (batchCancellationToken =>
    {
      processor(batchCancellationToken);
      return (object) null;
    });
  }

  internal static void ProcessAll(
    IEnumerable<System.Action<CancellationToken>> processors,
    CancellationToken cancellationToken)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    new PXBatchList<object>((System.Action<object>) null).ProcessAll(processors.Select<System.Action<CancellationToken>, Func<CancellationToken, object>>(PXBatchList.\u003C\u003EO.\u003C0\u003E__Adapter ?? (PXBatchList.\u003C\u003EO.\u003C0\u003E__Adapter = new Func<System.Action<CancellationToken>, Func<CancellationToken, object>>(PXBatchList.Adapter))), cancellationToken);
  }

  internal static void SplitAndProcessAll<T>(
    IList list,
    PXParallelProcessingOptions options,
    Func<(int start, int end), Func<CancellationToken, T>> processorFactory,
    System.Action<T> mergeAction,
    CancellationToken cancellationToken)
  {
    int? parallelThreadsCount = options.ParallelThreadsCount;
    PXBatchList<T> pxBatchList;
    if (parallelThreadsCount.HasValue)
    {
      int valueOrDefault = parallelThreadsCount.GetValueOrDefault();
      if (valueOrDefault > 0)
      {
        pxBatchList = new PXBatchList<T>(mergeAction, valueOrDefault);
        goto label_4;
      }
    }
    pxBatchList = new PXBatchList<T>(mergeAction);
label_4:
    IList list1 = list;
    PXParallelProcessingOptions options1 = options;
    Func<(int, int), Func<CancellationToken, T>> processorFactory1 = processorFactory;
    CancellationToken cancellationToken1 = cancellationToken;
    pxBatchList.SplitAndProcessAll(list1, options1, processorFactory1, cancellationToken1);
  }

  internal static void SplitAndProcessAll(
    IList list,
    PXParallelProcessingOptions parallelOpt,
    Func<(int start, int end), System.Action<CancellationToken>> processorFactory,
    CancellationToken cancellationToken)
  {
    PXBatchList.SplitAndProcessAll<object>(list, parallelOpt, (Func<(int, int), Func<CancellationToken, object>>) (range => PXBatchList.Adapter(processorFactory(range))), (System.Action<object>) null, cancellationToken);
  }
}
