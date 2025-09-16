// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARIntegrityCheck
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.Overrides.ARDocumentRelease;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARIntegrityCheck : PXGraph<ARIntegrityCheck>
{
  public const string RECALCULATE_CUSTOMER_BALANCES_SCREEN_ID = "AR.50.99.00";
  public PXCancel<ARIntegrityCheckFilter> Cancel;
  public PXFilter<ARIntegrityCheckFilter> Filter;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  [PXFilterable(new Type[] {})]
  [PXViewDetailsButton(typeof (Customer.acctCD))]
  public PXFilteredProcessing<Customer, ARIntegrityCheckFilter, Where<Match<Current<AccessInfo.userName>>>> ARCustomerList;
  public PXSelect<Customer, Where<Customer.customerClassID, Equal<Current<ARIntegrityCheckFilter.customerClassID>>, And<Match<Current<AccessInfo.userName>>>>> Customer_ClassID;
  public PXSelect<Customer, Where<Match<Current<AccessInfo.userName>>>> Customers;

  protected virtual IEnumerable arcustomerlist()
  {
    if (((PXSelectBase<ARIntegrityCheckFilter>) this.Filter).Current != null && ((PXSelectBase<ARIntegrityCheckFilter>) this.Filter).Current.CustomerClassID != null)
    {
      using (new PXFieldScope(((PXSelectBase) this.Customer_ClassID).View, new Type[3]
      {
        typeof (Customer.bAccountID),
        typeof (Customer.acctCD),
        typeof (Customer.customerClassID)
      }))
        return (IEnumerable) ((PXSelectBase<Customer>) this.Customer_ClassID).SelectDelegateResult(Array.Empty<object>());
    }
    using (new PXFieldScope(((PXSelectBase) this.Customers).View, new Type[3]
    {
      typeof (Customer.bAccountID),
      typeof (Customer.acctCD),
      typeof (Customer.customerClassID)
    }))
      return (IEnumerable) ((PXSelectBase<Customer>) this.Customers).SelectDelegateResult(Array.Empty<object>());
  }

  [InjectDependency]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  public ARIntegrityCheck()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXProcessing<Customer>) this.ARCustomerList).SetProcessTooltip("Recalculate balances of customers and customer documents");
    ((PXProcessing<Customer>) this.ARCustomerList).SetProcessAllTooltip("Recalculate balances of customers and customer documents");
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARIntegrityCheckFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARIntegrityCheck.\u003C\u003Ec__DisplayClass13_0 cDisplayClass130 = new ARIntegrityCheck.\u003C\u003Ec__DisplayClass13_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.filter = ((PXSelectBase<ARIntegrityCheckFilter>) this.Filter).Current;
    bool flag = PXUIFieldAttribute.GetErrors(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARIntegrityCheckFilter>>) e).Cache, (object) null, new PXErrorLevel[2]
    {
      (PXErrorLevel) 4,
      (PXErrorLevel) 5
    }).Count > 0;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetRequired<ARIntegrityCheckFilter.finPeriodID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARIntegrityCheckFilter>>) e).Cache, cDisplayClass130.filter.RecalcDocumentBalances.GetValueOrDefault() || cDisplayClass130.filter.RecalcCustomerBalancesReleased.GetValueOrDefault());
    ((PXProcessing<Customer>) this.ARCustomerList).SetProcessEnabled(!flag);
    ((PXProcessing<Customer>) this.ARCustomerList).SetProcessAllEnabled(!flag);
    ((PXProcessingBase<Customer>) this.ARCustomerList).SuppressMerge = true;
    ((PXProcessingBase<Customer>) this.ARCustomerList).SuppressUpdate = true;
    if (((PXGraph) this).Accessinfo.ScreenID == "AR.50.99.00")
    {
      // ISSUE: method pointer
      ((PXProcessingBase<Customer>) this.ARCustomerList).SetProcessDelegate<ARIntegrityCheck>(new PXProcessingBase<Customer>.ProcessItemDelegate<ARIntegrityCheck>((object) cDisplayClass130, __methodptr(\u003C_\u003Eb__0)));
      // ISSUE: method pointer
      ((PXProcessingBase<Customer>) this.ARCustomerList).SetParametersDelegate(new PXProcessingBase<Customer>.ParametersDelegate((object) cDisplayClass130, __methodptr(\u003C_\u003Eb__1)));
    }
    else
    {
      // ISSUE: method pointer
      ((PXProcessingBase<Customer>) this.ARCustomerList).SetProcessDelegate<ARReleaseProcess>(new PXProcessingBase<Customer>.ProcessItemDelegate<ARReleaseProcess>((object) cDisplayClass130, __methodptr(\u003C_\u003Eb__2)));
      // ISSUE: method pointer
      ((PXProcessingBase<Customer>) this.ARCustomerList).SetParametersDelegate(new PXProcessingBase<Customer>.ParametersDelegate((object) cDisplayClass130, __methodptr(\u003C_\u003Eb__3)));
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARIntegrityCheckFilter> e)
  {
    if (e.Row == null)
      return;
    object finPeriodId = (object) e.Row.FinPeriodID;
    object valuePending = ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARIntegrityCheckFilter>>) e).Cache.GetValuePending<ARIntegrityCheckFilter.finPeriodID>((object) e.Row);
    bool? nullable = e.Row.RecalcDocumentBalances;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
    {
      nullable = e.Row.RecalcCustomerBalancesReleased;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARIntegrityCheckFilter>>) e).Cache.GetStateExt<ARIntegrityCheckFilter.finPeriodID>((object) e.Row) is PXFieldState stateExt && stateExt.ErrorLevel >= 4 && finPeriodId != null && valuePending == null)
        {
          e.Row.FinPeriodID = (string) null;
          ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARIntegrityCheckFilter>>) e).Cache.RaiseExceptionHandling<ARIntegrityCheckFilter.finPeriodID>((object) e.Row, (object) null, (Exception) null);
        }
        nullable = e.Row.RecalcCustomerBalancesUnreleased;
        bool flag3 = false;
        if (!(nullable.GetValueOrDefault() == flag3 & nullable.HasValue))
          return;
        ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARIntegrityCheckFilter>>) e).Cache.RaiseExceptionHandling<ARIntegrityCheckFilter.recalcDocumentBalances>((object) e.Row, (object) false, (Exception) new PXSetPropertyException("Specify which balances should be recalculated by selecting the needed check boxes in the Selection area."));
        return;
      }
    }
    try
    {
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARIntegrityCheckFilter>>) e).Cache.RaiseFieldVerifying<ARIntegrityCheckFilter.finPeriodID>((object) e.Row, ref finPeriodId);
    }
    catch (PXSetPropertyException ex)
    {
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARIntegrityCheckFilter>>) e).Cache.RaiseExceptionHandling<ARIntegrityCheckFilter.finPeriodID>((object) e.Row, (object) e.Row.FinPeriodID, (Exception) ex);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARIntegrityCheckFilter.recalcDocumentBalances> e)
  {
    if (e.Row == null || e.NewValue == null || !(bool) e.NewValue)
      return;
    ((ARIntegrityCheckFilter) e.Row).RecalcCustomerBalancesReleased = new bool?(true);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ARIntegrityCheckFilter.finPeriodID> e)
  {
    if (e.Row == null)
      return;
    ARIntegrityCheckFilter row = (ARIntegrityCheckFilter) e.Row;
    bool? nullable = row.RecalcDocumentBalances;
    bool flag1 = false;
    if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
      return;
    nullable = row.RecalcCustomerBalancesReleased;
    bool flag2 = false;
    if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARIntegrityCheckFilter.finPeriodID>>) e).Cancel = true;
  }

  protected virtual void IntegrityCheckProc(Customer cust, ARIntegrityCheckFilter filter)
  {
    string minPeriod = this.GetMinPeriod(cust, filter.FinPeriodID);
    if (filter.RecalcDocumentBalances.GetValueOrDefault())
    {
      this.UpdateDocumentBalances(cust, minPeriod);
      this.ReopenRetainageDocuments(cust, minPeriod);
      this.UpdateARTranRetainageBalance(cust, minPeriod);
      this.UpdateARTranBalances(cust, minPeriod);
      this.ReopenPaymentsByLinesDocuments(cust, minPeriod);
      ARIntegrityCheck.ReopenDocumentsHavingPendingApplications((PXGraph) this, cust, minPeriod);
    }
    if (filter.RecalcCustomerBalancesReleased.GetValueOrDefault())
    {
      this.FillMissedARHistory(cust, minPeriod);
      this.FillMissedCuryARHistory(cust, minPeriod);
      this.FixPtdARHistory(cust, minPeriod);
      this.FixPtdCuryARHistory(cust, minPeriod);
      this.FixYtdARHistory(cust, minPeriod);
      this.FixYtdCuryARHistory(cust, minPeriod);
      this.UpdateARBalancesCurrent(cust);
    }
    if (!filter.RecalcCustomerBalancesUnreleased.GetValueOrDefault())
      return;
    this.UpdateARBalancesUnreleased(cust);
    this.UpdateARBalancesOpenOrders(cust);
  }

  protected virtual string GetMinPeriod(Customer cust, string startPeriod)
  {
    string strB = "190001";
    ARHistoryDetDeleted historyDetDeleted = PXResultset<ARHistoryDetDeleted>.op_Implicit(PXSelectBase<ARHistoryDetDeleted, PXSelectGroupBy<ARHistoryDetDeleted, Where<ARHistoryDetDeleted.customerID, Equal<Required<Customer.bAccountID>>>, Aggregate<Max<ARHistoryDetDeleted.finPeriodID>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) cust.BAccountID
    }));
    ARReleaseProcess instance = PXGraph.CreateInstance<ARReleaseProcess>();
    if (historyDetDeleted != null && historyDetDeleted.FinPeriodID != null)
      strB = instance.FinPeriodRepository.GetOffsetPeriodId(historyDetDeleted.FinPeriodID, 1, new int?(0));
    if (!string.IsNullOrEmpty(startPeriod) && string.Compare(startPeriod, strB) > 0)
      strB = startPeriod;
    return strB;
  }

  protected virtual void UpdateDocumentBalances(Customer cust, string finPeriod)
  {
    DateTime date = new PXDBLastChangeDateTimeAttribute(typeof (ARRegister.lastModifiedDateTime)).GetDate();
    PXUpdateJoin<Set<ARRegister.docBal, IIf<Where<ARRegisterTranPostGLGrouped.docType, Equal<ARDocType.prepaymentInvoice>>, IIf<Where<ARRegisterTranPostGLGrouped.calcDebitARAmt, NotEqual<Zero>>, ARRegisterTranPostGLGrouped.calcDebitARAmt, Minus<ARRegisterTranPostGLGrouped.calcCreditARAmt>>, ARRegisterTranPostGLGrouped.calcBalance>, Set<ARRegister.curyDocBal, IIf<Where<ARRegisterTranPostGLGrouped.docType, Equal<ARDocType.prepaymentInvoice>>, IIf<Where<ARRegisterTranPostGLGrouped.calcCuryDebitARAmt, NotEqual<Zero>>, ARRegisterTranPostGLGrouped.calcCuryDebitARAmt, Minus<ARRegisterTranPostGLGrouped.calcCuryCreditARAmt>>, ARRegisterTranPostGLGrouped.calcCuryBalance>, Set<ARRegister.openDoc, Switch<Case<Where<ARRegisterTranPostGLGrouped.docType, Equal<ARDocType.prepaymentInvoice>>, IIf<Where<ARRegisterTranPostGLGrouped.calcCreditARAmt, Equal<Zero>>, False, True>, Case<Where<ARRegisterTranPostGLGrouped.calcBalance, Equal<Zero>>, False>>, True>, Set<ARRegister.rGOLAmt, ARRegisterTranPostGLGrouped.calcRGOL, Set<ARRegister.curyRetainageReleased, ARRegisterTranPostGLGrouped.calcCuryRetainageReleased, Set<ARRegister.retainageReleased, ARRegisterTranPostGLGrouped.calcRetainageReleased, Set<ARRegister.curyRetainageUnreleasedAmt, ARRegisterTranPostGLGrouped.calcCuryRetainageUnreleased, Set<ARRegister.retainageUnreleasedAmt, ARRegisterTranPostGLGrouped.calcRetainageUnreleased, Set<ARRegister.curyRetainageUnpaidTotal, ARRegisterTranPostGLGrouped.calcCuryRetainageUnpaidTotal, Set<ARRegister.retainageUnpaidTotal, ARRegisterTranPostGLGrouped.calcRetainageUnpaidTotal, Set<ARRegister.status, Switch<Case<Where<ARRegisterTranPostGLGrouped.calcBalance, Equal<Zero>, And<ARRegisterTranPostGLGrouped.voided, Equal<True>>>, ARDocStatus.voided, Case<Where<ARRegisterTranPostGLGrouped.canceled, Equal<True>>, ARDocStatus.canceled, Case<Where<ARRegisterTranPostGLGrouped.pendingPayment, Equal<True>>, ARDocStatus.pendingPayment, Case<Where<ARRegisterTranPostGLGrouped.hold, Equal<True>>, ARDocStatus.reserved, Case<Where<ARRegisterTranPostGLGrouped.calcBalance, Equal<Zero>>, ARDocStatus.closed>>>>>, IIf<Where<ARRegister.docType, Equal<ARDocType.prepaymentInvoice>>, ARDocStatus.unapplied, ARDocStatus.open>>, Set<ARRegister.closedFinPeriodID, Switch<Case<Where<ARRegisterTranPostGLGrouped.docType, Equal<ARDocType.prepaymentInvoice>>, IIf<Where<ARRegisterTranPostGLGrouped.calcCreditARAmt, Equal<Zero>>, ARRegisterTranPostGLGrouped.maxFinPeriodID, Null>, Case<Where<ARRegisterTranPostGLGrouped.calcBalance, Equal<Zero>>, ARRegisterTranPostGLGrouped.maxFinPeriodID>>, Null>, Set<ARRegister.closedTranPeriodID, Switch<Case<Where<ARRegisterTranPostGLGrouped.docType, Equal<ARDocType.prepaymentInvoice>>, IIf<Where<ARRegisterTranPostGLGrouped.calcCreditARAmt, Equal<Zero>>, ARRegisterTranPostGLGrouped.maxTranPeriodID, Null>, Case<Where<ARRegisterTranPostGLGrouped.calcBalance, Equal<Zero>>, ARRegisterTranPostGLGrouped.maxTranPeriodID>>, Null>, Set<ARRegister.closedDate, Switch<Case<Where<ARRegisterTranPostGLGrouped.docType, Equal<ARDocType.prepaymentInvoice>>, IIf<Where<ARRegisterTranPostGLGrouped.calcCreditARAmt, Equal<Zero>>, ARRegisterTranPostGLGrouped.maxDocDate, Null>, Case<Where<ARRegisterTranPostGLGrouped.calcBalance, Equal<Zero>>, ARRegisterTranPostGLGrouped.maxDocDate>>, Null>, Set<ARRegister.lastModifiedByID, Required<ARRegister.lastModifiedByID>, Set<ARRegister.lastModifiedByScreenID, Required<ARRegister.lastModifiedByScreenID>, Set<ARRegister.lastModifiedDateTime, Required<ARRegister.lastModifiedDateTime>>>>>>>>>>>>>>>>>>, ARRegister, InnerJoin<ARRegisterTranPostGLGrouped, On<ARRegisterTranPostGLGrouped.docType, Equal<ARRegister.docType>, And<ARRegisterTranPostGLGrouped.refNbr, Equal<ARRegister.refNbr>, And<ARRegisterTranPostGLGrouped.customerID, Equal<Required<ARRegister.customerID>>, And<ARRegister.customerID, Equal<Required<ARRegister.customerID>>, And<ARRegister.tranPeriodID, GreaterEqual<Required<ARRegister.tranPeriodID>>>>>>>>>.Update((PXGraph) this, new object[6]
    {
      (object) this._currentUserInformationProvider.GetUserIdOrDefault(),
      (object) (((PXGraph) this).Accessinfo.ScreenID?.Replace(".", "") ?? "00000000"),
      (object) date,
      (object) cust.BAccountID,
      (object) cust.BAccountID,
      (object) finPeriod
    });
  }

  protected virtual void ReopenRetainageDocuments(Customer cust, string finPeriod)
  {
    PXUpdateJoin<Set<ARRegister.status, ARDocStatus.open, Set<ARRegister.openDoc, True, Set<ARRegister.closedFinPeriodID, Null, Set<ARRegister.closedTranPeriodID, Null, Set<ARRegister.closedDate, Null>>>>>, ARRegister, LeftJoin<OriginalRetainageDocumentsWithOpenBalance, On<OriginalRetainageDocumentsWithOpenBalance.origDocType, Equal<ARRegister.docType>, And<OriginalRetainageDocumentsWithOpenBalance.origRefNbr, Equal<ARRegister.refNbr>>>>, Where<ARRegister.retainageApply, Equal<True>, And<ARRegister.status, Equal<ARDocStatus.closed>, And<ARRegister.customerID, Equal<Required<ARRegister.customerID>>, And<ARRegister.tranPeriodID, GreaterEqual<Required<ARRegister.tranPeriodID>>, And<Where<ARRegister.curyRetainageUnreleasedAmt, NotEqual<Zero>, Or<OriginalRetainageDocumentsWithOpenBalance.origRefNbr, IsNotNull>>>>>>>>.Update((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) finPeriod
    });
  }

  protected virtual void ReopenPaymentsByLinesDocuments(Customer cust, string finPeriod)
  {
    PXUpdate<Set<ARRegister.status, ARDocStatus.open, Set<ARRegister.openDoc, True, Set<ARRegister.closedFinPeriodID, Null, Set<ARRegister.closedTranPeriodID, Null, Set<ARRegister.closedDate, Null>>>>>, ARRegister, Where<ARRegister.paymentsByLinesAllowed, Equal<True>, And<ARRegister.status, Equal<ARDocStatus.closed>, And<ARRegister.customerID, Equal<Required<ARRegister.customerID>>, And<ARRegister.tranPeriodID, GreaterEqual<Required<ARRegister.tranPeriodID>>, And<Exists<Select<ARTran, Where<ARTran.tranType, Equal<ARRegister.docType>, And<ARTran.refNbr, Equal<ARRegister.refNbr>, And<ARTran.curyTranBal, NotEqual<Zero>>>>>>>>>>>>.Update((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) finPeriod
    });
  }

  protected virtual void UpdateARTranRetainageBalance(Customer cust, string finPeriod)
  {
    PXUpdateJoin<Set<ARTran.retainageBal, Switch<Case<Where<ARRegister.isRetainageReversing, Equal<True>>, decimal0, Case<Where<BqlOperand<ARTran.tranType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO, ARDocType.cashSale>>, Sub<ARTran.origRetainageAmt, IsNull<ARTranRetainageReleasedTotal.retainageReleased, Zero>>>>, Add<ARTran.origRetainageAmt, IsNull<ARTranRetainageReleasedTotal.retainageReleased, Zero>>>, Set<ARTran.curyRetainageBal, Switch<Case<Where<ARRegister.isRetainageReversing, Equal<True>>, decimal0, Case<Where<BqlOperand<ARTran.tranType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO, ARDocType.cashSale>>, Sub<ARTran.curyOrigRetainageAmt, IsNull<ARTranRetainageReleasedTotal.curyRetainageReleased, Zero>>>>, Add<ARTran.curyOrigRetainageAmt, IsNull<ARTranRetainageReleasedTotal.curyRetainageReleased, Zero>>>>>, ARTran, InnerJoin<ARRegister, On<ARRegister.docType, Equal<ARTran.tranType>, And<ARRegister.refNbr, Equal<ARTran.refNbr>, And<ARRegister.retainageApply, Equal<True>, And<ARRegister.paymentsByLinesAllowed, Equal<True>, And<ARRegister.released, Equal<True>>>>>>, LeftJoin<ARTranRetainageReleasedTotal, On<ARTranRetainageReleasedTotal.origDocType, Equal<ARTran.tranType>, And<ARTranRetainageReleasedTotal.origRefNbr, Equal<ARTran.refNbr>, And<ARTranRetainageReleasedTotal.origLineNbr, Equal<ARTran.lineNbr>>>>>>, Where<ARTran.customerID, Equal<Required<ARTran.customerID>>, And<ARTran.tranPeriodID, GreaterEqual<Required<ARTran.tranPeriodID>>, And<ARTran.released, Equal<True>>>>>.Update((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) finPeriod
    });
  }

  protected virtual void UpdateARTranBalances(Customer cust, string finPeriod)
  {
    DateTime date = new PXDBLastChangeDateTimeAttribute(typeof (ARTran.lastModifiedDateTime)).GetDate();
    PXUpdateJoin<Set<ARTran.curyTranBal, Sub<ARTran.curyOrigTranAmt, IsNull<ARTranApplicationsTotal.curyAppBalanceTotal, Zero>>, Set<ARTran.tranBal, Sub<ARTran.origTranAmt, IsNull<ARTranApplicationsTotal.appBalanceTotal, Zero>>, Set<ARTran.lastModifiedByID, Required<ARTran.lastModifiedByID>, Set<ARTran.lastModifiedByScreenID, Required<ARTran.lastModifiedByScreenID>, Set<ARTran.lastModifiedDateTime, Required<ARTran.lastModifiedDateTime>>>>>>, ARTran, LeftJoin<ARTranApplicationsTotal, On<ARTranApplicationsTotal.tranType, Equal<ARTran.tranType>, And<ARTranApplicationsTotal.refNbr, Equal<ARTran.refNbr>, And<ARTranApplicationsTotal.lineNbr, Equal<ARTran.lineNbr>>>>>, Where<ARTran.customerID, Equal<Required<ARTran.customerID>>, And<ARTran.tranPeriodID, GreaterEqual<Required<ARTran.tranPeriodID>>, And<ARTran.released, Equal<True>>>>>.Update((PXGraph) this, new object[5]
    {
      (object) this._currentUserInformationProvider.GetUserIdOrDefault(),
      (object) (((PXGraph) this).Accessinfo.ScreenID?.Replace(".", "") ?? "00000000"),
      (object) date,
      (object) cust.BAccountID,
      (object) finPeriod
    });
  }

  private static void ReopenDocumentsHavingPendingApplications(
    PXGraph graph,
    Customer customer,
    string finPeriod)
  {
    PXUpdate<Set<ARRegister.openDoc, True>, ARRegister, Where<ARRegister.openDoc, Equal<False>, And<ARRegister.customerID, Equal<Required<ARRegister.customerID>>, And<ARRegister.tranPeriodID, GreaterEqual<Required<ARRegister.tranPeriodID>>, And<Exists<Select<ARAdjust, Where<ARAdjust.released, Equal<False>, And<ARAdjust.adjgDocType, Equal<ARRegister.docType>, And<ARAdjust.adjgRefNbr, Equal<ARRegister.refNbr>>>>>>>>>>>.Update(graph, new object[2]
    {
      (object) customer.BAccountID,
      (object) finPeriod
    });
    PXUpdate<Set<ARRegister.status, IIf<Where<ARRegister.docType, Equal<ARDocType.prepaymentInvoice>>, ARDocStatus.unapplied, ARDocStatus.open>>, ARRegister, Where<ARRegister.status, Equal<ARDocStatus.closed>, And<ARRegister.customerID, Equal<Required<ARRegister.customerID>>, And<ARRegister.tranPeriodID, GreaterEqual<Required<ARRegister.tranPeriodID>>, And<ARRegister.openDoc, Equal<True>>>>>>.Update(graph, new object[2]
    {
      (object) customer.BAccountID,
      (object) finPeriod
    });
  }

  protected virtual void FixPtdARHistory(Customer cust, string finPeriod)
  {
    PXUpdateJoin<Set<ARHistory.finPtdSales, ARHistoryFinGrouped.finPtdSalesSum, Set<ARHistory.finPtdPayments, ARHistoryFinGrouped.finPtdPaymentsSum, Set<ARHistory.finPtdDrAdjustments, ARHistoryFinGrouped.finPtdDrAdjustmentsSum, Set<ARHistory.finPtdCrAdjustments, ARHistoryFinGrouped.finPtdCrAdjustmentsSum, Set<ARHistory.finPtdDiscounts, ARHistoryFinGrouped.finPtdDiscountsSum, Set<ARHistory.finPtdRGOL, ARHistoryFinGrouped.finPtdRGOLSum, Set<ARHistory.finPtdFinCharges, ARHistoryFinGrouped.finPtdFinChargesSum, Set<ARHistory.finPtdDeposits, ARHistoryFinGrouped.finPtdDepositsSum, Set<ARHistory.finPtdRetainageWithheld, ARHistoryFinGrouped.finPtdRetainageWithheldSum, Set<ARHistory.finPtdRetainageReleased, ARHistoryFinGrouped.finPtdRetainageReleasedSum>>>>>>>>>>, ARHistory, LeftJoin<PX.Objects.GL.Branch, On<ARHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<ARHistory.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, InnerJoin<ARHistoryFinGrouped, On<ARHistory.customerID, Equal<Required<ARHistory.customerID>>, And<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.finPeriodID>>, And<ARHistoryFinGrouped.branchID, Equal<ARHistory.branchID>, And<ARHistoryFinGrouped.customerID, Equal<Required<ARHistory.customerID>>, And<ARHistoryFinGrouped.accountID, Equal<ARHistory.accountID>, And<ARHistoryFinGrouped.subID, Equal<ARHistory.subID>, And<ARHistoryFinGrouped.finPeriodID, Equal<ARHistory.finPeriodID>>>>>>>>>>>>.Update((PXGraph) this, new object[3]
    {
      (object) cust.BAccountID,
      (object) finPeriod,
      (object) cust.BAccountID
    });
    PXUpdateJoin<Set<ARHistory.tranPtdSales, ARHistoryTranGrouped.tranPtdSalesSum, Set<ARHistory.tranPtdPayments, ARHistoryTranGrouped.tranPtdPaymentsSum, Set<ARHistory.tranPtdDrAdjustments, ARHistoryTranGrouped.tranPtdDrAdjustmentsSum, Set<ARHistory.tranPtdCrAdjustments, ARHistoryTranGrouped.tranPtdCrAdjustmentsSum, Set<ARHistory.tranPtdDiscounts, ARHistoryTranGrouped.tranPtdDiscountsSum, Set<ARHistory.tranPtdRGOL, ARHistoryTranGrouped.tranPtdRGOLSum, Set<ARHistory.tranPtdFinCharges, ARHistoryTranGrouped.tranPtdFinChargesSum, Set<ARHistory.tranPtdDeposits, ARHistoryTranGrouped.tranPtdDepositsSum, Set<ARHistory.tranPtdRetainageWithheld, ARHistoryTranGrouped.tranPtdRetainageWithheldSum, Set<ARHistory.tranPtdRetainageReleased, ARHistoryTranGrouped.tranPtdRetainageReleasedSum>>>>>>>>>>, ARHistory, InnerJoin<ARHistoryTranGrouped, On<ARHistory.customerID, Equal<Required<ARHistory.customerID>>, And<ARHistory.finPeriodID, GreaterEqual<Required<ARHistory.finPeriodID>>, And<ARHistoryTranGrouped.branchID, Equal<ARHistory.branchID>, And<ARHistoryTranGrouped.customerID, Equal<Required<ARHistory.customerID>>, And<ARHistoryTranGrouped.accountID, Equal<ARHistory.accountID>, And<ARHistoryTranGrouped.subID, Equal<ARHistory.subID>, And<ARHistoryTranGrouped.finPeriodID, Equal<ARHistory.finPeriodID>>>>>>>>>>.Update((PXGraph) this, new object[3]
    {
      (object) cust.BAccountID,
      (object) finPeriod,
      (object) cust.BAccountID
    });
  }

  protected virtual void FixPtdCuryARHistory(Customer cust, string finPeriod)
  {
    PXUpdateJoin<Set<CuryARHistory.finPtdSales, CuryARHistoryFinGrouped.finPtdSalesSum, Set<CuryARHistory.finPtdPayments, CuryARHistoryFinGrouped.finPtdPaymentsSum, Set<CuryARHistory.finPtdDrAdjustments, CuryARHistoryFinGrouped.finPtdDrAdjustmentsSum, Set<CuryARHistory.finPtdCrAdjustments, CuryARHistoryFinGrouped.finPtdCrAdjustmentsSum, Set<CuryARHistory.finPtdDiscounts, CuryARHistoryFinGrouped.finPtdDiscountsSum, Set<CuryARHistory.finPtdRGOL, CuryARHistoryFinGrouped.finPtdRGOLSum, Set<CuryARHistory.finPtdFinCharges, CuryARHistoryFinGrouped.finPtdFinChargesSum, Set<CuryARHistory.finPtdDeposits, CuryARHistoryFinGrouped.finPtdDepositsSum, Set<CuryARHistory.finPtdRetainageWithheld, CuryARHistoryFinGrouped.finPtdRetainageWithheldSum, Set<CuryARHistory.finPtdRetainageReleased, CuryARHistoryFinGrouped.finPtdRetainageReleasedSum, Set<CuryARHistory.curyFinPtdSales, CuryARHistoryFinGrouped.curyFinPtdSalesSum, Set<CuryARHistory.curyFinPtdPayments, CuryARHistoryFinGrouped.curyFinPtdPaymentsSum, Set<CuryARHistory.curyFinPtdDrAdjustments, CuryARHistoryFinGrouped.curyFinPtdDrAdjustmentsSum, Set<CuryARHistory.curyFinPtdCrAdjustments, CuryARHistoryFinGrouped.curyFinPtdCrAdjustmentsSum, Set<CuryARHistory.curyFinPtdFinCharges, CuryARHistoryFinGrouped.curyFinPtdFinChargesSum, Set<CuryARHistory.curyFinPtdDiscounts, CuryARHistoryFinGrouped.curyFinPtdDiscountsSum, Set<CuryARHistory.curyFinPtdDeposits, CuryARHistoryFinGrouped.curyFinPtdDepositsSum, Set<CuryARHistory.curyFinPtdRetainageWithheld, CuryARHistoryFinGrouped.curyFinPtdRetainageWithheldSum, Set<CuryARHistory.curyFinPtdRetainageReleased, CuryARHistoryFinGrouped.curyFinPtdRetainageReleasedSum>>>>>>>>>>>>>>>>>>>, CuryARHistory, LeftJoin<PX.Objects.GL.Branch, On<CuryARHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<CuryARHistory.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, InnerJoin<CuryARHistoryFinGrouped, On<CuryARHistory.customerID, Equal<Required<CuryARHistory.customerID>>, And<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.finPeriodID>>, And<CuryARHistoryFinGrouped.branchID, Equal<CuryARHistory.branchID>, And<CuryARHistoryFinGrouped.customerID, Equal<Required<ARHistory.customerID>>, And<CuryARHistoryFinGrouped.accountID, Equal<CuryARHistory.accountID>, And<CuryARHistoryFinGrouped.subID, Equal<CuryARHistory.subID>, And<CuryARHistoryFinGrouped.curyID, Equal<CuryARHistory.curyID>, And<CuryARHistoryFinGrouped.finPeriodID, Equal<CuryARHistory.finPeriodID>>>>>>>>>>>>>.Update((PXGraph) this, new object[3]
    {
      (object) cust.BAccountID,
      (object) finPeriod,
      (object) cust.BAccountID
    });
    PXUpdateJoin<Set<CuryARHistory.tranPtdSales, CuryARHistoryTranGrouped.tranPtdSalesSum, Set<CuryARHistory.tranPtdPayments, CuryARHistoryTranGrouped.tranPtdPaymentsSum, Set<CuryARHistory.tranPtdDrAdjustments, CuryARHistoryTranGrouped.tranPtdDrAdjustmentsSum, Set<CuryARHistory.tranPtdCrAdjustments, CuryARHistoryTranGrouped.tranPtdCrAdjustmentsSum, Set<CuryARHistory.tranPtdDiscounts, CuryARHistoryTranGrouped.tranPtdDiscountsSum, Set<CuryARHistory.tranPtdRGOL, CuryARHistoryTranGrouped.tranPtdRGOLSum, Set<CuryARHistory.tranPtdFinCharges, CuryARHistoryTranGrouped.tranPtdFinChargesSum, Set<CuryARHistory.tranPtdDeposits, CuryARHistoryTranGrouped.tranPtdDepositsSum, Set<CuryARHistory.tranPtdRetainageWithheld, CuryARHistoryTranGrouped.tranPtdRetainageWithheldSum, Set<CuryARHistory.tranPtdRetainageReleased, CuryARHistoryTranGrouped.tranPtdRetainageReleasedSum, Set<CuryARHistory.curyTranPtdSales, CuryARHistoryTranGrouped.curyTranPtdSalesSum, Set<CuryARHistory.curyTranPtdPayments, CuryARHistoryTranGrouped.curyTranPtdPaymentsSum, Set<CuryARHistory.curyTranPtdDrAdjustments, CuryARHistoryTranGrouped.curyTranPtdDrAdjustmentsSum, Set<CuryARHistory.curyTranPtdCrAdjustments, CuryARHistoryTranGrouped.curyTranPtdCrAdjustmentsSum, Set<CuryARHistory.curyTranPtdDiscounts, CuryARHistoryTranGrouped.curyTranPtdDiscountsSum, Set<CuryARHistory.curyTranPtdFinCharges, CuryARHistoryTranGrouped.curyTranPtdFinChargesSum, Set<CuryARHistory.curyTranPtdDeposits, CuryARHistoryTranGrouped.curyTranPtdDepositsSum, Set<CuryARHistory.curyTranPtdRetainageWithheld, CuryARHistoryTranGrouped.curyTranPtdRetainageWithheldSum, Set<CuryARHistory.curyTranPtdRetainageReleased, CuryARHistoryTranGrouped.curyTranPtdRetainageReleasedSum>>>>>>>>>>>>>>>>>>>, CuryARHistory, InnerJoin<CuryARHistoryTranGrouped, On<CuryARHistory.customerID, Equal<Required<CuryARHistory.customerID>>, And<CuryARHistory.finPeriodID, GreaterEqual<Required<CuryARHistory.finPeriodID>>, And<CuryARHistoryTranGrouped.branchID, Equal<CuryARHistory.branchID>, And<CuryARHistoryTranGrouped.customerID, Equal<Required<ARHistory.customerID>>, And<CuryARHistoryTranGrouped.accountID, Equal<CuryARHistory.accountID>, And<CuryARHistoryTranGrouped.subID, Equal<CuryARHistory.subID>, And<CuryARHistoryTranGrouped.curyID, Equal<CuryARHistory.curyID>, And<CuryARHistoryTranGrouped.finPeriodID, Equal<CuryARHistory.finPeriodID>>>>>>>>>>>.Update((PXGraph) this, new object[3]
    {
      (object) cust.BAccountID,
      (object) finPeriod,
      (object) cust.BAccountID
    });
  }

  protected virtual void FixYtdARHistory(Customer cust, string finPeriod)
  {
    PXUpdateJoin<Set<ARHistory.finBegBalance, BqlFunctionMirror<Sub<Add<ARHistoryYtdGrouped.finPtdSalesSum, BqlOperand<ARHistoryYtdGrouped.finPtdDrAdjustmentsSum, IBqlDecimal>.Add<ARHistoryYtdGrouped.finPtdFinChargesSum>>, BqlOperand<ARHistoryYtdGrouped.finPtdPaymentsSum, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.finPtdCrAdjustmentsSum, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.finPtdDiscountsSum, IBqlDecimal>.Add<ARHistoryYtdGrouped.finPtdRGOLSum>>>>, IBqlDecimal>.Subtract<BqlFunction<Add<ARHistoryYtdGrouped.finPtdSales, BqlOperand<ARHistoryYtdGrouped.finPtdDrAdjustments, IBqlDecimal>.Add<ARHistoryYtdGrouped.finPtdFinCharges>>, IBqlDecimal>.Subtract<BqlOperand<ARHistoryYtdGrouped.finPtdPayments, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.finPtdCrAdjustments, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.finPtdDiscounts, IBqlDecimal>.Add<ARHistoryYtdGrouped.finPtdRGOL>>>>>, Set<ARHistory.finYtdBalance, BqlFunction<Add<ARHistoryYtdGrouped.finPtdSalesSum, BqlOperand<ARHistoryYtdGrouped.finPtdDrAdjustmentsSum, IBqlDecimal>.Add<ARHistoryYtdGrouped.finPtdFinChargesSum>>, IBqlDecimal>.Subtract<BqlOperand<ARHistoryYtdGrouped.finPtdPaymentsSum, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.finPtdCrAdjustmentsSum, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.finPtdDiscountsSum, IBqlDecimal>.Add<ARHistoryYtdGrouped.finPtdRGOLSum>>>>, Set<ARHistory.finYtdDeposits, ARHistoryYtdGrouped.finYtdDepositsSum, Set<ARHistory.finYtdRetainageWithheld, ARHistoryYtdGrouped.finYtdRetainageWithheldSum, Set<ARHistory.finYtdRetainageReleased, ARHistoryYtdGrouped.finYtdRetainageReleasedSum>>>>>, ARHistory, LeftJoin<PX.Objects.GL.Branch, On<ARHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<ARHistory.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>, And<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.masterFinPeriodID>>>>>, InnerJoin<ARHistoryYtdGrouped, On<ARHistory.customerID, Equal<Required<ARHistory.customerID>>, And<ARHistoryYtdGrouped.branchID, Equal<ARHistory.branchID>, And<ARHistoryYtdGrouped.customerID, Equal<ARHistory.customerID>, And<ARHistoryYtdGrouped.accountID, Equal<ARHistory.accountID>, And<ARHistoryYtdGrouped.subID, Equal<ARHistory.subID>, And<ARHistoryYtdGrouped.finPeriodID, Equal<ARHistory.finPeriodID>>>>>>>>>>>.Update((PXGraph) this, new object[2]
    {
      (object) finPeriod,
      (object) cust.BAccountID
    });
    PXUpdateJoin<Set<ARHistory.tranBegBalance, BqlFunctionMirror<Sub<Add<ARHistoryYtdGrouped.tranPtdSalesSum, BqlOperand<ARHistoryYtdGrouped.tranPtdDrAdjustmentsSum, IBqlDecimal>.Add<ARHistoryYtdGrouped.tranPtdFinChargesSum>>, BqlOperand<ARHistoryYtdGrouped.tranPtdPaymentsSum, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.tranPtdCrAdjustmentsSum, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.tranPtdDiscountsSum, IBqlDecimal>.Add<ARHistoryYtdGrouped.tranPtdRGOLSum>>>>, IBqlDecimal>.Subtract<BqlFunction<Add<ARHistoryYtdGrouped.tranPtdSales, BqlOperand<ARHistoryYtdGrouped.tranPtdDrAdjustments, IBqlDecimal>.Add<ARHistoryYtdGrouped.tranPtdFinCharges>>, IBqlDecimal>.Subtract<BqlOperand<ARHistoryYtdGrouped.tranPtdPayments, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.tranPtdCrAdjustments, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.tranPtdDiscounts, IBqlDecimal>.Add<ARHistoryYtdGrouped.tranPtdRGOL>>>>>, Set<ARHistory.tranYtdBalance, BqlFunction<Add<ARHistoryYtdGrouped.tranPtdSalesSum, BqlOperand<ARHistoryYtdGrouped.tranPtdDrAdjustmentsSum, IBqlDecimal>.Add<ARHistoryYtdGrouped.tranPtdFinChargesSum>>, IBqlDecimal>.Subtract<BqlOperand<ARHistoryYtdGrouped.tranPtdPaymentsSum, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.tranPtdCrAdjustmentsSum, IBqlDecimal>.Add<BqlOperand<ARHistoryYtdGrouped.tranPtdDiscountsSum, IBqlDecimal>.Add<ARHistoryYtdGrouped.tranPtdRGOLSum>>>>, Set<ARHistory.tranYtdDeposits, ARHistoryYtdGrouped.tranYtdDepositsSum, Set<ARHistory.tranYtdRetainageWithheld, ARHistoryYtdGrouped.tranYtdRetainageWithheldSum, Set<ARHistory.tranYtdRetainageReleased, ARHistoryYtdGrouped.tranYtdRetainageReleasedSum>>>>>, ARHistory, InnerJoin<ARHistoryYtdGrouped, On<ARHistory.customerID, Equal<Required<ARHistory.customerID>>, And<ARHistory.finPeriodID, GreaterEqual<Required<ARHistory.finPeriodID>>, And<ARHistoryYtdGrouped.branchID, Equal<ARHistory.branchID>, And<ARHistoryYtdGrouped.customerID, Equal<ARHistory.customerID>, And<ARHistoryYtdGrouped.accountID, Equal<ARHistory.accountID>, And<ARHistoryYtdGrouped.subID, Equal<ARHistory.subID>, And<ARHistoryYtdGrouped.finPeriodID, Equal<ARHistory.finPeriodID>>>>>>>>>>.Update((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) finPeriod
    });
  }

  protected virtual void FixYtdCuryARHistory(Customer cust, string finPeriod)
  {
    PXUpdateJoin<Set<CuryARHistory.finBegBalance, BqlFunction<Sub<Add<Add<CuryARHistoryYtdGrouped.finPtdSalesSum, CuryARHistoryYtdGrouped.finPtdDrAdjustmentsSum>, CuryARHistoryYtdGrouped.finPtdFinChargesSum>, BqlOperand<CuryARHistoryYtdGrouped.finPtdPaymentsSum, IBqlDecimal>.Add<BqlFunction<Add<CuryARHistoryYtdGrouped.finPtdCrAdjustmentsSum, CuryARHistoryYtdGrouped.finPtdDiscountsSum>, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.finPtdRGOLSum>>>, IBqlDecimal>.Subtract<BqlFunctionMirror<Add<Add<CuryARHistoryYtdGrouped.finPtdSales, CuryARHistoryYtdGrouped.finPtdDrAdjustments>, CuryARHistoryYtdGrouped.finPtdFinCharges>, IBqlDecimal>.Subtract<BqlOperand<CuryARHistoryYtdGrouped.finPtdPayments, IBqlDecimal>.Add<BqlFunction<Add<CuryARHistoryYtdGrouped.finPtdCrAdjustments, CuryARHistoryYtdGrouped.finPtdDiscounts>, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.finPtdRGOL>>>>, Set<CuryARHistory.finYtdBalance, BqlFunctionMirror<Add<Add<CuryARHistoryYtdGrouped.finPtdSalesSum, CuryARHistoryYtdGrouped.finPtdDrAdjustmentsSum>, CuryARHistoryYtdGrouped.finPtdFinChargesSum>, IBqlDecimal>.Subtract<BqlOperand<CuryARHistoryYtdGrouped.finPtdPaymentsSum, IBqlDecimal>.Add<BqlFunction<Add<CuryARHistoryYtdGrouped.finPtdCrAdjustmentsSum, CuryARHistoryYtdGrouped.finPtdDiscountsSum>, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.finPtdRGOLSum>>>, Set<CuryARHistory.finYtdDeposits, CuryARHistoryYtdGrouped.finYtdDepositsSum, Set<CuryARHistory.finYtdRetainageWithheld, CuryARHistoryYtdGrouped.finYtdRetainageWithheldSum, Set<CuryARHistory.finYtdRetainageReleased, CuryARHistoryYtdGrouped.finYtdRetainageReleasedSum, Set<CuryARHistory.curyFinBegBalance, BqlFunction<Sub<Add<Add<CuryARHistoryYtdGrouped.curyFinPtdSalesSum, CuryARHistoryYtdGrouped.curyFinPtdDrAdjustmentsSum>, CuryARHistoryYtdGrouped.curyFinPtdFinChargesSum>, BqlOperand<CuryARHistoryYtdGrouped.curyFinPtdPaymentsSum, IBqlDecimal>.Add<BqlOperand<CuryARHistoryYtdGrouped.curyFinPtdCrAdjustmentsSum, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.curyFinPtdDiscountsSum>>>, IBqlDecimal>.Subtract<BqlFunctionMirror<Add<Add<CuryARHistoryYtdGrouped.curyFinPtdSales, CuryARHistoryYtdGrouped.curyFinPtdDrAdjustments>, CuryARHistoryYtdGrouped.curyFinPtdFinCharges>, IBqlDecimal>.Subtract<BqlOperand<CuryARHistoryYtdGrouped.curyFinPtdPayments, IBqlDecimal>.Add<BqlOperand<CuryARHistoryYtdGrouped.curyFinPtdCrAdjustments, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.curyFinPtdDiscounts>>>>, Set<CuryARHistory.curyFinYtdBalance, BqlFunctionMirror<Add<Add<CuryARHistoryYtdGrouped.curyFinPtdSalesSum, CuryARHistoryYtdGrouped.curyFinPtdDrAdjustmentsSum>, CuryARHistoryYtdGrouped.curyFinPtdFinChargesSum>, IBqlDecimal>.Subtract<BqlOperand<CuryARHistoryYtdGrouped.curyFinPtdPaymentsSum, IBqlDecimal>.Add<BqlOperand<CuryARHistoryYtdGrouped.curyFinPtdCrAdjustmentsSum, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.curyFinPtdDiscountsSum>>>, Set<CuryARHistory.curyFinYtdDeposits, CuryARHistoryYtdGrouped.curyFinYtdDepositsSum, Set<CuryARHistory.curyFinYtdRetainageWithheld, CuryARHistoryYtdGrouped.curyFinYtdRetainageWithheldSum, Set<CuryARHistory.curyFinYtdRetainageReleased, CuryARHistoryYtdGrouped.curyFinYtdRetainageReleasedSum>>>>>>>>>>, CuryARHistory, LeftJoin<PX.Objects.GL.Branch, On<CuryARHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<CuryARHistory.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>, And<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.masterFinPeriodID>>>>>, InnerJoin<CuryARHistoryYtdGrouped, On<CuryARHistory.customerID, Equal<Required<CuryARHistory.customerID>>, And<CuryARHistoryYtdGrouped.branchID, Equal<CuryARHistory.branchID>, And<CuryARHistoryYtdGrouped.customerID, Equal<CuryARHistory.customerID>, And<CuryARHistoryYtdGrouped.accountID, Equal<CuryARHistory.accountID>, And<CuryARHistoryYtdGrouped.subID, Equal<CuryARHistory.subID>, And<CuryARHistoryYtdGrouped.curyID, Equal<CuryARHistory.curyID>, And<CuryARHistoryYtdGrouped.finPeriodID, Equal<CuryARHistory.finPeriodID>>>>>>>>>>>>.Update((PXGraph) this, new object[2]
    {
      (object) finPeriod,
      (object) cust.BAccountID
    });
    PXUpdateJoin<Set<CuryARHistory.tranBegBalance, BqlFunction<Sub<Add<Add<CuryARHistoryYtdGrouped.tranPtdSalesSum, CuryARHistoryYtdGrouped.tranPtdDrAdjustmentsSum>, CuryARHistoryYtdGrouped.tranPtdFinChargesSum>, BqlOperand<CuryARHistoryYtdGrouped.tranPtdPaymentsSum, IBqlDecimal>.Add<BqlFunction<Add<CuryARHistoryYtdGrouped.tranPtdCrAdjustmentsSum, CuryARHistoryYtdGrouped.tranPtdDiscountsSum>, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.tranPtdRGOLSum>>>, IBqlDecimal>.Subtract<BqlFunctionMirror<Add<Add<CuryARHistoryYtdGrouped.tranPtdSales, CuryARHistoryYtdGrouped.tranPtdDrAdjustments>, CuryARHistoryYtdGrouped.tranPtdFinCharges>, IBqlDecimal>.Subtract<BqlOperand<CuryARHistoryYtdGrouped.tranPtdPayments, IBqlDecimal>.Add<BqlFunction<Add<CuryARHistoryYtdGrouped.tranPtdCrAdjustments, CuryARHistoryYtdGrouped.tranPtdDiscounts>, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.tranPtdRGOL>>>>, Set<CuryARHistory.tranYtdBalance, BqlFunctionMirror<Add<Add<CuryARHistoryYtdGrouped.tranPtdSalesSum, CuryARHistoryYtdGrouped.tranPtdDrAdjustmentsSum>, CuryARHistoryYtdGrouped.tranPtdFinChargesSum>, IBqlDecimal>.Subtract<BqlOperand<CuryARHistoryYtdGrouped.tranPtdPaymentsSum, IBqlDecimal>.Add<BqlFunction<Add<CuryARHistoryYtdGrouped.tranPtdCrAdjustmentsSum, CuryARHistoryYtdGrouped.tranPtdDiscountsSum>, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.tranPtdRGOLSum>>>, Set<CuryARHistory.tranYtdDeposits, CuryARHistoryYtdGrouped.tranYtdDepositsSum, Set<CuryARHistory.tranYtdRetainageWithheld, CuryARHistoryYtdGrouped.tranYtdRetainageWithheldSum, Set<CuryARHistory.tranYtdRetainageReleased, CuryARHistoryYtdGrouped.tranYtdRetainageReleasedSum, Set<CuryARHistory.curyTranBegBalance, BqlFunction<Sub<Add<Add<CuryARHistoryYtdGrouped.curyTranPtdSalesSum, CuryARHistoryYtdGrouped.curyTranPtdDrAdjustmentsSum>, CuryARHistoryYtdGrouped.curyTranPtdFinChargesSum>, BqlOperand<CuryARHistoryYtdGrouped.curyTranPtdPaymentsSum, IBqlDecimal>.Add<BqlOperand<CuryARHistoryYtdGrouped.curyTranPtdCrAdjustmentsSum, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.curyTranPtdDiscountsSum>>>, IBqlDecimal>.Subtract<BqlFunctionMirror<Add<Add<CuryARHistoryYtdGrouped.curyTranPtdSales, CuryARHistoryYtdGrouped.curyTranPtdDrAdjustments>, CuryARHistoryYtdGrouped.curyTranPtdFinCharges>, IBqlDecimal>.Subtract<BqlOperand<CuryARHistoryYtdGrouped.curyTranPtdPayments, IBqlDecimal>.Add<BqlOperand<CuryARHistoryYtdGrouped.curyTranPtdCrAdjustments, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.curyTranPtdDiscounts>>>>, Set<CuryARHistory.curyTranYtdBalance, BqlFunctionMirror<Add<Add<CuryARHistoryYtdGrouped.curyTranPtdSalesSum, CuryARHistoryYtdGrouped.curyTranPtdDrAdjustmentsSum>, CuryARHistoryYtdGrouped.curyTranPtdFinChargesSum>, IBqlDecimal>.Subtract<BqlOperand<CuryARHistoryYtdGrouped.curyTranPtdPaymentsSum, IBqlDecimal>.Add<BqlOperand<CuryARHistoryYtdGrouped.curyTranPtdCrAdjustmentsSum, IBqlDecimal>.Add<CuryARHistoryYtdGrouped.curyTranPtdDiscountsSum>>>, Set<CuryARHistory.curyTranYtdDeposits, CuryARHistoryYtdGrouped.curyTranYtdDepositsSum, Set<CuryARHistory.curyTranYtdRetainageWithheld, CuryARHistoryYtdGrouped.curyTranYtdRetainageWithheldSum, Set<CuryARHistory.curyTranYtdRetainageReleased, CuryARHistoryYtdGrouped.curyTranYtdRetainageReleasedSum>>>>>>>>>>, CuryARHistory, InnerJoin<CuryARHistoryYtdGrouped, On<CuryARHistory.customerID, Equal<Required<CuryARHistory.customerID>>, And<CuryARHistory.finPeriodID, GreaterEqual<Required<CuryARHistory.finPeriodID>>, And<CuryARHistoryYtdGrouped.branchID, Equal<CuryARHistory.branchID>, And<CuryARHistoryYtdGrouped.customerID, Equal<CuryARHistory.customerID>, And<CuryARHistoryYtdGrouped.accountID, Equal<CuryARHistory.accountID>, And<CuryARHistoryYtdGrouped.subID, Equal<CuryARHistory.subID>, And<CuryARHistoryYtdGrouped.curyID, Equal<CuryARHistory.curyID>, And<CuryARHistoryYtdGrouped.finPeriodID, Equal<CuryARHistory.finPeriodID>>>>>>>>>>>.Update((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) finPeriod
    });
  }

  protected virtual void UpdateARBalancesCurrent(Customer cust)
  {
    DateTime date = new PXDBLastChangeDateTimeAttribute(typeof (ARBalances.lastModifiedDateTime)).GetDate();
    PXUpdateJoin<Set<ARBalances.currentBal, IsNull<ARCurrentBalanceSum.currentBalSum, Zero>, Set<ARBalances.lastModifiedByID, Required<ARBalances.lastModifiedByID>, Set<ARBalances.lastModifiedByScreenID, Required<ARBalances.lastModifiedByScreenID>, Set<ARBalances.lastModifiedDateTime, Required<ARBalances.lastModifiedDateTime>>>>>, ARBalances, LeftJoin<ARCurrentBalanceSum, On<ARCurrentBalanceSum.branchID, Equal<ARBalances.branchID>, And<ARCurrentBalanceSum.customerID, Equal<ARBalances.customerID>, And<ARCurrentBalanceSum.customerLocationID, Equal<ARBalances.customerLocationID>>>>>, Where<ARBalances.customerID, Equal<Required<ARBalances.customerID>>>>.Update((PXGraph) this, new object[4]
    {
      (object) this._currentUserInformationProvider.GetUserIdOrDefault(),
      (object) (((PXGraph) this).Accessinfo.ScreenID?.Replace(".", "") ?? "00000000"),
      (object) date,
      (object) cust.BAccountID
    });
  }

  protected virtual void UpdateARBalancesUnreleased(Customer cust)
  {
    DateTime date = new PXDBLastChangeDateTimeAttribute(typeof (ARBalances.lastModifiedDateTime)).GetDate();
    PXUpdateJoin<Set<ARBalances.unreleasedBal, IsNull<ARCurrentBalanceUnreleasedSum.unreleasedBalSum, Zero>, Set<ARBalances.lastModifiedByID, Required<ARBalances.lastModifiedByID>, Set<ARBalances.lastModifiedByScreenID, Required<ARBalances.lastModifiedByScreenID>, Set<ARBalances.lastModifiedDateTime, Required<ARBalances.lastModifiedDateTime>>>>>, ARBalances, LeftJoin<ARCurrentBalanceUnreleasedSum, On<ARCurrentBalanceUnreleasedSum.branchID, Equal<ARBalances.branchID>, And<ARCurrentBalanceUnreleasedSum.customerID, Equal<ARBalances.customerID>, And<ARCurrentBalanceUnreleasedSum.customerLocationID, Equal<ARBalances.customerLocationID>>>>>, Where<ARBalances.customerID, Equal<Required<ARBalances.customerID>>, And<ARBalances.unreleasedBal, NotEqual<IsNull<ARCurrentBalanceUnreleasedSum.unreleasedBalSum, Zero>>>>>.Update((PXGraph) this, new object[4]
    {
      (object) this._currentUserInformationProvider.GetUserIdOrDefault(),
      (object) (((PXGraph) this).Accessinfo.ScreenID?.Replace(".", "") ?? "00000000"),
      (object) date,
      (object) cust.BAccountID
    });
  }

  protected virtual void UpdateARBalancesOpenOrders(Customer cust)
  {
    PXUpdateJoin<Set<ARBalances.totalOpenOrders, IsNull<ARCurrentBalanceOpenOrdersSum.unbilledOrderTotal, Zero>>, ARBalances, LeftJoin<ARCurrentBalanceOpenOrdersSum, On<ARCurrentBalanceOpenOrdersSum.branchID, Equal<ARBalances.branchID>, And<ARCurrentBalanceOpenOrdersSum.customerID, Equal<ARBalances.customerID>, And<ARCurrentBalanceOpenOrdersSum.customerLocationID, Equal<ARBalances.customerLocationID>>>>>, Where<ARBalances.customerID, Equal<Required<ARBalances.customerID>>>>.Update((PXGraph) this, new object[1]
    {
      (object) cust.BAccountID
    });
  }

  protected virtual void FillMissedARHistory(Customer cust, string finPeriod)
  {
    GraphHelper.RowCast<ARTranPost>((IEnumerable) PXSelectBase<ARTranPost, PXSelectJoinGroupBy<ARTranPost, LeftJoin<ARHistory, On<ARHistory.branchID, Equal<ARTranPost.branchID>, And<ARHistory.accountID, Equal<ARTranPost.accountID>, And<ARHistory.subID, Equal<ARTranPost.subID>, And<ARHistory.customerID, Equal<ARTranPost.customerID>, And<ARHistory.finPeriodID, Equal<ARTranPost.finPeriodID>>>>>>>, Where<ARTranPost.customerID, Equal<Required<ARTranPost.customerID>>, And<ARTranPost.finPeriodID, GreaterEqual<Required<ARTranPost.finPeriodID>>, And<ARHistory.branchID, IsNull>>>, Aggregate<GroupBy<ARTranPost.branchID, GroupBy<ARTranPost.accountID, GroupBy<ARTranPost.subID, GroupBy<ARTranPost.customerID, GroupBy<ARTranPost.finPeriodID>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) finPeriod
    })).ForEach<ARTranPost>((Action<ARTranPost, int>) ((e, i) => this.AddARHistory(e, e.FinPeriodID)));
    GraphHelper.RowCast<ARTranPost>((IEnumerable) PXSelectBase<ARTranPost, PXSelectJoinGroupBy<ARTranPost, LeftJoin<ARHistory, On<ARHistory.branchID, Equal<ARTranPost.branchID>, And<ARHistory.accountID, Equal<ARTranPost.accountID>, And<ARHistory.subID, Equal<ARTranPost.subID>, And<ARHistory.customerID, Equal<ARTranPost.customerID>, And<ARHistory.finPeriodID, Equal<ARTranPost.tranPeriodID>>>>>>>, Where<ARTranPost.customerID, Equal<Required<ARTranPost.customerID>>, And<ARTranPost.finPeriodID, GreaterEqual<Required<ARTranPost.finPeriodID>>, And<ARHistory.branchID, IsNull>>>, Aggregate<GroupBy<ARTranPost.branchID, GroupBy<ARTranPost.accountID, GroupBy<ARTranPost.subID, GroupBy<ARTranPost.customerID, GroupBy<ARTranPost.tranPeriodID>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) finPeriod
    })).ForEach<ARTranPost>((Action<ARTranPost, int>) ((e, i) => this.AddARHistory(e, e.TranPeriodID)));
  }

  protected void AddARHistory(ARTranPost tranPost, string finPeriodID)
  {
    if (!tranPost.AccountID.HasValue || !tranPost.SubID.HasValue || finPeriodID == null)
      return;
    PXDatabase.Insert<ARHistory>(new PXDataFieldAssign[43]
    {
      new PXDataFieldAssign(typeof (ARHistory.branchID).Name, (object) tranPost.BranchID),
      new PXDataFieldAssign(typeof (ARHistory.accountID).Name, (object) tranPost.AccountID),
      new PXDataFieldAssign(typeof (ARHistory.subID).Name, (object) tranPost.SubID),
      new PXDataFieldAssign(typeof (ARHistory.customerID).Name, (object) tranPost.CustomerID),
      new PXDataFieldAssign(typeof (ARHistory.finPeriodID).Name, (object) finPeriodID),
      new PXDataFieldAssign(typeof (ARHistory.detDeleted).Name, (object) false),
      new PXDataFieldAssign(typeof (ARHistory.finPtdDrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdCrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdSales).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdPayments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdDiscounts).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finYtdBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finBegBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdCOGS).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdRGOL).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdFinCharges).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finYtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdItemDiscounts).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdRevalued).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdDrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdCrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdSales).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdPayments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdDiscounts).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranYtdBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranBegBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdCOGS).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdRGOL).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdFinCharges).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranYtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdItemDiscounts).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.numberInvoicePaid).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.paidInvoiceDays).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finYtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranYtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finPtdRetainageReleased).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.finYtdRetainageReleased).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranPtdRetainageReleased).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (ARHistory.tranYtdRetainageReleased).Name, (object) 0.0)
    });
  }

  protected virtual void FillMissedCuryARHistory(Customer cust, string finPeriod)
  {
    ((IEnumerable<PXResult<ARTranPost>>) PXSelectBase<ARTranPost, PXSelectJoinGroupBy<ARTranPost, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<ARTranPost.curyInfoID>>, LeftJoin<CuryARHistory, On<CuryARHistory.branchID, Equal<ARTranPost.branchID>, And<CuryARHistory.accountID, Equal<ARTranPost.accountID>, And<CuryARHistory.subID, Equal<ARTranPost.subID>, And<CuryARHistory.customerID, Equal<ARTranPost.customerID>, And<CuryARHistory.finPeriodID, Equal<ARTranPost.finPeriodID>, And<CuryARHistory.curyID, Equal<PX.Objects.CM.CurrencyInfo.curyID>>>>>>>>>, Where<ARTranPost.customerID, Equal<Required<ARTranPost.customerID>>, And<ARTranPost.finPeriodID, GreaterEqual<Required<ARTranPost.finPeriodID>>, And<CuryARHistory.branchID, IsNull>>>, Aggregate<GroupBy<ARTranPost.branchID, GroupBy<ARTranPost.accountID, GroupBy<ARTranPost.subID, GroupBy<ARTranPost.customerID, GroupBy<ARTranPost.finPeriodID, GroupBy<PX.Objects.CM.CurrencyInfo.curyID>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) finPeriod
    })).ForEach<PXResult<ARTranPost>>((Action<PXResult<ARTranPost>, int>) ((e, i) => this.AddCuryARHistory(PXResult<ARTranPost>.op_Implicit(e), PXResult.Unwrap<PX.Objects.CM.CurrencyInfo>((object) e)?.CuryID, PXResult.Unwrap<ARTranPost>((object) e).FinPeriodID)));
    ((IEnumerable<PXResult<ARTranPost>>) PXSelectBase<ARTranPost, PXSelectJoinGroupBy<ARTranPost, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<ARTranPost.curyInfoID>>, LeftJoin<CuryARHistory, On<CuryARHistory.branchID, Equal<ARTranPost.branchID>, And<CuryARHistory.accountID, Equal<ARTranPost.accountID>, And<CuryARHistory.subID, Equal<ARTranPost.subID>, And<CuryARHistory.customerID, Equal<ARTranPost.customerID>, And<CuryARHistory.finPeriodID, Equal<ARTranPost.tranPeriodID>, And<CuryARHistory.curyID, Equal<PX.Objects.CM.CurrencyInfo.curyID>>>>>>>>>, Where<ARTranPost.customerID, Equal<Required<ARTranPost.customerID>>, And<ARTranPost.tranPeriodID, GreaterEqual<Required<ARTranPost.finPeriodID>>, And<CuryARHistory.branchID, IsNull>>>, Aggregate<GroupBy<ARTranPost.branchID, GroupBy<ARTranPost.accountID, GroupBy<ARTranPost.subID, GroupBy<ARTranPost.customerID, GroupBy<ARTranPost.tranPeriodID, GroupBy<PX.Objects.CM.CurrencyInfo.curyID>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) finPeriod
    })).ForEach<PXResult<ARTranPost>>((Action<PXResult<ARTranPost>, int>) ((e, i) => this.AddCuryARHistory(PXResult<ARTranPost>.op_Implicit(e), PXResult.Unwrap<PX.Objects.CM.CurrencyInfo>((object) e)?.CuryID, PXResult.Unwrap<ARTranPost>((object) e).TranPeriodID)));
  }

  protected void AddCuryARHistory(ARTranPost tranPost, string curyID, string finPeriodID)
  {
    if (!tranPost.AccountID.HasValue || !tranPost.SubID.HasValue || finPeriodID == null || curyID == null)
      return;
    PXDatabase.Insert<CuryARHistory>(new PXDataFieldAssign[68]
    {
      new PXDataFieldAssign(typeof (CuryARHistory.branchID).Name, (object) tranPost.BranchID),
      new PXDataFieldAssign(typeof (CuryARHistory.accountID).Name, (object) (tranPost.AccountID ?? -123)),
      new PXDataFieldAssign(typeof (CuryARHistory.subID).Name, (object) (tranPost.SubID ?? -123)),
      new PXDataFieldAssign(typeof (CuryARHistory.customerID).Name, (object) tranPost.CustomerID),
      new PXDataFieldAssign(typeof (CuryARHistory.finPeriodID).Name, (object) finPeriodID),
      new PXDataFieldAssign(typeof (CuryARHistory.curyID).Name, (object) curyID),
      new PXDataFieldAssign(typeof (CuryARHistory.detDeleted).Name, (object) false),
      new PXDataFieldAssign(typeof (CuryARHistory.finBegBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdSales).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdPayments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdDrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdCrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdDiscounts).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdCOGS).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdRGOL).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdFinCharges).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finYtdBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finYtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranBegBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranPtdSales).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranPtdPayments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranPtdDrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranPtdCrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranPtdDiscounts).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranPtdRGOL).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranPtdCOGS).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranPtdFinCharges).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranYtdBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranPtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranYtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinBegBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinPtdSales).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinPtdPayments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinPtdDrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinPtdCrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinPtdDiscounts).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinPtdFinCharges).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinYtdBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinPtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinYtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranBegBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranPtdSales).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranPtdPayments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranPtdDrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranPtdCrAdjustments).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranPtdDiscounts).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranPtdFinCharges).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranYtdBalance).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranPtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranYtdDeposits).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdRevalued).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinPtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranPtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranPtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinYtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finYtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranYtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranYtdRetainageWithheld).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinPtdRetainageReleased).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finPtdRetainageReleased).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranPtdRetainageReleased).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranPtdRetainageReleased).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyFinYtdRetainageReleased).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.finYtdRetainageReleased).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.curyTranYtdRetainageReleased).Name, (object) 0.0),
      new PXDataFieldAssign(typeof (CuryARHistory.tranYtdRetainageReleased).Name, (object) 0.0)
    });
  }
}
