// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionInventoryItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.RQ;

/// <summary>
/// Selector.<br />
/// Show inventory items available for using in requisition.<br />
/// Hide Inventory Items witch restricted for current logged in user, <br />
/// With status inactive, marked for deletion, no purchases and no request. <br />
/// </summary>
[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noPurchases>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noRequest>>>), "Item cannot be purchased", new Type[] {})]
public class RQRequisitionInventoryItemAttribute : CrossItemAttribute
{
  public RQRequisitionInventoryItemAttribute()
    : base(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<Match<Current<AccessInfo.userName>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), INPrimaryAlternateType.VPN)
  {
  }
}
