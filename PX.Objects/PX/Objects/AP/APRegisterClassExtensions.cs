// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRegisterClassExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public static class APRegisterClassExtensions
{
  /// <summary>
  /// Returns an enumerable string array, which is included all
  /// possible original document types for voiding document.
  /// </summary>
  public static IEnumerable<string> PossibleOriginalDocumentTypes(this APRegister voidcheck)
  {
    string docType = voidcheck.DocType;
    if (docType == "VCK" || docType == "VRF" || docType == "VQC")
      return (IEnumerable<string>) APPaymentType.GetVoidedAPDocType(voidcheck.DocType);
    return (IEnumerable<string>) new string[1]
    {
      voidcheck.DocType
    };
  }

  /// <summary>
  /// Indicates whether the record is an original Retainage document
  /// with <see cref="P:PX.Objects.AP.APRegister.RetainageApply" /> flag equal to true
  /// </summary>
  public static bool IsOriginalRetainageDocument(this APRegister doc)
  {
    return doc.RetainageApply.GetValueOrDefault();
  }

  /// <summary>
  /// Indicates whether the record is a child Retainage document
  /// with <see cref="P:PX.Objects.AP.APRegister.IsRetainageDocument" /> flag equal to true
  /// and existing reference on the original Retainage document.
  /// </summary>
  public static bool IsChildRetainageDocument(this APRegister doc)
  {
    return doc.IsRetainageDocument.GetValueOrDefault() && !string.IsNullOrEmpty(doc.OrigDocType) && !string.IsNullOrEmpty(doc.OrigRefNbr);
  }

  /// <summary>
  /// Indicates whether the record is an Prepayment Invoice document
  /// </summary>
  public static bool IsPrepaymentInvoiceDocument(this APRegister doc) => doc.DocType == "PPI";

  /// <summary>
  /// Indicates whether the record is a reverse Prepayment Invoice document
  /// by Debit Memo.
  /// </summary>
  public static bool IsPrepaymentInvoiceDocumentReverse(this APRegister doc)
  {
    return doc.DocType == "ADR" && doc.OrigDocType == "PPI";
  }

  /// <summary>
  /// Indicates whether the record has zero document balance and zero lines balances
  /// depending on <see cref="P:PX.Objects.AP.APRegister.PaymentsByLinesAllowed" /> flag value.
  /// </summary>
  public static bool HasZeroBalance<TDocBal, TLineBal>(this APRegister document, PXGraph graph)
    where TDocBal : IBqlField
    where TLineBal : IBqlField
  {
    if (!((graph.Caches[typeof (APRegister)].GetValue<TDocBal>((object) document) as Decimal?).GetValueOrDefault() == 0M))
      return false;
    if (!document.PaymentsByLinesAllowed.GetValueOrDefault())
      return true;
    return !PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<TLineBal, NotEqual<decimal0>>>>>.Config>.Select(graph, (object) document.DocType, (object) document.RefNbr).AsEnumerable<PXResult<APTran>>().Any<PXResult<APTran>>();
  }
}
