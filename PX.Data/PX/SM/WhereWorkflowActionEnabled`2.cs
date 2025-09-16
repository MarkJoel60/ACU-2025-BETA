// Decompiled with JetBrains decompiler
// Type: PX.SM.WhereWorkflowActionEnabled`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

[Obsolete("Use WorkflowAction.IsEnabled<TEntity, TActionField> instead")]
public sealed class WhereWorkflowActionEnabled<TEntity, TActionField> : 
  IBqlWhere,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TEntity : IBqlTable
  where TActionField : IBqlField
{
  private readonly IBqlField _field;
  private readonly WorkflowAction.IsEnabled<TEntity, TActionField> impl = new WorkflowAction.IsEnabled<TEntity, TActionField>();

  /// <exclude />
  public WhereWorkflowActionEnabled()
  {
  }

  /// <exclude />
  public WhereWorkflowActionEnabled(IBqlField field) => this._field = field;

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.impl.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this.impl.Verify(cache, item, pars, ref result, ref value);
  }
}
