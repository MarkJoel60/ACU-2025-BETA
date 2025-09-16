// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlCommandInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class BqlCommandInfo
{
  /// Use it to pass this flag to <see cref="F:PX.Data.BqlCommand.Selection.UseColumnAliases" />
  internal bool UseColumnAliases;

  public BqlCommandInfo(bool initialize = true)
  {
    if (!initialize)
      return;
    this.Parameters = new List<IBqlParameter>();
    this.Tables = new List<System.Type>();
    this.Fields = new List<System.Type>();
    this.SortColumns = new List<IBqlSortColumn>();
  }

  public List<IBqlParameter> Parameters { get; set; }

  public List<System.Type> Tables { get; set; }

  public List<System.Type> Fields { get; set; }

  public List<IBqlSortColumn> SortColumns { get; set; }

  /// If false, AppendQuery() and AppendExpression() methods can skip expression building process - just collecting info
  public bool BuildExpression { get; set; } = true;

  public bool IsEmpty
  {
    get
    {
      return this.Parameters == null && this.Tables == null && this.Fields == null && this.SortColumns == null;
    }
  }

  internal bool OnlyExplicitSort { get; set; }
}
