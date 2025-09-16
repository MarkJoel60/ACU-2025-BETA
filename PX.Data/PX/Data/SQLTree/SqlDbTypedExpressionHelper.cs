// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SqlDbTypedExpressionHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree;

internal static class SqlDbTypedExpressionHelper
{
  internal static PXDbType SetExpressionDbType(
    PXDbType leftType,
    PXDbType rightType,
    ISQLDBTypedExpression left,
    ISQLDBTypedExpression right)
  {
    if (leftType == PXDbType.Unspecified && rightType != PXDbType.Unspecified)
    {
      SqlDbTypedExpressionHelper.SetDbType(left, leftType, rightType);
      return rightType;
    }
    if (leftType != PXDbType.Unspecified && rightType == PXDbType.Unspecified)
    {
      SqlDbTypedExpressionHelper.SetDbType(right, rightType, leftType);
      return leftType;
    }
    if (left == null && right != null)
    {
      SqlDbTypedExpressionHelper.SetDbType(right, rightType, leftType);
      return leftType;
    }
    if (left != null && right == null)
    {
      SqlDbTypedExpressionHelper.SetDbType(left, leftType, rightType);
      return rightType;
    }
    if (leftType != PXDbType.Unspecified && !leftType.IsString() && rightType.IsString() && right is SQLConst sqlConst1 && sqlConst1.GetValue() is string str1 && string.IsNullOrEmpty(str1))
    {
      sqlConst1.SetDBType(leftType);
      return leftType;
    }
    if (rightType == PXDbType.Unspecified || rightType.IsString() || !leftType.IsString() || !(left is SQLConst sqlConst2) || !(sqlConst2.GetValue() is string str2) || !string.IsNullOrEmpty(str2))
      return PXDbType.Unspecified;
    sqlConst2.SetDBType(rightType);
    return rightType;
  }

  internal static PXDbType SetExpressionDbType(
    (PXDbType type, ISQLDBTypedExpression expression)[] expressions)
  {
    if (expressions == null || !((IEnumerable<(PXDbType, ISQLDBTypedExpression)>) expressions).Any<(PXDbType, ISQLDBTypedExpression)>())
      return PXDbType.Unspecified;
    (PXDbType, ISQLDBTypedExpression)[] array = EnumerableExtensions.Distinct<(PXDbType, ISQLDBTypedExpression), PXDbType>(((IEnumerable<(PXDbType, ISQLDBTypedExpression)>) expressions).Where<(PXDbType, ISQLDBTypedExpression)>((Func<(PXDbType, ISQLDBTypedExpression), bool>) (e => e.type != PXDbType.Unspecified)), (Func<(PXDbType, ISQLDBTypedExpression), PXDbType>) (d => d.type)).ToArray<(PXDbType, ISQLDBTypedExpression)>();
    if (!((IEnumerable<(PXDbType, ISQLDBTypedExpression)>) array).Any<(PXDbType, ISQLDBTypedExpression)>())
      return PXDbType.Unspecified;
    PXDbType maxByPrecedence = SQLExpression.GetMaxByPrecedence(((IEnumerable<(PXDbType, ISQLDBTypedExpression)>) array).Select<(PXDbType, ISQLDBTypedExpression), PXDbType?>((Func<(PXDbType, ISQLDBTypedExpression), PXDbType?>) (s => new PXDbType?(s.type))).ToArray<PXDbType?>());
    foreach ((PXDbType, ISQLDBTypedExpression) tuple in ((IEnumerable<(PXDbType, ISQLDBTypedExpression)>) expressions).Where<(PXDbType, ISQLDBTypedExpression)>((Func<(PXDbType, ISQLDBTypedExpression), bool>) (e => e.type == PXDbType.Unspecified)))
      SqlDbTypedExpressionHelper.SetDbType(tuple.Item2, tuple.Item1, maxByPrecedence);
    return maxByPrecedence;
  }

  private static void SetDbType(
    ISQLDBTypedExpression expression,
    PXDbType type,
    PXDbType precedenceType)
  {
    if (expression == null || type == precedenceType)
      return;
    bool flag = expression is ISQLConstantExpression constantExpression && constantExpression.IsLatin1();
    if ((!precedenceType.IsAsciiString() || flag || !precedenceType.TryToUnicodeType(out precedenceType)) && (!precedenceType.IsUnicodeString() || !type.IsAsciiString()) && (flag || !precedenceType.IsAsciiString() || type.IsUnicodeString()) && (precedenceType.IsString() || type != PXDbType.Unspecified))
      return;
    expression.SetDBType(precedenceType);
  }

  private static bool TryToUnicodeType(this PXDbType dbType, out PXDbType unicodeType)
  {
    unicodeType = dbType;
    switch (dbType)
    {
      case PXDbType.Char:
        unicodeType = PXDbType.NChar;
        return true;
      case PXDbType.Text:
        unicodeType = PXDbType.NText;
        return true;
      case PXDbType.VarChar:
        unicodeType = PXDbType.NVarChar;
        return true;
      default:
        return false;
    }
  }
}
