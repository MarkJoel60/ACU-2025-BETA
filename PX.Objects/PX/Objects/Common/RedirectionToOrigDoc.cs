// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.RedirectionToOrigDoc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// A helper class that manages redirection to the original document in AR, AP, GL and CA.
/// </summary>
public static class RedirectionToOrigDoc
{
  /// <summary>
  /// Tries to find and redirect to the original document using given original document type, refNbr and module.
  /// </summary>
  /// <param name="origDocType">Type of the original document.</param>
  /// <param name="origRefNbr">The original document reference number.</param>
  /// <param name="origModule">The original document module.</param>
  public static void TryRedirect(
    string origDocType,
    string origRefNbr,
    string origModule,
    bool preferPrimaryDocForm = false)
  {
    if (string.IsNullOrWhiteSpace(origRefNbr) || string.IsNullOrWhiteSpace(origModule))
      return;
    PXGraph pxGraph = RedirectionToOrigDoc.PrepareDestinationGraph(origDocType, origRefNbr, origModule, preferPrimaryDocForm);
    if (pxGraph == null)
      return;
    PXRedirectHelper.TryRedirect(pxGraph, (PXRedirectHelper.WindowMode) 3);
  }

  private static PXGraph PrepareDestinationGraph(
    string origDocType,
    string origRefNbr,
    string origModule,
    bool preferPrimaryDocForm)
  {
    if (origDocType == "GLE")
      return RedirectionToOrigDoc.PrepareDestinationGraphForGL(origModule, origRefNbr);
    switch (origModule)
    {
      case "AP":
        return RedirectionToOrigDoc.PrepareDestinationGraphForAP(origDocType, origRefNbr, preferPrimaryDocForm);
      case "AR":
        return RedirectionToOrigDoc.PrepareDestinationGraphForAR(origDocType, origRefNbr, preferPrimaryDocForm);
      case "CA":
        return RedirectionToOrigDoc.PrepareDestinationGraphForCA(origDocType, origRefNbr);
      case "EP":
        return RedirectionToOrigDoc.PrepareDestinationGraphForEP(origDocType, origRefNbr);
      case "SO":
        return RedirectionToOrigDoc.MakeDestinationGraph<SOInvoiceEntry, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>(origDocType, origRefNbr);
      default:
        return (PXGraph) null;
    }
  }

  private static PXGraph PrepareDestinationGraphForGL(string origModule, string origRefNbr)
  {
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    ((PXSelectBase<PX.Objects.GL.Batch>) instance.BatchModule).Current = PXResultset<PX.Objects.GL.Batch>.op_Implicit(PXSelectBase<PX.Objects.GL.Batch, PXSelect<PX.Objects.GL.Batch, Where<PX.Objects.GL.Batch.module, Equal<Required<PX.Objects.GL.Batch.module>>, And<PX.Objects.GL.Batch.batchNbr, Equal<Required<PX.Objects.GL.Batch.batchNbr>>>>>.Config>.Select((PXGraph) instance, new object[2]
    {
      (object) origModule,
      (object) origRefNbr
    }));
    return (PXGraph) instance;
  }

  private static PXGraph PrepareDestinationGraphForEP(string docType, string origRefNbr)
  {
    if (!string.IsNullOrEmpty(docType))
    {
      switch (docType)
      {
        case "ECL":
          break;
        case "ECD":
          ExpenseClaimDetailEntry instance1 = PXGraph.CreateInstance<ExpenseClaimDetailEntry>();
          ((PXSelectBase<EPExpenseClaimDetails>) instance1.ClaimDetails).Current = PXResultset<EPExpenseClaimDetails>.op_Implicit(PXSelectBase<EPExpenseClaimDetails, PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.claimDetailCD, Equal<Required<EPExpenseClaimDetails.claimDetailCD>>>>.Config>.Select((PXGraph) instance1, new object[1]
          {
            (object) origRefNbr
          }));
          return (PXGraph) instance1;
        default:
          return (PXGraph) null;
      }
    }
    ExpenseClaimEntry instance2 = PXGraph.CreateInstance<ExpenseClaimEntry>();
    ((PXSelectBase<EPExpenseClaim>) instance2.ExpenseClaim).Current = PXResultset<EPExpenseClaim>.op_Implicit(PXSelectBase<EPExpenseClaim, PXSelect<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Required<EPExpenseClaim.refNbr>>>>.Config>.Select((PXGraph) instance2, new object[1]
    {
      (object) origRefNbr
    }));
    return (PXGraph) instance2;
  }

