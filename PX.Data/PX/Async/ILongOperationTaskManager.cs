// Decompiled with JetBrains decompiler
// Type: PX.Async.ILongOperationTaskManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.SM;
using System.Collections.Generic;

#nullable enable
namespace PX.Async;

internal interface ILongOperationTaskManager
{
  IEnumerable<RowTaskInfo> GetTasks(string? key = null, string? screenID = null);

  PXLongRunStatus GetSharedTaskStatus(RowTaskInfo taskInfo);

  void AbortTask(RowTaskInfo taskInfo);
}
