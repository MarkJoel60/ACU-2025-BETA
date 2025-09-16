// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPayBills
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.MigrationMode;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.Utility;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AP;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APPayBills : PXGraph<APPayBills>
{
  public PXFilter<PayBillsFilter> Filter;
  public PXCancel<PayBillsFilter> Cancel;
  public PXAction<PayBillsFilter> ViewDocument;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessingJoinOrderBy<APAdjust, PayBillsFilter, InnerJoin<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APAdjust.adjdDocType>, And<APTran.refNbr, Equal<APAdjust.adjdRefNbr>, And<APTran.lineNbr, Equal<APAdjust.adjdLineNbr>>>>>>>, PX.Data.OrderBy<Asc<APInvoice.docType>>> APDocumentList;
  public PXSelectJoin<APAdjust, InnerJoin<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APAdjust.adjdDocType>, And<APTran.refNbr, Equal<APAdjust.adjdRefNbr>, And<APTran.lineNbr, Equal<APAdjust.adjdLineNbr>>>>>>>> APExceptionsList;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo> currencyinfo;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public PXSelect<APInvoice> Invoice;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<PayBillsFilter.payAccountID>>>> cashaccount;
  public PXSetup<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Current<PayBillsFilter.payAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<PayBillsFilter.payTypeID>>>>> cashaccountdetail;
  public PXSetup<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<PayBillsFilter.payTypeID>>>> paymenttype;
  public APSetupNoMigrationMode APSetup;
  public PXAction<PayBillsFilter> viewInvoice;
  public PXAction<PayBillsFilter> viewOriginalDocument;
  private Dictionary<object, object> _copies = new Dictionary<object, object>();

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public APPayBills()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    this.APDocumentList.SetSelected<APAdjust.selected>();
    this.APDocumentList.SetProcessCaption("Process");
    this.APDocumentList.SetProcessAllCaption("Process All");
    this.APDocumentList.Cache.AllowInsert = true;
    PXUIFieldAttribute.SetEnabled<APAdjust.adjdDocType>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APAdjust.adjdRefNbr>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APAdjust.adjdLineNbr>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APAdjust.curyAdjgAmt>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APAdjust.curyAdjgDiscAmt>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APAdjust.separateCheck>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<PayBillsFilter.curyID>(this.Filter.Cache, (object) null, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>());
    this.APExceptionsList.AllowInsert = false;
    this.APExceptionsList.AllowUpdate = false;
    this.APExceptionsList.AllowDelete = false;
    PXUIFieldAttribute.SetDisplayName<APInvoice.origRefNbr>(this.Caches[typeof (APInvoice)], "Original Document");
    OpenPeriodAttribute.SetValidatePeriod<PayBillsFilter.payFinPeriodID>(this.Filter.Cache, (object) null, this.IsContractBasedAPI || this.IsImport || this.IsExport || this.UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  public override void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    base.InitCacheMapping(map);
    this.Caches.AddCacheMappingsWithInheritance((PXGraph) this, typeof (Vendor));
  }

  public override bool IsDirty => false;

  public override void Clear()
  {
    this.Filter.Current.CurySelTotal = new Decimal?(0M);
    this.Filter.Current.SelTotal = new Decimal?(0M);
    this.Filter.Current.SelCount = new int?(0);
    base.Clear();
  }

  [PXUIField(DisplayName = "View Invoice", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ViewInvoice(PXAdapter adapter)
  {
    if (this.APExceptionsList.Current != null)
    {
      APInvoice row = (APInvoice) PXSelectBase<APInvoice, PXSelectJoin<APInvoice, InnerJoin<APAdjust, On<Current<APAdjust.adjdDocType>, Equal<APInvoice.docType>, And<Current<APAdjust.adjdRefNbr>, Equal<APInvoice.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null);
      PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<APInvoiceEntry>(), (object) row, PXRedirectHelper.WindowMode.NewWindow);
    }
    return adapter.Get();
  }

  [PXButton]
  public virtual IEnumerable ViewOriginalDocument(PXAdapter adapter)
  {
    if (this.APDocumentList.Current != null)
    {
      APInvoice apInvoice = (APInvoice) PXSelectBase<APInvoice, PXSelectJoin<APInvoice, InnerJoin<APAdjust, On<Current<APAdjust.adjdDocType>, Equal<APInvoice.docType>, And<Current<APAdjust.adjdRefNbr>, Equal<APInvoice.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null);
      RedirectionToOrigDoc.TryRedirect(apInvoice.OrigDocType, apInvoice.OrigRefNbr, apInvoice.OrigModule);
    }
    return adapter.Get();
  }

  protected virtual void PayBillsFilter_Days_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    e.ReturnValue = (object) PXLocalizer.Localize(e.ReturnValue as string);
  }

  protected virtual void PayBillsFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row != null && this.cashaccount.Current != null && !object.Equals((object) this.cashaccount.Current.CashAccountID, (object) ((PayBillsFilter) e.Row).PayAccountID))
      this.cashaccount.Current = (PX.Objects.CA.CashAccount) null;
    if (e.Row != null && this.cashaccountdetail.Current != null && (!object.Equals((object) this.cashaccountdetail.Current.CashAccountID, (object) ((PayBillsFilter) e.Row).PayAccountID) || !object.Equals((object) this.cashaccountdetail.Current.PaymentMethodID, (object) ((PayBillsFilter) e.Row).PayTypeID)))
      this.cashaccountdetail.Current = (PaymentMethodAccount) null;
    if (e.Row != null && this.paymenttype.Current != null && !object.Equals((object) this.paymenttype.Current.PaymentMethodID, (object) ((PayBillsFilter) e.Row).PayTypeID))
      this.paymenttype.Current = (PX.Objects.CA.PaymentMethod) null;
    PayBillsFilter filter = e.Row as PayBillsFilter;
    if (filter != null)
    {
      PX.Objects.CM.Extensions.CurrencyInfo info = (PX.Objects.CM.Extensions.CurrencyInfo) this.CurrencyInfo_CuryInfoID.Select((object) filter.CuryInfoID);
      PX.Objects.CA.PaymentMethod paytype = this.paymenttype.Current;
      this.APDocumentList.SetProcessDelegate((PXProcessingBase<APAdjust>.ProcessListDelegate) (list => PXGraph.CreateInstance<APPayBills>().CreatePayments(list, filter, info, paytype)));
    }
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
    int num2;
    if (num1 != 0)
    {
      PaymentMethodAccount current2 = this.cashaccountdetail.Current;
      if (current2 == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = current2.APQuickBatchGeneration;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num2 = 0;
    bool flag1 = num2 != 0;
    PXUIFieldAttribute.SetVisible<PayBillsFilter.aPQuickBatchGeneration>(sender, e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<PayBillsFilter.aPQuickBatchGeneration>(sender, e.Row, flag1);
    PXUIFieldAttribute.SetVisible<APAdjust.vendorID>(this.APDocumentList.Cache, (object) null, true);
    PXCache cache1 = sender;
    PayBillsFilter data1 = filter;
    PayBillsFilter payBillsFilter1 = filter;
    int num3;
    if (payBillsFilter1 == null)
    {
      num3 = 0;
    }
    else
    {
      nullable = payBillsFilter1.ShowPayInLessThan;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<PayBillsFilter.payInLessThan>(cache1, (object) data1, num3 != 0);
    PXCache cache2 = sender;
    PayBillsFilter data2 = filter;
    PayBillsFilter payBillsFilter2 = filter;
    int num4;
    if (payBillsFilter2 == null)
    {
      num4 = 0;
    }
    else
    {
      nullable = payBillsFilter2.ShowDueInLessThan;
      num4 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<PayBillsFilter.dueInLessThan>(cache2, (object) data2, num4 != 0);
    PXCache cache3 = sender;
    PayBillsFilter data3 = filter;
    PayBillsFilter payBillsFilter3 = filter;
    int num5;
    if (payBillsFilter3 == null)
    {
      num5 = 0;
    }
    else
    {
      nullable = payBillsFilter3.ShowDiscountExpiresInLessThan;
      num5 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<PayBillsFilter.discountExpiresInLessThan>(cache3, (object) data3, num5 != 0);
    PXException pxException = (PXException) null;
    if (flag1)
    {
      nullable = filter.APQuickBatchGeneration;
      if (nullable.GetValueOrDefault())
      {
        PaymentMethodAccount current3 = this.cashaccountdetail.Current;
        bool? suggestNextNbr;
        if (current3 == null)
        {
          nullable = new bool?();
          suggestNextNbr = nullable;
        }
        else
          suggestNextNbr = current3.APAutoNextNbr;
        string apLastRefNbr = this.cashaccountdetail.Current?.APLastRefNbr;
        string cashAccountCd = this.cashaccount.Current?.CashAccountCD;
        ref PXException local = ref pxException;
        PaymentMethodAccountHelper.TryToVerifyAPLastReferenceNumber<PayBillsFilter.aPQuickBatchGeneration>(suggestNextNbr, apLastRefNbr, cashAccountCd, out local);
      }
    }
    sender.RaiseExceptionHandling<PayBillsFilter.aPQuickBatchGeneration>(e.Row, (object) filter.APQuickBatchGeneration, (Exception) pxException);
    bool flag2 = PXUIFieldAttribute.GetErrors(sender, (object) null, PXErrorLevel.Error, PXErrorLevel.RowError).Count > 0;
    this.APDocumentList.SetProcessEnabled(!flag2);
    this.APDocumentList.SetProcessAllEnabled(!flag2);
  }

  protected virtual void APAdjust_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    APAdjust row = (APAdjust) e.Row;
    if (string.IsNullOrEmpty(row.AdjdRefNbr))
    {
      e.Cancel = true;
    }
    else
    {
      int? nullable1 = row.VendorID;
      bool flag = !nullable1.HasValue;
      using (IEnumerator<PXResult<APInvoice>> enumerator = PXSelectBase<APInvoice, PXSelectJoin<APInvoice, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<Required<APTran.lineNbr>>>>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>>>, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, (object) row.AdjdLineNbr, (object) row.AdjdDocType, (object) row.AdjdRefNbr).GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return;
        PXResult<APInvoice, APTran, PX.Objects.CM.Extensions.CurrencyInfo> current = (PXResult<APInvoice, APTran, PX.Objects.CM.Extensions.CurrencyInfo>) enumerator.Current;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) current;
        APInvoice apInvoice = (APInvoice) current;
        APTran tran = (APTran) current;
        if (!apInvoice.PaymentsByLinesAllowed.GetValueOrDefault())
        {
          row.AdjdLineNbr = new int?(0);
        }
        else
        {
          nullable1 = row.AdjdLineNbr;
          int num = 0;
          if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
          {
            APAdjust apAdjust = row;
            nullable1 = new int?();
            int? nullable2 = nullable1;
            apAdjust.AdjdLineNbr = nullable2;
          }
        }
        nullable1 = row.AdjdLineNbr;
        if (!nullable1.HasValue)
        {
          e.Cancel = true;
        }
        else
        {
          this.CurrencyInfo_CuryInfoID.Cache.SetStatus((object) currencyInfo, PXEntryStatus.Notchanged);
          PX.Objects.CM.Extensions.CurrencyInfo info;
          try
          {
            info = this.GetExtension<APPayBills.MultiCurrency>().CloneCurrencyInfo(currencyInfo, this.Filter.Current.PayDate);
          }
          catch (PXRateIsNotDefinedForThisDateException ex)
          {
            info = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo);
            info.CuryInfoID = new long?();
            this.APDocumentList.Cache.RaiseExceptionHandling<APAdjust.curyAdjgAmt>((object) row, (object) 0M, (Exception) new PXSetPropertyException(ex.Message, PXErrorLevel.RowError));
          }
          row.VendorID = apInvoice.VendorID;
          row.AdjgDocDate = this.Filter.Current.PayDate;
          row.AdjgFinPeriodID = this.Filter.Current.PayFinPeriodID;
          row.AdjgCuryInfoID = this.Filter.Current.CuryInfoID;
          row.AdjdCuryInfoID = info.CuryInfoID;
          row.AdjdOrigCuryInfoID = currencyInfo.CuryInfoID;
          row.AdjdBranchID = apInvoice.BranchID;
          row.AdjdAPAcct = apInvoice.APAccountID;
          row.AdjdAPSub = apInvoice.APSubID;
          row.AdjdDocDate = apInvoice.DocDate;
          row.AdjdFinPeriodID = apInvoice.FinPeriodID;
          row.Released = new bool?(false);
          row.SeparateCheck = row.SeparateCheck ?? apInvoice.SeparateCheck;
          this.CurrencyInfo_CuryInfoID.View.Clear();
          this.GetExtension<APPayBills.MultiCurrency>().StoreResult(currencyInfo);
          this.GetExtension<APPayBills.MultiCurrency>().StoreResult(info);
          this.CalcBalances(row, apInvoice, tran, false);
          if (flag)
          {
            row.Selected = new bool?(true);
            apInvoice.ManualEntry = new bool?(true);
            this.Invoice.Cache.SetStatus((object) apInvoice, PXEntryStatus.Inserted);
          }
          if (!this.SetSuggestedAmounts(row, tran, apInvoice))
            return;
          this.CalcBalances(row, apInvoice, tran, true);
        }
      }
    }
  }

  protected virtual bool SetSuggestedAmounts(APAdjust adj, APTran tran, APInvoice invoice)
  {
    Decimal? curyDocBal1 = adj.CuryDocBal;
    Decimal num1 = 0M;
    Sign sign1 = curyDocBal1.GetValueOrDefault() < num1 & curyDocBal1.HasValue ? Sign.Minus : Sign.Plus;
    Decimal? curyWhTaxBal = adj.CuryWhTaxBal;
    Decimal num2 = 0M;
    Decimal? nullable1;
    Decimal? nullable2;
    Decimal? nullable3;
    if (curyWhTaxBal.GetValueOrDefault() >= num2 & curyWhTaxBal.HasValue)
    {
      Decimal? curyDiscBal = adj.CuryDiscBal;
      Decimal num3 = 0M;
      if (curyDiscBal.GetValueOrDefault() >= num3 & curyDiscBal.HasValue)
      {
        Decimal? curyDocBal2 = adj.CuryDocBal;
        nullable1 = adj.CuryWhTaxBal;
        nullable2 = curyDocBal2.HasValue & nullable1.HasValue ? new Decimal?(curyDocBal2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
        nullable3 = adj.CuryDiscBal;
        Decimal? nullable4;
        if (!(nullable2.HasValue & nullable3.HasValue))
        {
          nullable1 = new Decimal?();
          nullable4 = nullable1;
        }
        else
          nullable4 = new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
        Decimal? nullable5 = nullable4;
        Sign sign2 = sign1;
        Decimal? nullable6;
        if (!nullable5.HasValue)
        {
          nullable3 = new Decimal?();
          nullable6 = nullable3;
        }
        else
          nullable6 = new Decimal?(Sign.op_Multiply(nullable5.GetValueOrDefault(), sign2));
        Decimal? nullable7 = nullable6;
        Decimal num4 = 0M;
        if (nullable7.GetValueOrDefault() <= num4 & nullable7.HasValue)
          return false;
      }
    }
    Decimal? curyAdjgAmt = adj.CuryAdjgAmt;
    Decimal num5 = 0M;
    Decimal? nullable8;
    if (curyAdjgAmt.GetValueOrDefault() == num5 & curyAdjgAmt.HasValue)
    {
      APAdjust apAdjust = adj;
      nullable3 = adj.CuryDocBal;
      nullable2 = adj.CuryWhTaxBal;
      Decimal? nullable9;
      if (!(nullable3.HasValue & nullable2.HasValue))
      {
        nullable1 = new Decimal?();
        nullable9 = nullable1;
      }
      else
        nullable9 = new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault());
      Decimal? nullable10 = nullable9;
      nullable8 = adj.CuryDiscBal;
      Decimal? nullable11;
      if (!(nullable10.HasValue & nullable8.HasValue))
      {
        nullable2 = new Decimal?();
        nullable11 = nullable2;
      }
      else
        nullable11 = new Decimal?(nullable10.GetValueOrDefault() - nullable8.GetValueOrDefault());
      apAdjust.CuryAdjgAmt = nullable11;
    }
    nullable8 = adj.CuryAdjgDiscAmt;
    Decimal num6 = 0M;
    if (nullable8.GetValueOrDefault() == num6 & nullable8.HasValue)
      adj.CuryAdjgDiscAmt = adj.CuryDiscBal;
    adj.CuryAdjgWhTaxAmt = adj.CuryWhTaxBal;
    return true;
  }

  private bool CheckIfRowNotApprovedForPayment(APAdjust row)
  {
    return this.APSetup.Current.RequireApprovePayments.GetValueOrDefault() && this.CheckIfRowNotApprovedForPayment((APInvoice) PXSelectorAttribute.Select<APAdjust.adjdRefNbr>(this.APDocumentList.Cache, (object) row));
  }

  private bool CheckIfRowNotApprovedForPayment(APInvoice invoice)
  {
    return invoice != null && this.APSetup.Current.RequireApprovePayments.GetValueOrDefault() && !(invoice.DocType == "ADR") && !invoice.PaySel.GetValueOrDefault();
  }

  protected virtual void APAdjust_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || PXLongOperation.Exists(this.UID))
      return;
    PXUIFieldAttribute.SetEnabled<APAdjust.adjdDocType>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<APAdjust.adjdRefNbr>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<APAdjust.adjdLineNbr>(sender, e.Row, false);
    APInvoice invoice = (APInvoice) PXSelectorAttribute.Select<APAdjust.adjdRefNbr>(this.APDocumentList.Cache, (object) (APAdjust) e.Row);
    if (this.CheckIfRowNotApprovedForPayment(invoice))
      this.APDocumentList.Cache.RaiseExceptionHandling<APAdjust.curyAdjgAmt>((object) (APAdjust) e.Row, (object) 0M, (Exception) new PXSetPropertyException("Document is not approved for payment and will not be processed.", PXErrorLevel.RowWarning));
    PXException pxException = (PXException) null;
    bool? nullable = this.APSetup.Current.EarlyChecks;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
    {
      System.DateTime? adjdDocDate = ((APAdjust) e.Row).AdjdDocDate;
      System.DateTime? payDate = this.Filter.Current.PayDate;
      if ((adjdDocDate.HasValue & payDate.HasValue ? (adjdDocDate.GetValueOrDefault() > payDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        pxException = (PXException) new PXSetPropertyException("{0} cannot be less than Document Date.", PXErrorLevel.RowWarning, new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<PayBillsFilter.payDate>(this.Filter.Cache)
        });
    }
    nullable = this.APSetup.Current.EarlyChecks;
    bool flag2 = false;
    if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue && !string.IsNullOrEmpty(this.Filter.Current.PayFinPeriodID) && string.Compare(((APAdjust) e.Row).AdjdFinPeriodID, this.Filter.Current.PayFinPeriodID) > 0)
      pxException = (PXException) new PXSetPropertyException("{0} cannot be less than Document Financial Period.", PXErrorLevel.RowWarning, new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PayBillsFilter.payFinPeriodID>(this.Filter.Cache)
      });
    if (this.Filter.Current.ProjectID.HasValue && invoice != null)
    {
      nullable = invoice.PaymentsByLinesAllowed;
      if (!nullable.GetValueOrDefault())
      {
        int? projectId1 = this.Filter.Current.ProjectID;
        int? projectId2 = invoice.ProjectID;
        if (!(projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue))
        {
          pxException = (PXException) new PXSetPropertyException("The document has lines with projects different from the one selected in the Summary area. If you select this document, all its lines will be paid.", PXErrorLevel.RowWarning);
          pxException.Data.Add((object) "SetEnabled", (object) true);
        }
      }
    }
    sender.RaiseExceptionHandling<APAdjust.selected>(e.Row, (object) false, (Exception) pxException);
    PXCache cache = sender;
    object row = e.Row;
    if (pxException != null)
    {
      nullable = pxException.Data[(object) "SetEnabled"] as bool?;
      if (!nullable.GetValueOrDefault())
        goto label_16;
    }
    int num;
    if (((APAdjust) e.Row).CuryDocBal.HasValue)
    {
      num = !string.IsNullOrEmpty(this.Filter.Current.PayFinPeriodID) ? 1 : 0;
      goto label_17;
    }
