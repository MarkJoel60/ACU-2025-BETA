// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRBaseWorkflowUpdateProcess`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.MassProcess;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
[PXInternalUseOnly]
public abstract class CRBaseWorkflowUpdateProcess<TGraph, TPrimaryGraph, TPrimary, TMarkAttribute, TClassField> : 
  CRBaseUpdateProcess<TGraph, TPrimary, TMarkAttribute, TClassField>
  where TGraph : PXGraph, IMassProcess<TPrimary>, new()
  where TPrimaryGraph : PXGraph, new()
  where TPrimary : class, IBqlTable, IPXSelectable, new()
  where TMarkAttribute : PXEventSubscriberAttribute
  where TClassField : IBqlField
{
  public PXFilter<CRWorkflowMassActionFilter> Filter;
  public PXCancel<CRWorkflowMassActionFilter> Cancel;
  protected bool IsDefaultWorkflowExists;

  protected abstract PXFilteredProcessing<TPrimary, CRWorkflowMassActionFilter> ProcessingView { get; }

  public CRBaseWorkflowUpdateProcess()
  {
    PXGraph instance = (PXGraph) PXGraph.CreateInstance<TPrimaryGraph>();
    this.IsDefaultWorkflowExists = WorkflowExtensions.IsWorkflowExists(instance) && WorkflowExtensions.IsWorkflowDefinitionDefined(instance);
  }

  protected virtual void _(Events.RowSelected<CRWorkflowMassActionFilter> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<CRWorkflowMassActionFilter.operation>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRWorkflowMassActionFilter>>) e).Cache, (object) null, this.IsDefaultWorkflowExists);
    if (e.Row.Operation != "Execute")
    {
      PXUIFieldAttribute.SetVisible<CRWorkflowMassActionFilter.action>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRWorkflowMassActionFilter>>) e).Cache, (object) e.Row, false);
      e.Row.Action = string.Empty;
    }
    else
    {
      PXUIFieldAttribute.SetVisible<CRWorkflowMassActionFilter.action>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRWorkflowMassActionFilter>>) e).Cache, (object) e.Row, this.IsDefaultWorkflowExists);
      if (string.IsNullOrEmpty(e.Row.Action))
        return;
      ((PXProcessingBase<TPrimary>) this.ProcessingView).SetProcessWorkflowAction(e.Row.Action);
    }
  }
}
