// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.ActionHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.ContractBased.Models;
using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EndpointAdapters;

[PXInternalUseOnly]
public static class ActionHelper
{
  public static void SubscribeToPersist<TTable>(this PXGraph graph, Action<PXCache<TTable>> action) where TTable : class, IBqlTable, new()
  {
    graph.OnBeforePersist += new Action<PXGraph>(handler);

    void handler(PXGraph g)
    {
      g.OnBeforePersist -= new Action<PXGraph>(handler);
      action(GraphHelper.Caches<TTable>(g));
    }
  }

  public static void SubscribeToPersist<TTable>(this PXGraph graph, PXAction<TTable> action) where TTable : class, IBqlTable, new()
  {
    ((PXCache) GraphHelper.Caches<TTable>(graph)).IsDirty = true;
    graph.OnBeforePersist += new Action<PXGraph>(handler);

    void handler(PXGraph g)
    {
      g.OnBeforePersist -= new Action<PXGraph>(handler);
      ((PXAction) action).Press();
    }
  }

  public static void SubscribeToPersistDependingOnBoolField<TTable>(
    this PXGraph graph,
    EntityValueField holdField,
    PXAction<TTable> actionIfTrue,
    PXAction<TTable> actionIfFalse,
    Action<PXCache<TTable>> afterAction = null)
    where TTable : class, IBqlTable, new()
  {
    if (string.IsNullOrEmpty(holdField?.Value))
      return;
    bool result;
    if (!bool.TryParse(holdField.Value, out result))
      throw new InvalidOperationException($"'{((EntityField) holdField).Name}' value '{holdField.Value}' was not recognized as valid boolean");
    PXAction<TTable> action = result ? actionIfTrue : actionIfFalse;
    if (action == null || !((PXFieldState) (((PXAction) action).GetState(((PXCache) GraphHelper.Caches<TTable>(graph)).Current) as PXButtonState)).Enabled)
      return;
    graph.SubscribeToPersist<TTable>(action);
    if (afterAction == null)
      return;
    graph.SubscribeToPersist<TTable>(afterAction);
  }

  public static void SetDropDownValue<Field, T>(this PXGraph graph, string value, object data)
    where Field : IBqlField
    where T : class, IBqlTable, new()
  {
    PXStringState stateExt = (PXStringState) ((PXCache) GraphHelper.Caches<T>(graph)).GetStateExt<Field>(data);
    int index = ((IEnumerable<string>) stateExt.AllowedLabels).ToList<string>().IndexOf(value);
    if (index > -1)
      value = ((IEnumerable<string>) stateExt.AllowedValues).ElementAt<string>(index);
    else if (stateExt.ExclusiveValues && ((IEnumerable<string>) stateExt.AllowedValues).ToList<string>().IndexOf(value) == -1)
      throw new PXSetPropertyException("The '{0}' list value is not allowed for the {1} field. The allowed values are: {2}.", new object[3]
      {
        (object) value,
        (object) (((PXFieldState) stateExt).DisplayName ?? ((PXFieldState) stateExt).Name),
        (object) string.Join(", ", stateExt.AllowedLabels)
      });
    ((PXCache) GraphHelper.Caches<T>(graph)).SetValueExt<Field>(data, (object) value);
  }
}
