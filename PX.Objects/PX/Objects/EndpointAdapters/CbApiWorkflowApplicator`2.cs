// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.CbApiWorkflowApplicator`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.ContractBased.Adapters;
using PX.Api.ContractBased.Automation;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Data.Automation;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EndpointAdapters;

internal abstract class CbApiWorkflowApplicator<TTable, TStatusField>
  where TTable : class, IBqlTable, new()
  where TStatusField : IBqlField
{
  protected readonly IWorkflowServiceWrapper _workflowService;
  protected readonly string _entityName;

  public CbApiWorkflowApplicator(IWorkflowServiceWrapper workflowService, string entityName)
  {
    this._workflowService = workflowService ?? throw new ArgumentNullException(nameof (workflowService));
    this._entityName = entityName;
  }

  public void ApplyStatusChange(
    PXGraph graph,
    IEntityMetadataProvider metadataProvider,
    EntityImpl entity,
    TTable current = null)
  {
    string status = (string) null;
    try
    {
      this.ApplyStatusChange(graph, metadataProvider, entity, current, out status);
    }
    catch (InvalidOperationException ex)
    {
      if ((object) current == null)
        throw;
      ((PXCache) GraphHelper.Caches<TTable>(graph)).RaiseExceptionHandling<TStatusField>((object) current, (object) status, (Exception) ex);
    }
  }

  protected virtual void ApplyStatusChange(
    PXGraph graph,
    IEntityMetadataProvider metadataProvider,
    EntityImpl entity,
    TTable current,
    out string status)
  {
    status = this.GetStatus(metadataProvider, entity);
    if (status == null)
      return;
    if (!((IWorkflowService) this._workflowService).IsWorkflowDefinitionDefined(graph))
    {
      graph.SetDropDownValue<TStatusField, TTable>(status, (object) current);
    }
    else
    {
      if (((IWorkflowService) this._workflowService).IsInSpecifiedStatus(graph, status))
        return;
      TransitionInfo transition = this.GetTransition(graph, status);
      PXAction action = graph.Actions[((ActionInfo) transition).ActionName];
      if (action == null)
        throw new InvalidOperationException($"Action named: {((ActionInfo) transition).ActionName} for specified status: {status} is not available.");
      AllowWorkflowExtendedComboBoxValuesScope extendedComboBoxValuesScope = new AllowWorkflowExtendedComboBoxValuesScope();
      graph.OnAfterPersist += new Action<PXGraph>(handler);
      ((PXCache) GraphHelper.Caches<TTable>(graph)).IsDirty = true;

      void handler(PXGraph g)
      {
        g.OnAfterPersist -= new Action<PXGraph>(handler);
        this._workflowService.FillFormValues(transition, entity, graph, metadataProvider);
        extendedComboBoxValuesScope.Dispose();
        action.Press();
      }
    }
  }

  protected virtual string GetStatus(IEntityMetadataProvider metadataProvider, EntityImpl entity)
  {
    string viewName = metadataProvider.GetPrimaryViewName();
    return entity.Fields.OfType<EntityValueField>().Where<EntityValueField>((Func<EntityValueField, bool>) (f => (metadataProvider.GetMappedFields().Where<IMappedEntityField>((Func<IMappedEntityField, bool>) (i => i.MappedObject == viewName && i.MappedField.Equals(typeof (TStatusField).Name, StringComparison.OrdinalIgnoreCase))).Select<IMappedEntityField, string>((Func<IMappedEntityField, string>) (i => i.FieldName)).FirstOrDefault<string>() ?? throw new NotSupportedException($"Cannot find mapping for Status field: {typeof (TStatusField).Name}.")).Equals(((EntityField) f).Name, StringComparison.OrdinalIgnoreCase))).Select<EntityValueField, string>((Func<EntityValueField, string>) (f => f.Value)).FirstOrDefault<string>();
  }

  protected virtual TransitionInfo GetTransition(PXGraph graph, string status)
  {
    List<TransitionInfo> list = ((IWorkflowService) this._workflowService).GetPossibleTransition(graph, status).ToList<TransitionInfo>();
    if (list.Count == 0)
    {
      string message = $"Cannot find workflow transition applicable for entity: \"{this._entityName}\" to status: \"{status}\".";
      PXTrace.WriteWarning("CB-API Warning: " + message);
      throw new InvalidOperationException(message);
    }
    if (list.Count > 1)
      PXTrace.WriteVerbose($"CB-API Info: More than one workflow transition applicable for entity: \"{this._entityName}\" to status: \"{status}\" is found. ");
    PXTrace.WriteVerbose($"CB-API Info: Use workflow transition with name: {list[0].Name} for entity: \"{this._entityName}\" to status: \"{status}\".");
    return list[0];
  }
}
