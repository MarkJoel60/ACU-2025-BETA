// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLTreeTraversalVisitor`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree;

internal abstract class SQLTreeTraversalVisitor<T> : ISQLExpressionVisitor<T>, ISQLQueryVisitor<T>
{
  public virtual T Visit(SQLMultiOperation exp)
  {
    T res = this.Visit((SQLExpression) exp);
    return this.Break ? res : this.VisitCollection(res, (IEnumerable<SQLExpression>) exp.Arguments);
  }

  protected abstract T CombineResult(T a, T b);

  protected virtual T DefaultResult => default (T);

  protected bool Break { get; set; }

  public virtual T Visit(SQLExpression exp)
  {
    T a = this.DefaultResult;
    if (exp.LExpr() != null)
      a = this.CombineResult(a, exp.LExpr().Accept<T>((ISQLExpressionVisitor<T>) this));
    if (this.Break || exp.RExpr() == null)
      return a;
    a = this.CombineResult(a, exp.RExpr().Accept<T>((ISQLExpressionVisitor<T>) this));
    return a;
  }

  public virtual T Visit(Asterisk exp) => this.Visit((SQLExpression) exp);

  public virtual T Visit(Column exp)
  {
    T a = this.Visit((SQLExpression) exp);
    if (this.Break || exp.Table() == null)
      return a;
    a = this.CombineResult(a, exp.Table().Accept<T>((ISQLQueryVisitor<T>) this));
    return a;
  }

  public virtual T Visit(SQLConst exp) => this.Visit((SQLExpression) exp);

  public virtual T Visit(Literal exp) => this.Visit((SQLExpression) exp);

  public virtual T Visit(NoteIdExpression exp)
  {
    T a = this.Visit((SQLExpression) exp);
    if (this.Break || exp.MainExpr == null)
      return a;
    a = this.CombineResult(a, exp.MainExpr.Accept<T>((ISQLExpressionVisitor<T>) this));
    return a;
  }

  public virtual T Visit(SQLConvert exp) => this.Visit((SQLExpression) exp);

  public T Visit(SQLSmartConvert exp) => this.Visit((SQLExpression) exp);

  public virtual T Visit(SQLDateAdd exp) => this.Visit((SQLExpression) exp);

  public virtual T Visit(SQLDateByTimeZone exp) => this.Visit((SQLExpression) exp);

  public virtual T Visit(SQLDateDiff exp) => this.Visit((SQLExpression) exp);

  public virtual T Visit(SQLDatePart exp) => this.Visit((SQLExpression) exp);

  public virtual T Visit(SQLFullTextSearch exp)
  {
    T a = this.Visit((SQLExpression) exp);
    if (this.Break)
      return a;
    if (exp.KeyField() != null)
      a = this.CombineResult(a, exp.KeyField().Accept<T>((ISQLExpressionVisitor<T>) this));
    if (this.Break || exp.Limit() == null)
      return a;
    a = this.CombineResult(a, exp.Limit().Accept<T>((ISQLExpressionVisitor<T>) this));
    return a;
  }

  public virtual T Visit(SQLRank exp) => this.Visit((SQLExpression) exp);

  public virtual T Visit(SQLScalarFunction exp) => this.Visit((SQLExpression) exp);

  public virtual T Visit(SQLSwitch exp)
  {
    T a = this.Visit((SQLExpression) exp);
    if (this.Break)
      return a;
    foreach (Tuple<SQLExpression, SQLExpression> tuple in exp.GetCases())
    {
      if (tuple.Item1 != null)
        a = this.CombineResult(a, tuple.Item1.Accept<T>((ISQLExpressionVisitor<T>) this));
      if (this.Break)
        return a;
      if (tuple.Item2 != null)
        a = this.CombineResult(a, tuple.Item2.Accept<T>((ISQLExpressionVisitor<T>) this));
      if (this.Break)
        return a;
    }
    return a;
  }

