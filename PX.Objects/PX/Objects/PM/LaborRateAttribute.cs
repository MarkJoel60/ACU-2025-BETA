// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.LaborRateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PM;

public class LaborRateAttribute : PXEventSubscriberAttribute
{
  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
    LaborRateAttribute laborRateAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) laborRateAttribute1, __vmethodptr(laborRateAttribute1, SetRates));
    fieldUpdated1.AddHandler<PMLaborCostRate.regularHours>(pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = sender.Graph.FieldUpdated;
    LaborRateAttribute laborRateAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) laborRateAttribute2, __vmethodptr(laborRateAttribute2, SetRates));
    fieldUpdated2.AddHandler<PMLaborCostRate.annualSalary>(pxFieldUpdated2);
    PXGraph.FieldUpdatedEvents fieldUpdated3 = sender.Graph.FieldUpdated;
    LaborRateAttribute laborRateAttribute3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) laborRateAttribute3, __vmethodptr(laborRateAttribute3, WageRateUpdated));
    fieldUpdated3.AddHandler<PMLaborCostRate.wageRate>(pxFieldUpdated3);
    PXGraph.FieldUpdatedEvents fieldUpdated4 = sender.Graph.FieldUpdated;
    LaborRateAttribute laborRateAttribute4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated4 = new PXFieldUpdated((object) laborRateAttribute4, __vmethodptr(laborRateAttribute4, RateUpdated));
    fieldUpdated4.AddHandler<PMLaborCostRate.rate>(pxFieldUpdated4);
  }

  protected virtual void SetRates(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMLaborCostRate row) || !(row.EmploymentType == "S") && !(row.EmploymentType == "E"))
      return;
    Decimal hourlyRate = LaborRateAttribute.CalculateHourlyRate(row.RegularHours, row.AnnualSalary);
    if (PXAccess.FeatureInstalled<FeaturesSet.payrollModule>())
    {
      sender.SetValue<PMLaborCostRate.wageRate>((object) row, (object) hourlyRate);
      PXCache pxCache = sender;
      PMLaborCostRate pmLaborCostRate = row;
      Decimal num = hourlyRate;
      Decimal? burdenRate = row.BurdenRate;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) (burdenRate.HasValue ? new Decimal?(num + burdenRate.GetValueOrDefault()) : new Decimal?());
      pxCache.SetValueExt<PMLaborCostRate.rate>((object) pmLaborCostRate, (object) local);
    }
    else
    {
      sender.SetValue<PMLaborCostRate.wageRate>((object) row, (object) 0M);
      sender.SetValueExt<PMLaborCostRate.rate>((object) row, (object) hourlyRate);
    }
  }

  public static Decimal CalculateHourlyRate(Decimal? hours, Decimal? salary)
  {
    return hours.GetValueOrDefault() == 0M ? 0M : Math.Round(salary.GetValueOrDefault() / 52M / hours.Value, 2, MidpointRounding.ToEven);
  }

  protected virtual void WageRateUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMLaborCostRate row))
      return;
    Decimal? wageRate = row.WageRate;
    Decimal? rate = row.Rate;
    if (!(wageRate.GetValueOrDefault() > rate.GetValueOrDefault() & wageRate.HasValue & rate.HasValue))
      return;
    sender.SetValueExt<PMLaborCostRate.rate>((object) row, (object) row.WageRate);
  }

  protected virtual void RateUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMLaborCostRate row))
      return;
    Decimal? wageRate = row.WageRate;
    Decimal? rate = row.Rate;
    if (!(wageRate.GetValueOrDefault() > rate.GetValueOrDefault() & wageRate.HasValue & rate.HasValue))
      return;
    if (row.EmploymentType == "H")
      sender.SetValueExt<PMLaborCostRate.wageRate>((object) row, (object) row.Rate);
    else
      sender.SetValueExt<PMLaborCostRate.rate>((object) row, (object) row.WageRate);
  }
}
