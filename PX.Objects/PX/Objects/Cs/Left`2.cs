// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Left`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS;

public class Left<V1, V2> : IBqlFunction, IBqlCreator, IBqlVerifier, IBqlOperand
  where V1 : IBqlOperand
  where V2 : IBqlOperand
{
  private IBqlCreator _formula = (IBqlCreator) new Substring<V1, int1, V2>();

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this._formula.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    ((IBqlVerifier) this._formula).Verify(cache, item, pars, ref result, ref value);
  }

  public void GetAggregates(List<IBqlFunction> fields) => throw new NotImplementedException();

  public Type GetField()
  {
    return !typeof (IBqlCreator).IsAssignableFrom(typeof (V1)) ? typeof (V1) : (Type) null;
  }

  public bool IsGroupBy() => false;

  public string GetFunction() => throw new NotImplementedException();
}
