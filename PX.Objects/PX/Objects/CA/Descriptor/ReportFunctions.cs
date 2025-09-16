// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Descriptor.ReportFunctions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Aging;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA.Descriptor;

public class ReportFunctions
{
  private const int NUMBER_OF_AGING_BUCKETS = 5;

  public string GetAPPaymentInfo(
    string accountCD,
    string paymentMethodID,
    string detailID,
    string acctCD)
  {
    return this.GetAPPaymentInfo(accountCD, paymentMethodID, detailID, acctCD, (string) null);
  }

  public string GetAPPaymentInfo(
    string accountCD,
    string paymentMethodID,
    string detailID,
    string acctCD,
    string locationCD)
  {
    VendorPaymentMethodDetail paymentMethodDetail = PXResultset<VendorPaymentMethodDetail>.op_Implicit(PXSelectBase<VendorPaymentMethodDetail, PXSelectJoin<VendorPaymentMethodDetail, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<VendorPaymentMethodDetail.bAccountID>>, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<VendorPaymentMethodDetail.paymentMethodID>>, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<VendorPaymentMethodDetail.paymentMethodID>, And<PaymentMethodDetail.detailID, Equal<VendorPaymentMethodDetail.detailID>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PX.Objects.CA.CashAccount.accountID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Location.locationCD, Equal<Required<PX.Objects.CR.Location.locationCD>>>>>>>>>>>, Where<PX.Objects.GL.Account.accountCD, Equal<Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<VendorPaymentMethodDetail.detailID, Equal<Required<VendorPaymentMethodDetail.detailID>>, And<PX.Objects.CR.BAccount.acctCD, Equal<Required<PX.Objects.CR.BAccount.acctCD>>, And<Where2<Where<PX.Objects.CR.Location.locationID, IsNotNull>, And<VendorPaymentMethodDetail.locationID, Equal<PX.Objects.CR.Location.locationID>, Or2<Where<PX.Objects.CR.Location.locationID, IsNull>, And<VendorPaymentMethodDetail.locationID, Equal<PX.Objects.CR.BAccount.defLocationID>>>>>>>>>>>.Config>.Select((PXGraph) PXGraph.CreateInstance<BusinessAccountMaint>(), new object[5]
    {
      (object) locationCD,
      (object) accountCD,
      (object) paymentMethodID,
      (object) detailID,
      (object) acctCD
    }));
    return paymentMethodDetail == null ? string.Empty : paymentMethodDetail.DetailValue;
  }

  public string GetARPaymentInfo(
    string accountCD,
    string paymentMethodID,
    string detailID,
    string pMInstanceID)
  {
    if (!int.TryParse(pMInstanceID, out int _))
      throw new PXArgumentException(nameof (pMInstanceID), "The value of the parameter \"pMInstanceID\" cannot be represented as an integer.");
    CustomerPaymentMethodDetail paymentMethodDetail = PXResultset<CustomerPaymentMethodDetail>.op_Implicit(PXSelectBase<CustomerPaymentMethodDetail, PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>>, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>, And<PaymentMethodDetail.detailID, Equal<CustomerPaymentMethodDetail.detailID>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PX.Objects.CA.CashAccount.accountID>>>>>>>, Where<PX.Objects.GL.Account.accountCD, Equal<Required<PX.Objects.GL.Account.accountCD>>, And<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<CustomerPaymentMethodDetail.detailID, Equal<Required<CustomerPaymentMethodDetail.detailID>>, And<CustomerPaymentMethodDetail.pMInstanceID, Equal<Required<CustomerPaymentMethodDetail.pMInstanceID>>>>>>>.Config>.Select((PXGraph) PXGraph.CreateInstance<CustomerPaymentMethodMaint>(), new object[4]
    {
      (object) accountCD,
      (object) paymentMethodID,
      (object) detailID,
      (object) pMInstanceID
    }));
    return paymentMethodDetail == null ? string.Empty : paymentMethodDetail.Value;
  }

