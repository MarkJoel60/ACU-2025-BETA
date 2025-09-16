// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.PXTaskPoolExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Context;
using System;

#nullable disable
namespace PX.Async.Internal;

internal static class PXTaskPoolExtensions
{
  internal static PXLongOperationState GetContextTask(this PXTaskPool taskPool)
  {
    return taskPool.GetContextTask(SlotStore.Instance);
  }

  internal static bool HasContextTask(this PXTaskPool taskPool, ISlotStore slots)
  {
    return taskPool.GetContextTask(slots) != null;
  }

  internal static T TryReadFromUncompletedCurrent<T>(
    this PXTaskPool taskPool,
    Func<PXLongOperationState, T> reader)
  {
    PXLongOperationState contextTask = taskPool.GetContextTask();
    if (contextTask == null)
      return default (T);
    using (taskPool.GetLock(contextTask.Key))
      return contextTask.IsCompleted ? default (T) : reader(contextTask);
  }

  internal static void TryWriteToCurrent(
    this PXTaskPool taskPool,
    Action<PXLongOperationState> writer)
  {
    PXLongOperationState contextTask = taskPool.GetContextTask();
    if (contextTask == null)
      return;
    using (taskPool.GetLock(contextTask.Key))
      writer(contextTask);
  }

  internal static void TryWriteToUncompletedCurrent(
    this PXTaskPool taskPool,
    Action<PXLongOperationState> writer)
  {
    taskPool.TryWriteToCurrent((Action<PXLongOperationState>) (result =>
    {
      if (result.IsCompleted)
        return;
      writer(result);
    }));
  }

  internal static IDisposable GetLock(this PXTaskPool taskPool, OperationKey operationKey)
  {
    return taskPool.GetLock(operationKey.String);
  }

  internal static bool TryGetValue(
    this PXTaskPool taskPool,
    OperationKey key,
    out PXLongOperationState state)
  {
    return taskPool.TryGetValue(key.String, out state);
  }

  internal static bool TryGetLocal(
    this PXTaskPool taskPool,
    OperationKey key,
    out PXLongOperationState state)
  {
    return taskPool.TryGetLocal(key.String, out state);
  }

  internal static void TryWriteTo(
    this PXTaskPool taskPool,
    OperationKey key,
    Action<PXLongOperationState> writer)
  {
    taskPool.TryWriteTo(key.String, writer);
  }

  internal static bool Exists(this PXTaskPool taskPool, OperationKey key)
  {
    return taskPool.Exists(key.String);
  }

  internal static void Abort(this PXTaskPool taskPool, OperationKey key)
  {
    taskPool.Abort(key.String);
  }

  internal static void BindTaskToUserSession(this PXTaskPool taskPool, OperationKey key)
  {
    taskPool.BindTaskToUserSession(key.String);
  }

  internal static T TryReadFrom<T>(
    this PXTaskPool taskPool,
    OperationKey key,
    Func<PXLongOperationState, T> reader)
  {
    return key != null ? taskPool.TryReadFrom<T>(key.String, reader) : default (T);
  }
}
