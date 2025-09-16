// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.HavingConditionWrapper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BQL;

public sealed class HavingConditionWrapper<THavingCondition> : IBqlUnary, IBqlCreator, IBqlVerifier where THavingCondition : IBqlHavingCondition, new()
{
  private readonly THavingCondition _having = new THavingCondition();

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this._having.MatchingCondition.Verify(cache, item, pars, ref result, ref value);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this._having.MatchingCondition.AppendExpression(ref exp, graph, info, selection);
  }
}
