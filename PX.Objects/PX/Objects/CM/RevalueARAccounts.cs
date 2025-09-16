// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.RevalueARAccounts
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.Overrides.ARDocumentRelease;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CM;

[TableAndChartDashboardType]
public class RevalueARAccounts : RevalueAcountsBase<RevaluedARHistory>
{
  public PXCancel<RevalueFilter> Cancel;
  public PXFilter<RevalueFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<RevaluedARHistory, RevalueFilter, Where<boolTrue, Equal<boolTrue>>, OrderBy<Asc<RevaluedARHistory.accountID, Asc<RevaluedARHistory.subID, Asc<RevaluedARHistory.customerID>>>>> ARAccountList;
  public PXSelect<CurrencyInfo> currencyinfo;
  public PXSetup<ARSetup> arsetup;
  public PXSetup<CMSetup> cmsetup;
  public PXSetup<Company> company;

  public RevalueARAccounts()
  {
    ARSetup current1 = ((PXSelectBase<ARSetup>) this.arsetup).Current;
    CMSetup current2 = ((PXSelectBase<CMSetup>) this.cmsetup).Current;
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      throw new Exception("Multi-Currency is not activated");
    ((PXProcessing<RevaluedARHistory>) this.ARAccountList).SetProcessCaption("Revalue");
    ((PXProcessing<RevaluedARHistory>) this.ARAccountList).SetProcessAllVisible(false);
    PXUIFieldAttribute.SetEnabled<RevaluedARHistory.finPtdRevalued>(((PXSelectBase) this.ARAccountList).Cache, (object) null, true);
  }

