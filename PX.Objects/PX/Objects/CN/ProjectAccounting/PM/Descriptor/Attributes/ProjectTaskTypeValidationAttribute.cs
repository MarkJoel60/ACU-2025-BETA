// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.Descriptor.Attributes.ProjectTaskTypeValidationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.CN.ProjectAccounting.PM.Services;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.Descriptor.Attributes;

public class ProjectTaskTypeValidationAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber
{
  private IProjectTaskDataProvider projectTaskDataProvider;

  public Type ProjectIdField { get; set; }

  public Type ProjectTaskIdField { get; set; }

  public string WrongProjectTaskType { get; set; }

  public string Message { get; set; }

  public void RowPersisting(PXCache cache, PXRowPersistingEventArgs args)
  {
    if (!(cache.GetValue(args.Row, this.ProjectIdField) is int num1) || !(cache.GetValue(args.Row, this._FieldName) is int num2))
      return;
    this.ValidateProjectTaskType(cache, args.Row, new int?(num1), new int?(num2));
  }

  private void ValidateProjectTaskType(
    PXCache cache,
    object row,
    int? projectId,
    int? projectTaskId)
  {
    this.projectTaskDataProvider = cache.Graph.GetService<IProjectTaskDataProvider>();
    PMTask projectTask = this.projectTaskDataProvider.GetProjectTask(cache.Graph, projectId, projectTaskId);
    if (projectTask == null || !(projectTask.Type == this.WrongProjectTaskType))
      return;
    cache.RaiseException(this._FieldName, row, this.Message, (object) projectTask.TaskCD);
  }
}
