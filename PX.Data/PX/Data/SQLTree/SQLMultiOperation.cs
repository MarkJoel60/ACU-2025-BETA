// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLMultiOperation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree;

internal class SQLMultiOperation : SQLExpression
{
  public static SQLMultiOperation CreateAnd(List<SQLExpression> arguments)
  {
    return new SQLMultiOperation(arguments, SQLExpression.Operation.AND, " AND ");
  }

  public static SQLMultiOperation CreateOr(List<SQLExpression> arguments)
  {
    return new SQLMultiOperation(arguments, SQLExpression.Operation.OR, " OR ");
  }

  protected SQLMultiOperation(
    List<SQLExpression> arguments,
    SQLExpression.Operation op,
    string sqlSeparator)
    : base(op)
  {
    this.Arguments = arguments ?? throw new ArgumentNullException(nameof (arguments));
    this.SqlSeparator = sqlSeparator;
  }

  protected SQLMultiOperation(SQLMultiOperation other)
    : base((SQLExpression) other)
  {
    this.Arguments = other.Arguments.Select<SQLExpression, SQLExpression>((Func<SQLExpression, SQLExpression>) (e => e.Duplicate())).ToList<SQLExpression>();
    this.SqlSeparator = other.SqlSeparator;
  }

  public List<SQLExpression> Arguments { get; }

  public string SqlSeparator { get; }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLMultiOperation(this);

  public override bool Equals(SQLExpression other)
  {
    bool flag = base.Equals(other);
    if (!flag)
      return flag;
    SQLMultiOperation sqlMultiOperation = (SQLMultiOperation) other;
    return this.Arguments.SequenceEqual<SQLExpression>((IEnumerable<SQLExpression>) sqlMultiOperation.Arguments) && !(this.SqlSeparator != sqlMultiOperation.SqlSeparator);
  }

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);

  internal override SQLExpression substituteConstant(string from, string to)
  {
    this.Arguments.ForEach((System.Action<SQLExpression>) (e => e?.substituteConstant(from, to)));
    return (SQLExpression) this;
  }

  internal override SQLExpression substituteTableName(string from, string to)
  {
    this.Arguments.ForEach((System.Action<SQLExpression>) (e => e?.substituteTableName(from, to)));
    return (SQLExpression) this;
  }

  internal override SQLExpression substituteNode(SQLExpression from, SQLExpression to)
  {
    if (this.Equals(from))
      return to;
    for (int index = 0; index < this.Arguments.Count; ++index)
    {
      if (this.Arguments[index] != null)
      {
        if (this.Arguments[index].Equals(from))
          this.Arguments[index] = to;
        else
          this.Arguments[index].substituteNode(from, to);
      }
    }
    return (SQLExpression) this;
  }

  internal override SQLExpression substituteColumnAliases(Dictionary<string, string> dict)
  {
    this.Arguments.ForEach((System.Action<SQLExpression>) (e => e?.substituteColumnAliases(dict)));
    return (SQLExpression) this;
  }

  internal override SQLExpression substituteExternalColumnAliases(
    Dictionary<string, SQRenamer> aliases,
    QueryPart queryPart,
    bool insideAggregate = false,
    HashSet<string> excludes = null,
    bool isMaxAggregate = false,
    Func<Column, bool> isInsideGroupBy = null)
  {
    this.Arguments.ForEach((System.Action<SQLExpression>) (e => e?.substituteExternalColumnAliases(aliases, queryPart, insideAggregate, excludes, isMaxAggregate, isInsideGroupBy)));
    return (SQLExpression) this;
  }

  internal override void FillExpressionsOfType(
    Predicate<SQLExpression> predicate,
    List<SQLExpression> res)
  {
    base.FillExpressionsOfType(predicate, res);
    this.Arguments.ForEach((System.Action<SQLExpression>) (e => e?.FillExpressionsOfType(predicate, res)));
  }

  internal override SQLExpression addExternalAliasToTableNames(
    string alias,
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    this.Arguments.ForEach((System.Action<SQLExpression>) (e => e?.addExternalAliasToTableNames(alias, aliases, excludes)));
    return (SQLExpression) this;
  }

  protected internal override SQLExpression.CLType isConstantOrLiteral()
  {
    return !this.Arguments.Any<SQLExpression>() ? SQLExpression.CLType.OTHER : this.Arguments.Select<SQLExpression, SQLExpression.CLType>((Func<SQLExpression, SQLExpression.CLType>) (e => e == null ? SQLExpression.CLType.OTHER : e.isConstantOrLiteral())).Max<SQLExpression.CLType>();
  }

  internal override object evaluateConstant()
  {
    IEnumerable<SQLExpression> source = (IEnumerable<SQLExpression>) this.Arguments;
    if (this.Arguments.Count < 2)
      source = source.Prepend<SQLExpression>((SQLExpression) null);
    if (this.Arguments.Count < 1)
      source = source.Prepend<SQLExpression>((SQLExpression) null);
    return source.Select<SQLExpression, object>((Func<SQLExpression, object>) (e => e?.evaluateConstant())).Aggregate<object>(new Func<object, object, object>(((SQLExpression) this).evaluateConstant));
  }

  internal override bool TrySplitByAnd(List<SQLExpression> list)
  {
    return this.oper_ == SQLExpression.Operation.AND && this.Arguments.All<SQLExpression>((Func<SQLExpression, bool>) (a => a.TrySplitByAnd(list)));
  }
}
