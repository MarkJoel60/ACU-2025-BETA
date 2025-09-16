// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLExpressionExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

internal static class SQLExpressionExtensions
{
  internal static bool IsConcatable(this SQLExpression expression)
  {
    switch (expression)
    {
      case SQLConst sqlConst:
        return sqlConst.GetValue() is string;
      case SQLConcat _:
        return true;
      default:
        return SQLExpressionExtensions.IsConcatable(expression.GetDBType());
    }
  }

  private static bool IsConcatable(PXDbType dbtype)
  {
    switch (dbtype)
    {
      case PXDbType.Char:
      case PXDbType.NChar:
      case PXDbType.NText:
      case PXDbType.NVarChar:
      case PXDbType.Text:
      case PXDbType.VarChar:
        return true;
      default:
        return false;
    }
  }
}
