// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AnyInventoryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// Provides a base selector for the Inventory Items. The list is filtered by the user access rights and excludes Template and Unknown items.
/// </summary>
[PXRestrictor(typeof (Where<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>>), "Item reserved for Project Module to represent N/A item.", new Type[] {}, ShowWarning = true)]
[PXRestrictor(typeof (Where<InventoryItem.isTemplate, Equal<False>>), "The inventory item is a template item.", new Type[] {}, ShowWarning = true)]
public class AnyInventoryAttribute : BaseInventoryAttribute
{
  public AnyInventoryAttribute()
    : this(typeof (Search<InventoryItem.inventoryID, Where<Match<Current<AccessInfo.userName>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))
  {
  }

  public AnyInventoryAttribute(Type SearchType, Type SubstituteKey, Type DescriptionField)
    : base(SearchType, SubstituteKey, DescriptionField)
  {
  }

  public AnyInventoryAttribute(
    Type SearchType,
    Type SubstituteKey,
    Type DescriptionField,
    Type[] fields)
    : base(SearchType, SubstituteKey, DescriptionField, fields)
  {
  }
}
