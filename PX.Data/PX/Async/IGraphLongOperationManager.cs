// Decompiled with JetBrains decompiler
// Type: PX.Async.IGraphLongOperationManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Async;

/// <summary>An interface that is used to execute a long-running operation, such as processing data or releasing a document, asynchronously in a separate thread.</summary>
/// <remarks>
/// This interface should be obtained from a graph's <see cref="P:PX.Data.PXGraph.LongOperationManager" /> property.
/// In addition to the functionality exposed by <see cref="T:PX.Async.ILongOperationManager" />, this interface exposes methods that use the <see cref="P:PX.Data.PXGraph.UID" /> unique identifier of the graph.
/// </remarks>
public interface IGraphLongOperationManager : ILongOperationManager
{
  /// <summary>
  /// Starts the delegate specified in the <paramref name="method" /> parameter as a long-running
  /// operation in a separate thread. The method uses the unique identifier (UID)
  /// of the graph to which this instance is bound.
  /// </summary>
  /// <param name="method">The delegate method to be executed asynchronously in a separate thread as a long-running operation.</param>
  void StartOperation(Action<CancellationToken>? method);

  /// <summary>
  /// Starts the delegate specified in the <paramref name="method" /> parameter as a long-running
  /// operation in a separate thread. The method uses the unique identifier (UID)
  /// of the graph to which this instance is bound.
  /// </summary>
  /// <param name="method">The delegate method to be executed asynchronously in a separate thread as a long-running operation.</param>
  void StartAsyncOperation(Func<CancellationToken, Task> method);

  /// <summary>
  /// Returns the details of the long-running operation specified by the unique identifier (UID)
  /// of the graph to which this instance is bound.
  /// </summary>
  LongOperationDetails GetOperationDetails();

  /// <summary>
  /// Makes the current thread wait for the completion of the long-running operation specified by the unique identifier (UID)
  /// of the graph to which this instance is bound.
  /// </summary>
  /// <returns><c>true</c>, if the long-running operation was found; <c>false</c> otherwise.</returns>
  bool WaitCompletion();

  /// <summary>Removes information about the completed operation.</summary>
  void ClearStatus();

  [PXInternalUseOnly]
  object? GetCustomInfoForThis(string? customInfoKey, out object[]? processingList);
}
