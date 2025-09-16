// Decompiled with JetBrains decompiler
// Type: PX.Data.BusinessProcess.IBusinessProcessEventProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.PushNotifications;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BusinessProcess;

internal interface IBusinessProcessEventProcessor
{
  IReadOnlyCollection<(string actionName, string uniqueActionName, bool showMassAction, ProcessEventDelegate processDelegate)> GetManualActions(
    string screenId);

  void TriggerActionEvents(
    PXGraph graph,
    string screenId,
    string actionName,
    IDictionary<string, object> fieldsRow,
    List<object> records = null);

  void CollectAllFieldValuesAndTriggerActionEvents(
    PXGraph graph,
    string actionName,
    IDictionary<KeyWithAlias, object> additionFieldsRow,
    List<object> records = null);

  bool HasAnyActionEvent(string screenId, string actionName);

  Dictionary<KeyWithAlias, object> GetFormFieldsAsRow(PXGraph graph);

  Dictionary<string, object> GetViewsFieldValuesIfActionEventsExist(
    PXGraph graph,
    string screenId,
    string actionName);
}
