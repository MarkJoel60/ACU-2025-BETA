// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxCalendar`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.GL.FinPeriods;
using PX.Objects.TX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public class TaxCalendar<TTaxYear, TTaxPeriod> : TaxCalendar
  where TTaxYear : TaxYear, new()
  where TTaxPeriod : TaxPeriod, new()
{
  protected readonly PXGraph Graph;

  public TaxCalendar(PXGraph graph) => this.Graph = graph;

  public virtual TaxPeriod GetOrCreateCurrentTaxPeriod(
    PXCache taxYearCache,
    PXCache taxPeriodCache,
    int? organizationID,
    int? taxAgencyID)
  {
    PX.Objects.AP.Vendor byId = VendorMaint.GetByID(this.Graph, taxAgencyID);
    TaxPeriod preparedPeriod = TaxYearMaint.FindPreparedPeriod(this.Graph, organizationID, taxAgencyID);
    if (preparedPeriod != null)
      return preparedPeriod;
    TaxTran notReportedTaxTran = ReportTax.GetEarliestNotReportedTaxTran(this.Graph, byId, organizationID, new int?());
    DateTime? nullable1;
    DateTime? nullable2;
    if (notReportedTaxTran != null)
    {
      nullable1 = notReportedTaxTran.TranDate;
      if (nullable1.HasValue)
      {
        nullable2 = notReportedTaxTran.TranDate;
        goto label_6;
      }
    }
    nullable2 = this.Graph.Accessinfo.BusinessDate;
label_6:
    DateTime? nullable3 = nullable2;
    bool? nullable4;
    if (notReportedTaxTran != null)
    {
      nullable4 = byId.TaxReportFinPeriod;
      if (nullable4.GetValueOrDefault())
        nullable3 = notReportedTaxTran.FinDate;
    }
    if (TaxYearMaint.FindLastTaxPeriod(this.Graph, organizationID, taxAgencyID)?.Status == "C")
    {
      PXCache taxYearCache1 = taxYearCache;
      PXCache taxPeriodCache1 = taxPeriodCache;
      int? branchID = organizationID;
      PX.Objects.AP.Vendor vendor = byId;
      int? calendarYear = new int?();
      nullable1 = new DateTime?();
      DateTime? baseDate = nullable1;
      this.CreateAndAddToCache(taxYearCache1, taxPeriodCache1, branchID, vendor, calendarYear, baseDate);
    }
    TaxPeriod currentTaxPeriod1 = TaxYearMaint.FinTaxPeriodByDate(this.Graph, organizationID, taxAgencyID, nullable3);
    if (currentTaxPeriod1 != null)
    {
      nullable4 = byId.UpdClosedTaxPeriods;
      if (nullable4.GetValueOrDefault() && currentTaxPeriod1.Status == "C" || currentTaxPeriod1.Status == "D")
        return currentTaxPeriod1;
    }
    TaxPeriod currentTaxPeriod2 = TaxYearMaint.FindFirstOpenTaxPeriod(this.Graph, organizationID, taxAgencyID);
    if (currentTaxPeriod2 != null || !nullable3.HasValue)
      return currentTaxPeriod2;
    this.CreateAndAddToCache(taxYearCache, taxPeriodCache, organizationID, byId, new int?(nullable3.Value.Year), nullable3);
    currentTaxPeriod2 = TaxYearMaint.FinTaxPeriodByDate(this.Graph, organizationID, taxAgencyID, nullable3);
    if (currentTaxPeriod2 != null)
    {
      TaxPeriod taxPeriod = currentTaxPeriod2;
      string str;
      if (notReportedTaxTran != null)
      {
        nullable1 = notReportedTaxTran.TranDate;
        if (nullable1.HasValue)
        {
          str = currentTaxPeriod2.Status;
          goto label_21;
        }
      }
      str = "D";
label_21:
      taxPeriod.Status = str;
    }
    else
      currentTaxPeriod2 = TaxYearMaint.FinTaxPeriodByDate(this.Graph, organizationID, taxAgencyID, this.Graph.Accessinfo.BusinessDate) ?? (TaxPeriod) taxPeriodCache.Cached.Cast<TTaxPeriod>().Where<TTaxPeriod>((Func<TTaxPeriod, bool>) (period => period.Status == "N")).OrderBy<TTaxPeriod, string>((Func<TTaxPeriod, string>) (period => period.TaxPeriodID)).FirstOrDefault<TTaxPeriod>();
    return currentTaxPeriod2;
  }

  public virtual TaxYearWithPeriods<TTaxYear, TTaxPeriod> CreateByFinancialPeriod(
    int? organizationID,
    int? taxAgencyID,
    DateTime? date,
    int? periodCount)
  {
    TaxYearWithPeriods<TTaxYear, TTaxPeriod> byFinancialPeriod = new TaxYearWithPeriods<TTaxYear, TTaxPeriod>();
    foreach (PXResult<OrganizationFinYear, OrganizationFinPeriod> pxResult in PXSelectBase<OrganizationFinYear, PXSelectJoin<OrganizationFinYear, InnerJoin<OrganizationFinPeriod, On<OrganizationFinPeriod.finYear, Equal<OrganizationFinYear.year>, And<OrganizationFinPeriod.organizationID, Equal<OrganizationFinYear.organizationID>>>>, Where<OrganizationFinYear.startDate, LessEqual<Required<OrganizationFinYear.startDate>>, And<OrganizationFinYear.organizationID, Equal<Required<OrganizationFinYear.organizationID>>>>, OrderBy<Desc<OrganizationFinYear.year>>>.Config>.Select(this.Graph, new object[2]
    {
      (object) date,
      (object) organizationID
    }))
    {
      OrganizationFinYear organizationFinYear = PXResult<OrganizationFinYear, OrganizationFinPeriod>.op_Implicit(pxResult);
      if ((object) byFinancialPeriod.TaxYear == null)
      {
        TaxYearWithPeriods<TTaxYear, TTaxPeriod> taxYearWithPeriods = byFinancialPeriod;
        TTaxYear taxYear = new TTaxYear();
        taxYear.Year = organizationFinYear.Year;
        taxYear.StartDate = organizationFinYear.StartDate;
        taxYear.VendorID = taxAgencyID;
        taxYear.OrganizationID = organizationID;
        taxYear.TaxPeriodType = "F";
        taxYear.PlanPeriodsCount = new int?(0);
        taxYear.Filed = new bool?(false);
        taxYearWithPeriods.TaxYear = taxYear;
      }
      else if (!object.Equals((object) organizationFinYear.Year, (object) byFinancialPeriod.TaxYear.Year))
        break;
      TTaxYear taxYear1 = byFinancialPeriod.TaxYear;
      ref TTaxYear local1 = ref taxYear1;
      int? nullable1 = local1.PlanPeriodsCount;
      // ISSUE: variable of a boxed type
      __Boxed<TTaxYear> local2 = (object) local1;
      int? nullable2 = nullable1;
      int? nullable3 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
      local2.PlanPeriodsCount = nullable3;
      if (periodCount.HasValue)
      {
        int count = byFinancialPeriod.TaxPeriods.Count;
        nullable1 = periodCount;
        int valueOrDefault = nullable1.GetValueOrDefault();
        if (!(count <= valueOrDefault & nullable1.HasValue))
          continue;
      }
      OrganizationFinPeriod organizationFinPeriod = PXResult<OrganizationFinYear, OrganizationFinPeriod>.op_Implicit(pxResult);
      List<TTaxPeriod> taxPeriods = byFinancialPeriod.TaxPeriods;
      TTaxPeriod taxPeriod = new TTaxPeriod();
      taxPeriod.OrganizationID = byFinancialPeriod.TaxYear.OrganizationID;
      taxPeriod.TaxYear = organizationFinPeriod.FinYear;
      taxPeriod.TaxPeriodID = organizationFinPeriod.FinPeriodID;
      taxPeriod.StartDate = organizationFinPeriod.StartDate;
      taxPeriod.EndDate = organizationFinPeriod.EndDate;
      taxPeriod.VendorID = taxAgencyID;
      taxPeriods.Add(taxPeriod);
    }
    if ((object) byFinancialPeriod.TaxYear != null)
      byFinancialPeriod.TaxYear.PeriodsCount = periodCount ?? byFinancialPeriod.TaxYear.PlanPeriodsCount;
    return byFinancialPeriod;
  }

  /// <summary>
  /// Creates <see cref="T:PX.Objects.TX.TaxPeriod" /> and <see cref="T:PX.Objects.TX.TaxYear" /> records based on specified tax calendar periods configuration (not on financial periods).
  /// </summary>
  /// <param name="organizationID">The key of a master <see cref="!:Organization" /> record.</param>
  /// <param name="taxAgencyID">The key of a tax agency (<see cref="T:PX.Objects.AP.Vendor" />).</param>
  /// <param name="taxPeriodType">Value of <see cref="T:PX.Objects.TX.VendorTaxPeriodType" />.</param>
  /// <param name="startDate">Defines starting a day and a month of the target year and a year if the previous year does not exist.</param>
  /// <param name="taxYear">It is defined when previous <see cref="T:PX.Objects.TX.TaxYear" /> record exists and it is needed to create subsequent records.</param>
  /// <param name="periodCount">It is defined if <see cref="T:PX.Objects.TX.TaxYear" /> record has custom period count.</param>
  public virtual TaxYearWithPeriods<TTaxYear, TTaxPeriod> CreateByIndependentTaxCalendar(
    TaxCalendar.CreationParams creationParams)
  {
    short num1 = 0;
    switch (creationParams.TaxPeriodType)
    {
      case "M":
        num1 = (short) 12;
        break;
      case "B":
        num1 = (short) 24;
        break;
      case "Q":
        num1 = (short) 4;
        break;
      case "Y":
        num1 = (short) 1;
        break;
      case "E":
        num1 = (short) 6;
        break;
      case "H":
        num1 = (short) 2;
        break;
    }
    int year = creationParams.StartDateTime.Year;
    int month = creationParams.StartDateTime.Month;
    TaxYearWithPeriods<TTaxYear, TTaxPeriod> taxYearWithPeriods = new TaxYearWithPeriods<TTaxYear, TTaxPeriod>();
    TTaxYear taxYear1 = new TTaxYear();
    taxYear1.OrganizationID = new int?(creationParams.OrganizationID);
    taxYear1.VendorID = new int?(creationParams.TaxAgencyID);
    // ISSUE: variable of a boxed type
    __Boxed<TTaxYear> local = (object) taxYear1;
    int? nullable1 = creationParams.TaxYearNumber;
    string str = (nullable1 ?? year).ToString();
    local.Year = str;
    taxYear1.StartDate = new DateTime?(creationParams.StartDateTime);
    taxYear1.TaxPeriodType = creationParams.TaxPeriodType;
    taxYear1.PlanPeriodsCount = new int?((int) num1);
    taxYear1.Filed = new bool?(false);
    taxYearWithPeriods.TaxYear = taxYear1;
    TaxYearWithPeriods<TTaxYear, TTaxPeriod> independentTaxCalendar = taxYearWithPeriods;
    // ISSUE: variable of a boxed type
    __Boxed<TTaxYear> taxYear2 = (object) independentTaxCalendar.TaxYear;
    nullable1 = creationParams.PeriodCount;
    int? nullable2 = new int?(nullable1 ?? (int) num1);
    taxYear2.PeriodsCount = nullable2;
    for (int index = 1; index <= (int) num1; ++index)
    {
      int num2 = index;
      nullable1 = independentTaxCalendar.TaxYear.PeriodsCount;
      int valueOrDefault = nullable1.GetValueOrDefault();
      if (num2 <= valueOrDefault & nullable1.HasValue)
      {
        TTaxPeriod taxPeriod1 = new TTaxPeriod();
        taxPeriod1.OrganizationID = independentTaxCalendar.TaxYear.OrganizationID;
        taxPeriod1.VendorID = independentTaxCalendar.TaxYear.VendorID;
        taxPeriod1.TaxYear = independentTaxCalendar.TaxYear.Year;
        taxPeriod1.TaxPeriodID = independentTaxCalendar.TaxYear.Year + (index < 10 ? "0" : "") + index.ToString();
        taxPeriod1.Status = "N";
        taxPeriod1.Filed = new bool?(false);
        TTaxPeriod taxPeriod2 = taxPeriod1;
        if (num1 <= (short) 12)
        {
          taxPeriod2.StartDate = new DateTime?(new DateTime(year, month, 1));
          month += 12 / (int) num1;
          if (month > 12)
          {
            month -= 12;
            ++year;
          }
          taxPeriod2.EndDate = new DateTime?(new DateTime(year, month, 1));
        }
        else if (num1 == (short) 24)
        {
          taxPeriod2.StartDate = new DateTime?(index % 2 == 1 ? new DateTime(year, month, 1) : new DateTime(year, month, 16 /*0x10*/));
          if (index % 2 == 0)
          {
            ++month;
            if (month > 12)
            {
              month -= 12;
              ++year;
            }
          }
          taxPeriod2.EndDate = new DateTime?(index % 2 == 1 ? new DateTime(year, month, 16 /*0x10*/) : new DateTime(year, month, 1));
        }
        independentTaxCalendar.TaxPeriods.Add(taxPeriod2);
      }
      else
        break;
    }
    return independentTaxCalendar;
  }

  public virtual TaxYearWithPeriods<TTaxYear, TTaxPeriod> CreateWithCorrespondingTaxPeriodType(
    TaxCalendar.CreationParams creationParams)
  {
    return !(creationParams.TaxPeriodType == "F") ? this.CreateByIndependentTaxCalendar(creationParams) : this.CreateByFinancialPeriod(new int?(creationParams.OrganizationID), new int?(creationParams.TaxAgencyID), new DateTime?(creationParams.StartDateTime), creationParams.PeriodCount);
  }

  public virtual void CreateAndAddToCache(
    PXCache taxYearCache,
    PXCache taxPeriodCache,
    int? branchID,
    PX.Objects.AP.Vendor vendor,
    int? calendarYear = null,
    DateTime? baseDate = null)
  {
    TaxYearWithPeriods<TTaxYear, TTaxPeriod> taxYearWithPeriods = this.Create(branchID, vendor, calendarYear, baseDate);
    taxYearCache.Insert((object) taxYearWithPeriods.TaxYear);
    foreach (TTaxPeriod taxPeriod in taxYearWithPeriods.TaxPeriods)
      taxPeriodCache.Insert((object) taxPeriod);
  }

  public virtual TaxYearWithPeriods<TTaxYear, TTaxPeriod> Create(
    int? organizationID,
    PX.Objects.AP.Vendor vendor,
    int? calendarYear = null,
    DateTime? baseDate = null)
  {
    this.Graph.Caches[typeof (TaxYear)].Clear();
    this.Graph.Caches[typeof (TaxYear)].ClearQueryCacheObsolete();
    this.Graph.Caches[typeof (TaxPeriod)].Clear();
    this.Graph.Caches[typeof (TaxPeriod)].ClearQueryCacheObsolete();
    TTaxYear taxYear = default (TTaxYear);
    TaxYear lastTaxYear = TaxYearMaint.FindLastTaxYear(this.Graph, organizationID, vendor.BAccountID);
    if (lastTaxYear != null)
    {
      taxYear = new TTaxYear();
      this.Graph.Caches[typeof (TaxYear)].RestoreCopy((object) taxYear, (object) lastTaxYear);
    }
    TaxCalendar.CreationParams creationParams = new TaxCalendar.CreationParams()
    {
      OrganizationID = organizationID.Value,
      TaxAgencyID = vendor.BAccountID.Value
    };
    if ((object) taxYear != null)
    {
      creationParams.TaxPeriodType = taxYear.TaxPeriodType;
      TaxPeriod lastTaxPeriod = TaxYearMaint.FindLastTaxPeriod(this.Graph, organizationID, vendor.BAccountID);
      if (lastTaxPeriod != null)
      {
        int int32 = Convert.ToInt32(taxYear.Year);
        if (lastTaxPeriod.TaxYear != taxYear.Year)
          creationParams.PeriodCount = taxYear.PeriodsCount;
        else
          ++int32;
        creationParams.StartDateTime = lastTaxPeriod.EndDate.Value;
        creationParams.TaxYearNumber = new int?(int32);
        return this.CreateWithCorrespondingTaxPeriodType(creationParams);
      }
      creationParams.StartDateTime = taxYear.StartDate.Value;
      creationParams.TaxYearNumber = new int?(Convert.ToInt32(taxYear.Year));
      creationParams.PeriodCount = taxYear.PeriodsCount;
      return this.CreateWithCorrespondingTaxPeriodType(creationParams);
    }
    this.FillCalendarDataWhenLastOrganizationTaxYearDoesNotExist(calendarYear.Value, vendor, creationParams, baseDate);
    return this.CreateWithCorrespondingTaxPeriodType(creationParams);
  }

  public void FillCalendarDataWhenLastOrganizationTaxYearDoesNotExist(
    int calendarYear,
    PX.Objects.AP.Vendor vendor,
    TaxCalendar.CreationParams creationParams,
    DateTime? baseDate = null)
  {
    TTaxYear taxYear1 = default (TTaxYear);
    if (vendor.TaxPeriodType != "F")
    {
      TaxYear taxYear2 = PXResultset<TaxYear>.op_Implicit(PXSelectBase<TaxYear, PXSelect<TaxYear, Where<TaxYear.vendorID, Equal<Required<TaxYear.vendorID>>>, OrderBy<Desc<TaxYear.year>>>.Config>.SelectWindowed(this.Graph, 0, 1, new object[1]
      {
        (object) vendor.BAccountID
      }));
      if (taxYear2 != null)
      {
        taxYear1 = new TTaxYear();
        this.Graph.Caches[typeof (TaxYear)].RestoreCopy((object) taxYear1, (object) taxYear2);
      }
    }
    if ((object) taxYear1 != null)
    {
      int num = Convert.ToInt32(taxYear1.Year) - taxYear1.StartDate.Value.Year;
      TaxCalendar.CreationParams creationParams1 = creationParams;
      int year = calendarYear - num;
      DateTime? startDate = taxYear1.StartDate;
      DateTime dateTime1 = startDate.Value;
      int month = dateTime1.Month;
      startDate = taxYear1.StartDate;
      dateTime1 = startDate.Value;
      int day = dateTime1.Day;
      DateTime dateTime2 = new DateTime(year, month, day);
      creationParams1.StartDateTime = dateTime2;
      creationParams.TaxPeriodType = vendor.TaxPeriodType ?? taxYear1.TaxPeriodType;
    }
    else
    {
      creationParams.TaxPeriodType = vendor.TaxPeriodType;
      creationParams.StartDateTime = !(creationParams.TaxPeriodType != "F") ? baseDate ?? new DateTime(calendarYear, 1, 1) : new DateTime(calendarYear, 1, 1);
    }
    creationParams.TaxYearNumber = new int?(calendarYear);
  }

  /// <summary>Returns TaxPeriod form the given date.</summary>
  [Obsolete("The method is obsolete and will be removed in Acumatica 8.0.")]
  public static string GetPeriod(int vendorID, DateTime? fromdate)
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<TaxPeriod>(new PXDataField[4]
    {
      new PXDataField(typeof (TaxPeriod.taxPeriodID).Name),
      (PXDataField) new PXDataFieldValue(typeof (TaxPeriod.vendorID).Name, (PXDbType) 8, (object) vendorID),
      (PXDataField) new PXDataFieldValue(typeof (TaxPeriod.startDate).Name, (PXDbType) 33, new int?(4), (object) fromdate, (PXComp) 5),
      (PXDataField) new PXDataFieldValue(typeof (TaxPeriod.endDate).Name, (PXDbType) 33, new int?(4), (object) fromdate, (PXComp) 2)
    }))
    {
      if (pxDataRecord != null)
        return pxDataRecord.GetString(0);
    }
    return (string) null;
  }
}
