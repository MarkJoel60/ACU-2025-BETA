// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.DefaultEndpointImplPM
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Adapters;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EndpointAdapters;

[PXVersion("20.200.001", "Default")]
[PXVersion("22.200.001", "Default")]
[PXVersion("23.200.001", "Default")]
internal class DefaultEndpointImplPM : IAdapterWithMetadata
{
  public IEntityMetadataProvider MetadataProvider { protected get; set; }

  protected CbApiWorkflowApplicator.ProjectTemplateApplicator ProjectTemplateApplicator { get; }

  protected CbApiWorkflowApplicator.ProjectTaskApplicator ProjectTaskApplicator { get; }

  public DefaultEndpointImplPM(
    CbApiWorkflowApplicator.ProjectTemplateApplicator projectTemplateApplicator,
    CbApiWorkflowApplicator.ProjectTaskApplicator projectTaskApplicator)
  {
    this.ProjectTemplateApplicator = projectTemplateApplicator;
    this.ProjectTaskApplicator = projectTaskApplicator;
  }

  [FieldsProcessed(new string[] {"ProjectTemplateID", "Status"})]
  protected virtual void ProjectTemplate_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    TemplateMaint templateMaint = (TemplateMaint) graph;
    EntityValueField entityValueField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ProjectTemplateID")) as EntityValueField;
    PMProject instance = (PMProject) ((PXSelectBase) templateMaint.Project).Cache.CreateInstance();
    if (entityValueField == null)
      ((PXSelectBase) templateMaint.Project).Cache.SetDefaultExt<PMProject.contractCD>((object) instance);
    else
      ((PXSelectBase) templateMaint.Project).Cache.SetValueExt<PMProject.contractCD>((object) instance, (object) entityValueField.Value);
    ((PXSelectBase<PMProject>) templateMaint.Project).Current = ((PXSelectBase<PMProject>) templateMaint.Project).Insert(instance);
    this.ProjectTemplateApplicator.ApplyStatusChange(graph, this.MetadataProvider, entity);
  }

  [FieldsProcessed(new string[] {"Status"})]
  protected virtual void ProjectTemplate_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    this.EnsureAndGetCurrentForUpdate<PMProject>(graph);
    this.ProjectTemplateApplicator.ApplyStatusChange(graph, this.MetadataProvider, entity);
  }

  [FieldsProcessed(new string[] {"ProjectTaskID", "ProjectID", "Status"})]
  protected virtual void ProjectTask_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    ProjectTaskEntry projectTaskEntry = (ProjectTaskEntry) graph;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ProjectID")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ProjectTaskID")) as EntityValueField;
    PMTask instance = (PMTask) ((PXSelectBase) projectTaskEntry.Task).Cache.CreateInstance();
    if (entityValueField1 == null)
    {
      ((PXSelectBase) projectTaskEntry.Task).Cache.SetDefaultExt<PMTask.projectID>((object) instance);
    }
    else
    {
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractCD, Equal<Required<PMProject.contractCD>>>>.Config>.Select(graph, new object[1]
      {
        (object) entityValueField1.Value
      }));
      ((PXSelectBase) projectTaskEntry.Task).Cache.SetValueExt<PMTask.projectID>((object) instance, (object) pmProject.ContractID);
    }
    if (entityValueField2 == null)
      ((PXSelectBase) projectTaskEntry.Task).Cache.SetDefaultExt<PMTask.taskCD>((object) instance);
    else
      ((PXSelectBase) projectTaskEntry.Task).Cache.SetValueExt<PMTask.taskCD>((object) instance, (object) entityValueField2.Value);
    ((PXSelectBase<PMTask>) projectTaskEntry.Task).Current = ((PXSelectBase<PMTask>) projectTaskEntry.Task).Insert(instance);
    this.ProjectTaskApplicator.ApplyStatusChange(graph, this.MetadataProvider, entity);
  }

  [FieldsProcessed(new string[] {"Status"})]
  protected virtual void ProjectTask_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    this.EnsureAndGetCurrentForUpdate<PMTask>(graph);
    this.ProjectTaskApplicator.ApplyStatusChange(graph, this.MetadataProvider, entity);
  }

  private T EnsureAndGetCurrentForUpdate<T>(PXGraph graph) where T : class, IBqlTable, new()
  {
    PXCache cach = graph.Caches[typeof (T)];
    if ((object) (cach.Current as T) != null)
      return cach.Current as T;
    PXTrace.WriteWarning("No entity in cache for update. Create new entity instead.");
    graph.Clear();
    return cach.Insert() as T;
  }
}
