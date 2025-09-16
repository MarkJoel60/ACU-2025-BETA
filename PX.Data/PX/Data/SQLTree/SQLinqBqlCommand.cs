// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLinqBqlCommand
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.SQLTree;

internal class SQLinqBqlCommand : BqlCommand
{
  private readonly SQLinqBqlCommandInfo _info;

  private Query _flattenedQuery { get; }

  public SQLinqBqlCommand(Query query, SQLinqBqlCommandInfo info)
  {
    this.Query = query;
    this._flattenedQuery = query.FlattenSubselects();
    this._info = info;
  }

  internal Query Query { get; }

  public override Query GetQueryInternal(
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (info.Tables != null)
    {
      ProjectionItem projectionItem = this.Query.Projection();
      System.Type resultType = projectionItem.GetResultType();
      if (typeof (IBqlTable).IsAssignableFrom(resultType))
        info.Tables.Add(resultType);
      else if (projectionItem is ProjectionPXResult projectionPxResult)
        info.Tables.AddRange((IEnumerable<System.Type>) projectionPxResult.GetResultTypes());
      else
        this.AddIfNotNull<System.Type>(info.Tables, this._info.Tables);
    }
    this.AddIfNotNull<IBqlParameter>(info.Parameters, this._info.Parameters);
    this.AddIfNotNull<IBqlSortColumn>(info.SortColumns, this._info.SortColumns);
    this.AddIfNotNull<System.Type>(info.Fields, this._info.Fields);
    return !info.BuildExpression ? new Query() : (Query) this.Query.Duplicate();
  }

  internal override object GetUniqueKey() => (object) this.Query.ToString();

  public override System.Type GetSelectType()
  {
    System.Type selectType = this._info.BaseCommand?.GetSelectType();
    return (object) selectType != null ? selectType : base.GetSelectType();
  }

  private void AddIfNotNull<T>(List<T> to, List<T> from)
  {
    if (to == null || from == null)
      return;
    to.AddRange((IEnumerable<T>) from);
  }

  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) null;
    result = new bool?(this._flattenedQuery.Meet(cache, item, pars.ToArray()));
  }

  public override BqlCommand OrderByNew<newOrderBy>() => throw new NotSupportedException();

  public override BqlCommand OrderByNew(System.Type newOrderBy) => (BqlCommand) this;

  public override BqlCommand WhereAnd<TWhere>() => throw new NotSupportedException();

  public override BqlCommand WhereAnd(System.Type where) => throw new NotSupportedException();

  public override BqlCommand WhereNew<newWhere>() => throw new NotSupportedException();

  public override BqlCommand WhereNew(System.Type newWhere) => throw new NotSupportedException();

  public override BqlCommand WhereNot() => throw new NotSupportedException();

  public override BqlCommand WhereOr<TWhere>() => throw new NotSupportedException();

  public override BqlCommand WhereOr(System.Type where) => throw new NotSupportedException();
}
