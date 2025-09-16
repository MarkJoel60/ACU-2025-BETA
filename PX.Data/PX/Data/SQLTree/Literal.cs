// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Literal
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class Literal : SQLExpression, ISQLDBTypedExpression, ISQLConstantExpression
{
  private Literal()
    : base()
  {
  }

  public static Literal NewParameter(int p)
  {
    Literal literal = new Literal();
    literal.Number = p;
    literal.SetLeft((SQLExpression) SQLConst.NewParameter((object) p));
    return literal;
  }

  protected Literal(Literal other)
    : base((SQLExpression) other)
  {
    this.Number = other.Number;
  }

  public override SQLExpression Duplicate() => (SQLExpression) new Literal(this);

  public int Number { get; private set; }

  public void SetDBType(PXDbType type)
  {
    if (!(this.lexpr_ is SQLConst lexpr))
      return;
    // ISSUE: explicit non-virtual call
    __nonvirtual (lexpr.SetDBType(type));
  }

  internal override PXDbType GetDBType() => (this.lexpr_ as SQLConst).GetDBTypeOrDefault();

  internal override object evaluateConstant() => this.lexpr_?.evaluateConstant();

  protected internal override SQLExpression.CLType isConstantOrLiteral()
  {
    return this.oper_ == SQLExpression.Operation.EMPTY ? SQLExpression.CLType.CONSTANT : SQLExpression.CLType.LITERAL;
  }

  public override bool Equals(SQLExpression other)
  {
    return other is Literal literal && object.Equals((object) this.lexpr_, (object) literal.lexpr_);
  }

  bool ISQLConstantExpression.IsLatin1()
  {
    return this.lexpr_ is ISQLConstantExpression lexpr && lexpr.IsLatin1();
  }

  public override string ToString() => this.lexpr_ == null ? "" : "@P" + this.lexpr_?.ToString();

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
