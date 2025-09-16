// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.AUWorkflowEventsEngine
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

internal class AUWorkflowEventsEngine : IAUWorkflowEventsEngine
{
  private readonly IAUWorkflowEngine _auWorkflowEngine;

  public AUWorkflowEventsEngine(IAUWorkflowEngine auWorkflowEngine)
  {
    this._auWorkflowEngine = auWorkflowEngine;
  }

  public AUWorkflowHandler GetHandlerDefinition(string screenID, string handlerName)
  {
    return AUWorkflowEventsEngine.Slot.LocallyCachedSlot.IndexEventHandlers[screenID].FirstOrDefault<AUWorkflowHandler>((Func<AUWorkflowHandler, bool>) (it => string.Equals(it.HandlerName, handlerName, StringComparison.OrdinalIgnoreCase)));
  }

  public IEnumerable<IWorkflowUpdateField> GetHandlerFields(AUWorkflowHandler handler)
  {
    List<AUWorkflowHandlerUpdateField> handlerUpdateFieldList;
    return AUWorkflowEventsEngine.Slot.LocallyCachedSlot.WorkflowHandlerFields.TryGetValue(handler, out handlerUpdateFieldList) ? (IEnumerable<IWorkflowUpdateField>) handlerUpdateFieldList : (IEnumerable<IWorkflowUpdateField>) null;
  }

  public IEnumerable<AUWorkflowHandler> GetSubscribedHandlers(
    string eventContainerName,
    string eventName)
  {
    return AUWorkflowEventsEngine.Slot.LocallyCachedSlot.IndexEventHandlersByEventName[$"{eventContainerName}.{eventName}"];
  }

  public bool IsSubscribed(
    AUWorkflowHandler subscribedHandler,
    string stateID,
    string workflowId,
    string workflowSubId)
  {
    return AUWorkflowEventsEngine.Slot.LocallyCachedSlot.IndexEventHandlersByWorkflowAndState[$"{subscribedHandler.ScreenID}.{workflowId}.{workflowSubId}.{stateID}"].Any<AUWorkflowStateEventHandler>((Func<AUWorkflowStateEventHandler, bool>) (it => string.Equals(it.HandlerName, subscribedHandler.HandlerName, StringComparison.OrdinalIgnoreCase)));
  }

  public bool IsSubscribedRecursive(
    AUWorkflowHandler subscribedHandler,
    string stateId,
    string workflowId,
    string workflowSubId)
  {
    bool flag = AUWorkflowEventsEngine.Slot.LocallyCachedSlot.IndexEventHandlersByWorkflowAndState[$"{subscribedHandler.ScreenID}.{workflowId}.{workflowSubId}.{stateId}"].Any<AUWorkflowStateEventHandler>((Func<AUWorkflowStateEventHandler, bool>) (it => string.Equals(it.HandlerName, subscribedHandler.HandlerName, StringComparison.OrdinalIgnoreCase)));
    if (!flag)
    {
      AUWorkflowState state = this._auWorkflowEngine.GetState(subscribedHandler.ScreenID, workflowId, workflowSubId, stateId);
      if (!string.IsNullOrEmpty(state?.ParentState))
        return this.IsSubscribedRecursive(subscribedHandler, state.ParentState, workflowId, workflowSubId);
    }
    return flag;
  }

  public IEnumerable<AUWorkflowHandler> GetScreenEventHandlers(string screenId)
  {
    return AUWorkflowEventsEngine.Slot.LocallyCachedSlot.IndexEventHandlers[screenId];
  }

  public IEnumerable<string[]> GetCachePropertyEvents(string cacheTypeName)
  {
    return AUWorkflowEventsEngine.Slot.LocallyCachedSlot.IndexPropertyChangedEvents[cacheTypeName];
  }

  public class Slot : IPrefetchable, IPXCompanyDependent
  {
    public ILookup<string, string[]> IndexPropertyChangedEvents;
    public ILookup<string, AUWorkflowHandler> IndexEventHandlers;
    public ILookup<string, AUWorkflowHandler> IndexEventHandlersByEventName;
    public ILookup<string, AUWorkflowStateEventHandler> IndexEventHandlersByScreenId;
    public ILookup<string, AUWorkflowStateEventHandler> IndexEventHandlersByWorkflowAndState;
    public Dictionary<AUWorkflowHandler, List<AUWorkflowHandlerUpdateField>> WorkflowHandlerFields;

    public static AUWorkflowEventsEngine.Slot GetSlot()
    {
      return PXDatabase.GetSlot<AUWorkflowEventsEngine.Slot>("WorkflowHandlersDefinition", typeof (AUWorkflowHandler), typeof (AUWorkflow), typeof (AUWorkflowStateEventHandler), typeof (AUWorkflowHandlerUpdateField), typeof (PXGraph.FeaturesSet));
    }

    public static AUWorkflowEventsEngine.Slot LocallyCachedSlot
    {
      get
      {
        return PXContext.GetSlot<AUWorkflowEventsEngine.Slot>("WorkflowHandlersDefinition") ?? PXContext.SetSlot<AUWorkflowEventsEngine.Slot>("WorkflowHandlersDefinition", AUWorkflowEventsEngine.Slot.GetSlot());
      }
    }

