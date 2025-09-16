// Decompiled with JetBrains decompiler
// Type: PX.SM.WorkflowSyncScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

internal class WorkflowSyncScope : IDisposable
{
  private readonly IAUWorkflowActionsEngine _actionsEngine;
  private readonly string _previousLongRunWorkflowAction;
  private readonly IDictionary _previousLongRunWorkflowObjectKeys;
  private readonly IDictionary<string, object> _previousLongRunFormValues;
  private readonly IDictionary<string, object> _previousLongRunViewsValues;
  private readonly Type _previousLongRunWorkflowObjectType;
  private readonly string _previousLongRunWorkflowScreenId;
  private readonly bool _previousLongRunWorkflowShouldBeExecuted;
  private readonly bool _previousValue;

  public WorkflowSyncScope()
  {
    if (ServiceLocator.IsLocationProviderSet)
      this._actionsEngine = ServiceLocator.Current.GetInstance<IAUWorkflowActionsEngine>();
    if (this._actionsEngine != null)
    {
      this._previousLongRunWorkflowAction = this._actionsEngine.LongRunWorkflowAction;
      this._previousLongRunWorkflowObjectKeys = this._actionsEngine.LongRunWorkflowObjectKeys;
      this._previousLongRunFormValues = this._actionsEngine.LongRunFormValues;
      this._previousLongRunViewsValues = this._actionsEngine.LongRunActionViewsValues;
      this._previousLongRunWorkflowObjectType = this._actionsEngine.LongRunWorkflowObjectType;
      this._previousLongRunWorkflowScreenId = this._actionsEngine.LongRunWorkflowScreenId;
      this._previousLongRunWorkflowShouldBeExecuted = this._actionsEngine.LongRunWorkflowShouldBeExecuted;
      this.SynchronizeSlots();
    }
    this._previousValue = PXContext.GetSlot<bool>("Workflow.WorkflowSyncScope");
    PXContext.SetSlot<bool>("Workflow.WorkflowSyncScope", true);
  }

  private void SynchronizeSlots()
  {
    this._actionsEngine.LongRunWorkflowAction = this._actionsEngine.CurrentWorkflowAction;
    this._actionsEngine.LongRunWorkflowObjectKeys = this._actionsEngine.CurrentWorkflowObjectKeys;
    this._actionsEngine.LongRunFormValues = this._actionsEngine.CurrentFormValues;
    this._actionsEngine.LongRunActionViewsValues = this._actionsEngine.CurrentActionViewsValues;
    this._actionsEngine.LongRunWorkflowObjectType = this._actionsEngine.CurrentWorkflowObjectType;
    this._actionsEngine.LongRunWorkflowScreenId = this._actionsEngine.CurrentWorkflowScreenId;
    this._actionsEngine.LongRunWorkflowShouldBeExecuted = this._actionsEngine.CurrentWorkflowShouldBeExecuted;
  }

  public void Dispose()
  {
    if (this._actionsEngine != null)
    {
      this._actionsEngine.LongRunWorkflowAction = this._previousLongRunWorkflowAction;
      this._actionsEngine.LongRunWorkflowObjectKeys = this._previousLongRunWorkflowObjectKeys;
      this._actionsEngine.LongRunFormValues = this._previousLongRunFormValues;
      this._actionsEngine.LongRunActionViewsValues = this._previousLongRunViewsValues;
      this._actionsEngine.LongRunWorkflowObjectType = this._previousLongRunWorkflowObjectType;
      this._actionsEngine.LongRunWorkflowScreenId = this._previousLongRunWorkflowScreenId;
      this._actionsEngine.LongRunWorkflowShouldBeExecuted = this._previousLongRunWorkflowShouldBeExecuted;
    }
    PXContext.SetSlot<bool>("Workflow.WorkflowSyncScope", this._previousValue);
  }

  public static bool IsInWorkflowSyncScope()
  {
    return PXContext.GetSlot<bool>("Workflow.WorkflowSyncScope");
  }
}
