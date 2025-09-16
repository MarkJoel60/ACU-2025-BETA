// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.JoinedAttrQuery
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class JoinedAttrQuery : Query
{
  internal string srcTable_;
  internal string keyCol_;
  internal string valCol_;
  internal string kvExtCol_;

  public JoinedAttrQuery(
    string srcTable,
    string keyCol,
    string valCol,
    string kvExtCol,
    SQLExpression where)
    : this((Table) new SimpleTable(srcTable), keyCol, valCol, kvExtCol, where)
  {
  }

  public JoinedAttrQuery(
    Table srcTable,
    string keyCol,
    string valCol,
    string kvExtCol,
    SQLExpression where)
  {
    this.srcTable_ = srcTable.AliasOrName();
    this.keyCol_ = keyCol;
    this.valCol_ = valCol;
    this.kvExtCol_ = kvExtCol;
    this.AddJoin(new Joiner(Joiner.JoinType.MAIN_TABLE, srcTable, (Query) this));
    this.Where(where);
  }

  public JoinedAttrQuery(JoinedAttrQuery other)
    : base((Query) other)
  {
    this.srcTable_ = other.srcTable_;
    this.keyCol_ = other.keyCol_;
    this.valCol_ = other.valCol_;
    this.kvExtCol_ = other.kvExtCol_;
  }

  internal override Table Duplicate() => (Table) new JoinedAttrQuery(this);

  internal override T Accept<T>(ISQLQueryVisitor<T> visitor) => visitor.Visit(this);
}
