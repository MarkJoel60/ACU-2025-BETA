// Decompiled with JetBrains decompiler
// Type: PX.Async.GraphExtensionILongOperationManagerExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Async;

[PXInternalUseOnly]
public static class GraphExtensionILongOperationManagerExtensions
{
  /// <summary>
  /// Starts the delegate specified in the <paramref name="method" /> parameter as a long-running
  /// operation in a separate thread. The method uses the unique identifier (UID)
  /// of the graph extension specified in the <paramref name="graphExt" /> parameter to assign the long-running operation ID.
  /// </summary>
  /// <param name="graphExt">A graph extension UID that is used as the ID of the long-running operation.</param>
  /// <param name="method">The delegate method to be executed asynchronously in a separate thread as a long-running operation.</param>
  public static void StartOperation<TGraph>(
    this ILongOperationManager manager,
    PXGraphExtension<TGraph> graphExt,
    System.Action<CancellationToken>? method)
    where TGraph : PXGraph
  {
    manager.StartOperation((PXGraph) graphExt._Base, method);
  }

  /// <summary>
  /// Starts the delegate specified in the <paramref name="method" /> parameter as a long-running
  /// operation in a separate thread. The method uses the unique identifier (UID)
  /// of the graph extension specified in the <paramref name="graphExt" /> parameter to assign the long-running operation ID.
  /// </summary>
  /// <param name="graphExt">A graph extension UID that is used as the ID of the long-running operation.</param>
  /// <param name="method">The delegate method to be executed asynchronously in a separate thread as a long-running operation.</param>
  public static void StartAsyncOperation<TGraph>(
    this ILongOperationManager manager,
    PXGraphExtension<TGraph> graphExt,
    Func<CancellationToken, Task> method)
    where TGraph : PXGraph
  {
    manager.StartAsyncOperation((PXGraph) graphExt._Base, method);
  }
}
