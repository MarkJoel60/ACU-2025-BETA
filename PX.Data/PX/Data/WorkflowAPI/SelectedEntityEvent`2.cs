// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.SelectedEntityEvent`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Linq.Expressions;

#nullable disable
namespace PX.Data.WorkflowAPI;

public struct SelectedEntityEvent<TEntity, TParams>
  where TEntity : class, IBqlTable, new()
  where TParams : class, IBqlTable
{
  private readonly LambdaExpression _eventDef;

  internal SelectedEntityEvent(LambdaExpression eventDef) => this._eventDef = eventDef;

  public void FireOn(PXGraph graph, TEntity eventTarget, TParams eventParameter)
  {
    EventFirer<TEntity>.FireEvent(graph, this._eventDef, eventTarget, (object) eventParameter);
  }
}
