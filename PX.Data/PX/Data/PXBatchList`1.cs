// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBatchList`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Async;
using PX.Async.ParallelProcessing;
using PX.Common;
using PX.Licensing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data;

internal class PXBatchList<T>
{
  private readonly Action<System.Action<CancellationToken>, CancellationToken> _runner;
  private readonly System.Action<T> _mergeAction;
  private readonly HashSet<Task<T>> _tasks = new HashSet<Task<T>>();
  private readonly int _processorCount;
  private bool IsProcessedCorrectly = true;

  internal PXBatchList(
    Action<System.Action<CancellationToken>, CancellationToken> runner,
    System.Action<T> mergeAction,
    int processorCount = 1)
  {
    this._runner = runner;
    this._mergeAction = mergeAction ?? (System.Action<T>) (_ => { });
    this._processorCount = processorCount > 1 ? processorCount : 1;
  }

  internal PXBatchList(System.Action<T> mergeAction)
    : this(PXBatchList<T>.Runner(LongOperationManager.Instance), mergeAction, ParallelProcessingOptions.GetProcessorCount(LicensingManager.Instance.License))
  {
  }

  internal PXBatchList(System.Action<T> mergeAction, int processorCount)
    : this(PXBatchList<T>.Runner(LongOperationManager.Instance), mergeAction, processorCount)
  {
  }

  /// <summary>
  /// Adds a batch to the list to execut and run long operation.
  /// If the maximum number of elements in the simultaneous execution is reached, then expects the completion of any first element.
  /// </summary>
  private void ProcessBatch(
    Func<CancellationToken, T> processor,
    CancellationToken cancellationToken)
  {
    this.MergeCompleted();
    if (this._tasks.Count >= this._processorCount)
    {
      Task<Task<T>> task = Task.WhenAny<T>((IEnumerable<Task<T>>) this._tasks);
      task.Wait(cancellationToken);
      this.RemoveAndMerge(task.Result);
    }
    this._tasks.Add(PXProcessingBatch.Queue<T>(this._runner, processor, cancellationToken));
  }

  /// <summary>
  /// Clears the list of completed batches. Save last batch error.
  /// </summary>
  private void MergeCompleted()
  {
    foreach (Task<T> task in this._tasks.Where<Task<T>>((Func<Task<T>, bool>) (task => task.IsCompleted)).ToList<Task<T>>())
      this.RemoveAndMerge(task);
  }

  private void RemoveAndMerge(Task<T> task)
  {
    this._tasks.Remove(task);
    this.Merge(task);
  }

  private void Merge(Task<T> task)
  {
    T result;
    try
    {
      result = task.GetAwaiter().GetResult();
    }
    catch (TaskCanceledException ex)
    {
      return;
    }
    catch (PXOperationCompletedWithErrorException ex)
    {
      this.IsProcessedCorrectly = false;
      return;
    }
    catch (Exception ex) when (WebConfig.IsParallelProcessingSkipExceptions)
    {
      return;
    }
    this._mergeAction(result);
  }

  /// <summary>
  /// Throw exception if one of the batch is completed with an error
  /// </summary>
  private void Complete()
  {
    if (!this.IsProcessedCorrectly)
      throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
  }

  private IEnumerable<(int start, int end)> SplitToBatches(
    IList list,
    PXParallelProcessingOptions options)
  {
    if (options.BatchSize <= 0)
      throw new ArgumentOutOfRangeException(nameof (options));
    if (options.AutoBatchSize)
      options.BatchSize = list.Count > options.BatchSize ? (int) System.Math.Ceiling((Decimal) list.Count / (Decimal) this._processorCount) : list.Count;
    if (options.SplitToBatches == null)
    {
      for (int start = 0; start < list.Count; start += options.BatchSize)
        yield return (start, System.Math.Min(start + options.BatchSize - 1, list.Count - 1));
    }
    else
    {
      foreach ((int, int) batch in options.SplitToBatches(list, options))
        yield return batch;
    }
  }

  internal void SplitAndProcessAll(
    IList list,
    PXParallelProcessingOptions options,
    Func<(int start, int end), Func<CancellationToken, T>> processorFactory,
    CancellationToken cancellationToken)
  {
    this.ProcessAll(this.SplitToBatches(list, options).Select<(int, int), Func<CancellationToken, T>>(processorFactory), cancellationToken);
  }

  internal void ProcessAll(
    IEnumerable<Func<CancellationToken, T>> processors,
    CancellationToken externalCancellationToken)
  {
    using (CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(externalCancellationToken))
    {
      CancellationToken token = linkedTokenSource.Token;
      try
      {
        foreach (Func<CancellationToken, T> processor in processors)
        {
          token.ThrowIfCancellationRequested();
          this.ProcessBatch(processor, token);
        }
        try
        {
          Task.WhenAll<T>((IEnumerable<Task<T>>) this._tasks).Wait(token);
        }
        catch (AggregateException ex)
        {
        }
        foreach (Task<T> task in this._tasks)
          this.Merge(task);
      }
      catch
      {
        try
        {
          linkedTokenSource.Cancel();
        }
        catch
        {
        }
        throw;
      }
      finally
      {
        this._tasks.Clear();
      }
    }
    this.Complete();
  }

  private static Action<System.Action<CancellationToken>, CancellationToken> Runner(
    LongOperationManager longOperationManager)
  {
    return (Action<System.Action<CancellationToken>, CancellationToken>) ((action, cancellationToken) => longOperationManager.ClearOperationResultsOnCompletion().StartOperationWithForceAsync(Guid.NewGuid(), (System.Action<CancellationToken>) (operationCancellationToken =>
    {
      using (CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(operationCancellationToken, cancellationToken))
        action(linkedTokenSource.Token);
    })));
  }
}
