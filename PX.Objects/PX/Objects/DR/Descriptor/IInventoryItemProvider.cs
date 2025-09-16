// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.Descriptor.IInventoryItemProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.IN;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.DR.Descriptor;

public interface IInventoryItemProvider
{
  /// <summary>
  /// Given an inventory item ID and the required component allocation method,
  /// retrieves all inventory item components matching this method, along
  /// with the corresponding deferral codes in the form of
  /// <see cref="T:PX.Objects.DR.Descriptor.InventoryItemComponentInfo" />. If the allocation method does
  /// not support deferral codes, them <see cref="P:PX.Objects.DR.Descriptor.InventoryItemComponentInfo.DeferralCode" />
  /// will be <c>null</c>.
  /// </summary>
  IEnumerable<InventoryItemComponentInfo> GetInventoryItemComponents(
    int? inventoryItemID,
    string allocationMethod);

  /// <summary>
  /// Given an inventory item component, returns the corresponding
  /// substitute natural key - component name that as specified by
  /// <see cref="P:PX.Objects.IN.InventoryItem.InventoryCD" />.
  /// </summary>
  string GetComponentName(INComponent component);

  /// <summary>Returns the InventoryItem item by Id</summary>
  InventoryItem GetInventoryItemByID(int? inventoryItemID);
}
