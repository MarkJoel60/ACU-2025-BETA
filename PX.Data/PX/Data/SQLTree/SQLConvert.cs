// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLConvert
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.SQLTree;

public class SQLConvert : SQLExpression
{
  public System.Type TargetType { get; }

  internal string MySqlCharset { get; set; }

  internal int TargetTypeLength { get; }

  public SQLConvert(System.Type targetType, SQLExpression source)
    : base()
  {
    this.TargetType = targetType;
    this.rexpr_ = source;
  }

  protected SQLConvert(SQLConvert other)
    : base((SQLExpression) other)
  {
    this.TargetType = other.TargetType;
    this.TargetTypeLength = other.TargetTypeLength;
    this.MySqlCharset = other.MySqlCharset;
  }

  public SQLConvert(System.Type targetType, SQLExpression source, int targetTypeLength)
    : this(targetType, source)
  {
    this.TargetTypeLength = targetTypeLength;
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLConvert(this);

  internal override PXDbType GetDBType()
  {
    if (!string.IsNullOrEmpty(this.MySqlCharset))
      return this.rexpr_.GetDBType();
    if (this.TargetType == typeof (bool))
      return PXDbType.Bit;
    if (this.TargetType == typeof (System.DateTime))
      return PXDbType.DateTime;
    if (this.TargetType == typeof (string))
      return PXDbType.NVarChar;
    if (this.TargetType == typeof (float))
      return PXDbType.Real;
    if (this.TargetType == typeof (Decimal))
      return PXDbType.Decimal;
    if (this.TargetType == typeof (int))
      return PXDbType.Int;
    if (this.TargetType == typeof (short))
      return PXDbType.SmallInt;
    if (this.TargetType == typeof (long))
      return PXDbType.BigInt;
    if (this.TargetType == typeof (double))
      return PXDbType.Float;
    return this.TargetType == typeof (char) ? PXDbType.NChar : PXDbType.Unspecified;
  }

  public override string ToString()
  {
    return !string.IsNullOrEmpty(this.MySqlCharset) ? $"Convert({this.rexpr_} USING {this.MySqlCharset})" : $"Convert({this.TargetType.Name},{this.rexpr_})";
  }

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
