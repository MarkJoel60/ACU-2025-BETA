// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.IAUWorkflowEventsEngine
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

internal interface IAUWorkflowEventsEngine
{
  AUWorkflowHandler GetHandlerDefinition(string screenID, string handlerName);

  IEnumerable<IWorkflowUpdateField> GetHandlerFields(AUWorkflowHandler handler);

  IEnumerable<AUWorkflowHandler> GetScreenEventHandlers(string screenId);

  IEnumerable<AUWorkflowHandler> GetSubscribedHandlers(string eventContainerName, string eventName);

  bool IsSubscribed(
    AUWorkflowHandler subscribedHandler,
    string stateID,
    string workflowId,
    string workflowSubId);

  bool IsSubscribedRecursive(
    AUWorkflowHandler subscribedHandler,
    string stateId,
    string workflowId,
    string workflowSubId);

  IEnumerable<string[]> GetCachePropertyEvents(string cacheTypeName);
}
