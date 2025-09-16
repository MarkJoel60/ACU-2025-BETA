// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.APPaymentEntryJointCheck
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable enable
namespace PX.Objects.CN.JointChecks;

public class APPaymentEntryJointCheck : PXGraphExtension<
#nullable disable
APPaymentEntry>
{
  public PXSelect<JointPayeeDisplay, Where<JointPayeeDisplay.jointPayeeId, Equal<Current<APPayment.jointPayeeID>>>> SelectedJointPayee;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<JointPayeePayment, InnerJoin<JointPayee, On<JointPayee.jointPayeeId, Equal<JointPayeePayment.jointPayeeId>>>, Where<JointPayeePayment.paymentDocType, Equal<Current<APPayment.docType>>, And<JointPayeePayment.paymentRefNbr, Equal<Current<APPayment.refNbr>>, And<JointPayee.isMainPayee, Equal<False>>>>> JointPayments;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentPaymentReference> LinkToPayments;
  public PXFilter<APPaymentEntryJointCheck.JointPayeeFilter> BillWithJointPayeeFilter;
  [PXCopyPasteHiddenView]
  [PXVirtualDAC]
  public PXSelect<APPaymentEntryJointCheck.JointPayeeRecord, Where<APPaymentEntryJointCheck.JointPayeeRecord.curyBalance, NotEqual<Zero>>, OrderBy<Desc<APPaymentEntryJointCheck.JointPayeeRecord.refNbr, Asc<APPaymentEntryJointCheck.JointPayeeRecord.lineNbr, Asc<APPaymentEntryJointCheck.JointPayeeRecord.jointPayeeID>>>>> BillWithJointPayee;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocument> Compliance;
  public PXAction<APPayment> addJointPayeesFromDialog;
  public PXAction<APPayment> addJointPayee;
  private JointPayeePayment jointPayeePayment;

  [PXMergeAttributes]
  [PXDBDefault(typeof (APPayment.docType))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<JointPayeePayment.paymentDocType> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (APPayment.refNbr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<JointPayeePayment.paymentRefNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [APInvoiceType.AdjdRefNbr(typeof (Search5<APAdjust.APInvoice.refNbr, LeftJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APAdjust.APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APAdjust.APInvoice.refNbr>, And<APAdjust.released, Equal<False>, And<APAdjust.voided, Equal<False>, And<Where<APAdjust.adjgDocType, NotEqual<Current<APPayment.docType>>, Or<APAdjust.adjgRefNbr, NotEqual<Current<APPayment.refNbr>>>>>>>>>, LeftJoin<APAdjust2, On<APAdjust2.adjgDocType, Equal<APAdjust.APInvoice.docType>, And<APAdjust2.adjgRefNbr, Equal<APAdjust.APInvoice.refNbr>, And<APAdjust2.released, Equal<False>, And<APAdjust2.voided, Equal<False>>>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<APAdjust.APInvoice.docType>, And<APPayment.refNbr, Equal<APAdjust.APInvoice.refNbr>, And<Where<APPayment.docType, Equal<APDocType.prepayment>, Or<APPayment.docType, Equal<APDocType.debitAdj>>>>>>>>>, Where<APAdjust.APInvoice.vendorID, Equal<Optional<APPayment.vendorID>>, And<APAdjust.APInvoice.docType, Equal<Optional<APAdjust.adjdDocType>>, And2<Where<APAdjust.APInvoice.released, Equal<True>, Or<APRegister.prebooked, Equal<True>>>, And<APAdjust.APInvoice.openDoc, Equal<True>, And<APRegister.hold, Equal<False>, And2<Where<APAdjust.adjgRefNbr, IsNull, Or<APAdjust.APInvoice.isJointPayees, Equal<True>>>, And<APAdjust2.adjdRefNbr, IsNull, And2<Where<APPayment.refNbr, IsNull, And<Current<APPayment.docType>, NotEqual<APDocType.refund>, Or<APPayment.refNbr, IsNotNull, And<Current<APPayment.docType>, Equal<APDocType.refund>, Or<APPayment.docType, Equal<APDocType.debitAdj>, And<Current<APPayment.docType>, Equal<APDocType.check>, Or<APPayment.docType, Equal<APDocType.debitAdj>, And<Current<APPayment.docType>, Equal<APDocType.voidCheck>>>>>>>>>, And2<Where<APAdjust.APInvoice.docDate, LessEqual<Current<APPayment.adjDate>>, And<APAdjust.APInvoice.tranPeriodID, LessEqual<Current<APPayment.adjTranPeriodID>>, Or<Current<APPayment.adjTranPeriodID>, IsNull, Or<Current<APPayment.docType>, Equal<APDocType.check>, And<Current<APSetup.earlyChecks>, Equal<True>, Or<Current<APPayment.docType>, Equal<APDocType.voidCheck>, And<Current<APSetup.earlyChecks>, Equal<True>, Or<Current<APPayment.docType>, Equal<APDocType.prepayment>, And<Current<APSetup.earlyChecks>, Equal<True>>>>>>>>>>, And2<Where<Current<APSetup.migrationMode>, NotEqual<True>, Or<APAdjust.APInvoice.isMigratedRecord, Equal<Current<APRegister.isMigratedRecord>>>>, And<Where<APAdjust.APInvoice.pendingPPD, NotEqual<True>, Or<Current<APRegister.pendingPPD>, Equal<True>>>>>>>>>>>>>>, Aggregate<GroupBy<APAdjust.APInvoice.docType, GroupBy<APAdjust.APInvoice.refNbr>>>>), Filterable = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<APAdjust.adjdRefNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXDefault(typeof (Switch<Case<Where<Selector<APAdjust.adjdRefNbr, APAdjust.APInvoice.paymentsByLinesAllowed>, NotEqual<True>>, int0>, Null>))]
  [APInvoiceType.AdjdLineNbr]
  protected virtual void _(PX.Data.Events.CacheAttached<APAdjust.adjdLineNbr> e)
  {
  }

  public bool IsPreparePaymentsMassProcessing { get; set; }

  public virtual IEnumerable billWithJointPayee()
  {
    List<APPaymentEntryJointCheck.JointPayeeRecord> jointPayeeRecordList1 = new List<APPaymentEntryJointCheck.JointPayeeRecord>(202);
    bool flag = false;
    foreach (APPaymentEntryJointCheck.JointPayeeRecord jointPayeeRecord in ((PXSelectBase) this.BillWithJointPayee).Cache.Cached)
    {
      if (((PXSelectBase) this.BillWithJointPayee).Cache.GetStatus((object) jointPayeeRecord) == 2)
      {
        flag = true;
        jointPayeeRecordList1.Add(jointPayeeRecord);
      }
    }
    if (!flag)
    {
      PXView pxView = new PXView((PXGraph) this.Base, false, (BqlCommand) new Select2<APInvoice, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.curyTranBal, Greater<decimal0>>>>>, LeftJoin<JointPayeePerDoc, On<Where2<Where<JointPayeePerDoc.aPDocType, Equal<APInvoice.docType>, And<JointPayeePerDoc.aPRefNbr, Equal<APInvoice.refNbr>, And<APInvoice.isRetainageDocument, Equal<False>, And<JointPayeePerDoc.isMainPayee, Equal<False>>>>>, Or<Where<JointPayeePerDoc.aPDocType, Equal<APInvoice.origDocType>, And<JointPayeePerDoc.aPRefNbr, Equal<APInvoice.origRefNbr>, And<APInvoice.isRetainageDocument, Equal<True>, And<JointPayeePerDoc.isMainPayee, Equal<False>>>>>>>>, LeftJoin<JointPayeePerLine, On<Where2<Where<JointPayeePerLine.aPDocType, Equal<APInvoice.docType>, And<JointPayeePerLine.aPRefNbr, Equal<APInvoice.refNbr>, And<JointPayeePerLine.aPLineNbr, Equal<APTran.lineNbr>, And<APInvoice.isRetainageDocument, Equal<False>, And<JointPayeePerLine.isMainPayee, Equal<False>>>>>>, Or<Where<JointPayeePerLine.aPDocType, Equal<APInvoice.origDocType>, And<JointPayeePerLine.aPRefNbr, Equal<APInvoice.origRefNbr>, And<JointPayeePerLine.aPLineNbr, Equal<APTran.lineNbr>, And<APInvoice.isRetainageDocument, Equal<True>, And<JointPayeePerLine.isMainPayee, Equal<False>>>>>>>>>>>>, Where<APInvoice.vendorID, Equal<Current<APPayment.vendorID>>, And<APInvoice.docType, Equal<Current<APPaymentEntryJointCheck.JointPayeeFilter.docType>>, And<APInvoice.curyDocBal, Greater<decimal0>, And<APInvoice.isJointPayees, Equal<True>, And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>, And<Where<Current<APPaymentEntryJointCheck.JointPayeeFilter.refNbr>, IsNull, Or<Current<APPaymentEntryJointCheck.JointPayeeFilter.refNbr>, Equal<APInvoice.refNbr>>>>>>>>>>());
      int startRow = PXView.StartRow;
      int num = 0;
      object[] currents = PXView.Currents;
      object[] parameters = PXView.Parameters;
      object[] searches = PXView.Searches;
      string[] sortColumns = PXView.SortColumns;
      bool[] descendings = PXView.Descendings;
      PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
      ref int local1 = ref startRow;
      int maximumRows = PXView.MaximumRows;
      ref int local2 = ref num;
      List<object> objectList = pxView.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
      PXView.StartRow = 0;
      foreach (PXResult<APInvoice, APTran, JointPayeePerDoc, JointPayeePerLine> pxResult in objectList)
      {
        APInvoice apInvoice = PXResult<APInvoice, APTran, JointPayeePerDoc, JointPayeePerLine>.op_Implicit(pxResult);
        APTran apTran = PXResult<APInvoice, APTran, JointPayeePerDoc, JointPayeePerLine>.op_Implicit(pxResult);
        JointPayeePerDoc jointPayeePerDoc = PXResult<APInvoice, APTran, JointPayeePerDoc, JointPayeePerLine>.op_Implicit(pxResult);
        JointPayeePerLine jointPayeePerLine = PXResult<APInvoice, APTran, JointPayeePerDoc, JointPayeePerLine>.op_Implicit(pxResult);
        APPaymentEntryJointCheck.JointPayeeRecord jointPayeeRecord1 = new APPaymentEntryJointCheck.JointPayeeRecord();
        jointPayeeRecord1.DocType = apInvoice.DocType;
        jointPayeeRecord1.RefNbr = apInvoice.RefNbr;
        Decimal? nullable1;
        Decimal? nullable2;
        if (apInvoice.PaymentsByLinesAllowed.GetValueOrDefault())
        {
          jointPayeeRecord1.JointPayeeID = jointPayeePerLine.JointPayeeId;
          jointPayeeRecord1.LineNbr = apTran.LineNbr;
          jointPayeeRecord1.Name = jointPayeePerLine.JointPayeeExternalName ?? jointPayeePerLine.JointVendorName;
          jointPayeeRecord1.CuryJointAmountOwed = jointPayeePerLine.CuryJointAmountOwed;
          jointPayeeRecord1.JointAmountOwed = jointPayeePerLine.JointAmountOwed;
          jointPayeeRecord1.CuryJointAmountPaid = jointPayeePerLine.CuryJointAmountPaid;
          jointPayeeRecord1.JointAmountPaid = jointPayeePerLine.JointAmountPaid;
          APPaymentEntryJointCheck.JointPayeeRecord jointPayeeRecord2 = jointPayeeRecord1;
          nullable1 = jointPayeeRecord1.CuryJointAmountOwed;
          nullable2 = jointPayeeRecord1.CuryJointAmountPaid;
          Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          jointPayeeRecord2.CuryBalance = nullable3;
          APPaymentEntryJointCheck.JointPayeeRecord jointPayeeRecord3 = jointPayeeRecord1;
          nullable2 = jointPayeeRecord1.JointAmountOwed;
          nullable1 = jointPayeeRecord1.JointAmountPaid;
          Decimal? nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
          jointPayeeRecord3.Balance = nullable4;
        }
        else
        {
          jointPayeeRecord1.JointPayeeID = jointPayeePerDoc.JointPayeeId;
          jointPayeeRecord1.LineNbr = new int?(0);
          jointPayeeRecord1.Name = jointPayeePerDoc.JointPayeeExternalName ?? jointPayeePerDoc.JointVendorName;
          jointPayeeRecord1.CuryJointAmountOwed = jointPayeePerDoc.CuryJointAmountOwed;
          jointPayeeRecord1.JointAmountOwed = jointPayeePerDoc.JointAmountOwed;
          jointPayeeRecord1.CuryJointAmountPaid = jointPayeePerDoc.CuryJointAmountPaid;
          jointPayeeRecord1.JointAmountPaid = jointPayeePerDoc.JointAmountPaid;
          APPaymentEntryJointCheck.JointPayeeRecord jointPayeeRecord4 = jointPayeeRecord1;
          nullable1 = jointPayeeRecord1.CuryJointAmountOwed;
          nullable2 = jointPayeeRecord1.CuryJointAmountPaid;
          Decimal? nullable5 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          jointPayeeRecord4.CuryBalance = nullable5;
          APPaymentEntryJointCheck.JointPayeeRecord jointPayeeRecord5 = jointPayeeRecord1;
          nullable2 = jointPayeeRecord1.JointAmountOwed;
          nullable1 = jointPayeeRecord1.JointAmountPaid;
          Decimal? nullable6 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
          jointPayeeRecord5.Balance = nullable6;
        }
        if (((PXSelectBase<APPaymentEntryJointCheck.JointPayeeRecord>) this.BillWithJointPayee).Locate(jointPayeeRecord1) == null)
          jointPayeeRecordList1.Add(((PXSelectBase<APPaymentEntryJointCheck.JointPayeeRecord>) this.BillWithJointPayee).Insert(jointPayeeRecord1));
      }
    }
    HashSet<string> stringSet = new HashSet<string>();
    foreach (PXResult<APAdjust> pxResult in ((PXSelectBase<APAdjust>) this.Base.Adjustments).Select(Array.Empty<object>()))
    {
      APAdjust apAdjust = PXResult<APAdjust>.op_Implicit(pxResult);
      if (apAdjust.Voided.GetValueOrDefault())
        stringSet.Add($"{apAdjust.AdjdDocType}.{apAdjust.AdjdRefNbr}.{apAdjust.AdjdLineNbr}");
    }
    List<APPaymentEntryJointCheck.JointPayeeRecord> jointPayeeRecordList2 = new List<APPaymentEntryJointCheck.JointPayeeRecord>();
    foreach (APPaymentEntryJointCheck.JointPayeeRecord jointPayeeRecord in jointPayeeRecordList1)
    {
      string str = $"{jointPayeeRecord.DocType}.{jointPayeeRecord.RefNbr}.{jointPayeeRecord.LineNbr}";
      if (stringSet.Contains(str))
        jointPayeeRecordList2.Add(jointPayeeRecord);
    }
    foreach (APPaymentEntryJointCheck.JointPayeeRecord jointPayeeRecord in jointPayeeRecordList2)
      jointPayeeRecordList1.Remove(jointPayeeRecord);
    return (IEnumerable) jointPayeeRecordList1;
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXUIField(DisplayName = "Add Joint Payee")]
  [PXButton]
  public IEnumerable AddJointPayeesFromDialog(PXAdapter adapter)
  {
    if (((PXSelectBase) this.BillWithJointPayee).View.AskExt() == 1)
      this.AddSelectedJointPayees();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Add Joint Payee")]
  [PXButton]
  public IEnumerable AddJointPayee(PXAdapter adapter)
  {
    this.AddSelectedJointPayees();
    return adapter.Get();
  }

  protected virtual void AddSelectedJointPayees()
  {
    HashSet<string> stringSet = new HashSet<string>();
    foreach (PXResult<APAdjust> pxResult in ((PXSelectBase<APAdjust>) this.Base.Adjustments).Select(Array.Empty<object>()))
    {
      APAdjust apAdjust = PXResult<APAdjust>.op_Implicit(pxResult);
      string str = $"{apAdjust.AdjdDocType}.{apAdjust.AdjdRefNbr}.{apAdjust.AdjdLineNbr.GetValueOrDefault()}.{apAdjust.JointPayeeID.GetValueOrDefault()}";
      stringSet.Add(str);
    }
    foreach (APPaymentEntryJointCheck.JointPayeeRecord jointPayeeRecord in ((PXSelectBase) this.BillWithJointPayee).Cache.Cached)
    {
      if (jointPayeeRecord.Selected.GetValueOrDefault())
      {
        string str = $"{jointPayeeRecord.DocType}.{jointPayeeRecord.RefNbr}.{jointPayeeRecord.LineNbr.GetValueOrDefault()}.{jointPayeeRecord.JointPayeeID.GetValueOrDefault()}";
        if (!stringSet.Contains(str))
        {
          if (!((PXSelectBase<APPayment>) this.Base.Document).Current.JointPayeeID.HasValue)
          {
            ((PXSelectBase<APPayment>) this.Base.Document).Current.JointPayeeID = jointPayeeRecord.JointPayeeID;
            ((PXSelectBase<APPayment>) this.Base.Document).UpdateCurrent();
          }
          APAdjust apAdjust1 = ((PXSelectBase<APAdjust>) this.Base.Adjustments).Insert(new APAdjust()
          {
            AdjdDocType = jointPayeeRecord.DocType,
            AdjdRefNbr = jointPayeeRecord.RefNbr,
            AdjdLineNbr = jointPayeeRecord.LineNbr,
            JointPayeeID = jointPayeeRecord.JointPayeeID
          });
          if (apAdjust1 != null)
          {
            JointPayee jointPayee = JointPayee.PK.Find((PXGraph) this.Base, apAdjust1.JointPayeeID);
            if (jointPayee != null)
            {
              Decimal? nullable1 = jointPayee.CuryJointBalance;
              Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
              if (apAdjust1.AdjdCuryID != ((PXSelectBase<APPayment>) this.Base.Document).Current.CuryID)
              {
                PXCache cache = ((PXSelectBase) this.Base.Adjustments).Cache;
                APAdjust row = apAdjust1;
                nullable1 = jointPayee.JointBalance;
                Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
                ref Decimal local = ref valueOrDefault1;
                PX.Objects.CM.PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury<APAdjust.adjgCuryInfoID>(cache, (object) row, valueOrDefault2, out local);
              }
              APAdjust apAdjust2 = apAdjust1;
              nullable1 = apAdjust1.CuryAdjgAmt;
              Decimal? nullable2 = new Decimal?(Math.Min(nullable1.GetValueOrDefault(), valueOrDefault1));
              apAdjust2.CuryAdjgAmt = nullable2;
              apAdjust1.CuryAdjgDiscAmt = new Decimal?(0M);
              ((PXSelectBase<APAdjust>) this.Base.Adjustments).Update(apAdjust1);
            }
          }
        }
      }
    }
  }

  [PXOverride]
  public virtual void InitAdjustmentData(
    APAdjust adj,
    APRegister invoice,
    APTran tran,
    Action<APAdjust, APRegister, APTran> baseMethod)
  {
    if (adj.JointPayeeID.HasValue || !(invoice is APInvoice apInvoice) || !apInvoice.IsJointPayees.GetValueOrDefault())
      return;
    if (apInvoice.PaymentsByLinesAllowed.GetValueOrDefault())
    {
      JointPayee jointPayee = PXResultset<JointPayee>.op_Implicit(((PXSelectBase<JointPayee>) new PXSelect<JointPayee, Where<JointPayee.aPDocType, Equal<Required<JointPayee.aPDocType>>, And<JointPayee.aPRefNbr, Equal<Required<JointPayee.aPRefNbr>>, And<JointPayee.aPLineNbr, Equal<Required<JointPayee.aPLineNbr>>, And<JointPayee.isMainPayee, Equal<True>>>>>>((PXGraph) this.Base)).Select(new object[3]
      {
        (object) invoice.DocType,
        (object) invoice.RefNbr,
        (object) tran.LineNbr
      }));
      if (jointPayee == null)
        return;
      adj.JointPayeeID = jointPayee.JointPayeeId;
    }
    else
    {
      if (adj.VoidAppl.GetValueOrDefault())
      {
        string adjgRefNbr = adj.AdjgRefNbr;
        JointPayeePayment jointPayeePayment = PXResultset<JointPayeePayment>.op_Implicit(((PXSelectBase<JointPayeePayment>) new PXSelect<JointPayeePayment, Where<JointPayeePayment.paymentDocType, In<Required<JointPayeePayment.paymentDocType>>, And<JointPayeePayment.paymentRefNbr, Equal<Required<JointPayeePayment.paymentRefNbr>>>>>((PXGraph) this.Base)).Select(new object[2]
        {
          (object) APPaymentType.GetVoidedAPDocType(adj.AdjgDocType),
          (object) adjgRefNbr
        }));
        if (jointPayeePayment != null)
        {
          adj.JointPayeeID = jointPayeePayment.JointPayeeId;
          return;
        }
      }
      JointPayee jointPayee = PXResultset<JointPayee>.op_Implicit(((PXSelectBase<JointPayee>) new PXSelect<JointPayee, Where<JointPayee.aPDocType, Equal<Required<JointPayee.aPDocType>>, And<JointPayee.aPRefNbr, Equal<Required<JointPayee.aPRefNbr>>, And<JointPayee.isMainPayee, Equal<True>>>>>((PXGraph) this.Base)).Select(new object[2]
      {
        (object) invoice.DocType,
        (object) invoice.RefNbr
      }));
      if (jointPayee == null)
        return;
      adj.JointPayeeID = jointPayee.JointPayeeId;
    }
  }

  [PXOverride]
  public virtual Decimal CalculateApplicationAmount(
    APAdjust adj,
    Func<APAdjust, Decimal> baseMethod)
  {
    Decimal applicationAmount = baseMethod(adj);
    if (((PXSelectBase<APPayment>) this.Base.Document).Current.DocType == "ADR")
      applicationAmount = Math.Min(this.GetDebitAdjustmentMaxBalance(adj) ?? applicationAmount, applicationAmount);
    else if (adj.JointPayeeID.HasValue)
    {
      JointPayee jointPayee = JointPayee.PK.Find((PXGraph) this.Base, adj.JointPayeeID);
      applicationAmount = Math.Max(0M, Math.Min(applicationAmount, jointPayee.CuryJointBalance.GetValueOrDefault() - adj.CuryDiscBal.GetValueOrDefault()));
    }
    return applicationAmount;
  }

  protected virtual void _(PX.Data.Events.RowSelected<APPayment> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.addJointPayee).SetEnabled(e.Row.VendorID.HasValue && e.Row.DocType == "CHK" && e.Row.OpenDoc.GetValueOrDefault());
  }

  protected virtual void _(PX.Data.Events.RowUpdated<APPayment> e)
  {
    if (this.IsPreparePaymentsMassProcessing || e.Row == null || !e.Row.JointPayeeID.HasValue)
      return;
    APPayment row = e.Row;
    bool? nullable = row.OpenDoc;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = row.Hold;
    Decimal jointPayeeBalance;
    if (nullable.GetValueOrDefault() || !this.IsJointPayeeBalanceExceeds(row, out jointPayeeBalance))
      return;
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<APPayment>>) e).Cache.RaiseExceptionHandling<APPayment.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("The specified payment amount exceeds the joint payee balance. Payment amount must be equal to or less than {0}.", new object[2]
    {
      (object) Math.Round(jointPayeeBalance, CommonSetupDecPl.PrcCst),
      (object) (PXErrorLevel) 4
    }));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<APPayment> e)
  {
    if (this.IsPreparePaymentsMassProcessing || e.Row == null || !e.Row.JointPayeeID.HasValue)
      return;
    APPayment row = e.Row;
    bool? nullable = row.OpenDoc;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = row.Hold;
    Decimal jointPayeeBalance;
    if (!nullable.GetValueOrDefault() && this.IsJointPayeeBalanceExceeds(row, out jointPayeeBalance))
      throw new PXRowPersistingException(typeof (APPayment.curyOrigDocAmt).Name, (object) row.CuryOrigDocAmt, "The specified payment amount exceeds the joint payee balance. Payment amount must be equal to or less than {0}.", new object[1]
      {
        (object) Math.Round(jointPayeeBalance, CommonSetupDecPl.PrcCst)
      });
  }

  private bool IsJointPayeeBalanceExceeds(APPayment row, out Decimal jointPayeeBalance)
  {
    FbqlSelect<SelectFromBase<APAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<APInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>>>.And<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APAdjust.adjdDocType>>>>, FbqlJoins.Inner<JointPayee>.On<BqlOperand<JointPayee.jointPayeeId, IBqlInt>.IsEqual<APAdjust.jointPayeeID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APAdjust.adjgDocType, Equal<BqlField<APPayment.docType, IBqlString>.FromCurrent>>>>, And<BqlOperand<APAdjust.adjgRefNbr, IBqlString>.IsEqual<BqlField<APPayment.refNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<APAdjust.released, IBqlBool>.IsNotEqual<True>>>, And<BqlOperand<APInvoice.isJointPayees, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<APInvoice.isRetainageDocument, IBqlBool>.IsNotEqual<True>>>.Aggregate<To<Sum<JointPayee.jointBalance>>>, APAdjust>.View view = new FbqlSelect<SelectFromBase<APAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<APInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>>>.And<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APAdjust.adjdDocType>>>>, FbqlJoins.Inner<JointPayee>.On<BqlOperand<JointPayee.jointPayeeId, IBqlInt>.IsEqual<APAdjust.jointPayeeID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APAdjust.adjgDocType, Equal<BqlField<APPayment.docType, IBqlString>.FromCurrent>>>>, And<BqlOperand<APAdjust.adjgRefNbr, IBqlString>.IsEqual<BqlField<APPayment.refNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<APAdjust.released, IBqlBool>.IsNotEqual<True>>>, And<BqlOperand<APInvoice.isJointPayees, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<APInvoice.isRetainageDocument, IBqlBool>.IsNotEqual<True>>>.Aggregate<To<Sum<JointPayee.jointBalance>>>, APAdjust>.View((PXGraph) this.Base);
    using (new PXFieldScope(((PXSelectBase) view).View, new Type[1]
    {
      typeof (JointPayee.jointBalance)
    }))
    {
      JointPayee jointPayee = GraphHelper.RowCast<JointPayee>((IEnumerable) ((PXSelectBase<APAdjust>) view).Select(Array.Empty<object>())).FirstOrDefault<JointPayee>();
      jointPayeeBalance = 0M;
      if (jointPayee != null)
      {
        Decimal? nullable = jointPayee.JointBalance;
        if (nullable.HasValue)
        {
          ref Decimal local1 = ref jointPayeeBalance;
          nullable = jointPayee.JointBalance;
          Decimal valueOrDefault1 = nullable.GetValueOrDefault();
          local1 = valueOrDefault1;
          if (((PXSelectBase<APAdjust>) this.Base.Adjustments).Current != null && ((PXSelectBase<APAdjust>) this.Base.Adjustments).Current.AdjdCuryID != ((PXSelectBase<APPayment>) this.Base.Document).Current.CuryID)
          {
            PXCache cache = ((PXSelectBase) this.Base.Adjustments).Cache;
            PXSelectJoin<APAdjust, LeftJoin<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APAdjust.adjdDocType>, And<APTran.refNbr, Equal<APAdjust.adjdRefNbr>, And<APTran.lineNbr, Equal<APAdjust.adjdLineNbr>>>>>>>, Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<APAdjust.released, NotEqual<True>>>>> adjustments = this.Base.Adjustments;
            nullable = jointPayee.JointBalance;
            Decimal valueOrDefault2 = nullable.GetValueOrDefault();
            ref Decimal local2 = ref jointPayeeBalance;
            PX.Objects.CM.PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury<APAdjust.adjgCuryInfoID>(cache, (object) adjustments, valueOrDefault2, out local2);
          }
          if (jointPayee != null)
          {
            nullable = row.CuryOrigDocAmt;
            Decimal num = jointPayeeBalance;
            if (nullable.GetValueOrDefault() > num & nullable.HasValue)
              return true;
          }
        }
      }
    }
    return false;
  }

  private bool IsInReversingApplicationStateWithJointPayee()
  {
    foreach (JointPayeePayment jointPayeePayment in ((PXSelectBase) this.JointPayments).Cache.Updated)
    {
      if (jointPayeePayment.JointPayeeId.HasValue && jointPayeePayment.IsVoided.GetValueOrDefault())
        return true;
    }
    return false;
  }

  protected virtual void _(
    PX.Data.Events.RowUpdated<APPaymentEntryJointCheck.JointPayeeFilter> e)
  {
    ((PXSelectBase) this.BillWithJointPayee).Cache.Clear();
  }

  protected virtual void _(
    PX.Data.Events.RowPersisting<APPaymentEntryJointCheck.JointPayeeRecord> e)
  {
    e.Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APPaymentEntryJointCheck.JointPayeeRecord, APPaymentEntryJointCheck.JointPayeeRecord.selected> e)
  {
    HashSet<APPaymentEntryJointCheck.JointPayeeRecordKey> jointPayeeRecordKeySet1 = new HashSet<APPaymentEntryJointCheck.JointPayeeRecordKey>();
    foreach (PXResult<APAdjust> pxResult in ((PXSelectBase<APAdjust>) this.Base.Adjustments).Select(Array.Empty<object>()))
    {
      APAdjust apAdjust = PXResult<APAdjust>.op_Implicit(pxResult);
      int? nullable1;
      if (apAdjust.AdjdDocType == e.Row.DocType && apAdjust.AdjdRefNbr == e.Row.RefNbr)
      {
        nullable1 = apAdjust.AdjdLineNbr;
        int? nullable2 = e.Row.LineNbr;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = apAdjust.JointPayeeID;
          nullable1 = e.Row.JointPayeeID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            continue;
        }
      }
      if (!apAdjust.Voided.GetValueOrDefault())
      {
        HashSet<APPaymentEntryJointCheck.JointPayeeRecordKey> jointPayeeRecordKeySet2 = jointPayeeRecordKeySet1;
        string adjdDocType = apAdjust.AdjdDocType;
        string adjdRefNbr = apAdjust.AdjdRefNbr;
        nullable1 = apAdjust.AdjdLineNbr;
        int valueOrDefault1 = nullable1.GetValueOrDefault();
        nullable1 = apAdjust.JointPayeeID;
        int valueOrDefault2 = nullable1.GetValueOrDefault();
        APPaymentEntryJointCheck.JointPayeeRecordKey jointPayeeRecordKey = new APPaymentEntryJointCheck.JointPayeeRecordKey(adjdDocType, adjdRefNbr, valueOrDefault1, valueOrDefault2);
        jointPayeeRecordKeySet2.Add(jointPayeeRecordKey);
      }
    }
    foreach (APPaymentEntryJointCheck.JointPayeeRecord jointPayeeRecord in ((PXSelectBase) this.BillWithJointPayee).Cache.Cached)
    {
      int? nullable3;
      if (jointPayeeRecord.DocType == e.Row.DocType && jointPayeeRecord.RefNbr == e.Row.RefNbr)
      {
        nullable3 = jointPayeeRecord.LineNbr;
        int? nullable4 = e.Row.LineNbr;
        if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
        {
          nullable4 = jointPayeeRecord.JointPayeeID;
          nullable3 = e.Row.JointPayeeID;
          if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
            continue;
        }
      }
      if (jointPayeeRecord.Selected.GetValueOrDefault())
      {
        HashSet<APPaymentEntryJointCheck.JointPayeeRecordKey> jointPayeeRecordKeySet3 = jointPayeeRecordKeySet1;
        string docType = jointPayeeRecord.DocType;
        string refNbr = jointPayeeRecord.RefNbr;
        nullable3 = jointPayeeRecord.LineNbr;
        int valueOrDefault3 = nullable3.GetValueOrDefault();
        nullable3 = jointPayeeRecord.JointPayeeID;
        int valueOrDefault4 = nullable3.GetValueOrDefault();
        APPaymentEntryJointCheck.JointPayeeRecordKey jointPayeeRecordKey = new APPaymentEntryJointCheck.JointPayeeRecordKey(docType, refNbr, valueOrDefault3, valueOrDefault4);
        jointPayeeRecordKeySet3.Add(jointPayeeRecordKey);
      }
    }
    foreach (APPaymentEntryJointCheck.JointPayeeRecordKey jointPayeeRecordKey in jointPayeeRecordKeySet1)
    {
      int jointPayeeId = jointPayeeRecordKey.JointPayeeID;
      int? nullable = e.Row.JointPayeeID;
      int valueOrDefault = nullable.GetValueOrDefault();
      if (!(jointPayeeId == valueOrDefault & nullable.HasValue))
      {
        JointPayee jointPayee1 = JointPayee.PK.Find((PXGraph) this.Base, new int?(jointPayeeRecordKey.JointPayeeID));
        if (jointPayee1 != null)
        {
          nullable = jointPayee1.JointPayeeInternalId;
          if (!nullable.HasValue)
            throw new PXSetPropertyException("You can add only one bill with an external joint payee to the payment.", (PXErrorLevel) 5)
            {
              ErrorValue = (object) false
            };
          JointPayee jointPayee2 = JointPayee.PK.Find((PXGraph) this.Base, e.Row.JointPayeeID);
          if (jointPayee2 != null)
          {
            nullable = jointPayee2.JointPayeeInternalId;
            nullable = nullable.HasValue ? jointPayee2.JointPayeeInternalId : throw new PXSetPropertyException("You cannot add a line with an external joint vendor because the {0} vendor has already been selected for payment.", (PXErrorLevel) 5, new object[1]
            {
              (object) PX.Objects.AP.Vendor.PK.Find((PXGraph) this.Base, jointPayee1.JointPayeeInternalId).AcctName
            })
            {
              ErrorValue = (object) false
            };
            int? jointPayeeInternalId = jointPayee1.JointPayeeInternalId;
            if (!(nullable.GetValueOrDefault() == jointPayeeInternalId.GetValueOrDefault() & nullable.HasValue == jointPayeeInternalId.HasValue))
              throw new PXSetPropertyException("You can select only the lines with the same vendor.", (PXErrorLevel) 5)
              {
                ErrorValue = (object) false
              };
          }
        }
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowInserted<APAdjust> e)
  {
    if (e.Row.AdjdDocType != "INV")
      return;
    if (this.jointPayeePayment != null)
    {
      ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<APAdjust>>) e).Cache.SetValue<APAdjust.jointPayeeID>((object) e.Row, (object) this.jointPayeePayment.JointPayeeId);
      JointPayeePayment jointPayeePayment = this.jointPayeePayment;
      Decimal? jointAmountToPay = jointPayeePayment.CuryJointAmountToPay;
      Decimal? nullable1 = e.Row.CuryAdjgAmt;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      Decimal? nullable2;
      if (!jointAmountToPay.HasValue)
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new Decimal?(jointAmountToPay.GetValueOrDefault() + valueOrDefault);
      jointPayeePayment.CuryJointAmountToPay = nullable2;
      ((PXSelectBase<JointPayeePayment>) this.JointPayments).Update(this.jointPayeePayment);
    }
    else
    {
      if (this.Base.AutoPaymentApp || !e.Row.JointPayeeID.HasValue || e.Row.VoidAppl.GetValueOrDefault() || this.GetJointPayment(e.Row.AdjdDocType, e.Row.AdjdRefNbr, e.Row.AdjdLineNbr) != null)
        return;
      JointPayeePayment jointPayeePayment = ((PXSelectBase<JointPayeePayment>) this.JointPayments).Insert();
      jointPayeePayment.JointPayeeId = e.Row.JointPayeeID;
      jointPayeePayment.InvoiceDocType = e.Row.AdjdDocType;
      jointPayeePayment.InvoiceRefNbr = e.Row.AdjdRefNbr;
      jointPayeePayment.AdjustmentNumber = new int?(e.Row.AdjdLineNbr.GetValueOrDefault());
      jointPayeePayment.CuryJointAmountToPay = e.Row.CuryAdjgAmt;
      jointPayeePayment.JointAmountToPay = e.Row.AdjAmt;
      ((PXSelectBase<JointPayeePayment>) this.JointPayments).Insert(jointPayeePayment);
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<APAdjust> e)
  {
    if (e.Row.AdjdDocType != "INV")
      return;
    Decimal? curyAdjgAmt = e.Row.CuryAdjgAmt;
    Decimal? nullable1 = e.OldRow.CuryAdjgAmt;
    if (!(curyAdjgAmt.GetValueOrDefault() == nullable1.GetValueOrDefault() & curyAdjgAmt.HasValue == nullable1.HasValue) && !e.Row.Voided.GetValueOrDefault())
    {
      if (this.jointPayeePayment != null)
      {
        JointPayeePayment jointPayeePayment1 = this.jointPayeePayment;
        nullable1 = jointPayeePayment1.CuryJointAmountToPay;
        Decimal valueOrDefault1 = e.OldRow.CuryAdjgAmt.GetValueOrDefault();
        jointPayeePayment1.CuryJointAmountToPay = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault1) : new Decimal?();
        JointPayeePayment jointPayeePayment2 = this.jointPayeePayment;
        nullable1 = jointPayeePayment2.CuryJointAmountToPay;
        Decimal? nullable2 = e.Row.CuryAdjgAmt;
        Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
        Decimal? nullable3;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault2);
        jointPayeePayment2.CuryJointAmountToPay = nullable3;
        ((PXSelectBase<JointPayeePayment>) this.JointPayments).Update(this.jointPayeePayment);
      }
      else
      {
        JointPayeePayment jointPayment = this.GetJointPayment(e.Row.AdjdDocType, e.Row.AdjdRefNbr, e.Row.AdjdLineNbr);
        if (jointPayment != null)
        {
          JointPayeePayment jointPayeePayment3 = jointPayment;
          nullable1 = jointPayeePayment3.CuryJointAmountToPay;
          Decimal valueOrDefault3 = e.OldRow.CuryAdjgAmt.GetValueOrDefault();
          jointPayeePayment3.CuryJointAmountToPay = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault3) : new Decimal?();
          JointPayeePayment jointPayeePayment4 = jointPayment;
          nullable1 = jointPayeePayment4.CuryJointAmountToPay;
          Decimal valueOrDefault4 = e.Row.CuryAdjgAmt.GetValueOrDefault();
          jointPayeePayment4.CuryJointAmountToPay = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
          ((PXSelectBase<JointPayeePayment>) this.JointPayments).Update(jointPayment);
        }
      }
    }
    if (!this.Base.AutoPaymentApp || !e.Row.Voided.GetValueOrDefault())
      return;
    JointPayeePayment jointPayment1 = this.GetJointPayment(e.Row.AdjdDocType, e.Row.AdjdRefNbr, e.Row.VoidAdjNbr);
    if (jointPayment1 != null)
    {
      jointPayment1.IsVoided = new bool?(true);
      ((PXSelectBase<JointPayeePayment>) this.JointPayments).Update(jointPayment1);
      JointPayeePayment jointPayeePayment = ((PXSelectBase<JointPayeePayment>) this.JointPayments).Insert();
      jointPayeePayment.JointPayeeId = e.Row.JointPayeeID;
      jointPayeePayment.InvoiceDocType = e.Row.AdjdDocType;
      jointPayeePayment.InvoiceRefNbr = e.Row.AdjdRefNbr;
      jointPayeePayment.AdjustmentNumber = new int?(e.Row.AdjdLineNbr.GetValueOrDefault());
      jointPayeePayment.CuryJointAmountToPay = e.Row.CuryAdjgAmt;
      jointPayeePayment.JointAmountToPay = e.Row.AdjAmt;
      ((PXSelectBase<JointPayeePayment>) this.JointPayments).Update(jointPayeePayment);
    }
    this.ClearJointPayeeOnDocument();
  }

  private JointPayeePayment GetJointPayment(string docType, string refNbr, int? lineNbr)
  {
    return PXResultset<JointPayeePayment>.op_Implicit(((PXSelectBase<JointPayeePayment>) new PXSelect<JointPayeePayment, Where<JointPayeePayment.paymentDocType, Equal<Current<APPayment.docType>>, And<JointPayeePayment.paymentRefNbr, Equal<Current<APPayment.refNbr>>, And<JointPayeePayment.invoiceDocType, Equal<Required<JointPayeePayment.invoiceDocType>>, And<JointPayeePayment.invoiceRefNbr, Equal<Required<JointPayeePayment.invoiceRefNbr>>, And<JointPayeePayment.adjustmentNumber, Equal<Required<JointPayeePayment.adjustmentNumber>>, And<JointPayeePayment.isVoided, NotEqual<True>>>>>>>>((PXGraph) this.Base)).Select(new object[3]
    {
      (object) docType,
      (object) refNbr,
      (object) lineNbr.GetValueOrDefault()
    }));
  }

  protected virtual void _(PX.Data.Events.RowDeleted<APAdjust> e)
  {
    if (e.Row.AdjdDocType != "INV")
      return;
    if (this.jointPayeePayment != null)
    {
      JointPayeePayment jointPayeePayment = this.jointPayeePayment;
      Decimal? jointAmountToPay = jointPayeePayment.CuryJointAmountToPay;
      Decimal? curyAdjgAmt = e.Row.CuryAdjgAmt;
      jointPayeePayment.CuryJointAmountToPay = jointAmountToPay.HasValue & curyAdjgAmt.HasValue ? new Decimal?(jointAmountToPay.GetValueOrDefault() - curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<JointPayeePayment>) this.JointPayments).Update(this.jointPayeePayment);
    }
    else
    {
      JointPayeePayment jointPayment = this.GetJointPayment(e.Row.AdjdDocType, e.Row.AdjdRefNbr, e.Row.AdjdLineNbr);
      if (jointPayment != null)
      {
        JointPayeePayment jointPayeePayment = jointPayment;
        Decimal? jointAmountToPay = jointPayeePayment.CuryJointAmountToPay;
        Decimal valueOrDefault = e.Row.CuryAdjgAmt.GetValueOrDefault();
        jointPayeePayment.CuryJointAmountToPay = jointAmountToPay.HasValue ? new Decimal?(jointAmountToPay.GetValueOrDefault() - valueOrDefault) : new Decimal?();
        ((PXSelectBase<JointPayeePayment>) this.JointPayments).Update(jointPayment);
      }
    }
    this.ResetJointPayeeOnLastLineDeleted();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgAmt> e)
  {
    if (e.Row.JointPayeeID.HasValue)
    {
      JointPayee jointPayee = JointPayee.PK.Find((PXGraph) this.Base, e.Row.JointPayeeID);
      if (e.Row.AdjdCuryID == ((PXSelectBase<APPayment>) this.Base.Document).Current.CuryID)
      {
        if (jointPayee == null)
          return;
        Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgAmt>, APAdjust, object>) e).NewValue;
        Decimal? curyJointBalance = jointPayee.CuryJointBalance;
        if (newValue.GetValueOrDefault() > curyJointBalance.GetValueOrDefault() & newValue.HasValue & curyJointBalance.HasValue)
        {
          object[] objArray = new object[1];
          curyJointBalance = jointPayee.CuryJointBalance;
          objArray[0] = (object) curyJointBalance.Value.ToString("n2");
          throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
        }
      }
      else
      {
        if (jointPayee == null)
          return;
        Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgAmt>, APAdjust, object>) e).NewValue;
        Decimal? jointBalance = jointPayee.JointBalance;
        if (newValue.GetValueOrDefault() > jointBalance.GetValueOrDefault() & newValue.HasValue & jointBalance.HasValue)
        {
          object[] objArray = new object[1];
          jointBalance = jointPayee.JointBalance;
          objArray[0] = (object) jointBalance.Value.ToString("n2");
          throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
        }
      }
    }
    else
    {
      if (!(((PXSelectBase<APPayment>) this.Base.Document).Current.DocType == "ADR"))
        return;
      this.ValidateDebitAdjustment(e.Row, (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgAmt>, APAdjust, object>) e).NewValue);
    }
  }

  private void ValidateDebitAdjustment(APAdjust row, Decimal? newValue)
  {
    Decimal? adjustmentMaxBalance = this.GetDebitAdjustmentMaxBalance(row);
    if (!adjustmentMaxBalance.HasValue)
      return;
    Decimal? nullable1 = newValue;
    Decimal? nullable2 = adjustmentMaxBalance;
    if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
      throw new PXSetPropertyException("The debit adjustment cannot be applied to the Joint Amounts. The amount available for application is {0}.", new object[1]
      {
        (object) adjustmentMaxBalance.Value.ToString("n2")
      });
  }

  private Decimal? GetDebitAdjustmentMaxBalance(APAdjust row)
  {
    return PXResultset<JointPayee>.op_Implicit(((PXSelectBase<JointPayee>) new PXSelect<JointPayee, Where<JointPayee.aPDocType, Equal<Required<JointPayee.aPDocType>>, And<JointPayee.aPRefNbr, Equal<Required<JointPayee.aPRefNbr>>, And<JointPayee.isMainPayee, Equal<True>>>>>((PXGraph) this.Base)).Select(new object[2]
    {
      (object) row.AdjdDocType,
      (object) row.AdjdRefNbr
    }))?.CuryJointBalance.GetValueOrDefault();
  }

  private void ClearJointPayeeOnDocument()
  {
    bool flag = false;
    foreach (PXResult<APAdjust> pxResult in ((PXSelectBase<APAdjust>) this.Base.Adjustments).Select(Array.Empty<object>()))
    {
      if (!PXResult<APAdjust>.op_Implicit(pxResult).Voided.GetValueOrDefault())
      {
        flag = true;
        break;
      }
    }
    if (flag)
      return;
    ((PXSelectBase<APPayment>) this.Base.Document).Current.JointPayeeID = new int?();
    ((PXSelectBase<APPayment>) this.Base.Document).UpdateCurrent();
  }

  private void ResetJointPayeeOnLastLineDeleted()
  {
    if (((PXSelectBase<APPayment>) this.Base.Document).Current == null || ((PXSelectBase) this.Base.Document).Cache.GetStatus((object) ((PXSelectBase<APPayment>) this.Base.Document).Current) == 3 || ((PXSelectBase<APAdjust>) this.Base.Adjustments).Select(Array.Empty<object>()).Count != 0)
      return;
    ((PXSelectBase<APPayment>) this.Base.Document).Current.JointPayeeID = new int?();
    ((PXSelectBase<APPayment>) this.Base.Document).UpdateCurrent();
  }

  [PXOverride]
  public virtual void Segregate(
    APAdjust adj,
    PX.Objects.CM.Extensions.CurrencyInfo info,
    Action<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo> baseMethod)
  {
    int? adjNbr = adj.AdjNbr;
    if (((PXGraph) this.Base).IsDirty)
      ((PXAction) this.Base.Save).Press();
    APInvoice apInvoice = PXResultset<APInvoice>.op_Implicit(((PXSelectBase<APInvoice>) this.Base.APInvoice_VendorID_DocType_RefNbr).Select(new object[4]
    {
      (object) adj.AdjdLineNbr,
      (object) adj.VendorID,
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    }));
    JointPayee jointPayee = JointPayee.PK.Find((PXGraph) this.Base, adjNbr);
    bool? nullable1;
    int? nullable2;
    if (adjNbr.HasValue)
    {
      nullable1 = adj.SeparateCheck;
      if (!nullable1.GetValueOrDefault())
      {
        int num;
        if (jointPayee == null)
        {
          num = 1;
        }
        else
        {
          nullable1 = jointPayee.IsMainPayee;
          num = !nullable1.GetValueOrDefault() ? 1 : 0;
        }
        if (num != 0)
        {
          nullable2 = jointPayee.JointPayeeInternalId;
          string str = !nullable2.HasValue ? $"JP_{jointPayee.JointPayeeId}" : $"V_{jointPayee.JointPayeeInternalId}";
          APPayment apPayment = this.Base.created.Find<APPayment.vendorID, APPayment.vendorLocationID, APRegister.hiddenKey>((object) apInvoice.VendorID, (object) apInvoice.PayLocationID, (object) str);
          if (apPayment != null)
          {
            ((PXSelectBase<APPayment>) this.Base.Document).Current = PXResultset<APPayment>.op_Implicit(((PXSelectBase<APPayment>) this.Base.Document).Search<APPayment.refNbr>((object) apPayment.RefNbr, new object[1]
            {
              (object) apPayment.DocType
            }));
            PX.Objects.CM.Extensions.CurrencyInfo info1;
            if (this.Base.createdInfo.TryGetValue(((PXSelectBase<APPayment>) this.Base.Document).Current.CuryInfoID.Value, out info1))
              ((PXGraph) this.Base).FindImplementation<APPaymentEntry.MultiCurrency>()?.StoreResult(info1);
          }
          else
            baseMethod(adj, info);
          ((PXSelectBase<APPayment>) this.Base.Document).Current.HiddenKey = str;
          goto label_15;
        }
      }
    }
    baseMethod(adj, info);
label_15:
    if (!(adj.AdjdDocType == "INV"))
      return;
    this.jointPayeePayment = ((PXSelectBase<JointPayeePayment>) this.JointPayments).Insert();
    this.jointPayeePayment.JointPayeeId = adjNbr;
    this.jointPayeePayment.InvoiceDocType = adj.AdjdDocType;
    this.jointPayeePayment.InvoiceRefNbr = adj.AdjdRefNbr;
    JointPayeePayment jointPayeePayment = this.jointPayeePayment;
    nullable2 = adj.AdjdLineNbr;
    int? nullable3 = new int?(nullable2.GetValueOrDefault());
    jointPayeePayment.AdjustmentNumber = nullable3;
    nullable2 = ((PXSelectBase<APPayment>) this.Base.Document).Current.JointPayeeID;
    if (nullable2.HasValue || jointPayee == null)
      return;
    nullable1 = jointPayee.IsMainPayee;
    if (nullable1.GetValueOrDefault())
      return;
    ((PXSelectBase<APPayment>) this.Base.Document).Current.JointPayeeID = adjNbr;
  }

  [PXOverride]
  public virtual APPayment FindOrCreatePayment(
    APInvoice apdoc,
    APAdjust adj,
    Func<APInvoice, APAdjust, APPayment> baseMethod)
  {
    int? adjNbr = adj.AdjNbr;
    JointPayee jointPayee = JointPayee.PK.Find((PXGraph) this.Base, adjNbr);
    bool? nullable;
    if (adjNbr.HasValue)
    {
      nullable = adj.SeparateCheck;
      if (!nullable.GetValueOrDefault())
      {
        int num;
        if (jointPayee == null)
        {
          num = 1;
        }
        else
        {
          nullable = jointPayee.IsMainPayee;
          num = !nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num != 0)
        {
          string str = $"{jointPayee.JointPayeeInternalId ?? jointPayee.JointPayeeId}";
          return this.Base.created.Find<APPayment.vendorID, APPayment.vendorLocationID, APRegister.hiddenKey>((object) apdoc.VendorID, (object) apdoc.PayLocationID, (object) str) ?? new APPayment();
        }
      }
    }
    APPayment orCreatePayment = baseMethod(apdoc, adj);
    if (jointPayee != null)
    {
      nullable = jointPayee.IsMainPayee;
      if (!nullable.GetValueOrDefault())
        ((PXSelectBase) this.Base.Document).Cache.SetValue<APPayment.jointPayeeID>((object) orCreatePayment, (object) jointPayee.JointPayeeId);
    }
    return orCreatePayment;
  }

  [PXOverride]
  public virtual void Persist(Action baseMethod)
  {
    this.AddLinksToCompliance();
    baseMethod();
  }

  protected virtual void AddLinksToCompliance()
  {
    HashSet<string> stringSet = new HashSet<string>();
    if (((PXSelectBase<APPayment>) this.Base.Document).Current == null)
      return;
    foreach (PXResult<APAdjust> pxResult1 in ((PXSelectBase<APAdjust>) this.Base.Adjustments).Select(Array.Empty<object>()))
    {
      if (pxResult1 is PXResult<APAdjust, APInvoice, APTran> pxResult2)
      {
        APInvoice apdoc = PXResult<APAdjust, APInvoice, APTran>.op_Implicit(pxResult2);
        string str = $"{apdoc.DocType}.{apdoc.RefNbr}";
        if (!stringSet.Contains(str))
        {
          stringSet.Add(str);
          this.LinkComplianceToPayment(apdoc, ((PXSelectBase<APPayment>) this.Base.Document).Current);
        }
      }
    }
  }

  public virtual void LinkComplianceToPayment(APInvoice apdoc, APPayment payment)
  {
    foreach (PXResult<ComplianceDocumentReference, ComplianceDocument> pxResult in ((PXSelectBase<ComplianceDocumentReference>) new PXSelectJoin<ComplianceDocumentReference, InnerJoin<ComplianceDocument, On<ComplianceDocument.billID, Equal<ComplianceDocumentReference.complianceDocumentReferenceId>>>, Where<ComplianceDocumentReference.refNoteId, Equal<Required<ComplianceDocumentReference.refNoteId>>, And<ComplianceDocument.linkToPayment, Equal<True>, And<ComplianceDocument.apCheckId, IsNull>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) apdoc.NoteID
    }))
    {
      ComplianceDocument complianceDocument = PXResult<ComplianceDocumentReference, ComplianceDocument>.op_Implicit(pxResult);
      ComplianceDocumentPaymentReference paymentReference = new ComplianceDocumentPaymentReference();
      paymentReference.ComplianceDocumentReferenceId = new Guid?(Guid.NewGuid());
      ((PXSelectBase<ComplianceDocumentPaymentReference>) this.LinkToPayments).Insert(paymentReference);
      complianceDocument.ApCheckID = paymentReference.ComplianceDocumentReferenceId;
      complianceDocument.CheckNumber = payment.ExtRefNbr;
      ((PXSelectBase<ComplianceDocument>) this.Compliance).Update(complianceDocument);
    }
  }

  [PXHidden]
  public class JointPayeeFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(3, IsFixed = true, InputMask = "")]
    [PXDefault("INV")]
    [PXUIField]
    [APInvoiceType.AdjdList]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Reference Nbr.")]
    [PXSelector(typeof (Search<APInvoice.refNbr, Where<APInvoice.docType, Equal<Current<APPaymentEntryJointCheck.JointPayeeFilter.docType>>, And<APInvoice.vendorID, Equal<Current<APPayment.vendorID>>, And<APInvoice.isJointPayees, Equal<True>>>>>))]
    public virtual string RefNbr { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeFilter.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeFilter.refNbr>
    {
    }
  }

  [PXHidden]
  public class JointPayeeRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected { get; set; }

    [PXDBString(IsUnicode = true, IsKey = true, BqlField = typeof (APInvoice.docType))]
    [PXUIField(DisplayName = "Doc Type")]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true, BqlField = typeof (APInvoice.refNbr))]
    [PXUIField(DisplayName = "Reference Nbr.")]
    public virtual string RefNbr { get; set; }

    [PXDBInt(IsKey = true, BqlField = typeof (APTran.lineNbr))]
    [PXUIField(DisplayName = "Bill Line Nbr.", FieldClass = "PaymentsByLines")]
    public virtual int? LineNbr { get; set; }

    [PXInt(IsKey = true)]
    public virtual int? JointPayeeID { get; set; }

    [PXString(60, IsUnicode = true)]
    [PXUIField(DisplayName = "Joint Payee Name")]
    public virtual string Name { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (APPayment.curyInfoID), typeof (APPaymentEntryJointCheck.JointPayeeRecord.jointAmountOwed))]
    [PXUIField(DisplayName = "Joint Amount Owed")]
    public virtual Decimal? CuryJointAmountOwed { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBBaseCury]
    public virtual Decimal? JointAmountOwed { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (APPayment.curyInfoID), typeof (APPaymentEntryJointCheck.JointPayeeRecord.jointAmountPaid))]
    [PXUIField(DisplayName = "Joint Amount Paid", IsReadOnly = true)]
    public virtual Decimal? CuryJointAmountPaid { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBBaseCury]
    public virtual Decimal? JointAmountPaid { get; set; }

    [PXCurrency(typeof (APPayment.curyInfoID), typeof (APPaymentEntryJointCheck.JointPayeeRecord.balance), BaseCalc = false)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? CuryBalance { get; set; }

    [PXDecimal(2)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? Balance { get; set; }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.selected>
    {
    }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.refNbr>
    {
    }

    public abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.lineNbr>
    {
    }

    public abstract class jointPayeeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.jointPayeeID>
    {
    }

    public abstract class name : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.name>
    {
    }

    public abstract class curyJointAmountOwed : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.curyJointAmountOwed>
    {
    }

    public abstract class jointAmountOwed : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.jointAmountOwed>
    {
    }

    public abstract class curyJointAmountPaid : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.curyJointAmountPaid>
    {
    }

    public abstract class jointAmountPaid : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.jointAmountPaid>
    {
    }

    public abstract class curyBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.curyBalance>
    {
    }

    public abstract class balance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPaymentEntryJointCheck.JointPayeeRecord.balance>
    {
    }
  }

  [DebuggerDisplay("{ProjectID}.{OrderNbr}")]
  public class JointPayeeRecordKey
  {
    public readonly string DocType;
    public readonly string RefNbr;
    public readonly int LineNbr;
    public readonly int JointPayeeID;

    public JointPayeeRecordKey(string docType, string refNbr, int lineNbr, int jointPayeeID)
    {
      this.DocType = docType;
      this.RefNbr = refNbr;
      this.LineNbr = lineNbr;
      this.JointPayeeID = jointPayeeID;
    }

    public override int GetHashCode()
    {
      return (((17 * 23 + this.DocType.GetHashCode()) * 23 + this.RefNbr.GetHashCode()) * 23 + this.LineNbr.GetHashCode()) * 23 + this.JointPayeeID.GetHashCode();
    }
  }
}
