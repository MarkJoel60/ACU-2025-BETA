// Decompiled with JetBrains decompiler
// Type: PX.Data.Database.MsSql.MsSqlConstraintStatementErrorMessageHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Database.MsSql;

/// <summary>
/// Helper for parse errors with template
/// "The %ls statement conflicted with the %ls constraint \"%.*ls\". The conflict occurred in database \"%.*ls\", table \"%.*ls\"%ls%.*ls%ls."
/// or localized
/// </summary>
/// <remarks>
/// Patterns "%6!%7!%8 optional and may be not present. Pattern %0 is error message itself
/// </remarks>
internal static class MsSqlConstraintStatementErrorMessageHelper
{
  public const int ErrorCode = 547;
  public const int EnglishLanguageId = 1033;

  public static ConstraintStatementSqlError AsConstraintStatementSqlError(
    this ISqlErrorMessageParser parser,
    SqlException exception,
    int languageId)
  {
    if (exception == null || parser == null)
      return (ConstraintStatementSqlError) null;
    if (exception.Number != 547)
      return (ConstraintStatementSqlError) null;
    string template;
    if (!parser.TryGetMessageTemplate(exception.Number, languageId, out template))
      return (ConstraintStatementSqlError) null;
    List<string> messagePatterns;
    return !parser.TryParse(MsSqlConstraintStatementErrorMessageHelper.GetActualErrorMessage(exception), template, out messagePatterns) ? (ConstraintStatementSqlError) null : new ConstraintStatementSqlError(messagePatterns, (Func<int, int>) (patternNumber => languageId != 1033 ? parser.TryGetPatternIndex(template, patternNumber) : patternNumber));
  }

  private static string GetActualErrorMessage(SqlException exception)
  {
    if (exception.Errors.Count <= 1)
      return ((Exception) exception).Message;
    SqlError sqlError = ((IEnumerable) exception.Errors).OfType<SqlError>().FirstOrDefault<SqlError>((Func<SqlError, bool>) (e => e.Number == 547));
    return sqlError == null ? ((Exception) exception).Message : sqlError.Message;
  }
}
