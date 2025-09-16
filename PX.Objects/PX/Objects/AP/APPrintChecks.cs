// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPrintChecks
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP.MigrationMode;
using PX.Objects.AP.PaymentProcessor;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#nullable disable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APPrintChecks : PXGraph<APPrintChecks>
{
  public PXFilter<PrintChecksFilter> Filter;
  public PXCancel<PrintChecksFilter> Cancel;
  public PXAction<PrintChecksFilter> ViewDocument;
  [PXFilterable(new System.Type[] {})]
  public APPaymentProcessingWithWidget APPaymentList;
  public PXSelect<PX.Objects.CM.CurrencyInfo> currencyinfo;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public APSetupNoMigrationMode APSetup;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Current<PrintChecksFilter.payAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<PrintChecksFilter.payTypeID>>>>> cashaccountdetail;
  public PXSetup<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<PrintChecksFilter.payTypeID>>>> paymenttype;
  public PXSetup<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<PrintChecksFilter.payAccountID>>>> payAccount;
  private bool cleared;
  protected string ErrorMessageNextCheckNbr;
  private readonly Dictionary<object, object> _copies = new Dictionary<object, object>();

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton(OnClosingPopup = PXSpecialButtonType.Refresh)]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    if (this.APPaymentList.Current != null)
      PXRedirectHelper.TryRedirect(this.APPaymentList.Cache, (object) this.APPaymentList.Current, "Document", PXRedirectHelper.WindowMode.NewWindow);
    return adapter.Get();
  }

  public APPrintChecks()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    PXUIFieldAttribute.SetEnabled(this.APPaymentList.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<APPayment.selected>(this.APPaymentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APPayment.extRefNbr>(this.APPaymentList.Cache, (object) null, true);
    this.APPaymentList.SetSelected<APPayment.selected>();
    PXUIFieldAttribute.SetDisplayName<APPayment.vendorID>(this.APPaymentList.Cache, "Vendor ID");
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [APDocType.List]
  protected virtual void APPayment_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<PX.Objects.CS.FeaturesSet.branch>>.Or<FeatureInstalled<PX.Objects.CS.FeaturesSet.multiCompany>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<APPayment.branchID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<APPayment.extRefNbr> e)
  {
  }

  public override void Clear()
  {
    this.Filter.Current.CurySelTotal = new Decimal?(0M);
    this.Filter.Current.SelTotal = new Decimal?(0M);
    this.Filter.Current.SelCount = new int?(0);
    this.cleared = true;
    base.Clear();
  }

  protected virtual IEnumerable appaymentlist()
  {
    if (this.cleared)
    {
      foreach (APRegister apRegister in this.APPaymentList.Cache.Updated)
        apRegister.Passed = new bool?(false);
    }
    foreach (object apPayment in this.GetAPPaymentList())
      yield return apPayment;
    PXView.StartRow = 0;
  }

  protected virtual IEnumerable GetAPPaymentList()
  {
    foreach (PXResult<APPayment, Vendor, PX.Objects.CA.PaymentMethod, CABatchDetail> i1 in new PXSelectJoin<APPayment, InnerJoin<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<APPayment.paymentMethodID>>, LeftJoin<CABatchDetail, On<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<APPayment.refNbr>>>>>>>, Where2<Where<CABatchDetail.batchNbr, PX.Data.IsNull, And2<Where<APPayment.status, Equal<APDocStatus.pendingPrint>, PX.Data.Or<Where<APPayment.status, Equal<APDocStatus.pendingProcessing>, And<APPayment.externalPaymentID, PX.Data.IsNull>>>>, And<APPayment.cashAccountID, Equal<Current<PrintChecksFilter.payAccountID>>, And<APPayment.paymentMethodID, Equal<Current<PrintChecksFilter.payTypeID>>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>>>>, And<APPayment.docType, In3<APDocType.check, APDocType.prepayment, APDocType.quickCheck>>>>((PXGraph) this).SelectWithViewContext())
      yield return (object) new PXResult<APPayment, Vendor>(PXCache<APPayment>.CreateCopy((APPayment) i1), (Vendor) i1);
  }

  public virtual void AssignNumbers(
    APPaymentEntry pe,
    APPayment doc,
    ref string NextCheckNbr,
    bool skipStubs = false)
  {
    pe.RowPersisting.RemoveHandler<APAdjust>(new PXRowPersisting(pe.APAdjust_RowPersisting));
    pe.Clear();
    PXSelectJoin<APPayment, LeftJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>>, Where<APPayment.docType, Equal<Optional<APPayment.docType>>, PX.Data.And<Where<Vendor.bAccountID, PX.Data.IsNull, PX.Data.Or<Match<Vendor, Current<AccessInfo.userName>>>>>>> document1 = pe.Document;
    PXSelectJoin<APPayment, LeftJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>>, Where<APPayment.docType, Equal<Optional<APPayment.docType>>, PX.Data.And<Where<Vendor.bAccountID, PX.Data.IsNull, PX.Data.Or<Match<Vendor, Current<AccessInfo.userName>>>>>>> document2 = pe.Document;
    string refNbr = doc.RefNbr;
    object[] objArray = new object[1]
    {
      (object) doc.DocType
    };
    APPayment apPayment1;
    APPayment apPayment2 = apPayment1 = (APPayment) document2.Search<APPayment.refNbr>((object) refNbr, objArray);
    document1.Current = apPayment1;
    doc = apPayment2;
    PaymentMethodAccount det = (PaymentMethodAccount) pe.cashaccountdetail.Select();
    doc.IsPrintingProcess = new bool?(true);
    if (string.IsNullOrEmpty(NextCheckNbr))
      throw new PXException("Next Check Number is required to print AP Payments with 'Payment Ref.' empty");
    if (string.IsNullOrEmpty(pe.Document.Current.ExtRefNbr))
    {
      pe.Document.Current.StubCntr = new int?(1);
      pe.Document.Current.BillCntr = new int?(0);
      pe.Document.Current.ExtRefNbr = NextCheckNbr;
      pe.Document.Update(pe.Document.Current);
      if (EnumerableExtensions.IsIn<string>(pe.Document.Current.DocType, "QCK", "PPM"))
      {
        Decimal? curyOrigDocAmt = pe.Document.Current.CuryOrigDocAmt;
        Decimal num = 0M;
        if (curyOrigDocAmt.GetValueOrDefault() <= num & curyOrigDocAmt.HasValue)
          throw new PXException("Zero Check cannot be Printed or Exported.");
      }
      if (!skipStubs)
      {
        pe.DeletePrintList();
        IEnumerable<AdjustmentStubGroup> adjustmentsPrintList = pe.GetAdjustmentsPrintList();
        PX.Objects.CA.PaymentMethod paymentMethod = (PX.Objects.CA.PaymentMethod) pe.paymenttype.Select();
        int num1 = (int) System.Math.Ceiling((Decimal) adjustmentsPrintList.Count<AdjustmentStubGroup>() / (Decimal) (paymentMethod.APStubLines ?? (short) 1));
        if (num1 > 1 && !paymentMethod.APPrintRemittance.GetValueOrDefault())
        {
          string str = AutoNumberAttribute.NextNumber(NextCheckNbr, num1 - 1);
          string[] array = PXSelectBase<CashAccountCheck, PXSelect<CashAccountCheck, Where<CashAccountCheck.cashAccountID, Equal<Required<PrintChecksFilter.payAccountID>>, And<CashAccountCheck.paymentMethodID, Equal<Required<PrintChecksFilter.payTypeID>>, And<CashAccountCheck.checkNbr, GreaterEqual<Required<PrintChecksFilter.nextCheckNbr>>, And<CashAccountCheck.checkNbr, LessEqual<Required<PrintChecksFilter.nextCheckNbr>>, And<StrLen<CashAccountCheck.checkNbr>, Equal<StrLen<Required<PrintChecksFilter.nextCheckNbr>>>>>>>>>.Config>.Select((PXGraph) this, (object) det.CashAccountID, (object) det.PaymentMethodID, (object) NextCheckNbr, (object) str, (object) NextCheckNbr).RowCast<CashAccountCheck>().Select<CashAccountCheck, string>((Func<CashAccountCheck, string>) (check => check.CheckNbr)).ToArray<string>();
          if (((IEnumerable<string>) array).Any<string>())
            throw new PXException("This check consists of {0} stubs that require consecutive numbers from {1} to {2}. Please enter another number for the first stub because the number {3} is already used for another check.", new object[4]
            {
              (object) num1,
              (object) NextCheckNbr,
              (object) str,
              (object) string.Join(",", array)
            });
        }
        short num2 = 0;
        int stubOrdinal = 0;
        foreach (AdjustmentStubGroup adj in adjustmentsPrintList)
        {
          APPayment current1 = pe.Document.Current;
          int? nullable1 = current1.BillCntr;
          current1.BillCntr = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
          int num3 = (int) num2;
          short? apStubLines = paymentMethod.APStubLines;
          int? nullable2;
          if (!apStubLines.HasValue)
          {
            nullable1 = new int?();
            nullable2 = nullable1;
          }
          else
            nullable2 = new int?((int) apStubLines.GetValueOrDefault() - 1);
          int? nullable3 = nullable2;
          int valueOrDefault = nullable3.GetValueOrDefault();
          if (num3 > valueOrDefault & nullable3.HasValue)
          {
            if (paymentMethod.APPrintRemittance.GetValueOrDefault())
            {
              foreach (IAdjustmentStub groupedStub in (IEnumerable<IAdjustmentStub>) adj.GroupedStubs)
              {
                groupedStub.StubNbr = (string) null;
                if (groupedStub.Persistent)
                  pe.Caches[groupedStub.GetType()].Update((object) groupedStub);
              }
              pe.AddCheckDetail(adj, (string) null);
              continue;
            }
            NextCheckNbr = AutoNumberAttribute.NextNumber(NextCheckNbr);
            APPayment current2 = pe.Document.Current;
            nullable1 = current2.StubCntr;
            current2.StubCntr = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
            num2 = (short) 0;
            ++stubOrdinal;
          }
          foreach (IAdjustmentStub groupedStub in (IEnumerable<IAdjustmentStub>) adj.GroupedStubs)
            APPrintChecks.SetAdjustmentStubNumber(pe, doc, groupedStub, NextCheckNbr);
          pe.AddCheckDetail(adj, NextCheckNbr);
          this.StoreStubNumber(pe, doc, det, NextCheckNbr, stubOrdinal);
          ++num2;
        }
      }
      else
        det.APLastRefNbr = NextCheckNbr;
      pe.cashaccountdetail.Update(det);
      NextCheckNbr = AutoNumberAttribute.NextNumber(NextCheckNbr);
      pe.Document.Current.Hold = new bool?(false);
      PXEntityEventBase<APPayment>.Container<APPayment.Events>.Select((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (ev => ev.PrintCheck)).FireOn((PXGraph) pe, pe.Document.Current);
    }
    else
    {
      if (pe.Document.Current.Printed.GetValueOrDefault() && !pe.Document.Current.Hold.GetValueOrDefault())
        return;
      pe.Document.Current.Hold = new bool?(false);
      PXEntityEventBase<APPayment>.Container<APPayment.Events>.Select((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (ev => ev.PrintCheck)).FireOn((PXGraph) pe, pe.Document.Current);
    }
  }

  public virtual void StoreStubNumber(
    APPaymentEntry pe,
    APPayment doc,
    PaymentMethodAccount det,
    string StubNbr,
    int stubOrdinal)
  {
    if (doc.VoidAppl.GetValueOrDefault() || StubNbr == null)
      return;
    pe.CACheck.Insert(new CashAccountCheck()
    {
      CashAccountID = doc.CashAccountID,
      PaymentMethodID = doc.PaymentMethodID,
      CheckNbr = StubNbr,
      DocType = doc.DocType,
      RefNbr = doc.RefNbr,
      FinPeriodID = doc.FinPeriodID,
      DocDate = doc.DocDate,
      VendorID = doc.VendorID
    });
    det.APLastRefNbr = StubNbr;
  }

  private static void SetAdjustmentStubNumber(
    APPaymentEntry pe,
    APPayment doc,
    IAdjustmentStub adj,
    string StubNbr)
  {
    adj.StubNbr = StubNbr;
    adj.CashAccountID = doc.CashAccountID;
    adj.PaymentMethodID = doc.PaymentMethodID;
    if (!adj.Persistent)
      return;
    pe.Caches[adj.GetType()].Update((object) adj);
  }

  public static void AssignNumbersWithNoAdditionalProcessing(APPaymentEntry pe, APPayment doc)
  {
    PX.Objects.CA.PaymentMethod paymentMethod = (PX.Objects.CA.PaymentMethod) pe.paymenttype.Select((object) doc.PaymentMethodID);
    if (paymentMethod == null || paymentMethod.PrintOrExport.GetValueOrDefault())
    {
      pe.Document.Cache.MarkUpdated((object) doc);
    }
    else
    {
      pe.RowPersisting.RemoveHandler<APAdjust>(new PXRowPersisting(pe.APAdjust_RowPersisting));
      pe.RowUpdating.RemoveHandler<APAdjust>(new PXRowUpdating(pe.APAdjust_RowUpdating));
      pe.Clear();
      pe.Document.Current = (APPayment) pe.Document.Search<APPayment.refNbr>((object) doc.RefNbr, (object) doc.DocType);
      PaymentMethodAccount paymentMethodAccount = (PaymentMethodAccount) pe.cashaccountdetail.Select();
      pe.Document.Current.StubCntr = new int?(1);
      pe.Document.Current.BillCntr = new int?(0);
      pe.Document.Current.ExtRefNbr = doc.ExtRefNbr;
      foreach (AdjustmentStubGroup adjustmentsPrint in pe.GetAdjustmentsPrintList())
      {
        APPayment current = pe.Document.Current;
        int? billCntr = current.BillCntr;
        current.BillCntr = billCntr.HasValue ? new int?(billCntr.GetValueOrDefault() + 1) : new int?();
        foreach (IAdjustmentStub groupedStub in (IEnumerable<IAdjustmentStub>) adjustmentsPrint.GroupedStubs)
          APPrintChecks.SetAdjustmentStubNumber(pe, doc, groupedStub, doc.ExtRefNbr);
      }
      paymentMethodAccount.APLastRefNbr = doc.ExtRefNbr;
      pe.cashaccountdetail.Update(paymentMethodAccount);
      pe.Document.Current.Printed = new bool?(true);
      pe.Document.Current.Hold = new bool?(false);
      pe.Document.Update(pe.Document.Current);
    }
  }

  public virtual void PrintPayments(
    List<APPayment> list,
    PrintChecksFilter filter,
    PX.Objects.CA.PaymentMethod paymentMethod)
  {
    if (list.Count == 0)
      return;
    bool? nullable1 = paymentMethod.UseForAP;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = paymentMethod.APPrintChecks;
      if (nullable1.GetValueOrDefault() && string.IsNullOrEmpty(paymentMethod.APCheckReportID))
        throw new PXException("Payments cannot be processed. The '{0}' parameter is not specified for the '{1}' payment method on the Payment Methods (CA204000) form.", new object[2]
        {
          (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.CA.PaymentMethod.aPCheckReportID>(this.paymenttype.Cache),
          (object) paymentMethod.PaymentMethodID
        });
      nullable1 = paymentMethod.APPrintChecks;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = paymentMethod.APPrintRemittance;
        if (nullable1.GetValueOrDefault() && string.IsNullOrEmpty(paymentMethod.APRemittanceReportID))
          throw new PXException("Payments cannot be processed. The '{0}' parameter is not specified for the '{1}' payment method on the Payment Methods (CA204000) form.", new object[2]
          {
            (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.CA.PaymentMethod.aPRemittanceReportID>(this.paymenttype.Cache),
            (object) paymentMethod.PaymentMethodID
          });
      }
      if (!string.IsNullOrEmpty(this.payAccount.Current?.CashAccountCD))
        PaymentMethodAccountHelper.VerifyAPLastRefNbr(new bool?(true), filter.NextCheckNbr, this.payAccount.Current?.CashAccountCD, list.Count);
    }
    using (new LocalizationFeatureScope(OrganizationLocalizationHelper.GetCurrentLocalizationCodeForBranch(PXSelectorAttribute.Select<PrintChecksFilter.payAccountID>(this.Filter.Cache, (object) filter) is PX.Objects.CA.CashAccount cashAccount ? cashAccount.BranchID : new int?())))
    {
      bool printAdditionRemit = false;
      nullable1 = paymentMethod.APCreateBatchPayment;
      if (nullable1.GetValueOrDefault())
      {
        CABatchEntry instance1 = PXGraph.CreateInstance<CABatchEntry>();
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        string str = string.Empty;
        using (new CABatchEntry.PrintProcessContext(instance1))
        {
          CABatch caBatch = this.InitializeCABatch(filter, instance1);
          if (caBatch != null)
          {
            APPaymentEntry instance2 = PXGraph.CreateInstance<APPaymentEntry>();
            string nextCheckNbr = filter.NextCheckNbr;
            foreach (APPayment apPayment1 in list)
            {
              APPayment currentItem = apPayment1;
              int? cashAccountId1 = apPayment1.CashAccountID;
              int? cashAccountId2 = caBatch.CashAccountID;
              if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue) || apPayment1.PaymentMethodID != caBatch.PaymentMethodID)
                throw new PXException("One of the Payments in selection have wrong Cash Account or PaymentMethod");
              if (string.IsNullOrEmpty(apPayment1.ExtRefNbr) && string.IsNullOrEmpty(filter.NextCheckNbr))
                throw new PXException("Next Check Number is required to print AP Payments with 'Payment Ref.' empty");
              PXProcessing<APPayment>.SetCurrentItem((object) currentItem);
              try
              {
                APPayment apPayment2 = (APPayment) instance2.Document.Search<APPayment.refNbr>((object) currentItem.RefNbr, (object) currentItem.DocType);
                nullable1 = apPayment2.PrintCheck;
                if (!nullable1.GetValueOrDefault())
                  throw new PXException("The check cannot be printed because the Print Check check box is not selected on the Remittance Information tab of the Checks and Payments (AP302000) form");
                if (EnumerableExtensions.IsIn<string>(apPayment2.DocType, "CHK", "QCK", "PPM") && apPayment2.Status != "G")
                {
                  if (apPayment2.Status == "E")
                    throw new CheckIsPendingApprovalException();
                  throw new PXException("The check cannot be printed for this document. Checks can be printed only for documents that have the Pending Print status.");
                }
                this.AssignNumbers(instance2, apPayment2, ref nextCheckNbr, true);
                nullable1 = apPayment2.Passed;
                if (nullable1.GetValueOrDefault())
                  instance2.TimeStamp = apPayment2.tstamp;
                instance1.AddPayment(apPayment2, true);
                using (PXTransactionScope transactionScope = new PXTransactionScope())
                {
                  instance2.Save.Press();
                  apPayment2.tstamp = instance2.TimeStamp;
                  instance2.Clear();
                  instance1.Save.Press();
                  caBatch = instance1.Document.Current;
                  transactionScope.Complete();
                  PXProcessing<APPayment>.SetProcessed();
                }
              }
              catch (CheckIsPendingApprovalException ex)
              {
                PXProcessing<APPayment>.SetError((Exception) ex);
                flag2 = true;
                flag1 = true;
              }
              catch (AddendaCalculationException ex)
              {
                PXProcessing<APPayment>.SetError((Exception) ex);
                flag3 = true;
                str = ex.MessageNoPrefix;
                flag1 = true;
              }
              catch (PXException ex)
              {
                PXProcessing<APPayment>.SetError((Exception) ex);
                flag1 = true;
              }
            }
          }
          bool flag4 = instance1.BatchPayments.Select().Count > 0;
          if (flag1 & flag4)
          {
            if (flag2)
              throw new PXOperationCompletedWithErrorException("Some payments have not been added to the {0} batch because they have the Pending Approval status.", new object[1]
              {
                (object) caBatch.BatchNbr
              });
            throw new PXOperationCompletedWithErrorException("Some payments have not been added to the {0} batch.", new object[1]
            {
              (object) caBatch.BatchNbr
            });
          }
          if (flag1 && !flag4)
          {
            if (flag3)
              throw new PXOperationCompletedWithErrorException("Batch payment cannot be created because {0}", new object[1]
              {
                (object) str
              });
            throw new PXOperationCompletedWithErrorException();
          }
          if (this.IsImport)
            return;
          this.RedirectToResultWithCreateBatch(instance1.Document.Current);
        }
      }
      else
      {
        APReleaseChecks instance3 = PXGraph.CreateInstance<APReleaseChecks>();
        ReleaseChecksFilter copy = PXCache<ReleaseChecksFilter>.CreateCopy(instance3.Filter.Current);
        copy.BranchID = filter.BranchID;
        copy.PayAccountID = filter.PayAccountID;
        copy.PayTypeID = filter.PayTypeID;
        copy.CuryID = filter.CuryID;
        instance3.Filter.Cache.Update((object) copy);
        APPaymentEntry instance4 = PXGraph.CreateInstance<APPaymentEntry>();
        bool flag = false;
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        string NextCheckNbr = filter.NextCheckNbr;
        int num1 = 0;
        foreach (APPayment currentItem in list)
        {
          PXProcessing<APPayment>.SetCurrentItem((object) currentItem);
          string str1;
          try
          {
            str1 = NextCheckNbr;
            if (this.IsNextNumberDuplicated(filter, NextCheckNbr))
            {
              string str2 = NextCheckNbr;
              NextCheckNbr = AutoNumberAttribute.NextNumber(NextCheckNbr);
              throw new PXException("A check with the number '{0}' already exists in the system. Please enter another number.", new object[1]
              {
                (object) str2
              });
            }
            APPayment doc = (APPayment) instance4.Document.Search<APPayment.refNbr>((object) currentItem.RefNbr, (object) currentItem.DocType);
            nullable1 = doc.PrintCheck;
            if (!nullable1.GetValueOrDefault())
              throw new PXException("The check cannot be printed because the Print Check check box is not selected on the Remittance Information tab of the Checks and Payments (AP302000) form");
            if (EnumerableExtensions.IsIn<string>(doc.DocType, "CHK", "QCK", "PPM") && doc.Status != "G")
              throw new PXException("The check cannot be printed for this document. Checks can be printed only for documents that have the Pending Print status.");
            this.AssignNumbers(instance4, doc, ref NextCheckNbr);
            APPayment current = instance4.Document.Current;
            nullable1 = current.Passed;
            if (nullable1.GetValueOrDefault())
              instance4.TimeStamp = current.tstamp;
            instance4.Save.Press();
            current.tstamp = instance4.TimeStamp;
            instance4.Clear();
            APPayment apPayment = (APPayment) instance4.Document.Search<APPayment.refNbr>((object) current.RefNbr, (object) current.DocType);
            apPayment.Selected = new bool?(true);
            apPayment.Passed = new bool?(true);
            apPayment.tstamp = current.tstamp;
            instance3.APPaymentList.Cache.Update((object) apPayment);
            instance3.APPaymentList.Cache.SetStatus((object) apPayment, PXEntryStatus.Updated);
            int num2 = printAdditionRemit ? 1 : 0;
            int? billCntr = apPayment.BillCntr;
            short? apStubLines = paymentMethod.APStubLines;
            int? nullable2 = apStubLines.HasValue ? new int?((int) apStubLines.GetValueOrDefault()) : new int?();
            int num3 = billCntr.GetValueOrDefault() > nullable2.GetValueOrDefault() & billCntr.HasValue & nullable2.HasValue ? 1 : 0;
            printAdditionRemit = (num2 | num3) != 0;
            StringBuilder stringBuilder1 = new StringBuilder("APPayment.DocType");
            stringBuilder1.Append(Convert.ToString(num1));
            StringBuilder stringBuilder2 = new StringBuilder("APPayment.RefNbr");
            stringBuilder2.Append(Convert.ToString(num1));
            ++num1;
            dictionary[stringBuilder1.ToString()] = EnumerableExtensions.IsIn<string>(current.DocType, "PPM", "QCK") ? current.DocType : "CHK";
            dictionary[stringBuilder2.ToString()] = current.RefNbr;
            PXProcessing<APPayment>.SetProcessed();
          }
          catch (PXException ex)
          {
            PXProcessing<APPayment>.SetError((Exception) ex);
            flag = true;
            NextCheckNbr = str1;
          }
        }
        if (flag)
        {
          PXReportRequiredException inner = (PXReportRequiredException) null;
          if (dictionary.Count > 0)
          {
            dictionary["PrintFlag"] = "PRINT";
            inner = new PXReportRequiredException(dictionary, paymentMethod.APCheckReportID, PXBaseRedirectException.WindowMode.New, "Check");
          }
          throw new PXOperationCompletedWithErrorException("One or more documents could not be released.", (Exception) inner);
        }
        if (dictionary.Count <= 0)
          return;
        this.RedirectToResultNoBatch(instance3, dictionary, paymentMethod, printAdditionRemit, NextCheckNbr);
      }
    }
  }

  protected virtual CABatch InitializeCABatch(PrintChecksFilter filter, CABatchEntry be)
  {
    CABatch caBatch = be.Document.Insert(new CABatch());
    be.Document.Current = caBatch;
    CABatch copy = (CABatch) be.Document.Cache.CreateCopy((object) caBatch);
    copy.CashAccountID = filter.PayAccountID;
    copy.PaymentMethodID = filter.PayTypeID;
    be.Document.Update(copy);
    return be.Document.Current;
  }

  protected virtual void RedirectToResultWithCreateBatch(CABatch batch)
  {
    CABatchEntry instance = PXGraph.CreateInstance<CABatchEntry>();
    instance.Document.Current = (CABatch) instance.Document.Search<CABatch.batchNbr>((object) batch.BatchNbr);
    instance.SelectTimeStamp();
    throw new PXRedirectRequiredException((PXGraph) instance, "Redirect");
  }

  protected virtual void RedirectToResultNoBatch(
    APReleaseChecks pp,
    Dictionary<string, string> d,
    PX.Objects.CA.PaymentMethod paymentType,
    bool printAdditionRemit,
    string NextCheckNbr)
  {
    d["PrintFlag"] = "PRINT";
    PXReportRequiredException e = new PXReportRequiredException(d, paymentType.APCheckReportID, PXBaseRedirectException.WindowMode.New, "Check");
    if (((!paymentType.APPrintRemittance.GetValueOrDefault() ? 0 : (!string.IsNullOrEmpty(paymentType.APRemittanceReportID) ? 1 : 0)) & (printAdditionRemit ? 1 : 0)) != 0)
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>((IDictionary<string, string>) d)
      {
        ["StartCheckNbr"] = "",
        ["EndCheckNbr"] = NextCheckNbr
      };
      e.SeparateWindows = true;
      e.AddSibling(paymentType.APRemittanceReportID, parameters);
    }
    throw new PXRedirectWithReportException((PXGraph) pp, e, "Preview");
  }

  protected virtual void PrintChecksFilter_PayTypeID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.Filter.Cache.SetDefaultExt<PrintChecksFilter.payAccountID>(e.Row);
  }

  protected virtual void PrintChecksFilter_PayAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.Filter.Cache.SetDefaultExt<PrintChecksFilter.curyID>(e.Row);
    this.APPaymentList.Cache.Clear();
  }

  protected virtual void PrintChecksFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PrintChecksFilter oldRow = (PrintChecksFilter) e.OldRow;
    PrintChecksFilter row = (PrintChecksFilter) e.Row;
    if (oldRow.PayAccountID.HasValue || oldRow.PayTypeID != null)
    {
      int? payAccountId1 = oldRow.PayAccountID;
      int? payAccountId2 = row.PayAccountID;
      if (payAccountId1.GetValueOrDefault() == payAccountId2.GetValueOrDefault() & payAccountId1.HasValue == payAccountId2.HasValue && !(oldRow.PayTypeID != row.PayTypeID))
        goto label_12;
    }
    foreach (PXResult<APPayment> pxResult in this.APPaymentList.Select())
    {
      APPayment apPayment = (APPayment) pxResult;
      if (apPayment.Selected.GetValueOrDefault())
      {
        apPayment.Selected = new bool?(false);
        this.APPaymentList.Cache.Update((object) apPayment);
      }
    }
    ((PrintChecksFilter) e.Row).CurySelTotal = new Decimal?(0M);
    ((PrintChecksFilter) e.Row).SelTotal = new Decimal?(0M);
    ((PrintChecksFilter) e.Row).SelCount = new int?(0);
    ((PrintChecksFilter) e.Row).NextCheckNbr = (string) null;
