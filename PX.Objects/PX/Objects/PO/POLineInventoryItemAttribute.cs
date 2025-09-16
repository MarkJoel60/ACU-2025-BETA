// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLineInventoryItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// Specialized for POLine version of the CrossItemAttribute.<br />
/// Providing an Inventory ID selector for the field, it allows also user <br />
/// to select both InventoryID and SubItemID by typing AlternateID in the control<br />
/// As a result, if user type a correct Alternate id, values for InventoryID, SubItemID, <br />
/// and AlternateID fields in the row will be set.<br />
/// In this attribute, InventoryItems with a status inactive, markedForDeletion,<br />
/// noPurchase and noRequest are filtered out. It also fixes  INPrimaryAlternateType parameter to VPN <br />
/// This attribute may be used in combination with AlternativeItemAttribute on the AlternateID field of the row <br />
/// <example>
/// [POLineInventoryItem(Filterable = true)]
/// </example>
/// </summary>
[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noPurchases>>), "Item cannot be purchased", new Type[] {})]
public class POLineInventoryItemAttribute : CrossItemAttribute
{
  /// <summary>Default ctor</summary>
  public POLineInventoryItemAttribute()
    : base(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<Match<Current<AccessInfo.userName>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), INPrimaryAlternateType.VPN)
  {
  }
}