  public string GetRemitPaymentInfo(string cashAccountCD, string paymentMethodID, string detailID)
  {
    CashAccountPaymentMethodDetail paymentMethodDetail = PXResultset<CashAccountPaymentMethodDetail>.op_Implicit(PXSelectBase<CashAccountPaymentMethodDetail, PXSelectJoin<CashAccountPaymentMethodDetail, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<CashAccountPaymentMethodDetail.paymentMethodID>>, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<CashAccountPaymentMethodDetail.paymentMethodID>, And<PaymentMethodDetail.detailID, Equal<CashAccountPaymentMethodDetail.detailID>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<CashAccountPaymentMethodDetail.cashAccountID>, And<PX.Objects.CA.CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>>>>>, Where<PX.Objects.CA.CashAccount.cashAccountCD, Equal<Required<PX.Objects.CA.CashAccount.cashAccountCD>>, And<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<CashAccountPaymentMethodDetail.detailID, Equal<Required<CashAccountPaymentMethodDetail.detailID>>>>>>.Config>.Select((PXGraph) PXGraph.CreateInstance<CashAccountMaint>(), new object[3]
    {
      (object) cashAccountCD,
      (object) paymentMethodID,
      (object) detailID
    }));
    return paymentMethodDetail == null ? string.Empty : paymentMethodDetail.DetailValue;
  }

  private ConcurrentBag<CurrencyRate> GetCachedCurrencyRates()
  {
    ConcurrentBag<CurrencyRate> cachedCurrencyRates = (ConcurrentBag<CurrencyRate>) PXContext.GetSlot<ReportFunctions.CurrencyRateCollection>();
    if (cachedCurrencyRates == null)
      cachedCurrencyRates = (ConcurrentBag<CurrencyRate>) PXContext.SetSlot<ReportFunctions.CurrencyRateCollection>(PXDatabase.GetSlot<ReportFunctions.CurrencyRateCollection>(typeof (CurrencyRate).FullName, new System.Type[1]
      {
        typeof (CurrencyRate)
      }));
    return cachedCurrencyRates;
  }

