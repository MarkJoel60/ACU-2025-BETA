// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.BQL.HasUnreleasedVoidPayment`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.AP.Standalone;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.BQL;

/// <summary>
/// A predicate that returns <c>true</c> if and only if the payment defined
/// by its key fields (document type and reference number) has an unreleased
/// void payment. This may be needed to exclude such payments from processing
/// to prevent creating unnecessary applications.
/// </summary>
public class HasUnreleasedVoidPayment<TDocTypeField, TRefNbrField> : 
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TDocTypeField : IBqlField
  where TRefNbrField : IBqlField
{
  private readonly IBqlCreator exists = (IBqlCreator) new Exists<PX.Data.Select<APRegisterAlias2, Where<APRegisterAlias2.docType, Equal<Switch<Case<Where<TDocTypeField, Equal<APDocType.refund>>, APDocType.voidRefund>, APDocType.voidCheck>>, And<APRegisterAlias2.docType, NotEqual<TDocTypeField>, And<APRegisterAlias2.refNbr, Equal<TRefNbrField>, And<APRegisterAlias2.released, NotEqual<True>>>>>>>();

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
    string docType = cache.GetValue<TDocTypeField>(item) as string;
    string refNbr = cache.GetValue<TRefNbrField>(item) as string;
    value = (object) (result = new bool?(HasUnreleasedVoidPayment<TDocTypeField, TRefNbrField>.Select(cache.Graph, APPaymentType.GetVoidingAPDocType(docType), docType, refNbr) != null));
  }

  public static bool Verify(PXGraph graph, PX.Objects.AP.APRegister payment)
  {
    bool? result = new bool?();
    object obj = (object) null;
    new HasUnreleasedVoidPayment<PX.Objects.AP.APRegister.docType, PX.Objects.AP.APRegister.refNbr>().Verify(graph.Caches[payment.GetType()], (object) payment, new List<object>(0), ref result, ref obj);
    return result.GetValueOrDefault();
  }

  public static PX.Objects.AP.APRegister Select(PXGraph graph, PX.Objects.AP.APRegister payment)
  {
    return payment == null || payment.RefNbr == null || payment.DocType == null ? (PX.Objects.AP.APRegister) null : HasUnreleasedVoidPayment<TDocTypeField, TRefNbrField>.Select(graph, APPaymentType.GetVoidingAPDocType(payment.DocType), payment.DocType, payment.RefNbr);
  }

  [Obsolete("The method is obsolete. It will be removed in 2019R1. Please use Select(PXGraph , string , string , string ) instead.")]
  public static PX.Objects.AP.APRegister Select(PXGraph graph, string docType, string refNbr)
  {
    return HasUnreleasedVoidPayment<TDocTypeField, TRefNbrField>.Select(graph, "VCK", docType, refNbr);
  }

  public static PX.Objects.AP.APRegister Select(
    PXGraph graph,
    string voidDocType,
    string docType,
    string refNbr)
  {
    return (PX.Objects.AP.APRegister) PXSelectBase<APRegisterAlias2, PXSelect<APRegisterAlias2, Where<APRegisterAlias2.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<APRegisterAlias2.docType, NotEqual<Required<PX.Objects.AP.APRegister.docType>>, And<APRegisterAlias2.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>, And<APRegisterAlias2.released, NotEqual<True>>>>>>.Config>.SelectWindowed(graph, 0, 1, (object) voidDocType, (object) docType, (object) refNbr).RowCast<APRegisterAlias2>().FirstOrDefault<APRegisterAlias2>();
  }
}
