// Decompiled with JetBrains decompiler
// Type: PX.Data.Database.MsSql.ConstraintStatementSqlError
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Database.MsSql;

public class ConstraintStatementSqlError
{
  public ConstraintStatementSqlError()
  {
  }

  public ConstraintStatementSqlError(List<string> patterns, Func<int, int> getPatternIndex)
  {
    this.Statement = ConstraintStatementSqlError.GetPatternValue(patterns, getPatternIndex, 1);
    this.Constraint = ConstraintStatementSqlError.GetPatternValue(patterns, getPatternIndex, 2);
    this.Table = ConstraintStatementSqlError.GetPatternValue(patterns, getPatternIndex, 5);
  }

  private static string GetPatternValue(
    List<string> patterns,
    Func<int, int> getPatternIndex,
    int patternNumber)
  {
    int index = getPatternIndex(patternNumber);
    return patterns.Count <= index ? "" : patterns[index];
  }

  public bool IsDeleteStatement
  {
    get => this.Statement.Equals("DELETE", StringComparison.OrdinalIgnoreCase);
  }

  public bool IsInsertStatement
  {
    get => this.Statement.Equals("INSERT", StringComparison.OrdinalIgnoreCase);
  }

  public bool IsUpdateStatement
  {
    get => this.Statement.Equals("UPDATE", StringComparison.OrdinalIgnoreCase);
  }

  public bool HasFkConstraint
  {
    get => this.Constraint.Equals("FOREIGN KEY", StringComparison.OrdinalIgnoreCase);
  }

  public bool HasRefConstraint
  {
    get => this.Constraint.Equals("REFERENCE", StringComparison.OrdinalIgnoreCase);
  }

  public bool HasPkConstraint
  {
    get => this.Constraint.Equals("PRIMARY KEY", StringComparison.OrdinalIgnoreCase);
  }

  public string Statement { get; private set; } = "";

  public string Constraint { get; private set; } = "";

  public string Table { get; private set; } = "";
}
