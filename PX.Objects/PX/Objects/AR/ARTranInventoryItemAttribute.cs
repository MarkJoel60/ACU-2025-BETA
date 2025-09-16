// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranInventoryItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.AR;

[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<Current<ARSetup.migrationMode>, Equal<True>, Or<PX.Objects.IN.InventoryItem.stkItem, NotEqual<True>>>), "Inventory Item was not found.", new Type[] {})]
[PXRestrictor(typeof (Where<Current<ARSetup.migrationMode>, Equal<True>, Or<Current<ARTran.sOOrderNbr>, IsNotNull, Or<PX.Objects.IN.InventoryItem.stkItem, NotEqual<False>, Or<PX.Objects.IN.InventoryItem.kitItem, NotEqual<True>>>>>), "A non-stock kit cannot be added to a document manually. Use the Sales Orders (SO301000) form to prepare an invoice for the corresponding sales order.", new Type[] {})]
[PXRestrictor(typeof (Where<Current<ARSetup.migrationMode>, Equal<True>, Or<PX.Objects.IN.InventoryItem.stkItem, NotEqual<False>, Or<PX.Objects.IN.InventoryItem.kitItem, NotEqual<True>>>>), "A non-stock kit cannot be added to a cash transaction.", new Type[] {})]
public class ARTranInventoryItemAttribute : InventoryAttribute
{
  public ARTranInventoryItemAttribute()
    : base(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<Match<Current<AccessInfo.userName>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr))
  {
  }
}
