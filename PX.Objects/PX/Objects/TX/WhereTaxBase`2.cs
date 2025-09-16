// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.WhereTaxBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.TX;

public class WhereTaxBase<TaxID, TaxFlag> : IBqlWhere, IBqlUnary, IBqlCreator, IBqlVerifier
  where TaxID : IBqlOperand
  where TaxFlag : IBqlField
{
  private readonly IBqlCreator _where = (IBqlCreator) new Where<Selector<TaxID, TaxFlag>, Equal<True>, And<Selector<TaxID, Tax.statisticalTax>, Equal<False>, And<Selector<TaxID, Tax.reverseTax>, Equal<False>, And<Selector<TaxID, Tax.taxType>, Equal<CSTaxType.vat>>>>>();

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
