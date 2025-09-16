// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.UseLotSerialSpecificDetailsPurchaseSupplyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

[PXProtectedAccess(typeof (SOOrderLineSplittingExtension))]
public abstract class UseLotSerialSpecificDetailsPurchaseSupplyExt : 
  PXGraphExtension<PurchaseSupplyBaseExt, SOOrderLineSplittingExtension, SOOrderEntry>
{
  public static bool IsActive()
  {
    return PurchaseSupplyBaseExt.IsActive() && PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.PurchaseSupplyBaseExt.IsPOCreateEnabled(PX.Objects.SO.SOLine)" />.
  /// Disables Mark for PO check box in lines with items that have lot/serial class with checked Specify Lot/Serial Price and Description checkbox.
  /// </summary>
  [PXOverride]
  public bool IsPOCreateEnabled(PX.Objects.SO.SOLine row, Func<PX.Objects.SO.SOLine, bool> baseMethod)
  {
    if (baseMethod(row))
    {
      bool? nullable = ((PXSelectBase<PX.Objects.SO.SOOrderType>) ((PXGraphExtension<SOOrderEntry>) this).Base.soordertype).Current.RequireShipping;
      if (nullable.GetValueOrDefault() || ((PXSelectBase<PX.Objects.SO.SOOrderType>) ((PXGraphExtension<SOOrderEntry>) this).Base.soordertype).Current.Behavior == "BL")
      {
        INLotSerClass inLotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(row.InventoryID));
        if (inLotSerClass == null)
          return true;
        nullable = inLotSerClass.UseLotSerSpecificDetails;
        return !nullable.GetValueOrDefault();
      }
    }
    return false;
  }

  [PXProtectedAccess(null)]
  protected abstract PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> ReadInventoryItem(
    int? inventoryID);
}
