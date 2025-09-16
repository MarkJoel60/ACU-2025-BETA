// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxCalendar
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.TX;

public class TaxCalendar
{
  public class CreationParams
  {
    public int OrganizationID { get; set; }

    public int TaxAgencyID { get; set; }

    public string TaxPeriodType { get; set; }

    public DateTime StartDateTime { get; set; }

    public int? TaxYearNumber { get; set; }

    public int? PeriodCount { get; set; }

    public DateTime? BaseDate { get; set; }

    public static TaxCalendar.CreationParams FromTaxYear(TaxYear taxYear)
    {
      return new TaxCalendar.CreationParams()
      {
        OrganizationID = taxYear.OrganizationID.Value,
        TaxAgencyID = taxYear.VendorID.Value,
        TaxPeriodType = taxYear.TaxPeriodType,
        StartDateTime = taxYear.StartDate.Value,
        TaxYearNumber = new int?(Convert.ToInt32(taxYear.Year)),
        PeriodCount = taxYear.PeriodsCount
      };
    }
  }
}
