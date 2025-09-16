// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DateMinusDaysNotLessThenDate`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

public class DateMinusDaysNotLessThenDate<V1, V2, V3> : IBqlCreator, IBqlVerifier, IBqlOperand
  where V1 : IBqlOperand
  where V2 : IBqlOperand
  where V3 : IBqlOperand
{
  private IBqlCreator _formula = (IBqlCreator) new Switch<Case<Where<Sub<V1, V2>, Less<V3>, Or<Sub<V1, V2>, IsNull>>, V3>, Sub<V1, V2>>();

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
}
