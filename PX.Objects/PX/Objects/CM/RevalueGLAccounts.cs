// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.RevalueGLAccounts
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.Overrides.PostGraph;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.CM;

[TableAndChartDashboardType]
public class RevalueGLAccounts : RevalueAcountsBase<RevaluedGLHistory>
{
  public PXCancel<RevalueFilter> Cancel;
  public PXFilter<RevalueFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<RevaluedGLHistory, RevalueFilter, Where<boolTrue, Equal<boolTrue>>, OrderBy<Asc<RevaluedGLHistory.ledgerID, Asc<RevaluedGLHistory.accountID, Asc<RevaluedGLHistory.subID>>>>> GLAccountList;
  public PXSelect<CurrencyInfo> currencyinfo;
  public PXSetup<GLSetup> glsetup;
  public PXSetup<CMSetup> cmsetup;
  public PXSetup<Company> company;

  public RevalueGLAccounts()
  {
    CMSetup current1 = ((PXSelectBase<CMSetup>) this.cmsetup).Current;
    GLSetup current2 = ((PXSelectBase<GLSetup>) this.glsetup).Current;
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      throw new Exception("Multi-Currency is not activated");
    ((PXProcessing<RevaluedGLHistory>) this.GLAccountList).SetProcessCaption("Revalue");
    ((PXProcessing<RevaluedGLHistory>) this.GLAccountList).SetProcessAllVisible(false);
    PXUIFieldAttribute.SetEnabled<RevaluedGLHistory.finPtdRevalued>(((PXSelectBase) this.GLAccountList).Cache, (object) null, true);
  }

