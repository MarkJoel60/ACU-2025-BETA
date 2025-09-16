// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.POReceiptTransferLineSplittingExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

[PXProtectedAccess(typeof (POReceiptLineSplittingExtension))]
public abstract class POReceiptTransferLineSplittingExtension : 
  TransferLineSplittingExtension<POReceiptEntry, POReceiptLineSplittingExtension, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine, POReceiptLineSplit>
{
  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (POLotSerialNbrAttribute))]
  [POTransferLotSerialNbr(typeof (PX.Objects.PO.POReceiptLine.inventoryID), typeof (PX.Objects.PO.POReceiptLine.subItemID), typeof (PX.Objects.PO.POReceiptLine.locationID), typeof (PX.Objects.PO.POReceiptLine.costCenterID), typeof (PX.Objects.PO.POReceiptLine.tranType), typeof (PX.Objects.PO.POReceiptLine.origRefNbr), typeof (PX.Objects.PO.POReceiptLine.origLineNbr))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POReceiptLine.lotSerialNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (POLotSerialNbrAttribute))]
  [POTransferLotSerialNbr(typeof (POReceiptLineSplit.inventoryID), typeof (POReceiptLineSplit.subItemID), typeof (POReceiptLineSplit.locationID), typeof (PX.Objects.PO.POReceiptLine.costCenterID), typeof (POReceiptLineSplit.tranType), typeof (PX.Objects.PO.POReceiptLine.origRefNbr), typeof (PX.Objects.PO.POReceiptLine.origLineNbr), typeof (PX.Objects.PO.POReceiptLine.lotSerialNbr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POReceiptLineSplit.lotSerialNbr> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt> e)
  {
    if (e.Row == null)
      return;
    bool isTransferReceipt = e.Row.ReceiptType == "RX";
    PXCacheEx.AttributeAdjuster<INExpireDateAttribute> attributeAdjuster = PXCacheEx.Adjust<INExpireDateAttribute>((PXCache) this.LineCache, (object) null);
    attributeAdjuster.For<PX.Objects.PO.POReceiptLine.expireDate>((Action<INExpireDateAttribute>) (a => a.ForceDisable = isTransferReceipt));
    attributeAdjuster = PXCacheEx.Adjust<INExpireDateAttribute>((PXCache) this.SplitCache, (object) null);
    attributeAdjuster.For<POReceiptLineSplit.expireDate>((Action<INExpireDateAttribute>) (a => a.ForceDisable = isTransferReceipt));
  }

  protected override void VerifyUnassignedQty(PX.Objects.PO.POReceiptLine line)
  {
  }
}
