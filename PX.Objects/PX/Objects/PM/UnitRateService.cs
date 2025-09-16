// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.UnitRateService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.EP;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PM;

public class UnitRateService : IUnitRateService
{
  public virtual Decimal? CalculateUnitPrice(
    PXCache sender,
    int? projectID,
    int? projectTaskID,
    int? inventoryID,
    string UOM,
    Decimal? qty,
    DateTime? date,
    long? curyInfoID)
  {
    if (inventoryID.HasValue)
    {
      int? nullable = inventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(nullable.GetValueOrDefault() == emptyInventoryId & nullable.HasValue))
      {
        if (!date.HasValue)
          date = sender.Graph.Accessinfo.BusinessDate;
        string str = "BASE";
        if (projectTaskID.HasValue)
        {
          PMTask dirty = PMTask.PK.FindDirty(sender.Graph, projectID, projectTaskID);
          PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>.Config>.Select(sender.Graph, new object[1]
          {
            (object) dirty.LocationID
          }));
          if (location != null && !string.IsNullOrEmpty(location.CPriceClassID))
            str = location.CPriceClassID;
        }
        PMProject project = PMProject.PK.Find(sender.Graph, projectID);
        PX.Objects.CM.CurrencyInfo currencyInfo1 = (PX.Objects.CM.CurrencyInfo) null;
        if (curyInfoID.HasValue)
          currencyInfo1 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select(sender.Graph, new object[1]
          {
            (object) curyInfoID
          }));
        if (currencyInfo1 != null)
        {
          PXCache cach = sender.Graph.Caches[typeof (PMTran)];
          string custPriceClass = str;
          int? customerID;
          if (project == null)
          {
            nullable = new int?();
            customerID = nullable;
          }
          else
            customerID = project.CustomerID;
          int? inventoryID1 = inventoryID;
          nullable = new int?();
          int? siteID = nullable;
          PX.Objects.CM.CurrencyInfo currencyinfo = currencyInfo1;
          string UOM1 = UOM;
          Decimal? quantity = qty;
          DateTime date1 = date.Value;
          Decimal? currentUnitPrice = new Decimal?(0M);
          return ARSalesPriceMaint.CalculateSalesPrice(cach, custPriceClass, customerID, inventoryID1, siteID, currencyinfo, UOM1, quantity, date1, currentUnitPrice);
        }
        string projectBaseCuryId = UnitRateService.GetProjectBaseCuryID(project, sender);
        PX.Objects.CM.CurrencyInfo currencyInfo2 = new PX.Objects.CM.CurrencyInfo();
        currencyInfo2.CuryID = project?.CuryID ?? projectBaseCuryId;
        currencyInfo2.BaseCuryID = projectBaseCuryId;
        ARSalesPriceMaint arSalesPriceMaint = ARSalesPriceMaint.SingleARSalesPriceMaint;
        bool baseCurrencySetting = arSalesPriceMaint.GetAlwaysFromBaseCurrencySetting(sender);
        PXCache sender1 = sender;
        string custPriceClass1 = str;
        int? customerID1;
        if (project == null)
        {
          nullable = new int?();
          customerID1 = nullable;
        }
        else
          customerID1 = project.CustomerID;
        int? inventoryID2 = inventoryID;
        nullable = new int?();
        int? siteID1 = nullable;
        PX.Objects.CM.CurrencyInfo currencyinfo1 = currencyInfo2;
        Decimal? quantity1 = qty;
        string UOM2 = UOM;
        DateTime date2 = date.Value;
        int num1 = baseCurrencySetting ? 1 : 0;
        ARSalesPriceMaint.SalesPriceItem salesPriceItem = arSalesPriceMaint.CalculateSalesPriceItem(sender1, custPriceClass1, customerID1, inventoryID2, siteID1, currencyinfo1, quantity1, UOM2, date2, num1 != 0, false);
        if (salesPriceItem != null)
        {
          Decimal price = salesPriceItem.Price;
          if (salesPriceItem.UOM != UOM)
          {
            Decimal num2 = INUnitAttribute.ConvertFromBase(sender, inventoryID, salesPriceItem.UOM, price, INPrecision.UNITCOST);
            price = INUnitAttribute.ConvertToBase(sender, inventoryID, UOM, num2, INPrecision.UNITCOST);
          }
          return new Decimal?(price);
        }
      }
    }
    return new Decimal?();
  }

  public virtual Decimal? CalculateUnitCost(
    PXCache sender,
    int? projectID,
    int? projectTaskID,
    int? inventoryID,
    string UOM,
    int? employeeID,
    DateTime? date,
    long? curyInfoID)
  {
    if (inventoryID.HasValue)
    {
      int? nullable1 = inventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(nullable1.GetValueOrDefault() == emptyInventoryId & nullable1.HasValue))
      {
        if (!date.HasValue)
          date = sender.Graph.Accessinfo.BusinessDate;
        bool flag = employeeID.HasValue;
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID);
        PMProject project = PMProject.PK.Find(sender.Graph, projectID);
        int? nullable2 = new int?();
        if (inventoryItem != null)
        {
          if (inventoryItem.ItemType == "L")
            nullable2 = inventoryItem.InventoryID;
          else
            flag = false;
        }
        PX.Objects.CM.CurrencyInfo currencyInfo = this.GetCurrencyInfo(sender, curyInfoID, UnitRateService.GetProjectBaseCuryID(project, sender));
        Decimal baseval = 0M;
        string fromCuryID = currencyInfo.BaseCuryID;
        if (flag)
        {
          if (!nullable2.HasValue && !inventoryID.HasValue)
          {
            EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select(sender.Graph, new object[1]
            {
              (object) employeeID
            }));
            if (epEmployee != null)
              nullable2 = epEmployee.LabourItemID;
          }
          EmployeeCostEngine employeeCostEngine1 = this.CreateEmployeeCostEngine(sender);
          string regulatHoursType1 = this.GetRegulatHoursType(sender);
          int? laborItemID1 = nullable2;
          int? projectID1 = projectID;
          int? projectTaskID1 = projectTaskID;
          bool? certifiedJob1 = project?.CertifiedJob;
          int? employeeID1 = employeeID;
          DateTime date1 = date ?? DateTime.Now;
          nullable1 = new int?();
          int? shiftID1 = nullable1;
          EmployeeCostEngine.LaborCost employeeCost = employeeCostEngine1.CalculateEmployeeCost((string) null, regulatHoursType1, laborItemID1, projectID1, projectTaskID1, certifiedJob1, (string) null, employeeID1, date1, shiftID1);
          if (employeeCost == null && nullable2.HasValue)
          {
            EmployeeCostEngine employeeCostEngine2 = this.CreateEmployeeCostEngine(sender);
            string regulatHoursType2 = this.GetRegulatHoursType(sender);
            nullable1 = new int?();
            int? laborItemID2 = nullable1;
            int? projectID2 = projectID;
            int? projectTaskID2 = projectTaskID;
            bool? certifiedJob2 = project?.CertifiedJob;
            int? employeeID2 = employeeID;
            DateTime date2 = date ?? DateTime.Now;
            nullable1 = new int?();
            int? shiftID2 = nullable1;
            employeeCost = employeeCostEngine2.CalculateEmployeeCost((string) null, regulatHoursType2, laborItemID2, projectID2, projectTaskID2, certifiedJob2, (string) null, employeeID2, date2, shiftID2);
          }
          if (employeeCost == null && nullable2.HasValue)
          {
            EmployeeCostEngine employeeCostEngine3 = this.CreateEmployeeCostEngine(sender);
            string regulatHoursType3 = this.GetRegulatHoursType(sender);
            int? laborItemID3 = nullable2;
            int? projectID3 = projectID;
            int? projectTaskID3 = projectTaskID;
            bool? certifiedJob3 = project?.CertifiedJob;
            nullable1 = new int?();
            int? employeeID3 = nullable1;
            DateTime date3 = date ?? DateTime.Now;
            nullable1 = new int?();
            int? shiftID3 = nullable1;
            employeeCost = employeeCostEngine3.CalculateEmployeeCost((string) null, regulatHoursType3, laborItemID3, projectID3, projectTaskID3, certifiedJob3, (string) null, employeeID3, date3, shiftID3);
          }
          if (employeeCost != null)
          {
            Decimal valueOrDefault = employeeCost.Rate.GetValueOrDefault();
            baseval = valueOrDefault;
            fromCuryID = employeeCost.CuryID;
            if (inventoryID.HasValue || nullable2.HasValue)
            {
              PXCache sender1 = sender;
              nullable1 = inventoryID;
              int? InventoryID = nullable1 ?? nullable2;
              string FromUnit = UOM ?? "HOUR";
              Decimal num = valueOrDefault;
              baseval = INUnitAttribute.ConvertToBase(sender1, InventoryID, FromUnit, num, INPrecision.UNITCOST);
            }
          }
          else if (nullable2.HasValue && inventoryItem != null)
          {
            Decimal valueOrDefault = ((Decimal?) InventoryItemCurySettings.PK.Find(sender.Graph, inventoryItem.InventoryID, currencyInfo.BaseCuryID)?.StdCost).GetValueOrDefault();
            PXCache sender2 = sender;
            nullable1 = inventoryID;
            int? InventoryID = nullable1 ?? nullable2;
            string FromUnit = UOM ?? "HOUR";
            Decimal num = valueOrDefault;
            baseval = INUnitAttribute.ConvertToBase(sender2, InventoryID, FromUnit, num, INPrecision.UNITCOST);
          }
        }
        else if (inventoryItem != null)
        {
          Decimal num;
          if (inventoryItem.ItemType == "L")
          {
            EmployeeCostEngine employeeCostEngine = this.CreateEmployeeCostEngine(sender);
            string regulatHoursType = this.GetRegulatHoursType(sender);
            int? laborItemID = inventoryID;
            int? projectID4 = projectID;
            int? projectTaskID4 = projectTaskID;
            bool? certifiedJob = project?.CertifiedJob;
            nullable1 = new int?();
            int? employeeID4 = nullable1;
            DateTime date4 = date ?? DateTime.Now;
            nullable1 = new int?();
            int? shiftID = nullable1;
            EmployeeCostEngine.LaborCost employeeCost = employeeCostEngine.CalculateEmployeeCost((string) null, regulatHoursType, laborItemID, projectID4, projectTaskID4, certifiedJob, (string) null, employeeID4, date4, shiftID);
            if (employeeCost != null)
            {
              num = employeeCost.Rate.GetValueOrDefault();
              fromCuryID = employeeCost.CuryID;
            }
            else
              num = UnitRateService.GetUnitCostForDate(InventoryItemCurySettings.PK.Find(sender.Graph, inventoryItem.InventoryID, currencyInfo.BaseCuryID), date);
          }
          else
            num = !inventoryItem.StkItem.GetValueOrDefault() ? UnitRateService.GetUnitCostForDate(InventoryItemCurySettings.PK.Find(sender.Graph, inventoryItem.InventoryID, currencyInfo.BaseCuryID), date) : ((Decimal?) INItemCost.PK.Find(sender.Graph, inventoryID, currencyInfo.BaseCuryID)?.AvgCost).GetValueOrDefault();
          baseval = INUnitAttribute.ConvertToBase(sender, inventoryID, UOM, num, INPrecision.UNITCOST);
        }
        Decimal curyval;
        if (fromCuryID != currencyInfo.BaseCuryID)
        {
          if (fromCuryID == currencyInfo.CuryID)
          {
            curyval = baseval;
          }
          else
          {
            curyval = 0M;
            IPXCurrencyService pxCurrencyService = ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(sender.Graph);
            IPXCurrencyRate rate = pxCurrencyService.GetRate(fromCuryID, currencyInfo.CuryID, currencyInfo.CuryRateTypeID, new DateTime?(currencyInfo.CuryEffDate ?? DateTime.Now));
            if (rate != null)
            {
              int num = pxCurrencyService.CuryDecimalPlaces(project.CuryID);
              curyval = this.CuryConvCury(rate, baseval, new int?(num));
            }
          }
        }
        else
          PX.Objects.CM.PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(sender, currencyInfo, baseval, out curyval);
        return new Decimal?(curyval);
      }
    }
    return new Decimal?();
  }

  protected virtual bool GetAlwaysFromBaseCurrencySetting(PXCache sender)
  {
    ARSetup arSetup = (ARSetup) sender.Graph.Caches[typeof (ARSetup)].Current ?? PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
    return arSetup != null && arSetup.AlwaysFromBaseCury.GetValueOrDefault();
  }

  protected virtual PX.Objects.CM.CurrencyInfo GetCurrencyInfo(
    PXCache sender,
    long? curyInfoID,
    string projectBaseCuryID)
  {
    PX.Objects.CM.CurrencyInfo currencyInfo;
    if (curyInfoID.HasValue)
    {
      currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) curyInfoID
      }));
      if (currencyInfo == null)
        currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select(sender.Graph, new object[1]
        {
          (object) curyInfoID
        })).GetCM();
    }
    else
    {
      currencyInfo = new PX.Objects.CM.CurrencyInfo();
      currencyInfo.CuryID = projectBaseCuryID;
      currencyInfo.BaseCuryID = projectBaseCuryID;
      currencyInfo.CuryRate = new Decimal?((Decimal) 1);
    }
    return currencyInfo;
  }

  protected virtual EmployeeCostEngine CreateEmployeeCostEngine(PXCache sender)
  {
    return new EmployeeCostEngine(sender.Graph);
  }

  protected virtual string GetRegulatHoursType(PXCache sender)
  {
    EPSetup epSetup = PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelect<EPSetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
    return epSetup != null ? epSetup.RegularHoursType : "RG";
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

  protected static string GetProjectBaseCuryID(PMProject project, PXCache cache)
  {
    return project?.BaseCuryID ?? cache.Graph.Accessinfo.BaseCuryID;
  }

  protected static Decimal GetUnitCostForDate(
    InventoryItemCurySettings inventoryItemCurySettings,
    DateTime? date)
  {
    if (inventoryItemCurySettings == null)
      return 0M;
    if (!date.HasValue)
      return inventoryItemCurySettings.StdCost.GetValueOrDefault();
    DateTime? nullable = date;
    DateTime? stdCostDate = inventoryItemCurySettings.StdCostDate;
    return ((nullable.HasValue & stdCostDate.HasValue ? (nullable.GetValueOrDefault() < stdCostDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? inventoryItemCurySettings.LastStdCost : inventoryItemCurySettings.StdCost).GetValueOrDefault();
  }
}
