// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.Services.ProjectTaskDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.PM;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CN.ProjectAccounting.PM.Services;

public class ProjectTaskDataProvider : IProjectTaskDataProvider
{
  public 
  #nullable disable
  PMTask GetProjectTask(PXGraph graph, int? projectID, int? projectTaskId)
  {
    return PMTask.PK.FindDirty(graph, projectID, projectTaskId);
  }

  public IEnumerable<PMTask> GetProjectTasks(PXGraph graph, int? projectId)
  {
    return PXSelectBase<PMTask, PXViewOf<PMTask>.BasedOn<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMTask.projectID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) projectId
    }).FirstTableItems;
  }

  public IEnumerable<PMTask> GetProjectTasks<TTaskType>(PXGraph graph, int? projectId) where TTaskType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TTaskType>, new()
  {
    return PXSelectBase<PMTask, PXViewOf<PMTask>.BasedOn<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTask.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMTask.type, IBqlString>.IsEqual<TTaskType>>>>.Config>.Select(graph, new object[1]
    {
      (object) projectId
    }).FirstTableItems;
  }
}
