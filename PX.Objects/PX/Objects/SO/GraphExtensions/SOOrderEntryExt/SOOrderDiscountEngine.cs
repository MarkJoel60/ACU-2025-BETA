// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderDiscountEngine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Mappers;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SOOrderDiscountEngine : PXGraphExtension<DiscountEngine<SOLine, SOOrderDiscountDetail>>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  [PXOverride]
  public virtual void CalculateDocumentDiscountRate(
    PXCache cache,
    PXSelectBase<SOLine> documentDetails,
    SOLine currentLine,
    PXSelectBase<SOOrderDiscountDetail> discountDetails,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    bool forceFormulaCalculation,
    Action<PXCache, PXSelectBase<SOLine>, SOLine, PXSelectBase<SOOrderDiscountDetail>, DiscountEngine.DiscountCalculationOptions, bool> baseMethod)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() && !this.Base.IsDocumentDiscountRateCalculationNeeded(cache, currentLine, discountDetails))
      return;
    baseMethod(cache, documentDetails, currentLine, discountDetails, discountCalculationOptions, forceFormulaCalculation);
  }

  /// Overrides <see cref="M:PX.Objects.Common.Discount.DiscountEngine`2.UpdateDocumentDiscountRate(PX.Objects.Common.Discount.Mappers.AmountLineFields,System.Nullable{System.Decimal})" />
  [PXOverride]
  public bool UpdateDocumentDiscountRate(
    AmountLineFields aLine,
    Decimal? discountRate,
    Func<AmountLineFields, Decimal?, bool> baseImpl)
  {
    int num = baseImpl(aLine, discountRate) ? 1 : 0;
    if (num == 0)
      return num != 0;
    if (aLine.Cache.Current == aLine.MappedLine)
      return num != 0;
    Margin implementation = aLine.Cache.Graph.FindImplementation<Margin>();
    if (implementation == null)
      return num != 0;
    implementation.RequestRefreshLines();
    return num != 0;
  }

  /// Overrides <see cref="M:PX.Objects.Common.Discount.DiscountEngine`2.UpdateGroupDiscountRate(PX.Objects.Common.Discount.Mappers.AmountLineFields,System.Nullable{System.Decimal})" />
  [PXOverride]
  public bool UpdateGroupDiscountRate(
    AmountLineFields aLine,
    Decimal? discountRate,
    Func<AmountLineFields, Decimal?, bool> baseImpl)
  {
    int num = baseImpl(aLine, discountRate) ? 1 : 0;
    if (num == 0)
      return num != 0;
    if (aLine.Cache.Current == aLine.MappedLine)
      return num != 0;
    Margin implementation = aLine.Cache.Graph.FindImplementation<Margin>();
    if (implementation == null)
      return num != 0;
    implementation.RequestRefreshLines();
    return num != 0;
  }
}
