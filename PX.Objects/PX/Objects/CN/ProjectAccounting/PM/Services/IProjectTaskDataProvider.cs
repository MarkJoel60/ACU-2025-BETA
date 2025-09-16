// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.Services.IProjectTaskDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.PM;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CN.ProjectAccounting.PM.Services;

public interface IProjectTaskDataProvider
{
  #nullable disable
  PMTask GetProjectTask(PXGraph graph, int? projectID, int? projectTaskId);

  IEnumerable<PMTask> GetProjectTasks(PXGraph graph, int? projectId);

  IEnumerable<PMTask> GetProjectTasks<TTaskType>(PXGraph graph, int? projectId) where TTaskType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TTaskType>, new();
}
