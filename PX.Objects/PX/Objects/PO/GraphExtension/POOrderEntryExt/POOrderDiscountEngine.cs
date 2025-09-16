// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtension.POOrderEntryExt.POOrderDiscountEngine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Discount;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PO.GraphExtension.POOrderEntryExt;

public class POOrderDiscountEngine : PXGraphExtension<DiscountEngine<POLine, POOrderDiscountDetail>>
{
  public static bool IsActive() => !PXAccess.FeatureInstalled<FeaturesSet.vendorDiscounts>();

  [PXOverride]
  public virtual void CalculateDocumentDiscountRate(
    PXCache cache,
    PXSelectBase<POLine> documentDetails,
    POLine currentLine,
    PXSelectBase<POOrderDiscountDetail> discountDetails,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    bool forceFormulaCalculation,
    Action<PXCache, PXSelectBase<POLine>, POLine, PXSelectBase<POOrderDiscountDetail>, DiscountEngine.DiscountCalculationOptions, bool> baseMethod)
  {
    if (!this.Base.IsDocumentDiscountRateCalculationNeeded(cache, currentLine, discountDetails))
      return;
    baseMethod(cache, documentDetails, currentLine, discountDetails, discountCalculationOptions, forceFormulaCalculation);
  }
}
