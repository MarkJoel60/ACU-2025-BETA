// Decompiled with JetBrains decompiler
// Type: PX.Data.ColumnNameComparer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

internal class ColumnNameComparer : IEqualityComparer<string>
{
  private readonly string _tableName;
  private readonly ISqlDialect _sqlDialect;
  private readonly IEqualityComparer<string> _ignoreCaseComparer = (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase;

  public ColumnNameComparer(string tableName, ISqlDialect sqlDialect)
  {
    this._tableName = tableName.ToLower();
    this._sqlDialect = sqlDialect;
  }

  public bool Equals(string x, string y)
  {
    return x == y || x != null && y != null && (this._ignoreCaseComparer.Equals(this._sqlDialect.quoteDbIdentifier(x), this._sqlDialect.quoteDbIdentifier(y)) || this._ignoreCaseComparer.Equals(x, this._sqlDialect.quoteTableAndColumn(this._tableName, y)) || this._ignoreCaseComparer.Equals(y, this._sqlDialect.quoteTableAndColumn(this._tableName, x)));
  }

  public int GetHashCode(string obj)
  {
    string lower = obj.ToLower();
    return lower.Contains(this._tableName) ? lower.GetHashCode() : this._sqlDialect.quoteTableAndColumn(this._tableName, lower).GetHashCode();
  }
}
