// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Table
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

public abstract class Table
{
  public string Alias;

  public virtual StringBuilder SQLAlias(Connection conn) => this.SQLAlias(conn.CreateSQLWriter());

  public virtual StringBuilder SQLTableName(Connection conn)
  {
    return this.SQLTableName(conn.CreateSQLWriter());
  }

  internal virtual StringBuilder SQLAlias(BaseSQLWriterVisitor visitor) => visitor.SQLAlias(this);

  internal virtual StringBuilder SQLTableName(BaseSQLWriterVisitor conn)
  {
    return new StringBuilder("<TABLE>");
  }

  public Table()
  {
  }

  internal Table(Table other) => this.Alias = other.Alias;

  public Table As(string alias)
  {
    this.Alias = alias;
    return this;
  }

  internal abstract Table Duplicate();

  internal virtual Table transformTableName(string from, string to) => this;

  internal virtual Table substituteExternalColumnAliases(
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    return this;
  }

  internal virtual Table substituteColumnAliases(Dictionary<string, string> dict) => this;

  internal virtual Table addExternalAliasToTableNames(
    string alias,
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    return this;
  }

  public StringBuilder SQLQuery(Connection conn)
  {
    return this.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) conn.CreateSQLWriter());
  }

  public override string ToString() => "<TABLE>";

  [PXInternalUseOnly]
  public virtual void GetExpressionsOfType(
    Predicate<SQLExpression> predicate,
    List<SQLExpression> result)
  {
  }

  /// <summary>
  /// Verifies if the item meets the SQL Tree and provided parameters
  /// </summary>
  /// <param name="cache">Cache the item is contained in</param>
  /// <param name="item">Data item to verify</param>
  /// <param name="parameters">Query parameters</param>
  /// <returns>True if the item meets or it is impossible to determine</returns>
  internal bool Meet(PXCache cache, object item, object[] parameters)
  {
    return this.Accept<bool>((ISQLQueryVisitor<bool>) new ValidateVisitor(cache, item, parameters));
  }

  internal virtual T Accept<T>(ISQLQueryVisitor<T> visitor) => visitor.Visit(this);

  public virtual string AliasOrName() => this.Alias;
}
