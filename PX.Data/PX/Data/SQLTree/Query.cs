// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Query
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

[DebuggerDisplay("From {from_?.Count} sources")]
public class Query : Table
{
  public static long TimeAccumulatorNew;
  public static long TimeAccumulatorOld;
  private bool ok_;
  public bool IsDistinct;
  private int limitOffset_;
  private int limitRowCount_;
  private ProjectionItem projection_;
  private List<SQLExpression> selection_;
  private List<Joiner> from_;
  private List<Unioner> union_;
  private SQLExpression where_;
  private List<SQLExpression> groupBy_;
  private List<OrderSegment> order_;
  private SQLExpression _having;
  private QueryHints hints_;
  private bool defaultIfEmpty_;
  private bool restricted_;
  private bool _skipFlattening;
  internal const int SequenceClusteringThreshold = 100;
  internal const int SequenceClusterSize = 20;

  public bool IncludeRemovedRecords { get; set; }

  /// <summary>
  /// Changes name of tables in FROM, UNION and JOIN statements recursively.
  /// If table doesn't have alias, its old name is moved to alias.
  /// </summary>
  internal override Table transformTableName(string from, string to)
  {
    for (int index = 0; index < this.from_.Count; ++index)
      this.from_[index] = this.from_[index].transformTableName(from, to);
    for (int index = 0; index < this.union_.Count; ++index)
      this.union_[index] = this.union_[index].transformTableName(from, to);
    return (Table) this;
  }

  internal override Table substituteExternalColumnAliases(
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    HashSet<string> excludes1 = excludes == null ? new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) : new HashSet<string>(excludes.AsEnumerable<string>(), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (Joiner joiner in this.from_)
    {
      if (joiner.Table().Alias != null)
        excludes1.Add(joiner.Table().Alias);
      if (joiner.Table() is SimpleTable simpleTable)
        excludes1.Add(simpleTable.Name);
    }
    foreach (Unioner unioner in this.union_)
    {
      if (unioner.Table().Alias != null)
        excludes1.Add(unioner.Table().Alias);
      if (unioner.Table() is SimpleTable simpleTable)
        excludes1.Add(simpleTable.Name);
    }
    for (int index = 0; index < this.selection_.Count; ++index)
      this.selection_[index] = this.selection_[index].substituteExternalColumnAliases(aliases, QueryPart.Selection, excludes: excludes1);
    for (int index = 0; index < this.from_.Count; ++index)
      this.from_[index] = this.from_[index].substituteExternalColumnAliases(aliases, excludes1);
    for (int index = 0; index < this.union_.Count; ++index)
      this.union_[index] = this.union_[index].substituteExternalColumnAliases(aliases, excludes1);
    this.where_ = this.where_?.substituteExternalColumnAliases(aliases, QueryPart.Where, excludes: excludes1);
    if (this.groupBy_ != null)
    {
      for (int index = 0; index < this.groupBy_.Count; ++index)
        this.groupBy_[index] = this.groupBy_[index].substituteExternalColumnAliases(aliases, QueryPart.GroupBy, excludes: excludes1);
    }
    if (this.order_ != null)
    {
      foreach (OrderSegment orderSegment in this.order_)
        orderSegment.expr_ = orderSegment.expr_?.substituteExternalColumnAliases(aliases, QueryPart.OrderBy, excludes: excludes1);
    }
    this._having = this._having?.substituteExternalColumnAliases(aliases, QueryPart.Having, excludes: excludes1);
    return (Table) this;
  }

