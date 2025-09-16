// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPriceWorksheetMaintLotSerialExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.AR;

public class ARPriceWorksheetMaintLotSerialExt : PXGraphExtension<ARPriceWorksheetMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  protected virtual void ARPriceWorksheetDetail_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    if (e.Operation == 3)
      return;
    ARPriceWorksheetDetail row = (ARPriceWorksheetDetail) e.Row;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, (int?) row?.InventoryID);
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find(sender.Graph, inventoryItem?.LotSerClassID);
    if ((inLotSerClass != null ? (inLotSerClass.UseLotSerSpecificDetails.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) row, "The sales price cannot be saved because the Specify Lot/Serial Price and Description check box is selected for the lot or serial class of the {0} item on the Lot/Serial Classes (IN207000) form. Clear the check box, or specify the price for the lot or serial numbers of the item on the Lot/Serial Details form.", new object[1]
    {
      (object) inventoryItem.InventoryCD
    });
    PXUIFieldAttribute.SetError<ARPriceWorksheetDetail.inventoryID>(sender, (object) row, ((Exception) propertyException).Message);
  }
}
