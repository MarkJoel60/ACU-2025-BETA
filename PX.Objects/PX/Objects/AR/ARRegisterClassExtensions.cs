// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterClassExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public static class ARRegisterClassExtensions
{
  /// <summary>
  /// Returns an enumerable string array, which is included all
  /// possible original document types for voiding document.
  /// </summary>
  public static IEnumerable<string> PossibleOriginalDocumentTypes(this ARRegister voidpayment)
  {
    switch (voidpayment.DocType)
    {
      case "RCS":
        return (IEnumerable<string>) new string[1]{ "CSL" };
      case "VRF":
      case "RPM":
        return (IEnumerable<string>) ARPaymentType.GetVoidedARDocType(voidpayment.DocType);
      default:
        return (IEnumerable<string>) new string[1]
        {
          voidpayment.DocType
        };
    }
  }

  /// <summary>
  /// Indicates whether the record is an original Retainage document
  /// with <see cref="P:PX.Objects.AR.ARRegister.RetainageApply" /> flag equal to true.
  /// </summary>
  public static bool IsOriginalRetainageDocument(this ARRegister doc)
  {
    return doc.RetainageApply.GetValueOrDefault();
  }

  /// <summary>
  /// Indicates whether the record is a child Retainage document
  /// with <see cref="P:PX.Objects.AR.ARRegister.IsRetainageDocument" /> flag equal to true
  /// and existing reference on the original Retainage document.
  /// </summary>
  public static bool IsChildRetainageDocument(this ARRegister doc)
  {
    return doc.IsRetainageDocument.GetValueOrDefault() && !string.IsNullOrEmpty(doc.OrigDocType) && !string.IsNullOrEmpty(doc.OrigRefNbr);
  }

  /// <summary>
  /// Indicates whether the record is an Prepayment Invoice document.
  /// </summary>
  public static bool IsPrepaymentInvoiceDocument(this ARRegister doc) => doc.DocType == "PPI";

  /// <summary>
  /// Indicates whether the record is a reverse Prepayment Invoice document
  /// by Credit Memo.
  /// </summary>
  public static bool IsPrepaymentInvoiceDocumentReverse(this ARRegister doc)
  {
    return doc.DocType == "CRM" && doc.OrigDocType == "PPI";
  }

  /// <summary>
  /// Indicates whether the record has zero document balance and zero lines balances
  /// depending on <see cref="P:PX.Objects.AR.ARRegister.PaymentsByLinesAllowed" /> flag value.
  /// </summary>
  public static bool HasZeroBalance<TDocBal, TLineBal>(this ARRegister document, PXGraph graph)
    where TDocBal : IBqlField
    where TLineBal : IBqlField
  {
    if ((graph.Caches[typeof (ARRegister)].GetValue<TDocBal>((object) document) as Decimal?).GetValueOrDefault() != 0M)
      return false;
    if (document.PaymentsByLinesAllowed.GetValueOrDefault())
    {
      PXCache cach = graph.Caches[typeof (ARTran)];
      foreach (PXResult<ARTran> pxResult in PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>>>>.Config>.Select(graph, new object[2]
      {
        (object) document.DocType,
        (object) document.RefNbr
      }))
      {
        ARTran arTran = PXResult<ARTran>.op_Implicit(pxResult);
        if ((cach.GetValue<TLineBal>((object) arTran) as Decimal?).GetValueOrDefault() != 0M)
          return false;
      }
    }
    return true;
  }
}
