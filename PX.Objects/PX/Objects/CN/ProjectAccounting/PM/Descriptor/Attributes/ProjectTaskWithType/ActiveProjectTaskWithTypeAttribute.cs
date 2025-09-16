// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.Descriptor.Attributes.ProjectTaskWithType.ActiveProjectTaskWithTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.Descriptor.Attributes.ProjectTaskWithType;

public class ActiveProjectTaskWithTypeAttribute : ProjectTaskWithTypeAttribute
{
  public ActiveProjectTaskWithTypeAttribute(Type projectField)
    : base(projectField)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<PMTask.isCompleted, NotEqual<True>>), "Task is Completed and cannot be used for data entry.", Array.Empty<Type>()));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<PMTask.isCancelled, NotEqual<True>>), "Task is Canceled and cannot be used for data entry.", Array.Empty<Type>()));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<PMTask.status, NotEqual<ProjectTaskStatus.planned>>), "Project Task '{0}' is inactive.", new Type[1]
    {
      typeof (PMTask.taskCD)
    }));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>), "Task Type is not valid", Array.Empty<Type>()));
  }

  protected override Type GetSearchType(Type projectId)
  {
    return BqlCommand.Compose(new Type[8]
    {
      typeof (Search<,>),
      typeof (PMTask.taskID),
      typeof (Where<,,>),
      typeof (PMTask.projectID),
      typeof (Equal<>),
      typeof (Optional<>),
      projectId,
      typeof (And<PMTask.type, NotEqual<ProjectTaskType.revenue>, And<PMTask.status, Equal<ProjectTaskStatus.active>>>)
    });
  }

  protected override PXSelectBase<PMTask> GetRequiredDefaultProjectTaskQuery(PXGraph graph)
  {
    PXSelectBase<PMTask> projectTaskQuery = base.GetRequiredDefaultProjectTaskQuery(graph);
    projectTaskQuery.WhereAnd<Where<PMTask.status, Equal<ProjectTaskStatus.active>>>();
    return projectTaskQuery;
  }
}
