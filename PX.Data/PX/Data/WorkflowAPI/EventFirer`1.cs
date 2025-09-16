// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.EventFirer`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Linq.Expressions;
using System.Monads;
using System.Reflection;

#nullable disable
namespace PX.Data.WorkflowAPI;

internal static class EventFirer<TEntity> where TEntity : class, IBqlTable, new()
{
  public static void FireEvent(
    PXGraph graph,
    LambdaExpression eventDef,
    TEntity eventTarget,
    object eventParameter)
  {
    (string eventContainerName, string eventName) = MaybeObjects.With<MemberInfo, (string, string)>(((MemberExpression) eventDef.Body).Member, (Func<MemberInfo, (string, string)>) (m => (m.DeclaringType.FullName, m.Name)));
    graph.WorkflowService?.FireEvent<TEntity>(graph, eventContainerName, eventName, eventTarget, eventParameter);
  }
}
