// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.Descriptor.Attributes.ProjectTaskWithType.ActiveOrPlanningProjectTaskWithTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.Descriptor.Attributes.ProjectTaskWithType;

public class ActiveOrPlanningProjectTaskWithTypeAttribute : ProjectTaskWithTypeAttribute
{
  public ActiveOrPlanningProjectTaskWithTypeAttribute(Type projectField)
    : base(projectField)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<BqlOperand<PMTask.type, IBqlString>.IsNotEqual<ProjectTaskType.revenue>>), "Project Task Type is not valid. Only Tasks of 'Cost Task' and 'Cost and Revenue Task' types are allowed.", Array.Empty<Type>()));
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
      typeof (And<PMTask.type, NotEqual<ProjectTaskType.revenue>, And<PMTask.status, In3<ProjectTaskStatus.active, ProjectTaskStatus.planned>>>)
    });
  }

  protected override PXSelectBase<PMTask> GetRequiredDefaultProjectTaskQuery(PXGraph graph)
  {
    PXSelectBase<PMTask> projectTaskQuery = base.GetRequiredDefaultProjectTaskQuery(graph);
    projectTaskQuery.WhereAnd<Where<PMTask.status, In3<ProjectTaskStatus.active, ProjectTaskStatus.planned>>>();
    return projectTaskQuery;
  }
}
