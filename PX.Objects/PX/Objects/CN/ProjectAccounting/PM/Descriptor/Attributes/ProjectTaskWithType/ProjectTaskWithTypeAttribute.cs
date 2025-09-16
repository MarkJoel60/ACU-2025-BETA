// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.Descriptor.Attributes.ProjectTaskWithType.ProjectTaskWithTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.Descriptor.Attributes.ProjectTaskWithType;

public class ProjectTaskWithTypeAttribute : ProjectTaskAttribute
{
  private readonly Type projectField;

  public ProjectTaskWithTypeAttribute(Type projectField)
    : base(projectField)
  {
    this.projectField = projectField ?? throw new ArgumentNullException(nameof (projectField));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) this.CreateDimensionSelectorAttribute(projectField));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }

  public bool NeedsPrefilling { get; set; } = true;

  public bool ForceTaskUpdating { get; set; }

  protected override void OnProjectUpdated(PXCache cache, PXFieldUpdatedEventArgs args)
  {
    if (this.ForceTaskUpdating)
    {
      base.OnProjectUpdated(cache, args);
    }
    else
    {
      string field = cache.GetField(this.projectField);
      int? projectId = cache.GetValue(args.Row, field) as int?;
      if (this.DoesRequiredDefaultProjectTaskExist(cache.Graph, projectId) && this.NeedsPrefilling)
        base.OnProjectUpdated(cache, args);
      else
        cache.SetValue(args.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
    }
  }

  protected virtual Type GetSearchType(Type projectId)
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
      typeof (And<PMTask.type, NotEqual<ProjectTaskType.revenue>>)
    });
  }

  protected virtual PXSelectBase<PMTask> GetRequiredDefaultProjectTaskQuery(PXGraph graph)
  {
    return (PXSelectBase<PMTask>) new PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.revenue>>>>>(graph);
  }

  private bool DoesRequiredDefaultProjectTaskExist(PXGraph graph, int? projectId)
  {
    return this.GetRequiredDefaultProjectTaskQuery(graph).Select(new object[1]
    {
      (object) projectId
    }).FirstTableItems.Any<PMTask>();
  }

  private PXDimensionSelectorAttribute CreateDimensionSelectorAttribute(Type projectId)
  {
    return new PXDimensionSelectorAttribute("PROTASK", this.GetSearchType(projectId), typeof (PMTask.taskCD), new Type[4]
    {
      typeof (PMTask.taskCD),
      typeof (PMTask.type),
      typeof (PMTask.description),
      typeof (PMTask.status)
    })
    {
      DescriptionField = typeof (PMTask.description),
      ValidComboRequired = true,
      DescriptionDisplayName = "Project Task Description"
    };
  }
}
