// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLSwitch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

public class SQLSwitch : SQLExpression
{
  private List<Tuple<SQLExpression, SQLExpression>> whenThens_ = new List<Tuple<SQLExpression, SQLExpression>>();

  public SQLSwitch()
    : base()
  {
  }

  protected SQLSwitch(SQLSwitch other)
    : base((SQLExpression) other)
  {
    foreach (Tuple<SQLExpression, SQLExpression> whenThen in other.whenThens_)
      this.whenThens_.Add(Tuple.Create<SQLExpression, SQLExpression>(whenThen.Item1?.Duplicate(), whenThen.Item2?.Duplicate()));
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLSwitch(this);

  internal List<Tuple<SQLExpression, SQLExpression>> GetCases() => this.whenThens_;

  public SQLSwitch Case(SQLExpression when, SQLExpression then)
  {
    this.whenThens_.Add(Tuple.Create<SQLExpression, SQLExpression>(when, then));
    return this;
  }

  public SQLSwitch Case(SQLSwitch next)
  {
    if (next != null)
      this.whenThens_.AddRange((IEnumerable<Tuple<SQLExpression, SQLExpression>>) next.whenThens_);
    return this;
  }

  public SQLSwitch Default(SQLExpression def)
  {
    this.rexpr_ = def;
    return this;
  }

  public SQLExpression GetDefault() => this.rexpr_;

  internal override string getSurrogateAlias()
  {
    foreach (Tuple<SQLExpression, SQLExpression> whenThen in this.whenThens_)
    {
      string surrogateAlias = whenThen.Item1.getSurrogateAlias();
      if (surrogateAlias != null)
        return surrogateAlias;
    }
    return (string) null;
  }

  internal override SQLExpression substituteTableName(string from, string to)
  {
    for (int index = 0; index < this.whenThens_.Count; ++index)
    {
      this.whenThens_[index].Item1?.substituteTableName(from, to);
      this.whenThens_[index].Item2?.substituteTableName(from, to);
    }
    this.rexpr_?.substituteTableName(from, to);
    return (SQLExpression) this;
  }

  internal override SQLExpression substituteNode(SQLExpression from, SQLExpression to)
  {
    SQLExpression sqlExpression1 = base.substituteNode(from, to);
    if (this != sqlExpression1)
      return sqlExpression1;
    for (int index = 0; index < this.whenThens_.Count; ++index)
    {
      SQLExpression sqlExpression2 = this.whenThens_[index].Item1.substituteNode(from, to);
      SQLExpression sqlExpression3 = this.whenThens_[index].Item2.substituteNode(from, to);
      if (sqlExpression2 != this.whenThens_[index].Item1 || sqlExpression3 != this.whenThens_[index].Item2)
        this.whenThens_[index] = Tuple.Create<SQLExpression, SQLExpression>(sqlExpression2, sqlExpression3);
    }
    return (SQLExpression) this;
  }

  public override bool Equals(SQLExpression other)
  {
    if (!(other is SQLSwitch sqlSwitch))
      return false;
    if (this == sqlSwitch)
      return true;
    if (!object.Equals((object) this.rexpr_, (object) sqlSwitch.rexpr_) || this.whenThens_.Count != sqlSwitch.whenThens_.Count)
      return false;
    for (int index = 0; index < this.whenThens_.Count; ++index)
    {
      Tuple<SQLExpression, SQLExpression> whenThen1 = this.whenThens_[index];
      Tuple<SQLExpression, SQLExpression> whenThen2 = sqlSwitch.whenThens_[index];
      if (!object.Equals((object) whenThen1.Item1, (object) whenThen2.Item1) || !object.Equals((object) whenThen1.Item2, (object) whenThen2.Item2))
        return false;
    }
    return true;
  }

  internal override SQLExpression unembraceAnds()
  {
    base.unembraceAnds();
    foreach (Tuple<SQLExpression, SQLExpression> whenThen in this.whenThens_)
    {
      whenThen.Item1?.unembraceAnds();
      whenThen.Item2?.unembraceAnds();
    }
    return (SQLExpression) this;
  }

  internal override SQLExpression unembraceOrs()
  {
    base.unembraceOrs();
    foreach (Tuple<SQLExpression, SQLExpression> whenThen in this.whenThens_)
    {
      whenThen.Item1?.unembraceOrs();
      whenThen.Item2?.unembraceOrs();
    }
    return (SQLExpression) this;
  }

  internal override void FillExpressionsOfType(
    Predicate<SQLExpression> predicate,
    List<SQLExpression> res)
  {
    base.FillExpressionsOfType(predicate, res);
    foreach (Tuple<SQLExpression, SQLExpression> whenThen in this.whenThens_)
    {
      whenThen.Item1?.FillExpressionsOfType(predicate, res);
      whenThen.Item2?.FillExpressionsOfType(predicate, res);
    }
  }

  internal override SQLExpression substituteColumnAliases(Dictionary<string, string> dict)
  {
    foreach (Tuple<SQLExpression, SQLExpression> whenThen in this.whenThens_)
    {
      whenThen.Item1?.substituteColumnAliases(dict);
      whenThen.Item2?.substituteColumnAliases(dict);
    }
    this.rexpr_?.substituteColumnAliases(dict);
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
    for (int index = 0; index < this.whenThens_.Count; ++index)
      this.whenThens_[index] = Tuple.Create<SQLExpression, SQLExpression>(this.whenThens_[index].Item1?.substituteExternalColumnAliases(aliases, queryPart, insideAggregate, excludes, isMaxAggregate, isInsideGroupBy), this.whenThens_[index].Item2?.substituteExternalColumnAliases(aliases, queryPart, insideAggregate, excludes, isMaxAggregate, isInsideGroupBy));
    this.rexpr_ = this.rexpr_?.substituteExternalColumnAliases(aliases, queryPart, insideAggregate, excludes, isMaxAggregate, isInsideGroupBy);
    return (SQLExpression) this;
  }

  internal override SQLExpression addExternalAliasToTableNames(
    string alias,
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    foreach (Tuple<SQLExpression, SQLExpression> whenThen in this.whenThens_)
    {
      whenThen.Item1?.addExternalAliasToTableNames(alias, aliases, excludes);
      whenThen.Item2?.addExternalAliasToTableNames(alias, aliases, excludes);
    }
    this.rexpr_?.addExternalAliasToTableNames(alias, aliases, excludes);
    return (SQLExpression) this;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder("CASE ");
    foreach (Tuple<SQLExpression, SQLExpression> whenThen in this.whenThens_)
      stringBuilder.Append($"WHEN {whenThen.Item1} THEN {whenThen.Item2} ");
    if (this.rexpr_ != null)
      stringBuilder.Append($"ELSE {this.rexpr_} ");
    stringBuilder.Append("END ");
    return stringBuilder.ToString();
  }

  internal override PXDbType GetDBType()
  {
    List<PXDbType?> list = this.whenThens_.Select<Tuple<SQLExpression, SQLExpression>, PXDbType?>((Func<Tuple<SQLExpression, SQLExpression>, PXDbType?>) (wt => wt.Item2?.GetDBType())).ToList<PXDbType?>();
    if (this.rexpr_ != null)
      list.Add(new PXDbType?(this.rexpr_.GetDBType()));
    return SQLExpression.GetMaxByPrecedence(list.Where<PXDbType?>((Func<PXDbType?, bool>) (t =>
    {
      PXDbType? nullable = t;
      PXDbType pxDbType = PXDbType.Unspecified;
      return !(nullable.GetValueOrDefault() == pxDbType & nullable.HasValue);
    })).ToArray<PXDbType?>());
  }

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
