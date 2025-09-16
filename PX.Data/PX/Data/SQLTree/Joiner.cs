// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Joiner
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

[DebuggerDisplay("{table_?.Alias} {join_} {owner_}")]
public class Joiner
{
  private Joiner.JoinHint hint_;
  private Joiner.JoinType join_;
  private PX.Data.SQLTree.Table table_;
  private SQLExpression on_;
  private Query owner_;

  private Joiner()
  {
  }

  internal Joiner DuplicateTo(Query query)
  {
    return new Joiner()
    {
      join_ = this.join_,
      hint_ = this.hint_,
      table_ = this.table_?.Duplicate(),
      on_ = this.on_?.Duplicate(),
      owner_ = query,
      RowSharingAllowed = this.RowSharingAllowed
    };
  }

  internal Joiner transformTableName(string from, string to)
  {
    this.table_ = this.table_?.transformTableName(from, to);
    return this;
  }

  internal Joiner substituteExternalColumnAliases(
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    this.on_ = this.on_?.substituteExternalColumnAliases(aliases, QueryPart.From, excludes: excludes);
    this.table_ = this.table_?.substituteExternalColumnAliases(aliases, excludes);
    return this;
  }

  internal Joiner substituteColumnAliases(Dictionary<string, string> dict)
  {
    this.on_?.substituteColumnAliases(dict);
    this.table_?.substituteColumnAliases(dict);
    return this;
  }

  internal Joiner addExternalAliasToTableNames(
    string alias,
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    this.table_?.addExternalAliasToTableNames(alias, aliases, excludes);
    this.on_?.addExternalAliasToTableNames(alias, aliases, excludes);
    return this;
  }

  internal Joiner PreventRowSharing()
  {
    if (this.join_ != Joiner.JoinType.INNER_JOIN && this.join_ != Joiner.JoinType.LEFT_JOIN && this.join_ != Joiner.JoinType.MAIN_TABLE)
      return this;
    this.RowSharingAllowed = false;
    if (this.table_ is Query table)
    {
      List<Joiner> from = table.GetFrom();
      if (from != null)
        from.FirstOrDefault<Joiner>()?.PreventRowSharing();
    }
    return this;
  }

  internal bool RowSharingAllowed { get; private set; } = true;

  internal Joiner SetJoinType(Joiner.JoinType jt)
  {
    this.join_ = jt;
    return this;
  }

  internal Joiner SetHint(Joiner.JoinHint jh)
  {
    this.hint_ = jh;
    return this;
  }

  internal Joiner setTable(PX.Data.SQLTree.Table t)
  {
    this.table_ = t;
    return this;
  }

  internal SQLExpression getOn() => this.on_;

  internal Joiner setOn(SQLExpression on)
  {
    this.on_ = on;
    return this;
  }

  public Joiner.JoinType Join() => this.join_;

  internal Joiner.JoinHint Hint() => this.hint_;

  public PX.Data.SQLTree.Table Table() => this.table_;

  public SQLExpression Condition() => this.on_;

  public Joiner(PX.Data.SQLTree.Table t, Query q)
  {
    this.join_ = Joiner.JoinType.MAIN_TABLE;
    this.table_ = t;
    this.on_ = (SQLExpression) null;
    this.owner_ = q;
  }

  public Joiner(Joiner.JoinType jt, PX.Data.SQLTree.Table t, Query q)
  {
    this.join_ = jt;
    this.table_ = t;
    this.on_ = (SQLExpression) null;
    this.owner_ = q;
  }

  public Joiner(System.Type dac, Query q)
  {
    this.join_ = Joiner.JoinType.MAIN_TABLE;
    this.table_ = (PX.Data.SQLTree.Table) new SimpleTable(dac);
    this.on_ = (SQLExpression) null;
    this.owner_ = q;
  }

  public Joiner(Joiner.JoinType jt, System.Type dac, Query q)
  {
    this.join_ = jt;
    this.table_ = (PX.Data.SQLTree.Table) new SimpleTable(dac);
    this.on_ = (SQLExpression) null;
    this.owner_ = q;
  }

  public Query On(SQLExpression e)
  {
    if (this.IsMain())
      this.owner_.Where(e.And(this.owner_.GetWhere()));
    else
      this.on_ = e;
    return this.owner_;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    switch (this.join_)
    {
      case Joiner.JoinType.MAIN_TABLE:
        stringBuilder.Append("FROM ");
        break;
      case Joiner.JoinType.MAIN_OUTER_TABLE:
        stringBuilder.Append("FROM OUTER ");
        break;
      case Joiner.JoinType.CROSS_JOIN:
        stringBuilder.Append("CROSS JOIN ");
        break;
      case Joiner.JoinType.INNER_JOIN:
        stringBuilder.Append("INNER JOIN ");
        break;
      case Joiner.JoinType.LEFT_JOIN:
        stringBuilder.Append("LEFT JOIN ");
        break;
      case Joiner.JoinType.RIGHT_JOIN:
        stringBuilder.Append("RIGHT JOIN ");
        break;
      case Joiner.JoinType.FULL_JOIN:
        stringBuilder.Append("FULL JOIN ");
        break;
    }
    stringBuilder.Append((object) this.table_);
    if (this.on_ != null && this.on_.Oper() != SQLExpression.Operation.NONE)
      stringBuilder.Append(" ON " + this.on_?.ToString());
    return stringBuilder.ToString();
  }

  internal virtual void GetExpressionsOfType(
    Predicate<SQLExpression> predicate,
    List<SQLExpression> result)
  {
    this.table_?.GetExpressionsOfType(predicate, result);
    this.on_?.FillExpressionsOfType(predicate, result);
  }

  internal bool IsMain()
  {
    return this.join_ == Joiner.JoinType.MAIN_TABLE || this.join_ == Joiner.JoinType.MAIN_OUTER_TABLE;
  }

  internal virtual T Accept<T>(ISQLQueryVisitor<T> visitor) => visitor.Visit(this);

  public void GetAllTables(List<PX.Data.SQLTree.Table> tables)
  {
    if (tables == null)
      return;
    if (this.table_ != null)
      tables.Add(this.table_);
    if (!(this.table_ is Query table))
      return;
    table.GetAllTables(tables);
  }

  public enum JoinHint
  {
    NONE = 0,
    [Obsolete("This value acts like the STRAIGHT_FOR_TPT value. Explicitly use STRAIGHT_TPT or STRAIGHT_FORCED instead.")] STRAIGHT = 1,
    STRAIGHT_FOR_TPT = 1,
    STRAIGHT_FORCED = 2,
  }

  public enum JoinType
  {
    UNDEFINED,
    MAIN_TABLE,
    MAIN_OUTER_TABLE,
    CROSS_JOIN,
    INNER_JOIN,
    LEFT_JOIN,
    RIGHT_JOIN,
    FULL_JOIN,
  }
}
