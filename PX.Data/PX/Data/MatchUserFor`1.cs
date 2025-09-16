// Decompiled with JetBrains decompiler
// Type: PX.Data.MatchUserFor`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Matches only the data records the current user has access rights for.
/// The condition is applied to the data records of the table set with <tt>Table</tt>.
/// </summary>
/// <typeparam name="Table">The DAC whose data records are cheched by the condition.</typeparam>
public sealed class MatchUserFor<Table> : 
  BqlChainableConditionLite<MatchUserFor<Table>>,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where Table : IBqlTable
{
  private static readonly Lazy<IBqlUnary> Match = new Lazy<IBqlUnary>((Func<IBqlUnary>) (() => (IBqlUnary) new PX.Data.Match<Table, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>()));

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    MatchUserFor<Table>.Match.Value.Verify(cache, item, pars, ref result, ref value);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return MatchUserFor<Table>.Match.Value.AppendExpression(ref exp, graph, info, selection);
  }
}
