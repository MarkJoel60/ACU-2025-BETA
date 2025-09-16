// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementProtoHelpers.PXInvoiceSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA.BankStatementHelpers;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.BankStatementProtoHelpers;

[Obsolete("Use PXInvoiceSelectorAttribute")]
public class PXInvoiceSelectorAttribute : PXCustomSelectorAttribute
{
  protected Type _BatchModule;

  public PXInvoiceSelectorAttribute(Type BatchModule)
    : base(typeof (GeneralInvoice.refNbr), new Type[14]
    {
      typeof (GeneralInvoice.refNbr),
      typeof (GeneralInvoice.docDate),
      typeof (GeneralInvoice.finPeriodID),
      typeof (GeneralInvoice.vendorBAccountID),
      typeof (GeneralInvoice.customerBAccountID),
      typeof (GeneralInvoice.locationID),
      typeof (GeneralInvoice.curyID),
      typeof (GeneralInvoice.curyOrigDocAmt),
      typeof (GeneralInvoice.curyDocBal),
      typeof (GeneralInvoice.dueDate),
      typeof (GeneralInvoice.branchID),
      typeof (GeneralInvoice.apExtRefNbr),
      typeof (GeneralInvoice.arExtRefNbr),
      typeof (GeneralInvoice.description)
    })
  {
    this._BatchModule = BatchModule;
  }

  protected virtual IEnumerable GetRecords()
  {
    PXInvoiceSelectorAttribute selectorAttribute = this;
    PXCache cach1 = selectorAttribute._Graph.Caches[BqlCommand.GetItemType(selectorAttribute._BatchModule)];
    PXCache cach2 = selectorAttribute._Graph.Caches[typeof (CABankTranAdjustment)];
    CABankTran current1 = (CABankTran) selectorAttribute._Graph.Caches[typeof (CABankTran)].Current;
    object obj = (object) null;
    foreach (object current2 in PXView.Currents)
    {
      if (current2 != null && (current2.GetType() == typeof (CABankTranAdjustment) || current2.GetType().IsSubclassOf(typeof (CABankTranAdjustment))))
      {
        obj = current2;
        break;
      }
    }
    if (obj == null)
      obj = cach2.Current;
    CABankTranAdjustment currentAdj = obj as CABankTranAdjustment;
    if (cach1.Current != null)
    {
      switch ((string) cach1.GetValue(cach1.Current, selectorAttribute._BatchModule.Name))
      {
        case "AP":
          foreach (PX.Objects.AP.APAdjust.APInvoice apInvoice in PXInvoiceSelectorAttribute.GetRecordsAP(currentAdj, current1, cach2, selectorAttribute._Graph))
            yield return (object) new GeneralInvoice()
            {
              RefNbr = apInvoice.RefNbr,
              BranchID = apInvoice.BranchID,
              OrigModule = apInvoice.OrigModule,
              DocType = apInvoice.DocType,
              DocDate = apInvoice.DocDate,
              FinPeriodID = apInvoice.FinPeriodID,
              BAccountID = apInvoice.VendorID,
              LocationID = apInvoice.VendorLocationID,
              CuryID = apInvoice.CuryID,
              CuryOrigDocAmt = apInvoice.CuryOrigDocAmt,
              CuryDocBal = apInvoice.CuryDocBal,
              Status = apInvoice.Status,
              DueDate = apInvoice.DueDate,
              Description = apInvoice.DocDesc,
              ExtRefNbr = apInvoice.InvoiceNbr
            };
          break;
        case "AR":
          foreach (ARAdjust.ARInvoice arInvoice in PXInvoiceSelectorAttribute.GetRecordsAR(currentAdj, current1, cach2, selectorAttribute._Graph))
            yield return (object) new GeneralInvoice()
            {
              RefNbr = arInvoice.RefNbr,
              BranchID = arInvoice.BranchID,
              OrigModule = arInvoice.OrigModule,
              DocType = arInvoice.DocType,
              DocDate = arInvoice.DocDate,
              FinPeriodID = arInvoice.FinPeriodID,
              BAccountID = arInvoice.CustomerID,
              LocationID = arInvoice.CustomerLocationID,
              CuryID = arInvoice.CuryID,
              CuryOrigDocAmt = arInvoice.CuryOrigDocAmt,
              CuryDocBal = arInvoice.CuryDocBal,
              Status = arInvoice.Status,
              DueDate = arInvoice.DueDate,
              Description = arInvoice.DocDesc,
              ExtRefNbr = arInvoice.InvoiceNbr
            };
          break;
      }
    }
  }

