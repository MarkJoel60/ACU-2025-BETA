// Decompiled with JetBrains decompiler
// Type: PX.Data.BusinessProcess.DummyBusinessProcessEventProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.PushNotifications;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BusinessProcess;

internal class DummyBusinessProcessEventProcessor : IBusinessProcessEventProcessor
{
  IReadOnlyCollection<(string actionName, string uniqueActionName, bool showMassAction, ProcessEventDelegate processDelegate)> IBusinessProcessEventProcessor.GetManualActions(
    string screenId)
  {
    return (IReadOnlyCollection<(string, string, bool, ProcessEventDelegate)>) new (string, string, bool, ProcessEventDelegate)[0];
  }

  public void TriggerActionEvents(
    PXGraph graph,
    string screenId,
    string actionName,
    IDictionary<string, object> fieldsRow,
    List<object> records = null)
  {
  }

  public void CollectAllFieldValuesAndTriggerActionEvents(
    PXGraph graph,
    string actionName,
    IDictionary<KeyWithAlias, object> additionFieldsRow,
    List<object> records = null)
  {
  }

  public bool HasAnyActionEvent(string screenId, string actionName) => false;

  public Dictionary<KeyWithAlias, object> GetFormFieldsAsRow(PXGraph graph)
  {
    return new Dictionary<KeyWithAlias, object>();
  }

  public Dictionary<string, object> GetViewsFieldValuesIfActionEventsExist(
    PXGraph graph,
    string screenId,
    string actionName)
  {
    return new Dictionary<string, object>();
  }
}
