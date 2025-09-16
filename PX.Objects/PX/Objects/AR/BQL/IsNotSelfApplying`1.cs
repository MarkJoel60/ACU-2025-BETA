// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BQL.IsNotSelfApplying`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.BQL;

/// <summary>
/// A predicate which returns <c>true</c> whenever the value of its operand field
/// does not correspond to a self-applying document, which has no balance, e.g. a
/// <see cref="T:PX.Objects.AR.ARDocType.cashSale">Cash Sale</see> or a
/// <see cref="T:PX.Objects.AR.ARDocType.cashReturn">Cash Return</see>.
/// </summary>
public class IsNotSelfApplying<TDocTypeField> : IBqlUnary, IBqlCreator, IBqlVerifier where TDocTypeField : IBqlOperand
{
  private IBqlCreator _where = (IBqlCreator) new Where<TDocTypeField, NotEqual<ARDocType.cashSale>, And<TDocTypeField, NotEqual<ARDocType.cashReturn>>>();

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this._where.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    ((IBqlVerifier) this._where).Verify(cache, item, pars, ref result, ref value);
  }
}
