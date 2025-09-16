// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTranInventoryItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;

#nullable disable
namespace PX.Objects.AP;

[PXDBInt]
[PXUIField(DisplayName = "Inventory ID", Visibility = PXUIVisibility.Visible)]
[PXRestrictor(typeof (Where<Current<APSetup.migrationMode>, Equal<True>, Or<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, Or<Current<APTran.pONbr>, IsNotNull, Or<Current<APTran.receiptNbr>, IsNotNull, Or<Current<APTran.tranType>, Equal<APDocType.invoice>, Or<Current<APInvoice.isRetainageDocument>, Equal<True>>>>>>>), "It is not allowed to enter Stock Items in AP Bills directly. Please use Purchase Orders instead.", new System.Type[] {})]
[PXRestrictor(typeof (Where<Current<APSetup.migrationMode>, Equal<True>, Or<Current<APTran.pONbr>, IsNotNull, Or<PX.Objects.IN.InventoryItem.stkItem, NotEqual<False>, Or<PX.Objects.IN.InventoryItem.kitItem, NotEqual<True>>>>>), "A non-stock kit cannot be added to an AP bill manually. Use the Purchase Orders (PO301000) form to prepare an AP bill for the corresponding purchase order.", new System.Type[] {})]
public class APTranInventoryItemAttribute : InventoryAttribute
{
  public APTranInventoryItemAttribute()
    : base(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Data.Match<Current<AccessInfo.userName>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr))
  {
  }
}
