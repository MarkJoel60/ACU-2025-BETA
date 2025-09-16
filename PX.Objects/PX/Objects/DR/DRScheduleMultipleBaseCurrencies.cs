// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRScheduleMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.DR;

public sealed class DRScheduleMultipleBaseCurrencies : PXCacheExtension<
#nullable disable
DRSchedule>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXString]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXRestrictor(typeof (Where<CurrencyList.curyID, IsBaseCurrency>), "The currency cannot be selected as a schedule currency, because there is no branch with the same base currency in the system.", new Type[] {})]
  [PXUIField(DisplayName = "Currency")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXDBCalced(typeof (DRSchedule.baseCuryID), typeof (string))]
  public string BaseCuryIDASC606 { get; set; }

  public abstract class baseCuryIDASC606 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRScheduleMultipleBaseCurrencies.baseCuryIDASC606>
  {
  }
}
