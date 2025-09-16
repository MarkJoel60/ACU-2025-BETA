// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.BQL.IsPOLinked`2
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
namespace PX.Objects.AP.BQL;

/// <summary>
/// A predicate that returns <c>true</c> if and only if the
/// <see cref="T:PX.Objects.AP.APRegister" /> descendant record (as determined
/// by the pair of document type and reference number fields)
/// has any lines linked to a PO bill.
/// </summary>
public class IsPOLinked<TDocTypeField, TRefNbrField> : IBqlUnary, IBqlCreator, IBqlVerifier
  where TDocTypeField : IBqlField
  where TRefNbrField : IBqlField
{
  private readonly IBqlCreator exists = (IBqlCreator) new Exists<Select<APTran, Where<APTran.tranType, Equal<TDocTypeField>, And<APTran.refNbr, Equal<TRefNbrField>, And<Where<APTran.pONbr, IsNotNull, Or<APTran.pOLineNbr, IsNotNull, Or<APTran.receiptNbr, IsNotNull, Or<APTran.receiptLineNbr, IsNotNull>>>>>>>>>();

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.exists.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    string str1 = cache.GetValue<TDocTypeField>(item) as string;
    string str2 = cache.GetValue<TRefNbrField>(item) as string;
    ref object local1 = ref value;
    ref bool? local2 = ref result;
    PXGraph graph = cache.Graph;
    object[] objArray = new object[2]
    {
      (object) str1,
      (object) str2
    };
    bool? nullable1;
    bool? nullable2 = nullable1 = new bool?(PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<Where<APTran.pONbr, IsNotNull, Or<APTran.pOLineNbr, IsNotNull, Or<APTran.receiptNbr, IsNotNull, Or<APTran.receiptLineNbr, IsNotNull>>>>>>>>.Config>.SelectWindowed(graph, 0, 1, objArray).RowCast<APTran>().Any<APTran>());
    local2 = nullable1;
    // ISSUE: variable of a boxed type
    __Boxed<bool?> local3 = (ValueType) nullable2;
    local1 = (object) local3;
  }

  public static bool Verify(PXGraph graph, APRegister document)
  {
    bool? result = new bool?();
    object obj = (object) null;
    new IsPOLinked<TDocTypeField, TRefNbrField>().Verify(graph.Caches[typeof (APRegister)], (object) document, new List<object>(), ref result, ref obj);
    return result.GetValueOrDefault();
  }
}
