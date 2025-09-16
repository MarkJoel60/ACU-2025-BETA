// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PXInvoiceSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA.BankStatementHelpers;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

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

  protected virtual IEnumerable GetRecords(string refNbr)
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
          foreach (PX.Objects.AP.APAdjust.APInvoice apInvoice in PXInvoiceSelectorAttribute.GetRecordsAP(currentAdj, current1, cach2, selectorAttribute._Graph, refNbr))
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
          foreach (ARAdjust.ARInvoice arInvoice in PXInvoiceSelectorAttribute.GetRecordsAR(currentAdj, current1, cach2, selectorAttribute._Graph, refNbr))
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
    PXGraph graph,
    string refNbr = null)
  {
    foreach (ARAdjust.ARInvoice arInvoice in PXInvoiceSelectorAttribute.GetRecordsAR(currentAdj.AdjdDocType, currentAdj.TranID, currentAdj.AdjNbr, currentBankTran, adjustments, graph, refNbr))
      yield return arInvoice;
  }

  public static IEnumerable<PX.Objects.AP.APAdjust.APInvoice> GetRecordsAP(
    CABankTranAdjustment currentAdj,
    CABankTran currentBankTran,
    PXCache adjustments,
    PXGraph graph,
    string refNbr = null)
  {
    foreach (PX.Objects.AP.APAdjust.APInvoice apInvoice in PXInvoiceSelectorAttribute.GetRecordsAP(currentAdj.AdjdDocType, currentAdj.TranID, currentAdj.AdjNbr, currentBankTran, adjustments, graph, refNbr))
      yield return apInvoice;
  }

  public static IEnumerable<ARAdjust.ARInvoice> GetRecordsAR(
    string AdjdDocType,
    int? TranID,
    int? AdjNbr,
    CABankTran currentBankTran,
    PXCache adjustments,
    PXGraph graph,
    string refNbr = null)
  {
    PXSelectBase<ARAdjust.ARInvoice> pxSelectBase1 = (PXSelectBase<ARAdjust.ARInvoice>) new FbqlSelect<SelectFromBase<ARAdjust.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CABankTranMatch>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABankTranMatch.docModule, Equal<BatchModule.moduleAR>>>>, And<BqlOperand<CABankTranMatch.docType, IBqlString>.IsEqual<ARAdjust.ARInvoice.docType>>>, And<BqlOperand<CABankTranMatch.docRefNbr, IBqlString>.IsEqual<ARAdjust.ARInvoice.refNbr>>>>.And<BqlOperand<CABankTranMatch.tranID, IBqlInt>.IsNotEqual<P.AsInt>>>>, FbqlJoins.Left<ARAdjust>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdDocType, Equal<ARAdjust.ARInvoice.docType>>>>, And<BqlOperand<ARAdjust.adjdRefNbr, IBqlString>.IsEqual<ARAdjust.ARInvoice.refNbr>>>, And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsNotEqual<P.AsString.ASCII>>>, And<BqlOperand<ARAdjust.released, IBqlBool>.IsEqual<boolFalse>>>>.And<BqlOperand<ARAdjust.voided, IBqlBool>.IsEqual<boolFalse>>>>, FbqlJoins.Left<ARAdjust2>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust2.adjgDocType, Equal<ARAdjust.ARInvoice.docType>>>>, And<BqlOperand<ARAdjust2.adjgRefNbr, IBqlString>.IsEqual<ARAdjust.ARInvoice.refNbr>>>, And<BqlOperand<ARAdjust.ARInvoice.docType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>>, And<BqlOperand<ARAdjust2.released, IBqlBool>.IsEqual<boolFalse>>>>.And<BqlOperand<ARAdjust2.voided, IBqlBool>.IsEqual<boolFalse>>>>, FbqlJoins.Left<CABankTranAdjustment>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABankTranAdjustment.adjdModule, Equal<BatchModule.moduleAR>>>>, And<BqlOperand<CABankTranAdjustment.adjdDocType, IBqlString>.IsEqual<ARAdjust.ARInvoice.docType>>>, And<BqlOperand<CABankTranAdjustment.adjdRefNbr, IBqlString>.IsEqual<ARAdjust.ARInvoice.refNbr>>>, And<BqlOperand<CABankTranAdjustment.released, IBqlBool>.IsEqual<boolFalse>>>>.And<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABankTranAdjustment.tranID, NotEqual<P.AsInt>>>>, Or<BqlOperand<Required<Parameter.ofInt>, IBqlInt>.IsNull>>>.Or<BqlOperand<CABankTranAdjustment.adjNbr, IBqlInt>.IsNotEqual<P.AsInt>>>>>>, FbqlJoins.Left<CABankTran>.On<BqlOperand<CABankTran.tranID, IBqlInt>.IsEqual<CABankTranAdjustment.tranID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.ARInvoice.customerID, In2<SearchFor<PX.Objects.AR.Override.BAccount.bAccountID>.In<SelectFromBase<PX.Objects.AR.Override.BAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Override.BAccount.bAccountID, Equal<P.AsInt>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Override.BAccount.consolidatingBAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.AR.Override.BAccount.consolidateToParent, IBqlBool>.IsEqual<True>>>>>>>>>, And<BqlOperand<ARAdjust.ARInvoice.docType, IBqlString>.IsEqual<P.AsString.ASCII>>>, And<BqlOperand<ARAdjust.ARInvoice.released, IBqlBool>.IsEqual<boolTrue>>>, And<BqlOperand<ARAdjust.ARInvoice.openDoc, IBqlBool>.IsEqual<boolTrue>>>, And<BqlOperand<ARAdjust.adjgRefNbr, IBqlString>.IsNull>>, And<BqlOperand<ARAdjust2.adjgRefNbr, IBqlString>.IsNull>>, And<BqlOperand<ARAdjust.ARInvoice.pendingPPD, IBqlBool>.IsNotEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABankTranAdjustment.adjdRefNbr, IsNull>>>>.Or<BqlOperand<CABankTran.origModule, IBqlString>.IsNotEqual<BatchModule.moduleAR>>>>, ARAdjust.ARInvoice>.View(graph);
    if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>())
    {
      Decimal? curyDebitAmt = currentBankTran.CuryDebitAmt;
      Decimal num = 0M;
      if (curyDebitAmt.GetValueOrDefault() > num & curyDebitAmt.HasValue)
        pxSelectBase1.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.docType, NotEqual<ARDocType.prepaymentInvoice>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.pendingPayment, IBqlBool>.IsEqual<True>>>>>();
      else
        pxSelectBase1.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.docType, NotEqual<ARDocType.prepaymentInvoice>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.pendingPayment, IBqlBool>.IsNotEqual<True>>>>>();
    }
    if (!string.IsNullOrEmpty(refNbr))
      pxSelectBase1.WhereAnd<Where<ARAdjust.ARInvoice.refNbr, Equal<Required<ARAdjust.ARInvoice.refNbr>>>>();
    PXSelectBase<ARAdjust.ARInvoice> pxSelectBase2 = pxSelectBase1;
    object[] objArray = new object[9]
    {
      (object) currentBankTran.TranID,
      (object) AdjdDocType,
      (object) TranID,
      (object) AdjNbr,
      (object) AdjNbr,
      (object) currentBankTran.PayeeBAccountID,
      (object) currentBankTran.PayeeBAccountID,
      (object) AdjdDocType,
      (object) refNbr
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
    PXGraph graph,
    string refNbr = null)
  {
    PXSelectBase<PX.Objects.AP.APAdjust.APInvoice> pxSelectBase1 = (PXSelectBase<PX.Objects.AP.APAdjust.APInvoice>) new PXSelectJoin<PX.Objects.AP.APAdjust.APInvoice, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>, And<CABankTranMatch.docType, Equal<PX.Objects.AP.APAdjust.APInvoice.docType>, And<CABankTranMatch.docRefNbr, Equal<PX.Objects.AP.APAdjust.APInvoice.refNbr>, And<CABankTranMatch.tranID, NotEqual<Required<CABankTran.tranID>>>>>>, LeftJoin<PX.Objects.AP.APAdjust, On<PX.Objects.AP.APAdjust.adjdDocType, Equal<PX.Objects.AP.APAdjust.APInvoice.docType>, And<PX.Objects.AP.APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APAdjust.APInvoice.refNbr>, And<PX.Objects.AP.APAdjust.released, Equal<boolFalse>>>>, LeftJoin<CABankTranAdjustment, On<CABankTranAdjustment.adjdModule, Equal<BatchModule.moduleAP>, And<CABankTranAdjustment.adjdDocType, Equal<PX.Objects.AP.APAdjust.APInvoice.docType>, And<CABankTranAdjustment.adjdRefNbr, Equal<PX.Objects.AP.APAdjust.APInvoice.refNbr>, And<CABankTranAdjustment.released, Equal<boolFalse>, And<Where<CABankTranAdjustment.tranID, NotEqual<Required<CABankTranAdjustment.tranID>>, Or<Required<CABankTranAdjustment.adjNbr>, IsNull, Or<CABankTranAdjustment.adjNbr, NotEqual<Required<CABankTranAdjustment.adjNbr>>>>>>>>>>, LeftJoin<PX.Objects.AP.Standalone.APPayment, On<PX.Objects.AP.Standalone.APPayment.docType, Equal<PX.Objects.AP.APAdjust.APInvoice.docType>, And<PX.Objects.AP.Standalone.APPayment.refNbr, Equal<PX.Objects.AP.APAdjust.APInvoice.refNbr>, And<Where<PX.Objects.AP.Standalone.APPayment.docType, Equal<APDocType.prepayment>, Or<PX.Objects.AP.Standalone.APPayment.docType, Equal<APDocType.debitAdj>>>>>>, LeftJoin<CABankTran, On<CABankTran.tranID, Equal<CABankTranAdjustment.tranID>>>>>>>, Where<PX.Objects.AP.APAdjust.APInvoice.vendorID, Equal<Optional<CABankTran.payeeBAccountID>>, And<PX.Objects.AP.APAdjust.APInvoice.docType, Equal<Optional<CABankTranAdjustment.adjdDocType>>, And2<Where<PX.Objects.AP.APAdjust.APInvoice.released, Equal<True>, Or<PX.Objects.AP.APRegister.prebooked, Equal<True>>>, And<PX.Objects.AP.APAdjust.APInvoice.openDoc, Equal<boolTrue>, And2<Where<CABankTranAdjustment.adjdRefNbr, IsNull, Or<CABankTran.origModule, NotEqual<BatchModule.moduleAP>>>, And<PX.Objects.AP.APAdjust.adjgRefNbr, IsNull, And2<Where<PX.Objects.AP.Standalone.APPayment.refNbr, IsNull, And<Required<CABankTran.docType>, NotEqual<APDocType.refund>, Or<PX.Objects.AP.Standalone.APPayment.refNbr, IsNotNull, And<Required<CABankTran.docType>, Equal<APDocType.refund>, Or<PX.Objects.AP.Standalone.APPayment.docType, Equal<APDocType.debitAdj>, And<Required<CABankTran.docType>, Equal<APDocType.check>, Or<PX.Objects.AP.Standalone.APPayment.docType, Equal<APDocType.debitAdj>, And<Required<CABankTran.docType>, Equal<APDocType.voidCheck>>>>>>>>>, And<Where<PX.Objects.AP.APAdjust.APInvoice.docDate, LessEqual<Required<CABankTran.matchingPaymentDate>>, And<PX.Objects.AP.APAdjust.APInvoice.finPeriodID, LessEqual<Required<CABankTran.matchingfinPeriodID>>, Or<Current<APSetup.earlyChecks>, Equal<True>, And<Required<CABankTran.docType>, NotEqual<APDocType.refund>>>>>>>>>>>>>>(graph);
    if (!string.IsNullOrEmpty(refNbr))
      pxSelectBase1.WhereAnd<Where<PX.Objects.AP.APAdjust.APInvoice.refNbr, Equal<Required<PX.Objects.AP.APAdjust.APInvoice.refNbr>>>>();
    PXSelectBase<PX.Objects.AP.APAdjust.APInvoice> pxSelectBase2 = pxSelectBase1;
    object[] objArray = new object[14]
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
      (object) currentBankTran.DocType,
      (object) refNbr
    };
    foreach (PXResult<PX.Objects.AP.APAdjust.APInvoice, CABankTranMatch> pxResult in pxSelectBase2.Select(objArray))
    {
      PX.Objects.AP.APAdjust.APInvoice invoice = PXResult<PX.Objects.AP.APAdjust.APInvoice, CABankTranMatch>.op_Implicit(pxResult);
      CABankTranMatch match = PXResult<PX.Objects.AP.APAdjust.APInvoice, CABankTranMatch>.op_Implicit(pxResult);
      if (!(PXAccess.GetBranch(invoice.BranchID).BaseCuryID != PXAccess.GetBranch(graph.Accessinfo.BranchID).BaseCuryID) && !PXInvoiceSelectorAttribute.ShouldSkipRecord(match, (PX.Objects.CM.IRegister) invoice, adjustments, TranID, AdjNbr, graph, "AP"))
        yield return invoice;
    }
  }

  /// <summary>
  /// The handler of the <tt>FieldVerifying</tt> event.
  /// </summary>
  /// <param name="sender">The cache object that has raised the event.</param>
  /// <param name="e">The event arguments.</param>
  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (((PXSelectorAttribute) this).ValidateValue && e.NewValue != null && (sender.Keys.Count == 0 || ((PXEventSubscriberAttribute) this)._FieldName != sender.Keys[sender.Keys.Count - 1]))
    {
      List<object> objectList = sender.Graph.Views[((PXSelectorAttribute) this)._ViewName].SelectMultiBound(new object[1]
      {
        e.Row
      }, new object[1]{ e.NewValue });
      PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(((PXSelectorAttribute) this).ForeignField)];
      foreach (object obj1 in objectList)
      {
        object obj2 = (object) PXResult.UnwrapMain(obj1);
        object objA = cach.GetValue(obj2, ((PXSelectorAttribute) this).ForeignField.Name);
        if (object.Equals(objA, e.NewValue))
          return;
        if (objA is Array && e.NewValue is Array && ((Array) objA).Length == ((Array) e.NewValue).Length)
        {
          bool flag = true;
          int index = 0;
          while (index < ((Array) objA).Length && (flag = flag && object.Equals(((Array) objA).GetValue(index), ((Array) e.NewValue).GetValue(index))))
            ++index;
          if (flag)
            return;
        }
      }
      throw new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) $"[{((PXEventSubscriberAttribute) this)._FieldName}]"
      });
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
