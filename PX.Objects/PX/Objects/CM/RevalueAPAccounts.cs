// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.RevalueAPAccounts
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AP.Overrides.APDocumentRelease;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CM;

[TableAndChartDashboardType]
public class RevalueAPAccounts : RevalueAcountsBase<RevaluedAPHistory>
{
  public PXCancel<RevalueFilter> Cancel;
  public PXFilter<RevalueFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<RevaluedAPHistory, RevalueFilter, Where<boolTrue, Equal<boolTrue>>, OrderBy<Asc<RevaluedAPHistory.accountID, Asc<RevaluedAPHistory.subID, Asc<RevaluedAPHistory.vendorID>>>>> APAccountList;
  public PXSelect<CurrencyInfo> currencyinfo;
  public PXSetup<APSetup> apsetup;
  public PXSetup<CMSetup> cmsetup;
  public PXSetup<Company> company;

  public RevalueAPAccounts()
  {
    CMSetup current1 = ((PXSelectBase<CMSetup>) this.cmsetup).Current;
    APSetup current2 = ((PXSelectBase<APSetup>) this.apsetup).Current;
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      throw new Exception("Multi-Currency is not activated");
    ((PXProcessing<RevaluedAPHistory>) this.APAccountList).SetProcessCaption("Revalue");
    ((PXProcessing<RevaluedAPHistory>) this.APAccountList).SetProcessAllVisible(false);
    PXUIFieldAttribute.SetEnabled<RevaluedAPHistory.finPtdRevalued>(((PXSelectBase) this.APAccountList).Cache, (object) null, true);
  }

