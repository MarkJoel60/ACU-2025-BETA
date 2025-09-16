// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRSingleProcessMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.DR;

public class DRSingleProcessMultipleBaseCurrencies : PXGraphExtension<DRSingleProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXOverride]
  public void SetFairValueSalesPrice(
    DRScheduleDetail scheduleDetail,
    PX.Objects.CR.Location location,
    PX.Objects.CM.CurrencyInfo currencyInfo,
    DRSingleProcessMultipleBaseCurrencies.SetFairValueSalesPriceDelegate baseMethod)
  {
    bool takeInBaseCurrency = currencyInfo.CuryID == currencyInfo.BaseCuryID || ((PXSelectBase<DRSetup>) this.Base.Setup).Current.UseFairValuePricesInBaseCurrency.Value;
    DRSingleProcess.SetFairValueSalesPrice(((PXSelectBase<DRSchedule>) this.Base.Schedule).Current, scheduleDetail, (PXSelectBase<DRScheduleDetail>) this.Base.ScheduleDetail, location, currencyInfo, takeInBaseCurrency);
  }

  public delegate void SetFairValueSalesPriceDelegate(
    DRScheduleDetail scheduleDetail,
    PX.Objects.CR.Location location,
    PX.Objects.CM.CurrencyInfo currencyInfo);
}
