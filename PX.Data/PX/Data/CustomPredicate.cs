// Decompiled with JetBrains decompiler
// Type: PX.Data.CustomPredicate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Allows to easily define a shortcut for a custom predicate.
/// Use it if you want to express one predicate through a combination of other ones.
/// </summary>
public abstract class CustomPredicate : IBqlUnary, IBqlCreator, IBqlVerifier, IBqlCustomPredicate
{
  protected internal virtual IBqlUnary Original { get; }

  protected CustomPredicate(IBqlUnary customPredicate) => this.Original = customPredicate;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this.Original.Verify(cache, item, pars, ref result, ref value);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.Original.AppendExpression(ref exp, graph, info, selection);
  }

  IBqlUnary IBqlCustomPredicate.Original => this.Original;
}
