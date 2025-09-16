// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.GraphExtensions.APTranZeroBaseQtyValidationExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;

#nullable disable
namespace PX.Objects.AP.GraphExtensions;

public class APTranZeroBaseQtyValidationExt : 
  TransactionZeroBaseQtyValidationExtension<APInvoiceEntry, APInvoice, APInvoice.hold>
{
  protected override TransactionLineQtyMapping GetTranLineMapping()
  {
    return new TransactionLineQtyMapping(typeof (APTran))
    {
      Qty = typeof (APTran.qty),
      BaseQty = typeof (APTran.baseQty),
      InventoryID = typeof (APTran.inventoryID)
    };
  }
}
