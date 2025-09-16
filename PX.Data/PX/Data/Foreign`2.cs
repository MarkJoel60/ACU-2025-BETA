// Decompiled with JetBrains decompiler
// Type: PX.Data.Foreign`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[Obsolete("Use Selector<,>")]
public sealed class Foreign<KeyField, ForeignOperand> : IBqlCreator, IBqlVerifier, IBqlOperand
  where KeyField : IBqlOperand
  where ForeignOperand : IBqlOperand
{
  private readonly IBqlCreator _formula = (IBqlCreator) new Selector<KeyField, ForeignOperand>();

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this._formula.Verify(cache, item, pars, ref result, ref value);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this._formula.AppendExpression(ref exp, graph, info, selection);
  }
}
