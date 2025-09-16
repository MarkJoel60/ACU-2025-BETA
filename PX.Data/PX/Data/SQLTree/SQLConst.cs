// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLConst
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.SQLTree;

public class SQLConst : SQLExpression, ISQLDBTypedExpression, ISQLConstantExpression
{
  private object value_;
  private PXDbType type_ = PXDbType.Unspecified;

  protected SQLConst(SQLConst other)
    : base((SQLExpression) other)
  {
    this.value_ = other.value_;
    this.type_ = other.type_;
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLConst(this);

  internal static SQLConst NewParameter(object v)
  {
    return new SQLConst(v) { type_ = PXDbType.Unspecified };
  }

  public SQLConst(object v)
    : base()
  {
    this.value_ = v;
    switch (v)
    {
      case Decimal _:
        this.type_ = PXDbType.Decimal;
        break;
      case float _:
        this.type_ = PXDbType.Real;
        break;
      case double _:
        this.type_ = PXDbType.Float;
        break;
      case bool _:
        this.type_ = PXDbType.Bit;
        break;
      case System.DateTime _:
        this.type_ = PXDbType.DateTime;
        break;
      case int _:
        this.type_ = PXDbType.Int;
        break;
      case long _:
        this.type_ = PXDbType.BigInt;
        break;
    }
  }

  public object GetValue() => this.value_;

  public override bool Equals(SQLExpression other)
  {
    return other is SQLConst sqlConst && object.Equals(this.value_, sqlConst.value_);
  }

  internal override SQLExpression substituteConstant(string from, string to)
  {
    if (this.value_ as string == from)
      this.value_ = (object) to;
    return (SQLExpression) this;
  }

  internal override object evaluateConstant() => this.value_;

  protected internal override SQLExpression.CLType isConstantOrLiteral()
  {
    return SQLExpression.CLType.CONSTANT;
  }

  public void SetDBType(PXDbType type)
  {
    if (type == PXDbType.Unspecified)
      return;
    this.type_ = type;
  }

  internal override PXDbType GetDBType() => this.type_;

  bool ISQLConstantExpression.IsLatin1()
  {
    if (!(this.value_ is string str))
      return false;
    for (int index = 0; index < str.Length; ++index)
    {
      if (str[index] > 'ÿ')
        return false;
    }
    return true;
  }

  public override string ToString() => this.value_?.ToString() ?? "NULL";

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
