// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLDateAdd
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class SQLDateAdd : SQLExpression
{
  public SQLDateAdd(IConstant<string> datePart, SQLExpression number, SQLExpression date)
    : base()
  {
    this.DatePart = datePart.Value;
    this.lexpr_ = date;
    this.rexpr_ = number;
  }

  public string DatePart { get; }

  public SQLExpression Date => this.lexpr_;

  public SQLExpression Number => this.rexpr_;

  protected SQLDateAdd(SQLDateAdd other)
    : base((SQLExpression) other)
  {
    this.DatePart = other.DatePart;
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLDateAdd(this);

  internal override PXDbType GetDBType() => this.lexpr_.GetDBType();

  public override string ToString() => $"DateAdd({this.DatePart},{this.rexpr_},{this.lexpr_})";

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
