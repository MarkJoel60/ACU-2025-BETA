// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.ApPayBillsJointCheckExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CN.Compliance;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CN.JointChecks;

public class ApPayBillsJointCheckExt : PXGraphExtension<
#nullable disable
APPayBills>
{
  [PXCopyPasteHiddenView]
  public PXSetup<LienWaiverSetup> lienWaiverSetup;
  public FbqlSelect<SelectFromBase<APInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<APTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  APInvoice.paymentsByLinesAllowed, 
  #nullable disable
  Equal<True>>>>, And<BqlOperand<
  #nullable enable
  APTran.tranType, IBqlString>.IsEqual<
  #nullable disable
  APInvoice.docType>>>, And<BqlOperand<
  #nullable enable
  APTran.refNbr, IBqlString>.IsEqual<
  #nullable disable
  APInvoice.refNbr>>>>.And<BqlOperand<
  #nullable enable
  APTran.lineNbr, IBqlInt>.IsEqual<
  #nullable disable
  P.AsInt>>>>, FbqlJoins.Inner<PX.Objects.CM.Extensions.CurrencyInfo>.On<BqlOperand<
  #nullable enable
  PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, IBqlLong>.IsEqual<
  #nullable disable
  APInvoice.curyInfoID>>>, FbqlJoins.Left<JointPayeePerDoc>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  JointPayeePerDoc.aPDocType, 
  #nullable disable
  Equal<APInvoice.docType>>>>, And<BqlOperand<
  #nullable enable
  JointPayeePerDoc.aPRefNbr, IBqlString>.IsEqual<
  #nullable disable
  APInvoice.refNbr>>>, And<BqlOperand<
  #nullable enable
  APInvoice.isRetainageDocument, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  JointPayeePerDoc.aPDocType, 
  #nullable disable
  Equal<APInvoice.origDocType>>>>, And<BqlOperand<
  #nullable enable
  JointPayeePerDoc.aPRefNbr, IBqlString>.IsEqual<
  #nullable disable
  APInvoice.origRefNbr>>>>.And<BqlOperand<
  #nullable enable
  APInvoice.isRetainageDocument, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>>, FbqlJoins.Left<JointPayeePerLine>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  JointPayeePerLine.aPDocType, 
  #nullable disable
  Equal<APInvoice.docType>>>>, And<BqlOperand<
  #nullable enable
  JointPayeePerLine.aPRefNbr, IBqlString>.IsEqual<
  #nullable disable
  APInvoice.refNbr>>>, And<BqlOperand<
  #nullable enable
  JointPayeePerLine.aPLineNbr, IBqlInt>.IsEqual<
  #nullable disable
  APTran.lineNbr>>>, And<BqlOperand<
  #nullable enable
  APInvoice.isRetainageDocument, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  JointPayeePerLine.aPDocType, 
  #nullable disable
  Equal<APInvoice.origDocType>>>>, And<BqlOperand<
  #nullable enable
  JointPayeePerLine.aPRefNbr, IBqlString>.IsEqual<
  #nullable disable
  APInvoice.origRefNbr>>>, And<BqlOperand<
  #nullable enable
  JointPayeePerLine.aPLineNbr, IBqlInt>.IsEqual<
  #nullable disable
  APTran.lineNbr>>>>.And<BqlOperand<
  #nullable enable
  APInvoice.isRetainageDocument, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  APInvoice.docType, 
  #nullable disable
  Equal<P.AsString>>>>>.And<BqlOperand<
  #nullable enable
  APInvoice.refNbr, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>, APInvoice>.View APInvoiceJointPayeeLines;
  protected Dictionary<string, ApPayBillsJointCheckExt.PayBillData> billDataStore;
  private bool skipAmountToPayVerification;
  private int? lienWaiverDocType;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [APInvoiceType.AdjdRefNbr(typeof (Search2<APInvoice.refNbr, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<APInvoice.vendorID>, And<Where<PX.Objects.CR.BAccount.vStatus, Equal<VendorStatus.active>, Or<PX.Objects.CR.BAccount.vStatus, Equal<VendorStatus.oneTime>>>>>, LeftJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>, And<APAdjust.released, Equal<False>, And<APAdjust.voided, Equal<False>, And<Where<APAdjust.adjgDocType, NotEqual<Current<APPayment.docType>>, Or<APAdjust.adjgRefNbr, NotEqual<Current<APPayment.refNbr>>>>>>>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<APInvoice.docType>, And<APPayment.refNbr, Equal<APInvoice.refNbr>, And<APPayment.docType, Equal<APDocType.prepayment>>>>>>>, Where<APInvoice.docType, Equal<Optional<APAdjust.adjdDocType>>, And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>, And<APInvoice.openDoc, Equal<True>, And2<Where<APAdjust.adjgRefNbr, IsNull, Or<APInvoice.isJointPayees, Equal<True>>>, And<APPayment.refNbr, IsNull, And<APInvoice.pendingPPD, NotEqual<True>>>>>>>>), Filterable = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<APAdjust.adjdRefNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Joint Payees", Visible = false, FieldClass = "Construction")]
  protected virtual void _(PX.Data.Events.CacheAttached<APInvoice.isJointPayees> e)
  {
  }

  [PXOverride]
  public virtual IEnumerable apdocumentlist(Func<IEnumerable> baseMethod)
  {
    this.billDataStore = new Dictionary<string, ApPayBillsJointCheckExt.PayBillData>();
    IEnumerable enumerable = baseMethod();
    Dictionary<int, ApPayBillsJointCheckExt.BalanceAmount> jointPayeesBalance = this.GetJointPayeesBalance();
    Dictionary<string, ApPayBillsJointCheckExt.BalanceAmount> billBalance = new Dictionary<string, ApPayBillsJointCheckExt.BalanceAmount>();
    foreach (PXResult<APAdjust, APInvoice, APTran> pxResult in enumerable)
    {
      APAdjust apAdjust1;
      APInvoice apInvoice1;
      APTran apTran1;
      pxResult.Deconstruct(ref apAdjust1, ref apInvoice1, ref apTran1);
      APAdjust apAdjust2 = apAdjust1;
      APInvoice apInvoice2 = apInvoice1;
      APTran apTran2 = apTran1;
      bool? nullable1 = apInvoice2.IsJointPayees;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = apInvoice2.PaymentsByLinesAllowed;
        Decimal? nullable2;
        Decimal valueOrDefault1;
        if (!nullable1.GetValueOrDefault())
        {
          nullable2 = apInvoice2.CuryDocBal;
          valueOrDefault1 = nullable2.GetValueOrDefault();
        }
        else
        {
          nullable2 = apTran2.CuryTranBal;
          valueOrDefault1 = nullable2.GetValueOrDefault();
        }
        Decimal amount = valueOrDefault1;
        nullable1 = apInvoice2.PaymentsByLinesAllowed;
        Decimal valueOrDefault2;
        if (!nullable1.GetValueOrDefault())
        {
          nullable2 = apInvoice2.DocBal;
          valueOrDefault2 = nullable2.GetValueOrDefault();
        }
        else
        {
          nullable2 = apTran2.TranBal;
          valueOrDefault2 = nullable2.GetValueOrDefault();
        }
        Decimal amountInBase = valueOrDefault2;
        string keyForBillBalance = this.GetKeyForBillBalance(apAdjust2);
        if (!billBalance.ContainsKey(keyForBillBalance))
          billBalance.Add(keyForBillBalance, new ApPayBillsJointCheckExt.BalanceAmount(amount, amountInBase));
        this.CalculateJointAmountToPay(apAdjust2, jointPayeesBalance, billBalance);
      }
    }
    return enumerable;
  }

  private string GetKeyForBillBalance(APAdjust adjust)
  {
    return $"{adjust.AdjdDocType}.{adjust.AdjdRefNbr}.{adjust.AdjdLineNbr}";
  }

  private static string GetKeyForPayData(string docType, string refNbr, int lineNbr)
  {
    return $"{docType}.{refNbr}.{lineNbr}";
  }

  private Dictionary<int, ApPayBillsJointCheckExt.BalanceAmount> GetJointPayeesBalance()
  {
    Dictionary<int, ApPayBillsJointCheckExt.BalanceAmount> jointPayeesBalance = new Dictionary<int, ApPayBillsJointCheckExt.BalanceAmount>();
    foreach (ApPayBillsJointCheckExt.PayBillData payBillData in this.billDataStore.Values)
    {
      foreach (JointPayee jointPayee in payBillData.JointPayees)
      {
        Dictionary<int, ApPayBillsJointCheckExt.BalanceAmount> dictionary1 = jointPayeesBalance;
        int? jointPayeeId = jointPayee.JointPayeeId;
        int key1 = jointPayeeId.Value;
        if (!dictionary1.ContainsKey(key1))
        {
          Dictionary<int, ApPayBillsJointCheckExt.BalanceAmount> dictionary2 = jointPayeesBalance;
          jointPayeeId = jointPayee.JointPayeeId;
          int key2 = jointPayeeId.Value;
          ApPayBillsJointCheckExt.BalanceAmount balanceAmount = new ApPayBillsJointCheckExt.BalanceAmount(jointPayee.CuryJointBalance.GetValueOrDefault(), jointPayee.JointBalance.GetValueOrDefault());
          dictionary2.Add(key2, balanceAmount);
        }
      }
    }
    return jointPayeesBalance;
  }

  [PXOverride]
  public virtual BqlCommand ComposeBQLCommandForAPDocumentListSelect(Func<BqlCommand> baseMethod)
  {
    BqlCommand bqlCommand = BqlCommand.AppendJoin<LeftJoin<JointPayeePerLine, On<Where2<Where<JointPayeePerLine.aPDocType, Equal<APInvoice.docType>, And<JointPayeePerLine.aPRefNbr, Equal<APInvoice.refNbr>, And<JointPayeePerLine.aPLineNbr, Equal<APTran.lineNbr>, And<APInvoice.isRetainageDocument, Equal<False>>>>>, Or<Where<JointPayeePerLine.aPDocType, Equal<APInvoice.origDocType>, And<JointPayeePerLine.aPRefNbr, Equal<APInvoice.origRefNbr>, And<JointPayeePerLine.aPLineNbr, Equal<APTran.lineNbr>, And<APInvoice.isRetainageDocument, Equal<True>>>>>>>>>>(BqlCommand.AppendJoin<LeftJoin<JointPayeePerDoc, On<Where2<Where<JointPayeePerDoc.aPDocType, Equal<APInvoice.docType>, And<JointPayeePerDoc.aPRefNbr, Equal<APInvoice.refNbr>, And<APInvoice.isRetainageDocument, Equal<False>>>>, Or<Where<JointPayeePerDoc.aPDocType, Equal<APInvoice.origDocType>, And<JointPayeePerDoc.aPRefNbr, Equal<APInvoice.origRefNbr>, And<APInvoice.isRetainageDocument, Equal<True>>>>>>>>>(baseMethod()));
    if (!string.IsNullOrEmpty(((PXSelectBase<PayBillsFilter>) this.Base.Filter).Current.DocType) && !string.IsNullOrEmpty(((PXSelectBase<PayBillsFilter>) this.Base.Filter).Current.RefNbr))
      bqlCommand = bqlCommand.WhereAnd(typeof (Where2<Where<APInvoice.docType, Equal<Current<PayBillsFilter.docType>>, And<APInvoice.refNbr, Equal<Current<PayBillsFilter.refNbr>>>>, Or<Where<APInvoice.origDocType, Equal<Current<PayBillsFilter.docType>>, And<APInvoice.origRefNbr, Equal<Current<PayBillsFilter.refNbr>>>>>>));
    return bqlCommand.WhereAnd(typeof (Where2<Where<APInvoice.isJointPayees, Equal<False>, Or<APInvoice.isRetainageDocument, Equal<True>>>, Or<Where<JointPayeePerDoc.jointPayeeId, IsNotNull, Or<JointPayeePerLine.jointPayeeId, IsNotNull>>>>));
  }

  [PXOverride]
  public virtual void StoreResultset(
    PXResult res,
    string docType,
    string refNbr,
    int? lineNbr,
    Action<PXResult, string, string, int?> baseMethod)
  {
    APInvoice apInvoice = res.GetItem<APInvoice>();
    APTran apTran = res.GetItem<APTran>();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = res.GetItem<PX.Objects.CM.Extensions.CurrencyInfo>();
    JointPayeePerDoc jointPayeePerDoc = res.GetItem<JointPayeePerDoc>();
    JointPayeePerLine jointPayeePerLine = res.GetItem<JointPayeePerLine>();
    PXView view = ((PXSelectBase) this.APInvoiceJointPayeeLines).View;
    List<object> objectList = new List<object>();
    objectList.Add((object) new PXResult<APInvoice, APTran, PX.Objects.CM.Extensions.CurrencyInfo, JointPayeePerDoc, JointPayeePerLine>(apInvoice, apTran, currencyInfo, jointPayeePerDoc, jointPayeePerLine));
    PXQueryParameters pxQueryParameters = PXQueryParameters.ExplicitParameters(new object[3]
    {
      (object) lineNbr,
      (object) docType,
      (object) refNbr
    });
    view.StoreResult(objectList, pxQueryParameters);
  }

  [PXOverride]
  public virtual APAdjust InitRecord(PXResult res, Func<PXResult, APAdjust> baseMethod)
  {
    APInvoice invoice = res.GetItem<APInvoice>();
    APTran tran = res.GetItem<APTran>();
    PX.Objects.CM.Extensions.CurrencyInfo curyInfo = res.GetItem<PX.Objects.CM.Extensions.CurrencyInfo>();
    JointPayeePerDoc jointPayeePerDoc = res.GetItem<JointPayeePerDoc>();
    JointPayeePerLine jointPayeePerLine = res.GetItem<JointPayeePerLine>();
    APAdjust apAdjust = baseMethod(res);
    if (invoice == null)
      return apAdjust;
    bool valueOrDefault = invoice.PaymentsByLinesAllowed.GetValueOrDefault();
    if (invoice.IsJointPayees.GetValueOrDefault())
    {
      int? nullable = valueOrDefault ? jointPayeePerLine.JointPayeeId : jointPayeePerDoc.JointPayeeId;
      apAdjust.AdjNbr = nullable;
      apAdjust.JointPayeeID = nullable;
    }
    string docType = invoice.DocType;
    string refNbr = invoice.RefNbr;
    int? nullable1;
    int lineNbr;
    if (!valueOrDefault)
    {
      lineNbr = 0;
    }
    else
    {
      nullable1 = tran.LineNbr;
      lineNbr = nullable1.GetValueOrDefault();
    }
    string keyForPayData = ApPayBillsJointCheckExt.GetKeyForPayData(docType, refNbr, lineNbr);
    ApPayBillsJointCheckExt.PayBillData payBillData;
    if (!this.billDataStore.TryGetValue(keyForPayData, out payBillData))
    {
      payBillData = new ApPayBillsJointCheckExt.PayBillData(invoice, tran, curyInfo);
      this.billDataStore.Add(keyForPayData, payBillData);
    }
    if (valueOrDefault)
    {
      nullable1 = jointPayeePerLine.JointPayeeId;
      if (nullable1.HasValue)
        payBillData.JointPayees.Add((JointPayee) jointPayeePerLine);
    }
    else
    {
      nullable1 = jointPayeePerDoc.JointPayeeId;
      if (nullable1.HasValue)
        payBillData.JointPayees.Add((JointPayee) jointPayeePerDoc);
    }
    return apAdjust;
  }

  [PXOverride]
  public virtual bool SetSuggestedAmounts(
    APAdjust adj,
    APTran tran,
    APInvoice invoice,
    Func<APAdjust, APTran, APInvoice, bool> baseMethod)
  {
    bool flag = baseMethod(adj, tran, invoice);
    ApPayBillsJointCheckExt.PayBillData payBillData = this.SelectPayBillData(adj.AdjdDocType, adj.AdjdRefNbr, adj.AdjdLineNbr);
    if (payBillData != null)
    {
      bool? nullable = (bool?) payBillData.Invoice?.IsJointPayees;
      if (nullable.GetValueOrDefault())
      {
        ApAdjustExt extension = PXCache<APAdjust>.GetExtension<ApAdjustExt>(adj);
        if (extension != null)
        {
          extension.JointPayeeExternalName = payBillData.GetJointPayeeExternalName(adj.AdjNbr);
          JointPayee jointPayee = payBillData.GetJointPayee(adj.AdjNbr);
          if (jointPayee != null)
          {
            nullable = jointPayee.IsMainPayee;
            if (!nullable.GetValueOrDefault())
            {
              if (payBillData.Invoice.CuryID == ((PXSelectBase<PayBillsFilter>) this.Base.Filter).Current.CuryID)
              {
                extension.CuryJointAmountOwed = new Decimal?(jointPayee.CuryJointAmountOwed.GetValueOrDefault());
                extension.JointAmountOwed = new Decimal?(jointPayee.JointAmountOwed.GetValueOrDefault());
                extension.CuryJointBalance = new Decimal?(jointPayee.CuryJointBalance.GetValueOrDefault());
                extension.JointBalance = new Decimal?(jointPayee.JointBalance.GetValueOrDefault());
              }
              else
              {
                extension.JointAmountOwed = new Decimal?(jointPayee.JointAmountOwed.GetValueOrDefault());
                extension.JointBalance = new Decimal?(jointPayee.JointBalance.GetValueOrDefault());
                Decimal curyval;
                PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury<PayBillsFilter.curyInfoID>(((PXSelectBase) this.Base.APDocumentList).Cache, (object) extension, extension.JointAmountOwed.GetValueOrDefault(), out curyval);
                extension.CuryJointAmountOwed = new Decimal?(curyval);
                PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury<PayBillsFilter.curyInfoID>(((PXSelectBase) this.Base.APDocumentList).Cache, (object) extension, extension.JointBalance.GetValueOrDefault(), out curyval);
                extension.CuryJointBalance = new Decimal?(curyval);
              }
              adj.CuryDiscBal = new Decimal?(0M);
              adj.DiscBal = new Decimal?(0M);
              adj.CuryAdjgDiscAmt = new Decimal?(0M);
              adj.AdjDiscAmt = new Decimal?(0M);
            }
          }
          flag = true;
        }
      }
    }
    return flag;
  }

  [PXOverride]
  public virtual APPayment PaymentPostProcessing(
    APPaymentEntry pe,
    APPayment payment,
    Vendor vendor,
    Func<APPaymentEntry, APPayment, Vendor, APPayment> baseMethod)
  {
    APPayment apPayment = baseMethod(pe, payment, vendor);
    APPaymentEntryLienWaiver extension = ((PXGraph) pe).GetExtension<APPaymentEntryLienWaiver>();
    if (!apPayment.Hold.GetValueOrDefault())
    {
      bool? nullable = ((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.ShouldStopPayments;
      if (nullable.GetValueOrDefault() && extension.ContainsOutstandingLienWavers(payment))
      {
        payment.Hold = new bool?(true);
        ((PXSelectBase<APPayment>) pe.Document).Update(payment);
      }
      nullable = apPayment.Hold;
      if (!nullable.GetValueOrDefault())
        extension.GenerateLienWaivers();
      ((PXAction) pe.Save).Press();
    }
    return apPayment;
  }

  [PXOverride]
  public virtual APPaymentEntry CreatePaymentEntry(Func<APPaymentEntry> baseMethod)
  {
    APPaymentEntry paymentEntry = baseMethod();
    APPaymentEntryLienWaiver extension1 = ((PXGraph) paymentEntry).GetExtension<APPaymentEntryLienWaiver>();
    if (extension1 != null)
      extension1.IsPreparePaymentsMassProcessing = true;
    APPaymentEntryJointCheck extension2 = ((PXGraph) paymentEntry).GetExtension<APPaymentEntryJointCheck>();
    if (extension2 == null)
      return paymentEntry;
    extension2.IsPreparePaymentsMassProcessing = true;
    return paymentEntry;
  }

  private void CalculateJointAmountToPay(
    APAdjust adj,
    Dictionary<int, ApPayBillsJointCheckExt.BalanceAmount> jointPayeeBalance,
    Dictionary<string, ApPayBillsJointCheckExt.BalanceAmount> billBalance)
  {
    if (!adj.JointPayeeID.HasValue)
      return;
    ApAdjustExt extension = PXCache<APAdjust>.GetExtension<ApAdjustExt>(adj);
    bool? nullable1;
    int num1;
    if (extension == null)
    {
      num1 = 0;
    }
    else
    {
      nullable1 = extension.IsAmountPaidCalculated;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0)
      return;
    ApPayBillsJointCheckExt.PayBillData data = this.SelectPayBillData(adj.AdjdDocType, adj.AdjdRefNbr, adj.AdjdLineNbr);
    if (data == null)
      return;
    string keyForBillBalance = this.GetKeyForBillBalance(adj);
    if (data.Invoice.CuryID == ((PXSelectBase<PayBillsFilter>) this.Base.Filter).Current.CuryID)
    {
      Decimal amount = billBalance[keyForBillBalance].Amount;
      nullable1 = data.Invoice.PaymentsByLinesAllowed;
      Decimal num2 = nullable1.GetValueOrDefault() ? data.Tran.CuryCashDiscBal.GetValueOrDefault() : data.Invoice.CuryDiscBal.GetValueOrDefault();
      Decimal available = Math.Max(0M, amount - num2);
      Decimal valueOrDefault = this.GetAmountToPay(adj.JointPayeeID, data, available, jointPayeeBalance).GetValueOrDefault();
      adj.CuryAdjgAmt = new Decimal?(valueOrDefault);
      jointPayeeBalance[adj.JointPayeeID.Value].Amount -= valueOrDefault;
      billBalance[keyForBillBalance].Amount -= valueOrDefault;
    }
    else
    {
      Decimal amountInBase = billBalance[keyForBillBalance].AmountInBase;
      nullable1 = data.Invoice.PaymentsByLinesAllowed;
      Decimal num3 = nullable1.GetValueOrDefault() ? data.Tran.CashDiscBal.GetValueOrDefault() : data.Invoice.DiscBal.GetValueOrDefault();
      Decimal available = Math.Max(0M, amountInBase - num3);
      Decimal? nullable2 = this.GetAmountToPayInBase(adj.JointPayeeID, data, available, jointPayeeBalance);
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      adj.AdjAmt = new Decimal?(valueOrDefault1);
      PXCache cache = ((PXSelectBase) this.Base.Filter).Cache;
      PayBillsFilter current = ((PXSelectBase<PayBillsFilter>) this.Base.Filter).Current;
      nullable2 = adj.AdjAmt;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal num4;
      ref Decimal local = ref num4;
      PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury<PayBillsFilter.curyInfoID>(cache, (object) current, valueOrDefault2, out local);
      adj.CuryAdjgAmt = new Decimal?(num4);
      jointPayeeBalance[adj.JointPayeeID.Value].AmountInBase -= valueOrDefault1;
      billBalance[keyForBillBalance].AmountInBase -= valueOrDefault1;
    }
    try
    {
      this.skipAmountToPayVerification = true;
      adj = ((PXSelectBase<APAdjust>) this.Base.APDocumentList).Update(adj);
    }
    finally
    {
      this.skipAmountToPayVerification = false;
    }
    bool? nullable3 = data.GetJointPayee(adj.JointPayeeID).IsMainPayee;
    if (nullable3.GetValueOrDefault())
    {
      if (data.Invoice.CuryID == ((PXSelectBase<PayBillsFilter>) this.Base.Filter).Current.CuryID)
      {
        APAdjust apAdjust = adj;
        nullable3 = data.Invoice.PaymentsByLinesAllowed;
        Decimal? nullable4 = new Decimal?(nullable3.GetValueOrDefault() ? data.Tran.CuryCashDiscBal.GetValueOrDefault() : data.Invoice.CuryDiscBal.GetValueOrDefault());
        apAdjust.CuryAdjgDiscAmt = nullable4;
      }
      else
      {
        nullable3 = data.Invoice.PaymentsByLinesAllowed;
        Decimal curyval;
        PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury<PayBillsFilter.curyInfoID>(((PXSelectBase) this.Base.Filter).Cache, (object) ((PXSelectBase<PayBillsFilter>) this.Base.Filter).Current, nullable3.GetValueOrDefault() ? data.Tran.CashDiscBal.GetValueOrDefault() : data.Invoice.DiscBal.GetValueOrDefault(), out curyval);
        adj.CuryAdjgDiscAmt = new Decimal?(curyval);
      }
      adj = ((PXSelectBase<APAdjust>) this.Base.APDocumentList).Update(adj);
    }
    ((PXSelectBase) this.Base.APDocumentList).Cache.SetValue<ApAdjustExt.isAmountPaidCalculated>((object) adj, (object) true);
  }

  private Decimal? GetAmountToPay(
    int? jointPayeeID,
    ApPayBillsJointCheckExt.PayBillData data,
    Decimal available,
    Dictionary<int, ApPayBillsJointCheckExt.BalanceAmount> jointPayeeBalance)
  {
    foreach (JointPayee jointPayee in (IEnumerable<JointPayee>) data.JointPayees.Where<JointPayee>((Func<JointPayee, bool>) (payee => !payee.IsMainPayee.GetValueOrDefault())).OrderBy<JointPayee, int>((Func<JointPayee, int>) (payee => payee.JointPayeeId.GetValueOrDefault())))
    {
      Decimal amount = jointPayeeBalance[jointPayee.JointPayeeId.Value].Amount;
      int? jointPayeeId = jointPayee.JointPayeeId;
      int? nullable = jointPayeeID;
      if (jointPayeeId.GetValueOrDefault() == nullable.GetValueOrDefault() & jointPayeeId.HasValue == nullable.HasValue)
        return available > amount ? new Decimal?(amount) : new Decimal?(available);
      available = Math.Max(0M, available - amount);
    }
    if (available <= 0M)
      return new Decimal?(0M);
    JointPayee jointPayee1 = data.JointPayees.Where<JointPayee>((Func<JointPayee, bool>) (payee => payee.IsMainPayee.GetValueOrDefault())).SingleOrDefault<JointPayee>();
    if (jointPayee1 != null)
    {
      int? jointPayeeId = jointPayee1.JointPayeeId;
      int? nullable = jointPayeeID;
      if (jointPayeeId.GetValueOrDefault() == nullable.GetValueOrDefault() & jointPayeeId.HasValue == nullable.HasValue)
      {
        Decimal amount = jointPayeeBalance[jointPayee1.JointPayeeId.Value].Amount;
        return available > amount ? new Decimal?(amount) : new Decimal?(available);
      }
    }
    return new Decimal?(0M);
  }

  private Decimal? GetAmountToPayInBase(
    int? jointPayeeID,
    ApPayBillsJointCheckExt.PayBillData data,
    Decimal available,
    Dictionary<int, ApPayBillsJointCheckExt.BalanceAmount> jointPayeeBalance)
  {
    foreach (JointPayee jointPayee in (IEnumerable<JointPayee>) data.JointPayees.Where<JointPayee>((Func<JointPayee, bool>) (payee => !payee.IsMainPayee.GetValueOrDefault())).OrderBy<JointPayee, int>((Func<JointPayee, int>) (payee => payee.JointPayeeId.GetValueOrDefault())))
    {
      Decimal amountInBase = jointPayeeBalance[jointPayee.JointPayeeId.Value].AmountInBase;
      int? jointPayeeId = jointPayee.JointPayeeId;
      int? nullable = jointPayeeID;
      if (jointPayeeId.GetValueOrDefault() == nullable.GetValueOrDefault() & jointPayeeId.HasValue == nullable.HasValue)
        return available > amountInBase ? new Decimal?(amountInBase) : new Decimal?(available);
      available = Math.Max(0M, available - amountInBase);
    }
    if (available <= 0M)
      return new Decimal?(0M);
    JointPayee jointPayee1 = data.JointPayees.Where<JointPayee>((Func<JointPayee, bool>) (payee => payee.IsMainPayee.GetValueOrDefault())).SingleOrDefault<JointPayee>();
    if (jointPayee1 != null)
    {
      int? jointPayeeId = jointPayee1.JointPayeeId;
      int? nullable = jointPayeeID;
      if (jointPayeeId.GetValueOrDefault() == nullable.GetValueOrDefault() & jointPayeeId.HasValue == nullable.HasValue)
      {
        Decimal amountInBase = jointPayeeBalance[jointPayee1.JointPayeeId.Value].AmountInBase;
        return available > amountInBase ? new Decimal?(amountInBase) : new Decimal?(available);
      }
    }
    return new Decimal?(0M);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgAmt> e)
  {
    if (this.skipAmountToPayVerification)
      return;
    ApPayBillsJointCheckExt.PayBillData payBillData = this.SelectPayBillDataFromDatabase(e.Row.AdjdDocType, e.Row.AdjdRefNbr, e.Row.AdjdLineNbr);
    if (payBillData == null)
      return;
    bool? nullable1 = payBillData.Invoice.IsJointPayees;
    if (!nullable1.GetValueOrDefault())
      return;
    ApAdjustExt extension = ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgAmt>>) e).Cache.GetExtension<ApAdjustExt>((object) e.Row);
    if (extension == null)
      return;
    if (payBillData.Invoice.CuryID == ((PXSelectBase<PayBillsFilter>) this.Base.Filter).Current.CuryID)
    {
      JointPayee jointPayee = payBillData.GetJointPayee(e.Row.JointPayeeID);
      nullable1 = jointPayee.IsMainPayee;
      Decimal? nullable2;
      Decimal? nullable3;
      Decimal? nullable4;
      if (nullable1.GetValueOrDefault())
      {
        nullable2 = jointPayee.CuryJointBalance;
        nullable3 = e.Row.CuryAdjgDiscAmt;
        Decimal valueOrDefault = nullable3.GetValueOrDefault();
        Decimal? nullable5;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          nullable5 = nullable3;
        }
        else
          nullable5 = new Decimal?(nullable2.GetValueOrDefault() - valueOrDefault);
        nullable4 = nullable5;
      }
      else
        nullable4 = extension.CuryJointBalance;
      nullable2 = nullable4;
      nullable3 = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgAmt>, APAdjust, object>) e).NewValue;
      if (nullable2.GetValueOrDefault() < nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
        throw new PXSetPropertyException<APAdjust.curyAdjgAmt>("The amount must be less than or equal to {0}.", new object[1]
        {
          (object) nullable4
        });
      Decimal siblingsAmountPay = this.GetSiblingsAmountPay(e.Row);
      nullable1 = payBillData.Invoice.PaymentsByLinesAllowed;
      Decimal valueOrDefault1;
      if (!nullable1.GetValueOrDefault())
      {
        nullable3 = payBillData.Invoice.CuryDocBal;
        valueOrDefault1 = nullable3.GetValueOrDefault();
      }
      else
      {
        nullable3 = payBillData.Tran.CuryTranBal;
        valueOrDefault1 = nullable3.GetValueOrDefault();
      }
      Decimal num1 = valueOrDefault1;
      nullable3 = e.Row.CuryAdjgDiscAmt;
      Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
      Decimal num2 = num1 - valueOrDefault2;
      Decimal num3 = siblingsAmountPay;
      nullable3 = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgAmt>, APAdjust, object>) e).NewValue;
      Decimal valueOrDefault3 = nullable3.GetValueOrDefault();
      if (num3 + valueOrDefault3 > num2)
        throw new PXSetPropertyException<APAdjust.curyAdjgAmt>("The specified Amount Paid exceeds the balance of the AP bill line. Amount Paid must be equal to or less than {0}.", new object[1]
        {
          (object) (num2 - siblingsAmountPay).ToString("n2")
        });
    }
    else
    {
      JointPayee jointPayee = payBillData.GetJointPayee(e.Row.JointPayeeID);
      nullable1 = jointPayee.IsMainPayee;
      Decimal? nullable6 = !nullable1.GetValueOrDefault() ? extension.JointBalance : jointPayee.JointBalance;
      Decimal? nullable7 = nullable6;
      Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgAmt>, APAdjust, object>) e).NewValue;
      if (nullable7.GetValueOrDefault() < newValue.GetValueOrDefault() & nullable7.HasValue & newValue.HasValue)
        throw new PXSetPropertyException<APAdjust.curyAdjgAmt>("The amount must be less than or equal to {0}.", new object[1]
        {
          (object) nullable6
        });
      Decimal siblingsAmountPayInBase = this.GetSiblingsAmountPayInBase(e.Row);
      nullable1 = payBillData.Invoice.PaymentsByLinesAllowed;
      Decimal? nullable8;
      Decimal valueOrDefault4;
      if (!nullable1.GetValueOrDefault())
      {
        nullable8 = payBillData.Invoice.DocBal;
        valueOrDefault4 = nullable8.GetValueOrDefault();
      }
      else
      {
        nullable8 = payBillData.Tran.TranBal;
        valueOrDefault4 = nullable8.GetValueOrDefault();
      }
      Decimal num4 = valueOrDefault4;
      nullable8 = e.Row.CuryAdjgDiscAmt;
      Decimal valueOrDefault5 = nullable8.GetValueOrDefault();
      Decimal num5 = num4 - valueOrDefault5;
      Decimal num6 = siblingsAmountPayInBase;
      nullable8 = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgAmt>, APAdjust, object>) e).NewValue;
      Decimal valueOrDefault6 = nullable8.GetValueOrDefault();
      if (num6 + valueOrDefault6 > num5)
        throw new PXSetPropertyException<APAdjust.curyAdjgAmt>("The specified Amount Paid exceeds the balance of the AP bill line. Amount Paid must be equal to or less than {0}.", new object[1]
        {
          (object) (num5 - siblingsAmountPayInBase).ToString("n2")
        });
    }
  }

  private Decimal GetSiblingsAmountPay(APAdjust adjust)
  {
    Decimal siblingsAmountPay = 0M;
    foreach (APAdjust apAdjust in ((PXSelectBase) this.Base.APDocumentList).Cache.Inserted)
    {
      if (apAdjust.AdjdDocType == adjust.AdjdDocType && apAdjust.AdjdRefNbr == adjust.AdjdRefNbr)
      {
        int? nullable1 = apAdjust.AdjdLineNbr;
        int? nullable2 = adjust.AdjdLineNbr;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = apAdjust.AdjNbr;
          nullable1 = adjust.AdjNbr;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            siblingsAmountPay += apAdjust.CuryAdjgAmt.GetValueOrDefault();
        }
      }
    }
    return siblingsAmountPay;
  }

  private Decimal GetSiblingsAmountPayInBase(APAdjust adjust)
  {
    Decimal siblingsAmountPayInBase = 0M;
    foreach (APAdjust apAdjust in ((PXSelectBase) this.Base.APDocumentList).Cache.Inserted)
    {
      if (apAdjust.AdjdDocType == adjust.AdjdDocType && apAdjust.AdjdRefNbr == adjust.AdjdRefNbr)
      {
        int? nullable1 = apAdjust.AdjdLineNbr;
        int? nullable2 = adjust.AdjdLineNbr;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = apAdjust.AdjNbr;
          nullable1 = adjust.AdjNbr;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            siblingsAmountPayInBase += apAdjust.AdjAmt.GetValueOrDefault();
        }
      }
    }
    return siblingsAmountPayInBase;
  }

  protected virtual void _(PX.Data.Events.RowSelected<APAdjust> e)
  {
    if (e.Row == null || !((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.ShouldWarnOnBillEntry.GetValueOrDefault())
      return;
    ApPayBillsJointCheckExt.PayBillData payBillData = this.SelectPayBillData(e.Row.AdjdDocType, e.Row.AdjdRefNbr, e.Row.AdjdLineNbr);
    if (payBillData != null && this.ContainsOutstandingLienWaversByVendor(payBillData.Tran.ProjectID, e.Row.VendorID))
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APAdjust>>) e).Cache.RaiseExceptionHandling<APAdjust.vendorID>((object) e.Row, (object) e.Row.VendorID, (Exception) new PXSetPropertyException<APAdjust.vendorID>("The vendor has at least one outstanding lien waiver.  ", (PXErrorLevel) 2));
    if (payBillData == null || !payBillData.Invoice.IsJointPayees.GetValueOrDefault())
      return;
    JointPayee jointPayee = payBillData.GetJointPayee(e.Row.AdjNbr);
    if (jointPayee == null || !jointPayee.JointPayeeInternalId.HasValue || !this.ContainsOutstandingLienWaversByJointPayee(payBillData.Tran.ProjectID, jointPayee.JointPayeeInternalId))
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APAdjust>>) e).Cache.RaiseExceptionHandling<ApAdjustExt.jointPayeeExternalName>((object) e.Row, (object) jointPayee.JointPayeeExternalName, (Exception) new PXSetPropertyException<ApAdjustExt.jointPayeeExternalName>("The joint payee has at least one outstanding lien waiver.", (PXErrorLevel) 2));
  }

  private int? LienWaiverDocType
  {
    get
    {
      if (!this.lienWaiverDocType.HasValue)
        this.lienWaiverDocType = this.GetLienWaiverDocumentType();
      return this.lienWaiverDocType;
    }
  }

  private int? GetLienWaiverDocumentType()
  {
    return ((PXSelectBase<ComplianceAttributeType>) new PXSelect<ComplianceAttributeType, Where<ComplianceAttributeType.type, Equal<Required<ComplianceAttributeType.type>>>>((PXGraph) this.Base)).SelectSingle(new object[1]
    {
      (object) "Lien Waiver"
    })?.ComplianceAttributeTypeID;
  }

  private bool ContainsOutstandingLienWaversByVendor(int? projectID, int? vendorID)
  {
    return PXResultset<ComplianceDocument>.op_Implicit(((PXSelectBase<ComplianceDocument>) new PXSelectReadonly<ComplianceDocument, Where<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.vendorID, Equal<Required<ComplianceDocument.vendorID>>, And<ComplianceDocument.projectID, Equal<Required<ComplianceDocument.projectID>>, And<ComplianceDocument.throughDate, Less<Required<ComplianceDocument.throughDate>>, And<ComplianceDocument.received, NotEqual<True>>>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[4]
    {
      (object) this.LienWaiverDocType,
      (object) vendorID,
      (object) projectID,
      (object) ((PXGraph) this.Base).Accessinfo.BusinessDate
    })) != null;
  }

  private bool ContainsOutstandingLienWaversByJointPayee(int? projectID, int? vendorID)
  {
    return PXResultset<ComplianceDocument>.op_Implicit(((PXSelectBase<ComplianceDocument>) new PXSelectReadonly<ComplianceDocument, Where<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.jointVendorInternalId, Equal<Required<ComplianceDocument.jointVendorInternalId>>, And<ComplianceDocument.projectID, Equal<Required<ComplianceDocument.projectID>>, And<ComplianceDocument.throughDate, Less<Required<ComplianceDocument.throughDate>>, And<ComplianceDocument.received, NotEqual<True>>>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[4]
    {
      (object) this.LienWaiverDocType,
      (object) vendorID,
      (object) projectID,
      (object) ((PXGraph) this.Base).Accessinfo.BusinessDate
    })) != null;
  }

  protected ApPayBillsJointCheckExt.PayBillData SelectPayBillData(
    string docType,
    string refNbr,
    int? lineNbr)
  {
    if (this.billDataStore == null)
      return this.SelectPayBillDataFromDatabase(docType, refNbr, lineNbr);
    ApPayBillsJointCheckExt.PayBillData payBillData;
    this.billDataStore.TryGetValue(ApPayBillsJointCheckExt.GetKeyForPayData(docType, refNbr, lineNbr.GetValueOrDefault()), out payBillData);
    return payBillData;
  }

  protected ApPayBillsJointCheckExt.PayBillData SelectPayBillDataFromDatabase(
    string docType,
    string refNbr,
    int? lineNbr)
  {
    ApPayBillsJointCheckExt.PayBillData payBillData = (ApPayBillsJointCheckExt.PayBillData) null;
    foreach (PXResult<APInvoice, APTran, PX.Objects.CM.Extensions.CurrencyInfo, JointPayeePerDoc, JointPayeePerLine> pxResult in ((PXSelectBase<APInvoice>) this.APInvoiceJointPayeeLines).Select(new object[3]
    {
      (object) lineNbr,
      (object) docType,
      (object) refNbr
    }))
    {
      APInvoice apInvoice;
      APTran apTran;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo;
      JointPayeePerDoc jointPayeePerDoc1;
      JointPayeePerLine jointPayeePerLine1;
      pxResult.Deconstruct(ref apInvoice, ref apTran, ref currencyInfo, ref jointPayeePerDoc1, ref jointPayeePerLine1);
      APInvoice invoice = apInvoice;
      APTran tran = apTran;
      PX.Objects.CM.Extensions.CurrencyInfo curyInfo = currencyInfo;
      JointPayeePerDoc jointPayeePerDoc2 = jointPayeePerDoc1;
      JointPayeePerLine jointPayeePerLine2 = jointPayeePerLine1;
      if (payBillData == null)
        payBillData = new ApPayBillsJointCheckExt.PayBillData(invoice, tran, curyInfo);
      int? jointPayeeId;
      if (invoice.PaymentsByLinesAllowed.GetValueOrDefault())
      {
        jointPayeeId = jointPayeePerLine2.JointPayeeId;
        if (jointPayeeId.HasValue)
          payBillData.JointPayees.Add((JointPayee) jointPayeePerLine2);
      }
      else
      {
        jointPayeeId = jointPayeePerDoc2.JointPayeeId;
        if (jointPayeeId.HasValue)
          payBillData.JointPayees.Add((JointPayee) jointPayeePerDoc2);
      }
    }
    return payBillData;
  }

  private class BalanceAmount
  {
    public Decimal Amount { get; set; }

    public Decimal AmountInBase { get; set; }

    public BalanceAmount(Decimal amount, Decimal amountInBase)
    {
      this.Amount = amount;
      this.AmountInBase = amountInBase;
    }
  }

  public class PayBillData
  {
    public APInvoice Invoice { get; private set; }

    public APTran Tran { get; private set; }

    public PX.Objects.CM.Extensions.CurrencyInfo CuryInfo { get; private set; }

    public List<JointPayee> JointPayees { get; private set; }

    public PayBillData(APInvoice invoice, APTran tran, PX.Objects.CM.Extensions.CurrencyInfo curyInfo)
    {
      this.Invoice = invoice;
      this.Tran = tran;
      this.CuryInfo = curyInfo;
      this.JointPayees = new List<JointPayee>();
    }

    public string GetJointPayeeExternalName(int? payeeId)
    {
      if (this.JointPayees.Where<JointPayee>((Func<JointPayee, bool>) (j =>
      {
        int? jointPayeeId = j.JointPayeeId;
        int? nullable = payeeId;
        return jointPayeeId.GetValueOrDefault() == nullable.GetValueOrDefault() & jointPayeeId.HasValue == nullable.HasValue;
      })).SingleOrDefault<JointPayee>() is JointPayeePerLine jointPayeePerLine)
        return jointPayeePerLine.JointPayeeExternalName ?? jointPayeePerLine.JointVendorName;
      return this.JointPayees.Where<JointPayee>((Func<JointPayee, bool>) (j =>
      {
        int? jointPayeeId = j.JointPayeeId;
        int? nullable = payeeId;
        return jointPayeeId.GetValueOrDefault() == nullable.GetValueOrDefault() & jointPayeeId.HasValue == nullable.HasValue;
      })).SingleOrDefault<JointPayee>() is JointPayeePerDoc jointPayeePerDoc ? jointPayeePerDoc.JointPayeeExternalName ?? jointPayeePerDoc.JointVendorName : (string) null;
    }

    public JointPayee GetJointPayee(int? payeeId)
    {
      return this.JointPayees.Where<JointPayee>((Func<JointPayee, bool>) (j =>
      {
        int? jointPayeeId = j.JointPayeeId;
        int? nullable = payeeId;
        return jointPayeeId.GetValueOrDefault() == nullable.GetValueOrDefault() & jointPayeeId.HasValue == nullable.HasValue;
      })).SingleOrDefault<JointPayee>();
    }
  }
}
