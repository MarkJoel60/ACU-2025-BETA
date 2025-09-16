// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslationProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.CM;

[TableAndChartDashboardType]
[Serializable]
public class TranslationProcess : PXGraph<
#nullable disable
TranslationProcess>
{
  public PXFilter<TranslationProcess.TranslationParams> TranslationParamsFilter;
  [PXHidden]
  public PXSelect<TranslDef> TranslationDefinition;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<CurrencyRate, TranslationProcess.TranslationParams> TranslationCurrencyRateRecords;
  public PXSetup<PX.Objects.CM.CMSetup> CMSetup;
  public PXSetup<PX.Objects.CM.CMSetup> TSetup;
  [Obsolete("This field is not used anymore and will be removed in Acumatica ERP 8.0.")]
  public bool translAvailable;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  protected static string getCompanyFinPeriodID(
    int? BranchID,
    string FinPeriodID,
    IFinPeriodRepository FinPeriodRepository)
  {
    return (!PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>() ? 0 : (!BranchID.HasValue ? 1 : 0)) == 0 ? FinPeriodID : FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(PXAccess.GetBranchID()), FinPeriodID).GetValueOrRaiseError().FinPeriodID;
  }

  protected virtual void TranslateItems(
    List<CurrencyRate> list,
    TranslationProcess.TranslationParams parameters)
  {
    PXCache cach1 = ((PXGraph) this).Caches[typeof (TranslationProcess.TranslationParams)];
    if (parameters.TranslDefId == null || parameters.FinPeriodID == null || !parameters.CuryEffDate.HasValue)
      return;
    if (parameters.BranchID.HasValue)
      this.FinPeriodUtils.CanPostToPeriod((IFinPeriod) this.FinPeriodRepository.GetByID(parameters.FinPeriodID, PXAccess.GetParentOrganizationID(parameters.BranchID))).RaiseIfHasError();
    Currency currency = CurrencyCollection.GetCurrency(((PX.Objects.GL.Ledger) PXSelectorAttribute.Select<TranslationProcess.TranslationParams.destLedgerId>(((PXSelectBase) this.TranslationParamsFilter).Cache, (object) parameters)).BaseCuryID);
    if (currency == null || !currency.TranslationGainAcctID.HasValue || !currency.TranslationGainSubID.HasValue || !currency.TranslationLossAcctID.HasValue || !currency.TranslationLossSubID.HasValue)
      throw new PXException("To perform translation, specify Translation Loss/Gain Account (Subaccount) for currency {0}.", new object[1]
      {
        (object) (currency?.CuryID ?? "")
      });
    TranslationHistory translationHistory1 = PXResultset<TranslationHistory>.op_Implicit(PXSelectBase<TranslationHistory, PXSelect<TranslationHistory, Where<TranslationHistory.ledgerID, Equal<Required<TranslationHistory.ledgerID>>, And<TranslationHistory.status, NotEqual<TranslationStatus.released>, And<TranslationHistory.status, NotEqual<TranslationStatus.voided>, And<Where<TranslationHistory.branchID, Equal<Required<TranslationHistory.branchID>>, Or<TranslationHistory.branchID, IsNull, Or<Required<TranslationHistory.branchID>, IsNull>>>>>>>, OrderBy<Desc<TranslationHistory.finPeriodID>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) parameters.DestLedgerId,
      (object) parameters.BranchID,
      (object) parameters.BranchID
    }));
    if (translationHistory1 != null)
      throw new PXException(parameters.BranchID.HasValue ? "The translation cannot be prepared because there is an unreleased translation worksheet ({0}) created for the branch and destination ledger. To proceed, release or delete the worksheet on the Translation Worksheets (CM304000) form." : "The translation cannot be prepared because there is an unreleased translation worksheet ({0}) created for the destination ledger. To proceed, release or delete the worksheet on the Translation Worksheets (CM304000) form.", new object[1]
      {
        (object) translationHistory1.ReferenceNbr
      });
    if (((IQueryable<PXResult<TranslationHistory>>) PXSelectBase<TranslationHistory, PXSelectJoin<TranslationHistory, LeftJoin<Batch, On<TranslationHistory.batchNbr, Equal<Batch.batchNbr>, And<Batch.module, Equal<BatchModule.moduleCM>>>>, Where2<Where<TranslationHistory.ledgerID, Equal<Required<TranslationHistory.ledgerID>>, And<Batch.status, NotEqual<BatchStatus.posted>>>, And<Where<TranslationHistory.branchID, Equal<Required<TranslationHistory.branchID>>, Or<TranslationHistory.branchID, IsNull, Or<Required<TranslationHistory.branchID>, IsNull>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
    {
      (object) parameters.DestLedgerId,
      (object) parameters.BranchID,
      (object) parameters.BranchID
    })).Any<PXResult<TranslationHistory>>())
      throw new PXException("There are unposted batches in the reporting branch/ledger. Post the batches before proceeding.");
    TranslationHistory translationHistory2 = PXResultset<TranslationHistory>.op_Implicit(PXSelectBase<TranslationHistory, PXSelect<TranslationHistory, Where<TranslationHistory.finPeriodID, Greater<Required<TranslationHistory.finPeriodID>>, And<TranslationHistory.ledgerID, Equal<Required<TranslationHistory.ledgerID>>, And<TranslationHistory.released, Equal<boolTrue>>>>, OrderBy<Desc<TranslationHistory.finPeriodID>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) TranslationProcess.getCompanyFinPeriodID(parameters.BranchID, parameters.FinPeriodID, this.FinPeriodRepository),
      (object) parameters.DestLedgerId
    }));
    if (translationHistory2 != null)
      cach1.RaiseExceptionHandling<TranslationProcess.TranslationParams.finPeriodID>((object) parameters, PXFieldState.UnwrapValue(((PXSelectBase<TranslationProcess.TranslationParams>) this.TranslationParamsFilter).GetValueExt<TranslationProcess.TranslationParams.finPeriodID>(parameters)), (Exception) new PXSetPropertyException("Translation '{0}' is released in future period! Reverse this translation or create another one in the future period.", new object[2]
      {
        (object) translationHistory2.ReferenceNbr,
        (object) (PXErrorLevel) 2
      }));
    try
    {
      TranslationDefinitionMaint instance = PXGraph.CreateInstance<TranslationDefinitionMaint>();
      TranslDef def = PXResultset<TranslDef>.op_Implicit(PXSelectBase<TranslDef, PXSelect<TranslDef, Where<TranslDef.translDefId, Equal<Required<TranslDef.translDefId>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) parameters.TranslDefId
      }));
      if (def != null)
      {
        foreach (PXResult<TranslDefDet> pxResult in PXSelectBase<TranslDefDet, PXSelect<TranslDefDet, Where<TranslDefDet.translDefId, Equal<Required<TranslDef.translDefId>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) parameters.TranslDefId
        }))
        {
          TranslDefDet newRow = PXResult<TranslDefDet>.op_Implicit(pxResult);
          PXCache cach2 = ((PXGraph) instance).Caches[typeof (TranslDefDet)];
          instance.CheckDetail(cach2, newRow, def.Active.GetValueOrDefault(), parameters.DestLedgerId.Value, def, new Exception("Transaction can not be created!"));
        }
      }
    }
    catch (Exception ex)
    {
      throw new Exception("Translation Definition has some cross intervals. So Translation cannot be created.");
    }
    TranslationProcess.TranslHistCreate(parameters, list);
  }

  protected virtual IEnumerable translationCurrencyRateRecords()
  {
    List<CurrencyRate> source = new List<CurrencyRate>();
    if (((PXSelectBase<TranslationProcess.TranslationParams>) this.TranslationParamsFilter).Current.TranslDefId == null || ((PXSelectBase<TranslationProcess.TranslationParams>) this.TranslationParamsFilter).Current.FinPeriodID == null)
      return (IEnumerable) source;
    foreach (PXResult<TranslDef, TranslDefDet> pxResult in PXSelectBase<TranslDef, PXSelectJoinGroupBy<TranslDef, InnerJoin<TranslDefDet, On<TranslDefDet.translDefId, Equal<TranslDef.translDefId>>>, Where<TranslDef.translDefId, Equal<Current<TranslationProcess.TranslationParams.translDefId>>>, Aggregate<GroupBy<TranslDefDet.rateTypeId>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      TranslDef translDef = PXResult<TranslDef, TranslDefDet>.op_Implicit(pxResult);
      PX.Objects.GL.Ledger ledger1 = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelect<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Required<PX.Objects.GL.Ledger.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) translDef.SourceLedgerId
      }));
      PX.Objects.GL.Ledger ledger2 = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelect<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Required<PX.Objects.GL.Ledger.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) translDef.DestLedgerId
      }));
      if (ledger1 == null || ledger2 == null || ledger1.BaseCuryID == null || ledger2.BaseCuryID == null)
        throw new Exception("Translation Definition Ledger Not Found");
      CurrencyRate currencyRate = PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, LessEqual<Current<TranslationProcess.TranslationParams.curyEffDate>>, And<Where<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) PXResult<TranslDef, TranslDefDet>.op_Implicit(pxResult).RateTypeId,
        (object) ledger1.BaseCuryID,
        (object) ledger2.BaseCuryID
      }));
      if (currencyRate != null)
        source.Add(currencyRate);
    }
    if (!source.Any<CurrencyRate>() && ((PXSelectBase<TranslationProcess.TranslationParams>) this.TranslationParamsFilter).Current.SourceCuryID == ((PXSelectBase<TranslationProcess.TranslationParams>) this.TranslationParamsFilter).Current.DestCuryID)
    {
      CurrencyRate currencyRate = new CurrencyRate()
      {
        FromCuryID = ((PXSelectBase<TranslationProcess.TranslationParams>) this.TranslationParamsFilter).Current.SourceCuryID,
        ToCuryID = ((PXSelectBase<TranslationProcess.TranslationParams>) this.TranslationParamsFilter).Current.SourceCuryID,
        CuryRate = new Decimal?(1M),
        CuryMultDiv = "M"
      };
      source.Add(currencyRate);
    }
    return (IEnumerable) source;
  }

  protected static void TranslHistCreate(
    TranslationProcess.TranslationParams parameters,
    List<CurrencyRate> rateList)
  {
    TranslationHistoryMaint instance = PXGraph.CreateInstance<TranslationHistoryMaint>();
    PX.Objects.GL.Ledger ledger1 = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelect<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Required<PX.Objects.GL.Ledger.ledgerID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) parameters.SourceLedgerId
    }));
    PX.Objects.GL.Ledger ledger2 = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelect<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Required<PX.Objects.GL.Ledger.ledgerID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) parameters.DestLedgerId
    }));
    Currency currency = PXResultset<Currency>.op_Implicit(PXSelectBase<Currency, PXSelect<Currency, Where<Currency.curyID, Equal<Required<Currency.curyID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) ledger2.BaseCuryID
    }));
    PX.Objects.CM.CMSetup cmSetup = PXResultset<PX.Objects.CM.CMSetup>.op_Implicit(PXSelectBase<PX.Objects.CM.CMSetup, PXSelect<PX.Objects.CM.CMSetup>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
    PXResultset<GLSetup>.op_Implicit(PXSelectBase<GLSetup, PXSelect<GLSetup>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
    IFinPeriodRepository service = ((PXGraph) instance).GetService<IFinPeriodRepository>();
    string companyFinPeriodId = TranslationProcess.getCompanyFinPeriodID(parameters.BranchID, parameters.FinPeriodID, service);
    TranslationHistory translationHistory1 = new TranslationHistory();
    if (!parameters.BranchID.HasValue)
    {
      translationHistory1.BranchID = ((PXGraph) instance).Accessinfo.BranchID;
      translationHistory1.FinPeriodID = service.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(translationHistory1.BranchID), parameters.FinPeriodID).GetValueOrRaiseError().FinPeriodID;
    }
    else
    {
      translationHistory1.BranchID = parameters.BranchID;
      translationHistory1.FinPeriodID = parameters.FinPeriodID;
    }
    translationHistory1.CuryEffDate = parameters.CuryEffDate;
    translationHistory1.Description = parameters.Description;
    translationHistory1.LedgerID = parameters.DestLedgerId;
    translationHistory1.Status = "U";
    translationHistory1.TranslDefId = parameters.TranslDefId;
    TranslationHistory translationHistory2 = ((PXSelectBase<TranslationHistory>) instance.TranslHistRecords).Insert(translationHistory1);
    Dictionary<int?, Decimal?> dictionary1 = new Dictionary<int?, Decimal?>();
    foreach (PXResult<TranslDefDet> pxResult1 in PXSelectBase<TranslDefDet, PXSelect<TranslDefDet, Where<TranslDefDet.translDefId, Equal<Required<TranslDefDet.translDefId>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) parameters.TranslDefId
    }))
    {
      TranslDefDet translDefDet = PXResult<TranslDefDet>.op_Implicit(pxResult1);
      CurrencyRate currencyRate = (CurrencyRate) null;
      PXCache cach1 = ((PXGraph) instance).Caches[typeof (TranslDefDet)];
      PXCache cach2 = ((PXGraph) instance).Caches[typeof (CurrencyInfo)];
      string acct1 = TranslDefDet.GetAcct(cach1.Graph, translDefDet.AccountIdFrom.Value);
      int? nullable1 = translDefDet.SubIdFrom;
      string str1;
      if (!nullable1.HasValue)
      {
        str1 = (string) null;
      }
      else
      {
        PXGraph graph = cach1.Graph;
        nullable1 = translDefDet.SubIdFrom;
        int subId = nullable1.Value;
        str1 = TranslDefDet.GetSub(graph, subId);
      }
      string str2 = str1;
      PXGraph graph1 = cach1.Graph;
      nullable1 = translDefDet.AccountIdTo;
      int accId = nullable1.Value;
      string acct2 = TranslDefDet.GetAcct(graph1, accId);
      nullable1 = translDefDet.SubIdTo;
      string str3;
      if (!nullable1.HasValue)
      {
        str3 = (string) null;
      }
      else
      {
        PXGraph graph2 = cach1.Graph;
        nullable1 = translDefDet.SubIdTo;
        int subId = nullable1.Value;
        str3 = TranslDefDet.GetSub(graph2, subId);
      }
      string str4 = str3;
      for (int index = 0; index < rateList.Count; ++index)
      {
        CurrencyRate rate = rateList[index];
        if (rate.FromCuryID == ledger1.BaseCuryID && rate.ToCuryID == ledger2.BaseCuryID && rate.CuryRateType == translDefDet.RateTypeId)
        {
          currencyRate = rate;
          break;
        }
      }
      CurrencyInfo info = new CurrencyInfo();
      info.CuryID = ledger1.BaseCuryID;
      info.BaseCuryID = ledger2.BaseCuryID;
      info.CuryEffDate = parameters.CuryEffDate;
      info.CuryRateTypeID = translDefDet.RateTypeId;
      if (currencyRate != null)
      {
        info.CuryMultDiv = currencyRate.CuryMultDiv;
        info.CuryRate = currencyRate.CuryRate;
      }
      else if (object.Equals((object) info.CuryID, (object) info.BaseCuryID))
      {
        info.CuryMultDiv = "M";
        info.CuryRate = new Decimal?(1M);
      }
      else
      {
        info.CuryMultDiv = (string) null;
        info.CuryRate = new Decimal?();
      }
      short? calcMode = translDefDet.CalcMode;
      int? nullable2;
      if (!calcMode.HasValue)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new int?((int) calcMode.GetValueOrDefault());
      nullable1 = nullable2;
      if (nullable1.GetValueOrDefault() == 1)
      {
        PXSelectBase<GLHistoryByPeriod> pxSelectBase = (PXSelectBase<GLHistoryByPeriod>) new PXSelectJoin<GLHistoryByPeriod, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<GLHistoryByPeriod.accountID>>, InnerJoin<PX.Objects.GL.Sub, On<PX.Objects.GL.Sub.subID, Equal<GLHistoryByPeriod.subID>>, CrossJoin<GLSetup, InnerJoin<GLHistory, On<GLHistory.ledgerID, Equal<GLHistoryByPeriod.ledgerID>, And<GLHistory.branchID, Equal<GLHistoryByPeriod.branchID>, And<GLHistory.accountID, Equal<GLHistoryByPeriod.accountID>, And<GLHistory.subID, Equal<GLHistoryByPeriod.subID>, And<GLHistory.finPeriodID, Equal<GLHistoryByPeriod.lastActivityPeriod>>>>>>>>>>, Where<GLHistoryByPeriod.ledgerID, Equal<Required<GLHistoryByPeriod.ledgerID>>, And<GLHistoryByPeriod.finPeriodID, Equal<Required<GLHistoryByPeriod.finPeriodID>>, And<GLHistoryByPeriod.accountID, NotEqual<GLSetup.ytdNetIncAccountID>, And<GLHistoryByPeriod.accountID, NotEqual<Required<GLHistoryByPeriod.accountID>>, And<GLHistoryByPeriod.accountID, NotEqual<Required<GLHistoryByPeriod.accountID>>, And2<Where<PX.Objects.GL.Account.type, Equal<AccountType.asset>, Or<PX.Objects.GL.Account.type, Equal<AccountType.liability>, Or<GLHistoryByPeriod.lastActivityPeriod, GreaterEqual<Required<GLHistoryByPeriod.lastActivityPeriod>>>>>, And<Where<PX.Objects.GL.Account.accountCD, Between<Required<PX.Objects.GL.Account.accountCD>, Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.GL.Account.accountCD, NotEqual<Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.GL.Account.accountCD, NotEqual<Required<PX.Objects.GL.Account.accountCD>>, Or<PX.Objects.GL.Account.accountCD, Equal<Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.GL.Account.accountCD, Less<Required<PX.Objects.GL.Account.accountCD>>, And2<Where<Required<PX.Objects.GL.Sub.subCD>, IsNull, Or<Required<PX.Objects.GL.Sub.subCD>, IsNotNull, And<PX.Objects.GL.Sub.subCD, GreaterEqual<Required<PX.Objects.GL.Sub.subCD>>>>>, Or<PX.Objects.GL.Account.accountCD, Equal<Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.GL.Account.accountCD, Greater<Required<PX.Objects.GL.Account.accountCD>>, And2<Where<Required<PX.Objects.GL.Sub.subCD>, IsNull, Or<Required<PX.Objects.GL.Sub.subCD>, IsNotNull, And<PX.Objects.GL.Sub.subCD, LessEqual<Required<PX.Objects.GL.Sub.subCD>>>>>, Or<PX.Objects.GL.Account.accountCD, Equal<Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.GL.Account.accountCD, Equal<Required<PX.Objects.GL.Account.accountCD>>, And<Where<Required<PX.Objects.GL.Sub.subCD>, IsNull, And<Required<PX.Objects.GL.Sub.subCD>, IsNull, Or<Required<PX.Objects.GL.Sub.subCD>, IsNotNull, And<Required<PX.Objects.GL.Sub.subCD>, IsNotNull, And<PX.Objects.GL.Sub.subCD, Between<Required<PX.Objects.GL.Sub.subCD>, Required<PX.Objects.GL.Sub.subCD>>>>>>>>>>>>>>>>>>>>>>>>>>>((PXGraph) instance);
        foreach (PXResult<GLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLSetup, GLHistory> pxResult2 in pxSelectBase.Select(new object[27]
        {
          (object) parameters.SourceLedgerId,
          (object) companyFinPeriodId,
          (object) currency.TranslationGainAcctID,
          (object) currency.TranslationLossAcctID,
          (object) (companyFinPeriodId.Substring(0, 4) + "01"),
          (object) acct1,
          (object) acct2,
          (object) acct1,
          (object) acct2,
          (object) acct1,
          (object) acct2,
          (object) str2,
          (object) str2,
          (object) str2,
          (object) acct2,
          (object) acct1,
          (object) str4,
          (object) str4,
          (object) str4,
          (object) acct1,
          (object) acct2,
          (object) str2,
          (object) str4,
          (object) str2,
          (object) str4,
          (object) str2,
          (object) str4
        }))
        {
          GLHistory glHistory = PXResult<GLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLSetup, GLHistory>.op_Implicit(pxResult2);
          PX.Objects.GL.Account account = PXResult<GLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLSetup, GLHistory>.op_Implicit(pxResult2);
          nullable1 = parameters.BranchID;
          if (nullable1.HasValue)
          {
            nullable1 = parameters.BranchID;
            int? branchId = glHistory.BranchID;
            if (!(nullable1.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable1.HasValue == branchId.HasValue))
              continue;
          }
          Decimal? nullable3 = glHistory.FinYtdBalance;
          Decimal curyval = nullable3.Value;
          Decimal baseval;
          try
          {
            PXDBCurrencyAttribute.CuryConvBase(cach2, info, curyval, out baseval);
          }
          catch (PXRateNotFoundException ex)
          {
            throw new PXRateIsNotDefinedForThisDateException(info);
          }
          Decimal num1 = 0M;
          Decimal num2 = baseval;
          if (!(num2 == 0M))
          {
            TranslationHistoryDetails translationHistoryDetails1 = new TranslationHistoryDetails();
            translationHistoryDetails1.LedgerID = ledger2.LedgerID;
            translationHistoryDetails1.BranchID = glHistory.BranchID;
            translationHistoryDetails1.AccountID = glHistory.AccountID;
            translationHistoryDetails1.SubID = glHistory.SubID;
            translationHistoryDetails1.FinPeriodID = companyFinPeriodId;
            translationHistoryDetails1.CalcMode = translDefDet.CalcMode;
            translationHistoryDetails1.SourceAmt = new Decimal?(curyval);
            translationHistoryDetails1.TranslatedAmt = new Decimal?(baseval);
            translationHistoryDetails1.OrigTranslatedAmt = new Decimal?(num1);
            translationHistoryDetails1.CuryID = info.BaseCuryID;
            translationHistoryDetails1.CuryEffDate = info.CuryEffDate;
            translationHistoryDetails1.RateTypeID = info.CuryRateTypeID;
            translationHistoryDetails1.CuryMultDiv = info.CuryMultDiv;
            translationHistoryDetails1.CuryRate = info.CuryRate;
            translationHistoryDetails1.LineType = "T";
            translationHistoryDetails1.LineNbr = new int?(0);
            translationHistoryDetails1.ReferenceNbr = translationHistory2.ReferenceNbr;
            if (account.Type == "A" || account.Type == "E")
            {
              translationHistoryDetails1.DebitAmt = new Decimal?((Decimal) ((1 + Math.Sign(num2)) / 2) * Math.Abs(num2));
              translationHistoryDetails1.CreditAmt = new Decimal?((Decimal) ((1 - Math.Sign(num2)) / 2) * Math.Abs(num2));
            }
            else
            {
              translationHistoryDetails1.DebitAmt = new Decimal?((Decimal) ((1 - Math.Sign(num2)) / 2) * Math.Abs(num2));
              translationHistoryDetails1.CreditAmt = new Decimal?((Decimal) ((1 + Math.Sign(num2)) / 2) * Math.Abs(num2));
            }
            TranslationHistoryDetails translationHistoryDetails2 = ((PXSelectBase<TranslationHistoryDetails>) instance.TranslHistDetRecords).Insert(translationHistoryDetails1);
            if (dictionary1.ContainsKey(translationHistoryDetails2.BranchID))
            {
              Dictionary<int?, Decimal?> dictionary2 = dictionary1;
              int? branchId = translationHistoryDetails2.BranchID;
              Dictionary<int?, Decimal?> dictionary3 = dictionary2;
              int? key = branchId;
              nullable3 = dictionary2[branchId];
              Decimal? debitAmt = translationHistoryDetails2.DebitAmt;
              Decimal? nullable4 = translationHistoryDetails2.CreditAmt;
              Decimal? nullable5 = debitAmt.HasValue & nullable4.HasValue ? new Decimal?(debitAmt.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable6;
              if (!(nullable3.HasValue & nullable5.HasValue))
              {
                nullable4 = new Decimal?();
                nullable6 = nullable4;
              }
              else
                nullable6 = new Decimal?(nullable3.GetValueOrDefault() + nullable5.GetValueOrDefault());
              dictionary3[key] = nullable6;
            }
            else
            {
              Dictionary<int?, Decimal?> dictionary4 = dictionary1;
              int? branchId = translationHistoryDetails2.BranchID;
              Decimal? debitAmt = translationHistoryDetails2.DebitAmt;
              nullable3 = translationHistoryDetails2.CreditAmt;
              Decimal? nullable7 = debitAmt.HasValue & nullable3.HasValue ? new Decimal?(debitAmt.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
              dictionary4[branchId] = nullable7;
            }
          }
        }
        foreach (PXResult<GLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLSetup, GLHistory> pxResult3 in pxSelectBase.Select(new object[27]
        {
          (object) parameters.DestLedgerId,
          (object) companyFinPeriodId,
          (object) currency.TranslationGainAcctID,
          (object) currency.TranslationLossAcctID,
          (object) (companyFinPeriodId.Substring(0, 4) + "01"),
          (object) acct1,
          (object) acct2,
          (object) acct1,
          (object) acct2,
          (object) acct1,
          (object) acct2,
          (object) str2,
          (object) str2,
          (object) str2,
          (object) acct2,
          (object) acct1,
          (object) str4,
          (object) str4,
          (object) str4,
          (object) acct1,
          (object) acct2,
          (object) str2,
          (object) str4,
          (object) str2,
          (object) str4,
          (object) str2,
          (object) str4
        }))
        {
          GLHistory glHistory = PXResult<GLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLSetup, GLHistory>.op_Implicit(pxResult3);
          PX.Objects.GL.Account account = PXResult<GLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLSetup, GLHistory>.op_Implicit(pxResult3);
          int? branchId1;
          if (parameters.BranchID.HasValue)
          {
            int? branchId2 = parameters.BranchID;
            branchId1 = glHistory.BranchID;
            if (!(branchId2.GetValueOrDefault() == branchId1.GetValueOrDefault() & branchId2.HasValue == branchId1.HasValue))
              continue;
          }
          Decimal num3 = 0M;
          Decimal num4 = 0M;
          Decimal num5 = 0M;
          Decimal num6 = num4;
          if (glHistory != null)
          {
            Decimal? finYtdBalance = glHistory.FinYtdBalance;
            if (finYtdBalance.HasValue)
            {
              Decimal num7 = num6;
              finYtdBalance = glHistory.FinYtdBalance;
              Decimal num8 = finYtdBalance.Value;
              num6 = num7 - num8;
              Decimal num9 = num5;
              finYtdBalance = glHistory.FinYtdBalance;
              Decimal num10 = finYtdBalance.Value;
              num5 = num9 + num10;
            }
          }
          TranslationHistoryDetails translationHistoryDetails3 = new TranslationHistoryDetails();
          translationHistoryDetails3.ReferenceNbr = translationHistory2.ReferenceNbr;
          translationHistoryDetails3.BranchID = glHistory.BranchID;
          translationHistoryDetails3.AccountID = glHistory.AccountID;
          translationHistoryDetails3.SubID = glHistory.SubID;
          translationHistoryDetails3.LineType = "T";
          translationHistoryDetails3.LedgerID = ledger2.LedgerID;
          translationHistoryDetails3.FinPeriodID = companyFinPeriodId;
          translationHistoryDetails3.CalcMode = translDefDet.CalcMode;
          translationHistoryDetails3.SourceAmt = new Decimal?(num3);
          translationHistoryDetails3.TranslatedAmt = new Decimal?(num4);
          translationHistoryDetails3.OrigTranslatedAmt = new Decimal?(num5);
          translationHistoryDetails3.CuryID = info.BaseCuryID;
          translationHistoryDetails3.CuryEffDate = info.CuryEffDate;
          translationHistoryDetails3.RateTypeID = info.CuryRateTypeID;
          translationHistoryDetails3.CuryMultDiv = info.CuryMultDiv;
          translationHistoryDetails3.CuryRate = info.CuryRate;
          translationHistoryDetails3.LineNbr = new int?(0);
          TranslationHistoryDetails translationHistoryDetails4;
          if ((translationHistoryDetails4 = ((PXSelectBase<TranslationHistoryDetails>) instance.TranslHistDetRecords).Locate(translationHistoryDetails3)) != null)
          {
            translationHistoryDetails3 = PXCache<TranslationHistoryDetails>.CreateCopy(translationHistoryDetails4);
            TranslationHistoryDetails translationHistoryDetails5 = translationHistoryDetails3;
            Decimal? origTranslatedAmt = translationHistoryDetails5.OrigTranslatedAmt;
            Decimal num11 = num5;
            translationHistoryDetails5.OrigTranslatedAmt = origTranslatedAmt.HasValue ? new Decimal?(origTranslatedAmt.GetValueOrDefault() + num11) : new Decimal?();
            if (account.Type == "A" || account.Type == "E")
            {
              Decimal num12 = num6;
              Decimal? nullable8 = translationHistoryDetails3.DebitAmt;
              Decimal num13 = nullable8.Value;
              nullable8 = translationHistoryDetails3.CreditAmt;
              Decimal num14 = nullable8.Value;
              Decimal num15 = num13 - num14;
              num6 = num12 + num15;
            }
            else
            {
              Decimal num16 = num6;
              Decimal? nullable9 = translationHistoryDetails3.DebitAmt;
              Decimal num17 = nullable9.Value;
              nullable9 = translationHistoryDetails3.CreditAmt;
              Decimal num18 = nullable9.Value;
              Decimal num19 = num17 - num18;
              num6 = num16 - num19;
            }
            Dictionary<int?, Decimal?> dictionary5 = dictionary1;
            branchId1 = translationHistoryDetails3.BranchID;
            Dictionary<int?, Decimal?> dictionary6 = dictionary5;
            int? key = branchId1;
            Decimal? nullable10 = dictionary5[branchId1];
            Decimal? debitAmt = translationHistoryDetails3.DebitAmt;
            Decimal? nullable11 = translationHistoryDetails3.CreditAmt;
            Decimal? nullable12 = debitAmt.HasValue & nullable11.HasValue ? new Decimal?(debitAmt.GetValueOrDefault() - nullable11.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable13;
            if (!(nullable10.HasValue & nullable12.HasValue))
            {
              nullable11 = new Decimal?();
              nullable13 = nullable11;
            }
            else
              nullable13 = new Decimal?(nullable10.GetValueOrDefault() - nullable12.GetValueOrDefault());
            dictionary6[key] = nullable13;
          }
          if (num6 == 0M)
          {
            if (translationHistoryDetails4 != null)
              ((PXSelectBase<TranslationHistoryDetails>) instance.TranslHistDetRecords).Delete(translationHistoryDetails3);
          }
          else
          {
            if (account.Type == "A" || account.Type == "E")
            {
              translationHistoryDetails3.DebitAmt = new Decimal?((Decimal) ((1 + Math.Sign(num6)) / 2) * Math.Abs(num6));
              translationHistoryDetails3.CreditAmt = new Decimal?((Decimal) ((1 - Math.Sign(num6)) / 2) * Math.Abs(num6));
            }
            else
            {
              translationHistoryDetails3.DebitAmt = new Decimal?((Decimal) ((1 - Math.Sign(num6)) / 2) * Math.Abs(num6));
              translationHistoryDetails3.CreditAmt = new Decimal?((Decimal) ((1 + Math.Sign(num6)) / 2) * Math.Abs(num6));
            }
            TranslationHistoryDetails translationHistoryDetails6 = translationHistoryDetails4 != null ? ((PXSelectBase<TranslationHistoryDetails>) instance.TranslHistDetRecords).Update(translationHistoryDetails3) : ((PXSelectBase<TranslationHistoryDetails>) instance.TranslHistDetRecords).Insert(translationHistoryDetails3);
            if (dictionary1.ContainsKey(translationHistoryDetails6.BranchID))
            {
              Dictionary<int?, Decimal?> dictionary7 = dictionary1;
              branchId1 = translationHistoryDetails6.BranchID;
              Dictionary<int?, Decimal?> dictionary8 = dictionary7;
              int? key = branchId1;
              Decimal? nullable14 = dictionary7[branchId1];
              Decimal? debitAmt = translationHistoryDetails6.DebitAmt;
              Decimal? nullable15 = translationHistoryDetails6.CreditAmt;
              Decimal? nullable16 = debitAmt.HasValue & nullable15.HasValue ? new Decimal?(debitAmt.GetValueOrDefault() - nullable15.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable17;
              if (!(nullable14.HasValue & nullable16.HasValue))
              {
                nullable15 = new Decimal?();
                nullable17 = nullable15;
              }
              else
                nullable17 = new Decimal?(nullable14.GetValueOrDefault() + nullable16.GetValueOrDefault());
              dictionary8[key] = nullable17;
            }
            else
            {
              Dictionary<int?, Decimal?> dictionary9 = dictionary1;
              int? branchId3 = translationHistoryDetails6.BranchID;
              Decimal? debitAmt = translationHistoryDetails6.DebitAmt;
              Decimal? creditAmt = translationHistoryDetails6.CreditAmt;
              Decimal? nullable18 = debitAmt.HasValue & creditAmt.HasValue ? new Decimal?(debitAmt.GetValueOrDefault() - creditAmt.GetValueOrDefault()) : new Decimal?();
              dictionary9[branchId3] = nullable18;
            }
          }
        }
      }
      else
      {
        PXSelectBase<GLHistory> pxSelectBase = (PXSelectBase<GLHistory>) new PXSelectJoin<GLHistory, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<GLHistory.accountID>>, InnerJoin<PX.Objects.GL.Sub, On<PX.Objects.GL.Sub.subID, Equal<GLHistory.subID>>, CrossJoin<GLSetup>>>, Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>, And<GLHistory.finPeriodID, Equal<Required<GLHistory.finPeriodID>>, And<GLHistory.accountID, NotEqual<GLSetup.ytdNetIncAccountID>, And<GLHistory.accountID, NotEqual<Required<GLHistory.accountID>>, And<GLHistory.accountID, NotEqual<Required<GLHistory.accountID>>, And<Where<PX.Objects.GL.Account.accountCD, Between<Required<PX.Objects.GL.Account.accountCD>, Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.GL.Account.accountCD, NotEqual<Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.GL.Account.accountCD, NotEqual<Required<PX.Objects.GL.Account.accountCD>>, Or<PX.Objects.GL.Account.accountCD, Equal<Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.GL.Account.accountCD, Less<Required<PX.Objects.GL.Account.accountCD>>, And2<Where<Required<PX.Objects.GL.Sub.subCD>, IsNull, Or<Required<PX.Objects.GL.Sub.subCD>, IsNotNull, And<PX.Objects.GL.Sub.subCD, GreaterEqual<Required<PX.Objects.GL.Sub.subCD>>>>>, Or<PX.Objects.GL.Account.accountCD, Equal<Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.GL.Account.accountCD, Greater<Required<PX.Objects.GL.Account.accountCD>>, And2<Where<Required<PX.Objects.GL.Sub.subCD>, IsNull, Or<Required<PX.Objects.GL.Sub.subCD>, IsNotNull, And<PX.Objects.GL.Sub.subCD, LessEqual<Required<PX.Objects.GL.Sub.subCD>>>>>, Or<PX.Objects.GL.Account.accountCD, Equal<Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.GL.Account.accountCD, Equal<Required<PX.Objects.GL.Account.accountCD>>, And<Where<Required<PX.Objects.GL.Sub.subCD>, IsNull, And<Required<PX.Objects.GL.Sub.subCD>, IsNull, Or<Required<PX.Objects.GL.Sub.subCD>, IsNotNull, And<Required<PX.Objects.GL.Sub.subCD>, IsNotNull, And<PX.Objects.GL.Sub.subCD, Between<Required<PX.Objects.GL.Sub.subCD>, Required<PX.Objects.GL.Sub.subCD>>>>>>>>>>>>>>>>>>>>>>>>>>((PXGraph) instance);
        foreach (PXResult<GLHistory, PX.Objects.GL.Account> pxResult4 in pxSelectBase.Select(new object[26]
        {
          (object) parameters.SourceLedgerId,
          (object) companyFinPeriodId,
          (object) currency.TranslationGainAcctID,
          (object) currency.TranslationLossAcctID,
          (object) acct1,
          (object) acct2,
          (object) acct1,
          (object) acct2,
          (object) acct1,
          (object) acct2,
          (object) str2,
          (object) str2,
          (object) str2,
          (object) acct2,
          (object) acct1,
          (object) str4,
          (object) str4,
          (object) str4,
          (object) acct1,
          (object) acct2,
          (object) str2,
          (object) str4,
          (object) str2,
          (object) str4,
          (object) str2,
          (object) str4
        }))
        {
          GLHistory glHistory = PXResult<GLHistory, PX.Objects.GL.Account>.op_Implicit(pxResult4);
          PXResult<GLHistory, PX.Objects.GL.Account>.op_Implicit(pxResult4);
          nullable1 = parameters.BranchID;
          if (nullable1.HasValue)
          {
            nullable1 = parameters.BranchID;
            int? branchId = glHistory.BranchID;
            if (!(nullable1.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable1.HasValue == branchId.HasValue))
              continue;
          }
          Decimal? finPtdDebit = glHistory.FinPtdDebit;
          Decimal? nullable19 = glHistory.FinPtdCredit;
          Decimal? nullable20 = finPtdDebit.HasValue & nullable19.HasValue ? new Decimal?(finPtdDebit.GetValueOrDefault() - nullable19.GetValueOrDefault()) : new Decimal?();
          Decimal curyval = nullable20.Value;
          Decimal baseval;
          try
          {
            PXDBCurrencyAttribute.CuryConvBase(cach2, info, curyval, out baseval);
          }
          catch (PXRateNotFoundException ex)
          {
            throw new PXRateIsNotDefinedForThisDateException(info);
          }
          Decimal num20 = 0M;
          Decimal num21 = baseval;
          if (!(num21 == 0M))
          {
            TranslationHistoryDetails translationHistoryDetails = ((PXSelectBase<TranslationHistoryDetails>) instance.TranslHistDetRecords).Insert(new TranslationHistoryDetails()
            {
              LedgerID = ledger2.LedgerID,
              BranchID = glHistory.BranchID,
              AccountID = glHistory.AccountID,
              SubID = glHistory.SubID,
              FinPeriodID = companyFinPeriodId,
              CalcMode = translDefDet.CalcMode,
              SourceAmt = new Decimal?(curyval),
              TranslatedAmt = new Decimal?(baseval),
              OrigTranslatedAmt = new Decimal?(num20),
              CuryID = info.BaseCuryID,
              CuryEffDate = info.CuryEffDate,
              RateTypeID = info.CuryRateTypeID,
              CuryMultDiv = info.CuryMultDiv,
              CuryRate = info.CuryRate,
              LineType = "T",
              LineNbr = new int?(0),
              ReferenceNbr = translationHistory2.ReferenceNbr,
              DebitAmt = new Decimal?((Decimal) ((1 + Math.Sign(num21)) / 2) * Math.Abs(num21)),
              CreditAmt = new Decimal?((Decimal) ((1 - Math.Sign(num21)) / 2) * Math.Abs(num21))
            });
            if (dictionary1.ContainsKey(translationHistoryDetails.BranchID))
            {
              Dictionary<int?, Decimal?> dictionary10 = dictionary1;
              int? branchId = translationHistoryDetails.BranchID;
              Dictionary<int?, Decimal?> dictionary11 = dictionary10;
              int? key = branchId;
              Decimal? nullable21 = dictionary10[branchId];
              nullable20 = translationHistoryDetails.DebitAmt;
              Decimal? nullable22 = translationHistoryDetails.CreditAmt;
              nullable19 = nullable20.HasValue & nullable22.HasValue ? new Decimal?(nullable20.GetValueOrDefault() - nullable22.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable23;
              if (!(nullable21.HasValue & nullable19.HasValue))
              {
                nullable22 = new Decimal?();
                nullable23 = nullable22;
              }
              else
                nullable23 = new Decimal?(nullable21.GetValueOrDefault() + nullable19.GetValueOrDefault());
              dictionary11[key] = nullable23;
            }
            else
            {
              Dictionary<int?, Decimal?> dictionary12 = dictionary1;
              int? branchId = translationHistoryDetails.BranchID;
              nullable19 = translationHistoryDetails.DebitAmt;
              Decimal? creditAmt = translationHistoryDetails.CreditAmt;
              Decimal? nullable24 = nullable19.HasValue & creditAmt.HasValue ? new Decimal?(nullable19.GetValueOrDefault() - creditAmt.GetValueOrDefault()) : new Decimal?();
              dictionary12[branchId] = nullable24;
            }
          }
        }
        foreach (PXResult<GLHistory, PX.Objects.GL.Account> pxResult5 in pxSelectBase.Select(new object[26]
        {
          (object) parameters.DestLedgerId,
          (object) companyFinPeriodId,
          (object) currency.TranslationGainAcctID,
          (object) currency.TranslationLossAcctID,
          (object) acct1,
          (object) acct2,
          (object) acct1,
          (object) acct2,
          (object) acct1,
          (object) acct2,
          (object) str2,
          (object) str2,
          (object) str2,
          (object) acct2,
          (object) acct1,
          (object) str4,
          (object) str4,
          (object) str4,
          (object) acct1,
          (object) acct2,
          (object) str2,
          (object) str4,
          (object) str2,
          (object) str4,
          (object) str2,
          (object) str4
        }))
        {
          GLHistory glHistory = PXResult<GLHistory, PX.Objects.GL.Account>.op_Implicit(pxResult5);
          PXResult<GLHistory, PX.Objects.GL.Account>.op_Implicit(pxResult5);
          int? branchId4;
          if (parameters.BranchID.HasValue)
          {
            int? branchId5 = parameters.BranchID;
            branchId4 = glHistory.BranchID;
            if (!(branchId5.GetValueOrDefault() == branchId4.GetValueOrDefault() & branchId5.HasValue == branchId4.HasValue))
              continue;
          }
          Decimal num22 = 0M;
          Decimal num23 = 0M;
          Decimal num24 = 0M;
          Decimal num25 = num23;
          if (glHistory != null)
          {
            Decimal num26 = num25;
            Decimal? finPtdDebit1 = glHistory.FinPtdDebit;
            Decimal? finPtdCredit = glHistory.FinPtdCredit;
            Decimal? nullable25 = finPtdDebit1.HasValue & finPtdCredit.HasValue ? new Decimal?(finPtdDebit1.GetValueOrDefault() - finPtdCredit.GetValueOrDefault()) : new Decimal?();
            Decimal num27 = nullable25.Value;
            num25 = num26 - num27;
            Decimal num28 = num24;
            Decimal? finPtdDebit2 = glHistory.FinPtdDebit;
            finPtdCredit = glHistory.FinPtdCredit;
            Decimal? nullable26;
            if (!(finPtdDebit2.HasValue & finPtdCredit.HasValue))
            {
              nullable25 = new Decimal?();
              nullable26 = nullable25;
            }
            else
              nullable26 = new Decimal?(finPtdDebit2.GetValueOrDefault() - finPtdCredit.GetValueOrDefault());
            nullable25 = nullable26;
            Decimal num29 = nullable25.Value;
            num24 = num28 + num29;
          }
          TranslationHistoryDetails translationHistoryDetails7 = new TranslationHistoryDetails();
          translationHistoryDetails7.ReferenceNbr = translationHistory2.ReferenceNbr;
          translationHistoryDetails7.BranchID = glHistory.BranchID;
          translationHistoryDetails7.AccountID = glHistory.AccountID;
          translationHistoryDetails7.SubID = glHistory.SubID;
          translationHistoryDetails7.LineType = "T";
          translationHistoryDetails7.LedgerID = ledger2.LedgerID;
          translationHistoryDetails7.FinPeriodID = companyFinPeriodId;
          translationHistoryDetails7.CalcMode = translDefDet.CalcMode;
          translationHistoryDetails7.SourceAmt = new Decimal?(num22);
          translationHistoryDetails7.TranslatedAmt = new Decimal?(num23);
          translationHistoryDetails7.OrigTranslatedAmt = new Decimal?(num24);
          translationHistoryDetails7.CuryID = info.BaseCuryID;
          translationHistoryDetails7.CuryEffDate = info.CuryEffDate;
          translationHistoryDetails7.RateTypeID = info.CuryRateTypeID;
          translationHistoryDetails7.CuryMultDiv = info.CuryMultDiv;
          translationHistoryDetails7.CuryRate = info.CuryRate;
          translationHistoryDetails7.LineNbr = new int?(0);
          TranslationHistoryDetails translationHistoryDetails8;
          if ((translationHistoryDetails8 = ((PXSelectBase<TranslationHistoryDetails>) instance.TranslHistDetRecords).Locate(translationHistoryDetails7)) != null)
          {
            translationHistoryDetails7 = PXCache<TranslationHistoryDetails>.CreateCopy(translationHistoryDetails8);
            TranslationHistoryDetails translationHistoryDetails9 = translationHistoryDetails7;
            Decimal? origTranslatedAmt = translationHistoryDetails9.OrigTranslatedAmt;
            Decimal num30 = num24;
            translationHistoryDetails9.OrigTranslatedAmt = origTranslatedAmt.HasValue ? new Decimal?(origTranslatedAmt.GetValueOrDefault() + num30) : new Decimal?();
            Decimal num31 = num25;
            Decimal? nullable27 = translationHistoryDetails7.DebitAmt;
            Decimal num32 = nullable27.Value;
            nullable27 = translationHistoryDetails7.CreditAmt;
            Decimal num33 = nullable27.Value;
            Decimal num34 = num32 - num33;
            num25 = num31 + num34;
            Dictionary<int?, Decimal?> dictionary13 = dictionary1;
            branchId4 = translationHistoryDetails7.BranchID;
            Dictionary<int?, Decimal?> dictionary14 = dictionary13;
            int? key = branchId4;
            Decimal? nullable28 = dictionary13[branchId4];
            Decimal? debitAmt = translationHistoryDetails7.DebitAmt;
            Decimal? nullable29 = translationHistoryDetails7.CreditAmt;
            Decimal? nullable30 = debitAmt.HasValue & nullable29.HasValue ? new Decimal?(debitAmt.GetValueOrDefault() - nullable29.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable31;
            if (!(nullable28.HasValue & nullable30.HasValue))
            {
              nullable29 = new Decimal?();
              nullable31 = nullable29;
            }
            else
              nullable31 = new Decimal?(nullable28.GetValueOrDefault() - nullable30.GetValueOrDefault());
            dictionary14[key] = nullable31;
          }
          if (num25 == 0M)
          {
            if (translationHistoryDetails8 != null)
              ((PXSelectBase<TranslationHistoryDetails>) instance.TranslHistDetRecords).Delete(translationHistoryDetails7);
          }
          else
          {
            translationHistoryDetails7.DebitAmt = new Decimal?((Decimal) ((1 + Math.Sign(num25)) / 2) * Math.Abs(num25));
            translationHistoryDetails7.CreditAmt = new Decimal?((Decimal) ((1 - Math.Sign(num25)) / 2) * Math.Abs(num25));
            TranslationHistoryDetails translationHistoryDetails10 = translationHistoryDetails8 != null ? ((PXSelectBase<TranslationHistoryDetails>) instance.TranslHistDetRecords).Update(translationHistoryDetails7) : ((PXSelectBase<TranslationHistoryDetails>) instance.TranslHistDetRecords).Insert(translationHistoryDetails7);
            if (dictionary1.ContainsKey(translationHistoryDetails10.BranchID))
            {
              Dictionary<int?, Decimal?> dictionary15 = dictionary1;
              branchId4 = translationHistoryDetails10.BranchID;
              Dictionary<int?, Decimal?> dictionary16 = dictionary15;
              int? key = branchId4;
              Decimal? nullable32 = dictionary15[branchId4];
              Decimal? debitAmt = translationHistoryDetails10.DebitAmt;
              Decimal? nullable33 = translationHistoryDetails10.CreditAmt;
              Decimal? nullable34 = debitAmt.HasValue & nullable33.HasValue ? new Decimal?(debitAmt.GetValueOrDefault() - nullable33.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable35;
              if (!(nullable32.HasValue & nullable34.HasValue))
              {
                nullable33 = new Decimal?();
                nullable35 = nullable33;
              }
              else
                nullable35 = new Decimal?(nullable32.GetValueOrDefault() + nullable34.GetValueOrDefault());
              dictionary16[key] = nullable35;
            }
            else
            {
              Dictionary<int?, Decimal?> dictionary17 = dictionary1;
              int? branchId6 = translationHistoryDetails10.BranchID;
              Decimal? debitAmt = translationHistoryDetails10.DebitAmt;
              Decimal? creditAmt = translationHistoryDetails10.CreditAmt;
              Decimal? nullable36 = debitAmt.HasValue & creditAmt.HasValue ? new Decimal?(debitAmt.GetValueOrDefault() - creditAmt.GetValueOrDefault()) : new Decimal?();
              dictionary17[branchId6] = nullable36;
            }
          }
        }
      }
    }
    foreach (KeyValuePair<int?, Decimal?> keyValuePair in dictionary1)
    {
      Decimal? nullable37 = keyValuePair.Value;
      if (Math.Abs(nullable37.Value) > 0M)
      {
        TranslationHistoryDetails translationHistoryDetails11 = new TranslationHistoryDetails();
        PXProcessing.SetCurrentItem((object) translationHistoryDetails11);
        nullable37 = keyValuePair.Value;
        if (Math.Sign(nullable37.Value) == 1)
        {
          translationHistoryDetails11.AccountID = currency.TranslationGainAcctID;
          translationHistoryDetails11.SubID = GainLossSubAccountMaskAttribute.GetSubID<Currency.translationGainSubID>((PXGraph) instance, keyValuePair.Key, currency);
        }
        else
        {
          translationHistoryDetails11.AccountID = currency.TranslationLossAcctID;
          translationHistoryDetails11.SubID = GainLossSubAccountMaskAttribute.GetSubID<Currency.translationLossSubID>((PXGraph) instance, keyValuePair.Key, currency);
        }
        translationHistoryDetails11.BranchID = keyValuePair.Key;
        translationHistoryDetails11.FinPeriodID = companyFinPeriodId;
        translationHistoryDetails11.CalcMode = new short?((short) 0);
        translationHistoryDetails11.CuryID = ledger2.BaseCuryID;
        translationHistoryDetails11.CuryEffDate = parameters.CuryEffDate;
        translationHistoryDetails11.CuryRate = new Decimal?(1M);
        translationHistoryDetails11.RateTypeID = cmSetup.GLRateTypeDflt;
        translationHistoryDetails11.LineNbr = new int?(0);
        translationHistoryDetails11.LineType = "G";
        TranslationHistoryDetails translationHistoryDetails12 = translationHistoryDetails11;
        nullable37 = keyValuePair.Value;
        Decimal num35 = (Decimal) ((1 - Math.Sign(nullable37.Value)) / 2);
        nullable37 = keyValuePair.Value;
        Decimal num36 = Math.Abs(nullable37.Value);
        Decimal? nullable38 = new Decimal?(num35 * num36);
        translationHistoryDetails12.DebitAmt = nullable38;
        TranslationHistoryDetails translationHistoryDetails13 = translationHistoryDetails11;
        nullable37 = keyValuePair.Value;
        Decimal num37 = (Decimal) ((1 + Math.Sign(nullable37.Value)) / 2);
        nullable37 = keyValuePair.Value;
        Decimal num38 = Math.Abs(nullable37.Value);
        Decimal? nullable39 = new Decimal?(num37 * num38);
        translationHistoryDetails13.CreditAmt = nullable39;
        translationHistoryDetails11.ReferenceNbr = translationHistory2.ReferenceNbr;
        ((PXSelectBase<TranslationHistoryDetails>) instance.TranslHistDetRecords).Insert(translationHistoryDetails11);
        PXProcessing.SetProcessed();
      }
    }
    bool flag = false;
    try
    {
      if (!((PXSelectBase) instance.TranslHistDetRecords).Cache.IsInsertedUpdatedDeleted)
        throw new PXException("No new transactions have been generated by the translation process.");
      translationHistory2.ControlTot = translationHistory2.CreditTot;
      ((PXSelectBase<TranslationHistory>) instance.TranslHistRecords).Update(translationHistory2);
      ((PXAction) instance.Save).Press();
      flag = true;
    }
    catch (Exception ex)
    {
      PXProcessing.SetError(ex);
    }
    if (!flag)
      return;
    using (new PXTimeStampScope((byte[]) null))
    {
      ((PXGraph) instance).Clear();
      ((PXSelectBase<TranslationHistory>) instance.TranslHistRecords).Current = PXResultset<TranslationHistory>.op_Implicit(PXSelectBase<TranslationHistory, PXSelect<TranslationHistory, Where<TranslationHistory.referenceNbr, Equal<Required<TranslationHistory.referenceNbr>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) translationHistory2.ReferenceNbr
      }));
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Translation Record");
    }
  }

  public TranslationProcess()
  {
    ((PXProcessing<CurrencyRate>) this.TranslationCurrencyRateRecords).SetProcessVisible(false);
    ((PXProcessing<CurrencyRate>) this.TranslationCurrencyRateRecords).SetProcessEnabled(false);
    ((PXProcessing<CurrencyRate>) this.TranslationCurrencyRateRecords).SetProcessAllCaption("Create Translation");
    PX.Objects.CM.CMSetup current = ((PXSelectBase<PX.Objects.CM.CMSetup>) this.CMSetup).Current;
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      throw new Exception("Multi-Currency is not activated");
  }

  protected virtual void TranslationParams_TranslDefID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    TranslationProcess.TranslationParams row = (TranslationProcess.TranslationParams) e.Row;
    if (row != null && row.TranslDefId != null)
      cache.SetDefaultExt<TranslationProcess.TranslationParams.sourceLedgerId>((object) row);
    cache.SetDefaultExt<TranslationProcess.TranslationParams.destLedgerId>((object) row);
    cache.SetDefaultExt<TranslationProcess.TranslationParams.sourceCuryID>((object) row);
    cache.SetDefaultExt<TranslationProcess.TranslationParams.destCuryID>((object) row);
    cache.SetDefaultExt<TranslationProcess.TranslationParams.description>((object) row);
    cache.SetDefaultExt<TranslationProcess.TranslationParams.branchID>((object) row);
    cache.SetDefaultExt<TranslationProcess.TranslationParams.finPeriodID>((object) row);
    cache.SetDefaultExt<TranslationProcess.TranslationParams.lastFinPeriodID>((object) row);
  }

  protected virtual void TranslationParams_CuryEffDate_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    TranslationProcess.TranslationParams row = (TranslationProcess.TranslationParams) e.Row;
    if (row == null)
      return;
    bool flag = !row.BranchID.HasValue;
    FinPeriod byId = this.FinPeriodRepository.FindByID(this.FinPeriodRepository.GetCalendarOrganizationID(row.BranchID, new bool?(flag)), row.FinPeriodID);
    if (byId == null)
      return;
    DateTime? endDate = byId.EndDate;
    if (!endDate.HasValue)
      return;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    endDate = byId.EndDate;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime> local = (ValueType) endDate.Value.AddDays(-1.0);
    defaultingEventArgs.NewValue = (object) local;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void TranslationParams_LastFinPeriodID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    TranslationProcess.TranslationParams row = (TranslationProcess.TranslationParams) e.Row;
    if (row == null || row.TranslDefId == null)
      return;
    FinPeriod finPeriod = PXResultset<FinPeriod>.op_Implicit(PXSelectBase<FinPeriod, PXSelectJoin<FinPeriod, InnerJoin<PX.Objects.GL.Branch, On<FinPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>, InnerJoin<TranslationHistory, On<TranslationHistory.finPeriodID, Equal<FinPeriod.finPeriodID>, And<TranslationHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>>>>, Where<TranslationHistory.translDefId, Equal<Required<TranslationProcess.TranslationParams.translDefId>>>, OrderBy<Desc<FinPeriod.finPeriodID>>>.Config>.SelectSingleBound(cache.Graph, new object[0], new object[1]
    {
      (object) row.TranslDefId
    }));
    if (finPeriod == null)
      return;
    e.NewValue = (object) finPeriod.FinPeriodID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void TranslationParams_FinPeriodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<TranslationProcess.TranslationParams.curyEffDate>(e.Row);
  }

  protected virtual void TranslationParams_FinPeriodID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  [Obsolete("This handler is not used anymore and will be removed in Acumatica ERP 8.0.")]
  protected virtual void TranslationParams_CuryEffDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
  }

  protected virtual void VerifyCurrencyEffectiveDate(
    PXCache sender,
    TranslationProcess.TranslationParams translationParameters)
  {
    DateTime? nullable;
    int num1;
    if (translationParameters == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = translationParameters.CuryEffDate;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      return;
    nullable = translationParameters.CuryEffDate;
    DateTime dateTime1 = nullable.Value;
    bool flag1 = !translationParameters.BranchID.HasValue;
    FinPeriod byId = this.FinPeriodRepository.FindByID(this.FinPeriodRepository.GetCalendarOrganizationID(translationParameters.BranchID, new bool?(flag1)), translationParameters.FinPeriodID);
    if (byId == null)
      return;
    bool? isAdjustment = byId.IsAdjustment;
    int num2;
    if (isAdjustment.GetValueOrDefault())
    {
      DateTime dateTime2 = dateTime1;
      nullable = byId.EndDate;
      DateTime dateTime3 = nullable.Value.AddDays(-1.0);
      if (dateTime2 == dateTime3)
      {
        num2 = 1;
        goto label_14;
      }
    }
    isAdjustment = byId.IsAdjustment;
    if (!isAdjustment.GetValueOrDefault())
    {
      DateTime dateTime4 = dateTime1;
      nullable = byId.StartDate;
      if ((nullable.HasValue ? (dateTime4 >= nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        DateTime dateTime5 = dateTime1;
        nullable = byId.EndDate;
        num2 = nullable.HasValue ? (dateTime5 < nullable.GetValueOrDefault() ? 1 : 0) : 0;
        goto label_14;
      }
    }
    num2 = 0;
label_14:
    bool flag2 = num2 != 0;
    if (byId == null || flag2)
      return;
    sender.RaiseExceptionHandling<TranslationProcess.TranslationParams.curyEffDate>((object) translationParameters, (object) dateTime1, (Exception) new PXSetPropertyException("Currency rate effective date is outside the specified financial period."));
  }

  protected virtual void VerifyFinancialPeriodID(PXCache sender, int? branchID, string finPeriodID)
  {
    bool flag = !branchID.HasValue;
    if (flag)
      return;
    FinPeriod byId = this.FinPeriodRepository.FindByID(this.FinPeriodRepository.GetCalendarOrganizationID(new int?(), branchID, new bool?(flag)), finPeriodID);
    if (byId == null)
      return;
    ProcessingResult period = this.FinPeriodUtils.CanPostToPeriod((IFinPeriod) byId);
    if (period.HasWarningOrError)
      throw new PXSetPropertyException(period.GeneralMessage);
  }

  protected virtual void VerifyFinancialPeriodID(
    PXCache sender,
    TranslationProcess.TranslationParams translationParameters)
  {
    try
    {
      this.VerifyFinancialPeriodID(sender, translationParameters.BranchID, translationParameters.FinPeriodID);
    }
    catch (PXException ex)
    {
      sender.RaiseExceptionHandling<TranslationProcess.TranslationParams.finPeriodID>((object) translationParameters, (object) FinPeriodIDFormattingAttribute.FormatForDisplay(translationParameters.FinPeriodID), (Exception) ex);
    }
  }

  protected virtual void TranslDef_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    using (new PXConnectionScope())
    {
      TranslDef row = (TranslDef) e.Row;
      if (row == null)
        return;
      if (row.SourceLedgerId.HasValue)
      {
        PX.Objects.GL.Ledger ledger = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelectReadonly<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Required<PX.Objects.GL.Ledger.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.SourceLedgerId
        }));
        row.SourceCuryID = ledger?.BaseCuryID;
      }
      if (!row.DestLedgerId.HasValue)
        return;
      PX.Objects.GL.Ledger ledger1 = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelectReadonly<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Required<PX.Objects.GL.Ledger.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.DestLedgerId
      }));
      row.DestCuryID = ledger1?.BaseCuryID;
    }
  }

  [Obsolete("This handler is not used anymore and will be removed in Acumatica ERP 8.0.")]
  protected virtual void TranslationParams_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
  }

  protected virtual void TranslationParams_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    TranslationProcess.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new TranslationProcess.\u003C\u003Ec__DisplayClass31_0();
    object row = e.Row;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass310.translationParameters = row as TranslationProcess.TranslationParams;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass310.translationParameters == null)
      return;
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetError<TranslationProcess.TranslationParams.curyEffDate>(sender, (object) cDisplayClass310.translationParameters, (string) null);
    // ISSUE: reference to a compiler-generated field
    this.VerifyCurrencyEffectiveDate(sender, cDisplayClass310.translationParameters);
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetError<TranslationProcess.TranslationParams.finPeriodID>(sender, (object) cDisplayClass310.translationParameters, (string) null);
    // ISSUE: reference to a compiler-generated field
    this.VerifyFinancialPeriodID(sender, cDisplayClass310.translationParameters);
    // ISSUE: method pointer
    ((PXProcessingBase<CurrencyRate>) this.TranslationCurrencyRateRecords).SetProcessDelegate(new PXProcessingBase<CurrencyRate>.ProcessListDelegate((object) cDisplayClass310, __methodptr(\u003CTranslationParams_RowSelected\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ((PXProcessing<CurrencyRate>) this.TranslationCurrencyRateRecords).SetProcessAllEnabled(!PXUIFieldAttribute.GetErrors(sender, (object) cDisplayClass310.translationParameters, Array.Empty<PXErrorLevel>()).Any<KeyValuePair<string, string>>() && cDisplayClass310.translationParameters.TranslDefId != null && cDisplayClass310.translationParameters.FinPeriodID != null);
  }

  protected virtual void CurrencyRate_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CurrencyRate row = (CurrencyRate) e.Row;
    if (row == null)
      return;
    cache.AllowDelete = false;
    cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CurrencyRate.curyRate>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<CurrencyRate.curyMultDiv>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<CurrencyRate.rateReciprocal>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<CurrencyRate.selected>(cache, (object) row, true);
  }

  protected virtual void CurrencyRate_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  [Serializable]
  public class TranslationParams : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _TranslDefId;
    protected string _Description;
    protected int? _SourceLedgerId;
    protected int? _DestLedgerId;
    protected string _FinPeriodID;
    protected DateTime? _CuryEffDate;
    protected string _SourceCuryID;
    protected string _DestCuryID;
    protected string _LastFinPeriodID;
    protected int? _BranchID;

    [PXString(10, IsUnicode = true)]
    [PXDefault(typeof (Search2<PX.Objects.CM.CMSetup.translDefId, LeftJoin<TranslDef, On<PX.Objects.CM.CMSetup.translDefId, Equal<TranslDef.translDefId>>>, Where<TranslDef.active, Equal<True>>>))]
    [PXUIField(DisplayName = "Translation ID", Required = true)]
    [PXSelector(typeof (Search<TranslDef.translDefId, Where<TranslDef.active, Equal<True>>>), DescriptionField = typeof (TranslDef.description))]
    public virtual string TranslDefId
    {
      get => this._TranslDefId;
      set => this._TranslDefId = value;
    }

    [PXString(60, IsUnicode = true)]
    [PXDefault(typeof (Search<TranslDef.description, Where<TranslDef.translDefId, Equal<Current<TranslationProcess.TranslationParams.translDefId>>>>))]
    [PXUIField]
    public virtual string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    [PXInt]
    [PXDefault(typeof (Search<TranslDef.sourceLedgerId, Where<TranslDef.translDefId, Equal<Current<TranslationProcess.TranslationParams.translDefId>>>>))]
    [PXUIField]
    [PXSelector(typeof (PX.Objects.GL.Ledger.ledgerID), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD), DescriptionField = typeof (PX.Objects.GL.Ledger.descr), CacheGlobal = true)]
    public virtual int? SourceLedgerId
    {
      get => this._SourceLedgerId;
      set => this._SourceLedgerId = value;
    }

    [PXInt]
    [PXDefault(typeof (Search<TranslDef.destLedgerId, Where<TranslDef.translDefId, Equal<Current<TranslationProcess.TranslationParams.translDefId>>>>))]
    [PXUIField]
    [PXSelector(typeof (PX.Objects.GL.Ledger.ledgerID), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD), DescriptionField = typeof (PX.Objects.GL.Ledger.descr), CacheGlobal = true)]
    public virtual int? DestLedgerId
    {
      get => this._DestLedgerId;
      set => this._DestLedgerId = value;
    }

    [ClosedPeriod(null, typeof (AccessInfo.businessDate), typeof (TranslationProcess.TranslationParams.branchID), null, null, null, null, false, true)]
    [PXUIField]
    public virtual string FinPeriodID
    {
      get => this._FinPeriodID;
      set => this._FinPeriodID = value;
    }

    [PXDate]
    [PXUIField(DisplayName = "Currency Effective Date")]
    public virtual DateTime? CuryEffDate
    {
      get => this._CuryEffDate;
      set => this._CuryEffDate = value;
    }

    [PXString(5, IsUnicode = true)]
    [PXUIField]
    [PXDefault(typeof (Search2<PX.Objects.GL.Ledger.baseCuryID, InnerJoin<TranslDef, On<PX.Objects.GL.Ledger.ledgerID, Equal<TranslDef.sourceLedgerId>>>, Where<TranslDef.translDefId, Equal<Current<TranslationProcess.TranslationParams.translDefId>>>>))]
    public virtual string SourceCuryID
    {
      get => this._SourceCuryID;
      set => this._SourceCuryID = value;
    }

    [PXString(5, IsUnicode = true)]
    [PXDefault(typeof (Search2<PX.Objects.GL.Ledger.baseCuryID, InnerJoin<TranslDef, On<PX.Objects.GL.Ledger.ledgerID, Equal<TranslDef.destLedgerId>>>, Where<TranslDef.translDefId, Equal<Current<TranslationProcess.TranslationParams.translDefId>>>>))]
    [PXUIField]
    public virtual string DestCuryID
    {
      get => this._DestCuryID;
      set => this._DestCuryID = value;
    }

    [PXString(6, IsFixed = true)]
    [FinPeriodIDFormatting]
    [PXUIField]
    public virtual string LastFinPeriodID
    {
      get => this._LastFinPeriodID;
      set => this._LastFinPeriodID = value;
    }

    [PXInt]
    [PXDefault(typeof (Search<TranslDef.branchID, Where<TranslDef.translDefId, Equal<Current<TranslationProcess.TranslationParams.translDefId>>>>))]
    [PXUIField]
    [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    public abstract class translDefId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TranslationProcess.TranslationParams.translDefId>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TranslationProcess.TranslationParams.description>
    {
    }

    public abstract class sourceLedgerId : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TranslationProcess.TranslationParams.sourceLedgerId>
    {
    }

    public abstract class destLedgerId : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TranslationProcess.TranslationParams.destLedgerId>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TranslationProcess.TranslationParams.finPeriodID>
    {
    }

    public abstract class curyEffDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      TranslationProcess.TranslationParams.curyEffDate>
    {
    }

    public abstract class sourceCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TranslationProcess.TranslationParams.sourceCuryID>
    {
    }

    public abstract class destCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TranslationProcess.TranslationParams.destCuryID>
    {
    }

    public abstract class lastFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TranslationProcess.TranslationParams.lastFinPeriodID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TranslationProcess.TranslationParams.branchID>
    {
    }
  }
}
