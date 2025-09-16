// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.WhereAPPPDTaxable`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.TX;

public class WhereAPPPDTaxable<TaxID> : IBqlWhere, IBqlUnary, IBqlCreator, IBqlVerifier where TaxID : IBqlOperand
{
  private readonly IBqlCreator _where = (IBqlCreator) new Where<Selector<TaxID, Tax.includeInTaxable>, Equal<True>, And<Selector<TaxID, Tax.statisticalTax>, Equal<False>, And<Selector<TaxID, Tax.taxType>, Equal<CSTaxType.vat>>>>();

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return (1 & (this._where.AppendExpression(ref exp, graph, info, selection) ? 1 : 0)) != 0;
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
