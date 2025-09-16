// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.PXEntityEventBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.WorkflowAPI;

public abstract class PXEntityEventBase<TEntity> where TEntity : class, IBqlTable, new()
{
  public abstract class Container<TSelf> where TSelf : PXEntityEventBase<TEntity>.Container<TSelf>
  {
    public static SelectedEntityEvent<TEntity> Select(
      Expression<Func<TSelf, PXEntityEvent<TEntity>>> eventSelector)
    {
      return new SelectedEntityEvent<TEntity>((LambdaExpression) (eventSelector ?? throw new ArgumentNullException(nameof (eventSelector))));
    }

    public static SelectedEntityEvent<TEntity, TParams> Select<TParams>(
      Expression<Func<TSelf, PXEntityEvent<TEntity, TParams>>> eventSelector)
      where TParams : class, IBqlTable
    {
      return new SelectedEntityEvent<TEntity, TParams>((LambdaExpression) (eventSelector ?? throw new ArgumentNullException(nameof (eventSelector))));
    }

    public static void FireOnPropertyChanged<TField>(PXGraph graph, TEntity eventTarget) where TField : IBqlField
    {
      graph.WorkflowService?.FireOnPropertyChangedEvent<TEntity, TField>(graph, eventTarget);
    }
  }
}