  protected virtual void RevalueFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RevalueAPAccounts.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new RevalueAPAccounts.\u003C\u003Ec__DisplayClass8_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass80.filter = (RevalueFilter) e.Row;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass80.filter == null)
      return;
    // ISSUE: method pointer
    ((PXProcessingBase<RevaluedAPHistory>) this.APAccountList).SetProcessDelegate(new PXProcessingBase<RevaluedAPHistory>.ProcessListDelegate((object) cDisplayClass80, __methodptr(\u003CRevalueFilter_RowSelected\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    this.VerifyCurrencyEffectiveDate(sender, cDisplayClass80.filter);
  }

  protected virtual void RevalueFilter_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
  }

  protected virtual void RevalueFilter_OrgBAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RevalueFilter row = (RevalueFilter) e.Row;
    if (row != null)
      row.CuryID = (string) null;
    ((PXSelectBase) this.APAccountList).Cache.Clear();
    sender.SetDefaultExt<RevalueFilter.curyEffDate>(e.Row);
  }

  protected virtual void RevalueFilter_FinPeriodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.APAccountList).Cache.Clear();
    sender.SetDefaultExt<RevalueFilter.curyEffDate>(e.Row);
  }

  protected virtual void RevalueFilter_CuryEffDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.APAccountList).Cache.Clear();
  }

  protected virtual void RevalueFilter_CuryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.APAccountList).Cache.Clear();
  }

  protected virtual void RevalueFilter_TotalRevalued_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    Decimal num = 0M;
    foreach (RevaluedAPHistory revaluedApHistory in ((PXSelectBase) this.APAccountList).Cache.Updated)
    {
      if (revaluedApHistory.Selected.Value)
        num += revaluedApHistory.FinPtdRevalued.Value;
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

  public virtual IEnumerable apaccountlist()
  {
    RevalueAPAccounts revalueApAccounts = this;
    foreach (PXResult<APHistoryByPeriod, RevaluedAPHistory, Vendor, PX.Objects.GL.Branch, FinPeriod, APHistoryLastRevaluation> pxResult in PXSelectBase<APHistoryByPeriod, PXSelectJoin<APHistoryByPeriod, LeftJoin<RevaluedAPHistory, On<RevaluedAPHistory.vendorID, Equal<APHistoryByPeriod.vendorID>, And<RevaluedAPHistory.branchID, Equal<APHistoryByPeriod.branchID>, And<RevaluedAPHistory.accountID, Equal<APHistoryByPeriod.accountID>, And<RevaluedAPHistory.subID, Equal<APHistoryByPeriod.subID>, And<RevaluedAPHistory.curyID, Equal<APHistoryByPeriod.curyID>, And<RevaluedAPHistory.finPeriodID, Equal<APHistoryByPeriod.lastActivityPeriod>>>>>>>, InnerJoin<Vendor, On<Vendor.bAccountID, Equal<APHistoryByPeriod.vendorID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<APHistoryByPeriod.branchID>, And<Where<PX.Objects.GL.Branch.branchID, InsideBranchesOf<Current2<RevalueFilter.orgBAccountID>>, Or<Current2<RevalueFilter.orgBAccountID>, IsNull, And<Not<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>>>>>>, InnerJoin<FinPeriod, On<APHistoryByPeriod.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, LeftJoin<APHistoryLastRevaluation, On<APHistoryByPeriod.vendorID, Equal<APHistoryLastRevaluation.vendorID>, And<APHistoryByPeriod.branchID, Equal<APHistoryLastRevaluation.branchID>, And<APHistoryByPeriod.accountID, Equal<APHistoryLastRevaluation.accountID>, And<APHistoryByPeriod.subID, Equal<APHistoryLastRevaluation.subID>, And<APHistoryByPeriod.curyID, Equal<APHistoryLastRevaluation.curyID>>>>>>>>>>>, Where<APHistoryByPeriod.curyID, Equal<Current<RevalueFilter.curyID>>, And2<Where2<Where<FinPeriod.masterFinPeriodID, Equal<Current<RevalueFilter.finPeriodID>>, And<Current2<RevalueFilter.orgBAccountID>, IsNull>>, Or<Where<FinPeriod.finPeriodID, Equal<Current<RevalueFilter.finPeriodID>>, And<Current2<RevalueFilter.orgBAccountID>, IsNotNull>>>>, And<Where<RevaluedAPHistory.curyFinYtdBalance, NotEqual<decimal0>, Or<RevaluedAPHistory.finYtdBalance, NotEqual<decimal0>, Or<RevaluedAPHistory.finPtdRevalued, NotEqual<decimal0>, Or<RevaluedAPHistory.curyFinYtdDeposits, NotEqual<decimal0>>>>>>>>>.Config>.Select((PXGraph) revalueApAccounts, Array.Empty<object>()))
    {
      APHistoryByPeriod apHistoryByPeriod = PXResult<APHistoryByPeriod, RevaluedAPHistory, Vendor, PX.Objects.GL.Branch, FinPeriod, APHistoryLastRevaluation>.op_Implicit(pxResult);
      APHistoryLastRevaluation historyLastRevaluation = PXResult<APHistoryByPeriod, RevaluedAPHistory, Vendor, PX.Objects.GL.Branch, FinPeriod, APHistoryLastRevaluation>.op_Implicit(pxResult);
      RevaluedAPHistory copy = PXCache<RevaluedAPHistory>.CreateCopy(PXResult<APHistoryByPeriod, RevaluedAPHistory, Vendor, PX.Objects.GL.Branch, FinPeriod, APHistoryLastRevaluation>.op_Implicit(pxResult));
      Vendor vendor = PXResult<APHistoryByPeriod, RevaluedAPHistory, Vendor, PX.Objects.GL.Branch, FinPeriod, APHistoryLastRevaluation>.op_Implicit(pxResult);
      RevaluedAPHistory revaluedApHistory1;
      if ((revaluedApHistory1 = ((PXSelectBase<RevaluedAPHistory>) revalueApAccounts.APAccountList).Locate(copy)) != null)
      {
        yield return (object) revaluedApHistory1;
      }
      else
      {
        copy.VendorClassID = vendor.VendorClassID;
        copy.CuryRateTypeID = ((PXSelectBase<CMSetup>) revalueApAccounts.cmsetup).Current.APRateTypeReval ?? PXResult<APHistoryByPeriod, RevaluedAPHistory, Vendor, PX.Objects.GL.Branch, FinPeriod, APHistoryLastRevaluation>.op_Implicit(pxResult).CuryRateTypeID;
        Decimal? nullable1;
        if (string.IsNullOrEmpty(copy.CuryRateTypeID))
        {
          ((PXSelectBase) revalueApAccounts.APAccountList).Cache.RaiseExceptionHandling<RevaluedGLHistory.curyRateTypeID>((object) copy, (object) null, (Exception) new PXSetPropertyException("Currency Rate Type is not defined."));
        }
        else
        {
          PXResultset<CurrencyRate> pxResultset;
          if (((PXSelectBase<RevalueFilter>) revalueApAccounts.Filter).Current.OrganizationBaseCuryID != null)
            pxResultset = PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Current<RevalueFilter.curyID>>, And<CurrencyRate.toCuryID, Equal<Current<RevalueFilter.organizationBaseCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<Vendor.curyRateTypeID>>, And<CurrencyRate.curyEffDate, LessEqual<Current<RevalueFilter.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select((PXGraph) revalueApAccounts, new object[1]
            {
              (object) copy.CuryRateTypeID
            });
          else
            pxResultset = PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Current<RevalueFilter.curyID>>, And<CurrencyRate.toCuryID, Equal<Current<Company.baseCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<Vendor.curyRateTypeID>>, And<CurrencyRate.curyEffDate, LessEqual<Current<RevalueFilter.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select((PXGraph) revalueApAccounts, new object[1]
            {
              (object) copy.CuryRateTypeID
            });
          CurrencyRate currencyRate = PXResultset<CurrencyRate>.op_Implicit(pxResultset);
          if (currencyRate == null || currencyRate.CuryMultDiv == null)
          {
            copy.CuryMultDiv = "M";
            copy.CuryRate = new Decimal?(1M);
            copy.RateReciprocal = new Decimal?(1M);
            copy.CuryEffDate = ((PXSelectBase<RevalueFilter>) revalueApAccounts.Filter).Current.CuryEffDate;
            ((PXSelectBase) revalueApAccounts.APAccountList).Cache.RaiseExceptionHandling<RevaluedAPHistory.curyRate>((object) copy, (object) 1M, (Exception) new PXSetPropertyException("Currency Rate is not defined.", (PXErrorLevel) 3));
          }
          else
          {
            copy.CuryRate = currencyRate.CuryRate;
            copy.RateReciprocal = currencyRate.RateReciprocal;
            copy.CuryEffDate = currencyRate.CuryEffDate;
            copy.CuryMultDiv = currencyRate.CuryMultDiv;
          }
          CurrencyInfo currencyInfo = new CurrencyInfo();
          currencyInfo.BaseCuryID = ((PXSelectBase<RevalueFilter>) revalueApAccounts.Filter).Current.OrganizationBaseCuryID == null ? ((PXSelectBase<Company>) revalueApAccounts.company).Current.BaseCuryID : ((PXSelectBase<RevalueFilter>) revalueApAccounts.Filter).Current.OrganizationBaseCuryID;
          currencyInfo.CuryID = copy.CuryID;
          currencyInfo.CuryMultDiv = copy.CuryMultDiv;
          currencyInfo.CuryRate = copy.CuryRate;
          currencyInfo.RecipRate = copy.RateReciprocal;
          Decimal? nullable2;
          if (((PXSelectBase<CMSetup>) revalueApAccounts.cmsetup).Current.RevalueAPPrepaymentsOption.GetValueOrDefault())
          {
            RevaluedAPHistory revaluedApHistory2 = copy;
            nullable2 = revaluedApHistory2.CuryFinYtdBalance;
            nullable1 = copy.CuryFinYtdDeposits;
            revaluedApHistory2.CuryFinYtdBalance = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
            RevaluedAPHistory revaluedApHistory3 = copy;
            nullable1 = revaluedApHistory3.FinYtdBalance;
            nullable2 = copy.FinYtdDeposits;
            revaluedApHistory3.FinYtdBalance = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          }
          PXCache cache = ((PXSelectBase) revalueApAccounts.currencyinfo).Cache;
          CurrencyInfo info = currencyInfo;
          nullable2 = copy.CuryFinYtdBalance;
          Decimal curyval = nullable2.Value;
          Decimal num;
          ref Decimal local = ref num;
          PXCurrencyAttribute.CuryConvBase(cache, info, curyval, out local);
          copy.FinYtdRevalued = new Decimal?(num);
          copy.FinPrevRevalued = string.Equals(apHistoryByPeriod.FinPeriodID, apHistoryByPeriod.LastActivityPeriod) ? copy.FinPtdRevalued : new Decimal?(0M);
          RevaluedAPHistory revaluedApHistory4 = copy;
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
          revaluedApHistory4.FinPtdRevalued = nullable4;
          copy.LastRevaluedFinPeriodID = historyLastRevaluation?.LastActivityPeriod;
        }
        nullable1 = copy.CuryFinYtdBalance;
        Decimal num1 = 0M;
        if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
        {
          nullable1 = copy.FinYtdBalance;
          Decimal num2 = 0M;
          if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
          {
            nullable1 = copy.FinPtdRevalued;
            Decimal num3 = 0M;
            if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue)
              continue;
          }
        }
        ((PXSelectBase) revalueApAccounts.APAccountList).Cache.SetStatus((object) copy, (PXEntryStatus) 5);
        yield return (object) copy;
      }
    }
  }

  public void Revalue(RevalueFilter filter, List<RevaluedAPHistory> list)
  {
    JournalEntry instance1 = PXGraph.CreateInstance<JournalEntry>();
    PostGraph instance2 = PXGraph.CreateInstance<PostGraph>();
    PXCache cach1 = ((PXGraph) instance1).Caches[typeof (CuryAPHist)];
    PXCache cach2 = ((PXGraph) instance1).Caches[typeof (APHist)];
    ((PXGraph) instance1).Views.Caches.Add(typeof (CuryAPHist));
    ((PXGraph) instance1).Views.Caches.Add(typeof (APHist));
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
      foreach (RevaluedAPHistory revaluedApHistory in list)
      {
        PXProcessing<RevaluedAPHistory>.SetCurrentItem((object) revaluedApHistory);
        Decimal? nullable1 = revaluedApHistory.FinPtdRevalued;
        Decimal num1 = 0M;
        if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
        {
          PXProcessing<RevaluedAPHistory>.SetProcessed();
        }
        else
        {
          PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(filter.OrgBAccountID);
          int? nullable2 = filter.OrgBAccountID;
          string finPeriodID = !nullable2.HasValue || organizationByBaccountId != null && organizationByBaccountId.IsGroup ? this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(revaluedApHistory.BranchID), filter.FinPeriodID).Result.FinPeriodID : filter.FinPeriodID;
          if (!this.CheckFinPeriod(finPeriodID, revaluedApHistory.BranchID).IsSuccess)
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
            Batch batch = documentList.Find<Batch.branchID>((object) revaluedApHistory.BranchID) ?? new Batch();
            if (batch.BatchNbr == null)
            {
              ((PXGraph) instance1).Clear();
              CurrencyInfo currencyInfo1 = new CurrencyInfo();
              currencyInfo1.CuryID = revaluedApHistory.CuryID;
              currencyInfo1.CuryEffDate = revaluedApHistory.CuryEffDate;
              currencyInfo1.BaseCalc = new bool?(false);
              CurrencyInfo currencyInfo2 = ((PXSelectBase<CurrencyInfo>) instance1.currencyinfo).Insert(currencyInfo1) ?? currencyInfo1;
              ((PXSelectBase<Batch>) instance1.BatchModule).Insert(new Batch()
              {
                BranchID = revaluedApHistory.BranchID,
                Module = "CM",
                Status = "U",
                AutoReverse = new bool?(true),
                Released = new bool?(true),
                Hold = new bool?(false),
                DateEntered = filter.CuryEffDate,
                FinPeriodID = finPeriodID,
                CuryID = revaluedApHistory.CuryID,
                CuryInfoID = currencyInfo2.CuryInfoID,
                DebitTotal = new Decimal?(0M),
                CreditTotal = new Decimal?(0M),
                Description = filter.Description
              });
              CurrencyInfo currencyInfo3 = PXResultset<CurrencyInfo>.op_Implicit(((PXSelectBase<CurrencyInfo>) instance1.currencyinfo).Select(Array.Empty<object>()));
              if (currencyInfo3 != null)
              {
                currencyInfo3.CuryID = revaluedApHistory.CuryID;
                currencyInfo3.CuryEffDate = revaluedApHistory.CuryEffDate;
                currencyInfo3.CuryRateTypeID = revaluedApHistory.CuryRateTypeID;
                currencyInfo3.CuryRate = revaluedApHistory.CuryRate;
                currencyInfo3.RecipRate = revaluedApHistory.RateReciprocal;
                currencyInfo3.CuryMultDiv = revaluedApHistory.CuryMultDiv;
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
            nullable2 = currency.APProvAcctID;
            int? nullable3 = nullable2 ?? revaluedApHistory.AccountID;
            glTran2.AccountID = nullable3;
            PX.Objects.GL.GLTran glTran3 = glTran1;
            nullable2 = currency.APProvSubID;
            int? nullable4 = nullable2 ?? revaluedApHistory.SubID;
            glTran3.SubID = nullable4;
            glTran1.CuryDebitAmt = new Decimal?(0M);
            glTran1.CuryCreditAmt = new Decimal?(0M);
            PX.Objects.GL.GLTran glTran4 = glTran1;
            nullable1 = revaluedApHistory.FinPtdRevalued;
            Decimal num2 = 0M;
            Decimal? nullable5;
            Decimal? nullable6;
            if (!(nullable1.GetValueOrDefault() < num2 & nullable1.HasValue))
            {
              nullable6 = new Decimal?(0M);
            }
            else
            {
              Decimal num3 = -1M;
              nullable1 = revaluedApHistory.FinPtdRevalued;
              if (!nullable1.HasValue)
              {
                nullable5 = new Decimal?();
                nullable6 = nullable5;
              }
              else
                nullable6 = new Decimal?(num3 * nullable1.GetValueOrDefault());
            }
            glTran4.DebitAmt = nullable6;
            PX.Objects.GL.GLTran glTran5 = glTran1;
            nullable1 = revaluedApHistory.FinPtdRevalued;
            Decimal num4 = 0M;
            Decimal? nullable7 = nullable1.GetValueOrDefault() < num4 & nullable1.HasValue ? new Decimal?(0M) : revaluedApHistory.FinPtdRevalued;
            glTran5.CreditAmt = nullable7;
            glTran1.TranType = "REV";
            glTran1.TranClass = "L";
            glTran1.RefNbr = string.Empty;
            glTran1.TranDesc = filter.Description;
            glTran1.FinPeriodID = finPeriodID;
            glTran1.TranDate = filter.CuryEffDate;
            glTran1.CuryInfoID = new long?();
            glTran1.Released = new bool?(true);
            glTran1.ReferenceID = revaluedApHistory.VendorID;
            ((PXSelectBase<PX.Objects.GL.GLTran>) instance1.GLTranModuleBatNbr).Insert(glTran1);
            VendorClass vendorClass1 = PXResultset<VendorClass>.op_Implicit(PXSelectBase<VendorClass, PXSelectReadonly<VendorClass, Where<VendorClass.vendorClassID, Equal<Required<VendorClass.vendorClassID>>>>.Config>.Select((PXGraph) instance1, new object[1]
            {
              (object) revaluedApHistory.VendorClassID
            })) ?? new VendorClass();
            nullable2 = vendorClass1.UnrealizedGainAcctID;
            if (!nullable2.HasValue)
            {
              VendorClass vendorClass2 = vendorClass1;
              nullable2 = new int?();
              int? nullable8 = nullable2;
              vendorClass2.UnrealizedGainSubID = nullable8;
            }
            nullable2 = vendorClass1.UnrealizedLossAcctID;
            if (!nullable2.HasValue)
            {
              VendorClass vendorClass3 = vendorClass1;
              nullable2 = new int?();
              int? nullable9 = nullable2;
              vendorClass3.UnrealizedLossSubID = nullable9;
            }
            PX.Objects.GL.GLTran glTran6 = new PX.Objects.GL.GLTran();
            glTran6.SummPost = new bool?(true);
            glTran6.ZeroPost = new bool?(false);
            glTran6.CuryDebitAmt = new Decimal?(0M);
            glTran6.CuryCreditAmt = new Decimal?(0M);
            nullable1 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.DebitTotal;
            nullable5 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.CreditTotal;
            if (nullable1.GetValueOrDefault() > nullable5.GetValueOrDefault() & nullable1.HasValue & nullable5.HasValue)
            {
              PX.Objects.GL.GLTran glTran7 = glTran6;
              nullable2 = vendorClass1.UnrealizedGainAcctID;
              int? nullable10 = nullable2 ?? currency.UnrealizedGainAcctID;
              glTran7.AccountID = nullable10;
              PX.Objects.GL.GLTran glTran8 = glTran6;
              nullable2 = vendorClass1.UnrealizedGainSubID;
              int? nullable11 = nullable2 ?? GainLossSubAccountMaskAttribute.GetSubID<Currency.unrealizedGainSubID>((PXGraph) instance1, revaluedApHistory.BranchID, currency);
              glTran8.SubID = nullable11;
              glTran6.DebitAmt = new Decimal?(0M);
              PX.Objects.GL.GLTran glTran9 = glTran6;
              nullable5 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.DebitTotal;
              nullable1 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.CreditTotal;
              Decimal? nullable12 = nullable5.HasValue & nullable1.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
              glTran9.CreditAmt = nullable12;
            }
            else
            {
              PX.Objects.GL.GLTran glTran10 = glTran6;
              nullable2 = vendorClass1.UnrealizedLossAcctID;
              int? nullable13 = nullable2 ?? currency.UnrealizedLossAcctID;
              glTran10.AccountID = nullable13;
              PX.Objects.GL.GLTran glTran11 = glTran6;
              nullable2 = vendorClass1.UnrealizedLossSubID;
              int? nullable14 = nullable2 ?? GainLossSubAccountMaskAttribute.GetSubID<Currency.unrealizedLossSubID>((PXGraph) instance1, revaluedApHistory.BranchID, currency);
              glTran11.SubID = nullable14;
              PX.Objects.GL.GLTran glTran12 = glTran6;
              nullable1 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.CreditTotal;
              nullable5 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.DebitTotal;
              Decimal? nullable15 = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
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
            CuryAPHist curyApHist1 = new CuryAPHist();
            curyApHist1.BranchID = revaluedApHistory.BranchID;
            curyApHist1.AccountID = revaluedApHistory.AccountID;
            curyApHist1.SubID = revaluedApHistory.SubID;
            curyApHist1.FinPeriodID = filter.FinPeriodID;
            curyApHist1.VendorID = revaluedApHistory.VendorID;
            curyApHist1.CuryID = revaluedApHistory.CuryID;
            CuryAPHist curyApHist2 = (CuryAPHist) cach1.Insert((object) curyApHist1);
            nullable5 = curyApHist2.FinPtdRevalued;
            nullable1 = revaluedApHistory.FinPtdRevalued;
            curyApHist2.FinPtdRevalued = nullable5.HasValue & nullable1.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
            APHist apHist1 = new APHist();
            apHist1.BranchID = revaluedApHistory.BranchID;
            apHist1.AccountID = revaluedApHistory.AccountID;
            apHist1.SubID = revaluedApHistory.SubID;
            apHist1.FinPeriodID = filter.FinPeriodID;
            apHist1.VendorID = revaluedApHistory.VendorID;
            APHist apHist2 = (APHist) cach2.Insert((object) apHist1);
            nullable1 = apHist2.FinPtdRevalued;
            nullable5 = revaluedApHistory.FinPtdRevalued;
            apHist2.FinPtdRevalued = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
            PXProcessing<RevaluedAPHistory>.SetProcessed();
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
    PXProcessing<RevaluedAPHistory>.SetCurrentItem((object) null);
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
    foreach (RevaluedAPHistory revaluedApHistory in ((PXSelectBase) this.APAccountList).Cache.Updated)
    {
      nullable = revaluedApHistory.Selected;
      if (nullable.Value)
        num += revaluedApHistory.FinPtdRevalued.Value;
    }
    if (num == 0M)
      throw new PXOperationCompletedWithWarningException("No revaluation entry was made since Original Balance equals the Revalued Balance");
  }
}
