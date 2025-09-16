// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLScalarFunction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class SQLScalarFunction : SQLExpression
{
  public string Name { get; }

  public SQLScalarFunction(string name, params SQLExpression[] funcArgs)
    : base()
  {
    this.Name = name;
    foreach (SQLExpression funcArg in funcArgs)
      this.lexpr_ = this.lexpr_?.Seq(funcArg) ?? funcArg;
  }

  protected SQLScalarFunction(SQLScalarFunction other)
    : base((SQLExpression) other)
  {
    this.Name = other.Name;
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLScalarFunction(this);

  internal override PXDbType GetDBType() => PXDbType.Unspecified;

  public override string ToString() => $"{this.Name}({this.lexpr_})";

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
