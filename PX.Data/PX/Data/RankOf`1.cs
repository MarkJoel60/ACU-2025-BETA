// Decompiled with JetBrains decompiler
// Type: PX.Data.RankOf`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class RankOf<Field> : IBqlCreator, IBqlVerifier, IBqlOperand where Field : IBqlField
{
  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (graph == null || !info.BuildExpression || BqlUnaryFullTextExtensions.getFullTextSupportMode(typeof (Field), PXDatabase.Provider, graph, info.Tables, selection) == BqlFullTextRenderingMethod.NeutralLike)
      return true;
    string name1 = typeof (Field).Name;
    string name2 = char.ToUpper(name1[0]).ToString() + name1.Substring(1);
    string name3 = BqlCommand.FindRealTableForType(info.Tables, BqlCommand.GetItemType(typeof (Field))).Name;
    SQLRank sqlRank = new SQLRank();
    exp = (SQLExpression) sqlRank;
    sqlRank.SetField((SQLExpression) new Column(name2, (Table) new SimpleTable(name3)));
    return true;
  }
}
