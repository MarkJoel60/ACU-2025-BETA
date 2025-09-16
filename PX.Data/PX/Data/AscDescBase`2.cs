// Decompiled with JetBrains decompiler
// Type: PX.Data.AscDescBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class AscDescBase<Field, NextField> : IBqlSortColumn, IBqlCreator, IBqlVerifier
  where Field : IBqlOperand
  where NextField : IBqlSortColumn, new()
{
  private IBqlCreator _operand;
  private IBqlSortColumn _next;

  protected IBqlCreator ensureOperand()
  {
    if (this._operand == null)
      this._operand = (object) Activator.CreateInstance<Field>() as IBqlCreator;
    return this._operand != null ? this._operand : throw new PXArgumentException(nameof (Field), "'{0}' either has to be a class field or has to expose the IBqlCreator interface.");
  }

  protected IBqlSortColumn ensureNext()
  {
    return typeof (NextField) == typeof (BqlNone) ? (IBqlSortColumn) null : this._next ?? (this._next = (IBqlSortColumn) new NextField());
  }

  public abstract bool IsDescending { get; }

  public System.Type GetReferencedType()
  {
    return !typeof (IBqlCreator).IsAssignableFrom(typeof (Field)) ? typeof (Field) : (System.Type) null;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    Query query = new Query();
    this.AppendQuery(query, graph, info, selection);
    return query.IsOK();
  }

  public void AppendQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.SortColumns?.Add((IBqlSortColumn) this);
    if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Field)))
    {
      if (info.BuildExpression)
      {
        SQLExpression singleExpression = BqlCommand.GetSingleExpression(typeof (Field), graph, info.Tables, selection, BqlCommand.FieldPlace.OrderBy);
        if (this.IsDescending)
          query.OrderDesc(singleExpression);
        else
          query.OrderAsc(singleExpression);
      }
    }
    else
    {
      this.ensureOperand();
      SQLExpression exp = (SQLExpression) null;
      this._operand.AppendExpression(ref exp, graph, info, selection);
      if (exp != null && info.BuildExpression)
      {
        if (this.IsDescending)
          query.OrderDesc(exp);
        else
          query.OrderAsc(exp);
      }
    }
    if (this.ensureNext() == null)
      return;
    this._next.AppendQuery(query, graph, info, selection);
  }
}