label_12:
    if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<PrintChecksFilter.nextPaymentRefNbr>(sender, (object) row)))
      sender.RaiseExceptionHandling<PrintChecksFilter.nextPaymentRefNbr>((object) row, (object) row.NextCheckNbr, (Exception) null);
    if (string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<PrintChecksFilter.nextCheckNbr>(sender, (object) row)))
      return;
    sender.RaiseExceptionHandling<PrintChecksFilter.nextCheckNbr>((object) row, (object) row.NextCheckNbr, (Exception) null);
  }

  protected virtual void PrintChecksFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    bool flag1 = false;
    PrintChecksFilter filter = (PrintChecksFilter) e.Row;
    PXUIFieldAttribute.SetVisible<PrintChecksFilter.curyID>(sender, (object) null, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>());
    if (e.Row != null && this.cashaccountdetail.Current != null && (!object.Equals((object) this.cashaccountdetail.Current.CashAccountID, (object) filter.PayAccountID) || !object.Equals((object) this.cashaccountdetail.Current.PaymentMethodID, (object) filter.PayTypeID)))
    {
      this.cashaccountdetail.Current = (PaymentMethodAccount) null;
      flag1 = true;
    }
    if (e.Row != null && this.paymenttype.Current != null && !object.Equals((object) this.paymenttype.Current.PaymentMethodID, (object) filter.PayTypeID))
      this.paymenttype.Current = (PX.Objects.CA.PaymentMethod) null;
    if (e.Row != null && string.IsNullOrEmpty(filter.NextCheckNbr))
      flag1 = true;
    PX.Objects.CA.PaymentMethod current1 = this.paymenttype.Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.APCreateBatchPayment;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool isVisible1 = num1 != 0;
    PX.Objects.CA.PaymentMethod current2 = this.paymenttype.Current;
    int num2;
    if (current2 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = current2.APPrintChecks;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool isVisible2 = num2 != 0;
    PXUIFieldAttribute.SetVisible<PrintChecksFilter.nextCheckNbr>(sender, (object) null, isVisible2);
    PXUIFieldAttribute.SetVisible<PrintChecksFilter.nextPaymentRefNbr>(sender, (object) null, isVisible1);
    if (e.Row == null)
      return;
    int num3;
    if (this.cashaccountdetail.Current != null)
    {
      nullable = this.cashaccountdetail.Current.APAutoNextNbr;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    int num4 = flag1 ? 1 : 0;
    if ((num3 & num4) != 0)
    {
      this.ErrorMessageNextCheckNbr = (string) null;
      try
      {
        filter.NextCheckNbr = PaymentRefAttribute.GetNextPaymentRef(sender.Graph, this.cashaccountdetail.Current.CashAccountID, this.cashaccountdetail.Current.PaymentMethodID);
      }
      catch (AutoNumberException ex)
      {
        this.ErrorMessageNextCheckNbr = "The AP/PR Last Reference Number specified for the {0} payment method and the {1} cash account cannot be incremented. Update the last number on the Cash Accounts (CA202000) form.";
      }
    }
    int num5;
    if (this.paymenttype.Current != null)
    {
      nullable = this.paymenttype.Current.PrintOrExport;
      num5 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num5 = 1;
    bool flag2 = num5 != 0;
    bool flag3 = this.paymenttype.Current != null && this.paymenttype.Current.PaymentType == "EPP" && this.paymenttype.Current.ExternalPaymentProcessorID != null;
    bool isNextNumberDuplicated;
    this.VerifyNextCheckNbr(sender, filter, out isNextNumberDuplicated);
    this.APPaymentList.SetProcessEnabled(((!(!isNextNumberDuplicated & flag2) ? 0 : (filter.PayTypeID != null ? 1 : 0)) | (flag3 ? 1 : 0)) != 0);
    this.APPaymentList.SetProcessAllEnabled(((!(!isNextNumberDuplicated & flag2) ? 0 : (filter.PayTypeID != null ? 1 : 0)) | (flag3 ? 1 : 0)) != 0);
    PX.Objects.CA.PaymentMethod pt = this.paymenttype.Current;
    this.APPaymentList.SetParametersDelegate((PXProcessingBase<APPayment>.ParametersDelegate) (list =>
    {
      this.VerifyNextCheckNbr(sender, filter, out bool _, true);
      return true;
    }));
    bool isImport = this.IsImport;
    this.APPaymentList.SetProcessDelegate((PXProcessingBase<APPayment>.ProcessListDelegate) (list =>
    {
      APPrintChecks instance = PXGraph.CreateInstance<APPrintChecks>();
      instance.IsImport = isImport;
      instance.PrintPayments(list, filter, pt);
    }));
  }

  protected virtual void VerifyNextCheckNbr(
    PXCache sender,
    PrintChecksFilter row,
    out bool isNextNumberDuplicated,
    bool throwError = false)
  {
    PX.Objects.CA.PaymentMethod current1 = this.paymenttype.Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.APCreateBatchPayment;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    PX.Objects.CA.PaymentMethod current2 = this.paymenttype.Current;
    int num2;
    if (current2 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = current2.APPrintChecks;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag = num2 != 0;
    if (num1 != 0)
      this.VerifyNextCheckNbr<PrintChecksFilter.nextPaymentRefNbr>(sender, row, out isNextNumberDuplicated, throwError);
    else if (flag)
      this.VerifyNextCheckNbr<PrintChecksFilter.nextCheckNbr>(sender, row, out isNextNumberDuplicated, throwError);
    else
      isNextNumberDuplicated = false;
  }

  protected virtual void VerifyNextCheckNbr<T>(
    PXCache sender,
    PrintChecksFilter row,
    out bool isNextNumberDuplicated,
    bool throwError = false)
    where T : IBqlField
  {
    PXErrorLevel errorLevel = throwError ? PXErrorLevel.Error : PXErrorLevel.Warning;
    isNextNumberDuplicated = false;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    string errorOnly = PXUIFieldAttribute.GetErrorOnly<T>(sender, (object) row);
    if (!string.IsNullOrEmpty(this.ErrorMessageNextCheckNbr))
    {
      PX.Objects.CA.CashAccount cashAccount = new FbqlSelect<SelectFromBase<PX.Objects.CA.CashAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CA.CashAccount.cashAccountID, IBqlInt>.IsEqual<P.AsInt>>, PX.Objects.CA.CashAccount>.View((PXGraph) this).SelectSingle((object) this.cashaccountdetail.Current.CashAccountID);
      sender.DisplayFieldError<T>((object) row, this.ErrorMessageNextCheckNbr, (object) this.cashaccountdetail.Current.PaymentMethodID, (object) cashAccount?.CashAccountCD.Trim());
    }
    if (!string.IsNullOrEmpty(errorOnly))
    {
      isNextNumberDuplicated = (errorOnly.Equals(PXMessages.LocalizeFormatNoPrefix("A check with the number '{0}' already exists in the system. Please enter another number.", (object) row.NextCheckNbr)) ? 1 : 0) != 0;
    }
    else
    {
      bool? nullable;
      if (string.IsNullOrEmpty(errorOnly) && this.paymenttype.Current != null)
      {
        nullable = this.paymenttype.Current.PrintOrExport;
        if (nullable.GetValueOrDefault() && string.IsNullOrEmpty(row.NextCheckNbr))
        {
          propertyException = new PXSetPropertyException((IBqlTable) row, "Next Check Number is required to print AP Payments with 'Payment Ref.' empty", errorLevel);
          goto label_15;
        }
      }
      if (string.IsNullOrEmpty(errorOnly) && !string.IsNullOrEmpty(row.NextCheckNbr) && !AutoNumberAttribute.TryToGetNextNumber(row.NextCheckNbr))
      {
        string format = AutoNumberAttribute.CheckIfNumberEndsDigit(row.NextCheckNbr) ? "The {0} value cannot be incremented because the last possible value of the sequence has been reached." : "The value in the {0} box must end with a number.";
        propertyException = new PXSetPropertyException((IBqlTable) row, format, errorLevel, new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<T>(sender)
        });
      }
      else
      {
        if (this.paymenttype.Current != null)
        {
          nullable = this.paymenttype.Current.APPrintChecks;
          if (nullable.GetValueOrDefault() && (isNextNumberDuplicated = this.IsNextNumberDuplicated(row, row.NextCheckNbr)))
          {
            propertyException = new PXSetPropertyException((IBqlTable) row, "A check with the number '{0}' already exists in the system. Please enter another number.", new object[1]
            {
              (object) row.NextCheckNbr
            });
            goto label_15;
          }
        }
        int? selCount = row.SelCount;
        int num = 0;
        if (selCount.GetValueOrDefault() > num & selCount.HasValue)
        {
          string payTypeId = row.PayTypeID;
          int? payAccountId = row.PayAccountID;
          string message;
          ref string local = ref message;
          selCount = row.SelCount;
          int count = selCount.Value;
          string nextCheckNbr = row.NextCheckNbr;
          if (!PaymentMethodAccountHelper.TryToVerifyAPLastReferenceNumber((PXGraph) this, payTypeId, payAccountId, out local, count, nextCheckNbr))
            propertyException = new PXSetPropertyException((IBqlTable) row, message, errorLevel);
        }
      }
label_15:
      sender.RaiseExceptionHandling<T>((object) row, (object) row.NextCheckNbr, (Exception) propertyException);
      if (throwError && propertyException != null)
        throw propertyException;
    }
  }

  protected virtual void APPayment_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PrintChecksFilter current = this.Filter.Current;
    if (current == null)
      return;
    APPayment oldRow = e.OldRow as APPayment;
    APPayment row = e.Row as APPayment;
    PrintChecksFilter printChecksFilter1 = current;
    Decimal? curySelTotal = printChecksFilter1.CurySelTotal;
    Decimal? nullable1 = oldRow.Selected.GetValueOrDefault() ? oldRow.CuryOrigDocAmt : new Decimal?(0M);
    printChecksFilter1.CurySelTotal = curySelTotal.HasValue & nullable1.HasValue ? new Decimal?(curySelTotal.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    PrintChecksFilter printChecksFilter2 = current;
    nullable1 = printChecksFilter2.CurySelTotal;
    Decimal? nullable2 = row.Selected.GetValueOrDefault() ? row.CuryOrigDocAmt : new Decimal?(0M);
    printChecksFilter2.CurySelTotal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    PrintChecksFilter printChecksFilter3 = current;
    nullable2 = printChecksFilter3.SelTotal;
    nullable1 = oldRow.Selected.GetValueOrDefault() ? oldRow.OrigDocAmt : new Decimal?(0M);
    printChecksFilter3.SelTotal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    PrintChecksFilter printChecksFilter4 = current;
    nullable1 = printChecksFilter4.SelTotal;
    nullable2 = row.Selected.GetValueOrDefault() ? row.OrigDocAmt : new Decimal?(0M);
    printChecksFilter4.SelTotal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    PrintChecksFilter printChecksFilter5 = current;
    int? selCount = printChecksFilter5.SelCount;
    int num1 = oldRow.Selected.GetValueOrDefault() ? 1 : 0;
    printChecksFilter5.SelCount = selCount.HasValue ? new int?(selCount.GetValueOrDefault() - num1) : new int?();
    PrintChecksFilter printChecksFilter6 = current;
    selCount = printChecksFilter6.SelCount;
    int num2 = row.Selected.GetValueOrDefault() ? 1 : 0;
    printChecksFilter6.SelCount = selCount.HasValue ? new int?(selCount.GetValueOrDefault() + num2) : new int?();
  }

  public virtual bool IsNextNumberDuplicated(PrintChecksFilter filter, string nextNumber)
  {
    return PaymentRefAttribute.IsNextNumberDuplicated((PXGraph) this, filter.PayAccountID, filter.PayTypeID, nextNumber);
  }
}
