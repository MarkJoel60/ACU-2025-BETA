// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.IsPOLinkedAPBill
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public class IsPOLinkedAPBill : IBqlUnary, IBqlCreator, IBqlVerifier
{
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    ref object local1 = ref value;
    ref bool? local2 = ref result;
    PXGraph graph = cache.Graph;
    object[] currents = new object[1]{ item };
    object[] objArray = Array.Empty<object>();
    bool? nullable1;
    bool? nullable2 = nullable1 = new bool?(PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Current<APInvoice.docType>>, And<APTran.refNbr, Equal<Current<APInvoice.refNbr>>, And<Where<APTran.pOLineNbr, IsNotNull, Or<APTran.pONbr, IsNotNull, Or<APTran.receiptLineNbr, IsNotNull, Or<APTran.receiptNbr, IsNotNull>>>>>>>>.Config>.SelectSingleBound(graph, currents, objArray).AsEnumerable<PXResult<APTran>>().Any<PXResult<APTran>>());
    local2 = nullable1;
    // ISSUE: variable of a boxed type
    __Boxed<bool?> local3 = (ValueType) nullable2;
    local1 = (object) local3;
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return true;
  }

  public static bool Ensure(PXCache cache, APInvoice bill)
  {
    bool? result = new bool?();
    object obj = (object) null;
    new IsPOLinkedAPBill().Verify(cache, (object) bill, new List<object>(), ref result, ref obj);
    return result.GetValueOrDefault();
  }
}
