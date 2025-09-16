// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ValidateColumnAliasesVisitor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.SQLTree;

internal class ValidateColumnAliasesVisitor : SQLTreeTraversalVisitor<bool>
{
  private HashSet<string> _validTables;

  public ValidateColumnAliasesVisitor(IEnumerable<string> validTables)
  {
    this._validTables = new HashSet<string>(validTables, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  protected override bool DefaultResult => true;

  protected override bool CombineResult(bool a, bool b) => a & b;

  public override bool Visit(Column exp)
  {
    if (exp.Table() == null || this._validTables.Contains(exp.Table().AliasOrName()))
      return true;
    this.Break = true;
    return false;
  }

  public override bool Visit(Query table)
  {
    HashSet<string> validTables = this._validTables;
    this._validTables = new HashSet<string>((IEnumerable<string>) this._validTables, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (Joiner joiner in table.GetFrom())
    {
      string str = joiner?.Table()?.AliasOrName();
      if (!string.IsNullOrEmpty(str))
        this._validTables.Add(str);
    }
    int num = base.Visit(table) ? 1 : 0;
    this._validTables = validTables;
    return num != 0;
  }
}