  public virtual T Visit(SubQuery exp)
  {
    T a = this.Visit((SQLExpression) exp);
    if (this.Break || exp.Query() == null)
      return a;
    a = this.CombineResult(a, exp.Query().Accept<T>((ISQLQueryVisitor<T>) this));
    return a;
  }

  public virtual T Visit(SQLGroupConcat exp)
  {
    T res1 = this.Visit((SQLExpression) exp);
    if (this.Break)
      return res1;
    T res2 = this.VisitCollection(res1, (IEnumerable<SQLExpression>) exp.Arguments);
    List<OrderSegment> orderBy = exp.OrderBy;
    IEnumerable<SQLExpression> items = orderBy != null ? orderBy.Select<OrderSegment, SQLExpression>((Func<OrderSegment, SQLExpression>) (o => o?.expr_)) : (IEnumerable<SQLExpression>) null;
    return this.VisitCollection(res2, items);
  }

  public T Visit(Md5Hash exp) => this.Visit((SQLExpression) exp);

  public T Visit(SQLAggConcat exp) => this.Visit((SQLExpression) exp);

  public virtual T Visit(Table table) => this.DefaultResult;

  public virtual T Visit(SimpleTable table) => this.Visit((Table) table);

  public virtual T Visit(Query table)
  {
    T res1 = this.Visit((Table) table);
    if (this.Break)
      return res1;
    T obj = this.VisitCollection(res1, (IEnumerable<SQLExpression>) table.GetSelection());
    if (this.Break)
      return obj;
    if (table.GetFrom() != null)
    {
      foreach (Joiner joiner in table.GetFrom().Where<Joiner>((Func<Joiner, bool>) (s => s != null)))
      {
        obj = this.CombineResult(obj, joiner.Accept<T>((ISQLQueryVisitor<T>) this));
        if (this.Break)
          return obj;
      }
    }
    if (table.GetWhere() != null)
      obj = this.CombineResult(obj, table.GetWhere().Accept<T>((ISQLExpressionVisitor<T>) this));
    if (this.Break)
      return obj;
    T a = this.VisitCollection(obj, (IEnumerable<SQLExpression>) table.GetGroupBy());
    if (this.Break)
      return a;
    if (table.GetHaving() != null)
      a = this.CombineResult(a, table.GetHaving().Accept<T>((ISQLExpressionVisitor<T>) this));
    if (this.Break)
      return a;
    T res2 = a;
    List<OrderSegment> order = table.GetOrder();
    IEnumerable<SQLExpression> items = order != null ? order.Select<OrderSegment, SQLExpression>((Func<OrderSegment, SQLExpression>) (o => o?.expr_)) : (IEnumerable<SQLExpression>) null;
    return this.VisitCollection(res2, items);
  }

  private T VisitCollection(T res, IEnumerable<SQLExpression> items)
  {
    if (items != null)
    {
      foreach (SQLExpression sqlExpression in items.Where<SQLExpression>((Func<SQLExpression, bool>) (i => i != null)))
      {
        res = this.CombineResult(res, sqlExpression.Accept<T>((ISQLExpressionVisitor<T>) this));
        if (this.Break)
          return res;
      }
    }
    return res;
  }

  public virtual T Visit(JoinedAttrQuery table) => this.Visit((Query) table);

  public virtual T Visit(XMLPathQuery table) => this.Visit((Query) table);

  public virtual T Visit(Joiner joiner)
  {
    T a = this.DefaultResult;
    if (joiner.Table() != null)
      a = this.CombineResult(a, joiner.Table().Accept<T>((ISQLQueryVisitor<T>) this));
    if (this.Break || joiner.getOn() == null)
      return a;
    a = this.CombineResult(a, joiner.getOn().Accept<T>((ISQLExpressionVisitor<T>) this));
    return a;
  }

  public T Visit(Unioner unioner)
  {
    T a = this.DefaultResult;
    if (unioner.Table() != null)
      a = this.CombineResult(a, unioner.Table().Accept<T>((ISQLQueryVisitor<T>) this));
    int num = this.Break ? 1 : 0;
    return a;
  }
}
