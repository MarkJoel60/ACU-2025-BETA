// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineSuppressValidationWithManufacturingExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO;

/// <exclude />
public class SOLineSuppressValidationWithManufacturingExt : PXCacheExtension<SOLine>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.manufacturingProductConfigurator>() || PXAccess.FeatureInstalled<FeaturesSet.manufacturingEstimating>();
  }

  [PXCustomizeBaseAttribute(typeof (PXFormulaAttribute), "ValidateAggregateCalculation", false)]
  public virtual Decimal? OrderQty { get; set; }
}