  protected virtual void _(PX.Data.Events.RowSelected<RevaluedGLHistory> e)
  {
    RevaluedGLHistory row = e.Row;
    if (row == null)
      return;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, row.AccountID);
    PXUIFieldAttribute.SetEnabled<RevaluedGLHistory.selected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<RevaluedGLHistory>>) e).Cache, (object) row, account.Active.GetValueOrDefault());
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<RevaluedGLHistory>>) e).Cache;
    RevaluedGLHistory revaluedGlHistory = row;
    PXSetPropertyException propertyException;
    if (!account.Active.GetValueOrDefault())
      propertyException = new PXSetPropertyException("The {0} account is inactive.", (PXErrorLevel) 3, new object[1]
      {
        (object) account.AccountCD
      });
    else
      propertyException = (PXSetPropertyException) null;
    cache.RaiseExceptionHandling<RevaluedGLHistory.selected>((object) revaluedGlHistory, (object) null, (Exception) propertyException);
  }

  protected virtual void RevalueFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RevalueGLAccounts.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new RevalueGLAccounts.\u003C\u003Ec__DisplayClass9_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.filter = (RevalueFilter) e.Row;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.filter == null)
      return;
    // ISSUE: method pointer
    ((PXProcessingBase<RevaluedGLHistory>) this.GLAccountList).SetProcessDelegate(new PXProcessingBase<RevaluedGLHistory>.ProcessListDelegate((object) cDisplayClass90, __methodptr(\u003CRevalueFilter_RowSelected\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    this.VerifyCurrencyEffectiveDate(sender, cDisplayClass90.filter);
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
    ((PXSelectBase) this.GLAccountList).Cache.Clear();
    sender.SetDefaultExt<RevalueFilter.curyEffDate>(e.Row);
  }

  protected virtual void RevalueFilter_FinPeriodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.GLAccountList).Cache.Clear();
    sender.SetDefaultExt<RevalueFilter.curyEffDate>(e.Row);
  }

  protected virtual void RevalueFilter_CuryEffDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.GLAccountList).Cache.Clear();
  }

  protected virtual void RevalueFilter_CuryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.GLAccountList).Cache.Clear();
  }

  protected virtual void RevalueFilter_TotalRevalued_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    Decimal num1 = 0M;
    foreach (RevaluedGLHistory revaluedGlHistory in ((PXSelectBase) this.GLAccountList).Cache.Updated)
    {
      if (revaluedGlHistory.Selected.Value)
      {
        Decimal num2 = AccountRules.IsDEALAccount(revaluedGlHistory.AccountType) ? 1.0M : -1.0M;
        num1 += num2 * revaluedGlHistory.FinPtdRevalued.Value;
      }
    }
    if (num1 == 0M)
    {
      TimeSpan timeSpan;
      Exception exception;
      if (PXLongOperation.GetStatus(((PXGraph) this).UID, ref timeSpan, ref exception) != 2)
        return;
      ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<RevalueFilter.totalRevalued>(e.Row, (object) num1, (Exception) new PXSetPropertyException("No revaluation entry was made since Original Balance equals the Revalued Balance", (PXErrorLevel) 2));
    }
    else
      e.ReturnValue = (object) num1;
  }

  public virtual IEnumerable glaccountlist()
  {
    RevalueGLAccounts revalueGlAccounts = this;
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    // ISSUE: method reference
    bool flag = ((IQueryable<PXResult<PX.Objects.GL.DAC.Organization>>) PXSelectBase<PX.Objects.GL.DAC.Organization, PXViewOf<PX.Objects.GL.DAC.Organization>.BasedOn<SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.DAC.Organization.bAccountID, IBqlInt>.IsEqual<BqlField<RevalueFilter.orgBAccountID, IBqlInt>.FromCurrent>>>.Config>.Select((PXGraph) revalueGlAccounts, Array.Empty<object>())).Select<PXResult<PX.Objects.GL.DAC.Organization>, string>(Expression.Lambda<Func<PXResult<PX.Objects.GL.DAC.Organization>, string>>((Expression) Expression.Property((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PX.Objects.GL.DAC.Organization.get_OrganizationType))), parameterExpression1)).SingleOrDefault<string>() == "Group";
    PXResultset<PX.Objects.GL.Branch> source1 = PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, LeftJoin<FinPeriod, On<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>, And<Where<PX.Objects.GL.Branch.branchID, InsideBranchesOf<Current2<RevalueFilter.orgBAccountID>>, Or<Current2<RevalueFilter.orgBAccountID>, IsNull, And<Not<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>>>>>>>, Where2<Where<FinPeriod.masterFinPeriodID, Equal<Current<RevalueFilter.finPeriodID>>, And<Where<Current2<RevalueFilter.orgBAccountID>, IsNull, Or<True, Equal<Required<PX.Objects.GL.DAC.Organization.selected>>>>>>, Or<Where<FinPeriod.finPeriodID, Equal<Current<RevalueFilter.finPeriodID>>, And<Current2<RevalueFilter.orgBAccountID>, IsNotNull, And<False, Equal<Required<PX.Objects.GL.DAC.Organization.selected>>>>>>>>.Config>.Select((PXGraph) revalueGlAccounts, new object[2]
    {
      (object) flag,
      (object) flag
    });
    ParameterExpression instance1;
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    Expression<Func<PXResult<PX.Objects.GL.Branch>, Tuple<string, int?, int?>>> selector1 = Expression.Lambda<Func<PXResult<PX.Objects.GL.Branch>, Tuple<string, int?, int?>>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Tuple.Create)), new Expression[3]
    {
      (Expression) Expression.Property((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (FinPeriod.get_FinPeriodID))),
      (Expression) Expression.Property((Expression) Expression.Call((Expression) instance1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PX.Objects.GL.Branch.get_BranchID))),
      (Expression) Expression.Property((Expression) Expression.Call((Expression) instance1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PX.Objects.GL.Branch.get_LedgerID)))
    }), instance1);
    foreach (Tuple<string, int?, int?> tuple1 in (IEnumerable<Tuple<string, int?, int?>>) ((IQueryable<PXResult<PX.Objects.GL.Branch>>) source1).Select<PXResult<PX.Objects.GL.Branch>, Tuple<string, int?, int?>>(selector1))
    {
      PXResultset<GLHistoryByPeriod> source2 = PXSelectBase<GLHistoryByPeriod, PXSelectJoin<GLHistoryByPeriod, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<GLHistoryByPeriod.accountID>>, LeftJoin<GLHistory, On<GLHistory.ledgerID, Equal<GLHistoryByPeriod.ledgerID>, And<GLHistory.branchID, Equal<GLHistoryByPeriod.branchID>, And<GLHistory.accountID, Equal<GLHistoryByPeriod.accountID>, And<GLHistory.subID, Equal<GLHistoryByPeriod.subID>, And<GLHistory.finPeriodID, Equal<GLHistoryByPeriod.lastActivityPeriod>>>>>>>>, Where<GLHistoryByPeriod.finPeriodID, Equal<Required<GLHistoryByPeriod.finPeriodID>>, And<GLHistoryByPeriod.branchID, Equal<Required<GLHistoryByPeriod.branchID>>, And<GLHistoryByPeriod.ledgerID, Equal<Required<GLHistoryByPeriod.ledgerID>>, And<PX.Objects.GL.Account.curyID, Equal<Current<RevalueFilter.curyID>>, And<Where<GLHistory.curyFinYtdBalance, NotEqual<decimal0>, Or<GLHistory.finYtdBalance, NotEqual<decimal0>>>>>>>>>.Config>.Select((PXGraph) revalueGlAccounts, new object[3]
      {
        (object) tuple1.Item1,
        (object) tuple1.Item2,
        (object) tuple1.Item3
      });
      ParameterExpression instance2;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      Expression<Func<PXResult<GLHistoryByPeriod>, Tuple<GLHistory, PX.Objects.GL.Account>>> selector2 = Expression.Lambda<Func<PXResult<GLHistoryByPeriod>, Tuple<GLHistory, PX.Objects.GL.Account>>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Tuple.Create)), new Expression[2]
      {
        (Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()),
        (Expression) Expression.Call((Expression) instance2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>())
      }), instance2);
      foreach (Tuple<GLHistory, PX.Objects.GL.Account> tuple2 in (IEnumerable<Tuple<GLHistory, PX.Objects.GL.Account>>) ((IQueryable<PXResult<GLHistoryByPeriod>>) source2).Select<PXResult<GLHistoryByPeriod>, Tuple<GLHistory, PX.Objects.GL.Account>>(selector2))
      {
        PX.Objects.GL.Account account = tuple2.Item2;
        RevaluedGLHistory revaluedGlHistory1 = new RevaluedGLHistory();
        PXCache<GLHistory>.RestoreCopy((GLHistory) revaluedGlHistory1, tuple2.Item1);
        revaluedGlHistory1.AccountType = account.Type;
        RevaluedGLHistory revaluedGlHistory2;
        if ((revaluedGlHistory2 = ((PXSelectBase<RevaluedGLHistory>) revalueGlAccounts.GLAccountList).Locate(revaluedGlHistory1)) != null)
        {
          yield return (object) revaluedGlHistory2;
        }
        else
        {
          ((PXSelectBase) revalueGlAccounts.GLAccountList).Cache.SetStatus((object) revaluedGlHistory1, (PXEntryStatus) 5);
          if (string.IsNullOrEmpty(revaluedGlHistory1.CuryRateTypeID = account.RevalCuryRateTypeId))
            revaluedGlHistory1.CuryRateTypeID = ((PXSelectBase<CMSetup>) revalueGlAccounts.cmsetup).Current.GLRateTypeReval;
          if (string.IsNullOrEmpty(revaluedGlHistory1.CuryRateTypeID))
          {
            ((PXSelectBase) revalueGlAccounts.GLAccountList).Cache.RaiseExceptionHandling<RevaluedGLHistory.curyRateTypeID>((object) revaluedGlHistory1, (object) null, (Exception) new PXSetPropertyException("Currency Rate Type is not defined."));
          }
          else
          {
            PXResultset<CurrencyRate> pxResultset;
            if (((PXSelectBase<RevalueFilter>) revalueGlAccounts.Filter).Current.OrganizationBaseCuryID != null)
              pxResultset = PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Current<RevalueFilter.curyID>>, And<CurrencyRate.toCuryID, Equal<Current<RevalueFilter.organizationBaseCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<PX.Objects.GL.Account.revalCuryRateTypeId>>, And<CurrencyRate.curyEffDate, LessEqual<Current<RevalueFilter.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select((PXGraph) revalueGlAccounts, new object[1]
              {
                (object) revaluedGlHistory1.CuryRateTypeID
              });
            else
              pxResultset = PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Current<RevalueFilter.curyID>>, And<CurrencyRate.toCuryID, Equal<Current<Company.baseCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<PX.Objects.GL.Account.revalCuryRateTypeId>>, And<CurrencyRate.curyEffDate, LessEqual<Current<RevalueFilter.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select((PXGraph) revalueGlAccounts, new object[1]
              {
                (object) revaluedGlHistory1.CuryRateTypeID
              });
            CurrencyRate currencyRate = PXResultset<CurrencyRate>.op_Implicit(pxResultset);
            if (currencyRate == null || currencyRate.CuryMultDiv == null)
            {
              revaluedGlHistory1.CuryMultDiv = "M";
              revaluedGlHistory1.CuryRate = new Decimal?(1M);
              revaluedGlHistory1.RateReciprocal = new Decimal?(1M);
              revaluedGlHistory1.CuryEffDate = ((PXSelectBase<RevalueFilter>) revalueGlAccounts.Filter).Current.CuryEffDate;
              ((PXSelectBase) revalueGlAccounts.GLAccountList).Cache.RaiseExceptionHandling<RevaluedGLHistory.curyRate>((object) revaluedGlHistory1, (object) 1M, (Exception) new PXSetPropertyException("Currency Rate is not defined.", (PXErrorLevel) 3));
            }
            else
            {
              revaluedGlHistory1.CuryRate = currencyRate.CuryRate;
              revaluedGlHistory1.RateReciprocal = currencyRate.RateReciprocal;
              revaluedGlHistory1.CuryEffDate = currencyRate.CuryEffDate;
              revaluedGlHistory1.CuryMultDiv = currencyRate.CuryMultDiv;
            }
            Decimal baseval;
            PXCurrencyAttribute.CuryConvBase(((PXSelectBase) revalueGlAccounts.currencyinfo).Cache, new CurrencyInfo()
            {
              BaseCuryID = ((PXSelectBase<RevalueFilter>) revalueGlAccounts.Filter).Current.OrganizationBaseCuryID == null ? ((PXSelectBase<Company>) revalueGlAccounts.company).Current.BaseCuryID : ((PXSelectBase<RevalueFilter>) revalueGlAccounts.Filter).Current.OrganizationBaseCuryID,
              CuryID = revaluedGlHistory1.CuryID,
              CuryMultDiv = revaluedGlHistory1.CuryMultDiv,
              CuryRate = revaluedGlHistory1.CuryRate
            }, revaluedGlHistory1.CuryFinYtdBalance.Value, out baseval);
            revaluedGlHistory1.FinYtdRevalued = new Decimal?(baseval);
            RevaluedGLHistory revaluedGlHistory3 = revaluedGlHistory1;
            Decimal? finYtdRevalued = revaluedGlHistory1.FinYtdRevalued;
            Decimal? finYtdBalance = revaluedGlHistory1.FinYtdBalance;
            Decimal? nullable = finYtdRevalued.HasValue & finYtdBalance.HasValue ? new Decimal?(finYtdRevalued.GetValueOrDefault() - finYtdBalance.GetValueOrDefault()) : new Decimal?();
            revaluedGlHistory3.FinPtdRevalued = nullable;
            ParameterExpression parameterExpression2;
            // ISSUE: method reference
            // ISSUE: method reference
            revaluedGlHistory1.LastRevaluedFinPeriodID = ((IQueryable<PXResult<GLHistory>>) PXSelectBase<GLHistory, PXSelect<GLHistory, Where<GLHistory.finPtdRevalued, NotEqual<decimal0>, And<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>, And<GLHistory.branchID, Equal<Required<GLHistory.branchID>>, And<GLHistory.accountID, Equal<Required<GLHistoryByPeriod.accountID>>, And<GLHistory.subID, Equal<Required<GLHistoryByPeriod.subID>>>>>>>>.Config>.Select((PXGraph) revalueGlAccounts, new object[4]
            {
              (object) revaluedGlHistory1.LedgerID,
              (object) revaluedGlHistory1.BranchID,
              (object) revaluedGlHistory1.AccountID,
              (object) revaluedGlHistory1.SubID
            })).Select<PXResult<GLHistory>, string>(Expression.Lambda<Func<PXResult<GLHistory>, string>>((Expression) Expression.Property((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (BaseGLHistory.get_FinPeriodID))), parameterExpression2)).Max<string>();
          }
          yield return (object) revaluedGlHistory1;
        }
      }
    }
  }

  public void Revalue(RevalueFilter filter, List<RevaluedGLHistory> list)
  {
    JournalEntry instance1 = PXGraph.CreateInstance<JournalEntry>();
    PX.Objects.GL.PostGraph instance2 = PXGraph.CreateInstance<PX.Objects.GL.PostGraph>();
    PXCache cach = ((PXGraph) instance1).Caches[typeof (AcctHist)];
    ((PXGraph) instance1).Views.Caches.Add(typeof (AcctHist));
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
      foreach (RevaluedGLHistory revaluedGlHistory in list)
      {
        PXProcessing<RevaluedGLHistory>.SetCurrentItem((object) revaluedGlHistory);
        Decimal? finPtdRevalued = revaluedGlHistory.FinPtdRevalued;
        Decimal num1 = 0M;
        if (finPtdRevalued.GetValueOrDefault() == num1 & finPtdRevalued.HasValue)
        {
          PXProcessing<RevaluedGLHistory>.SetProcessed();
        }
        else
        {
          PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(filter.OrgBAccountID);
          string finPeriodID = !filter.OrgBAccountID.HasValue || organizationByBaccountId != null && organizationByBaccountId.IsGroup ? this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(revaluedGlHistory.BranchID), filter.FinPeriodID).Result.FinPeriodID : filter.FinPeriodID;
          if (!this.CheckFinPeriod(finPeriodID, revaluedGlHistory.BranchID).IsSuccess)
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
            Batch batch = documentList.Find<Batch.branchID>((object) revaluedGlHistory.BranchID) ?? new Batch();
            if (batch.BatchNbr == null)
            {
              ((PXGraph) instance1).Clear();
              CurrencyInfo currencyInfo1 = new CurrencyInfo();
              currencyInfo1.CuryID = revaluedGlHistory.CuryID;
              currencyInfo1.CuryEffDate = revaluedGlHistory.CuryEffDate;
              currencyInfo1.BaseCalc = new bool?(false);
              CurrencyInfo currencyInfo2 = ((PXSelectBase<CurrencyInfo>) instance1.currencyinfo).Insert(currencyInfo1) ?? currencyInfo1;
              ((PXSelectBase<Batch>) instance1.BatchModule).Insert(new Batch()
              {
                BranchID = revaluedGlHistory.BranchID,
                Module = "CM",
                Status = "U",
                AutoReverse = new bool?(false),
                Released = new bool?(true),
                Hold = new bool?(false),
                DateEntered = filter.CuryEffDate,
                FinPeriodID = finPeriodID,
                CuryID = revaluedGlHistory.CuryID,
                CuryInfoID = currencyInfo2.CuryInfoID,
                DebitTotal = new Decimal?(0M),
                CreditTotal = new Decimal?(0M),
                Description = filter.Description
              });
              CurrencyInfo currencyInfo3 = PXResultset<CurrencyInfo>.op_Implicit(((PXSelectBase<CurrencyInfo>) instance1.currencyinfo).Select(Array.Empty<object>()));
              if (currencyInfo3 != null)
              {
                currencyInfo3.CuryID = revaluedGlHistory.CuryID;
                currencyInfo3.CuryEffDate = revaluedGlHistory.CuryEffDate;
                currencyInfo3.CuryRateTypeID = revaluedGlHistory.CuryRateTypeID;
                currencyInfo3.CuryRate = revaluedGlHistory.CuryRate;
                currencyInfo3.RecipRate = revaluedGlHistory.RateReciprocal;
                currencyInfo3.CuryMultDiv = revaluedGlHistory.CuryMultDiv;
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
            GLTran glTran1 = new GLTran();
            glTran1.SummPost = new bool?(false);
            glTran1.AccountID = revaluedGlHistory.AccountID;
            glTran1.SubID = revaluedGlHistory.SubID;
            glTran1.CuryDebitAmt = new Decimal?(0M);
            glTran1.CuryCreditAmt = new Decimal?(0M);
            Decimal? nullable1;
            if (revaluedGlHistory.AccountType == "A" || revaluedGlHistory.AccountType == "E")
            {
              GLTran glTran2 = glTran1;
              finPtdRevalued = revaluedGlHistory.FinPtdRevalued;
              Decimal num2 = 0M;
              Decimal? nullable2 = finPtdRevalued.GetValueOrDefault() < num2 & finPtdRevalued.HasValue ? new Decimal?(0M) : revaluedGlHistory.FinPtdRevalued;
              glTran2.DebitAmt = nullable2;
              GLTran glTran3 = glTran1;
              finPtdRevalued = revaluedGlHistory.FinPtdRevalued;
              Decimal num3 = 0M;
              Decimal? nullable3;
              if (!(finPtdRevalued.GetValueOrDefault() < num3 & finPtdRevalued.HasValue))
              {
                nullable3 = new Decimal?(0M);
              }
              else
              {
                Decimal num4 = -1M;
                finPtdRevalued = revaluedGlHistory.FinPtdRevalued;
                if (!finPtdRevalued.HasValue)
                {
                  nullable1 = new Decimal?();
                  nullable3 = nullable1;
                }
                else
                  nullable3 = new Decimal?(num4 * finPtdRevalued.GetValueOrDefault());
              }
              glTran3.CreditAmt = nullable3;
            }
            else
            {
              GLTran glTran4 = glTran1;
              finPtdRevalued = revaluedGlHistory.FinPtdRevalued;
              Decimal num5 = 0M;
              Decimal? nullable4;
              if (!(finPtdRevalued.GetValueOrDefault() < num5 & finPtdRevalued.HasValue))
              {
                nullable4 = new Decimal?(0M);
              }
              else
              {
                Decimal num6 = -1M;
                finPtdRevalued = revaluedGlHistory.FinPtdRevalued;
                if (!finPtdRevalued.HasValue)
                {
                  nullable1 = new Decimal?();
                  nullable4 = nullable1;
                }
                else
                  nullable4 = new Decimal?(num6 * finPtdRevalued.GetValueOrDefault());
              }
              glTran4.DebitAmt = nullable4;
              GLTran glTran5 = glTran1;
              finPtdRevalued = revaluedGlHistory.FinPtdRevalued;
              Decimal num7 = 0M;
              Decimal? nullable5 = finPtdRevalued.GetValueOrDefault() < num7 & finPtdRevalued.HasValue ? new Decimal?(0M) : revaluedGlHistory.FinPtdRevalued;
              glTran5.CreditAmt = nullable5;
            }
            glTran1.TranType = "REV";
            glTran1.TranClass = revaluedGlHistory.AccountType;
            glTran1.RefNbr = string.Empty;
            glTran1.TranDesc = filter.Description;
            glTran1.FinPeriodID = finPeriodID;
            glTran1.TranDate = filter.CuryEffDate;
            glTran1.CuryInfoID = new long?();
            glTran1.Released = new bool?(true);
            glTran1.ReferenceID = new int?();
            glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
            ((PXSelectBase<GLTran>) instance1.GLTranModuleBatNbr).Insert(glTran1);
            foreach (PXResult<GLTran> pxResult in ((PXSelectBase<GLTran>) instance1.GLTranModuleBatNbr).SearchAll<Asc<GLTran.tranClass>>(new object[1]
            {
              (object) "G"
            }, Array.Empty<object>()))
            {
              GLTran glTran6 = PXResult<GLTran>.op_Implicit(pxResult);
              ((PXSelectBase<GLTran>) instance1.GLTranModuleBatNbr).Delete(glTran6);
            }
            GLTran glTran7 = new GLTran();
            glTran7.SummPost = new bool?(true);
            glTran7.ZeroPost = new bool?(false);
            glTran7.CuryDebitAmt = new Decimal?(0M);
            glTran7.CuryCreditAmt = new Decimal?(0M);
            Decimal? debitTotal = ((PXSelectBase<Batch>) instance1.BatchModule).Current.DebitTotal;
            nullable1 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.CreditTotal;
            if (debitTotal.GetValueOrDefault() > nullable1.GetValueOrDefault() & debitTotal.HasValue & nullable1.HasValue)
            {
              glTran7.AccountID = currency.RevalGainAcctID;
              glTran7.SubID = GainLossSubAccountMaskAttribute.GetSubID<Currency.revalGainSubID>((PXGraph) instance1, revaluedGlHistory.BranchID, currency);
              glTran7.DebitAmt = new Decimal?(0M);
              GLTran glTran8 = glTran7;
              nullable1 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.DebitTotal;
              Decimal? creditTotal = ((PXSelectBase<Batch>) instance1.BatchModule).Current.CreditTotal;
              Decimal? nullable6 = nullable1.HasValue & creditTotal.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - creditTotal.GetValueOrDefault()) : new Decimal?();
              glTran8.CreditAmt = nullable6;
            }
            else
            {
              glTran7.AccountID = currency.RevalLossAcctID;
              glTran7.SubID = GainLossSubAccountMaskAttribute.GetSubID<Currency.revalLossSubID>((PXGraph) instance1, revaluedGlHistory.BranchID, currency);
              GLTran glTran9 = glTran7;
              Decimal? creditTotal = ((PXSelectBase<Batch>) instance1.BatchModule).Current.CreditTotal;
              nullable1 = ((PXSelectBase<Batch>) instance1.BatchModule).Current.DebitTotal;
              Decimal? nullable7 = creditTotal.HasValue & nullable1.HasValue ? new Decimal?(creditTotal.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
              glTran9.DebitAmt = nullable7;
              glTran7.CreditAmt = new Decimal?(0M);
            }
            glTran7.TranType = "REV";
            glTran7.TranClass = "G";
            glTran7.RefNbr = string.Empty;
            glTran7.TranDesc = filter.Description;
            glTran7.Released = new bool?(true);
            glTran7.ReferenceID = new int?();
            ((PXSelectBase<GLTran>) instance1.GLTranModuleBatNbr).Insert(glTran7);
            AcctHist acctHist1 = new AcctHist();
            acctHist1.BranchID = revaluedGlHistory.BranchID;
            acctHist1.LedgerID = revaluedGlHistory.LedgerID;
            acctHist1.AccountID = revaluedGlHistory.AccountID;
            acctHist1.SubID = revaluedGlHistory.SubID;
            acctHist1.FinPeriodID = filter.FinPeriodID;
            acctHist1.CuryID = revaluedGlHistory.CuryID;
            acctHist1.BalanceType = "A";
            AcctHist acctHist2 = (AcctHist) cach.Insert((object) acctHist1);
            nullable1 = acctHist2.FinPtdRevalued;
            finPtdRevalued = revaluedGlHistory.FinPtdRevalued;
            acctHist2.FinPtdRevalued = nullable1.HasValue & finPtdRevalued.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + finPtdRevalued.GetValueOrDefault()) : new Decimal?();
            PXProcessing<RevaluedGLHistory>.SetProcessed();
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
    {
      PXProcessing<RevaluedGLHistory>.SetCurrentItem((object) null);
      throw new PXException("At least one item has not been processed.");
    }
    if (documentList.Count > 0)
    {
      ((PXSelectBase<Batch>) instance1.BatchModule).Current = documentList[documentList.Count - 1];
      throw new PXRedirectRequiredException((PXGraph) instance1, "Preview");
    }
    Decimal num8 = 0M;
    foreach (RevaluedGLHistory revaluedGlHistory in ((PXSelectBase) this.GLAccountList).Cache.Updated)
    {
      nullable = revaluedGlHistory.Selected;
      if (nullable.Value)
      {
        Decimal num9 = AccountRules.IsDEALAccount(revaluedGlHistory.AccountType) ? 1.0M : -1.0M;
        num8 += num9 * revaluedGlHistory.FinPtdRevalued.Value;
      }
    }
    if (num8 == 0M)
      throw new PXOperationCompletedWithWarningException("No revaluation entry was made since Original Balance equals the Revalued Balance");
  }
}
