// Decompiled with JetBrains decompiler
// Type: PX.Data.Database.Common.SqlDialectExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Database.Common;

public static class SqlDialectExtensions
{
  public static bool IsValueGetDate(this ISqlDialect _sqlDialect, object value)
  {
    return string.Equals(value as string, _sqlDialect.GetDate, StringComparison.OrdinalIgnoreCase);
  }

  public static bool IsValueGetUtcDate(this ISqlDialect _sqlDialect, object value)
  {
    return string.Equals(value as string, _sqlDialect.GetUtcDate, StringComparison.OrdinalIgnoreCase);
  }

  public static bool IsValueGetDateOrGetUtcDate(this ISqlDialect _sqlDialect, object value)
  {
    return _sqlDialect.IsValueGetDate(value) || _sqlDialect.IsValueGetUtcDate(value);
  }
}
