// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.CrossReferenceUniquenessPriceWorksheet`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// Extension that enforces uniqueness of a newly entered Alternate ID for an inventory item <see cref="T:PX.Objects.IN.InventoryItem" />
/// </summary>
[Serializable]
public class CrossReferenceUniquenessPriceWorksheet<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  protected static bool IsActiveImpl()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.crossReferenceUniqueness>();
  }

  [PXMergeAttributes]
  [CrossReferenceUniqueness(new Type[] {typeof (INNonStockItemXRef.bAccountID), typeof (INNonStockItemXRef.alternateType)})]
  protected void INNonStockItemXRefHandler(
    Events.CacheAttached<INNonStockItemXRef.alternateID> e)
  {
  }

  [PXMergeAttributes]
  [CrossReferenceUniqueness(new Type[] {typeof (INStockItemXRef.bAccountID), typeof (INStockItemXRef.alternateType)})]
  protected void INStockItemXRefHandler(
    Events.CacheAttached<INStockItemXRef.alternateID> e)
  {
  }
}
