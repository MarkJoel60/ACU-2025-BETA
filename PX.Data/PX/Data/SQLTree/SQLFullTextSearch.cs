// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLFullTextSearch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.SQLTree;

public class SQLFullTextSearch : SQLExpression
{
  private Column keyField_;
  private SQLExpression limit_;

  public SQLFullTextSearch(bool isFreeText)
    : base(isFreeText ? SQLExpression.Operation.FREETEXT : SQLExpression.Operation.CONTAINS)
  {
  }

  protected SQLFullTextSearch(SQLFullTextSearch other)
    : base((SQLExpression) other)
  {
    this.keyField_ = (Column) other.keyField_?.Duplicate();
    this.limit_ = other.limit_?.Duplicate();
    this.Table = other.Table;
  }

  public SimpleTable Table { get; private set; }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLFullTextSearch(this);

  public SQLFullTextSearch SetLimit(SQLExpression limit)
  {
    this.limit_ = limit;
    return this;
  }

  public SQLFullTextSearch SetKeyField(SQLExpression field)
  {
    this.keyField_ = field as Column;
    return this;
  }

  public SQLFullTextSearch SetSearchField(Column field)
  {
    this.lexpr_ = (SQLExpression) field;
    this.Table = (SimpleTable) field?.Table();
    return this;
  }

  public Column KeyField() => this.keyField_;

  public SQLExpression Limit() => this.limit_;

  internal override SQLExpression substituteColumnAliases(Dictionary<string, string> dict)
  {
    this.lexpr_?.substituteColumnAliases(dict);
    this.rexpr_?.substituteColumnAliases(dict);
    this.keyField_?.substituteColumnAliases(dict);
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
    base.substituteExternalColumnAliases(aliases, queryPart, insideAggregate, excludes, isMaxAggregate, isInsideGroupBy);
    this.keyField_ = (Column) this.keyField_?.substituteExternalColumnAliases(aliases, queryPart, insideAggregate, excludes, isMaxAggregate, isInsideGroupBy);
    return (SQLExpression) this;
  }

  internal override void FillExpressionsOfType(
    Predicate<SQLExpression> predicate,
    List<SQLExpression> res)
  {
    base.FillExpressionsOfType(predicate, res);
    this.keyField_?.FillExpressionsOfType(predicate, res);
    this.limit_?.FillExpressionsOfType(predicate, res);
  }

  public Column SearchField() => this.lexpr_ as Column;

  public SQLExpression SearchValue
  {
    get => this.rexpr_;
    internal set => this.rexpr_ = value;
  }

  internal override SQLExpression substituteNode(SQLExpression from, SQLExpression to)
  {
    SQLExpression sqlExpression = base.substituteNode(from, to);
    if (this != sqlExpression)
      return sqlExpression;
    if (this.keyField_.substituteNode(from, to) is Column column)
      this.keyField_ = column;
    this.limit_ = this.limit_.substituteNode(from, to);
    return (SQLExpression) this;
  }

  public override string ToString()
  {
    string str = "CONTAINS";
    if (this.oper_ == SQLExpression.Operation.FREETEXT)
      str = "FREETEXT";
    return $"{str}({this.rexpr_}, {this.lexpr_}, {this.keyField_.Name}, {this.limit_})";
  }

  internal override bool IsBoolType() => true;

  internal override PXDbType GetDBType() => PXDbType.Bit;

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
