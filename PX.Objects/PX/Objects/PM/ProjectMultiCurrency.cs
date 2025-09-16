// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectMultiCurrency
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class ProjectMultiCurrency : IProjectMultiCurrency
{
  protected Dictionary<ProjectMultiCurrency.CurrencyInfoKey, PX.Objects.CM.Extensions.CurrencyInfo> directRates = new Dictionary<ProjectMultiCurrency.CurrencyInfoKey, PX.Objects.CM.Extensions.CurrencyInfo>();
  protected Dictionary<ProjectMultiCurrency.CurrencyInfoKey, PX.Objects.CM.Extensions.CurrencyInfo> rates = new Dictionary<ProjectMultiCurrency.CurrencyInfoKey, PX.Objects.CM.Extensions.CurrencyInfo>();

  public virtual void CalculateCurrencyValues(
    PXGraph graph,
    GLTran tran,
    PMTran pmt,
    Batch batch,
    PMProject project,
    PX.Objects.GL.Ledger ledger)
  {
    Decimal? curyDebitAmt = tran.CuryDebitAmt;
    Decimal? nullable1 = tran.CuryCreditAmt;
    Decimal? nullable2 = curyDebitAmt.HasValue & nullable1.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    PMTran pmTran1 = (PMTran) null;
    if (batch.AutoReverseCopy.GetValueOrDefault())
    {
      GLTran glTran = GLTran.PK.Find(graph, tran.OrigModule, tran.OrigBatchNbr, tran.OrigLineNbr);
      if (glTran != null)
        pmTran1 = PMTran.PK.Find(graph, glTran.PMTranID);
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>())
    {
      pmt.TranCuryID = batch.CuryID;
      pmt.ProjectCuryID = project.CuryID;
      pmt.TranCuryAmount = nullable2;
      if (batch.CuryID == project.BaseCuryID)
      {
        if (pmTran1 != null)
        {
          pmt.BaseCuryInfoID = pmTran1.BaseCuryInfoID;
        }
        else
        {
          PX.Objects.CM.Extensions.CurrencyInfo directRate = this.CreateDirectRate(graph, project.BaseCuryID, tran.TranDate, "PM");
          pmt.BaseCuryInfoID = directRate.CuryInfoID;
        }
        pmt.Amount = nullable2;
      }
      else if (pmTran1 != null)
      {
        pmt.BaseCuryInfoID = pmTran1.BaseCuryInfoID;
        pmt.Amount = new Decimal?(MultiCurrencyCalculator.GetCurrencyInfo<PMTran.baseCuryInfoID>(graph, (object) pmt).CuryConvBase(nullable2.GetValueOrDefault()));
      }
      else
      {
        PX.Objects.CM.Extensions.CurrencyInfo rate = this.CreateRate(graph, pmt.TranCuryID, project.BaseCuryID, tran.TranDate, project.RateTypeID, "PM");
        pmt.BaseCuryInfoID = rate.CuryInfoID;
        pmt.Amount = new Decimal?(rate.CuryConvBase(nullable2.GetValueOrDefault()));
      }
      if (project.CuryID == batch.CuryID)
      {
        if (pmTran1 != null)
        {
          pmt.ProjectCuryInfoID = pmTran1.ProjectCuryInfoID;
        }
        else
        {
          PX.Objects.CM.Extensions.CurrencyInfo directRate = this.CreateDirectRate(graph, project.CuryID, tran.TranDate, "PM");
          pmt.ProjectCuryInfoID = directRate.CuryInfoID;
        }
        pmt.ProjectCuryAmount = pmt.TranCuryAmount;
      }
      else if (pmTran1 != null)
      {
        pmt.ProjectCuryInfoID = pmTran1.ProjectCuryInfoID;
        PMTran pmTran2 = pmt;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = MultiCurrencyCalculator.GetCurrencyInfo<PMTran.projectCuryInfoID>(graph, (object) pmt);
        nullable1 = pmt.TranCuryAmount;
        Decimal valueOrDefault = nullable1.GetValueOrDefault();
        Decimal? nullable3 = new Decimal?(currencyInfo.CuryConvBase(valueOrDefault));
        pmTran2.ProjectCuryAmount = nullable3;
      }
      else
      {
        PX.Objects.CM.Extensions.CurrencyInfo rate = this.CreateRate(graph, pmt.TranCuryID, project.CuryID, tran.TranDate, project.RateTypeID, "PM");
        pmt.ProjectCuryInfoID = rate.CuryInfoID;
        PMTran pmTran3 = pmt;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = rate;
        nullable1 = pmt.TranCuryAmount;
        Decimal valueOrDefault = nullable1.GetValueOrDefault();
        Decimal? nullable4 = new Decimal?(currencyInfo.CuryConvBase(valueOrDefault));
        pmTran3.ProjectCuryAmount = nullable4;
      }
    }
    else
    {
      nullable1 = tran.DebitAmt;
      Decimal? creditAmt = tran.CreditAmt;
      Decimal? nullable5 = nullable1.HasValue & creditAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - creditAmt.GetValueOrDefault()) : new Decimal?();
      pmt.TranCuryID = project.BaseCuryID;
      pmt.ProjectCuryID = project.BaseCuryID;
      if (ledger.BaseCuryID == project.BaseCuryID)
      {
        pmt.Amount = nullable5;
        if (batch.CuryID == project.BaseCuryID)
        {
          pmt.ProjectCuryInfoID = tran.CuryInfoID;
          pmt.BaseCuryInfoID = tran.CuryInfoID;
        }
        else if (pmTran1 != null)
        {
          pmt.ProjectCuryInfoID = pmTran1.ProjectCuryInfoID;
          pmt.BaseCuryInfoID = pmTran1.BaseCuryInfoID;
        }
        else
        {
          PX.Objects.CM.Extensions.CurrencyInfo directRate = this.CreateDirectRate(graph, project.BaseCuryID, tran.TranDate, "PM");
          pmt.ProjectCuryInfoID = directRate.CuryInfoID;
          pmt.BaseCuryInfoID = directRate.CuryInfoID;
        }
      }
      else
      {
        if (batch.CuryID == project.BaseCuryID)
        {
          pmt.Amount = nullable5;
        }
        else
        {
          PX.Objects.CM.Extensions.CurrencyInfo rate = this.CreateRate(graph, batch.CuryID, project.BaseCuryID, tran.TranDate, project.RateTypeID, "PM");
          pmt.Amount = new Decimal?(rate.CuryConvBase(nullable2.GetValueOrDefault()));
        }
        if (pmTran1 != null)
        {
          pmt.ProjectCuryInfoID = pmTran1.ProjectCuryInfoID;
          pmt.BaseCuryInfoID = pmTran1.BaseCuryInfoID;
        }
        else
        {
          PX.Objects.CM.Extensions.CurrencyInfo directRate = this.CreateDirectRate(graph, project.BaseCuryID, tran.TranDate, "PM");
          pmt.ProjectCuryInfoID = directRate.CuryInfoID;
          pmt.BaseCuryInfoID = directRate.CuryInfoID;
        }
      }
      pmt.TranCuryAmount = pmt.Amount;
      pmt.ProjectCuryAmount = pmt.Amount;
    }
  }

  public virtual PX.Objects.CM.Extensions.CurrencyInfo CreateDirectRate(
    PXGraph graph,
    string curyID,
    DateTime? date,
    string module)
  {
    ProjectMultiCurrency.CurrencyInfoKey key = new ProjectMultiCurrency.CurrencyInfoKey(curyID, date.GetValueOrDefault());
    PX.Objects.CM.Extensions.CurrencyInfo directRate;
    if (!this.directRates.TryGetValue(key, out directRate))
    {
      directRate = new ProjectMultiCurrency.CurrencyInfoInsertingAdapter(graph).Insert(new PX.Objects.CM.Extensions.CurrencyInfo()
      {
        ModuleCode = module,
        BaseCuryID = curyID,
        CuryID = curyID,
        CuryRateTypeID = (string) null,
        CuryEffDate = date,
        CuryRate = new Decimal?((Decimal) 1),
        RecipRate = new Decimal?((Decimal) 1)
      });
      this.directRates.Add(key, directRate);
    }
    return directRate;
  }

  public virtual PX.Objects.CM.Extensions.CurrencyInfo CreateRate(
    PXGraph graph,
    string curyID,
    string baseCuryID,
    DateTime? date,
    string rateTypeID,
    string module)
  {
    IPXCurrencyService pxCurrencyService = ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(graph);
    ProjectMultiCurrency.CurrencyInfoKey key = new ProjectMultiCurrency.CurrencyInfoKey(curyID, baseCuryID, rateTypeID ?? pxCurrencyService.DefaultRateTypeID(module), date.GetValueOrDefault());
    PX.Objects.CM.Extensions.CurrencyInfo rate;
    if (!this.rates.TryGetValue(key, out rate))
    {
      if (pxCurrencyService.GetRate(key.CuryID, key.BaseCuryID, key.RateTypeID, new DateTime?(key.Date)) == null)
        throw new PXException("The conversion rate from the transaction currency {0} to the project currency {1} cannot be found for the {2} rate type and {3:d} date.", new object[4]
        {
          (object) key.CuryID,
          (object) key.BaseCuryID,
          (object) key.RateTypeID,
          (object) date
        });
      rate = new ProjectMultiCurrency.CurrencyInfoInsertingAdapter(graph).Insert(new PX.Objects.CM.Extensions.CurrencyInfo()
      {
        ModuleCode = module,
        BaseCuryID = key.BaseCuryID,
        CuryID = key.CuryID,
        CuryRateTypeID = key.RateTypeID,
        CuryEffDate = new DateTime?(key.Date)
      });
      this.rates.Add(key, rate);
    }
    return rate;
  }

  public virtual Decimal GetValueInProjectCurrency(
    PXGraph graph,
    PMProject project,
    string docCuryID,
    DateTime? docDate,
    Decimal? value)
  {
    if (project == null || value.GetValueOrDefault() == 0M)
      return 0M;
    if (project.CuryID == docCuryID)
      return value.GetValueOrDefault();
    if (project.BaseCuryID == docCuryID)
      return MultiCurrencyCalculator.GetCurrencyInfo<PMProject.curyInfoID>(graph, (object) project).CuryConvCury(value.GetValueOrDefault());
    IPXCurrencyService pxCurrencyService = ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(graph);
    IPXCurrencyRate rate = pxCurrencyService.GetRate(docCuryID, project.CuryID, this.GetRateTypeID(graph, project), new DateTime?(docDate ?? DateTime.Now));
    if (rate == null)
      throw new PXException("Please define a conversion rate from the {0} to {1} currency within the {2} currency rate type and the {3:d} effective date on the Currency Rates (CM301000) form.", new object[4]
      {
        (object) docCuryID,
        (object) project.CuryID,
        (object) this.GetRateTypeID(graph, project),
        (object) (docDate ?? DateTime.Now)
      });
    int num = pxCurrencyService.CuryDecimalPlaces(project.CuryID);
    return this.CuryConvCury(rate, value.GetValueOrDefault(), new int?(num));
  }

  public virtual Decimal GetValueInBillingCurrency(
    PXGraph graph,
    PMProject project,
    PX.Objects.CM.Extensions.CurrencyInfo docCurrencyInfo,
    Decimal? value)
  {
    if (project == null || value.GetValueOrDefault() == 0M)
      return 0M;
    if (project.CuryID == project.BillingCuryID)
      return value.GetValueOrDefault();
    if (docCurrencyInfo == null)
      throw new ArgumentNullException(nameof (docCurrencyInfo));
    if (docCurrencyInfo.BaseCuryID == project.CuryID)
      return docCurrencyInfo.CuryConvCury(value.GetValueOrDefault());
    IPXCurrencyService pxCurrencyService = ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(graph);
    IPXCurrencyRate rate = pxCurrencyService.GetRate(project.CuryID, project.BillingCuryID, docCurrencyInfo.CuryRateTypeID, new DateTime?(docCurrencyInfo.CuryEffDate ?? DateTime.Now));
    if (rate == null)
      throw new PXException("Please define a conversion rate from the {0} to {1} currency within the {2} currency rate type and the {3:d} effective date on the Currency Rates (CM301000) form.", new object[4]
      {
        (object) project.CuryID,
        (object) project.BillingCuryID,
        (object) docCurrencyInfo.CuryRateTypeID,
        (object) (docCurrencyInfo.CuryEffDate ?? DateTime.Now)
      });
    int num = pxCurrencyService.CuryDecimalPlaces(project.CuryID);
    return this.CuryConvCury(rate, value.GetValueOrDefault(), new int?(num));
  }

  public void Clear()
  {
    this.directRates.Clear();
    this.rates.Clear();
  }

  protected virtual Decimal CuryConvCury(
    IPXCurrencyRate foundRate,
    Decimal baseval,
    int? precision)
  {
    if (baseval == 0M)
      return 0M;
    if (foundRate == null)
      throw new ArgumentNullException(nameof (foundRate));
    Decimal num;
    try
    {
      num = foundRate.CuryRate.Value;
    }
    catch (InvalidOperationException ex)
    {
      throw new PXRateNotFoundException();
    }
    if (num == 0.0M)
      num = 1.0M;
    Decimal d = foundRate.CuryMultDiv != "D" ? baseval * num : baseval / num;
    if (precision.HasValue)
      d = Decimal.Round(d, precision.Value, MidpointRounding.AwayFromZero);
    return d;
  }

  protected virtual string GetRateTypeID(PXGraph graph, PMProject project)
  {
    string rateTypeId = project.RateTypeID;
    if (string.IsNullOrEmpty(rateTypeId))
      rateTypeId = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select(graph, Array.Empty<object>()))?.PMRateTypeDflt;
    return rateTypeId;
  }

  private class CurrencyInfoInsertingAdapter
  {
    private readonly PXGraph graph;
    private readonly IPXCurrencyHelper currencyHelper;

    public CurrencyInfoInsertingAdapter(PXGraph graph)
    {
      this.graph = graph;
      this.currencyHelper = graph.FindImplementation<IPXCurrencyHelper>();
    }

    public PX.Objects.CM.Extensions.CurrencyInfo Insert(PX.Objects.CM.Extensions.CurrencyInfo currencyInfo)
    {
      if (this.currencyHelper == null)
      {
        PXCache cach = this.graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)];
        PX.Objects.CM.CurrencyInfo current = cach.Current as PX.Objects.CM.CurrencyInfo;
        PX.Objects.CM.Extensions.CurrencyInfo ex = PX.Objects.CM.Extensions.CurrencyInfo.GetEX(cach.Insert((object) currencyInfo.GetCM()) as PX.Objects.CM.CurrencyInfo);
        if (current == null)
          return ex;
        cach.Current = (object) current;
        return ex;
      }
      PXCache cach1 = this.graph.Caches[typeof (PX.Objects.CM.Extensions.CurrencyInfo)];
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.currencyHelper.GetDefaultCurrencyInfo();
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = cach1.Insert((object) currencyInfo) as PX.Objects.CM.Extensions.CurrencyInfo;
      if (defaultCurrencyInfo == null)
        return currencyInfo1;
      cach1.Current = (object) defaultCurrencyInfo;
      return currencyInfo1;
    }
  }

  protected class CurrencyInfoKey
  {
    public readonly string CuryID;
    public readonly string BaseCuryID;
    public readonly string RateTypeID;
    public readonly DateTime Date;

    public CurrencyInfoKey(string curyID, string baseCuryID, string rateTypeID, DateTime date)
    {
      this.CuryID = curyID;
      this.BaseCuryID = baseCuryID;
      this.RateTypeID = rateTypeID;
      this.Date = date;
    }

    public CurrencyInfoKey(string curyID, DateTime date)
      : this(curyID, curyID, string.Empty, date)
    {
    }

    public override int GetHashCode()
    {
      return (((17 * 23 + this.CuryID.GetHashCode()) * 23 + this.BaseCuryID.GetHashCode()) * 23 + this.RateTypeID.GetHashCode()) * 23 + this.Date.GetHashCode();
    }
  }
}
