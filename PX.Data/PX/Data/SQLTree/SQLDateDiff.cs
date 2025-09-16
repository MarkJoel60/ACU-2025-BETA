// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLDateDiff
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class SQLDateDiff : SQLExpression
{
  private string uom_;

  protected SQLDateDiff(SQLDateDiff other)
    : base((SQLExpression) other)
  {
    this.uom_ = other.uom_;
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLDateDiff(this);

  private SQLDateDiff(string uom)
    : base()
  {
    this.oper_ = SQLExpression.Operation.DATE_DIFF;
    this.uom_ = uom;
  }

  public SQLDateDiff(IConstant<string> uom)
    : this(uom.Value)
  {
  }

  public SQLDateDiff(IConstant<string> uom, SQLExpression left, SQLExpression right)
    : this(uom)
  {
    this.lexpr_ = left;
    this.rexpr_ = right;
  }

  internal string UOM() => this.uom_;

  internal override PXDbType GetDBType() => PXDbType.Int;

  public override string ToString() => $"DATEDIFF({this.uom_}, {this.lexpr_}, {this.rexpr_})";

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
