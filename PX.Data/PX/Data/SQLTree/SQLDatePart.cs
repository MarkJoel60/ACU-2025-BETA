// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLDatePart
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class SQLDatePart : SQLExpression
{
  private string uom_;

  protected SQLDatePart(SQLDatePart other)
    : base((SQLExpression) other)
  {
    this.uom_ = other.uom_;
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLDatePart(this);

  public SQLDatePart(IConstant<string> uom, SQLExpression date)
    : base()
  {
    this.oper_ = SQLExpression.Operation.DATE_PART;
    this.uom_ = uom.Value;
    this.lexpr_ = date;
  }

  internal string UOM() => this.uom_;

  public SQLExpression Date => this.lexpr_;

  internal override PXDbType GetDBType() => PXDbType.Int;

  public override string ToString() => $"DATEPART({this.uom_}, {this.lexpr_}, {this.rexpr_})";

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
