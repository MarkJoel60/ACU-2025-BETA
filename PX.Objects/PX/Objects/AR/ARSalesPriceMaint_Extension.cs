// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSalesPriceMaint_Extension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.DAC;
using System;

#nullable disable
namespace PX.Objects.AR;

public class ARSalesPriceMaint_Extension : PXGraphExtension<ARSalesPriceMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  [PXOverride]
  public ARSalesPriceMaint.SalesPriceItem SelectCustomItemPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    string lotSerialNbr,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool isFairValue,
    string taxCalcMode,
    ARSalesPriceMaint_Extension.SelectCustomItemPriceDelegate baseMethod)
  {
    if (lotSerialNbr == null)
      return (ARSalesPriceMaint.SalesPriceItem) null;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID);
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find(sender.Graph, inventoryItem?.LotSerClassID);
    if (inventoryItem == null || inLotSerClass == null || !inLotSerClass.UseLotSerSpecificDetails.GetValueOrDefault())
      return (ARSalesPriceMaint.SalesPriceItem) null;
    INItemLotSerialAttributesHeader attributesHeader = INItemLotSerialAttributesHeader.PK.Find(sender.Graph, inventoryID, lotSerialNbr);
    return attributesHeader != null ? new ARSalesPriceMaint.SalesPriceItem(inventoryItem.BaseUnit, attributesHeader.SalesPrice.GetValueOrDefault(), baseCuryID) : (ARSalesPriceMaint.SalesPriceItem) null;
  }

  protected virtual void ARSalesPrice_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation == 3)
      return;
    ARSalesPrice row = (ARSalesPrice) e.Row;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, (int?) row?.InventoryID);
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find(sender.Graph, inventoryItem?.LotSerClassID);
    if ((inLotSerClass != null ? (inLotSerClass.UseLotSerSpecificDetails.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) row, "The sales price cannot be saved because the Specify Lot/Serial Price and Description check box is selected for the lot or serial class of the {0} item on the Lot/Serial Classes (IN207000) form. Clear the check box, or specify the price for the lot or serial numbers of the item on the Lot/Serial Details form.", new object[1]
    {
      (object) inventoryItem.InventoryCD
    });
    PXUIFieldAttribute.SetError<ARSalesPrice.inventoryID>(sender, (object) row, ((Exception) propertyException).Message);
  }

  public delegate ARSalesPriceMaint.SalesPriceItem SelectCustomItemPriceDelegate(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    string lotSerialNbr,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool isFairValue,
    string taxCalcMode);
}
