// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.Status
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

/// <summary>
/// Attaches Status field to SOOrderShipment DAC in SOShipmentEntry graph
/// </summary>
[PXUIField(DisplayName = "Status", Enabled = false)]
public class Status : PXFieldAttachedTo<PX.Objects.SO.SOOrderShipment>.By<SOOrderEntry>.AsString.Named<Status>
{
  public override string GetValue(PX.Objects.SO.SOOrderShipment row)
  {
    if (row.ShipmentType == "H")
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.Objects.PO.POReceipt>(new PXDataField[3]
      {
        (PXDataField) new PXDataField<PX.Objects.PO.POReceipt.status>(),
        (PXDataField) new PXDataFieldValue<PX.Objects.PO.POReceipt.receiptType>((PXDbType) 3, new int?(), row.Operation == "I" ? (object) "RT" : (object) "RN", (PXComp) 0),
        (PXDataField) new PXDataFieldValue<PX.Objects.PO.POReceipt.receiptNbr>((PXDbType) 12, new int?(), (object) row.ShipmentNbr, (PXComp) 0)
      }))
        return pxDataRecord == null ? (string) null : new POReceiptStatus.ListAttribute().ValueLabelDic[pxDataRecord.GetString(0)];
    }
    using (new PXReadThroughArchivedScope())
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.Objects.SO.SOShipment>(new PXDataField[3]
      {
        (PXDataField) new PXDataField<PX.Objects.SO.SOShipment.status>(),
        (PXDataField) new PXDataFieldValue<PX.Objects.SO.SOShipment.shipmentType>((PXDbType) 3, new int?(), (object) row.ShipmentType, (PXComp) 0),
        (PXDataField) new PXDataFieldValue<PX.Objects.SO.SOShipment.shipmentNbr>((PXDbType) 12, new int?(), (object) row.ShipmentNbr, (PXComp) 0)
      }))
        return pxDataRecord != null ? new SOShipmentStatus.ListAttribute().ValueLabelDic[pxDataRecord.GetString(0)] : PXMessages.LocalizeNoPrefix("Auto-Generated");
    }
  }
}
