// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SPCommissionCalendar
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.FinPeriods;
using System;

#nullable disable
namespace PX.Objects.AR;

public static class SPCommissionCalendar
{
  public static TPeriod Create<TPeriod>(
    PXGraph graph,
    PXSelectBase<ARSPCommissionYear> Year_Select,
    PXSelectBase<TPeriod> Period_Select,
    ARSetup aSetup,
    DateTime? date)
    where TPeriod : ARSPCommissionPeriod, new()
  {
    short? nullable1 = new short?();
    switch (aSetup.SPCommnPeriodType)
    {
      case "M":
        nullable1 = new short?((short) 12);
        break;
      case "Q":
        nullable1 = new short?((short) 4);
        break;
      case "Y":
        nullable1 = new short?((short) 1);
        break;
    }
    if (nullable1.HasValue)
    {
      int year = date.Value.Year;
      int month = 1;
      string str = year.ToString();
      ARSPCommissionYear arspCommissionYear = PXResultset<ARSPCommissionYear>.op_Implicit(Year_Select.Select(new object[1]
      {
        (object) str
      }));
      if (arspCommissionYear == null)
        arspCommissionYear = (ARSPCommissionYear) ((PXSelectBase) Year_Select).Cache.Insert((object) new ARSPCommissionYear()
        {
          Year = str
        });
      int num1 = 1;
      while (true)
      {
        int num2 = num1;
        short? nullable2 = nullable1;
        int? nullable3 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault() + 1) : new int?();
        int valueOrDefault = nullable3.GetValueOrDefault();
        if (num2 < valueOrDefault & nullable3.HasValue)
        {
          TPeriod period1 = new TPeriod();
          period1.Year = arspCommissionYear.Year;
          period1.CommnPeriodID = arspCommissionYear.Year + (num1 < 10 ? "0" : "") + num1.ToString();
          period1.StartDate = new DateTime?(new DateTime(year, month, 1));
          month += 12 / (int) nullable1.Value;
          if (month > 12)
          {
            month = 1;
            ++year;
          }
          period1.EndDate = new DateTime?(new DateTime(year, month, 1));
          DateTime? endDate = period1.EndDate;
          DateTime dateTime = date.Value;
          if ((endDate.HasValue ? (endDate.GetValueOrDefault() <= dateTime ? 1 : 0) : 0) != 0)
            period1.Status = "C";
          TPeriod period2 = (TPeriod) ((PXSelectBase) Period_Select).Cache.Insert((object) period1);
          ++num1;
        }
        else
          break;
      }
    }
    else
    {
      ARSPCommissionYear arspCommissionYear = (ARSPCommissionYear) null;
      foreach (PXResult<MasterFinYear, MasterFinPeriod> pxResult in PXSelectBase<MasterFinYear, PXSelectJoin<MasterFinYear, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finYear, Equal<MasterFinYear.year>>>, Where<MasterFinYear.startDate, LessEqual<Required<MasterFinYear.startDate>>>, OrderBy<Desc<MasterFinYear.year>>>.Config>.Select(graph, new object[1]
      {
        (object) date
      }))
      {
        if (arspCommissionYear == null)
        {
          arspCommissionYear = PXResultset<ARSPCommissionYear>.op_Implicit(Year_Select.Select(new object[1]
          {
            (object) PXResult<MasterFinYear, MasterFinPeriod>.op_Implicit(pxResult).Year
          }));
          if (arspCommissionYear == null)
          {
            ARSPCommissionYear from = SPCommissionCalendar.CreateFrom(PXResult<MasterFinYear, MasterFinPeriod>.op_Implicit(pxResult));
            arspCommissionYear = (ARSPCommissionYear) ((PXSelectBase) Year_Select).Cache.Insert((object) from);
          }
        }
        else if (!object.Equals((object) PXResult<MasterFinYear, MasterFinPeriod>.op_Implicit(pxResult).Year, (object) arspCommissionYear.Year))
          break;
        ARSPCommissionPeriod from1 = SPCommissionCalendar.CreateFrom(PXResult<MasterFinYear, MasterFinPeriod>.op_Implicit(pxResult));
        DateTime? endDate = from1.EndDate;
        DateTime dateTime = date.Value;
        if ((endDate.HasValue ? (endDate.GetValueOrDefault() <= dateTime ? 1 : 0) : 0) != 0)
          from1.Status = "C";
        ((PXSelectBase) Period_Select).Cache.Insert((object) from1);
      }
    }
    return PXResultset<TPeriod>.op_Implicit(Period_Select.Select(new object[2]
    {
      (object) date,
      (object) date
    }));
  }

  public static ARSPCommissionPeriod CreateFrom(MasterFinPeriod aFiscalPeriod)
  {
    return new ARSPCommissionPeriod()
    {
      CommnPeriodID = aFiscalPeriod.FinPeriodID,
      StartDate = aFiscalPeriod.StartDate,
      EndDate = aFiscalPeriod.EndDate,
      Year = aFiscalPeriod.FinYear
    };
  }

  public static ARSPCommissionYear CreateFrom(MasterFinYear aFiscalYear)
  {
    return new ARSPCommissionYear()
    {
      Year = aFiscalYear.Year
    };
  }
}
