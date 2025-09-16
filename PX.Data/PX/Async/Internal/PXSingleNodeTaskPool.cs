// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.PXSingleNodeTaskPool
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Update;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace PX.Async.Internal;

internal sealed class PXSingleNodeTaskPool : PXTaskPool
{
  internal override bool TryGetValue(string key, out PXLongOperationState state)
  {
    return this.TryGetLocal(key, out state);
  }

  internal override void BindTaskToUserSession(string key)
  {
  }

  internal override void Abort(string key) => this.AbortLocalOperation(key);

  protected override void Remove(string key, Guid instanceId) => this.TryRemoveLocal(key);

  internal override void FlushState()
  {
  }

  internal override void Flush(PXLongOperationState value)
  {
  }

  internal override IEnumerable<RowTaskInfo> GetTaskList(string key, string screenID)
  {
    return this.GetTaskListLocal().Where<RowTaskInfo>((Func<RowTaskInfo, bool>) (task => key == null || task.Key == key)).Where<RowTaskInfo>((Func<RowTaskInfo, bool>) (task => screenID == null || task.Screen == screenID));
  }

  private IEnumerable<RowTaskInfo> GetTaskListLocal()
  {
    PXSingleNodeTaskPool singleNodeTaskPool = this;
    int databaseId = PXInstanceHelper.DatabaseId;
    foreach (PXLongOperationState localTask in singleNodeTaskPool.GetLocalTasks())
    {
      if (localTask != null && !localTask.IsCompleted && localTask.DatabaseID == databaseId)
      {
        TimeSpan timeSpan = System.DateTime.Now - localTask.StartStamp;
        PXProgress pxProgress = localTask.GetProgress() ?? new PXProgress();
        yield return new RowTaskInfo()
        {
          OperationKey = localTask.Key,
          Key = localTask.Key.String,
          WorkTime = timeSpan.ToString("hh\\:mm\\:ss"),
          User = localTask.UserName,
          Screen = localTask.ScreenID,
          Processed = pxProgress.Current,
          Total = pxProgress.Total,
          Errors = pxProgress.Errors
        };
      }
    }
  }

  internal override void Complete(PXLongOperationState item)
  {
  }

  protected override Task HandleAbortMessageAsync(AbortLongOperationMessage message)
  {
    return Task.CompletedTask;
  }

  protected override Task HandleRemoveMessageAsync(RemoveLongOperationMessage message)
  {
    return Task.CompletedTask;
  }
}