  private static PXGraph PrepareDestinationGraphForAP(
    string origDocType,
    string origRefNbr,
    bool preferPrimaryDocForm)
  {
    if (origDocType == "INV" || origDocType == "PPI" || origDocType == "ACR" || origDocType == "ADR" & preferPrimaryDocForm)
      return RedirectionToOrigDoc.MakeDestinationGraph<APInvoiceEntry, PX.Objects.AP.APInvoice, PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>(origDocType, origRefNbr);
    if (origDocType != null && origDocType.Length == 3)
    {
      switch (origDocType[1])
      {
        case 'B':
          if (origDocType == "CBT")
          {
            CABatchEntry instance = PXGraph.CreateInstance<CABatchEntry>();
            ((PXSelectBase<CABatch>) instance.Document).Current = PXResultset<CABatch>.op_Implicit(PXSelectBase<CABatch, PXSelect<CABatch, Where<CABatch.batchNbr, Equal<Required<CATran.origRefNbr>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) origRefNbr
            }));
            return (PXGraph) instance;
          }
          goto label_14;
        case 'C':
          switch (origDocType)
          {
            case "QCK":
              break;
            case "VCK":
              goto label_13;
            default:
              goto label_14;
          }
          break;
        case 'D':
          if (origDocType == "ADR")
            goto label_13;
          goto label_14;
        case 'E':
          if (origDocType == "REF")
            goto label_13;
          goto label_14;
        case 'H':
          if (origDocType == "CHK")
            goto label_13;
          goto label_14;
        case 'P':
          if (origDocType == "PPM")
            goto label_13;
          goto label_14;
        case 'Q':
          if (origDocType == "VQC" || origDocType == "RQC")
            break;
          goto label_14;
        default:
          goto label_14;
      }
      return RedirectionToOrigDoc.MakeDestinationGraph<APQuickCheckEntry, APQuickCheck, APQuickCheck.docType, APQuickCheck.refNbr>(origDocType, origRefNbr);
label_13:
      return RedirectionToOrigDoc.MakeDestinationGraph<APPaymentEntry, PX.Objects.AP.APPayment, PX.Objects.AP.APPayment.docType, PX.Objects.AP.APPayment.refNbr>(origDocType, origRefNbr);
    }
