// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLAggConcat
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class SQLAggConcat : SQLExpression
{
  private string separator_;

  protected SQLAggConcat(SQLAggConcat other)
    : base((SQLExpression) other)
  {
    this.separator_ = other.separator_;
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLAggConcat(this);

  private SQLAggConcat(string separator)
    : base()
  {
    this.oper_ = SQLExpression.Operation.AGG_CONCAT;
    this.separator_ = separator;
  }

  public SQLAggConcat(IConstant<string> separator)
    : this(separator.Value)
  {
  }

  public SQLAggConcat(IConstant<string> separator, SQLExpression left)
    : this(separator)
  {
    this.lexpr_ = left;
  }

  public SQLAggConcat(SQLConst separator, SQLExpression left)
    : this(separator.GetValue().ToString())
  {
    this.lexpr_ = left;
  }

  internal string GetSeparator() => this.separator_;

  public override string ToString() => $"AGG_STRING({this.lexpr_}, {this.separator_})";

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
