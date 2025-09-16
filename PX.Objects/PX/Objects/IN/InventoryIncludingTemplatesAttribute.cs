// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryIncludingTemplatesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// Provides a selector for the Inventory Items including Template Items.
/// The list is filtered by the user access rights and Inventory Item status - inactive and marked to delete items are not shown.
/// </summary>
[PXRestrictor(typeof (Where<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>>), "Item reserved for Project Module to represent N/A item.", new Type[] {}, ShowWarning = true)]
[PXRestrictor(typeof (Where<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>), "The inventory item is {0}.", new Type[] {typeof (InventoryItem.itemStatus)}, ShowWarning = true)]
public class InventoryIncludingTemplatesAttribute : BaseInventoryAttribute
{
  public InventoryIncludingTemplatesAttribute()
  {
  }

  public InventoryIncludingTemplatesAttribute(
    Type SearchType,
    Type SubstituteKey,
    Type DescriptionField)
    : base(SearchType, SubstituteKey, DescriptionField)
  {
  }

  public InventoryIncludingTemplatesAttribute(
    Type SearchType,
    Type SubstituteKey,
    Type DescriptionField,
    Type[] fields)
    : base(SearchType, SubstituteKey, DescriptionField, fields)
  {
  }
}