  protected virtual void RevalueFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RevalueARAccounts.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new RevalueARAccounts.\u003C\u003Ec__DisplayClass8_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass80.filter = (RevalueFilter) e.Row;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass80.filter == null)
      return;
    // ISSUE: method pointer
    ((PXProcessingBase<RevaluedARHistory>) this.ARAccountList).SetProcessDelegate(new PXProcessingBase<RevaluedARHistory>.ProcessListDelegate((object) cDisplayClass80, __methodptr(\u003CRevalueFilter_RowSelected\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    this.VerifyCurrencyEffectiveDate(sender, cDisplayClass80.filter);
  }

  protected virtual void RevalueFilter_OrgBAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RevalueFilter row = (RevalueFilter) e.Row;
    if (row != null)
      row.CuryID = (string) null;
    ((PXSelectBase) this.ARAccountList).Cache.Clear();
    sender.SetDefaultExt<RevalueFilter.curyEffDate>(e.Row);
  }

  protected virtual void RevalueFilter_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
  }

  protected virtual void RevalueFilter_FinPeriodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.ARAccountList).Cache.Clear();
    sender.SetDefaultExt<RevalueFilter.curyEffDate>(e.Row);
  }

  protected virtual void RevalueFilter_CuryEffDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.ARAccountList).Cache.Clear();
  }

  protected virtual void RevalueFilter_CuryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.ARAccountList).Cache.Clear();
  }

  protected virtual void RevalueFilter_TotalRevalued_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    Decimal num = 0M;
    foreach (RevaluedARHistory revaluedArHistory in ((PXSelectBase) this.ARAccountList).Cache.Updated)
    {
      if (revaluedArHistory.Selected.Value)
        num += revaluedArHistory.FinPtdRevalued.Value;
    }
    if (num == 0M)
    {
      TimeSpan timeSpan;
      Exception exception;
      if (PXLongOperation.GetStatus(((PXGraph) this).UID, ref timeSpan, ref exception) != 2)
        return;
      ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<RevalueFilter.totalRevalued>(e.Row, (object) num, (Exception) new PXSetPropertyException("No revaluation entry was made since Original Balance equals the Revalued Balance", (PXErrorLevel) 2));
    }
    else
      e.ReturnValue = (object) num;
  }

  public virtual IEnumerable araccountlist()
  {
    RevalueARAccounts revalueArAccounts = this;
    foreach (PXResult<ARHistoryByPeriod, RevaluedARHistory, Customer, PX.Objects.GL.Branch, FinPeriod, ARHistoryLastRevaluation> pxResult in PXSelectBase<ARHistoryByPeriod, PXSelectJoin<ARHistoryByPeriod, LeftJoin<RevaluedARHistory, On<RevaluedARHistory.customerID, Equal<ARHistoryByPeriod.customerID>, And<RevaluedARHistory.branchID, Equal<ARHistoryByPeriod.branchID>, And<RevaluedARHistory.accountID, Equal<ARHistoryByPeriod.accountID>, And<RevaluedARHistory.subID, Equal<ARHistoryByPeriod.subID>, And<RevaluedARHistory.curyID, Equal<ARHistoryByPeriod.curyID>, And<RevaluedARHistory.finPeriodID, Equal<ARHistoryByPeriod.lastActivityPeriod>>>>>>>, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARHistoryByPeriod.customerID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARHistoryByPeriod.branchID>, And<Where<PX.Objects.GL.Branch.branchID, InsideBranchesOf<Current2<RevalueFilter.orgBAccountID>>, Or<Current2<RevalueFilter.orgBAccountID>, IsNull, And<Not<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>>>>>>, InnerJoin<FinPeriod, On<ARHistoryByPeriod.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, LeftJoin<ARHistoryLastRevaluation, On<ARHistoryByPeriod.customerID, Equal<ARHistoryLastRevaluation.customerID>, And<ARHistoryByPeriod.branchID, Equal<ARHistoryLastRevaluation.branchID>, And<ARHistoryByPeriod.accountID, Equal<ARHistoryLastRevaluation.accountID>, And<ARHistoryByPeriod.subID, Equal<ARHistoryLastRevaluation.subID>, And<ARHistoryByPeriod.curyID, Equal<ARHistoryLastRevaluation.curyID>>>>>>>>>>>, Where<ARHistoryByPeriod.curyID, Equal<Current<RevalueFilter.curyID>>, And2<Where2<Where<FinPeriod.masterFinPeriodID, Equal<Current<RevalueFilter.finPeriodID>>, And<Current2<RevalueFilter.orgBAccountID>, IsNull>>, Or<Where<FinPeriod.finPeriodID, Equal<Current<RevalueFilter.finPeriodID>>, And<Current2<RevalueFilter.orgBAccountID>, IsNotNull>>>>, And<Where<RevaluedARHistory.curyFinYtdBalance, NotEqual<decimal0>, Or<RevaluedARHistory.finYtdBalance, NotEqual<decimal0>, Or<RevaluedARHistory.finPtdRevalued, NotEqual<decimal0>, Or<RevaluedARHistory.curyFinYtdDeposits, NotEqual<decimal0>, Or<RevaluedARHistory.finYtdDeposits, NotEqual<decimal0>>>>>>>>>>.Config>.Select((PXGraph) revalueArAccounts, Array.Empty<object>()))
    {
      ARHistoryByPeriod arHistoryByPeriod = PXResult<ARHistoryByPeriod, RevaluedARHistory, Customer, PX.Objects.GL.Branch, FinPeriod, ARHistoryLastRevaluation>.op_Implicit(pxResult);
      ARHistoryLastRevaluation historyLastRevaluation = PXResult<ARHistoryByPeriod, RevaluedARHistory, Customer, PX.Objects.GL.Branch, FinPeriod, ARHistoryLastRevaluation>.op_Implicit(pxResult);
      RevaluedARHistory copy = PXCache<RevaluedARHistory>.CreateCopy(PXResult<ARHistoryByPeriod, RevaluedARHistory, Customer, PX.Objects.GL.Branch, FinPeriod, ARHistoryLastRevaluation>.op_Implicit(pxResult));
      Customer customer = PXResult<ARHistoryByPeriod, RevaluedARHistory, Customer, PX.Objects.GL.Branch, FinPeriod, ARHistoryLastRevaluation>.op_Implicit(pxResult);
      RevaluedARHistory revaluedArHistory1;
      if ((revaluedArHistory1 = ((PXSelectBase<RevaluedARHistory>) revalueArAccounts.ARAccountList).Locate(copy)) != null)
      {
        yield return (object) revaluedArHistory1;
      }
      else
      {
        copy.CustomerClassID = customer.CustomerClassID;
        copy.CuryRateTypeID = ((PXSelectBase<CMSetup>) revalueArAccounts.cmsetup).Current.ARRateTypeReval ?? customer.CuryRateTypeID;
        Decimal? nullable1;
        if (string.IsNullOrEmpty(copy.CuryRateTypeID))
        {
          ((PXSelectBase) revalueArAccounts.ARAccountList).Cache.RaiseExceptionHandling<RevaluedGLHistory.curyRateTypeID>((object) copy, (object) null, (Exception) new PXSetPropertyException("Currency Rate Type is not defined."));
        }
        else
        {
          PXResultset<CurrencyRate> pxResultset;
          if (((PXSelectBase<RevalueFilter>) revalueArAccounts.Filter).Current.OrganizationBaseCuryID != null)
            pxResultset = PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Current<RevalueFilter.curyID>>, And<CurrencyRate.toCuryID, Equal<Current<RevalueFilter.organizationBaseCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<Customer.curyRateTypeID>>, And<CurrencyRate.curyEffDate, LessEqual<Current<RevalueFilter.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select((PXGraph) revalueArAccounts, new object[1]
            {
              (object) copy.CuryRateTypeID
            });
          else
            pxResultset = PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Current<RevalueFilter.curyID>>, And<CurrencyRate.toCuryID, Equal<Current<Company.baseCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<Customer.curyRateTypeID>>, And<CurrencyRate.curyEffDate, LessEqual<Current<RevalueFilter.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select((PXGraph) revalueArAccounts, new object[1]
            {
              (object) copy.CuryRateTypeID
            });
          CurrencyRate currencyRate = PXResultset<CurrencyRate>.op_Implicit(pxResultset);
          if (currencyRate == null || currencyRate.CuryMultDiv == null)
          {
            copy.CuryMultDiv = "M";
            copy.CuryRate = new Decimal?(1M);
            copy.RateReciprocal = new Decimal?(1M);
            copy.CuryEffDate = ((PXSelectBase<RevalueFilter>) revalueArAccounts.Filter).Current.CuryEffDate;
            ((PXSelectBase) revalueArAccounts.ARAccountList).Cache.RaiseExceptionHandling<RevaluedARHistory.curyRate>((object) copy, (object) 1M, (Exception) new PXSetPropertyException("Currency Rate is not defined.", (PXErrorLevel) 3));
          }
          else
          {
            copy.CuryRate = currencyRate.CuryRate;
            copy.RateReciprocal = currencyRate.RateReciprocal;
            copy.CuryEffDate = currencyRate.CuryEffDate;
            copy.CuryMultDiv = currencyRate.CuryMultDiv;
          }
          CurrencyInfo currencyInfo = new CurrencyInfo();
          currencyInfo.BaseCuryID = ((PXSelectBase<RevalueFilter>) revalueArAccounts.Filter).Current.OrganizationBaseCuryID == null ? ((PXSelectBase<Company>) revalueArAccounts.company).Current.BaseCuryID : ((PXSelectBase<RevalueFilter>) revalueArAccounts.Filter).Current.OrganizationBaseCuryID;
          currencyInfo.CuryID = copy.CuryID;
          currencyInfo.CuryMultDiv = copy.CuryMultDiv;
          currencyInfo.CuryRate = copy.CuryRate;
          Decimal? nullable2;
          if (((PXSelectBase<CMSetup>) revalueArAccounts.cmsetup).Current.RevalueARPrepaymentsOption.GetValueOrDefault())
          {
            RevaluedARHistory revaluedArHistory2 = copy;
            nullable2 = revaluedArHistory2.CuryFinYtdBalance;
            nullable1 = copy.CuryFinYtdDeposits;
            revaluedArHistory2.CuryFinYtdBalance = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
            RevaluedARHistory revaluedArHistory3 = copy;
            nullable1 = revaluedArHistory3.FinYtdBalance;
            nullable2 = copy.FinYtdDeposits;
            revaluedArHistory3.FinYtdBalance = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          }
          PXCache cache = ((PXSelectBase) revalueArAccounts.currencyinfo).Cache;
          CurrencyInfo info = currencyInfo;
          nullable2 = copy.CuryFinYtdBalance;
          Decimal curyval = nullable2.Value;
          Decimal num;
          ref Decimal local = ref num;
          PXCurrencyAttribute.CuryConvBase(cache, info, curyval, out local);
          copy.FinYtdRevalued = new Decimal?(num);
          copy.FinPrevRevalued = string.Equals(arHistoryByPeriod.FinPeriodID, arHistoryByPeriod.LastActivityPeriod) ? copy.FinPtdRevalued : new Decimal?(0M);
          RevaluedARHistory revaluedArHistory4 = copy;
          Decimal? finYtdRevalued = copy.FinYtdRevalued;
          Decimal? nullable3 = copy.FinPrevRevalued;
          nullable2 = finYtdRevalued.HasValue & nullable3.HasValue ? new Decimal?(finYtdRevalued.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
          nullable1 = copy.FinYtdBalance;
          Decimal? nullable4;
          if (!(nullable2.HasValue & nullable1.HasValue))
          {
            nullable3 = new Decimal?();
            nullable4 = nullable3;
          }
          else
            nullable4 = new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault());
          revaluedArHistory4.FinPtdRevalued = nullable4;
          copy.LastRevaluedFinPeriodID = historyLastRevaluation?.LastActivityPeriod;
        }
        nullable1 = copy.FinYtdBalance;
        Decimal num1 = 0M;
        if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
        {
          nullable1 = copy.CuryFinYtdBalance;
          Decimal num2 = 0M;
          if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
          {
            nullable1 = copy.FinPtdRevalued;
            Decimal num3 = 0M;
            if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue)
              continue;
          }
        }
        ((PXSelectBase) revalueArAccounts.ARAccountList).Cache.SetStatus((object) copy, (PXEntryStatus) 5);
        yield return (object) copy;
      }
    }
  }

  public void Revalue(RevalueFilter filter, List<RevaluedARHistory> list)
  {
    JournalEntry instance1 = PXGraph.CreateInstance<JournalEntry>();
    PostGraph instance2 = PXGraph.CreateInstance<PostGraph>();
    PXCache cach1 = ((PXGraph) instance1).Caches[typeof (CuryARHist)];
    PXCache cach2 = ((PXGraph) instance1).Caches[typeof (ARHist)];
    ((PXGraph) instance1).Views.Caches.Add(typeof (CuryARHist));
    ((PXGraph) instance1).Views.Caches.Add(typeof (ARHist));
    string refNbrNumberingId = ((PXSelectBase<CMSetup>) instance1.CMSetup).Current.ExtRefNbrNumberingID;
    if (!string.IsNullOrEmpty(refNbrNumberingId))
      new RevaluationRefNbrHelper(refNbrNumberingId).Subscribe(instance1);
    DocumentList<Batch> documentList = new DocumentList<Batch>((PXGraph) instance1);
    Currency currency = PXResultset<Currency>.op_Implicit(PXSelectBase<Currency, PXSelect<Currency, Where<Currency.curyID, Equal<Required<Currency.curyID>>>>.Config>.Select((PXGraph) instance1, new object[1]
    {
      (object) filter.CuryID
    }));
    bool flag = false;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (RevaluedARHistory revaluedArHistory in list)
      {
        PXProcessing<RevaluedARHistory>.SetCurrentItem((object) revaluedArHistory);
        Decimal? nullable1 = revaluedArHistory.FinPtdRevalued;
        Decimal num1 = 0M;
        if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
        {
          PXProcessing<RevaluedARHistory>.SetProcessed();
        }
        else
        {
          PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(filter.OrgBAccountID);
          int? nullable2 = filter.OrgBAccountID;
          string finPeriodID = !nullable2.HasValue || organizationByBaccountId != null && organizationByBaccountId.IsGroup ? this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(revaluedArHistory.BranchID), filter.FinPeriodID).Result.FinPeriodID : filter.FinPeriodID;
          if (!this.CheckFinPeriod(finPeriodID, revaluedArHistory.BranchID).IsSuccess)
          {
            flag = true;
          }
          else
          {
            if (((PXSelectBase) instance1.GLTranModuleBatNbr).Cache.IsInsertedUpdatedDeleted)
            {
              ((PXAction) instance1.Save).Press();
              if (documentList.Find((object) ((PXSelectBase<Batch>) instance1.BatchModule).Current) == null)
                documentList.Add(((PXSelectBase<Batch>) instance1.BatchModule).Current);
            }
            Batch batch = documentList.Find<Batch.branchID>((object) revaluedArHistory.BranchID) ?? new Batch();
            if (batch.BatchNbr == null)
            {
              ((PXGraph) instance1).Clear();
              CurrencyInfo currencyInfo1 = new CurrencyInfo();
              currencyInfo1.CuryID = revaluedArHistory.CuryID;
              currencyInfo1.CuryEffDate = revaluedArHistory.CuryEffDate;
              currencyInfo1.BaseCalc = new bool?(false);
              CurrencyInfo currencyInfo2 = ((PXSelectBase<CurrencyInfo>) instance1.currencyinfo).Insert(currencyInfo1) ?? currencyInfo1;
              ((PXSelectBase<Batch>) instance1.BatchModule).Insert(new Batch()
              {
                BranchID = revaluedArHistory.BranchID,
                Module = "CM",
                Status = "U",
                AutoReverse = new bool?(true),
                Released = new bool?(true),
                Hold = new bool?(false),
                DateEntered = filter.CuryEffDate,
                FinPeriodID = finPeriodID,
                CuryID = revaluedArHistory.CuryID,
                CuryInfoID = currencyInfo2.CuryInfoID,
                DebitTotal = new Decimal?(0M),
                CreditTotal = new Decimal?(0M),
                Description = filter.Description
              });
              CurrencyInfo currencyInfo3 = PXResultset<CurrencyInfo>.op_Implicit(((PXSelectBase<CurrencyInfo>) instance1.currencyinfo).Select(Array.Empty<object>()));
              if (currencyInfo3 != null)
              {
                currencyInfo3.CuryID = revaluedArHistory.CuryID;
                currencyInfo3.CuryEffDate = revaluedArHistory.CuryEffDate;
                currencyInfo3.CuryRateTypeID = revaluedArHistory.CuryRateTypeID;
                currencyInfo3.CuryRate = revaluedArHistory.CuryRate;
                currencyInfo3.RecipRate = revaluedArHistory.RateReciprocal;
                currencyInfo3.CuryMultDiv = revaluedArHistory.CuryMultDiv;
                ((PXSelectBase<CurrencyInfo>) instance1.currencyinfo).Update(currencyInfo3);
              }
            }
            else
            {
              if (!((PXSelectBase) instance1.BatchModule).Cache.ObjectsEqual((object) ((PXSelectBase<Batch>) instance1.BatchModule).Current, (object) batch))
                ((PXGraph) instance1).Clear();
              ((PXSelectBase<Batch>) instance1.BatchModule).Current = PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) instance1.BatchModule).Search<Batch.batchNbr>((object) batch.BatchNbr, new object[1]
              {
                (object) batch.Module
              }));
            }
            PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
            glTran1.SummPost = new bool?(false);
            PX.Objects.GL.GLTran glTran2 = glTran1;
            nullable2 = currency.ARProvAcctID;
            int? nullable3 = nullable2 ?? revaluedArHistory.AccountID;
            glTran2.AccountID = nullable3;
            PX.Objects.GL.GLTran glTran3 = glTran1;
            nullable2 = currency.ARProvSubID;
            int? nullable4 = nullable2 ?? revaluedArHistory.SubID;
            glTran3.SubID = nullable4;
            glTran1.CuryDebitAmt = new Decimal?(0M);
            glTran1.CuryCreditAmt = new Decimal?(0M);
            PX.Objects.GL.GLTran glTran4 = glTran1;
            nullable1 = revaluedArHistory.FinPtdRevalued;
            Decimal num2 = 0M;
            Decimal? nullable5 = nullable1.GetValueOrDefault() < num2 & nullable1.HasValue ? new Decimal?(0M) : revaluedArHistory.FinPtdRevalued;
            glTran4.DebitAmt = nullable5;
            PX.Objects.GL.GLTran glTran5 = glTran1;
            nullable1 = revaluedArHistory.FinPtdRevalued;
            Decimal num3 = 0M;
            Decimal? nullable6;
            Decimal? nullable7;
            if (!(nullable1.GetValueOrDefault() < num3 & nullable1.HasValue))
            {
              nullable7 = new Decimal?(0M);
            }
            else
            {
              Decimal num4 = -1M;
              nullable1 = revaluedArHistory.FinPtdRevalued;
              if (!nullable1.HasValue)
              {
                nullable6 = new Decimal?();
                nullable7 = nullable6;
              }
              else
                nullable7 = new Decimal?(num4 * nullable1.GetValueOrDefault());
            }
            glTran5.CreditAmt = nullable7;
            glTran1.TranType = "REV";
            glTran1.TranClass = "A";
            glTran1.RefNbr = string.Empty;
            glTran1.TranDesc = filter.Description;
            glTran1.FinPeriodID = finPeriodID;
            glTran1.TranDate = filter.CuryEffDate;
            glTran1.CuryInfoID = new long?();
            glTran1.Released = new bool?(true);
            glTran1.ReferenceID = revaluedArHistory.CustomerID;
            ((PXSelectBase<PX.Objects.GL.GLTran>) instance1.GLTranModuleBatNbr).Insert(glTran1);
            CustomerClass customerClass1 = PXResultset<CustomerClass>.op_Implicit(PXSelectBase<CustomerClass, PXSelectReadonly<CustomerClass, Where<CustomerClass.customerClassID, Equal<Required<CustomerClass.customerClassID>>>>.Config>.Select((PXGraph) instance1, new object[1]
            {
              (object) revaluedArHistory.CustomerClassID
            })) ?? new CustomerClass();
            nullable2 = customerClass1.UnrealizedGainAcctID;
            if (!nullable2.HasValue)
            {
              CustomerClass customerClass2 = customerClass1;
              nullable2 = new int?();
              int? nullable8 = nullable2;
              customerClass2.UnrealizedGainSubID = nullable8;
            }
            nullable2 = customerClass1.UnrealizedLossAcctID;
            if (!nullable2.HasValue)
            {
              CustomerClass customerClass3 = customerClass1;
              nullable2 = new int?();
              int? nullable9 = nullable2;
              customerClass3.UnrealizedLossSubID = nullable9;
            }
            PX.Objects.GL.GLTran glTran6 = new PX.Objects.GL.GLTran();
            glTran6.SummPost = new bool?(true);
            glTran6.ZeroPost = new bool?(false);
            glTran6.CuryDebitAmt = new Decimal?(0M);
            glTran6.CuryCreditAmt = new Decimal?(0M);
            nullable1 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.DebitTotal;
            nullable6 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.CreditTotal;
            if (nullable1.GetValueOrDefault() > nullable6.GetValueOrDefault() & nullable1.HasValue & nullable6.HasValue)
            {
              PX.Objects.GL.GLTran glTran7 = glTran6;
              nullable2 = customerClass1.UnrealizedGainAcctID;
              int? nullable10 = nullable2 ?? currency.UnrealizedGainAcctID;
              glTran7.AccountID = nullable10;
              PX.Objects.GL.GLTran glTran8 = glTran6;
              nullable2 = customerClass1.UnrealizedGainSubID;
              int? nullable11 = nullable2 ?? GainLossSubAccountMaskAttribute.GetSubID<Currency.unrealizedGainSubID>((PXGraph) instance1, revaluedArHistory.BranchID, currency);
              glTran8.SubID = nullable11;
              glTran6.DebitAmt = new Decimal?(0M);
              PX.Objects.GL.GLTran glTran9 = glTran6;
              nullable6 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.DebitTotal;
              nullable1 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.CreditTotal;
              Decimal? nullable12 = nullable6.HasValue & nullable1.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
              glTran9.CreditAmt = nullable12;
            }
            else
            {
              PX.Objects.GL.GLTran glTran10 = glTran6;
              nullable2 = customerClass1.UnrealizedLossAcctID;
              int? nullable13 = nullable2 ?? currency.UnrealizedLossAcctID;
              glTran10.AccountID = nullable13;
              PX.Objects.GL.GLTran glTran11 = glTran6;
              nullable2 = customerClass1.UnrealizedLossSubID;
              int? nullable14 = nullable2 ?? GainLossSubAccountMaskAttribute.GetSubID<Currency.unrealizedLossSubID>((PXGraph) instance1, revaluedArHistory.BranchID, currency);
              glTran11.SubID = nullable14;
              PX.Objects.GL.GLTran glTran12 = glTran6;
              nullable1 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.CreditTotal;
              nullable6 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.DebitTotal;
              Decimal? nullable15 = nullable1.HasValue & nullable6.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
              glTran12.DebitAmt = nullable15;
              glTran6.CreditAmt = new Decimal?(0M);
            }
            glTran6.TranType = "REV";
            glTran6.TranClass = "G";
            glTran6.RefNbr = string.Empty;
            glTran6.TranDesc = filter.Description;
            glTran6.Released = new bool?(true);
            PX.Objects.GL.GLTran glTran13 = glTran6;
            nullable2 = new int?();
            int? nullable16 = nullable2;
            glTran13.ReferenceID = nullable16;
            ((PXSelectBase<PX.Objects.GL.GLTran>) instance1.GLTranModuleBatNbr).Insert(glTran6);
            CuryARHist curyArHist1 = new CuryARHist();
            curyArHist1.BranchID = revaluedArHistory.BranchID;
            curyArHist1.AccountID = revaluedArHistory.AccountID;
            curyArHist1.SubID = revaluedArHistory.SubID;
            curyArHist1.FinPeriodID = filter.FinPeriodID;
            curyArHist1.CustomerID = revaluedArHistory.CustomerID;
            curyArHist1.CuryID = revaluedArHistory.CuryID;
            CuryARHist curyArHist2 = (CuryARHist) cach1.Insert((object) curyArHist1);
            nullable6 = curyArHist2.FinPtdRevalued;
            nullable1 = revaluedArHistory.FinPtdRevalued;
            curyArHist2.FinPtdRevalued = nullable6.HasValue & nullable1.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
            ARHist arHist1 = new ARHist();
            arHist1.BranchID = revaluedArHistory.BranchID;
            arHist1.AccountID = revaluedArHistory.AccountID;
            arHist1.SubID = revaluedArHistory.SubID;
            arHist1.FinPeriodID = filter.FinPeriodID;
            arHist1.CustomerID = revaluedArHistory.CustomerID;
            ARHist arHist2 = (ARHist) cach2.Insert((object) arHist1);
            nullable1 = arHist2.FinPtdRevalued;
            nullable6 = revaluedArHistory.FinPtdRevalued;
            arHist2.FinPtdRevalued = nullable1.HasValue & nullable6.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
            PXProcessing<RevaluedARHistory>.SetProcessed();
          }
        }
      }
      if (((PXSelectBase) instance1.GLTranModuleBatNbr).Cache.IsInsertedUpdatedDeleted)
      {
        ((PXAction) instance1.Save).Press();
        if (documentList.Find((object) ((PXSelectBase<Batch>) instance1.BatchModule).Current) == null)
          documentList.Add(((PXSelectBase<Batch>) instance1.BatchModule).Current);
      }
      transactionScope.Complete();
    }
    PXProcessing<RevaluedARHistory>.SetCurrentItem((object) null);
    CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) instance1, Array.Empty<object>()));
    bool? nullable;
    for (int index = 0; index < documentList.Count; ++index)
    {
      nullable = cmSetup.AutoPostOption;
      if (nullable.GetValueOrDefault())
      {
        ((PXGraph) instance2).Clear();
        instance2.PostBatchProc(documentList[index]);
      }
    }
    if (flag)
      throw new PXException("At least one item has not been processed.");
    if (documentList.Count > 0)
    {
      ((PXSelectBase<Batch>) instance1.BatchModule).Current = documentList[documentList.Count - 1];
      throw new PXRedirectRequiredException((PXGraph) instance1, "Preview");
    }
    Decimal num = 0M;
    foreach (RevaluedARHistory revaluedArHistory in ((PXSelectBase) this.ARAccountList).Cache.Updated)
    {
      nullable = revaluedArHistory.Selected;
      if (nullable.Value)
        num += revaluedArHistory.FinPtdRevalued.Value;
    }
    if (num == 0M)
      throw new PXOperationCompletedWithWarningException("No revaluation entry was made since Original Balance equals the Revalued Balance");
  }
}
