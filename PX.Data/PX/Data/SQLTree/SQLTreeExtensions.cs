// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLTreeExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.SQLTree;

internal static class SQLTreeExtensions
{
  private static bool TryGetReadArchivedRestrictionIfNeeded(
    companySetting setting,
    Table table,
    out SQLExpression expression,
    bool isRightJoin,
    bool isKvExt = false,
    bool showArchived = false)
  {
    expression = (SQLExpression) null;
    if (setting.RecordStatus == null | isKvExt)
      return false;
    SQLExpression r1 = (SQLExpression) new SQLConst((object) 0);
    Column l = new Column(setting.RecordStatus, table);
    if (!PXDatabase.ReadOnlyArchived)
      expression = !isRightJoin ? SQLExpressionExt.EQ(l, r1) : l.IsNull().Or(SQLExpressionExt.EQ(l, r1));
    SQLExpression r2 = (SQLExpression) new SQLConst((object) 1);
    if (showArchived || PXDatabase.ReadThroughArchived)
      expression = expression == null ? SQLExpressionExt.EQ(l, r2) : expression.Or(SQLExpressionExt.EQ(l, r2));
    expression = expression.Embrace();
    return true;
  }

  public static bool TryGetReadArchivedRestrictionIfNeeded(
    companySetting setting,
    string tableName,
    out SQLExpression expression,
    bool isRightJoin,
    bool isKvExt = false,
    bool showArchived = false)
  {
    return SQLTreeExtensions.TryGetReadArchivedRestrictionIfNeeded(setting, (Table) new SimpleTable(tableName), out expression, isRightJoin, isKvExt, showArchived);
  }

  public static SQLExpression AddReadArchivedRestrictionIfNeeded(
    this SQLExpression restrictions,
    companySetting setting,
    Table table,
    bool isRightJoin,
    bool isKvExt,
    bool showArchived = false)
  {
    SQLExpression expression;
    return SQLTreeExtensions.TryGetReadArchivedRestrictionIfNeeded(setting, table, out expression, isRightJoin, isKvExt, showArchived) ? restrictions.And(expression) : restrictions;
  }

  public static SQLExpression AddReadArchivedRestrictionIfNeeded(
    this SQLExpression restrictions,
    companySetting setting,
    string tableName,
    bool isRightJoin,
    bool isKvExt = false,
    bool showArchived = false)
  {
    return restrictions.AddReadArchivedRestrictionIfNeeded(setting, (Table) new SimpleTable(tableName), isRightJoin, isKvExt, showArchived);
  }

  public static SQLExpression AddCastToIfNeeded(
    this SQLExpression expression,
    System.Type type,
    int castToPrecision,
    int castToScale)
  {
    if (PXDatabase.Provider.SqlDialect.isStronglyTyped)
      expression = expression != null ? expression.AddCastTo(type, castToPrecision, castToScale) : (SQLExpression) null;
    return expression;
  }

  public static SQLExpression AddCastTo(
    this SQLExpression expression,
    System.Type type,
    int castToPrecision,
    int castToScale)
  {
    if (type == typeof (Decimal))
      expression = expression?.Embrace().CastAsDecimal(castToPrecision, castToScale);
    return expression;
  }

  internal static bool IsPadSpacedStringColumn(this SQLExpression expression)
  {
    return expression is Column column && column.PadSpaced && column.GetDBType().IsString();
  }

  internal static bool IsNotPadSpacedStringColumn(this SQLExpression expression)
  {
    return expression is Column column && !column.PadSpaced && column.GetDBType().IsString();
  }

  internal static bool IsConstLiteral(this SQLExpression expression)
  {
    return expression is Literal literal && literal.LExpr() is SQLConst;
  }

  internal static bool IsConstOrConstLiteral(this SQLExpression expression)
  {
    return expression is SQLConst || expression.IsConstLiteral();
  }

  internal static bool IsStringColumnLiteral(this SQLExpression expression)
  {
    return expression is Literal literal && literal.LExpr() is Column column && column.GetDBType().IsString();
  }

  internal static bool IsPadSpacedStringColumnLiteral(this SQLExpression expression)
  {
    return expression.IsStringColumnLiteral() && expression.LExpr().IsPadSpacedStringColumn();
  }

  internal static bool IsStringColumnLiteralCompare(this SQLExpression expression)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return expression.IsRightLeftCompare(SQLTreeExtensions.\u003C\u003EO.\u003C0\u003E__IsStringColumnLiteralCompare ?? (SQLTreeExtensions.\u003C\u003EO.\u003C0\u003E__IsStringColumnLiteralCompare = new Func<SQLExpression, SQLExpression, bool>(SQLTreeExtensions.IsStringColumnLiteralCompare)));
  }

  private static bool IsStringColumnLiteralCompare(SQLExpression left, SQLExpression right)
  {
    if (!(left is Column column) || !column.GetDBType().IsString())
      return false;
    return right.IsConstLiteral() || right.IsConstLiteralInConvert();
  }

  private static bool IsConstLiteralInConvert(this SQLExpression expression)
  {
    return expression is SQLConvert sqlConvert && sqlConvert.RExpr().IsConstLiteral();
  }

  internal static bool IsRightLeftCompare(
    this SQLExpression expression,
    Func<SQLExpression, SQLExpression, bool> check)
  {
    return check(expression.LExpr(), expression.RExpr()) || check(expression.RExpr(), expression.LExpr());
  }
}
