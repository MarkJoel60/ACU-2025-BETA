// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.APReleaseProcessJointCheck
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.Common.Abstractions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.JointChecks;

public class APReleaseProcessJointCheck : PXGraphExtension<APReleaseProcess>
{
  public PXSelectJoin<JointPayeePayment, InnerJoin<JointPayee, On<JointPayee.jointPayeeId, Equal<JointPayeePayment.jointPayeeId>>, InnerJoin<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<JointPayeePayment.invoiceDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<JointPayeePayment.invoiceRefNbr>>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APInvoice.curyInfoID>>>>>, Where<JointPayeePayment.paymentDocType, Equal<Current<PX.Objects.AP.APPayment.docType>>, And<JointPayeePayment.paymentRefNbr, Equal<Current<PX.Objects.AP.APPayment.refNbr>>, And<JointPayeePayment.isVoided, NotEqual<True>>>>> JointPayment;
  public PXSelect<JointPayee> JointPayees;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXOverride]
  public virtual List<PX.Objects.AP.APRegister> ReleaseDocProc(
    JournalEntry journalEntry,
    PX.Objects.AP.APRegister document,
    bool isPreBooking,
    out List<INRegister> documents,
    APReleaseProcessJointCheck.ReleaseDocProcDelegate baseHandler)
  {
    this.ValidateInvoice((IDocumentKey) document);
    return baseHandler(journalEntry, document, isPreBooking, out documents);
  }

  private void ValidateInvoice(IDocumentKey document)
  {
    if (document.DocType != "INV")
      return;
    PX.Objects.AP.APInvoice apInvoice = PX.Objects.AP.APInvoice.PK.Find((PXGraph) this.Base, document.DocType, document.RefNbr);
    if (!apInvoice.IsJointPayees.GetValueOrDefault() || !apInvoice.PaymentsByLinesAllowed.GetValueOrDefault())
      return;
    if (GraphHelper.RowCast<APTran>((IEnumerable) ((PXSelectBase<APTran>) this.Base.APTran_TranType_RefNbr).Select(new object[2]
    {
      (object) apInvoice.DocType,
      (object) apInvoice.RefNbr
    })).Any<APTran>((Func<APTran, bool>) (_ =>
    {
      Decimal? curyLineAmt = _.CuryLineAmt;
      Decimal num = 0M;
      return curyLineAmt.GetValueOrDefault() < num & curyLineAmt.HasValue;
    })))
      throw new PXException("Negative lines are not allowed in documents with the Joint Payees check box selected.");
  }

  [PXOverride]
  public virtual void ProcessPayment(
    JournalEntry je,
    PX.Objects.AP.APRegister doc,
    PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, PX.Objects.AP.Vendor, PX.Objects.CA.CashAccount> res,
    Action<JournalEntry, PX.Objects.AP.APRegister, PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, PX.Objects.AP.Vendor, PX.Objects.CA.CashAccount>> baseMethod)
  {
    baseMethod(je, doc, res);
    Dictionary<int, Decimal> dictionary1 = new Dictionary<int, Decimal>();
    foreach (PXResult<JointPayeePayment, JointPayee, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo> pxResult in ((PXSelectBase<JointPayeePayment>) this.JointPayment).Select(Array.Empty<object>()))
    {
      JointPayeePayment jointPayeePayment = PXResult<JointPayeePayment, JointPayee, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      JointPayee jointPayee = PXResult<JointPayeePayment, JointPayee, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.AP.APInvoice apInvoice = PXResult<JointPayeePayment, JointPayee, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo info = PXResult<JointPayeePayment, JointPayee, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      Decimal curyval;
      if (apInvoice.CuryID == doc.CuryID)
        curyval = jointPayeePayment.CuryJointAmountToPay.GetValueOrDefault();
      else
        PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(((PXGraph) this.Base).Caches[typeof (PX.Objects.AP.APInvoice)], info, jointPayeePayment.JointAmountToPay.GetValueOrDefault(), out curyval);
      Dictionary<int, Decimal> dictionary2 = dictionary1;
      int? jointPayeeId = jointPayee.JointPayeeId;
      int key1 = jointPayeeId.Value;
      if (dictionary2.ContainsKey(key1))
      {
        Dictionary<int, Decimal> dictionary3 = dictionary1;
        jointPayeeId = jointPayee.JointPayeeId;
        int key2 = jointPayeeId.Value;
        dictionary3[key2] += curyval;
      }
      else
      {
        Dictionary<int, Decimal> dictionary4 = dictionary1;
        jointPayeeId = jointPayee.JointPayeeId;
        int key3 = jointPayeeId.Value;
        Decimal num = curyval;
        dictionary4.Add(key3, num);
      }
    }
    foreach (KeyValuePair<int, Decimal> keyValuePair in dictionary1)
      this.AppendAmountPaid(new int?(keyValuePair.Key), keyValuePair.Value);
    if (doc.DocType == "PPM")
      this.ProcessPrepaymentWithJointBills(doc);
    if (!(doc.DocType == "VCK"))
      return;
    this.ProcessVoidCheck(doc);
  }

  private void AppendAmountPaid(int? jointPayeeID, Decimal curyJointAmountPaid)
  {
    JointPayee jointPayee = JointPayee.PK.Find((PXGraph) this.Base, jointPayeeID);
    if (jointPayee == null)
      return;
    jointPayee.CuryJointAmountPaid = new Decimal?(jointPayee.CuryJointAmountPaid.GetValueOrDefault() + curyJointAmountPaid);
    ((PXSelectBase<JointPayee>) this.JointPayees).Update(jointPayee);
  }

  protected virtual void ProcessPrepaymentWithJointBills(PX.Objects.AP.APRegister doc)
  {
    PXSelectJoin<APAdjust, InnerJoin<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<APAdjust.adjdDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APInvoice.curyInfoID>>, InnerJoin<JointPayee, On<JointPayee.aPDocType, Equal<APAdjust.adjdDocType>, And<JointPayee.aPDocType, Equal<APAdjust.adjdDocType>, And<JointPayee.aPRefNbr, Equal<APAdjust.adjdRefNbr>, And<JointPayee.aPLineNbr, Equal<APAdjust.adjdLineNbr>, And<JointPayee.isMainPayee, Equal<True>>>>>>>>>, Where<PX.Objects.AP.APInvoice.isJointPayees, Equal<True>, And<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>>>>> pxSelectJoin = new PXSelectJoin<APAdjust, InnerJoin<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<APAdjust.adjdDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APInvoice.curyInfoID>>, InnerJoin<JointPayee, On<JointPayee.aPDocType, Equal<APAdjust.adjdDocType>, And<JointPayee.aPDocType, Equal<APAdjust.adjdDocType>, And<JointPayee.aPRefNbr, Equal<APAdjust.adjdRefNbr>, And<JointPayee.aPLineNbr, Equal<APAdjust.adjdLineNbr>, And<JointPayee.isMainPayee, Equal<True>>>>>>>>>, Where<PX.Objects.AP.APInvoice.isJointPayees, Equal<True>, And<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>>>>>((PXGraph) this.Base);
    Dictionary<int, Decimal> dictionary1 = new Dictionary<int, Decimal>();
    object[] objArray = new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    };
    foreach (PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo, JointPayee> pxResult in ((PXSelectBase<APAdjust>) pxSelectJoin).Select(objArray))
    {
      APAdjust apAdjust = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo, JointPayee>.op_Implicit(pxResult);
      JointPayee jointPayee = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo, JointPayee>.op_Implicit(pxResult);
      PX.Objects.AP.APInvoice apInvoice = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo, JointPayee>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo info = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo, JointPayee>.op_Implicit(pxResult);
      JointPayeePayment jointPayeePayment = ((PXSelectBase<JointPayeePayment>) this.JointPayment).Insert();
      jointPayeePayment.JointPayeeId = jointPayee.JointPayeeId;
      jointPayeePayment.InvoiceDocType = apAdjust.AdjdDocType;
      jointPayeePayment.InvoiceRefNbr = apAdjust.AdjdRefNbr;
      jointPayeePayment.InvoiceRefNbr = apAdjust.AdjdRefNbr;
      jointPayeePayment.PaymentDocType = apAdjust.AdjgDocType;
      jointPayeePayment.PaymentRefNbr = apAdjust.AdjgRefNbr;
      int? nullable = apAdjust.AdjdLineNbr;
      jointPayeePayment.AdjustmentNumber = new int?(nullable.GetValueOrDefault());
      Decimal curyval;
      if (apInvoice.CuryID == doc.CuryID)
        curyval = apAdjust.CuryAdjdAmt.GetValueOrDefault();
      else
        PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(((PXGraph) this.Base).Caches[typeof (PX.Objects.AP.APInvoice)], info, apAdjust.AdjAmt.GetValueOrDefault(), out curyval);
      jointPayeePayment.CuryJointAmountToPay = new Decimal?(curyval);
      Dictionary<int, Decimal> dictionary2 = dictionary1;
      nullable = jointPayee.JointPayeeId;
      int key1 = nullable.Value;
      if (dictionary2.ContainsKey(key1))
      {
        Dictionary<int, Decimal> dictionary3 = dictionary1;
        nullable = jointPayee.JointPayeeId;
        int key2 = nullable.Value;
        dictionary3[key2] += curyval;
      }
      else
      {
        Dictionary<int, Decimal> dictionary4 = dictionary1;
        nullable = jointPayee.JointPayeeId;
        int key3 = nullable.Value;
        Decimal num = curyval;
        dictionary4.Add(key3, num);
      }
    }
    foreach (KeyValuePair<int, Decimal> keyValuePair in dictionary1)
      this.AppendAmountPaid(new int?(keyValuePair.Key), keyValuePair.Value);
  }

  protected virtual void ProcessVoidCheck(PX.Objects.AP.APRegister doc)
  {
    PXSelectJoin<APAdjust, InnerJoin<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<APAdjust.adjdDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APInvoice.curyInfoID>>, InnerJoin<JointPayee, On<JointPayee.jointPayeeId, Equal<APAdjust.jointPayeeID>>>>>, Where<PX.Objects.AP.APInvoice.isJointPayees, Equal<True>, And<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>>>>> pxSelectJoin = new PXSelectJoin<APAdjust, InnerJoin<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<APAdjust.adjdDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APInvoice.curyInfoID>>, InnerJoin<JointPayee, On<JointPayee.jointPayeeId, Equal<APAdjust.jointPayeeID>>>>>, Where<PX.Objects.AP.APInvoice.isJointPayees, Equal<True>, And<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>>>>>((PXGraph) this.Base);
    Dictionary<int, Decimal> dictionary1 = new Dictionary<int, Decimal>();
    object[] objArray = new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    };
    foreach (PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo, JointPayee> pxResult in ((PXSelectBase<APAdjust>) pxSelectJoin).Select(objArray))
    {
      APAdjust apAdjust = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo, JointPayee>.op_Implicit(pxResult);
      JointPayee jointPayee = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo, JointPayee>.op_Implicit(pxResult);
      PX.Objects.AP.APInvoice apInvoice = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo, JointPayee>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo info = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo, JointPayee>.op_Implicit(pxResult);
      JointPayeePayment jointPayeePayment = ((PXSelectBase<JointPayeePayment>) this.JointPayment).Insert();
      jointPayeePayment.JointPayeeId = jointPayee.JointPayeeId;
      jointPayeePayment.InvoiceDocType = apAdjust.AdjdDocType;
      jointPayeePayment.InvoiceRefNbr = apAdjust.AdjdRefNbr;
      jointPayeePayment.InvoiceRefNbr = apAdjust.AdjdRefNbr;
      jointPayeePayment.PaymentDocType = apAdjust.AdjgDocType;
      jointPayeePayment.PaymentRefNbr = apAdjust.AdjgRefNbr;
      int? nullable = apAdjust.AdjdLineNbr;
      jointPayeePayment.AdjustmentNumber = new int?(nullable.GetValueOrDefault());
      Decimal curyval;
      if (apInvoice.CuryID == doc.CuryID)
        curyval = apAdjust.CuryAdjdAmt.GetValueOrDefault();
      else
        PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(((PXGraph) this.Base).Caches[typeof (PX.Objects.AP.APInvoice)], info, apAdjust.AdjAmt.GetValueOrDefault(), out curyval);
      jointPayeePayment.CuryJointAmountToPay = new Decimal?(curyval);
      Dictionary<int, Decimal> dictionary2 = dictionary1;
      nullable = jointPayee.JointPayeeId;
      int key1 = nullable.Value;
      if (dictionary2.ContainsKey(key1))
      {
        Dictionary<int, Decimal> dictionary3 = dictionary1;
        nullable = jointPayee.JointPayeeId;
        int key2 = nullable.Value;
        dictionary3[key2] += curyval;
      }
      else
      {
        Dictionary<int, Decimal> dictionary4 = dictionary1;
        nullable = jointPayee.JointPayeeId;
        int key3 = nullable.Value;
        Decimal num = curyval;
        dictionary4.Add(key3, num);
      }
    }
    foreach (KeyValuePair<int, Decimal> keyValuePair in dictionary1)
      this.AppendAmountPaid(new int?(keyValuePair.Key), keyValuePair.Value);
  }

  [PXOverride]
  public void PerformPersist(PXGraph.IPersistPerformer persister)
  {
    persister.Insert(((PXSelectBase) this.JointPayment).Cache);
    persister.Update(((PXSelectBase) this.JointPayment).Cache);
    persister.Update(((PXSelectBase) this.JointPayees).Cache);
  }

  public delegate List<PX.Objects.AP.APRegister> ReleaseDocProcDelegate(
    JournalEntry journalEntry,
    PX.Objects.AP.APRegister document,
    bool isPreBooking,
    out List<INRegister> documents);
}