    public void Prefetch()
    {
      AUWorkflow[] array1 = PXSystemWorkflows.SelectTable<AUWorkflow>().ToArray<AUWorkflow>();
      AUWorkflowHandler[] array2 = PXSystemWorkflows.SelectTable<AUWorkflowHandler>().ToArray<AUWorkflowHandler>();
      AUWorkflowStateEventHandler[] array3 = PXSystemWorkflows.SelectTable<AUWorkflowStateEventHandler>().ToArray<AUWorkflowStateEventHandler>();
      AUWorkflowHandlerUpdateField[] array4 = PXSystemWorkflows.SelectTable<AUWorkflowHandlerUpdateField>().ToArray<AUWorkflowHandlerUpdateField>();
      AUWorkflowStateEventHandler[] inner = array3;
      List<\u003C\u003Ef__AnonymousType80<AUWorkflow, AUWorkflowStateEventHandler>> list = ((IEnumerable<AUWorkflow>) array1).Join((IEnumerable<AUWorkflowStateEventHandler>) inner, w => new
      {
        ScreenID = w.ScreenID,
        WorkflowGUID = w.WorkflowGUID
      }, s => new
      {
        ScreenID = s.ScreenID,
        WorkflowGUID = s.WorkflowGUID
      }, (w, s) => new
      {
        Workflow = w,
        StateEventHandler = s
      }).Where(it =>
      {
        bool? isSystem1 = it.Workflow.IsSystem;
        bool flag1 = true;
        if (isSystem1.GetValueOrDefault() == flag1 & isSystem1.HasValue)
          return true;
        bool? isSystem2 = it.StateEventHandler.IsSystem;
        bool flag2 = false;
        return isSystem2.GetValueOrDefault() == flag2 & isSystem2.HasValue;
      }).ToList();
      list.Select(it => it.StateEventHandler).ToArray<AUWorkflowStateEventHandler>();
      this.IndexPropertyChangedEvents = EnumerableExtensions.Distinct<AUWorkflowHandler, string>(((IEnumerable<AUWorkflowHandler>) array2).Where<AUWorkflowHandler>((Func<AUWorkflowHandler, bool>) (it => it.EventContainerName.StartsWith("@") && it.EventContainerName.EndsWith("PropertyChanged"))), (Func<AUWorkflowHandler, string>) (it => $"{it.EventContainerName}@{it.EventName}")).ToLookup<AUWorkflowHandler, string, string[]>((Func<AUWorkflowHandler, string>) (it => it.EventContainerName.Substring(1).RemoveFromEnd("PropertyChanged")), (Func<AUWorkflowHandler, string[]>) (it => it.EventName.Split('|')));
      this.IndexEventHandlers = ((IEnumerable<AUWorkflowHandler>) array2).ToLookup<AUWorkflowHandler, string, AUWorkflowHandler>((Func<AUWorkflowHandler, string>) (it => it.ScreenID), (Func<AUWorkflowHandler, AUWorkflowHandler>) (it => it));
      this.IndexEventHandlersByEventName = ((IEnumerable<AUWorkflowHandler>) array2).ToLookup<AUWorkflowHandler, string, AUWorkflowHandler>((Func<AUWorkflowHandler, string>) (it => $"{it.EventContainerName}.{it.EventName}"), (Func<AUWorkflowHandler, AUWorkflowHandler>) (it => it));
      this.IndexEventHandlersByWorkflowAndState = list.ToLookup(it => $"{it.StateEventHandler.ScreenID}.{it.Workflow.WorkflowID}.{it.Workflow.WorkflowSubID}.{it.StateEventHandler.StateName}", it => it.StateEventHandler);
      this.IndexEventHandlersByScreenId = ((IEnumerable<AUWorkflowStateEventHandler>) array3).ToLookup<AUWorkflowStateEventHandler, string, AUWorkflowStateEventHandler>((Func<AUWorkflowStateEventHandler, string>) (it => it.ScreenID), (Func<AUWorkflowStateEventHandler, AUWorkflowStateEventHandler>) (it => it));
      this.WorkflowHandlerFields = ((IEnumerable<AUWorkflowHandler>) array2).Join((IEnumerable<AUWorkflowHandlerUpdateField>) array4, s => new
      {
        ScreenID = s.ScreenID,
        HandlerName = s.HandlerName
      }, sp => new
      {
        ScreenID = sp.ScreenID,
        HandlerName = sp.HandlerName
      }, (s, sp) => new{ Handler = s, HandlerField = sp }).GroupBy(it => it.Handler).ToDictionary<IGrouping<AUWorkflowHandler, \u003C\u003Ef__AnonymousType82<AUWorkflowHandler, AUWorkflowHandlerUpdateField>>, AUWorkflowHandler, List<AUWorkflowHandlerUpdateField>>(it => it.Key, it => it.Select(_ => _.HandlerField).OrderBy<AUWorkflowHandlerUpdateField, int?>((Func<AUWorkflowHandlerUpdateField, int?>) (_ => _.HandlerFieldLineNbr)).ToList<AUWorkflowHandlerUpdateField>());
    }
  }
}
