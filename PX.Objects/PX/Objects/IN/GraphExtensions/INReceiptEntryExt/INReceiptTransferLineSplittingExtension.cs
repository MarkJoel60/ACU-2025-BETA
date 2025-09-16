// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INReceiptEntryExt.INReceiptTransferLineSplittingExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INReceiptEntryExt;

[PXProtectedAccess(typeof (INReceiptEntry.LineSplittingExtension))]
public abstract class INReceiptTransferLineSplittingExtension : 
  TransferLineSplittingExtension<INReceiptEntry, INReceiptEntry.LineSplittingExtension, INRegister, INTran, INTranSplit>
{
  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (INLotSerialNbrAttribute))]
  [TransferLotSerialNbr(typeof (INTran.inventoryID), typeof (INTran.subItemID), typeof (INTran.locationID), typeof (INTran.costCenterID), typeof (INTran.tranType), typeof (INTran.origRefNbr), typeof (INTran.origLineNbr))]
  protected virtual void _(Events.CacheAttached<INTran.lotSerialNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (INLotSerialNbrAttribute))]
  [TransferLotSerialNbr(typeof (INTranSplit.inventoryID), typeof (INTranSplit.subItemID), typeof (INTranSplit.locationID), typeof (INTran.costCenterID), typeof (INTranSplit.tranType), typeof (INTran.origRefNbr), typeof (INTran.origLineNbr), typeof (INTran.lotSerialNbr))]
  protected virtual void _(Events.CacheAttached<INTranSplit.lotSerialNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXDBQuantityAttribute), "MinValue", 0.0)]
  protected virtual void _(Events.CacheAttached<INTranSplit.qty> e)
  {
  }

  protected virtual void _(Events.RowSelected<INRegister> e)
  {
    if (e.Row == null)
      return;
    bool isTransferReceipt = e.Row.TransferNbr != null;
    PXCacheEx.AttributeAdjuster<INExpireDateAttribute> attributeAdjuster = PXCacheEx.Adjust<INExpireDateAttribute>((PXCache) this.LineCache, (object) null);
    attributeAdjuster.For<INTran.expireDate>((Action<INExpireDateAttribute>) (a => a.ForceDisable = isTransferReceipt));
    attributeAdjuster = PXCacheEx.Adjust<INExpireDateAttribute>((PXCache) this.SplitCache, (object) null);
    attributeAdjuster.For<INTranSplit.expireDate>((Action<INExpireDateAttribute>) (a => a.ForceDisable = isTransferReceipt));
  }
}
