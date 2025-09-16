// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BQL.HasUnreleasedVoidPayment`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.AR.Standalone;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.BQL;

/// <summary>
/// A predicate that returns <c>true</c> if and only if the payment defined
/// by its key fields (document type and reference number) has an unreleased
/// void payment. This may be needed to exclude such payments from processing
/// to prevent creating unnecessary applications, see e.g. the
/// <see cref="T:PX.Objects.AR.ARAutoApplyPayments" /> graph.
/// </summary>
public class HasUnreleasedVoidPayment<TDocTypeField, TRefNbrField> : 
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TDocTypeField : IBqlOperand
  where TRefNbrField : IBqlOperand
{
  private readonly IBqlCreator exists = (IBqlCreator) new Exists<PX.Data.Select<ARRegisterAlias2, Where<ARRegisterAlias2.docType, Equal<Switch<Case<Where<TDocTypeField, Equal<ARDocType.refund>>, ARDocType.voidRefund>, ARDocType.voidPayment>>, And<ARRegisterAlias2.docType, NotEqual<TDocTypeField>, And<ARRegisterAlias2.refNbr, Equal<TRefNbrField>, And<ARRegisterAlias2.released, NotEqual<True>>>>>>>();

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.exists.AppendExpression(ref exp, graph, info, selection);
  }

  private string GetFieldName<T>()
  {
    if (typeof (IBqlField).IsAssignableFrom(typeof (T)))
      return typeof (T).Name;
    return typeof (IBqlParameter).IsAssignableFrom(typeof (T)) ? ((object) Activator.CreateInstance<T>() as IBqlParameter).GetReferencedType().Name : (string) null;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    string docType = cache.GetValue(item, this.GetFieldName<TDocTypeField>()) as string;
    string refNbr = cache.GetValue(item, this.GetFieldName<TRefNbrField>()) as string;
    value = (object) (result = new bool?(HasUnreleasedVoidPayment<TDocTypeField, TRefNbrField>.Select(cache.Graph, ARPaymentType.GetVoidingARDocType(docType), docType, refNbr) != null));
  }

  public static bool Verify(PXGraph graph, PX.Objects.AR.ARRegister payment)
  {
    bool? result = new bool?();
    object obj = (object) null;
    new HasUnreleasedVoidPayment<PX.Objects.AR.ARRegister.docType, PX.Objects.AR.ARRegister.refNbr>().Verify(graph.Caches[((object) payment).GetType()], (object) payment, new List<object>(0), ref result, ref obj);
    return result.GetValueOrDefault();
  }

  public static PX.Objects.AR.ARRegister Select(PXGraph graph, PX.Objects.AR.ARRegister payment)
  {
    return payment == null || payment.RefNbr == null || payment.DocType == null ? (PX.Objects.AR.ARRegister) null : HasUnreleasedVoidPayment<TDocTypeField, TRefNbrField>.Select(graph, ARPaymentType.GetVoidingARDocType(payment.DocType), payment.DocType, payment.RefNbr);
  }

  public static PX.Objects.AR.ARRegister Select(
    PXGraph graph,
    string voidDocType,
    string docType,
    string refNbr)
  {
    return (PX.Objects.AR.ARRegister) GraphHelper.RowCast<ARRegisterAlias2>((IEnumerable) PXSelectBase<ARRegisterAlias2, PXSelect<ARRegisterAlias2, Where<ARRegisterAlias2.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<ARRegisterAlias2.docType, NotEqual<Required<PX.Objects.AR.ARRegister.docType>>, And<ARRegisterAlias2.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>, And<ARRegisterAlias2.released, NotEqual<True>>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[3]
    {
      (object) voidDocType,
      (object) docType,
      (object) refNbr
    })).FirstOrDefault<ARRegisterAlias2>();
  }
}
