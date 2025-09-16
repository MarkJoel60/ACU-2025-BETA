// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Unioner
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

public class Unioner
{
  private Unioner.UnionType union_;
  private PX.Data.SQLTree.Table table_;
  private Query owner_;

  private Unioner()
  {
  }

  internal Unioner DuplicateTo(Query query)
  {
    return new Unioner()
    {
      union_ = this.union_,
      table_ = this.table_?.Duplicate(),
      owner_ = query
    };
  }

  internal Unioner transformTableName(string from, string to)
  {
    this.table_ = this.table_?.transformTableName(from, to);
    return this;
  }

  internal Unioner substituteExternalColumnAliases(
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    this.table_ = this.table_?.substituteExternalColumnAliases(aliases, excludes);
    return this;
  }

  internal Unioner substituteColumnAliases(Dictionary<string, string> dict)
  {
    this.table_?.substituteColumnAliases(dict);
    return this;
  }

  internal Unioner addExternalAliasToTableNames(
    string alias,
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    this.table_?.addExternalAliasToTableNames(alias, aliases, excludes);
    return this;
  }

  internal Unioner SetUnionType(Unioner.UnionType ut)
  {
    this.union_ = ut;
    return this;
  }

  internal Unioner setTable(PX.Data.SQLTree.Table t)
  {
    this.table_ = t;
    return this;
  }

  public Unioner.UnionType Union() => this.union_;

  public PX.Data.SQLTree.Table Table() => this.table_;

  public Unioner(PX.Data.SQLTree.Table t, Query q)
    : this(Unioner.UnionType.MAIN_TABLE, t, q)
  {
  }

  public Unioner(Unioner.UnionType ut, PX.Data.SQLTree.Table t, Query q)
  {
    this.union_ = ut;
    this.table_ = t;
    this.owner_ = q;
  }

  public Unioner(System.Type dac, Query q)
    : this(Unioner.UnionType.MAIN_TABLE, dac, q)
  {
  }

  public Unioner(Unioner.UnionType ut, System.Type dac, Query q)
  {
    this.union_ = ut;
    this.table_ = (PX.Data.SQLTree.Table) new SimpleTable(dac);
    this.owner_ = q;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    switch (this.union_)
    {
      case Unioner.UnionType.MAIN_TABLE:
        stringBuilder.Append("FROM ");
        break;
      case Unioner.UnionType.UNION:
        stringBuilder.Append("UNION ");
        break;
      case Unioner.UnionType.UNIONALL:
        stringBuilder.Append("UNION ALL ");
        break;
    }
    stringBuilder.Append((object) this.table_);
    return stringBuilder.ToString();
  }

  internal virtual void GetExpressionsOfType(
    Predicate<SQLExpression> predicate,
    List<SQLExpression> result)
  {
    this.table_?.GetExpressionsOfType(predicate, result);
  }

  internal bool IsMain() => this.union_ == Unioner.UnionType.MAIN_TABLE;

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

  public enum UnionType
  {
    MAIN_TABLE,
    UNION,
    UNIONALL,
  }
}
