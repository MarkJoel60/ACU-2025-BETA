// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLRank
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class SQLRank : SQLExpression
{
  public SQLRank()
    : base()
  {
  }

  protected SQLRank(SQLRank other)
    : base((SQLExpression) other)
  {
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLRank(this);

  public SQLRank SetField(SQLExpression field)
  {
    this.rexpr_ = field;
    return this;
  }

  public SQLRank SetFTS(SQLFullTextSearch fts)
  {
    this.FTS = fts;
    return this;
  }

  public SQLExpression Field => this.rexpr_;

  public SQLFullTextSearch FTS { get; private set; }

  public override string ToString()
  {
    string str = this.lexpr_ == null ? "" : this.lexpr_.ToString();
    return $"{(this.IsEmbraced() ? "(" : "")}RankOf({str}){(this.IsEmbraced() ? ")" : "")}";
  }

  internal override PXDbType GetDBType() => PXDbType.Int;

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
