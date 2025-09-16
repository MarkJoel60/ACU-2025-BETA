// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryLotSerialExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.DAC;

#nullable disable
namespace PX.Objects.SO.GraphExtensions;

public class SOInvoiceEntryLotSerialExtension : PXGraphExtension<SOInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  protected virtual void ARTran_LotSerialNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PX.Objects.AR.ARTran row = (PX.Objects.AR.ARTran) e.Row;
    if (row == null || row.LotSerialNbr == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, row.InventoryID);
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find(sender.Graph, inventoryItem?.LotSerClassID);
    if ((inLotSerClass != null ? (inLotSerClass.UseLotSerSpecificDetails.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    sender.SetDefaultExt<PX.Objects.AR.ARTran.curyUnitPrice>((object) row);
    INItemLotSerialAttributesHeader attributesHeader = INItemLotSerialAttributesHeader.PK.Find(sender.Graph, row.InventoryID, row.LotSerialNbr);
    if (attributesHeader == null)
      return;
    sender.SetValueExt<PX.Objects.AR.ARTran.tranDesc>((object) row, (object) attributesHeader.Descr);
  }
}
