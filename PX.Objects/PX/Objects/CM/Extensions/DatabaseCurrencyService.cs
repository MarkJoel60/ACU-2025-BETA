// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.DatabaseCurrencyService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CM.Extensions;

public class DatabaseCurrencyService : IPXCurrencyService
{
  protected PXGraph Graph;

  public DatabaseCurrencyService(PXGraph graph) => this.Graph = graph;

  public int BaseDecimalPlaces()
  {
    return (int) ((short?) CurrencyCollection.GetBaseCurrency()?.DecimalPlaces ?? (short) 2);
  }

  public int CuryDecimalPlaces(string curyID)
  {
    return (int) ((short?) CurrencyCollection.GetCurrency(curyID)?.DecimalPlaces ?? (short) 2);
  }

  public int PriceCostDecimalPlaces()
  {
    return (int) ((short?) PXResultset<CommonSetup>.op_Implicit(PXSetup<CommonSetup>.Select(this.Graph, Array.Empty<object>()))?.DecPlPrcCst ?? (short) 2);
  }

  public int QuantityDecimalPlaces()
  {
    return (int) ((short?) PXResultset<CommonSetup>.op_Implicit(PXSetup<CommonSetup>.Select(this.Graph, Array.Empty<object>()))?.DecPlQty ?? (short) 2);
  }

  public string DefaultRateTypeID(string moduleCode)
  {
    string str = (string) null;
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
    {
      if (moduleCode == "CR")
      {
        str = PXSetup<CRSetup>.Select(this.Graph, Array.Empty<object>())?.TopFirst.DefaultRateTypeID;
      }
      else
      {
        CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSetup<CMSetup>.Select(this.Graph, Array.Empty<object>()));
        if (cmSetup != null)
        {
          switch (moduleCode)
          {
            case "CA":
              str = cmSetup.CARateTypeDflt;
              break;
            case "AP":
            case "PO":
              str = cmSetup.APRateTypeDflt;
              break;
            case "AR":
              str = cmSetup.ARRateTypeDflt;
              break;
            case "GL":
              str = cmSetup.GLRateTypeDflt;
              break;
            case "PM":
              str = cmSetup.PMRateTypeDflt;
              break;
            default:
              str = (string) null;
              break;
          }
        }
      }
    }
    return str;
  }

  public IPXCurrencyRate GetRate(
    string fromCuryID,
    string toCuryID,
    string rateTypeID,
    DateTime? curyEffDate)
  {
    return (IPXCurrencyRate) PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyInfo.baseCuryID>>, And<CurrencyRate.fromCuryID, Equal<Required<CurrencyInfo.curyID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyInfo.curyRateTypeID>>, And<CurrencyRate.curyEffDate, LessEqual<Required<CurrencyInfo.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.SelectWindowed(this.Graph, 0, 1, new object[4]
    {
      (object) toCuryID,
      (object) fromCuryID,
      (object) rateTypeID,
      (object) curyEffDate
    }));
  }

  public int GetRateEffDays(string rateTypeID)
  {
    return (int) ((short?) PXResultset<CurrencyRateType>.op_Implicit(PXSelectBase<CurrencyRateType, PXSelect<CurrencyRateType, Where<CurrencyRateType.curyRateTypeID, Equal<Required<CurrencyRateType.curyRateTypeID>>>>.Config>.Select(this.Graph, new object[1]
    {
      (object) rateTypeID
    }))?.RateEffDays).GetValueOrDefault();
  }

  /// <summary>Returns base currency of the tenant.</summary>
  public string BaseCuryID() => this.BaseCuryID(new int?());

  /// <summary>
  /// Returns base currency of the branch or base currency of the tenant if branchID is null.
  /// </summary>
  public string BaseCuryID(int? branchID)
  {
    string baseCuryId = PXAccess.GetBranch(branchID)?.BaseCuryID;
    if (baseCuryId != null)
      return baseCuryId;
    return CurrencyCollection.GetBaseCurrency()?.CuryID;
  }

  public IEnumerable<IPXCurrency> Currencies()
  {
    foreach (PXResult<Currency> pxResult in PXSelectBase<Currency, PXSelect<Currency>.Config>.Select(this.Graph, Array.Empty<object>()))
      yield return (IPXCurrency) PXResult<Currency>.op_Implicit(pxResult);
  }

  public IEnumerable<IPXCurrencyRateType> CurrencyRateTypes()
  {
    foreach (PXResult<CurrencyRateType> pxResult in PXSelectBase<CurrencyRateType, PXSelect<CurrencyRateType>.Config>.Select(this.Graph, Array.Empty<object>()))
      yield return (IPXCurrencyRateType) PXResult<CurrencyRateType>.op_Implicit(pxResult);
  }

  public void PopulatePrecision(PXCache cache, CurrencyInfo info)
  {
    if (info == null || info.CuryPrecision.HasValue && info.BasePrecision.HasValue)
      return;
    if (!info.CuryPrecision.HasValue)
      info.CuryPrecision = new short?(Convert.ToInt16(this.CuryDecimalPlaces(info.CuryID)));
    if (!info.BasePrecision.HasValue)
      info.BasePrecision = new short?(Convert.ToInt16(this.CuryDecimalPlaces(info.BaseCuryID)));
    if (cache.GetStatus((object) info) != null)
      return;
    cache.SetStatus((object) info, (PXEntryStatus) 5);
  }
}
