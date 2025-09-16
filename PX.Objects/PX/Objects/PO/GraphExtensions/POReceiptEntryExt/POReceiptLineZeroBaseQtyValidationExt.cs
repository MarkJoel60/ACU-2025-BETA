// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.POReceiptLineZeroBaseQtyValidationExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class POReceiptLineZeroBaseQtyValidationExt : 
  TransactionZeroBaseQtyValidationExtension<POReceiptEntry, POReceipt, POReceipt.hold>
{
  protected override TransactionLineQtyMapping GetTranLineMapping()
  {
    return new TransactionLineQtyMapping(typeof (POReceiptLine))
    {
      Qty = typeof (POReceiptLine.receiptQty),
      BaseQty = typeof (POReceiptLine.baseReceiptQty),
      InventoryID = typeof (POReceiptLine.inventoryID)
    };
  }

  public override bool PreventSaveOnHold => true;
}
