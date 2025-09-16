// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLSmartConvert
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class SQLSmartConvert : SQLConvert
{
  public SQLSmartConvert(System.Type targetType, SQLExpression source)
    : base(targetType, source)
  {
  }

  protected SQLSmartConvert(SQLConvert other)
    : base(other)
  {
  }

  public override SQLExpression Duplicate()
  {
    return (SQLExpression) new SQLSmartConvert((SQLConvert) this);
  }

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
