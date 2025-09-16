// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.MinGrossProfitMsg
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.AR;

[PXLocalizable]
public static class MinGrossProfitMsg
{
  public const string ValidationFailed = "Minimum Gross Profit requirement is not satisfied.";
  public const string ValidationFailedAndDiscountFixed = "Minimum Gross Profit requirement is not satisfied. Discount was reduced to maximum valid value.";
  public const string ValidationFailedAndSalesPriceFixed = "Minimum Gross Profit requirement is not satisfied. Sales price was set to minimum valid value.";
  public const string ValidationFailedAndUnitPriceFixed = "Minimum Gross Profit requirement is not satisfied. Unit price was set to minimum valid value.";
  public const string ValidationFailedNoCost = "No Average or Last cost found to determine minimum valid Unit price.";
}