  private CurrencyRate FindCurrencyRate(
    ConcurrentBag<CurrencyRate> rates,
    string fromCury,
    string toCury,
    string rateType,
    DateTime effectiveDate)
  {
    CurrencyRate currencyRate = (CurrencyRate) null;
    if (rates.Count != 0)
    {
      foreach (CurrencyRate rate in rates)
      {
        if (rate.CuryRateType == rateType && rate.FromCuryID == fromCury && rate.ToCuryID == toCury)
        {
          DateTime? curyEffDate1 = rate.CuryEffDate;
          DateTime dateTime1 = effectiveDate;
          if ((curyEffDate1.HasValue ? (curyEffDate1.GetValueOrDefault() == dateTime1 ? 1 : 0) : 0) != 0)
          {
            currencyRate = rate;
            break;
          }
          DateTime? curyEffDate2 = rate.CuryEffDate;
          DateTime dateTime2 = effectiveDate;
          if ((curyEffDate2.HasValue ? (curyEffDate2.GetValueOrDefault() < dateTime2 ? 1 : 0) : 0) != 0)
          {
            if (currencyRate != null)
            {
              DateTime? curyEffDate3 = rate.CuryEffDate;
              DateTime? curyEffDate4 = currencyRate.CuryEffDate;
              if ((curyEffDate3.HasValue & curyEffDate4.HasValue ? (curyEffDate3.GetValueOrDefault() > curyEffDate4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                goto label_10;
            }
            if (currencyRate != null)
              continue;
label_10:
            currencyRate = rate;
          }
        }
      }
    }
    return currencyRate;
  }

  private CurrencyRate FindCurrencyRate(
    string fromCury,
    string toCury,
    string rateType,
    DateTime effectiveDate)
  {
    ConcurrentBag<CurrencyRate> cachedCurrencyRates = this.GetCachedCurrencyRates();
    CurrencyRate currencyRate1 = this.FindCurrencyRate(cachedCurrencyRates, fromCury, toCury, rateType, effectiveDate);
    if (currencyRate1 == null)
    {
      PXGraph pxGraph = new PXGraph();
      PXResultset<CurrencyRate> pxResultset = PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, GreaterEqual<Required<CurrencyRate.curyEffDate>>>>>>, OrderBy<Asc<CurrencyRate.curyEffDate>>>.Config>.SelectWindowed(pxGraph, 0, 100, new object[4]
      {
        (object) fromCury,
        (object) toCury,
        (object) rateType,
        (object) effectiveDate
      });
      if (pxResultset.Count == 0)
        pxResultset = PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, Less<Required<CurrencyRate.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.SelectWindowed(pxGraph, 0, 1, new object[4]
        {
          (object) fromCury,
          (object) toCury,
          (object) rateType,
          (object) effectiveDate
        });
      foreach (PXResult<CurrencyRate> pxResult in pxResultset)
      {
        CurrencyRate currencyRate2 = PXResult<CurrencyRate>.op_Implicit(pxResult);
        ((IProducerConsumerCollection<CurrencyRate>) cachedCurrencyRates).TryAdd(currencyRate2);
      }
      currencyRate1 = this.FindCurrencyRate(cachedCurrencyRates, fromCury, toCury, rateType, effectiveDate);
    }
    return currencyRate1;
  }

  public object CuryConvCury(
    object fromCury,
    object toCury,
    object rateType,
    object baseval,
    object effectiveDate)
  {
    CurrencyRate currencyRate = this.FindCurrencyRate((string) fromCury, (string) toCury, (string) rateType, (DateTime) effectiveDate);
    Decimal num1;
    if (currencyRate != null)
    {
      Decimal num2;
      try
      {
        num2 = currencyRate.CuryRate.Value;
      }
      catch (InvalidOperationException ex)
      {
        throw new PXRateNotFoundException();
      }
      if (num2 == 0.0M)
        num2 = 1.0M;
      num1 = currencyRate.CuryMultDiv != "D" ? (Decimal) baseval * num2 : (Decimal) baseval / num2;
    }
    else
      num1 = baseval == null ? 0M : (Decimal) baseval;
    return (object) num1;
  }

  public object CuryConvBase(
    object fromCury,
    object rateType,
    object curyval,
    object effectiveDate)
  {
    Company company = PXResultset<Company>.op_Implicit(PXSelectBase<Company, PXSelect<Company>.Config>.Select(new PXGraph(), Array.Empty<object>()));
    object obj = this.CuryConvCury(fromCury, (object) company.BaseCuryID, rateType, curyval, effectiveDate);
    return (object) (obj != null ? (Decimal) obj : 0M);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2019 R2.")]
  public string GetBucketDescriptionForAgedReport(
    DateTime? reportDate,
    int? dayBucketBoundary0,
    int? dayBucketBoundary1,
    int? dayBucketBoundary2,
    int? dayBucketBoundary3,
    bool? isByFinancialPeriod,
    bool? isForwardAging,
    int? bucketIndex)
  {
    return this.GetBucketDescriptionForAgedReport(reportDate, dayBucketBoundary0, dayBucketBoundary1, dayBucketBoundary2, dayBucketBoundary3, isByFinancialPeriod, isForwardAging, bucketIndex, 0, true);
  }

  public string GetBucketDescriptionForAgedReport(
    DateTime? reportDate,
    int? dayBucketBoundary0,
    int? dayBucketBoundary1,
    int? dayBucketBoundary2,
    int? dayBucketBoundary3,
    bool? isByFinancialPeriod,
    bool? isForwardAging,
    int? bucketIndex,
    int calendarOrganizationID,
    bool usePeriodDescription)
  {
    if (!reportDate.HasValue || !dayBucketBoundary0.HasValue || !dayBucketBoundary1.HasValue || !dayBucketBoundary2.HasValue || !dayBucketBoundary3.HasValue || !bucketIndex.HasValue)
      return (string) null;
    AgingDirection agingDirection = isForwardAging.GetValueOrDefault() ? AgingDirection.Forward : AgingDirection.Backwards;
    if (isByFinancialPeriod.GetValueOrDefault())
    {
      try
      {
        return AgingEngine.GetPeriodAgingBucketDescriptions(new PXGraph().GetService<IFinPeriodRepository>(), reportDate.Value, agingDirection, 5, calendarOrganizationID, usePeriodDescription).ElementAtOrDefault<string>(bucketIndex.Value);
      }
      catch (PXFinPeriodException ex)
      {
        throw new PXFinPeriodException(isForwardAging.GetValueOrDefault() ? "The aging period names cannot be determined for a statement cycle with the End of Period schedule type. The financial period should be defined for the aging date on the Financial Periods (GL201000) form, as well as the four subsequent financial periods." : "The aging period names cannot be determined for a statement cycle with the End of Period schedule type. The financial period should be defined for the aging date on the Financial Periods (GL201000) form, as well as the four preceding financial periods.");
      }
    }
    else
      return AgingEngine.GetDayAgingBucketDescriptions(agingDirection, (IEnumerable<int>) new int[4]
      {
        dayBucketBoundary0.GetValueOrDefault(),
        dayBucketBoundary1.GetValueOrDefault(),
        dayBucketBoundary2.GetValueOrDefault(),
        dayBucketBoundary3.GetValueOrDefault()
      }, true).ElementAtOrDefault<string>(bucketIndex.Value);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2019 R2.")]
  public object GetBucketNumberForAgedReport(
    DateTime? reportDate,
    DateTime? dateToAge,
    int? dayBucketBoundary0,
    int? dayBucketBoundary1,
    int? dayBucketBoundary2,
    int? dayBucketBoundary3,
    bool? isByFinancialPeriod,
    bool? isForwardAging)
  {
    return this.GetBucketNumberForAgedReport(reportDate, dateToAge, dayBucketBoundary0, dayBucketBoundary1, dayBucketBoundary2, dayBucketBoundary3, isByFinancialPeriod, isForwardAging, 0);
  }

  public object GetBucketNumberForAgedReport(
    DateTime? reportDate,
    DateTime? dateToAge,
    int? dayBucketBoundary0,
    int? dayBucketBoundary1,
    int? dayBucketBoundary2,
    int? dayBucketBoundary3,
    bool? isByFinancialPeriod,
    bool? isForwardAging,
    int organizationID)
  {
    if (!reportDate.HasValue || !dayBucketBoundary0.HasValue || !dayBucketBoundary1.HasValue || !dayBucketBoundary2.HasValue || !dayBucketBoundary3.HasValue)
      return (object) null;
    AgingDirection agingDirection = isForwardAging.GetValueOrDefault() ? AgingDirection.Forward : AgingDirection.Backwards;
    PXGraph graph = new PXGraph();
    int numberForAgedReport;
    if (!isByFinancialPeriod.GetValueOrDefault())
      numberForAgedReport = AgingEngine.AgeByDays(reportDate.Value, dateToAge.Value, agingDirection, dayBucketBoundary0.GetValueOrDefault(), dayBucketBoundary1.GetValueOrDefault(), dayBucketBoundary2.GetValueOrDefault(), dayBucketBoundary3.GetValueOrDefault());
    else
      numberForAgedReport = AgingEngine.AgeByPeriods(reportDate.Value, dateToAge.Value, graph.GetService<IFinPeriodRepository>(), agingDirection, 5, organizationID);
    return (object) numberForAgedReport;
  }

  /// <summary>
  /// Retrieves the financial period with the same <see cref="P:PX.Objects.GL.FinPeriods.MasterFinPeriod.PeriodNbr" />
  /// as the one specified, but residing in the previous financial year.
  /// If no such period exists, returns <c>null</c>.
  /// </summary>
  public string GetSamePeriodInPreviousYear(string financialPeriodID)
  {
    if (financialPeriodID == null)
      return (string) null;
    PXGraph instance = (PXGraph) PXGraph.CreateInstance<MasterFinPeriodMaint>();
    try
    {
      return FinPeriodIDFormattingAttribute.FormatForDisplay(instance.GetService<IFinPeriodRepository>().GetSamePeriodInPreviousYear(financialPeriodID, new int?(0)));
    }
    catch (PXFinPeriodException ex)
    {
      return (string) null;
    }
  }

  /// <summary>
  /// Retrieves the first financial period of the year corresponding
  /// to the financial period specified.
  /// If no such period exists, returns <c>null</c>.
  /// </summary>
  public string GetFirstPeriodOfYear(string financialPeriodID)
  {
    if (financialPeriodID == null)
      return (string) null;
    MasterFinPeriodMaint instance = PXGraph.CreateInstance<MasterFinPeriodMaint>();
    string finPeriodIdOfYear = FinPeriodUtils.GetFirstFinPeriodIDOfYear(financialPeriodID);
    return !((PXGraph) instance).GetService<IFinPeriodRepository>().PeriodExists(financialPeriodID, new int?(0)) ? (string) null : FinPeriodIDFormattingAttribute.FormatForDisplay(finPeriodIdOfYear);
  }

  /// <summary>
  /// Returns a value indicating whether the two specified financial
  /// period IDs belong to the same financial year.
  /// </summary>
  public bool ArePeriodsInSameYear(string firstPeriodID, string secondPeriodID)
  {
    string periodYear1 = this.GetPeriodYear(firstPeriodID);
    string periodYear2 = this.GetPeriodYear(secondPeriodID);
    return periodYear1 != null && periodYear2 != null && string.Compare(periodYear1, periodYear2, StringComparison.OrdinalIgnoreCase) == 0;
  }

  /// <summary>
  /// Extracts the year of the financial period
  /// from the specified financial period ID.
  /// </summary>
  public string GetPeriodYear(string financialPeriodID) => financialPeriodID?.Substring(0, 4);

  /// <summary>
  /// Extracts the number of the financial period
  /// from the specified financial period ID.
  /// </summary>
  public string GetPeriodNumber(string financialPeriodID) => financialPeriodID?.Substring(4, 2);

  /// <summary>
  /// Get the localizable amount description for applications in
  /// AR statement (AR641500 and AR642000) reports.
  /// </summary>
  public string GetAmountDescriptionForStatementApplication(
    Decimal? writeOffAmount,
    Decimal? cashDiscountAmount,
    Decimal? gainLossAmount,
    string localeName)
  {
    string str = string.Empty;
    Decimal? nullable1 = writeOffAmount;
    Decimal num1 = 0M;
    if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
    {
      Decimal? nullable2 = cashDiscountAmount;
      Decimal num2 = 0M;
      if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
      {
        Decimal? nullable3 = gainLossAmount;
        Decimal num3 = 0M;
        if (!(nullable3.GetValueOrDefault() == num3 & nullable3.HasValue))
        {
          str = "Write-off, cash discount, and RGOL amount for";
          goto label_20;
        }
      }
    }
    Decimal? nullable4 = writeOffAmount;
    Decimal num4 = 0M;
    if (!(nullable4.GetValueOrDefault() == num4 & nullable4.HasValue) && cashDiscountAmount.IsNullOrZero())
    {
      Decimal? nullable5 = gainLossAmount;
      Decimal num5 = 0M;
      if (!(nullable5.GetValueOrDefault() == num5 & nullable5.HasValue))
      {
        str = "Write-off and RGOL amount for";
        goto label_20;
      }
    }
    if (writeOffAmount.IsNullOrZero())
    {
      Decimal? nullable6 = cashDiscountAmount;
      Decimal num6 = 0M;
      if (!(nullable6.GetValueOrDefault() == num6 & nullable6.HasValue))
      {
        Decimal? nullable7 = gainLossAmount;
        Decimal num7 = 0M;
        if (!(nullable7.GetValueOrDefault() == num7 & nullable7.HasValue))
        {
          str = "Cash discount and RGOL amount for";
          goto label_20;
        }
      }
    }
    Decimal? nullable8 = writeOffAmount;
    Decimal num8 = 0M;
    if (!(nullable8.GetValueOrDefault() == num8 & nullable8.HasValue))
    {
      Decimal? nullable9 = cashDiscountAmount;
      Decimal num9 = 0M;
      if (!(nullable9.GetValueOrDefault() == num9 & nullable9.HasValue) && gainLossAmount.IsNullOrZero())
      {
        str = "Cash discount and write-off amount for";
        goto label_20;
      }
    }
    Decimal? nullable10 = gainLossAmount;
    Decimal num10 = 0M;
    if (!(nullable10.GetValueOrDefault() == num10 & nullable10.HasValue))
    {
      str = "RGOL amount for";
    }
    else
    {
      Decimal? nullable11 = cashDiscountAmount;
      Decimal num11 = 0M;
      if (!(nullable11.GetValueOrDefault() == num11 & nullable11.HasValue))
      {
        str = "Cash discount amount for";
      }
      else
      {
        Decimal? nullable12 = writeOffAmount;
        Decimal num12 = 0M;
        if (!(nullable12.GetValueOrDefault() == num12 & nullable12.HasValue))
          str = "Write-off amount for";
      }
    }
label_20:
    using (new PXLocaleScope(localeName))
      return PXMessages.LocalizeNoPrefix(str);
  }

  /// <summary>
  /// Gets the localized "applied to" string for AR641500 and AR642000 AR statement
  /// reports.
  /// </summary>
  public string AppliedToLocalized(string localeName)
  {
    using (new PXLocaleScope(localeName))
      return PXMessages.LocalizeNoPrefix("applied to");
  }

  /// <summary>
  /// Using the specified statement cycle aging settings and information about
  /// various dates, returns the relevant date value to be compared to the aging
  /// date to age the corresponding document in AR Aged reports.
  /// </summary>
  /// <param name="invoiceDueDate">
  /// The due date of the invoice document. When it is <c>null</c>, the document
  /// is considered a credit document, and the end result will be affected
  /// by the <paramref name="ageCredits" /> parameter.
  /// </param>
  public DateTime? GetDocumentDateForAgedReport(
    DateTime? agingDate,
    DateTime? invoiceDueDate,
    DateTime? documentDate,
    string ageBasedOnType,
    bool? ageCredits)
  {
    switch (ageBasedOnType)
    {
      case "U":
        if (invoiceDueDate.HasValue)
          return invoiceDueDate;
        return ageCredits.GetValueOrDefault() ? documentDate : agingDate;
      case "O":
        return !invoiceDueDate.HasValue && !ageCredits.GetValueOrDefault() ? agingDate : documentDate;
      default:
        return agingDate;
    }
  }

  private class CurrencyRateCollection : ConcurrentBag<CurrencyRate>, IPXCompanyDependent
  {
  }
}
