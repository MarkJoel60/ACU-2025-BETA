// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLineInventoryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PO;

[PXDBInt]
[PXUIField]
public class POReceiptLineInventoryAttribute : CrossItemAttribute
{
  public POReceiptLineInventoryAttribute(Type receiptType)
    : base(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<Match<Current<AccessInfo.userName>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), INPrimaryAlternateType.VPN)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(BqlTemplate.OfCondition<Where2<Where<Current2<BqlPlaceholder.A>, Equal<POReceiptType.transferreceipt>, Or<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noPurchases>>>, And2<Not<FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>>, Or<PX.Objects.IN.InventoryItem.nonStockReceiptAsService, Equal<True>>>>>.Replace<BqlPlaceholder.A>(receiptType).ToType(), "Item cannot be purchased", Array.Empty<Type>()));
  }
}
