// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLExpressionExt
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.SQLTree;

public static class SQLExpressionExt
{
  public static PXDbType GetDBType(this TypeCode tc)
  {
    switch (tc)
    {
      case TypeCode.Boolean:
        return PXDbType.Bit;
      case TypeCode.Byte:
        return PXDbType.TinyInt;
      case TypeCode.Int16:
        return PXDbType.SmallInt;
      case TypeCode.UInt16:
        return PXDbType.SmallInt;
      case TypeCode.Int32:
        return PXDbType.Int;
      case TypeCode.UInt32:
        return PXDbType.Int;
      case TypeCode.Int64:
        return PXDbType.BigInt;
      case TypeCode.UInt64:
        return PXDbType.BigInt;
      case TypeCode.Single:
        return PXDbType.Real;
      case TypeCode.Double:
        return PXDbType.Float;
      case TypeCode.Decimal:
        return PXDbType.Decimal;
      case TypeCode.DateTime:
        return PXDbType.DateTime;
      default:
        return PXDbType.Unspecified;
    }
  }

  public static SQLExpression EQ(this SQLExpression l, SQLExpression r) => SQLExpression.EQ(l, r);

  public static SQLExpression EQ(this SQLExpression l, object r)
  {
    return SQLExpression.EQ(l, (SQLExpression) new SQLConst(r));
  }

  public static SQLExpression EQ(this System.Type l, SQLExpression r)
  {
    return SQLExpression.EQ((SQLExpression) new PX.Data.SQLTree.Column(l), r);
  }

  public static SQLExpression EQ(this System.Type l, System.Type r)
  {
    return SQLExpression.EQ((SQLExpression) new PX.Data.SQLTree.Column(l), (SQLExpression) new PX.Data.SQLTree.Column(r));
  }

  public static SQLExpression EQ(this System.Type l, object r)
  {
    return SQLExpression.EQ((SQLExpression) new PX.Data.SQLTree.Column(l), (SQLExpression) new SQLConst(r));
  }

  public static SQLExpression NE(this SQLExpression l, SQLExpression r) => SQLExpression.NE(l, r);

  public static SQLExpression NE(this SQLExpression l, object r)
  {
    return SQLExpression.NE(l, (SQLExpression) new SQLConst(r));
  }

  public static SQLExpression NE(this System.Type l, SQLExpression r)
  {
    return SQLExpression.NE((SQLExpression) new PX.Data.SQLTree.Column(l), r);
  }

  public static SQLExpression NE(this System.Type l, System.Type r)
  {
    return SQLExpression.NE((SQLExpression) new PX.Data.SQLTree.Column(l), (SQLExpression) new PX.Data.SQLTree.Column(r));
  }

  public static SQLExpression NE(this System.Type l, object r)
  {
    return SQLExpression.NE((SQLExpression) new PX.Data.SQLTree.Column(l), (SQLExpression) new SQLConst(r));
  }

  public static SQLExpression Concat(this SQLExpression left, SQLExpression right)
  {
    return SQLExpression.ConcatSequence(((left == null || left.Oper() != SQLExpression.Operation.CONCAT ? left : left.RExpr()) ?? SQLExpression.None()).Seq((right == null || right.Oper() != SQLExpression.Operation.CONCAT ? right : right.RExpr()) ?? SQLExpression.None()));
  }

  public static SQLExpression In(this System.Type l, Query r) => new PX.Data.SQLTree.Column(l).In(r);

  public static List<SQLExpression> DecomposeSequence(this SQLExpression seq)
  {
    List<SQLExpression> sqlExpressionList = new List<SQLExpression>();
    Stack<SQLExpression> sqlExpressionStack = new Stack<SQLExpression>();
    sqlExpressionStack.Push(seq);
    while (sqlExpressionStack.Count > 0)
    {
      SQLExpression sqlExpression = sqlExpressionStack.Pop();
      if (sqlExpression.Oper() == SQLExpression.Operation.SEQ)
      {
        if (sqlExpression.RExpr() != null)
          sqlExpressionStack.Push(sqlExpression.RExpr());
        if (sqlExpression.LExpr() != null)
          sqlExpressionStack.Push(sqlExpression.LExpr());
      }
      else
        sqlExpressionList.Add(sqlExpression);
    }
    return sqlExpressionList;
  }

  public static PX.Data.SQLTree.Column Column(this System.Type field) => new PX.Data.SQLTree.Column(field);

  public static PXDbType GetDBTypeOrDefault(this SQLExpression exp)
  {
    return exp == null ? PXDbType.Unspecified : exp.GetDBType();
  }
}
