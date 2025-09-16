// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.BQL.SuggestRelatedItemsIsTrue`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using PX.Objects.AR;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.BQL;

public class SuggestRelatedItemsIsTrue<TranType, Released, CustomerID> : 
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TranType : IBqlOperand
  where Released : IBqlOperand
  where CustomerID : IBqlOperand
{
  private IBqlCreator formula = (IBqlCreator) new Where2<Where<TranType, Equal<ARDocType.invoice>, Or<TranType, Equal<ARDocType.cashSale>>>, And<Released, NotEqual<True>, And<Use<Selector<CustomerID, PX.Objects.AR.Customer.suggestRelatedItems>>.AsBool, Equal<True>>>>();

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.formula.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    ((IBqlVerifier) this.formula).Verify(cache, item, pars, ref result, ref value);
  }
}
