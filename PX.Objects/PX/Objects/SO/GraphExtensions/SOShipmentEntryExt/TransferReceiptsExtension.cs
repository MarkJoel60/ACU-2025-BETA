// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.TransferReceiptsExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class TransferReceiptsExtension : PXGraphExtension<SOShipmentEntry>
{
  public PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Empty>.Where<Exists<SelectFromBase<POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<POReceiptLine.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<POReceiptLine.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, POReceiptLine>, PX.Objects.PO.POReceipt, POReceiptLine>.And<KeysRelation<CompositeKey<Field<POReceiptLine.sOShipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<POReceiptLine.sOShipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, POReceiptLine>, PX.Objects.SO.SOShipment, POReceiptLine>.SameAsCurrent>>>>>.ReadOnly RelatedTransferReceipts;

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Receipt Type")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POReceipt.receiptType> e)
  {
  }
}