  internal override Table substituteColumnAliases(Dictionary<string, string> dict)
  {
    IEnumerable<string> localAliases = this.from_.Select<Joiner, string>((Func<Joiner, string>) (j => j.Table()?.AliasOrName()));
    Dictionary<string, string> dictionary = dict.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (d => !localAliases.Any<string>((Func<string, bool>) (a => a.OrdinalEquals(d.Key))))).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (e => e.Key), (Func<KeyValuePair<string, string>, string>) (e => e.Value), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (SQLExpression sqlExpression in this.selection_)
      sqlExpression.substituteColumnAliases(dictionary);
    foreach (Joiner joiner in this.from_)
      joiner.substituteColumnAliases(dictionary);
    foreach (Unioner unioner in this.union_)
      unioner.substituteColumnAliases(dictionary);
    this.where_?.substituteColumnAliases(dictionary);
    if (this.groupBy_ != null)
    {
      foreach (SQLExpression sqlExpression in this.groupBy_)
        sqlExpression.substituteColumnAliases(dictionary);
    }
    if (this.order_ != null)
    {
      foreach (OrderSegment orderSegment in this.order_)
        orderSegment.expr_?.substituteColumnAliases(dictionary);
    }
    this._having?.substituteColumnAliases(dictionary);
    return (Table) this;
  }

  internal override Table addExternalAliasToTableNames(
    string alias,
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    if (excludes == null)
      excludes = new HashSet<string>();
    foreach (Joiner joiner in this.from_)
    {
      string str = joiner.Table().Alias;
      if (string.IsNullOrEmpty(str) && joiner.Table() is SimpleTable simpleTable)
        str = simpleTable.Name;
      excludes.Add(str);
    }
    foreach (Unioner unioner in this.union_)
    {
      string str = unioner.Table().Alias;
      if (string.IsNullOrEmpty(str) && unioner.Table() is SimpleTable simpleTable)
        str = simpleTable.Name;
      excludes.Add(str);
    }
    foreach (SQLExpression sqlExpression in this.selection_)
      sqlExpression.addExternalAliasToTableNames(alias, aliases, excludes);
    foreach (Joiner joiner in this.from_)
      joiner.addExternalAliasToTableNames(alias, aliases, excludes);
    foreach (Unioner unioner in this.union_)
      unioner.addExternalAliasToTableNames(alias, aliases, excludes);
    this.where_?.addExternalAliasToTableNames(alias, aliases, excludes);
    if (this.groupBy_ != null)
    {
      foreach (SQLExpression sqlExpression in this.groupBy_)
        sqlExpression.addExternalAliasToTableNames(alias, aliases, excludes);
    }
    if (this.order_ != null)
    {
      foreach (OrderSegment orderSegment in this.order_)
        orderSegment.expr_?.addExternalAliasToTableNames(alias, aliases, excludes);
    }
    this._having?.addExternalAliasToTableNames(alias, aliases, excludes);
    return (Table) this;
  }

  private bool removeOrderDuplicates()
  {
    if (this.order_ == null)
      return false;
    bool flag = false;
    for (int index1 = 0; index1 < this.order_.Count; ++index1)
    {
      SQLExpression expr = this.order_[index1].expr_;
      if (expr.Oper() == SQLExpression.Operation.NULL || expr is SQLConst)
      {
        this.order_.RemoveAt(index1);
        flag = true;
        --index1;
      }
      else
      {
        string str = expr.ToString();
        for (int index2 = index1 + 1; index2 < this.order_.Count; ++index2)
        {
          if (this.order_[index2].expr_.ToString() == str || Query.ColumnExpressionComparer.DefaultComparer.CompareColumns(this.order_[index2].expr_ as Column, expr as Column))
          {
            this.order_.RemoveAt(index2);
            flag = true;
            --index2;
          }
        }
      }
    }
    return flag;
  }

  protected Query(Query other)
    : base((Table) other)
  {
    this.ok_ = other.ok_;
    this.IsDistinct = other.IsDistinct;
    this.limitOffset_ = other.limitOffset_;
    this.limitRowCount_ = other.limitRowCount_;
    this.projection_ = other.projection_;
    this.restricted_ = other.restricted_;
    this._skipFlattening = other._skipFlattening;
    this.ShowArchivedRecords = other.ShowArchivedRecords;
    this.IncludeRemovedRecords = other.IncludeRemovedRecords;
    this.SkipDefaultQueryHints = other.SkipDefaultQueryHints;
    this.selection_ = new List<SQLExpression>();
    foreach (SQLExpression sqlExpression in other.selection_)
      this.selection_.Add(sqlExpression?.Duplicate());
    this.from_ = new List<Joiner>();
    foreach (Joiner joiner in other.from_)
      this.from_.Add(joiner?.DuplicateTo(this));
    this.union_ = new List<Unioner>();
    foreach (Unioner unioner in other.union_)
      this.union_.Add(unioner?.DuplicateTo(this));
    this.where_ = other.where_?.Duplicate();
    if (other.groupBy_ != null)
    {
      this.groupBy_ = new List<SQLExpression>();
      foreach (SQLExpression sqlExpression in other.groupBy_)
        this.groupBy_.Add(sqlExpression?.Duplicate());
    }
    if (other.order_ != null)
    {
      this.order_ = new List<OrderSegment>();
      foreach (OrderSegment orderSegment in other.order_)
        this.order_.Add(orderSegment.Duplicate());
    }
    this._having = other._having?.Duplicate();
    this.hints_ = other.hints_;
  }

  internal override Table Duplicate() => (Table) new Query(this);

  public Query()
  {
    this.ok_ = true;
    this.selection_ = new List<SQLExpression>();
    this.from_ = new List<Joiner>();
    this.union_ = new List<Unioner>();
    this.limitOffset_ = 0;
    this.limitRowCount_ = 0;
  }

  internal bool ShowArchivedRecords { get; set; }

  internal bool SkipDefaultQueryHints { get; set; }

  public List<SQLExpression> GetSelection() => this.selection_;

  internal Query ClearSelection()
  {
    this.selection_ = new List<SQLExpression>();
    return this;
  }

  public Query SetSelection(List<SQLExpression> sel)
  {
    this.selection_ = sel;
    return this;
  }

  internal Query PrependSelection(List<SQLExpression> sel)
  {
    List<SQLExpression> sqlExpressionList = new List<SQLExpression>();
    sqlExpressionList.AddRange((IEnumerable<SQLExpression>) sel);
    sqlExpressionList.AddRange((IEnumerable<SQLExpression>) this.selection_);
    this.selection_ = sqlExpressionList;
    return this;
  }

  internal Query ReplaceFirstInSelection(SQLExpression source, SQLExpression target)
  {
    for (int index = 0; index < this.selection_.Count; ++index)
    {
      if (this.selection_[index] == source)
      {
        this.selection_[index] = target;
        break;
      }
    }
    return this;
  }

  public void MarkDefault(bool def = true) => this.defaultIfEmpty_ = def;

  public bool HaveDefault() => this.defaultIfEmpty_;

  [PXInternalUseOnly]
  public List<Joiner> GetFrom() => this.from_;

  [PXInternalUseOnly]
  public List<Unioner> GetUnion() => this.union_;

  internal SQLExpression GetWhere() => this.where_;

  internal List<SQLExpression> GetGroupBy() => this.groupBy_;

  internal SQLExpression GetHaving() => this._having;

  internal List<OrderSegment> GetOrder() => this.order_;

  internal QueryHints GetHints()
  {
    return !this.SkipDefaultQueryHints ? this.hints_ : this.hints_ ^ PXDatabaseProvider.DefaultQueryHints;
  }

  internal int GetLimit() => this.limitRowCount_;

  internal int GetOffset() => this.limitOffset_;

  internal bool HasPaging() => this.GetLimit() > 0 || this.GetOffset() > 0;

  internal void SetProjection(ProjectionItem projection) => this.projection_ = projection;

  internal ProjectionItem Projection() => this.projection_;

  public Query Distinct(bool d = true)
  {
    this.IsDistinct = d;
    return this;
  }

  public void NotOK() => this.ok_ = false;

  public bool IsOK() => this.ok_;

  internal List<Literal> CollectParams()
  {
    List<SQLExpression> sqlExpressionList = new List<SQLExpression>();
    this.GetExpressionsOfType((Predicate<SQLExpression>) (e => e is Literal), sqlExpressionList);
    return sqlExpressionList.OfType<Literal>().ToList<Literal>();
  }

  internal Query unembrace()
  {
    this.where_?.unembraceAnds().unembraceOrs();
    this._having?.unembraceAnds().unembraceOrs();
    foreach (Joiner joiner in this.from_)
    {
      joiner.getOn()?.unembraceAnds().unembraceOrs();
      if (joiner.Table() is Query query)
        query.unembrace();
    }
    foreach (Unioner unioner in this.union_)
    {
      if (unioner.Table() is Query query)
        query.unembrace();
    }
    foreach (SQLExpression sqlExpression in this.selection_)
    {
      if (sqlExpression is SubQuery)
        (sqlExpression as SubQuery).Query()?.unembrace();
      else
        sqlExpression.unembraceAnds().unembraceOrs();
    }
    return this;
  }

  private List<Query> getAllSubQueries()
  {
    List<Query> allSubQueries = new List<Query>() { this };
    List<SQLExpression> sqlExpressionList = this.where_?.GetExpressionsOfType(SQLExpression.Operation.SUB_QUERY) ?? new List<SQLExpression>();
    if (this._having != null)
      sqlExpressionList.AddRange((IEnumerable<SQLExpression>) this._having.GetExpressionsOfType(SQLExpression.Operation.SUB_QUERY));
    if (this.order_ != null)
    {
      foreach (OrderSegment orderSegment in this.order_)
      {
        if (orderSegment.expr_ != null)
          sqlExpressionList.AddRange((IEnumerable<SQLExpression>) orderSegment.expr_.GetExpressionsOfType(SQLExpression.Operation.SUB_QUERY));
      }
    }
    if (this.selection_ != null)
    {
      foreach (SQLExpression sqlExpression in this.selection_)
      {
        if (sqlExpression != null)
          sqlExpressionList.AddRange((IEnumerable<SQLExpression>) sqlExpression.GetExpressionsOfType(SQLExpression.Operation.SUB_QUERY));
      }
    }
    foreach (Joiner joiner in this.from_)
    {
      if (joiner.getOn() != null)
        sqlExpressionList.AddRange((IEnumerable<SQLExpression>) joiner.getOn().GetExpressionsOfType(SQLExpression.Operation.SUB_QUERY));
      if (joiner.Table() is Query query)
        allSubQueries.AddRange(EnumerableExtensions.Except<Query>((IEnumerable<Query>) query.getAllSubQueries(), query));
    }
    foreach (Unioner unioner in this.union_)
    {
      if (unioner.Table() is Query query)
        allSubQueries.AddRange(EnumerableExtensions.Except<Query>((IEnumerable<Query>) query.getAllSubQueries(), query));
    }
    foreach (SQLExpression sqlExpression in sqlExpressionList)
    {
      if (sqlExpression is SubQuery subQuery)
        allSubQueries.AddRange((IEnumerable<Query>) subQuery.Query().getAllSubQueries());
    }
    return allSubQueries;
  }

  internal void SkipFlattening() => this._skipFlattening = true;

  private bool GetRidOfNULLAggregates()
  {
    bool changed = false;
    for (int index = 0; index < this.selection_.Count; ++index)
    {
      SQLExpression sqlExpression = this.selection_[index];
      if (sqlExpression.IsAggregate() && sqlExpression.RExpr() is Column column)
      {
        string name = column.Name;
        string tbl = column.Table().AliasOrName();
        if (tbl != null)
        {
          GetRidOfNull(this.from_.Select<Joiner, Query>((Func<Joiner, Query>) (j => j.Table() as Query)).Where<Query>((Func<Query, bool>) (f => f?.Alias == tbl)).FirstOrDefault<Query>(), name, index);
          GetRidOfNull(this.union_.Select<Unioner, Query>((Func<Unioner, Query>) (j => j.Table() as Query)).Where<Query>((Func<Query, bool>) (f => f?.Alias == tbl)).FirstOrDefault<Query>(), name, index);
        }
      }
    }
    return changed;

    void GetRidOfNull(Query sq, string name, int i)
    {
      if (sq == null)
        return;
      SQLExpression sqlExpression = sq.selection_.Where<SQLExpression>((Func<SQLExpression, bool>) (s => s.AliasOrName() == name)).FirstOrDefault<SQLExpression>();
      if (sqlExpression == null || sqlExpression.Oper() != SQLExpression.Operation.NULL)
        return;
      this.selection_[i] = SQLExpression.Null();
      changed = true;
    }
  }

  [PXInternalUseOnly]
  public Query FlattenSubselects() => this.FlattenSubselects(out bool _);

  internal Query FlattenSubselects(out bool changed)
  {
    Query query = (Query) this.Duplicate();
    try
    {
      changed = query.FlattenSubselectsInternal();
      return changed ? query : this;
    }
    catch (PXException ex)
    {
      PXTrace.WithSourceLocation(nameof (FlattenSubselects), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLTree.cs", 7466).Information((Exception) ex, "[FLF]: FlatteningFailed");
      changed = this.GetRidOfNULLAggregates();
      return this;
    }
  }

  /// <summary>
  /// This method changes internal structure and don't guarantee it will be valid if it fails.
  /// </summary>
  /// <exception cref="T:PX.Data.PXNotSupportedException">If flattening is not supported for this query</exception>
  private bool FlattenSubselectsInternal()
  {
    bool flag1 = false;
    List<Joiner> joinerList1 = new List<Joiner>();
    Dictionary<string, SQRenamer> aliases = new Dictionary<string, SQRenamer>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (SubQuery subQuery in this.selection_.OfType<SubQuery>())
    {
      bool changed;
      subQuery.SetQuery(subQuery.Query().FlattenSubselects(out changed));
      flag1 |= changed;
    }
    if (this.order_ != null)
    {
      foreach (OrderSegment orderSegment in this.order_)
      {
        if (!(orderSegment.expr_ is SubQuery subQuery1))
          subQuery1 = (orderSegment.expr_ is Literal expr ? expr.LExpr() : (SQLExpression) null) as SubQuery;
        SubQuery subQuery2 = subQuery1;
        if (subQuery2 != null)
        {
          bool changed;
          subQuery2.SetQuery(subQuery2.Query().FlattenSubselects(out changed));
          flag1 |= changed;
        }
      }
    }
    List<Joiner> joinerList2 = new List<Joiner>();
    bool flag2 = false;
    foreach (Joiner j in this.from_)
    {
      if (j.Join() == Joiner.JoinType.FULL_JOIN)
        throw new PXNotSupportedException("Queries with full join doesn't support flattening.");
      if (!(j.Table() is Query))
      {
        joinerList2.Add(j);
        joinerList1.Add(j);
      }
      else
      {
        Query t1 = (Query) j.Table();
        Query query = (Query) t1.Duplicate();
        if (query.FlattenSubselectsInternal())
        {
          t1 = query;
          j.setTable((Table) t1);
          query = (Query) t1.Duplicate();
          flag1 = true;
        }
        if (j.Join() != Joiner.JoinType.RIGHT_JOIN)
        {
          Joiner nextJoin = this.GetNextJoin(j);
          if ((nextJoin != null ? (nextJoin.Join() == Joiner.JoinType.RIGHT_JOIN ? 1 : 0) : 0) == 0)
            goto label_27;
        }
        t1.SkipFlattening();
label_27:
        if (!t1.CanBeFlattened())
        {
          joinerList2.Add(j);
          joinerList1.Add(j);
          if (t1.groupBy_ != null)
            flag2 = true;
        }
        else
        {
          string alias = t1.Alias;
          SQRenamer sqRenamer = new SQRenamer();
          aliases[alias] = sqRenamer;
          this.fillSub2Internal(alias, aliases, t1.from_);
          foreach (SQLExpression sqlExpression in t1.GetSelection())
          {
            string key = sqlExpression.AliasOrName();
            if (key != null)
              sqRenamer.extCol2Int[key] = sqlExpression.addExternalAliasToTableNames(alias, aliases).Duplicate();
          }
          if (!this.SelectionCanBeFlattening(aliases))
          {
            Query t2 = query;
            j.setTable((Table) t2);
            t2.SkipFlattening();
            joinerList2.Add(j);
            joinerList1.Add(j);
            if (t2.groupBy_ != null)
              flag2 = true;
            aliases.Remove(alias);
          }
          else
          {
            flag1 = true;
            for (int index = 0; index < this.selection_.Count; ++index)
            {
              string a = this.selection_[index].AliasOrName() ?? this.selection_[index].UnwrapAggregate()?.AliasOrName();
              this.selection_[index] = this.selection_[index].substituteExternalColumnAliases(aliases, QueryPart.Selection, isInsideGroupBy: (Func<Column, bool>) (c => this.IsInsideGroupBy((SQLExpression) c)));
              this.selection_[index].SetAlias(a);
            }
            Query t3 = this.flattenFrom(alias, aliases, t1.from_);
            Joiner joiner = new Joiner(j.Join(), (Table) t3, this);
            joiner.setOn(j.getOn()?.substituteExternalColumnAliases(aliases, QueryPart.From));
            joinerList1.Add(joiner);
            t1.where_?.substituteColumnAliases(sqRenamer.sub2External);
            if (joiner.getOn() != null)
              joiner.setOn(joiner.getOn().And(t1.where_));
            else
              this.where_ = this.where_ == null ? t1.where_ : this.where_.And(t1.where_);
            if (t1.order_ != null && t1.order_.Any<OrderSegment>() && this.from_.Count == 1 && (this.order_ == null || !this.order_.Any<OrderSegment>()) && (this.groupBy_ == null || !this.groupBy_.Any<SQLExpression>()))
            {
              foreach (OrderSegment orderSegment in t1.order_)
                orderSegment.expr_?.substituteColumnAliases(sqRenamer.sub2External);
              this.order_ = t1.order_;
            }
            if (t1.HasPaging() && this.from_.Count == 1 && !this.HasPaging())
            {
              this.Limit(t1.GetLimit());
              this.Offset(t1.GetOffset());
            }
          }
        }
      }
    }
    if (!aliases.Keys.Any<string>())
    {
      if (flag2)
        flag1 |= this.injectConstantsIntoSubselects();
      return flag1;
    }
    foreach (Joiner joiner in joinerList2)
      joiner.setOn(joiner.getOn()?.substituteExternalColumnAliases(aliases, QueryPart.From));
    this.from_ = joinerList1;
    this.where_ = this.where_?.substituteExternalColumnAliases(aliases, QueryPart.Where);
    if (this.order_ != null)
    {
      foreach (OrderSegment orderSegment in this.order_)
      {
        orderSegment.expr_ = orderSegment.expr_?.substituteExternalColumnAliases(aliases, QueryPart.OrderBy);
        this.SubstituteOrderBy(orderSegment);
      }
      flag1 |= this.removeOrderDuplicates();
    }
    if (this.groupBy_ != null)
    {
      for (int index = 0; index < this.groupBy_.Count; ++index)
        this.groupBy_[index] = this.groupBy_[index].substituteExternalColumnAliases(aliases, QueryPart.GroupBy);
    }
    this._having = this._having?.substituteExternalColumnAliases(aliases, QueryPart.Having, isInsideGroupBy: (Func<Column, bool>) (c => this.IsInsideGroupBy((SQLExpression) c)));
    if (flag2)
      flag1 |= this.injectConstantsIntoSubselects();
    return flag1;
  }

  private void SubstituteOrderBy(OrderSegment orderSegment)
  {
    Query query1 = ((orderSegment.expr_ is Literal expr1 ? expr1.LExpr() : (SQLExpression) null) is SubQuery subQuery ? subQuery.Query() : (Query) null) ?? (orderSegment.expr_ is SubQuery expr2 ? expr2.Query() : (Query) null);
    if (query1 == null || query1.selection_.Count != 1 || query1.from_.Count != 1)
      return;
    SimpleTable mainTable = query1.from_.SingleOrDefault<Joiner>()?.Table() as SimpleTable;
    if (mainTable == null)
      return;
    List<SQLExpression> sqlExpressionList1 = new List<SQLExpression>();
    query1.where_?.TrySplitByAnd(sqlExpressionList1);
    foreach (Joiner joiner1 in this.from_)
    {
      if (joiner1.Table() is Query query2)
      {
        SimpleTable simpleTable = ((IEnumerable<SimpleTable>) query2.GetAllTables().OfType<SimpleTable>().ToArray<SimpleTable>()).FirstOrDefault<SimpleTable>((Func<SimpleTable, bool>) (t => t.Name.OrdinalEquals(mainTable.Name)));
        if (simpleTable != null)
        {
          foreach (Joiner joiner2 in query2.from_)
          {
            if (joiner2.getOn() != null)
            {
              List<SQLExpression> sqlExpressionList2 = new List<SQLExpression>();
              if (joiner2.getOn().TrySplitByAnd(sqlExpressionList2) && NonGenericIEnumerableExtensions.Empty_((IEnumerable) sqlExpressionList1.Except<SQLExpression>((IEnumerable<SQLExpression>) sqlExpressionList2, (IEqualityComparer<SQLExpression>) new Query.ColumnExpressionComparer(mainTable))))
                orderSegment.expr_ = query1.selection_.Single<SQLExpression>().substituteTableName(mainTable.AliasOrName(), simpleTable.AliasOrName());
            }
          }
        }
      }
    }
  }

  private bool SelectionCanBeFlattening(Dictionary<string, SQRenamer> aliases)
  {
    foreach (SQLExpression sqlExpression in this.selection_)
    {
      if (!sqlExpression.CanBeFlattened(aliases, sqlExpression.IsAggregate(), sqlExpression.Oper() == SQLExpression.Operation.MAX, (Func<Column, bool>) (c => this.IsInsideGroupBy((SQLExpression) c))))
        return false;
    }
    return true;
  }

  private bool IsInsideGroupBy(SQLExpression expression)
  {
    if (expression == null || this.groupBy_ == null)
      return false;
    Column column = expression as Column;
    if (column == null)
      return false;
    List<SQLExpression> groupBy = this.groupBy_;
    return groupBy != null && groupBy.Any<SQLExpression>((Func<SQLExpression, bool>) (g =>
    {
      if (!(g is Column column2) || !column2.Name.Equals(column.Name, StringComparison.OrdinalIgnoreCase))
        return false;
      Table table = column2.Table();
      if (table == null)
        return false;
      string a = table.AliasOrName();
      bool? nullable = a != null ? new bool?(a.OrdinalEquals(column.Table()?.AliasOrName())) : new bool?();
      bool flag = true;
      return nullable.GetValueOrDefault() == flag & nullable.HasValue;
    }));
  }

  private bool CanBeFlattened()
  {
    SQLExpression where = this.GetWhere();
    bool flag = where != null && where.GetExpressionsOfType(SQLExpression.Operation.CONTAINS).Any<SQLExpression>() || where != null && where.GetExpressionsOfType(SQLExpression.Operation.FREETEXT).Any<SQLExpression>();
    if (this.groupBy_ == null)
    {
      List<Unioner> union = this.union_;
      if ((union != null ? (!union.Any<Unioner>() ? 1 : 0) : 1) != 0 && this.from_ != null && this.from_.Any<Joiner>() && !flag)
        return !this._skipFlattening;
    }
    return false;
  }

  private bool injectConstantsIntoSubselects()
  {
    List<SQLExpression> filter = new List<SQLExpression>();
    this.where_?.getConstantFilterList(ref filter);
    foreach (Joiner j in this.from_.Where<Joiner>((Func<Joiner, bool>) (f => f.Table() is Query)))
    {
      Query subselect = (Query) j.Table();
      j.getOn()?.getConstantFilterList(ref filter);
      string alias = subselect.Alias;
      foreach (SQLExpression sqlExpression in filter)
      {
        if (sqlExpression.LExpr() is Column column && column.Table() is SimpleTable simpleTable && simpleTable.Name == alias)
        {
          if (this.CanInjectConstantIntoSubselect(j, sqlExpression))
          {
            string extColName = column.Name;
            SQLExpression field = subselect.selection_.FirstOrDefault<SQLExpression>((Func<SQLExpression, bool>) (s =>
            {
              string str = s.AliasOrName() ?? s.UnwrapAggregate().AliasOrName();
              return str != null && str.Equals(extColName, StringComparison.InvariantCultureIgnoreCase);
            }));
            if (this.TryInjectFilterIntoSubselect(sqlExpression, field, subselect) && j.IsMain() && this.from_.All<Joiner>((Func<Joiner, bool>) (f => f.Join() != Joiner.JoinType.RIGHT_JOIN)))
              this.Where(this.where_.substituteNode(sqlExpression, SQLExpression.IsTrue(true)));
          }
        }
      }
    }
    return filter.Any<SQLExpression>();
  }

  private bool TryInjectFilterIntoSubselect(
    SQLExpression filter,
    SQLExpression field,
    Query subselect)
  {
    SQLExpression sqlExpression1 = field == null || !field.IsAggregate() ? field : field.UnwrapAggregate();
    if (sqlExpression1 == null || !(sqlExpression1 is Column column1))
      return false;
    Column column2 = new Column(column1.Name, column1.Table(), column1.GetDBType());
    SQLExpression sqlExpression2 = filter.Duplicate();
    if (field.IsAggregate())
    {
      if (field.Oper() != SQLExpression.Operation.SUM)
      {
        List<SQLExpression> groupBy = subselect.GetGroupBy();
        // ISSUE: explicit non-virtual call
        if ((groupBy != null ? (!__nonvirtual (groupBy.Contains(sqlExpression1)) ? 1 : 0) : 0) == 0)
          goto label_6;
      }
      sqlExpression2.SetLeft(SQLExpression.Aggregate(field.Oper(), (SQLExpression) column2));
      SQLExpression having1 = subselect.GetHaving();
      SQLExpression having2 = having1 != null ? having1.Embrace().And(sqlExpression2) : sqlExpression2;
      subselect.Having(having2);
      goto label_7;
    }
label_6:
    sqlExpression2.SetLeft((SQLExpression) column2);
    subselect.AndWhere(sqlExpression2);
label_7:
    return true;
  }

  private bool CanInjectConstantIntoSubselect(Joiner j, SQLExpression node)
  {
    if (node.Oper() != SQLExpression.Operation.IS_NULL && node.Oper() != SQLExpression.Operation.IS_NOT_NULL)
      return true;
    if (EnumerableExtensions.IsIn<Joiner.JoinType>(j.Join(), Joiner.JoinType.FULL_JOIN, Joiner.JoinType.LEFT_JOIN))
      return false;
    Joiner nextJoin = this.GetNextJoin(j);
    return nextJoin == null || EnumerableExtensions.IsNotIn<Joiner.JoinType>(nextJoin.Join(), Joiner.JoinType.FULL_JOIN, Joiner.JoinType.RIGHT_JOIN);
  }

  private Joiner GetNextJoin(Joiner j)
  {
    int index = this.from_.IndexOf(j) + 1;
    return index <= 0 || this.from_.Count <= index ? (Joiner) null : this.from_[index];
  }

  private Unioner GetNextUnion(Unioner u)
  {
    int index = this.union_.IndexOf(u) + 1;
    return index <= 0 || this.from_.Count <= index ? (Unioner) null : this.union_[index];
  }

  private void fillSub2Internal(
    string outerAlias,
    Dictionary<string, SQRenamer> aliases,
    List<Joiner> from)
  {
    foreach (Joiner joiner in from)
    {
      if (joiner.Table() is Query query)
      {
        if (query.CanBeFlattened())
          this.fillSub2Internal(outerAlias, aliases, query.GetFrom());
        else
          aliases[outerAlias].sub2External[query.Alias] = $"{outerAlias}_{query.Alias}";
      }
      else if (joiner.Table() is SimpleTable simpleTable)
      {
        string key = simpleTable.Duplicate().AliasOrName();
        aliases[outerAlias].sub2External[key] = $"{outerAlias}_{key}";
      }
    }
  }

  private Query flattenFrom(
    string outerAlias,
    Dictionary<string, SQRenamer> aliases,
    List<Joiner> from)
  {
    Query q = new Query();
    foreach (Joiner joiner1 in from)
    {
      if (joiner1.Table() is Query t1)
      {
        if (t1.CanBeFlattened())
          t1 = this.flattenFrom(outerAlias, aliases, t1.GetFrom());
        else
          t1.Alias = $"{outerAlias}_{t1.Alias}";
        Joiner joiner2 = new Joiner((Table) t1, q);
        joiner2.SetJoinType(joiner1.Join());
        joiner2.SetHint(joiner1.Hint());
        joiner2.setOn(joiner1.getOn()?.substituteColumnAliases(aliases[outerAlias].sub2External));
        q.from_.Add(joiner2);
      }
      else if (joiner1.Table() is SimpleTable simpleTable)
      {
        Table t = simpleTable.Duplicate();
        t.Alias = $"{outerAlias}_{t.AliasOrName()}";
        Joiner joiner3 = new Joiner(t, q);
        joiner3.SetJoinType(joiner1.Join());
        joiner3.SetHint(joiner1.Hint());
        joiner3.setOn(joiner1.getOn()?.substituteColumnAliases(aliases[outerAlias].sub2External));
        q.from_.Add(joiner3);
      }
    }
    return q;
  }

  internal void SkipRestrictions() => this.restricted_ = true;

  internal Query AppendRestrictions(bool isMainQuery = true)
  {
    if (!(PXDatabase.Provider is PXDatabaseProviderBase provider) || this.restricted_)
      return this;
    this.restricted_ = true;
    foreach (Query allSubQuery in this.getAllSubQueries())
    {
      if (allSubQuery != this && allSubQuery.GetLimit() == 0)
      {
        switch (allSubQuery)
        {
          case XMLPathQuery _:
          case JoinedAttrQuery _:
            break;
          default:
            allSubQuery.Limit(1);
            break;
        }
      }
      bool flag = allSubQuery.from_.Any<Joiner>((Func<Joiner, bool>) (j => j.Join() == Joiner.JoinType.FULL_JOIN || j.Join() == Joiner.JoinType.RIGHT_JOIN));
      foreach (Joiner joiner in allSubQuery.from_)
      {
        Table table = joiner.Table();
        string str1;
        switch (table)
        {
          case Query _:
            joiner.setTable((Table) (table as Query).AppendRestrictions(false));
            continue;
          case SimpleTable simpleTable:
            str1 = simpleTable.Name;
            break;
          default:
            str1 = (string) null;
            break;
        }
        string str2 = str1;
        if (str2 != null)
        {
          if (joiner.Join() == Joiner.JoinType.FULL_JOIN || joiner.Join() == Joiner.JoinType.RIGHT_JOIN)
          {
            List<SQLExpression> sqlExpressionList = new List<SQLExpression>();
            SimpleTable t1 = (SimpleTable) table;
            string columnTable = t1.AliasOrName();
            this.GetExpressionsOfType((Predicate<SQLExpression>) (ex => ex is Column column && string.Equals(column.Table()?.AliasOrName(), columnTable, StringComparison.OrdinalIgnoreCase)), sqlExpressionList);
            SQLExpression[] sqlExpressionArray = sqlExpressionList.Select<SQLExpression, SQLExpression>((Func<SQLExpression, SQLExpression>) (e => e.Duplicate().SetAlias((string) null))).Distinct<SQLExpression>().ToArray<SQLExpression>();
            if (sqlExpressionArray.Length == 0)
              sqlExpressionArray = new SQLExpression[1]
              {
                (SQLExpression) new Asterisk()
              };
            Query t2 = new Query().Select(sqlExpressionArray).From((Table) t1);
            t2.As(t1.AliasOrName());
            t2.SkipFlattening();
            t2.AppendRestrictions(false);
            joiner.setTable((Table) t2);
          }
          else
          {
            bool isKvExt = false;
            bool needKvExtOrdering = false;
            if (str2.Length >= 5 && str2.IndexOf("KvExt", StringComparison.OrdinalIgnoreCase) == str2.Length - 5)
            {
              str2 = str2.Substring(0, str2.Length - 5);
              isKvExt = true;
            }
            companySetting settings;
            int companyId = provider.getCompanyID(str2, out settings);
            if (companyId == 0)
            {
              if (companySetting.NeedRestrict(settings))
              {
                Column l = new Column("CompanyID", joiner.Table());
                SQLExpression sqlExpression = (joiner.Join() == Joiner.JoinType.RIGHT_JOIN || joiner.Join() == Joiner.JoinType.MAIN_TABLE & flag ? l.IsNull() : SQLExpression.None()).Or(SQLExpressionExt.EQ(l, (SQLExpression) new SQLConst((object) 0)));
                joiner.On(sqlExpression.And(joiner.Condition()));
                if (joiner.Join() == Joiner.JoinType.CROSS_JOIN)
                  joiner.SetJoinType(Joiner.JoinType.INNER_JOIN);
              }
              else
                continue;
            }
            Column companyColumn = new Column("CompanyID", joiner.Table());
            Column companyMaskColumn = new Column("CompanyMask", joiner.Table());
            SQLExpression restrictions = (joiner.Join() == Joiner.JoinType.RIGHT_JOIN || joiner.Join() == Joiner.JoinType.MAIN_TABLE & flag ? companyColumn.IsNull() : SQLExpression.None()).Or(SQLExpression.GetRowSharingExpression(settings, companyColumn, companyMaskColumn, isKvExt, companyId, joiner.RowSharingAllowed, out needKvExtOrdering));
            if (isKvExt & needKvExtOrdering && allSubQuery is JoinedAttrQuery && joiner.Join() == Joiner.JoinType.MAIN_TABLE)
              allSubQuery.OrderDesc((SQLExpression) new Column("CompanyID", joiner.Table()));
            if (!isKvExt && !allSubQuery.IncludeRemovedRecords && settings.Deleted != null)
            {
              SQLExpression r = (SQLExpression) null;
              if (!PXDatabase.ReadDeleted)
                r = (SQLExpression) new SQLConst((object) 0);
              else if (allSubQuery == this & isMainQuery && (joiner.Join() == Joiner.JoinType.MAIN_TABLE || joiner.Join() == Joiner.JoinType.RIGHT_JOIN || joiner.Join() == Joiner.JoinType.FULL_JOIN) && PXDatabase.ReadOnlyDeleted)
                r = (SQLExpression) new SQLConst((object) 1);
              if (r != null)
              {
                Column l = new Column(settings.Deleted, joiner.Table());
                restrictions = joiner.Join() != Joiner.JoinType.RIGHT_JOIN ? restrictions.And(SQLExpressionExt.EQ(l, r)) : restrictions.And(l.IsNull().Or(SQLExpressionExt.EQ(l, r)));
              }
            }
            if (!isKvExt && settings.WebAppType != null)
            {
              SQLExpression r = (SQLExpression) new SQLConst((object) WebAppType.Current.AppTypeId);
              if (r != null)
              {
                Column l = new Column(settings.WebAppType, joiner.Table());
                restrictions = joiner.Join() != Joiner.JoinType.RIGHT_JOIN ? restrictions.And(SQLExpressionExt.EQ(l, r)) : restrictions.And(l.IsNull().Or(SQLExpressionExt.EQ(l, r)));
              }
            }
            SQLExpression sqlExpression1 = restrictions.AddReadArchivedRestrictionIfNeeded(settings, table, joiner.Join() == Joiner.JoinType.RIGHT_JOIN, isKvExt, this.ShowArchivedRecords);
            if (!isKvExt && settings.Branch != null && !PXDatabase.ReadBranchRestricted && (PXDatabase.SpecificBranchTable == null || string.Equals(str2, PXDatabase.SpecificBranchTable, StringComparison.OrdinalIgnoreCase)))
            {
              List<int> list = PXDatabase.BranchIDs ?? ((IEnumerable<int>) PXAccess.GetBranchIDs()).ToList<int>();
              int count = list == null ? 0 : list.Count;
              Column column = new Column(settings.Branch, joiner.Table());
              sqlExpression1 = count <= 0 ? sqlExpression1.And(column.IsNull()) : sqlExpression1.And(column.IsNull().Or(Query.CreateOptimizedListInclusionCheck((SQLExpression) column, list)));
            }
            if (joiner.Join() == Joiner.JoinType.CROSS_JOIN)
              joiner.SetJoinType(Joiner.JoinType.INNER_JOIN);
            if (joiner.IsMain())
              allSubQuery.where_ = sqlExpression1.And(allSubQuery.where_);
            else
              joiner.On(sqlExpression1.And(joiner.Condition()));
          }
        }
      }
      foreach (Unioner unioner in allSubQuery.union_)
      {
        Table table = unioner.Table();
        if (table is Query)
          unioner.setTable((Table) (table as Query).AppendRestrictions(false));
      }
      allSubQuery.removeOrderDuplicates();
    }
    return this;
  }

  /// <summary>
  /// Creates expression to check if the <paramref name="column" /> value included in the <paramref name="list" /> elements.
  /// </summary>
  /// <param name="column">Column to be checked</param>
  /// <param name="list">List of values</param>
  /// <remarks>
  /// As recursion is used while transforming IN <see cref="T:PX.Data.SQLTree.SQLExpression" /> to SQL query, it fails with StackOverflowException
  /// on large amount of elements. So we have to avoid using IN in such a case.
  /// If the <paramref name="list" /> contains more than <see cref="F:PX.Data.SQLTree.Query.SequenceClusteringThreshold" /> elements, BETWEEN will be used instead of simple WHERE IN clause.
  /// In that case it will split the source <paramref name="list" /> to sequential chunks and use its minimal and maximal values in the BETWEEN clause.
  /// To avoid StackOverflowException during multiple OR processing, several expressions grouped into clusters with <see cref="F:PX.Data.SQLTree.Query.SequenceClusterSize" /> elements and embraced.
  /// For example,
  /// [1, 2, 3, 4, 5] will be converted to "column BETWEEN 1 AND 5",
  /// [1, 2, ..., 24, 25, 27, 28, ..., 44, 45] will be converted to "column BETWEEN 1 AND 25 OR column BETWEEN 27 AND 45"
  /// [1, 2, ..., 24, 25, 27, 29, ..., 44, 45] will be converted to "column BETWEEN 1 AND 25 OR column = 27 OR column BETWEEN 29 AND 45"
  /// </remarks>
  internal static SQLExpression CreateOptimizedListInclusionCheck(
    SQLExpression column,
    List<int> list)
  {
    if (list.Count == 1)
      return column.EQ((object) list[0]);
    if (list.Count <= 100)
      return CreateListExpression(list);
    List<int> list1 = list.OrderBy<int, int>((Func<int, int>) (x => x)).Distinct<int>().ToList<int>();
    List<int> intList = new List<int>();
    List<SQLExpression> expressions = new List<SQLExpression>();
    do
    {
      int v1 = list1.First<int>();
      int v2 = v1;
      for (int index = 1; index < list1.Count; ++index)
      {
        int num = list1[index];
        if (num > v2 + 1)
        {
          list1.RemoveRange(0, index);
          break;
        }
        v2 = num;
      }
      if (v2 == list1.Last<int>())
        list1.Clear();
      if (v2 == v1)
        intList.Add(v2);
      else
        expressions.Add(column.Between((SQLExpression) new SQLConst((object) v1), (SQLExpression) new SQLConst((object) v2)));
      if (intList.Count >= 20)
      {
        expressions.Add(CreateListExpression(intList));
        intList.Clear();
      }
    }
    while (list1.Any<int>());
    if (intList.Any<int>())
      expressions.Add(CreateListExpression(intList));
    return Query.JoinExpressions(expressions);

    SQLExpression CreateListExpression(List<int> items)
    {
      if (items.Count == 1)
        return column.EQ((object) items[0]);
      SQLExpression exp = SQLExpression.None();
      foreach (int v in items)
        exp = exp.Seq((SQLExpression) new SQLConst((object) v));
      return column.In(exp).Embrace();
    }
  }

  private static SQLExpression JoinExpressions(List<SQLExpression> expressions)
  {
    SQLExpression sqlExpression = (SQLExpression) null;
    if (expressions.Count <= 20)
    {
      foreach (SQLExpression expression in expressions)
        sqlExpression = sqlExpression?.Or(expression) ?? expression;
    }
    else
    {
      int chunkSize = expressions.Count / 20;
      if (chunkSize < 20)
        chunkSize = 20;
      foreach (List<SQLExpression> expressions1 in expressions.Select<SQLExpression, (SQLExpression, int)>((Func<SQLExpression, int, (SQLExpression, int)>) ((x, index) => (x, index / chunkSize))).GroupBy<(SQLExpression, int), int>((Func<(SQLExpression, int), int>) (x => x.ChunkNumber)).Select<IGrouping<int, (SQLExpression, int)>, List<SQLExpression>>((Func<IGrouping<int, (SQLExpression, int)>, List<SQLExpression>>) (x => x.Select<(SQLExpression, int), SQLExpression>((Func<(SQLExpression, int), SQLExpression>) (e => e.Expression)).ToList<SQLExpression>())))
      {
        SQLExpression r = Query.JoinExpressions(expressions1).Embrace();
        sqlExpression = sqlExpression?.Or(r) ?? r;
      }
    }
    return sqlExpression;
  }

  internal Query InjectDirectExpressions(PXDataValue[] pars)
  {
    List<Literal> source = this.CollectParams();
    for (int index = 0; index < pars.Length; ++index)
    {
      PXDataValue par = pars[index];
      if (par.ValueType == PXDbType.DirectExpression)
      {
        int j = index;
        IEnumerable<Literal> literals = source.Where<Literal>((Func<Literal, bool>) (l => l.Number == j));
        if (par.Value is Array v)
        {
          foreach (Literal literal in literals)
          {
            literal.SetOper(SQLExpression.Operation.EMPTY);
            literal.SetLeft((SQLExpression) new SQLConst((object) v));
          }
        }
        else if (par is PXFieldName)
        {
          foreach (Literal literal in literals)
          {
            literal.SetOper(SQLExpression.Operation.EMPTY);
            if ((par as PXFieldName).FieldExpr == null)
              literal.SetLeft((SQLExpression) new SQLConst((object) par));
            else
              literal.SetLeft((par as PXFieldName).FieldExpr);
          }
        }
        else
        {
          foreach (Literal literal in literals)
          {
            literal.SetOper(SQLExpression.Operation.EMPTY);
            literal.SetLeft((SQLExpression) new SQLConst(par.Value));
          }
        }
      }
    }
    return this;
  }

  internal Query EnforceSelectionAliases()
  {
    foreach (SQLExpression sqlExpression in this.selection_)
    {
      if (sqlExpression.Alias() == null)
      {
        if (sqlExpression is Column column)
          sqlExpression.SetAlias(column.Name);
        if (sqlExpression is SubQuery subQuery)
          sqlExpression.SetAlias(subQuery.getSurrogateAlias());
      }
    }
    return this;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = this.SQLQuery((Connection) new MSSQLConnection());
    if (this.projection_ != null)
      stringBuilder.Append(" INTO " + this.projection_.ToString());
    return stringBuilder.ToString();
  }

  public Query Limit(int l)
  {
    this.limitRowCount_ = l;
    return this;
  }

  public Query Limit(int rowCount, int offset = 0)
  {
    this.limitOffset_ = offset;
    this.limitRowCount_ = rowCount;
    return this;
  }

  public Query Offset(int o)
  {
    this.limitOffset_ = o;
    return this;
  }

  public SQLExpression Exists() => new SubQuery(this).Exists();

  public SQLExpression NotExists() => new SubQuery(this).NotExists();

  public Query GroupBy(List<SQLExpression> gb)
  {
    this.groupBy_ = gb;
    return this;
  }

  public Query GroupBy(SQLExpression e)
  {
    if (this.groupBy_ == null)
      this.groupBy_ = new List<SQLExpression>();
    this.groupBy_.Add(e);
    return this;
  }

  public Query From(Table t)
  {
    this.from_.Add(new Joiner(t, this));
    return this;
  }

  public Query From<DAC>() where DAC : IBqlTable => this.From(typeof (DAC));

  public Query From(System.Type dac)
  {
    this.from_.Add(new Joiner(dac, this));
    return this;
  }

  public Query Where(SQLExpression w)
  {
    this.where_ = w;
    return this;
  }

  public Query AndWhere(SQLExpression w)
  {
    this.where_ = this.where_?.And(w) ?? w;
    return this;
  }

  public Query OrWhere(SQLExpression w)
  {
    this.where_ = this.where_?.Or(w) ?? w;
    return this;
  }

  public Query Having(SQLExpression having)
  {
    this._having = having;
    return this;
  }

  public Query ApplyHints(QueryHints h)
  {
    this.hints_ = h;
    return this;
  }

  public Joiner Join<DAC>() where DAC : IBqlTable => this.Join(typeof (DAC));

  public Joiner Join(System.Type dac, Joiner.JoinType joinType = Joiner.JoinType.INNER_JOIN)
  {
    Joiner joiner = new Joiner(joinType, dac, this);
    this.from_.Add(joiner);
    return joiner;
  }

  public Joiner LeftJoin(System.Type dac)
  {
    Joiner joiner = new Joiner(Joiner.JoinType.LEFT_JOIN, dac, this);
    this.from_.Add(joiner);
    return joiner;
  }

  public Joiner LeftJoin(Table t)
  {
    Joiner joiner = new Joiner(Joiner.JoinType.LEFT_JOIN, t, this);
    this.from_.Add(joiner);
    return joiner;
  }

  public Joiner InnerJoin(Table t)
  {
    Joiner joiner = new Joiner(Joiner.JoinType.INNER_JOIN, t, this);
    this.from_.Add(joiner);
    return joiner;
  }

  internal Joiner RightJoin(Table t)
  {
    Joiner joiner = new Joiner(Joiner.JoinType.RIGHT_JOIN, t, this);
    this.from_.Add(joiner);
    return joiner;
  }

  internal Joiner FullJoin(Table t)
  {
    Joiner joiner = new Joiner(Joiner.JoinType.FULL_JOIN, t, this);
    this.from_.Add(joiner);
    return joiner;
  }

  internal Joiner CrossJoin(Table t)
  {
    Joiner joiner = new Joiner(Joiner.JoinType.CROSS_JOIN, t, this);
    this.from_.Add(joiner);
    return joiner;
  }

  internal Joiner AddJoin(Joiner j)
  {
    this.from_.Add(j);
    return j;
  }

  public Unioner Union<DAC>() where DAC : IBqlTable => this.Union(typeof (DAC));

  public Unioner UnionAll<DAC>() where DAC : IBqlTable => this.UnionAll(typeof (DAC));

  public Unioner Union(System.Type dac)
  {
    Unioner unioner = new Unioner(Unioner.UnionType.UNION, dac, this);
    this.union_.Add(unioner);
    return unioner;
  }

  public Unioner UnionAll(System.Type dac)
  {
    Unioner unioner = new Unioner(Unioner.UnionType.UNIONALL, dac, this);
    this.union_.Add(unioner);
    return unioner;
  }

  internal Unioner AddUnion(Unioner u)
  {
    this.union_.Add(u);
    return u;
  }

  public Query OrderAsc(SQLExpression exp)
  {
    if (this.order_ == null)
      this.order_ = new List<OrderSegment>();
    this.order_.Add(new OrderSegment(exp));
    return this;
  }

  public Query OrderDesc(SQLExpression exp)
  {
    if (this.order_ == null)
      this.order_ = new List<OrderSegment>();
    this.order_.Add(new OrderSegment(exp, false));
    return this;
  }

  public Query OrderAsc(System.Type field)
  {
    if (this.order_ == null)
      this.order_ = new List<OrderSegment>();
    this.order_.Add(new OrderSegment((SQLExpression) new Column(field)));
    return this;
  }

  public Query OrderDesc(System.Type field)
  {
    if (this.order_ == null)
      this.order_ = new List<OrderSegment>();
    this.order_.Add(new OrderSegment((SQLExpression) new Column(field), false));
    return this;
  }

  internal void AddOrderSegment(OrderSegment segment)
  {
    if (this.order_ == null)
      this.order_ = new List<OrderSegment>();
    this.order_.Add(segment);
  }

  public Query Field(Query sq)
  {
    this.selection_.Add((SQLExpression) new SubQuery(sq));
    return this;
  }

  public Query this[Query sq]
  {
    get
    {
      this.Field(sq);
      return this;
    }
  }

  public Query Fields(System.Type dac)
  {
    SimpleTable t = new SimpleTable(dac);
    foreach (PropertyInfo property in dac.GetProperties())
    {
      bool flag = false;
      foreach (object customAttribute in property.GetCustomAttributes(true))
      {
        if (typeof (PXDBFieldAttribute).IsAssignableFrom(customAttribute.GetType()) || typeof (PXDBCreatedByIDAttribute).IsAssignableFrom(customAttribute.GetType()))
        {
          flag = true;
          break;
        }
      }
      if (flag)
        this.selection_.Add((SQLExpression) new Column(property.Name, (Table) t));
    }
    return this;
  }

  public Query Fields(SQLExpression fieldList)
  {
    if (fieldList == null)
      return this;
    if (fieldList.Oper() == SQLExpression.Operation.SEQ)
    {
      this.Fields(fieldList.LExpr());
      this.Fields(fieldList.RExpr());
    }
    else
      this.Field(fieldList);
    return this;
  }

  public Query Field(SQLExpression field)
  {
    this.selection_.Add(field);
    return this;
  }

  public Query Field(System.Type field, string alias = null)
  {
    Column column = new Column(field);
    if (alias != null)
      column.SetAlias(alias);
    this.selection_.Add((SQLExpression) column);
    return this;
  }

  public Query this[System.Type field]
  {
    get
    {
      this.Field(field);
      return this;
    }
  }

  public Query SelectAll<DAC>() where DAC : IBqlTable => this.Fields(typeof (DAC));

  public Query Select<FIELD>() where FIELD : IBqlField
  {
    this.selection_.Add((SQLExpression) new Column(typeof (FIELD)));
    return this;
  }

  public Query Select(params SQLExpression[] selection)
  {
    this.selection_.AddRange((IEnumerable<SQLExpression>) selection);
    return this;
  }

  [PXInternalUseOnly]
  public override void GetExpressionsOfType(
    Predicate<SQLExpression> predicate,
    List<SQLExpression> result)
  {
    base.GetExpressionsOfType(predicate, result);
    this.selection_?.ForEach((System.Action<SQLExpression>) (e => e.FillExpressionsOfType(predicate, result)));
    this.from_?.ForEach((System.Action<Joiner>) (e => e.GetExpressionsOfType(predicate, result)));
    this.union_?.ForEach((System.Action<Unioner>) (e => e.GetExpressionsOfType(predicate, result)));
    this.where_?.FillExpressionsOfType(predicate, result);
    this.groupBy_?.ForEach((System.Action<SQLExpression>) (e => e.FillExpressionsOfType(predicate, result)));
    this._having?.FillExpressionsOfType(predicate, result);
    this.order_?.ForEach((System.Action<OrderSegment>) (e => e.expr_?.FillExpressionsOfType(predicate, result)));
  }

  internal override T Accept<T>(ISQLQueryVisitor<T> visitor) => visitor.Visit(this);

  internal List<Table> GetAllTables()
  {
    List<Table> tables = new List<Table>();
    this.GetAllTables(tables);
    return tables;
  }

  internal void GetAllTables(List<Table> tables)
  {
    if (tables == null)
      return;
    tables.Add((Table) this);
    this.from_.ForEach((System.Action<Joiner>) (e => e.GetAllTables(tables)));
    this.union_.ForEach((System.Action<Unioner>) (e => e.GetAllTables(tables)));
  }

  internal class SimpleColumnDesc
  {
    public string TableAlias;
    public string ColumnName;
    public string Alias;

    public SimpleColumnDesc(string table, string column, string alias)
    {
      this.TableAlias = table;
      this.ColumnName = column;
      this.Alias = alias;
    }

    public override string ToString() => $"{this.TableAlias}.{this.ColumnName} {this.Alias}";
  }

  private class ColumnExpressionComparer : IEqualityComparer<SQLExpression>
  {
    private readonly SimpleTable _mainTable;
    internal static Query.ColumnExpressionComparer DefaultComparer = new Query.ColumnExpressionComparer(new SimpleTable(nameof (ColumnExpressionComparer)));

    public ColumnExpressionComparer(SimpleTable mainTable) => this._mainTable = mainTable;

    public bool Equals(SQLExpression x, SQLExpression y)
    {
      if (x == null || y == null || x.Oper() != y.Oper())
        return false;
      if (this.SimpleCompare(x.LExpr(), y.LExpr()) && this.SimpleCompare(x.RExpr(), y.RExpr()))
        return true;
      return this.SimpleCompare(x.LExpr(), y.RExpr()) && this.SimpleCompare(x.RExpr(), y.LExpr());
    }

    internal bool CompareColumns(Column xLColumn, Column yLColumn)
    {
      if (xLColumn == null || yLColumn == null || !xLColumn.Name.OrdinalEquals(yLColumn.Name))
        return false;
      SimpleTable simpleTable1 = xLColumn.Table() as SimpleTable;
      SimpleTable simpleTable2 = yLColumn.Table() as SimpleTable;
      return simpleTable1 != null && simpleTable2 != null && (!simpleTable1.Name.OrdinalEquals(this._mainTable.Name) || simpleTable2.Name.OrdinalEquals(this._mainTable.Name) || simpleTable2.AliasOrName().OrdinalEquals(this._mainTable.AliasOrName())) && (simpleTable1.Name.OrdinalEquals(this._mainTable.Name) || simpleTable1.AliasOrName().OrdinalEquals(simpleTable2.AliasOrName()));
    }

    private bool SimpleCompare(SQLExpression x, SQLExpression y)
    {
      if (x == null || y == null || x.Oper() != y.Oper())
        return false;
      return this.CompareColumns(x as Column, y as Column) || x.Equals(y);
    }

    public int GetHashCode(SQLExpression obj)
    {
      SQLExpression.Operation operation = obj.Oper();
      int num1 = (operation.GetHashCode() * 397 ^ obj.GetType().GetHashCode()) * 397;
      int num2;
      if (obj.RExpr() == null)
      {
        num2 = 0;
      }
      else
      {
        operation = obj.RExpr().Oper();
        num2 = operation.GetHashCode();
      }
      int num3 = ((num1 ^ num2) * 397 ^ (obj.RExpr() != null ? obj.RExpr().GetType().GetHashCode() : 0)) * 397;
      int num4;
      if (obj.LExpr() == null)
      {
        num4 = 0;
      }
      else
      {
        operation = obj.LExpr().Oper();
        num4 = operation.GetHashCode();
      }
      return (num3 ^ num4) * 397 ^ (obj.LExpr() != null ? obj.LExpr().GetType().GetHashCode() : 0);
    }
  }
}