label_16:
    num = 0;
label_17:
    PXUIFieldAttribute.SetEnabled<APAdjust.selected>(cache, row, num != 0);
  }

  protected virtual IEnumerable aPExceptionsList()
  {
    PayBillsFilter current = this.Filter.Current;
    PXResultMapper mapper = new PXResultMapper((PXGraph) this, new Dictionary<System.Type, System.Type>()
    {
      {
        typeof (APAdjust.adjdDocType),
        typeof (APInvoice.docType)
      },
      {
        typeof (APAdjust.adjdRefNbr),
        typeof (APInvoice.refNbr)
      },
      {
        typeof (APAdjust.adjdLineNbr),
        typeof (APTran.lineNbr)
      }
    }, new System.Type[3]
    {
      typeof (APAdjust),
      typeof (APInvoice),
      typeof (APTran)
    });
    EnumerableExtensions.AddRange<System.Type>((ISet<System.Type>) mapper.ExtFilters, (IEnumerable<System.Type>) new System.Type[5]
    {
      typeof (APAdjust.separateCheck),
      typeof (APAdjust.curyAdjgAmt),
      typeof (APAdjust.curyDocBal),
      typeof (APAdjust.curyAdjgDiscAmt),
      typeof (APAdjust.curyDiscBal)
    });
    EnumerableExtensions.AddRange<System.Type>((ISet<System.Type>) mapper.SuppressSorts, (IEnumerable<System.Type>) new System.Type[3]
    {
      typeof (APAdjust.adjgDocType),
      typeof (APAdjust.adjgRefNbr),
      typeof (APAdjust.adjNbr)
    });
    bool flag = ((IEnumerable<string>) mapper.SortColumns).Any<string>((Func<string, bool>) (field => mapper.ExtFilters.Contains(this.APDocumentList.Cache.GetBqlField(field))));
    PXDelegateResult delegateResult = mapper.CreateDelegateResult(!flag);
    if ((current != null ? (!current.PayDate.HasValue ? 1 : 0) : 1) != 0 || current.PayTypeID == null || !current.PayAccountID.HasValue)
      return (IEnumerable) delegateResult;
    System.DateTime dateTime1 = current.PayDate.Value;
    ref System.DateTime local1 = ref dateTime1;
    short? nullable1 = current.PayInLessThan;
    double valueOrDefault1 = (double) nullable1.GetValueOrDefault();
    System.DateTime dateTime2 = local1.AddDays(valueOrDefault1);
    System.DateTime dateTime3 = current.PayDate.Value;
    ref System.DateTime local2 = ref dateTime3;
    nullable1 = current.DueInLessThan;
    double valueOrDefault2 = (double) nullable1.GetValueOrDefault();
    System.DateTime dateTime4 = local2.AddDays(valueOrDefault2);
    System.DateTime dateTime5 = current.PayDate.Value;
    ref System.DateTime local3 = ref dateTime5;
    nullable1 = current.DiscountExpiresInLessThan;
    double valueOrDefault3 = (double) nullable1.GetValueOrDefault();
    System.DateTime dateTime6 = local3.AddDays(valueOrDefault3);
    int startRow = delegateResult.IsResultTruncated ? PXView.StartRow : 0;
    int maximumRows1 = delegateResult.IsResultTruncated ? PXView.MaximumRows : 0;
    int num1 = 0;
    PXSelectJoin<APInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, InnerJoin<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>, PX.Data.And<Where<Vendor.vStatus, Equal<VendorStatus.active>, Or<Vendor.vStatus, Equal<VendorStatus.oneTime>>>>>, InnerJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>, And<APAdjust.released, Equal<False>>>>, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<APAdjust.adjdLineNbr>>>>>, InnerJoin<APPayment, On<APPayment.docType, Equal<APAdjust.adjgDocType>, And<APPayment.refNbr, Equal<APAdjust.adjgRefNbr>>>, LeftJoinSingleTable<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<APInvoice.vendorID>, And<PX.Objects.CR.Location.locationID, Equal<APInvoice.vendorLocationID>>>>>>>>>, Where<APInvoice.openDoc, Equal<True>, And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>, And2<Where<APInvoice.paySel, Equal<True>, Or2<Where<Current<PX.Objects.AP.APSetup.requireApprovePayments>, Equal<False>>, Or<APInvoice.docType, Equal<APDocType.debitAdj>>>>, And2<Where<Vendor.vendorClassID, Equal<Current<PayBillsFilter.vendorClassID>>, Or<Current<PayBillsFilter.vendorClassID>, PX.Data.IsNull>>, And2<Where<APInvoice.vendorID, Equal<Current<PayBillsFilter.vendorID>>, Or<Current<PayBillsFilter.vendorID>, PX.Data.IsNull>>, And<APInvoice.payAccountID, Equal<Current<PayBillsFilter.payAccountID>>, And<APInvoice.payTypeID, Equal<Current<PayBillsFilter.payTypeID>>, And2<Where<APPayment.status, Equal<APDocStatus.hold>, Or2<Where<APPayment.status, Equal<APDocStatus.pendingApproval>>, Or<APPayment.status, Equal<APDocStatus.rejected>>>>, And2<Where2<Where2<Where<Current<PayBillsFilter.showPayInLessThan>, Equal<True>, And<APInvoice.payDate, LessEqual<Required<APInvoice.payDate>>>>, Or2<Where<Current<PayBillsFilter.showDueInLessThan>, Equal<True>, PX.Data.And<Where<APInvoice.dueDate, LessEqual<Required<APInvoice.dueDate>>, Or<APInvoice.dueDate, PX.Data.IsNull>>>>, PX.Data.Or<Where<Current<PayBillsFilter.showDiscountExpiresInLessThan>, Equal<True>, PX.Data.And<Where<APInvoice.discDate, LessEqual<Required<APInvoice.discDate>>, Or<APInvoice.discDate, PX.Data.IsNull>>>>>>>, PX.Data.Or<Where<Current<PayBillsFilter.showPayInLessThan>, Equal<False>, And<Current<PayBillsFilter.showDueInLessThan>, Equal<False>, And<Current<PayBillsFilter.showDiscountExpiresInLessThan>, Equal<False>, Or<APInvoice.docType, Equal<APDocType.debitAdj>>>>>>>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>>>>>>>>>> pxSelectJoin = new PXSelectJoin<APInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, InnerJoin<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>, PX.Data.And<Where<Vendor.vStatus, Equal<VendorStatus.active>, Or<Vendor.vStatus, Equal<VendorStatus.oneTime>>>>>, InnerJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>, And<APAdjust.released, Equal<False>>>>, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<APAdjust.adjdLineNbr>>>>>, InnerJoin<APPayment, On<APPayment.docType, Equal<APAdjust.adjgDocType>, And<APPayment.refNbr, Equal<APAdjust.adjgRefNbr>>>, LeftJoinSingleTable<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<APInvoice.vendorID>, And<PX.Objects.CR.Location.locationID, Equal<APInvoice.vendorLocationID>>>>>>>>>, Where<APInvoice.openDoc, Equal<True>, And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>, And2<Where<APInvoice.paySel, Equal<True>, Or2<Where<Current<PX.Objects.AP.APSetup.requireApprovePayments>, Equal<False>>, Or<APInvoice.docType, Equal<APDocType.debitAdj>>>>, And2<Where<Vendor.vendorClassID, Equal<Current<PayBillsFilter.vendorClassID>>, Or<Current<PayBillsFilter.vendorClassID>, PX.Data.IsNull>>, And2<Where<APInvoice.vendorID, Equal<Current<PayBillsFilter.vendorID>>, Or<Current<PayBillsFilter.vendorID>, PX.Data.IsNull>>, And<APInvoice.payAccountID, Equal<Current<PayBillsFilter.payAccountID>>, And<APInvoice.payTypeID, Equal<Current<PayBillsFilter.payTypeID>>, And2<Where<APPayment.status, Equal<APDocStatus.hold>, Or2<Where<APPayment.status, Equal<APDocStatus.pendingApproval>>, Or<APPayment.status, Equal<APDocStatus.rejected>>>>, And2<Where2<Where2<Where<Current<PayBillsFilter.showPayInLessThan>, Equal<True>, And<APInvoice.payDate, LessEqual<Required<APInvoice.payDate>>>>, Or2<Where<Current<PayBillsFilter.showDueInLessThan>, Equal<True>, PX.Data.And<Where<APInvoice.dueDate, LessEqual<Required<APInvoice.dueDate>>, Or<APInvoice.dueDate, PX.Data.IsNull>>>>, PX.Data.Or<Where<Current<PayBillsFilter.showDiscountExpiresInLessThan>, Equal<True>, PX.Data.And<Where<APInvoice.discDate, LessEqual<Required<APInvoice.discDate>>, Or<APInvoice.discDate, PX.Data.IsNull>>>>>>>, PX.Data.Or<Where<Current<PayBillsFilter.showPayInLessThan>, Equal<False>, And<Current<PayBillsFilter.showDueInLessThan>, Equal<False>, And<Current<PayBillsFilter.showDiscountExpiresInLessThan>, Equal<False>, Or<APInvoice.docType, Equal<APDocType.debitAdj>>>>>>>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>>>>>>>>>>((PXGraph) this);
    object[] searches1;
    string[] sorts;
    APPayBills.ObtainSearchesAndSorts(mapper, out searches1, out sorts);
    PXView view = pxSelectJoin.View;
    object[] currents = (object[]) new PayBillsFilter[1]
    {
      current
    };
    object[] parameters = new object[3]
    {
      (object) dateTime2,
      (object) dateTime4,
      (object) dateTime6
    };
    object[] searches2 = searches1;
    string[] sortcolumns = sorts;
    bool[] descendings = mapper.Descendings;
    PXFilterRow[] filters = (PXFilterRow[]) mapper.Filters;
    ref int local4 = ref startRow;
    int maximumRows2 = maximumRows1;
    ref int local5 = ref num1;
    foreach (PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Vendor, APAdjust, APTran> row in view.Select(currents, parameters, searches2, sortcolumns, descendings, filters, ref local4, maximumRows2, ref local5))
    {
      APInvoice apInvoice = (APInvoice) row;
      APTran apTran = (APTran) row;
      APAdjust data1 = new APAdjust();
      PX.Objects.CM.Extensions.CurrencyInfo i2 = (PX.Objects.CM.Extensions.CurrencyInfo) row;
      data1.VendorID = apInvoice.VendorID;
      data1.AdjdDocType = apInvoice.DocType;
      data1.AdjdRefNbr = apInvoice.RefNbr;
      APAdjust apAdjust1 = data1;
      int? nullable2;
      int? nullable3;
      if (apTran == null)
      {
        nullable2 = new int?();
        nullable3 = nullable2;
      }
      else
        nullable3 = apTran.LineNbr;
      nullable2 = nullable3;
      int? nullable4 = new int?(nullable2.GetValueOrDefault());
      apAdjust1.AdjdLineNbr = nullable4;
      data1.AdjgDocType = "CHK";
      APAdjust apAdjust2 = data1;
      PXCache cach = this.Caches[typeof (APPayment)];
      APPayment data2 = new APPayment();
      data2.DocType = "CHK";
      string newNumberSymbol = AutoNumberAttribute.GetNewNumberSymbol<APPayment.refNbr>(cach, (object) data2);
      apAdjust2.AdjgRefNbr = newNumberSymbol;
      data1.SeparateCheck = apInvoice.SeparateCheck;
      APAdjust i0 = this.APExceptionsList.Locate(data1);
      if (i0 == null)
      {
        PXSelectBase<APInvoice, PXSelectJoin<APInvoice, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<Required<APTran.lineNbr>>>>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>>>, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.StoreResult((PXGraph) this, new List<object>()
        {
          (object) new PXResult<APInvoice, APTran, PX.Objects.CM.Extensions.CurrencyInfo>(apInvoice, apTran, i2)
        }, PXQueryParameters.ExplicitParameters((object) data1.AdjdLineNbr, (object) data1.AdjdDocType, (object) data1.AdjdRefNbr));
        PXSelectorAttribute.StoreResult<APAdjust.adjdRefNbr>(this.APExceptionsList.Cache, (object) data1, (IBqlTable) apInvoice);
        PXSelectorAttribute.StoreResult<APInvoice.vendorLocationID>(this.Invoice.Cache, (object) apInvoice, (IBqlTable) PXResult.Unwrap<PX.Objects.CR.Location>((object) row));
        nullable2 = data1.AdjdLineNbr;
        int num2 = 0;
        if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
          PXSelectorAttribute.StoreResult<APAdjust.adjdLineNbr>(this.APExceptionsList.Cache, (object) data1, (IBqlTable) apTran);
        delegateResult.Add((object) new PXResult<APAdjust, APInvoice, APTran>(this.APExceptionsList.Insert(data1), apInvoice, apTran));
      }
      else
        delegateResult.Add((object) new PXResult<APAdjust, APInvoice, APTran>(i0, apInvoice, apTran));
    }
    if (delegateResult.IsResultTruncated)
      PXView.StartRow = 0;
    this.APExceptionsList.Cache.IsDirty = false;
    return (IEnumerable) delegateResult;
  }

  private static void ObtainSearchesAndSorts(
    PXResultMapper mapper,
    out object[] searches,
    out string[] sorts)
  {
    searches = mapper.Searches;
    sorts = mapper.SortColumns;
    int index = ((IEnumerable<string>) sorts).FindIndex<string>((Predicate<string>) (field => field == $"{typeof (APTran).Name}__{typeof (APTran.lineNbr).Name}"));
    if (index <= 0 || !(searches[index]?.ToString() == "0"))
      return;
    searches[index] = (object) null;
  }

  protected virtual IEnumerable apdocumentlist()
  {
    PayBillsFilter current = this.Filter.Current;
    BqlCommand command = this.ComposeBQLCommandForAPDocumentListSelect();
    PXResultMapper mapper = new PXResultMapper((PXGraph) this, new Dictionary<System.Type, System.Type>()
    {
      {
        typeof (APAdjust.adjdDocType),
        typeof (APInvoice.docType)
      },
      {
        typeof (APAdjust.adjdRefNbr),
        typeof (APInvoice.refNbr)
      },
      {
        typeof (APAdjust.adjdLineNbr),
        typeof (APTran.lineNbr)
      }
    }, new System.Type[3]
    {
      typeof (APAdjust),
      typeof (APInvoice),
      typeof (APTran)
    });
    EnumerableExtensions.AddRange<System.Type>((ISet<System.Type>) mapper.ExtFilters, (IEnumerable<System.Type>) new System.Type[6]
    {
      typeof (APAdjust.selected),
      typeof (APAdjust.separateCheck),
      typeof (APAdjust.curyAdjgAmt),
      typeof (APAdjust.curyDocBal),
      typeof (APAdjust.curyAdjgDiscAmt),
      typeof (APAdjust.curyDiscBal)
    });
    EnumerableExtensions.AddRange<System.Type>((ISet<System.Type>) mapper.SuppressSorts, (IEnumerable<System.Type>) new System.Type[3]
    {
      typeof (APAdjust.adjgDocType),
      typeof (APAdjust.adjgRefNbr),
      typeof (APAdjust.adjNbr)
    });
    bool flag = ((IEnumerable<string>) mapper.SortColumns).Any<string>((Func<string, bool>) (field => mapper.ExtFilters.Contains(this.APDocumentList.Cache.GetBqlField(field))));
    PXDelegateResult delegateResult = mapper.CreateDelegateResult(!flag);
    int startRow = delegateResult.IsResultTruncated ? PXView.StartRow : 0;
    int maximumRows1 = delegateResult.IsResultTruncated ? PXView.MaximumRows : 0;
    int num1 = 0;
    object[] searches1;
    string[] sorts;
    APPayBills.ObtainSearchesAndSorts(mapper, out searches1, out sorts);
    PXView view = command.CreateView((PXGraph) this, mergeCache: true);
    object[] currents = (object[]) new PayBillsFilter[1]
    {
      current
    };
    object[] parameters = this.ComposeParametersForAPDocumentListSelect();
    object[] searches2 = searches1;
    string[] sortcolumns = sorts;
    bool[] descendings = mapper.Descendings;
    PXFilterRow[] filters = this.SelectDocumentListFilters((PXFilterRow[]) mapper.Filters);
    ref int local1 = ref startRow;
    int maximumRows2 = maximumRows1;
    ref int local2 = ref num1;
    foreach (PXResult<APInvoice, APTran, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in view.Select(currents, parameters, searches2, sortcolumns, descendings, filters, ref local1, maximumRows2, ref local2))
    {
      APInvoice apInvoice = (APInvoice) pxResult;
      APTran apTran = (APTran) pxResult;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult;
      APAdjust apAdjust1 = this.InitRecord((PXResult) pxResult);
      if (this.APDocumentList.Locate(apAdjust1) == null)
      {
        this.StoreResultset((PXResult) pxResult, apAdjust1.AdjdDocType, apAdjust1.AdjdRefNbr, apAdjust1.AdjdLineNbr);
        PXSelectorAttribute.StoreResult<APAdjust.adjdRefNbr>(this.APDocumentList.Cache, (object) apAdjust1, (IBqlTable) apInvoice);
        using (new PXReadBranchRestrictedScope())
          PXSelectorAttribute.StoreResult<APInvoice.vendorLocationID>(this.Invoice.Cache, (object) apInvoice, (IBqlTable) Utilities.Clone<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Location>((PXGraph) this, PXResult.Unwrap<PX.Objects.CR.Standalone.Location>((object) pxResult)));
        int? adjdLineNbr = apAdjust1.AdjdLineNbr;
        int num2 = 0;
        if (!(adjdLineNbr.GetValueOrDefault() == num2 & adjdLineNbr.HasValue))
          PXSelectorAttribute.StoreResult<APAdjust.adjdLineNbr>(this.APDocumentList.Cache, (object) apAdjust1, (IBqlTable) apTran);
        delegateResult.Add((object) new PXResult<APAdjust, APInvoice, APTran>(this.APDocumentList.Insert(apAdjust1), apInvoice, apTran));
        this._copies.Add((object) apAdjust1, (object) PXCache<APAdjust>.CreateCopy(apAdjust1));
      }
      else
      {
        APAdjust apAdjust2 = this.APDocumentList.Locate(apAdjust1);
        apAdjust2.SeparateCheck = apAdjust2.SeparateCheck ?? apInvoice.SeparateCheck;
        delegateResult.Add((object) new PXResult<APAdjust, APInvoice, APTran>(apAdjust2, apInvoice, apTran));
        if (this._copies.ContainsKey((object) apAdjust2))
          this._copies.Remove((object) apAdjust2);
        this._copies.Add((object) apAdjust2, (object) PXCache<APAdjust>.CreateCopy(apAdjust2));
      }
    }
    this.APDocumentList.Cache.IsDirty = false;
    if (delegateResult.IsResultTruncated)
      PXView.StartRow = 0;
    return (IEnumerable) delegateResult;
  }

  protected virtual PXFilterRow[] SelectDocumentListFilters(PXFilterRow[] filters) => filters;

  protected virtual APAdjust InitRecord(PXResult res)
  {
    APInvoice apInvoice = res.GetItem<APInvoice>();
    APTran apTran = res.GetItem<APTran>();
    APAdjust apAdjust = new APAdjust();
    apAdjust.VendorID = apInvoice.VendorID;
    apAdjust.AdjdDocType = apInvoice.DocType;
    apAdjust.AdjdRefNbr = apInvoice.RefNbr;
    apAdjust.AdjgDocType = "CHK";
    PXCache cach = this.Caches[typeof (APPayment)];
    APPayment data = new APPayment();
    data.DocType = "CHK";
    apAdjust.AdjgRefNbr = AutoNumberAttribute.GetNewNumberSymbol<APPayment.refNbr>(cach, (object) data);
    apAdjust.AdjdLineNbr = new int?(((int?) apTran?.LineNbr).GetValueOrDefault());
    apAdjust.Selected = new bool?(false);
    apAdjust.SeparateCheck = apInvoice.SeparateCheck;
    return apAdjust;
  }

  protected virtual void StoreResultset(PXResult res, string docType, string refNbr, int? lineNbr)
  {
    PXSelectBase<APInvoice, PXSelectJoin<APInvoice, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<Required<APTran.lineNbr>>>>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>>>, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.StoreResult((PXGraph) this, new List<object>()
    {
      (object) new PXResult<APInvoice, APTran, PX.Objects.CM.Extensions.CurrencyInfo>(res.GetItem<APInvoice>(), res.GetItem<APTran>(), res.GetItem<PX.Objects.CM.Extensions.CurrencyInfo>())
    }, PXQueryParameters.ExplicitParameters((object) lineNbr, (object) docType, (object) refNbr));
  }

  /// <summary>
  /// Composes parameters for the BQL command created by
  /// <see cref="M:PX.Objects.AP.APPayBills.ComposeBQLCommandForAPDocumentListSelect" /> method.
  /// This method can be overridden along with <see cref="M:PX.Objects.AP.APPayBills.ComposeBQLCommandForAPDocumentListSelect" />
  /// to modify filtering conditions.
  /// </summary>
  /// <returns>The method returns an array of objects that contain required parameters
  /// for BQL select.</returns>
  public virtual object[] ComposeParametersForAPDocumentListSelect()
  {
    PayBillsFilter current = this.Filter.Current;
    return new object[3]
    {
      (object) current.PayDate.Value.AddDays((double) current.PayInLessThan.GetValueOrDefault()),
      (object) current.PayDate.Value.AddDays((double) current.DueInLessThan.GetValueOrDefault()),
      (object) current.PayDate.Value.AddDays((double) current.DiscountExpiresInLessThan.GetValueOrDefault())
    };
  }

  /// <summary>
  /// Composes a BQL query for the <see cref="M:PX.Objects.AP.APPayBills.apdocumentlist" />
  /// select delegate.
  /// The <see cref="M:PX.Objects.AP.APPayBills.ComposeParametersForAPDocumentListSelect" />
  /// method provides parameters for the query.
  /// </summary>
  public virtual BqlCommand ComposeBQLCommandForAPDocumentListSelect()
  {
    BqlCommand bqlCommand = (BqlCommand) new Select2<APInvoice, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.curyTranBal, NotEqual<decimal0>, PX.Data.And<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<PayBillsFilter.projectID>, PX.Data.IsNull>>>>.Or<BqlOperand<APTran.projectID, IBqlInt>.IsEqual<BqlField<PayBillsFilter.projectID, IBqlInt>.FromCurrent>>>>>>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, InnerJoin<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>, PX.Data.And<Where<Vendor.vStatus, Equal<VendorStatus.active>, Or<Vendor.vStatus, Equal<VendorStatus.oneTime>>>>>, LeftJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>, And<APAdjust.released, Equal<False>, And<APAdjust.voided, Equal<False>, And<APInvoice.isJointPayees, NotEqual<True>>>>>>, LeftJoin<APAdjust2, On<APAdjust2.adjgDocType, Equal<APInvoice.docType>, And<APAdjust2.adjgRefNbr, Equal<APInvoice.refNbr>, And<APAdjust2.released, Equal<False>, And<APInvoice.isJointPayees, NotEqual<True>>>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<APInvoice.docType>, And<APPayment.refNbr, Equal<APInvoice.refNbr>, And<APPayment.docType, Equal<APDocType.prepayment>>>>, LeftJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<APInvoice.vendorID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<APInvoice.vendorLocationID>>>>>>>>>>, Where<APInvoice.manualEntry, Equal<True>, PX.Data.Or<Where<APInvoice.openDoc, Equal<True>, And<APInvoice.hold, Equal<False>, And<APInvoice.curyDocBal, NotEqual<decimal0>, And<APInvoice.pendingPPD, NotEqual<True>, And2<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.paymentsByLinesAllowed, NotEqual<True>>>>>.Or<BqlOperand<APTran.lineNbr, IBqlInt>.IsNotNull>>, And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>, And2<Where<APInvoice.paySel, Equal<True>, Or2<Where<Current<PX.Objects.AP.APSetup.requireApprovePayments>, Equal<False>>, Or<APInvoice.docType, Equal<APDocType.debitAdj>>>>, And2<Where<Vendor.vendorClassID, Equal<Current<PayBillsFilter.vendorClassID>>, Or<Current<PayBillsFilter.vendorClassID>, PX.Data.IsNull>>, And2<Where<APInvoice.vendorID, Equal<Current<PayBillsFilter.vendorID>>, Or<Current<PayBillsFilter.vendorID>, PX.Data.IsNull>>, And2<Where<APInvoice.docType, NotEqual<APDocType.prepaymentInvoice>, Or<APInvoice.pendingPayment, Equal<True>>>, And<APInvoice.payAccountID, Equal<Current<PayBillsFilter.payAccountID>>, And<APInvoice.payTypeID, Equal<Current<PayBillsFilter.payTypeID>>, And<APAdjust.adjgRefNbr, PX.Data.IsNull, And<APAdjust2.adjdRefNbr, PX.Data.IsNull, And<APPayment.refNbr, PX.Data.IsNull, And2<Where2<Where2<Where<Current<PayBillsFilter.showPayInLessThan>, Equal<True>, And<APInvoice.payDate, LessEqual<Required<APInvoice.payDate>>>>, Or2<Where<Current<PayBillsFilter.showDueInLessThan>, Equal<True>, PX.Data.And<Where<APInvoice.dueDate, LessEqual<Required<APInvoice.dueDate>>, Or<APInvoice.dueDate, PX.Data.IsNull>>>>, PX.Data.Or<Where<Current<PayBillsFilter.showDiscountExpiresInLessThan>, Equal<True>, PX.Data.And<Where<APInvoice.discDate, LessEqual<Required<APInvoice.discDate>>, Or<APInvoice.discDate, PX.Data.IsNull>>>>>>>, PX.Data.Or<Where<Current<PayBillsFilter.showPayInLessThan>, Equal<False>, And<Current<PayBillsFilter.showDueInLessThan>, Equal<False>, And<Current<PayBillsFilter.showDiscountExpiresInLessThan>, Equal<False>, Or<APInvoice.docType, Equal<APDocType.debitAdj>>>>>>>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>>>>>>>>>>>>>>>>>>>();
    if (this.Filter.Current.ProjectID.HasValue)
      bqlCommand = bqlCommand.WhereAnd<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.paymentsByLinesAllowed, Equal<True>>>>>.Or<Exists<SelectFromBase<APTran2, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTran2.tranType, Equal<APInvoice.docType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTran2.refNbr, Equal<APInvoice.refNbr>>>>>.And<BqlOperand<APTran.projectID, IBqlInt>.IsEqual<BqlField<PayBillsFilter.projectID, IBqlInt>.FromCurrent>>>>>>>>();
    return bqlCommand;
  }

  public virtual void CreatePayments(
    List<APAdjust> list,
    PayBillsFilter filter,
    PX.Objects.CM.Extensions.CurrencyInfo info,
    PX.Objects.CA.PaymentMethod paymenttype)
  {
    if (RunningFlagScope<APPayBills>.IsRunning)
      throw new PXSetPropertyException("Another 'Prepare Payments' process is already running. Please wait until it is finished.", PXErrorLevel.Warning);
    using (new RunningFlagScope<APPayBills>())
      this._createPayments(list, filter, info, paymenttype);
  }

  protected virtual void _createPayments(
    List<APAdjust> list,
    PayBillsFilter filter,
    PX.Objects.CM.Extensions.CurrencyInfo info,
    PX.Objects.CA.PaymentMethod paymenttype)
  {
    foreach (APAdjust row in list)
    {
      row.AdjgDocDate = filter.PayDate;
      row.AdjgBranchID = filter.BranchID;
      FinPeriodIDAttribute.CalcMasterPeriodID<APAdjust.adjgFinPeriodID>(this.Caches[typeof (APAdjust)], (object) row);
      row.AdjgFinPeriodID = filter.PayFinPeriodID;
      row.AdjgTranPeriodID = this.FinPeriodRepository.GetByID(row.AdjgFinPeriodID, PXAccess.GetParentOrganizationID(row.AdjgBranchID)).MasterFinPeriodID;
    }
    bool flag = false;
    APPaymentEntry paymentEntry = this.CreatePaymentEntry();
    Dictionary<APPayment, PXResult<APPayment, Vendor>> dictionary1 = new Dictionary<APPayment, PXResult<APPayment, Vendor>>((IEqualityComparer<APPayment>) paymentEntry.Document.Cache.GetComparer());
    list.Sort((Comparison<APAdjust>) ((a, b) =>
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = num1 + (1 + ((IComparable) a.VendorID).CompareTo((object) b.VendorID)) / 2 * 1000;
      int num4 = num2 + (1 - ((IComparable) a.VendorID).CompareTo((object) b.VendorID)) / 2 * 1000;
      int num5 = num3 + (a.AdjdDocType == "ADR" ? 100 : 0);
      int num6 = num4 + (b.AdjdDocType == "ADR" ? 100 : 0);
      int num7 = num5;
      Decimal? adjAmt1 = a.AdjAmt;
      Decimal num8 = 0M;
      int num9 = adjAmt1.GetValueOrDefault() < num8 & adjAmt1.HasValue ? 50 : 0;
      int num10 = num7 + num9;
      int num11 = num6;
      Decimal? adjAmt2 = b.AdjAmt;
      Decimal num12 = 0M;
      int num13 = adjAmt2.GetValueOrDefault() < num12 & adjAmt2.HasValue ? 50 : 0;
      int num14 = num11 + num13;
      int num15 = num10 + (1 + a.AdjdRefNbr.CompareTo((object) b.AdjdRefNbr)) / 2 * 10;
      int num16 = num14 + (1 - a.AdjdRefNbr.CompareTo((object) b.AdjdRefNbr)) / 2 * 10;
      int valueOrDefault1 = PXResult.Unwrap<APAdjust>((object) a).AdjdLineNbr.GetValueOrDefault();
      int valueOrDefault2 = PXResult.Unwrap<APAdjust>((object) b).AdjdLineNbr.GetValueOrDefault();
      return (num15 + (1 + ((IComparable) valueOrDefault1).CompareTo((object) valueOrDefault2)) / 2).CompareTo(num16 + (1 - ((IComparable) valueOrDefault1).CompareTo((object) valueOrDefault2)) / 2);
    }));
    Dictionary<string, APAdjust> dictionary2 = new Dictionary<string, APAdjust>();
    foreach (APAdjust apAdjust1 in list)
    {
      string key = $"{apAdjust1.AdjdDocType}_{apAdjust1.AdjdRefNbr}";
      APAdjust apAdjust2;
      if (dictionary2.TryGetValue(key, out apAdjust2))
      {
        bool? separateCheck = apAdjust2.SeparateCheck;
        if (!separateCheck.GetValueOrDefault())
        {
          separateCheck = apAdjust1.SeparateCheck;
          if (separateCheck.GetValueOrDefault())
            apAdjust2.SeparateCheck = new bool?(true);
        }
        apAdjust1.SeparateCheck = new bool?(false);
      }
      else
        dictionary2[key] = apAdjust1;
    }
    foreach (IGrouping<string, APAdjust> grouping in list.GroupBy<APAdjust, string>((Func<APAdjust, string>) (_ => _.AdjdDocType + _.AdjdRefNbr + _.JointPayeeID.ToString())))
    {
      PXProcessing<APAdjust>.SetCurrentItem((object) grouping.Last<APAdjust>());
      try
      {
        foreach (APAdjust row in (IEnumerable<APAdjust>) grouping)
        {
          if (this.CheckIfRowNotApprovedForPayment(row))
            throw new PXSetPropertyException((IBqlTable) row, "Document is not approved for payment and will not be processed.", PXErrorLevel.RowError);
          PXView view = paymentEntry.APInvoice_DocType_RefNbr.View;
          object[] currents = new object[1];
          APPayment apPayment = new APPayment();
          apPayment.VendorID = row.VendorID;
          currents[0] = (object) apPayment;
          object[] objArray = new object[3]
          {
            (object) row.AdjdLineNbr.GetValueOrDefault(),
            (object) row.AdjdDocType,
            (object) row.AdjdRefNbr
          };
          PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran> pxResult = (PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran>) view.SelectSingleBound(currents, objArray);
          APInvoice i0 = (APInvoice) pxResult;
          APTran i1 = (APTran) pxResult;
          i0.PayAccountID = filter.PayAccountID;
          i0.PayTypeID = filter.PayTypeID;
          paymentEntry.TakeDiscAlways = filter.TakeDiscAlways.GetValueOrDefault();
          PXSelectReadonly2<APInvoice, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<Required<APAdjust.adjdLineNbr>>>>>>>, Where<APInvoice.vendorID, Equal<Required<APInvoice.vendorID>>, And<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>> vendorIdDocTypeRefNbr = paymentEntry.APInvoice_VendorID_DocType_RefNbr;
          List<object> selectResult1 = new List<object>();
          selectResult1.Add((object) new PXResult<APInvoice, APTran>(i0, i1));
          PXQueryParameters queryParameters1 = PXQueryParameters.ExplicitParameters((object) row.AdjdLineNbr, (object) row.VendorID, (object) row.AdjdDocType, (object) row.AdjdRefNbr);
          vendorIdDocTypeRefNbr.StoreResult(selectResult1, queryParameters1);
          PXSelectJoin<APInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<Required<APAdjust.adjdLineNbr>>>>>>>>, Where<APInvoice.vendorID, Equal<Current<APPayment.vendorID>>, And<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>> invoiceDocTypeRefNbr = paymentEntry.APInvoice_DocType_RefNbr;
          List<object> selectResult2 = new List<object>();
          selectResult2.Add((object) pxResult);
          PXQueryParameters queryParameters2 = PXQueryParameters.ExplicitParameters((object) row.AdjdLineNbr, (object) row.VendorID, (object) i0.DocType, (object) i0.RefNbr);
          invoiceDocTypeRefNbr.StoreResult(selectResult2, queryParameters2);
        }
        paymentEntry.CreatePayment((IEnumerable<APAdjust>) grouping, info);
        Vendor current1 = paymentEntry.vendor.Current;
        APPayment current2 = paymentEntry.Document.Current;
        paymentEntry.Clear();
        dictionary1[current2] = new PXResult<APPayment, Vendor>(current2, current1);
        PXProcessing<APAdjust>.SetProcessed();
      }
      catch (PXException ex)
      {
        if (ex is PXSetPropertyException e && e.ErrorLevel == PXErrorLevel.Warning)
        {
          PXProcessing<APAdjust>.SetWarning((Exception) e);
        }
        else
        {
          PXProcessing<APAdjust>.SetError((Exception) ex);
          flag = true;
          paymentEntry.Clear();
        }
      }
    }
    List<PXResult<APPayment, Vendor>> payments = new List<PXResult<APPayment, Vendor>>();
    foreach (APPayment key in dictionary1.Keys)
    {
      PXResult<APPayment, Vendor> pxResult = dictionary1[key];
      APPayment payment = (APPayment) pxResult;
      Vendor vendor = (Vendor) pxResult;
      try
      {
        paymentEntry.Clear();
        paymentEntry.Document.Search<APPayment.refNbr>((object) payment.RefNbr, (object) payment.DocType);
        APPayment i0 = this.PaymentPostProcessing(paymentEntry, payment, vendor);
        payments.Add(new PXResult<APPayment, Vendor>(i0, vendor));
      }
      catch (PXException ex)
      {
        if (ex is PXSetPropertyException e && e.ErrorLevel == PXErrorLevel.Warning)
        {
          PXProcessing<APAdjust>.SetWarning((Exception) e);
        }
        else
        {
          PXProcessing<APAdjust>.SetError((Exception) ex);
          flag = true;
          paymentEntry.Clear();
        }
      }
    }
    if (flag)
      throw new PXOperationCompletedWithErrorException("One or more documents could not be released.");
    if (filter.APQuickBatchGeneration.GetValueOrDefault())
    {
      try
      {
        PaymentMethodAccountHelper.VerifyAPLastReferenceNumber((PXGraph) this, filter.PayTypeID, filter.PayAccountID, payments.Count);
      }
      catch (PXException ex)
      {
        throw new PXOperationCompletedWithErrorException(ex.Message);
      }
    }
    if (paymentEntry.created.Count <= 0)
      return;
    this.RedirectToResult(payments, paymenttype, filter);
  }

  protected virtual APPayment PaymentPostProcessing(
    APPaymentEntry pe,
    APPayment payment,
    Vendor vendor)
  {
    APPayment apPayment = PXCache<APPayment>.CreateCopy(pe.Document.Current);
    bool? hold = apPayment.Hold;
    bool flag = false;
    if (!(hold.GetValueOrDefault() == flag & hold.HasValue))
    {
      apPayment.Hold = new bool?(false);
      apPayment = pe.Document.Update(apPayment);
      pe.Save.Press();
    }
    payment.Hold = apPayment.Hold;
    payment.Status = apPayment.Status;
    return payment;
  }

  protected virtual APPaymentEntry CreatePaymentEntry()
  {
    PXRowSelecting handler = (PXRowSelecting) ((cache, e) =>
    {
      if (!(e.Row is APPayment row2))
        return;
      row2.CuryApplAmt = row2.CuryOrigDocAmt;
      row2.CuryUnappliedBal = new Decimal?(0M);
    });
    APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
    instance.RowSelecting.RemoveHandler<APPayment>(new PXRowSelecting(instance.APPayment_RowSelecting));
    instance.RowSelecting.AddHandler<APPayment>(handler);
    return instance;
  }

  protected virtual void RedirectToResult(
    List<PXResult<APPayment, Vendor>> payments,
    PX.Objects.CA.PaymentMethod paymentMethod,
    PayBillsFilter filter)
  {
    bool flag = false;
    foreach (PXResult<APPayment, Vendor> payment in payments)
    {
      if (((APPayment) payment).Status != "E")
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      return;
    PrintChecksFilter printChecksFilter1 = (PrintChecksFilter) null;
    PXGraph graph1;
    PXSelectBase<APPayment> apPaymentList;
    if (paymentMethod != null && paymentMethod.PrintOrExport.GetValueOrDefault() || paymentMethod?.PaymentType == "EPP" && paymentMethod.ExternalPaymentProcessorID != null)
    {
      APPrintChecks instance = PXGraph.CreateInstance<APPrintChecks>();
      instance.Clear();
      printChecksFilter1 = PXCache<PrintChecksFilter>.CreateCopy(instance.Filter.Current);
      this.CopyFilterValues(instance, filter, printChecksFilter1);
      instance.Filter.Cache.Update((object) printChecksFilter1);
      graph1 = (PXGraph) instance;
      apPaymentList = (PXSelectBase<APPayment>) instance.APPaymentList;
    }
    else
    {
      APReleaseChecks instance = PXGraph.CreateInstance<APReleaseChecks>();
      ReleaseChecksFilter copy = PXCache<ReleaseChecksFilter>.CreateCopy(instance.Filter.Current);
      copy.PayTypeID = filter.PayTypeID;
      copy.PayAccountID = filter.PayAccountID;
      copy.CuryID = filter.CuryID;
      instance.Filter.Cache.Update((object) copy);
      graph1 = (PXGraph) instance;
      apPaymentList = (PXSelectBase<APPayment>) instance.APPaymentList;
    }
    if (graph1 != null && apPaymentList != null)
    {
      apPaymentList.Select();
      PXCache cache = apPaymentList.Cache;
      List<APPayment> list = new List<APPayment>();
      foreach (PXResult<APPayment, Vendor> payment in payments)
      {
        APPayment apPayment1 = (APPayment) payment;
        Vendor vendor = (Vendor) payment;
        APPayment apPayment2 = apPaymentList.Locate(apPayment1) ?? apPayment1;
        if (!apPayment2.Selected.GetValueOrDefault())
        {
          graph1.Caches[typeof (Vendor)].Current = (object) vendor;
          apPayment2.Selected = new bool?(true);
          apPayment2.Passed = new bool?(true);
          if (printChecksFilter1 != null)
          {
            PrintChecksFilter printChecksFilter2 = printChecksFilter1;
            Decimal? curySelTotal = printChecksFilter2.CurySelTotal;
            Decimal? curyOrigDocAmt = apPayment2.CuryOrigDocAmt;
            printChecksFilter2.CurySelTotal = curySelTotal.HasValue & curyOrigDocAmt.HasValue ? new Decimal?(curySelTotal.GetValueOrDefault() + curyOrigDocAmt.GetValueOrDefault()) : new Decimal?();
            PrintChecksFilter printChecksFilter3 = printChecksFilter1;
            Decimal? selTotal = printChecksFilter3.SelTotal;
            Decimal? origDocAmt = apPayment2.OrigDocAmt;
            printChecksFilter3.SelTotal = selTotal.HasValue & origDocAmt.HasValue ? new Decimal?(selTotal.GetValueOrDefault() + origDocAmt.GetValueOrDefault()) : new Decimal?();
            PrintChecksFilter printChecksFilter4 = printChecksFilter1;
            int? selCount = printChecksFilter4.SelCount;
            printChecksFilter4.SelCount = selCount.HasValue ? new int?(selCount.GetValueOrDefault() + 1) : new int?();
          }
          cache.SetStatus((object) apPayment2, PXEntryStatus.Updated);
          list.Add(apPayment2);
        }
      }
      graph1.Caches[typeof (PrintChecksFilter)].Update((object) printChecksFilter1);
      cache.IsDirty = false;
      graph1.TimeStamp = this.TimeStamp;
      bool? nullable = filter.APQuickBatchGeneration;
      if (nullable.GetValueOrDefault() && paymentMethod != null)
      {
        nullable = paymentMethod.PrintOrExport;
        if (nullable.GetValueOrDefault())
        {
          APPrintChecks graph2 = (APPrintChecks) graph1;
          printChecksFilter1.NextCheckNbr = PaymentRefAttribute.GetNextPaymentRef((PXGraph) graph2, graph2.cashaccountdetail.Current.CashAccountID, graph2.cashaccountdetail.Current.PaymentMethodID);
          graph2.PrintPayments(list, printChecksFilter1, paymentMethod);
        }
      }
      throw new PXRedirectRequiredException(graph1, "NextProcessing");
    }
  }

  protected virtual void CopyFilterValues(
    APPrintChecks printChecksGraph,
    PayBillsFilter filter,
    PrintChecksFilter printChecksFilter)
  {
    printChecksFilter.BranchID = filter.BranchID;
    printChecksFilter.PayTypeID = filter.PayTypeID;
    printChecksFilter.PayAccountID = filter.PayAccountID;
    printChecksFilter.CuryID = filter.CuryID;
  }

  protected virtual void PayBillsFilter_PayAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    foreach (PXResult<PX.Objects.CM.Extensions.CurrencyInfo> data in PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<PayBillsFilter.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null))
      this.currencyinfo.Cache.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>((object) (PX.Objects.CM.Extensions.CurrencyInfo) data);
  }

  protected virtual void PayBillsFilter_PayTypeID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<PayBillsFilter.payDate>(e.Row);
    sender.SetDefaultExt<PayBillsFilter.payAccountID>(e.Row);
  }

  protected virtual void PayBillsFilter_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<PayBillsFilter.payDate>(e.Row);
  }

  protected virtual void PayBillsFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.Invoice.Cache.Clear();
    this.APDocumentList.Cache.Clear();
    this.APExceptionsList.Cache.Clear();
    this.Filter.Current.CurySelTotal = new Decimal?(0M);
    this.Filter.Current.SelTotal = new Decimal?(0M);
    this.Filter.Current.SelCount = new int?(0);
    int num = !sender.ObjectsEqual<PayBillsFilter.payAccountID>(e.OldRow, e.Row) ? 1 : 0;
    bool flag = !sender.ObjectsEqual<PayBillsFilter.payTypeID>(e.OldRow, e.Row);
    if (num != 0)
      this.Filter.Cache.SetDefaultExt<PayBillsFilter.curyID>(e.Row);
    if ((num | (flag ? 1 : 0)) == 0)
      return;
    this.Filter.Cache.SetDefaultExt<PayBillsFilter.aPQuickBatchGeneration>(e.Row);
  }

  protected virtual void PayBillsFilter_PayDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    sender.RaiseExceptionHandling<PayBillsFilter.payDate>(e.Row, e.NewValue, (Exception) null);
  }

  protected virtual void PayBillsFilter_PayDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    foreach (PXResult<PX.Objects.CM.Extensions.CurrencyInfo> data in PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<PayBillsFilter.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null))
      this.currencyinfo.Cache.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>((object) (PX.Objects.CM.Extensions.CurrencyInfo) data);
    this.APDocumentList.Cache.Clear();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PayBillsFilter.showPayInLessThan> e)
  {
    e?.Cache?.SetDefaultExt<PayBillsFilter.payInLessThan>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PayBillsFilter.showDueInLessThan> e)
  {
    e?.Cache?.SetDefaultExt<PayBillsFilter.dueInLessThan>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PayBillsFilter.showDiscountExpiresInLessThan> e)
  {
    e?.Cache?.SetDefaultExt<PayBillsFilter.discountExpiresInLessThan>(e.Row);
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    foreach (PayBillsFilter payBillsFilter in this.Filter.Cache.Inserted)
      e.NewValue = (object) payBillsFilter.PayDate;
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    foreach (PayBillsFilter payBillsFilter in this.Filter.Cache.Inserted)
    {
      if (this.cashaccount.Current != null && !string.IsNullOrEmpty(this.cashaccount.Current.CuryRateTypeID))
      {
        e.NewValue = (object) this.cashaccount.Current.CuryRateTypeID;
        e.Cancel = true;
      }
    }
  }

  [PXCustomizeBaseAttribute(typeof (VendorAttribute), "DisplayName", "Vendor ID")]
  protected virtual void _(PX.Data.Events.CacheAttached<APAdjust.vendorID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDimensionSelector("SUBACCOUNT", typeof (PX.Objects.GL.Sub.subCD), typeof (PX.Objects.GL.Sub.subCD))]
  [PXRemoveBaseAttribute(typeof (SubAccountAttribute))]
  protected virtual void APAdjust_AdjdAPSub_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (PayBillsFilter.curyInfoID))]
  protected virtual void APAdjust_AdjgCuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDefault("CHK")]
  protected virtual void APAdjust_AdjgDocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  protected virtual void APAdjust_AdjgRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDefault("INV")]
  [PXUIField(DisplayName = "Document Type", Visibility = PXUIVisibility.Visible)]
  [APInvoiceType.List]
  protected virtual void APAdjust_AdjdDocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible)]
  [APInvoiceType.AdjdRefNbr(typeof (Search2<APInvoice.refNbr, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<APInvoice.vendorID>, PX.Data.And<Where<PX.Objects.CR.BAccount.vStatus, Equal<VendorStatus.active>, Or<PX.Objects.CR.BAccount.vStatus, Equal<VendorStatus.oneTime>>>>>, LeftJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>, And<APAdjust.released, Equal<False>, And<APAdjust.voided, Equal<False>, PX.Data.And<Where<APAdjust.adjgDocType, NotEqual<Current<APPayment.docType>>, Or<APAdjust.adjgRefNbr, NotEqual<Current<APPayment.refNbr>>>>>>>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<APInvoice.docType>, And<APPayment.refNbr, Equal<APInvoice.refNbr>, And<APPayment.docType, Equal<APDocType.prepayment>>>>>>>, Where<APInvoice.docType, Equal<Optional<APAdjust.adjdDocType>>, And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>, And<APInvoice.openDoc, Equal<True>, And2<Where<APInvoice.docType, NotEqual<APDocType.prepaymentInvoice>, Or<APInvoice.pendingPayment, Equal<True>>>, And<APInvoice.hold, Equal<False>, And<APAdjust.adjgRefNbr, PX.Data.IsNull, And<APPayment.refNbr, PX.Data.IsNull, And<APInvoice.pendingPPD, NotEqual<True>>>>>>>>>>), Filterable = true)]
  protected virtual void APAdjust_AdjdRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  protected virtual void APAdjust_AdjdWhTaxAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  protected virtual void APAdjust_AdjdWhTaxSubID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void APAdjust_VendorID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void APAdjust_CuryDocBal_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row != null && ((APAdjust) e.Row).AdjdCuryInfoID.HasValue && !((APAdjust) e.Row).CuryDocBal.HasValue)
      this.CalcBalances((APAdjust) e.Row, false);
    if (e.Row != null)
      e.NewValue = (object) ((APAdjust) e.Row).CuryDocBal;
    e.Cancel = true;
  }

  protected virtual void APAdjust_AdjdRefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<APAdjust.adjdLineNbr>(e.Row);
  }

  protected virtual void APAdjust_CuryDiscBal_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row != null && ((APAdjust) e.Row).AdjdCuryInfoID.HasValue && !((APAdjust) e.Row).CuryDiscBal.HasValue)
      this.CalcBalances((APAdjust) e.Row, false);
    if (e.Row != null)
      e.NewValue = (object) ((APAdjust) e.Row).CuryDiscBal;
    e.Cancel = true;
  }

  protected virtual void APAdjust_CuryAdjgAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    APAdjust row = (APAdjust) e.Row;
    if (!row.CuryDocBal.HasValue)
      this.CalcBalances(row, false);
    Decimal? nullable1 = row.CuryAdjgAmt;
    if (!nullable1.HasValue)
      row.CuryAdjgAmt = new Decimal?(0M);
    nullable1 = row.CuryAdjgDiscAmt;
    if (!nullable1.HasValue)
      row.CuryAdjgDiscAmt = new Decimal?(0M);
    nullable1 = row.CuryOrigDocAmt;
    Decimal num1 = 0M;
    Sign sign1 = nullable1.GetValueOrDefault() < num1 & nullable1.HasValue ? Sign.Minus : Sign.Plus;
    nullable1 = (Decimal?) e.NewValue;
    Decimal num2 = 0M;
    Sign sign2 = nullable1.GetValueOrDefault() < num2 & nullable1.HasValue ? Sign.Minus : Sign.Plus;
    if (Sign.op_Inequality(sign1, sign2) && Sign.op_Multiply((Decimal) e.NewValue, sign1) < 0M)
      throw new PXSetPropertyException(Sign.op_Equality(sign1, Sign.Plus) ? "Incorrect value. The value to be entered must be greater than or equal to {0}." : "Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
      {
        (object) 0.ToString()
      });
    if (Sign.op_Inequality(sign1, sign2) && Sign.op_Multiply((Decimal) e.NewValue, sign1) > 0M)
      throw new PXSetPropertyException(Sign.op_Equality(sign1, Sign.Plus) ? "Incorrect value. The value to be entered must be less than or equal to {0}." : "Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) 0.ToString()
      });
    Decimal? curyDocBal = row.CuryDocBal;
    Decimal? curyAdjgAmt = row.CuryAdjgAmt;
    Decimal? nullable2 = curyDocBal.HasValue & curyAdjgAmt.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() + curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
    Decimal newValue = (Decimal) e.NewValue;
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - newValue) : new Decimal?();
    Sign sign3 = sign1;
    nullable1 = nullable3.HasValue ? new Decimal?(Sign.op_Multiply(nullable3.GetValueOrDefault(), sign3)) : new Decimal?();
    Decimal num3 = 0M;
    if (nullable1.GetValueOrDefault() < num3 & nullable1.HasValue)
    {
      string format = Sign.op_Equality(sign1, Sign.Plus) ? "The amount must be less than or equal to {0}." : "Incorrect value. The value to be entered must be greater than or equal to {0}.";
      object[] objArray = new object[1];
      nullable1 = row.CuryDocBal;
      Decimal num4 = nullable1.Value;
      nullable1 = row.CuryAdjgAmt;
      Decimal num5 = nullable1.Value;
      objArray[0] = (object) (num4 + num5).ToString();
      throw new PXSetPropertyException(format, objArray);
    }
  }

  protected virtual void APAdjust_CuryAdjgAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.OldValue != null)
    {
      Decimal? nullable = ((APAdjust) e.Row).CuryDocBal;
      Decimal num = 0M;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      {
        nullable = ((APAdjust) e.Row).CuryAdjgAmt;
        Decimal oldValue = (Decimal) e.OldValue;
        if (nullable.GetValueOrDefault() < oldValue & nullable.HasValue)
          ((APAdjust) e.Row).CuryAdjgDiscAmt = new Decimal?(0M);
      }
    }
    this.CalcBalances((APAdjust) e.Row, true);
  }

  protected virtual void APAdjust_CuryAdjgDiscAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    APAdjust row = (APAdjust) e.Row;
    if (!row.CuryDiscBal.HasValue)
      this.CalcBalances(row, false);
    Decimal? nullable1 = row.CuryAdjgAmt;
    if (!nullable1.HasValue)
      row.CuryAdjgAmt = new Decimal?(0M);
    nullable1 = row.CuryAdjgDiscAmt;
    if (!nullable1.HasValue)
      row.CuryAdjgDiscAmt = new Decimal?(0M);
    Decimal? nullable2 = row.CuryDiscBal;
    Decimal? curyAdjgDiscAmt1 = row.CuryAdjgDiscAmt;
    nullable1 = nullable2.HasValue & curyAdjgDiscAmt1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + curyAdjgDiscAmt1.GetValueOrDefault()) : new Decimal?();
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
    {
      nullable1 = (Decimal?) e.NewValue;
      Decimal num2 = 0M;
      if (nullable1.GetValueOrDefault() > num2 & nullable1.HasValue)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be equal to {0}.", new object[1]
        {
          (object) 0.ToString()
        });
    }
    Decimal? curyDiscBal = row.CuryDiscBal;
    Decimal? curyAdjgDiscAmt2 = row.CuryAdjgDiscAmt;
    Decimal? nullable3 = curyDiscBal.HasValue & curyAdjgDiscAmt2.HasValue ? new Decimal?(curyDiscBal.GetValueOrDefault() + curyAdjgDiscAmt2.GetValueOrDefault()) : new Decimal?();
    nullable2 = (Decimal?) e.NewValue;
    nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal num3 = 0M;
    if (nullable1.GetValueOrDefault() < num3 & nullable1.HasValue)
    {
      object[] objArray = new object[1];
      nullable1 = row.CuryDiscBal;
      Decimal num4 = nullable1.Value;
      nullable1 = row.CuryAdjgDiscAmt;
      Decimal num5 = nullable1.Value;
      objArray[0] = (object) (num4 + num5).ToString();
      throw new PXSetPropertyException("The entered amount must be less than or equal to {0}.", objArray);
    }
    nullable1 = row.CuryAdjgAmt;
    if (!nullable1.HasValue)
      return;
    if (sender.GetValuePending<APAdjust.curyAdjgAmt>(e.Row) != PXCache.NotSetValue)
    {
      nullable1 = (Decimal?) sender.GetValuePending<APAdjust.curyAdjgAmt>(e.Row);
      nullable2 = row.CuryAdjgAmt;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        return;
    }
    nullable2 = row.CuryDocBal;
    Decimal num6 = nullable2.Value;
    nullable2 = row.CuryAdjgDiscAmt;
    Decimal num7 = nullable2.Value;
    if (num6 + num7 - (Decimal) e.NewValue < 0M)
    {
      object[] objArray = new object[1];
      nullable2 = row.CuryDocBal;
      Decimal num8 = nullable2.Value;
      nullable2 = row.CuryAdjgDiscAmt;
      Decimal num9 = nullable2.Value;
      objArray[0] = (object) (num8 + num9).ToString();
      throw new PXSetPropertyException("The entered amount must be less than or equal to {0}.", objArray);
    }
  }

  protected virtual void APAdjust_CuryAdjgDiscAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.CalcBalances((APAdjust) e.Row, true);
  }

  private void CalcBalances(APAdjust row, bool isCalcRGOL)
  {
    APAdjust adj = row;
    using (IEnumerator<PXResult<APInvoice>> enumerator = PXSelectBase<APInvoice, PXSelectJoin<APInvoice, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<Required<APTran.lineNbr>>>>>>>, Where<APInvoice.vendorID, Equal<Required<APInvoice.vendorID>>, And<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>>.Config>.Select((PXGraph) this, (object) adj.AdjdLineNbr, (object) adj.VendorID, (object) adj.AdjdDocType, (object) adj.AdjdRefNbr).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      PXResult<APInvoice, APTran> current = (PXResult<APInvoice, APTran>) enumerator.Current;
      APInvoice voucher = (APInvoice) current;
      APTran tran = (APTran) current;
      this.CalcBalances(adj, voucher, tran, isCalcRGOL);
    }
  }

  protected virtual void CalcBalances(
    APAdjust adj,
    APInvoice voucher,
    APTran tran,
    bool isCalcRGOL)
  {
    try
    {
      APPaymentBalanceCalculator balanceCalculator = new APPaymentBalanceCalculator((IPXCurrencyHelper) this.GetExtension<APPayBills.MultiCurrency>());
      APAdjust adj1 = adj;
      APInvoice originalInvoice = voucher;
      int num1 = isCalcRGOL ? 1 : 0;
      bool? takeDiscAlways = this.Filter.Current.TakeDiscAlways;
      bool flag = false;
      int num2 = takeDiscAlways.GetValueOrDefault() == flag & takeDiscAlways.HasValue ? 1 : 0;
      APTran tran1 = tran;
      balanceCalculator.CalcBalances<APInvoice>(adj1, originalInvoice, num1 != 0, num2 != 0, tran1);
    }
    catch (PXRateIsNotDefinedForThisDateException ex)
    {
      this.APDocumentList.Cache.RaiseExceptionHandling<APAdjust.curyAdjgAmt>((object) adj, (object) 0M, (Exception) new PXSetPropertyException(ex.Message, PXErrorLevel.RowError));
    }
  }

  protected virtual void APAdjust_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    PayBillsFilter current = this.Filter.Current;
    if (current != null)
    {
      APAdjust row = e.Row as APAdjust;
      PayBillsFilter payBillsFilter1 = current;
      Decimal? curySelTotal = payBillsFilter1.CurySelTotal;
      Decimal? nullable1;
      if (!row.Selected.GetValueOrDefault())
      {
        nullable1 = new Decimal?(0M);
      }
      else
      {
        Decimal? adjgBalSign = row.AdjgBalSign;
        Decimal? curyAdjgAmt = row.CuryAdjgAmt;
        nullable1 = adjgBalSign.HasValue & curyAdjgAmt.HasValue ? new Decimal?(adjgBalSign.GetValueOrDefault() * curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
      }
      Decimal? nullable2 = nullable1;
      payBillsFilter1.CurySelTotal = curySelTotal.HasValue & nullable2.HasValue ? new Decimal?(curySelTotal.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      PayBillsFilter payBillsFilter2 = current;
      nullable2 = payBillsFilter2.SelTotal;
      Decimal? nullable3;
      if (!row.Selected.GetValueOrDefault())
      {
        nullable3 = new Decimal?(0M);
      }
      else
      {
        Decimal? adjgBalSign = row.AdjgBalSign;
        Decimal? adjAmt = row.AdjAmt;
        nullable3 = adjgBalSign.HasValue & adjAmt.HasValue ? new Decimal?(adjgBalSign.GetValueOrDefault() * adjAmt.GetValueOrDefault()) : new Decimal?();
      }
      Decimal? nullable4 = nullable3;
      payBillsFilter2.SelTotal = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      PayBillsFilter payBillsFilter3 = current;
      int? selCount = payBillsFilter3.SelCount;
      int num = row.Selected.GetValueOrDefault() ? 1 : 0;
      payBillsFilter3.SelCount = selCount.HasValue ? new int?(selCount.GetValueOrDefault() + num) : new int?();
    }
    if (!this.CheckIfRowNotApprovedForPayment((APAdjust) e.Row))
      return;
    this.APDocumentList.Cache.RaiseExceptionHandling<APAdjust.curyAdjgAmt>((object) (APAdjust) e.Row, (object) 0M, (Exception) new PXSetPropertyException("Document is not approved for payment and will not be processed.", PXErrorLevel.RowWarning));
  }

  protected virtual void APAdjust_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PayBillsFilter current = this.Filter.Current;
    if (current == null)
      return;
    object oldRow = e.OldRow;
    if (e.Row == e.OldRow && !this._copies.TryGetValue(e.Row, out oldRow))
    {
      Decimal? nullable1 = new Decimal?(0M);
      Decimal? nullable2 = new Decimal?(0M);
      int? nullable3 = new int?(0);
      foreach (PXResult<APAdjust> pxResult in this.APDocumentList.Select(new object[1]))
      {
        APAdjust apAdjust = (APAdjust) pxResult;
        if (apAdjust.Selected.GetValueOrDefault())
        {
          Decimal? nullable4 = nullable1;
          Decimal? nullable5 = apAdjust.AdjgBalSign;
          Decimal? curyAdjgAmt = apAdjust.CuryAdjgAmt;
          Decimal valueOrDefault1 = (nullable5.HasValue & curyAdjgAmt.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * curyAdjgAmt.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
          nullable1 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
          nullable5 = nullable2;
          Decimal? adjgBalSign = apAdjust.AdjgBalSign;
          Decimal? adjAmt = apAdjust.AdjAmt;
          Decimal valueOrDefault2 = (adjgBalSign.HasValue & adjAmt.HasValue ? new Decimal?(adjgBalSign.GetValueOrDefault() * adjAmt.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
          nullable2 = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
          int? nullable6 = nullable3;
          nullable3 = nullable6.HasValue ? new int?(nullable6.GetValueOrDefault() + 1) : new int?();
        }
      }
      current.CurySelTotal = nullable1;
      current.SelTotal = nullable2;
      current.SelCount = nullable3;
    }
    else
    {
      APAdjust apAdjust = oldRow as APAdjust;
      APAdjust row = e.Row as APAdjust;
      PayBillsFilter payBillsFilter1 = current;
      Decimal? curySelTotal = payBillsFilter1.CurySelTotal;
      bool? selected = apAdjust.Selected;
      Decimal? nullable7;
      Decimal? nullable8;
      if (!selected.GetValueOrDefault())
      {
        nullable8 = new Decimal?(0M);
      }
      else
      {
        Decimal? adjgBalSign = apAdjust.AdjgBalSign;
        nullable7 = apAdjust.CuryAdjgAmt;
        nullable8 = adjgBalSign.HasValue & nullable7.HasValue ? new Decimal?(adjgBalSign.GetValueOrDefault() * nullable7.GetValueOrDefault()) : new Decimal?();
      }
      Decimal? nullable9 = nullable8;
      Decimal? nullable10;
      if (!(curySelTotal.HasValue & nullable9.HasValue))
      {
        nullable7 = new Decimal?();
        nullable10 = nullable7;
      }
      else
        nullable10 = new Decimal?(curySelTotal.GetValueOrDefault() - nullable9.GetValueOrDefault());
      payBillsFilter1.CurySelTotal = nullable10;
      PayBillsFilter payBillsFilter2 = current;
      nullable9 = payBillsFilter2.CurySelTotal;
      selected = row.Selected;
      Decimal? nullable11;
      Decimal? nullable12;
      if (!selected.GetValueOrDefault())
      {
        nullable12 = new Decimal?(0M);
      }
      else
      {
        nullable7 = row.AdjgBalSign;
        nullable11 = row.CuryAdjgAmt;
        nullable12 = nullable7.HasValue & nullable11.HasValue ? new Decimal?(nullable7.GetValueOrDefault() * nullable11.GetValueOrDefault()) : new Decimal?();
      }
      Decimal? nullable13 = nullable12;
      Decimal? nullable14;
      if (!(nullable9.HasValue & nullable13.HasValue))
      {
        nullable11 = new Decimal?();
        nullable14 = nullable11;
      }
      else
        nullable14 = new Decimal?(nullable9.GetValueOrDefault() + nullable13.GetValueOrDefault());
      payBillsFilter2.CurySelTotal = nullable14;
      PayBillsFilter payBillsFilter3 = current;
      nullable13 = payBillsFilter3.SelTotal;
      selected = apAdjust.Selected;
      Decimal? nullable15;
      if (!selected.GetValueOrDefault())
      {
        nullable15 = new Decimal?(0M);
      }
      else
      {
        nullable11 = apAdjust.AdjgBalSign;
        nullable7 = apAdjust.AdjAmt;
        nullable15 = nullable11.HasValue & nullable7.HasValue ? new Decimal?(nullable11.GetValueOrDefault() * nullable7.GetValueOrDefault()) : new Decimal?();
      }
      nullable9 = nullable15;
      Decimal? nullable16;
      if (!(nullable13.HasValue & nullable9.HasValue))
      {
        nullable7 = new Decimal?();
        nullable16 = nullable7;
      }
      else
        nullable16 = new Decimal?(nullable13.GetValueOrDefault() - nullable9.GetValueOrDefault());
      payBillsFilter3.SelTotal = nullable16;
      PayBillsFilter payBillsFilter4 = current;
      nullable9 = payBillsFilter4.SelTotal;
      selected = row.Selected;
      Decimal? nullable17;
      if (!selected.GetValueOrDefault())
      {
        nullable17 = new Decimal?(0M);
      }
      else
      {
        nullable7 = row.AdjgBalSign;
        nullable11 = row.AdjAmt;
        nullable17 = nullable7.HasValue & nullable11.HasValue ? new Decimal?(nullable7.GetValueOrDefault() * nullable11.GetValueOrDefault()) : new Decimal?();
      }
      nullable13 = nullable17;
      Decimal? nullable18;
      if (!(nullable9.HasValue & nullable13.HasValue))
      {
        nullable11 = new Decimal?();
        nullable18 = nullable11;
      }
      else
        nullable18 = new Decimal?(nullable9.GetValueOrDefault() + nullable13.GetValueOrDefault());
      payBillsFilter4.SelTotal = nullable18;
      PayBillsFilter payBillsFilter5 = current;
      int? selCount = payBillsFilter5.SelCount;
      selected = apAdjust.Selected;
      int num1 = selected.GetValueOrDefault() ? 1 : 0;
      payBillsFilter5.SelCount = selCount.HasValue ? new int?(selCount.GetValueOrDefault() - num1) : new int?();
      PayBillsFilter payBillsFilter6 = current;
      selCount = payBillsFilter6.SelCount;
      selected = row.Selected;
      int num2 = selected.GetValueOrDefault() ? 1 : 0;
      payBillsFilter6.SelCount = selCount.HasValue ? new int?(selCount.GetValueOrDefault() + num2) : new int?();
    }
  }

  protected virtual void CurrencyInfo_SampleCuryRate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (this.Filter.Current == null)
      return;
    long? curyInfoId1 = ((PX.Objects.CM.Extensions.CurrencyInfo) e.Row).CuryInfoID;
    long? curyInfoId2 = this.Filter.Current.CuryInfoID;
    if (!(curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue))
      return;
    if ((PX.Objects.CM.Extensions.CurrencyInfo) this.CurrencyInfo_CuryInfoID.Select((object) this.Filter.Current.CuryInfoID) == null)
      return;
    foreach (PXResult<APAdjust, APInvoice, APTran> pxResult in this.APDocumentList.Select())
      this.CalcBalances((APAdjust) pxResult, (APInvoice) pxResult, (APTran) pxResult, true);
    this.APDocumentList.View.RequestRefresh();
  }

  protected virtual void CurrencyInfo_SampleRecipRate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (this.Filter.Current == null)
      return;
    long? curyInfoId1 = ((PX.Objects.CM.Extensions.CurrencyInfo) e.Row).CuryInfoID;
    long? curyInfoId2 = this.Filter.Current.CuryInfoID;
    if (!(curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue))
      return;
    if ((PX.Objects.CM.Extensions.CurrencyInfo) this.CurrencyInfo_CuryInfoID.Select((object) this.Filter.Current.CuryInfoID) == null)
      return;
    foreach (PXResult<APAdjust, APInvoice, APTran> pxResult in this.APDocumentList.Select())
      this.CalcBalances((APAdjust) pxResult, (APInvoice) pxResult, (APTran) pxResult, true);
    this.APDocumentList.View.RequestRefresh();
  }

  public override int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (viewName == "Filter" && this.Filter.Current != null)
    {
      int? selCount = this.Filter.Current.SelCount;
      int num = 0;
      if (selCount.GetValueOrDefault() > num & selCount.HasValue && this.Filter.View.Ask(viewName, "Some documents are selected in the table. Once you change any criteria, all the documents will be unselected. Do you want to continue?", MessageButtons.YesNo) == WebDialogResult.No)
        return 0;
    }
    return base.ExecuteUpdate(viewName, keys, values, parameters);
  }

  public class MultiCurrency : APMultiCurrencyGraph<APPayBills, PayBillsFilter>
  {
    protected override string DocumentStatus => "B";

    protected override MultiCurrencyGraph<APPayBills, PayBillsFilter>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<APPayBills, PayBillsFilter>.CurySourceMapping(typeof (PX.Objects.CA.CashAccount))
      {
        CuryID = typeof (PX.Objects.CA.CashAccount.curyID),
        CuryRateTypeID = typeof (PX.Objects.CA.CashAccount.curyRateTypeID)
      };
    }

    protected override MultiCurrencyGraph<APPayBills, PayBillsFilter>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<APPayBills, PayBillsFilter>.DocumentMapping(typeof (PayBillsFilter))
      {
        DocumentDate = typeof (PayBillsFilter.payDate)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.Filter
      };
    }

    protected override void DateFieldUpdated<CuryInfoID, DocumentDate>(
      PXCache sender,
      IBqlTable row)
    {
    }
  }

  protected class APDocumentListViewExecuteParamsBuilder
  {
    protected readonly string APInvoiceViewPrefix = "APInvoice__";
    protected Dictionary<string, string> AdjFieldsToSubstituteByName;
    protected string DocTypeFieldName;
    protected string RefNbrFieldName;
    protected string AdjdDocTypeFieldName;
    protected string AdjdRefNbrFieldName;

    public APDocumentListViewExecuteParamsBuilder()
    {
      this.DocTypeFieldName = typeof (APInvoice.docType).Name.Capitalize();
      this.RefNbrFieldName = typeof (APInvoice.refNbr).Name.Capitalize();
      this.AdjdDocTypeFieldName = typeof (APAdjust.adjdDocType).Name.Capitalize();
      this.AdjdRefNbrFieldName = typeof (APAdjust.adjdRefNbr).Name.Capitalize();
      this.AdjFieldsToSubstituteByName = new Dictionary<string, string>()
      {
        {
          this.AdjdDocTypeFieldName,
          this.DocTypeFieldName
        },
        {
          this.AdjdRefNbrFieldName,
          this.RefNbrFieldName
        }
      };
    }

    public virtual APPayBills.APDocumentListViewExecuteParamsBuilder.ViewExecutingParams BuildViewExecutingParams(
      PXView view)
    {
      APPayBills.APDocumentListViewExecuteParamsBuilder.ViewExecutingParams viewExecutingParams = this.BuildSortingAndDescendings(view);
      viewExecutingParams.FilterRows = this.BuildFilters(view);
      return viewExecutingParams;
    }

    protected virtual APPayBills.APDocumentListViewExecuteParamsBuilder.ViewExecutingParams BuildSortingAndDescendings(
      PXView view)
    {
      APPayBills.APDocumentListViewExecuteParamsBuilder.ViewExecutingParams viewExecutingParams = new APPayBills.APDocumentListViewExecuteParamsBuilder.ViewExecutingParams()
      {
        Sorts = new List<string>(),
        Descendings = new List<bool>(),
        FilterRows = new List<PXFilterRow>()
      };
      string[] externalSorts = view.GetExternalSorts();
      bool[] externalDescendings = view.GetExternalDescendings();
      if (externalSorts != null)
      {
        for (int index = 0; index < externalSorts.Length; ++index)
        {
          string str = externalSorts[index];
          if (this.AdjFieldsToSubstituteByName.ContainsKey(str))
            viewExecutingParams.Sorts.Add(this.AdjFieldsToSubstituteByName[str]);
          else if (str.Contains(this.APInvoiceViewPrefix))
            viewExecutingParams.Sorts.Add(this.ConvertToFieldNameWithoutPrefix(str, this.APInvoiceViewPrefix));
          else
            viewExecutingParams.Sorts.Add(str);
          viewExecutingParams.Descendings.Add(externalDescendings[index]);
        }
      }
      string[] strArray = new string[2]
      {
        this.DocTypeFieldName,
        this.RefNbrFieldName
      };
      foreach (string str in strArray)
      {
        if (!viewExecutingParams.Sorts.Contains(str))
        {
          viewExecutingParams.Sorts.Add(str);
          viewExecutingParams.Descendings.Add(false);
        }
      }
      return viewExecutingParams;
    }

    protected virtual List<PXFilterRow> BuildFilters(PXView view)
    {
      PXFilterRow[] externalFilters = view.GetExternalFilters();
      List<PXFilterRow> pxFilterRowList = (externalFilters != null ? ((IEnumerable<PXFilterRow>) externalFilters).ToList<PXFilterRow>() : (List<PXFilterRow>) null) ?? new List<PXFilterRow>();
      foreach (PXFilterRow pxFilterRow in pxFilterRowList)
      {
        if (this.AdjFieldsToSubstituteByName.ContainsKey(pxFilterRow.DataField))
          pxFilterRow.DataField = this.AdjFieldsToSubstituteByName[pxFilterRow.DataField];
        else if (pxFilterRow.DataField.Contains(this.APInvoiceViewPrefix))
          pxFilterRow.DataField = this.ConvertToFieldNameWithoutPrefix(pxFilterRow.DataField, this.APInvoiceViewPrefix);
      }
      return pxFilterRowList;
    }

    private string ConvertToFieldNameWithoutPrefix(string fieldNameWithPrefix, string prefix)
    {
      return char.ToUpper(fieldNameWithPrefix[prefix.Length]).ToString() + fieldNameWithPrefix.Substring(prefix.Length + 1, fieldNameWithPrefix.Length - prefix.Length - 1);
    }

    public class ViewExecutingParams
    {
      public List<string> Sorts;
      public List<bool> Descendings;
      public List<PXFilterRow> FilterRows;
    }
  }
}
