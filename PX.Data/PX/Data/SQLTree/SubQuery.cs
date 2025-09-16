// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SubQuery
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree;

public class SubQuery : SQLExpression
{
  private PX.Data.SQLTree.Query query_;

  protected SubQuery(SubQuery other)
    : base((SQLExpression) other)
  {
    this.query_ = (PX.Data.SQLTree.Query) other.query_?.Duplicate();
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SubQuery(this);

  public SubQuery(PX.Data.SQLTree.Query q)
    : base()
  {
    this.oper_ = SQLExpression.Operation.SUB_QUERY;
    this.query_ = q;
  }

  public PX.Data.SQLTree.Query Query() => this.query_;

  internal void SetQuery(PX.Data.SQLTree.Query q) => this.query_ = q;

  public SQLExpression Exists() => this.UnaryOperation(SQLExpression.Operation.EXISTS);

  public SQLExpression NotExists() => this.UnaryOperation(SQLExpression.Operation.NOT_EXISTS);

  internal override string getSurrogateAlias()
  {
    if (this.Alias() != null)
      return this.Alias();
    if (this.query_ is JoinedAttrQuery)
    {
      JoinedAttrQuery query = this.query_ as JoinedAttrQuery;
      return $"{query.srcTable_}_{query.valCol_}";
    }
    SQLExpression sqlExpression1 = this.query_.GetSelection().First<SQLExpression>();
    if (sqlExpression1 is Column)
      return sqlExpression1.AliasOrName();
    if (sqlExpression1.IsAggregate())
    {
      SQLExpression sqlExpression2 = sqlExpression1.RExpr();
      if (sqlExpression2 is Column)
      {
        Column column = (Column) sqlExpression2;
        string name = column.Table() is SimpleTable simpleTable ? simpleTable.Name : (string) null;
        return $"{sqlExpression1.Oper().ToString()}_{name}_{column.AliasOrName()}";
      }
      if (sqlExpression1.Oper() == SQLExpression.Operation.COUNT)
      {
        string name = this.query_.GetFrom().First<Joiner>().Table() is SimpleTable simpleTable ? simpleTable.Name : (string) null;
        return $"{sqlExpression1.Oper().ToString()}_{name}";
      }
    }
    throw new PXException("Only columns and simple aggregates are supported for getSurrogateAlias(). ");
  }

  internal override SQLExpression substituteTableName(string from, string to)
  {
    this.query_.GetSelection().ForEach((System.Action<SQLExpression>) (e => e.substituteTableName(from, to)));
    this.query_.GetWhere()?.substituteTableName(from, to);
    this.query_.GetGroupBy()?.ForEach((System.Action<SQLExpression>) (e => e.substituteTableName(from, to)));
    this.query_.GetHaving()?.substituteTableName(from, to);
    this.query_.GetOrder()?.ForEach((System.Action<OrderSegment>) (o => o.expr_.substituteTableName(from, to)));
    return (SQLExpression) this;
  }

  internal override SQLExpression substituteNode(SQLExpression from, SQLExpression to)
  {
    SQLExpression sqlExpression = base.substituteNode(from, to);
    if (this != sqlExpression)
      return sqlExpression;
    List<SQLExpression> selection = this.query_.GetSelection();
    for (int index = 0; index < selection.Count; ++index)
      selection[index] = selection[index].substituteNode(from, to);
    this.query_.Where(this.query_.GetWhere()?.substituteNode(from, to));
    List<SQLExpression> groupBy = this.query_.GetGroupBy();
    if (groupBy != null)
    {
      for (int index = 0; index < groupBy.Count; ++index)
        groupBy[index] = groupBy[index].substituteNode(from, to);
    }
    this.query_.Having(this.query_.GetHaving()?.substituteNode(from, to));
    List<OrderSegment> order = this.query_.GetOrder();
    if (order != null)
    {
      for (int index = 0; index < order.Count; ++index)
        order[index].expr_ = order[index].expr_.substituteNode(from, to);
    }
    return (SQLExpression) this;
  }

  private bool IsSimpleQuery()
  {
    List<Joiner> from = this.query_.GetFrom();
    // ISSUE: explicit non-virtual call
    return (from != null ? (__nonvirtual (from.Count) == 1 ? 1 : 0) : 0) != 0 && this.query_.GetWhere() != null && this.query_.GetOrder() == null;
  }

  internal override bool CanBeFlattened(
    Dictionary<string, SQRenamer> aliases,
    bool insideAggregate = false,
    bool isMaxAggregate = false,
    Func<Column, bool> isInsideGroupBy = null)
  {
    if (!insideAggregate)
      return true;
    if (isInsideGroupBy == null || !this.IsSimpleQuery())
      return false;
    if (isMaxAggregate)
      return true;
    Table mainTable = this.query_.GetFrom().First<Joiner>().Table();
    return !this.query_.GetWhere().GetExpressionsOfType<Column>().Where<Column>((Func<Column, bool>) (c => c.Table() != null && !c.Table().AliasOrName().Equals(mainTable.AliasOrName()))).Where<Column>((Func<Column, bool>) (c => !isInsideGroupBy(c))).Any<Column>();
  }

  internal override SQLExpression substituteExternalColumnAliases(
    Dictionary<string, SQRenamer> aliases,
    QueryPart queryPart,
    bool insideAggregate = false,
    HashSet<string> excludes = null,
    bool isMaxAggregate = false,
    Func<Column, bool> isInsideGroupBy = null)
  {
    if (insideAggregate)
    {
      if (isInsideGroupBy != null && this.IsSimpleQuery())
      {
        Table mainTable = this.query_.GetFrom().First<Joiner>().Table();
        if (isMaxAggregate)
        {
          List<SQLExpression> expressions = new List<SQLExpression>();
          this.query_.GetWhere().FillExpressionsOfType((Predicate<SQLExpression>) (e =>
          {
            if (e is Column && !expressions.Any<SQLExpression>((Func<SQLExpression, bool>) (ex => ex.UnwrapAggregate().Equals(e))))
              return true;
            return e.IsAggregate() && e.RExpr() is Column && !expressions.Any<SQLExpression>((Func<SQLExpression, bool>) (ex => ex.UnwrapAggregate().Equals(e.RExpr()) || ex.Equals(e)));
          }), expressions);
          foreach (SQLExpression sqlExpression in expressions.Where<SQLExpression>((Func<SQLExpression, bool>) (c => ((Column) c.UnwrapAggregate()).Table() != null && !((Column) c.UnwrapAggregate()).Table().AliasOrName().Equals(mainTable.AliasOrName()))))
          {
            if (!sqlExpression.IsAggregate() && !isInsideGroupBy((Column) sqlExpression.RExpr()))
              this.query_.GetWhere().substituteNode(sqlExpression, SQLExpression.Aggregate(SQLExpression.Operation.MAX, sqlExpression));
          }
        }
        else if (this.query_.GetWhere().GetExpressionsOfType<Column>().Where<Column>((Func<Column, bool>) (c => c.Table() != null && !c.Table().AliasOrName().Equals(mainTable.AliasOrName()))).Where<Column>((Func<Column, bool>) (c => !isInsideGroupBy(c))).Any<Column>())
          ThrowCannotPutScalar();
      }
      else
        ThrowCannotPutScalar();
    }
    this.query_ = this.query_.substituteExternalColumnAliases(aliases, excludes) as PX.Data.SQLTree.Query;
    return (SQLExpression) this;

    static void ThrowCannotPutScalar() => throw new PXCannotPutScalarIntoAggregateException();
  }

  internal override SQLExpression substituteColumnAliases(Dictionary<string, string> dict)
  {
    this.query_.substituteColumnAliases(dict);
    return (SQLExpression) this;
  }

  internal override SQLExpression addExternalAliasToTableNames(
    string alias,
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    this.query_.addExternalAliasToTableNames(alias, aliases, excludes);
    return (SQLExpression) this;
  }

  public override bool Equals(SQLExpression other)
  {
    if (!(other is SubQuery subQuery))
      return false;
    if (this == subQuery)
      return true;
    return this.query_ != null && this.query_.ToString().Equals(subQuery.query_?.ToString(), StringComparison.OrdinalIgnoreCase);
  }

  internal override PXDbType GetDBType()
  {
    PX.Data.SQLTree.Query query = this.query_;
    return (query != null ? (query.GetSelection().Count == 1 ? 1 : 0) : 0) != 0 ? this.query_.GetSelection().First<SQLExpression>().GetDBType() : PXDbType.Unspecified;
  }

  public override string ToString()
  {
    return this.query_.ToString() + (this.Alias() == null ? "" : " AS " + this.Alias());
  }

  internal override void FillExpressionsOfType(
    Predicate<SQLExpression> predicate,
    List<SQLExpression> res)
  {
    base.FillExpressionsOfType(predicate, res);
    this.query_.GetExpressionsOfType(predicate, res);
  }

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