  public static IEnumerable<ARAdjust.ARInvoice> GetRecordsAR(
    CABankTranAdjustment currentAdj,
    CABankTran currentBankTran,
    PXCache adjustments,
    PXGraph graph)
  {
    foreach (ARAdjust.ARInvoice arInvoice in PXInvoiceSelectorAttribute.GetRecordsAR(currentAdj.AdjdDocType, currentAdj.TranID, currentAdj.AdjNbr, currentBankTran, adjustments, graph))
      yield return arInvoice;
  }

  public static IEnumerable<PX.Objects.AP.APAdjust.APInvoice> GetRecordsAP(
    CABankTranAdjustment currentAdj,
    CABankTran currentBankTran,
    PXCache adjustments,
    PXGraph graph)
  {
    foreach (PX.Objects.AP.APAdjust.APInvoice apInvoice in PXInvoiceSelectorAttribute.GetRecordsAP(currentAdj.AdjdDocType, currentAdj.TranID, currentAdj.AdjNbr, currentBankTran, adjustments, graph))
      yield return apInvoice;
  }

  public static IEnumerable<ARAdjust.ARInvoice> GetRecordsAR(
    string AdjdDocType,
    int? TranID,
    int? AdjNbr,
    CABankTran currentBankTran,
    PXCache adjustments,
    PXGraph graph)
  {
    PXSelectBase<ARAdjust.ARInvoice> pxSelectBase1 = (PXSelectBase<ARAdjust.ARInvoice>) new PXSelectJoin<ARAdjust.ARInvoice, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAR>, And<CABankTranMatch.docType, Equal<ARAdjust.ARInvoice.docType>, And<CABankTranMatch.docRefNbr, Equal<ARAdjust.ARInvoice.refNbr>, And<CABankTranMatch.tranID, NotEqual<Required<CABankTran.tranID>>>>>>, LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARAdjust.ARInvoice.docType>, And<ARAdjust.adjdRefNbr, Equal<ARAdjust.ARInvoice.refNbr>, And<ARAdjust.released, Equal<boolFalse>, And<ARAdjust.voided, Equal<boolFalse>, And<Where<ARAdjust.adjgDocType, NotEqual<Required<CABankTranAdjustment.adjdDocType>>>>>>>>, LeftJoin<CABankTranAdjustment, On<CABankTranAdjustment.adjdModule, Equal<BatchModule.moduleAR>, And<CABankTranAdjustment.adjdDocType, Equal<ARAdjust.ARInvoice.docType>, And<CABankTranAdjustment.adjdRefNbr, Equal<ARAdjust.ARInvoice.refNbr>, And<CABankTranAdjustment.released, Equal<boolFalse>, And<Where<CABankTranAdjustment.tranID, NotEqual<Required<CABankTranAdjustment.tranID>>, Or<Required<CABankTranAdjustment.adjNbr>, IsNull, Or<CABankTranAdjustment.adjNbr, NotEqual<Required<CABankTranAdjustment.adjNbr>>>>>>>>>>, LeftJoin<CABankTran, On<CABankTran.tranID, Equal<CABankTranAdjustment.tranID>>>>>>, Where<ARAdjust.ARInvoice.customerID, In2<Search<PX.Objects.AR.Override.BAccount.bAccountID, Where<PX.Objects.AR.Override.BAccount.bAccountID, Equal<Required<CABankTran.payeeBAccountID>>, Or<Where<PX.Objects.AR.Override.BAccount.consolidatingBAccountID, Equal<Required<CABankTran.payeeBAccountID>>, And<PX.Objects.AR.Override.BAccount.consolidateToParent, Equal<True>>>>>>>, And<ARAdjust.ARInvoice.docType, Equal<Required<CABankTranAdjustment.adjdDocType>>, And<ARAdjust.ARInvoice.released, Equal<boolTrue>, And<ARAdjust.ARInvoice.openDoc, Equal<boolTrue>, And<ARAdjust.adjgRefNbr, IsNull, And<ARAdjust.ARInvoice.pendingPPD, NotEqual<True>, And<Where<CABankTranAdjustment.adjdRefNbr, IsNull, Or<CABankTran.origModule, NotEqual<BatchModule.moduleAR>>>>>>>>>>>(graph);
    if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>())
      pxSelectBase1.WhereAnd<Where<PX.Objects.AR.ARInvoice.docType, NotEqual<ARDocType.prepaymentInvoice>, Or<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>, And<PX.Objects.AR.ARInvoice.pendingPayment, Equal<True>>>>>();
    PXSelectBase<ARAdjust.ARInvoice> pxSelectBase2 = pxSelectBase1;
    object[] objArray = new object[8]
    {
      (object) currentBankTran.TranID,
      (object) AdjdDocType,
      (object) TranID,
      (object) AdjNbr,
      (object) AdjNbr,
      (object) currentBankTran.PayeeBAccountID,
      (object) currentBankTran.PayeeBAccountID,
      (object) AdjdDocType
    };
    foreach (PXResult<ARAdjust.ARInvoice, CABankTranMatch> pxResult in pxSelectBase2.Select(objArray))
    {
      ARAdjust.ARInvoice invoice = PXResult<ARAdjust.ARInvoice, CABankTranMatch>.op_Implicit(pxResult);
      if (!PXInvoiceSelectorAttribute.ShouldSkipRecord(PXResult<ARAdjust.ARInvoice, CABankTranMatch>.op_Implicit(pxResult), (PX.Objects.CM.IRegister) invoice, adjustments, TranID, AdjNbr, graph, "AR"))
        yield return invoice;
    }
  }

  public static IEnumerable<PX.Objects.AP.APAdjust.APInvoice> GetRecordsAP(
    string AdjdDocType,
    int? TranID,
    int? AdjNbr,
    CABankTran currentBankTran,
    PXCache adjustments,
    PXGraph graph)
  {
    PXSelectBase<PX.Objects.AP.APAdjust.APInvoice> pxSelectBase1 = (PXSelectBase<PX.Objects.AP.APAdjust.APInvoice>) new PXSelectJoin<PX.Objects.AP.APAdjust.APInvoice, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>, And<CABankTranMatch.docType, Equal<PX.Objects.AP.APAdjust.APInvoice.docType>, And<CABankTranMatch.docRefNbr, Equal<PX.Objects.AP.APAdjust.APInvoice.refNbr>, And<CABankTranMatch.tranID, NotEqual<Required<CABankTran.tranID>>>>>>, LeftJoin<PX.Objects.AP.APAdjust, On<PX.Objects.AP.APAdjust.adjdDocType, Equal<PX.Objects.AP.APAdjust.APInvoice.docType>, And<PX.Objects.AP.APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APAdjust.APInvoice.refNbr>, And<PX.Objects.AP.APAdjust.released, Equal<boolFalse>>>>, LeftJoin<CABankTranAdjustment, On<CABankTranAdjustment.adjdModule, Equal<BatchModule.moduleAP>, And<CABankTranAdjustment.adjdDocType, Equal<PX.Objects.AP.APAdjust.APInvoice.docType>, And<CABankTranAdjustment.adjdRefNbr, Equal<PX.Objects.AP.APAdjust.APInvoice.refNbr>, And<CABankTranAdjustment.released, Equal<boolFalse>, And<Where<CABankTranAdjustment.tranID, NotEqual<Required<CABankTranAdjustment.tranID>>, Or<Required<CABankTranAdjustment.adjNbr>, IsNull, Or<CABankTranAdjustment.adjNbr, NotEqual<Required<CABankTranAdjustment.adjNbr>>>>>>>>>>, LeftJoin<PX.Objects.AP.Standalone.APPayment, On<PX.Objects.AP.Standalone.APPayment.docType, Equal<PX.Objects.AP.APAdjust.APInvoice.docType>, And<PX.Objects.AP.Standalone.APPayment.refNbr, Equal<PX.Objects.AP.APAdjust.APInvoice.refNbr>, And<Where<PX.Objects.AP.Standalone.APPayment.docType, Equal<APDocType.prepayment>, Or<PX.Objects.AP.Standalone.APPayment.docType, Equal<APDocType.debitAdj>>>>>>, LeftJoin<CABankTran, On<CABankTran.tranID, Equal<CABankTranAdjustment.tranID>>>>>>>, Where<PX.Objects.AP.APAdjust.APInvoice.vendorID, Equal<Optional<CABankTran.payeeBAccountID>>, And<PX.Objects.AP.APAdjust.APInvoice.docType, Equal<Optional<CABankTranAdjustment.adjdDocType>>, And2<Where<PX.Objects.AP.APAdjust.APInvoice.released, Equal<True>, Or<PX.Objects.AP.APRegister.prebooked, Equal<True>>>, And<PX.Objects.AP.APAdjust.APInvoice.openDoc, Equal<boolTrue>, And2<Where<CABankTranAdjustment.adjdRefNbr, IsNull, Or<CABankTran.origModule, NotEqual<BatchModule.moduleAP>>>, And<PX.Objects.AP.APAdjust.adjgRefNbr, IsNull, And2<Where<PX.Objects.AP.Standalone.APPayment.refNbr, IsNull, And<Required<CABankTran.docType>, NotEqual<APDocType.refund>, Or<PX.Objects.AP.Standalone.APPayment.refNbr, IsNotNull, And<Required<CABankTran.docType>, Equal<APDocType.refund>, Or<PX.Objects.AP.Standalone.APPayment.docType, Equal<APDocType.debitAdj>, And<Required<CABankTran.docType>, Equal<APDocType.check>, Or<PX.Objects.AP.Standalone.APPayment.docType, Equal<APDocType.debitAdj>, And<Required<CABankTran.docType>, Equal<APDocType.voidCheck>>>>>>>>>, And<Where<PX.Objects.AP.APAdjust.APInvoice.docDate, LessEqual<Required<CABankTran.matchingPaymentDate>>, And<PX.Objects.AP.APAdjust.APInvoice.finPeriodID, LessEqual<Required<CABankTran.matchingfinPeriodID>>, Or<Current<APSetup.earlyChecks>, Equal<True>, And<Required<CABankTran.docType>, NotEqual<APDocType.refund>>>>>>>>>>>>>>(graph);
    if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAP>())
      pxSelectBase1.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APInvoice.docType, NotEqual<APDocType.prepaymentInvoice>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APInvoice.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<PX.Objects.AP.APInvoice.pendingPayment, IBqlBool>.IsEqual<True>>>>>();
    PXSelectBase<PX.Objects.AP.APAdjust.APInvoice> pxSelectBase2 = pxSelectBase1;
    object[] objArray = new object[13]
    {
      (object) currentBankTran.TranID,
      (object) TranID,
      (object) AdjNbr,
      (object) AdjNbr,
      (object) currentBankTran.PayeeBAccountID,
      (object) AdjdDocType,
      (object) currentBankTran.DocType,
      (object) currentBankTran.DocType,
      (object) currentBankTran.DocType,
      (object) currentBankTran.DocType,
      (object) currentBankTran.MatchingPaymentDate,
      (object) currentBankTran.MatchingFinPeriodID,
      (object) currentBankTran.DocType
    };
    foreach (PXResult<PX.Objects.AP.APAdjust.APInvoice, CABankTranMatch> pxResult in pxSelectBase2.Select(objArray))
    {
      PX.Objects.AP.APAdjust.APInvoice invoice = PXResult<PX.Objects.AP.APAdjust.APInvoice, CABankTranMatch>.op_Implicit(pxResult);
      CABankTranMatch match = PXResult<PX.Objects.AP.APAdjust.APInvoice, CABankTranMatch>.op_Implicit(pxResult);
      if (!(PXAccess.GetBranch(invoice.BranchID).BaseCuryID != PXAccess.GetBranch(graph.Accessinfo.BranchID).BaseCuryID) && !PXInvoiceSelectorAttribute.ShouldSkipRecord(match, (PX.Objects.CM.IRegister) invoice, adjustments, TranID, AdjNbr, graph, "AP"))
        yield return invoice;
    }
  }

  protected static bool ShouldSkipRecord(
    CABankTranMatch match,
    PX.Objects.CM.IRegister invoice,
    PXCache adjustments,
    int? TranID,
    int? AdjNbr,
    PXGraph graph,
    string module)
  {
    return PXInvoiceSelectorAttribute.IsAlreadyMatched(match, invoice, graph, module) || PXInvoiceSelectorAttribute.IsAlreadySelected(adjustments, invoice, TranID, AdjNbr);
  }

  protected static bool IsAlreadyMatched(
    CABankTranMatch match,
    PX.Objects.CM.IRegister invoice,
    PXGraph graph,
    string module)
  {
    if (match.TranID.HasValue && match.LineNbr.HasValue)
      return true;
    foreach (CABankTranMatch caBankTranMatch in graph.Caches[typeof (CABankTranMatch)].Inserted)
    {
      if (caBankTranMatch.DocModule == module && caBankTranMatch.DocType == invoice.DocType && caBankTranMatch.DocRefNbr == invoice.RefNbr)
        return true;
    }
    return false;
  }

  protected static bool IsAlreadySelected(
    PXCache adjustments,
    PX.Objects.CM.IRegister invoice,
    int? TranID,
    int? AdjNbr)
  {
    foreach (CABankTranAdjustment bankTranAdjustment in adjustments.Inserted)
    {
      if (bankTranAdjustment.AdjdDocType == invoice.DocType && bankTranAdjustment.AdjdRefNbr == invoice.RefNbr)
      {
        int? tranId = bankTranAdjustment.TranID;
        int? nullable1 = TranID;
        if (tranId.GetValueOrDefault() == nullable1.GetValueOrDefault() & tranId.HasValue == nullable1.HasValue)
        {
          int? adjNbr = bankTranAdjustment.AdjNbr;
          int? nullable2 = AdjNbr;
          if (!(adjNbr.GetValueOrDefault() == nullable2.GetValueOrDefault() & adjNbr.HasValue == nullable2.HasValue))
            goto label_6;
        }
        else
          goto label_6;
      }
      if (!(bankTranAdjustment.AdjdDocType == invoice.DocType) || !(bankTranAdjustment.AdjdRefNbr == invoice.RefNbr))
        continue;
label_6:
      return true;
    }
    return false;
  }
}
