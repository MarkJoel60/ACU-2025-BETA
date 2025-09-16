// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderLineZeroBaseQtyValidationExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SOOrderLineZeroBaseQtyValidationExt : 
  TransactionZeroBaseQtyValidationExtension<SOOrderEntry, SOOrder, SOOrder.hold>
{
  protected override TransactionLineQtyMapping GetTranLineMapping()
  {
    return new TransactionLineQtyMapping(typeof (SOLine))
    {
      Qty = typeof (SOLine.orderQty),
      BaseQty = typeof (SOLine.baseOrderQty),
      InventoryID = typeof (SOLine.inventoryID)
    };
  }
}