label_14:
    return (PXGraph) null;
  }

  private static PXGraph PrepareDestinationGraphForAR(
    string origDocType,
    string origRefNbr,
    bool preferPrimaryDocForm)
  {
    if (origDocType == "INV" || origDocType == "PPI" || origDocType == "DRM" || origDocType == "CRM" & preferPrimaryDocForm)
      return RedirectionToOrigDoc.MakeDestinationGraph<ARInvoiceEntry, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>(origDocType, origRefNbr);
    if (origDocType != null && origDocType.Length == 3)
    {
      switch (origDocType[2])
      {
        case 'B':
          if (origDocType == "SMB")
            goto label_16;
          goto label_17;
        case 'F':
          if (origDocType == "REF" || origDocType == "VRF")
            goto label_16;
          goto label_17;
        case 'L':
          if (origDocType == "CSL")
            goto label_12;
          goto label_17;
        case 'M':
          switch (origDocType)
          {
            case "DRM":
              break;
            case "CRM":
            case "PPM":
            case "RPM":
              goto label_16;
            default:
              goto label_17;
          }
          break;
        case 'S':
          if (origDocType == "RCS")
            goto label_12;
          goto label_17;
        case 'T':
          if (origDocType == "PMT")
            goto label_16;
          goto label_17;
        case 'V':
          if (origDocType == "INV")
            break;
          goto label_17;
        default:
          goto label_17;
      }
      return RedirectionToOrigDoc.MakeDestinationGraph<ARInvoiceEntry, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>(origDocType, origRefNbr);
label_12:
      ARCashSaleEntry instance = PXGraph.CreateInstance<ARCashSaleEntry>();
      ARCashSale originalDocument = RedirectionToOrigDoc.GetOriginalDocument<ARCashSale, ARCashSale.docType, ARCashSale.refNbr>((PXGraph) instance, origDocType, origRefNbr);
      if (originalDocument?.OrigModule == "SO")
      {
        bool? released = originalDocument.Released;
        bool flag = false;
        if (released.GetValueOrDefault() == flag & released.HasValue)
          return RedirectionToOrigDoc.MakeDestinationGraph<SOInvoiceEntry, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>(origDocType, origRefNbr);
      }
      ((PXSelectBase<ARCashSale>) instance.Document).Current = originalDocument;
      return (PXGraph) instance;
label_16:
      return RedirectionToOrigDoc.MakeDestinationGraph<ARPaymentEntry, PX.Objects.AR.ARPayment, PX.Objects.AR.ARPayment.docType, PX.Objects.AR.ARPayment.refNbr>(origDocType, origRefNbr);
    }
label_17:
    return (PXGraph) null;
  }

  private static PXGraph PrepareDestinationGraphForCA(string origDocType, string origRefNbr)
  {
    if (origDocType != null && origDocType.Length == 3)
    {
      switch (origDocType[2])
      {
        case 'D':
          if (origDocType == "CVD")
            goto label_9;
          goto label_11;
        case 'E':
          switch (origDocType)
          {
            case "CAE":
              break;
            case "CTE":
              goto label_10;
            default:
              goto label_11;
          }
          break;
        case 'G':
          switch (origDocType)
          {
            case "CAG":
              break;
            case "CTG":
              goto label_10;
            default:
              goto label_11;
          }
          break;
        case 'I':
          if (origDocType == "CTI")
            goto label_10;
          goto label_11;
        case 'O':
          if (origDocType == "CTO")
            goto label_10;
          goto label_11;
        case 'T':
          if (origDocType == "CDT")
            goto label_9;
          goto label_11;
        default:
          goto label_11;
      }
      CATranEntry instance1 = PXGraph.CreateInstance<CATranEntry>();
      ((PXSelectBase<CAAdj>) instance1.CAAdjRecords).Current = origDocType == "CTE" ? RedirectionToOrigDoc.GetOriginalDocument<CAAdj, CAAdj.adjTranType, CAAdj.transferNbr>((PXGraph) instance1, origDocType, origRefNbr) : RedirectionToOrigDoc.GetOriginalDocument<CAAdj, CAAdj.adjTranType, CAAdj.adjRefNbr>((PXGraph) instance1, origDocType, origRefNbr);
      return (PXGraph) instance1;
label_9:
      return RedirectionToOrigDoc.MakeDestinationGraph<CADepositEntry, CADeposit, CADeposit.tranType, CADeposit.refNbr>(origDocType, origRefNbr);
label_10:
      CashTransferEntry instance2 = PXGraph.CreateInstance<CashTransferEntry>();
      ((PXSelectBase<CATransfer>) instance2.Transfer).Current = PXResultset<CATransfer>.op_Implicit(PXSelectBase<CATransfer, PXSelect<CATransfer, Where<CATransfer.transferNbr, Equal<Required<CATransfer.transferNbr>>>>.Config>.Select((PXGraph) instance2, new object[1]
      {
        (object) origRefNbr
      }));
      return (PXGraph) instance2;
    }
label_11:
    return (PXGraph) null;
  }

  /// <summary>
  /// Makes the destination graph for most common redirection cases.
  /// </summary>
  /// <typeparam name="TGraph">The type of the destination graph.</typeparam>
  /// <typeparam name="TOrigDoc">The type of the original document.</typeparam>
  /// <typeparam name="TDocType">The type of the document type field.</typeparam>
  /// <typeparam name="TRefNbr">The type of the reference number field.</typeparam>
  /// <param name="origDocType">Type of the original document.</param>
  /// <param name="origRefNbr">The original document reference number.</param>
  /// <returns></returns>
  private static PXGraph MakeDestinationGraph<TGraph, TOrigDoc, TDocType, TRefNbr>(
    string origDocType,
    string origRefNbr)
    where TGraph : PXGraph, new()
    where TOrigDoc : class, IBqlTable, new()
    where TDocType : IBqlField
    where TRefNbr : IBqlField
  {
    TGraph instance = PXGraph.CreateInstance<TGraph>();
    TOrigDoc originalDocument = RedirectionToOrigDoc.GetOriginalDocument<TOrigDoc, TDocType, TRefNbr>((PXGraph) instance, origDocType, origRefNbr);
    Type type = originalDocument?.GetType();
    if ((object) type == null)
      type = typeof (TOrigDoc);
    instance.Caches[type].Current = (object) originalDocument;
    return (PXGraph) instance;
  }

  private static TOrigDoc GetOriginalDocument<TOrigDoc, TDocType, TRefNbr>(
    PXGraph origDocGraph,
    string origDocType,
    string origRefNbr)
    where TOrigDoc : class, IBqlTable, new()
    where TDocType : IBqlField
    where TRefNbr : IBqlField
  {
    return PXResultset<TOrigDoc>.op_Implicit(PXSelectBase<TOrigDoc, PXSelect<TOrigDoc, Where<TDocType, Equal<Required<TDocType>>, And<TRefNbr, Equal<Required<TRefNbr>>>>>.Config>.Select(origDocGraph, new object[2]
    {
      (object) origDocType,
      (object) origRefNbr
    }));
  }
}
