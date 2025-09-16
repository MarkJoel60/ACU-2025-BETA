// Decompiled with JetBrains decompiler
// Type: PX.Async.CancellationIgnorantExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Threading;

#nullable disable
namespace PX.Async;

internal static class CancellationIgnorantExtensions
{
  internal static void RunWithCancellationViaThreadAbort(
    System.Action method,
    CancellationToken cancellationToken)
  {
    Thread thread = Thread.CurrentThread;
    using (cancellationToken.Register((System.Action) (() => thread.Abort())))
      method();
  }

  private static System.Action<CancellationToken> ToCancellationViaThreadAbort(
    this PXToggleAsyncDelegate method)
  {
    return method != null ? (System.Action<CancellationToken>) (cancellationToken => CancellationIgnorantExtensions.RunWithCancellationViaThreadAbort((System.Action) (() => method()), cancellationToken)) : throw new ArgumentNullException(nameof (method));
  }

  internal static void StartOperation(
    this ILongOperationManager manager,
    PXGraph graph,
    PXToggleAsyncDelegate method)
  {
    manager.StartOperation(graph, method != null ? method.ToCancellationViaThreadAbort() : (System.Action<CancellationToken>) null);
  }

  internal static void StartOperation<TGraph>(
    this ILongOperationManager manager,
    PXGraphExtension<TGraph> graphExt,
    PXToggleAsyncDelegate method)
    where TGraph : PXGraph
  {
    manager.StartOperation((PXGraph) graphExt._Base, method != null ? method.ToCancellationViaThreadAbort() : (System.Action<CancellationToken>) null);
  }

  internal static void StartOperation(
    this ILongOperationManager manager,
    object key,
    PXToggleAsyncDelegate method)
  {
    manager.StartOperation(key, method != null ? method.ToCancellationViaThreadAbort() : (System.Action<CancellationToken>) null);
  }

  internal static void StartOperationWithForceAsync(
    this LongOperationManager manager,
    Guid key,
    PXToggleAsyncDelegate method)
  {
    manager.StartOperationWithForceAsync(key, method.ToCancellationViaThreadAbort());
  }
}
